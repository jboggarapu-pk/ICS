Public Interface IAttachCertification
    Inherits IView

    Property SuccessText() As String
    Property ErrorText() As String

    Property SkuId() As String
    Property CertificateNumber() As String
    Property ExtensionEn() As String
    Property CertificationTypeId() As String
    Property AttachCertificationTitle() As String
    Property MateNumErrMsgFlag() As Boolean

    Event Save As CustomEvents.PlainEventHandler
    Event ReloadViewData As CustomEvents.PlainEventHandler
    Event CheckCertificateNumberExists As CustomEvents.PlainEventHandler

    Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    Sub SetupViewData(ByVal p_blnAttachCert As Boolean)
End Interface