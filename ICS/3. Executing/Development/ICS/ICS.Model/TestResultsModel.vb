Imports CooperTire.ICS.Datasets
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

Imports System.Globalization

''' <summary>
'''  Test Results - Business Process Model.
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
Public Class TestResultsModel

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.
    ' Added Operation as parameter for HDR SaveTestResults_NoTransaction methods

#Region "Variables"
    ''' <summary>
    ''' variable to hold the True value
    ''' </summary>
    ''' <remarks></remarks>
    Private trueValue As String = "Y"

    ''' <summary>
    ''' variable to hold the False value
    ''' </summary>
    ''' <remarks></remarks>
    Private falseValue As String = "N"

    ''' <summary>
    ''' variable to hold the Certification Name value
    ''' </summary>
    ''' <remarks></remarks>
    Private certificationName As String = "ECE3054"

    ''' <summary>
    ''' variable to hold the Certification Type Name value
    ''' </summary>
    ''' <remarks></remarks>
    Private certificationTypeName1 As String = "GSO"

    ''' <summary>
    ''' variable to hold the Certification Name value
    ''' </summary>
    ''' <remarks></remarks>
    Private certificationTypeName2 As String = "NOM"

    ''' <summary>
    ''' variable to hold the Substitution Field value
    ''' </summary>
    ''' <remarks></remarks>
    Private substitutionField1 As String = "PASSENGER ENDURANCE INFLATION"

    ''' <summary>
    ''' variable to hold the Substitution Field value
    ''' </summary>
    ''' <remarks></remarks>
    Private substitutionField2 As String = "PASSENGER WHEEL TEST SPEED"

    ''' <summary>
    ''' variable to hold the Substitution Field value
    ''' </summary>
    ''' <remarks></remarks>
    Private substitutionField3 As String = "PASSENGER HIGHSPEED INFLATION"

    ''' <summary>
    ''' variable to hold the zero value
    ''' </summary>
    ''' <remarks></remarks>
    Private zero As String = "0"c


#End Region

#Region "Methods"

    ''' <summary>
    '''  Method to Determine Whether To Retrieve Product Data From TRACS or ICS Database.
    ''' </summary>
    ''' <returns>TRProductSectionData</returns> 
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_strCertificationNumber">Certificate number</param>
    ''' <param name="p_iSKUID">SKU Id</param>
    ''' <param name="p_iCertificationTypeId">Certificate type</param>
    ''' <param name="p_iManufacturingLocationId">Location Id to pull TRACS data from</param>
    ''' <param name="p_iTireTypeId">Tire type</param>
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
    Public Function GetProductTRSectionData(ByVal p_strMatlNum As String, ByVal p_strCertificationNumber As String, _
                                        ByVal p_iSKUID As Integer, ByRef p_iTireTypeId As Integer, ByVal p_iCertificationTypeId As Integer, _
                                        ByVal p_iManufacturingLocationId As Integer) As TRProductSectionData
        Try
            Dim objTRProductSectionData As TRProductSectionData

            '--that didn't work -- put code back in again.
            If Not String.IsNullOrEmpty(p_strCertificationNumber) Then
                'Get Product data from ICS database
                objTRProductSectionData = GetProductTRSectionData(p_strMatlNum, p_iSKUID, p_strCertificationNumber, p_iTireTypeId)
            Else
                'get product data from call to GetProductData
                objTRProductSectionData = GetProductTRSectionData_SKUTRACS(p_strMatlNum, p_iSKUID, p_strCertificationNumber, p_iTireTypeId, p_iCertificationTypeId, p_iManufacturingLocationId)

                If p_iTireTypeId = 0 Then
                    Dim objTRTemp As TRProductSectionData = GetProductTRSectionData(p_strMatlNum, p_iSKUID, p_strCertificationNumber, p_iTireTypeId)
                End If
            End If

            Return objTRProductSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Retrieve Product Data From ICS Database.
    ''' </summary>
    ''' <returns>TRProductSectionData</returns> 
    '''  <param name="p_iSKUID">SKU Id.</param>
    ''' <param name="p_iTireTypeId">Tire Type Id.</param>
    ''' <param name="p_strCertificationNumber">Certification Number</param>
    ''' <param name="p_strMatlNum">Material Number</param>
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
    Private Function GetProductTRSectionData(ByVal p_strMatlNum As String, ByVal p_iSKUID As Integer, ByVal p_strCertificationNumber As String, ByRef p_iTireTypeId As Integer) As TRProductSectionData
        Try
            Dim dtbProduct As ICSDataSet.ProductDataDataTable = Depository.Current.GetProductData(p_strMatlNum, p_iSKUID)
            Dim objProduct As Product = CType(MapICSDataTableToProduct(dtbProduct, p_strMatlNum, p_iSKUID, p_strCertificationNumber), Product)

            Dim objTRProductSectionData As New TRProductSectionData
            If Not objProduct Is Nothing Then
                p_iTireTypeId = objProduct.TireTypeId
                objTRProductSectionData = MapProductToTRSectionData(objProduct, False)
            End If

            Return objTRProductSectionData

        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Retrieve Product Data From TRACS.
    ''' </summary>
    ''' <returns>TRProductSectionData</returns> 
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_iSKUID">SKU Id</param>
    ''' <param name="p_strCertificationNumber">Certification number</param>
    ''' <param name="p_iTireTypeId">Tire type</param>
    ''' <param name="p_iCertificationTypeId">Certification type</param>
    ''' <param name="p_iManufacturingLocationId">Location Id to pull TRACS data from</param>
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
    Private Function GetProductTRSectionData_SKUTRACS(ByVal p_strMatlNum As String, ByVal p_iSKUID As Integer, ByVal p_strCertificationNumber As String, ByRef p_iTireTypeId As Integer,
                                                      ByVal p_iCertificationTypeId As Integer, ByVal p_iManufacturingLocationId As Integer) As TRProductSectionData
        Try
            'gets product data from ICS product table.
            Dim dstSKU As ICS.Datasets.SKUtoICSDataset = Depository.Current.GetProductData_SKUTRACS(p_strMatlNum)


            Dim objTRTemp As TRProductSectionData = GetProductTRSectionData(p_strMatlNum, p_iSKUID, p_strCertificationNumber, p_iTireTypeId)
            Dim dstTRACS As ICS.Datasets.TRACStoICSDataset = Depository.Current.GetTRACSData(p_iCertificationTypeId, p_iTireTypeId, p_strMatlNum, p_iManufacturingLocationId)

            Dim objProduct As New Product
            If Not (dstTRACS Is Nothing Or dstSKU Is Nothing) Then
                objProduct = CType(MapSKUTRACSDataTableToProduct(dstSKU.ProductData, dstTRACS.RecentTestData, p_strMatlNum, p_iSKUID, p_strCertificationNumber), Product)
                p_iTireTypeId = objProduct.TireTypeId
            End If
            objProduct.MFGWWYY = GetRecentSerialDate(dstTRACS)
            objProduct.MostRecentTestDate = GetRecentTestDate(dstTRACS) 'JESEITZ 3/21/14

            Dim objTRProductSectionData As New TRProductSectionData
            If Not objProduct Is Nothing Then
                objTRProductSectionData = MapProductToTRSectionData(objProduct, True)
            End If

            Return objTRProductSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get the most recent serial date from *HDR tables.
    ''' </summary>
    ''' <returns>String</returns> 
    ''' <param name="dstTRACS">TRACS to ICS dataset.</param>
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
    Private Function GetRecentSerialDate(ByVal dstTRACS As ICS.Datasets.TRACStoICSDataset) As String
        Try
            Dim recentDate As String = "0100"
            Dim hdrSerialDate As String = recentDate
            Dim tableIndex As Integer = 0
            Dim hdrTable As DataTable = Nothing
            If (Not dstTRACS Is Nothing AndAlso dstTRACS.Tables.Count > 0) Then
                For tableIndex = 0 To dstTRACS.Tables.Count - 1
                    If (dstTRACS.Tables(tableIndex).TableName.Contains("Hdr")) Then
                        hdrTable = dstTRACS.Tables(tableIndex)
                        If (hdrTable.Rows.Count > 0) Then
                            hdrSerialDate = GetHdrDateInWWYYFormat(hdrTable)
                            recentDate = GetHdrSerialDate(hdrSerialDate.Trim(), recentDate.Trim())
                        End If
                    End If
                Next
            End If
            If (recentDate = "0100") Then
                recentDate = String.Empty
            End If
            Return recentDate
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Compare and get the recent date in Hdr Tables .
    ''' </summary>
    ''' <returns>String</returns> 
    ''' <param name="hdrSerialDate">Serial date.</param>
    ''' <param name="recentSerialDate">Recent Serial Date.</param>    
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
    Private Function GetHdrSerialDate(ByVal hdrSerialDate As String, ByVal recentSerialDate As String) As String
        Try
            Dim recentDate As String = recentSerialDate
            If (String.IsNullOrEmpty(hdrSerialDate) Or hdrSerialDate.Length <> 4 Or Not IsNumeric(hdrSerialDate)) Then
                Return recentDate
            Else

                If (CInt(hdrSerialDate.Substring(2, 2)) > CInt(recentSerialDate.Substring(2, 2))) Then
                    recentDate = hdrSerialDate
                ElseIf (CInt(hdrSerialDate.Substring(2, 2)) = CInt(recentSerialDate.Substring(2, 2))) Then
                    If (CInt(hdrSerialDate.Substring(0, 2)) > CInt(recentSerialDate.Substring(0, 2))) Then
                        recentDate = hdrSerialDate
                    End If
                End If
                Return recentDate
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get Hdr Date in WWYY Format.
    ''' </summary>
    ''' <returns>String</returns> 
    ''' <param name="hdrTable">Table.</param>
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
    Private Function GetHdrDateInWWYYFormat(ByVal hdrTable As DataTable) As String
        Try
            Dim hdrDateInWWYYFormat As String = String.Empty
            If (Not hdrTable.Rows(0)("SerialDate") Is System.DBNull.Value _
                                    AndAlso Not String.IsNullOrEmpty(CStr(hdrTable.Rows(0).Item("SerialDate")))) Then
                If (IsDate(hdrTable.Rows(0)("SerialDate"))) Then
                    hdrDateInWWYYFormat = GetWeekOfYearInWWYYFormat(CDate(hdrTable.Rows(0)("SerialDate")))
                Else
                    hdrDateInWWYYFormat = hdrTable.Rows(0)("SerialDate").ToString()
                End If
            End If
            Return hdrDateInWWYYFormat
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Convert date into Week Year format (WWYY). Get the Week and year from passed serial date.Prefix "0" if the wwyy format contains 3 digits.
    ''' </summary>
    ''' <returns>Dataset</returns> 
    ''' <param name="serialDate">Serial Date.</param>
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
    Private Function GetWeekOfYearInWWYYFormat(ByVal serialDate As Date) As String
        Try
            Dim convertedValue As String = String.Empty
            Dim calendarWeek As String = String.Empty
            Dim calendarYear As String = String.Empty
            Dim zeroString As String = "0"
            Dim dfi As DateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo
            Dim cal As Calendar = dfi.Calendar
            If (Not String.IsNullOrEmpty(CStr(serialDate))) Then
                calendarYear = CStr(serialDate.Year)
                calendarYear = calendarYear.Substring(2, 2)
                calendarWeek = CStr(cal.GetWeekOfYear(serialDate, dfi.CalendarWeekRule, dfi.FirstDayOfWeek))
                convertedValue = String.Format("{0}{1}", calendarWeek, calendarYear)
                If convertedValue.Length = 3 Then
                    convertedValue = String.Format("{0}{1}", zeroString, convertedValue)
                End If
            End If
            Return convertedValue
        Catch ex As Exception
            Throw
        End Try
    End Function

    'JESEITZ added to get most recent test date 3/21/2014
    ''' <summary>
    '''  Get the most recent serial date from *HDR tables.
    ''' </summary>
    ''' <returns>Date</returns> 
    ''' <param name="dstTRACS">TRACS dataset.</param>
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
    Private Function GetRecentTestDate(ByVal dstTRACS As ICS.Datasets.TRACStoICSDataset) As Date
        Try
            Dim recentDate As Date = CDate("01/01/1901")
            Dim hdrTestDate As Date = recentDate
            Dim tableIndex As Integer = 0
            Dim hdrTable As DataTable = Nothing
            If (Not dstTRACS Is Nothing AndAlso dstTRACS.Tables.Count > 0) Then
                For tableIndex = 0 To dstTRACS.Tables.Count - 1
                    If (dstTRACS.Tables(tableIndex).TableName.Contains("Hdr")) Then
                        hdrTable = dstTRACS.Tables(tableIndex)
                        If (hdrTable.Rows.Count > 0) Then
                            hdrTestDate = GetHdrDateInDateFormat(hdrTable)
                            recentDate = GetHdrTestDate(hdrTestDate, recentDate)
                        End If
                    End If
                Next
            End If
            Return recentDate
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Compare and get the recent date in Hdr Tables .
    ''' </summary>
    ''' <returns>Date</returns> 
    ''' <param name="hdrTestDate">Test Date.</param>
    ''' <param name="recentTestDate">recent test Data.</param>    
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
    Private Function GetHdrTestDate(ByVal hdrTestDate As Date, ByVal recentTestDate As Date) As Date
        Try
            Dim recentDate As String = CStr(recentTestDate)

            If hdrTestDate > recentTestDate Then
                recentDate = CStr(hdrTestDate)
            Else
                recentDate = CStr(recentTestDate)
            End If
            Return CDate(recentDate)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get Hdr Date in WWYY Format.
    ''' </summary>
    ''' <returns>Date</returns> 
    ''' <param name="hdrTable">Table.</param>
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
    Private Function GetHdrDateInDateFormat(ByVal hdrTable As DataTable) As Date
        Try
            Dim hdrDateInDateFormat As Date = CDate("01/01/1901")
            If (Not hdrTable.Rows(0)("CompletionDate") Is System.DBNull.Value _
                                    AndAlso Not String.IsNullOrEmpty(CStr(hdrTable.Rows(0).Item("CompletionDate")))) Then
                If (IsDate(hdrTable.Rows(0)("CompletionDate"))) Then
                    hdrDateInDateFormat = CDate(hdrTable.Rows(0).Item("CompletionDate"))
                Else
                    'may have to convert? -- see what we get.
                    hdrDateInDateFormat = CDate(hdrTable.Rows(0).Item("CompletionDate"))
                End If
            End If
            Return hdrDateInDateFormat
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map ICS Datasets product datatable to Product entity object.
    ''' </summary>
    ''' <param name="p_dtbICSProduct">ICS Product.</param>
    ''' <param name="p_strCertificationNumber">Certification Number.</param>    
    ''' <param name="p_iSKUID">SKU Id</param>
    ''' <param name="p_strMatlNum">Material Number</param>
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
    Private Function MapICSDataTableToProduct(ByVal p_dtbICSProduct As ICSDataSet.ProductDataDataTable, ByVal p_strMatlNum As String, ByVal p_iSKUID As Integer, ByVal p_strCertificationNumber As String) As Product
        Try
            Dim objProduct As Product
            Dim dstTRACSProduct As New ICS.Datasets.SKUtoICSDataset
            dstTRACSProduct.EnforceConstraints = False
            dstTRACSProduct.ProductData.Merge(p_dtbICSProduct)

            objProduct = CType(MapSKUTRACSDataTableToProduct(dstTRACSProduct.ProductData, Nothing, p_strMatlNum, p_iSKUID, p_strCertificationNumber), Product)

            Dim drwProd As ICSDataSet.ProductDataRow

            If p_dtbICSProduct.Rows.Count = 0 Then
                Return objProduct
            End If

            drwProd = CType(p_dtbICSProduct.Rows(0), ICSDataSet.ProductDataRow)

            objProduct.MaterialNumber = drwProd.MATL_NUM.TrimStart(CChar(zero))

            If Not drwProd.IsBiasBeltedRadialNull Then
                objProduct.BiasBeltedRadial = drwProd.BiasBeltedRadial
            End If
            If Not drwProd.IsTREADPATTERNNull Then
                objProduct.TreadPattern = drwProd.TREADPATTERN
            End If
            If Not drwProd.IsSPECIALPROTECTIVEBANDNull Then
                objProduct.SpecialProtectiveBand = drwProd.SPECIALPROTECTIVEBAND
            End If
            If Not drwProd.IsNOMINALTIREWIDTHNull Then
                objProduct.NominalTireWidth = drwProd.NOMINALTIREWIDTH
            End If
            If Not drwProd.IsASPECTRATIONull Then
                objProduct.AspectRatio = drwProd.ASPECTRATIO
            End If
            If Not drwProd.IsTREADWEARINDICATORSNull Then
                objProduct.TreadwearIndicator = drwProd.TREADWEARINDICATORS
            End If
            If Not drwProd.IsNAMEOFMANUFACTURERNull Then
                objProduct.NameOfManufacture = drwProd.NAMEOFMANUFACTURER
            End If
            If Not drwProd.IsFAMILYNull Then
                objProduct.Family = drwProd.FAMILY
            End If
            If Not drwProd.IsBrandDescNull Then
                objProduct.BrandDesc = drwProd.BrandDesc
            End If
            If Not drwProd.IsBrandNull Then
                objProduct.Brand = drwProd.Brand
            End If
            If Not drwProd.IsBrand_LineNull Then
                objProduct.BrandLine = drwProd.Brand_Line
            End If
            If Not drwProd.IsDisContinuedDateNull Then
                objProduct.DiscontinuedDate = drwProd.DisContinuedDate
            End If
            If Not drwProd.IsPSNNull Then
                objProduct.PSN = drwProd.PSN
            End If
            If Not drwProd.IsSpecNumberNull Then
                objProduct.SpecNumber = drwProd.SpecNumber
            End If
            If Not drwProd.IsTireTypeIdNull Then
                objProduct.TireTypeId = drwProd.TireTypeId
            End If
            If Not drwProd.IsDOTSerialNumberNull Then
                objProduct.DOTSerialNumber = drwProd.DOTSerialNumber
            End If
            If Not drwProd.IsMostRecentTestDateNull Then
                objProduct.MostRecentTestDate = CDate(drwProd.MostRecentTestDate)
            End If
            If Not drwProd.IsSerialDateNull Then
                objProduct.SerialDate = CDate(drwProd.SerialDate)
            End If
            If Not drwProd.IsPlantProducedNull Then
                objProduct.PlantProduced = drwProd.PlantProduced
            End If
            If Not drwProd.IsTireTypeIdNull Then
                objProduct.TireId = CStr(drwProd.TireTypeId)
            End If
            'Added TPN
            If Not drwProd.IsTPNNull Then
                objProduct.TPN = drwProd.TPN
            End If
            If Not drwProd.IsMFGWWYYNull Then
                objProduct.MFGWWYY = drwProd.MFGWWYY
            End If
            If Not drwProd.IsSEVEREWEATHERINDNull Then
                objProduct.SevereWeatherIndicator = CBool(IIf(drwProd.SEVEREWEATHERIND.ToUpper() = trueValue, True, False))
            End If
            Return objProduct
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map SKUTRACS datasets product datatable to Product.
    ''' </summary> 
    ''' <param name="p_strCertificationNumber">Certification Number.</param>
    ''' <param name="p_strMatlNum">Material Number.</param>    
    ''' <param name="p_dtbProduct">Product Table</param>
    ''' <param name="p_dtbRecentTestData">Recent Test Data</param>
    ''' <param name="p_iSKUID">SKU Id</param>
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
    Private Function MapSKUTRACSDataTableToProduct(ByVal p_dtbProduct As ICS.Datasets.SKUtoICSDataset.ProductDataDataTable,
                                                   ByVal p_dtbRecentTestData As ICS.Datasets.TRACStoICSDataset.RecentTestDataDataTable, ByVal p_strMatlNum As String, ByVal p_iSKUID As Integer,
                                                   ByVal p_strCertificationNumber As String) As Product
        Try
            Dim objProduct As New Product()
            Dim drwProd As ICS.Datasets.SKUtoICSDataset.ProductDataRow
            Dim drwRTD As ICS.Datasets.TRACStoICSDataset.RecentTestDataRow

            If p_dtbProduct.Rows.Count = 0 Then
                objProduct.MaterialNumber = p_strMatlNum
                Return objProduct
            End If

            drwProd = CType(p_dtbProduct.Rows(0), Datasets.SKUtoICSDataset.ProductDataRow)

            objProduct.MaterialNumber = drwProd.SKU
            objProduct.SKUID = p_iSKUID

            If Not drwProd.IsBrandDescNull And Not String.IsNullOrEmpty(drwProd.BrandDesc) Then
                objProduct.BrandDesc = drwProd.BrandDesc
            End If

            If Not drwProd.IsSizeStampNull And Not String.IsNullOrEmpty(drwProd.SizeStamp) Then
                objProduct.TireSizeStamp = drwProd.SizeStamp
            End If
            If Not drwProd.IsSpeedRatingNull And Not String.IsNullOrEmpty(drwProd.SpeedRating) Then
                objProduct.SpeedRating = drwProd.SpeedRating
            End If
            If Not drwProd.IsSingLoadIndexNull And Not String.IsNullOrEmpty(drwProd.SingLoadIndex) Then
                objProduct.SingLoadIndex = drwProd.SingLoadIndex
            End If
            If Not drwProd.IsDualLoadIndexNull And Not String.IsNullOrEmpty(drwProd.DualLoadIndex) Then
                objProduct.DualLoadIndex = drwProd.DualLoadIndex
            End If
            If Not drwProd.IsBiasBeltedRadialNull And Not String.IsNullOrEmpty(drwProd.BiasBeltedRadial) Then
                objProduct.BiasBeltedRadial = drwProd.BiasBeltedRadial.ToUpper()
            End If
            If Not drwProd.IsTubelessYNNull AndAlso drwProd.TubelessYN.ToLower().Equals(trueValue) Then
                objProduct.TubelessYN = True
            End If
            If Not drwProd.IsReinforcedYNNull AndAlso drwProd.ReinforcedYN.ToLower().Equals(trueValue) Then
                objProduct.ReinforcedYN = True
            End If
            If Not drwProd.IsExtraLoadYNNull AndAlso drwProd.ExtraLoadYN.ToLower().Equals(trueValue) Then
                objProduct.ExtraLoadYN = True
            End If
            If Not drwProd.IsUTQGTempNull Then
                objProduct.UTQGTemp = drwProd.UTQGTemp
            End If
            If Not drwProd.IsUTQGTractionNull Then
                objProduct.UTQGTraction = drwProd.UTQGTraction
            End If
            If Not drwProd.IsUTQGTreadwearNull Then
                objProduct.UTQGTreadwear = drwProd.UTQGTreadwear
            End If
            If Not drwProd.IsMudSnowYNNull AndAlso drwProd.MudSnowYN.ToLower().Equals(trueValue) Then
                objProduct.MudSnow = True
            End If
            If Not drwProd.IsRimDiameterNull Then
                objProduct.RimDiameter = drwProd.RimDiameter
            End If
            If Not drwProd.IsLoadRangeNull Then
                objProduct.LoadRange = drwProd.LoadRange
            End If

            If Not drwProd.IsRegroovableIndNull Then
                Select Case drwProd.RegroovableInd
                    Case trueValue
                        objProduct.RegroovableInd = True
                    Case falseValue
                        objProduct.RegroovableInd = False
                    Case Else
                        objProduct.RegroovableInd = False
                End Select
            End If

            If Not drwProd.IsIMarkNull Then
                objProduct.IMark = drwProd.IMark
            End If

            'Added Aspect Ratio to TRACS API
            If Not drwProd.IsAspectRatioNull Then
                objProduct.AspectRatio = drwProd.AspectRatio
            End If

            'Added TPN to TRACS API
            If Not drwProd.IsPPNNull Then
                objProduct.TPN = drwProd.PPN
            End If

            'Grab the data from RecentTestData table
            If Not p_dtbRecentTestData Is Nothing Then
                drwRTD = CType(p_dtbRecentTestData.Rows(0), Datasets.TRACStoICSDataset.RecentTestDataRow)

                Try
                    If Not drwRTD.IsSerialDateNull Then
                        objProduct.SerialDate = CDate(drwRTD.SerialDate)
                    End If
                Catch exc As Exception
                    ' Leave default value
                End Try

                If Not drwRTD.IsDOTSerialNumberNull Then
                    objProduct.DOTSerialNumber = drwRTD.DOTSerialNumber
                End If

                If Not drwRTD.IsMeaRimWidthNull Then
                    objProduct.MeaRimWidth = drwRTD.MeaRimWidth
                End If

                If Not drwRTD.IsPlantProducedNull Then
                    objProduct.PlantProduced = drwRTD.PlantProduced
                End If

                Try
                    If Not drwRTD.IsMostRecentTestDateNull Then
                        objProduct.MostRecentTestDate = ConvertToDateTime(drwRTD.MostRecentTestDate)
                    End If
                Catch exc As Exception
                    ' Leave default value
                End Try

            End If
            If Not drwProd.IsSevereWeatherIndNull Then
                objProduct.SevereWeatherIndicator = CBool(IIf(drwProd.SevereWeatherInd.Trim().ToUpper() = trueValue, True, False))
            End If
            If Not drwProd.IsMFGWWYYNull Then
                objProduct.MFGWWYY = drwProd.MFGWWYY
            End If
            ''Conversion
            If p_strCertificationNumber Is Nothing Then
                ''NOTE: need to make sure MeaRimWidth is MM

            End If

            If Not drwProd.IsTreadPatternNull AndAlso Not String.IsNullOrEmpty(drwProd.TreadPattern) Then
                objProduct.TreadPattern = drwProd.TreadPattern
            End If
            Return objProduct
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map Product To TR Section Data.
    ''' </summary>
    ''' <returns>TR Product Section Data</returns> 
    ''' <param name="p_blnSKUTRACSIndicator">SKU TRACS Indicator.</param>
    ''' <param name="p_objProduct">Product Object.</param>
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
    Private Function MapProductToTRSectionData(ByVal p_objProduct As Product, ByVal p_blnSKUTRACSIndicator As Boolean) As TRProductSectionData
        Try
            Dim objTRProductSectionData As TRProductSectionData = New TRProductSectionData()

            objTRProductSectionData.ProductFamily = p_objProduct.Family

            objTRProductSectionData.Trademark = p_objProduct.BrandDesc

            objTRProductSectionData.TreadPattern = p_objProduct.TreadPattern

            objTRProductSectionData.SizeDesignation = p_objProduct.TireSizeStamp

            objTRProductSectionData.SpecialProtectiveBand = p_objProduct.SpecialProtectiveBand

            objTRProductSectionData.StructureConstruction = p_objProduct.BiasBeltedRadial
            objTRProductSectionData.SpeedCategory = p_objProduct.SpeedRating
            objTRProductSectionData.SingLoadCapacityIndex = p_objProduct.SingLoadIndex
            objTRProductSectionData.DualLoadCapacityIndex = p_objProduct.DualLoadIndex

            Dim PlyRatingNumber As Integer
            Select Case p_objProduct.LoadRange
                Case "B"
                    PlyRatingNumber = 4
                Case "C"
                    PlyRatingNumber = 6
                Case "D"
                    PlyRatingNumber = 8
                Case "E"
                    PlyRatingNumber = 10
                Case "F" 'these were added 1/5/12 per Rhonda Riedel - jeseitz
                    PlyRatingNumber = 12
                Case "G"
                    PlyRatingNumber = 14
                Case "H"
                    PlyRatingNumber = 16
                Case "J"
                    PlyRatingNumber = 18
                Case "L"
                    PlyRatingNumber = 20
                Case "M"
                    PlyRatingNumber = 22
                Case falseValue
                    PlyRatingNumber = 24
                Case Else
                    PlyRatingNumber = 0
            End Select
            objTRProductSectionData.PlyRatingNumber = PlyRatingNumber.ToString()
            objTRProductSectionData.IndicationTubeless = p_objProduct.TubelessYN
            objTRProductSectionData.IndicationReinforced = p_objProduct.ReinforcedYN
            objTRProductSectionData.IndicationExtraLoad = p_objProduct.ExtraLoadYN
            objTRProductSectionData.Regroovable = p_objProduct.RegroovableInd
            objTRProductSectionData.MeasuringRim = CStr(p_objProduct.MeaRimWidth)

            If p_objProduct.SerialDate.Equals(DateTime.MinValue) Then
                objTRProductSectionData.DateOfManufacture = String.Empty
            Else
                objTRProductSectionData.DateOfManufacture = p_objProduct.SerialDate.ToShortDateString()
            End If

            objTRProductSectionData.TireSerialNumber = p_objProduct.DOTSerialNumber

            objTRProductSectionData.DOTCode = p_objProduct.DOTSerialNumber

            If Trim(p_objProduct.NominalTireWidth).Length = 0 Then
                If InStr(p_objProduct.TireSizeStamp, "/") > 0 Then
                    objTRProductSectionData.NominalTireWidth = p_objProduct.TireSizeStamp.Substring(InStr(p_objProduct.TireSizeStamp, "/") - 4, 3)
                Else
                    objTRProductSectionData.NominalTireWidth = String.Empty
                End If
            Else
                objTRProductSectionData.NominalTireWidth = p_objProduct.NominalTireWidth
            End If

            objTRProductSectionData.AspectRatio = p_objProduct.AspectRatio
            objTRProductSectionData.NominalRimDiameter = CStr(p_objProduct.RimDiameter)
            objTRProductSectionData.TemperatureRating = p_objProduct.UTQGTemp
            objTRProductSectionData.Traction = p_objProduct.UTQGTraction
            objTRProductSectionData.TreadWear = p_objProduct.UTQGTreadwear
            objTRProductSectionData.ManufacturingLocationOfOrigin = p_objProduct.PlantProduced
            objTRProductSectionData.TreadwearIndicators = "1.6"
            objTRProductSectionData.InmetroMark = p_objProduct.IMark
            objTRProductSectionData.CargoCapacity = p_objProduct.LoadRange
            objTRProductSectionData.Type = CStr(IIf(p_objProduct.TubelessYN, "Tubeless", "Tube"))
            objTRProductSectionData.NameOfManufacturer = p_objProduct.NameOfManufacture
            If Not String.IsNullOrEmpty(p_objProduct.TPN) Then
                objTRProductSectionData.TPN = p_objProduct.TPN.ToString()
            End If

            If p_objProduct.MostRecentTestDate.Year <= 1901 Then
                objTRProductSectionData.DateOfTest = String.Empty
            Else
                objTRProductSectionData.DateOfTest = p_objProduct.MostRecentTestDate.ToShortDateString()
            End If

            objTRProductSectionData.DOTCode = p_objProduct.DOTSerialNumber

            If Not p_blnSKUTRACSIndicator Then
                objTRProductSectionData.OriginalProduct = p_objProduct
            End If

            If String.IsNullOrEmpty(p_objProduct.MFGWWYY) Then
                objTRProductSectionData.MFGWWYY = String.Empty
            Else
                objTRProductSectionData.MFGWWYY = p_objProduct.MFGWWYY
            End If

            objTRProductSectionData.SevereWeatherIndicator = p_objProduct.SevereWeatherIndicator
            objTRProductSectionData.MudSnow = p_objProduct.MudSnow

            If String.IsNullOrEmpty(CStr(p_objProduct.TireTypeId)) Then
                objTRProductSectionData.TireId = CStr(0)
            Else
                objTRProductSectionData.TireId = CStr(p_objProduct.TireTypeId)
            End If

            Return objTRProductSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get TR Section Data.
    ''' </summary>
    ''' <returns>List of Object</returns> 
    ''' <param name="p_intCertificateNumberID">Certificate Number Id.</param>
    ''' <param name="p_intCertificationTypeId">Certification Type Id.</param>    
    ''' <param name="p_intManufacturingLocationId">Manufacturing Location Id</param>
    ''' <param name="p_intTireTypeId">Tire Type Id</param>
    ''' <param name="p_iSKUId">SKU Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strMatlNum">Material Number</param>
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
    Public Function GetTRSectionData(ByVal p_strMatlNum As String, _
                                    ByVal p_iSKUId As Integer, _
                                    ByVal p_strCertificateNumber As String, _
                                    ByVal p_intCertificateNumberID As Integer, _
                                    ByVal p_intCertificationTypeId As String, _
                                    ByVal p_intTireTypeId As Integer, _
                                    ByVal p_intManufacturingLocationId As Integer) As List(Of Object)
        Try
            Dim dstTRACStoICSDataset As ICS.Datasets.TRACStoICSDataset
            Dim dstICSDataset As ICSDataSet
            Dim intCertType As Integer

            Dim retList As New List(Of Object)

            'Gets the Information from database or mock repository
            If Not String.IsNullOrEmpty(p_strCertificateNumber) Then
                dstICSDataset = Depository.Current.GetTestResultData(p_strMatlNum, p_iSKUId, p_strCertificateNumber, p_intCertificateNumberID, CInt(p_intCertificationTypeId))
                retList = MapICSDataSetToTRSectionData(dstICSDataset, p_strMatlNum, p_iSKUId, p_intCertificateNumberID, p_intCertificationTypeId)
            Else
                intCertType = CInt(p_intCertificationTypeId)
                dstTRACStoICSDataset = Depository.Current.GetTRACSData(intCertType, p_intTireTypeId, p_strMatlNum, p_intManufacturingLocationId)

                'Set the certificate id to 0 so we always convert data from TRACS to metric.
                If Not dstTRACStoICSDataset Is Nothing Then
                    retList = MapSKUTRACSDataSetToTRSectionData(dstTRACStoICSDataset, p_strMatlNum, p_iSKUId, 0, p_intCertificationTypeId)
                End If

            End If

            Return retList
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get Requested Tests.
    ''' </summary>
    ''' <returns>List of Objects</returns> 
    ''' <param name="p_dtbClientRequest">Client Request.</param>
    ''' <param name="p_iCertificateNumberId">Certificate Number Id.</param>    
    ''' <param name="p_iCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_iSKUId">SKU Id</param>
    ''' <param name="p_iTireTypeId">Tire Type Id</param>
    ''' <param name="p_MFGWWYY">MFGWWYY</param>
    ''' <param name="p_MostRecentTestDate">Most Recent Test Date</param>
    ''' <param name="p_strMatlNum">material Number</param>
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
    Public Function GetRequestedTests(ByVal p_strMatlNum As String, ByVal p_iSKUId As Integer, ByVal p_iCertificateNumberId As Integer, _
                                      ByVal p_iCertificationTypeId As Integer, ByVal p_iTireTypeId As Integer, _
                                      ByVal p_dtbClientRequest As DataTable, _
                                      ByRef p_MFGWWYY As String, ByRef p_MostRecentTestDate As Date) As List(Of Object)

        Try
            Dim dstTRACStoICSDataset As New ICS.Datasets.TRACStoICSDataset
            Dim ostTestReq As New ClientRequest
            Dim retList As New List(Of Object)

            For Each Row As DataRow In p_dtbClientRequest.Rows
                Dim ostTestReqRow As ClientRequest.TestRequestRow = CType(ostTestReq.TestRequest.NewRow, ClientRequest.TestRequestRow)
                ostTestReqRow.ProjectNum = CStr(Row.Item("ProjectNum"))
                ostTestReqRow.TireNum = CShort(Row.Item("TireNum"))

                'Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
                ostTestReqRow.Operation = CStr(Row.Item("Operation"))
                ostTestReqRow.TestSpec = CStr(Row.Item("TestSpec"))

                ostTestReq.TestRequest.AddTestRequestRow(ostTestReqRow)
            Next

            dstTRACStoICSDataset = CType(Depository.Current.GetRequestedTests(p_iCertificationTypeId, p_iTireTypeId, ostTestReq), Datasets.TRACStoICSDataset)

            p_MFGWWYY = GetRecentSerialDate(dstTRACStoICSDataset)
            p_MostRecentTestDate = GetRecentTestDate(dstTRACStoICSDataset) 'JESEITZ 2/25/15

            retList = MapSKUTRACSDataSetToTRSectionData(dstTRACStoICSDataset, p_strMatlNum, p_iSKUId, p_iCertificateNumberId, CStr(p_iCertificationTypeId))

            Return retList
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Gets the product country status table.
    ''' </summary>
    ''' <returns>List of Objects</returns> 
    ''' <param name="dstICSDataset">ICS Dataset.</param>
    ''' <param name="p_intCertificateNumberID">Certificate Number Id.</param>    
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_iSKUId">SKU Id</param>
    ''' <param name="p_strMatlNum">material Number</param>
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
    Private Function MapICSDataSetToTRSectionData(ByVal dstICSDataset As ICSDataSet, ByVal p_strMatlNum As String, _
                                                ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer, _
                                                ByVal p_intCertificationTypeId As String) As List(Of Object)
        Try
            Dim retList As New List(Of Object)

            'Get's the objTRMeasurementSectionData And insert it on the list 
            Dim objMeasure As Measure = MapICSDataTableToMeasure(dstICSDataset.MeasureHdr, dstICSDataset.MeasureDtl, CInt(p_intCertificationTypeId), p_strMatlNum, p_iSKUId, p_intCertificateNumberID)
            Dim objTreadwear As Treadwear = MapICSDataTableToTreadwear(dstICSDataset.TreadWearHdr, dstICSDataset.TreadWearDtl, CInt(p_intCertificationTypeId), p_strMatlNum, p_iSKUId, p_intCertificateNumberID)
            Dim objPlunger As Plunger = MapICSDataTableToPlunger(dstICSDataset.PlungerHdr, dstICSDataset.PlungerDtl, CInt(p_intCertificationTypeId), p_strMatlNum, p_iSKUId, p_intCertificateNumberID)
            Dim objBeadUnSeat As BeadUnSeat = MapICSDataTableToBeadUnSeat(dstICSDataset.BeadUnseatHdr, dstICSDataset.BeadUnseatDtl, CInt(p_intCertificationTypeId), p_strMatlNum, p_iSKUId, p_intCertificateNumberID)
            Dim objEndurance As Endurance = MapICSDataTableToEndurance(dstICSDataset.EnduranceHdr, dstICSDataset.EnduranceDtl, CInt(p_intCertificationTypeId), p_strMatlNum, p_iSKUId, p_intCertificateNumberID)
            Dim objHighSpeed As HighSpeed = MapICSDataTableToHighSpeed(dstICSDataset.HighSpeedHdr, dstICSDataset.HighSpeedDtl, dstICSDataset.SpeedTestDetail, CInt(p_intCertificationTypeId), p_strMatlNum,
                                                                       p_iSKUId, p_intCertificateNumberID)
            Dim objSound As Sound = MapICSDataTableToSound(dstICSDataset.SoundHdr, dstICSDataset.SoundDetail, CInt(p_intCertificationTypeId), p_strMatlNum, p_iSKUId, p_intCertificateNumberID)
            Dim objWetGrip As WetGrip = MapICSDataTableToWetGrip(dstICSDataset.WetGripHdr, dstICSDataset.WetGripDetail, CInt(p_intCertificationTypeId), p_strMatlNum, p_iSKUId, p_intCertificateNumberID)

            retList = MapEntityToTRSectionData(objMeasure, objTreadwear, objPlunger, objBeadUnSeat, objEndurance, objHighSpeed, objSound, objWetGrip, CInt(p_intCertificationTypeId))

            Return retList
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Gets the product country status table.
    ''' </summary>
    ''' <returns>List of Objects</returns> 
    ''' <param name="dstTRACStoICSDataset">TRACS to ICS Dataset.</param>
    ''' <param name="p_intCertificateNumberID">Certificate Number Id.</param>    
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_iSKUId">SKU Id</param>
    ''' <param name="p_strMatlNum">material Number</param>
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
    Private Function MapSKUTRACSDataSetToTRSectionData(ByVal dstTRACStoICSDataset As ICS.Datasets.TRACStoICSDataset, _
                                                    ByVal p_strMatlNum As String, ByVal p_iSKUId As Integer, _
                                                    ByVal p_intCertificateNumberID As Integer, ByVal p_intCertificationTypeId As String) As List(Of Object)
        Try
            Dim retList As New List(Of Object)

            'Get's the objTRMeasurementSectionData And insert it on the list 
            If (Not dstTRACStoICSDataset Is Nothing And dstTRACStoICSDataset.Tables.Count > 0) Then

                Dim objMeasure As Measure = MapTRACSDataTableToMeasure(dstTRACStoICSDataset.MeasureHdr, dstTRACStoICSDataset.MeasureDtl, CInt(p_intCertificationTypeId), p_strMatlNum, p_iSKUId,
                                                                       p_intCertificateNumberID)
                Dim objTreadwear As Treadwear = MapTRACSDataTableToTreadwear(dstTRACStoICSDataset.TreadWearHdr, dstTRACStoICSDataset.TreadWearDtl, CInt(p_intCertificationTypeId), p_strMatlNum,
                                                                             p_iSKUId, p_intCertificateNumberID)
                Dim objPlunger As Plunger = MapTRACSDataTableToPlunger(dstTRACStoICSDataset.PlungerHdr, dstTRACStoICSDataset.PlungerDtl, CInt(p_intCertificationTypeId), p_strMatlNum, p_iSKUId,
                                                                       p_intCertificateNumberID)
                Dim objBeadUnSeat As BeadUnSeat = MapTRACSDataTableToBeadUnSeat(dstTRACStoICSDataset.BeadUnseatHdr, dstTRACStoICSDataset.BeadUnseatDtl, CInt(p_intCertificationTypeId), p_strMatlNum,
                                                                                p_iSKUId, p_intCertificateNumberID)
                Dim objEndurance As Endurance = MapTRACSDataTableToEndurance(dstTRACStoICSDataset.EnduranceHdr, dstTRACStoICSDataset.EnduranceDtl, CInt(p_intCertificationTypeId), p_strMatlNum,
                                                                             p_iSKUId, p_intCertificateNumberID)
                Dim objHighSpeed As HighSpeed = MapTRACSDataTableToHighSpeed(dstTRACStoICSDataset.HighSpeedHdr, dstTRACStoICSDataset.HighSpeedDtl, CInt(p_intCertificationTypeId), p_strMatlNum,
                                                                             p_iSKUId, p_intCertificateNumberID)
                retList = MapEntityToTRSectionData(objMeasure, objTreadwear, objPlunger, objBeadUnSeat, objEndurance, objHighSpeed, Nothing, Nothing, CInt(p_intCertificationTypeId))

            End If
            Return retList
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Gets the product country status table.
    ''' </summary>
    ''' <returns>List of Objects</returns> 
    ''' <param name="p_objBeadUnSeat">Bead Unseat.</param>
    ''' <param name="p_objEndurance">Endurance.</param>    
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_objHighSpeed">High Speed</param>
    ''' <param name="p_objMeasure">Measure</param>
    ''' <param name="p_objPlunger">Plunger</param>
    ''' <param name="p_objSound">Sound</param>
    ''' <param name="p_objTreadwear">Treadwear</param>
    ''' <param name="p_objWetGrip">WetGrip</param>
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
    Private Function MapEntityToTRSectionData(ByVal p_objMeasure As Measure, ByVal p_objTreadwear As Treadwear, _
                                            ByVal p_objPlunger As Plunger, ByVal p_objBeadUnSeat As BeadUnSeat, _
                                            ByVal p_objEndurance As Endurance, ByVal p_objHighSpeed As HighSpeed, _
                                            ByVal p_objSound As Sound, ByVal p_objWetGrip As WetGrip, ByVal p_intCertificationTypeId As Integer) As List(Of Object)
        Try
            Dim retList As New List(Of Object)

            Dim objTRMeasurementSectionData As TRMeasurementSectionData = MapEntitiesToTRMeasurementSectionData(p_objMeasure, p_objTreadwear, p_objPlunger, p_objBeadUnSeat, p_intCertificationTypeId)

            Dim objTRProjectSectionData As TRProjectSectionData = MapProjectSectionData(p_objMeasure, p_objPlunger, p_objBeadUnSeat, p_objTreadwear, p_objEndurance, p_objHighSpeed, p_objSound, p_objWetGrip)
            Dim objTREnduranceBeforeSectionData As TREnduranceTestGeneralBeforeSectionData = MapEnduranceBeforeSectionData(p_objEndurance)
            Dim objTREnduranceSectionData As TREnduranceSectionData = MapEnduranceSectionData(p_objEndurance)
            Dim objTREnduranceAfterSectionData As TREnduranceTestGeneralAfterSectionData = MapEnduranceAfterSectionData(p_objEndurance)

            Dim objTRHighSpeedBeforeSectionData As TRHighSpeedTestGeneralBeforeSectionData = MapHighSpeedBeforeSectionData(p_objHighSpeed)
            Dim objTRHighSpeedSectionData As TRHighSpeedSectionData = MapHighSpeedSectionData(p_objHighSpeed)
            Dim objTRHighSpeedAfterSectionData As TRHighSpeedTestGeneralAfterSectionData = MapHighSpeedAfterSectionData(p_objHighSpeed)

            Dim objTRSoundWetGripSectionData As TRSoundWetSectionData = MapSoundWetSectionData(p_objSound, p_objWetGrip)

            retList.Add(objTRProjectSectionData)
            retList.Add(objTRMeasurementSectionData)
            retList.Add(objTREnduranceBeforeSectionData)
            retList.Add(objTREnduranceSectionData)
            retList.Add(objTREnduranceAfterSectionData)
            retList.Add(objTRHighSpeedBeforeSectionData)
            retList.Add(objTRHighSpeedSectionData)
            retList.Add(objTRHighSpeedAfterSectionData)
            retList.Add(objTRSoundWetGripSectionData)

            Return retList
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map ICS DataTable To Measure.
    ''' </summary>
    ''' <returns>Measure Object</returns> 
    ''' <param name="p_dtbICSMeasureDtl">dtbICS Measure table.</param>    
    ''' <param name="p_dtbICSMeasureHdr">dtbICS Measure Hdr</param>
    ''' <param name="p_iCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_intCertificateNumberID">Certification Number Id</param>
    ''' <param name="p_iSKUId">SKU Id</param>
    ''' <param name="p_strMatlNum">Material Number</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapICSDataTableToMeasure(ByVal p_dtbICSMeasureHdr As ICSDataSet.MeasureHdrDataTable, _
                                            ByVal p_dtbICSMeasureDtl As ICSDataSet.MeasureDtlDataTable, _
                                            ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                            ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As Measure
        Try
            Dim objMeasure As New Measure
            Dim dtbTRACSMeasureHdr As New ICS.Datasets.TRACStoICSDataset.MeasureHdrDataTable
            Dim dtbTRACSMeasureDtl As New ICS.Datasets.TRACStoICSDataset.MeasureDtlDataTable
            Dim drwMeasureHdr As ICSDataSet.MeasureHdrRow

            If p_dtbICSMeasureHdr.Rows.Count = 0 Then
                Return objMeasure
            End If

            dtbTRACSMeasureHdr.Merge(p_dtbICSMeasureHdr)
            dtbTRACSMeasureDtl.Merge(p_dtbICSMeasureDtl)

            objMeasure = MapTRACSDataTableToMeasure(dtbTRACSMeasureHdr, dtbTRACSMeasureDtl, p_iCertificationTypeId, p_strMatlNum, p_iSKUId, p_intCertificateNumberID)

            drwMeasureHdr = CType(p_dtbICSMeasureHdr.Rows(0), ICSDataSet.MeasureHdrRow)

            If Not drwMeasureHdr.IsMATL_NUMNull Then
                objMeasure.MaterialNumber = drwMeasureHdr.MATL_NUM.TrimStart(CChar(zero))
            End If

            If Not drwMeasureHdr.IsDOTSerialNumberNull Then
                objMeasure.DotSerialNumber = drwMeasureHdr.DOTSerialNumber
            End If

            If Not drwMeasureHdr.IsSTARTINFLATIONPRESSURENull Then
                objMeasure.StartInfPressure = CShort(drwMeasureHdr.STARTINFLATIONPRESSURE)
            End If

            If Not drwMeasureHdr.IsENDINFLATIONPRESSURENull Then
                objMeasure.EndInfPressure = CShort(drwMeasureHdr.ENDINFLATIONPRESSURE)
            End If
            If Not drwMeasureHdr.IsADJUSTMENTNull Then
                objMeasure.Adjustment = drwMeasureHdr.ADJUSTMENT
            End If
            If Not drwMeasureHdr.IsCIRCUNFERENCENull Then
                objMeasure.Circumference = drwMeasureHdr.CIRCUNFERENCE
            End If
            If Not drwMeasureHdr.IsNOMINALDIAMETERNull Then
                objMeasure.NominalDiameter = drwMeasureHdr.NOMINALDIAMETER
            End If
            If Not drwMeasureHdr.IsNOMINALWIDTHNull Then
                objMeasure.NominalWidth = drwMeasureHdr.NOMINALWIDTH
            End If
            If Not drwMeasureHdr.IsNOMINALWIDTHPASSFAILNull Then
                objMeasure.NominalWidthPassYN = CBool(IIf(drwMeasureHdr.NOMINALWIDTHPASSFAIL = trueValue, True, False))
            End If
            If Not drwMeasureHdr.IsNOMINALWIDTHDIFERENCENull Then
                objMeasure.NominalWidthDifference = drwMeasureHdr.NOMINALWIDTHDIFERENCE
            End If
            If Not drwMeasureHdr.IsNOMINALWIDTHTOLERANCENull Then
                objMeasure.NominalWidthTolerance = drwMeasureHdr.NOMINALWIDTHTOLERANCE
            End If

            If Not drwMeasureHdr.IsMAXOVERALLDIAMETERNull Then
                objMeasure.MaxOverallDiameter = drwMeasureHdr.MAXOVERALLDIAMETER
            End If
            If Not drwMeasureHdr.IsMINOVERALLDIAMETERNull Then
                objMeasure.MinOverallDiameter = drwMeasureHdr.MINOVERALLDIAMETER
            End If
            If Not drwMeasureHdr.IsOVERALLWIDTHPASSFAILNull Then
                objMeasure.OverallWidthPassYN = CBool(IIf(drwMeasureHdr.OVERALLWIDTHPASSFAIL = falseValue, False, True))
            End If
            If Not drwMeasureHdr.IsOVERALLDIAMETERPASSFAILNull Then
                objMeasure.OverallDiameterPassYN = CBool(IIf(drwMeasureHdr.OVERALLDIAMETERPASSFAIL = trueValue, True, False))
            End If
            If Not drwMeasureHdr.IsDIAMETERDIFERENCENull Then
                objMeasure.DiameterDifference = drwMeasureHdr.DIAMETERDIFERENCE
            End If
            If Not drwMeasureHdr.IsDIAMETERTOLERANCENull Then
                objMeasure.DiameterTolerance = drwMeasureHdr.DIAMETERTOLERANCE
            End If

            If Not drwMeasureHdr.IsTENSILESTRENGHT1Null Then
                objMeasure.TensileStrength1 = drwMeasureHdr.TENSILESTRENGHT1
            End If
            If Not drwMeasureHdr.IsTENSILESTRENGHT2Null Then
                objMeasure.TensileStrength2 = drwMeasureHdr.TENSILESTRENGHT2
            End If
            If Not drwMeasureHdr.IsELONGATION1Null Then
                objMeasure.Elongation1 = CShort(drwMeasureHdr.ELONGATION1)
            End If
            If Not drwMeasureHdr.IsELONGATION2Null Then
                objMeasure.Elongation2 = CShort(drwMeasureHdr.ELONGATION2)
            End If
            If Not drwMeasureHdr.IsTENSILESTRENGHTAFTERAGE1Null Then
                objMeasure.TensileStrengthAfterAging1 = drwMeasureHdr.TENSILESTRENGHTAFTERAGE1
            End If
            If Not drwMeasureHdr.IsTENSILESTRENGHTAFTERAGE2Null Then
                objMeasure.TensileStrengthAfterAging2 = drwMeasureHdr.TENSILESTRENGHTAFTERAGE2
            End If
            If Not drwMeasureHdr.IsTEMPRESISTANCEGRADINGNull Then
                objMeasure.TemperatureResistanceGrading = drwMeasureHdr.TEMPRESISTANCEGRADING
            End If

            'Minimum Size Factor
            If Not drwMeasureHdr.IsSIZEFACTORNull Then
                objMeasure.MinSizeFactor = drwMeasureHdr.SIZEFACTOR
            End If

            If Not drwMeasureHdr.IsGTSPECNull Then
                objMeasure.GTSpecMaterialNumber = drwMeasureHdr.GTSPEC
            End If

            Return objMeasure
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Defect 10034: Modified by offshore on 26NOV2012. Commented out all metric conversion statements.
    ''' <summary>
    '''  Method to Map the data table to measure..
    ''' </summary>
    ''' <returns>Measure Object</returns> 
    ''' <param name="p_dtbTRACSMeasureHdr">The P_DTB measure HDR.</param>
    ''' <param name="p_dtbTRACSMeasureDtl">The P_DTB measure DTL.</param>
    ''' <param name="p_iCertificationTypeId">The p_i certification type id.</param>
    ''' <param name="p_strMatlNum">The p_strMatlNum. </param>
    ''' <param name="p_iSKUId">The p_i SKU id.</param>
    ''' <param name="p_intCertificateNumberID">The P_STR certificate Id.</param>   
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRACSDataTableToMeasure(ByVal p_dtbTRACSMeasureHdr As ICS.Datasets.TRACStoICSDataset.MeasureHdrDataTable, _
                                            ByVal p_dtbTRACSMeasureDtl As ICS.Datasets.TRACStoICSDataset.MeasureDtlDataTable, _
                                            ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                            ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As Measure
        Try
            Dim objMeasure As New Measure()
            Dim drwMeasure As ICS.Datasets.TRACStoICSDataset.MeasureHdrRow
            Dim drwMeasureDetail As ICS.Datasets.TRACStoICSDataset.MeasureDtlRow

            objMeasure.CertificationTypeId = p_iCertificationTypeId
            objMeasure.SKUID = p_iSKUId
            objMeasure.CertificateNumberID = p_intCertificateNumberID

            If p_dtbTRACSMeasureHdr.Rows.Count = 0 Then
                Return objMeasure
            End If

            'Measure Header
            drwMeasure = CType(p_dtbTRACSMeasureHdr.Rows(0), Datasets.TRACStoICSDataset.MeasureHdrRow)

            objMeasure.ProjectNumber = drwMeasure.ProjectNum
            objMeasure.TireNumber = drwMeasure.TireNum
            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not drwMeasure.IsOperationNull Then
                objMeasure.Operation = drwMeasure.Operation
            End If
            objMeasure.TestSpec = CStr(IIf(drwMeasure.IsTestSpecNull, String.Empty, drwMeasure.TestSpec))

            If Not drwMeasure.IsTestSKUNull Then
                objMeasure.MaterialNumber = drwMeasure.TestSKU
            End If


            Try
                '(End Date, End Time)
                If Not drwMeasure.IsCompletionDateNull Then
                    objMeasure.CompletionDate = ConvertToDateTime(drwMeasure.CompletionDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            '(Inflation Pressure, Pressure)
            If Not drwMeasure.IsInflationPressureNull Then
                objMeasure.InflationPressure = drwMeasure.InflationPressure
            End If

            'Mold Design
            If Not drwMeasure.IsMoldDesignNull Then
                objMeasure.MoldDesign = drwMeasure.MoldDesign
            End If

            'Rim
            If Not drwMeasure.IsRimWidthNull Then
                objMeasure.RimWidth = drwMeasure.RimWidth
            End If

            'DOT Serial Number
            If Not drwMeasure.IsDOTSerialNumberNull Then
                objMeasure.DotSerialNumber = drwMeasure.DOTSerialNumber
            End If

            Try
                If Not drwMeasure.IsSerialDateNull Then
                    objMeasure.SerialDate = ConvertToDateTime(drwMeasure.SerialDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            '(Outer Diameter, O.D.)
            If Not drwMeasure.IsDiameterNull Then
                objMeasure.Diameter = drwMeasure.Diameter
            End If

            'O.W.
            If Not drwMeasure.IsAvgOverallWidthNull Then
                objMeasure.AvgOverallWidth = drwMeasure.AvgOverallWidth
            End If

            'Average Width
            If Not drwMeasure.IsAvgSectionWidthNull Then
                objMeasure.AvgSectionWidth = drwMeasure.AvgSectionWidth
            End If

            'Max Overall Width
            If Not drwMeasure.IsMaxOverallWidthNull Then
                objMeasure.MaxOverallWidth = drwMeasure.MaxOverallWidth
            End If

            'jeseitz 8/11/15 added
            'Max Overall Diameter
            If Not drwMeasure.IsMAXOVERALLDIAMETERNull Then
                objMeasure.MaxOverallDiameter = drwMeasure.MAXOVERALLDIAMETER
            End If

            'jeseitz 8/11/15 added
            'Min Overall Diameter
            If Not drwMeasure.IsMINOVERALLDIAMETERNull Then
                objMeasure.MinOverallDiameter = drwMeasure.MINOVERALLDIAMETER
            End If

            'Minimum Size Factor
            If Not drwMeasure.IsMinSizeFactorNull Then
                objMeasure.MinSizeFactor = drwMeasure.MinSizeFactor
            End If

            Try
                '(Start Date, Start Time)
                If Not drwMeasure.IsMountTimeNull Then
                    objMeasure.MountTime = ConvertToDateTime(drwMeasure.MountTime)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            'Mount Temp
            If Not drwMeasure.IsMountTempNull Then
                objMeasure.MountTemp = drwMeasure.MountTemp
            End If

            Try
                If Not drwMeasure.IsEndTimeNull Then
                    objMeasure.EndTime = ConvertToDateTime(drwMeasure.EndTime)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            'Measure ID
            If Not drwMeasure.IsMea_IDNull Then
                objMeasure.MeasureId = drwMeasure.Mea_ID
            End If

            'Actual Size Factor
            If Not drwMeasure.IsActSizeFactorNull Then
                objMeasure.ActSizeFactor = drwMeasure.ActSizeFactor
            End If

            'Measure Detail
            For Each drwMeasureDetail In p_dtbTRACSMeasureDtl.Rows

                Dim objMeasureDetail As New MeasureDetail


                If Not drwMeasureDetail.IsMea_IDNull Then
                    objMeasureDetail.MeasureId = drwMeasureDetail.Mea_ID
                End If
                If Not drwMeasureDetail.IsIterationNull Then
                    objMeasureDetail.Iteration = drwMeasureDetail.Iteration
                End If
                If Not drwMeasureDetail.IsSectionWidthNull Then
                    objMeasureDetail.SectionWidth = drwMeasureDetail.SectionWidth
                End If
                If Not drwMeasureDetail.IsOverallWidthNull Then
                    objMeasureDetail.OverallWidth = drwMeasureDetail.OverallWidth
                End If

                objMeasure.MeasureDetails.Add(objMeasureDetail)

            Next

            'Conversion
            If p_intCertificateNumberID = 0 Then
                'Modified by offshore on 26NOV2012.
                '1 pound per square inch = 0.0689475729 bar

                objMeasure.InflationPressure = CShort(Math.Round(objMeasure.InflationPressure * 0.01, 1))
                objMeasure.RimWidth = CSng(Math.Round(objMeasure.RimWidth * 25.4))

            End If

            If Not drwMeasure.IsGTSPECNull Then
                objMeasure.GTSpecMaterialNumber = drwMeasure.GTSPEC.TrimStart(CChar(zero))
            End If

            Return objMeasure
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map ICS DataTable To Treadwear.
    ''' </summary>
    ''' <returns>Treadwear Object</returns> 
    ''' <param name="p_dtbICSTreadwearDtl">p_dtbICS Treadwear Dtl.</param>
    ''' <param name="p_dtbICSTreadwearHdr">p_dtbICS Treadwear Hdr</param>
    ''' <param name="p_iCertificationTypeId">The p_i certification type id.</param>
    ''' <param name="p_strMatlNum">The p_strMatlNum. </param>
    ''' <param name="p_iSKUId">The p_i SKU id.</param>
    ''' <param name="p_intCertificateNumberID">The P_STR certificate Id.</param>   
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapICSDataTableToTreadwear(ByVal p_dtbICSTreadwearHdr As ICSDataSet.TreadWearHdrDataTable, _
                                            ByVal p_dtbICSTreadwearDtl As ICSDataSet.TreadWearDtlDataTable, _
                                            ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                            ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As Treadwear
        Try
            Debug.WriteLine("MapICSDataTableToTreadwear")
            Dim objTreadwear As New Treadwear
            Dim dtbTRACSTreadwearHdr As New ICS.Datasets.TRACStoICSDataset.TreadWearHdrDataTable
            Dim dtbTRACSTreadwearDtl As New ICS.Datasets.TRACStoICSDataset.TreadWearDtlDataTable
            Dim drwTreadwearHdr As ICSDataSet.TreadWearHdrRow

            If p_dtbICSTreadwearHdr.Rows.Count = 0 Then
                Return objTreadwear
            End If

            dtbTRACSTreadwearHdr.Merge(p_dtbICSTreadwearHdr)
            dtbTRACSTreadwearDtl.Merge(p_dtbICSTreadwearDtl)

            objTreadwear = MapTRACSDataTableToTreadwear(dtbTRACSTreadwearHdr, dtbTRACSTreadwearDtl, p_iCertificationTypeId, p_strMatlNum, p_iSKUId, p_intCertificateNumberID)

            drwTreadwearHdr = CType(p_dtbICSTreadwearHdr.Rows(0), ICSDataSet.TreadWearHdrRow)

            If Not drwTreadwearHdr.IsINDICATORSREQUIREMENTNull Then
                objTreadwear.IndicatorRequirement = drwTreadwearHdr.INDICATORSREQUIREMENT
            End If

            If Not drwTreadwearHdr.IsPassYNNull Then
                If drwTreadwearHdr.PassYN.ToUpper = trueValue Then
                    objTreadwear.PassYN = True
                Else
                    objTreadwear.PassYN = False
                End If
            End If

            If Not drwTreadwearHdr.IsMATL_NUMNull Then
                objTreadwear.MaterialNumber = drwTreadwearHdr.MATL_NUM.TrimStart(CChar(zero))
            End If

            If Not drwTreadwearHdr.IsGTSPECNull Then
                objTreadwear.GTSpecMaterialNumber = drwTreadwearHdr.GTSPEC.TrimStart(CChar(zero))
            End If

            Return objTreadwear
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map TRACS DataTable To Treadwear.
    ''' </summary>
    ''' <returns>Treadwear object</returns> 
    ''' <param name="p_dtbTreadwearDtl">p_dtb Treadwear Dtl.</param>
    ''' <param name="p_dtbTreadwearHdr">p_dtb Treadwear Hdr</param>
    ''' <param name="p_iCertificationTypeId">The p_i certification type id.</param>
    ''' <param name="p_strMatlNum">The p_strMatlNum. </param>
    ''' <param name="p_iSKUId">The p_i SKU id.</param>
    ''' <param name="p_intCertificateNumberID">The P_STR certificate Id.</param>  
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRACSDataTableToTreadwear(ByVal p_dtbTreadwearHdr As ICS.Datasets.TRACStoICSDataset.TreadWearHdrDataTable, _
                                                ByVal p_dtbTreadwearDtl As ICS.Datasets.TRACStoICSDataset.TreadWearDtlDataTable, _
                                                ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                                ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As Treadwear
        Try
            Dim objTreadwear As New Treadwear()
            Dim drwTreadwear As ICS.Datasets.TRACStoICSDataset.TreadWearHdrRow
            Dim drwTreadwearDetail As ICS.Datasets.TRACStoICSDataset.TreadWearDtlRow

            objTreadwear.CertificationTypeId = p_iCertificationTypeId
            objTreadwear.SkuId = p_iSKUId
            objTreadwear.CertificateNumberID = p_intCertificateNumberID

            If p_dtbTreadwearHdr.Rows.Count = 0 Then
                Return objTreadwear
            End If

            drwTreadwear = CType(p_dtbTreadwearHdr.Rows(0), Datasets.TRACStoICSDataset.TreadWearHdrRow)

            objTreadwear.ProjectNumber = drwTreadwear.ProjectNum
            objTreadwear.TireNumber = drwTreadwear.TireNum
            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not drwTreadwear.IsOperationNull Then
                objTreadwear.Operation = drwTreadwear.Operation
            End If
            objTreadwear.TestSpec = CStr(IIf(drwTreadwear.IsTestSpecNull, String.Empty, drwTreadwear.TestSpec))

            If Not drwTreadwear.IsTestSKUNull Then
                objTreadwear.MaterialNumber = drwTreadwear.TestSKU
            End If

            Try
                If Not drwTreadwear.IsCompletionDateNull Then
                    objTreadwear.CompletionDate = CDate(drwTreadwear.CompletionDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwTreadwear.IsDOTSerialNumberNull Then
                objTreadwear.DotSerialNumber = drwTreadwear.DOTSerialNumber
            End If

            Try
                If Not drwTreadwear.IsSerialDateNull Then
                    objTreadwear.SerialDate = ConvertToDateTime(drwTreadwear.SerialDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwTreadwear.IsLowestWearbarNull Then
                objTreadwear.LowestWearBar = drwTreadwear.LowestWearbar
            End If

            If Not drwTreadwear.IsPassYNNull Then
                objTreadwear.PassYN = CBool(IIf(drwTreadwear.PassYN.ToLower() = trueValue, True, False))
            End If

            If Not drwTreadwear.IsTW_IDNull Then
                objTreadwear.TreadWearId = drwTreadwear.TW_ID
            End If

            If Not drwTreadwear.IsGTSPECNull Then
                objTreadwear.GTSpecMaterialNumber = drwTreadwear.GTSPEC.TrimStart(CChar(zero))
            End If

            'Treadwear Detail
            For Each drwTreadwearDetail In p_dtbTreadwearDtl.Rows

                Dim objTreadwearDetail As New TreadwearDetail

                If Not drwTreadwearDetail.IsTW_IDNull Then
                    objTreadwearDetail.TreadWearId = drwTreadwearDetail.TW_ID
                End If

                If Not drwTreadwearDetail.IsITERATIONNull Then
                    objTreadwearDetail.Iteration = drwTreadwearDetail.ITERATION
                End If

                If Not drwTreadwearDetail.IsWearbarHeightNull Then
                    objTreadwearDetail.WearBarHeight = drwTreadwearDetail.WearbarHeight
                End If

                objTreadwear.TreadwearDetails.Add(objTreadwearDetail)

            Next

            'Conversion
            If p_intCertificateNumberID = 0 Then
                If objTreadwear.CertificationTypeId <> Depository.Current.GetCertificationTypeID(certificationName) Then
                    For Each objDetail As TreadwearDetail In objTreadwear.TreadwearDetails
                        objDetail.WearBarHeight = CSng(Math.Round(objDetail.WearBarHeight * 25.4))
                    Next
                Else
                    For Each objDetail As TreadwearDetail In objTreadwear.TreadwearDetails
                        objDetail.WearBarHeight = CSng(Math.Round(objDetail.WearBarHeight * 25.4, 1))
                    Next
                End If
            End If

            Return objTreadwear
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map ICS DataTable To Plunger.
    ''' </summary>
    ''' <returns>Plunger object</returns> 
    ''' <param name="p_dtbICSPlungerDtl">p_dtb ICS Plunger Dtl</param>
    ''' <param name="p_dtbICSPlungerHdr">p_dtb ICS Plunger Hdr</param>
    ''' <param name="p_iCertificationTypeId">The p_i certification type id.</param>
    ''' <param name="p_strMatlNum">The p_strMatlNum. </param>
    ''' <param name="p_iSKUId">The p_i SKU id.</param>
    ''' <param name="p_intCertificateNumberID">The P_STR certificate Id.</param> 
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapICSDataTableToPlunger(ByVal p_dtbICSPlungerHdr As ICSDataSet.PlungerHdrDataTable, _
                                            ByVal p_dtbICSPlungerDtl As ICSDataSet.PlungerDtlDataTable, _
                                            ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                            ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As Plunger
        Try
            Debug.WriteLine("MapICSDataTableToPlunger")
            Dim objPlunger As New Plunger
            Dim dtbTRACSPlungerHdr As New ICS.Datasets.TRACStoICSDataset.PlungerHdrDataTable
            Dim dtbTRACSPlungerDtl As New ICS.Datasets.TRACStoICSDataset.PlungerDtlDataTable
            Dim drwPlungerHdr As ICSDataSet.PlungerHdrRow

            If p_dtbICSPlungerHdr.Rows.Count = 0 Then
                Return objPlunger
            End If

            Dim dst As New ICS.Datasets.TRACStoICSDataset
            dst.EnforceConstraints = False

            dtbTRACSPlungerHdr = dst.PlungerHdr
            dtbTRACSPlungerHdr.Merge(p_dtbICSPlungerHdr)
            dtbTRACSPlungerDtl.Merge(p_dtbICSPlungerDtl)

            objPlunger = MapTRACSDataTableToPlunger(dtbTRACSPlungerHdr, dtbTRACSPlungerDtl, p_iCertificationTypeId, p_strMatlNum, p_iSKUId, p_intCertificateNumberID)

            drwPlungerHdr = CType(p_dtbICSPlungerHdr.Rows(0), ICSDataSet.PlungerHdrRow)

            If Not drwPlungerHdr.IsMATL_NUMNull Then
                objPlunger.MaterialNumber = drwPlungerHdr.MATL_NUM.TrimStart(CChar(zero))
            End If

            If Not drwPlungerHdr.IsGTSPECNull Then
                objPlunger.GTSpecMaterialNumber = drwPlungerHdr.GTSPEC.TrimStart(CChar(zero))
            End If

            Return objPlunger
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Defect 10034: Modified by offshore on 26NOV2012. Commented out all metric conversion statements.
    ''' <summary>
    '''  Method to Gets the product country status table.
    ''' </summary>
    ''' <returns>Plunger Object</returns> 
    ''' <param name="p_dtbPlungerDtl">p_dtb Plunger Dtl</param>
    ''' <param name="p_dtbPlungerHdr">p_dtb Plunger Hdr</param>
    ''' <param name="p_iCertificationTypeId">The p_i certification type id.</param>
    ''' <param name="p_strMatlNum">The p_strMatlNum. </param>
    ''' <param name="p_iSKUId">The p_i SKU id.</param>
    ''' <param name="p_intCertificateNumberID">The P_STR certificate Id.</param>    
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRACSDataTableToPlunger(ByVal p_dtbPlungerHdr As ICS.Datasets.TRACStoICSDataset.PlungerHdrDataTable, _
                                            ByVal p_dtbPlungerDtl As ICS.Datasets.TRACStoICSDataset.PlungerDtlDataTable, _
                                            ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                            ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As Plunger
        Try
            Dim objPlunger As New Plunger()
            Dim drwPlunger As ICS.Datasets.TRACStoICSDataset.PlungerHdrRow
            Dim drwPlungerDetail As ICS.Datasets.TRACStoICSDataset.PlungerDtlRow

            objPlunger.CertificationTypeId = p_iCertificationTypeId
            objPlunger.SkuId = p_iSKUId
            objPlunger.CertificateNumberID = p_intCertificateNumberID

            If p_dtbPlungerHdr.Rows.Count = 0 Then
                Return objPlunger
            End If

            drwPlunger = CType(p_dtbPlungerHdr.Rows(0), Datasets.TRACStoICSDataset.PlungerHdrRow)

            objPlunger.ProjectNumber = drwPlunger.ProjectNum
            objPlunger.TireNumber = drwPlunger.TireNum
            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not drwPlunger.IsOperationNull Then
                objPlunger.Operation = drwPlunger.Operation
            End If
            objPlunger.TestSpec = CStr(IIf(drwPlunger.IsTestSpecNull, String.Empty, drwPlunger.TestSpec))

            If Not drwPlunger.IsTestSKUNull Then
                objPlunger.MaterialNumber = drwPlunger.TestSKU
            End If

            Try
                If Not drwPlunger.IsCompletionDateNull Then
                    objPlunger.CompletionDate = CDate(drwPlunger.CompletionDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwPlunger.IsDOTSerialNumberNull Then
                objPlunger.DotSerialNumber = drwPlunger.DOTSerialNumber
            End If

            Try
                If Not drwPlunger.IsSerialDateNull Then
                    objPlunger.SerialDate = ConvertToDateTime(drwPlunger.SerialDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwPlunger.IsAvgBreakingEnergyNull Then
                objPlunger.AVGBreakingEnergy = drwPlunger.AvgBreakingEnergy
            End If

            If Not drwPlunger.IsPassYNNull Then
                objPlunger.PassYN = CBool(IIf(drwPlunger.PassYN.ToLower() = trueValue, True, False))
            End If

            If Not drwPlunger.IsMinPlungerNull Then
                objPlunger.MinPlunger = drwPlunger.MinPlunger
            End If

            If Not drwPlunger.IsPLG_IDNull Then
                objPlunger.PlungerId = drwPlunger.PLG_ID
            End If

            'Plunger Detail
            For Each drwPlungerDetail In p_dtbPlungerDtl.Rows

                Dim objPlungerDetail As New PlungerDetail

                If Not drwPlungerDetail.IsPLG_IDNull Then
                    objPlungerDetail.PlungerId = drwPlungerDetail.PLG_ID
                End If

                If Not drwPlungerDetail.IsITERATIONNull Then
                    objPlungerDetail.Iteration = drwPlungerDetail.ITERATION
                End If

                If Not drwPlungerDetail.IsBreakingEnergyNull Then
                    objPlungerDetail.BreakingEnergy = drwPlungerDetail.BreakingEnergy
                End If

                objPlunger.PlungerDetails.Add(objPlungerDetail)

            Next

            If Not drwPlunger.IsGTSPECNull Then
                objPlunger.GTSpecMaterialNumber = drwPlunger.GTSPEC.TrimStart(CChar(zero))
            End If

            Return objPlunger
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map ICS DataTable To Bead UnSeat.
    ''' </summary>
    ''' <returns>BeadUnSeat Object</returns> 
    ''' <param name="p_dtbICSBeadUnSeatDtl">p_dtb ICS Bead UnSeatDtl</param>
    ''' <param name="p_dtbICSBeadUnSeatHdr">p_dtb ICS Plunger Hdr</param>
    ''' <param name="p_iCertificationTypeId">The p_i certification type id.</param>
    ''' <param name="p_strMatlNum">The p_strMatlNum. </param>
    ''' <param name="p_iSKUId">The p_i SKU id.</param>
    ''' <param name="p_intCertificateNumberID">The P_STR certificate Id.</param>    
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapICSDataTableToBeadUnSeat(ByVal p_dtbICSBeadUnSeatHdr As ICSDataSet.BeadUnseatHdrDataTable, _
                                                ByVal p_dtbICSBeadUnSeatDtl As ICSDataSet.BeadUnseatDtlDataTable, _
                                                ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                                ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As BeadUnSeat
        Try
            Debug.WriteLine("MapICSDataTableToBeadUnSeat")
            Dim objBeadUnSeat As New BeadUnSeat
            Dim dtbTRACSBeadUnSeatHdr As New ICS.Datasets.TRACStoICSDataset.BeadUnseatHdrDataTable
            Dim dtbTRACSBeadUnSeatDtl As New ICS.Datasets.TRACStoICSDataset.BeadUnseatDtlDataTable
            Dim drwBeadUnSeatHdr As ICSDataSet.BeadUnseatHdrRow

            If p_dtbICSBeadUnSeatHdr.Rows.Count = 0 Then
                Return objBeadUnSeat
            End If

            dtbTRACSBeadUnSeatHdr.Merge(p_dtbICSBeadUnSeatHdr)
            dtbTRACSBeadUnSeatDtl.Merge(p_dtbICSBeadUnSeatDtl)

            objBeadUnSeat = MapTRACSDataTableToBeadUnSeat(dtbTRACSBeadUnSeatHdr, dtbTRACSBeadUnSeatDtl, p_iCertificationTypeId, p_strMatlNum, p_iSKUId, p_intCertificateNumberID)

            drwBeadUnSeatHdr = CType(p_dtbICSBeadUnSeatHdr.Rows(0), ICSDataSet.BeadUnseatHdrRow)

            If Not drwBeadUnSeatHdr.IsTestPassFailNull Then
                objBeadUnSeat.TestPassFail = CBool(IIf(drwBeadUnSeatHdr.TestPassFail = trueValue, True, False))
            End If

            If Not drwBeadUnSeatHdr.IsMATL_NUMNull Then
                objBeadUnSeat.MaterialNumber = drwBeadUnSeatHdr.MATL_NUM.TrimStart(CChar(zero))
            End If

            If Not drwBeadUnSeatHdr.IsGTSPECNull Then
                objBeadUnSeat.GTSpecMaterialNumber = drwBeadUnSeatHdr.GTSPEC.TrimStart(CChar(zero))
            End If

            Return objBeadUnSeat
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map TRACS DataTable To Bead UnSeat.
    ''' </summary>
    ''' <returns>BeadUnSeat Object</returns> 
    ''' <param name="p_dtbBeadUnSeatDtl">p_dtb Bead UnSeat Dtl</param>
    ''' <param name="p_dtbBeadUnSeatHdr">p_dtb Bead UnSeat Hdr</param>
    ''' <param name="p_iCertificationTypeId">The p_i certification type id.</param>
    ''' <param name="p_strMatlNum">The p_strMatlNum. </param>
    ''' <param name="p_iSKUId">The p_i SKU id.</param>
    ''' <param name="p_intCertificateNumberID">The P_STR certificate Id.</param>    
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item>  
    ''' </list>
    ''' </remarks>
    Private Function MapTRACSDataTableToBeadUnSeat(ByVal p_dtbBeadUnSeatHdr As ICS.Datasets.TRACStoICSDataset.BeadUnseatHdrDataTable, _
                                                ByVal p_dtbBeadUnSeatDtl As ICS.Datasets.TRACStoICSDataset.BeadUnseatDtlDataTable, _
                                                ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                                ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As BeadUnSeat
        Try
            Dim objBeadUnSeat As New BeadUnSeat()
            Dim drwBeadUnSeat As ICS.Datasets.TRACStoICSDataset.BeadUnseatHdrRow
            Dim drwBeadUnSeatDetail As ICS.Datasets.TRACStoICSDataset.BeadUnseatDtlRow

            objBeadUnSeat.CertificationTypeId = p_iCertificationTypeId
            objBeadUnSeat.SkuId = p_iSKUId
            objBeadUnSeat.CertificateNumberID = p_intCertificateNumberID

            If p_dtbBeadUnSeatHdr.Rows.Count = 0 Then
                Return objBeadUnSeat
            End If

            drwBeadUnSeat = CType(p_dtbBeadUnSeatHdr.Rows(0), Datasets.TRACStoICSDataset.BeadUnseatHdrRow)

            objBeadUnSeat.ProjectNumber = drwBeadUnSeat.ProjectNum
            objBeadUnSeat.TireNumber = drwBeadUnSeat.TireNum
            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not drwBeadUnSeat.IsOperationNull Then
                objBeadUnSeat.Operation = drwBeadUnSeat.Operation
            End If
            objBeadUnSeat.TestSpec = CStr(IIf(drwBeadUnSeat.IsTestSpecNull, String.Empty, drwBeadUnSeat.TestSpec))

            If Not drwBeadUnSeat.IsTestSKUNull Then
                objBeadUnSeat.MaterialNumber = drwBeadUnSeat.TestSKU
            End If

            Try
                If Not drwBeadUnSeat.IsCompletionDateNull Then
                    objBeadUnSeat.CompletionDate = CDate(drwBeadUnSeat.CompletionDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwBeadUnSeat.IsDOTSerialNumberNull Then
                objBeadUnSeat.DotSerialNumber = drwBeadUnSeat.DOTSerialNumber
            End If

            Try
                If Not drwBeadUnSeat.IsSerialDateNull Then
                    objBeadUnSeat.SerialDate = ConvertToDateTime(drwBeadUnSeat.SerialDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwBeadUnSeat.IsLowestUnseatValueNull Then
                objBeadUnSeat.LowestUnSeatValue = drwBeadUnSeat.LowestUnseatValue
            End If

            If Not drwBeadUnSeat.IsPassYNNull Then
                objBeadUnSeat.PassYN = CBool(IIf(drwBeadUnSeat.PassYN.ToLower() = trueValue, True, False))
                objBeadUnSeat.TestPassFail = CBool(IIf(drwBeadUnSeat.PassYN.ToLower() = trueValue, True, False)) 'Not used so just set the same.
            End If

            If Not drwBeadUnSeat.IsBU_IDNull Then
                objBeadUnSeat.BeadUnSeatId = drwBeadUnSeat.BU_ID
            End If

            If Not drwBeadUnSeat.IsMinBeadUnseatNull Then
                objBeadUnSeat.MinBeadUnseat = drwBeadUnSeat.MinBeadUnseat
            End If

            'BeadUnSeat Detail
            For Each drwBeadUnSeatDetail In p_dtbBeadUnSeatDtl.Rows

                Dim objBeadUnSeatDetail As New BeadUnSeatDetail

                If Not drwBeadUnSeatDetail.IsBU_IDNull Then
                    objBeadUnSeatDetail.BeadUnSeatId = drwBeadUnSeatDetail.BU_ID
                End If

                If Not drwBeadUnSeatDetail.IsITERATIONNull Then
                    objBeadUnSeatDetail.Iteration = drwBeadUnSeatDetail.ITERATION
                End If

                If Not drwBeadUnSeatDetail.IsUnseatForceNull Then
                    objBeadUnSeatDetail.UnSeatForce = drwBeadUnSeatDetail.UnseatForce
                End If

                objBeadUnSeat.BeadUnSeatDetails.Add(objBeadUnSeatDetail)

            Next

            'Conversion
            If p_intCertificateNumberID = 0 Then
                'convert pounds-force to Newton

            End If

            If Not drwBeadUnSeat.IsGTSPECNull Then
                objBeadUnSeat.GTSpecMaterialNumber = drwBeadUnSeat.GTSPEC.TrimStart(CChar(zero))
            End If

            Return objBeadUnSeat
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map Data From ICS Database Endurance Header And Detail Tables To Endurance Entity .
    ''' </summary>
    ''' <returns>Endurance Object</returns> 
    ''' <param name="p_dtbICSEnduranceHdr"></param>
    ''' <param name="p_dtbICSEnduranceDtl"></param>
    ''' <param name="p_iCertificationTypeId"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_iSKUId"></param>
    ''' <param name="p_intCertificateNumberID"></param> 
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Private Function MapICSDataTableToEndurance(ByVal p_dtbICSEnduranceHdr As ICSDataSet.EnduranceHdrDataTable, ByVal p_dtbICSEnduranceDtl As ICSDataSet.EnduranceDtlDataTable, ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As Endurance
        Try
            Dim objEndurance As New Endurance
            Dim dtbTRACSEnduranceHdr As New ICS.Datasets.TRACStoICSDataset.EnduranceHdrDataTable
            Dim dtbTRACSEnduranceDtl As New ICS.Datasets.TRACStoICSDataset.EnduranceDtlDataTable
            Dim drwEnduranceHdr As ICSDataSet.EnduranceHdrRow

            If p_dtbICSEnduranceHdr.Rows.Count = 0 Then
                Return objEndurance
            End If

            dtbTRACSEnduranceHdr.Merge(p_dtbICSEnduranceHdr)
            dtbTRACSEnduranceDtl.Merge(p_dtbICSEnduranceDtl)

            objEndurance = MapTRACSDataTableToEndurance(dtbTRACSEnduranceHdr, dtbTRACSEnduranceDtl, p_iCertificationTypeId, p_strMatlNum, p_iSKUId, p_intCertificateNumberID)

            drwEnduranceHdr = CType(p_dtbICSEnduranceHdr.Rows(0), ICSDataSet.EnduranceHdrRow)

            If Not drwEnduranceHdr.IsMATL_NUMNull Then
                objEndurance.MaterialNumber = drwEnduranceHdr.MATL_NUM.TrimStart(CChar(zero))
            End If

            If Not drwEnduranceHdr.IsDIAMETERTESTDRUMNull Then
                objEndurance.DiameterTestDrum = drwEnduranceHdr.DIAMETERTESTDRUM
            End If

            If Not drwEnduranceHdr.IsPreCondTimeNull Then
                objEndurance.PrecondTime = drwEnduranceHdr.PreCondTime
            End If

            If Not drwEnduranceHdr.IsPRECONDTEMPNull Then
                objEndurance.PrecondStartTemp = CShort(drwEnduranceHdr.PRECONDTEMP)
            End If
            If Not drwEnduranceHdr.IsINFLATIONPRESSUREREADJUSTEDNull Then
                objEndurance.InflationPressureReadjusted = CShort(drwEnduranceHdr.INFLATIONPRESSUREREADJUSTED)
            End If
            If Not drwEnduranceHdr.IsCIRCUNFERENCEBEFORETESTNull Then
                objEndurance.CircumferenceBeforeTesting = drwEnduranceHdr.CIRCUNFERENCEBEFORETEST
            End If
            If Not drwEnduranceHdr.IsENDURANCEHOURSNull Then
                objEndurance.EnduranceXHour = drwEnduranceHdr.ENDURANCEHOURS
            End If
            If Not drwEnduranceHdr.IsRESULTPASSFAILNull Then
                objEndurance.ResultPassFail = CBool(IIf(drwEnduranceHdr.RESULTPASSFAIL = trueValue, True, False))
            End If
            If Not drwEnduranceHdr.IsPassYNNull Then
                objEndurance.PassYN = CBool(IIf(drwEnduranceHdr.PassYN = trueValue, True, False))
                objEndurance.EnduranceTestPassYN = CBool(IIf(drwEnduranceHdr.PassYN = trueValue, True, False))
            End If
            If Not drwEnduranceHdr.IsPOSSIBLEFAILURESFOUNDNull Then
                objEndurance.PossibleFailuresFound = drwEnduranceHdr.POSSIBLEFAILURESFOUND
            End If

            If Not drwEnduranceHdr.IsPostCondTimeNull Then
                objEndurance.PostcondTime = drwEnduranceHdr.PostCondTime
            End If
            If Not drwEnduranceHdr.IsCIRCUNFERENCEAFTERTESTNull Then
                objEndurance.CircumferenceAfterTesting = drwEnduranceHdr.CIRCUNFERENCEAFTERTEST
            End If
            If Not drwEnduranceHdr.IsOUTERDIAMETERDIFERENCENull Then
                objEndurance.OuterDiameterDifference = drwEnduranceHdr.OUTERDIAMETERDIFERENCE
            End If
            If Not drwEnduranceHdr.IsODDIFERENCETOLERANCENull Then
                objEndurance.OuterDiameterTolerance = drwEnduranceHdr.ODDIFERENCETOLERANCE
            End If
            If Not drwEnduranceHdr.IsSERIENOMNull Then
                objEndurance.SerieNOM = drwEnduranceHdr.SERIENOM
            End If
            If Not drwEnduranceHdr.IsFINALJUDGEMENTNull Then
                objEndurance.FinalJudgement = CBool(IIf(drwEnduranceHdr.FINALJUDGEMENT = trueValue, True, False))
            End If
            If Not drwEnduranceHdr.IsAPPROVERNull Then
                objEndurance.Approver = drwEnduranceHdr.APPROVER
            End If
            If Not drwEnduranceHdr.IsBeforeInflationNull Then
                objEndurance.BeforeInflation = drwEnduranceHdr.BeforeInflation
            End If
            If Not drwEnduranceHdr.IsAfterInflationNull Then
                objEndurance.AfterInflation = drwEnduranceHdr.AfterInflation
            End If
            If Not drwEnduranceHdr.IsInflationPressureNull Then
                objEndurance.InflationPressure = drwEnduranceHdr.InflationPressure
            End If

            'Rim Width
            If Not drwEnduranceHdr.IsRimWidthNull Then
                objEndurance.RimWidth = drwEnduranceHdr.RimWidth
            End If

            'Rim Diameter
            If Not drwEnduranceHdr.IsRimDiameterNull Then
                objEndurance.RimDiameter = drwEnduranceHdr.RimDiameter
            End If

            'Low Pressure Start Inflation
            If Not drwEnduranceHdr.IsLOWPRESSURESTARTINFLATIONNull Then
                objEndurance.LowInfStartInflation = CSng(drwEnduranceHdr.LOWPRESSURESTARTINFLATION)
            End If

            'Low Pressure End Inflation
            If Not drwEnduranceHdr.IsLOWPRESSUREENDINFLATIONNull Then
                objEndurance.LowInfEndInflation = CSng(drwEnduranceHdr.LOWPRESSUREENDINFLATION)
            End If

            'Low Pressure End Temperature
            If Not drwEnduranceHdr.IsLOWPRESSUREENDTEMPNull Then
                objEndurance.LowInfEndTemp = drwEnduranceHdr.LOWPRESSUREENDTEMP
            End If

            If Not drwEnduranceHdr.IsGTSPECNull Then
                objEndurance.GTSpecMaterialNumber = drwEnduranceHdr.GTSPEC.TrimStart(CChar(zero))
            End If

            Return objEndurance
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Defect 10034: Modified by offshore on 26NOV2012. Commented out all metric conversion statements.
    ''' <summary>
    '''  Method to Map TRACS DataTable To Endurance.
    ''' </summary>
    ''' <returns>Endurance Object</returns> 
    ''' <param name="p_dtbEnduranceDtl">p_dtb Endurance Dtl</param>
    ''' <param name="p_dtbEnduranceHdr">p_dtb Endurance Hdr</param>
    ''' <param name="p_iCertificationTypeId">The p_i certification type id.</param>
    ''' <param name="p_strMatlNum">The p_strMatlNum. </param>
    ''' <param name="p_iSKUId">The p_i SKU id.</param>
    ''' <param name="p_intCertificateNumberID">The P_STR certificate Id.</param>    
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRACSDataTableToEndurance(ByVal p_dtbEnduranceHdr As ICS.Datasets.TRACStoICSDataset.EnduranceHdrDataTable, _
                                                ByVal p_dtbEnduranceDtl As ICS.Datasets.TRACStoICSDataset.EnduranceDtlDataTable, _
                                                ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                                ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As Endurance
        Try
            Dim objEndurance As New Endurance
            Dim drwEndurance As ICS.Datasets.TRACStoICSDataset.EnduranceHdrRow
            Dim drwEnduranceDetail As ICS.Datasets.TRACStoICSDataset.EnduranceDtlRow

            objEndurance.CertificationTypeId = p_iCertificationTypeId
            objEndurance.SKUID = p_iSKUId
            objEndurance.CertificateNumberID = p_intCertificateNumberID

            If p_dtbEnduranceHdr.Rows.Count = 0 Then
                Return objEndurance
            End If

            drwEndurance = CType(p_dtbEnduranceHdr.Rows(0), Datasets.TRACStoICSDataset.EnduranceHdrRow)

            If Not drwEndurance.IsProjectNumNull Then
                objEndurance.ProjectNumber = drwEndurance.ProjectNum
            End If

            If Not drwEndurance.IsTireNumNull Then
                objEndurance.TireNumber = drwEndurance.TireNum
            End If

            If Not drwEndurance.IsTestSpecNull Then
                objEndurance.TestSpec = drwEndurance.TestSpec
            End If

            'Added Operation  as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not drwEndurance.IsOperationNull Then
                objEndurance.Operation = drwEndurance.Operation
            End If


            If Not drwEndurance.IsTestSKUNull Then
                objEndurance.MaterialNumber = drwEndurance.TestSKU
            End If

            If Not drwEndurance.IsTestSpecNull Then
                objEndurance.GTSpecMaterialNumber = drwEndurance.TestSpec
            End If

            Try
                If Not drwEndurance.IsCompletionDateNull Then
                    objEndurance.CompletionDate = ConvertToDateTime(drwEndurance.CompletionDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwEndurance.IsDOTSerialNumberNull Then
                objEndurance.DotSerialNumber = drwEndurance.DOTSerialNumber
            End If

            Try
                If Not drwEndurance.IsSerialDateNull Then
                    objEndurance.SerialDate = ConvertToDateTime(drwEndurance.SerialDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            Try
                If Not drwEndurance.IsPreCondStartDateNull Then
                    objEndurance.PrecondStartDate = ConvertToDateTime(drwEndurance.PreCondStartDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwEndurance.IsPreCondSartTempNull Then
                objEndurance.PrecondStartTemp = drwEndurance.PreCondSartTemp
            End If

            If Not drwEndurance.IsPreCondTimeNull Then
                objEndurance.PrecondTime = drwEndurance.PreCondTime
            End If

            If Not drwEndurance.IsRimDiameterNull Then
                objEndurance.RimDiameter = drwEndurance.RimDiameter
            End If

            If Not drwEndurance.IsRimWidthNull Then
                objEndurance.RimWidth = drwEndurance.RimWidth
            End If

            Try
                If Not drwEndurance.IsPreCondEndDateNull Then
                    objEndurance.PrecondEndDate = ConvertToDateTime(drwEndurance.PreCondEndDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwEndurance.IsPreCondEndTempNull Then
                objEndurance.PrecondEndTemp = drwEndurance.PreCondEndTemp
            End If

            If Not drwEndurance.IsInflationPressureNull Then
                objEndurance.InflationPressure = drwEndurance.InflationPressure
            End If

            If Not drwEndurance.IsBeforeDiameterNull Then
                objEndurance.BeforeDiameter = drwEndurance.BeforeDiameter
            End If

            If Not drwEndurance.IsAfterDiameterNull Then
                objEndurance.AfterDiameter = drwEndurance.AfterDiameter
            End If

            If Not drwEndurance.IsBeforeInflationNull Then
                objEndurance.BeforeInflation = drwEndurance.BeforeInflation
            End If

            If Not drwEndurance.IsAfterInflationNull Then
                objEndurance.AfterInflation = drwEndurance.AfterInflation
            End If

            If Not drwEndurance.IsWheelPositionNull Then
                objEndurance.WheelPosition = drwEndurance.WheelPosition
            End If

            If Not drwEndurance.IsWheelNumNull Then
                objEndurance.WheelNumber = drwEndurance.WheelNum
            End If

            If Not drwEndurance.IsFinalTempNull Then
                objEndurance.FinalTemp = drwEndurance.FinalTemp
            End If

            If Not drwEndurance.IsFinalDistanceNull Then
                objEndurance.FinalDistance = drwEndurance.FinalDistance
            End If

            If Not drwEndurance.IsFinalInflationNull Then
                objEndurance.FinalInflation = drwEndurance.FinalInflation
            End If

            Try
                If Not drwEndurance.IsPostCondStartDateNull Then
                    objEndurance.PostcondStartDate = ConvertToDateTime(drwEndurance.PostCondStartDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            Try
                If Not drwEndurance.IsPostCondEndDateNull Then
                    objEndurance.PostcondEndDate = ConvertToDateTime(drwEndurance.PostCondEndDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwEndurance.IsPostCondEndTempNull Then
                objEndurance.PostcondEndTemp = drwEndurance.PostCondEndTemp
            End If

            If Not drwEndurance.IsPostCondTimeNull Then
                objEndurance.PostcondTime = drwEndurance.PostCondTime
            End If

            If Not drwEndurance.IsPassYNNull Then
                objEndurance.EnduranceTestPassYN = CBool(IIf(drwEndurance.PassYN.ToLower() = trueValue, True, False))
                objEndurance.ResultPassFail = CBool(IIf(drwEndurance.PassYN.ToLower() = trueValue, True, False))
            End If

            If Not drwEndurance.IsEND_IDNull Then
                objEndurance.END_ID = drwEndurance.END_ID
            End If

            'Low Pressure Start Inflation
            If Not drwEndurance.IsLowInfStartInflationNull Then
                If drwEndurance.LowInfStartInflation = -1 Then
                    objEndurance.LowInfStartInflation = 0
                Else
                    objEndurance.LowInfStartInflation = drwEndurance.LowInfStartInflation
                End If
            End If

            'Low Pressure End Inflation
            If Not drwEndurance.IsLowInfEndInflationNull Then
                If drwEndurance.LowInfEndInflation = -1 Then
                    objEndurance.LowInfEndInflation = 0
                Else
                    objEndurance.LowInfEndInflation = drwEndurance.LowInfEndInflation
                End If
            End If

            'Low Pressure End Temperature
            If Not drwEndurance.IsLowInfEndTempNull Then
                objEndurance.LowInfEndTemp = drwEndurance.LowInfEndTemp
            End If

            'Endurance Detail
            Dim Iteration As Integer = 0
            For Each drwEnduranceDetail In p_dtbEnduranceDtl.Rows

                Dim objEnduranceDetail As New EnduranceDetail

                If Not drwEnduranceDetail.IsEND_IDNull Then
                    objEnduranceDetail.END_ID = drwEnduranceDetail.END_ID
                End If

                objEnduranceDetail.Iteration = Iteration

                If Not drwEnduranceDetail.IsTestStepNull Then
                    objEnduranceDetail.TestStep = drwEnduranceDetail.TestStep
                End If

                If Not drwEnduranceDetail.IsTimeInMinNull Then
                    objEnduranceDetail.TimeInMin = drwEnduranceDetail.TimeInMin
                End If

                If Not drwEnduranceDetail.IsSpeedNull Then
                    objEnduranceDetail.Speed = drwEnduranceDetail.Speed
                End If

                If Not drwEnduranceDetail.IsTotMilesNull Then
                    objEnduranceDetail.TotMiles = drwEnduranceDetail.TotMiles
                End If

                If Not drwEnduranceDetail.IsLoadNull Then
                    objEnduranceDetail.Load = drwEnduranceDetail.Load
                End If

                If Not drwEnduranceDetail.IsLoadPercentNull Then
                    objEnduranceDetail.LoadPercent = drwEnduranceDetail.LoadPercent
                End If

                If Not drwEnduranceDetail.IsSetInflationNull Then
                    objEnduranceDetail.SetInflation = drwEnduranceDetail.SetInflation
                End If

                If Not drwEnduranceDetail.IsAmbTempNull Then
                    objEnduranceDetail.AmbTemp = drwEnduranceDetail.AmbTemp
                Else
                    objEnduranceDetail.AmbTemp = -1
                End If

                If Not drwEnduranceDetail.IsInfPressureNull Then
                    objEnduranceDetail.InfPressure = drwEnduranceDetail.InfPressure
                End If

                Try
                    If Not drwEnduranceDetail.IsStepCompletionDateNull Then
                        objEnduranceDetail.StepCompletionDate = CDate(drwEnduranceDetail.StepCompletionDate)
                    End If
                Catch exc As Exception
                    ' Leave default value
                End Try

                objEndurance.EnduranceDetails.Add(objEnduranceDetail)

                Iteration = Iteration + 1

            Next

            'Conversion and Approved Substitutions Section
            If p_intCertificateNumberID = 0 Then
                If objEndurance.CertificationTypeId <> Depository.Current.GetCertificationTypeID(certificationName) Then

                    If objEndurance.CertificationTypeId = Depository.Current.GetCertificationTypeID(certificationTypeName1) Or objEndurance.CertificationTypeId = Depository.Current.GetCertificationTypeID(certificationTypeName2) Then
                        'Approved Substitutions
                        objEndurance.InflationPressure = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objEndurance.CertificationTypeId, substitutionField1, objEndurance.InflationPressure, objEndurance.SKUID)))
                        objEndurance.FinalInflation = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objEndurance.CertificationTypeId, substitutionField1, objEndurance.FinalInflation, objEndurance.SKUID)))
                        objEndurance.AfterInflation = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objEndurance.CertificationTypeId, substitutionField1, objEndurance.AfterInflation, objEndurance.SKUID)))
                        objEndurance.BeforeInflation = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objEndurance.CertificationTypeId, substitutionField1, objEndurance.BeforeInflation, objEndurance.SKUID)))
                        objEndurance.LowInfStartInflation = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objEndurance.CertificationTypeId, substitutionField1, objEndurance.LowInfStartInflation, objEndurance.SKUID)))
                        objEndurance.LowInfEndInflation = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objEndurance.CertificationTypeId, substitutionField1, objEndurance.LowInfEndInflation, objEndurance.SKUID)))
                    End If
                Else

                    objEndurance.AfterInflation = CSng(Math.Round(objEndurance.AfterInflation * 0.01, 1))
                    objEndurance.BeforeInflation = CSng(Math.Round(objEndurance.BeforeInflation * 0.01, 1))
                    objEndurance.FinalInflation = CSng(Math.Round(objEndurance.FinalInflation * 0.01, 1))
                    objEndurance.InflationPressure = CSng(Math.Round(objEndurance.InflationPressure * 0.01, 1))
                    objEndurance.LowInfStartInflation = CSng(Math.Round(objEndurance.LowInfStartInflation * 0.01, 1))
                    objEndurance.LowInfEndInflation = CSng(Math.Round(objEndurance.LowInfEndInflation * 0.01, 1))
                End If

                'Modified by offshore on 26NOV2012.
                ''NOTE: Not consistent to Product (use inch)
                objEndurance.RimWidth = CSng(Math.Round(objEndurance.RimWidth * 25.4))

                '1 mile = 1.609344 kilometers

                For Each objDetail As EnduranceDetail In objEndurance.EnduranceDetails
                    'Modified by offshore on 26NOV2012.
                    If objEndurance.CertificationTypeId = Depository.Current.GetCertificationTypeID(certificationName) Then
                        objDetail.InfPressure = CSng(Math.Round(objDetail.InfPressure * 0.01, 1))
                    End If

                    'Modified by offshore on 26NOV2012.
                    If objEndurance.CertificationTypeId <> Depository.Current.GetCertificationTypeID(certificationName) Then

                        '1 Psi = 6.89475729 KPA

                        If objEndurance.CertificationTypeId = Depository.Current.GetCertificationTypeID(certificationTypeName1) Or objEndurance.CertificationTypeId = Depository.Current.GetCertificationTypeID(certificationTypeName2) Then
                            'Approved Substitutions
                            objDetail.InfPressure = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objEndurance.CertificationTypeId, substitutionField1, objDetail.InfPressure, objEndurance.SKUID)))
                        End If
                    Else
                        '1 pound per square inch = 0.0689475729 bar

                        objDetail.InfPressure = CSng(Math.Round(objDetail.InfPressure * 0.01, 1))
                    End If

                    'Modified by offshore on 26NOV2012.

                    If objEndurance.CertificationTypeId = Depository.Current.GetCertificationTypeID(certificationTypeName2) Then
                        'Approved Substitutions
                        objDetail.Speed = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objEndurance.CertificationTypeId, substitutionField2, objDetail.Speed, objEndurance.SKUID)))
                    End If
                Next
            End If

            If Not drwEndurance.IsGTSPECNull Then
                objEndurance.GTSpecMaterialNumber = drwEndurance.GTSPEC.TrimStart(CChar(zero))
            End If

            Return objEndurance
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map ICS DataTable To High Speed.
    ''' </summary>
    ''' <returns>HighSpeed Object</returns> 
    ''' <param name="p_dtbICSHighSpeedDtl">p_dtb ICS High Speed Dtl</param>
    ''' <param name="p_dtbICSHighSpeedHdr">p_dtb ICS High Speed Hdr</param>
    ''' <param name="p_dtbICSSpeedTestDetail">p_dtb ICS Speed Test Detail</param>
    ''' <param name="p_iCertificationTypeId">The p_i certification type id.</param>
    ''' <param name="p_strMatlNum">The p_strMatlNum. </param>
    ''' <param name="p_iSKUId">The p_i SKU id.</param>
    ''' <param name="p_intCertificateNumberID">The P_STR certificate Id.</param>    
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapICSDataTableToHighSpeed(ByVal p_dtbICSHighSpeedHdr As ICSDataSet.HighSpeedHdrDataTable, _
                                                ByVal p_dtbICSHighSpeedDtl As ICSDataSet.HighSpeedDtlDataTable, _
                                                ByVal p_dtbICSSpeedTestDetail As ICSDataSet.SpeedTestDetailDataTable, _
                                                ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                                ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As HighSpeed
        Try
            Debug.WriteLine("MapICSDataTableToHighSpeed")
            Dim objHighSpeed As New HighSpeed
            Dim dtbTRACSHighSpeedHdr As New ICS.Datasets.TRACStoICSDataset.HighSpeedHdrDataTable
            Dim dtbTRACSHighSpeedDtl As New ICS.Datasets.TRACStoICSDataset.HighSpeedDtlDataTable
            Dim drwHighSpeedHdr As ICSDataSet.HighSpeedHdrRow

            If p_dtbICSHighSpeedHdr.Rows.Count = 0 Then
                Return objHighSpeed
            End If

            dtbTRACSHighSpeedHdr.Merge(p_dtbICSHighSpeedHdr)
            dtbTRACSHighSpeedDtl.Merge(p_dtbICSHighSpeedDtl)

            objHighSpeed = MapTRACSDataTableToHighSpeed(dtbTRACSHighSpeedHdr, dtbTRACSHighSpeedDtl, p_iCertificationTypeId, p_strMatlNum, p_iSKUId, p_intCertificateNumberID)

            drwHighSpeedHdr = CType(p_dtbICSHighSpeedHdr.Rows(0), ICSDataSet.HighSpeedHdrRow)

            If Not drwHighSpeedHdr.IsMATL_NUMNull Then
                objHighSpeed.MaterialNumber = drwHighSpeedHdr.MATL_NUM.TrimStart(CChar(zero))
            End If

            If Not drwHighSpeedHdr.IsDIAMETERTESTDRUMNull Then
                objHighSpeed.DiameterTestDrum = drwHighSpeedHdr.DIAMETERTESTDRUM
            End If

            If Not drwHighSpeedHdr.IsPreCondTimeNull Then
                objHighSpeed.PrecondTime = drwHighSpeedHdr.PreCondTime
            End If
            If Not drwHighSpeedHdr.IsPRECONDTEMPNull Then
                objHighSpeed.PrecondTemp = drwHighSpeedHdr.PRECONDTEMP
            End If
            If Not drwHighSpeedHdr.IsINFLATIONPRESSUREREADJUSTEDNull Then
                objHighSpeed.InflationPressureReadjusted = drwHighSpeedHdr.INFLATIONPRESSUREREADJUSTED
            End If
            If Not drwHighSpeedHdr.IsCIRCUNFERENCEBEFORETESTNull Then
                objHighSpeed.CircumferenceBeforeTesting = drwHighSpeedHdr.CIRCUNFERENCEBEFORETEST
            End If
            If Not drwHighSpeedHdr.IsSPEEDTOTALTIMENull Then
                objHighSpeed.SpeedTotalTime = drwHighSpeedHdr.SPEEDTOTALTIME
            End If
            If Not drwHighSpeedHdr.IsWHEELSPEEDRPMNull Then
                objHighSpeed.WheelSpeedRPM = drwHighSpeedHdr.WHEELSPEEDRPM
            End If
            If Not drwHighSpeedHdr.IsWHEELSPEEDKMHNull Then
                objHighSpeed.WheelSpeedKMH = drwHighSpeedHdr.WHEELSPEEDKMH
            End If
            If Not drwHighSpeedHdr.IsPASSATKMHNull Then
                objHighSpeed.SpeedTestPassAt = drwHighSpeedHdr.PASSATKMH
            End If
            If Not drwHighSpeedHdr.IsSPEEDTTESTPASSFAILNull Then
                objHighSpeed.SpeedTestPassYN = CBool(IIf(drwHighSpeedHdr.SPEEDTTESTPASSFAIL = trueValue, True, False))
            End If

            If Not drwHighSpeedHdr.IsPostCondTimeNull Then
                objHighSpeed.PostcondTime = drwHighSpeedHdr.PostCondTime
            End If
            If Not drwHighSpeedHdr.IsCIRCUNFERENCEAFTERTESTNull Then
                objHighSpeed.CircumferenceAfterTesting = drwHighSpeedHdr.CIRCUNFERENCEAFTERTEST
            End If
            If Not drwHighSpeedHdr.IsODDIFERENCENull Then
                objHighSpeed.OuterDiameterDifference = drwHighSpeedHdr.ODDIFERENCE
            End If
            If Not drwHighSpeedHdr.IsODDIFERENCETOLERANCENull Then
                objHighSpeed.OuterDiameterTolerance = drwHighSpeedHdr.ODDIFERENCETOLERANCE
            End If
            If Not drwHighSpeedHdr.IsSERIENOMNull Then
                objHighSpeed.SerieNOM = drwHighSpeedHdr.SERIENOM
            End If
            If Not drwHighSpeedHdr.IsMAXSPEEDNull Then
                objHighSpeed.MaxSpeed = drwHighSpeedHdr.MAXSPEED
            End If
            If Not drwHighSpeedHdr.IsMAXLOADNull Then
                objHighSpeed.MaxLoad = drwHighSpeedHdr.MAXLOAD
            End If
            If Not drwHighSpeedHdr.IsFINALJUDGEMENTNull Then
                objHighSpeed.FinalJudgement = CBool(IIf(drwHighSpeedHdr.FINALJUDGEMENT = trueValue, True, False))
            End If
            If Not drwHighSpeedHdr.IsAPPROVERNull Then
                objHighSpeed.Approver = drwHighSpeedHdr.APPROVER
            End If
            If Not drwHighSpeedHdr.IsBeforeInflationNull Then
                objHighSpeed.BeforeInflation = drwHighSpeedHdr.BeforeInflation
            End If
            If Not drwHighSpeedHdr.IsInflationPressureNull Then
                objHighSpeed.InflationPressure = drwHighSpeedHdr.InflationPressure
            End If
            If Not drwHighSpeedHdr.IsAfterInflationNull Then
                objHighSpeed.AfterInflation = drwHighSpeedHdr.AfterInflation
            End If

            If Not drwHighSpeedHdr.IsGTSPECNull Then
                objHighSpeed.GTSpecMaterialNumber = drwHighSpeedHdr.GTSPEC.TrimStart(CChar(zero))
            End If

            objHighSpeed.SpeedTestDetails = MapICSDataTableToHighSpeedSpeedTestDetail(p_dtbICSSpeedTestDetail)

            Return objHighSpeed
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map ICS DataTable ToHighSpeed Speed Test Detail.
    ''' </summary>
    ''' <returns>List of SpeedTestDetail object</returns> 
    ''' <param name="p_dtbICSSpeedTestDetail">p_dtb ICS Speed Test Detail</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapICSDataTableToHighSpeedSpeedTestDetail(ByVal p_dtbICSSpeedTestDetail As ICSDataSet.SpeedTestDetailDataTable) As List(Of SpeedTestDetail)
        Try
            Dim SpeedTestDetails As List(Of SpeedTestDetail) = New List(Of SpeedTestDetail)
            Dim drwSpeedTestDetail As ICSDataSet.SpeedTestDetailRow

            For Each drwSpeedTestDetail In p_dtbICSSpeedTestDetail.Rows
                If Not drwSpeedTestDetail.IsIterationNull Then
                    Dim objSpeedTestDetail As New SpeedTestDetail
                    objSpeedTestDetail.HS_ID = drwSpeedTestDetail.HS_ID
                    objSpeedTestDetail.Iteration = CShort(drwSpeedTestDetail.Iteration)
                    If Not drwSpeedTestDetail.IsSpeedNull Then
                        objSpeedTestDetail.Speed = drwSpeedTestDetail.Speed
                    End If
                    If Not drwSpeedTestDetail.IsTimeNull Then
                        objSpeedTestDetail.Time = drwSpeedTestDetail.Time
                    End If
                    SpeedTestDetails.Add(objSpeedTestDetail)
                End If
            Next

            Return SpeedTestDetails
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Defect 10034: Modified by offshore on 26NOV2012. Commented out all metric conversion statements.
    ''' <summary>
    '''  Method to Map TRACS datatable to High Speed.
    ''' </summary>
    ''' <returns>High Speed Object</returns> 
    ''' <param name="p_dtbHighSpeedDtl">High SpeedDtl.</param>
    ''' <param name="p_dtbHighSpeedHdr">High SPeed Hdr.</param>    
    ''' <param name="p_iCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_intCertificateNumberID">Certificate Number Id</param>
    ''' <param name="p_iSKUId">SKU Id</param>
    ''' <param name="p_strMatlNum">Material Number</param>
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
    Private Function MapTRACSDataTableToHighSpeed(ByVal p_dtbHighSpeedHdr As ICS.Datasets.TRACStoICSDataset.HighSpeedHdrDataTable, _
                                                ByVal p_dtbHighSpeedDtl As ICS.Datasets.TRACStoICSDataset.HighSpeedDtlDataTable, _
                                                ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                                ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As HighSpeed
        Try
            Dim objHighSpeed As New HighSpeed
            Dim drwHighSpeed As ICS.Datasets.TRACStoICSDataset.HighSpeedHdrRow
            Dim drwHighSpeedDetail As ICS.Datasets.TRACStoICSDataset.HighSpeedDtlRow

            objHighSpeed.CertificationTypeId = p_iCertificationTypeId
            objHighSpeed.SKUID = p_iSKUId
            objHighSpeed.CertificateNumberID = p_intCertificateNumberID

            If p_dtbHighSpeedHdr.Rows.Count = 0 Then
                Return objHighSpeed
            End If

            drwHighSpeed = CType(p_dtbHighSpeedHdr.Rows(0), Datasets.TRACStoICSDataset.HighSpeedHdrRow)

            If Not drwHighSpeed.IsProjectNumNull Then
                objHighSpeed.ProjectNumber = drwHighSpeed.ProjectNum
            End If

            If Not drwHighSpeed.IsTireNumNull Then
                objHighSpeed.TireNumber = drwHighSpeed.TireNum
            End If

            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not drwHighSpeed.IsOperationNull Then
                objHighSpeed.Operation = drwHighSpeed.Operation
            End If

            If Not drwHighSpeed.IsTestSpecNull Then
                objHighSpeed.TestSpec = drwHighSpeed.TestSpec
            End If

            If Not drwHighSpeed.IsTestSKUNull Then
                objHighSpeed.MaterialNumber = drwHighSpeed.TestSKU
            End If

            Try
                If Not drwHighSpeed.IsCompletionDateNull Then
                    objHighSpeed.CompletionDate = ConvertToDateTime(drwHighSpeed.CompletionDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwHighSpeed.IsDOTSerialNumberNull Then
                objHighSpeed.DotSerialNumber = drwHighSpeed.DOTSerialNumber
            End If

            Try
                If Not drwHighSpeed.IsSerialDateNull Then
                    objHighSpeed.SerialDate = ConvertToDateTime(drwHighSpeed.SerialDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            Try
                If Not drwHighSpeed.IsPreCondStartDateNull Then
                    objHighSpeed.PrecondStartDate = ConvertToDateTime(drwHighSpeed.PreCondStartDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwHighSpeed.IsPreCondSartTempNull Then
                objHighSpeed.PrecondStartTemp = drwHighSpeed.PreCondSartTemp
            End If

            If Not drwHighSpeed.IsRimDiameterNull Then
                objHighSpeed.RimDiameter = drwHighSpeed.RimDiameter
            End If

            If Not drwHighSpeed.IsRimWidthNull Then
                objHighSpeed.RimWidth = drwHighSpeed.RimWidth
            End If

            Try
                If Not drwHighSpeed.IsPreCondEndDateNull Then
                    objHighSpeed.PrecondEndDate = ConvertToDateTime(drwHighSpeed.PreCondEndDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwHighSpeed.IsPreCondEndTempNull Then
                objHighSpeed.PrecondEndTemp = drwHighSpeed.PreCondEndTemp
            End If

            If Not drwHighSpeed.IsPreCondTimeNull Then
                objHighSpeed.PrecondTime = drwHighSpeed.PreCondTime
            End If

            If Not drwHighSpeed.IsInflationPressureNull Then
                objHighSpeed.InflationPressure = drwHighSpeed.InflationPressure
            End If

            If Not drwHighSpeed.IsBeforeDiameterNull Then
                objHighSpeed.BeforeDiameter = drwHighSpeed.BeforeDiameter
            End If

            If Not drwHighSpeed.IsAfterDiameterNull Then
                objHighSpeed.AfterDiameter = drwHighSpeed.AfterDiameter
            End If

            If Not drwHighSpeed.IsBeforeInflationNull Then
                objHighSpeed.BeforeInflation = drwHighSpeed.BeforeInflation
            End If

            If Not drwHighSpeed.IsAfterInflationNull Then
                objHighSpeed.AfterInflation = drwHighSpeed.AfterInflation
            End If

            If Not drwHighSpeed.IsWheelPositionNull Then
                objHighSpeed.WheelPosition = drwHighSpeed.WheelPosition
            Else
                objHighSpeed.WheelPosition = -1
            End If

            If Not drwHighSpeed.IsWheelNumNull Then
                objHighSpeed.WheelNumber = drwHighSpeed.WheelNum
            Else
                objHighSpeed.WheelNumber = -1
            End If

            If Not drwHighSpeed.IsFinalTempNull Then
                objHighSpeed.FinalTemp = drwHighSpeed.FinalTemp
            End If

            If Not drwHighSpeed.IsFinalDistanceNull Then
                objHighSpeed.FinalDistance = drwHighSpeed.FinalDistance
            End If

            If Not drwHighSpeed.IsFinalInflationNull Then
                objHighSpeed.FinalInflation = drwHighSpeed.FinalInflation
            End If

            Try
                If Not drwHighSpeed.IsPostCondStartDateNull Then
                    objHighSpeed.PostcondStartDate = ConvertToDateTime(drwHighSpeed.PostCondStartDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            Try
                If Not drwHighSpeed.IsPostCondEndDateNull Then
                    objHighSpeed.PostcondEndDate = ConvertToDateTime(drwHighSpeed.PostCondEndDate)
                End If
            Catch exc As Exception
                ' Leave default value
            End Try

            If Not drwHighSpeed.IsPostCondTimeNull Then
                objHighSpeed.PostcondTime = drwHighSpeed.PostCondTime
            End If

            If Not drwHighSpeed.IsPostCondEndTempNull Then
                objHighSpeed.PostcondEndTemp = drwHighSpeed.PostCondEndTemp
            End If

            If Not drwHighSpeed.IsPassYNNull Then
                objHighSpeed.DurationTestPassYN = CBool(IIf(drwHighSpeed.PassYN.ToLower() = trueValue, True, False))
            End If

            If Not drwHighSpeed.IsHS_IDNull Then
                objHighSpeed.HS_ID = drwHighSpeed.HS_ID
            End If

            If Not drwHighSpeed.IsGTSPECNull Then
                objHighSpeed.GTSpecMaterialNumber = drwHighSpeed.GTSPEC.TrimStart(CChar(zero))
            End If

            'HighSpeed Detail
            Dim Iteration As Integer = 0
            For Each drwHighSpeedDetail In p_dtbHighSpeedDtl.Rows

                Dim objHighSpeedDetail As New HighSpeedDetail

                If Not drwHighSpeedDetail.IsHS_IDNull Then
                    objHighSpeedDetail.HS_ID = drwHighSpeedDetail.HS_ID
                End If

                objHighSpeedDetail.Iteration = CShort(Iteration)

                If Not drwHighSpeedDetail.IsTestStepNull Then
                    objHighSpeedDetail.TestStep = drwHighSpeedDetail.TestStep
                End If

                If Not drwHighSpeedDetail.IsTimeInMinNull Then
                    objHighSpeedDetail.TimeInMin = drwHighSpeedDetail.TimeInMin
                End If

                objHighSpeed.SpeedTotalTime = objHighSpeed.SpeedTotalTime + objHighSpeedDetail.TimeInMin

                If Not drwHighSpeedDetail.IsSpeedNull Then
                    objHighSpeedDetail.Speed = drwHighSpeedDetail.Speed
                End If

                If Not drwHighSpeedDetail.IsTotMilesNull Then
                    objHighSpeedDetail.TotMiles = drwHighSpeedDetail.TotMiles
                End If

                If Not drwHighSpeedDetail.IsLoadNull Then
                    objHighSpeedDetail.Load = drwHighSpeedDetail.Load
                End If

                If Not drwHighSpeedDetail.IsLoadPercentNull Then
                    objHighSpeedDetail.LoadPercent = drwHighSpeedDetail.LoadPercent
                End If

                If Not drwHighSpeedDetail.IsSetInflationNull Then
                    objHighSpeedDetail.SetInflation = drwHighSpeedDetail.SetInflation
                End If

                If Not drwHighSpeedDetail.IsAmbTempNull Then
                    objHighSpeedDetail.AmbTemp = drwHighSpeedDetail.AmbTemp
                Else
                    objHighSpeedDetail.AmbTemp = -1
                End If

                If Not drwHighSpeedDetail.IsInfPressureNull Then
                    objHighSpeedDetail.InfPressure = drwHighSpeedDetail.InfPressure
                End If

                Try
                    If Not drwHighSpeedDetail.IsStepCompletionDateNull Then
                        objHighSpeedDetail.StepCompletionDate = CDate(drwHighSpeedDetail.StepCompletionDate)
                    End If
                Catch exc As Exception
                    ' Leave default value
                End Try

                objHighSpeed.HighSpeedDetails.Add(objHighSpeedDetail)

                Iteration = Iteration + 1

            Next

            'Conversion
            If p_intCertificateNumberID = 0 Then

                'Modified by offshore on 26NOV2012.

                If objHighSpeed.CertificationTypeId <> Depository.Current.GetCertificationTypeID(certificationName) Then

                    If objHighSpeed.CertificationTypeId = Depository.Current.GetCertificationTypeID(certificationTypeName1) Or objHighSpeed.CertificationTypeId = Depository.Current.GetCertificationTypeID(certificationTypeName2) Then
                        'Approved Substitutions
                        objHighSpeed.AfterInflation = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objHighSpeed.CertificationTypeId, substitutionField3, objHighSpeed.AfterInflation, objHighSpeed.SKUID)))
                        objHighSpeed.BeforeInflation = CShort(Math.Round(Depository.Current.GetApprovedSubstitution(objHighSpeed.CertificationTypeId, substitutionField3, objHighSpeed.BeforeInflation, objHighSpeed.SKUID)))
                        objHighSpeed.FinalInflation = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objHighSpeed.CertificationTypeId, substitutionField3, objHighSpeed.FinalInflation, objHighSpeed.SKUID)))
                        objHighSpeed.InflationPressure = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objHighSpeed.CertificationTypeId, substitutionField3, objHighSpeed.InflationPressure, objHighSpeed.SKUID)))
                    End If
                Else

                    objHighSpeed.AfterInflation = CSng(Math.Round(objHighSpeed.AfterInflation * 0.01, 1))
                    objHighSpeed.BeforeInflation = CShort(Math.Round(objHighSpeed.BeforeInflation * 0.01, 1))
                    objHighSpeed.FinalInflation = CSng(Math.Round(objHighSpeed.FinalInflation * 0.01, 1))
                    objHighSpeed.InflationPressure = CSng(Math.Round(objHighSpeed.InflationPressure * 0.01, 1))
                End If

                objHighSpeed.RimWidth = CSng(Math.Round(objHighSpeed.RimWidth * 25.4))

                For Each objDetail As HighSpeedDetail In objHighSpeed.HighSpeedDetails

                    'Modified by offshore on 26NOV2012.

                    If objHighSpeed.CertificationTypeId <> Depository.Current.GetCertificationTypeID(certificationName) Then

                        If objHighSpeed.CertificationTypeId = Depository.Current.GetCertificationTypeID(certificationTypeName1) Or objHighSpeed.CertificationTypeId = Depository.Current.GetCertificationTypeID(certificationTypeName2) Then
                            'Approved Substitutions
                            objDetail.InfPressure = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objHighSpeed.CertificationTypeId, substitutionField1, objDetail.InfPressure, objHighSpeed.SKUID)))
                        End If

                    Else

                        '1 pound per square inch = 0.0689475729 bar
                        objDetail.InfPressure = CSng(Math.Round(objDetail.InfPressure * 0.01, 1))
                    End If

                    If objHighSpeed.CertificationTypeId = Depository.Current.GetCertificationTypeID(certificationTypeName2) Then
                        'Approved Substitutions
                        objDetail.Speed = CSng(Math.Round(Depository.Current.GetApprovedSubstitution(objHighSpeed.CertificationTypeId, substitutionField2, objDetail.Speed, objHighSpeed.SKUID)))
                    End If

                Next

            End If

            Return objHighSpeed
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map ICS to Sound.
    ''' </summary>
    ''' <returns>Sound Object</returns> 
    ''' <param name="p_strMatlNum">Material Number.</param>    
    ''' <param name="p_dtbSoundDtl">Sound Datatable</param>
    ''' <param name="p_dtbSoundHdr">Sound Hdr</param>
    ''' <param name="p_iCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_intCertificateNumberID">Certification Number Id</param>
    ''' <param name="p_iSKUId">SKU Id</param>
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
    Private Function MapICSDataTableToSound(ByVal p_dtbSoundHdr As ICSDataSet.SoundHdrDataTable, _
                                            ByVal p_dtbSoundDtl As ICSDataSet.SoundDetailDataTable, _
                                            ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                            ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As Sound
        Try
            Debug.WriteLine("MapICSDataTableToSound")

            Dim objSound As New Sound
            Dim drwSound As ICSDataSet.SoundHdrRow
            Dim drwSoundDetail As ICSDataSet.SoundDetailRow

            objSound.CertificationTypeID = p_iCertificationTypeId
            objSound.SKUId = p_iSKUId
            objSound.CertificateNumberID = p_intCertificateNumberID

            If p_dtbSoundHdr.Rows.Count = 0 Then
                Return objSound
            End If

            drwSound = CType(p_dtbSoundHdr.Rows(0), ICSDataSet.SoundHdrRow)

            If Not drwSound.IsPROJECTNUMBERNull Then
                objSound.ProjectNumber = drwSound.PROJECTNUMBER
            End If
            If Not drwSound.IsTIRENUMBERNull Then
                objSound.TireNumber = CInt(drwSound.TIRENUMBER)
            End If
            'Added Operation  as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not drwSound.IsOperationNull Then
                objSound.Operation = drwSound.Operation
            End If
            If Not drwSound.IsTESTSPECNull Then
                objSound.TestSpec = drwSound.TESTSPEC
            End If
            If Not drwSound.IsSOUNDIDNull Then
                objSound.SoundID = CShort(drwSound.SOUNDID)
            End If

            If Not drwSound.IsTESTREPORTNUMBERNull Then
                objSound.TestReportNumber = drwSound.TESTREPORTNUMBER
            End If
            If Not drwSound.IsMANUFACTUREANDBRANDNull Then
                objSound.ManufactureAndBrand = drwSound.MANUFACTUREANDBRAND
            End If
            If Not drwSound.IsTIRECLASSNull Then
                objSound.TireClass = drwSound.TIRECLASS
            End If
            If Not drwSound.IsCATEGORYOFUSENull Then
                objSound.CategoryOfUse = drwSound.CATEGORYOFUSE
            End If
            If Not drwSound.IsDATEOFTESTNull Then
                objSound.DateOfTest = drwSound.DATEOFTEST
            End If
            If Not drwSound.IsTESTVEHICULENull Then
                objSound.TestVehicule = drwSound.TESTVEHICULE
            End If
            If Not drwSound.IsTESTVEHICULEWHEELBASENull Then
                objSound.TestVehiculeWheelbase = drwSound.TESTVEHICULEWHEELBASE
            End If
            If Not drwSound.IsLOCATIONOFTESTTRACKNull Then
                objSound.LocationOfTestTrack = drwSound.LOCATIONOFTESTTRACK
            End If
            If Not drwSound.IsDATETRACKCERTIFTOISONull Then
                objSound.DateTrackCertifToISO = drwSound.DATETRACKCERTIFTOISO
            End If
            If Not drwSound.IsTIRESIZEDESIGNATIONNull Then
                objSound.TireSizeDesignation = drwSound.TIRESIZEDESIGNATION
            End If
            If Not drwSound.IsTIRESERVICEDESCRIPTIONNull Then
                objSound.TireServiceDescription = drwSound.TIRESERVICEDESCRIPTION
            End If
            If Not drwSound.IsReferenceInflationPressureNull Then
                objSound.ReferenceInflationPressure = drwSound.ReferenceInflationPressure
            End If

            If Not drwSound.IsTESTMASS_FRONTLNull Then
                objSound.TestMass_FrontL = drwSound.TESTMASS_FRONTL
            End If
            If Not drwSound.IsTESTMASS_FRONTRNull Then
                objSound.TestMass_FrontR = drwSound.TESTMASS_FRONTR
            End If
            If Not drwSound.IsTESTMASS_REARLNull Then
                objSound.TestMass_RearL = drwSound.TESTMASS_REARL
            End If
            If Not drwSound.IsTESTMASS_REARRNull Then
                objSound.TestMass_RearR = drwSound.TESTMASS_REARR
            End If
            If Not drwSound.IsTIRELOADINDEX_FRONTLNull Then
                objSound.TireLoadIndex_FrontL = drwSound.TIRELOADINDEX_FRONTL
            End If
            If Not drwSound.IsTIRELOADINDEX_FRONTRNull Then
                objSound.TireLoadIndex_FrontR = drwSound.TIRELOADINDEX_FRONTR
            End If
            If Not drwSound.IsTIRELOADINDEX_REARLNull Then
                objSound.TireLoadIndex_RearL = CStr(drwSound.IsTIRELOADINDEX_REARLNull)
            End If
            If Not drwSound.IsTIRELOADINDEX_REARRNull Then
                objSound.TireLoadIndex_RearR = CStr(drwSound.IsTIRELOADINDEX_REARRNull)
            End If
            If Not drwSound.IsINFLATIONPRESSURECO_FRONTLNull Then
                objSound.InflationPressureCo_FrontL = drwSound.INFLATIONPRESSURECO_FRONTL
            End If
            If Not drwSound.IsINFLATIONPRESSURECO_FRONTRNull Then
                objSound.InflationPressureCo_FrontR = drwSound.INFLATIONPRESSURECO_FRONTR
            End If
            If Not drwSound.IsINFLATIONPRESSURECO_REARLNull Then
                objSound.InflationPressureCo_RearL = drwSound.INFLATIONPRESSURECO_REARL
            End If
            If Not drwSound.IsINFLATIONPRESSURECO_REARRNull Then
                objSound.InflationPressureCo_RearR = drwSound.INFLATIONPRESSURECO_REARR
            End If

            If Not drwSound.IsTESTRIMWIDTHCODENull Then
                objSound.TestRimWidthCode = drwSound.TESTRIMWIDTHCODE
            End If
            If Not drwSound.IsTEMPMEASURESENSORTYPENull Then
                objSound.TempMeasureSensorType = drwSound.TEMPMEASURESENSORTYPE
            End If

            If Not drwSound.IsGTSPECNull Then
                objSound.GTSpecMaterialNumber = drwSound.GTSPEC.TrimStart(CChar(zero))
            End If

            'Sound Detail
            For Each drwSoundDetail In p_dtbSoundDtl.Rows

                Dim objSoundDetail As New SoundDetail
                objSoundDetail.SoundID = CShort(drwSoundDetail.SOUNDID)

                objSoundDetail.Iteration = CInt(drwSoundDetail.ITERATION)

                If Not drwSoundDetail.IsTESTSPEEDNull Then
                    objSoundDetail.TestSpeed = drwSoundDetail.TESTSPEED
                End If

                If Not drwSoundDetail.IsDIRECTIONOFRUNNull Then
                    objSoundDetail.DirectionOfRun = drwSoundDetail.DIRECTIONOFRUN
                End If

                If Not drwSoundDetail.IsSOUNDLEVELLEFTNull Then
                    objSoundDetail.SoundLevelLeft = drwSoundDetail.SOUNDLEVELLEFT
                End If

                If Not drwSoundDetail.IsSOUNDLEVELRIGHTNull Then
                    objSoundDetail.SoundLevelRight = drwSoundDetail.SOUNDLEVELRIGHT
                End If

                If Not drwSoundDetail.IsAIRTEMPNull Then
                    objSoundDetail.AirTemp = drwSoundDetail.AIRTEMP
                End If

                If Not drwSoundDetail.IsTRACKTEMPNull Then
                    objSoundDetail.TrackTemp = drwSoundDetail.TRACKTEMP
                End If

                If Not drwSoundDetail.IsSOUNDLEVELLEFT_TEMPCORRECTEDNull Then
                    objSoundDetail.SoundLevelLeft_TempCorrected = drwSoundDetail.SOUNDLEVELLEFT_TEMPCORRECTED
                End If

                If Not drwSoundDetail.IsSOUNDLEVELRIGHT_TEMPCORRECTEDNull Then
                    objSoundDetail.SoundLevelRight_TempCorrected = drwSoundDetail.SOUNDLEVELRIGHT_TEMPCORRECTED
                End If

                objSound.SoundDetails.Add(objSoundDetail)

            Next

            Return objSound
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map ICS to Wet Grip.
    ''' </summary>
    ''' <returns>WetGrip</returns> 
    ''' <param name="p_dtbWetGripDtl">Wet Grip datatable.</param>
    ''' <param name="p_dtbWetGripHdr">Wet Grip Hdr.</param>    
    ''' <param name="p_iCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_intCertificateNumberID">Certificate Number Id</param>
    ''' <param name="p_iSKUId">SKU Id</param>
    ''' <param name="p_strMatlNum">Material Number</param>
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
    Private Function MapICSDataTableToWetGrip(ByVal p_dtbWetGripHdr As ICSDataSet.WetGripHdrDataTable, _
                                            ByVal p_dtbWetGripDtl As ICSDataSet.WetGripDetailDataTable, _
                                            ByVal p_iCertificationTypeId As Integer, ByVal p_strMatlNum As String, _
                                            ByVal p_iSKUId As Integer, ByVal p_intCertificateNumberID As Integer) As WetGrip
        Try
            Debug.WriteLine("MapICSDataTableToWetGrip")

            Dim objWetGrip As New WetGrip
            Dim drwWetGrip As ICSDataSet.WetGripHdrRow
            Dim drwWetGripDetail As ICSDataSet.WetGripDetailRow

            objWetGrip.CertificationTypeID = p_iCertificationTypeId
            objWetGrip.SKUId = p_iSKUId
            objWetGrip.CertificateNumberID = p_intCertificateNumberID

            If p_dtbWetGripHdr.Rows.Count = 0 Then
                Return objWetGrip
            End If

            drwWetGrip = CType(p_dtbWetGripHdr.Rows(0), ICSDataSet.WetGripHdrRow)

            If Not drwWetGrip.IsPROJECTNUMBERNull Then
                objWetGrip.ProjectNumber = drwWetGrip.PROJECTNUMBER
            End If
            If Not drwWetGrip.IsTIRENUMBERNull Then
                objWetGrip.TireNumber = CInt(drwWetGrip.TIRENUMBER)
            End If

            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not drwWetGrip.IsOperationNull Then
                objWetGrip.Operation = drwWetGrip.Operation
            End If

            If Not drwWetGrip.IsTESTSPECNull Then
                objWetGrip.TestSpec = drwWetGrip.TESTSPEC
            End If
            If Not drwWetGrip.IsWETGRIPIDNull Then
                objWetGrip.WetGripID = CShort(drwWetGrip.WETGRIPID)
            End If

            If Not drwWetGrip.IsDATEOFTESTNull Then
                objWetGrip.DateOfTest = drwWetGrip.DATEOFTEST
            End If
            If Not drwWetGrip.IsTESTVEHICLENull Then
                objWetGrip.TestVehicle = drwWetGrip.TESTVEHICLE
            End If
            If Not drwWetGrip.IsLOCATIONOFTESTTRACKNull Then
                objWetGrip.LocationOfTestTrack = drwWetGrip.LOCATIONOFTESTTRACK
            End If
            If Not drwWetGrip.IsTESTTRACKCHARACTERISTICSNull Then
                objWetGrip.TestTrackCharacteristics = drwWetGrip.TESTTRACKCHARACTERISTICS
            End If
            If Not drwWetGrip.IsISSUEBYNull Then
                objWetGrip.IssueBy = drwWetGrip.ISSUEBY
            End If
            If Not drwWetGrip.IsMETHODOFCERTIFICATIONNull Then
                objWetGrip.MethodOfCertification = drwWetGrip.METHODOFCERTIFICATION
            End If
            If Not drwWetGrip.IsTESTTIREDETAILSNull Then
                objWetGrip.TestTireDetails = drwWetGrip.TESTTIREDETAILS
            End If
            If Not drwWetGrip.IsTIRESIZEANDSERVICEDESCNull Then
                objWetGrip.TireSizeAndServiceDesc = drwWetGrip.TIRESIZEANDSERVICEDESC
            End If
            If Not drwWetGrip.IsTIREBRANDANDTRADEDESCNull Then
                objWetGrip.TireBrandAndTradeDesc = drwWetGrip.TIREBRANDANDTRADEDESC
            End If
            If Not drwWetGrip.IsREFERENCEINFLATIONPRESSURENull Then
                objWetGrip.ReferenceInflationPressure = drwWetGrip.REFERENCEINFLATIONPRESSURE
            End If

            If Not drwWetGrip.IsTESTTIRELOAD_SRTTNull Then
                objWetGrip.TestTireLoad_SRTT = drwWetGrip.TESTTIRELOAD_SRTT
            End If
            If Not drwWetGrip.IsTESTTIRELOAD_CANDIDATENull Then
                objWetGrip.TestTireLoad_Candidate = drwWetGrip.TESTTIRELOAD_CANDIDATE
            End If
            If Not drwWetGrip.IsTESTTIRELOAD_CONTROLNull Then
                objWetGrip.TestTireLoad_Control = drwWetGrip.TESTTIRELOAD_CONTROL
            End If
            If Not drwWetGrip.IsWATERDEPTH_SRTTNull Then
                objWetGrip.WaterDepth_SRTT = drwWetGrip.WATERDEPTH_SRTT
            End If
            If Not drwWetGrip.IsWATERDEPTH_CANDIDATENull Then
                objWetGrip.WaterDepth_Candidate = drwWetGrip.WATERDEPTH_CANDIDATE
            End If
            If Not drwWetGrip.IsWATERDEPTH_CONTROLNull Then
                objWetGrip.WaterDepth_Control = drwWetGrip.WATERDEPTH_CONTROL
            End If
            If Not drwWetGrip.IsWETTEDTRACKTEMPAVGNull Then
                objWetGrip.WettedTrackTempAvg = drwWetGrip.WETTEDTRACKTEMPAVG
            End If

            If Not drwWetGrip.IsTESTRIMWITHCODENull Then
                objWetGrip.TestRimWithCode = drwWetGrip.TESTRIMWITHCODE
            End If
            If Not drwWetGrip.IsTEMPMEASURESENSORTYPENull Then
                objWetGrip.TempMeasureSensorType = drwWetGrip.TEMPMEASURESENSORTYPE
            End If
            If Not drwWetGrip.IsIDENTIFICATIONSRTTNull Then
                objWetGrip.IdentificationSRTT = drwWetGrip.IDENTIFICATIONSRTT
            End If

            If Not drwWetGrip.IsGTSPECNull Then
                objWetGrip.GTSpecMaterialNumber = drwWetGrip.GTSPEC.TrimStart(CChar(zero))
            End If

            'WetGrip Detail
            For Each drwWetGripDetail In p_dtbWetGripDtl.Rows

                Dim objWetGripDetail As New WetGripDetail
                objWetGripDetail.WetGripID = CShort(drwWetGripDetail.WETGRIPID)

                objWetGripDetail.Iteration = CInt(drwWetGripDetail.ITERATION)

                If Not drwWetGripDetail.IsTESTSPEEDNull Then
                    objWetGripDetail.TestSpeed = drwWetGripDetail.TESTSPEED
                End If

                If Not drwWetGripDetail.IsDIRECTIONOFRUNNull Then
                    objWetGripDetail.DirectionOfRun = drwWetGripDetail.DIRECTIONOFRUN
                End If

                If Not drwWetGripDetail.IsSRTTNull Then
                    objWetGripDetail.SRTT = drwWetGripDetail.SRTT
                End If

                If Not drwWetGripDetail.IsCANDIDATETIRENull Then
                    objWetGripDetail.CandidateTire = drwWetGripDetail.CANDIDATETIRE
                End If

                If Not drwWetGripDetail.IsPEAKBREAKFORCECOEFICIENTNull Then
                    objWetGripDetail.PeakBreakForceCoeficient = drwWetGripDetail.PEAKBREAKFORCECOEFICIENT
                End If

                If Not drwWetGripDetail.IsMEANFULLYDEVELOPEDDECELERATIONNull Then
                    objWetGripDetail.MeanFullyDevelopedDeceleration = drwWetGripDetail.MEANFULLYDEVELOPEDDECELERATION
                End If

                If Not drwWetGripDetail.IsWETGRIPINDEXNull Then
                    objWetGripDetail.WetGripIndex = drwWetGripDetail.WETGRIPINDEX
                End If

                If Not drwWetGripDetail.IsCOMMENTSNull Then
                    objWetGripDetail.Comments = drwWetGripDetail.COMMENTS
                End If

                objWetGrip.WetGripDetails.Add(objWetGripDetail)

            Next

            Return objWetGrip
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map entities to TR measurement Section Data.
    ''' </summary>
    ''' <returns>TRMeasurementSectionData</returns> 
    ''' <param name="p_intCertificationTypeId">Cetification Type Id.</param>
    ''' <param name="p_objBeadUnSeat">Bead Unseat.</param>    
    ''' <param name="p_objMeasure">Measure</param>
    ''' <param name="p_objPlunger">Plunger</param>
    ''' <param name="p_objTreadwear">Tread Wear</param>
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
    Private Function MapEntitiesToTRMeasurementSectionData(ByVal p_objMeasure As Measure, ByVal p_objTreadwear As Treadwear, ByVal p_objPlunger As Plunger, ByVal p_objBeadUnSeat As BeadUnSeat, ByVal p_intCertificationTypeId As Integer) As TRMeasurementSectionData
        Dim timeFormat As String = "hh:mm tt"
        Dim hourFormat As String = "Hr"
        Dim minFormat As String = "Min"
        Dim treadwearHeight As String = "TreadwearHeight"
        Dim plunger As String = "Plunger"
        Dim beadUnseatTest As String = "BeadUnseatTest"

        Try
            Dim objTRMeasurementSectionData As New TRMeasurementSectionData

            objTRMeasurementSectionData.MeasureMatlNum = p_objMeasure.MaterialNumber
            objTRMeasurementSectionData.TreadwearMatlNum = p_objTreadwear.MaterialNumber
            objTRMeasurementSectionData.PlungerMatlNum = p_objPlunger.MaterialNumber
            objTRMeasurementSectionData.BeadUnseatMatlNum = p_objBeadUnSeat.MaterialNumber

            objTRMeasurementSectionData.GTSpecMeasureMatlNum = p_objMeasure.GTSpecMaterialNumber
            objTRMeasurementSectionData.GTSpecTreadwearMatlNum = p_objTreadwear.GTSpecMaterialNumber
            objTRMeasurementSectionData.GTSpecPlungerMatlNum = p_objPlunger.GTSpecMaterialNumber
            objTRMeasurementSectionData.GTSpecBeadUnseatMatlNum = p_objBeadUnSeat.GTSpecMaterialNumber

            objTRMeasurementSectionData.DOTSerialNumber = p_objMeasure.DotSerialNumber

            objTRMeasurementSectionData.StartDate = p_objMeasure.MountTime.ToShortDateString()
            objTRMeasurementSectionData.EndDate = p_objMeasure.CompletionDate.ToShortDateString()
            objTRMeasurementSectionData.StartTime = p_objMeasure.MountTime.ToString(timeFormat)
            objTRMeasurementSectionData.EndTime = p_objMeasure.CompletionDate.ToString(timeFormat)


            Dim tsTotal As TimeSpan = p_objMeasure.CompletionDate - p_objMeasure.MountTime
            Dim intMin As Integer = tsTotal.Minutes
            Dim intHour As Integer = tsTotal.Days * 24 + tsTotal.Hours
            objTRMeasurementSectionData.TotalTime = intHour.ToString() + hourFormat + intMin.ToString() + minFormat
            objTRMeasurementSectionData.InflationPressure = p_objMeasure.InflationPressure.ToString()

            objTRMeasurementSectionData.StartInflationPressure = p_objMeasure.StartInfPressure.ToString()
            objTRMeasurementSectionData.EndInflationPressure = p_objMeasure.EndInfPressure.ToString()

            Dim intDetailsCount As Integer ' Detail data values count
            Dim intFieldCount As Integer ' Detail field / control count

            intDetailsCount = p_objMeasure.MeasureDetails.Count()

            If p_objMeasure.MeasureDetails.Count() > 0 Then

                If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(0).OverallWidth.ToString()) Then
                    objTRMeasurementSectionData.OverallWidth1 = p_objMeasure.MeasureDetails(0).OverallWidth.ToString()
                Else
                    objTRMeasurementSectionData.OverallWidth1 = String.Empty
                End If

                If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(0).SectionWidth.ToString()) Then
                    objTRMeasurementSectionData.SectionWidth1 = p_objMeasure.MeasureDetails(0).SectionWidth.ToString()
                Else
                    objTRMeasurementSectionData.SectionWidth1 = String.Empty
                End If


                If intDetailsCount > 1 Then
                    If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(1).OverallWidth.ToString()) Then
                        objTRMeasurementSectionData.OverallWidth2 = p_objMeasure.MeasureDetails(1).OverallWidth.ToString()
                    Else
                        objTRMeasurementSectionData.OverallWidth2 = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(1).SectionWidth.ToString()) Then
                        objTRMeasurementSectionData.SectionWidth2 = p_objMeasure.MeasureDetails(1).SectionWidth.ToString()
                    Else
                        objTRMeasurementSectionData.SectionWidth2 = String.Empty
                    End If

                End If
                If intDetailsCount > 2 Then
                    If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(2).OverallWidth.ToString()) Then
                        objTRMeasurementSectionData.OverallWidth3 = p_objMeasure.MeasureDetails(2).OverallWidth.ToString()
                    Else
                        objTRMeasurementSectionData.OverallWidth3 = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(2).SectionWidth.ToString()) Then
                        objTRMeasurementSectionData.SectionWidth3 = p_objMeasure.MeasureDetails(2).SectionWidth.ToString()
                    Else
                        objTRMeasurementSectionData.SectionWidth3 = String.Empty
                    End If

                End If

                If intDetailsCount > 3 Then
                    If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(3).OverallWidth.ToString()) Then
                        objTRMeasurementSectionData.OverallWidth4 = p_objMeasure.MeasureDetails(3).OverallWidth.ToString()
                    Else
                        objTRMeasurementSectionData.OverallWidth4 = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(3).SectionWidth.ToString()) Then
                        objTRMeasurementSectionData.SectionWidth4 = p_objMeasure.MeasureDetails(3).SectionWidth.ToString()
                    Else
                        objTRMeasurementSectionData.SectionWidth4 = String.Empty
                    End If
                End If

                If intDetailsCount > 4 Then
                    If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(4).OverallWidth.ToString()) Then
                        objTRMeasurementSectionData.OverallWidth5 = p_objMeasure.MeasureDetails(4).OverallWidth.ToString()
                    Else
                        objTRMeasurementSectionData.OverallWidth5 = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(4).SectionWidth.ToString()) Then
                        objTRMeasurementSectionData.SectionWidth5 = p_objMeasure.MeasureDetails(4).SectionWidth.ToString()
                    Else
                        objTRMeasurementSectionData.SectionWidth5 = String.Empty
                    End If

                End If
                If intDetailsCount > 5 Then
                    If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(5).OverallWidth.ToString()) Then
                        objTRMeasurementSectionData.OverallWidth6 = p_objMeasure.MeasureDetails(5).OverallWidth.ToString()
                    Else
                        objTRMeasurementSectionData.OverallWidth6 = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(p_objMeasure.MeasureDetails(5).SectionWidth.ToString()) Then
                        objTRMeasurementSectionData.SectionWidth6 = p_objMeasure.MeasureDetails(5).SectionWidth.ToString()
                    Else
                        objTRMeasurementSectionData.SectionWidth6 = String.Empty
                    End If
                End If

            End If

            objTRMeasurementSectionData.AverageWidth = p_objMeasure.AvgSectionWidth.ToString()

            objTRMeasurementSectionData.Adjustment = p_objMeasure.Adjustment

            objTRMeasurementSectionData.ActualSizeFactor = p_objMeasure.ActSizeFactor.ToString()
            objTRMeasurementSectionData.MinimumSizeFactor = p_objMeasure.MinSizeFactor.ToString()
            objTRMeasurementSectionData.Circumference = Math.Round(p_objMeasure.Diameter * 3.1416).ToString()
            objTRMeasurementSectionData.OuterDiameter = p_objMeasure.Diameter.ToString()

            objTRMeasurementSectionData.NominalDiameter = p_objMeasure.NominalDiameter.ToString()
            objTRMeasurementSectionData.NominalWidth = p_objMeasure.NominalWidth.ToString()

            objTRMeasurementSectionData.NominalWidthYN = p_objMeasure.NominalWidthPassYN
            objTRMeasurementSectionData.NominalDifference = (p_objMeasure.AvgSectionWidth - p_objMeasure.NominalWidth).ToString()

            objTRMeasurementSectionData.NominalTolerance = p_objMeasure.NominalWidthTolerance.ToString()
            objTRMeasurementSectionData.MaxOverallWidth = p_objMeasure.MaxOverallWidth.ToString()
            objTRMeasurementSectionData.MaxOverallDiameter = p_objMeasure.MaxOverallDiameter.ToString()
            objTRMeasurementSectionData.MinOverallDiameter = p_objMeasure.MinOverallDiameter.ToString()

            objTRMeasurementSectionData.OW = p_objMeasure.AvgOverallWidth.ToString()
            objTRMeasurementSectionData.OWYN = p_objMeasure.OverallWidthPassYN

            objTRMeasurementSectionData.OD = p_objMeasure.Diameter.ToString()
            objTRMeasurementSectionData.ODYN = p_objMeasure.OverallDiameterPassYN
            objTRMeasurementSectionData.OverallDifference = (p_objMeasure.Diameter - p_objMeasure.NominalDiameter).ToString()
            objTRMeasurementSectionData.OverallTolerance = CStr(p_objMeasure.DiameterTolerance)

            objTRMeasurementSectionData.RimRim = p_objMeasure.RimWidth.ToString()
            objTRMeasurementSectionData.RimPressure = p_objMeasure.InflationPressure.ToString()

            objTRMeasurementSectionData.DiameterXRimWidth = p_objMeasure.Diameter.ToString() & "X" & p_objMeasure.RimWidth.ToString()

            'Treadwear section
            objTRMeasurementSectionData.TreadwearIndicatorsResult = p_objTreadwear.LowestWearBar.ToString()
            objTRMeasurementSectionData.TreadwearIndicatorsRequirement = p_objTreadwear.IndicatorRequirement.ToString()
            objTRMeasurementSectionData.TreadwearIndicatorsYN = p_objTreadwear.PassYN

            intFieldCount = 6
            intDetailsCount = p_objTreadwear.TreadwearDetails.Count()
            intDetailsCount = Math.Min(intDetailsCount, intFieldCount)

            Dim intIndex As Integer = 1
            Do While (intIndex <= intDetailsCount)
                Dim field As System.Reflection.FieldInfo = objTRMeasurementSectionData.GetType().GetField(treadwearHeight + intIndex.ToString())
                field.SetValue(objTRMeasurementSectionData, p_objTreadwear.TreadwearDetails(intIndex - 1).WearBarHeight.ToString())
                intIndex = (intIndex + 1)
            Loop

            'Plunger section

            objTRMeasurementSectionData.PlungerAverageJ = p_objPlunger.MinPlunger.ToString()
            objTRMeasurementSectionData.PlungerYN = p_objPlunger.PassYN

            intFieldCount = 5
            intDetailsCount = p_objPlunger.PlungerDetails.Count()
            intDetailsCount = Math.Min(intDetailsCount, intFieldCount)

            intIndex = 1
            Dim intPlungerTotal As Integer 'For calculating average
            Do While (intIndex <= intDetailsCount)
                Dim field As System.Reflection.FieldInfo = objTRMeasurementSectionData.GetType().GetField(plunger + intIndex.ToString())
                field.SetValue(objTRMeasurementSectionData, p_objPlunger.PlungerDetails(intIndex - 1).BreakingEnergy.ToString())

                intPlungerTotal = intPlungerTotal + p_objPlunger.PlungerDetails(intIndex - 1).BreakingEnergy
                intIndex = (intIndex + 1)
            Loop

            If intPlungerTotal > 0 Then
                p_objPlunger.AVGBreakingEnergy = CInt((intPlungerTotal / intDetailsCount))
                objTRMeasurementSectionData.PlungerAverage = p_objPlunger.AVGBreakingEnergy.ToString()
            Else
                objTRMeasurementSectionData.PlungerAverage = p_objPlunger.AVGBreakingEnergy.ToString()
            End If

            'BeadUnseat section
            If p_intCertificationTypeId = 2 Then 'GSO
                objTRMeasurementSectionData.BeadUnseatTestYN = p_objBeadUnSeat.PassYN

                intFieldCount = 5
                intDetailsCount = p_objBeadUnSeat.BeadUnSeatDetails.Count()
                intDetailsCount = Math.Min(intDetailsCount, intFieldCount)

                intIndex = 1
                objTRMeasurementSectionData.BeadUnseatTestKN = p_objBeadUnSeat.LowestUnSeatValue.ToString()
                p_objBeadUnSeat.MinBeadUnseat = p_objBeadUnSeat.LowestUnSeatValue
                Do While (intIndex <= intDetailsCount)
                    Dim field As System.Reflection.FieldInfo = objTRMeasurementSectionData.GetType().GetField(beadUnseatTest + intIndex.ToString())
                    field.SetValue(objTRMeasurementSectionData, p_objBeadUnSeat.BeadUnSeatDetails(intIndex - 1).UnSeatForce.ToString())

                    intIndex = (intIndex + 1)
                Loop
            Else
                objTRMeasurementSectionData.BeadUnseatTestYN = p_objBeadUnSeat.PassYN

                intFieldCount = 5
                intDetailsCount = p_objBeadUnSeat.BeadUnSeatDetails.Count()
                intDetailsCount = Math.Min(intDetailsCount, intFieldCount)

                intIndex = 1
                Dim intUnseatForceTotal As Integer 'For calculating the average
                Do While (intIndex <= intDetailsCount)
                    Dim field As System.Reflection.FieldInfo = objTRMeasurementSectionData.GetType().GetField(beadUnseatTest + intIndex.ToString())
                    field.SetValue(objTRMeasurementSectionData, p_objBeadUnSeat.BeadUnSeatDetails(intIndex - 1).UnSeatForce.ToString())

                    intUnseatForceTotal = intUnseatForceTotal + p_objBeadUnSeat.BeadUnSeatDetails(intIndex - 1).UnSeatForce
                    intIndex = (intIndex + 1)
                Loop

                If intUnseatForceTotal > 0 Then
                    If p_intCertificationTypeId = Depository.Current.GetCertificationTypeID(certificationTypeName2) Then
                        intUnseatForceTotal = intUnseatForceTotal - p_objBeadUnSeat.BeadUnSeatDetails(0).UnSeatForce
                        p_objBeadUnSeat.MinBeadUnseat = CInt((intUnseatForceTotal / (intDetailsCount - 1)).ToString)
                        p_objBeadUnSeat.LowestUnSeatValue = CInt((intUnseatForceTotal / (intDetailsCount - 1)).ToString)
                        objTRMeasurementSectionData.BeadUnseatTestKN = CStr(p_objBeadUnSeat.MinBeadUnseat)
                    Else
                        p_objBeadUnSeat.MinBeadUnseat = CInt((intUnseatForceTotal / intDetailsCount).ToString)
                        p_objBeadUnSeat.LowestUnSeatValue = CInt((intUnseatForceTotal / intDetailsCount).ToString)
                        objTRMeasurementSectionData.BeadUnseatTestKN = CStr(p_objBeadUnSeat.MinBeadUnseat)
                    End If
                Else
                    objTRMeasurementSectionData.BeadUnseatTestKN = CStr(p_objBeadUnSeat.MinBeadUnseat)
                End If
            End If

            objTRMeasurementSectionData.TensileStrength1 = p_objMeasure.TensileStrength1.ToString()
            objTRMeasurementSectionData.TensileStrength2 = p_objMeasure.TensileStrength2.ToString()
            objTRMeasurementSectionData.Elongation1 = p_objMeasure.Elongation1.ToString()
            objTRMeasurementSectionData.Elongation2 = p_objMeasure.Elongation2.ToString()
            objTRMeasurementSectionData.TensileStrengthafterAging1 = p_objMeasure.TensileStrengthAfterAging1.ToString()
            objTRMeasurementSectionData.TensileStrengthafterAging2 = p_objMeasure.TensileStrengthAfterAging2.ToString()
            objTRMeasurementSectionData.TemperatureResistanceGrading = p_objMeasure.TemperatureResistanceGrading

            If p_objMeasure.CertificateNumberID > 0 Then
                objTRMeasurementSectionData.OriginalMeasure = p_objMeasure
                objTRMeasurementSectionData.OriginalTreadwear = p_objTreadwear
                objTRMeasurementSectionData.OriginalPlunger = p_objPlunger
                objTRMeasurementSectionData.OriginalBeadUnSeat = p_objBeadUnSeat
            End If

            Return objTRMeasurementSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map Endurance Before Section Data.
    ''' </summary>
    ''' <returns>TREnduranceTestGeneralBeforeSectionData</returns> 
    ''' <param name="p_objEndurance">Endurance.</param>
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
    Private Function MapEnduranceBeforeSectionData(ByVal p_objEndurance As Endurance) As TREnduranceTestGeneralBeforeSectionData
        Try
            Dim objTREnduranceBeforeSectionData As New TREnduranceTestGeneralBeforeSectionData

            objTREnduranceBeforeSectionData.EnduranceCircumferenceBefore = CStr(p_objEndurance.BeforeDiameter * CDbl(3.1416.ToString()))
            objTREnduranceBeforeSectionData.EnduranceTestMachine = p_objEndurance.WheelNumber.ToString()
            objTREnduranceBeforeSectionData.EnduranceDiameterTestDrum = p_objEndurance.DiameterTestDrum.ToString()
            'NOTE: missing Contour parameter for calculation!!
            objTREnduranceBeforeSectionData.EnduranceTestRim = (p_objEndurance.RimDiameter + p_objEndurance.RimWidth).ToString()

            'Date of Manufacture is Serial Date from TRACS API
            If p_objEndurance.SerialDate.Equals(DateTime.MinValue) Then
                objTREnduranceBeforeSectionData.EnduranceDateOfManufacture = String.Empty
            Else
                objTREnduranceBeforeSectionData.EnduranceDateOfManufacture = p_objEndurance.SerialDate.ToShortDateString()
            End If

            objTREnduranceBeforeSectionData.EnduranceTestWheelPosition = p_objEndurance.WheelPosition.ToString()
            objTREnduranceBeforeSectionData.EnduranceTireSerialNumber = p_objEndurance.DotSerialNumber
            objTREnduranceBeforeSectionData.EnduranceDOTCode = p_objEndurance.DotSerialNumber

            'Preconditioning Temp
            objTREnduranceBeforeSectionData.EndurancePreconditioningTemperature = p_objEndurance.PrecondEndTemp.ToString()

            'Preconditioning Time
            If p_objEndurance.PrecondTime = 0 Then
                objTREnduranceBeforeSectionData.EndurancePreconditioningTime = Math.Abs((p_objEndurance.PrecondStartDate - p_objEndurance.PrecondEndDate).Hours).ToString()
            Else
                objTREnduranceBeforeSectionData.EndurancePreconditioningTime = p_objEndurance.PrecondTime.ToString()
            End If

            objTREnduranceBeforeSectionData.EnduranceInflationPressure = p_objEndurance.InflationPressure.ToString()
            objTREnduranceBeforeSectionData.EnduranceInflationPressureAdjusted = p_objEndurance.InflationPressureReadjusted.ToString()
            objTREnduranceBeforeSectionData.EnduranceOuterDiameterBefore = p_objEndurance.BeforeDiameter.ToString()
            objTREnduranceBeforeSectionData.EnduranceTestInflationPressureBefore = p_objEndurance.BeforeInflation.ToString()

            Return objTREnduranceBeforeSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map Endurance Section Data.
    ''' </summary>
    ''' <returns>TREnduranceSectionData</returns> 
    ''' <param name="p_objEndurance">Endurance.</param>
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
    Private Function MapEnduranceSectionData(ByVal p_objEndurance As Endurance) As TREnduranceSectionData
        Dim enduranceDate As String = "EnduranceDate"
        Dim enduranceTotalKm As String = "EnduranceTotalKm"
        Dim enduranceClockTime As String = "EnduranceClockTime"
        Dim enduranceAirPressure As String = "EnduranceAirPressure"
        Dim enduranceRoomTemperature As String = "EnduranceRoomTemperature"
        Dim enduranceLoadPercentage As String = "EnduranceLoadPercentage"
        Dim enduranceLoadKG As String = "EnduranceLoadKG"
        Dim enduranceSpeed As String = "EnduranceSpeed"
        Try
            Dim objTREnduranceSectionData As New TREnduranceSectionData

            Dim TotalHour As Short = 0

            objTREnduranceSectionData.EnduranceMatlNum = p_objEndurance.MaterialNumber
            objTREnduranceSectionData.GTSpecEnduranceMatlNum = p_objEndurance.GTSpecMaterialNumber

            For Each enduranceDetail As EnduranceDetail In p_objEndurance.EnduranceDetails

                ' NOTE: Field names are 0-based

                If enduranceDetail.Iteration = 0 Then
                    objTREnduranceSectionData.EnduranceTemperatureBefore = enduranceDetail.AmbTemp.ToString()
                    objTREnduranceSectionData.EnduranceDate0 = enduranceDetail.StepCompletionDate.ToShortDateString()
                    objTREnduranceSectionData.EnduranceClockTime0 = enduranceDetail.StepCompletionDate.ToShortTimeString()

                Else
                    If enduranceDetail.Iteration = 1 Then
                        objTREnduranceSectionData.EnduranceSpeed = enduranceDetail.Speed.ToString()
                    End If

                    Dim DateField As System.Reflection.FieldInfo = objTREnduranceSectionData.GetType().GetField(enduranceDate + (enduranceDetail.Iteration).ToString())
                    DateField.SetValue(objTREnduranceSectionData, enduranceDetail.StepCompletionDate.ToShortDateString())

                    Dim HourField As System.Reflection.FieldInfo = objTREnduranceSectionData.GetType().GetField(enduranceDate + (enduranceDetail.Iteration).ToString())
                    HourField.SetValue(objTREnduranceSectionData, enduranceDetail.TimeInMin.ToString())

                    Dim TotalKmfield As System.Reflection.FieldInfo = objTREnduranceSectionData.GetType().GetField(enduranceTotalKm + (enduranceDetail.Iteration).ToString())
                    TotalKmfield.SetValue(objTREnduranceSectionData, enduranceDetail.TotMiles.ToString())

                    Dim Clockfield As System.Reflection.FieldInfo = objTREnduranceSectionData.GetType().GetField(enduranceClockTime + (enduranceDetail.Iteration).ToString())
                    Clockfield.SetValue(objTREnduranceSectionData, enduranceDetail.StepCompletionDate.ToShortTimeString)

                    Dim AirPressurefield As System.Reflection.FieldInfo = objTREnduranceSectionData.GetType().GetField(enduranceAirPressure + (enduranceDetail.Iteration).ToString())
                    AirPressurefield.SetValue(objTREnduranceSectionData, enduranceDetail.InfPressure.ToString())

                    If enduranceDetail.AmbTemp <> -1 Then
                        Dim RoomTempfield As System.Reflection.FieldInfo = objTREnduranceSectionData.GetType().GetField(enduranceRoomTemperature + (enduranceDetail.Iteration).ToString())
                        RoomTempfield.SetValue(objTREnduranceSectionData, enduranceDetail.AmbTemp.ToString())
                    Else
                        Dim RoomTempfield As System.Reflection.FieldInfo = objTREnduranceSectionData.GetType().GetField(enduranceRoomTemperature + (enduranceDetail.Iteration).ToString())
                        RoomTempfield.SetValue(objTREnduranceSectionData, String.Empty)
                    End If

                    Dim Loadfield As System.Reflection.FieldInfo = objTREnduranceSectionData.GetType().GetField(enduranceLoadKG + (enduranceDetail.Iteration).ToString())
                    Loadfield.SetValue(objTREnduranceSectionData, enduranceDetail.Load.ToString())

                    Dim LoadPercfield As System.Reflection.FieldInfo = objTREnduranceSectionData.GetType().GetField(enduranceLoadPercentage + (enduranceDetail.Iteration).ToString())
                    LoadPercfield.SetValue(objTREnduranceSectionData, enduranceDetail.LoadPercent.ToString())

                    Dim Speedfield As System.Reflection.FieldInfo = objTREnduranceSectionData.GetType().GetField(enduranceSpeed + (enduranceDetail.Iteration).ToString())
                    Speedfield.SetValue(objTREnduranceSectionData, enduranceDetail.Speed.ToString())
                End If
                TotalHour = TotalHour + enduranceDetail.TimeInMin
            Next

            'Low Pressure Start Inflation
            objTREnduranceSectionData.EnduranceLowPressureStartInflation = p_objEndurance.LowInfStartInflation.ToString()

            'Low Pressure End Inflation
            objTREnduranceSectionData.EnduranceLowPressureEndInflation = p_objEndurance.LowInfEndInflation.ToString()

            'Low Pressure End Temp
            objTREnduranceSectionData.EnduranceLowPressureEndTemp = p_objEndurance.LowInfEndTemp.ToString()

            objTREnduranceSectionData.EnduranceFinalTotalKM = p_objEndurance.FinalDistance.ToString()
            objTREnduranceSectionData.EnduranceTestPassYN = p_objEndurance.EnduranceTestPassYN
            objTREnduranceSectionData.EnduranceInflationPressureBefore = p_objEndurance.InflationPressure.ToString()
            objTREnduranceSectionData.EnduranceInflationPressureAfter = p_objEndurance.FinalInflation.ToString()
            objTREnduranceSectionData.EnduranceTemperatureAfter = p_objEndurance.FinalTemp.ToString()


            If p_objEndurance.EnduranceXHour > 0 Then
                objTREnduranceSectionData.EnduranceXHours = CStr(p_objEndurance.EnduranceXHour)
            Else
                objTREnduranceSectionData.EnduranceXHours = (TotalHour / 60).ToString()
            End If
            objTREnduranceSectionData.EnduranceTestResultYN = p_objEndurance.ResultPassFail
            objTREnduranceSectionData.PossibleFailuresFound = p_objEndurance.PossibleFailuresFound

            If p_objEndurance.CertificateNumberID > 0 Then
                objTREnduranceSectionData.OriginalEndurance = p_objEndurance
            End If

            Return objTREnduranceSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map Endurance After Section Data.
    ''' </summary>
    ''' <returns>TREnduranceTestGeneralAfterSectionData</returns> 
    ''' <param name="p_objEndurance">Endurance.</param>
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
    Private Function MapEnduranceAfterSectionData(ByVal p_objEndurance As Endurance) As TREnduranceTestGeneralAfterSectionData
        Try
            Dim objTREnduranceAfterSectionData As New TREnduranceTestGeneralAfterSectionData

            'Post conditioning Time
            If p_objEndurance.PostcondTime = 0 Then
                objTREnduranceAfterSectionData.EndurancePostConditioningTime = Math.Abs((p_objEndurance.PostcondStartDate - p_objEndurance.PostcondEndDate).Hours).ToString()
            Else
                objTREnduranceAfterSectionData.EndurancePostConditioningTime = p_objEndurance.PostcondTime.ToString()
            End If

            objTREnduranceAfterSectionData.EnduranceCircumferenceAfter = Math.Round(p_objEndurance.AfterDiameter * 3.1416).ToString()
            objTREnduranceAfterSectionData.EnduranceOuterDiameterAfter = p_objEndurance.AfterDiameter.ToString()
            objTREnduranceAfterSectionData.EnduranceTestInflationPressureAfter = p_objEndurance.AfterInflation.ToString()
            objTREnduranceAfterSectionData.EnduranceDifferenceOuterDiameterMMAfter = (p_objEndurance.AfterDiameter - p_objEndurance.BeforeDiameter).ToString()
            objTREnduranceAfterSectionData.EnduranceDifferenceOuterDiameterToleranceAfter = p_objEndurance.OuterDiameterTolerance.ToString()
            objTREnduranceAfterSectionData.EnduranceSeriesAfter = p_objEndurance.SerieNOM
            objTREnduranceAfterSectionData.EnduranceFinalJudgement = p_objEndurance.FinalJudgement
            objTREnduranceAfterSectionData.EnduranceApproverAfter = p_objEndurance.Approver

            Return objTREnduranceAfterSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map HighSpeed Before Section Data.
    ''' </summary>
    ''' <returns>TRHighSpeedTestGeneralBeforeSectionData object</returns> 
    ''' <param name="p_objHighSpeed">High Speed.</param>
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
    Private Function MapHighSpeedBeforeSectionData(ByVal p_objHighSpeed As HighSpeed) As TRHighSpeedTestGeneralBeforeSectionData
        Try
            Dim objTRHighSpeedBeforeSectionData As New TRHighSpeedTestGeneralBeforeSectionData

            If p_objHighSpeed.WheelNumber = -1 Then
                objTRHighSpeedBeforeSectionData.HighSpeedTestMachine = String.Empty
            Else
                objTRHighSpeedBeforeSectionData.HighSpeedTestMachine = p_objHighSpeed.WheelNumber.ToString()
            End If
            objTRHighSpeedBeforeSectionData.HighSpeedDiameterTestDrum = p_objHighSpeed.DiameterTestDrum.ToString()

            'NOTE missing Contour parameter for calculation!!
            objTRHighSpeedBeforeSectionData.HighSpeedTestRim = (p_objHighSpeed.RimDiameter + p_objHighSpeed.RimWidth).ToString()

            'Date of Manufacture is Serial Date from TRACS API
            If p_objHighSpeed.SerialDate.Equals(DateTime.MinValue) Then
                objTRHighSpeedBeforeSectionData.HighSpeedDateOfManufacture = String.Empty
            Else
                objTRHighSpeedBeforeSectionData.HighSpeedDateOfManufacture = p_objHighSpeed.SerialDate.ToShortDateString()
            End If

            If p_objHighSpeed.WheelPosition = -1 Then
                objTRHighSpeedBeforeSectionData.HighSpeedTestWheelPosition = String.Empty
            Else
                objTRHighSpeedBeforeSectionData.HighSpeedTestWheelPosition = p_objHighSpeed.WheelPosition.ToString()
            End If
            objTRHighSpeedBeforeSectionData.HighSpeedTireSerialNumber = p_objHighSpeed.DotSerialNumber
            objTRHighSpeedBeforeSectionData.HighSpeedDOTCode = p_objHighSpeed.DotSerialNumber

            'Preconditioning Temp
            objTRHighSpeedBeforeSectionData.HighSpeedPreconditioningTemperature = p_objHighSpeed.PrecondTemp.ToString()

            'Preconditioning Time
            If p_objHighSpeed.PrecondTime = 0 Then
                objTRHighSpeedBeforeSectionData.HighSpeedPreconditioningTime = Math.Abs((p_objHighSpeed.PrecondStartDate - p_objHighSpeed.PrecondEndDate).Hours).ToString()
            Else
                objTRHighSpeedBeforeSectionData.HighSpeedPreconditioningTime = p_objHighSpeed.PrecondTime.ToString()
            End If

            objTRHighSpeedBeforeSectionData.HighSpeedInflationPressure = p_objHighSpeed.InflationPressure.ToString()

            objTRHighSpeedBeforeSectionData.HighSpeedInflationPressureAdjusted = p_objHighSpeed.InflationPressureReadjusted.ToString()
            objTRHighSpeedBeforeSectionData.HighSpeedCircumferenceBefore = Math.Round(p_objHighSpeed.BeforeDiameter * 3.1416).ToString()
            objTRHighSpeedBeforeSectionData.HighSpeedOuterDiameterBefore = p_objHighSpeed.BeforeDiameter.ToString()
            objTRHighSpeedBeforeSectionData.HighSpeedTestInflationPressureBefore = p_objHighSpeed.BeforeInflation.ToString()

            Return objTRHighSpeedBeforeSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map HighSpeed Section Data.
    ''' </summary>
    ''' <returns>TRHighSpeedSectionData object</returns> 
    ''' <param name="p_objHighSpeed">HighSpeed.</param>
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
    Private Function MapHighSpeedSectionData(ByVal p_objHighSpeed As HighSpeed) As TRHighSpeedSectionData
        Dim highSpeedDurationStep As String = "HighSpeedDurationStep"
        Dim highSpeedDurationTime As String = "HighSpeedDurationTime"
        Dim highSpeedDurationDate As String = "HighSpeedDurationDate"
        Dim highSpeedDurationTotalTime As String = "HighSpeedDurationTotalTime"
        Dim highSpeedDurationActSpeed As String = "HighSpeedDurationActSpeed"
        Dim highSpeedDurationRoomTemp As String = "HighSpeedDurationRoomTemp"
        Dim highSpeedDurationLoad As String = "HighSpeedDurationLoad"
        Dim highSpeedDurationLoadPerc As String = "HighSpeedDurationLoadPerc"
        Dim highSpeedSpeedTestTime As String = "HighSpeedSpeedTestTime"
        Dim highSpeedSpeedTestSpeed As String = "HighSpeedSpeedTestSpeed"

        Try
            Dim intDetailsCount As Integer ' Detail data values count
            Dim intFieldCount As Integer ' Detail field / control count

            Dim objTRHighSpeedSectionData As New TRHighSpeedSectionData

            Dim TotalHour As Short = 0
            Dim MaxSpeed As Single = 0
            Dim MaxLoad As Single = 0

            intFieldCount = 9
            intDetailsCount = p_objHighSpeed.HighSpeedDetails.Count()
            intDetailsCount = Math.Min(intDetailsCount, intFieldCount)

            objTRHighSpeedSectionData.HighSpeedMatlNum = p_objHighSpeed.MaterialNumber
            objTRHighSpeedSectionData.GTSpecHighSpeedMatlNum = p_objHighSpeed.GTSpecMaterialNumber

            For i As Integer = 0 To intDetailsCount - 1

                Dim objHighSpeedDetail As HighSpeedDetail = p_objHighSpeed.HighSpeedDetails(i)

                If objHighSpeedDetail.TestStep = 0 Then
                    objTRHighSpeedSectionData.HighSpeedTempBefore = objHighSpeedDetail.AmbTemp.ToString()
                End If

                ' NOTE: Field names are 0-based
                Dim StepField As System.Reflection.FieldInfo = objTRHighSpeedSectionData.GetType().GetField(highSpeedDurationStep + objHighSpeedDetail.Iteration.ToString())
                StepField.SetValue(objTRHighSpeedSectionData, objHighSpeedDetail.TestStep.ToString())

                If objHighSpeedDetail.StepCompletionDate <> DateTime.MinValue Then
                    Dim DateField As System.Reflection.FieldInfo = objTRHighSpeedSectionData.GetType().GetField(highSpeedDurationDate + objHighSpeedDetail.Iteration.ToString())
                    DateField.SetValue(objTRHighSpeedSectionData, objHighSpeedDetail.StepCompletionDate.ToShortDateString())
                    Dim TimeField As System.Reflection.FieldInfo = objTRHighSpeedSectionData.GetType().GetField(highSpeedDurationDate + objHighSpeedDetail.Iteration.ToString())
                    TimeField.SetValue(objTRHighSpeedSectionData, objHighSpeedDetail.StepCompletionDate.ToShortTimeString())
                End If
                Dim TimeTotalTimefield As System.Reflection.FieldInfo = objTRHighSpeedSectionData.GetType().GetField(highSpeedDurationTotalTime + objHighSpeedDetail.Iteration.ToString())
                TimeTotalTimefield.SetValue(objTRHighSpeedSectionData, objHighSpeedDetail.TimeInMin.ToString())
                Dim Speedfield As System.Reflection.FieldInfo = objTRHighSpeedSectionData.GetType().GetField(highSpeedDurationActSpeed + objHighSpeedDetail.Iteration.ToString())
                Speedfield.SetValue(objTRHighSpeedSectionData, objHighSpeedDetail.Speed.ToString())

                If objHighSpeedDetail.AmbTemp = -1 Then
                    Dim RoomTempfield As System.Reflection.FieldInfo = objTRHighSpeedSectionData.GetType().GetField(highSpeedDurationRoomTemp + objHighSpeedDetail.Iteration.ToString())
                    RoomTempfield.SetValue(objTRHighSpeedSectionData, String.Empty)
                Else
                    Dim RoomTempfield As System.Reflection.FieldInfo = objTRHighSpeedSectionData.GetType().GetField(highSpeedDurationRoomTemp + objHighSpeedDetail.Iteration.ToString())
                    RoomTempfield.SetValue(objTRHighSpeedSectionData, objHighSpeedDetail.AmbTemp.ToString())
                End If

                Dim Loadfield As System.Reflection.FieldInfo = objTRHighSpeedSectionData.GetType().GetField(highSpeedDurationLoad + objHighSpeedDetail.Iteration.ToString())
                Loadfield.SetValue(objTRHighSpeedSectionData, objHighSpeedDetail.Load.ToString())
                Dim LoadPercfield As System.Reflection.FieldInfo = objTRHighSpeedSectionData.GetType().GetField(highSpeedDurationLoadPerc + objHighSpeedDetail.Iteration.ToString())
                LoadPercfield.SetValue(objTRHighSpeedSectionData, objHighSpeedDetail.LoadPercent.ToString())

                TotalHour = TotalHour + objHighSpeedDetail.TimeInMin
            Next

            'Show the range for the first test step in high speed duration test table
            If intDetailsCount > 0 Then
                objTRHighSpeedSectionData.HighSpeedDurationStep0 = objTRHighSpeedSectionData.HighSpeedDurationStep0 + " - " + objTRHighSpeedSectionData.HighSpeedDurationStep1
            End If

            objTRHighSpeedSectionData.HighSpeedTotalTime = CStr(p_objHighSpeed.SpeedTotalTime)
            objTRHighSpeedSectionData.HighSpeedMaxSpeed = CStr(p_objHighSpeed.MaxSpeed)
            objTRHighSpeedSectionData.HighSpeedMaxLoad = CStr(p_objHighSpeed.MaxLoad)
            objTRHighSpeedSectionData.HighSpeedWheelSpeedRPM = CStr(p_objHighSpeed.WheelSpeedRPM)
            objTRHighSpeedSectionData.HighSpeedWheelSpeedKMH = CStr(p_objHighSpeed.WheelSpeedKMH)

            objTRHighSpeedSectionData.HighSpeedDurationPassYN = p_objHighSpeed.DurationTestPassYN
            objTRHighSpeedSectionData.HighSpeedInfPressureBefore = CStr(p_objHighSpeed.InflationPressure)
            objTRHighSpeedSectionData.HighSpeedInfPressureAfter = CStr(p_objHighSpeed.FinalInflation)
            objTRHighSpeedSectionData.HighSpeedTempAfter = CStr(p_objHighSpeed.FinalTemp)

            intFieldCount = 9
            intDetailsCount = p_objHighSpeed.SpeedTestDetails.Count()
            intDetailsCount = Math.Min(intDetailsCount, intFieldCount)

            For i As Integer = 0 To intDetailsCount - 1

                Dim objSpeedTestDetail As SpeedTestDetail = p_objHighSpeed.SpeedTestDetails(i)

                ' NOTE: Field names are 1-based
                If objSpeedTestDetail.Iteration = 0 Then
                    objTRHighSpeedSectionData.HighSpeedSpeedTestTimeInitial = objSpeedTestDetail.Time.ToString()
                    objTRHighSpeedSectionData.HighSpeedSpeedTestSpeedInitial = objSpeedTestDetail.Speed.ToString()
                Else
                    Dim Timefield As System.Reflection.FieldInfo = objTRHighSpeedSectionData.GetType().GetField(highSpeedSpeedTestTime + objSpeedTestDetail.Iteration.ToString())
                    Timefield.SetValue(objTRHighSpeedSectionData, objSpeedTestDetail.Time.ToString())
                    Dim Speedfield As System.Reflection.FieldInfo = objTRHighSpeedSectionData.GetType().GetField(highSpeedSpeedTestSpeed + objSpeedTestDetail.Iteration.ToString())
                    Speedfield.SetValue(objTRHighSpeedSectionData, objSpeedTestDetail.Speed.ToString())
                End If

            Next

            objTRHighSpeedSectionData.HighSpeedSpeedTestPassAt = p_objHighSpeed.SpeedTestPassAt.ToString()
            objTRHighSpeedSectionData.HighSpeedSpeedTestPassYN = p_objHighSpeed.SpeedTestPassYN

            If p_objHighSpeed.CertificateNumberID > 0 Then
                objTRHighSpeedSectionData.OriginalHighSpeed = p_objHighSpeed
            End If

            Return objTRHighSpeedSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map HighSpeed After Section Data.
    ''' </summary>
    ''' <returns>TRHighSpeedTestGeneralAfterSectionData object</returns> 
    ''' <param name="p_objHighSpeed">HighSpeed.</param>
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
    Private Function MapHighSpeedAfterSectionData(ByVal p_objHighSpeed As HighSpeed) As TRHighSpeedTestGeneralAfterSectionData
        Try
            Dim objTRHighSpeedAfterSectionData As New TRHighSpeedTestGeneralAfterSectionData

            'Post Conditioning Time
            If p_objHighSpeed.PostcondTime = 0 Then
                objTRHighSpeedAfterSectionData.HighSpeedPostConditioningTime = Math.Abs((p_objHighSpeed.PostcondStartDate - p_objHighSpeed.PostcondEndDate).Hours).ToString()
            Else
                objTRHighSpeedAfterSectionData.HighSpeedPostConditioningTime = p_objHighSpeed.PostcondTime.ToString()
            End If

            objTRHighSpeedAfterSectionData.HighSpeedPostConditioningTime = (p_objHighSpeed.PostcondEndDate - p_objHighSpeed.PostcondStartDate).Hours().ToString()
            objTRHighSpeedAfterSectionData.HighSpeedCircumferenceAfter = Math.Round(p_objHighSpeed.AfterDiameter * 3.1416).ToString()
            objTRHighSpeedAfterSectionData.HighSpeedOuterDiameterAfter = p_objHighSpeed.AfterDiameter.ToString()
            objTRHighSpeedAfterSectionData.HighSpeedTestInflationPressureAfter = p_objHighSpeed.AfterInflation.ToString()
            objTRHighSpeedAfterSectionData.HighSpeedDifferenceOuterDiameterMMAfter = (p_objHighSpeed.AfterDiameter - p_objHighSpeed.BeforeDiameter).ToString()

            objTRHighSpeedAfterSectionData.HighSpeedDifferenceOuterDiameterToleranceAfter = p_objHighSpeed.OuterDiameterTolerance.ToString()
            objTRHighSpeedAfterSectionData.HighSpeedSeriesAfter = p_objHighSpeed.SerieNOM
            objTRHighSpeedAfterSectionData.HighSpeedFinalJudgement = p_objHighSpeed.FinalJudgement
            objTRHighSpeedAfterSectionData.HighSpeedApproverAfter = p_objHighSpeed.Approver

            Return objTRHighSpeedAfterSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map Sound Wet Section Data.
    ''' </summary>
    ''' <returns>TRSoundWetSectionData object</returns> 
    ''' <param name="p_objSound">Sound.</param>
    ''' <param name="p_objWetGrip">Wet Grip.</param>    
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
    Private Function MapSoundWetSectionData(ByVal p_objSound As Sound, ByVal p_objWetGrip As WetGrip) As TRSoundWetSectionData
        Dim soundSpeed As String = "SoundSpeed"
        Dim soundDirectOfRun As String = "SoundDirectOfRun"
        Dim soundLevelLeftMeasured As String = "SoundLevelLeftMeasured"
        Dim soundLevelRightMeasured As String = "SoundLevelRightMeasured"
        Dim soundAirTemp As String = "SoundAirTemp"
        Dim soundTrackTemp As String = "SoundTrackTemp"
        Dim soundLevelLeftTempCorrected As String = "SoundLevelLeftTempCorrected"
        Dim soundLevelRightTempCorrected As String = "SoundLevelRightTempCorrected"
        Dim wetSpeed As String = "WetSpeed"
        Dim WetDirectOfRun As String = "WetDirectOfRun"
        Dim WetSRTT As String = "WetSRTT"
        Dim WetCandidateTire As String = "WetCandidateTire"
        Dim WetPBFC As String = "WetPBFC"
        Dim WetMFDD As String = "WetMFDD"
        Dim WetWetGripIndex As String = "WetWetGripIndex"
        Dim WetComments As String = "WetComments"
        Try
            Debug.WriteLine("MapSoundWetSectionData")
            Dim objTRSoundWetSectionData As New TRSoundWetSectionData

            If p_objSound Is Nothing Or p_objWetGrip Is Nothing Then
                Return objTRSoundWetSectionData
            End If

            'Sound section
            objTRSoundWetSectionData.SWTestReportNo = p_objSound.TestReportNumber
            objTRSoundWetSectionData.SWManuNameOrBrandName = p_objSound.ManufactureAndBrand
            objTRSoundWetSectionData.SWTireClass = p_objSound.TireClass
            objTRSoundWetSectionData.SWCategoryOfUse = p_objSound.CategoryOfUse
            If p_objSound.DateOfTest <> DateTime.MinValue Then
                objTRSoundWetSectionData.SoundDateOfTest = p_objSound.DateOfTest.ToString()
            End If
            objTRSoundWetSectionData.SoundTestVehicle = p_objSound.TestVehicule
            objTRSoundWetSectionData.SoundTestVehicleWheelbase = p_objSound.TestVehiculeWheelbase
            objTRSoundWetSectionData.SoundLocationOfTestTrack = p_objSound.LocationOfTestTrack
            If p_objSound.DateTrackCertifToISO <> DateTime.MinValue Then
                objTRSoundWetSectionData.SoundDateOfTrackCertification = p_objSound.DateTrackCertifToISO.ToString()
            End If
            objTRSoundWetSectionData.SoundTireSizeDesignation = p_objSound.TireSizeDesignation
            objTRSoundWetSectionData.SoundTireServiceDescription = p_objSound.TireServiceDescription
            objTRSoundWetSectionData.SoundReferenceInflationPressure = p_objSound.ReferenceInflationPressure

            objTRSoundWetSectionData.SoundTestMassFL = p_objSound.TestMass_FrontL
            objTRSoundWetSectionData.SoundTestMassFR = p_objSound.TestMass_FrontR
            objTRSoundWetSectionData.SoundTestMassRL = p_objSound.TestMass_RearL
            objTRSoundWetSectionData.SoundTestMassRR = p_objSound.TestMass_RearR
            objTRSoundWetSectionData.SoundTireLoadIndexFL = p_objSound.TireLoadIndex_FrontL
            objTRSoundWetSectionData.SoundTireLoadIndexFR = p_objSound.TireLoadIndex_FrontR
            objTRSoundWetSectionData.SoundTireLoadIndexRL = p_objSound.TireLoadIndex_RearL
            objTRSoundWetSectionData.SoundTireLoadIndexRR = p_objSound.TireLoadIndex_RearR
            objTRSoundWetSectionData.SoundInflationPressureFL = p_objSound.InflationPressureCo_FrontL
            objTRSoundWetSectionData.SoundInflationPressureFR = p_objSound.InflationPressureCo_FrontR
            objTRSoundWetSectionData.SoundInflationPressureRL = p_objSound.InflationPressureCo_RearL
            objTRSoundWetSectionData.SoundInflationPressureRR = p_objSound.InflationPressureCo_RearR

            objTRSoundWetSectionData.SoundTestRimWidthCode = p_objSound.TestRimWidthCode
            objTRSoundWetSectionData.SoundTemperatureMeasurementSensorType = p_objSound.TempMeasureSensorType

            For Each SoundDetail As SoundDetail In p_objSound.SoundDetails
                Dim SpeedField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(soundSpeed + SoundDetail.Iteration.ToString())
                SpeedField.SetValue(objTRSoundWetSectionData, SoundDetail.TestSpeed.ToString())
                Dim DirectionField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(soundDirectOfRun + SoundDetail.Iteration.ToString())
                DirectionField.SetValue(objTRSoundWetSectionData, SoundDetail.DirectionOfRun.ToString())
                Dim LevelLeftField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(soundLevelLeftMeasured + SoundDetail.Iteration.ToString())
                LevelLeftField.SetValue(objTRSoundWetSectionData, SoundDetail.SoundLevelLeft.ToString())
                Dim LevelRightField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(soundLevelRightMeasured + SoundDetail.Iteration.ToString())
                LevelRightField.SetValue(objTRSoundWetSectionData, SoundDetail.SoundLevelRight.ToString())
                Dim AirTempField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(soundAirTemp + SoundDetail.Iteration.ToString())
                AirTempField.SetValue(objTRSoundWetSectionData, SoundDetail.AirTemp.ToString())
                Dim TrackTempField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(soundTrackTemp + SoundDetail.Iteration.ToString())
                TrackTempField.SetValue(objTRSoundWetSectionData, SoundDetail.TrackTemp.ToString())
                Dim LevelLeftTempField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(soundLevelLeftTempCorrected + SoundDetail.Iteration.ToString())
                LevelLeftTempField.SetValue(objTRSoundWetSectionData, SoundDetail.SoundLevelLeft_TempCorrected.ToString())
                Dim LevelRightTempField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(soundLevelRightTempCorrected + SoundDetail.Iteration.ToString())
                LevelRightTempField.SetValue(objTRSoundWetSectionData, SoundDetail.SoundLevelRight_TempCorrected.ToString())
            Next

            'WetGrip section
            If p_objWetGrip.DateOfTest <> DateTime.MinValue Then
                objTRSoundWetSectionData.WetDateOfTest = p_objWetGrip.DateOfTest.ToString()
            End If
            objTRSoundWetSectionData.WetTestVehicle = p_objWetGrip.TestVehicle
            objTRSoundWetSectionData.WetLocationOfTestTrack = p_objWetGrip.LocationOfTestTrack
            objTRSoundWetSectionData.WetTestTrackCharacteristics = p_objWetGrip.TestTrackCharacteristics
            objTRSoundWetSectionData.WetIssuedBy = p_objWetGrip.IssueBy
            objTRSoundWetSectionData.WetMethodOfCertification = p_objWetGrip.MethodOfCertification
            objTRSoundWetSectionData.WetTestTireDetail = p_objWetGrip.TestTireDetails
            objTRSoundWetSectionData.WetTireSizeDesignation = p_objWetGrip.TireSizeAndServiceDesc
            objTRSoundWetSectionData.wetTireBrand = p_objWetGrip.TireBrandAndTradeDesc
            objTRSoundWetSectionData.WetReferenceInflationPressure = p_objWetGrip.ReferenceInflationPressure

            objTRSoundWetSectionData.WetTestTireLoadSRTT = p_objWetGrip.TestTireLoad_SRTT
            objTRSoundWetSectionData.WetTestTireLoadCandidate = p_objWetGrip.TestTireLoad_Candidate
            objTRSoundWetSectionData.WetTestTireLoadControl = p_objWetGrip.TestTireLoad_Control
            objTRSoundWetSectionData.WetWaterDepthSRTT = p_objWetGrip.WaterDepth_SRTT
            objTRSoundWetSectionData.WetWaterDepthCandidate = p_objWetGrip.WaterDepth_Candidate
            objTRSoundWetSectionData.WetWaterDepthControl = p_objWetGrip.WaterDepth_Control
            objTRSoundWetSectionData.WetWettedTrackTemperature = p_objWetGrip.WettedTrackTempAvg

            objTRSoundWetSectionData.WetTestRimWidthCode = p_objWetGrip.TestRimWithCode
            objTRSoundWetSectionData.WetTemperatureMeasurementSensorType = p_objWetGrip.TempMeasureSensorType
            objTRSoundWetSectionData.WetIdentificationOfSRTT = p_objWetGrip.IdentificationSRTT

            For Each WetGripDetail As WetGripDetail In p_objWetGrip.WetGripDetails
                Dim SpeedField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(wetSpeed + WetGripDetail.Iteration.ToString())
                SpeedField.SetValue(objTRSoundWetSectionData, WetGripDetail.TestSpeed.ToString())
                Dim DirectionField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(WetDirectOfRun + WetGripDetail.Iteration.ToString())
                DirectionField.SetValue(objTRSoundWetSectionData, WetGripDetail.DirectionOfRun.ToString())
                Dim SRTTField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(WetSRTT + WetGripDetail.Iteration.ToString())
                SRTTField.SetValue(objTRSoundWetSectionData, WetGripDetail.SRTT.ToString())
                Dim CandidateField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(WetCandidateTire + WetGripDetail.Iteration.ToString())
                CandidateField.SetValue(objTRSoundWetSectionData, WetGripDetail.CandidateTire.ToString())
                Dim PBFCField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(WetPBFC + WetGripDetail.Iteration.ToString())
                PBFCField.SetValue(objTRSoundWetSectionData, WetGripDetail.PeakBreakForceCoeficient.ToString())
                Dim MFDDField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(WetMFDD + WetGripDetail.Iteration.ToString())
                MFDDField.SetValue(objTRSoundWetSectionData, WetGripDetail.MeanFullyDevelopedDeceleration.ToString())
                Dim WetGripIndexField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(WetWetGripIndex + WetGripDetail.Iteration.ToString())
                WetGripIndexField.SetValue(objTRSoundWetSectionData, WetGripDetail.WetGripIndex.ToString())
                Dim CommentsField As System.Reflection.FieldInfo = objTRSoundWetSectionData.GetType().GetField(WetComments + WetGripDetail.Iteration.ToString())
                CommentsField.SetValue(objTRSoundWetSectionData, WetGripDetail.Comments.ToString())
            Next

            If p_objSound.CertificateNumberID > 0 Then
                objTRSoundWetSectionData.OriginalSound = p_objSound
                objTRSoundWetSectionData.OriginalWetGrip = p_objWetGrip
            End If

            Return objTRSoundWetSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Objects</returns> 
    ''' <param name="p_blnIsTransactional">Is Transactional.</param>
    ''' <param name="p_objProduct">Product</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function SaveTestResults(ByVal p_objProduct As Product, ByVal p_blnIsTransactional As Boolean) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            If p_blnIsTransactional Then
                enumSaveResult = SaveTestResults(p_objProduct)
            Else
                enumSaveResult = SaveTestResults_NoTransaction(p_objProduct)
            End If

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Save Product data to database.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Object</returns> 
    ''' <param name="p_objProduct">Product</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults(ByVal p_objProduct As Product) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            enumSaveResult = SaveTestResults_NoTransaction(p_objProduct)

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Object</returns> 
    ''' <param name="p_objProduct">Product</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults_NoTransaction(ByVal p_objProduct As Product) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            Dim chrTubelessYN As String = CStr(IIf(p_objProduct.TubelessYN, trueValue, falseValue))
            Dim chrReinforcedYN As String = CStr(IIf(p_objProduct.ReinforcedYN, trueValue, falseValue))
            Dim chrExtraLoadYN As String = CStr(IIf(p_objProduct.ExtraLoadYN, trueValue, falseValue))
            Dim chrMudSnowYN As String = CStr(IIf(p_objProduct.MudSnow, trueValue, falseValue))
            Dim chrRegroovableYN As String = CStr(IIf(p_objProduct.RegroovableInd, trueValue, falseValue))
            Dim chrSevereWeatherIndicator As String = CStr(IIf(p_objProduct.SevereWeatherIndicator, trueValue, falseValue))
            If Not String.IsNullOrEmpty(p_objProduct.TireId) Then
                p_objProduct.TireTypeId = CInt(p_objProduct.TireId)
            End If
            enumSaveResult = Depository.Current.Save_Product(p_objProduct.SKUID, _
                                                p_objProduct.MaterialNumber, _
                                                p_objProduct.Brand, _
                                                 p_objProduct.BrandLine, _
                                                p_objProduct.TireTypeId, _
                                                p_objProduct.PSN, _
                                                p_objProduct.TireSizeStamp, _
                                                p_objProduct.DiscontinuedDate, _
                                                p_objProduct.SpecNumber, _
                                                p_objProduct.SpeedRating, _
                                                p_objProduct.SingLoadIndex, _
                                                p_objProduct.DualLoadIndex, _
                                                p_objProduct.BiasBeltedRadial, _
                                                chrTubelessYN, _
                                                chrReinforcedYN, _
                                                chrExtraLoadYN, _
                                                p_objProduct.UTQGTreadwear, _
                                                p_objProduct.UTQGTraction, _
                                                p_objProduct.UTQGTemp, _
                                                chrMudSnowYN, _
                                                p_objProduct.RimDiameter, _
                                                p_objProduct.SerialDate, _
                                                p_objProduct.BrandDesc, _
                                                p_objProduct.MeaRimWidth, _
                                                p_objProduct.LoadRange, _
                                                chrRegroovableYN, _
                                                p_objProduct.PlantProduced, _
                                                p_objProduct.MostRecentTestDate, _
                                                p_objProduct.IMark, _
                                                p_objProduct.InformeNumber, _
                                                p_objProduct.FechaDate, _
                                                p_objProduct.TreadPattern, _
                                                p_objProduct.SpecialProtectiveBand, _
                                                p_objProduct.NominalTireWidth, _
                                                p_objProduct.AspectRatio, _
                                                p_objProduct.TreadwearIndicator, _
                                                p_objProduct.NameOfManufacture, _
                                                p_objProduct.Family, _
                                                p_objProduct.DOTSerialNumber, _
                                                p_objProduct.TPN, _
                                                SecurityModel.GetUserName, _
                                                chrSevereWeatherIndicator, _
                                                p_objProduct.MFGWWYY)

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult object</returns> 
    ''' <param name="p_blnIsTransactional">p_bln IsTransactional.</param>    
    ''' <param name="p_objBeadUnseat">p_obj BeadUnseat</param>
    ''' <param name="p_objEndurance">obj Endurance</param>
    ''' <param name="p_objHighSpeed">obj HighSpeed</param>
    ''' <param name="p_objMeasure">Obj Measure</param>
    ''' <param name="p_objPlunger">obj Plunger</param>
    ''' <param name="p_objSound">obj Sound</param>
    ''' <param name="p_objTreadwear">obj Treadwear</param>
    ''' <param name="p_objWetGrip">obj Wet Grip</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function SaveTestResults(ByVal p_objMeasure As Measure, _
                                    ByVal p_objPlunger As Plunger, _
                                    ByVal p_objTreadwear As Treadwear, _
                                    ByVal p_objBeadUnseat As BeadUnSeat, _
                                    ByVal p_objEndurance As Endurance, _
                                    ByVal p_objHighSpeed As HighSpeed, _
                                    ByVal p_objSound As Sound, _
                                    ByVal p_objWetGrip As WetGrip, _
                                    ByVal p_blnIsTransactional As Boolean) As NameAid.SaveResult

        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
            Dim blnIsValid As Boolean = False

            If p_blnIsTransactional Then
                enumSaveResult = SaveTestResults(p_objMeasure)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults(p_objPlunger)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults(p_objBeadUnseat)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults(p_objTreadwear)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults(p_objEndurance)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults(p_objHighSpeed)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults(p_objSound)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults(p_objWetGrip)
            Else
                enumSaveResult = SaveTestResults_NoTransaction(p_objMeasure)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults_NoTransaction(p_objPlunger)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults_NoTransaction(p_objBeadUnseat)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults_NoTransaction(p_objTreadwear)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults_NoTransaction(p_objEndurance)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults_NoTransaction(p_objHighSpeed)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults_NoTransaction(p_objSound)
                If enumSaveResult = NameAid.SaveResult.Sucess Then enumSaveResult = SaveTestResults_NoTransaction(p_objWetGrip)
            End If

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Objects</returns> 
    ''' <param name="p_objMeasure">Measure.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults_NoTransaction(ByVal p_objMeasure As Measure) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
            Dim strNominalWithPassYN As String = CStr(IIf(p_objMeasure.NominalWidthPassYN.Equals(True), trueValue, falseValue))
            Dim strOVERALLWIDTHPASSFAIL As String = CStr(IIf(p_objMeasure.OverallWidthPassYN.Equals(True), trueValue, falseValue))
            Dim strOVERALLDIAMETERPASSFAIL As String = CStr(IIf(p_objMeasure.OverallDiameterPassYN.Equals(True), trueValue, falseValue))

            Dim p_iMEASUREID As Integer
            enumSaveResult = Depository.Current.SaveMeasurement(p_objMeasure.ProjectNumber, _
                                               p_objMeasure.TireNumber, _
                                               p_objMeasure.TestSpec, _
                                               p_objMeasure.CompletionDate, _
                                               p_objMeasure.InflationPressure, _
                                               p_objMeasure.MoldDesign, _
                                               p_objMeasure.RimWidth, _
                                               p_objMeasure.DotSerialNumber, _
                                               p_objMeasure.Diameter, _
                                               p_objMeasure.AvgSectionWidth, _
                                               p_objMeasure.AvgOverallWidth, _
                                               p_objMeasure.MaxOverallWidth, _
                                               p_objMeasure.MinSizeFactor, _
                                               p_objMeasure.MountTime, _
                                               p_objMeasure.MountTemp, _
                                               p_objMeasure.SKUID, _
                                               p_objMeasure.CertificationTypeId, _
                                               CStr(p_objMeasure.CertificateNumberID), _
                                               p_iMEASUREID, _
                                               p_objMeasure.SerialDate, _
                                               p_objMeasure.EndTime, _
                                               p_objMeasure.ActSizeFactor, _
                                               p_objMeasure.StartInfPressure, _
                                               p_objMeasure.EndInfPressure, _
                                               p_objMeasure.Adjustment, _
                                               p_objMeasure.Circumference, _
                                               p_objMeasure.NominalDiameter, _
                                               p_objMeasure.NominalWidth, _
                                               strNominalWithPassYN, _
                                               p_objMeasure.NominalWidthDifference, _
                                               p_objMeasure.NominalWidthTolerance, _
                                               p_objMeasure.MaxOverallDiameter, _
                                               p_objMeasure.MinOverallDiameter, _
                                               strOVERALLWIDTHPASSFAIL, _
                                               strOVERALLDIAMETERPASSFAIL, _
                                               p_objMeasure.DiameterDifference, _
                                               p_objMeasure.DiameterTolerance, _
                                               p_objMeasure.TemperatureResistanceGrading, _
                                               p_objMeasure.TensileStrength1, _
                                               p_objMeasure.TensileStrength2, _
                                               p_objMeasure.Elongation1, _
                                               p_objMeasure.Elongation2, _
                                               p_objMeasure.TensileStrengthAfterAging1, _
                                               p_objMeasure.TensileStrengthAfterAging2, _
                                               SecurityModel.GetUserName, _
                                               p_objMeasure.CertificateNumberID, _
                                               p_objMeasure.MaterialNumber, _
                                               p_objMeasure.Operation, _
                                               p_objMeasure.GTSpecMaterialNumber, _
                                               p_objMeasure.MFGWWYY)


            For Each detail As MeasureDetail In p_objMeasure.MeasureDetails
                If Not enumSaveResult = NameAid.SaveResult.Sucess Then Exit For

                enumSaveResult = Depository.Current.MeasurementDetail_Save(detail.SectionWidth, detail.OverallWidth, p_iMEASUREID, detail.Iteration, SecurityModel.GetUserName)
            Next

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Objects</returns> 
    ''' <param name="p_objMeasure">Measure.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults(ByVal p_objMeasure As Measure) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            enumSaveResult = SaveTestResults_NoTransaction(p_objMeasure)

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Objects</returns> 
    ''' <param name="p_objPlunger">Plunger.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults(ByVal p_objPlunger As Plunger) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            enumSaveResult = SaveTestResults_NoTransaction(p_objPlunger)

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Objects</returns> 
    ''' <param name="p_objPlunger">Plunger.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults_NoTransaction(ByVal p_objPlunger As Plunger) As NameAid.SaveResult
        Try
            Debug.WriteLine("SaveTestResults_NoTransaction")
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
            Dim p_intPlungerId As Integer
            Dim p_strPassYN As Object = IIf(p_objPlunger.PassYN.Equals(True), trueValue, falseValue)

            '  ByVal p_iMOUNTTEMP As Single not found yet
            enumSaveResult = Depository.Current.SavePlunger(p_objPlunger.ProjectNumber, _
                                                    p_objPlunger.TireNumber, _
                                                    p_objPlunger.TestSpec, _
                                                    p_objPlunger.CompletionDate, _
                                                    p_objPlunger.DotSerialNumber, _
                                                    p_objPlunger.AVGBreakingEnergy, _
                                                    CStr(p_strPassYN), _
                                                    p_objPlunger.SkuId, _
                                                    p_objPlunger.CertificationTypeId, _
                                                    CStr(p_objPlunger.CertificateNumberID), _
                                                    p_intPlungerId, _
                                                    p_objPlunger.SerialDate, _
                                                    p_objPlunger.MinPlunger, _
                                                    SecurityModel.GetUserName, _
                                                    p_objPlunger.CertificateNumberID, _
                                                    p_objPlunger.MaterialNumber, _
                                                    p_objPlunger.Operation, _
                                                    p_objPlunger.GTSpecMaterialNumber, _
                                                    p_objPlunger.MFGWWYY)

            For Each detail As PlungerDetail In p_objPlunger.PlungerDetails
                If Not enumSaveResult = NameAid.SaveResult.Sucess Then Exit For

                enumSaveResult = Depository.Current.SavePlungerDetail(detail.BreakingEnergy, p_intPlungerId, detail.Iteration, SecurityModel.GetUserName)
            Next

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Objects</returns> 
    ''' <param name="p_objTreadwear">TreadWear</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults(ByVal p_objTreadwear As Treadwear) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            enumSaveResult = SaveTestResults_NoTransaction(p_objTreadwear)

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    '''  Method to Save TestResults No Transaction.
    ''' </summary>
    ''' <returns>NameAid.SaveResult object</returns> 
    ''' <param name="p_objTreadwear">p_obj Treadwear</param> 
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults_NoTransaction(ByVal p_objTreadwear As Treadwear) As NameAid.SaveResult
        Try
            Debug.WriteLine("SaveTestResults_NoTransaction")
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
            Dim p_intTreadwearId As Integer
            Dim p_strPassYN As Object = IIf(p_objTreadwear.PassYN.Equals(True), trueValue, falseValue)

            '  ByVal p_iMOUNTTEMP As Single not found yet
            enumSaveResult = Depository.Current.SaveTreadWear(p_objTreadwear.ProjectNumber, _
                                                    p_objTreadwear.TireNumber, _
                                                    p_objTreadwear.TestSpec, _
                                                    p_objTreadwear.CompletionDate, _
                                                    p_objTreadwear.DotSerialNumber, _
                                                    p_objTreadwear.LowestWearBar, _
                                                    CStr(p_strPassYN), _
                                                    p_objTreadwear.SkuId, _
                                                    p_objTreadwear.CertificationTypeId, _
                                                    CStr(p_objTreadwear.CertificateNumberID), _
                                                    p_intTreadwearId, _
                                                    p_objTreadwear.SerialDate, _
                                                    SecurityModel.GetUserName, _
                                                    p_objTreadwear.IndicatorRequirement, _
                                                    p_objTreadwear.CertificateNumberID, _
                                                    p_objTreadwear.MaterialNumber, _
                                                    p_objTreadwear.Operation, _
                                                    p_objTreadwear.GTSpecMaterialNumber, _
                                                    p_objTreadwear.MFGWWYY)

            For Each detail As TreadwearDetail In p_objTreadwear.TreadwearDetails
                If Not enumSaveResult = NameAid.SaveResult.Sucess Then Exit For

                enumSaveResult = Depository.Current.SaveTreadWearDetail(detail.WearBarHeight, p_intTreadwearId, detail.Iteration, SecurityModel.GetUserName)
            Next

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult object</returns> 
    ''' <param name="p_objBeadUnSeat">p_obj BeadUnSeat</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults(ByVal p_objBeadUnSeat As BeadUnSeat) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            enumSaveResult = SaveTestResults_NoTransaction(p_objBeadUnSeat)

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save Test Results With No Transaction.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Object</returns> 
    ''' <param name="p_objBeadUnSeat">BeadUnseat.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults_NoTransaction(ByVal p_objBeadUnSeat As BeadUnSeat) As NameAid.SaveResult
        Try
            Debug.WriteLine("SaveTestResults_NoTransaction")
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
            Dim p_intBeadUnSeatId As Integer
            Dim p_strPassYN As String = CStr(IIf(p_objBeadUnSeat.PassYN.Equals(True), trueValue, falseValue))
            Dim p_TestPassFail As String = CStr(IIf(p_objBeadUnSeat.TestPassFail.Equals(True), trueValue, falseValue))

            enumSaveResult = Depository.Current.SaveBeadUnseat(p_objBeadUnSeat.ProjectNumber, _
                                                        p_objBeadUnSeat.TireNumber, _
                                                        p_objBeadUnSeat.TestSpec, _
                                                        p_objBeadUnSeat.CompletionDate, _
                                                        p_objBeadUnSeat.DotSerialNumber, _
                                                        p_objBeadUnSeat.LowestUnSeatValue, _
                                                        p_TestPassFail, _
                                                        p_objBeadUnSeat.CertificationTypeId, _
                                                        CStr(p_objBeadUnSeat.CertificateNumberID), _
                                                        p_intBeadUnSeatId, _
                                                        p_objBeadUnSeat.SerialDate, _
                                                        p_objBeadUnSeat.MinBeadUnseat, _
                                                        p_TestPassFail, _
                                                        SecurityModel.GetUserName, _
                                                        p_objBeadUnSeat.CertificateNumberID, _
                                                        p_objBeadUnSeat.MaterialNumber, _
                                                        p_objBeadUnSeat.Operation, _
                                                        p_objBeadUnSeat.GTSpecMaterialNumber, _
                                                        p_objBeadUnSeat.MFGWWYY)

            For Each detail As BeadUnSeatDetail In p_objBeadUnSeat.BeadUnSeatDetails
                If Not enumSaveResult = NameAid.SaveResult.Sucess Then Exit For

                enumSaveResult = Depository.Current.SaveBeadUnseatDetail(p_intBeadUnSeatId, detail.UnSeatForce, detail.Iteration, SecurityModel.GetUserName)
            Next

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Object</returns> 
    ''' <param name="p_objEndurance">Endurance.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults(ByVal p_objEndurance As Endurance) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            enumSaveResult = SaveTestResults_NoTransaction(p_objEndurance)

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save Test Results with No Transaction.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Object</returns> 
    ''' <param name="p_objEndurance">Endurance.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults_NoTransaction(ByVal p_objEndurance As Endurance) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            Dim p_intEnduranceId As Integer

            Dim p_strPassYN As Object = IIf(p_objEndurance.EnduranceTestPassYN.Equals(True), trueValue, falseValue)
            Dim p_strResultPassFail As Object = IIf(p_objEndurance.ResultPassFail.Equals(True), trueValue, falseValue)
            Dim p_strFinalJudgement As Object = IIf(p_objEndurance.FinalJudgement.Equals(True), trueValue, falseValue)

            enumSaveResult = Depository.Current.SaveEndurance(p_intEnduranceId, _
                                                         p_objEndurance.ProjectNumber, _
                                                         p_objEndurance.TireNumber, _
                                                         p_objEndurance.TestSpec, _
                                                         p_objEndurance.CompletionDate, _
                                                         p_objEndurance.DotSerialNumber, _
                                                         p_objEndurance.PrecondStartDate, _
                                                         p_objEndurance.PrecondStartTemp, _
                                                         p_objEndurance.RimDiameter, _
                                                         p_objEndurance.RimWidth, _
                                                         p_objEndurance.PrecondEndDate, _
                                                         p_objEndurance.PrecondEndTemp, _
                                                         CInt(p_objEndurance.InflationPressure), _
                                                         p_objEndurance.BeforeDiameter, _
                                                         p_objEndurance.AfterDiameter, _
                                                         CInt(p_objEndurance.BeforeInflation), _
                                                         CInt(p_objEndurance.AfterInflation), _
                                                         p_objEndurance.WheelPosition, _
                                                         p_objEndurance.WheelNumber, _
                                                         p_objEndurance.FinalTemp, _
                                                         p_objEndurance.FinalDistance, _
                                                         CInt(p_objEndurance.FinalInflation), _
                                                         p_objEndurance.PostcondStartDate, _
                                                         p_objEndurance.PostcondEndDate, _
                                                         p_objEndurance.PostcondEndTemp, _
                                                         CStr(p_strPassYN), _
                                                         p_objEndurance.CertificationTypeId, _
                                                         CStr(p_objEndurance.CertificateNumberID), _
                                                         p_objEndurance.SerialDate, _
                                                         p_objEndurance.PostcondTime, _
                                                         p_objEndurance.PrecondTime, _
                                                         p_objEndurance.DiameterTestDrum, _
                                                         p_objEndurance.PrecondTemp, _
                                                         p_objEndurance.InflationPressureReadjusted, _
                                                         p_objEndurance.CircumferenceBeforeTesting, _
                                                         CStr(p_strResultPassFail), _
                                                         p_objEndurance.EnduranceXHour, _
                                                         p_objEndurance.PossibleFailuresFound, _
                                                         p_objEndurance.CircumferenceAfterTesting, _
                                                         p_objEndurance.OuterDiameterDifference, _
                                                         p_objEndurance.OuterDiameterTolerance, _
                                                         p_objEndurance.SerieNOM, _
                                                         CStr(p_strFinalJudgement), _
                                                         p_objEndurance.Approver, _
                                                         SecurityModel.GetUserName, _
                                                         p_objEndurance.CertificateNumberID, _
                                                         p_objEndurance.MaterialNumber, _
                                                         p_objEndurance.LowInfStartInflation, _
                                                         p_objEndurance.LowInfEndInflation, _
                                                         CInt(p_objEndurance.LowInfEndTemp), _
                                                         p_objEndurance.Operation, _
                                                         p_objEndurance.GTSpecMaterialNumber, _
                                                         p_objEndurance.MFGWWYY)

            For Each detail As EnduranceDetail In p_objEndurance.EnduranceDetails
                If Not enumSaveResult = NameAid.SaveResult.Sucess Then Exit For

                enumSaveResult = Depository.Current.SaveEnduranceDetail(detail.TestStep, _
                                                                   detail.TimeInMin, _
                                                                   CInt(detail.Speed), _
                                                                   detail.TotMiles, _
                                                                   detail.Load, _
                                                                   detail.LoadPercent, _
                                                                   CInt(detail.SetInflation), _
                                                                   detail.AmbTemp, _
                                                                   CInt(detail.InfPressure), _
                                                                   detail.StepCompletionDate, _
                                                                   p_intEnduranceId)
            Next

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Object</returns> 
    ''' <param name="p_objHighSpeed">High Speed.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults(ByVal p_objHighSpeed As HighSpeed) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            enumSaveResult = SaveTestResults_NoTransaction(p_objHighSpeed)

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save Test Results with No Transaction.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Object</returns> 
    ''' <param name="p_objHighSpeed">High Speed.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults_NoTransaction(ByVal p_objHighSpeed As HighSpeed) As NameAid.SaveResult
        Try
            Debug.WriteLine("SaveTestResults_NoTransaction")
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            Dim p_intHighSpeedId As Integer

            Dim p_strPassYN As String = CStr(IIf(p_objHighSpeed.DurationTestPassYN.Equals(True), trueValue, falseValue))
            Dim p_strFinalJudg As String = CStr(IIf(p_objHighSpeed.FinalJudgement.Equals(True), trueValue, falseValue))
            Dim p_strSpeedPassFail As String = CStr(IIf(p_objHighSpeed.SpeedTestPassYN.Equals(True), trueValue, falseValue))

            enumSaveResult = Depository.Current.SaveHighSpeed(p_intHighSpeedId, _
                                                         p_objHighSpeed.ProjectNumber, _
                                                         p_objHighSpeed.TireNumber, _
                                                         p_objHighSpeed.TestSpec, _
                                                         p_objHighSpeed.CompletionDate, _
                                                         p_objHighSpeed.DotSerialNumber, _
                                                         p_objHighSpeed.MFGWWYY, _
                                                         p_objHighSpeed.PrecondStartDate, _
                                                         p_objHighSpeed.PrecondStartTemp, _
                                                         p_objHighSpeed.RimDiameter, _
                                                         p_objHighSpeed.RimWidth, _
                                                         p_objHighSpeed.PrecondEndDate, _
                                                         p_objHighSpeed.PrecondEndTemp, _
                                                         CInt(p_objHighSpeed.InflationPressure), _
                                                         p_objHighSpeed.BeforeDiameter, _
                                                         p_objHighSpeed.AfterDiameter, _
                                                         p_objHighSpeed.BeforeInflation, _
                                                         CInt(p_objHighSpeed.AfterInflation), _
                                                         p_objHighSpeed.WheelPosition, _
                                                         p_objHighSpeed.WheelNumber, _
                                                         p_objHighSpeed.FinalTemp, _
                                                         p_objHighSpeed.FinalDistance, _
                                                         CInt(p_objHighSpeed.FinalInflation), _
                                                         p_objHighSpeed.PostcondStartDate, _
                                                         p_objHighSpeed.PostcondEndDate, _
                                                         p_objHighSpeed.PostcondEndTemp, _
                                                         p_objHighSpeed.PrecondTime, _
                                                         p_objHighSpeed.PostcondTime, _
                                                         p_strPassYN, _
                                                         p_objHighSpeed.SerialDate, _
                                                         p_objHighSpeed.CertificationTypeId, _
                                                         CStr(p_objHighSpeed.CertificateNumberID), _
                                                        p_objHighSpeed.DiameterTestDrum, _
                                                        p_objHighSpeed.PrecondTemp, _
                                                        p_objHighSpeed.InflationPressureReadjusted, _
                                                        p_objHighSpeed.CircumferenceBeforeTesting, _
                                                        p_objHighSpeed.WheelSpeedRPM, _
                                                        p_objHighSpeed.WheelSpeedKMH, _
                                                        p_objHighSpeed.CircumferenceAfterTesting, _
                                                        p_objHighSpeed.OuterDiameterDifference, _
                                                        p_objHighSpeed.OuterDiameterTolerance, _
                                                        p_objHighSpeed.SerieNOM, _
                                                        p_strFinalJudg, _
                                                        p_objHighSpeed.Approver, _
                                                        p_objHighSpeed.SpeedTestPassAt, _
                                                        p_strSpeedPassFail, _
                                                        p_objHighSpeed.SpeedTotalTime, _
                                                        p_objHighSpeed.MaxSpeed, _
                                                        p_objHighSpeed.MaxLoad, _
                                                        SecurityModel.GetUserName, _
                                                        p_objHighSpeed.CertificateNumberID, _
                                                        p_objHighSpeed.MaterialNumber, _
                                                        p_objHighSpeed.Operation, _
                                                        p_objHighSpeed.GTSpecMaterialNumber)

            For Each detail As HighSpeedDetail In p_objHighSpeed.HighSpeedDetails
                If Not enumSaveResult = NameAid.SaveResult.Sucess Then Exit For

                enumSaveResult = Depository.Current.SaveHighSpeedDetail(p_intHighSpeedId, _
                                                                   SecurityModel.GetUserName, _
                                                                   detail.TestStep, _
                                                                   detail.TimeInMin, _
                                                                   detail.Speed, _
                                                                   detail.TotMiles, _
                                                                   detail.Load, _
                                                                   detail.LoadPercent, _
                                                                   CInt(detail.SetInflation), _
                                                                   detail.AmbTemp, _
                                                                   CInt(detail.InfPressure), _
                                                                   detail.StepCompletionDate)
            Next

            For Each speedDetail As SpeedTestDetail In p_objHighSpeed.SpeedTestDetails
                If Not enumSaveResult = NameAid.SaveResult.Sucess Then Exit For

                enumSaveResult = Depository.Current.SaveHighSpeed_SpeedTestDetail(p_intHighSpeedId, _
                                                                            speedDetail.Iteration, _
                                                                            speedDetail.Time, _
                                                                            speedDetail.Speed, _
                                                                            SecurityModel.GetUserName)
            Next

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Save Test Results.
    ''' </summary>
    ''' <returns>NameAid.Saveresult Object</returns> 
    ''' <param name="p_objSound">Sound.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults(ByVal p_objSound As Sound) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            enumSaveResult = SaveTestResults_NoTransaction(p_objSound)

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save Test Results with No Transaction.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Object</returns> 
    ''' <param name="p_objSound">Sound.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults_NoTransaction(ByVal p_objSound As Sound) As NameAid.SaveResult
        Try
            Debug.WriteLine("SaveTestResults_NoTransaction")
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            Dim p_intSoundId As Integer

            enumSaveResult = Depository.Current.SaveSound(SecurityModel.GetUserName, _
                                                     p_intSoundId, _
                                                     p_objSound.ProjectNumber, _
                                                     p_objSound.TireNumber, _
                                                     p_objSound.TestSpec, _
                                                     p_objSound.TestReportNumber, _
                                                     p_objSound.ManufactureAndBrand, _
                                                     p_objSound.TireClass, _
                                                     p_objSound.CategoryOfUse, _
                                                     p_objSound.DateOfTest, _
                                                     p_objSound.TestVehicule, _
                                                     p_objSound.TestVehiculeWheelbase, _
                                                     p_objSound.LocationOfTestTrack, _
                                                     p_objSound.DateTrackCertifToISO, _
                                                     p_objSound.TireSizeDesignation, _
                                                     p_objSound.TireServiceDescription, _
                                                     p_objSound.ReferenceInflationPressure, _
                                                     p_objSound.TestMass_FrontL, _
                                                     p_objSound.TestMass_FrontR, _
                                                     p_objSound.TestMass_RearL, _
                                                     p_objSound.TestMass_RearR, _
                                                     p_objSound.TireLoadIndex_FrontL, _
                                                     p_objSound.TireLoadIndex_FrontR, _
                                                     p_objSound.TireLoadIndex_RearL, _
                                                     p_objSound.TireLoadIndex_RearR, _
                                                     p_objSound.InflationPressureCo_FrontL, _
                                                     p_objSound.InflationPressureCo_FrontR, _
                                                     p_objSound.InflationPressureCo_RearL, _
                                                     p_objSound.InflationPressureCo_RearR, _
                                                     p_objSound.TestRimWidthCode, _
                                                     p_objSound.TempMeasureSensorType, _
                                                     p_objSound.CertificationTypeID, _
                                                     CStr(p_objSound.CertificateNumberID), _
                                                     p_objSound.SKUId, _
                                                     p_objSound.CertificateNumberID, _
                                                     p_objSound.Operation, _
                                                     p_objSound.GTSpecMaterialNumber, _
                                                     p_objSound.MFGWWYY)

            For Each detail As SoundDetail In p_objSound.SoundDetails
                If Not enumSaveResult = NameAid.SaveResult.Sucess Then Exit For

                enumSaveResult = Depository.Current.SaveSoundDetail(SecurityModel.GetUserName, _
                                                               detail.Iteration, _
                                                               detail.TestSpeed, _
                                                               detail.DirectionOfRun, _
                                                               detail.SoundLevelLeft, _
                                                               detail.SoundLevelRight, _
                                                               detail.AirTemp, _
                                                               detail.TrackTemp, _
                                                               detail.SoundLevelLeft_TempCorrected, _
                                                               detail.SoundLevelRight_TempCorrected, _
                                                               p_intSoundId)
            Next

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to save Test Results.
    ''' </summary>
    ''' <returns>NameAid.SaveResult Object</returns> 
    ''' <param name="p_objWetGrip">WetGrip Object.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults(ByVal p_objWetGrip As WetGrip) As NameAid.SaveResult
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            enumSaveResult = SaveTestResults_NoTransaction(p_objWetGrip)

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save Test Results with No Transaction.
    ''' </summary>
    ''' <returns>NameAid.Save result object</returns> 
    ''' <param name="p_objWetGrip">Wet Grip.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function SaveTestResults_NoTransaction(ByVal p_objWetGrip As WetGrip) As NameAid.SaveResult
        Try
            Debug.WriteLine("SaveTestResults_NoTransaction")
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            Dim p_intWetGripId As Integer

            enumSaveResult = Depository.Current.SaveWetGrip(SecurityModel.GetUserName, _
                                                       p_intWetGripId, _
                                                       p_objWetGrip.ProjectNumber, _
                                                       p_objWetGrip.TireNumber, _
                                                       p_objWetGrip.TestSpec, _
                                                       p_objWetGrip.DateOfTest, _
                                                       p_objWetGrip.TestVehicle, _
                                                       p_objWetGrip.LocationOfTestTrack, _
                                                       p_objWetGrip.TestTrackCharacteristics, _
                                                       p_objWetGrip.IssueBy, _
                                                       p_objWetGrip.MethodOfCertification, _
                                                       p_objWetGrip.TestTireDetails, _
                                                       p_objWetGrip.TireSizeAndServiceDesc, _
                                                       p_objWetGrip.TireBrandAndTradeDesc, _
                                                       p_objWetGrip.ReferenceInflationPressure, _
                                                       p_objWetGrip.TestRimWithCode, _
                                                       p_objWetGrip.TempMeasureSensorType, _
                                                       p_objWetGrip.IdentificationSRTT, _
                                                       p_objWetGrip.TestTireLoad_SRTT, _
                                                       p_objWetGrip.TestTireLoad_Candidate, _
                                                       p_objWetGrip.TestTireLoad_Control, _
                                                       p_objWetGrip.WaterDepth_SRTT, _
                                                       p_objWetGrip.WaterDepth_Candidate, _
                                                       p_objWetGrip.WaterDepth_Control, _
                                                       p_objWetGrip.WettedTrackTempAvg, _
                                                       p_objWetGrip.CertificationTypeID, _
                                                       CStr(p_objWetGrip.CertificateNumberID), _
                                                       p_objWetGrip.SKUId, _
                                                       p_objWetGrip.CertificateNumberID, _
                                                       p_objWetGrip.Operation, _
                                                     p_objWetGrip.GTSpecMaterialNumber, _
                                                     p_objWetGrip.MFGWWYY)

            For Each detail As WetGripDetail In p_objWetGrip.WetGripDetails
                If Not enumSaveResult = NameAid.SaveResult.Sucess Then Exit For

                enumSaveResult = Depository.Current.SaveWetGripDetail(SecurityModel.GetUserName, _
                                                                 detail.Iteration, _
                                                                 detail.TestSpeed, _
                                                                 detail.DirectionOfRun, _
                                                                 detail.SRTT, _
                                                                 detail.CandidateTire, _
                                                                 detail.PeakBreakForceCoeficient, _
                                                                 detail.MeanFullyDevelopedDeceleration, _
                                                                 detail.WetGripIndex, _
                                                                 detail.Comments, _
                                                                 p_intWetGripId)
            Next

            Return enumSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Map TRProductSectionData to product entity class.
    ''' </summary>
    ''' <returns>Date</returns> 
    ''' <param name="p_objTRProductSectionData">TR Product Section Data.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function MapTRProductSectionDataToProduct(ByVal p_objTRProductSectionData As TRProductSectionData, _
                                                    ByVal p_objCertificate As Certificate) As Product
        Try
            Dim objProduct As New Product()

            objProduct.SKUID = p_objCertificate.SKUID
            objProduct.MaterialNumber = p_objCertificate.MaterialNumber
            If Not String.IsNullOrEmpty(p_objCertificate.Family_I) Then
                objProduct.Family = p_objCertificate.Family_I
            End If

            If p_objTRProductSectionData.OriginalProduct IsNot Nothing Then
                objProduct.Brand = p_objTRProductSectionData.OriginalProduct.Brand
                objProduct.BrandLine = p_objTRProductSectionData.OriginalProduct.BrandLine
                objProduct.TireTypeId = p_objTRProductSectionData.OriginalProduct.TireTypeId
                objProduct.PSN = p_objTRProductSectionData.OriginalProduct.PSN
                objProduct.SpecNumber = p_objTRProductSectionData.OriginalProduct.SpecNumber
                objProduct.DiscontinuedDate = p_objTRProductSectionData.OriginalProduct.DiscontinuedDate
            End If

            If Not String.IsNullOrEmpty(p_objTRProductSectionData.Trademark) Then
                objProduct.BrandDesc = p_objTRProductSectionData.Trademark
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.TreadPattern) Then
                objProduct.TreadPattern = p_objTRProductSectionData.TreadPattern
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.SizeDesignation) Then
                objProduct.TireSizeStamp = p_objTRProductSectionData.SizeDesignation
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.SpecialProtectiveBand) Then
                objProduct.SpecialProtectiveBand = p_objTRProductSectionData.SpecialProtectiveBand
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.StructureConstruction) Then
                objProduct.BiasBeltedRadial = p_objTRProductSectionData.StructureConstruction
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.SpeedCategory) Then
                objProduct.SpeedRating = p_objTRProductSectionData.SpeedCategory
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.SingLoadCapacityIndex) Then
                objProduct.SingLoadIndex = p_objTRProductSectionData.SingLoadCapacityIndex
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.DualLoadCapacityIndex) Then
                objProduct.DualLoadIndex = p_objTRProductSectionData.DualLoadCapacityIndex
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.PlyRatingNumber) Then
                Select Case p_objTRProductSectionData.PlyRatingNumber
                    Case "4"
                        objProduct.LoadRange = "B"
                    Case "6"
                        objProduct.LoadRange = "C"
                    Case "8"
                        objProduct.LoadRange = "D"
                    Case "10"
                        objProduct.LoadRange = "E"
                    Case "12" 'these were added 1/5/12 per Rhonda Riedel - jeseitz
                        objProduct.LoadRange = "F"
                    Case "14"
                        objProduct.LoadRange = "G"
                    Case "16"
                        objProduct.LoadRange = "H"
                    Case "18"
                        objProduct.LoadRange = "J"
                    Case "20"
                        objProduct.LoadRange = "L"
                    Case "22"
                        objProduct.LoadRange = "M"
                    Case "24"
                        objProduct.LoadRange = falseValue
                    Case Else
                        objProduct.LoadRange = String.Empty
                End Select
            End If
            If Not String.IsNullOrEmpty(CStr(p_objTRProductSectionData.IndicationTubeless)) Then
                objProduct.TubelessYN = p_objTRProductSectionData.IndicationTubeless
            End If
            If Not String.IsNullOrEmpty(CStr(p_objTRProductSectionData.IndicationReinforced)) Then
                objProduct.ReinforcedYN = p_objTRProductSectionData.IndicationReinforced
            End If
            If Not String.IsNullOrEmpty(CStr(p_objTRProductSectionData.IndicationExtraLoad)) Then
                objProduct.ExtraLoadYN = p_objTRProductSectionData.IndicationExtraLoad
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.MeasuringRim) Then
                objProduct.MeaRimWidth = ConvertToSingle(p_objTRProductSectionData.MeasuringRim)
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.DateOfManufacture) Then
                objProduct.SerialDate = ConvertToDateTime(p_objTRProductSectionData.DateOfManufacture)
            End If

            'Both using DOTSerialNumber handled

            If Not String.IsNullOrEmpty(p_objTRProductSectionData.DOTCode) Then
                objProduct.DOTSerialNumber = p_objTRProductSectionData.DOTCode
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.NominalTireWidth) Then
                objProduct.NominalTireWidth = p_objTRProductSectionData.NominalTireWidth
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.AspectRatio) Then
                objProduct.AspectRatio = p_objTRProductSectionData.AspectRatio
            End If

            If Not String.IsNullOrEmpty(p_objTRProductSectionData.NominalRimDiameter) Then
                objProduct.RimDiameter = ConvertToSingle(p_objTRProductSectionData.NominalRimDiameter)
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.TemperatureRating) Then
                objProduct.UTQGTemp = p_objTRProductSectionData.TemperatureRating
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.Traction) Then
                objProduct.UTQGTraction = p_objTRProductSectionData.Traction
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.TreadWear) Then
                objProduct.UTQGTreadwear = p_objTRProductSectionData.TreadWear
            End If

            If Not String.IsNullOrEmpty(CStr(p_objTRProductSectionData.Regroovable)) Then
                objProduct.RegroovableInd = p_objTRProductSectionData.Regroovable
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.ManufacturingLocationOfOrigin) Then
                objProduct.PlantProduced = p_objTRProductSectionData.ManufacturingLocationOfOrigin
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.TreadwearIndicators) Then
                objProduct.TreadwearIndicator = p_objTRProductSectionData.TreadwearIndicators
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.InmetroMark) Then
                objProduct.IMark = p_objTRProductSectionData.InmetroMark
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.CargoCapacity) Then
                objProduct.LoadRange = p_objTRProductSectionData.CargoCapacity
            End If

            If Not String.IsNullOrEmpty(p_objTRProductSectionData.NameOfManufacturer) Then
                objProduct.NameOfManufacture = p_objTRProductSectionData.NameOfManufacturer
            End If

            If Not String.IsNullOrEmpty(p_objTRProductSectionData.DateOfTest) Then
                objProduct.MostRecentTestDate = ConvertToDateTime(p_objTRProductSectionData.DateOfTest)
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.ProductFamily) Then
                objProduct.Family = p_objTRProductSectionData.ProductFamily
            End If
            If Not String.IsNullOrEmpty(p_objTRProductSectionData.TPN) Then
                objProduct.TPN = p_objTRProductSectionData.TPN
            End If

            objProduct.OriginalProduct = p_objTRProductSectionData.OriginalProduct

            If Not String.IsNullOrEmpty(p_objTRProductSectionData.MFGWWYY) Then
                objProduct.MFGWWYY = p_objTRProductSectionData.MFGWWYY
            End If

            If Not String.IsNullOrEmpty(CStr(p_objTRProductSectionData.MudSnow)) Then
                objProduct.MudSnow = p_objTRProductSectionData.MudSnow
            End If

            If Not String.IsNullOrEmpty(CStr(p_objTRProductSectionData.SevereWeatherIndicator)) Then
                objProduct.SevereWeatherIndicator = p_objTRProductSectionData.SevereWeatherIndicator
            End If

            If Not String.IsNullOrEmpty(p_objTRProductSectionData.TireId) Then
                objProduct.TireId = p_objTRProductSectionData.TireId
            End If
            Return objProduct
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Map Project Section Data.
    ''' </summary>
    ''' <returns>TR Project Section Data</returns> 
    ''' <param name="p_objBeadUnSeat">Bead Unseat.</param>
    ''' <param name="p_objEndurance">Endurance</param>
    ''' <param name="p_objHighSpeed">High Speed</param>
    ''' <param name="p_objMeasure">Measure</param>
    ''' <param name="p_objPlunger">Plunger</param>
    ''' <param name="p_objSound">Sound</param>
    ''' <param name="p_objTreadwear">TreadWear</param>
    ''' <param name="p_objWetGrip">WetGrip</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapProjectSectionData(ByVal p_objMeasure As Measure, ByVal p_objPlunger As Plunger, _
                                        ByVal p_objBeadUnSeat As BeadUnSeat, ByVal p_objTreadwear As Treadwear, _
                                        ByVal p_objEndurance As Endurance, ByVal p_objHighSpeed As HighSpeed, _
                                        ByVal p_objSound As Sound, ByVal p_objWetGrip As WetGrip) As TRProjectSectionData
        Try
            Dim objProjectSectionData As New TRProjectSectionData
            objProjectSectionData.MeasureProjectNumber = p_objMeasure.ProjectNumber
            objProjectSectionData.MeasureTireNumber = p_objMeasure.TireNumber.ToString()
            objProjectSectionData.MeasureOperation = p_objMeasure.Operation
            objProjectSectionData.MeasureTestSpec = p_objMeasure.TestSpec

            objProjectSectionData.PlungerProjectNumber = p_objPlunger.ProjectNumber
            objProjectSectionData.PlungerTireNumber = p_objPlunger.TireNumber.ToString()
            objProjectSectionData.PlungerOperation = p_objPlunger.Operation
            objProjectSectionData.PlungerTestSpec = p_objPlunger.TestSpec

            objProjectSectionData.BeadUnSeatProjectNumber = p_objBeadUnSeat.ProjectNumber
            objProjectSectionData.BeadUnSeatTireNumber = p_objBeadUnSeat.TireNumber.ToString()
            objProjectSectionData.BeadUnSeatOperation = p_objBeadUnSeat.Operation
            objProjectSectionData.BeadUnSeatTestSpec = p_objBeadUnSeat.TestSpec

            objProjectSectionData.TreadwearProjectNumber = p_objTreadwear.ProjectNumber
            objProjectSectionData.TreadwearTireNumber = p_objTreadwear.TireNumber.ToString()
            objProjectSectionData.TreadwearOperation = p_objTreadwear.Operation
            objProjectSectionData.TreadwearTestSpec = p_objTreadwear.TestSpec

            objProjectSectionData.EnduranceProjectNumber = p_objEndurance.ProjectNumber
            objProjectSectionData.EnduranceTireNumber = p_objEndurance.TireNumber.ToString()
            objProjectSectionData.EnduranceOperation = p_objEndurance.Operation
            objProjectSectionData.EnduranceTestSpec = p_objEndurance.TestSpec

            objProjectSectionData.HighSpeedProjectNumber = p_objHighSpeed.ProjectNumber
            objProjectSectionData.HighSpeedTireNumber = CStr(p_objHighSpeed.TireNumber)
            objProjectSectionData.HighSpeedOperation = p_objHighSpeed.Operation
            objProjectSectionData.HighSpeedTestSpec = p_objHighSpeed.TestSpec

            If p_objSound IsNot Nothing Then
                objProjectSectionData.SoundProjectNumber = p_objSound.ProjectNumber
                objProjectSectionData.SoundTireNumber = CStr(p_objSound.TireNumber)
                objProjectSectionData.SoundOperation = p_objSound.Operation
                objProjectSectionData.SoundTestSpec = p_objSound.TestSpec
            End If

            If p_objWetGrip IsNot Nothing Then
                objProjectSectionData.WetGripProjectNumber = p_objWetGrip.ProjectNumber
                objProjectSectionData.WetGripTireNumber = CStr(p_objWetGrip.TireNumber)
                objProjectSectionData.WetGripOperation = p_objWetGrip.Operation
                objProjectSectionData.WetGripTestSpec = p_objWetGrip.TestSpec
            End If

            Return objProjectSectionData
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
     
    ''' <summary>
    ''' Method to Maps the Test Result section Data to Measure Entity.
    ''' </summary>
    ''' <returns>Measure Object</returns> 
    ''' <param name="p_objTRMeasurement"></param>
    ''' <param name="p_objTRProject"></param>
    ''' <param name="p_objCertificate"></param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function MapTRMeasurementSectionDataToMeasure(ByVal p_objTRMeasurement As TRMeasurementSectionData, _
                                                    ByVal p_objTRProject As TRProjectSectionData, _
                                                    ByVal p_objCertificate As Certificate) As Measure
        Try
            Dim objMeasure As New Measure

            If Not p_objTRMeasurement.OriginalMeasure Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                objMeasure.MeasureId = p_objTRMeasurement.OriginalMeasure.MeasureId

                objMeasure.SerialDate = p_objTRMeasurement.OriginalMeasure.SerialDate
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.MeasureMatlNum) Then
                objMeasure.MaterialNumber = p_objTRMeasurement.MeasureMatlNum
            End If

            If Not String.IsNullOrEmpty(p_objTRProject.MeasureProjectNumber) Then
                objMeasure.ProjectNumber = p_objTRProject.MeasureProjectNumber
            End If

            If Not String.IsNullOrEmpty(p_objTRProject.MeasureTireNumber) Then
                objMeasure.TireNumber = ConvertToInteger(p_objTRProject.MeasureTireNumber)
            End If

            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not String.IsNullOrEmpty(p_objTRProject.MeasureOperation) Then
                objMeasure.Operation = p_objTRProject.MeasureOperation
            End If

            If Not String.IsNullOrEmpty(p_objTRProject.MeasureTestSpec) Then
                objMeasure.TestSpec = p_objTRProject.MeasureTestSpec
            End If

            'Get the certificationTypeID based on the certification type name
            'jillfix
            Dim intCertType As Integer = Depository.Current.GetCertificationTypeID(p_objCertificate.CertificationTypeName)

            objMeasure.CertificateNumberID = p_objCertificate.CertificateNumberID
            objMeasure.CertificationTypeId = intCertType
            objMeasure.SKUID = p_objCertificate.SKUID



            If Not String.IsNullOrEmpty(p_objTRMeasurement.StartDate) And ConvertToDateTime(p_objTRMeasurement.StartDate) <> DateTime.MinValue Then
                Dim intStartHour As Integer = 1, intStartMinute As Integer = 1, strStartAMPM As String
                If Not String.IsNullOrEmpty(p_objTRMeasurement.StartTime) Then
                    intStartHour = ConvertToInteger(p_objTRMeasurement.StartTime.Substring(0, 2))
                    intStartMinute = ConvertToInteger(p_objTRMeasurement.StartTime.Substring(3, 2))
                    strStartAMPM = p_objTRMeasurement.StartTime.Substring(6, 2)
                    If strStartAMPM = "AM" And intStartHour = 12 Then
                        intStartHour = 0
                    End If
                    If strStartAMPM = "PM" And intStartHour <> 12 Then
                        intStartHour = intStartHour + 12
                        'If intStartHour >= 24 Then
                        '    intStartHour = 0
                        'End If
                    End If
                End If

                Dim dteStartDate As DateTime = ConvertToDateTime(p_objTRMeasurement.StartDate)
                objMeasure.MountTime = New DateTime(dteStartDate.Year, dteStartDate.Month, dteStartDate.Day, intStartHour, intStartMinute, 0)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.EndDate) And ConvertToDateTime(p_objTRMeasurement.EndDate) <> DateTime.MinValue Then
                Dim intEndHour As Integer = 1, intEndMinute As Integer = 1, strEndAMPM As String
                If Not String.IsNullOrEmpty(p_objTRMeasurement.EndTime) Then
                    intEndHour = ConvertToInteger(p_objTRMeasurement.EndTime.Substring(0, 2))
                    intEndMinute = ConvertToInteger(p_objTRMeasurement.EndTime.Substring(3, 2))
                    strEndAMPM = p_objTRMeasurement.EndTime.Substring(6, 2)
                    If strEndAMPM = "PM" Then
                        intEndHour = intEndHour + 12
                        If intEndHour >= 24 Then
                            intEndHour = 0
                        End If
                    End If
                End If
                Dim dteEndDate As DateTime = ConvertToDateTime(p_objTRMeasurement.EndDate)
                objMeasure.CompletionDate = New DateTime(dteEndDate.Year, dteEndDate.Month, dteEndDate.Day, intEndHour, intEndMinute, 0)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.DOTSerialNumber) Then
                objMeasure.DotSerialNumber = p_objTRMeasurement.DOTSerialNumber
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.InflationPressure) Then
                objMeasure.InflationPressure = ConvertToShort(p_objTRMeasurement.InflationPressure)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.StartInflationPressure) Then
                objMeasure.StartInfPressure = ConvertToShort(p_objTRMeasurement.StartInflationPressure)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.EndInflationPressure) Then
                objMeasure.EndInfPressure = ConvertToShort(p_objTRMeasurement.EndInflationPressure)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.AverageWidth) Then
                objMeasure.AvgSectionWidth = ConvertToSingle(p_objTRMeasurement.AverageWidth)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.Adjustment) Then
                objMeasure.Adjustment = p_objTRMeasurement.Adjustment
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.ActualSizeFactor) Then
                objMeasure.ActSizeFactor = ConvertToSingle(p_objTRMeasurement.ActualSizeFactor)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.MinimumSizeFactor) Then
                objMeasure.MinSizeFactor = ConvertToSingle(p_objTRMeasurement.MinimumSizeFactor)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.Circumference) Then
                objMeasure.Circumference = ConvertToSingle(p_objTRMeasurement.Circumference)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.OuterDiameter) Then
                objMeasure.Diameter = ConvertToSingle(p_objTRMeasurement.OuterDiameter)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.NominalDiameter) Then
                objMeasure.NominalDiameter = ConvertToSingle(p_objTRMeasurement.NominalDiameter)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.NominalWidth) Then
                objMeasure.NominalWidth = ConvertToSingle(p_objTRMeasurement.NominalWidth)
            End If

            objMeasure.NominalWidthPassYN = p_objTRMeasurement.NominalWidthYN

            If Not String.IsNullOrEmpty(p_objTRMeasurement.NominalDifference) Then
                objMeasure.NominalWidthDifference = ConvertToSingle(p_objTRMeasurement.NominalDifference)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.NominalTolerance) Then
                objMeasure.NominalWidthTolerance = ConvertToSingle(p_objTRMeasurement.NominalTolerance)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.MaxOverallDiameter) Then
                objMeasure.MaxOverallDiameter = ConvertToSingle(p_objTRMeasurement.MaxOverallDiameter)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.MaxOverallWidth) Then
                objMeasure.MaxOverallWidth = ConvertToSingle(p_objTRMeasurement.MaxOverallWidth)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.MinOverallDiameter) Then
                objMeasure.MinOverallDiameter = ConvertToSingle(p_objTRMeasurement.MinOverallDiameter)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.OW) Then
                objMeasure.AvgOverallWidth = ConvertToSingle(p_objTRMeasurement.OW)
            End If

            objMeasure.OverallWidthPassYN = p_objTRMeasurement.OWYN

            If Not String.IsNullOrEmpty(p_objTRMeasurement.OD) Then
                objMeasure.Diameter = ConvertToSingle(p_objTRMeasurement.OD)
            End If

            objMeasure.OverallDiameterPassYN = p_objTRMeasurement.ODYN

            If Not String.IsNullOrEmpty(p_objTRMeasurement.OverallDifference) Then
                objMeasure.DiameterDifference = ConvertToSingle(p_objTRMeasurement.OverallDifference)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.OverallTolerance) Then
                objMeasure.DiameterTolerance = ConvertToSingle(p_objTRMeasurement.OverallTolerance)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.RimRim) Then
                objMeasure.RimWidth = ConvertToSingle(p_objTRMeasurement.RimRim)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.RimPressure) Then
                objMeasure.InflationPressure = ConvertToShort(p_objTRMeasurement.RimPressure)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.TensileStrength1) Then
                objMeasure.TensileStrength1 = ConvertToSingle(p_objTRMeasurement.TensileStrength1)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.TensileStrength2) Then
                objMeasure.TensileStrength2 = ConvertToSingle(p_objTRMeasurement.TensileStrength2)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.Elongation1) Then
                objMeasure.Elongation1 = ConvertToShort(p_objTRMeasurement.Elongation1)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.Elongation2) Then
                objMeasure.Elongation2 = ConvertToShort(p_objTRMeasurement.Elongation2)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.TensileStrengthafterAging1) Then
                objMeasure.TensileStrengthAfterAging1 = ConvertToSingle(p_objTRMeasurement.TensileStrengthafterAging1)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.TensileStrengthafterAging2) Then
                objMeasure.TensileStrengthAfterAging2 = ConvertToSingle(p_objTRMeasurement.TensileStrengthafterAging2)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.TemperatureResistanceGrading) Then
                objMeasure.TemperatureResistanceGrading = p_objTRMeasurement.TemperatureResistanceGrading
            End If

            'NOTE: p_objTRMeasurement.TemperatureResistanceGrading1 come from product based on the mapping excel file, can not save with measurement

            objMeasure.MeasureDetails = MapTRMeasurementSectionDataToMeasureDetail(p_objTRMeasurement)

            objMeasure.OriginalMeasure = p_objTRMeasurement.OriginalMeasure

            If Not String.IsNullOrEmpty(p_objTRMeasurement.GTSpecMeasureMatlNum) Then
                objMeasure.GTSpecMaterialNumber = p_objTRMeasurement.GTSpecMeasureMatlNum
            End If

            Return objMeasure
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Maps the Test Result section Data to Measure Detail entity
    ''' </summary>
    ''' <returns>List of Measure Detail</returns> 
    ''' <param name="p_objTRMeasurement">TR Measurement.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRMeasurementSectionDataToMeasureDetail(ByVal p_objTRMeasurement As TRMeasurementSectionData) As List(Of MeasureDetail)

        Try
            Dim objMeasureDetail As MeasureDetail
            Dim detailList As New List(Of MeasureDetail)

            objMeasureDetail = New MeasureDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRMeasurement.OriginalMeasure Is Nothing Then
                objMeasureDetail.MeasureId = p_objTRMeasurement.OriginalMeasure.MeasureId
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.OverallWidth1) Then
                objMeasureDetail.OverallWidth = ConvertToSingle(p_objTRMeasurement.OverallWidth1)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.SectionWidth1) Then
                objMeasureDetail.SectionWidth = ConvertToSingle(p_objTRMeasurement.SectionWidth1)
            End If

            objMeasureDetail.Iteration = 1

            detailList.Add(objMeasureDetail)

            objMeasureDetail = New MeasureDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRMeasurement.OriginalMeasure Is Nothing Then
                objMeasureDetail.MeasureId = p_objTRMeasurement.OriginalMeasure.MeasureId
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.OverallWidth2) Then
                objMeasureDetail.OverallWidth = ConvertToSingle(p_objTRMeasurement.OverallWidth2)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.SectionWidth2) Then
                objMeasureDetail.SectionWidth = ConvertToSingle(p_objTRMeasurement.SectionWidth2)
            End If

            objMeasureDetail.Iteration = 2

            detailList.Add(objMeasureDetail)

            objMeasureDetail = New MeasureDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRMeasurement.OriginalMeasure Is Nothing Then
                objMeasureDetail.MeasureId = p_objTRMeasurement.OriginalMeasure.MeasureId
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.OverallWidth3) Then
                objMeasureDetail.OverallWidth = ConvertToSingle(p_objTRMeasurement.OverallWidth3)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.SectionWidth3) Then
                objMeasureDetail.SectionWidth = ConvertToSingle(p_objTRMeasurement.SectionWidth3)
            End If

            objMeasureDetail.Iteration = 3

            detailList.Add(objMeasureDetail)

            objMeasureDetail = New MeasureDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRMeasurement.OriginalMeasure Is Nothing Then
                objMeasureDetail.MeasureId = p_objTRMeasurement.OriginalMeasure.MeasureId
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.OverallWidth4) Then
                objMeasureDetail.OverallWidth = ConvertToSingle(p_objTRMeasurement.OverallWidth4)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.SectionWidth4) Then
                objMeasureDetail.SectionWidth = ConvertToSingle(p_objTRMeasurement.SectionWidth4)
            End If

            objMeasureDetail.Iteration = 4

            detailList.Add(objMeasureDetail)

            objMeasureDetail = New MeasureDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRMeasurement.OriginalMeasure Is Nothing Then
                objMeasureDetail.MeasureId = p_objTRMeasurement.OriginalMeasure.MeasureId
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.OverallWidth5) Then
                objMeasureDetail.OverallWidth = ConvertToSingle(p_objTRMeasurement.OverallWidth5)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.SectionWidth5) Then
                objMeasureDetail.SectionWidth = ConvertToSingle(p_objTRMeasurement.SectionWidth5)
            End If

            objMeasureDetail.Iteration = 5

            detailList.Add(objMeasureDetail)

            objMeasureDetail = New MeasureDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRMeasurement.OriginalMeasure Is Nothing Then
                objMeasureDetail.MeasureId = p_objTRMeasurement.OriginalMeasure.MeasureId
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.OverallWidth6) Then
                objMeasureDetail.OverallWidth = ConvertToSingle(p_objTRMeasurement.OverallWidth6)
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.SectionWidth6) Then
                objMeasureDetail.SectionWidth = ConvertToSingle(p_objTRMeasurement.SectionWidth6)
            End If

            objMeasureDetail.Iteration = 6

            detailList.Add(objMeasureDetail)

            Return detailList
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Map TR Measurement Section Data To Plunger.
    ''' </summary>
    ''' <returns>Plunger Object</returns> 
    ''' <param name="p_intCertificateNumberID">Certificate Number Id.</param>
    ''' <param name="p_intCertType">Certification Type</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_objProject">Project</param>
    ''' <param name="p_objTRMeasurement">TR Measurement</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function MapTRMeasurementSectionDataToPlunger(ByVal p_objTRMeasurement As TRMeasurementSectionData, _
                                                          ByVal p_objProject As TRProjectSectionData, _
                                                          ByVal p_intCertificateNumberID As Integer, _
                                                          ByVal p_intCertType As Integer, _
                                                          ByVal p_intSKUID As Integer) As Plunger
        Try
            Dim objPlunger As New Plunger

            If Not p_objTRMeasurement.OriginalPlunger Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                objPlunger.PlungerId = p_objTRMeasurement.OriginalPlunger.PlungerId
            End If

            objPlunger.CertificateNumberID = p_intCertificateNumberID
            objPlunger.CertificationTypeId = p_intCertType
            objPlunger.SkuId = p_intSKUID

            If Not String.IsNullOrEmpty(p_objTRMeasurement.PlungerMatlNum) Then
                objPlunger.MaterialNumber = p_objTRMeasurement.PlungerMatlNum
            End If

            If Not String.IsNullOrEmpty(p_objProject.PlungerProjectNumber) Then
                objPlunger.ProjectNumber = p_objProject.PlungerProjectNumber
            End If
            If Not String.IsNullOrEmpty(p_objProject.PlungerTireNumber) Then
                objPlunger.TireNumber = ConvertToInteger(p_objProject.PlungerTireNumber)
            End If
            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not String.IsNullOrEmpty(p_objProject.PlungerOperation) Then
                objPlunger.Operation = p_objProject.PlungerOperation
            End If
            If Not String.IsNullOrEmpty(p_objProject.PlungerTestSpec) Then
                objPlunger.TestSpec = p_objProject.PlungerTestSpec
            End If
            If Not String.IsNullOrEmpty(p_objTRMeasurement.StartDate) Then
                objPlunger.CompletionDate = ConvertToDateTime(p_objTRMeasurement.StartDate)
            End If

            If Not p_objTRMeasurement.OriginalMeasure Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                objPlunger.DotSerialNumber = p_objTRMeasurement.OriginalMeasure.DotSerialNumber
                objPlunger.SerialDate = p_objTRMeasurement.OriginalMeasure.SerialDate
            End If

            objPlunger.PassYN = p_objTRMeasurement.PlungerYN

            Try
                If Not String.IsNullOrEmpty(p_objTRMeasurement.PlungerAverage) Then
                    objPlunger.AVGBreakingEnergy = CInt(p_objTRMeasurement.PlungerAverage)
                End If
            Catch ex As Exception
                objPlunger.AVGBreakingEnergy = 0
            End Try

            If Not String.IsNullOrEmpty(p_objTRMeasurement.PlungerAverageJ) Then
                objPlunger.MinPlunger = ConvertToInteger(p_objTRMeasurement.PlungerAverageJ)
            End If

            objPlunger.PlungerDetails = Me.MapTRMeasurementSectionDataToPlungerDetail(p_objTRMeasurement)

            objPlunger.OriginalPlunger = p_objTRMeasurement.OriginalPlunger

            If Not String.IsNullOrEmpty(p_objTRMeasurement.GTSpecPlungerMatlNum) Then
                objPlunger.GTSpecMaterialNumber = p_objTRMeasurement.GTSpecPlungerMatlNum
            End If

            Return objPlunger
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Map TR Measurement Section Data To Plunger Detail.
    ''' </summary>
    ''' <returns>List of Plunger Detail</returns> 
    ''' <param name="p_objTRMeasurement">TR Measurement.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRMeasurementSectionDataToPlungerDetail(ByVal p_objTRMeasurement As TRMeasurementSectionData) As List(Of PlungerDetail)
        Try
            Dim objPlungerDetail As PlungerDetail
            Dim plungerDetails As New List(Of PlungerDetail)

            If Not String.IsNullOrEmpty(p_objTRMeasurement.Plunger1) Then
                objPlungerDetail = New PlungerDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalPlunger Is Nothing Then
                    objPlungerDetail.PlungerId = p_objTRMeasurement.OriginalPlunger.PlungerId
                End If
                objPlungerDetail.BreakingEnergy = ConvertToInteger(p_objTRMeasurement.Plunger1)
                objPlungerDetail.Iteration = 1
                plungerDetails.Add(objPlungerDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.Plunger2) Then
                objPlungerDetail = New PlungerDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalPlunger Is Nothing Then
                    objPlungerDetail.PlungerId = p_objTRMeasurement.OriginalPlunger.PlungerId
                End If
                objPlungerDetail.BreakingEnergy = ConvertToInteger(p_objTRMeasurement.Plunger2)
                objPlungerDetail.Iteration = 2
                plungerDetails.Add(objPlungerDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.Plunger3) Then
                objPlungerDetail = New PlungerDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalPlunger Is Nothing Then
                    objPlungerDetail.PlungerId = p_objTRMeasurement.OriginalPlunger.PlungerId
                End If
                objPlungerDetail.BreakingEnergy = ConvertToInteger(p_objTRMeasurement.Plunger3)
                objPlungerDetail.Iteration = 3
                plungerDetails.Add(objPlungerDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.Plunger4) Then
                objPlungerDetail = New PlungerDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalPlunger Is Nothing Then
                    objPlungerDetail.PlungerId = p_objTRMeasurement.OriginalPlunger.PlungerId
                End If
                objPlungerDetail.BreakingEnergy = ConvertToInteger(p_objTRMeasurement.Plunger4)
                objPlungerDetail.Iteration = 4
                plungerDetails.Add(objPlungerDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.Plunger5) Then
                objPlungerDetail = New PlungerDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalPlunger Is Nothing Then
                    objPlungerDetail.PlungerId = p_objTRMeasurement.OriginalPlunger.PlungerId
                End If
                objPlungerDetail.BreakingEnergy = ConvertToInteger(p_objTRMeasurement.Plunger5)
                objPlungerDetail.Iteration = 5
                plungerDetails.Add(objPlungerDetail)
            End If

            Return plungerDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    ' Maps the TR measurement section data to tread wear.

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Maps the TR measurement section data to tread wear.
    ''' </summary>
    ''' <returns>Treadwear Object</returns> 
    ''' <param name="p_objTRMeasurement">TR Measurement</param>
    ''' <param name="p_objProject">Project</param>
    ''' <param name="p_intCertificateNumberID">Certificate Number Id</param>
    ''' <param name="p_intCertType">Certificate Type</param>
    ''' <param name="p_intSKUID">SKU ID</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function MapTRMeasurementSectionDataToTreadWear(ByVal p_objTRMeasurement As TRMeasurementSectionData, _
                                                                  ByVal p_objProject As TRProjectSectionData, _
                                                                  ByVal p_intCertificateNumberID As Integer, _
                                                                  ByVal p_intCertType As Integer, _
                                                                  ByVal p_intSKUID As Integer) As Treadwear
        Try
            Dim objTreadwear As New Treadwear

            If Not p_objTRMeasurement.OriginalTreadwear Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                objTreadwear.TreadWearId = p_objTRMeasurement.OriginalTreadwear.TreadWearId
            End If

            objTreadwear.CertificateNumberID = p_intCertificateNumberID
            objTreadwear.CertificationTypeId = p_intCertType
            objTreadwear.SkuId = p_intSKUID

            If Not String.IsNullOrEmpty(p_objTRMeasurement.TreadwearMatlNum) Then
                objTreadwear.MaterialNumber = p_objTRMeasurement.TreadwearMatlNum
            End If

            If Not String.IsNullOrEmpty(p_objProject.TreadwearProjectNumber) Then
                objTreadwear.ProjectNumber = p_objProject.TreadwearProjectNumber
            End If
            If Not String.IsNullOrEmpty(p_objProject.TreadwearTireNumber) Then
                objTreadwear.TireNumber = ConvertToInteger(p_objProject.TreadwearTireNumber)
            End If
            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not String.IsNullOrEmpty(p_objProject.TreadwearOperation) Then
                objTreadwear.Operation = p_objProject.TreadwearOperation
            End If
            If Not String.IsNullOrEmpty(p_objProject.TreadwearTestSpec) Then
                objTreadwear.TestSpec = p_objProject.TreadwearTestSpec
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.StartDate) Then
                objTreadwear.CompletionDate = ConvertToDateTime(p_objTRMeasurement.StartDate)
            End If

            If Not p_objTRMeasurement.OriginalMeasure Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                objTreadwear.DotSerialNumber = p_objTRMeasurement.OriginalMeasure.DotSerialNumber
                objTreadwear.SerialDate = p_objTRMeasurement.OriginalMeasure.SerialDate
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.TreadwearIndicatorsResult) Then
                objTreadwear.LowestWearBar = ConvertToSingle(p_objTRMeasurement.TreadwearIndicatorsResult)
            End If
            objTreadwear.PassYN = p_objTRMeasurement.TreadwearIndicatorsYN

            If Not String.IsNullOrEmpty(p_objTRMeasurement.TreadwearIndicatorsRequirement) Then
                objTreadwear.IndicatorRequirement = ConvertToSingle(p_objTRMeasurement.TreadwearIndicatorsRequirement)
            End If

            objTreadwear.TreadwearDetails = Me.MapTRMeasurementSectionDataToTreadwearDetail(p_objTRMeasurement)

            objTreadwear.OriginalTreadwear = p_objTRMeasurement.OriginalTreadwear

            If Not String.IsNullOrEmpty(p_objTRMeasurement.GTSpecTreadwearMatlNum) Then
                objTreadwear.GTSpecMaterialNumber = p_objTRMeasurement.GTSpecTreadwearMatlNum
            End If

            Return objTreadwear
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  method to Maps the TR measurement section data to treadwear detail.
    ''' </summary>
    ''' <returns>Tread Wear Detail</returns> 
    ''' <param name="p_objTRMeasurement">TR Measurement.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRMeasurementSectionDataToTreadwearDetail(ByVal p_objTRMeasurement As TRMeasurementSectionData) As List(Of TreadwearDetail)
        Try
            Dim objTreadwearDetail As TreadwearDetail
            Dim treadwearDetails As New List(Of TreadwearDetail)

            If Not String.IsNullOrEmpty(p_objTRMeasurement.TreadwearHeight1) Then
                objTreadwearDetail = New TreadwearDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalTreadwear Is Nothing Then
                    objTreadwearDetail.TreadWearId = p_objTRMeasurement.OriginalTreadwear.TreadWearId
                End If
                objTreadwearDetail.WearBarHeight = ConvertToSingle(p_objTRMeasurement.TreadwearHeight1)
                objTreadwearDetail.Iteration = 1
                treadwearDetails.Add(objTreadwearDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.TreadwearHeight2) Then
                objTreadwearDetail = New TreadwearDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalTreadwear Is Nothing Then
                    objTreadwearDetail.TreadWearId = p_objTRMeasurement.OriginalTreadwear.TreadWearId
                End If
                objTreadwearDetail.WearBarHeight = ConvertToSingle(p_objTRMeasurement.TreadwearHeight2)
                objTreadwearDetail.Iteration = 2
                treadwearDetails.Add(objTreadwearDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.TreadwearHeight3) Then
                objTreadwearDetail = New TreadwearDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalTreadwear Is Nothing Then
                    objTreadwearDetail.TreadWearId = p_objTRMeasurement.OriginalTreadwear.TreadWearId
                End If
                objTreadwearDetail.WearBarHeight = ConvertToSingle(p_objTRMeasurement.TreadwearHeight3)
                objTreadwearDetail.Iteration = 3
                treadwearDetails.Add(objTreadwearDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.TreadwearHeight4) Then
                objTreadwearDetail = New TreadwearDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalTreadwear Is Nothing Then
                    objTreadwearDetail.TreadWearId = p_objTRMeasurement.OriginalTreadwear.TreadWearId
                End If
                objTreadwearDetail.WearBarHeight = ConvertToSingle(p_objTRMeasurement.TreadwearHeight4)
                objTreadwearDetail.Iteration = 4
                treadwearDetails.Add(objTreadwearDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.TreadwearHeight5) Then
                objTreadwearDetail = New TreadwearDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalTreadwear Is Nothing Then
                    objTreadwearDetail.TreadWearId = p_objTRMeasurement.OriginalTreadwear.TreadWearId
                End If
                objTreadwearDetail.WearBarHeight = ConvertToSingle(p_objTRMeasurement.TreadwearHeight5)
                objTreadwearDetail.Iteration = 5
                treadwearDetails.Add(objTreadwearDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.TreadwearHeight6) Then
                objTreadwearDetail = New TreadwearDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalTreadwear Is Nothing Then
                    objTreadwearDetail.TreadWearId = p_objTRMeasurement.OriginalTreadwear.TreadWearId
                End If
                objTreadwearDetail.WearBarHeight = ConvertToSingle(p_objTRMeasurement.TreadwearHeight6)
                objTreadwearDetail.Iteration = 6
                treadwearDetails.Add(objTreadwearDetail)
            End If

            Return treadwearDetails
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation     
    ''' <summary>
    ''' Method to Maps the TR measurement section data to bead unseat.
    ''' </summary>
    ''' <returns>BeadUnseat Object</returns> 
    ''' <param name="p_objTRMeasurement">TR Measurement</param>
    ''' <param name="p_objProject">Project</param>
    ''' <param name="p_intCertificateNumberID">Certificate Number Id</param>
    ''' <param name="p_intCertType">Certificate Type</param>
    ''' <param name="p_intSKUID">SKU ID</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function MapTRMeasurementSectionDataToBeadUnSeat(ByVal p_objTRMeasurement As TRMeasurementSectionData, _
                                                                  ByVal p_objProject As TRProjectSectionData, _
                                                                  ByVal p_intCertificateNumberID As Integer, _
                                                                  ByVal p_intCertType As Integer, _
                                                                  ByVal p_intSKUID As Integer) As BeadUnSeat
        Try
            Dim objBeadUnSeat As New BeadUnSeat

            If Not p_objTRMeasurement.OriginalBeadUnSeat Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                objBeadUnSeat.BeadUnSeatId = p_objTRMeasurement.OriginalBeadUnSeat.BeadUnSeatId
            End If

            objBeadUnSeat.CertificateNumberID = p_intCertificateNumberID
            objBeadUnSeat.CertificationTypeId = p_intCertType
            objBeadUnSeat.SkuId = p_intSKUID

            If Not String.IsNullOrEmpty(p_objTRMeasurement.BeadUnseatMatlNum) Then
                objBeadUnSeat.MaterialNumber = p_objTRMeasurement.BeadUnseatMatlNum
            End If

            If Not String.IsNullOrEmpty(p_objProject.BeadUnSeatProjectNumber) Then
                objBeadUnSeat.ProjectNumber = p_objProject.BeadUnSeatProjectNumber
            End If
            If Not String.IsNullOrEmpty(p_objProject.BeadUnSeatTireNumber) Then
                objBeadUnSeat.TireNumber = ConvertToInteger(p_objProject.BeadUnSeatTireNumber)
            End If
            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not String.IsNullOrEmpty(p_objProject.BeadUnSeatOperation) Then
                objBeadUnSeat.Operation = p_objProject.BeadUnSeatOperation
            End If
            If Not String.IsNullOrEmpty(p_objProject.BeadUnSeatTestSpec) Then
                objBeadUnSeat.TestSpec = p_objProject.BeadUnSeatTestSpec
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.StartDate) Then
                objBeadUnSeat.CompletionDate = ConvertToDateTime(p_objTRMeasurement.StartDate)
            End If

            If Not p_objTRMeasurement.OriginalMeasure Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                objBeadUnSeat.DotSerialNumber = p_objTRMeasurement.OriginalMeasure.DotSerialNumber
                objBeadUnSeat.SerialDate = p_objTRMeasurement.OriginalMeasure.SerialDate
            End If

            objBeadUnSeat.PassYN = p_objTRMeasurement.BeadUnseatTestYN

            Try
                If Not String.IsNullOrEmpty(p_objTRMeasurement.BeadUnseatTestKN) Then
                    objBeadUnSeat.LowestUnSeatValue = CInt(p_objTRMeasurement.BeadUnseatTestKN)
                End If
            Catch ex As Exception
                objBeadUnSeat.LowestUnSeatValue = 0
            End Try

            Try
                If Not String.IsNullOrEmpty(p_objTRMeasurement.BeadUnseatTestKN) Then
                    objBeadUnSeat.MinBeadUnseat = CInt(p_objTRMeasurement.BeadUnseatTestKN)
                End If
            Catch ex As Exception
                objBeadUnSeat.MinBeadUnseat = 0
            End Try

            objBeadUnSeat.TestPassFail = p_objTRMeasurement.BeadUnseatTestYN

            objBeadUnSeat.BeadUnSeatDetails = Me.MapTRMeasurementSectionDataToBeadUnSeatDetail(p_objTRMeasurement)

            objBeadUnSeat.OriginalBeadUnSeat = p_objTRMeasurement.OriginalBeadUnSeat

            If Not String.IsNullOrEmpty(p_objTRMeasurement.GTSpecBeadUnseatMatlNum) Then
                objBeadUnSeat.GTSpecMaterialNumber = p_objTRMeasurement.GTSpecBeadUnseatMatlNum
            End If

            Return objBeadUnSeat
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Map TestResults measurement section data.
    ''' </summary>
    ''' <returns>List of BeadUnsearDetail object</returns> 
    ''' <param name="p_objTRMeasurement">TR Measurement.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRMeasurementSectionDataToBeadUnSeatDetail(ByVal p_objTRMeasurement As TRMeasurementSectionData) As List(Of BeadUnSeatDetail)
        Try
            Dim objBeadUnSeatDetail As New BeadUnSeatDetail
            Dim beadUnseats As New List(Of BeadUnSeatDetail)

            If Not String.IsNullOrEmpty(p_objTRMeasurement.BeadUnseatTest1) Then
                objBeadUnSeatDetail = New BeadUnSeatDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalBeadUnSeat Is Nothing Then
                    objBeadUnSeatDetail.BeadUnSeatId = p_objTRMeasurement.OriginalBeadUnSeat.BeadUnSeatId
                End If
                objBeadUnSeatDetail.UnSeatForce = ConvertToInteger(p_objTRMeasurement.BeadUnseatTest1)
                objBeadUnSeatDetail.Iteration = 1
                beadUnseats.Add(objBeadUnSeatDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.BeadUnseatTest2) Then
                objBeadUnSeatDetail = New BeadUnSeatDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalBeadUnSeat Is Nothing Then
                    objBeadUnSeatDetail.BeadUnSeatId = p_objTRMeasurement.OriginalBeadUnSeat.BeadUnSeatId
                End If
                objBeadUnSeatDetail.UnSeatForce = ConvertToInteger(p_objTRMeasurement.BeadUnseatTest2)
                objBeadUnSeatDetail.Iteration = 2
                beadUnseats.Add(objBeadUnSeatDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.BeadUnseatTest3) Then
                objBeadUnSeatDetail = New BeadUnSeatDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalBeadUnSeat Is Nothing Then
                    objBeadUnSeatDetail.BeadUnSeatId = p_objTRMeasurement.OriginalBeadUnSeat.BeadUnSeatId
                End If
                objBeadUnSeatDetail.UnSeatForce = ConvertToInteger(p_objTRMeasurement.BeadUnseatTest3)
                objBeadUnSeatDetail.Iteration = 3
                beadUnseats.Add(objBeadUnSeatDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.BeadUnseatTest4) Then
                objBeadUnSeatDetail = New BeadUnSeatDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalBeadUnSeat Is Nothing Then
                    objBeadUnSeatDetail.BeadUnSeatId = p_objTRMeasurement.OriginalBeadUnSeat.BeadUnSeatId
                End If
                objBeadUnSeatDetail.UnSeatForce = ConvertToInteger(p_objTRMeasurement.BeadUnseatTest4)
                objBeadUnSeatDetail.Iteration = 4
                beadUnseats.Add(objBeadUnSeatDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRMeasurement.BeadUnseatTest5) Then
                objBeadUnSeatDetail = New BeadUnSeatDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRMeasurement.OriginalBeadUnSeat Is Nothing Then
                    objBeadUnSeatDetail.BeadUnSeatId = p_objTRMeasurement.OriginalBeadUnSeat.BeadUnSeatId
                End If
                objBeadUnSeatDetail.UnSeatForce = ConvertToInteger(p_objTRMeasurement.BeadUnseatTest5)
                objBeadUnSeatDetail.Iteration = 5
                beadUnseats.Add(objBeadUnSeatDetail)
            End If


            Return beadUnseats
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' method to Map TR Endurance General Section Data To Endurance.
    ''' </summary>
    ''' <returns>Endurance Object</returns> 
    ''' <param name="p_intCertificateNumberID">Certificate Number Id.</param>
    ''' <param name="p_intCertType">certificate Type</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_objProject">Project</param>
    ''' <param name="p_objTREndAfterData">TR End After Data</param>
    ''' <param name="p_objTREndBeforeData">TR End Before Data</param>
    ''' <param name="p_objTREndData">TR End Data</param>
    ''' <param name="p_strMatlNum">Material Number</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function MapTREnduranceGeneralSectionDataToEndurance(ByVal p_objTREndBeforeData As TREnduranceTestGeneralBeforeSectionData, _
                                                                ByVal p_objTREndData As TREnduranceSectionData, _
                                                                ByVal p_objTREndAfterData As TREnduranceTestGeneralAfterSectionData, _
                                                                ByVal p_objProject As TRProjectSectionData, _
                                                                ByVal p_intCertificateNumberID As Integer, _
                                                                ByVal p_intCertType As Integer, _
                                                                ByVal p_intSKUID As Integer, _
                                                                ByVal p_strMatlNum As String) As Endurance
        Try
            Dim objEnduance As New Endurance

            If Not p_objTREndData.OriginalEndurance Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                objEnduance.END_ID = p_objTREndData.OriginalEndurance.END_ID

                'NOTE: not directly mapping to database from the view
                objEnduance.PostcondEndDate = p_objTREndData.OriginalEndurance.PostcondEndDate
                objEnduance.PostcondEndTemp = p_objTREndData.OriginalEndurance.PostcondEndTemp
                objEnduance.PostcondStartDate = p_objTREndData.OriginalEndurance.PostcondStartDate
                objEnduance.PrecondEndDate = p_objTREndData.OriginalEndurance.PrecondEndDate
                objEnduance.PrecondEndTemp = p_objTREndData.OriginalEndurance.PrecondEndTemp
                objEnduance.PrecondStartDate = p_objTREndData.OriginalEndurance.PrecondStartDate
                objEnduance.RimDiameter = p_objTREndData.OriginalEndurance.RimDiameter
                objEnduance.RimWidth = p_objTREndData.OriginalEndurance.RimWidth
            End If

            objEnduance.CertificateNumberID = p_intCertificateNumberID
            objEnduance.CertificationTypeId = p_intCertType
            objEnduance.SKUID = p_intSKUID

            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceMatlNum) Then
                objEnduance.MaterialNumber = p_objTREndData.EnduranceMatlNum
            End If

            If Not String.IsNullOrEmpty(p_objProject.EnduranceProjectNumber) Then
                objEnduance.ProjectNumber = p_objProject.EnduranceProjectNumber
            End If
            If Not String.IsNullOrEmpty(p_objProject.EnduranceTireNumber) Then
                objEnduance.TireNumber = ConvertToInteger(p_objProject.EnduranceTireNumber)
            End If
            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not String.IsNullOrEmpty(p_objProject.EnduranceOperation) Then
                objEnduance.Operation = p_objProject.EnduranceOperation
            End If
            If Not String.IsNullOrEmpty(p_objProject.EnduranceTestSpec) Then
                objEnduance.TestSpec = p_objProject.EnduranceTestSpec
            End If

            If Not String.IsNullOrEmpty(p_objTREndBeforeData.EnduranceDiameterTestDrum) Then
                objEnduance.DiameterTestDrum = ConvertToSingle(p_objTREndBeforeData.EnduranceDiameterTestDrum)
            End If

            'objEnduance.SerialDate

            If Not String.IsNullOrEmpty(p_objTREndBeforeData.EndurancePreconditioningTime) Then
                objEnduance.PrecondTime = ConvertToSingle(p_objTREndBeforeData.EndurancePreconditioningTime)
            End If
            If Not String.IsNullOrEmpty(p_objTREndBeforeData.EndurancePreconditioningTemperature) Then
                objEnduance.PrecondStartTemp = ConvertToShort(p_objTREndBeforeData.EndurancePreconditioningTemperature)
            End If
            If Not String.IsNullOrEmpty(p_objTREndBeforeData.EnduranceInflationPressureAdjusted) Then
                objEnduance.InflationPressureReadjusted = ConvertToShort(p_objTREndBeforeData.EnduranceInflationPressureAdjusted)
            End If
            If Not String.IsNullOrEmpty(p_objTREndBeforeData.EnduranceCircumferenceBefore) Then
                objEnduance.CircumferenceBeforeTesting = ConvertToSingle(p_objTREndBeforeData.EnduranceCircumferenceBefore)
            End If

            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceXHours) Then
                objEnduance.EnduranceXHour = ConvertToSingle(p_objTREndData.EnduranceXHours)
            End If

            objEnduance.ResultPassFail = p_objTREndData.EnduranceTestResultYN
            objEnduance.EnduranceTestPassYN = p_objTREndData.EnduranceTestPassYN
            objEnduance.PassYN = p_objTREndData.EnduranceTestPassYN

            If Not String.IsNullOrEmpty(p_objTREndData.PossibleFailuresFound) Then
                objEnduance.PossibleFailuresFound = p_objTREndData.PossibleFailuresFound
            End If
            If Not String.IsNullOrEmpty(p_objTREndAfterData.EndurancePostConditioningTime) Then
                objEnduance.PostcondTime = ConvertToSingle(p_objTREndAfterData.EndurancePostConditioningTime)
            End If
            If Not String.IsNullOrEmpty(p_objTREndAfterData.EnduranceCircumferenceAfter) Then
                objEnduance.CircumferenceAfterTesting = ConvertToSingle(p_objTREndAfterData.EnduranceCircumferenceAfter)
            End If
            If Not String.IsNullOrEmpty(p_objTREndAfterData.EnduranceDifferenceOuterDiameterMMAfter) Then
                objEnduance.OuterDiameterDifference = ConvertToSingle(p_objTREndAfterData.EnduranceDifferenceOuterDiameterMMAfter)
            End If
            If Not String.IsNullOrEmpty(p_objTREndAfterData.EnduranceDifferenceOuterDiameterToleranceAfter) Then
                objEnduance.OuterDiameterTolerance = ConvertToSingle(p_objTREndAfterData.EnduranceDifferenceOuterDiameterToleranceAfter)
            End If
            If Not String.IsNullOrEmpty(p_objTREndAfterData.EnduranceSeriesAfter) Then
                objEnduance.SerieNOM = p_objTREndAfterData.EnduranceSeriesAfter
            End If

            objEnduance.FinalJudgement = p_objTREndAfterData.EnduranceFinalJudgement

            If Not String.IsNullOrEmpty(p_objTREndAfterData.EnduranceApproverAfter) Then
                objEnduance.Approver = p_objTREndAfterData.EnduranceApproverAfter
            End If

            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceDate4) Then
                objEnduance.CompletionDate = ConvertToDateTime(p_objTREndData.EnduranceDate4)
            End If

            If Not String.IsNullOrEmpty(p_objTREndAfterData.EnduranceOuterDiameterAfter) Then
                objEnduance.AfterDiameter = ConvertToSingle(p_objTREndAfterData.EnduranceOuterDiameterAfter)
            End If
            If Not String.IsNullOrEmpty(p_objTREndAfterData.EnduranceTestInflationPressureAfter) Then
                objEnduance.AfterInflation = ConvertToShort(p_objTREndAfterData.EnduranceTestInflationPressureAfter)
            End If
            If Not String.IsNullOrEmpty(p_objTREndBeforeData.EnduranceOuterDiameterBefore) Then
                objEnduance.BeforeDiameter = ConvertToSingle(p_objTREndBeforeData.EnduranceOuterDiameterBefore)
            End If
            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceInflationPressureBefore) Then
                objEnduance.BeforeInflation = ConvertToShort(p_objTREndBeforeData.EnduranceTestInflationPressureBefore)
            End If
            If Not String.IsNullOrEmpty(p_objTREndBeforeData.EnduranceDOTCode) Then
                objEnduance.DotSerialNumber = p_objTREndBeforeData.EnduranceDOTCode
            End If
            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceFinalTotalKM) Then
                objEnduance.FinalDistance = ConvertToSingle(p_objTREndData.EnduranceFinalTotalKM)
            End If
            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceInflationPressureAfter) Then
                objEnduance.FinalInflation = ConvertToShort(p_objTREndData.EnduranceInflationPressureAfter)
            End If
            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceTemperatureAfter) Then
                objEnduance.FinalTemp = ConvertToShort(p_objTREndData.EnduranceTemperatureAfter)
            End If
            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceInflationPressureBefore) Then
                objEnduance.InflationPressure = ConvertToShort(p_objTREndData.EnduranceInflationPressureBefore)
            End If

            If Not String.IsNullOrEmpty(p_objTREndAfterData.EnduranceSeriesAfter) Then
                objEnduance.SerialDate = ConvertToDateTime(p_objTREndAfterData.EnduranceSeriesAfter)
            End If

            If Not String.IsNullOrEmpty(p_objTREndBeforeData.EnduranceTestMachine) Then
                objEnduance.WheelNumber = ConvertToShort(p_objTREndBeforeData.EnduranceTestMachine)
            End If
            If Not String.IsNullOrEmpty(p_objTREndBeforeData.EnduranceTestWheelPosition) Then
                objEnduance.WheelPosition = ConvertToShort(p_objTREndBeforeData.EnduranceTestWheelPosition)
            End If

            'Low Pressure Start Inflation
            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceLowPressureStartInflation) Then
                objEnduance.LowInfStartInflation = ConvertToShort(p_objTREndData.EnduranceLowPressureStartInflation)
            End If

            'Low Pressure End Inflation
            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceLowPressureEndInflation) Then
                objEnduance.LowInfEndInflation = ConvertToShort(p_objTREndData.EnduranceLowPressureEndInflation)
            End If

            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceLowPressureEndTemp) Then
                objEnduance.LowInfEndTemp = ConvertToShort(p_objTREndData.EnduranceLowPressureEndTemp)
            End If

            objEnduance.EnduranceDetails = Me.MapTREnduranceSectionDataToEnduranceDetail(p_objTREndData)

            objEnduance.OriginalEndurance = p_objTREndData.OriginalEndurance

            If Not String.IsNullOrEmpty(p_objTREndData.GTSpecEnduranceMatlNum) Then
                objEnduance.GTSpecMaterialNumber = p_objTREndData.GTSpecEnduranceMatlNum
            End If

            Return objEnduance
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Map TR Endurance Section Data To Endurance Detail.
    ''' </summary>
    ''' <returns>Endurance Detail Object</returns> 
    ''' <param name="p_objTREndData">TR end Data.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTREnduranceSectionDataToEnduranceDetail(ByVal p_objTREndData As TREnduranceSectionData) As List(Of EnduranceDetail)
        Try
            Dim objEnduranceDetail As New EnduranceDetail
            Dim Endurances As New List(Of EnduranceDetail)

            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceDate0) Then
                objEnduranceDetail = New EnduranceDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTREndData.OriginalEndurance Is Nothing Then
                    objEnduranceDetail.END_ID = p_objTREndData.OriginalEndurance.END_ID
                    objEnduranceDetail.Load = p_objTREndData.OriginalEndurance.EnduranceDetails(0).Load
                    objEnduranceDetail.LoadPercent = p_objTREndData.OriginalEndurance.EnduranceDetails(0).LoadPercent
                    objEnduranceDetail.Speed = p_objTREndData.OriginalEndurance.EnduranceDetails(0).Speed
                    objEnduranceDetail.TimeInMin = p_objTREndData.OriginalEndurance.EnduranceDetails(0).TimeInMin
                    objEnduranceDetail.TotMiles = p_objTREndData.OriginalEndurance.EnduranceDetails(0).TotMiles
                Else
                    objEnduranceDetail.Load = 0
                    objEnduranceDetail.LoadPercent = 0
                    objEnduranceDetail.Speed = 0
                    objEnduranceDetail.TimeInMin = 0
                    objEnduranceDetail.TotMiles = 0
                End If
                objEnduranceDetail.Iteration = 0
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceDate0) Then
                    objEnduranceDetail.StepCompletionDate = ConvertToDateTime(p_objTREndData.EnduranceDate0)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceTemperatureBefore) Then
                    objEnduranceDetail.AmbTemp = ConvertToShort(p_objTREndData.EnduranceTemperatureBefore)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceInflationPressureBefore) Then
                    objEnduranceDetail.InfPressure = ConvertToShort(p_objTREndData.EnduranceInflationPressureBefore)
                End If

                objEnduranceDetail.TestStep = 0

                Endurances.Add(objEnduranceDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceDate1) Then
                objEnduranceDetail = New EnduranceDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTREndData.OriginalEndurance Is Nothing Then
                    objEnduranceDetail.END_ID = p_objTREndData.OriginalEndurance.END_ID
                End If
                objEnduranceDetail.Iteration = 1
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceDate1) Then
                    objEnduranceDetail.StepCompletionDate = ConvertToDateTime(p_objTREndData.EnduranceDate1)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceRoomTemperature1) Then
                    objEnduranceDetail.AmbTemp = ConvertToShort(p_objTREndData.EnduranceRoomTemperature1)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceAirPressure1) Then
                    objEnduranceDetail.InfPressure = ConvertToShort(p_objTREndData.EnduranceAirPressure1)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceLoadKG1) Then
                    objEnduranceDetail.Load = ConvertToSingle(p_objTREndData.EnduranceLoadKG1)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceLoadPercentage1) Then
                    objEnduranceDetail.LoadPercent = ConvertToSingle(p_objTREndData.EnduranceLoadPercentage1)
                End If

                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceSpeed) Then
                    objEnduranceDetail.Speed = ConvertToSingle(p_objTREndData.EnduranceSpeed)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceHours1) Then
                    objEnduranceDetail.TimeInMin = ConvertToShort(p_objTREndData.EnduranceHours1)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceTotalKm1) Then
                    objEnduranceDetail.TotMiles = ConvertToSingle(p_objTREndData.EnduranceTotalKm1)
                End If

                'not mapping
                objEnduranceDetail.TestStep = 1

                Endurances.Add(objEnduranceDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceDate2) Then
                objEnduranceDetail = New EnduranceDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTREndData.OriginalEndurance Is Nothing Then
                    objEnduranceDetail.END_ID = p_objTREndData.OriginalEndurance.END_ID
                End If
                objEnduranceDetail.Iteration = 2
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceDate2) Then
                    objEnduranceDetail.StepCompletionDate = ConvertToDateTime(p_objTREndData.EnduranceDate2)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceRoomTemperature2) Then
                    objEnduranceDetail.AmbTemp = ConvertToShort(p_objTREndData.EnduranceRoomTemperature2)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceAirPressure2) Then
                    objEnduranceDetail.InfPressure = ConvertToShort(p_objTREndData.EnduranceAirPressure2)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceLoadKG2) Then
                    objEnduranceDetail.Load = ConvertToSingle(p_objTREndData.EnduranceLoadKG2)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceLoadPercentage2) Then
                    objEnduranceDetail.LoadPercent = ConvertToSingle(p_objTREndData.EnduranceLoadPercentage2)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceSpeed2) Then
                    objEnduranceDetail.Speed = ConvertToSingle(p_objTREndData.EnduranceSpeed2)
                End If

                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceHours2) Then
                    objEnduranceDetail.TimeInMin = ConvertToShort(p_objTREndData.EnduranceHours2)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceTotalKm2) Then
                    objEnduranceDetail.TotMiles = ConvertToSingle(p_objTREndData.EnduranceTotalKm2)
                End If

                'not mapping
                objEnduranceDetail.TestStep = 2

                Endurances.Add(objEnduranceDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceDate3) Then
                objEnduranceDetail = New EnduranceDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTREndData.OriginalEndurance Is Nothing Then
                    objEnduranceDetail.END_ID = p_objTREndData.OriginalEndurance.END_ID
                End If
                objEnduranceDetail.Iteration = 3
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceDate3) Then
                    objEnduranceDetail.StepCompletionDate = ConvertToDateTime(p_objTREndData.EnduranceDate3)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceRoomTemperature3) Then
                    objEnduranceDetail.AmbTemp = ConvertToShort(p_objTREndData.EnduranceRoomTemperature3)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceAirPressure3) Then
                    objEnduranceDetail.InfPressure = ConvertToShort(p_objTREndData.EnduranceAirPressure3)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceLoadKG3) Then
                    objEnduranceDetail.Load = ConvertToSingle(p_objTREndData.EnduranceLoadKG3)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceLoadPercentage3) Then
                    objEnduranceDetail.LoadPercent = ConvertToSingle(p_objTREndData.EnduranceLoadPercentage3)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceSpeed3) Then
                    objEnduranceDetail.Speed = ConvertToSingle(p_objTREndData.EnduranceSpeed3)
                End If

                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceHours3) Then
                    objEnduranceDetail.TimeInMin = ConvertToShort(p_objTREndData.EnduranceHours3)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceTotalKm3) Then
                    objEnduranceDetail.TotMiles = ConvertToSingle(p_objTREndData.EnduranceTotalKm3)
                End If

                'not mapping
                objEnduranceDetail.TestStep = 3

                Endurances.Add(objEnduranceDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTREndData.EnduranceDate4) Then
                objEnduranceDetail = New EnduranceDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTREndData.OriginalEndurance Is Nothing Then
                    objEnduranceDetail.END_ID = p_objTREndData.OriginalEndurance.END_ID
                End If
                objEnduranceDetail.Iteration = 4
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceDate4) Then
                    objEnduranceDetail.StepCompletionDate = ConvertToDateTime(p_objTREndData.EnduranceDate4)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceRoomTemperature4) Then
                    objEnduranceDetail.AmbTemp = ConvertToShort(p_objTREndData.EnduranceRoomTemperature4)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceAirPressure4) Then
                    objEnduranceDetail.InfPressure = ConvertToShort(p_objTREndData.EnduranceAirPressure4)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceLoadKG4) Then
                    objEnduranceDetail.Load = ConvertToSingle(p_objTREndData.EnduranceLoadKG4)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceLoadPercentage4) Then
                    objEnduranceDetail.LoadPercent = ConvertToSingle(p_objTREndData.EnduranceLoadPercentage4)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceSpeed4) Then
                    objEnduranceDetail.Speed = ConvertToSingle(p_objTREndData.EnduranceSpeed4)
                End If

                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceHours4) Then
                    objEnduranceDetail.TimeInMin = ConvertToShort(p_objTREndData.EnduranceHours4)
                End If
                If Not String.IsNullOrEmpty(p_objTREndData.EnduranceTotalKm4) Then
                    objEnduranceDetail.TotMiles = ConvertToSingle(p_objTREndData.EnduranceTotalKm4)
                End If

                'not mapping
                objEnduranceDetail.TestStep = 4

                Endurances.Add(objEnduranceDetail)
            End If

            Return Endurances
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Map TR HighSpeed General Section Data To HighSpeed.
    ''' </summary>
    ''' <returns>High Speed Object</returns> 
    ''' <param name="p_intCertificateNumberID">Certificate Number Id.</param>
    ''' <param name="p_intCertType">certificate Type</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_objProject">Project</param>
    ''' <param name="p_objTRHSAfterData">TRS After Data</param>
    ''' <param name="p_objTRHSBeforeData">TRHS Before Data</param>
    ''' <param name="p_objTRHSData">TRHS Data</param>
    ''' <param name="p_strMatlNum">Material Number</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function MapTRHighSpeedGeneralSectionDataToHighSpeed(ByVal p_objTRHSBeforeData As TRHighSpeedTestGeneralBeforeSectionData, _
                                                                ByVal p_objTRHSData As TRHighSpeedSectionData, _
                                                                ByVal p_objTRHSAfterData As TRHighSpeedTestGeneralAfterSectionData, _
                                                                ByVal p_objProject As TRProjectSectionData, _
                                                                ByVal p_intCertificateNumberID As Integer, _
                                                                ByVal p_intCertType As Integer, _
                                                                ByVal p_intSKUID As Integer, _
                                                                ByVal p_strMatlNum As String) As HighSpeed
        Try
            Dim objHighSpeed As New HighSpeed

            If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                objHighSpeed.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID

                'NOTE: Not directly mapping to database from the view
                objHighSpeed.PostcondEndDate = p_objTRHSData.OriginalHighSpeed.PostcondEndDate
                objHighSpeed.PostcondEndTemp = p_objTRHSData.OriginalHighSpeed.PostcondEndTemp
                objHighSpeed.PostcondStartDate = p_objTRHSData.OriginalHighSpeed.PostcondStartDate
                objHighSpeed.PrecondEndDate = p_objTRHSData.OriginalHighSpeed.PrecondEndDate
                objHighSpeed.PrecondEndTemp = p_objTRHSData.OriginalHighSpeed.PrecondEndTemp
                objHighSpeed.PrecondStartDate = p_objTRHSData.OriginalHighSpeed.PrecondStartDate
                objHighSpeed.PrecondStartTemp = p_objTRHSData.OriginalHighSpeed.PrecondStartTemp
                objHighSpeed.RimDiameter = p_objTRHSData.OriginalHighSpeed.RimDiameter
                objHighSpeed.RimWidth = p_objTRHSData.OriginalHighSpeed.RimWidth
            End If

            objHighSpeed.CertificateNumberID = p_intCertificateNumberID
            objHighSpeed.CertificationTypeId = p_intCertType
            objHighSpeed.SKUID = p_intSKUID

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedMatlNum) Then
                objHighSpeed.MaterialNumber = p_objTRHSData.HighSpeedMatlNum
            End If

            If Not String.IsNullOrEmpty(p_objProject.HighSpeedProjectNumber) Then
                objHighSpeed.ProjectNumber = p_objProject.HighSpeedProjectNumber
            End If

            If Not String.IsNullOrEmpty(p_objProject.HighSpeedTireNumber) Then
                objHighSpeed.TireNumber = ConvertToInteger(p_objProject.HighSpeedTireNumber)
            End If
            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not String.IsNullOrEmpty(p_objProject.HighSpeedOperation) Then
                objHighSpeed.Operation = p_objProject.HighSpeedOperation
            End If
            If Not String.IsNullOrEmpty(p_objProject.HighSpeedTestSpec) Then
                objHighSpeed.TestSpec = p_objProject.HighSpeedTestSpec
            End If

            ''-1 represent the blank field on the view
            If Not String.IsNullOrEmpty(p_objTRHSBeforeData.HighSpeedTestMachine) Then
                objHighSpeed.WheelNumber = ConvertToShort(p_objTRHSBeforeData.HighSpeedTestMachine)
            Else
                objHighSpeed.WheelNumber = -1
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationDate4) Then
                objHighSpeed.CompletionDate = ConvertToDateTime(p_objTRHSData.HighSpeedDurationDate4)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSBeforeData.HighSpeedDiameterTestDrum) Then
                objHighSpeed.DiameterTestDrum = ConvertToSingle(p_objTRHSBeforeData.HighSpeedDiameterTestDrum)
            End If

            'objHighSpeed.SerialDate

            If Not String.IsNullOrEmpty(p_objTRHSBeforeData.HighSpeedDOTCode) Then
                objHighSpeed.DotSerialNumber = p_objTRHSBeforeData.HighSpeedDOTCode
            End If

            ''-1 represent the blank field on the view
            If Not String.IsNullOrEmpty(p_objTRHSBeforeData.HighSpeedTestWheelPosition) Then
                objHighSpeed.WheelPosition = ConvertToShort(p_objTRHSBeforeData.HighSpeedTestWheelPosition)
            Else
                objHighSpeed.WheelPosition = -1
            End If

            If Not String.IsNullOrEmpty(p_objTRHSBeforeData.HighSpeedPreconditioningTime) Then
                objHighSpeed.PrecondTime = ConvertToSingle(p_objTRHSBeforeData.HighSpeedPreconditioningTime)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSBeforeData.HighSpeedPreconditioningTemperature) Then
                objHighSpeed.PrecondTemp = ConvertToSingle(p_objTRHSBeforeData.HighSpeedPreconditioningTemperature)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureBefore) Then
                objHighSpeed.InflationPressure = ConvertToShort(p_objTRHSData.HighSpeedInfPressureBefore)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSBeforeData.HighSpeedInflationPressureAdjusted) Then
                objHighSpeed.InflationPressureReadjusted = ConvertToShort(p_objTRHSBeforeData.HighSpeedInflationPressureAdjusted)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSBeforeData.HighSpeedCircumferenceBefore) Then
                objHighSpeed.CircumferenceBeforeTesting = ConvertToSingle(p_objTRHSBeforeData.HighSpeedCircumferenceBefore)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSBeforeData.HighSpeedOuterDiameterBefore) Then
                objHighSpeed.BeforeDiameter = ConvertToSingle(p_objTRHSBeforeData.HighSpeedOuterDiameterBefore)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSBeforeData.HighSpeedTestInflationPressureBefore) Then
                objHighSpeed.BeforeInflation = ConvertToShort(p_objTRHSBeforeData.HighSpeedTestInflationPressureBefore)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedTotalTime) Then
                objHighSpeed.SpeedTotalTime = ConvertToSingle(p_objTRHSData.HighSpeedTotalTime)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedMaxSpeed) Then
                objHighSpeed.MaxSpeed = ConvertToSingle(p_objTRHSData.HighSpeedMaxSpeed)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedMaxLoad) Then
                objHighSpeed.MaxLoad = ConvertToSingle(p_objTRHSData.HighSpeedMaxLoad)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedWheelSpeedRPM) Then
                objHighSpeed.WheelSpeedRPM = ConvertToSingle(p_objTRHSData.HighSpeedWheelSpeedRPM)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedWheelSpeedKMH) Then
                objHighSpeed.WheelSpeedKMH = ConvertToSingle(p_objTRHSData.HighSpeedWheelSpeedKMH)
            End If

            objHighSpeed.DurationTestPassYN = p_objTRHSData.HighSpeedDurationPassYN

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestPassAt) Then
                objHighSpeed.SpeedTestPassAt = ConvertToSingle(p_objTRHSData.HighSpeedSpeedTestPassAt)
            End If

            objHighSpeed.SpeedTestPassYN = p_objTRHSData.HighSpeedSpeedTestPassYN

            If Not String.IsNullOrEmpty(p_objTRHSAfterData.HighSpeedPostConditioningTime) Then
                objHighSpeed.PostcondTime = ConvertToSingle(p_objTRHSAfterData.HighSpeedPostConditioningTime)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSAfterData.HighSpeedCircumferenceAfter) Then
                objHighSpeed.CircumferenceAfterTesting = ConvertToSingle(p_objTRHSAfterData.HighSpeedCircumferenceAfter)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSAfterData.HighSpeedOuterDiameterAfter) Then
                objHighSpeed.AfterDiameter = ConvertToSingle(p_objTRHSAfterData.HighSpeedOuterDiameterAfter)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSAfterData.HighSpeedTestInflationPressureAfter) Then
                objHighSpeed.AfterInflation = ConvertToShort(p_objTRHSAfterData.HighSpeedTestInflationPressureAfter)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSAfterData.HighSpeedDifferenceOuterDiameterMMAfter) Then
                objHighSpeed.OuterDiameterDifference = ConvertToSingle(p_objTRHSAfterData.HighSpeedDifferenceOuterDiameterMMAfter)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSAfterData.HighSpeedDifferenceOuterDiameterToleranceAfter) Then
                objHighSpeed.OuterDiameterTolerance = ConvertToSingle(p_objTRHSAfterData.HighSpeedDifferenceOuterDiameterToleranceAfter)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSAfterData.HighSpeedSeriesAfter) Then
                objHighSpeed.SerieNOM = p_objTRHSAfterData.HighSpeedSeriesAfter
            End If

            objHighSpeed.FinalJudgement = p_objTRHSAfterData.HighSpeedFinalJudgement

            If Not String.IsNullOrEmpty(p_objTRHSAfterData.HighSpeedApproverAfter) Then
                objHighSpeed.Approver = p_objTRHSAfterData.HighSpeedApproverAfter
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureAfter) Then
                objHighSpeed.FinalInflation = ConvertToShort(p_objTRHSData.HighSpeedInfPressureAfter)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureBefore) Then
                objHighSpeed.BeforeInflation = ConvertToShort(p_objTRHSData.HighSpeedInfPressureBefore)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedTempAfter) Then
                objHighSpeed.FinalTemp = ConvertToShort(p_objTRHSData.HighSpeedTempAfter)
            End If
            objHighSpeed.DurationTestPassYN = p_objTRHSData.HighSpeedDurationPassYN

            objHighSpeed.HighSpeedDetails = Me.MapTRHighSpeedSectionDataToHighSpeedDetail(p_objTRHSData)

            objHighSpeed.SpeedTestDetails = Me.MapTRHighSpeedSectionDataToSpeedTestDetail(p_objTRHSData)

            If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                For i As Integer = 0 To p_objTRHSData.OriginalHighSpeed.HighSpeedDetails.Count - 1 'objHighSpeed.HighSpeedDetails.Count - 1
                    If p_objTRHSData.OriginalHighSpeed.HighSpeedDetails.Count > 0 Then
                        objHighSpeed.HighSpeedDetails(i).InfPressure = p_objTRHSData.OriginalHighSpeed.HighSpeedDetails(i).InfPressure
                    End If
                Next
            End If

            objHighSpeed.OriginalHighSpeed = p_objTRHSData.OriginalHighSpeed

            If Not String.IsNullOrEmpty(p_objTRHSData.GTSpecHighSpeedMatlNum) Then
                objHighSpeed.GTSpecMaterialNumber = p_objTRHSData.GTSpecHighSpeedMatlNum
            End If

            Return objHighSpeed
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Map TR HighSpeed Section Data To HighSpeed Detail.
    ''' </summary>
    ''' <returns>List of HighSpeed Detail.</returns> 
    ''' <param name="p_objTRHSData">TRHS Data.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRHighSpeedSectionDataToHighSpeedDetail(ByVal p_objTRHSData As TRHighSpeedSectionData) As List(Of HighSpeedDetail)
        Try
            Dim objHighSpeedDetail As New HighSpeedDetail
            Dim HighSpeeds As New List(Of HighSpeedDetail)


            objHighSpeedDetail = New HighSpeedDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                objHighSpeedDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
            End If
            'convert the value from range to integer
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationStep0) Then
                Dim tempString As String() = p_objTRHSData.HighSpeedDurationStep0.Split(CChar("-"))
                If Not String.IsNullOrEmpty(tempString(0).Trim()) Then
                    objHighSpeedDetail.TestStep = ConvertToShort(tempString(0).Trim())
                End If
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationDate0) Then
                objHighSpeedDetail.StepCompletionDate = ConvertToDateTime(p_objTRHSData.HighSpeedDurationDate0)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTime0) Then
                    objHighSpeedDetail.StepCompletionDate = objHighSpeedDetail.StepCompletionDate.AddHours(ConvertToDateTime(p_objTRHSData.HighSpeedDurationTime0).Hour)
                End If
            End If
            ''-1 represent the blank field on the view
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationRoomTemp0) Then
                objHighSpeedDetail.AmbTemp = ConvertToShort(p_objTRHSData.HighSpeedDurationRoomTemp0)
            Else
                objHighSpeedDetail.AmbTemp = -1
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoad0) Then
                objHighSpeedDetail.Load = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoad0)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoadPerc0) Then
                objHighSpeedDetail.LoadPercent = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoadPerc0)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTotalTime0) Then
                objHighSpeedDetail.TimeInMin = ConvertToShort(p_objTRHSData.HighSpeedDurationTotalTime0)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationActSpeed0) Then
                objHighSpeedDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedDurationActSpeed0)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureBefore) Then
                objHighSpeedDetail.InfPressure = ConvertToShort(p_objTRHSData.HighSpeedInfPressureBefore)
            End If
            'NOTE: No mapping rules
            objHighSpeedDetail.SetInflation = 0
            objHighSpeedDetail.TotMiles = 0

            HighSpeeds.Add(objHighSpeedDetail)

            objHighSpeedDetail = New HighSpeedDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                objHighSpeedDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationStep1) Then
                objHighSpeedDetail.TestStep = ConvertToShort(p_objTRHSData.HighSpeedDurationStep1)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationDate1) Then
                objHighSpeedDetail.StepCompletionDate = ConvertToDateTime(p_objTRHSData.HighSpeedDurationDate1)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTime1) Then
                    objHighSpeedDetail.StepCompletionDate = objHighSpeedDetail.StepCompletionDate.AddHours(ConvertToDateTime(p_objTRHSData.HighSpeedDurationTime1).Hour)
                End If
            End If
            ''-1 represent the blank field on the view
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationRoomTemp1) Then
                objHighSpeedDetail.AmbTemp = ConvertToShort(p_objTRHSData.HighSpeedDurationRoomTemp1)
            Else
                objHighSpeedDetail.AmbTemp = -1
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoad1) Then
                objHighSpeedDetail.Load = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoad1)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoadPerc1) Then
                objHighSpeedDetail.LoadPercent = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoadPerc1)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTotalTime1) Then
                objHighSpeedDetail.TimeInMin = ConvertToShort(p_objTRHSData.HighSpeedDurationTotalTime1)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationActSpeed1) Then
                objHighSpeedDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedDurationActSpeed1)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureBefore) Then
                objHighSpeedDetail.InfPressure = ConvertToShort(p_objTRHSData.HighSpeedInfPressureBefore)
            End If
            'NOTE: No mapping rules
            objHighSpeedDetail.InfPressure = 0
            objHighSpeedDetail.TotMiles = 0

            HighSpeeds.Add(objHighSpeedDetail)

            objHighSpeedDetail = New HighSpeedDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                objHighSpeedDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationStep2) Then
                objHighSpeedDetail.TestStep = ConvertToShort(p_objTRHSData.HighSpeedDurationStep2)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationDate2) Then
                objHighSpeedDetail.StepCompletionDate = ConvertToDateTime(p_objTRHSData.HighSpeedDurationDate2)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTime2) Then
                    objHighSpeedDetail.StepCompletionDate = objHighSpeedDetail.StepCompletionDate.AddHours(ConvertToDateTime(p_objTRHSData.HighSpeedDurationTime2).Hour)
                End If
            End If
            ''-1 represent the blank field on the view
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationRoomTemp2) Then
                objHighSpeedDetail.AmbTemp = ConvertToShort(p_objTRHSData.HighSpeedDurationRoomTemp2)
            Else
                objHighSpeedDetail.AmbTemp = -1
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoad2) Then
                objHighSpeedDetail.Load = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoad2)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoadPerc2) Then
                objHighSpeedDetail.LoadPercent = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoadPerc2)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTotalTime2) Then
                objHighSpeedDetail.TimeInMin = ConvertToShort(p_objTRHSData.HighSpeedDurationTotalTime2)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationActSpeed2) Then
                objHighSpeedDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedDurationActSpeed2)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureBefore) Then
                objHighSpeedDetail.InfPressure = ConvertToShort(p_objTRHSData.HighSpeedInfPressureBefore)
            End If
            'NOTE: No mapping rules
            objHighSpeedDetail.SetInflation = 0
            objHighSpeedDetail.TotMiles = 0

            HighSpeeds.Add(objHighSpeedDetail)

            objHighSpeedDetail = New HighSpeedDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                objHighSpeedDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationStep3) Then
                objHighSpeedDetail.TestStep = ConvertToShort(p_objTRHSData.HighSpeedDurationStep3)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationDate3) Then
                objHighSpeedDetail.StepCompletionDate = ConvertToDateTime(p_objTRHSData.HighSpeedDurationDate3)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTime3) Then
                    objHighSpeedDetail.StepCompletionDate = objHighSpeedDetail.StepCompletionDate.AddHours(ConvertToDateTime(p_objTRHSData.HighSpeedDurationTime3).Hour)
                End If
            End If
            ''-1 represent the blank field on the view
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationRoomTemp3) Then
                objHighSpeedDetail.AmbTemp = ConvertToShort(p_objTRHSData.HighSpeedDurationRoomTemp3)
            Else
                objHighSpeedDetail.AmbTemp = -1
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoad3) Then
                objHighSpeedDetail.Load = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoad3)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoadPerc3) Then
                objHighSpeedDetail.LoadPercent = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoadPerc3)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTotalTime3) Then
                objHighSpeedDetail.TimeInMin = ConvertToShort(p_objTRHSData.HighSpeedDurationTotalTime3)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationActSpeed3) Then
                objHighSpeedDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedDurationActSpeed3)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureBefore) Then
                objHighSpeedDetail.InfPressure = ConvertToShort(p_objTRHSData.HighSpeedInfPressureBefore)
            End If
            'NOTE: No mapping rules
            objHighSpeedDetail.SetInflation = 0
            objHighSpeedDetail.TotMiles = 0

            HighSpeeds.Add(objHighSpeedDetail)

            objHighSpeedDetail = New HighSpeedDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                objHighSpeedDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationStep4) Then
                objHighSpeedDetail.TestStep = ConvertToShort(p_objTRHSData.HighSpeedDurationStep4)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationDate4) Then
                objHighSpeedDetail.StepCompletionDate = ConvertToDateTime(p_objTRHSData.HighSpeedDurationDate4)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTime4) Then
                    objHighSpeedDetail.StepCompletionDate = objHighSpeedDetail.StepCompletionDate.AddHours(ConvertToDateTime(p_objTRHSData.HighSpeedDurationTime4).Hour)
                End If
            End If
            ''-1 represent the blank field on the view
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationRoomTemp4) Then
                objHighSpeedDetail.AmbTemp = ConvertToShort(p_objTRHSData.HighSpeedDurationRoomTemp4)
            Else
                objHighSpeedDetail.AmbTemp = -1
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoad4) Then
                objHighSpeedDetail.Load = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoad4)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoadPerc4) Then
                objHighSpeedDetail.LoadPercent = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoadPerc4)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTotalTime4) Then
                objHighSpeedDetail.TimeInMin = ConvertToShort(p_objTRHSData.HighSpeedDurationTotalTime4)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationActSpeed4) Then
                objHighSpeedDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedDurationActSpeed4)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureBefore) Then
                objHighSpeedDetail.InfPressure = ConvertToShort(p_objTRHSData.HighSpeedInfPressureBefore)
            End If
            'NOTE: No mapping rules
            objHighSpeedDetail.SetInflation = 0
            objHighSpeedDetail.TotMiles = 0

            HighSpeeds.Add(objHighSpeedDetail)

            objHighSpeedDetail = New HighSpeedDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                objHighSpeedDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationStep5) Then
                objHighSpeedDetail.TestStep = ConvertToShort(p_objTRHSData.HighSpeedDurationStep5)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationDate5) Then
                objHighSpeedDetail.StepCompletionDate = ConvertToDateTime(p_objTRHSData.HighSpeedDurationDate5)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTime5) Then
                    objHighSpeedDetail.StepCompletionDate = objHighSpeedDetail.StepCompletionDate.AddHours(ConvertToDateTime(p_objTRHSData.HighSpeedDurationTime5).Hour)
                End If
            End If
            ''-1 represent the blank field on the view
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationRoomTemp5) Then
                objHighSpeedDetail.AmbTemp = ConvertToShort(p_objTRHSData.HighSpeedDurationRoomTemp5)
            Else
                objHighSpeedDetail.AmbTemp = -1
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoad5) Then
                objHighSpeedDetail.Load = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoad5)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoadPerc5) Then
                objHighSpeedDetail.LoadPercent = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoadPerc5)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTotalTime5) Then
                objHighSpeedDetail.TimeInMin = ConvertToShort(p_objTRHSData.HighSpeedDurationTotalTime5)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationActSpeed5) Then
                objHighSpeedDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedDurationActSpeed5)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureBefore) Then
                objHighSpeedDetail.InfPressure = ConvertToShort(p_objTRHSData.HighSpeedInfPressureBefore)
            End If
            'NOTE: No mapping rules
            objHighSpeedDetail.SetInflation = 0
            objHighSpeedDetail.TotMiles = 0

            HighSpeeds.Add(objHighSpeedDetail)
            'End If


            objHighSpeedDetail = New HighSpeedDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                objHighSpeedDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationStep6) Then
                objHighSpeedDetail.TestStep = ConvertToShort(p_objTRHSData.HighSpeedDurationStep6)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationDate6) Then
                objHighSpeedDetail.StepCompletionDate = ConvertToDateTime(p_objTRHSData.HighSpeedDurationDate6)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTime6) Then
                    objHighSpeedDetail.StepCompletionDate = objHighSpeedDetail.StepCompletionDate.AddHours(ConvertToDateTime(p_objTRHSData.HighSpeedDurationTime6).Hour)
                End If
            End If
            ''-1 represent the blank field on the view
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationRoomTemp6) Then
                objHighSpeedDetail.AmbTemp = ConvertToShort(p_objTRHSData.HighSpeedDurationRoomTemp6)
            Else
                objHighSpeedDetail.AmbTemp = -1
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoad6) Then
                objHighSpeedDetail.Load = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoad6)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoadPerc6) Then
                objHighSpeedDetail.LoadPercent = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoadPerc6)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTotalTime6) Then
                objHighSpeedDetail.TimeInMin = ConvertToShort(p_objTRHSData.HighSpeedDurationTotalTime6)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationActSpeed6) Then
                objHighSpeedDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedDurationActSpeed6)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureBefore) Then
                objHighSpeedDetail.InfPressure = ConvertToShort(p_objTRHSData.HighSpeedInfPressureBefore)
            End If
            'NOTE: No mapping rules
            objHighSpeedDetail.SetInflation = 0
            objHighSpeedDetail.TotMiles = 0

            HighSpeeds.Add(objHighSpeedDetail)

            objHighSpeedDetail = New HighSpeedDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                objHighSpeedDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationStep7) Then
                objHighSpeedDetail.TestStep = ConvertToShort(p_objTRHSData.HighSpeedDurationStep7)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationDate7) Then
                objHighSpeedDetail.StepCompletionDate = ConvertToDateTime(p_objTRHSData.HighSpeedDurationDate7)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTime7) Then
                    objHighSpeedDetail.StepCompletionDate = objHighSpeedDetail.StepCompletionDate.AddHours(ConvertToDateTime(p_objTRHSData.HighSpeedDurationTime7).Hour)
                End If
            End If
            ''-1 represent the blank field on the view
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationRoomTemp7) Then
                objHighSpeedDetail.AmbTemp = ConvertToShort(p_objTRHSData.HighSpeedDurationRoomTemp7)
            Else
                objHighSpeedDetail.AmbTemp = -1
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoad7) Then
                objHighSpeedDetail.Load = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoad7)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoadPerc7) Then
                objHighSpeedDetail.LoadPercent = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoadPerc7)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTotalTime7) Then
                objHighSpeedDetail.TimeInMin = ConvertToShort(p_objTRHSData.HighSpeedDurationTotalTime7)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationActSpeed7) Then
                objHighSpeedDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedDurationActSpeed7)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureBefore) Then
                objHighSpeedDetail.InfPressure = ConvertToShort(p_objTRHSData.HighSpeedInfPressureBefore)
            End If
            'NOTE: No mapping rules
            objHighSpeedDetail.SetInflation = 0
            objHighSpeedDetail.TotMiles = 0

            HighSpeeds.Add(objHighSpeedDetail)


            objHighSpeedDetail = New HighSpeedDetail
            ' Not used for update, prevents unneeded audit entries;
            If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                objHighSpeedDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationStep8) Then
                objHighSpeedDetail.TestStep = ConvertToShort(p_objTRHSData.HighSpeedDurationStep8)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationDate8) Then
                objHighSpeedDetail.StepCompletionDate = ConvertToDateTime(p_objTRHSData.HighSpeedDurationDate8)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTime8) Then
                    objHighSpeedDetail.StepCompletionDate = objHighSpeedDetail.StepCompletionDate.AddHours(ConvertToDateTime(p_objTRHSData.HighSpeedDurationTime8).Hour)
                End If
            End If
            ''-1 represent the blank field on the view
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationRoomTemp8) Then
                objHighSpeedDetail.AmbTemp = ConvertToShort(p_objTRHSData.HighSpeedDurationRoomTemp8)
            Else
                objHighSpeedDetail.AmbTemp = -1
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoad8) Then
                objHighSpeedDetail.Load = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoad8)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationLoadPerc8) Then
                objHighSpeedDetail.LoadPercent = ConvertToSingle(p_objTRHSData.HighSpeedDurationLoadPerc8)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationTotalTime8) Then
                objHighSpeedDetail.TimeInMin = ConvertToShort(p_objTRHSData.HighSpeedDurationTotalTime8)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedDurationActSpeed8) Then
                objHighSpeedDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedDurationActSpeed8)
            End If
            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedInfPressureBefore) Then
                objHighSpeedDetail.InfPressure = ConvertToShort(p_objTRHSData.HighSpeedInfPressureBefore)
            End If
            'NOTE: No mapping rules
            objHighSpeedDetail.SetInflation = 0
            objHighSpeedDetail.TotMiles = 0

            HighSpeeds.Add(objHighSpeedDetail)

            Return HighSpeeds
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map TR HighSpeed Section Data To Speed Test Detail.
    ''' </summary>
    ''' <returns>List of Speed Test Detail</returns> 
    ''' <param name="p_objTRHSData">TRHS Data Object.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRHighSpeedSectionDataToSpeedTestDetail(ByVal p_objTRHSData As TRHighSpeedSectionData) As List(Of SpeedTestDetail)
        Try
            Dim objSpeedTestDetail As New SpeedTestDetail
            Dim SpeedTests As New List(Of SpeedTestDetail)

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestSpeedInitial) Then
                objSpeedTestDetail = New SpeedTestDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                    objSpeedTestDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
                End If
                objSpeedTestDetail.Iteration = 0
                objSpeedTestDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedSpeedTestSpeedInitial)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestTimeInitial) Then
                    objSpeedTestDetail.Time = ConvertToDateTime(p_objTRHSData.HighSpeedSpeedTestTimeInitial)
                End If

                SpeedTests.Add(objSpeedTestDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestSpeed1) Then
                objSpeedTestDetail = New SpeedTestDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                    objSpeedTestDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
                End If
                objSpeedTestDetail.Iteration = 1
                objSpeedTestDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedSpeedTestSpeed1)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestTime1) Then
                    objSpeedTestDetail.Time = ConvertToDateTime(p_objTRHSData.HighSpeedSpeedTestTime1)
                End If

                SpeedTests.Add(objSpeedTestDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestSpeed2) Then
                objSpeedTestDetail = New SpeedTestDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                    objSpeedTestDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
                End If
                objSpeedTestDetail.Iteration = 2
                objSpeedTestDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedSpeedTestSpeed2)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestTime2) Then
                    objSpeedTestDetail.Time = ConvertToDateTime(p_objTRHSData.HighSpeedSpeedTestTime2)
                End If

                SpeedTests.Add(objSpeedTestDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestSpeed3) Then
                objSpeedTestDetail = New SpeedTestDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                    objSpeedTestDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
                End If
                objSpeedTestDetail.Iteration = 3
                objSpeedTestDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedSpeedTestSpeed3)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestTime3) Then
                    objSpeedTestDetail.Time = ConvertToDateTime(p_objTRHSData.HighSpeedSpeedTestTime3)
                End If

                SpeedTests.Add(objSpeedTestDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestSpeed4) Then
                objSpeedTestDetail = New SpeedTestDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                    objSpeedTestDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
                End If
                objSpeedTestDetail.Iteration = 4
                objSpeedTestDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedSpeedTestSpeed4)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestTime4) Then
                    objSpeedTestDetail.Time = ConvertToDateTime(p_objTRHSData.HighSpeedSpeedTestTime4)
                End If

                SpeedTests.Add(objSpeedTestDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestSpeed5) Then
                objSpeedTestDetail = New SpeedTestDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                    objSpeedTestDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
                End If
                objSpeedTestDetail.Iteration = 5
                objSpeedTestDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedSpeedTestSpeed5)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestTime5) Then
                    objSpeedTestDetail.Time = ConvertToDateTime(p_objTRHSData.HighSpeedSpeedTestTime5)
                End If

                SpeedTests.Add(objSpeedTestDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestSpeed6) Then
                objSpeedTestDetail = New SpeedTestDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                    objSpeedTestDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
                End If
                objSpeedTestDetail.Iteration = 6
                objSpeedTestDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedSpeedTestSpeed6)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestTime6) Then
                    objSpeedTestDetail.Time = ConvertToDateTime(p_objTRHSData.HighSpeedSpeedTestTime6)
                End If

                SpeedTests.Add(objSpeedTestDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestSpeed7) Then
                objSpeedTestDetail = New SpeedTestDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                    objSpeedTestDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
                End If
                objSpeedTestDetail.Iteration = 7
                objSpeedTestDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedSpeedTestSpeed7)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestTime7) Then
                    objSpeedTestDetail.Time = ConvertToDateTime(p_objTRHSData.HighSpeedSpeedTestTime7)
                End If

                SpeedTests.Add(objSpeedTestDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestSpeed8) Then
                objSpeedTestDetail = New SpeedTestDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                    objSpeedTestDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
                End If
                objSpeedTestDetail.Iteration = 8
                objSpeedTestDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedSpeedTestSpeed8)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestTime8) Then
                    objSpeedTestDetail.Time = ConvertToDateTime(p_objTRHSData.HighSpeedSpeedTestTime8)
                End If

                SpeedTests.Add(objSpeedTestDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestSpeed9) Then
                objSpeedTestDetail = New SpeedTestDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRHSData.OriginalHighSpeed Is Nothing Then
                    objSpeedTestDetail.HS_ID = p_objTRHSData.OriginalHighSpeed.HS_ID
                End If
                objSpeedTestDetail.Iteration = 9
                objSpeedTestDetail.Speed = ConvertToSingle(p_objTRHSData.HighSpeedSpeedTestSpeed9)
                If Not String.IsNullOrEmpty(p_objTRHSData.HighSpeedSpeedTestTime9) Then
                    objSpeedTestDetail.Time = ConvertToDateTime(p_objTRHSData.HighSpeedSpeedTestTime9)
                End If

                SpeedTests.Add(objSpeedTestDetail)
            End If

            Return SpeedTests
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Map TR SoundWet Section Data To Sound.
    ''' </summary>
    ''' <returns>Sound object</returns> 
    ''' <param name="p_intCertificateNumberID">Certificate Number Id.</param>
    ''' <param name="p_intCertType">Certificate Type</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_objProject">Project</param>
    ''' <param name="p_objTRSWData">TRSWData</param>
    ''' <param name="p_strMatlNum">Material Number</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function MapTRSoundWetSectionDataToSound(ByVal p_objTRSWData As TRSoundWetSectionData, _
                                                            ByVal p_objProject As TRProjectSectionData, _
                                                            ByVal p_intCertificateNumberID As Integer, _
                                                            ByVal p_intCertType As Integer, _
                                                            ByVal p_intSKUID As Integer, _
                                                            ByVal p_strMatlNum As String) As Sound
        Try
            Dim objSound As New Sound

            If Not p_objTRSWData.OriginalSound Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                objSound.SoundID = p_objTRSWData.OriginalSound.SoundID
            End If

            objSound.CertificateNumberID = p_intCertificateNumberID
            objSound.CertificationTypeID = p_intCertType
            objSound.SKUId = p_intSKUID

            If Not String.IsNullOrEmpty(p_objProject.SoundProjectNumber) Then
                objSound.ProjectNumber = p_objProject.SoundProjectNumber
            End If
            If Not String.IsNullOrEmpty(p_objProject.SoundTireNumber) Then
                objSound.TireNumber = ConvertToInteger(p_objProject.SoundTireNumber)
            End If
            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not String.IsNullOrEmpty(p_objProject.SoundOperation) Then
                objSound.Operation = p_objProject.SoundOperation
            End If
            If Not String.IsNullOrEmpty(p_objProject.SoundTestSpec) Then
                objSound.TestSpec = p_objProject.SoundTestSpec
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.SWTestReportNo) Then
                objSound.TestReportNumber = p_objTRSWData.SWTestReportNo
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SWManuNameOrBrandName) Then
                objSound.ManufactureAndBrand = p_objTRSWData.SWManuNameOrBrandName
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SWTireClass) Then
                objSound.TireClass = p_objTRSWData.SWTireClass
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SWCategoryOfUse) Then
                objSound.CategoryOfUse = p_objTRSWData.SWCategoryOfUse
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundDateOfTest) Then
                objSound.DateOfTest = ConvertToDateTime(p_objTRSWData.SoundDateOfTest)
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTestVehicle) Then
                objSound.TestVehicule = p_objTRSWData.SoundTestVehicle
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTestVehicleWheelbase) Then
                objSound.TestVehiculeWheelbase = p_objTRSWData.SoundTestVehicleWheelbase
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundLocationOfTestTrack) Then
                objSound.LocationOfTestTrack = p_objTRSWData.SoundLocationOfTestTrack
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundDateOfTrackCertification) Then
                objSound.DateTrackCertifToISO = CDate(p_objTRSWData.SoundDateOfTrackCertification)
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTireSizeDesignation) Then
                objSound.TireSizeDesignation = p_objTRSWData.SoundTireSizeDesignation
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTireServiceDescription) Then
                objSound.TireServiceDescription = p_objTRSWData.SoundTireServiceDescription
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundReferenceInflationPressure) Then
                objSound.ReferenceInflationPressure = p_objTRSWData.SoundReferenceInflationPressure
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTestMassFL) Then
                objSound.TestMass_FrontL = p_objTRSWData.SoundTestMassFL
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTestMassFR) Then
                objSound.TestMass_FrontR = p_objTRSWData.SoundTestMassFR
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTestMassRL) Then
                objSound.TestMass_RearL = p_objTRSWData.SoundTestMassRL
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTestMassRR) Then
                objSound.TestMass_RearR = p_objTRSWData.SoundTestMassRR
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTireLoadIndexFL) Then
                objSound.TireLoadIndex_FrontL = p_objTRSWData.SoundTireLoadIndexFL
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTireLoadIndexFR) Then
                objSound.TireLoadIndex_FrontR = p_objTRSWData.SoundTireLoadIndexFR
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTireLoadIndexRL) Then
                objSound.TireLoadIndex_RearL = p_objTRSWData.SoundTireLoadIndexRL
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTireLoadIndexRR) Then
                objSound.TireLoadIndex_RearR = p_objTRSWData.SoundTireLoadIndexRR
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundInflationPressureFL) Then
                objSound.InflationPressureCo_FrontL = p_objTRSWData.SoundInflationPressureFL
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundInflationPressureFR) Then
                objSound.InflationPressureCo_FrontR = p_objTRSWData.SoundInflationPressureFR
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundInflationPressureRL) Then
                objSound.InflationPressureCo_RearL = p_objTRSWData.SoundInflationPressureRL
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundInflationPressureRR) Then
                objSound.InflationPressureCo_RearR = p_objTRSWData.SoundInflationPressureRR
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTestRimWidthCode) Then
                objSound.TestRimWidthCode = p_objTRSWData.SoundTestRimWidthCode
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.SoundTemperatureMeasurementSensorType) Then
                objSound.TempMeasureSensorType = p_objTRSWData.SoundTemperatureMeasurementSensorType
            End If

            objSound.SoundDetails = Me.MapTRSoundWetSectionDataToSoundDetail(p_objTRSWData)

            objSound.OriginalSound = p_objTRSWData.OriginalSound

            If Not String.IsNullOrEmpty(p_objTRSWData.GTSpecSoundMatlNum) Then
                objSound.GTSpecMaterialNumber = p_objTRSWData.GTSpecSoundMatlNum
            End If

            Return objSound
        Catch ex As Exception
            Throw
        End Try

    End Function

    ''' <summary>
    ''' Method to Map TRSound Wet Section Data To Sound Detail.
    ''' </summary>
    ''' <returns>List of Sound Detail</returns> 
    ''' <param name="p_objTRSWData">TRSW Data.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRSoundWetSectionDataToSoundDetail(ByVal p_objTRSWData As TRSoundWetSectionData) As List(Of SoundDetail)
        Try
            Dim objSoundDetail As SoundDetail
            Dim SoundDetails As New List(Of SoundDetail)

            If Not String.IsNullOrEmpty(p_objTRSWData.SoundSpeed1) And Not String.IsNullOrEmpty(p_objTRSWData.SoundDirectOfRun1) Then
                objSoundDetail = New SoundDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalSound Is Nothing Then
                    objSoundDetail.SoundID = p_objTRSWData.OriginalSound.SoundID
                End If
                objSoundDetail.Iteration = 1
                objSoundDetail.TestSpeed = p_objTRSWData.SoundSpeed1
                objSoundDetail.DirectionOfRun = p_objTRSWData.SoundDirectOfRun1
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftMeasured1) Then
                    objSoundDetail.SoundLevelLeft = p_objTRSWData.SoundLevelLeftMeasured1
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightMeasured1) Then
                    objSoundDetail.SoundLevelRight = p_objTRSWData.SoundLevelRightMeasured1
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundAirTemp1) Then
                    objSoundDetail.AirTemp = p_objTRSWData.SoundAirTemp1
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundTrackTemp1) Then
                    objSoundDetail.TrackTemp = p_objTRSWData.SoundTrackTemp1
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftTempCorrected1) Then
                    objSoundDetail.SoundLevelLeft_TempCorrected = p_objTRSWData.SoundLevelLeftTempCorrected1
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightTempCorrected1) Then
                    objSoundDetail.SoundLevelRight_TempCorrected = p_objTRSWData.SoundLevelRightTempCorrected1
                End If

                SoundDetails.Add(objSoundDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.SoundSpeed2) And Not String.IsNullOrEmpty(p_objTRSWData.SoundDirectOfRun2) Then
                objSoundDetail = New SoundDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalSound Is Nothing Then
                    objSoundDetail.SoundID = p_objTRSWData.OriginalSound.SoundID
                End If
                objSoundDetail.Iteration = 2
                objSoundDetail.TestSpeed = p_objTRSWData.SoundSpeed2
                objSoundDetail.DirectionOfRun = p_objTRSWData.SoundDirectOfRun2
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftMeasured2) Then
                    objSoundDetail.SoundLevelLeft = p_objTRSWData.SoundLevelLeftMeasured2
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightMeasured2) Then
                    objSoundDetail.SoundLevelRight = p_objTRSWData.SoundLevelRightMeasured2
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundAirTemp2) Then
                    objSoundDetail.AirTemp = p_objTRSWData.SoundAirTemp2
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundTrackTemp2) Then
                    objSoundDetail.TrackTemp = p_objTRSWData.SoundTrackTemp2
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftTempCorrected2) Then
                    objSoundDetail.SoundLevelLeft_TempCorrected = p_objTRSWData.SoundLevelLeftTempCorrected2
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightTempCorrected2) Then
                    objSoundDetail.SoundLevelRight_TempCorrected = p_objTRSWData.SoundLevelRightTempCorrected2
                End If

                SoundDetails.Add(objSoundDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.SoundSpeed3) And Not String.IsNullOrEmpty(p_objTRSWData.SoundDirectOfRun3) Then
                objSoundDetail = New SoundDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalSound Is Nothing Then
                    objSoundDetail.SoundID = p_objTRSWData.OriginalSound.SoundID
                End If
                objSoundDetail.Iteration = 3
                objSoundDetail.TestSpeed = p_objTRSWData.SoundSpeed3
                objSoundDetail.DirectionOfRun = p_objTRSWData.SoundDirectOfRun3
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftMeasured3) Then
                    objSoundDetail.SoundLevelLeft = p_objTRSWData.SoundLevelLeftMeasured3
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightMeasured3) Then
                    objSoundDetail.SoundLevelRight = p_objTRSWData.SoundLevelRightMeasured3
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundAirTemp3) Then
                    objSoundDetail.AirTemp = p_objTRSWData.SoundAirTemp3
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundTrackTemp3) Then
                    objSoundDetail.TrackTemp = p_objTRSWData.SoundTrackTemp3
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftTempCorrected3) Then
                    objSoundDetail.SoundLevelLeft_TempCorrected = p_objTRSWData.SoundLevelLeftTempCorrected3
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightTempCorrected3) Then
                    objSoundDetail.SoundLevelRight_TempCorrected = p_objTRSWData.SoundLevelRightTempCorrected3
                End If

                SoundDetails.Add(objSoundDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.SoundSpeed4) And Not String.IsNullOrEmpty(p_objTRSWData.SoundDirectOfRun4) Then
                objSoundDetail = New SoundDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalSound Is Nothing Then
                    objSoundDetail.SoundID = p_objTRSWData.OriginalSound.SoundID
                End If
                objSoundDetail.Iteration = 4
                objSoundDetail.TestSpeed = p_objTRSWData.SoundSpeed4
                objSoundDetail.DirectionOfRun = p_objTRSWData.SoundDirectOfRun4
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftMeasured4) Then
                    objSoundDetail.SoundLevelLeft = p_objTRSWData.SoundLevelLeftMeasured4
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightMeasured4) Then
                    objSoundDetail.SoundLevelRight = p_objTRSWData.SoundLevelRightMeasured4
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundAirTemp4) Then
                    objSoundDetail.AirTemp = p_objTRSWData.SoundAirTemp4
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundTrackTemp4) Then
                    objSoundDetail.TrackTemp = p_objTRSWData.SoundTrackTemp4
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftTempCorrected4) Then
                    objSoundDetail.SoundLevelLeft_TempCorrected = p_objTRSWData.SoundLevelLeftTempCorrected4
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightTempCorrected4) Then
                    objSoundDetail.SoundLevelRight_TempCorrected = p_objTRSWData.SoundLevelRightTempCorrected4
                End If

                SoundDetails.Add(objSoundDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.SoundSpeed5) And Not String.IsNullOrEmpty(p_objTRSWData.SoundDirectOfRun5) Then
                objSoundDetail = New SoundDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalSound Is Nothing Then
                    objSoundDetail.SoundID = p_objTRSWData.OriginalSound.SoundID
                End If
                objSoundDetail.Iteration = 5
                objSoundDetail.TestSpeed = p_objTRSWData.SoundSpeed5
                objSoundDetail.DirectionOfRun = p_objTRSWData.SoundDirectOfRun5
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftMeasured5) Then
                    objSoundDetail.SoundLevelLeft = p_objTRSWData.SoundLevelLeftMeasured5
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightMeasured5) Then
                    objSoundDetail.SoundLevelRight = p_objTRSWData.SoundLevelRightMeasured5
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundAirTemp5) Then
                    objSoundDetail.AirTemp = p_objTRSWData.SoundAirTemp5
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundTrackTemp5) Then
                    objSoundDetail.TrackTemp = p_objTRSWData.SoundTrackTemp5
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftTempCorrected5) Then
                    objSoundDetail.SoundLevelLeft_TempCorrected = p_objTRSWData.SoundLevelLeftTempCorrected5
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightTempCorrected5) Then
                    objSoundDetail.SoundLevelRight_TempCorrected = p_objTRSWData.SoundLevelRightTempCorrected5
                End If

                SoundDetails.Add(objSoundDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.SoundSpeed6) And Not String.IsNullOrEmpty(p_objTRSWData.SoundDirectOfRun6) Then
                objSoundDetail = New SoundDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalSound Is Nothing Then
                    objSoundDetail.SoundID = p_objTRSWData.OriginalSound.SoundID
                End If
                objSoundDetail.Iteration = 6
                objSoundDetail.TestSpeed = p_objTRSWData.SoundSpeed6
                objSoundDetail.DirectionOfRun = p_objTRSWData.SoundDirectOfRun6
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftMeasured6) Then
                    objSoundDetail.SoundLevelLeft = p_objTRSWData.SoundLevelLeftMeasured6
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightMeasured6) Then
                    objSoundDetail.SoundLevelRight = p_objTRSWData.SoundLevelRightMeasured6
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundAirTemp6) Then
                    objSoundDetail.AirTemp = p_objTRSWData.SoundAirTemp6
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundTrackTemp6) Then
                    objSoundDetail.TrackTemp = p_objTRSWData.SoundTrackTemp6
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftTempCorrected6) Then
                    objSoundDetail.SoundLevelLeft_TempCorrected = p_objTRSWData.SoundLevelLeftTempCorrected6
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightTempCorrected6) Then
                    objSoundDetail.SoundLevelRight_TempCorrected = p_objTRSWData.SoundLevelRightTempCorrected6
                End If

                SoundDetails.Add(objSoundDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.SoundSpeed7) And Not String.IsNullOrEmpty(p_objTRSWData.SoundDirectOfRun7) Then
                objSoundDetail = New SoundDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalSound Is Nothing Then
                    objSoundDetail.SoundID = p_objTRSWData.OriginalSound.SoundID
                End If
                objSoundDetail.Iteration = 7
                objSoundDetail.TestSpeed = p_objTRSWData.SoundSpeed7
                objSoundDetail.DirectionOfRun = p_objTRSWData.SoundDirectOfRun7
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftMeasured7) Then
                    objSoundDetail.SoundLevelLeft = p_objTRSWData.SoundLevelLeftMeasured7
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightMeasured7) Then
                    objSoundDetail.SoundLevelRight = p_objTRSWData.SoundLevelRightMeasured7
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundAirTemp7) Then
                    objSoundDetail.AirTemp = p_objTRSWData.SoundAirTemp7
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundTrackTemp7) Then
                    objSoundDetail.TrackTemp = p_objTRSWData.SoundTrackTemp7
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftTempCorrected7) Then
                    objSoundDetail.SoundLevelLeft_TempCorrected = p_objTRSWData.SoundLevelLeftTempCorrected7
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightTempCorrected7) Then
                    objSoundDetail.SoundLevelRight_TempCorrected = p_objTRSWData.SoundLevelRightTempCorrected7
                End If

                SoundDetails.Add(objSoundDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.SoundSpeed8) And Not String.IsNullOrEmpty(p_objTRSWData.SoundDirectOfRun8) Then
                objSoundDetail = New SoundDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalSound Is Nothing Then
                    objSoundDetail.SoundID = p_objTRSWData.OriginalSound.SoundID
                End If
                objSoundDetail.Iteration = 8
                objSoundDetail.TestSpeed = p_objTRSWData.SoundSpeed8
                objSoundDetail.DirectionOfRun = p_objTRSWData.SoundDirectOfRun8
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftMeasured8) Then
                    objSoundDetail.SoundLevelLeft = p_objTRSWData.SoundLevelLeftMeasured8
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightMeasured8) Then
                    objSoundDetail.SoundLevelRight = p_objTRSWData.SoundLevelRightMeasured8
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundAirTemp8) Then
                    objSoundDetail.AirTemp = p_objTRSWData.SoundAirTemp8
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundTrackTemp8) Then
                    objSoundDetail.TrackTemp = p_objTRSWData.SoundTrackTemp8
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelLeftTempCorrected8) Then
                    objSoundDetail.SoundLevelLeft_TempCorrected = p_objTRSWData.SoundLevelLeftTempCorrected8
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.SoundLevelRightTempCorrected8) Then
                    objSoundDetail.SoundLevelRight_TempCorrected = p_objTRSWData.SoundLevelRightTempCorrected8
                End If

                SoundDetails.Add(objSoundDetail)
            End If
            Return SoundDetails
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    '''  Get the most recent serial date from *HDR tables.
    ''' </summary>
    ''' <returns>WetGrip object</returns> 
    ''' <param name="p_intCertificateNumberID">Certificate Number Id</param>
    ''' <param name="p_intCertType">Certificate Type</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_objProject">Project</param>
    ''' <param name="p_objTRSWData">TRSW Data</param>
    ''' <param name="p_strMatlNum">Material number</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function MapTRSoundWetSectionDataToWetGrip(ByVal p_objTRSWData As TRSoundWetSectionData, _
                                                        ByVal p_objProject As TRProjectSectionData, _
                                                        ByVal p_intCertificateNumberID As Integer, _
                                                        ByVal p_intCertType As Integer, _
                                                        ByVal p_intSKUID As Integer, _
                                                        ByVal p_strMatlNum As String) As WetGrip
        Try
            Dim objWetGrip As New WetGrip

            If Not p_objTRSWData.OriginalSound Is Nothing Then
                ' Not used for update, prevents unneeded audit entries;
                objWetGrip.WetGripID = p_objTRSWData.OriginalWetGrip.WetGripID
            End If

            objWetGrip.CertificateNumberID = p_intCertificateNumberID
            objWetGrip.CertificationTypeID = p_intCertType
            objWetGrip.SKUId = p_intSKUID

            If Not String.IsNullOrEmpty(p_objProject.WetGripProjectNumber) Then
                objWetGrip.ProjectNumber = p_objProject.WetGripProjectNumber
            End If
            If Not String.IsNullOrEmpty(p_objProject.WetGripTireNumber) Then
                objWetGrip.TireNumber = ConvertToInteger(p_objProject.WetGripTireNumber)
            End If
            'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            If Not String.IsNullOrEmpty(p_objProject.WetGripOperation) Then
                objWetGrip.Operation = p_objProject.WetGripOperation
            End If
            If Not String.IsNullOrEmpty(p_objProject.WetGripTestSpec) Then
                objWetGrip.TestSpec = p_objProject.WetGripTestSpec
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.WetDateOfTest) Then
                objWetGrip.DateOfTest = CDate(p_objTRSWData.WetDateOfTest)
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetTestVehicle) Then
                objWetGrip.TestVehicle = p_objTRSWData.WetTestVehicle
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetLocationOfTestTrack) Then
                objWetGrip.LocationOfTestTrack = p_objTRSWData.WetLocationOfTestTrack
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetTestTrackCharacteristics) Then
                objWetGrip.TestTrackCharacteristics = p_objTRSWData.WetTestTrackCharacteristics
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetIssuedBy) Then
                objWetGrip.IssueBy = p_objTRSWData.WetIssuedBy
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetMethodOfCertification) Then
                objWetGrip.MethodOfCertification = p_objTRSWData.WetMethodOfCertification
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetTestTireDetail) Then
                objWetGrip.TestTireDetails = p_objTRSWData.WetTestTireDetail
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetTireSizeDesignation) Then
                objWetGrip.TireSizeAndServiceDesc = p_objTRSWData.WetTireSizeDesignation
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.wetTireBrand) Then
                objWetGrip.TireBrandAndTradeDesc = p_objTRSWData.wetTireBrand
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetReferenceInflationPressure) Then
                objWetGrip.ReferenceInflationPressure = p_objTRSWData.WetReferenceInflationPressure
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetTestRimWidthCode) Then
                objWetGrip.TestRimWithCode = p_objTRSWData.WetTestRimWidthCode
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetTemperatureMeasurementSensorType) Then
                objWetGrip.TempMeasureSensorType = p_objTRSWData.WetTemperatureMeasurementSensorType
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetIdentificationOfSRTT) Then
                objWetGrip.IdentificationSRTT = p_objTRSWData.WetIdentificationOfSRTT
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetTestTireLoadSRTT) Then
                objWetGrip.TestTireLoad_SRTT = p_objTRSWData.WetTestTireLoadSRTT
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetTestTireLoadCandidate) Then
                objWetGrip.TestTireLoad_Candidate = p_objTRSWData.WetTestTireLoadCandidate
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetTestTireLoadControl) Then
                objWetGrip.TestTireLoad_Control = p_objTRSWData.WetTestTireLoadControl
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetWaterDepthSRTT) Then
                objWetGrip.WaterDepth_SRTT = p_objTRSWData.WetWaterDepthSRTT
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetWaterDepthCandidate) Then
                objWetGrip.WaterDepth_Candidate = p_objTRSWData.WetWaterDepthCandidate
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetWaterDepthControl) Then
                objWetGrip.WaterDepth_Control = p_objTRSWData.WetWaterDepthControl
            End If
            If Not String.IsNullOrEmpty(p_objTRSWData.WetWettedTrackTemperature) Then
                objWetGrip.WettedTrackTempAvg = p_objTRSWData.WetWettedTrackTemperature
            End If

            objWetGrip.WetGripDetails = Me.MapTRSoundWetSectionDataToWetGripDetail(p_objTRSWData)

            objWetGrip.OriginalWetGrip = p_objTRSWData.OriginalWetGrip

            If Not String.IsNullOrEmpty(p_objTRSWData.GTSpecWetGripMatlNum) Then
                objWetGrip.GTSpecMaterialNumber = p_objTRSWData.GTSpecWetGripMatlNum
            End If

            Return objWetGrip
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map TRSound Wet Section Data To WetGrip Detail.
    ''' </summary>
    ''' <returns>List of WetGrip Detail</returns> 
    ''' <param name="p_objTRSWData">TRSW data.</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function MapTRSoundWetSectionDataToWetGripDetail(ByVal p_objTRSWData As TRSoundWetSectionData) As List(Of WetGripDetail)
        Try
            Dim objWetGripDetail As WetGripDetail
            Dim WetGripDetails As New List(Of WetGripDetail)

            If Not String.IsNullOrEmpty(p_objTRSWData.WetSpeed1) And Not String.IsNullOrEmpty(p_objTRSWData.WetDirectOfRun1) Then
                objWetGripDetail = New WetGripDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalWetGrip Is Nothing Then
                    objWetGripDetail.WetGripID = p_objTRSWData.OriginalWetGrip.WetGripID
                End If
                objWetGripDetail.Iteration = 1
                objWetGripDetail.TestSpeed = p_objTRSWData.WetSpeed1
                objWetGripDetail.DirectionOfRun = p_objTRSWData.WetDirectOfRun1
                If Not String.IsNullOrEmpty(p_objTRSWData.WetSRTT1) Then
                    objWetGripDetail.SRTT = p_objTRSWData.WetSRTT1
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetCandidateTire1) Then
                    objWetGripDetail.CandidateTire = p_objTRSWData.WetCandidateTire1
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetPBFC1) Then
                    objWetGripDetail.PeakBreakForceCoeficient = p_objTRSWData.WetPBFC1
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetMFDD1) Then
                    objWetGripDetail.MeanFullyDevelopedDeceleration = p_objTRSWData.WetMFDD1
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetWetGripIndex1) Then
                    objWetGripDetail.WetGripIndex = p_objTRSWData.WetWetGripIndex1
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetComments1) Then
                    objWetGripDetail.Comments = p_objTRSWData.WetComments1
                End If

                WetGripDetails.Add(objWetGripDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.WetSpeed2) And Not String.IsNullOrEmpty(p_objTRSWData.WetDirectOfRun2) Then
                objWetGripDetail = New WetGripDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalWetGrip Is Nothing Then
                    objWetGripDetail.WetGripID = p_objTRSWData.OriginalWetGrip.WetGripID
                End If
                objWetGripDetail.Iteration = 2
                objWetGripDetail.TestSpeed = p_objTRSWData.WetSpeed2
                objWetGripDetail.DirectionOfRun = p_objTRSWData.WetDirectOfRun2
                If Not String.IsNullOrEmpty(p_objTRSWData.WetSRTT2) Then
                    objWetGripDetail.SRTT = p_objTRSWData.WetSRTT2
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetCandidateTire2) Then
                    objWetGripDetail.CandidateTire = p_objTRSWData.WetCandidateTire2
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetPBFC2) Then
                    objWetGripDetail.PeakBreakForceCoeficient = p_objTRSWData.WetPBFC2
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetMFDD2) Then
                    objWetGripDetail.MeanFullyDevelopedDeceleration = p_objTRSWData.WetMFDD2
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetWetGripIndex2) Then
                    objWetGripDetail.WetGripIndex = p_objTRSWData.WetWetGripIndex2
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetComments2) Then
                    objWetGripDetail.Comments = p_objTRSWData.WetComments2
                End If

                WetGripDetails.Add(objWetGripDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.WetSpeed3) And Not String.IsNullOrEmpty(p_objTRSWData.WetDirectOfRun3) Then
                objWetGripDetail = New WetGripDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalWetGrip Is Nothing Then
                    objWetGripDetail.WetGripID = p_objTRSWData.OriginalWetGrip.WetGripID
                End If
                objWetGripDetail.Iteration = 3
                objWetGripDetail.TestSpeed = p_objTRSWData.WetSpeed3
                objWetGripDetail.DirectionOfRun = p_objTRSWData.WetDirectOfRun3
                If Not String.IsNullOrEmpty(p_objTRSWData.WetSRTT3) Then
                    objWetGripDetail.SRTT = p_objTRSWData.WetSRTT3
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetCandidateTire3) Then
                    objWetGripDetail.CandidateTire = p_objTRSWData.WetCandidateTire3
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetPBFC3) Then
                    objWetGripDetail.PeakBreakForceCoeficient = p_objTRSWData.WetPBFC3
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetMFDD3) Then
                    objWetGripDetail.MeanFullyDevelopedDeceleration = p_objTRSWData.WetMFDD3
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetWetGripIndex3) Then
                    objWetGripDetail.WetGripIndex = p_objTRSWData.WetWetGripIndex3
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetComments3) Then
                    objWetGripDetail.Comments = p_objTRSWData.WetComments3
                End If

                WetGripDetails.Add(objWetGripDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.WetSpeed4) And Not String.IsNullOrEmpty(p_objTRSWData.WetDirectOfRun4) Then
                objWetGripDetail = New WetGripDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalWetGrip Is Nothing Then
                    objWetGripDetail.WetGripID = p_objTRSWData.OriginalWetGrip.WetGripID
                End If
                objWetGripDetail.Iteration = 4
                objWetGripDetail.TestSpeed = p_objTRSWData.WetSpeed1
                objWetGripDetail.DirectionOfRun = p_objTRSWData.WetDirectOfRun1
                If Not String.IsNullOrEmpty(p_objTRSWData.WetSRTT4) Then
                    objWetGripDetail.SRTT = p_objTRSWData.WetSRTT4
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetCandidateTire4) Then
                    objWetGripDetail.CandidateTire = p_objTRSWData.WetCandidateTire4
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetPBFC4) Then
                    objWetGripDetail.PeakBreakForceCoeficient = p_objTRSWData.WetPBFC4
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetMFDD4) Then
                    objWetGripDetail.MeanFullyDevelopedDeceleration = p_objTRSWData.WetMFDD4
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetWetGripIndex4) Then
                    objWetGripDetail.WetGripIndex = p_objTRSWData.WetWetGripIndex4
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetComments4) Then
                    objWetGripDetail.Comments = p_objTRSWData.WetComments4
                End If

                WetGripDetails.Add(objWetGripDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.WetSpeed5) And Not String.IsNullOrEmpty(p_objTRSWData.WetDirectOfRun5) Then
                objWetGripDetail = New WetGripDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalWetGrip Is Nothing Then
                    objWetGripDetail.WetGripID = p_objTRSWData.OriginalWetGrip.WetGripID
                End If
                objWetGripDetail.Iteration = 5
                objWetGripDetail.TestSpeed = p_objTRSWData.WetSpeed5
                objWetGripDetail.DirectionOfRun = p_objTRSWData.WetDirectOfRun5
                If Not String.IsNullOrEmpty(p_objTRSWData.WetSRTT5) Then
                    objWetGripDetail.SRTT = p_objTRSWData.WetSRTT5
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetCandidateTire5) Then
                    objWetGripDetail.CandidateTire = p_objTRSWData.WetCandidateTire5
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetPBFC5) Then
                    objWetGripDetail.PeakBreakForceCoeficient = p_objTRSWData.WetPBFC5
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetMFDD5) Then
                    objWetGripDetail.MeanFullyDevelopedDeceleration = p_objTRSWData.WetMFDD5
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetWetGripIndex5) Then
                    objWetGripDetail.WetGripIndex = p_objTRSWData.WetWetGripIndex5
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetComments5) Then
                    objWetGripDetail.Comments = p_objTRSWData.WetComments5
                End If

                WetGripDetails.Add(objWetGripDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.WetSpeed6) And Not String.IsNullOrEmpty(p_objTRSWData.WetDirectOfRun6) Then
                objWetGripDetail = New WetGripDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalWetGrip Is Nothing Then
                    objWetGripDetail.WetGripID = p_objTRSWData.OriginalWetGrip.WetGripID
                End If
                objWetGripDetail.Iteration = 6
                objWetGripDetail.TestSpeed = p_objTRSWData.WetSpeed6
                objWetGripDetail.DirectionOfRun = p_objTRSWData.WetDirectOfRun6
                If Not String.IsNullOrEmpty(p_objTRSWData.WetSRTT6) Then
                    objWetGripDetail.SRTT = p_objTRSWData.WetSRTT6
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetCandidateTire6) Then
                    objWetGripDetail.CandidateTire = p_objTRSWData.WetCandidateTire6
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetPBFC6) Then
                    objWetGripDetail.PeakBreakForceCoeficient = p_objTRSWData.WetPBFC6
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetMFDD6) Then
                    objWetGripDetail.MeanFullyDevelopedDeceleration = p_objTRSWData.WetMFDD6
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetWetGripIndex6) Then
                    objWetGripDetail.WetGripIndex = p_objTRSWData.WetWetGripIndex6
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetComments6) Then
                    objWetGripDetail.Comments = p_objTRSWData.WetComments6
                End If

                WetGripDetails.Add(objWetGripDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.WetSpeed7) And Not String.IsNullOrEmpty(p_objTRSWData.WetDirectOfRun7) Then
                objWetGripDetail = New WetGripDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalWetGrip Is Nothing Then
                    objWetGripDetail.WetGripID = p_objTRSWData.OriginalWetGrip.WetGripID
                End If
                objWetGripDetail.Iteration = 7
                objWetGripDetail.TestSpeed = p_objTRSWData.WetSpeed7
                objWetGripDetail.DirectionOfRun = p_objTRSWData.WetDirectOfRun7
                If Not String.IsNullOrEmpty(p_objTRSWData.WetSRTT7) Then
                    objWetGripDetail.SRTT = p_objTRSWData.WetSRTT7
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetCandidateTire7) Then
                    objWetGripDetail.CandidateTire = p_objTRSWData.WetCandidateTire7
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetPBFC7) Then
                    objWetGripDetail.PeakBreakForceCoeficient = p_objTRSWData.WetPBFC7
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetMFDD7) Then
                    objWetGripDetail.MeanFullyDevelopedDeceleration = p_objTRSWData.WetMFDD7
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetWetGripIndex7) Then
                    objWetGripDetail.WetGripIndex = p_objTRSWData.WetWetGripIndex7
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetComments7) Then
                    objWetGripDetail.Comments = p_objTRSWData.WetComments7
                End If

                WetGripDetails.Add(objWetGripDetail)
            End If

            If Not String.IsNullOrEmpty(p_objTRSWData.WetSpeed8) And Not String.IsNullOrEmpty(p_objTRSWData.WetDirectOfRun8) Then
                objWetGripDetail = New WetGripDetail
                ' Not used for update, prevents unneeded audit entries;
                If Not p_objTRSWData.OriginalWetGrip Is Nothing Then
                    objWetGripDetail.WetGripID = p_objTRSWData.OriginalWetGrip.WetGripID
                End If
                objWetGripDetail.Iteration = 8
                objWetGripDetail.TestSpeed = p_objTRSWData.WetSpeed8
                objWetGripDetail.DirectionOfRun = p_objTRSWData.WetDirectOfRun8
                If Not String.IsNullOrEmpty(p_objTRSWData.WetSRTT8) Then
                    objWetGripDetail.SRTT = p_objTRSWData.WetSRTT8
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetCandidateTire8) Then
                    objWetGripDetail.CandidateTire = p_objTRSWData.WetCandidateTire8
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetPBFC8) Then
                    objWetGripDetail.PeakBreakForceCoeficient = p_objTRSWData.WetPBFC8
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetMFDD8) Then
                    objWetGripDetail.MeanFullyDevelopedDeceleration = p_objTRSWData.WetMFDD8
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetWetGripIndex8) Then
                    objWetGripDetail.WetGripIndex = p_objTRSWData.WetWetGripIndex8
                End If
                If Not String.IsNullOrEmpty(p_objTRSWData.WetComments8) Then
                    objWetGripDetail.Comments = p_objTRSWData.WetComments8
                End If

                WetGripDetails.Add(objWetGripDetail)
            End If

            Return WetGripDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
 
    ''' <summary>
    ''' Method to  Audit test data.
    ''' </summary>
    ''' <returns>List of Audit Log Entry</returns> 
    ''' <param name="p_strAreaOfChange">Area of Change</param>
    ''' <param name="p_objProduct">Product</param>
    ''' <param name="p_objMeasure">Measure</param>
    ''' <param name="p_objPlunger">Plunger</param>
    ''' <param name="p_objTreadwear">Treadwear</param>
    ''' <param name="p_objBeadUnseat">BeadUnseat</param>
    ''' <param name="p_objEndurance">Endurance</param>
    ''' <param name="p_objHighSpeed">High Speed</param>
    ''' <param name="p_objSound">Sound</param>
    ''' <param name="p_objWetGrip">WetGrip</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function AuditTestData(ByVal p_strAreaOfChange As String, _
                                            ByVal p_objProduct As Product, ByVal p_objMeasure As Measure, _
                                            ByVal p_objPlunger As Plunger, ByVal p_objTreadwear As Treadwear, _
                                            ByVal p_objBeadUnseat As BeadUnSeat, ByVal p_objEndurance As Endurance, _
                                            ByVal p_objHighSpeed As HighSpeed, ByVal p_objSound As Sound, _
                                            ByVal p_objWetGrip As WetGrip) As List(Of AuditLogEntry)

        Try
            Dim objAuditLog As New AuditLog(Of Product)(p_objProduct.OriginalProduct, p_objProduct)
            Dim listProductAuditLog As List(Of AuditLogEntry)
            Dim listTestDataAuditLog As New List(Of AuditLogEntry)
            Dim listMeasureAuditLog As List(Of AuditLogEntry)
            Dim listBeadUnseatAuditLog As List(Of AuditLogEntry)
            Dim listPlungerAuditLog As List(Of AuditLogEntry)
            Dim listTreadwearAuditLog As List(Of AuditLogEntry)
            Dim listEnduranceAuditLog As List(Of AuditLogEntry)
            Dim listHighSpeedAuditLog As List(Of AuditLogEntry)
            Dim listSoundAuditLog As List(Of AuditLogEntry)
            Dim listWetGripAuditLog As List(Of AuditLogEntry)

            listProductAuditLog = objAuditLog.RunAudit(p_strAreaOfChange)
            If listProductAuditLog.Count > 0 Then
                Return listProductAuditLog
            End If

            listMeasureAuditLog = RunAuditHdrDtl(p_strAreaOfChange, p_objMeasure)
            If listMeasureAuditLog.Count > 0 Then
                Return listMeasureAuditLog
            End If

            listBeadUnseatAuditLog = RunAuditHdrDtl(p_strAreaOfChange, p_objBeadUnseat)
            If listBeadUnseatAuditLog.Count > 0 Then
                Return listBeadUnseatAuditLog
            End If

            listPlungerAuditLog = RunAuditHdrDtl(p_strAreaOfChange, p_objPlunger)
            If listPlungerAuditLog.Count > 0 Then
                Return listPlungerAuditLog
            End If

            listTreadwearAuditLog = RunAuditHdrDtl(p_strAreaOfChange, p_objTreadwear)
            If listTreadwearAuditLog.Count > 0 Then
                Return listTreadwearAuditLog
            End If

            listEnduranceAuditLog = RunAuditHdrDtl(p_strAreaOfChange, p_objEndurance)
            If listEnduranceAuditLog.Count > 0 Then
                Return listEnduranceAuditLog
            End If

            listHighSpeedAuditLog = RunAuditHdrDtl(p_strAreaOfChange, p_objHighSpeed)
            If listHighSpeedAuditLog.Count > 0 Then
                Return listHighSpeedAuditLog
            End If

            listSoundAuditLog = RunAuditHdrDtl(p_strAreaOfChange, p_objSound)
            If listSoundAuditLog.Count > 0 Then
                Return listSoundAuditLog
            End If

            listWetGripAuditLog = RunAuditHdrDtl(p_strAreaOfChange, p_objWetGrip)
            If listWetGripAuditLog.Count > 0 Then
                Return listWetGripAuditLog
            End If

            Return listTestDataAuditLog
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Run audit of HDR / DTL tables.
    ''' </summary>
    ''' <returns>List of Audit Log Entry</returns> 
    ''' <param name="p_objMeasure">ObjMeasure</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function RunAuditHdrDtl(ByVal p_strAreaOfChange As String, ByVal p_objMeasure As Measure) As List(Of AuditLogEntry)
        Try
            Debug.WriteLine("RunAuditHdrDtl - Measure")
            Dim objAuditLogHdr As New AuditLog(Of Measure)(p_objMeasure.OriginalMeasure, p_objMeasure)
            Dim listMeasureAuditLog As List(Of AuditLogEntry) = objAuditLogHdr.RunAudit(p_strAreaOfChange)

            Dim objAuditLogDtl As AuditLog(Of MeasureDetail) = Nothing
            Dim listMeasureDtlAuditLog As List(Of AuditLogEntry)
            For i As Integer = 0 To p_objMeasure.MeasureDetails.Count - 1 Step 1

                If p_objMeasure.OriginalMeasure Is Nothing Then Exit For
                If i = p_objMeasure.OriginalMeasure.MeasureDetails.Count Then Exit For

                Dim detail As MeasureDetail = p_objMeasure.MeasureDetails(i)
                Dim detailOrig As MeasureDetail = p_objMeasure.OriginalMeasure.MeasureDetails(i)

                objAuditLogDtl = New AuditLog(Of MeasureDetail)(detailOrig, detail)
                listMeasureDtlAuditLog = objAuditLogDtl.RunAudit(p_strAreaOfChange)

                For j As Integer = 0 To listMeasureDtlAuditLog.Count() - 1
                    listMeasureAuditLog.Add(listMeasureDtlAuditLog.Item(j))
                Next
            Next

            Return listMeasureAuditLog
        Catch ex As Exception
            Throw
        End Try
    End Function
 
    ''' <summary>
    '''  Method to Run audit of HDR / DTL tables
    ''' </summary>
    ''' <returns>List of AuditLogEntry</returns> 
    ''' <param name="p_objHighSpeed"> ObjHighSpeed</param>
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function RunAuditHdrDtl(ByVal p_strAreaOfChange As String, ByVal p_objHighSpeed As HighSpeed) As List(Of AuditLogEntry)
        Try
            Debug.WriteLine("RunAuditHdrDtl - HighSpeed")
            Dim objAuditLogHdr As New AuditLog(Of HighSpeed)(p_objHighSpeed.OriginalHighSpeed, p_objHighSpeed)
            Dim listHighSpeedAuditLog As List(Of AuditLogEntry) = objAuditLogHdr.RunAudit(p_strAreaOfChange)

            Dim objAuditLogDtl As AuditLog(Of HighSpeedDetail) = Nothing
            Dim listHighSpeedDtlAuditLog As List(Of AuditLogEntry)
            For i As Integer = 0 To p_objHighSpeed.HighSpeedDetails.Count - 1 Step 1

                If p_objHighSpeed.OriginalHighSpeed Is Nothing Then Exit For
                If i = p_objHighSpeed.OriginalHighSpeed.HighSpeedDetails.Count Then Exit For

                Dim detail As HighSpeedDetail = p_objHighSpeed.HighSpeedDetails(i)
                Dim detailOrig As HighSpeedDetail = p_objHighSpeed.OriginalHighSpeed.HighSpeedDetails(i)

                objAuditLogDtl = New AuditLog(Of HighSpeedDetail)(detailOrig, detail)
                listHighSpeedDtlAuditLog = objAuditLogDtl.RunAudit(p_strAreaOfChange)

                For j As Integer = 0 To listHighSpeedDtlAuditLog.Count() - 1
                    listHighSpeedAuditLog.Add(listHighSpeedDtlAuditLog.Item(j))
                Next
            Next

            Return listHighSpeedAuditLog
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Run audit of HDR / DTL tables.
    ''' </summary>
    ''' <returns>List of Audit Log Entry</returns> 
    ''' <param name="p_objSound">Sound.</param>
    ''' <param name="p_strAreaOfChange">Area of Change.</param>    
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function RunAuditHdrDtl(ByVal p_strAreaOfChange As String, ByVal p_objSound As Sound) As List(Of AuditLogEntry)
        Try
            Debug.WriteLine("RunAuditHdrDtl - Sound")
            Dim objAuditLogHdr As New AuditLog(Of Sound)(p_objSound.OriginalSound, p_objSound)
            Dim listSoundAuditLog As List(Of AuditLogEntry) = objAuditLogHdr.RunAudit(p_strAreaOfChange)

            Dim objAuditLogDtl As AuditLog(Of SoundDetail) = Nothing
            Dim listSoundDtlAuditLog As List(Of AuditLogEntry)
            For i As Integer = 0 To p_objSound.SoundDetails.Count - 1 Step 1

                If p_objSound.OriginalSound Is Nothing Then Exit For
                If i = p_objSound.OriginalSound.SoundDetails.Count Then Exit For

                Dim detail As SoundDetail = p_objSound.SoundDetails(i)
                Dim detailOrig As SoundDetail = p_objSound.OriginalSound.SoundDetails(i)

                objAuditLogDtl = New AuditLog(Of SoundDetail)(detailOrig, detail)
                listSoundDtlAuditLog = objAuditLogDtl.RunAudit(p_strAreaOfChange)

                For j As Integer = 0 To listSoundDtlAuditLog.Count() - 1
                    listSoundAuditLog.Add(listSoundDtlAuditLog.Item(j))
                Next
            Next

            Return listSoundAuditLog
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Run audit of HDR / DTL tables.
    ''' </summary>
    ''' <returns>List of Audit Log Entry</returns> 
    ''' <param name="p_objWetGrip">Wet Grip.</param>
    ''' <param name="p_strAreaOfChange">Area of Change.</param>    
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function RunAuditHdrDtl(ByVal p_strAreaOfChange As String, ByVal p_objWetGrip As WetGrip) As List(Of AuditLogEntry)
        Try
            Debug.WriteLine("RunAuditHdrDtl - WetGrip")
            Dim objAuditLogHdr As New AuditLog(Of WetGrip)(p_objWetGrip.OriginalWetGrip, p_objWetGrip)
            Dim listWetGripAuditLog As List(Of AuditLogEntry) = objAuditLogHdr.RunAudit(p_strAreaOfChange)

            Dim objAuditLogDtl As AuditLog(Of WetGripDetail) = Nothing
            Dim listWetGripDtlAuditLog As List(Of AuditLogEntry)
            For i As Integer = 0 To p_objWetGrip.WetGripDetails.Count - 1 Step 1

                If p_objWetGrip.OriginalWetGrip Is Nothing Then Exit For
                If i = p_objWetGrip.OriginalWetGrip.WetGripDetails.Count Then Exit For

                Dim detail As WetGripDetail = p_objWetGrip.WetGripDetails(i)
                Dim detailOrig As WetGripDetail = p_objWetGrip.OriginalWetGrip.WetGripDetails(i)

                objAuditLogDtl = New AuditLog(Of WetGripDetail)(detailOrig, detail)
                listWetGripDtlAuditLog = objAuditLogDtl.RunAudit(p_strAreaOfChange)

                For j As Integer = 0 To listWetGripDtlAuditLog.Count() - 1
                    listWetGripAuditLog.Add(listWetGripDtlAuditLog.Item(j))
                Next
            Next

            Return listWetGripAuditLog
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Run audit of HDR / DTL tables.
    ''' </summary>
    ''' <returns>List of Audit Log Entry</returns> 
    ''' <param name="p_objPlunger">Plunger.</param>
    ''' <param name="p_strAreaOfChange">Area of Change.</param>    
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
    ''' <para>10/09/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function RunAuditHdrDtl(ByVal p_strAreaOfChange As String, ByVal p_objPlunger As Plunger) As List(Of AuditLogEntry)
        Try
            Debug.WriteLine("RunAuditHdrDtl - Plunger")
            Dim objAuditLogHdr As New AuditLog(Of Plunger)(p_objPlunger.OriginalPlunger, p_objPlunger)
            Dim listPlungerAuditLog As List(Of AuditLogEntry) = objAuditLogHdr.RunAudit(p_strAreaOfChange)

            Dim objAuditLogDtl As AuditLog(Of PlungerDetail) = Nothing
            Dim listPlungerDtlAuditLog As List(Of AuditLogEntry)
            For i As Integer = 0 To p_objPlunger.PlungerDetails.Count - 1 Step 1

                If p_objPlunger.OriginalPlunger Is Nothing Then Exit For
                If i = p_objPlunger.OriginalPlunger.PlungerDetails.Count Then Exit For

                Dim detail As PlungerDetail = p_objPlunger.PlungerDetails(i)
                Dim detailOrig As PlungerDetail = p_objPlunger.OriginalPlunger.PlungerDetails(i)

                objAuditLogDtl = New AuditLog(Of PlungerDetail)(detailOrig, detail)
                listPlungerDtlAuditLog = objAuditLogDtl.RunAudit(p_strAreaOfChange)

                For j As Integer = 0 To listPlungerDtlAuditLog.Count() - 1
                    listPlungerAuditLog.Add(listPlungerDtlAuditLog.Item(j))
                Next
            Next

            Return listPlungerAuditLog
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Run audit of HDR / DTL tables.
    ''' </summary>
    ''' <returns>List of Audit Log entry</returns> 
    ''' <param name="p_strAreaOfChange">Area of Change.</param>
    ''' <param name="p_objBeadUnSeat">Bead Unseat.</param>    
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
    Private Function RunAuditHdrDtl(ByVal p_strAreaOfChange As String, ByVal p_objBeadUnSeat As BeadUnSeat) As List(Of AuditLogEntry)
        Try
            Debug.WriteLine("RunAuditHdrDtl - BeadUnSeat")
            Dim objAuditLogHdr As New AuditLog(Of BeadUnSeat)(p_objBeadUnSeat.OriginalBeadUnSeat, p_objBeadUnSeat)
            Dim listBeadUnseatAuditLog As List(Of AuditLogEntry) = objAuditLogHdr.RunAudit(p_strAreaOfChange)

            Dim objAuditLogDtl As AuditLog(Of BeadUnSeatDetail) = Nothing
            Dim listBeadUnseatDtlAuditLog As List(Of AuditLogEntry)
            For i As Integer = 0 To p_objBeadUnSeat.BeadUnSeatDetails.Count - 1 Step 1

                If p_objBeadUnSeat.OriginalBeadUnSeat Is Nothing Then Exit For
                If i = p_objBeadUnSeat.OriginalBeadUnSeat.BeadUnSeatDetails.Count Then Exit For

                Dim detail As BeadUnSeatDetail = p_objBeadUnSeat.BeadUnSeatDetails(i)
                Dim detailOrig As BeadUnSeatDetail = p_objBeadUnSeat.OriginalBeadUnSeat.BeadUnSeatDetails(i)

                objAuditLogDtl = New AuditLog(Of BeadUnSeatDetail)(detailOrig, detail)
                listBeadUnseatDtlAuditLog = objAuditLogDtl.RunAudit(p_strAreaOfChange)

                For j As Integer = 0 To listBeadUnseatDtlAuditLog.Count() - 1
                    listBeadUnseatAuditLog.Add(listBeadUnseatDtlAuditLog.Item(j))
                Next
            Next

            Return listBeadUnseatAuditLog
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Run audit of HDR / DTL tables.
    ''' </summary>
    ''' <returns>List of Audit Log entry</returns> 
    ''' <param name="p_strAreaOfChange">Area of change.</param>
    ''' <param name="p_objTreadwear">TreadWear.</param>    
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
    Private Function RunAuditHdrDtl(ByVal p_strAreaOfChange As String, ByVal p_objTreadwear As Treadwear) As List(Of AuditLogEntry)
        Try
            Debug.WriteLine("RunAuditHdrDtl - Treadwear")
            Dim objAuditLogHdr As New AuditLog(Of Treadwear)(p_objTreadwear.OriginalTreadwear, p_objTreadwear)
            Dim listTreadwearAuditLog As List(Of AuditLogEntry) = objAuditLogHdr.RunAudit(p_strAreaOfChange)

            Dim objAuditLogDtl As AuditLog(Of TreadwearDetail) = Nothing
            Dim listTreadwearDtlAuditLog As List(Of AuditLogEntry)
            For i As Integer = 0 To p_objTreadwear.TreadwearDetails.Count - 1 Step 1

                If p_objTreadwear.OriginalTreadwear Is Nothing Then Exit For
                If i = p_objTreadwear.OriginalTreadwear.TreadwearDetails.Count Then Exit For

                Dim detail As TreadwearDetail = p_objTreadwear.TreadwearDetails(i)
                Dim detailOrig As TreadwearDetail = p_objTreadwear.OriginalTreadwear.TreadwearDetails(i)

                objAuditLogDtl = New AuditLog(Of TreadwearDetail)(detailOrig, detail)
                listTreadwearDtlAuditLog = objAuditLogDtl.RunAudit(p_strAreaOfChange)

                For j As Integer = 0 To listTreadwearDtlAuditLog.Count() - 1
                    listTreadwearAuditLog.Add(listTreadwearDtlAuditLog.Item(j))
                Next
            Next

            Return listTreadwearAuditLog
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Run audit of HDR / DTL tables.
    ''' </summary>
    ''' <returns>List of Audit Log entry</returns>     
    ''' <param name="p_objEndurance">Endurance</param>
    ''' <param name="p_strAreaOfChange">Area of Change</param>
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
    Private Function RunAuditHdrDtl(ByVal p_strAreaOfChange As String, ByVal p_objEndurance As Endurance) As List(Of AuditLogEntry)
        Try
            Debug.WriteLine("RunAuditHdrDtl - Endurance")
            Dim objAuditLogHdr As New AuditLog(Of Endurance)(p_objEndurance.OriginalEndurance, p_objEndurance)
            Dim listEnduranceAuditLog As List(Of AuditLogEntry) = objAuditLogHdr.RunAudit(p_strAreaOfChange)

            Dim objAuditLogDtl As AuditLog(Of EnduranceDetail) = Nothing
            Dim listEnduranceDtlAuditLog As List(Of AuditLogEntry)
            For i As Integer = 0 To p_objEndurance.EnduranceDetails.Count - 1 Step 1

                If p_objEndurance.OriginalEndurance Is Nothing Then Exit For
                If i = p_objEndurance.OriginalEndurance.EnduranceDetails.Count Then Exit For

                Dim detail As EnduranceDetail = p_objEndurance.EnduranceDetails(i)
                Dim detailOrig As EnduranceDetail = p_objEndurance.OriginalEndurance.EnduranceDetails(i)

                objAuditLogDtl = New AuditLog(Of EnduranceDetail)(detailOrig, detail)
                listEnduranceDtlAuditLog = objAuditLogDtl.RunAudit(p_strAreaOfChange)

                For j As Integer = 0 To listEnduranceDtlAuditLog.Count() - 1
                    listEnduranceAuditLog.Add(listEnduranceDtlAuditLog.Item(j))
                Next
            Next

            Return listEnduranceAuditLog
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Convert to DateTime.
    ''' </summary>
    ''' <returns>DateTime</returns>     
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
    Private Function ConvertToDateTime(ByVal p_strDate As String) As DateTime
        Try
            Dim dteConvertedDate As DateTime = DateTime.MinValue

            If Not String.IsNullOrEmpty(p_strDate) Then
                Dim dteTemp As DateTime
                If DateTime.TryParse(p_strDate, dteTemp) Then
                    dteConvertedDate = dteTemp
                End If
            End If

            Return dteConvertedDate
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Convert to Single.
    ''' </summary>
    ''' <returns>Single</returns>     
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
    Private Function ConvertToSingle(ByVal p_strValue As String) As Single
        Try
            Dim sngConverted As Single = 0

            If Not String.IsNullOrEmpty(p_strValue) Then
                Dim sngTemp As Single
                If Single.TryParse(p_strValue, sngTemp) Then
                    sngConverted = sngTemp
                End If
            End If

            Return sngConverted
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Convert to Short.
    ''' </summary>
    ''' <returns>Short</returns>     
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
    Private Function ConvertToShort(ByVal p_strValue As String) As Short
        Try
            Dim srtConverted As Short = 0

            If Not String.IsNullOrEmpty(p_strValue) Then
                Dim srtTemp As Short
                If Short.TryParse(p_strValue, srtTemp) Then
                    srtConverted = srtTemp
                End If
            End If

            Return srtConverted
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Convert to Integer
    ''' </summary>
    ''' <returns>Integer</returns>     
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
    Private Function ConvertToInteger(ByVal p_strValue As String) As Integer
        Try
            Dim intConverted As Integer = 0

            If Not String.IsNullOrEmpty(p_strValue) Then
                Dim intTemp As Integer
                If Integer.TryParse(p_strValue, intTemp) Then
                    intConverted = intTemp
                End If
            End If

            Return intConverted
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Get type's of Tiers.
    ''' </summary>
    ''' <returns>Datatable</returns>     
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
    Public Function GetTireType() As DataTable
        Try
            Return Depository.Current.GetTireType()
        Catch ex As Exception
            Throw
        End Try

    End Function

#End Region

End Class
