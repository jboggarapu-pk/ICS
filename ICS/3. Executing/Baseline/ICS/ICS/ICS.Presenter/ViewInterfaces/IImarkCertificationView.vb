Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Imark interface to the Imark Certification User control view
''' </summary>
Public Interface IImarkCertificationView
    Inherits IView
    Inherits ICertificationView

    Property DateAssigned() As String
    Property CertDateSubmitted() As String
    Property CertDateApproved() As String
    Property DateSubmitted() As String
    Property DateApproved() As String
    Property DateExpiry() As String
    Property RenewalRequired() As Boolean
    Property ActiveStatus() As Boolean
    Property EmarkReference() As String
    Property ImarkFamily() As String
    Property ImarkStamping() As String
    Property ProductData() As String
    Property ToBeRenewedCertificate() As Certificate
    Property RemoveMatlNum() As Boolean

    Property DiscDate() As String

    Property MoldChgRequired() As Boolean       'JBH_2.00 Project 5325 - Added Mold Changed Flag
    Property OperDateApproved() As String       'JBH_2.00 Project 5325 - Added Operation Approval Date
    Property AddInfo() As String                'Jeseitz 10/29/2016 req 203625

    Event Renew As CustomEvents.PlainEventHandler
    Event MoldStampingRefresh As CustomEvents.PlainEventHandler

End Interface
