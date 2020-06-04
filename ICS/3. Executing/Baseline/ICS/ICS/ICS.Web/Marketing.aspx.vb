Imports CooperTire.ICS.Presenter

Partial Public Class Marketing
    Inherits BasePage
    Implements IMarketingView

    ' Added dropdown lists Brand and Brand line instead of Brand code , changed material number instead of sku in grid view.
    ' Also displayed the material attributes as tooltip. 
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Members"

    Public Event Save As EventHandler Implements IMarketingView.Save
    Public Event Search As EventHandler Implements IMarketingView.Search
    Public Event RegionChanged As EventHandler Implements IMarketingView.RegionChanged
    Public Event AddRegionChanged As EventHandler Implements IMarketingView.AddRegionChanged
    Public Event SelectChangedWithDataDirty As EventHandler Implements IMarketingView.SelectChangedWithDataDirty
    Public Event ProductCountryStatusChanged As CustomEvents.StatusChangedEventHandler Implements IMarketingView.ProductCountryStatusChanged
    Public Event ChangedAllCountriesStatusByMaterial As CustomEvents.SelectAllBySKUEventHandler Implements IMarketingView.ChangeAllCountriesStatusByMaterial
    Public Event ChangedAllMaterialsStatusByCountry As CustomEvents.SelectAllByCountryEventHandler Implements IMarketingView.ChangeAllMaterialsStatusByCountry

    Private m_presenter As MarketingPresenter

#End Region

#Region "Constructors"

    Public Sub New()
        m_presenter = New MarketingPresenter(Me)
    End Sub

#End Region

#Region "Properties"

    Public Property InfoText() As String Implements IMarketingView.InfoText
        Get
            Return lblInfo.Text
        End Get
        Set(ByVal value As String)
            lblInfo.Text = value
        End Set
    End Property

    Public Property SuccessText() As String Implements IMarketingView.SuccessText
        Get
            Return lblSuccess.Text
        End Get
        Set(ByVal value As String)
            lblSuccess.Text = value
        End Set
    End Property

    Public Property ErrorText() As String Implements IMarketingView.ErrorText
        Get
            Return lblError.Text
        End Get
        Set(ByVal value As String)
            lblError.Text = value
        End Set
    End Property

    Property CertifiedRegions() As List(Of String) Implements IMarketingView.CertifiedRegions
        Get
            Return Session("CertifiedRegions")
        End Get
        Set(ByVal value As List(Of String))
            Session("CertifiedRegions") = value
            ddlCertifiedRegions.DataSource = value
            If value.Count() > 1 Then
                ddlCertifiedRegions.Enabled = True
            End If
        End Set
    End Property

    Property UncertifiedRegions() As List(Of String) Implements IMarketingView.UncertifiedRegions
        Get
            Return Session("UncertifiedRegions")
        End Get
        Set(ByVal value As List(Of String))
            Session("UncertifiedRegions") = value
            ddlUncertifiedRegions.DataSource = value
            If value.Count() > 1 Then
                ddlUncertifiedRegions.Enabled = True
            End If
        End Set
    End Property

    Property ProductCountryStatusData() As DataTable Implements IMarketingView.ProductCountryStatusData
        Get
            Return Session("ProductCountryStatusData")
        End Get
        Set(ByVal value As DataTable)
            Session("ProductCountryStatusData") = value
            grdCertRegion.DataSource = value
        End Set
    End Property

    Property CertificationGroupCountries() As DataTable Implements IMarketingView.CertificationGroupCountries
        Get
            Return Session("CertificationGroupCountries")
        End Get
        Set(ByVal value As DataTable)
            Session("CertificationGroupCountries") = value
        End Set
    End Property

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation    

    Property Brands() As List(Of String) Implements IMarketingView.Brands
        Get
            Return ddlBrand.DataSource
        End Get
        Set(ByVal value As List(Of String))
            ddlBrand.DataSource = value
        End Set
    End Property

    Property BrandLines() As List(Of String) Implements IMarketingView.BrandLines
        Get
            Return ddlBrandLine.DataSource
        End Get
        Set(ByVal value As List(Of String))
            ddlBrandLine.DataSource = value
        End Set
    End Property

    ReadOnly Property Brand() As String Implements IMarketingView.Brand
        Get
            Return ddlBrand.SelectedItem.Text
        End Get
    End Property

    ReadOnly Property BrandLine() As String Implements IMarketingView.BrandLine
        Get
            If (ddlBrandLine.Items.Count > 0) Then
                Return ddlBrandLine.SelectedItem.Text
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Property SelectedRegionName() As String Implements IMarketingView.SelectedRegionName
        Get
            Return lblSelectedRegion.Text
        End Get
        Set(ByVal value As String)
            lblSelectedRegion.Text = value
            If Not CertifiedRegions Is Nothing Then
                If CertifiedRegions.Contains(value) Then
                    ddlCertifiedRegions.Text = value
                    ddlUncertifiedRegions.SelectedIndex = 0
                ElseIf UncertifiedRegions.Contains(value) Then
                    ddlUncertifiedRegions.Text = value
                    ddlCertifiedRegions.SelectedIndex = 0
                End If
            End If
        End Set
    End Property

    Property DataDirtyFlag() As Boolean Implements IMarketingView.DataDirtyFlag
        Get
            Return hidDataDirtyFlag.Value
        End Get
        Set(ByVal value As Boolean)
            hidDataDirtyFlag.Value = value
        End Set
    End Property

    Property OrignalCertRegionSelectValue() As String Implements IMarketingView.OrignalCertRegionSelectValue
        Get
            Return hidCertRegionName.Value
        End Get
        Set(ByVal value As String)
            hidCertRegionName.Value = value
        End Set
    End Property

    Property OrignalUnCertRegionSelectValue() As String Implements IMarketingView.OrignalUnCertRegionSelectValue
        Get
            Return hidUnCertRegionName.Value
        End Get
        Set(ByVal value As String)
            hidUnCertRegionName.Value = value
        End Set
    End Property

