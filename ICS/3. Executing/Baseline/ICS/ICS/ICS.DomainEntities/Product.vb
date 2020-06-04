Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Product properties for all certification types
''' </summary>
''' <remarks></remarks>
<HasSelfValidation()> _
Public Class Product

    ' Changed sku to material number, ppn to tpn , nprid to psn and added brand and brand line instead of brand code as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"

    Private m_iSKUID As Integer = 0
    Private m_strMatlNum As String = String.Empty

    Private m_strBrandDesc As String = String.Empty
    Private m_strSerialDate As DateTime = DateTime.MinValue
    Private m_strDOTSerialNumber As String = String.Empty

    Private m_strBrand As String = String.Empty
    Private m_strBrandLine As String = String.Empty
    Private m_intTireTypeId As Integer = 0
    Private m_strPSN As String = String.Empty
    Private m_dteDiscontinuedDate As DateTime = DateTime.MinValue
    Private m_strSpecNumber As String = String.Empty

    Private m_strTireSizeStamp As String = String.Empty
    Private m_strSpeedRating As String = String.Empty
    Private m_strSingLoadIndex As String = String.Empty
    Private m_strDualLoadIndex As String = String.Empty
    Private m_strBiasBeltedRadial As String = String.Empty
    Private m_blnTubelessYN As Boolean = False
    Private m_blnReinforcedYN As Boolean = False
    Private m_blnExtraLoadYN As Boolean = False
    Private m_strUTQGTreadwear As String = String.Empty
    Private m_strUTQGTraction As String = String.Empty
    Private m_strUTQGTemp As String = String.Empty
    Private m_sngRimDiameter As Single = 0

    Private m_strLoadRange As String = String.Empty
    Private m_sngMeaRimWidth As Single = 0
    Private m_strRegroovableInd As Boolean = False
    Private m_strPlantProduced As String = String.Empty
    Private m_dteMostRecentTestDate As DateTime = DateTime.MinValue
    Private m_strIMark As String = String.Empty
    Private m_strTPN As String = String.Empty

    'Key In fields
    Private m_strInformeNumber As String = String.Empty
    Private m_dteFechaDate As DateTime = DateTime.MinValue
    Private m_strTreadPattern As String = String.Empty
    Private m_strSpecialProtectiveBand As String = String.Empty
    Private m_strNominalTireWidth As String = String.Empty
    Private m_strAspectRatio As String = String.Empty
    Private m_strTreadwearIndicator As String = String.Empty
    Private m_strNameOfManufacture As String = "Cooper Tire & Rubber Co."
    Private m_strFamily As String = String.Empty

    Private m_objOriginalProduct As Product = Nothing

    Private m_strDateOfManufactureWWYY As String = String.Empty
    Private m_strTypeId As String = String.Empty
    Private m_blnMudPlusSnow As Boolean = False
    Private m_blnSevereWeatherIndicator As Boolean = False
#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

