Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' Delete Certification form for all types
Partial Public Class DeleteCertificationUC
    Inherits BaseUserControl
    Implements IDeleteCertificationView

#Region "Members"

    Public Event CheckForCertifiedMaterials As CustomEvents.PlainEventHandler Implements IDeleteCertificationView.CheckForCertifiedMaterials
    Public Event Save As CustomEvents.PlainEventHandler Implements IDeleteCertificationView.Save
    Public Event ReloadViewData() Implements Presenter.IDeleteCertificationView.ReloadViewData

    Private m_presenter As DeleteCertificationPresenter

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New DeleteCertificationPresenter(Me)

    End Sub

#End Region

#Region "Properties"

    Public Property SuccessText() As String Implements Presenter.IDeleteCertificationView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    Public Property ErrorText() As String Implements Presenter.IDeleteCertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property CertificateNumber() As String Implements Presenter.IDeleteCertificationView.CertificateNumber
        Get
            Return txtCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtCertNumber.Text = value
        End Set
    End Property

    Public Property Extension() As String Implements Presenter.IDeleteCertificationView.Extension
        Get
            Return txtExtension.Text
        End Get
        Set(ByVal value As String)
            txtExtension.Text = value
        End Set
    End Property

    Public Property CertificationName() As String Implements Presenter.IDeleteCertificationView.CertificationName
        Get
            Return Session("AddCertName")
        End Get
        Set(ByVal value As String)
            Session("AddCertName") = value
        End Set
    End Property

    Public Property AddCertificationTitle() As String Implements Presenter.IDeleteCertificationView.AddCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    Public Property CertNumErrMsgFlag() As Boolean Implements Presenter.IDeleteCertificationView.CertNumErrMsgFlag
        Get
            Return lblErrCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrCertNumber.Visible = value
        End Set
    End Property

    Public Property ExtensionErrMsgFlag() As Boolean Implements Presenter.IDeleteCertificationView.ExtensionErrMsgFlag
        Get
            Return lblErrExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrExtension.Visible = value
        End Set
    End Property

    Public Property WarningMessage() As String Implements Presenter.IDeleteCertificationView.WarningMessage
        Get
            Return lblWarningMessage.Text
        End Get
        Set(ByVal value As String)
            lblWarningMessage.Text = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Setup view data if certificate is new
    ''' </summary>
    ''' <param name="p_strCertificationName"></param>
    ''' <param name="p_blnDelete"></param>
    ''' <remarks></remarks>
    Public Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnDelete As Boolean) Implements Presenter.IDeleteCertificationView.SetupViewData

        If p_blnDelete Then
            CertificationName = p_strCertificationName
            RaiseEvent ReloadViewData()
        End If

    End Sub

    ''' <summary>
    ''' Set up the add certification view based on the certification type
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetupAddCertificationView() Implements IDeleteCertificationView.SetupDeleteCertificationView

        'Show extension based on certification type
        If CertificationName = "ECE3054" Or CertificationName = "ECE117" Then
            ShowExtension(True)
        Else
            ShowExtension(False)
        End If

    End Sub

    Public Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IDeleteCertificationView.SetupCertNumErrMsg

        lblErrCertNumber.Visible = p_blnDuplicateFlag

    End Sub

    Public Sub SetupExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IDeleteCertificationView.SetupExtensionErrMsg

        lblErrExtension.Visible = p_blnDuplicateFlag

    End Sub

    Protected Sub Click_btnSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

       ClearMessages()

        If CheckForErrorMessages() Then
            RaiseEvent CheckForCertifiedMaterials()
            If Not String.IsNullOrEmpty(WarningMessage) Then
                Me.ConfirmPopUp.Show()
            End If
        End If

    End Sub

    Protected Sub Click_SaveConfirm(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ClearMessages()
        RaiseEvent Save()
        Me.ConfirmPopUp.Dispose()

    End Sub

    Protected Sub Click_SaveCancel(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.ConfirmPopUp.Dispose()

    End Sub

    Protected Sub Click_btnCancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        If CertificateNumber IsNot String.Empty Then
            Me.CancelAlertPopUp.Show()
        Else
            'jeseitz 6/15/2016 - added 0 as second parameter - not used in this case.
            CType(Me.Page, CertificationSearchEx).ActivateCertificateControl(String.Empty, 0)
        End If

    End Sub

    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.CancelAlertPopUp.Dispose()
        CType(Me.Page, CertificationSearchEx).ActivateCertificateControl(String.Empty, 0)

    End Sub

    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.CancelAlertPopUp.Dispose()

    End Sub

    Private Function CheckForErrorMessages() As Boolean

        If lblErrCertNumber.Visible Or lblErrExtension.Visible Then
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' Show or hide extension controls
    ''' </summary>
    ''' <param name="p_blnShow"></param>
    ''' <remarks></remarks>
    Private Sub ShowExtension(ByVal p_blnShow As Boolean)

        lblExtension.Visible = p_blnShow
        txtExtension.Visible = p_blnShow

    End Sub

    ''' <summary>
    ''' Clear all messages
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearMessages()

        SuccessText = "&nbsp;"
        ErrorText = String.Empty
        WarningMessage = String.Empty

    End Sub

#End Region

End Class