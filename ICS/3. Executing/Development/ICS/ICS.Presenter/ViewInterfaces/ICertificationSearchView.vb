Imports System.Xml

''' <summary>
''' Interface to the Certification Search View
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
Public Interface ICertificationSearchView
    Inherits IView

    ' Changed sku to material number, brand code to brand and brand line as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    '''  Search event
    ''' </summary>
    ''' <remarks></remarks>
    Event Search As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  LeafNodeSelected event
    ''' </summary>
    ''' <remarks></remarks>
    Event LeafNodeSelected As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  AddCertificateSelected event
    ''' </summary>
    ''' <remarks></remarks>
    Event AddCertificateSelected As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  Execute event
    ''' </summary>
    ''' <remarks></remarks>
    Event Execute As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  AddCertInfoText
    ''' </summary>
    ''' <remarks></remarks>
    Property AddCertInfoText() As String
    ''' <summary>
    '''  SearchCertInfoText
    ''' </summary>
    ''' <remarks></remarks>
    Property SearchCertInfoText() As String
    ''' <summary>
    '''  ErrorText
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorText() As String
    ''' <summary>
    '''  CertificationNames
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificationNames() As List(Of String)
    ''' <summary>
    '''  CerttificationSearchTypes
    ''' </summary>
    ''' <remarks></remarks>
    Property CerttificationSearchTypes() As List(Of String)
    ''' <summary>
    '''  SearchCriteria
    ''' </summary>
    ''' <remarks></remarks>
    Property SearchCriteria() As String
    ''' <summary>
    '''  ExtensionNo
    ''' </summary>
    ''' <remarks></remarks>
    Property ExtensionNo() As String
    ''' <summary>
    '''  ImarkFamily
    ''' </summary>
    ''' <remarks></remarks>
    Property ImarkFamily() As String
    ''' <summary>
    '''  CurrentAddCertificateName
    ''' </summary>
    ''' <remarks></remarks>
    Property CurrentAddCertificateName() As String
    ''' <summary>
    '''  CurrentCertificateNumber
    ''' </summary>
    ''' <remarks></remarks>
    Property CurrentCertificateNumber() As String
    ''' <summary>
    '''  CurrentCertificateName
    ''' </summary>
    ''' <remarks></remarks>
    Property CurrentCertificateName() As String
    ''' <summary>
    '''  CurrentCertificatonTypeID
    ''' </summary>
    ''' <remarks></remarks>
    Property CurrentCertificatonTypeID() As Integer
    ''' <summary>
    '''  CurrentCertificateMaterialNum
    ''' </summary>
    ''' <remarks></remarks>
    Property CurrentCertificateMaterialNum() As String
    ''' <summary>
    '''  CurrentCertificateExtension
    ''' </summary>
    ''' <remarks></remarks>
    Property CurrentCertificateExtension() As String
    ''' <summary>
    '''  CurrentCertificateSKUID
    ''' </summary>
    ''' <remarks></remarks>
    Property CurrentCertificateSKUID() As String
    ''' <summary>
    '''  CurrentCertificateCustomer
    ''' </summary>
    ''' <remarks></remarks>
    Property CurrentCertificateCustomer() As String
    ''' <summary>
    '''  SearchType
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property SearchType() As String
    ''' <summary>
    '''  SelectedSearchResult
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property SelectedSearchResult() As String
    ''' <summary>
    '''  SelectedSearchResultParentText
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property SelectedSearchResultParentText() As String
    ''' <summary>
    '''  SelectedSearchResultParentValue
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property SelectedSearchResultParentValue() As String
    ''' <summary>
    '''  SelectedSearchResultParentOfParentText
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property SelectedSearchResultParentOfParentText() As String
    ''' <summary>
    '''  SelectedSearchResultParentOfParentValue
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property SelectedSearchResultParentOfParentValue() As String
    ''' <summary>
    '''  SelectedSearchResultParentOfParentOfParentText
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property SelectedSearchResultParentOfParentOfParentText() As String
    ''' <summary>
    '''  SelectedSearchResultParentOfParentOfParentValue
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property SelectedSearchResultParentOfParentOfParentValue() As String

    ' Added  properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    '''  Brands
    ''' </summary>
    ''' <remarks></remarks>
    Property Brands() As List(Of String)
    ''' <summary>
    '''  BrandLines
    ''' </summary>
    ''' <remarks></remarks>
    Property BrandLines() As List(Of String)
    ''' <summary>
    '''  Brand
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property Brand() As String
    ''' <summary>
    '''  BrandLine
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property BrandLine() As String
    ''' <summary>
    '''  ShowCurrentCertificate
    ''' </summary>
    ''' <remarks></remarks>
    Sub ShowCurrentCertificate()
    ''' <summary>
    '''  ShowAddCertification
    ''' </summary>
    ''' <remarks></remarks>
    Sub ShowAddCertification(ByVal p_blnNewAddCertView As Boolean)
    ''' <summary>
    '''  AddCertificationName
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property AddCertificationName() As String
    ''' <summary>
    '''  PopulateTreeView
    ''' </summary>
    ''' <remarks></remarks>
    Sub PopulateTreeView(ByVal doc As XmlDocument)
    ''' <summary>
    '''  InitializeTreeView
    ''' </summary>
    ''' <remarks></remarks>
    Sub InitializeTreeView()
    ''' <summary>
    '''  ExpandSelectedNode
    ''' </summary>
    ''' <remarks></remarks>
    Sub ExpandSelectedNode(ByVal p_strMatlNum As String)

    ' Added as per project 2706 technical specification
    ''' <summary>
    '''  ActionName
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property ActionName() As String
    ''' <summary>
    '''  MaterialMaintName
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property MaterialMaintName() As String
    ''' <summary>
    '''  PopulateTreeForCertificateNumber
    ''' </summary>
    ''' <param name="doc">Doc</param>
    ''' <param name="dtbCertificateNo">Certificate No</param>
    ''' <remarks></remarks>
    Sub PopulateTreeForCertificateNumber(ByVal doc As XmlDocument, ByVal dtbCertificateNo As DataTable)

End Interface
