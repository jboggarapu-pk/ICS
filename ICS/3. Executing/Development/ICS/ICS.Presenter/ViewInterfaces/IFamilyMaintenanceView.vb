Imports CooperTire.ICS.DomainEntities

''' <summary>
''' IFamilyMaintenance interface to the FamilyMaintenance User control view
''' </summary>
''' <remarks>
''' <list type="table">
''' <listheader>
''' <term>Author</term>
''' <description>Description</description>
''' </listheader>
''' <item>
''' <term>N/A</term>
''' <description>
''' <para>N/A</para>
''' <para>Original Code.</para>
''' </description>
''' </item>
''' <item>
''' <term>Srinivas S</term>
''' <description>
''' <para>11/12/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Interface IFamilyMaintenanceView
    Inherits IView

    ''' <summary>
    ''' Represents property of Success Text.
    ''' </summary>
    ''' <remarks></remarks>
    Property SuccessText() As String

    ''' <summary>
    ''' Represents property of Error Text.
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorText() As String

    ''' <summary>
    ''' Represents property of Certification Name.
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificationName() As String

    ''' <summary>
    ''' Represents property of Add Title.
    ''' </summary>
    ''' <remarks></remarks>
    Property AddTitle() As String

    ''' <summary>
    ''' Represents property of Families data table.
    ''' </summary>
    ''' <remarks></remarks>
    Property Families() As DataTable

    ''' <summary>
    ''' Represents property of Family Id.
    ''' </summary>
    ''' <remarks></remarks>
    Property FamilyId() As Integer

    ''' <summary>
    ''' Represents property of Family Code.
    ''' </summary>
    ''' <remarks></remarks>
    Property FamilyCode() As String

    ''' <summary>
    ''' Represents property of Family Description.
    ''' </summary>
    ''' <remarks></remarks>
    Property FamilyDesc() As String

    ''' <summary>
    ''' Represents property of Application Category.
    ''' </summary>
    ''' <remarks></remarks>
    Property ApplicationCat() As String

    ''' <summary>
    ''' Represents property of Construction Type.
    ''' </summary>
    ''' <remarks></remarks>
    Property ConstructionType() As String

    ''' <summary>
    ''' Represents property of Structure Type.
    ''' </summary>
    ''' <remarks></remarks>
    Property StructureType() As String

    ''' <summary>
    ''' Represents property of Mounting Type.
    ''' </summary>
    ''' <remarks></remarks>
    Property MountingType() As String

    ''' <summary>
    ''' Represents property of Aspect Ratio Category.
    ''' </summary>
    ''' <remarks></remarks>
    Property AspectRatioCat() As String

    ''' <summary>
    ''' Represents property of Speed Rating Category.
    ''' </summary>
    ''' <remarks></remarks>
    Property SpeedRatingCat() As String

    ''' <summary>
    ''' Represents property of Load Index Category.
    ''' </summary>
    ''' <remarks></remarks>
    Property LoadIndexCat() As String

    ''' <summary>
    ''' Represents property of Quality User.
    ''' </summary>
    ''' <remarks></remarks>
    Property QualityUser() As Boolean

    ''' <summary>
    ''' Represents property of Imark Certificates Data table.
    ''' </summary>
    ''' <remarks></remarks>
    Property ImarkCertificates() As DataTable

    ''' <summary>
    ''' Represents property of Imark Certificates Selected Status.
    ''' </summary>
    ''' <remarks></remarks>
    Property ImarkCertificateSelected() As Long

    ''' <summary>
    ''' Represents method SaveFamily to save family data.
    ''' </summary>
    ''' <remarks></remarks>
    Event SaveFamily As CustomEvents.PlainEventHandler

    ''' <summary>
    ''' Represents method LoadViewData to load view data.
    ''' </summary>
    ''' <remarks></remarks>
    Event LoadViewData As CustomEvents.PlainEventHandler

    ''' <summary>
    ''' Represents method ShowFamiles to display families data.
    ''' </summary>
    ''' <remarks></remarks>
    Event ShowFamiles As CustomEvents.PlainEventHandler

    ''' <summary>
    ''' Represents method DeleteFamily to delete family.
    ''' </summary>
    ''' <remarks></remarks>
    Event DeleteFamily As CustomEvents.PlainArgumentEventHandler

    ''' <summary>
    ''' Represents method AddFamily to add family.
    ''' </summary>
    ''' <remarks></remarks>
    Event AddFamily As CustomEvents.PlainEventHandler

    ''' <summary>
    ''' Represents method EditFamily to edit family.
    ''' </summary>
    ''' <remarks></remarks>
    Event EditFamily As CustomEvents.PlainArgumentEventHandler

    ''' <summary>
    ''' Represents method ChangeCertificate to change certificate.
    ''' </summary>
    ''' <remarks></remarks>
    Event ChangeCertificate As CustomEvents.PlainArgumentEventHandler

    ''' <summary>
    ''' Used to set the view data.
    ''' </summary>
    ''' <param name="p_strCertificationName">Certification Name.</param>
    ''' <param name="p_blnAnew">Certification status.</param>
    ''' <remarks></remarks>
    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)

End Interface