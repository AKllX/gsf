' 08-29-06

Imports System.ComponentModel
Imports System.ServiceProcess
Imports Tva.Services
Imports Tva.Tro.Ssam
Imports Tva.Communication
Imports Tva.Serialization

Public Class ServiceHelper

    Private m_service As ServiceBase
    Private m_processes As Dictionary(Of String, ServiceProcess)
    Private m_clientInfo As Dictionary(Of Guid, ClientInfo)
    Private m_serviceComponents As List(Of IServiceComponent)
    Private m_startedEventHandlerList As List(Of EventHandler)
    Private m_stoppedEventHandlerList As List(Of EventHandler)

    ''' <summary>
    ''' Occurs when the service has started.
    ''' </summary>
    ''' <remarks>This is a non-blocking event.</remarks>
    Public Custom Event Started As EventHandler
        AddHandler(ByVal value As EventHandler)
            m_startedEventHandlerList.Add(value)
        End AddHandler

        RemoveHandler(ByVal value As EventHandler)
            m_startedEventHandlerList.Remove(value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)
            For Each handler As EventHandler In m_startedEventHandlerList
                handler.BeginInvoke(sender, e, Nothing, Nothing)
            Next
        End RaiseEvent
    End Event

    ''' <summary>
    ''' Occurs when the service has stopped.
    ''' </summary>
    ''' <remarks>This is a non-blocking event.</remarks>
    Public Custom Event Stopped As EventHandler
        AddHandler(ByVal value As EventHandler)
            m_startedEventHandlerList.Add(value)
        End AddHandler

        RemoveHandler(ByVal value As EventHandler)
            m_startedEventHandlerList.Remove(value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)
            For Each handler As EventHandler In m_startedEventHandlerList
                handler.BeginInvoke(sender, e, Nothing, Nothing)
            Next
        End RaiseEvent
    End Event

    ''' <summary>
    ''' Occurs when the service is paused.
    ''' </summary>
    Public Event Paused As EventHandler

    ''' <summary>
    ''' Occurs when the service is resumed.
    ''' </summary>
    Public Event Resumed As EventHandler

    ''' <summary>
    ''' Occurs when the system is being shutdowm.
    ''' </summary>
    Public Event Shutdown As EventHandler

    ''' <summary>
    ''' Occurs when a request is received from a client.
    ''' </summary>
    ''' <param name="clientID">ID of the client that sent the request.</param>
    ''' <param name="clientRequest">The request sent by the client.</param>
    Public Event ReceivedClientRequest(ByVal clientID As Guid, ByRef clientRequest As ClientRequest)

    ''' <summary>
    ''' Gets or sets the parent service to which the service helper belongs.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The parent service to which the service helper belongs.</returns>
    Public Property Service() As ServiceBase
        Get
            Return m_service
        End Get
        Set(ByVal value As ServiceBase)
            m_service = value
        End Set
    End Property

    <Browsable(False)> _
    Public ReadOnly Property Processes() As Dictionary(Of String, ServiceProcess)
        Get
            Return m_processes
        End Get
    End Property

    ''' <summary>
    ''' Gets the instance of TCP server used for communicating with the clients.
    ''' </summary>
    ''' <value></value>
    ''' <returns>An instance of TCP server.</returns>
    <TypeConverter(GetType(ExpandableObjectConverter)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property TcpServer() As TcpServer
        Get
            Return SHTcpServer
        End Get
    End Property

    ''' <summary>
    ''' Gets the instance of schedule manager that can be used for scheduling jobs/tasks.
    ''' </summary>
    ''' <value></value>
    ''' <returns>An instance of schedule manager.</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property ScheduleManager() As ScheduleManager
        Get
            Return SHScheduleManager
        End Get
    End Property

    ''' <summary>
    ''' Gets the instance of SSAM logger that can be used to log events to SSAM.
    ''' </summary>
    ''' <value></value>
    ''' <returns>An instance of SSAM logger.</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property SsamLogger() As SsamLogger
        Get
            Return SHSsamLogger
        End Get
    End Property

    ''' <summary>
    ''' Gets a list of all the components that implement the Tva.Services.IServiceComponent interface.
    ''' </summary>
    ''' <value></value>
    ''' <returns>An instance of System.Collections.Generic.List(Of Tva.Services.IServiceComponent).</returns>
    <Browsable(False)> _
    Public ReadOnly Property ServiceComponents() As List(Of IServiceComponent)
        Get
            Return m_serviceComponents
        End Get
    End Property

    ''' <summary>
    ''' To be called when the service is starts (inside the service's OnStart method).
    ''' </summary>
    Public Sub OnStart()

        For Each component As IServiceComponent In m_serviceComponents
            component.ServiceStateChanged(ServiceState.Started)
        Next

        RaiseEvent Started(Me, EventArgs.Empty)

    End Sub

    ''' <summary>
    ''' To be called when the service is stopped (inside the service's OnStop method).
    ''' </summary>
    Public Sub OnStop()

        For Each component As IServiceComponent In m_serviceComponents
            component.ServiceStateChanged(ServiceState.Stopped)
        Next

        RaiseEvent Stopped(Me, EventArgs.Empty)

    End Sub

    ''' <summary>
    ''' To be called when the service is paused (inside the service's OnPause method).
    ''' </summary>
    Public Sub OnPause()

        For Each component As IServiceComponent In m_serviceComponents
            component.ServiceStateChanged(ServiceState.Paused)
        Next

        RaiseEvent Paused(Me, EventArgs.Empty)

    End Sub

    ''' <summary>
    ''' To be called when the service is resumed (inside the service's OnContinue method).
    ''' </summary>
    Public Sub OnResume()

        For Each component As IServiceComponent In m_serviceComponents
            component.ServiceStateChanged(ServiceState.Resumed)
        Next

        RaiseEvent Resumed(Me, EventArgs.Empty)

    End Sub

    ''' <summary>
    ''' To be when the system is shutting down (inside the service's OnShutdown method).
    ''' </summary>
    Public Sub OnShutdown()

        For Each component As IServiceComponent In m_serviceComponents
            component.ServiceStateChanged(ServiceState.Shutdown)
        Next

        RaiseEvent Shutdown(Me, EventArgs.Empty)

    End Sub

    ''' <summary>
    ''' To be called when the state of a process changes.
    ''' </summary>
    ''' <param name="processName">Name of the process whose state changed.</param>
    ''' <param name="processState">New state of the process.</param>
    Public Sub ProcessStateChanged(ByVal processName As String, ByVal processState As ProcessState)

        For Each component As IServiceComponent In m_serviceComponents
            component.ProcessStateChanged(processName, processState)
        Next

    End Sub

    Public Sub AddProcess(ByVal processExecutionMethod As ServiceProcess.ExecutionMethodSignature, _
            ByVal processName As String)

        AddProcess(processExecutionMethod, processName)

    End Sub

    Public Sub AddProcess(ByVal processExecutionMethod As ServiceProcess.ExecutionMethodSignature, _
            ByVal processName As String, ByVal processParameters As Object())

        If Not m_processes.ContainsKey(processName) Then
            m_processes.Add(processName, New ServiceProcess(processExecutionMethod, processName, processParameters, Me))
        Else
            Throw New InvalidOperationException("Process '" & processName & "' already exists.")
        End If

    End Sub

    Public Sub AddScheduledProcess(ByVal processExecutionMethod As ServiceProcess.ExecutionMethodSignature, _
            ByVal processName As String, ByVal processSchedule As String)

        AddScheduledProcess(processExecutionMethod, processName, Nothing, processSchedule)

    End Sub

    Public Sub AddScheduledProcess(ByVal processExecutionMethod As ServiceProcess.ExecutionMethodSignature, _
            ByVal processName As String, ByVal processParameters As Object(), ByVal processSchedule As String)

        AddProcess(processExecutionMethod, processName, processParameters)
        ScheduleProcess(processName, processSchedule)

    End Sub

    Public Sub ScheduleProcess(ByVal processName As String, ByVal processSchedule As String)

        If m_processes.ContainsKey(processName) Then
            Dim schedule As Schedule = Nothing
            If Not SHScheduleManager.Schedules.TryGetValue(processName, schedule) Then
                ' Update the process schedule if it is already exists.
                schedule.Rule = processSchedule
            Else
                ' Schedule the process if it is not scheduled already.
                schedule = New Schedule(processName)
                schedule.Rule = processSchedule
                SHScheduleManager.Schedules.Add(processName, schedule)
            End If
        Else
            Throw New InvalidOperationException("Process '" & processName & "' does not exist.")
        End If

    End Sub

    Public Sub SendUpdateStatusResponse(ByVal response As String)

        Dim serviceResponse As New ServiceResponse()
        serviceResponse.Type = "UPDATESTATUS"
        serviceResponse.Message = response
        SendResponse(serviceResponse)

    End Sub

    Public Sub SendUpdateStatusResponse(ByVal clientID As Guid, ByVal response As String)

        Dim serviceResponse As New ServiceResponse()
        serviceResponse.Type = "UPDATESTATUS"
        serviceResponse.Message = response
        SendResponse(clientID, serviceResponse)

    End Sub

    ''' <summary>
    ''' Sends the specified response to all of the connected clients.
    ''' </summary>
    ''' <param name="response">The response to be sent to the clients.</param>
    Public Sub SendResponse(ByVal response As ServiceResponse)

        SHTcpServer.Multicast(response)

    End Sub

    ''' <summary>
    ''' Sends the specified resonse to the specified client only.
    ''' </summary>
    ''' <param name="clientID">ID of the client to whom the response is to be sent.</param>
    ''' <param name="response">The response to be sent to the client.</param>
    Public Sub SendResponse(ByVal clientID As Guid, ByVal response As ServiceResponse)

        SHTcpServer.SendTo(clientID, response)

    End Sub

#Region " TcpServer Events "

    Private Sub SHTcpServer_ClientConnected(ByVal clientID As System.Guid) Handles SHTcpServer.ClientConnected

        m_clientInfo.Add(clientID, Nothing)

    End Sub

    Private Sub SHTcpServer_ClientDisconnected(ByVal clientID As System.Guid) Handles SHTcpServer.ClientDisconnected

        m_clientInfo.Remove(clientID)

    End Sub

    Private Sub SHTcpServer_ReceivedClientData(ByVal clientID As System.Guid, ByVal data() As System.Byte) Handles SHTcpServer.ReceivedClientData

        Dim info As ClientInfo = GetObject(Of ClientInfo)(data)
        Dim request As ClientRequest = GetObject(Of ClientRequest)(data)

        If info IsNot Nothing Then
            m_clientInfo(clientID) = info
        ElseIf request IsNot Nothing Then
            RaiseEvent ReceivedClientRequest(clientID, request)

            If Not request.ServiceHandled Then
                ' We'll process the request only if the service didn't handle it.
                Select Case request.Type.ToUpper()
                    Case "LISTPROCESSES"
                    Case "STARTPROCESS"
                    Case "ABORTPROCESS"
                    Case "UNSCHEDULEPROCESS"
                    Case "RESCHEDULEPROCESS"
                    Case "PINGSERVICE"
                        'HandlePingServiceRequest()
                    Case "PINGALLCLIENTS"
                        'HandlePingAllClientsRequest()
                    Case "LISTALLCLIENTS"
                        'HandleListAllClientsRequest()
                    Case "GETSERVICESTATUS"
                        'HandlePingServiceRequest()
                    Case "GETPROCESSSTATUS"
                        'HandleGetProcessStatusRequest()
                    Case "GETCOMMANDHISTORY"
                        'HandleGetCommandHistoryRequest()
                    Case "GETDIRECTORYLISTING"
                        'HandleGetRequest()
                    Case "LISTSETTINGS"
                    Case "UPDATESETTING"
                    Case "SAVESETTINGS"
                    Case Else

                End Select
            End If
        Else

        End If

    End Sub

#End Region

#Region " ScheduleManager Events "

    Private Sub SHScheduleManager_ScheduleDue(ByVal schedule As Schedule) Handles SHScheduleManager.ScheduleDue

        Dim scheduledProcess As ServiceProcess = Nothing
        If m_processes.TryGetValue(schedule.Name, scheduledProcess) Then
            scheduledProcess.StartProcess() ' Start the process execution if it exists.
        End If

    End Sub

#End Region

#Region " Private Methods "

#End Region

End Class