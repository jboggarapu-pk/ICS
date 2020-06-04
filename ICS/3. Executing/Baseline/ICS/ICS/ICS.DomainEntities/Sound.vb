Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Sound properties for all certification types
''' </summary>
''' <remarks></remarks>
Public Class Sound

#Region "Members"

    Private m_srtSoundID As Short = 0
    Private m_strProjectNumber As String = String.Empty
    Private m_intTireNumber As Integer = 0
    Private m_strTestSpec As String = String.Empty
    'Added m_strOperation  as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Private m_strOperation As String = String.Empty
    Private m_strTestReportNumber As String = String.Empty
    Private m_strManufactureAndBrand As String = String.Empty
    Private m_strTireClass As String = String.Empty
    Private m_strCategoryOfUse As String = String.Empty
    Private m_dteDateOfTest As DateTime = DateTime.MinValue
    Private m_strTestVehicule As String = String.Empty
    Private m_strTestVehiculeWheelbase As String = String.Empty
    Private m_strLocationOfTestTrack As String = String.Empty
    Private m_dteDateTrackCertifToISO As DateTime = DateTime.MinValue
    Private m_strTireSizeDesignation As String = String.Empty
    Private m_strTireServiceDescription As String = String.Empty
    Private m_strReferenceInflationPressure As String = String.Empty
    Private m_strTestMass_FrontL As String = String.Empty
    Private m_strTestMass_FrontR As String = String.Empty
    Private m_strTestMass_RearL As String = String.Empty
    Private m_strTestMass_RearR As String = String.Empty
    Private m_strTireLoadIndex_FrontL As String = String.Empty
    Private m_strTireLoadIndex_FrontR As String = String.Empty
    Private m_strTireLoadIndex_RearL As String = String.Empty
    Private m_strTireLoadIndex_RearR As String = String.Empty
    Private m_strInflationPressureCo_FrontL As String = String.Empty
    Private m_strInflationPressureCo_FrontR As String = String.Empty
    Private m_strInflationPressureCo_RearL As String = String.Empty
    Private m_strInflationPressureCo_RearR As String = String.Empty
    Private m_strTestRimWidthCode As String = String.Empty
    Private m_strTempMeasureSensorType As String = String.Empty
    Private m_intCertificationTypeID As Integer = 0 '  NOT NULL , 
    Private m_intCertificateNumberID As Integer = 0 ' NOT NULL , 
    Private m_intSKUId As Integer = 0 '  NOT NULL ,

    Public SoundDetails As List(Of SoundDetail) = New List(Of SoundDetail)

    Private m_objOriginalSound As Sound = Nothing
    Private m_strGTSpecMaterialNumber As String = String.Empty
    Private m_strMFGWWYY As String = String.Empty
#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

