Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Certification interface to a Certification User control view
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
''' <para>11/12/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
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


    'ReadOnly Property CertificationType() As Integer 'NameAid.Certification
    ''' <summary>
    '''  CertificationType
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property CertificationType() As Integer
    ''' <summary>
    '''  CertificateNumberID
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateNumberID() As Integer
    ''' <summary>
    '''  CertificationNumber
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificationNumber() As String
    ''' <summary>
    '''  ExtensionNo
    ''' </summary>
    ''' <remarks></remarks>
    Property ExtensionNo() As String

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    '''  MaterialNumber
    ''' </summary>
    ''' <remarks></remarks>
    Property MaterialNumber() As String
    ''' <summary>
    '''  SKUID
    ''' </summary>
    ''' <remarks></remarks>
    Property SKUID() As Integer
    ''' <summary>
    '''  ReasonDS
    ''' </summary>
    ''' <remarks></remarks>
    Property ReasonDS() As DataSet
    ''' <summary>
    '''  AuditLogList
    ''' </summary>
    ''' <remarks></remarks>
    Property AuditLogList() As List(Of AuditLogEntry)
    ''' <summary>
    '''  SimilarCertificateDS
    ''' </summary>
    ''' <remarks></remarks>
    Property SimilarCertificateDS() As DataTable
    ''' <summary>
    '''  OriginalCertificate
    ''' </summary>
    ''' <remarks></remarks>
    Property OriginalCertificate() As Certificate
    ''' <summary>
    '''  SimilarTireCertificate
    ''' </summary>
    ''' <remarks></remarks>
    Property SimilarTireCertificate() As Certificate
    ''' <summary>
    '''  SimilarTireMessage
    ''' </summary>
    ''' <remarks></remarks>
    Property SimilarTireMessage() As String
    ''' <summary>
    '''  ManufacturingLocationId
    ''' </summary>
    ''' <remarks></remarks>
    Property ManufacturingLocationId() As String
    ''' <summary>
    '''  TRView
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property TRView() As ITestResultsView

    ''' <summary>
    '''  Save event
    ''' </summary>
    ''' <remarks></remarks>
    Event Save As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  SaveReasons event
    ''' </summary>
    ''' <remarks></remarks>
    Event SaveReasons As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  ShowTestResults event
    ''' </summary>
    ''' <remarks></remarks>
    Event ShowTestResults As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  GetTestResults event
    ''' </summary>
    ''' <remarks></remarks>
    Event GetTestResults As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  GetBlankResults event
    ''' </summary>
    ''' <remarks></remarks>
    Event GetBlankResults As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  CopySimilarTireSKUCertificate event
    ''' </summary>
    ''' <remarks></remarks>
    Event CopySimilarTireSKUCertificate As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  ShowDefaultValues event
    ''' </summary>
    ''' <remarks></remarks>
    Event ShowDefaultValues As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  ShowSimilarTirePrompt
    ''' </summary>
    ''' <remarks></remarks>
    Sub ShowSimilarTirePrompt()
    ''' <summary>
    '''  SetupDefaultValuesView
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetupDefaultValuesView()
    ''' <summary>
    '''  SetupControlContextState
    ''' </summary>
    ''' <param name="p_enuContext">Context</param>
    ''' <remarks></remarks>
    Sub SetupControlContextState(ByVal p_enuContext As ICertificationView.Context)
    ''' <summary>
    '''  DoLoadView
    ''' </summary>
    ''' <remarks></remarks>
    Sub DoLoadView()
    ''' <summary>
    '''  DisplayChangesToClient
    ''' </summary>
    ''' <remarks></remarks>
    Sub DisplayChangesToClient()

End Interface
