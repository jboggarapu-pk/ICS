Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Customer properties specific to NOM certification type
''' </summary>
''' <remarks>Object validation should be done for particular type (RuleSet)</remarks>
<HasSelfValidation()> _
Public Class Customer

#Region "Members"

    Private m_intSKUID As Integer = 0

    Private m_strCustomer_N As String = String.Empty
    Private m_strCustomerAddress_N As String = String.Empty
    Private m_strImporter_N As String = String.Empty
    Private m_strImporterAddress_N As String = String.Empty
    Private m_strImporterRepresentative_N As String = String.Empty
    Private m_strCountryLocation_N As String = String.Empty

#End Region

#Region "Properties"

    Public Property SKUID() As Integer
        Get
            Return m_intSKUID
        End Get
        Set(ByVal value As Integer)
            m_intSKUID = value
        End Set
    End Property

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

    Public Property CustomerAddress_N() As String
        Get
            Return m_strCustomerAddress_N
        End Get
        Set(ByVal value As String)
            m_strCustomerAddress_N = value
        End Set
    End Property

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

    '<NotNullValidator()> _
    '<StringLengthValidator(1, 100, MessageTemplate:="Country location must be 1-100 characters")> _
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
    ''' Do Validate with default (anonymous) rule set
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' Do Validate with specific rule set
    ''' </summary>
    ''' <param name="p_strRuleSetName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
