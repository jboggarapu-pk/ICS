''' <summary>
''' Interface to the Rename certification User control view
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
Public Interface IDeleteCertificationView
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
    '''  WarningMessage
    ''' </summary>
    ''' <remarks></remarks>
    Property WarningMessage() As String
    ''' <summary>
    '''  CheckForCertifiedMaterials event
    ''' </summary>
    ''' <remarks></remarks>
    Event CheckForCertifiedMaterials As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  Save event
    ''' </summary>
    ''' <remarks></remarks>
    Event Save As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  ReloadViewData
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
    '''  SetupDeleteCertificationView
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetupDeleteCertificationView()
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
