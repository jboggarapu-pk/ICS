Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Measurement properties for all certification types
''' </summary>
''' <remarks></remarks>
Public Class Measure

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

#Region "Members"

    Private m_srtMeasureId As Short = 0
    Private m_strProjectNumber As String = String.Empty
    Private m_intTireNumber As Integer = 0
    Private m_strTestSpec As String = String.Empty
    'Added m_strOperation  as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Private m_strOperation As String = String.Empty
    Private m_dteCompletionDate As DateTime = DateTime.MinValue
    Private m_srtInflationPressure As Short = 0
    Private m_strMoldDesign As String = String.Empty
    Private m_sngRimWidth As Single = 0
    Private m_strDOTSerialNumber As String = String.Empty

    Private m_dteSerialDate As DateTime = DateTime.MinValue

    Private m_sngDiameter As Single = 0
    Private m_sngAvgSectionWidth As Single = 0
    Private m_sngAvgOverallWidth As Single = 0
    Private m_sngMaxOverallWidth As Single = 0

    Private m_sngMinSizeFactor As Single = 0
    Private m_sngActSizeFactor As Single = 0
    Private m_sngEndTime As DateTime = DateTime.MinValue

    Private m_dteMountTime As DateTime = DateTime.MinValue
    Private m_sngMountTemp As Single = 0
    Private m_strMatlNum As String = String.Empty
    Private m_intSKUID As Integer = 0
    Private m_intCertificationTypeId As Integer = 0
    '    Private m_strCertificateNumber As String = String.Empty
    Private m_intCertificateNumberID As Integer = 0

    'Key in Field
    Private m_srtStartInfPressure As Short = 0
    Private m_srtEndInfPressure As Short = 0
    Private m_strAdjustment As String = String.Empty
    Private m_sngCircumference As Single = 0
    Private m_sngNominalDiameter As Single = 0
    Private m_sngNominalWidth As Single = 0
    Private m_blnNominalWithPassYN As Boolean = False
    Private m_sngNominalWidthDifference As Single = 0
    Private m_sngNominalWidthTolerance As Single = 0
    Private m_sngMaxOverallDiameter As Single = 0
    Private m_sngMinOverallDiameter As Single = 0
    Private m_blnOverallWidthPassYN As Boolean = True
    Private m_blnOverallDiameterPassYN As Boolean = False
    Private m_sngDiameterDifference As Single = 0
    Private m_sngDiameterTolerance As Single = 0
    Private m_sngTensileStrength1 As Single = 0
    Private m_sngTensileStrength2 As Single = 1.2
    Private m_srtElongation1 As Short = 0
    Private m_srtElongation2 As Short = 300
    Private m_sngTensileStrengthAfterAging1 As Single = 0
    Private m_sngTensileStrengthAfterAging2 As Single = 80
    Private m_sngTemperatureResistanceGrading As String = String.Empty

    Public MeasureDetails As List(Of MeasureDetail) = New List(Of MeasureDetail)

    Private m_objOriginalMeasure As Measure = Nothing
    Private m_strGTSpecMatlNum As String = String.Empty
    Private m_strMFGWWYY As String = String.Empty

    


#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

