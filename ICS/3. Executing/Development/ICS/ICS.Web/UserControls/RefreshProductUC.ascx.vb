Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Partial Public Class RefreshProductUC
    Inherits BaseUserControl
    Implements IRefresbProductView
#Region "Members"
    ''' <summary>
    ''' variable to hold presenter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_presenter As RefreshProductPresenter = Nothing

    ''' <summary>
    ''' variable to hold Reload View Data.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ReloadViewData() Implements Presenter.IRefresbProductView.ReloadViewData

    ''' <summary>
    ''' variable to hold Save.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Save() Implements Presenter.IRefresbProductView.Save

#End Region

#Region "Constructors"
    ''' <summary>
    ''' Constructor for this class.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub New()
        m_presenter = New RefreshProductPresenter(Me)
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets Error text.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Error Text.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements IRefresbProductView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Refresh product Title value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Refresh Product Title Value.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property RefreshProductTitle() As String Implements Presenter.IRefresbProductView.RefreshProductTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Material Number.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Material Number.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>     
    Public Property MaterialNumber() As String Implements Presenter.IRefresbProductView.MaterialNumber
        Get
            Return txtMateNumber.Text
        End Get
        Set(ByVal value As String)
            txtMateNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Success Text value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Success Text.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
    ''' Method to Setup Refresh Product.
    ''' </summary> 
    ''' <param name="p_blnCopyCertView">Boolean</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub SetupViewData(ByVal p_blnCopyCertView As Boolean) Implements Presenter.IRefresbProductView.SetupViewData
        Try
            If p_blnCopyCertView Then
                RaiseEvent ReloadViewData()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Show/hide Material Number error message.
    ''' </summary> 
    ''' <param name="p_blnExistsFlag">Boolean</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub SetupMaterialErrMsg(ByVal p_blnExistsFlag As Boolean) Implements Presenter.IRefresbProductView.SetupMaterialErrMsg
        Try
            lblErrMateNumber.Visible = Not p_blnExistsFlag
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Confirm Button Click .
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClickConfirm.Click
        SuccessText = String.Empty
        ErrorText = String.Empty

        RaiseEvent Save()

        Me.ConfirmPopUp.Dispose()
    End Sub

    ''' <summary>
    ''' Cancel button Click.
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks> 
    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brnClickCancel.Click
        Try
            Me.pnlConfirm.Dispose()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' On click refreshes Product data.
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnRefreshMaterial_Command(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnRefreshMaterial.Command
        Try
            Dim strErrorMessage As String = m_presenter.IsMaterialExists()

            If strErrorMessage = String.Empty Then
                Me.ConfirmPopUp.Show()
            Else
                ErrorText = strErrorMessage
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region


End Class