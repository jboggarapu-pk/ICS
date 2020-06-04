Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' Add Certification form for all types
Partial Public Class AddCertificationUC
    Inherits BaseUserControl
    Implements IAddCertificationView

    ' Changed the sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Members"

    Public Event Save As CustomEvents.PlainEventHandler Implements IAddCertificationView.Save
    Public Event ReloadViewData() Implements Presenter.IAddCertificationView.ReloadViewData
    'Public Event Finish As CustomEvents.PlainEventHandler Implements IAddCertificationView.Finish
    Public Event CheckSKUExist As CustomEvents.CheckSKUExistEventHandler Implements IAddCertificationView.CheckSKUExist
    Public Event CheckDuplicateCertificateNumber As CustomEvents.PlainEventHandler Implements IAddCertificationView.CheckDuplicateCertificateNumber

    Private m_presenter As AddCertificationPresenter
    Private m_importers As DataSet
    Private m_ErrorNum As Integer
    Private m_InsertPC As String
    Private m_ErrorDesc As String
#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New AddCertificationPresenter(Me)

    End Sub

#End Region

#Region "Properties"

    Public Property SuccessText() As String Implements Presenter.IAddCertificationView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    Public Property ErrorText() As String Implements Presenter.IAddCertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property CertificateNumber() As String Implements Presenter.IAddCertificationView.CertificateNumber
        Get
            Return txtCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtCertNumber.Text = value
        End Set
    End Property

    Public Property Extension() As String Implements Presenter.IAddCertificationView.Extension
        Get
            Return txtExtension.Text
        End Get
        Set(ByVal value As String)
            txtExtension.Text = value
        End Set
    End Property

    Public Property CertificationName() As String Implements Presenter.IAddCertificationView.CertificationName
        Get
            Return Session("AddCertName")
        End Get
        Set(ByVal value As String)
            Session("AddCertName") = value
        End Set
    End Property

    Public Property AddCertificationTitle() As String Implements Presenter.IAddCertificationView.AddCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    Public Property MaterialList() As List(Of String) Implements Presenter.IAddCertificationView.MatlNumList
        Get
            Return Session("MaterialNumList")
        End Get
        Set(ByVal value As List(Of String))
            Session("MaterialNumList") = value
        End Set
    End Property

    Public Property CertNumErrMsgFlag() As Boolean Implements Presenter.IAddCertificationView.CertNumErrMsgFlag
        Get
            Return lblErrCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrCertNumber.Visible = value
        End Set
    End Property

    Public Property ExtensionErrMsgFlag() As Boolean Implements Presenter.IAddCertificationView.ExtensionErrMsgFlag
        Get
            Return lblErrExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrExtension.Visible = value
        End Set
    End Property

    Public Property Importers() As DataTable Implements IAddCertificationView.Importers
        Get
            Return ddlImporterNumber_N.DataSource
        End Get
        Set(ByVal value As DataTable)
            ddlImporterNumber_N.DataSource = value
            ddlImporterNumber_N.DataValueField = "importerid"
            ddlImporterNumber_N.DataTextField = "importer"
        End Set
    End Property

    Public Property Importer() As String Implements IAddCertificationView.Importer
        Get
            Return ddlImporterNumber_N.SelectedValue
        End Get
        Set(ByVal value As String)
            ddlImporterNumber_N.SelectedValue = value
        End Set
    End Property

    Public Property Customers() As DataTable Implements IAddCertificationView.Customers
        Get
            Return ddlCustomerNumber_N.DataSource
        End Get
        Set(ByVal value As DataTable)
            ddlCustomerNumber_N.DataSource = value
            ddlCustomerNumber_N.DataValueField = "customerid"
            ddlCustomerNumber_N.DataTextField = "customer"
        End Set
    End Property

    Public Property Customer() As String Implements IAddCertificationView.Customer
        Get
            Return ddlCustomerNumber_N.SelectedValue
        End Get
        Set(ByVal value As String)
            ddlCustomerNumber_N.SelectedValue = value
        End Set
    End Property

    Public Property ErrorNum() As Integer Implements IAddCertificationView.ErrorNum
        Get
            Return m_ErrorNum
        End Get
        Set(ByVal value As Integer)
            m_ErrorNum = value
        End Set
    End Property

    Public Property ErrorDesc() As String Implements IAddCertificationView.ErrorDesc
        Get
            Return m_ErrorDesc
        End Get
        Set(ByVal value As String)
            m_ErrorDesc = value
        End Set
    End Property

    Public Property InsertPC() As String Implements IAddCertificationView.InsertPC
        Get
            Return m_InsertPC
        End Get
        Set(ByVal value As String)
            m_InsertPC = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Setup view data if certificate is new
    ''' </summary>
    ''' <param name="p_strCertificationName"></param>
    ''' <param name="p_blnAnew"></param>
    ''' <remarks></remarks>
    Public Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean) Implements Presenter.IAddCertificationView.SetupViewData

        If p_blnAnew Then
            CertificationName = p_strCertificationName
            'Added as per project 2706 technical specification
            'Show extension based on certification type
            If CertificationName = "ECE3054" Or CertificationName = "ECE117" Then
                ShowExtension(True)
            Else
                ShowExtension(False)
            End If
            RaiseEvent ReloadViewData()
        End If

    End Sub

    ''' <summary>
    ''' Set up the add certification view based on the certification type
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetupAddCertificationView() Implements IAddCertificationView.SetupAddCertificationView

        'set up the cert number based on certification type
        Select Case CertificationName
            Case "Imark"
                'pnlAddCert_I.Visible = True
                txtCertNumber.Text = "544"
                txtCertNumber.ReadOnly = False  'Allow certificate number to be changed. - jeseitz 3/10/16
            Case "NOM"
                pnlAddCert_N.Visible = True
                txtCertNumber.ReadOnly = False
            Case "GSO"
                txtCertNumber.ReadOnly = False
                pnlAddCert_I.Visible = False
                pnlAddCert_N.Visible = False
            Case Else
                pnlAddCert_I.Visible = False
                pnlAddCert_N.Visible = False
                txtCertNumber.ReadOnly = False
        End Select

        're-populate the Material table since it's auto generated
        tblMatlNum.Rows.Clear()
        Dim i As Integer
        If MaterialList IsNot Nothing AndAlso MaterialList.Count <> 0 Then
            For i = 0 To MaterialList.Count - 1
                AddSKUTableRow(MaterialList(i))
            Next
        End If

    End Sub

    Public Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IAddCertificationView.SetupCertNumErrMsg

        lblErrCertNumber.Visible = p_blnDuplicateFlag

    End Sub

    Public Sub SetupExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IAddCertificationView.SetupExtensionErrMsg

        lblErrExtension.Visible = p_blnDuplicateFlag

    End Sub

    ''' <summary>
    ''' Dynamically add Material number textbox to the view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_btnAddMaterial(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMaterial.Click

        SuccessText = "&nbsp;"
        ErrorText = String.Empty

        If MaterialList IsNot Nothing Then
            If MaterialList.Count <> 0 Then
                If String.IsNullOrEmpty(MaterialList(MaterialList.Count - 1)) Then
                    Me.ConfirmPopUp.Show()
                    Return
                End If
            End If
        End If

        AddSKUTableRow(String.Empty)
        'Add to the Material list session object
        If MaterialList Is Nothing Then
            MaterialList = New List(Of String)
        End If
        MaterialList.Add(String.Empty)

    End Sub

    Protected Sub Click_btnSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        SuccessText = "&nbsp;"
        ErrorText = String.Empty

        If CheckForErrorMessages() Then
            RaiseEvent Save()
            If ErrorNum = 7 Then
                lblErr2.Text = m_ErrorDesc
                ConfirmPopUpErr2.Show()
            End If
        End If
    End Sub

    Protected Sub Click_btnCancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        If CertificateNumber IsNot String.Empty Or MaterialList IsNot Nothing Then
            Me.CancelAlertPopUp.Show()
        Else
            'jeseitz 6/15/2016 - added 0 as second parameter - not used in this case.
            CType(Me.Page, CertificationSearchEx).ActivateCertificateControl(String.Empty, 0)
        End If

    End Sub

    Protected Sub Click_OK(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.ConfirmPopUp.Dispose()
        RaiseEvent Save()
        If ErrorNum = 7 Then
            lblErr2.Text = m_ErrorDesc
            ConfirmPopUpErr2.Show()
        End If
    End Sub

    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.CancelAlertPopUp.Dispose()
        'jeseitz 6/15/2016 - added 0 as second parameter - not used in this case.
        CType(Me.Page, CertificationSearchEx).ActivateCertificateControl(String.Empty, 0)

    End Sub

    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.CancelAlertPopUp.Dispose()

    End Sub

    ''' <summary>
    ''' Setup the Material Table rows
    ''' </summary>
    ''' <param name="p_strMatlNum"></param>
    ''' <remarks></remarks>
    Private Sub AddSKUTableRow(ByVal p_strMatlNum As String)

        Dim tRow As New HtmlTableRow()
        Dim rowCount As Integer = tblMatlNum.Rows.Count
        Dim tCellLeft As New HtmlTableCell()
        tCellLeft.Align = "right"
        Dim tCellRight As New HtmlTableCell()
        Dim tCellError As New HtmlTableCell()
        tCellError.Width = 320
        tCellError.Align = "left"

        Dim lblMaterialNum As New Label()
        Dim txtMaterialNum As New TextBox()
        Dim lblErr As New Label()

        lblMaterialNum.ID = "lblMaterial" + rowCount.ToString
        lblMaterialNum.Text = "Material :"
        lblMaterialNum.CssClass = "AddCertificationSKULable"
        lblMaterialNum.Width = 100
        lblMaterialNum.Font.Size = FontUnit.XSmall

        txtMaterialNum.ID = "txtMaterial" + rowCount.ToString
        txtMaterialNum.Text = p_strMatlNum
        AddHandler txtMaterialNum.TextChanged, AddressOf Me.TextChanged_txtMaterialNum
        txtMaterialNum.Font.Size = FontUnit.XSmall
        txtMaterialNum.Width = 170
        txtMaterialNum.AutoPostBack = False

        lblErr.ID = "lblErrMaterial" + rowCount.ToString
        lblErr.Text = "The Material Number does not exist."
        lblErr.CssClass = "AddCertificationSKUError"
        lblErr.Visible = False
        lblErr.Width = 320
        lblErr.Font.Size = FontUnit.XSmall

        tCellLeft.Controls.Add(lblMaterialNum)
        tCellRight.Controls.Add(txtMaterialNum)
        tCellError.Controls.Add(lblErr)
        tRow.Cells.Add(tCellLeft)
        tRow.Cells.Add(tCellRight)
        tRow.Cells.Add(tCellError)

        tblMatlNum.Rows.Add(tRow)

    End Sub

    ''' <summary>
    ''' Called when textbox textchanged event raised
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TextChanged_txtMaterialNum(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim txtMaterial As TextBox = CType(sender, TextBox)
        Dim intIndex As Integer = txtMaterial.ID.Substring(11)
        RaiseEvent CheckSKUExist(txtMaterial.Text, intIndex)

    End Sub

    Public Sub SetupSKUErrMsg(ByVal p_intIndex As Integer, ByVal p_blnExistFlag As Boolean) Implements Presenter.IAddCertificationView.SetupSKUErrMsg

        For Each tbrRow As HtmlTableRow In tblMatlNum.Rows
            If CType(tbrRow.Cells(2).Controls(0), Label).ID.Contains("ErrMaterialNum" + p_intIndex.ToString()) Then
                CType(tbrRow.Cells(2).Controls(0), Label).Visible = Not p_blnExistFlag
                Exit For
            End If
        Next

    End Sub

    Private Function CheckForErrorMessages() As Boolean

        If lblErrCertNumber.Visible Or lblErrExtension.Visible Then
            Return False
        End If
        For Each tbrRow As HtmlTableRow In tblMatlNum.Rows
            If CType(tbrRow.Cells(2).Controls(0), Label).Visible Then
                Return False
            End If
        Next
        Return True

    End Function

    'Added as per project 2706 technical specification
    ''' <summary>
    ''' Show or hide Extension label and textbox
    ''' </summary>
    ''' <param name="p_blnShowFlag"></param>
    ''' <remarks></remarks>
    Private Sub ShowExtension(ByVal p_blnShowFlag As Boolean)

        lblExtension.Visible = p_blnShowFlag
        txtExtension.Visible = p_blnShowFlag

    End Sub

    ''' <summary>
    ''' button Ok Err2 Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnOkErr2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOkErr2.Click
        InsertPC = "y"
        RaiseEvent Save()
        ConfirmPopUpErr2.Dispose()
    End Sub

    ''' <summary>
    ''' button Cancle Err2 Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnCancleErr2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancleErr2.Click
        ConfirmPopUpErr2.Dispose()
    End Sub
#End Region

    
End Class