''' <summary>
''' Class contains Project Section Data properties.
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
Public Class TRProjectSectionData

    ' Added strings as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' variable to hold Measure Project Number .
    ''' </summary>
    ''' <remarks></remarks>
    Public MeasureProjectNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Measure Tire Number .
    ''' </summary>
    ''' <remarks></remarks>
    Public MeasureTireNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Measure Test Spec .
    ''' </summary>
    ''' <remarks></remarks>
    Public MeasureTestSpec As String = String.Empty

    'Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' variable to hold Measure Operation.
    ''' </summary>
    ''' <remarks></remarks>
    Public MeasureOperation As String = String.Empty

    ''' <summary>
    ''' variable to hold Plunger Project Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public PlungerProjectNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Plunger Tire Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public PlungerTireNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Plunger Test Spec.
    ''' </summary>
    ''' <remarks></remarks>
    Public PlungerTestSpec As String = String.Empty

    'Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' variable to hold Plunger Operation .
    ''' </summary>
    ''' <remarks></remarks>
    Public PlungerOperation As String = String.Empty

    ''' <summary>
    ''' variable to hold Bead UnSeat Project Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnSeatProjectNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Bead UnSeat Tire Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnSeatTireNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Bead UnSeat Test Spec.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnSeatTestSpec As String = String.Empty

    'Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' variable to hold Bead UnSeat Operation.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnSeatOperation As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Project Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearProjectNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Tire Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearTireNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Tread wear Test Spec.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearTestSpec As String = String.Empty

    'Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' variable to hold Treadwear Operation.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearOperation As String = String.Empty

    ''' <summary>
    ''' variable to hold Endurance Project Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public EnduranceProjectNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Endurance Tire Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public EnduranceTireNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Endurance TestSpec.
    ''' </summary>
    ''' <remarks></remarks>
    Public EnduranceTestSpec As String = String.Empty

    'Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' variable to hold Endurance Operation.
    ''' </summary>
    ''' <remarks></remarks>
    Public EnduranceOperation As String = String.Empty

    ''' <summary>
    ''' variable to hold HighSpeed Project Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public HighSpeedProjectNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold HighSpeed Tire Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public HighSpeedTireNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold HighSpeed Test Spec.
    ''' </summary>
    ''' <remarks></remarks>
    Public HighSpeedTestSpec As String = String.Empty

    'Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' variable to hold HighSpeed Operation.
    ''' </summary>
    ''' <remarks></remarks>
    Public HighSpeedOperation As String = String.Empty

    ''' <summary>
    ''' variable to hold Sound Project Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public SoundProjectNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Sound Tire Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public SoundTireNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Sound TestSpec.
    ''' </summary>
    ''' <remarks></remarks>
    Public SoundTestSpec As String = String.Empty

    'Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' variable to hold Sound Operation.
    ''' </summary>
    ''' <remarks></remarks>
    Public SoundOperation As String = String.Empty

    ''' <summary>
    ''' variable to hold WetGrip Project Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public WetGripProjectNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold WetGrip Tire Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public WetGripTireNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold WetGrip Test Spec.
    ''' </summary>
    ''' <remarks></remarks>
    Public WetGripTestSpec As String = String.Empty

    'Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    ''' <summary>
    ''' variable to hold WetGrip Operation.
    ''' </summary>
    ''' <remarks></remarks>
    Public WetGripOperation As String = String.Empty

End Class
