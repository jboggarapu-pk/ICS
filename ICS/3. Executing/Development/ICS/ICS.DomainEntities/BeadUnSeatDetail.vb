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
Public Class BeadUnSeatDetail

#Region "Members"

    ''' <summary>
    ''' Variable to hold the instance of short.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intBeadUnSeatId As Short = 0

    ''' <summary>
    ''' Variable to hold the instance of Integer.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intUnSeatForce As Integer = 0

    ''' <summary>
    ''' Variable to hold the instance of short.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intIteration As Short = 0

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
            Return m_intBeadUnSeatId
        End Get
        Set(ByVal value As Short)
            m_intBeadUnSeatId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets UnSeat Force value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>UnSeatForce value.</returns>
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
    Public Property UnSeatForce() As Integer
        Get
            Return m_intUnSeatForce
        End Get
        Set(ByVal value As Integer)
            m_intUnSeatForce = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Iteration value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Iteration value.</returns>
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
    Public Property Iteration() As Short
        Get
            Return m_intIteration
        End Get
        Set(ByVal value As Short)
            m_intIteration = value
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
            Dim results As ValidationResults = Validation.Validate(Of BeadUnSeatDetail)(Me)
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
            Dim results As ValidationResults = Validation.Validate(Of BeadUnSeatDetail)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch excValidate As Exception
            EventLogger.Enter(excValidate)
            Throw excValidate
        End Try
        Return blnValid

    End Function

#End Region

End Class
