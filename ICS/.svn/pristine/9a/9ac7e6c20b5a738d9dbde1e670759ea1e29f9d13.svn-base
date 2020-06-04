Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Interface to Imark Certification User control view.
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
''' <term>Sujitha</term>
''' <description>
''' <para>11/12/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Interface IImarkCertificationView
    Inherits IView
    Inherits ICertificationView

    ''' <summary>
    ''' Variable to hold Date Assigned.
    ''' </summary>
    ''' <remarks></remarks>
    Property DateAssigned() As String

    ''' <summary>
    ''' Variable to hold Cerificate Date Submitted.
    ''' </summary>
    ''' <remarks></remarks>
    Property CertDateSubmitted() As String

    ''' <summary>
    ''' Variable to hold Certificate Date Approved.
    ''' </summary>
    ''' <remarks></remarks>
    Property CertDateApproved() As String

    ''' <summary>
    ''' Variable to hold Date Submitted.
    ''' </summary>
    ''' <remarks></remarks>
    Property DateSubmitted() As String

    ''' <summary>
    ''' Variable to hold Date Approved.
    ''' </summary>
    ''' <remarks></remarks>
    Property DateApproved() As String

    ''' <summary>
    ''' Variable to hold Date Expiry.
    ''' </summary>
    ''' <remarks></remarks>
    Property DateExpiry() As String

    ''' <summary>
    ''' Variable to hold Renewal Required.
    ''' </summary>
    ''' <remarks></remarks>
    Property RenewalRequired() As Boolean

    ''' <summary>
    ''' Variable to hold Active Status.
    ''' </summary>
    ''' <remarks></remarks>
    Property ActiveStatus() As Boolean

    ''' <summary>
    ''' Variable to hold Emark Reference.
    ''' </summary>
    ''' <remarks></remarks>
    Property EmarkReference() As String

    ''' <summary>
    ''' Variable to hold Imark Family.
    ''' </summary>
    ''' <remarks></remarks>
    Property ImarkFamily() As String

    ''' <summary>
    ''' Variable to hold Imark Stamping.
    ''' </summary>
    ''' <remarks></remarks>
    Property ImarkStamping() As String

    ''' <summary>
    ''' Variable to hold Product Data.
    ''' </summary>
    ''' <remarks></remarks>
    Property ProductData() As String

    ''' <summary>
    ''' Variable to hold To Be Renewed Certificate.
    ''' </summary>
    ''' <remarks></remarks>
    Property ToBeRenewedCertificate() As Certificate

    ''' <summary>
    ''' Variable to hold Remove Material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Property RemoveMatlNum() As Boolean

    ''' <summary>
    ''' Variable to hold DiscDate.
    ''' </summary>
    ''' <remarks></remarks>
    Property DiscDate() As String

    'JBH_2.00 Project 5325 - Added Mold Changed Flag
    ''' <summary>
    ''' Variable to hold Mold Change Required.
    ''' </summary>
    ''' <remarks></remarks>
    Property MoldChgRequired() As Boolean

    'JBH_2.00 Project 5325 - Added Operation Approval Date
    ''' <summary>
    ''' Variable to hold Operation Date Approved.
    ''' </summary>
    ''' <remarks></remarks>
    Property OperDateApproved() As String

    'Jeseitz 10/29/2016 req 203625
    ''' <summary>
    ''' Variable to hold Add Info.
    ''' </summary>
    ''' <remarks></remarks>
    Property AddInfo() As String

    ''' <summary>
    ''' Event to Renew.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event Renew As CustomEvents.PlainEventHandler

    ''' <summary>
    ''' Event to Mold Stamping Refresh.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event MoldStampingRefresh As CustomEvents.PlainEventHandler

End Interface
