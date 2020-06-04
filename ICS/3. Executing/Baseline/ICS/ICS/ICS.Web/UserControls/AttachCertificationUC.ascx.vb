Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Partial Public Class AttachCertificationUC
    Inherits BaseUserControl
    Implements IAttachCertification

#Region "Members"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_presenter As AttachCertificationPresenter

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ReloadViewData() Implements Presenter.IAttachCertification.ReloadViewData

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Save() Implements Presenter.IAttachCertification.Save

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Event CheckCertificateNumberExists() Implements Presenter.IAttachCertification.CheckCertificateNumberExists

    Private p_skuid As Integer
    Private p_certificateTypeId As Integer

#End Region

#Region "Constructors"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        m_presenter = New AttachCertificationPresenter(Me)
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Error Text.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrorText() As String Implements IAttachCertification.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Attach Certification Title.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Attach Certification Title</returns>
    ''' <remarks></remarks>
    Public Property AttachCertificationTitle() As String Implements Presenter.IAttachCertification.AttachCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Material Number Error Message Flag.
    ''' </summary>
    ''' <value></value>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    Public Property MateNumErrMsgFlag() As Boolean Implements Presenter.IAttachCertification.MateNumErrMsgFlag
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As Boolean)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' SkuId
    ''' </summary>
    ''' <value></value>
    ''' <returns>Integer</returns>
    ''' <remarks></remarks>
    Public Property SkuId() As String Implements Presenter.IAttachCertification.SkuId
        Get
            If Integer.TryParse(txtSkuId.Text.Trim, p_skuid) Then
                Return p_skuid.ToString()
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            txtSkuId.Text = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Success Text.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Success Text.</returns>
    ''' <remarks></remarks>
    Public Property SuccessText() As String Implements Presenter.IAttachCertification.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Certification Type Id.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Certification Type Id.</returns>
    ''' <remarks></remarks>
    Public Property CertificationTypeId() As String Implements Presenter.IAttachCertification.CertificationTypeId
        Get
            If Integer.TryParse(txtCertificationTypeId.Text.Trim, p_certificateTypeId) Then
                Return p_certificateTypeId.ToString
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            txtCertificationTypeId.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Certification Number.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Certification Number.</returns>
    ''' <remarks></remarks>
    Public Property CertificationNumber() As String Implements Presenter.IAttachCertification.CertificateNumber
        Get
            Return TxtCertificateNo.Text
        End Get
        Set(ByVal value As String)
            TxtCertificateNo.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Extension Number.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Extension</returns>
    ''' <remarks></remarks>
    Public Property ExtensionEn() As String Implements Presenter.IAttachCertification.ExtensionEn
        Get
            Return txtExtensionNo.Text
        End Get
        Set(ByVal value As String)
            txtExtensionNo.Text = value
        End Set
    End Property

#End Region

#Region "Method"

    ''' <summary>
    ''' Setup Attach Certification.
    ''' </summary>
    ''' <param name="p_blnAttachCertView">Boolean</param>
    ''' <remarks></remarks>
    Public Sub SetupViewData(ByVal p_blnAttachCertView As Boolean) Implements Presenter.IAttachCertification.SetupViewData
        If p_blnAttachCertView Then
            RaiseEvent ReloadViewData()
        End If
    End Sub

    ''' <summary>
    ''' Confirm Button Click .
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
    ''' <remarks></remarks>
    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SuccessText = "&nbsp;"
        ErrorText = String.Empty

        RaiseEvent Save()

        Me.ConfirmPopUp.Dispose()
    End Sub

    ''' <summary>
    ''' Cancel Button Click.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ConfirmPopUp.Dispose()
    End Sub

    ''' <summary>
    ''' Attach Certification Button Command.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAttachCertification_Command(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnAttachCertification.Command
        Dim strErrorMessage As String = m_presenter.CheckCertificateNumberExists()

        If strErrorMessage = String.Empty Then
            Me.ConfirmPopUp.Show()
        Else
            ErrorText = strErrorMessage
        End If
    End Sub

    ''' <summary>
    ''' Setup Certificate Number Error Message.
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag"></param>
    ''' <remarks></remarks>
    Public Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IAttachCertification.SetupCertNumErrMsg
        lblErrCertificateNo.Visible = Not p_blnDuplicateFlag
    End Sub

#End Region

End Class