#Region "Properties"

    '<NotNullValidator()> _
    '<StringLengthValidator(11, 18, MessageTemplate:="Material must be 11-18 characters")> _
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
            Return m_intCertificationTypeId
        End Get
        Set(ByVal value As Integer)
            m_intCertificationTypeId = value
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

    Public Property MeasureId() As Short
        Get
            Return m_srtMeasureId
        End Get
        Set(ByVal value As Short)
            m_srtMeasureId = value
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

    ' <DateTimeRangeValidator("2000-01-31T00:00:00", "2012-12-31T00:00:00")> _
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

    Public Property InflationPressure() As Short
        Get
            Return m_srtInflationPressure
        End Get
        Set(ByVal value As Short)
            m_srtInflationPressure = value
        End Set
    End Property

    Public Property MoldDesign() As String
        Get
            Return m_strMoldDesign
        End Get
        Set(ByVal value As String)
            m_strMoldDesign = value
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

    Public Property DotSerialNumber() As String
        Get
            Return m_strDOTSerialNumber
        End Get
        Set(ByVal value As String)
            m_strDOTSerialNumber = value
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

    Public Property Diameter() As Single
        Get
            Return m_sngDiameter
        End Get
        Set(ByVal value As Single)
            m_sngDiameter = value
        End Set
    End Property

    Public Property AvgSectionWidth() As Single
        Get
            Return m_sngAvgSectionWidth
        End Get
        Set(ByVal value As Single)
            m_sngAvgSectionWidth = value
        End Set
    End Property

    Public Property AvgOverallWidth() As Single
        Get
            Return m_sngAvgOverallWidth
        End Get
        Set(ByVal value As Single)
            m_sngAvgOverallWidth = value
        End Set
    End Property

    Public Property MaxOverallWidth() As Single
        Get
            Return m_sngMaxOverallWidth
        End Get
        Set(ByVal value As Single)
            m_sngMaxOverallWidth = value
        End Set
    End Property

    Public Property MinSizeFactor() As Single
        Get
            Return m_sngMinSizeFactor
        End Get
        Set(ByVal value As Single)
            m_sngMinSizeFactor = value
        End Set
    End Property
    Public Property ActSizeFactor() As Single
        Get
            Return m_sngActSizeFactor
        End Get
        Set(ByVal value As Single)
            m_sngActSizeFactor = value
        End Set
    End Property
    Public Property EndTime() As DateTime
        Get
            Return m_sngEndTime
        End Get
        Set(ByVal value As DateTime)
            m_sngEndTime = value
        End Set
    End Property

    '<DateTimeRangeValidator("2000-01-31T00:00:00", "2012-12-31T00:00:00")> _
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

    Public Property MountTemp() As Single
        Get
            Return m_sngMountTemp
        End Get
        Set(ByVal value As Single)
            m_sngMountTemp = value
        End Set
    End Property


    Public Property OriginalMeasure() As Measure
        Get
            Return m_objOriginalMeasure
        End Get
        Set(ByVal value As Measure)
            m_objOriginalMeasure = value
        End Set
    End Property

    Public Property StartInfPressure() As Short
        Get
            Return m_srtStartInfPressure
        End Get
        Set(ByVal value As Short)
            m_srtStartInfPressure = value
        End Set
    End Property
    Public Property EndInfPressure() As Short
        Get
            Return m_srtEndInfPressure
        End Get
        Set(ByVal value As Short)
            m_srtEndInfPressure = value
        End Set
    End Property
    Public Property Adjustment() As String
        Get
            Return m_strAdjustment
        End Get
        Set(ByVal value As String)
            m_strAdjustment = value
        End Set
    End Property
    Public Property Circumference() As Single
        Get
            Return m_sngCircumference
        End Get
        Set(ByVal value As Single)
            m_sngCircumference = value
        End Set
    End Property
    Public Property NominalDiameter() As Single
        Get
            Return m_sngNominalDiameter
        End Get
        Set(ByVal value As Single)
            m_sngNominalDiameter = value
        End Set
    End Property
    Public Property NominalWidth() As Single
        Get
            Return m_sngNominalWidth
        End Get
        Set(ByVal value As Single)
            m_sngNominalWidth = value
        End Set
    End Property
    Public Property NominalWidthPassYN() As Boolean
        Get
            Return m_blnNominalWithPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnNominalWithPassYN = value
        End Set
    End Property
    Public Property NominalWidthDifference() As Single
        Get
            Return m_sngNominalWidthDifference
        End Get
        Set(ByVal value As Single)
            m_sngNominalWidthDifference = value
        End Set
    End Property
    Public Property NominalWidthTolerance() As Single
        Get
            Return m_sngNominalWidthTolerance
        End Get
        Set(ByVal value As Single)
            m_sngNominalWidthTolerance = value
        End Set
    End Property
    Public Property MaxOverallDiameter() As Single
        Get
            Return m_sngMaxOverallDiameter
        End Get
        Set(ByVal value As Single)
            m_sngMaxOverallDiameter = value
        End Set
    End Property
    Public Property MinOverallDiameter() As Single
        Get
            Return m_sngMinOverallDiameter
        End Get
        Set(ByVal value As Single)
            m_sngMinOverallDiameter = value
        End Set
    End Property
    Public Property OverallWidthPassYN() As Boolean
        Get
            Return m_blnOverallWidthPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnOverallWidthPassYN = value
        End Set
    End Property
    Public Property OverallDiameterPassYN() As Boolean
        Get
            Return m_blnOverallDiameterPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnOverallDiameterPassYN = value
        End Set
    End Property
    Public Property DiameterDifference() As Single
        Get
            Return m_sngDiameterDifference
        End Get
        Set(ByVal value As Single)
            m_sngDiameterDifference = value
        End Set
    End Property
    Public Property DiameterTolerance() As Single
        Get
            Return m_sngDiameterTolerance
        End Get
        Set(ByVal value As Single)
            m_sngDiameterTolerance = value
        End Set
    End Property
    Public Property TensileStrength1() As Single
        Get
            Return m_sngTensileStrength1
        End Get
        Set(ByVal value As Single)
            m_sngTensileStrength1 = value
        End Set
    End Property
    Public Property TensileStrength2() As Single
        Get
            Return m_sngTensileStrength2
        End Get
        Set(ByVal value As Single)
            m_sngTensileStrength2 = value
        End Set
    End Property
    Public Property Elongation1() As Short
        Get
            Return m_srtElongation1
        End Get
        Set(ByVal value As Short)
            m_srtElongation1 = value
        End Set
    End Property
    Public Property Elongation2() As Short
        Get
            Return m_srtElongation2
        End Get
        Set(ByVal value As Short)
            m_srtElongation2 = value
        End Set
    End Property
    Public Property TensileStrengthAfterAging1() As Single
        Get
            Return m_sngTensileStrengthAfterAging1
        End Get
        Set(ByVal value As Single)
            m_sngTensileStrengthAfterAging1 = value
        End Set
    End Property
    Public Property TensileStrengthAfterAging2() As Single
        Get
            Return m_sngTensileStrengthAfterAging2
        End Get
        Set(ByVal value As Single)
            m_sngTensileStrengthAfterAging2 = value
        End Set
    End Property

    Public Property TemperatureResistanceGrading() As String
        Get
            Return m_sngTemperatureResistanceGrading
        End Get
        Set(ByVal value As String)
            m_sngTemperatureResistanceGrading = value
        End Set
    End Property

    Public Property GTSpecMaterialNumber() As String
        Get
            Return m_strGTSpecMatlNum
        End Get
        Set(ByVal value As String)
            m_strGTSpecMatlNum = value
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
    ''' Do Validate with specific rule set
    ''' </summary>
    ''' <param name="p_strRuleSetName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
