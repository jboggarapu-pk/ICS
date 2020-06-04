Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Class contains Measurement properties for all certification types.
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
''' <para>10/18/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks> 
Public Class Measure

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

#Region "Members"

    ''' <summary>
    ''' variable to hold High Measure Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtMeasureId As Short = 0

    ''' <summary>
    ''' variable to hold Project Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strProjectNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intTireNumber As Integer = 0

    ''' <summary>
    ''' variable to hold Test Spec.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestSpec As String = String.Empty

    'Added m_strOperation  as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' variable to hold Operation.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strOperation As String = String.Empty

    ''' <summary>
    ''' variable to hold Completion Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteCompletionDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Inflation Pressure.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtInflationPressure As Short = 0

    ''' <summary>
    ''' variable to hold Mold Design.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMoldDesign As String = String.Empty

    ''' <summary>
    ''' variable to hold Rim Width.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngRimWidth As Single = 0

    ''' <summary>
    ''' variable to DOT Serial Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strDOTSerialNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Serial Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteSerialDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngDiameter As Single = 0

    ''' <summary>
    ''' variable to hold  Avg Section Width.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngAvgSectionWidth As Single = 0

    ''' <summary>
    ''' variable to hold Avg Overall Width.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngAvgOverallWidth As Single = 0

    ''' <summary>
    ''' variable to hold Max Overall Width.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngMaxOverallWidth As Single = 0

    ''' <summary>
    ''' variable to hold Min Size Factor.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngMinSizeFactor As Single = 0

    ''' <summary>
    ''' variable to hold Act Size Factor.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngActSizeFactor As Single = 0

    ''' <summary>
    ''' variable to hold End Time.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngEndTime As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Mount Time.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteMountTime As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Mount Temp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngMountTemp As Single = 0

    ''' <summary>
    ''' variable to hold Start Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMatlNum As String = String.Empty

    ''' <summary>
    ''' variable to hold Start SKU ID.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intSKUID As Integer = 0

    ''' <summary>
    ''' variable to hold Certification Type Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificationTypeId As Integer = 0

    ''' <summary>
    ''' variable to hold Certificate Number ID.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificateNumberID As Integer = 0

    'Key in Field
    ''' <summary>
    ''' variable to hold Start InfPressure.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtStartInfPressure As Short = 0

    ''' <summary>
    ''' variable to hold End InfPressure.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtEndInfPressure As Short = 0

    ''' <summary>
    ''' variable to hold Adjustment.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strAdjustment As String = String.Empty

    ''' <summary>
    ''' variable to hold Circumference.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngCircumference As Single = 0

    ''' <summary>
    ''' variable to hold Nominal Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngNominalDiameter As Single = 0

    ''' <summary>
    ''' variable to hold Nominal Width.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngNominalWidth As Single = 0

    ''' <summary>
    ''' variable to hold Nominal With PassYN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnNominalWithPassYN As Boolean = False

    ''' <summary>
    ''' variable to hold Nominal Width Difference.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngNominalWidthDifference As Single = 0

    ''' <summary>
    ''' variable to hold Nominal Width Tolerance.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngNominalWidthTolerance As Single = 0

    ''' <summary>
    ''' variable to hold Max Overall Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngMaxOverallDiameter As Single = 0

    ''' <summary>
    ''' variable to hold Min Overall Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngMinOverallDiameter As Single = 0

    ''' <summary>
    ''' variable to hold Overall Width PassYN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnOverallWidthPassYN As Boolean = True

    ''' <summary>
    ''' variable to hold overall Diameter PassYN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnOverallDiameterPassYN As Boolean = False

    ''' <summary>
    ''' variable to hold Diameter Difference.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngDiameterDifference As Single = 0

    ''' <summary>
    ''' variable to hold Diameter Tolerance.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngDiameterTolerance As Single = 0

    ''' <summary>
    ''' variable to hold Tensile Strength 1.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngTensileStrength1 As Single = 0

    ''' <summary>
    ''' variable to hold Tensile Strength 2.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngTensileStrength2 As Single = 1.2

    ''' <summary>
    ''' variable to hold Elongation1.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtElongation1 As Short = 0

    ''' <summary>
    ''' variable to hold Elongation2.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtElongation2 As Short = 300

    ''' <summary>
    ''' variable to hold Tensile Strength After Aging1.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngTensileStrengthAfterAging1 As Single = 0

    ''' <summary>
    ''' variable to hold Tensile Strength After Aging2.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngTensileStrengthAfterAging2 As Single = 80

    ''' <summary>
    ''' variable to hold Temperature Resistance Grading.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngTemperatureResistanceGrading As String = String.Empty

    ''' <summary>
    ''' variable to List of Measure details.
    ''' </summary>
    ''' <remarks></remarks>
    Public MeasureDetails As List(Of MeasureDetail) = New List(Of MeasureDetail)

    ''' <summary>
    ''' variable to hold Original Measure.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_objOriginalMeasure As Measure = Nothing

    ''' <summary>
    ''' variable to hold GT Spec Material Num.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strGTSpecMatlNum As String = String.Empty

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMFGWWYY As String = String.Empty

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
    ''' <para>10/18/2019</para>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MaterialNumber() As String
        Get
            Return m_strMatlNum
        End Get
        Set(ByVal value As String)
            m_strMatlNum = value
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SKUID() As Integer
        Get
            Return m_intSKUID
        End Get
        Set(ByVal value As Integer)
            m_intSKUID = value
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
    Public Property CertificationTypeId() As Integer
        Get
            Return m_intCertificationTypeId
        End Get
        Set(ByVal value As Integer)
            m_intCertificationTypeId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Number Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Certificate Number Id.</returns>
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
    Public Property CertificateNumberID() As Integer
        Get
            Return m_intCertificateNumberID
        End Get
        Set(ByVal value As Integer)
            m_intCertificateNumberID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Measure Id value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Measure Id.</returns>
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
    Public Property MeasureId() As Short
        Get
            Return m_srtMeasureId
        End Get
        Set(ByVal value As Short)
            m_srtMeasureId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets project Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Project Number.</returns>
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
    Public Property ProjectNumber() As String
        Get
            Return m_strProjectNumber
        End Get
        Set(ByVal value As String)
            m_strProjectNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Number value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Tire Number.</returns>
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
    Public Property TireNumber() As Integer
        Get
            Return m_intTireNumber
        End Get
        Set(ByVal value As Integer)
            m_intTireNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Spec value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Spec.</returns>
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
    Public Property TestSpec() As String
        Get
            Return m_strTestSpec
        End Get
        Set(ByVal value As String)
            m_strTestSpec = value
        End Set
    End Property

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Gets or sets Operation value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Operation.</returns>
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
    Public Property Operation() As String
        Get
            Return m_strOperation
        End Get
        Set(ByVal value As String)
            m_strOperation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Completion Date value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Completion Date.</returns>
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
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr)> _
   <PropertyComparisonValidator("CompletionDate", ComparisonOperator.GreaterThanEqual)> _
   Public Property CompletionDate() As DateTime
        Get
            Return m_dteCompletionDate
        End Get
        Set(ByVal value As DateTime)
            m_dteCompletionDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Inflation Pressure value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Inflation Pressure.</returns>
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
    Public Property InflationPressure() As Short
        Get
            Return m_srtInflationPressure
        End Get
        Set(ByVal value As Short)
            m_srtInflationPressure = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Mold Design value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Mold Design.</returns>
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
    Public Property MoldDesign() As String
        Get
            Return m_strMoldDesign
        End Get
        Set(ByVal value As String)
            m_strMoldDesign = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Rim Width value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Rim Width.</returns>
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
    Public Property RimWidth() As Single
        Get
            Return m_sngRimWidth
        End Get
        Set(ByVal value As Single)
            m_sngRimWidth = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Dot Serial Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Dot Serial Number.</returns>
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
    Public Property DotSerialNumber() As String
        Get
            Return m_strDOTSerialNumber
        End Get
        Set(ByVal value As String)
            m_strDOTSerialNumber = value
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SerialDate() As DateTime
        Get
            Return m_dteSerialDate
        End Get
        Set(ByVal value As DateTime)
            m_dteSerialDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Diameter value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Diameter.</returns>
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
    Public Property Diameter() As Single
        Get
            Return m_sngDiameter
        End Get
        Set(ByVal value As Single)
            m_sngDiameter = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Avg Section Width value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Avg Section Width.</returns>
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
    Public Property AvgSectionWidth() As Single
        Get
            Return m_sngAvgSectionWidth
        End Get
        Set(ByVal value As Single)
            m_sngAvgSectionWidth = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Avg Overall Width value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Avg Overall Width.</returns>
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
    Public Property AvgOverallWidth() As Single
        Get
            Return m_sngAvgOverallWidth
        End Get
        Set(ByVal value As Single)
            m_sngAvgOverallWidth = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Max Overall Width value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Max Overall Width.</returns>
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
    Public Property MaxOverallWidth() As Single
        Get
            Return m_sngMaxOverallWidth
        End Get
        Set(ByVal value As Single)
            m_sngMaxOverallWidth = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Min Size Factor value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Min Size Factor.</returns>
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
    Public Property MinSizeFactor() As Single
        Get
            Return m_sngMinSizeFactor
        End Get
        Set(ByVal value As Single)
            m_sngMinSizeFactor = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Act Size Factor value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Act Size Factor.</returns>
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
    Public Property ActSizeFactor() As Single
        Get
            Return m_sngActSizeFactor
        End Get
        Set(ByVal value As Single)
            m_sngActSizeFactor = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets End Time value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>End Time.</returns>
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
    Public Property EndTime() As DateTime
        Get
            Return m_sngEndTime
        End Get
        Set(ByVal value As DateTime)
            m_sngEndTime = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Mount Time value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Mount Time.</returns>
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
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    <PropertyComparisonValidator("MountTime", ComparisonOperator.GreaterThanEqual)> _
    Public Property MountTime() As DateTime
        Get
            Return m_dteMountTime
        End Get
        Set(ByVal value As DateTime)
            m_dteMountTime = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Mount Temp value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Mount Temp.</returns>
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
    Public Property MountTemp() As Single
        Get
            Return m_sngMountTemp
        End Get
        Set(ByVal value As Single)
            m_sngMountTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Original Measure value.
    ''' </summary>
    ''' <value>Measure object</value>
    ''' <returns>Original Measure.</returns>
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
    Public Property OriginalMeasure() As Measure
        Get
            Return m_objOriginalMeasure
        End Get
        Set(ByVal value As Measure)
            m_objOriginalMeasure = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Start InfPressure value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Start Infpressure.</returns>
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
    Public Property StartInfPressure() As Short
        Get
            Return m_srtStartInfPressure
        End Get
        Set(ByVal value As Short)
            m_srtStartInfPressure = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets End InfPressure value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>End InfPressure.</returns>
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
    Public Property EndInfPressure() As Short
        Get
            Return m_srtEndInfPressure
        End Get
        Set(ByVal value As Short)
            m_srtEndInfPressure = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Adjustment value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Adjustment.</returns>
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
    Public Property Adjustment() As String
        Get
            Return m_strAdjustment
        End Get
        Set(ByVal value As String)
            m_strAdjustment = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Circumference value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Circumference.</returns>
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
    Public Property Circumference() As Single
        Get
            Return m_sngCircumference
        End Get
        Set(ByVal value As Single)
            m_sngCircumference = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Nominal Diameter value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Nominal Diameter.</returns>
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
    Public Property NominalDiameter() As Single
        Get
            Return m_sngNominalDiameter
        End Get
        Set(ByVal value As Single)
            m_sngNominalDiameter = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Nominal Width value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Nominal Width.</returns>
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
    Public Property NominalWidth() As Single
        Get
            Return m_sngNominalWidth
        End Get
        Set(ByVal value As Single)
            m_sngNominalWidth = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Nominal Width PassYN value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Nominal Width PassYN.</returns>
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
    Public Property NominalWidthPassYN() As Boolean
        Get
            Return m_blnNominalWithPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnNominalWithPassYN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Nominal Width Difference value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Nominal Width Difference.</returns>
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
    Public Property NominalWidthDifference() As Single
        Get
            Return m_sngNominalWidthDifference
        End Get
        Set(ByVal value As Single)
            m_sngNominalWidthDifference = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Nominal Width Tolerance value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Nominal Width Tolerance.</returns>
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
    Public Property NominalWidthTolerance() As Single
        Get
            Return m_sngNominalWidthTolerance
        End Get
        Set(ByVal value As Single)
            m_sngNominalWidthTolerance = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Max Overall Diameter value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Max Overall Diameter.</returns>
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
    Public Property MaxOverallDiameter() As Single
        Get
            Return m_sngMaxOverallDiameter
        End Get
        Set(ByVal value As Single)
            m_sngMaxOverallDiameter = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Min Overall Diameter value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Min Overall Diameter.</returns>
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
    Public Property MinOverallDiameter() As Single
        Get
            Return m_sngMinOverallDiameter
        End Get
        Set(ByVal value As Single)
            m_sngMinOverallDiameter = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Overall width PassYN value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Overall Width PassYN.</returns>
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
    Public Property OverallWidthPassYN() As Boolean
        Get
            Return m_blnOverallWidthPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnOverallWidthPassYN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Overall Diameter PassYN value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Overall Diameter PassYN.</returns>
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
    Public Property OverallDiameterPassYN() As Boolean
        Get
            Return m_blnOverallDiameterPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnOverallDiameterPassYN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Diameter Difference value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Diameter Difference.</returns>
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
    Public Property DiameterDifference() As Single
        Get
            Return m_sngDiameterDifference
        End Get
        Set(ByVal value As Single)
            m_sngDiameterDifference = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Diameter Tolerance value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Diameter Tolerance.</returns>
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
    Public Property DiameterTolerance() As Single
        Get
            Return m_sngDiameterTolerance
        End Get
        Set(ByVal value As Single)
            m_sngDiameterTolerance = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets tensile Strength1 value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Tensile Strength1.</returns>
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
    Public Property TensileStrength1() As Single
        Get
            Return m_sngTensileStrength1
        End Get
        Set(ByVal value As Single)
            m_sngTensileStrength1 = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tensile Strength2 value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Tensile Strength2.</returns>
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
    Public Property TensileStrength2() As Single
        Get
            Return m_sngTensileStrength2
        End Get
        Set(ByVal value As Single)
            m_sngTensileStrength2 = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Elongation1 value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Elongation1.</returns>
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
    Public Property Elongation1() As Short
        Get
            Return m_srtElongation1
        End Get
        Set(ByVal value As Short)
            m_srtElongation1 = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Elongation2 value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Elongation2.</returns>
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
    Public Property Elongation2() As Short
        Get
            Return m_srtElongation2
        End Get
        Set(ByVal value As Short)
            m_srtElongation2 = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tensile Strength After Aging1 value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Tensile Strength After Aging1.</returns>
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
    Public Property TensileStrengthAfterAging1() As Single
        Get
            Return m_sngTensileStrengthAfterAging1
        End Get
        Set(ByVal value As Single)
            m_sngTensileStrengthAfterAging1 = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tensile Strength After Aging2 value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Tensile Strength After Aging2.</returns>
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
    Public Property TensileStrengthAfterAging2() As Single
        Get
            Return m_sngTensileStrengthAfterAging2
        End Get
        Set(ByVal value As Single)
            m_sngTensileStrengthAfterAging2 = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Temperature Resistance Grading value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Temperature Resistance Grading.</returns>
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
    Public Property TemperatureResistanceGrading() As String
        Get
            Return m_sngTemperatureResistanceGrading
        End Get
        Set(ByVal value As String)
            m_sngTemperatureResistanceGrading = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets GT Spec Material Number value.
    ''' </summary>
    ''' <value>string</value>
    ''' <returns>GT Spec Material Number.</returns>
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
    Public Property GTSpecMaterialNumber() As String
        Get
            Return m_strGTSpecMatlNum
        End Get
        Set(ByVal value As String)
            m_strGTSpecMatlNum = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets MFGWWYYvalue.
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MFGWWYY() As String
        Get
            Return m_strMFGWWYY
        End Get
        Set(ByVal value As String)
            m_strMFGWWYY = value
        End Set
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Method  for Do Validate with default (anonymous) rule set.
    ''' </summary>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Function DoValidate() As Boolean

        Dim blnValid As Boolean = False
        Try
            Dim results As ValidationResults = Validation.Validate(Of Measure)(Me)
            blnValid = results.IsValid
            'For Each vr As ValidationResult In results
            '    Dim str As String = vr.Message
            'Next
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Function DoValidate(ByVal p_strRuleSetName As String) As Boolean

        Dim blnValid As Boolean = False
        Try
            Dim results As ValidationResults = Validation.Validate(Of Measure)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region
End Class
