''' <summary>
''' Interface to the Rename certification User control view
''' </summary>
Public Interface IDeleteCertificationView
    Inherits IView

    Property SuccessText() As String
    Property ErrorText() As String

    Property CertificationName() As String
    Property AddCertificationTitle() As String
    Property CertificateNumber() As String
    Property Extension() As String
    Property CertNumErrMsgFlag() As Boolean
    Property ExtensionErrMsgFlag() As Boolean
    Property WarningMessage() As String

    Event CheckForCertifiedMaterials As CustomEvents.PlainEventHandler
    Event Save As CustomEvents.PlainEventHandler
    Event ReloadViewData As CustomEvents.PlainEventHandler

    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)
    Sub SetupDeleteCertificationView()
    Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    Sub SetupExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean)

End Interface
