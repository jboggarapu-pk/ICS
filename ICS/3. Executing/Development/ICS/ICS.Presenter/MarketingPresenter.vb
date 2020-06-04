Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Marketing view presenter
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
''' <term>Jhansi</term>
''' <description>
''' <para>10/21/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class MarketingPresenter
    Inherits ViewPresenterBase

    ' Changed sku to material number , added methods for populating the data to view for brand , brand lines and attributes for material numbers.
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members and Constants"
    ''' <summary>
    ''' Interface to be implemented by marketing view.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_view As IMarketingView
    ''' <summary>
    '''  Constant to hold MaterialNumber text
    ''' </summary>
    Private Const MaterialNumberText As String = "MATL_NUM"
    ''' <summary>
    '''  Constant to hold Requested text
    ''' </summary>
    Private Const RequestedText As String = "Requested"
    ''' <summary>
    '''  Constant to hold Uncertified text
    ''' </summary>
    Private Const UncertifiedText As String = "Uncertified"
    ''' <summary>
    '''  Constant to hold Select text
    ''' </summary>
    Private Const SelectText As String = "Select ..."
#End Region

#Region "Constructors / Destructors"
    ''' <summary>
    ''' Custom Constructor to initialize class members.
    ''' </summary>
    ''' <param name="p_view">View</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As IMarketingView)

        MyBase.New(p_view)
        Const ErrorCreatingText As String = "Error creating "
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw New Exception(ErrorCreatingText + Me.ToString())
        End Try
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Attach presenter to view�s events.
    ''' </summary>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()
        Try
            AddHandler m_view.LoadView, AddressOf OnLoadView
            AddHandler m_view.Save, AddressOf OnSave
            AddHandler m_view.ChangeAllMaterialsStatusByCountry, AddressOf OnChangeAllSKUsStatusByCountry
            AddHandler m_view.ChangeAllCountriesStatusByMaterial, AddressOf OnChangeAllCountriesStatusBySKU
            AddHandler m_view.Search, AddressOf OnSearch
            AddHandler m_view.RegionChanged, AddressOf OnRegionChanged
            AddHandler m_view.AddRegionChanged, AddressOf OnAddRegionChanged
            AddHandler m_view.SelectChangedWithDataDirty, AddressOf OnSelectChangedWithDataDirty
            AddHandler m_view.ProductCountryStatusChanged, AddressOf OnProductCountryStatusChanged
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Load data for the view.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)
        Const ErrorLoadFormText As String = "Error loading form data."
        Try
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
            Else
                RestoreView()
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorLoadFormText
        End Try

    End Sub

    ''' <summary>
    ''' Load data from business process.
    ''' </summary>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadViewData()
        Try
            m_view.DataBindView()
            m_view.DataDirtyFlag = False
            LoadBrands(String.Empty)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' restore the view when there's no postback.
    ''' </summary>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub RestoreView()
        Try
            If Not m_view.ProductCountryStatusData Is Nothing Then
                m_view.SetupRegionView(m_view.ProductCountryStatusData)
            End If
            m_view.SuccessText = ""
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Update regional status.
    ''' </summary>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub RefreshCurrentRegionView()

        Dim strBrand As String = m_view.Brand.Trim()
        Dim strBrandLine As String = m_view.BrandLine.Trim()
        Dim strRegionName As String = m_view.SelectedRegionName.Trim()
        Const ProductCountryStatusText As String = "ProductCountryStatus"
        Const CertifiedCountriesText As String = "CertifiedCountries"
        Const ToolTipText As String = "ToolTip"
        Const SystemText As String = "System.String"
        Const NoRegionDataText As String = "no region data found."
        Try
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
                If Not dstProductCountryStatusAndCertificationCountry.Tables(ProductCountryStatusText).Columns.Contains(ToolTipText) Then
                    dstProductCountryStatusAndCertificationCountry.Tables(ProductCountryStatusText).Columns.Add(ToolTipText, Type.GetType(SystemText))
                End If
                For Each dr As DataRow In dstProductCountryStatusAndCertificationCountry.Tables(ProductCountryStatusText).Rows
                    If Not (dr(MaterialNumberText) Is Nothing Or IsDBNull(dr(MaterialNumberText))) Then
                        dr(ToolTipText) = GetMaterialAttribs(dr(MaterialNumberText).ToString())
                    End If
                Next
                dstProductCountryStatusAndCertificationCountry.AcceptChanges()

                Dim dtbProductCountryStatusTable As DataTable = dstProductCountryStatusAndCertificationCountry.Tables(ProductCountryStatusText)
                Dim dtbCertificationCountryTable As DataTable = dstProductCountryStatusAndCertificationCountry.Tables(CertifiedCountriesText)
                m_view.ProductCountryStatusData = dtbProductCountryStatusTable
                m_view.CertificationGroupCountries = dtbCertificationCountryTable

                If m_view.ProductCountryStatusData.Rows.Count = 0 Then
                    m_view.ErrorText = NoRegionDataText
                End If
                m_view.SetupRegionView(m_view.ProductCountryStatusData)
            End If

            m_view.DataBindView()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Select/DeSelect All SKU by country.
    ''' </summary>
    ''' <param name="p_strCountryId">Country Id</param>
    ''' <param name="p_blnChecked">checked</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnChangeAllSKUsStatusByCountry(ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)
        Const ErrorSelectText As String = "Error select/deselect all certificate for Country."
        Try
            Dim dtbProductCountryStatus As DataTable = m_view.ProductCountryStatusData
            For Each drwStatus As DataRow In dtbProductCountryStatus.Rows
                If p_blnChecked And drwStatus(p_strCountryId) Is UncertifiedText Then
                    drwStatus(p_strCountryId) = RequestedText
                ElseIf Not p_blnChecked And drwStatus(p_strCountryId) Is RequestedText Then
                    drwStatus(p_strCountryId) = UncertifiedText
                End If
            Next
            m_view.ProductCountryStatusData = dtbProductCountryStatus
            m_view.DataBindView()
            m_view.DataDirtyFlag = True
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSelectText
        End Try
    End Sub

    ''' <summary>
    ''' Select/DeSelect All country by SKU.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_blnChecked">Checked</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnChangeAllCountriesStatusBySKU(ByVal p_strMatlNum As String, ByVal p_blnChecked As Boolean)
        Const SKUId As String = "SKUID"
        Const SizeText As String = "Size"
        Const SingLoadIndexText As String = "SingLoadIndex"
        Const DualLoadIndexText As String = "DualLoadIndex"
        Const SpeedRatingText As String = "SpeedRating"
        Const ErrorSelectMaterialNumberText As String = "Error select/deselect all certificate for Material Number "

        Try
            Dim dtbProductCountryStatus As DataTable = m_view.ProductCountryStatusData
            ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            Dim position As Integer
            position = p_strMatlNum.IndexOf(" ")
            If (position > -1) Then
                p_strMatlNum = p_strMatlNum.Substring(0, position)
            End If
            Dim drwStatus As DataRow = dtbProductCountryStatus.Rows.Find(p_strMatlNum.PadLeft(18, CChar("0")))
            For Each drwStatusCloumn As DataColumn In drwStatus.Table.Columns
                If drwStatusCloumn.ColumnName <> SKUId And drwStatusCloumn.ColumnName <> MaterialNumberText And drwStatusCloumn.ColumnName <> SizeText And _
                drwStatusCloumn.ColumnName <> SingLoadIndexText And drwStatusCloumn.ColumnName <> DualLoadIndexText And _
                drwStatusCloumn.ColumnName <> SpeedRatingText Then
                    If p_blnChecked And drwStatus(drwStatusCloumn) Is UncertifiedText Then
                        drwStatus(drwStatusCloumn) = RequestedText
                    ElseIf Not p_blnChecked And drwStatus(drwStatusCloumn) Is RequestedText Then
                        drwStatus(drwStatusCloumn) = UncertifiedText
                    End If
                End If
            Next
            m_view.ProductCountryStatusData = dtbProductCountryStatus
            m_view.DataBindView()
            m_view.DataDirtyFlag = True
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSelectMaterialNumberText + p_strMatlNum + "."
        End Try
    End Sub

    ''' <summary>
    ''' Certified region dropdown list index changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnRegionChanged(ByVal sender As Object, ByVal e As EventArgs)
        Const ErrorLoadDataSelectedRegionText As String = "Error loading data for selected region "
        Try
            Dim strSelectRegion As Object = CType(sender, String)
            If strSelectRegion Is SelectText Then
                m_view.SelectedRegionName = CStr(strSelectRegion)
                RefreshCurrentRegionView()
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorLoadDataSelectedRegionText + m_view.SelectedRegionName + "."
        End Try
    End Sub

    ''' <summary>
    ''' Uncertified region dropdown list index changed. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnAddRegionChanged(ByVal sender As Object, ByVal e As EventArgs)
        Const ErrorLoadDataAddSelectedRegionText As String = "Error loading data for add selected region "
        Try
            Dim strSelectRegion As Object = CType(sender, String)
            If strSelectRegion Is SelectText Then
                m_view.SelectedRegionName = CStr(strSelectRegion)
                RefreshCurrentRegionView()
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorLoadDataAddSelectedRegionText + m_view.SelectedRegionName + "."
        End Try
    End Sub

    ''' <summary>
    ''' Store original value when dropdown list index changed with data dirty.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnSelectChangedWithDataDirty(ByVal sender As Object, ByVal e As EventArgs)
        Const ErrorSaveDataOriginalSelectedRegionText As String = "Error saving data for original selected region."
        Try
            Dim strSelectRegion As Object = CType(sender, String)
            If m_view.CertifiedRegions.Contains(m_view.SelectedRegionName) Then
                m_view.OrignalCertRegionSelectValue = m_view.SelectedRegionName
                m_view.OrignalUnCertRegionSelectValue = Nothing
            Else
                m_view.OrignalCertRegionSelectValue = Nothing
                m_view.OrignalUnCertRegionSelectValue = m_view.SelectedRegionName
            End If
            m_view.SelectedRegionName = CStr(strSelectRegion)
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSaveDataOriginalSelectedRegionText
        End Try
    End Sub

    ''' <summary>
    ''' Event handler for check box checked/unchecked per country and SKU.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_strCountryId">Country Id</param>
    ''' <param name="p_blnChecked">checked</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnProductCountryStatusChanged(ByVal p_strMatlNum As String, ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)
        Const ErrorSaveDataText As String = "Error Saving data."

        Try
            Dim dtbProductCountryStatus As DataTable = m_view.ProductCountryStatusData
            ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            Dim position As Integer
            position = p_strMatlNum.IndexOf(" ")
            If (position > -1) Then
                p_strMatlNum = p_strMatlNum.Substring(0, position)
            End If
            Dim drwStatus As DataRow = dtbProductCountryStatus.Rows.Find(p_strMatlNum.PadLeft(18, CChar("0")))
            If Not (drwStatus Is Nothing) Then
                If drwStatus.Table.Columns.Contains(p_strCountryId) Then
                    If p_blnChecked Then
                        drwStatus(p_strCountryId) = RequestedText
                    Else
                        drwStatus(p_strCountryId) = UncertifiedText
                    End If
                End If
            End If
            m_view.ProductCountryStatusData = dtbProductCountryStatus
            m_view.DataBindView()
            m_view.DataDirtyFlag = True
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSaveDataText
        End Try
    End Sub

    ''' <summary>
    ''' Search the product country status based on brand. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnSearch(ByVal sender As Object, ByVal e As EventArgs)
        Const SelectBrandtext As String = "Please select a brand and brand line for search."
        Const NoRegionDataText As String = "No region data found for brand "
        Const ErorLoadBrand As String = "Error loading data for brand "
        Const BrandLineText As String = " and brand line "
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

            If (strBrand = SelectText Or strBrandLine = SelectText Or strBrandLine = String.Empty) Then
                'clear the existing data when empty input of brand 
                m_view.SelectedRegionName = ""
                m_view.ErrorText = SelectBrandtext
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
                listCertifiedRegions.Add(SelectText)
                m_view.CertifiedRegions = listCertifiedRegions
                Dim listUnCertifiedRegions As List(Of String) = New List(Of String)
                listUnCertifiedRegions.Add(SelectText)
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
                m_view.ErrorText = NoRegionDataText + m_view.Brand.Trim() + BrandLineText + m_view.BrandLine + "."
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
                listCertifiedRegions.Add(SelectText)
                m_view.CertifiedRegions = listCertifiedRegions
                Dim listUnCertifiedRegions As List(Of String) = New List(Of String)
                listUnCertifiedRegions.Add(SelectText)
                m_view.UncertifiedRegions = listUnCertifiedRegions
                m_view.SetupRegionView(m_view.ProductCountryStatusData)
                m_view.DataBindView()
            Else
                Dim listCertifiedRegions As List(Of String) = New List(Of String)
                listCertifiedRegions.Add(SelectText)
                listCertifiedRegions.InsertRange(1, listCertifiedRegion)
                m_view.CertifiedRegions = listCertifiedRegions
                Dim listUnCertifiedRegions As List(Of String) = New List(Of String)
                listUnCertifiedRegions.Add(SelectText)
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
            m_view.ErrorText = ErorLoadBrand + m_view.Brand.Trim() + BrandLineText + m_view.BrandLine + "."
        End Try
    End Sub

    ''' <summary>
    ''' Save the changes and refresh.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnSave(ByVal sender As Object, ByVal e As EventArgs)
        Dim strInfoText As String = String.Empty
        Const ErrorSaveData As String = "Error saving data for region "
        Const SaveFailText As String = "Save Failed."
        Const DisableText As String = "Notification disabled."
        Const FailText As String = "Notification failed."
        Const SentText As String = "Notification sent."
        Const SaveText As String = "Saved."
        Try
            Dim dtmSaveTime As DateTime = DateTime.Now
            Dim blnDone As Boolean = False
            Dim enuNotificationResult As AuditLogModel.NotificationResult

            Dim marketing As Marketing = New Marketing()
            blnDone = marketing.SaveCertificationTable(m_view.ProductCountryStatusData, m_view.CertificationGroupCountries)

            If blnDone Then
                strInfoText = SaveText

                enuNotificationResult = AuditLogModel.CheckForChangesAndSendNotification(AuditLogEntry.AreaOfChange.Marketing, dtmSaveTime)
                strInfoText &= " "
                If enuNotificationResult = AuditLogModel.NotificationResult.Sent Then
                    strInfoText &= SentText
                ElseIf enuNotificationResult = AuditLogModel.NotificationResult.SendError Then
                    strInfoText &= FailText
                ElseIf enuNotificationResult = AuditLogModel.NotificationResult.Disabled Then
                    strInfoText &= DisableText
                End If

                OnSearch(sender, e)
                m_view.DataDirtyFlag = False
            Else
                strInfoText = SaveFailText
            End If

            m_view.SuccessText = strInfoText
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSaveData + m_view.SelectedRegionName + "."
        End Try
    End Sub

    ' Added LoadBrands method as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation   
    ''' <summary>
    ''' Load Data for the Brands to View.
    ''' </summary>
    ''' <param name="p_strBrand">Brand</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadBrands(ByVal p_strBrand As String)
        Const ErrorLoadBrandData As String = "Error loading Brand Types data."
        Try
            ' Set page controls to default view.
            m_view.SetupDefaultView()
            'Get the Brand names for Add Brand drop-down list
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim listBrandNames As List(Of String) = certSearch.GetBrands(p_strBrand)
            listBrandNames.Insert(0, SelectText)
            m_view.Brands = listBrandNames
            m_view.DataBindView()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadBrandData
        End Try
    End Sub

    ''' <summary>
    ''' Load Data for the BrandLines to View.
    ''' </summary>
    ''' <param name="p_strLine">Line</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub LoadBrandLines(ByVal p_strLine As String)
        Const ErrorLoadBrandData As String = "Error loading Brand data."
        Try
            ' Set page controls to default view.
            m_view.SetupDefaultView()
            'Get the Brand Line names for Add Brand Line drop-down list
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim listBrandLineNames As List(Of String) = certSearch.GetBrandLines(p_strLine)
            listBrandLineNames.Insert(0, SelectText)
            m_view.BrandLines = listBrandLineNames
            m_view.DataBindView()

        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadBrandData
        End Try
    End Sub

    ''' <summary>
    ''' Load Data for the BrandLines to View.
    ''' </summary>
    ''' <param name="p_strMaterialNum">Material number</param>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/21/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetMaterialAttribs(ByVal p_strMaterialNum As String) As String
        Dim strToolTip As String = String.Empty
        Const ErrorLoadBrandData As String = "Error loading Brand Line data."
        Try
            'Get the Tool tip for Material Number
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim dtTooltip As New DataTable
            dtTooltip = certSearch.GetMaterialAttribs(p_strMaterialNum)
            strToolTip = certSearch.FormatMaterialAttribsForToolTip(dtTooltip)
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadBrandData
        End Try
        Return strToolTip
    End Function
#End Region

End Class
