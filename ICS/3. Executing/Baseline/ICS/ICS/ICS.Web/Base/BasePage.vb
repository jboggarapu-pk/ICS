Imports CooperTire.ICS.Presenter

''' <summary>
''' Base page for an ICS views
''' </summary>
''' <remarks></remarks>
Public Class BasePage
    Inherits System.Web.UI.Page
    Implements IView

#Region "Members"

    Public Event LoadView As EventHandler Implements IView.LoadView
    Public Event InitView As CustomEvents.PlainEventHandler Implements IView.InitView
    Public Event UnloadView As EventHandler Implements IView.UnloadView

#End Region

#Region "Methods"

    ''' <summary>
    ''' Analog of respective Page property
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsPostBackView() As Boolean Implements IView.IsPostBackView
        Get
            Return Me.IsPostBack
        End Get
    End Property

    ''' <summary>
    ''' Raises Event to be processed by subscribed presenter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        RaiseEvent InitView()

    End Sub

    ''' <summary>
    ''' Raises Event to be processed by subscribed presenter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim masterICS As ICS = Master
        Dim milliseconds As Integer = 60000
        masterICS.SessionMonitor.TimerInterval = Session.Timeout * milliseconds
        'Response.Cache.SetExpires(DateTime.Now)
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        RaiseEvent LoadView(sender, e)

    End Sub

    ''' <summary>
    ''' Raises Event to be processed by subscribed presenter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

        RaiseEvent UnloadView(sender, e)

    End Sub

    ''' <summary>
    ''' Analog of respective Page method
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DataBindView() Implements IView.DataBindView

        Me.DataBind()

    End Sub

    ''' <summary>
    ''' Show ics main entry view
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMainView() Implements IView.ShowMainView

        Dim strMainViewPath = "~/Home.aspx"
        If AppRelativeVirtualPath <> strMainViewPath Then
            Response.Redirect(strMainViewPath)
        End If

    End Sub

    ''' <summary>
    ''' Hide user's no-go menu items 
    ''' </summary>
    ''' <param name="listNoGoMenuItemValues"></param>
    ''' <remarks></remarks>
    Public Sub HideUserMenuItems(ByVal listNoGoMenuItemValues As System.Collections.Generic.List(Of String)) Implements Presenter.IView.HideUserMenuItems

        Dim masterICS As ICS = Master
        Dim menu As Menu = Nothing

        Select Case masterICS.EffectiveMenu
            Case ICS.EffectiveMenuEnum.Marketing
                menu = masterICS.FindControl("menuMarketing")
            Case ICS.EffectiveMenuEnum.Quality
                menu = masterICS.FindControl("menuQuality")
        End Select

        If Not menu Is Nothing Then
            For Each mi As MenuItem In menu.Items
                If listNoGoMenuItemValues.Contains(mi.Value) Then
                    mi.Enabled = False
                End If
            Next
        End If

    End Sub

#End Region

End Class