#Region "Properties"

    Public Property SoundID() As Short
        Get
            Return m_srtSoundID
        End Get
        Set(ByVal value As Short)
            m_srtSoundID = value
        End Set
    End Property

    '<NotNullValidator(MessageTemplate:="Project Number not null")> _
    '<StringLengthValidator(1, 6, MessageTemplate:="Project Number incorrect")> _
    Public Property ProjectNumber() As String
        Get
            Return m_strProjectNumber
        End Get
        Set(ByVal value As String)
            m_strProjectNumber = value
        End Set
    End Property

    '<NotNullValidator(MessageTemplate:="Tire Number not null")> _
    '<RangeValidator(1, RangeBoundaryType.Inclusive, 9999, RangeBoundaryType.Inclusive, MessageTemplate:="Tire Number incorrect")> _
    Public Property TireNumber() As Integer
        Get
            Return m_intTireNumber
        End Get
        Set(ByVal value As Integer)
            m_intTireNumber = value
        End Set
    End Property

    '<NotNullValidator(MessageTemplate:="Test Spec not null")> _
    '<StringLengthValidator(1, 7, MessageTemplate:="Test Spec incorrect")> _
    Public Property TestSpec() As String
        Get
            Return m_strTestSpec
        End Get
        Set(ByVal value As String)
            m_strTestSpec = value
        End Set
    End Property
    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    '<NotNullValidator(MessageTemplate:="Operation not null")> _
    '<StringLengthValidator(1, 4, MessageTemplate:="Operation incorrect")> _
    Public Property Operation() As String
        Get
            Return m_strOperation
        End Get
        Set(ByVal value As String)
            m_strOperation = value
        End Set
    End Property

    Public Property TestReportNumber() As String
        Get
            Return m_strTestReportNumber
        End Get
        Set(ByVal value As String)
            m_strTestReportNumber = value
        End Set
    End Property

    Public Property ManufactureAndBrand() As String
        Get
            Return m_strManufactureAndBrand
        End Get
        Set(ByVal value As String)
            m_strManufactureAndBrand = value
        End Set
    End Property

    Public Property TireClass() As String
        Get
            Return m_strTireClass
        End Get
        Set(ByVal value As String)
            m_strTireClass = value
        End Set
    End Property
    Public Property CategoryOfUse() As String
        Get
            Return m_strCategoryOfUse
        End Get
        Set(ByVal value As String)
            m_strCategoryOfUse = value
        End Set
    End Property

    Public Property DateOfTest() As DateTime
        Get
            Return m_dteDateOfTest
        End Get
        Set(ByVal value As DateTime)
            m_dteDateOfTest = value
        End Set
    End Property

    Public Property TestVehicule() As String
        Get
            Return m_strTestVehicule
        End Get
        Set(ByVal value As String)
            m_strTestVehicule = value
        End Set
    End Property

    Public Property TestVehiculeWheelbase() As String
        Get
            Return m_strTestVehiculeWheelbase
        End Get
        Set(ByVal value As String)
            m_strTestVehiculeWheelbase = value
        End Set
    End Property

    Public Property LocationOfTestTrack() As String
        Get
            Return m_strLocationOfTestTrack
        End Get
        Set(ByVal value As String)
            m_strLocationOfTestTrack = value
        End Set
    End Property

    Public Property DateTrackCertifToISO() As DateTime
        Get
            Return m_dteDateTrackCertifToISO
        End Get
        Set(ByVal value As DateTime)
            m_dteDateTrackCertifToISO = value
        End Set
    End Property

    Public Property TireSizeDesignation() As String
        Get
            Return m_strTireSizeDesignation
        End Get
        Set(ByVal value As String)
            m_strTireSizeDesignation = value
        End Set
    End Property

    Public Property TireServiceDescription() As String
        Get
            Return m_strTireServiceDescription
        End Get
        Set(ByVal value As String)
            m_strTireServiceDescription = value
        End Set
    End Property

    Public Property ReferenceInflationPressure() As String
        Get
            Return m_strReferenceInflationPressure
        End Get
        Set(ByVal value As String)
            m_strReferenceInflationPressure = value
        End Set
    End Property


    Public Property TestMass_FrontL() As String
        Get
            Return m_strTestMass_FrontL
        End Get
        Set(ByVal value As String)
            m_strTestMass_FrontL = value
        End Set
    End Property

    Public Property TestMass_FrontR() As String
        Get
            Return m_strTestMass_FrontR
        End Get
        Set(ByVal value As String)
            m_strTestMass_FrontR = value
        End Set
    End Property

    Public Property TestMass_RearL() As String
        Get
            Return m_strTestMass_RearL
        End Get
        Set(ByVal value As String)
            m_strTestMass_RearL = value
        End Set
    End Property

    Public Property TestMass_RearR() As String
        Get
            Return m_strTestMass_RearR
        End Get
        Set(ByVal value As String)
            m_strTestMass_RearR = value
        End Set
    End Property

    Public Property TireLoadIndex_FrontL() As String
        Get
            Return m_strTireLoadIndex_FrontL
        End Get
        Set(ByVal value As String)
            m_strTireLoadIndex_FrontL = value
        End Set
    End Property

    Public Property TireLoadIndex_FrontR() As String
        Get
            Return m_strTireLoadIndex_FrontR
        End Get
        Set(ByVal value As String)
            m_strTireLoadIndex_FrontR = value
        End Set
    End Property

    Public Property TireLoadIndex_RearL() As String
        Get
            Return m_strTireLoadIndex_RearL
        End Get
        Set(ByVal value As String)
            m_strTireLoadIndex_RearL = value
        End Set
    End Property

    Public Property TireLoadIndex_RearR() As String
        Get
            Return m_strTireLoadIndex_RearR
        End Get
        Set(ByVal value As String)
            m_strTireLoadIndex_RearR = value
        End Set
    End Property

    Public Property InflationPressureCo_FrontL() As String
        Get
            Return m_strInflationPressureCo_FrontL
        End Get
        Set(ByVal value As String)
            m_strInflationPressureCo_FrontL = value
        End Set
    End Property

    Public Property InflationPressureCo_FrontR() As String
        Get
            Return m_strInflationPressureCo_FrontR
        End Get
        Set(ByVal value As String)
            m_strInflationPressureCo_FrontR = value
        End Set
    End Property

    Public Property InflationPressureCo_RearL() As String
        Get
            Return m_strInflationPressureCo_RearL
        End Get
        Set(ByVal value As String)
            m_strInflationPressureCo_RearL = value
        End Set
    End Property

    Public Property InflationPressureCo_RearR() As String
        Get
            Return m_strInflationPressureCo_RearR
        End Get
        Set(ByVal value As String)
            m_strInflationPressureCo_RearR = value
        End Set
    End Property

    Public Property TestRimWidthCode() As String
        Get
            Return m_strTestRimWidthCode
        End Get
        Set(ByVal value As String)
            m_strTestRimWidthCode = value
        End Set
    End Property

    Public Property TempMeasureSensorType() As String
        Get
            Return m_strTempMeasureSensorType
        End Get
        Set(ByVal value As String)
            m_strTempMeasureSensorType = value
        End Set
    End Property

    Public Property CertificationTypeID() As Integer
        Get
            Return m_intCertificationTypeID
        End Get
        Set(ByVal value As Integer)
            m_intCertificationTypeID = value
        End Set
    End Property

    Public Property CertificateNumberID() As Integer
        Get
            Return m_intCertificateNumberID
        End Get
        Set(ByVal value As Integer)
            m_intCertificateNumberID = value
        End Set
    End Property

    Public Property SKUId() As Integer
        Get
            Return m_intSKUId
        End Get
        Set(ByVal value As Integer)
            m_intSKUId = value
        End Set
    End Property

    Public Property OriginalSound() As Sound
        Get
            Return m_objOriginalSound
        End Get
        Set(ByVal value As Sound)
            m_objOriginalSound = value
        End Set
    End Property

    Public Property GTSpecMaterialNumber() As String
        Get
            Return m_strGTSpecMaterialNumber
        End Get
        Set(ByVal value As String)
            m_strGTSpecMaterialNumber = value
        End Set
    End Property

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
    ''' Method is used for default (anonymous) validation
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks></remarks>
    <SelfValidation()> _
    Private Sub SelfValidate(ByVal results As ValidationResults)
        'NOTE: this is implementation example.
        '        If m_val1 < m_val2 Then
        '            results.AddResult(New ValidationResult("Message", Me, Nothing, Nothing, Nothing))
        '        End If

    End Sub

    ''' <summary>
    ''' Do Validate with default (anonymous) rule set
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' Do Validate with specific rule set
    ''' </summary>
    ''' <param name="p_strRuleSetName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
