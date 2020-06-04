Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

' Certification default values view
Partial Public Class CertificationDefaultsUC
    Inherits BaseCertificationControl
    Implements ICertificationDefaultsView

#Region "Members"
    ''' <summary>
    ''' Certification defaults view presenter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_presenter As CertificationDefaultsPresenter
    ''' <summary>
    ''' Save Certificate Type Defaults event.
    ''' </summary>
    ''' <param name="p_object"></param>
    ''' <remarks></remarks>
    Public Event SaveCertificateTypeDefaults(ByVal p_object As Object) Implements ICertificationDefaultsView.SaveCertificateTypeDefaults
    ''' <summary>
    ''' Save Certificate Defaults event.
    ''' </summary>
    ''' <param name="p_object"></param>
    ''' <remarks></remarks>
    Public Event SaveCertificateDefaults(ByVal p_object As Object) Implements Presenter.ICertificationDefaultsView.SaveCertificateDefaults
    ''' <summary>
    ''' Certificate Type selected event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event CertificateTypeSelected As CustomEvents.PlainEventHandler Implements ICertificationDefaultsView.CertificateTypeSelected
    ''' <summary>
    '''  Constant to hold FieldValue text
    ''' </summary>
    Private Const FieldValueText As String = "FieldValue"
    ''' <summary>
    '''  Constant to hold GSO text
    ''' </summary>
    Private Const GSOText As String = "GSO"
    ''' <summary>
    '''  Constant to hold Rayon text
    ''' </summary>
    Private Const RayonText As String = "Rayon"
    ''' <summary>
    '''  Constant to hold Other text
    ''' </summary>
    Private Const OtherText As String = "Other"
    ''' <summary>
    '''  Constant to hold FieldRad text
    ''' </summary>
    Private Const FieldRadText As String = "FieldRad"
#End Region

#Region "Constructors"
    ''' <summary>
    ''' Default Constructor to initialize class members.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New CertificationDefaultsPresenter(Me)

    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets or sets Error Text value.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements ICertificationDefaultsView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Info Text value.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property InfoText() As String Implements ICertificationDefaultsView.InfoText
        Get
            Return lblInfoText.Text
        End Get
        Set(ByVal value As String)
            lblInfoText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certification Names value.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property CertificationNames() As List(Of String) Implements ICertificationDefaultsView.CertificationNames
        Get
            Return CType(ddlCertNames.DataSource, Global.System.Collections.Generic.List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            ddlCertNames.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Type value.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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

    ''' <summary>
    ''' Gets or sets CertificateNo ToShow value.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property CertificateNoToShow() As String Implements ICertificationDefaultsView.CertificateNoToShow
        Get
            Return txtCertificateNo.Text
        End Get
        Set(ByVal value As String)
            txtCertificateNo.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets CertificateNo value.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property CertificateNo() As String Implements ICertificationDefaultsView.CertificateNo
        Get
            Return CStr(Session("CertificationDefaultsView_CertificateNo"))
        End Get
        Set(ByVal value As String)
            Session("CertificationDefaultsView_CertificateNo") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets CertificateNumber ID value.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertificateNumberID() As Integer Implements Presenter.ICertificationDefaultsView.CertificateNumberID
        Get
            Return CInt(Session(Me.GetType().Name & "CertificateNumberID"))
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "CertificateNumberID") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Fields value.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertificateFields() As System.Collections.Generic.List(Of DomainEntities.CertificationDefaultField) Implements ICertificationDefaultsView.CertificateFields
        Get
            Return CType(Session("CertiFieldEntries"), Global.System.Collections.Generic.List(Of Global.CooperTire.ICS.DomainEntities.CertificationDefaultField))
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of DomainEntities.CertificationDefaultField))
            Session("CertiFieldEntries") = value
            gvCertificateFields.DataSource = value
            gvCertificateFields.DataBind()
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Save default Button Click.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnSaveDefault(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDefault.Click
        Try
            If ddlCertNames.SelectedValue = GSOText Then
                For i As Integer = 0 To gvCertificateFields.Rows.Count - 1
                    If gvCertificateFields.Rows(i).DataItemIndex() = 5 Then
                        Dim rbFieldRad As RadioButtonList
                        Dim tbFieldValue As TextBox
                        rbFieldRad = CType(gvCertificateFields.Rows(i).FindControl(FieldRadText), RadioButtonList)
                        tbFieldValue = CType(gvCertificateFields.Rows(i).FindControl(FieldValueText), TextBox)
                        If rbFieldRad.SelectedIndex = 0 Then
                            tbFieldValue.Text = RayonText
                        Else
                            tbFieldValue.Text = OtherText
                        End If
                    End If
                Next
            End If

            RaiseEvent SaveCertificateTypeDefaults(GetDefaultFieldItemsFromGrid())
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Save Certificate Button Click.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnSaveCert(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCert.Click
        Try
            If ddlCertNames.SelectedValue = GSOText Then
                For i As Integer = 0 To gvCertificateFields.Rows.Count - 1
                    If gvCertificateFields.Rows(i).DataItemIndex() = 5 Then
                        Dim rbFieldRad As RadioButtonList
                        Dim tbFieldValue As TextBox
                        rbFieldRad = CType(gvCertificateFields.Rows(i).FindControl(FieldRadText), RadioButtonList)
                        tbFieldValue = CType(gvCertificateFields.Rows(i).FindControl(FieldValueText), TextBox)
                        If rbFieldRad.SelectedIndex = 0 Then
                            tbFieldValue.Text = RayonText
                        Else
                            tbFieldValue.Text = OtherText
                        End If
                    End If
                Next
            End If

            RaiseEvent SaveCertificateDefaults(GetDefaultFieldItemsFromGrid())
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Certificate names selected index change.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub ddlCertNames_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCertNames.SelectedIndexChanged
        Try
            If ddlCertNames.SelectedValue.Length > 0 Then
                RaiseEvent CertificateTypeSelected()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Get modified default field items from grid.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns></returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function GetDefaultFieldItemsFromGrid() As List(Of CertificationDefaultField)

        Dim listCertificationDefaultField As List(Of CertificationDefaultField) = New List(Of CertificationDefaultField)
        Dim certDefaultField As CertificationDefaultField
        Try
            For Each row As GridViewRow In gvCertificateFields.Rows
                Dim pos As Integer = row.DataItemIndex
                Dim tbxFieldVal As TextBox = CType(row.FindControl(FieldValueText), TextBox)

                certDefaultField = CertificateFields(pos)
                certDefaultField.Value = tbxFieldVal.Text
                listCertificationDefaultField.Add(certDefaultField)
            Next

            Return listCertificationDefaultField
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Get view input parameters.
    ''' </summary>
    ''' <param name="p_strCertificateType">Certificate Type</param>
    ''' <param name="p_intCertificateNoID">Certificate No Id</param>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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
    ''' Setup view controls state relevant to current context.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupControlContextState() Implements Presenter.ICertificationDefaultsView.SetupControlContextState
        Const CertificationTypeText As String = "Certification Type Default Values"
        Const CertificationTypeDefaultvaluesText As String = "Certification Default Values"
        Const ECE3054Text As String = "ECE3054"
        Const ECE117Text As String = "ECE117"
        Try
            If txtCertificateNo.Text = String.Empty Then
                txtCertificateNo.Visible = False
                btnSaveCert.Visible = False
                lblCertNo.Visible = False
                lblInfo.Text = CertificationTypeText

                btnSaveDefault.Enabled = (Not gvCertificateFields.DataSource Is Nothing)
            Else
                txtCertificateNo.ReadOnly = True
                btnSaveDefault.Visible = False
                ddlCertNames.Enabled = False
                lblInfo.Text = CertificationTypeDefaultvaluesText
            End If

            'Emark 
            If ddlCertNames.SelectedValue = ECE3054Text Or ddlCertNames.SelectedValue = ECE117Text Then
                For i As Integer = 0 To gvCertificateFields.Rows.Count - 1
                    If gvCertificateFields.Rows(i).DataItemIndex() = 1 Or gvCertificateFields.Rows(i).DataItemIndex() = 2 Or gvCertificateFields.Rows(i).DataItemIndex() = 22 Then 'Manufacturer's name and address or Technical Service
                        Dim tbFieldValue As TextBox
                        tbFieldValue = CType(gvCertificateFields.Rows(i).FindControl(FieldValueText), TextBox)
                        tbFieldValue.Height = 50
                        tbFieldValue.TextMode = TextBoxMode.MultiLine
                    End If
                Next
            End If

            'GSO
            If ddlCertNames.SelectedValue = GSOText Then
                Dim lstRadiobutton As New List(Of String)
                lstRadiobutton.Add(RayonText)
                lstRadiobutton.Add(OtherText)
                For i As Integer = 0 To gvCertificateFields.Rows.Count - 1
                    If gvCertificateFields.Rows(i).DataItemIndex() = 5 Then
                        'Intialize and show radio button list
                        Dim rbFieldRad As RadioButtonList
                        rbFieldRad = CType(gvCertificateFields.Rows(i).FindControl(FieldRadText), RadioButtonList)
                        rbFieldRad.DataSource = lstRadiobutton
                        rbFieldRad.DataBind()
                        rbFieldRad.Visible = True
                        'Hide text box
                        Dim tbFieldValue As TextBox
                        tbFieldValue = CType(gvCertificateFields.Rows(i).FindControl(FieldValueText), TextBox)
                        If tbFieldValue.Text = RayonText Then
                            rbFieldRad.SelectedIndex = 0
                        Else
                            rbFieldRad.SelectedIndex = 1
                        End If
                        tbFieldValue.Visible = False
                    End If
                Next
            End If
        Catch
            Throw
        End Try
    End Sub
#End Region

End Class
