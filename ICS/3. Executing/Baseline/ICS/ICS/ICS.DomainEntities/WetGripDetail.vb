Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' WetGripDetail properties for all certification types
''' </summary>
''' <remarks></remarks>
Public Class WetGripDetail

#Region "Members"

    Private m_intIteration As Integer = 0
    Private m_strTestSpeed As String = String.Empty
    Private m_strDirectionOfRun As String = String.Empty
    Private m_strSRTT As String = String.Empty
    Private m_strCandidateTire As String = String.Empty
    Private m_strPeakBreakForceCoeficient As String = String.Empty
    Private m_strMeanFullyDevelopedDeceleration As String = String.Empty
    Private m_strWetGripIndex As String = String.Empty
    Private m_strComments As String = String.Empty
    Private m_srtWetGripID As Short = 0

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

    Public Property SRTT() As String
        Get
            Return m_strSRTT
        End Get
        Set(ByVal value As String)
            m_strSRTT = value
        End Set
    End Property

    Public Property CandidateTire() As String
        Get
            Return m_strCandidateTire
        End Get
        Set(ByVal value As String)
            m_strCandidateTire = value
        End Set
    End Property

    Public Property PeakBreakForceCoeficient() As String
        Get
            Return m_strPeakBreakForceCoeficient
        End Get
        Set(ByVal value As String)
            m_strPeakBreakForceCoeficient = value
        End Set
    End Property

    Public Property MeanFullyDevelopedDeceleration() As String
        Get
            Return m_strMeanFullyDevelopedDeceleration
        End Get
        Set(ByVal value As String)
            m_strMeanFullyDevelopedDeceleration = value
        End Set
    End Property

    Public Property WetGripIndex() As String
        Get
            Return m_strWetGripIndex
        End Get
        Set(ByVal value As String)
            m_strWetGripIndex = value
        End Set
    End Property

    Public Property Comments() As String
        Get
            Return m_strComments
        End Get
        Set(ByVal value As String)
            m_strComments = value
        End Set
    End Property

    Public Property WetGripID() As Short
        Get
            Return m_srtWetGripID
        End Get
        Set(ByVal value As Short)
            m_srtWetGripID = value
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
            Dim results As ValidationResults = Validation.Validate(Of WetGripDetail)(Me)
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
            Dim results As ValidationResults = Validation.Validate(Of WetGripDetail)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region

End Class
