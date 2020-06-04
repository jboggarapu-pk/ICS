''' <summary>
''' Interface  implemented by Marketing view.
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
Public Interface IMarketingNewView
    Inherits IView

    ' Changed sku to material number, brand code to brand and brand line as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' Event to  Search.
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
    ''' Event to  Save.
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
    ''' Event to Select Changed With Data Dirty.
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
    ''' Event to Product Request Status Changed.
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
    Event ProductRequestStatusChanged As CustomEvents.StatusChangedEventHandler

    ''' <summary>
    ''' Event to Change All Certificate Status By Material.
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
    Event ChangeAllCertStatusByMaterial As CustomEvents.SelectAllBySKUEventHandler

    ''' <summary>
    ''' Event to  Change All Materials Status By Certificate.
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
    Event ChangeAllMaterialsStatusByCert As CustomEvents.SelectAllByCountryEventHandler

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
    ''' Variable to hold Product Request Status Data.
    ''' </summary>
    ''' <remarks></remarks>
    Property ProductRequestStatusData() As DataTable

    ''' <summary>
    ''' Variable to hold Certificate Types.
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateTypes() As DataTable

    ''' <summary>
    ''' Variable to hold Data Dirty Flag.
    ''' </summary>
    ''' <remarks></remarks>
    Property DataDirtyFlag() As Boolean

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
    ''' Event to  .
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
    Sub SetupCertificateView(ByVal p_dtbProductRequestStatusData As DataTable)

    ' Added method as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Event to  .
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
