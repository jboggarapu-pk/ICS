Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Marketing view pesenter
''' </summary>
''' <remarks></remarks>
Public Class MarketingPresenter
    Inherits ViewPresenterBase

    ' Changed sku to material number , added methods for populating the data to view for brand , brand lines and attributes for material numbers.
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"

    Private m_view As IMarketingView

#End Region

#Region "Constructors / Destructors"

    Public Sub New(ByVal p_view As IMarketingView)

        MyBase.New(p_view)

        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw New Exception("Error creating " + Me.ToString())
        End Try

    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Attach presenter to view’s events.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SubscribeToEvents()

        AddHandler m_view.LoadView, AddressOf OnLoadView
        AddHandler m_view.Save, AddressOf OnSave
        AddHandler m_view.ChangeAllMaterialsStatusByCountry, AddressOf OnChangeAllSKUsStatusByCountry
        AddHandler m_view.ChangeAllCountriesStatusByMaterial, AddressOf OnChangeAllCountriesStatusBySKU
        AddHandler m_view.Search, AddressOf OnSearch
        AddHandler m_view.RegionChanged, AddressOf OnRegionChanged
        AddHandler m_view.AddRegionChanged, AddressOf OnAddRegionChanged
        AddHandler m_view.SelectChangedWithDataDirty, AddressOf OnSelectChangedWithDataDirty
        AddHandler m_view.ProductCountryStatusChanged, AddressOf OnProductCountryStatusChanged

    End Sub

    ''' <summary>
    ''' Load data for the view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)

        Try
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
            Else
                RestoreView()
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error loading form data."
        End Try

    End Sub

    ''' <summary>
    ''' Load data from business process
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewData()

        m_view.DataBindView()
        m_view.DataDirtyFlag = False
        LoadBrands(String.Empty)

    End Sub

    ''' <summary>
    ''' restore the view when there's no postback
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RestoreView()

        If Not m_view.ProductCountryStatusData Is Nothing Then
            m_view.SetupRegionView(m_view.ProductCountryStatusData)
        End If
        m_view.SuccessText = ""

    End Sub

    ''' <summary>
    ''' Update regional status
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshCurrentRegionView()

        Dim strBrand As String = m_view.Brand.Trim()
        Dim strBrandLine As String = m_view.BrandLine.Trim()
        Dim strRegionName As String = m_view.SelectedRegionName.Trim()

        Dim marketing As Marketing = New Marketing()

        m_view.ErrorText = ""

        ' there's no selected region name
        If strRegionName = "" Then
            If Not m_view.ProductCountryStatusData Is Nothing Then
                If m_view.ProductCountryStatusData.Rows.Count > 0 Then
                    m_view.ProductCountryStatusData.Clear()
                    m_view.SetupRegionView(m_view.ProductCountryStatusData)
                End If
            End If
        Else
            Dim dstProductCountryStatusAndCertificationCountry As DataSet = marketing.GetProductCountryStatusTable(strBrand, strBrandLine, strRegionName)
            If Not dstProductCountryStatusAndCertificationCountry.Tables("ProductCountryStatus").Columns.Contains("ToolTip") Then
                dstProductCountryStatusAndCertificationCountry.Tables("ProductCountryStatus").Columns.Add("ToolTip", Type.GetType("System.String"))
            End If
            For Each dr As DataRow In dstProductCountryStatusAndCertificationCountry.Tables("ProductCountryStatus").Rows
                If Not (dr("MATL_NUM") Is Nothing Or IsDBNull(dr("MATL_NUM"))) Then
                    dr("ToolTip") = GetMaterialAttribs(dr("MATL_NUM").ToString())
                End If
            Next
            dstProductCountryStatusAndCertificationCountry.AcceptChanges()

            Dim dtbProductCountryStatusTable As DataTable = dstProductCountryStatusAndCertificationCountry.Tables("ProductCountryStatus")
            Dim dtbCertificationCountryTable As DataTable = dstProductCountryStatusAndCertificationCountry.Tables("CertifiedCountries")
            m_view.ProductCountryStatusData = dtbProductCountryStatusTable
            m_view.CertificationGroupCountries = dtbCertificationCountryTable

            If m_view.ProductCountryStatusData.Rows.Count = 0 Then
                m_view.ErrorText = "no region data found."
            End If
            m_view.SetupRegionView(m_view.ProductCountryStatusData)
        End If

        m_view.DataBindView()

    End Sub

    ''' <summary>
    ''' Select/DeSelect All SKU by country
    ''' </summary>
    ''' <param name="p_strCountryId"></param>
    ''' <param name="p_blnChecked"></param>
    ''' <remarks></remarks>
    Private Sub OnChangeAllSKUsStatusByCountry(ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)

        Try
            Dim dtbProductCountryStatus As DataTable = m_view.ProductCountryStatusData
            For Each drwStatus As DataRow In dtbProductCountryStatus.Rows
                If p_blnChecked And drwStatus(p_strCountryId) = "Uncertified" Then
                    drwStatus(p_strCountryId) = "Requested"
                ElseIf Not p_blnChecked And drwStatus(p_strCountryId) = "Requested" Then
                    drwStatus(p_strCountryId) = "Uncertified"
                End If
            Next
            m_view.ProductCountryStatusData = dtbProductCountryStatus
            m_view.DataBindView()
            m_view.DataDirtyFlag = True
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error select/deselect all certificate for Country."
        End Try

    End Sub

    ''' <summary>
    ''' Select/DeSelect All country by SKU
    ''' </summary>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_blnChecked"></param>
    ''' <remarks></remarks>
    Private Sub OnChangeAllCountriesStatusBySKU(ByVal p_strMatlNum As String, ByVal p_blnChecked As Boolean)

        Try
            Dim dtbProductCountryStatus As DataTable = m_view.ProductCountryStatusData
            ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            Dim position As Integer
            position = p_strMatlNum.IndexOf(" ")
            If (position > -1) Then
                p_strMatlNum = p_strMatlNum.Substring(0, position)
            End If
            Dim drwStatus As DataRow = dtbProductCountryStatus.Rows.Find(p_strMatlNum.PadLeft(18, "0"))
            For Each drwStatusCloumn As DataColumn In drwStatus.Table.Columns
                If drwStatusCloumn.ColumnName <> "SKUID" And drwStatusCloumn.ColumnName <> "MATL_NUM" And drwStatusCloumn.ColumnName <> "Size" And _
                drwStatusCloumn.ColumnName <> "SingLoadIndex" And drwStatusCloumn.ColumnName <> "DualLoadIndex" And _
                drwStatusCloumn.ColumnName <> "SpeedRating" Then
                    If p_blnChecked And drwStatus(drwStatusCloumn) = "Uncertified" Then
                        drwStatus(drwStatusCloumn) = "Requested"
                    ElseIf Not p_blnChecked And drwStatus(drwStatusCloumn) = "Requested" Then
                        drwStatus(drwStatusCloumn) = "Uncertified"
                    End If
                End If
            Next
            m_view.ProductCountryStatusData = dtbProductCountryStatus
            m_view.DataBindView()
            m_view.DataDirtyFlag = True
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error select/deselect all certificate for Material Number " + p_strMatlNum + "."
        End Try

    End Sub

    ''' <summary>
    ''' Certified region dropdown list index changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnRegionChanged(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim strSelectRegion = CType(sender, String)
            If strSelectRegion <> "Select ..." Then
                m_view.SelectedRegionName = strSelectRegion
                RefreshCurrentRegionView()
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error loading data for selected region " + m_view.SelectedRegionName + "."
        End Try

    End Sub

    ''' <summary>
    ''' Uncertified region dropdown list index changed 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnAddRegionChanged(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim strSelectRegion = CType(sender, String)
            If strSelectRegion <> "Select ..." Then
                m_view.SelectedRegionName = strSelectRegion
                RefreshCurrentRegionView()
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error loading data for add selected region " + m_view.SelectedRegionName + "."
        End Try

    End Sub

    ''' <summary>
    ''' Store original value when dropdown list index changed with data dirty
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnSelectChangedWithDataDirty(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim strSelectRegion = CType(sender, String)
            If m_view.CertifiedRegions.Contains(m_view.SelectedRegionName) Then
                m_view.OrignalCertRegionSelectValue = m_view.SelectedRegionName
                m_view.OrignalUnCertRegionSelectValue = Nothing
            Else
                m_view.OrignalCertRegionSelectValue = Nothing
                m_view.OrignalUnCertRegionSelectValue = m_view.SelectedRegionName
            End If
            m_view.SelectedRegionName = strSelectRegion
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error saving data for original selected region."
        End Try

    End Sub

    ''' <summary>
    ''' Event handler for check box checked/unchecked per country and SKU
    ''' </summary>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strCountryId"></param>
    ''' <param name="p_blnChecked"></param>
    ''' <remarks></remarks>
    Private Sub OnProductCountryStatusChanged(ByVal p_strMatlNum As String, ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)

        Try
            Dim dtbProductCountryStatus = m_view.ProductCountryStatusData
            ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            Dim position As Integer
            position = p_strMatlNum.IndexOf(" ")
            If (position > -1) Then
                p_strMatlNum = p_strMatlNum.Substring(0, position)
            End If
            Dim drwStatus As DataRow = dtbProductCountryStatus.Rows.Find(p_strMatlNum.PadLeft(18, "0"))
            If Not (drwStatus Is Nothing) Then
                If drwStatus.Table.Columns.Contains(p_strCountryId) Then
                    If p_blnChecked Then
                        drwStatus(p_strCountryId) = "Requested"
                    Else
                        drwStatus(p_strCountryId) = "Uncertified"
                    End If
                End If
            End If
            m_view.ProductCountryStatusData = dtbProductCountryStatus
            m_view.DataBindView()
            m_view.DataDirtyFlag = True
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error Saving data."
        End Try

    End Sub

    ''' <summary>
    ''' Search the product country status based on brand 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnSearch(ByVal sender As Object, ByVal e As EventArgs)

        Try
            ' chekc if this is add region situation
            Dim bnlAddRegionFlag As Boolean = False
           
            If Not m_view.CertifiedRegions Is Nothing Then
                If m_view.CertifiedRegions.Count > 1 And Not m_view.CertifiedRegions.Contains(m_view.SelectedRegionName) Then
                    bnlAddRegionFlag = True
                End If
            End If

            Dim strBrand As String = m_view.Brand.Trim()
            Dim strBrandLine As String = m_view.BrandLine.Trim()

            If (strBrand = "Select ..." Or strBrandLine = "Select ..." Or strBrandLine = String.Empty) Then
                'clear the existing data when empty input of brand 
                m_view.SelectedRegionName = ""
                m_view.ErrorText = "Please select a brand and brand line for search."
                If m_view.CertifiedRegions IsNot Nothing Then
                    m_view.CertifiedRegions.Clear()
                End If
                If m_view.UncertifiedRegions IsNot Nothing Then
                    m_view.UncertifiedRegions.Clear()
                End If
                If m_view.ProductCountryStatusData IsNot Nothing Then
                    m_view.ProductCountryStatusData.Clear()
                End If
                'initial the region lists
                Dim listCertifiedRegions As List(Of String) = New List(Of String)
                listCertifiedRegions.Add("Select ...")
                m_view.CertifiedRegions = listCertifiedRegions
                Dim listUnCertifiedRegions As List(Of String) = New List(Of String)
                listUnCertifiedRegions.Add("Select ...")
                m_view.UncertifiedRegions = listUnCertifiedRegions
                m_view.SetupRegionView(m_view.ProductCountryStatusData)
                m_view.DataBindView()
                Return
            End If

            Dim marketing As Marketing = New Marketing()

            ' list contains certified region list and uncertified region list
            Dim listCertifiedUncertifiedRegions As List(Of List(Of String)) = marketing.GetCertifiedAndUncertifiedRegions(strBrand, strBrandLine)
            Dim listCertifiedRegion As List(Of String) = listCertifiedUncertifiedRegions(0)
            Dim listUnCertifiedRegion As List(Of String) = listCertifiedUncertifiedRegions(1)
            ' empty dataset get from model
            If listCertifiedRegion.Count() = 0 And listUnCertifiedRegion.Count() = 0 Then
                m_view.SelectedRegionName = ""
                m_view.ErrorText = "No region data found for brand " + m_view.Brand.Trim() + " and brand line " + m_view.BrandLine + "."
                If m_view.CertifiedRegions IsNot Nothing Then
                    m_view.CertifiedRegions.Clear()
                End If
                If m_view.UncertifiedRegions IsNot Nothing Then
                    m_view.UncertifiedRegions.Clear()
                End If
                If m_view.ProductCountryStatusData IsNot Nothing Then
                    m_view.ProductCountryStatusData.Clear()
                End If
                'initial the region lists
                Dim listCertifiedRegions As List(Of String) = New List(Of String)
                listCertifiedRegions.Add("Select ...")
                m_view.CertifiedRegions = listCertifiedRegions
                Dim listUnCertifiedRegions As List(Of String) = New List(Of String)
                listUnCertifiedRegions.Add("Select ...")
                m_view.UncertifiedRegions = listUnCertifiedRegions
                m_view.SetupRegionView(m_view.ProductCountryStatusData)
                m_view.DataBindView()
            Else
                Dim listCertifiedRegions As List(Of String) = New List(Of String)
                listCertifiedRegions.Add("Select ...")
                listCertifiedRegions.InsertRange(1, listCertifiedRegion)
                m_view.CertifiedRegions = listCertifiedRegions
                Dim listUnCertifiedRegions As List(Of String) = New List(Of String)
                listUnCertifiedRegions.Add("Select ...")
                listUnCertifiedRegions.InsertRange(1, listUnCertifiedRegion)
                m_view.UncertifiedRegions = listUnCertifiedRegions
                m_view.DataBindView()

                If listCertifiedRegions.Count > 1 Then
                    ' no selected Region Name available
                    If m_view.SelectedRegionName = "" Then
                        m_view.SelectedRegionName = listCertifiedRegions.Item(1)
                        ' Delete region situation
                    ElseIf Not listCertifiedRegions.Contains(m_view.SelectedRegionName) Then
                        m_view.SelectedRegionName = listCertifiedRegions.Item(1)
                        ' Add region situation
                    ElseIf bnlAddRegionFlag Then
                        m_view.SelectedRegionName = listCertifiedRegions.Item(1)
                    Else
                        ' regular save in certified region list
                        ' Assign the value back to restore the index value of dropdown list
                        m_view.SelectedRegionName = m_view.SelectedRegionName
                    End If
                    RefreshCurrentRegionView()
                    ' no certified region under this brand and brand line.
                ElseIf listCertifiedRegions.Count = 1 Then
                    m_view.SelectedRegionName = ""
                    RefreshCurrentRegionView()
                Else
                    m_view.DataBindView()
                End If
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.SelectedRegionName = ""
            m_view.ErrorText = "Error loading data for brand " + m_view.Brand.Trim() + " and brand line " + m_view.BrandLine + "."
        End Try

    End Sub

    ''' <summary>
    ''' Save the changes and refresh
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnSave(ByVal sender As Object, ByVal e As EventArgs)

        Dim strInfoText As String = String.Empty

        Try

            Dim dtmSaveTime As DateTime = DateTime.Now
            Dim blnDone As Boolean = False
            Dim enuNotificationResult As AuditLogModel.NotificationResult

            Dim marketing As Marketing = New Marketing()
            blnDone = marketing.SaveCertificationTable(m_view.ProductCountryStatusData, m_view.CertificationGroupCountries)

            If blnDone Then
                strInfoText = "Saved."

                enuNotificationResult = AuditLogModel.CheckForChangesAndSendNotification(AuditLogEntry.AreaOfChange.Marketing, dtmSaveTime)
                strInfoText &= " "
                If enuNotificationResult = AuditLogModel.NotificationResult.Sent Then
                    strInfoText &= "Notification sent."
                ElseIf enuNotificationResult = AuditLogModel.NotificationResult.SendError Then
                    strInfoText &= "Notification failed."
                ElseIf enuNotificationResult = AuditLogModel.NotificationResult.Disabled Then
                    strInfoText &= "Notification disabled."
                End If

                OnSearch(sender, e)
                m_view.DataDirtyFlag = False
            Else
                strInfoText = "Save Failed."
            End If

            m_view.SuccessText = strInfoText
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error saving data for region " + m_view.SelectedRegionName + "."
        End Try

    End Sub

    ' Added LoadBrands method as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation   
    ''' <summary>
    ''' Load Data for the Brands to View.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadBrands(ByVal p_strBrand As String)
        Try
            ' Set page controls to default view.
            m_view.SetupDefaultView()
            'Get the Brand names for Add Brand drop-down list
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim listBrandNames As List(Of String) = certSearch.GetBrands(p_strBrand)
            listBrandNames.Insert(0, "Select ...")
            m_view.Brands = listBrandNames
            m_view.DataBindView()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error loading Brand Types data."
        End Try
    End Sub

    ''' <summary>
    ''' Load Data for the BrandLines to View.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadBrandLines(ByVal p_strLine As String)
        Try
            ' Set page controls to default view.
            m_view.SetupDefaultView()
            'Get the Brand Line names for Add Brand Line drop-down list
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim listBrandLineNames As List(Of String) = certSearch.GetBrandLines(p_strLine)
            listBrandLineNames.Insert(0, "Select ...")
            m_view.BrandLines = listBrandLineNames
            m_view.DataBindView()

        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error loading Brand data."
        End Try
    End Sub

    Public Function GetMaterialAttribs(ByVal p_strMaterialNum As String) As String
        Dim strToolTip As String = String.Empty
        Try
            'Get the Tool tip for Material Number
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim dtTooltip As New DataTable
            dtTooltip = certSearch.GetMaterialAttribs(p_strMaterialNum)
            strToolTip = certSearch.FormatMaterialAttribsForToolTip(dtTooltip)
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error loading Brand Line data."
        End Try
        Return strToolTip
    End Function


#End Region

End Class
