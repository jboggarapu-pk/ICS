Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Certificate comprises properties from all certification types
''' </summary>
''' <remarks>Object validation should be done for particular type (RuleSet)</remarks>
<HasSelfValidation()> _
Public Class Certificate

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    'JBH_2.00 Project 5325 - Added Operation Approval Date and Mold Changed Flag

#Region "Members"

    Private m_strCertificationTypeName As String = String.Empty

    ' General Property
    Private m_strMatlNum As String = String.Empty
    Private m_intSKUID As Integer = 0
    Private m_blnRemoveMatlNum As Boolean = False
    Private m_blnAddNewImporter As Boolean = False
    Private m_blnAddNewCustomer As Boolean = False
    Private m_blnActSigReq As Boolean = False
    Private m_strCertificateNumber As String = String.Empty
    Private m_intCertificateNumberID As Integer = 0
    Private m_dteDateSubmitted As DateTime = DateTime.MinValue
    Private m_dteCertDateSubmitted As DateTime = DateTime.MinValue
    Private m_strActiveStatus As Boolean = False
    Private m_strSizeStamp As String = String.Empty
    Private m_strSingLoadIndex As String = String.Empty
    Private m_strDualLoadIndex As String = String.Empty
    Private m_strSpeedRating As String = String.Empty
    Private m_strBrandDesc As String = String.Empty
    Private m_strTubelessYN As String = String.Empty
    ' Added as per project 2706 technical specification
    Private m_strTPN As String = String.Empty
    Private m_dteDiscontinuedDate As DateTime = DateTime.MinValue

    ' Multiple used Property
    Private m_strSupplementalRequired_EI As Boolean = False
    Private m_strSupplementalNumber_EI As String = String.Empty
    Private m_intExtensionRevision_EN As Integer = 0
    Private m_strJobReportNumber_CEN As String = String.Empty
    Private m_dteDateAssigned_EGI As DateTime = DateTime.MinValue
    Private m_dteDateApproved_CEGI As DateTime = DateTime.MinValue
    Private m_dteCertDateApproved_CEGI As DateTime = DateTime.MinValue
    Private m_blnRenewalRequired_CGIN As Boolean = False
    Private m_dteOperDateApproved As DateTime = DateTime.MinValue       'JBH_2.00 Operation Approval Date
    Private m_blnMoldChgRequired As Boolean = False                     'JBH_2.00 Mold Change Required
    Private m_strAddInfo As String = String.Empty                       'jeseitz 10/29/2016 

    ' Single used property
    Private m_dteSupplementalDateAssigned_E As DateTime = DateTime.MinValue
    Private m_dteSupplementalDateSubmitted_E As DateTime = DateTime.MinValue
    Private m_dteSupplementalDateApproved_E As DateTime = DateTime.MinValue

    Private m_strProductLocation As String = String.Empty
    Private m_strSupplementalMoldStamping_E As String = String.Empty
    Private m_strBatchNumber_G As String = String.Empty
    Private m_strEmarkReference_I As String = String.Empty
    Private m_strImarkFamily_I As String = String.Empty
    Private m_dteExpiryDate_I As DateTime = DateTime.MinValue

    Private m_strCountryOfManufacture_N As String = String.Empty
    Private m_strCustomerSpecific_N As Boolean = False
    Private m_intCustomerId As Integer = 0
    Private m_strCustomer_N As String = String.Empty
    Private m_strCustomerAddress_N As String = String.Empty
    Private m_strImporter_N As String = String.Empty
    Private m_strImporterAddress_N As String = String.Empty
    Private m_strImporterRepresentative_N As String = String.Empty
    Private m_intImporterId As Integer = 0
    Private m_strCountryLocation_N As String = String.Empty
    Private m_strCompanyName As String = String.Empty
    Private m_strMoldStamping As String = String.Empty

    ' Imark specific
    Private m_objToBeRenewedCertificate_I As Certificate = Nothing
    ' Introduced for for auditing purposes
    Private m_objOriginalCertificate As Certificate = Nothing

#End Region

