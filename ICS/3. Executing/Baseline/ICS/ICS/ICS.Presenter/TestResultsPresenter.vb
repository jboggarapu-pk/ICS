Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Test results presenter
''' </summary>
''' <remarks></remarks>
Public Class TestResultsPresenter

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"

    Private m_view As ITestResultsView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As ITestResultsView)
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw New Exception("Error creating " + Me.ToString())
        End Try

    End Sub

#End Region

#Region "Properties"
#End Region

#Region "Methods"

    ''' <summary>
    ''' Attach presenter to view’s events.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SubscribeToEvents()

        AddHandler m_view.LoadView, AddressOf OnLoadView
        AddHandler m_view.DoLoadViewDataEvent, AddressOf OnDoLoadViewData
        AddHandler m_view.DoLoadViewBlankEvent, AddressOf OnDoLoadViewBlank
        AddHandler m_view.GetRequestedTests, AddressOf OnGetRequestedTests

    End Sub

    ''' <summary>
    ''' Load data for the view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)

        Try
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error loading form data."
        End Try

    End Sub

    ''' <summary>
    ''' Unconditional load view data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnDoLoadViewData()

        Try
            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error loading form data."

        End Try

    End Sub

    ''' <summary>
    ''' Unconditional load view blank
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnDoLoadViewBlank()

        Try
            LoadViewBlank()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error loading form data."
        End Try
    End Sub

    ''' <summary>
    ''' Unconditional get requested tests
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnGetRequestedTests()

        Try
            GetRequestedTests()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error getting requested test results."

        End Try

    End Sub

    ''' <summary>
    ''' Load data from business process
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewData()

        If Not m_view.IsVisible Then
            Return
        End If

        m_view.InfoText = String.Empty
        m_view.ErrorText = String.Empty

        LoadData()
        m_view.AdjustViewToCertificationType()

    End Sub

    ''' <summary>
    ''' Load blank test results form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewBlank()

        If Not m_view.IsVisible Then
            Return
        End If

        m_view.InfoText = String.Empty
        m_view.ErrorText = String.Empty

        LoadBlank()
        m_view.AdjustViewToCertificationType()

    End Sub

    ''' <summary>
    ''' Get requested tests from business process
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRequestedTests()

        If Not m_view.IsVisible Then
            Return
        End If

        m_view.InfoText = String.Empty
        m_view.ErrorText = String.Empty

        GetTests()
        m_view.AdjustViewToCertificationType()

    End Sub

    ''' <summary>
    ''' Load and set view data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadData()

        Dim testResultModel As New TestResultsModel()
        Dim TireTypeId As Integer

        Dim objTRProductSectionData As TRProductSectionData = testResultModel.GetProductTRSectionData(m_view.MaterialNumber.PadLeft(18, "0"), _
                                                                    m_view.CertificateNumber, m_view.SKUID, TireTypeId, m_view.CertificationTypeId, _
                                                                    m_view.ManufacturingLocationId)


        objTRProductSectionData.TireType = testResultModel.GetTireType()
        objTRProductSectionData.TireId = TireTypeId
        m_view.TireTypeId = TireTypeId
        Dim strCertifiedMaterialNo As String = m_view.MaterialNumber.PadLeft(18, "0")
        Dim intCertifiedSKUID As Integer = m_view.SKUID
        If m_view.SimilarTireMatlNum IsNot String.Empty AndAlso m_view.SimilarTireSKUID <> 0 Then
            ' Similar certified Material number with test results
            strCertifiedMaterialNo = m_view.SimilarTireMatlNum
            intCertifiedSKUID = m_view.SimilarTireSKUID
        End If

        If (m_view.CertificationTypeId = 4) Then
            objTRProductSectionData.IMarkMudSnow = objTRProductSectionData.MudSnow
            objTRProductSectionData.IMarkSevereWeatherInd = objTRProductSectionData.SevereWeatherIndicator
        End If

        Dim testResultDataList As List(Of Object) = testResultModel.GetTRSectionData(strCertifiedMaterialNo, intCertifiedSKUID, _
                                                                m_view.CertificateNumber, m_view.CertificateNumberID, _
                                                                m_view.CertificationTypeId, TireTypeId, m_view.ManufacturingLocationId)
        If testResultDataList.Count > 0 Then

            Dim objTRProjectSectionData As TRProjectSectionData = CType(testResultDataList(0), TRProjectSectionData)
            Dim objTRMeasurementSectionData As TRMeasurementSectionData = CType(testResultDataList(1), TRMeasurementSectionData)
            Dim objTREnduranceBeforeSectionData As TREnduranceTestGeneralBeforeSectionData = CType(testResultDataList(2), TREnduranceTestGeneralBeforeSectionData)
            Dim objTREnduranceSectionData As TREnduranceSectionData = CType(testResultDataList(3), TREnduranceSectionData)
            Dim objTREnduranceAfterSectionData As TREnduranceTestGeneralAfterSectionData = CType(testResultDataList(4), TREnduranceTestGeneralAfterSectionData)
            Dim objTRHighSpeedBeforeSectionData As TRHighSpeedTestGeneralBeforeSectionData = CType(testResultDataList(5), TRHighSpeedTestGeneralBeforeSectionData)
            Dim objTRHighSpeedSectionData As TRHighSpeedSectionData = CType(testResultDataList(6), TRHighSpeedSectionData)
            Dim objTRHighSpeedAfterSectionData As TRHighSpeedTestGeneralAfterSectionData = CType(testResultDataList(7), TRHighSpeedTestGeneralAfterSectionData)
            Dim objTRSoundWetSectionData As TRSoundWetSectionData = CType(testResultDataList(8), TRSoundWetSectionData)

            m_view.SetTRProductSectionData(objTRProductSectionData)
            m_view.SetTRMeasureSectionData(objTRMeasurementSectionData)
            m_view.SetTRProjectSectionData(objTRProjectSectionData)
            m_view.SetTREnduranceBeforeSectionData(objTREnduranceBeforeSectionData)
            m_view.SetTREnduranceSectionData(objTREnduranceSectionData)
            m_view.SetTREnduranceAfterSectionData(objTREnduranceAfterSectionData)
            m_view.SetTRHighSpeedBeforeSectionData(objTRHighSpeedBeforeSectionData)
            m_view.SetTRHighSpeedSectionData(objTRHighSpeedSectionData)
            m_view.SetTRHighSpeedAfterSectionData(objTRHighSpeedAfterSectionData)
            m_view.SetTRSoundWetSectionData(objTRSoundWetSectionData)

        End If

    End Sub

    ''' <summary>
    ''' Load and set view blank
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadBlank()

        Dim testResultModel As New TestResultsModel()
        Dim TireTypeId As Integer

        Dim objTRProductSectionData As TRProductSectionData = testResultModel.GetProductTRSectionData(m_view.MaterialNumber.PadLeft(18, "0"), _
                                                                    m_view.CertificateNumber, m_view.SKUID, TireTypeId, m_view.CertificationTypeId, _
                                                                    m_view.ManufacturingLocationId)

        m_view.TireTypeId = TireTypeId

        Dim objTRProjectSectionData As TRProjectSectionData = New TRProjectSectionData
        Dim objTRMeasurementSectionData As TRMeasurementSectionData = New TRMeasurementSectionData
        Dim objTREnduranceBeforeSectionData As TREnduranceTestGeneralBeforeSectionData = New TREnduranceTestGeneralBeforeSectionData
        Dim objTREnduranceSectionData As TREnduranceSectionData = New TREnduranceSectionData
        Dim objTREnduranceAfterSectionData As TREnduranceTestGeneralAfterSectionData = New TREnduranceTestGeneralAfterSectionData
        Dim objTRHighSpeedBeforeSectionData As TRHighSpeedTestGeneralBeforeSectionData = New TRHighSpeedTestGeneralBeforeSectionData
        Dim objTRHighSpeedSectionData As TRHighSpeedSectionData = New TRHighSpeedSectionData
        Dim objTRHighSpeedAfterSectionData As TRHighSpeedTestGeneralAfterSectionData = New TRHighSpeedTestGeneralAfterSectionData
        Dim objTRSoundWetSectionData As TRSoundWetSectionData = New TRSoundWetSectionData

        m_view.SetTRProductSectionData(objTRProductSectionData)
        m_view.SetTRMeasureSectionData(objTRMeasurementSectionData)
        m_view.SetTRProjectSectionData(objTRProjectSectionData)
        m_view.SetTREnduranceBeforeSectionData(objTREnduranceBeforeSectionData)
        m_view.SetTREnduranceSectionData(objTREnduranceSectionData)
        m_view.SetTREnduranceAfterSectionData(objTREnduranceAfterSectionData)
        m_view.SetTRHighSpeedBeforeSectionData(objTRHighSpeedBeforeSectionData)
        m_view.SetTRHighSpeedSectionData(objTRHighSpeedSectionData)
        m_view.SetTRHighSpeedAfterSectionData(objTRHighSpeedAfterSectionData)
        m_view.SetTRSoundWetSectionData(objTRSoundWetSectionData)

    End Sub

    ''' <summary>
    ''' Get requested tests and set view data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetTests()

        Dim testResultModel As New TestResultsModel()
        Dim strMFGWWYY As String
        Dim dteMostRecentTestDate As Date

        strMFGWWYY = String.Empty
        dteMostRecentTestDate = "01/01/1901"
        Dim testResultDataList As List(Of Object) = testResultModel.GetRequestedTests(m_view.MaterialNumber.PadLeft(18, "0"), m_view.SKUID, 0, m_view.CertificationTypeId, _
           m_view.TireTypeId, m_view.ClientRequest, strMFGWWYY, dteMostRecentTestDate)

        Dim objTRProjectSectionData As TRProjectSectionData = CType(testResultDataList(0), TRProjectSectionData)
        Dim objTRMeasurementSectionData As TRMeasurementSectionData = CType(testResultDataList(1), TRMeasurementSectionData)
        Dim objTREnduranceBeforeSectionData As TREnduranceTestGeneralBeforeSectionData = CType(testResultDataList(2), TREnduranceTestGeneralBeforeSectionData)
        Dim objTREnduranceSectionData As TREnduranceSectionData = CType(testResultDataList(3), TREnduranceSectionData)
        Dim objTREnduranceAfterSectionData As TREnduranceTestGeneralAfterSectionData = CType(testResultDataList(4), TREnduranceTestGeneralAfterSectionData)
        Dim objTRHighSpeedBeforeSectionData As TRHighSpeedTestGeneralBeforeSectionData = CType(testResultDataList(5), TRHighSpeedTestGeneralBeforeSectionData)
        Dim objTRHighSpeedSectionData As TRHighSpeedSectionData = CType(testResultDataList(6), TRHighSpeedSectionData)
        Dim objTRHighSpeedAfterSectionData As TRHighSpeedTestGeneralAfterSectionData = CType(testResultDataList(7), TRHighSpeedTestGeneralAfterSectionData)
        Dim objTRSoundWetSectionData As TRSoundWetSectionData = CType(testResultDataList(8), TRSoundWetSectionData)

        If (m_view.ClientRequest.Rows.Count > 0) Then
            Dim drClientRequest As DataRow = Nothing
            Dim testSpec As String = String.Empty
            Dim projectNum As String = String.Empty
            Dim tireNum As String = String.Empty
            Dim operation As String = String.Empty
            For Each drClientRequest In m_view.ClientRequest.Rows

                testSpec = drClientRequest("TestSpec")
                projectNum = drClientRequest("ProjectNum")
                tireNum = drClientRequest("TireNum")
                operation = drClientRequest("Operation")
                If testSpec = objTRProjectSectionData.MeasureTestSpec Then
                    If (projectNum = objTRProjectSectionData.MeasureProjectNumber And tireNum = objTRProjectSectionData.MeasureTireNumber And operation = objTRProjectSectionData.MeasureOperation) Then
                        m_view.SetTRMeasureSectionData(objTRMeasurementSectionData)
                    End If
                End If

                If testSpec = objTRProjectSectionData.PlungerTestSpec Then
                    If Not (projectNum = objTRProjectSectionData.PlungerProjectNumber And tireNum = objTRProjectSectionData.PlungerTireNumber And operation = objTRProjectSectionData.PlungerOperation) Then
                        objTRProjectSectionData.PlungerProjectNumber = projectNum
                        objTRProjectSectionData.PlungerTireNumber = tireNum
                        objTRProjectSectionData.PlungerOperation = operation
                    End If
                End If

                If testSpec = objTRProjectSectionData.BeadUnSeatTestSpec Then
                    If Not (projectNum = objTRProjectSectionData.BeadUnSeatProjectNumber And tireNum = objTRProjectSectionData.BeadUnSeatTireNumber And operation = objTRProjectSectionData.BeadUnSeatOperation) Then
                        objTRProjectSectionData.BeadUnSeatProjectNumber = projectNum
                        objTRProjectSectionData.BeadUnSeatTireNumber = tireNum
                        objTRProjectSectionData.BeadUnSeatOperation = operation
                    End If
                End If

                If testSpec = objTRProjectSectionData.EnduranceTestSpec Then
                    If (projectNum = objTRProjectSectionData.EnduranceProjectNumber And tireNum = objTRProjectSectionData.EnduranceTireNumber And operation = objTRProjectSectionData.EnduranceOperation) Then
                        m_view.SetTREnduranceBeforeSectionData(objTREnduranceBeforeSectionData)
                        m_view.SetTREnduranceSectionData(objTREnduranceSectionData)
                    End If
                End If

                If testSpec = objTRProjectSectionData.HighSpeedTestSpec Then
                    If (projectNum = objTRProjectSectionData.HighSpeedProjectNumber And tireNum = objTRProjectSectionData.HighSpeedTireNumber And operation = objTRProjectSectionData.HighSpeedOperation) Then
                        m_view.SetTRHighSpeedBeforeSectionData(objTRHighSpeedBeforeSectionData)
                        m_view.SetTRHighSpeedSectionData(objTRHighSpeedSectionData)
                        m_view.SetTRHighSpeedAfterSectionData(objTRHighSpeedAfterSectionData)
                    End If
                End If

                If testSpec = objTRProjectSectionData.SoundTestSpec Then
                    If (projectNum = objTRProjectSectionData.SoundProjectNumber And tireNum = objTRProjectSectionData.SoundTireNumber And operation = objTRProjectSectionData.SoundOperation) Then
                        m_view.SetTRSoundWetSectionData(objTRSoundWetSectionData)
                    End If
                End If

                If testSpec = objTRProjectSectionData.WetGripTestSpec Then
                    If (projectNum = objTRProjectSectionData.WetGripProjectNumber And tireNum = objTRProjectSectionData.WetGripTireNumber And operation = objTRProjectSectionData.WetGripOperation) Then
                        m_view.SetTRSoundWetSectionData(objTRSoundWetSectionData)
                    End If
                End If
            Next

            'GET product data off the screen to update the serial week or test date from the test results
            If dteMostRecentTestDate <> "01/01/1901" Or Not strMFGWWYY Is String.Empty Then
                Dim objTRProductSectionData As TRProductSectionData = m_view.GetTRProductSectionData
                'set most recent test dates
                If dteMostRecentTestDate <> "01/01/1901" Then
                    objTRProductSectionData.DateOfTest = dteMostRecentTestDate
                End If
                If Not strMFGWWYY Is String.Empty Then
                    objTRProductSectionData.MFGWWYY = strMFGWWYY
                End If
                'write back to screen
                m_view.SetTRProductSectionData(objTRProductSectionData)
            End If
        Else
            'm_view.SetTRProductSectionData(objTRProductSectionData)
            m_view.SetTRMeasureSectionData(objTRMeasurementSectionData)
            m_view.SetTRProjectSectionData(objTRProjectSectionData)
            m_view.SetTREnduranceBeforeSectionData(objTREnduranceBeforeSectionData)
            m_view.SetTREnduranceSectionData(objTREnduranceSectionData)
            m_view.SetTREnduranceAfterSectionData(objTREnduranceAfterSectionData)
            m_view.SetTRHighSpeedBeforeSectionData(objTRHighSpeedBeforeSectionData)
            m_view.SetTRHighSpeedSectionData(objTRHighSpeedSectionData)
            m_view.SetTRHighSpeedAfterSectionData(objTRHighSpeedAfterSectionData)
            m_view.SetTRSoundWetSectionData(objTRSoundWetSectionData)
        End If

    End Sub

    Public Function GetCertificationTypeName(ByVal p_intCertificationTypeID As Integer) As String

        Dim objCertModel As New CertificateModel

        Dim strCertificateName As String = objCertModel.GetCertificationName(p_intCertificationTypeID)

        Return strCertificateName

    End Function


    Public Function CertTemplate(ByVal strCertificationName As String) As String
        Dim certSearch As CertificationSearch = New CertificationSearch()
        Dim strCertTemplate As String
        strCertTemplate = certSearch.GetCertTemplate(strCertificationName)
        ' strCertTemplate is ""if certification name is not in CertificationType table
        Return strCertTemplate

    End Function

#End Region

End Class
