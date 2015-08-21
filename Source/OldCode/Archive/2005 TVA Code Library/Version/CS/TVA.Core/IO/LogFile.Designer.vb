Namespace IO

    Partial Class LogFile
        Inherits System.ComponentModel.Component

        <System.Diagnostics.DebuggerNonUserCode()> _
        Public Sub New(ByVal container As System.ComponentModel.IContainer)
            MyClass.New()

            'Required for Windows.Forms Class Composition Designer support.
            If (container IsNot Nothing) Then
                container.Add(Me)
            End If

        End Sub

        <System.Diagnostics.DebuggerNonUserCode()> _
        Public Sub New()
            MyBase.New()

            'Required by the Component Designer.
            InitializeComponent()

            m_name = DefaultName
            m_size = DefaultSize
            m_autoOpen = DefaultAutoOpen
            m_fileFullOperation = DefaultFileFullOperation
            m_persistSettings = DefaultPersistSettings
            m_settingsCategoryName = DefaultSettingsCategoryName

            m_operationWaitHandle = New System.Threading.ManualResetEvent(True)

            m_logEntryQueue = TVA.Collections.ProcessQueue(Of String).CreateSynchronousQueue(AddressOf WriteLogEntries)

        End Sub

        'Overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If Not m_disposed Then
                    Close()         ' Closes the file.
                    SaveSettings()  ' Saves settings to the config file.
                    If disposing AndAlso components IsNot Nothing Then
                        components.Dispose()
                    End If
                End If
                m_disposed = True
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Component Designer.
        Private components As System.ComponentModel.IContainer

        'NOTE: Required by the Component Designer
        'Can be modified using the Component Designer.
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
        End Sub

    End Class

End Namespace