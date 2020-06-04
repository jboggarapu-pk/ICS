Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

Partial Public Class SessionTimeOutMonitorUC
    Inherits BaseUserControl
    Implements ISessionTimeOutMonitorView

#Region "Members"

    Private m_presenter As SessionTimeOutMonitorPresenter

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New SessionTimeOutMonitorPresenter(Me)

    End Sub

#End Region

#Region "Properties"

    Public Property TimerInterval() As Integer Implements Presenter.ISessionTimeOutMonitorView.TimerInterval
        Get
            Return TimerTimeOut.Interval
        End Get
        Set(ByVal value As Integer)
            TimerTimeOut.Interval = value
        End Set
    End Property

    Public ReadOnly Property SessionTimeout() As Integer Implements Presenter.ISessionTimeOutMonitorView.SessionTimeout
        Get
            Return Session.Timeout
        End Get
    End Property

#End Region

#Region "Methods"

    Protected Sub TimerTimeOut_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.ConfirmPopUp.Show()

    End Sub

    Protected Sub btnNaviHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strSessionExpiredRedirect As String = CooperTire.ICS.Common.AppSettingsAid.GetSessionExpiredRedirectUrl()

        If Not String.IsNullOrEmpty(strSessionExpiredRedirect) Then
            If strSessionExpiredRedirect.IndexOf("~") = 0 Then
                Response.Redirect(VirtualPathUtility.ToAppRelative(strSessionExpiredRedirect))
            Else
                Response.Redirect(strSessionExpiredRedirect)
            End If
        End If

    End Sub

#End Region

End Class