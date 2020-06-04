''' <summary>
''' Interface to the Edit certification User control view
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
Public Interface IEditCertification
    Inherits IView

    ''' <summary>
    '''  SuccessText
    ''' </summary>
    ''' <remarks></remarks>
    Property SuccessText() As String
    ''' <summary>
    '''  ErrorText
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorText() As String
    ''' <summary>
    '''  MaterialName
    ''' </summary>
    ''' <remarks></remarks>
    Property MaterialName() As String
    ''' <summary>
    '''  CopyCertificationTitle
    ''' </summary>
    ''' <remarks></remarks>
    Property CopyCertificationTitle() As String
    ''' <summary>
    '''  Material Number
    ''' </summary>
    ''' <remarks></remarks>
    Property MaterialNumber() As String
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
    ''' SetupMateNumErrMsg
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate Flag</param>
    ''' <remarks></remarks>
    Sub SetupMateNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)
    ''' <summary>
    '''  SetupViewData
    ''' </summary>
    ''' <param name="p_strCertificationName"></param>
    ''' <param name="p_blnAnew"></param>
    ''' <remarks></remarks>
    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)
End Interface
