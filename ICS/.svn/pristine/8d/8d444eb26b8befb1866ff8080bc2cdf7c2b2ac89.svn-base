''' <summary>
''' Interface to the Detach Or Move certification User control view
''' </summary>
Public Interface IDetachOrMoveCertificationView
    Inherits IView

    Property SuccessText() As String
    Property ErrorText() As String

    Property CertificationName() As String
    Property AddCertificationTitle() As String
    Property CertificateNumber() As String
    Property Extension() As String
    Property SkuId() As Integer
    Property CertificateId() As Integer
    Property NewCertificateNumber() As String
    Property NewExtension() As String
    Property CertNumErrMsgFlag() As Boolean
    Property ExtensionErrMsgFlag() As Boolean
    Property NewCertNumErrMsgFlag() As Boolean
    Property NewExtensionErrMsgFlag() As Boolean
    Property CertificateMaterials() As DataTable

    Event ShowCertificateMaterials As CustomEvents.PlainEventHandler
    Event Detach As CustomEvents.PlainEventHandler
    Event Move As CustomEvents.PlainEventHandler
    Event ReloadViewData As CustomEvents.PlainEventHandler

    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)
    Sub SetupDetachOrMoveCertificationView()
    Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    Sub SetupExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean)

End Interface
