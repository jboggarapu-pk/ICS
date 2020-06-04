Imports CooperTire.ICS.DomainEntities

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Interface ICertificationDefaultsView
    Inherits IView

    Event SaveCertificateTypeDefaults As CustomEvents.PlainArgumentEventHandler
    Event SaveCertificateDefaults As CustomEvents.PlainArgumentEventHandler
    Event CertificateTypeSelected As CustomEvents.PlainEventHandler

    Property ErrorText() As String
    Property InfoText() As String
    Property CertificationNames() As List(Of String)
    Property CertificateFields() As List(Of CertificationDefaultField)
    Property CertificateType() As String
    Property CertificateNo() As String
    Property CertificateNoToShow() As String
    Property CertificateNumberID() As Integer

    Sub GetViewInputParams(ByRef p_strCertificateType As String, ByRef p_intCertificateNoID As Integer)
    Sub SetupControlContextState()

End Interface
