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

#Region "Members and Constants"
    ''' <summary>
    '''  Certification search view presenter
    ''' </summary>
    Private m_presenter As CertificationSearchPresenter
    'Private m_lstExpandedNodes As New List(Of Integer)
    ''' <summary>
    '''  Search event
    ''' </summary>
    Public Event Search As CustomEvents.PlainEventHandler Implements ICertificationSearchView.Search
    ''' <summary>
    '''  Add certificate selected event
    ''' </summary>
    Public Event AddCertificateSelected As CustomEvents.PlainEventHandler Implements ICertificationSearchView.AddCertificateSelected
    ''' <summary>
    '''  Leaf node selected event
    ''' </summary>
    Public Event LeafNodeSelected As CustomEvents.PlainEventHandler Implements ICertificationSearchView.LeafNodeSelected
    ' Added as per project 2706 technical specification
    ''' <summary>
    '''  Execute event
    ''' </summary>
    Public Event Execute As CustomEvents.PlainEventHandler Implements ICertificationSearchView.Execute
    ''' <summary>
    '''  Constant to hold Certification Number text
    ''' </summary>
    Private Const CertificationNoText As String = "txtCertificationNo"
    ''' <summary>
    '''  Constant to hold Extension text
    ''' </summary>
    Private Const ExtensionText As String = "txtExtension"
    ''' <summary>
    '''  Constant to hold No Data Found text
    ''' </summary>
    Private Const NoDataFoundText As String = "No data found"
    ''' <summary>
    '''  Constant to hold Brand text
    ''' </summary>
    Private Const BrandText As String = "Brand"
    ''' <summary>
    '''  Constant to hold Material Number text
    ''' </summary>
    Private Const MaterialNumberText As String = "MaterialNumber"
    ''' <summary>
    '''  Constant to hold Single Load Index text
    ''' </summary>
    Private Const SingleLoadIndexText As String = "SINGLOADINDEX"
    ''' <summary>
    '''  Constant to hold Dual Load Index text
    ''' </summary>
    Private Const DualLoadIndexText As String = "DUALLOADINDEX"
    ''' <summary>
    '''  Constant to hold Speed Rating text
    ''' </summary>
    Private Const SpeedRatingText As String = "SPEEDRATING"
    ''' <summary>
    '''  Constant to hold Tire Size text
    ''' </summary>
    Private Const TireSizeText As String = "TireSize"
    ''' <summary>
    '''  Constant to hold SKU Id text
    ''' </summary>
    Private Const SKUIdText As String = "SKUID"
    ''' <summary>
    '''  Constant to hold SKU Id text
    ''' </summary>
    Private Const CertificateNo As String = "CertificateNo"
    ''' <summary>
    '''  Constant to hold Nom text
    ''' </summary>
    Private Const Nom As String = "nom"
    ''' <summary>
    '''  Constant to hold Customer text
    ''' </summary>
    Private Const CustomerText As String = "Customer"
    ''' <summary>
    '''  Constant to hold State text
    ''' </summary>
    Private Const StateText As String = "State"
    ''' <summary>
    '''  Constant to hold Requested text
    ''' </summary>
    Private Const RequestedText As String = "Requested"
    ''' <summary>
    '''  Constant to hold Approved text
    ''' </summary>
    Private Const ApprovedText As String = "Approved"
    ''' <summary>
    '''  Constant to hold InProgress text
    ''' </summary>
    Private Const InProgressText As String = "InProgress"
    ''' <summary>
    '''  Constant to hold ExtensionOrRevision text
    ''' </summary>
    Private Const ExtensionOrRevisionText As String = "txtExtensionOrRevision"
    ''' <summary>
    '''  Constant to hold ToolTip text
    ''' </summary>
    Private Const ToolTipText As String = "ToolTip"
    ''' <summary>
    '''  Constant to hold Extension
    ''' </summary>
    Private Const ExtensionTxt As String = "EXTENSION"
    ''' <summary>
    '''  Constant to hold CertificateNumber Text
    ''' </summary>
    Private Const CertificateNumberText As String = "CERTIFICATENUMBER"
    ''' <summary>
    '''  Constant to hold NOM Text
    ''' </summary>
    Private Const NOMText As String = "NOM"
    ''' <summary>
    '''  Constant to hold Imark Text
    ''' </summary>
    Private Const ImarkText As String = "Imark"
    ''' <summary>
    '''  Constant to hold * text
    ''' </summary>
    Private Const Text As String = "*"
#End Region

#Region "Constructors"

    ''' <summary>
    '''  Default Constructor to initialize class members.
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New CertificationSearchPresenter(Me)

    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    '''  Gets or sets Add certifiate info text value.
    ''' </summary>
    ''' <returns>
    ''' Add info text 
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property AddCertInfoText() As String Implements ICertificationSearchView.AddCertInfoText
        Get
            Return lblAddInfo.Text
        End Get
        Set(ByVal value As String)
            lblAddInfo.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Search certifiate info text value.
    ''' </summary>
    ''' <returns>
    ''' Search info text 
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property SearchCertInfoText() As String Implements ICertificationSearchView.SearchCertInfoText
        Get
            Return lblSearchInfo.Text
        End Get
        Set(ByVal value As String)
            lblSearchInfo.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Error text value.
    ''' </summary>
    ''' <returns>
    ''' Error text 
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements ICertificationSearchView.ErrorText
        Get
            Return lblError.Text
        End Get
        Set(ByVal value As String)
            lblError.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Tree view visibility value.
    ''' </summary>
    ''' <returns>
    ''' Tree search type results value
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Property TreeViewVisibility() As Boolean
        Get
            Return treSearchTypeResults.Visible
        End Get
        Set(ByVal value As Boolean)
            treSearchTypeResults.Visible = value
        End Set
    End Property

    ''' <summary>
    '''  Gets Selected search result value.
    ''' </summary>
    ''' <returns>
    ''' Tree search type results value
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property SelectedSearchResult() As String Implements ICertificationSearchView.SelectedSearchResult
        Get
            'Node text has the collor code on the text so we are going to use the value...
            Return treSearchTypeResults.SelectedNode.Value
        End Get
    End Property

    ''' <summary>
    '''  Gets Selected search result parent text value.
    ''' </summary>
    ''' <returns>
    ''' Tree search type results text
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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

    ''' <summary>
    '''  Gets Selected search result parent value value.
    ''' </summary>
    ''' <returns>
    ''' Tree search type results parent value
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property SelectedSearchResultParentValue() As String Implements ICertificationSearchView.SelectedSearchResultParentValue
        Get
            Dim str As String = String.Empty
            If Not treSearchTypeResults.SelectedNode.Parent Is Nothing Then
                str = treSearchTypeResults.SelectedNode.Parent.Value
            End If
            Return str
        End Get
    End Property

    ''' <summary>
    '''  Gets Selected search result parent of parent text value.
    ''' </summary>
    ''' <returns>
    ''' Tree search type results parent parent text
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property SelectedSearchResultParentOfParentText() As String Implements Presenter.ICertificationSearchView.SelectedSearchResultParentOfParentText
        Get
            Dim str As String = String.Empty
            Const ExtText As String = " ext"
            Const Text900 As String = "900"
            Const Text999 As String = "999"

            If treSearchTypeResults.SelectedNode.Parent IsNot Nothing AndAlso treSearchTypeResults.SelectedNode.Parent.Parent IsNot Nothing Then
                str = treSearchTypeResults.SelectedNode.Parent.Parent.Text
            End If
            ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
            Dim position As Integer
            position = str.IndexOf(ExtText) 'on a certificate node, check for ext to get the whole certificate - jeseitz 12/17/15
            If position > -1 Then
                Return str.Substring(0, position)
            Else
                'check for a material number line - this has material number, size, load, speed rating
                'the material number is either 11 or 13 digits long (depending if it is an SAP material number,
                'or a sku with 999 in front.
                position = str.IndexOf(" ")
                If (position = 11 And str.Substring(0, 3) = Text900) Or _
                   (position = 13 And str.Substring(0, 3) = Text999) Then

                    Return str.Substring(0, position)
                Else
                    Return str

                End If
            End If
        End Get
    End Property

    ''' <summary>
    '''  Gets Selected search result parent of parent value.
    ''' </summary>
    ''' <returns>
    ''' Tree search type results  parent parent value
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property SelectedSearchResultParentOfParentValue() As String Implements Presenter.ICertificationSearchView.SelectedSearchResultParentOfParentValue
        Get
            Dim str As String = String.Empty
            If treSearchTypeResults.SelectedNode.Parent IsNot Nothing AndAlso treSearchTypeResults.SelectedNode.Parent.Parent IsNot Nothing Then
                str = treSearchTypeResults.SelectedNode.Parent.Parent.Value
            End If
            Return str
        End Get
    End Property

    ''' <summary>
    '''  Gets Selected search result parent of parent of parent of parent text value.
    ''' </summary>
    ''' <returns>
    ''' Tree search type results parent parent parent text
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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

    ''' <summary>
    '''  Gets Selected search result parent of parent of parent of parent text value.
    ''' </summary>
    ''' <returns>
    ''' Tree search type results parent parent parent text
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property SelectedSearchResultParentofParentofParentValue() As String Implements Presenter.ICertificationSearchView.SelectedSearchResultParentOfParentOfParentValue
        Get
            Dim str As String = String.Empty
            If treSearchTypeResults.SelectedNode.Parent.Parent IsNot Nothing AndAlso treSearchTypeResults.SelectedNode.Parent.Parent.Parent IsNot Nothing Then
                str = treSearchTypeResults.SelectedNode.Parent.Parent.Parent.Value
            End If
            Return str
        End Get
    End Property

    ''' <summary>
    '''  Gets or sets Certification Names value.
    ''' </summary>
    ''' <returns>
    ''' certificate names
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property CertificationNames() As List(Of String) Implements ICertificationSearchView.CertificationNames
        Get
            Return CType(ddlCertNames.DataSource, Global.System.Collections.Generic.List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            ddlCertNames.DataSource = value
        End Set
    End Property

    ''' <summary>
    '''  Gets Action name value.
    ''' </summary>
    ''' <returns>
    ''' selected item text
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    ReadOnly Property ActionName() As String Implements ICertificationSearchView.ActionName
        Get
            Return ddlAction.SelectedItem.Text
        End Get
    End Property

    ''' <summary>
    '''  Gets Material Maintenance name value.
    ''' </summary>
    ''' <returns>
    ''' selected item text
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    ReadOnly Property MaterialMaintName() As String Implements ICertificationSearchView.MaterialMaintName
        Get
            Return ddlMaterialMaint.SelectedItem.Text
        End Get
    End Property

    ''' <summary>
    '''  Gets or sets Certification search type value.
    ''' </summary>
    ''' <returns>
    ''' search type value
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property CerttificationSearchTypes() As List(Of String) Implements ICertificationSearchView.CerttificationSearchTypes
        Get
            Return CType(ddlSearchTypes.DataSource, Global.System.Collections.Generic.List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            ddlSearchTypes.DataSource = value
        End Set
    End Property

    ' Added Brands property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    '''  Gets or sets Brands value.
    ''' </summary>
    ''' <returns>
    ''' Brand value
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property Brands() As List(Of String) Implements ICertificationSearchView.Brands
        Get
            Return CType(ddlBrand.DataSource, Global.System.Collections.Generic.List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            ddlBrand.DataSource = value
        End Set
    End Property

    ' Added BrandLines property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    '''  Gets or sets Brand lines value.
    ''' </summary>
    ''' <returns>
    ''' Brand line value
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property BrandLines() As List(Of String) Implements ICertificationSearchView.BrandLines
        Get
            Return CType(ddlBrandLine.DataSource, Global.System.Collections.Generic.List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            ddlBrandLine.DataSource = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Search criteria value.
    ''' </summary>
    ''' <returns>
    ''' search text
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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

    ''' <summary>
    '''  Gets or sets Extension number value.
    ''' </summary>
    ''' <returns>
    ''' Extension number text
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property ExtensionNo() As String Implements ICertificationSearchView.ExtensionNo
        Get
            Return txtExtensionNo.Text
        End Get
        Set(ByVal value As String)
            txtExtensionNo.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Imark family value.
    ''' </summary>
    ''' <returns>
    ''' Imark family text
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property ImarkFamily() As String Implements ICertificationSearchView.ImarkFamily
        Get
            Return txtImarkFamily.Text
        End Get
        Set(ByVal value As String)
            txtImarkFamily.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets Search type value.
    ''' </summary>
    ''' <returns>
    ''' selected item text
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    ReadOnly Property SearchType() As String Implements ICertificationSearchView.SearchType
        Get
            Return ddlSearchTypes.SelectedItem.Text
        End Get
    End Property

    ' Added Brand property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    '''  Gets Brand value.
    ''' </summary>
    ''' <returns>
    ''' selected text
    ''' </returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    ReadOnly Property Brand() As String Implements ICertificationSearchView.Brand
        Get
            Return ddlBrand.SelectedItem.Text
        End Get
    End Property

    ' Added BrandLine property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    '''  Gets Brand line value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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

    ''' <summary>
    '''  Gets Add certification name value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property AddCertificationName() As String Implements ICertificationSearchView.AddCertificationName
        Get
            Return ddlCertNames.SelectedItem.Text
        End Get
    End Property

    ''' <summary>
    '''  Gets Current add certificate name value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CurrentAddCertificateName() As String Implements ICertificationSearchView.CurrentAddCertificateName
        Get
            Return CStr(Session(Me.GetType().Name & "CurrentAddCertificateName"))
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentAddCertificateName") = value
        End Set
    End Property

    ''' <summary>
    '''  Gets Current certificate number value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CurrentCertificateNumber() As String Implements ICertificationSearchView.CurrentCertificateNumber
        Get
            Return CStr(Session(Me.GetType().Name & "CurrentCertificateNumber"))
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentCertificateNumber") = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Current certificate extension value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CurrentCertificateExtension() As String Implements ICertificationSearchView.CurrentCertificateExtension
        Get
            Return CStr(Session(Me.GetType().Name & "CurrentCertificateExtension"))
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentCertificateExtension") = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Current certificate name value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CurrentCertificateName() As String Implements ICertificationSearchView.CurrentCertificateName
        Get
            Return CStr(Session(Me.GetType().Name & "CurrentCertificateName"))
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentCertificateName") = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Current certificate type id value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CurrentCertificationTypeID() As Integer Implements ICertificationSearchView.CurrentCertificatonTypeID
        Get
            Return CInt(Session(Me.GetType().Name & "CurrentCertificationTypeID"))
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "CurrentCertificationTypeID") = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Current certificate material number value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CurrentCertificateMaterialNum() As String Implements Presenter.ICertificationSearchView.CurrentCertificateMaterialNum
        Get
            Return CStr(Session(Me.GetType().Name & "CurrentCertificateMaterialNum"))
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentCertificateMaterialNum") = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Current certificate SKU Id value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CurrentCertificateSKUID() As String Implements Presenter.ICertificationSearchView.CurrentCertificateSKUID
        Get
            Return CStr(Session(Me.GetType().Name & "CurrentCertificateSKUID"))
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "CurrentCertificateSKUID") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Specific to NOM certificate value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CurrentCertificateCustomer() As String Implements Presenter.ICertificationSearchView.CurrentCertificateCustomer
        Get
            Return CStr(Session(Me.GetType().Name & "CurrentCertificateCustomer"))
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnSearch(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Clear out existing certification number

        Dim tbGSOCertNo As TextBox = CType(ucGSOCertification.FindControl(CertificationNoText), TextBox)
        Dim tbCCCCertNo As TextBox = CType(ucCCCCertification.FindControl(CertificationNoText), TextBox)
        Dim tbE117CertNo As TextBox = CType(ucEmark117Certification.FindControl(CertificationNoText), TextBox)
        Dim tbEmarkCertNo As TextBox = CType(ucEmarkCertification.FindControl(CertificationNoText), TextBox)
        Dim tbImarkCertNo As TextBox = CType(ucImarkCertification.FindControl(CertificationNoText), TextBox)
        Dim tbNOMCertNo As TextBox = CType(ucNOMCertification.FindControl(CertificationNoText), TextBox)
        Dim tbGeneralCertNo As TextBox = CType(ucGeneralCertification.FindControl(CertificationNoText), TextBox)
        Dim tbE117ExtNo As TextBox = CType(ucEmark117Certification.FindControl(ExtensionText), TextBox)
        Dim tbEmarkExtNo As TextBox = CType(ucEmarkCertification.FindControl(ExtensionText), TextBox)

        tbGSOCertNo.Text = String.Empty
        tbCCCCertNo.Text = String.Empty
        tbE117CertNo.Text = String.Empty
        tbEmarkCertNo.Text = String.Empty
        tbImarkCertNo.Text = String.Empty
        tbNOMCertNo.Text = String.Empty
        tbGeneralCertNo.Text = String.Empty
        tbEmarkExtNo.Text = String.Empty
        tbE117ExtNo.Text = String.Empty
        Try
            Me.ErrorText = String.Empty
            treSearchTypeResults.Nodes.Clear()
            'jeseitz 6/15/2016 - added 0 for seconde parameter - certificationtypeid not needed for this
            ActivateCertificateControl(String.Empty, 0)
            RaiseEvent Search()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Raise Execute certification event
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnExecute(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
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
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Raise appropriate event
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub SelectedIndexChanged_ddlSearchTypes(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.treSearchTypeResults.Nodes.Clear()
        Me.SearchCriteria = String.Empty
        trBrand.Visible = False
        trBrandLine.Visible = False
        trSearch.Visible = True
        Const CertificationNoText As String = "Certification No."
        Const BrandText As String = "Brand"
        Const Text544 As String = "544"

        Try
            'Show extension field if search type selected is certifcate
            Select Case SearchType
                Case CertificationNoText
                    Extension.Visible = True
                    txtExtensionNo.Visible = True
                    lblExtInfo.Visible = True
                    lblExtHighest.Visible = True
                    txtExtensionNo.Text = Text
                    txtSearchFor.Text = String.Empty
                    lblImarkFamily.Visible = False
                    txtImarkFamily.Visible = False
                    txtImarkFamily.Text = String.Empty
                Case ImarkText
                    Extension.Visible = False
                    txtExtensionNo.Visible = False
                    lblExtInfo.Visible = False
                    lblExtHighest.Visible = False
                    txtExtensionNo.Text = Text
                    lblImarkFamily.Visible = True
                    txtImarkFamily.Visible = True
                    txtSearchFor.Text = Text544 '"I033" jeseitz 4/12/16
                    ' Added swith case for Brand as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
                Case BrandText
                    Extension.Visible = False
                    txtExtensionNo.Visible = False
                    lblExtInfo.Visible = False
                    lblExtHighest.Visible = False
                    txtExtensionNo.Text = Text
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
                    txtExtensionNo.Text = Text
                    lblImarkFamily.Visible = False
                    txtImarkFamily.Visible = False
                    txtImarkFamily.Text = String.Empty
                    txtSearchFor.Text = String.Empty
            End Select
        Catch
            Throw
        End Try
    End Sub

    ' Added SelectedIndexChanged_ddlBrand event as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Raise appropriate event
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub SelectedIndexChanged_ddlBrand(ByVal sender As Object, ByVal e As System.EventArgs)
        Const SelectText As String = "Select ..."
        Try
            If (ddlBrand.SelectedItem.Text <> SelectText) Then
                m_presenter.LoadBrandLines(Brand)
                trBrandLine.Visible = True
            Else
                trBrandLine.Visible = False
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Raise appropriate event
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub SelectedNodeChanged_treSearchTypeResults(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Clear out existing certification number
        Dim tbGSOCertNo As TextBox = CType(ucGSOCertification.FindControl(CertificationNoText), TextBox)
        Dim tbCCCCertNo As TextBox = CType(ucCCCCertification.FindControl(CertificationNoText), TextBox)
        Dim tbE117CertNo As TextBox = CType(ucEmark117Certification.FindControl(CertificationNoText), TextBox)
        Dim tbEmarkCertNo As TextBox = CType(ucEmarkCertification.FindControl(CertificationNoText), TextBox)
        Dim tbImarkCertNo As TextBox = CType(ucImarkCertification.FindControl(CertificationNoText), TextBox)
        Dim tbNOMCertNo As TextBox = CType(ucNOMCertification.FindControl(CertificationNoText), TextBox)
        Dim tbGeneralCertNo As TextBox = CType(ucGeneralCertification.FindControl(CertificationNoText), TextBox)
        Dim tbE117ExtNo As TextBox = CType(ucEmark117Certification.FindControl(ExtensionText), TextBox)
        Dim tbEmarkExtNo As TextBox = CType(ucEmarkCertification.FindControl(ExtensionText), TextBox)

        tbGSOCertNo.Text = String.Empty
        tbCCCCertNo.Text = String.Empty
        tbE117CertNo.Text = String.Empty
        tbEmarkCertNo.Text = String.Empty
        tbImarkCertNo.Text = String.Empty
        tbNOMCertNo.Text = String.Empty
        tbGeneralCertNo.Text = String.Empty
        tbEmarkExtNo.Text = String.Empty
        tbE117ExtNo.Text = String.Empty
        Try
            If treSearchTypeResults.SelectedNode.ChildNodes.Count = 0 Then
                RaiseEvent LeafNodeSelected()
            End If
        Catch
            Throw
        End Try
    End Sub

    ' Changed the PopulateTreeView method for displaying the material number in tree view and material attributes as tooltip.
    ''' <summary>
    ''' Populates the tree view.
    ''' </summary>
    ''' <param name="xDoc">The XMLDocument doc.</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub PopulateTreeView(ByVal xDoc As XmlDocument) Implements ICertificationSearchView.PopulateTreeView

        Dim tvParent, tvMaterialNum, tvCertif, tvCertNo As TreeNode
        Dim strSearch As String
        Const MaterialNoText As String = "Material No."
        Const PSNText As String = "PSN"
        Const BrandText As String = "Brand"
        Const BatchNoText As String = "Batch No."
        Const SpecNoText As String = "Spec No."

        Try
            treSearchTypeResults.Nodes.Clear()

            If xDoc.ChildNodes(1).ChildNodes.Count = 0 Then
                tvParent = New TreeNode(NoDataFoundText)
                treSearchTypeResults.Nodes.Add(tvParent)
            Else
                If Me.SearchType.Equals(MaterialNoText) Then
                    tvParent = New TreeNode(Me.SearchType)
                ElseIf Me.SearchType.Equals(PSNText) Then
                    tvParent = New TreeNode(PSNText)
                ElseIf Me.SearchType.Equals(BrandText) Then
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

                        MaterialNo = xNode.SelectSingleNode(MaterialNumberText).InnerText
                        SingLoadIndex = xNode.SelectSingleNode(SingleLoadIndexText).InnerText
                        DualLoadIndex = xNode.SelectSingleNode(DualLoadIndexText).InnerText
                        SpeedRating = xNode.SelectSingleNode(SpeedRatingText).InnerText
                        TireSize = xNode.SelectSingleNode(TireSizeText).InnerText

                        If (Not String.IsNullOrEmpty(DualLoadIndex) And DualLoadIndex <> "0") Then
                            tvMaterialNum.Text = String.Format("{0} {1} {2} / {3} {4}", MaterialNo.TrimStart("0"c), TireSize, SingLoadIndex, DualLoadIndex, SpeedRating)
                        Else
                            tvMaterialNum.Text = String.Format("{0} {1} {2} {3}", MaterialNo.TrimStart("0"c), TireSize, SingLoadIndex, SpeedRating)
                        End If

                        tvMaterialNum.ToolTip = xNode.SelectSingleNode(ToolTipText).InnerText
                        tvMaterialNum.Value = xNode.FirstChild.Attributes(SKUIdText).InnerText

                        For Each Item As XmlNode In xNode.SelectNodes(NameAid.Column.CertificationName)
                            Select Case Me.SearchType
                                Case MaterialNoText, _
                                     BrandText, _
                                     BatchNoText, _
                                     SpecNoText, _
                                     PSNText
                                    If Not Item.FirstChild Is Nothing Then
                                        If Item.FirstChild.InnerText.ToUpper().Equals(NOMText) Then
                                            'We have NOM certificate
                                            tvCertNo = New TreeNode
                                            If Item.SelectNodes(CertificateNo).Count > 0 Then
                                                For Each certNo As XmlNode In Item.SelectNodes(CertificateNo)
                                                    Me.FillTreeNodeWithChildNOM(Item, certNo, tvCertNo, String.Empty)
                                                Next
                                                tvMaterialNum.ChildNodes.Add(tvCertNo)
                                            Else
                                                Me.FillTreeNode(Item, tvMaterialNum)
                                            End If
                                        Else
                                            tvCertNo = New TreeNode
                                            If Item.SelectNodes(CertificateNo).Count > 0 Then
                                                For Each certNo As XmlNode In Item.SelectNodes(CertificateNo)
                                                    Me.FillTreeNodeWithChild(Item, certNo, tvCertNo, String.Empty)
                                                Next
                                                tvMaterialNum.ChildNodes.Add(tvCertNo)
                                            Else
                                                Me.FillTreeNode(Item, tvMaterialNum)
                                            End If
                                        End If
                                    End If
                                Case Else
                                    'Treat the Nom certificate that might have customers
                                    If Item.FirstChild.InnerText.ToLower().Equals(Nom) Then
                                        If xNode.SelectNodes(CustomerText).Count > 0 Then
                                            tvCertif = New TreeNode
                                            For Each customer As XmlNode In xNode.SelectNodes(CustomerText)
                                                Me.FillTreeNodeWithChild(Item, customer, tvCertif, Item.InnerText.ToLower())
                                            Next
                                            tvMaterialNum.ChildNodes.Add(tvCertif)
                                        Else
                                            'Nom Certificate that has no custumer
                                            Me.FillTreeNode(Item, tvMaterialNum)
                                        End If
                                    Else
                                        'All Other certification type requested that are not nom
                                        Me.FillTreeNode(Item, tvMaterialNum)
                                    End If
                            End Select
                        Next
                        'Adds node to the parent node.
                        tvParent.ChildNodes.Add(tvMaterialNum)
                    End If
                Next
                treSearchTypeResults.Nodes.Add(tvParent)
                treSearchTypeResults.ShowLines = True
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method to initialize tree view.
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub InitializeTreeView() Implements ICertificationSearchView.InitializeTreeView
        Try
            Me.TreeViewVisibility = True
            Me.treSearchTypeResults.Nodes.Clear()
            Dim tvMaterialNum As New TreeNode(String.Empty)
            Me.treSearchTypeResults.Nodes.Add(tvMaterialNum)
            Me.treSearchTypeResults.DataBind()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method to fill tree node.
    ''' </summary>
    ''' <param name="xNode">XmlNode</param>
    ''' <param name="parentNode">TreeNode</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub FillTreeNode(ByVal xNode As XmlNode, ByRef parentNode As TreeNode)

        Dim tvNode As New TreeNode()
        Dim sNodeText As String
        Try
            If xNode.Attributes(StateText).InnerText.Equals(RequestedText) Then
                sNodeText = GiveNodeTextRedColor(xNode.InnerText)
            ElseIf xNode.Attributes(StateText).InnerText.Equals(ApprovedText) Then
                sNodeText = GiveNodeTextGreenColor(xNode.InnerText)
            ElseIf xNode.Attributes(StateText).InnerText.Equals(InProgressText) Then
                sNodeText = GiveNodeTextYellowColor(xNode.InnerText)
            Else
                sNodeText = xNode.InnerText
            End If

            tvNode.Text = sNodeText
            tvNode.Value = xNode.InnerText

            parentNode.ChildNodes.Add(tvNode)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method to fill tree node with child.
    ''' </summary>
    ''' <param name="xNode">XmlNode</param>
    ''' <param name="xCustomer">XmlNode</param>
    ''' <param name="CertifNode">TreeNode</param>
    ''' <param name="CertifType">certificate type</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub FillTreeNodeWithChild(ByVal xNode As XmlNode, ByVal xCustomer As XmlNode, ByRef CertifNode As TreeNode, ByVal CertifType As String)

        Dim tvNode As New TreeNode()
        Dim sNodeText As String = String.Empty
        Dim sNodeCustomer As String = String.Empty
        Dim sNodeCustomerText As String = String.Empty
        Try
            If xCustomer.InnerText.Length > 20 Then
                sNodeCustomer = String.Concat(xCustomer.InnerText.Substring(0, 20), "...")
            Else
                sNodeCustomer = xCustomer.InnerText
            End If

            If CertifType.Equals(Nom) Then
                If xNode.Attributes(StateText).InnerText.Equals(RequestedText) Then
                    sNodeText = GiveNodeTextRedColor(xNode.FirstChild.InnerText)
                    sNodeCustomer = GiveNodeTextRedColor(sNodeCustomer)
                ElseIf xNode.Attributes(StateText).InnerText.Equals(ApprovedText) Then
                    sNodeText = GiveNodeTextGreenColor(xNode.FirstChild.InnerText)
                    sNodeCustomer = GiveNodeTextGreenColor(sNodeCustomer)
                ElseIf xNode.Attributes(StateText).InnerText.Equals(InProgressText) Then
                    sNodeText = GiveNodeTextYellowColor(xNode.FirstChild.InnerText)
                    sNodeCustomer = GiveNodeTextYellowColor(sNodeCustomer)
                Else
                    sNodeText = xNode.FirstChild.InnerText
                End If
            Else
                If xCustomer.Attributes(StateText).InnerText.Equals(RequestedText) Then
                    sNodeText = xNode.FirstChild.InnerText
                    sNodeCustomer = GiveNodeTextRedColor(sNodeCustomer)
                ElseIf xCustomer.Attributes(StateText).InnerText.Equals(ApprovedText) Then
                    sNodeText = xNode.FirstChild.InnerText
                    sNodeCustomer = GiveNodeTextGreenColor(sNodeCustomer)
                ElseIf xCustomer.Attributes(StateText).InnerText.Equals(InProgressText) Then
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
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method to fill tree node with child NOM.
    ''' </summary>
    ''' <param name="xNode">XmlNode</param>
    ''' <param name="xCustomer">XmlNode</param>
    ''' <param name="CertifNode">TreeNode</param>
    ''' <param name="CertifType">certificate type</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub FillTreeNodeWithChildNOM(ByVal xNode As XmlNode, ByVal xCustomer As XmlNode, ByRef CertifNode As TreeNode, ByVal CertifType As String)

        Dim tvNodeCert As New TreeNode()
        Dim tvNodeCust As New TreeNode()
        Dim sCertTypeNode As String = String.Empty
        Dim sCertNode As String = String.Empty
        Dim sNodeCustomer As String = String.Empty
        Try
            If Not xCustomer.NextSibling Is Nothing Then
                If xCustomer.NextSibling.Name = CustomerText Then
                    sCertTypeNode = xNode.FirstChild.InnerText
                    sCertNode = xCustomer.InnerText

                    tvNodeCert.Text = sCertNode
                    tvNodeCert.Value = sCertNode

                    'For Each customer As XmlNode In xNode.SelectNodes("Customer")
                    sNodeCustomer = xCustomer.NextSibling.InnerText
                    If xCustomer.Attributes(StateText).InnerText.Equals(RequestedText) Then
                        sNodeCustomer = GiveNodeTextRedColor(sNodeCustomer)
                    ElseIf xCustomer.Attributes(StateText).InnerText.Equals(ApprovedText) Then
                        sNodeCustomer = GiveNodeTextGreenColor(sNodeCustomer)
                    ElseIf xCustomer.Attributes(StateText).InnerText.Equals(InProgressText) Then
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
                    If xCustomer.Attributes(StateText).InnerText.Equals(RequestedText) Then
                        sCertTypeNode = xNode.FirstChild.InnerText
                        sCertNode = GiveNodeTextRedColor(xCustomer.InnerText)
                    ElseIf xCustomer.Attributes(StateText).InnerText.Equals(ApprovedText) Then
                        sCertTypeNode = xNode.FirstChild.InnerText
                        sCertNode = GiveNodeTextGreenColor(xCustomer.InnerText)
                    ElseIf xCustomer.Attributes(StateText).InnerText.Equals(InProgressText) Then
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
                If xCustomer.Attributes(StateText).InnerText.Equals(RequestedText) Then
                    sCertTypeNode = xNode.FirstChild.InnerText
                    sCertNode = GiveNodeTextRedColor(xCustomer.InnerText)
                ElseIf xCustomer.Attributes(StateText).InnerText.Equals(ApprovedText) Then
                    sCertTypeNode = xNode.FirstChild.InnerText
                    sCertNode = GiveNodeTextGreenColor(xCustomer.InnerText)
                ElseIf xCustomer.Attributes(StateText).InnerText.Equals(InProgressText) Then
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
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method to give node text yellow colour.
    ''' </summary>
    ''' <param name="nodeText">nodeText</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>node text colour</returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function GiveNodeTextYellowColor(ByVal nodeText As String) As String
        Dim strCollorIni As String = "<span style='color: orange;'>"
        Dim strCollorEnd As String = "</span'>"
        Try
            Return String.Concat(strCollorIni, nodeText, strCollorEnd)
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to give node text green colour.
    ''' </summary>
    ''' <param name="nodeText">nodeText</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>node text colour</returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function GiveNodeTextGreenColor(ByVal nodeText As String) As String
        Dim strCollorIni As String = "<span style='color: green;'>"
        Dim strCollorEnd As String = "</span'>"
        Try
            Return String.Concat(strCollorIni, nodeText, strCollorEnd)
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to give node text red colour.
    ''' </summary>
    ''' <param name="nodeText">nodeText</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>node text colour</returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function GiveNodeTextRedColor(ByVal nodeText As String) As String
        Dim strCollorIni As String = "<span style='color: red;'>"
        Dim strCollorEnd As String = "</span'>"
        Try
            Return String.Concat(strCollorIni, nodeText, strCollorEnd)
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Show current certificate according search result selection
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
    ''' <para>30/09/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowCurrentCertificate() Implements ICertificationSearchView.ShowCurrentCertificate
        Try
            ShowCertificate(CurrentCertificateName,
                            CurrentCertificationTypeID,
                            CurrentCertificateMaterialNum,
                            CurrentCertificateSKUID, CurrentCertificateCustomer,
                            CurrentCertificateNumber, CurrentCertificateExtension)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Show certificate for a Material number
    ''' </summary>
    ''' <param name="p_strCertName">certificate name</param>
    ''' <param name="p_intCertTypeID">certificate type id</param>
    ''' <param name="p_strMatlNum">material number</param>
    ''' <param name="p_strSkuId">Sku Id</param>
    ''' <param name="p_strCustomer">Customer</param>
    ''' <param name="p_strCertNumber">Certificate number</param>
    ''' <param name="p_strCertExtension">Certificate extension</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ShowCertificate(ByVal p_strCertName As String,
                                ByVal p_intCertTypeID As Integer,
                                ByVal p_strMatlNum As String, ByVal p_strSkuId As String,
                                ByVal p_strCustomer As String, ByVal p_strCertNumber As String,
                                ByVal p_strCertExtension As String)
        Try
            Dim ctlUC As Control = ActivateCertificateControl(p_strCertName, p_intCertTypeID)
            SetupCertificateControlProperties(ctlUC, p_strMatlNum, p_strSkuId, p_strCustomer, p_strCertNumber, p_strCertExtension)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Activate (show) appropriate certificate control on the right panel
    ''' </summary>
    ''' <param name="p_strUCControlName">certification name or control type name (for the other)</param>
    ''' <param name="p_intCertTypeID">certificate type id</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>UC</returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function ActivateCertificateControl(ByVal p_strUCControlName As String, ByVal p_intCertTypeID As Integer) As Control

        Dim ctrlUC As Control = Nothing
        Dim intIndexInMultiView As Integer = -1
        Const GeneralCertificationTypeNameText As String = "GeneralCertificationTypeName"
        Const GeneralCertificationTypeIdText As String = "GeneralCertificationTypeID"
        Const ECE3054Text As String = "ECE3054"
        Const ECE117Text As String = "ECE117"
        Const GSOText As String = "GSO"
        Const CCCText As String = "CCC"
        Const IndiaMarkText As String = "India_Mark"
        Const GeneralText As String = "GENERAL"

        Try
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
                Case ECE3054Text
                    intIndexInMultiView = 1
                    ctrlUC = ucEmarkCertification
                Case ECE117Text
                    intIndexInMultiView = 6
                    ctrlUC = ucEmark117Certification
                Case GSOText
                    intIndexInMultiView = 2
                    ctrlUC = ucGSOCertification
                Case NOMText
                    intIndexInMultiView = 3
                    ctrlUC = ucNOMCertification
                Case ImarkText
                    intIndexInMultiView = 4
                    ctrlUC = ucImarkCertification
                Case CCCText
                    intIndexInMultiView = 5
                    ctrlUC = ucCCCCertification
                Case IndiaMarkText
                    intIndexInMultiView = 7
                    ctrlUC = ucIndiaMarkCertification
                Case GeneralText
                    intIndexInMultiView = 17
                    ctrlUC = ucGeneralCertification
                    Session(Me.GetType().Name & GeneralCertificationTypeNameText) = p_strUCControlName
                    Session(Me.GetType().Name & GeneralCertificationTypeIdText) = p_intCertTypeID
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
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Setup certificate control specific properties
    ''' </summary>
    ''' <param name="p_ctlCertUC">certificate uc</param>
    ''' <param name="p_strMatlNum">material number</param>
    ''' <param name="p_strSKUID">SKU id</param>
    ''' <param name="p_strCustomer">customer</param>
    ''' <param name="p_strCertNumber">certificate number</param>
    ''' <param name="p_strCertExtension">certificate extension</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SetupCertificateControlProperties(ByVal p_ctlCertUC As Control,
                                                  ByVal p_strMatlNum As String,
                                                  ByVal p_strSKUID As String,
                                                  ByVal p_strCustomer As String,
                                                  ByVal p_strCertNumber As String,
                                                  ByVal p_strCertExtension As String)
        Const HText As String = "H"
        Try
            If p_ctlCertUC Is Nothing Then
                Return
            End If

            Dim tbCertNo As TextBox = CType(p_ctlCertUC.FindControl(CertificationNoText), TextBox)

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
            If p_ctlCertUC.GetType().BaseType.Name = GetType(EmarkCertificationUC).Name Or
                p_ctlCertUC.GetType().BaseType.Name = GetType(Emark117CertificationUC).Name Or
                p_ctlCertUC.GetType().BaseType.Name = GetType(ImarkCertificationUC).Name Then
                Dim tbExtNo As TextBox = CType(p_ctlCertUC.FindControl(ExtensionText), TextBox)

                If tbExtNo.Text.Length > 0 Then
                    If CurrentCertificateExtension <> tbExtNo.Text Then
                        If CurrentCertificateExtension.Length > 0 Then
                            If CurrentCertificateExtension <> Text AndAlso CurrentCertificateExtension <> HText Then
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
                Dim tbExtNo As TextBox = CType(p_ctlCertUC.FindControl(ExtensionOrRevisionText), TextBox)

                If tbExtNo.Text.Length > 0 Then
                    If CurrentCertificateExtension <> tbExtNo.Text Then
                        If CurrentCertificateExtension.Length > 0 Then
                            If CurrentCertificateExtension <> Text AndAlso CurrentCertificateExtension <> HText Then
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
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Show add certification user control
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowAddCertification(ByVal p_blnNewAddCertView As Boolean) Implements Presenter.ICertificationSearchView.ShowAddCertification
        'Dim ucAddCert As AddCertificationUC  =ActivateCertificateControl(GetType(AddCertificationUC).Name)
        'Added as per project 2706 technical specification
        'jeseitz 6/15/16 - Added 0 as second parameter to ActivateCertifcateControl - it should be certificationtypeid, but it is not used for these UC's
        Dim strUsercontrolName As String = GetType(AddCertificationUC).Name
        Const AddText As String = "Add"
        Const RenameText As String = "Rename"
        Const DeleteText As String = "Delete"
        Const DetachOrMoveText As String = "Detach/Move"
        Const MaterialmaintText As String = "Material Maint"
        Const CopyText As String = "Copy"
        Const EditText As String = "Edit"
        Const AttachText As String = "Attach"
        Const RefreshProductDataText As String = "Refresh Product Data"
        Const FamilyMaintenanceText As String = "Family Maintenance"

        Try
            Select Case ActionName
                Case AddText
                    strUsercontrolName = GetType(AddCertificationUC).Name
                    Dim ucAddCert As AddCertificationUC = CType(ActivateCertificateControl(strUsercontrolName, 0), AddCertificationUC)
                    SetupAddCertificateControlProperties(ucAddCert, p_blnNewAddCertView)
                Case RenameText
                    strUsercontrolName = GetType(RenameCertificationUC).Name
                    Dim ucRenameCert As RenameCertificationUC = CType(ActivateCertificateControl(strUsercontrolName, 0), RenameCertificationUC)
                    SetupRenameCertificateControlProperties(ucRenameCert, p_blnNewAddCertView)
                Case DeleteText
                    strUsercontrolName = GetType(DeleteCertificationUC).Name
                    Dim ucDeleteCert As DeleteCertificationUC = CType(ActivateCertificateControl(strUsercontrolName, 0), DeleteCertificationUC)
                    SetupDeleteCertificateControlProperties(ucDeleteCert, p_blnNewAddCertView)
                Case DetachOrMoveText
                    strUsercontrolName = GetType(DetachOrMoveCertificationUC).Name
                    Dim ucDetachOrMoveCert As DetachOrMoveCertificationUC = CType(ActivateCertificateControl(strUsercontrolName, 0), DetachOrMoveCertificationUC)
                    SetupDetachOrMoveCertificateControlProperties(ucDetachOrMoveCert, p_blnNewAddCertView)
                Case MaterialmaintText
                    Select Case MaterialMaintName
                        Case CopyText
                            strUsercontrolName = GetType(CopyCertificationUC).Name
                            Dim ucCopyCert As CopyCertificationUC = CType(ActivateCertificateControl(strUsercontrolName, 0), CopyCertificationUC)
                            SetupCopyCertificateControlProperties(ucCopyCert, p_blnNewAddCertView)
                        Case DeleteText
                            strUsercontrolName = GetType(DupCorrectCertificationUC).Name
                            Dim ucDupCorrectCert As DupCorrectCertificationUC = CType(ActivateCertificateControl(strUsercontrolName, 0), DupCorrectCertificationUC)
                            SetupDupCorrectCertificateControlProperties(ucDupCorrectCert, p_blnNewAddCertView)
                        Case EditText
                            strUsercontrolName = GetType(EditCertificationUC).Name
                            Dim ucEditCert As EditCertificationUC = CType(ActivateCertificateControl(strUsercontrolName, 0), EditCertificationUC)
                            SetupEditCertificateControlProperties(ucEditCert, p_blnNewAddCertView)
                        Case AttachText
                            strUsercontrolName = GetType(AttachCertificationUC).Name
                            Dim ucAttachCert As AttachCertificationUC = CType(ActivateCertificateControl(strUsercontrolName, 0), AttachCertificationUC)
                            SetupAttachCertificateControlProperties(ucAttachCert, p_blnNewAddCertView)
                        Case RefreshProductDataText
                            strUsercontrolName = GetType(RefreshProductUC).Name
                            Dim ucRefreshProd As RefreshProductUC = CType(ActivateCertificateControl(strUsercontrolName, 0), RefreshProductUC)
                            SetupRefreshProductControlProperties(ucRefreshProd, p_blnNewAddCertView)
                    End Select
                Case FamilyMaintenanceText
                    strUsercontrolName = GetType(FamilyMaintenanceUC).Name
                    Dim ucFamilyMaint As FamilyMaintenanceUC = CType(ActivateCertificateControl(strUsercontrolName, 0), FamilyMaintenanceUC)
                    SetupFamilyMaintenanceControlProperties(ucFamilyMaint, p_blnNewAddCertView)
            End Select
        Catch
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' Setup add certificate view properties
    ''' </summary>
    ''' <param name="p_ctlAddCertUC">Add certificate UC</param>
    ''' <param name="p_blnNewAddCertView">New add certificate view</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SetupAddCertificateControlProperties(ByVal p_ctlAddCertUC As AddCertificationUC, ByVal p_blnNewAddCertView As Boolean)
        Try
            If p_ctlAddCertUC Is Nothing Then
                Return
            End If

            p_ctlAddCertUC.SetupViewData(AddCertificationName, p_blnNewAddCertView)

        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup rename certificate view properties
    ''' </summary>
    ''' <param name="p_ctlRenameCertUC">Rename certificate UC</param>
    ''' <param name="p_blnRenameCertView">Rename certificate iew</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SetupRenameCertificateControlProperties(ByVal p_ctlRenameCertUC As RenameCertificationUC, ByVal p_blnRenameCertView As Boolean)
        Try
            If p_ctlRenameCertUC Is Nothing Then
                Return
            End If
            p_ctlRenameCertUC.SetupViewData(AddCertificationName, p_blnRenameCertView)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup delete certificate view properties
    ''' </summary>
    ''' <param name="p_ctlDeleteCertUC">Delete certificate UC</param>
    ''' <param name="p_blnDeleteCertView">Delete certificate view</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SetupDeleteCertificateControlProperties(ByVal p_ctlDeleteCertUC As DeleteCertificationUC, ByVal p_blnDeleteCertView As Boolean)
        Try
            If p_ctlDeleteCertUC Is Nothing Then
                Return
            End If
            p_ctlDeleteCertUC.SetupViewData(AddCertificationName, p_blnDeleteCertView)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup detach or move certificate view properties
    ''' </summary>
    ''' <param name="p_ctlDetachOrMoveCertUC">Detach or Move certificate UC</param>
    ''' <param name="p_blnDetachOrMoveCertView">Detach or Move certificate view</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SetupDetachOrMoveCertificateControlProperties(ByVal p_ctlDetachOrMoveCertUC As DetachOrMoveCertificationUC, ByVal p_blnDetachOrMoveCertView As Boolean)
        Try
            If p_ctlDetachOrMoveCertUC Is Nothing Then
                Return
            End If
            p_ctlDetachOrMoveCertUC.SetupViewData(AddCertificationName, p_blnDetachOrMoveCertView)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup dup correct certificate view properties
    ''' </summary>
    ''' <param name="p_ctlDupCorrectCertUC">Dup correct certificate UC</param>
    ''' <param name="p_blnDupCorrectCertView">Dup correct certificate view</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SetupDupCorrectCertificateControlProperties(ByVal p_ctlDupCorrectCertUC As DupCorrectCertificationUC, ByVal p_blnDupCorrectCertView As Boolean)
        Try
            If p_ctlDupCorrectCertUC Is Nothing Then
                Return
            End If
            p_ctlDupCorrectCertUC.SetupViewData(AddCertificationName, p_blnDupCorrectCertView)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup dup correct certificate view properties
    ''' </summary>
    ''' <param name="p_ctlCopyCertUC">Copy certificate UC</param>
    ''' <param name="p_blnDCopyCertView">Copy certificate view</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SetupCopyCertificateControlProperties(ByVal p_ctlCopyCertUC As CopyCertificationUC, ByVal p_blnDCopyCertView As Boolean)
        Try
            If p_ctlCopyCertUC Is Nothing Then
                Return
            End If
            p_ctlCopyCertUC.SetupViewData(p_blnDCopyCertView)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup dup correct certificate view properties
    ''' </summary>
    ''' <param name="p_ctlDupCorrectCertUC">Dup correct certificate UC</param>
    ''' <param name="p_blnDupCorrectCertView">Dup correct certificate view</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SetupEditCertificateControlProperties(ByVal p_ctlDupCorrectCertUC As EditCertificationUC, ByVal p_blnDupCorrectCertView As Boolean)
        Try
            If p_ctlDupCorrectCertUC Is Nothing Then
                Return
            End If
            p_ctlDupCorrectCertUC.SetupViewData(p_blnDupCorrectCertView)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup dup correct certificate view properties
    ''' </summary>
    ''' <param name="p_ucAttachCertUC">Attach certificate UC</param>
    ''' <param name="p_blnAttachCertView">Attach certificate view</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SetupAttachCertificateControlProperties(ByVal p_ucAttachCertUC As AttachCertificationUC, ByVal p_blnAttachCertView As Boolean)
        Try
            If p_ucAttachCertUC Is Nothing Then
                Return
            End If
            p_ucAttachCertUC.SetupViewData(p_blnAttachCertView)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup Refresh Product Control Properties
    ''' </summary>
    ''' <param name="p_ucRefreshProdUC">Refresh Product user control object</param>
    ''' <param name="p_blnRefreshProdView">Boolean Value</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SetupRefreshProductControlProperties(ByVal p_ucRefreshProdUC As RefreshProductUC, ByVal p_blnRefreshProdView As Boolean)
        Try
            If p_ucRefreshProdUC Is Nothing Then
                Return
            End If
            p_ucRefreshProdUC.SetupViewData(p_blnRefreshProdView)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup family maintenance view properties
    ''' </summary>
    ''' <param name="p_ctlFamilyMaintUC">Family maintenance UC</param>
    ''' <param name="p_blnFamilyMaintView">Family maintenance view</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SetupFamilyMaintenanceControlProperties(ByVal p_ctlFamilyMaintUC As FamilyMaintenanceUC, ByVal p_blnFamilyMaintView As Boolean)
        Try
            If p_ctlFamilyMaintUC Is Nothing Then
                Return
            End If
            p_ctlFamilyMaintUC.SetupViewData(AddCertificationName, p_blnFamilyMaintView)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Expand selected node
    ''' </summary>
    ''' <param name="p_strMatlNum">Material number</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ExpandSelectedNode(ByVal p_strMatlNum As String) Implements ICertificationSearchView.ExpandSelectedNode
        Try
            For Each node As TreeNode In treSearchTypeResults.Nodes(0).ChildNodes
                node.CollapseAll()
                If node.Text.Substring(0, 11).Equals(p_strMatlNum) Then
                    node.ExpandAll()
                End If
            Next
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' tree search type results load
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub treSearchTypeResults_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not treSearchTypeResults Is Nothing AndAlso Not treSearchTypeResults.SelectedNode Is Nothing Then
                If Not Me.SelectedSearchResultParentText Is Nothing Then
                    Me.ExpandSelectedNode(Me.SelectedSearchResultParentText)
                End If
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Populates the tree view for Certification No search type
    ''' </summary>
    ''' <param name="xDoc">The XMLDocument doc.</param>
    ''' <param name="dtbCertNoSearch">datatable certificate no search</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub PopulateTreeForCertificateNumber(ByVal xDoc As XmlDocument, ByVal dtbCertNoSearch As DataTable) Implements ICertificationSearchView.PopulateTreeForCertificateNumber

        Const CertificateIdText As String = "CERTIFICATEID"
        Const CertificationIdText As String = "CERTIFICATIONID"

        Dim tvParent, tvParentGSO, tvMaterialNum, tvCertif As TreeNode
        treSearchTypeResults.Nodes.Clear()
        Try
            If xDoc.ChildNodes(1).ChildNodes.Count = 0 Then
                tvParent = New TreeNode(NoDataFoundText)
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

                        SkuId = xNode.FirstChild.Attributes(SKUIdText).InnerText
                        MaterialNo = xNode.SelectSingleNode(MaterialNumberText).InnerText
                        SingLoadIndex = xNode.SelectSingleNode(SingleLoadIndexText).InnerText
                        DualLoadIndex = xNode.SelectSingleNode(DualLoadIndexText).InnerText
                        SpeedRating = xNode.SelectSingleNode(SpeedRatingText).InnerText
                        TireSize = xNode.SelectSingleNode(TireSizeText).InnerText
                        certificateId = xNode.SelectSingleNode(CertificateIdText).InnerText

                        If (Not String.IsNullOrEmpty(DualLoadIndex) And DualLoadIndex <> "0") Then
                            tvMaterialNum.Text = String.Format("{0} {1} {2} / {3} {4}", MaterialNo.TrimStart("0"c), TireSize, SingLoadIndex, DualLoadIndex, SpeedRating)
                        Else
                            tvMaterialNum.Text = String.Format("{0} {1} {2} {3}", MaterialNo.TrimStart("0"c), TireSize, SingLoadIndex, SpeedRating)
                        End If

                        tvMaterialNum.ToolTip = xNode.SelectSingleNode(ToolTipText).InnerText
                        tvMaterialNum.Value = xNode.FirstChild.Attributes(SKUIdText).InnerText
                        count = 0
                        For Each Item As XmlNode In xNode.SelectNodes(NameAid.Column.CertificationName)
                            count = count + 1
                            If Item.FirstChild.InnerText.ToLower().Equals(Nom) Then
                                If xNode.SelectNodes(CustomerText).Count > 0 Then
                                    tvCertif = New TreeNode
                                    For Each customer As XmlNode In xNode.SelectNodes(CustomerText)
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
                        If (Convert.ToInt32(dtbCertNoSearch.Rows(0)(CertificationIdText)) = 1 Or Convert.ToInt32(dtbCertNoSearch.Rows(0)(CertificationIdText)) = 6) Then
                            Dim ExtNo As Integer = 0
                            tvParent = FindExtensionNode(CInt(SkuId), CInt(certificateId), dtbCertNoSearch, arrtvParentExt, ExtNo)
                            tvParent.ChildNodes.Add(tvMaterialNum)
                            tvParent.Value = CStr(ExtNo)
                        Else
                            tvParentGSO.ChildNodes.Add(tvMaterialNum)
                        End If

                    End If
                Next

                If (Convert.ToInt32(dtbCertNoSearch.Rows(0)(CertificationIdText)) = 1 Or Convert.ToInt32(dtbCertNoSearch.Rows(0)(CertificationIdText)) = 6) Then
                    AddExtensionNodes(arrtvParentExt)
                Else
                    treSearchTypeResults.Nodes.Add(tvParentGSO)
                End If
                treSearchTypeResults.ShowLines = True
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Create extension Nodes for Certificate Number Search Type
    ''' </summary>
    ''' <param name="dtCertificate">datatable certificate</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>XML Node</returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function CreateExtensionNodes(ByVal dtCertificate As DataTable) As TreeNode()
        Dim dtbExtension As DataTable = dtCertificate.DefaultView.ToTable(True, ExtensionTxt)
        Dim extensionNodes(dtbExtension.Rows.Count - 1) As TreeNode
        Dim strCertificateNumber As String = dtCertificate.Rows(0)(CertificateNumberText).ToString()
        Dim iIndex As Integer = 0
        Try
            For iIndex = 0 To dtbExtension.Rows.Count - 1
                Dim intExtention As Integer = CInt(dtbExtension.Rows(iIndex)(ExtensionTxt))
                Dim tvParentExt As New TreeNode(String.Format("{0} ext {1}", strCertificateNumber, intExtention.ToString()))
                extensionNodes(iIndex) = tvParentExt
            Next
            Return extensionNodes
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Find extension Node
    ''' </summary>
    ''' <param name="skuId">sku Id</param>
    ''' <param name="certId">Certificate Id</param>
    ''' <param name="dtbCertificate">datatable certificate</param>
    ''' <param name="extensionNodes">extension nodes</param>
    ''' <param name="extension">extension</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>TreeNode</returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function FindExtensionNode(ByVal skuId As Integer, ByVal certId As Integer, ByVal dtbCertificate As DataTable, ByVal extensionNodes() As TreeNode, ByRef extension As Integer) As TreeNode
        Dim strExtension As String = String.Format("SKUID={0} And CERTIFICATEID={1}", skuId, certId)
        extension = CInt(dtbCertificate.Select(strExtension)(0)(ExtensionTxt))
        Dim index As Integer = 0
        Try
            For index = 0 To extensionNodes.Length - 1
                If (extensionNodes(index).Text = String.Format("{0} ext {1}", dtbCertificate.Rows(0)(CertificateNumberText).ToString(), extension.ToString())) Then
                    Return extensionNodes(index)
                    Exit For
                End If
            Next
            Return New TreeNode(dtbCertificate.Rows(0)(CertificateNumberText).ToString())
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Check if node already exists in the tree
    ''' </summary>
    ''' <param name="tvMaterialNum">tv material number</param>
    ''' <param name="nodeValue">node value</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>Boolean</returns>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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
    ''' <param name="extensionNodes">Extension Nodes</param>
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
    ''' <para>10/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub AddExtensionNodes(ByVal extensionNodes() As TreeNode)
        Dim nodeIndex As Integer = 0
        Dim extensionIndex As Integer = 0
        Dim extention As String = String.Empty
        Dim extensions(extensionNodes.Length - 1) As Integer
        Try
            For extensionIndex = 0 To extensions.Length - 1
                extensions(extensionIndex) = CInt(extensionNodes(extensionIndex).Value)
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
        Catch
            Throw
        End Try
    End Sub
#End Region

End Class
