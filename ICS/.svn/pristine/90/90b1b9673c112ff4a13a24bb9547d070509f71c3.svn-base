Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

Partial Public Class SessionTimeOutMonitorUC
    Inherits BaseUserControl
    Implements ISessionTimeOutMonitorView

#Region "Members"

    ''' <summary>
    '''  Declaring SessionTimeOutMonitorPresenter object
    ''' </summary>
    Private m_presenter As SessionTimeOutMonitorPresenter

#End Region

#Region "Constructors"

    ''' <summary>   
    ''' Constructor to initialize class members.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>    
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New SessionTimeOutMonitorPresenter(Me)

    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets Timer Interval value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>TimerInterval.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TimerInterval() As Integer Implements Presenter.ISessionTimeOutMonitorView.TimerInterval
        Get
            Return TimerTimeOut.Interval
        End Get
        Set(ByVal value As Integer)
            TimerTimeOut.Interval = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Session Timeout value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>SessionTimeout.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property SessionTimeout() As Integer Implements Presenter.ISessionTimeOutMonitorView.SessionTimeout
        Get
            Return Session.Timeout
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Timer tick event.
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Protected Sub TimerTimeOut_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.ConfirmPopUp.Show()

    End Sub

    ''' <summary>
    ''' NaviHome button click event.
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
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