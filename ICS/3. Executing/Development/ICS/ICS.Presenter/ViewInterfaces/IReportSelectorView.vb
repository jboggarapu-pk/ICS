Imports CrystalDecisions.CrystalReports.Engine
Imports CooperTire.ICS.Common
Imports System.IO

Public Interface IReportSelectorView
    Inherits IView

    ' Changed sku to material number, brand code to brand and brand line as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' Variable to hold Info Text.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property SelectedReportName() As String

    ''' <summary>
    ''' Variable to hold Info Text.
    ''' </summary>
    ''' <remarks></remarks>
    Property InfoText() As String

    ''' <summary>
    ''' Variable to hold Error Text.
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorText() As String

    ''' <summary>
    ''' Variable to hold Param1.
    ''' </summary>
    ''' <remarks></remarks>
    Property Param1() As String

    ''' <summary>
    ''' Variable to hold Param2.
    ''' </summary>
    ''' <remarks></remarks>
    Property Param2() As String

    ''' <summary>
    ''' Variable to hold Param3.
    ''' </summary>
    ''' <remarks></remarks>
    Property Param3() As String

    ''' <summary>
    ''' Variable to hold Param4.
    ''' </summary>
    ''' <remarks></remarks>
    Property Param4() As String

    ''' <summary>
    ''' Variable to hold Param5.
    ''' </summary>
    ''' <remarks></remarks>
    Property Param5() As String

    ''' <summary>
    ''' Variable to hold Include Archived.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property IncludeArchived() As String

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
    ''' Variable to hold Avail Reports.
    ''' </summary>
    ''' <remarks></remarks>
    Property AvailReports() As Dictionary(Of NameAid.Report, String)

    ''' <summary>
    ''' Variable to hold Certification Types.
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificationTypes() As List(Of String)

    ''' <summary>
    ''' Variable to hold Certification Type.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property CertificationType() As String

    ''' <summary>
    ''' Variable to hold RepDocument.
    ''' </summary>
    ''' <remarks></remarks>
    Property RepDocument() As ReportDocument

    ''' <summary>
    ''' Variable to hold Excel Export DataSet.
    ''' </summary>
    ''' <remarks></remarks>
    Property ExcelExportDataSet() As DataSet

    ''' <summary>
    ''' Variable to hold RepPath.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property RepPath() As String

    ''' <summary>
    ''' Method to Show Material Prompt.
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
    Sub ShowMaterialPrompt()

    ''' <summary>
    ''' Method to Show Certificate ExtPrompt.
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
    Sub ShowCertificateExtPrompt()

    ''' <summary>
    ''' Method to Show Certificate Prompt.
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
    Sub ShowCertificatePrompt()

    ''' <summary>
    ''' Method to Show Date Prompt.
    ''' </summary>
    ''' <param name="strLabelText">Label text</param>
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
    Sub ShowDatePrompt(ByVal strLabelText As String)

    ''' <summary>
    ''' Method to Show No Param Prompt.
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
    Sub ShowNoParamPrompt()

    ''' <summary>
    ''' Method to Show Traceability Prompt.
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
    Sub ShowTraceabilityPrompt()

    ''' <summary>
    ''' Method to Show Certificate Brand Prompt.
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
    Sub ShowCertificateBrandPrompt()

    ''' <summary>
    ''' Method to Show Material Only Prompt.
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
    Sub ShowMaterialOnlyPrompt()

    ''' <summary>
    ''' Method to Show Batch Prompt.
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
    Sub ShowBatchPrompt()

    ''' <summary>
    ''' Event to Select Report.
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
    Event SelectReport As CustomEvents.PlainArgumentEventHandler

    ''' <summary>
    ''' Event to Submit.
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
    Event Submit As CustomEvents.PlainEventHandler

    ''' <summary>
    ''' Event to Refresh Page.
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
    Event RefreshPage As CustomEvents.PlainEventHandler

End Interface

