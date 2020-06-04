''' <summary>
''' CCC interface to the CCC Certification User control view
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
Public Interface ICCCCertificationView
    Inherits IView
    Inherits ICertificationView

    ''' <summary>
    '''  RenewalRequired
    ''' </summary>
    ''' <remarks></remarks>
    Property RenewalRequired() As Boolean
    ''' <summary>
    '''  ActiveStatus
    ''' </summary>
    ''' <remarks></remarks>
    Property ActiveStatus() As Boolean
    ''' <summary>
    '''  RemoveMatlNum
    ''' </summary>
    Property RemoveMatlNum() As Boolean
    ''' <summary>
    '''  CertDateSubmitted
    ''' </summary>
    ''' <remarks></remarks>
    Property CertDateSubmitted() As String
    ''' <summary>
    '''  CertDateApproved
    ''' </summary>
    ''' <remarks></remarks>
    Property CertDateApproved() As String
    ''' <summary>
    '''  DateSubmitted
    ''' </summary>
    ''' <remarks></remarks>
    Property DateSubmitted() As String
    ''' <summary>
    '''  DateApproved
    ''' </summary>
    ''' <remarks></remarks>
    Property DateApproved() As String
    ''' <summary>
    '''  CCC_JobReportNumber
    ''' </summary>
    ''' <remarks></remarks>
    Property CCC_JobReportNumber() As String
    ''' <summary>
    '''  CCC_ProductLocation
    ''' </summary>
    ''' <remarks></remarks>
    Property CCC_ProductLocation() As String
    ''' <summary>
    '''  ProductData
    ''' </summary>
    ''' <remarks></remarks>
    Property ProductData() As String
    ''' <summary>
    '''  DiscDate
    ''' </summary>
    ''' <remarks></remarks>
    Property DiscDate() As String
    ''' <summary>
    '''  DateOfExpiry
    ''' </summary>
    ''' <remarks></remarks>
    Property DateOfExpiry() As String
    ''' <summary>
    '''  MoldChgRequired
    ''' </summary>
    ''' <remarks></remarks>
    Property MoldChgRequired() As Boolean
    ''' <summary>
    '''  OperDateApproved
    ''' </summary>
    ''' <remarks></remarks>
    Property OperDateApproved() As String
    ''' <summary>
    '''  AddInfo
    ''' </summary>
    ''' <remarks></remarks>
    Property AddInfo() As String

End Interface
