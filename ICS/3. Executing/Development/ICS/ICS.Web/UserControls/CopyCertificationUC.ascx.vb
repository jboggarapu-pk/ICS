Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Partial Public Class CopyCertificationUC
    Inherits BaseUserControl
    Implements ICopyCertification

#Region "Members"
    ''' <summary>
    ''' Copy Certification presenter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_presenter As CopyCertificationPresenter
    ''' <summary>
    ''' Reload view Data event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ReloadViewData() Implements Presenter.ICopyCertification.ReloadViewData
    ''' <summary>
    ''' Save Event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Save() Implements Presenter.ICopyCertification.Save

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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()
        m_presenter = New CopyCertificationPresenter(Me)
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets Error Text value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements ICopyCertification.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Copy Certification Title value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CopyCertificationTitle() As String Implements Presenter.ICopyCertification.CopyCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Material Numbererror message flag value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Boolean</returns>
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property MateNumErrMsgFlag() As Boolean Implements Presenter.ICopyCertification.MateNumErrMsgFlag
        Get
            Return CBool(lblErrorText.Text)
        End Get
        Set(ByVal value As Boolean)
            lblErrorText.Text = CStr(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Material Number value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Material Number</returns>
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property MaterialNumber() As String Implements Presenter.ICopyCertification.MaterialNumber
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
    ''' <value></value>
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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
    ''' <param name="p_blnCopyCertView">Copy Certificate View</param>
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupViewData(ByVal p_blnCopyCertView As Boolean) Implements Presenter.ICopyCertification.SetupViewData
        Try
            If p_blnCopyCertView Then
                RaiseEvent ReloadViewData()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup Materail Number Error Message.
    ''' </summary>
    ''' <param name="p_blnExistsFlag">Exists flag</param>
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupMateNumErrMsg(ByVal p_blnExistsFlag As Boolean) Implements Presenter.ICopyCertification.SetupMateNumErrMsg
        Try
            lblErrMateNumber.Visible = Not p_blnExistsFlag
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Ok button click.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SuccessText = "&nbsp;"
        ErrorText = String.Empty
        Try
            RaiseEvent Save()

            Me.ConfirmPopUp.Dispose()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Cancel button Click .
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Me.pnlConfirm.Dispose()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Button  Copy Material Command.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnCopyMaterial_Command(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnCopyMaterial.Command
        Try
            Dim strErrorMessage As String = m_presenter.CheckMaterialNumberExists()

            If strErrorMessage = String.Empty Then
                Me.ConfirmPopUp.Show()
            Else
                ErrorText = strErrorMessage
            End If
        Catch
            Throw
        End Try
    End Sub
#End Region

End Class