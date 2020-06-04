''' <summary>
''' Interface to the Copy certification User control view
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
''' <para>11/12/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Interface ICopyCertification
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
    '''  Material Number
    ''' </summary>
    ''' <remarks></remarks>
    Property MaterialNumber() As String
    ''' <summary>
    '''  Copy Certification Title
    ''' </summary>
    ''' <remarks></remarks>
    Property CopyCertificationTitle() As String
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
    '''  ReloadViewData event
    ''' </summary>
    ''' <remarks></remarks>
    Event ReloadViewData As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  SetupMateNumErrMsg
    ''' </summary>
    ''' <param name="p_blnExistsFlag"></param>
    ''' <remarks></remarks>Exists Flag
    Sub SetupMateNumErrMsg(ByVal p_blnExistsFlag As Boolean)
    ''' <summary>
    '''  SetupViewData
    ''' </summary>
    ''' <param name="p_blnCopyCert">Copy Certificate</param>
    ''' <remarks></remarks>
    Sub SetupViewData(ByVal p_blnCopyCert As Boolean)
End Interface