#End Region

#Region "Methods"

    Protected Sub Click_btnSearchBrand(ByVal sender As System.Object, ByVal e As System.EventArgs)

        RaiseEvent Search(sender, e)

    End Sub

    Protected Sub Click_btnSave(ByVal sender As System.Object, ByVal e As System.EventArgs)

        RaiseEvent Save(sender, e)

    End Sub

    Protected Sub SelectChange_ddlUncertifiedRegions(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Me.DataDirtyFlag Then
            RaiseEvent SelectChangedWithDataDirty(ddlUncertifiedRegions.SelectedItem.Text, e)
            Me.ConfirmPopUp.Show()
        Else
            RaiseEvent AddRegionChanged(ddlUncertifiedRegions.SelectedItem.Text, e)
        End If

    End Sub

    Protected Sub SelectChange_ddlCertifiedRegions(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Me.DataDirtyFlag Then
            RaiseEvent SelectChangedWithDataDirty(ddlCertifiedRegions.SelectedItem.Text, e)
            Me.ConfirmPopUp.Show()
        Else
            RaiseEvent RegionChanged(ddlCertifiedRegions.SelectedItem.Text, e)
        End If

    End Sub

    ' Added SelectedIndexChanged_ddlBrand()  as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Raise appropriate event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectedIndexChanged_ddlBrand(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Load the Brand Line from Product data Web Service
        If (ddlBrand.SelectedItem.Text <> "Select ...") Then
            m_presenter.LoadBrandLines(Brand)
            trBrandLine.Visible = True
        Else
            trBrandLine.Visible = False
        End If
    End Sub

    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.DataDirtyFlag = False
        RaiseEvent RegionChanged(Me.SelectedRegionName, e)

    End Sub

    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' Restore the selected region label and reset the value of changed select region list
        Me.DataDirtyFlag = True
        If hidCertRegionName.Value <> "" Then
            ddlCertifiedRegions.SelectedValue = hidCertRegionName.Value
            lblSelectedRegion.Text = hidCertRegionName.Value
        ElseIf hidUnCertRegionName.Value <> "" Then
            ddlUncertifiedRegions.SelectedValue = hidUnCertRegionName.Value
            lblSelectedRegion.Text = hidUnCertRegionName.Value
        End If

    End Sub

    Public Sub ProductCountryStatusChange(ByVal p_strMaterialNum As String, ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)

        RaiseEvent ProductCountryStatusChanged(p_strMaterialNum, p_strCountryId, p_blnChecked)

    End Sub

    Public Sub SelectAllByMaterials(ByVal p_strMaterialNum As String, ByVal p_blnChecked As Boolean)

        RaiseEvent ChangedAllCountriesStatusByMaterial(p_strMaterialNum, p_blnChecked)

    End Sub

    Public Sub SelectAllByCountry(ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)

        RaiseEvent ChangedAllMaterialsStatusByCountry(p_strCountryId, p_blnChecked)

    End Sub

    Protected Sub grdCertRegion_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        grdCertRegion.PageIndex = e.NewPageIndex
        grdCertRegion.DataSource = Me.ProductCountryStatusData
        grdCertRegion.DataBind()
    End Sub


#End Region

    ''' <summary>
    ''' public method called by presenter to dynamically generate the gridview based on the datatable get from database
    ''' </summary>
    ''' <param name="p_dtbProdRegCountryStatus"></param>
    ''' <remarks></remarks>
    Public Sub SetupRegionView(ByVal p_dtbProdRegCountryStatus As DataTable) Implements IMarketingView.SetupRegionView

        ProductCountryStatusData = p_dtbProdRegCountryStatus

        grdCertRegion.Columns.Clear()

        If p_dtbProdRegCountryStatus Is Nothing Then
            Return
        End If

        For Each col As DataColumn In p_dtbProdRegCountryStatus.Columns
            'Declare the bound field and allocate memory for the bound field.
            Dim tfield As TemplateField = New TemplateField
            'Initalize the DataField value.
            If col.ColumnName = "SKUID" Then
                Continue For
            ElseIf col.ColumnName = "ToolTip" Then
                'Initialize the HeaderText field value.
                tfield.HeaderTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                tfield.HeaderStyle.Height = 75
                'Add the newly created bound field to the GridView with select all options.
                tfield.ItemTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.AlternatingItem, col.ColumnName, col.ColumnName, Nothing)
                tfield.ItemStyle.Wrap = False
                grdCertRegion.Columns.Add(tfield)
                Dim intToolTipPos = grdCertRegion.Columns.Count
                grdCertRegion.Columns(intToolTipPos - 1).Visible = False
            ElseIf col.ColumnName = "MATL_NUM" Then
                'Initialize the HeaderText field value.
                tfield.HeaderTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                tfield.HeaderStyle.Height = 75
                'Add the newly created bound field to the GridView with select all options.
                tfield.ItemTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.AlternatingItem, col.ColumnName, col.ColumnName, Nothing)
                tfield.ItemStyle.Wrap = False
                tfield.ItemStyle.CssClass = "MatlNumTD"
                grdCertRegion.Columns.Add(tfield)
            ElseIf col.ColumnName = "Size" Then
                tfield.HeaderTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                tfield.HeaderStyle.Height = 75
                'Initialize the HeaderText field value.
                tfield.ItemTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.Item, col.ColumnName, col.ColumnName, Nothing)
                'Add the newly created bound field to the GridView.
                grdCertRegion.Columns.Add(tfield)
            ElseIf col.ColumnName = "SingLoadIndex" Then
                'Initialize the HeaderText field value.
                tfield.HeaderTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                tfield.HeaderStyle.Height = 75
                'Add the newly created bound field to the GridView with select all options.
                tfield.ItemTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.AlternatingItem, col.ColumnName, col.ColumnName, Nothing)
                tfield.ItemStyle.Wrap = False
                grdCertRegion.Columns.Add(tfield)
                grdCertRegion.Columns(2).Visible = False
            ElseIf col.ColumnName = "DualLoadIndex" Then
                'Initialize the HeaderText field value.
                tfield.HeaderTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                tfield.HeaderStyle.Height = 75
                'Add the newly created bound field to the GridView with select all options.
                tfield.ItemTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.AlternatingItem, col.ColumnName, col.ColumnName, Nothing)
                tfield.ItemStyle.Wrap = False
                grdCertRegion.Columns.Add(tfield)
                grdCertRegion.Columns(3).Visible = False
            ElseIf col.ColumnName = "SpeedRating" Then
                'Initialize the HeaderText field value.
                tfield.HeaderTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.Header, col.ColumnName, col.Caption, Nothing)
                tfield.HeaderStyle.Height = 75
                'Add the newly created bound field to the GridView with select all options.
                tfield.ItemTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.AlternatingItem, col.ColumnName, col.ColumnName, Nothing)
                tfield.ItemStyle.Wrap = False
                grdCertRegion.Columns.Add(tfield)
                grdCertRegion.Columns(4).Visible = False
            ElseIf col.ColumnName = col.Caption Then
                'grouping column
                tfield.HeaderTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.Pager, col.ColumnName, col.Caption, Me.CertificationGroupCountries)
                tfield.HeaderStyle.Height = 75
                tfield.HeaderStyle.Width = 60
                tfield.ItemTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.SelectedItem, col.ColumnName, col.ColumnName, Nothing)
                'tfield.ItemStyle.BackColor = Drawing.Color.Red
                grdCertRegion.Columns.Add(tfield)
            Else
                tfield.HeaderTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.Footer, col.ColumnName, col.Caption, Nothing)
                tfield.HeaderStyle.Height = 75
                tfield.HeaderStyle.Width = 60
                tfield.ItemTemplate = New ProductCountryStatusGridViewTemplate(ListItemType.SelectedItem, col.ColumnName, col.ColumnName, Nothing)
                'tfield.ItemStyle.BackColor = Drawing.Color.Red
                grdCertRegion.Columns.Add(tfield)
            End If
        Next

        grdCertRegion.DataSource = p_dtbProdRegCountryStatus
        grdCertRegion.DataBind()

        ' Setup the view if no data returned from database
        btnSave.Visible = p_dtbProdRegCountryStatus.Rows.Count > 0

        If p_dtbProdRegCountryStatus.Rows.Count = 0 Then
            If Me.CertifiedRegions.Count = 1 Then
                ddlCertifiedRegions.Items.Clear()
                ddlCertifiedRegions.Items.Add("Select ...")
                ddlCertifiedRegions.Enabled = False
            End If
            If Me.UncertifiedRegions.Count = 1 Then
                ddlUncertifiedRegions.Items.Clear()
                ddlUncertifiedRegions.Items.Add("Select ...")
                ddlUncertifiedRegions.Enabled = False
            End If
        End If

    End Sub
    ' Added method as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    '''  public method called by presenter to set page controls to default view.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetupDefaultView() Implements IMarketingView.SetupDefaultView

		'Clear previous grid data
		ProductCountryStatusData = Nothing

        grdCertRegion.DataSource = ProductCountryStatusData
        grdCertRegion.DataBind()

        ' Setup the view if no data returned from database
        btnSave.Visible = False
        lblSelectedRegion.Text = String.Empty
        lblSuccess.Text = String.Empty
        lblError.Text = String.Empty

        ddlCertifiedRegions.Items.Clear()
        ddlCertifiedRegions.Items.Add("Select ...")
        ddlCertifiedRegions.Enabled = False

        ddlUncertifiedRegions.Items.Clear()
        ddlUncertifiedRegions.Items.Add("Select ...")
        ddlUncertifiedRegions.Enabled = False
    End Sub

    ' Added grdCertRegion_RowDataBound event to add a tooltip to material number in grid view.
    Protected Sub grdCertRegion_RowDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        'This condition is used to assign the tooltip for material number.

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblMatlNum As Label
            Dim lblToolTip As Label
            Dim lblSingLoadIndex As Label
            Dim lblDualLoadIndex As Label
            Dim lblSpeedRating As Label

            Dim intToolTipPos = grdCertRegion.Columns.Count
            lblMatlNum = e.Row.Cells(0).Controls(0)
            lblSingLoadIndex = e.Row.Cells(2).Controls(0)
            lblDualLoadIndex = e.Row.Cells(3).Controls(0)
            lblSpeedRating = e.Row.Cells(4).Controls(0)

            lblToolTip = e.Row.Cells(intToolTipPos - 1).Controls(0)
            If (Not String.IsNullOrEmpty(lblMatlNum.Text)) Then
                e.Row.Cells(0).ToolTip = lblToolTip.Text

                If (Not String.IsNullOrEmpty(lblDualLoadIndex.Text) And lblDualLoadIndex.Text <> "0") Then
                    lblMatlNum.Text = String.Format("{0} {1}  / {2} {3} ", lblMatlNum.Text.TrimStart("0"c), lblSingLoadIndex.Text, lblDualLoadIndex.Text, lblSpeedRating.Text)
                Else
                    lblMatlNum.Text = String.Format("{0} {1} {2} ", lblMatlNum.Text.TrimStart("0"c), lblSingLoadIndex.Text, lblSpeedRating.Text)
                End If
            End If
            
        End If
    End Sub

End Class

''' <summary>
''' Item template for Product-Region-Country-Status grid
''' </summary>
''' <remarks></remarks>
Public Class ProductCountryStatusGridViewTemplate
    Implements ITemplate

    Private litTemplateType As ListItemType
    Private strColumnName As String
    Private strColumnCaption As String
    Private dtbCertificationCountryTable As DataTable

    Private ReadOnly m_strCheckedImageUrl As String = "~/Images/checked.png"
    Private ReadOnly m_strUncheckedImageUrl As String = "~/Images/unchecked.png"

    Sub New(ByVal type As ListItemType, ByVal colname As String, ByVal colcaption As String, ByVal certificationCountryTable As DataTable)

        litTemplateType = type
        strColumnName = colname
        strColumnCaption = colcaption
        dtbCertificationCountryTable = certificationCountryTable

    End Sub

    ''' <summary>
    ''' instantiate the item template of gridview template field based on the listItemType and 
    ''' add the actual controls with event handler to the item template
    ''' </summary>
    ''' <param name="container"></param>
    ''' <remarks></remarks>
    Sub ITemplate_InstantiateIn(ByVal container As System.Web.UI.Control) Implements ITemplate.InstantiateIn

        Select Case (litTemplateType)
            Case ListItemType.Header
                'Creates a new label control and add it to the container.
                Dim lblHead As Label = New Label
                'Allocates the new label object.
                lblHead.Text = strColumnCaption
                If (lblHead.Text = "MATL_NUM") Then
                    lblHead.Text = "Material Number"
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
                btnSelectAllByColumn.ID = ("btnSelectAllByColumn" + strColumnName)
                btnSelectAllByColumn.ImageUrl = m_strCheckedImageUrl
                btnSelectAllByColumn.ToolTip = "Select All"
                Dim btnDeSelectAllByColumn As ImageButton = New ImageButton
                AddHandler btnDeSelectAllByColumn.Click, AddressOf Me.btnDeSelectAllByColumn_Clicked
                btnDeSelectAllByColumn.ID = ("btnDeSelectAllByColumn" + strColumnName)
                btnDeSelectAllByColumn.ImageUrl = m_strUncheckedImageUrl
                btnDeSelectAllByColumn.ToolTip = "DeSelect All"
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
                pnlHeaderText.ToolTip = GetCountryListString(strColumnCaption)

                'Add panel for holding the select/deselect all buttons
                Dim pnlSelectButtons As Panel = New Panel
                pnlSelectButtons.ControlStyle.Width = 60

                'Create Select All button
                Dim btnSelectAllByColumn As ImageButton = New ImageButton
                AddHandler btnSelectAllByColumn.Click, AddressOf Me.btnSelectAllByColumn_Clicked
                btnSelectAllByColumn.ID = ("btnSelectAllByColumn" + strColumnName)
                btnSelectAllByColumn.ImageUrl = m_strCheckedImageUrl
                btnSelectAllByColumn.ToolTip = "Select All"
                Dim btnDeSelectAllByColumn As ImageButton = New ImageButton
                AddHandler btnDeSelectAllByColumn.Click, AddressOf Me.btnDeSelectAllByColumn_Clicked
                btnDeSelectAllByColumn.ID = ("btnDeSelectAllByColumn" + strColumnName)
                btnDeSelectAllByColumn.ImageUrl = m_strUncheckedImageUrl
                btnDeSelectAllByColumn.ToolTip = "DeSelect All"
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
                lblItem.ID = ("lbl" + strColumnName)
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
                chkColumn.ID = ("Chk" + strColumnName)
                container.Controls.Add(chkColumn)
            Case ListItemType.AlternatingItem
                'Creates a new label control and add it to the container.
                Dim lblItem As Label = New Label
                'Allocates the new label object.
                AddHandler lblItem.DataBinding, AddressOf Me.lblItem_DataBinding
                lblItem.ID = ("lbl" + strColumnName)

                'Create Select All button
                Dim btnSelectAllByRow As ImageButton = New ImageButton
                AddHandler btnSelectAllByRow.Click, AddressOf Me.btnSelectAllByRow_Clicked
                btnSelectAllByRow.ID = ("btnSelectAllByRow" + strColumnName)
                btnSelectAllByRow.ImageUrl = m_strCheckedImageUrl
                btnSelectAllByRow.ToolTip = "Select All"
                Dim btnDeSelectAllByRow As ImageButton = New ImageButton
                AddHandler btnDeSelectAllByRow.Click, AddressOf Me.btnDeSelectAllByRow_Clicked
                btnDeSelectAllByRow.ID = ("btnDeSelectAllByRow" + strColumnName)
                btnDeSelectAllByRow.ImageUrl = m_strUncheckedImageUrl
                btnDeSelectAllByRow.ToolTip = "DeSelect All"
                'Assigns the name of the column in the lable.
                container.Controls.Add(lblItem)
                container.Controls.Add(btnSelectAllByRow)
                container.Controls.Add(btnDeSelectAllByRow)
        End Select

    End Sub

    ''' <summary>
    ''' return the tooltip helper text for grouping countries
    ''' </summary>
    ''' <param name="p_strCertificationName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCountryListString(ByVal p_strCertificationName As String) As String

        Dim strCountrylist As String = ""
        For Each drwRow As DataRow In dtbCertificationCountryTable.Rows
            If drwRow("CERTIFICATIONNAME") = p_strCertificationName Then
                strCountrylist = strCountrylist & drwRow("COUNTRYNAME") & Environment.NewLine
            End If
        Next
        Return strCountrylist

    End Function

    ''' <summary>
    ''' This is the event, which will be raised when the binding happens.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tb1_DataBinding(ByVal sender As Object, ByVal e As EventArgs)

        Dim txtdata As TextBox = CType(sender, TextBox)
        Dim container As GridViewRow = CType(txtdata.NamingContainer, GridViewRow)
        Dim dataValue As Object = DataBinder.Eval(container.DataItem, strColumnName)
        '' If (dataValue <> DBNull.Value) Then
        txtdata.Text = dataValue.ToString
        ''End If

    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the binding happens.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lblItem_DataBinding(ByVal sender As Object, ByVal e As EventArgs)

        Dim lbldata As Label = CType(sender, Label)
        Dim container As GridViewRow = CType(lbldata.NamingContainer, GridViewRow)
        Dim dataValue As Object = DataBinder.Eval(container.DataItem, strColumnName)
        lbldata.Text = dataValue.ToString

    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the binding happens.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub chkColumn_DataBinding(ByVal sender As Object, ByVal e As EventArgs)

        Dim chkdata As CheckBox = CType(sender, CheckBox)
        Dim container As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
        Dim dataValue As Object = DataBinder.Eval(container.DataItem, strColumnName)
        If dataValue Is DBNull.Value Then
            dataValue = "Uncertified"
        End If
        Select Case (dataValue)
            Case "Uncertified"
                chkdata.Checked = False
            Case "Requested"
                chkdata.Checked = True
                chkdata.ControlStyle.BackColor = Drawing.Color.Red
            Case "Approved"
                chkdata.Checked = True
                chkdata.ControlStyle.BackColor = Drawing.Color.Green
                chkdata.Enabled = False
            Case "InProgress"
                chkdata.Checked = True
                chkdata.ControlStyle.BackColor = Drawing.Color.Yellow
                chkdata.Enabled = False
        End Select

    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the checked event happens.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub chkColumn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim chkdata As CheckBox = CType(sender, CheckBox)
        Dim containerGridViewRow As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
        Dim containerGridView As GridView = CType(containerGridViewRow.NamingContainer, GridView)
        Dim pageMarketing As Marketing = CType(containerGridViewRow.NamingContainer.NamingContainer.NamingContainer.NamingContainer, Marketing)
        pageMarketing.ProductCountryStatusChange(CType(containerGridViewRow.Cells(0).Controls(0), Label).Text, strColumnName, chkdata.Checked)

    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the checked event happens.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSelectAllByRow_Clicked(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim chkdata As ImageButton = CType(sender, ImageButton)
        Dim containerGridViewRow As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
        Dim containerGridView As GridView = CType(containerGridViewRow.NamingContainer, GridView)
        Dim containerPage As Marketing = CType(containerGridViewRow.NamingContainer.NamingContainer.NamingContainer.NamingContainer, Marketing)
        containerPage.SelectAllByMaterials(CType(containerGridViewRow.Cells(0).Controls(0), Label).Text, True)

    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the checked event happens.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnDeSelectAllByRow_Clicked(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim chkdata As ImageButton = CType(sender, ImageButton)
        Dim containerGridViewRow As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
        Dim containerGridView As GridView = CType(containerGridViewRow.NamingContainer, GridView)
        Dim containerPage As Marketing = CType(containerGridViewRow.NamingContainer.NamingContainer.NamingContainer.NamingContainer, Marketing)
        containerPage.SelectAllByMaterials(CType(containerGridViewRow.Cells(0).Controls(0), Label).Text, False)

    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the checked event happens.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSelectAllByColumn_Clicked(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim chkdata As ImageButton = CType(sender, ImageButton)
        Dim containerGridViewRow As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
        Dim containerGridView As GridView = CType(containerGridViewRow.NamingContainer, GridView)
        Dim containerPage As Marketing = CType(containerGridViewRow.NamingContainer.NamingContainer.NamingContainer.NamingContainer, Marketing)
        containerPage.SelectAllByCountry(strColumnName, True)

    End Sub

    ''' <summary>
    ''' This is the event, which will be raised when the checked event happens.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnDeSelectAllByColumn_Clicked(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim chkdata As ImageButton = CType(sender, ImageButton)
        Dim containerGridViewRow As GridViewRow = CType(chkdata.NamingContainer, GridViewRow)
        Dim containerGridView As GridView = CType(containerGridViewRow.NamingContainer, GridView)
        Dim containerPage As Marketing = CType(containerGridViewRow.NamingContainer.NamingContainer.NamingContainer.NamingContainer, Marketing)
        containerPage.SelectAllByCountry(strColumnName, False)

    End Sub

End Class