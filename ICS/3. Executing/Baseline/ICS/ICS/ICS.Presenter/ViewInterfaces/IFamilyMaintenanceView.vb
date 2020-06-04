Imports CooperTire.ICS.DomainEntities

''' <summary>
''' IFamilyMaintenance interface to the FamilyMaintenance User control view
''' </summary>
Public Interface IFamilyMaintenanceView
    Inherits IView

    Property SuccessText() As String
    Property ErrorText() As String
    Property CertificationName() As String
    Property AddTitle() As String
    Property Families() As DataTable
    Property FamilyId() As Integer
    Property FamilyCode() As String
    Property FamilyDesc() As String
    Property ApplicationCat() As String
    Property ConstructionType() As String
    Property StructureType() As String
    Property MountingType() As String
    Property AspectRatioCat() As String
    Property SpeedRatingCat() As String
    Property LoadIndexCat() As String
    Property QualityUser() As Boolean
    Property ImarkCertificates() As DataTable
    Property ImarkCertificateSelected() As Long

    Event SaveFamily As CustomEvents.PlainEventHandler
    Event LoadViewData As CustomEvents.PlainEventHandler
    Event ShowFamiles As CustomEvents.PlainEventHandler
    Event DeleteFamily As CustomEvents.PlainArgumentEventHandler
    Event AddFamily As CustomEvents.PlainEventHandler
    Event EditFamily As CustomEvents.PlainArgumentEventHandler
    Event ChangeCertificate As CustomEvents.PlainArgumentEventHandler

    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)

End Interface