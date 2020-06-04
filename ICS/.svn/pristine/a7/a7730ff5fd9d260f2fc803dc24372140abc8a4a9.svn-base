Public Interface ICopyCertification
    Inherits IView

    Property SuccessText() As String
    Property ErrorText() As String

    Property MaterialNumber() As String
    Property CopyCertificationTitle() As String
    Property MateNumErrMsgFlag() As Boolean

    Event Save As CustomEvents.PlainEventHandler
    Event ReloadViewData As CustomEvents.PlainEventHandler

    Sub SetupMateNumErrMsg(ByVal p_blnExistsFlag As Boolean)
    Sub SetupViewData(ByVal p_blnCopyCert As Boolean)
End Interface
