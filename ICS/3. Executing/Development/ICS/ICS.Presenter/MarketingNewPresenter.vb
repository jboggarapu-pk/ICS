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
Public Class MarketingNewPresenter
    Inherits ViewPresenterBase

    ' Changed sku to material number , added methods for populating the data to view for brand , brand lines and attributes for material numbers.
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members and Constants"
    ''' <summary>
    ''' interface to be implemented by marketing view.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_view As IMarketingNewView
    ''' <summary>
    '''  Constant to hold Uncertified text
    ''' </summary>
    Private Const UncertifiedText As String = "Uncertified"
    ''' <summary>
    '''  Constant to hold Requested text
    ''' </summary>
    Private Const RequestedText As String = "Requested"
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
    Public Sub New(ByVal p_view As IMarketingNewView)

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
    ''' Attach presenter to view’s events.
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
            AddHandler m_view.ChangeAllMaterialsStatusByCert, AddressOf OnChangeAllSKUsStatusByCert
            AddHandler m_view.ChangeAllCertStatusByMaterial, AddressOf OnChangeAllCertStatusBySKU
            AddHandler m_view.Search, AddressOf OnSearch
            AddHandler m_view.ProductRequestStatusChanged, AddressOf OnProductRequestStatusChanged
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
        Const ErrorLoadFormDataText As String = "Error loading form data."
        Try
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
            Else
                RestoreView()
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorLoadFormDataText
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
            If Not m_view.ProductRequestStatusData Is Nothing Then
                m_view.SetupCertificateView(m_view.ProductRequestStatusData)
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
    Private Sub RefreshCurrentCertView()
        Const NoCertError As String = "no certification data found."
        Const ProductRequestStatusText As String = "ProductRequestStatus"
        Const ToolTipText As String = "ToolTip"
        Const SystemStringText As String = "System.String"
        Const MaterialNumberText As String = "MATL_NUM"
        Const CertificateTypesText As String = "CertificateTypes"

        Try
            Dim strBrand As String = m_view.Brand.Trim()
            Dim strBrandLine As String = m_view.BrandLine.Trim()


            Dim marketingnew As MarketingNew = New MarketingNew()

            m_view.ErrorText = ""

            'If m_view.ProductRequestStatusData Is Nothing Then

            ' jeseitz - check this!!! new procedure!
            Dim dstProductRequestStatus As DataSet = marketingnew.GetProductRequestStatusTable(strBrand, strBrandLine)
            If Not dstProductRequestStatus.Tables(ProductRequestStatusText).Columns.Contains(ToolTipText) Then
                dstProductRequestStatus.Tables(ProductRequestStatusText).Columns.Add(ToolTipText, Type.GetType(SystemStringText))
            End If
            For Each dr As DataRow In dstProductRequestStatus.Tables(ProductRequestStatusText).Rows
                If Not (dr(MaterialNumberText) Is Nothing Or IsDBNull(dr(MaterialNumberText))) Then
                    dr(ToolTipText) = GetMaterialAttribs(dr(MaterialNumberText).ToString())
                End If
            Next
            dstProductRequestStatus.AcceptChanges()

            Dim dtbProductRequestStatusTable As DataTable = dstProductRequestStatus.Tables(ProductRequestStatusText)
            Dim dtbCertificateTypesTable As DataTable = dstProductRequestStatus.Tables(CertificateTypesText)

            m_view.ProductRequestStatusData = dtbProductRequestStatusTable
            m_view.CertificateTypes = dtbCertificateTypesTable

            If m_view.ProductRequestStatusData.Rows.Count = 0 Then
                m_view.ErrorText = NoCertError
            End If
            m_view.SetupCertificateView(m_view.ProductRequestStatusData)

            m_view.DataBindView()
            ' End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Select/DeSelect All country by SKU.
    ''' </summary>
    ''' <param name="p_strCertId">Certificate Id</param>
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
    Private Sub OnChangeAllSKUsStatusByCert(ByVal p_strCertId As String, ByVal p_blnChecked As Boolean)
        Const ErrorSelectCountry As String = "Error select/deselect all certificate for Country."

        Try
            Dim dtbProductRequestStatus As DataTable = m_view.ProductRequestStatusData
            For Each drwStatus As DataRow In dtbProductRequestStatus.Rows
                If p_blnChecked And drwStatus(p_strCertId).ToString() = UncertifiedText Then
                    drwStatus(p_strCertId) = RequestedText
                ElseIf Not p_blnChecked And drwStatus(p_strCertId).ToString() = RequestedText Then
                    drwStatus(p_strCertId) = UncertifiedText
                End If
            Next
            m_view.ProductRequestStatusData = dtbProductRequestStatus
            m_view.DataBindView()
            m_view.DataDirtyFlag = True
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSelectCountry
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
    Private Sub OnChangeAllCertStatusBySKU(ByVal p_strMatlNum As String, ByVal p_blnChecked As Boolean)
        Const ErrorSelectCountry As String = "Error select/deselect all certificate for Material Number "
        Const SKUIDText As String = "SKUID"
        Const MaterialNumberText As String = "MATL_NUM"
        Const SizeText As String = "Size"
        Const SingleLoadIndexText As String = "SingLoadIndex"
        Const DualLoadIndexText As String = "DualLoadIndex"
        Const SpeedRatingText As String = "SpeedRating"

        Try
            Dim dtbProductRequestStatus As DataTable = m_view.ProductRequestStatusData
            ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            Dim position As Integer
            position = p_strMatlNum.IndexOf(" ")
            If (position > -1) Then
                p_strMatlNum = p_strMatlNum.Substring(0, position)
            End If
            Dim drwStatus As DataRow = dtbProductRequestStatus.Rows.Find(p_strMatlNum.PadLeft(18, CChar("0")))
            For Each drwStatusCloumn As DataColumn In drwStatus.Table.Columns
                If drwStatusCloumn.ColumnName <> SKUIDText And drwStatusCloumn.ColumnName <> MaterialNumberText And drwStatusCloumn.ColumnName <> SizeText And _
                drwStatusCloumn.ColumnName <> SingleLoadIndexText And drwStatusCloumn.ColumnName <> DualLoadIndexText And _
                drwStatusCloumn.ColumnName <> SpeedRatingText Then
                    If p_blnChecked And drwStatus(drwStatusCloumn) Is UncertifiedText Then
                        drwStatus(drwStatusCloumn) = RequestedText
                    ElseIf Not p_blnChecked And drwStatus(drwStatusCloumn) Is RequestedText Then
                        drwStatus(drwStatusCloumn) = UncertifiedText
                    End If
                End If
            Next
            m_view.ProductRequestStatusData = dtbProductRequestStatus
            m_view.DataBindView()
            m_view.DataDirtyFlag = True
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSelectCountry + p_strMatlNum + "."
        End Try

    End Sub

    ''' <summary>
    ''' Event handler for check box checked/unchecked per country and SKU.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_strCountryId">country Id</param>
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
    Private Sub OnProductRequestStatusChanged(ByVal p_strMatlNum As String, ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)
        Const ErrorSaveData As String = "Error Saving data."
        Try
            Dim dtbProductCountryStatus As DataTable = m_view.ProductRequestStatusData
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
            m_view.ProductRequestStatusData = CType(dtbProductCountryStatus, DataTable)
            m_view.DataBindView()
            m_view.DataDirtyFlag = True
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSaveData
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
        Const ErorLoadBrand As String = "Error loading data for brand "
        Const BrandLineText As String = " and brand line "
        Try
            Dim strBrand As String = m_view.Brand.Trim()
            Dim strBrandLine As String = m_view.BrandLine.Trim()

            If (strBrand = SelectText Or strBrandLine = SelectText Or strBrandLine = String.Empty) Then
                
                m_view.ErrorText = SelectBrandtext

                If m_view.ProductRequestStatusData IsNot Nothing Then
                    m_view.ProductRequestStatusData.Clear()
                End If

                m_view.SetupCertificateView(m_view.ProductRequestStatusData)
                m_view.DataBindView()
                Return
            End If

            RefreshCurrentCertView()

        Catch exc As Exception
            EventLogger.Enter(exc)
            'm_view.SelectedRegionName = ""
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
        Const SaveText As String = "Saved."
        Const SentText As String = "Notification sent."
        Const FailedText As String = "Notification failed."
        Const DisabledText As String = "Notification disabled."
        Const SaveFailText As String = "Save Failed."
        Try
            Dim dtmSaveTime As DateTime = DateTime.Now
            Dim blnDone As Boolean = False
            Dim enuNotificationResult As AuditLogModel.NotificationResult

            Dim marketingnew As MarketingNew = New MarketingNew()
            blnDone = marketingnew.SaveCertificationTable(m_view.ProductRequestStatusData, m_view.CertificateTypes)

            If blnDone Then
                strInfoText = SaveText

                enuNotificationResult = AuditLogModel.CheckForChangesAndSendNotification(AuditLogEntry.AreaOfChange.Marketing, dtmSaveTime)
                strInfoText &= " "
                If enuNotificationResult = AuditLogModel.NotificationResult.Sent Then
                    strInfoText &= SentText
                ElseIf enuNotificationResult = AuditLogModel.NotificationResult.SendError Then
                    strInfoText &= FailedText
                ElseIf enuNotificationResult = AuditLogModel.NotificationResult.Disabled Then
                    strInfoText &= DisabledText
                End If

                OnSearch(sender, e)
                m_view.DataDirtyFlag = False
            Else
                strInfoText = SaveFailText
            End If

            m_view.SuccessText = strInfoText
        Catch exc As Exception
            EventLogger.Enter(exc)
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
        Const ErrorLoadData As String = "Error loading Brand Types data."
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
            m_view.ErrorText = ErrorLoadData
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
        Const ErrorLoadData As String = "Error loading Brand data."
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
            m_view.ErrorText = ErrorLoadData
        End Try
    End Sub

    ''' <summary>
    ''' Load Data for the BrandLines to View.
    ''' </summary>
    ''' <param name="p_strMaterialNum">material number</param>
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
        Const ErrorLoadData As String = "Error loading Brand Line data."
        Try
            'Get the Tool tip for Material Number
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim dtTooltip As New DataTable
            dtTooltip = certSearch.GetMaterialAttribs(p_strMaterialNum)
            strToolTip = certSearch.FormatMaterialAttribsForToolTip(dtTooltip)
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadData
        End Try
        Return strToolTip
    End Function
#End Region

End Class
