Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

'Imark certificate Form
Partial Public Class ImarkCertificationUC
    Inherits BaseCertificationControl
    Implements IImarkCertificationView

    ' Changed the sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Members"
    ''' <summary>
    '''  Save event
    ''' </summary>
    Public Event Save As CustomEvents.PlainEventHandler Implements ICertificationView.Save
    ''' <summary>
    '''  Save Reasons event
    ''' </summary>
    Public Event SaveReasons As CustomEvents.PlainEventHandler Implements ICertificationView.SaveReasons
    ''' <summary>
    '''  Show Test Results event
    ''' </summary>
    Public Event ShowTestResults As CustomEvents.PlainEventHandler Implements ICertificationView.ShowTestResults
    ''' <summary>
    '''  Get Test Results event
    ''' </summary>
    Public Event GetTestResults As CustomEvents.PlainEventHandler Implements ICertificationView.GetTestResults
    ''' <summary>
    '''  Get Blank Results event
    ''' </summary>
    Public Event GetBlankResults As CustomEvents.PlainEventHandler Implements ICertificationView.GetBlankResults
    ''' <summary>
    '''  Copy Similar Tire SKU Certificate event
    ''' </summary>
    Public Event CopySimilarTireSKUCertificate As CustomEvents.PlainEventHandler Implements ICertificationView.CopySimilarTireSKUCertificate
    ''' <summary>
    '''  Show Default Values event
    ''' </summary>
    Public Event ShowDefaultValues() Implements Presenter.ICertificationView.ShowDefaultValues
    ''' <summary>
    '''  Renew event
    ''' </summary>
    Public Event Renew() Implements Presenter.IImarkCertificationView.Renew
    ''' <summary>
    '''  Mold Stamping Refresh event
    ''' </summary>
    Public Event MoldStampingRefresh() Implements Presenter.IImarkCertificationView.MoldStampingRefresh
    ''' <summary>
    '''  Imark Certification view presenter. 
    ''' </summary>
    Private m_presenter As ImarkCertificationPresenter
    ''' <summary>
    ''' SKU ID
    ''' </summary>
    Private m_skuid As Integer
    ''' <summary>
    '''  Certificate Type
    ''' </summary>
    Private m_enuCertType As Integer = 4 'NameAid.Certification = NameAid.Certification.Imark
    ''' <summary>
    '''  Manufacturing Locs
    ''' </summary>
    Private m_manufacturingLocs As DataSet
    ''' <summary>
    '''  Audit Log
    ''' </summary>
    Private m_lstAuditLog As List(Of AuditLogEntry)
    ''' <summary>
    '''  Reasons
    ''' </summary>
    Private m_dstReasons As DataSet
    ''' <summary>
    '''  Similar Certificate
    ''' </summary>
    Private m_dtbSimilarCertificate As DataTable
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New ImarkCertificationPresenter(Me)

    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets Certification Type value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
    ''' Gets or sets TRView value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property TRView() As Presenter.ITestResultsView Implements Presenter.IImarkCertificationView.TRView
        Get
            Return ucTestResults
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets Info Text value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property InfoText() As String Implements ICertificationView.InfoText
        Get
            Return lblInfoText.Text
        End Get
        Set(ByVal value As String)
            lblInfoText.Text = value
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements ICertificationView.ErrorText
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ProductData() As String Implements IImarkCertificationView.ProductData
        Get
            Return lblProductData.Text
        End Get
        Set(ByVal value As String)
            lblProductData.Text = value
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
    ''' Gets or sets Remove Material Number value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property RemoveMatlNum() As Boolean Implements IImarkCertificationView.RemoveMatlNum
        Get
            Return cbxRemoveMatlNum.Checked
        End Get
        Set(ByVal value As Boolean)
            cbxRemoveMatlNum.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets ReasonDS value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ExtensionNo() As String Implements ICertificationView.ExtensionNo
        Get
            Return txtExtension.Text
        End Get
        Set(ByVal value As String)
            txtExtension.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Number ID value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
    ''' Gets or sets Date Assigned value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property DateAssigned() As String Implements IImarkCertificationView.DateAssigned
        Get
            Return txtDateAssigned.Text
        End Get
        Set(ByVal value As String)
            txtDateAssigned.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Date Submitted value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertDateSubmitted() As String Implements IImarkCertificationView.CertDateSubmitted
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertDateApproved() As String Implements IImarkCertificationView.CertDateApproved
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property DateSubmitted() As String Implements IImarkCertificationView.DateSubmitted
        Get
            Return txtDateSubmitted.Text
        End Get
        Set(ByVal value As String)
            txtDateSubmitted.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Date Approved value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property DateApproved() As String Implements IImarkCertificationView.DateApproved
        Get
            Return txtDateApproved.Text
        End Get
        Set(ByVal value As String)
            txtDateApproved.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Date Expiry value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property DateExpiry() As String Implements IImarkCertificationView.DateExpiry
        Get
            Return txtDateExpiry.Text
        End Get
        Set(ByVal value As String)
            txtDateExpiry.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Renewal Required value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property RenewalRequired() As Boolean Implements IImarkCertificationView.RenewalRequired
        Get
            Return rbtnLstrenealRequired.Items(0).Selected
        End Get
        Set(ByVal value As Boolean)
            rbtnLstrenealRequired.Items(0).Selected = value
            rbtnLstrenealRequired.Items(1).Selected = Not value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Active Status value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ActiveStatus() As Boolean Implements IImarkCertificationView.ActiveStatus
        Get
            Return rbtnlstActiveStatus.Items(0).Selected
        End Get
        Set(ByVal value As Boolean)
            rbtnlstActiveStatus.Items(0).Selected = value
            rbtnlstActiveStatus.Items(1).Selected = Not value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Emark Reference value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property EmarkReference() As String Implements IImarkCertificationView.EmarkReference
        Get
            Return txtEmarkReference.Text
        End Get
        Set(ByVal value As String)
            txtEmarkReference.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Imark Family value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ImarkFamily() As String Implements IImarkCertificationView.ImarkFamily
        Get
            Return txtImarkFamily.Text
        End Get
        Set(ByVal value As String)
            txtImarkFamily.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Imark Stamping value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ImarkStamping() As String Implements IImarkCertificationView.ImarkStamping
        Get
            Return txtIMarkStamping.Text
        End Get
        Set(ByVal value As String)
            txtIMarkStamping.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets To Be Renewed value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ToBeRenewedCertificate() As DomainEntities.Certificate Implements Presenter.IImarkCertificationView.ToBeRenewedCertificate
        Get
            Return CType(Session("ToBeRenewedCertificate"), Certificate)
        End Get
        Set(ByVal value As DomainEntities.Certificate)
            Session("ToBeRenewedCertificate") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Manufacturing Location Id value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ManufacturingLocationId() As String Implements ICertificationView.ManufacturingLocationId
        Get
            Return CStr(0)
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Audit Log List value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property DiscDate() As String Implements IImarkCertificationView.DiscDate
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property AddInfo() As String Implements IImarkCertificationView.AddInfo
        Get
            Return txtAddInfo.Text
        End Get
        Set(ByVal value As String)
            txtAddInfo.Text = value
        End Set

    End Property

    'JBH_2.00 Project 5325 - Added Operation Approval Date
    ''' <summary>
    ''' Gets or sets Oper Date Approved value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property OperDateApproved() As String Implements IImarkCertificationView.OperDateApproved
        Get
            Return txtOperDateApproved.Text
        End Get
        Set(ByVal value As String)
            txtOperDateApproved.Text = value
        End Set
    End Property

    'JBH_2.00 Project 5325 - Added Mold Changed Flag
    ''' <summary>
    ''' Gets or sets Mold Charge Required value.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property MoldChgRequired() As Boolean Implements IImarkCertificationView.MoldChgRequired
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
    ''' Save Button Click.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            RaiseEvent Save()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Get Test Result Button Click.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnGettestResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTestResult.Click
        Try
            RaiseEvent GetTestResults()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Blank Result Button Click.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnBlankResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlankResult.Click
        Try
            RaiseEvent GetBlankResults()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup view control states according to specific context.
    ''' </summary>
    ''' <param name="p_enuContext">Context</param>
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupControlContextState(ByVal p_enuContext As Presenter.ICertificationView.Context) Implements Presenter.ICertificationView.SetupControlContextState
        Const WarningText As String = "Warning: unknown certificate status"
        Try
            Select Case p_enuContext
                Case ICertificationView.Context.NewCertificate
                    TRView.IsVisible = False
                    lbtnDefaultValues.Enabled = False

                    btnGetTestResult.Enabled = True
                    btnRenew.Enabled = False

                    ' NOTE: m.b. [not applic] warning;
                    ' if so, remove the warning and enable the gettestresult button when the adding Imark certificate approved;
                Case ICertificationView.Context.JustAddedCertificate
                    TRView.IsVisible = False
                    lbtnDefaultValues.Enabled = False
                    txtCertificationNo.ReadOnly = True

                    btnGetTestResult.Enabled = True
                    btnRenew.Enabled = False
                    ' ErrorText = "Warning: unknown IMARK certificate status"
                Case ICertificationView.Context.GotTestResults
                    TRView.IsVisible = True
                    lbtnDefaultValues.Enabled = False

                    btnGetTestResult.Enabled = False
                    btnRenew.Enabled = False
                Case ICertificationView.Context.ExistingCertificate
                    TRView.IsVisible = True
                    lbtnDefaultValues.Enabled = True

                    btnGetTestResult.Enabled = False
                    btnRenew.Enabled = False
                Case ICertificationView.Context.ExistingCertificateNoResults
                    TRView.IsVisible = False
                    lbtnDefaultValues.Enabled = False

                    btnGetTestResult.Enabled = True
                    btnRenew.Enabled = False
                Case Else
                    ' Default
                    ErrorText = WarningText
            End Select
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Show Similar Tire Prompt.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowSimilarTirePrompt() Implements ICertificationView.ShowSimilarTirePrompt
        Const SKUIdText As String = "SKUID"
        Const MaterialNumberText As String = "MATL_NUM"
        Try
            If Me.SimilarCertificateDS.Rows.Count > 0 Then
                Dim Row As DataRow = Me.SimilarCertificateDS.Rows(0)
                Me.lblSimilarSKUID.Text = Row.Item(SKUIdText).ToString()
                Me.lblSimilarMaterial.Text = Row.Item(MaterialNumberText).ToString()
                Me.ddlSimilarCertificate.DataBind()
                Me.SimilarTirePopUp.Show()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Yes Button Click.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_Yes(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strCertificationNumber As String = Me.ddlSimilarCertificate.SelectedItem.Text
        Dim intCertificationTypeID As Integer = Me.CertificationType
        Dim intSKUID As Integer = CInt(Me.lblSimilarSKUID.Text)
        Dim strMaterialNum As String = Me.lblSimilarMaterial.Text
        Dim blnDone As Boolean = False
        Try
            If m_presenter.GetCertificate(strCertificationNumber, intCertificationTypeID, intSKUID, strMaterialNum) Then
                Me.SimilarTirePopUp.Dispose()

                RaiseEvent CopySimilarTireSKUCertificate()
                RaiseEvent ShowTestResults()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' No Button Click.
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
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Cancel Reasons Click.
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

            'Re-enable buttons
            btnBlankResult.Enabled = True
            btnGetTestResult.Enabled = True
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Save Reasons Click.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_SaveReasons(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim ddlReasonList As DropDownList
        Dim txtNote As TextBox
        Dim lstAuditLog As New List(Of AuditLogEntry)
        Const AreaText As String = "Area"
        Const ChangeDateText As String = "Change Date"
        Const ChangeFieldText As String = "Changed Field"
        Const NoteText As String = "Note"
        Const NoteTxt As String = "txtNote"
        Const ReasonListText As String = "ddlReasonList"
        Try
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
                            txtNote = CType(Row.Cells(1).FindControl(NoteTxt), TextBox)
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

            btnBlankResult.Enabled = True

            RaiseEvent SaveReasons()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Set up Default Values View.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Default Values Button Click.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub lbtnDefaultValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDefaultValues.Click
        Try
            RaiseEvent ShowDefaultValues()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Renew Button Click.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnRenew(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRenew.Click
        Const CertificationSearchEXText As String = "CertificationSearchEx.aspx"
        Try
            RaiseEvent Renew()
            'We need to back out so that the certificate can be refreshed.
            Response.Redirect(CertificationSearchEXText)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Force view load.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub DoLoadView() Implements Presenter.ICertificationView.DoLoadView
        Try
            MyBase.Page_Load(Nothing, Nothing)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Display Changes to Client.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
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

            Me.btnBlankResult.Enabled = False
            Me.btnRenew.Enabled = False
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Certification Number Text Changed.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub txtCertificationNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCertificationNo.TextChanged
        Try
            CertificationSearchPresenter.CurrentCertificateNumber = Me.txtCertificationNo.Text
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Remove Material Number Checked Changed.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub cbxRemoveMatlNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxRemoveMatlNum.CheckedChanged
        Try
            RemoveMatlNum = cbxRemoveMatlNum.Checked
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Event for Refresh Button Click.
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
    ''' <para>11/06/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As ImageClickEventArgs) Handles btnRefresh.Click
        Try
            RaiseEvent MoldStampingRefresh()
        Catch
            Throw
        End Try
    End Sub

#End Region

End Class
