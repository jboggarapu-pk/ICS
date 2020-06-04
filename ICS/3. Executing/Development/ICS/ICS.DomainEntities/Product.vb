Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Class contains Product properties for all certification types.
''' </summary>
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
''' <term>Sujitha</term>
''' <description>
''' <para>10/28/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks> 

<HasSelfValidation()> _
Public Class Product

    ' Changed sku to material number, ppn to tpn , nprid to psn and added brand and brand line instead of brand code as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"

    ''' <summary>
    ''' variable to hold SKU ID.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_iSKUID As Integer = 0

    ''' <summary>
    ''' variable to hold Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMatlNum As String = String.Empty

    ''' <summary>
    ''' variable to hold Brand Desc.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strBrandDesc As String = String.Empty

    ''' <summary>
    ''' variable to hold Serial Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSerialDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold DOT Serial Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strDOTSerialNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Brand.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strBrand As String = String.Empty

    ''' <summary>
    ''' variable to hold Brand Line.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strBrandLine As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Type Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intTireTypeId As Integer = 0

    ''' <summary>
    ''' variable to hold PSN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strPSN As String = String.Empty

    ''' <summary>
    ''' variable to hold Discontinue Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteDiscontinuedDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Spec Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSpecNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Size Stamp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTireSizeStamp As String = String.Empty

    ''' <summary>
    ''' variable to hold Speed rating.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSpeedRating As String = String.Empty

    ''' <summary>
    ''' variable to hold SingLoad Index.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSingLoadIndex As String = String.Empty

    ''' <summary>
    ''' variable to hold Dual Load Index.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strDualLoadIndex As String = String.Empty

    ''' <summary>
    ''' variable to hold Bias Belted Redial.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strBiasBeltedRadial As String = String.Empty

    ''' <summary>
    ''' variable to hold TubelessYN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnTubelessYN As Boolean = False

    ''' <summary>
    ''' variable to hold ReinforcedYN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnReinforcedYN As Boolean = False

    ''' <summary>
    ''' variable to hold Extra LoadYN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnExtraLoadYN As Boolean = False

    ''' <summary>
    ''' variable to hold UTQG TreadWear.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strUTQGTreadwear As String = String.Empty

    ''' <summary>
    ''' variable to hold UTQGTraction.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strUTQGTraction As String = String.Empty

    ''' <summary>
    ''' variable to hold UTQGTemp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strUTQGTemp As String = String.Empty

    ''' <summary>
    ''' variable to hold Rim Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngRimDiameter As Single = 0

    ''' <summary>
    ''' variable to hold Load Range.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strLoadRange As String = String.Empty

    ''' <summary>
    ''' variable to hold MeaRim Width.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngMeaRimWidth As Single = 0

    ''' <summary>
    ''' variable to hold Regroovable Ind.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strRegroovableInd As Boolean = False

    ''' <summary>
    ''' variable to hold Plant Produced.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strPlantProduced As String = String.Empty

    ''' <summary>
    ''' variable to hold Most Recent Test Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteMostRecentTestDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold IMark.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strIMark As String = String.Empty

    ''' <summary>
    ''' variable to hold TPN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTPN As String = String.Empty

    'Key In fields
    ''' <summary>
    ''' variable to hold Informe Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strInformeNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Fecha Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteFechaDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Tread Pattern.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTreadPattern As String = String.Empty

    ''' <summary>
    ''' variable to hold Special Protective Band.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSpecialProtectiveBand As String = String.Empty

    ''' <summary>
    ''' variable to hold Nominal Tire Width.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strNominalTireWidth As String = String.Empty

    ''' <summary>
    ''' variable to hold Aspect Ratio.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strAspectRatio As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Indicator.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTreadwearIndicator As String = String.Empty

    ''' <summary>
    ''' variable to hold Name of Manufacture.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strNameOfManufacture As String = "Cooper Tire & Rubber Co."

    ''' <summary>
    ''' variable to hold Family.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strFamily As String = String.Empty

    ''' <summary>
    ''' variable to hold Original Product.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_objOriginalProduct As Product = Nothing

    ''' <summary>
    ''' variable to hold Date of Manfacture WWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strDateOfManufactureWWYY As String = String.Empty

    ''' <summary>
    ''' variable to hold Type Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTypeId As String = String.Empty

    ''' <summary>
    ''' variable to hold Mud Plus Snow.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnMudPlusSnow As Boolean = False

    ''' <summary>
    ''' variable to hold Severe Weather Indicator.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnSevereWeatherIndicator As Boolean = False
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub New()

    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets or sets SKU Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>SKU Id.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SKUID() As Integer
        Get
            Return m_iSKUID
        End Get
        Set(ByVal value As Integer)
            m_iSKUID = value
        End Set
    End Property

    ' Changed validation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Gets or sets material Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Plunger Id.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <NotNullValidator()> _
    <StringLengthValidator(11, 18, MessageTemplate:="Material Number must be 11-18 characters")> _
    Public Property MaterialNumber() As String
        Get
            Return m_strMatlNum
        End Get
        Set(ByVal value As String)
            m_strMatlNum = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Brand Desc value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Brand Desc.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property BrandDesc() As String
        Get
            Return m_strBrandDesc
        End Get
        Set(ByVal value As String)
            m_strBrandDesc = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Serial Date value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Serial Date.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SerialDate() As DateTime
        Get
            Return m_strSerialDate
        End Get
        Set(ByVal value As DateTime)
            m_strSerialDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets DOT Serial Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>DOT Serial Number.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property DOTSerialNumber() As String
        Get
            Return m_strDOTSerialNumber
        End Get
        Set(ByVal value As String)
            m_strDOTSerialNumber = value
        End Set
    End Property

    ' Added brand property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Gets or sets Brand value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Brand.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Brand() As String
        Get
            Return m_strBrand
        End Get
        Set(ByVal value As String)
            m_strBrand = value
        End Set
    End Property

    ' Added brand line property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Gets or sets Brand Line value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Brand Line.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property BrandLine() As String
        Get
            Return m_strBrandLine
        End Get
        Set(ByVal value As String)
            m_strBrandLine = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Type ID value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Tire Type Id.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <NotNullValidator()> _
    Public Property TireTypeId() As Integer
        Get
            Return m_intTireTypeId
        End Get
        Set(ByVal value As Integer)
            m_intTireTypeId = value
        End Set
    End Property

    ' Changed nprid to psn as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Gets or sets PSN value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>PSN.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property PSN() As String
        Get
            Return m_strPSN
        End Get
        Set(ByVal value As String)
            m_strPSN = value
        End Set
    End Property

    ' Changed ppn to tpn as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Gets or sets TPN value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>TPN.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TPN() As String
        Get
            Return m_strTPN
        End Get
        Set(ByVal value As String)
            m_strTPN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Size Stamp value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tire Size Stamp.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <NotNullValidator()> _
    <StringLengthValidator(5, 25, MessageTemplate:="Tire Designation must be 5-25 characters")> _
    Public Property TireSizeStamp() As String
        Get
            Return m_strTireSizeStamp.Trim()
        End Get
        Set(ByVal value As String)
            m_strTireSizeStamp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Discontinued Date value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Discontinued Date.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property DiscontinuedDate() As DateTime
        Get
            Return m_dteDiscontinuedDate
        End Get
        Set(ByVal value As DateTime)
            m_dteDiscontinuedDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Spec Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Spec Number.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SpecNumber() As String
        Get
            Return m_strSpecNumber
        End Get
        Set(ByVal value As String)
            m_strSpecNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Speed Rating value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Speed Rating.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SpeedRating() As String
        Get
            Return m_strSpeedRating
        End Get
        Set(ByVal value As String)
            m_strSpeedRating = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Singload Index value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Sing Load Index.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SingLoadIndex() As String
        Get
            Return m_strSingLoadIndex
        End Get
        Set(ByVal value As String)
            m_strSingLoadIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Dual Load Index value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Dual Load Index.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property DualLoadIndex() As String
        Get
            Return m_strDualLoadIndex
        End Get
        Set(ByVal value As String)
            m_strDualLoadIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Bias Belted Radial value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Bias Belted Radial.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property BiasBeltedRadial() As String
        Get
            Return m_strBiasBeltedRadial
        End Get
        Set(ByVal value As String)
            m_strBiasBeltedRadial = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets TubelessYN value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>TubelessYN.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TubelessYN() As Boolean
        Get
            Return m_blnTubelessYN
        End Get
        Set(ByVal value As Boolean)
            m_blnTubelessYN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Reinforced YN value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>ReinforcedYN.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ReinforcedYN() As Boolean
        Get
            Return m_blnReinforcedYN
        End Get
        Set(ByVal value As Boolean)
            m_blnReinforcedYN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Extra LoadYN value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Extra LoadYN.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ExtraLoadYN() As Boolean
        Get
            Return m_blnExtraLoadYN
        End Get
        Set(ByVal value As Boolean)
            m_blnExtraLoadYN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets UTQG Tread Wear value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>UTQG Treadwear.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property UTQGTreadwear() As String
        Get
            Return m_strUTQGTreadwear
        End Get
        Set(ByVal value As String)
            m_strUTQGTreadwear = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets UTQG Traction value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>UTQG Traction.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property UTQGTraction() As String
        Get
            Return m_strUTQGTraction
        End Get
        Set(ByVal value As String)
            m_strUTQGTraction = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets UTQGTemp value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>UTQGTemp.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property UTQGTemp() As String
        Get
            Return m_strUTQGTemp
        End Get
        Set(ByVal value As String)
            m_strUTQGTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Load range value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Load Range.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property LoadRange() As String
        Get
            Return m_strLoadRange
        End Get
        Set(ByVal value As String)
            m_strLoadRange = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Mea Rim Width value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>MeaRimWidth.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MeaRimWidth() As Single
        Get
            Return m_sngMeaRimWidth
        End Get
        Set(ByVal value As Single)
            m_sngMeaRimWidth = value
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets Regroovable Ind value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Regroovable Ind.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property RegroovableInd() As Boolean
        Get
            Return m_strRegroovableInd
        End Get
        Set(ByVal value As Boolean)
            m_strRegroovableInd = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Plant Produced value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>plant produced.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property PlantProduced() As String
        Get
            Return m_strPlantProduced
        End Get
        Set(ByVal value As String)
            m_strPlantProduced = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Most Recent Test Date value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Most Recent Test Date.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MostRecentTestDate() As DateTime
        Get
            Return m_dteMostRecentTestDate
        End Get
        Set(ByVal value As DateTime)
            m_dteMostRecentTestDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets IMark value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>IMark.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property IMark() As String
        Get
            Return m_strIMark
        End Get
        Set(ByVal value As String)
            m_strIMark = value
        End Set
    End Property

    '<RangeValidator(0, RangeBoundaryType.Inclusive, 9999, RangeBoundaryType.Inclusive, MessageTemplate:="Must be in a range 0 - 9999")> _
    ''' <summary>
    ''' Gets or sets Rim Diameter value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Rim Diameter.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property RimDiameter() As Single
        Get
            Return m_sngRimDiameter
        End Get
        Set(ByVal value As Single)
            m_sngRimDiameter = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Original Product value.
    ''' </summary>
    ''' <value>Product object</value>
    ''' <returns>Original Product.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property OriginalProduct() As Product
        Get
            Return m_objOriginalProduct
        End Get
        Set(ByVal value As Product)
            m_objOriginalProduct = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Informe Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>InformeNumber.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property InformeNumber() As String
        Get
            Return m_strInformeNumber
        End Get
        Set(ByVal value As String)
            m_strInformeNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Fecha Date value.
    ''' </summary>
    ''' <value>Datetime</value>
    ''' <returns>Fecha Date.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property FechaDate() As DateTime
        Get
            Return m_dteFechaDate
        End Get
        Set(ByVal value As DateTime)
            m_dteFechaDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tread Pattern value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tread Pattern.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TreadPattern() As String
        Get
            Return m_strTreadPattern
        End Get
        Set(ByVal value As String)
            m_strTreadPattern = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Special protective Band value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Special protective Band.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SpecialProtectiveBand() As String
        Get
            Return m_strSpecialProtectiveBand
        End Get
        Set(ByVal value As String)
            m_strSpecialProtectiveBand = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Nominal tire Width value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Nominal Tire Width.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property NominalTireWidth() As String
        Get
            Return m_strNominalTireWidth
        End Get
        Set(ByVal value As String)
            m_strNominalTireWidth = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Aspect Ratio value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Aspect Ratio.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property AspectRatio() As String
        Get
            Return m_strAspectRatio
        End Get
        Set(ByVal value As String)
            m_strAspectRatio = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Treadwear Indicator value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>TreasWear Indicator.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TreadwearIndicator() As String
        Get
            Return m_strTreadwearIndicator
        End Get
        Set(ByVal value As String)
            m_strTreadwearIndicator = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Name of Manufacture value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Name of Manufacture.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property NameOfManufacture() As String
        Get
            Return m_strNameOfManufacture
        End Get
        Set(ByVal value As String)
            m_strNameOfManufacture = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Family value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Family.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Family() As String
        Get
            Return m_strFamily
        End Get
        Set(ByVal value As String)
            m_strFamily = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets MFGWWYY value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>MFGWWYY.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <RegexValidator("^\s*$|^\d{4}$", MessageTemplate:="Manufacture Date(WWYY) must be a 4 digit numeric number")> _
    Public Property MFGWWYY() As String
        Get
            Return m_strDateOfManufactureWWYY
        End Get
        Set(ByVal value As String)
            m_strDateOfManufactureWWYY = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Id value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tire Id.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TireId() As String
        Get
            Return m_strTypeId
        End Get
        Set(ByVal value As String)
            m_strTypeId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets MudSnow value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Mud Snow.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MudSnow() As Boolean
        Get
            Return m_blnMudPlusSnow
        End Get
        Set(ByVal value As Boolean)
            m_blnMudPlusSnow = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Severe Weather Indicator value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Severe Weather Indicator.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SevereWeatherIndicator() As Boolean
        Get
            Return m_blnSevereWeatherIndicator
        End Get
        Set(ByVal value As Boolean)
            m_blnSevereWeatherIndicator = value
        End Set
    End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Method  for Validate with default (anonymous) rule set
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Function DoValidate() As Boolean

        Dim blnValid As Boolean = False
        Try
            Dim results As ValidationResults = Validation.Validate(Of Product)(Me)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

    ''' <summary>
    ''' Method  for Do Validate with specific rule set.
    ''' </summary>
    ''' <param name="p_strRuleSetName">RuleSet Name</param>
    ''' <returns>Boolean.</returns>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Function DoValidate(ByVal p_strRuleSetName As String) As Boolean

        Dim blnValid As Boolean = False
        Try
            Dim results As ValidationResults = Validation.Validate(Of Product)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function
#End Region

End Class
