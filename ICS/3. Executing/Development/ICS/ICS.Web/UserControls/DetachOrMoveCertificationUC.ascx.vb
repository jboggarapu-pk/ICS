Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' DetachOrMove Certification form for all types
Partial Public Class DetachOrMoveCertificationUC
    Inherits BaseUserControl
    Implements IDetachOrMoveCertificationView

#Region "Members"
    ''' <summary>
    ''' Show Certificate Materials event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ShowCertificateMaterials As CustomEvents.PlainEventHandler Implements IDetachOrMoveCertificationView.ShowCertificateMaterials
    ''' <summary>
    ''' Detach event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Detach As CustomEvents.PlainEventHandler Implements IDetachOrMoveCertificationView.Detach
    ''' <summary>
    ''' Move event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Move As CustomEvents.PlainEventHandler Implements IDetachOrMoveCertificationView.Move
    ''' <summary>
    ''' reload view data event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ReloadViewData() Implements Presenter.IDetachOrMoveCertificationView.ReloadViewData
    ''' <summary>
    ''' Detach Certification presenter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_presenter As DetachOrMoveCertificationPresenter
    ''' <summary>
    '''  Constant to hold ECE3054 text
    ''' </summary>
    Private Const ECE3054Text As String = "ECE3054"
    ''' <summary>
    '''  Constant to hold ECE117 text
    ''' </summary>
    Private Const ECE117Text As String = "ECE117"

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

        m_presenter = New DetachOrMoveCertificationPresenter(Me)

    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets or sets Success Text value.
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
    Public Property SuccessText() As String Implements Presenter.IDetachOrMoveCertificationView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Error Text value.
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
    Public Property ErrorText() As String Implements Presenter.IDetachOrMoveCertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Number value.
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
    Public Property CertificateNumber() As String Implements Presenter.IDetachOrMoveCertificationView.CertificateNumber
        Get
            Return txtCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtCertNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Extension value.
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
    Public Property Extension() As String Implements Presenter.IDetachOrMoveCertificationView.Extension
        Get
            Return txtExtension.Text
        End Get
        Set(ByVal value As String)
            txtExtension.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets SKU Id value.
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
    Public Property SkuId() As Integer Implements Presenter.IDetachOrMoveCertificationView.SkuId
        Get
            Return CInt(hidSkuId.Value)
        End Get
        Set(ByVal value As Integer)
            hidSkuId.Value = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificte Id value.
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
    Public Property CertificateId() As Integer Implements Presenter.IDetachOrMoveCertificationView.CertificateId
        Get
            Return CInt(hidCertificateId.Value)
        End Get
        Set(ByVal value As Integer)
            hidCertificateId.Value = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets New Certificate Number value.
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
    Public Property NewCertificateNumber() As String Implements Presenter.IDetachOrMoveCertificationView.NewCertificateNumber
        Get
            Return txtNewCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtNewCertNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets New Extension value.
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
    Public Property NewExtension() As String Implements Presenter.IDetachOrMoveCertificationView.NewExtension
        Get
            Return txtNewExtension.Text
        End Get
        Set(ByVal value As String)
            txtNewExtension.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certification Name value.
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
    Public Property CertificationName() As String Implements Presenter.IDetachOrMoveCertificationView.CertificationName
        Get
            Return CStr(Session("AddCertName"))
        End Get
        Set(ByVal value As String)
            Session("AddCertName") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Add Certification Title value.
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
    Public Property AddCertificationTitle() As String Implements Presenter.IDetachOrMoveCertificationView.AddCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Number Error Message Flag value.
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
    Public Property CertNumErrMsgFlag() As Boolean Implements Presenter.IDetachOrMoveCertificationView.CertNumErrMsgFlag
        Get
            Return lblErrCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrCertNumber.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Extension Error Message Flag value.
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
    Public Property ExtensionErrMsgFlag() As Boolean Implements Presenter.IDetachOrMoveCertificationView.ExtensionErrMsgFlag
        Get
            Return lblErrExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrExtension.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets New Certificate Number Error Message Flag.
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
    Public Property NewCertNumErrMsgFlag() As Boolean Implements Presenter.IDetachOrMoveCertificationView.NewCertNumErrMsgFlag
        Get
            Return lblErrNewCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrNewCertNumber.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets New Extension Error Message Flag value.
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
    Public Property NewExtensionErrMsgFlag() As Boolean Implements Presenter.IDetachOrMoveCertificationView.NewExtensionErrMsgFlag
        Get
            Return lblErrNewExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrNewExtension.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Materials value.
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
    Public Property CertificateMaterials() As DataTable Implements Presenter.IDetachOrMoveCertificationView.CertificateMaterials
        Get
            Return CType(Session("CertMaterials"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("CertMaterials") = value
            gvCertMaterials.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Setup view data if certificate is new.
    ''' </summary>
    ''' <param name="p_strCertificationName">Certification Name</param>
    ''' <param name="p_blnDetachOrMove">Detach Or Move</param>
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
    Public Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnDetachOrMove As Boolean) Implements Presenter.IDetachOrMoveCertificationView.SetupViewData
        Try
            If p_blnDetachOrMove Then
                CertificationName = p_strCertificationName
                RaiseEvent ReloadViewData()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Set up the add certification view based on the certification type.
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
    Public Sub SetupDetachOrMoveCertificationView() Implements Presenter.IDetachOrMoveCertificationView.SetupDetachOrMoveCertificationView
        Try
            'Show extension based on certification type
            If CertificationName = ECE3054Text Or CertificationName = ECE117Text Then
                ShowExtension(True)
            Else
                ShowExtension(False)
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IDetachOrMoveCertificationView.SetupCertNumErrMsg
        Try
            lblErrCertNumber.Visible = p_blnDuplicateFlag
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup Extension Error Messge.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IDetachOrMoveCertificationView.SetupExtensionErrMsg
        Try
            lblErrExtension.Visible = p_blnDuplicateFlag
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Certificate Materials Page Index Changing.
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
    Protected Sub gvCertMaterials_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCertMaterials.PageIndexChanging
        Try
            gvCertMaterials.PageIndex = e.NewPageIndex
            gvCertMaterials.DataSource = Me.CertificateMaterials
            gvCertMaterials.DataBind()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' List Button Click.
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
    Protected Sub Click_btnList(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnList.Click
        Try
            ClearMessages()
            ClearGrid()
            If CheckForErrorMessages() Then
                RaiseEvent ShowCertificateMaterials()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Detatch Button Click.
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
    Protected Sub Click_btnDetach(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ClearMessages()
            GetSkuIdAndCertificateId(sender)
            Me.DetachAlertPopUp.Show()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Detatch Confirm Button Click.
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
    Protected Sub Click_DetachConfirm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetachOk.Click
        Try
            RaiseEvent Detach()
            Me.DetachAlertPopUp.Dispose()
            If ErrorText = String.Empty Then
                RaiseEvent ShowCertificateMaterials()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Detatch Cancel Button Click.
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
    Protected Sub Click_DetachCancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetachCancel.Click
        Try
            Me.DetachAlertPopUp.Dispose()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Move Button Click.
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
    Protected Sub Click_btnMove(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ClearMessages()
            ClearMovePopupControls()
            GetSkuIdAndCertificateId(sender)
            'Show extension based on certification type
            If CertificationName = ECE3054Text Or CertificationName = ECE117Text Then
                ShowNewExtension(True)
            Else
                ShowNewExtension(False)
            End If

            Me.MoveAlertPopUp.Show()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Move Confirm Button Click.
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
    Protected Sub Click_MoveConfirm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveConfirm.Click
        Try
            If CheckForNewExtErrorMessages() Then
                RaiseEvent Move()
                Me.MoveAlertPopUp.Dispose()
                If ErrorText = String.Empty Then
                    RaiseEvent ShowCertificateMaterials()
                End If
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Move Cancel Button Click.
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
    Protected Sub Click_MoveCancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveCancel.Click
        Try
            Me.MoveAlertPopUp.Dispose()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Check for Error messages.
    ''' </summary>
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
    Private Function CheckForErrorMessages() As Boolean
        Try
            If lblErrCertNumber.Visible Or lblErrExtension.Visible Then
                Return False
            End If
            Return True
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Check for New Extension Error messages.
    ''' </summary>
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
    Private Function CheckForNewExtErrorMessages() As Boolean
        Try
            If lblErrNewCertNumber.Visible Or lblErrNewExtension.Visible Then
                Return False
            End If
            Return True
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Get SkuId and CertificateId.
    ''' </summary>
    ''' <param name="sender">Sender Object</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>DataItem Index as Integer</returns>
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
    ''' <para>Fixed ICS Issues</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function GetSkuIdAndCertificateId(ByVal sender As System.Object) As Integer
        Const SKUIdText As String = "SKUID"
        Const CertificateIdText As String = "CERTIFICATEID"
        Try
            Dim obj As DataControlFieldCell = CType(DirectCast(sender, System.Web.UI.WebControls.Button).Parent, DataControlFieldCell)
            Dim obj2 As GridViewRow = CType(obj.Parent, GridViewRow)
            Dim pos As Integer = obj2.DataItemIndex

            SkuId = CInt(Me.CertificateMaterials.Rows(CInt(pos))(SKUIdText))
            CertificateId = CInt(Me.CertificateMaterials.Rows(CInt(pos))(CertificateIdText))
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Show or hide extension controls.
    ''' </summary>
    ''' <param name="p_blnShow">Boolean Value(True/False)</param>
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
    Private Sub ShowExtension(ByVal p_blnShow As Boolean)
        Try
            lblExtension.Visible = p_blnShow
            txtExtension.Visible = p_blnShow
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Show or hide new extension controls.
    ''' </summary>
    ''' <param name="p_blnShow">Boolean Value(True/False)</param>
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
    Private Sub ShowNewExtension(ByVal p_blnShow As Boolean)
        Try
            lblNewExtension.Visible = p_blnShow
            txtNewExtension.Visible = p_blnShow
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Clear grid.
    ''' </summary>
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
    Private Sub ClearGrid()
        Try
            gvCertMaterials.PageIndex = 0
            gvCertMaterials.DataSource = Nothing
            gvCertMaterials.DataBind()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Clear move popup controls.
    ''' </summary>
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
    Private Sub ClearMovePopupControls()
        Try
            NewCertificateNumber = String.Empty
            NewExtension = String.Empty
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Clear success and error messages.
    ''' </summary>
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
    Private Sub ClearMessages()
        Try
            SuccessText = "&nbsp;"
            ErrorText = String.Empty
        Catch
            Throw
        End Try
    End Sub
#End Region

End Class