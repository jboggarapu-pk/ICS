Imports System.Data
Imports System.Data.OracleClient
Imports System.Configuration
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Datasets

''' <summary>
''' Data access layer file to handle the reports data info
''' </summary>
''' <remarks>
''' <list type="table">
''' <listheader>
''' <term>Author</term>
''' <description>Description</description>
''' </listheader>    
''' <item>
''' <term>NA</term>
''' <description>
''' <para>NA</para>
''' <para>Original Code.</para>
''' </description>
''' </item>
''' <item>
''' <term>Srinivas S</term>
''' <description>
''' <para>09/24/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class ReportsDalc
    Inherits CooperTire.ICS.CTS.DALC.CtsDalc

#Region "ReportsDALC Constants"

    ''' <summary>
    ''' Constant to hold string pc_Certificate.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcCertificate As String = "pc_Certificate"

    ''' <summary>
    ''' Constant to hold string pc_Brand.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcBrand As String = "pc_Brand"

    ''' <summary>
    ''' Constant to hold string pc_Product.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcProduct As String = "pc_Product"

    ''' <summary>
    ''' Constant to hold string pc_CertificateDfValue.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcCertificateDfValue As String = "pc_CertificateDfValue"

    ''' <summary>
    ''' Constant to hold string pc_skudetailscount.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcSkuDetailsCount As String = "pc_skudetailscount"

    ''' <summary>
    ''' Constant to hold string ps_certificateNumber.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PsCertificateNumber As String = "ps_certificateNumber"

    ''' <summary>
    ''' Constant to hold string ps_extension.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PsExtension As String = "ps_extension"

    ''' <summary>
    ''' Constant to hold string pi_certificationTypeID.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PiCertificationTypeId As String = "pi_certificationTypeID"

    ''' <summary>
    ''' Constant to hold string ps_Operatorid.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PsOperationId As String = "ps_Operatorid"

    ''' <summary>
    ''' Constant to hold string ICSDEV.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const IcsDev As String = "ICSDEV"

    ''' <summary>
    ''' Constant to hold string pi_TireTypeID.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PiTireTypeId As String = "pi_TireTypeID"

    ''' <summary>
    ''' Constant to hold string Table.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table As String = "Table"

    ''' <summary>
    ''' Constant to hold string Table1.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table1 As String = "Table1"

    ''' <summary>
    ''' Constant to hold string Table2.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table2 As String = "Table2"

    ''' <summary>
    ''' Constant to hold string Table3.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table3 As String = "Table3"

    ''' <summary>
    ''' Constant to hold string Table4.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table4 As String = "Table4"

    ''' <summary>
    ''' Constant to hold string Table5.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table5 As String = "Table5"

    ''' <summary>
    ''' Constant to hold string Table6.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table6 As String = "Table6"

    ''' <summary>
    ''' Constant to hold string Table7.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table7 As String = "Table7"

    ''' <summary>
    ''' Constant to hold string Table8.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table8 As String = "Table8"

    ''' <summary>
    ''' Constant to hold string Table9.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table9 As String = "Table9"

    ''' <summary>
    ''' Constant to hold string Table10.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table10 As String = "Table10"

    ''' <summary>
    ''' Constant to hold string Table11.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table11 As String = "Table11"

    ''' <summary>
    ''' Constant to hold string Table12.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table12 As String = "Table12"

    ''' <summary>
    ''' Constant to hold string Table13.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table13 As String = "Table13"

    ''' <summary>
    ''' Constant to hold string Table14.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table14 As String = "as_Matl_Num"

    ''' <summary>
    ''' Constant to hold string Table15.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table15 As String = "Table15"

    ''' <summary>
    ''' Constant to hold string Table16.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Table16 As String = "Table16"

    ''' <summary>
    ''' Constant to hold string CERTIFICATE.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const Certificate As String = "CERTIFICATE"

    ''' <summary>
    ''' Constant to hold string BRAND_VIEW.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const BrandView As String = "BRAND_VIEW"

    ''' <summary>
    ''' Constant to hold string PRODUCTDATA_REPORT_VIEW.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ProductionDataReportView As String = "PRODUCTDATA_REPORT_VIEW"

    ''' <summary>
    ''' Constant to hold string DEFAULTVALUES_VIEW.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DefaultValuesView As String = "DEFAULTVALUES_VIEW"

    ''' <summary>
    ''' Constant to hold string SKUDETAILSCOUNT.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SkuDetailsCount As String = "SKUDETAILSCOUNT"

    ''' <summary>
    ''' Constant to hold string pc_SkuList.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcSkuList As String = "pc_SkuList"

    ''' <summary>
    ''' Constant to hold string pc_MeasureHDR.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcMeasureHdr As String = "pc_MeasureHDR"

    ''' <summary>
    ''' Constant to hold string pc_PlungerHDR.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcPlungerHdr As String = "pc_PlungerHDR"

    ''' <summary>
    ''' Constant to hold string pc_beadunseathdr.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcBeadUnseathdr As String = "pc_beadunseathdr"

    ''' <summary>
    ''' Constant to hold string pc_treadwearhdr.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcTreadWearhdr As String = "pc_treadwearhdr"

    ''' <summary>
    ''' Constant to hold string pc_highspeedhdr.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcHighSpeedhdr As String = "pc_highspeedhdr"

    ''' <summary>
    ''' Constant to hold string SKULIST_VIEW.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SkuListView As String = "SKULIST_VIEW"

    ''' <summary>
    ''' Constant to hold string MEASUREHDR.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MeasureHdr As String = "MEASUREHDR"

    ''' <summary>
    ''' Constant to hold string PLUNGERHDR.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PlungerHdr As String = "PLUNGERHDR"

    ''' <summary>
    ''' Constant to hold string BEADUNSEATHDR.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const BeadUnseathdr As String = "BEADUNSEATHDR"

    ''' <summary>
    ''' Constant to hold string TREADWEARHDR.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TreadwearHdr As String = "TREADWEARHDR"

    ''' <summary>
    ''' Constant to hold string ENDURANCEHDR.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const EnduranceHdr As String = "ENDURANCEHDR"

    ''' <summary>
    ''' Constant to hold string HIGHSPEEDHDR.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const HighSpeedHdr As String = "HIGHSPEEDHDR"

    ''' <summary>
    ''' Constant to hold string pc_MeasureDtl.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcMeasureDtl As String = "pc_MeasureDtl"

    ''' <summary>
    ''' Constant to hold string pc_TreadWearDtl.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcTreadWearDtl As String = "pc_TreadWearDtl"

    ''' <summary>
    ''' Constant to hold string pc_HighSpeedDtl.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcHighSpeedDtl As String = "pc_HighSpeedDtl"

    ''' <summary>
    ''' Constant to hold string ps_Matl_Num.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PsMatlNum As String = "ps_Matl_Num"

    ''' <summary>
    ''' Constant to hold string MeasureDtl.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MeasureDtl As String = "MeasureDtl"

    ''' <summary>
    ''' Constant to hold string TreadWearDtl.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ThreadWearDtl As String = "TreadWearDtl"

    ''' <summary>
    ''' Constant to hold string HighSpeedDtl.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const HighSpeedDtl As String = "HighSpeedDtl"

    ''' <summary>
    ''' Constant to hold string CERTIFICATE_VIEW.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CertificateView As String = "CERTIFICATE_VIEW"

    ''' <summary>
    ''' Constant to hold string pc_CertificateInfo.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcCertificateInfo As String = "pc_CertificateInfo"

    ''' <summary>
    ''' Constant to hold string pc_BEADUNSEATDTL.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcBeadUnseatdtl As String = "pc_BEADUNSEATDTL"

    ''' <summary>
    ''' Constant to hold string pc_PLUNGERDTL.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcPlungerDtl As String = "pc_PLUNGERDTL"

    ''' <summary>
    ''' Constant to hold string pc_ENDURANCEHDR.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcEnduranceHdr As String = "pc_ENDURANCEHDR"

    ''' <summary>
    ''' Constant to hold string pc_ENDURANCEDTL.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcEnduranceDtl As String = "pc_ENDURANCEDTL"

    ''' <summary>
    ''' Constant to hold string pc_SPEEDTESTDETAIL.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PcSpeedTestDetail As String = "pc_SPEEDTESTDETAIL"

    ''' <summary>
    ''' Constant to hold string BEADUNSEATDTL.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const BeadUnSeatDtl As String = "BEADUNSEATDTL"

    ''' <summary>
    ''' Constant to hold string PLUNGERDTL.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PlungerDtl As String = "PLUNGERDTL"

    ''' <summary>
    ''' Constant to hold string ENDURANCEDTL.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const EnduranceDtl As String = "ENDURANCEDTL"

    ''' <summary>
    ''' Constant to hold string SPEEDTESTDETAIL.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SpeedTestDetail As String = "SPEEDTESTDETAIL"

