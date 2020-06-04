Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Interface to the Certification Defaults User control view
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
Public Interface ICertificationDefaultsView
    Inherits IView

    ''' <summary>
    '''  SaveCertificateTypeDefaults event
    ''' </summary>
    ''' <remarks></remarks>
    Event SaveCertificateTypeDefaults As CustomEvents.PlainArgumentEventHandler
    ''' <summary>
    '''  SaveCertificateDefaults event
    ''' </summary>
    ''' <remarks></remarks>
    Event SaveCertificateDefaults As CustomEvents.PlainArgumentEventHandler
    ''' <summary>
    '''  CertificateTypeSelected event
    ''' </summary>
    ''' <remarks></remarks>
    Event CertificateTypeSelected As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  ErrorText
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorText() As String
    ''' <summary>
    '''  InfoText
    ''' </summary>
    ''' <remarks></remarks>
    Property InfoText() As String
    ''' <summary>
    '''  CertificationNames
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificationNames() As List(Of String)
    ''' <summary>
    '''  CertificateFields
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateFields() As List(Of CertificationDefaultField)
    ''' <summary>
    '''  CertificateType
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateType() As String
    ''' <summary>
    '''  CertificateNo
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateNo() As String
    ''' <summary>
    '''  CertificateNoToShow
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateNoToShow() As String
    ''' <summary>
    '''  CertificateNumberID
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificateNumberID() As Integer
    ''' <summary>
    '''  GetViewInputParams
    ''' </summary>
    ''' <param name="p_strCertificateType">Certificate Type</param>
    ''' <param name="p_intCertificateNoID">Certificate No Id</param>
    ''' <remarks></remarks>
    Sub GetViewInputParams(ByRef p_strCertificateType As String, ByRef p_intCertificateNoID As Integer)
    ''' <summary>
    '''  SetupControlContextState
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetupControlContextState()

End Interface
