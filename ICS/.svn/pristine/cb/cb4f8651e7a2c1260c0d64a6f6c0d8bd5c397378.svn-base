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
Public Class HighSpeed

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

#Region "Members"
    ''' <summary>
    ''' variable to hold High Speed Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtHighSpeedId As Short = 0

    ''' <summary>
    ''' variable to hold project Number.
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
    ''' variable to hold DotSerial Number .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strDotSerialNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold  Precond Start Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dtePrecondStartDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Precond Start Temp.
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
    ''' variable to hold Precond End Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dtePrecondEndDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Precond End Temp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtPrecondEndTemp As Short = 0

    ''' <summary>
    ''' variable to hold Inflation Pressure.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtInflationPressure As Single = 0

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
    ''' variable to hold Final Temp.
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
    ''' variable to hold Duration Test PassYN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnDurationTestPassYN As Boolean = True ' - JLC

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
    ''' variable to hold Certification NUmber Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificateNumberID As Integer = 0

    ''' <summary>
    ''' variable to hold Serial Date.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteSerialDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Precond Time.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngPrecondTime As Single = 0

    ''' <summary>
    ''' variable to hold PostCond Time.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngPostcondTime As Single = 0

    ''' <summary>
    ''' variable to hold Diameter Test Drum.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngDiameterTestDrum As Single = 1.707

    ''' <summary>
    ''' variable to hold PreCond Temp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngPrecondTemp As Single = 0

    ''' <summary>
    ''' variable to hold Inflation Pressure Read justed .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtInflationPressureReadjusted As Single = 0

    ''' <summary>
    ''' variable to hold Circumference Before Testing .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngCircumferenceBeforeTesting As Single = 0

    ''' <summary>
    ''' variable to hold WHEEL SPEEDRPM .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngWHEELSPEEDRPM As Single = 0

    ''' <summary>
    ''' variable to hold WHEEL SPEEDKMH .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngWHEELSPEEDKMH As Single = 0

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
    ''' variable to hold SERIENOM.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSERIENOM As String = String.Empty

    ''' <summary>
    ''' variable to hold Final Judgement.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnFinalJudgement As Boolean = True '- JLC

    ''' <summary>
    ''' variable to hold Approver.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strApprover As String = String.Empty

    ''' <summary>
    ''' variable to hold Speed Test PassYN.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnSpeedTestPassYN As Boolean = False

    ''' <summary>
    ''' variable to hold Speed Test PassAt.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngSpeedTestPassAt As Single = 0

    ''' <summary>
    ''' variable to hold SPEED TOTAL TIME.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngSPEEDTOTALTIME As Single = 0

    ''' <summary>
    ''' variable to hold MAX SPEED.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngMAXSPEED As Single = 0

    ''' <summary>
    ''' variable to hold MAX LOAD.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngMAXLOAD As Single = 0

    ''' <summary>
    ''' variable to hold High Speed Details .
    ''' </summary>
    ''' <remarks></remarks>
    Public HighSpeedDetails As List(Of HighSpeedDetail) = New List(Of HighSpeedDetail)

    ''' <summary>
    ''' variable to hold Speed Test Details.
    ''' </summary>
    ''' <remarks></remarks>
    Public SpeedTestDetails As List(Of SpeedTestDetail) = New List(Of SpeedTestDetail)

    ''' <summary>
    ''' variable to hold Original High Speed.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_objOriginalHighSpeed As HighSpeed = Nothing

    ''' <summary>
    ''' variable to hold GTSpec Material Number .
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
    ''' Gets or sets HS ID value.
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property HS_ID() As Short
        Get
            Return m_srtHighSpeedId
        End Get
        Set(ByVal value As Short)
            m_srtHighSpeedId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Project Number.
    ''' </summary>
    ''' <value>String</value>
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
    ''' Gets or sets Tire Numbervalue.
    ''' </summary>
    ''' <value>Integer</value>
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
    ''' <para>10/18/2019</para>
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
    ''' <value>Datetime</value>
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
    ''' <para>10/18/2019</para>
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
    ''' Gets or sets Dot Serial Number value.
    ''' </summary>
    ''' <value>String</value>
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
    ''' <para>10/18/2019</para>
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
    ''' Gets or sets Precond Start Date value.
    ''' </summary>
    ''' <value>DateTime</value>
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
    ''' <para>10/18/2019</para>
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
    ''' Gets or sets Precond Start Temp value.
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
    ''' <para>10/18/2019</para>
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
    ''' Gets or sets Rim Diameter Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Field value.</returns>
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
    Public Property RimDiameter() As Single
        Get
            Return m_sngRimDiameter
        End Get
        Set(ByVal value As Single)
            m_sngRimDiameter = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Rim Width Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Field value.</returns>
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
    ''' Gets or sets Precond End Date Value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Field value.</returns>
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
    Public Property PrecondEndDate() As DateTime
        Get
            Return m_dtePrecondEndDate
        End Get
        Set(ByVal value As DateTime)
            m_dtePrecondEndDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets precond End Temp Value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Field value.</returns>
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
    ''' <returns>Field value.</returns>
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
    Public Property InflationPressure() As Single
        Get
            Return m_srtInflationPressure
        End Get
        Set(ByVal value As Single)
            m_srtInflationPressure = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Before Diameter Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Field value.</returns>
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
    ''' <returns>Field value.</returns>
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
    ''' <value>Short</value>
    ''' <returns>Field value.</returns>
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
    Public Property BeforeInflation() As Short
        Get
            Return CShort(m_srtBeforeInflation)
        End Get
        Set(ByVal value As Short)
            m_srtBeforeInflation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets After Inflation Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Field value.</returns>
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
    ''' <returns>Field value.</returns>
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
    ''' <returns>Field value.</returns>
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
    ''' <para>10/18/2019</para>
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
    ''' <para>10/18/2019</para>
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
    ''' <para>10/18/2019</para>
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
    ''' <returns>Postcond Start Date value.</returns>
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
    Public Property PostcondStartDate() As DateTime
        Get
            Return m_dtePostcondStartDate
        End Get
        Set(ByVal value As DateTime)
            m_dtePostcondStartDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets postCond End Date Value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>postCond End Datevalue.</returns>
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
    ''' <returns>Postcond End Temp value.</returns>
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
    Public Property PostcondEndTemp() As Short
        Get
            Return m_srtPostcondEndTemp
        End Get
        Set(ByVal value As Short)
            m_srtPostcondEndTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Duration Test PassYN Value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Duration Test PassYN value.</returns>
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
    Public Property DurationTestPassYN() As Boolean
        Get
            Return m_blnDurationTestPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnDurationTestPassYN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Material Number Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Material Number value.</returns>
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
    ''' Gets or sets SKU ID Value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>SKU ID value.</returns>
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
    ''' Gets or sets Certification Type Id Value.
    ''' </summary>
    ''' <value>integer</value>
    ''' <returns>Certification Type Id value.</returns>
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
            Return m_iCertificationTypeId
        End Get
        Set(ByVal value As Integer)
            m_iCertificationTypeId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Number ID Value.
    ''' </summary>
    ''' <value>integer</value>
    ''' <returns>Certificate Number ID value.</returns>
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
    ''' Gets or sets preCond Time Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>preCond Time value.</returns>
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
    Public Property PrecondTime() As Single
        Get
            Return m_sngPrecondTime
        End Get
        Set(ByVal value As Single)
            m_sngPrecondTime = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets postCond Time  Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>postCond Time value.</returns>
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
    Public Property PostcondTime() As Single
        Get
            Return m_sngPostcondTime
        End Get
        Set(ByVal value As Single)
            m_sngPostcondTime = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Original HighSpeed Value.
    ''' </summary>
    ''' <value>High SPeed object</value>
    ''' <returns>Original HighSpeed value.</returns>
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
    Public Property OriginalHighSpeed() As HighSpeed
        Get
            Return m_objOriginalHighSpeed
        End Get
        Set(ByVal value As HighSpeed)
            m_objOriginalHighSpeed = value
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
    ''' <para>10/18/2019</para>
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
    ''' Gets or sets Precond Temp Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Precond Temp value.</returns>
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
    Public Property PrecondTemp() As Single
        Get
            Return m_sngPrecondTemp
        End Get
        Set(ByVal value As Single)
            m_sngPrecondTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Inflation Pressure Read justed Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Inflation Pressure Read justed value.</returns>
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
    Public Property InflationPressureReadjusted() As Single
        Get
            Return m_srtInflationPressureReadjusted
        End Get
        Set(ByVal value As Single)
            m_srtInflationPressureReadjusted = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Circumference Before Testing Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Circumference Before Testing value.</returns>
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
    Public Property CircumferenceBeforeTesting() As Single
        Get
            Return m_sngCircumferenceBeforeTesting
        End Get
        Set(ByVal value As Single)
            m_sngCircumferenceBeforeTesting = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Wheel Speed RPM Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Wheel Speed RPM value.</returns>
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
    Public Property WheelSpeedRPM() As Single
        Get
            Return m_sngWHEELSPEEDRPM
        End Get
        Set(ByVal value As Single)
            m_sngWHEELSPEEDRPM = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Wheel Speed KMH Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Wheel Speed KMHvalue.</returns>
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
    Public Property WheelSpeedKMH() As Single
        Get
            Return m_sngWHEELSPEEDKMH
        End Get
        Set(ByVal value As Single)
            m_sngWHEELSPEEDKMH = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Circumference After Testing Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Circumference After Testing value.</returns>
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
    ''' <returns>outer Diameter Difference value.</returns>
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
    ''' <returns>Outer Diameter Tolerance value.</returns>
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
    Public Property OuterDiameterTolerance() As Single
        Get
            Return m_sngOuterDiameterTolerance
        End Get
        Set(ByVal value As Single)
            m_sngOuterDiameterTolerance = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets SerieNom Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>SerieNOM value.</returns>
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
    Public Property SerieNOM() As String
        Get
            Return m_strSERIENOM
        End Get
        Set(ByVal value As String)
            m_strSERIENOM = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Final Judgement Value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Final Judgement value.</returns>
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
    ''' <returns>Aprrover value.</returns>
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
    Public Property Approver() As String
        Get
            Return m_strApprover
        End Get
        Set(ByVal value As String)
            m_strApprover = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Speed Test PassAt Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Speed test PassAtvalue.</returns>
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
    Public Property SpeedTestPassAt() As Single
        Get
            Return m_sngSpeedTestPassAt
        End Get
        Set(ByVal value As Single)
            m_sngSpeedTestPassAt = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Speed Test PassYN Value.
    ''' </summary>
    ''' <value>boolean</value>
    ''' <returns>Speed Test passYN value.</returns>
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
    Public Property SpeedTestPassYN() As Boolean
        Get
            Return m_blnSpeedTestPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnSpeedTestPassYN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Speed Total Time Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Speed Total Time value.</returns>
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
    Public Property SpeedTotalTime() As Single
        Get
            Return m_sngSPEEDTOTALTIME
        End Get
        Set(ByVal value As Single)
            m_sngSPEEDTOTALTIME = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Max Speed Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Max Speed value.</returns>
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
    Public Property MaxSpeed() As Single
        Get
            Return m_sngMAXSPEED
        End Get
        Set(ByVal value As Single)
            m_sngMAXSPEED = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets max Load Value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Max load value.</returns>
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
    Public Property MaxLoad() As Single
        Get
            Return m_sngMAXLOAD
        End Get
        Set(ByVal value As Single)
            m_sngMAXLOAD = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets GTSpec Material Number Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>GTSpec Material Number value.</returns>
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
    ''' <returns>MFGWWYY value.</returns>
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
            Dim results As ValidationResults = Validation.Validate(Of HighSpeed)(Me)
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Function DoValidate(ByVal p_strRuleSetName As String) As Boolean

        Dim blnValid As Boolean = False
        Try
            Dim results As ValidationResults = Validation.Validate(Of HighSpeed)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region
End Class
