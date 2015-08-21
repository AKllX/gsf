'*******************************************************************************************************
'  TVA.Measurements.FrameQueue.vb - Implementation of a queue of IFrame's
'  Copyright � 2006 - TVA, all rights reserved - Gbtc
'
'  Build Environment: VB.NET, Visual Studio 2005
'  Primary Developer: J. Ritchie Carroll, Operations Data Architecture [TVA]
'      Office: COO - TRNS/PWR ELEC SYS O, CHATTANOOGA, TN - MR 2W-C
'       Phone: 423/751-2827
'       Email: jrcarrol@tva.gov
'
'  Code Modification History:
'  -----------------------------------------------------------------------------------------------------
'  11/01/2007 - J. Ritchie Carroll
'       Initial version of source generated
'  11/08/2007 - J. Ritchie Carroll
'       Optimized "Pop" call to be a no-wait operation
'  02/19/2008 - J. Ritchie Carroll
'       Added code to detect and avoid redundant calls to Dispose().
'
'*******************************************************************************************************

Imports System.Threading
Imports TVA.Collections
Imports TVA.DateTime
Imports TVA.DateTime.Common

Namespace Measurements

    Public Delegate Function CreateNewFrameFunctionSignature(ByVal ticks As Long) As IFrame

    Public Class FrameQueue

        Implements IDisposable

        Private m_frameList As LinkedList(Of IFrame)        ' We keep this list sorted by timestamp so frames are processed in order
        Private m_frameHash As Dictionary(Of Long, IFrame)  ' This list not guaranteed to be sorted, but used for fast frame lookup
        Private m_popQueue As ProcessQueue(Of Long)
        Private m_syncSignal As AutoResetEvent
        Private m_publishedTicks As Long
        Private m_head, m_last As IFrame
        Private m_ticksPerFrame As Decimal
        Private m_createNewFrameFunction As CreateNewFrameFunctionSignature
        Private m_missedLocks As Long
        Private m_lockAttempts As Long
        Private m_disposed As Boolean = False

        Public Sub New(ByVal ticksPerFrame As Decimal, ByVal initialCapacity As Integer, ByVal createNewFrameFunction As CreateNewFrameFunctionSignature)

            m_frameList = New LinkedList(Of IFrame)
            m_frameHash = New Dictionary(Of Long, IFrame)(initialCapacity)
            m_popQueue = ProcessQueue(Of Long).CreateRealTimeQueue(AddressOf PopStub, AddressOf CanPop)
            m_syncSignal = New AutoResetEvent(False)

            m_ticksPerFrame = ticksPerFrame
            m_createNewFrameFunction = createNewFrameFunction

        End Sub

        Protected Overrides Sub Finalize()

            Dispose(True)

        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)

            If Not m_disposed Then
                If disposing Then
                    If m_popQueue IsNot Nothing Then m_popQueue.Dispose()
                    m_popQueue = Nothing
                    If m_syncSignal IsNot Nothing Then m_syncSignal.Close()
                    m_syncSignal = Nothing
                    If m_frameList IsNot Nothing Then m_frameList.Clear()
                    m_frameList = Nothing
                    If m_frameHash IsNot Nothing Then m_frameHash.Clear()
                    m_frameHash = Nothing
                    m_createNewFrameFunction = Nothing
                End If
            End If

            m_disposed = True

        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose

            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)

        End Sub

        Public Sub Start()

            m_popQueue.Start()

        End Sub

        Public Sub [Stop]()

            m_popQueue.Stop()

        End Sub

        Public ReadOnly Property MissedLocks() As Long
            Get
                Return m_missedLocks
            End Get
        End Property

        Public ReadOnly Property LockAttempts() As Long
            Get
                Return m_lockAttempts
            End Get
        End Property

        Public Property TicksPerFrame() As Decimal
            Get
                Return m_ticksPerFrame
            End Get
            Set(ByVal value As Decimal)
                m_ticksPerFrame = value
            End Set
        End Property

        Public ReadOnly Property CreateNewFrameFunction() As CreateNewFrameFunctionSignature
            Get
                Return m_createNewFrameFunction
            End Get
        End Property

        Public Sub Pop()

            ' We track latest published ticks - don't want to allow slow moving measurements
            ' to inject themselves after a certain publication timeframe has passed - this
            ' avoids any possible out-of-sequence frame publication...
            m_last = m_head
            m_head = Nothing
            m_publishedTicks = m_last.Ticks

            ' Frame's already been handled so there's no rush in removing it
            m_popQueue.Add(m_publishedTicks)

        End Sub

        Private Function CanPop(ByVal publishedTicks As Long) As Boolean

            ' We didn't try for an immediate lock to remove top frame from original
            ' "Pop" call - this call was running on the already burdened publication
            ' thread and we need to give consumer as much of the available timeslice
            ' as possible. So now we're running on an independent thread and we'll
            ' hang around until we can get our work done. This process "smooths" the
            ' frame publication process by not waiting on lock synchrnonization
            ' for queue clean up - the separate thread should also avoid a potential
            ' deadlock that could be caused by waiting for locks between the frame
            ' queue and frame measurements, i.e., sorting vs. publication contention

            ' Attempt a lock, if we get it handle pop - otherwise return False and
            ' process queue will retry...
            m_lockAttempts += 1

            If Monitor.TryEnter(m_frameList) Then
                Try
                    m_frameList.RemoveFirst()

                    If m_frameList.Count > 0 Then
                        m_head = m_frameList.First.Value
                    Else
                        m_head = Nothing
                    End If

                    ' Release waiting thread...
                    m_syncSignal.Set()

                    m_frameHash.Remove(publishedTicks)
                Finally
                    Monitor.Exit(m_frameList)
                End Try

                Return True
            End If

            m_missedLocks += 1
            Return False

        End Function

        Private Sub PopStub(ByVal publishedTicks As Long)

            ' CanPop does the real work - nothing for us to do now... :)

        End Sub

        Public ReadOnly Property Head() As IFrame
            Get
                ' Wait until new head has been assigned...
                m_syncSignal.WaitOne()

                ' We track the head separately to avoid sync-lock on frame list to safely access first item...
                Return m_head
            End Get
        End Property

        Public ReadOnly Property Last() As IFrame
            Get
                Return m_last
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return m_frameList.Count
            End Get
        End Property

        Public Function GetFrame(ByVal ticks As Long) As IFrame

            ' Calculate destination ticks for this frame
            Dim destinationTicks As Long = CLng(ticks / m_ticksPerFrame) * m_ticksPerFrame
            Dim frame As IFrame
            Dim nodeAdded As Boolean

            ' Make sure ticks are newer than latest published ticks...
            If destinationTicks > m_publishedTicks Then
                ' Wait for queue lock - we wait because calling function demands a destination frame
                SyncLock m_frameList
                    If Not m_frameHash.TryGetValue(destinationTicks, frame) Then
                        ' Didn't find frame for this timestamp so we create one
                        frame = m_createNewFrameFunction(destinationTicks)

                        If m_frameList.Count > 0 Then
                            ' Insert frame into proper sorted position...
                            Dim node As LinkedListNode(Of IFrame) = m_frameList.Last

                            Do
                                If destinationTicks > node.Value.Ticks Then
                                    m_frameList.AddAfter(node, frame)
                                    nodeAdded = True
                                    Exit Do
                                End If

                                node = node.Previous
                            Loop Until node Is Nothing
                        End If

                        If Not nodeAdded Then
                            m_frameList.AddFirst(frame)
                            m_head = frame
                            m_syncSignal.Set()
                        End If

                        ' Since we'll be requesting this frame over and over, we'll use
                        ' a hash table for quick frame lookups by timestamp
                        m_frameHash.Add(destinationTicks, frame)
                    End If
                End SyncLock
            End If

            Return frame

        End Function

        'Private Sub LoadFramesProc()

        '    'Dim framesPerSecond As Integer = CInt(CDec(TicksPerSecond) / m_ticksPerFrame)
        '    Dim x, destinationTicks As Long
        '    Dim frame As IFrame
        '    Dim frameIndex As Integer

        '    Do While True
        '        ' Attempt a lock, no need to wait...
        '        If Monitor.TryEnter(m_frames) Then
        '            Try
        '                ' We have a lock now, so let's check to see if we need to add some frames,
        '                ' we'll try to keep a full second's worth of future frames out there
        '                frame = Nothing

        '                For x = m_currentTicks To m_currentTicks + TicksPerSecond Step m_ticksPerFrame
        '                    destinationTicks = CLng(x / m_ticksPerFrame) * m_ticksPerFrame
        '                    frameIndex = m_frames.BinarySearch(New Frame(destinationTicks))

        '                    If frameIndex < 0 Then
        '                        ' Didn't find frame for this timestamp so we create one
        '                        frame = m_createNewFrameFunction(destinationTicks)
        '                        m_frames.Add(frame)
        '                    End If
        '                Next

        '                If frame IsNot Nothing Then
        '                    If m_tail Is Nothing OrElse frame.CompareTo(m_tail) > 0 Then
        '                        m_tail = frame
        '                    Else
        '                        m_frames.Sort()
        '                    End If

        '                    If m_head Is Nothing AndAlso m_frames.Count = 1 Then m_head = m_tail
        '                End If
        '            Finally
        '                Monitor.Exit(m_frames)
        '            End Try
        '        End If

        '        ' This is a lazy process, we are always snoozing and trying again...
        '        Thread.Sleep(10)
        '    Loop

        'End Sub

    End Class

End Namespace