Imports System.Data
Imports System.Data.OracleClient
Imports System.Configuration

Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Datasets


''' <summary>
''' Data access layer file to handle de reports data info
''' </summary>
Public Class ReportsDalc
    Inherits CooperTire.ICS.CTS.DALC.CtsDalc

    ' Modified methods as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ' Used Material Number instead of SKU, PSN instead of NPRId, TPN instead of PPN and Brand, Brand Line instead of Brand Code.

    ''' <summary>
    ''' Gets the data for emark passenger report.
    ''' </summary>
    ''' <param name="p_strCertificateNumber">The  certificate number.</param>
    ''' <param name="p_intCertificationTypeId">The  certification type id.</param>
    ''' <param name="p_strExtension">The  extension.</param>
    ''' <returns></returns>
    Public Function GetDataForEmarkPassengerReport(ByVal p_strCertificateNumber As String, _
                                                   ByVal p_intCertificationTypeId As Integer, _
                                                   ByVal p_strExtension As String, _
                                                   ByVal p_intTireTypeId As Integer) As DataSet

        Dim dstResults As New DataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "reports_package.GetEmarkReportPassengerInfo"
            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand("pc_Certificate", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Brand", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand("pc_CertificateDfValue", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_skudetailscount, -- this is a cursor containing the number of distinct sets of sku values.
            ParametersHelper.AddParametersToCommand("pc_skudetailscount", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand("ps_certificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand("ps_extension", ParameterDirection.Input, OracleType.VarChar, p_strExtension, oraCmd)
            'pi_certificationTypeID in number,
            ParametersHelper.AddParametersToCommand("pi_certificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand("ps_Operatorid", ParameterDirection.Input, OracleType.VarChar, "ICSDEV", oraCmd)
            'pi_TireTypeID in number
            ParametersHelper.AddParametersToCommand("pi_TireTypeID", ParameterDirection.Input, OracleType.Number, p_intTireTypeId, oraCmd)

            'TODO:GEt the user

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add("Table", "CERTIFICATE")
            oraAdp.TableMappings.Add("Table1", "BRAND_VIEW")
            oraAdp.TableMappings.Add("Table2", "PRODUCTDATA_REPORT_VIEW")
            oraAdp.TableMappings.Add("Table3", "DEFAULTVALUES_VIEW")
            oraAdp.TableMappings.Add("Table4", "SKUDETAILSCOUNT")
            oraAdp.Fill(dstResults)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Get data for Emark E117 report
    ''' </summary>
    ''' <param name="p_strCertificateNumber"></param>
    ''' <param name="p_intCertificationTypeId"></param>
    ''' <param name="p_strExtension"></param>
    ''' <param name="p_intTireTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataForEmarkE117Report(ByVal p_strCertificateNumber As String, _
                                                   ByVal p_intCertificationTypeId As Integer, _
                                                   ByVal p_strExtension As String, _
                                                   ByVal p_intTireTypeId As Integer) As DataSet

        Dim dstResults As New DataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "reports_package.GetEmark117Info"

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand("pc_Certificate", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Brand", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand("pc_CertificateDfValue", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_skudetailscount, -- this is a cursor containing the number of distinct sets of sku values.
            ParametersHelper.AddParametersToCommand("pc_skudetailscount", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand("ps_certificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand("ps_extension", ParameterDirection.Input, OracleType.VarChar, p_strExtension, oraCmd)
            'pi_certificationTypeID in number,
            ParametersHelper.AddParametersToCommand("pi_certificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand("ps_Operatorid", ParameterDirection.Input, OracleType.VarChar, "ICSDEV", oraCmd)
            'pi_TireTypeID in number
            ParametersHelper.AddParametersToCommand("pi_TireTypeID", ParameterDirection.Input, OracleType.Number, p_intTireTypeId, oraCmd)

            'TODO:GEt the user

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add("Table", "CERTIFICATE")
            oraAdp.TableMappings.Add("Table1", "BRAND_VIEW")
            oraAdp.TableMappings.Add("Table2", "PRODUCTDATA_REPORT_VIEW")
            oraAdp.TableMappings.Add("Table3", "DEFAULTVALUES_VIEW")
            oraAdp.TableMappings.Add("Table4", "SKUDETAILSCOUNT")
            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the data for GSO passenger report.
    ''' </summary>
    ''' <param name="p_strCertificateNumber">The P_STR certificate number.</param>
    ''' <param name="p_intCertificationTypeId">The p_int certification type id.</param>
    ''' <param name="p_strExtension">The P_STR extension.</param>
    ''' <param name="p_intTireTypeId">The tire type id. (ignored for now but kept in place for future use.)</param>
    ''' <returns></returns>
    Public Function GetDataForGSOPassengerReport(ByVal p_strCertificateNumber As String, _
                                                   ByVal p_intCertificationTypeId As Integer, _
                                                   ByVal p_strExtension As String, _
                                                   ByVal p_intTireTypeId As Integer) As GSOCertificateDataSet
        'Note:
        'p_intTireTypeId is ignored for now, but kept in place for future use.

        Dim dstResults As New GSOCertificateDataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "reports_package.GetGSOPassengerReport"

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand("pc_Certificate", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Brand", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_SkuList out retCursor,
            ParametersHelper.AddParametersToCommand("pc_SkuList", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand("pc_CertificateDfValue", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_MeasureHDR out retcursor,
            ParametersHelper.AddParametersToCommand("pc_MeasureHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_PlungerHDR out retcursor,
            ParametersHelper.AddParametersToCommand("pc_PlungerHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_beadunseathdr out retcursor,
            ParametersHelper.AddParametersToCommand("pc_beadunseathdr", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_treadwearhdr out retcursor,
            ParametersHelper.AddParametersToCommand("pc_treadwearhdr", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_endurance out retcursor,
            ParametersHelper.AddParametersToCommand("pc_endurance", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_highspeedhdr out retcursor,
            ParametersHelper.AddParametersToCommand("pc_highspeedhdr", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand("ps_certificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand("ps_extension", ParameterDirection.Input, OracleType.VarChar, p_strExtension, oraCmd)
            'pi_certificationTypeID in number,
            ParametersHelper.AddParametersToCommand("pi_certificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand("ps_Operatorid", ParameterDirection.Input, OracleType.VarChar, "ICSDEV", oraCmd)
            'pi_TireTypeID in number
            ParametersHelper.AddParametersToCommand("pi_TireTypeID", ParameterDirection.Input, OracleType.Number, p_intTireTypeId, oraCmd)
            'TODO:GEt the user

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add("Table", "CERTIFICATE")
            oraAdp.TableMappings.Add("Table1", "BRAND_VIEW")
            oraAdp.TableMappings.Add("Table2", "SKULIST_VIEW")
            oraAdp.TableMappings.Add("Table3", "PRODUCTDATA_REPORT_VIEW")
            oraAdp.TableMappings.Add("Table4", "DEFAULTVALUES_VIEW")
            oraAdp.TableMappings.Add("Table5", "MEASUREHDR")
            oraAdp.TableMappings.Add("Table6", "PLUNGERHDR")
            oraAdp.TableMappings.Add("Table7", "BEADUNSEATHDR")
            oraAdp.TableMappings.Add("Table8", "TREADWEARHDR")
            oraAdp.TableMappings.Add("Table9", "ENDURANCEHDR")
            oraAdp.TableMappings.Add("Table10", "HIGHSPEEDHDR")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    Public Function GetDataForGSOConformityReport(ByVal p_strBatchNumber As String, _
                                                ByVal p_intCertificationTypeId As Integer, _
                                                ByVal p_intTireTypeId As Integer) As DataSet
        'Note:
        'p_intTireTypeId is ignored for now, but kept in place for future use.

        Dim dstResults As New DataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "reports_package.GetGSOConformityReport"



            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand("pc_Certificate", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Brand", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_SkuList out retCursor,
            ParametersHelper.AddParametersToCommand("pc_SkuList", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand("ps_BatchNumber", ParameterDirection.Input, OracleType.VarChar, p_strBatchNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand("pi_certificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand("ps_Operatorid", ParameterDirection.Input, OracleType.VarChar, "ICSDEV", oraCmd)
            'pi_TireTypeID in number
            ParametersHelper.AddParametersToCommand("pi_TireTypeID", ParameterDirection.Input, OracleType.Number, p_intTireTypeId, oraCmd)


            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add("Table", "CERTIFICATE")
            oraAdp.TableMappings.Add("Table1", "BRAND_VIEW")
            oraAdp.TableMappings.Add("Table2", "SKULIST_VIEW")


            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the data for CCC report.
    ''' </summary>
    ''' <param name="p_strCertificateNumber"></param>
    ''' <param name="p_intCertificationTypeId"></param>
    ''' <param name="p_strExtension"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataForCCCReport(ByVal p_strCertificateNumber As String, _
                                                   ByVal p_intCertificationTypeId As Integer, _
                                                   ByVal p_strExtension As String) As CCCSequentialDataSet

        Dim dstResults As New CCCSequentialDataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "reports_package.GetCCCSequentialReportInfo"

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand("pc_Certificate", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Brand", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand("pc_CertificateDfValue", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand("ps_certificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand("ps_extension", ParameterDirection.Input, OracleType.VarChar, p_strExtension, oraCmd)
            'pi_certificationTypeID in number,
            ParametersHelper.AddParametersToCommand("pi_certificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand("ps_Operatorid", ParameterDirection.Input, OracleType.VarChar, "ICSDEV", oraCmd)
            'TODO:GEt the user

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add("Table", "CERTIFICATE")
            oraAdp.TableMappings.Add("Table1", "BRAND_VIEW")
            oraAdp.TableMappings.Add("Table2", "PRODUCTDATA_REPORT_VIEW")
            oraAdp.TableMappings.Add("Table3", "DEFAULTVALUES_VIEW")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the data for CCC Product Description report.
    ''' </summary>
    ''' <param name="p_strCertificateNumber"></param>
    ''' <param name="p_intCertificationTypeId"></param>
    ''' <param name="p_strExtension"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataForCCCProductDescriptionReport(ByVal p_strCertificateNumber As String, _
                                                   ByVal p_intCertificationTypeId As Integer, _
                                                   ByVal p_strExtension As String) As CCCProductDescriptionDataSet

        Dim dstResults As New CCCProductDescriptionDataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "reports_package.GetCCCProductDescReportInfo"

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand("pc_Certificate", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Brand", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand("pc_CertificateDfValue", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand("ps_certificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand("ps_extension", ParameterDirection.Input, OracleType.VarChar, p_strExtension, oraCmd)
            'pi_certificationTypeID in number,
            ParametersHelper.AddParametersToCommand("pi_certificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand("ps_Operatorid", ParameterDirection.Input, OracleType.VarChar, "ICSDEV", oraCmd)
            'TODO:GEt the user

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add("Table", "CERTIFICATE")
            oraAdp.TableMappings.Add("Table1", "BRAND_VIEW")
            oraAdp.TableMappings.Add("Table2", "CCCPRODUCTDESC_REPORT_VIEW")
            oraAdp.TableMappings.Add("Table3", "DEFAULTVALUES_VIEW")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the data for emark passenger report.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDataForIMARKConformityReport() As DataSet

        Dim dstResults As New DataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "reports_package.GetIMarkReportPassengerInfo"

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand("pc_Certificate", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Brand", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand("pc_CertificateDfValue", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_ProdBrandList out retcursor
            ParametersHelper.AddParametersToCommand("pc_ProdBrandList", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add("Table", "CERTIFICATE")
            oraAdp.TableMappings.Add("Table1", "BRAND_VIEW")
            oraAdp.TableMappings.Add("Table2", "PRODUCTDATA_REPORT_VIEW")
            oraAdp.TableMappings.Add("Table3", "DEFAULTVALUES_VIEW")
            oraAdp.TableMappings.Add("Table4", "PRODBRANDLIST_VIEW")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Get data for imark sampling tire tests report
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataForImarkSamplingTireTestsReport(ByVal p_str_MatlNum As String) As DataSet

        Dim dstResults As New DataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "reports_package.GetImarkSamplingAndTestsInfo"

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand("pc_Certificate", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_MeasureHdr out retCursor,
            ParametersHelper.AddParametersToCommand("pc_MeasureHdr", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_MeasureDtl out retCursor,
            ParametersHelper.AddParametersToCommand("pc_MeasureDtl", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_TreadWearHdr out retCursor,
            ParametersHelper.AddParametersToCommand("pc_TreadWearHdr", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_TreadWearDtl out retCursor,
            ParametersHelper.AddParametersToCommand("pc_TreadWearDtl", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_HighSpeedHdr out retCursor,
            ParametersHelper.AddParametersToCommand("pc_HighSpeedHdr", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_HighSpeedDtl out retCursor,
            ParametersHelper.AddParametersToCommand("pc_HighSpeedDtl", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand("pc_CertificateDfValue", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_Matl_Num in varchar2,
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_str_MatlNum, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add("Table", "CERTIFICATE")
            oraAdp.TableMappings.Add("Table1", "PRODUCTDATA_REPORT_VIEW")
            oraAdp.TableMappings.Add("Table2", "MeasureHdr")
            oraAdp.TableMappings.Add("Table3", "MeasureDtl")
            oraAdp.TableMappings.Add("Table4", "TreadWearHdr")
            oraAdp.TableMappings.Add("Table5", "TreadWearDtl")
            oraAdp.TableMappings.Add("Table6", "HighSpeedHdr")
            oraAdp.TableMappings.Add("Table7", "HighSpeedDtl")
            oraAdp.TableMappings.Add("Table8", "DEFAULTVALUES_VIEW")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Get data for the Imark Certiffication Report
    ''' </summary>
    ''' <param name="p_dt_Date"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataForImarkCertification(ByVal p_dt_Date As DateTime) As ImarkCertificationDataSet

        Dim dstResults As New ImarkCertificationDataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "reports_package.GetImarkCertificationInfo"
            'pc_Product     out retCursor,
            ParametersHelper.AddParametersToCommand("pc_ImarkCertification", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pd_DateSearchCriteria", ParameterDirection.Input, OracleType.DateTime, p_dt_Date, oraCmd)
            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            oraAdp.TableMappings.Add("Table", "IMARKCERTIFICATE_VIEW")
            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults
    End Function

    ' Added ps_BrandLine parameter as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    Public Function GetSKUCertificateReportInfo(ByVal p_str_MatlNum As String, ByVal p_strBrand As String, ByVal p_strBrandLine As String, ByVal p_strCertType As String) As CertificateReport

        Dim dstResults As New CertificateReport

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "reports_package.GetCertificateReportInfoBySKU"

            'pc_Product     out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Certificate out retCursor,
            ParametersHelper.AddParametersToCommand("pc_Certificate", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_TESTREFERENCE out retCursor,
            ParametersHelper.AddParametersToCommand("PC_TESTREFERENCE", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_Matl_Num in varchar2,
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_str_MatlNum, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand("ps_Operatorid", ParameterDirection.Input, OracleType.VarChar, Nothing, oraCmd)
            'ps_Brand in varchar2
            ParametersHelper.AddParametersToCommand("ps_Brand", ParameterDirection.Input, OracleType.VarChar, p_strBrand, oraCmd)
            'ps_Brand_Line in varchar2
            ParametersHelper.AddParametersToCommand("ps_Brand_Line", ParameterDirection.Input, OracleType.VarChar, p_strBrandLine, oraCmd)
            'ps_CertType in varchar2
            ParametersHelper.AddParametersToCommand("ps_CertType", ParameterDirection.Input, OracleType.VarChar, p_strCertType, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            oraAdp.TableMappings.Add("Table", "PRODUCTDATA_REPORT_VIEW")
            oraAdp.TableMappings.Add("Table1", "CERTIFICATE_VIEW")
            oraAdp.TableMappings.Add("Table2", "TESTREFERENCE_VIEW")

            oraAdp.Fill(dstResults)

        Catch exp As Exception
            'EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Get data for the Emark Certiffication Report
    ''' </summary>
    ''' <returns>EmarkCertificationTypeDataset</returns>
    Public Function GetDataForEmarkCertificationReport(ByVal p_strCertificationNo, ByVal p_strBrand, ByVal p_strBrandLine) As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "reports_package.GetEmarkCertificationInfo"


            'ps_Brand in varchar2
            ParametersHelper.AddParametersToCommand("ps_Brand", ParameterDirection.Input, OracleType.VarChar, p_strBrand, oraCmd)

            'ps_Brand_Line in varchar2
            ParametersHelper.AddParametersToCommand("ps_Brand_Line", ParameterDirection.Input, OracleType.VarChar, p_strBrandLine, oraCmd)

            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand("ps_certificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificationNo, oraCmd)



            ParametersHelper.AddParametersToCommand("pc_EmarkCertification", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)


            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add("Table", "EMARKCERTIFICATIONREPORT_VIEW")
            oraAdp.TableMappings.Add("Table1", "PRODUCT")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the data for emark with TR.
    ''' </summary>
    ''' <param name="p_strCertificateNumber">The certificate number.</param>
    ''' <param name="p_srtTireTypeID">The tire type ID.</param>
    ''' <returns></returns>
    Public Function GetDataForEmarkWithTR(ByVal p_strCertificateNumber As String, ByVal p_srtTireTypeID As Short) As DataSet
        'Dim dstResults As New EmarkPassengerWithTR
        Dim dstResults As New DataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'ps_CertificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'pi_tiretypeid        in number,
            ParametersHelper.AddParametersToCommand("pi_tiretypeid", ParameterDirection.Input, OracleType.Number, p_srtTireTypeID, oraCmd)            '
            'pc_CertificateDfValue  out retCursor,
            ParametersHelper.AddParametersToCommand("pc_CertificateDfValue", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateInfo   out retCursor,
            ParametersHelper.AddParametersToCommand("pc_CertificateInfo", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product           out retcursor,
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_MeasureHDR        out retCursor,
            ParametersHelper.AddParametersToCommand("pc_MeasureHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_measureDtl        out retCursor,
            ParametersHelper.AddParametersToCommand("pc_measureDtl", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_BEADUNSEATHDR     out retCursor,
            ParametersHelper.AddParametersToCommand("pc_BEADUNSEATHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_BEADUNSEATDTL     out retCursor,
            ParametersHelper.AddParametersToCommand("pc_BEADUNSEATDTL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_PLUNGERHDR        out retCursor,
            ParametersHelper.AddParametersToCommand("pc_PLUNGERHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_PLUNGERDTL        out retCursor,
            ParametersHelper.AddParametersToCommand("pc_PLUNGERDTL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_TREADWEARHDR      out retCursor,
            ParametersHelper.AddParametersToCommand("pc_TREADWEARHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_TREADWEARDTL      out retCursor,
            ParametersHelper.AddParametersToCommand("pc_TREADWEARDTL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_ENDURANCEHDR      out retCursor,
            ParametersHelper.AddParametersToCommand("pc_ENDURANCEHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_ENDURANCEDTL      out retCursor,
            ParametersHelper.AddParametersToCommand("pc_ENDURANCEDTL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_HIGHSPEEDHDR      out retCursor,
            ParametersHelper.AddParametersToCommand("pc_HIGHSPEEDHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_HIGHSPEEDDTL      out retCursor,
            ParametersHelper.AddParametersToCommand("pc_HIGHSPEEDDTL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_SPEEDTESTDETAIL   out retCursor
            ParametersHelper.AddParametersToCommand("pc_SPEEDTESTDETAIL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand             out retCursor
            ParametersHelper.AddParametersToCommand("pc_Brand", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_Procs.REPORTS_PACKAGE.GetEmarkTestReportInfo"

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add("Table", "DEFAULTVALUES_VIEW")
            oraAdp.TableMappings.Add("Table1", "CERTIFICATE_VIEW")
            oraAdp.TableMappings.Add("Table2", "PRODUCTDATA_REPORT_VIEW")
            oraAdp.TableMappings.Add("Table3", "MEASUREHDR")
            oraAdp.TableMappings.Add("Table4", "MEASUREDTL")
            oraAdp.TableMappings.Add("Table5", "BEADUNSEATHDR")
            oraAdp.TableMappings.Add("Table6", "BEADUNSEATDTL")
            oraAdp.TableMappings.Add("Table7", "PLUNGERHDR")
            oraAdp.TableMappings.Add("Table8", "PLUNGERDTL")
            oraAdp.TableMappings.Add("Table9", "TREADWEARHDR")
            oraAdp.TableMappings.Add("Table10", "TREADWEARDTL")
            oraAdp.TableMappings.Add("Table11", "ENDURANCEHDR")
            oraAdp.TableMappings.Add("Table12", "ENDURANCEDTL")
            oraAdp.TableMappings.Add("Table13", "HIGHSPEEDHDR")
            oraAdp.TableMappings.Add("Table14", "HIGHSPEEDDTL")
            oraAdp.TableMappings.Add("Table15", "SPEEDTESTDETAIL")
            oraAdp.TableMappings.Add("Table16", "BRAND_VIEW")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the traceability report info.
    ''' </summary>
    ''' <param name="p_strCertificateNumber">The certificate number.</param>
    ''' <param name="p_intCertificationTypeID">The certification type ID.</param>
    ''' <param name="p_strIncludeArchived">Include Archived certificates.</param>
    ''' <returns></returns>
    Public Function GetTraceabilityReportInfo(ByVal p_strCertificateNumber As String, _
                                              ByVal p_intCertificationTypeID As Integer, _
                                              ByVal p_strIncludeArchived As String) As Traceability
        Dim dstResults As New Traceability
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            ParametersHelper.AddParametersToCommand("pc_Traceability", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_CertificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            ' pi_certificationTypeID in number
            ParametersHelper.AddParametersToCommand("pi_certificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeID, oraCmd)
            'p_strIncludeArchived in varchar2,
            ParametersHelper.AddParametersToCommand("ps_IncludeArchived", ParameterDirection.Input, OracleType.VarChar, p_strIncludeArchived, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_PROCS.REPORTS_PACKAGE.GetTraceabilityReportInfo"

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add("Table", "TRACEABILITY_VIEW")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the exception report info.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetExceptionReportInfo() As ExceptionReport_DataSet

        Dim dstResults As New ExceptionReport_DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ParametersHelper.AddParametersToCommand("pc_Exception", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "REPORTS_PACKAGE.GetExceptionReportInfo"

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add("Table", "EXCEPTIONREPORT")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    Public Function GetEmarkApplication(ByVal p_strCertificateNumber As String, ByVal p_intTireTypeID As Integer) As DataSet

        'Dim dstResults As New ExceptionReport_DataSet
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_tiretypeid", ParameterDirection.Input, OracleType.Number, p_intTireTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_CertificateDfValue", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_CertificateInfo", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_MeasureHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_HIGHSPEEDHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_Brand", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)


            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "REPORTS_PACKAGE.GetEmarkApplicationInfo"


            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add("Table", "DEFAULTVALUES_VIEW")
            oraAdp.TableMappings.Add("Table1", "CERTIFICATE_VIEW")
            oraAdp.TableMappings.Add("Table2", "PRODUCTDATA_REPORT_VIEW")
            oraAdp.TableMappings.Add("Table3", "MEASUREHDR")
            oraAdp.TableMappings.Add("Table4", "HIGHSPEEDHDR")
            oraAdp.TableMappings.Add("Table5", "BRAND_VIEW")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    Public Function GetNomCertification(ByVal p_strCertificateNumber As String, ByVal p_intTireTypeID As Integer) As DataSet

        'Dim dstResults As New ExceptionReport_DataSet
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_tiretypeid", ParameterDirection.Input, OracleType.Number, p_intTireTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_CertificateDfValue", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_CertificateInfo", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_Product", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_MeasureHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_measureDtl", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_BEADUNSEATHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_BEADUNSEATDTL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_PLUNGERHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_PLUNGERDTL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_TREADWEARHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_TREADWEARDTL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_ENDURANCEHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_ENDURANCEDTL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_HIGHSPEEDHDR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_HIGHSPEEDDTL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_SPEEDTESTDETAIL", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_Brand", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_Procs.REPORTS_PACKAGE.GetNOMCertification"

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add("Table", "DEFAULTVALUES_VIEW")
            oraAdp.TableMappings.Add("Table1", "CERTIFICATE")
            oraAdp.TableMappings.Add("Table2", "PRODUCTDATA")
            oraAdp.TableMappings.Add("Table3", "MEASUREHDR")
            oraAdp.TableMappings.Add("Table4", "MEASUREDTL")
            oraAdp.TableMappings.Add("Table5", "BEADUNSEATHDR")
            oraAdp.TableMappings.Add("Table6", "BEADUNSEATDTL")
            oraAdp.TableMappings.Add("Table7", "PLUNGERHDR")
            oraAdp.TableMappings.Add("Table8", "PLUNGERDTL")
            oraAdp.TableMappings.Add("Table9", "TREADWEARHDR")
            oraAdp.TableMappings.Add("Table10", "TREADWEARDTL")
            oraAdp.TableMappings.Add("Table11", "ENDURANCEHDR")
            oraAdp.TableMappings.Add("Table12", "ENDURANCEDTL")
            oraAdp.TableMappings.Add("Table13", "HIGHSPEEDHDR")
            oraAdp.TableMappings.Add("Table14", "HIGHSPEEDDTL")
            oraAdp.TableMappings.Add("Table15", "SPEEDTESTDETAIL")
            oraAdp.TableMappings.Add("Table16", "BRAND_VIEW")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    Public Function GetAuthenticityReport() As DataSet

        'Dim dstResults As New ExceptionReport_DataSet
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            ParametersHelper.AddParametersToCommand("pc_Authenticity", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "REPORTS_PACKAGE.GetAuthenticityReportInfo"

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add("Table", "CERTIFICATE")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    Public Function GetEmarkSimilarCertificateSearchReport(ByVal p_strMatlNum As String) As DataSet

        'Dim dstResults As New ExceptionReport_DataSet
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'ps_Matl_Num in varchar2,
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_SimilarCertificates", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)


            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "REPORTS_PACKAGE.getEceSimilarCertificates"

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add("Table", "SimilarCertificates")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    Public Function GetCertificationRenewal(ByVal p_strCertificateNumber As String, ByVal p_intCertificationTypeID As Integer) As DataSet

        'Dim dstResults As New ExceptionReport_DataSet
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'parametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ParametersHelper.AddParametersToCommand(" pi_tiretypeid", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand(" pc_CertificateInfo", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "REPORTS_PACKAGE.GetCertificateRenewal"

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add("Table", "CERTIFICATE")

            oraAdp.Fill(dstResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

End Class

