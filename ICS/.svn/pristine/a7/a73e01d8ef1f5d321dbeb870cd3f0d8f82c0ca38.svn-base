Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports System.IO

' Report selector view
Partial Public Class ReportSelectorView
    Inherits BasePage
    Implements IReportSelectorView

    ' Added dropdown lists Brand and Brand line , changed material number instead of sku.
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Members"

    Private m_presenter As ReportSelectorPresenter
    Public Event Submit() Implements IReportSelectorView.Submit
    Public Event SelectReport(ByVal p_object As Object) Implements IReportSelectorView.SelectReport
    Public Event RefreshPage() Implements IReportSelectorView.RefreshPage

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New ReportSelectorPresenter(Me)

    End Sub

#End Region

#Region "Properties"

    Public Property ErrorText() As String Implements IReportSelectorView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property InfoText() As String Implements IReportSelectorView.InfoText
        Get
            Return lblInfoText.Text
        End Get
        Set(ByVal value As String)
            lblInfoText.Text = value
        End Set
    End Property

    Public Property AvailReports() As Dictionary(Of NameAid.Report, String) Implements IReportSelectorView.AvailReports
        Get
            Return Me.ddlReports.DataSource
        End Get
        Set(ByVal value As Dictionary(Of NameAid.Report, String))
            ddlReports.DataSource = value
            ddlReports.DataTextField = "Value"
            ddlReports.DataValueField = "Key"

            ddlReports.DataBind()
        End Set
    End Property

    'Public Property ReportVisibility() As Boolean Implements IReportSelectorView.ReportVisibility
    '    Get
    '        Return crViewer.Visible
    '    End Get
    '    Set(ByVal value As Boolean)
    '        crViewer.Visible = value
    '    End Set
    'End Property

    Public ReadOnly Property SelectedReportName() As String Implements IReportSelectorView.SelectedReportName
        Get
            Return ddlReports.SelectedItem.Value
        End Get

    End Property

    'Public Property RepSource() As ReportDocument Implements IReportSelectorView.RepSource
    '    Get
    '        Return crViewer.ReportSource
    '    End Get
    '    Set(ByVal value As ReportDocument)
    '        crViewer.ReportSource = value
    '    End Set
    'End Property

    Public Property RepDocument() As ReportDocument Implements IReportSelectorView.RepDocument
        Get
            Return CType(Session(Me.GetType().Name & "ReportDocument"), ReportDocument)
        End Get
        Set(ByVal value As ReportDocument)
            If RepDocument IsNot Nothing Then
                RepDocument.Close()
                RepDocument.Dispose()
            End If
            Session(Me.GetType().Name & "ReportDocument") = value
        End Set
    End Property

    Public ReadOnly Property RepPath() As String Implements IReportSelectorView.RepPath
        Get
            Return "C:\ICS"
        End Get
    End Property

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ReadOnly Property Brand() As String Implements IReportSelectorView.Brand
        Get
            If (ddlBrand.SelectedIndex > 0) Then
                Return ddlBrand.SelectedItem.Text
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ReadOnly Property BrandLine() As String Implements IReportSelectorView.BrandLine
        Get
            If (ddlBrandLine.Items.Count > 0) Then
                Return ddlBrandLine.SelectedItem.Text
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ReadOnly Property CertificationType() As String Implements IReportSelectorView.CertificationType
        Get
            If (ddlCertTypes.SelectedIndex > 0) Then
                Return ddlCertTypes.SelectedItem.Text
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public Property ExcelExportDataSet() As DataSet Implements IReportSelectorView.ExcelExportDataSet
        Get
            Return CType(Session(Me.GetType().Name & "DataSet"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            If ExcelExportDataSet IsNot Nothing Then
                ExcelExportDataSet.Dispose()
            End If
            Session(Me.GetType().Name & "DataSet") = value
        End Set
    End Property

    Public Property CertificationTypes() As List(Of String) Implements IReportSelectorView.CertificationTypes
        Get
            Return ddlCertTypes.DataSource
        End Get
        Set(ByVal value As List(Of String))
            ddlCertTypes.DataSource = value
        End Set
    End Property

    Public Property Param1() As String Implements IReportSelectorView.Param1
        Get
            Return txtParam.Text
        End Get
        Set(ByVal value As String)
            txtParam.Text = value
        End Set
    End Property

    Public Property Param2() As String Implements IReportSelectorView.Param2
        Get
            Return txtParam2.Text
        End Get
        Set(ByVal value As String)
            txtParam2.Text = value
        End Set
    End Property

    Public Property Param3() As String Implements IReportSelectorView.Param3
        Get
            Return txtParam3.Text
        End Get
        Set(ByVal value As String)
            txtParam3.Text = value
        End Set
    End Property

    Public Property Param4() As String Implements IReportSelectorView.Param4
        Get
            Return ddlCertTypes.SelectedItem.Value
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    Public Property Param5() As String Implements IReportSelectorView.Param5
        Get
            Return txtParam5.Text
        End Get
        Set(ByVal value As String)
            txtParam5.Text = value
        End Set
    End Property

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Property Brands() As List(Of String) Implements IReportSelectorView.Brands
        Get
            Return ddlBrand.DataSource
        End Get
        Set(ByVal value As List(Of String))
            ddlBrand.DataSource = value
        End Set
    End Property

    Property BrandLines() As List(Of String) Implements IReportSelectorView.BrandLines
        Get
            Return ddlBrandLine.DataSource
        End Get
        Set(ByVal value As List(Of String))
            ddlBrandLine.DataSource = value
        End Set
    End Property

    ' Added IncludeArchived parameter as per IDEA2706 International Certification System Modifications (TD#2)
    Public ReadOnly Property IncludeArchived() As String Implements IReportSelectorView.IncludeArchived
        Get
            If (cbIncludeArchivedCertificates.Checked) Then
                Return "Y"
            Else
                Return "N"
            End If
        End Get
    End Property

#End Region

#Region "Methods"

    Protected Sub Click_btnSubmit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim strDispType As String

        ' Create the report -- returns either an R for Crystal Report or
        '                   E for Excel Report
        strDispType = m_presenter.SubmitReport()
        If strDispType = "R" Then ' Report is a crystal report

            Dim req As New ExportRequestContext
            Dim Options As New ExportOptions

            Options.ExportFormatType = ExportFormatType.PortableDocFormat

            'set report options for report
            req.ExportInfo = Options

            'export the report to an IO Stream
            Global_asax.IOStream = RepDocument.FormatEngine.ExportToStream(req)

            Response.Redirect("Report.aspx?DispType=" & strDispType)
        ElseIf strDispType = "E" Then ' EXCEL FILE
            'TESTDS = ExcelExportDataSet

            'Response.Redirect("Report.aspx?DispType=" & strDispType)
            Response.Redirect("ExcelExport.aspx")
        End If

    End Sub

    Protected Sub Click_ddlReports(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlReports.SelectedIndexChanged

        RaiseEvent SelectReport(SelectedReportName)

    End Sub

    Public Sub ShowMaterialPrompt() Implements IReportSelectorView.ShowMaterialPrompt

        Dim line1 As HtmlTableRow = CType(Me.ParamLine1, HtmlTableRow)
        Dim line2 As HtmlTableRow = CType(Me.ParamLine2, HtmlTableRow)
        Dim line3 As HtmlTableRow = CType(Me.ParamLine3, HtmlTableRow)
        Dim line4 As HtmlTableRow = CType(Me.ParamLine4, HtmlTableRow)
        Dim line5 As HtmlTableRow = CType(Me.ParamLine5, HtmlTableRow)
        Dim line6 As HtmlTableRow = CType(Me.ParamLine6, HtmlTableRow)
        Dim line7 As HtmlTableRow = CType(Me.ParamLine7, HtmlTableRow)
        Dim line8 As HtmlTableRow = CType(Me.ParamLine8, HtmlTableRow)
        FlushParams()

        line1.Visible = False
        line2.Visible = True
        lblParam2.Text = "Material Number:"
        line3.Visible = False
        line4.Visible = True
        line5.Visible = False
        line6.Visible = True
        line7.Visible = False
        line8.Visible = False

    End Sub

    Public Sub ShowMaterialOnlyPrompt() Implements IReportSelectorView.ShowMaterialOnlyPrompt

        Dim line1 As HtmlTableRow = CType(Me.ParamLine1, HtmlTableRow)
        Dim line2 As HtmlTableRow = CType(Me.ParamLine2, HtmlTableRow)
        Dim line3 As HtmlTableRow = CType(Me.ParamLine3, HtmlTableRow)
        Dim line4 As HtmlTableRow = CType(Me.ParamLine4, HtmlTableRow)
        Dim line5 As HtmlTableRow = CType(Me.ParamLine5, HtmlTableRow)
        Dim line6 As HtmlTableRow = CType(Me.ParamLine6, HtmlTableRow)
        Dim line7 As HtmlTableRow = CType(Me.ParamLine7, HtmlTableRow)
        Dim line8 As HtmlTableRow = CType(Me.ParamLine8, HtmlTableRow)
        FlushParams()

        line1.Visible = False
        line2.Visible = False
        line3.Visible = False
        line4.Visible = False
        line5.Visible = True
        line6.Visible = False
        line7.Visible = False
        line8.Visible = False
        lblParam5.Text = "Material Number:"

    End Sub

    Public Sub ShowTraceabilityPrompt() Implements IReportSelectorView.ShowTraceabilityPrompt

        Dim line1 As HtmlTableRow = CType(Me.ParamLine1, HtmlTableRow)
        Dim line2 As HtmlTableRow = CType(Me.ParamLine2, HtmlTableRow)
        Dim line3 As HtmlTableRow = CType(Me.ParamLine3, HtmlTableRow)
        Dim line4 As HtmlTableRow = CType(Me.ParamLine4, HtmlTableRow)
        Dim line5 As HtmlTableRow = CType(Me.ParamLine5, HtmlTableRow)
        Dim line6 As HtmlTableRow = CType(Me.ParamLine6, HtmlTableRow)
        Dim line7 As HtmlTableRow = CType(Me.ParamLine7, HtmlTableRow)
        Dim line8 As HtmlTableRow = CType(Me.ParamLine8, HtmlTableRow)
        FlushParams()

        line1.Visible = True
        lblParam1.Text = "Certification Number:"
        line2.Visible = False
        line3.Visible = False
        line4.Visible = True
        line5.Visible = False
        line6.Visible = False
        line7.Visible = False
        line8.Visible = True

    End Sub

    Public Sub ShowCertificateExtPrompt() Implements IReportSelectorView.ShowCertificateExtPrompt

        Dim line1 As HtmlTableRow = CType(Me.ParamLine1, HtmlTableRow)
        Dim line2 As HtmlTableRow = CType(Me.ParamLine2, HtmlTableRow)
        Dim line3 As HtmlTableRow = CType(Me.ParamLine3, HtmlTableRow)
        Dim line4 As HtmlTableRow = CType(Me.ParamLine4, HtmlTableRow)
        Dim line5 As HtmlTableRow = CType(Me.ParamLine5, HtmlTableRow)
        Dim line6 As HtmlTableRow = CType(Me.ParamLine6, HtmlTableRow)
        Dim line7 As HtmlTableRow = CType(Me.ParamLine7, HtmlTableRow)
        Dim line8 As HtmlTableRow = CType(Me.ParamLine8, HtmlTableRow)
        FlushParams()

        line1.Visible = True
        lblParam1.Text = "Certification Number:"
        line2.Visible = True
        lblParam2.Text = "Extension Number:"
        line3.Visible = False
        line4.Visible = False
        line5.Visible = False
        line6.Visible = False
        line7.Visible = False
        line8.Visible = False

    End Sub

    Public Sub ShowCertificatePrompt() Implements IReportSelectorView.ShowCertificatePrompt

        Dim line1 As HtmlTableRow = CType(Me.ParamLine1, HtmlTableRow)
        Dim line2 As HtmlTableRow = CType(Me.ParamLine2, HtmlTableRow)
        Dim line3 As HtmlTableRow = CType(Me.ParamLine3, HtmlTableRow)
        Dim line4 As HtmlTableRow = CType(Me.ParamLine4, HtmlTableRow)
        Dim line5 As HtmlTableRow = CType(Me.ParamLine5, HtmlTableRow)
        Dim line6 As HtmlTableRow = CType(Me.ParamLine6, HtmlTableRow)
        Dim line7 As HtmlTableRow = CType(Me.ParamLine7, HtmlTableRow)
        Dim line8 As HtmlTableRow = CType(Me.ParamLine8, HtmlTableRow)
        FlushParams()

        line1.Visible = True
        lblParam1.Text = "Certification Number:"
        line2.Visible = False
        line3.Visible = False
        line4.Visible = False
        line5.Visible = False
        line6.Visible = False
        line7.Visible = False
        line8.Visible = False

    End Sub

    Public Sub ShowDatePrompt(ByVal strLabelText As String) Implements IReportSelectorView.ShowDatePrompt

        Dim line1 As HtmlTableRow = CType(Me.ParamLine1, HtmlTableRow)
        Dim line2 As HtmlTableRow = CType(Me.ParamLine2, HtmlTableRow)
        Dim line3 As HtmlTableRow = CType(Me.ParamLine3, HtmlTableRow)
        Dim line4 As HtmlTableRow = CType(Me.ParamLine4, HtmlTableRow)
        Dim line5 As HtmlTableRow = CType(Me.ParamLine5, HtmlTableRow)
        Dim line6 As HtmlTableRow = CType(Me.ParamLine6, HtmlTableRow)
        Dim line7 As HtmlTableRow = CType(Me.ParamLine7, HtmlTableRow)
        Dim line8 As HtmlTableRow = CType(Me.ParamLine8, HtmlTableRow)
        FlushParams()

        line1.Visible = False
        line2.Visible = False
        line3.Visible = True
        lblParam3.Text = strLabelText
        line4.Visible = False
        line5.Visible = False
        line6.Visible = False
        line7.Visible = False
        line8.Visible = False

    End Sub

    Public Sub ShowCertificateBrandPrompt() Implements IReportSelectorView.ShowCertificateBrandPrompt

        Dim line1 As HtmlTableRow = CType(Me.ParamLine1, HtmlTableRow)
        Dim line2 As HtmlTableRow = CType(Me.ParamLine2, HtmlTableRow)
        Dim line3 As HtmlTableRow = CType(Me.ParamLine3, HtmlTableRow)
        Dim line4 As HtmlTableRow = CType(Me.ParamLine4, HtmlTableRow)
        Dim line5 As HtmlTableRow = CType(Me.ParamLine5, HtmlTableRow)
        Dim line6 As HtmlTableRow = CType(Me.ParamLine6, HtmlTableRow)
        Dim line7 As HtmlTableRow = CType(Me.ParamLine7, HtmlTableRow)
        Dim line8 As HtmlTableRow = CType(Me.ParamLine8, HtmlTableRow)
        FlushParams()

        line1.Visible = True
        lblParam1.Text = "Certification Number:"
        line2.Visible = False
        line3.Visible = False
        line4.Visible = False
        line5.Visible = True
        line6.Visible = False
        line7.Visible = False
        line8.Visible = False
        lblParam5.Text = "Brand:"

    End Sub

    ' Added SelectedIndexChanged_ddlBrand event as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Raise appropriate event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectedIndexChanged_ddlBrand(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Load the Brand Line from Product data Web Service
        If (ddlBrand.SelectedItem.Text <> "Select ...") Then
            m_presenter.LoadBrandLines(Brand)
            ParamLine7.Visible = True
        Else
            ParamLine7.Visible = False
        End If
    End Sub

    Public Sub ShowBatchPrompt() Implements IReportSelectorView.ShowBatchPrompt

        Dim line1 As HtmlTableRow = CType(Me.ParamLine1, HtmlTableRow)
        Dim line2 As HtmlTableRow = CType(Me.ParamLine2, HtmlTableRow)
        Dim line3 As HtmlTableRow = CType(Me.ParamLine3, HtmlTableRow)
        Dim line4 As HtmlTableRow = CType(Me.ParamLine4, HtmlTableRow)
        Dim line5 As HtmlTableRow = CType(Me.ParamLine5, HtmlTableRow)
        Dim line6 As HtmlTableRow = CType(Me.ParamLine6, HtmlTableRow)
        Dim line7 As HtmlTableRow = CType(Me.ParamLine7, HtmlTableRow)
        Dim line8 As HtmlTableRow = CType(Me.ParamLine8, HtmlTableRow)
        FlushParams()

        line1.Visible = False
        line2.Visible = False
        line3.Visible = False
        line4.Visible = False
        line5.Visible = True
        line6.Visible = False
        line7.Visible = False
        line8.Visible = False
        lblParam5.Text = "Batch:"

    End Sub

    Public Sub ShowNoParamPrompt() Implements IReportSelectorView.ShowNoParamPrompt

        Dim line1 As HtmlTableRow = CType(Me.ParamLine1, HtmlTableRow)
        Dim line2 As HtmlTableRow = CType(Me.ParamLine2, HtmlTableRow)
        Dim line3 As HtmlTableRow = CType(Me.ParamLine3, HtmlTableRow)
        Dim line4 As HtmlTableRow = CType(Me.ParamLine4, HtmlTableRow)
        Dim line5 As HtmlTableRow = CType(Me.ParamLine5, HtmlTableRow)
        Dim line6 As HtmlTableRow = CType(Me.ParamLine6, HtmlTableRow)
        Dim line7 As HtmlTableRow = CType(Me.ParamLine7, HtmlTableRow)
        Dim line8 As HtmlTableRow = CType(Me.ParamLine8, HtmlTableRow)
        FlushParams()

        line1.Visible = False
        line2.Visible = False
        line3.Visible = False
        line4.Visible = False
        line5.Visible = False
        line6.Visible = False
        line7.Visible = False
        line8.Visible = False

    End Sub

    Private Sub FlushParams()

        txtParam.Text = String.Empty
        txtParam2.Text = String.Empty
        txtParam3.Text = String.Empty
        txtParam5.Text = String.Empty

    End Sub

#End Region

End Class