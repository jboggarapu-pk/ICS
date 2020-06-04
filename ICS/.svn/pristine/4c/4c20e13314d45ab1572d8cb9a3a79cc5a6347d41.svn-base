Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

Partial Public Class EditCertificationUC
    Inherits BaseUserControl
    Implements IEditMaterialMaintView

#Region "Members"
    Public Event ReloadViewData As CustomEvents.PlainEventHandler Implements IEditMaterialMaintView.ReloadViewData
    Public Event EditMaterial As CustomEvents.PlainArgumentEventHandler Implements IEditMaterialMaintView.EditMaterial
    Public Event ShowMaterial As CustomEvents.PlainEventHandler Implements IEditMaterialMaintView.ShowMaterial
    Public Event UpdateMaterial As CustomEvents.PlainEventHandler Implements IEditMaterialMaintView.UpdateMaterial
    Public Event CancelMaterial As CustomEvents.PlainEventHandler Implements IEditMaterialMaintView.CancelMaterial

    Private m_presenter As EditMaterialMaintPresenter
    Private m_lstIMarkFamily As List(Of EditMaterialMaint)

#End Region

#Region "Constructors"

    Public Sub New()
        m_presenter = New EditMaterialMaintPresenter(Me)
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' SuccessText
    ''' </summary>
    ''' <value></value>
    ''' <returns>SuccessText</returns>
    ''' <remarks></remarks>
    Public Property SuccessText() As String Implements Presenter.IEditMaterialMaintView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' ErrorText
    ''' </summary>
    ''' <value></value>
    ''' <returns>ErrorText</returns>
    ''' <remarks></remarks>
    Public Property ErrorText() As String Implements Presenter.IEditMaterialMaintView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' SKUID
    ''' </summary>
    ''' <value></value>
    ''' <returns>SKUID</returns>
    ''' <remarks></remarks>
    Public Property SKUID() As Integer Implements Presenter.IEditMaterialMaintView.SKUID
        Get
            Return txtSKUId.Text
        End Get
        Set(ByVal value As Integer)
            txtSKUId.Text = value
        End Set
    End Property

    ''' <summary>
    ''' SKU
    ''' </summary>
    ''' <value></value>
    ''' <returns>SKU</returns>
    ''' <remarks></remarks>
    Public Property SKU() As String Implements Presenter.IEditMaterialMaintView.SKU
        Get
            Return txtSKU.Text
        End Get
        Set(ByVal value As String)
            txtSKU.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Speed rating
    ''' </summary>
    ''' <value>String</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Speedrating() As String Implements Presenter.IEditMaterialMaintView.Speedrating
        Get
            Return txtSpeedrating.Text
        End Get
        Set(ByVal value As String)
            txtSpeedrating.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Material Number.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property MaterialNumber() As String Implements Presenter.IEditMaterialMaintView.MaterialNumber
        Get
            Return txtMaterialNumber.Text.TrimStart("0"c)
        End Get
        Set(ByVal value As String)
            txtMaterialNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Material Number Input
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property MatNumberInput() As String Implements Presenter.IEditMaterialMaintView.MatNumberInput
        Get
            Return txtMatNo.Text
        End Get
        Set(ByVal value As String)
            txtMatNo.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Material Maintainance.
    ''' </summary>
    ''' <value></value>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Property MaterialMaint() As DataTable Implements Presenter.IEditMaterialMaintView.MaterialMaint
        Get
            Return Session("MaterialMaint")
        End Get
        Set(ByVal value As DataTable)
            Session("MaterialMaint") = value
            gvMaterialMaint.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Setup view data if certificate is new
    ''' </summary>
    ''' <param name="p_blnEditMaterialMaint"></param>
    ''' <remarks></remarks>
    Public Sub SetupViewData(ByVal p_blnEditMaterialMaint As Boolean) Implements Presenter.IEditMaterialMaintView.SetupViewData
        If (p_blnEditMaterialMaint = True) Then
            RaiseEvent ReloadViewData()
        End If
    End Sub

    ''' <summary>
    ''' This event fires when we click delete/edit link of any row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Grid_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "EditMaterialMaint" Then
            RaiseEvent EditMaterial(e.CommandArgument)
        End If
    End Sub

    ''' <summary>
    ''' Show Material Maint Details
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnShowMatNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowMatNo.Click
        RaiseEvent ShowMaterial()
    End Sub

    ''' <summary>
    ''' Update Material Maint
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdateMaterialNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateMaterialNo.Click
        RaiseEvent UpdateMaterial()
    End Sub

    ''' <summary>
    ''' Close the Edit material Div
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        RaiseEvent CancelMaterial()
    End Sub

    ''' <summary>
    ''' Hide/Unhide controls
    ''' </summary>
    ''' <param name="p_blnGridStatus"></param>
    ''' <param name="p_blnDetailsStatus"></param>
    ''' <remarks></remarks>
    Protected Sub HideMatlMaintPanel(ByVal p_blnGridStatus As Boolean, ByVal p_blnDetailsStatus As Boolean) Implements Presenter.IEditMaterialMaintView.HideMatlMaintPanel
        tblMain.Visible = p_blnGridStatus
        divMaterialMaint.Visible = p_blnGridStatus
        lblMessage.Visible = p_blnGridStatus
        divEditMatMaintDetails.Visible = p_blnDetailsStatus
    End Sub

#End Region

End Class