Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Partial Public Class RefreshProductUC
    Inherits BaseUserControl
    Implements IRefresbProductView
#Region "Members"

    Private m_presenter As RefreshProductPresenter = Nothing
    Public Event ReloadViewData() Implements Presenter.IRefresbProductView.ReloadViewData
    Public Event Save() Implements Presenter.IRefresbProductView.Save

#End Region

#Region "Constructors"

    Public Sub New()
        m_presenter = New RefreshProductPresenter(Me)
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Error Text
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property ErrorText() As String Implements IRefresbProductView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Refresh Product Title
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property RefreshProductTitle() As String Implements Presenter.IRefresbProductView.RefreshProductTitle
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
    ''' <returns>Material Number</returns>
    ''' <remarks></remarks>
    Public Property MaterialNumber() As String Implements Presenter.IRefresbProductView.MaterialNumber
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
    Public Property SuccessText() As String Implements Presenter.IRefresbProductView.SuccessText
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
    ''' Setup Refresh Product.
    ''' </summary>
    ''' <param name="p_blnCopyCertView">Boolean</param>
    ''' <remarks></remarks>
    Public Sub SetupViewData(ByVal p_blnCopyCertView As Boolean) Implements Presenter.IRefresbProductView.SetupViewData
        If p_blnCopyCertView Then
            RaiseEvent ReloadViewData()
        End If
    End Sub

    ''' <summary>
    ''' Show/hide Material Number error message.
    ''' </summary>
    ''' <param name="p_blnExistsFlag">Boolean</param>
    ''' <remarks></remarks>
    Public Sub SetupMaterialErrMsg(ByVal p_blnExistsFlag As Boolean) Implements Presenter.IRefresbProductView.SetupMaterialErrMsg
        lblErrMateNumber.Visible = Not p_blnExistsFlag
    End Sub

    ''' <summary>
    ''' Confirm button click.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClickConfirm.Click
        SuccessText = String.Empty
        ErrorText = String.Empty

        RaiseEvent Save()

        Me.ConfirmPopUp.Dispose()
    End Sub

    ''' <summary>
    ''' Cancel button Click.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brnClickCancel.Click
        Me.pnlConfirm.Dispose()
    End Sub

    ''' <summary>
    ''' On click refreshes Product data.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnRefreshMaterial_Command(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnRefreshMaterial.Command
        Dim strErrorMessage As String = m_presenter.IsMaterialExists()

        If strErrorMessage = String.Empty Then
            Me.ConfirmPopUp.Show()
        Else
            ErrorText = strErrorMessage
        End If
    End Sub

#End Region


End Class