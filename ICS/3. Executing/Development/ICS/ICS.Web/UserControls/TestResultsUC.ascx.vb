Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

' Test results form
Partial Public Class TestResultsUC
    Inherits BaseUserControl
    Implements ITestResultsView

    ' Changed the sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.

#Region "Members and Constants"

    ''' <summary>
    '''  Declaring Load View Data Event
    ''' </summary>
    Public Event DoLoadViewDataEvent() Implements Presenter.ITestResultsView.DoLoadViewDataEvent

    ''' <summary>
    '''  Declaring Load View Blank Event
    ''' </summary>
    Public Event DoLoadViewBlankEvent() Implements Presenter.ITestResultsView.DoLoadViewBlankEvent

    ''' <summary>
    '''  Declaring Get Requested Tests
    ''' </summary>
    Public Event GetRequestedTests() Implements Presenter.ITestResultsView.GetRequestedTests

    ''' <summary>
    '''  Declaring object of TestResultsPresenter class
    ''' </summary>
    Private m_presenter As TestResultsPresenter

    ''' <summary>
    '''  Constant to hold TestResultMaterialNumber text
    ''' </summary>
    Private Const TestResultMaterialNumber As String = "TestResultMaterialNumber"

    ''' <summary>
    '''  Constant to hold TestResultSKUID text
    ''' </summary>
    Private Const TestResultSKUID As String = "TestResultSKUID"

    ''' <summary>
    '''  Constant to hold TestResultCertificationTypeId text
    ''' </summary>
    Private Const TestResultCertificationTypeId As String = "TestResultCertificationTypeId"

    ''' <summary>
    '''  Constant to hold TestResultCertificateNumber text
    ''' </summary>
    Private Const TestResultCertificateNumber As String = "TestResultCertificateNumber"

    ''' <summary>
    '''  Constant to hold TestResultExtensionNo text
    ''' </summary>
    Private Const TestResultExtensionNo As String = "TestResultExtensionNo"

    ''' <summary>
    '''  Constant to hold CertificateNumberID text
    ''' </summary>
    Private Const CertificateNumberIDText As String = "CertificateNumberID"

    ''' <summary>
    '''  Constant to hold SimilarTireMatlNum text
    ''' </summary>
    Private Const SimilarTireMatlNum As String = "SimilarTireMatlNum"

    ''' <summary>
    '''  Constant to hold SimilarTireSKUID text
    ''' </summary>
    Private Const SimilarTireSKUIDText As String = "SimilarTireSKUID"

    ''' <summary>
    '''  Constant to hold ManufacturingLocationId text
    ''' </summary>
    Private Const ManufacturingLocationIdText As String = "ManufacturingLocationId"

    ''' <summary>
    '''  Constant to hold TireTypeId text
    ''' </summary>
    Private Const TireTypeIdText As String = "TireTypeId"

    ''' <summary>
    '''  Constant to hold ClientRequest text
    ''' </summary>
    Private Const ClientRequestText As String = "ClientRequest"

    ''' <summary>
    '''  Constant to hold TRProductSectionData_OriginalProduct text
    ''' </summary>
    Private Const TRProductSectionDataOriginalProductText As String = "TRProductSectionData_OriginalProduct"

    ''' <summary>
    '''  Constant to hold TRMeasureSectionData_OriginalMeasure text
    ''' </summary>
    Private Const TRMeasureSectionDataOriginalMeasureText As String = "TRMeasureSectionData_OriginalMeasure"

    ''' <summary>
    '''  Constant to hold TRMeasureSectionData_OriginalTreadwear text
    ''' </summary>
    Private Const TRMeasureSectionDataOriginalTreadwearText As String = "TRMeasureSectionData_OriginalTreadwear"

    ''' <summary>
    '''  Constant to hold TRMeasureSectionData_OriginalPlunger text
    ''' </summary>
    Private Const TRMeasureSectionDataOriginalPlungerText As String = "TRMeasureSectionData_OriginalPlunger"

    ''' <summary>
    '''  Constant to hold TRMeasureSectionData_OriginalBeadUnSeat text
    ''' </summary>
    Private Const TRMeasureSectionDataOriginalBeadUnSeatText As String = "TRMeasureSectionData_OriginalBeadUnSeat"

    ''' <summary>
    '''  Constant to hold TREnduranceSectionData_OriginalEndurance text
    ''' </summary>
    Private Const TREnduranceSectionDataOriginalEnduranceText As String = "TREnduranceSectionData_OriginalEndurance"

    ''' <summary>
    '''  Constant to hold TRHighSpeedSectionData_OriginalHighSpeed text
    ''' </summary>
    Private Const TRHighSpeedSectionDataOriginalHighSpeedText As String = "TRHighSpeedSectionData_OriginalHighSpeed"

    ''' <summary>
    '''  Constant to hold TRSoundWetSectionData_OriginalSound text
    ''' </summary>
    Private Const TRSoundWetSectionDataOriginalSoundText As String = "TRSoundWetSectionData_OriginalSound"

    ''' <summary>
    '''  Constant to hold TRSoundWetSectionData_OriginalWetGrip text
    ''' </summary>
    Private Const TRSoundWetSectionDataOriginalWetGripText As String = "TRSoundWetSectionData_OriginalWetGrip"
#End Region

#Region "Constructors"

    ''' <summary>   
    ''' Constructor to initialize class members.
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New TestResultsPresenter(Me)

    End Sub

#End Region

