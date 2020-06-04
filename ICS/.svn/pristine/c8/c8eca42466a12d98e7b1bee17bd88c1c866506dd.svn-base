Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

Public Class SpeedTestDetail
#Region "Members"

    Private m_srtHS_ID As Short = 0
    Private m_srtIteration As Short = 0
    Private m_sngSpeed As Single = 0
    Private m_dteTime As DateTime = DateTime.MinValue

#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

#Region "Properties"

    Public Property HS_ID() As Short
        Get
            Return m_srtHS_ID
        End Get
        Set(ByVal value As Short)
            m_srtHS_ID = value
        End Set
    End Property
    Public Property Iteration() As Short
        Get
            Return m_srtIteration
        End Get
        Set(ByVal value As Short)
            m_srtIteration = value
        End Set
    End Property
    Public Property Speed() As Single
        Get
            Return m_sngSpeed
        End Get
        Set(ByVal value As Single)
            m_sngSpeed = value
        End Set
    End Property
    Public Property Time() As DateTime
        Get
            Return m_dteTime
        End Get
        Set(ByVal value As DateTime)
            m_dteTime = value
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
            Dim results As ValidationResults = Validation.Validate(Of SpeedTestDetail)(Me)
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
            Dim results As ValidationResults = Validation.Validate(Of SpeedTestDetail)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region

End Class