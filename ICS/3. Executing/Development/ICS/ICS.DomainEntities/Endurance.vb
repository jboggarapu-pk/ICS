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
''' <para>10/17/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class Endurance

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

#Region "Members"
    ''' <summary>
    ''' variable to hold Endurance Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtEnduranceId As Short = 0

    ''' <summary>
    ''' variable to hold Project Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strProjectNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtTireNumber As Integer = 0

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
    ''' variable to hold Dot Serial Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strDotSerialNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold m_dtePrecond Start Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dtePrecondStartDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold m_srtPrecond Start Temp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtPrecondStartTemp As Short = 0

    ''' <summary>
    ''' variable to hold Rim Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngRimDiameter As Single = 0

    ''' <summary>
    ''' variable to hold Rim Width.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngRimWidth As Single = 0

    ''' <summary>
    ''' variable to hold m_dtePrecond End Date .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dtePrecondEndDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold m_srtPrecond End Temp .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtPrecondEndTemp As Short = 0

    ''' <summary>
    ''' variable to hold Inflation Pressure.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtInflationPressure As Short = 0

    ''' <summary>
    ''' variable to hold Before Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngBeforeDiameter As Single = 0

    ''' <summary>
    ''' variable to hold After Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngAfterDiameter As Single = 0

    ''' <summary>
    ''' variable to hold Before Inflation.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtBeforeInflation As Single = 0

    ''' <summary>
    ''' variable to hold After Inflation.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtAfterInflation As Single = 0

    ''' <summary>
    ''' variable to hold Wheel Position.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtWheelPosition As Short = 0

    ''' <summary>
    ''' variable to hold Wheel Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtWheelNumber As Short = 0

    ''' <summary>
    ''' variable to hold Final temp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtFinalTemp As Short = 0

    ''' <summary>
    ''' variable to hold Final Distance.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngFinalDistance As Single = 0

    ''' <summary>
    ''' variable to hold Final Inflation.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtFinalInflation As Single = 0

    ''' <summary>
    ''' variable to hold Low Start Inflation.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtLowInfStartInflation As Single = 0

    ''' <summary>
    ''' variable to hold Low End Inflation.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtLowInfEndInflation As Single = 0

    ''' <summary>
    ''' variable to hold Low End Temp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtLowInfEndTemp As Single = 0

    ''' <summary>
    ''' variable to hold Postcond Start Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dtePostcondStartDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Postcond End Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dtePostcondEndDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Postcond End Temp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtPostcondEndTemp As Short = 0

    ''' <summary>
    ''' variable to hold PassYN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnPassYN As Boolean = False

    ''' <summary>
    ''' variable to hold Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMatlNum As String = String.Empty

    ''' <summary>
    ''' variable to hold SKU ID.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intSKUID As Integer = 0

    ''' <summary>
    ''' variable to hold Certification Type Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_iCertificationTypeId As Integer = 0

    ''' <summary>
    ''' variable to hold Certification Number Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificateNumberID As Integer = 0

    ''' <summary>
    ''' variable to hold Serial Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteSerialDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold m_sngPrecond Time.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngPrecondTime As Single = 0

    ''' <summary>
    ''' variable to hold m_sngPostcond Time .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngPostcondTime As Single = 0

    ''' <summary>
    ''' variable to hold m_sngPrecond Temp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngPrecondTemp As Single = 0

    ''' <summary>
    ''' variable to hold Result Pass Fail.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnResultPassFail As Boolean = False

    ''' <summary>
    ''' variable to hold Diameter Test Drum.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngDiameterTestDrum As Single = 1.707

    ''' <summary>
    ''' variable to hold Inflation Pressure Read justed.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtInflationPressureReadjusted As Short = 0

    ''' <summary>
    ''' variable to hold Circumference Before Testing.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngCircumferenceBeforeTesting As Single = 0

    ''' <summary>
    ''' variable to hold Endurance XHour.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngEnduranceXHour As Single = 0

    ''' <summary>
    ''' variable to hold Endurance Test PassYN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnEnduranceTestPassYN As Boolean = False

    ''' <summary>
    ''' variable to hold Possible Failures Found.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strPossibleFailuresFound As String = String.Empty

    ''' <summary>
    ''' variable to hold Circumference After Testing.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngCircumferenceAfterTesting As Single = 0

    ''' <summary>
    ''' variable to hold Outer Diameter Difference.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngOuterDiameterDifference As Single = 0

    ''' <summary>
    ''' variable to hold Outer Diameter Tolerance.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngOuterDiameterTolerance As Single = 0

    ''' <summary>
    ''' variable to hold SerieNOM.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSerieNOM As String = String.Empty

    ''' <summary>
    ''' variable to hold Final Judgement.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnFinalJudgement As Boolean = False

    ''' <summary>
    ''' variable to hold Approver.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strApprover As String = String.Empty

    ''' <summary>
    ''' variable to hold Endurance Details.
    ''' </summary>
    ''' <remarks></remarks>
    Public EnduranceDetails As List(Of EnduranceDetail) = New List(Of EnduranceDetail)

    ''' <summary>
    ''' variable to hold Original Endurance.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_objOriginalEndurance As Endurance = Nothing

    ''' <summary>
    ''' variable to hold GTSpecMaterial Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strGTSpecMaterialNumber As String = String.Empty

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
    ''' <para>10/17/2019</para>
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
    ''' Gets or sets Endurance ID value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Endurance ID.</returns>
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
    Public Property END_ID() As Short
        Get
            Return m_srtEnduranceId
        End Get
        Set(ByVal value As Short)
            m_srtEnduranceId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Project Number value.
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
    ''' <para>10/17/2019</para>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TireNumber() As Integer
        Get
            Return m_srtTireNumber
        End Get
        Set(ByVal value As Integer)
            m_srtTireNumber = value
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
    ''' <para>10/17/2019</para>
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
    ''' Gets or sets Operation Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Operation value.</returns>
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
    Public Property Operation() As String
        Get
            Return m_strOperation
        End Get
        Set(ByVal value As String)
            m_strOperation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Comepletion Date Value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Completion Date value.</returns>
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
    Public Property CompletionDate() As DateTime
        Get
            Return m_dteCompletionDate
        End Get
        Set(ByVal value As DateTime)
            m_dteCompletionDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Dot Serial Number Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Dot Serial Number value.</returns>
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
    Public Property DotSerialNumber() As String
        Get
            Return m_strDotSerialNumber
        End Get
        Set(ByVal value As String)
            m_strDotSerialNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Precond StartDate Value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Precond StartDate value.</returns>
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
    Public Property PrecondStartDate() As DateTime
        Get
            Return m_dtePrecondStartDate
        End Get
        Set(ByVal value As DateTime)
            m_dtePrecondStartDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Precond StartTemp Value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Precond StartTemp value.</returns>
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
    Public Property PrecondStartTemp() As Short
        Get
            Return m_srtPrecondStartTemp
        End Get
        Set(ByVal value As Short)
            m_srtPrecondStartTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets RimDiameter Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>RimDiameter value.</returns>
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
    Public Property RimDiameter() As Single
        Get
            Return m_sngRimDiameter
        End Get
        Set(ByVal value As Single)
            m_sngRimDiameter = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets RimWidth Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>RimWidth value.</returns>
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
    Public Property RimWidth() As Single
        Get
            Return m_sngRimWidth
        End Get
        Set(ByVal value As Single)
            m_sngRimWidth = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Precond End Date Value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Precond End Date value.</returns>
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
    Public Property PrecondEndDate() As DateTime
        Get
            Return m_dtePrecondEndDate
        End Get
        Set(ByVal value As DateTime)
            m_dtePrecondEndDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Precond End temp Value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Precond End temp value.</returns>
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
    Public Property PrecondEndTemp() As Short
        Get
            Return m_srtPrecondEndTemp
        End Get
        Set(ByVal value As Short)
            m_srtPrecondEndTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Inflation Pressure Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Inflation Pressure value.</returns>
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
    Public Property InflationPressure() As Single
        Get
            Return m_srtInflationPressure
        End Get
        Set(ByVal value As Single)
            m_srtInflationPressure = CShort(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Low InfEnd Inflation Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Low InfEnd Inflation value.</returns>
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
    Public Property LowInfEndInflation() As Single
        Get
            Return m_srtLowInfEndInflation
        End Get
        Set(ByVal value As Single)
            m_srtLowInfEndInflation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Low InfStart Inflation Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Low InfStart Inflation value.</returns>
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
    Public Property LowInfStartInflation() As Single
        Get
            Return m_srtLowInfStartInflation
        End Get
        Set(ByVal value As Single)
            m_srtLowInfStartInflation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Low InfEnd Temp Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Low InfEnd Temp Value.</returns>
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
    Public Property LowInfEndTemp() As Single
        Get
            Return m_srtLowInfEndTemp
        End Get
        Set(ByVal value As Single)
            m_srtLowInfEndTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Before Diameter Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Before Diameter Value.</returns>
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
    Public Property BeforeDiameter() As Single
        Get
            Return m_sngBeforeDiameter
        End Get
        Set(ByVal value As Single)
            m_sngBeforeDiameter = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets After Diameter Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>After Diameter value.</returns>
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
    Public Property AfterDiameter() As Single
        Get
            Return m_sngAfterDiameter
        End Get
        Set(ByVal value As Single)
            m_sngAfterDiameter = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Before Inflation Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Before Inflation value.</returns>
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
    Public Property BeforeInflation() As Single
        Get
            Return m_srtBeforeInflation
        End Get
        Set(ByVal value As Single)
            m_srtBeforeInflation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets After Inflation Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>After Inflation value.</returns>
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
    Public Property AfterInflation() As Single
        Get
            Return m_srtAfterInflation
        End Get
        Set(ByVal value As Single)
            m_srtAfterInflation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Wheel Position Value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns> Wheel Position value.</returns>
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
    Public Property WheelPosition() As Short
        Get
            Return m_srtWheelPosition
        End Get
        Set(ByVal value As Short)
            m_srtWheelPosition = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Wheel Number Value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns> Wheel Number value.</returns>
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
    Public Property WheelNumber() As Short
        Get
            Return m_srtWheelNumber
        End Get
        Set(ByVal value As Short)
            m_srtWheelNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Final Temp Value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Final Temp value.</returns>
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
    Public Property FinalTemp() As Short
        Get
            Return m_srtFinalTemp
        End Get
        Set(ByVal value As Short)
            m_srtFinalTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Final Distance Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Final Distance value.</returns>
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
    Public Property FinalDistance() As Single
        Get
            Return m_sngFinalDistance
        End Get
        Set(ByVal value As Single)
            m_sngFinalDistance = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Final Inflation Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Final Inflation value.</returns>
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
    Public Property FinalInflation() As Single
        Get
            Return m_srtFinalInflation
        End Get
        Set(ByVal value As Single)
            m_srtFinalInflation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Postcond Start Date Value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Post cond Start Date value.</returns>
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
    Public Property PostcondStartDate() As DateTime
        Get
            Return m_dtePostcondStartDate
        End Get
        Set(ByVal value As DateTime)
            m_dtePostcondStartDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Postcond End Date Value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Post cond End value.</returns>
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
    Public Property PostcondEndDate() As DateTime
        Get
            Return m_dtePostcondEndDate
        End Get
        Set(ByVal value As DateTime)
            m_dtePostcondEndDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Postcond End Temp Value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Post cond End Temp value.</returns>
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
    Public Property PostcondEndTemp() As Short
        Get
            Return m_srtPostcondEndTemp
        End Get
        Set(ByVal value As Short)
            m_srtPostcondEndTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets PassYN Value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>PassYN value.</returns>
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
    Public Property PassYN() As Boolean
        Get
            Return m_blnPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnPassYN = value
        End Set
    End Property

    ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Gets or sets Material Number Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>material Number value.</returns>
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
    Public Property MaterialNumber() As String
        Get
            Return m_strMatlNum
        End Get
        Set(ByVal value As String)
            m_strMatlNum = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets SKU ID Value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>SKUID value.</returns>
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
    Public Property SKUID() As Integer
        Get
            Return m_intSKUID
        End Get
        Set(ByVal value As Integer)
            m_intSKUID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certification Type ID Value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Certification Type ID value.</returns>
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
    Public Property CertificationTypeId() As Integer
        Get
            Return m_iCertificationTypeId
        End Get
        Set(ByVal value As Integer)
            m_iCertificationTypeId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certification Number ID Value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Certification Number ID value.</returns>
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
    Public Property CertificateNumberID() As Integer
        Get
            Return m_intCertificateNumberID
        End Get
        Set(ByVal value As Integer)
            m_intCertificateNumberID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Serial Date Value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Serial Date value.</returns>
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
    Public Property SerialDate() As DateTime
        Get
            Return m_dteSerialDate
        End Get
        Set(ByVal value As DateTime)
            m_dteSerialDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Precond Time Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Precond Time value.</returns>
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
    Public Property PrecondTime() As Single
        Get
            Return m_sngPrecondTime
        End Get
        Set(ByVal value As Single)
            m_sngPrecondTime = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets PostcondTime Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Postcond Time value.</returns>
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
    Public Property PostcondTime() As Single
        Get
            Return m_sngPostcondTime
        End Get
        Set(ByVal value As Single)
            m_sngPostcondTime = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Original Endurance Value.
    ''' </summary>
    ''' <value>Endurance object</value>
    ''' <returns>Original Endurance value.</returns>
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
    Public Property OriginalEndurance() As Endurance
        Get
            Return m_objOriginalEndurance
        End Get
        Set(ByVal value As Endurance)
            m_objOriginalEndurance = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Result PassFail Value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Result Pass Fail value.</returns>
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
    Public Property ResultPassFail() As Boolean
        Get
            Return m_blnResultPassFail
        End Get
        Set(ByVal value As Boolean)
            m_blnResultPassFail = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Diameter Test Drum Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Diameter Test Drum value.</returns>
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
    Public Property DiameterTestDrum() As Single
        Get
            Return m_sngDiameterTestDrum
        End Get
        Set(ByVal value As Single)
            m_sngDiameterTestDrum = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Inflation Pressure Read justed Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Diameter Test Drum value.</returns>
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
    Public Property InflationPressureReadjusted() As Short
        Get
            Return m_srtInflationPressureReadjusted
        End Get
        Set(ByVal value As Short)
            m_srtInflationPressureReadjusted = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Circumference Before Testing Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Circumference Before Testing.</returns>
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
    Public Property CircumferenceBeforeTesting() As Single
        Get
            Return m_sngCircumferenceBeforeTesting
        End Get
        Set(ByVal value As Single)
            m_sngCircumferenceBeforeTesting = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Circumference Before Testing Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Circumference Before Testing.</returns>
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
    Public Property EnduranceXHour() As Single
        Get
            Return m_sngEnduranceXHour
        End Get
        Set(ByVal value As Single)
            m_sngEnduranceXHour = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets EnduranceTest PassYN Value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>EnduranceTest PassYN.</returns>
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
    Public Property EnduranceTestPassYN() As Boolean
        Get
            Return m_blnEnduranceTestPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnEnduranceTestPassYN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Possible Failures Found Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Possible Failures Found.</returns>
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
    Public Property PossibleFailuresFound() As String
        Get
            Return m_strPossibleFailuresFound
        End Get
        Set(ByVal value As String)
            m_strPossibleFailuresFound = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Circumference After Testing Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Circumference After Testing.</returns>
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
    Public Property CircumferenceAfterTesting() As Single
        Get
            Return m_sngCircumferenceAfterTesting
        End Get
        Set(ByVal value As Single)
            m_sngCircumferenceAfterTesting = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Outer Diameter Difference Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Outer Diameter Difference.</returns>
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
    Public Property OuterDiameterDifference() As Single
        Get
            Return m_sngOuterDiameterDifference
        End Get
        Set(ByVal value As Single)
            m_sngOuterDiameterDifference = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Outer Diameter Tolerance Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Outer Diameter Tolerance.</returns>
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
    Public Property OuterDiameterTolerance() As Single
        Get
            Return m_sngOuterDiameterTolerance
        End Get
        Set(ByVal value As Single)
            m_sngOuterDiameterTolerance = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Serie NOM Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Serie NOM.</returns>
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
    Public Property SerieNOM() As String
        Get
            Return m_strSerieNOM
        End Get
        Set(ByVal value As String)
            m_strSerieNOM = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Final Judgement Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Final Judgement.</returns>
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
    Public Property FinalJudgement() As Boolean
        Get
            Return m_blnFinalJudgement
        End Get
        Set(ByVal value As Boolean)
            m_blnFinalJudgement = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Approver Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Approver.</returns>
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
    Public Property Approver() As String
        Get
            Return m_strApprover
        End Get
        Set(ByVal value As String)
            m_strApprover = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Precond Temp Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Precond Temp.</returns>
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
    Public Property PrecondTemp() As Single
        Get
            Return m_sngPrecondTemp
        End Get
        Set(ByVal value As Single)
            m_sngPrecondTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets GTSpec Material Number Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>GTSpec Material Number.</returns>
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
    Public Property GTSpecMaterialNumber() As String
        Get
            Return m_strGTSpecMaterialNumber
        End Get
        Set(ByVal value As String)
            m_strGTSpecMaterialNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets MFGWWYY Value.
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
    ''' <para>10/17/2019</para>
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
    ''' Do Validate with default (anonymous) rule set
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
            Dim results As ValidationResults = Validation.Validate(Of Endurance)(Me)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

    ''' <summary>
    ''' Do Validate with specific rule set
    ''' </summary>
    ''' <param name="p_strRuleSetName">RuleSet name.</param>
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
            Dim results As ValidationResults = Validation.Validate(Of Endurance)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region
End Class
