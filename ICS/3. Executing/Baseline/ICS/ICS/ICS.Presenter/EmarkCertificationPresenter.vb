Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Emark Certification view pesenter
''' </summary>
''' <remarks></remarks>
Public Class EmarkCertificationPresenter
    Inherits CertificationPresenterBase

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"

    Private m_view As IEmarkCertificationView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IEmarkCertificationView)

        MyBase.New(p_view)

        m_view = p_view

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Map view data To Certificate entity
    ''' </summary>
    ''' <returns>null in case of failure</returns>
    ''' <remarks></remarks>
    Protected Overrides Function MapViewToCertificate() As Certificate

        Dim objCertificate As Certificate = Nothing

        Try
            objCertificate = New Certificate()

            objCertificate.MaterialNumber = m_view.MaterialNumber
            objCertificate.CertificationTypeName = "ECE3054"
            objCertificate.SKUID = m_view.SKUID
            objCertificate.RemoveMatlNum = m_view.RemoveMatlNum

            'objCertificate.SupplementalRequired_EI = m_view.SupplementalRequired

            objCertificate.CertificateNumber = m_view.CertificationNumber.Trim()
            objCertificate.CertificateNumberID = m_view.CertificateNumberID

            'objCertificate.SupplementalNumber_EI = m_view.SupplementalNumber
            objCertificate.Extension_EN = m_view.ExtensionNo
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

            'If Not String.IsNullOrEmpty(m_view.SupplementalDateAssigned) Then
            '    objCertificate.SupplementalDateAssigned_E = CType(m_view.SupplementalDateAssigned, DateTime)
            'Else
            '    objCertificate.SupplementalDateAssigned_E = Nothing
            'End If
            'If Not String.IsNullOrEmpty(m_view.SupplementalDateSubmitted) Then
            '    objCertificate.SupplementalDateSubmitted_E = CType(m_view.SupplementalDateSubmitted, DateTime)
            'Else
            '    objCertificate.SupplementalDateSubmitted_E = Nothing
            'End If
            'If Not String.IsNullOrEmpty(m_view.SupplementalDateApproved) Then
            '    objCertificate.SupplementalDateApproved_E = CType(m_view.SupplementalDateApproved, DateTime)
            'Else
            '    objCertificate.SupplementalDateApproved_E = Nothing
            'End If

            objCertificate.ActiveStatus = m_view.ActiveStatus
            'objCertificate.SupplementalMoldStamping_E = m_view.SupplementalMoldStamping

            'Manufacturing Location to retrieve test results from.


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
    ''' Map Certificate to view properties
    ''' </summary>
    ''' <param name="p_objCertificate"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub MapCertificateToView(ByVal p_objCertificate As Certificate)

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
        m_view.ExtensionNo = p_objCertificate.Extension_EN
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
        m_view.ManufacturingLocationId = 0
       

        If Not p_objCertificate.CompanyName = String.Empty Then
            m_view.CompanyNameSelectedValue = p_objCertificate.CompanyName
        Else
            m_view.CompanyNameSelectedValue = "COOPER"
        End If

        'ProductData
        m_view.ProductData = String.Concat(p_objCertificate.lblSizeStamp, Chr(9), p_objCertificate.lblSingLoadIndex, "/", p_objCertificate.lblDualLoadIndex, p_objCertificate.lblSpeedRating, Chr(9), p_objCertificate.lblBrandDesc, Chr(9), IIf(p_objCertificate.lblTubelessYN.ToUpper.Equals("Y"), "Tubeless", "Tube"), Chr(9), p_objCertificate.TPN)

        'Added as per project 2706 technical specification
        'Discontinued Date
        If p_objCertificate.DiscontinuedDate.Equals(DateTime.MinValue) Then
            m_view.DiscDate = String.Empty
        Else
            m_view.DiscDate = p_objCertificate.DiscontinuedDate.ToShortDateString()
        End If

        'm_view.SupplementalMoldStamping = p_objCertificate.SupplementalMoldStamping_E

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


    End Sub

    ''' <summary>
    ''' Display changes to client
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DisplayChanges()

        m_view.DisplayChangesToClient()

    End Sub


#End Region

End Class
