Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' NOM Certification view presenter
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
''' <para>10/21/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class NOMCertificationPresenter
    Inherits CertificationPresenterBase

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"
    ''' <summary>
    ''' Interface to the NOM certification user control view.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_view As INOMCertificationView

#End Region

#Region "Constructors"
    ''' <summary>
    ''' Custom Constructor to initialize class members.
    ''' </summary>
    ''' <param name="p_view">View</param>
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
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As INOMCertificationView)
        MyBase.New(p_view)
        m_view = p_view
        SubscribeToEvents()
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Attach presenter to view’s events.
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
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()

    End Sub

    ''' <summary>
    ''' Map view data To Certificate entity.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>null in case of failure</returns>
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
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Overrides Function MapViewToCertificate() As Certificate

        Dim objCertificate As Certificate = Nothing
        Const NOMText As String = "NOM"
        Try
            objCertificate = New Certificate()

            m_view.InfoText = String.Empty
            m_view.ErrorText = String.Empty

            objCertificate.MaterialNumber = m_view.MaterialNumber
            objCertificate.CertificationTypeName = NOMText
            objCertificate.SKUID = m_view.SKUID

            objCertificate.CertificateNumber = m_view.CertificationNumber.Trim()
            objCertificate.CertificateNumberID = m_view.CertificateNumberID

            objCertificate.Extension_EN = CInt(m_view.ExtensionNo)
            objCertificate.JobReportNumber_CEN = m_view.JobReportNumber

            If Not String.IsNullOrEmpty(m_view.CertDateSubmitted) Then
                objCertificate.CertDateSubmitted = CType(m_view.CertDateSubmitted, DateTime)
            Else
                objCertificate.CertDateSubmitted = Date.MinValue
            End If

            If Not String.IsNullOrEmpty(m_view.DateSubmitted) Then
                objCertificate.DateSubmitted = CType(m_view.DateSubmitted, DateTime)
            Else
                objCertificate.DateSubmitted = Date.MinValue
            End If

            'jeseitz 10/29/2016
            objCertificate.AddInfo = m_view.AddInfo


            objCertificate.RenewalRequired_CGIN = m_view.RenewalRequired
            objCertificate.ActiveStatus = m_view.ActiveStatus
            objCertificate.CountryOfManufacture_N = m_view.CountryOfManufacture
            objCertificate.CustomerSpecific_N = m_view.CustomerSpecific

            'Importer
            objCertificate.AddNewImporter = m_view.AddNewImporter
            objCertificate.ImporterID = CInt(m_view.ImporterId)
            objCertificate.Importer_N = m_view.Importer
            objCertificate.ImporterAddress_N = m_view.ImporterAddress
            objCertificate.ImporterRepresentative_N = m_view.ImporterRepresentative
            objCertificate.CountryLocation_N = m_view.CountryLocation

            'Customer
            objCertificate.AddNewCustomer = m_view.AddNewCustomer
            objCertificate.ActSigReq = m_view.ActSigReq
            objCertificate.CustomerID = CInt(m_view.CustomerId)
            objCertificate.Customer_N = m_view.Customer
            objCertificate.CustomerAddress_N = m_view.CustomerAddress

            objCertificate.OriginalCertificate = m_view.OriginalCertificate
        Catch exc As Exception
            objCertificate = Nothing
            EventLogger.Enter(exc)
        End Try
        Return objCertificate
    End Function

    ''' <summary>
    ''' Map Certificate to view properties.
    ''' </summary>
    ''' <param name="p_objCertificate">Certificate</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Overrides Sub MapCertificateToView(ByVal p_objCertificate As Certificate)
        Const YText As String = "Y"
        Const TubelessText As String = "Tubeless"
        Const TubeText As String = "Tube"
        Try
            m_view.CertificationNumber = p_objCertificate.CertificateNumber
            m_view.CertificateNumberID = p_objCertificate.CertificateNumberID

            m_view.ExtensionNo = CStr(p_objCertificate.Extension_EN)
            m_view.JobReportNumber = p_objCertificate.JobReportNumber_CEN


            m_view.CertDateSubmitted = String.Empty
            If Not p_objCertificate.CertDateSubmitted.Equals(DateTime.MinValue) Then
                m_view.CertDateSubmitted = p_objCertificate.CertDateSubmitted.ToShortDateString()
            End If

            m_view.DateSubmitted = String.Empty
            If Not p_objCertificate.DateSubmitted.Equals(DateTime.MinValue) Then
                m_view.DateSubmitted = p_objCertificate.DateSubmitted.ToShortDateString()
            End If

            If p_objCertificate.RenewalRequired_CGIN.Equals(Nothing) Then
                m_view.RenewalRequired = False
            Else
                m_view.RenewalRequired = p_objCertificate.RenewalRequired_CGIN
            End If

            If p_objCertificate.ActiveStatus.Equals(Nothing) Then
                m_view.ActiveStatus = False
            Else
                m_view.ActiveStatus = p_objCertificate.ActiveStatus
            End If

            m_view.CountryOfManufacture = p_objCertificate.CountryOfManufacture_N

            If p_objCertificate.CustomerSpecific_N.Equals(Nothing) Then
                m_view.CustomerSpecific = False
            Else
                m_view.CustomerSpecific = p_objCertificate.CustomerSpecific_N
            End If

            'Customer
            m_view.ActSigReq = p_objCertificate.ActSigReq
            m_view.CustomerId = p_objCertificate.CustomerID.ToString()
            m_view.Customer = p_objCertificate.Customer_N
            m_view.CustomerAddress = p_objCertificate.CustomerAddress_N

            'Importer
            m_view.ImporterId = p_objCertificate.ImporterID.ToString()
            m_view.Importer = p_objCertificate.Importer_N
            m_view.ImporterAddress = p_objCertificate.ImporterAddress_N
            m_view.ImporterRepresentative = p_objCertificate.ImporterRepresentative_N
            m_view.CountryLocation = p_objCertificate.CountryLocation_N

            'ProductData
            m_view.ProductData = String.Concat(p_objCertificate.lblSizeStamp,
                                               Chr(9),
                                               p_objCertificate.lblSingLoadIndex,
                                               "/", p_objCertificate.lblDualLoadIndex,
                                               p_objCertificate.lblSpeedRating, Chr(9),
                                               p_objCertificate.lblBrandDesc, Chr(9),
                                               IIf(p_objCertificate.lblTubelessYN.ToUpper.Equals(YText), TubelessText, TubeText),
                                               Chr(9), p_objCertificate.TPN)

            'Added as per project 2706 technical specification
            'Discontinued Date
            If p_objCertificate.DiscontinuedDate.Equals(DateTime.MinValue) Then
                m_view.DiscDate = String.Empty
            Else
                m_view.DiscDate = p_objCertificate.DiscontinuedDate.ToShortDateString()
            End If

            'jeseitz 10/29/2016 Req 203625
            m_view.AddInfo = p_objCertificate.AddInfo
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Map Certificate to view properties.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ClearCustomerSectionView()
        Try
            m_view.Customer = String.Empty
            m_view.Importer = String.Empty
            m_view.ImporterAddress = String.Empty
            m_view.ImporterRepresentative = String.Empty
            m_view.CountryLocation = String.Empty
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Map Certificate to view properties.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function MapViewToCustomer() As Customer

        Dim objCustomer As Customer = Nothing
        Try
            objCustomer = New Customer()

            objCustomer.SKUID = m_view.SKUID
            objCustomer.Customer_N = m_view.Customer
            objCustomer.CustomerAddress_N = m_view.CustomerAddress
            objCustomer.Importer_N = m_view.Importer
            objCustomer.ImporterAddress_N = m_view.ImporterAddress
            objCustomer.ImporterRepresentative_N = m_view.ImporterRepresentative
            objCustomer.CountryLocation_N = m_view.CountryLocation
        Catch exc As Exception
            objCustomer = Nothing
            EventLogger.Enter(exc)
        End Try
        Return objCustomer
    End Function

    ''' <summary>
    ''' Map Certificate to view properties.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub MapCustomerToView(ByVal p_objCustomer As Customer)
        Try
            m_view.Customer = p_objCustomer.Customer_N
            m_view.Importer = p_objCustomer.Importer_N
            m_view.ImporterAddress = p_objCustomer.ImporterAddress_N
            m_view.ImporterRepresentative = p_objCustomer.ImporterRepresentative_N
            m_view.CountryLocation = p_objCustomer.CountryLocation_N
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Display changes to client.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Overrides Sub DisplayChanges()
        Try
            m_view.DisplayChangesToClient()
        Catch
            Throw
        End Try
    End Sub
#End Region

End Class
