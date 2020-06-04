''' <summary>
''' India interface to the Indian Mark Certification User control view
''' yfye changed 11/21/2011
''' </summary>
Public Interface IIndiaMarkCertificationView
    Inherits IView
    Inherits ICertificationView

    Property RenewalRequired() As Boolean
    Property ActiveStatus() As Boolean
    Property RemoveMatlNum() As Boolean
    Property CertDateSubmitted() As String
    Property CertDateApproved() As String
    Property DateSubmitted() As String
    Property DateApproved() As String
    Property IndiaMark_JobReportNumber() As String
    Property IndiaMark_ProductLocation() As String
    Property ProductData() As String
    Property DiscDate() As String
    Property DateOfExpiry() As String
    Property MoldChgRequired() As Boolean       'JBH_2.00 Project 5325 - Added Mold Changed Flag
    Property OperDateApproved() As String       'JBH_2.00 Project 5325 - Added Operation Approval Date
    Property AddInfo() As String                'Jeseitz 10/29/2016 req 203625

End Interface
