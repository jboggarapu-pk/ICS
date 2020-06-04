''' <summary>
''' Test Results Product Section Data - to be used in populating of UI controls
''' </summary>
''' <remarks>NOTE: Member names must match control IDs in a UI form</remarks>
Public Class TRProductSectionData

    ' Changed ppn to tpn as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.

    Public ProductFamily As String = String.Empty
    Public InformeNumber As String = String.Empty
    Public FechaDate As String = String.Empty
    Public Trademark As String = String.Empty
    Public TreadPattern As String = String.Empty

    Public SizeDesignation As String = String.Empty
    Public SpecialProtectiveBand As String = String.Empty
    Public StructureConstruction As String = String.Empty
    Public SpeedCategory As String = String.Empty
    Public SingLoadCapacityIndex As String = String.Empty
    Public DualLoadCapacityIndex As String = String.Empty
    Public PlyRatingNumber As String = String.Empty
    Public IndicationTubeless As Boolean = False
    Public IndicationReinforced As Boolean = False
    Public IndicationExtraLoad As Boolean = False
    Public Regroovable As Boolean = False
    Public MeasuringRim As String = String.Empty
    Public DateOfManufacture As String = String.Empty
    Public TireSerialNumber As String = String.Empty
    Public DOTCode As String = String.Empty
    Public NominalTireWidth As String = String.Empty
    Public AspectRatio As String = String.Empty
    Public NominalRimDiameter As String = String.Empty
    Public TemperatureRating As String = String.Empty
    Public Traction As String = String.Empty
    Public TreadWear As String = String.Empty
    Public MS As String = String.Empty
    Public ManufacturingLocationOfOrigin As String = String.Empty
    Public TreadwearIndicators As String = String.Empty
    Public InmetroMark As String = String.Empty
    Public CargoCapacity As String = String.Empty
    Public Type As String = String.Empty
    Public NameOfManufacturer As String = String.Empty
    Public TPN As String = String.Empty

    Public DateOfTest As String = String.Empty

    Public OriginalProduct As Product = Nothing

    Public MFGWWYY As String = String.Empty
    Public TireType As DataTable = Nothing
    Public TireId As String = Nothing
    Public MudSnow As Boolean = False
    Public SevereWeatherIndicator As Boolean = False
    Public IMarkMudSnow As Boolean = False
    Public IMarkSevereWeatherInd As Boolean = False

End Class
