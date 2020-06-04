Public Interface IEditCertification
    Inherits IView

    Property SuccessText() As String
    Property ErrorText() As String

    Property MaterialName() As String
    Property CopyCertificationTitle() As String
    Property MaterialNumber() As String
    Property MateNumErrMsgFlag() As Boolean

    Event Save As CustomEvents.PlainEventHandler
    Event ReloadViewData As CustomEvents.PlainEventHandler

    Sub SetupMateNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)
End Interface
