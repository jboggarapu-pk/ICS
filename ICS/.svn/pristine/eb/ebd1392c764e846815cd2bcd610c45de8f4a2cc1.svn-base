Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Approval process view pesenter
''' </summary>
''' <remarks></remarks>
Public Class ApprovalPresenter
    Inherits ViewPresenterBase

#Region "Members"

    Private m_view As IApprovalView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IApprovalView)

        MyBase.New(p_view)

        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw New Exception("Error creating " + Me.ToString())
        End Try

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Attach presenter to view’s events.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SubscribeToEvents()

        AddHandler m_view.InitView, AddressOf OnInitView
        AddHandler m_view.ApproveSelected, AddressOf OnApproveSelected
        AddHandler m_view.DenySelected, AddressOf OnDenySelected

    End Sub

    ''' <summary>
    ''' Init data for the view
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overloads Sub OnInitView()

        Try
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error initialzing form data."
        End Try

    End Sub

    Private Sub LoadViewData()

        Dim objApprovalModel As New AuditLogModel()
        Dim blnPending As Boolean = True
        Dim listAuditLog As List(Of AuditLogEntry) = objApprovalModel.GetAuditLog(blnPending)
        m_view.AuditLogEntries = listAuditLog
        m_view.DataBindView()

    End Sub

    Private Sub OnApproveSelected(ByVal p_objListLogID As Object)

        UpdateSelectedEntriesStatus(p_objListLogID, True)

    End Sub

    Private Sub OnDenySelected(ByVal p_objListLogID As Object)

        UpdateSelectedEntriesStatus(p_objListLogID, False)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_objListLogID"></param>
    ''' <param name="p_blnIsApproved"></param>
    ''' <remarks></remarks>
    Private Sub UpdateSelectedEntriesStatus(ByVal p_objListLogID As Object, ByVal p_blnIsApproved As Boolean)

        Dim strInfoText As String = String.Empty
        m_view.ErrorText = String.Empty
        Dim enuNotificationResult As AuditLogModel.NotificationResult

        Try
            Dim lstChangeLogID As List(Of Integer) = CType(p_objListLogID, List(Of Integer))
            Dim dtmSaveTime As DateTime = DateTime.Now

            'fixing bug 24
            If lstChangeLogID.Count = 0 Then
                m_view.ErrorText = "Please select a certificate from the list before proceed to approve or deny the certificate"
            Else
                Dim blnSaved As Boolean = UpdateRefreshAuditLog(lstChangeLogID, p_blnIsApproved)
                strInfoText = IIf(blnSaved, "Updated.", "No updates.")
            End If

            enuNotificationResult = AuditLogModel.CheckForChangesAndSendNotification(AuditLogEntry.AreaOfChange.Approval, dtmSaveTime)
            strInfoText &= " "
            If enuNotificationResult = AuditLogModel.NotificationResult.Sent Then
                strInfoText &= "Notification sent."
            ElseIf enuNotificationResult = AuditLogModel.NotificationResult.SendError Then
                strInfoText &= "Notification failed."
            ElseIf enuNotificationResult = AuditLogModel.NotificationResult.Disabled Then
                strInfoText &= "Notification disabled."
            End If

            m_view.InfoText = strInfoText
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error on Approve / Deny Selected."
        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_lstChangeLogID"></param>
    ''' <param name="p_blnIsApproved"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateRefreshAuditLog(ByVal p_lstChangeLogID As List(Of Integer), ByVal p_blnIsApproved As Boolean) As Boolean

        Dim blnDone As Boolean = False

        If p_lstChangeLogID.Count > 0 Then

            Dim objAuditLogModel As New AuditLogModel()
            blnDone = objAuditLogModel.UpdateAuditLog(m_view.AuditLogEntries, DateTime.Now, p_lstChangeLogID, p_blnIsApproved)
            If blnDone Then
                objAuditLogModel.RefreshAuditList(m_view.AuditLogEntries, p_lstChangeLogID, p_blnIsApproved)
            End If

        End If

        Return blnDone

    End Function

#End Region

End Class
