' 02/14/2007

Imports System.Xml
Imports System.Data.SqlClient
Imports TVA.Security.Cryptography
Imports TVA.Security.Cryptography.Common
Imports TVA.Security.Application
Imports TVA.Identity.Common
Imports TVA.Web.Services.Common
Imports System.security.Principal

Namespace Services

    '<WebService(Namespace:="http://troweb/DataServices/")> _
    '<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    '<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Public MustInherit Class BaseWebService
        Inherits System.Web.Services.WebService
        Implements IBusinessObjectsAdapter

        Private m_businessObjectAdapter As IBusinessObjectsAdapter

        Protected DllName As String
        Protected FullyQualifiedClassName As String
        Protected TVAWebServiceCredentials As AuthenticationSoapHeader

        Public Sub New()

            MyBase.New()

        End Sub

        Public Function UserHasAccessToData(ByVal roleName As String) As Boolean

            With TVAWebServiceCredentials
                Return AuthenticateUser( _
                    Decrypt(.UserName, WebServiceSecurityKey, EncryptLevel.Level4), _
                    Decrypt(.Password, WebServiceSecurityKey, EncryptLevel.Level4), _
                    roleName, .Server, .PassThroughAuthentication)
            End With

        End Function

        Public Function BuildMessage() As String Implements IBusinessObjectsAdapter.BuildMessage

            Return m_businessObjectAdapter.BuildMessage()

        End Function

        Public Sub Initialize(ByVal ParamArray itemList() As Object) Implements IBusinessObjectsAdapter.Initialize

            Dim a As System.Reflection.Assembly = System.Reflection.Assembly.LoadFrom(Server.MapPath(DllName)) '"C:\Documents and Settings\sjohn\My Documents\Visual Studio 2005\Projects\abc\abc\bin\Release\abc.dll")
            Dim t As System.Type = a.GetType(FullyQualifiedClassName, True)
            m_businessObjectAdapter = Activator.CreateInstance(t)
            m_businessObjectAdapter.Initialize(itemList)

        End Sub

        Public Function ConvertToXMLDoc(ByVal xmlData As String) As XmlDocument

            Dim xmlDoc As New XmlDocument()
            Dim xmlReader As System.Xml.XmlTextReader
            xmlReader = New System.Xml.XmlTextReader(xmlData, System.Xml.XmlNodeType.Document, Nothing)
            xmlReader.ReadOuterXml()
            xmlDoc.Load(xmlReader)

            Return xmlDoc

        End Function

        Public Shared Function AuthenticateUser(ByVal userID As String, ByVal password As String, ByVal roleName As String, ByVal server As SecurityServer, ByVal passThroughAuthentication As Boolean) As Boolean

            '' Don't allow users to spoof authentication :)
            'TVA.Configuration.Common.CategorizedSettings("WebServicesDetails").Add("TestUser", My.User.CurrentPrincipal.Identity.Name, "test", False)
            'TVA.Configuration.Common.SaveSettings()
            'If passThroughAuthentication Then
            ' Dim userName As String = System.Threading.Thread.CurrentPrincipal.Identity.Name
            ' If userName.Contains("\") Then userName = userName.Split("\"c)(1).Trim()
            ' If String.Compare(userID, userName, True) <> 0 Then Return False
            'End If

            Dim primaryConnectionString As String
            Dim backupConnectString As String
            Dim connectionString As String
            With System.Configuration.ConfigurationManager.AppSettings
                Select Case server
                    Case SecurityServer.Development
                        primaryConnectionString = .Item("DevelopmentSecurityServer")
                    Case SecurityServer.Acceptance
                        primaryConnectionString = .Item("AcceptanceSecurityServer")
                    Case SecurityServer.Production
                        primaryConnectionString = .Item("ProductionSecurityServer")
                    Case Else
                        primaryConnectionString = .Item("DevelopmentSecurityServer")
                End Select

                backupConnectString = .Item("BackUpSecurityServer")
            End With

            'First try connecting to PrimaryDb
            Try
                Using checkConnection As New SqlConnection(primaryConnectionString)
                    checkConnection.Open()
                End Using

                connectionString = primaryConnectionString
            Catch ex As Exception
                connectionString = backupConnectString
            End Try

            Try
                With New User(userID, password, New SqlConnection(connectionString))
                    ' When not using pass through authentication, web service validates user name and password
                    ' otherwise only user name is used to verify user is in role and it becomes the responsibility
                    ' of the owning application to handle user authentication...
                    If Not passThroughAuthentication AndAlso Not .IsAuthenticated() Then Return False

                    If .FindRole(roleName) IsNot Nothing Then Return True
                End With
            Catch

            End Try

            Return False

        End Function

    End Class

End Namespace