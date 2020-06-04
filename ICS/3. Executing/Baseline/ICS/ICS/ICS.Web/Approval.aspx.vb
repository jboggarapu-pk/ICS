Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities
Imports System.Web.UI
Imports System.Collections.Generic

' Implements IApprovalView to present Approval process data
Partial Public Class Approval
    Inherits BasePage
    Implements IApprovalView

#Region "Members"

    Private m_presenter As ApprovalPresenter

    Public Event ApproveSelected(ByVal p_object As Object) Implements Presenter.IApprovalView.ApproveSelected
    Public Event DenySelected(ByVal p_object As Object) Implements Presenter.IApprovalView.DenySelected

#End Region

#Region "Properties"

    Public Property ErrorText() As String Implements Presenter.IApprovalView.ErrorText
        Get
            Return lblError.Text
        End Get
        Set(ByVal value As String)
            lblError.Text = value
        End Set
    End Property

    Public Property InfoText() As String Implements Presenter.IApprovalView.InfoText
        Get
            Return lblInfo.Text
        End Get
        Set(ByVal value As String)
            lblInfo.Text = value
        End Set
    End Property

    Public Property AuditLogEntries() As List(Of DomainEntities.AuditLogEntry) Implements Presenter.IApprovalView.AuditLogEntries
        Get
            Return Session("AuditLogEntries")
        End Get
        Set(ByVal value As List(Of DomainEntities.AuditLogEntry))
            Session("AuditLogEntries") = value
            grdAuditLog.DataSource = value
            grdAuditLog.DataBind()
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New ApprovalPresenter(Me)

    End Sub

#End Region

#Region "Methods"

    Protected Sub PageIndexChanging_grdAuditLog(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)

        grdAuditLog.PageIndex = e.NewPageIndex
        grdAuditLog.DataSource = AuditLogEntries
        grdAuditLog.DataBind()

    End Sub

    Protected Sub Click_btnApproveSelected(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim listLogID As New List(Of Integer)
        listLogID = ScanDataSource()

        RaiseEvent ApproveSelected(listLogID)

    End Sub

     Protected Sub Click_btnDenySelected(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim listLogID As New List(Of Integer)
        listLogID = ScanDataSource()
        RaiseEvent DenySelected(listLogID)

    End Sub

    Protected Sub Approved_clicked(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim listLogID As List(Of Integer) = FindChangeLogID(sender)

        RaiseEvent ApproveSelected(listLogID)

   End Sub

   Protected Sub Deny_clicked(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim listLogID As List(Of Integer) = FindChangeLogID(sender)
        RaiseEvent DenySelected(listLogID)

   End Sub

    ''' <summary>
    ''' First find the row position in the grid.
    ''' Then find the position within the session object of the list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FindChangeLogID(ByVal sender As System.Object) As List(Of Integer)

        Dim listLogID As New List(Of Integer)
        Dim obj As DataControlFieldCell = CType(sender.Parent, DataControlFieldCell)
        Dim obj2 As GridViewRow = obj.Parent
        Dim pos = obj2.DataItemIndex
        Dim id As Integer = AuditLogEntries(pos).ChangeLogID
        listLogID.Add(id)
        Return listLogID

    End Function

    ''' <summary>
    ''' Identify all checkboxes that were check by the user
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ScanDataSource() As List(Of Integer)

        Dim listLogID As New List(Of Integer)
        Dim listAuditLogEntry As List(Of AuditLogEntry) = AuditLogEntries


        For Each row As GridViewRow In grdAuditLog.Rows
            Dim pos = row.DataItemIndex
            Dim cb As CheckBox = row.FindControl("AuditLogEntrySelector")
            If (cb.Checked) Then
                Dim changeId As String = listAuditLogEntry(pos).ChangeLogID

                listLogID.Add(changeId)
            End If

        Next

        Return listLogID

    End Function

    Public Sub click_selCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        For Each row As GridViewRow In grdAuditLog.Rows
            Dim cb As CheckBox = row.FindControl("AuditLogEntrySelector")
            cb.Checked = CType(sender, CheckBox).Checked
        Next


    End Sub
 
#End Region

 
End Class
