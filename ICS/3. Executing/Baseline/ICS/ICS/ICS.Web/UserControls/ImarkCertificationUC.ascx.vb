Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

'Imark certificate Form
Partial Public Class ImarkCertificationUC
    Inherits BaseCertificationControl
    Implements IImarkCertificationView

    ' Changed the sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Members"

    Public Event Save As CustomEvents.PlainEventHandler Implements ICertificationView.Save
    Public Event SaveReasons As CustomEvents.PlainEventHandler Implements ICertificationView.SaveReasons
    Public Event ShowTestResults As CustomEvents.PlainEventHandler Implements ICertificationView.ShowTestResults
    Public Event GetTestResults As CustomEvents.PlainEventHandler Implements ICertificationView.GetTestResults
    Public Event GetBlankResults As CustomEvents.PlainEventHandler Implements ICertificationView.GetBlankResults
    Public Event CopySimilarTireSKUCertificate As CustomEvents.PlainEventHandler Implements ICertificationView.CopySimilarTireSKUCertificate
    Public Event ShowDefaultValues() Implements Presenter.ICertificationView.ShowDefaultValues
    Public Event Renew() Implements Presenter.IImarkCertificationView.Renew
    Public Event MoldStampingRefresh() Implements Presenter.IImarkCertificationView.MoldStampingRefresh

    Private m_presenter As ImarkCertificationPresenter
    Private m_skuid As Integer
    Private m_enuCertType As Integer = 4 'NameAid.Certification = NameAid.Certification.Imark
    Private m_manufacturingLocs As DataSet
    Private m_lstAuditLog As List(Of AuditLogEntry)
    Private m_dstReasons As DataSet
    Private m_dtbSimilarCertificate As DataTable
    'Private m_strExtensionNo As String

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New ImarkCertificationPresenter(Me)

    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property CertificationType() As Integer Implements Presenter.ICertificationView.CertificationType
        Get
            Return m_enuCertType
        End Get
    End Property

    Public Property OriginalCertificate() As DomainEntities.Certificate Implements Presenter.ICertificationView.OriginalCertificate
        Get
            Return Session("OriginalCertificate")
        End Get
        Set(ByVal value As DomainEntities.Certificate)
            Session("OriginalCertificate") = value
        End Set
    End Property

    Public Property SimilarTireCertificate() As DomainEntities.Certificate Implements Presenter.ICertificationView.SimilarTireCertificate
        Get
            Return Session("SimilarTireCertificate")
        End Get
        Set(ByVal value As DomainEntities.Certificate)
            Session("SimilarTireCertificate") = value
        End Set
    End Property

    Public Property SimilarTireMessage() As String Implements Presenter.ICertificationView.SimilarTireMessage
        Get
            Return lblSimilarTireMessage.Text
        End Get
        Set(ByVal value As String)
            lblSimilarTireMessage.Text = value
        End Set
    End Property

    Public ReadOnly Property TRView() As Presenter.ITestResultsView Implements Presenter.IImarkCertificationView.TRView
        Get
            Return ucTestResults
        End Get
    End Property

    Public Property InfoText() As String Implements ICertificationView.InfoText
        Get
            Return lblInfoText.Text
        End Get
        Set(ByVal value As String)
            lblInfoText.Text = value
        End Set
    End Property

    Public Property ErrorText() As String Implements ICertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property MaterialNumber() As String Implements ICertificationView.MaterialNumber
        Get
            Return txtMaterialNo.Text
        End Get
        Set(ByVal value As String)
            txtMaterialNo.Text = value
        End Set
    End Property
    Public Property ProductData() As String Implements IImarkCertificationView.ProductData
        Get
            Return lblProductData.Text
        End Get
        Set(ByVal value As String)
            lblProductData.Text = value
        End Set
    End Property

    Public Property SKUID() As Integer Implements ICertificationView.SKUID
        Get
            Return m_skuid
        End Get
        Set(ByVal value As Integer)
            m_skuid = value
        End Set
    End Property

    Public Property RemoveMatlNum() As Boolean Implements IImarkCertificationView.RemoveMatlNum
        Get
            Return cbxRemoveMatlNum.Checked
        End Get
        Set(ByVal value As Boolean)
            cbxRemoveMatlNum.Checked = value
        End Set
    End Property

    Public Property ReasonDS() As DataSet Implements ICertificationView.ReasonDS
        Get
            Return m_dstReasons
        End Get
        Set(ByVal value As DataSet)
            m_dstReasons = value
        End Set
    End Property

    Public Property CertificationNumber() As String Implements ICertificationView.CertificationNumber
        Get
            Return txtCertificationNo.Text
        End Get
        Set(ByVal value As String)
            txtCertificationNo.Text = value
        End Set
    End Property

    Public Property ExtensionNo() As String Implements ICertificationView.ExtensionNo
        Get
            Return txtExtension.Text
        End Get
        Set(ByVal value As String)
            txtExtension.Text = value
        End Set
    End Property

    Public Property CertificateNumberID() As Integer Implements Presenter.ICertificationView.CertificateNumberID
        Get
            Return Session(Me.GetType().Name & "CertificateNumberID")
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "CertificateNumberID") = value
        End Set
    End Property

    Public Property DateAssigned() As String Implements IImarkCertificationView.DateAssigned
        Get
            Return txtDateAssigned.Text
        End Get
        Set(ByVal value As String)
            txtDateAssigned.Text = value
        End Set
    End Property

    Public Property CertDateSubmitted() As String Implements IImarkCertificationView.CertDateSubmitted
        Get
            Return txtCertDateSubmitted.Text
        End Get
        Set(ByVal value As String)
            txtCertDateSubmitted.Text = value
        End Set
    End Property

    Public Property CertDateApproved() As String Implements IImarkCertificationView.CertDateApproved
        Get
            Return txtCertDateApproved.Text
        End Get
        Set(ByVal value As String)
            txtCertDateApproved.Text = value
        End Set
    End Property

    Public Property DateSubmitted() As String Implements IImarkCertificationView.DateSubmitted
        Get
            Return txtDateSubmitted.Text
        End Get
        Set(ByVal value As String)
            txtDateSubmitted.Text = value
        End Set
    End Property

    Public Property DateApproved() As String Implements IImarkCertificationView.DateApproved
        Get
            Return txtDateApproved.Text
        End Get
        Set(ByVal value As String)
            txtDateApproved.Text = value
        End Set
    End Property
    Public Property DateExpiry() As String Implements IImarkCertificationView.DateExpiry
        Get
            Return txtDateExpiry.Text
        End Get
        Set(ByVal value As String)
            txtDateExpiry.Text = value
        End Set
    End Property

    Public Property RenewalRequired() As Boolean Implements IImarkCertificationView.RenewalRequired
        Get
            Return rbtnLstrenealRequired.Items(0).Selected
        End Get
        Set(ByVal value As Boolean)
            rbtnLstrenealRequired.Items(0).Selected = value
            rbtnLstrenealRequired.Items(1).Selected = Not value
        End Set
    End Property

    Public Property ActiveStatus() As Boolean Implements IImarkCertificationView.ActiveStatus
        Get
            Return rbtnlstActiveStatus.Items(0).Selected
        End Get
        Set(ByVal value As Boolean)
            rbtnlstActiveStatus.Items(0).Selected = value
            rbtnlstActiveStatus.Items(1).Selected = Not value
        End Set
    End Property

    Public Property EmarkReference() As String Implements IImarkCertificationView.EmarkReference
        Get
            Return txtEmarkReference.Text
        End Get
        Set(ByVal value As String)
            txtEmarkReference.Text = value
        End Set
    End Property

    Public Property ImarkFamily() As String Implements IImarkCertificationView.ImarkFamily
        Get
            Return txtImarkFamily.Text
        End Get
        Set(ByVal value As String)
            txtImarkFamily.Text = value
        End Set
    End Property

    Public Property ImarkStamping() As String Implements IImarkCertificationView.ImarkStamping
        Get
            Return txtIMarkStamping.Text
        End Get
        Set(ByVal value As String)
            txtIMarkStamping.Text = value
        End Set
    End Property

    Public Property ToBeRenewedCertificate() As DomainEntities.Certificate Implements Presenter.IImarkCertificationView.ToBeRenewedCertificate
        Get
            Return Session("ToBeRenewedCertificate")
        End Get
        Set(ByVal value As DomainEntities.Certificate)
            Session("ToBeRenewedCertificate") = value
        End Set
    End Property


    Public Property ManufacturingLocationId() As String Implements ICertificationView.ManufacturingLocationId
        Get
            Return 0
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    Public Property AuditLogList() As List(Of AuditLogEntry) Implements ICertificationView.AuditLogList
        Get
            Return m_lstAuditLog
        End Get
        Set(ByVal value As List(Of AuditLogEntry))
            m_lstAuditLog = value
        End Set
    End Property

    Public Property SimilarCertificateDS() As DataTable Implements ICertificationView.SimilarCertificateDS
        Get
            Return m_dtbSimilarCertificate
        End Get
        Set(ByVal value As DataTable)
            m_dtbSimilarCertificate = value
        End Set
    End Property

    'Added as per project 2706 technical specification
    Public Property DiscDate() As String Implements IImarkCertificationView.DiscDate
        Get
            Return lblDiscDate.Text
        End Get
        Set(ByVal value As String)
            lblDiscDate.Text = value
        End Set
    End Property
    'added for request 203625 - jeseitz 10/29/2016
    Public Property AddInfo() As String Implements IImarkCertificationView.AddInfo
        Get
            Return txtAddInfo.Text
        End Get
        Set(ByVal value As String)
            txtAddInfo.Text = value
        End Set

    End Property

    'JBH_2.00 Project 5325 - Added Operation Approval Date
    Public Property OperDateApproved() As String Implements IImarkCertificationView.OperDateApproved
        Get
            Return txtOperDateApproved.Text
        End Get
        Set(ByVal value As String)
            txtOperDateApproved.Text = value
        End Set
    End Property

    'JBH_2.00 Project 5325 - Added Mold Changed Flag
    Public Property MoldChgRequired() As Boolean Implements IImarkCertificationView.MoldChgRequired
        Get
            Return cbxMoldChgRequired.Checked
        End Get
        Set(ByVal value As Boolean)
            cbxMoldChgRequired.Checked = value
        End Set
    End Property