#Region "Properties"

    Public Property SKUID() As Integer
        Get
            Return m_iSKUID
        End Get
        Set(ByVal value As Integer)
            m_iSKUID = value
        End Set
    End Property
    ' Changed validation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
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

    Public Property BrandDesc() As String
        Get
            Return m_strBrandDesc
        End Get
        Set(ByVal value As String)
            m_strBrandDesc = value
        End Set
    End Property

    Public Property SerialDate() As DateTime
        Get
            Return m_strSerialDate
        End Get
        Set(ByVal value As DateTime)
            m_strSerialDate = value
        End Set
    End Property

    Public Property DOTSerialNumber() As String
        Get
            Return m_strDOTSerialNumber
        End Get
        Set(ByVal value As String)
            m_strDOTSerialNumber = value
        End Set
    End Property
    ' Added brand property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Public Property Brand() As String
        Get
            Return m_strBrand
        End Get
        Set(ByVal value As String)
            m_strBrand = value
        End Set
    End Property
    ' Added brand line property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Public Property BrandLine() As String
        Get
            Return m_strBrandLine
        End Get
        Set(ByVal value As String)
            m_strBrandLine = value
        End Set
    End Property

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
    Public Property PSN() As String
        Get
            Return m_strPSN
        End Get
        Set(ByVal value As String)
            m_strPSN = value
        End Set
    End Property
    ' Changed ppn to tpn as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Public Property TPN() As String
        Get
            Return m_strTPN
        End Get
        Set(ByVal value As String)
            m_strTPN = value
        End Set
    End Property

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

    Public Property DiscontinuedDate() As DateTime
        Get
            Return m_dteDiscontinuedDate
        End Get
        Set(ByVal value As DateTime)
            m_dteDiscontinuedDate = value
        End Set
    End Property

    Public Property SpecNumber() As String
        Get
            Return m_strSpecNumber
        End Get
        Set(ByVal value As String)
            m_strSpecNumber = value
        End Set
    End Property

    Public Property SpeedRating() As String
        Get
            Return m_strSpeedRating
        End Get
        Set(ByVal value As String)
            m_strSpeedRating = value
        End Set
    End Property

    Public Property SingLoadIndex() As String
        Get
            Return m_strSingLoadIndex
        End Get
        Set(ByVal value As String)
            m_strSingLoadIndex = value
        End Set
    End Property

    Public Property DualLoadIndex() As String
        Get
            Return m_strDualLoadIndex
        End Get
        Set(ByVal value As String)
            m_strDualLoadIndex = value
        End Set
    End Property

    Public Property BiasBeltedRadial() As String
        Get
            Return m_strBiasBeltedRadial
        End Get
        Set(ByVal value As String)
            m_strBiasBeltedRadial = value
        End Set
    End Property

    Public Property TubelessYN() As Boolean
        Get
            Return m_blnTubelessYN
        End Get
        Set(ByVal value As Boolean)
            m_blnTubelessYN = value
        End Set
    End Property

    Public Property ReinforcedYN() As Boolean
        Get
            Return m_blnReinforcedYN
        End Get
        Set(ByVal value As Boolean)
            m_blnReinforcedYN = value
        End Set
    End Property

    Public Property ExtraLoadYN() As Boolean
        Get
            Return m_blnExtraLoadYN
        End Get
        Set(ByVal value As Boolean)
            m_blnExtraLoadYN = value
        End Set
    End Property

    Public Property UTQGTreadwear() As String
        Get
            Return m_strUTQGTreadwear
        End Get
        Set(ByVal value As String)
            m_strUTQGTreadwear = value
        End Set
    End Property

    Public Property UTQGTraction() As String
        Get
            Return m_strUTQGTraction
        End Get
        Set(ByVal value As String)
            m_strUTQGTraction = value
        End Set
    End Property

    Public Property UTQGTemp() As String
        Get
            Return m_strUTQGTemp
        End Get
        Set(ByVal value As String)
            m_strUTQGTemp = value
        End Set
    End Property

    Public Property LoadRange() As String
        Get
            Return m_strLoadRange
        End Get
        Set(ByVal value As String)
            m_strLoadRange = value
        End Set
    End Property

    '<RangeValidator(CType(0, Single), RangeBoundaryType.Inclusive, CType(9999.9, Single), RangeBoundaryType.Inclusive, MessageTemplate:="Must be in a range 0 - 9999")> _
    Public Property MeaRimWidth() As Single
        Get
            Return m_sngMeaRimWidth
        End Get
        Set(ByVal value As Single)
            m_sngMeaRimWidth = value
        End Set
    End Property

    Public Property RegroovableInd() As Boolean
        Get
            Return m_strRegroovableInd
        End Get
        Set(ByVal value As Boolean)
            m_strRegroovableInd = value
        End Set
    End Property

    Public Property PlantProduced() As String
        Get
            Return m_strPlantProduced
        End Get
        Set(ByVal value As String)
            m_strPlantProduced = value
        End Set
    End Property

    Public Property MostRecentTestDate() As DateTime
        Get
            Return m_dteMostRecentTestDate
        End Get
        Set(ByVal value As DateTime)
            m_dteMostRecentTestDate = value
        End Set
    End Property

    Public Property IMark() As String
        Get
            Return m_strIMark
        End Get
        Set(ByVal value As String)
            m_strIMark = value
        End Set
    End Property

    '<RangeValidator(0, RangeBoundaryType.Inclusive, 9999, RangeBoundaryType.Inclusive, MessageTemplate:="Must be in a range 0 - 9999")> _
    Public Property RimDiameter() As Single
        Get
            Return m_sngRimDiameter
        End Get
        Set(ByVal value As Single)
            m_sngRimDiameter = value
        End Set
    End Property

    Public Property OriginalProduct() As Product
        Get
            Return m_objOriginalProduct
        End Get
        Set(ByVal value As Product)
            m_objOriginalProduct = value
        End Set
    End Property

    Public Property InformeNumber() As String
        Get
            Return m_strInformeNumber
        End Get
        Set(ByVal value As String)
            m_strInformeNumber = value
        End Set
    End Property

    Public Property FechaDate() As DateTime
        Get
            Return m_dteFechaDate
        End Get
        Set(ByVal value As DateTime)
            m_dteFechaDate = value
        End Set
    End Property
    Public Property TreadPattern() As String
        Get
            Return m_strTreadPattern
        End Get
        Set(ByVal value As String)
            m_strTreadPattern = value
        End Set
    End Property

    Public Property SpecialProtectiveBand() As String
        Get
            Return m_strSpecialProtectiveBand
        End Get
        Set(ByVal value As String)
            m_strSpecialProtectiveBand = value
        End Set
    End Property

    Public Property NominalTireWidth() As String
        Get
            Return m_strNominalTireWidth
        End Get
        Set(ByVal value As String)
            m_strNominalTireWidth = value
        End Set
    End Property

    Public Property AspectRatio() As String
        Get
            Return m_strAspectRatio
        End Get
        Set(ByVal value As String)
            m_strAspectRatio = value
        End Set
    End Property

    Public Property TreadwearIndicator() As String
        Get
            Return m_strTreadwearIndicator
        End Get
        Set(ByVal value As String)
            m_strTreadwearIndicator = value
        End Set
    End Property

    Public Property NameOfManufacture() As String
        Get
            Return m_strNameOfManufacture
        End Get
        Set(ByVal value As String)
            m_strNameOfManufacture = value
        End Set
    End Property

    Public Property Family() As String
        Get
            Return m_strFamily
        End Get
        Set(ByVal value As String)
            m_strFamily = value
        End Set
    End Property

    <RegexValidator("^\s*$|^\d{4}$", MessageTemplate:="Manufacture Date(WWYY) must be a 4 digit numeric number")> _
    Public Property MFGWWYY() As String
        Get
            Return m_strDateOfManufactureWWYY
        End Get
        Set(ByVal value As String)
            m_strDateOfManufactureWWYY = value
        End Set
    End Property

    Public Property TireId() As String
        Get
            Return m_strTypeId
        End Get
        Set(ByVal value As String)
            m_strTypeId = value
        End Set
    End Property

    Public Property MudSnow() As Boolean
        Get
            Return m_blnMudPlusSnow
        End Get
        Set(ByVal value As Boolean)
            m_blnMudPlusSnow = value
        End Set
    End Property

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
            Dim results As ValidationResults = Validation.Validate(Of Product)(Me)
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
