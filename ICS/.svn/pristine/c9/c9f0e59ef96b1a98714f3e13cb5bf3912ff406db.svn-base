''' <summary>
''' Interface to the Add certification User control view
''' </summary>
Public Interface IAddCertificationView
    Inherits IView

    Property SuccessText() As String
    Property ErrorText() As String

    Property CertificationName() As String
    Property AddCertificationTitle() As String
    Property MatlNumList() As List(Of String)
    Property CertificateNumber() As String
    Property Extension() As String
    Property CertNumErrMsgFlag() As Boolean
    Property ExtensionErrMsgFlag() As Boolean
    Property Importers() As DataTable
    Property Customers() As DataTable
    Property Importer() As String
    Property Customer() As String
    Property ErrorDesc() As String
    Property InsertPC() As String
    Property ErrorNum() As Integer

    Event Save As CustomEvents.PlainEventHandler
    Event ReloadViewData As CustomEvents.PlainEventHandler
    'Event Finish As CustomEvents.PlainEventHandler
    Event CheckSKUExist As CustomEvents.CheckSKUExistEventHandler
    Event CheckDuplicateCertificateNumber As CustomEvents.PlainEventHandler

'    Sub ReloadViewData()
    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)
    Sub SetupAddCertificationView()
    Sub SetupSKUErrMsg(ByVal p_intIndex As Integer, ByVal p_blnExistFlag As Boolean)
    Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    Sub SetupExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean)

End Interface
