Imports System.Web.SessionState
Imports CooperTire.ICS.Common
Imports System.IO

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Public Shared IOStream As Stream

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    ' Fires when an error occurs
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)

        Dim excLast As Exception = Server.GetLastError() '' GetBaseException()
        EventLogger.Enter(excLast)
        If Not Debugger.IsAttached Then
            Response.Redirect("Error.aspx")
        End If

    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

    Public ReadOnly Property Version() As String
        Get
            Dim strVersion As String = String.Empty
            If System.Reflection.Assembly.GetExecutingAssembly() IsNot Nothing Then
                strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
            End If
            Return strVersion
        End Get
    End Property

End Class