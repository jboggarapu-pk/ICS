''' <summary>
''' Interface implemented by Marketing view.
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
''' <term>Sujitha</term>
''' <description>
''' <para>11/12/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Interface IMarketingView
    Inherits IView

    ' Changed sku to material number, brand code to brand and brand line as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' Event to Do Load View Data Event.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event Search As EventHandler

    ''' <summary>
    ''' Event to Do Load View Data Event.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event Save As EventHandler

    ''' <summary>
    ''' Event to Do Load View Data Event.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event RegionChanged As EventHandler

    ''' <summary>
    ''' Event to Do Load View Data Event.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event AddRegionChanged As EventHandler

    ''' <summary>
    ''' Event to Do Load View Data Event.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event SelectChangedWithDataDirty As EventHandler

    ''' <summary>
    ''' Event to Do Load View Data Event.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event ProductCountryStatusChanged As CustomEvents.StatusChangedEventHandler

    ''' <summary>
    ''' Event to Do Load View Data Event.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event ChangeAllCountriesStatusByMaterial As CustomEvents.SelectAllBySKUEventHandler

    ''' <summary>
    ''' Event to Do Load View Data Event.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event ChangeAllMaterialsStatusByCountry As CustomEvents.SelectAllByCountryEventHandler

    ''' <summary>
    ''' Variable to hold Info Text.
    ''' </summary>
    ''' <remarks></remarks>
    Property InfoText() As String

    ''' <summary>
    ''' Variable to hold Success Text.
    ''' </summary>
    ''' <remarks></remarks>
    Property SuccessText() As String

    ''' <summary>
    ''' Variable to hold Error Text.
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorText() As String

    ''' <summary>
    ''' Variable to hold Certified Regions.
    ''' </summary>
    ''' <remarks></remarks>
    Property CertifiedRegions() As List(Of String)

    ''' <summary>
    ''' Variable to hold Uncertified Regions.
    ''' </summary>
    ''' <remarks></remarks>
    Property UncertifiedRegions() As List(Of String)

    ''' <summary>
    ''' Variable to hold Product Country Status Data.
    ''' </summary>
    ''' <remarks></remarks>
    Property ProductCountryStatusData() As DataTable

    ''' <summary>
    ''' Variable to hold Certification Group Countries.
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificationGroupCountries() As DataTable

    ''' <summary>
    ''' Variable to hold Selected Region Name.
    ''' </summary>
    ''' <remarks></remarks>
    Property SelectedRegionName() As String

    ''' <summary>
    ''' Variable to hold Data Dirty Flag.
    ''' </summary>
    ''' <remarks></remarks>
    Property DataDirtyFlag() As Boolean

    ''' <summary>
    ''' Variable to hold Orignal CertRegion Select Value.
    ''' </summary>
    ''' <remarks></remarks>
    Property OrignalCertRegionSelectValue() As String

    ''' <summary>
    ''' Variable to hold Orignal UnCertRegion Select Value.
    ''' </summary>
    ''' <remarks></remarks>
    Property OrignalUnCertRegionSelectValue() As String

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Variable to hold Brands.
    ''' </summary>
    ''' <remarks></remarks>
    Property Brands() As List(Of String)

    ''' <summary>
    ''' Variable to hold Brand Lines.
    ''' </summary>
    ''' <remarks></remarks>
    Property BrandLines() As List(Of String)

    ''' <summary>
    ''' Variable to hold Brand.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property Brand() As String

    ''' <summary>
    ''' Variable to hold Brand Line.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property BrandLine() As String

    ''' <summary>
    ''' Function to Setup Region View.
    ''' </summary>
    ''' <param name="p_dtbCertRegions">Certificate regions</param>
    ''' <exception cref="Exception"> 
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub SetupRegionView(ByVal p_dtbCertRegions As DataTable)

    ' Added method as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Function to Setup Default View .
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub SetupDefaultView()

End Interface
