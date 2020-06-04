''' <summary>
''' Interface to the Rename certification User control view
''' </summary>
Public Interface IRenameCertificationView
    Inherits IView

    Property SuccessText() As String
    Property ErrorText() As String

    Property CertificationName() As String
    Property AddCertificationTitle() As String
    Property OldCertificateNumber() As String
    Property NewCertificateNumber() As String
    Property OldExtension() As String
    Property NewExtension() As String
    Property OldCertNumErrMsgFlag() As Boolean
    Property NewCertNumErrMsgFlag() As Boolean
    Property OldExtensionErrMsgFlag() As Boolean
    Property NewExtensionErrMsgFlag() As Boolean
    Property WarningMessage() As String

    Event CheckForCertifiedMaterials As CustomEvents.PlainEventHandler
    Event Save As CustomEvents.PlainEventHandler
    Event ReloadViewData As CustomEvents.PlainEventHandler
   
    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)
    Sub SetupRenameCertificationView()
    Sub SetupOldCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    Sub SetupNewCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    Sub SetupOldExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    Sub SetupNewExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean)

End Interface
