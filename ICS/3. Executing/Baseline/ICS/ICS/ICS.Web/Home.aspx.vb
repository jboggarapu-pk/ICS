Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' Implements IHomeView to present ICS starting point
Partial Public Class Home
    Inherits BasePage
    Implements IHomeView

#Region "Members"

    Public Event EnterMarketing As EventHandler Implements IHomeView.EnterMarketing
    Public Event EnterMarketingNew As EventHandler Implements IHomeView.EnterMarketingNew
    Public Event EnterQuality As EventHandler Implements IHomeView.EnterQuality

    Private m_presenter As HomePresenter

#End Region

#Region "Properties"

    Public Property InfoText() As String Implements IHomeView.InfoText
        Get
            Return lblInfo.Text
        End Get
        Set(ByVal value As String)
            lblInfo.Text = value
        End Set
    End Property

    Public Property PendingApprovalCount() As Integer Implements Presenter.IHomeView.PendingApprovalCount
        Get
            If Session(Me.GetType().Name & "PendingApprovalCount") Is Nothing Then
                Session(Me.GetType().Name & "PendingApprovalCount") = -1
            End If
            Return Session(Me.GetType().Name & "PendingApprovalCount")
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "PendingApprovalCount") = value
        End Set
    End Property

    Public Property IsUserNameShown() As Boolean Implements Presenter.IHomeView.IsUserNameShown
        Get
            If Session(Me.GetType().Name & "IsUserNameShown") Is Nothing Then
                Session(Me.GetType().Name & "IsUserNameShown") = True
            End If
            Return Session(Me.GetType().Name & "IsUserNameShown")
        End Get
        Set(ByVal value As Boolean)
            Session(Me.GetType().Name & "IsUserNameShown") = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New HomePresenter(Me)

    End Sub

#End Region

#Region "Methods"

    Protected Sub Click_btnMarketing(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarketing.Click

        RaiseEvent EnterMarketing(sender, e)

    End Sub

    Protected Sub Click_btnMarketingNew(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarketingNew.Click

        RaiseEvent EnterMarketingNew(sender, e)

    End Sub

    Protected Sub Click_btnQuality(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuality.Click

        RaiseEvent EnterQuality(sender, e)

    End Sub

    ''' <summary>
    ''' Setup start properties according to operationmode
    ''' </summary>
    ''' <param name="p_enuOperationMode"></param>
    ''' <remarks></remarks>
    Public Sub SetupStartProperties(ByVal p_enuOperationMode As IHomeView.OperationMode) Implements IHomeView.SetupStartProperties

        Dim masterICS As ICS = Master
        masterICS.EffectiveMenu = ICS.EffectiveMenuEnum.None

        btnMarketing.Enabled = (p_enuOperationMode = IHomeView.OperationMode.Marketing OrElse p_enuOperationMode = IHomeView.OperationMode.Both)
        btnMarketingNew.Enabled = (p_enuOperationMode = IHomeView.OperationMode.Marketing OrElse p_enuOperationMode = IHomeView.OperationMode.Both)
        btnQuality.Enabled = (p_enuOperationMode = IHomeView.OperationMode.Quality OrElse p_enuOperationMode = IHomeView.OperationMode.Both)

    End Sub

    ''' <summary>
    ''' Show default marketing form
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMarketingForm() Implements IHomeView.ShowMarketingForm

        Dim masterICS As ICS = Master
        masterICS.EffectiveMenu = ICS.EffectiveMenuEnum.Marketing
        Response.Redirect("Marketing.aspx")

    End Sub


    ''' <summary>
    ''' Show default marketingnew form
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMarketingNewForm() Implements IHomeView.ShowMarketingNewForm

        Dim masterICS As ICS = Master
        masterICS.EffectiveMenu = ICS.EffectiveMenuEnum.MarketingNew
        Response.Redirect("MarketingNew.aspx")

    End Sub

    ''' <summary>
    ''' Show default certification form
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowCertificationForm() Implements IHomeView.ShowCertificationForm

        Dim masterICS As ICS = Master
        masterICS.EffectiveMenu = ICS.EffectiveMenuEnum.Quality
        Response.Redirect("CertificationSearchEx.aspx")

    End Sub

#End Region

End Class