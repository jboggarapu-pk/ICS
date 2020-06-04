Imports System.Data
Imports System.Data.OracleClient
Imports System.Configuration
Imports System.Text

Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Datasets

''' <summary>
''' Certification specific data access methods
''' </summary>
''' <remarks>
''' </remarks>
Public Class CertificationDalc
    Inherits CooperTire.ICS.CTS.DALC.CtsDalc

    ' Modified as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ' Used Material Number instead of SKU, PSN instead of NPRId, TPN instead of PPN and Brand, Brand Line instead of Brand Code.
    ' Added Operation as paramter for HDR save methods, also added methods to retrieve data from web service.

#Region "Variables"
    Private errCode As Integer = 0
    Private errMessage As String = String.Empty
#End Region

#Region "Methods"

    ''' <summary>
    ''' Gets the search type results.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSearchTypeResults() As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_CRUD.GetSearchTypes"

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
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
    ''' Gets the manufacturing locations results.
    ''' </summary>
    ''' <param name="p_strSize"></param>
    ''' <returns></returns>
    Public Function GetManufacturingLocationsResults(ByVal p_strSize As String) As DataSet

        Dim dstResults As New DataSet           'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Try

            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_CRUD.GetManufacturingLocs"

            ''add the parameters
            'oraCmd.Parameters.Add(oraResults)

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
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
    ''' Get the grid data source for the Query user control
    ''' </summary>
    ''' <returns>dtbGridSource</returns>
    Public Function GetQueryControlGridSource() As DataTable
        Dim dstGridSource As New DataSet
        Dim dtbGridSource As New DataTable
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_CRUD.GET_QUERY_SOURCE"

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
            oraAdp.Fill(dstGridSource)

            dtbGridSource = dstGridSource.Tables(0)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dtbGridSource

    End Function

    ''' <summary>
    ''' Gets the manufacturing locations results.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCompanyNameList() As DataSet

        Dim dstResults As New DataSet           'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Try

            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_CRUD.GetCompanyNames"

            ''add the parameters
            'oraCmd.Parameters.Add(oraResults)

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
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
    ''' Get all Certification names
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCertifications() As DataSet

        Dim dstResults As New DataSet           'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            Dim oraResults As New OracleParameter("pc_retCursor", OracleType.Cursor)
            oraResults.Direction = ParameterDirection.Output
            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_CRUD.GetCertifications"
            ' Add the parameters
            oraCmd.Parameters.Add(oraResults)
            Connect()
            oraCmd.Connection = Connection
            ' Get the data
            oraAdp.SelectCommand = oraCmd
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
    ''' Get all Regions data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllRegions() As DataSet

        Dim dstResults As New DataSet           'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Dim oraResults As New OracleParameter("pc_retCursor", OracleType.Cursor)
        oraResults.Direction = ParameterDirection.Output

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = "ICS_CRUD.GetRegions"

        ' Add the parameters
        oraCmd.Parameters.Add(oraResults)

        Try
            Connect()
            oraCmd.Connection = Connection

            ' Get the data
            oraAdp.SelectCommand = oraCmd
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
    ''' <summary>
    ''' Get all Regions data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRegionCertStatus(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Dim oraBRANDPRODUCT As New OracleParameter()
        oraBRANDPRODUCT.ParameterName = "PC_BRANDPRODUCT"
        oraBRANDPRODUCT.OracleType = OracleType.Cursor
        oraBRANDPRODUCT.Direction = ParameterDirection.Output

        Dim oraREGIONSCERTIFIED As New OracleParameter()
        oraREGIONSCERTIFIED.ParameterName = "PC_REGIONSCERTIFIED"
        oraREGIONSCERTIFIED.OracleType = OracleType.Cursor
        oraREGIONSCERTIFIED.Direction = ParameterDirection.Output

        Dim oraEGIONSNOTCERTIFIED As New OracleParameter()
        oraEGIONSNOTCERTIFIED.ParameterName = "PC_REGIONNOTCERTIFIED"
        oraEGIONSNOTCERTIFIED.OracleType = OracleType.Cursor
        oraEGIONSNOTCERTIFIED.Direction = ParameterDirection.Output

        ParametersHelper.AddParametersToCommand("ps_Brand", ParameterDirection.Input, OracleType.VarChar, p_strBrand, oraCmd)
        ParametersHelper.AddParametersToCommand("ps_Brand_Line", ParameterDirection.Input, OracleType.VarChar, p_strBrandLine, oraCmd)

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = "ICS_CRUD.SEARCHBRAND"

        ' Add the parameters
        oraCmd.Parameters.Add(oraBRANDPRODUCT)
        oraCmd.Parameters.Add(oraREGIONSCERTIFIED)
        oraCmd.Parameters.Add(oraEGIONSNOTCERTIFIED)

        Try
            Connect()
            oraCmd.Connection = Connection

            ' Get the data
            oraAdp.SelectCommand = oraCmd
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

    Public Function GetProductCertStatus(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet
        'jeseitz 5/25/2016 - used in new marketing screen to retreive certification requests for products


        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Dim oraBRANDPRODUCT As New OracleParameter()
        oraBRANDPRODUCT.ParameterName = "PC_BRANDPRODUCT"
        oraBRANDPRODUCT.OracleType = OracleType.Cursor
        oraBRANDPRODUCT.Direction = ParameterDirection.Output

  
        ParametersHelper.AddParametersToCommand("ps_Brand", ParameterDirection.Input, OracleType.VarChar, p_strBrand, oraCmd)
        ParametersHelper.AddParametersToCommand("ps_Brand_Line", ParameterDirection.Input, OracleType.VarChar, p_strBrandLine, oraCmd)

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = "ICS_CRUD.SEARCHBRANDREQUESTS"

        ' Add the parameters
        oraCmd.Parameters.Add(oraBRANDPRODUCT)

        Try
            Connect()
            oraCmd.Connection = Connection

            ' Get the data
            oraAdp.SelectCommand = oraCmd
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
    ''' Saves the certification request.
    ''' </summary>
    ''' <param name="p_DeleteMe">if set to <c>true</c> [p_ delete me].</param>
    ''' <param name="p_strMatlNum">SAP Material</param>
    ''' <param name="p_intCountryID">The country ID that certification is being requested.</param>
    ''' <param name="p_intSKUID">The SKU ID that certification is being requested.</param>
    Public Function SaveCertificationRequest(ByVal p_DeleteMe As Boolean, ByVal p_strMatlNum As String, _
                                        ByVal p_intCountryID As Integer, ByVal p_intSKUID As Integer) As Boolean

        Dim oraCmd As New OracleCommand
        Dim blnSaved As Boolean = False

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_CRUD.CreateOrDeleteProductCountry"

            Dim opDeleteMe As New OracleParameter()
            opDeleteMe.ParameterName = "ps_DeleteMe"
            opDeleteMe.Direction = ParameterDirection.Input
            opDeleteMe.OracleType = OracleType.Char
            If p_DeleteMe Then
                opDeleteMe.Value = "y"
            Else
                opDeleteMe.Value = "n"
            End If
            opDeleteMe.Size = 1
            oraCmd.Parameters.Add(opDeleteMe)

            Dim opMatlNum As New OracleParameter()
            opMatlNum.ParameterName = "ps_Matl_Num"
            opMatlNum.Direction = ParameterDirection.Input
            opMatlNum.OracleType = OracleType.VarChar
            opMatlNum.Value = p_strMatlNum
            opMatlNum.Size = p_strMatlNum.Length
            oraCmd.Parameters.Add(opMatlNum)

            ParametersHelper.AddParametersToCommand("pi_SkuId", ParameterDirection.Input, OracleType.Number, p_intSKUID, oraCmd)

            Dim opCountryId As New OracleParameter()
            opCountryId.ParameterName = "pi_Countryid"
            opCountryId.Direction = ParameterDirection.Input
            opCountryId.OracleType = OracleType.Number
            opCountryId.Value = p_intCountryID
            oraCmd.Parameters.Add(opCountryId)

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnSaved = (rowsaffected = 1)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnSaved

    End Function

    ''' <summary>
    ''' Saves the certification group countries.
    ''' </summary>
    ''' <param name="p_DeleteMe">if set to <c>true</c> [p_ delete me].</param>
    ''' <param name="p_strMatlNum">SAP Material</param>
    ''' <param name="p_intCertificationID">The country ID that certification is being requested.</param>
    ''' <param name="p_intSKUID">The SKU ID that certification is being requested.</param>
    Public Function SaveCertificationGroup(ByVal p_DeleteMe As Boolean, ByVal p_strMatlNum As String, _
                                        ByVal p_intCertificationID As Integer, ByVal p_intSKUID As Integer) As Boolean

        Dim oraCmd As New OracleCommand
        Dim blnSaved As Boolean = False

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_CRUD.ProductCountry_Save"

            Dim opDeleteMe As New OracleParameter()
            opDeleteMe.ParameterName = "ps_DeleteMe"
            opDeleteMe.Direction = ParameterDirection.Input
            opDeleteMe.OracleType = OracleType.Char
            If p_DeleteMe Then
                opDeleteMe.Value = "y"
            Else
                opDeleteMe.Value = "n"
            End If
            oraCmd.Parameters.Add(opDeleteMe)

            Dim opMatlNum As New OracleParameter()
            opMatlNum.ParameterName = "ps_Matl_Num"
            opMatlNum.Direction = ParameterDirection.Input
            opMatlNum.OracleType = OracleType.VarChar
            opMatlNum.Value = p_strMatlNum
            oraCmd.Parameters.Add(opMatlNum)

            Dim opSKUID As New OracleParameter()
            opSKUID.ParameterName = "pi_SKUID"
            opSKUID.Direction = ParameterDirection.Input
            opSKUID.OracleType = OracleType.Number
            opSKUID.Value = p_intSKUID
            oraCmd.Parameters.Add(opSKUID)

            Dim opCertificationId As New OracleParameter()
            opCertificationId.ParameterName = "pi_CertificationId"
            opCertificationId.Direction = ParameterDirection.Input
            opCertificationId.OracleType = OracleType.Number
            opCertificationId.Value = p_intCertificationID
            oraCmd.Parameters.Add(opCertificationId)

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnSaved = (rowsaffected = 1)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnSaved

    End Function


    ''' <summary>
    ''' Saves the certification requests from grid on MarketingNew screen.
    ''' </summary>
    ''' <param name="p_DeleteMe">if set to <c>true</c> [p_ delete me].</param>
    ''' <param name="p_strMatlNum">SAP Material</param>
    ''' <param name="p_intCertificationID">The country ID that certification is being requested.</param>
    ''' <param name="p_intSKUID">The SKU ID that certification is being requested.</param>
    Public Function SaveRequestCert(ByVal p_DeleteMe As Boolean, ByVal p_strMatlNum As String, _
                                        ByVal p_intCertificationID As Integer, ByVal p_intSKUID As Integer) As Boolean

        Dim oraCmd As New OracleCommand
        Dim blnSaved As Boolean = False

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_CRUD.ProductRequestCert_Save"

            Dim opDeleteMe As New OracleParameter()
            opDeleteMe.ParameterName = "ps_DeleteMe"
            opDeleteMe.Direction = ParameterDirection.Input
            opDeleteMe.OracleType = OracleType.Char
            If p_DeleteMe Then
                opDeleteMe.Value = "y"
            Else
                opDeleteMe.Value = "n"
            End If
            oraCmd.Parameters.Add(opDeleteMe)

            Dim opMatlNum As New OracleParameter()
            opMatlNum.ParameterName = "ps_Matl_Num"
            opMatlNum.Direction = ParameterDirection.Input
            opMatlNum.OracleType = OracleType.VarChar
            opMatlNum.Value = p_strMatlNum
            oraCmd.Parameters.Add(opMatlNum)

            Dim opSKUID As New OracleParameter()
            opSKUID.ParameterName = "pi_SKUID"
            opSKUID.Direction = ParameterDirection.Input
            opSKUID.OracleType = OracleType.Number
            opSKUID.Value = p_intSKUID
            oraCmd.Parameters.Add(opSKUID)

            Dim opCertificationId As New OracleParameter()
            opCertificationId.ParameterName = "pi_CertificationId"
            opCertificationId.Direction = ParameterDirection.Input
            opCertificationId.OracleType = OracleType.Number
            opCertificationId.Value = p_intCertificationID
            oraCmd.Parameters.Add(opCertificationId)

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnSaved = (rowsaffected = 1)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnSaved

    End Function



    ''' <summary>
    ''' Get all Regions data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCountries(ByVal p_strRegionName As String) As DataSet

        Dim dstResults As New DataSet           'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = "ICS_CRUD.GetCountriesByRegionName"

        Dim oraResults As New OracleParameter()
        oraResults.ParameterName = "pc_Countries"
        oraResults.OracleType = OracleType.Cursor
        oraResults.Direction = ParameterDirection.Output
        ' Add the parameters
        oraCmd.Parameters.Add(oraResults)

        Dim oraRegionName As New OracleParameter()
        oraRegionName.ParameterName = "ps_RegionName"
        oraRegionName.OracleType = OracleType.VarChar
        oraRegionName.Direction = ParameterDirection.Input
        oraRegionName.Value = p_strRegionName
        oraRegionName.Size = p_strRegionName.Length
        ' Add the parameters
        oraCmd.Parameters.Add(oraRegionName)

        Try
            Connect()
            oraCmd.Connection = Connection

            ' Get the data
            oraAdp.SelectCommand = oraCmd
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
    ''' Get NOM Importers
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetImporters() As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = "ICS_CRUD.GETIMPORTERS"

        ParametersHelper.AddParametersToCommand("pc_importers", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

        Try
            Connect()
            oraCmd.Connection = Connection

            ' Get the data
            oraAdp.SelectCommand = oraCmd
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
    ''' Get NOM Customers
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomers() As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = "ICS_CRUD.GETCUSTOMERS"

        ParametersHelper.AddParametersToCommand("pc_customers", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

        Try
            Connect()
            oraCmd.Connection = Connection

            ' Get the data
            oraAdp.SelectCommand = oraCmd
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
    ''' <summary>
    ''' Gets the certification search results.
    ''' </summary>
    ''' <param name="ps_SearchCriteria">The search criteria.</param>
    ''' <param name="p_SearchType">Type of the search.</param>
    ''' <returns></returns>
    Public Function GetCertificationSearchResults(ByVal ps_SearchCriteria As String, ByVal p_SearchType As String, ByVal p_strExtensionNo As String, ByVal p_strImarkFamily As String, ByVal ps_BrandLine As String) As DataTable

        Dim dstResults As New DataSet       'return

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'Add's the parameter info to the Oracle Command
            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_SearchCriteria", ParameterDirection.Input, OracleType.VarChar, ps_SearchCriteria, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_SearchType", ParameterDirection.Input, OracleType.VarChar, p_SearchType, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_ExtensionNo", ParameterDirection.Input, OracleType.VarChar, p_strExtensionNo, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_imarkFamily", ParameterDirection.Input, OracleType.VarChar, p_strImarkFamily, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_BrandLine", ParameterDirection.Input, OracleType.VarChar, ps_BrandLine, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.GET_CERTIFICATIONSEARCHRESULT"

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
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

        Return dstResults.Tables(0)

    End Function

    ' Added GetBrands method to retrieve Brands through Product Data Web Service
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Get Brands
    ''' </summary>
    ''' <param name="p_strBrand"> Brand </param>
    ''' <returns>DataTable</returns>
    Public Function GetBrands(ByVal p_strBrand As String) As DataTable

        'Dim dstResults As New DataSet
        'errCode = 0
        'errMessage = String.Empty
        'Try
        '    oProductDataWebService = New ProductDataWebService.ProductDataWebService
        '    oProductDataWebService.Credentials = System.Net.CredentialCache.DefaultCredentials
        '    dstResults = oProductDataWebService.GetProductLine(p_strBrand, errCode, errMessage)
        '    Return GetDistinctRecords(dstResults.Tables(0), "BRAND")
        'Catch exp As Exception
        '    EventLogger.Enter(exp)
        '    Throw exp
        'Finally
        '    ' Ensures connection is closed in the event of an exception           
        'End Try

        'Added as per Incident # 31208 and Change Order # 6074
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = "CERTIFICATION_CRUD.GET_BRANDS"

        ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

        Try
            Connect()
            oraCmd.Connection = Connection

            ' Get the data
            oraAdp.SelectCommand = oraCmd
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

        Return dstResults.Tables(0)

    End Function

    ' Added GetBrandLines method to retrieve the Brand Lines from Product data web service 
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Gets the Brand Line results.
    ''' </summary>
    ''' <param name="p_strLine"></param>    
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetBrandLines(ByVal p_strLine As String) As DataTable

        'Dim dstResults As New DataSet
        'errCode = 0
        'errMessage = String.Empty
        'Dim dv As DataView
        'Dim strFilterCriteria As String
        'Try
        '    oProductDataWebService = New ProductDataWebService.ProductDataWebService
        '    oProductDataWebService.Credentials = System.Net.CredentialCache.DefaultCredentials
        '    dstResults = oProductDataWebService.GetProductLine(String.Empty, errCode, errMessage)

        '    dv = dstResults.Tables(0).DefaultView
        '    strFilterCriteria = "BRAND = '" & p_strLine & "'"
        '    dv.RowFilter = strFilterCriteria
        '    Return GetDistinctRecords(dv.ToTable(), "LINE")
        'Catch exp As Exception
        '    EventLogger.Enter(exp)
        '    Throw exp
        'Finally
        '    ' Ensures connection is closed in the event of an exception
        'End Try

        'Added as per Incident # 31208 and Change Order # 6074
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = "CERTIFICATION_CRUD.GET_BRANDLINES"

        ParametersHelper.AddParametersToCommand("ps_Brand", ParameterDirection.Input, OracleType.VarChar, p_strLine, oraCmd)
        ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

        Try
            Connect()
            oraCmd.Connection = Connection

            ' Get the data
            oraAdp.SelectCommand = oraCmd
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

        Return dstResults.Tables(0)

    End Function

    ' Added GetMaterialAttribs method to retrieve the attributes for material number from Product data web service 
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Gets the attributes for Material ID List.
    ''' </summary>
    ''' <param name="p_strMaterialIdList"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetMaterialAttribs(ByVal p_strMaterialIdList As String) As DataTable
        Dim dstResults As New DataSet
        Dim dtAttribs As New DataTable
        errCode = 0
        errMessage = String.Empty

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = "CERTIFICATION_CRUD.GETSKUDESCRIPTORS"

        ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMaterialIdList, oraCmd)
        ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

        Try
            Connect()
            oraCmd.Connection = Connection

            ' Get the data
            oraAdp.SelectCommand = oraCmd
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

        Return dstResults.Tables(0)
    End Function

    ''' <summary>
    ''' Gets the certification search results by brand.
    ''' </summary>
    ''' <param name="p_strBrandCode">Brand Code</param>
    ''' <returns></returns>
    'Public Function GetCertificationSearchResultsByBrandCode(ByVal p_strBrandCode As String) As DataTable

    '    no longer used JESEITZ 6/20/16

    '    Dim dstResults As New DataSet       'return

    '    Dim oraCmd As New OracleCommand
    '    Dim oraAdp As New OracleDataAdapter

    '    Try
    '        Dim oraResults As New OracleParameter("pc_retCursor", OracleType.Cursor)
    '        oraResults.Direction = ParameterDirection.Output

    '        Dim oraSearchCriteria As New OracleParameter("ps_Brandcode", OracleType.VarChar)
    '        oraSearchCriteria.Direction = ParameterDirection.Input
    '        oraSearchCriteria.Value = p_strBrandCode

    '        'configure the command object
    '        oraCmd.CommandType = CommandType.StoredProcedure
    '        oraCmd.CommandText = "CERTIFICATION_CRUD.GET_CERTIFICATIONBYBRANDCODE"

    '        'add the parameters
    '        oraCmd.Parameters.Add(oraResults)
    '        oraCmd.Parameters.Add(oraSearchCriteria)

    '        Connect()
    '        oraCmd.Connection = Connection

    '        'Get the data
    '        oraAdp.SelectCommand = oraCmd
    '        oraAdp.Fill(dstResults)
    '    Catch exp As Exception
    '        EventLogger.Enter(exp)
    '        Throw exp
    '    Finally
    '        ' Ensures connection is closed in the event of an exception
    '        Disconnect()
    '        oraCmd.Dispose()
    '        oraAdp.Dispose()
    '    End Try

    '    Return dstResults.Tables(0)

    'End Function

    ''' <summary>
    ''' Save Audit Log entry
    ''' </summary>
    ''' <param name="p_dteChangeDateTime"></param>
    ''' <param name="m_strChangedBy"></param>
    ''' <param name="m_strArea"></param>
    ''' <param name="m_strChangedFieldElement"></param>
    ''' <param name="m_strOldValue"></param>
    ''' <param name="m_strNewValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAuditLogEntry(ByVal p_dteChangeDateTime As DateTime, _
                                ByVal m_strChangedBy As String, _
                                ByVal m_strArea As String, _
                                ByVal m_strChangedFieldElement As String, _
                                ByVal m_strOldValue As String, _
                                ByVal m_strNewValue As String, _
                                ByVal m_intReasonID As Integer, _
                                ByVal m_strNote As String) As System.Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.AUDITLOG_INSERT"

            ParametersHelper.AddParametersToCommand("pd_ChangeDateTime", ParameterDirection.Input, OracleType.DateTime, DateTime.Now(), oraCmd)
            ParametersHelper.AddParametersToCommand("ps_ChangedBy", ParameterDirection.Input, OracleType.VarChar, m_strChangedBy, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_Area", ParameterDirection.Input, OracleType.VarChar, m_strArea, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_ChangedFiled_Element", ParameterDirection.Input, OracleType.VarChar, m_strChangedFieldElement, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_OLDValue", ParameterDirection.Input, OracleType.VarChar, m_strOldValue, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_NewValue", ParameterDirection.Input, OracleType.VarChar, m_strNewValue, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_ReasonID", ParameterDirection.Input, OracleType.Number, m_intReasonID, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_Note", ParameterDirection.Input, OracleType.VarChar, m_strNote, oraCmd)
            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If rowsaffected = 1 Then
                blnSaved = True
            End If
        Catch ex As Exception
            blnSaved = False
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnSaved

    End Function

    ''' <summary>
    ''' Gets the certificate info.
    ''' </summary>
    ''' <param name="ps_CertificationNumber"></param>
    ''' <param name="ps_ExtensionNo"></param>
    ''' <param name="ps_CertificationTypeID"></param>
    ''' <param name="p_iSKUID"></param>
    ''' <param name="p_blnTRsExist"></param>
    ''' <returns>dstResults</returns>
    Public Function GetCertificate(ByVal ps_CertificationNumber As String, ByVal ps_ExtensionNo As String, ByVal ps_CertificationTypeID As Integer, ByVal p_iSKUID As Integer, ByRef p_blnTRsExist As Boolean) As DataTable

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_certificationNumber", ParameterDirection.Input, OracleType.VarChar, ps_CertificationNumber, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_extensionNo", ParameterDirection.Input, OracleType.VarChar, ps_ExtensionNo, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_certificationTypeID", ParameterDirection.Input, OracleType.Number, ps_CertificationTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_SKUId", ParameterDirection.Input, OracleType.Number, p_iSKUID, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_TRExists", ParameterDirection.Output, OracleType.VarChar, 1, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.GetCertificatesInfo"

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
            oraAdp.Fill(dstResults)

            p_blnTRsExist = False
            If Not oraCmd.Parameters.Item("ps_TRExists").Value.Equals(DBNull.Value) Then
                p_blnTRsExist = oraCmd.Parameters.Item("ps_TRExists").Value.Equals("y")
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults.Tables(0)

    End Function

    ''' <summary>
    ''' Gets the similar certificate info.
    ''' </summary>
    ''' <param name="p_iCertificationTypeID">Id of the certification.</param>
    ''' <param name="p_strMatlNum">Material Number.</param>
    ''' <returns></returns>
    Public Function GetSimilarCertificate(ByVal p_iCertificationTypeID As Integer, ByVal p_strMatlNum As String, ByVal p_strCertificationNumber As String) As DataTable
        Dim dstResults As New DataSet       'return

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'GetCertificatesInfo(pc_retCursor out retCursor,ps_SKU IN VARCHAR2,ps_certificationName in varchar2);

            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_CertificationTypeID", ParameterDirection.Input, OracleType.Number, p_iCertificationTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_CertificationNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificationNumber, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.GetSimilarCertificateInfo"

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
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

        Return dstResults.Tables(0)

    End Function

    ''Added p_strFamilyDesc parameter as per IDEA2706 International Certification System Modifications 
    ''JBH_2.00 Project 5325: Added Mold Change Required and Operations Date Approved parameters
    ''' <summary>
    ''' Save Certificate
    ''' </summary>
    Public Function SaveCertificate(ByVal p_iSKUId As Integer, _
                                ByVal p_strMatlNum As String, _
                                ByVal p_blnRemoveMatlNum As Boolean, _
                                ByVal p_strCertificationTypeName As String, _
                                ByVal p_strCERTIFICATENUMBER As String, _
                                ByVal p_dteCertDateSubmitted As DateTime, _
                                ByVal p_dteCertDateApproved_CEGI As DateTime, _
                                ByVal p_dteDATESUBMITED As DateTime, _
                                ByVal pc_ACTIVESTATUS As String, _
                                ByVal p_dteDATEASSIGNED_EGI As DateTime, _
                                ByVal p_dteDATEAPROVED_CEGI As DateTime, _
                                ByVal pc_RENEWALREQUIRED_CGIN As Char, _
                                ByVal p_strJOBREPORTNUMBER_CEN As String, _
                                ByVal p_strEXTENSION_EN As String, _
                                ByVal p_strSUPPLEMENTALMOLDSTAMPING_E As String, _
                                ByVal p_strEMARKREFERENCE_I As String, _
                                ByVal p_dteEXPIRYDATE_I As DateTime, _
                                ByVal p_strFAMILY_I As String, _
                                ByVal p_strPRODUCTLOCATION As String, _
                                ByVal p_strCOUNTRYOFMANUFACTURE_N As String, _
                                ByVal p_blnAddNewCustomer As Boolean, _
                                ByVal p_strActSigReq As String, _
                                ByVal p_intCUSTOMERID As Integer, _
                                ByVal p_strCUSTOMER_N As String, _
                                ByVal p_strCUSTOMERADDRESS_N As String, _
                                ByVal p_strCUSTOMERSPECIFIC_N As String, _
                                ByVal p_blnAddNewImporter As Boolean, _
                                ByVal p_intIMPORTERID As Integer, _
                                ByVal p_strIMPORTER_N As String, _
                                ByVal p_strIMPORTERADDRESS_N As String, _
                                ByVal p_strIMPORTERREPRESENTATIVE_N As String, _
                                ByVal p_strCOUNTRYLOCATION_N As String, _
                                ByVal p_strBATCHNUMBER_G As String, _
                                ByVal p_dteSUPPLEMENTALASSIGNED As DateTime, _
                                ByVal p_dteSUPPLEMENTALSUBMITTED As DateTime, _
                                ByVal p_dteSUPPLEMENTALAPPROVED As DateTime, _
                                ByVal p_strCOMPANYNAME As String, _
                                ByVal p_strUserName As String, _
                                ByRef p_intCertificateNumberID As Integer, _
                                ByVal p_strFamilyDesc As String, _
                                ByVal p_blnMoldChgRequired As Boolean, _
                                ByVal p_dteOperDateApproved As DateTime, _
                                ByVal p_strAddInfo As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Dim oraParam As OracleParameter

        Try
            ParametersHelper.AddParametersToCommand("pi_RetId", ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            '"pi_CertificateID", p_intCertificateNumberID
            ParametersHelper.AddParametersToCommand("pi_CertificateID", ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)
            '0  pi_SKUID
            ParametersHelper.AddParametersToCommand("pi_SKUId", ParameterDirection.Input, OracleType.Number, p_iSKUId, oraCmd)
            '1  ps_Matl_Num 
            oraParam = New OracleParameter("ps_Matl_Num", p_strMatlNum)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '2 p_strRemoveMatlNum
            Dim p_strRemoveMatlNum As String = String.Empty
            If p_blnRemoveMatlNum = True Then
                p_strRemoveMatlNum = "y"
            Else
                p_strRemoveMatlNum = "n"
            End If
            oraParam = New OracleParameter("ps_Remove_Matl_Num", p_strRemoveMatlNum)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '3  ps_CertificationTypeName
            oraParam = New OracleParameter("ps_CertificationTypeName", p_strCertificationTypeName)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '4  ps_CERTIFICATENUMBER 
            oraParam = New OracleParameter("ps_CERTIFICATENUMBER", p_strCERTIFICATENUMBER)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            'pd_certdatesubmitted
            oraParam = New OracleParameter("pd_certdatesubmitted", IIf(p_dteCertDateSubmitted.Equals(DateTime.MinValue), DBNull.Value, p_dteCertDateSubmitted))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.DateTime
            oraCmd.Parameters.Add(oraParam)
            'pd_certdateapproved_cegi
            oraParam = New OracleParameter("pd_certdateapproved_cegi", IIf(p_dteCertDateApproved_CEGI.Equals(DateTime.MinValue), DBNull.Value, p_dteCertDateApproved_CEGI))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.DateTime
            oraCmd.Parameters.Add(oraParam)

            '5 pd_DATESUBMITED
            oraParam = New OracleParameter("pd_DATESUBMITED", IIf(p_dteDATESUBMITED.Equals(DateTime.MinValue), DBNull.Value, p_dteDATESUBMITED))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.DateTime
            oraCmd.Parameters.Add(oraParam)
            '6 pc_ACTIVESTATUS
            oraParam = New OracleParameter("pc_ACTIVESTATUS", pc_ACTIVESTATUS)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '7 pd_DATEASSIGNED_EGI
            oraParam = New OracleParameter("pd_DATEASSIGNED_EGI", IIf(p_dteDATEASSIGNED_EGI.Equals(DateTime.MinValue), DBNull.Value, p_dteDATEASSIGNED_EGI))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.DateTime
            oraCmd.Parameters.Add(oraParam)
            '8 pd_DateApproved_CEGI
            oraParam = New OracleParameter("pd_DateApproved_CEGI", IIf(p_dteDATEAPROVED_CEGI.Equals(DateTime.MinValue), DBNull.Value, p_dteDATEAPROVED_CEGI))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.DateTime
            oraCmd.Parameters.Add(oraParam)
            '9  pc_RENEWALREQUIRED_CGIN
            oraParam = New OracleParameter("pc_RENEWALREQUIRED_CGIN", pc_RENEWALREQUIRED_CGIN)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '12 ps_JOBREPORTNUMBER_CEN
            oraParam = New OracleParameter("ps_JOBREPORTNUMBER_CEN", p_strJOBREPORTNUMBER_CEN)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '13 ps_EXTENSION_EN
            oraParam = New OracleParameter("ps_EXTENSION_EN", p_strEXTENSION_EN)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '14 ps_SUPPLEMENTALMOLDSTAMPING_E
            oraParam = New OracleParameter("ps_SUPPLEMENTALMOLDSTAMPING_E", p_strSUPPLEMENTALMOLDSTAMPING_E)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '15 ps_EMARKREFERENCE_I
            oraParam = New OracleParameter("ps_EMARKREFERENCE_I", p_strEMARKREFERENCE_I)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '16 pd_EXPIRYDATE_I
            oraParam = New OracleParameter("pd_EXPIRYDATE_I", IIf(p_dteEXPIRYDATE_I.Equals(DateTime.MinValue), DBNull.Value, p_dteEXPIRYDATE_I))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.DateTime
            oraCmd.Parameters.Add(oraParam)
            '17 ps_FAMILY_I
            oraParam = New OracleParameter("ps_FAMILY_I", p_strFAMILY_I)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            '18 ps_Family_Desc
            oraParam = New OracleParameter("ps_Family_Desc", p_strFamilyDesc)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            '19 ps_PRODUCTLOCATION_C
            oraParam = New OracleParameter("ps_PRODUCTLOCATION", p_strPRODUCTLOCATION)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '20 ps_COUNTRYOFMANUFACTURE_N
            oraParam = New OracleParameter("ps_COUNTRYOFMANUFACTURE_N", p_strCOUNTRYOFMANUFACTURE_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            Dim p_strAddNewCustomer As String = String.Empty
            If p_blnAddNewCustomer = True Then
                p_strAddNewCustomer = "y"
            Else
                p_strAddNewCustomer = "n"
            End If
            oraParam = New OracleParameter("ps_addnewCustomer", p_strAddNewCustomer)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            'Actual Signature Required
            ParametersHelper.AddParametersToCommand("ps_actSigreq", ParameterDirection.Input, OracleType.VarChar, p_strActSigReq, oraCmd)

            'Customer ID
            ParametersHelper.AddParametersToCommand("pi_CUSTOMERID", ParameterDirection.Input, OracleType.Number, p_intCUSTOMERID, oraCmd)

            '22 ps_CUSTOMER_N
            oraParam = New OracleParameter("ps_CUSTOMER_N", p_strCUSTOMER_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            oraParam = New OracleParameter("ps_CUSTOMERADDRESS_N", p_strCUSTOMERADDRESS_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '21 ps_CUSTOMERSPECIFIC_N
            oraParam = New OracleParameter("ps_CUSTOMERSPECIFIC_N", p_strCUSTOMERSPECIFIC_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            Dim p_strAddNewImporter As String = String.Empty
            If p_blnAddNewImporter = True Then
                p_strAddNewImporter = "y"
            Else
                p_strAddNewImporter = "n"
            End If
            oraParam = New OracleParameter("ps_addnewImporter", p_strAddNewImporter)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            ParametersHelper.AddParametersToCommand("pi_IMPORTERID", ParameterDirection.Input, OracleType.Number, p_intIMPORTERID, oraCmd)

            '23 ps_IMPORTER_N
            oraParam = New OracleParameter("ps_IMPORTER_N", p_strIMPORTER_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '24 ps_IMPORTERADDRESS_N
            oraParam = New OracleParameter("ps_IMPORTERADDRESS_N", p_strIMPORTERADDRESS_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '25 ps_IMPORTERREPRESENTATIVE_N
            oraParam = New OracleParameter("ps_IMPORTERREPRESENTATIVE_N", p_strIMPORTERREPRESENTATIVE_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            '26 ps_COUNTRYLOCATION_N
            oraParam = New OracleParameter("ps_COUNTRYLOCATION_N", p_strCOUNTRYLOCATION_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '27  ps_BATCHNUMBER_G
            oraParam = New OracleParameter("ps_BATCHNUMBER_G", p_strBATCHNUMBER_G)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            oraParam = New OracleParameter("ps_COMPANYNAME", p_strCOMPANYNAME)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            oraParam = New OracleParameter("ps_UserName", p_strUserName)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            'Mold Change Required - JBH_2.00 Project 5325
            Dim p_strMoldChgRequired As String = String.Empty
            If p_blnMoldChgRequired = True Then
                p_strMoldChgRequired = "y"
            Else
                p_strMoldChgRequired = "n"
            End If
            oraParam = New OracleParameter("ps_Mold_Changed", p_strMoldChgRequired)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            'Operations Approval Date - JBH_2.00 Project 5325
            oraParam = New OracleParameter("pd_Oper_Date_Approved", IIf(p_dteOperDateApproved.Equals(DateTime.MinValue), DBNull.Value, p_dteOperDateApproved))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.DateTime
            oraCmd.Parameters.Add(oraParam)

            'Additional Information - jeseitz 10/29/2016
            oraParam = New OracleParameter("ps_Additional_Info", p_strAddInfo)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)


            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.CERTIFICATE_SAVE"
            'If p_strCertificationTypeName = NameAid.Certification.Imark.ToString() Then
            '    oraCmd.CommandText = "CERTIFICATION_CRUD.Certificate_Save_IMARK"
            'Else
            '    oraCmd.CommandText = "CERTIFICATION_CRUD.Certificate_Save"
            'End If

            Connect()
            oraCmd.Connection = Connection

            Dim rowsAffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsAffected > 0) Then
                ' Pass back the ID for a new certificate:
                If p_intCertificateNumberID = 0 AndAlso Not oraCmd.Parameters.Item("pi_RetId").Value.Equals(DBNull.Value) Then
                    p_intCertificateNumberID = oraCmd.Parameters.Item("pi_RetId").Value
                End If
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Mass Update Batch Numbers
    ''' </summary>
    Public Function BatchNumMassUpdate(ByVal p_strCertifName As String, _
                                ByVal p_strTempBatchNum As String, _
                                ByVal p_strGSOBatchNum As String, _
                                ByVal p_strUserName As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Dim oraParam As OracleParameter

        Try
            '1  ps_certificationtypename
            oraParam = New OracleParameter("ps_certificationtypename", p_strCertifName)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '2  ps_temp_batchnumber_g
            oraParam = New OracleParameter("ps_temp_batchnumber_g", p_strTempBatchNum)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '3  ps_batchnumber_g
            oraParam = New OracleParameter("ps_batchnumber_g", p_strGSOBatchNum)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)
            '4 ps_username
            oraParam = New OracleParameter("ps_username", p_strUserName)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(oraParam)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.certificate_update_batch"

            Connect()
            oraCmd.Connection = Connection

            oraCmd.ExecuteNonQuery()
            enumSaveResult = NameAid.SaveResult.Sucess
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Check Similar Tire
    ''' </summary>
    ''' <param name="pi_certType"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="ps_similarMatlNum"></param>
    ''' <param name="pi_imarkFamily"></param>
    ''' <param name="ps_eceReference"></param>
    ''' <param name="ps_errorDesc"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckSimilarTire(ByVal pi_certType As Integer, ByVal p_strMatlNum As String, ByRef ps_similarMatlNum As String, ByRef pi_imarkFamily As Integer, ByRef ps_eceReference As String, ByRef ps_errorDesc As String) As Integer

        Dim oraCmd As New OracleCommand
        Dim li_errorNum As Integer

        Try
            'Oracle command
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = String.Empty

            'Input parameters
            ParametersHelper.AddParametersToCommand("pn_cert_type", ParameterDirection.Input, OracleType.Number, pi_certType, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_in_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)

            'Output parameters
            ParametersHelper.AddParametersToCommand("ps_Similar_Matl_Num", ParameterDirection.Output, OracleType.VarChar, 30, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_imark_family", ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_ece_reference", ParameterDirection.Output, OracleType.VarChar, 30, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pn_error_num", ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_error_desc", ParameterDirection.Output, OracleType.VarChar, 200, Nothing, oraCmd)

            'Stored procedure
            oraCmd.CommandText = "SIMILAR_TIRES.GET_SIMILAR_SKU"

            'Make connection and execute
            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            li_errorNum = oraCmd.Parameters.Item("pn_error_num").Value

            If Not oraCmd.Parameters.Item("ps_error_desc").Value.Equals(DBNull.Value) Then
                ps_errorDesc = oraCmd.Parameters.Item("ps_error_desc").Value
            Else
                ps_errorDesc = String.Empty
            End If

            If Not oraCmd.Parameters.Item("ps_Similar_Matl_Num").Value.Equals(DBNull.Value) Then
                ps_similarMatlNum = oraCmd.Parameters.Item("ps_Similar_Matl_Num").Value
            Else
                ps_similarMatlNum = String.Empty
            End If

            If Not oraCmd.Parameters.Item("ps_imark_family").Value.Equals(DBNull.Value) Then
                pi_imarkFamily = oraCmd.Parameters.Item("ps_imark_family").Value
            Else
                pi_imarkFamily = 0
            End If

            If Not oraCmd.Parameters.Item("ps_ece_reference").Value.Equals(DBNull.Value) Then
                ps_eceReference = oraCmd.Parameters.Item("ps_ece_reference").Value
            Else
                ps_eceReference = String.Empty
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return li_errorNum

    End Function

    ''' <summary>
    ''' Renew Imark Certificate
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RenewCertificate(ByVal p_intCertificateID As Integer, ByRef p_intNewCertificateID As Integer, ByVal p_strUserName As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim dstResults As New DataSet       'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = String.Empty

            ParametersHelper.AddParametersToCommand("pi_newId", ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_oldId", ParameterDirection.Input, OracleType.Number, p_intCertificateID, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_operatorName", ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)

            oraCmd.CommandText = "certification_crud.IMARKCERTIFICATE_RENEW"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsAffected As Integer = oraCmd.ExecuteNonQuery()
            'If (rowsAffected > 0) Then
            p_intNewCertificateID = oraCmd.Parameters.Item("pi_newId").Value
            enumSaveResult = NameAid.SaveResult.Sucess
            'End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Gets the product information to be displayed on the product section of test result.
    ''' </summary>
    ''' <param name="p_strMatlNum">The Material Number.</param>
    ''' <returns>data table containing product info</returns>
    Public Function GetProductData(ByVal p_strMatlNum As String, ByVal p_iSKUID As Integer) As ICSDataSet.ProductDataDataTable

        'Dim dsProduct As New TRACSSharedDatasets.SKUtoICSDataset
        Dim dsICSDatasets As New ICSDataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ' Add the parameters
            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_SKUId", ParameterDirection.Input, OracleType.Number, p_iSKUID, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.GetProductData"

            Connect()
            oraCmd.Connection = Connection
            ' Get the data
            oraAdp.SelectCommand = oraCmd
            dsICSDatasets.EnforceConstraints = False
            oraAdp.TableMappings.Add("Table", "ProductData")
            oraAdp.Fill(dsICSDatasets)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dsICSDatasets.ProductData

    End Function

    ''' <summary>
    ''' Save_s the product.
    ''' </summary>    
    Public Function Save_Product(ByVal p_iSKUID As Integer, _
                                 ByVal p_strMatlNum As String, _
                                ByVal p_strBrand As String, _
                                ByVal p_strBrandLine As String, _
                                ByVal p_iTireTypeId As Integer, _
                                ByVal ps_PSN As String, _
                                ByVal p_strSizeStamp As String, _
                                ByVal p_dteDiscontinuedDate As DateTime, _
                                ByVal p_strSPECNUMBER As String, _
                                ByVal p_strSPEEDRATING As String, _
                                ByVal p_strSINGLOADINDEX As String, _
                                ByVal p_strDUALLOADINDEX As String, _
                                ByVal p_strBIASBELTEDRADIAL As String, _
                                ByVal p_strTUBElESSYN As String, _
                                ByVal p_strREINFORCEDYN As String, _
                                ByVal p_strEXTRALOADYN As String, _
                                ByVal p_strUTQGTREADWEAR As String, _
                                ByVal p_strUTQGTRACTION As String, _
                                ByVal p_strUTQGTEMP As String, _
                                ByVal p_strMUDSNOWYN As String, _
                                ByVal p_iRIMDIAMETER As Single, _
                                ByVal p_dteSerialDate As DateTime, _
                                ByVal p_strBrandDesc As String, _
                                ByVal p_strMeaRimWidth As Single, _
                                ByVal p_strLoadRange As String, _
                                ByVal p_strRegroovableInd As String, _
                                ByVal p_strPlantProduced As String, _
                                ByVal p_dteMostRecentTestDate As DateTime, _
                                ByVal p_strIMark As String, _
                                ByVal p_strInformeNumber As String, _
                                ByVal p_dteFechaDate As DateTime, _
                                ByVal p_strTreadPattern As String, _
                                ByVal p_strSpecialProtectiveBand As String, _
                                ByVal p_strNominalTireWidth As String, _
                                ByVal p_strAspectRadio As String, _
                                ByVal p_strTreadwearIndicators As String, _
                                ByVal p_strNameOfManufacturer As String, _
                                ByVal p_strFamily As String, _
                                ByVal p_strDOTSerialNumber As String, _
                                ByVal p_strTPN As String, _
                                ByVal p_strUserName As String, _
                                ByVal p_strSEVEREWEATHERIND As String, _
                                ByVal p_strMFGWWYY As String) As NameAid.SaveResult


        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'pi_SKUID in Number
            ParametersHelper.AddParametersToCommand("pi_SKUID", ParameterDirection.Input, OracleType.Number, p_iSKUID, oraCmd)
            '  ps_Matl_Num	      in VARCHAR2,
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            'ps_BRAND	in 	VARCHAR2,
            ParametersHelper.AddParametersToCommand("ps_Brand", ParameterDirection.Input, OracleType.VarChar, p_strBrand, oraCmd)
            'ps_Brand_Line	in 	VARCHAR2,
            ParametersHelper.AddParametersToCommand("ps_Brand_Line", ParameterDirection.Input, OracleType.VarChar, p_strBrandLine, oraCmd)
            'pi_TIRETYPEID	in 	NUMBER,p_iTireTypeId
            ParametersHelper.AddParametersToCommand("pi_TIRETYPEID", ParameterDirection.Input, OracleType.Number, p_iTireTypeId, oraCmd)
            'ps_PSN	    in 	VARCHAR2,ps_PSN
            ParametersHelper.AddParametersToCommand("ps_PSN", ParameterDirection.Input, OracleType.VarChar, ps_PSN, oraCmd)
            'ps_SIZESTAMP	in 	VARCHAR2,p_strSizeStamp
            ParametersHelper.AddParametersToCommand("ps_SIZESTAMP", ParameterDirection.Input, OracleType.VarChar, p_strSizeStamp, oraCmd)
            'pd_DISCONTINUEDDATE	in 	DATE,p_dteDiscontinuedDate
            ParametersHelper.AddParametersToCommand("pd_DISCONTINUEDDATE", ParameterDirection.Input, OracleType.DateTime, p_dteDiscontinuedDate, oraCmd)
            'ps_SPECNUMBER	    in 	VARCHAR2,p_strSPECNUMBER
            ParametersHelper.AddParametersToCommand("ps_SPECNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strSPECNUMBER, oraCmd)
            'ps_SPEEDRATING	  in 	VARCHAR2,p_strSPEEDRATING
            ParametersHelper.AddParametersToCommand("ps_SPEEDRATING", ParameterDirection.Input, OracleType.VarChar, p_strSPEEDRATING, oraCmd)
            'ps_SINGLOADINDEX	in 	VARCHAR2,p_strSINGLOADINDEX
            ParametersHelper.AddParametersToCommand("ps_SINGLOADINDEX", ParameterDirection.Input, OracleType.VarChar, p_strSINGLOADINDEX, oraCmd)
            'ps_DUALLOADINDEX	in 	VARCHAR2,p_strDUALLOADINDEX
            ParametersHelper.AddParametersToCommand("ps_DUALLOADINDEX", ParameterDirection.Input, OracleType.VarChar, p_strDUALLOADINDEX, oraCmd)
            'ps_BELTEDRADIALYN	in 	VARCHAR2,p_strBELTEDRADIALYN
            ParametersHelper.AddParametersToCommand("ps_BIASBELTEDRADIAL", ParameterDirection.Input, OracleType.VarChar, p_strBIASBELTEDRADIAL, oraCmd)
            'ps_TUBElESSYN	    in 	VARCHAR2,p_strTUBElESSYN
            ParametersHelper.AddParametersToCommand("ps_TUBElESSYN", ParameterDirection.Input, OracleType.VarChar, p_strTUBElESSYN, oraCmd)
            'ps_REINFORCEDYN	  in 	VARCHAR2,p_strREINFORCEDYN
            ParametersHelper.AddParametersToCommand("ps_REINFORCEDYN", ParameterDirection.Input, OracleType.VarChar, p_strREINFORCEDYN, oraCmd)
            'ps_EXTRALOADYN	  in 	VARCHAR2,p_strEXTRALOADYN
            ParametersHelper.AddParametersToCommand("ps_EXTRALOADYN", ParameterDirection.Input, OracleType.VarChar, p_strEXTRALOADYN, oraCmd)
            'ps_UTQGTREADWEAR	in 	VARCHAR2,p_strUTQGTREADWEAR
            ParametersHelper.AddParametersToCommand("ps_UTQGTREADWEAR", ParameterDirection.Input, OracleType.VarChar, p_strUTQGTREADWEAR, oraCmd)
            'ps_UTQGTRACTION	  in 	VARCHAR2,p_strUTQGTRACTION
            ParametersHelper.AddParametersToCommand("ps_UTQGTRACTION", ParameterDirection.Input, OracleType.VarChar, p_strUTQGTRACTION, oraCmd)
            'ps_UTQGTEMP	      in 	VARCHAR2,p_strUTQGTEMP
            ParametersHelper.AddParametersToCommand("ps_UTQGTEMP", ParameterDirection.Input, OracleType.VarChar, p_strUTQGTEMP, oraCmd)
            'ps_MUDSNOWYN	    in 	VARCHAR2,p_strMUDSNOWYN
            ParametersHelper.AddParametersToCommand("ps_MUDSNOWYN", ParameterDirection.Input, OracleType.VarChar, p_strMUDSNOWYN, oraCmd)
            'pi_RIMDIAMETER	  in 	NUMBER,p_iRIMDIAMETER
            ParametersHelper.AddParametersToCommand("pi_RIMDIAMETER", ParameterDirection.Input, OracleType.Number, p_iRIMDIAMETER, oraCmd)
            'PD_SerialDate      IN DATE  ,
            ParametersHelper.AddParametersToCommand("pd_SERIALDATE", ParameterDirection.Input, OracleType.DateTime, p_dteSerialDate, oraCmd)
            'PS_BrandDesc      in varchar2,
            ParametersHelper.AddParametersToCommand("ps_BRANDDESC", ParameterDirection.Input, OracleType.VarChar, p_strBrandDesc, oraCmd)
            'PS_MeaRimWidth    in 	VARCHAR2,
            ParametersHelper.AddParametersToCommand("pi_MEARIMWIDTH", ParameterDirection.Input, OracleType.Number, p_strMeaRimWidth, oraCmd)
            'PS_LoadRange      in 	VARCHAR2,
            ParametersHelper.AddParametersToCommand("ps_LOADRANGE", ParameterDirection.Input, OracleType.VarChar, p_strLoadRange, oraCmd)
            'PS_RegroovableInd in 	VARCHAR2,
            ParametersHelper.AddParametersToCommand("ps_REGROOVABLEIND", ParameterDirection.Input, OracleType.VarChar, p_strRegroovableInd, oraCmd)
            'PS_PlantProduced  in 	VARCHAR2,
            ParametersHelper.AddParametersToCommand("ps_PLANTPRODUCED", ParameterDirection.Input, OracleType.VarChar, p_strPlantProduced, oraCmd)
            'PD_MostRecentTestDate IN DATE,
            ParametersHelper.AddParametersToCommand("pd_MOSTRECENTTESTDATE", ParameterDirection.Input, OracleType.DateTime, p_dteMostRecentTestDate, oraCmd)
            'PS_IMark          in 	VARCHAR2
            ParametersHelper.AddParametersToCommand("ps_IMARK", ParameterDirection.Input, OracleType.VarChar, p_strIMark, oraCmd)

            'ps_INFORMENUMBER  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_INFORMENUMBER", ParameterDirection.Input, OracleType.VarChar, p_strInformeNumber, oraCmd)
            'pd_FECHADATE      in TimeStamp,
            ParametersHelper.AddParametersToCommand("pd_FECHADATE", ParameterDirection.Input, OracleType.DateTime, p_dteFechaDate, oraCmd)
            'ps_TREADPATTERN        in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TREADPATTERN", ParameterDirection.Input, OracleType.VarChar, p_strTreadPattern, oraCmd)
            'ps_SPECIALPROTECTIVEBAND    in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_SPECIALPROTECTIVEBAND", ParameterDirection.Input, OracleType.VarChar, p_strSpecialProtectiveBand, oraCmd)
            'ps_NOMINALTIREWIDTH         in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_NOMINALTIREWIDTH", ParameterDirection.Input, OracleType.VarChar, p_strNominalTireWidth, oraCmd)
            'ps_ASPECTRADIO              in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_ASPECTRADIO", ParameterDirection.Input, OracleType.VarChar, p_strAspectRadio, oraCmd)
            'ps_TREADWEARINDICATORS      in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TREADWEARINDICATORS", ParameterDirection.Input, OracleType.VarChar, p_strTreadwearIndicators, oraCmd)
            'ps_NAMEOFMANUFACTURER       in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_NAMEOFMANUFACTURER", ParameterDirection.Input, OracleType.VarChar, p_strNameOfManufacturer, oraCmd)
            'ps_FAMILY                   in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_FAMILY", ParameterDirection.Input, OracleType.VarChar, p_strFamily, oraCmd)
            'ps_DOTSERIALNUMBER          in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_DOTSERIALNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strDOTSerialNumber, oraCmd)
            'ps_TPN
            ParametersHelper.AddParametersToCommand("ps_TPN", ParameterDirection.Input, OracleType.VarChar, p_strTPN, oraCmd)
            'ps_UserName                   in Varchar2
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_MFGWWYY", ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_SEVEREWEATHERIND", ParameterDirection.Input, OracleType.VarChar, p_strSEVEREWEATHERIND, oraCmd)


            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "TESTRESULTS_CRUD.Product_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            Else
                enumSaveResult = NameAid.SaveResult.SaveError
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Return distinct records on given column.
    ''' </summary>
    ''' <param name="dtResults"></param>
    ''' <param name="strCriteria"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Private Function GetDistinctRecords(ByVal dtResults As DataTable, ByVal strCriteria As String) As DataTable

        Dim dtUniqRecords As New DataTable
        dtUniqRecords = dtResults.DefaultView.ToTable(True, strCriteria)
        Return dtUniqRecords

    End Function

    Public Function GetCertificatesByType(ByVal p_certificationtypeid As Integer, ByVal p_all As String) As DataTable
        Dim dtDatatable As New DataTable
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ' Add the parameters
            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("pn_CertificationTypeID", ParameterDirection.Input, OracleType.Number, p_certificationtypeid, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_All", ParameterDirection.Input, OracleType.VarChar, p_all, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ics_common_functions.GetCertificatesByType"

            Connect()
            oraCmd.Connection = Connection
            ' Get the data
            oraAdp.SelectCommand = oraCmd
            oraAdp.Fill(dtDatatable)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dtDatatable

    End Function

    Public Function GetCertTemplate(ByVal p_strCertificationTypeName As String) As String

        'Returns the CertTemplate for a certification type name - added for generic certification types 6/9/16 jeseitz
        Dim oraCmd As New OracleCommand
        Dim p_strCertTemplate As String

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_COMMON_FUNCTIONS.GetCertTemplate"
            ParametersHelper.AddParametersToCommand("ps_CertificationTypeName", ParameterDirection.Input, OracleType.VarChar, 50, p_strCertificationTypeName, oraCmd)
            ParametersHelper.AddParametersToCommand("retvalue", ParameterDirection.ReturnValue, OracleType.VarChar, 50, "", oraCmd)
            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            'Gets the certificate number exists value
            p_strCertTemplate = Convert.ToString(oraCmd.Parameters.Item("retvalue").Value)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return p_strCertTemplate

    End Function

    Function GetCertificationNameByID(ByVal p_intCertificationTypeID As Integer) As String
        'Returns the CertTemplate for a certification type name - added for generic certification types 6/9/16 jeseitz
        Dim oraCmd As New OracleCommand
        Dim p_strCertificationType As String

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_COMMON_FUNCTIONS.GetCertificationNameByID"
            ParametersHelper.AddParametersToCommand("pn_CertificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand("retvalue", ParameterDirection.ReturnValue, OracleType.VarChar, 50, "", oraCmd)
            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            'Gets the certificationName value
            p_strCertificationType = Convert.ToString(oraCmd.Parameters.Item("retvalue").Value)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return p_strCertificationType

    End Function

#End Region
#Region "Test Result Data Measurement"

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Save Measurement part of the test.
    ''' </summary>        
    ''' <param name="p_strPROJECTNUMBER">The  PROJECTNUMBER.</param>
    ''' <param name="p_sngTIRENUMBER">The  TIRENUMBER.</param>
    ''' <param name="p_strTESTSPEC">The  TESTSPEC.</param>
    ''' <param name="p_dteCOMPLETIONDATE">The  COMPLETIONDATE.</param>
    ''' <param name="p_sngINFLATIONPRESSURE">The  INFLATIONPRESSURE.</param>
    ''' <param name="p_strMOLDDESIGN">The MOLDDESIGN.</param>
    ''' <param name="p_sngRIMWIDTH">The  RIMWIDTH.</param>
    ''' <param name="p_strDOTSERIALNUMBER">The  DOTSERIALNUMBER.</param>
    ''' <param name="p_sngDIAMETER">The  DIAMETER.</param>
    ''' <param name="p_sngAVGSECTIONWIDTH">The  AVGSECTIONWIDTH.</param>
    ''' <param name="p_sngAVGOVERALLWIDTH">The  AVGOVERALLWIDTH.</param>
    ''' <param name="p_sngMAXOVERALLWIDTH">The  MAXOVERALLWIDTH.</param>
    ''' <param name="p_sngSIZEFACTOR">The P_SNG SIZEFACTOR.</param>
    ''' <param name="p_dteMOUNTTIME">The p_dte MOUNTTIME.</param>
    ''' <param name="p_intSKUID">The p_int SKUID.</param>
    ''' <param name="p_intCertType">Type of the p_enu cert.</param>
    ''' <param name="p_strCERTIFICATENUMBER">The P_STR CERTIFICATENUMBER.</param>
    ''' <param name="p_dteSerialDate">The p_dte serial date.</param>
    ''' <param name="p_dteEndTime">The p_dte end time.</param>
    ''' <param name="p_sngActSizeFactor">The P_SNG act size factor.</param>
    ''' <param name="p_srtSTARTINFLATIONPRESSURE">The P_SRT STARTINFLATIONPRESSURE.</param>
    ''' <param name="p_srtENDINFLATIONPRESSURE">The P_SRT ENDINFLATIONPRESSURE.</param>
    ''' <param name="p_strADJUSTMENT">The P_STR ADJUSTMENT.</param>
    ''' <param name="p_sngCIRCUNFERENCE">The P_SNG CIRCUNFERENCE.</param>
    ''' <param name="p_sngNOMINALDIAMETER">The P_SNG NOMINALDIAMETER.</param>
    ''' <param name="p_sngNOMINALWIDTH">The P_SNG NOMINALWIDTH.</param>
    ''' <param name="p_strNOMINALWIDTHPASSFAIL">The P_STR NOMINALWIDTHPASSFAIL.</param>
    ''' <param name="p_sngNOMINALWIDTHDIFERENCE">The P_SNG NOMINALWIDTHDIFERENCE.</param>
    ''' <param name="p_sngNOMINALWIDTHTOLERANCE">The P_SNG NOMINALWIDTHTOLERANCE.</param>
    ''' <param name="p_sngMAXOVERALLDIAMETER">The P_SNG MAXOVERALLDIAMETER.</param>
    ''' <param name="p_sngMINOVERALLDIAMETER">The P_SNG MINOVERALLDIAMETER.</param>
    ''' <param name="p_strOVERALLWIDTHPASSFAIL">The P_STR OVERALLWIDTHPASSFAIL.</param>
    ''' <param name="p_strOVERALLDIAMETERPASSFAIL">The P_STR OVERALLDIAMETERPASSFAIL.</param>
    ''' <param name="p_sngDIAMETERDIFERENCE">The P_SNG DIAMETERDIFERENCE.</param>
    ''' <param name="p_sngDIAMETERTOLERANCE">The P_SNG DIAMETERTOLERANCE.</param>
    ''' <param name="p_strTEMPRESISTANCEGRADING">The P_SNG TEMPRESISTANCEGRADING.</param>
    ''' <param name="p_sngTENSILESTRENGHT1">The P_SNG TENSILESTRENGH t1.</param>
    ''' <param name="p_sngTENSILESTRENGHT2">The P_SNG TENSILESTRENGH t2.</param>
    ''' <param name="p_sngELONGATION1">The P_SNG ELONGATIO n1.</param>
    ''' <param name="p_sngELONGATION2">The P_SNG ELONGATIO n2.</param>
    ''' <param name="p_sngTENSILESTRENGHTAFTERAGE1">The P_SNG TENSILESTRENGHTAFTERAG e1.</param>
    ''' <param name="p_sngTENSILESTRENGHTAFTERAGE2">The P_SNG TENSILESTRENGHTAFTERAG e2.</param>
    ''' <param name="p_strOperatorName">Name of the P_STR operator.</param>
    ''' <param name="p_intCertificateID">Certificate Id.</param>
    ''' <param name="p_strMatlNum">SAP Material Number.</param>
    ''' <param name="p_strOperation">Opearion number Id.</param>
    ''' <param name="p_strGTSpecMeasure">GT Spec.</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY.</param>
    ''' <returns></returns>
    Public Function SaveMeasurement(ByVal p_strPROJECTNUMBER As String, _
                                    ByVal p_sngTIRENUMBER As Single, _
                                    ByVal p_strTESTSPEC As String, _
                                    ByVal p_dteCOMPLETIONDATE As DateTime, _
                                    ByVal p_sngINFLATIONPRESSURE As Single, _
                                    ByVal p_strMOLDDESIGN As String, _
                                    ByVal p_sngRIMWIDTH As Single, _
                                    ByVal p_strDOTSERIALNUMBER As String, _
                                    ByVal p_sngDIAMETER As Single, _
                                    ByVal p_sngAVGSECTIONWIDTH As Single, _
                                    ByVal p_sngAVGOVERALLWIDTH As Single, _
                                    ByVal p_sngMAXOVERALLWIDTH As Single, _
                                    ByVal p_sngSIZEFACTOR As Single, _
                                    ByVal p_dteMOUNTTIME As DateTime, _
                                    ByVal p_sngMOUNTTEMP As Single, _
                                    ByVal p_intSKUID As Integer, _
                                    ByVal p_intCertType As Integer, _
                                    ByVal p_strCERTIFICATENUMBER As String, _
                                    ByRef p_intMEASUREID As Integer, _
                                    ByVal p_dteSerialDate As DateTime, _
                                    ByVal p_dteEndTime As DateTime, _
                                    ByVal p_sngActSizeFactor As Single, _
                                    ByVal p_srtSTARTINFLATIONPRESSURE As Short, _
                                    ByVal p_srtENDINFLATIONPRESSURE As Short, _
                                    ByVal p_strADJUSTMENT As String, _
                                    ByVal p_sngCIRCUNFERENCE As Single, _
                                    ByVal p_sngNOMINALDIAMETER As Single, _
                                    ByVal p_sngNOMINALWIDTH As Single, _
                                    ByVal p_strNOMINALWIDTHPASSFAIL As String, _
                                    ByVal p_sngNOMINALWIDTHDIFERENCE As Single, _
                                    ByVal p_sngNOMINALWIDTHTOLERANCE As Single, _
                                    ByVal p_sngMAXOVERALLDIAMETER As Single, _
                                    ByVal p_sngMINOVERALLDIAMETER As Single, _
                                    ByVal p_strOVERALLWIDTHPASSFAIL As String, _
                                    ByVal p_strOVERALLDIAMETERPASSFAIL As String, _
                                    ByVal p_sngDIAMETERDIFERENCE As Single, _
                                    ByVal p_sngDIAMETERTOLERANCE As Single, _
                                    ByVal p_strTEMPRESISTANCEGRADING As String, _
                                    ByVal p_sngTENSILESTRENGHT1 As Single, _
                                    ByVal p_sngTENSILESTRENGHT2 As Single, _
                                    ByVal p_sngELONGATION1 As Single, _
                                    ByVal p_sngELONGATION2 As Single, _
                                    ByVal p_sngTENSILESTRENGHTAFTERAGE1 As Single, _
                                    ByVal p_sngTENSILESTRENGHTAFTERAGE2 As Single, _
                                    ByVal p_strOperatorName As String, _
                                    ByVal p_intCertificateID As Integer, _
                                    ByVal p_strMatlNum As String, _
                                    ByVal p_strOperation As String, _
                                    ByVal p_strGTSpecMeasure As String, _
                                    ByVal p_strMFGWWYY As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

        Dim oraCmd As New OracleCommand

        Try
            'pi_MEASUREID out Number,
            ParametersHelper.AddParametersToCommand("pi_MEASUREID", ParameterDirection.Output, OracleType.Number, p_intMEASUREID, oraCmd)
            'pi_CertificateID  in number
            ParametersHelper.AddParametersToCommand("pi_CertificateID", ParameterDirection.Input, OracleType.Number, p_intCertificateID, oraCmd)
            'ps_PROJECTNUMBER in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_PROJECTNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            'pi_TIRENUMBER in Number,
            ParametersHelper.AddParametersToCommand("pi_TIRENUMBER", ParameterDirection.Input, OracleType.Number, p_sngTIRENUMBER, oraCmd)
            'ps_TESTSPEC in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            'pd_COMPLETIONDATE in TimeStamp,
            ParametersHelper.AddParametersToCommand("pd_COMPLETIONDATE", ParameterDirection.Input, OracleType.DateTime, p_dteCOMPLETIONDATE, oraCmd)
            'pi_INFLATIONPRESSURE in Number,
            ParametersHelper.AddParametersToCommand("pi_INFLATIONPRESSURE", ParameterDirection.Input, OracleType.Number, p_sngINFLATIONPRESSURE, oraCmd)
            'ps_MOLDDESIGN in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_MOLDDESIGN", ParameterDirection.Input, OracleType.VarChar, p_strMOLDDESIGN, oraCmd)
            'pi_RIMWIDTH in Number,
            ParametersHelper.AddParametersToCommand("pi_RIMWIDTH", ParameterDirection.Input, OracleType.Number, p_sngRIMWIDTH, oraCmd)
            'ps_DOTSERIALNUMBER in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_DOTSERIALNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            'pi_DIAMETER in Number,
            ParametersHelper.AddParametersToCommand("pi_DIAMETER", ParameterDirection.Input, OracleType.Number, p_sngDIAMETER, oraCmd)
            'pi_AVGSECTIONWIDTH in Number,
            ParametersHelper.AddParametersToCommand("pi_AVGSECTIONWIDTH", ParameterDirection.Input, OracleType.Number, p_sngAVGSECTIONWIDTH, oraCmd)
            'pi_AVGOVERALLWIDTH in Number,
            ParametersHelper.AddParametersToCommand("pi_AVGOVERALLWIDTH", ParameterDirection.Input, OracleType.Number, p_sngAVGOVERALLWIDTH, oraCmd)
            'pi_MAXOVERALLWIDTH in Number,
            ParametersHelper.AddParametersToCommand("pi_MAXOVERALLWIDTH", ParameterDirection.Input, OracleType.Number, p_sngMAXOVERALLWIDTH, oraCmd)
            'pi_SIZEFACTOR in Number,
            ParametersHelper.AddParametersToCommand("pi_SIZEFACTOR", ParameterDirection.Input, OracleType.Number, p_sngSIZEFACTOR, oraCmd)
            'pd_MOUNTTIME in TimeStamp,
            ParametersHelper.AddParametersToCommand("pd_MOUNTTIME", ParameterDirection.Input, OracleType.DateTime, p_dteMOUNTTIME, oraCmd)
            'pi_MOUNTTEMP in Number,
            ParametersHelper.AddParametersToCommand("pi_MOUNTTEMP", ParameterDirection.Input, OracleType.Number, p_sngMOUNTTEMP, oraCmd)
            'pd_SERIALDATE in TimeStamp,
            ParametersHelper.AddParametersToCommand("pd_SERIALDATE", ParameterDirection.Input, OracleType.DateTime, p_dteSerialDate, oraCmd)
            'pd_ENDTIME in TimeStamp,
            ParametersHelper.AddParametersToCommand("pd_ENDTIME", ParameterDirection.Input, OracleType.DateTime, p_dteEndTime, oraCmd)
            'pi_ACTSIZEFACTOR in Number,
            ParametersHelper.AddParametersToCommand("pi_ACTSIZEFACTOR", ParameterDirection.Input, OracleType.Number, p_sngActSizeFactor, oraCmd)
            'pi_CERTIFICATIONTYPEID in Number,
            ParametersHelper.AddParametersToCommand("pi_CERTIFICATIONTYPEID", ParameterDirection.Input, OracleType.Number, p_intCertType, oraCmd)
            ''ps_CERTIFICATENUMBER in Varchar2,
            'ParametersHelper.AddParametersToCommand("ps_CERTIFICATENUMBER", ParameterDirection.Input, OracleType.VarChar, p_strCERTIFICATENUMBER, oraCmd)
            'pi_STARTINFLATIONPRESSURE in Number,
            ParametersHelper.AddParametersToCommand("pi_STARTINFLATIONPRESSURE", ParameterDirection.Input, OracleType.Number, p_srtSTARTINFLATIONPRESSURE, oraCmd)
            'pi_ENDINFLATIONPRESSURE in Number,
            ParametersHelper.AddParametersToCommand("pi_ENDINFLATIONPRESSURE", ParameterDirection.Input, OracleType.Number, p_srtENDINFLATIONPRESSURE, oraCmd)
            'ps_ADJUSTMENT in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_ADJUSTMENT", ParameterDirection.Input, OracleType.VarChar, p_strADJUSTMENT, oraCmd)
            'pi_CIRCUNFERENCE in Number,
            ParametersHelper.AddParametersToCommand("pi_CIRCUNFERENCE", ParameterDirection.Input, OracleType.Number, p_sngCIRCUNFERENCE, oraCmd)
            'pi_NOMINALDIAMETER in Number,
            ParametersHelper.AddParametersToCommand("pi_NOMINALDIAMETER", ParameterDirection.Input, OracleType.Number, p_sngNOMINALDIAMETER, oraCmd)
            'pi_NOMINALWIDTH in Number,
            ParametersHelper.AddParametersToCommand("pi_NOMINALWIDTH", ParameterDirection.Input, OracleType.Number, p_sngNOMINALWIDTH, oraCmd)
            'ps_NOMINALWIDTHPASSFAIL in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_NOMINALWIDTHPASSFAIL", ParameterDirection.Input, OracleType.VarChar, p_strNOMINALWIDTHPASSFAIL, oraCmd)
            'pi_NOMINALWIDTHDIFERENCE in Number,
            ParametersHelper.AddParametersToCommand("pi_NOMINALWIDTHDIFERENCE", ParameterDirection.Input, OracleType.Number, p_sngNOMINALWIDTHDIFERENCE, oraCmd)
            'pi_NOMINALWIDTHTOLERANCE in Number,
            ParametersHelper.AddParametersToCommand("pi_NOMINALWIDTHTOLERANCE", ParameterDirection.Input, OracleType.Number, p_sngNOMINALWIDTHTOLERANCE, oraCmd)
            'pi_MAXOVERALLDIAMETER in Number,
            ParametersHelper.AddParametersToCommand("pi_MAXOVERALLDIAMETER", ParameterDirection.Input, OracleType.Number, p_sngMAXOVERALLDIAMETER, oraCmd)
            'pi_MINOVERALLDIAMETER in Number,
            ParametersHelper.AddParametersToCommand("pi_MINOVERALLDIAMETER", ParameterDirection.Input, OracleType.Number, p_sngMINOVERALLDIAMETER, oraCmd)
            'ps_OVERALLWIDTHPASSFAIL in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_OVERALLWIDTHPASSFAIL", ParameterDirection.Input, OracleType.VarChar, p_strOVERALLWIDTHPASSFAIL, oraCmd)
            'ps_OVERALLDIAMETERPASSFAIL in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_OVERALLDIAMETERPASSFAIL", ParameterDirection.Input, OracleType.VarChar, p_strOVERALLDIAMETERPASSFAIL, oraCmd)
            'pi_DIAMETERDIFERENCE in Number,
            ParametersHelper.AddParametersToCommand("pi_DIAMETERDIFERENCE", ParameterDirection.Input, OracleType.Number, p_sngDIAMETERDIFERENCE, oraCmd)
            'pi_DIAMETERTOLERANCE in Number,
            ParametersHelper.AddParametersToCommand("pi_DIAMETERTOLERANCE", ParameterDirection.Input, OracleType.Number, p_sngDIAMETERTOLERANCE, oraCmd)
            'pi_TEMPRESISTANCEGRADING in Number,
            ParametersHelper.AddParametersToCommand("pi_TEMPRESISTANCEGRADING", ParameterDirection.Input, OracleType.VarChar, p_strTEMPRESISTANCEGRADING, oraCmd)
            'pi_TENSILESTRENGHT1 in Number,
            ParametersHelper.AddParametersToCommand("pi_TENSILESTRENGHT1", ParameterDirection.Input, OracleType.Number, p_sngTENSILESTRENGHT1, oraCmd)
            'pi_TENSILESTRENGHT2 in Number,
            ParametersHelper.AddParametersToCommand("pi_TENSILESTRENGHT2", ParameterDirection.Input, OracleType.Number, p_sngTENSILESTRENGHT2, oraCmd)
            'pi_ELONGATION1 in Number,
            ParametersHelper.AddParametersToCommand("pi_ELONGATION1", ParameterDirection.Input, OracleType.Number, p_sngELONGATION1, oraCmd)
            'pi_ELONGATION2 in Number,
            ParametersHelper.AddParametersToCommand("pi_ELONGATION2", ParameterDirection.Input, OracleType.Number, p_sngELONGATION2, oraCmd)
            'pi_TENSILESTRENGHTAFTERAGE1 in Number,
            ParametersHelper.AddParametersToCommand("pi_TENSILESTRENGHTAFTERAGE1", ParameterDirection.Input, OracleType.Number, p_sngTENSILESTRENGHTAFTERAGE1, oraCmd)
            'pi_TENSILESTRENGHTAFTERAGE2 in Number,
            ParametersHelper.AddParametersToCommand("pi_TENSILESTRENGHTAFTERAGE2", ParameterDirection.Input, OracleType.Number, p_sngTENSILESTRENGHTAFTERAGE2, oraCmd)
            'ps_OperatorName in varchar2
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            'ps_Matl_Num
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            'ps_Operation 
            ParametersHelper.AddParametersToCommand("ps_Operation", ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_GTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strGTSpecMeasure, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_MFGWWYY", ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)


            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.Measure_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()

            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Mesure Id to be inserted on the detail table
            If oraCmd.Parameters.Item("pi_MEASUREID").Value.Equals(DBNull.Value) Then
                p_intMEASUREID = 0
            Else
                p_intMEASUREID = oraCmd.Parameters.Item("pi_MEASUREID").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()

        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' save the Measurements detail .
    ''' </summary>
    ''' <param name="p_sngSectionWidth"></param>
    ''' <param name="p_sngOVERALLWIDTH"></param>
    ''' <param name="p_iMEASUREID"></param>
    ''' <param name="p_sngITERATION"></param>
    ''' <param name="p_strUserName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MeasurementDetail_Save(ByVal p_sngSectionWidth As Single, _
                                            ByVal p_sngOVERALLWIDTH As Single, _
                                            ByVal p_iMEASUREID As Integer, _
                                            ByVal p_sngITERATION As Single, _
                                            ByVal p_strUserName As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try

            ParametersHelper.AddParametersToCommand("pi_SECTIONWIDTH", ParameterDirection.Input, OracleType.Number, p_sngSectionWidth, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_OVERALLWIDTH", ParameterDirection.Input, OracleType.Number, p_sngOVERALLWIDTH, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_MEASUREID", ParameterDirection.Input, OracleType.Number, p_iMEASUREID, oraCmd)
            'PI_ITERATION IN NUMBER
            ParametersHelper.AddParametersToCommand("PI_ITERATION", ParameterDirection.Input, OracleType.Number, p_sngITERATION, oraCmd)
            'ps_UserName   in Varchar2
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.MeasureDetail_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return enumSaveResult

    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Save the plunger part of the test results
    ''' </summary>
    ''' <param name="p_strPROJECTNUMBER"></param>
    ''' <param name="p_sngTIRENUMBER"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_dteCOMPLETIONDATE"></param>
    ''' <param name="p_strDOTSERIALNUMBER"></param>
    ''' <param name="p_sngAVGBREAKINGENERGY"></param>
    ''' <param name="p_strPASSYN"></param>
    ''' <param name="p_intSKUID"></param>
    ''' <param name="p_intCertType"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_intPLUNGERID"></param>
    ''' <param name="p_dteSerialDate"></param>
    ''' <param name="p_sngMinPlunger"></param>
    ''' <param name="p_strUserName"></param>
    ''' <param name="p_intCertificateNumberID"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strOperation"></param>
    ''' <param name="p_strGTSpecPlunger">GT Spec.</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePlunger(ByVal p_strPROJECTNUMBER As String, _
                                ByVal p_sngTIRENUMBER As Single, _
                                ByVal p_strTESTSPEC As String, _
                                ByVal p_dteCOMPLETIONDATE As DateTime, _
                                ByVal p_strDOTSERIALNUMBER As String, _
                                ByVal p_sngAVGBREAKINGENERGY As Single, _
                                ByVal p_strPASSYN As String, _
                                ByVal p_intSKUID As Integer, _
                                ByVal p_intCertType As Integer, _
                                ByVal p_strCERTIFICATENUMBER As String, _
                                ByRef p_intPLUNGERID As Integer, _
                                ByVal p_dteSerialDate As DateTime, _
                                ByVal p_sngMinPlunger As Single, _
                                ByVal p_strUserName As String, _
                                ByVal p_intCertificateNumberID As Integer, _
                                ByVal p_strMatlNum As String, _
                                ByVal p_strOperation As String, _
                                ByVal p_strGTSpecPlunger As String, _
                                ByVal p_strMFGWWYY As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try
            'pi_PLUNGERID out Number,
            ParametersHelper.AddParametersToCommand("pi_PLUNGERID", ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            'ps_PROJECTNUMBER in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_PROJECTNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            'pi_TIRENUMBER in Number,
            ParametersHelper.AddParametersToCommand("pi_TIRENUMBER", ParameterDirection.Input, OracleType.Number, p_sngTIRENUMBER, oraCmd)
            'ps_TESTSPEC in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            'pd_COMPLETIONDATE in TimeStamp,
            ParametersHelper.AddParametersToCommand("pd_COMPLETIONDATE", ParameterDirection.Input, OracleType.DateTime, p_dteCOMPLETIONDATE, oraCmd)
            'ps_DOTSERIALNUMBER in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_DOTSERIALNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            'pi_AVGBREAKINGENERGY in Number,
            ParametersHelper.AddParametersToCommand("pi_AVGBREAKINGENERGY", ParameterDirection.Input, OracleType.VarChar, p_sngAVGBREAKINGENERGY, oraCmd)
            'ps_PASSYN in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_PASSYN", ParameterDirection.Input, OracleType.VarChar, p_strPASSYN, oraCmd)
            'pi_CERTIFICATIONTYPEID in Number,
            ParametersHelper.AddParametersToCommand("pi_CERTIFICATIONTYPEID", ParameterDirection.Input, OracleType.Number, p_intCertType, oraCmd)
            ''ps_CERTIFICATENUMBER in Varchar2,
            'ParametersHelper.AddParametersToCommand("ps_CERTIFICATENUMBER", ParameterDirection.Input, OracleType.VarChar, p_strCERTIFICATENUMBER, oraCmd)
            'pd_SERIALDATE in TimeStamp,
            ParametersHelper.AddParametersToCommand("pd_SERIALDATE", ParameterDirection.Input, OracleType.DateTime, p_dteSerialDate, oraCmd)
            'pi_MINPLUNGER in Number,
            ParametersHelper.AddParametersToCommand("pi_MINPLUNGER", ParameterDirection.Input, OracleType.Number, p_sngMinPlunger, oraCmd)
            'ps_OperatorName in varchar2 
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)
            ' pi_CertificateID in Number
            ParametersHelper.AddParametersToCommand("pi_CertificateID", ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)
            'ps_Matl_Num
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            'ps_Operation 
            ParametersHelper.AddParametersToCommand("ps_Operation", ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_GTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strGTSpecPlunger, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_MFGWWYY", ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.PLUNGER_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If


            'Gets the Mesure Id to be inserted on the detail table
            If oraCmd.Parameters.Item("PI_PLUNGERID").Value.Equals(DBNull.Value) Then
                p_intPLUNGERID = 0
            Else
                p_intPLUNGERID = oraCmd.Parameters.Item("PI_PLUNGERID").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()

        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Saves the plunger detail.
    ''' </summary>
    ''' <param name="p_sngBREAKINGENERGY">The  BREAKINGENERGY.</param>
    ''' <param name="p_intPlungerID">The  plunger ID.</param>
    ''' <param name="p_sngIteration">The  iteration.</param>
    ''' <param name="p_strUserName">Name of the  user.</param>
    ''' <returns></returns>
    Public Function SavePlungerDetail(ByVal p_sngBREAKINGENERGY As Single, _
                                      ByVal p_intPlungerID As Integer, _
                                      ByVal p_sngIteration As Single, _
                                      ByVal p_strUserName As String) As NameAid.SaveResult

        '        Dim blnSaved As Boolean = False
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'PI_BREAKINGENERGY  IN NUMBER,
            ParametersHelper.AddParametersToCommand("PI_BREAKINGENERGY", ParameterDirection.Input, OracleType.Number, p_sngBREAKINGENERGY, oraCmd)
            'PI_PLUNGERID  IN NUMBER
            ParametersHelper.AddParametersToCommand("PI_PLUNGERID", ParameterDirection.Input, OracleType.Number, p_intPlungerID, oraCmd)
            ParametersHelper.AddParametersToCommand("PI_ITERATION", ParameterDirection.Input, OracleType.Number, p_sngIteration, oraCmd)
            'ps_OperatorName in varchar2
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.PLUNGERDETAIL_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()

            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return enumSaveResult

    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Save TreadWear header table data
    ''' </summary>
    ''' <param name="p_strPROJECTNUMBER"></param>
    ''' <param name="p_sngTIRENUMBER"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_dteCOMPLETIONDATE"></param>
    ''' <param name="p_strDOTSERIALNUMBER"></param>
    ''' <param name="p_sngLOWESTWEARBAR"></param>
    ''' <param name="p_strPassyn"></param>
    ''' <param name="p_intSKUID"></param>
    ''' <param name="p_intCertType"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_intTREADWEARID"></param>
    ''' <param name="p_dteSERIALDATE"></param>
    ''' <param name="p_strOperatorName"></param>
    ''' <param name="p_sngINDICATORSREQUIREMENT"></param>
    ''' <param name="p_intCertificateID"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strOperation"></param>
    ''' <param name="p_strGTSpecTreadWear">GT Spec.</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveTreadWear(ByVal p_strPROJECTNUMBER As String, _
                                  ByVal p_sngTIRENUMBER As Single, _
                                  ByVal p_strTESTSPEC As String, _
                                  ByVal p_dteCOMPLETIONDATE As DateTime, _
                                  ByVal p_strDOTSERIALNUMBER As String, _
                                  ByVal p_sngLOWESTWEARBAR As Single, _
                                  ByVal p_strPassyn As String, _
                                  ByVal p_intSKUID As Integer, _
                                  ByVal p_intCertType As Integer, _
                                  ByVal p_strCERTIFICATENUMBER As String, _
                                  ByRef p_intTREADWEARID As Integer, _
                                  ByVal p_dteSERIALDATE As DateTime, _
                                  ByVal p_strOperatorName As String, _
                                  ByVal p_sngINDICATORSREQUIREMENT As Single, _
                                  ByVal p_intCertificateID As Integer, _
                                  ByVal p_strMatlNum As String, _
                                  ByVal p_strOperation As String, _
                                  ByVal p_strGTSpecTreadWear As String, _
                                  ByVal p_strMFGWWYY As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Try

            'PI_TREADWEARID OUT NUMBER,
            ParametersHelper.AddParametersToCommand("PI_TREADWEARID", ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            'PS_PROJECTNUMBER  IN VARCHAR2,
            ParametersHelper.AddParametersToCommand("PS_PROJECTNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            'PI_TIRENUMBER IN NUMBER,
            ParametersHelper.AddParametersToCommand("PI_TIRENUMBER", ParameterDirection.Input, OracleType.Number, p_sngTIRENUMBER, oraCmd)
            'PS_TESTSPEC  IN VARCHAR2,
            ParametersHelper.AddParametersToCommand("PS_TESTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            'PD_COMPLETIONDATE IN DATE,
            ParametersHelper.AddParametersToCommand("PD_COMPLETIONDATE", ParameterDirection.Input, OracleType.DateTime, p_dteCOMPLETIONDATE, oraCmd)
            'PS_DOTSERIALNUMBER  IN VARCHAR2,
            ParametersHelper.AddParametersToCommand("PS_DOTSERIALNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            'PI_LOWESTWEARBAR IN NUMBER,
            ParametersHelper.AddParametersToCommand("PI_LOWESTWEARBAR", ParameterDirection.Input, OracleType.Number, p_sngLOWESTWEARBAR, oraCmd)
            'PS_PASSYN  IN VARCHAR2,
            ParametersHelper.AddParametersToCommand("PS_PASSYN", ParameterDirection.Input, OracleType.VarChar, p_strPassyn, oraCmd)
            'pi_CertificationTypeID in number,
            ParametersHelper.AddParametersToCommand("pi_CertificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertType, oraCmd)
            'PD_SERIALDATE IN DATE,
            ParametersHelper.AddParametersToCommand("PD_SERIALDATE", ParameterDirection.Input, OracleType.DateTime, p_dteSERIALDATE, oraCmd)
            'ps_OperatorName in varchar2,
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            'pi_INDICATORSREQUIREMENT in number
            ParametersHelper.AddParametersToCommand("pi_INDICATORSREQUIREMENT", ParameterDirection.Input, OracleType.Number, p_sngINDICATORSREQUIREMENT, oraCmd)
            'pi_CertificateID in Number
            ParametersHelper.AddParametersToCommand("pi_CertificateID", ParameterDirection.Input, OracleType.Number, p_intCertificateID, oraCmd)
            'ps_Matl_Num
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            'ps_Operation 
            ParametersHelper.AddParametersToCommand("ps_Operation", ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_GTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strGTSpecTreadWear, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_MFGWWYY", ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.TREADWEAR_SAVE"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()

            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Mesure Id to be inserted on the detail table
            If oraCmd.Parameters.Item("PI_TREADWEARID").Value.Equals(DBNull.Value) Then
                p_intTREADWEARID = 0
            Else
                p_intTREADWEARID = oraCmd.Parameters.Item("PI_TREADWEARID").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()

        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Saves the tread wear detail.
    ''' </summary>
    ''' <param name="details"></param>
    ''' <param name="p_intTREADWEARID"></param>
    ''' <param name="p_strOperatorName"></param>
    ''' <param name="p_intITERATION"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NewSaveTreadWearDetail(ByVal details As List(Of TreadwearDetail), _
                                           ByVal p_intTREADWEARID As Integer, _
                                           ByVal p_strOperatorName As String, _
                                           ByVal p_intITERATION As Integer) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Dim rowsaffected As Integer = 0
        Try
            Connect()
            oraCmd.Connection = Connection
            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.TREADWEARDETAIL_SAVE"
            For Each detail As TreadwearDetail In details

                ParametersHelper.AddParametersToCommand("PI_WEARBARHEIGHT", ParameterDirection.Input, OracleType.Number, detail.WearBarHeight, oraCmd)
                ParametersHelper.AddParametersToCommand("PI_TREADWEARID", ParameterDirection.Input, OracleType.Number, p_intTREADWEARID, oraCmd)
                'PI_ITERATION
                ParametersHelper.AddParametersToCommand("PI_ITERATION", ParameterDirection.Input, OracleType.Number, p_intITERATION, oraCmd)
                'ps_OperatorName in varchar2,
                ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

                rowsaffected = oraCmd.ExecuteNonQuery()
                oraCmd.Parameters.Clear()
            Next

            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Saves the tread wear detail.
    ''' </summary>
    ''' <param name="p_sngwearbarheight">The wearbar height.</param>
    ''' <param name="p_intTREADWEARID">The  TREADWEARID.</param>
    ''' <param name="p_sngIteration">The  iteration.</param>
    ''' <param name="p_strOperatorName">Name of the operator name.</param>
    ''' <returns></returns>
    Public Function SaveTreadWearDetail(ByVal p_sngwearbarheight As Single, _
                                        ByVal p_intTREADWEARID As Integer, _
                                        ByVal p_sngIteration As Single, _
                                        ByVal p_strOperatorName As String) As NameAid.SaveResult


        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Dim rowsaffected As Integer = 0
        Try
            Connect()
            oraCmd.Connection = Connection
            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.TREADWEARDETAIL_SAVE"

            ParametersHelper.AddParametersToCommand("PI_WEARBARHEIGHT", ParameterDirection.Input, OracleType.Number, p_sngwearbarheight, oraCmd)
            ParametersHelper.AddParametersToCommand("PI_TREADWEARID", ParameterDirection.Input, OracleType.Number, p_intTREADWEARID, oraCmd)
            ParametersHelper.AddParametersToCommand("PI_ITERATION", ParameterDirection.Input, OracleType.Number, p_sngIteration, oraCmd)
            'ps_OperatorName in varchar2,
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)


            rowsaffected = oraCmd.ExecuteNonQuery()

            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return enumSaveResult

    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Saves the bead unseat.
    ''' </summary>
    ''' <param name="p_strPROJECTNUMBER">The  PROJECTNUMBER.</param>
    ''' <param name="p_sngTIRENUMBER">The  TIRENUMBER.</param>
    ''' <param name="p_strTESTSPEC">The  TESTSPEC.</param>
    ''' <param name="p_dteCOMPLETIONDATE">The  COMPLETIONDATE.</param>
    ''' <param name="p_strDOTSERIALNUMBER">The  DOTSERIALNUMBER.</param>
    ''' <param name="p_sngLOWESTUNSEATVALUE">The  LOWESTUNSEATVALUE.</param>
    ''' <param name="p_strPassyn">The  passyn.</param>
    ''' <param name="p_intCertType">Type of the p_enu cert.</param>
    ''' <param name="p_strCERTIFICATENUMBER">The  CERTIFICATENUMBER.</param>
    ''' <param name="p_intBeadUnseatID">The  bead unseat ID.</param>
    ''' <param name="p_dteSerialDate">The  serial date.</param>
    ''' <param name="p_sngMINBEADUNSEAT">The  MINBEADUNSEAT.</param>
    ''' <param name="p_strTESTPASSFAIL">The  TESTPASSFAIL.</param>
    ''' <param name="p_strOperatorName">Name of the operator.</param>
    ''' <param name="p_intCertificateID">Certificate Id.</param>
    ''' <param name="p_strMatlNum">SAP Material number.</param>
    ''' <param name="p_strOperation">Operation number.</param>
    ''' <returns></returns>
    Public Function SaveBeadUnseat(ByVal p_strPROJECTNUMBER As String, _
                                   ByVal p_sngTIRENUMBER As Single, _
                                   ByVal p_strTESTSPEC As String, _
                                   ByVal p_dteCOMPLETIONDATE As DateTime, _
                                   ByVal p_strDOTSERIALNUMBER As String, _
                                   ByVal p_sngLOWESTUNSEATVALUE As Single, _
                                   ByVal p_strPassyn As String, _
                                   ByVal p_intCertType As Integer, _
                                   ByVal p_strCERTIFICATENUMBER As String, _
                                   ByRef p_intBeadUnseatID As Integer, _
                                   ByVal p_dteSerialDate As DateTime, _
                                   ByVal p_sngMINBEADUNSEAT As Single, _
                                   ByVal p_strTESTPASSFAIL As String, _
                                   ByVal p_strOperatorName As String, _
                                   ByVal p_intCertificateID As Integer, _
                                   ByVal p_strMatlNum As String, _
                                   ByVal p_strOperation As String, _
                                   ByVal p_strGTSpecBeadUnSeat As String, _
                                   ByVal p_strMFGWWYY As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Try
            'pi_BEADUNSEATID out Number,
            ParametersHelper.AddParametersToCommand("pi_BEADUNSEATID", ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            'ps_PROJECTNUMBER in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_PROJECTNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            'pi_TIRENUMBER in Number,
            ParametersHelper.AddParametersToCommand("pi_TIRENUMBER", ParameterDirection.Input, OracleType.Number, p_sngTIRENUMBER, oraCmd)
            'ps_TESTSPEC in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            'pd_COMPLETIONDATE in TimeStamp,
            ParametersHelper.AddParametersToCommand("pd_COMPLETIONDATE", ParameterDirection.Input, OracleType.DateTime, p_dteCOMPLETIONDATE, oraCmd)
            'ps_DOTSERIALNUMBER in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_DOTSERIALNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            'pi_LOWESTUNSEATVALUE in Number,
            ParametersHelper.AddParametersToCommand("pi_LOWESTUNSEATVALUE", ParameterDirection.Input, OracleType.Number, p_sngLOWESTUNSEATVALUE, oraCmd)
            'ps_PASSYN in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_PASSYN", ParameterDirection.Input, OracleType.VarChar, p_strPassyn, oraCmd)
            'pi_CERTIFICATIONTYPEID in Number,
            ParametersHelper.AddParametersToCommand("pi_CERTIFICATIONTYPEID", ParameterDirection.Input, OracleType.Number, p_intCertType, oraCmd)
            'pd_SERIALDATE in TimeStamp,
            ParametersHelper.AddParametersToCommand("pd_SERIALDATE", ParameterDirection.Input, OracleType.DateTime, p_dteSerialDate, oraCmd)
            'pi_MINBEADUNSEAT in Number, 
            ParametersHelper.AddParametersToCommand("pi_MINBEADUNSEAT", ParameterDirection.Input, OracleType.Number, p_sngMINBEADUNSEAT, oraCmd)
            'ps_TESTPASSFAIL in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTPASSFAIL", ParameterDirection.Input, OracleType.VarChar, p_strTESTPASSFAIL, oraCmd)
            'ps_OperatorName   in varchar2
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            'pi_CertificateID  in number
            ParametersHelper.AddParametersToCommand("pi_CertificateID", ParameterDirection.Input, OracleType.Number, p_intCertificateID, oraCmd)
            'ps_Matl_Num
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            'ps_Operation 
            ParametersHelper.AddParametersToCommand("ps_Operation", ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_GTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strGTSpecBeadUnSeat, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_MFGWWYY", ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.BeadUnseat_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Mesure Id to be inserted on the detail table
            If oraCmd.Parameters.Item("pi_BEADUNSEATID").Value.Equals(DBNull.Value) Then
                p_intBeadUnseatID = 0
            Else
                p_intBeadUnseatID = oraCmd.Parameters.Item("pi_BEADUNSEATID").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()

        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Saves the bead unseat detail.
    ''' </summary>
    ''' <param name="p_intBEADUNSEATID">The p_int BEADUNSEATID.</param>
    ''' <param name="p_sngUNSEATFORCE">The P_SNG UNSEATFORCE.</param>
    ''' <param name="p_sngIteration">The P_SNG iteration.</param>
    ''' <param name="p_strOperatorName">Name of the P_STR operator.</param>
    ''' <returns></returns>
    Public Function SaveBeadUnseatDetail(ByVal p_intBEADUNSEATID As Integer, _
                                         ByVal p_sngUNSEATFORCE As Single, _
                                         ByVal p_sngIteration As Single, _
                                         ByVal p_strOperatorName As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try
            'pi_BEADUNSEATID in NUMBER, 
            ParametersHelper.AddParametersToCommand("pi_BEADUNSEATID", ParameterDirection.Input, OracleType.Number, p_intBEADUNSEATID, oraCmd)
            'pi_UNSEATFORCE in NUMBER
            ParametersHelper.AddParametersToCommand("pi_UNSEATFORCE", ParameterDirection.Input, OracleType.Number, p_sngUNSEATFORCE, oraCmd)
            'PI_ITERATION IN NUMBER
            ParametersHelper.AddParametersToCommand("PI_ITERATION", ParameterDirection.Input, OracleType.Number, p_sngIteration, oraCmd)
            'ps_OperatorName   in varchar2
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.BeadUnseatDetail_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Saves the endurance.
    ''' </summary>
    ''' <param name="p_intENDURANCEID">The p_int ENDURANCEID.</param>
    ''' <param name="p_strProjectNumber">The P_STR project number.</param>
    ''' <param name="p_intTireNumber">The p_int tire number.</param>
    ''' <param name="p_strTESTSPEC">The P_STR TESTSPEC.</param>
    ''' <param name="p_dteCOMPLETIONDATE">The p_dte COMPLETIONDATE.</param>
    ''' <param name="p_strDOTSERIALNUMBER">The P_STR DOTSERIALNUMBER.</param>
    ''' <param name="p_dtePRECONDSTARTDATE">The p_dte PRECONDSTARTDATE.</param>
    ''' <param name="p_sngPRECONDSTARTTEMP">The P_SNG PRECONDSTARTTEMP.</param>
    ''' <param name="p_sngRIMDIAMETER">The P_SNG RIMDIAMETER.</param>
    ''' <param name="p_sngRIMWIDTH">The P_SNG RIMWIDTH.</param>
    ''' <param name="p_dtePRECONDENDDATE">The p_dte PRECONDENDDATE.</param>
    ''' <param name="p_intPRECONDENDTEMP">The p_int PRECONDENDTEMP.</param>
    ''' <param name="p_intINFLATIONPRESSURE">The p_int INFLATIONPRESSURE.</param>
    ''' <param name="p_sngBEFOREDIAMETER">The P_SNG BEFOREDIAMETER.</param>
    ''' <param name="p_sngAFTERDIAMETER">The P_SNG AFTERDIAMETER.</param>
    ''' <param name="p_intBEFOREINFLATION">The p_int BEFOREINFLATION.</param>
    ''' <param name="p_intAFTERINFLATION">The p_int AFTERINFLATION.</param>
    ''' <param name="p_intWHEELPOSITION">The p_int WHEELPOSITION.</param>
    ''' <param name="p_intWHEELNUMBER">The p_int WHEELNUMBER.</param>
    ''' <param name="p_intFINALTEMP">The p_int FINALTEMP.</param>
    ''' <param name="p_sngFINALDISTANCE">The P_SNG FINALDISTANCE.</param>
    ''' <param name="p_intFINALINFLATION">The p_int FINALINFLATION.</param>
    ''' <param name="p_dtePOSTCONDSTARTDATE">The p_dte POSTCONDSTARTDATE.</param>
    ''' <param name="p_dtePOSTCONDENDDATE">The p_dte POSTCONDENDDATE.</param>
    ''' <param name="p_intPOSTCONDENDTEMP">The p_int POSTCONDENDTEMP.</param>
    ''' <param name="p_strPASSYN">The P_STR PASSYN.</param>
    ''' <param name="p_intCertificationTypeID">The p_int certification type ID.</param>
    ''' <param name="p_strCERTIFICATENUMBER">The P_STR CERTIFICATENUMBER.</param>
    ''' <param name="p_dteSerialDate">The p_dte serial date.</param>
    ''' <param name="p_sngPostCondTime">The P_SNG post cond time.</param>
    ''' <param name="p_sngPreCondTime">The P_SNG pre cond time.</param>
    ''' <param name="p_sngDIAMETERTESTDRUM">The P_SNG DIAMETERTESTDRUM.</param>
    ''' <param name="p_sngPRECONDTEMP">The P_SNG PRECONDTEMP.</param>
    ''' <param name="p_sngINFLATIONPRESSUREREADJUSTED">The P_SNG INFLATIONPRESSUREREADJUSTED.</param>
    ''' <param name="p_sngCIRCUNFERENCEBEFORETEST">The P_SNG CIRCUNFERENCEBEFORETEST.</param>
    ''' <param name="p_strRESULTPASSFAIL">The P_STR RESULTPASSFAIL.</param>
    ''' <param name="p_sngENDURANCEHOURS">The P_SNG ENDURANCEHOURS.</param>
    ''' <param name="p_strPOSSIBLEFAILURESFOUND">The P_STR POSSIBLEFAILURESFOUND.</param>
    ''' <param name="p_sngCIRCUNFERENCEAFTERTEST">The P_SNG CIRCUNFERENCEAFTERTEST.</param>
    ''' <param name="p_sngOUTERDIAMETERDIFERENCE">The P_SNG OUTERDIAMETERDIFERENCE.</param>
    ''' <param name="p_sngODDIFERENCETOLERANCE">The P_SNG ODDIFERENCETOLERANCE.</param>
    ''' <param name="p_strSERIENOM">The P_STR SERIENOM.</param>
    ''' <param name="p_strFINALJUDGEMENT">The P_STR FINALJUDGEMENT.</param>
    ''' <param name="p_strAPPROVER">The P_STR APPROVER.</param>
    ''' <param name="p_strOperatorName">Name of the P_STR operator.</param>
    ''' <param name="p_intCertificateNumberID"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_sngLowInfStartInflation"></param>
    ''' <param name="p_sngLowInfEndInflation"></param>
    ''' <param name="p_intLowInfEndTemp"></param>
    ''' <param name="p_strOperation"></param>
    ''' <returns></returns>
    Public Function SaveEndurance(ByRef p_intENDURANCEID As Integer, _
                                  ByVal p_strProjectNumber As String, _
                                  ByVal p_intTireNumber As Integer, _
                                  ByVal p_strTESTSPEC As String, _
                                  ByVal p_dteCOMPLETIONDATE As DateTime, _
                                  ByVal p_strDOTSERIALNUMBER As String, _
                                  ByVal p_dtePRECONDSTARTDATE As DateTime, _
                                  ByVal p_sngPRECONDSTARTTEMP As Single, _
                                  ByVal p_sngRIMDIAMETER As Single, _
                                  ByVal p_sngRIMWIDTH As Single, _
                                  ByVal p_dtePRECONDENDDATE As DateTime, _
                                  ByVal p_intPRECONDENDTEMP As Integer, _
                                  ByVal p_intINFLATIONPRESSURE As Integer, _
                                  ByVal p_sngBEFOREDIAMETER As Single, _
                                  ByVal p_sngAFTERDIAMETER As Single, _
                                  ByVal p_intBEFOREINFLATION As Integer, _
                                  ByVal p_intAFTERINFLATION As Integer, _
                                  ByVal p_intWHEELPOSITION As Integer, _
                                  ByVal p_intWHEELNUMBER As Integer, _
                                  ByVal p_intFINALTEMP As Integer, _
                                  ByVal p_sngFINALDISTANCE As Single, _
                                  ByVal p_intFINALINFLATION As Integer, _
                                  ByVal p_dtePOSTCONDSTARTDATE As DateTime, _
                                  ByVal p_dtePOSTCONDENDDATE As DateTime, _
                                  ByVal p_intPOSTCONDENDTEMP As Integer, _
                                  ByVal p_strPASSYN As String, _
                                  ByVal p_intCertificationTypeID As Integer, _
                                  ByVal p_strCERTIFICATENUMBER As String, _
                                  ByVal p_dteSerialDate As DateTime, _
                                  ByVal p_sngPostCondTime As Single, _
                                  ByVal p_sngPreCondTime As Single, _
                                  ByVal p_sngDIAMETERTESTDRUM As Single, _
                                  ByVal p_sngPRECONDTEMP As Single, _
                                  ByVal p_sngINFLATIONPRESSUREREADJUSTED As Single, _
                                  ByVal p_sngCIRCUNFERENCEBEFORETEST As Single, _
                                  ByVal p_strRESULTPASSFAIL As String, _
                                  ByVal p_sngENDURANCEHOURS As Single, _
                                  ByVal p_strPOSSIBLEFAILURESFOUND As String, _
                                  ByVal p_sngCIRCUNFERENCEAFTERTEST As Single, _
                                  ByVal p_sngOUTERDIAMETERDIFERENCE As Single, _
                                  ByVal p_sngODDIFERENCETOLERANCE As Single, _
                                  ByVal p_strSERIENOM As String, _
                                  ByVal p_strFINALJUDGEMENT As String, _
                                  ByVal p_strAPPROVER As String, _
                                  ByVal p_strOperatorName As String, _
                                  ByVal p_intCertificateNumberID As Integer, _
                                  ByVal p_strMatlNum As String, _
                                  ByVal p_sngLowInfStartInflation As Single, _
                                  ByVal p_sngLowInfEndInflation As Single, _
                                  ByVal p_intLowInfEndTemp As Integer, _
                                  ByVal p_strOperation As String, _
                                  ByVal p_strGTSpecEndurance As String, _
                                  ByVal p_strMFGWWYY As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try

            'pi_ENDURANCEID out number,
            ParametersHelper.AddParametersToCommand("pi_ENDURANCEID", ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            'ps_PROJECTNUMBER in varchar2,
            ParametersHelper.AddParametersToCommand("ps_PROJECTNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strProjectNumber, oraCmd)
            'pi_TIRENUMBER in number,
            ParametersHelper.AddParametersToCommand("pi_TIRENUMBER", ParameterDirection.Input, OracleType.Number, p_intTireNumber, oraCmd)
            'ps_TESTSPEC in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            'pd_COMPLETIONDATE in date,
            ParametersHelper.AddParametersToCommand("pd_COMPLETIONDATE", ParameterDirection.Input, OracleType.DateTime, p_dteCOMPLETIONDATE, oraCmd)
            'ps_DOTSERIALNUMBER in varchar2,
            ParametersHelper.AddParametersToCommand("ps_DOTSERIALNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            'ps_MFGWWYY in varchar2,
            ParametersHelper.AddParametersToCommand("ps_MFGWWYY", ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)
            'pd_PRECONDSTARTDATE in date,
            ParametersHelper.AddParametersToCommand("pd_PRECONDSTARTDATE", ParameterDirection.Input, OracleType.DateTime, p_dtePRECONDSTARTDATE, oraCmd)
            'pi_PRECONDSTARTTEMP in number,
            ParametersHelper.AddParametersToCommand("pi_PRECONDSTARTTEMP", ParameterDirection.Input, OracleType.Number, p_sngPRECONDSTARTTEMP, oraCmd)
            'pi_RIMDIAMETER in number,
            ParametersHelper.AddParametersToCommand("pi_RIMDIAMETER", ParameterDirection.Input, OracleType.Number, p_sngRIMDIAMETER, oraCmd)
            'pi_RIMWIDTH in number,
            ParametersHelper.AddParametersToCommand("pi_RIMWIDTH", ParameterDirection.Input, OracleType.Number, p_sngRIMWIDTH, oraCmd)
            'pd_PRECONDENDDATE in date,
            ParametersHelper.AddParametersToCommand("pd_PRECONDENDDATE", ParameterDirection.Input, OracleType.DateTime, p_dtePRECONDENDDATE, oraCmd)
            'pi_PRECONDENDTEMP in number,
            ParametersHelper.AddParametersToCommand("pi_PRECONDENDTEMP", ParameterDirection.Input, OracleType.Number, p_intPRECONDENDTEMP, oraCmd)
            'pi_INFLATIONPRESSURE in number,
            ParametersHelper.AddParametersToCommand("pi_INFLATIONPRESSURE", ParameterDirection.Input, OracleType.Number, p_intINFLATIONPRESSURE, oraCmd)
            'pi_BEFOREDIAMETER in number,
            ParametersHelper.AddParametersToCommand("pi_BEFOREDIAMETER", ParameterDirection.Input, OracleType.Number, p_sngBEFOREDIAMETER, oraCmd)
            'pi_AFTERDIAMETER in number,
            ParametersHelper.AddParametersToCommand("pi_AFTERDIAMETER", ParameterDirection.Input, OracleType.Number, p_sngAFTERDIAMETER, oraCmd)
            'pi_BEFOREINFLATION in number,
            ParametersHelper.AddParametersToCommand("pi_BEFOREINFLATION", ParameterDirection.Input, OracleType.Number, p_intBEFOREINFLATION, oraCmd)
            'pi_AFTERINFLATION in number,
            ParametersHelper.AddParametersToCommand("pi_AFTERINFLATION", ParameterDirection.Input, OracleType.Number, p_intAFTERINFLATION, oraCmd)
            'pi_WHEELPOSITION in number,
            ParametersHelper.AddParametersToCommand("pi_WHEELPOSITION", ParameterDirection.Input, OracleType.Number, p_intWHEELPOSITION, oraCmd)
            'pi_WHEELNUMBER in number,
            ParametersHelper.AddParametersToCommand("pi_WHEELNUMBER", ParameterDirection.Input, OracleType.Number, p_intWHEELNUMBER, oraCmd)
            'pi_FINALTEMP in number,
            ParametersHelper.AddParametersToCommand("pi_FINALTEMP", ParameterDirection.Input, OracleType.Number, p_intFINALTEMP, oraCmd)
            'pi_FINALDISTANCE in number,
            ParametersHelper.AddParametersToCommand("pi_FINALDISTANCE", ParameterDirection.Input, OracleType.Number, p_sngFINALDISTANCE, oraCmd)
            'pi_FINALINFLATION in number,
            ParametersHelper.AddParametersToCommand("pi_FINALINFLATION", ParameterDirection.Input, OracleType.Number, p_intFINALINFLATION, oraCmd)
            'pd_POSTCONDSTARTDATE in date,
            ParametersHelper.AddParametersToCommand("pd_POSTCONDSTARTDATE", ParameterDirection.Input, OracleType.DateTime, p_dtePOSTCONDSTARTDATE, oraCmd)
            'pd_POSTCONDENDDATE in date,
            ParametersHelper.AddParametersToCommand("pd_POSTCONDENDDATE", ParameterDirection.Input, OracleType.DateTime, p_dtePOSTCONDENDDATE, oraCmd)
            'pi_POSTCONDENDTEMP in number,
            ParametersHelper.AddParametersToCommand("pi_POSTCONDENDTEMP", ParameterDirection.Input, OracleType.Number, p_intPOSTCONDENDTEMP, oraCmd)
            'ps_PASSYN in varchar2,
            ParametersHelper.AddParametersToCommand("ps_PASSYN", ParameterDirection.Input, OracleType.VarChar, p_strPASSYN, oraCmd)
            'pi_CertificationTypeID in number,
            ParametersHelper.AddParametersToCommand("pi_CertificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeID, oraCmd)
            'pd_SerialDate in date,
            ParametersHelper.AddParametersToCommand("pd_SerialDate", ParameterDirection.Input, OracleType.DateTime, p_dteSerialDate, oraCmd)
            'pi_PreCondTime in number,
            ParametersHelper.AddParametersToCommand("pi_PreCondTime", ParameterDirection.Input, OracleType.Number, p_sngPreCondTime, oraCmd)
            'pi_PostCondTime  in number 
            ParametersHelper.AddParametersToCommand("pi_PostCondTime", ParameterDirection.Input, OracleType.Number, p_sngPostCondTime, oraCmd)
            'pi_DIAMETERTESTDRUM in Number,
            ParametersHelper.AddParametersToCommand("pi_DIAMETERTESTDRUM", ParameterDirection.Input, OracleType.Number, p_sngDIAMETERTESTDRUM, oraCmd)
            'pi_PRECONDTEMP in Number,
            ParametersHelper.AddParametersToCommand("pi_PRECONDTEMP", ParameterDirection.Input, OracleType.Number, p_sngPRECONDTEMP, oraCmd)
            'pi_INFLATIONPRESSUREREADJUSTED in Number,
            ParametersHelper.AddParametersToCommand("pi_INFLATIONPRESSUREREADJUSTED", ParameterDirection.Input, OracleType.Number, p_sngINFLATIONPRESSUREREADJUSTED, oraCmd)
            'pi_CIRCUNFERENCEBEFORETEST in Number,
            ParametersHelper.AddParametersToCommand("pi_CIRCUNFERENCEBEFORETEST", ParameterDirection.Input, OracleType.Number, p_sngCIRCUNFERENCEBEFORETEST, oraCmd)
            'ps_RESULTPASSFAIL in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_RESULTPASSFAIL", ParameterDirection.Input, OracleType.VarChar, p_strRESULTPASSFAIL, oraCmd)
            'pi_ENDURANCEHOURS in Number,
            ParametersHelper.AddParametersToCommand("pi_ENDURANCEHOURS", ParameterDirection.Input, OracleType.Number, p_sngENDURANCEHOURS, oraCmd)
            'ps_POSSIBLEFAILURESFOUND in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_POSSIBLEFAILURESFOUND", ParameterDirection.Input, OracleType.VarChar, p_strPOSSIBLEFAILURESFOUND, oraCmd)
            'pi_CIRCUNFERENCEAFTERTEST in Number,
            ParametersHelper.AddParametersToCommand("pi_CIRCUNFERENCEAFTERTEST", ParameterDirection.Input, OracleType.Number, p_sngCIRCUNFERENCEAFTERTEST, oraCmd)
            'pi_OUTERDIAMETERDIFERENCE in Number,
            ParametersHelper.AddParametersToCommand("pi_OUTERDIAMETERDIFERENCE", ParameterDirection.Input, OracleType.Number, p_sngOUTERDIAMETERDIFERENCE, oraCmd)
            'pi_ODDIFERENCETOLERANCE in Number,
            ParametersHelper.AddParametersToCommand("pi_ODDIFERENCETOLERANCE", ParameterDirection.Input, OracleType.Number, p_sngODDIFERENCETOLERANCE, oraCmd)
            'ps_SERIENOM in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_SERIENOM", ParameterDirection.Input, OracleType.VarChar, p_strSERIENOM, oraCmd)
            'ps_FINALJUDGEMENT in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_FINALJUDGEMENT", ParameterDirection.Input, OracleType.VarChar, p_strFINALJUDGEMENT, oraCmd)
            'ps_APPROVER in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_APPROVER", ParameterDirection.Input, OracleType.VarChar, p_strAPPROVER, oraCmd)
            'ps_OperatorName in  Varchar2
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            'pi_certificateid in number
            ParametersHelper.AddParametersToCommand("pi_certificateid", ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)
            'ps_Matl_Num
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)

            'Low Pressure Start Inflation
            ParametersHelper.AddParametersToCommand("pn_lowInfstartinflation", ParameterDirection.Input, OracleType.Number, p_sngLowInfStartInflation, oraCmd)

            'Low Pressure End Inflation
            ParametersHelper.AddParametersToCommand("pn_lowInfendinflation", ParameterDirection.Input, OracleType.Number, p_sngLowInfEndInflation, oraCmd)

            'Low Pressure End Temp
            ParametersHelper.AddParametersToCommand("pn_lowInfendtemp", ParameterDirection.Input, OracleType.Number, p_intLowInfEndTemp, oraCmd)

            'ps_Operation 
            ParametersHelper.AddParametersToCommand("ps_Operation", ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_GTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strGTSpecEndurance, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.Endurance_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Mesure Id to be inserted on the detail table
            If oraCmd.Parameters.Item("pi_ENDURANCEID").Value.Equals(DBNull.Value) Then
                p_intENDURANCEID = 0
            Else
                p_intENDURANCEID = oraCmd.Parameters.Item("pi_ENDURANCEID").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()

        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Saves the endurance detail.
    ''' </summary>
    ''' <param name="p_intTESTSTEP">The p_int TESTSTEP.</param>
    ''' <param name="p_intTIMEINMIN">The p_int TIMEINMIN.</param>
    ''' <param name="p_intSpeed">The p_int speed.</param>
    ''' <param name="p_sngTOTMILES">The P_SNG TOTMILES.</param>
    ''' <param name="p_sngtLOAD">The P_SNGT LOAD.</param>
    ''' <param name="p_sngLOADPERCENT">The P_SNG LOADPERCENT.</param>
    ''' <param name="p_intSETINFLATION">The p_int SETINFLATION.</param>
    ''' <param name="p_intAMBTEMP">The p_int AMBTEMP.</param>
    ''' <param name="p_intINFPRESSURE">The p_int INFPRESSURE.</param>
    ''' <param name="p_dteSTEPCOMPLETIONDATE">The p_dte STEPCOMPLETIONDATE.</param>
    ''' <param name="p_intENDURANCEID">The p_int ENDURANCEID.</param>
    ''' <returns></returns>
    Public Function SaveEnduranceDetail(ByVal p_intTESTSTEP As Integer, _
                                        ByVal p_intTIMEINMIN As Integer, _
                                        ByVal p_intSpeed As Integer, _
                                        ByVal p_sngTOTMILES As Single, _
                                        ByVal p_sngtLOAD As Single, _
                                        ByVal p_sngLOADPERCENT As Single, _
                                        ByVal p_intSETINFLATION As Integer, _
                                        ByVal p_intAMBTEMP As Integer, _
                                        ByVal p_intINFPRESSURE As Integer, _
                                        ByVal p_dteSTEPCOMPLETIONDATE As DateTime, _
                                        ByVal p_intENDURANCEID As Integer) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try

            'PI_STEP IN NUMBER,
            ParametersHelper.AddParametersToCommand("PI_TESTSTEP", ParameterDirection.Input, OracleType.Number, p_intTESTSTEP, oraCmd)
            'pi_TIMEINMIN IN NUMBER,
            ParametersHelper.AddParametersToCommand("pi_TIMEINMIN", ParameterDirection.Input, OracleType.Number, p_intTIMEINMIN, oraCmd)
            'PI_SPEED IN NUMBER,
            ParametersHelper.AddParametersToCommand("PI_SPEED", ParameterDirection.Input, OracleType.Number, p_intSpeed, oraCmd)
            'PI_TOTMILES IN NUMBER,
            ParametersHelper.AddParametersToCommand("PI_TOTMILES", ParameterDirection.Input, OracleType.Number, p_sngTOTMILES, oraCmd)
            'PI_LOAD IN NUMBER,
            ParametersHelper.AddParametersToCommand("PI_LOAD", ParameterDirection.Input, OracleType.Number, p_sngtLOAD, oraCmd)
            'PI_LOADPERCENT IN NUMBER,
            ParametersHelper.AddParametersToCommand("PI_LOADPERCENT", ParameterDirection.Input, OracleType.Number, p_sngLOADPERCENT, oraCmd)
            'PI_SETINFLATION IN NUMBER,
            ParametersHelper.AddParametersToCommand("PI_SETINFLATION", ParameterDirection.Input, OracleType.Number, p_intSETINFLATION, oraCmd)
            'PI_AMBTEMP IN NUMBER,
            ParametersHelper.AddParametersToCommand("PI_AMBTEMP", ParameterDirection.Input, OracleType.Number, p_intAMBTEMP, oraCmd)
            'PI_INFPRESSURE IN NUMBER,
            ParametersHelper.AddParametersToCommand("PI_INFPRESSURE", ParameterDirection.Input, OracleType.Number, p_intINFPRESSURE, oraCmd)
            'PD_STEPCOMPLETIONDATE IN DATE,
            ParametersHelper.AddParametersToCommand("PD_STEPCOMPLETIONDATE", ParameterDirection.Input, OracleType.VarChar, p_dteSTEPCOMPLETIONDATE.ToShortDateString(), oraCmd)
            'PI_ENDURANCEID IN NUMBER
            ParametersHelper.AddParametersToCommand("PI_ENDURANCEID", ParameterDirection.Input, OracleType.Number, p_intENDURANCEID, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.ENDURANCEDETAIL_SAVE"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Save high speed header table data
    ''' </summary>
    ''' <param name="p_intHighSpeedID"></param>
    ''' <param name="p_strPROJECTNUMBER"></param>
    ''' <param name="p_intTIRENUM"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_dteCOMPETIONDATE"></param>
    ''' <param name="p_strDOTSERIALNUMBER"></param>
    ''' <param name="p_strMFGWWYY"></param>
    ''' <param name="p_dtePRECONDSTARTDATE"></param>
    ''' <param name="p_intPRECONDSARTTEMP"></param>
    ''' <param name="p_sngRIMDIAMETER"></param>
    ''' <param name="p_sngRIMWIDTH"></param>
    ''' <param name="p_dtePRECONDENDDATE"></param>
    ''' <param name="p_intPRECONDENDTEMP"></param>
    ''' <param name="p_intINFLATIONPRESSURE"></param>
    ''' <param name="p_sngBEFOREDIAMETER"></param>
    ''' <param name="p_sngAFTERDIAMETER"></param>
    ''' <param name="p_intBEFOREINFLATION"></param>
    ''' <param name="p_intAFTERINFLATION"></param>
    ''' <param name="p_intWHEELPOSITION"></param>
    ''' <param name="p_intWHEELNUMBER"></param>
    ''' <param name="p_intFINALTEMP"></param>
    ''' <param name="p_sngFINALDISTANCE"></param>
    ''' <param name="p_intFINALINFLATION"></param>
    ''' <param name="p_dtePOSTCONDSTARTDATE"></param>
    ''' <param name="p_dtePOSTCONDENDDATE"></param>
    ''' <param name="p_intPOSTCONDENDTEMP"></param>
    ''' <param name="p_sngPRECONDTIME"></param>
    ''' <param name="p_sngPOSTCONDTIME"></param>
    ''' <param name="p_strPASSYN"></param>
    ''' <param name="p_dteSERIALDATE"></param>
    ''' <param name="p_intCERTIFICATIONTYPEID"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_sngDIAMETERTESTDRUM"></param>
    ''' <param name="p_sngPRECONDTEMP"></param>
    ''' <param name="p_sngINFLATIONPRESSUREREADJUSTED"></param>
    ''' <param name="p_sngCIRCUNFERENCEBEFORETEST"></param>
    ''' <param name="p_sngWHEELSPEEDRPM"></param>
    ''' <param name="p_sngWHEELSPEEDKMH"></param>
    ''' <param name="p_sngCIRCUNFERENCEAFTERTEST"></param>
    ''' <param name="p_sngODDIFERENCE"></param>
    ''' <param name="p_sngODDIFERENCETOLERANCE"></param>
    ''' <param name="p_strSERIENOM"></param>
    ''' <param name="p_strFINALJUDGEMENT"></param>
    ''' <param name="p_strAPPROVER"></param>
    ''' <param name="p_sngPASSATKMH"></param>
    ''' <param name="p_strSPEEDTTESTPASSFAIL"></param>
    ''' <param name="p_sngSPEEDTOTALTIME"></param>
    ''' <param name="p_sngMAXSPEED"></param>
    ''' <param name="p_sngMAXLOAD"></param>
    ''' <param name="p_strOperatorName"></param>
    ''' <param name="p_intCertificateNumberID"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strOperation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveHighSpeed(ByRef p_intHighSpeedID As Integer, _
                                  ByVal p_strPROJECTNUMBER As String, _
                                  ByVal p_intTIRENUM As Integer, _
                                  ByVal p_strTESTSPEC As String, _
                                  ByVal p_dteCOMPETIONDATE As DateTime, _
                                  ByVal p_strDOTSERIALNUMBER As String, _
                                  ByVal p_strMFGWWYY As String, _
                                  ByVal p_dtePRECONDSTARTDATE As DateTime, _
                                  ByVal p_intPRECONDSARTTEMP As Integer, _
                                  ByVal p_sngRIMDIAMETER As Single, _
                                  ByVal p_sngRIMWIDTH As Single, _
                                  ByVal p_dtePRECONDENDDATE As DateTime, _
                                  ByVal p_intPRECONDENDTEMP As Integer, _
                                  ByVal p_intINFLATIONPRESSURE As Integer, _
                                  ByVal p_sngBEFOREDIAMETER As Single, _
                                  ByVal p_sngAFTERDIAMETER As Single, _
                                  ByVal p_intBEFOREINFLATION As Integer, _
                                  ByVal p_intAFTERINFLATION As Integer, _
                                  ByVal p_intWHEELPOSITION As Integer, _
                                  ByVal p_intWHEELNUMBER As Integer, _
                                  ByVal p_intFINALTEMP As Integer, _
                                  ByVal p_sngFINALDISTANCE As Single, _
                                  ByVal p_intFINALINFLATION As Integer, _
                                  ByVal p_dtePOSTCONDSTARTDATE As DateTime, _
                                  ByVal p_dtePOSTCONDENDDATE As DateTime, _
                                  ByVal p_intPOSTCONDENDTEMP As Integer, _
                                  ByVal p_sngPRECONDTIME As Single, _
                                  ByVal p_sngPOSTCONDTIME As Single, _
                                  ByVal p_strPASSYN As String, _
                                  ByVal p_dteSERIALDATE As DateTime, _
                                  ByVal p_intCERTIFICATIONTYPEID As Integer, _
                                  ByVal p_strCERTIFICATENUMBER As String, _
                                  ByVal p_sngDIAMETERTESTDRUM As Single, _
                                  ByVal p_sngPRECONDTEMP As Single, _
                                  ByVal p_sngINFLATIONPRESSUREREADJUSTED As Single, _
                                  ByVal p_sngCIRCUNFERENCEBEFORETEST As Single, _
                                  ByVal p_sngWHEELSPEEDRPM As Single, _
                                  ByVal p_sngWHEELSPEEDKMH As Single, _
                                  ByVal p_sngCIRCUNFERENCEAFTERTEST As Single, _
                                  ByVal p_sngODDIFERENCE As Single, _
                                  ByVal p_sngODDIFERENCETOLERANCE As Single, _
                                  ByVal p_strSERIENOM As String, _
                                  ByVal p_strFINALJUDGEMENT As String, _
                                  ByVal p_strAPPROVER As String, _
                                  ByVal p_sngPASSATKMH As Single, _
                                  ByVal p_strSPEEDTTESTPASSFAIL As String, _
                                  ByVal p_sngSPEEDTOTALTIME As Single, _
                                  ByVal p_sngMAXSPEED As Single, _
                                  ByVal p_sngMAXLOAD As Single, _
                                  ByVal p_strOperatorName As String, _
                                  ByVal p_intCertificateNumberID As Integer, _
                                  ByVal p_strMatlNum As String, _
                                  ByVal p_strOperation As String, _
                                  ByVal p_strGTSpecHighSpeed As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try
            ParametersHelper.AddParametersToCommand("pi_HIGHSPEEDID", ParameterDirection.Output, OracleType.Number, p_intHighSpeedID, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_PROJECTNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_TIRENUM", ParameterDirection.Input, OracleType.Number, p_intTIRENUM, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_TESTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            ParametersHelper.AddParametersToCommand("pd_COMPETIONDATE", ParameterDirection.Input, OracleType.DateTime, p_dteCOMPETIONDATE, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_DOTSERIALNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_MFGWWYY", ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)
            ParametersHelper.AddParametersToCommand("pd_PRECONDSTARTDATE", ParameterDirection.Input, OracleType.DateTime, p_dtePRECONDSTARTDATE, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_PRECONDSARTTEMP", ParameterDirection.Input, OracleType.Number, p_intPRECONDSARTTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand("pd_PRECONDTIME", ParameterDirection.Input, OracleType.Number, p_sngPRECONDTIME, oraCmd)

            ParametersHelper.AddParametersToCommand("pi_RIMDIAMETER", ParameterDirection.Input, OracleType.Number, p_sngRIMDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_RIMWIDTH", ParameterDirection.Input, OracleType.Number, p_sngRIMWIDTH, oraCmd)
            ParametersHelper.AddParametersToCommand("pd_PRECONDENDDATE", ParameterDirection.Input, OracleType.DateTime, p_dtePRECONDENDDATE, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_PRECONDENDTEMP", ParameterDirection.Input, OracleType.Number, p_intPRECONDENDTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_INFLATIONPRESSURE", ParameterDirection.Input, OracleType.Number, p_intINFLATIONPRESSURE, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_BEFOREDIAMETER", ParameterDirection.Input, OracleType.Number, p_sngBEFOREDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_AFTERDIAMETER", ParameterDirection.Input, OracleType.Number, p_sngAFTERDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_BEFOREINFLATION", ParameterDirection.Input, OracleType.Number, p_intBEFOREINFLATION, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_AFTERINFLATION", ParameterDirection.Input, OracleType.Number, p_intAFTERINFLATION, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_WHEELPOSITION", ParameterDirection.Input, OracleType.Number, p_intWHEELPOSITION, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_WHEELNUMBER", ParameterDirection.Input, OracleType.Number, p_intWHEELNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_FINALTEMP", ParameterDirection.Input, OracleType.Number, p_intFINALTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_FINALDISTANCE", ParameterDirection.Input, OracleType.Number, p_sngFINALDISTANCE, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_FINALINFLATION", ParameterDirection.Input, OracleType.Number, p_intFINALINFLATION, oraCmd)
            ParametersHelper.AddParametersToCommand("pd_POSTCONDSTARTDATE", ParameterDirection.Input, OracleType.DateTime, p_dtePOSTCONDSTARTDATE, oraCmd)
            ParametersHelper.AddParametersToCommand("pd_POSTCONDENDDATE", ParameterDirection.Input, OracleType.DateTime, p_dtePOSTCONDENDDATE, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_POSTCONDENDTEMP", ParameterDirection.Input, OracleType.Number, p_intPOSTCONDENDTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_PASSYN", ParameterDirection.Input, OracleType.VarChar, p_strPASSYN, oraCmd)
            ParametersHelper.AddParametersToCommand("pd_SERIALDATE", ParameterDirection.Input, OracleType.DateTime, p_dteSERIALDATE, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_POSTCONDTIME", ParameterDirection.Input, OracleType.Number, p_sngPOSTCONDTIME, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_CERTIFICATIONTYPEID", ParameterDirection.Input, OracleType.Number, p_intCERTIFICATIONTYPEID, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_DIAMETERTESTDRUM", ParameterDirection.Input, OracleType.Number, p_sngDIAMETERTESTDRUM, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_PRECONDTEMP", ParameterDirection.Input, OracleType.Number, p_sngPRECONDTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_INFLATIONPRESSUREREADJUSTED", ParameterDirection.Input, OracleType.Number, p_sngINFLATIONPRESSUREREADJUSTED, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_CIRCUNFERENCEBEFORETEST", ParameterDirection.Input, OracleType.Number, p_sngCIRCUNFERENCEBEFORETEST, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_WHEELSPEEDRPM", ParameterDirection.Input, OracleType.Number, p_sngWHEELSPEEDRPM, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_WHEELSPEEDKMH", ParameterDirection.Input, OracleType.Number, p_sngWHEELSPEEDKMH, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_CIRCUNFERENCEAFTERTEST", ParameterDirection.Input, OracleType.Number, p_sngCIRCUNFERENCEAFTERTEST, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_ODDIFERENCE", ParameterDirection.Input, OracleType.Number, p_sngODDIFERENCE, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_ODDIFERENCETOLERANCE", ParameterDirection.Input, OracleType.Number, p_sngODDIFERENCETOLERANCE, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_SERIENOM", ParameterDirection.Input, OracleType.VarChar, p_strSERIENOM, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_FINALJUDGEMENT", ParameterDirection.Input, OracleType.VarChar, p_strFINALJUDGEMENT, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_APPROVER", ParameterDirection.Input, OracleType.VarChar, p_strAPPROVER, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_PASSATKMH", ParameterDirection.Input, OracleType.Number, p_sngPASSATKMH, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_SPEEDTTESTPASSFAIL", ParameterDirection.Input, OracleType.VarChar, p_strSPEEDTTESTPASSFAIL, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_SPEEDTOTALTIME", ParameterDirection.Input, OracleType.Number, p_sngSPEEDTOTALTIME, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_MAXSPEED", ParameterDirection.Input, OracleType.Number, p_sngMAXSPEED, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_MAXLOAD", ParameterDirection.Input, OracleType.Number, p_sngMAXLOAD, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_CertificateID", ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)
            'ps_Matl_Num
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            'ps_Operation 
            ParametersHelper.AddParametersToCommand("ps_Operation", ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_GTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strGTSpecHighSpeed, oraCmd)


            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.HighSpeedHdr_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Mesure Id to be inserted on the detail table
            If oraCmd.Parameters.Item("pi_HIGHSPEEDID").Value.Equals(DBNull.Value) Then
                p_intHighSpeedID = 0
            Else
                p_intHighSpeedID = oraCmd.Parameters.Item("pi_HIGHSPEEDID").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Saves the high speed detail.
    ''' </summary>
    ''' <param name="p_intHighSpeedID">The p_int high speed ID.</param>
    ''' <param name="p_strOperatorId">The P_STR operator id.</param>
    ''' <param name="p_intTESTSTEP">The p_int TESTSTEP.</param>
    ''' <param name="p_intTimeMin">The p_int time min.</param>
    ''' <param name="p_sngSpeed">The P_SNG speed.</param>
    ''' <param name="p_sngTotMiles">The P_SNG tot miles.</param>
    ''' <param name="p_sngLoad">The P_SNG load.</param>
    ''' <param name="p_intLoadPercent">The p_int load percent.</param>
    ''' <param name="p_intSetInflation">The p_int set inflation.</param>
    ''' <param name="p_intAmbTemp">The p_int amb temp.</param>
    ''' <param name="p_intInfPressure">The p_int inf pressure.</param>
    ''' <param name="p_dteStepCompletionDate">The p_dte step completion date.</param>
    ''' <returns></returns>
    Public Function SaveHighSpeedDetail(ByVal p_intHighSpeedID As Integer, _
                                        ByVal p_strOperatorId As String, _
                                        ByVal p_intTESTSTEP As Integer, _
                                        ByVal p_intTimeMin As Integer, _
                                        ByVal p_sngSpeed As Single, _
                                        ByVal p_sngTotMiles As Single, _
                                        ByVal p_sngLoad As Single, _
                                        ByVal p_intLoadPercent As Single, _
                                        ByVal p_intSetInflation As Integer, _
                                        ByVal p_intAmbTemp As Integer, _
                                        ByVal p_intInfPressure As Integer, _
                                        ByVal p_dteStepCompletionDate As DateTime) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try
            'pi_HIGHSPEEDID in Number,
            ParametersHelper.AddParametersToCommand("pi_HIGHSPEEDID", ParameterDirection.Input, OracleType.Number, p_intHighSpeedID, oraCmd)
            'pi_TESTSTEP in number,
            ParametersHelper.AddParametersToCommand("pi_TESTSTEP", ParameterDirection.Input, OracleType.Number, p_intTESTSTEP, oraCmd)
            'pi_TIMEINMIN in Number,
            ParametersHelper.AddParametersToCommand("pi_TIMEINMIN", ParameterDirection.Input, OracleType.Number, p_intTimeMin, oraCmd)
            'pi_SPEED in ICS.HighSpeedDtl.SPEED%Type,
            ParametersHelper.AddParametersToCommand("pi_SPEED", ParameterDirection.Input, OracleType.Number, p_sngSpeed, oraCmd)
            'pi_TOTMILES in ICS.HighSpeedDtl.TOTMILES%Type,
            ParametersHelper.AddParametersToCommand("pi_TOTMILES", ParameterDirection.Input, OracleType.Number, p_sngTotMiles, oraCmd)
            'pi_LOAD in ICS.HighSpeedDtl.LOAD%Type,
            ParametersHelper.AddParametersToCommand("pi_LOAD", ParameterDirection.Input, OracleType.Number, p_sngLoad, oraCmd)
            'pi_LOADPERCENT in Number,
            ParametersHelper.AddParametersToCommand("pi_LOADPERCENT", ParameterDirection.Input, OracleType.Number, p_intLoadPercent, oraCmd)
            'pi_SETINFLATION in Number,
            ParametersHelper.AddParametersToCommand("pi_SETINFLATION", ParameterDirection.Input, OracleType.Number, p_intSetInflation, oraCmd)
            'pi_AMBTEMP in Number,
            ParametersHelper.AddParametersToCommand("pi_AMBTEMP", ParameterDirection.Input, OracleType.Number, p_intAmbTemp, oraCmd)
            'pi_INFPRESSURE in Number,
            ParametersHelper.AddParametersToCommand("pi_INFPRESSURE", ParameterDirection.Input, OracleType.Number, p_intInfPressure, oraCmd)
            'pd_STEPCOMPLETIONDATE in Timestamp,
            If p_dteStepCompletionDate.Equals(DateTime.MinValue) Then
                ParametersHelper.AddParametersToCommand("pd_STEPCOMPLETIONDATE", ParameterDirection.Input, OracleType.VarChar, DBNull.Value, oraCmd)
            Else
                ParametersHelper.AddParametersToCommand("pd_STEPCOMPLETIONDATE", ParameterDirection.Input, OracleType.VarChar, p_dteStepCompletionDate.ToShortDateString(), oraCmd)
            End If
            'ps_OperatorID in varchar2
            ParametersHelper.AddParametersToCommand("ps_OperatorID", ParameterDirection.Input, OracleType.VarChar, p_strOperatorId, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.HighSpeedDetail_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Save highspeed speedtest detail table 
    ''' </summary>
    ''' <param name="p_intHighSpeedID"></param>
    ''' <param name="p_intIteration"></param>
    ''' <param name="p_dteTime"></param>
    ''' <param name="p_sngSpeed"></param>
    ''' <param name="p_strUserName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveHighSpeed_SpeedTestDetail(ByVal p_intHighSpeedID As Integer, _
                                                  ByVal p_intIteration As Integer, _
                                                  ByVal p_dteTime As DateTime, _
                                                  ByVal p_sngSpeed As Single, _
                                                  ByVal p_strUserName As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try
            ParametersHelper.AddParametersToCommand("pi_ITERATION", ParameterDirection.Input, OracleType.Number, p_intIteration, oraCmd)
            ParametersHelper.AddParametersToCommand("pd_TIME", ParameterDirection.Input, OracleType.DateTime, p_dteTime, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_SPEED", ParameterDirection.Input, OracleType.Number, p_sngSpeed, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_HIGHSPEEDID", ParameterDirection.Input, OracleType.Number, p_intHighSpeedID, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.HIghSpeed_SpeedTestDetail_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' save sound header table
    ''' </summary>
    ''' <param name="p_strUserId"></param>
    ''' <param name="p_intSoundID"></param>
    ''' <param name="p_strPROJECTNUMBER"></param>
    ''' <param name="p_intTIRENUM"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_strTESTREPORTNUMBER"></param>
    ''' <param name="p_strMANUFACTUREANDBRAND"></param>
    ''' <param name="p_strTIRECLASS"></param>
    ''' <param name="p_strCATEGORYOFUSE"></param>
    ''' <param name="p_dteDATEOFTEST"></param>
    ''' <param name="p_strTESTVEHICULE"></param>
    ''' <param name="p_strTESTVEHICULEWHEELBASE"></param>
    ''' <param name="p_strLOCATIONOFTESTTRACK"></param>
    ''' <param name="p_dteDATETRACKCERTIFTOISO"></param>
    ''' <param name="p_strTIRESIZEDESIGNATION"></param>
    ''' <param name="p_strTIRESERVICEDESCRIPTION"></param>
    ''' <param name="p_strREFERENCEINFLATIONPRESSURE"></param>
    ''' <param name="p_strTESTMASS_FRONTL"></param>
    ''' <param name="p_strTESTMASS_FRONTR"></param>
    ''' <param name="p_strTESTMASS_REARL"></param>
    ''' <param name="p_strTESTMASS_REARR"></param>
    ''' <param name="p_strTIRELOADINDEX_FRONTL"></param>
    ''' <param name="p_strTIRELOADINDEX_FRONTR"></param>
    ''' <param name="p_strTIRELOADINDEX_REARL"></param>
    ''' <param name="p_strTIRELOADINDEX_REARR"></param>
    ''' <param name="p_strINFLATIONPRESSURECO_FRONTL"></param>
    ''' <param name="p_strINFLATIONPRESSURECO_FRONTR"></param>
    ''' <param name="p_strINFLATIONPRESSURECO_REARL"></param>
    ''' <param name="p_strINFLATIONPRESSURECO_REARR"></param>
    ''' <param name="p_strTESTRIMWIDTHCODE"></param>
    ''' <param name="p_strTEMPMEASURESENSORTYPE"></param>
    ''' <param name="p_intCERTIFICATIONTYPEID"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_intSKUID"></param>
    ''' <param name="p_intCertificateNUmberID"></param>
    ''' <param name="p_strOperation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSound(ByVal p_strUserId As String, _
                              ByRef p_intSoundID As Integer, _
                              ByVal p_strPROJECTNUMBER As String, _
                              ByVal p_intTIRENUM As Integer, _
                              ByVal p_strTESTSPEC As String, _
                              ByVal p_strTESTREPORTNUMBER As String, _
                              ByVal p_strMANUFACTUREANDBRAND As String, _
                              ByVal p_strTIRECLASS As String, _
                              ByVal p_strCATEGORYOFUSE As String, _
                              ByVal p_dteDATEOFTEST As DateTime, _
                              ByVal p_strTESTVEHICULE As String, _
                              ByVal p_strTESTVEHICULEWHEELBASE As String, _
                              ByVal p_strLOCATIONOFTESTTRACK As String, _
                              ByVal p_dteDATETRACKCERTIFTOISO As DateTime, _
                              ByVal p_strTIRESIZEDESIGNATION As String, _
                              ByVal p_strTIRESERVICEDESCRIPTION As String, _
                              ByVal p_strREFERENCEINFLATIONPRESSURE As String, _
                              ByVal p_strTESTMASS_FRONTL As String, _
                              ByVal p_strTESTMASS_FRONTR As String, _
                              ByVal p_strTESTMASS_REARL As String, _
                              ByVal p_strTESTMASS_REARR As String, _
                              ByVal p_strTIRELOADINDEX_FRONTL As String, _
                              ByVal p_strTIRELOADINDEX_FRONTR As String, _
                              ByVal p_strTIRELOADINDEX_REARL As String, _
                              ByVal p_strTIRELOADINDEX_REARR As String, _
                              ByVal p_strINFLATIONPRESSURECO_FRONTL As String, _
                              ByVal p_strINFLATIONPRESSURECO_FRONTR As String, _
                              ByVal p_strINFLATIONPRESSURECO_REARL As String, _
                              ByVal p_strINFLATIONPRESSURECO_REARR As String, _
                              ByVal p_strTESTRIMWIDTHCODE As String, _
                              ByVal p_strTEMPMEASURESENSORTYPE As String, _
                              ByVal p_intCERTIFICATIONTYPEID As Integer, _
                              ByVal p_strCERTIFICATENUMBER As String, _
                              ByVal p_intSKUID As Integer, _
                              ByVal p_intCertificateNUmberID As Integer, _
                              ByVal p_strOperation As String, _
                              ByVal p_strGTSpecSound As String, _
                              ByVal p_strMFGWWYY As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try
            'ps_UserID              in varchar2,
            ParametersHelper.AddParametersToCommand("ps_UserID", ParameterDirection.Input, OracleType.VarChar, p_strUserId, oraCmd)
            'pi_SoundID             out number , 
            ParametersHelper.AddParametersToCommand("pi_SoundID", ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            'ps_PROJECTNUMBER        in varchar2,
            ParametersHelper.AddParametersToCommand("ps_PROJECTNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            'pi_TIRENUMBER              in number  ,
            ParametersHelper.AddParametersToCommand("pi_TIRENUMBER", ParameterDirection.Input, OracleType.Number, p_intTIRENUM, oraCmd)
            'ps_TESTSPEC             in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            'ps_TESTREPORTNUMBER        in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTREPORTNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strTESTREPORTNUMBER, oraCmd)
            'ps_MANUFACTUREANDBRAND      in varchar2,
            ParametersHelper.AddParametersToCommand("ps_MANUFACTUREANDBRAND", ParameterDirection.Input, OracleType.VarChar, p_strMANUFACTUREANDBRAND, oraCmd)
            'ps_TIRECLASS              in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TIRECLASS", ParameterDirection.Input, OracleType.VarChar, p_strTIRECLASS, oraCmd)
            'ps_CATEGORYOFUSE     in varchar2,
            ParametersHelper.AddParametersToCommand("ps_CATEGORYOFUSE", ParameterDirection.Input, OracleType.VarChar, p_strCATEGORYOFUSE, oraCmd)
            'pd_DATEOFTEST      in TIMESTAMP ,
            ParametersHelper.AddParametersToCommand("pd_DATEOFTEST", ParameterDirection.Input, OracleType.DateTime, p_dteDATEOFTEST, oraCmd)
            'ps_TESTVEHICULE          in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTVEHICULE", ParameterDirection.Input, OracleType.VarChar, p_strTESTVEHICULE, oraCmd)
            'ps_TESTVEHICULEWHEELBASE             in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTVEHICULEWHEELBASE", ParameterDirection.Input, OracleType.VarChar, p_strTESTVEHICULEWHEELBASE, oraCmd)
            'ps_LOCATIONOFTESTTRACK       in varchar2,
            ParametersHelper.AddParametersToCommand("ps_LOCATIONOFTESTTRACK", ParameterDirection.Input, OracleType.VarChar, p_strLOCATIONOFTESTTRACK, oraCmd)
            'pd_DATETRACKCERTIFTOISO       in TIMESTAMP ,
            ParametersHelper.AddParametersToCommand("pd_DATETRACKCERTIFTOISO", ParameterDirection.Input, OracleType.DateTime, p_dteDATETRACKCERTIFTOISO, oraCmd)
            'ps_TIRESIZEDESIGNATION    in varchar2 ,
            ParametersHelper.AddParametersToCommand("ps_TIRESIZEDESIGNATION", ParameterDirection.Input, OracleType.VarChar, p_strTIRESIZEDESIGNATION, oraCmd)
            'ps_TIRESERVICEDESCRIPTION       in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TIRESERVICEDESCRIPTION", ParameterDirection.Input, OracleType.VarChar, p_strTIRESERVICEDESCRIPTION, oraCmd)

            'ps_REFERENCEINFLATIONPRESSURE       in varchar2,
            ParametersHelper.AddParametersToCommand("ps_REFERENCEINFLATIONPRESSURE", ParameterDirection.Input, OracleType.VarChar, p_strREFERENCEINFLATIONPRESSURE, oraCmd)

            'ps_TESTMASS_FRONTL        in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTMASS_FRONTL", ParameterDirection.Input, OracleType.VarChar, p_strTESTMASS_FRONTL, oraCmd)
            'ps_TESTMASS_FRONTR      in varchar2 ,
            ParametersHelper.AddParametersToCommand("ps_TESTMASS_FRONTR", ParameterDirection.Input, OracleType.VarChar, p_strTESTMASS_FRONTR, oraCmd)
            'ps_TESTMASS_REARL       in varchar2 ,
            ParametersHelper.AddParametersToCommand("ps_TESTMASS_REARL", ParameterDirection.Input, OracleType.VarChar, p_strTESTMASS_REARL, oraCmd)
            'ps_TESTMASS_REARR        in varchar2 ,
            ParametersHelper.AddParametersToCommand("ps_TESTMASS_REARR", ParameterDirection.Input, OracleType.VarChar, p_strTESTMASS_REARR, oraCmd)
            'ps_TIRELOADINDEX_FRONTL          in varchar2 ,
            ParametersHelper.AddParametersToCommand("ps_TIRELOADINDEX_FRONTL", ParameterDirection.Input, OracleType.VarChar, p_strTIRELOADINDEX_FRONTL, oraCmd)
            'ps_TIRELOADINDEX_FRONTR            in varchar2 ,
            ParametersHelper.AddParametersToCommand("ps_TIRELOADINDEX_FRONTR", ParameterDirection.Input, OracleType.VarChar, p_strTIRELOADINDEX_FRONTR, oraCmd)
            'ps_TIRELOADINDEX_REARL        in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TIRELOADINDEX_REARL", ParameterDirection.Input, OracleType.VarChar, p_strTIRELOADINDEX_REARL, oraCmd)
            'ps_TIRELOADINDEX_REARR       in varchar2 ,
            ParametersHelper.AddParametersToCommand("ps_TIRELOADINDEX_REARR", ParameterDirection.Input, OracleType.VarChar, p_strTIRELOADINDEX_REARR, oraCmd)
            'ps_INFLATIONPRESSURECO_FRONTL    in varchar2,
            ParametersHelper.AddParametersToCommand("ps_INFLATIONPRESSURECO_FRONTL", ParameterDirection.Input, OracleType.VarChar, p_strINFLATIONPRESSURECO_FRONTL, oraCmd)
            'ps_INFLATIONPRESSURECO_FRONTR      in varchar2,
            ParametersHelper.AddParametersToCommand("ps_INFLATIONPRESSURECO_FRONTR", ParameterDirection.Input, OracleType.VarChar, p_strINFLATIONPRESSURECO_FRONTR, oraCmd)
            'ps_INFLATIONPRESSURECO_REARL      in varchar2 ,
            ParametersHelper.AddParametersToCommand("ps_INFLATIONPRESSURECO_REARL", ParameterDirection.Input, OracleType.VarChar, p_strINFLATIONPRESSURECO_REARL, oraCmd)
            'ps_INFLATIONPRESSURECO_REARR               in varchar2,
            ParametersHelper.AddParametersToCommand("ps_INFLATIONPRESSURECO_REARR", ParameterDirection.Input, OracleType.VarChar, p_strINFLATIONPRESSURECO_REARR, oraCmd)
            'ps_TESTRIMWIDTHCODE           in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTRIMWIDTHCODE", ParameterDirection.Input, OracleType.VarChar, p_strTESTRIMWIDTHCODE, oraCmd)
            'ps_TEMPMEASURESENSORTYPE         in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TEMPMEASURESENSORTYPE", ParameterDirection.Input, OracleType.VarChar, p_strTEMPMEASURESENSORTYPE, oraCmd)
            'pi_CERTIFICATIONTYPEID         in number,
            ParametersHelper.AddParametersToCommand("pi_CERTIFICATIONTYPEID", ParameterDirection.Input, OracleType.Number, p_intCERTIFICATIONTYPEID, oraCmd)
            'pi_SKUID                in number 
            ParametersHelper.AddParametersToCommand("pi_SKUID", ParameterDirection.Input, OracleType.Number, p_intSKUID, oraCmd)
            'pi_CertificateID in number
            ParametersHelper.AddParametersToCommand("pi_CertificateID", ParameterDirection.Input, OracleType.Number, p_intCertificateNUmberID, oraCmd)
            'ps_Operation 
            ParametersHelper.AddParametersToCommand("ps_Operation", ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_GTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strGTSpecSound, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_MFGWWYY", ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.SoundHDR_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Mesure Id to be inserted on the detail table
            If oraCmd.Parameters.Item("pi_SoundID").Value.Equals(DBNull.Value) Then
                p_intSoundID = 0
            Else
                p_intSoundID = oraCmd.Parameters.Item("pi_SoundID").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult
    End Function

    ''' <summary>
    ''' save sound detail table
    ''' </summary>
    ''' <param name="p_strUserId"></param>
    ''' <param name="p_intITERATION"></param>
    ''' <param name="p_strTESTSPEED"></param>
    ''' <param name="p_strDIRECTIONOFRUN"></param>
    ''' <param name="p_strSOUNDLEVELLEFT"></param>
    ''' <param name="p_strSOUNDLEVELRIGHT"></param>
    ''' <param name="p_strAIRTEMP"></param>
    ''' <param name="p_strTRACKTEMP"></param>
    ''' <param name="p_strSOUNDLEVELLEFT_TEMPCOR"></param>
    ''' <param name="p_strSOUNDLEVELRIGHT_TEMPCOR"></param>
    ''' <param name="p_intSoundID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSoundDetail(ByVal p_strUserId As String, _
                                    ByVal p_intITERATION As Integer, _
                                    ByVal p_strTESTSPEED As String, _
                                    ByVal p_strDIRECTIONOFRUN As String, _
                                    ByVal p_strSOUNDLEVELLEFT As String, _
                                    ByVal p_strSOUNDLEVELRIGHT As String, _
                                    ByVal p_strAIRTEMP As String, _
                                    ByVal p_strTRACKTEMP As String, _
                                    ByVal p_strSOUNDLEVELLEFT_TEMPCOR As String, _
                                    ByVal p_strSOUNDLEVELRIGHT_TEMPCOR As String, _
                                    ByVal p_intSoundID As Integer) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try

            'ps_UserID in varchar2,
            ParametersHelper.AddParametersToCommand("ps_UserID", ParameterDirection.Input, OracleType.VarChar, p_strUserId, oraCmd)
            'pi_ITERATION in number,
            ParametersHelper.AddParametersToCommand("pi_ITERATION", ParameterDirection.Input, OracleType.Number, p_intITERATION, oraCmd)
            'ps_TESTSPEED  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTSPEED", ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEED, oraCmd)
            'ps_DIRECTIONOFRUN  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_DIRECTIONOFRUN", ParameterDirection.Input, OracleType.VarChar, p_strDIRECTIONOFRUN, oraCmd)
            'ps_SOUNDLEVELLEFT  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_SOUNDLEVELLEFT", ParameterDirection.Input, OracleType.VarChar, p_strSOUNDLEVELLEFT, oraCmd)
            'ps_SOUNDLEVELRIGHT  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_SOUNDLEVELRIGHT", ParameterDirection.Input, OracleType.VarChar, p_strSOUNDLEVELRIGHT, oraCmd)
            'ps_AIRTEMP  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_AIRTEMP", ParameterDirection.Input, OracleType.VarChar, p_strAIRTEMP, oraCmd)
            'ps_TRACKTEMP in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TRACKTEMP", ParameterDirection.Input, OracleType.VarChar, p_strTRACKTEMP, oraCmd)
            'ps_SOUNDLEVELLEFT_TEMPCOR in varchar2,
            ParametersHelper.AddParametersToCommand("ps_SOUNDLEVELLEFT_TEMPCOR", ParameterDirection.Input, OracleType.VarChar, p_strSOUNDLEVELLEFT_TEMPCOR, oraCmd)
            'ps_SOUNDLEVELRIGHT_TEMPCOR  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_SOUNDLEVELRIGHT_TEMPCOR", ParameterDirection.Input, OracleType.VarChar, p_strSOUNDLEVELRIGHT_TEMPCOR, oraCmd)
            'pi_SOUNDID in number)as
            ParametersHelper.AddParametersToCommand("pi_SOUNDID", ParameterDirection.Input, OracleType.Number, p_intSoundID, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.SoundDetail_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' save wetgrip table
    ''' </summary>
    ''' <param name="p_strUserId"></param>
    ''' <param name="p_intWetGripID"></param>
    ''' <param name="p_strPROJECTNUMBER"></param>
    ''' <param name="p_intTIRENUM"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_dteDATEOFTEST"></param>
    ''' <param name="p_strTESTVEHICLE"></param>
    ''' <param name="p_strLOCATIONOFTESTTRACK"></param>
    ''' <param name="p_strTESTTRACKCHARACTERISTICS"></param>
    ''' <param name="p_strISSUEBY"></param>
    ''' <param name="p_strMETHODOFCERTIFICATION"></param>
    ''' <param name="p_strTESTTIREDETAILS"></param>
    ''' <param name="p_strTIRESIZEANDSERVICEDESC"></param>
    ''' <param name="p_strTIREBRANDANDTRADEDESC"></param>
    ''' <param name="p_strREFERENCEINFLATIONPRESSURE"></param>
    ''' <param name="p_strTESTRIMWITHCODE"></param>
    ''' <param name="p_strTEMPMEASURESENSORTYPE"></param>
    ''' <param name="p_strIDENTIFICATIONSRTT"></param>
    ''' <param name="p_strTESTTIRELOAD_SRTT"></param>
    ''' <param name="p_strTESTTIRELOAD_CANDIDATE"></param>
    ''' <param name="p_strTESTTIRELOAD_CONTROL"></param>
    ''' <param name="p_strWATERDEPTH_SRTT"></param>
    ''' <param name="p_strWATERDEPTH_CANDIDATE"></param>
    ''' <param name="p_strWATERDEPTH_CONTROL"></param>
    ''' <param name="p_strWETTEDTRACKTEMPAVG"></param>
    ''' <param name="p_intCERTIFICATIONTYPEID"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_intSKUID"></param>
    ''' <param name="p_intCertificateNUmberID"></param>
    ''' <param name="p_strOperation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveWetGrip(ByVal p_strUserId As String, _
                                ByRef p_intWetGripID As Integer, _
                                ByVal p_strPROJECTNUMBER As String, _
                                ByVal p_intTIRENUM As Integer, _
                                ByVal p_strTESTSPEC As String, _
                                ByVal p_dteDATEOFTEST As DateTime, _
                                ByVal p_strTESTVEHICLE As String, _
                                ByVal p_strLOCATIONOFTESTTRACK As String, _
                                ByVal p_strTESTTRACKCHARACTERISTICS As String, _
                                ByVal p_strISSUEBY As String, _
                                ByVal p_strMETHODOFCERTIFICATION As String, _
                                ByVal p_strTESTTIREDETAILS As String, _
                                ByVal p_strTIRESIZEANDSERVICEDESC As String, _
                                ByVal p_strTIREBRANDANDTRADEDESC As String, _
                                ByVal p_strREFERENCEINFLATIONPRESSURE As String, _
                                ByVal p_strTESTRIMWITHCODE As String, _
                                ByVal p_strTEMPMEASURESENSORTYPE As String, _
                                ByVal p_strIDENTIFICATIONSRTT As String, _
                                ByVal p_strTESTTIRELOAD_SRTT As String, _
                                ByVal p_strTESTTIRELOAD_CANDIDATE As String, _
                                ByVal p_strTESTTIRELOAD_CONTROL As String, _
                                ByVal p_strWATERDEPTH_SRTT As String, _
                                ByVal p_strWATERDEPTH_CANDIDATE As String, _
                                ByVal p_strWATERDEPTH_CONTROL As String, _
                                ByVal p_strWETTEDTRACKTEMPAVG As String, _
                                ByVal p_intCERTIFICATIONTYPEID As Integer, _
                                ByVal p_strCERTIFICATENUMBER As String, _
                                ByVal p_intSKUID As Integer, _
                                ByVal p_intCertificateNUmberID As Integer, _
                                ByVal p_strOperation As String, _
                                ByVal p_strGTSpecWetGrip As String, _
                                ByVal p_strMFGWWYY As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try
            'ps_UserID              in varchar2,
            ParametersHelper.AddParametersToCommand("ps_UserID", ParameterDirection.Input, OracleType.VarChar, p_strUserId, oraCmd)
            'pi_WETGRIPID             out number , 
            ParametersHelper.AddParametersToCommand("pi_WETGRIPID", ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            'ps_PROJECTNUMBER        in varchar2,
            ParametersHelper.AddParametersToCommand("ps_PROJECTNUMBER", ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            'pi_TIRENUMBER              in number  ,
            ParametersHelper.AddParametersToCommand("pi_TIRENUMBER", ParameterDirection.Input, OracleType.Number, p_intTIRENUM, oraCmd)
            'ps_TESTSPEC             in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            'pd_DATEOFTEST  in TIMESTAMP,
            ParametersHelper.AddParametersToCommand("pd_DATEOFTEST", ParameterDirection.Input, OracleType.DateTime, p_dteDATEOFTEST, oraCmd)
            'ps_TESTVEHICLE  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTVEHICLE", ParameterDirection.Input, OracleType.VarChar, p_strTESTVEHICLE, oraCmd)
            'ps_LOCATIONOFTESTTRACK  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_LOCATIONOFTESTTRACK", ParameterDirection.Input, OracleType.VarChar, p_strLOCATIONOFTESTTRACK, oraCmd)
            'ps_TESTTRACKCHARACTERISTICS  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTTRACKCHARACTERISTICS", ParameterDirection.Input, OracleType.VarChar, p_strTESTTRACKCHARACTERISTICS, oraCmd)
            'ps_ISSUEBY  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_ISSUEBY", ParameterDirection.Input, OracleType.VarChar, p_strISSUEBY, oraCmd)
            'ps_METHODOFCERTIFICATION  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_METHODOFCERTIFICATION", ParameterDirection.Input, OracleType.VarChar, p_strMETHODOFCERTIFICATION, oraCmd)
            'ps_TESTTIREDETAILS  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTTIREDETAILS", ParameterDirection.Input, OracleType.VarChar, p_strTESTTIREDETAILS, oraCmd)
            'ps_TIRESIZEANDSERVICEDESC  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TIRESIZEANDSERVICEDESC", ParameterDirection.Input, OracleType.VarChar, p_strTIRESIZEANDSERVICEDESC, oraCmd)
            'ps_TIREBRANDANDTRADEDESC  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TIREBRANDANDTRADEDESC", ParameterDirection.Input, OracleType.VarChar, p_strTIREBRANDANDTRADEDESC, oraCmd)
            'ps_REFERENCEINFLATIONPRESSURE  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_REFERENCEINFLATIONPRESSURE", ParameterDirection.Input, OracleType.VarChar, p_strREFERENCEINFLATIONPRESSURE, oraCmd)
            'ps_TESTRIMWITHCODE  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTRIMWITHCODE", ParameterDirection.Input, OracleType.VarChar, p_strTESTRIMWITHCODE, oraCmd)
            'ps_TEMPMEASURESENSORTYPE  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TEMPMEASURESENSORTYPE", ParameterDirection.Input, OracleType.VarChar, p_strTEMPMEASURESENSORTYPE, oraCmd)
            'ps_IDENTIFICATIONSRTT  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_IDENTIFICATIONSRTT", ParameterDirection.Input, OracleType.VarChar, p_strIDENTIFICATIONSRTT, oraCmd)
            'ps_TESTTIRELOAD_SRTT  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTTIRELOAD_SRTT", ParameterDirection.Input, OracleType.VarChar, p_strTESTTIRELOAD_SRTT, oraCmd)
            'ps_TESTTIRELOAD_CANDIDATE  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTTIRELOAD_CANDIDATE", ParameterDirection.Input, OracleType.VarChar, p_strTESTTIRELOAD_CANDIDATE, oraCmd)
            'ps_TESTTIRELOAD_CONTROL  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTTIRELOAD_CONTROL", ParameterDirection.Input, OracleType.VarChar, p_strTESTTIRELOAD_CONTROL, oraCmd)
            'ps_WATERDEPTH_SRTT  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_WATERDEPTH_SRTT", ParameterDirection.Input, OracleType.VarChar, p_strWATERDEPTH_SRTT, oraCmd)
            'ps_WATERDEPTH_CANDIDATE  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_WATERDEPTH_CANDIDATE", ParameterDirection.Input, OracleType.VarChar, p_strWATERDEPTH_CANDIDATE, oraCmd)
            'ps_WATERDEPTH_CONTROL  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_WATERDEPTH_CONTROL", ParameterDirection.Input, OracleType.VarChar, p_strWATERDEPTH_CONTROL, oraCmd)
            'ps_WETTEDTRACKTEMPAVG  in Varchar2,
            ParametersHelper.AddParametersToCommand("ps_WETTEDTRACKTEMPAVG", ParameterDirection.Input, OracleType.VarChar, p_strWETTEDTRACKTEMPAVG, oraCmd)
            'pi_CERTIFICATIONTYPEID         in number,
            ParametersHelper.AddParametersToCommand("pi_CERTIFICATIONTYPEID", ParameterDirection.Input, OracleType.Number, p_intCERTIFICATIONTYPEID, oraCmd)
            'pi_SKUID                in number 
            ParametersHelper.AddParametersToCommand("pi_SKUID", ParameterDirection.Input, OracleType.Number, p_intSKUID, oraCmd)
            'pi_CertificateID in number
            ParametersHelper.AddParametersToCommand("pi_CertificateID", ParameterDirection.Input, OracleType.Number, p_intCertificateNUmberID, oraCmd)
            'ps_Operation 
            ParametersHelper.AddParametersToCommand("ps_Operation", ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_GTSPEC", ParameterDirection.Input, OracleType.VarChar, p_strGTSpecWetGrip, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_MFGWWYY", ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.WetGripHDR_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Mesure Id to be inserted on the detail table
            If oraCmd.Parameters.Item("pi_WETGRIPID").Value.Equals(DBNull.Value) Then
                p_intWetGripID = 0
            Else
                p_intWetGripID = oraCmd.Parameters.Item("pi_WETGRIPID").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' save wetgrip detail table
    ''' </summary>
    ''' <param name="p_strUserId"></param>
    ''' <param name="p_intITERATION"></param>
    ''' <param name="p_strTESTSPEED"></param>
    ''' <param name="p_strDIRECTIONOFRUN"></param>
    ''' <param name="p_strSRTT"></param>
    ''' <param name="p_strCANDIDATETIRE"></param>
    ''' <param name="p_strPEAKBREAKFORCECOEFICIENT"></param>
    ''' <param name="p_strMEANFULLYDEVDECELERATION"></param>
    ''' <param name="p_strWETGRIPINDEX"></param>
    ''' <param name="p_strCOMMENTS"></param>
    ''' <param name="p_intWetGripID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveWetGripDetail(ByVal p_strUserId As String, _
                                      ByVal p_intITERATION As Integer, _
                                      ByVal p_strTESTSPEED As String, _
                                      ByVal p_strDIRECTIONOFRUN As String, _
                                      ByVal p_strSRTT As String, _
                                      ByVal p_strCANDIDATETIRE As String, _
                                      ByVal p_strPEAKBREAKFORCECOEFICIENT As String, _
                                      ByVal p_strMEANFULLYDEVDECELERATION As String, _
                                      ByVal p_strWETGRIPINDEX As String, _
                                      ByVal p_strCOMMENTS As String, _
                                      ByVal p_intWetGripID As Integer) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try

            'ps_UserID in varchar2,
            ParametersHelper.AddParametersToCommand("ps_UserID", ParameterDirection.Input, OracleType.VarChar, p_strUserId, oraCmd)
            'pi_ITERATION in number,
            ParametersHelper.AddParametersToCommand("pi_ITERATION", ParameterDirection.Input, OracleType.Number, p_intITERATION, oraCmd)
            'ps_TESTSPEED  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_TESTSPEED", ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEED, oraCmd)
            'ps_DIRECTIONOFRUN  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_DIRECTIONOFRUN", ParameterDirection.Input, OracleType.VarChar, p_strDIRECTIONOFRUN, oraCmd)
            'ps_SRTT  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_SRTT", ParameterDirection.Input, OracleType.VarChar, p_strSRTT, oraCmd)
            'ps_CANDIDATETIRE  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_CANDIDATETIRE", ParameterDirection.Input, OracleType.VarChar, p_strCANDIDATETIRE, oraCmd)
            'ps_PEAKBREAKFORCECOEFICIENT  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_PEAKBREAKFORCECOEFICIENT", ParameterDirection.Input, OracleType.VarChar, p_strPEAKBREAKFORCECOEFICIENT, oraCmd)
            'ps_MEANFULLYDEVDECELERATION  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_MEANFULLYDEVDECELERATION", ParameterDirection.Input, OracleType.VarChar, p_strMEANFULLYDEVDECELERATION, oraCmd)
            'ps_WETGRIPINDEX  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_WETGRIPINDEX", ParameterDirection.Input, OracleType.VarChar, p_strWETGRIPINDEX, oraCmd)
            'ps_COMMENTS  in varchar2,
            ParametersHelper.AddParametersToCommand("ps_COMMENTS", ParameterDirection.Input, OracleType.VarChar, p_strCOMMENTS, oraCmd)
            'pi_WETGRIPID in number
            ParametersHelper.AddParametersToCommand("pi_WETGRIPID", ParameterDirection.Input, OracleType.Number, p_intWetGripID, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.WetGripDetail_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Gets the test result data.
    ''' </summary>
    ''' <param name="p_strMatlNum">The Material Number.</param>
    ''' <param name="p_strCertificateNumber">The certificate number.</param>
    ''' <param name="p_intCertificationTypeId">Id of the certification type.</param>
    ''' <returns>Test result data</returns>
    Public Function GetTestResultData(ByVal p_strMatlNum As String, _
                                      ByVal p_intSKUID As Integer, _
                                      ByVal p_strCertificateNumber As String, _
                                      ByVal p_intCertificateNumberID As Integer, _
                                      ByVal p_intCertificationTypeId As Integer) As ICSDataSet

        Dim dsResults As New ICSDataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'pc_MesureCursor out retCursor,
            ParametersHelper.AddParametersToCommand("PC_MEASURECURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_MesureDetailCursor out retCursor,
            ParametersHelper.AddParametersToCommand("PC_MEASUREDETAILCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_PLUNGERHDRCURSOR out retcursor,
            ParametersHelper.AddParametersToCommand("PC_PLUNGERHDRCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_PLUNGERDTLCURSOR out retcursor,
            ParametersHelper.AddParametersToCommand("PC_PLUNGERDTLCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_TREADWEARHDRCURSOR out retcursor,
            ParametersHelper.AddParametersToCommand("PC_TREADWEARHDRCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ' PC_TREADWEARDTLCURSOR out retcursor,
            ParametersHelper.AddParametersToCommand("PC_TREADWEARDTLCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_BEADUNSEATHDRCURSOR out retcursor,
            ParametersHelper.AddParametersToCommand("PC_BEADUNSEATHDRCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_BEADUNSEATDTLCURSOR out retcursor,
            ParametersHelper.AddParametersToCommand("PC_BEADUNSEATDTLCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_ENDURANCEHDRCURSOR out retcursor
            ParametersHelper.AddParametersToCommand("PC_ENDURANCEHDRCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_ENDURANCEDTLCURSOR out retcursor
            ParametersHelper.AddParametersToCommand("PC_ENDURANCEDTLCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_HIGHSPEEDCURSOR  out retcursor,
            ParametersHelper.AddParametersToCommand("PC_HIGHSPEEDCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_HIGHSPEEDDETAILCURSOR  out retcursor,
            ParametersHelper.AddParametersToCommand("PC_HIGHSPEEDDETAILCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'pc_HSSpeedTestDetail out retcursor,
            ParametersHelper.AddParametersToCommand("pc_HSSpeedTestDetail", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_SOUNDHdrCURSOR  out retcursor,
            ParametersHelper.AddParametersToCommand("PC_SOUNDHdrCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_SOUNDDETAILCURSOR  out retcursor,
            ParametersHelper.AddParametersToCommand("PC_SOUNDDETAILCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_WETGRIPHDRCURSOR  out retcursor,
            ParametersHelper.AddParametersToCommand("PC_WETGRIPHDRCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'PC_WETGRIPDETAILCURSOR  out retcursor, 
            ParametersHelper.AddParametersToCommand("PC_WETGRIPDETAILCURSOR", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ''pi_CertificationTypeID in number
            ParametersHelper.AddParametersToCommand("PI_CERTIFICATIONTYPEID", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            'pi_SKUId in number,
            ParametersHelper.AddParametersToCommand("PI_SKUID", ParameterDirection.Input, OracleType.Number, p_intSKUID, oraCmd)
            'ps_CertificateNumber in varchar
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_CertificateNumberID in number
            ParametersHelper.AddParametersToCommand("pi_CertificateNumberID", ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "testresults_crud.GetTestresults"

            Connect()
            oraCmd.Connection = Connection
            ' Get the data
            oraAdp.SelectCommand = oraCmd

            dsResults.EnforceConstraints = False

            oraAdp.TableMappings.Add("Table", "MeasureHdr")
            oraAdp.TableMappings.Add("Table1", "MeasureDtl")
            oraAdp.TableMappings.Add("Table2", "PlungerHdr")
            oraAdp.TableMappings.Add("Table3", "PlungerDtl")
            oraAdp.TableMappings.Add("Table4", "TreadWearHdr")
            oraAdp.TableMappings.Add("Table5", "TreadWearDtl")
            oraAdp.TableMappings.Add("Table6", "BeadUnseatHdr")
            oraAdp.TableMappings.Add("Table7", "BeadUnseatDtl")
            oraAdp.TableMappings.Add("Table8", "EnduranceHdr")
            oraAdp.TableMappings.Add("Table9", "EnduranceDtl")
            oraAdp.TableMappings.Add("Table10", "HighSpeedHdr")
            oraAdp.TableMappings.Add("Table11", "HighSpeedDtl")
            oraAdp.TableMappings.Add("Table12", "SpeedTestDetail")
            oraAdp.TableMappings.Add("Table13", "SoundHdr")
            oraAdp.TableMappings.Add("Table14", "SoundDetail")
            oraAdp.TableMappings.Add("Table15", "WetGripHdr")
            oraAdp.TableMappings.Add("Table16", "WetGripDetail")

            oraAdp.Fill(dsResults)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dsResults

    End Function

#End Region

#Region "Methods - CERTIFICATION_AUDIT_LOG"

    ''' <summary>
    ''' Get entire AuditLog
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAuditLog() As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "certification_crud.GET_AUDITLOG"
            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ''add the parameters
            'oraCmd.Parameters.Add(oraResults)
            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd
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
    ''' Get Approval Reasons
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApprovalReasons(ByVal p_intCertificationTypeId As Integer) As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "certification_crud.GET_APPROVALREASONS"
            ParametersHelper.AddParametersToCommand("pi_CertificationTypeId", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
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
    ''' Update single AuditLog entry
    ''' </summary>
    ''' <param name="p_intChangeLogID"></param>
    ''' <param name="p_strApprovalStatus"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateAuditLogEntry(ByVal p_intChangeLogID As Integer, ByVal p_dtemChangeDateTime As DateTime, _
                                        ByVal p_strApprovalStatus As String, ByVal p_strApprover As String) As Boolean

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "certification_crud.AUDITLOG_UpdateApprovalStatus"
            ParametersHelper.AddParametersToCommand("pi_ChangeLogId", ParameterDirection.Input, OracleType.Number, p_intChangeLogID, oraCmd)
            ParametersHelper.AddParametersToCommand("pd_ChangeDateTime", ParameterDirection.Input, OracleType.DateTime, p_dtemChangeDateTime, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_Status", ParameterDirection.Input, OracleType.VarChar, p_strApprovalStatus, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_Approver", ParameterDirection.Input, OracleType.VarChar, p_strApprover, oraCmd)

            'oraCmd.Parameters.Add(oraResults)
            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd
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

        Return True

    End Function

    ''' <summary>
    ''' Get audit log after given date
    ''' </summary>
    ''' <param name="p_dtemChangeDateTime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAuditLogAfterDate(ByVal p_dtemChangeDateTime As DateTime) As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "certification_crud.GET_AuditLogAfterDate"
            ParametersHelper.AddParametersToCommand("pd_ChangeDateTime", ParameterDirection.Input, OracleType.DateTime, p_dtemChangeDateTime, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ''add the parameters
            'oraCmd.Parameters.Add(oraResults)
            Connect()
            oraCmd.Connection = Connection
            'Get the data
            oraAdp.SelectCommand = oraCmd
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

#End Region

#Region "Methods - ADD_CERTIFICATION"

    ''' <summary>
    ''' Check if Material number exists
    ''' </summary>
    ''' <param name="p_strMatlNum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckIfMatlNumExists(ByVal p_strMatlNum As String) As Boolean

        Dim oraCmd As New OracleCommand
        Dim strMatlNumExists As String

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ics_common_functions.CheckIfSKUExists"
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)

            Dim oraMatlNumExist As New OracleParameter()
            oraMatlNumExist.ParameterName = "ps_MatlExist"
            oraMatlNumExist.OracleType = OracleType.VarChar
            oraMatlNumExist.Direction = ParameterDirection.Output
            oraMatlNumExist.Size = 1

            oraCmd.Parameters.Add(oraMatlNumExist)

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            'Gets the Material number exists
            strMatlNumExists = oraCmd.Parameters.Item("ps_MatlExist").Value

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return IIf(strMatlNumExists.ToLower().Equals("y"), True, False)

    End Function

    Public Function GetCertificateID(ByVal p_strCertificateNumber As String, ByVal p_intCertificateTypeId As Integer, ByVal p_strExtensionNo As String) As Integer


        Dim oraCmd As New OracleCommand
        Dim p_intCertificateId As Int32

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_COMMON_FUNCTIONS.GetCertificateIDByNumber"
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, 20, p_strCertificateNumber, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_CertificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertificateTypeId, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_ExtensionNo", ParameterDirection.Input, OracleType.VarChar, p_strExtensionNo, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_CertificateID", ParameterDirection.Output, OracleType.Number, p_intCertificateId, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            'Gets the certificate number exists value
            p_intCertificateId = Convert.ToInt32(oraCmd.Parameters.Item("pi_CertificateId").Value)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return p_intCertificateId

    End Function

    Public Function GetApprovedSubstitution(ByVal p_intCertificationTypeId As Integer, ByVal p_strField As String, ByVal p_sngValue As Single, ByVal p_intSKUID As Integer) As Single

        Dim oraCmd As New OracleCommand
        Dim sngNewValue As Single

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.GetApprovedSubstitution"
            ParametersHelper.AddParametersToCommand("pi_CertificationTypeID", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_Field", ParameterDirection.Input, OracleType.VarChar, 50, p_strField, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_Value", ParameterDirection.Input, OracleType.Number, p_sngValue, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_SkuId", ParameterDirection.Input, OracleType.Number, p_intSKUID, oraCmd)
            ParametersHelper.AddParametersToCommand("pi_NewValue", ParameterDirection.Output, OracleType.Number, sngNewValue, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            'Gets the new value
            sngNewValue = Convert.ToSingle(oraCmd.Parameters.Item("pi_NewValue").Value)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return sngNewValue

    End Function

    ''' <summary>
    ''' Check if certificate number exists
    ''' </summary>
    ''' <param name="p_strCertificateNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckIfCertificateNumberExists(ByVal p_strCertificateNumber As String) As Boolean

        Dim oraCmd As New OracleCommand
        Dim strCertificateNumberExists As String

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ics_common_functions.CheckIfCertificateNumberExists"
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)

            Dim oraMatlNumExist As New OracleParameter()
            oraMatlNumExist.ParameterName = "ps_CertificateNumberExists"
            oraMatlNumExist.OracleType = OracleType.VarChar
            oraMatlNumExist.Direction = ParameterDirection.Output
            oraMatlNumExist.Size = 1

            oraCmd.Parameters.Add(oraMatlNumExist)

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            'Gets the certificate number exists value
            strCertificateNumberExists = oraCmd.Parameters.Item("ps_CertificateNumberExists").Value

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return IIf(strCertificateNumberExists.ToLower().Equals("y"), True, False)

    End Function

    Public Function SaveNewCertificate(ByVal p_strCertificateNumber As String, _
                                       ByVal p_intCertificateTypeId As Integer, _
                                       ByVal p_strMatlNum As String, _
                                       ByVal p_strImporter As String, _
                                       ByVal p_strCustomer As String, _
                                       ByVal p_strOperatorName As String, _
                                       ByVal p_strCertificateExtension As String, _
                                       ByVal p_InsertPC As String, _
                                       ByRef p_ErrorDesc As String) As Integer

        Dim oraCmd As New OracleCommand
        Dim resNum As Integer
        Try

            'p_strMatlNum                in VARCHAR2,           
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            'pi_CertificationTypeId in  number,
            ParametersHelper.AddParametersToCommand("pi_CertificationTypeId", ParameterDirection.Input, OracleType.Number, p_intCertificateTypeId, oraCmd)
            'ps_CertificateNumber   in  varchar2,
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'pi_importerid IN NUMBER
            Dim strImporter As String
            If p_strImporter = String.Empty Then
                strImporter = 0
            Else
                strImporter = p_strImporter
            End If
            ParametersHelper.AddParametersToCommand("pi_importerid", ParameterDirection.Input, OracleType.Number, CInt(strImporter), oraCmd)
            'pi_customerid IN NUMBER
            Dim strCustomer As String
            If p_strCustomer = String.Empty Then
                strCustomer = 0
            Else
                strCustomer = p_strCustomer
            End If
            ParametersHelper.AddParametersToCommand("pi_customerid", ParameterDirection.Input, OracleType.Number, CInt(strCustomer), oraCmd)

            'ps_OperatorName        in  varchar2 
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            'ps_Extension_En   in  varchar2,
            ParametersHelper.AddParametersToCommand("ps_Extension_En", ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strCertificateExtension), oraCmd)
            'ps_InsertPC   in  varchar2,
            ParametersHelper.AddParametersToCommand("ps_InsertPC", ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_InsertPC), oraCmd)
            'pn_Error_Num   in  Number,
            ParametersHelper.AddParametersToCommand("pn_Error_Num", ParameterDirection.Output, OracleType.Number, 20, Nothing, oraCmd)
            'ps_ErrorMsg   in  varchar2,
            ParametersHelper.AddParametersToCommand("ps_ErrorMsg", ParameterDirection.Output, OracleType.VarChar, 4000, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.CertificateBasicInfo_Save"

            Connect()
            oraCmd.Connection = Connection

            oraCmd.ExecuteReader()
            resNum = oraCmd.Parameters.Item("pn_Error_Num").Value

            If oraCmd.Parameters.Item("ps_ErrorMsg").Value IsNot DBNull.Value Then
                p_ErrorDesc = oraCmd.Parameters.Item("ps_ErrorMsg").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return resNum

    End Function

    Public Function SaveNewCertificate(ByVal p_strCertificateNumber As String, _
                                       ByVal p_intCertificateTypeId As Integer, _
                                       ByVal p_strMatlNum As String, _
                                       ByVal p_strImporter As String, _
                                       ByVal p_strCustomer As String, _
                                       ByVal p_strOperatorName As String, _
                                       ByVal p_strCertificateExtension As String) As Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand

        Try

            'p_strMatlNum                in VARCHAR2,           
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            'pi_CertificationTypeId in  number,
            ParametersHelper.AddParametersToCommand("pi_CertificationTypeId", ParameterDirection.Input, OracleType.Number, p_intCertificateTypeId, oraCmd)
            'ps_CertificateNumber   in  varchar2,
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'pi_importerid IN NUMBER
            Dim strImporter As String
            If p_strImporter = String.Empty Then
                strImporter = 0
            Else
                strImporter = p_strImporter
            End If
            ParametersHelper.AddParametersToCommand("pi_importerid", ParameterDirection.Input, OracleType.Number, CInt(strImporter), oraCmd)
            'pi_customerid IN NUMBER
            Dim strCustomer As String
            If p_strCustomer = String.Empty Then
                strCustomer = 0
            Else
                strCustomer = p_strCustomer
            End If
            ParametersHelper.AddParametersToCommand("pi_customerid", ParameterDirection.Input, OracleType.Number, CInt(strCustomer), oraCmd)

            'ps_OperatorName        in  varchar2 
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            'ps_Extension_En   in  varchar2,
            ParametersHelper.AddParametersToCommand("ps_Extension_En", ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strCertificateExtension), oraCmd)
            'ps_InsertPC   in  varchar2,
            ParametersHelper.AddParametersToCommand("ps_InsertPC", ParameterDirection.Input, OracleType.VarChar, Nothing, oraCmd)
            'pn_Error_Num   in  Number,
            ParametersHelper.AddParametersToCommand("pn_Error_Num", ParameterDirection.Output, OracleType.Number, 20, Nothing, oraCmd)
            'ps_ErrorMsg   in  varchar2,
            ParametersHelper.AddParametersToCommand("ps_ErrorMsg", ParameterDirection.Output, OracleType.VarChar, 4000, Nothing, oraCmd)
            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.CertificateBasicInfo_Save"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnSaved = (rowsaffected = 1)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnSaved

    End Function

    Public Function ArchiveCertification(ByVal p_strCertificateNumber As String, _
                                         ByVal p_strOperatorName As String) As Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand

        Try

            'ps_CertificateNumber   in  varchar2,
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            'ps_OperatorName        in  varchar2 
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.Certificate_Archive"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnSaved = (rowsaffected = 1)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnSaved

    End Function

    Public Function GetCertifExtension(ByVal p_intImarkCertId As Integer) As String

        Dim strImarkCertExtension As String = String.Empty
        Dim oraCmd As New OracleCommand

        Try
            'pi_certificateId   IN NUMBER
            ParametersHelper.AddParametersToCommand("pi_certificateId", ParameterDirection.Input, OracleType.Number, p_intImarkCertId, oraCmd)

            'ps_extensionNumber OUT VARCHAR2
            ParametersHelper.AddParametersToCommand("ps_extensionNumber", ParameterDirection.Output, OracleType.VarChar, 0, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.GetCertifExtension"

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            If oraCmd.Parameters.Item("ps_extensionNumber").Value IsNot DBNull.Value Then
                strImarkCertExtension = oraCmd.Parameters.Item("ps_extensionNumber").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return strImarkCertExtension

    End Function

    Public Function GetLatestImarkCertifId() As Integer

        Dim intImarkCertId As Integer
        Dim oraCmd As New OracleCommand

        Try

            'ps_certificateNumber                out VARCHAR2,           
            ParametersHelper.AddParametersToCommand("pi_certificateId", ParameterDirection.Output, OracleType.Number, 0, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.GetLatestImarkCertifId"

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            If oraCmd.Parameters.Item("pi_certificateId").Value IsNot Nothing Then
                intImarkCertId = oraCmd.Parameters.Item("pi_certificateId").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return intImarkCertId

    End Function

    Public Function GetLatestGSOCertifNumber() As String

        Dim strGSOTempCertNumber As String = String.Empty
        Dim oraCmd As New OracleCommand

        Try

            'ps_certificateNumber                out VARCHAR2,           
            ParametersHelper.AddParametersToCommand("ps_certificateNumber", ParameterDirection.Output, OracleType.VarChar, 8000, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.getlatestgsocertifnumber"

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            If oraCmd.Parameters.Item("ps_certificateNumber").Value IsNot DBNull.Value Then
                strGSOTempCertNumber = oraCmd.Parameters.Item("ps_certificateNumber").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return strGSOTempCertNumber

    End Function

    Public Function GetCertificationTypeID(ByVal p_strCertificationTypeName As String) As Integer

        'Returns the certificationtypeid for a certification type name
        Dim oraCmd As New OracleCommand
        Dim p_intCertificationTypeId As Int32

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_COMMON_FUNCTIONS.GetCertificationID"
            ParametersHelper.AddParametersToCommand("ps_CertificationTypeName", ParameterDirection.Input, OracleType.VarChar, 50, p_strCertificationTypeName, oraCmd)
            ParametersHelper.AddParametersToCommand("retvalue", ParameterDirection.ReturnValue, OracleType.Int16, 0, oraCmd)
            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            'Gets the certificate number exists value
            p_intCertificationTypeId = Convert.ToInt32(oraCmd.Parameters.Item("retvalue").Value)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return p_intCertificationTypeId

    End Function


#End Region

#Region "Methods - NOM_Customer"

    Public Function AddCustomer(ByVal p_intSKUId As Integer, _
                                ByVal p_strCustomer As String, _
                                ByVal p_strImporter As String, _
                                ByVal p_strImporterRepresentative As String, _
                                ByVal p_strImporterAddress As String, _
                                ByVal p_strCountryLocation As String) As NameAid.SaveResult

        '        Dim blnSaved As Boolean = False
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand

        Try
            'pi_skuid in number,
            ParametersHelper.AddParametersToCommand("pi_skuid", ParameterDirection.Input, OracleType.Number, p_intSKUId, oraCmd)

            'ps_CUSTOMER in varchar2,
            ParametersHelper.AddParametersToCommand("ps_CUSTOMER", ParameterDirection.Input, OracleType.VarChar, p_strCustomer, oraCmd)

            'ps_IMPORTER in varchar2,
            ParametersHelper.AddParametersToCommand("ps_IMPORTER", ParameterDirection.Input, OracleType.VarChar, p_strImporter, oraCmd)

            'ps_IMPORTERREPRESENTATIVE in varchar2,
            ParametersHelper.AddParametersToCommand("ps_IMPORTERREPRESENTATIVE", ParameterDirection.Input, OracleType.VarChar, p_strImporterRepresentative, oraCmd)

            'ps_IMPORTERADDRESS in varchar2,
            ParametersHelper.AddParametersToCommand("ps_IMPORTERADDRESS", ParameterDirection.Input, OracleType.VarChar, p_strImporterAddress, oraCmd)

            'ps_COUNTRYLOCATION in varchar2
            ParametersHelper.AddParametersToCommand("ps_COUNTRYLOCATION", ParameterDirection.Input, OracleType.VarChar, p_strCountryLocation, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "CERTIFICATION_CRUD.AddCustomer"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = 1) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return enumSaveResult

    End Function

#End Region

#Region "Methods - ICS_MAINTENANCE"

    ' Added as per project 2706 technical specification

    ''' <summary>
    ''' Gets input value by checking not null or empty condition
    ''' </summary>
    ''' <param name="inputParameterValue">Input Parameter Value</param>
    ''' <returns>Returns DBNull.Value if input string is Null or Empty else returns input value</returns>
    ''' <remarks></remarks>
    Public Function GetInputValue(ByVal inputParameterValue As String) As Object

        If Not String.IsNullOrEmpty(inputParameterValue) Then
            Return inputParameterValue
        End If

        Return DBNull.Value

    End Function

    ''' <summary>
    ''' Gets the certified materials count
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strCertificateExtension">Certificate Extension</param>
    ''' <returns>Certified materials count as integer</returns>
    ''' <remarks></remarks>
    Public Function GetCertifiedMaterialCount(ByVal p_intCertificationTypeId As Integer, _
                                              ByVal p_strCertificateNumber As String, _
                                              ByVal p_strCertificateExtension As String) As Integer

        Dim intCertifiedMaterialsCount As Integer
        Dim oraCmd As New OracleCommand

        Try
            ' pn_CertificationTypeId IN NUMBER          
            ParametersHelper.AddParametersToCommand("pn_CertificationTypeId", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)

            ' ps_CertificateNumber IN VARCHAR2           
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)

            ' ps_Extension_EN IN VARCHAR2          
            ParametersHelper.AddParametersToCommand("ps_Extension_EN", ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strCertificateExtension), oraCmd)

            ' pn_Matl_Cnt OUT NUMBER          
            ParametersHelper.AddParametersToCommand("pn_Matl_Cnt", ParameterDirection.Output, OracleType.Number, 0, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.GetCertificateMatlCount"

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            If oraCmd.Parameters.Item("pn_Matl_Cnt").Value IsNot DBNull.Value Then
                intCertifiedMaterialsCount = oraCmd.Parameters.Item("pn_Matl_Cnt").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return intCertifiedMaterialsCount

    End Function

    ''' <summary>
    ''' Renames the certificate
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strOldCertificateNumber">Old Certificate Number</param>
    ''' <param name="p_strOldCertificateExtension">Old Certificate Extension</param>
    ''' <param name="p_strNewCertificateNumber">New Certificate Number</param>
    ''' <param name="p_strNewCertificateExtension">New Certificate Extension</param>
    ''' <returns>Successfully renamed or not boolean value (True/False)</returns>
    ''' <remarks></remarks>
    Public Function RenameCertificate(ByVal p_intCertificationTypeId As Integer, _
                                      ByVal p_strOldCertificateNumber As String, _
                                      ByVal p_strOldCertificateExtension As String, _
                                      ByVal p_strNewCertificateNumber As String, _
                                      ByVal p_strNewCertificateExtension As String, _
                                      ByVal p_strOperatorName As String) As Boolean

        Dim blnRenamed As Boolean = False
        Dim oraCmd As New OracleCommand

        Try
            ' pn_CertificationTypeId IN NUMBER          
            ParametersHelper.AddParametersToCommand("pn_CertificationTypeId", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)

            ' ps_OldCertificateNumber IN VARCHAR2          
            ParametersHelper.AddParametersToCommand("ps_OldCertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strOldCertificateNumber, oraCmd)

            ' ps_OldExtension_EN IN VARCHAR2          
            ParametersHelper.AddParametersToCommand("ps_OldExtension_EN", ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strOldCertificateExtension), oraCmd)

            ' ps_NewCertificateNumber IN VARCHAR2           
            ParametersHelper.AddParametersToCommand("ps_NewCertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strNewCertificateNumber, oraCmd)

            ' ps_NewExtension_EN IN VARCHAR2           
            ParametersHelper.AddParametersToCommand("ps_NewExtension_EN", ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strNewCertificateExtension), oraCmd)

            ' ps_OperatorName IN VARCHAR2
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.RenameCertificate"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnRenamed = (rowsaffected = 1)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnRenamed

    End Function

    ''' <summary>
    ''' Deletes the certificate
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strCertificateExtension">Certificate Extension</param>
    ''' <returns>Successfully deleted or not boolean value (True/False)</returns>
    ''' <remarks></remarks>
    Public Function DeleteCertificate(ByVal p_intCertificationTypeId As Integer, _
                                      ByVal p_strCertificateNumber As String, _
                                      ByVal p_strCertificateExtension As String, _
                                      ByVal p_strOperatorName As String) As Boolean

        Dim blnDeleted As Boolean = False
        Dim oraCmd As New OracleCommand

        Try
            ' pn_CertificationTypeId IN NUMBER          
            ParametersHelper.AddParametersToCommand("pn_CertificationTypeId", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)

            ' ps_CertificateNumber IN VARCHAR2           
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)

            ' ps_Extension_EN IN VARCHAR2          
            ParametersHelper.AddParametersToCommand("ps_Extension_EN", ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strCertificateExtension), oraCmd)

            ' ps_OperatorName IN VARCHAR2
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.DeleteCertificate"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnDeleted = (rowsaffected = 1)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnDeleted

    End Function

    ''' <summary>
    ''' Gets the certificate materials
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strCertificateExtension">Certificate Extension</param>
    ''' <returns>Certificate Materials as DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetCertificateMaterials(ByVal p_intCertificationTypeId As Integer, _
                                            ByVal p_strCertificateNumber As String, _
                                            ByVal p_strCertificateExtension As String) As DataTable

        Dim dtCertificateMaterials As New DataTable
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            ' pn_CertificationTypeId IN NUMBER           
            ParametersHelper.AddParametersToCommand("pn_CertificationTypeId", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)

            ' ps_CertificateNumber IN VARCHAR2          
            ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)

            ' ps_Extension_EN IN VARCHAR2           
            ParametersHelper.AddParametersToCommand("ps_Extension_EN", ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strCertificateExtension), oraCmd)

            ' pc_Cursor OUT SYS_REFCURSOR
            ParametersHelper.AddParametersToCommand("pc_Cursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.GetCertificateMatls"

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
            oraAdp.Fill(dtCertificateMaterials)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dtCertificateMaterials

    End Function

    ''' <summary>
    ''' Detaches the certificate
    ''' </summary>
    ''' <param name="p_intSkuId">Certification Id</param>
    ''' <param name="p_intCertificateId">Certificate Id</param>
    ''' <returns>Successfully detached or not boolean value (True/False)</returns>
    ''' <remarks></remarks>
    Public Function DetachCertificate(ByVal p_intSkuId As Integer, _
                                      ByVal p_intCertificateId As Integer) As Boolean

        Dim blnDetached As Boolean = False
        Dim oraCmd As New OracleCommand

        Try
            ' pn_SkuId IN NUMBER           
            ParametersHelper.AddParametersToCommand("pn_SkuId", ParameterDirection.Input, OracleType.Number, p_intSkuId, oraCmd)

            ' pn_CertificateId IN NUMBER           
            ParametersHelper.AddParametersToCommand("pn_CertificateId", ParameterDirection.Input, OracleType.Number, p_intCertificateId, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.DetachCertificate"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnDetached = (rowsaffected = 1)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnDetached

    End Function

    ''' <summary>
    ''' Moves the certificate
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strNewCertificateNumber">New Certificate Number</param>
    ''' <param name="p_strNewCertificateExtension">New Certificate Extension</param>
    ''' <returns>Successfully moved or not boolean value (True/False)</returns>
    ''' <remarks></remarks>
    Public Function MoveCertificate(ByVal p_intCertificationTypeId As Integer, _
                                    ByVal p_strNewCertificateNumber As String, _
                                    ByVal p_strNewCertificateExtension As String, _
                                    ByVal p_intSkuId As Integer, _
                                    ByVal p_intCertificateId As Integer, _
                                    ByVal p_strOperatorName As String) As Boolean

        Dim blnMoved As Boolean = False
        Dim oraCmd As New OracleCommand

        Try
            ' pn_CertificationTypeId IN NUMBER           
            ParametersHelper.AddParametersToCommand("pn_CertificationTypeId", ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)

            ' ps_CertificateNumber IN VARCHAR2           
            ParametersHelper.AddParametersToCommand("ps_NewCertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strNewCertificateNumber, oraCmd)

            ' ps_Extension_EN IN VARCHAR2           
            ParametersHelper.AddParametersToCommand("ps_NewExtension_EN", ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strNewCertificateExtension), oraCmd)

            ' pn_SkuId IN NUMBER           
            ParametersHelper.AddParametersToCommand("pn_SkuId", ParameterDirection.Input, OracleType.Number, p_intSkuId, oraCmd)

            ' pn_CertificateId IN NUMBER           
            ParametersHelper.AddParametersToCommand("pn_CertificateId", ParameterDirection.Input, OracleType.Number, p_intCertificateId, oraCmd)

            ' ps_OperatorName IN VARCHAR2
            ParametersHelper.AddParametersToCommand("ps_OperatorName", ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.MoveCertificate"

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnMoved = (rowsaffected = 1)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnMoved

    End Function

    ''' <summary>
    ''' Gets duplicate certificates
    ''' </summary>
    ''' <param name="p_strMaterialNumber">Material number</param>
    ''' <param name="p_strSpeedRating">Speed rating</param>
    ''' <returns>Duplicate correct certificates as DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetDuplicateCertificates(ByVal p_strMaterialNumber As String, _
                                             ByVal p_strSpeedRating As String) As DataTable

        Dim dtResults As New DataTable
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            'Ps_Matl_Num IN VARCHAR2
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMaterialNumber, oraCmd)

            'Ps_SpeedRating IN VARCHAR2
            ParametersHelper.AddParametersToCommand("ps_SpeedRating", ParameterDirection.Input, OracleType.VarChar, p_strSpeedRating, oraCmd)

            'Ps_Result OUT RETCURSOR
            ParametersHelper.AddParametersToCommand("ps_Result", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.GetDuplicateCert"

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
            oraAdp.Fill(dtResults)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dtResults

    End Function

    ''' <summary>
    ''' Delete duplicate certificates
    ''' </summary>
    ''' <param name="p_intSkuId">Sku id</param>
    ''' <returns>Indicates whether rows deleted or not as True (or) False</returns>
    ''' <remarks></remarks>
    Public Function DeleteDuplicateCertificates(ByVal p_intSkuId As Integer) As Boolean

        Dim blnDeleted As Boolean = False
        Dim oraCmd As New OracleCommand

        Try
            ' pn_SkuId IN NUMBER           
            ParametersHelper.AddParametersToCommand("pn_SkuId", ParameterDirection.Input, OracleType.Number, p_intSkuId, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.RemoveDuplicateCert"

            Connect()
            oraCmd.Connection = Connection

            Dim intRowsAffected As Integer = oraCmd.ExecuteNonQuery()
            blnDeleted = IIf(intRowsAffected > 0, True, False)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnDeleted

    End Function

    ''' <summary>
    ''' Get the records from Imark Family table
    ''' </summary>
    ''' <returns>datatable</returns>
    ''' <remarks></remarks>
    Public Function GetFamilies(ByVal pn_intCertificateID As Integer) As DataTable

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Try

            ParametersHelper.AddParametersToCommand("pn_CertificateID", ParameterDirection.Input, OracleType.Number, pn_intCertificateID, oraCmd)

            ParametersHelper.AddParametersToCommand("ps_Result", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.GetFamilies"

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
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

        Return dstResults.Tables(0)

    End Function

    ''' <summary>
    ''' Check whether the Family id exists or not and get the Family Desc
    ''' </summary>
    ''' <param name="p_intFamilyId">Family Id</param>
    ''' <param name="p_strFamilyDesc">Family Desc</param>
    ''' <returns>boolean value</returns>
    ''' <remarks></remarks>
    Public Function CheckIsFamilyIdExist(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As String, ByRef p_strFamilyDesc As String) As Boolean

        Dim oraCmd As New OracleCommand
        Dim strFamilyIdExists As String = String.Empty

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.CHECKIFFAMILYEXISTS"

            ParametersHelper.AddParametersToCommand("pn_Certificateid", ParameterDirection.Input, OracleType.Int32, p_intCertificateid, oraCmd)
            ParametersHelper.AddParametersToCommand("pn_FamilyID", ParameterDirection.Input, OracleType.Int32, p_intFamilyId, oraCmd)

            Dim oraFamilyIdExist As New OracleParameter()
            oraFamilyIdExist.ParameterName = "ps_Family_Exist"
            oraFamilyIdExist.OracleType = OracleType.VarChar
            oraFamilyIdExist.Direction = ParameterDirection.Output
            oraFamilyIdExist.Size = 1

            oraCmd.Parameters.Add(oraFamilyIdExist)

            Dim oraFamilyDesc As New OracleParameter()
            oraFamilyDesc.ParameterName = "ps_Family_Desc"
            oraFamilyDesc.OracleType = OracleType.VarChar
            oraFamilyDesc.Direction = ParameterDirection.Output
            oraFamilyDesc.Size = 50

            oraCmd.Parameters.Add(oraFamilyDesc)

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            strFamilyIdExists = oraCmd.Parameters.Item("ps_Family_Exist").Value
            p_strFamilyDesc = IIf(oraCmd.Parameters.Item("ps_Family_Desc").Value Is System.DBNull.Value, String.Empty, oraCmd.Parameters.Item("ps_Family_Desc").Value)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return IIf(strFamilyIdExists.ToUpper() = "Y", True, False)

    End Function


    ''' <summary>
    ''' Gets type's of Tyres.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTireType() As DataTable
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            Dim oraResults As New OracleParameter("pc_TIRETYPES", OracleType.Cursor)
            oraResults.Direction = ParameterDirection.Output
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "TESTRESULTS_CRUD.GetTireTypes"

            oraCmd.Parameters.Add(oraResults)

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
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

        Return dstResults.Tables(0)

    End Function

    'Added this operation as per IDEA2706 International Certification System Modifications 
    ''' <summary>
    ''' Inserts/updates the record in Imark family table
    ''' </summary>
    ''' <param name="p_intFamilyID">FamilyID </param>
    ''' <param name="p_strFamilyCode">FamilyCode</param>
    ''' <param name="p_strFamilyDesc">FamilyDesc</param>
    ''' <param name="p_strApplicationCat">ApplicationCat</param>
    ''' <param name="p_strConstructionType">ConstructionType</param>
    ''' <param name="p_strStructureType">StructureType</param>
    '''  <param name="p_strMountingType">MountingType</param>
    ''' <param name="p_strAspectRatioCat">AspectRatioCat</param>
    ''' <param name="p_strSpeedRatingCat">SpeedRatingCat</param>
    ''' <param name="p_strLoadIndexCat">LoadIndexCat</param>    
    ''' <returns>boolean value</returns>
    ''' <remarks></remarks>
    Public Function SaveFamily(ByVal p_intCertificateid As Integer, _
                                ByVal p_intFamilyID As Integer, _
                                ByVal p_strFamilyCode As String, _
                                ByVal p_strFamilyDesc As String, _
                                ByVal p_strApplicationCat As String, _
                                ByVal p_strConstructionType As String, _
                                ByVal p_strStructureType As String, _
                                ByVal p_strMountingType As String, _
                                ByVal p_strAspectRatioCat As String, _
                                ByVal p_strSpeedRatingCat As String, _
                                ByVal p_strLoadIndexCat As String, _
                                ByVal p_strUserName As String) As System.Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand
        Dim rowsaffected As Integer = 0
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.SAVEFAMILY"

            ParametersHelper.AddParametersToCommand("pn_Certificateid", ParameterDirection.Input, OracleType.Number, p_intCertificateid, oraCmd)
            ParametersHelper.AddParametersToCommand("pn_FamilyID", ParameterDirection.Input, OracleType.Number, p_intFamilyID, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_FamilyCode", ParameterDirection.Input, OracleType.VarChar, p_strFamilyCode, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_FamilyDesc", ParameterDirection.Input, OracleType.VarChar, p_strFamilyDesc, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_ApplicationCat", ParameterDirection.Input, OracleType.VarChar, p_strApplicationCat, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_ConstructionType", ParameterDirection.Input, OracleType.VarChar, p_strConstructionType, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_StructureType", ParameterDirection.Input, OracleType.VarChar, p_strStructureType, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_MountingType", ParameterDirection.Input, OracleType.VarChar, p_strMountingType, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_AspectRatioCat", ParameterDirection.Input, OracleType.VarChar, p_strAspectRatioCat, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_SpeedRatingCat", ParameterDirection.Input, OracleType.VarChar, p_strSpeedRatingCat, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_LoadIndexCat", ParameterDirection.Input, OracleType.VarChar, p_strLoadIndexCat, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_UserName", ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            rowsaffected = oraCmd.ExecuteNonQuery()
            blnSaved = IIf(rowsaffected = 1, True, False)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try
        Return blnSaved

    End Function

    ''' <summary>
    ''' Delete the record from Imark family table for the given family id
    ''' </summary>
    ''' <param name="p_intFamilyId">FamilyId</param>
    ''' <returns>Indicates whether rows deleted or not as True (or) False</returns>
    ''' <remarks></remarks>
    Public Function Deletefamily(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As Integer) As Boolean

        Dim blnDeleted As Boolean = False
        Dim oraCmd As New OracleCommand
        Dim intRowsAffected As Integer = 0
        Try
            ParametersHelper.AddParametersToCommand("pn_Certificateid", ParameterDirection.Input, OracleType.Number, p_intCertificateid, oraCmd)
            ParametersHelper.AddParametersToCommand("pn_FamilyID", ParameterDirection.Input, OracleType.Number, p_intFamilyId, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.DELETEFAMILY"

            Connect()
            oraCmd.Connection = Connection

            intRowsAffected = oraCmd.ExecuteNonQuery()
            blnDeleted = IIf(intRowsAffected > 0, True, False)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnDeleted

    End Function

    ''' <summary>
    ''' Get the Material Details
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetMaterial(ByVal p_strMaterialNumber As String) As DataTable

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Try
            ParametersHelper.AddParametersToCommand("ps_MATL_NUM", ParameterDirection.Input, OracleType.VarChar, p_strMaterialNumber, oraCmd)
            ParametersHelper.AddParametersToCommand("pc_Cursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.GET_MATERIALS"

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
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

        Return dstResults.Tables(0)

    End Function

    ''' <summary>
    ''' Updating Speedrating 
    ''' </summary>
    ''' <param name="p_intSkuID"></param>
    ''' <param name="p_strSpeedrating"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function EditMaterial(ByVal p_intSkuID As Integer, _
                                 ByVal p_strSpeedrating As String) As System.Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand
        Dim rowsaffected As Integer = 0
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.EDIT_PRODUCT"

            ParametersHelper.AddParametersToCommand("pn_SKUID", ParameterDirection.Input, OracleType.Number, p_intSkuID, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_speedrating", ParameterDirection.Input, OracleType.VarChar, p_strSpeedrating, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            rowsaffected = oraCmd.ExecuteNonQuery()
            blnSaved = IIf(rowsaffected = 1, True, False)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try
        Return blnSaved

    End Function


    ''' <summary>
    ''' Copy the record 
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Indicates whether material copied or not as True (or) False</returns>
    ''' <remarks></remarks>
    Public Function CopyCertification(ByVal p_strMatlNum As String) As Boolean

        Dim blnCopied As Boolean = False
        Dim oraCmd As New OracleCommand
        Dim intRowsAffected As Integer = 0
        Try
            ' pn_SkuId IN NUMBER           
            ParametersHelper.AddParametersToCommand("ps_MATL_NUM", ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.COPY_PRODUCT"

            Connect()
            oraCmd.Connection = Connection

            intRowsAffected = oraCmd.ExecuteNonQuery()
            blnCopied = IIf(intRowsAffected > 0, True, False)
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnCopied

    End Function

    ''' <summary>
    ''' Attach the record 
    ''' </summary>
    ''' <param name="p_strCertNum">Certificate Number</param>
    ''' <returns>Indicates whether material copied or not as True (or) False</returns>
    ''' <remarks></remarks>
    Public Function AttachCertification(ByVal p_skuid As Integer, ByVal p_strCertNum As String, ByVal p_strExtensionEn As String, ByVal p_certificationtypeid As Integer) As String


        Dim oraCmd As New OracleCommand
        Dim intRowsAffected As Integer = 0
        Dim ErrorMsg As String = String.Empty
        Try
            ' pn_SkuId IN NUMBER           
            ParametersHelper.AddParametersToCommand("pn_skuid", ParameterDirection.Input, OracleType.Number, p_skuid, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_certificateNumber", ParameterDirection.Input, OracleType.VarChar, p_strCertNum, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_Extension_EN", ParameterDirection.Input, OracleType.VarChar, p_strExtensionEn, oraCmd)
            ParametersHelper.AddParametersToCommand("pn_certificationtypeid", ParameterDirection.Input, OracleType.Number, p_certificationtypeid, oraCmd)


            Dim oraErrorMsg As New OracleParameter()
            oraErrorMsg.ParameterName = "ps_ErrorMsg"
            oraErrorMsg.OracleType = OracleType.VarChar
            oraErrorMsg.Direction = ParameterDirection.Output
            oraErrorMsg.Size = 30

            oraCmd.Parameters.Add(oraErrorMsg)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "ICS_MAINTENANCE.ATTACH_PRODUCT"

            Connect()
            oraCmd.Connection = Connection

            oraCmd.ExecuteReader()

            If oraCmd.Parameters.Item("ps_ErrorMsg").Value IsNot DBNull.Value Then
                ErrorMsg = oraCmd.Parameters.Item("ps_ErrorMsg").Value
            End If



        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return ErrorMsg

    End Function

    ''' <summary>
    ''' Refreshes Product data.
    ''' </summary>
    ''' <param name="p_strMaterialNumber">Material Number</param>
    ''' <param name="p_strErrorDesc">Error Description</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RefreshProduct(ByVal p_strMaterialNumber As String, ByRef p_strErrorDesc As String) As Integer
        Dim oraCmd As New OracleCommand
        Dim errNumber As Integer = 0

        Try
            ParametersHelper.AddParametersToCommand("ps_Matl_Num", ParameterDirection.Input, OracleType.VarChar, p_strMaterialNumber, oraCmd)

            ParametersHelper.AddParametersToCommand("pn_ErrorNum", ParameterDirection.Output, OracleType.Number, 20, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand("ps_ErrorMsg", ParameterDirection.Output, OracleType.VarChar, 4000, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "Ics_Maintenance.Refresh_Product"

            Connect()
            oraCmd.Connection = Connection

            oraCmd.ExecuteReader()

            errNumber = oraCmd.Parameters.Item("pn_ErrorNum").Value

            If oraCmd.Parameters.Item("ps_ErrorMsg").Value IsNot DBNull.Value Then
                p_strErrorDesc = oraCmd.Parameters.Item("ps_ErrorMsg").Value
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return errNumber
    End Function
#End Region

End Class