'*******************************************************************************************************
'  TVA.Console.Common.vb - Common Configuration Functions
'  Copyright � 2006 - TVA, all rights reserved - Gbtc
'
'  Build Environment: VB.NET, Visual Studio 2005
'  Primary Developer: Pinal C. Patel, Operations Data Architecture [TVA]
'      Office: COO - TRNS/PWR ELEC SYS O, CHATTANOOGA, TN - MR 2W-C
'       Phone: 423/751-2250
'       Email: pcpatel@tva.gov
'
'  Code Modification History:
'  -----------------------------------------------------------------------------------------------------
'  12/28/2006 - Pinal C. Patel
'       Generated original version of source code.
'  08/31/2007 - Darrell Zuercher
'       Edited code comments.
'
'*******************************************************************************************************

Option Explicit On

Imports System.Text
Imports System.Runtime.InteropServices

Namespace Console

    Public NotInheritable Class Common

#Region " Member Declaration "

        Private Enum ConsoleEventType As UInteger
            CancelKeyPress = 0
            BreakKeyPress = 1
            ConsoleClosing = 2
            UserLoggingOff = 5
            SystemShutdown = 6
        End Enum

        Private Shared m_handler As ConsoleWindowEventHandler

        Private Delegate Function ConsoleWindowEventHandler(ByVal controlType As ConsoleEventType) As Boolean

#End Region

#Region " Event Declaration "

        Public Shared Event CancelKeyPress As EventHandler(Of System.ComponentModel.CancelEventArgs)

        Public Shared Event BreakKeyPress As EventHandler(Of System.ComponentModel.CancelEventArgs)

        Public Shared Event ConsoleClosing As EventHandler(Of System.ComponentModel.CancelEventArgs)

        Public Shared Event UserLoggingOff As EventHandler

        Public Shared Event SystemShutdown As EventHandler

#End Region

#Region " Public Code "

        ' VB does not provide you with an array of tokenized command line arguments; they are all in one string.  
        ' So, this function creates the desired tokenized argument array from the VB command line.
        ' This function will always return at least one argument, even if it is an empty string.
        Public Shared Function ParseCommand(ByVal command As String) As String()

            Dim parsedCommand As New List(Of String)()
            If command.Length > 0 Then
                Dim quotedArgument As String = ""
                Dim encodedQuote = Guid.NewGuid.ToString()
                Dim encodedSpace = Guid.NewGuid.ToString()
                Dim encodedCommand As New StringBuilder()
                Dim argumentInQuotes As Boolean
                Dim currentCharacter As Char

                ' Encodes embedded quotes. It allows embedded/nested quotes encoded as \".
                command = Replace(command, "\""", encodedQuote)

                ' Combines any quoted strings into a single arg by encoding embedded spaces.
                For x As Integer = 0 To command.Length - 1
                    currentCharacter = command.Chars(x)

                    If currentCharacter = """"c Then
                        If argumentInQuotes Then
                            argumentInQuotes = False
                        Else
                            argumentInQuotes = True
                        End If
                    End If

                    If argumentInQuotes Then
                        If currentCharacter = " "c Then
                            encodedCommand.Append(encodedSpace)
                        Else
                            encodedCommand.Append(currentCharacter)
                        End If
                    Else
                        encodedCommand.Append(currentCharacter)
                    End If
                Next

                command = encodedCommand.ToString()

                ' Parses every argument out by space and combine any quoted strings into a single arg.
                For Each argument As String In command.Split(" "c)
                    ' Adds tokenized argument, making sure to unencode any embedded quotes or spaces.
                    argument = Trim(Replace(Replace(argument, encodedQuote, """"), encodedSpace, " "))
                    If argument.Length > 0 Then parsedCommand.Add(argument)
                Next
            End If

            Return parsedCommand.ToArray()

        End Function

        Public Shared Sub EnableRaisingEvents()

            ' Member variable is used here so that the delegate is not garbage collected by the time it is called 
            ' by WIN API when any of the control events take place.
            ' http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=996045&SiteID=1
            m_handler = AddressOf HandleConsoleWindowEvents
            SetConsoleWindowEventRaising(m_handler, True)

        End Sub

        Public Shared Sub DisableRaisingEvents()

            m_handler = AddressOf HandleConsoleWindowEvents
            SetConsoleWindowEventRaising(m_handler, False)

        End Sub

#End Region

#Region " Private Code "

        Private Sub New()

            ' This class contains only global functions and is not meant to be instantiated

        End Sub

        <DllImport("kernel32.dll", EntryPoint:="SetConsoleCtrlHandler")> _
        Private Shared Function SetConsoleWindowEventRaising(ByVal handler As ConsoleWindowEventHandler, ByVal enable As Boolean) As Boolean

        End Function

        Private Shared Function HandleConsoleWindowEvents(ByVal controlType As ConsoleEventType) As Boolean

            ' ms-help://MS.VSCC.v80/MS.MSDN.v80/MS.WIN32COM.v10.en/dllproc/base/handlerroutine.htm

            ' When this function does not return True, the default handler is called and the default action takes 
            ' place.
            Select Case controlType
                Case ConsoleEventType.CancelKeyPress
                    Dim ctrlCKeyPressEventData As New System.ComponentModel.CancelEventArgs()
                    RaiseEvent CancelKeyPress(Nothing, ctrlCKeyPressEventData)
                    If ctrlCKeyPressEventData.Cancel Then Return True
                Case ConsoleEventType.BreakKeyPress
                    Dim ctrlBreakKeyPressEventData As New System.ComponentModel.CancelEventArgs()
                    RaiseEvent BreakKeyPress(Nothing, ctrlBreakKeyPressEventData)
                    If ctrlBreakKeyPressEventData.Cancel Then Return True
                Case ConsoleEventType.ConsoleClosing
                    Dim consoleClosingEventData As New System.ComponentModel.CancelEventArgs()
                    RaiseEvent ConsoleClosing(Nothing, consoleClosingEventData)
                    If consoleClosingEventData.Cancel Then Return True
                Case ConsoleEventType.UserLoggingOff
                    RaiseEvent UserLoggingOff(Nothing, EventArgs.Empty)
                Case ConsoleEventType.SystemShutdown
                    RaiseEvent SystemShutdown(Nothing, EventArgs.Empty)
            End Select

            Return False

        End Function

#End Region

    End Class

End Namespace