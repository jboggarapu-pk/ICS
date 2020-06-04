''' <summary>
''' To be implemented by Marketing view
''' </summary>
''' <remarks></remarks>
Public Interface IMarketingNewView
    Inherits IView

    ' Changed sku to material number, brand code to brand and brand line as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

    Event Search As EventHandler
    Event Save As EventHandler
    'Event RegionChanged As EventHandler
    'Event AddRegionChanged As EventHandler
    Event SelectChangedWithDataDirty As EventHandler
    Event ProductRequestStatusChanged As CustomEvents.StatusChangedEventHandler
    Event ChangeAllCertStatusByMaterial As CustomEvents.SelectAllBySKUEventHandler
    Event ChangeAllMaterialsStatusByCert As CustomEvents.SelectAllByCountryEventHandler


    Property InfoText() As String
    Property SuccessText() As String
    Property ErrorText() As String
    'Property CertifiedRegions() As List(Of String)
    'Property UncertifiedRegions() As List(Of String)
    'Property ProductCountryStatusData() As DataTable
    'Property CertificationGroupCountries() As DataTable
    Property ProductRequestStatusData() As DataTable
    Property CertificateTypes() As DataTable

    'Property SelectedRegionName() As String
    Property DataDirtyFlag() As Boolean
 

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    Property Brands() As List(Of String)
    Property BrandLines() As List(Of String)

    ReadOnly Property Brand() As String
    ReadOnly Property BrandLine() As String
    Sub SetupCertificateView(ByVal p_dtbProductRequestStatusData As DataTable)
    ' Added method as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    Sub SetupDefaultView()

End Interface
