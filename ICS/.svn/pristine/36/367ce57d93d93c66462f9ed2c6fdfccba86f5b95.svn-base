Imports CrystalDecisions.CrystalReports.Engine
Imports CooperTire.ICS.Common
Imports System.IO

Public Interface IReportSelectorView
    Inherits IView

    ' Changed sku to material number, brand code to brand and brand line as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

    ReadOnly Property SelectedReportName() As String
    Property InfoText() As String
    Property ErrorText() As String
    Property Param1() As String
    Property Param2() As String
    Property Param3() As String
    Property Param4() As String
    Property Param5() As String

    ReadOnly Property IncludeArchived() As String

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Property Brands() As List(Of String)
    Property BrandLines() As List(Of String)


    ReadOnly Property Brand() As String
    ReadOnly Property BrandLine() As String
    Property AvailReports() As Dictionary(Of NameAid.Report, String)
    'Property CertificationTypes() As Dictionary(Of NameAid.Certification, String)
    Property CertificationTypes() As List(Of String)
    ReadOnly Property CertificationType() As String
    'Property RepSource() As ReportDocument
    Property RepDocument() As ReportDocument
    Property ExcelExportDataSet() As DataSet
    'Property ReportVisibility() As Boolean
    ReadOnly Property RepPath() As String

    Sub ShowMaterialPrompt()
    Sub ShowCertificateExtPrompt()
    Sub ShowCertificatePrompt()
    Sub ShowDatePrompt(ByVal strLabelText As String)
    Sub ShowNoParamPrompt()
    Sub ShowTraceabilityPrompt()
    Sub ShowCertificateBrandPrompt()
    Sub ShowMaterialOnlyPrompt()
    Sub ShowBatchPrompt()

    Event SelectReport As CustomEvents.PlainArgumentEventHandler
    Event Submit As CustomEvents.PlainEventHandler
    Event RefreshPage As CustomEvents.PlainEventHandler

End Interface

