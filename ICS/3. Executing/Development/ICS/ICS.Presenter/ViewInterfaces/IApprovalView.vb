Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Approval interface to the Approval form view
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
''' <term>Jhansi</term>
''' <description>
''' <para>11/11/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Interface IApprovalView
    Inherits IView

    ''' <summary>
    '''  InfoText
    ''' </summary>
    ''' <remarks></remarks>
    Property InfoText() As String
    ''' <summary>
    '''  ErrorText
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorText() As String
    ''' <summary>
    '''  AuditLogEntries
    ''' </summary>
    ''' <remarks></remarks>
    Property AuditLogEntries() As List(Of AuditLogEntry)
    ''' <summary>
    '''  ApproveSelected event
    ''' </summary>
    ''' <remarks></remarks>
    Event ApproveSelected As CustomEvents.PlainArgumentEventHandler
    ''' <summary>
    '''  DenySelected event
    ''' </summary>
    ''' <remarks></remarks>
    Event DenySelected As CustomEvents.PlainArgumentEventHandler

End Interface
