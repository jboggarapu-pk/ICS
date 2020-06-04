Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Emark Certification view presenter
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
''' <para>10/17/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class Emark117CertificationPresenter
    Inherits CertificationPresenterBase

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"
    ''' <summary>
    ''' Interface to the Emark Certification User control view 
    ''' </summary>
    Private m_view As IEmark117CertificationView

#End Region

#Region "Constructors"
    ''' <summary>
    ''' Custom Constructor to initialize class members.
    ''' </summary>
    ''' <param name="p_view">View</param>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As IEmark117CertificationView)

        MyBase.New(p_view)

        m_view = p_view

    End Sub

#End Region

#Region "Methods"

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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Overrides Function MapViewToCertificate() As Certificate

        Dim objCertificate As Certificate = Nothing
        Const ECE117Text As String = "ECE117"
        Try
            objCertificate = New Certificate()

            objCertificate.MaterialNumber = m_view.MaterialNumber
            objCertificate.CertificationTypeName = ECE117Text
            objCertificate.SKUID = m_view.SKUID
            objCertificate.RemoveMatlNum = m_view.RemoveMatlNum

            'objCertificate.SupplementalRequired_EI = m_view.SupplementalRequired

            objCertificate.CertificateNumber = m_view.CertificationNumber.Trim()
            objCertificate.CertificateNumberID = m_view.CertificateNumberID

            'objCertificate.SupplementalNumber_EI = m_view.SupplementalNumber
            objCertificate.Extension_EN = CInt(m_view.Extension)
            objCertificate.JobReportNumber_CEN = m_view.JobReportNumber

            If String.IsNullOrEmpty(m_view.DateAssigned) Then
                objCertificate.DateAssigned_EGI = Nothing
            Else
                objCertificate.DateAssigned_EGI = CType(m_view.DateAssigned, DateTime)
            End If


            If Not String.IsNullOrEmpty(m_view.DateApproved) Then
                objCertificate.DateApproved_CEGI = CType(m_view.DateApproved, DateTime)
            Else
                objCertificate.DateApproved_CEGI = Date.MinValue
            End If


            If Not String.IsNullOrEmpty(m_view.DateSubmitted) Then
                objCertificate.DateSubmitted = CType(m_view.DateSubmitted, DateTime)
            Else
                objCertificate.DateSubmitted = Nothing
            End If

            objCertificate.ActiveStatus = m_view.ActiveStatus
            objCertificate.SupplementalMoldStamping_E = m_view.SupplementalMoldStamping

            objCertificate.CompanyName = m_view.CompanyNameSelectedValue

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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Overrides Sub MapCertificateToView(ByVal p_objCertificate As Certificate)
        Const CooperText As String = "COOPER"
        Const TubelessText As String = "Tubeless"
        Const TubeText As String = "Tube"
        Const YText As String = "Y"
        Try
            'm_view.SupplementalRequired = p_objCertificate.SupplementalRequired_EI

            m_view.CertificationNumber = p_objCertificate.CertificateNumber
            m_view.CertificateNumberID = p_objCertificate.CertificateNumberID

            'Get the data for the manufacturing locations drop-down list
            Dim certModel As CertificateModel = New CertificateModel()
            Dim dstLocs As DataSet = certModel.GetManufacturingLocationsList

            'Get the data for the company name drop-down list
            Dim dstCompanyName As DataSet = certModel.GetCompanyNameList
            m_view.CompanyName = dstCompanyName.Tables(0)
            m_view.DataBindView()

            'm_view.SupplementalNumber = p_objCertificate.SupplementalNumber_EI
            m_view.Extension = CStr(p_objCertificate.Extension_EN)
            m_view.JobReportNumber = p_objCertificate.JobReportNumber_CEN

            If p_objCertificate.DateAssigned_EGI.Equals(DateTime.MinValue) Then
                m_view.DateAssigned = String.Empty
            Else
                m_view.DateAssigned = p_objCertificate.DateAssigned_EGI.ToShortDateString()
            End If


            If p_objCertificate.DateSubmitted.Equals(DateTime.MinValue) Then
                m_view.DateSubmitted = String.Empty
            Else
                m_view.DateSubmitted = p_objCertificate.DateSubmitted.ToShortDateString()
            End If


            If p_objCertificate.DateApproved_CEGI.Equals(DateTime.MinValue) Then
                m_view.DateApproved = String.Empty
            Else
                m_view.DateApproved = p_objCertificate.DateApproved_CEGI.ToShortDateString()
            End If

            'Remove Material number checkbox
            m_view.RemoveMatlNum = p_objCertificate.RemoveMatlNum

            If p_objCertificate.ActiveStatus.Equals(Nothing) Then
                m_view.ActiveStatus = False
            Else
                m_view.ActiveStatus = p_objCertificate.ActiveStatus
            End If

            'Set selection for Manufacturing Location drop-down
            m_view.ManufacturingLocationId = CStr(0)

            If Not p_objCertificate.CompanyName = String.Empty Then
                m_view.CompanyNameSelectedValue = p_objCertificate.CompanyName
            Else
                m_view.CompanyNameSelectedValue = CooperText
            End If

            m_view.SupplementalMoldStamping = p_objCertificate.SupplementalMoldStamping_E

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
    ''' <para>10/17/2019</para>
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