#End Region

#Region "ReportsDalc class Methods"

    ' Modified methods as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ' Used Material Number instead of SKU, PSN instead of NPRId, TPN instead of PPN and Brand, Brand Line instead of Brand Code.
    ''' <summary>
    ''' Gets the data for emark passenger report.
    ''' </summary>
    ''' <param name="p_strCertificateNumber">The  certificate number.</param>
    ''' <param name="p_intCertificationTypeId">The  certification type id.</param>
    ''' <param name="p_strExtension">The  extension.</param>
    ''' <param name="p_intTireTypeId">Tire type Id.</param>
    ''' <returns>Data table returned with information about emark passenger report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForEmarkPassengerReport(ByVal p_strCertificateNumber As String, _
                                                   ByVal p_intCertificationTypeId As Integer, _
                                                   ByVal p_strExtension As String, _
                                                   ByVal p_intTireTypeId As Integer) As DataSet

        Const SpGetEMarkReportPassengerInfo As String = "reports_package.GetEmarkReportPassengerInfo"
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetEMarkReportPassengerInfo
            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand(PcCertificate, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand(PcBrand, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand(PcCertificateDfValue, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_skudetailscount, -- this is a cursor containing the number of distinct sets of sku values.
            ParametersHelper.AddParametersToCommand(PcSkuDetailsCount, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand(PsCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand(PsExtension, ParameterDirection.Input, OracleType.VarChar, p_strExtension, oraCmd)
            'pi_certificationTypeID in number,
            ParametersHelper.AddParametersToCommand(PiCertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand(PsOperationId, ParameterDirection.Input, OracleType.VarChar, IcsDev, oraCmd)
            'pi_TireTypeID in number
            ParametersHelper.AddParametersToCommand(PiTireTypeId, ParameterDirection.Input, OracleType.Number, p_intTireTypeId, oraCmd)

            'TODO:GEt the user

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd
            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add(Table, Certificate)
            oraAdp.TableMappings.Add(Table1, BrandView)
            oraAdp.TableMappings.Add(Table2, ProductionDataReportView)
            oraAdp.TableMappings.Add(Table3, DefaultValuesView)
            oraAdp.TableMappings.Add(Table4, SkuDetailsCount)
            oraAdp.Fill(dstResults)

        Catch expDataForEmarkPassengerReport As Exception
            EventLogger.Enter(expDataForEmarkPassengerReport)
            Throw expDataForEmarkPassengerReport
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
    ''' <returns>Data table returned with information about Emark E117 report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForEmarkE117Report(ByVal p_strCertificateNumber As String, _
                                                   ByVal p_intCertificationTypeId As Integer, _
                                                   ByVal p_strExtension As String, _
                                                   ByVal p_intTireTypeId As Integer) As DataSet

        Const SpGetEmark117Info As String = "reports_package.GetEmark117Info"
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetEmark117Info

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand(PcCertificate, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand(PcBrand, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand(PcCertificateDfValue, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_skudetailscount, -- this is a cursor containing the number of distinct sets of sku values.
            ParametersHelper.AddParametersToCommand(PcSkuDetailsCount, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand(PsCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand(PsExtension, ParameterDirection.Input, OracleType.VarChar, p_strExtension, oraCmd)
            'pi_certificationTypeID in number,
            ParametersHelper.AddParametersToCommand(PiCertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand(PsOperationId, ParameterDirection.Input, OracleType.VarChar, IcsDev, oraCmd)
            'pi_TireTypeID in number
            ParametersHelper.AddParametersToCommand(PiTireTypeId, ParameterDirection.Input, OracleType.Number, p_intTireTypeId, oraCmd)

            'TODO:GEt the user

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add(Table, Certificate)
            oraAdp.TableMappings.Add(Table1, BrandView)
            oraAdp.TableMappings.Add(Table2, ProductionDataReportView)
            oraAdp.TableMappings.Add(Table3, DefaultValuesView)
            oraAdp.TableMappings.Add(Table4, SkuDetailsCount)
            oraAdp.Fill(dstResults)
        Catch expDataForEmarkE117Report As Exception
            EventLogger.Enter(expDataForEmarkE117Report)
            Throw expDataForEmarkE117Report
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
    ''' <returns>Data table returned with information about  GSO passenger report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForGSOPassengerReport(ByVal p_strCertificateNumber As String, _
                                                   ByVal p_intCertificationTypeId As Integer, _
                                                   ByVal p_strExtension As String, _
                                                   ByVal p_intTireTypeId As Integer) As GSOCertificateDataSet
        'Note:
        'p_intTireTypeId is ignored for now, but kept in place for future use.

        Const SpGetGsoPassengerReport As String = "reports_package.GetGSOPassengerReport"
        Const PcEndurance As String = "pc_endurance"
        Dim dstResults As New GSOCertificateDataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetGsoPassengerReport

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand(PcCertificate, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand(PcBrand, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_SkuList out retCursor,
            ParametersHelper.AddParametersToCommand(PcSkuList, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand(PcCertificateDfValue, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_MeasureHDR out retcursor,
            ParametersHelper.AddParametersToCommand(PcMeasureHdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_PlungerHDR out retcursor,
            ParametersHelper.AddParametersToCommand(PcPlungerHdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_beadunseathdr out retcursor,
            ParametersHelper.AddParametersToCommand(PcBeadUnseathdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_treadwearhdr out retcursor,
            ParametersHelper.AddParametersToCommand(PcTreadWearhdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_endurance out retcursor,
            ParametersHelper.AddParametersToCommand(PcEndurance, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_highspeedhdr out retcursor,
            ParametersHelper.AddParametersToCommand(PcHighSpeedhdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand(PsCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand(PsExtension, ParameterDirection.Input, OracleType.VarChar, p_strExtension, oraCmd)
            'pi_certificationTypeID in number,
            ParametersHelper.AddParametersToCommand(PiCertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand(PsOperationId, ParameterDirection.Input, OracleType.VarChar, IcsDev, oraCmd)
            'pi_TireTypeID in number
            ParametersHelper.AddParametersToCommand(PiTireTypeId, ParameterDirection.Input, OracleType.Number, p_intTireTypeId, oraCmd)
            'TODO:GEt the user

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add(Table, Certificate)
            oraAdp.TableMappings.Add(Table1, BrandView)
            oraAdp.TableMappings.Add(Table2, SkuListView)
            oraAdp.TableMappings.Add(Table3, ProductionDataReportView)
            oraAdp.TableMappings.Add(Table4, DefaultValuesView)
            oraAdp.TableMappings.Add(Table5, MeasureHdr)
            oraAdp.TableMappings.Add(Table6, PlungerHdr)
            oraAdp.TableMappings.Add(Table7, BeadUnseathdr)
            oraAdp.TableMappings.Add(Table8, TreadwearHdr)
            oraAdp.TableMappings.Add(Table9, EnduranceHdr)
            oraAdp.TableMappings.Add(Table10, HighSpeedHdr)

            oraAdp.Fill(dstResults)
        Catch expDataForGSOPassengerReport As Exception
            EventLogger.Enter(expDataForGSOPassengerReport)
            Throw expDataForGSOPassengerReport
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function


    ''' <summary>
    ''' Gets the data for GSO Conformity report.
    ''' </summary>
    ''' <param name="p_strBatchNumber">The P_STR Batch number.</param>
    ''' <param name="p_intCertificationTypeId">The p_int certification type id.</param>
    ''' <param name="p_intTireTypeId">The tire type id. (ignored for now but kept in place for future use.)</param>
    ''' <returns>Data set returned with information about  GSO Conformity report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForGSOConformityReport(ByVal p_strBatchNumber As String, _
                                                ByVal p_intCertificationTypeId As Integer, _
                                                ByVal p_intTireTypeId As Integer) As DataSet
        'Note:
        'p_intTireTypeId is ignored for now, but kept in place for future use.
        Const SpGetGsoConformityReport As String = "reports_package.GetGSOConformityReport"
        Const PsBatchNumber As String = "ps_BatchNumber"
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetGsoConformityReport

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand(PcCertificate, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand(PcBrand, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_SkuList out retCursor,
            ParametersHelper.AddParametersToCommand(PcSkuList, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand(PsBatchNumber, ParameterDirection.Input, OracleType.VarChar, p_strBatchNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand(PiCertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand(PsOperationId, ParameterDirection.Input, OracleType.VarChar, IcsDev, oraCmd)
            'pi_TireTypeID in number
            ParametersHelper.AddParametersToCommand(PiTireTypeId, ParameterDirection.Input, OracleType.Number, p_intTireTypeId, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add(Table, Certificate)
            oraAdp.TableMappings.Add(Table1, BrandView)
            oraAdp.TableMappings.Add(Table2, SkuListView)
            oraAdp.Fill(dstResults)
        Catch expDataForGSOConformityReport As Exception
            EventLogger.Enter(expDataForGSOConformityReport)
            Throw expDataForGSOConformityReport
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
    ''' <param name="p_strCertificateNumber">The P_STR Certificate number.</param>
    ''' <param name="p_intCertificationTypeId">The p_int certification type id.</param>
    ''' <param name="p_strExtension">The Extention.</param>
    ''' <returns>Data set returned with information about CCC report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForCCCReport(ByVal p_strCertificateNumber As String, _
                                                   ByVal p_intCertificationTypeId As Integer, _
                                                   ByVal p_strExtension As String) As CCCSequentialDataSet

        Const SpGetCccSequentialReprtInfo As String = "reports_package.GetCCCSequentialReportInfo"
        Dim dstResults As New CCCSequentialDataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetCccSequentialReprtInfo

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand(PcCertificate, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand(PcBrand, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand(PcCertificateDfValue, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand(PsCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand(PsExtension, ParameterDirection.Input, OracleType.VarChar, p_strExtension, oraCmd)
            'pi_certificationTypeID in number,
            ParametersHelper.AddParametersToCommand(PiCertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand(PsOperationId, ParameterDirection.Input, OracleType.VarChar, IcsDev, oraCmd)
            'TODO:GEt the user

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add(Table, Certificate)
            oraAdp.TableMappings.Add(Table1, BrandView)
            oraAdp.TableMappings.Add(Table2, ProductionDataReportView)
            oraAdp.TableMappings.Add(Table3, DefaultValuesView)

            oraAdp.Fill(dstResults)
        Catch expDataForCCCReport As Exception
            EventLogger.Enter(expDataForCCCReport)
            Throw expDataForCCCReport
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
    ''' <param name="p_strCertificateNumber">Certificate number.</param>
    ''' <param name="p_intCertificationTypeId">certification type id.</param>
    ''' <param name="p_strExtension">Extention.</param>
    ''' <returns>Data set returned with information about CCC Product Description report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForCCCProductDescriptionReport(ByVal p_strCertificateNumber As String, _
                                                   ByVal p_intCertificationTypeId As Integer, _
                                                   ByVal p_strExtension As String) As CCCProductDescriptionDataSet

        Const SpGetCccProductDescReportInfo As String = "reports_package.GetCCCProductDescReportInfo"
        Const CccProductDescReportView As String = "CCCPRODUCTDESC_REPORT_VIEW"
        Dim dstResults As New CCCProductDescriptionDataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetCccProductDescReportInfo

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand(PcCertificate, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand(PcBrand, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand(PcCertificateDfValue, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand(PsCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_extension in varchar2,
            ParametersHelper.AddParametersToCommand(PsExtension, ParameterDirection.Input, OracleType.VarChar, p_strExtension, oraCmd)
            'pi_certificationTypeID in number,
            ParametersHelper.AddParametersToCommand(PiCertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand(PsOperationId, ParameterDirection.Input, OracleType.VarChar, IcsDev, oraCmd)
            'TODO:GEt the user

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add(Table, Certificate)
            oraAdp.TableMappings.Add(Table1, BrandView)
            oraAdp.TableMappings.Add(Table2, CccProductDescReportView)
            oraAdp.TableMappings.Add(Table3, DefaultValuesView)

            oraAdp.Fill(dstResults)
        Catch expCCCProductDescriptionReport As Exception
            EventLogger.Enter(expCCCProductDescriptionReport)
            Throw expCCCProductDescriptionReport
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function


    ''' <summary>
    ''' Gets the data for IMARK Conformity report.
    ''' </summary>  
    ''' <returns>Data set returned with information about IMARK Conformity report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForIMARKConformityReport() As DataSet

        Const SpGetImarkReportPassengerInfo As String = "reports_package.GetIMarkReportPassengerInfo"
        Const PcProdBrandList As String = "pc_ProdBrandList"
        Const ProdBrandListView As String = "PRODBRANDLIST_VIEW"
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetImarkReportPassengerInfo

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand(PcCertificate, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand out retCursor,
            ParametersHelper.AddParametersToCommand(PcBrand, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand(PcCertificateDfValue, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_ProdBrandList out retcursor
            ParametersHelper.AddParametersToCommand(PcProdBrandList, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add(Table, Certificate)
            oraAdp.TableMappings.Add(Table1, BrandView)
            oraAdp.TableMappings.Add(Table2, ProductionDataReportView)
            oraAdp.TableMappings.Add(Table3, DefaultValuesView)
            oraAdp.TableMappings.Add(Table4, ProdBrandListView)

            oraAdp.Fill(dstResults)
        Catch expIMARKConformityReport As Exception
            EventLogger.Enter(expIMARKConformityReport)
            Throw expIMARKConformityReport
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
    ''' <param name="p_str_MatlNum">Material number.</param>
    ''' <returns>Data set returned with information about imark sampling tire tests report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForImarkSamplingTireTestsReport(ByVal p_str_MatlNum As String) As DataSet

        Const SpGetImarkSamlingAndTestsInfo As String = "reports_package.GetImarkSamplingAndTestsInfo"
        Dim dstResults As New DataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetImarkSamlingAndTestsInfo

            'pc_Certificate out retCursor
            ParametersHelper.AddParametersToCommand(PcCertificate, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product out retCursor,
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_MeasureHdr out retCursor,
            ParametersHelper.AddParametersToCommand(PcMeasureHdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_MeasureDtl out retCursor,
            ParametersHelper.AddParametersToCommand(PcMeasureDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_TreadWearHdr out retCursor,
            ParametersHelper.AddParametersToCommand(PcTreadWearhdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_TreadWearDtl out retCursor,
            ParametersHelper.AddParametersToCommand(PcTreadWearDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_HighSpeedHdr out retCursor,
            ParametersHelper.AddParametersToCommand(PcHighSpeedhdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_HighSpeedDtl out retCursor,
            ParametersHelper.AddParametersToCommand(PcHighSpeedDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateDfValue out retcursor,
            ParametersHelper.AddParametersToCommand(PcCertificateDfValue, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_Matl_Num in varchar2,
            ParametersHelper.AddParametersToCommand(PsMatlNum, ParameterDirection.Input, OracleType.VarChar, p_str_MatlNum, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False

            oraAdp.TableMappings.Add(Table, Certificate)
            oraAdp.TableMappings.Add(Table1, ProductionDataReportView)
            oraAdp.TableMappings.Add(Table2, MeasureHdr)
            oraAdp.TableMappings.Add(Table3, MeasureDtl)
            oraAdp.TableMappings.Add(Table4, TreadwearHdr)
            oraAdp.TableMappings.Add(Table5, ThreadWearDtl)
            oraAdp.TableMappings.Add(Table6, HighSpeedHdr)
            oraAdp.TableMappings.Add(Table7, HighSpeedDtl)
            oraAdp.TableMappings.Add(Table8, DefaultValuesView)

            oraAdp.Fill(dstResults)
        Catch expImarkSamplingTireTestsReport As Exception
            EventLogger.Enter(expImarkSamplingTireTestsReport)
            Throw expImarkSamplingTireTestsReport
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
    ''' <param name="p_dt_Date">Date.</param>
    ''' <returns>Data set returned with information about Imark Certiffication report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForImarkCertification(ByVal p_dt_Date As DateTime) As ImarkCertificationDataSet

        Const SpGetIMarkCertificationInfo As String = "reports_package.GetImarkCertificationInfo"
        Const PcImarkCertification As String = "pc_ImarkCertification"
        Const PdDateSearchCriteria As String = "pd_DateSearchCriteria"
        Const IMarkCertificateView As String = "IMARKCERTIFICATE_VIEW"
        Dim dstResults As New ImarkCertificationDataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetIMarkCertificationInfo
            'pc_Product     out retCursor,
            ParametersHelper.AddParametersToCommand(PcImarkCertification, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PdDateSearchCriteria, ParameterDirection.Input, OracleType.DateTime, p_dt_Date, oraCmd)
            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            oraAdp.TableMappings.Add(Table, IMarkCertificateView)
            oraAdp.Fill(dstResults)
        Catch expImarkCertification As Exception
            EventLogger.Enter(expImarkCertification)
            Throw expImarkCertification
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults
    End Function

    ' Added ps_BrandLine parameter as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Get data for the SKU Certiffication Report
    ''' </summary>  
    ''' <param name="p_str_MatlNum">Material Number.</param>
    ''' <param name="p_strBrand">Brand</param>
    ''' <param name="p_strBrandLine">Brand Line.</param>
    ''' <param name="p_strCertType">Certificate type.</param>
    ''' <returns>Data set returned with information about SKU Certiffication report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetSKUCertificateReportInfo(ByVal p_str_MatlNum As String, ByVal p_strBrand As String, _
                                                ByVal p_strBrandLine As String, ByVal p_strCertType As String) As CertificateReport

        Const SpGetCertificateReportInfoBySKU As String = "reports_package.GetCertificateReportInfoBySKU"
        Const PcTestReference As String = "PC_TESTREFERENCE"
        Const PsBrand As String = "ps_Brand"
        Const PsBrandLine As String = "ps_Brand_Line"
        Const PsCertType As String = "ps_CertType"
        Const TestReferenceView As String = "TESTREFERENCE_VIEW"
        Dim dstResults As New CertificateReport

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetCertificateReportInfoBySKU

            'pc_Product     out retCursor,
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Certificate out retCursor,
            ParametersHelper.AddParametersToCommand(PcCertificate, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_TESTREFERENCE out retCursor,
            ParametersHelper.AddParametersToCommand(PcTestReference, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_Matl_Num in varchar2,
            ParametersHelper.AddParametersToCommand(PsMatlNum, ParameterDirection.Input, OracleType.VarChar, p_str_MatlNum, oraCmd)
            'ps_Operatorid in varchar2
            ParametersHelper.AddParametersToCommand(PsOperationId, ParameterDirection.Input, OracleType.VarChar, Nothing, oraCmd)
            'ps_Brand in varchar2
            ParametersHelper.AddParametersToCommand(PsBrand, ParameterDirection.Input, OracleType.VarChar, p_strBrand, oraCmd)
            'ps_Brand_Line in varchar2
            ParametersHelper.AddParametersToCommand(PsBrandLine, ParameterDirection.Input, OracleType.VarChar, p_strBrandLine, oraCmd)
            'ps_CertType in varchar2
            ParametersHelper.AddParametersToCommand(PsCertType, ParameterDirection.Input, OracleType.VarChar, p_strCertType, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd

            oraAdp.TableMappings.Add(Table, ProductionDataReportView)
            oraAdp.TableMappings.Add(Table1, CertificateView)
            oraAdp.TableMappings.Add(Table2, TestReferenceView)

            oraAdp.Fill(dstResults)

        Catch expSKUCertificateReport As Exception
            'EventLogger.Enter(exp)
            Throw expSKUCertificateReport
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Get data for the Emark Certification Report
    ''' </summary>  
    ''' <param name="p_strCertificationNo">Certification Number.</param>
    ''' <param name="p_strBrand">Brand</param>
    ''' <param name="p_strBrandLine">Brand Line.</param>
    ''' <returns>Data set returned with information about Emark Certification report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>''' 
    ''' </list>
    ''' </remarks>
    Public Function GetDataForEmarkCertificationReport(ByVal p_strCertificationNo As String, ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet

        Const SpGetEmarkCertificationInfo As String = "reports_package.GetEmarkCertificationInfo"
        Const PsBrand As String = "ps_Brand"
        Const PsBrandLine As String = "ps_Brand_Line"
        Const PcEmarkCertification As String = "pc_EmarkCertification"
        Const EmarkCertificationReportView As String = "EMARKCERTIFICATIONREPORT_VIEW"
        Const ProductText As String = "PRODUCT"
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetEmarkCertificationInfo


            'ps_Brand in varchar2
            ParametersHelper.AddParametersToCommand(PsBrand, ParameterDirection.Input, OracleType.VarChar, p_strBrand, oraCmd)

            'ps_Brand_Line in varchar2
            ParametersHelper.AddParametersToCommand(PsBrandLine, ParameterDirection.Input, OracleType.VarChar, p_strBrandLine, oraCmd)

            'ps_certificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand(PsCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificationNo, oraCmd)
            ParametersHelper.AddParametersToCommand(PcEmarkCertification, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add(Table, EmarkCertificationReportView)
            oraAdp.TableMappings.Add(Table1, ProductText)

            oraAdp.Fill(dstResults)
        Catch expEmarkCertificationReport As Exception
            EventLogger.Enter(expEmarkCertificationReport)
            Throw expEmarkCertificationReport
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
    ''' <param name="p_strCertificateNumber">Certification Number.</param>
    ''' <param name="p_srtTireTypeID">Tire Type Id</param>
    ''' <returns>Data set returned with information about emark with TR.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForEmarkWithTR(ByVal p_strCertificateNumber As String, ByVal p_srtTireTypeID As Short) As DataSet
        'Dim dstResults As New EmarkPassengerWithTR

        Const SpGetEmarkTestReportInfo As String = "ICS_Procs.REPORTS_PACKAGE.GetEmarkTestReportInfo"
        Dim dstResults As New DataSet

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'ps_CertificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand(PsCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'pi_tiretypeid        in number,
            ParametersHelper.AddParametersToCommand(PiTireTypeId, ParameterDirection.Input, OracleType.Number, p_srtTireTypeID, oraCmd)            '
            'pc_CertificateDfValue  out retCursor,
            ParametersHelper.AddParametersToCommand(PcCertificateDfValue, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_CertificateInfo   out retCursor,
            ParametersHelper.AddParametersToCommand(PcCertificateInfo, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Product           out retcursor,
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_MeasureHDR        out retCursor,
            ParametersHelper.AddParametersToCommand(PcMeasureHdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_measureDtl        out retCursor,
            ParametersHelper.AddParametersToCommand(PcMeasureDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_BEADUNSEATHDR     out retCursor,
            ParametersHelper.AddParametersToCommand(PcBeadUnseathdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_BEADUNSEATDTL     out retCursor,
            ParametersHelper.AddParametersToCommand(PcBeadUnseatdtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_PLUNGERHDR        out retCursor,
            ParametersHelper.AddParametersToCommand(PcPlungerHdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_PLUNGERDTL        out retCursor,
            ParametersHelper.AddParametersToCommand(PcPlungerDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_TREADWEARHDR      out retCursor,
            ParametersHelper.AddParametersToCommand(PcTreadWearhdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_TREADWEARDTL      out retCursor,
            ParametersHelper.AddParametersToCommand(PcTreadWearDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_ENDURANCEHDR      out retCursor,
            ParametersHelper.AddParametersToCommand(PcEnduranceHdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_ENDURANCEDTL      out retCursor,
            ParametersHelper.AddParametersToCommand(PcEnduranceDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_HIGHSPEEDHDR      out retCursor,
            ParametersHelper.AddParametersToCommand(PcHighSpeedhdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_HIGHSPEEDDTL      out retCursor,
            ParametersHelper.AddParametersToCommand(PcHighSpeedDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_SPEEDTESTDETAIL   out retCursor
            ParametersHelper.AddParametersToCommand(PcSpeedTestDetail, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_Brand             out retCursor
            ParametersHelper.AddParametersToCommand(PcBrand, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetEmarkTestReportInfo

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add(Table, DefaultValuesView)
            oraAdp.TableMappings.Add(Table1, CertificateView)
            oraAdp.TableMappings.Add(Table2, ProductionDataReportView)
            oraAdp.TableMappings.Add(Table3, MeasureHdr)
            oraAdp.TableMappings.Add(Table4, MeasureDtl)
            oraAdp.TableMappings.Add(Table5, BeadUnseathdr)
            oraAdp.TableMappings.Add(Table6, BeadUnSeatDtl)
            oraAdp.TableMappings.Add(Table7, PlungerHdr)
            oraAdp.TableMappings.Add(Table8, PlungerDtl)
            oraAdp.TableMappings.Add(Table9, TreadwearHdr)
            oraAdp.TableMappings.Add(Table10, ThreadWearDtl)
            oraAdp.TableMappings.Add(Table11, EnduranceHdr)
            oraAdp.TableMappings.Add(Table12, EnduranceDtl)
            oraAdp.TableMappings.Add(Table13, HighSpeedHdr)
            oraAdp.TableMappings.Add(Table14, HighSpeedDtl)
            oraAdp.TableMappings.Add(Table15, SpeedTestDetail)
            oraAdp.TableMappings.Add(Table16, BrandView)

            oraAdp.Fill(dstResults)
        Catch expEmarkWithTR As Exception
            EventLogger.Enter(expEmarkWithTR)
            Throw expEmarkWithTR
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
    ''' <returns>Data table of type Traceability returned with information about traceability report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetTraceabilityReportInfo(ByVal p_strCertificateNumber As String, _
                                              ByVal p_intCertificationTypeID As Integer, _
                                              ByVal p_strIncludeArchived As String) As Traceability

        Const SpGetTrceabilityReportInfo As String = "ICS_PROCS.REPORTS_PACKAGE.GetTraceabilityReportInfo"
        Const TraceabilityView As String = "TRACEABILITY_VIEW"
        Const PcTraceability As String = "pc_Traceability"
        Const PsIncludeArchived As String = "ps_IncludeArchived"
        Dim dstResults As New Traceability
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            ParametersHelper.AddParametersToCommand(PcTraceability, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_CertificateNumber in varchar2,
            ParametersHelper.AddParametersToCommand(PsCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            ' pi_certificationTypeID in number
            ParametersHelper.AddParametersToCommand(PiCertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeID, oraCmd)
            'p_strIncludeArchived in varchar2,
            ParametersHelper.AddParametersToCommand(PsIncludeArchived, ParameterDirection.Input, OracleType.VarChar, p_strIncludeArchived, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetTrceabilityReportInfo

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add(Table, TraceabilityView)

            oraAdp.Fill(dstResults)
        Catch expTraceabilityReport As Exception
            EventLogger.Enter(expTraceabilityReport)
            Throw expTraceabilityReport
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
    ''' <returns>Data table of type ExceptionReport_DataSet returned with information about exception report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetExceptionReportInfo() As ExceptionReport_DataSet

        Const SpGetExceptionReportInfo As String = "REPORTS_PACKAGE.GetExceptionReportInfo"
        Const PcException As String = "pc_Exception"
        Const ExceptionReport As String = "EXCEPTIONREPORT"
        Dim dstResults As New ExceptionReport_DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ParametersHelper.AddParametersToCommand(PcException, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetExceptionReportInfo

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add(Table, ExceptionReport)

            oraAdp.Fill(dstResults)
        Catch expExceptionReport As Exception
            EventLogger.Enter(expExceptionReport)
            Throw expExceptionReport
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the Emark Application info
    ''' </summary>  
    ''' <param name="p_strCertificateNumber">The certificate number.</param>
    ''' <param name="p_intTireTypeID">The certification type ID.</param>
    ''' <returns>Data set returned with information about Emark Application.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetEmarkApplication(ByVal p_strCertificateNumber As String, ByVal p_intTireTypeID As Integer) As DataSet

        'Dim dstResults As New ExceptionReport_DataSet
        Const SpGetEmarkApplicationInfo As String = "REPORTS_PACKAGE.GetEmarkApplicationInfo"
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ParametersHelper.AddParametersToCommand(PsCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(PiTireTypeId, ParameterDirection.Input, OracleType.Number, p_intTireTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand(PcCertificateDfValue, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcCertificateInfo, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcMeasureHdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcHighSpeedhdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcBrand, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetEmarkApplicationInfo

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add(Table, DefaultValuesView)
            oraAdp.TableMappings.Add(Table1, CertificateView)
            oraAdp.TableMappings.Add(Table2, ProductionDataReportView)
            oraAdp.TableMappings.Add(Table3, MeasureHdr)
            oraAdp.TableMappings.Add(Table4, HighSpeedHdr)
            oraAdp.TableMappings.Add(Table5, BrandView)

            oraAdp.Fill(dstResults)
        Catch expEmarkApplication As Exception
            EventLogger.Enter(expEmarkApplication)
            Throw expEmarkApplication
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the Nom Application info
    ''' </summary>  
    ''' <param name="p_strCertificateNumber">The certificate number.</param>
    ''' <param name="p_intTireTypeID">The certification type ID.</param>
    ''' <returns>Data set returned with information about Nom Certification.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetNomCertification(ByVal p_strCertificateNumber As String, ByVal p_intTireTypeID As Integer) As DataSet

        'Dim dstResults As New ExceptionReport_DataSet
        Const SpGetNomCerification As String = "ICS_Procs.REPORTS_PACKAGE.GetNOMCertification"
        Const ProductData As String = "PRODUCTDATA"
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ParametersHelper.AddParametersToCommand(PsCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(PiTireTypeId, ParameterDirection.Input, OracleType.Number, p_intTireTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand(PcCertificateDfValue, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcCertificateInfo, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcProduct, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcMeasureHdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcMeasureDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcBeadUnseathdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcBeadUnseatdtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcPlungerHdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcPlungerDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcTreadWearhdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcTreadWearDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcEnduranceHdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcEnduranceDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcHighSpeedhdr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcHighSpeedDtl, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcSpeedTestDetail, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PcBrand, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetNomCerification

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add(Table, DefaultValuesView)
            oraAdp.TableMappings.Add(Table1, Certificate)
            oraAdp.TableMappings.Add(Table2, ProductData)
            oraAdp.TableMappings.Add(Table3, MeasureHdr)
            oraAdp.TableMappings.Add(Table4, MeasureDtl)
            oraAdp.TableMappings.Add(Table5, BeadUnseathdr)
            oraAdp.TableMappings.Add(Table6, BeadUnSeatDtl)
            oraAdp.TableMappings.Add(Table7, PlungerHdr)
            oraAdp.TableMappings.Add(Table8, PlungerDtl)
            oraAdp.TableMappings.Add(Table9, TreadwearHdr)
            oraAdp.TableMappings.Add(Table10, ThreadWearDtl)
            oraAdp.TableMappings.Add(Table11, EnduranceHdr)
            oraAdp.TableMappings.Add(Table12, EnduranceDtl)
            oraAdp.TableMappings.Add(Table13, HighSpeedHdr)
            oraAdp.TableMappings.Add(Table14, HighSpeedDtl)
            oraAdp.TableMappings.Add(Table15, SpeedTestDetail)
            oraAdp.TableMappings.Add(Table16, BrandView)

            oraAdp.Fill(dstResults)
        Catch expNomCertification As Exception
            EventLogger.Enter(expNomCertification)
            Throw expNomCertification
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the Emark Authenticity report
    ''' </summary>
    ''' <returns>Data set returned with information about Authenticity report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetAuthenticityReport() As DataSet

        'Dim dstResults As New ExceptionReport_DataSet
        Const SpGetAuthenticityReportInfo As String = "REPORTS_PACKAGE.GetAuthenticityReportInfo"
        Const PcAuthenticity As String = "pc_Authenticity"
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            ParametersHelper.AddParametersToCommand(PcAuthenticity, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetAuthenticityReportInfo

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add(Table, Certificate)

            oraAdp.Fill(dstResults)
        Catch expAuthenticityReport As Exception
            EventLogger.Enter(expAuthenticityReport)
            Throw expAuthenticityReport
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the EmarkSimilarCertificateSearch Report info
    ''' </summary>  
    ''' <param name="p_strMatlNum">Material Number.</param>
    ''' <returns>Data set returned with information abou EmarkSimilarCertificateSearch Report.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetEmarkSimilarCertificateSearchReport(ByVal p_strMatlNum As String) As DataSet

        'Dim dstResults As New ExceptionReport_DataSet
        Const SpGetEceSimilarCertificates As String = "REPORTS_PACKAGE.getEceSimilarCertificates"
        Const PcSimilarCertificates As String = "pc_SimilarCertificates"
        Const SimilarCertificates As String = "SimilarCertificates"
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'ps_Matl_Num in varchar2,
            ParametersHelper.AddParametersToCommand(PsMatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(PcSimilarCertificates, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetEceSimilarCertificates

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add(Table, SimilarCertificates)

            oraAdp.Fill(dstResults)
        Catch expCertificateSearchReport As Exception
            EventLogger.Enter(expCertificateSearchReport)
            Throw expCertificateSearchReport
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the Certification Renewal info
    ''' </summary>  
    ''' <param name="p_strCertificateNumber">Certification Number.</param>
    ''' <param name="p_intCertificationTypeID">Certification type Id.</param>
    ''' <returns>Data set returned with information about Certification Renewal.</returns>
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
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertificationRenewal(ByVal p_strCertificateNumber As String, ByVal p_intCertificationTypeID As Integer) As DataSet

        'Dim dstResults As New ExceptionReport_DataSet
        Const SpGetCertificateInfo As String = "REPORTS_PACKAGE.GetCertificateRenewal"
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'parametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ParametersHelper.AddParametersToCommand(" pi_tiretypeid", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand(PcCertificateInfo, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpGetCertificateInfo

            Connect()
            oraCmd.Connection = Connection
            oraAdp.SelectCommand = oraCmd

            dstResults.EnforceConstraints = False
            oraAdp.TableMappings.Add(Table, Certificate)

            oraAdp.Fill(dstResults)
        Catch expCertificationRenewal As Exception
            EventLogger.Enter(expCertificationRenewal)
            Throw expCertificationRenewal
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function
#End Region

End Class

