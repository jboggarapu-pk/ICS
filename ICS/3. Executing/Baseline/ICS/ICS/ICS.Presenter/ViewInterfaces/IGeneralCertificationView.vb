''' <summary>
''' General Certificate interface to the General Certification User control view
''' This can be used for multiple certificates
''' jeseitz 6/8/2016
''' </summary>
Public Interface IGeneralCertificationView
    Inherits IView
    Inherits ICertificationView

    'Property CertificationType() As Integer
    Property RenewalRequired() As Boolean
    Property ActiveStatus() As Boolean
    Property RemoveMatlNum() As Boolean
    Property CertDateSubmitted() As String
    Property CertDateApproved() As String
    Property DateSubmitted() As String
    Property DateApproved() As String
    Property General_JobReportNumber() As String
    Property General_ProductLocation() As String
    Property ProductData() As String
    Property DiscDate() As String
    Property DateOfExpiry() As String
    Property MoldChgRequired() As Boolean       'JBH_2.00 Project 5325 - Added Mold Changed Flag
    Property OperDateApproved() As String       'JBH_2.00 Project 5325 - Added Operation Approval Date
    Property AddInfo() As String                'Jeseitz 10/29/2016 req 203625
    ' Property CertTypeName() As String
    ' Property CertTypeID() As Integer
End Interface
