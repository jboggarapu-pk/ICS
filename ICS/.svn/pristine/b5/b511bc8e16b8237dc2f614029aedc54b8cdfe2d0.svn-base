Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' Rename Certification form for all types
Partial Public Class RenameCertificationUC
    Inherits BaseUserControl
    Implements IRenameCertificationView

#Region "Members"

    Public Event CheckForCertifiedMaterials As CustomEvents.PlainEventHandler Implements IRenameCertificationView.CheckForCertifiedMaterials
    Public Event Save As CustomEvents.PlainEventHandler Implements IRenameCertificationView.Save
    Public Event ReloadViewData() Implements Presenter.IRenameCertificationView.ReloadViewData

    Private m_presenter As RenameCertificationPresenter

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New RenameCertificationPresenter(Me)

    End Sub

#End Region

#Region "Properties"

    Public Property SuccessText() As String Implements Presenter.IRenameCertificationView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    Public Property ErrorText() As String Implements Presenter.IRenameCertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property OldCertificateNumber() As String Implements Presenter.IRenameCertificationView.OldCertificateNumber
        Get
            Return txtOldCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtOldCertNumber.Text = value
        End Set
    End Property

    Public Property NewCertificateNumber() As String Implements Presenter.IRenameCertificationView.NewCertificateNumber
        Get
            Return txtNewCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtNewCertNumber.Text = value
        End Set
    End Property

    Public Property OldExtension() As String Implements Presenter.IRenameCertificationView.OldExtension
        Get
            Return txtOldExtension.Text
        End Get
        Set(ByVal value As String)
            txtOldExtension.Text = value
        End Set
    End Property

    Public Property NewExtension() As String Implements Presenter.IRenameCertificationView.NewExtension
        Get
            Return txtNewExtension.Text
        End Get
        Set(ByVal value As String)
            txtNewExtension.Text = value
        End Set
    End Property

    Public Property CertificationName() As String Implements Presenter.IRenameCertificationView.CertificationName
        Get
            Return Session("AddCertName")
        End Get
        Set(ByVal value As String)
            Session("AddCertName") = value
        End Set
    End Property

    Public Property AddCertificationTitle() As String Implements Presenter.IRenameCertificationView.AddCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    Public Property OldCertNumErrMsgFlag() As Boolean Implements Presenter.IRenameCertificationView.OldCertNumErrMsgFlag
        Get
            Return lblErrOldCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrOldCertNumber.Visible = value
        End Set
    End Property

    Public Property NewCertNumErrMsgFlag() As Boolean Implements Presenter.IRenameCertificationView.NewCertNumErrMsgFlag
        Get
            Return lblErrNewCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrNewCertNumber.Visible = value
        End Set
    End Property

    Public Property OldExtensionErrMsgFlag() As Boolean Implements Presenter.IRenameCertificationView.OldExtensionErrMsgFlag
        Get
            Return lblErrOldExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrOldExtension.Visible = value
        End Set
    End Property

    Public Property NewExtensionErrMsgFlag() As Boolean Implements Presenter.IRenameCertificationView.NewExtensionErrMsgFlag
        Get
            Return lblErrNewExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrNewExtension.Visible = value
        End Set
    End Property

    Public Property WarningMessage() As String Implements Presenter.IRenameCertificationView.WarningMessage
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
    ''' <param name="p_blnRename"></param>
    ''' <remarks></remarks>
    Public Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnRename As Boolean) Implements Presenter.IRenameCertificationView.SetupViewData

        If p_blnRename Then
            CertificationName = p_strCertificationName
            RaiseEvent ReloadViewData()
        End If

    End Sub

    ''' <summary>
    ''' Set up the add certification view based on the certification type
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetupAddCertificationView() Implements IRenameCertificationView.SetupRenameCertificationView

        'Show extension based on certification type
        If CertificationName = "ECE3054" Or CertificationName = "ECE117" Then
            ShowExtension(True)
        Else
            ShowExtension(False)
        End If

    End Sub

    Public Sub SetupOldCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IRenameCertificationView.SetupOldCertNumErrMsg

        lblErrOldCertNumber.Visible = p_blnDuplicateFlag

    End Sub

    Public Sub SetupNewCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IRenameCertificationView.SetupNewCertNumErrMsg

        lblErrNewCertNumber.Visible = p_blnDuplicateFlag

    End Sub

    Public Sub SetupOldCExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IRenameCertificationView.SetupOldExtensionErrMsg

        lblErrOldExtension.Visible = p_blnDuplicateFlag

    End Sub

    Public Sub SetupNewExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IRenameCertificationView.SetupNewExtensionErrMsg

        lblErrNewExtension.Visible = p_blnDuplicateFlag

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

    Protected Sub Click_btnCancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        If OldCertificateNumber IsNot String.Empty Or NewCertificateNumber IsNot Nothing Then
            Me.CancelAlertPopUp.Show()
        Else
            'jeseitz 6/15/2016 - added 0 as second parameter - not used in this case.
            CType(Me.Page, CertificationSearchEx).ActivateCertificateControl(String.Empty, 0)
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

    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.CancelAlertPopUp.Dispose()
        'jeseitz 6/15/2016 - added 0 as second parameter - not used in this case.
        CType(Me.Page, CertificationSearchEx).ActivateCertificateControl(String.Empty, 0)

    End Sub

    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.CancelAlertPopUp.Dispose()

    End Sub

    Private Function CheckForErrorMessages() As Boolean

        If lblErrOldCertNumber.Visible Or lblErrNewCertNumber.Visible Or _
           lblErrOldExtension.Visible Or lblErrNewExtension.Visible Then
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

        lblOldExtension.Visible = p_blnShow
        txtOldExtension.Visible = p_blnShow
        lblNewExtension.Visible = p_blnShow
        txtNewExtension.Visible = p_blnShow

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