''' <summary>
''' Interface to the delete duplicate certificates user control view
''' </summary>
Public Interface IDupCorrectCertificationView
    Inherits IView

    Property MaterialNumber() As String
    Property SpeedRating() As String
    Property DuplicateCertificates() As DataTable
    Property CertId() As Integer
    Property SuccessText() As String
    Property ErrorText() As String
    Property CertificationName() As String
    Property AddCertificationTitle() As String

    Event View As CustomEvents.PlainEventHandler
    Event Delete As CustomEvents.PlainEventHandler
    Event ReloadViewData As CustomEvents.PlainEventHandler

    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)

End Interface
