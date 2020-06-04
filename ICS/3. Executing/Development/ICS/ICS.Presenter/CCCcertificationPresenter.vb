Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' CCC Certification view presenter
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
''' <para>10/15/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class CCCcertificationPresenter
    Inherits CertificationPresenterBase

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"
    ''' <summary>
    ''' CCC interface to the CCC Certification User control view
    ''' </summary>
    Private m_view As ICCCCertificationView

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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As ICCCCertificationView)

        MyBase.New(p_view)

        m_view = p_view

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Map view data To Certificate entity
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Overrides Function MapViewToCertificate() As Certificate

        Dim objCertificate As Certificate = Nothing
        Const CCCText As String = "CCC"
        Try
            objCertificate = New Certificate()

            objCertificate.MaterialNumber = m_view.MaterialNumber
            objCertificate.CertificationTypeName = CCCText 'NameAid.Certification.CCC.ToString()
            objCertificate.SKUID = m_view.SKUID
            objCertificate.RemoveMatlNum = m_view.RemoveMatlNum

            objCertificate.CertificateNumber = m_view.CertificationNumber.Trim()
            objCertificate.CertificateNumberID = m_view.CertificateNumberID

            If Not String.IsNullOrEmpty(m_view.CertDateApproved) Then
                objCertificate.CertDateApproved_CEGI = CType(m_view.CertDateApproved, DateTime)
            Else
                objCertificate.CertDateApproved_CEGI = Date.MinValue
            End If

            If Not String.IsNullOrEmpty(m_view.DateApproved) Then
                objCertificate.DateApproved_CEGI = CType(m_view.DateApproved, DateTime)
            Else
                objCertificate.DateApproved_CEGI = Date.MinValue
            End If

            If Not String.IsNullOrEmpty(m_view.CertDateSubmitted) Then
                objCertificate.CertDateSubmitted = CType(m_view.CertDateSubmitted, DateTime)
            Else
                objCertificate.DateSubmitted = Date.MinValue
            End If

            If Not String.IsNullOrEmpty(m_view.DateSubmitted) Then
                objCertificate.DateSubmitted = CType(m_view.DateSubmitted, DateTime)
            Else
                objCertificate.DateSubmitted = Date.MinValue
            End If

            If Not String.IsNullOrEmpty(m_view.DateOfExpiry) Then
                objCertificate.ExpiryDate_I = CType(m_view.DateOfExpiry, DateTime)
            Else
                objCertificate.ExpiryDate_I = Date.MinValue
            End If

            objCertificate.ProductLocation = m_view.CCC_ProductLocation
            objCertificate.JobReportNumber_CEN = m_view.CCC_JobReportNumber
            objCertificate.ActiveStatus = m_view.ActiveStatus

            objCertificate.RenewalRequired_CGIN = m_view.RenewalRequired

            objCertificate.OriginalCertificate = m_view.OriginalCertificate

            'JBH_2.00 Project 5325 - Mold Change Required Checkbox
            objCertificate.MoldChgRequired = m_view.MoldChgRequired

            'JBH_2.00 Project 5325 - Operation Approval Date
            If Not String.IsNullOrEmpty(m_view.OperDateApproved) Then
                objCertificate.OperDateApproved = CType(m_view.OperDateApproved, DateTime)
            Else
                objCertificate.OperDateApproved = Date.MinValue
            End If

            'jeseitz 10/29/2016
            objCertificate.AddInfo = m_view.AddInfo


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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Overrides Sub MapCertificateToView(ByVal p_objCertificate As Certificate)
        Const TubelessText As String = "Tubeless"
        Const TubeText As String = "Tube"
        Const YText As String = "Y"
        Try
            m_view.CertificationNumber = p_objCertificate.CertificateNumber
            m_view.CertificateNumberID = p_objCertificate.CertificateNumberID

            m_view.CCC_JobReportNumber = p_objCertificate.JobReportNumber_CEN

            If p_objCertificate.CertDateSubmitted.Equals(DateTime.MinValue) Then
                m_view.CertDateSubmitted = String.Empty
            Else
                m_view.CertDateSubmitted = p_objCertificate.CertDateSubmitted.ToShortDateString()
            End If

            If p_objCertificate.DateSubmitted.Equals(DateTime.MinValue) Then
                m_view.DateSubmitted = String.Empty
            Else
                m_view.DateSubmitted = p_objCertificate.DateSubmitted.ToShortDateString()
            End If

            If p_objCertificate.CertDateApproved_CEGI.Equals(DateTime.MinValue) Then
                m_view.CertDateApproved = String.Empty
            Else
                m_view.CertDateApproved = p_objCertificate.CertDateApproved_CEGI.ToShortDateString
            End If

            If p_objCertificate.DateApproved_CEGI.Equals(DateTime.MinValue) Then
                m_view.DateApproved = String.Empty
            Else
                m_view.DateApproved = p_objCertificate.DateApproved_CEGI.ToShortDateString()
            End If

            If p_objCertificate.ActiveStatus.Equals(Nothing) Then
                m_view.ActiveStatus = False
            Else
                m_view.ActiveStatus = p_objCertificate.ActiveStatus
            End If

            'Remove Material number checkbox
            m_view.RemoveMatlNum = p_objCertificate.RemoveMatlNum

            If p_objCertificate.RenewalRequired_CGIN.Equals(Nothing) Then
                m_view.RenewalRequired = False
            Else
                m_view.RenewalRequired = p_objCertificate.RenewalRequired_CGIN
            End If

            m_view.CCC_ProductLocation = p_objCertificate.ProductLocation

            'ProductData
            m_view.ProductData = String.Concat(p_objCertificate.lblSizeStamp,
                                               Chr(9), p_objCertificate.lblSingLoadIndex,
                                               "/", p_objCertificate.lblDualLoadIndex,
                                               p_objCertificate.lblSpeedRating, Chr(9),
                                               p_objCertificate.lblBrandDesc, Chr(9),
                                               IIf(p_objCertificate.lblTubelessYN.ToUpper.Equals(YText), TubelessText, TubeText), Chr(9), p_objCertificate.TPN)

            'Added as per project 2706 technical specification
            'Discontinued Date
            If p_objCertificate.DiscontinuedDate.Equals(DateTime.MinValue) Then
                m_view.DiscDate = String.Empty
            Else
                m_view.DiscDate = p_objCertificate.DiscontinuedDate.ToShortDateString()
            End If

            If p_objCertificate.ExpiryDate_I.Equals(DateTime.MinValue) Then
                m_view.DateOfExpiry = String.Empty
            Else
                m_view.DateOfExpiry = p_objCertificate.ExpiryDate_I.ToShortDateString()
            End If

            'JBH_2.00 Project 5325 - Mold Change Required Checkbox
            m_view.MoldChgRequired = p_objCertificate.MoldChgRequired

            'JBH_2.00 Project 5325 - Operation Approval Date
            If p_objCertificate.OperDateApproved.Equals(DateTime.MinValue) Then
                m_view.OperDateApproved = String.Empty
            Else
                m_view.OperDateApproved = p_objCertificate.OperDateApproved.ToShortDateString()
            End If

            'jeseitz 10/29/2016 Req 203625
            m_view.AddInfo = p_objCertificate.AddInfo
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
    ''' <para>10/15/2019</para>
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
