''' <summary>
''' Interface to the Detach Or Move certification User control view
''' </summary>
''' <remarks>
''' <list type="table">
''' <listheader>
''' <term>Author</term>
''' <description>Description</description>
''' </listheader>
''' <item>
''' <term>N/A</term>
''' <description>
''' <para>N/A</para>
''' <para>Original Code.</para>
''' </description>
''' </item>
''' <item>
''' <term>Jhansi</term>
''' <description>
''' <para>11/13/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Interface IDetachOrMoveCertificationView
    Inherits IView

    ''' <summary>
    '''  Success Text
    ''' </summary>
    ''' <remarks></remarks>
    Property SuccessText() As String
    ''' <summary>
    '''  Error Text
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorText() As String
    ''' <summary>
    '''  Certification Name
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificationName() As String
    ''' <summary>
    '''  Add Certification Title
    ''' </summary>
    ''' <remarks></remarks>
    Property AddCertificationTitle() As String
    ''' <summary>
    '''  Certificate Number
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateNumber() As String
    ''' <summary>
    '''  Extension
    ''' </summary>
    ''' <remarks></remarks>
    Property Extension() As String
    ''' <summary>
    '''  SkuId
    ''' </summary>
    ''' <remarks></remarks>
    Property SkuId() As Integer
    ''' <summary>
    '''  CertificateId
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateId() As Integer
    ''' <summary>
    '''  NewCertificateNumber
    ''' </summary>
    ''' <remarks></remarks>
    Property NewCertificateNumber() As String
    ''' <summary>
    '''  NewExtension
    ''' </summary>
    ''' <remarks></remarks>
    Property NewExtension() As String
    ''' <summary>
    '''  CertNumErrMsgFlag
    ''' </summary>
    ''' <remarks></remarks>
    Property CertNumErrMsgFlag() As Boolean
    ''' <summary>
    '''  ExtensionErrMsgFlag
    ''' </summary>
    ''' <remarks></remarks>
    Property ExtensionErrMsgFlag() As Boolean
    ''' <summary>
    '''  NewCertNumErrMsgFlag
    ''' </summary>
    ''' <remarks></remarks>
    Property NewCertNumErrMsgFlag() As Boolean
    ''' <summary>
    '''  NewExtensionErrMsgFlag
    ''' </summary>
    ''' <remarks></remarks>
    Property NewExtensionErrMsgFlag() As Boolean
    ''' <summary>
    '''  CertificateMaterials
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateMaterials() As DataTable
    ''' <summary>
    '''  ShowCertificateMaterials event
    ''' </summary>
    ''' <remarks></remarks>
    Event ShowCertificateMaterials As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  Detach event
    ''' </summary>
    ''' <remarks></remarks>
    Event Detach As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  Move event
    ''' </summary>
    ''' <remarks></remarks>
    Event Move As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  ReloadViewData event
    ''' </summary>
    ''' <remarks></remarks>
    Event ReloadViewData As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  SetupViewData
    ''' </summary>
    ''' <param name="p_strCertificationName">Certification Name</param>
    ''' <param name="p_blnAnew">A New</param>
    ''' <remarks></remarks>
    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)
    ''' <summary>
    '''  SetupDetachOrMoveCertificationView
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetupDetachOrMoveCertificationView()
    ''' <summary>
    '''  SetupCertNumErrMsg
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate Flag</param>
    ''' <remarks></remarks>
    Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    ''' <summary>
    '''  SetupExtensionErrMsg
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate Flag</param>
    ''' <remarks></remarks>
    Sub SetupExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean)

End Interface
