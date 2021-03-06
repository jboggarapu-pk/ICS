Imports System.Xml

Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' Implements ICertificationSearchView to present certification data
Partial Public Class CertificationSearchEx
    Inherits BasePage
    Implements ICertificationSearchView

    ' Added dropdown lists Brand and Brand line, changed material number instead of sku in tree view for all search types.
    ' Displayed the attributes for material number as tooltip.
    ' Search types changed from NPRID to PSN , SKU NO to Material no and Brand Code to Brand.
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Members"

    Private m_presenter As CertificationSearchPresenter
    'Private m_lstExpandedNodes As New List(Of Integer)

    Public Event Search As CustomEvents.PlainEventHandler Implements ICertificationSearchView.Search
    Public Event AddCertificateSelected As CustomEvents.PlainEventHandler Implements ICertificationSearchView.AddCertificateSelected
    Public Event LeafNodeSelected As CustomEvents.PlainEventHandler Implements ICertificationSearchView.LeafNodeSelected
    ' Added as per project 2706 technical specification
    Public Event Execute As CustomEvents.PlainEventHandler Implements ICertificationSearchView.Execute

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New CertificationSearchPresenter(Me)

    End Sub

#End Region

#Region "Properties"

    Public Property AddCertInfoText() As String Implements ICertificationSearchView.AddCertInfoText
        Get
            Return lblAddInfo.Text
        End Get
        Set(ByVal value As String)
            lblAddInfo.Text = value
        End Set
    End Property

    Public Property SearchCertInfoText() As String Implements ICertificationSearchView.SearchCertInfoText
        Get
            Return lblSearchInfo.Text
        End Get
        Set(ByVal value As String)
            lblSearchInfo.Text = value
        End Set
    End Property

    Public Property ErrorText() As String Implements ICertificationSearchView.ErrorText
        Get
            Return lblError.Text
        End Get
        Set(ByVal value As String)
            lblError.Text = value
        End Set
    End Property

    Private Property TreeViewVisibility() As Boolean
        Get
            Return treSearchTypeResults.Visible
        End Get
        Set(ByVal value As Boolean)
            treSearchTypeResults.Visible = value
        End Set
    End Property

    Public ReadOnly Property SelectedSearchResult() As String Implements ICertificationSearchView.SelectedSearchResult
        Get
            'Node text has the collor code on the text so we are going to use the value...
            Return treSearchTypeResults.SelectedNode.Value
        End Get
    End Property

    Public ReadOnly Property SelectedSearchResultParentText() As String Implements ICertificationSearchView.SelectedSearchResultParentText
        Get
            Dim str As String = String.Empty
            If Not treSearchTypeResults.SelectedNode.Parent Is Nothing Then
                str = treSearchTypeResults.SelectedNode.Parent.Text
            End If
            ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            Dim position As Integer
            position = str.IndexOf(" ")
            If (position > -1) Then
                Return str.Substring(0, position)
            Else
                Return str
            End If
        End Get
    End Property

    Public ReadOnly Property SelectedSearchResultParentValue() As String Implements ICertificationSearchView.SelectedSearchResultParentValue
        Get
            Dim str As String = String.Empty
            If Not treSearchTypeResults.SelectedNode.Parent Is Nothing Then
                str = treSearchTypeResults.SelectedNode.Parent.Value
            End If
            Return str
        End Get
    End Property

    Public ReadOnly Property SelectedSearchResultParentOfParentText() As String Implements Presenter.ICertificationSearchView.SelectedSearchResultParentOfParentText
        Get
            Dim str As String = String.Empty
            If treSearchTypeResults.SelectedNode.Parent IsNot Nothing AndAlso treSearchTypeResults.SelectedNode.Parent.Parent IsNot Nothing Then
                str = treSearchTypeResults.SelectedNode.Parent.Parent.Text
            End If
            ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            Dim position As Integer
            position = str.IndexOf(" ext") 'on a certificate node, check for ext to get the whole certificate - jeseitz 12/17/15
            If position > -1 Then
                Return str.Substring(0, position)
            Else
                'check for a material number line - this has material number, size, load, speed rating
                'the material number is either 11 or 13 digits long (depending if it is an SAP material number,
                'or a sku with 999 in front.
                position = str.IndexOf(" ")
                If (position = 11 And str.Substring(0, 3) = "900") Or _
                   (position = 13 And str.Substring(0, 3) = "999") Then

                    Return str.Substring(0, position)
                Else
                    Return str

                End If
            End If
        End Get
    End Property

    Public ReadOnly Property SelectedSearchResultParentOfParentValue() As String Implements Presenter.ICertificationSearchView.SelectedSearchResultParentOfParentValue
        Get
            Dim str As String = String.Empty
            If treSearchTypeResults.SelectedNode.Parent IsNot Nothing AndAlso treSearchTypeResults.SelectedNode.Parent.Parent IsNot Nothing Then
                str = treSearchTypeResults.SelectedNode.Parent.Parent.Value
            End If
            Return str
        End Get
    End Property

    Public ReadOnly Property SelectedSearchResultParentofParentofParentText() As String Implements Presenter.ICertificationSearchView.SelectedSearchResultParentOfParentOfParentText
        Get
            Dim str As String = String.Empty
            If treSearchTypeResults.SelectedNode.Parent.Parent IsNot Nothing AndAlso treSearchTypeResults.SelectedNode.Parent.Parent.Parent IsNot Nothing Then
                str = treSearchTypeResults.SelectedNode.Parent.Parent.Parent.Text
            End If
            ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            Dim position As Integer
            position = str.IndexOf(" ")
            If (position > -1) Then
                Return str.Substring(0, position)
            Else
                Return str
            End If
        End Get
    End Property

    Public ReadOnly Property SelectedSearchResultParentofParentofParentValue() As String Implements Presenter.ICertificationSearchView.SelectedSearchResultParentOfParentOfParentValue
        Get
            Dim str As String = String.Empty
            If treSearchTypeResults.SelectedNode.Parent.Parent IsNot Nothing AndAlso treSearchTypeResults.SelectedNode.Parent.Parent.Parent IsNot Nothing Then
                str = treSearchTypeResults.SelectedNode.Parent.Parent.Parent.Value
            End If
            Return str
        End Get
    End Property

    Property CertificationNames() As List(Of String) Implements ICertificationSearchView.CertificationNames
        Get
            Return ddlCertNames.DataSource
        End Get
        Set(ByVal value As List(Of String))
            ddlCertNames.DataSource = value
        End Set
    End Property

    ReadOnly Property ActionName() As String Implements ICertificationSearchView.ActionName
        Get
            Return ddlAction.SelectedItem.Text
        End Get
    End Property

    ReadOnly Property MaterialMaintName() As String Implements ICertificationSearchView.MaterialMaintName
        Get
            Return ddlMaterialMaint.SelectedItem.Text
        End Get
    End Property

    Property CerttificationSearchTypes() As List(Of String) Implements ICertificationSearchView.CerttificationSearchTypes
        Get
            Return ddlSearchTypes.DataSource
        End Get
        Set(ByVal value As List(Of String))
            ddlSearchTypes.DataSource = value
        End Set
    End Property

    ' Added Brands property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Property Brands() As List(Of String) Implements ICertificationSearchView.Brands
        Get
            Return ddlBrand.DataSource
        End Get
        Set(ByVal value As List(Of String))
            ddlBrand.DataSource = value
        End Set
    End Property

    ' Added BrandLines property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Property BrandLines() As List(Of String) Implements ICertificationSearchView.BrandLines
        Get
            Return ddlBrandLine.DataSource
        End Get
        Set(ByVal value As List(Of String))
            ddlBrandLine.DataSource = value
        End Set
    End Property

    Property SearchCriteria() As String Implements ICertificationSearchView.SearchCriteria
        Get
            If (SearchType = "Brand") Then
                Return Brand
            Else
                Return txtSearchFor.Text
            End If
        End Get
        Set(ByVal value As String)
            txtSearchFor.Text = value
        End Set
    End Property

    Property ExtensionNo() As String Implements ICertificationSearchView.ExtensionNo
        Get
            Return txtExtensionNo.Text
        End Get
        Set(ByVal value As String)
            txtExtensionNo.Text = value
        End Set
    End Property

    Property ImarkFamily() As String Implements ICertificationSearchView.ImarkFamily
        Get
            Return txtImarkFamily.Text
        End Get
        Set(ByVal value As String)
            txtImarkFamily.Text = value
        End Set
    End Property

    ReadOnly Property SearchType() As String Implements ICertificationSearchView.SearchType
        Get
            Return ddlSearchTypes.SelectedItem.Text
        End Get
    End Property

    ' Added Brand property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ReadOnly Property Brand() As String Implements ICertificationSearchView.Brand
        Get
            Return ddlBrand.SelectedItem.Text
        End Get
    End Property

    ' Added BrandLine property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ReadOnly Property BrandLine() As String Implements ICertificationSearchView.BrandLine
        Get
            If (SearchType = "Brand") Then
                If (ddlBrandLine.Items.Count > 0) Then
                    Return ddlBrandLine.SelectedItem.Text
                Else
                    Return String.Empty
                End If
            Else
                Return String.Empty
            End If
        End Get
    End Property
    Public ReadOnly Property AddCertificationName() As String Implements ICertificationSearchView.AddCertificationName
        Get
            Return ddlCertNames.SelectedItem.Text
        End Get
    End Property

    Public Property CurrentAddCertificateName() As String Implements ICertificationSearchView.CurrentAddCertificateName
        Get
            Return Session(Me.GetType().Name & "CurrentAddCertificateName")
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentAddCertificateName") = value
        End Set
    End Property

    Public Property CurrentCertificateNumber() As String Implements ICertificationSearchView.CurrentCertificateNumber
        Get
            Return Session(Me.GetType().Name & "CurrentCertificateNumber")
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentCertificateNumber") = value
        End Set
    End Property

    Public Property CurrentCertificateExtension() As String Implements ICertificationSearchView.CurrentCertificateExtension
        Get
            Return Session(Me.GetType().Name & "CurrentCertificateExtension")
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentCertificateExtension") = value
        End Set
    End Property

    Public Property CurrentCertificateName() As String Implements ICertificationSearchView.CurrentCertificateName
        Get
            Return Session(Me.GetType().Name & "CurrentCertificateName")
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentCertificateName") = value
        End Set
    End Property

    Public Property CurrentCertificationTypeID() As Integer Implements ICertificationSearchView.CurrentCertificatonTypeID
        Get
            Return Session(Me.GetType().Name & "CurrentCertificationTypeID")
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "CurrentCertificationTypeID") = value
        End Set
    End Property

    Public Property CurrentCertificateMaterialNum() As String Implements Presenter.ICertificationSearchView.CurrentCertificateMaterialNum
        Get
            Return Session(Me.GetType().Name & "CurrentCertificateMaterialNum")
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentCertificateMaterialNum") = value
        End Set
    End Property

    Public Property CurrentCertificateSKUID() As String Implements Presenter.ICertificationSearchView.CurrentCertificateSKUID
        Get
            Return Session(Me.GetType().Name & "CurrentCertificateSKUID")
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentCertificateSKUID") = value
        End Set
    End Property

    ''' <summary>
    ''' Specific to NOM certificate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CurrentCertificateCustomer() As String Implements Presenter.ICertificationSearchView.CurrentCertificateCustomer
        Get
            Return Session(Me.GetType().Name & "CurrentCertificateCustomer")
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentCertificateCustomer") = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Starts the search and raise event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_btnSearch(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Clear out existing certification number
        Dim tbGSOCertNo As TextBox = ucGSOCertification.FindControl("txtCertificationNo")
        Dim tbCCCCertNo As TextBox = ucCCCCertification.FindControl("txtCertificationNo")
        Dim tbE117CertNo As TextBox = ucEmark117Certification.FindControl("txtCertificationNo")
        Dim tbEmarkCertNo As TextBox = ucEmarkCertification.FindControl("txtCertificationNo")
        Dim tbImarkCertNo As TextBox = ucImarkCertification.FindControl("txtCertificationNo")
        Dim tbNOMCertNo As TextBox = ucNOMCertification.FindControl("txtCertificationNo")
        Dim tbGeneralCertNo As TextBox = ucGeneralCertification.FindControl("txtCertificationNo")
        Dim tbE117ExtNo As TextBox = ucEmark117Certification.FindControl("txtExtension")
        Dim tbEmarkExtNo As TextBox = ucEmarkCertification.FindControl("txtExtension")

        tbGSOCertNo.Text = String.Empty
        tbCCCCertNo.Text = String.Empty
        tbE117CertNo.Text = String.Empty
        tbEmarkCertNo.Text = String.Empty
        tbImarkCertNo.Text = String.Empty
        tbNOMCertNo.Text = String.Empty
        tbGeneralCertNo.Text = String.Empty
        tbEmarkExtNo.Text = String.Empty
        tbE117ExtNo.Text = String.Empty


        Me.ErrorText = String.Empty
        treSearchTypeResults.Nodes.Clear()
        'jeseitz 6/15/2016 - added 0 for seconde parameter - certificationtypeid not needed for this
        ActivateCertificateControl(String.Empty, 0)

        'Clear out tree view list
        'm_lstExpandedNodes.Clear()

        RaiseEvent Search()

    End Sub

    ''' <summary>
    ''' Raise Execute certification event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_btnExecute(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Cleanup
        ErrorText = String.Empty
        treSearchTypeResults.Nodes.Clear()
        ' Added as per project 2706 technical specification
        RaiseEvent Execute()
        'jeseitz 6/15/2016 - added 0 as second paraemeter - should be certificatiointypeid but not used in this case
        ActivateCertificateControl(String.Empty, 0)

        If ErrorText = String.Empty Then
            RaiseEvent AddCertificateSelected()
        End If

    End Sub

    ''' <summary>
    ''' Raise appropriate event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectedIndexChanged_ddlSearchTypes(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.treSearchTypeResults.Nodes.Clear()
        Me.SearchCriteria = String.Empty
        trBrand.Visible = False
        trBrandLine.Visible = False
        trSearch.Visible = True

        'Show extension field if search type selected is certifcate
        Select Case SearchType
            Case "Certification No."
                Extension.Visible = True
                txtExtensionNo.Visible = True
                lblExtInfo.Visible = True
                lblExtHighest.Visible = True
                txtExtensionNo.Text = "*"
                txtSearchFor.Text = String.Empty
                lblImarkFamily.Visible = False
                txtImarkFamily.Visible = False
                txtImarkFamily.Text = String.Empty
            Case "Imark"
                Extension.Visible = False
                txtExtensionNo.Visible = False
                lblExtInfo.Visible = False
                lblExtHighest.Visible = False
                txtExtensionNo.Text = "*"
                lblImarkFamily.Visible = True
                txtImarkFamily.Visible = True
                txtSearchFor.Text = "544" '"I033" jeseitz 4/12/16
                ' Added swith case for Brand as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
            Case "Brand"
                Extension.Visible = False
                txtExtensionNo.Visible = False
                lblExtInfo.Visible = False
                lblExtHighest.Visible = False
                txtExtensionNo.Text = "*"
                lblImarkFamily.Visible = False
                txtImarkFamily.Visible = False
                txtImarkFamily.Text = String.Empty
                txtSearchFor.Text = String.Empty
                trBrand.Visible = True
                trSearch.Visible = False
            Case Else
                Extension.Visible = False
                txtExtensionNo.Visible = False
                lblExtInfo.Visible = False
                lblExtHighest.Visible = False
                txtExtensionNo.Text = "*"
                lblImarkFamily.Visible = False
                txtImarkFamily.Visible = False
                txtImarkFamily.Text = String.Empty
                txtSearchFor.Text = String.Empty
        End Select
    End Sub

    ' Added SelectedIndexChanged_ddlBrand event as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Raise appropriate event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectedIndexChanged_ddlBrand(ByVal sender As Object, ByVal e As System.EventArgs)
        If (ddlBrand.SelectedItem.Text <> "Select ...") Then
            m_presenter.LoadBrandLines(Brand)
            trBrandLine.Visible = True
        Else
            trBrandLine.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Raise appropriate event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectedNodeChanged_treSearchTypeResults(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Clear out existing certification number
        Dim tbGSOCertNo As TextBox = ucGSOCertification.FindControl("txtCertificationNo")
        Dim tbCCCCertNo As TextBox = ucCCCCertification.FindControl("txtCertificationNo")
        Dim tbE117CertNo As TextBox = ucEmark117Certification.FindControl("txtCertificationNo")
        Dim tbEmarkCertNo As TextBox = ucEmarkCertification.FindControl("txtCertificationNo")
        Dim tbImarkCertNo As TextBox = ucImarkCertification.FindControl("txtCertificationNo")
        Dim tbNOMCertNo As TextBox = ucNOMCertification.FindControl("txtCertificationNo")
        Dim tbGeneralCertNo As TextBox = ucGeneralCertification.FindControl("txtCertificationNo")
        Dim tbE117ExtNo As TextBox = ucEmark117Certification.FindControl("txtExtension")
        Dim tbEmarkExtNo As TextBox = ucEmarkCertification.FindControl("txtExtension")

        tbGSOCertNo.Text = String.Empty
        tbCCCCertNo.Text = String.Empty
        tbE117CertNo.Text = String.Empty
        tbEmarkCertNo.Text = String.Empty
        tbImarkCertNo.Text = String.Empty
        tbNOMCertNo.Text = String.Empty
        tbGeneralCertNo.Text = String.Empty
        tbEmarkExtNo.Text = String.Empty
        tbE117ExtNo.Text = String.Empty

        If treSearchTypeResults.SelectedNode.ChildNodes.Count = 0 Then
            RaiseEvent LeafNodeSelected()
        End If

        'For i As Integer = 0 To treSearchTypeResults.Nodes.Count - 1
        '    For j As Integer = 0 To m_lstExpandedNodes.Count - 1
        '        If treSearchTypeResults.Nodes(i).Text = m_lstExpandedNodes.Item(j) Then
        '            treSearchTypeResults.Nodes(i).Expand()
        '        End If
        '    Next
        'Next

    End Sub

    ' Changed the PopulateTreeView method for displaying the material number in tree view and material attributes as tooltip.
    ''' <summary>
    ''' Populates the tree view.
    ''' </summary>
    ''' <param name="xDoc">The XMLDocument doc.</param>
    Public Sub PopulateTreeView(ByVal xDoc As XmlDocument) Implements ICertificationSearchView.PopulateTreeView

        Dim tvParent, tvMaterialNum, tvCertif, tvCertNo As TreeNode
        Dim strSearch As String

        treSearchTypeResults.Nodes.Clear()

        If xDoc.ChildNodes(1).ChildNodes.Count = 0 Then
            tvParent = New TreeNode("No data found")
            treSearchTypeResults.Nodes.Add(tvParent)
        Else
            If Me.SearchType.Equals("Material No.") Then
                tvParent = New TreeNode(Me.SearchType)
            ElseIf Me.SearchType.Equals("PSN") Then
                tvParent = New TreeNode("PSN")
            ElseIf Me.SearchType.Equals("Brand") Then
                strSearch = String.Format("{0} {1}", Me.SearchCriteria, Me.BrandLine)
                tvParent = New TreeNode(strSearch)
            Else
                tvParent = New TreeNode(Me.SearchCriteria)
            End If
            For Each xNode As XmlNode In xDoc.ChildNodes(1).ChildNodes
                If xNode.HasChildNodes Then
                    tvMaterialNum = New TreeNode()

                    Dim MaterialNo As String = String.Empty
                    Dim SingLoadIndex As String = String.Empty
                    Dim DualLoadIndex As String = String.Empty
                    Dim SpeedRating As String = String.Empty
                    Dim TireSize As String = String.Empty

                    MaterialNo = xNode.SelectSingleNode("MaterialNumber").InnerText
                    SingLoadIndex = xNode.SelectSingleNode("SINGLOADINDEX").InnerText
                    DualLoadIndex = xNode.SelectSingleNode("DUALLOADINDEX").InnerText
                    SpeedRating = xNode.SelectSingleNode("SPEEDRATING").InnerText
                    TireSize = xNode.SelectSingleNode("TireSize").InnerText

                    If (Not String.IsNullOrEmpty(DualLoadIndex) And DualLoadIndex <> "0") Then
                        tvMaterialNum.Text = String.Format("{0} {1} {2} / {3} {4}", MaterialNo.TrimStart("0"c), TireSize, SingLoadIndex, DualLoadIndex, SpeedRating)
                    Else
                        tvMaterialNum.Text = String.Format("{0} {1} {2} {3}", MaterialNo.TrimStart("0"c), TireSize, SingLoadIndex, SpeedRating)
                    End If

                    tvMaterialNum.ToolTip = xNode.SelectSingleNode("ToolTip").InnerText
                    tvMaterialNum.Value = xNode.FirstChild.Attributes("SKUID").InnerText

                    For Each Item As XmlNode In xNode.SelectNodes(NameAid.Column.CertificationName)
                        Select Case Me.SearchType
                            Case "Material No.", _
                                 "Brand", _
                                 "Batch No.", _
                                 "Spec No.", _
                                 "PSN"
                                If Not Item.FirstChild Is Nothing Then
                                    If Item.FirstChild.InnerText.ToUpper().Equals("NOM") Then
                                        'We have NOM certificate
                                        tvCertNo = New TreeNode
                                        If Item.SelectNodes("CertificateNo").Count > 0 Then
                                            For Each certNo As XmlNode In Item.SelectNodes("CertificateNo")
                                                Me.FillTreeNodeWithChildNOM(Item, certNo, tvCertNo, String.Empty)
                                            Next
                                            tvMaterialNum.ChildNodes.Add(tvCertNo)
                                        Else
                                            Me.FillTreeNode(Item, tvMaterialNum)
                                        End If
                                    Else
                                        tvCertNo = New TreeNode
                                        If Item.SelectNodes("CertificateNo").Count > 0 Then
                                            For Each certNo As XmlNode In Item.SelectNodes("CertificateNo")
                                                Me.FillTreeNodeWithChild(Item, certNo, tvCertNo, String.Empty)
                                            Next
                                            tvMaterialNum.ChildNodes.Add(tvCertNo)
                                        Else
                                            Me.FillTreeNode(Item, tvMaterialNum)
                                        End If
                                    End If
                                End If
                                'Treat the Nom certificate that might have customers
                                'If Not Item.FirstChild Is Nothing Then
                                '    If Item.FirstChild.InnerText.ToUpper().Equals("NOM") Then
                                '        If Item.SelectNodes("Customer").Count > 0 Then
                                '            If Item.SelectNodes("CertificateNo").Count > 0 Then
                                '                'tvCertNo = New TreeNode
                                '                'For Each certNo As XmlNode In Item.SelectNodes("CertificateNo")
                                '                '    Me.FillTreeNodeWithChild(Item, certNo, tvCertNo, String.Empty)
                                '                'Next

                                '                tvCertif = New TreeNode
                                '                For Each customer As XmlNode In Item.SelectNodes("Customer")
                                '                    'Me.FillTreeNodeNOM(customer, tvCertif)
                                '                    Me.FillTreeNodeWithChild(Item, customer, tvCertif, Item.FirstChild.InnerText.ToLower())
                                '                Next
                                '                'tvCertNo.ChildNodes.Add(tvCertif)
                                '                tvMaterialNum.ChildNodes.Add(tvCertif)
                                '                'tvCertNo.ChildNodes.Add(tvCertif)
                                '            Else
                                '                Me.FillTreeNode(Item, tvMaterial)
                                '            End If

                                '            'tvCertif = New TreeNode
                                '            'For Each customer As XmlNode In xNode.SelectNodes("Customer")
                                '            '    Me.FillTreeNodeWithChild(Item, customer, tvCertif, Item.FirstChild.InnerText.ToLower())
                                '            'Next
                                '            'tvMaterialNum.ChildNodes.Add(tvCertif)
                                '        Else
                                '            tvCertNo = New TreeNode
                                '            If Item.SelectNodes("CertificateNo").Count > 0 Then
                                '                For Each certNo As XmlNode In Item.SelectNodes("CertificateNo")
                                '                    Me.FillTreeNodeWithChild(Item, certNo, tvCertNo, String.Empty)
                                '                Next
                                '                tvMaterialNum.ChildNodes.Add(tvCertNo)
                                '            Else
                                '                Me.FillTreeNode(Item, tvMaterial)
                                '            End If
                                '        End If
                                '    Else
                                '        tvCertNo = New TreeNode
                                '        If Item.SelectNodes("CertificateNo").Count > 0 Then
                                '            For Each certNo As XmlNode In Item.SelectNodes("CertificateNo")
                                '                Me.FillTreeNodeWithChild(Item, certNo, tvCertNo, String.Empty)
                                '            Next
                                '            tvMaterialNum.ChildNodes.Add(tvCertNo)
                                '        Else
                                '            Me.FillTreeNode(Item, tvMaterial)
                                '        End If
                                '    End If
                                'End If
                            Case Else
                                'Treat the Nom certificate that might have customers
                                If Item.FirstChild.InnerText.ToLower().Equals("nom") Then
                                    If xNode.SelectNodes("Customer").Count > 0 Then
                                        tvCertif = New TreeNode
                                        For Each customer As XmlNode In xNode.SelectNodes("Customer")
                                            Me.FillTreeNodeWithChild(Item, customer, tvCertif, Item.InnerText.ToLower())
                                        Next
                                        tvMaterialNum.ChildNodes.Add(tvCertif)
                                    Else
                                        'Nom Certificate that has no custumer
                                        Me.FillTreeNode(Item, tvMaterialNum)
                                    End If
                                Else
                                    'All Other certifucation type requested that are not nom
                                    Me.FillTreeNode(Item, tvMaterialNum)
                                End If
                        End Select
                    Next
                    'Add`s node to the parent node.
                    tvParent.ChildNodes.Add(tvMaterialNum)
                End If
            Next
            treSearchTypeResults.Nodes.Add(tvParent)
            treSearchTypeResults.ShowLines = True
        End If

    End Sub

    Public Sub InitializeTreeView() Implements ICertificationSearchView.InitializeTreeView

        Me.TreeViewVisibility = True
        Me.treSearchTypeResults.Nodes.Clear()
        Dim tvMaterialNum As New TreeNode(String.Empty)
        Me.treSearchTypeResults.Nodes.Add(tvMaterialNum)
        Me.treSearchTypeResults.DataBind()

    End Sub

    Private Sub FillTreeNode(ByVal xNode As XmlNode, ByRef parentNode As TreeNode)

        Dim tvNode As New TreeNode()
        Dim sNodeText As String

        If xNode.Attributes("State").InnerText.Equals("Requested") Then
            sNodeText = GiveNodeTextRedColor(xNode.InnerText)
        ElseIf xNode.Attributes("State").InnerText.Equals("Approved") Then
            sNodeText = GiveNodeTextGreenColor(xNode.InnerText)
        ElseIf xNode.Attributes("State").InnerText.Equals("InProgress") Then
            sNodeText = GiveNodeTextYellowColor(xNode.InnerText)
        Else
            sNodeText = xNode.InnerText
        End If

        tvNode.Text = sNodeText
        tvNode.Value = xNode.InnerText

        parentNode.ChildNodes.Add(tvNode)

    End Sub

    Private Sub FillTreeNodeWithChild(ByVal xNode As XmlNode, ByVal xCustomer As XmlNode, ByRef CertifNode As TreeNode, ByVal CertifType As String)

        Dim tvNode As New TreeNode()
        Dim sNodeText As String = String.Empty
        Dim sNodeCustomer As String = String.Empty
        Dim sNodeCustomerText As String = String.Empty

        If xCustomer.InnerText.Length > 20 Then
            sNodeCustomer = String.Concat(xCustomer.InnerText.Substring(0, 20), "...")
        Else
            sNodeCustomer = xCustomer.InnerText
        End If

        If CertifType.Equals("nom") Then
            If xNode.Attributes("State").InnerText.Equals("Requested") Then
                sNodeText = GiveNodeTextRedColor(xNode.FirstChild.InnerText)
                sNodeCustomer = GiveNodeTextRedColor(sNodeCustomer)
            ElseIf xNode.Attributes("State").InnerText.Equals("Approved") Then
                sNodeText = GiveNodeTextGreenColor(xNode.FirstChild.InnerText)
                sNodeCustomer = GiveNodeTextGreenColor(sNodeCustomer)
            ElseIf xNode.Attributes("State").InnerText.Equals("InProgress") Then
                sNodeText = GiveNodeTextYellowColor(xNode.FirstChild.InnerText)
                sNodeCustomer = GiveNodeTextYellowColor(sNodeCustomer)
            Else
                sNodeText = xNode.FirstChild.InnerText
            End If
        Else
            If xCustomer.Attributes("State").InnerText.Equals("Requested") Then
                sNodeText = xNode.FirstChild.InnerText
                sNodeCustomer = GiveNodeTextRedColor(sNodeCustomer)
            ElseIf xCustomer.Attributes("State").InnerText.Equals("Approved") Then
                sNodeText = xNode.FirstChild.InnerText
                sNodeCustomer = GiveNodeTextGreenColor(sNodeCustomer)
            ElseIf xCustomer.Attributes("State").InnerText.Equals("InProgress") Then
                sNodeText = xNode.FirstChild.InnerText
                sNodeCustomer = GiveNodeTextYellowColor(sNodeCustomer)
            Else
                sNodeText = xNode.FirstChild.InnerText
                sNodeCustomer = xCustomer.InnerText
            End If
        End If

        tvNode.Text = sNodeCustomer
        tvNode.Value = xCustomer.InnerText
        tvNode.ToolTip = xCustomer.InnerText
        CertifNode.Text = sNodeText
        CertifNode.Value = xNode.InnerText

        CertifNode.ChildNodes.Add(tvNode)


    End Sub

    Private Sub FillTreeNodeWithChildNOM(ByVal xNode As XmlNode, ByVal xCustomer As XmlNode, ByRef CertifNode As TreeNode, ByVal CertifType As String)

        Dim tvNodeCert As New TreeNode()
        Dim tvNodeCust As New TreeNode()
        Dim sCertTypeNode As String = String.Empty
        Dim sCertNode As String = String.Empty
        Dim sNodeCustomer As String = String.Empty

        'If xNode.SelectNodes("Customer").Count > 0 Then
        If Not xCustomer.NextSibling Is Nothing Then
            If xCustomer.NextSibling.Name = "Customer" Then
                sCertTypeNode = xNode.FirstChild.InnerText
                sCertNode = xCustomer.InnerText

                tvNodeCert.Text = sCertNode
                tvNodeCert.Value = sCertNode

                'For Each customer As XmlNode In xNode.SelectNodes("Customer")
                sNodeCustomer = xCustomer.NextSibling.InnerText
                If xCustomer.Attributes("State").InnerText.Equals("Requested") Then
                    sNodeCustomer = GiveNodeTextRedColor(sNodeCustomer)
                ElseIf xCustomer.Attributes("State").InnerText.Equals("Approved") Then
                    sNodeCustomer = GiveNodeTextGreenColor(sNodeCustomer)
                ElseIf xCustomer.Attributes("State").InnerText.Equals("InProgress") Then
                    sNodeCustomer = GiveNodeTextYellowColor(sNodeCustomer)
                End If

                tvNodeCust.Text = sNodeCustomer
                tvNodeCust.Value = xCustomer.NextSibling.InnerText
                tvNodeCust.ToolTip = xCustomer.NextSibling.InnerText

                tvNodeCert.ChildNodes.Add(tvNodeCust)
                'Next


                CertifNode.Text = sCertTypeNode
                CertifNode.Value = sCertTypeNode

                CertifNode.ChildNodes.Add(tvNodeCert)
            Else
                'NOM does not have customers
                If xCustomer.Attributes("State").InnerText.Equals("Requested") Then
                    sCertTypeNode = xNode.FirstChild.InnerText
                    sCertNode = GiveNodeTextRedColor(xCustomer.InnerText)
                ElseIf xCustomer.Attributes("State").InnerText.Equals("Approved") Then
                    sCertTypeNode = xNode.FirstChild.InnerText
                    sCertNode = GiveNodeTextGreenColor(xCustomer.InnerText)
                ElseIf xCustomer.Attributes("State").InnerText.Equals("InProgress") Then
                    sCertTypeNode = xNode.FirstChild.InnerText
                    sCertNode = GiveNodeTextYellowColor(xCustomer.InnerText)
                Else
                    sCertTypeNode = xNode.FirstChild.InnerText
                    sCertNode = xCustomer.InnerText
                End If

                tvNodeCert.Text = sCertNode
                tvNodeCert.Value = xCustomer.InnerText
                tvNodeCert.ToolTip = xCustomer.InnerText
                CertifNode.Text = sCertTypeNode
                CertifNode.Value = sCertTypeNode

                CertifNode.ChildNodes.Add(tvNodeCert)
            End If
        Else
            'NOM does not have customers
            If xCustomer.Attributes("State").InnerText.Equals("Requested") Then
                sCertTypeNode = xNode.FirstChild.InnerText
                sCertNode = GiveNodeTextRedColor(xCustomer.InnerText)
            ElseIf xCustomer.Attributes("State").InnerText.Equals("Approved") Then
                sCertTypeNode = xNode.FirstChild.InnerText
                sCertNode = GiveNodeTextGreenColor(xCustomer.InnerText)
            ElseIf xCustomer.Attributes("State").InnerText.Equals("InProgress") Then
                sCertTypeNode = xNode.FirstChild.InnerText
                sCertNode = GiveNodeTextYellowColor(xCustomer.InnerText)
            Else
                sCertTypeNode = xNode.FirstChild.InnerText
                sCertNode = xCustomer.InnerText
            End If

            tvNodeCert.Text = sCertNode
            tvNodeCert.Value = xCustomer.InnerText
            tvNodeCert.ToolTip = xCustomer.InnerText
            CertifNode.Text = sCertTypeNode
            CertifNode.Value = sCertTypeNode

            CertifNode.ChildNodes.Add(tvNodeCert)
        End If

    End Sub

    Private Function GiveNodeTextYellowColor(ByVal nodeText As String) As String
        Dim strCollorIni As String = "<span style='color: orange;'>"
        Dim strCollorEnd As String = "</span'>"
        Return String.Concat(strCollorIni, nodeText, strCollorEnd)
    End Function

    Private Function GiveNodeTextGreenColor(ByVal nodeText As String) As String
        Dim strCollorIni As String = "<span style='color: green;'>"
        Dim strCollorEnd As String = "</span'>"
        Return String.Concat(strCollorIni, nodeText, strCollorEnd)
    End Function

    Private Function GiveNodeTextRedColor(ByVal nodeText As String) As String
        Dim strCollorIni As String = "<span style='color: red;'>"
        Dim strCollorEnd As String = "</span'>"
        Return String.Concat(strCollorIni, nodeText, strCollorEnd)
    End Function

    ''' <summary>
    ''' Show current certificate according search result selection
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowCurrentCertificate() Implements ICertificationSearchView.ShowCurrentCertificate

        ShowCertificate(CurrentCertificateName, currentcertificationtypeid, CurrentCertificateMaterialNum, CurrentCertificateSKUID, CurrentCertificateCustomer, CurrentCertificateNumber, CurrentCertificateExtension)

    End Sub

    ''' <summary>
    ''' Show certificate for a Material number
    ''' </summary>
    ''' <param name="p_strCertName"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strSkuId"></param>
    ''' <param name="p_strCustomer"></param>
    ''' <param name="p_strCertNumber"></param>
    ''' <remarks></remarks>
    Private Sub ShowCertificate(ByVal p_strCertName As String, ByVal p_intCertTypeID As Integer, ByVal p_strMatlNum As String, ByVal p_strSkuId As String, ByVal p_strCustomer As String, ByVal p_strCertNumber As String, ByVal p_strCertExtension As String)

        Dim ctlUC As Control = ActivateCertificateControl(p_strCertName, p_intCertTypeID)
        SetupCertificateControlProperties(ctlUC, p_strMatlNum, p_strSkuId, p_strCustomer, p_strCertNumber, p_strCertExtension)

    End Sub

    ''' <summary>
    ''' Activate (show) appropriate certificate control on the right panel
    ''' </summary>
    ''' <param name="p_strUCControlName">certification name or control type name (for the other)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ActivateCertificateControl(ByVal p_strUCControlName As String, ByVal p_intCertTypeID As Integer) As Control

        Dim ctrlUC As Control = Nothing
        Dim intIndexInMultiView As Integer = -1

        'check if certificationtype is a generic one.
        Dim strUCControlName As String
        strUCControlName = m_presenter.CertTemplate(p_strUCControlName)
        If strUCControlName Is "" Then
            strUCControlName = p_strUCControlName
        End If
        Select Case strUCControlName
            Case GetType(AddCertificationUC).Name
                intIndexInMultiView = 0
                ctrlUC = ucAddCertification
                'Added as per project 2706 technical specification
            Case GetType(RenameCertificationUC).Name
                intIndexInMultiView = 8
                ctrlUC = ucRenameCertification
            Case GetType(DeleteCertificationUC).Name
                intIndexInMultiView = 9
                ctrlUC = ucDeleteCertification
            Case GetType(DetachOrMoveCertificationUC).Name
                intIndexInMultiView = 10
                ctrlUC = ucDetachOrMoveCertification
            Case GetType(DupCorrectCertificationUC).Name
                intIndexInMultiView = 11
                ctrlUC = ucDupCorrectCertification
            Case "ECE3054"
                intIndexInMultiView = 1
                ctrlUC = ucEmarkCertification
            Case "ECE117"
                intIndexInMultiView = 6
                ctrlUC = ucEmark117Certification
            Case "GSO"
                intIndexInMultiView = 2
                ctrlUC = ucGSOCertification
            Case "NOM"
                intIndexInMultiView = 3
                ctrlUC = ucNOMCertification
            Case "Imark"
                intIndexInMultiView = 4
                ctrlUC = ucImarkCertification
            Case "CCC"
                intIndexInMultiView = 5
                ctrlUC = ucCCCCertification
            Case "India_Mark"
                intIndexInMultiView = 7
                ctrlUC = ucIndiaMarkCertification
            Case "GENERAL"
                intIndexInMultiView = 17
                ctrlUC = ucGeneralCertification
                Session(Me.GetType().Name & "GeneralCertificationTypeName") = p_strUCControlName
                Session(Me.GetType().Name & "GeneralCertificationTypeID") = p_intCertTypeID
            Case GetType(FamilyMaintenanceUC).Name
                intIndexInMultiView = 12
                ctrlUC = ucFamilyMaintenance
            Case GetType(CopyCertificationUC).Name
                intIndexInMultiView = 13
                ctrlUC = ucCopyCertification
            Case GetType(EditCertificationUC).Name
                intIndexInMultiView = 14
                ctrlUC = ucEditCertification
            Case GetType(AttachCertificationUC).Name
                intIndexInMultiView = 15
                ctrlUC = ucAttachCertification
            Case GetType(RefreshProductUC).Name
                intIndexInMultiView = 16
                ctrlUC = ucRefreshProduct

            Case Else
                intIndexInMultiView = -1
        End Select

        mvRight.ActiveViewIndex = intIndexInMultiView

        Return ctrlUC

    End Function

    ''' <summary>
    ''' Setup certificate control specific properties
    ''' </summary>
    ''' <param name="p_ctlCertUC"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strSKUID"></param>
    ''' <param name="p_strCustomer"></param>
    ''' <param name="p_strCertNumber"></param>
    ''' <remarks></remarks>
    Private Sub SetupCertificateControlProperties(ByVal p_ctlCertUC As Control, ByVal p_strMatlNum As String, ByVal p_strSKUID As String, ByVal p_strCustomer As String, ByVal p_strCertNumber As String, ByVal p_strCertExtension As String)

        If p_ctlCertUC Is Nothing Then
            Return
        End If

        Dim tbCertNo As TextBox = p_ctlCertUC.FindControl("txtCertificationNo")

        CType(p_ctlCertUC, ICertificationView).MaterialNumber = p_strMatlNum
        CType(p_ctlCertUC, ICertificationView).SKUID = CType(p_strSKUID, Integer)

        'GSO Certificate - Will change certification number
        If p_ctlCertUC.GetType().BaseType.Name = GetType(GSOCertificationUC).Name _
        Or p_ctlCertUC.GetType().BaseType.Name = GetType(Emark117CertificationUC).Name _
        Or p_ctlCertUC.GetType().BaseType.Name = GetType(CCCCertificationUC).Name _
        Or p_ctlCertUC.GetType().BaseType.Name = GetType(GeneralCertificationUC).Name Then
            If tbCertNo.Text.Length > 0 And tbCertNo.Text <> CurrentCertificateNumber Then
                CType(p_ctlCertUC, ICertificationView).CertificationNumber = tbCertNo.Text
            Else
                If CurrentCertificateNumber.Length > 0 Then
                    CType(p_ctlCertUC, ICertificationView).CertificationNumber = CurrentCertificateNumber
                Else
                    CType(p_ctlCertUC, ICertificationView).CertificationNumber = tbCertNo.Text
                End If
                'CType(p_ctlCertUC, ICertificationView).CertificationNumber = CurrentCertificateNumber
            End If
        Else
            'For now have the other certificates follow the same path
            If tbCertNo.Text.Length > 0 Then
                If CurrentCertificateNumber <> tbCertNo.Text Then
                    If CurrentCertificateNumber.Length > 0 Then
                        CType(p_ctlCertUC, ICertificationView).CertificationNumber = CurrentCertificateNumber
                    Else
                        CType(p_ctlCertUC, ICertificationView).CertificationNumber = tbCertNo.Text
                    End If
                Else
                    CType(p_ctlCertUC, ICertificationView).CertificationNumber = tbCertNo.Text
                End If
            Else
                CType(p_ctlCertUC, ICertificationView).CertificationNumber = p_strCertNumber
            End If
        End If

        'Extension Number - Emark3054 and E117
        If p_ctlCertUC.GetType().BaseType.Name = GetType(EmarkCertificationUC).Name Or p_ctlCertUC.GetType().BaseType.Name = GetType(Emark117CertificationUC).Name Or p_ctlCertUC.GetType().BaseType.Name = GetType(ImarkCertificationUC).Name Then
            Dim tbExtNo As TextBox = p_ctlCertUC.FindControl("txtExtension")

            If tbExtNo.Text.Length > 0 Then
                If CurrentCertificateExtension <> tbExtNo.Text Then
                    If CurrentCertificateExtension.Length > 0 Then
                        If CurrentCertificateExtension <> "*" AndAlso CurrentCertificateExtension <> "H" Then
                            CType(p_ctlCertUC, ICertificationView).ExtensionNo = CurrentCertificateExtension
                        Else
                            CType(p_ctlCertUC, ICertificationView).ExtensionNo = tbExtNo.Text
                        End If
                    Else
                        CType(p_ctlCertUC, ICertificationView).ExtensionNo = tbExtNo.Text
                    End If
                Else
                    CType(p_ctlCertUC, ICertificationView).ExtensionNo = tbExtNo.Text
                End If
            Else
                CType(p_ctlCertUC, ICertificationView).ExtensionNo = p_strCertExtension
            End If
            'Extension Number - NOM - NOT CURRENTLY NEEDED FOR NOM
        ElseIf p_ctlCertUC.GetType().BaseType.Name = GetType(NOMCertificationUC).Name Then
            Dim tbExtNo As TextBox = p_ctlCertUC.FindControl("txtExtensionOrRevision")

            If tbExtNo.Text.Length > 0 Then
                If CurrentCertificateExtension <> tbExtNo.Text Then
                    If CurrentCertificateExtension.Length > 0 Then
                        If CurrentCertificateExtension <> "*" AndAlso CurrentCertificateExtension <> "H" Then
                            CType(p_ctlCertUC, ICertificationView).ExtensionNo = CurrentCertificateExtension
                        Else
                            CType(p_ctlCertUC, ICertificationView).ExtensionNo = tbExtNo.Text
                        End If
                    Else
                        CType(p_ctlCertUC, ICertificationView).ExtensionNo = tbExtNo.Text
                    End If
                Else
                    CType(p_ctlCertUC, ICertificationView).ExtensionNo = tbExtNo.Text
                End If
            Else
                CType(p_ctlCertUC, ICertificationView).ExtensionNo = p_strCertExtension
            End If
        Else
            CType(p_ctlCertUC, ICertificationView).ExtensionNo = p_strCertExtension
        End If

        'Customer - NOM
        If p_ctlCertUC.GetType().BaseType.Name = GetType(NOMCertificationUC).Name Then
            CType(p_ctlCertUC, INOMCertificationView).CurrentCustomer = p_strCustomer
        End If

        CType(p_ctlCertUC, ICertificationView).DoLoadView()

    End Sub

    ''' <summary>
    ''' Show add certification user control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowAddCertification(ByVal p_blnNewAddCertView As Boolean) Implements Presenter.ICertificationSearchView.ShowAddCertification
        'Dim ucAddCert As AddCertificationUC  =ActivateCertificateControl(GetType(AddCertificationUC).Name)
        'Added as per project 2706 technical specification
        'jeseitz 6/15/16 - Added 0 as second parameter to ActivateCertifcateControl - it should be certificationtypeid, but it is not used for these UC's
        Dim strUsercontrolName As String = GetType(AddCertificationUC).Name
        Select Case ActionName
            Case "Add"
                strUsercontrolName = GetType(AddCertificationUC).Name
                Dim ucAddCert As AddCertificationUC = ActivateCertificateControl(strUsercontrolName, 0)
                SetupAddCertificateControlProperties(ucAddCert, p_blnNewAddCertView)
            Case "Rename"
                strUsercontrolName = GetType(RenameCertificationUC).Name
                Dim ucRenameCert As RenameCertificationUC = ActivateCertificateControl(strUsercontrolName, 0)
                SetupRenameCertificateControlProperties(ucRenameCert, p_blnNewAddCertView)
            Case "Delete"
                strUsercontrolName = GetType(DeleteCertificationUC).Name
                Dim ucDeleteCert As DeleteCertificationUC = ActivateCertificateControl(strUsercontrolName, 0)
                SetupDeleteCertificateControlProperties(ucDeleteCert, p_blnNewAddCertView)
            Case "Detach/Move"
                strUsercontrolName = GetType(DetachOrMoveCertificationUC).Name
                Dim ucDetachOrMoveCert As DetachOrMoveCertificationUC = ActivateCertificateControl(strUsercontrolName, 0)
                SetupDetachOrMoveCertificateControlProperties(ucDetachOrMoveCert, p_blnNewAddCertView)
            Case "Material Maint"
                Select Case MaterialMaintName
                    Case "Copy"
                        strUsercontrolName = GetType(CopyCertificationUC).Name
                        Dim ucCopyCert As CopyCertificationUC = ActivateCertificateControl(strUsercontrolName, 0)
                        SetupCopyCertificateControlProperties(ucCopyCert, p_blnNewAddCertView)
                    Case "Delete"
                        strUsercontrolName = GetType(DupCorrectCertificationUC).Name
                        Dim ucDupCorrectCert As DupCorrectCertificationUC = ActivateCertificateControl(strUsercontrolName, 0)
                        SetupDupCorrectCertificateControlProperties(ucDupCorrectCert, p_blnNewAddCertView)
                    Case "Edit"
                        strUsercontrolName = GetType(EditCertificationUC).Name
                        Dim ucEditCert As EditCertificationUC = ActivateCertificateControl(strUsercontrolName, 0)
                        SetupEditCertificateControlProperties(ucEditCert, p_blnNewAddCertView)
                    Case "Attach"
                        strUsercontrolName = GetType(AttachCertificationUC).Name
                        Dim ucAttachCert As AttachCertificationUC = ActivateCertificateControl(strUsercontrolName, 0)
                        SetupAttachCertificateControlProperties(ucAttachCert, p_blnNewAddCertView)
                    Case "Refresh Product Data"
                        strUsercontrolName = GetType(RefreshProductUC).Name
                        Dim ucRefreshProd As RefreshProductUC = ActivateCertificateControl(strUsercontrolName, 0)
                        SetupRefreshProductControlProperties(ucRefreshProd, p_blnNewAddCertView)
                End Select
            Case "Family Maintenance"
                strUsercontrolName = GetType(FamilyMaintenanceUC).Name
                Dim ucFamilyMaint As FamilyMaintenanceUC = ActivateCertificateControl(strUsercontrolName, 0)
                SetupFamilyMaintenanceControlProperties(ucFamilyMaint, p_blnNewAddCertView)
        End Select


    End Sub

    ''' <summary>
    ''' Setup add certificate view properties
    ''' </summary>
    ''' <param name="p_ctlAddCertUC"></param>
    ''' <param name="p_blnNewAddCertView"></param>
    ''' <remarks></remarks>
    Private Sub SetupAddCertificateControlProperties(ByVal p_ctlAddCertUC As AddCertificationUC, ByVal p_blnNewAddCertView As Boolean)

        If p_ctlAddCertUC Is Nothing Then
            Return
        End If

        p_ctlAddCertUC.SetupViewData(AddCertificationName, p_blnNewAddCertView)

    End Sub

    ''' <summary>
    ''' Setup rename certificate view properties
    ''' </summary>
    ''' <param name="p_ctlRenameCertUC"></param>
    ''' <param name="p_blnRenameCertView"></param>
    ''' <remarks></remarks>
    Private Sub SetupRenameCertificateControlProperties(ByVal p_ctlRenameCertUC As RenameCertificationUC, ByVal p_blnRenameCertView As Boolean)

        If p_ctlRenameCertUC Is Nothing Then
            Return
        End If

        p_ctlRenameCertUC.SetupViewData(AddCertificationName, p_blnRenameCertView)

    End Sub

    ''' <summary>
    ''' Setup delete certificate view properties
    ''' </summary>
    ''' <param name="p_ctlDeleteCertUC"></param>
    ''' <param name="p_blnDeleteCertView"></param>
    ''' <remarks></remarks>
    Private Sub SetupDeleteCertificateControlProperties(ByVal p_ctlDeleteCertUC As DeleteCertificationUC, ByVal p_blnDeleteCertView As Boolean)

        If p_ctlDeleteCertUC Is Nothing Then
            Return
        End If

        p_ctlDeleteCertUC.SetupViewData(AddCertificationName, p_blnDeleteCertView)

    End Sub

    ''' <summary>
    ''' Setup detach or move certificate view properties
    ''' </summary>
    ''' <param name="p_ctlDetachOrMoveCertUC"></param>
    ''' <param name="p_blnDetachOrMoveCertView"></param>
    ''' <remarks></remarks>
    Private Sub SetupDetachOrMoveCertificateControlProperties(ByVal p_ctlDetachOrMoveCertUC As DetachOrMoveCertificationUC, ByVal p_blnDetachOrMoveCertView As Boolean)

        If p_ctlDetachOrMoveCertUC Is Nothing Then
            Return
        End If

        p_ctlDetachOrMoveCertUC.SetupViewData(AddCertificationName, p_blnDetachOrMoveCertView)

    End Sub

    ''' <summary>
    ''' Setup dup correct certificate view properties
    ''' </summary>
    ''' <param name="p_ctlDupCorrectCertUC"></param>
    ''' <param name="p_blnDupCorrectCertView"></param>
    ''' <remarks></remarks>
    Private Sub SetupDupCorrectCertificateControlProperties(ByVal p_ctlDupCorrectCertUC As DupCorrectCertificationUC, ByVal p_blnDupCorrectCertView As Boolean)

        If p_ctlDupCorrectCertUC Is Nothing Then
            Return
        End If

        p_ctlDupCorrectCertUC.SetupViewData(AddCertificationName, p_blnDupCorrectCertView)

    End Sub


    ''' <summary>
    ''' Setup dup correct certificate view properties
    ''' </summary>
    ''' <param name="p_ctlCopyCertUC"></param>
    ''' <param name="p_blnDCopyCertView"></param>
    ''' <remarks></remarks>
    Private Sub SetupCopyCertificateControlProperties(ByVal p_ctlCopyCertUC As CopyCertificationUC, ByVal p_blnDCopyCertView As Boolean)
        If p_ctlCopyCertUC Is Nothing Then
            Return
        End If
        p_ctlCopyCertUC.SetupViewData(p_blnDCopyCertView)
    End Sub

    ''' <summary>
    ''' Setup dup correct certificate view properties
    ''' </summary>
    ''' <param name="p_ctlDupCorrectCertUC"></param>
    ''' <param name="p_blnDupCorrectCertView"></param>
    ''' <remarks></remarks>
    Private Sub SetupEditCertificateControlProperties(ByVal p_ctlDupCorrectCertUC As EditCertificationUC, ByVal p_blnDupCorrectCertView As Boolean)
        If p_ctlDupCorrectCertUC Is Nothing Then
            Return
        End If
        p_ctlDupCorrectCertUC.SetupViewData(p_blnDupCorrectCertView)
    End Sub

    ''' <summary>
    ''' Setup dup correct certificate view properties
    ''' </summary>
    ''' <param name="p_ucAttachCertUC"></param>
    ''' <param name="p_blnAttachCertView"></param>
    ''' <remarks></remarks>
    Private Sub SetupAttachCertificateControlProperties(ByVal p_ucAttachCertUC As AttachCertificationUC, ByVal p_blnAttachCertView As Boolean)

        If p_ucAttachCertUC Is Nothing Then
            Return
        End If
        p_ucAttachCertUC.SetupViewData(p_blnAttachCertView)
    End Sub

    ''' <summary>
    ''' Setup Refresh Product Control Properties
    ''' </summary>
    ''' <param name="p_ucRefreshProdUC">Refresh Product user control object</param>
    ''' <param name="p_blnRefreshProdView">Boolean Value</param>
    ''' <remarks></remarks>
    Private Sub SetupRefreshProductControlProperties(ByVal p_ucRefreshProdUC As RefreshProductUC, ByVal p_blnRefreshProdView As Boolean)
        If p_ucRefreshProdUC Is Nothing Then
            Return
        End If
        p_ucRefreshProdUC.SetupViewData(p_blnRefreshProdView)
    End Sub

    ''' <summary>
    ''' Setup family maintenance view properties
    ''' </summary>
    ''' <param name="p_ctlFamilyMaintUC"></param>
    ''' <param name="p_blnFamilyMaintView"></param>
    ''' <remarks></remarks>
    Private Sub SetupFamilyMaintenanceControlProperties(ByVal p_ctlFamilyMaintUC As FamilyMaintenanceUC, ByVal p_blnFamilyMaintView As Boolean)
        If p_ctlFamilyMaintUC Is Nothing Then
            Return
        End If

        p_ctlFamilyMaintUC.SetupViewData(AddCertificationName, p_blnFamilyMaintView)
    End Sub

    Private Sub ExpandSelectedNode(ByVal p_strMatlNum As String) Implements ICertificationSearchView.ExpandSelectedNode
        For Each node As TreeNode In treSearchTypeResults.Nodes(0).ChildNodes
            node.CollapseAll()
            If node.Text.Substring(0, 11).Equals(p_strMatlNum) Then
                node.ExpandAll()
            End If
        Next
    End Sub

    Protected Sub treSearchTypeResults_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Not treSearchTypeResults Is Nothing AndAlso Not treSearchTypeResults.SelectedNode Is Nothing Then
            If Not Me.SelectedSearchResultParentText Is Nothing Then
                Me.ExpandSelectedNode(Me.SelectedSearchResultParentText)
            End If


        End If

    End Sub

    'Private Sub treSearchTypeResults_TreeNodeCollapsed(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treSearchTypeResults.TreeNodeCollapsed
    '    'This event is called each time a tree node is collapsed

    '    'Update list to hold expanded nodes
    '    m_lstExpandedNodes.Remove(e.Node.Text)
    'End Sub

    'Private Sub treSearchTypeResults_TreeNodeExpanded(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treSearchTypeResults.TreeNodeExpanded
    '    'This event is called each time a tree node is expanded

    '    'Update list to hold expanded nodes
    '    m_lstExpandedNodes.Add(e.Node.)

    'End Sub


    ''' <summary>
    ''' Populates the tree view for Certification No search type
    ''' </summary>
    ''' <param name="xDoc">The XMLDocument doc.</param>
    Public Sub PopulateTreeForCertificateNumber(ByVal xDoc As XmlDocument, ByVal dtbCertNoSearch As DataTable) Implements ICertificationSearchView.PopulateTreeForCertificateNumber
        Dim tvParent, tvParentGSO, tvMaterialNum, tvCertif As TreeNode
        treSearchTypeResults.Nodes.Clear()

        If xDoc.ChildNodes(1).ChildNodes.Count = 0 Then
            tvParent = New TreeNode("No data found")
            treSearchTypeResults.Nodes.Add(tvParent)
        Else
            Dim count As Integer = 0
            Dim MaterialNo As String = String.Empty
            Dim SingLoadIndex As String = String.Empty
            Dim DualLoadIndex As String = String.Empty
            Dim SpeedRating As String = String.Empty
            Dim TireSize As String = String.Empty
            Dim SkuId As String = String.Empty
            Dim certificateId As String = String.Empty

            tvParent = New TreeNode(Me.SearchCriteria)
            tvParentGSO = New TreeNode(Me.SearchCriteria)

            Dim arrtvParentExt() As TreeNode = CreateExtensionNodes(dtbCertNoSearch)
            For Each xNode As XmlNode In xDoc.ChildNodes(1).ChildNodes
                If xNode.HasChildNodes Then
                    tvMaterialNum = New TreeNode()

                    SkuId = xNode.FirstChild.Attributes("SKUID").InnerText
                    MaterialNo = xNode.SelectSingleNode("MaterialNumber").InnerText
                    SingLoadIndex = xNode.SelectSingleNode("SINGLOADINDEX").InnerText
                    DualLoadIndex = xNode.SelectSingleNode("DUALLOADINDEX").InnerText
                    SpeedRating = xNode.SelectSingleNode("SPEEDRATING").InnerText
                    TireSize = xNode.SelectSingleNode("TireSize").InnerText
                    certificateId = xNode.SelectSingleNode("CERTIFICATEID").InnerText

                    If (Not String.IsNullOrEmpty(DualLoadIndex) And DualLoadIndex <> "0") Then
                        tvMaterialNum.Text = String.Format("{0} {1} {2} / {3} {4}", MaterialNo.TrimStart("0"c), TireSize, SingLoadIndex, DualLoadIndex, SpeedRating)
                    Else
                        tvMaterialNum.Text = String.Format("{0} {1} {2} {3}", MaterialNo.TrimStart("0"c), TireSize, SingLoadIndex, SpeedRating)
                    End If

                    tvMaterialNum.ToolTip = xNode.SelectSingleNode("ToolTip").InnerText
                    tvMaterialNum.Value = xNode.FirstChild.Attributes("SKUID").InnerText
                    count = 0
                    For Each Item As XmlNode In xNode.SelectNodes(NameAid.Column.CertificationName)
                        count = count + 1
                        If Item.FirstChild.InnerText.ToLower().Equals("nom") Then
                            If xNode.SelectNodes("Customer").Count > 0 Then
                                tvCertif = New TreeNode
                                For Each customer As XmlNode In xNode.SelectNodes("Customer")
                                    Me.FillTreeNodeWithChild(Item, customer, tvCertif, Item.InnerText.ToLower())
                                Next
                                tvMaterialNum.ChildNodes.Add(tvCertif)
                            Else
                                If (count = 1) Then
                                    Me.FillTreeNode(Item, tvMaterialNum)
                                Else
                                    If (CheckNodeExists(tvMaterialNum, Item.InnerText) = False) Then
                                        Me.FillTreeNode(Item, tvMaterialNum)
                                    End If
                                End If
                            End If
                        Else
                            If (count = 1) Then
                                Me.FillTreeNode(Item, tvMaterialNum)
                            Else
                                If (CheckNodeExists(tvMaterialNum, Item.InnerText) = False) Then
                                    Me.FillTreeNode(Item, tvMaterialNum)
                                End If
                            End If
                        End If
                    Next
                    If (dtbCertNoSearch.Rows(0)("CERTIFICATIONID") = 1 Or dtbCertNoSearch.Rows(0)("CERTIFICATIONID") = 6) Then
                        Dim ExtNo As Integer = 0
                        tvParent = FindExtensionNode(SkuId, certificateId, dtbCertNoSearch, arrtvParentExt, ExtNo)
                        tvParent.ChildNodes.Add(tvMaterialNum)
                        tvParent.Value = ExtNo
                    Else
                        tvParentGSO.ChildNodes.Add(tvMaterialNum)
                    End If

                End If
            Next

            If (dtbCertNoSearch.Rows(0)("CERTIFICATIONID") = 1 Or dtbCertNoSearch.Rows(0)("CERTIFICATIONID") = 6) Then
                AddExtensionNodes(arrtvParentExt)
            Else
                treSearchTypeResults.Nodes.Add(tvParentGSO)
            End If
            treSearchTypeResults.ShowLines = True
        End If

    End Sub

    ''' <summary>
    ''' Create extension Nodes for Certificate Number Search Type
    ''' </summary>
    ''' <param name="dtCertificate"></param>
    ''' <returns>XML Node</returns>
    ''' <remarks></remarks>
    Private Function CreateExtensionNodes(ByVal dtCertificate As DataTable) As TreeNode()
        Dim dtbExtension As DataTable = dtCertificate.DefaultView.ToTable(True, "EXTENSION")
        Dim extensionNodes(dtbExtension.Rows.Count - 1) As TreeNode
        Dim strCertificateNumber As String = dtCertificate.Rows(0)("CERTIFICATENUMBER").ToString()

        Dim iIndex As Integer = 0
        For iIndex = 0 To dtbExtension.Rows.Count - 1
            Dim intExtention As Integer = CInt(dtbExtension.Rows(iIndex)("EXTENSION"))
            Dim tvParentExt As New TreeNode(String.Format("{0} ext {1}", strCertificateNumber, intExtention.ToString()))
            extensionNodes(iIndex) = tvParentExt
        Next

        Return extensionNodes
    End Function

    ''' <summary>
    ''' Find extension Node
    ''' </summary>
    ''' <param name="skuId"></param>
    ''' <param name="certId"></param>
    ''' <param name="dtbCertificate"></param>
    ''' <param name="extensionNodes"></param>
    ''' <param name="extension"></param>
    ''' <returns>TreeNode</returns>
    ''' <remarks></remarks>
    Private Function FindExtensionNode(ByVal skuId As Integer, ByVal certId As Integer, ByVal dtbCertificate As DataTable, ByVal extensionNodes() As TreeNode, ByRef extension As Integer) As TreeNode
        Dim strExtension As String = String.Format("SKUID={0} And CERTIFICATEID={1}", skuId, certId)
        extension = CInt(dtbCertificate.Select(strExtension)(0)("EXTENSION"))
        Dim index As Integer = 0

        For index = 0 To extensionNodes.Length - 1
            If (extensionNodes(index).Text = String.Format("{0} ext {1}", dtbCertificate.Rows(0)("CERTIFICATENUMBER").ToString(), extension.ToString())) Then
                Return extensionNodes(index)
                Exit For
            End If
        Next

        Return New TreeNode(dtbCertificate.Rows(0)("CERTIFICATENUMBER").ToString())
    End Function

    ''' <summary>
    ''' Check if node already exists in the tree
    ''' </summary>
    ''' <param name="tvMaterialNum"></param>
    ''' <param name="nodeValue"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function CheckNodeExists(ByVal tvMaterialNum As TreeNode, ByVal nodeValue As String) As Boolean
        Try
            If tvMaterialNum.ChildNodes.Count <= 0 Or String.IsNullOrEmpty(nodeValue) Then
                Return False
            End If

            Dim index As Integer = 0
            For index = 0 To tvMaterialNum.ChildNodes.Count - 1
                If (tvMaterialNum.ChildNodes(index).Value = nodeValue) Then
                    Return True
                End If
            Next

            Return False
        Catch ex As Exception
            Return True
        End Try
    End Function

    ''' <summary>
    ''' Append Extension nodes to Tree View
    ''' </summary>
    ''' <param name="extensionNodes"></param>
    ''' <remarks></remarks>
    Private Sub AddExtensionNodes(ByVal extensionNodes() As TreeNode)
        Dim nodeIndex As Integer = 0
        Dim extensionIndex As Integer = 0
        Dim extention As String = String.Empty
        Dim extensions(extensionNodes.Length - 1) As Integer

        For extensionIndex = 0 To extensions.Length - 1
            extensions(extensionIndex) = extensionNodes(extensionIndex).Value
        Next
        Array.Sort(extensions)
       
        For extensionIndex = 0 To extensions.Length - 1
            extention = extensions(extensionIndex).ToString()
            For nodeIndex = 0 To extensionNodes.Length - 1
                If (extensionNodes(nodeIndex).Value.ToString() = extention) Then
                    If Not (treSearchTypeResults.Nodes.Contains(extensionNodes(nodeIndex))) Then
                        treSearchTypeResults.Nodes.Add(extensionNodes(nodeIndex))
                        Exit For
                    End If
                End If
            Next
        Next

    End Sub

#End Region


End Class
