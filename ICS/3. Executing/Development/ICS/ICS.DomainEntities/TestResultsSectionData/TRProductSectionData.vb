''' <summary>
''' Class contains Test Results Product Section Data to be used in populating of UI controls.
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
''' <para>11/05/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks> 
''' NOTE: Member names must match control IDs in a UI form
Public Class TRProductSectionData

    ' Changed ppn to tpn as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.
    ''' <summary>
    ''' variable to hold Product Family.
    ''' </summary>
    ''' <remarks></remarks>
    Public ProductFamily As String = String.Empty

    ''' <summary>
    ''' variable to hold InformeNumber.
    ''' </summary>
    ''' <remarks></remarks>
    Public InformeNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold FechaDate.
    ''' </summary>
    ''' <remarks></remarks>
    Public FechaDate As String = String.Empty

    ''' <summary>
    ''' variable to hold Trade mark.
    ''' </summary>
    ''' <remarks></remarks>
    Public Trademark As String = String.Empty

    ''' <summary>
    ''' variable to hold Tread Pattern.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadPattern As String = String.Empty

    ''' <summary>
    ''' variable to hold Size Designation.
    ''' </summary>
    ''' <remarks></remarks>
    Public SizeDesignation As String = String.Empty

    ''' <summary>
    ''' variable to hold Special Protective Band.
    ''' </summary>
    ''' <remarks></remarks>
    Public SpecialProtectiveBand As String = String.Empty

    ''' <summary>
    ''' variable to hold Structure Construction.
    ''' </summary>
    ''' <remarks></remarks>
    Public StructureConstruction As String = String.Empty

    ''' <summary>
    ''' variable to hold Speed Category.
    ''' </summary>
    ''' <remarks></remarks>
    Public SpeedCategory As String = String.Empty

    ''' <summary>
    ''' variable to hold Sing Load Capacity Index.
    ''' </summary>
    ''' <remarks></remarks>
    Public SingLoadCapacityIndex As String = String.Empty

    ''' <summary>
    ''' variable to hold Dual Load Capacity Index.
    ''' </summary>
    ''' <remarks></remarks>
    Public DualLoadCapacityIndex As String = String.Empty

    ''' <summary>
    ''' variable to hold PlyRating Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public PlyRatingNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold Indication Tubeless.
    ''' </summary>
    ''' <remarks></remarks>
    Public IndicationTubeless As Boolean = False

    ''' <summary>
    ''' variable to hold Indication Reinforced.
    ''' </summary>
    ''' <remarks></remarks>
    Public IndicationReinforced As Boolean = False

    ''' <summary>
    ''' variable to hold Indication ExtraLoad.
    ''' </summary>
    ''' <remarks></remarks>
    Public IndicationExtraLoad As Boolean = False

    ''' <summary>
    ''' variable to hold Regroovable.
    ''' </summary>
    ''' <remarks></remarks>
    Public Regroovable As Boolean = False

    ''' <summary>
    ''' variable to hold Measuring Rim.
    ''' </summary>
    ''' <remarks></remarks>
    Public MeasuringRim As String = String.Empty

    ''' <summary>
    ''' variable to hold Date Of Manufacture.
    ''' </summary>
    ''' <remarks></remarks>
    Public DateOfManufacture As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Serial Number.
    ''' </summary>
    ''' <remarks></remarks>
    Public TireSerialNumber As String = String.Empty

    ''' <summary>
    ''' variable to hold DOTCode.
    ''' </summary>
    ''' <remarks></remarks>
    Public DOTCode As String = String.Empty

    ''' <summary>
    ''' variable to hold Nominal TireWidth.
    ''' </summary>
    ''' <remarks></remarks>
    Public NominalTireWidth As String = String.Empty

    ''' <summary>
    ''' variable to hold Aspect Ratio.
    ''' </summary>
    ''' <remarks></remarks>
    Public AspectRatio As String = String.Empty

    ''' <summary>
    ''' variable to hold Nominal Rim Diameter.
    ''' </summary>
    ''' <remarks></remarks>
    Public NominalRimDiameter As String = String.Empty

    ''' <summary>
    ''' variable to hold Temperature Rating.
    ''' </summary>
    ''' <remarks></remarks>
    Public TemperatureRating As String = String.Empty

    ''' <summary>
    ''' variable to hold Traction.
    ''' </summary>
    ''' <remarks></remarks>
    Public Traction As String = String.Empty

    ''' <summary>
    ''' variable to hold TreadWear.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadWear As String = String.Empty

    ''' <summary>
    ''' variable to hold MS.
    ''' </summary>
    ''' <remarks></remarks>
    Public MS As String = String.Empty

    ''' <summary>
    ''' variable to hold Manufacturing Location Of Origin.
    ''' </summary>
    ''' <remarks></remarks>
    Public ManufacturingLocationOfOrigin As String = String.Empty

    ''' <summary>
    ''' variable to hold Treadwear Indicators.
    ''' </summary>
    ''' <remarks></remarks>
    Public TreadwearIndicators As String = String.Empty

    ''' <summary>
    ''' variable to hold Inmetro Mark.
    ''' </summary>
    ''' <remarks></remarks>
    Public InmetroMark As String = String.Empty

    ''' <summary>
    ''' variable to hold Cargo Capacity.
    ''' </summary>
    ''' <remarks></remarks>
    Public CargoCapacity As String = String.Empty

    ''' <summary>
    ''' variable to hold Type.
    ''' </summary>
    ''' <remarks></remarks>
    Public Type As String = String.Empty

    ''' <summary>
    ''' variable to hold Name Of Manufacturer.
    ''' </summary>
    ''' <remarks></remarks>
    Public NameOfManufacturer As String = String.Empty

    ''' <summary>
    ''' variable to hold TPN.
    ''' </summary>
    ''' <remarks></remarks>
    Public TPN As String = String.Empty

    ''' <summary>
    ''' variable to hold Date Of Test.
    ''' </summary>
    ''' <remarks></remarks>
    Public DateOfTest As String = String.Empty

    ''' <summary>
    ''' variable to hold Original Product.
    ''' </summary>
    ''' <remarks></remarks>
    Public OriginalProduct As Product = Nothing

    ''' <summary>
    ''' variable to hold MFGWWYY.
    ''' </summary>
    ''' <remarks></remarks>
    Public MFGWWYY As String = String.Empty

    ''' <summary>
    ''' variable to hold Tire Type.
    ''' </summary>
    ''' <remarks></remarks>
    Public TireType As DataTable = Nothing

    ''' <summary>
    ''' variable to hold TireId.
    ''' </summary>
    ''' <remarks></remarks>
    Public TireId As String = Nothing

    ''' <summary>
    ''' variable to hold MudSnow.
    ''' </summary>
    ''' <remarks></remarks>
    Public MudSnow As Boolean = False

    ''' <summary>
    ''' variable to hold Severe Weather Indicator.
    ''' </summary>
    ''' <remarks></remarks>
    Public SevereWeatherIndicator As Boolean = False

    ''' <summary>
    ''' variable to hold IMark MudSnow.
    ''' </summary>
    ''' <remarks></remarks>
    Public IMarkMudSnow As Boolean = False

    ''' <summary>
    ''' variable to hold IMark Severe Weather Ind.
    ''' </summary>
    ''' <remarks></remarks>
    Public IMarkSevereWeatherInd As Boolean = False

End Class
