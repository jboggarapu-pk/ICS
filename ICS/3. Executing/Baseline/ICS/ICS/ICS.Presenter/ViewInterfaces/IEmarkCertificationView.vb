''' <summary>
''' Emark interface to the Emark Certification User control view
''' </summary>
Public Interface IEmarkCertificationView
    Inherits IView
    Inherits ICertificationView

    Property ActiveStatus() As Boolean
    Property RemoveMatlNum() As Boolean
    Property JobReportNumber() As String
    Property DateAssigned() As String
    Property DateSubmitted() As String
    Property DateApproved() As String
    Property CompanyNameSelectedValue() As String
    Property CompanyName() As DataTable
    Property ProductData() As String
    Property DiscDate() As String
    Property MoldChgRequired() As Boolean       'JBH_2.00 Project 5325 - Added Mold Changed Flag
    Property OperDateApproved() As String       'JBH_2.00 Project 5325 - Added Operation Approval Date
    Property AddInfo() As String                'Jeseitz 10/29/2016 req 203625

End Interface
