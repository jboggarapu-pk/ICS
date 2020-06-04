
''' <summary>
''' Class contains Test Results Meazurement Section Data - to be used in populating of UI controls.
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
''' <para>11/04/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks> 
'''NOTE: Member names must match control IDs in a UI form
Public Class TRMeasurementSectionData

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.
    ''' <summary>
    ''' variable to hold Measure Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public MeasureMatlNum As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Material Number .
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearMatlNum As String = String.Empty

    ''' <summary>
    ''' variable to hold Plunger Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public PlungerMatlNum As String = String.Empty

    ''' <summary>
    ''' variable to hold Bead Unseat Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnseatMatlNum As String = String.Empty

    ''' <summary>
    ''' variable to hold DOT Serial Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public DOTSerialNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Start Date.
    ''' </summary>
    ''' <remarks></remarks>
    Public StartDate As String = String.Empty

    ''' <summary>
    ''' variable to hold End Date.
    ''' </summary>
    ''' <remarks></remarks>
    Public EndDate As String = String.Empty

    ''' <summary>
    ''' variable to hold StartTime.
    ''' </summary>
    ''' <remarks></remarks>
    Public StartTime As String = String.Empty

    ''' <summary>
    ''' variable to hold EndTime.
    ''' </summary>
    ''' <remarks></remarks>
    Public EndTime As String = String.Empty

    ''' <summary>
    ''' variable to hold Total Time.
    ''' </summary>
    ''' <remarks></remarks>
    Public TotalTime As String = String.Empty

    ''' <summary>
    ''' variable to hold Inflation Pressure.
    ''' </summary>
    ''' <remarks></remarks>
    Public InflationPressure As String = String.Empty

    ''' <summary>
    ''' variable to hold Start Inflation Pressure.
    ''' </summary>
    ''' <remarks></remarks>
    Public StartInflationPressure As String = String.Empty

    ''' <summary>
    ''' variable to hold End Inflation Pressure.
    ''' </summary>
    ''' <remarks></remarks>
    Public EndInflationPressure As String = String.Empty

    ''' <summary>
    ''' variable to hold Overall Width1.
    ''' </summary>
    ''' <remarks></remarks>
    Public OverallWidth1 As String = String.Empty

    ''' <summary>
    ''' variable to hold Overall Width2.
    ''' </summary>
    ''' <remarks></remarks>
    Public OverallWidth2 As String = String.Empty

    ''' <summary>
    ''' variable to hold Overall Width3.
    ''' </summary>
    ''' <remarks></remarks>
    Public OverallWidth3 As String = String.Empty

    ''' <summary>
    ''' variable to hold Overall Width4.
    ''' </summary>
    ''' <remarks></remarks>
    Public OverallWidth4 As String = String.Empty

    ''' <summary>
    ''' variable to hold Overall Width5.
    ''' </summary>
    ''' <remarks></remarks>
    Public OverallWidth5 As String = String.Empty

    ''' <summary>
    ''' variable to hold Overall Width6.
    ''' </summary>
    ''' <remarks></remarks>
    Public OverallWidth6 As String = String.Empty

    ''' <summary>
    ''' variable to hold Section Width1.
    ''' </summary>
    ''' <remarks></remarks>
    Public SectionWidth1 As String = String.Empty

    ''' <summary>
    ''' variable to hold Section Width2.
    ''' </summary>
    ''' <remarks></remarks>
    Public SectionWidth2 As String = String.Empty

    ''' <summary>
    ''' variable to hold Section Width3.
    ''' </summary>
    ''' <remarks></remarks>
    Public SectionWidth3 As String = String.Empty

    ''' <summary>
    ''' variable to hold Section Width4.
    ''' </summary>
    ''' <remarks></remarks>
    Public SectionWidth4 As String = String.Empty

    ''' <summary>
    ''' variable to hold Section Width5.
    ''' </summary>
    ''' <remarks></remarks>
    Public SectionWidth5 As String = String.Empty

    ''' <summary>
    ''' variable to hold Section Width6.
    ''' </summary>
    ''' <remarks></remarks>
    Public SectionWidth6 As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Height1.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearHeight1 As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Height2.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearHeight2 As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Height3.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearHeight3 As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Height4.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearHeight4 As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Height5.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearHeight5 As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Height6.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearHeight6 As String = String.Empty

    ''' <summary>
    ''' variable to hold Average Width.
    ''' </summary>
    ''' <remarks></remarks>
    Public AverageWidth As String = String.Empty

    ''' <summary>
    ''' variable to hold Adjustment.
    ''' </summary>
    ''' <remarks></remarks>
    Public Adjustment As String = String.Empty

    ''' <summary>
    ''' variable to hold Actual Size Factor.
    ''' </summary>
    ''' <remarks></remarks>
    Public ActualSizeFactor As String = String.Empty

    ''' <summary>
    ''' variable to hold Minimum Size Factor.
    ''' </summary>
    ''' <remarks></remarks>
    Public MinimumSizeFactor As String = String.Empty

    ''' <summary>
    ''' variable to hold Circumference.
    ''' </summary>
    ''' <remarks></remarks>
    Public Circumference As String = String.Empty

    ''' <summary>
    ''' variable to hold Outer Diameter .
    ''' </summary>
    ''' <remarks></remarks>
    Public OuterDiameter As String = String.Empty

    ''' <summary>
    ''' variable to hold Nominal Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Public NominalDiameter As String = String.Empty

    ''' <summary>
    ''' variable to hold Nominal Width.
    ''' </summary>
    ''' <remarks></remarks>
    Public NominalWidth As String = String.Empty

    ''' <summary>
    ''' variable to hold Nominal WidthYN.
    ''' </summary>
    ''' <remarks></remarks>
    Public NominalWidthYN As Boolean = False

    ''' <summary>
    ''' variable to hold Nominal Difference.
    ''' </summary>
    ''' <remarks></remarks>
    Public NominalDifference As String = String.Empty

    ''' <summary>
    ''' variable to hold Nominal Tolerance.
    ''' </summary>
    ''' <remarks></remarks>
    Public NominalTolerance As String = String.Empty

    ''' <summary>
    ''' variable to hold Max Overall Width.
    ''' </summary>
    ''' <remarks></remarks>
    Public MaxOverallWidth As String = String.Empty

    ''' <summary>
    ''' variable to hold Max Overall Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Public MaxOverallDiameter As String = String.Empty

    ''' <summary>
    ''' variable to hold Min Overall Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Public MinOverallDiameter As String = String.Empty

    ''' <summary>
    ''' variable to hold OW.
    ''' </summary>
    ''' <remarks></remarks>
    Public OW As String = String.Empty

    ''' <summary>
    ''' variable to hold OWYN.
    ''' </summary>
    ''' <remarks></remarks>
    Public OWYN As Boolean = False

    ''' <summary>
    ''' variable to hold OD.
    ''' </summary>
    ''' <remarks></remarks>
    Public OD As String = String.Empty

    ''' <summary>
    ''' variable to hold ODYN.
    ''' </summary>
    ''' <remarks></remarks>
    Public ODYN As Boolean = False

    ''' <summary>
    ''' variable to hold OverallDifference.
    ''' </summary>
    ''' <remarks></remarks>
    Public OverallDifference As String = String.Empty

    ''' <summary>
    ''' variable to hold OverallTolerance.
    ''' </summary>
    ''' <remarks></remarks>
    Public OverallTolerance As String = String.Empty

    ''' <summary>
    ''' variable to hold DiameterXRimWidth.
    ''' </summary>
    ''' <remarks></remarks>
    Public DiameterXRimWidth As String = String.Empty

    ''' <summary>
    ''' variable to hold RimRim.
    ''' </summary>
    ''' <remarks></remarks>
    Public RimRim As String = String.Empty

    ''' <summary>
    ''' variable to hold RimPressure.
    ''' </summary>
    ''' <remarks></remarks>
    Public RimPressure As String = String.Empty

    ''' <summary>
    ''' variable to hold TreadwearIndicatorsResult.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearIndicatorsResult As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Indicators Requirement.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearIndicatorsRequirement As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear IndicatorsYN.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearIndicatorsYN As Boolean = False

    ''' <summary>
    ''' variable to hold Plunger1.
    ''' </summary>
    ''' <remarks></remarks>
    Public Plunger1 As String = String.Empty

    ''' <summary>
    ''' variable to hold Plunger2.
    ''' </summary>
    ''' <remarks></remarks>
    Public Plunger2 As String = String.Empty

    ''' <summary>
    ''' variable to hold Plunger3.
    ''' </summary>
    ''' <remarks></remarks>
    Public Plunger3 As String = String.Empty

    ''' <summary>
    ''' variable to hold Plunger4.
    ''' </summary>
    ''' <remarks></remarks>
    Public Plunger4 As String = String.Empty

    ''' <summary>
    ''' variable to hold Plunger5.
    ''' </summary>
    ''' <remarks></remarks>
    Public Plunger5 As String = String.Empty

    ''' <summary>
    ''' variable to hold PlungerAverage.
    ''' </summary>
    ''' <remarks></remarks>
    Public PlungerAverage As String = String.Empty

    ''' <summary>
    ''' variable to hold PlungerAverageJ.
    ''' </summary>
    ''' <remarks></remarks>
    Public PlungerAverageJ As String = String.Empty

    ''' <summary>
    ''' variable to hold PlungerYN.
    ''' </summary>
    ''' <remarks></remarks>
    Public PlungerYN As Boolean = False

    ''' <summary>
    ''' variable to hold Bead Unseat Test1.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnseatTest1 As String = String.Empty

    ''' <summary>
    ''' variable to hold Bead Unseat Test2.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnseatTest2 As String = String.Empty

    ''' <summary>
    ''' variable to hold Bead Unseat Test3.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnseatTest3 As String = String.Empty

    ''' <summary>
    ''' variable to hold Bead Unseat Test4.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnseatTest4 As String = String.Empty

    ''' <summary>
    ''' variable to hold Bead Unseat Test5.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnseatTest5 As String = String.Empty

    ''' <summary>
    ''' variable to hold Bead Unseat TestKN.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnseatTestKN As String = String.Empty

    ''' <summary>
    ''' variable to hold Bead Unseat TestYN.
    ''' </summary>
    ''' <remarks></remarks>
    Public BeadUnseatTestYN As Boolean = False

    ''' <summary>
    ''' variable to hold Tensile Strength1.
    ''' </summary>
    ''' <remarks></remarks>
    Public TensileStrength1 As String = String.Empty

    ''' <summary>
    ''' variable to hold Tensile Strength2.
    ''' </summary>
    ''' <remarks></remarks>
    Public TensileStrength2 As String = String.Empty

    ''' <summary>
    ''' variable to hold Elongation1.
    ''' </summary>
    ''' <remarks></remarks>
    Public Elongation1 As String = String.Empty

    ''' <summary>
    ''' variable to hold Elongation2.
    ''' </summary>
    ''' <remarks></remarks>
    Public Elongation2 As String = String.Empty

    ''' <summary>
    ''' variable to hold Tensile Strength afterAging1.
    ''' </summary>
    ''' <remarks></remarks>
    Public TensileStrengthafterAging1 As String = String.Empty

    ''' <summary>
    ''' variable to hold Tensile Strength afterAging2.
    ''' </summary>
    ''' <remarks></remarks>
    Public TensileStrengthafterAging2 As String = String.Empty

    ''' <summary>
    ''' variable to hold Temperature Resistance Grading.
    ''' </summary>
    ''' <remarks></remarks>
    Public TemperatureResistanceGrading As String = String.Empty

    ''' <summary>
    ''' variable to hold Original Measure.
    ''' </summary>
    ''' <remarks></remarks>
    Public OriginalMeasure As Measure = Nothing

    ''' <summary>
    ''' variable to hold Original Treadwear.
    ''' </summary>
    ''' <remarks></remarks>
    Public OriginalTreadwear As Treadwear = Nothing

    ''' <summary>
    ''' variable to hold Original Plunger.
    ''' </summary>
    ''' <remarks></remarks>
    Public OriginalPlunger As Plunger = Nothing

    ''' <summary>
    ''' variable to hold Original BeadUnSeat.
    ''' </summary>
    ''' <remarks></remarks>
    Public OriginalBeadUnSeat As BeadUnSeat = Nothing

    ''' <summary>
    ''' variable to hold GTSpec Measure Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public GTSpecMeasureMatlNum As String = String.Empty

    ''' <summary>
    ''' variable to hold GTSpec Treadwear Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public GTSpecTreadwearMatlNum As String = String.Empty

    ''' <summary>
    ''' variable to hold GTSpec Plunger Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public GTSpecPlungerMatlNum As String = String.Empty

    ''' <summary>
    ''' variable to hold GTSpec BeadUnseat Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public GTSpecBeadUnseatMatlNum As String = String.Empty

End Class
