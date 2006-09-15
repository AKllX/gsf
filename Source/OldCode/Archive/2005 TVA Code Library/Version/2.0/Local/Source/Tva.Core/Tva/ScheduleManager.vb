' 08-01-06

Option Strict On

Imports System.Drawing
Imports System.ComponentModel
Imports System.Threading
Imports Tva.Services
Imports Tva.Configuration
Imports Tva.Configuration.Common

<ToolboxBitmap(GetType(ScheduleManager)), DefaultEvent("ScheduleDue")> _
Public Class ScheduleManager
    Implements IServiceComponent

    Private m_configurationElement As String
    Private m_persistSchedules As Boolean
    Private m_enabled As Boolean
    Private m_schedules As Dictionary(Of String, Schedule)
    Private m_startTimerThread As Thread
    Private m_scheduleDueEventHandlerList As List(Of ScheduleDueEventHandler)
    Private WithEvents m_timer As System.Timers.Timer

    Public Delegate Sub ScheduleDueEventHandler(ByVal schedule As Schedule)

    ''' <summary>
    ''' Occurs while the schedule manager is waiting to start at top of the minute.
    ''' </summary>
    <Category("State")> _
    Public Event Starting As EventHandler

    ''' <summary>
    ''' Occurs when the schedule manager has started.
    ''' </summary>
    <Category("State")> _
    Public Event Started As EventHandler

    ''' <summary>
    ''' Occurs when the schedule manager has stopped.
    ''' </summary>
    <Category("State")> _
    Public Event Stopped As EventHandler

    ''' <summary>
    ''' Occurs when the a particular schedule is being checked to see if it is due.
    ''' </summary>
    ''' <param name="schedule">The schedule that is being checked.</param>
    <Category("Schedules")> _
    Public Event CheckingSchedule(ByVal schedule As Schedule)

    ''' <summary>
    ''' Occurs when a particular schedule is due.
    ''' </summary>
    ''' <remarks>This is a non-blocking event.</remarks>
    <Category("Schedules")> _
    Public Custom Event ScheduleDue As ScheduleDueEventHandler
        AddHandler(ByVal value As ScheduleDueEventHandler)
            m_scheduleDueEventHandlerList.Add(value)
        End AddHandler

        RemoveHandler(ByVal value As ScheduleDueEventHandler)
            m_scheduleDueEventHandlerList.Remove(value)
        End RemoveHandler

        RaiseEvent(ByVal schedule As Schedule)
            For Each handler As ScheduleDueEventHandler In m_scheduleDueEventHandlerList
                handler.BeginInvoke(schedule, Nothing, Nothing)
            Next
        End RaiseEvent
    End Event

    Public Sub New(ByVal persistSchedules As Boolean)
        MyBase.New()
        Me.ConfigurationElement = "ScheduleManager"
        Me.PersistSchedules = persistSchedules
        Me.Enabled = True
        m_schedules = New Dictionary(Of String, Schedule)()
        m_scheduleDueEventHandlerList = New List(Of ScheduleDueEventHandler)()
        m_timer = New System.Timers.Timer(60000)
    End Sub

    ''' <summary>
    ''' Gets or sets the element name of the application configuration file under which the schedules will be saved.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The element name of the application configuration file under which the schedules will be saved.</returns>
    <Category("Configuration"), DefaultValue(GetType(String), "ScheduleManager")> _
    Public Property ConfigurationElement() As String
        Get
            Return m_configurationElement
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                m_configurationElement = value
            Else
                Throw New ArgumentNullException("value")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a boolean value indicating whether the schedules will be saved to the application 
    ''' configuration file when this instance of Tva.ScheduleManager is stopped or disposed.
    ''' </summary>
    ''' <value>True if the schedules will be saved to the application configuration file; otherwise False.</value>
    <Category("Configuration"), DefaultValue(GetType(Boolean), "True")> _
    Public Property PersistSchedules() As Boolean
        Get
            Return m_persistSchedules
        End Get
        Set(ByVal value As Boolean)
            m_persistSchedules = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a boolean value indicating whether the schedule manager is enabled.
    ''' </summary>
    ''' <value></value>
    ''' <returns>True if the schedule manager is enabled; otherwise False.</returns>
    <Category("Behavior"), DefaultValue(GetType(Boolean), "True")> _
    Public Property Enabled() As Boolean
        Get
            Return m_enabled
        End Get
        Set(ByVal value As Boolean)
            m_enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Gets a boolean value indicating whether the schedule manager is running.
    ''' </summary>
    ''' <value></value>
    ''' <returns>True if the schedule manager is running; otherwise False.</returns>
    <Browsable(False)> _
    Public ReadOnly Property IsRunning() As Boolean
        Get
            Return m_timer.Enabled
        End Get
    End Property

    ''' <summary>
    ''' Gets a list of all the schedules.
    ''' </summary>
    ''' <value></value>
    ''' <returns>A list of the schedules.</returns>
    <Browsable(False)> _
    Public ReadOnly Property Schedules() As Dictionary(Of String, Schedule)
        Get
            Return m_schedules
        End Get
    End Property

    ''' <summary>
    ''' Starts the schedule manager asynchronously.
    ''' </summary>
    Public Sub Start()

        If Not m_timer.Enabled AndAlso m_enabled Then
            LoadSchedules()
            m_startTimerThread = New Thread(AddressOf StartTimer)
            m_startTimerThread.Start()
        End If

    End Sub

    ''' <summary>
    ''' Stops the schedule manager.
    ''' </summary>
    Public Sub [Stop]()

        If m_enabled Then
            If m_startTimerThread IsNot Nothing Then m_startTimerThread.Abort()
            If m_timer.Enabled Then
                m_timer.Stop()
                SaveSchedules()
                RaiseEvent Stopped(Me, EventArgs.Empty)
            End If
        End If

    End Sub

    ''' <summary>
    ''' Loads previously saved schedules from the application configuration file.
    ''' </summary>
    Public Sub LoadSchedules()

        If m_enabled AndAlso m_persistSchedules Then
            Try
                For Each savedSchedule As CategorizedSettingsElement In DefaultConfigFile.CategorizedSettings(m_configurationElement)
                    Dim schedule As New Schedule(savedSchedule.Name)
                    schedule.Rule = savedSchedule.Value
                    m_schedules(savedSchedule.Name) = schedule
                Next
            Catch ex As Exception
                ' We can safely ignore any exceptions encountered while loading schedules from the config file.
            End Try
        End If

    End Sub

    ''' <summary>
    ''' Saves all schedules to the application configuration file.
    ''' </summary>
    Public Sub SaveSchedules()

        If m_enabled AndAlso m_persistSchedules Then
            Try
                DefaultConfigFile.CategorizedSettings(m_configurationElement).Clear()
                For Each scheduleName As String In m_schedules.Keys
                    DefaultConfigFile.CategorizedSettings(m_configurationElement).Add(scheduleName, m_schedules(scheduleName).Rule)
                Next
                SaveSettings()
            Catch ex As Exception
                ' We can safely ignore any exceptions encountered while saving schedules to the config file.
            End Try
        End If

    End Sub

    ''' <summary>
    ''' Checks the specified schedule to determine if it is due.
    ''' </summary>
    ''' <param name="scheduleName">Name of the schedule to be checked.</param>
    Public Sub CheckSchedule(ByVal scheduleName As String)

        If m_enabled Then
            RaiseEvent CheckingSchedule(m_schedules(scheduleName))
            If m_schedules(scheduleName).IsDue() Then
                RaiseEvent ScheduleDue(m_schedules(scheduleName))   ' This event will be raised asynchronously.
            End If
        End If

    End Sub

    ''' <summary>
    ''' Checks all of the schedules to determine if they are due.
    ''' </summary>
    Public Sub CheckAllSchedules()

        If m_enabled Then
            For Each scheduleName As String In m_schedules.Keys
                CheckSchedule(scheduleName)
            Next
        End If

    End Sub

    Private Sub StartTimer()

        Do While True
            RaiseEvent Starting(Me, EventArgs.Empty)
            If System.DateTime.Now.Second = 0 Then
                m_timer.Start()
                RaiseEvent Started(Me, EventArgs.Empty)
                CheckAllSchedules()
                Exit Do
            End If
        Loop

        m_startTimerThread = Nothing

    End Sub

    Private Sub m_timer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles m_timer.Elapsed

        CheckAllSchedules()

    End Sub

