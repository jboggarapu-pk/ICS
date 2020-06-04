Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities
Imports System.Web.UI
Imports System.Collections.Generic

' Implements IApprovalView to present Approval process data
Partial Public Class Approval
    Inherits BasePage
    Implements IApprovalView

#Region "Members and Constants"
    ''' <summary>
    '''  Approval process view presenter
    ''' </summary>
    Private m_presenter As ApprovalPresenter
    ''' <summary>
    '''  Approve selected event
    ''' </summary>
    Public Event ApproveSelected(ByVal p_object As Object) Implements Presenter.IApprovalView.ApproveSelected
    ''' <summary>
    '''  Deny selected event
    ''' </summary>
    Public Event DenySelected(ByVal p_object As Object) Implements Presenter.IApprovalView.DenySelected
    ''' <summary>
    '''  Constant to hold AuditLog text
    ''' </summary>
    Private Const Audilogtext As String = "AuditLogEntrySelector"
#End Region

#Region "Properties"
    ''' <summary>
    '''  Gets or Sets Error text property
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements Presenter.IApprovalView.ErrorText
        Get
            Return lblError.Text
        End Get
        Set(ByVal value As String)
            lblError.Text = value
        End Set
    End Property
    ''' <summary>
    '''  Gets or Sets Info text property
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property InfoText() As String Implements Presenter.IApprovalView.InfoText
        Get
            Return lblInfo.Text
        End Get
        Set(ByVal value As String)
            lblInfo.Text = value
        End Set
    End Property
    ''' <summary>
    '''  Audit log entries property
    ''' </summary>
    ''' <returns>
    ''' Gets or Sets AuditLogEntries
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property AuditLogEntries() As List(Of DomainEntities.AuditLogEntry) Implements Presenter.IApprovalView.AuditLogEntries
        Get
            Return CType(Session("AuditLogEntries"), Global.System.Collections.Generic.List(Of Global.CooperTire.ICS.DomainEntities.AuditLogEntry))
        End Get
        Set(ByVal value As List(Of DomainEntities.AuditLogEntry))
            Session("AuditLogEntries") = value
            grdAuditLog.DataSource = value
            grdAuditLog.DataBind()
        End Set
    End Property

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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New ApprovalPresenter(Me)

    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    '''  Method to check page index changing in grid audit log.
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub PageIndexChanging_grdAuditLog(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            grdAuditLog.PageIndex = e.NewPageIndex
            grdAuditLog.DataSource = AuditLogEntries
            grdAuditLog.DataBind()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for Approve Select button click.
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnApproveSelected(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim listLogID As New List(Of Integer)
        Try
            listLogID = ScanDataSource()
            RaiseEvent ApproveSelected(listLogID)
        Catch
            Throw
        End Try
    End Sub
    ''' <summary>
    '''  Method for Deny Select button click.
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnDenySelected(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim listLogID As New List(Of Integer)
        Try
            listLogID = ScanDataSource()
            RaiseEvent DenySelected(listLogID)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for Approved click.
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Approved_clicked(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim listLogID As List(Of Integer) = FindChangeLogID(sender)
        Try
            RaiseEvent ApproveSelected(listLogID)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method for Deny click.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Deny_clicked(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim listLogID As List(Of Integer) = FindChangeLogID(sender)
        Try
            RaiseEvent DenySelected(listLogID)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' First find the row position in the grid.
    ''' Then find the position within the session object of the list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list> 
    ''' </remarks>
    Private Function FindChangeLogID(ByVal sender As System.Object) As List(Of Integer)
        Dim listLogID As New List(Of Integer)
        Dim obj As DataControlFieldCell = CType(DirectCast(sender, System.Web.UI.WebControls.ImageButton).Parent, DataControlFieldCell)        
        Dim obj2 As GridViewRow = CType(obj.Parent, GridViewRow)
        Dim pos As Integer = obj2.DataItemIndex
        Try
            Dim id As Integer = AuditLogEntries(pos).ChangeLogID
            listLogID.Add(id)
            Return listLogID
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Identify all checkboxes that were check by the user
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function ScanDataSource() As List(Of Integer)

        Dim listLogID As New List(Of Integer)
        Dim listAuditLogEntry As List(Of AuditLogEntry) = AuditLogEntries

        Try
            For Each row As GridViewRow In grdAuditLog.Rows
                Dim pos As Integer = row.DataItemIndex
                Dim cb As CheckBox = CType(row.FindControl(Audilogtext), CheckBox)
                If (cb.Checked) Then
                    Dim changeId As String = CStr(listAuditLogEntry(pos).ChangeLogID)

                    listLogID.Add(CInt(changeId))
                End If

            Next
            Return listLogID
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method for selected check change click.
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
    ''' <para>09/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub click_selCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each row As GridViewRow In grdAuditLog.Rows
                Dim cb As CheckBox = CType(row.FindControl(Audilogtext), CheckBox)
                cb.Checked = CType(sender, CheckBox).Checked
            Next
        Catch
            Throw

        End Try
    End Sub
#End Region

End Class
