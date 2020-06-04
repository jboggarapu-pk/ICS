''' <summary>
''' Interface to the delete duplicate certificates user control view
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
Public Interface IDupCorrectCertificationView
    Inherits IView
    ''' <summary>
    '''  Material Number
    ''' </summary>
    ''' <remarks></remarks>
    Property MaterialNumber() As String
    ''' <summary>
    '''  Speed Rating
    ''' </summary>
    ''' <remarks></remarks>
    Property SpeedRating() As String
    ''' <summary>
    '''  DuplicateCertificates
    ''' </summary>
    ''' <remarks></remarks>
    Property DuplicateCertificates() As DataTable
    ''' <summary>
    '''  CertId
    ''' </summary>
    ''' <remarks></remarks>
    Property CertId() As Integer
    ''' <summary>
    '''  Success Text
    ''' </summary>
    ''' <remarks></remarks>
    Property SuccessText() As String
    ''' <summary>
    '''  ErrorText
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
    '''  View
    ''' </summary>
    ''' <remarks></remarks>
    Event View As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  Delete event
    ''' </summary>
    ''' <remarks></remarks>
    Event Delete As CustomEvents.PlainEventHandler
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

End Interface
