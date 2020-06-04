''' <summary>
''' This interface common to all views.
''' </summary>
''' <remarks></remarks>
Public Interface IView

    Event InitView As CustomEvents.PlainEventHandler
    Event LoadView As EventHandler
    Event UnloadView As EventHandler

    ReadOnly Property IsPostBackView() As Boolean

    Sub DataBindView()

    Sub ShowMainView()
    Sub HideUserMenuItems(ByVal listMenuItemValues As List(Of String))

End Interface
