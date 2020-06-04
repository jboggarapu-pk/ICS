Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

Partial Public Class EditCertificationUC
    Inherits BaseUserControl
    Implements IEditMaterialMaintView

#Region "Members"
    ''' <summary>
    ''' Reload View Data event.
    ''' </summary>
    Public Event ReloadViewData As CustomEvents.PlainEventHandler Implements IEditMaterialMaintView.ReloadViewData
    ''' <summary>
    ''' Edit Material event.
    ''' </summary>
    Public Event EditMaterial As CustomEvents.PlainArgumentEventHandler Implements IEditMaterialMaintView.EditMaterial
    ''' <summary>
    ''' Show Material event.
    ''' </summary>
    Public Event ShowMaterial As CustomEvents.PlainEventHandler Implements IEditMaterialMaintView.ShowMaterial
    ''' <summary>
    ''' Update Material event.
    ''' </summary>
    Public Event UpdateMaterial As CustomEvents.PlainEventHandler Implements IEditMaterialMaintView.UpdateMaterial
    ''' <summary>
    ''' Cancel Material event.
    ''' </summary>
    Public Event CancelMaterial As CustomEvents.PlainEventHandler Implements IEditMaterialMaintView.CancelMaterial
    ''' <summary>
    '''  Edit Material Maint presenter.
    ''' </summary>
    Private m_presenter As EditMaterialMaintPresenter
    ''' <summary>
    '''  List Imark family.
    ''' </summary>
    Private m_lstIMarkFamily As List(Of EditMaterialMaint)

#End Region

#Region "Constructors"
    ''' <summary>
    ''' Default Constructor to initialize class members.
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()
        m_presenter = New EditMaterialMaintPresenter(Me)
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets SuccessText value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>SuccessText</returns>
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property SuccessText() As String Implements Presenter.IEditMaterialMaintView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets ErrorText value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>ErrorText</returns>
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements Presenter.IEditMaterialMaintView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets SKUID value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>SKUID</returns>
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property SKUID() As Integer Implements Presenter.IEditMaterialMaintView.SKUID
        Get
            Return CInt(txtSKUId.Text)
        End Get
        Set(ByVal value As Integer)
            txtSKUId.Text = CStr(value)
        End Set
    End Property

    ''' <summary>
    ''' SKU.
    ''' </summary>
    ''' <value></value>
    ''' <returns>SKU</returns>
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property SKU() As String Implements Presenter.IEditMaterialMaintView.SKU
        Get
            Return txtSKU.Text
        End Get
        Set(ByVal value As String)
            txtSKU.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Speed rating.
    ''' </summary>
    ''' <value>String</value>
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property MaterialNumber() As String Implements Presenter.IEditMaterialMaintView.MaterialNumber
        Get
            Return txtMaterialNumber.Text.TrimStart("0"c)
        End Get
        Set(ByVal value As String)
            txtMaterialNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Material Number Input.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property MaterialMaint() As DataTable Implements Presenter.IEditMaterialMaintView.MaterialMaint
        Get
            Return CType(Session("MaterialMaint"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("MaterialMaint") = value
            gvMaterialMaint.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Setup view data if certificate is new.
    ''' </summary>
    ''' <param name="p_blnEditMaterialMaint">Edit Material Maint</param>
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupViewData(ByVal p_blnEditMaterialMaint As Boolean) Implements Presenter.IEditMaterialMaintView.SetupViewData
        Try
            If (p_blnEditMaterialMaint = True) Then
                RaiseEvent ReloadViewData()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This event fires when we click delete/edit link of any row.
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Grid_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Const EditMaterialMaintText As String = "EditMaterialMaint"
        Try
            If e.CommandName = EditMaterialMaintText Then
                RaiseEvent EditMaterial(e.CommandArgument)
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Show Material Maint Details.
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnShowMatNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowMatNo.Click
        Try
            RaiseEvent ShowMaterial()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Update Material Maint.
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnUpdateMaterialNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateMaterialNo.Click
        Try
            RaiseEvent UpdateMaterial()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Cancel Button click.
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            RaiseEvent CancelMaterial()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Hide/Unhide controls.
    ''' </summary>
    ''' <param name="p_blnGridStatus">Grid Status</param>
    ''' <param name="p_blnDetailsStatus">Details Status</param>
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
    ''' <para>11/01/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub HideMatlMaintPanel(ByVal p_blnGridStatus As Boolean, ByVal p_blnDetailsStatus As Boolean) Implements Presenter.IEditMaterialMaintView.HideMatlMaintPanel
        Try
            tblMain.Visible = p_blnGridStatus
            divMaterialMaint.Visible = p_blnGridStatus
            lblMessage.Visible = p_blnGridStatus
            divEditMatMaintDetails.Visible = p_blnDetailsStatus
        Catch
            Throw
        End Try
    End Sub
#End Region

End Class