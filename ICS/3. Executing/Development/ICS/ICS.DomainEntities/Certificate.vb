Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators
Imports CooperTire.ICS.Common


''' <summary>
''' Certificate comprises properties from all certification types
''' </summary>
''' <remarks>
''' <list type="table">
''' <listheader>
''' <term>Author</term>
''' <description>Object validation should be done for particular type (RuleSet)</description>
''' </listheader>    
''' <item>
''' <term>NA</term>
''' <description>
''' <para>NA</para>
''' <para>Original Code.</para>
''' </description>
''' </item>
''' <item>
''' <term>Srinivas S</term>
''' <description>
''' <para>09/26/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
<HasSelfValidation()> _
Public Class Certificate

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    'JBH_2.00 Project 5325 - Added Operation Approval Date and Mold Changed Flag

#Region "Certificate class Private Members"

    ''' <summary>
    ''' Constant to hold the Material Number Validation Text.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MaterialNumberText As String = "Material Number must be 11-18 characters"

    ''' <summary>
    ''' Constant to hold the Certificate Number Validation Text.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CertificateNumberMessage As String = "CertificateNumber must be 3-20 characters long"

    ''' <summary>
    ''' Constant to hold the Extension Validation Text.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ExtensionMessage As String = "Extension maximum is 5 digits"

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCertificationTypeName As String = String.Empty

    ' General Property

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMatlNum As String = String.Empty

    ''' <summary>
    ''' Variable to hold the value of Integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intSKUID As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of Boolean.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnRemoveMatlNum As Boolean = False

    ''' <summary>
    ''' Variable to hold the instance of Boolean.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnAddNewImporter As Boolean = False

    ''' <summary>
    ''' Variable to hold the instance of Boolean.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnAddNewCustomer As Boolean = False

    ''' <summary>
    ''' Variable to hold the instance of Boolean.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnActSigReq As Boolean = False

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCertificateNumber As String = String.Empty

    ''' <summary>
    ''' Variable to hold the value of integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificateNumberID As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of DateTime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteDateSubmitted As DateTime = DateTime.MinValue

    ''' <summary>
    ''' Variable to hold the instance of DateTime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteCertDateSubmitted As DateTime = DateTime.MinValue

    ''' <summary>
    ''' Variable to hold the instance of Boolean.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strActiveStatus As Boolean = False

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSizeStamp As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSingLoadIndex As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strDualLoadIndex As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSpeedRating As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strBrandDesc As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTubelessYN As String = String.Empty

    ' Added as per project 2706 technical specification
    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTPN As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of DateTime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteDiscontinuedDate As DateTime = DateTime.MinValue

    ' Multiple used Property
    ''' <summary>
    ''' Variable to hold the instance of Boolean.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSupplementalRequired_EI As Boolean = False

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSupplementalNumber_EI As String = String.Empty

    ''' <summary>
    ''' Variable to hold the value of Integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intExtensionRevision_EN As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strJobReportNumber_CEN As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of DateTime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteDateAssigned_EGI As DateTime = DateTime.MinValue

    ''' <summary>
    ''' Variable to hold the instance of DateTime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteDateApproved_CEGI As DateTime = DateTime.MinValue

    ''' <summary>
    ''' Variable to hold the instance of DateTime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteCertDateApproved_CEGI As DateTime = DateTime.MinValue

    ''' <summary>
    ''' Variable to hold the instance of Boolean.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnRenewalRequired_CGIN As Boolean = False

    ''' <summary>
    ''' Variable to hold the instance of DateTime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteOperDateApproved As DateTime = DateTime.MinValue       'JBH_2.00 Operation Approval Date

    ''' <summary>
    ''' Variable to hold the instance of Boolean.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnMoldChgRequired As Boolean = False                     'JBH_2.00 Mold Change Required

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strAddInfo As String = String.Empty                       'jeseitz 10/29/2016 

    ' Single used property
    ''' <summary>
    ''' Variable to hold the instance of DateTime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteSupplementalDateAssigned_E As DateTime = DateTime.MinValue
    ''' <summary>
    ''' Variable to hold the instance of DateTime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteSupplementalDateSubmitted_E As DateTime = DateTime.MinValue
    ''' <summary>
    ''' Variable to hold the instance of DateTime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteSupplementalDateApproved_E As DateTime = DateTime.MinValue

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strProductLocation As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSupplementalMoldStamping_E As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strBatchNumber_G As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strEmarkReference_I As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strImarkFamily_I As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of DateTime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteExpiryDate_I As DateTime = DateTime.MinValue


    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCountryOfManufacture_N As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of Boolean.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCustomerSpecific_N As Boolean = False

    ''' <summary>
    ''' Variable to hold the value of Integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCustomerId As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCustomer_N As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCustomerAddress_N As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strImporter_N As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strImporterAddress_N As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strImporterRepresentative_N As String = String.Empty

    ''' <summary>
    ''' Variable to hold the value of Integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intImporterId As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCountryLocation_N As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCompanyName As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMoldStamping As String = String.Empty

    ' Imark specific
    ''' <summary>
    ''' Variable to hold the instance of Certificate.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_objToBeRenewedCertificate_I As Certificate = Nothing

    ' Introduced for for auditing purposes
    ''' <summary>
    ''' Variable to hold the instance of Certificate.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_objOriginalCertificate As Certificate = Nothing

