Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

' Certification default values view
Partial Public Class CertificationDefaultsUC
    Inherits BaseCertificationControl
    Implements ICertificationDefaultsView

#Region "Members"

    Private m_presenter As CertificationDefaultsPresenter

    Public Event SaveCertificateTypeDefaults(ByVal p_object As Object) Implements ICertificationDefaultsView.SaveCertificateTypeDefaults
    Public Event SaveCertificateDefaults(ByVal p_object As Object) Implements Presenter.ICertificationDefaultsView.SaveCertificateDefaults
    Public Event CertificateTypeSelected As CustomEvents.PlainEventHandler Implements ICertificationDefaultsView.CertificateTypeSelected

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New CertificationDefaultsPresenter(Me)

    End Sub

#End Region

#Region "Properties"

    Public Property ErrorText() As String Implements ICertificationDefaultsView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property InfoText() As String Implements ICertificationDefaultsView.InfoText
        Get
            Return lblInfoText.Text
        End Get
        Set(ByVal value As String)
            lblInfoText.Text = value
        End Set
    End Property

    Property CertificationNames() As List(Of String) Implements ICertificationDefaultsView.CertificationNames
        Get
            Return ddlCertNames.DataSource
        End Get
        Set(ByVal value As List(Of String))
            ddlCertNames.DataSource = value
        End Set
    End Property

    Public Property CertificateType() As String Implements Presenter.ICertificationDefaultsView.CertificateType
        Get
            If ddlCertNames.SelectedIndex = 0 Then
                Return String.Empty
            Else
                Return ddlCertNames.SelectedValue
            End If
        End Get
        Set(ByVal value As String)
            Dim liCertType As ListItem = ddlCertNames.Items.FindByText(value)
            If Not liCertType Is Nothing Then
                liCertType.Selected = True
            Else
                ddlCertNames.SelectedIndex = 0
            End If
        End Set
    End Property

    Property CertificateNoToShow() As String Implements ICertificationDefaultsView.CertificateNoToShow
        Get
            Return txtCertificateNo.Text
        End Get
        Set(ByVal value As String)
            txtCertificateNo.Text = value
        End Set
    End Property

    Property CertificateNo() As String Implements ICertificationDefaultsView.CertificateNo
        Get
            Return Session("CertificationDefaultsView_CertificateNo")
        End Get
        Set(ByVal value As String)
            Session("CertificationDefaultsView_CertificateNo") = value
        End Set
    End Property

    Public Property CertificateNumberID() As Integer Implements Presenter.ICertificationDefaultsView.CertificateNumberID
        Get
            Return Session(Me.GetType().Name & "CertificateNumberID")
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "CertificateNumberID") = value
        End Set
    End Property

    Public Property CertificateFields() As System.Collections.Generic.List(Of DomainEntities.CertificationDefaultField) Implements ICertificationDefaultsView.CertificateFields
        Get
            Return Session("CertiFieldEntries")
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of DomainEntities.CertificationDefaultField))
            Session("CertiFieldEntries") = value
            gvCertificateFields.DataSource = value
            gvCertificateFields.DataBind()
        End Set
    End Property

#End Region

