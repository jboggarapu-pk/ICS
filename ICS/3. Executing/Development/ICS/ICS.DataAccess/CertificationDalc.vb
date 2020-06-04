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
''' <list type="table">
''' <listheader>
''' <term>Author</term>
''' <description>Description</description>
''' </listheader>
''' <item>
''' <item>
''' <term>NA</term>
''' <description>
''' <para>NA</para>
''' <para>Original Code.</para>
''' </description>
''' </item>
''' <term>Sujitha</term>
''' <description>
''' <para>11/25/2019</para>
''' <para>Implemented code standardization.</para>
''' </description>
''' </item> 
''' </list>
''' </remarks>
Public Class CertificationDalc
    Inherits CooperTire.ICS.CTS.DALC.CtsDalc

    ' Modified as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ' Used Material Number instead of SKU, PSN instead of NPRId, TPN instead of PPN and Brand, Brand Line instead of Brand Code.
    ' Added Operation as parameter for HDR save methods, also added methods to retrieve data from web service.

#Region "CertificationDalc Variables"
    ''' <summary>
    ''' variable to hold the error code from the exception
    ''' </summary>
    ''' <remarks></remarks>
    Private errCode As Integer = 0

    ''' <summary>
    ''' variable to hold the error message from the exception
    ''' </summary>
    ''' <remarks></remarks>
    Private errMessage As String = String.Empty

    ''' <summary>
    ''' variable to hold the RetCursor value
    ''' </summary>
    ''' <remarks></remarks>
    Const RetCursor As String = "pc_retCursor"

    ''' <summary>
    ''' variable to hold the Material Number
    ''' </summary>
    ''' <remarks></remarks>
    Const MatlNum As String = "ps_Matl_Num"

    ''' <summary>
    ''' variable to hold Yes value
    ''' </summary>
    ''' <remarks></remarks>
    Const YesStr As String = "y"

    ''' <summary>
    ''' variable to hold No value
    ''' </summary>
    ''' <remarks></remarks>
    Const NoStr As String = "n"
#End Region

#Region "CertificationDalc Methods"

    ''' <summary>
    ''' Gets the search type results.
    ''' </summary>
    ''' <returns>Returns search type results as data set </returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetSearchTypeResults() As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Const GetSearchTypes As String = "ICS_CRUD.GetSearchTypes"
        Try
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetSearchTypes

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
    ''' <param name="p_strSize">size</param>
    ''' <returns>Returns manufacturing location results as data set.</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetManufacturingLocationsResults(ByVal p_strSize As String) As DataSet

        Dim dstResults As New DataSet           'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetManufacturingLocs As String = "ICS_CRUD.GetManufacturingLocs"
        Try
            'TODO : This function isn't being used anywhere, and this function can be removed
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetManufacturingLocs
             
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
    ''' Gets the grid data source for the Query user control
    ''' </summary>
    ''' <returns>Returns the grid data source as data table </returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetQueryControlGridSource() As DataTable
        Dim dstGridSource As New DataSet
        Dim dtbGridSource As New DataTable
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetQuerySource As String = "ICS_CRUD.GET_QUERY_SOURCE"
        Try
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetQuerySource

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
    ''' Gets the list of company names
    ''' </summary>
    ''' <returns>Returns the company names data as data set </returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCompanyNameList() As DataSet

        Dim dstResults As New DataSet           'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetCompanyNames As String = "ICS_CRUD.GetCompanyNames"
        Try

            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCompanyNames
 
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
    ''' Gets all Certification names
    ''' </summary>
    ''' <returns>Returns the certification names as data set </returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCertifications() As DataSet

        Dim dstResults As New DataSet           'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetCertification As String = "ICS_CRUD.GetCertifications"

        Try
            Dim oraResults As New OracleParameter(RetCursor, OracleType.Cursor)
            oraResults.Direction = ParameterDirection.Output
            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCertification
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
    ''' Gets all Regions data
    ''' </summary>
    ''' <returns>Returns the Regions data as data set </returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetAllRegions() As DataSet

        Dim dstResults As New DataSet           'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetRegions As String = "ICS_CRUD.GetRegions"

        Dim oraResults As New OracleParameter(RetCursor, OracleType.Cursor)
        oraResults.Direction = ParameterDirection.Output

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = GetRegions

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

    ''' <summary>
    ''' Gets the status of region certificate
    ''' </summary>
    ''' <param name="p_strBrand">brand</param>
    ''' <param name="p_strBrandLine">brand line</param>
    ''' <returns>Returns the region certificate status as data set.</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetRegionCertStatus(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet
        ' Added ps_BrandLine parameter as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const BrandProduct As String = "PC_BRANDPRODUCT"
        Const RegionsCertified As String = "PC_REGIONSCERTIFIED"
        Const RegionNotCertified As String = "PC_REGIONNOTCERTIFIED"
        Const PsBrand As String = "ps_Brand"
        Const PsBrandLine As String = "ps_Brand_Line"
        Const Searchbrand As String = "ICS_CRUD.SEARCHBRAND"

        Dim oraBRANDPRODUCT As New OracleParameter()
        oraBRANDPRODUCT.ParameterName = BrandProduct
        oraBRANDPRODUCT.OracleType = OracleType.Cursor
        oraBRANDPRODUCT.Direction = ParameterDirection.Output

        Dim oraREGIONSCERTIFIED As New OracleParameter()
        oraREGIONSCERTIFIED.ParameterName = RegionsCertified
        oraREGIONSCERTIFIED.OracleType = OracleType.Cursor
        oraREGIONSCERTIFIED.Direction = ParameterDirection.Output

        Dim oraEGIONSNOTCERTIFIED As New OracleParameter()
        oraEGIONSNOTCERTIFIED.ParameterName = RegionNotCertified
        oraEGIONSNOTCERTIFIED.OracleType = OracleType.Cursor
        oraEGIONSNOTCERTIFIED.Direction = ParameterDirection.Output

        ParametersHelper.AddParametersToCommand(PsBrand, ParameterDirection.Input, OracleType.VarChar, p_strBrand, oraCmd)
        ParametersHelper.AddParametersToCommand(PsBrandLine, ParameterDirection.Input, OracleType.VarChar, p_strBrandLine, oraCmd)

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = Searchbrand

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
    ''' <summary>
    ''' Gets the status of product certificate
    ''' </summary>
    ''' <param name="p_strBrand">brand</param>
    ''' <param name="p_strBrandLine">brand line</param>
    ''' <returns>Returns the product certificate status as data set.</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetProductCertStatus(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet
        'jeseitz 5/25/2016 - used in new marketing screen to retrieve certification requests for products
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const BrandProduct As String = "PC_BRANDPRODUCT"
        Const PsBrand As String = "ps_Brand"
        Const PsBrandLine As String = "ps_Brand_Line"
        Const SearchBrandRequests As String = "ICS_CRUD.SEARCHBRANDREQUESTS"

        Dim oraBRANDPRODUCT As New OracleParameter()
        oraBRANDPRODUCT.ParameterName = BrandProduct
        oraBRANDPRODUCT.OracleType = OracleType.Cursor
        oraBRANDPRODUCT.Direction = ParameterDirection.Output

        ParametersHelper.AddParametersToCommand(PsBrand, ParameterDirection.Input, OracleType.VarChar, p_strBrand, oraCmd)
        ParametersHelper.AddParametersToCommand(PsBrandLine, ParameterDirection.Input, OracleType.VarChar, p_strBrandLine, oraCmd)

        ' Configure the command object
        oraCmd.CommandType = CommandType.StoredProcedure
        oraCmd.CommandText = SearchBrandRequests

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
    ''' Saves the certification request
    ''' </summary>
    ''' <param name="p_DeleteMe">if set to <c>true</c> [p_ delete me].</param>
    ''' <param name="p_strMatlNum">SAP Material</param>
    ''' <param name="p_intCountryID">The country ID that certification is being requested.</param>
    ''' <param name="p_intSKUID">The SKU ID that certification is being requested.</param>
    ''' <returns>Returns true if saved, else returns false</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function SaveCertificationRequest(ByVal p_DeleteMe As Boolean, ByVal p_strMatlNum As String, _
                                        ByVal p_intCountryID As Integer, ByVal p_intSKUID As Integer) As Boolean

        Dim oraCmd As New OracleCommand
        Dim blnSaved As Boolean = False
        Const CreateOrDeleteProductCountry As String = "ICS_CRUD.CreateOrDeleteProductCountry"
        Const DeleteMe As String = "ps_DeleteMe"
        Const PiSkuId As String = "pi_SkuId"
        Const PiCountryId As String = "pi_Countryid"

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CreateOrDeleteProductCountry

            Dim opDeleteMe As New OracleParameter()
            opDeleteMe.ParameterName = DeleteMe
            opDeleteMe.Direction = ParameterDirection.Input
            opDeleteMe.OracleType = OracleType.Char
            If p_DeleteMe Then
                opDeleteMe.Value = YesStr
            Else
                opDeleteMe.Value = NoStr
            End If
            opDeleteMe.Size = 1
            oraCmd.Parameters.Add(opDeleteMe)

            Dim opMatlNum As New OracleParameter()
            opMatlNum.ParameterName = MatlNum
            opMatlNum.Direction = ParameterDirection.Input
            opMatlNum.OracleType = OracleType.VarChar
            opMatlNum.Value = p_strMatlNum
            opMatlNum.Size = p_strMatlNum.Length
            oraCmd.Parameters.Add(opMatlNum)

            ParametersHelper.AddParametersToCommand(PiSkuId, ParameterDirection.Input, OracleType.Number, p_intSKUID, oraCmd)

            Dim opCountryId As New OracleParameter()
            opCountryId.ParameterName = PiCountryId
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
    ''' Saves the certification group countries
    ''' </summary>
    ''' <param name="p_DeleteMe">if set to <c>true</c> [p_ delete me].</param>
    ''' <param name="p_strMatlNum">SAP Material</param>
    ''' <param name="p_intCertificationID">The country ID that certification is being requested.</param>
    ''' <param name="p_intSKUID">The SKU ID that certification is being requested.</param>
    ''' <returns>Returns true if saved, else returns false</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function SaveCertificationGroup(ByVal p_DeleteMe As Boolean, ByVal p_strMatlNum As String, _
                                        ByVal p_intCertificationID As Integer, ByVal p_intSKUID As Integer) As Boolean

        Dim oraCmd As New OracleCommand
        Dim blnSaved As Boolean = False
        Const ProductCountrySave As String = "ICS_CRUD.ProductCountry_Save"
        Const DeleteMe As String = "ps_DeleteMe"
        Const PiSkuId As String = "pi_SKUID"
        Const PiCertificationId As String = "pi_CertificationId"

        Try

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = ProductCountrySave

            Dim opDeleteMe As New OracleParameter()
            opDeleteMe.ParameterName = DeleteMe
            opDeleteMe.Direction = ParameterDirection.Input
            opDeleteMe.OracleType = OracleType.Char
            If p_DeleteMe Then
                opDeleteMe.Value = YesStr
            Else
                opDeleteMe.Value = NoStr
            End If
            oraCmd.Parameters.Add(opDeleteMe)

            Dim opMatlNum As New OracleParameter()
            opMatlNum.ParameterName = MatlNum
            opMatlNum.Direction = ParameterDirection.Input
            opMatlNum.OracleType = OracleType.VarChar
            opMatlNum.Value = p_strMatlNum
            oraCmd.Parameters.Add(opMatlNum)

            Dim opSKUID As New OracleParameter()
            opSKUID.ParameterName = PiSkuId
            opSKUID.Direction = ParameterDirection.Input
            opSKUID.OracleType = OracleType.Number
            opSKUID.Value = p_intSKUID
            oraCmd.Parameters.Add(opSKUID)

            Dim opCertificationId As New OracleParameter()
            opCertificationId.ParameterName = PiCertificationId
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
    ''' Saves the certification requests from grid on MarketingNew screen
    ''' </summary>
    ''' <param name="p_DeleteMe">if set to <c>true</c> [p_ delete me].</param>
    ''' <param name="p_strMatlNum">SAP Material</param>
    ''' <param name="p_intCertificationID">The country ID that certification is being requested.</param>
    ''' <param name="p_intSKUID">The SKU ID that certification is being requested.</param>
    ''' <returns>Returns true if saved, else returns false</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function SaveRequestCert(ByVal p_DeleteMe As Boolean, ByVal p_strMatlNum As String, _
                                        ByVal p_intCertificationID As Integer, ByVal p_intSKUID As Integer) As Boolean

        Dim oraCmd As New OracleCommand
        Dim blnSaved As Boolean = False
        Const ProductRequestCertSave As String = "ICS_CRUD.ProductRequestCert_Save"
        Const DeleteMe As String = "ps_DeleteMe"
        Const PiSkuId As String = "pi_SKUID"
        Const PiCertificationId As String = "pi_CertificationId"

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = ProductRequestCertSave

            Dim opDeleteMe As New OracleParameter()
            opDeleteMe.ParameterName = DeleteMe
            opDeleteMe.Direction = ParameterDirection.Input
            opDeleteMe.OracleType = OracleType.Char
            If p_DeleteMe Then
                opDeleteMe.Value = YesStr
            Else
                opDeleteMe.Value = NoStr
            End If
            oraCmd.Parameters.Add(opDeleteMe)

            Dim opMatlNum As New OracleParameter()
            opMatlNum.ParameterName = MatlNum
            opMatlNum.Direction = ParameterDirection.Input
            opMatlNum.OracleType = OracleType.VarChar
            opMatlNum.Value = p_strMatlNum
            oraCmd.Parameters.Add(opMatlNum)

            Dim opSKUID As New OracleParameter()
            opSKUID.ParameterName = PiSkuId
            opSKUID.Direction = ParameterDirection.Input
            opSKUID.OracleType = OracleType.Number
            opSKUID.Value = p_intSKUID
            oraCmd.Parameters.Add(opSKUID)

            Dim opCertificationId As New OracleParameter()
            opCertificationId.ParameterName = PiCertificationId
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
    ''' Gets the countries names based on provided region name
    ''' </summary>
    ''' <param name="p_strRegionName">Region name to get the country data</param>
    ''' <returns>Returns countries data as data set</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCountries(ByVal p_strRegionName As String) As DataSet

        Dim dstResults As New DataSet           'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetCountriesByRegionName As String = "ICS_CRUD.GetCountriesByRegionName"
        Const Countries As String = "pc_Countries"
        Const RegionName As String = "ps_RegionName"

        Try
            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCountriesByRegionName

            Dim oraResults As New OracleParameter()
            oraResults.ParameterName = Countries
            oraResults.OracleType = OracleType.Cursor
            oraResults.Direction = ParameterDirection.Output
            ' Add the parameters
            oraCmd.Parameters.Add(oraResults)

            Dim oraRegionName As New OracleParameter()
            oraRegionName.ParameterName = RegionName
            oraRegionName.OracleType = OracleType.VarChar
            oraRegionName.Direction = ParameterDirection.Input
            oraRegionName.Value = p_strRegionName
            oraRegionName.Size = p_strRegionName.Length
            ' Add the parameters
            oraCmd.Parameters.Add(oraRegionName)
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
    ''' Gets NOM Importers
    ''' </summary>
    ''' <returns>Returns importers data as data set</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetImporters() As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetImporter As String = "ICS_CRUD.GETIMPORTERS"
        Const Importers As String = "pc_importers"

        Try
            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetImporter

            ParametersHelper.AddParametersToCommand(Importers, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
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
    ''' Gets NOM Customers
    ''' </summary>
    ''' <returns>Returns Nom customers data as data set</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCustomers() As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetCustomer As String = "ICS_CRUD.GETCUSTOMERS"
        Const Customers As String = "pc_customers"

        Try
            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCustomer

            ParametersHelper.AddParametersToCommand(Customers, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
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
    ''' Gets the certification search results
    ''' </summary>
    ''' <param name="ps_SearchCriteria">Search criteria</param>
    ''' <param name="p_SearchType">Search type</param>
    ''' <param name="p_strExtensionNo">Extension number</param>
    ''' <param name="p_strImarkFamily">Imark family</param>
    ''' <param name="ps_BrandLine">Brand line</param>
    ''' <returns>Returns certification search results as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCertificationSearchResults(ByVal ps_SearchCriteria As String, ByVal p_SearchType As String, ByVal p_strExtensionNo As String, ByVal p_strImarkFamily As String, ByVal ps_BrandLine As String) As DataTable
        ' Added ps_BrandLine parameter as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
        Dim dstResults As New DataSet       'return

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const SearchCriteria As String = "ps_SearchCriteria"
        Const SearchType As String = "ps_SearchType"
        Const ExtensionNo As String = "ps_ExtensionNo"
        Const ImarkFamily As String = "ps_imarkFamily"
        Const BrandLine As String = "ps_BrandLine"
        Const GetCertificationSearchResult As String = "CERTIFICATION_CRUD.GET_CERTIFICATIONSEARCHRESULT"

        Try
            'Add's the parameter info to the Oracle Command
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(SearchCriteria, ParameterDirection.Input, OracleType.VarChar, ps_SearchCriteria, oraCmd)
            ParametersHelper.AddParametersToCommand(SearchType, ParameterDirection.Input, OracleType.VarChar, p_SearchType, oraCmd)
            ParametersHelper.AddParametersToCommand(ExtensionNo, ParameterDirection.Input, OracleType.VarChar, p_strExtensionNo, oraCmd)
            ParametersHelper.AddParametersToCommand(ImarkFamily, ParameterDirection.Input, OracleType.VarChar, p_strImarkFamily, oraCmd)
            ParametersHelper.AddParametersToCommand(BrandLine, ParameterDirection.Input, OracleType.VarChar, ps_BrandLine, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCertificationSearchResult

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
    ''' Gets brands
    ''' </summary>
    ''' <param name="p_strBrand">Brand name</param>
    ''' <returns>Returns brands data as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetBrands(ByVal p_strBrand As String) As DataTable

        ' Added GetBrands method to retrieve Brands through Product Data Web Service
        ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

        'Added as per Incident # 31208 and Change Order # 6074
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetBrand As String = "CERTIFICATION_CRUD.GET_BRANDS"

        Try
            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetBrand

            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
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
    ''' Gets the Brand Line results
    ''' </summary>
    ''' <param name="p_strLine">Brand line</param>
    ''' <returns>Returns brand lines data as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetBrandLines(ByVal p_strLine As String) As DataTable

        ' Added GetBrandLines method to retrieve the Brand Lines from Product data web service 
        ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

        'Added as per Incident # 31208 and Change Order # 6074
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetBrandLine As String = "CERTIFICATION_CRUD.GET_BRANDLINES"
        Const Brand As String = "ps_Brand"

        Try
            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetBrandLine

            ParametersHelper.AddParametersToCommand(Brand, ParameterDirection.Input, OracleType.VarChar, p_strLine, oraCmd)
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
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
    ''' Gets the attributes for Material ID List
    ''' </summary>
    ''' <param name="p_strMaterialIdList">Material Id list</param>
    ''' <returns>Returns material attributes as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetMaterialAttribs(ByVal p_strMaterialIdList As String) As DataTable
        ' Added GetMaterialAttribs method to retrieve the attributes for material number from Product data web service 
        ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

        Dim dstResults As New DataSet
        Dim dtAttribs As New DataTable
        errCode = 0
        errMessage = String.Empty

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetSkuDescriptors As String = "CERTIFICATION_CRUD.GETSKUDESCRIPTORS"
         
        Try
            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetSkuDescriptors

            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMaterialIdList, oraCmd)
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
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
    ''' Saves audit Log entry
    ''' </summary>
    ''' <param name="p_dteChangeDateTime">Changed date and time</param>
    ''' <param name="m_strChangedBy">Changed by</param>
    ''' <param name="m_strArea">Area</param>
    ''' <param name="m_strChangedFieldElement">Changed field</param>
    ''' <param name="m_strOldValue">Old value</param>
    ''' <param name="m_strNewValue">New value</param>
    ''' <param name="m_intReasonID">Reason Id</param>
    ''' <param name="m_strNote">Notes</param>
    ''' <returns>Returns true if saved, else returns false</returns>
    ''' <exception cref="Exception">
    ''' In case of exception, function returns false
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        Const AuditLogInsert As String = "CERTIFICATION_CRUD.AUDITLOG_INSERT"
        Const ChangeDateTime As String = "pd_ChangeDateTime"
        Const ChangedBy As String = "ps_ChangedBy"
        Const Area As String = "ps_Area"
        Const ChangedFiledElement As String = "ps_ChangedFiled_Element"
        Const OldValue As String = "ps_OLDValue"
        Const NewValue As String = "ps_NewValue"
        Const ReasonId As String = "pi_ReasonID"
        Const Note As String = "ps_Note"
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = AuditLogInsert

            ParametersHelper.AddParametersToCommand(ChangeDateTime, ParameterDirection.Input, OracleType.DateTime, DateTime.Now(), oraCmd)
            ParametersHelper.AddParametersToCommand(ChangedBy, ParameterDirection.Input, OracleType.VarChar, m_strChangedBy, oraCmd)
            ParametersHelper.AddParametersToCommand(Area, ParameterDirection.Input, OracleType.VarChar, m_strArea, oraCmd)
            ParametersHelper.AddParametersToCommand(ChangedFiledElement, ParameterDirection.Input, OracleType.VarChar, m_strChangedFieldElement, oraCmd)
            ParametersHelper.AddParametersToCommand(OldValue, ParameterDirection.Input, OracleType.VarChar, m_strOldValue, oraCmd)
            ParametersHelper.AddParametersToCommand(NewValue, ParameterDirection.Input, OracleType.VarChar, m_strNewValue, oraCmd)
            ParametersHelper.AddParametersToCommand(ReasonId, ParameterDirection.Input, OracleType.Number, m_intReasonID, oraCmd)
            ParametersHelper.AddParametersToCommand(Note, ParameterDirection.Input, OracleType.VarChar, m_strNote, oraCmd)
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
    ''' Gets the certificate info
    ''' </summary>
    ''' <param name="ps_CertificationNumber">Certification number</param>
    ''' <param name="ps_ExtensionNo">Extension number</param>
    ''' <param name="ps_CertificationTypeID">Certification type id</param>
    ''' <param name="p_iSKUID">Sku id</param>
    ''' <param name="p_blnTRsExist">True/False</param>
    ''' <returns>Returns certificate data as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCertificate(ByVal ps_CertificationNumber As String, ByVal ps_ExtensionNo As String, ByVal ps_CertificationTypeID As Integer, ByVal p_iSKUID As Integer, ByRef p_blnTRsExist As Boolean) As DataTable

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const CertificatioNumber As String = "ps_certificationNumber"
        Const ExtensionNo As String = "ps_extensionNo"
        Const CertificationTypeId As String = "pi_certificationTypeID"
        Const SkuId As String = "pi_SKUId"
        Const TrExists As String = "ps_TRExists"
        Const GetCertificatesInfo As String = "CERTIFICATION_CRUD.GetCertificatesInfo"
        Try
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificatioNumber, ParameterDirection.Input, OracleType.VarChar, ps_CertificationNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(ExtensionNo, ParameterDirection.Input, OracleType.VarChar, ps_ExtensionNo, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, ps_CertificationTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_iSKUID, oraCmd)
            ParametersHelper.AddParametersToCommand(TrExists, ParameterDirection.Output, OracleType.VarChar, 1, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCertificatesInfo

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
            oraAdp.Fill(dstResults)

            p_blnTRsExist = False
            If Not oraCmd.Parameters.Item(TrExists).Value.Equals(DBNull.Value) Then
                p_blnTRsExist = oraCmd.Parameters.Item(TrExists).Value.Equals(YesStr)
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
    ''' Gets the similar certificate info
    ''' </summary>
    ''' <param name="p_iCertificationTypeID">Certification type id</param>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_strCertificationNumber">Certification number</param>
    ''' <returns>Returns similar certificate information as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetSimilarCertificate(ByVal p_iCertificationTypeID As Integer, ByVal p_strMatlNum As String, ByVal p_strCertificationNumber As String) As DataTable
        Dim dstResults As New DataSet       'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const CertificationTypeId As String = "pi_CertificationTypeID"
        Const CertificationNumber As String = "ps_CertificationNumber"
        Const GetSimilarCertificateInfo As String = "CERTIFICATION_CRUD.GetSimilarCertificateInfo"
        Try
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_iCertificationTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificationNumber, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetSimilarCertificateInfo

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
    ''' Saves certificate info
    ''' </summary>
    ''' <param name="p_iSKUId">Sku Id</param>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_blnRemoveMatlNum">Remove material number?True/False</param>
    ''' <param name="p_strCertificationTypeName">Certification type</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate number</param>
    ''' <param name="p_dteCertDateSubmitted">Certificate submitted date</param>
    ''' <param name="p_dteCertDateApproved_CEGI">Certificate approved date</param>
    ''' <param name="p_dteDATESUBMITED">Submitted date</param>
    ''' <param name="pc_ACTIVESTATUS">Active status</param>
    ''' <param name="p_dteDATEASSIGNED_EGI">EGI assigned date</param>
    ''' <param name="p_dteDATEAPROVED_CEGI">CEGI approved date</param>
    ''' <param name="pc_RENEWALREQUIRED_CGIN">CGIN renewal required</param>
    ''' <param name="p_strJOBREPORTNUMBER_CEN">CEN job report number</param>
    ''' <param name="p_strEXTENSION_EN">EN extension</param>
    ''' <param name="p_strSUPPLEMENTALMOLDSTAMPING_E">Supplemental mold stamping</param>
    ''' <param name="p_strEMARKREFERENCE_I">Mark reference</param>
    ''' <param name="p_dteEXPIRYDATE_I">Expiry date</param>
    ''' <param name="p_strFAMILY_I">Family</param>
    ''' <param name="p_strPRODUCTLOCATION">Product location</param>
    ''' <param name="p_strCOUNTRYOFMANUFACTURE_N">Manufacturing country</param>
    ''' <param name="p_blnAddNewCustomer">Add new customer?True/False</param>
    ''' <param name="p_strActSigReq">Act Sig Required</param>
    ''' <param name="p_intCUSTOMERID">Customer Id</param>
    ''' <param name="p_strCUSTOMER_N">Customer</param>
    ''' <param name="p_strCUSTOMERADDRESS_N">Customer address</param>
    ''' <param name="p_strCUSTOMERSPECIFIC_N">Customer specific info</param>
    ''' <param name="p_blnAddNewImporter">Add new importer?True/False</param>
    ''' <param name="p_intIMPORTERID">Importer id</param>
    ''' <param name="p_strIMPORTER_N">Importer</param>
    ''' <param name="p_strIMPORTERADDRESS_N">Importer address</param>
    ''' <param name="p_strIMPORTERREPRESENTATIVE_N">Importer representative</param>
    ''' <param name="p_strCOUNTRYLOCATION_N">Country location</param>
    ''' <param name="p_strBATCHNUMBER_G">Batch number</param>
    ''' <param name="p_dteSUPPLEMENTALASSIGNED">Supplemental assigned date</param>
    ''' <param name="p_dteSUPPLEMENTALSUBMITTED">Supplemental submitted date</param>
    ''' <param name="p_dteSUPPLEMENTALAPPROVED">Supplemental approved date</param>
    ''' <param name="p_strCOMPANYNAME">Company name</param>
    ''' <param name="p_strUserName">User name</param>
    ''' <param name="p_intCertificateNumberID">Certificate number Id</param>
    ''' <param name="p_strFamilyDesc">Family description</param>
    ''' <param name="p_blnMoldChgRequired">Mold change required?True/False</param>
    ''' <param name="p_dteOperDateApproved">Oper approved date</param>
    ''' <param name="p_strAddInfo">additional info</param>
    ''' <returns>Returns saves result as integer value</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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

        ''Added p_strFamilyDesc parameter as per IDEA2706 International Certification System Modifications 
        ''JBH_2.00 Project 5325: Added Mold Change Required and Operations Date Approved parameters

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Dim oraParam As OracleParameter
        Const RetId As String = "pi_RetId"
        Const CertificateId As String = "pi_CertificateID"
        Const SkuId As String = "pi_SKUId"
        Const RemoveMatlNum As String = "ps_Remove_Matl_Num"
        Const CertificationTypeName As String = "ps_CertificationTypeName"
        Const CertificateNumber As String = "ps_CERTIFICATENUMBER"
        Const CertDateSubmitted As String = "pd_certdatesubmitted"
        Const CertDateApprovedCegi As String = "pd_certdateapproved_cegi"
        Const DateSubmitted As String = "pd_DATESUBMITED"
        Const ActiveStatus As String = "pc_ACTIVESTATUS"
        Const DateAssignedEgi As String = "pd_DATEASSIGNED_EGI"
        Const DateApprovedCegi As String = "pd_DateApproved_CEGI"
        Const RenewalRequiredCgin As String = "pc_RENEWALREQUIRED_CGIN"
        Const JobReportNumberCen As String = "ps_JOBREPORTNUMBER_CEN"
        Const ExtensionEn As String = "ps_EXTENSION_EN"
        Const SupplementalMoldStamping As String = "ps_SUPPLEMENTALMOLDSTAMPING_E"
        Const EmarkReference As String = "ps_EMARKREFERENCE_I"
        Const ExpiryDate As String = "pd_EXPIRYDATE_I"
        Const FamilyI As String = "ps_FAMILY_I"
        Const FamilyDesc As String = "ps_Family_Desc"
        Const ProductLocation As String = "ps_PRODUCTLOCATION"
        Const CountryOfManufacture As String = "ps_COUNTRYOFMANUFACTURE_N"
        Const AddNewcustomer As String = "ps_addnewCustomer"
        Const ActSigReq As String = "ps_actSigreq"
        Const CustomerId As String = "pi_CUSTOMERID"
        Const CustomerN As String = "ps_CUSTOMER_N"
        Const CustomerAddressN As String = "ps_CUSTOMERADDRESS_N"
        Const CustomerSpecificN As String = "ps_CUSTOMERSPECIFIC_N"
        Const AddNewImporter As String = "ps_addnewImporter"
        Const ImporterId As String = "pi_IMPORTERID"
        Const ImporterN As String = "ps_IMPORTER_N"
        Const ImporterAddressN As String = "ps_IMPORTERADDRESS_N"
        Const ImporterRepresentativeN As String = "ps_IMPORTERREPRESENTATIVE_N"
        Const CountrylocationN As String = "ps_COUNTRYLOCATION_N"
        Const BatchNumberG As String = "ps_BATCHNUMBER_G"
        Const CompanyName As String = "ps_COMPANYNAME"
        Const Username As String = "ps_UserName"
        Const MoldChanged As String = "ps_Mold_Changed"
        Const OperDateApproved As String = "pd_Oper_Date_Approved"
        Const AdditionalInfo As String = "ps_Additional_Info"
        Const CertificateSave As String = "CERTIFICATION_CRUD.CERTIFICATE_SAVE"
        Try
            ParametersHelper.AddParametersToCommand(retId, ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(certificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_iSKUId, oraCmd)

            oraParam = New OracleParameter(MatlNum, p_strMatlNum)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            Dim p_strRemoveMatlNum As String = String.Empty
            If p_blnRemoveMatlNum = True Then
                p_strRemoveMatlNum = YesStr
            Else
                p_strRemoveMatlNum = NoStr
            End If
            oraParam = New OracleParameter(RemoveMatlNum, p_strRemoveMatlNum)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '3  ps_CertificationTypeName
            oraParam = New OracleParameter(CertificationTypeName, p_strCertificationTypeName)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '4  ps_CERTIFICATENUMBER 
            oraParam = New OracleParameter(CertificateNumber, p_strCERTIFICATENUMBER)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            'pd_certdatesubmitted
            oraParam = New OracleParameter(CertDateSubmitted, IIf(p_dteCertDateSubmitted.Equals(DateTime.MinValue), DBNull.Value, p_dteCertDateSubmitted))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.DateTime, DbType)
            oraCmd.Parameters.Add(oraParam)
            'pd_certdateapproved_cegi
            oraParam = New OracleParameter(CertDateApprovedCegi, IIf(p_dteCertDateApproved_CEGI.Equals(DateTime.MinValue), DBNull.Value, p_dteCertDateApproved_CEGI))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.DateTime, DbType)
            oraCmd.Parameters.Add(oraParam)

            '5 pd_DATESUBMITED
            oraParam = New OracleParameter(DateSubmitted, IIf(p_dteDATESUBMITED.Equals(DateTime.MinValue), DBNull.Value, p_dteDATESUBMITED))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.DateTime, DbType)
            oraCmd.Parameters.Add(oraParam)
            '6 pc_ACTIVESTATUS
            oraParam = New OracleParameter(ActiveStatus, pc_ACTIVESTATUS)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '7 pd_DATEASSIGNED_EGI
            oraParam = New OracleParameter(DateAssignedEgi, IIf(p_dteDATEASSIGNED_EGI.Equals(DateTime.MinValue), DBNull.Value, p_dteDATEASSIGNED_EGI))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.DateTime, DbType)
            oraCmd.Parameters.Add(oraParam)
            '8 pd_DateApproved_CEGI
            oraParam = New OracleParameter(DateApprovedCegi, IIf(p_dteDATEAPROVED_CEGI.Equals(DateTime.MinValue), DBNull.Value, p_dteDATEAPROVED_CEGI))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.DateTime, DbType)
            oraCmd.Parameters.Add(oraParam)
            '9  pc_RENEWALREQUIRED_CGIN
            oraParam = New OracleParameter(RenewalRequiredCgin, pc_RENEWALREQUIRED_CGIN)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '12 ps_JOBREPORTNUMBER_CEN
            oraParam = New OracleParameter(JobReportNumberCen, p_strJOBREPORTNUMBER_CEN)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '13 ps_EXTENSION_EN
            oraParam = New OracleParameter(ExtensionEn, p_strEXTENSION_EN)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '14 ps_SUPPLEMENTALMOLDSTAMPING_E
            oraParam = New OracleParameter(SupplementalMoldStamping, p_strSUPPLEMENTALMOLDSTAMPING_E)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '15 ps_EMARKREFERENCE_I
            oraParam = New OracleParameter(EmarkReference, p_strEMARKREFERENCE_I)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '16 pd_EXPIRYDATE_I
            oraParam = New OracleParameter(ExpiryDate, IIf(p_dteEXPIRYDATE_I.Equals(DateTime.MinValue), DBNull.Value, p_dteEXPIRYDATE_I))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.DateTime, DbType)
            oraCmd.Parameters.Add(oraParam)
            '17 ps_FAMILY_I
            oraParam = New OracleParameter(FamilyI, p_strFAMILY_I)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            '18 ps_Family_Desc
            oraParam = New OracleParameter(FamilyDesc, p_strFamilyDesc)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            '19 ps_PRODUCTLOCATION_C
            oraParam = New OracleParameter(ProductLocation, p_strPRODUCTLOCATION)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '20 ps_COUNTRYOFMANUFACTURE_N
            oraParam = New OracleParameter(CountryOfManufacture, p_strCOUNTRYOFMANUFACTURE_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            Dim p_strAddNewCustomer As String = String.Empty
            If p_blnAddNewCustomer = True Then
                p_strAddNewCustomer = YesStr
            Else
                p_strAddNewCustomer = NoStr
            End If
            oraParam = New OracleParameter(AddNewcustomer, p_strAddNewCustomer)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            'Actual Signature Required
            ParametersHelper.AddParametersToCommand(ActSigReq, ParameterDirection.Input, OracleType.VarChar, p_strActSigReq, oraCmd)

            'Customer ID
            ParametersHelper.AddParametersToCommand(CustomerId, ParameterDirection.Input, OracleType.Number, p_intCUSTOMERID, oraCmd)

            '22 ps_CUSTOMER_N
            oraParam = New OracleParameter(CustomerN, p_strCUSTOMER_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            oraParam = New OracleParameter(CustomerAddressN, p_strCUSTOMERADDRESS_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '21 ps_CUSTOMERSPECIFIC_N
            oraParam = New OracleParameter(CustomerSpecificN, p_strCUSTOMERSPECIFIC_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            Dim p_strAddNewImporter As String = String.Empty
            If p_blnAddNewImporter = True Then
                p_strAddNewImporter = YesStr
            Else
                p_strAddNewImporter = NoStr
            End If
            oraParam = New OracleParameter(AddNewImporter, p_strAddNewImporter)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            ParametersHelper.AddParametersToCommand(ImporterId, ParameterDirection.Input, OracleType.Number, p_intIMPORTERID, oraCmd)

            '23 ps_IMPORTER_N
            oraParam = New OracleParameter(ImporterN, p_strIMPORTER_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '24 ps_IMPORTERADDRESS_N
            oraParam = New OracleParameter(ImporterAddressN, p_strIMPORTERADDRESS_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '25 ps_IMPORTERREPRESENTATIVE_N
            oraParam = New OracleParameter(ImporterRepresentativeN, p_strIMPORTERREPRESENTATIVE_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            '26 ps_COUNTRYLOCATION_N
            oraParam = New OracleParameter(CountrylocationN, p_strCOUNTRYLOCATION_N)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '27  ps_BATCHNUMBER_G
            oraParam = New OracleParameter(BatchNumberG, p_strBATCHNUMBER_G)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            oraParam = New OracleParameter(CompanyName, p_strCOMPANYNAME)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            oraParam = New OracleParameter(Username, p_strUserName)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            'Mold Change Required - JBH_2.00 Project 5325
            Dim p_strMoldChgRequired As String = String.Empty
            If p_blnMoldChgRequired = True Then
                p_strMoldChgRequired = YesStr
            Else
                p_strMoldChgRequired = NoStr
            End If
            oraParam = New OracleParameter(MoldChanged, p_strMoldChgRequired)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            'Operations Approval Date - JBH_2.00 Project 5325
            oraParam = New OracleParameter(OperDateApproved, IIf(p_dteOperDateApproved.Equals(DateTime.MinValue), DBNull.Value, p_dteOperDateApproved))
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.DateTime, DbType)
            oraCmd.Parameters.Add(oraParam)

            'Additional Information - jeseitz 10/29/2016
            oraParam = New OracleParameter(AdditionalInfo, p_strAddInfo)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)


            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CertificateSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsAffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsAffected > 0) Then
                ' Pass back the ID for a new certificate:
                If p_intCertificateNumberID = 0 AndAlso Not oraCmd.Parameters.Item(RetId).Value.Equals(DBNull.Value) Then
                    p_intCertificateNumberID = CInt(oraCmd.Parameters.Item(RetId).Value)
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
    ''' Updates Batch Numbers
    ''' </summary>
    ''' <param name="p_strCertifName">Certification name</param>
    ''' <param name="p_strTempBatchNum">Temporary batch number</param>
    ''' <param name="p_strGSOBatchNum">GSO batch number</param>
    ''' <param name="p_strUserName">User name</param>
    ''' <returns>Returns save result status as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function BatchNumMassUpdate(ByVal p_strCertifName As String, _
                                ByVal p_strTempBatchNum As String, _
                                ByVal p_strGSOBatchNum As String, _
                                ByVal p_strUserName As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Dim oraParam As OracleParameter
        Const CertificationTypeName As String = "ps_certificationtypename"
        Const TempBatchNumberG As String = "ps_temp_batchnumber_g"
        Const BatchNumberG As String = "ps_batchnumber_g"
        Const UserName As String = "ps_username"
        Const CertificateUpdateBatch As String = "CERTIFICATION_CRUD.certificate_update_batch"

        Try
            '1  ps_certificationtypename
            oraParam = New OracleParameter(CertificationTypeName, p_strCertifName)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '2  ps_temp_batchnumber_g
            oraParam = New OracleParameter(TempBatchNumberG, p_strTempBatchNum)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '3  ps_batchnumber_g
            oraParam = New OracleParameter(BatchNumberG, p_strGSOBatchNum)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)
            '4 ps_username
            oraParam = New OracleParameter(UserName, p_strUserName)
            oraParam.Direction = ParameterDirection.Input
            oraParam.DbType = CType(OracleType.VarChar, DbType)
            oraCmd.Parameters.Add(oraParam)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CertificateUpdateBatch

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
    ''' Checks Similar Tire
    ''' </summary>
    ''' <param name="pi_certType">Certificate type</param>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="ps_similarMatlNum">Similar material number</param>
    ''' <param name="pi_imarkFamily">Imark family</param>
    ''' <param name="ps_eceReference">Ece reference</param>
    ''' <param name="ps_errorDesc">Error description</param>
    ''' <returns>Returns error number as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function CheckSimilarTire(ByVal pi_certType As Integer, ByVal p_strMatlNum As String, ByRef ps_similarMatlNum As String, ByRef pi_imarkFamily As Integer, ByRef ps_eceReference As String, ByRef ps_errorDesc As String) As Integer

        Dim oraCmd As New OracleCommand
        Dim li_errorNum As Integer
        Const CertType As String = "pn_cert_type"
        Const InMatlNum As String = "ps_in_Matl_Num"
        Const SimilarMatlNum As String = "ps_Similar_Matl_Num"
        Const ImarkFamily As String = "ps_imark_family"
        Const EceReference As String = "ps_ece_reference"
        Const ErrorNum As String = "pn_error_num"
        Const ErrorDesc As String = "ps_error_desc"
        Const GetSimilarSku As String = "SIMILAR_TIRES.GET_SIMILAR_SKU"

        Try
            'Oracle command
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = String.Empty

            'Input parameters
            ParametersHelper.AddParametersToCommand(CertType, ParameterDirection.Input, OracleType.Number, pi_certType, oraCmd)
            ParametersHelper.AddParametersToCommand(InMatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)

            'Output parameters
            ParametersHelper.AddParametersToCommand(SimilarMatlNum, ParameterDirection.Output, OracleType.VarChar, 30, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ImarkFamily, ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(EceReference, ParameterDirection.Output, OracleType.VarChar, 30, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ErrorNum, ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ErrorDesc, ParameterDirection.Output, OracleType.VarChar, 200, Nothing, oraCmd)

            'Stored procedure
            oraCmd.CommandText = GetSimilarSku

            'Make connection and execute
            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            li_errorNum = CInt(oraCmd.Parameters.Item(ErrorNum).Value)

            If Not oraCmd.Parameters.Item(ErrorDesc).Value.Equals(DBNull.Value) Then
                ps_errorDesc = CStr(oraCmd.Parameters.Item(ErrorDesc).Value)
            Else
                ps_errorDesc = String.Empty
            End If

            If Not oraCmd.Parameters.Item(SimilarMatlNum).Value.Equals(DBNull.Value) Then
                ps_similarMatlNum = CStr(oraCmd.Parameters.Item(SimilarMatlNum).Value)
            Else
                ps_similarMatlNum = String.Empty
            End If

            If Not oraCmd.Parameters.Item(ImarkFamily).Value.Equals(DBNull.Value) Then
                pi_imarkFamily = CInt(oraCmd.Parameters.Item(ImarkFamily).Value)
            Else
                pi_imarkFamily = 0
            End If

            If Not oraCmd.Parameters.Item(EceReference).Value.Equals(DBNull.Value) Then
                ps_eceReference = CStr(oraCmd.Parameters.Item(EceReference).Value)
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
    ''' <param name="p_intCertificateID">Certificate id</param>
    ''' <param name="p_intNewCertificateID">New certificate id</param>
    ''' <param name="p_strUserName">User name</param>
    ''' <returns>Returns renewal result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function RenewCertificate(ByVal p_intCertificateID As Integer, ByRef p_intNewCertificateID As Integer, ByVal p_strUserName As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim dstResults As New DataSet       'return
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const NewId As String = "pi_newId"
        Const OldId As String = "pi_oldId"
        Const OperatorName As String = "ps_operatorName"
        Const ImarkCertificateRenew As String = "certification_crud.IMARKCERTIFICATE_RENEW"
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = String.Empty

            ParametersHelper.AddParametersToCommand(NewId, ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(OldId, ParameterDirection.Input, OracleType.Number, p_intCertificateID, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)

            oraCmd.CommandText = ImarkCertificateRenew

            Connect()
            oraCmd.Connection = Connection

            Dim rowsAffected As Integer = oraCmd.ExecuteNonQuery()
            p_intNewCertificateID = CInt(oraCmd.Parameters.Item(NewId).Value)
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
    ''' Gets the product information to be displayed on the product section of test result
    ''' </summary>
    ''' <param name="p_strMatlNum">The Material Number.</param>
    ''' <param name="p_iSKUID">Id</param>
    ''' <returns>Returns data table containing product info</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetProductData(ByVal p_strMatlNum As String, ByVal p_iSKUID As Integer) As ICSDataSet.ProductDataDataTable

        'Dim dsProduct As New TRACSSharedDatasets.SKUtoICSDataset
        Dim dsICSDatasets As New ICSDataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Const SkuId As String = "pi_SKUId"
        Const GetProductsData As String = "testresults_crud.GetProductData"
        Const TableStr As String = "Table"
        Const ProdData As String = "ProductData"

        Try
            ' Add the parameters
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_iSKUID, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetProductsData

            Connect()
            oraCmd.Connection = Connection
            ' Get the data
            oraAdp.SelectCommand = oraCmd
            dsICSDatasets.EnforceConstraints = False
            oraAdp.TableMappings.Add(TableStr, ProdData)
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
    ''' Gets the product information to be displayed on the product section of test result by Material Number
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Returns data set containing product info</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>19/11/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetProductInfo(ByVal p_strMatlNum As String, ByRef blnSuccess As Boolean) As ICS.Datasets.SKUtoICSDataset

        Dim dsICSDatasets As New ICS.Datasets.SKUtoICSDataset
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const RetCursor As String = "pc_product"
        Const PnErrorNumber As String = "pn_error_num"
        Const PsMatlNum As String = "ps_MATL_NUM"
        Const GetProductsData As String = "ICS_MAINTENANCE.GET_PRODUCT_INFO"
        Const TableStr As String = "Table"
        Const ProdData As String = "ProductData"
        Const PsErrorDesc As String = "ps_error_desc"

        Try
            ' Add the parameters
            ParametersHelper.AddParametersToCommand(PsMatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PnErrorNumber, ParameterDirection.Output, OracleType.Number, -1, oraCmd)
            ParametersHelper.AddParametersToCommand(PsErrorDesc, ParameterDirection.Output, OracleType.VarChar, "", oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetProductsData

            Connect()
            oraCmd.Connection = Connection
            ' Get the data
            oraAdp.SelectCommand = oraCmd
            dsICSDatasets.EnforceConstraints = False
            oraAdp.TableMappings.Add(TableStr, ProdData)
            oraAdp.Fill(dsICSDatasets)

            If Not String.IsNullOrEmpty(Convert.ToString(oraCmd.Parameters(PsErrorDesc).Value).Trim) Then
                ' Set flag as failure
                blnSuccess = False
                ' Error occurred, create an exception
                Throw New Exception(Convert.ToString(oraCmd.Parameters(PsErrorDesc).Value))
            Else
                ' Set flag as success
                blnSuccess = True
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

        Return dsICSDatasets

    End Function

    ''' <summary>
    ''' Saves the product
    ''' </summary>
    ''' <param name="p_iSKUID">Id</param>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_strBrand">Brand string</param>
    ''' <param name="p_strBrandLine">Brand line </param>
    ''' <param name="p_iTireTypeId">Tire type Id</param>
    ''' <param name="ps_PSN">PSN</param>
    ''' <param name="p_strSizeStamp">Size stamp</param>
    ''' <param name="p_dteDiscontinuedDate">Date of discontinue</param>
    ''' <param name="p_strSPECNUMBER">Spec number</param>
    ''' <param name="p_strSPEEDRATING">Speed rating</param>
    ''' <param name="p_strSINGLOADINDEX">Single load index</param>
    ''' <param name="p_strDUALLOADINDEX">Dual load index</param>
    ''' <param name="p_strBIASBELTEDRADIAL">Bias Belted Radial</param>
    ''' <param name="p_strTUBElESSYN">Tubeless</param>
    ''' <param name="p_strREINFORCEDYN">Reinforced</param>
    ''' <param name="p_strEXTRALOADYN">Extra load</param>
    ''' <param name="p_strUTQGTREADWEAR">UTQGT Read wear</param>
    ''' <param name="p_strUTQGTRACTION">UTQGTR Action</param>
    ''' <param name="p_strUTQGTEMP">UTQG temporary</param>
    ''' <param name="p_strMUDSNOWYN">Mudsnowyn</param>
    ''' <param name="p_iRIMDIAMETER">Rim diameter</param>
    ''' <param name="p_dteSerialDate">Serial date</param>
    ''' <param name="p_strBrandDesc">Brand description</param>
    ''' <param name="p_strMeaRimWidth">Rim width</param>
    ''' <param name="p_strLoadRange">Load range</param>
    ''' <param name="p_strRegroovableInd">Regroovable ind</param>
    ''' <param name="p_strPlantProduced">Plant produced</param>
    ''' <param name="p_dteMostRecentTestDate">Most recent test date</param>
    ''' <param name="p_strIMark">Imark</param>
    ''' <param name="p_strInformeNumber">Inform number</param>
    ''' <param name="p_dteFechaDate">Fetch date</param>
    ''' <param name="p_strTreadPattern">Tread pattern</param>
    ''' <param name="p_strSpecialProtectiveBand">Special protective band</param>
    ''' <param name="p_strNominalTireWidth">Nominal tire width</param>
    ''' <param name="p_strAspectRadio">Aspect radio</param>
    ''' <param name="p_strTreadwearIndicators">Tread wear indicators</param>
    ''' <param name="p_strNameOfManufacturer">Manufacturer name</param>
    ''' <param name="p_strFamily">Family</param>
    ''' <param name="p_strDOTSerialNumber">DOT serial number</param>
    ''' <param name="p_strTPN">TPN</param>
    ''' <param name="p_strUserName">User name</param>
    ''' <param name="p_strSEVEREWEATHERIND">Severe weather</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY</param>
    ''' <returns>Returns the save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        Const SkuId As String = "pi_SKUID"
        Const Brand As String = "ps_Brand"
        Const BrandLine As String = "ps_Brand_Line"
        Const TireTypeId As String = "pi_TIRETYPEID"
        Const Psn As String = "ps_PSN"
        Const SizeStamp As String = "ps_SIZESTAMP"
        Const DiscontinueDate As String = "pd_DISCONTINUEDDATE"
        Const SpecNumber As String = "ps_SPECNUMBER"
        Const SpeedRating As String = "ps_SPEEDRATING"
        Const SingLoadIndex As String = "ps_SINGLOADINDEX"
        Const DualLoadIndex As String = "ps_DUALLOADINDEX"
        Const BiasBeltedRadial As String = "ps_BIASBELTEDRADIAL"
        Const Tubelessyn As String = "ps_TUBElESSYN"
        Const ReInforcedyn As String = "ps_REINFORCEDYN"
        Const ExtraLoadyn As String = "ps_EXTRALOADYN"
        Const UtqgtReadWear As String = "ps_UTQGTREADWEAR"
        Const UtqgtrAction As String = "ps_UTQGTRACTION"
        Const UtqgTemp As String = "ps_UTQGTEMP"
        Const MudsNowyn As String = "ps_MUDSNOWYN"
        Const RimDiameter As String = "pi_RIMDIAMETER"
        Const SerialDate As String = "pd_SERIALDATE"
        Const BrandDesc As String = "ps_BRANDDESC"
        Const RimWidth As String = "pi_MEARIMWIDTH"
        Const LoadRange As String = "ps_LOADRANGE"
        Const RegroovableInd As String = "ps_REGROOVABLEIND"
        Const PlantProduced As String = "ps_PLANTPRODUCED"
        Const MostRecentTestDate As String = "pd_MOSTRECENTTESTDATE"
        Const Imark As String = "ps_IMARK"
        Const InformNumber As String = "ps_INFORMENUMBER"
        Const FechaDate As String = "pd_FECHADATE"
        Const TreadPattern As String = "ps_TREADPATTERN"
        Const SpecialProtectiveBand As String = "ps_SPECIALPROTECTIVEBAND"
        Const NominalTireWidth As String = "ps_NOMINALTIREWIDTH"
        Const AspectRadio As String = "ps_ASPECTRADIO"
        Const TreadWearIndicators As String = "ps_TREADWEARINDICATORS"
        Const NameOfManufacturer As String = "ps_NAMEOFMANUFACTURER"
        Const Family As String = "ps_FAMILY"
        Const DotSerialNumber As String = "ps_DOTSERIALNUMBER"
        Const Tpn As String = "ps_TPN"
        Const OperatorName As String = "ps_OperatorName"
        Const MfgWwyy As String = "ps_MFGWWYY"
        Const SevereWeatherInd As String = "ps_SEVEREWEATHERIND"
        Const ProductSave As String = "TESTRESULTS_CRUD.Product_Save"

        Try
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_iSKUID, oraCmd)
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(Brand, ParameterDirection.Input, OracleType.VarChar, p_strBrand, oraCmd)
            ParametersHelper.AddParametersToCommand(BrandLine, ParameterDirection.Input, OracleType.VarChar, p_strBrandLine, oraCmd)
            ParametersHelper.AddParametersToCommand(TireTypeId, ParameterDirection.Input, OracleType.Number, p_iTireTypeId, oraCmd)
            ParametersHelper.AddParametersToCommand(Psn, ParameterDirection.Input, OracleType.VarChar, ps_PSN, oraCmd)
            ParametersHelper.AddParametersToCommand(SizeStamp, ParameterDirection.Input, OracleType.VarChar, p_strSizeStamp, oraCmd)
            ParametersHelper.AddParametersToCommand(DiscontinueDate, ParameterDirection.Input, OracleType.DateTime, p_dteDiscontinuedDate, oraCmd)
            ParametersHelper.AddParametersToCommand(SpecNumber, ParameterDirection.Input, OracleType.VarChar, p_strSPECNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(SpeedRating, ParameterDirection.Input, OracleType.VarChar, p_strSPEEDRATING, oraCmd)
            ParametersHelper.AddParametersToCommand(SingLoadIndex, ParameterDirection.Input, OracleType.VarChar, p_strSINGLOADINDEX, oraCmd)
            ParametersHelper.AddParametersToCommand(DualLoadIndex, ParameterDirection.Input, OracleType.VarChar, p_strDUALLOADINDEX, oraCmd)
            ParametersHelper.AddParametersToCommand(BiasBeltedRadial, ParameterDirection.Input, OracleType.VarChar, p_strBIASBELTEDRADIAL, oraCmd)
            ParametersHelper.AddParametersToCommand(Tubelessyn, ParameterDirection.Input, OracleType.VarChar, p_strTUBElESSYN, oraCmd)
            ParametersHelper.AddParametersToCommand(ReInforcedyn, ParameterDirection.Input, OracleType.VarChar, p_strREINFORCEDYN, oraCmd)
            ParametersHelper.AddParametersToCommand(ExtraLoadyn, ParameterDirection.Input, OracleType.VarChar, p_strEXTRALOADYN, oraCmd)
            ParametersHelper.AddParametersToCommand(UtqgtReadWear, ParameterDirection.Input, OracleType.VarChar, p_strUTQGTREADWEAR, oraCmd)
            ParametersHelper.AddParametersToCommand(UtqgtrAction, ParameterDirection.Input, OracleType.VarChar, p_strUTQGTRACTION, oraCmd)
            ParametersHelper.AddParametersToCommand(UtqgTemp, ParameterDirection.Input, OracleType.VarChar, p_strUTQGTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(MudsNowyn, ParameterDirection.Input, OracleType.VarChar, p_strMUDSNOWYN, oraCmd)
            ParametersHelper.AddParametersToCommand(RimDiameter, ParameterDirection.Input, OracleType.Number, p_iRIMDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand(SerialDate, ParameterDirection.Input, OracleType.DateTime, p_dteSerialDate, oraCmd)
            ParametersHelper.AddParametersToCommand(BrandDesc, ParameterDirection.Input, OracleType.VarChar, p_strBrandDesc, oraCmd)
            ParametersHelper.AddParametersToCommand(RimWidth, ParameterDirection.Input, OracleType.Number, p_strMeaRimWidth, oraCmd)
            ParametersHelper.AddParametersToCommand(LoadRange, ParameterDirection.Input, OracleType.VarChar, p_strLoadRange, oraCmd)
            ParametersHelper.AddParametersToCommand(RegroovableInd, ParameterDirection.Input, OracleType.VarChar, p_strRegroovableInd, oraCmd)
            ParametersHelper.AddParametersToCommand(PlantProduced, ParameterDirection.Input, OracleType.VarChar, p_strPlantProduced, oraCmd)
            ParametersHelper.AddParametersToCommand(MostRecentTestDate, ParameterDirection.Input, OracleType.DateTime, p_dteMostRecentTestDate, oraCmd)
            ParametersHelper.AddParametersToCommand(Imark, ParameterDirection.Input, OracleType.VarChar, p_strIMark, oraCmd)
            ParametersHelper.AddParametersToCommand(InformNumber, ParameterDirection.Input, OracleType.VarChar, p_strInformeNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(FechaDate, ParameterDirection.Input, OracleType.DateTime, p_dteFechaDate, oraCmd)
            ParametersHelper.AddParametersToCommand(TreadPattern, ParameterDirection.Input, OracleType.VarChar, p_strTreadPattern, oraCmd)
            ParametersHelper.AddParametersToCommand(SpecialProtectiveBand, ParameterDirection.Input, OracleType.VarChar, p_strSpecialProtectiveBand, oraCmd)
            ParametersHelper.AddParametersToCommand(NominalTireWidth, ParameterDirection.Input, OracleType.VarChar, p_strNominalTireWidth, oraCmd)
            ParametersHelper.AddParametersToCommand(AspectRadio, ParameterDirection.Input, OracleType.VarChar, p_strAspectRadio, oraCmd)
            ParametersHelper.AddParametersToCommand(TreadWearIndicators, ParameterDirection.Input, OracleType.VarChar, p_strTreadwearIndicators, oraCmd)
            ParametersHelper.AddParametersToCommand(NameOfManufacturer, ParameterDirection.Input, OracleType.VarChar, p_strNameOfManufacturer, oraCmd)
            ParametersHelper.AddParametersToCommand(Family, ParameterDirection.Input, OracleType.VarChar, p_strFamily, oraCmd)
            ParametersHelper.AddParametersToCommand(DotSerialNumber, ParameterDirection.Input, OracleType.VarChar, p_strDOTSerialNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(Tpn, ParameterDirection.Input, OracleType.VarChar, p_strTPN, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)
            ParametersHelper.AddParametersToCommand(MfgWwyy, ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)
            ParametersHelper.AddParametersToCommand(SevereWeatherInd, ParameterDirection.Input, OracleType.VarChar, p_strSEVEREWEATHERIND, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = ProductSave

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
    ''' Returns distinct records on given column
    ''' </summary>
    ''' <param name="dtResults">results table to search from</param>
    ''' <param name="strCriteria">Search criteria</param>
    ''' <returns>Returns distinct records as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function GetDistinctRecords(ByVal dtResults As DataTable, ByVal strCriteria As String) As DataTable
        Dim dtUniqRecords As New DataTable
        Try
            dtUniqRecords = dtResults.DefaultView.ToTable(True, strCriteria)

        Catch exp As Exception
            EventLogger.Enter(exp)
        End Try

        Return dtUniqRecords

    End Function
    ''' <summary>
    ''' Gets certificates data based on type
    ''' </summary>
    ''' <param name="p_certificationtypeid">Certification type id</param>
    ''' <param name="p_all">All</param>
    ''' <returns>Returns certificates data as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCertificatesByType(ByVal p_certificationtypeid As Integer, ByVal p_all As String) As DataTable
        Dim dtDatatable As New DataTable
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const CertificationTypeId As String = "pn_CertificationTypeID"
        Const AllStr As String = "ps_All"
        Const GetCertificateByType As String = "ics_common_functions.GetCertificatesByType"

        Try
            ' Add the parameters
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_certificationtypeid, oraCmd)
            ParametersHelper.AddParametersToCommand(AllStr, ParameterDirection.Input, OracleType.VarChar, p_all, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCertificateByType

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
    ''' <summary>
    ''' Gets the Certificate template for a certification type name
    ''' </summary>
    ''' <param name="p_strCertificationTypeName">Certification type name</param>
    ''' <returns>Returns certificate template as string</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCertTemplate(ByVal p_strCertificationTypeName As String) As String

        'Returns the CertTemplate for a certification type name - added for generic certification types 6/9/16 jeseitz
        Dim oraCmd As New OracleCommand
        Dim p_strCertTemplate As String
        Const GetCertiTemplate As String = "ICS_COMMON_FUNCTIONS.GetCertTemplate"
        Const CertificationTypeName As String = "ps_CertificationTypeName"
        Const RetValue As String = "retvalue"

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCertiTemplate
            ParametersHelper.AddParametersToCommand(CertificationTypeName, ParameterDirection.Input, OracleType.VarChar, 50, p_strCertificationTypeName, oraCmd)
            ParametersHelper.AddParametersToCommand(RetValue, ParameterDirection.ReturnValue, OracleType.VarChar, 50, "", oraCmd)
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
    ''' <summary>
    ''' Gets the Certification name based on certification type id
    ''' </summary>
    ''' <param name="p_intCertificationTypeID">Certification type id</param>
    ''' <returns>Returns certification name as string</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Function GetCertificationNameByID(ByVal p_intCertificationTypeID As Integer) As String
        'Returns the CertTemplate for a certification type name - added for generic certification types 6/9/16 jeseitz
        Dim oraCmd As New OracleCommand
        Dim p_strCertificationType As String
        Const GetCertficationNameById As String = "ICS_COMMON_FUNCTIONS.GetCertificationNameByID"
        Const CertificationTypeId As String = "pn_CertificationTypeID"
        Const RetValue As String = "retvalue"
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCertficationNameById
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand(RetValue, ParameterDirection.ReturnValue, OracleType.VarChar, 50, "", oraCmd)
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
    ''' <summary>
    ''' Saves Measurement part of the test.
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
    ''' <param name="p_strOperation">Operation number Id.</param>
    ''' <param name="p_strGTSpecMeasure">GT Spec.</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY.</param>
    ''' <returns>Returns save result as integer</returns>
    ''' ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Const MeasureId As String = "pi_MEASUREID"
        Const CertificateId As String = "pi_CertificateID"
        Const ProjectNumber As String = "ps_PROJECTNUMBER"
        Const TireNumber As String = "pi_TIRENUMBER"
        Const TestSpec As String = "ps_TESTSPEC"
        Const CompletionDate As String = "pd_COMPLETIONDATE"
        Const InflationPressure As String = "pi_INFLATIONPRESSURE"
        Const MoldDesign As String = "ps_MOLDDESIGN"
        Const RimWidth As String = "pi_RIMWIDTH"
        Const DotSerialNumber As String = "ps_DOTSERIALNUMBER"
        Const Diameter As String = "pi_DIAMETER"
        Const AvgSectionWidth As String = "pi_AVGSECTIONWIDTH"
        Const AvgOverallWidth As String = "pi_AVGOVERALLWIDTH"
        Const MaxOverallWidth As String = "pi_MAXOVERALLWIDTH"
        Const SizeFactor As String = "pi_SIZEFACTOR"
        Const MountTime As String = "pd_MOUNTTIME"
        Const MountTemp As String = "pi_MOUNTTEMP"
        Const SerialDate As String = "pd_SERIALDATE"
        Const EndTime As String = "pd_ENDTIME"
        Const ActSizeFactor As String = "pi_ACTSIZEFACTOR"
        Const CertificationTypeId As String = "pi_CERTIFICATIONTYPEID"
        Const StartInflationPressure As String = "pi_STARTINFLATIONPRESSURE"
        Const EndInflationPressure As String = "pi_ENDINFLATIONPRESSURE"
        Const Adjustment As String = "ps_ADJUSTMENT"
        Const Circunference As String = "pi_CIRCUNFERENCE"
        Const NominalDiameter As String = "pi_NOMINALDIAMETER"
        Const NominalWidth As String = "pi_NOMINALWIDTH"
        Const NominalWidthPassFail As String = "ps_NOMINALWIDTHPASSFAIL"
        Const NominalWidthDiference As String = "pi_NOMINALWIDTHDIFERENCE"
        Const NominalWidthTolerance As String = "pi_NOMINALWIDTHTOLERANCE"
        Const MaxOverallDiameter As String = "pi_MAXOVERALLDIAMETER"
        Const MinOverallDiameter As String = "pi_MINOVERALLDIAMETER"
        Const OverallWidthPassFail As String = "ps_OVERALLWIDTHPASSFAIL"
        Const OverallDiameterPassFail As String = "ps_OVERALLDIAMETERPASSFAIL"
        Const DiameterDiference As String = "pi_DIAMETERDIFERENCE"
        Const DiameterTolerance As String = "pi_DIAMETERTOLERANCE"
        Const TempResistanceGrading As String = "pi_TEMPRESISTANCEGRADING"
        Const TensileStrenght1 As String = "pi_TENSILESTRENGHT1"
        Const TensileStrenght2 As String = "pi_TENSILESTRENGHT2"
        Const Elongation1 As String = "pi_ELONGATION1"
        Const Elongation2 As String = "pi_ELONGATION2"
        Const TensileStrenghtAfterAge1 As String = "pi_TENSILESTRENGHTAFTERAGE1"
        Const TensileStrenghtAfterAge2 As String = "pi_TENSILESTRENGHTAFTERAGE2"
        Const OperatorName As String = "ps_OperatorName"
        Const Operation As String = "ps_Operation"
        Const GtSpec As String = "ps_GTSPEC"
        Const MfgWwyy As String = "ps_MFGWWYY"
        Const MeasureSave As String = "testresults_crud.Measure_Save"
        Const OneNum As Short = 1
        Const ZeroNum As Short = 0
        Try
            ParametersHelper.AddParametersToCommand(MeasureId, ParameterDirection.Output, OracleType.Number, p_intMEASUREID, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateID, oraCmd)
            ParametersHelper.AddParametersToCommand(ProjectNumber, ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(TireNumber, ParameterDirection.Input, OracleType.Number, p_sngTIRENUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(TestSpec, ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            ParametersHelper.AddParametersToCommand(CompletionDate, ParameterDirection.Input, OracleType.DateTime, p_dteCOMPLETIONDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(InflationPressure, ParameterDirection.Input, OracleType.Number, p_sngINFLATIONPRESSURE, oraCmd)
            ParametersHelper.AddParametersToCommand(MoldDesign, ParameterDirection.Input, OracleType.VarChar, p_strMOLDDESIGN, oraCmd)
            ParametersHelper.AddParametersToCommand(RimWidth, ParameterDirection.Input, OracleType.Number, p_sngRIMWIDTH, oraCmd)
            ParametersHelper.AddParametersToCommand(DotSerialNumber, ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(Diameter, ParameterDirection.Input, OracleType.Number, p_sngDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand(AvgSectionWidth, ParameterDirection.Input, OracleType.Number, p_sngAVGSECTIONWIDTH, oraCmd)
            ParametersHelper.AddParametersToCommand(AvgOverallWidth, ParameterDirection.Input, OracleType.Number, p_sngAVGOVERALLWIDTH, oraCmd)
            ParametersHelper.AddParametersToCommand(MaxOverallWidth, ParameterDirection.Input, OracleType.Number, p_sngMAXOVERALLWIDTH, oraCmd)
            ParametersHelper.AddParametersToCommand(SizeFactor, ParameterDirection.Input, OracleType.Number, p_sngSIZEFACTOR, oraCmd)
            ParametersHelper.AddParametersToCommand(MountTime, ParameterDirection.Input, OracleType.DateTime, p_dteMOUNTTIME, oraCmd)
            ParametersHelper.AddParametersToCommand(MountTemp, ParameterDirection.Input, OracleType.Number, p_sngMOUNTTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(SerialDate, ParameterDirection.Input, OracleType.DateTime, p_dteSerialDate, oraCmd)
            ParametersHelper.AddParametersToCommand(EndTime, ParameterDirection.Input, OracleType.DateTime, p_dteEndTime, oraCmd)
            ParametersHelper.AddParametersToCommand(ActSizeFactor, ParameterDirection.Input, OracleType.Number, p_sngActSizeFactor, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertType, oraCmd)
            ParametersHelper.AddParametersToCommand(StartInflationPressure, ParameterDirection.Input, OracleType.Number, p_srtSTARTINFLATIONPRESSURE, oraCmd)
            ParametersHelper.AddParametersToCommand(EndInflationPressure, ParameterDirection.Input, OracleType.Number, p_srtENDINFLATIONPRESSURE, oraCmd)
            ParametersHelper.AddParametersToCommand(Adjustment, ParameterDirection.Input, OracleType.VarChar, p_strADJUSTMENT, oraCmd)
            ParametersHelper.AddParametersToCommand(Circunference, ParameterDirection.Input, OracleType.Number, p_sngCIRCUNFERENCE, oraCmd)
            ParametersHelper.AddParametersToCommand(NominalDiameter, ParameterDirection.Input, OracleType.Number, p_sngNOMINALDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand(NominalWidth, ParameterDirection.Input, OracleType.Number, p_sngNOMINALWIDTH, oraCmd)
            ParametersHelper.AddParametersToCommand(NominalWidthPassFail, ParameterDirection.Input, OracleType.VarChar, p_strNOMINALWIDTHPASSFAIL, oraCmd)
            ParametersHelper.AddParametersToCommand(NominalWidthDiference, ParameterDirection.Input, OracleType.Number, p_sngNOMINALWIDTHDIFERENCE, oraCmd)
            ParametersHelper.AddParametersToCommand(NominalWidthTolerance, ParameterDirection.Input, OracleType.Number, p_sngNOMINALWIDTHTOLERANCE, oraCmd)
            ParametersHelper.AddParametersToCommand(MaxOverallDiameter, ParameterDirection.Input, OracleType.Number, p_sngMAXOVERALLDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand(MinOverallDiameter, ParameterDirection.Input, OracleType.Number, p_sngMINOVERALLDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand(OverallWidthPassFail, ParameterDirection.Input, OracleType.VarChar, p_strOVERALLWIDTHPASSFAIL, oraCmd)
            ParametersHelper.AddParametersToCommand(OverallDiameterPassFail, ParameterDirection.Input, OracleType.VarChar, p_strOVERALLDIAMETERPASSFAIL, oraCmd)
            ParametersHelper.AddParametersToCommand(DiameterDiference, ParameterDirection.Input, OracleType.Number, p_sngDIAMETERDIFERENCE, oraCmd)
            ParametersHelper.AddParametersToCommand(DiameterTolerance, ParameterDirection.Input, OracleType.Number, p_sngDIAMETERTOLERANCE, oraCmd)
            ParametersHelper.AddParametersToCommand(TempResistanceGrading, ParameterDirection.Input, OracleType.VarChar, p_strTEMPRESISTANCEGRADING, oraCmd)
            ParametersHelper.AddParametersToCommand(TensileStrenght1, ParameterDirection.Input, OracleType.Number, p_sngTENSILESTRENGHT1, oraCmd)
            ParametersHelper.AddParametersToCommand(TensileStrenght2, ParameterDirection.Input, OracleType.Number, p_sngTENSILESTRENGHT2, oraCmd)
            ParametersHelper.AddParametersToCommand(Elongation1, ParameterDirection.Input, OracleType.Number, p_sngELONGATION1, oraCmd)
            ParametersHelper.AddParametersToCommand(Elongation2, ParameterDirection.Input, OracleType.Number, p_sngELONGATION2, oraCmd)
            ParametersHelper.AddParametersToCommand(TensileStrenghtAfterAge1, ParameterDirection.Input, OracleType.Number, p_sngTENSILESTRENGHTAFTERAGE1, oraCmd)
            ParametersHelper.AddParametersToCommand(TensileStrenghtAfterAge2, ParameterDirection.Input, OracleType.Number, p_sngTENSILESTRENGHTAFTERAGE2, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(Operation, ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)
            ParametersHelper.AddParametersToCommand(GtSpec, ParameterDirection.Input, OracleType.VarChar, p_strGTSpecMeasure, oraCmd)
            ParametersHelper.AddParametersToCommand(MfgWwyy, ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = MeasureSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()

            If (rowsaffected = OneNum) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Measure Id to be inserted on the detail table
            If oraCmd.Parameters.Item(MeasureId).Value.Equals(DBNull.Value) Then
                p_intMEASUREID = ZeroNum
            Else
                p_intMEASUREID = CInt(oraCmd.Parameters.Item(MeasureId).Value)
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
    ''' Saves the measurements details
    ''' </summary>
    ''' <param name="p_sngSectionWidth">Section width</param>
    ''' <param name="p_sngOVERALLWIDTH">Overall width</param>
    ''' <param name="p_iMEASUREID">Measure Id</param>
    ''' <param name="p_sngITERATION">Iteration value</param>
    ''' <param name="p_strUserName">User name</param>
    ''' <returns>Returns save result as integer</returns>
    ''' ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function MeasurementDetail_Save(ByVal p_sngSectionWidth As Single, _
                                            ByVal p_sngOVERALLWIDTH As Single, _
                                            ByVal p_iMEASUREID As Integer, _
                                            ByVal p_sngITERATION As Single, _
                                            ByVal p_strUserName As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const SectionWidth As String = "pi_SECTIONWIDTH"
        Const OverallWidth As String = "pi_OVERALLWIDTH"
        Const MeasureId As String = "pi_MEASUREID"
        Const Iteration As String = "PI_ITERATION"
        Const OperatorName As String = "ps_OperatorName"
        Const MeasureDetailSave As String = "testresults_crud.MeasureDetail_Save"
        Const OneNum As Short = 1
        Try
            ParametersHelper.AddParametersToCommand(SectionWidth, ParameterDirection.Input, OracleType.Number, p_sngSectionWidth, oraCmd)
            ParametersHelper.AddParametersToCommand(OverallWidth, ParameterDirection.Input, OracleType.Number, p_sngOVERALLWIDTH, oraCmd)
            ParametersHelper.AddParametersToCommand(MeasureId, ParameterDirection.Input, OracleType.Number, p_iMEASUREID, oraCmd)
            ParametersHelper.AddParametersToCommand(Iteration, ParameterDirection.Input, OracleType.Number, p_sngITERATION, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = MeasureDetailSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
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
    ''' Saves the plunger part of the test results
    ''' </summary>
    ''' <param name="p_strPROJECTNUMBER">Project number</param>
    ''' <param name="p_sngTIRENUMBER">Tire number</param>
    ''' <param name="p_strTESTSPEC">Test specification</param>
    ''' <param name="p_dteCOMPLETIONDATE">Completion date</param>
    ''' <param name="p_strDOTSERIALNUMBER">Dot serial number</param>
    ''' <param name="p_sngAVGBREAKINGENERGY">Average breaking energy</param>
    ''' <param name="p_strPASSYN">Passyn</param>
    ''' <param name="p_intSKUID">Sku id</param>
    ''' <param name="p_intCertType">Certificate type</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate number</param>
    ''' <param name="p_intPLUNGERID">Plunger id</param>
    ''' <param name="p_dteSerialDate">Serial date</param>
    ''' <param name="p_sngMinPlunger">Minimum plunger value</param>
    ''' <param name="p_strUserName">User name</param>
    ''' <param name="p_intCertificateNumberID">Certificate number id</param>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strGTSpecPlunger">GT Specification plunger</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY.</param>
    ''' <returns>Returns save result as integer</returns>
    ''' ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Const PlungerId As String = "pi_PLUNGERID"
        Const ProjectNumber As String = "ps_PROJECTNUMBER"
        Const TireNumber As String = "pi_TIRENUMBER"
        Const TestSpec As String = "ps_TESTSPEC"
        Const CompletionDate As String = "pd_COMPLETIONDATE"
        Const DotSerialNumber As String = "ps_DOTSERIALNUMBER"
        Const AvgBreakingEnergy As String = "pi_AVGBREAKINGENERGY"
        Const Passyn As String = "ps_PASSYN"
        Const CertificationTypeId As String = "pi_CERTIFICATIONTYPEID"
        Const SerialDate As String = "pd_SERIALDATE"
        Const MinPlunger As String = "pi_MINPLUNGER"
        Const OperatorName As String = "ps_OperatorName"
        Const CertificateId As String = "pi_CertificateID"
        Const Operation As String = "ps_Operation"
        Const GtSpec As String = "ps_GTSPEC"
        Const MfgWwyy As String = "ps_MFGWWYY"
        Const PlungerSave As String = "testresults_crud.PLUNGER_Save"
        Const OneNum As Short = 1
        Const ZeroNum As Short = 0

        Try
            ParametersHelper.AddParametersToCommand(PlungerId, ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ProjectNumber, ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(TireNumber, ParameterDirection.Input, OracleType.Number, p_sngTIRENUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(TestSpec, ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            ParametersHelper.AddParametersToCommand(CompletionDate, ParameterDirection.Input, OracleType.DateTime, p_dteCOMPLETIONDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(DotSerialNumber, ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(AvgBreakingEnergy, ParameterDirection.Input, OracleType.VarChar, p_sngAVGBREAKINGENERGY, oraCmd)
            ParametersHelper.AddParametersToCommand(Passyn, ParameterDirection.Input, OracleType.VarChar, p_strPASSYN, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertType, oraCmd)
            ParametersHelper.AddParametersToCommand(SerialDate, ParameterDirection.Input, OracleType.DateTime, p_dteSerialDate, oraCmd)
            ParametersHelper.AddParametersToCommand(MinPlunger, ParameterDirection.Input, OracleType.Number, p_sngMinPlunger, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(Operation, ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)
            ParametersHelper.AddParametersToCommand(GtSpec, ParameterDirection.Input, OracleType.VarChar, p_strGTSpecPlunger, oraCmd)
            ParametersHelper.AddParametersToCommand(MfgWwyy, ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = PlungerSave

            Connect()
            oraCmd.Connection = Connection
            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Measure Id to be inserted on the detail table
            If oraCmd.Parameters.Item(PlungerId).Value.Equals(DBNull.Value) Then   '"PI_PLUNGERID"
                p_intPLUNGERID = ZeroNum
            Else
                p_intPLUNGERID = CInt(oraCmd.Parameters.Item(PlungerId).Value)
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
    ''' Saves the plunger details
    ''' </summary>
    ''' <param name="p_sngBREAKINGENERGY">Breaking energy</param>
    ''' <param name="p_intPlungerID">Plunger Id</param>
    ''' <param name="p_sngIteration">The  iteration.</param>
    ''' <param name="p_strUserName">Name of the  user</param>
    ''' <returns>Returns save result as integer</returns>
    ''' ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function SavePlungerDetail(ByVal p_sngBREAKINGENERGY As Single, _
                                      ByVal p_intPlungerID As Integer, _
                                      ByVal p_sngIteration As Single, _
                                      ByVal p_strUserName As String) As NameAid.SaveResult
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const BreakingEnergy As String = "PI_BREAKINGENERGY"
        Const PlungerId As String = "PI_PLUNGERID"
        Const Iteration As String = "PI_ITERATION"
        Const OperatorName As String = "ps_OperatorName"
        Const PlungerDetailSave As String = "testresults_crud.PLUNGERDETAIL_Save"
        Const OneNum As Short = 1

        Try
            ParametersHelper.AddParametersToCommand(BreakingEnergy, ParameterDirection.Input, OracleType.Number, p_sngBREAKINGENERGY, oraCmd)
            ParametersHelper.AddParametersToCommand(PlungerId, ParameterDirection.Input, OracleType.Number, p_intPlungerID, oraCmd)
            ParametersHelper.AddParametersToCommand(Iteration, ParameterDirection.Input, OracleType.Number, p_sngIteration, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = PlungerDetailSave

            Connect()
            oraCmd.Connection = Connection
            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
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
    ''' Saves treadWear header table data
    ''' </summary>
    ''' <param name="p_strPROJECTNUMBER">Project number</param>
    ''' <param name="p_sngTIRENUMBER">Tire number</param>
    ''' <param name="p_strTESTSPEC">Test specification</param>
    ''' <param name="p_dteCOMPLETIONDATE">Completion date</param>
    ''' <param name="p_strDOTSERIALNUMBER">Dot serial number</param>
    ''' <param name="p_sngLOWESTWEARBAR">Lowest wear bar</param>
    ''' <param name="p_strPassyn">Passyn string</param>
    ''' <param name="p_intSKUID">Sku id</param>
    ''' <param name="p_intCertType">Certificate type</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate number</param>
    ''' <param name="p_intTREADWEARID">Tread wear id</param>
    ''' <param name="p_dteSERIALDATE">Serial date</param>
    ''' <param name="p_strOperatorName">Operator name</param>
    ''' <param name="p_sngINDICATORSREQUIREMENT">Indicators requirement</param>
    ''' <param name="p_intCertificateID">Certificate id</param>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_strOperation">Operation string</param>
    ''' <param name="p_strGTSpecTreadWear">GT Spec for tread wear</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY</param>
    ''' <returns>Returns save result as integer</returns>
    ''' ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Const TreadwearId As String = "PI_TREADWEARID"
        Const ProjectNumber As String = "PS_PROJECTNUMBER"
        Const TireNumber As String = "PI_TIRENUMBER"
        Const TestSpec As String = "PS_TESTSPEC"
        Const CompletionDate As String = "PD_COMPLETIONDATE"
        Const DotSerialNumber As String = "PS_DOTSERIALNUMBER"
        Const LowestWearBar As String = "PI_LOWESTWEARBAR"
        Const Passyn As String = "PS_PASSYN"
        Const CertificationTypeId As String = "pi_CertificationTypeID"
        Const SerialDate As String = "PD_SERIALDATE"
        Const OperatorName As String = "ps_OperatorName"
        Const IndicatorsRequirement As String = "pi_INDICATORSREQUIREMENT"
        Const CertificateId As String = "pi_CertificateID"
        Const Operation As String = "ps_Operation"
        Const GtSpec As String = "ps_GTSPEC"
        Const MfgWwyy As String = "ps_MFGWWYY"
        Const TreadwearSave As String = "testresults_crud.TREADWEAR_SAVE"
        Const OneNum As Short = 1

        Try
            ParametersHelper.AddParametersToCommand(TreadwearId, ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ProjectNumber, ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(TireNumber, ParameterDirection.Input, OracleType.Number, p_sngTIRENUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(TestSpec, ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            ParametersHelper.AddParametersToCommand(CompletionDate, ParameterDirection.Input, OracleType.DateTime, p_dteCOMPLETIONDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(DotSerialNumber, ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(LowestWearBar, ParameterDirection.Input, OracleType.Number, p_sngLOWESTWEARBAR, oraCmd)
            ParametersHelper.AddParametersToCommand(Passyn, ParameterDirection.Input, OracleType.VarChar, p_strPassyn, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertType, oraCmd)
            ParametersHelper.AddParametersToCommand(SerialDate, ParameterDirection.Input, OracleType.DateTime, p_dteSERIALDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            ParametersHelper.AddParametersToCommand(IndicatorsRequirement, ParameterDirection.Input, OracleType.Number, p_sngINDICATORSREQUIREMENT, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateID, oraCmd)
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(Operation, ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)
            ParametersHelper.AddParametersToCommand(GtSpec, ParameterDirection.Input, OracleType.VarChar, p_strGTSpecTreadWear, oraCmd)
            ParametersHelper.AddParametersToCommand(MfgWwyy, ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = TreadwearSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()

            If (rowsaffected = OneNum) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Measure Id to be inserted on the detail table
            If oraCmd.Parameters.Item(TreadwearId).Value.Equals(DBNull.Value) Then
                p_intTREADWEARID = UInteger.MinValue
            Else
                p_intTREADWEARID = CInt(oraCmd.Parameters.Item(TreadwearId).Value)
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
    ''' Saves treadWear details
    ''' </summary>
    ''' <param name="details">List of treadwear details</param>
    ''' <param name="p_intTREADWEARID">Treadwear id</param>
    ''' <param name="p_strOperatorName">Operator name</param>
    ''' <param name="p_intITERATION">Iteration value</param>
    ''' <returns>Returns save result as integer</returns>
    ''' ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function NewSaveTreadWearDetail(ByVal details As List(Of TreadwearDetail), _
                                           ByVal p_intTREADWEARID As Integer, _
                                           ByVal p_strOperatorName As String, _
                                           ByVal p_intITERATION As Integer) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Dim rowsaffected As Integer = 0
        Const TreadwearDetailSave As String = "testresults_crud.TREADWEARDETAIL_SAVE"
        Const WearBarHeight As String = "PI_WEARBARHEIGHT"
        Const TreadwearId As String = "PI_TREADWEARID"
        Const Iteration As String = "PI_ITERATION"
        Const OperatorName As String = "ps_OperatorName"
        Const OneNum As Short = 1
        Try
            Connect()
            oraCmd.Connection = Connection
            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = TreadwearDetailSave
            For Each detail As TreadwearDetail In details

                ParametersHelper.AddParametersToCommand(WearBarHeight, ParameterDirection.Input, OracleType.Number, detail.WearBarHeight, oraCmd)
                ParametersHelper.AddParametersToCommand(TreadwearId, ParameterDirection.Input, OracleType.Number, p_intTREADWEARID, oraCmd)
                ParametersHelper.AddParametersToCommand(Iteration, ParameterDirection.Input, OracleType.Number, p_intITERATION, oraCmd)
                ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

                rowsaffected = oraCmd.ExecuteNonQuery()
                oraCmd.Parameters.Clear()
            Next

            If (rowsaffected = OneNum) Then
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
    ''' Saves the tread wear details
    ''' </summary>
    ''' <param name="p_sngwearbarheight">Wearbar height</param>
    ''' <param name="p_intTREADWEARID">Treadwear id</param>
    ''' <param name="p_sngIteration">Iteration value</param>
    ''' <param name="p_strOperatorName">Name of the operator</param>
    ''' <returns>Returns save result as integer</returns>
    ''' ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function SaveTreadWearDetail(ByVal p_sngwearbarheight As Single, _
                                        ByVal p_intTREADWEARID As Integer, _
                                        ByVal p_sngIteration As Single, _
                                        ByVal p_strOperatorName As String) As NameAid.SaveResult


        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Dim rowsaffected As Integer = 0
        Const TreadwearDetailSave As String = "testresults_crud.TREADWEARDETAIL_SAVE"
        Const WearBarHeight As String = "PI_WEARBARHEIGHT"
        Const TreadwearId As String = "PI_TREADWEARID"
        Const Iteration As String = "PI_ITERATION"
        Const OperatorName As String = "ps_OperatorName"
        Const OneNum As Short = 1
        Try
            Connect()
            oraCmd.Connection = Connection
            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = TreadwearDetailSave

            ParametersHelper.AddParametersToCommand(WearBarHeight, ParameterDirection.Input, OracleType.Number, p_sngwearbarheight, oraCmd)
            ParametersHelper.AddParametersToCommand(TreadwearId, ParameterDirection.Input, OracleType.Number, p_intTREADWEARID, oraCmd)
            ParametersHelper.AddParametersToCommand(Iteration, ParameterDirection.Input, OracleType.Number, p_sngIteration, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            rowsaffected = oraCmd.ExecuteNonQuery()

            If (rowsaffected = OneNum) Then
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
    ''' Saves the bead unseat
    ''' </summary>
    ''' <param name="p_strPROJECTNUMBER">Project number</param>
    ''' <param name="p_sngTIRENUMBER">Tire number</param>
    ''' <param name="p_strTESTSPEC">Test specification</param>
    ''' <param name="p_dteCOMPLETIONDATE">Completion date</param>
    ''' <param name="p_strDOTSERIALNUMBER">Dot serial number</param>
    ''' <param name="p_sngLOWESTUNSEATVALUE">Lowest unseat value</param>
    ''' <param name="p_strPassyn">Passyn string</param>
    ''' <param name="p_intCertType">Certificate type</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate number</param>
    ''' <param name="p_intBeadUnseatID">Bead unseat id</param>
    ''' <param name="p_dteSerialDate">Serial date</param>
    ''' <param name="p_sngMINBEADUNSEAT">Minimum bed unseat value</param>
    ''' <param name="p_strTESTPASSFAIL">Test pass or fail string</param>
    ''' <param name="p_strOperatorName">Name of the operator</param>
    ''' <param name="p_intCertificateID">Certificate id</param>
    ''' <param name="p_strMatlNum">SAP material number</param>
    ''' <param name="p_strOperation">Operation number</param>
    ''' <param name="p_strGTSpecBeadUnSeat">Gt Spec for bead unseat</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Const BeadUnseatId As String = "pi_BEADUNSEATID"
        Const ProjectNumber As String = "ps_PROJECTNUMBER"
        Const TireNumber As String = "pi_TIRENUMBER"
        Const TestSpec As String = "ps_TESTSPEC"
        Const Completiondate As String = "pd_COMPLETIONDATE"
        Const DotSerialNumber As String = "ps_DOTSERIALNUMBER"
        Const LowestUnseatValue As String = "pi_LOWESTUNSEATVALUE"
        Const Passyn As String = "ps_PASSYN"
        Const CertificationTypeId As String = "pi_CERTIFICATIONTYPEID"
        Const Serialdate As String = "pd_SERIALDATE"
        Const MinBeadUnseat As String = "pi_MINBEADUNSEAT"
        Const TestPassFail As String = "ps_TESTPASSFAIL"
        Const OperatorName As String = "ps_OperatorName"
        Const CertificateId As String = "pi_CertificateID"
        Const Operation As String = "ps_Operation"
        Const GtSpec As String = "ps_GTSPEC"
        Const MfgWwyy As String = "ps_MFGWWYY"
        Const BeadUnseatSave As String = "testresults_crud.BeadUnseat_Save"
        Const OneNum As Short = 1

        Try
            ParametersHelper.AddParametersToCommand(BeadUnseatId, ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ProjectNumber, ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(TireNumber, ParameterDirection.Input, OracleType.Number, p_sngTIRENUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(TestSpec, ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            ParametersHelper.AddParametersToCommand(Completiondate, ParameterDirection.Input, OracleType.DateTime, p_dteCOMPLETIONDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(DotSerialNumber, ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(LowestUnseatValue, ParameterDirection.Input, OracleType.Number, p_sngLOWESTUNSEATVALUE, oraCmd)
            ParametersHelper.AddParametersToCommand(Passyn, ParameterDirection.Input, OracleType.VarChar, p_strPassyn, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertType, oraCmd)
            ParametersHelper.AddParametersToCommand(Serialdate, ParameterDirection.Input, OracleType.DateTime, p_dteSerialDate, oraCmd)
            ParametersHelper.AddParametersToCommand(MinBeadUnseat, ParameterDirection.Input, OracleType.Number, p_sngMINBEADUNSEAT, oraCmd)
            ParametersHelper.AddParametersToCommand(TestPassFail, ParameterDirection.Input, OracleType.VarChar, p_strTESTPASSFAIL, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateID, oraCmd)
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(Operation, ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)
            ParametersHelper.AddParametersToCommand(GtSpec, ParameterDirection.Input, OracleType.VarChar, p_strGTSpecBeadUnSeat, oraCmd)
            ParametersHelper.AddParametersToCommand(MfgWwyy, ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = BeadUnseatSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Measure Id to be inserted on the detail table
            If oraCmd.Parameters.Item(BeadUnseatId).Value.Equals(DBNull.Value) Then
                p_intBeadUnseatID = UShort.MinValue
            Else
                p_intBeadUnseatID = CInt(oraCmd.Parameters.Item(BeadUnseatId).Value)
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
    ''' Saves the bead unseat details
    ''' </summary>
    ''' <param name="p_intBEADUNSEATID">Bead unseat id</param>
    ''' <param name="p_sngUNSEATFORCE">Unseat force value</param>
    ''' <param name="p_sngIteration">Iteration value</param>
    ''' <param name="p_strOperatorName">Name of the operator</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function SaveBeadUnseatDetail(ByVal p_intBEADUNSEATID As Integer, _
                                         ByVal p_sngUNSEATFORCE As Single, _
                                         ByVal p_sngIteration As Single, _
                                         ByVal p_strOperatorName As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Const BeadUnseatId As String = "pi_BEADUNSEATID"
        Const UnseatForce As String = "pi_UNSEATFORCE"
        Const Iteration As String = "PI_ITERATION"
        Const OperatorName As String = "ps_OperatorName"
        Const BeadUnseatDetailSave As String = "testresults_crud.BeadUnseatDetail_Save"
        Const OneNum As Short = 1
        Try
            ParametersHelper.AddParametersToCommand(BeadUnseatId, ParameterDirection.Input, OracleType.Number, p_intBEADUNSEATID, oraCmd)
            ParametersHelper.AddParametersToCommand(UnseatForce, ParameterDirection.Input, OracleType.Number, p_sngUNSEATFORCE, oraCmd)
            ParametersHelper.AddParametersToCommand(Iteration, ParameterDirection.Input, OracleType.Number, p_sngIteration, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = BeadUnseatDetailSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
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
    ''' Saves the endurance
    ''' </summary>
    ''' <param name="p_intENDURANCEID">Endurance id</param>
    ''' <param name="p_strProjectNumber">Project number</param>
    ''' <param name="p_intTireNumber">Tire number</param>
    ''' <param name="p_strTESTSPEC">Test specification</param>
    ''' <param name="p_dteCOMPLETIONDATE">Completion date</param>
    ''' <param name="p_strDOTSERIALNUMBER">Dot serial number</param>
    ''' <param name="p_dtePRECONDSTARTDATE">Pre condition start date</param>
    ''' <param name="p_sngPRECONDSTARTTEMP">Pre condition start temp</param>
    ''' <param name="p_sngRIMDIAMETER">Rim diameter</param>
    ''' <param name="p_sngRIMWIDTH">Rim width</param>
    ''' <param name="p_dtePRECONDENDDATE">Precond end date</param>
    ''' <param name="p_intPRECONDENDTEMP">Precond end temp</param>
    ''' <param name="p_intINFLATIONPRESSURE">Inflation pressure</param>
    ''' <param name="p_sngBEFOREDIAMETER">Before diameter value</param>
    ''' <param name="p_sngAFTERDIAMETER">After diameter value</param>
    ''' <param name="p_intBEFOREINFLATION">Before inflation value</param>
    ''' <param name="p_intAFTERINFLATION">After inflation value</param>
    ''' <param name="p_intWHEELPOSITION">Wheel position</param>
    ''' <param name="p_intWHEELNUMBER">Wheel number</param>
    ''' <param name="p_intFINALTEMP">Final temp</param>
    ''' <param name="p_sngFINALDISTANCE">Final distance</param>
    ''' <param name="p_intFINALINFLATION">Final inflation</param>
    ''' <param name="p_dtePOSTCONDSTARTDATE">Post condition start date</param>
    ''' <param name="p_dtePOSTCONDENDDATE">Post condition end date</param>
    ''' <param name="p_intPOSTCONDENDTEMP">Post condition end temp</param>
    ''' <param name="p_strPASSYN">Passyn string</param>
    ''' <param name="p_intCertificationTypeID">Certification type id</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate number</param>
    ''' <param name="p_dteSerialDate">Serial date</param>
    ''' <param name="p_sngPostCondTime">Post condition time</param>
    ''' <param name="p_sngPreCondTime">Pre condition time</param>
    ''' <param name="p_sngDIAMETERTESTDRUM">Diameter test drum</param>
    ''' <param name="p_sngPRECONDTEMP">Pre condition temp</param>
    ''' <param name="p_sngINFLATIONPRESSUREREADJUSTED">Inflation pressure adjusted value</param>
    ''' <param name="p_sngCIRCUNFERENCEBEFORETEST">Circumference before test</param>
    ''' <param name="p_strRESULTPASSFAIL">Pass or fail result</param>
    ''' <param name="p_sngENDURANCEHOURS">Endurance hours</param>
    ''' <param name="p_strPOSSIBLEFAILURESFOUND">Possible failures found</param>
    ''' <param name="p_sngCIRCUNFERENCEAFTERTEST">Circumference after test</param>
    ''' <param name="p_sngOUTERDIAMETERDIFERENCE">Outer diameter difference</param>
    ''' <param name="p_sngODDIFERENCETOLERANCE">Outer diameter difference tolerance</param>
    ''' <param name="p_strSERIENOM">SERIENOM</param>
    ''' <param name="p_strFINALJUDGEMENT">Final judgment</param>
    ''' <param name="p_strAPPROVER">Approver</param>
    ''' <param name="p_strOperatorName">Name of the operator</param>
    ''' <param name="p_intCertificateNumberID">Certificate number id</param>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_sngLowInfStartInflation">Low inf start inflation</param>
    ''' <param name="p_sngLowInfEndInflation">Low inf end inflation</param>
    ''' <param name="p_intLowInfEndTemp">Low inf end temp</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strGTSpecEndurance">GT spec endurance</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Const EnduranceId As String = "pi_ENDURANCEID"
        Const ProjectNumber As String = "ps_PROJECTNUMBER"
        Const TireNumber As String = "pi_TIRENUMBER"
        Const TestSpec As String = "ps_TESTSPEC"
        Const CompletionDate As String = "pd_COMPLETIONDATE"
        Const DotSerialNumber As String = "ps_DOTSERIALNUMBER"
        Const MfgWwyy As String = "ps_MFGWWYY"
        Const PrecondStartDate As String = "pd_PRECONDSTARTDATE"
        Const PrecondStartTemp As String = "pi_PRECONDSTARTTEMP"
        Const RimDiameter As String = "pi_RIMDIAMETER"
        Const RimWidth As String = "pi_RIMWIDTH"
        Const PrecondEndDate As String = "pd_PRECONDENDDATE"
        Const PrecondEndTemp As String = "pi_PRECONDENDTEMP"
        Const InflationPressure As String = "pi_INFLATIONPRESSURE"
        Const BeforeDiameter As String = "pi_BEFOREDIAMETER"
        Const AfterDiameter As String = "pi_AFTERDIAMETER"
        Const BeforeInflation As String = "pi_BEFOREINFLATION"
        Const AfterInflation As String = "pi_AFTERINFLATION"
        Const WheelPosition As String = "pi_WHEELPOSITION"
        Const WheelNumber As String = "pi_WHEELNUMBER"
        Const FinalTemp As String = "pi_FINALTEMP"
        Const FinalDistance As String = "pi_FINALDISTANCE"
        Const FinalInflation As String = "pi_FINALINFLATION"
        Const PostCondStartDate As String = "pd_POSTCONDSTARTDATE"
        Const PostCondEndDate As String = "pd_POSTCONDENDDATE"
        Const PostCondEndTemp As String = "pi_POSTCONDENDTEMP"
        Const Passyn As String = "ps_PASSYN"
        Const CertificationTypeId As String = "pi_CertificationTypeID"
        Const SerialDate As String = "pd_SerialDate"
        Const PreCondTime As String = "pi_PreCondTime"
        Const PostCondTime As String = "pi_PostCondTime"
        Const DiameterTestDrum As String = "pi_DIAMETERTESTDRUM"
        Const PreCondTemp As String = "pi_PRECONDTEMP"
        Const InflationPressureReAdjusted As String = "pi_INFLATIONPRESSUREREADJUSTED"
        Const CircunferenceBeforeTest As String = "pi_CIRCUNFERENCEBEFORETEST"
        Const ResultPassFail As String = "ps_RESULTPASSFAIL"
        Const EnduranceHours As String = "pi_ENDURANCEHOURS"
        Const PossibleFailuresFound As String = "ps_POSSIBLEFAILURESFOUND"
        Const CircunferenceAfterTest As String = "pi_CIRCUNFERENCEAFTERTEST"
        Const OuterDiameterDiference As String = "pi_OUTERDIAMETERDIFERENCE"
        Const OdDiferenceTolerance As String = "pi_ODDIFERENCETOLERANCE"
        Const SerieNom As String = "ps_SERIENOM"
        Const FinalJudgement As String = "ps_FINALJUDGEMENT"
        Const Approver As String = "ps_APPROVER"
        Const OperatorName As String = "ps_OperatorName"
        Const CertificateId As String = "pi_certificateid"
        Const LowInfStartInflation As String = "pn_lowInfstartinflation"
        Const LowInfEndInflation As String = "pn_lowInfendinflation"
        Const LowInfEndTemp As String = "pn_lowInfendtemp"
        Const Operation As String = "ps_Operation"
        Const GtSpec As String = "ps_GTSPEC"
        Const EnduranceSave As String = "testresults_crud.Endurance_Save"
        Const OneNum As Short = 1

        Try
            ParametersHelper.AddParametersToCommand(EnduranceId, ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ProjectNumber, ParameterDirection.Input, OracleType.VarChar, p_strProjectNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(TireNumber, ParameterDirection.Input, OracleType.Number, p_intTireNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(TestSpec, ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            ParametersHelper.AddParametersToCommand(CompletionDate, ParameterDirection.Input, OracleType.DateTime, p_dteCOMPLETIONDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(DotSerialNumber, ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(MfgWwyy, ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)
            ParametersHelper.AddParametersToCommand(PrecondStartDate, ParameterDirection.Input, OracleType.DateTime, p_dtePRECONDSTARTDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(PrecondStartTemp, ParameterDirection.Input, OracleType.Number, p_sngPRECONDSTARTTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(RimDiameter, ParameterDirection.Input, OracleType.Number, p_sngRIMDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand(RimWidth, ParameterDirection.Input, OracleType.Number, p_sngRIMWIDTH, oraCmd)
            ParametersHelper.AddParametersToCommand(PrecondEndDate, ParameterDirection.Input, OracleType.DateTime, p_dtePRECONDENDDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(PrecondEndTemp, ParameterDirection.Input, OracleType.Number, p_intPRECONDENDTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(InflationPressure, ParameterDirection.Input, OracleType.Number, p_intINFLATIONPRESSURE, oraCmd)
            ParametersHelper.AddParametersToCommand(BeforeDiameter, ParameterDirection.Input, OracleType.Number, p_sngBEFOREDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand(AfterDiameter, ParameterDirection.Input, OracleType.Number, p_sngAFTERDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand(BeforeInflation, ParameterDirection.Input, OracleType.Number, p_intBEFOREINFLATION, oraCmd)
            ParametersHelper.AddParametersToCommand(AfterInflation, ParameterDirection.Input, OracleType.Number, p_intAFTERINFLATION, oraCmd)
            ParametersHelper.AddParametersToCommand(WheelPosition, ParameterDirection.Input, OracleType.Number, p_intWHEELPOSITION, oraCmd)
            ParametersHelper.AddParametersToCommand(WheelNumber, ParameterDirection.Input, OracleType.Number, p_intWHEELNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(FinalTemp, ParameterDirection.Input, OracleType.Number, p_intFINALTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(FinalDistance, ParameterDirection.Input, OracleType.Number, p_sngFINALDISTANCE, oraCmd)
            ParametersHelper.AddParametersToCommand(FinalInflation, ParameterDirection.Input, OracleType.Number, p_intFINALINFLATION, oraCmd)
            ParametersHelper.AddParametersToCommand(PostCondStartDate, ParameterDirection.Input, OracleType.DateTime, p_dtePOSTCONDSTARTDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(PostCondEndDate, ParameterDirection.Input, OracleType.DateTime, p_dtePOSTCONDENDDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(PostCondEndTemp, ParameterDirection.Input, OracleType.Number, p_intPOSTCONDENDTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(Passyn, ParameterDirection.Input, OracleType.VarChar, p_strPASSYN, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeID, oraCmd)
            ParametersHelper.AddParametersToCommand(SerialDate, ParameterDirection.Input, OracleType.DateTime, p_dteSerialDate, oraCmd)
            ParametersHelper.AddParametersToCommand(PreCondTime, ParameterDirection.Input, OracleType.Number, p_sngPreCondTime, oraCmd)
            ParametersHelper.AddParametersToCommand(PostCondTime, ParameterDirection.Input, OracleType.Number, p_sngPostCondTime, oraCmd)
            ParametersHelper.AddParametersToCommand(DiameterTestDrum, ParameterDirection.Input, OracleType.Number, p_sngDIAMETERTESTDRUM, oraCmd)
            ParametersHelper.AddParametersToCommand(PreCondTemp, ParameterDirection.Input, OracleType.Number, p_sngPRECONDTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(InflationPressureReAdjusted, ParameterDirection.Input, OracleType.Number, p_sngINFLATIONPRESSUREREADJUSTED, oraCmd)
            ParametersHelper.AddParametersToCommand(CircunferenceBeforeTest, ParameterDirection.Input, OracleType.Number, p_sngCIRCUNFERENCEBEFORETEST, oraCmd)
            ParametersHelper.AddParametersToCommand(ResultPassFail, ParameterDirection.Input, OracleType.VarChar, p_strRESULTPASSFAIL, oraCmd)
            ParametersHelper.AddParametersToCommand(EnduranceHours, ParameterDirection.Input, OracleType.Number, p_sngENDURANCEHOURS, oraCmd)
            ParametersHelper.AddParametersToCommand(PossibleFailuresFound, ParameterDirection.Input, OracleType.VarChar, p_strPOSSIBLEFAILURESFOUND, oraCmd)
            ParametersHelper.AddParametersToCommand(CircunferenceAfterTest, ParameterDirection.Input, OracleType.Number, p_sngCIRCUNFERENCEAFTERTEST, oraCmd)
            ParametersHelper.AddParametersToCommand(OuterDiameterDiference, ParameterDirection.Input, OracleType.Number, p_sngOUTERDIAMETERDIFERENCE, oraCmd)
            ParametersHelper.AddParametersToCommand(OdDiferenceTolerance, ParameterDirection.Input, OracleType.Number, p_sngODDIFERENCETOLERANCE, oraCmd)
            ParametersHelper.AddParametersToCommand(SerieNom, ParameterDirection.Input, OracleType.VarChar, p_strSERIENOM, oraCmd)
            ParametersHelper.AddParametersToCommand(FinalJudgement, ParameterDirection.Input, OracleType.VarChar, p_strFINALJUDGEMENT, oraCmd)
            ParametersHelper.AddParametersToCommand(Approver, ParameterDirection.Input, OracleType.VarChar, p_strAPPROVER, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(LowInfStartInflation, ParameterDirection.Input, OracleType.Number, p_sngLowInfStartInflation, oraCmd)
            ParametersHelper.AddParametersToCommand(LowInfEndInflation, ParameterDirection.Input, OracleType.Number, p_sngLowInfEndInflation, oraCmd)
            ParametersHelper.AddParametersToCommand(LowInfEndTemp, ParameterDirection.Input, OracleType.Number, p_intLowInfEndTemp, oraCmd)
            ParametersHelper.AddParametersToCommand(Operation, ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)
            ParametersHelper.AddParametersToCommand(GtSpec, ParameterDirection.Input, OracleType.VarChar, p_strGTSpecEndurance, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = EnduranceSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Measure Id to be inserted on the detail table
            If oraCmd.Parameters.Item(EnduranceId).Value.Equals(DBNull.Value) Then
                p_intENDURANCEID = UInteger.MinValue
            Else
                p_intENDURANCEID = CInt(oraCmd.Parameters.Item(EnduranceId).Value)
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
    ''' Saves the endurance details
    ''' </summary>
    ''' <param name="p_intTESTSTEP">Test step</param>
    ''' <param name="p_intTIMEINMIN">Time in minutes</param>
    ''' <param name="p_intSpeed">Speed</param>
    ''' <param name="p_sngTOTMILES">Total miles</param>
    ''' <param name="p_sngtLOAD">Load value</param>
    ''' <param name="p_sngLOADPERCENT">Load percent</param>
    ''' <param name="p_intSETINFLATION">Set inflation</param>
    ''' <param name="p_intAMBTEMP">Amb temp</param>
    ''' <param name="p_intINFPRESSURE">Inf pressure</param>
    ''' <param name="p_dteSTEPCOMPLETIONDATE">Step completion date</param>
    ''' <param name="p_intENDURANCEID">Endurance id</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        Const TestStep As String = "PI_TESTSTEP"
        Const TimeInMin As String = "pi_TIMEINMIN"
        Const Speed As String = "PI_SPEED"
        Const TotMiles As String = "PI_TOTMILES"
        Const PiLoad As String = "PI_LOAD"
        Const LoadPercent As String = "PI_LOADPERCENT"
        Const SetInflation As String = "PI_SETINFLATION"
        Const AmbTemp As String = "PI_AMBTEMP"
        Const InfPressure As String = "PI_INFPRESSURE"
        Const StepCompletionDate As String = "PD_STEPCOMPLETIONDATE"
        Const EnduranceId As String = "PI_ENDURANCEID"
        Const EnduranceDetailSave As String = "testresults_crud.ENDURANCEDETAIL_SAVE"
        Const OneNum As Short = 1
        Try
            ParametersHelper.AddParametersToCommand(TestStep, ParameterDirection.Input, OracleType.Number, p_intTESTSTEP, oraCmd)
            ParametersHelper.AddParametersToCommand(TimeInMin, ParameterDirection.Input, OracleType.Number, p_intTIMEINMIN, oraCmd)
            ParametersHelper.AddParametersToCommand(Speed, ParameterDirection.Input, OracleType.Number, p_intSpeed, oraCmd)
            ParametersHelper.AddParametersToCommand(TotMiles, ParameterDirection.Input, OracleType.Number, p_sngTOTMILES, oraCmd)
            ParametersHelper.AddParametersToCommand(PiLoad, ParameterDirection.Input, OracleType.Number, p_sngtLOAD, oraCmd)
            ParametersHelper.AddParametersToCommand(LoadPercent, ParameterDirection.Input, OracleType.Number, p_sngLOADPERCENT, oraCmd)
            ParametersHelper.AddParametersToCommand(SetInflation, ParameterDirection.Input, OracleType.Number, p_intSETINFLATION, oraCmd)
            ParametersHelper.AddParametersToCommand(AmbTemp, ParameterDirection.Input, OracleType.Number, p_intAMBTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(InfPressure, ParameterDirection.Input, OracleType.Number, p_intINFPRESSURE, oraCmd)
            ParametersHelper.AddParametersToCommand(StepCompletionDate, ParameterDirection.Input, OracleType.VarChar, p_dteSTEPCOMPLETIONDATE.ToShortDateString(), oraCmd)
            ParametersHelper.AddParametersToCommand(EnduranceId, ParameterDirection.Input, OracleType.Number, p_intENDURANCEID, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = EnduranceDetailSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
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
    ''' Saves high speed header table data
    ''' </summary>
    ''' <param name="p_intHighSpeedID">High speed id</param>
    ''' <param name="p_strPROJECTNUMBER">Project number</param>
    ''' <param name="p_intTIRENUM">Tire number</param>
    ''' <param name="p_strTESTSPEC">Test spec</param>
    ''' <param name="p_dteCOMPETIONDATE">Completion date</param>
    ''' <param name="p_strDOTSERIALNUMBER">Dot serial number</param>
    ''' <param name="p_strMFGWWYY">MfgWwyy</param>
    ''' <param name="p_dtePRECONDSTARTDATE">Pre condition start date</param>
    ''' <param name="p_intPRECONDSARTTEMP">Pre condition start temp</param>
    ''' <param name="p_sngRIMDIAMETER">Rim diameter</param>
    ''' <param name="p_sngRIMWIDTH">Rim width</param>
    ''' <param name="p_dtePRECONDENDDATE">Pre condition end date</param>
    ''' <param name="p_intPRECONDENDTEMP">Pre condition end temp</param>
    ''' <param name="p_intINFLATIONPRESSURE">Inflation pressure</param>
    ''' <param name="p_sngBEFOREDIAMETER">Before diameter</param>
    ''' <param name="p_sngAFTERDIAMETER">After diameter</param>
    ''' <param name="p_intBEFOREINFLATION">Before inflation</param>
    ''' <param name="p_intAFTERINFLATION">After inflation</param>
    ''' <param name="p_intWHEELPOSITION">Wheel position</param>
    ''' <param name="p_intWHEELNUMBER">Wheel number</param>
    ''' <param name="p_intFINALTEMP">Final temp</param>
    ''' <param name="p_sngFINALDISTANCE">Final distance</param>
    ''' <param name="p_intFINALINFLATION">Final inflation</param>
    ''' <param name="p_dtePOSTCONDSTARTDATE">Post condition start date</param>
    ''' <param name="p_dtePOSTCONDENDDATE">Post condition end date</param>
    ''' <param name="p_intPOSTCONDENDTEMP">Post condition end temp</param>
    ''' <param name="p_sngPRECONDTIME">Pre condition time</param>
    ''' <param name="p_sngPOSTCONDTIME">Post condition time</param>
    ''' <param name="p_strPASSYN">Passyn</param>
    ''' <param name="p_dteSERIALDATE">Serial date</param>
    ''' <param name="p_intCERTIFICATIONTYPEID">Certification type id</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate number</param>
    ''' <param name="p_sngDIAMETERTESTDRUM">Diameter test drum</param>
    ''' <param name="p_sngPRECONDTEMP">Pre condition temp</param>
    ''' <param name="p_sngINFLATIONPRESSUREREADJUSTED">Inflation pressure readjusted value</param>
    ''' <param name="p_sngCIRCUNFERENCEBEFORETEST">Circumference before test</param>
    ''' <param name="p_sngWHEELSPEEDRPM">Wheel speed RPM</param>
    ''' <param name="p_sngWHEELSPEEDKMH">Wheel speed KMH</param>
    ''' <param name="p_sngCIRCUNFERENCEAFTERTEST">Circumference after test</param>
    ''' <param name="p_sngODDIFERENCE">Outer diameter difference</param>
    ''' <param name="p_sngODDIFERENCETOLERANCE">Outer diameter difference tolerance</param>
    ''' <param name="p_strSERIENOM">SerieNOM</param>
    ''' <param name="p_strFINALJUDGEMENT">Final Judgment</param>
    ''' <param name="p_strAPPROVER">Approver</param>
    ''' <param name="p_sngPASSATKMH">Pass at KMH</param>
    ''' <param name="p_strSPEEDTTESTPASSFAIL">Speed test pass or fail</param>
    ''' <param name="p_sngSPEEDTOTALTIME">Speed total time</param>
    ''' <param name="p_sngMAXSPEED">Maximum speed</param>
    ''' <param name="p_sngMAXLOAD">Maximum load</param>
    ''' <param name="p_strOperatorName">Operator name</param>
    ''' <param name="p_intCertificateNumberID">Certificate number id</param>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strGTSpecHighSpeed">GT spec high speed</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Const HighSpeedId As String = "pi_HIGHSPEEDID"
        Const ProjectNumber As String = "ps_PROJECTNUMBER"
        Const TireNum As String = "pi_TIRENUM"
        Const TestSpec As String = "ps_TESTSPEC"
        Const CompetionDate As String = "pd_COMPETIONDATE"
        Const DotSerialNumber As String = "ps_DOTSERIALNUMBER"
        Const MfgWwyy As String = "ps_MFGWWYY"
        Const PreCondStartDate As String = "pd_PRECONDSTARTDATE"
        Const PreCondSartTemp As String = "pi_PRECONDSARTTEMP"
        Const PreCondTime As String = "pd_PRECONDTIME"
        Const RimDiameter As String = "pi_RIMDIAMETER"
        Const RimWidth As String = "pi_RIMWIDTH"
        Const PreCondEndDate As String = "pd_PRECONDENDDATE"
        Const PreCondEndTemp As String = "pi_PRECONDENDTEMP"
        Const InflationPressure As String = "pi_INFLATIONPRESSURE"
        Const BeforeDiameter As String = "pi_BEFOREDIAMETER"
        Const AfterDiameter As String = "pi_AFTERDIAMETER"
        Const BeforeInflation As String = "pi_BEFOREINFLATION"
        Const AfterInflation As String = "pi_AFTERINFLATION"
        Const WheelPosition As String = "pi_WHEELPOSITION"
        Const WheelNumber As String = "pi_WHEELNUMBER"
        Const FinalTemp As String = "pi_FINALTEMP"
        Const FinalDistance As String = "pi_FINALDISTANCE"
        Const FinalInflation As String = "pi_FINALINFLATION"
        Const PostCondStartDate As String = "pd_POSTCONDSTARTDATE"
        Const PostCondEndDate As String = "pd_POSTCONDENDDATE"
        Const PostCondEndTemp As String = "pi_POSTCONDENDTEMP"
        Const Passyn As String = "ps_PASSYN"
        Const SerialDate As String = "pd_SERIALDATE"
        Const PostCondTime As String = "pi_POSTCONDTIME"
        Const CertificationTypeId As String = "pi_CERTIFICATIONTYPEID"
        Const DiameterTestDrum As String = "pi_DIAMETERTESTDRUM"
        Const PreCondTemp As String = "pi_PRECONDTEMP"
        Const InflationPressureReAdjusted As String = "pi_INFLATIONPRESSUREREADJUSTED"
        Const CircunferenceBeforeTest As String = "pi_CIRCUNFERENCEBEFORETEST"
        Const WheelSpeedRpm As String = "pi_WHEELSPEEDRPM"
        Const WheelSpeedKmh As String = "pi_WHEELSPEEDKMH"
        Const CircunferenceAfterTest As String = "pi_CIRCUNFERENCEAFTERTEST"
        Const OdDiference As String = "pi_ODDIFERENCE"
        Const OddiferenceTolerance As String = "pi_ODDIFERENCETOLERANCE"
        Const SerieNom As String = "ps_SERIENOM"
        Const FinalJudgement As String = "ps_FINALJUDGEMENT"
        Const Approver As String = "ps_APPROVER"
        Const PassAtKmh As String = "pi_PASSATKMH"
        Const SpeedTtestPassFail As String = "ps_SPEEDTTESTPASSFAIL"
        Const SpeedTotalTime As String = "pi_SPEEDTOTALTIME"
        Const MaxSpeed As String = "pi_MAXSPEED"
        Const MaxLoad As String = "pi_MAXLOAD"
        Const OperatorName As String = "ps_OperatorName"
        Const CertificateId As String = "pi_CertificateID"
        Const Operation As String = "ps_Operation"
        Const GtSpec As String = "ps_GTSPEC"
        Const HighSpeedHdrSave As String = "testresults_crud.HighSpeedHdr_Save"
        Const OneNum As Short = 1

        Try
            ParametersHelper.AddParametersToCommand(HighSpeedId, ParameterDirection.Output, OracleType.Number, p_intHighSpeedID, oraCmd)
            ParametersHelper.AddParametersToCommand(ProjectNumber, ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(TireNum, ParameterDirection.Input, OracleType.Number, p_intTIRENUM, oraCmd)
            ParametersHelper.AddParametersToCommand(TestSpec, ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            ParametersHelper.AddParametersToCommand(CompetionDate, ParameterDirection.Input, OracleType.DateTime, p_dteCOMPETIONDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(DotSerialNumber, ParameterDirection.Input, OracleType.VarChar, p_strDOTSERIALNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(MfgWwyy, ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)
            ParametersHelper.AddParametersToCommand(PreCondStartDate, ParameterDirection.Input, OracleType.DateTime, p_dtePRECONDSTARTDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(PreCondSartTemp, ParameterDirection.Input, OracleType.Number, p_intPRECONDSARTTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(PreCondTime, ParameterDirection.Input, OracleType.Number, p_sngPRECONDTIME, oraCmd)
            ParametersHelper.AddParametersToCommand(RimDiameter, ParameterDirection.Input, OracleType.Number, p_sngRIMDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand(RimWidth, ParameterDirection.Input, OracleType.Number, p_sngRIMWIDTH, oraCmd)
            ParametersHelper.AddParametersToCommand(PreCondEndDate, ParameterDirection.Input, OracleType.DateTime, p_dtePRECONDENDDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(PreCondEndTemp, ParameterDirection.Input, OracleType.Number, p_intPRECONDENDTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(InflationPressure, ParameterDirection.Input, OracleType.Number, p_intINFLATIONPRESSURE, oraCmd)
            ParametersHelper.AddParametersToCommand(BeforeDiameter, ParameterDirection.Input, OracleType.Number, p_sngBEFOREDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand(AfterDiameter, ParameterDirection.Input, OracleType.Number, p_sngAFTERDIAMETER, oraCmd)
            ParametersHelper.AddParametersToCommand(BeforeInflation, ParameterDirection.Input, OracleType.Number, p_intBEFOREINFLATION, oraCmd)
            ParametersHelper.AddParametersToCommand(AfterInflation, ParameterDirection.Input, OracleType.Number, p_intAFTERINFLATION, oraCmd)
            ParametersHelper.AddParametersToCommand(WheelPosition, ParameterDirection.Input, OracleType.Number, p_intWHEELPOSITION, oraCmd)
            ParametersHelper.AddParametersToCommand(WheelNumber, ParameterDirection.Input, OracleType.Number, p_intWHEELNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(FinalTemp, ParameterDirection.Input, OracleType.Number, p_intFINALTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(FinalDistance, ParameterDirection.Input, OracleType.Number, p_sngFINALDISTANCE, oraCmd)
            ParametersHelper.AddParametersToCommand(FinalInflation, ParameterDirection.Input, OracleType.Number, p_intFINALINFLATION, oraCmd)
            ParametersHelper.AddParametersToCommand(PostCondStartDate, ParameterDirection.Input, OracleType.DateTime, p_dtePOSTCONDSTARTDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(PostCondEndDate, ParameterDirection.Input, OracleType.DateTime, p_dtePOSTCONDENDDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(PostCondEndTemp, ParameterDirection.Input, OracleType.Number, p_intPOSTCONDENDTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(Passyn, ParameterDirection.Input, OracleType.VarChar, p_strPASSYN, oraCmd)
            ParametersHelper.AddParametersToCommand(SerialDate, ParameterDirection.Input, OracleType.DateTime, p_dteSERIALDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(PostCondTime, ParameterDirection.Input, OracleType.Number, p_sngPOSTCONDTIME, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCERTIFICATIONTYPEID, oraCmd)
            ParametersHelper.AddParametersToCommand(DiameterTestDrum, ParameterDirection.Input, OracleType.Number, p_sngDIAMETERTESTDRUM, oraCmd)
            ParametersHelper.AddParametersToCommand(PreCondTemp, ParameterDirection.Input, OracleType.Number, p_sngPRECONDTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(InflationPressureReAdjusted, ParameterDirection.Input, OracleType.Number, p_sngINFLATIONPRESSUREREADJUSTED, oraCmd)
            ParametersHelper.AddParametersToCommand(CircunferenceBeforeTest, ParameterDirection.Input, OracleType.Number, p_sngCIRCUNFERENCEBEFORETEST, oraCmd)
            ParametersHelper.AddParametersToCommand(WheelSpeedRpm, ParameterDirection.Input, OracleType.Number, p_sngWHEELSPEEDRPM, oraCmd)
            ParametersHelper.AddParametersToCommand(WheelSpeedKmh, ParameterDirection.Input, OracleType.Number, p_sngWHEELSPEEDKMH, oraCmd)
            ParametersHelper.AddParametersToCommand(CircunferenceAfterTest, ParameterDirection.Input, OracleType.Number, p_sngCIRCUNFERENCEAFTERTEST, oraCmd)
            ParametersHelper.AddParametersToCommand(OdDiference, ParameterDirection.Input, OracleType.Number, p_sngODDIFERENCE, oraCmd)
            ParametersHelper.AddParametersToCommand(OddiferenceTolerance, ParameterDirection.Input, OracleType.Number, p_sngODDIFERENCETOLERANCE, oraCmd)
            ParametersHelper.AddParametersToCommand(SerieNom, ParameterDirection.Input, OracleType.VarChar, p_strSERIENOM, oraCmd)
            ParametersHelper.AddParametersToCommand(FinalJudgement, ParameterDirection.Input, OracleType.VarChar, p_strFINALJUDGEMENT, oraCmd)
            ParametersHelper.AddParametersToCommand(Approver, ParameterDirection.Input, OracleType.VarChar, p_strAPPROVER, oraCmd)
            ParametersHelper.AddParametersToCommand(PassAtKmh, ParameterDirection.Input, OracleType.Number, p_sngPASSATKMH, oraCmd)
            ParametersHelper.AddParametersToCommand(SpeedTtestPassFail, ParameterDirection.Input, OracleType.VarChar, p_strSPEEDTTESTPASSFAIL, oraCmd)
            ParametersHelper.AddParametersToCommand(SpeedTotalTime, ParameterDirection.Input, OracleType.Number, p_sngSPEEDTOTALTIME, oraCmd)
            ParametersHelper.AddParametersToCommand(MaxSpeed, ParameterDirection.Input, OracleType.Number, p_sngMAXSPEED, oraCmd)
            ParametersHelper.AddParametersToCommand(MaxLoad, ParameterDirection.Input, OracleType.Number, p_sngMAXLOAD, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(Operation, ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)
            ParametersHelper.AddParametersToCommand(GtSpec, ParameterDirection.Input, OracleType.VarChar, p_strGTSpecHighSpeed, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = HighSpeedHdrSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Measure Id to be inserted on the detail table
            If oraCmd.Parameters.Item(HighSpeedId).Value.Equals(DBNull.Value) Then
                p_intHighSpeedID = UInteger.MinValue
            Else
                p_intHighSpeedID = CInt(oraCmd.Parameters.Item(HighSpeedId).Value)
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
    ''' Saves the high speed details
    ''' </summary>
    ''' <param name="p_intHighSpeedID">High speed id</param>
    ''' <param name="p_strOperatorId">Operator id</param>
    ''' <param name="p_intTESTSTEP">Test step value</param>
    ''' <param name="p_intTimeMin">Time in minutes</param>
    ''' <param name="p_sngSpeed">Speed</param>
    ''' <param name="p_sngTotMiles">Total miles</param>
    ''' <param name="p_sngLoad">Load value</param>
    ''' <param name="p_intLoadPercent">Load percent</param>
    ''' <param name="p_intSetInflation">Set inflation</param>
    ''' <param name="p_intAmbTemp">Amb temp</param>
    ''' <param name="p_intInfPressure">Inf pressure</param>
    ''' <param name="p_dteStepCompletionDate">Step completion date</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        Const HighSpeedId As String = "pi_HIGHSPEEDID"
        Const TestStep As String = "pi_TESTSTEP"
        Const TimeInMin As String = "pi_TIMEINMIN"
        Const Speed As String = "pi_SPEED"
        Const TotMiles As String = "pi_TOTMILES"
        Const PiLoad As String = "pi_LOAD"
        Const LoadPercent As String = "pi_LOADPERCENT"
        Const SetInflation As String = "pi_SETINFLATION"
        Const AmbTemp As String = "pi_AMBTEMP"
        Const InfPressure As String = "pi_INFPRESSURE"
        Const StepCompletionDate As String = "pd_STEPCOMPLETIONDATE"
        Const OperatorId As String = "ps_OperatorID"
        Const HighSpeedDetailSave As String = "testresults_crud.HighSpeedDetail_Save"
        Const OneNum As Short = 1

        Try
            ParametersHelper.AddParametersToCommand(HighSpeedId, ParameterDirection.Input, OracleType.Number, p_intHighSpeedID, oraCmd)
            ParametersHelper.AddParametersToCommand(TestStep, ParameterDirection.Input, OracleType.Number, p_intTESTSTEP, oraCmd)
            ParametersHelper.AddParametersToCommand(TimeInMin, ParameterDirection.Input, OracleType.Number, p_intTimeMin, oraCmd)
            ParametersHelper.AddParametersToCommand(Speed, ParameterDirection.Input, OracleType.Number, p_sngSpeed, oraCmd)
            ParametersHelper.AddParametersToCommand(TotMiles, ParameterDirection.Input, OracleType.Number, p_sngTotMiles, oraCmd)
            ParametersHelper.AddParametersToCommand(PiLoad, ParameterDirection.Input, OracleType.Number, p_sngLoad, oraCmd)
            ParametersHelper.AddParametersToCommand(LoadPercent, ParameterDirection.Input, OracleType.Number, p_intLoadPercent, oraCmd)
            ParametersHelper.AddParametersToCommand(SetInflation, ParameterDirection.Input, OracleType.Number, p_intSetInflation, oraCmd)
            ParametersHelper.AddParametersToCommand(AmbTemp, ParameterDirection.Input, OracleType.Number, p_intAmbTemp, oraCmd)
            ParametersHelper.AddParametersToCommand(InfPressure, ParameterDirection.Input, OracleType.Number, p_intInfPressure, oraCmd)
            If p_dteStepCompletionDate.Equals(DateTime.MinValue) Then
                ParametersHelper.AddParametersToCommand(StepCompletionDate, ParameterDirection.Input, OracleType.VarChar, DBNull.Value, oraCmd)
            Else
                ParametersHelper.AddParametersToCommand(StepCompletionDate, ParameterDirection.Input, OracleType.VarChar, p_dteStepCompletionDate.ToShortDateString(), oraCmd)
            End If
            ParametersHelper.AddParametersToCommand(OperatorId, ParameterDirection.Input, OracleType.VarChar, p_strOperatorId, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = HighSpeedDetailSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
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
    ''' Saves High speed speed test detail table 
    ''' </summary>
    ''' <param name="p_intHighSpeedID">High speed id</param>
    ''' <param name="p_intIteration">Iteration value</param>
    ''' <param name="p_dteTime">Time</param>
    ''' <param name="p_sngSpeed">Speed</param>
    ''' <param name="p_strUserName">User name</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function SaveHighSpeed_SpeedTestDetail(ByVal p_intHighSpeedID As Integer, _
                                                  ByVal p_intIteration As Integer, _
                                                  ByVal p_dteTime As DateTime, _
                                                  ByVal p_sngSpeed As Single, _
                                                  ByVal p_strUserName As String) As NameAid.SaveResult

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Const Iteration As String = "pi_ITERATION"
        Const PdTime As String = "pd_TIME"
        Const Speed As String = "pi_SPEED"
        Const HighSpeedId As String = "pi_HIGHSPEEDID"
        Const OperatorName As String = "ps_OperatorName"
        Const HighSpeedTestDetailSave As String = "testresults_crud.HIghSpeed_SpeedTestDetail_Save"
        Const OneNum As Short = 1
        Try
            ParametersHelper.AddParametersToCommand(Iteration, ParameterDirection.Input, OracleType.Number, p_intIteration, oraCmd)
            ParametersHelper.AddParametersToCommand(PdTime, ParameterDirection.Input, OracleType.DateTime, p_dteTime, oraCmd)
            ParametersHelper.AddParametersToCommand(Speed, ParameterDirection.Input, OracleType.Number, p_sngSpeed, oraCmd)
            ParametersHelper.AddParametersToCommand(HighSpeedId, ParameterDirection.Input, OracleType.Number, p_intHighSpeedID, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = HighSpeedTestDetailSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
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
    ''' Saves sound header table
    ''' </summary>
    ''' <param name="p_strUserId">User id</param>
    ''' <param name="p_intSoundID">Sound id</param>
    ''' <param name="p_strPROJECTNUMBER">Project number</param>
    ''' <param name="p_intTIRENUM">Tire number</param>
    ''' <param name="p_strTESTSPEC">Test spec</param>
    ''' <param name="p_strTESTREPORTNUMBER">Test report number</param>
    ''' <param name="p_strMANUFACTUREANDBRAND">Manufacture and brand</param>
    ''' <param name="p_strTIRECLASS">Tire class</param>
    ''' <param name="p_strCATEGORYOFUSE">Category of use</param>
    ''' <param name="p_dteDATEOFTEST">Date of test</param>
    ''' <param name="p_strTESTVEHICULE">Test vehicle</param>
    ''' <param name="p_strTESTVEHICULEWHEELBASE">Test vehicle wheel base</param>
    ''' <param name="p_strLOCATIONOFTESTTRACK">Location of test track</param>
    ''' <param name="p_dteDATETRACKCERTIFTOISO">Track certificate date</param>
    ''' <param name="p_strTIRESIZEDESIGNATION">Tire size designation</param>
    ''' <param name="p_strTIRESERVICEDESCRIPTION">Tire service description</param>
    ''' <param name="p_strREFERENCEINFLATIONPRESSURE">Reference inflation pressure</param>
    ''' <param name="p_strTESTMASS_FRONTL">Test mass Front left</param>
    ''' <param name="p_strTESTMASS_FRONTR">Test mass front right</param>
    ''' <param name="p_strTESTMASS_REARL">Test mass rear left</param>
    ''' <param name="p_strTESTMASS_REARR">Test mass rear right</param>
    ''' <param name="p_strTIRELOADINDEX_FRONTL">Tire load index front left</param>
    ''' <param name="p_strTIRELOADINDEX_FRONTR">Tire load index front right</param>
    ''' <param name="p_strTIRELOADINDEX_REARL">Tire load index rear left</param>
    ''' <param name="p_strTIRELOADINDEX_REARR">Tire load index rear right</param>
    ''' <param name="p_strINFLATIONPRESSURECO_FRONTL">Inflation pressure front left</param>
    ''' <param name="p_strINFLATIONPRESSURECO_FRONTR">Inflation pressure front right</param>
    ''' <param name="p_strINFLATIONPRESSURECO_REARL">Inflation pressure rear left</param>
    ''' <param name="p_strINFLATIONPRESSURECO_REARR">Inflation pressure rear right</param>
    ''' <param name="p_strTESTRIMWIDTHCODE">Test rim width code</param>
    ''' <param name="p_strTEMPMEASURESENSORTYPE">Temporary measure sensor type</param>
    ''' <param name="p_intCERTIFICATIONTYPEID">Certification type id</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate number</param>
    ''' <param name="p_intSKUID">SKU id</param>
    ''' <param name="p_intCertificateNUmberID">Certificate number id</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strGTSpecSound">GT spec sound</param>
    ''' <param name="p_strMFGWWYY">MfgWwyy</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Const UserId As String = "ps_UserID"
        Const SoundId As String = "pi_SoundID"
        Const ProjectNumber As String = "ps_PROJECTNUMBER"
        Const TireNumber As String = "pi_TIRENUMBER"
        Const TestSpec As String = "ps_TESTSPEC"
        Const TestReportNumber As String = "ps_TESTREPORTNUMBER"
        Const ManufactureAndBrand As String = "ps_MANUFACTUREANDBRAND"
        Const TireClass As String = "ps_TIRECLASS"
        Const CategoryOfUse As String = "ps_CATEGORYOFUSE"
        Const DateOfTest As String = "pd_DATEOFTEST"
        Const TestVehicule As String = "ps_TESTVEHICULE"
        Const TestVehiculeWheelBase As String = "ps_TESTVEHICULEWHEELBASE"
        Const LocationOfTestTrack As String = "ps_LOCATIONOFTESTTRACK"
        Const DateTrackCertifToiso As String = "pd_DATETRACKCERTIFTOISO"
        Const TireSizeDesignation As String = "ps_TIRESIZEDESIGNATION"
        Const TireServiceDescription As String = "ps_TIRESERVICEDESCRIPTION"
        Const ReferenceInflationPressure As String = "ps_REFERENCEINFLATIONPRESSURE"
        Const TestmassFrontL As String = "ps_TESTMASS_FRONTL"
        Const TestmassFrontR As String = "ps_TESTMASS_FRONTR"
        Const TestmassRearL As String = "ps_TESTMASS_REARL"
        Const TestmassRearR As String = "ps_TESTMASS_REARR"
        Const TireLoadIndexFrontL As String = "ps_TIRELOADINDEX_FRONTL"
        Const TireLoadIndexFrontR As String = "ps_TIRELOADINDEX_FRONTR"
        Const TireLoadIndexRearL As String = "ps_TIRELOADINDEX_REARL"
        Const TireLoadIndexRearR As String = "ps_TIRELOADINDEX_REARR"
        Const InflationPressureCoFrontL As String = "ps_INFLATIONPRESSURECO_FRONTL"
        Const InflationPressureCoFrontR As String = "ps_INFLATIONPRESSURECO_FRONTR"
        Const InflationPressureCoRearL As String = "ps_INFLATIONPRESSURECO_REARL"
        Const InflationPressureCoRearR As String = "ps_INFLATIONPRESSURECO_REARR"
        Const TestRimWidthCode As String = "ps_TESTRIMWIDTHCODE"
        Const TempMeasureSensorType As String = "ps_TEMPMEASURESENSORTYPE"
        Const CertificationTypeId As String = "pi_CERTIFICATIONTYPEID"
        Const SkuId As String = "pi_SKUID"
        Const CertificateId As String = "pi_CertificateID"
        Const Operation As String = "ps_Operation"
        Const GtSpec As String = "ps_GTSPEC"
        Const MfgWwyy As String = "ps_MFGWWYY"
        Const SoundHDRSave As String = "testresults_crud.SoundHDR_Save"
        Const OneNum As Short = 1
        Try
            ParametersHelper.AddParametersToCommand(UserId, ParameterDirection.Input, OracleType.VarChar, p_strUserId, oraCmd)
            ParametersHelper.AddParametersToCommand(SoundId, ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ProjectNumber, ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(TireNumber, ParameterDirection.Input, OracleType.Number, p_intTIRENUM, oraCmd)
            ParametersHelper.AddParametersToCommand(TestSpec, ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            ParametersHelper.AddParametersToCommand(TestReportNumber, ParameterDirection.Input, OracleType.VarChar, p_strTESTREPORTNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(ManufactureAndBrand, ParameterDirection.Input, OracleType.VarChar, p_strMANUFACTUREANDBRAND, oraCmd)
            ParametersHelper.AddParametersToCommand(TireClass, ParameterDirection.Input, OracleType.VarChar, p_strTIRECLASS, oraCmd)
            ParametersHelper.AddParametersToCommand(CategoryOfUse, ParameterDirection.Input, OracleType.VarChar, p_strCATEGORYOFUSE, oraCmd)
            ParametersHelper.AddParametersToCommand(DateOfTest, ParameterDirection.Input, OracleType.DateTime, p_dteDATEOFTEST, oraCmd)
            ParametersHelper.AddParametersToCommand(TestVehicule, ParameterDirection.Input, OracleType.VarChar, p_strTESTVEHICULE, oraCmd)
            ParametersHelper.AddParametersToCommand(TestVehiculeWheelBase, ParameterDirection.Input, OracleType.VarChar, p_strTESTVEHICULEWHEELBASE, oraCmd)
            ParametersHelper.AddParametersToCommand(LocationOfTestTrack, ParameterDirection.Input, OracleType.VarChar, p_strLOCATIONOFTESTTRACK, oraCmd)
            ParametersHelper.AddParametersToCommand(DateTrackCertifToiso, ParameterDirection.Input, OracleType.DateTime, p_dteDATETRACKCERTIFTOISO, oraCmd)
            ParametersHelper.AddParametersToCommand(TireSizeDesignation, ParameterDirection.Input, OracleType.VarChar, p_strTIRESIZEDESIGNATION, oraCmd)
            ParametersHelper.AddParametersToCommand(TireServiceDescription, ParameterDirection.Input, OracleType.VarChar, p_strTIRESERVICEDESCRIPTION, oraCmd)
            ParametersHelper.AddParametersToCommand(ReferenceInflationPressure, ParameterDirection.Input, OracleType.VarChar, p_strREFERENCEINFLATIONPRESSURE, oraCmd)
            ParametersHelper.AddParametersToCommand(TestmassFrontL, ParameterDirection.Input, OracleType.VarChar, p_strTESTMASS_FRONTL, oraCmd)
            ParametersHelper.AddParametersToCommand(TestmassFrontR, ParameterDirection.Input, OracleType.VarChar, p_strTESTMASS_FRONTR, oraCmd)
            ParametersHelper.AddParametersToCommand(TestmassRearL, ParameterDirection.Input, OracleType.VarChar, p_strTESTMASS_REARL, oraCmd)
            ParametersHelper.AddParametersToCommand(TestmassRearR, ParameterDirection.Input, OracleType.VarChar, p_strTESTMASS_REARR, oraCmd)
            ParametersHelper.AddParametersToCommand(TireLoadIndexFrontL, ParameterDirection.Input, OracleType.VarChar, p_strTIRELOADINDEX_FRONTL, oraCmd)
            ParametersHelper.AddParametersToCommand(TireLoadIndexFrontR, ParameterDirection.Input, OracleType.VarChar, p_strTIRELOADINDEX_FRONTR, oraCmd)
            ParametersHelper.AddParametersToCommand(TireLoadIndexRearL, ParameterDirection.Input, OracleType.VarChar, p_strTIRELOADINDEX_REARL, oraCmd)
            ParametersHelper.AddParametersToCommand(TireLoadIndexRearR, ParameterDirection.Input, OracleType.VarChar, p_strTIRELOADINDEX_REARR, oraCmd)
            ParametersHelper.AddParametersToCommand(InflationPressureCoFrontL, ParameterDirection.Input, OracleType.VarChar, p_strINFLATIONPRESSURECO_FRONTL, oraCmd)
            ParametersHelper.AddParametersToCommand(InflationPressureCoFrontR, ParameterDirection.Input, OracleType.VarChar, p_strINFLATIONPRESSURECO_FRONTR, oraCmd)
            ParametersHelper.AddParametersToCommand(InflationPressureCoRearL, ParameterDirection.Input, OracleType.VarChar, p_strINFLATIONPRESSURECO_REARL, oraCmd)
            ParametersHelper.AddParametersToCommand(InflationPressureCoRearR, ParameterDirection.Input, OracleType.VarChar, p_strINFLATIONPRESSURECO_REARR, oraCmd)
            ParametersHelper.AddParametersToCommand(TestRimWidthCode, ParameterDirection.Input, OracleType.VarChar, p_strTESTRIMWIDTHCODE, oraCmd)
            ParametersHelper.AddParametersToCommand(TempMeasureSensorType, ParameterDirection.Input, OracleType.VarChar, p_strTEMPMEASURESENSORTYPE, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCERTIFICATIONTYPEID, oraCmd)
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_intSKUID, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateNUmberID, oraCmd)
            ParametersHelper.AddParametersToCommand(Operation, ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)
            ParametersHelper.AddParametersToCommand(GtSpec, ParameterDirection.Input, OracleType.VarChar, p_strGTSpecSound, oraCmd)
            ParametersHelper.AddParametersToCommand(MfgWwyy, ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)
            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SoundHDRSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Measure Id to be inserted on the detail table
            If oraCmd.Parameters.Item(SoundId).Value.Equals(DBNull.Value) Then
                p_intSoundID = UInteger.MinValue
            Else
                p_intSoundID = CInt(oraCmd.Parameters.Item(SoundId).Value)
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
    ''' Saves sound detail table
    ''' </summary>
    ''' <param name="p_strUserId">User id</param>
    ''' <param name="p_intITERATION">Iteration</param>
    ''' <param name="p_strTESTSPEED">Test speed</param>
    ''' <param name="p_strDIRECTIONOFRUN">Direction of run</param>
    ''' <param name="p_strSOUNDLEVELLEFT">Sound level left</param>
    ''' <param name="p_strSOUNDLEVELRIGHT">Sound level right</param>
    ''' <param name="p_strAIRTEMP">Air temperature</param>
    ''' <param name="p_strTRACKTEMP">Track temperature</param>
    ''' <param name="p_strSOUNDLEVELLEFT_TEMPCOR">Sound level left temp cor</param>
    ''' <param name="p_strSOUNDLEVELRIGHT_TEMPCOR">Sound level right temp cor</param>
    ''' <param name="p_intSoundID">Sound id</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        Const UserId As String = "ps_UserID"
        Const Iteration As String = "pi_ITERATION"
        Const TestSpeed As String = "ps_TESTSPEED"
        Const DirectionOfRun As String = "ps_DIRECTIONOFRUN"
        Const SoundLevelLeft As String = "ps_SOUNDLEVELLEFT"
        Const SoundLevelRight As String = "ps_SOUNDLEVELRIGHT"
        Const AirTemp As String = "ps_AIRTEMP"
        Const TrackTemp As String = "ps_TRACKTEMP"
        Const SoundLevelLeftTempCor As String = "ps_SOUNDLEVELLEFT_TEMPCOR"
        Const SoundLevelRightTempCor As String = "ps_SOUNDLEVELRIGHT_TEMPCOR"
        Const SoundId As String = "pi_SOUNDID"
        Const SoundDetailSave As String = "testresults_crud.SoundDetail_Save"
        Const OneNum As Short = 1
        Try
            ParametersHelper.AddParametersToCommand(UserId, ParameterDirection.Input, OracleType.VarChar, p_strUserId, oraCmd)
            ParametersHelper.AddParametersToCommand(Iteration, ParameterDirection.Input, OracleType.Number, p_intITERATION, oraCmd)
            ParametersHelper.AddParametersToCommand(TestSpeed, ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEED, oraCmd)
            ParametersHelper.AddParametersToCommand(DirectionOfRun, ParameterDirection.Input, OracleType.VarChar, p_strDIRECTIONOFRUN, oraCmd)
            ParametersHelper.AddParametersToCommand(SoundLevelLeft, ParameterDirection.Input, OracleType.VarChar, p_strSOUNDLEVELLEFT, oraCmd)
            ParametersHelper.AddParametersToCommand(SoundLevelRight, ParameterDirection.Input, OracleType.VarChar, p_strSOUNDLEVELRIGHT, oraCmd)
            ParametersHelper.AddParametersToCommand(AirTemp, ParameterDirection.Input, OracleType.VarChar, p_strAIRTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(TrackTemp, ParameterDirection.Input, OracleType.VarChar, p_strTRACKTEMP, oraCmd)
            ParametersHelper.AddParametersToCommand(SoundLevelLeftTempCor, ParameterDirection.Input, OracleType.VarChar, p_strSOUNDLEVELLEFT_TEMPCOR, oraCmd)
            ParametersHelper.AddParametersToCommand(SoundLevelRightTempCor, ParameterDirection.Input, OracleType.VarChar, p_strSOUNDLEVELRIGHT_TEMPCOR, oraCmd)
            ParametersHelper.AddParametersToCommand(SoundId, ParameterDirection.Input, OracleType.Number, p_intSoundID, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SoundDetailSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
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
    ''' Saves wetgrip table
    ''' </summary>
    ''' <param name="p_strUserId">User id</param>
    ''' <param name="p_intWetGripID">Wetgrip id</param>
    ''' <param name="p_strPROJECTNUMBER">Project number</param>
    ''' <param name="p_intTIRENUM">Tire number</param>
    ''' <param name="p_strTESTSPEC">Test spec</param>
    ''' <param name="p_dteDATEOFTEST">Date of test</param>
    ''' <param name="p_strTESTVEHICLE">Test vehicle</param>
    ''' <param name="p_strLOCATIONOFTESTTRACK">Location of test track</param>
    ''' <param name="p_strTESTTRACKCHARACTERISTICS">Test track characteristics</param>
    ''' <param name="p_strISSUEBY">Issue by</param>
    ''' <param name="p_strMETHODOFCERTIFICATION">Method of certification</param>
    ''' <param name="p_strTESTTIREDETAILS">Test tire details</param>
    ''' <param name="p_strTIRESIZEANDSERVICEDESC">Tire size and service description</param>
    ''' <param name="p_strTIREBRANDANDTRADEDESC">Tire brand and trade description</param>
    ''' <param name="p_strREFERENCEINFLATIONPRESSURE">Reference inflation pressure</param>
    ''' <param name="p_strTESTRIMWITHCODE">Test rim with code</param>
    ''' <param name="p_strTEMPMEASURESENSORTYPE">Temp measure sensor type</param>
    ''' <param name="p_strIDENTIFICATIONSRTT">Identification SRTT</param>
    ''' <param name="p_strTESTTIRELOAD_SRTT">Test tire load SRTT</param>
    ''' <param name="p_strTESTTIRELOAD_CANDIDATE">Test tire load candidate</param>
    ''' <param name="p_strTESTTIRELOAD_CONTROL">Test tire load control</param>
    ''' <param name="p_strWATERDEPTH_SRTT">Water depth SRTT</param>
    ''' <param name="p_strWATERDEPTH_CANDIDATE">Water depth candidate</param>
    ''' <param name="p_strWATERDEPTH_CONTROL">Water depth control</param>
    ''' <param name="p_strWETTEDTRACKTEMPAVG">Wetter track temp average</param>
    ''' <param name="p_intCERTIFICATIONTYPEID">Certification type id</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate number</param>
    ''' <param name="p_intSKUID">Sku id</param>
    ''' <param name="p_intCertificateNUmberID">Certificate number id</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strGTSpecWetGrip">GT spec wet grip</param>
    ''' <param name="p_strMFGWWYY">MfgWwyy</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Const UserId As String = "ps_UserID"
        Const WetGripId As String = "pi_WETGRIPID"
        Const ProjectNumber As String = "ps_PROJECTNUMBER"
        Const TireNumber As String = "pi_TIRENUMBER"
        Const TestSpec As String = "ps_TESTSPEC"
        Const DateOfTest As String = "pd_DATEOFTEST"
        Const TestVehicle As String = "ps_TESTVEHICLE"
        Const LocationOfTestTrack As String = "ps_LOCATIONOFTESTTRACK"
        Const TestTrackCharacteristics As String = "ps_TESTTRACKCHARACTERISTICS"
        Const IssueBy As String = "ps_ISSUEBY"
        Const MethodOfCertification As String = "ps_METHODOFCERTIFICATION"
        Const TestTireDetails As String = "ps_TESTTIREDETAILS"
        Const TireSizeAndServiceDesc As String = "ps_TIRESIZEANDSERVICEDESC"
        Const TireBrandAndTradeDesc As String = "ps_TIREBRANDANDTRADEDESC"
        Const ReferenceInflationPressure As String = "ps_REFERENCEINFLATIONPRESSURE"
        Const TestRimWithCode As String = "ps_TESTRIMWITHCODE"
        Const TempMeasureSensorType As String = "ps_TEMPMEASURESENSORTYPE"
        Const IdentificationSrtt As String = "ps_IDENTIFICATIONSRTT"
        Const TestTireLoadSrtt As String = "ps_TESTTIRELOAD_SRTT"
        Const TestTireLoadCandidate As String = "ps_TESTTIRELOAD_CANDIDATE"
        Const TestTireLoadcontrol As String = "ps_TESTTIRELOAD_CONTROL"
        Const WaterDepthSrtt As String = "ps_WATERDEPTH_SRTT"
        Const WaterDepthCandidate As String = "ps_WATERDEPTH_CANDIDATE"
        Const WaterDepthControl As String = "ps_WATERDEPTH_CONTROL"
        Const WettedTrackTempAvg As String = "ps_WETTEDTRACKTEMPAVG"
        Const CertificationTypeId As String = "pi_CERTIFICATIONTYPEID"
        Const SkuId As String = "pi_SKUID"
        Const CertificateId As String = "pi_CertificateID"
        Const Operation As String = "ps_Operation"
        Const GtSpec As String = "ps_GTSPEC"
        Const MfgWwyy As String = "ps_MFGWWYY"
        Const WetGripHDRSave As String = "testresults_crud.WetGripHDR_Save"
        Const OneNum As Short = 1
        Try
            ParametersHelper.AddParametersToCommand(UserId, ParameterDirection.Input, OracleType.VarChar, p_strUserId, oraCmd)
            ParametersHelper.AddParametersToCommand(WetGripId, ParameterDirection.Output, OracleType.Number, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ProjectNumber, ParameterDirection.Input, OracleType.VarChar, p_strPROJECTNUMBER, oraCmd)
            ParametersHelper.AddParametersToCommand(TireNumber, ParameterDirection.Input, OracleType.Number, p_intTIRENUM, oraCmd)
            ParametersHelper.AddParametersToCommand(TestSpec, ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEC, oraCmd)
            ParametersHelper.AddParametersToCommand(DateOfTest, ParameterDirection.Input, OracleType.DateTime, p_dteDATEOFTEST, oraCmd)
            ParametersHelper.AddParametersToCommand(TestVehicle, ParameterDirection.Input, OracleType.VarChar, p_strTESTVEHICLE, oraCmd)
            ParametersHelper.AddParametersToCommand(LocationOfTestTrack, ParameterDirection.Input, OracleType.VarChar, p_strLOCATIONOFTESTTRACK, oraCmd)
            ParametersHelper.AddParametersToCommand(TestTrackCharacteristics, ParameterDirection.Input, OracleType.VarChar, p_strTESTTRACKCHARACTERISTICS, oraCmd)
            ParametersHelper.AddParametersToCommand(IssueBy, ParameterDirection.Input, OracleType.VarChar, p_strISSUEBY, oraCmd)
            ParametersHelper.AddParametersToCommand(MethodOfCertification, ParameterDirection.Input, OracleType.VarChar, p_strMETHODOFCERTIFICATION, oraCmd)
            ParametersHelper.AddParametersToCommand(TestTireDetails, ParameterDirection.Input, OracleType.VarChar, p_strTESTTIREDETAILS, oraCmd)
            ParametersHelper.AddParametersToCommand(TireSizeAndServiceDesc, ParameterDirection.Input, OracleType.VarChar, p_strTIRESIZEANDSERVICEDESC, oraCmd)
            ParametersHelper.AddParametersToCommand(TireBrandAndTradeDesc, ParameterDirection.Input, OracleType.VarChar, p_strTIREBRANDANDTRADEDESC, oraCmd)
            ParametersHelper.AddParametersToCommand(ReferenceInflationPressure, ParameterDirection.Input, OracleType.VarChar, p_strREFERENCEINFLATIONPRESSURE, oraCmd)
            ParametersHelper.AddParametersToCommand(TestRimWithCode, ParameterDirection.Input, OracleType.VarChar, p_strTESTRIMWITHCODE, oraCmd)
            ParametersHelper.AddParametersToCommand(TempMeasureSensorType, ParameterDirection.Input, OracleType.VarChar, p_strTEMPMEASURESENSORTYPE, oraCmd)
            ParametersHelper.AddParametersToCommand(IdentificationSrtt, ParameterDirection.Input, OracleType.VarChar, p_strIDENTIFICATIONSRTT, oraCmd)
            ParametersHelper.AddParametersToCommand(TestTireLoadSrtt, ParameterDirection.Input, OracleType.VarChar, p_strTESTTIRELOAD_SRTT, oraCmd)
            ParametersHelper.AddParametersToCommand(TestTireLoadCandidate, ParameterDirection.Input, OracleType.VarChar, p_strTESTTIRELOAD_CANDIDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(TestTireLoadcontrol, ParameterDirection.Input, OracleType.VarChar, p_strTESTTIRELOAD_CONTROL, oraCmd)
            ParametersHelper.AddParametersToCommand(WaterDepthSrtt, ParameterDirection.Input, OracleType.VarChar, p_strWATERDEPTH_SRTT, oraCmd)
            ParametersHelper.AddParametersToCommand(WaterDepthCandidate, ParameterDirection.Input, OracleType.VarChar, p_strWATERDEPTH_CANDIDATE, oraCmd)
            ParametersHelper.AddParametersToCommand(WaterDepthControl, ParameterDirection.Input, OracleType.VarChar, p_strWATERDEPTH_CONTROL, oraCmd)
            ParametersHelper.AddParametersToCommand(WettedTrackTempAvg, ParameterDirection.Input, OracleType.VarChar, p_strWETTEDTRACKTEMPAVG, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCERTIFICATIONTYPEID, oraCmd)
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_intSKUID, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateNUmberID, oraCmd)
            ParametersHelper.AddParametersToCommand(Operation, ParameterDirection.Input, OracleType.VarChar, p_strOperation, oraCmd)
            ParametersHelper.AddParametersToCommand(GtSpec, ParameterDirection.Input, OracleType.VarChar, p_strGTSpecWetGrip, oraCmd)
            ParametersHelper.AddParametersToCommand(MfgWwyy, ParameterDirection.Input, OracleType.VarChar, p_strMFGWWYY, oraCmd)
            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = WetGripHDRSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
                enumSaveResult = NameAid.SaveResult.Sucess
            End If

            'Gets the Measure Id to be inserted on the detail table
            If oraCmd.Parameters.Item(WetGripId).Value.Equals(DBNull.Value) Then
                p_intWetGripID = UInteger.MinValue
            Else
                p_intWetGripID = CInt(oraCmd.Parameters.Item(WetGripId).Value)
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
    ''' Saves wetgrip detail table
    ''' </summary>
    ''' <param name="p_strUserId">User id</param>
    ''' <param name="p_intITERATION">Iteration</param>
    ''' <param name="p_strTESTSPEED">Test speed</param>
    ''' <param name="p_strDIRECTIONOFRUN">Direction of run</param>
    ''' <param name="p_strSRTT">SRTT</param>
    ''' <param name="p_strCANDIDATETIRE">Candidate tire</param>
    ''' <param name="p_strPEAKBREAKFORCECOEFICIENT">Peak break force coefficient</param>
    ''' <param name="p_strMEANFULLYDEVDECELERATION">Fully dev deceleration</param>
    ''' <param name="p_strWETGRIPINDEX">Wet grip index</param>
    ''' <param name="p_strCOMMENTS">Comments</param>
    ''' <param name="p_intWetGripID">Wet grip id</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        Const UserId As String = "ps_UserID"
        Const Iteration As String = "pi_ITERATION"
        Const TestSpeed As String = "ps_TESTSPEED"
        Const DirectionOfRun As String = "ps_DIRECTIONOFRUN"
        Const Srtt As String = "ps_SRTT"
        Const CandidateTire As String = "ps_CANDIDATETIRE"
        Const PeakBreakForceCoEficient As String = "ps_PEAKBREAKFORCECOEFICIENT"
        Const MeanfullyDevDeceleration As String = "ps_MEANFULLYDEVDECELERATION"
        Const WetGripIndex As String = "ps_WETGRIPINDEX"
        Const Comments As String = "ps_COMMENTS"
        Const WetGripId As String = "pi_WETGRIPID"
        Const WetGripDetailSave As String = "testresults_crud.WetGripDetail_Save"
        Const OneNum As Short = 1
        Try
            ParametersHelper.AddParametersToCommand(UserId, ParameterDirection.Input, OracleType.VarChar, p_strUserId, oraCmd)
            ParametersHelper.AddParametersToCommand(Iteration, ParameterDirection.Input, OracleType.Number, p_intITERATION, oraCmd)
            ParametersHelper.AddParametersToCommand(TestSpeed, ParameterDirection.Input, OracleType.VarChar, p_strTESTSPEED, oraCmd)
            ParametersHelper.AddParametersToCommand(DirectionOfRun, ParameterDirection.Input, OracleType.VarChar, p_strDIRECTIONOFRUN, oraCmd)
            ParametersHelper.AddParametersToCommand(Srtt, ParameterDirection.Input, OracleType.VarChar, p_strSRTT, oraCmd)
            ParametersHelper.AddParametersToCommand(CandidateTire, ParameterDirection.Input, OracleType.VarChar, p_strCANDIDATETIRE, oraCmd)
            ParametersHelper.AddParametersToCommand(PeakBreakForceCoEficient, ParameterDirection.Input, OracleType.VarChar, p_strPEAKBREAKFORCECOEFICIENT, oraCmd)
            ParametersHelper.AddParametersToCommand(MeanfullyDevDeceleration, ParameterDirection.Input, OracleType.VarChar, p_strMEANFULLYDEVDECELERATION, oraCmd)
            ParametersHelper.AddParametersToCommand(WetGripIndex, ParameterDirection.Input, OracleType.VarChar, p_strWETGRIPINDEX, oraCmd)
            ParametersHelper.AddParametersToCommand(Comments, ParameterDirection.Input, OracleType.VarChar, p_strCOMMENTS, oraCmd)
            ParametersHelper.AddParametersToCommand(WetGripId, ParameterDirection.Input, OracleType.Number, p_intWetGripID, oraCmd)
            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = WetGripDetailSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = OneNum) Then
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
    ''' Gets the test result data
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number.</param>
    ''' <param name="p_intSKUID">Sku id</param>
    ''' <param name="p_strCertificateNumber">Certificate number</param>
    ''' <param name="p_intCertificateNumberID">Certificate number id</param>
    ''' <param name="p_intCertificationTypeId">Certification type id</param>
    ''' <returns>Returns ICS data set</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetTestResultData(ByVal p_strMatlNum As String, _
                                      ByVal p_intSKUID As Integer, _
                                      ByVal p_strCertificateNumber As String, _
                                      ByVal p_intCertificateNumberID As Integer, _
                                      ByVal p_intCertificationTypeId As Integer) As ICSDataSet

        Dim dsResults As New ICSDataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const MeasureCursor As String = "PC_MEASURECURSOR"
        Const MeasureDetailCursor As String = "PC_MEASUREDETAILCURSOR"
        Const PlungerHdrCursor As String = "PC_PLUNGERHDRCURSOR"
        Const PlungerDtlCursor As String = "PC_PLUNGERDTLCURSOR"
        Const TreadwearhdrCursor As String = "PC_TREADWEARHDRCURSOR"
        Const TreadwearDtlCursor As String = "PC_TREADWEARDTLCURSOR"
        Const BeadUnseatHdrCursor As String = "PC_BEADUNSEATHDRCURSOR"
        Const BeadUnseatDtlCursor As String = "PC_BEADUNSEATDTLCURSOR"
        Const EnduranceHdrCursor As String = "PC_ENDURANCEHDRCURSOR"
        Const EnduranceDtlCursor As String = "PC_ENDURANCEDTLCURSOR"
        Const HighSpeedCursor As String = "PC_HIGHSPEEDCURSOR"
        Const HighSpeedDetailCursor As String = "PC_HIGHSPEEDDETAILCURSOR"
        Const HsSpeedTestDetail As String = "pc_HSSpeedTestDetail"
        Const SoundHdrCursor As String = "PC_SOUNDHdrCURSOR"
        Const SoundDetailCursor As String = "PC_SOUNDDETAILCURSOR"
        Const WetGripHdrCursor As String = "PC_WETGRIPHDRCURSOR"
        Const WetGripDetailcursor As String = "PC_WETGRIPDETAILCURSOR"
        Const CertificationTypeId As String = "PI_CERTIFICATIONTYPEID"
        Const SkuId As String = "PI_SKUID"
        Const CertificateNumber As String = "ps_CertificateNumber"
        Const CertificateNumberId As String = "pi_CertificateNumberID"
        Const GetTestResults As String = "testresults_crud.GetTestresults"
        Const TableStr As String = "Table"
        Const MeasureHdr As String = "MeasureHdr"
        Const Table1 As String = "Table1"
        Const MeasureDtl As String = "MeasureDtl"
        Const Table2 As String = "Table2"
        Const PlungerHdr As String = "PlungerHdr"
        Const Table3 As String = "Table3"
        Const PlungerDtl As String = "PlungerDtl"
        Const Table4 As String = "Table4"
        Const TreadWearHdr As String = "TreadWearHdr"
        Const Table5 As String = "Table5"
        Const TreadWearDtl As String = "TreadWearDtl"
        Const Table6 As String = "Table6"
        Const BeadUnseatHdr As String = "BeadUnseatHdr"
        Const Table7 As String = "Table7"
        Const BeadUnseatDtl As String = "BeadUnseatDtl"
        Const Table8 As String = "Table8"
        Const EnduranceHdr As String = "EnduranceHdr"
        Const Table9 As String = "Table9"
        Const EnduranceDtl As String = "EnduranceDtl"
        Const Table10 As String = "Table10"
        Const HighSpeedHdr As String = "HighSpeedHdr"
        Const Table11 As String = "Table11"
        Const HighSpeedDtl As String = "HighSpeedDtl"
        Const Table12 As String = "Table12"
        Const SpeedTestDetail As String = "SpeedTestDetail"
        Const Table13 As String = "Table13"
        Const SoundHdr As String = "SoundHdr"
        Const Table14 As String = "Table14"
        Const SoundDetail As String = "SoundDetail"
        Const Table15 As String = "Table15"
        Const WetGripHdr As String = "WetGripHdr"
        Const Table16 As String = "Table16"
        Const WetGripDetail As String = "WetGripDetail"
        Try
            ParametersHelper.AddParametersToCommand(MeasureCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(MeasureDetailCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PlungerHdrCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(PlungerDtlCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(TreadwearhdrCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(TreadwearDtlCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(BeadUnseatHdrCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(BeadUnseatDtlCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(EnduranceHdrCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(EnduranceDtlCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(HighSpeedCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(HighSpeedDetailCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(HsSpeedTestDetail, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(SoundHdrCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(SoundDetailCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(WetGripHdrCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(WetGripDetailcursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_intSKUID, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateNumberId, ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetTestResults
            Connect()
            oraCmd.Connection = Connection
            ' Get the data
            oraAdp.SelectCommand = oraCmd
            dsResults.EnforceConstraints = False
            oraAdp.TableMappings.Add(TableStr, MeasureHdr)
            oraAdp.TableMappings.Add(Table1, MeasureDtl)
            oraAdp.TableMappings.Add(Table2, PlungerHdr)
            oraAdp.TableMappings.Add(Table3, PlungerDtl)
            oraAdp.TableMappings.Add(Table4, TreadWearHdr)
            oraAdp.TableMappings.Add(Table5, TreadWearDtl)
            oraAdp.TableMappings.Add(Table6, BeadUnseatHdr)
            oraAdp.TableMappings.Add(Table7, BeadUnseatDtl)
            oraAdp.TableMappings.Add(Table8, EnduranceHdr)
            oraAdp.TableMappings.Add(Table9, EnduranceDtl)
            oraAdp.TableMappings.Add(Table10, HighSpeedHdr)
            oraAdp.TableMappings.Add(Table11, HighSpeedDtl)
            oraAdp.TableMappings.Add(Table12, SpeedTestDetail)
            oraAdp.TableMappings.Add(Table13, SoundHdr)
            oraAdp.TableMappings.Add(Table14, SoundDetail)
            oraAdp.TableMappings.Add(Table15, WetGripHdr)
            oraAdp.TableMappings.Add(Table16, WetGripDetail)

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
    ''' Gets the entire audit log
    ''' </summary>
    ''' <returns>Returns audit log data as data set</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetAuditLog() As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetAuditLogData As String = "certification_crud.GET_AUDITLOG"
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetAuditLogData
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
         
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
    ''' Gets the approval reasons based on certification type id
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification type id</param>
    ''' <returns>Returns approval reasons as data set.</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetApprovalReasons(ByVal p_intCertificationTypeId As Integer) As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetApprovalReason As String = "certification_crud.GET_APPROVALREASONS"
        Const CertificationTypeId As String = "pi_CertificationTypeId"
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetApprovalReason
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

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
    ''' Updates single audit log entry
    ''' </summary>
    ''' <param name="p_intChangeLogID">Change log id</param>
    ''' <param name="p_dtemChangeDateTime">Change date time</param>
    ''' <param name="p_strApprovalStatus">Approval status</param>
    ''' <param name="p_strApprover">Approver</param>
    ''' <returns>Returns update status as boolean value</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function UpdateAuditLogEntry(ByVal p_intChangeLogID As Integer, ByVal p_dtemChangeDateTime As DateTime, _
                                        ByVal p_strApprovalStatus As String, ByVal p_strApprover As String) As Boolean

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const UpdateApprovalStatus As String = "certification_crud.AUDITLOG_UpdateApprovalStatus"
        Const ChangeLogId As String = "pi_ChangeLogId"
        Const ChangeDateTime As String = "pd_ChangeDateTime"
        Const Status As String = "ps_Status"
        Const Approver As String = "ps_Approver"

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = UpdateApprovalStatus
            ParametersHelper.AddParametersToCommand(ChangeLogId, ParameterDirection.Input, OracleType.Number, p_intChangeLogID, oraCmd)
            ParametersHelper.AddParametersToCommand(ChangeDateTime, ParameterDirection.Input, OracleType.DateTime, p_dtemChangeDateTime, oraCmd)
            ParametersHelper.AddParametersToCommand(Status, ParameterDirection.Input, OracleType.VarChar, p_strApprovalStatus, oraCmd)
            ParametersHelper.AddParametersToCommand(Approver, ParameterDirection.Input, OracleType.VarChar, p_strApprover, oraCmd)

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
    ''' Gets audit log after given date
    ''' </summary>
    ''' <param name="p_dtemChangeDateTime">Change date and time</param>
    ''' <returns>Returns audit log as dataset</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetAuditLogAfterDate(ByVal p_dtemChangeDateTime As DateTime) As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const GetAuditLogAftDate As String = "certification_crud.GET_AuditLogAfterDate"
        Const ChangeDateTime As String = "pd_ChangeDateTime"
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetAuditLogAftDate
            ParametersHelper.AddParametersToCommand(ChangeDateTime, ParameterDirection.Input, OracleType.DateTime, p_dtemChangeDateTime, oraCmd)
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

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
    ''' Checks if material number exists
    ''' </summary>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <returns>Returns boolean value based on the check result</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function CheckIfMatlNumExists(ByVal p_strMatlNum As String) As Boolean

        Dim oraCmd As New OracleCommand
        Dim strMatlNumExists As String
        Const CheckIfSkuExists As String = "ics_common_functions.CheckIfSKUExists"
        Const MatlExist As String = "ps_MatlExist"

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CheckIfSkuExists
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)

            Dim oraMatlNumExist As New OracleParameter()
            oraMatlNumExist.ParameterName = MatlExist
            oraMatlNumExist.OracleType = OracleType.VarChar
            oraMatlNumExist.Direction = ParameterDirection.Output
            oraMatlNumExist.Size = 1

            oraCmd.Parameters.Add(oraMatlNumExist)

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            'Gets the Material number exists
            strMatlNumExists = CStr(oraCmd.Parameters.Item(MatlExist).Value)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return CBool(IIf(strMatlNumExists.ToLower().Equals("y"), True, False))

    End Function
    ''' <summary>
    ''' Gets certificate id
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Certificate number</param>
    ''' <param name="p_intCertificateTypeId">Certificate type id</param>
    ''' <param name="p_strExtensionNo">Extension number</param>
    ''' <returns>Returns Certificate id as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCertificateID(ByVal p_strCertificateNumber As String, ByVal p_intCertificateTypeId As Integer, ByVal p_strExtensionNo As String) As Integer


        Dim oraCmd As New OracleCommand
        Dim p_intCertificateId As Int32
        Const GetCertificateIdByNumber As String = "ICS_COMMON_FUNCTIONS.GetCertificateIDByNumber"
        Const CertificateNumber As String = "ps_CertificateNumber"
        Const CertificationTypeId As String = "pi_CertificationTypeID"
        Const ExtensionNo As String = "ps_ExtensionNo"
        Const CertificateId As String = "pi_CertificateID"

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCertificateIdByNumber
            ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Input, OracleType.VarChar, 20, p_strCertificateNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificateTypeId, oraCmd)
            ParametersHelper.AddParametersToCommand(ExtensionNo, ParameterDirection.Input, OracleType.VarChar, p_strExtensionNo, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Output, OracleType.Number, p_intCertificateId, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            'Gets the certificate number exists value
            p_intCertificateId = Convert.ToInt32(oraCmd.Parameters.Item(certificateId).Value)

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
    ''' <summary>
    ''' Gets the approved substitution value
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification type id</param>
    ''' <param name="p_strField">Field value</param>
    ''' <param name="p_sngValue">Single value</param>
    ''' <param name="p_intSKUID">Sku id</param>
    ''' <returns>Returns approved substitution value as single</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetApprovedSubstitution(ByVal p_intCertificationTypeId As Integer, ByVal p_strField As String, ByVal p_sngValue As Single, ByVal p_intSKUID As Integer) As Single

        Dim oraCmd As New OracleCommand
        Dim sngNewValue As Single
        Const GetApprovedSub As String = "CERTIFICATION_CRUD.GetApprovedSubstitution"
        Const CertificationTypeId As String = "pi_CertificationTypeID"
        Const FieldStr As String = "ps_Field"
        Const ValueStr As String = "pi_Value"
        Const SkuId As String = "pi_SkuId"
        Const NewValueStr As String = "pi_NewValue"
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetApprovedSub
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            ParametersHelper.AddParametersToCommand(FieldStr, ParameterDirection.Input, OracleType.VarChar, 50, p_strField, oraCmd)
            ParametersHelper.AddParametersToCommand(ValueStr, ParameterDirection.Input, OracleType.Number, p_sngValue, oraCmd)
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_intSKUID, oraCmd)
            ParametersHelper.AddParametersToCommand(NewValueStr, ParameterDirection.Output, OracleType.Number, sngNewValue, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            'Gets the new value
            sngNewValue = Convert.ToSingle(oraCmd.Parameters.Item(NewValueStr).Value)

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
    ''' Checks if the give certificate number exists
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Certificate number</param>
    ''' <returns>Returns boolean value based on certificate number search result</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function CheckIfCertificateNumberExists(ByVal p_strCertificateNumber As String) As Boolean

        Dim oraCmd As New OracleCommand
        Dim strCertificateNumberExists As String
        Const CheckIfCertificateNumberExist As String = "ics_common_functions.CheckIfCertificateNumberExists"
        Const CertificateNumber As String = "ps_CertificateNumber"
        Const CertificateNumberExists As String = "ps_CertificateNumberExists"
        Const NumOne As Short = 1

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CheckIfCertificateNumberExist
            ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)

            Dim oraMatlNumExist As New OracleParameter()
            oraMatlNumExist.ParameterName = CertificateNumberExists
            oraMatlNumExist.OracleType = OracleType.VarChar
            oraMatlNumExist.Direction = ParameterDirection.Output
            oraMatlNumExist.Size = NumOne

            oraCmd.Parameters.Add(oraMatlNumExist)

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            'Gets the certificate number exists value
            strCertificateNumberExists = CStr(oraCmd.Parameters.Item(CertificateNumberExists).Value)

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return CBool(IIf(strCertificateNumberExists.ToLower().Equals("y"), True, False))

    End Function
    ''' <summary>
    ''' Saves new certificate
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Certificate number</param>
    ''' <param name="p_intCertificateTypeId">Certificate Type id</param>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strCustomer">Customer</param>
    ''' <param name="p_strOperatorName">Operator name</param>
    ''' <param name="p_strCertificateExtension">Certificate extension</param>
    ''' <param name="p_InsertPC">Inserting pc name</param>
    ''' <param name="p_ErrorDesc">Error description</param>
    ''' <returns>Returns save result as integer value</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        Const CertificationTypeId As String = "pi_CertificationTypeId"
        Const CertificateNumber As String = "ps_CertificateNumber"
        Const ImporterId As String = "pi_importerid"
        Const CustomerId As String = "pi_customerid"
        Const OperatorName As String = "ps_OperatorName"
        Const ExtensionEn As String = "ps_Extension_En"
        Const InsertPc As String = "ps_InsertPC"
        Const ErrorNum As String = "pn_Error_Num"
        Const ErrorMsg As String = "ps_ErrorMsg"
        Const CertificateBasicInfoSave As String = "CERTIFICATION_CRUD.CertificateBasicInfo_Save"
        Const ZeroNum As Short = 0
        Try

            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificateTypeId, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            Dim strImporter As String
            If p_strImporter = String.Empty Then
                strImporter = CStr(ZeroNum)
            Else
                strImporter = p_strImporter
            End If
            ParametersHelper.AddParametersToCommand(ImporterId, ParameterDirection.Input, OracleType.Number, CInt(strImporter), oraCmd)
            Dim strCustomer As String
            If p_strCustomer = String.Empty Then
                strCustomer = CStr(ZeroNum)
            Else
                strCustomer = p_strCustomer
            End If
            ParametersHelper.AddParametersToCommand(CustomerId, ParameterDirection.Input, OracleType.Number, CInt(strCustomer), oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            ParametersHelper.AddParametersToCommand(ExtensionEn, ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strCertificateExtension), oraCmd)
            ParametersHelper.AddParametersToCommand(InsertPc, ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_InsertPC), oraCmd)
            ParametersHelper.AddParametersToCommand(ErrorNum, ParameterDirection.Output, OracleType.Number, 20, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ErrorMsg, ParameterDirection.Output, OracleType.VarChar, 4000, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CertificateBasicInfoSave

            Connect()
            oraCmd.Connection = Connection

            oraCmd.ExecuteReader()
            resNum = CInt(oraCmd.Parameters.Item(ErrorNum).Value)

            If oraCmd.Parameters.Item(ErrorMsg).Value IsNot DBNull.Value Then
                p_ErrorDesc = CStr(oraCmd.Parameters.Item(ErrorMsg).Value)
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
    ''' <summary>
    ''' Saves new certificate
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Certificate number</param>
    ''' <param name="p_intCertificateTypeId">Certificate Type id</param>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strCustomer">Customer</param>
    ''' <param name="p_strOperatorName">Operator name</param>
    ''' <param name="p_strCertificateExtension">Certificate extension</param>
    ''' <returns>Returns save result as boolean value</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function SaveNewCertificate(ByVal p_strCertificateNumber As String, _
                                       ByVal p_intCertificateTypeId As Integer, _
                                       ByVal p_strMatlNum As String, _
                                       ByVal p_strImporter As String, _
                                       ByVal p_strCustomer As String, _
                                       ByVal p_strOperatorName As String, _
                                       ByVal p_strCertificateExtension As String) As Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand

        Const CertificationTypeId As String = "pi_CertificationTypeId"
        Const CertificateNumber As String = "ps_CertificateNumber"
        Const ImporterId As String = "pi_importerid"
        Const CustomerId As String = "pi_customerid"
        Const OperatorName As String = "ps_OperatorName"
        Const ExtensionEn As String = "ps_Extension_En"
        Const InsertPc As String = "ps_InsertPC"
        Const ErrorNum As String = "pn_Error_Num"
        Const ErrorMsg As String = "ps_ErrorMsg"
        Const CertificateBasicInfoSave As String = "CERTIFICATION_CRUD.CertificateBasicInfo_Save"
        Const ZeroNum As Short = 0
        Const OneNum As Short = 1

        Try
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificateTypeId, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            Dim strImporter As String
            If p_strImporter = String.Empty Then
                strImporter = CStr(ZeroNum)
            Else
                strImporter = p_strImporter
            End If
            ParametersHelper.AddParametersToCommand(ImporterId, ParameterDirection.Input, OracleType.Number, CInt(strImporter), oraCmd)
            Dim strCustomer As String
            If p_strCustomer = String.Empty Then
                strCustomer = CStr(ZeroNum)
            Else
                strCustomer = p_strCustomer
            End If
            ParametersHelper.AddParametersToCommand(CustomerId, ParameterDirection.Input, OracleType.Number, CInt(strCustomer), oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)
            ParametersHelper.AddParametersToCommand(ExtensionEn, ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strCertificateExtension), oraCmd)
            ParametersHelper.AddParametersToCommand(InsertPc, ParameterDirection.Input, OracleType.VarChar, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ErrorNum, ParameterDirection.Output, OracleType.Number, 20, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ErrorMsg, ParameterDirection.Output, OracleType.VarChar, 4000, Nothing, oraCmd)
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CertificateBasicInfoSave

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnSaved = (rowsaffected = OneNum)

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
    ''' Archives the certificates
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Certificate number</param>
    ''' <param name="p_strOperatorName">Operator name</param>
    ''' <returns>Returns archive result as boolean value</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function ArchiveCertification(ByVal p_strCertificateNumber As String, _
                                         ByVal p_strOperatorName As String) As Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand
        Const CertificateNumber As String = "ps_CertificateNumber"
        Const OperatorName As String = "ps_OperatorName"
        Const CertificateArchive As String = "CERTIFICATION_CRUD.Certificate_Archive"
        Const OneNum As Short = 1
        Try
            ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CertificateArchive
            Connect()
            oraCmd.Connection = Connection
            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnSaved = (rowsaffected = OneNum)

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
    ''' Gets certificate extension based on given imark certificate id
    ''' </summary>
    ''' <param name="p_intImarkCertId">Imark certificate Id</param>
    ''' <returns>Returns certificate extension string</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCertifExtension(ByVal p_intImarkCertId As Integer) As String

        Dim strImarkCertExtension As String = String.Empty
        Dim oraCmd As New OracleCommand
        Const CertificateId As String = "pi_certificateId"
        Const ExtensionNumber As String = "ps_extensionNumber"
        Const GetCertExtension As String = "CERTIFICATION_CRUD.GetCertifExtension"
        Try
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intImarkCertId, oraCmd)
            ParametersHelper.AddParametersToCommand(ExtensionNumber, ParameterDirection.Output, OracleType.VarChar, 0, oraCmd)

            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCertExtension

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            If oraCmd.Parameters.Item(ExtensionNumber).Value IsNot DBNull.Value Then
                strImarkCertExtension = CStr(oraCmd.Parameters.Item(ExtensionNumber).Value)
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
    ''' <summary>
    ''' Gets latest imark certificate id
    ''' </summary>
    ''' <returns>Returns latest imark certificate id as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetLatestImarkCertifId() As Integer

        Dim intImarkCertId As Integer
        Dim oraCmd As New OracleCommand
        Const CertificateId As String = "pi_certificateId"
        Const GetLatestImarkCertId As String = "CERTIFICATION_CRUD.GetLatestImarkCertifId"
        Try
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Output, OracleType.Number, 0, oraCmd)
            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetLatestImarkCertId

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            If oraCmd.Parameters.Item(CertificateId).Value IsNot Nothing Then
                intImarkCertId = CInt(oraCmd.Parameters.Item(CertificateId).Value)
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
    ''' <summary>
    ''' Gets latest GSO certificate number
    ''' </summary>
    ''' <returns>Returns latest GSO certificate number as string</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetLatestGSOCertifNumber() As String

        Dim strGSOTempCertNumber As String = String.Empty
        Dim oraCmd As New OracleCommand
        Const CertificateNumber As String = "ps_certificateNumber"
        Const GetLatestGsoCertNumber As String = "CERTIFICATION_CRUD.getlatestgsocertifnumber"
        Try
            ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Output, OracleType.VarChar, 8000, Nothing, oraCmd)
            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetLatestGsoCertNumber

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            If oraCmd.Parameters.Item(CertificateNumber).Value IsNot DBNull.Value Then
                strGSOTempCertNumber = CStr(oraCmd.Parameters.Item(CertificateNumber).Value)
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
    ''' <summary>
    ''' Gets certification type id based on given certification type name
    ''' </summary>
    ''' <param name="p_strCertificationTypeName">Certification type name</param>
    ''' <returns>Returns certification type id as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCertificationTypeID(ByVal p_strCertificationTypeName As String) As Integer

        'Returns the certification type id for a certification type name
        Dim oraCmd As New OracleCommand
        Dim p_intCertificationTypeId As Int32
        Const GetCertificationId As String = "ICS_COMMON_FUNCTIONS.GetCertificationID"
        Const CertificationTypeName As String = "ps_CertificationTypeName"
        Const Retvalue As String = "retvalue"
        Const FiftyNum As Short = 50
        Const ZeroNum As Short = 0
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCertificationId
            ParametersHelper.AddParametersToCommand(CertificationTypeName, ParameterDirection.Input, OracleType.VarChar, FiftyNum, p_strCertificationTypeName, oraCmd)
            ParametersHelper.AddParametersToCommand(Retvalue, ParameterDirection.ReturnValue, OracleType.Int16, ZeroNum, oraCmd)
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
    ''' <summary>
    ''' Adds customer information
    ''' </summary>
    ''' <param name="p_intSKUId">Sku id</param>
    ''' <param name="p_strCustomer">Customer name</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strImporterRepresentative">Importer representative</param>
    ''' <param name="p_strImporterAddress">Importer address</param>
    ''' <param name="p_strCountryLocation">Country location</param>
    ''' <returns>Returns save result as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function AddCustomer(ByVal p_intSKUId As Integer, _
                                ByVal p_strCustomer As String, _
                                ByVal p_strImporter As String, _
                                ByVal p_strImporterRepresentative As String, _
                                ByVal p_strImporterAddress As String, _
                                ByVal p_strCountryLocation As String) As NameAid.SaveResult
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim oraCmd As New OracleCommand
        Const SkuId As String = "pi_skuid"
        Const CustomerStr As String = "ps_CUSTOMER"
        Const Importer As String = "ps_IMPORTER"
        Const ImporterRepresentative As String = "ps_IMPORTERREPRESENTATIVE"
        Const ImporterAddress As String = "ps_IMPORTERADDRESS"
        Const CountryLocation As String = "ps_COUNTRYLOCATION"
        Const AddCustomerInfo As String = "CERTIFICATION_CRUD.AddCustomer"
        Const NumOne As Short = 1
        Try
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_intSKUId, oraCmd)
            ParametersHelper.AddParametersToCommand(CustomerStr, ParameterDirection.Input, OracleType.VarChar, p_strCustomer, oraCmd)
            ParametersHelper.AddParametersToCommand(Importer, ParameterDirection.Input, OracleType.VarChar, p_strImporter, oraCmd)
            ParametersHelper.AddParametersToCommand(ImporterRepresentative, ParameterDirection.Input, OracleType.VarChar, p_strImporterRepresentative, oraCmd)
            ParametersHelper.AddParametersToCommand(ImporterAddress, ParameterDirection.Input, OracleType.VarChar, p_strImporterAddress, oraCmd)
            ParametersHelper.AddParametersToCommand(CountryLocation, ParameterDirection.Input, OracleType.VarChar, p_strCountryLocation, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = AddCustomerInfo

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If (rowsaffected = NumOne) Then
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
    ''' <summary>
    ''' Gets passed input value and checks if empty
    ''' </summary>
    ''' <param name="inputParameterValue">Input value</param>
    ''' <returns>Returns passed input value if not empty or else returns null</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetInputValue(ByVal inputParameterValue As String) As Object
        ' Added as per project 2706 technical specification
        Try
            If Not String.IsNullOrEmpty(inputParameterValue) Then
                Return inputParameterValue
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
        End Try
        Return DBNull.Value

    End Function

    ''' <summary>
    ''' Gets the certified materials count
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification type id</param>
    ''' <param name="p_strCertificateNumber">Certificate number</param>
    ''' <param name="p_strCertificateExtension">Certificate extension</param>
    ''' <returns>Returns certified material count as integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCertifiedMaterialCount(ByVal p_intCertificationTypeId As Integer, _
                                              ByVal p_strCertificateNumber As String, _
                                              ByVal p_strCertificateExtension As String) As Integer

        Dim intCertifiedMaterialsCount As Integer
        Dim oraCmd As New OracleCommand
        Const CertificationTypeId As String = "pn_CertificationTypeId"
        Const CertificateNumber As String = "ps_CertificateNumber"
        Const ExtensionEn As String = "ps_Extension_EN"
        Const MatlCnt As String = "pn_Matl_Cnt"
        Const GetCertificateMatlCount As String = "ICS_MAINTENANCE.GetCertificateMatlCount"
        Const ZeroNum As Short = 0
        Try
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(ExtensionEn, ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strCertificateExtension), oraCmd)
            ParametersHelper.AddParametersToCommand(MatlCnt, ParameterDirection.Output, OracleType.Number, ZeroNum, oraCmd)
            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCertificateMatlCount

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()
            If oraCmd.Parameters.Item(MatlCnt).Value IsNot DBNull.Value Then
                intCertifiedMaterialsCount = CInt(oraCmd.Parameters.Item(MatlCnt).Value)
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
    ''' <param name="p_intCertificationTypeId">Certification type id</param>
    ''' <param name="p_strOldCertificateNumber">Old certificate number</param>
    ''' <param name="p_strOldCertificateExtension">Old certificate extension</param>
    ''' <param name="p_strNewCertificateNumber">New certificate number</param>
    ''' <param name="p_strNewCertificateExtension">New certificate extension</param>
    ''' <param name="p_strOperatorName">Operator name</param>
    ''' <returns>Returns boolean value based on renaming result</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function RenameCertificate(ByVal p_intCertificationTypeId As Integer, _
                                      ByVal p_strOldCertificateNumber As String, _
                                      ByVal p_strOldCertificateExtension As String, _
                                      ByVal p_strNewCertificateNumber As String, _
                                      ByVal p_strNewCertificateExtension As String, _
                                      ByVal p_strOperatorName As String) As Boolean

        Dim blnRenamed As Boolean = False
        Dim oraCmd As New OracleCommand
        Const CertificationTypeId As String = "pn_CertificationTypeId"
        Const OldCertificateNumber As String = "ps_OldCertificateNumber"
        Const OldExtensionEn As String = "ps_OldExtension_EN"
        Const NewCertificateNumber As String = "ps_NewCertificateNumber"
        Const NewExtensionEn As String = "ps_NewExtension_EN"
        Const OperatorName As String = "ps_OperatorName"
        Const RenameCert As String = "ICS_MAINTENANCE.RenameCertificate"
        Const OneNum As Short = 1
        Try
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)

            ' ps_OldCertificateNumber IN VARCHAR2          
            ParametersHelper.AddParametersToCommand(OldCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strOldCertificateNumber, oraCmd)

            ' ps_OldExtension_EN IN VARCHAR2          
            ParametersHelper.AddParametersToCommand(OldExtensionEn, ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strOldCertificateExtension), oraCmd)

            ' ps_NewCertificateNumber IN VARCHAR2           
            ParametersHelper.AddParametersToCommand(NewCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strNewCertificateNumber, oraCmd)

            ' ps_NewExtension_EN IN VARCHAR2           
            ParametersHelper.AddParametersToCommand(NewExtensionEn, ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strNewCertificateExtension), oraCmd)

            ' ps_OperatorName IN VARCHAR2
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = RenameCert

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnRenamed = (rowsaffected = OneNum)

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
    ''' <param name="p_strOperatorName">Operator name</param>
    ''' <returns>Returns boolean value based on deleting result</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function DeleteCertificate(ByVal p_intCertificationTypeId As Integer, _
                                      ByVal p_strCertificateNumber As String, _
                                      ByVal p_strCertificateExtension As String, _
                                      ByVal p_strOperatorName As String) As Boolean

        Dim blnDeleted As Boolean = False
        Dim oraCmd As New OracleCommand
        Const CertificationTypeId As String = "pn_CertificationTypeId"
        Const CertificateNumber As String = "ps_CertificateNumber"
        Const ExtensionEn As String = "ps_Extension_EN"
        Const OperatorName As String = "ps_OperatorName"
        Const DelCertificate As String = "ICS_MAINTENANCE.DeleteCertificate"
        Const OneNum As Short = 1
        Try
            ' pn_CertificationTypeId IN NUMBER          
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)

            ' ps_CertificateNumber IN VARCHAR2           
            ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)

            ' ps_Extension_EN IN VARCHAR2          
            ParametersHelper.AddParametersToCommand(ExtensionEn, ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strCertificateExtension), oraCmd)

            ' ps_OperatorName IN VARCHAR2
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = DelCertificate

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnDeleted = (rowsaffected = OneNum)

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
    ''' <param name="p_strCertificateNumber">Certificate number</param>
    ''' <param name="p_strCertificateExtension">Certificate extension</param>
    ''' <returns>Returns certificate materials as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetCertificateMaterials(ByVal p_intCertificationTypeId As Integer, _
                                            ByVal p_strCertificateNumber As String, _
                                            ByVal p_strCertificateExtension As String) As DataTable

        Dim dtCertificateMaterials As New DataTable
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const CertificationTypeId As String = "pn_CertificationTypeId"
        Const CertificateNumber As String = "ps_CertificateNumber"
        Const ExtensionEn As String = "ps_Extension_EN"
        Const CursorStr As String = "pc_Cursor"
        Const GetCertificateMatls As String = "ICS_MAINTENANCE.GetCertificateMatls"

        Try
            ' pn_CertificationTypeId IN NUMBER           
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)

            ' ps_CertificateNumber IN VARCHAR2          
            ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertificateNumber, oraCmd)

            ' ps_Extension_EN IN VARCHAR2           
            ParametersHelper.AddParametersToCommand(ExtensionEn, ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strCertificateExtension), oraCmd)

            ' pc_Cursor OUT SYS_REFCURSOR
            ParametersHelper.AddParametersToCommand(CursorStr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetCertificateMatls

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
    ''' <returns>Returns boolean value based on detach result</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function DetachCertificate(ByVal p_intSkuId As Integer, _
                                      ByVal p_intCertificateId As Integer) As Boolean

        Dim blnDetached As Boolean = False
        Dim oraCmd As New OracleCommand
        Const SkuId As String = "pn_SkuId"
        Const CertificateId As String = "pn_CertificateId"
        Const DetachCert As String = "ICS_MAINTENANCE.DetachCertificate"
        Const OneNum As Short = 1

        Try
            ' pn_SkuId IN NUMBER           
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_intSkuId, oraCmd)

            ' pn_CertificateId IN NUMBER           
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateId, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = DetachCert

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnDetached = (rowsaffected = OneNum)

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
    ''' <param name="p_intSkuId">Sku Id</param>
    ''' <param name="p_intCertificateId">Certificate Id</param>
    ''' <param name="p_strOperatorName">Operator name</param>
    ''' <returns>Returns boolean value based on the move result</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function MoveCertificate(ByVal p_intCertificationTypeId As Integer, _
                                    ByVal p_strNewCertificateNumber As String, _
                                    ByVal p_strNewCertificateExtension As String, _
                                    ByVal p_intSkuId As Integer, _
                                    ByVal p_intCertificateId As Integer, _
                                    ByVal p_strOperatorName As String) As Boolean

        Dim blnMoved As Boolean = False
        Dim oraCmd As New OracleCommand
        Const CertificationTypeId As String = "pn_CertificationTypeId"
        Const NewCertificateNumber As String = "ps_NewCertificateNumber"
        Const NewExtensionEn As String = "ps_NewExtension_EN"
        Const SkuId As String = "pn_SkuId"
        Const CertificateId As String = "pn_CertificateId"
        Const OperatorName As String = "ps_OperatorName"
        Const MoveCert As String = "ICS_MAINTENANCE.MoveCertificate"
        Const OneNum As Short = 1

        Try
            ' pn_CertificationTypeId IN NUMBER           
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_intCertificationTypeId, oraCmd)

            ' ps_CertificateNumber IN VARCHAR2           
            ParametersHelper.AddParametersToCommand(NewCertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strNewCertificateNumber, oraCmd)

            ' ps_Extension_EN IN VARCHAR2           
            ParametersHelper.AddParametersToCommand(NewExtensionEn, ParameterDirection.Input, OracleType.VarChar, GetInputValue(p_strNewCertificateExtension), oraCmd)

            ' pn_SkuId IN NUMBER           
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_intSkuId, oraCmd)

            ' pn_CertificateId IN NUMBER           
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateId, oraCmd)

            ' ps_OperatorName IN VARCHAR2
            ParametersHelper.AddParametersToCommand(OperatorName, ParameterDirection.Input, OracleType.VarChar, p_strOperatorName, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = MoveCert

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            blnMoved = (rowsaffected = OneNum)

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
    ''' <returns>Returns duplicate certificates as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetDuplicateCertificates(ByVal p_strMaterialNumber As String, _
                                             ByVal p_strSpeedRating As String) As DataTable

        Dim dtResults As New DataTable
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const SpeedRating As String = "ps_SpeedRating"
        Const ResultStr As String = "ps_Result"
        Const GetDuplicateCert As String = "ICS_MAINTENANCE.GetDuplicateCert"
        Try
            'Ps_Matl_Num IN VARCHAR2
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMaterialNumber, oraCmd)

            'Ps_SpeedRating IN VARCHAR2
            ParametersHelper.AddParametersToCommand(SpeedRating, ParameterDirection.Input, OracleType.VarChar, p_strSpeedRating, oraCmd)

            'Ps_Result OUT RETCURSOR
            ParametersHelper.AddParametersToCommand(ResultStr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetDuplicateCert

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
    ''' Deletes duplicate certificates
    ''' </summary>
    ''' <param name="p_intSkuId">Sku id</param>
    ''' <returns>Returns boolean value based on deletion result</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function DeleteDuplicateCertificates(ByVal p_intSkuId As Integer) As Boolean

        Dim blnDeleted As Boolean = False
        Dim oraCmd As New OracleCommand
        Const SkuId As String = "pn_SkuId"
        Const RemoveDuplicateCert As String = "ICS_MAINTENANCE.RemoveDuplicateCert"
        Const ZeroNum As Short = 0
        Try
            ' pn_SkuId IN NUMBER           
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_intSkuId, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = RemoveDuplicateCert

            Connect()
            oraCmd.Connection = Connection

            Dim intRowsAffected As Integer = oraCmd.ExecuteNonQuery()
            blnDeleted = CBool(IIf(intRowsAffected > ZeroNum, True, False))

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
    ''' Gets the records from imark family table based on given certificate id
    ''' </summary>
    ''' <param name="pn_intCertificateID">Certificate Id</param>
    ''' <returns>Returns the imark family table as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetFamilies(ByVal pn_intCertificateID As Integer) As DataTable

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const CertificateId As String = "pn_CertificateID"
        Const ResultStr As String = "ps_Result"
        Const GetFam As String = "ICS_MAINTENANCE.GetFamilies"
        Const ZeroNum As Short = 0
        Try
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, pn_intCertificateID, oraCmd)
            ParametersHelper.AddParametersToCommand(ResultStr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetFam

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

        Return dstResults.Tables(ZeroNum)

    End Function

    ''' <summary>
    ''' Checks whether the family id exists or not and gets the family desc
    ''' </summary>
    ''' <param name="p_intCertificateid">CertificateId</param>
    ''' <param name="p_intFamilyId">Family Id</param>
    ''' <param name="p_strFamilyDesc">Family Desc</param>
    ''' <returns>Returns boolean value based on family id existence</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function CheckIsFamilyIdExist(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As String, ByRef p_strFamilyDesc As String) As Boolean

        Dim oraCmd As New OracleCommand
        Dim strFamilyIdExists As String = String.Empty
        Const CheckIfFamilyExists As String = "ICS_MAINTENANCE.CHECKIFFAMILYEXISTS"
        Const CertificateId As String = "pn_Certificateid"
        Const FamilyId As String = "pn_FamilyID"
        Const FamilyExist As String = "ps_Family_Exist"
        Const FamilyDesc As String = "ps_Family_Desc"
        Const OneNum As Short = 1
        Const FiftyNum As Short = 50
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CheckIfFamilyExists

            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Int32, p_intCertificateid, oraCmd)
            ParametersHelper.AddParametersToCommand(FamilyId, ParameterDirection.Input, OracleType.Int32, p_intFamilyId, oraCmd)

            Dim oraFamilyIdExist As New OracleParameter()
            oraFamilyIdExist.ParameterName = FamilyExist
            oraFamilyIdExist.OracleType = OracleType.VarChar
            oraFamilyIdExist.Direction = ParameterDirection.Output
            oraFamilyIdExist.Size = OneNum

            oraCmd.Parameters.Add(oraFamilyIdExist)

            Dim oraFamilyDesc As New OracleParameter()
            oraFamilyDesc.ParameterName = FamilyDesc
            oraFamilyDesc.OracleType = OracleType.VarChar
            oraFamilyDesc.Direction = ParameterDirection.Output
            oraFamilyDesc.Size = FiftyNum

            oraCmd.Parameters.Add(oraFamilyDesc)

            Connect()
            oraCmd.Connection = Connection
            oraCmd.ExecuteReader()

            strFamilyIdExists = CStr(oraCmd.Parameters.Item(FamilyExist).Value)
            p_strFamilyDesc = CStr(IIf(oraCmd.Parameters.Item(FamilyDesc).Value Is System.DBNull.Value, String.Empty, oraCmd.Parameters.Item(FamilyDesc).Value))

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return CBool(IIf(strFamilyIdExists.ToUpper() = YesStr, True, False))

    End Function
    ''' <summary>
    ''' Gets type of tires
    ''' </summary>
    ''' <returns>Returns type of tires as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetTireType() As DataTable
        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const TireTypes As String = "pc_TIRETYPES"
        Const GetTireTypes As String = "TESTRESULTS_CRUD.GetTireTypes"
        Const ZeroNum As Short = 0

        Try
            Dim oraResults As New OracleParameter(TireTypes, OracleType.Cursor)
            oraResults.Direction = ParameterDirection.Output
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetTireTypes

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

        Return dstResults.Tables(ZeroNum)

    End Function
    ''' <summary>
    ''' Inserts/updates the record in Imark family table
    ''' </summary>
    ''' <param name="p_intCertificateid">Certificate id</param>
    ''' <param name="p_intFamilyID">Family id</param>
    ''' <param name="p_strFamilyCode">Family code</param>
    ''' <param name="p_strFamilyDesc">Family description</param>
    ''' <param name="p_strApplicationCat">Application cat</param>
    ''' <param name="p_strConstructionType">Construction type</param>
    ''' <param name="p_strStructureType">Structure type</param>
    '''  <param name="p_strMountingType">Mounting type</param>
    ''' <param name="p_strAspectRatioCat">Aspect ratio cat</param>
    ''' <param name="p_strSpeedRatingCat">Speed rating cat</param>
    ''' <param name="p_strLoadIndexCat">Load index cat</param>
    ''' <param name="p_strUserName">User name</param>  
    ''' <returns>Returns boolean value based on save operation</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
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
        'Added this operation as per IDEA2706 International Certification System Modifications 

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand
        Dim rowsaffected As Integer = 0
        Const saveFam As String = "ICS_MAINTENANCE.SAVEFAMILY"
        Const certificateId As String = "pn_Certificateid"
        Const familyId As String = "pn_FamilyID"
        Const familyCode As String = "ps_FamilyCode"
        Const familyDesc As String = "ps_FamilyDesc"
        Const applicationCat As String = "ps_ApplicationCat"
        Const constructionType As String = "ps_ConstructionType"
        Const structureType As String = "ps_StructureType"
        Const mountingType As String = "ps_MountingType"
        Const aspectRatioCat As String = "ps_AspectRatioCat"
        Const speedRatingCat As String = "ps_SpeedRatingCat"
        Const loadIndexCat As String = "ps_LoadIndexCat"
        Const username As String = "ps_UserName"
        Const oneNum As Short = 1
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SaveFam

            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateid, oraCmd)
            ParametersHelper.AddParametersToCommand(FamilyId, ParameterDirection.Input, OracleType.Number, p_intFamilyID, oraCmd)
            ParametersHelper.AddParametersToCommand(FamilyCode, ParameterDirection.Input, OracleType.VarChar, p_strFamilyCode, oraCmd)
            ParametersHelper.AddParametersToCommand(FamilyDesc, ParameterDirection.Input, OracleType.VarChar, p_strFamilyDesc, oraCmd)
            ParametersHelper.AddParametersToCommand(ApplicationCat, ParameterDirection.Input, OracleType.VarChar, p_strApplicationCat, oraCmd)
            ParametersHelper.AddParametersToCommand(ConstructionType, ParameterDirection.Input, OracleType.VarChar, p_strConstructionType, oraCmd)
            ParametersHelper.AddParametersToCommand(StructureType, ParameterDirection.Input, OracleType.VarChar, p_strStructureType, oraCmd)
            ParametersHelper.AddParametersToCommand(MountingType, ParameterDirection.Input, OracleType.VarChar, p_strMountingType, oraCmd)
            ParametersHelper.AddParametersToCommand(AspectRatioCat, ParameterDirection.Input, OracleType.VarChar, p_strAspectRatioCat, oraCmd)
            ParametersHelper.AddParametersToCommand(SpeedRatingCat, ParameterDirection.Input, OracleType.VarChar, p_strSpeedRatingCat, oraCmd)
            ParametersHelper.AddParametersToCommand(LoadIndexCat, ParameterDirection.Input, OracleType.VarChar, p_strLoadIndexCat, oraCmd)
            ParametersHelper.AddParametersToCommand(Username, ParameterDirection.Input, OracleType.VarChar, p_strUserName, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            rowsaffected = oraCmd.ExecuteNonQuery()
            blnSaved = CBool(IIf(rowsaffected = oneNum, True, False))
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
    ''' Deletes the record from Imark family table for the given family id
    ''' </summary>
    ''' <param name="p_intCertificateid">Certificate id</param>
    ''' <param name="p_intFamilyID">Family id</param>
    ''' <returns>Returns boolean value based on delete operation</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function Deletefamily(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As Integer) As Boolean

        Dim blnDeleted As Boolean = False
        Dim oraCmd As New OracleCommand
        Dim intRowsAffected As Integer = 0
        Const CertificateId As String = "pn_Certificateid"
        Const FamilyId As String = "pn_FamilyID"
        Const DeleteFam As String = "ICS_MAINTENANCE.DELETEFAMILY"
        Const ZeroNum As Short = 0
        Try
            ParametersHelper.AddParametersToCommand(CertificateId, ParameterDirection.Input, OracleType.Number, p_intCertificateid, oraCmd)
            ParametersHelper.AddParametersToCommand(FamilyId, ParameterDirection.Input, OracleType.Number, p_intFamilyId, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = DeleteFam

            Connect()
            oraCmd.Connection = Connection

            intRowsAffected = oraCmd.ExecuteNonQuery()
            blnDeleted = CBool(IIf(intRowsAffected > ZeroNum, True, False))
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
    ''' Gets the material details for a given material number
    ''' </summary>
    ''' <param name="p_strMaterialNumber">Material number</param>
    ''' <returns>Returns material details as data table</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetMaterial(ByVal p_strMaterialNumber As String) As DataTable

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const CursorStr As String = "pc_Cursor"
        Const GetMaterials As String = "ICS_MAINTENANCE.GET_MATERIALS"
        Const ZeroNum As Short = 0

        Try
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMaterialNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(CursorStr, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetMaterials

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

        Return dstResults.Tables(ZeroNum)

    End Function

    ''' <summary>
    ''' Updates the speed rating
    ''' </summary>
    ''' <param name="p_intSkuID">Sku Id</param>
    ''' <param name="p_strSpeedrating">Speed rating</param>
    ''' <returns>Returns boolean value based on edit result</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function EditMaterial(ByVal p_intSkuID As Integer, _
                                 ByVal p_strSpeedrating As String) As System.Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand
        Dim rowsaffected As Integer = 0
        Const EditProduct As String = "ICS_MAINTENANCE.EDIT_PRODUCT"
        Const SkuId As String = "pn_SKUID"
        Const SpeedRating As String = "ps_speedrating"
        Const OneNum As Short = 1
        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = EditProduct

            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_intSkuID, oraCmd)
            ParametersHelper.AddParametersToCommand(SpeedRating, ParameterDirection.Input, OracleType.VarChar, p_strSpeedrating, oraCmd)

            Connect()
            oraCmd.Connection = Connection
            rowsaffected = oraCmd.ExecuteNonQuery()
            blnSaved = CBool(IIf(rowsaffected = OneNum, True, False))

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
    ''' Copies certification
    ''' </summary>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <returns>Returns boolean value based on copy result</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function CopyCertification(ByVal p_strMatlNum As String) As Boolean

        Dim blnCopied As Boolean = False
        Dim oraCmd As New OracleCommand
        Dim intRowsAffected As Integer = 0
        Const CopyProduct As String = "ICS_MAINTENANCE.COPY_PRODUCT"
        Const ZeroNum As Short = 0
        Try
            ' pn_SkuId IN NUMBER           
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMatlNum, oraCmd)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CopyProduct

            Connect()
            oraCmd.Connection = Connection

            intRowsAffected = oraCmd.ExecuteNonQuery()
            blnCopied = CBool(IIf(intRowsAffected > ZeroNum, True, False))
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
    ''' Attaches the certification record
    ''' </summary>
    ''' <param name="p_skuid">Sku Id</param>
    ''' <param name="p_strCertNum">Certificate number</param>
    ''' <param name="p_strExtensionEn">Extension</param>
    ''' <param name="p_certificationtypeid">Certification type id</param>
    ''' <returns>Returns certification result as string</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function AttachCertification(ByVal p_skuid As Integer, ByVal p_strCertNum As String, ByVal p_strExtensionEn As String, ByVal p_certificationtypeid As Integer) As String


        Dim oraCmd As New OracleCommand
        Dim intRowsAffected As Integer = 0
        Dim ErrorMsg As String = String.Empty
        Const SkuId As String = "pn_skuid"
        Const CertificateNumber As String = "ps_certificateNumber"
        Const ExtensionEn As String = "ps_Extension_EN"
        Const CertificationTypeId As String = "pn_certificationtypeid"
        Const ErrorMsgStr As String = "ps_ErrorMsg"
        Const AttachProduct As String = "ICS_MAINTENANCE.ATTACH_PRODUCT"

        Try
            ' pn_SkuId IN NUMBER           
            ParametersHelper.AddParametersToCommand(SkuId, ParameterDirection.Input, OracleType.Number, p_skuid, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_strCertNum, oraCmd)
            ParametersHelper.AddParametersToCommand(ExtensionEn, ParameterDirection.Input, OracleType.VarChar, p_strExtensionEn, oraCmd)
            ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, p_certificationtypeid, oraCmd)

            Dim oraErrorMsg As New OracleParameter()
            oraErrorMsg.ParameterName = ErrorMsgStr
            oraErrorMsg.OracleType = OracleType.VarChar
            oraErrorMsg.Direction = ParameterDirection.Output
            oraErrorMsg.Size = 30

            oraCmd.Parameters.Add(oraErrorMsg)

            ' Configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = AttachProduct

            Connect()
            oraCmd.Connection = Connection

            oraCmd.ExecuteReader()

            If oraCmd.Parameters.Item(ErrorMsgStr).Value IsNot DBNull.Value Then
                ErrorMsg = CStr(oraCmd.Parameters.Item(ErrorMsgStr).Value)
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
    ''' Refreshes product data
    ''' </summary>
    ''' <param name="p_strMaterialNumber">Material number</param>
    ''' <param name="p_strErrorDesc">Error description</param>
    ''' <returns>Returns error number as an integer</returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function RefreshProduct(ByVal p_strMaterialNumber As String, ByRef p_strErrorDesc As String) As Integer
        Dim oraCmd As New OracleCommand
        Dim errNumber As Integer = 0
        Const ErrorNum As String = "pn_ErrorNum"
        Const ErrorMsg As String = "ps_ErrorMsg"
        Const RefreshProd As String = "Ics_Maintenance.Refresh_Product"
        Const TwentyNum As Short = 20
        Const FourThousandNum As Short = 4000

        Try
            ParametersHelper.AddParametersToCommand(MatlNum, ParameterDirection.Input, OracleType.VarChar, p_strMaterialNumber, oraCmd)
            ParametersHelper.AddParametersToCommand(ErrorNum, ParameterDirection.Output, OracleType.Number, TwentyNum, Nothing, oraCmd)
            ParametersHelper.AddParametersToCommand(ErrorMsg, ParameterDirection.Output, OracleType.VarChar, FourThousandNum, Nothing, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = RefreshProd

            Connect()
            oraCmd.Connection = Connection

            oraCmd.ExecuteReader()

            errNumber = CInt(oraCmd.Parameters.Item(ErrorNum).Value)

            If oraCmd.Parameters.Item(ErrorMsg).Value IsNot DBNull.Value Then
                p_strErrorDesc = CStr(oraCmd.Parameters.Item(ErrorMsg).Value)
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