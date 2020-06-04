Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

Public Class HighSpeedDetail
#Region "Members"

    Private m_srtHS_ID As Short = 0
    Private m_srtTestStep As Short = 0
    Private m_intIteration As Short = 0
    Private m_srtTimeInMin As Short = 0
    Private m_sngSpeed As Single = 0
    Private m_sngTotMiles As Single = 0
    Private m_sngLoad As Single = 0
    Private m_sngLoadPercent As Single = 0
    Private m_srtSetInflation As Single = 0
    Private m_srtAmbTemp As Short = 0
    Private m_srtInfPressure As Single = 0
    Private m_steStepCompletionDate As DateTime = DateTime.MinValue

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
    Public Property TestStep() As Short
        Get
            Return m_srtTestStep
        End Get
        Set(ByVal value As Short)
            m_srtTestStep = value
        End Set
    End Property
    Public Property Iteration() As Short
        Get
            Return m_intIteration
        End Get
        Set(ByVal value As Short)
            m_intIteration = value
        End Set
    End Property
    Public Property TimeInMin() As Short
        Get
            Return m_srtTimeInMin
        End Get
        Set(ByVal value As Short)
            m_srtTimeInMin = value
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
    Public Property TotMiles() As Single
        Get
            Return m_sngTotMiles
        End Get
        Set(ByVal value As Single)
            m_sngTotMiles = value
        End Set
    End Property
    Public Property Load() As Single
        Get
            Return m_sngLoad
        End Get
        Set(ByVal value As Single)
            m_sngLoad = value
        End Set
    End Property
    Public Property LoadPercent() As Single
        Get
            Return m_sngLoadPercent
        End Get
        Set(ByVal value As Single)
            m_sngLoadPercent = value
        End Set
    End Property
    Public Property SetInflation() As Single
        Get
            Return m_srtSetInflation
        End Get
        Set(ByVal value As Single)
            m_srtSetInflation = value
        End Set
    End Property
    Public Property AmbTemp() As Short
        Get
            Return m_srtAmbTemp
        End Get
        Set(ByVal value As Short)
            m_srtAmbTemp = value
        End Set
    End Property
    Public Property InfPressure() As Single
        Get
            Return m_srtInfPressure
        End Get
        Set(ByVal value As Single)
            m_srtInfPressure = value
        End Set
    End Property
    Public Property StepCompletionDate() As DateTime
        Get
            Return m_steStepCompletionDate
        End Get
        Set(ByVal value As DateTime)
            m_steStepCompletionDate = value
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
            Dim results As ValidationResults = Validation.Validate(Of HighSpeedDetail)(Me)
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
            Dim results As ValidationResults = Validation.Validate(Of HighSpeedDetail)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region

End Class
