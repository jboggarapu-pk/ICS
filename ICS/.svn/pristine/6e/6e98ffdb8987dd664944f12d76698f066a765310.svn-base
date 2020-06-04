Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' WetGrip properties for all certification types
''' </summary>
''' <remarks></remarks>
Public Class WetGrip

#Region "Members"

    Private m_srtWetGripID As Short = 0
    Private m_strProjectNumber As String = String.Empty
    Private m_intTireNumber As Integer = 0
    Private m_strTestSpec As String = String.Empty
    'Added m_strOperation  as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Private m_strOperation As String = String.Empty
    Private m_dteDateOfTest As DateTime = DateTime.MinValue
    Private m_strTestVehicle As String = String.Empty
    Private m_strLocationOfTestTrack As String = String.Empty
    Private m_strTestTrackCharacteristics As String = String.Empty
    Private m_strIssueBy As String = String.Empty
    Private m_strMethodOfCertification As String = String.Empty
    Private m_strTestTireDetails As String = String.Empty
    Private m_strTireSizeAndServiceDesc As String = String.Empty
    Private m_strTireBrandAndTradeDesc As String = String.Empty
    Private m_strReferenceInflationPressure As String = String.Empty
    Private m_strTestRimWithCode As String = String.Empty
    Private m_strTempMeasureSensorType As String = String.Empty
    Private m_strIdentificationSRTT As String = String.Empty
    Private m_strTestTireLoad_SRTT As String = String.Empty
    Private m_strTestTireLoad_Candidate As String = String.Empty
    Private m_strTestTireLoad_Control As String = String.Empty
    Private m_strWaterDepth_SRTT As String = String.Empty
    Private m_strWaterDepth_Candidate As String = String.Empty
    Private m_strWaterDepth_Control As String = String.Empty
    Private m_strWettedTrackTempAvg As String = String.Empty
    Private m_intCertificationTypeID As Integer = 0
    Private m_intCertificateNumberID As Integer = 0
    Private m_intSKUId As Integer = 0

    Public WetGripDetails As List(Of WetGripDetail) = New List(Of WetGripDetail)

    Private m_objOriginalWetGrip As WetGrip = Nothing
    Private m_strGTSpecMaterialNumber As String = String.Empty
    Private m_strMFGWWYY As String = String.Empty
#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

#Region "Properties"

    Public Property WetGripID() As Short
        Get
            Return m_srtWetGripID
        End Get
        Set(ByVal value As Short)
            m_srtWetGripID = value
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

    Public Property DateOfTest() As DateTime
        Get
            Return m_dteDateOfTest
        End Get
        Set(ByVal value As DateTime)
            m_dteDateOfTest = value
        End Set
    End Property

    Public Property TestVehicle() As String
        Get
            Return m_strTestVehicle
        End Get
        Set(ByVal value As String)
            m_strTestVehicle = value
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

    Public Property TestTrackCharacteristics() As String
        Get
            Return m_strTestTrackCharacteristics
        End Get
        Set(ByVal value As String)
            m_strTestTrackCharacteristics = value
        End Set
    End Property

    Public Property IssueBy() As String
        Get
            Return m_strIssueBy
        End Get
        Set(ByVal value As String)
            m_strIssueBy = value
        End Set
    End Property

    Public Property MethodOfCertification() As String
        Get
            Return m_strMethodOfCertification
        End Get
        Set(ByVal value As String)
            m_strMethodOfCertification = value
        End Set
    End Property

    Public Property TestTireDetails() As String
        Get
            Return m_strTestTireDetails
        End Get
        Set(ByVal value As String)
            m_strTestTireDetails = value
        End Set
    End Property

    Public Property TireSizeAndServiceDesc() As String
        Get
            Return m_strTireSizeAndServiceDesc
        End Get
        Set(ByVal value As String)
            m_strTireSizeAndServiceDesc = value
        End Set
    End Property
    Public Property TireBrandAndTradeDesc() As String
        Get
            Return m_strTireBrandAndTradeDesc
        End Get
        Set(ByVal value As String)
            m_strTireBrandAndTradeDesc = value
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

    Public Property TestRimWithCode() As String
        Get
            Return m_strTestRimWithCode
        End Get
        Set(ByVal value As String)
            m_strTestRimWithCode = value
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

    Public Property IdentificationSRTT() As String
        Get
            Return m_strIdentificationSRTT
        End Get
        Set(ByVal value As String)
            m_strIdentificationSRTT = value
        End Set
    End Property

    Public Property TestTireLoad_SRTT() As String
        Get
            Return m_strTestTireLoad_SRTT
        End Get
        Set(ByVal value As String)
            m_strTestTireLoad_SRTT = value
        End Set
    End Property

    Public Property TestTireLoad_Candidate() As String
        Get
            Return m_strTestTireLoad_Candidate
        End Get
        Set(ByVal value As String)
            m_strTestTireLoad_Candidate = value
        End Set
    End Property

    Public Property TestTireLoad_Control() As String
        Get
            Return m_strTestTireLoad_Control
        End Get
        Set(ByVal value As String)
            m_strTestTireLoad_Control = value
        End Set
    End Property

    Public Property WaterDepth_SRTT() As String
        Get
            Return m_strWaterDepth_SRTT
        End Get
        Set(ByVal value As String)
            m_strWaterDepth_SRTT = value
        End Set
    End Property

    Public Property WaterDepth_Candidate() As String
        Get
            Return m_strWaterDepth_Candidate
        End Get
        Set(ByVal value As String)
            m_strWaterDepth_Candidate = value
        End Set
    End Property

    Public Property WaterDepth_Control() As String
        Get
            Return m_strWaterDepth_Control
        End Get
        Set(ByVal value As String)
            m_strWaterDepth_Control = value
        End Set
    End Property

    Public Property WettedTrackTempAvg() As String
        Get
            Return m_strWettedTrackTempAvg
        End Get
        Set(ByVal value As String)
            m_strWettedTrackTempAvg = value
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

    Public Property OriginalWetGrip() As WetGrip
        Get
            Return m_objOriginalWetGrip
        End Get
        Set(ByVal value As WetGrip)
            m_objOriginalWetGrip = value
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
            Dim results As ValidationResults = Validation.Validate(Of WetGrip)(Me)
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
