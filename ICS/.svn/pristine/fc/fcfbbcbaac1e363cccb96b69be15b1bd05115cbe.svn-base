Public Partial Class ICS
    Inherits System.Web.UI.MasterPage
    ''' <summary>
    '''  Effective Menu Enum
    ''' </summary>
    Public Enum EffectiveMenuEnum
        None
        Marketing
        MarketingNew
        Quality
    End Enum
    ''' <summary>
    '''  Effective Menu property
    ''' </summary>
    ''' <returns>
    ''' Effective Menu value
    ''' </returns>
    Public Property EffectiveMenu() As EffectiveMenuEnum
        Get
            Return CType(Session("EffectiveMenu"), EffectiveMenuEnum)
        End Get
        Set(ByVal value As EffectiveMenuEnum)
            Session("EffectiveMenu") = value
        End Set
    End Property
    ''' <summary>
    '''  SessionMonitor property
    ''' </summary>
    ''' <returns>
    ''' SessionTimeOutMonitor value
    ''' </returns>
    Public ReadOnly Property SessionMonitor() As SessionTimeOutMonitorUC
        Get
            Return Me.SessionTimeOutMonitorUC1
        End Get
    End Property
    ''' <summary>
    '''  Method for Page Init.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>s
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        menuQuality.Visible = (EffectiveMenu = EffectiveMenuEnum.Quality)
        menuMarketing.Visible = (EffectiveMenu = EffectiveMenuEnum.Marketing)
        menuMarketing.Visible = (EffectiveMenu = EffectiveMenuEnum.MarketingNew)


        If Not IsPostBack Then
            lblVersion.Text &= ", " & "Version " & CType(HttpContext.Current.ApplicationInstance, Global_asax).Version
        End If

    End Sub

End Class