#Region "Properties"

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

    Public Property SKUID() As Integer
        Get
            Return m_intSKUID
        End Get
        Set(ByVal value As Integer)
            m_intSKUID = value
        End Set
    End Property

    Public Property lblSizeStamp() As String
        Get
            Return m_strSizeStamp
        End Get
        Set(ByVal value As String)
            m_strSizeStamp = value
        End Set
    End Property

    Public Property lblSingLoadIndex() As String
        Get
            Return m_strSingLoadIndex
        End Get
        Set(ByVal value As String)
            m_strSingLoadIndex = value
        End Set
    End Property

    Public Property lblDualLoadIndex() As String
        Get
            Return m_strDualLoadIndex
        End Get
        Set(ByVal value As String)
            m_strDualLoadIndex = value
        End Set
    End Property

    Public Property lblSpeedRating() As String
        Get
            Return m_strSpeedRating
        End Get
        Set(ByVal value As String)
            m_strSpeedRating = value
        End Set
    End Property

    Public Property lblBrandDesc() As String
        Get
            Return m_strBrandDesc
        End Get
        Set(ByVal value As String)
            m_strBrandDesc = value
        End Set
    End Property

    Public Property lblTubelessYN() As String
        Get
            Return m_strTubelessYN
        End Get
        Set(ByVal value As String)
            m_strTubelessYN = value
        End Set
    End Property

    Public Property ImporterID() As Integer
        Get
            Return m_intImporterId
        End Get
        Set(ByVal value As Integer)
            m_intImporterId = value
        End Set
    End Property

    Public Property CustomerID() As Integer
        Get
            Return m_intCustomerId
        End Get
        Set(ByVal value As Integer)
            m_intCustomerId = value
        End Set
    End Property

    Public Property RemoveMatlNum() As Boolean
        Get
            Return m_blnRemoveMatlNum
        End Get
        Set(ByVal value As Boolean)
            m_blnRemoveMatlNum = value
        End Set
    End Property

    Public Property AddNewImporter() As Boolean
        Get
            Return m_blnAddNewImporter
        End Get
        Set(ByVal value As Boolean)
            m_blnAddNewImporter = value
        End Set
    End Property

    Public Property AddNewCustomer() As Boolean
        Get
            Return m_blnAddNewCustomer
        End Get
        Set(ByVal value As Boolean)
            m_blnAddNewCustomer = value
        End Set
    End Property

    Public Property ActSigReq() As Boolean
        Get
            Return m_blnActSigReq
        End Get
        Set(ByVal value As Boolean)
            m_blnActSigReq = value
        End Set
    End Property

    Public Property CertificationTypeName() As String
        Get
            Return m_strCertificationTypeName
        End Get
        Set(ByVal value As String)
            m_strCertificationTypeName = value
        End Set
    End Property

    <NotNullValidator()> _
    <StringLengthValidator(3, 20, MessageTemplate:="CertificateNumber must be 3-20 characters long")> _
    Public Property CertificateNumber() As String
        Get
            Return m_strCertificateNumber
        End Get
        Set(ByVal value As String)
            m_strCertificateNumber = value
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
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr)> _
    Public Property CertDateSubmitted() As DateTime
        Get
            Return m_dteCertDateSubmitted
        End Get
        Set(ByVal value As DateTime)
            m_dteCertDateSubmitted = value
        End Set
    End Property

    Public Property CompanyName() As String
        Get
            Return m_strCompanyName
        End Get
        Set(ByVal value As String)
            m_strCompanyName = value
        End Set
    End Property

    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr)> _
    Public Property DateSubmitted() As DateTime
        Get
            Return m_dteDateSubmitted
        End Get
        Set(ByVal value As DateTime)
            m_dteDateSubmitted = value
        End Set
    End Property

    Public Property ActiveStatus() As Boolean
        Get
            Return m_strActiveStatus
        End Get
        Set(ByVal value As Boolean)
            m_strActiveStatus = value
        End Set
    End Property

    ' Specific Property
    '<PropertyComparisonValidator("ApprovedDate", ComparisonOperator.GreaterThanEqual)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.GSO)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Imark)> _
    Public Property DateAssigned_EGI() As DateTime
        Get
            Return m_dteDateAssigned_EGI
        End Get
        Set(ByVal value As DateTime)
            m_dteDateAssigned_EGI = value
        End Set
    End Property

    '    <PropertyComparisonValidator("SubmittedDate", ComparisonOperator.GreaterThanEqual)> _    
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.CCC)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.GSO)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Imark)> _
    Public Property CertDateApproved_CEGI() As DateTime
        Get
            Return m_dteCertDateApproved_CEGI
        End Get
        Set(ByVal value As DateTime)
            m_dteCertDateApproved_CEGI = value
        End Set
    End Property

    '    <PropertyComparisonValidator("SubmittedDate", ComparisonOperator.GreaterThanEqual)> _    
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.CCC)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.GSO)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Imark)> _
    Public Property DateApproved_CEGI() As DateTime
        Get
            Return m_dteDateApproved_CEGI
        End Get
        Set(ByVal value As DateTime)
            m_dteDateApproved_CEGI = value
        End Set
    End Property

    Public Property RenewalRequired_CGIN() As Boolean
        Get
            Return m_blnRenewalRequired_CGIN
        End Get
        Set(ByVal value As Boolean)
            m_blnRenewalRequired_CGIN = value
        End Set
    End Property

    Public Property SupplementalRequired_EI() As Boolean
        Get
            Return m_strSupplementalRequired_EI
        End Get
        Set(ByVal value As Boolean)
            m_strSupplementalRequired_EI = value
        End Set
    End Property

    Public Property SupplementalNumber_EI() As String
        Get
            Return m_strSupplementalNumber_EI
        End Get
        Set(ByVal value As String)
            m_strSupplementalNumber_EI = value
        End Set
    End Property

    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    Public Property SupplementalDateAssigned_E() As DateTime
        Get
            Return m_dteSupplementalDateAssigned_E
        End Get
        Set(ByVal value As DateTime)
            m_dteSupplementalDateAssigned_E = value
        End Set
    End Property

    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    Public Property SupplementalDateSubmitted_E() As DateTime
        Get
            Return m_dteSupplementalDateSubmitted_E
        End Get
        Set(ByVal value As DateTime)
            m_dteSupplementalDateSubmitted_E = value
        End Set
    End Property

    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    Public Property SupplementalDateApproved_E() As DateTime
        Get
            Return m_dteSupplementalDateApproved_E
        End Get
        Set(ByVal value As DateTime)
            m_dteSupplementalDateApproved_E = value
        End Set
    End Property

    Public Property JobReportNumber_CEN() As String
        Get
            Return m_strJobReportNumber_CEN
        End Get
        Set(ByVal value As String)
            m_strJobReportNumber_CEN = value
        End Set
    End Property

    '<StringLengthValidator(5, 14, MessageTemplate:="EmarkExtension must be 5 characters", _
    'Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    '<StringLengthValidator(5, 14, MessageTemplate:="EmarkExtension must be 5 characters", _
    'Ruleset:=ValidatorAid.RuleSetName.NOM)> _
    <RangeValidator(0, RangeBoundaryType.Inclusive, 99999, RangeBoundaryType.Inclusive, _
    Ruleset:=ValidatorAid.RuleSetName.Emark, _
    MessageTemplate:="Extension maximum is 5 digits")> _
    Public Property Extension_EN() As Integer
        Get
            Return m_intExtensionRevision_EN
        End Get
        Set(ByVal value As Integer)
            m_intExtensionRevision_EN = value
        End Set
    End Property

    Public Property SupplementalMoldStamping_E() As String
        Get
            Return m_strSupplementalMoldStamping_E
        End Get
        Set(ByVal value As String)
            m_strSupplementalMoldStamping_E = value
        End Set
    End Property

    Public Property EmarkReference_I() As String
        Get
            Return m_strEmarkReference_I
        End Get
        Set(ByVal value As String)
            m_strEmarkReference_I = value
        End Set
    End Property

    '<DateTimeRangeValidator(MinDateStr, MaxDateStr)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.CCC)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Imark)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.India_Mark)> _
   Public Property ExpiryDate_I() As DateTime
        Get
            Return m_dteExpiryDate_I
        End Get
        Set(ByVal value As DateTime)
            m_dteExpiryDate_I = value
        End Set
    End Property

    Public Property Family_I() As String
        Get
            Return m_strImarkFamily_I
        End Get
        Set(ByVal value As String)
            m_strImarkFamily_I = value
        End Set
    End Property

    Public Property MoldStamping() As String
        Get
            Return m_strMoldStamping
        End Get
        Set(ByVal value As String)
            m_strMoldStamping = value
        End Set
    End Property

    Public Property ProductLocation() As String
        Get
            Return m_strProductLocation
        End Get
        Set(ByVal value As String)
            m_strProductLocation = value
        End Set
    End Property

    Public Property CountryOfManufacture_N() As String
        Get
            Return m_strCountryOfManufacture_N
        End Get
        Set(ByVal value As String)
            m_strCountryOfManufacture_N = value
        End Set
    End Property

    Public Property Customer_N() As String
        Get
            Return m_strCustomer_N
        End Get
        Set(ByVal value As String)
            m_strCustomer_N = value
        End Set
    End Property

    Public Property CustomerSpecific_N() As Boolean
        Get
            Return m_strCustomerSpecific_N
        End Get
        Set(ByVal value As Boolean)
            m_strCustomerSpecific_N = value
        End Set
    End Property

    Public Property Importer_N() As String
        Get
            Return m_strImporter_N
        End Get
        Set(ByVal value As String)
            m_strImporter_N = value
        End Set
    End Property

    Public Property ImporterAddress_N() As String
        Get
            Return m_strImporterAddress_N
        End Get
        Set(ByVal value As String)
            m_strImporterAddress_N = value
        End Set
    End Property

    Public Property CustomerAddress_N() As String
        Get
            Return m_strCustomerAddress_N
        End Get
        Set(ByVal value As String)
            m_strCustomerAddress_N = value
        End Set
    End Property

    Public Property ImporterRepresentative_N() As String
        Get
            Return m_strImporterRepresentative_N
        End Get
        Set(ByVal value As String)
            m_strImporterRepresentative_N = value
        End Set
    End Property

    Public Property CountryLocation_N() As String
        Get
            Return m_strCountryLocation_N
        End Get
        Set(ByVal value As String)
            m_strCountryLocation_N = value
        End Set
    End Property

    ' GSO
    Public Property BatchNumber_G() As String
        Get
            Return m_strBatchNumber_G
        End Get
        Set(ByVal value As String)
            m_strBatchNumber_G = value
        End Set
    End Property

    Public Property OriginalCertificate() As Certificate
        Get
            Return m_objOriginalCertificate
        End Get
        Set(ByVal value As Certificate)
            m_objOriginalCertificate = value
        End Set
    End Property

    Public Property ToBeRenewedCertificate_I() As Certificate
        Get
            Return m_objToBeRenewedCertificate_I
        End Get
        Set(ByVal value As Certificate)
            m_objToBeRenewedCertificate_I = value
        End Set
    End Property

    'Added as per project 2706 technical specification
    Public Property TPN() As String
        Get
            Return m_strTPN
        End Get
        Set(ByVal value As String)
            m_strTPN = value
        End Set
    End Property

    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr)> _
    Public Property DiscontinuedDate() As DateTime
        Get
            Return m_dteDiscontinuedDate
        End Get
        Set(ByVal value As DateTime)
            m_dteDiscontinuedDate = value
        End Set
    End Property

    'JBH_2.00 Project 5325 - Add - Mold Change Required
    Public Property MoldChgRequired() As Boolean
        Get
            Return m_blnMoldChgRequired
        End Get
        Set(ByVal value As Boolean)
            m_blnMoldChgRequired = value
        End Set
    End Property

    'JBH_2.00 Project 5325 - Add - Operation Approval Date
    '    <PropertyComparisonValidator("SubmittedDate", ComparisonOperator.GreaterThanEqual)> _    
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.CCC)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.India_Mark)> _
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Imark)> _
    Public Property OperDateApproved() As DateTime
        Get
            Return m_dteOperDateApproved
        End Get
        Set(ByVal value As DateTime)
            m_dteOperDateApproved = value
        End Set
    End Property

    'Added for request 203625 Additional Info
    Public Property AddInfo() As String
        Get
            Return m_strAddInfo
        End Get
        Set(ByVal value As String)
            m_strAddInfo = value
        End Set
    End Property
 

#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

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
    ''' Method is used for self validate specific to certification type (Ruleset)
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks></remarks>
    <SelfValidation(Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    Public Sub SelfValidateSupplementalRequired(ByVal results As ValidationResults)

        'If SupplementalRequired_EI And String.IsNullOrEmpty(SupplementalNumber_EI) Then
        'NOTE: this is implementation example (add to the view)
        '    results.AddResult(New ValidationResult("Supplemental Id is required", Me, Nothing, Nothing, Nothing))
        'End If

    End Sub

    ''' <summary>
    ''' Method is used for validation specific to certification type (Ruleset)
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks></remarks>
    <SelfValidation(Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    Private Sub SelfValidateDates(ByVal results As ValidationResults)
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
            Dim results As ValidationResults = Validation.Validate(Of Certificate)(Me)
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
            Dim results As ValidationResults = Validation.Validate(Of Certificate)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region

End Class
