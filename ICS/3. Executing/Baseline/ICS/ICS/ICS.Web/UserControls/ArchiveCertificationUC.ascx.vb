Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Partial Public Class ArchiveCertificationUC
    Inherits BaseUserControl
    Implements IArchiveCertificationView
#Region "Constructors"

    Public Sub New()

        m_presenter = New ArchiveCertificationPresenter(Me)

    End Sub

#End Region

#Region "Members"

    Public Event CheckCertificateNumberExists As CustomEvents.PlainEventHandler Implements IArchiveCertificationView.CheckCertificateNumberExists
    Public Event ReloadViewData() Implements Presenter.IArchiveCertificationView.ReloadViewData
    Public Event Save As CustomEvents.PlainEventHandler Implements IArchiveCertificationView.Save
    Private m_presenter As ArchiveCertificationPresenter

#End Region

#Region "Properties"

    Public Property SuccessText() As String Implements Presenter.IArchiveCertificationView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    Public Property ErrorText() As String Implements Presenter.IArchiveCertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property ArchiveCertificationTitle() As String Implements Presenter.IArchiveCertificationView.ArchiveCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    Public Property CertificateNumber() As String Implements Presenter.IArchiveCertificationView.CertificateNumber
        Get
            Return txtCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtCertNumber.Text = value
        End Set
    End Property

    Public Property CertificationName() As String Implements Presenter.IArchiveCertificationView.CertificationName
        Get
            Return Session("AddCertName")
        End Get
        Set(ByVal value As String)
            Session("AddCertName") = value
        End Set
    End Property

    Public Property CertNumErrMsgFlag() As Boolean Implements Presenter.IArchiveCertificationView.CertNumErrMsgFlag
        Get
            Return lblErrCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrCertNumber.Visible = value
        End Set
    End Property


#End Region

#Region "Methods"
    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs)

        SuccessText = "&nbsp;"
        ErrorText = String.Empty

        RaiseEvent Save()

        Me.ConfirmPopUp.Dispose()

    End Sub

    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.ConfirmPopUp.Dispose()

    End Sub

    Private Sub btnArchiveCertification_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnArchiveCertification.Command

        Dim strErrorMessage As String = String.Empty

        strErrorMessage = m_presenter.CheckCertificateNumberExists()

        If strErrorMessage = String.Empty Then
            Me.ConfirmPopUp.Show()
        Else
            ErrorText = strErrorMessage
        End If

    End Sub

    Public Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IArchiveCertificationView.SetupCertNumErrMsg

        lblErrCertNumber.Visible = Not p_blnDuplicateFlag

    End Sub
#End Region
    
End Class