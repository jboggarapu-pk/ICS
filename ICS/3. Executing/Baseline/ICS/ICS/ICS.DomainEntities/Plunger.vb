Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Plunger properties
''' </summary>
''' <remarks></remarks>
Public Class Plunger

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

#Region "Members"

    Private m_iPlungerId As Short = 0
    Private m_strProjectNumber As String = String.Empty
    Private m_iTireNumber As Integer = 0
    Private m_strTestSpec As String = String.Empty
    'Added m_strOperation  as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Private m_strOperation As String = String.Empty
    Private m_dteCompletionDate As DateTime = DateTime.MinValue
    Private m_strDotSerialNumber As String = String.Empty

    Private m_dteSerialDate As DateTime = DateTime.MinValue
    Private m_lngMinPlunger As Long = 0

    Private m_iAVGBreakingEnergy As Integer = 0
    Private m_blnPassYN As Boolean = False
    Private m_iCertificationTypeId As Integer = 0
    Private m_intCertificateNumberID As Integer = 0
    Private m_intSkuId As Integer = 0
    Private m_strMatlNum As String = String.Empty

    Public PlungerDetails As List(Of PlungerDetail) = New List(Of PlungerDetail)

    Private m_objOriginalPlunger As Plunger = Nothing
    Private m_strGTSpecMatlNum As String = String.Empty
    Private m_strMFGWWYY As String = String.Empty
#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

#Region "Properties"

    Public Property PlungerId() As Short
        Get
            Return m_iPlungerId
        End Get
        Set(ByVal value As Short)
            m_iPlungerId = value
        End Set
    End Property
    Public Property MaterialNumber() As String
        Get
            Return m_strMatlNum
        End Get
        Set(ByVal value As String)
            m_strMatlNum = value
        End Set
    End Property

    '<NotNullValidator(MessageTemplate:="Project Number not null", Ruleset:=ValidatorAid.RuleSetName.NOM)> _
    '<NotNullValidator(MessageTemplate:="Project Number not null", Ruleset:=ValidatorAid.RuleSetName.GSO)> _
    '<StringLengthValidator(1, 6, MessageTemplate:="Project Number incorrect", Ruleset:=ValidatorAid.RuleSetName.NOM)> _
    '<StringLengthValidator(1, 6, MessageTemplate:="Project Number incorrect", Ruleset:=ValidatorAid.RuleSetName.GSO)> _
    Public Property ProjectNumber() As String
        Get
            Return m_strProjectNumber
        End Get
        Set(ByVal value As String)
            m_strProjectNumber = value
        End Set
    End Property

    '<NotNullValidator(MessageTemplate:="Tire Number not null", Ruleset:=ValidatorAid.RuleSetName.NOM)> _
    '<NotNullValidator(MessageTemplate:="Tire Number not null", Ruleset:=ValidatorAid.RuleSetName.GSO)> _
    '<RangeValidator(1, RangeBoundaryType.Inclusive, 9999, RangeBoundaryType.Inclusive, MessageTemplate:="Tire Number incorrect", Ruleset:=ValidatorAid.RuleSetName.NOM)> _
    '<RangeValidator(1, RangeBoundaryType.Inclusive, 9999, RangeBoundaryType.Inclusive, MessageTemplate:="Tire Number incorrect", Ruleset:=ValidatorAid.RuleSetName.GSO)> _
    Public Property TireNumber() As Integer
        Get
            Return m_iTireNumber
        End Get
        Set(ByVal value As Integer)
            m_iTireNumber = value
        End Set
    End Property

    '<NotNullValidator(MessageTemplate:="Test Spec not null", Ruleset:=ValidatorAid.RuleSetName.NOM)> _
    '<NotNullValidator(MessageTemplate:="Test Spec not null", Ruleset:=ValidatorAid.RuleSetName.GSO)> _
    '<StringLengthValidator(1, 7, MessageTemplate:="Test Spec incorrect", Ruleset:=ValidatorAid.RuleSetName.NOM)> _
    '<StringLengthValidator(1, 7, MessageTemplate:="Test Spec incorrect", Ruleset:=ValidatorAid.RuleSetName.GSO)> _
    Public Property TestSpec() As String
        Get
            Return m_strTestSpec
        End Get
        Set(ByVal value As String)
            m_strTestSpec = value
        End Set
    End Property
    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    '<NotNullValidator(MessageTemplate:="Operation not null", Ruleset:=ValidatorAid.RuleSetName.NOM)> _
    '<NotNullValidator(MessageTemplate:="Operation not null", Ruleset:=ValidatorAid.RuleSetName.GSO)> _
    '<StringLengthValidator(1, 4, MessageTemplate:="Operation incorrect", Ruleset:=ValidatorAid.RuleSetName.NOM)> _
    '<StringLengthValidator(1, 4, MessageTemplate:="Operation incorrect", Ruleset:=ValidatorAid.RuleSetName.GSO)> _
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
    Public Property SerialDate() As DateTime
        Get
            Return m_dteSerialDate
        End Get
        Set(ByVal value As DateTime)
            m_dteSerialDate = value
        End Set
    End Property
    Public Property MinPlunger() As Long
        Get
            Return m_lngMinPlunger
        End Get
        Set(ByVal value As Long)
            m_lngMinPlunger = value
        End Set
    End Property
    Public Property AVGBreakingEnergy() As Integer
        Get
            Return m_iAVGBreakingEnergy
        End Get
        Set(ByVal value As Integer)
            m_iAVGBreakingEnergy = value
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
    Public Property SkuId() As Integer
        Get
            Return m_intSkuId
        End Get
        Set(ByVal value As Integer)
            m_intSkuId = value
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

    Public Property OriginalPlunger() As Plunger
        Get
            Return m_objOriginalPlunger
        End Get
        Set(ByVal value As Plunger)
            m_objOriginalPlunger = value
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
            Dim results As ValidationResults = Validation.Validate(Of Plunger)(Me)
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
            Dim results As ValidationResults = Validation.Validate(Of Plunger)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region

End Class
