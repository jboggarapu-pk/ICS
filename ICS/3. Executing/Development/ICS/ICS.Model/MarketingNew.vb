Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.DepositoryTender

''' <summary>
'''  Class Contains Marketing New methods and properties.
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
Public Class MarketingNew

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
    '''  Method to create Certificate request status.
    ''' </summary>
    ''' <returns>Datatable</returns> 
    '''  <param name="dtbCertificationType">Certification Type.</param>
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
    Private Function CreateCertRequestStatusTable(ByVal dtbCertificationType As DataTable) As DataTable
        Try
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
                Dim dcCertColumn As New DataColumn(CStr(drwRow(NameAid.Column.CertificationName)), GetType(String))
                dcCertColumn.Caption = CStr(drwRow(NameAid.Column.CertificationName))
                dtbCertReqStatus.Columns.Add(dcCertColumn)

            Next
            Dim dclToolTip As New DataColumn("ToolTip", GetType(String))
            dclToolTip.Caption = "ToolTip"
            dtbCertReqStatus.Columns.Add(dclToolTip)

            Return dtbCertReqStatus
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Gets the product request status table.
    ''' </summary>
    ''' <returns>Dataset</returns> 
    '''<param name="p_strBrand"> brand.</param>
    ''' <param name="p_strBrandLine"> brand line.</param> 
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
    Public Function GetProductRequestStatusTable(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet
        Try
            Dim dstProductRequestStatusAndCertificationGroup As DataSet = New DataSet()

            ' Create table of certificate types
            Dim dtbCertificateTypes As DataTable = Depository.Current.GetCertifications.Tables(0)
            dtbCertificateTypes.TableName = "CertificateTypes"

            Dim dtbCertReqStatus As DataTable = CreateCertRequestStatusTable(dtbCertificateTypes)
            dtbCertReqStatus.TableName = "ProductRequestStatus"

            Dim dtbProductCert As New DataTable
            dtbProductCert = Depository.Current.GetProductCertStatus(p_strBrand, p_strBrandLine).Tables(0)

            ' Create Product-Cert-Status Table 
            For Each drwProdCert As DataRow In dtbProductCert.Rows
                'if we haven't created this row yet, create it.
                Dim drwStatus As DataRow = dtbCertReqStatus.Rows.Find(drwProdCert(NameAid.Column.MaterialNumber))

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
                    drwStatus(drwProdCert(NameAid.Column.CertificationName).ToString) = drwProdCert(NameAid.Column.State)
                End If


            Next

            dstProductRequestStatusAndCertificationGroup.Tables.Add(dtbCertReqStatus)

            sh_listOriginal = MakeListFromTable(dtbCertReqStatus)

            Return dstProductRequestStatusAndCertificationGroup
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Make request status list from table.
    ''' </summary>
    ''' <returns>List of Product Country CertReqStatus</returns> 
    ''' <param name="dtbProductRequestStatus">Product Request Status</param>
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
    Private Function MakeListFromTable(ByVal dtbProductRequestStatus As DataTable) As List(Of ProductCountryCertReqStatus)
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

            For Each drw As DataRow In dtbProductRequestStatus.Rows

                intSKUID = CInt(drw.Item(NameAid.Column.SKUID))
                strMatlNum = CStr(drw.Item(NameAid.Column.MaterialNumber))
                strSizeStamp = CStr(drw.Item(NameAid.Column.Size))
                If Not drw.Item("ToolTip") Is DBNull.Value Then
                    strToolTip = CStr(drw.Item("ToolTip"))
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
                    strCertificationName = dcl.ColumnName

                    blnReqStatus = drw.Item(dcl) Is NameAid.MarketingStatus.Uncertified

                    listOriginal.Add(New ProductCountryCertReqStatus(intSKUID, strMatlNum, strSizeStamp, intCountryID, strCountryName, strCertificationName, blnReqStatus))

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
    ''' <param name="p_dtbProductRequestStatus">Product Request Status</param>    
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
    Private Function RunAuditAndSaveResults(ByVal p_dtbProductRequestStatus As DataTable) As Boolean
        Try
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

                strAreaOfChange &= listSave(i).CertTypeName

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
    '''  Method to Save the change for productrequest table.
    ''' </summary>
    ''' <returns>Boolean</returns> 
    '''  <param name="dtbCertificationType">Certification Type.</param>
    ''' <param name="dtbProdRequestStatus">Product request Status</param>
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
    ''' <para>10/04a/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function SaveCertificationTable(ByVal dtbProdRequestStatus As DataTable, ByVal dtbCertificationType As DataTable) As Boolean
        Try
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

                        blnDone = Depository.Current.SaveRequestCert(Not (drwProdRequest(dclCert).ToString() <> NameAid.MarketingStatus.Uncertified), CStr(drwProdRequest(NameAid.Column.MaterialNumber)), nCertificationTypeID, CInt(drwProdRequest(NameAid.Column.SKUID)))
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
