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

#Region "Members and Constants"
    ''' <summary>
    '''  Report selector presenter
    ''' </summary>
    Private m_presenter As ReportSelectorPresenter
    ''' <summary>
    '''  Submit event
    ''' </summary>
    Public Event Submit() Implements IReportSelectorView.Submit
    ''' <summary>
    '''  Select report event
    ''' </summary>
    Public Event SelectReport(ByVal p_object As Object) Implements IReportSelectorView.SelectReport
    ''' <summary>
    '''  Refresh page event
    ''' </summary>
    Public Event RefreshPage() Implements IReportSelectorView.RefreshPage
    ''' <summary>
    '''  Constant to hold Certification Number text
    ''' </summary>
    Private Const CertificationNumberText As String = "Certification Number:"
    ''' <summary>
    '''  Constant to hold Material Number text
    ''' </summary>
    Private Const MaterialNumberText As String = "Material Number:"
#End Region

#Region "Constructors"
    ''' <summary>
    '''  Default Constructor to initialize class members.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New ReportSelectorPresenter(Me)

    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    '''  Gets or sets Error text value.
    ''' </summary>
    ''' <returns>
    ''' Error text 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements IReportSelectorView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Info text value.
    ''' </summary>
    ''' <returns>
    ''' Info text 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property InfoText() As String Implements IReportSelectorView.InfoText
        Get
            Return lblInfoText.Text
        End Get
        Set(ByVal value As String)
            lblInfoText.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Avail reports value.
    ''' </summary>
    ''' <returns>
    ''' reports 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property AvailReports() As Dictionary(Of NameAid.Report, String) Implements IReportSelectorView.AvailReports
        Get
            Return CType(Me.ddlReports.DataSource, Global.System.Collections.Generic.Dictionary(Of Global.CooperTire.ICS.Common.NameAid.Report, String))
        End Get
        Set(ByVal value As Dictionary(Of NameAid.Report, String))
            ddlReports.DataSource = value
            ddlReports.DataTextField = "Value"
            ddlReports.DataValueField = "Key"

            ddlReports.DataBind()
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets selected report name value.
    ''' </summary>
    ''' <returns>
    ''' selected item 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property SelectedReportName() As String Implements IReportSelectorView.SelectedReportName
        Get
            Return ddlReports.SelectedItem.Value
        End Get

    End Property

    ''' <summary>
    '''  Gets or sets report document value.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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

    ''' <summary>
    '''  Gets report path value.
    ''' </summary>
    ''' <returns>
    ''' report path
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property RepPath() As String Implements IReportSelectorView.RepPath
        Get
            Return "C:\ICS"
        End Get
    End Property

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    '''  Gets or sets Brand value.
    ''' </summary>
    ''' <returns>
    ''' selected item text
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    ReadOnly Property Brand() As String Implements IReportSelectorView.Brand
        Get
            If (ddlBrand.SelectedIndex > 0) Then
                Return ddlBrand.SelectedItem.Text
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ''' <summary>
    '''  Gets or sets Brand line value.
    ''' </summary>
    ''' <returns>
    ''' selected item text
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    ReadOnly Property BrandLine() As String Implements IReportSelectorView.BrandLine
        Get
            If (ddlBrandLine.Items.Count > 0) Then
                Return ddlBrandLine.SelectedItem.Text
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ''' <summary>
    '''  Gets or sets certification type value.
    ''' </summary>
    ''' <returns>
    ''' selected item text 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    ReadOnly Property CertificationType() As String Implements IReportSelectorView.CertificationType
        Get
            If (ddlCertTypes.SelectedIndex > 0) Then
                Return ddlCertTypes.SelectedItem.Text
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ''' <summary>
    '''  Gets or sets Excel export dataset value.
    ''' </summary>
    ''' <returns>
    ''' dataset 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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

    ''' <summary>
    '''  Gets or sets certification types value.
    ''' </summary>
    ''' <returns>
    ''' certification types
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertificationTypes() As List(Of String) Implements IReportSelectorView.CertificationTypes
        Get
            Return CType(ddlCertTypes.DataSource, Global.System.Collections.Generic.List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            ddlCertTypes.DataSource = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets param1 value.
    ''' </summary>
    ''' <returns>
    ''' param text 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Param1() As String Implements IReportSelectorView.Param1
        Get
            Return txtParam.Text
        End Get
        Set(ByVal value As String)
            txtParam.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets param2 value.
    ''' </summary>
    ''' <returns>
    ''' param text 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Param2() As String Implements IReportSelectorView.Param2
        Get
            Return txtParam2.Text
        End Get
        Set(ByVal value As String)
            txtParam2.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets param3 value.
    ''' </summary>
    ''' <returns>
    ''' param text 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Param3() As String Implements IReportSelectorView.Param3
        Get
            Return txtParam3.Text
        End Get
        Set(ByVal value As String)
            txtParam3.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets param4 value.
    ''' </summary>
    ''' <returns>
    ''' param text 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Param4() As String Implements IReportSelectorView.Param4
        Get
            Return ddlCertTypes.SelectedItem.Value
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets param5 value.
    ''' </summary>
    ''' <returns>
    ''' param text 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Param5() As String Implements IReportSelectorView.Param5
        Get
            Return txtParam5.Text
        End Get
        Set(ByVal value As String)
            txtParam5.Text = value
        End Set
    End Property

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    '''  Gets or sets Brands value.
    ''' </summary>
    ''' <returns>
    ''' brand 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property Brands() As List(Of String) Implements IReportSelectorView.Brands
        Get
            Return CType(ddlBrand.DataSource, Global.System.Collections.Generic.List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            ddlBrand.DataSource = value
        End Set
    End Property

    ''' <summary>
    '''  Gets ors sets Brandlines value.
    ''' </summary>
    ''' <returns>
    ''' brandline 
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property BrandLines() As List(Of String) Implements IReportSelectorView.BrandLines
        Get
            Return CType(ddlBrandLine.DataSource, Global.System.Collections.Generic.List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            ddlBrandLine.DataSource = value
        End Set
    End Property

    ' Added IncludeArchived parameter as per IDEA2706 International Certification System Modifications (TD#2)
    ''' <summary>
    '''  Gets Include archived value.
    ''' </summary>
    ''' <returns>
    ''' Yes/No
    ''' </returns>
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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
    ''' <summary>
    '''  Method for submit button click.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnSubmit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Const ReportText As String = "Report.aspx?DispType="
        Const ExcelExportText As String = "ExcelExport.aspx"
        Const RText As String = "R"
        Const EText As String = "E"
        Try
            Dim strDispType As String

            ' Create the report -- returns either an R for Crystal Report or
            '                   E for Excel Report
            strDispType = m_presenter.SubmitReport()
            If strDispType = RText Then ' Report is a crystal report

                Dim req As New ExportRequestContext
                Dim Options As New ExportOptions

                Options.ExportFormatType = ExportFormatType.PortableDocFormat

                'set report options for report
                req.ExportInfo = Options

                'export the report to an IO Stream
                Global_asax.IOStream = RepDocument.FormatEngine.ExportToStream(req)

                Response.Redirect(ReportText & strDispType)
            ElseIf strDispType = EText Then ' EXCEL FILE
                'TESTDS = ExcelExportDataSet

                'Response.Redirect("Report.aspx?DispType=" & strDispType)
                Response.Redirect(ExcelExportText)
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for ddl reports click.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_ddlReports(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlReports.SelectedIndexChanged
        Try
            RaiseEvent SelectReport(SelectedReportName)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for show material prompt.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowMaterialPrompt() Implements IReportSelectorView.ShowMaterialPrompt
        Try
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
            lblParam2.Text = MaterialNumberText
            line3.Visible = False
            line4.Visible = True
            line5.Visible = False
            line6.Visible = True
            line7.Visible = False
            line8.Visible = False
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for show material only prompt.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowMaterialOnlyPrompt() Implements IReportSelectorView.ShowMaterialOnlyPrompt
        Try
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
            lblParam5.Text = MaterialNumberText
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for show traceability prompt.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowTraceabilityPrompt() Implements IReportSelectorView.ShowTraceabilityPrompt
        Try
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
            lblParam1.Text = CertificationNumberText
            line2.Visible = False
            line3.Visible = False
            line4.Visible = True
            line5.Visible = False
            line6.Visible = False
            line7.Visible = False
            line8.Visible = True
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for show certificate extension prompt.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowCertificateExtPrompt() Implements IReportSelectorView.ShowCertificateExtPrompt
        Const ExtensionNumberText As String = "Extension Number:"
        Try
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
            lblParam1.Text = CertificationNumberText
            line2.Visible = True
            lblParam2.Text = ExtensionNumberText
            line3.Visible = False
            line4.Visible = False
            line5.Visible = False
            line6.Visible = False
            line7.Visible = False
            line8.Visible = False
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for show certificate prompt.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowCertificatePrompt() Implements IReportSelectorView.ShowCertificatePrompt
        Try
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
            lblParam1.Text = CertificationNumberText
            line2.Visible = False
            line3.Visible = False
            line4.Visible = False
            line5.Visible = False
            line6.Visible = False
            line7.Visible = False
            line8.Visible = False
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for show date prompt.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowDatePrompt(ByVal strLabelText As String) Implements IReportSelectorView.ShowDatePrompt
        Try
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
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for show certificate brand prompt.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowCertificateBrandPrompt() Implements IReportSelectorView.ShowCertificateBrandPrompt
        Const BrandText As String = "Brand:"
        Try
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
            lblParam1.Text = CertificationNumberText
            line2.Visible = False
            line3.Visible = False
            line4.Visible = False
            line5.Visible = True
            line6.Visible = False
            line7.Visible = False
            line8.Visible = False
            lblParam5.Text = BrandText
        Catch
            Throw
        End Try
    End Sub

    ' Added SelectedIndexChanged_ddlBrand event as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Raise appropriate event
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub SelectedIndexChanged_ddlBrand(ByVal sender As Object, ByVal e As System.EventArgs)
        Const SelectText As String = "Select ..."
        Try
            ' Load the Brand Line from Product data Web Service
            If (ddlBrand.SelectedItem.Text <> SelectText) Then
                m_presenter.LoadBrandLines(Brand)
                ParamLine7.Visible = True
            Else
                ParamLine7.Visible = False
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for show batch prompt.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowBatchPrompt() Implements IReportSelectorView.ShowBatchPrompt
        Const BatchText As String = "Batch:"
        Try
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
            lblParam5.Text = BatchText
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for show no param prompt.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowNoParamPrompt() Implements IReportSelectorView.ShowNoParamPrompt
        Try
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
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for flush params.
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
    ''' <para>10/03/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub FlushParams()
        Try
            txtParam.Text = String.Empty
            txtParam2.Text = String.Empty
            txtParam3.Text = String.Empty
            txtParam5.Text = String.Empty
        Catch
            Throw
        End Try
    End Sub

#End Region

End Class