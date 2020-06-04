Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators

Imports CooperTire.ICS.Common


''' <summary>
''' Class contains  WetGripDetail properties for all certification types.
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
''' <para>10/29/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>   
Public Class WetGripDetail

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
    ''' variable to hold Direction of run.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strDirectionOfRun As String = String.Empty

    ''' <summary>
    ''' variable to hold SRTT.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSRTT As String = String.Empty

    ''' <summary>
    ''' variable to hold Candidate Tire.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCandidateTire As String = String.Empty

    ''' <summary>
    ''' variable to hold Peak Break Force Coeficient.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strPeakBreakForceCoeficient As String = String.Empty

    ''' <summary>
    ''' variable to hold Mean Fully Developed Deceleration.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMeanFullyDevelopedDeceleration As String = String.Empty

    ''' <summary>
    ''' variable to hold Wet Grip Index.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strWetGripIndex As String = String.Empty

    ''' <summary>
    ''' variable to hold Comments.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strComments As String = String.Empty

    ''' <summary>
    ''' variable to hold WetGrip Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_srtWetGripID As Short = 0

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
    ''' <para>10/29/2019</para>
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
    ''' <value>Integer</value>
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
    ''' <para>10/29/2019</para>
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
    ''' <para>10/29/2019</para>
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
    ''' Gets or sets Direction of Run  value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Direction of Run.</returns>
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
    ''' <para>10/29/2019</para>
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
    ''' Gets or sets SRTT value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>SRTT.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SRTT() As String
        Get
            Return m_strSRTT
        End Get
        Set(ByVal value As String)
            m_strSRTT = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Candidate Tire value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Candidate Tire.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CandidateTire() As String
        Get
            Return m_strCandidateTire
        End Get
        Set(ByVal value As String)
            m_strCandidateTire = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Peak Break Force Coeficient value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Peak Break Force Coeficient.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property PeakBreakForceCoeficient() As String
        Get
            Return m_strPeakBreakForceCoeficient
        End Get
        Set(ByVal value As String)
            m_strPeakBreakForceCoeficient = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Mean Fully Developed Deceleration value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Mean Fully Developed Deceleration.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MeanFullyDevelopedDeceleration() As String
        Get
            Return m_strMeanFullyDevelopedDeceleration
        End Get
        Set(ByVal value As String)
            m_strMeanFullyDevelopedDeceleration = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Wet Grip Index value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Wet Grip Index.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property WetGripIndex() As String
        Get
            Return m_strWetGripIndex
        End Get
        Set(ByVal value As String)
            m_strWetGripIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Comments value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Comments.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Comments() As String
        Get
            Return m_strComments
        End Get
        Set(ByVal value As String)
            m_strComments = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Wet Grip Id value.
    ''' </summary>
    ''' <value>Short</value>
    ''' <returns>Wet Grip Id.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
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
