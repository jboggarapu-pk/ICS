Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Partial Public Class AttachCertificationUC
    Inherits BaseUserControl
    Implements IAttachCertification

#Region "Members"

    ''' <summary>
    ''' Attach certification presenter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_presenter As AttachCertificationPresenter

    ''' <summary>
    ''' Reload view data Event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ReloadViewData() Implements Presenter.IAttachCertification.ReloadViewData

    ''' <summary>
    ''' Save Event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Save() Implements Presenter.IAttachCertification.Save

    ''' <summary>
    ''' Check Certification number exists event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event CheckCertificateNumberExists() Implements Presenter.IAttachCertification.CheckCertificateNumberExists
    ''' <summary>
    ''' SKU Id
    ''' </summary>
    ''' <remarks></remarks>
    Private p_skuid As Integer
    ''' <summary>
    ''' Certificate Type Id
    ''' </summary>
    ''' <remarks></remarks>
    Private p_certificateTypeId As Integer

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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()
        m_presenter = New AttachCertificationPresenter(Me)
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets Error Text value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements IAttachCertification.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Attach Certification Title value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Attach Certification Title</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property AttachCertificationTitle() As String Implements Presenter.IAttachCertification.AttachCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Material Number Error Message Flag value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>boolean</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property MateNumErrMsgFlag() As Boolean Implements Presenter.IAttachCertification.MateNumErrMsgFlag
        Get
            Return CBool(lblErrorText.Text)
        End Get
        Set(ByVal value As Boolean)
            lblErrorText.Text = CStr(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets SkuId value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Integer</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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
    ''' Gets or sets Success Text value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Success Text.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property SuccessText() As String Implements Presenter.IAttachCertification.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certification Type Id value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Certification Type Id.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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
    ''' Gets or sets Certification Number value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Certification Number.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    '''</remarks>
    Public Property CertificationNumber() As String Implements Presenter.IAttachCertification.CertificateNumber
        Get
            Return TxtCertificateNo.Text
        End Get
        Set(ByVal value As String)
            TxtCertificateNo.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Extension Number value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Extension</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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
    ''' <param name="p_blnAttachCertView">AttachCertificateView</param>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupViewData(ByVal p_blnAttachCertView As Boolean) Implements Presenter.IAttachCertification.SetupViewData
        Try
            If p_blnAttachCertView Then
                RaiseEvent ReloadViewData()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Confirm Button Click .
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <para>10/28/2019</para>
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
    ''' Cancel Button Click.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            ConfirmPopUp.Dispose()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Attach Certification Button Command.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnAttachCertification_Command(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnAttachCertification.Command
        Dim strErrorMessage As String = m_presenter.CheckCertificateNumberExists()
        Try
            If strErrorMessage = String.Empty Then
                Me.ConfirmPopUp.Show()
            Else
                ErrorText = strErrorMessage
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup Certificate Number Error Message.
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate Flag</param>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IAttachCertification.SetupCertNumErrMsg
        Try
            lblErrCertificateNo.Visible = Not p_blnDuplicateFlag
        Catch
            Throw
        End Try
    End Sub

#End Region

End Class