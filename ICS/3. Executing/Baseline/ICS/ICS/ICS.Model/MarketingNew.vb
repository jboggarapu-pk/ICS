Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.DepositoryTender


Public Class MarketingNew

    ' Changed sku to material number , added methods for populating the data to view for brand , brand lines and attributes for material numbers.
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"

    ' Original status values, used for audit before save
    Private Shared sh_listOriginal As List(Of ProductCountryCertReqStatus) = Nothing

#End Region


#Region "Methods"

    'Public Function GetCertifiedProductRequests(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As List(Of List(Of String))



    '    Dim dstResult As New DataSet
    '    dstResult = Depository.Current.GetProductCertStatus(p_strBrand, p_strBrandLine)

    '    Dim dtbProductStatus As DataTable = dstResult.Tables(0)

    '    ' check if there's Material number data returned, if no data return all empty list for both certified and uncertified region list
    '    If dtbProductStatus.Rows.Count() = 0 Then

    '    Else
    '        For Each drwRow As DataRow In dtbProductStatus.Rows
    '            If Not listCertRegions.Contains(drwRow(NameAid.Column.RegionName)) Then
    '                listCertRegions.Add(drwRow(NameAid.Column.RegionName))
    '            End If
    '        Next
    '        listRegions.Add(listCertRegions)

    '        For Each drwRow As DataRow In dtbUnCertifiedCountries.Rows
    '            If Not listUnCertRegions.Contains(drwRow(NameAid.Column.RegionName)) Then
    '                listUnCertRegions.Add(drwRow(NameAid.Column.RegionName))
    '            End If
    '        Next
    '        listRegions.Add(listUnCertRegions)
    '    End If

    '    Return listRegions

    'End Function

    ''' <summary>
    ''' Get Certified and UnCertified Regions list
    ''' </summary>
    ''' <param name="p_strBrand"></param>
    ''' <param name="p_strBrandLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Function GetCertifiedAndUncertifiedRegions(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As List(Of List(Of String))

    '    Dim listRegions As List(Of List(Of String)) = New List(Of List(Of String))
    '    Dim listCertRegions As New List(Of String)
    '    Dim listUnCertRegions As New List(Of String)

    '    Dim dstResult As New DataSet
    '    dstResult = Depository.Current.GetRegionCertStatus(p_strBrand, p_strBrandLine)
    '    ' Table index: 0 - Product-Country table; 1 - Certified countries; 2 - Not Certified countries
    '    Dim dtbProductStatus As DataTable = dstResult.Tables(0)
    '    Dim dtbCertifiedCountries As DataTable = dstResult.Tables(1)
    '    Dim dtbUnCertifiedCountries As DataTable = dstResult.Tables(2)

    '    ' check if there's Material number data returned, if no data return all empty list for both certified and uncertified region list
    '    If dtbProductStatus.Rows.Count() = 0 Then
    '        listRegions.Add(listCertRegions)
    '        listRegions.Add(listUnCertRegions)
    '    Else
    '        For Each drwRow As DataRow In dtbCertifiedCountries.Rows
    '            If Not listCertRegions.Contains(drwRow(NameAid.Column.RegionName)) Then
    '                listCertRegions.Add(drwRow(NameAid.Column.RegionName))
    '            End If
    '        Next
    '        listRegions.Add(listCertRegions)

    '        For Each drwRow As DataRow In dtbUnCertifiedCountries.Rows
    '            If Not listUnCertRegions.Contains(drwRow(NameAid.Column.RegionName)) Then
    '                listUnCertRegions.Add(drwRow(NameAid.Column.RegionName))
    '            End If
    '        Next
    '        listRegions.Add(listUnCertRegions)
    '    End If

    '    Return listRegions

    'End Function

    ''' <summary>
    ''' Get the dataset of certified and uncertified countries
    ''' </summary>
    ''' <param name="p_strRegionName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Private Function GetUnCertifiedCountriesAndCertifiedCountryGroup(ByVal p_strRegionName As String) As DataSet

    '    Dim dstCountries As DataSet = New DataSet

    '    Dim listUnCertifiedCountries As New DataTable
    '    listUnCertifiedCountries.TableName = "UnCertifiedCountries"
    '    Dim dclUnCertCountryIdCloumn As New DataColumn(NameAid.Column.CountryID, System.Type.GetType("System.String"))
    '    Dim dclUnCertCountryNameCloumn As New DataColumn(NameAid.Column.CountryName, System.Type.GetType("System.String"))
    '    listUnCertifiedCountries.Columns.Add(dclUnCertCountryIdCloumn)
    '    listUnCertifiedCountries.Columns.Add(dclUnCertCountryNameCloumn)

    '    Dim listCertifiedCountryGroup As New DataTable
    '    listCertifiedCountryGroup.TableName = "CertifiedCountries"
    '    Dim dclCertIdCloumn As New DataColumn(NameAid.Column.CertificationId, System.Type.GetType("System.String"))
    '    Dim dclCertNameCloumn As New DataColumn(NameAid.Column.CertificationName, System.Type.GetType("System.String"))
    '    Dim dclCertCountryIdCloumn As New DataColumn(NameAid.Column.CountryID, System.Type.GetType("System.String"))
    '    Dim dclCertCountryNameCloumn As New DataColumn(NameAid.Column.CountryName, System.Type.GetType("System.String"))
    '    listCertifiedCountryGroup.Columns.Add(dclCertIdCloumn)
    '    listCertifiedCountryGroup.Columns.Add(dclCertNameCloumn)
    '    listCertifiedCountryGroup.Columns.Add(dclCertCountryIdCloumn)
    '    listCertifiedCountryGroup.Columns.Add(dclCertCountryNameCloumn)
    '    Dim dclKeys(1) As DataColumn
    '    dclKeys(0) = dclCertCountryIdCloumn
    '    listCertifiedCountryGroup.PrimaryKey = dclKeys

    '    Dim dtbRegCountries As New DataTable

    '    dtbRegCountries = Depository.Current.GetCountries(p_strRegionName)

    '    For Each drwRow As DataRow In dtbRegCountries.Rows
    '        If drwRow(NameAid.Column.CertificationId) Is DBNull.Value Then
    '            listUnCertifiedCountries.Rows.Add(drwRow(NameAid.Column.CountryID), drwRow(NameAid.Column.CountryName))
    '        Else
    '            listCertifiedCountryGroup.Rows.Add(drwRow(NameAid.Column.CertificationId), drwRow(NameAid.Column.CertificationName), drwRow(NameAid.Column.CountryID), drwRow(NameAid.Column.CountryName))
    '        End If
    '    Next

    '    dstCountries.Tables.Add(listCertifiedCountryGroup)
    '    dstCountries.Tables.Add(listUnCertifiedCountries)

    '    Return dstCountries

    'End Function

  
    Private Function CreateCertRequestStatusTable(ByVal dtbCertificationType As DataTable) As DataTable
        'jes(ByRef p_dstCertUnCertCountries As DataSet) As DataTable

        'Dim dtbUnCertCountries As DataTable = New DataTable()
        'Dim dtbCertCountriesWithCertification As DataTable = New DataTable()

        'dtbCertCountriesWithCertification = p_dstCertUnCertCountries.Tables("CertifiedCountries")
        'dtbUnCertCountries = p_dstCertUnCertCountries.Tables("UnCertifiedCountries")

        ' Create table of product status in region's countries
        Dim dtbCertReqStatus As New DataTable

        ''        Dim dclSKUID As New DataColumn("SKUID", System.Type.GetType("System.Int32"))
        Dim dclSKUID As New DataColumn("SKUID", GetType(Integer))
        dclSKUID.Caption = "SKUID"
        dtbCertReqStatus.Columns.Add(dclSKUID)

        Dim dclMatlNum As New DataColumn("MATL_NUM", GetType(String))
        dclMatlNum.Caption = "MATL_NUM"
        dtbCertReqStatus.Columns.Add(dclMatlNum)

        Dim dclSize As New DataColumn("Size", GetType(String))
        dclSize.Caption = "Size"
        dtbCertReqStatus.Columns.Add(dclSize)

        Dim dclSingLoadIndex As New DataColumn("SingLoadIndex", GetType(String))
        dclSingLoadIndex.Caption = "SingLoadIndex"
        dtbCertReqStatus.Columns.Add(dclSingLoadIndex)

        Dim dclDualLoadIndex As New DataColumn("DualLoadIndex", GetType(String))
        dclDualLoadIndex.Caption = "DualLoadIndex"
        dtbCertReqStatus.Columns.Add(dclDualLoadIndex)

        Dim dclSpeedRating As New DataColumn("SpeedRating", GetType(String))
        dclSpeedRating.Caption = "SpeedRating"
        dtbCertReqStatus.Columns.Add(dclSpeedRating)

        Dim dclKeys(1) As DataColumn
        dclKeys(0) = dclMatlNum
        dtbCertReqStatus.PrimaryKey = dclKeys

        ' Dealing with Certified countries and make it grouping in region
        Dim RemoveIndexList As List(Of Integer) = New List(Of Integer)
        For Each drwRow As DataRow In dtbCertificationType.Rows
            'If Not dtbCertReqStatus.Columns.Contains(drwRow(NameAid.Column.CertificationName)) Then
            '    If CountTheCountryNumberPerCertification(dtbCertCountriesWithCertification, drwRow(NameAid.Column.CertificationName)) = 1 Then
            Dim dcCertColumn As New DataColumn(drwRow(NameAid.Column.CertificationName), GetType(String))
            dcCertColumn.Caption = drwRow(NameAid.Column.CertificationName)
            dtbCertReqStatus.Columns.Add(dcCertColumn)
            '' Add to remove list to clear the rows which is shown as country not certification
            'RemoveIndexList.Add(dtbCertCountriesWithCertification.Rows.IndexOf(drwRow))
            '    Else
            'Dim dcCertificationCloumn As New DataColumn(drwRow(NameAid.Column.CertificationName), GetType(String))
            'dcCertificationCloumn.Caption = drwRow(NameAid.Column.CertificationName)
            'dtbCertReqStatus.Columns.Add(dcCertificationCloumn)
            '    End If
            'End If
        Next

        '' remove the certified country from the certified countries table since it's only have one country per certification
        'Dim intListIndex As Integer = RemoveIndexList.Count - 1
        'Do While (intListIndex >= 0)
        '    dtbCertCountriesWithCertification.Rows(RemoveIndexList(intListIndex)).Delete()
        '    intListIndex = (intListIndex - 1)
        'Loop

        '' Dealing with UnCertification status per country in region
        'For Each drwRow As DataRow In dtbUnCertCountries.Rows
        '    Dim dcCountryCloumn As New DataColumn(drwRow(NameAid.Column.CountryID), GetType(String))
        '    dcCountryCloumn.Caption = drwRow(NameAid.Column.CountryName)
        '    dtbCertReqStatus.Columns.Add(dcCountryCloumn)
        'Next

        Dim dclToolTip As New DataColumn("ToolTip", GetType(String))
        dclToolTip.Caption = "ToolTip"
        dtbCertReqStatus.Columns.Add(dclToolTip)


        Return dtbCertReqStatus

    End Function

    ''' <summary>
    ''' Check the counts of countries per certification
    ''' </summary>
    ''' <param name="p_dtbCertCountriesWithCertification"></param>
    ''' <param name="p_CertificationName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Private Function CountTheCountryNumberPerCertification(ByVal p_dtbCertCountriesWithCertification As DataTable, ByVal p_CertificationName As String) As Integer

    '    Dim intCountriesCount As Integer
    '    For Each drwRow As DataRow In p_dtbCertCountriesWithCertification.Rows
    '        If drwRow(NameAid.Column.CertificationName) = p_CertificationName Then
    '            intCountriesCount = intCountriesCount + 1
    '        End If
    '    Next

    '    Return intCountriesCount

    'End Function

    ''' <summary>
    ''' Gets the product request status table.
    ''' </summary>
    ''' <param name="p_strBrand"> brand.</param>
    ''' <param name="p_strBrandLine"> brand line.</param>
    ''' <returns></returns>
    Public Function GetProductRequestStatusTable(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet

        'return dataset contains product country status table and certification country group table
        Dim dstProductRequestStatusAndCertificationGroup As DataSet = New DataSet()


        'Dim dtbCertReqStatus As DataTable = New DataTable()

        ' Create table of certificate types
        Dim dtbCertificateTypes As DataTable = Depository.Current.GetCertifications.Tables(0)
        dtbCertificateTypes.TableName = "CertificateTypes"

        Dim dtbCertReqStatus As DataTable = CreateCertRequestStatusTable(dtbCertificateTypes)
        dtbCertReqStatus.TableName = "ProductRequestStatus"
        '**dtbCertCountriesWithCertification = dstCountries.Tables("CertifiedCountries").Copy()

        Dim dtbProductCert As New DataTable
        dtbProductCert = Depository.Current.GetProductCertStatus(p_strBrand, p_strBrandLine).Tables(0)

        ' Create Product-Cert-Status Table 
        For Each drwProdCert As DataRow In dtbProductCert.Rows
            'if we haven't created this row yet, create it.
            Dim drwStatus As DataRow = dtbCertReqStatus.Rows.Find(drwProdCert(NameAid.Column.MaterialNumber))
            'Dim drwStatus As DataRow = dtbProdRegCountryStatus.Rows.Find(drwProdCountry(NameAid.Column.MaterialNumber))

            'If dtbCertReqStatus.Rows.Count = 0 Then
            '    drwStatus = Nothing
            'Else
            '    'Dim drwStatus As DataRow = dtbCertReqStatus.Rows.Find(drwProdCert(NameAid.Column.MaterialNumber
            '    drwStatus = dtbCertReqStatus.Rows.Find(drwProdCert(NameAid.Column.MaterialNumber))
            'End If
            If (drwStatus Is Nothing) Then
                drwStatus = dtbCertReqStatus.NewRow()
                drwStatus.Item(0) = drwProdCert(NameAid.Column.SKUID)
                drwStatus.Item(1) = drwProdCert(NameAid.Column.MaterialNumber)
                drwStatus.Item(2) = drwProdCert(NameAid.Column.SizeStamp)
                drwStatus.Item(3) = drwProdCert(NameAid.Column.SingleLoadIndex)
                drwStatus.Item(4) = drwProdCert(NameAid.Column.DualLoadIndex)
                drwStatus.Item(5) = drwProdCert(NameAid.Column.SpeedRating)


                ' add columns for certification types - unchecked
                Dim i As Integer = 6
                'Reduced count by 1 to exclude ToolTip column
                Do While (i < dtbCertReqStatus.Columns.Count - 1)
                    drwStatus.Item(i) = NameAid.MarketingStatus.Uncertified
                    i += 1
                Loop
                ' Add product row with country status
                dtbCertReqStatus.Rows.Add(drwStatus)
            End If

            ' Flag  status certificate type 
            If Not drwProdCert(NameAid.Column.CertificationName).Equals(DBNull.Value) Then
                drwStatus(drwProdCert(NameAid.Column.CertificationName)) = drwProdCert(NameAid.Column.State)
            End If


        Next

        dstProductRequestStatusAndCertificationGroup.Tables.Add(dtbCertReqStatus)
        'dstProductRequestStatusAndCertificationGroup.Tables.Add(dtbCertificateTypes)

        sh_listOriginal = MakeListFromTable(dtbCertReqStatus)

        Return dstProductRequestStatusAndCertificationGroup
    End Function

    ''' <summary>
    ''' Make request status list from table
    ''' </summary>
    ''' <param name="p_dtbProductRequestStatus"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MakeListFromTable(ByVal dtbProductRequestStatus As DataTable) As List(Of ProductCountryCertReqStatus)

        Dim listOriginal As New List(Of ProductCountryCertReqStatus)

        Dim intSKUID As Integer = 0
        Dim strMatlNum As String = String.Empty
        Dim strSizeStamp As String = String.Empty
        Dim intCountryID As Integer = 0
        Dim strCountryName As String = String.Empty
        Dim blnReqStatus As Boolean = False
        Dim strCertificationName As String = String.Empty
        Dim strToolTip As String = String.Empty

        For Each drw As DataRow In dtbProductRequestStatus.Rows

            intSKUID = drw.Item(NameAid.Column.SKUID)
            strMatlNum = drw.Item(NameAid.Column.MaterialNumber)
            'Debug.Print(intSKUID & " " & strMatlNum)
            strSizeStamp = drw.Item(NameAid.Column.Size)
            'Debug.Print("size=" & strSizeStamp)
            If Not drw.Item("ToolTip") Is DBNull.Value Then
                strToolTip = drw.Item("ToolTip")
            End If


            For Each dcl As DataColumn In dtbProductRequestStatus.Columns
                If dcl.ColumnName = NameAid.Column.SKUID OrElse dcl.ColumnName = NameAid.Column.MaterialNumber _
                OrElse dcl.ColumnName = NameAid.Column.Size OrElse dcl.ColumnName = "ToolTip" _
                OrElse dcl.ColumnName.ToUpper = NameAid.Column.SingleLoadIndex _
                OrElse dcl.ColumnName.ToUpper = NameAid.Column.DualLoadIndex _
                OrElse dcl.ColumnName.ToUpper = NameAid.Column.SpeedRating Then Continue For

                intCountryID = 0
                strCountryName = String.Empty
                strCertificationName = String.Empty
                'If Integer.TryParse(dcl.ColumnName, intCountryID) Then
                'strCountryName = dcl.Caption
                'Else
                strCertificationName = dcl.ColumnName
                'End If

                blnReqStatus = drw.Item(dcl) <> NameAid.MarketingStatus.Uncertified

                listOriginal.Add(New ProductCountryCertReqStatus(intSKUID, strMatlNum, strSizeStamp, intCountryID, strCountryName, strCertificationName, blnReqStatus))
                ''Debug.WriteLine(intSKUID.ToString() & " " & intCountryID.ToString() & " " & strCertificationName & " " & blnReqStatus.ToString())
            Next

        Next

        Return listOriginal

    End Function

    ''' <summary>
    ''' Run audit against original status list and save results
    ''' </summary>
    ''' <param name="p_dtbProductRequestStatus"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RunAuditAndSaveResults(ByVal p_dtbProductRequestStatus As DataTable) As Boolean

        'this is using the ProductCountryCertReqStatus class set up from the original Marketing screen. However, country won't be used.

        Dim blnDone As Boolean = False
        Dim objAuditLog As AuditLog(Of ProductCountryCertReqStatus) = Nothing

        Dim listSave As List(Of ProductCountryCertReqStatus) = MakeListFromTable(p_dtbProductRequestStatus)

        If sh_listOriginal.Count <> listSave.Count Then
            Throw New Exception("ProdRegCountryStatus original and save counts do not match.")
        End If

        For i As Integer = 0 To listSave.Count - 1

            ' Compose area-of-change:
            Dim strAreaOfChange As String = DomainEntities.AuditLogEntry.AreaOfChange.Marketing.ToString() _
                                            & "-" & listSave(i).MaterialNumber & ","
            ' If listSave(i).CountryID <> 0 Then
            'strAreaOfChange &= listSave(i).CountryName
            ' Else
            strAreaOfChange &= listSave(i).CertTypeName
            ' End If

            objAuditLog = New AuditLog(Of ProductCountryCertReqStatus)(sh_listOriginal(i), listSave(i))
            blnDone = objAuditLog.RunAuditAndSaveResultsMarketing(strAreaOfChange)

            If Not blnDone Then Exit For
        Next

        Return blnDone

    End Function

    ''' <summary>
    ''' Save the change for productrequest table
    ''' </summary>
    ''' <param name="dtbProdRequestStatus"></param>
    ''' <remarks></remarks>
    Public Function SaveCertificationTable(ByVal dtbProdRequestStatus As DataTable, ByVal dtbCertificationType As DataTable) As Boolean

        Dim blnDone As Boolean = False

        blnDone = RunAuditAndSaveResults(dtbProdRequestStatus)
        If Not blnDone Then
            Return blnDone
        End If

        ' Loop the updated datatable to update each cells
        For Each drwProdRequest As DataRow In dtbProdRequestStatus.Rows
            For Each dclCert As DataColumn In drwProdRequest.Table.Columns

                If dclCert.ColumnName <> "SKUID" And dclCert.ColumnName <> "MATL_NUM" And dclCert.ColumnName <> "Size" And dclCert.ColumnName <> "ToolTip" _
                And dclCert.ColumnName <> "SingLoadIndex" And dclCert.ColumnName <> "DualLoadIndex" And dclCert.ColumnName <> "SpeedRating" Then

                    Dim nCertificationTypeID As Integer = (Depository.Current.GetCertificationTypeID(dclCert.ColumnName)) ' dtbCertificationType.Rows.Find(dclCert.ColumnName) '

                    blnDone = Depository.Current.SaveRequestCert(Not (drwProdRequest(dclCert) <> NameAid.MarketingStatus.Uncertified), drwProdRequest(NameAid.Column.MaterialNumber), nCertificationTypeID, drwProdRequest(NameAid.Column.SKUID))


                    If Not blnDone Then Exit For

                End If

            Next
        Next

        Return blnDone

    End Function

#End Region

End Class
