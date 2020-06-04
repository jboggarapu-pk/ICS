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
Public Interface IArchiveCertificationView
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
    '''  ArchiveCertificationTitle
    ''' </summary>
    ''' <remarks></remarks>
    Property ArchiveCertificationTitle() As String
    ''' <summary>
    '''  CertificateNumber
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateNumber() As String
    ''' <summary>
    '''  CertNumErrMsgFlag
    ''' </summary>
    ''' <remarks></remarks>
    Property CertNumErrMsgFlag() As Boolean
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
    '''  CheckCertificateNumberExists event
    ''' </summary>
    ''' <remarks></remarks>
    Event CheckCertificateNumberExists As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  SetupCertNumErrMsg
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate Flag</param>
    ''' <remarks></remarks>
    Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)

End Interface
