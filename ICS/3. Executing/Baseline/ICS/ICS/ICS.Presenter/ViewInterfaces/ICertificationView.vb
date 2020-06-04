Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Certification interface to a Certification User control view
''' </summary>
Public Interface ICertificationView
    Inherits IView

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

    ''' <summary>
    ''' View context as for certificate data status
    ''' </summary>
    ''' <remarks></remarks>
    Enum Context
        NewCertificate
        JustAddedCertificate
        GotTestResults
        ExistingCertificate
        ExistingCertificateNoResults
    End Enum

    Property InfoText() As String
    Property ErrorText() As String


    'ReadOnly Property CertificationType() As Integer 'NameAid.Certification
    ReadOnly Property CertificationType() As Integer
    Property CertificateNumberID() As Integer
    Property CertificationNumber() As String
    Property ExtensionNo() As String

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Property MaterialNumber() As String

    Property SKUID() As Integer
    Property ReasonDS() As DataSet
    Property AuditLogList() As List(Of AuditLogEntry)
    Property SimilarCertificateDS() As DataTable
    Property OriginalCertificate() As Certificate
    Property SimilarTireCertificate() As Certificate
    Property SimilarTireMessage() As String
    Property ManufacturingLocationId() As String
    ReadOnly Property TRView() As ITestResultsView

    Event Save As CustomEvents.PlainEventHandler
    Event SaveReasons As CustomEvents.PlainEventHandler
    Event ShowTestResults As CustomEvents.PlainEventHandler
    Event GetTestResults As CustomEvents.PlainEventHandler
    Event GetBlankResults As CustomEvents.PlainEventHandler
    Event CopySimilarTireSKUCertificate As CustomEvents.PlainEventHandler
    Event ShowDefaultValues As CustomEvents.PlainEventHandler

    Sub ShowSimilarTirePrompt()
    Sub SetupDefaultValuesView()
    Sub SetupControlContextState(ByVal p_enuContext As ICertificationView.Context)
    Sub DoLoadView()
    Sub DisplayChangesToClient()

End Interface
