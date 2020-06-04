''' <summary>
''' Test Results Meazurement Section Data - to be used in populating of UI controls
''' </summary>
''' <remarks>NOTE: Member names must match control IDs in a UI form</remarks>
Public Class TRMeasurementSectionData

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.

    Public MeasureMatlNum As String = String.Empty
    Public TreadwearMatlNum As String = String.Empty
    Public PlungerMatlNum As String = String.Empty
    Public BeadUnseatMatlNum As String = String.Empty

    Public DOTSerialNumber As String = String.Empty
    Public StartDate As String = String.Empty
    Public EndDate As String = String.Empty
    Public StartTime As String = String.Empty
    Public EndTime As String = String.Empty
    Public TotalTime As String = String.Empty
    Public InflationPressure As String = String.Empty
    Public StartInflationPressure As String = String.Empty
    Public EndInflationPressure As String = String.Empty
    Public OverallWidth1 As String = String.Empty
    Public OverallWidth2 As String = String.Empty
    Public OverallWidth3 As String = String.Empty
    Public OverallWidth4 As String = String.Empty
    Public OverallWidth5 As String = String.Empty
    Public OverallWidth6 As String = String.Empty
    Public SectionWidth1 As String = String.Empty
    Public SectionWidth2 As String = String.Empty
    Public SectionWidth3 As String = String.Empty
    Public SectionWidth4 As String = String.Empty
    Public SectionWidth5 As String = String.Empty
    Public SectionWidth6 As String = String.Empty
    Public TreadwearHeight1 As String = String.Empty
    Public TreadwearHeight2 As String = String.Empty
    Public TreadwearHeight3 As String = String.Empty
    Public TreadwearHeight4 As String = String.Empty
    Public TreadwearHeight5 As String = String.Empty
    Public TreadwearHeight6 As String = String.Empty
    Public AverageWidth As String = String.Empty
    Public Adjustment As String = String.Empty

    Public ActualSizeFactor As String = String.Empty

    Public MinimumSizeFactor As String = String.Empty
    Public Circumference As String = String.Empty
    Public OuterDiameter As String = String.Empty
    Public NominalDiameter As String = String.Empty
    Public NominalWidth As String = String.Empty
    Public NominalWidthYN As Boolean = False
    Public NominalDifference As String = String.Empty
    Public NominalTolerance As String = String.Empty
    Public MaxOverallWidth As String = String.Empty
    Public MaxOverallDiameter As String = String.Empty
    Public MinOverallDiameter As String = String.Empty
    Public OW As String = String.Empty
    Public OWYN As Boolean = False
    Public OD As String = String.Empty
    Public ODYN As Boolean = False
    Public OverallDifference As String = String.Empty
    Public OverallTolerance As String = String.Empty
    Public DiameterXRimWidth As String = String.Empty

    Public RimRim As String = String.Empty

    Public RimPressure As String = String.Empty
    Public TreadwearIndicatorsResult As String = String.Empty
    Public TreadwearIndicatorsRequirement As String = String.Empty
    Public TreadwearIndicatorsYN As Boolean = False
    Public Plunger1 As String = String.Empty
    Public Plunger2 As String = String.Empty
    Public Plunger3 As String = String.Empty
    Public Plunger4 As String = String.Empty
    Public Plunger5 As String = String.Empty
    Public PlungerAverage As String = String.Empty
    Public PlungerAverageJ As String = String.Empty

    Public PlungerYN As Boolean = False

    Public BeadUnseatTest1 As String = String.Empty
    Public BeadUnseatTest2 As String = String.Empty
    Public BeadUnseatTest3 As String = String.Empty
    Public BeadUnseatTest4 As String = String.Empty
    Public BeadUnseatTest5 As String = String.Empty
    Public BeadUnseatTestKN As String = String.Empty
    Public BeadUnseatTestYN As Boolean = False
    Public TensileStrength1 As String = String.Empty
    Public TensileStrength2 As String = String.Empty
    Public Elongation1 As String = String.Empty
    Public Elongation2 As String = String.Empty
    Public TensileStrengthafterAging1 As String = String.Empty
    Public TensileStrengthafterAging2 As String = String.Empty
    Public TemperatureResistanceGrading As String = String.Empty

    Public OriginalMeasure As Measure = Nothing
    Public OriginalTreadwear As Treadwear = Nothing
    Public OriginalPlunger As Plunger = Nothing
    Public OriginalBeadUnSeat As BeadUnSeat = Nothing


    Public GTSpecMeasureMatlNum As String = String.Empty
    Public GTSpecTreadwearMatlNum As String = String.Empty
    Public GTSpecPlungerMatlNum As String = String.Empty
    Public GTSpecBeadUnseatMatlNum As String = String.Empty






End Class
