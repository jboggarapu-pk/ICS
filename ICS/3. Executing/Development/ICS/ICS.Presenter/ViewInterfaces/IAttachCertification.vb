''' <summary>
''' Interface to the Attach certification User control view
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
Public Interface IAttachCertification
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
    '''  SkuId
    ''' </summary>
    ''' <remarks></remarks>
    Property SkuId() As String
    ''' <summary>
    '''  Certificate Number
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateNumber() As String
    ''' <summary>
    '''  ExtensionEn
    ''' </summary>
    ''' <remarks></remarks>
    Property ExtensionEn() As String
    ''' <summary>
    '''  CertificationTypeId
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificationTypeId() As String
    ''' <summary>
    '''  AttachCertificationTitle
    ''' </summary>
    ''' <remarks></remarks>
    Property AttachCertificationTitle() As String
    ''' <summary>
    '''  MateNumErrMsgFlag
    ''' </summary>
    ''' <remarks></remarks>
    Property MateNumErrMsgFlag() As Boolean
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
    '''  CheckCertificateNumberExists
    ''' </summary>
    ''' <remarks></remarks>
    Event CheckCertificateNumberExists As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  SetupCertNumErrMsg
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate Flag</param>
    ''' <remarks></remarks>
    Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    ''' <summary>
    '''  SetupViewData
    ''' </summary>
    ''' <param name="p_blnAttachCert">Attach Certificate</param>
    ''' <remarks></remarks>
    Sub SetupViewData(ByVal p_blnAttachCert As Boolean)
End Interface