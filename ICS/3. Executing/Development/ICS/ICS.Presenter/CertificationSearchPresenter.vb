Imports System.Xml

Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Certification Search view presenter
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
''' <para>10/15/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class CertificationSearchPresenter
    Inherits ViewPresenterBase

    ' Changed sku to material number , added methods for populating the data to view for brand , brand lines and attributes for material numbers.
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Properties"
    ''' <summary>
    ''' Gets or sets Current Certificate Number value.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Shared Property CurrentCertificateNumber() As String
        Get
            Return m_view.CurrentCertificateNumber
        End Get
        Set(ByVal value As String)
            m_view.CurrentCertificateNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Current Certificate SKU ID value.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Shared Property CurrentCertificateSKUID() As String
        Get
            Return m_view.CurrentCertificateSKUID
        End Get
        Set(ByVal value As String)
            m_view.CurrentCertificateSKUID = value
        End Set
    End Property

#End Region

#Region "Members and Constants"
    ''' <summary>
    ''' Certification Search view pesenter.
    ''' </summary>
    Private Shared m_view As ICertificationSearchView
    ''' <summary>
    '''  Constant to hold ErrorLoadFormData text
    ''' </summary>
    Private Const ErrorLoadFormDataText As String = "Error loading form data."
    ''' <summary>
    '''  Constant to hold Select text
    ''' </summary>
    Private Const SelectText As String = "Select ..."
    ''' <summary>
    '''  Constant to hold Certification Number text
    ''' </summary>
    Private Const CertificationNumberText As String = "Certification No."
    ''' <summary>
    '''  Constant to hold Imark Text
    ''' </summary>
    Private Const ImarkText As String = "Imark"
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As ICertificationSearchView)

        MyBase.New(p_view)
        Const ErrorCreatingText As String = "Error creating "
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exp As Exception
            EventLogger.Enter(exp)
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()
        Try
            AddHandler m_view.LoadView, AddressOf OnLoadView
            AddHandler m_view.Search, AddressOf OnSearch
            AddHandler m_view.LeafNodeSelected, AddressOf OnLeafNodeSelected
            AddHandler m_view.AddCertificateSelected, AddressOf OnAddCertificateSelected
            AddHandler m_view.Execute, AddressOf OnExecute
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
                m_view.DataBindView()
            Else
                RestoreView()
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadViewData()
        Try
            'Get the certification names for Add Certification drop-down list
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim listCertNames As List(Of String) = certSearch.GetCertificationNames()
            ' Added as per project 2706 technical specification
            listCertNames.Insert(0, SelectText)
            m_view.CertificationNames = listCertNames

            'Get the search types for the Search drop-down list
            Dim lstSearchTypes As List(Of String) = certSearch.GetSearchTypesList()
            m_view.CerttificationSearchTypes = lstSearchTypes

            ' Fill Brands drop-down list
            LoadBrands(String.Empty)

            m_view.InitializeTreeView()

            m_view.CurrentCertificateName = String.Empty
            m_view.CurrentCertificatonTypeID = 0
            m_view.CurrentCertificateMaterialNum = String.Empty
            m_view.CurrentAddCertificateName = String.Empty
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Restore view.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub RestoreView()
        Try
            If String.IsNullOrEmpty(m_view.CurrentAddCertificateName) Then
                m_view.ShowCurrentCertificate()
            Else
                m_view.ShowAddCertification(False)
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Called when search button is pressed.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnSearch()
        Const NoBrandErrorText As String = "No Brand or Brand Line selected."
        Const NoSearchCriteriaError As String = "No search criteria entered."
        Const NoExtensionErrorText As String = "No extension or * entered."
        Const ImarkSearchErrorText As String = "Please use Imark search option for I033"
        Const NoFamilyErrorText As String = "No family entered"
        Const BrandText As String = "Brand"
        Const I033Text As String = "I033"
        Const MaterialNumberText As String = "Material No."
        Const SimTireText As String = "SimTire"

        Try
            m_view.CurrentCertificateName = String.Empty
            m_view.CurrentCertificateMaterialNum = String.Empty
            m_view.CurrentAddCertificateName = String.Empty
            m_view.CurrentCertificatonTypeID = 0

            'Get SearchType from the view
            Dim strSearchType As String = m_view.SearchType.Trim()

            'Get search criteria from the view.  Validate that search criteria was entered.
            Dim strSearchCriteria As String = m_view.SearchCriteria.Trim()
            Dim strExtensionNo As String = m_view.ExtensionNo.Trim()
            Dim strImarkFamily As String = m_view.ImarkFamily
            Dim strBrandLine As String = m_view.BrandLine

            If strSearchType = BrandText Then
                If strSearchCriteria = SelectText Or strExtensionNo = SelectText Or strBrandLine = String.Empty Then
                    m_view.ErrorText = NoBrandErrorText
                    Return
                End If

            ElseIf strSearchCriteria = String.Empty Then
                m_view.ErrorText = NoSearchCriteriaError
                Return
            Else
                Select Case strSearchType
                    Case CertificationNumberText
                        If strExtensionNo = String.Empty Then
                            m_view.ErrorText = NoExtensionErrorText
                            Return
                        End If
                        If strSearchCriteria = I033Text Then
                            m_view.ErrorText = ImarkSearchErrorText
                            Return
                        End If
                    Case ImarkText
                        If strImarkFamily = String.Empty Then
                            m_view.ErrorText = NoFamilyErrorText
                            Return
                        End If
                    Case Else
                End Select
            End If

            'Call the model and get the search results from database -- For SKU search this checks if SKU exists on any current certificates
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim xDoc As XmlDocument = Nothing
            Dim dtbCertSearch As DataTable = Nothing
            If (strSearchType = CertificationNumberText) Then
                xDoc = certSearch.GetCertificationSearchResultsForCertificateNumber(strSearchCriteria, strSearchType, strExtensionNo, strImarkFamily, strBrandLine, dtbCertSearch)
            Else
                xDoc = certSearch.GetCertificationSearchResults(strSearchCriteria, strSearchType, strExtensionNo, strImarkFamily, strBrandLine)
            End If

            If strSearchType = MaterialNumberText Then
                Dim xSimTireDoc As XmlDocument = certSearch.GetCertificationSearchResults(strSearchCriteria, SimTireText, String.Empty, String.Empty, String.Empty)
                If xDoc.ChildNodes(1).ChildNodes.Count = 0 Then
                    m_view.PopulateTreeView(xSimTireDoc)
                    Exit Sub
                Else
                    For Each xNode As XmlNode In xDoc.ChildNodes(1).ChildNodes
                        For Each Item As XmlNode In xNode.SelectNodes(NameAid.Column.CertificationName)
                            Dim strCertificationName As String = Item.FirstChild.InnerText.ToLower()
                            For Each xSimNode As XmlNode In xSimTireDoc.ChildNodes(1).ChildNodes
                                For Each SimItem As XmlNode In xSimNode.SelectNodes(NameAid.Column.CertificationName)
                                    If Not SimItem.FirstChild Is Nothing Then
                                        If SimItem.FirstChild.InnerText.ToLower() = strCertificationName Then
                                            xSimNode.RemoveChild(SimItem)
                                        End If
                                    End If
                                Next
                            Next
                        Next
                    Next

                    'Marry the two XML docs
                    For Each xSimNode As XmlNode In xSimTireDoc.ChildNodes(1).ChildNodes
                        For Each SimItem As XmlNode In xSimNode.SelectNodes(NameAid.Column.CertificationName)
                            xDoc.ChildNodes(1).ChildNodes(0).AppendChild(xDoc.ImportNode(SimItem, True))
                        Next
                    Next
                    'xDoc.Save("c:\TestXDocMarried.xml")
                    m_view.PopulateTreeView(xDoc)
                    Exit Sub
                End If
            End If

            'Call View Populate TreeView to populate the treeview with the search results
            'xDoc.Save("c:\TestXDocMarried.xml")
            If (strSearchType = CertificationNumberText) Then
                m_view.PopulateTreeForCertificateNumber(xDoc, dtbCertSearch)
            Else
                m_view.PopulateTreeView(xDoc)
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadFormDataText
        End Try

    End Sub

    ''' <summary>
    ''' Add certificate.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnAddCertificateSelected()
        Const LoadCertificateFormError As String = "Error loading add certificate form."
        Try
            m_view.CurrentAddCertificateName = m_view.AddCertificationName
            m_view.ShowAddCertification(True)
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = LoadCertificateFormError
        End Try
    End Sub

    ''' <summary>
    ''' Search tree leaf node selected.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnLeafNodeSelected()
        Const EmptySearchResultText As String = "Empty search result."
        Const ErrorLoadFormDataForSelectedCertText As String = "Error loading form data for selected certificate."
        Const NOMText As String = "NOM"
        Const ECE3054Text As String = "ECE3054"
        Const ECE117Text As String = "ECE117"
        Const MaterialNoText As String = "Material No."
        Const PSNText As String = "PSN"
        Const BrandText As String = "Brand"
        Const BatchNoText As String = "Batch No."
        Const SpecNoText As String = "Spec No."
        Const CCCText As String = "CCC"
        Const IndiaMarkText As String = "India_Mark"
        Const GSOText As String = "GSO"
        Const GeneralText As String = "GENERAL"

        Try
            Dim strSelectedSearchResult As String = String.Empty
            'Dim certSearch As CertificationSearch = New CertificationSearch()
            Select Case m_view.SearchType
                Case MaterialNoText, _
                     BrandText, _
                     PSNText, _
                     BatchNoText, _
                     SpecNoText
                    strSelectedSearchResult = m_view.SelectedSearchResultParentText

                    'Get the certification names for Add Certification drop-down list

                    Dim strSelectedSearchResultNew As String = CertTemplate(strSelectedSearchResult)
                    'if strselecteSerachResultNew comes back as null then reset it to the original.
                    'this is just checking if it contains a certification type and converts it to the generic if needed.
                    If strSelectedSearchResultNew = "" Then
                        strSelectedSearchResultNew = strSelectedSearchResult
                    End If

                    Select Case strSelectedSearchResultNew
                        Case String.Empty
                            m_view.ErrorText = EmptySearchResultText
                        Case ECE3054Text, _
                            ECE117Text, _
                            ImarkText, _
                            CCCText, _
                            IndiaMarkText, _
                            GSOText, _
                            NOMText, _
                            GeneralText

                            ' NOTE: New NOM certificates has NO customers - NO Customer leaves:
                            m_view.CurrentCertificateNumber = m_view.SelectedSearchResult
                            m_view.CurrentCertificateName = strSelectedSearchResult
                            m_view.CurrentCertificatonTypeID = CertTypeID(strSelectedSearchResult)

                            m_view.CurrentCertificateMaterialNum = m_view.SelectedSearchResultParentOfParentText
                            m_view.CurrentCertificateSKUID = m_view.SelectedSearchResultParentOfParentValue
                            m_view.CurrentCertificateCustomer = String.Empty
                            m_view.CurrentCertificateExtension = "*"

                            CertificationPresenterBase.FreshStart = True
                            m_view.ShowCurrentCertificate()
                            CertificationPresenterBase.FreshStart = False
                        Case Else

                            Dim strMviewSelectedSearchResultNew As String = CertTemplate(m_view.SelectedSearchResult)
                            'if strMviewSelectedSearchResultNew comes back as null then reset it to the original.
                            'this is just checking if it contains a certification type and converts it to the generic if needed.
                            If strMviewSelectedSearchResultNew = "" Then
                                strMviewSelectedSearchResultNew = m_view.SelectedSearchResult
                            End If


                            Select Case strMviewSelectedSearchResultNew
                                Case ECE3054Text, _
                                    ECE117Text, _
                                    ImarkText, _
                                    CCCText, _
                                    IndiaMarkText, _
                                    GSOText, _
                                    NOMText, _
                                    GeneralText

                                    m_view.CurrentCertificateNumber = String.Empty
                                    m_view.CurrentCertificateName = m_view.SelectedSearchResult
                                    m_view.CurrentCertificatonTypeID = CertTypeID(m_view.CurrentCertificateName)
                                    m_view.CurrentCertificateMaterialNum = m_view.SelectedSearchResultParentText
                                    m_view.CurrentCertificateSKUID = m_view.SelectedSearchResultParentValue
                                    m_view.CurrentCertificateCustomer = String.Empty
                                    m_view.CurrentCertificateExtension = m_view.ExtensionNo.ToString()

                                    CertificationPresenterBase.FreshStart = True
                                    m_view.ShowCurrentCertificate()
                                    CertificationPresenterBase.FreshStart = False
                                Case Else
                                    ' NOTE: only NOM certificates have Customer leaves:
                                    m_view.CurrentCertificateName = NOMText 'NameAid.Certification.NOM.ToString()
                                    m_view.CurrentCertificatonTypeID = 3
                                    m_view.CurrentCertificateNumber = m_view.SelectedSearchResultParentText
                                    m_view.CurrentCertificateMaterialNum = m_view.SelectedSearchResultParentOfParentOfParentText
                                    m_view.CurrentCertificateSKUID = m_view.SelectedSearchResultParentOfParentOfParentValue
                                    m_view.CurrentCertificateCustomer = m_view.SelectedSearchResult
                                    m_view.CurrentCertificateExtension = "*"

                                    CertificationPresenterBase.FreshStart = True
                                    m_view.ShowCurrentCertificate()
                                    CertificationPresenterBase.FreshStart = False
                            End Select
                    End Select
                Case Else
                    strSelectedSearchResult = m_view.SelectedSearchResult

                    Dim strSelectedSearchResultNew As String = CertTemplate(strSelectedSearchResult)
                    'if strselecteSerachResultNew comes back as null then reset it to the original.
                    'this is just checking if it contains a certification type and converts it to the generic if needed.
                    If strSelectedSearchResultNew = "" Then
                        strSelectedSearchResultNew = strSelectedSearchResult
                    End If
                    Select Case strSelectedSearchResultNew
                        Case String.Empty
                            m_view.ErrorText = EmptySearchResultText
                        Case ECE3054Text, _
                            ECE117Text, _
                            ImarkText, _
                            CCCText, _
                            GSOText, _
                            NOMText, _
                            IndiaMarkText, _
                            GeneralText

                            ' NOTE: New NOM certificates has NO customers - NO Customer leaves:
                            m_view.CurrentCertificateNumber = m_view.SelectedSearchResultParentOfParentText
                            m_view.CurrentCertificateName = strSelectedSearchResult
                            m_view.CurrentCertificatonTypeID = CertTypeID(m_view.CurrentCertificateName)
                            m_view.CurrentCertificateMaterialNum = m_view.SelectedSearchResultParentText
                            m_view.CurrentCertificateSKUID = m_view.SelectedSearchResultParentValue
                            m_view.CurrentCertificateCustomer = String.Empty
                            If (m_view.SearchType = CertificationNumberText) And (ECE3054Text = strSelectedSearchResult Or ECE117Text = strSelectedSearchResult) Then
                                m_view.CurrentCertificateExtension = m_view.SelectedSearchResultParentOfParentValue
                            Else
                                m_view.CurrentCertificateExtension = m_view.ExtensionNo.ToString()
                            End If

                            CertificationPresenterBase.FreshStart = True
                            m_view.ShowCurrentCertificate()
                            CertificationPresenterBase.FreshStart = False
                        Case Else
                            ' NOTE: only NOM certificates have Customer leaves:
                            m_view.CurrentCertificateNumber = m_view.SelectedSearchResultParentOfParentOfParentText
                            m_view.CurrentCertificateName = NOMText
                            m_view.CurrentCertificatonTypeID = 3
                            m_view.CurrentCertificateMaterialNum = m_view.SelectedSearchResultParentOfParentText
                            m_view.CurrentCertificateSKUID = m_view.SelectedSearchResultParentOfParentValue
                            m_view.CurrentCertificateCustomer = strSelectedSearchResult
                            m_view.CurrentCertificateExtension = m_view.ExtensionNo.ToString()

                            CertificationPresenterBase.FreshStart = True
                            m_view.ShowCurrentCertificate()
                            CertificationPresenterBase.FreshStart = False
                    End Select
            End Select

            m_view.ExpandSelectedNode(m_view.CurrentCertificateMaterialNum)

        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadFormDataForSelectedCertText
        End Try
    End Sub

    'Added as per project 2706 technical specification
    ''' <summary>
    ''' Called when exeute button is pressed.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnExecute()
        Const MaterialMaintText As String = "Material Maint"
        Const NoActionError As String = "No Action selected."
        Const NoCertificateTypeError As String = "No Certificate Type selected."
        Const NoMaterialError As String = "No Material Maint selected."
        Try
            'Get Certification Name and Action Name from the view and validate.
            Dim strCertName As String = m_view.AddCertificationName
            Dim strActionName As String = m_view.ActionName
            Dim strMaterialMaintName As String = m_view.MaterialMaintName
            If (strActionName = SelectText) Then
                m_view.ErrorText = NoActionError
                Return
            End If
            If (strActionName <> MaterialMaintText AndAlso strCertName = SelectText) Then
                m_view.ErrorText = NoCertificateTypeError
                Return
            ElseIf (strActionName = MaterialMaintText AndAlso strMaterialMaintName = SelectText) Then
                m_view.ErrorText = NoMaterialError
                Return
            End If
            Return
        Catch
            Throw
        End Try
    End Sub

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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub LoadBrands(ByVal p_strBrand As String)
        Const ErrorLoadBrandData As String = "Error loading Brand data."
        Try
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub LoadBrandLines(ByVal p_strLine As String)
        Const ErrorLoadBrandLine As String = "Error loading Brand Line data."
        Try
            'Get the Brand Line names for Add Brand Line drop-down list
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim listBrandLineNames As List(Of String) = certSearch.GetBrandLines(p_strLine)
            listBrandLineNames.Insert(0, SelectText)
            m_view.BrandLines = listBrandLineNames
            m_view.DataBindView()

        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadBrandLine
        End Try
    End Sub

    ''' <summary>
    ''' Certificate template.
    ''' </summary>
    ''' <param name="strCertificationName">Certification name</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function CertTemplate(ByVal strCertificationName As String) As String
        Dim certSearch As CertificationSearch = New CertificationSearch()
        Dim strCertTemplate As String
        Try
            strCertTemplate = certSearch.GetCertTemplate(strCertificationName)
            ' strCertTemplate is ""if certification name is not in CertificationType table
            Return strCertTemplate
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Certificate type Id.
    ''' </summary>
    ''' <param name="strCertificationName">Certification name</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function CertTypeID(ByVal strCertificationName As String) As Integer
        Dim objCert As CertificateModel = New CertificateModel()
        Dim intCertTypeID As String
        Try
            intCertTypeID = CStr(objCert.GetCertificateTypeID(strCertificationName))
            Return CInt(intCertTypeID)
        Catch
            Throw
        End Try        
    End Function
#End Region

End Class