#Region " IServiceComponent Implementation "

    Private m_previouslyEnabled As Boolean = False

    <Browsable(False)> _
    Public ReadOnly Property Name() As String Implements Services.IServiceComponent.Name
        Get
            Return Me.GetType().Name
        End Get
    End Property

    <Browsable(False)> _
    Public ReadOnly Property Status() As String Implements Services.IServiceComponent.Status
        Get
            With New System.Text.StringBuilder()
                .Append("        Number of schedules:")
                .Append(m_schedules.Count)
                .Append(Environment.NewLine)
                .Append(Environment.NewLine)
                For Each scheduleName As String In m_schedules.Keys
                    .Append(m_schedules(scheduleName).Status)
                    .Append(Environment.NewLine)
                Next

                Return .ToString()
            End With
        End Get
    End Property

    Public Sub ProcessStateChanged(ByVal processName As String, ByVal newState As Services.ProcessState) Implements Services.IServiceComponent.ProcessStateChanged

    End Sub

    Public Sub ServiceStateChanged(ByVal newState As Services.ServiceState) Implements Services.IServiceComponent.ServiceStateChanged

        Select Case newState
            Case ServiceState.Started
                Me.Start()
            Case ServiceState.Stopped
                Me.Stop()
            Case ServiceState.Paused
                m_previouslyEnabled = Enabled
                Me.Enabled = False
            Case ServiceState.Resumed
                Me.Enabled = m_previouslyEnabled
            Case ServiceState.Shutdown
                Me.Dispose()
        End Select

    End Sub

#End Region

End Class
