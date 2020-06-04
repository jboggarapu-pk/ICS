Imports CrystalDecisions.CrystalReports.Engine
Imports System.Configuration
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports System.Reflection

Imports CooperTire.ICS.Datasets
Imports CooperTire.ICS.DepositoryTender

''' <summary>
'''  Class to execute any crystal reports requested by the UI.
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
''' <para>10/04/2019</para>
''' <para>Implemented Coding Standards</para>
''' </description>
''' </item> 
''' </list>
''' </remarks>
Public Class ReportSelectorModel

    ' Modified the report tiles from SKU to Material number and generated new datasets by incrementing with 1 and saved in ICS.Datasets project.
    ' and reports are set with new datasets as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Member"

    ''' <summary>
    ''' Variable to hold ReportDir.
    ''' </summary>
    ''' <remarks></remarks>
    Private _strReportDir As String = ""

    ''' <summary>
    ''' Variable to hold Dictionary.
    ''' </summary>
    ''' <remarks></remarks>
    Private _dict As Dictionary(Of NameAid.Report, String)

#End Region

#Region "Constructor"
    ''' <summary>
    '''  Constructor  for class.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_pathUrl As String)
        _strReportDir = p_pathUrl + "\"
        BuildList()
    End Sub
#End Region

#Region "Properties"

    ''' <summary>
    ''' Variable to hold Avail reports
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property AvailReports() As Dictionary(Of NameAid.Report, String)
        Get
            Return _dict
        End Get

    End Property

#End Region