#Region "Public Properties"

    ''' <summary>
    ''' Gets or sets Is Invisible value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Is Visible.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property IsVisible() As Boolean Implements Presenter.ITestResultsView.IsVisible
        Get
            Return Me.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.Visible = value
        End Set
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property InfoText() As String Implements Presenter.ITestResultsView.InfoText
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
    ''' <value>String</value>
    ''' <returns>Error Text.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements Presenter.ITestResultsView.ErrorText
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MaterialNumber() As String Implements Presenter.ITestResultsView.MaterialNumber
        Get
            Return CStr(Session(TestResultMaterialNumber))
        End Get
        Set(ByVal value As String)
            Session(TestResultMaterialNumber) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Sku ID value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Sku Id.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SKUID() As Integer Implements Presenter.ITestResultsView.SKUID
        Get
            Return CInt(Session(TestResultSKUID))
        End Get
        Set(ByVal value As Integer)
            Session(TestResultSKUID) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certification Type ID value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Certification Type Id.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertificationTypeId() As Integer Implements Presenter.ITestResultsView.CertificationTypeId
        Get
            Return CInt(Session(TestResultCertificationTypeId))
        End Get
        Set(ByVal value As Integer)
            Session(TestResultCertificationTypeId) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Certificate Number.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertificateNumber() As String Implements Presenter.ITestResultsView.CertificateNumber
        Get
            Return CStr(Session(TestResultCertificateNumber))
        End Get
        Set(ByVal value As String)
            Session(TestResultCertificateNumber) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Extension No value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Extension No.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ExtensionNo() As String Implements Presenter.ITestResultsView.ExtensionNo
        Get
            Return CStr(Session(TestResultExtensionNo))
        End Get
        Set(ByVal value As String)
            Session(TestResultExtensionNo) = value
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertificateNumberID() As Integer Implements Presenter.ITestResultsView.CertificateNumberID
        Get
            Return CInt(Session(Me.GetType().Name & CertificateNumberIDText))
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & CertificateNumberIDText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Similar Tire Material Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Similar Tire Material Number.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SimilarTireMaterialNum() As String Implements Presenter.ITestResultsView.SimilarTireMatlNum
        Get
            Return CStr(Session(Me.GetType().Name & SimilarTireMatlNum))
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & SimilarTireMatlNum) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Similar Tire SKUID value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Similar Tire SKUID.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SimilarTireSKUID() As Integer Implements Presenter.ITestResultsView.SimilarTireSKUID
        Get
            Return CInt(Session(Me.GetType().Name & SimilarTireSKUIDText))
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & SimilarTireSKUIDText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Manufacturing Location Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Manufacturing Location Id.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ManufacturingLocationId() As Integer Implements Presenter.ITestResultsView.ManufacturingLocationId
        Get
            Return CInt(Session(Me.GetType().Name & ManufacturingLocationIdText))
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & ManufacturingLocationIdText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Type Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Manufacturing Location Id.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TireTypeId() As Integer Implements Presenter.ITestResultsView.TireTypeId
        Get
            Return CInt(Session(Me.GetType().Name & TireTypeIdText))
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & TireTypeIdText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Client Request value.
    ''' </summary>
    ''' <value>DataTable</value>
    ''' <returns>Client Request.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ClientRequest() As DataTable Implements Presenter.ITestResultsView.ClientRequest
        Get
            Return CType(Session(Me.GetType().Name & ClientRequestText), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session(Me.GetType().Name & ClientRequestText) = value
        End Set
    End Property

#End Region

#Region "Private Properties"

    ''' <summary>
    ''' Gets or sets TRProductSectionData OriginalProduct value.
    ''' </summary>
    ''' <value>Product</value>
    ''' <returns>TRProductSectionData OriginalProduct.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Property TRProductSectionData_OriginalProduct() As Product
        Get
            Return CType(Session(TRProductSectionDataOriginalProductText), Product)
        End Get
        Set(ByVal value As Product)
            Session(TRProductSectionDataOriginalProductText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets TRMeasureSectionData OriginalMeasure value.
    ''' </summary>
    ''' <value>Measure</value>
    ''' <returns>TRMeasureSectionData OriginalMeasure.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Property TRMeasureSectionData_OriginalMeasure() As Measure
        Get
            Return CType(Session(TRMeasureSectionDataOriginalMeasureText), Measure)
        End Get
        Set(ByVal value As Measure)
            Session(TRMeasureSectionDataOriginalMeasureText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets TRMeasureSectionData OriginalTreadwear value.
    ''' </summary>
    ''' <value>Treadwear</value>
    ''' <returns>TRMeasureSectionData OriginalTreadwear.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Property TRMeasureSectionData_OriginalTreadwear() As Treadwear
        Get
            Return CType(Session(TRMeasureSectionDataOriginalTreadwearText), Treadwear)
        End Get
        Set(ByVal value As Treadwear)
            Session(TRMeasureSectionDataOriginalTreadwearText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets TRMeasureSectionData OriginalPlunger value.
    ''' </summary>
    ''' <value>Plunger</value>
    ''' <returns>TRMeasureSectionData OriginalPlunger.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Property TRMeasureSectionData_OriginalPlunger() As Plunger
        Get
            Return CType(Session(TRMeasureSectionDataOriginalPlungerText), Plunger)
        End Get
        Set(ByVal value As Plunger)
            Session(TRMeasureSectionDataOriginalPlungerText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets TRMeasureSectionData OriginalBeadUnSeat value.
    ''' </summary>
    ''' <value>BeadUnSeat</value>
    ''' <returns>TRMeasureSectionData OriginalBeadUnSeat.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Property TRMeasureSectionData_OriginalBeadUnSeat() As BeadUnSeat
        Get
            Return CType(Session(TRMeasureSectionDataOriginalBeadUnSeatText), BeadUnSeat)
        End Get
        Set(ByVal value As BeadUnSeat)
            Session(TRMeasureSectionDataOriginalBeadUnSeatText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets TREnduranceSectionData OriginalEndurance value.
    ''' </summary>
    ''' <value>Endurance</value>
    ''' <returns>TREnduranceSectionData OriginalEndurance.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Property TREnduranceSectionData_OriginalEndurance() As Endurance
        Get
            Return CType(Session(TREnduranceSectionDataOriginalEnduranceText), Endurance)
        End Get
        Set(ByVal value As Endurance)
            Session(TREnduranceSectionDataOriginalEnduranceText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets TRHighSpeedSectionData OriginalHighSpeed value.
    ''' </summary>
    ''' <value>HighSpeed</value>
    ''' <returns>TRHighSpeedSectionData OriginalHighSpeed.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Property TRHighSpeedSectionData_OriginalHighSpeed() As HighSpeed
        Get
            Return CType(Session(TRHighSpeedSectionDataOriginalHighSpeedText), HighSpeed)
        End Get
        Set(ByVal value As HighSpeed)
            Session(TRHighSpeedSectionDataOriginalHighSpeedText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets TRSoundWetSectionData OriginalSound value.
    ''' </summary>
    ''' <value>Sound</value>
    ''' <returns>TRSoundWetSectionData OriginalSound.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Property TRSoundWetSectionData_OriginalSound() As Sound
        Get
            Return CType(Session(TRSoundWetSectionDataOriginalSoundText), Sound)
        End Get
        Set(ByVal value As Sound)
            Session(TRSoundWetSectionDataOriginalSoundText) = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets TRSoundWetSectionData OriginalWetGrip value.
    ''' </summary>
    ''' <value>WetGrip</value>
    ''' <returns>TRSoundWetSectionData OriginalWetGrip.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Property TRSoundWetSectionData_OriginalWetGrip() As WetGrip
        Get
            Return CType(Session(TRSoundWetSectionDataOriginalWetGripText), WetGrip)
        End Get
        Set(ByVal value As WetGrip)
            Session(TRSoundWetSectionDataOriginalWetGripText) = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    '''  Initiate data load
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub DoLoadViewData() Implements Presenter.ITestResultsView.DoLoadViewData

        RaiseEvent DoLoadViewDataEvent()

    End Sub

    ''' <summary>
    ''' Initiate blank load
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub DoLoadViewBlank() Implements Presenter.ITestResultsView.DoLoadViewBlank

        RaiseEvent DoLoadViewBlankEvent()

    End Sub

    ''' <summary>
    ''' Initiate Adjust Certification Type View
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub AdjustViewToCertificationType() Implements ITestResultsView.AdjustViewToCertificationType
        Const General As String = "GENERAL"
        Const XText As String = "X"
        Const India As String = "India"
        Const DText As String = "D"
        Const SmallDText As String = "d"
        Const cText As String = "c"
        Const SmallXText As String = "x"
        Const DataText As String = "_Data"

        'Dim strCertificationTypeName As String = [Enum].GetName(GetType(NameAid.Certification), CertificationTypeId)
        Dim strCertificationTypeName As String = m_presenter.GetCertificationTypeName(CertificationTypeId)

        'Indicator is used to determine which fields to display on screen
        Dim strCertificationIndicator As String

        'check here for generic type
        Dim strCertTemplate As String = m_presenter.CertTemplate(strCertificationTypeName)

        If strCertTemplate = General Then
            strCertificationIndicator = XText

        Else

            'yfye added logic to include india mark
            strCertificationIndicator = CStr(IIf(strCertificationTypeName.Contains(India), DText, strCertificationTypeName.Substring(0, 1)))

        End If

        'Hide Test Results title label if CCC.
        If strCertificationIndicator.ToLower() = cText Or strCertificationIndicator.ToLower = SmallDText Or strCertificationIndicator.ToLower = SmallXText Then
            title.Visible = False
        End If

        For Each ctlControl As Control In pnlTestResult.Controls
            If ctlControl.ID IsNot Nothing Then
                If ctlControl.GetType().Name = GetType(Panel).Name Then
                    ctlControl.Visible = BelongsToCertificationType(ctlControl.ID, strCertificationIndicator)
                    For Each ctlSection As Control In ctlControl.Controls
                        If ctlSection.ID IsNot Nothing Then
                            If ctlSection.GetType().Name = GetType(Panel).Name And ctlSection.ID.Contains(DataText) Then
                                For Each ctlData As Control In ctlSection.Controls
                                    If ctlData.GetType().Name = GetType(Panel).Name Then
                                        ctlData.Visible = BelongsToCertificationType(ctlData.ID, strCertificationIndicator)
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If
            End If
        Next

        'ECE30/54
        If CertificationTypeId = 1 Then
            Me.SoundWetGrip_E.Visible = False
            Me.pnlSoundWetGrip_E.Visible = False
            Me.optlstNominalWidthYN.Visible = False
            If TireTypeId = 1 Then
                'ECE30/54 Turn off these for passenger tire types
                Me.lblSingLoadCapacityIndex.Visible = False
                Me.txtSingLoadCapacityIndex_CEGID.Visible = False
                Me.lblDualLoadCapacityIndex.Visible = False
                Me.txtDualLoadCapacityIndex_CEGID.Visible = False
                Me.lblIndicationRegroovable.Visible = False
                Me.optlstIndicationRegroovable_CEGD.Visible = False
            Else
                Me.lblSingLoadCapacityIndex.Visible = True
                Me.txtSingLoadCapacityIndex_CEGID.Visible = True
                Me.lblDualLoadCapacityIndex.Visible = True
                Me.txtDualLoadCapacityIndex_CEGID.Visible = True
                Me.lblIndicationRegroovable.Visible = True
                Me.optlstIndicationRegroovable_CEGD.Visible = True
            End If
        End If

        'ECE117
        If CertificationTypeId = 6 Then
            Me.pnlEndurance_Data.Visible = False
            Me.Endurance_EGIN.Visible = False
            Me.EnduranceTestBefore_EI.Visible = False
            Me.EnduranceTestAfter_EIN.Visible = False
            Me.pnlHighSpeed_Data.Visible = False
            Me.HighSpeed_EGIN.Visible = False
            Me.HighSpeedTestBefore_EI.Visible = False
            Me.HighSpeedTestAfter_EIN.Visible = False
            Me.pnlMeasurement_Data.Visible = False
            Me.Measurement_EGIN.Visible = False
            If TireTypeId = 1 Then
                'ECE117 Turn off these for passenger tire types
                Me.lblSingLoadCapacityIndex.Visible = False
                Me.txtSingLoadCapacityIndex_CEGID.Visible = False
                Me.lblDualLoadCapacityIndex.Visible = False
                Me.txtDualLoadCapacityIndex_CEGID.Visible = False
                Me.lblIndicationRegroovable.Visible = False
                Me.optlstIndicationRegroovable_CEGD.Visible = False
            Else
                Me.lblSingLoadCapacityIndex.Visible = True
                Me.txtSingLoadCapacityIndex_CEGID.Visible = True
                Me.lblDualLoadCapacityIndex.Visible = True
                Me.txtDualLoadCapacityIndex_CEGID.Visible = True
                Me.lblIndicationRegroovable.Visible = True
                Me.optlstIndicationRegroovable_CEGD.Visible = True
            End If
        End If

        If CertificationTypeId = 1 Then
            Me.pnlEnduranceDifferenceOuterDiameterToleranceAfter_E.Visible = False
            Me.pnlHighSpeedDifferenceOuterDiameterToleranceAfter_E.Visible = False
        End If

        'NOM
        If CertificationTypeId = 3 Then
            Me.EnduranceTestAfter_EIN.Visible = False
            Me.HighSpeedTestAfter_EIN.Visible = False
        End If

        'Hidden field. Do not display to client
        Me.pnlDOTSerialNumber_CEGIND.Visible = False

    End Sub

    ''' <summary>
    ''' Check if data control belongs to certification type
    ''' </summary>
    ''' <param name="p_strControlID">Control Id</param>
    ''' <param name="p_strCertificationIndicator">Certification Indicator</param>
    ''' <returns>Boolean.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Private Function BelongsToCertificationType(ByVal p_strControlID As String, ByVal p_strCertificationIndicator As String) As Boolean
        Const UnderScoreText As String = "_"
        Dim iIndex As Integer
        Dim strCertificationIndicator As String
        iIndex = p_strControlID.IndexOf(UnderScoreText)
        If iIndex <= 0 Then
            Return True
        End If
        strCertificationIndicator = p_strControlID.Substring(iIndex)
        Return strCertificationIndicator.Contains(p_strCertificationIndicator)

    End Function

    ''' <summary>
    ''' Get TR data control value and set respective field value in data object
    ''' </summary>
    ''' <param name="p_fField">Field Info</param>
    ''' <param name="p_ctlDataElem">Data Element</param>
    ''' <param name="p_objTRSectionData">TR Section Data</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Private Sub GetTRDataControlValue(ByRef p_fField As System.Reflection.FieldInfo, ByVal p_ctlDataElem As Control, ByRef p_objTRSectionData As Object)
        Const StringText As String = "String"
        Const optlstStructureConstruction As String = "optlstStructureConstruction"
        Const RADIALText As String = "RADIAL"
        Const BIASText As String = "BIAS"
        Const BELTEDText As String = "BELTED"

        Dim strTypeName As String = p_ctlDataElem.GetType().Name
        Select Case (strTypeName)
            Case GetType(TextBox).Name
                p_fField.SetValue(p_objTRSectionData, CType(p_ctlDataElem, TextBox).Text)
            Case GetType(RadioButtonList).Name
                If p_fField.FieldType.Name = StringText Then
                    If p_ctlDataElem.ID = optlstStructureConstruction Then
                        If CType(p_ctlDataElem, RadioButtonList).Items(0).Selected Then
                            p_fField.SetValue(p_objTRSectionData, RADIALText) 'radial
                        ElseIf CType(p_ctlDataElem, RadioButtonList).Items(1).Selected Then
                            p_fField.SetValue(p_objTRSectionData, BIASText) 'bias
                        ElseIf CType(p_ctlDataElem, RadioButtonList).Items(2).Selected Then
                            p_fField.SetValue(p_objTRSectionData, BELTEDText) 'bias belted
                        Else
                            p_fField.SetValue(p_objTRSectionData, RADIALText)
                        End If
                    End If
                Else
                    p_fField.SetValue(p_objTRSectionData, CType(p_ctlDataElem, RadioButtonList).Items(0).Selected)
                End If
            Case GetType(DropDownList).Name

            Case Else
                Throw New Exception("Error getting value from " + p_ctlDataElem.ID)
        End Select

    End Sub

    ''' <summary>
    ''' Set the data value to the view
    ''' </summary>
    ''' <param name="p_ctlDataElem">Data Element</param>
    ''' <param name="p_objValue">Object Value</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>  
    Private Sub SetTRDataControlValue(ByVal p_ctlDataElem As Control, ByVal p_objValue As Object)
        Const TxtMeasureMatlNumCEGINDText As String = "txtMeasureMatlNum_CEGIND"
        Const TxtTreadwearMatlNumCEGINDText As String = "txtTreadwearMatlNum_CEGIND"
        Const TxtPlungerMatlNumCEGINDText As String = "txtPlungerMatlNum_CEGIND"
        Const TxtBeadUnseatMatlNumCEGINDText As String = "txtBeadUnseatMatlNum_CEGIND"
        Const TxtEnduranceMatlNumText As String = "txtEnduranceMatlNum"
        Const TxtHighSpeedMatlNumText As String = "txtHighSpeedMatlNum"
        Const TxtGTSpecMeasureMatlNumCEGINDText As String = "txtGTSpecMeasureMatlNum_CEGIND"
        Const TxtGTSpecTreadwearMatlNumCEGINDText As String = "txtGTSpecTreadwearMatlNum_CEGIND"
        Const TxtGTSpecPlungerMatlNumCEGINDText As String = "txtGTSpecPlungerMatlNum_CEGIND"
        Const TxtGTSpecBeadUnseatMatlNumCEGINDText As String = "txtGTSpecBeadUnseatMatlNum_CEGIND"
        Const TxtGTSpecEnduranceMatlNumEGINText As String = "txtGTSpecEnduranceMatlNum_EGIN"
        Const TxtGTSpecHighSpeedMatlNumText As String = "txtGTSpecHighSpeedMatlNum"
        Const TxtTireIdText As String = "txtTireId"
        Const StringText As String = "String"
        Const OptlstStructureConstructionText As String = "optlstStructureConstruction"
        Const RADIALText As String = "RADIAL"
        Const BIASText As String = "BIAS"
        Const BELTEDText As String = "BELTED"
        Const DdlTireTypeText As String = "ddlTireType"

        Dim strTypeName As String = p_ctlDataElem.GetType().Name
        Select Case (strTypeName)
            Case GetType(TextBox).Name
                CType(p_ctlDataElem, TextBox).Text = CType(p_objValue, String)
                If CType(p_ctlDataElem, TextBox).ID = TxtMeasureMatlNumCEGINDText Or CType(p_ctlDataElem, TextBox).ID = TxtTreadwearMatlNumCEGINDText _
                    Or CType(p_ctlDataElem, TextBox).ID = TxtPlungerMatlNumCEGINDText Or CType(p_ctlDataElem, TextBox).ID = TxtBeadUnseatMatlNumCEGINDText _
                    Or CType(p_ctlDataElem, TextBox).ID = TxtEnduranceMatlNumText Or CType(p_ctlDataElem, TextBox).ID = TxtHighSpeedMatlNumText Then
                    CType(p_ctlDataElem, TextBox).Text = CType(p_ctlDataElem, TextBox).Text.TrimStart("0"c)
                End If
                If CType(p_ctlDataElem, TextBox).ID = TxtGTSpecMeasureMatlNumCEGINDText Or CType(p_ctlDataElem, TextBox).ID = TxtGTSpecTreadwearMatlNumCEGINDText _
                                    Or CType(p_ctlDataElem, TextBox).ID = TxtGTSpecPlungerMatlNumCEGINDText Or CType(p_ctlDataElem, TextBox).ID = TxtGTSpecBeadUnseatMatlNumCEGINDText _
                                    Or CType(p_ctlDataElem, TextBox).ID = TxtGTSpecEnduranceMatlNumEGINText Or CType(p_ctlDataElem, TextBox).ID = TxtGTSpecHighSpeedMatlNumText Then
                    CType(p_ctlDataElem, TextBox).Text = CType(p_ctlDataElem, TextBox).Text.TrimStart("0"c)
                End If

                If CType(p_ctlDataElem, TextBox).ID = TxtTireIdText Then
                    CType(p_ctlDataElem, TextBox).Text = CType(p_objValue, String)
                    ddlTireType.SelectedValue = CStr(CType(CType(p_ctlDataElem, TextBox).Text, Integer))
                End If
            Case GetType(RadioButtonList).Name
                If p_objValue.GetType.Name = StringText Then
                    If p_ctlDataElem.ID = OptlstStructureConstructionText Then
                        Select Case p_objValue.ToString().ToUpper()
                            Case RADIALText 'radial
                                CType(p_ctlDataElem, RadioButtonList).Items(0).Selected = True
                                CType(p_ctlDataElem, RadioButtonList).Items(1).Selected = False
                                CType(p_ctlDataElem, RadioButtonList).Items(2).Selected = False
                            Case BIASText 'bias
                                CType(p_ctlDataElem, RadioButtonList).Items(0).Selected = False
                                CType(p_ctlDataElem, RadioButtonList).Items(1).Selected = True
                                CType(p_ctlDataElem, RadioButtonList).Items(2).Selected = False
                            Case BELTEDText 'bias belted
                                CType(p_ctlDataElem, RadioButtonList).Items(0).Selected = False
                                CType(p_ctlDataElem, RadioButtonList).Items(1).Selected = False
                                CType(p_ctlDataElem, RadioButtonList).Items(2).Selected = True
                        End Select
                    End If
                Else
                    CType(p_ctlDataElem, RadioButtonList).Items(0).Selected = CType(p_objValue, Boolean)
                    CType(p_ctlDataElem, RadioButtonList).Items(1).Selected = Not CType(p_objValue, Boolean)
                End If
            Case GetType(DropDownList).Name
                If CType(p_ctlDataElem, DropDownList).ID = DdlTireTypeText Then
                    Dim ddlTireType As DropDownList = CType(p_ctlDataElem, DropDownList)
                    ddlTireType.DataSource = CType(p_objValue, DataTable)
                    ddlTireType.DataBind()
                End If
            Case Else
                Throw New Exception("Error setting value to " + p_ctlDataElem.ID)
        End Select

    End Sub

    ''' <summary>
    ''' Put TR section data to panel data controls
    ''' </summary>
    ''' <param name="p_objTRSectionData">TR Section Data</param>
    ''' <param name="p_pnlTRSD">Panel Information</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Private Sub PutTRSectionDataToPanel(ByVal p_objTRSectionData As Object, ByVal p_pnlTRSD As Panel)

        Dim typeTRSD As Type = p_objTRSectionData.GetType()
        Dim fields As Reflection.FieldInfo() = typeTRSD.GetFields()

        For Each field As Reflection.FieldInfo In fields
            Dim strDataElemName As String = field.Name
            Dim ctlDataElem As Control = FindTRDataControl(strDataElemName, p_pnlTRSD)

            ' Set control value depending on member-value type
            If ctlDataElem IsNot Nothing Then
                SetTRDataControlValue(ctlDataElem, field.GetValue(p_objTRSectionData))
            End If
        Next

    End Sub

    ''' <summary>
    ''' Read TR section data from panel into respective data object
    ''' </summary>
    ''' <param name="p_objTRSectionData">TR Section Data</param>
    ''' <param name="p_pnlTRSD">Panel Information</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Private Sub ReadTRSectionDataFromPanel(ByRef p_objTRSectionData As Object, ByVal p_pnlTRSD As Panel)

        Dim typeTRSD As Type = p_objTRSectionData.GetType()
        Dim fields As System.Reflection.FieldInfo() = typeTRSD.GetFields()

        For Each field As System.Reflection.FieldInfo In fields
            Dim strDataElemName As String = field.Name
            Dim ctlDataElem As Control = FindTRDataControl(strDataElemName, p_pnlTRSD)

            If ctlDataElem IsNot Nothing Then
                GetTRDataControlValue(field, ctlDataElem, p_objTRSectionData)
            End If
        Next

    End Sub

    ''' <summary>
    ''' Set TRProject section data to the view
    ''' </summary>
    ''' <param name="p_objTRPSD">TR Project Section Data</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Sub SetTRProjectSectionData(ByVal p_objTRPSD As DomainEntities.TRProjectSectionData) Implements Presenter.ITestResultsView.SetTRProjectSectionData

        PutTRSectionDataToPanel(p_objTRPSD, pnlProjectTest_Data)

    End Sub

    ''' <summary>
    ''' Set TRProduct section data to the view
    ''' </summary>
    ''' <param name="p_objTRPSD">TR Project Section Data</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub SetTRProductSectionData(ByVal p_objTRPSD As DomainEntities.TRProductSectionData) Implements Presenter.ITestResultsView.SetTRProductSectionData

        TRProductSectionData_OriginalProduct = p_objTRPSD.OriginalProduct

        PutTRSectionDataToPanel(p_objTRPSD, pnlProductData_Data)

    End Sub

    ''' <summary>
    ''' Set Measurement section data to the view
    ''' </summary>
    ''' <param name="p_objTRMSD">TR Measurement Section Data</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub SetTRMeasureSectionData(ByVal p_objTRMSD As TRMeasurementSectionData) Implements Presenter.ITestResultsView.SetTRMeasureSectionData

        TRMeasureSectionData_OriginalMeasure = p_objTRMSD.OriginalMeasure
        TRMeasureSectionData_OriginalTreadwear = p_objTRMSD.OriginalTreadwear
        TRMeasureSectionData_OriginalPlunger = p_objTRMSD.OriginalPlunger
        TRMeasureSectionData_OriginalBeadUnSeat = p_objTRMSD.OriginalBeadUnSeat

        PutTRSectionDataToPanel(p_objTRMSD, pnlMeasurement_Data)

    End Sub

    ''' <summary>
    ''' Set TR Endurance Before Section Data to the view
    ''' </summary>
    ''' <param name="p_objTREBSD">TR Endurance Test General Before Section Data</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub SetTREnduranceBeforeSectionData(ByVal p_objTREBSD As TREnduranceTestGeneralBeforeSectionData) Implements Presenter.ITestResultsView.SetTREnduranceBeforeSectionData

        PutTRSectionDataToPanel(p_objTREBSD, pnlEnduranceTestBefore_Data)

    End Sub

    ''' <summary>
    ''' Set TR Endurance Section Data to the view
    ''' </summary>
    ''' <param name="p_objTRESD">TR Endurance Section Data</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub SetTREnduranceSectionData(ByVal p_objTRESD As TREnduranceSectionData) Implements Presenter.ITestResultsView.SetTREnduranceSectionData

        TREnduranceSectionData_OriginalEndurance = p_objTRESD.OriginalEndurance

        PutTRSectionDataToPanel(p_objTRESD, pnlEndurance_Data)

    End Sub

    ''' <summary>
    ''' Set TR Endurance After Section Data to the view
    ''' </summary>
    ''' <param name="p_objTREASD">TR Endurance After Section Data</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub SetTREnduranceAfterSectionData(ByVal p_objTREASD As TREnduranceTestGeneralAfterSectionData) Implements Presenter.ITestResultsView.SetTREnduranceAfterSectionData

        PutTRSectionDataToPanel(p_objTREASD, pnlEnduranceTestAfter_Data)

    End Sub

    ''' <summary>
    ''' Set TR High Speed Before Section Data to the view
    ''' </summary>
    ''' <param name="p_objTRHSBSD">TR High Speed Before Section Data</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub SetTRHighSpeedBeforeSectionData(ByVal p_objTRHSBSD As TRHighSpeedTestGeneralBeforeSectionData) Implements Presenter.ITestResultsView.SetTRHighSpeedBeforeSectionData

        PutTRSectionDataToPanel(p_objTRHSBSD, pnlHighSpeedTestBefore_Data)

    End Sub

    ''' <summary>
    ''' Set TR High Speed Section Data to the view
    ''' </summary>
    ''' <param name="p_objTRHSSD">TR High Speed Before Section Data</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub SetTRHighSpeedSectionData(ByVal p_objTRHSSD As TRHighSpeedSectionData) Implements Presenter.ITestResultsView.SetTRHighSpeedSectionData

        TRHighSpeedSectionData_OriginalHighSpeed = p_objTRHSSD.OriginalHighSpeed

        PutTRSectionDataToPanel(p_objTRHSSD, pnlHighSpeed_Data)

    End Sub

    ''' <summary>
    ''' Set TR High Speed After Section Data to the view
    ''' </summary>
    ''' <param name="p_objTRHSASD">TR High Speed After Section Data</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub SetTRHighSpeedAfterSectionData(ByVal p_objTRHSASD As TRHighSpeedTestGeneralAfterSectionData) Implements Presenter.ITestResultsView.SetTRHighSpeedAfterSectionData

        PutTRSectionDataToPanel(p_objTRHSASD, pnlHighSpeedTestAfter_Data)

    End Sub

    ''' <summary>
    ''' Set TR Sound Wet Section Data to the view
    ''' </summary>
    ''' <param name="p_objTRSWSD">TR Sound Wet Section Data</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Sub SetTRSoundWetSectionData(ByVal p_objTRSWSD As TRSoundWetSectionData) Implements Presenter.ITestResultsView.SetTRSoundWetSectionData

        TRSoundWetSectionData_OriginalSound = p_objTRSWSD.OriginalSound
        TRSoundWetSectionData_OriginalWetGrip = p_objTRSWSD.OriginalWetGrip

        PutTRSectionDataToPanel(p_objTRSWSD, pnlSoundWetGrip_Data)

    End Sub

    ''' <summary>
    ''' Find TR section Data Control for respective data element
    ''' </summary>
    ''' <param name="p_strDataElemName">Data Element Name</param>
    ''' <param name="p_ctlContainer">Control Container</param>
    ''' <returns>Control object.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Private Function FindTRDataControl(ByVal p_strDataElemName As String, ByVal p_ctlContainer As Control) As Control

        Dim ctlDataElem As Control = Nothing

        For Each control As Control In p_ctlContainer.Controls
            If control.ID Is Nothing Then Continue For

            If control.GetType().Name = GetType(Panel).Name Then

                For Each ctlData As Control In control.Controls
                    If ctlData.ID Is Nothing Then Continue For
                    If ctlData.GetType().Name = GetType(Label).Name Then Continue For
                    If Not ctlData.ID.ToUpper.Contains(p_strDataElemName.ToUpper) Then Continue For

                    ctlDataElem = ctlData
                    Exit For
                Next

            End If

            If Not ctlDataElem Is Nothing Then Exit For
        Next

        Return ctlDataElem

    End Function

    ''' <summary>
    ''' Gets the ProjectSectionData
    ''' </summary>
    ''' <returns>TRProjectSectionData Object.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Function GetTRProjectSectionData() As DomainEntities.TRProjectSectionData Implements Presenter.ITestResultsView.GetTRProjectSectionData

        Dim objTRProjectData As TRProjectSectionData = Nothing

        If Not Me.IsVisible Then
            Return objTRProjectData
        End If

        objTRProjectData = New TRProjectSectionData()
        ReadTRSectionDataFromPanel(objTRProjectData.ToString, pnlProjectTest_Data)

        Return objTRProjectData

    End Function

    ''' <summary>
    ''' Get TR Product section data from the view controls
    ''' </summary>
    ''' <returns>TRProductSectionData Object.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Function GetTRProductSectionData() As DomainEntities.TRProductSectionData Implements Presenter.ITestResultsView.GetTRProductSectionData

        Dim objProductData As TRProductSectionData = Nothing
        If Not Me.IsVisible Then
            Return objProductData
        End If

        objProductData = New TRProductSectionData()
        ReadTRSectionDataFromPanel(objProductData.ToString, pnlProductData_Data)

        objProductData.OriginalProduct = TRProductSectionData_OriginalProduct

        Return objProductData

    End Function

    ''' <summary>
    ''' Get TR Measurement section data from the view controls
    ''' </summary>
    ''' <returns>TRMeasurementSectionData Object.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Function GetTRMeasurementSectionData() As DomainEntities.TRMeasurementSectionData Implements Presenter.ITestResultsView.GetTRMeasurementSectionData

        Dim objMeasurementData As TRMeasurementSectionData = Nothing
        If Not Me.IsVisible Then
            Return objMeasurementData
        End If

        objMeasurementData = New TRMeasurementSectionData()
        ReadTRSectionDataFromPanel(objMeasurementData.ToString, pnlMeasurement_Data)


        objMeasurementData.OriginalMeasure = TRMeasureSectionData_OriginalMeasure
        objMeasurementData.OriginalTreadwear = TRMeasureSectionData_OriginalTreadwear
        objMeasurementData.OriginalPlunger = TRMeasureSectionData_OriginalPlunger
        objMeasurementData.OriginalBeadUnSeat = TRMeasureSectionData_OriginalBeadUnSeat

        Return objMeasurementData

    End Function

    ''' <summary>
    ''' Get TR Endurance Test Before section data from the view controls
    ''' </summary>
    ''' <returns>TREnduranceTestGeneralBeforeSectionData Object.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Function GetTREnduranceBeforeSectionData() As DomainEntities.TREnduranceTestGeneralBeforeSectionData Implements Presenter.ITestResultsView.GetTREnduranceBeforeSectionData

        Dim objEnduranceBeforeSectionData As TREnduranceTestGeneralBeforeSectionData = Nothing
        If Not Me.IsVisible Then
            Return objEnduranceBeforeSectionData
        End If

        objEnduranceBeforeSectionData = New TREnduranceTestGeneralBeforeSectionData()
        ReadTRSectionDataFromPanel(objEnduranceBeforeSectionData.ToString, pnlEnduranceTestBefore_Data)

        Return objEnduranceBeforeSectionData

    End Function

    ''' <summary>
    ''' Get TR Endurance section data from the view controls
    ''' </summary>
    ''' <returns>TREnduranceSectionData Object.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Function GetTREnduranceSectionData() As DomainEntities.TREnduranceSectionData Implements Presenter.ITestResultsView.GetTREnduranceSectionData

        Dim objEnduranceSectionData As TREnduranceSectionData = Nothing
        If Not Me.IsVisible Then
            Return objEnduranceSectionData
        End If

        objEnduranceSectionData = New TREnduranceSectionData()
        ReadTRSectionDataFromPanel(objEnduranceSectionData.ToString, pnlEndurance_Data)


        objEnduranceSectionData.OriginalEndurance = TREnduranceSectionData_OriginalEndurance

        Return objEnduranceSectionData

    End Function

    ''' <summary>
    ''' Get TR Endurance test after section data from the view controls
    ''' </summary>
    ''' <returns>TREnduranceTestGeneralAfterSectionData Object.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Function GetTREnduranceAfterSectionData() As DomainEntities.TREnduranceTestGeneralAfterSectionData Implements Presenter.ITestResultsView.GetTREnduranceAfterSectionData

        Dim objEnduranceAfterSectionData As TREnduranceTestGeneralAfterSectionData = Nothing
        If Not Me.IsVisible Then
            Return objEnduranceAfterSectionData
        End If

        objEnduranceAfterSectionData = New TREnduranceTestGeneralAfterSectionData()
        ReadTRSectionDataFromPanel(objEnduranceAfterSectionData.ToString, pnlEnduranceTestAfter_Data)

        Return objEnduranceAfterSectionData

    End Function

    ''' <summary>
    ''' Get TR HighSpeed Test Before section data from the view controls
    ''' </summary>
    ''' <returns>TRHighSpeedTestGeneralBeforeSectionData Object.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Function GetTRHighSpeedBeforeSectionData() As DomainEntities.TRHighSpeedTestGeneralBeforeSectionData Implements Presenter.ITestResultsView.GetTRHighSpeedBeforeSectionData

        Dim objHighSpeedBeforeSectionData As TRHighSpeedTestGeneralBeforeSectionData = Nothing
        If Not Me.IsVisible Then
            Return objHighSpeedBeforeSectionData
        End If

        objHighSpeedBeforeSectionData = New TRHighSpeedTestGeneralBeforeSectionData()
        ReadTRSectionDataFromPanel(objHighSpeedBeforeSectionData.ToString, pnlHighSpeedTestBefore_Data)

        Return objHighSpeedBeforeSectionData

    End Function

    ''' <summary>
    ''' Get TR HighSpeed Test Before section data from the view controls
    ''' </summary>
    ''' <returns>TRHighSpeedSectionData Object.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Function GetTRHighSpeedSectionData() As DomainEntities.TRHighSpeedSectionData Implements Presenter.ITestResultsView.GetTRHighSpeedSectionData

        Dim objHighSpeedSectionData As TRHighSpeedSectionData = Nothing
        If Not Me.IsVisible Then
            Return objHighSpeedSectionData
        End If

        objHighSpeedSectionData = New TRHighSpeedSectionData()
        ReadTRSectionDataFromPanel(objHighSpeedSectionData.ToString, pnlHighSpeed_Data)

        objHighSpeedSectionData.OriginalHighSpeed = TRHighSpeedSectionData_OriginalHighSpeed

        Return objHighSpeedSectionData

    End Function

    ''' <summary>
    ''' Get TR HighSpeed test after section data from the view controls
    ''' </summary>
    ''' <returns>TRHighSpeedTestGeneralAfterSectionData Object.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Function GetTRHighSpeedAfterSectionData() As DomainEntities.TRHighSpeedTestGeneralAfterSectionData Implements Presenter.ITestResultsView.GetTRHighSpeedAfterSectionData

        Dim objHighSpeedAfterSectionData As TRHighSpeedTestGeneralAfterSectionData = Nothing
        If Not Me.IsVisible Then
            Return objHighSpeedAfterSectionData
        End If

        objHighSpeedAfterSectionData = New TRHighSpeedTestGeneralAfterSectionData()
        ReadTRSectionDataFromPanel(objHighSpeedAfterSectionData.ToString, pnlHighSpeedTestAfter_Data)

        Return objHighSpeedAfterSectionData

    End Function

    ''' <summary>
    ''' Get TR Sound Wet test result section data from the view controls
    ''' </summary>
    ''' <returns>TRSoundWetSectionData Object.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Public Function GetTRSoundWetSectionData() As DomainEntities.TRSoundWetSectionData Implements Presenter.ITestResultsView.GetTRSoundWetSectionData

        Dim objSoundWetSectionData As TRSoundWetSectionData = Nothing
        If Not Me.IsVisible Then
            Return objSoundWetSectionData
        End If

        objSoundWetSectionData = New TRSoundWetSectionData()
        ReadTRSectionDataFromPanel(objSoundWetSectionData.ToString, pnlSoundWetGrip_Data)

        objSoundWetSectionData.OriginalSound = TRSoundWetSectionData_OriginalSound
        objSoundWetSectionData.OriginalWetGrip = TRSoundWetSectionData_OriginalWetGrip

        Return objSoundWetSectionData

    End Function


    ''' <summary>
    ''' Event for validates and converts to number for PPV number 
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">ValueConvertEventArgs.</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Protected Sub ppvNumber_ValueConvert(ByVal sender As System.Object, ByVal e As Microsoft.Practices.EnterpriseLibrary.Validation.Integration.ValueConvertEventArgs)
        Const ErrorMessage As String = "number Please..."

        If String.IsNullOrEmpty(CStr(e.ValueToConvert)) Then
            e.ConvertedValue = 0
            Return
        End If

        Dim convertedValue As Integer

        If Int32.TryParse(CStr(e.ValueToConvert), convertedValue) Then
            e.ConvertedValue = convertedValue
        Else
            e.ConversionErrorMessage = ErrorMessage
        End If

    End Sub

    'Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.

    ''' <summary>
    ''' Event for gets requested tests 
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">EventArgs</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Private Sub btnGetRequestedTests_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetRequestedTests.Click
        Const ProjectNumText As String = "ProjectNum"
        Const TireNumText As String = "TireNum"
        Const OperationText As String = "Operation"
        Const TestSpecText As String = "TestSpec"
        Const TestSequenceText As String = "TestSequence"
        Const TestTypeText As String = "TestType"

        Dim dtbClientRequest As New DataTable

        Dim dcProjectNum As New DataColumn
        Dim dcTireNum As New DataColumn
        Dim dcOperation As New DataColumn
        Dim dcTestSpec As New DataColumn
        Dim dcTestSequence As New DataColumn
        Dim dcTestType As New DataColumn
        Dim drNewRow As DataRow

        dcProjectNum.ColumnName = ProjectNumText
        dcTireNum.ColumnName = TireNumText
        dcOperation.ColumnName = OperationText
        dcTestSpec.ColumnName = TestSpecText
        dcTestSequence.ColumnName = TestSequenceText
        dcTestType.ColumnName = TestTypeText

        dtbClientRequest.Columns.Add(dcProjectNum)
        dtbClientRequest.Columns.Add(dcTireNum)
        dtbClientRequest.Columns.Add(dcOperation)
        dtbClientRequest.Columns.Add(dcTestSpec)
        dtbClientRequest.Columns.Add(dcTestSequence)
        dtbClientRequest.Columns.Add(dcTestType)

        'Bead UnSeat
        If txtBeadUnSeatProjectNumber.Text.Length > 0 And txtBeadUnSeatTireNumber.Text.Length > 0 And txtBeadUnSeatTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item(ProjectNumText) = txtBeadUnSeatProjectNumber.Text
            drNewRow.Item(TireNumText) = txtBeadUnSeatTireNumber.Text
            drNewRow.Item(OperationText) = txtBeadUnSeatOperation.Text
            drNewRow.Item(TestSpecText) = txtBeadUnSeatTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Endurance
        If txtEnduranceProjectNumber.Text.Length > 0 And txtEnduranceTireNumber.Text.Length > 0 And txtEnduranceTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item(ProjectNumText) = txtEnduranceProjectNumber.Text
            drNewRow.Item(TireNumText) = txtEnduranceTireNumber.Text
            drNewRow.Item(OperationText) = txtEnduranceOperation.Text
            drNewRow.Item(TestSpecText) = txtEnduranceTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'High Speed
        If txtHighSpeedProjectNumber.Text.Length > 0 And txtHighSpeedTireNumber.Text.Length > 0 And txtHighSpeedTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item(ProjectNumText) = txtHighSpeedProjectNumber.Text
            drNewRow.Item(TireNumText) = txtHighSpeedTireNumber.Text
            drNewRow.Item(OperationText) = txtHighSpeedOperation.Text
            drNewRow.Item(TestSpecText) = txtHighSpeedTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Measure
        If txtMeasureProjectNumber.Text.Length > 0 And txtMeasureTireNumber.Text.Length > 0 And txtMeasureTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item(ProjectNumText) = txtMeasureProjectNumber.Text
            drNewRow.Item(TireNumText) = txtMeasureTireNumber.Text
            drNewRow.Item(OperationText) = txtMeasureOperation.Text
            drNewRow.Item(TestSpecText) = txtMeasureTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Plunger
        If txtPlungerProjectNumber.Text.Length > 0 And txtPlungerTireNumber.Text.Length > 0 And txtPlungerTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item(ProjectNumText) = txtPlungerProjectNumber.Text
            drNewRow.Item(TireNumText) = txtPlungerTireNumber.Text
            drNewRow.Item(OperationText) = txtPlungerOperation.Text
            drNewRow.Item(TestSpecText) = txtPlungerTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Sound
        If txtSoundProjectNumber.Text.Length > 0 And txtSoundTireNumber.Text.Length > 0 And txtSoundTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item(ProjectNumText) = txtSoundProjectNumber.Text
            drNewRow.Item(TireNumText) = txtSoundTireNumber.Text
            drNewRow.Item(OperationText) = txtSoundOperation.Text
            drNewRow.Item(TestSpecText) = txtSoundTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Treadwear
        If txtTreadwearProjectNumber.Text.Length > 0 And txtTreadwearTireNumber.Text.Length > 0 And txtTreadwearTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item(ProjectNumText) = txtTreadwearProjectNumber.Text
            drNewRow.Item(TireNumText) = txtTreadwearTireNumber.Text
            drNewRow.Item(OperationText) = txtTreadwearOperation.Text
            drNewRow.Item(TestSpecText) = txtTreadwearTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Wet Grip
        If txtWetGripProjectNumber.Text.Length > 0 And txtWetGripTireNumber.Text.Length > 0 And txtWetGripTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item(ProjectNumText) = txtWetGripProjectNumber.Text
            drNewRow.Item(TireNumText) = txtWetGripTireNumber.Text
            drNewRow.Item(OperationText) = txtWetGripOperation.Text
            drNewRow.Item(TestSpecText) = txtWetGripTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        ClientRequest = dtbClientRequest

        RaiseEvent GetRequestedTests()

    End Sub

    ''' <summary>
    ''' Event which captures selected Tire type Id.
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">EventArgs</param>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>   
    ''' </list>
    ''' </remarks>
    Protected Sub TireType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTireType.SelectedIndexChanged
        If (ddlTireType.SelectedIndex <> -1) Then
            txtTireId.Text = ddlTireType.SelectedValue.ToString()
        End If
    End Sub

#End Region

End Class
