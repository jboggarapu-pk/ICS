Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' SoundDetail properties for all certification types
''' </summary>
''' <remarks></remarks>
Public Class SoundDetail

#Region "Members"

    Private m_intIteration As Integer = 0
    Private m_strTestSpeed As String = String.Empty
    Private m_strDirectionOfRun As String = String.Empty
    Private m_strSoundLevelLeft As String = String.Empty
    Private m_strSoundLevelRight As String = String.Empty
    Private m_strAirTemp As String = String.Empty
    Private m_strTrackTemp As String = String.Empty
    Private m_strSoundLevelLeft_TempCorrected As String = String.Empty
    Private m_strSoundLevelRight_TempCorrected As String = String.Empty
    Private m_srtSoundID As Short = 0

#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

#Region "Properties"

    Public Property Iteration() As Integer
        Get
            Return m_intIteration
        End Get
        Set(ByVal value As Integer)
            m_intIteration = value
        End Set
    End Property

    Public Property TestSpeed() As String
        Get
            Return m_strTestSpeed
        End Get
        Set(ByVal value As String)
            m_strTestSpeed = value
        End Set
    End Property

    Public Property DirectionOfRun() As String
        Get
            Return m_strDirectionOfRun
        End Get
        Set(ByVal value As String)
            m_strDirectionOfRun = value
        End Set
    End Property

    Public Property SoundLevelLeft() As String
        Get
            Return m_strSoundLevelLeft
        End Get
        Set(ByVal value As String)
            m_strSoundLevelLeft = value
        End Set
    End Property

    Public Property SoundLevelRight() As String
        Get
            Return m_strSoundLevelRight
        End Get
        Set(ByVal value As String)
            m_strSoundLevelRight = value
        End Set
    End Property

    Public Property AirTemp() As String
        Get
            Return m_strAirTemp
        End Get
        Set(ByVal value As String)
            m_strAirTemp = value
        End Set
    End Property

    Public Property TrackTemp() As String
        Get
            Return m_strTrackTemp
        End Get
        Set(ByVal value As String)
            m_strTrackTemp = value
        End Set
    End Property

    Public Property SoundLevelLeft_TempCorrected() As String
        Get
            Return m_strSoundLevelLeft_TempCorrected
        End Get
        Set(ByVal value As String)
            m_strSoundLevelLeft_TempCorrected = value
        End Set
    End Property

    Public Property SoundLevelRight_TempCorrected() As String
        Get
            Return m_strSoundLevelRight_TempCorrected
        End Get
        Set(ByVal value As String)
            m_strSoundLevelRight_TempCorrected = value
        End Set
    End Property

    Public Property SoundID()
        Get
            Return m_srtSoundID
        End Get
        Set(ByVal value)
            m_srtSoundID = value
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
            Dim results As ValidationResults = Validation.Validate(Of SoundDetail)(Me)
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
            Dim results As ValidationResults = Validation.Validate(Of SoundDetail)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region

End Class
