Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Class contains Sound properties for all certification types.
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
Public Class Sound

#Region "Members"

    ''' <summary>
    ''' variable to hold Sound ID.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtSoundID As Short = 0

    ''' <summary>
    ''' variable to hold Project Number .
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
    ''' variable to hold Operation .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strOperation As String = String.Empty

    ''' <summary>
    ''' variable to hold Test Report Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestReportNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Manufacture And Brand.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strManufactureAndBrand As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Class.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTireClass As String = String.Empty

    ''' <summary>
    ''' variable to hold Category Of Use .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCategoryOfUse As String = String.Empty

    ''' <summary>
    ''' variable to hold Date Of Test.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteDateOfTest As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Test Vehicle.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestVehicule As String = String.Empty

    ''' <summary>
    ''' variable to hold Test Vehicle Wheel base.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestVehiculeWheelbase As String = String.Empty

    ''' <summary>
    ''' variable to hold Location Of Test Track.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strLocationOfTestTrack As String = String.Empty

    ''' <summary>
    ''' variable to hold Date Track CertifToISO.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteDateTrackCertifToISO As DateTime = DateTime.MinValue

    ''' <summary>
    ''' variable to hold Tire Size Designation.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTireSizeDesignation As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Service Description.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTireServiceDescription As String = String.Empty

    ''' <summary>
    ''' variable to hold Reference Inflation Pressure.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strReferenceInflationPressure As String = String.Empty

    ''' <summary>
    ''' variable to hold Test Mass_FrontL.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestMass_FrontL As String = String.Empty

    ''' <summary>
    ''' variable to hold Test Mass_FrontR.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestMass_FrontR As String = String.Empty

    ''' <summary>
    ''' variable to hold Test Mass_RearL.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestMass_RearL As String = String.Empty

    ''' <summary>
    ''' variable to hold TestMass_RearR.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestMass_RearR As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Load Index_FrontL.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTireLoadIndex_FrontL As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Load Index_FrontR.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTireLoadIndex_FrontR As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Load index_RearL.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTireLoadIndex_RearL As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Load Index_RearR.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTireLoadIndex_RearR As String = String.Empty

    ''' <summary>
    ''' variable to hold Inflation PressureCo_FrontL.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strInflationPressureCo_FrontL As String = String.Empty

    ''' <summary>
    ''' variable to hold Inflation PressureCo_FrontR.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strInflationPressureCo_FrontR As String = String.Empty

    ''' <summary>
    ''' variable to hold Inflation PressureCo_RearL.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strInflationPressureCo_RearL As String = String.Empty

    ''' <summary>
    ''' variable to hold inflation presuureCo_RearR.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strInflationPressureCo_RearR As String = String.Empty

    ''' <summary>
    ''' variable to hold Test Rim Width Code.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestRimWidthCode As String = String.Empty

    ''' <summary>
    ''' variable to hold Temp Measure Sensor Type.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTempMeasureSensorType As String = String.Empty

    ''' <summary>
    ''' variable to hold Certification Type Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificationTypeID As Integer = 0 '  NOT NULL , 

    ''' <summary>
    ''' variable to holdCertificate Number Id .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificateNumberID As Integer = 0 ' NOT NULL , 

    ''' <summary>
    ''' variable to hold SKU Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intSKUId As Integer = 0 '  NOT NULL ,

    ''' <summary>
    ''' variable to hold Sound Details.
    ''' </summary>
    ''' <remarks></remarks>
    Public SoundDetails As List(Of SoundDetail) = New List(Of SoundDetail)

    ''' <summary>
    ''' variable to hold Original Sound.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_objOriginalSound As Sound = Nothing

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
    ''' Gets or sets Sound Id value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Sound Id.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property SoundID() As Short
        Get
            Return m_srtSoundID
        End Get
        Set(ByVal value As Short)
            m_srtSoundID = value
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
    ''' <para>10/28/2019</para>
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
    ''' <para>10/28/2019</para>
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
    ''' <para>10/28/2019</para>
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
    ''' Gets or sets operation value.
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
    ''' <para>10/28/2019</para>
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
    ''' Gets or sets test Report Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Report Number.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TestReportNumber() As String
        Get
            Return m_strTestReportNumber
        End Get
        Set(ByVal value As String)
            m_strTestReportNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Manufacture and Brand value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Manufacture and Brand.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property ManufactureAndBrand() As String
        Get
            Return m_strManufactureAndBrand
        End Get
        Set(ByVal value As String)
            m_strManufactureAndBrand = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Class value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tire Class.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TireClass() As String
        Get
            Return m_strTireClass
        End Get
        Set(ByVal value As String)
            m_strTireClass = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Category of use value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Category of Use.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property CategoryOfUse() As String
        Get
            Return m_strCategoryOfUse
        End Get
        Set(ByVal value As String)
            m_strCategoryOfUse = value
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
    ''' <para>10/28/2019</para>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TestVehicule() As String
        Get
            Return m_strTestVehicule
        End Get
        Set(ByVal value As String)
            m_strTestVehicule = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Vehicle Wheel base value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Vehicle Wheel base .</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TestVehiculeWheelbase() As String
        Get
            Return m_strTestVehiculeWheelbase
        End Get
        Set(ByVal value As String)
            m_strTestVehiculeWheelbase = value
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
    ''' <para>10/28/2019</para>
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
    ''' Gets or sets Date Track CertifToISO value.
    ''' </summary>
    ''' <value>Datetime</value>
    ''' <returns>Date Track CertifToISO.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property DateTrackCertifToISO() As DateTime
        Get
            Return m_dteDateTrackCertifToISO
        End Get
        Set(ByVal value As DateTime)
            m_dteDateTrackCertifToISO = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Size Designation value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tire Size Designation.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TireSizeDesignation() As String
        Get
            Return m_strTireSizeDesignation
        End Get
        Set(ByVal value As String)
            m_strTireSizeDesignation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Service Description value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tire Service Description.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TireServiceDescription() As String
        Get
            Return m_strTireServiceDescription
        End Get
        Set(ByVal value As String)
            m_strTireServiceDescription = value
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
    ''' <para>10/28/2019</para>
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
    ''' Gets or sets Test Mass_FrontL value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Mass_FrontL.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TestMass_FrontL() As String
        Get
            Return m_strTestMass_FrontL
        End Get
        Set(ByVal value As String)
            m_strTestMass_FrontL = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Mass_FrontR value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Mass_FrontR.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TestMass_FrontR() As String
        Get
            Return m_strTestMass_FrontR
        End Get
        Set(ByVal value As String)
            m_strTestMass_FrontR = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Mass_RearL value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Mass_RearL.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TestMass_RearL() As String
        Get
            Return m_strTestMass_RearL
        End Get
        Set(ByVal value As String)
            m_strTestMass_RearL = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Mass_RearR value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Mass_RearR.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TestMass_RearR() As String
        Get
            Return m_strTestMass_RearR
        End Get
        Set(ByVal value As String)
            m_strTestMass_RearR = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Load Index_FrontL value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tire Load Index_FrontL.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TireLoadIndex_FrontL() As String
        Get
            Return m_strTireLoadIndex_FrontL
        End Get
        Set(ByVal value As String)
            m_strTireLoadIndex_FrontL = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Load Index_FrontR value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tire Load Index_FrontR.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TireLoadIndex_FrontR() As String
        Get
            Return m_strTireLoadIndex_FrontR
        End Get
        Set(ByVal value As String)
            m_strTireLoadIndex_FrontR = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Load Index_RearL value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tire Load Index_RearL.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TireLoadIndex_RearL() As String
        Get
            Return m_strTireLoadIndex_RearL
        End Get
        Set(ByVal value As String)
            m_strTireLoadIndex_RearL = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Tire Load Index_RearR value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Tire Load Index_RearR.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TireLoadIndex_RearR() As String
        Get
            Return m_strTireLoadIndex_RearR
        End Get
        Set(ByVal value As String)
            m_strTireLoadIndex_RearR = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Inflation Pressure Co_FrontL value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Inflation Pressure Co_FrontL.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property InflationPressureCo_FrontL() As String
        Get
            Return m_strInflationPressureCo_FrontL
        End Get
        Set(ByVal value As String)
            m_strInflationPressureCo_FrontL = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Inflation Pressure Co_FrontR value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Inflation Pressure Co_FrontR.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property InflationPressureCo_FrontR() As String
        Get
            Return m_strInflationPressureCo_FrontR
        End Get
        Set(ByVal value As String)
            m_strInflationPressureCo_FrontR = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Inflation Pressure Co_RearL value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Inflation Pressure Co_RearL.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property InflationPressureCo_RearL() As String
        Get
            Return m_strInflationPressureCo_RearL
        End Get
        Set(ByVal value As String)
            m_strInflationPressureCo_RearL = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Inflation Pressure Co_RearR value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Inflation Pressure Co_RearR.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property InflationPressureCo_RearR() As String
        Get
            Return m_strInflationPressureCo_RearR
        End Get
        Set(ByVal value As String)
            m_strInflationPressureCo_RearR = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Rim Width Code value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Rim Width Code.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property TestRimWidthCode() As String
        Get
            Return m_strTestRimWidthCode
        End Get
        Set(ByVal value As String)
            m_strTestRimWidthCode = value
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
    ''' <para>10/28/2019</para>
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
    ''' <para>10/28/2019</para>
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
    ''' <para>10/28/2019</para>
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
    ''' <para>10/28/2019</para>
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
    ''' Gets or sets Original Sound value.
    ''' </summary>
    ''' <value>Sound Object</value>
    ''' <returns>Original Sound.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
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
    Public Property OriginalSound() As Sound
        Get
            Return m_objOriginalSound
        End Get
        Set(ByVal value As Sound)
            m_objOriginalSound = value
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
    ''' <para>10/28/2019</para>
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
    ''' <para>10/28/2019</para>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Function DoValidate() As Boolean

        Dim blnValid As Boolean = False
        Try
            Dim results As ValidationResults = Validation.Validate(Of Sound)(Me)
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
            Dim results As ValidationResults = Validation.Validate(Of Sound)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region

End Class
