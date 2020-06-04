Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common

''' <summary>
''' Class contains Measurement properties for all certification types.
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
''' <para>10/17/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class EnduranceDetail
#Region "Members"
    ''' <summary>
    ''' variable to hold Endurance Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtEND_ID As Short = 0

    ''' <summary>
    ''' variable to hold Test Step.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtTestStep As Short = 0

    ''' <summary>
    ''' variable to hold Iteration.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intIteration As Integer = 0

    ''' <summary>
    ''' variable to hold time In Min.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtTimeInMin As Short = 0

    ''' <summary>
    ''' variable to hold Speed.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngSpeed As Single = 0

    ''' <summary>
    ''' variable to hold To Miles.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngTotMiles As Single = 0

    ''' <summary>
    ''' variable to hold Load.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngLoad As Single = 0

    ''' <summary>
    ''' variable to hold  load Percent.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_sngLoadPercent As Single = 0

    ''' <summary>
    ''' variable to hold Set Inflation.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtSetInflation As Single = 0

    ''' <summary>
    ''' variable to hold AmbTemp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtAmbTemp As Short = 0

    ''' <summary>
    ''' variable to hold InfPressure.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtInfPressure As Single = 0

    ''' <summary>
    ''' variable to hold Step CompletionDate.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_steStepCompletionDate As DateTime = DateTime.MinValue

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

#Region "Properties"

    ''' <summary>
    ''' Gets or sets Endurance ID value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Endurance ID.</returns>
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
    Public Property END_ID() As Short
        Get
            Return m_srtEND_ID
        End Get
        Set(ByVal value As Short)
            m_srtEND_ID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets test Step value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Test Step.</returns>
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
    Public Property TestStep() As Short
        Get
            Return m_srtTestStep
        End Get
        Set(ByVal value As Short)
            m_srtTestStep = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Iteration value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Iteration Value.</returns>
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
    Public Property Iteration() As Integer
        Get
            Return m_intIteration
        End Get
        Set(ByVal value As Integer)
            m_intIteration = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Time In Min value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Time In Min.</returns>
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
    Public Property TimeInMin() As Short
        Get
            Return m_srtTimeInMin
        End Get
        Set(ByVal value As Short)
            m_srtTimeInMin = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Speed value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Speed.</returns>
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
    Public Property Speed() As Single
        Get
            Return m_sngSpeed
        End Get
        Set(ByVal value As Single)
            m_sngSpeed = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets TotMiles value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>TotMiles.</returns>
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
    Public Property TotMiles() As Single
        Get
            Return m_sngTotMiles
        End Get
        Set(ByVal value As Single)
            m_sngTotMiles = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Load value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Load.</returns>
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
    Public Property Load() As Single
        Get
            Return m_sngLoad
        End Get
        Set(ByVal value As Single)
            m_sngLoad = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Load Percent value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>LoadPercent.</returns>
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
    Public Property LoadPercent() As Single
        Get
            Return m_sngLoadPercent
        End Get
        Set(ByVal value As Single)
            m_sngLoadPercent = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Set Inflation value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Set Inflation.</returns>
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
    Public Property SetInflation() As Single
        Get
            Return m_srtSetInflation
        End Get
        Set(ByVal value As Single)
            m_srtSetInflation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Amb Temp value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Amb Temp.</returns>
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
    Public Property AmbTemp() As Short
        Get
            Return m_srtAmbTemp
        End Get
        Set(ByVal value As Short)
            m_srtAmbTemp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Inf Pressure value.
    ''' </summary>
    ''' <value>Single</value>
    ''' <returns>Inf Pressure.</returns>
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
    Public Property InfPressure() As Single
        Get
            Return m_srtInfPressure
        End Get
        Set(ByVal value As Single)
            m_srtInfPressure = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Step Completion Date value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>Step Completion Date.</returns>
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
    ''' Do Validate with default (anonymous) rule set
    ''' </summary>
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
    Public Function DoValidate() As Boolean

        Dim blnValid As Boolean = False
        Try
            Dim results As ValidationResults = Validation.Validate(Of EnduranceDetail)(Me)
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
            Dim results As ValidationResults = Validation.Validate(Of EnduranceDetail)(Me, p_strRuleSetName)
            blnValid = results.IsValid
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try
        Return blnValid

    End Function

#End Region
End Class