#End Region

#Region " Certificate Class Properties"

    ''' <summary>
    ''' Gets or sets Material Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>MaterialNumber value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <NotNullValidator()> _
        <StringLengthValidator(11, 18, MessageTemplate:=MaterialNumberText)> _
    Public Property MaterialNumber() As String
        Get
            Return m_strMatlNum
        End Get
        Set(ByVal value As String)
            m_strMatlNum = value
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets sku Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>SKUID value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
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
    ''' Gets or sets lbl Size Stamp value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>lblSizeStamp value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property lblSizeStamp() As String
        Get
            Return m_strSizeStamp
        End Get
        Set(ByVal value As String)
            m_strSizeStamp = value
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets lbl Sing Load Index value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>lblSingLoadIndex value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property lblSingLoadIndex() As String
        Get
            Return m_strSingLoadIndex
        End Get
        Set(ByVal value As String)
            m_strSingLoadIndex = value
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets lblDual Load Index value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>lblDualLoadIndex value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property lblDualLoadIndex() As String
        Get
            Return m_strDualLoadIndex
        End Get
        Set(ByVal value As String)
            m_strDualLoadIndex = value
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets lblSpeed Rating value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>lblSpeedRating value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property lblSpeedRating() As String
        Get
            Return m_strSpeedRating
        End Get
        Set(ByVal value As String)
            m_strSpeedRating = value
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets lblBrand Desc value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>lblBrandDesc value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property lblBrandDesc() As String
        Get
            Return m_strBrandDesc
        End Get
        Set(ByVal value As String)
            m_strBrandDesc = value
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets lblTube less Y or N value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>lblTubelessYN value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property lblTubelessYN() As String
        Get
            Return m_strTubelessYN
        End Get
        Set(ByVal value As String)
            m_strTubelessYN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Importer ID value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>ImporterID value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ImporterID() As Integer
        Get
            Return m_intImporterId
        End Get
        Set(ByVal value As Integer)
            m_intImporterId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Customer ID value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>CustomerID value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CustomerID() As Integer
        Get
            Return m_intCustomerId
        End Get
        Set(ByVal value As Integer)
            m_intCustomerId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Remove Matl Num value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>RemoveMatlNum value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property RemoveMatlNum() As Boolean
        Get
            Return m_blnRemoveMatlNum
        End Get
        Set(ByVal value As Boolean)
            m_blnRemoveMatlNum = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Add New Importer value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>AddNewImporter value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property AddNewImporter() As Boolean
        Get
            Return m_blnAddNewImporter
        End Get
        Set(ByVal value As Boolean)
            m_blnAddNewImporter = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Add New Customer value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>AddNewCustomer value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property AddNewCustomer() As Boolean
        Get
            Return m_blnAddNewCustomer
        End Get
        Set(ByVal value As Boolean)
            m_blnAddNewCustomer = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Act Sig Req value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>ActSigReq value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ActSigReq() As Boolean
        Get
            Return m_blnActSigReq
        End Get
        Set(ByVal value As Boolean)
            m_blnActSigReq = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certification Type Name value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>CertificationTypeName value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertificationTypeName() As String
        Get
            Return m_strCertificationTypeName
        End Get
        Set(ByVal value As String)
            m_strCertificationTypeName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>CertificateNumber value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <NotNullValidator()> _
    <StringLengthValidator(3, 20, MessageTemplate:=CertificateNumberMessage)> _
    Public Property CertificateNumber() As String
        Get
            Return m_strCertificateNumber
        End Get
        Set(ByVal value As String)
            m_strCertificateNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Number ID value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>CertificateNumberID value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
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
    ''' Gets or sets Cert Date Submitted value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>CertDateSubmitted value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr)> _
    Public Property CertDateSubmitted() As DateTime
        Get
            Return m_dteCertDateSubmitted
        End Get
        Set(ByVal value As DateTime)
            m_dteCertDateSubmitted = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Company Name value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>CompanyName value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CompanyName() As String
        Get
            Return m_strCompanyName
        End Get
        Set(ByVal value As String)
            m_strCompanyName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Date Submitted value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>DateSubmitted value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr)> _
    Public Property DateSubmitted() As DateTime
        Get
            Return m_dteDateSubmitted
        End Get
        Set(ByVal value As DateTime)
            m_dteDateSubmitted = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Active Status value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>ActiveStatus value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
    ''' <summary>
    ''' Gets or sets Date Assigned EGI value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>DateAssigned_EGI value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
    ''' <summary>
    ''' Gets or sets Cert Date Approved_CEGI value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>CertDateApproved CEGI value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
    ''' <summary>
    ''' Gets or sets Date Approved CEGI value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>DateApproved_CEGI value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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

    ''' <summary>
    ''' Gets or sets Renewal Required CGIN value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>RenewalRequired_CGIN value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property RenewalRequired_CGIN() As Boolean
        Get
            Return m_blnRenewalRequired_CGIN
        End Get
        Set(ByVal value As Boolean)
            m_blnRenewalRequired_CGIN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Supplemental Required EI value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>SupplementalRequired_EI value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SupplementalRequired_EI() As Boolean
        Get
            Return m_strSupplementalRequired_EI
        End Get
        Set(ByVal value As Boolean)
            m_strSupplementalRequired_EI = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Supplemental Number EI value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>SupplementalNumber_EI value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SupplementalNumber_EI() As String
        Get
            Return m_strSupplementalNumber_EI
        End Get
        Set(ByVal value As String)
            m_strSupplementalNumber_EI = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Supplemental Date Assigned E value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>SupplementalDateAssigned_E value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    Public Property SupplementalDateAssigned_E() As DateTime
        Get
            Return m_dteSupplementalDateAssigned_E
        End Get
        Set(ByVal value As DateTime)
            m_dteSupplementalDateAssigned_E = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Supplemental Date Submitted E value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>SupplementalDateSubmitted_E value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    Public Property SupplementalDateSubmitted_E() As DateTime
        Get
            Return m_dteSupplementalDateSubmitted_E
        End Get
        Set(ByVal value As DateTime)
            m_dteSupplementalDateSubmitted_E = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Supplemental Date Approved E value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>SupplementalDateApproved_E value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <DateTimeRangeValidator(ValidatorAid.MinDateStr, ValidatorAid.MaxDateStr, Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    Public Property SupplementalDateApproved_E() As DateTime
        Get
            Return m_dteSupplementalDateApproved_E
        End Get
        Set(ByVal value As DateTime)
            m_dteSupplementalDateApproved_E = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Job Report Number CEN value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>JobReportNumber_CEN value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
    ''' <summary>
    ''' Gets or sets Extension EN value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Extension_EN value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <RangeValidator(0, RangeBoundaryType.Inclusive, 99999, RangeBoundaryType.Inclusive, _
    Ruleset:=ValidatorAid.RuleSetName.Emark, _
    MessageTemplate:=ExtensionMessage)> _
    Public Property Extension_EN() As Integer
        Get
            Return m_intExtensionRevision_EN
        End Get
        Set(ByVal value As Integer)
            m_intExtensionRevision_EN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Supplemental Mold Stamping E value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>SupplementalMoldStamping_E value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SupplementalMoldStamping_E() As String
        Get
            Return m_strSupplementalMoldStamping_E
        End Get
        Set(ByVal value As String)
            m_strSupplementalMoldStamping_E = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Emark Reference I value.
    ''' </summary> 
    ''' <value>String</value>
    ''' <returns>EmarkReference_I value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property EmarkReference_I() As String
        Get
            Return m_strEmarkReference_I
        End Get
        Set(ByVal value As String)
            m_strEmarkReference_I = value
        End Set
    End Property

    '<DateTimeRangeValidator(MinDateStr, MaxDateStr)> _
    ''' <summary>
    ''' Gets or sets Expiry Date I value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>ExpiryDate_I value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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

    ''' <summary>
    ''' Gets or sets Family I value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Family_I value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Family_I() As String
        Get
            Return m_strImarkFamily_I
        End Get
        Set(ByVal value As String)
            m_strImarkFamily_I = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Mold Stamping value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>MoldStamping value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MoldStamping() As String
        Get
            Return m_strMoldStamping
        End Get
        Set(ByVal value As String)
            m_strMoldStamping = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Product Location value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>ProductLocation value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ProductLocation() As String
        Get
            Return m_strProductLocation
        End Get
        Set(ByVal value As String)
            m_strProductLocation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Country Of Manufacture N value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>CountryOfManufacture_N value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CountryOfManufacture_N() As String
        Get
            Return m_strCountryOfManufacture_N
        End Get
        Set(ByVal value As String)
            m_strCountryOfManufacture_N = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Customer N value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Customer_N value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Customer_N() As String
        Get
            Return m_strCustomer_N
        End Get
        Set(ByVal value As String)
            m_strCustomer_N = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Customer Specific N value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>CustomerSpecific_N value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CustomerSpecific_N() As Boolean
        Get
            Return m_strCustomerSpecific_N
        End Get
        Set(ByVal value As Boolean)
            m_strCustomerSpecific_N = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Importer N value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Importer_N value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Importer_N() As String
        Get
            Return m_strImporter_N
        End Get
        Set(ByVal value As String)
            m_strImporter_N = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Importer Address N value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>ImporterAddress_N value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ImporterAddress_N() As String
        Get
            Return m_strImporterAddress_N
        End Get
        Set(ByVal value As String)
            m_strImporterAddress_N = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Customer Address N value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>CustomerAddress_N value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CustomerAddress_N() As String
        Get
            Return m_strCustomerAddress_N
        End Get
        Set(ByVal value As String)
            m_strCustomerAddress_N = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Importer Representative N value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>ImporterRepresentative_N value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ImporterRepresentative_N() As String
        Get
            Return m_strImporterRepresentative_N
        End Get
        Set(ByVal value As String)
            m_strImporterRepresentative_N = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Country Location N value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>CountryLocation_N value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CountryLocation_N() As String
        Get
            Return m_strCountryLocation_N
        End Get
        Set(ByVal value As String)
            m_strCountryLocation_N = value
        End Set
    End Property

    ' GSO
    ''' <summary>
    ''' Gets or sets Batch Number G value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>BatchNumber_G value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property BatchNumber_G() As String
        Get
            Return m_strBatchNumber_G
        End Get
        Set(ByVal value As String)
            m_strBatchNumber_G = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Original Certificate value.
    ''' </summary>
    ''' <value>Certificate</value>
    ''' <returns>OriginalCertificate value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property OriginalCertificate() As Certificate
        Get
            Return m_objOriginalCertificate
        End Get
        Set(ByVal value As Certificate)
            m_objOriginalCertificate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets To Be Renewed Certificate I value.
    ''' </summary>
    ''' <value>Certificate</value>
    ''' <returns>ToBeRenewedCertificate_I value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ToBeRenewedCertificate_I() As Certificate
        Get
            Return m_objToBeRenewedCertificate_I
        End Get
        Set(ByVal value As Certificate)
            m_objToBeRenewedCertificate_I = value
        End Set
    End Property

    'Added as per project 2706 technical specification
    ''' <summary>
    ''' Gets or sets TPN value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>TPN value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TPN() As String
        Get
            Return m_strTPN
        End Get
        Set(ByVal value As String)
            m_strTPN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Discontinued Date value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>DiscontinuedDate value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
    ''' <summary>
    ''' Gets or sets Mold Chg Required value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>MoldChgRequired value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
    ''' <summary>
    ''' Gets or sets Oper Date Approved value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>OperDateApproved value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
    ''' <summary>
    ''' Gets or sets Add Info value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>AddInfo value.</returns>
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property AddInfo() As String
        Get
            Return m_strAddInfo
        End Get
        Set(ByVal value As String)
            m_strAddInfo = value
        End Set
    End Property


#End Region

#Region "Certificate class Constructors"

    ''' <summary>   
    ''' Constructor to initialize class members.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>    
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

    End Sub

#End Region

#Region "Certificate class Methods"

    ''' <summary>
    ''' Method is used for default (anonymous) validation
    ''' </summary>
    ''' <param name="p_objResults">Validation Results</param>
    ''' <exception cref="Exception">
    ''' </exception>   
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    <SelfValidation()> _
    Private Sub SelfValidate(ByVal p_objResults As ValidationResults)
        'NOTE: this is implementation example.
        '        If m_val1 < m_val2 Then
        '            results.AddResult(New ValidationResult("Message", Me, Nothing, Nothing, Nothing))
        '        End If

    End Sub

    ''' <summary>
    ''' Method is used for self validate specific to certification type (Ruleset)
    ''' </summary>
    ''' <param name="p_objResults">Validation Results</param>
    ''' <exception cref="Exception">
    ''' </exception>   
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    <SelfValidation(Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    Public Sub SelfValidateSupplementalRequired(ByVal p_objResults As ValidationResults)

        'If SupplementalRequired_EI And String.IsNullOrEmpty(SupplementalNumber_EI) Then
        'NOTE: this is implementation example (add to the view)
        '    results.AddResult(New ValidationResult("Supplemental Id is required", Me, Nothing, Nothing, Nothing))
        'End If

    End Sub

    ''' <summary>
    ''' Method is used for validation specific to certification type (Ruleset)
    ''' </summary>
    ''' <param name="p_objResults">Validation Results</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>   
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    <SelfValidation(Ruleset:=ValidatorAid.RuleSetName.Emark)> _
    Private Sub SelfValidateDates(ByVal p_objResults As ValidationResults)
        'NOTE: this is implementation example.
        '        If m_val1 < m_val2 Then
        '            results.AddResult(New ValidationResult("Message", Me, Nothing, Nothing, Nothing))
        '        End If

    End Sub

    ''' <summary>
    ''' Do Validate with default (anonymous) rule set
    ''' </summary>
    ''' <returns>Returns boolean status value.</returns>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>   
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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
    ''' <param name="p_strRuleSetName">Rule Set Name</param>
    ''' <returns>Returns boolean status value.</returns>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>   
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
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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
