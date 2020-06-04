
''' <summary>
''' General Certificate interface to the General Certification User control view
''' This can be used for multiple certificates
''' jeseitz 6/8/2016
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
''' <term>Srinivas S</term>
''' <description>
''' <para>11/12/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Interface IGeneralCertificationView
    Inherits IView
    Inherits ICertificationView

    'Property CertificationType() As Integer

    ''' <summary>
    ''' Represents property of Renewal Required.
    ''' </summary>
    ''' <remarks></remarks>
    Property RenewalRequired() As Boolean

    ''' <summary>
    ''' Represents property of Active Status.
    ''' </summary>
    ''' <remarks></remarks>
    Property ActiveStatus() As Boolean

    ''' <summary>
    ''' Represents property of Remove Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Property RemoveMatlNum() As Boolean

    ''' <summary>
    ''' Represents property of Certificate Date Submitted.
    ''' </summary>
    ''' <remarks></remarks>
    Property CertDateSubmitted() As String

    ''' <summary>
    ''' Represents property of Certificate Date Approved.
    ''' </summary>
    ''' <remarks></remarks>
    Property CertDateApproved() As String

    ''' <summary>
    ''' Represents property of Date Submitted.
    ''' </summary>
    ''' <remarks></remarks>
    Property DateSubmitted() As String

    ''' <summary>
    ''' Represents property of Date Approved.
    ''' </summary>
    ''' <remarks></remarks>
    Property DateApproved() As String

    ''' <summary>
    ''' Represents property of General Job Report Number.
    ''' </summary>
    ''' <remarks></remarks>
    Property General_JobReportNumber() As String

    ''' <summary>
    ''' Represents property of General Product Location.
    ''' </summary>
    ''' <remarks></remarks>
    Property General_ProductLocation() As String

    ''' <summary>
    ''' Represents property of Product Data.
    ''' </summary>
    ''' <remarks></remarks>
    Property ProductData() As String

    ''' <summary>
    ''' Represents property of Disc Date.
    ''' </summary>
    ''' <remarks></remarks>
    Property DiscDate() As String

    ''' <summary>
    ''' Represents property of Expiry Date.
    ''' </summary>
    ''' <remarks></remarks>
    Property DateOfExpiry() As String

    ''' <summary>
    ''' Represents property of Mold Changes Required Flag.
    ''' </summary>
    ''' <remarks></remarks>
    Property MoldChgRequired() As Boolean       'JBH_2.00 Project 5325 - Added Mold Changed Flag

    ''' <summary>
    ''' Represents property of Operation Approved Date.
    ''' </summary>
    ''' <remarks></remarks>
    Property OperDateApproved() As String       'JBH_2.00 Project 5325 - Added Operation Approval Date

    ''' <summary>
    ''' Represents property of Add Info.
    ''' </summary>
    ''' <remarks></remarks>
    Property AddInfo() As String                'Jeseitz 10/29/2016 req 203625

    ' Property CertTypeName() As String

    ' Property CertTypeID() As Integer
End Interface
