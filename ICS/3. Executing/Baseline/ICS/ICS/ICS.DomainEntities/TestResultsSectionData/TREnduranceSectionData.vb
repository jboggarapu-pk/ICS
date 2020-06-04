''' <summary>
''' Test Results Endurance Section Data - to be used in populating of UI controls
''' </summary>
''' <remarks>NOTE: Member names must match control IDs in a UI form</remarks>
Public Class TREnduranceSectionData

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.

    Public EnduranceMatlNum As String = String.Empty

    Public EnduranceInflationPressureBefore As String = String.Empty
    Public EnduranceTemperatureBefore As String = String.Empty
    Public EnduranceSpeed As String = String.Empty
    Public EnduranceDate0 As String = String.Empty
    Public EnduranceDate1 As String = String.Empty
    Public EnduranceDate2 As String = String.Empty
    Public EnduranceDate3 As String = String.Empty
    Public EnduranceDate4 As String = String.Empty
    Public EnduranceHours1 As String = String.Empty
    Public EnduranceHours2 As String = String.Empty
    Public EnduranceHours3 As String = String.Empty
    Public EnduranceHours4 As String = String.Empty
    Public EnduranceTotalKm1 As String = String.Empty
    Public EnduranceTotalKm2 As String = String.Empty
    Public EnduranceTotalKm3 As String = String.Empty
    Public EnduranceTotalKm4 As String = String.Empty
    Public EnduranceClockTime0 As String = String.Empty
    Public EnduranceClockTime1 As String = String.Empty
    Public EnduranceClockTime2 As String = String.Empty
    Public EnduranceClockTime3 As String = String.Empty
    Public EnduranceClockTime4 As String = String.Empty
    Public EnduranceAirPressure1 As String = String.Empty
    Public EnduranceAirPressure2 As String = String.Empty
    Public EnduranceAirPressure3 As String = String.Empty
    Public EnduranceAirPressure4 As String = String.Empty
    Public EnduranceRoomTemperature1 As String = String.Empty
    Public EnduranceRoomTemperature2 As String = String.Empty
    Public EnduranceRoomTemperature3 As String = String.Empty
    Public EnduranceRoomTemperature4 As String = String.Empty
    Public EnduranceLoadKG1 As String = String.Empty
    Public EnduranceLoadKG2 As String = String.Empty
    Public EnduranceLoadKG3 As String = String.Empty
    Public EnduranceLoadKG4 As String = String.Empty
    Public EnduranceLoadPercentage1 As String = String.Empty
    Public EnduranceLoadPercentage2 As String = String.Empty
    Public EnduranceLoadPercentage3 As String = String.Empty
    Public EnduranceLoadPercentage4 As String = String.Empty
    Public EnduranceSpeed1 As String = String.Empty
    Public EnduranceSpeed2 As String = String.Empty
    Public EnduranceSpeed3 As String = String.Empty
    Public EnduranceSpeed4 As String = String.Empty
    Public EnduranceFinalTotalKM As String = String.Empty
    Public EnduranceTestPassYN As Boolean = False
    Public EnduranceInflationPressureAfter As String = String.Empty
    Public EnduranceTemperatureAfter As String = String.Empty
    Public EnduranceXHours As String = String.Empty
    Public EnduranceTestResultYN As Boolean = False
    Public PossibleFailuresFound As String = String.Empty
    Public EnduranceLowPressureStartInflation As String = String.Empty
    Public EnduranceLowPressureEndInflation As String = String.Empty
    Public EnduranceLowPressureEndTemp As String = String.Empty

    Public OriginalEndurance As Endurance = Nothing

    Public GTSpecEnduranceMatlNum As String = String.Empty

End Class
