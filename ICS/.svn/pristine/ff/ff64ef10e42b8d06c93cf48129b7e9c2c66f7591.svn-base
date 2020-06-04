Imports CooperTire.ICS.Presenter

''' <summary>
''' Base control for an ICS view
''' </summary>
''' <remarks></remarks>
Public Class BaseUserControl
    Inherits System.Web.UI.UserControl
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

    Public Sub ShowMainView() Implements IView.ShowMainView

        DirectCast(Page, BasePage).ShowMainView()

    End Sub

    Public Sub HideUserMenuItems(ByVal listMenuItemValues As System.Collections.Generic.List(Of String)) Implements Presenter.IView.HideUserMenuItems

        DirectCast(Page, BasePage).HideUserMenuItems(listMenuItemValues)

    End Sub

#End Region

End Class
