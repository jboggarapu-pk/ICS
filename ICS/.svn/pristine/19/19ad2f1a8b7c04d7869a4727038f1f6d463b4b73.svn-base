Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Partial Public Class CopyCertificationUC
    Inherits BaseUserControl
    Implements ICopyCertification

#Region "Members"

    Private m_presenter As CopyCertificationPresenter
    Public Event ReloadViewData() Implements Presenter.ICopyCertification.ReloadViewData
    Public Event Save() Implements Presenter.ICopyCertification.Save

#End Region

#Region "Constructors"

    Public Sub New()
        m_presenter = New CopyCertificationPresenter(Me)
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Error Text
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property ErrorText() As String Implements ICopyCertification.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Copy Certification Title
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property CopyCertificationTitle() As String Implements Presenter.ICopyCertification.CopyCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Material Number.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Property MateNumErrMsgFlag() As Boolean Implements Presenter.ICopyCertification.MateNumErrMsgFlag
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As Boolean)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Material Number.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Material Number</returns>
    ''' <remarks></remarks>
    Public Property MaterialNumber() As String Implements Presenter.ICopyCertification.MaterialNumber
        Get
            Return txtMateNumber.Text
        End Get
        Set(ByVal value As String)
            txtMateNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Success Text.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SuccessText() As String Implements Presenter.ICopyCertification.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Setup copy certificate.
    ''' </summary>
    ''' <param name="p_blnCopyCertView">Boolean</param>
    ''' <remarks></remarks>
    Public Sub SetupViewData(ByVal p_blnCopyCertView As Boolean) Implements Presenter.ICopyCertification.SetupViewData
        If p_blnCopyCertView Then
            RaiseEvent ReloadViewData()
        End If
    End Sub

    ''' <summary>
    ''' Setup Materail Number Error Message.
    ''' </summary>
    ''' <param name="p_blnExistsFlag">Boolean</param>
    ''' <remarks></remarks>
    Public Sub SetupMateNumErrMsg(ByVal p_blnExistsFlag As Boolean) Implements Presenter.ICopyCertification.SetupMateNumErrMsg
        lblErrMateNumber.Visible = Not p_blnExistsFlag
    End Sub

    ''' <summary>
    ''' Ok button click.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SuccessText = "&nbsp;"
        ErrorText = String.Empty

        RaiseEvent Save()

        Me.ConfirmPopUp.Dispose()
    End Sub

    ''' <summary>
    ''' Cancel bbutton Click .
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.pnlConfirm.Dispose()
    End Sub

    ''' <summary>
    ''' Button  Copy Material Command.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnCopyMaterial_Command(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnCopyMaterial.Command
        Dim strErrorMessage As String = m_presenter.CheckMaterialNumberExists()

        If strErrorMessage = String.Empty Then
            Me.ConfirmPopUp.Show()
        Else
            ErrorText = strErrorMessage
        End If
    End Sub

#End Region

End Class