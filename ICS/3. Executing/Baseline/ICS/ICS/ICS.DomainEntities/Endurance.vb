Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Measurement properties for all certification types
''' </summary>
''' <remarks></remarks>
Public Class Endurance

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

#Region "Members"

    Private m_srtEnduranceId As Short = 0
    Private m_strProjectNumber As String = String.Empty
    Private m_srtTireNumber As Integer = 0
    Private m_strTestSpec As String = String.Empty
    'Added m_strOperation  as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Private m_strOperation As String = String.Empty
    Private m_dteCompletionDate As DateTime = DateTime.MinValue
    Private m_strDotSerialNumber As String = String.Empty
    Private m_dtePrecondStartDate As DateTime = DateTime.MinValue
    Private m_srtPrecondStartTemp As Short = 0
    Private m_sngRimDiameter As Single = 0
    Private m_sngRimWidth As Single = 0
    Private m_dtePrecondEndDate As DateTime = DateTime.MinValue
    Private m_srtPrecondEndTemp As Short = 0
    Private m_srtInflationPressure As Short = 0
    Private m_sngBeforeDiameter As Single = 0
    Private m_sngAfterDiameter As Single = 0
    Private m_srtBeforeInflation As Single = 0
    Private m_srtAfterInflation As Single = 0
    Private m_srtWheelPosition As Short = 0
    Private m_srtWheelNumber As Short = 0
    Private m_srtFinalTemp As Short = 0
    Private m_sngFinalDistance As Single = 0
    Private m_srtFinalInflation As Single = 0
    Private m_srtLowInfStartInflation As Single = 0
    Private m_srtLowInfEndInflation As Single = 0
    Private m_srtLowInfEndTemp As Single = 0
    Private m_dtePostcondStartDate As DateTime = DateTime.MinValue
    Private m_dtePostcondEndDate As DateTime = DateTime.MinValue
    Private m_srtPostcondEndTemp As Short = 0
    Private m_blnPassYN As Boolean = False
    Private m_strMatlNum As String = String.Empty
    Private m_intSKUID As Integer = 0
    Private m_iCertificationTypeId As Integer = 0
    Private m_intCertificateNumberID As Integer = 0

    Private m_dteSerialDate As DateTime = DateTime.MinValue
    Private m_sngPrecondTime As Single = 0
    Private m_sngPostcondTime As Single = 0


    Private m_sngPrecondTemp As Single = 0
    Private m_blnResultPassFail As Boolean = False

    Private m_sngDiameterTestDrum As Single = 1.707
    Private m_srtInflationPressureReadjusted As Short = 0
    Private m_sngCircumferenceBeforeTesting As Single = 0
    Private m_sngEnduranceXHour As Single = 0
    Private m_blnEnduranceTestPassYN As Boolean = False
    Private m_strPossibleFailuresFound As String = String.Empty
    Private m_sngCircumferenceAfterTesting As Single = 0
    Private m_sngOuterDiameterDifference As Single = 0
    Private m_sngOuterDiameterTolerance As Single = 0
    Private m_strSerieNOM As String = String.Empty
    Private m_blnFinalJudgement As Boolean = False
    Private m_strApprover As String = String.Empty

    Public EnduranceDetails As List(Of EnduranceDetail) = New List(Of EnduranceDetail)

    Private m_objOriginalEndurance As Endurance = Nothing

    Private m_strGTSpecMaterialNumber As String = String.Empty
    Private m_strMFGWWYY As String = String.Empty
#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

