Imports CooperTire.ICS.Presenter

Partial Public Class MarketingNew
    Inherits BasePage
    Implements IMarketingNewView

    ' Added dropdown lists Brand and Brand line instead of Brand code , changed material number instead of sku in grid view.
    ' Also displayed the material attributes as tooltip. 
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Members"
    ''' <summary>
    '''  Save event
    ''' </summary>
    Public Event Save As EventHandler Implements IMarketingNewView.Save
    ''' <summary>
    '''  Search event
    ''' </summary>
    Public Event Search As EventHandler Implements IMarketingNewView.Search
    'Public Event RegionChanged As EventHandler Implements IMarketingNewView.RegionChanged
    'Public Event AddRegionChanged As EventHandler Implements IMarketingNewView.AddRegionChanged
    ''' <summary>
    '''  Select Changed With DataDirty event
    ''' </summary>
    Public Event SelectChangedWithDataDirty As EventHandler Implements IMarketingNewView.SelectChangedWithDataDirty
    ''' <summary>
    '''  Product Request Status Changed event
    ''' </summary>
    Public Event ProductRequestStatusChanged As CustomEvents.StatusChangedEventHandler Implements IMarketingNewView.ProductRequestStatusChanged
    ''' <summary>
    '''  Changed All Certificate Status By Material event
    ''' </summary>
    Public Event ChangedAllCertStatusByMaterial As CustomEvents.SelectAllBySKUEventHandler Implements IMarketingNewView.ChangeAllCertStatusByMaterial
    ''' <summary>
    '''  Changed All Materials Status By Certificate event
    ''' </summary>
    Public Event ChangedAllMaterialsStatusByCert As CustomEvents.SelectAllByCountryEventHandler Implements IMarketingNewView.ChangeAllMaterialsStatusByCert

    Private m_presenter As MarketingNewPresenter

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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()
        m_presenter = New MarketingNewPresenter(Me)
    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    '''  Gets or sets Info text value.
    ''' </summary>
    ''' <returns>
    ''' Info text 
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property InfoText() As String Implements IMarketingNewView.InfoText
        Get
            Return lblInfo.Text
        End Get
        Set(ByVal value As String)
            lblInfo.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Success text value.
    ''' </summary>
    ''' <returns>
    ''' Success text 
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property SuccessText() As String Implements IMarketingNewView.SuccessText
        Get
            Return lblSuccess.Text
        End Get
        Set(ByVal value As String)
            lblSuccess.Text = value
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements IMarketingNewView.ErrorText
        Get
            Return lblError.Text
        End Get
        Set(ByVal value As String)
            lblError.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Product Request Status Data value.
    ''' </summary>
    ''' <returns>
    ''' Product Request Status Data 
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property ProductRequestStatusData() As DataTable Implements IMarketingNewView.ProductRequestStatusData
        Get
            Return CType(Session("ProductRequestStatusData"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("ProductRequestStatusData") = value
            grdCert.DataSource = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Certificate Types value.
    ''' </summary>
    ''' <returns>
    ''' Certificate Types 
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property CertificateTypes() As DataTable Implements IMarketingNewView.CertificateTypes
        Get
            Return CType(Session("CertifcateTypes"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("CertificateTypes") = value
            grdCert.DataSource = value
        End Set
    End Property

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation    
    ''' <summary>
    '''  Gets or sets Brands value.
    ''' </summary>
    ''' <returns>
    ''' Brands 
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property Brands() As List(Of String) Implements IMarketingNewView.Brands
        Get
            Return CType(ddlBrand.DataSource, Global.System.Collections.Generic.List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            ddlBrand.DataSource = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets BrandLines value.
    ''' </summary>
    ''' <returns>
    ''' BrandLines 
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property BrandLines() As List(Of String) Implements IMarketingNewView.BrandLines
        Get
            Return CType(ddlBrandLine.DataSource, Global.System.Collections.Generic.List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            ddlBrandLine.DataSource = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Brands value.
    ''' </summary>
    ''' <returns>
    ''' SelectedItem text
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    ReadOnly Property Brand() As String Implements IMarketingNewView.Brand
        Get
            Return ddlBrand.SelectedItem.Text
        End Get
    End Property

    ''' <summary>
    '''  BrandLine value.
    ''' </summary>
    ''' <returns>
    ''' SelectedItem text
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    ReadOnly Property BrandLine() As String Implements IMarketingNewView.BrandLine
        Get
            If (ddlBrandLine.Items.Count > 0) Then
                Return ddlBrandLine.SelectedItem.Text
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ''' <summary>
    '''  Gets or sets Data Dirty Flag value.
    ''' </summary>
    ''' <returns>
    ''' DataDirtyFlag value 
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Property DataDirtyFlag() As Boolean Implements IMarketingNewView.DataDirtyFlag
        Get
            Return CBool(hidDataDirtyFlag.Value)
        End Get
        Set(ByVal value As Boolean)
            hidDataDirtyFlag.Value = CStr(value)
        End Set
    End Property


#End Region

#Region "Methods"
    ''' <summary>
    '''  Method for search brand button click.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnSearchBrand(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            RaiseEvent Search(sender, e)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for save button click.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnSave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            RaiseEvent Save(sender, e)
        Catch
            Throw
        End Try
    End Sub

    ' Added SelectedIndexChanged_ddlBrand()  as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub SelectedIndexChanged_ddlBrand(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Load the Brand Line from Product data Web Service
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
    '''  Method for confirm click.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.DataDirtyFlag = False
            'RaiseEvent RegionChanged(Me.SelectedRegionName, e)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for cancel click.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ' Restore the selected region label and reset the value of changed select region list
            Me.DataDirtyFlag = True
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Raise appropriate event
    ''' </summary>
    ''' <param name="p_strMaterialNum">material number</param>
    ''' <param name="p_strCountryId">country Id</param>
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ProductRequestStatusChange(ByVal p_strMaterialNum As String, ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)
        Try
            RaiseEvent ProductRequestStatusChanged(p_strMaterialNum, p_strCountryId, p_blnChecked)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Raise appropriate event
    ''' </summary>
    ''' <param name="p_strMaterialNum">material number</param>
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SelectAllByMaterials(ByVal p_strMaterialNum As String, ByVal p_blnChecked As Boolean)
        Try
            RaiseEvent ChangedAllCertStatusByMaterial(p_strMaterialNum, p_blnChecked)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Raise appropriate event
    ''' </summary>
    ''' <param name="p_strCountryId">country Id</param>
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SelectAllByCountry(ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)
        Try
            RaiseEvent ChangedAllMaterialsStatusByCert(p_strCountryId, p_blnChecked)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' certificate page index change
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub grdCert_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            grdCert.PageIndex = e.NewPageIndex
            grdCert.DataSource = Me.ProductRequestStatusData
            grdCert.DataBind()
        Catch
            Throw
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' setup certificate view
    ''' </summary>
    ''' <param name="p_dtbProdRequestStatus">product request status</param>
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupCertificateView(ByVal p_dtbProdRequestStatus As DataTable) Implements IMarketingNewView.SetupCertificateView
        Const SingleLoadIndexText As String = "SingLoadIndex"
        Const DualLoadIndexText As String = "DualLoadIndex"
        Const SpeedRatingText As String = "SpeedRating"
        Const ToolTipText As String = "ToolTip"
        Const SKUIdText As String = "SKUID"
        Const MaterialNumText As String = "MATL_NUM"
        Const MaterialNumTDText As String = "MatlNumTD"
        Const SizeText As String = "Size"
        Try
            ProductRequestStatusData = p_dtbProdRequestStatus
            grdCert.Columns.Clear()

            If p_dtbProdRequestStatus Is Nothing Then
                Return
            End If

            For Each col As DataColumn In p_dtbProdRequestStatus.Columns
                'Declare the bound field and allocate memory for the bound field.
                Dim tfield As TemplateField = New TemplateField
                'Initalize the DataField value.
                If col.ColumnName = SKUIdText Then
                    Continue For
                ElseIf col.ColumnName = ToolTipText Then
                    'Initialize the HeaderText field value.
                    tfield.HeaderTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                    tfield.HeaderStyle.Height = 75
                    'Add the newly created bound field to the GridView with select all options.
                    tfield.ItemTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.AlternatingItem, col.ColumnName, col.ColumnName, Nothing)
                    tfield.ItemStyle.Wrap = False
                    grdCert.Columns.Add(tfield)
                    Dim intToolTipPos As Integer = grdCert.Columns.Count
                    grdCert.Columns(intToolTipPos - 1).Visible = False
                ElseIf col.ColumnName = MaterialNumText Then
                    'Initialize the HeaderText field value.
                    tfield.HeaderTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                    tfield.HeaderStyle.Height = 75
                    'Add the newly created bound field to the GridView with select all options.
                    tfield.ItemTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.AlternatingItem, col.ColumnName, col.ColumnName, Nothing)
                    tfield.ItemStyle.Wrap = False
                    tfield.ItemStyle.CssClass = MaterialNumTDText
                    grdCert.Columns.Add(tfield)
                ElseIf col.ColumnName = SizeText Then
                    tfield.HeaderTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                    tfield.HeaderStyle.Height = 75
                    'Initialize the HeaderText field value.
                    tfield.ItemTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.Item, col.ColumnName, col.ColumnName, Nothing)
                    'Add the newly created bound field to the GridView.
                    grdCert.Columns.Add(tfield)
                ElseIf col.ColumnName = SingleLoadIndexText Then
                    'Initialize the HeaderText field value.
                    tfield.HeaderTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                    tfield.HeaderStyle.Height = 75
                    'Add the newly created bound field to the GridView with select all options.
                    tfield.ItemTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.AlternatingItem, col.ColumnName, col.ColumnName, Nothing)
                    tfield.ItemStyle.Wrap = False
                    grdCert.Columns.Add(tfield)
                    grdCert.Columns(2).Visible = False
                ElseIf col.ColumnName = DualLoadIndexText Then
                    'Initialize the HeaderText field value.
                    tfield.HeaderTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                    tfield.HeaderStyle.Height = 75
                    'Add the newly created bound field to the GridView with select all options.
                    tfield.ItemTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.AlternatingItem, col.ColumnName, col.ColumnName, Nothing)
                    tfield.ItemStyle.Wrap = False
                    grdCert.Columns.Add(tfield)
                    grdCert.Columns(3).Visible = False
                ElseIf col.ColumnName = SpeedRatingText Then
                    'Initialize the HeaderText field value.
                    tfield.HeaderTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                    tfield.HeaderStyle.Height = 75
                    'Add the newly created bound field to the GridView with select all options.
                    tfield.ItemTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.AlternatingItem, col.ColumnName, col.ColumnName, Nothing)
                    tfield.ItemStyle.Wrap = False
                    grdCert.Columns.Add(tfield)
                    grdCert.Columns(4).Visible = False
                    
                Else
                    tfield.HeaderTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.Footer, col.ColumnName, col.Caption, Nothing)
                    tfield.HeaderStyle.Height = 75
                    tfield.HeaderStyle.Width = 60
                    tfield.ItemTemplate = New ProductRequestStatusGridViewTemplate(ListItemType.SelectedItem, col.ColumnName, col.ColumnName, Nothing)
                    'tfield.ItemStyle.BackColor = Drawing.Color.Red
                    grdCert.Columns.Add(tfield)
                End If
            Next

            grdCert.DataSource = p_dtbProdRequestStatus
            grdCert.DataBind()

            ' Setup the view if no data returned from database
            btnSave.Visible = p_dtbProdRequestStatus.Rows.Count > 0
        Catch
            Throw
        End Try
    End Sub

    ' Added method as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    '''  public method called by presenter to set page controls to default view.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupDefaultView() Implements IMarketingNewView.SetupDefaultView

        'Clear previous grid data
        ProductRequestStatusData = Nothing
        Try
            grdCert.DataSource = ProductRequestStatusData
            grdCert.DataBind()
        Catch
            Throw
        End Try
        ' Setup the view if no data returned from database
        btnSave.Visible = False
        lblSelectedRegion.Text = String.Empty
        lblSuccess.Text = String.Empty
        lblError.Text = String.Empty
    End Sub

    ' Added grdCertRegion_RowDataBound event to add a tooltip to material number in grid view.
    ''' <summary>
    '''  grid certificate row data bound.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub grdCert_RowDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        'This condition is used to assign the tooltip for material number.
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblMatlNum As Label
                Dim lblToolTip As Label
                Dim lblSingLoadIndex As Label
                Dim lblDualLoadIndex As Label
                Dim lblSpeedRating As Label

                Dim intToolTipPos As Integer = grdCert.Columns.Count
                lblMatlNum = CType(e.Row.Cells(0).Controls(0), Label)
                lblSingLoadIndex = CType(e.Row.Cells(2).Controls(0), Label)
                lblDualLoadIndex = CType(e.Row.Cells(3).Controls(0), Label)
                lblSpeedRating = CType(e.Row.Cells(4).Controls(0), Label)

                lblToolTip = CType(e.Row.Cells(intToolTipPos - 1).Controls(0), Label)
                If (Not String.IsNullOrEmpty(lblMatlNum.Text)) Then
                    e.Row.Cells(0).ToolTip = lblToolTip.Text

                    If (Not String.IsNullOrEmpty(lblDualLoadIndex.Text) And lblDualLoadIndex.Text <> "0") Then
                        lblMatlNum.Text = String.Format("{0} {1}  / {2} {3} ", lblMatlNum.Text.TrimStart("0"c), lblSingLoadIndex.Text, lblDualLoadIndex.Text, lblSpeedRating.Text)
                    Else
                        lblMatlNum.Text = String.Format("{0} {1} {2} ", lblMatlNum.Text.TrimStart("0"c), lblSingLoadIndex.Text, lblSpeedRating.Text)
                    End If
                End If
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Raise appropriate event.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub btnSearchBrand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchBrand.Click
        Try
            RaiseEvent Search(sender, e)
        Catch
            Throw
        End Try
    End Sub

