''' <summary>
''' Interface to the Add certification User control view
''' </summary>
Public Interface IArchiveCertificationView
    Inherits IView

    Property SuccessText() As String
    Property ErrorText() As String

    Property CertificationName() As String
    Property ArchiveCertificationTitle() As String
    Property CertificateNumber() As String
    Property CertNumErrMsgFlag() As Boolean

    Event Save As CustomEvents.PlainEventHandler
    Event ReloadViewData As CustomEvents.PlainEventHandler
    Event CheckCertificateNumberExists As CustomEvents.PlainEventHandler

    Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)

End Interface
