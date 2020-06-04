''' <summary>
''' Interface to the Add certification User control view
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
''' <para>11/11/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Interface IAddCertificationView
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
    '''  CertificationName
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificationName() As String
    ''' <summary>
    '''  AddCertificationTitle
    ''' </summary>
    ''' <remarks></remarks>
    Property AddCertificationTitle() As String
    ''' <summary>
    '''  MatlNumList
    ''' </summary>
    ''' <remarks></remarks>
    Property MatlNumList() As List(Of String)
    ''' <summary>
    '''  CertificateNumber
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateNumber() As String
    ''' <summary>
    '''  Extension
    ''' </summary>
    ''' <remarks></remarks>
    Property Extension() As String
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
    '''  Importers
    ''' </summary>
    ''' <remarks></remarks>
    Property Importers() As DataTable
    ''' <summary>
    '''  Customers
    ''' </summary>
    ''' <remarks></remarks>
    Property Customers() As DataTable
    ''' <summary>
    '''  Importer
    ''' </summary>
    ''' <remarks></remarks>
    Property Importer() As String
    ''' <summary>
    '''  Customer
    ''' </summary>
    ''' <remarks></remarks>
    Property Customer() As String
    ''' <summary>
    '''  ErrorDesc
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorDesc() As String
    ''' <summary>
    '''  InsertPC
    ''' </summary>
    ''' <remarks></remarks>
    Property InsertPC() As String
    ''' <summary>
    '''  ErrorNum 
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorNum() As Integer

    ''' <summary>
    '''  Save event
    ''' </summary>
    ''' <remarks></remarks>
    Event Save As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  ReloadViewData event
    ''' </summary>
    ''' <remarks></remarks>
    Event ReloadViewData As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  CheckSKUExist event
    ''' </summary>
    ''' <remarks></remarks>
    Event CheckSKUExist As CustomEvents.CheckSKUExistEventHandler
    ''' <summary>
    '''  CheckDuplicateCertificateNumber event
    ''' </summary>
    ''' <remarks></remarks>
    Event CheckDuplicateCertificateNumber As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  SetupViewData
    ''' </summary>
    ''' <param name="p_strCertificationName"></param>
    ''' <param name="p_blnAnew"></param>
    ''' <remarks></remarks>
    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)
    ''' <summary>
    '''  SetupAddCertificationView
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetupAddCertificationView()
    ''' <summary>
    '''  SetupSKUErrMsg
    ''' </summary>
    ''' <param name="p_intIndex"></param>
    ''' <param name="p_blnExistFlag"></param>
    ''' <remarks></remarks>
    Sub SetupSKUErrMsg(ByVal p_intIndex As Integer, ByVal p_blnExistFlag As Boolean)
    ''' <summary>
    '''  SetupCertNumErrMsg
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag"></param>
    ''' <remarks></remarks>
    Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    ''' <summary>
    '''  SetupExtensionErrMsg 
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag"></param>
    ''' <remarks></remarks>
    Sub SetupExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean)

End Interface
