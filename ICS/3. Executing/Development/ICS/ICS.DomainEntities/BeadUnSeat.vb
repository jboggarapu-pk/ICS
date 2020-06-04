Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators
Imports CooperTire.ICS.Common

''' <summary>
''' Measurement properties for all certification types
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
''' <term>Srinivas S</term>
''' <description>
''' <para>09/26/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class BeadUnSeat

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"

    ''' <summary>
    ''' Variable to hold the instance of short.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_iBeadUnSeatId As Short = 0

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strProjectNumber As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_iTireNumber As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestSpec As String = String.Empty

    'Added m_strOperation  as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strOperation As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of datetime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteCompletionDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strDotSerialNumber As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of datetime.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dteSerialDate As DateTime = DateTime.MinValue

    ''' <summary>
    ''' Variable to hold the instance of integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intMinBeadUnseat As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_iLowestUnSeatValue As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of boolean.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnPassYN As Boolean = False

    ''' <summary>
    ''' Variable to hold the instance of integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intSkuId As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of string.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMatlNum As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_iCertificationTypeId As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificateNumberID As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of list BeadUnSeatDetail
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnSeatDetails As List(Of BeadUnSeatDetail) = New List(Of BeadUnSeatDetail)

    ''' <summary>
    ''' Variable to hold the instance of BeadUnSeat object
    ''' </summary>
    ''' <remarks></remarks>
    Private m_objOriginalBeadUnSeat As BeadUnSeat = Nothing

    ''' <summary>
    ''' Variable to hold the instance of boolean
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnTestPassFail As Boolean = False

    ''' <summary>
    ''' Variable to hold the instance of string
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strGTSpecMatlNum As String = String.Empty

    ''' <summary>
    ''' Variable to hold the instance of string
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMFGWWYY As String = String.Empty
#End Region

#Region "Constructors"

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

#Region "Properties"

    ''' <summary>
    ''' Gets or sets Bead UnSeatId value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>BeadUnSeatId value.</returns>
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
    Public Property BeadUnSeatId() As Short
        Get
            Return m_iBeadUnSeatId
        End Get
        Set(ByVal value As Short)
            m_iBeadUnSeatId = value
        End Set
    End Property

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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
    ''' <summary>
    ''' Gets or sets Project Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>ProjectNumber value.</returns>
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
    ''' <summary>
    ''' Gets or sets Tire Number value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>TireNumber value.</returns>
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
    ''' <summary>
    ''' Gets or sets Test Spec value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>TestSpec value.</returns>
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
    '<StringLengthValidator(1, 4, MessageTemplate:="Test Spec incorrect", Ruleset:=ValidatorAid.RuleSetName.NOM)> _
    '<StringLengthValidator(1, 4, MessageTemplate:="Test Spec incorrect", Ruleset:=ValidatorAid.RuleSetName.GSO)> _
    ''' <summary>
    ''' Gets or sets Operation value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Operation value.</returns>
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
    Public Property Operation() As String
        Get
            Return m_strOperation
        End Get
        Set(ByVal value As String)
            m_strOperation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Completion Date value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>CompletionDate value.</returns>
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
    Public Property CompletionDate() As DateTime
        Get
            Return m_dteCompletionDate
        End Get
        Set(ByVal value As DateTime)
            m_dteCompletionDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Dot Serial Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>DotSerialNumber value.</returns>
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
    Public Property DotSerialNumber() As String
        Get
            Return m_strDotSerialNumber
        End Get
        Set(ByVal value As String)
            m_strDotSerialNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Serial Date value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>SerialDate value.</returns>
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

    Public Property SerialDate() As DateTime
        Get
            Return m_dteSerialDate
        End Get
        Set(ByVal value As DateTime)
            m_dteSerialDate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Min Bead Unseat value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>MinBeadUnseat value.</returns>
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
    Public Property MinBeadUnseat() As Integer
        Get
            Return m_intMinBeadUnseat
        End Get
        Set(ByVal value As Integer)
            m_intMinBeadUnseat = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Lowest UnSeat Value value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>LowestUnSeatValue value.</returns>
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
    Public Property LowestUnSeatValue() As Integer
        Get
            Return m_iLowestUnSeatValue
        End Get
        Set(ByVal value As Integer)
            m_iLowestUnSeatValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Pass Y or N value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>PassYN value.</returns>
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
    Public Property PassYN() As Boolean
        Get
            Return m_blnPassYN
        End Get
        Set(ByVal value As Boolean)
            m_blnPassYN = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Sku Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>SkuId value.</returns>
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
    Public Property SkuId() As Integer
        Get
            Return m_intSkuId
        End Get
        Set(ByVal value As Integer)
            m_intSkuId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certification Type Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>CertificationTypeId value.</returns>
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
    Public Property CertificationTypeId() As Integer
        Get
            Return m_iCertificationTypeId
        End Get
        Set(ByVal value As Integer)
            m_iCertificationTypeId = value
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
    ''' Gets or sets Original Bead UnSeat value.
    ''' </summary>
    ''' <value>BeadUnSeat</value>
    ''' <returns>OriginalBeadUnSeat value.</returns>
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
    Public Property OriginalBeadUnSeat() As BeadUnSeat
        Get
            Return m_objOriginalBeadUnSeat
        End Get
        Set(ByVal value As BeadUnSeat)
            m_objOriginalBeadUnSeat = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Pass Fail value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>TestPassFail value.</returns>
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
    Public Property TestPassFail() As Boolean
        Get
            Return m_blnTestPassFail
        End Get
        Set(ByVal value As Boolean)
            m_blnTestPassFail = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets GTSpec Material Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>GTSpecMaterialNumber value.</returns>
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
    Public Property GTSpecMaterialNumber() As String
        Get
            Return m_strGTSpecMatlNum
        End Get
        Set(ByVal value As String)
            m_strGTSpecMatlNum = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets MFGWWYY value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>MFGWWYY value.</returns>
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
            Dim results As ValidationResults = Validation.Validate(Of BeadUnSeat)(Me)
            blnValid = results.IsValid
        Catch excValidate As Exception
            EventLogger.Enter(excValidate)
            Throw excValidate
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
            Dim results As ValidationResults = Validation.Validate(Of BeadUnSeat)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch excValidate As Exception
            EventLogger.Enter(excValidate)
            Throw excValidate
        End Try
        Return blnValid

    End Function

#End Region

End Class
