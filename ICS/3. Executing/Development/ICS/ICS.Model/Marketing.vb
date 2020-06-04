Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.DepositoryTender

''' <summary>
'''  Class containing Marketing related methods.
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
''' <para>09/26/2019</para>
''' <para>Implemented Coding Standards</para>
''' </description>
''' </item> 
''' </list>
''' </remarks> 
Public Class Marketing

    ' Changed sku to material number , added methods for populating the data to view for brand , brand lines and attributes for material numbers.
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"

    ' Original status values, used for audit before save
    ''' <summary>
    ''' Variable to hold List of Original.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared sh_listOriginal As List(Of ProductCountryCertReqStatus) = Nothing

#End Region


#Region "Methods"

    ''' <summary>
    '''  Method to Get Certified and UnCertified Regions list
    ''' </summary>
    ''' <returns>List of Regions</returns> 
    ''' <param name="p_strBrand">Brand</param>
    ''' <param name="p_strBrandLine">BrandLine</param>    
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
    Public Function GetCertifiedAndUncertifiedRegions(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As List(Of List(Of String))
        Try
            Dim listRegions As List(Of List(Of String)) = New List(Of List(Of String))
            Dim listCertRegions As New List(Of String)
            Dim listUnCertRegions As New List(Of String)

            Dim dstResult As New DataSet
            dstResult = Depository.Current.GetRegionCertStatus(p_strBrand, p_strBrandLine)
            ' Table index: 0 - Product-Country table; 1 - Certified countries; 2 - Not Certified countries
            Dim dtbProductStatus As DataTable = dstResult.Tables(0)
            Dim dtbCertifiedCountries As DataTable = dstResult.Tables(1)
            Dim dtbUnCertifiedCountries As DataTable = dstResult.Tables(2)

            ' check if there's Material number data returned, if no data return all empty list for both certified and uncertified region list
            If dtbProductStatus.Rows.Count() = 0 Then
                listRegions.Add(listCertRegions)
                listRegions.Add(listUnCertRegions)
            Else
                For Each drwRow As DataRow In dtbCertifiedCountries.Rows
                    If Not listCertRegions.Contains(CStr(drwRow(NameAid.Column.RegionName))) Then
                        listCertRegions.Add(CStr(drwRow(NameAid.Column.RegionName)))
                    End If
                Next
                listRegions.Add(listCertRegions)

                For Each drwRow As DataRow In dtbUnCertifiedCountries.Rows
                    If Not listUnCertRegions.Contains(CStr(drwRow(NameAid.Column.RegionName))) Then
                        listUnCertRegions.Add(CStr(drwRow(NameAid.Column.RegionName)))
                    End If
                Next
                listRegions.Add(listUnCertRegions)
            End If

            Return listRegions
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get the dataset of certified and uncertified countries
    ''' </summary>
    ''' <returns>Dataset</returns> 
    ''' <param name="p_strRegionName">Region Name</param>  
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Private Function GetUnCertifiedCountriesAndCertifiedCountryGroup(ByVal p_strRegionName As String) As DataSet
        Try
            Dim dstCountries As DataSet = New DataSet

            Dim listUnCertifiedCountries As New DataTable
            listUnCertifiedCountries.TableName = "UnCertifiedCountries"
            Dim dclUnCertCountryIdCloumn As New DataColumn(NameAid.Column.CountryID, System.Type.GetType("System.String"))
            Dim dclUnCertCountryNameCloumn As New DataColumn(NameAid.Column.CountryName, System.Type.GetType("System.String"))
            listUnCertifiedCountries.Columns.Add(dclUnCertCountryIdCloumn)
            listUnCertifiedCountries.Columns.Add(dclUnCertCountryNameCloumn)

            Dim listCertifiedCountryGroup As New DataTable
            listCertifiedCountryGroup.TableName = "CertifiedCountries"
            Dim dclCertIdCloumn As New DataColumn(NameAid.Column.CertificationId, System.Type.GetType("System.String"))
            Dim dclCertNameCloumn As New DataColumn(NameAid.Column.CertificationName, System.Type.GetType("System.String"))
            Dim dclCertCountryIdCloumn As New DataColumn(NameAid.Column.CountryID, System.Type.GetType("System.String"))
            Dim dclCertCountryNameCloumn As New DataColumn(NameAid.Column.CountryName, System.Type.GetType("System.String"))
            listCertifiedCountryGroup.Columns.Add(dclCertIdCloumn)
            listCertifiedCountryGroup.Columns.Add(dclCertNameCloumn)
            listCertifiedCountryGroup.Columns.Add(dclCertCountryIdCloumn)
            listCertifiedCountryGroup.Columns.Add(dclCertCountryNameCloumn)
            Dim dclKeys(1) As DataColumn
            dclKeys(0) = dclCertCountryIdCloumn
            listCertifiedCountryGroup.PrimaryKey = dclKeys

            Dim dtbRegCountries As New DataTable

            dtbRegCountries = Depository.Current.GetCountries(p_strRegionName)

            For Each drwRow As DataRow In dtbRegCountries.Rows
                If drwRow(NameAid.Column.CertificationId) Is DBNull.Value Then
                    listUnCertifiedCountries.Rows.Add(drwRow(NameAid.Column.CountryID), drwRow(NameAid.Column.CountryName))
                Else
                    listCertifiedCountryGroup.Rows.Add(drwRow(NameAid.Column.CertificationId), drwRow(NameAid.Column.CertificationName), drwRow(NameAid.Column.CountryID), drwRow(NameAid.Column.CountryName))
                End If
            Next

            dstCountries.Tables.Add(listCertifiedCountryGroup)
            dstCountries.Tables.Add(listUnCertifiedCountries)

            Return dstCountries
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Create the product country status table.
    ''' </summary>
    ''' <returns>Dataset</returns> 
    ''' <param name="p_dstCertUnCertCountries">Cert UnCert Countries</param>  
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function CreateProdCountryStatusTable(ByRef p_dstCertUnCertCountries As DataSet) As DataTable
        Try
            Dim dtbUnCertCountries As DataTable = New DataTable()
            Dim dtbCertCountriesWithCertification As DataTable = New DataTable()

            dtbCertCountriesWithCertification = p_dstCertUnCertCountries.Tables("CertifiedCountries")
            dtbUnCertCountries = p_dstCertUnCertCountries.Tables("UnCertifiedCountries")

            ' Create table of product status in region's countries
            Dim dtbProdRegCountryStatus As New DataTable

            ''        Dim dclSKUID As New DataColumn("SKUID", System.Type.GetType("System.Int32"))
            Dim dclSKUID As New DataColumn("SKUID", GetType(Integer))
            dclSKUID.Caption = "SKUID"
            dtbProdRegCountryStatus.Columns.Add(dclSKUID)

            Dim dclMatlNum As New DataColumn("MATL_NUM", GetType(String))
            dclMatlNum.Caption = "MATL_NUM"
            dtbProdRegCountryStatus.Columns.Add(dclMatlNum)

            Dim dclSize As New DataColumn("Size", GetType(String))
            dclSize.Caption = "Size"
            dtbProdRegCountryStatus.Columns.Add(dclSize)

            Dim dclSingLoadIndex As New DataColumn("SingLoadIndex", GetType(String))
            dclSingLoadIndex.Caption = "SingLoadIndex"
            dtbProdRegCountryStatus.Columns.Add(dclSingLoadIndex)

            Dim dclDualLoadIndex As New DataColumn("DualLoadIndex", GetType(String))
            dclDualLoadIndex.Caption = "DualLoadIndex"
            dtbProdRegCountryStatus.Columns.Add(dclDualLoadIndex)

            Dim dclSpeedRating As New DataColumn("SpeedRating", GetType(String))
            dclSpeedRating.Caption = "SpeedRating"
            dtbProdRegCountryStatus.Columns.Add(dclSpeedRating)

            Dim dclKeys(1) As DataColumn
            dclKeys(0) = dclMatlNum
            dtbProdRegCountryStatus.PrimaryKey = dclKeys

            ' Dealing with Certified countries and make it grouping in region
            Dim RemoveIndexList As List(Of Integer) = New List(Of Integer)
            For Each drwRow As DataRow In dtbCertCountriesWithCertification.Rows
                If Not dtbProdRegCountryStatus.Columns.Contains(CStr(drwRow(NameAid.Column.CertificationName))) Then
                    If CountTheCountryNumberPerCertification(dtbCertCountriesWithCertification, CStr(drwRow(NameAid.Column.CertificationName))) = 1 Then
                        Dim dcCountryCloumn As New DataColumn(CStr(drwRow(NameAid.Column.CountryID)), GetType(String))
                        dcCountryCloumn.Caption = CStr(drwRow(NameAid.Column.CountryName))
                        dtbProdRegCountryStatus.Columns.Add(dcCountryCloumn)
                        ' Add to remove list to clear the rows which is shown as country not certification
                        RemoveIndexList.Add(dtbCertCountriesWithCertification.Rows.IndexOf(drwRow))
                    Else
                        Dim dcCertificationCloumn As New DataColumn(CStr(drwRow(NameAid.Column.CertificationName)), GetType(String))
                        dcCertificationCloumn.Caption = CStr(drwRow(NameAid.Column.CertificationName))
                        dtbProdRegCountryStatus.Columns.Add(dcCertificationCloumn)
                    End If
                End If
            Next

            ' remove the certified country from the certified countries table since it's only have one country per certification
            Dim intListIndex As Integer = RemoveIndexList.Count - 1
            Do While (intListIndex >= 0)
                dtbCertCountriesWithCertification.Rows(RemoveIndexList(intListIndex)).Delete()
                intListIndex = (intListIndex - 1)
            Loop

            ' Dealing with UnCertification status per country in region
            For Each drwRow As DataRow In dtbUnCertCountries.Rows
                Dim dcCountryCloumn As New DataColumn(CStr(drwRow(NameAid.Column.CountryID)), GetType(String))
                dcCountryCloumn.Caption = CStr(drwRow(NameAid.Column.CountryName))
                dtbProdRegCountryStatus.Columns.Add(dcCountryCloumn)
            Next

            Dim dclToolTip As New DataColumn("ToolTip", GetType(String))
            dclToolTip.Caption = "ToolTip"
            dtbProdRegCountryStatus.Columns.Add(dclToolTip)


            Return dtbProdRegCountryStatus
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Check the counts of countries per certification
    ''' </summary>
    ''' <returns>Integer</returns> 
    ''' <param name="p_dtbCertCountriesWithCertification">Cert Countries with Certification</param>
    ''' <param name="p_CertificationName">Certification Name</param>
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function CountTheCountryNumberPerCertification(ByVal p_dtbCertCountriesWithCertification As DataTable, ByVal p_CertificationName As String) As Integer
        Try
            Dim intCountriesCount As Integer
            For Each drwRow As DataRow In p_dtbCertCountriesWithCertification.Rows
                If drwRow(NameAid.Column.CertificationName) Is p_CertificationName Then
                    intCountriesCount = intCountriesCount + 1
                End If
            Next

            Return intCountriesCount
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Gets the product country status table.
    ''' </summary>
    ''' <returns>Dataset</returns> 
    '''  <param name="p_strBrand"> brand.</param>
    ''' <param name="p_strBrandLine"> brand line.</param>
    ''' <param name="p_strRegionName">Name of the region.</param>    
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetProductCountryStatusTable(ByVal p_strBrand As String, ByVal p_strBrandLine As String, ByVal p_strRegionName As String) As DataSet
        Try
            'return dataset contains product country status table and certification country group table
            Dim dstProductCountryStatusAndCertificationGroup As DataSet = New DataSet()

            ' Get uncertified countries table and certified countries table with certification name associated
            Dim dtbCertCountriesWithCertification As DataTable = New DataTable()

            Dim dstCountries As DataSet = GetUnCertifiedCountriesAndCertifiedCountryGroup(p_strRegionName)

            ''TO DO pass the country table to generate grouping datatable for gridview

            ' Create table of product status in region's countries
            Dim dtbProdRegCountryStatus As DataTable = CreateProdCountryStatusTable(dstCountries)
            dtbProdRegCountryStatus.TableName = "ProductCountryStatus"

            dtbCertCountriesWithCertification = dstCountries.Tables("CertifiedCountries").Copy()

            ' Product-country table
            ' Table index: 0 - Product-Country table; 1 - Certified countries; 2 - Not Certified countries
            Dim dtbProductCountry As New DataTable

            dtbProductCountry = Depository.Current.GetRegionCertStatus(p_strBrand, p_strBrandLine).Tables(0)

            ' Create Product-Country-Status Table for a region
            For Each drwProdCountry As DataRow In dtbProductCountry.Rows

                Dim drwStatus As DataRow = dtbProdRegCountryStatus.Rows.Find(drwProdCountry(NameAid.Column.MaterialNumber))
                If (drwStatus Is Nothing) Then
                    drwStatus = dtbProdRegCountryStatus.NewRow()
                    drwStatus.Item(0) = drwProdCountry(NameAid.Column.SKUID)
                    drwStatus.Item(1) = drwProdCountry(NameAid.Column.MaterialNumber)
                    drwStatus.Item(2) = drwProdCountry(NameAid.Column.SizeStamp)
                    drwStatus.Item(3) = drwProdCountry(NameAid.Column.SingleLoadIndex)
                    drwStatus.Item(4) = drwProdCountry(NameAid.Column.DualLoadIndex)
                    drwStatus.Item(5) = drwProdCountry(NameAid.Column.SpeedRating)


                    ' Country certification defaults - unchecked
                    Dim i As Integer = 6
                    'Reduced count by 1 to exclude ToolTip column
                    Do While (i < dtbProdRegCountryStatus.Columns.Count - 1)
                        drwStatus.Item(i) = NameAid.MarketingStatus.Uncertified
                        i += 1
                    Loop
                    ' Add product row with country status
                    dtbProdRegCountryStatus.Rows.Add(drwStatus)
                End If

                ' Flag only status of a country existing in the current regional table
                If Not drwProdCountry(NameAid.Column.CountryID).Equals(DBNull.Value) Then
                    Dim strCountryID As String = drwProdCountry(NameAid.Column.CountryID).ToString
                    If drwStatus.Table.Columns.Contains(strCountryID) Then
                        drwStatus(strCountryID) = drwProdCountry(NameAid.Column.State)
                    Else
                        Dim strCertificationName As String = drwProdCountry(NameAid.Column.CertificationName).ToString
                        If dtbCertCountriesWithCertification.Rows.Contains(strCountryID) And drwStatus.Table.Columns.Contains(strCertificationName) Then
                            drwStatus(drwProdCountry(NameAid.Column.CertificationName).ToString) = drwProdCountry(NameAid.Column.State)
                        End If
                    End If
                End If

            Next

            dstProductCountryStatusAndCertificationGroup.Tables.Add(dtbProdRegCountryStatus)
            dstProductCountryStatusAndCertificationGroup.Tables.Add(dtbCertCountriesWithCertification)

            sh_listOriginal = MakeListFromTable(dtbProdRegCountryStatus)

            Return dstProductCountryStatusAndCertificationGroup
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Make request status list from table.
    ''' </summary>
    ''' <returns>Dataset</returns> 
    '''  <param name="dtbProdRegCountryStatus">Prod Region Country Status</param> 
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function MakeListFromTable(ByVal dtbProdRegCountryStatus As DataTable) As List(Of ProductCountryCertReqStatus)
        Try
            Dim listOriginal As New List(Of ProductCountryCertReqStatus)

            Dim intSKUID As Integer = 0
            Dim strMatlNum As String = String.Empty
            Dim strSizeStamp As String = String.Empty
            Dim intCountryID As Integer = 0
            Dim strCountryName As String = String.Empty
            Dim blnReqStatus As Boolean = False
            Dim strCertificationName As String = String.Empty
            Dim strToolTip As String = String.Empty

            For Each drw As DataRow In dtbProdRegCountryStatus.Rows

                intSKUID = CInt(drw.Item(NameAid.Column.SKUID))
                strMatlNum = CStr(drw.Item(NameAid.Column.MaterialNumber))
                strSizeStamp = CStr(drw.Item(NameAid.Column.Size))
                If Not drw.Item("ToolTip") Is DBNull.Value Then
                    strToolTip = CStr(drw.Item("ToolTip"))
                End If


                For Each dcl As DataColumn In dtbProdRegCountryStatus.Columns
                    If dcl.ColumnName = NameAid.Column.SKUID OrElse dcl.ColumnName = NameAid.Column.MaterialNumber _
                    OrElse dcl.ColumnName = NameAid.Column.Size OrElse dcl.ColumnName = "ToolTip" _
                    OrElse dcl.ColumnName.ToUpper = NameAid.Column.SingleLoadIndex _
                    OrElse dcl.ColumnName.ToUpper = NameAid.Column.DualLoadIndex _
                    OrElse dcl.ColumnName.ToUpper = NameAid.Column.SpeedRating Then Continue For

                    intCountryID = 0
                    strCountryName = String.Empty
                    strCertificationName = String.Empty
                    If Integer.TryParse(dcl.ColumnName, intCountryID) Then
                        strCountryName = dcl.Caption
                    Else
                        strCertificationName = dcl.ColumnName
                    End If

                    blnReqStatus = drw.Item(dcl) Is NameAid.MarketingStatus.Uncertified

                    listOriginal.Add(New ProductCountryCertReqStatus(intSKUID, strMatlNum, strSizeStamp, intCountryID, strCountryName, strCertificationName, blnReqStatus))
                    ''Debug.WriteLine(intSKUID.ToString() & " " & intCountryID.ToString() & " " & strCertificationName & " " & blnReqStatus.ToString())
                Next

            Next

            Return listOriginal
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Run audit against original status list and save results.
    ''' </summary>
    ''' <returns>Boolean</returns> 
    ''' <param name="p_dtbProdRegCountryStatus">Prod Region Country Status</param> 
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function RunAuditAndSaveResults(ByVal p_dtbProdRegCountryStatus As DataTable) As Boolean
        Try
            Dim blnDone As Boolean = False
            Dim objAuditLog As AuditLog(Of ProductCountryCertReqStatus) = Nothing

            Dim listSave As List(Of ProductCountryCertReqStatus) = MakeListFromTable(p_dtbProdRegCountryStatus)

            If sh_listOriginal.Count <> listSave.Count Then
                Throw New Exception("ProdRegCountryStatus original and save counts do not match.")
            End If

            For i As Integer = 0 To listSave.Count - 1

                ' Compose area-of-change:
                Dim strAreaOfChange As String = DomainEntities.AuditLogEntry.AreaOfChange.Marketing.ToString() _
                                                & "-" & listSave(i).MaterialNumber & ","
                If listSave(i).CountryID <> 0 Then
                    strAreaOfChange &= listSave(i).CountryName
                Else
                    strAreaOfChange &= listSave(i).CertTypeName
                End If

                objAuditLog = New AuditLog(Of ProductCountryCertReqStatus)(sh_listOriginal(i), listSave(i))
                blnDone = objAuditLog.RunAuditAndSaveResultsMarketing(strAreaOfChange)

                If Not blnDone Then Exit For
            Next

            Return blnDone
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Save the change for current product-country-status table
    ''' </summary>
    ''' <returns>Boolean</returns> 
    ''' <param name="dtbProdRegCountryStatus">Prod Region Country Status</param> 
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks> 
    Public Function SaveCertificationTable(ByVal dtbProdRegCountryStatus As DataTable, ByVal dtbCertificationGroupCountries As DataTable) As Boolean
        Try
            Dim blnDone As Boolean = False

            blnDone = RunAuditAndSaveResults(dtbProdRegCountryStatus)
            If Not blnDone Then
                Return blnDone
            End If

            ' Loop the updated datatable to update each cells
            For Each drwProdCountry As DataRow In dtbProdRegCountryStatus.Rows
                For Each dclProdCountry As DataColumn In drwProdCountry.Table.Columns

                    If dclProdCountry.ColumnName <> "SKUID" And dclProdCountry.ColumnName <> "MATL_NUM" And dclProdCountry.ColumnName <> "Size" And dclProdCountry.ColumnName <> "ToolTip" _
                    And dclProdCountry.ColumnName <> "SingLoadIndex" And dclProdCountry.ColumnName <> "DualLoadIndex" And dclProdCountry.ColumnName <> "SpeedRating" Then

                        If dclProdCountry.ColumnName <> dclProdCountry.Caption Then
                            ' uncertified country update
                            blnDone = Depository.Current.SaveCertificationRequest(Not (drwProdCountry(dclProdCountry) Is NameAid.MarketingStatus.Uncertified), CStr(drwProdCountry(NameAid.Column.MaterialNumber)), CInt(Val(dclProdCountry.ColumnName)), CInt(drwProdCountry(NameAid.Column.SKUID)))
                        Else
                            Dim CertificationId As Integer
                            For Each drwCertificationGroupRow As DataRow In dtbCertificationGroupCountries.Rows
                                If dclProdCountry.ColumnName Is drwCertificationGroupRow(NameAid.Column.CertificationName) Then
                                    CertificationId = CInt(drwCertificationGroupRow(NameAid.Column.CertificationId))
                                    Exit For
                                End If
                            Next
                            blnDone = Depository.Current.SaveCertificationGroup(Not (drwProdCountry(dclProdCountry) Is NameAid.MarketingStatus.Uncertified), CStr(drwProdCountry(NameAid.Column.MaterialNumber)), CertificationId, CInt(drwProdCountry(NameAid.Column.SKUID)))
                        End If

                        If Not blnDone Then Exit For

                    End If

                Next
            Next

            Return blnDone

        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region

End Class
