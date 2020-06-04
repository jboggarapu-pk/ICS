Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Approval interface to the Approval form view
''' </summary>
Public Interface IApprovalView
    Inherits IView

    Property InfoText() As String
    Property ErrorText() As String

    Property AuditLogEntries() As List(Of AuditLogEntry)

    Event ApproveSelected As CustomEvents.PlainArgumentEventHandler
    Event DenySelected As CustomEvents.PlainArgumentEventHandler

End Interface
