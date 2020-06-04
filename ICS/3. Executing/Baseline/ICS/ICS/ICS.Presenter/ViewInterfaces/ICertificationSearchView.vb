Imports System.Xml

''' <summary>
''' To be implemented by Certification Search View
''' </summary>
''' <remarks></remarks>
Public Interface ICertificationSearchView
    Inherits IView

    ' Changed sku to material number, brand code to brand and brand line as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

    Event Search As CustomEvents.PlainEventHandler
    Event LeafNodeSelected As CustomEvents.PlainEventHandler
    Event AddCertificateSelected As CustomEvents.PlainEventHandler
    Event Execute As CustomEvents.PlainEventHandler

    Property AddCertInfoText() As String
    Property SearchCertInfoText() As String
    Property ErrorText() As String

    Property CertificationNames() As List(Of String)
    Property CerttificationSearchTypes() As List(Of String)
    Property SearchCriteria() As String
    Property ExtensionNo() As String
    Property ImarkFamily() As String

    Property CurrentAddCertificateName() As String
    Property CurrentCertificateNumber() As String
    Property CurrentCertificateName() As String
    Property CurrentCertificatonTypeID() As Integer
    Property CurrentCertificateMaterialNum() As String
    Property CurrentCertificateExtension() As String
    Property CurrentCertificateSKUID() As String
    Property CurrentCertificateCustomer() As String

    ReadOnly Property SearchType() As String
    ReadOnly Property SelectedSearchResult() As String
    ReadOnly Property SelectedSearchResultParentText() As String
    ReadOnly Property SelectedSearchResultParentValue() As String
    ReadOnly Property SelectedSearchResultParentOfParentText() As String
    ReadOnly Property SelectedSearchResultParentOfParentValue() As String
    ReadOnly Property SelectedSearchResultParentOfParentOfParentText() As String
    ReadOnly Property SelectedSearchResultParentOfParentOfParentValue() As String

    ' Added  properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Property Brands() As List(Of String)
    Property BrandLines() As List(Of String)

    ReadOnly Property Brand() As String
    ReadOnly Property BrandLine() As String

    Sub ShowCurrentCertificate()
    Sub ShowAddCertification(ByVal p_blnNewAddCertView As Boolean)

    ReadOnly Property AddCertificationName() As String

    Sub PopulateTreeView(ByVal doc As XmlDocument)
    Sub InitializeTreeView()

    Sub ExpandSelectedNode(ByVal p_strMatlNum As String)

    ' Added as per project 2706 technical specification
    ReadOnly Property ActionName() As String
    ReadOnly Property MaterialMaintName() As String
    Sub PopulateTreeForCertificateNumber(ByVal doc As XmlDocument, ByVal dtbCertificateNo As DataTable)

End Interface