#Region "Methods"

    Protected Sub Click_btnSaveDefault(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDefault.Click

        If ddlCertNames.SelectedValue = "GSO" Then
            For i As Integer = 0 To gvCertificateFields.Rows.Count - 1
                If gvCertificateFields.Rows(i).DataItemIndex() = 5 Then
                    Dim rbFieldRad As RadioButtonList
                    Dim tbFieldValue As TextBox
                    rbFieldRad = gvCertificateFields.Rows(i).FindControl("FieldRad")
                    tbFieldValue = gvCertificateFields.Rows(i).FindControl("FieldValue")
                    If rbFieldRad.SelectedIndex = 0 Then
                        tbFieldValue.Text = "Rayon"
                    Else
                        tbFieldValue.Text = "Other"
                    End If
                End If
            Next
        End If

        RaiseEvent SaveCertificateTypeDefaults(GetDefaultFieldItemsFromGrid())

    End Sub

    Protected Sub Click_btnSaveCert(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCert.Click

        If ddlCertNames.SelectedValue = "GSO" Then
            For i As Integer = 0 To gvCertificateFields.Rows.Count - 1
                If gvCertificateFields.Rows(i).DataItemIndex() = 5 Then
                    Dim rbFieldRad As RadioButtonList
                    Dim tbFieldValue As TextBox
                    rbFieldRad = gvCertificateFields.Rows(i).FindControl("FieldRad")
                    tbFieldValue = gvCertificateFields.Rows(i).FindControl("FieldValue")
                    If rbFieldRad.SelectedIndex = 0 Then
                        tbFieldValue.Text = "Rayon"
                    Else
                        tbFieldValue.Text = "Other"
                    End If
                End If
            Next
        End If

        RaiseEvent SaveCertificateDefaults(GetDefaultFieldItemsFromGrid())

    End Sub

    Protected Sub ddlCertNames_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCertNames.SelectedIndexChanged

        If ddlCertNames.SelectedValue.Length > 0 Then

            RaiseEvent CertificateTypeSelected()

        End If

    End Sub

    ''' <summary>
    ''' Get modified default field items from grid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDefaultFieldItemsFromGrid() As List(Of CertificationDefaultField)

        Dim listCertificationDefaultField As List(Of CertificationDefaultField) = New List(Of CertificationDefaultField)
        Dim certDefaultField As CertificationDefaultField

        For Each row As GridViewRow In gvCertificateFields.Rows
            Dim pos = row.DataItemIndex
            Dim tbxFieldVal As TextBox = row.FindControl("FieldValue")

            certDefaultField = CertificateFields(pos)
            certDefaultField.Value = tbxFieldVal.Text
            listCertificationDefaultField.Add(certDefaultField)
        Next

        Return listCertificationDefaultField

    End Function

    ''' <summary>
    ''' Get view input parameters
    ''' </summary>
    ''' <param name="p_strCertificateType"></param>
    ''' <param name="p_intCertificateNoID"></param>
    ''' <remarks></remarks>
    Public Sub GetViewInputParams(ByRef p_strCertificateType As String, ByRef p_intCertificateNoID As Integer) Implements Presenter.ICertificationDefaultsView.GetViewInputParams

        Try
            If Page.Request.QueryString.Count = 2 Then
                Dim enuCertificateType As Integer = CType(Page.Request.QueryString(0).ToString, Integer)
                p_strCertificateType = enuCertificateType.ToString()
                p_intCertificateNoID = Convert.ToInt32(Page.Request.QueryString(1))
            End If
        Catch ex As Exception
            p_strCertificateType = String.Empty
            p_intCertificateNoID = 0
        End Try

    End Sub

    ''' <summary>
    ''' Setup view controls state relevant to current context
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetupControlContextState() Implements Presenter.ICertificationDefaultsView.SetupControlContextState

        If txtCertificateNo.Text = String.Empty Then
            txtCertificateNo.Visible = False
            btnSaveCert.Visible = False
            lblCertNo.Visible = False
            lblInfo.Text = "Certification Type Default Values"

            btnSaveDefault.Enabled = (Not gvCertificateFields.DataSource Is Nothing)
        Else
            txtCertificateNo.ReadOnly = True
            btnSaveDefault.Visible = False
            ddlCertNames.Enabled = False
            lblInfo.Text = "Certification Default Values"
        End If

        'Emark 
        If ddlCertNames.SelectedValue = "ECE3054" Or ddlCertNames.SelectedValue = "ECE117" Then
            For i As Integer = 0 To gvCertificateFields.Rows.Count - 1
                If gvCertificateFields.Rows(i).DataItemIndex() = 1 Or gvCertificateFields.Rows(i).DataItemIndex() = 2 Or gvCertificateFields.Rows(i).DataItemIndex() = 22 Then 'Manufacturer's name and address or Technical Service
                    Dim tbFieldValue As TextBox
                    tbFieldValue = gvCertificateFields.Rows(i).FindControl("FieldValue")
                    tbFieldValue.Height = 50
                    tbFieldValue.TextMode = TextBoxMode.MultiLine
                End If
            Next
        End If

        'GSO
        If ddlCertNames.SelectedValue = "GSO" Then
            Dim lstRadiobutton As New List(Of String)
            lstRadiobutton.Add("Rayon")
            lstRadiobutton.Add("Other")
            For i As Integer = 0 To gvCertificateFields.Rows.Count - 1
                If gvCertificateFields.Rows(i).DataItemIndex() = 5 Then
                    'Intialize and show radio button list
                    Dim rbFieldRad As RadioButtonList
                    rbFieldRad = gvCertificateFields.Rows(i).FindControl("FieldRad")
                    rbFieldRad.DataSource = lstRadiobutton
                    rbFieldRad.DataBind()
                    rbFieldRad.Visible = True
                    'Hide text box
                    Dim tbFieldValue As TextBox
                    tbFieldValue = gvCertificateFields.Rows(i).FindControl("FieldValue")
                    If tbFieldValue.Text = "Rayon" Then
                        rbFieldRad.SelectedIndex = 0
                    Else
                        rbFieldRad.SelectedIndex = 1
                    End If
                    tbFieldValue.Visible = False
                End If
            Next
        End If

    End Sub

#End Region

End Class
