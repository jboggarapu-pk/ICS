Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Class contains WetGrip properties for all certification types.
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
''' <para>10/29/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>  
Public Class WetGrip

#Region "Members"

    ''' <summary>
    ''' variable to hold WetGrip Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtWetGripID As Short = 0

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
    ''' variable to hold Date of Test.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteDateOfTest As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold test Vehicle.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestVehicle As String = String.Empty

    ''' <summary>
    ''' variable to hold Location of Test Track.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strLocationOfTestTrack As String = String.Empty

    ''' <summary>
    ''' variable to hold Test Track Characteristics.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestTrackCharacteristics As String = String.Empty

    ''' <summary>
    ''' variable to hold Issueby.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strIssueBy As String = String.Empty

    ''' <summary>
    ''' variable to hold Method of Certification.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMethodOfCertification As String = String.Empty

    ''' <summary>
    ''' variable to hold Test Tire Details.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestTireDetails As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Size and Service Desc.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTireSizeAndServiceDesc As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Brand And Trade Desc.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTireBrandAndTradeDesc As String = String.Empty

    ''' <summary>
    ''' variable to hold reference Inflation Pressure.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strReferenceInflationPressure As String = String.Empty

    ''' <summary>
    ''' variable to hold test Rim With Code.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestRimWithCode As String = String.Empty

    ''' <summary>
    ''' variable to hold Temp Measure Sensor Type.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTempMeasureSensorType As String = String.Empty

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strIdentificationSRTT As String = String.Empty

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestTireLoad_SRTT As String = String.Empty

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestTireLoad_Candidate As String = String.Empty

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestTireLoad_Control As String = String.Empty

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strWaterDepth_SRTT As String = String.Empty

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strWaterDepth_Candidate As String = String.Empty

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strWaterDepth_Control As String = String.Empty

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strWettedTrackTempAvg As String = String.Empty

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificationTypeID As Integer = 0

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificateNumberID As Integer = 0

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intSKUId As Integer = 0

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Public WetGripDetails As List(Of WetGripDetail) = New List(Of WetGripDetail)

    ''' <summary>
    ''' variable to hold Original WetGrip.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_objOriginalWetGrip As WetGrip = Nothing

    ''' <summary>
    ''' variable to hold GT Spec Material Number.
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
    ''' <para>10/29/2019</para>
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
    ''' Gets or sets Wet Grip Id value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Wet Grip Id.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property WetGripID() As Short
        Get
            Return m_srtWetGripID
        End Get
        Set(ByVal value As Short)
            m_srtWetGripID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Project number value.
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
    ''' <para>10/29/2019</para>
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
    ''' <para>10/29/2019</para>
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
    ''' <returns>test Spec.</returns>
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
    ''' <para>10/29/2019</para>
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
    ''' <para>10/29/2019</para>
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
    ''' Gets or sets Date Of Test value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Date of Test.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property DateOfTest() As DateTime
        Get
            Return m_dteDateOfTest
        End Get
        Set(ByVal value As DateTime)
            m_dteDateOfTest = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Vehicle value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Vehicle.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TestVehicle() As String
        Get
            Return m_strTestVehicle
        End Get
        Set(ByVal value As String)
            m_strTestVehicle = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Location Of Test Track value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Location Of Test Track.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property LocationOfTestTrack() As String
        Get
            Return m_strLocationOfTestTrack
        End Get
        Set(ByVal value As String)
            m_strLocationOfTestTrack = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Track Characteristics value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Track Characteristics.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TestTrackCharacteristics() As String
        Get
            Return m_strTestTrackCharacteristics
        End Get
        Set(ByVal value As String)
            m_strTestTrackCharacteristics = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Issue by value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Issue by.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property IssueBy() As String
        Get
            Return m_strIssueBy
        End Get
        Set(ByVal value As String)
            m_strIssueBy = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Method Of Certification value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Method Of Certification.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MethodOfCertification() As String
        Get
            Return m_strMethodOfCertification
        End Get
        Set(ByVal value As String)
            m_strMethodOfCertification = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Tire Details value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Tire details.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TestTireDetails() As String
        Get
            Return m_strTestTireDetails
        End Get
        Set(ByVal value As String)
            m_strTestTireDetails = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Size And Service Desc value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tire Size And Service Desc.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TireSizeAndServiceDesc() As String
        Get
            Return m_strTireSizeAndServiceDesc
        End Get
        Set(ByVal value As String)
            m_strTireSizeAndServiceDesc = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Brand And TradeDesc value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tire Brand And TradeDesc.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TireBrandAndTradeDesc() As String
        Get
            Return m_strTireBrandAndTradeDesc
        End Get
        Set(ByVal value As String)
            m_strTireBrandAndTradeDesc = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Reference Inflation Pressure value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Reference Inflation Pressure.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ReferenceInflationPressure() As String
        Get
            Return m_strReferenceInflationPressure
        End Get
        Set(ByVal value As String)
            m_strReferenceInflationPressure = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets test Rim with code value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Rim With Code.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TestRimWithCode() As String
        Get
            Return m_strTestRimWithCode
        End Get
        Set(ByVal value As String)
            m_strTestRimWithCode = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Temp Measure Sensor Type value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Temp Measure Sensor Type.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TempMeasureSensorType() As String
        Get
            Return m_strTempMeasureSensorType
        End Get
        Set(ByVal value As String)
            m_strTempMeasureSensorType = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Identification SRTT value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Identification SRTT.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property IdentificationSRTT() As String
        Get
            Return m_strIdentificationSRTT
        End Get
        Set(ByVal value As String)
            m_strIdentificationSRTT = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Tire Load SRTT value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Tire Load SRTT.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TestTireLoad_SRTT() As String
        Get
            Return m_strTestTireLoad_SRTT
        End Get
        Set(ByVal value As String)
            m_strTestTireLoad_SRTT = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Tire Load Candidate value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Tire Load Candidate.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TestTireLoad_Candidate() As String
        Get
            Return m_strTestTireLoad_Candidate
        End Get
        Set(ByVal value As String)
            m_strTestTireLoad_Candidate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test tire load Control value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test tire load Control.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TestTireLoad_Control() As String
        Get
            Return m_strTestTireLoad_Control
        End Get
        Set(ByVal value As String)
            m_strTestTireLoad_Control = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Water Depth SRTT value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Water Depth SRTT.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property WaterDepth_SRTT() As String
        Get
            Return m_strWaterDepth_SRTT
        End Get
        Set(ByVal value As String)
            m_strWaterDepth_SRTT = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Water Depth Candidate value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Water Depth Candidate.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property WaterDepth_Candidate() As String
        Get
            Return m_strWaterDepth_Candidate
        End Get
        Set(ByVal value As String)
            m_strWaterDepth_Candidate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Water Depth Control value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Water Depth Control.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property WaterDepth_Control() As String
        Get
            Return m_strWaterDepth_Control
        End Get
        Set(ByVal value As String)
            m_strWaterDepth_Control = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Wetted Track TempAvg value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Wetted Track TempAvg.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property WettedTrackTempAvg() As String
        Get
            Return m_strWettedTrackTempAvg
        End Get
        Set(ByVal value As String)
            m_strWettedTrackTempAvg = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certification Type Id value.
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertificationTypeID() As Integer
        Get
            Return m_intCertificationTypeID
        End Get
        Set(ByVal value As Integer)
            m_intCertificationTypeID = value
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
    ''' <para>10/29/2019</para>
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
    ''' Gets or sets SKU Id value.
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
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SKUId() As Integer
        Get
            Return m_intSKUId
        End Get
        Set(ByVal value As Integer)
            m_intSKUId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Original Wet Grip value.
    ''' </summary>
    ''' <value>WetGrip</value>
    ''' <returns>Original Wet Grip.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property OriginalWetGrip() As WetGrip
        Get
            Return m_objOriginalWetGrip
        End Get
        Set(ByVal value As WetGrip)
            m_objOriginalWetGrip = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets GT Spec Material Number value.
    ''' </summary>
    ''' <value>String</value>
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
    ''' <para>10/29/2019</para>
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
    ''' <para>10/29/2019</para>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Function DoValidate() As Boolean

        Dim blnValid As Boolean = False
        Try
            Dim results As ValidationResults = Validation.Validate(Of WetGrip)(Me)
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
            Dim results As ValidationResults = Validation.Validate(Of WetGrip)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region

End Class
