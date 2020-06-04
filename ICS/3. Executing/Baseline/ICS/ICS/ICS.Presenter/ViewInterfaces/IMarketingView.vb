''' <summary>
''' To be implemented by Marketing view
''' </summary>
''' <remarks></remarks>
Public Interface IMarketingView
    Inherits IView

    ' Changed sku to material number, brand code to brand and brand line as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

    Event Search As EventHandler
    Event Save As EventHandler
    Event RegionChanged As EventHandler
    Event AddRegionChanged As EventHandler
    Event SelectChangedWithDataDirty As EventHandler
    Event ProductCountryStatusChanged As CustomEvents.StatusChangedEventHandler
    Event ChangeAllCountriesStatusByMaterial As CustomEvents.SelectAllBySKUEventHandler
    Event ChangeAllMaterialsStatusByCountry As CustomEvents.SelectAllByCountryEventHandler

    Property InfoText() As String
    Property SuccessText() As String
    Property ErrorText() As String
    Property CertifiedRegions() As List(Of String)
    Property UncertifiedRegions() As List(Of String)
    Property ProductCountryStatusData() As DataTable
    Property CertificationGroupCountries() As DataTable

    Property SelectedRegionName() As String
    Property DataDirtyFlag() As Boolean
    Property OrignalCertRegionSelectValue() As String
    Property OrignalUnCertRegionSelectValue() As String

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    Property Brands() As List(Of String)
    Property BrandLines() As List(Of String)

    ReadOnly Property Brand() As String
    ReadOnly Property BrandLine() As String
    Sub SetupRegionView(ByVal p_dtbCertRegions As DataTable)
    ' Added method as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    Sub SetupDefaultView()

End Interface
