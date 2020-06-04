Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common


''' <summary>
''' Class contains SoundDetail properties for all certification types.
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
''' <term>Sujitha</term>
''' <description>
''' <para>10/28/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks> 
Public Class SoundDetail

#Region "Members"

    ''' <summary>
    ''' variable to hold Iteration.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intIteration As Integer = 0

    ''' <summary>
    ''' variable to hold Test Speed.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTestSpeed As String = String.Empty

    ''' <summary>
    ''' variable to hold .
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strDirectionOfRun As String = String.Empty

    ''' <summary>
    ''' variable to hold Sound Level Left.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSoundLevelLeft As String = String.Empty

    ''' <summary>
    ''' variable to hold Sound Level Right.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSoundLevelRight As String = String.Empty

    ''' <summary>
    ''' variable to hold Air Temp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strAirTemp As String = String.Empty

    ''' <summary>
    ''' variable to hold Track Temp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strTrackTemp As String = String.Empty

    ''' <summary>
    ''' variable to hold Sound Level Left_Temp Corrected.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSoundLevelLeft_TempCorrected As String = String.Empty

    ''' <summary>
    ''' variable to hold Sound Level Right_Temp Corrected.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSoundLevelRight_TempCorrected As String = String.Empty

    ''' <summary>
    ''' variable to hold Sound Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtSoundID As Short = 0

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
    ''' <para>10/28/2019</para>
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
    ''' Gets or sets Iteration value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Iteration.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Iteration() As Integer
        Get
            Return m_intIteration
        End Get
        Set(ByVal value As Integer)
            m_intIteration = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Test Speed value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Test Speed.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TestSpeed() As String
        Get
            Return m_strTestSpeed
        End Get
        Set(ByVal value As String)
            m_strTestSpeed = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Direction of run value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Direction of run.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property DirectionOfRun() As String
        Get
            Return m_strDirectionOfRun
        End Get
        Set(ByVal value As String)
            m_strDirectionOfRun = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Sound level left value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Sound level left.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SoundLevelLeft() As String
        Get
            Return m_strSoundLevelLeft
        End Get
        Set(ByVal value As String)
            m_strSoundLevelLeft = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Sound level right value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Sound Level Right.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SoundLevelRight() As String
        Get
            Return m_strSoundLevelRight
        End Get
        Set(ByVal value As String)
            m_strSoundLevelRight = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Air Temp value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Air Temp.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property AirTemp() As String
        Get
            Return m_strAirTemp
        End Get
        Set(ByVal value As String)
            m_strAirTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Track Temp value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Track Temp.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property TrackTemp() As String
        Get
            Return m_strTrackTemp
        End Get
        Set(ByVal value As String)
            m_strTrackTemp = value
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets Sound Level left_Temp Corrected value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Sound Level Left_Temp Corrected.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SoundLevelLeft_TempCorrected() As String
        Get
            Return m_strSoundLevelLeft_TempCorrected
        End Get
        Set(ByVal value As String)
            m_strSoundLevelLeft_TempCorrected = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Sound Level Right_Temp Corrected value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Sound Level Right_Temp Corrected.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SoundLevelRight_TempCorrected() As String
        Get
            Return m_strSoundLevelRight_TempCorrected
        End Get
        Set(ByVal value As String)
            m_strSoundLevelRight_TempCorrected = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Sound Id value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Sound Id.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SoundID() As Short
        Get
            Return m_srtSoundID
        End Get
        Set(ByVal value As Short)
            m_srtSoundID = CShort(value)
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Method  for Validate with default (anonymous) rule set
    ''' </summary>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
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
    ''' Method  for Do Validate with specific rule set.
    ''' </summary>
    ''' <param name="p_strRuleSetName">RuleSet Name</param>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
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
