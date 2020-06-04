Imports CooperTire.ICS.DomainEntities
''' <summary>
''' GSO interface to the GSO Certification User control view
''' </summary>
Public Interface IGSOCertificationView
    Inherits IView
    Inherits ICertificationView

    Property BatchNumber() As String
    Property DateAssigned() As String
    Property CertDateSubmitted() As String
    Property CertDateApproved() As String
    Property DateSubmitted() As String
    Property DateApproved() As String
    Property RenewalRequired() As Boolean
    Property ProductData() As String
    Property ActiveStatus() As Boolean
    Property DiscDate() As String
    Event BatchNumMassUpdate(ByVal p_strCertifName As String, ByVal p_strTempBatchNum As String, ByVal p_strGSOBatchNum As String)
    Property AddInfo() As String                'Jeseitz 10/29/2016 req 203625

End Interface
