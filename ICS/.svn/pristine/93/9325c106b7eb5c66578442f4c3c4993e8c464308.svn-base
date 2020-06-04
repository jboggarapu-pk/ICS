Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' DetachOrMove Certification form for all types
Partial Public Class DetachOrMoveCertificationUC
    Inherits BaseUserControl
    Implements IDetachOrMoveCertificationView

#Region "Members"

    Public Event ShowCertificateMaterials As CustomEvents.PlainEventHandler Implements IDetachOrMoveCertificationView.ShowCertificateMaterials
    Public Event Detach As CustomEvents.PlainEventHandler Implements IDetachOrMoveCertificationView.Detach
    Public Event Move As CustomEvents.PlainEventHandler Implements IDetachOrMoveCertificationView.Move
    Public Event ReloadViewData() Implements Presenter.IDetachOrMoveCertificationView.ReloadViewData

    Private m_presenter As DetachOrMoveCertificationPresenter

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New DetachOrMoveCertificationPresenter(Me)

    End Sub

#End Region

#Region "Properties"

    Public Property SuccessText() As String Implements Presenter.IDetachOrMoveCertificationView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    Public Property ErrorText() As String Implements Presenter.IDetachOrMoveCertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property CertificateNumber() As String Implements Presenter.IDetachOrMoveCertificationView.CertificateNumber
        Get
            Return txtCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtCertNumber.Text = value
        End Set
    End Property

    Public Property Extension() As String Implements Presenter.IDetachOrMoveCertificationView.Extension
        Get
            Return txtExtension.Text
        End Get
        Set(ByVal value As String)
            txtExtension.Text = value
        End Set
    End Property

    Public Property SkuId() As Integer Implements Presenter.IDetachOrMoveCertificationView.SkuId
        Get
            Return CInt(hidSkuId.Value)
        End Get
        Set(ByVal value As Integer)
            hidSkuId.Value = value.ToString()
        End Set
    End Property

    Public Property CertificateId() As Integer Implements Presenter.IDetachOrMoveCertificationView.CertificateId
        Get
            Return CInt(hidCertificateId.Value)
        End Get
        Set(ByVal value As Integer)
            hidCertificateId.Value = value.ToString()
        End Set
    End Property

    Public Property NewCertificateNumber() As String Implements Presenter.IDetachOrMoveCertificationView.NewCertificateNumber
        Get
            Return txtNewCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtNewCertNumber.Text = value
        End Set
    End Property

    Public Property NewExtension() As String Implements Presenter.IDetachOrMoveCertificationView.NewExtension
        Get
            Return txtNewExtension.Text
        End Get
        Set(ByVal value As String)
            txtNewExtension.Text = value
        End Set
    End Property

    Public Property CertificationName() As String Implements Presenter.IDetachOrMoveCertificationView.CertificationName
        Get
            Return Session("AddCertName")
        End Get
        Set(ByVal value As String)
            Session("AddCertName") = value
        End Set
    End Property

    Public Property AddCertificationTitle() As String Implements Presenter.IDetachOrMoveCertificationView.AddCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    Public Property CertNumErrMsgFlag() As Boolean Implements Presenter.IDetachOrMoveCertificationView.CertNumErrMsgFlag
        Get
            Return lblErrCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrCertNumber.Visible = value
        End Set
    End Property

    Public Property ExtensionErrMsgFlag() As Boolean Implements Presenter.IDetachOrMoveCertificationView.ExtensionErrMsgFlag
        Get
            Return lblErrExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrExtension.Visible = value
        End Set
    End Property

    Public Property NewCertNumErrMsgFlag() As Boolean Implements Presenter.IDetachOrMoveCertificationView.NewCertNumErrMsgFlag
        Get
            Return lblErrNewCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrNewCertNumber.Visible = value
        End Set
    End Property

    Public Property NewExtensionErrMsgFlag() As Boolean Implements Presenter.IDetachOrMoveCertificationView.NewExtensionErrMsgFlag
        Get
            Return lblErrNewExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrNewExtension.Visible = value
        End Set
    End Property

    Public Property CertificateMaterials() As DataTable Implements Presenter.IDetachOrMoveCertificationView.CertificateMaterials
        Get
            Return Session("CertMaterials")
        End Get
        Set(ByVal value As DataTable)
            Session("CertMaterials") = value
            gvCertMaterials.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Setup view data if certificate is new
    ''' </summary>
    ''' <param name="p_strCertificationName"></param>
    ''' <param name="p_blnDetachOrMove"></param>
    ''' <remarks></remarks>
    Public Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnDetachOrMove As Boolean) Implements Presenter.IDetachOrMoveCertificationView.SetupViewData

        If p_blnDetachOrMove Then
            CertificationName = p_strCertificationName
            RaiseEvent ReloadViewData()
        End If

    End Sub

    ''' <summary>
    ''' Set up the add certification view based on the certification type
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetupDetachOrMoveCertificationView() Implements Presenter.IDetachOrMoveCertificationView.SetupDetachOrMoveCertificationView

        'Show extension based on certification type
        If CertificationName = "ECE3054" Or CertificationName = "ECE117" Then
            ShowExtension(True)
        Else
            ShowExtension(False)
        End If

    End Sub

    Public Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IDetachOrMoveCertificationView.SetupCertNumErrMsg

        lblErrCertNumber.Visible = p_blnDuplicateFlag

    End Sub

    Public Sub SetupExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IDetachOrMoveCertificationView.SetupExtensionErrMsg

        lblErrExtension.Visible = p_blnDuplicateFlag

    End Sub

    Protected Sub gvCertMaterials_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCertMaterials.PageIndexChanging

        gvCertMaterials.PageIndex = e.NewPageIndex
        gvCertMaterials.DataSource = Me.CertificateMaterials
        gvCertMaterials.DataBind()

    End Sub

    Protected Sub Click_btnList(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnList.Click

        ClearMessages()

        ClearGrid()

        If CheckForErrorMessages() Then
            RaiseEvent ShowCertificateMaterials()
        End If

    End Sub

    Protected Sub Click_btnDetach(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ClearMessages()

        GetSkuIdAndCertificateId(sender)

        Me.DetachAlertPopUp.Show()

    End Sub

    Protected Sub Click_DetachConfirm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetachOk.Click

        RaiseEvent Detach()
        Me.DetachAlertPopUp.Dispose()
        If ErrorText = String.Empty Then
            RaiseEvent ShowCertificateMaterials()
        End If

    End Sub

    Protected Sub Click_DetachCancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetachCancel.Click

        Me.DetachAlertPopUp.Dispose()

    End Sub

    Protected Sub Click_btnMove(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ClearMessages()

        ClearMovePopupControls()

        GetSkuIdAndCertificateId(sender)

        'Show extension based on certification type
        If CertificationName = "ECE3054" Or CertificationName = "ECE117" Then
            ShowNewExtension(True)
        Else
            ShowNewExtension(False)
        End If

        Me.MoveAlertPopUp.Show()

    End Sub

    Protected Sub Click_MoveConfirm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveConfirm.Click

        If CheckForNewExtErrorMessages() Then
            RaiseEvent Move()
            Me.MoveAlertPopUp.Dispose()
            If ErrorText = String.Empty Then
                RaiseEvent ShowCertificateMaterials()
            End If
        End If

    End Sub

    Protected Sub Click_MoveCancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveCancel.Click

        Me.MoveAlertPopUp.Dispose()

    End Sub

    Private Function CheckForErrorMessages() As Boolean

        If lblErrCertNumber.Visible Or lblErrExtension.Visible Then
            Return False
        End If
        Return True

    End Function

    Private Function CheckForNewExtErrorMessages() As Boolean

        If lblErrNewCertNumber.Visible Or lblErrNewExtension.Visible Then
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' Get SkuId and CertificateId
    ''' </summary>
    ''' <param name="sender">Sender Object</param>
    ''' <returns>DataItem Index as Integer</returns>
    ''' <remarks></remarks>
    Private Function GetSkuIdAndCertificateId(ByVal sender As System.Object) As Integer

        Dim obj As DataControlFieldCell = CType(sender.Parent, DataControlFieldCell)
        Dim obj2 As GridViewRow = obj.Parent
        Dim pos = obj2.DataItemIndex

        SkuId = CInt(Me.CertificateMaterials.Rows(pos)("SKUID"))
        CertificateId = CInt(Me.CertificateMaterials.Rows(pos)("CERTIFICATEID"))

    End Function

    ''' <summary>
    ''' Show or hide extension controls
    ''' </summary>
    ''' <param name="p_blnShow">Boolean Value(True/False)</param>
    ''' <remarks></remarks>
    Private Sub ShowExtension(ByVal p_blnShow As Boolean)

        lblExtension.Visible = p_blnShow
        txtExtension.Visible = p_blnShow

    End Sub

    ''' <summary>
    ''' Show or hide new extension controls
    ''' </summary>
    ''' <param name="p_blnShow">Boolean Value(True/False)</param>
    ''' <remarks></remarks>
    Private Sub ShowNewExtension(ByVal p_blnShow As Boolean)

        lblNewExtension.Visible = p_blnShow
        txtNewExtension.Visible = p_blnShow

    End Sub

    ''' <summary>
    ''' Clear grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearGrid()

        gvCertMaterials.PageIndex = 0
        gvCertMaterials.DataSource = Nothing
        gvCertMaterials.DataBind()

    End Sub

    ''' <summary>
    ''' Clear move popup controls
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearMovePopupControls()

        NewCertificateNumber = String.Empty
        NewExtension = String.Empty

    End Sub

    ''' <summary>
    ''' Clear success and error messages
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearMessages()

        SuccessText = "&nbsp;"
        ErrorText = String.Empty

    End Sub

#End Region

End Class