#End Region

#Region "Methods"

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        RaiseEvent Save()

    End Sub

    Protected Sub btnGettestResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTestResult.Click

        RaiseEvent GetTestResults()

    End Sub

    Protected Sub btnBlankResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlankResult.Click

        RaiseEvent GetBlankResults()

    End Sub



    ''' <summary>
    ''' Setup view control states according to specific context
    ''' </summary>
    ''' <param name="p_enuContext"></param>
    ''' <remarks></remarks>
    Public Sub SetupControlContextState(ByVal p_enuContext As Presenter.ICertificationView.Context) Implements Presenter.ICertificationView.SetupControlContextState

        Select Case p_enuContext
            Case ICertificationView.Context.NewCertificate
                TRView.IsVisible = False
                lbtnDefaultValues.Enabled = False

                btnGetTestResult.Enabled = True
                btnRenew.Enabled = False

                ' NOTE: m.b. [not applic] warning;
                ' if so, remove the warning and enable the gettestresult button when the adding Imark certificate approved;
            Case ICertificationView.Context.JustAddedCertificate
                TRView.IsVisible = False
                lbtnDefaultValues.Enabled = False
                txtCertificationNo.ReadOnly = True

                btnGetTestResult.Enabled = True
                btnRenew.Enabled = False
                ' ErrorText = "Warning: unknown IMARK certificate status"
            Case ICertificationView.Context.GotTestResults
                TRView.IsVisible = True
                lbtnDefaultValues.Enabled = False

                btnGetTestResult.Enabled = False
                btnRenew.Enabled = False
            Case ICertificationView.Context.ExistingCertificate
                TRView.IsVisible = True
                lbtnDefaultValues.Enabled = True

                btnGetTestResult.Enabled = False
                btnRenew.Enabled = False
            Case ICertificationView.Context.ExistingCertificateNoResults
                TRView.IsVisible = False
                lbtnDefaultValues.Enabled = False

                btnGetTestResult.Enabled = True
                btnRenew.Enabled = False
            Case Else
                ' Default
                ErrorText = "Warning: unknown certificate status"
        End Select

    End Sub

    Public Sub ShowSimilarTirePrompt() Implements ICertificationView.ShowSimilarTirePrompt

        If Me.SimilarCertificateDS.Rows.Count > 0 Then
            Dim Row As DataRow = Me.SimilarCertificateDS.Rows(0)
            Me.lblSimilarSKUID.Text = Row.Item("SKUID").ToString()
            Me.lblSimilarMaterial.Text = Row.Item("MATL_NUM").ToString()
            Me.ddlSimilarCertificate.DataBind()
            Me.SimilarTirePopUp.Show()
        End If

    End Sub

    Protected Sub Click_Yes(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strCertificationNumber As String = Me.ddlSimilarCertificate.SelectedItem.Text
        Dim intCertificationTypeID As Integer = Me.CertificationType
        Dim intSKUID As Integer = CInt(Me.lblSimilarSKUID.Text)
        Dim strMaterialNum As String = Me.lblSimilarMaterial.Text
        Dim blnDone As Boolean = False

        If m_presenter.GetCertificate(strCertificationNumber, intCertificationTypeID, intSKUID, strMaterialNum) Then
            Me.SimilarTirePopUp.Dispose()

            RaiseEvent CopySimilarTireSKUCertificate()
            RaiseEvent ShowTestResults()
        End If
    End Sub

    Protected Sub Click_No(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.SimilarTirePopUp.Dispose()
        RaiseEvent ShowTestResults()

    End Sub

    Protected Sub Click_CancelReasons(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Dispose pop-up.  Do not save changes to certificate
        ChangedFieldsPopup.Dispose()

        'Re-enable buttons
        btnBlankResult.Enabled = True
        btnGetTestResult.Enabled = True
    End Sub

    Protected Sub Click_SaveReasons(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim ddlReasonList As DropDownList
        Dim txtNote As TextBox
        Dim lstAuditLog As New List(Of AuditLogEntry)

        For Each Row As GridViewRow In gvChangedFields.Rows
            Dim entAuditLogEntry As New AuditLogEntry

            For j As Integer = 0 To gvChangedFields.Columns.Count() - 1
                Select Case gvChangedFields.Columns(j).HeaderText
                    Case "Area"
                        entAuditLogEntry.Area = Row.Cells(j).Text
                    Case "Change Date"
                        entAuditLogEntry.ChangeDateTime = Row.Cells(j).Text
                    Case "Changed Field"
                        entAuditLogEntry.ChangedFieldElement = Row.Cells(j).Text
                    Case "ChangeLogID"
                        If Row.Cells(j).Text = String.Empty Then
                            entAuditLogEntry.ChangeLogID = 0
                        Else
                            entAuditLogEntry.ChangeLogID = CInt(Row.Cells(j).Text)
                        End If
                    Case "ChangedBy"
                        entAuditLogEntry.ChangedBy = Row.Cells(j).Text
                    Case "OldValue"
                        entAuditLogEntry.OldValue = Row.Cells(j).Text
                    Case "NewValue"
                        entAuditLogEntry.NewValue = Row.Cells(j).Text
                    Case "Approver"
                        entAuditLogEntry.Approver = Row.Cells(j).Text
                    Case "Note"
                        txtNote = Row.Cells(1).FindControl("txtNote")
                        entAuditLogEntry.Note = txtNote.Text
                End Select
            Next
            ddlReasonList = Row.Cells(0).FindControl("ddlReasonList")
            entAuditLogEntry.ReasonID = CInt(ddlReasonList.SelectedValue())
            lstAuditLog.Add(entAuditLogEntry)
        Next

        AuditLogList = lstAuditLog

        'Dispose the Ajax pop-up
        ChangedFieldsPopup.Dispose()

        btnBlankResult.Enabled = True

        RaiseEvent SaveReasons()

    End Sub

    Public Sub SetupDefaultValuesView() Implements Presenter.ICertificationView.SetupDefaultValuesView

        If Me.CertificateNumberID > 0 Then
            Dim strUrl = "CertificationDefaults.aspx" & "?CertificationType=" & Me.CertificationType & "&CertificateNumberID=" & Me.CertificateNumberID.ToString()
            Response.Redirect(strUrl)
        End If

    End Sub

    Protected Sub lbtnDefaultValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDefaultValues.Click

        RaiseEvent ShowDefaultValues()

    End Sub

    Protected Sub Click_btnRenew(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRenew.Click

        RaiseEvent Renew()

        'We need to back out so that the certificate can be refreshed.
        Response.Redirect("CertificationSearchEx.aspx")

    End Sub

    ''' <summary>
    ''' Force view load
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DoLoadView() Implements Presenter.ICertificationView.DoLoadView

        MyBase.Page_Load(Nothing, Nothing)

    End Sub

    ''' <summary>
    ''' Display Changes to Client
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DisplayChangesToClient() Implements ICertificationView.DisplayChangesToClient

        'Set the data source(s) for the grid
        gvChangedFields.DataSource = AuditLogList

        For i As Integer = 0 To gvChangedFields.Columns.Count() - 1
            gvChangedFields.Columns(i).Visible = True
        Next

        'Bind the data source to the grid view
        gvChangedFields.DataBind()

        For i As Integer = 0 To gvChangedFields.Columns.Count() - 1
            Select Case gvChangedFields.Columns(i).HeaderText
                Case "ChangeLogID", _
                    "ChangedBy", _
                    "OldValue", _
                    "NewValue", _
                    "Approver"

                    gvChangedFields.Columns(i).Visible = False
            End Select
        Next

        'Display the ajax popup
        ChangedFieldsPopup.Show()

        Me.btnBlankResult.Enabled = False
        Me.btnRenew.Enabled = False

    End Sub

    Private Sub txtCertificationNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCertificationNo.TextChanged

        CertificationSearchPresenter.CurrentCertificateNumber = Me.txtCertificationNo.Text

    End Sub

    Private Sub cbxRemoveMatlNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxRemoveMatlNum.CheckedChanged

        RemoveMatlNum = cbxRemoveMatlNum.Checked


    End Sub

    ''' <summary>
    ''' Event for Family Text Change
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Public Sub txtImarkFamily_OnTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtImarkFamily.TextChanged
    '    RaiseEvent MoldStampingRefresh()
    'End Sub

    ''' <summary>
    ''' Event for Refresh Button Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As ImageClickEventArgs) Handles btnRefresh.Click
        RaiseEvent MoldStampingRefresh()
    End Sub
#End Region


End Class