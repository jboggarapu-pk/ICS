Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common 
Imports CrystalDecisions.Shared
Imports System.IO
Imports Microsoft.Office.Interop

''' <summary>
''' Report selector presenter
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
''' <para>10/23/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class ReportSelectorPresenter
    Inherits ViewPresenterBase

    ' Changed sku to material number , added methods for populating the data to view for brand , brand lines.
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members and Constants"
    ''' <summary>
    ''' Interface to Report selector view.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_view As IReportSelectorView
    ''' <summary>
    '''  Constant to hold ErrorLoadFormData text
    ''' </summary>
    Private Const ErrorLoadFormDataText As String = "Error loading form data."
    ''' <summary>
    '''  Constant to hold Select text
    ''' </summary>
    Private Const SelectText As String = "Select ..."
#End Region

#Region "Constructors / Destructors"
    ''' <summary>
    ''' Custom Constructor to initialize class members.
    ''' </summary>
    ''' <param name="p_view">View</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/23/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As IReportSelectorView)
        MyBase.New(p_view)
        Const ErrorCreatingText As String = "Error creating "
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw New Exception(ErrorCreatingText + Me.ToString())
        End Try
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Attach presenter to view�s events.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/23/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()
        Try
            AddHandler m_view.LoadView, AddressOf OnLoadView
            AddHandler m_view.SelectReport, AddressOf OnSelectReport
            AddHandler m_view.RefreshPage, AddressOf OnRefreshPage
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Load data for the view.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/23/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
            Else
                RefreshView()
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorLoadFormDataText
        End Try
    End Sub

    ''' <summary>
    ''' OnSelect report.
    ''' </summary>
    ''' <param name="p_reportName">Report name.</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/23/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnSelectReport(ByVal p_reportName As Object)
        Const ErrorSelectReportText As String = "Error selecting report."
        Const DateSubmittedText As String = "Date Submitted"
        Try
            Select Case p_reportName.ToString
                Case NameAid.Report.EmarkCertification.ToString
                    m_view.ShowCertificateBrandPrompt()
                Case NameAid.Report.ImarkSamplingAndTireTests.ToString, _
                    NameAid.Report.EmarkSimilarCertificateSearch.ToString()
                    m_view.ShowMaterialOnlyPrompt()
                Case NameAid.Report.SKUCertification.ToString
                    m_view.ShowMaterialPrompt()
                Case NameAid.Report.ImarkSamplingAndTireTests.ToString, _
                    NameAid.Report.ImarkConformityMarkReport.ToString, _
                    NameAid.Report.ExceptionReport.ToString, _
                    NameAid.Report.ImarkECEAuthenticityReport.ToString
                    m_view.ShowNoParamPrompt()
                Case NameAid.Report.ImarkCertification.ToString
                    m_view.ShowDatePrompt(DateSubmittedText)
                Case NameAid.Report.TraceabilityReport.ToString
                    m_view.ShowTraceabilityPrompt()
                Case NameAid.Report.EmarkMelkshamTestReport.ToString, _
                    NameAid.Report.EmarkMSRPassengerReport.ToString, _
                    NameAid.Report.EmarkMSRTruckReport.ToString, _
                    NameAid.Report.EmarkMETruckReport.ToString, _
                    NameAid.Report.GSOTruckCertification.ToString, _
                    NameAid.Report.GSOPassengerCertification.ToString, _
                    NameAid.Report.CCCProductDescriptionReport.ToString, _
                    NameAid.Report.CCCSequentialReport.ToString, _
                    NameAid.Report.EmarkPassengerApplication.ToString, _
                    NameAid.Report.EmarkLightTruckApplication.ToString, _
                    NameAid.Report.NOMPassengerCertification.ToString, _
                    NameAid.Report.NOMLightTruckCertification.ToString, _
                    NameAid.Report.CertificationRenewalReport.ToString
                    m_view.ShowCertificatePrompt()
                Case NameAid.Report.GSOConformityCertificateReport.ToString
                    m_view.ShowBatchPrompt()
                Case Else
                    ' default
                    m_view.ShowCertificateExtPrompt()
            End Select
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSelectReportText
        End Try
    End Sub

    ''' <summary>
    ''' Get previous Data Source and then re-apply it to the viewer.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/23/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnRefreshPage()
        Try
            LoadViewData()
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorLoadFormDataText
        End Try
    End Sub

    ''' <summary>
    ''' Get previous Data Source and then re-apply it to the viewer.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/23/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub LoadViewData()
        Try
            Dim strRptPath As String = m_view.RepPath
            Dim reportsModel As ReportSelectorModel = New ReportSelectorModel(strRptPath)
            m_view.AvailReports = reportsModel.AvailReports

            LoadCertificateTypes(String.Empty)
            LoadBrands(String.Empty)
            m_view.ShowCertificateExtPrompt()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Get previous Data Source and then re-apply it to the viewer.
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
    ''' <para>10/23/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub RefreshView()

        'm_view.RepSource = m_view.RepDocument

    End Sub

    ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Submit report.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/23/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SubmitReport() As String
        Const CertFieldEmptyText As String = "certificate field is empty please enter a value."
        Const NoCertFoundText As String = "No certification found."
        Const BatchNumberEmptyText As String = "Batch number is empty - please enter a value."
        Const BatchNoFoundText As String = "Batch not found."
        Const EmptyMaterialNumberText As String = "Please enter a Material Number."
        Const NoCertsFoundText As String = "No certifications found."
        Const CertFieldEmptyValueText As String = "Certificate field is empty.  Please enter a value."
        Const BrandEmptyText As String = "Material Number/Brand are empty. Please enter at least one of them."
        Const BrandLineNotSelectText As String = "Brand Line is not selected."
        Const MissingDateSubmitText As String = "Missing Date Submitted"
        Const ErrorInDataInputText As String = "Error in data input"
        Const MustEnterText As String = "Must enter Certificate number or Brand and Brand Line"
        Const NoDataFoundText As String = "No data found."
        Const MissingCertNumberText As String = "Missing Certificate number or Certificate Type"
        Const NoExcpDataFoundText As String = "No exception data found."
        Const ErrorLoadReport As String = "Error loading report."
        Const CCCText As String = "CCC"
        Const IndiaMarkText As String = "India_Mark"
        Const ECE3054Text As String = "ECE3054"
        Const ECE117Text As String = "ECE117"
        Const GSOText As String = "GSO"
        Const ImarkText As String = "Imark"
        Const NOMText As String = "NOM"
        Const NText As String = "N"
        Const EText As String = "E"
        Const RText As String = "R"

        Try
            Dim errorMsg As String = ""
            Dim strRptPath As String = m_view.RepPath
            Dim reportsModel As ReportSelectorModel = New ReportSelectorModel(strRptPath)
            Dim certModel As CertificateModel = New CertificateModel
            Dim strSelectedReportName As String = m_view.SelectedReportName
            Dim blnExcelExport As Boolean = False
            Dim ds As DataSet = Nothing
            Dim crExportOptions As New ExportOptions
            Dim blnReturn As Boolean = False
            Dim strReturn As String

            m_view.RepDocument = Nothing
            m_view.ExcelExportDataSet = Nothing

            Select Case strSelectedReportName
                Case NameAid.Report.EmarkLightTruckCertification.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetEmarkLightTruckCertification(m_view.Param1, m_view.Param2)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.EmarkPassengerCertification.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetEmarkPassengerCertification(m_view.Param1, m_view.Param2)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.EmarkE117Certification.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetEmarkE117Certification(m_view.Param1, m_view.Param2)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.EmarkMSRPassengerReport.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetEmarkMSRPassengerData(m_view.Param1)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.EmarkMSRTruckReport.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetEmarkMSRTruckData(m_view.Param1)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.EmarkMETruckReport.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetEmarkMETruckData(m_view.Param1)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.EmarkMelkshamTestReport.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetEmarkMelkshamReportData(m_view.Param1)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.GSOPassengerCertification.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetGSOPassengerCertification(m_view.Param1, m_view.Param2)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.GSOTruckCertification.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetGSOLightTruckSequential(m_view.Param1, m_view.Param2)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.GSOConformityCertificateReport.ToString
                    If m_view.Param5 = "" Then
                        errorMsg = BatchNumberEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetGSOConformityReportData(m_view.Param5)
                        errorMsg = BatchNoFoundText
                    End If
                Case NameAid.Report.ImarkConformityMarkReport.ToString
                    m_view.RepDocument = reportsModel.GetImarkConformityCertification()
                    errorMsg = NoCertFoundText
                Case NameAid.Report.ImarkSamplingAndTireTests.ToString
                    If m_view.Param5 = "" Then
                        errorMsg = EmptyMaterialNumberText
                    Else
                        m_view.RepDocument = reportsModel.GetImarkSamplingAndTireTests(m_view.Param5)
                        errorMsg = NoCertsFoundText
                    End If
                Case NameAid.Report.CCCSequentialReport.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyValueText
                    Else
                        m_view.RepDocument = reportsModel.GetCCCSequentialReport(m_view.Param1, m_view.Param2)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.CCCProductDescriptionReport.ToString
                    ' Mark this as an Excel export and not a Crystal Report
                    blnExcelExport = True
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyValueText
                    Else
                        m_view.ExcelExportDataSet = reportsModel.GetCCCProductDescriptionReport(m_view.Param1, m_view.Param2)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.SKUCertification.ToString
                    If (m_view.Param2 = "" And (m_view.Brand = SelectText Or m_view.Brand = String.Empty)) Then
                        errorMsg = BrandEmptyText
                    Else
                        If m_view.Param2 = "" And (m_view.Brand <> SelectText Or String.IsNullOrEmpty(m_view.Brand)) Then
                            If (m_view.BrandLine = SelectText Or m_view.BrandLine = String.Empty) Then
                                errorMsg = BrandLineNotSelectText
                            Else
                                m_view.RepDocument = reportsModel.GetSKUCertification(String.Empty, m_view.Brand, m_view.BrandLine, m_view.Param4)
                                errorMsg = NoCertFoundText
                            End If
                        Else
                            m_view.RepDocument = reportsModel.GetSKUCertification(m_view.Param2, m_view.Brand, m_view.BrandLine, m_view.Param4)
                            errorMsg = NoCertFoundText
                        End If
                    End If
                Case NameAid.Report.ImarkCertification.ToString
                    Dim workdate As DateTime
                    If m_view.Param3.Length = 0 Then
                        m_view.RepDocument = reportsModel.GetImarkCertification(Nothing)
                    Else
                        If (DateTime.TryParse(m_view.Param3, workdate)) Then
                            m_view.RepDocument = reportsModel.GetImarkCertification(workdate)
                            errorMsg = MissingDateSubmitText
                        Else
                            errorMsg = ErrorInDataInputText
                        End If
                    End If
                    errorMsg = NoCertFoundText
                Case NameAid.Report.EmarkCertification.ToString
                    If (CBool(m_view.Param1) And (m_view.Brand = SelectText Or m_view.Brand = String.Empty)) Then
                        errorMsg = MustEnterText
                    Else
                        If m_view.Param1 = "" And (m_view.Brand <> SelectText Or String.IsNullOrEmpty(m_view.Brand)) Then
                            If (m_view.BrandLine = SelectText Or m_view.BrandLine = String.Empty) Then
                                errorMsg = BrandLineNotSelectText
                            Else
                                m_view.RepDocument = reportsModel.GetEmarkCertification(m_view.Param1, m_view.Brand, m_view.BrandLine)
                                errorMsg = NoDataFoundText
                            End If
                        Else
                            m_view.RepDocument = reportsModel.GetEmarkCertification(m_view.Param1, m_view.Brand, m_view.BrandLine)
                            errorMsg = NoDataFoundText
                        End If
                    End If
                Case NameAid.Report.TraceabilityReport.ToString
                    Dim CertTypeId As Integer
                    Select Case m_view.Param4
                        Case CCCText ' NameAid.Certification.CCC.ToString
                            CertTypeId = certModel.GetCertificateTypeID(CCCText)
                        Case IndiaMarkText 'NameAid.Certification.India_Mark.ToString
                            CertTypeId = certModel.GetCertificateTypeID(IndiaMarkText)
                        Case ECE3054Text ' NameAid.Certification.ECE3054.ToString
                            CertTypeId = certModel.GetCertificateTypeID(ECE3054Text)
                        Case ECE117Text ' NameAid.Certification.ECE117.ToString
                            CertTypeId = certModel.GetCertificateTypeID(ECE117Text)
                        Case GSOText ' NameAid.Certification.GSO.ToString
                            CertTypeId = certModel.GetCertificateTypeID(GSOText)
                        Case ImarkText 'NameAid.Certification.Imark.ToString
                            CertTypeId = certModel.GetCertificateTypeID(ImarkText)
                        Case NOMText ' NameAid.Certification.NOM.ToString
                            CertTypeId = certModel.GetCertificateTypeID(NOMText)
                        Case Else
                            CertTypeId = 0
                    End Select
                    If m_view.Param1 = "" And CertTypeId = 0 Then
                        errorMsg = MissingCertNumberText
                    Else

                        Dim CertNum As String = Nothing
                        If Not String.IsNullOrEmpty(m_view.Param1.Trim) Then
                            CertNum = m_view.Param1.Trim
                        End If
                        m_view.RepDocument = reportsModel.GetTraceabilityData(CertNum, CertTypeId, m_view.IncludeArchived)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.ExceptionReport.ToString
                    m_view.RepDocument = reportsModel.GetExceptionReportData()
                    errorMsg = NoExcpDataFoundText
                Case NameAid.Report.EmarkPassengerApplication.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetEmarkPassengerApplication(m_view.Param1)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.EmarkLightTruckApplication.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetEmarkLightTruckApplication(m_view.Param1)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.NOMPassengerCertification.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetNomPassengerCertification(m_view.Param1)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.NOMLightTruckCertification.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetNomLightTruckCertification(m_view.Param1)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.ImarkECEAuthenticityReport.ToString
                    m_view.RepDocument = reportsModel.GetAuthenticity
                    errorMsg = NoDataFoundText

                Case NameAid.Report.CertificationRenewalReport.ToString
                    If m_view.Param1 = "" Then
                        errorMsg = CertFieldEmptyText
                    Else
                        m_view.RepDocument = reportsModel.GetCertificationRenewal(m_view.Param1, 1)
                        errorMsg = NoCertFoundText
                    End If
                Case NameAid.Report.EmarkSimilarCertificateSearch.ToString
                    If m_view.Param5 = "" Then
                        errorMsg = EmptyMaterialNumberText
                    Else
                        m_view.RepDocument = reportsModel.EmarkSimilarCertificateSearch(m_view.Param5)
                        errorMsg = NoCertsFoundText
                    End If
            End Select

            If blnExcelExport = True Then
                'Excel Export
                If m_view.ExcelExportDataSet Is Nothing Then
                    m_view.ErrorText = errorMsg
                    m_view.InfoText = String.Empty
                    strReturn = NText
                Else
                    strReturn = EText
                End If
            Else
                If m_view.RepDocument Is Nothing OrElse m_view.RepDocument.FileName = String.Empty Then
                    m_view.ErrorText = errorMsg
                    m_view.InfoText = String.Empty
                    strReturn = NText
                Else
                    'Return True
                    strReturn = RText
                End If
            End If
            Return strReturn
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorLoadReport
            Return NText
        End Try
    End Function

    ''' <summary>
    ''' Load Data for the Brands to View.
    ''' </summary>
    ''' <param name="p_strBrand">Brand</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/23/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub LoadBrands(ByVal p_strBrand As String)
        Const ErrorLoadBrandDataText As String = "Error loading Brand data."
        Try
            'Get the Brand names for Add Brand drop-down list
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim listBrandNames As List(Of String) = certSearch.GetBrands(p_strBrand)
            listBrandNames.Insert(0, SelectText)
            m_view.Brands = listBrandNames
            m_view.DataBindView()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadBrandDataText
        End Try
    End Sub

    ''' <summary>
    ''' Load Data for the BrandLines to View.
    ''' </summary>
    ''' <param name="p_strLine">Line</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/23/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub LoadBrandLines(ByVal p_strLine As String)
        Const ErrorLoadBrandLineText As String = "Error loading Brand Line data."
        Try
            'Get the Brand Line names for Add Brand Line drop-down list
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim listBrandLineNames As List(Of String) = certSearch.GetBrandLines(p_strLine)
            listBrandLineNames.Insert(0, SelectText)
            m_view.BrandLines = listBrandLineNames
            m_view.DataBindView()

        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadBrandLineText
        End Try
    End Sub

    ''' <summary>
    ''' Load Certificate types.
    ''' </summary>
    ''' <param name="p_strCertType">Certificate type</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/23/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub LoadCertificateTypes(ByVal p_strCertType As String)
        Const ErrorLoadCertTypeDataText As String = "Error loading Certification Type data."
        Try
            'Get the certification types for ddlCertTypes
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim listCertificationTypes As List(Of String) = certSearch.GetCertificationNames
            listCertificationTypes.Insert(0, SelectText)
            m_view.CertificationTypes = listCertificationTypes
            m_view.DataBindView()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadCertTypeDataText
        End Try
    End Sub
#End Region

End Class
