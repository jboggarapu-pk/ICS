Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' Dup Correct Certification form for all types
Partial Public Class DupCorrectCertificationUC
    Inherits BaseUserControl
    Implements IDupCorrectCertificationView

#Region "Members"

    Public Event ReloadViewData() Implements Presenter.IDupCorrectCertificationView.ReloadViewData
    Public Event View As CustomEvents.PlainEventHandler Implements IDupCorrectCertificationView.View
    Public Event Delete As CustomEvents.PlainEventHandler Implements IDupCorrectCertificationView.Delete

    Private m_presenter As DupCorrectCertificationPresenter

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New DupCorrectCertificationPresenter(Me)

    End Sub

#End Region

#Region "Properties"

    Public Property DuplicateCertificates() As DataTable Implements Presenter.IDupCorrectCertificationView.DuplicateCertificates
        Get
            Return Session("DupCertificates")
        End Get
        Set(ByVal value As DataTable)
            Session("DupCertificates") = value
            gvDuplicateCertificates.DataSource = value
        End Set
    End Property

    Public Property SuccessText() As String Implements Presenter.IDupCorrectCertificationView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    Public Property ErrorText() As String Implements Presenter.IDupCorrectCertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property CertId() As Integer Implements Presenter.IDupCorrectCertificationView.CertId
        Get
            Return CInt(Session("CertId"))
        End Get
        Set(ByVal value As Integer)
            Session("CertId") = value
        End Set
    End Property

    Public Property MaterialNumber() As String Implements Presenter.IDupCorrectCertificationView.MaterialNumber
        Get
            Return txtMatlNumber.Text
        End Get
        Set(ByVal value As String)
            txtMatlNumber.Text = value
        End Set
    End Property

    Public Property SpeedRange() As String Implements Presenter.IDupCorrectCertificationView.SpeedRating
        Get
            Return txtSpeedRating.Text
        End Get
        Set(ByVal value As String)
            txtSpeedRating.Text = value
        End Set
    End Property

    Public Property CertificationName() As String Implements Presenter.IDupCorrectCertificationView.CertificationName
        Get
            Return Session("AddCertName")
        End Get
        Set(ByVal value As String)
            Session("AddCertName") = value
        End Set
    End Property

    Public Property AddCertificationTitle() As String Implements Presenter.IDupCorrectCertificationView.AddCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Setup view data if certificate is new
    ''' </summary>
    ''' <param name="p_strCertificationName"></param>
    ''' <param name="p_blnDupCorrect"></param>
    ''' <remarks></remarks>
    Public Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnDupCorrect As Boolean) Implements Presenter.IDupCorrectCertificationView.SetupViewData

        If p_blnDupCorrect Then
            CertificationName = p_strCertificationName
            RaiseEvent ReloadViewData()
        End If

    End Sub

    Protected Sub Click_btnView(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click

        ClearGrid()
        RaiseEvent View()

    End Sub

    Private Sub ClearGrid()

        gvDuplicateCertificates.PageIndex = 0
        gvDuplicateCertificates.DataSource = Nothing
        gvDuplicateCertificates.DataBind()

    End Sub

    Protected Sub gvDuplicateCertificates_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvDuplicateCertificates.PageIndexChanging

        gvDuplicateCertificates.PageIndex = e.NewPageIndex
        gvDuplicateCertificates.DataSource = DuplicateCertificates
        gvDuplicateCertificates.DataBind()

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim obj As DataControlFieldCell = CType(sender.Parent, DataControlFieldCell)
        Dim obj2 As GridViewRow = obj.Parent
        Dim pos = obj2.DataItemIndex
        Session("CertId") = CInt(DuplicateCertificates.Rows(pos)(0))

        Me.CancelAlertPopUp.Show()

    End Sub

    Protected Sub gvDuplicateCertificates_DataBound(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gvDuplicateCertificates.DataBound

        If gvDuplicateCertificates.Rows.Count > 0 Then
            Dim dupSku As List(Of String) = New List(Of String)
            For Each row As GridViewRow In gvDuplicateCertificates.Rows
                Dim btn As Button = DirectCast(row.Cells(5).Controls(1), Button)
                If (btn IsNot Nothing) Then
                    Dim skuId As String = CStr(row.Cells(0).Text)
                    If dupSku.Contains(skuId) Then
                        btn.Visible = False
                    Else
                        dupSku.Add(skuId)
                    End If
                End If
            Next
            dupSku.Clear()
        End If

    End Sub

    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.CancelAlertPopUp.Dispose()
        RaiseEvent Delete()

    End Sub

    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.CancelAlertPopUp.Dispose()

    End Sub

#End Region

End Class