#Region "Properties"

    Public Property END_ID() As Short
        Get
            Return m_srtEnduranceId
        End Get
        Set(ByVal value As Short)
            m_srtEnduranceId = value
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
            Return m_srtTireNumber
        End Get
        Set(ByVal value As Integer)
            m_srtTireNumber = value
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
    '<StringLengthValidator(1,4, MessageTemplate:="Operation incorrect")> _
    Public Property Operation() As String
        Get
            Return m_strOperation
        End Get
        Set(ByVal value As String)
            m_strOperation = value
        End Set
    End Property
    Public Property CompletionDate() As DateTime
        Get
            Return m_dteCompletionDate
        End Get
        Set(ByVal value As DateTime)
            m_dteCompletionDate = value
        End Set
    End Property
    Public Property DotSerialNumber() As String
        Get
            Return m_strDotSerialNumber
        End Get
        Set(ByVal value As String)
            m_strDotSerialNumber = value
        End Set
    End Property
    Public Property PrecondStartDate() As DateTime
        Get
            Return m_dtePrecondStartDate
        End Get
        Set(ByVal value As DateTime)
            m_dtePrecondStartDate = value
        End Set
    End Property
    Public Property PrecondStartTemp() As Short
        Get
            Return m_srtPrecondStartTemp
        End Get
        Set(ByVal value As Short)
            m_srtPrecondStartTemp = value
        End Set
    End Property
    Public Property RimDiameter() As Single
        Get
            Return m_sngRimDiameter
        End Get
        Set(ByVal value As Single)
            m_sngRimDiameter = value
        End Set
    End Property
    Public Property RimWidth() As Single
        Get
            Return m_sngRimWidth
        End Get
        Set(ByVal value As Single)
            m_sngRimWidth = value
        End Set
    End Property
    Public Property PrecondEndDate() As DateTime
        Get
            Return m_dtePrecondEndDate
        End Get
        Set(ByVal value As DateTime)
            m_dtePrecondEndDate = value
        End Set
    End Property
    Public Property PrecondEndTemp() As Short
        Get
            Return m_srtPrecondEndTemp
        End Get
        Set(ByVal value As Short)
            m_srtPrecondEndTemp = value
        End Set
    End Property
    Public Property InflationPressure() As Single
        Get
            Return m_srtInflationPressure
        End Get
        Set(ByVal value As Single)
            m_srtInflationPressure = value
        End Set
    End Property
    Public Property LowInfEndInflation() As Single
        Get
            Return m_srtLowInfEndInflation
        End Get
        Set(ByVal value As Single)
            m_srtLowInfEndInflation = value
        End Set
    End Property
    Public Property LowInfStartInflation() As Single
        Get
            Return m_srtLowInfStartInflation
        End Get
        Set(ByVal value As Single)
            m_srtLowInfStartInflation = value
        End Set
    End Property

    Public Property LowInfEndTemp() As Single
        Get
            Return m_srtLowInfEndTemp
        End Get
        Set(ByVal value As Single)
            m_srtLowInfEndTemp = value
        End Set
    End Property
    Public Property BeforeDiameter() As Single
        Get
            Return m_sngBeforeDiameter
        End Get
        Set(ByVal value As Single)
            m_sngBeforeDiameter = value
        End Set
    End Property
    Public Property AfterDiameter() As Single
        Get
            Return m_sngAfterDiameter
        End Get
        Set(ByVal value As Single)
            m_sngAfterDiameter = value
        End Set
    End Property
    Public Property BeforeInflation() As Single
        Get
            Return m_srtBeforeInflation
        End Get
        Set(ByVal value As Single)
            m_srtBeforeInflation = value
        End Set
    End Property
    Public Property AfterInflation() As Single
        Get
            Return m_srtAfterInflation
        End Get
        Set(ByVal value As Single)
            m_srtAfterInflation = value
        End Set
    End Property
    Public Property WheelPosition() As Short
        Get
            Return m_srtWheelPosition
        End Get
        Set(ByVal value As Short)
            m_srtWheelPosition = value
        End Set
    End Property
    Public Property WheelNumber() As Short
        Get
            Return m_srtWheelNumber
        End Get
        Set(ByVal value As Short)
            m_srtWheelNumber = value
        End Set
    End Property
    Public Property FinalTemp() As Short
        Get
            Return m_srtFinalTemp
        End Get
        Set(ByVal value As Short)
            m_srtFinalTemp = value
        End Set
    End Property
    Public Property FinalDistance() As Single
        Get
            Return m_sngFinalDistance
        End Get
        Set(ByVal value As Single)
            m_sngFinalDistance = value
        End Set
    End Property
    Public Property FinalInflation() As Single
        Get
            Return m_srtFinalInflation
        End Get
        Set(ByVal value As Single)
            m_srtFinalInflation = value
        End Set
    End Property
    Public Property PostcondStartDate() As DateTime
        Get
            Return m_dtePostcondStartDate
        End Get
        Set(ByVal value As DateTime)
            m_dtePostcondStartDate = value
        End Set
    End Property
    Public Property PostcondEndDate() As DateTime
        Get
            Return m_dtePostcondEndDate
        End Get
        Set(ByVal value As DateTime)
            m_dtePostcondEndDate = value
        End Set
    End Property
    Public Property PostcondEndTemp() As Short
        Get
            Return m_srtPostcondEndTemp
        End Get
        Set(ByVal value As Short)
            m_srtPostcondEndTemp = value
        End Set
    End Property
    Public Property PassYN() As Boolean
        Get
            Return m_blnPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnPassYN = value
        End Set
    End Property
    ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Public Property MaterialNumber() As String
        Get
            Return m_strMatlNum
        End Get
        Set(ByVal value As String)
            m_strMatlNum = value
        End Set
    End Property
    Public Property SKUID() As Integer
        Get
            Return m_intSKUID
        End Get
        Set(ByVal value As Integer)
            m_intSKUID = value
        End Set
    End Property
    Public Property CertificationTypeId() As Integer
        Get
            Return m_iCertificationTypeId
        End Get
        Set(ByVal value As Integer)
            m_iCertificationTypeId = value
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

    Public Property SerialDate() As DateTime
        Get
            Return m_dteSerialDate
        End Get
        Set(ByVal value As DateTime)
            m_dteSerialDate = value
        End Set
    End Property
    Public Property PrecondTime() As Single
        Get
            Return m_sngPrecondTime
        End Get
        Set(ByVal value As Single)
            m_sngPrecondTime = value
        End Set
    End Property
    Public Property PostcondTime() As Single
        Get
            Return m_sngPostcondTime
        End Get
        Set(ByVal value As Single)
            m_sngPostcondTime = value
        End Set
    End Property

    Public Property OriginalEndurance() As Endurance
        Get
            Return m_objOriginalEndurance
        End Get
        Set(ByVal value As Endurance)
            m_objOriginalEndurance = value
        End Set
    End Property


    Public Property ResultPassFail() As Boolean
        Get
            Return m_blnResultPassFail
        End Get
        Set(ByVal value As Boolean)
            m_blnResultPassFail = value
        End Set
    End Property


    Public Property DiameterTestDrum() As Single
        Get
            Return m_sngDiameterTestDrum
        End Get
        Set(ByVal value As Single)
            m_sngDiameterTestDrum = value
        End Set
    End Property
    Public Property InflationPressureReadjusted() As Short
        Get
            Return m_srtInflationPressureReadjusted
        End Get
        Set(ByVal value As Short)
            m_srtInflationPressureReadjusted = value
        End Set
    End Property
    Public Property CircumferenceBeforeTesting() As Single
        Get
            Return m_sngCircumferenceBeforeTesting
        End Get
        Set(ByVal value As Single)
            m_sngCircumferenceBeforeTesting = value
        End Set
    End Property
    Public Property EnduranceXHour() As Single
        Get
            Return m_sngEnduranceXHour
        End Get
        Set(ByVal value As Single)
            m_sngEnduranceXHour = value
        End Set
    End Property
    Public Property EnduranceTestPassYN() As Boolean
        Get
            Return m_blnEnduranceTestPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnEnduranceTestPassYN = value
        End Set
    End Property
    Public Property PossibleFailuresFound() As String
        Get
            Return m_strPossibleFailuresFound
        End Get
        Set(ByVal value As String)
            m_strPossibleFailuresFound = value
        End Set
    End Property
    Public Property CircumferenceAfterTesting() As Single
        Get
            Return m_sngCircumferenceAfterTesting
        End Get
        Set(ByVal value As Single)
            m_sngCircumferenceAfterTesting = value
        End Set
    End Property
    Public Property OuterDiameterDifference() As Single
        Get
            Return m_sngOuterDiameterDifference
        End Get
        Set(ByVal value As Single)
            m_sngOuterDiameterDifference = value
        End Set
    End Property
    Public Property OuterDiameterTolerance() As Single
        Get
            Return m_sngOuterDiameterTolerance
        End Get
        Set(ByVal value As Single)
            m_sngOuterDiameterTolerance = value
        End Set
    End Property
    Public Property SerieNOM() As String
        Get
            Return m_strSerieNOM
        End Get
        Set(ByVal value As String)
            m_strSerieNOM = value
        End Set
    End Property
    Public Property FinalJudgement() As Boolean
        Get
            Return m_blnFinalJudgement
        End Get
        Set(ByVal value As Boolean)
            m_blnFinalJudgement = value
        End Set
    End Property
    Public Property Approver() As String
        Get
            Return m_strApprover
        End Get
        Set(ByVal value As String)
            m_strApprover = value
        End Set
    End Property
    Public Property PrecondTemp() As Single
        Get
            Return m_sngPrecondTemp
        End Get
        Set(ByVal value As Single)
            m_sngPrecondTemp = value
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
    ''' <param name="p_strRuleSetName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
