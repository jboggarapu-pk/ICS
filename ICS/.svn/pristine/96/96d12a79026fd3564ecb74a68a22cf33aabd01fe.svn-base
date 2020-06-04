Public Partial Class ICS
    Inherits System.Web.UI.MasterPage

    Public Enum EffectiveMenuEnum
        None
        Marketing
        MarketingNew
        Quality
    End Enum

    Public Property EffectiveMenu() As EffectiveMenuEnum
        Get
            Return Session("EffectiveMenu")
        End Get
        Set(ByVal value As EffectiveMenuEnum)
            Session("EffectiveMenu") = value
        End Set
    End Property

    Public ReadOnly Property SessionMonitor() As SessionTimeOutMonitorUC
        Get
            Return Me.SessionTimeOutMonitorUC1
        End Get
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        menuQuality.Visible = (EffectiveMenu = EffectiveMenuEnum.Quality)
        menuMarketing.Visible = (EffectiveMenu = EffectiveMenuEnum.Marketing)
        menuMarketing.Visible = (EffectiveMenu = EffectiveMenuEnum.MarketingNew)


        If Not IsPostBack Then
            lblVersion.Text &= ", " & "Version " & CType(HttpContext.Current.ApplicationInstance, Global_asax).Version
        End If

    End Sub

End Class