Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Certification Presenters common base 
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
''' <para>10/24/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public MustInherit Class CertificationPresenterBase

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members and Constants"
    ''' <summary>
    ''' Save result
    ''' </summary>
    ''' <remarks></remarks>
    Protected m_enumSaveResult As NameAid.SaveResult
    ''' <summary>
    ''' Certification interface to a Certification user control view.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_view As ICertificationView
    Private m_blnTRsExist As Boolean = False
    Private Shared m_blnFreshStart As Boolean = False
    ''' <summary>
    '''  Constant to hold Error loading test results data text
    ''' </summary>
    Const ErrorLoadTestResultsDataText As String = "Error loading test results data."
    ''' <summary>
    '''  Constant to hold Incorrect value types in Certificate text
    ''' </summary>
    Const IncorrectValueTypeInCertificateText As String = "Incorrect value types in Certificate."
    ''' <summary>
    '''  Constant to hold Certificate saved text
    ''' </summary>
    Const CertificateSavedText As String = "Certificate saved."
    ''' <summary>
    '''  Constant to hold Error loading certificate data text
    ''' </summary>
    Const ErrorLoadCertificateDataText As String = "Error loading certificate data."
    ''' <summary>
    '''  Constant to hold Save error text
    ''' </summary>
    Const SaveErrorText As String = "Save error."
    ''' <summary>
    '''  Constant to hold Value validation errors text
    ''' </summary>
    Const ValueValidationErrorsText As String = "Value validation errors."
#End Region

#Region "Properties"

    ''' <summary>
    ''' Signifies the need for fresh data on view load.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Shared Property FreshStart() As Boolean
        Get
            Return m_blnFreshStart
        End Get
        Set(ByVal value As Boolean)
            m_blnFreshStart = value
        End Set
    End Property

#End Region

#Region "Constructors / Destructors"
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As ICertificationView)
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
    ''' Attach presenter to view�s events.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()
        Try
            AddHandler m_view.LoadView, AddressOf OnLoadView
            AddHandler m_view.Save, AddressOf OnSave
            AddHandler m_view.SaveReasons, AddressOf OnSaveReasons
            AddHandler m_view.ShowTestResults, AddressOf OnShowTestResults
            AddHandler m_view.GetTestResults, AddressOf OnGetTestResults
            AddHandler m_view.GetBlankResults, AddressOf OnGetBlankResults
            AddHandler m_view.CopySimilarTireSKUCertificate, AddressOf OnCopySimilarTireSKUCertificate
            AddHandler m_view.ShowDefaultValues, AddressOf OnShowDefaultValues
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Load data for the view.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)
        Const ErrorLoadFormDataText As String = "Error loading form data."
        Try
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
            ElseIf CertificationPresenterBase.FreshStart Then
                m_view.TRView.CertificateNumber = Nothing
                m_view.TRView.CertificateNumberID = 0
                m_view.TRView.IsVisible = False
                LoadViewData()
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadFormDataText
        End Try
    End Sub

    ''' <summary>
    ''' Load data from business process.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadViewData()
        Try
            m_view.InfoText = String.Empty
            m_view.ErrorText = String.Empty
            ClearSimilarTireMode()

            LoadCertificateData()

            ' Setup appropriate state of view controls
            If m_view.OriginalCertificate Is Nothing Then
                If m_view.TRView.IsVisible Then
                    ShowTestResults()
                    m_view.SetupControlContextState(ICertificationView.Context.GotTestResults)
                Else
                    m_view.SetupControlContextState(ICertificationView.Context.NewCertificate)
                End If
            Else
                If Not m_blnTRsExist AndAlso Not m_view.TRView.IsVisible Then
                    m_view.OriginalCertificate = Nothing
                    m_view.SetupControlContextState(ICertificationView.Context.JustAddedCertificate)
                Else
                    ShowTestResults()
                    m_view.SetupControlContextState(ICertificationView.Context.ExistingCertificate)
                End If
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Load Certificate view data.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub LoadCertificateData()
        Dim intCertificatonTypeID As String
        Try
            If m_view.MaterialNumber.Equals(String.Empty) Then
                Return
            End If

            Dim certModel As New CertificateModel()

            If m_view.CertificationType <> 0 Then
                intCertificatonTypeID = CStr(m_view.CertificationType)
            Else
                intCertificatonTypeID = CStr(0)
            End If
            m_view.OriginalCertificate = certModel.GetCertificate(m_view.CertificationNumber, m_view.ExtensionNo, m_view.MaterialNumber, CInt(intCertificatonTypeID), m_view.SKUID, m_blnTRsExist)

            If m_view.OriginalCertificate Is Nothing Then
                MapCertificateToView(New Certificate())
            Else
                CertificationSearchPresenter.CurrentCertificateSKUID = CStr(m_view.OriginalCertificate.SKUID)
                MapCertificateToView(m_view.OriginalCertificate)
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Map certificate data to view controls.
    ''' </summary>
    ''' <param name="p_objCertificate">Certificate</param>
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected MustOverride Sub MapCertificateToView(ByVal p_objCertificate As Certificate)

    ''' <summary>
    ''' Map view controls to certificate data
    ''' </summary>
    ''' <returns></returns>
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected MustOverride Function MapViewToCertificate() As Certificate

    ''' <summary>
    ''' Map view controls to certificate data
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected MustOverride Sub DisplayChanges()

    ''' <summary>
    ''' Starts the save process by auditing the certificate, displaying the changed fields,
    ''' and prompting the client to specify a reason for changes.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Protected Sub OnSave()
        m_enumSaveResult = NameAid.SaveResult.SaveError

        m_view.InfoText = String.Empty
        m_view.ErrorText = String.Empty

        Dim strInfoText As String = String.Empty
        Dim strErrorText As String = String.Empty
        Dim enuCertViewContext As ICCCCertificationView.Context
        Const ErrorRetriveReasonCodesFromDatabaseText As String = "Error retrieving reason codes from database."
        Const ErrorAuditingCertificateDataForMaterialNumberText As String = "Error auditing certificate data for Material number "

        Try
            ' Maps the data to the Certificate entity
            Dim objCertificate As Certificate = MapViewToCertificate()
            Dim objCustomer As Customer = Nothing
            'If m_view.CertificationType = "NOM" Then
            If m_view.CertificationType = 3 Then ' JESEITZ 6/17/2016
                objCustomer = New NOMCertificationPresenter(CType(m_view, INOMCertificationView)).MapViewToCustomer()
            End If
            If objCertificate Is Nothing Then
                m_view.ErrorText = IncorrectValueTypeInCertificateText
                Return
            End If
            '***************************************************************************************************************

            '***************************************************************************************************************
            ' Map the test results section data to its corresponding entity
            Dim objTRPSData As TRProductSectionData = m_view.TRView.GetTRProductSectionData()
            Dim objTRMSData As TRMeasurementSectionData = m_view.TRView.GetTRMeasurementSectionData()
            Dim objTRPrjdata As TRProjectSectionData = m_view.TRView.GetTRProjectSectionData()
            Dim objTREBData As TREnduranceTestGeneralBeforeSectionData = m_view.TRView.GetTREnduranceBeforeSectionData()
            Dim objTREData As TREnduranceSectionData = m_view.TRView.GetTREnduranceSectionData()
            Dim objTREAData As TREnduranceTestGeneralAfterSectionData = m_view.TRView.GetTREnduranceAfterSectionData()
            Dim objTRHSBData As TRHighSpeedTestGeneralBeforeSectionData = m_view.TRView.GetTRHighSpeedBeforeSectionData()
            Dim objTRHSData As TRHighSpeedSectionData = m_view.TRView.GetTRHighSpeedSectionData()
            Dim objTRHSAData As TRHighSpeedTestGeneralAfterSectionData = m_view.TRView.GetTRHighSpeedAfterSectionData()
            Dim objTRSWData As TRSoundWetSectionData = m_view.TRView.GetTRSoundWetSectionData()
            '****************************************************************************************************************

            Dim certModel As New CertificateModel()
            Dim objTRModel As New TestResultsModel()
            'Dim intCertType As Integer = certModel.GetCertificateTypeID(objCertificate.CertificationTypeName)
            Dim intCertType As Integer = certModel.GetCertificateTypeID(objCertificate.CertificationTypeName)

            'This could be a problem if no product data is found for Material number
            'If there is not product section then Get Results has not happened, so save Certificate without test results
            If objTRPSData Is Nothing Then
                m_enumSaveResult = certModel.SaveCertificate(objCertificate)
                Select Case m_enumSaveResult
                    Case NameAid.SaveResult.Sucess
                        ' Assigns ID for a new certificate, no change otherwise
                        m_view.CertificateNumberID = objCertificate.CertificateNumberID

                        ClearSimilarTireMode()

                        strInfoText = CertificateSavedText

                        enuCertViewContext = ICertificationView.Context.ExistingCertificateNoResults

                        ' Refresh certificate and test results to see the saved data:
                        Try
                            LoadCertificateData()
                        Catch exc As Exception
                            EventLogger.Enter(exc)
                            strErrorText &= ErrorLoadCertificateDataText
                        End Try
                    Case NameAid.SaveResult.SaveError
                        strErrorText = SaveErrorText
                        enuCertViewContext = CType(IIf(objCertificate.OriginalCertificate Is Nothing, ICertificationView.Context.GotTestResults, ICertificationView.Context.ExistingCertificate), ICertificationView.Context)
                End Select

                m_view.InfoText = strInfoText
                m_view.ErrorText = strErrorText
                m_view.SetupControlContextState(enuCertViewContext)
                Exit Sub
            End If

            '*****************************************************************************************************************
            'Map all the object prior to validation to their corresponding entities.
            Dim objProduct As Product = objTRModel.MapTRProductSectionDataToProduct(objTRPSData, objCertificate)
            Dim objMeasure As Measure = objTRModel.MapTRMeasurementSectionDataToMeasure(objTRMSData, objTRPrjdata, objCertificate)
            Dim objPlunger As Plunger = objTRModel.MapTRMeasurementSectionDataToPlunger(objTRMSData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID)
            Dim objTreadwear As Treadwear = objTRModel.MapTRMeasurementSectionDataToTreadWear(objTRMSData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID)
            Dim objBeadUnseat As BeadUnSeat = objTRModel.MapTRMeasurementSectionDataToBeadUnSeat(objTRMSData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID)
            Dim objEndurance As Endurance = objTRModel.MapTREnduranceGeneralSectionDataToEndurance(objTREBData, objTREData, objTREAData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID, objCertificate.MaterialNumber)
            Dim objHighSpeed As HighSpeed = objTRModel.MapTRHighSpeedGeneralSectionDataToHighSpeed(objTRHSBData, objTRHSData, objTRHSAData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID, objCertificate.MaterialNumber)
            Dim objSound As Sound = objTRModel.MapTRSoundWetSectionDataToSound(objTRSWData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID, objCertificate.MaterialNumber)
            Dim objWetGrip As WetGrip = objTRModel.MapTRSoundWetSectionDataToWetGrip(objTRSWData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID, objCertificate.MaterialNumber)
            '******************************************************************************************************************

            'Validate Objects prior the saving
            m_enumSaveResult = certModel.ValidateEntireCertificate(objCertificate, objCustomer, objProduct, objMeasure, objPlunger, objTreadwear, objBeadUnseat, objEndurance, objHighSpeed, objSound, objWetGrip)
            If m_enumSaveResult = NameAid.SaveResult.Sucess Then

                ' Audit all Certificate-wise data:
                m_view.AuditLogList = certModel.AuditEntireCertificate(objCertificate, objTRModel, objProduct, objMeasure, _
                                                         objPlunger, objTreadwear, objBeadUnseat, objEndurance, _
                                                         objHighSpeed, objSound, objWetGrip)

                If m_view.AuditLogList.Count() > 0 And m_view.SimilarTireCertificate Is Nothing Then
                    m_view.ReasonDS = AuditLogModel.GetApprovalReasons(m_view.CertificationType)

                    If Not m_view.ReasonDS Is Nothing Then
                        'Display change to the client
                        DisplayChanges()
                    Else
                        strErrorText = ErrorRetriveReasonCodesFromDatabaseText
                        enuCertViewContext = CType(IIf(objCertificate.OriginalCertificate Is Nothing, ICertificationView.Context.GotTestResults, ICertificationView.Context.ExistingCertificate), ICertificationView.Context)
                    End If
                Else
                    OnSaveReasons()
                    Exit Sub
                End If
            Else
                strErrorText = ValueValidationErrorsText
                enuCertViewContext = CType(IIf(objCertificate.OriginalCertificate Is Nothing, ICertificationView.Context.GotTestResults, ICertificationView.Context.ExistingCertificate), ICertificationView.Context)
            End If

            m_view.InfoText = strInfoText
            m_view.ErrorText = strErrorText
            'm_view.SetupControlContextState(enuCertViewContext)
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorAuditingCertificateDataForMaterialNumberText + m_view.MaterialNumber + "."
            m_view.SetupControlContextState(ICertificationView.Context.GotTestResults)
        End Try

    End Sub

    ''' <summary>
    ''' Completes the save process by saving all data from Certificate and test results.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Protected Sub OnSaveReasons()

        m_enumSaveResult = NameAid.SaveResult.SaveError

        m_view.InfoText = String.Empty
        m_view.ErrorText = String.Empty

        Dim strInfoText As String = String.Empty
        Dim strErrorText As String = String.Empty
        Dim dtmSaveTime As DateTime
        Dim enuNotificationResult As AuditLogModel.NotificationResult
        Dim enuCertViewContext As ICCCCertificationView.Context
        Const ErrorSaveAuditLogText As String = "Error saving to audit log."
        Const DuplicateCertificationNumberText As String = "Duplicate certification number."
        Const NotificationSentText As String = "Notification sent."
        Const NotificationFailedText As String = "Notification failed."
        Const NotificationDisabledText As String = "Notification disabled."
        Const ErrorSaveDataForMaterialNumberText As String = "Error saving data for Material Number "
        Try
            '**************************************************************************************************************
            ' Maps the data to the Certificate entity
            Dim objCertificate As Certificate = MapViewToCertificate()
            Dim objCustomer As Customer = Nothing
            ' If m_view.CertificationType = "NOM" Then
            If m_view.CertificationType = 3 Then ' jeseitz 6/17/2016
                objCustomer = New NOMCertificationPresenter(CType(m_view, INOMCertificationView)).MapViewToCustomer()
            End If
            If objCertificate Is Nothing Then
                m_view.ErrorText = IncorrectValueTypeInCertificateText
                Return
            End If
            '***************************************************************************************************************
            dtmSaveTime = DateTime.Now
            '***************************************************************************************************************
            ' Map the test results section data to its corresponding entity
            Dim objTRPSData As TRProductSectionData = m_view.TRView.GetTRProductSectionData()
            Dim objTRMSData As TRMeasurementSectionData = m_view.TRView.GetTRMeasurementSectionData()
            Dim objTRPrjdata As TRProjectSectionData = m_view.TRView.GetTRProjectSectionData()
            Dim objTREBData As TREnduranceTestGeneralBeforeSectionData = m_view.TRView.GetTREnduranceBeforeSectionData()
            Dim objTREData As TREnduranceSectionData = m_view.TRView.GetTREnduranceSectionData()
            Dim objTREAData As TREnduranceTestGeneralAfterSectionData = m_view.TRView.GetTREnduranceAfterSectionData()
            Dim objTRHSBData As TRHighSpeedTestGeneralBeforeSectionData = m_view.TRView.GetTRHighSpeedBeforeSectionData()
            Dim objTRHSData As TRHighSpeedSectionData = m_view.TRView.GetTRHighSpeedSectionData()
            Dim objTRHSAData As TRHighSpeedTestGeneralAfterSectionData = m_view.TRView.GetTRHighSpeedAfterSectionData()
            Dim objTRSWData As TRSoundWetSectionData = m_view.TRView.GetTRSoundWetSectionData()
            '****************************************************************************************************************

            Dim certModel As New CertificateModel()
            Dim objTRModel As New TestResultsModel()
            Dim intCertType As Integer = certModel.GetCertificateTypeID(objCertificate.CertificationTypeName)

            '*****************************************************************************************************************
            'Map all the object prior to validation to their corresponding entities.
            Dim objProduct As Product = objTRModel.MapTRProductSectionDataToProduct(objTRPSData, objCertificate)
            Dim objMeasure As Measure = objTRModel.MapTRMeasurementSectionDataToMeasure(objTRMSData, objTRPrjdata, objCertificate)
            Dim objPlunger As Plunger = objTRModel.MapTRMeasurementSectionDataToPlunger(objTRMSData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID)
            Dim objTreadwear As Treadwear = objTRModel.MapTRMeasurementSectionDataToTreadWear(objTRMSData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID)
            Dim objBeadUnseat As BeadUnSeat = objTRModel.MapTRMeasurementSectionDataToBeadUnSeat(objTRMSData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID)
            Dim objEndurance As Endurance = objTRModel.MapTREnduranceGeneralSectionDataToEndurance(objTREBData, objTREData, objTREAData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID, objCertificate.MaterialNumber)
            Dim objHighSpeed As HighSpeed = objTRModel.MapTRHighSpeedGeneralSectionDataToHighSpeed(objTRHSBData, objTRHSData, objTRHSAData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID, objCertificate.MaterialNumber)
            Dim objSound As Sound = objTRModel.MapTRSoundWetSectionDataToSound(objTRSWData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID, objCertificate.MaterialNumber)
            Dim objWetGrip As WetGrip = objTRModel.MapTRSoundWetSectionDataToWetGrip(objTRSWData, objTRPrjdata, objCertificate.CertificateNumberID, intCertType, objCertificate.SKUID, objCertificate.MaterialNumber)
            '******************************************************************************************************************

            If m_view.AuditLogList.Count > 0 And m_view.SimilarTireCertificate Is Nothing Then
                If Not AuditLogModel.SaveResults(m_view.AuditLogList) Then
                    m_view.ErrorText = ErrorSaveAuditLogText
                    Return
                End If
            End If

            If m_view.SimilarTireCertificate IsNot Nothing Then
                m_enumSaveResult = certModel.SaveSKUAssociationAndCertificateData(objCertificate, m_view.MaterialNumber, objCustomer, objProduct, objMeasure, objPlunger, objTreadwear, objBeadUnseat, objEndurance, objHighSpeed, objSound, objWetGrip)
            Else
                m_enumSaveResult = certModel.SaveCertificateData(objCertificate, objCustomer, objProduct, objMeasure, objPlunger, objTreadwear, objBeadUnseat, objEndurance, objHighSpeed, objSound, objWetGrip)
            End If

            Select Case m_enumSaveResult
                Case NameAid.SaveResult.ValidationError
                    strErrorText = ValueValidationErrorsText
                    enuCertViewContext = CType(IIf(objCertificate.OriginalCertificate Is Nothing, ICertificationView.Context.GotTestResults, ICertificationView.Context.ExistingCertificate), ICertificationView.Context)
                Case NameAid.SaveResult.DuplicationError
                    strErrorText = DuplicateCertificationNumberText
                    enuCertViewContext = CType(IIf(objCertificate.OriginalCertificate Is Nothing, ICertificationView.Context.GotTestResults, ICertificationView.Context.ExistingCertificate), ICertificationView.Context)
                Case NameAid.SaveResult.Sucess
                    ' Assigns ID for a new certificate, no change otherwise
                    m_view.CertificateNumberID = objCertificate.CertificateNumberID

                    ClearSimilarTireMode()

                    strInfoText = CertificateSavedText

                    enuNotificationResult = AuditLogModel.CheckForChangesAndSendNotification(AuditLogEntry.AreaOfChange.Certification, dtmSaveTime)

                    If enuNotificationResult = AuditLogModel.NotificationResult.Sent Then
                        strInfoText &= NotificationSentText
                    ElseIf enuNotificationResult = AuditLogModel.NotificationResult.SendError Then
                        strErrorText = NotificationFailedText
                    ElseIf enuNotificationResult = AuditLogModel.NotificationResult.Disabled Then
                        strInfoText &= NotificationDisabledText
                    End If
                    enuCertViewContext = ICertificationView.Context.ExistingCertificate

                    ' Refresh certificate and test results to see the saved data:
                    Try
                        LoadCertificateData()
                    Catch exc As Exception
                        EventLogger.Enter(exc)
                        strErrorText &= ErrorLoadCertificateDataText
                    End Try
                    ShowTestResults()

                Case NameAid.SaveResult.SaveError
                    strErrorText = SaveErrorText
                    enuCertViewContext = CType(IIf(objCertificate.OriginalCertificate Is Nothing, ICertificationView.Context.GotTestResults, ICertificationView.Context.ExistingCertificate), ICertificationView.Context)
            End Select

            m_view.InfoText = strInfoText
            m_view.ErrorText = strErrorText
            m_view.SetupControlContextState(enuCertViewContext)
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSaveDataForMaterialNumberText + m_view.MaterialNumber + "."
            m_view.SetupControlContextState(ICertificationView.Context.GotTestResults)
        End Try
    End Sub

    ''' <summary>
    ''' Event handler for get test result button click.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnShowTestResults()
        Try
            ShowTestResults()
            m_view.SetupControlContextState(ICertificationView.Context.GotTestResults)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Load and show test results view.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ShowTestResults()
        Try
            m_view.TRView.CertificationTypeId = m_view.CertificationType
            m_view.TRView.MaterialNumber = m_view.MaterialNumber
            m_view.TRView.SKUID = m_view.SKUID

            'Get location id
            m_view.TRView.ManufacturingLocationId = CInt(m_view.ManufacturingLocationId)

            If Not m_view.OriginalCertificate Is Nothing Then
                If Not m_view.TRView.IsVisible And Not m_blnTRsExist Then
                    m_view.TRView.CertificateNumber = String.Empty
                Else
                    m_view.TRView.CertificateNumber = m_view.OriginalCertificate.CertificateNumber
                End If

                m_view.TRView.ExtensionNo = CStr(m_view.OriginalCertificate.Extension_EN)
                m_view.TRView.CertificateNumberID = m_view.OriginalCertificate.CertificateNumberID

                If m_view.SimilarTireCertificate IsNot Nothing Then
                    m_view.TRView.SimilarTireMatlNum = m_view.SimilarTireCertificate.MaterialNumber
                    m_view.TRView.SimilarTireSKUID = m_view.SimilarTireCertificate.SKUID
                End If

            End If
            m_view.TRView.IsVisible = True

            m_view.TRView.DoLoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadTestResultsDataText
        End Try
    End Sub

    ''' <summary>
    ''' Event handler for get blank result button click.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnGetBlankResults()
        Try
            GetBlankResults()
            m_view.SetupControlContextState(ICertificationView.Context.GotTestResults)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Show blank results.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub GetBlankResults()
        Try
            m_view.TRView.CertificationTypeId = m_view.CertificationType
            m_view.TRView.MaterialNumber = m_view.MaterialNumber
            m_view.TRView.SKUID = m_view.SKUID

            'Get location id
            m_view.TRView.ManufacturingLocationId = CInt(m_view.ManufacturingLocationId)
            m_view.TRView.IsVisible = True
            m_view.TRView.DoLoadViewBlank()
        Catch ex As Exception
            EventLogger.Enter(ex)
            m_view.ErrorText = ErrorLoadTestResultsDataText
        End Try
    End Sub

    ''' <summary>
    ''' On Get Similar Tires Certification.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnGetTestResults()
        Const ErrorCheckSimilarTireCertificationText As String = "Error Checking for similar tire certification."
        Try
            Dim certModel As New CertificateModel()
            Dim strMessage As String = String.Empty
            Dim strECEReference As String = String.Empty
            Dim intImarkFamily As Integer

            ClearSimilarTireMode()
            'If Not m_view.CertificationType = "ECE3054" Or m_view.CertificationType = "ECE117" Then
            If Not m_view.CertificationType = 1 Or m_view.CertificationType = 1 Then
                m_view.SimilarCertificateDS = certModel.CheckSimilarTireFromSKUTRACS(m_view.CertificationNumber, m_view.MaterialNumber, m_view.CertificationType, intImarkFamily, strECEReference, strMessage)
            Else
                m_view.SimilarCertificateDS = Nothing
            End If

            If Not m_view.SimilarCertificateDS Is Nothing Then
                If m_view.SimilarCertificateDS.Rows.Count > 0 Then
                    m_view.SimilarTireMessage = strMessage & System.Environment.NewLine & "Do you want to use a similar Material Number certification?"
                    m_view.ShowSimilarTirePrompt()
                Else
                    OnShowTestResults()
                End If
            Else
                OnShowTestResults()
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)

            m_view.ErrorText = ErrorCheckSimilarTireCertificationText
        End Try
    End Sub

    ''' <summary>
    ''' Copy similar tire certificate with test results.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnCopySimilarTireSKUCertificate()
        Try
            If m_view.SimilarTireCertificate IsNot Nothing Then
                'Add 1 to extension for similar tire if date submitted is not null for ECE30/54
                'If m_view.CertificationType = "ECE3054" Or m_view.CertificationType = "ECE117" Then
                If m_view.CertificationType = 1 Or m_view.CertificationType = 1 Then

                    'How should I check date submitted since this is at the SKU level.
                    If Not m_view.SimilarTireCertificate.DateSubmitted.Equals(DateTime.MinValue) Then
                        m_view.SimilarTireCertificate.Extension_EN = m_view.SimilarTireCertificate.Extension_EN + 1
                        m_view.SimilarTireCertificate.DateAssigned_EGI = Today()
                        m_view.SimilarTireCertificate.DateSubmitted = Nothing
                        m_view.SimilarTireCertificate.DateApproved_CEGI = Nothing
                    End If
                End If
                MapCertificateToView(m_view.SimilarTireCertificate)
                CertificationSearchPresenter.CurrentCertificateNumber = m_view.SimilarTireCertificate.CertificateNumber
                m_view.OriginalCertificate = m_view.SimilarTireCertificate
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Show default values view for this certificate.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnShowDefaultValues()
        Const ErrorShowDefaultValueText As String = "Error showing default values."
        Try
            m_view.SetupDefaultValuesView()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorShowDefaultValueText
        End Try
    End Sub

    ''' <summary>
    ''' Clear Similar Tire Certificate.
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ClearSimilarTireMode()
        Try
            m_view.SimilarTireCertificate = Nothing
            m_view.TRView.SimilarTireMatlNum = String.Empty
            m_view.TRView.SimilarTireSKUID = 0
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Chcek whether Family exists and get the value of Family Desc.
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <param name="p_strCertificationNumber">Certification number</param>
    ''' <param name="p_intCertificationTypeID">Certification Type Id</param>
    ''' <param name="p_intSKUID">SKU Id</param>
    ''' <param name="p_strMatlNum">Material Number</param>
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertificate(ByVal p_strCertificationNumber As String, ByVal p_intCertificationTypeID As Integer, ByVal p_intSKUID As Integer, ByVal p_strMatlNum As String) As Boolean

        Dim blnDone As Boolean = False
        Const ErrorSimilarTireCertificationText As String = "Error retrieving similar tire certification."
        Try
            Dim certModel As New CertificateModel()
            Dim blnTRExists As Boolean  'Not used in this circumstance.  Just need to pass it so I can use this function.

            m_view.SimilarTireCertificate = certModel.GetCertificate(p_strCertificationNumber, "*", p_strMatlNum, p_intCertificationTypeID, p_intSKUID, blnTRExists)
            blnDone = True
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorSimilarTireCertificationText
            Return blnDone
        End Try

        Return blnDone
    End Function

    ''' <summary>
    ''' Chcek whether Family exists and get the value of Family Desc.
    ''' </summary>
    ''' <param name="p_intCertificateid">Certificate Id</param>
    ''' <param name="p_intFamilyId">Family Id</param>
    ''' <param name="p_strFamilyDesc">Family Desc</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>Boolean</returns>
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
    ''' <para>10/24/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function IsFamilyExist(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As String, ByRef p_strFamilyDesc As String) As Boolean
        Const FamilyIdNotExistText As String = "Family Id does not exist."
        Try
            Dim objCertificationModel As New CertificateModel
            Return objCertificationModel.CheckIsFamilyIdExist(p_intCertificateid, p_intFamilyId, p_strFamilyDesc)
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = FamilyIdNotExistText
            Return CBool(m_view.ErrorText)
        End Try
    End Function
#End Region

End Class
