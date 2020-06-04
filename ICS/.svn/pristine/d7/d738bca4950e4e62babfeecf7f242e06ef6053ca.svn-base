Imports CooperTire.ICS.DomainEntities

''' <summary>
''' GSO interface to the GSO Certification User control view
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
Public Interface IGSOCertificationView
    Inherits IView
    Inherits ICertificationView

    ''' <summary>
    ''' Represents property of Batch Number.
    ''' </summary>
    ''' <remarks></remarks>
    Property BatchNumber() As String

    ''' <summary>
    ''' Represents property of Date Assigned.
    ''' </summary>
    ''' <remarks></remarks>
    Property DateAssigned() As String

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
    ''' Represents property of Renewal Required.
    ''' </summary>
    ''' <remarks></remarks>
    Property RenewalRequired() As Boolean

    ''' <summary>
    ''' Represents property of Product Data.
    ''' </summary>
    ''' <remarks></remarks>
    Property ProductData() As String

    ''' <summary>
    ''' Represents property of Active Status.
    ''' </summary>
    ''' <remarks></remarks>
    Property ActiveStatus() As Boolean

    ''' <summary>
    ''' Represents property of Disc Date.
    ''' </summary>
    ''' <remarks></remarks>
    Property DiscDate() As String

    ''' <summary>
    ''' Used to scan bar-code data.
    ''' </summary>
    ''' <param name="p_strCertifName">Certificate Name.</param>
    ''' <param name="p_strTempBatchNum">Temporary Batch Number.</param>
    ''' <param name="p_strGSOBatchNum">GSO Batch Number.</param>
    ''' <remarks></remarks>
    Event BatchNumMassUpdate(ByVal p_strCertifName As String, ByVal p_strTempBatchNum As String, ByVal p_strGSOBatchNum As String)

    ''' <summary>
    ''' Represents property of Add Info.
    ''' </summary>
    ''' <remarks></remarks>
    Property AddInfo() As String                'Jeseitz 10/29/2016 req 203625

End Interface
