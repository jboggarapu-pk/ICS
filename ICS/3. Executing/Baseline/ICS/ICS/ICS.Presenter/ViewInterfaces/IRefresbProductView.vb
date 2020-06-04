Public Interface IRefresbProductView
    Inherits IView

    Property SuccessText() As String
    Property ErrorText() As String

    Property MaterialNumber() As String
    Property RefreshProductTitle() As String
    
    Event Save As CustomEvents.PlainEventHandler
    Event ReloadViewData As CustomEvents.PlainEventHandler

    Sub SetupMaterialErrMsg(ByVal p_blnExistsFlag As Boolean)
    Sub SetupViewData(ByVal p_blnCopyCert As Boolean)
End Interface