End Class

''' <summary>
''' Contains methods related to product request status grid view template.
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
''' <para>10/04/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class ProductRequestStatusGridViewTemplate
    Implements ITemplate

    Private litTemplateType As ListItemType
    Private strColumnName As String
    Private strColumnCaption As String
    Private dtbCertificationCountryTable As DataTable

    Private ReadOnly m_strCheckedImageUrl As String = "~/Images/checked.png"
    Private ReadOnly m_strUncheckedImageUrl As String = "~/Images/unchecked.png"
    ''' <summary>
    ''' Defaullt Constructor to initialize class members.
    ''' </summary>
    ''' <param name="type">type</param>
    ''' <param name="colname">Column Name</param>
    ''' <param name="colcaption">Caption</param>
    ''' <param name="certificationCountryTable">Certification country table</param>
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub New(ByVal type As ListItemType, ByVal colname As String, ByVal colcaption As String, ByVal certificationCountryTable As DataTable)
        Try
            litTemplateType = type
            strColumnName = colname
            strColumnCaption = colcaption
            dtbCertificationCountryTable = certificationCountryTable
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' instantiate the item template of gridview template field based on the listItemType and 
    ''' add the actual controls with event handler to the item template
    ''' </summary>
    ''' <param name="container">Container</param>
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub ITemplate_InstantiateIn(ByVal container As System.Web.UI.Control) Implements ITemplate.InstantiateIn
        Const MaterialNumText As String = "MATL_NUM"
        Const MaterialNumberText As String = "Material Number"
        Const SelectAllByColumnText As String = "btnSelectAllByColumn"
        Const DeSelectAllByColumnText As String = "btnDeSelectAllByColumn"
        Const SelectAllText As String = "Select All"
        Const DeSelectAllText As String = "DeSelect All"
        Const SelectAllByRowText As String = "btnSelectAllByRow"
        Const DeSelectAllByRowText As String = "btnDeSelectAllByRow"
        Const lblText As String = "lbl"
        Const ChkText As String = "Chk"
        Try
            Select Case (litTemplateType)
                Case ListItemType.Header
                    'Creates a new label control and add it to the container.
                    Dim lblHead As Label = New Label
                    'Allocates the new label object.
                    lblHead.Text = strColumnCaption
                    If (lblHead.Text = MaterialNumText) Then
                        lblHead.Text = MaterialNumberText
                    End If
                    'Assigns the name of the column in the lable.
                    container.Controls.Add(lblHead)
                    'Adds the newly created label control to the container.
                Case ListItemType.Footer
                    'Create panel to hold the header text
                    Dim pnlHeaderText As Panel = New Panel
                    pnlHeaderText.ControlStyle.Width = 60
                    pnlHeaderText.ControlStyle.Height = 60
                    'Create different header with select/deselect options
                    Dim lblHead As Label = New Label
                    'Allocates the new label object.
                    lblHead.Text = strColumnCaption
                    lblHead.Width = 60
                    lblHead.Height = 60
                    pnlHeaderText.Controls.Add(lblHead)

                    'Add panel for holding the select/deselect all buttons
                    Dim pnlSelectButtons As Panel = New Panel
                    pnlSelectButtons.ControlStyle.Width = 60

                    'Create Select All button
                    Dim btnSelectAllByColumn As ImageButton = New ImageButton
                    AddHandler btnSelectAllByColumn.Click, AddressOf Me.btnSelectAllByColumn_Clicked
                    btnSelectAllByColumn.ID = (SelectAllByColumnText + strColumnName)
                    btnSelectAllByColumn.ImageUrl = m_strCheckedImageUrl
                    btnSelectAllByColumn.ToolTip = SelectAllText
                    Dim btnDeSelectAllByColumn As ImageButton = New ImageButton
                    AddHandler btnDeSelectAllByColumn.Click, AddressOf Me.btnDeSelectAllByColumn_Clicked
                    btnDeSelectAllByColumn.ID = (DeSelectAllByColumnText + strColumnName)
                    btnDeSelectAllByColumn.ImageUrl = m_strUncheckedImageUrl
                    btnDeSelectAllByColumn.ToolTip = DeSelectAllText
                    'Adds the newly created label control to the container.
                    pnlSelectButtons.Controls.Add(btnSelectAllByColumn)
                    pnlSelectButtons.Controls.Add(btnDeSelectAllByColumn)
                    container.Controls.Add(pnlHeaderText)
                    container.Controls.Add(pnlSelectButtons)
                Case ListItemType.Pager
                    'Create panel to hold the header text
                    Dim pnlHeaderText As Panel = New Panel
                    pnlHeaderText.ControlStyle.Width = 60
                    pnlHeaderText.ControlStyle.Height = 60
                    'Create different header with select/deselect options
                    Dim lblHead As Label = New Label
                    'Allocates the new label object.
                    lblHead.Text = strColumnCaption
                    lblHead.Width = 60
                    lblHead.Height = 60
                    pnlHeaderText.Controls.Add(lblHead)

                    'jeseitz 6/8/16   pnlHeaderText.ToolTip = GetCountryListString(strColumnCaption)

                    'Add panel for holding the select/deselect all buttons
                    Dim pnlSelectButtons As Panel = New Panel
                    pnlSelectButtons.ControlStyle.Width = 60

                    'Create Select All button
                    Dim btnSelectAllByColumn As ImageButton = New ImageButton
                    AddHandler btnSelectAllByColumn.Click, AddressOf Me.btnSelectAllByColumn_Clicked
                    btnSelectAllByColumn.ID = (SelectAllByColumnText + strColumnName)
                    btnSelectAllByColumn.ImageUrl = m_strCheckedImageUrl
                    btnSelectAllByColumn.ToolTip = SelectAllText
                    Dim btnDeSelectAllByColumn As ImageButton = New ImageButton
                    AddHandler btnDeSelectAllByColumn.Click, AddressOf Me.btnDeSelectAllByColumn_Clicked
                    btnDeSelectAllByColumn.ID = (SelectAllByColumnText + strColumnName)
                    btnDeSelectAllByColumn.ImageUrl = m_strUncheckedImageUrl
                    btnDeSelectAllByColumn.ToolTip = DeSelectAllText
                    'Adds the newly created label control to the container.
                    pnlSelectButtons.Controls.Add(btnSelectAllByColumn)
                    pnlSelectButtons.Controls.Add(btnDeSelectAllByColumn)
                    container.Controls.Add(pnlHeaderText)
                    container.Controls.Add(pnlSelectButtons)
                Case ListItemType.Item
                    'Creates a new label control and add it to the container.
                    Dim lblItem As Label = New Label
                    'Allocates the new label object.
                    AddHandler lblItem.DataBinding, AddressOf Me.lblItem_DataBinding
                    lblItem.ID = (lblText + strColumnName)
                    'Assigns the name of the column in the lable.
                    container.Controls.Add(lblItem)
                    'Adds the newly created label control to the container.
                Case ListItemType.EditItem
                    'Creates a new text box control and add it to the container.
                    Dim tb1 As TextBox = New TextBox
                    'Allocates the new text box object.
                    AddHandler tb1.DataBinding, AddressOf Me.tb1_DataBinding
                    'Attaches the data binding event.
                    tb1.Columns = 4
                    'Creates a column with size 4.
                    container.Controls.Add(tb1)
                    'Adds the newly created textbox to the container.
                Case ListItemType.SelectedItem
                    'CheckBox controls
                    Dim chkColumn As CheckBox = New CheckBox
                    chkColumn.AutoPostBack = True
                    AddHandler chkColumn.DataBinding, AddressOf Me.chkColumn_DataBinding
                    AddHandler chkColumn.CheckedChanged, AddressOf Me.chkColumn_CheckedChanged
                    chkColumn.ID = (ChkText + strColumnName)
                    container.Controls.Add(chkColumn)
                Case ListItemType.AlternatingItem
                    'Creates a new label control and add it to the container.
                    Dim lblItem As Label = New Label
                    'Allocates the new label object.
                    AddHandler lblItem.DataBinding, AddressOf Me.lblItem_DataBinding
                    lblItem.ID = (lblText + strColumnName)

                    'Create Select All button
                    Dim btnSelectAllByRow As ImageButton = New ImageButton
                    AddHandler btnSelectAllByRow.Click, AddressOf Me.btnSelectAllByRow_Clicked
                    btnSelectAllByRow.ID = (SelectAllByRowText + strColumnName)
                    btnSelectAllByRow.ImageUrl = m_strCheckedImageUrl
                    btnSelectAllByRow.ToolTip = SelectAllText
                    Dim btnDeSelectAllByRow As ImageButton = New ImageButton
                    AddHandler btnDeSelectAllByRow.Click, AddressOf Me.btnDeSelectAllByRow_Clicked
                    btnDeSelectAllByRow.ID = (DeSelectAllByRowText + strColumnName)
                    btnDeSelectAllByRow.ImageUrl = m_strUncheckedImageUrl
                    btnDeSelectAllByRow.ToolTip = DeSelectAllText
                    'Assigns the name of the column in the lable.
                    container.Controls.Add(lblItem)
                    container.Controls.Add(btnSelectAllByRow)
                    container.Controls.Add(btnDeSelectAllByRow)
            End Select
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the binding happens.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub tb1_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim txtdata As TextBox = CType(sender, TextBox)
            Dim container As GridViewRow = CType(txtdata.NamingContainer, GridViewRow)
            Dim dataValue As Object = DataBinder.Eval(container.DataItem, strColumnName)
            txtdata.Text = dataValue.ToString
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the binding happens.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub lblItem_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim lbldata As Label = CType(sender, Label)
            Dim container As GridViewRow = CType(lbldata.NamingContainer, GridViewRow)
            Dim dataValue As Object = DataBinder.Eval(container.DataItem, strColumnName)
            lbldata.Text = dataValue.ToString
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the binding happens.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub chkColumn_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Const UncertifiedText As String = "Uncertified"
        Const RequestedText As String = "Requested"
        Const ApprovedText As String = "Approved"
        Const InProgressText As String = "InProgress"
        Try
            Dim chkdata As CheckBox = CType(sender, CheckBox)
            Dim container As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
            Dim dataValue As Object = DataBinder.Eval(container.DataItem, strColumnName)
            If dataValue Is DBNull.Value Then
                dataValue = UncertifiedText
            End If
            Select Case (dataValue).ToString
                Case UncertifiedText
                    chkdata.Checked = False
                Case RequestedText
                    chkdata.Checked = True
                    chkdata.ControlStyle.BackColor = Drawing.Color.Red
                Case ApprovedText
                    chkdata.Checked = True
                    chkdata.ControlStyle.BackColor = Drawing.Color.Green
                    chkdata.Enabled = False
                Case InProgressText
                    chkdata.Checked = True
                    chkdata.ControlStyle.BackColor = Drawing.Color.Yellow
                    chkdata.Enabled = False
            End Select
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the checked event happens.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub chkColumn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim chkdata As CheckBox = CType(sender, CheckBox)
            Dim containerGridViewRow As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
            Dim containerGridView As GridView = CType(containerGridViewRow.NamingContainer, GridView)
            Dim pageMarketing As MarketingNew = CType(containerGridViewRow.NamingContainer.NamingContainer.NamingContainer.NamingContainer, MarketingNew)
            pageMarketing.ProductRequestStatusChange(CType(containerGridViewRow.Cells(0).Controls(0), Label).Text, strColumnName, chkdata.Checked)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the checked event happens.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub btnSelectAllByRow_Clicked(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Try
            Dim chkdata As ImageButton = CType(sender, ImageButton)
            Dim containerGridViewRow As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
            Dim containerGridView As GridView = CType(containerGridViewRow.NamingContainer, GridView)
            Dim containerPage As MarketingNew = CType(containerGridViewRow.NamingContainer.NamingContainer.NamingContainer.NamingContainer, MarketingNew)
            containerPage.SelectAllByMaterials(CType(containerGridViewRow.Cells(0).Controls(0), Label).Text, True)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the checked event happens.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub btnDeSelectAllByRow_Clicked(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Try
            Dim chkdata As ImageButton = CType(sender, ImageButton)
            Dim containerGridViewRow As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
            Dim containerGridView As GridView = CType(containerGridViewRow.NamingContainer, GridView)
            Dim containerPage As MarketingNew = CType(containerGridViewRow.NamingContainer.NamingContainer.NamingContainer.NamingContainer, MarketingNew)
            containerPage.SelectAllByMaterials(CType(containerGridViewRow.Cells(0).Controls(0), Label).Text, False)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the checked event happens.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub btnSelectAllByColumn_Clicked(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Try
            Dim chkdata As ImageButton = CType(sender, ImageButton)
            Dim containerGridViewRow As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
            Dim containerGridView As GridView = CType(containerGridViewRow.NamingContainer, GridView)
            Dim containerPage As MarketingNew = CType(containerGridViewRow.NamingContainer.NamingContainer.NamingContainer.NamingContainer, MarketingNew)
            containerPage.SelectAllByCountry(strColumnName, True)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the checked event happens.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub btnDeSelectAllByColumn_Clicked(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Try
            Dim chkdata As ImageButton = CType(sender, ImageButton)
            Dim containerGridViewRow As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
            Dim containerGridView As GridView = CType(containerGridViewRow.NamingContainer, GridView)
            Dim containerPage As MarketingNew = CType(containerGridViewRow.NamingContainer.NamingContainer.NamingContainer.NamingContainer, MarketingNew)
            containerPage.SelectAllByCountry(strColumnName, False)
        Catch
            Throw
        End Try
    End Sub

End Class
