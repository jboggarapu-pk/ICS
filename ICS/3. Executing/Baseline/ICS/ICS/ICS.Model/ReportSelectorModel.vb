Imports CrystalDecisions.CrystalReports.Engine
Imports System.Configuration
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports System.Reflection
Imports TRACSSharedDatasets
Imports CooperTire.ICS.Datasets
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Class to execute any crystal reports requested by the UI
''' </summary>
''' <remarks></remarks>
Public Class ReportSelectorModel

    ' Modified the report tiles from SKU to Material number and generated new datasets by incrementing with 1 and saved in ICS.Datasets project.
    ' and reports are set with new datsets as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Member"
    Private _strReportDir As String = ""
    Private _dict As Dictionary(Of NameAid.Report, String)
    'Private _types As Dictionary(Of NameAid.Certification, String)
#End Region

#Region "Constructor"
    Public Sub New(ByVal p_pathUrl As String)
        _strReportDir = p_pathUrl + "\"
        BuildList()
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property AvailReports() As Dictionary(Of NameAid.Report, String)
        Get
            Return _dict
        End Get

    End Property

    'Public ReadOnly Property CertificateTypes() As Dictionary(Of NameAid.Certification, String)
    '    Get
    '        Return _types
    '    End Get

    'End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Executes the Emark Truck Certification report
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <param name="p_strExtensionNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmarkE117Certification(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument

        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkE117Certification)
        Dim rdReportDoc As ReportDocument = New EmarkE117Report
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetDataForEmarkE117Report(p_strCertificationNo, Depository.Current.GetCertificationTypeID("ECE117"), p_strExtensionNo, NameAid.TireType.SpecialtyLightTruck) 'TODO:Find out if it is medium truck here....
        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\Emark117b.XSD")
        'GoTo skip_Report
        'jes
        If dstResults.Tables(0).Rows.Count > 0 Then
            rdReportDoc.SetDataSource(dstResults)
        End If

        Return rdReportDoc
        'SKIP_REPORT:
    End Function

    ''' <summary>
    ''' Get emark melksham report data
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmarkMelkshamReportData(ByVal p_strCertificationNo As String) As ReportDocument

        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkMelkshamTestReport)
        Dim rdReportDoc As ReportDocument = New EmarkMelkshamTestReport
        'Dim dstResults As New EmarkPassengerWithTR
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetDataForEmarkReportWithTR(p_strCertificationNo, NameAid.TireType.Passenger)

        'If dstResults.CERTIFICATE_VIEW.Rows.Count > 0 Then
        If dstResults.Tables("CERTIFICATE_VIEW").Rows.Count > 0 Then
            'If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
            ' dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(dstResults.CERTIFICATE_VIEW.Rows(0), "", "", "", "", "", "", "", "", "", "", "")
            'End If

            rdReportDoc.SetDataSource(dstResults)

        End If

        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Executes the Emark Passenger Certification Report
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <param name="p_strExtensionNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmarkPassengerCertification(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument

        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkPassengerCertification)
        Dim rdReportDoc As ReportDocument = New EmarkPassengerCertification
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetDataForEmarkPassengerReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("ECE3054"), p_strExtensionNo, NameAid.TireType.Passenger)

        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\EmarkCertificationReport5.XSD")
        'GoTo skip_Report
        'jes


        If dstResults.Tables(0).Rows.Count > 0 Then


            'If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
            ' dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(dstResults.Certificate.Rows(0), "", "", "", "", "", "", "", "", "", "", "")
            'End If

            rdReportDoc.SetDataSource(dstResults)
        End If

        Return rdReportDoc
        'skip_report:
    End Function

    ''' <summary>
    ''' Executes the Emark Light Truck Certification Report
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <param name="p_strExtensionNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmarkLightTruckCertification(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument

        Dim rdReportDoc As ReportDocument = New EmarkLightTruckCertification
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkLightTruckCertification)
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetDataForEmarkPassengerReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("ECE3054"), p_strExtensionNo, NameAid.TireType.LightTruck)
        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\EmarkCertificationReport5.XSD")
        'GoTo skip_Report
        'jes


        If dstResults.Tables(0).Rows.Count > 0 Then
            'If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
            'dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(dstResults.Certificate.Rows(0), "", "", "", "", "", "", "", "", "", "", "")
            'End If

            rdReportDoc.SetDataSource(dstResults)
        End If
        Return rdReportDoc

    End Function

    ''' <summary>
    ''' CCC Certification for a given Certification Number
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCCCSequentialReport(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument
        Dim dstResults As New CCCSequentialDataSet
        Dim rdReportDoc As ReportDocument = New CCCSequentialReport

        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.CCCSequentialReport)

        dstResults = Depository.Current.GetDataForCCCReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("CCC"), p_strExtensionNo)

        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\CCCSequentialDataSet1.XSD")
        'GoTo skip_Report
        'jes

        If dstResults.CERTIFICATE.Rows.Count > 0 Then

            If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
                dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(dstResults.CERTIFICATE.Rows(0), "", "", "", "", "", "", "", _
                                                                        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                                        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                                        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                                        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                                        "", "", "", "", "", "")
            End If

            rdReportDoc.SetDataSource(dstResults)
        End If
        Return rdReportDoc
    End Function

    ''' <summary>
    ''' CCC Product Description Report for a given Certification Number
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <param name="p_strExtensionNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCCCProductDescriptionReport(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As DataSet

        Dim dstResults As New CCCProductDescriptionDataSet
        'Dim rdReportDoc As ReportDocument = New ReportDocument()
        'Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.CCCSequentialReport)

        dstResults = Depository.Current.GetDataForCCCProductDescriptionReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("CCC"), p_strExtensionNo)

        If Not dstResults.CERTIFICATE.Rows.Count > 0 Then
            dstResults = Nothing
            '    If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
            '        dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(dstResults.CERTIFICATE.Rows(0), "", "", "", "", "", "", "", _
            '                                                                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
            '                                                                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
            '                                                                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
            '                                                                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
            '                                                                "", "", "", "", "", "")
            '    End If

            '    rdReportDoc.Load(_strReportDir + crystalFileName)
            '    rdReportDoc.SetDataSource(dstResults)
        End If
        Return dstResults
    End Function

    ''' <summary>
    ''' Executes Imark Certification report
    ''' </summary>
    ''' <param name="p_dtDateParam">Search date is optional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function GetImarkCertification(ByVal p_dtDateParam As DateTime) As ReportDocument

        Dim dstResults As New ImarkCertificationDataSet
        Dim rdReportDoc As ReportDocument = New ImarkCertification
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.ImarkCertification)

        dstResults = Depository.Current.GetDataForImarkCertification(p_dtDateParam)

        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\ImarkCertificationDataSet3.XSD")
        'GoTo skip_Report
        'jes

        If dstResults.IMARKCERTIFICATE_VIEW.Rows.Count > 0 Then
            rdReportDoc.SetDataSource(dstResults)
        End If
        'skip_report:
        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Executes Emark Certification report
    ''' </summary>
    ''' <returns>ReportDocument</returns>
    ''' <remarks></remarks>
    Public Function GetEmarkCertification(ByVal p_strCertificationNo, ByVal p_strBrand, ByVal p_strBrandLine) As ReportDocument

        Dim dstResults As New DataSet
        Dim rdReportDoc As ReportDocument = New EmarkCertification
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkCertification)

        dstResults = Depository.Current.GetDataForEmarkCertificationReport(p_strCertificationNo, p_strBrand, p_strBrandLine)


        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\EmarkCertificationListDataset.XSD")
        'GoTo skip_report1
        'jes

        If dstResults.Tables(0).Rows.Count > 0 Then
            rdReportDoc.SetDataSource(dstResults)
        End If
        'skip_report1:
        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Get Imark Conformity report data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetImarkConformityCertification() As ReportDocument

        Dim rdReportDoc As ReportDocument = New ImarkConformityMarkReport
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.ImarkConformityMarkReport)
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetDataForImarkConformityReport()


        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\ImarkConformity1.XSD")
        'GoTo skip_report
        'jes

        If dstResults.Tables(0).Rows.Count > 0 Then



            'If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
            '    dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(dstResults.Certificate.Rows(0), "", "", "", "", "", "", "", "", "", "", "")
            'End If

            rdReportDoc.SetDataSource(dstResults)
        End If
        'skip_report:
        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Get Imark Sampling and test results data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetImarkSamplingAndTireTests(ByVal p_strMatlNum As String) As ReportDocument

        Dim dstResults As New DataSet
        Dim rdReportDoc As ReportDocument = New ImarkSamplingAndTestTireReport
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.ImarkSamplingAndTireTests)

        dstResults = Depository.Current.GetDataForImarkSamplingTireTestsReport(p_strMatlNum)
        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\ImarkSamplingDataset2.XSD")
        'GoTo skip_report
        'jes
        If dstResults.Tables(0).Rows.Count > 0 Then
            'If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
            '    dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(dstResults.Certificate.Rows(0), "", "", "", "", "", "", "", "", "", "", "")
            'End If
            rdReportDoc.SetDataSource(dstResults)
        End If

        Return rdReportDoc
        'SKIP_REPORT:
    End Function

    ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Get SKU certification data
    ''' </summary>
    ''' <param name="p_strMatlNum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSKUCertification(ByVal p_strMatlNum As String, ByVal p_strBrand As String, ByVal p_strBrandLine As String, ByVal p_strCertType As String) As ReportDocument
        Dim rdReportDoc As ReportDocument = New SKUCertification
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.SKUCertification)
        Dim dstResults As New CertificateReport

        dstResults = Depository.Current.GetDataForSKUCertification(p_strMatlNum, p_strBrand, p_strBrandLine, p_strCertType)

        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\CertificateReport1.XSD")
        'GoTo skip_report
        'jes

        If dstResults.PRODUCTDATA_REPORT_VIEW.Rows.Count > 0 Then
            rdReportDoc.SetDataSource(dstResults)
        End If

        Return rdReportDoc
    End Function

    ''' <summary>
    ''' Report for a GSO Light truck sequential
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <param name="p_strExtensionNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGSOLightTruckSequential(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument
        Dim rdReportDoc As ReportDocument = New GSOLightTruckSequential

        ' As per IDEA2706, Modified the name to GSOTruckCertification from GSOLightTruckSequential (TD #1)
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.GSOTruckCertification)
        Dim dstResults As New GSOCertificateDataSet

        dstResults = Depository.Current.GetDataForGSOPassengerReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("GSO"), p_strExtensionNo, NameAid.TireType.LightTruck)

        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\GSOCertificateDataset1.XSD")
        'GoTo skip_report
        'jes

        If dstResults.Certificate.Rows.Count > 0 Then

            If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
                dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(dstResults.Certificate.Rows(0), "", "", "", "", "", "", "", "")
            End If

            If dstResults.ENDURANCEHDR.Rows.Count = 0 Then
                dstResults.ENDURANCEHDR.AddENDURANCEHDRRow(0, "", 0, "", DateTime.Now(), "", "", DateTime.Now(), 0, 0, 0, DateTime.Now(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now(), DateTime.Now(), 0, "", 0, dstResults.Certificate.Rows(0), DateTime.MinValue, 0, 0, "", DateTime.MinValue, "", Nothing, 0, 0, 0, 0, "", 0, "", 0, 0, 0, "", "", "")
            End If

            If dstResults.HIGHSPEEDHDR.Rows.Count = 0 Then
                dstResults.HIGHSPEEDHDR.AddHIGHSPEEDHDRRow(0, "", 0, "", DateTime.Now(), "", "", DateTime.Now(), 0, 0, 0, DateTime.Now(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now(), DateTime.Now(), 0, "", DateTime.MinValue, 0, 0, dstResults.Certificate.Rows(0), "", DateTime.MinValue, "", DateTime.MinValue, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", 0, "", 0, 0, 0)
            End If

            rdReportDoc.SetDataSource(dstResults)

        End If

        Return rdReportDoc

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <param name="p_strExtensionNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGSOPassengerCertification(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument

        Dim rdReportDoc As ReportDocument = New GSOPassengerCertification
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.GSOPassengerCertification)
        Dim dstResults As New GSOCertificateDataSet

        dstResults = Depository.Current.GetDataForGSOPassengerReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("GSO"), p_strExtensionNo, NameAid.TireType.Passenger)

        If dstResults.Certificate.Rows.Count > 0 Then

            If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
                dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(dstResults.Certificate.Rows(0), "", "", "", "", "", "", "", "")
            End If

            If dstResults.ENDURANCEHDR.Rows.Count = 0 Then
                dstResults.ENDURANCEHDR.AddENDURANCEHDRRow(0, "", 0, "", DateTime.Now(), "", "", DateTime.Now(), 0, 0, 0, DateTime.Now(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now(), DateTime.Now(), 0, "", 0, dstResults.Certificate.Rows(0), DateTime.MinValue, 0, 0, "", DateTime.MinValue, "", Nothing, 0, 0, 0, 0, "", 0, "", 0, 0, 0, "", "", "")
            End If

            If dstResults.HIGHSPEEDHDR.Rows.Count = 0 Then
                dstResults.HIGHSPEEDHDR.AddHIGHSPEEDHDRRow(0, "", 0, "", DateTime.Now(), "", "", DateTime.Now(), 0, 0, 0, DateTime.Now(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now(), DateTime.Now(), 0, "", DateTime.MinValue, 0, 0, dstResults.Certificate.Rows(0), "", DateTime.MinValue, "", DateTime.MinValue, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", 0, "", 0, 0, 0)
            End If

            If dstResults.BEADUNSEATHDR.Rows.Count = 0 Then
                dstResults.BEADUNSEATHDR.AddBEADUNSEATHDRRow(0, String.Empty, 0, String.Empty, DateTime.Now, String.Empty, 0, String.Empty, 0, dstResults.Certificate.Rows(0), DateTime.Now, 0, String.Empty, DateTime.Now, String.Empty, DateTime.Now, String.Empty)
            End If
            If dstResults.MEASUREHDR.Rows.Count = 0 Then
                dstResults.MEASUREHDR.AddMEASUREHDRRow(0, String.Empty, 0, String.Empty, DateTime.Now, 0, String.Empty, 0, String.Empty, 0, 0, 0, 0, 0, DateTime.Now, 0, DateTime.Now, DateTime.Now, 0, 0, dstResults.Certificate.Rows(0), String.Empty, DateTime.Now, String.Empty, DateTime.Now, 0, 0, String.Empty, 0, 0, 0, String.Empty, 0, 0, 0, 0, String.Empty, String.Empty, 0, 0, 0, 0, 0, 0, 0, 0, 0)
            End If
            If dstResults.PLUNGERHDR.Rows.Count = 0 Then
                dstResults.PLUNGERHDR.AddPLUNGERHDRRow(0, String.Empty, 0, String.Empty, DateTime.Now, String.Empty, 0, String.Empty, 0, dstResults.Certificate.Rows(0), DateTime.Now, 0, String.Empty, DateTime.Now, String.Empty, DateTime.Now)
            End If

            If dstResults.TREADWEARHDR.Rows.Count = 0 Then
                dstResults.TREADWEARHDR.AddTREADWEARHDRRow(0, String.Empty, 0, String.Empty, DateTime.Now, 0, 0, String.Empty, DateTime.Now, 0, dstResults.Certificate.Rows(0), String.Empty, DateTime.Now, String.Empty, DateTime.Now, 0)
            End If

            rdReportDoc.SetDataSource(dstResults)

        End If

        Return rdReportDoc

    End Function

    Public Function GetGSOConformityReportData(ByVal p_strBatchNo As String) As ReportDocument
        Dim rdReportDoc As ReportDocument = New GSOConformityCertificateReport
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.GSOConformityCertificateReport)
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetDataForGSOConformityReport(p_strBatchNo, Depository.Current.GetCertificationTypeID("GSO"), NameAid.TireType.Passenger)

        If dstResults.Tables(0).Rows.Count > 0 Then
            'jes - uncomment this to create or update the schema, then comment back out.
            'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\GSOConformity.XSD")
            'GoTo skip_Report
            'jes
            rdReportDoc.SetDataSource(dstResults)

        End If
        'skip_report:
        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Get emark MSR passenger data
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmarkMSRPassengerData(ByVal p_strCertificationNo As String) As ReportDocument

        Dim rdReportDoc As ReportDocument = New EmarkTestReport
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkMSRPassengerReport)
        'Dim dstResults As New EmarkPassengerWithTR
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetDataForEmarkReportWithTR(p_strCertificationNo, NameAid.TireType.Passenger)

        If dstResults.Tables("CERTIFICATE_VIEW").Rows.Count > 0 Then

            'If dstResults.Tables("DEFAULTVALUES_VIEW").Rows.Count = 0 Then
            ' dstResults.Tables("DEFAULTVALUES_VIEW").AddDEFAULTVALUES_VIEWRow(dstResults.Tables("CERTIFICATE_VIEW").Rows(0), "", "", "", "", "", "", "", "", "", "", "")
            'End If

            'jes - uncomment this to create or update the schema, then comment back out.
            'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\EmarkTestReportInfo9.XSD")
            'GoTo skip_Report
            'jes


            rdReportDoc.SetDataSource(dstResults)
            rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'MSRPass'"
        End If
        'SKIP_REPORT:
        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Get emark MSR truck data
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmarkMSRTruckData(ByVal p_strCertificationNo As String) As ReportDocument

        Dim rdReportDoc As ReportDocument = New EmarkTestReport
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkMSRTruckReport)
        'Dim dstResults As New EmarkPassengerWithTR
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetDataForEmarkReportWithTR(p_strCertificationNo, NameAid.TireType.LightTruck)

        'If dstResults.CERTIFICATE_VIEW.Rows.Count > 0 Then
        If dstResults.Tables("CERTIFICATE_VIEW").Rows.Count > 0 Then

            'If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
            ' dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(dstResults.CERTIFICATE_VIEW.Rows(0), "", "", "", "", "", "", "", "", "", "", "")
            'End If

            rdReportDoc.SetDataSource(dstResults)
            rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'MSRTruck'"
        End If

        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Get emark ME truck data
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmarkMETruckData(ByVal p_strCertificationNo As String) As ReportDocument

        Dim rdReportDoc As ReportDocument = New EmarkTestReport
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkMETruckReport)
        'Dim dstResults As New EmarkPassengerWithTR
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetDataForEmarkReportWithTR(p_strCertificationNo, NameAid.TireType.LightTruck)

        If dstResults.Tables(0).Rows.Count > 0 Then

            'If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
            'dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(dstResults.CERTIFICATE_VIEW.Rows(0), "", "", "", "", "", "", "", "", "", "", "")
            'End If

            rdReportDoc.SetDataSource(dstResults)
            rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'METruck'"
        End If

        Return rdReportDoc

    End Function

    'Added p_strIncludeArchived as per IDEA2706 (TD#2)
    ''' <summary>
    ''' Get traceability report info
    ''' </summary>
    ''' <param name="p_strCertificationNo"></param>
    ''' <param name="p_intCertificationTypeId"></param>
    '''  <param name="p_strIncludeArchived">p_strIncludeArchived</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTraceabilityData(ByVal p_strCertificationNo As String, ByVal p_intCertificationTypeId As Integer, ByVal p_strIncludeArchived As String) As ReportDocument

        Dim rdReportDoc As ReportDocument = New TraceabilityReport
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.TraceabilityReport)
        Dim dstResults As New Traceability

        dstResults = Depository.Current.GetTraceabilityReportInfo(p_strCertificationNo, p_intCertificationTypeId, p_strIncludeArchived)

        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\Traceability2.XSD")
        'GoTo skip_Report
        'jes

        If dstResults.TRACEABILITY_VIEW.Rows.Count > 0 Then

            rdReportDoc.SetDataSource(dstResults)

        End If

        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Get exception report data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetExceptionReportData() As ReportDocument

        Dim rdReportDoc As ReportDocument = New ExceptionReport
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.ExceptionReport)
        Dim dstResults As New ExceptionReport_DataSet

        dstResults = Depository.Current.GetExceptionReportInfo()

        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\ExceptionReport_DataSet1.XSD")
        'GoTo skip_Report
        'jes

        If dstResults.EXCEPTIONREPORT.Rows.Count > 0 Then

            rdReportDoc.SetDataSource(dstResults)

        End If

        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Get emark passenger app. data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmarkPassengerApplication(ByVal p_strCertificateNumber As String) As ReportDocument

        Dim rdReportDoc As ReportDocument = New EmarkApp
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkPassengerApplication)
        'Dim dstResults As New ExceptionReport_DataSet
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetEmarkApplication(p_strCertificateNumber, NameAid.TireType.Passenger)

        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.Datasets\EmarkAppDataset3.XSD")
        'GoTo skip_report
        'jes

        If dstResults.Tables("CERTIFICATE_VIEW").Rows.Count > 0 Then

            rdReportDoc.SetDataSource(dstResults)
            rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'Pass'"
        End If
        'skip_report:
        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Get emark light truck app.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmarkLightTruckApplication(ByVal p_strCertificateNumber As String) As ReportDocument

        Dim rdReportDoc As ReportDocument = New EmarkApp
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkLightTruckApplication)
        'Dim dstResults As New ExceptionReport_DataSet
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetEmarkApplication(p_strCertificateNumber, NameAid.TireType.LightTruck)

        If dstResults.Tables("CERTIFICATE_VIEW").Rows.Count > 0 Then

            rdReportDoc.SetDataSource(dstResults)
            rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'LightTruck'"
        End If

        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Get nom passenger cert data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNomPassengerCertification(ByVal p_strCertificateNumber As String) As ReportDocument

        Dim rdReportDoc As ReportDocument = New NOMCertification
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.NOMPassengerCertification)
        'Dim dstResults As New ExceptionReport_DataSet
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetNomCertification(p_strCertificateNumber, NameAid.TireType.Passenger)

        If dstResults.Tables("CERTIFICATE").Rows.Count > 0 Then

            'jes - uncomment this to create or update the schema, then comment back out.
            'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.DATASETS\NOMCertificationDataset7.XSD")
            'GoTo skip_report
            'jes

            'rdReportDoc.SetDataSource(dstResults)
            'rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'Pass'"

            ' If they selected passemger, use passenger form unless it is a light truck tire with
            ' a load index less than 113, then use special passenger form.
            If dstResults.Tables("PRODUCTDATA").Rows(0).Item("TIRETYPEID").ToString.Trim <> "1" And _
              CInt(dstResults.Tables("PRODUCTDATA").Rows(0).Item("SINGLOADINDEX").ToString) < 113 Then
                rdReportDoc.SetDataSource(dstResults)
                rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'PassLT'"
            Else
                rdReportDoc.SetDataSource(dstResults)
                rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'Pass'"
            End If



        End If
        'skip_report:
        Return rdReportDoc

    End Function

    Public Function GetNomLightTruckCertification(ByVal p_strCertificateNumber As String) As ReportDocument

        Dim rdReportDoc As ReportDocument = New NOMCertification
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.NOMLightTruckCertification)
        'Dim dstResults As New ExceptionReport_DataSet
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetNomCertification(p_strCertificateNumber, NameAid.TireType.LightTruck)

        If dstResults.Tables("CERTIFICATE").Rows.Count > 0 Then
            ' rdReportDoc.SetDataSource(dstResults)
            ' If they selected light truck, use light truck form unless it is a passenger tire with
            ' a load index greather than 112, then use special light truck form.
            If dstResults.Tables("PRODUCTDATA").Rows(0).Item("TIRETYPEID").ToString.Trim = "1" And _
            CInt(dstResults.Tables("PRODUCTDATA").Rows(0).Item("SINGLOADINDEX").ToString) > 112 Then
                rdReportDoc.SetDataSource(dstResults)
                rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'PassOnLT'"
            Else
                rdReportDoc.SetDataSource(dstResults)
                rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'LightTruck'"
            End If

            'If CInt(dstResults.Tables("PRODUCTDATA").Rows(0).Item("SINGLOADINDEX").ToString) < 113 Then
            '    'use passenger format if load index is less than 113

            '    rdReportDoc.SetDataSource(dstResults)
            '    rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'PassLT'"
            'Else
            '    rdReportDoc.SetDataSource(dstResults)
            '    rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'LightTruck'"
            'End If

        End If

        Return rdReportDoc

    End Function

    ''' <summary>
    ''' Get exception report data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAuthenticity() As ReportDocument

        Dim rdReportDoc As ReportDocument = New AuthenticityLetter
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.ImarkECEAuthenticityReport)
        'Dim dstResults As New ExceptionReport_DataSet
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetAuthenticityReport
        'jes - uncomment this to create or update the schema, then comment back out.
        'jes dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\AuthenticityDataset.XSD")
        'jes GoTo skip_ReportA
        'jes
        If dstResults.Tables(0).Rows.Count > 0 Then

            rdReportDoc.SetDataSource(dstResults)

        End If
        'jes skip_ReportA:
        Return rdReportDoc

    End Function

    Public Function EmarkSimilarCertificateSearch(ByVal p_strMatlNum As String) As ReportDocument
        Dim rdReportDoc As ReportDocument = New EmarkSimilarCertificate
        'Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkSimilarCertificateSearch)
        Dim dstResults As New DataSet
        dstResults = Depository.Current.GetEmarkSimilarCertificateSearchReport(p_strMatlNum)

        'jes - uncomment this to create or update the schema, then comment back out.
        'dstResults.WriteXmlSchema("C:\Inetpub\wwwroot\ICS\ICS.DATASETS\EmarkSimilarCertificatesDataset3.XSD")
        'GoTo skip_Report
        'jes
        If dstResults.Tables(0).Rows.Count > 0 Then

            rdReportDoc.SetDataSource(dstResults)
            rdReportDoc.DataDefinition.FormulaFields.Item("QuerySku").Text = "'" & p_strMatlNum & "'"
        End If
skip_Report:
        Return rdReportDoc


    End Function

    Public Function GetCertificationRenewal(ByVal p_strCertificateNumber As String, ByVal p_intCertificationTypeID As Integer) As ReportDocument

        Dim rdReportDoc As ReportDocument = New EmarkApp 'jill - change this
        Dim crystalFileName = AppSettingsAid.GetReportFileName(NameAid.Report.CertificationRenewalReport)
        'Dim dstResults As New ExceptionReport_DataSet
        Dim dstResults As New DataSet

        dstResults = Depository.Current.GetCertificationRenewal(p_strCertificateNumber, p_intCertificationTypeID)

        If dstResults.Tables(0).Rows.Count > 0 Then

            rdReportDoc.SetDataSource(dstResults)

        End If

        Return rdReportDoc

    End Function

    ' Modified the report tiles from SKU to Material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ' As per Idea 2706,Modified the name of GSOLightTruckSequential to GSOTruckCertification in reports dropdown (TD #1)
    Private Sub BuildList()

        _dict = New Dictionary(Of NameAid.Report, String)
        _dict.Add(NameAid.Report.CCCSequentialReport, "CCC Sequential Report")
        _dict.Add(NameAid.Report.CCCProductDescriptionReport, "CCC Product Description Report")
        '_dict.Add(NameAid.Report.EmarkCertification, "Emark Certification")
        _dict.Add(NameAid.Report.EmarkPassengerApplication, "Emark Passenger Application")
        _dict.Add(NameAid.Report.EmarkLightTruckApplication, "Emark Light Truck Application")
        _dict.Add(NameAid.Report.EmarkLightTruckCertification, "Emark Light Truck Certification")
        _dict.Add(NameAid.Report.EmarkPassengerCertification, "Emark Passenger Certification")
        _dict.Add(NameAid.Report.EmarkE117Certification, "Emark E117 Report")
        _dict.Add(NameAid.Report.EmarkMSRPassengerReport, "Emark MSR Passenger Test Report")
        _dict.Add(NameAid.Report.EmarkMSRTruckReport, "Emark MSR Truck Test Report")
        _dict.Add(NameAid.Report.EmarkMETruckReport, "Emark ME Truck Test Report")
        '_dict.Add(NameAid.Report.EmarkMelkshamTestReport, "Emark Melksham Test Report")

        'Changed report name as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
        _dict.Add(NameAid.Report.EmarkSimilarCertificateSearch, "ECE Approval Numbers with Similar Materials Report")

        _dict.Add(NameAid.Report.GSOPassengerCertification, "GSO Passenger Certification")
        _dict.Add(NameAid.Report.GSOTruckCertification, "GSO Truck Certification")
        _dict.Add(NameAid.Report.GSOConformityCertificateReport, "GSO Conformity Certificate Report")
        _dict.Add(NameAid.Report.ImarkCertification, "Imark Certification")
        _dict.Add(NameAid.Report.ImarkConformityMarkReport, "Imark Conformity Mark Report")
        _dict.Add(NameAid.Report.ImarkSamplingAndTireTests, "Imark Sampling and Tire Tests")
        _dict.Add(NameAid.Report.ImarkECEAuthenticityReport, "Imark ECE Authenticity Report")
        'jes 7/22/11    _dict.Add(NameAid.Report.NOMPassengerCertification, "NOM Passenger Certification Report")
        _dict.Add(NameAid.Report.NOMPassengerCertification, "NOM-086-SCFI-2010")
        _dict.Add(NameAid.Report.NOMLightTruckCertification, "NOM-086/1-SCFI-2011")

        'Changed report name as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
        _dict.Add(NameAid.Report.SKUCertification, "Material Certification")

        _dict.Add(NameAid.Report.TraceabilityReport, "Performance Report")

        'Changed report name as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
        _dict.Add(NameAid.Report.ExceptionReport, "Material Master / ICS Difference Report")

        '_dict.Add(NameAid.Report.CertificationRenewalReport, "Certification Renewal Report")

        '_types = New Dictionary(Of NameAid.Certification, String)
        '_types.Add(0, String.Empty)
        '_types.Add(NameAid.Certification.CCC, NameAid.Certification.CCC.ToString())
        '_types.Add(NameAid.Certification.ECE3054, NameAid.Certification.ECE3054.ToString())
        '_types.Add(NameAid.Certification.ECE117, NameAid.Certification.ECE117.ToString())
        '_types.Add(NameAid.Certification.GSO, NameAid.Certification.GSO.ToString())
        '_types.Add(NameAid.Certification.Imark, NameAid.Certification.Imark.ToString())
        '_types.Add(NameAid.Certification.NOM, NameAid.Certification.NOM.ToString())
        '_types.Add(NameAid.Certification.India_Mark, NameAid.Certification.India_Mark.ToString())

    End Sub

#End Region

End Class
