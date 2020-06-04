Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common
 
''' <summary>
''' Class contains Customer properties specific to NOM certification type.
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
''' <term>Sujitha</term>
''' <description>
''' <para>10/17/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
<HasSelfValidation()> _
Public Class Customer

#Region "Members"

    ''' <summary>
    ''' variable to hold SKU Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intSKUID As Integer = 0

    ''' <summary>
    ''' variable to hold Customer_N.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCustomer_N As String = String.Empty

    ''' <summary>
    ''' variable to hold Customer Address_N.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCustomerAddress_N As String = String.Empty

    ''' <summary>
    ''' variable to hold Importer_N.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strImporter_N As String = String.Empty

    ''' <summary>
    ''' variable to hold Importer Address_N.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strImporterAddress_N As String = String.Empty

    ''' <summary>
    ''' variable to hold Importer Representative_N.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strImporterRepresentative_N As String = String.Empty

    ''' <summary>
    ''' variable to hold SKU Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCountryLocation_N As String = String.Empty

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets SKU ID value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>SKU ID.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
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
    ''' Gets or sets Customer_N Value.
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
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <NotNullValidator()> _
    <StringLengthValidator(1, 100, MessageTemplate:="Customer must be 1-100 characters")> _
    Public Property Customer_N() As String
        Get
            Return m_strCustomer_N
        End Get
        Set(ByVal value As String)
            m_strCustomer_N = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Importer_N Value.
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
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <NotNullValidator()> _
    <StringLengthValidator(1, 100, MessageTemplate:="Importer must be 1-100 characters")> _
    Public Property Importer_N() As String
        Get
            Return m_strImporter_N
        End Get
        Set(ByVal value As String)
            m_strImporter_N = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Importer Address_N Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Importer Address_N value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <NotNullValidator()> _
    <StringLengthValidator(1, 200, MessageTemplate:="Importer address must be 1-200 characters")> _
    Public Property ImporterAddress_N() As String
        Get
            Return m_strImporterAddress_N
        End Get
        Set(ByVal value As String)
            m_strImporterAddress_N = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Customer Address_N Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Customer Address_N value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
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
    ''' Gets or sets Importer Representative_N Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Importer Representative_N value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    <NotNullValidator()> _
    <StringLengthValidator(1, 200, MessageTemplate:="Importer representative must be 1-200 characters")> _
    Public Property ImporterRepresentative_N() As String
        Get
            Return m_strImporterRepresentative_N
        End Get
        Set(ByVal value As String)
            m_strImporterRepresentative_N = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Country Location_N Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Country Location_N value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Method is used for default (anonymous) validation.
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    <SelfValidation()> _
    Private Sub SelfValidate(ByVal results As ValidationResults)

    End Sub

    ''' <summary>
    ''' Do Validate with default (anonymous) rule set.
    ''' </summary> 
    ''' <returns>Boolean.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>     
    Public Function DoValidate() As Boolean

        Dim blnValid As Boolean = False
        Try
            Dim results As ValidationResults = Validation.Validate(Of Customer)(Me)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

    ''' <summary>
    ''' Do Validate with specific rule set.
    ''' </summary>
    ''' <param name="p_strRuleSetName">RuleSet name.</param>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>       
    Public Function DoValidate(ByVal p_strRuleSetName As String) As Boolean

        Dim blnValid As Boolean = False
        Try
            Dim results As ValidationResults = Validation.Validate(Of Customer)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region

End Class
