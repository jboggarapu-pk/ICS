Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' GSO Certification view pesenter
''' </summary>
''' <remarks></remarks>
Public Class GSOCertificationPresenter
    Inherits CertificationPresenterBase

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"

    Private m_view As IGSOCertificationView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IGSOCertificationView)

        MyBase.New(p_view)
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw New Exception("Error creating " + Me.ToString())
        End Try
        
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Attach presenter to view’s events.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SubscribeToEvents()

        AddHandler m_view.BatchNumMassUpdate, AddressOf OnBatchNumMassUpdate

    End Sub

    ''' <summary>
    ''' Map view data To Certificate entity
    ''' </summary>
    ''' <returns>null in case of failure</returns>
    ''' <remarks></remarks>
    Protected Overrides Function MapViewToCertificate() As Certificate

        Dim objCertificate As Certificate = Nothing

        Try
            objCertificate = New Certificate()

            m_view.InfoText = String.Empty
            m_view.ErrorText = String.Empty

            objCertificate.MaterialNumber = m_view.MaterialNumber
            objCertificate.SKUID = m_view.SKUID
            objCertificate.BatchNumber_G = m_view.BatchNumber
            objCertificate.CertificationTypeName = "GSO"

            objCertificate.CertificateNumber = m_view.CertificationNumber.Trim()
            objCertificate.CertificateNumberID = m_view.CertificateNumberID

            If Not String.IsNullOrEmpty(m_view.DateAssigned) Then
                objCertificate.DateAssigned_EGI = CType(m_view.DateAssigned, DateTime)
            Else
                objCertificate.DateAssigned_EGI = Date.MinValue
            End If

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

            objCertificate.RenewalRequired_CGIN = m_view.RenewalRequired
            objCertificate.ActiveStatus = m_view.ActiveStatus

            'Manufacturing Location to retrieve test results from.

            'jeseitz 10/29/2016
            objCertificate.AddInfo = m_view.AddInfo


            objCertificate.OriginalCertificate = m_view.OriginalCertificate
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

        m_view.BatchNumber = p_objCertificate.BatchNumber_G

        m_view.CertificationNumber = p_objCertificate.CertificateNumber
        m_view.CertificateNumberID = p_objCertificate.CertificateNumberID

        'Get the data for the manufacturing locations drop-down list
        Dim certModel As CertificateModel = New CertificateModel()
        Dim dstLocs As DataSet = certModel.GetManufacturingLocationsList

        m_view.DataBindView()

        If p_objCertificate.DateAssigned_EGI.Equals(DateTime.MinValue) Then
            m_view.DateAssigned = String.Empty
        Else
            m_view.DateAssigned = p_objCertificate.DateAssigned_EGI.ToShortDateString()
        End If

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
            m_view.CertDateApproved = p_objCertificate.CertDateApproved_CEGI.ToShortDateString()
        End If

        If p_objCertificate.DateApproved_CEGI.Equals(DateTime.MinValue) Then
            m_view.DateApproved = String.Empty
        Else
            m_view.DateApproved = p_objCertificate.DateApproved_CEGI.ToShortDateString()
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

        'Set selection for Manufacturing Location drop-down
        m_view.ManufacturingLocationId = 0
      

        'ProductData
        m_view.ProductData = String.Concat(p_objCertificate.lblSizeStamp, Chr(9), p_objCertificate.lblSingLoadIndex, "/", p_objCertificate.lblDualLoadIndex, p_objCertificate.lblSpeedRating, Chr(9), p_objCertificate.lblBrandDesc, Chr(9), IIf(p_objCertificate.lblTubelessYN.ToUpper.Equals("Y"), "Tubeless", "Tube"), Chr(9), p_objCertificate.TPN)

        'Added as per project 2706 technical specification
        'Discontinued Date
        If p_objCertificate.DiscontinuedDate.Equals(DateTime.MinValue) Then
            m_view.DiscDate = String.Empty
        Else
            m_view.DiscDate = p_objCertificate.DiscontinuedDate.ToShortDateString()
        End If


        'jeseitz 10/29/2016 Req 203625
        m_view.AddInfo = p_objCertificate.AddInfo


    End Sub

    ''' <summary>
    ''' Mass Update to replace temporary batch number with the one from GSO.
    ''' </summary>    
    Protected Sub OnBatchNumMassUpdate(ByVal p_strCertifName As String, ByVal p_strTempBatchNum As String, ByVal p_strGSOBatchNum As String)

        m_enumSaveResult = NameAid.SaveResult.SaveError

        m_view.InfoText = String.Empty
        m_view.ErrorText = String.Empty

        Dim strInfoText As String = String.Empty
        Dim strErrorText As String = String.Empty

        Try
            Dim certModel As New CertificateModel()

            m_enumSaveResult = certModel.BatchNumMassUpdate(p_strCertifName, p_strTempBatchNum, p_strGSOBatchNum)

            Select Case m_enumSaveResult
                Case NameAid.SaveResult.Sucess
                    strInfoText = "Batch numbers updated. Certificate saved"

                    ' Refresh certificate and test results to see the saved data:
                    Try
                        LoadCertificateData()
                    Catch exc As Exception
                        EventLogger.Enter(exc)
                        strErrorText &= "Error loading certificate data."
                    End Try

                Case NameAid.SaveResult.SaveError
                    strErrorText = "Error mass updating batch numbers."
            End Select

            m_view.InfoText = strInfoText
            m_view.ErrorText = strErrorText
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error mass updating batch numbers."
            m_view.SetupControlContextState(ICertificationView.Context.GotTestResults)
        End Try

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
