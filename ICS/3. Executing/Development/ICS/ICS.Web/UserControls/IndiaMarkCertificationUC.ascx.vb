Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities


' added this file 11/21/2011
Partial Public Class IndiaMarkCertificationUC
    Inherits BaseCertificationControl
    Implements IIndiaMarkCertificationView

    ' Changed the sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Members and Constants"
    ''' <summary>
    ''' variable to hold Save.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Save As CustomEvents.PlainEventHandler Implements ICertificationView.Save
    ''' <summary>
    ''' variable to hold Save Reasons.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event SaveReasons As CustomEvents.PlainEventHandler Implements ICertificationView.SaveReasons

    ''' <summary>
    ''' variable to hold Show Test Results.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ShowTestResults As CustomEvents.PlainEventHandler Implements ICertificationView.ShowTestResults

    ''' <summary>
    ''' variable to hold Get Test Results.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event GetTestResults As CustomEvents.PlainEventHandler Implements ICertificationView.GetTestResults

    ''' <summary>
    ''' variable to hold Get Blank Results.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event GetBlankResults As CustomEvents.PlainEventHandler Implements ICertificationView.GetBlankResults

    ''' <summary>
    ''' variable to hold Copy Similar Tire SKU Certificate.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event CopySimilarTireSKUCertificate As CustomEvents.PlainEventHandler Implements ICertificationView.CopySimilarTireSKUCertificate

    ''' <summary>
    ''' variable to hold Show Default Values.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ShowDefaultValues() Implements Presenter.ICertificationView.ShowDefaultValues

    ''' <summary>
    ''' variable to hold Presenter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_presenter As IndiaMarkcertificationPresenter

    ''' <summary>
    ''' variable to hold SKU ID.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_skuid As Integer

    ''' <summary>
    ''' variable to hold Enu Certificate Types.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_enuCertType As Integer = 7

    ''' <summary>
    ''' variable to hold Dataset of Manufacturing Locs.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_manufacturingLocs As DataSet

    ''' <summary>
    ''' variable to hold List of Audit Log.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_lstAuditLog As List(Of AuditLogEntry)

    ''' <summary>
    ''' variable to hold Datatable Similar Certificate.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dtbSimilarCertificate As DataTable

    ''' <summary>
    ''' variable to hold Test Datatable.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dtbTest As DataTable

    ''' <summary>
    ''' variable to hold Dataset of Reasons.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dstReasons As DataSet

    ''' <summary>
    ''' variable to hold Extension Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strExtensionNo As String
    ''' <summary>
    '''  Constant to hold Change Log Id text
    ''' </summary>
    Private Const ChangeLogIdText As String = "ChangeLogID"
    ''' <summary>
    '''  Constant to hold Changed By text
    ''' </summary>
    Private Const ChangedByText As String = "ChangedBy"
    ''' <summary>
    '''  Constant to hold Old Value text
    ''' </summary>
    Private Const OldValueText As String = "OldValue"
    ''' <summary>
    '''  Constant to hold New Value text
    ''' </summary>
    Private Const NewValueText As String = "NewValue"
    ''' <summary>
    '''  Constant to hold Approver text
    ''' </summary>
    Private Const ApproverText As String = "Approver"
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub New()
        m_presenter = New IndiaMarkcertificationPresenter(Me)
    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets or sets Certification Type value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Certification Type.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property CertificationType() As Integer Implements Presenter.ICertificationView.CertificationType
        Get
            Return m_enuCertType
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets Original Certificate value.
    ''' </summary>
    ''' <value>Certificate Object</value>
    ''' <returns>Original Certificate.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property OriginalCertificate() As DomainEntities.Certificate Implements Presenter.ICertificationView.OriginalCertificate
        Get
            Return CType(Session("OriginalCertificate"), Certificate)
        End Get
        Set(ByVal value As DomainEntities.Certificate)
            Session("OriginalCertificate") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Similar Tire Certificate value.
    ''' </summary>
    ''' <value>Certificate Object</value>
    ''' <returns>Similar Tire Certificate.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SimilarTireCertificate() As DomainEntities.Certificate Implements Presenter.ICertificationView.SimilarTireCertificate
        Get
            Return CType(Session("SimilarTireCertificate"), Certificate)
        End Get
        Set(ByVal value As DomainEntities.Certificate)
            Session("SimilarTireCertificate") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Similar Tire Message value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Similar Tire Message.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SimilarTireMessage() As String Implements Presenter.ICertificationView.SimilarTireMessage
        Get
            Return lblSimilarTireMessage.Text
        End Get
        Set(ByVal value As String)
            lblSimilarTireMessage.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets TR View value.
    ''' </summary>
    ''' <value>ITest Results View</value>
    ''' <returns>TR View.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property TRView() As Presenter.ITestResultsView Implements Presenter.IIndiaMarkCertificationView.TRView
        Get
            Return ucTestResults
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets Info Text value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Info Text.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property InfoText() As String Implements IIndiaMarkCertificationView.InfoText
        Get
            Return lblInfoText.Text
        End Get
        Set(ByVal value As String)
            lblInfoText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Error Text1 value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Error Text1.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ErrorText1() As String Implements Presenter.ICertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Material Number value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MaterialNumber() As String Implements ICertificationView.MaterialNumber
        Get
            Return txtMaterialNo.Text
        End Get
        Set(ByVal value As String)
            txtMaterialNo.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Product Data value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Product Data.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ProductData() As String Implements IIndiaMarkCertificationView.ProductData
        Get
            Return lblProductData.Text
        End Get
        Set(ByVal value As String)
            lblProductData.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Remove Material Number value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Remove Material Number.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property RemoveMatlNum() As Boolean Implements IIndiaMarkCertificationView.RemoveMatlNum
        Get
            Return cbxRemoveMatlNum.Checked
        End Get
        Set(ByVal value As Boolean)
            cbxRemoveMatlNum.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets SKU ID value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>SKU ID.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SKUID() As Integer Implements ICertificationView.SKUID
        Get
            Return m_skuid
        End Get
        Set(ByVal value As Integer)
            m_skuid = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Reason DS value.
    ''' </summary>
    ''' <value>Dataset</value>
    ''' <returns>Reason DS.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ReasonDS() As DataSet Implements ICertificationView.ReasonDS
        Get
            Return m_dstReasons
        End Get
        Set(ByVal value As DataSet)
            m_dstReasons = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certification Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Certification Number.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertificationNumber() As String Implements ICertificationView.CertificationNumber
        Get
            Return txtCertificationNo.Text
        End Get
        Set(ByVal value As String)
            txtCertificationNo.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Extension Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Extension Number.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ExtensionNo() As String Implements ICertificationView.ExtensionNo
        Get
            Return m_strExtensionNo
        End Get
        Set(ByVal value As String)
            m_strExtensionNo = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Number ID value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Certificate Number ID.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertificateNumberID() As Integer Implements Presenter.ICertificationView.CertificateNumberID
        Get
            Return CInt(Session(Me.GetType().Name & "CertificateNumberID"))
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "CertificateNumberID") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Date Submitted value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Certificate Date Submitted.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertDateSubmitted() As String Implements IIndiaMarkCertificationView.CertDateSubmitted
        Get
            Return txtCertDateSubmitted.Text
        End Get
        Set(ByVal value As String)
            txtCertDateSubmitted.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Date Approved value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Certificate Date Approved.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertDateApproved() As String Implements IIndiaMarkCertificationView.CertDateApproved
        Get
            Return txtCertDateApproved.Text
        End Get
        Set(ByVal value As String)
            txtCertDateApproved.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Date Submitted value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Date Submitted.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property DateSubmitted() As String Implements IIndiaMarkCertificationView.DateSubmitted
        Get
            Return txtDateSubmitted.Text
        End Get
        Set(ByVal value As String)
            txtDateSubmitted.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Date Approved value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Date Approved.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property DateApproved() As String Implements IIndiaMarkCertificationView.DateApproved
        Get
            Return txtDateApproved.Text
        End Get
        Set(ByVal value As String)
            txtDateApproved.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Active Status value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Active Status.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ActiveStatus() As Boolean Implements IIndiaMarkCertificationView.ActiveStatus
        Get
            Dim result As Boolean = rbtnLstActiveStatus.Items(0).Selected
            Return result
        End Get
        Set(ByVal value As Boolean)
            rbtnLstActiveStatus.Items(0).Selected = value
            rbtnLstActiveStatus.Items(1).Selected = Not value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Renewal Required value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Renewal required.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property RenewalRequired() As Boolean Implements IIndiaMarkCertificationView.RenewalRequired
        Get
            Dim result As Boolean = rbtnlstRenewalRequired.Items(0).Selected
            Return result
        End Get
        Set(ByVal value As Boolean)
            rbtnlstRenewalRequired.Items(0).Selected = value
            rbtnlstRenewalRequired.Items(1).Selected = Not value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets India Mark Job Report Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>India Mark Job Report Number.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property IndiaMark_JobReportNumber() As String Implements IIndiaMarkCertificationView.IndiaMark_JobReportNumber
        Get
            Return txtJobReportNo.Text
        End Get
        Set(ByVal value As String)
            txtJobReportNo.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets India Mark Product Location value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>India Mark Product Location.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property IndiaMark_ProductLocation() As String Implements IIndiaMarkCertificationView.IndiaMark_ProductLocation
        Get
            Return txtProductLocation.Text
        End Get
        Set(ByVal value As String)
            txtProductLocation.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Manufacturing Location Id value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Manufacturing Location Id.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ManufacturingLocationId() As String Implements ICertificationView.ManufacturingLocationId
        Get
            Return Nothing
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Audit Log List value.
    ''' </summary>
    ''' <value>list of Audit Log entry</value>
    ''' <returns>Audit Log List.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property AuditLogList() As List(Of AuditLogEntry) Implements ICertificationView.AuditLogList
        Get
            Return m_lstAuditLog
        End Get
        Set(ByVal value As List(Of AuditLogEntry))
            m_lstAuditLog = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Similar Certificate DS value.
    ''' </summary>
    ''' <value>Datatable</value>
    ''' <returns>Similar Certificate DS.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SimilarCertificateDS() As DataTable Implements ICertificationView.SimilarCertificateDS
        Get
            Return m_dtbSimilarCertificate
        End Get
        Set(ByVal value As DataTable)
            m_dtbSimilarCertificate = value
        End Set
    End Property

    'Added as per project 2706 technical specification
    ''' <summary>
    ''' Gets or sets Disc Date value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Disc Date.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property DiscDate() As String Implements IIndiaMarkCertificationView.DiscDate
        Get
            Return lblDiscDate.Text
        End Get
        Set(ByVal value As String)
            lblDiscDate.Text = value
        End Set
    End Property

    'added for request 203625 - jeseitz 10/29/2016
    ''' <summary>
    ''' Gets or sets Add Info value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Add Info.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property AddInfo() As String Implements IIndiaMarkCertificationView.AddInfo
        Get
            Return txtAddInfo.Text
        End Get
        Set(ByVal value As String)
            txtAddInfo.Text = value
        End Set

    End Property

    ''' <summary>
    ''' Gets or sets Date Of Expiry value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Date Of Expiry.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property DateOfExpiry() As String Implements IIndiaMarkCertificationView.DateOfExpiry
        Get
            Return txtDateExpiry.Text
        End Get
        Set(ByVal value As String)
            txtDateExpiry.Text = value
        End Set
    End Property

    'JBH_2.00 Project 5325 - Added Operation Approval Date
    ''' <summary>
    ''' Gets or sets Operation Approval Date value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Operation Approval Date.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property OperDateApproved() As String Implements IIndiaMarkCertificationView.OperDateApproved
        Get
            Return txtOperDateApproved.Text
        End Get
        Set(ByVal value As String)
            txtOperDateApproved.Text = value
        End Set
    End Property

    'JBH_2.00 Project 5325 - Added Mold Changed Flag
    ''' <summary>
    ''' Gets or sets Mold Change Required value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Mold Change Required.</returns>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MoldChgRequired() As Boolean Implements IIndiaMarkCertificationView.MoldChgRequired
        Get
            Return cbxMoldChgRequired.Checked
        End Get
        Set(ByVal value As Boolean)
            cbxMoldChgRequired.Checked = value
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Save Button Click .
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            RaiseEvent Save()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' get Test Results Button Click .
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnGetTestResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGettestResult.Click

        RaiseEvent GetTestResults()

    End Sub

    ''' <summary>
    ''' Method to Show Similar Tire Prompt.
    ''' </summary>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub ShowSimilarTirePrompt() Implements ICertificationView.ShowSimilarTirePrompt
        Const SkuIdText As String = "SKUID"
        Const MatlNumText As String = "MATL_NUM"

        Try
            If Me.SimilarCertificateDS.Rows.Count > 0 Then
                Dim Row As DataRow = Me.SimilarCertificateDS.Rows(0)
                Me.lblSimilarSKUID.Text = Row.Item(SkuIdText).ToString()
                Me.lblSimilarMaterial.Text = Row.Item(MatlNumText).ToString()
                Me.ddlSimilarCertificate.DataBind()
                Me.SimilarTirePopUp.Show()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Yes Button Click .
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_Yes(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim strCertificationNumber As String = Me.ddlSimilarCertificate.SelectedItem.Text
            Dim intCertificationTypeID As Integer = Me.CertificationType
            Dim intSKUID As Integer = CInt(Me.lblSimilarSKUID.Text)
            Dim strMaterialNum As String = Me.lblSimilarMaterial.Text
            Dim blnDone As Boolean = False

            If m_presenter.GetCertificate(strCertificationNumber, intCertificationTypeID, intSKUID, strMaterialNum) Then
                Me.SimilarTirePopUp.Dispose()

                RaiseEvent CopySimilarTireSKUCertificate()
                RaiseEvent ShowTestResults()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' No Click .
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_No(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.SimilarTirePopUp.Dispose()
            RaiseEvent ShowTestResults()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Cancel Reasons Click .
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_CancelReasons(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Dispose pop-up.  Do not save changes to certificate
            ChangedFieldsPopup.Dispose()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Save reasons Click .
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_SaveReasons(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Const AreaText As String = "Area"
        Const ChangeDateText As String = "Change Date"
        Const ChangeFieldText As String = "Changed Field"
        Const NoteText As String = "Note"
        Const TextNote As String = "txtNote"
        Const ReasonListText As String = "ddlReasonList"

        Try
            Dim ddlReasonList As DropDownList
            Dim txtNote As TextBox
            Dim lstAuditLog As New List(Of AuditLogEntry)

            For Each Row As GridViewRow In gvChangedFields.Rows
                Dim entAuditLogEntry As New AuditLogEntry

                For j As Integer = 0 To gvChangedFields.Columns.Count() - 1
                    Select Case gvChangedFields.Columns(j).HeaderText
                        Case AreaText
                            entAuditLogEntry.Area = Row.Cells(j).Text
                        Case ChangeDateText
                            entAuditLogEntry.ChangeDateTime = CDate(Row.Cells(j).Text)
                        Case ChangeFieldText
                            entAuditLogEntry.ChangedFieldElement = Row.Cells(j).Text
                        Case ChangeLogIdText
                            If Row.Cells(j).Text = String.Empty Then
                                entAuditLogEntry.ChangeLogID = 0
                            Else
                                entAuditLogEntry.ChangeLogID = CInt(Row.Cells(j).Text)
                            End If
                        Case ChangedByText
                            entAuditLogEntry.ChangedBy = Row.Cells(j).Text
                        Case OldValueText
                            entAuditLogEntry.OldValue = Row.Cells(j).Text
                        Case NewValueText
                            entAuditLogEntry.NewValue = Row.Cells(j).Text
                        Case ApproverText
                            entAuditLogEntry.Approver = Row.Cells(j).Text
                        Case NoteText
                            txtNote = CType(Row.Cells(1).FindControl(TextNote), TextBox)
                            entAuditLogEntry.Note = txtNote.Text
                    End Select
                Next
                ddlReasonList = CType(Row.Cells(0).FindControl(ReasonListText), DropDownList)
                entAuditLogEntry.ReasonID = CInt(ddlReasonList.SelectedValue())
                lstAuditLog.Add(entAuditLogEntry)
            Next

            AuditLogList = lstAuditLog

            'Dispose the Ajax pop-up
            ChangedFieldsPopup.Dispose()

            RaiseEvent SaveReasons()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Display Changes to Client.
    ''' </summary>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub DisplayChangesToClient() Implements ICertificationView.DisplayChangesToClient
        Try
            'Set the data source(s) for the grid
            gvChangedFields.DataSource = AuditLogList

            For i As Integer = 0 To gvChangedFields.Columns.Count() - 1
                gvChangedFields.Columns(i).Visible = True
            Next

            'Bind the data source to the grid view
            gvChangedFields.DataBind()

            For i As Integer = 0 To gvChangedFields.Columns.Count() - 1
                Select Case gvChangedFields.Columns(i).HeaderText
                    Case ChangeLogIdText, _
                        ChangedByText, _
                        OldValueText, _
                        NewValueText, _
                        ApproverText

                        gvChangedFields.Columns(i).Visible = False
                End Select
            Next

            'Display the ajax popup
            ChangedFieldsPopup.Show()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Setup Default Values View.
    ''' </summary>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub SetupDefaultValuesView() Implements Presenter.ICertificationView.SetupDefaultValuesView
        Const CertificationDefaultsText As String = "CertificationDefaults.aspx"
        Const CertificationTypeText As String = "?CertificationType="
        Const CertificateNumberText As String = "&CertificateNumberID="
        Try
            If Me.CertificateNumberID > 0 Then
                Dim strUrl As String = CertificationDefaultsText & CertificationTypeText & Me.CertificationType & CertificateNumberText & Me.CertificateNumberID.ToString()
                Response.Redirect(strUrl)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Default Values Click .
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub lbtnDefaultValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDefaultValues.Click
        Try
            RaiseEvent ShowDefaultValues()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Setup view control states according to specific context.
    ''' </summary>
    ''' <param name="p_enuContext">enu Context</param>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Sub SetupControlContextState(ByVal p_enuContext As Presenter.ICertificationView.Context) Implements Presenter.ICertificationView.SetupControlContextState
        Const WarningText As String = "Warning: unknown certificate status"
        Try
            ppvCertificationNUmber.Validate()

            Select Case p_enuContext
                Case ICertificationView.Context.NewCertificate
                    TRView.IsVisible = False
                    lbtnDefaultValues.Enabled = False
                    txtCertificationNo.ReadOnly = False

                    btnGettestResult.Enabled = True
                Case ICertificationView.Context.JustAddedCertificate
                    TRView.IsVisible = False
                    lbtnDefaultValues.Enabled = False
                    txtCertificationNo.ReadOnly = False

                    btnGettestResult.Enabled = True
                Case ICertificationView.Context.GotTestResults
                    TRView.IsVisible = True
                    lbtnDefaultValues.Enabled = False
                    txtCertificationNo.ReadOnly = False

                    btnGettestResult.Enabled = False
                Case ICertificationView.Context.ExistingCertificate
                    TRView.IsVisible = True
                    lbtnDefaultValues.Enabled = True
                    txtCertificationNo.ReadOnly = False

                    btnGettestResult.Enabled = False
                Case ICertificationView.Context.ExistingCertificateNoResults
                    TRView.IsVisible = False
                    lbtnDefaultValues.Enabled = False

                    btnGettestResult.Enabled = True
                Case Else
                    ' Default
                    ErrorText1 = WarningText
            End Select

        Catch ex As Exception
            Throw
        End Try

    End Sub


    ''' <summary>
    ''' Method to Force view load.
    ''' </summary>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub DoLoadView() Implements Presenter.ICertificationView.DoLoadView
        Try
            MyBase.Page_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' txtCertificationNo Text Changed event.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub txtCertificationNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCertificationNo.TextChanged
        Try
            CertificationSearchPresenter.CurrentCertificateNumber = Me.txtCertificationNo.Text
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' cbxRemoveMatlNum Checked Changed event.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub cbxRemoveMatlNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxRemoveMatlNum.CheckedChanged
        Try
            RemoveMatlNum = cbxRemoveMatlNum.Checked
        Catch ex As Exception
            Throw
        End Try

    End Sub

#End Region


End Class