#Region "Methods"

    ''' <summary>
    '''  Method to Executes the Emark Truck Certification report.
    ''' </summary>
    ''' <returns>Report Document object</returns> 
    ''' <param name="p_strCertificationNo">Certification Number</param>
    ''' <param name="p_strExtensionNo">Extension Number</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetEmarkE117Certification(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument
        Try
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkE117Certification)
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
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get emark melksham report data.
    ''' </summary>
    ''' <returns>Report Document object</returns> 
    ''' <param name="p_strCertificationNo">Certification Number.</param>    
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetEmarkMelkshamReportData(ByVal p_strCertificationNo As String) As ReportDocument
        Try
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkMelkshamTestReport)
            Dim rdReportDoc As ReportDocument = New EmarkMelkshamTestReport
            'Dim dstResults As New EmarkPassengerWithTR
            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetDataForEmarkReportWithTR(p_strCertificationNo, NameAid.TireType.Passenger)

            If dstResults.Tables("CERTIFICATE_VIEW").Rows.Count > 0 Then
                rdReportDoc.SetDataSource(dstResults)
            End If
            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Executes the Emark Passenger Certification Report.
    ''' </summary>
    ''' <returns>Report Document object </returns> 
    '''  <param name="p_strCertificationNo"> Certification number.</param>
    ''' <param name="p_strExtensionNo">Extension number.</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>     
    Public Function GetEmarkPassengerCertification(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument
        Try
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkPassengerCertification)
            Dim rdReportDoc As ReportDocument = New EmarkPassengerCertification
            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetDataForEmarkPassengerReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("ECE3054"), p_strExtensionNo, NameAid.TireType.Passenger)

            If dstResults.Tables(0).Rows.Count > 0 Then
                rdReportDoc.SetDataSource(dstResults)
            End If

            Return rdReportDoc
            'skip_report:
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Executes the Emark Light Truck Certification Report.
    ''' </summary>
    ''' <returns>Report Document object</returns> 
    ''' <param name="p_strCertificationNo"> Certification number.</param>
    ''' <param name="p_strExtensionNo">Extension number.</param>    
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item>  
    ''' </list>
    ''' </remarks>   
    Public Function GetEmarkLightTruckCertification(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New EmarkLightTruckCertification
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkLightTruckCertification)
            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetDataForEmarkPassengerReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("ECE3054"), p_strExtensionNo, NameAid.TireType.LightTruck)

            If dstResults.Tables(0).Rows.Count > 0 Then
                rdReportDoc.SetDataSource(dstResults)
            End If
            Return rdReportDoc

        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method  to CCC Certification for a given Certification Number.
    ''' </summary>
    ''' <returns>Report Document</returns> 
    ''' <param name="p_strCertificationNo">Certification number</param>
    ''' <param name="p_strExtensionNo">Extension number</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>>
    Public Function GetCCCSequentialReport(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument
        Try
            Dim dstResults As New CCCSequentialDataSet
            Dim rdReportDoc As ReportDocument = New CCCSequentialReport

            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.CCCSequentialReport)

            dstResults = Depository.Current.GetDataForCCCReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("CCC"), p_strExtensionNo)

            If dstResults.CERTIFICATE.Rows.Count > 0 Then

                If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
                    dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(CType(dstResults.CERTIFICATE.Rows(0), CCCSequentialDataSet.CERTIFICATERow), "", "", "", "", "", "", "", _
                                                                            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                                            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                                            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                                            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                                            "", "", "", "", "", "")
                End If

                rdReportDoc.SetDataSource(dstResults)
            End If
            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to CCC Product Description Report for a given Certification Number.
    ''' </summary>
    ''' <returns>Dataset</returns> 
    ''' <param name="p_strCertificationNo">Certification number</param>
    ''' <param name="p_strExtensionNo">Extension number</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCCCProductDescriptionReport(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As DataSet
        Try
            Dim dstResults As New CCCProductDescriptionDataSet
            dstResults = Depository.Current.GetDataForCCCProductDescriptionReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("CCC"), p_strExtensionNo)

            If Not dstResults.CERTIFICATE.Rows.Count > 0 Then
                dstResults = Nothing
            End If
            Return dstResults
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Executes Imark Certification report.
    ''' </summary>
    ''' <returns>Report Document object</returns> 
    ''' <param name="p_dtDateParam">Search date is optional</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetImarkCertification(ByVal p_dtDateParam As DateTime) As ReportDocument
        Try
            Dim dstResults As New ImarkCertificationDataSet
            Dim rdReportDoc As ReportDocument = New ImarkCertification
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.ImarkCertification)

            dstResults = Depository.Current.GetDataForImarkCertification(p_dtDateParam)

            If dstResults.IMARKCERTIFICATE_VIEW.Rows.Count > 0 Then
                rdReportDoc.SetDataSource(dstResults)
            End If
            'skip_report:
            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Executes Emark Certification report
    ''' </summary>
    ''' <returns>ReportDocument</returns>
    ''' <remarks></remarks>
    Public Function GetEmarkCertification(ByVal p_strCertificationNo As String, ByVal p_strBrand As String, ByVal p_strBrandLine As String) As ReportDocument
        Try
            Dim dstResults As New DataSet
            Dim rdReportDoc As ReportDocument = New EmarkCertification
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkCertification)

            dstResults = Depository.Current.GetDataForEmarkCertificationReport(p_strCertificationNo, p_strBrand, p_strBrandLine)

            If dstResults.Tables(0).Rows.Count > 0 Then
                rdReportDoc.SetDataSource(dstResults)
            End If
            'skip_report1:
            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get Imark Conformity report data.
    ''' </summary>
    ''' <returns>Report Document object</returns> 
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetImarkConformityCertification() As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New ImarkConformityMarkReport
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.ImarkConformityMarkReport)
            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetDataForImarkConformityReport()

            If dstResults.Tables(0).Rows.Count > 0 Then
                rdReportDoc.SetDataSource(dstResults)
            End If
            'skip_report:
            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get Imark Sampling and test results data.
    ''' </summary>
    ''' <returns>Report Document object</returns> 
    ''' <param name="p_strMatlNum">Material number.</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Function GetImarkSamplingAndTireTests(ByVal p_strMatlNum As String) As ReportDocument
        Try
            Dim dstResults As New DataSet
            Dim rdReportDoc As ReportDocument = New ImarkSamplingAndTestTireReport
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.ImarkSamplingAndTireTests)

            dstResults = Depository.Current.GetDataForImarkSamplingTireTestsReport(p_strMatlNum)

            If dstResults.Tables(0).Rows.Count > 0 Then
                rdReportDoc.SetDataSource(dstResults)
            End If

            Return rdReportDoc
            'SKIP_REPORT:
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

    ''' <summary>
    '''  Method to Get SKU certification data.
    ''' </summary>
    ''' <returns>Report Document object</returns> 
    '''  <param name="p_strBrand"> brand.</param>
    ''' <param name="p_strBrandLine"> brand line.</param>
    ''' <param name="p_strCertType">CertificateType.</param>    
    ''' <param name="p_strMatlNum">Material number.</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetSKUCertification(ByVal p_strMatlNum As String, ByVal p_strBrand As String, ByVal p_strBrandLine As String, ByVal p_strCertType As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New SKUCertification
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.SKUCertification)
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
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Report for a GSO Light truck sequential.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
    ''' <param name="p_strCertificationNo">Certification number.</param>
    ''' <param name="p_strExtensionNo">Extension number.</param>    
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetGSOLightTruckSequential(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument
        Dim rdReportDoc As ReportDocument = New GSOLightTruckSequential
        Try
            ' As per IDEA2706, Modified the name to GSOTruckCertification from GSOLightTruckSequential (TD #1)
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.GSOTruckCertification)
            Dim dstResults As New GSOCertificateDataSet

            dstResults = Depository.Current.GetDataForGSOPassengerReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("GSO"), p_strExtensionNo, NameAid.TireType.LightTruck)

            If dstResults.Certificate.Rows.Count > 0 Then

                If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
                    dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(CType(dstResults.Certificate.Rows(0), GSOCertificateDataSet.CertificateRow), "", "", "", "", "", "", "", "")
                End If

                If dstResults.ENDURANCEHDR.Rows.Count = 0 Then
                    dstResults.ENDURANCEHDR.AddENDURANCEHDRRow(0, "", 0, "", DateTime.Now(), "", "", DateTime.Now(), 0, 0, 0, DateTime.Now(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now(), DateTime.Now(), 0, "", 0, CType(dstResults.Certificate.Rows(0), GSOCertificateDataSet.CertificateRow), DateTime.MinValue, 0, 0, "", DateTime.MinValue, "", Nothing, 0, 0, 0, 0, "", 0, "", 0, 0, 0, "", "", "")
                End If

                If dstResults.HIGHSPEEDHDR.Rows.Count = 0 Then
                    dstResults.HIGHSPEEDHDR.AddHIGHSPEEDHDRRow(0, "", 0, "", DateTime.Now(), "", "", DateTime.Now(), 0, 0, 0, DateTime.Now(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now(), DateTime.Now(), 0, "", DateTime.MinValue, 0, 0, CType(dstResults.Certificate.Rows(0), GSOCertificateDataSet.CertificateRow), "", DateTime.MinValue, "", DateTime.MinValue, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", 0, "", 0, 0, 0)
                End If

                rdReportDoc.SetDataSource(dstResults)

            End If

            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Gets GSO Passenger Certification.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
    '''  <param name="p_strCertificationNo">Certification number.</param>
    ''' <param name="p_strExtensionNo">Extension number.</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetGSOPassengerCertification(ByVal p_strCertificationNo As String, ByVal p_strExtensionNo As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New GSOPassengerCertification
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.GSOPassengerCertification)
            Dim dstResults As New GSOCertificateDataSet

            dstResults = Depository.Current.GetDataForGSOPassengerReport(p_strCertificationNo, Depository.Current.GetCertificationTypeID("GSO"), p_strExtensionNo, NameAid.TireType.Passenger)

            If dstResults.Certificate.Rows.Count > 0 Then

                If dstResults.DEFAULTVALUES_VIEW.Rows.Count = 0 Then
                    dstResults.DEFAULTVALUES_VIEW.AddDEFAULTVALUES_VIEWRow(CType(dstResults.Certificate.Rows(0), GSOCertificateDataSet.CertificateRow), "", "", "", "", "", "", "", "")
                End If

                If dstResults.ENDURANCEHDR.Rows.Count = 0 Then
                    dstResults.ENDURANCEHDR.AddENDURANCEHDRRow(0, "", 0, "", DateTime.Now(), "", "", DateTime.Now(), 0, 0, 0, DateTime.Now(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now(), DateTime.Now(), 0, "", 0, CType(dstResults.Certificate.Rows(0), GSOCertificateDataSet.CertificateRow), DateTime.MinValue, 0, 0, "", DateTime.MinValue, "", Nothing, 0, 0, 0, 0, "", 0, "", 0, 0, 0, "", "", "")
                End If

                If dstResults.HIGHSPEEDHDR.Rows.Count = 0 Then
                    dstResults.HIGHSPEEDHDR.AddHIGHSPEEDHDRRow(0, "", 0, "", DateTime.Now(), "", "", DateTime.Now(), 0, 0, 0, DateTime.Now(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now(), DateTime.Now(), 0, "", DateTime.MinValue, 0, 0, CType(dstResults.Certificate.Rows(0), GSOCertificateDataSet.CertificateRow), "", DateTime.MinValue, "", DateTime.MinValue, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", 0, "", 0, 0, 0)
                End If

                If dstResults.BEADUNSEATHDR.Rows.Count = 0 Then
                    dstResults.BEADUNSEATHDR.AddBEADUNSEATHDRRow(0, String.Empty, 0, String.Empty, DateTime.Now, String.Empty, 0, String.Empty, 0, CType(dstResults.Certificate.Rows(0), GSOCertificateDataSet.CertificateRow), DateTime.Now, 0, String.Empty, DateTime.Now, String.Empty, DateTime.Now, String.Empty)
                End If
                If dstResults.MEASUREHDR.Rows.Count = 0 Then
                    dstResults.MEASUREHDR.AddMEASUREHDRRow(0, String.Empty, 0, String.Empty, DateTime.Now, 0, String.Empty, 0, String.Empty, 0, 0, 0, 0, 0, DateTime.Now, 0, DateTime.Now, DateTime.Now, 0, 0, CType(dstResults.Certificate.Rows(0), GSOCertificateDataSet.CertificateRow), String.Empty, DateTime.Now, String.Empty, DateTime.Now, 0, 0, String.Empty, 0, 0, 0, String.Empty, 0, 0, 0, 0, String.Empty, String.Empty, 0, 0, CStr(0), 0, 0, 0, 0, 0, 0)
                End If
                If dstResults.PLUNGERHDR.Rows.Count = 0 Then
                    dstResults.PLUNGERHDR.AddPLUNGERHDRRow(0, String.Empty, 0, String.Empty, DateTime.Now, String.Empty, 0, String.Empty, 0, CType(dstResults.Certificate.Rows(0), GSOCertificateDataSet.CertificateRow), DateTime.Now, 0, String.Empty, DateTime.Now, String.Empty, DateTime.Now)
                End If

                If dstResults.TREADWEARHDR.Rows.Count = 0 Then
                    dstResults.TREADWEARHDR.AddTREADWEARHDRRow(0, String.Empty, 0, String.Empty, DateTime.Now, CStr(0), 0, String.Empty, DateTime.Now, 0, CType(dstResults.Certificate.Rows(0), GSOCertificateDataSet.CertificateRow), String.Empty, DateTime.Now, String.Empty, DateTime.Now, 0)
                End If

                rdReportDoc.SetDataSource(dstResults)

            End If

            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get GSO conformity report data.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
    '''  <param name="p_strBatchNo">Bathc number.</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetGSOConformityReportData(ByVal p_strBatchNo As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New GSOConformityCertificateReport
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.GSOConformityCertificateReport)
            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetDataForGSOConformityReport(p_strBatchNo, Depository.Current.GetCertificationTypeID("GSO"), NameAid.TireType.Passenger)

            If dstResults.Tables(0).Rows.Count > 0 Then
                rdReportDoc.SetDataSource(dstResults)

            End If
            'skip_report:
            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get emark MSR passenger data.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
    ''' <param name="p_strCertificationNo">Ceritification number.</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Function GetEmarkMSRPassengerData(ByVal p_strCertificationNo As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New EmarkTestReport
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkMSRPassengerReport)
            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetDataForEmarkReportWithTR(p_strCertificationNo, NameAid.TireType.Passenger)

            If dstResults.Tables("CERTIFICATE_VIEW").Rows.Count > 0 Then

                rdReportDoc.SetDataSource(dstResults)
                rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'MSRPass'"
            End If
            'SKIP_REPORT:
            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get emark MSR truck data.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
    ''' <param name="p_strCertificationNo">Certification number.</param>    
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetEmarkMSRTruckData(ByVal p_strCertificationNo As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New EmarkTestReport
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkMSRTruckReport)
            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetDataForEmarkReportWithTR(p_strCertificationNo, NameAid.TireType.LightTruck)

            If dstResults.Tables("CERTIFICATE_VIEW").Rows.Count > 0 Then

                rdReportDoc.SetDataSource(dstResults)
                rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'MSRTruck'"
            End If

            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get emark ME truck data.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
    ''' <param name="p_strCertificationNo">Certification number.</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetEmarkMETruckData(ByVal p_strCertificationNo As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New EmarkTestReport
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkMETruckReport)
            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetDataForEmarkReportWithTR(p_strCertificationNo, NameAid.TireType.LightTruck)

            If dstResults.Tables(0).Rows.Count > 0 Then
                rdReportDoc.SetDataSource(dstResults)
                rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'METruck'"
            End If

            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added p_strIncludeArchived as per IDEA2706 (TD#2)

    ''' <summary>
    '''  Method to Get traceability report info.
    ''' </summary>
    ''' <returns>Report Document Object</returns>     
    ''' <param name="p_strCertificationNo">Certification number</param>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    '''  <param name="p_strIncludeArchived">Include Archived string</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetTraceabilityData(ByVal p_strCertificationNo As String, ByVal p_intCertificationTypeId As Integer, ByVal p_strIncludeArchived As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New TraceabilityReport
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.TraceabilityReport)
            Dim dstResults As New Traceability

            dstResults = Depository.Current.GetTraceabilityReportInfo(p_strCertificationNo, p_intCertificationTypeId, p_strIncludeArchived)

            If dstResults.TRACEABILITY_VIEW.Rows.Count > 0 Then

                rdReportDoc.SetDataSource(dstResults)

            End If

            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get exception report data.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item>  
    ''' </list>
    ''' </remarks>
    Public Function GetExceptionReportData() As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New ExceptionReport
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.ExceptionReport)
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
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Get emark passenger app data.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetEmarkPassengerApplication(ByVal p_strCertificateNumber As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New EmarkApp
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkPassengerApplication)
            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetEmarkApplication(p_strCertificateNumber, NameAid.TireType.Passenger)

            If dstResults.Tables("CERTIFICATE_VIEW").Rows.Count > 0 Then

                rdReportDoc.SetDataSource(dstResults)
                rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'Pass'"
            End If
            'skip_report:
            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Get emark light truck app..
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
    ''' <param name="p_strCertificateNumber">Certification number.</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetEmarkLightTruckApplication(ByVal p_strCertificateNumber As String) As ReportDocument

        Try
            Dim rdReportDoc As ReportDocument = New EmarkApp
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.EmarkLightTruckApplication)

            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetEmarkApplication(p_strCertificateNumber, NameAid.TireType.LightTruck)

            If dstResults.Tables("CERTIFICATE_VIEW").Rows.Count > 0 Then

                rdReportDoc.SetDataSource(dstResults)
                rdReportDoc.DataDefinition.FormulaFields.Item("RptType").Text = "'LightTruck'"
            End If

            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Gets the product country status table.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
    ''' <param name="p_strCertificateNumber">Certificate number.</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetNomPassengerCertification(ByVal p_strCertificateNumber As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New NOMCertification
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.NOMPassengerCertification)

            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetNomCertification(p_strCertificateNumber, NameAid.TireType.Passenger)

            If dstResults.Tables("CERTIFICATE").Rows.Count > 0 Then

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
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Gets Nom light Truck Certification.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
    ''' <param name="p_strCertificateNumber">Certificate number.</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetNomLightTruckCertification(ByVal p_strCertificateNumber As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New NOMCertification
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.NOMLightTruckCertification)

            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetNomCertification(p_strCertificateNumber, NameAid.TireType.LightTruck)

            If dstResults.Tables("CERTIFICATE").Rows.Count > 0 Then

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

            End If

            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get exception report data.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetAuthenticity() As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New AuthenticityLetter
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.ImarkECEAuthenticityReport)

            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetAuthenticityReport

            If dstResults.Tables(0).Rows.Count > 0 Then

                rdReportDoc.SetDataSource(dstResults)

            End If
            'jes skip_ReportA:
            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Emark Similar Certificate Search.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
    ''' <param name="p_strMatlNum">Material number.</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function EmarkSimilarCertificateSearch(ByVal p_strMatlNum As String) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New EmarkSimilarCertificate
            Dim dstResults As New DataSet
            dstResults = Depository.Current.GetEmarkSimilarCertificateSearchReport(p_strMatlNum)

            If dstResults.Tables(0).Rows.Count > 0 Then

                rdReportDoc.SetDataSource(dstResults)
                rdReportDoc.DataDefinition.FormulaFields.Item("QuerySku").Text = "'" & p_strMatlNum & "'"
            End If
skip_Report:
            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get Certification renewal.
    ''' </summary>
    ''' <returns>Report Document Object</returns> 
    ''' <param name="p_strCertificateNumber">Certificate number.</param>
    ''' <param name="p_intCertificationTypeID">Certification Type Id.</param>    
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCertificationRenewal(ByVal p_strCertificateNumber As String, ByVal p_intCertificationTypeID As Integer) As ReportDocument
        Try
            Dim rdReportDoc As ReportDocument = New EmarkApp 'jill - change this
            Dim crystalFileName As String = AppSettingsAid.GetReportFileName(NameAid.Report.CertificationRenewalReport)
            Dim dstResults As New DataSet

            dstResults = Depository.Current.GetCertificationRenewal(p_strCertificateNumber, p_intCertificationTypeID)

            If dstResults.Tables(0).Rows.Count > 0 Then

                rdReportDoc.SetDataSource(dstResults)

            End If

            Return rdReportDoc
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' Modified the report tiles from SKU to Material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ' As per Idea 2706,Modified the name of GSOLightTruckSequential to GSOTruckCertification in reports dropdown (TD #1)
    ''' <summary>
    '''  Method to Build dictionary list.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Sub BuildList()
        Try
            _dict = New Dictionary(Of NameAid.Report, String)
            _dict.Add(NameAid.Report.CCCSequentialReport, "CCC Sequential Report")
            _dict.Add(NameAid.Report.CCCProductDescriptionReport, "CCC Product Description Report")
            _dict.Add(NameAid.Report.EmarkPassengerApplication, "Emark Passenger Application")
            _dict.Add(NameAid.Report.EmarkLightTruckApplication, "Emark Light Truck Application")
            _dict.Add(NameAid.Report.EmarkLightTruckCertification, "Emark Light Truck Certification")
            _dict.Add(NameAid.Report.EmarkPassengerCertification, "Emark Passenger Certification")
            _dict.Add(NameAid.Report.EmarkE117Certification, "Emark E117 Report")
            _dict.Add(NameAid.Report.EmarkMSRPassengerReport, "Emark MSR Passenger Test Report")
            _dict.Add(NameAid.Report.EmarkMSRTruckReport, "Emark MSR Truck Test Report")
            _dict.Add(NameAid.Report.EmarkMETruckReport, "Emark ME Truck Test Report")

            'Changed report name as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
            _dict.Add(NameAid.Report.EmarkSimilarCertificateSearch, "ECE Approval Numbers with Similar Materials Report")

            _dict.Add(NameAid.Report.GSOPassengerCertification, "GSO Passenger Certification")
            _dict.Add(NameAid.Report.GSOTruckCertification, "GSO Truck Certification")
            _dict.Add(NameAid.Report.GSOConformityCertificateReport, "GSO Conformity Certificate Report")
            _dict.Add(NameAid.Report.ImarkCertification, "Imark Certification")
            _dict.Add(NameAid.Report.ImarkConformityMarkReport, "Imark Conformity Mark Report")
            _dict.Add(NameAid.Report.ImarkSamplingAndTireTests, "Imark Sampling and Tire Tests")
            _dict.Add(NameAid.Report.ImarkECEAuthenticityReport, "Imark ECE Authenticity Report")
            _dict.Add(NameAid.Report.NOMPassengerCertification, "NOM-086-SCFI-2010")
            _dict.Add(NameAid.Report.NOMLightTruckCertification, "NOM-086/1-SCFI-2011")

            'Changed report name as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
            _dict.Add(NameAid.Report.SKUCertification, "Material Certification")

            _dict.Add(NameAid.Report.TraceabilityReport, "Performance Report")

            'Changed report name as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
            _dict.Add(NameAid.Report.ExceptionReport, "Material Master / ICS Difference Report")
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

End Class
