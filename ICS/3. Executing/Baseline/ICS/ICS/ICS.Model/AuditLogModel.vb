
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender
Imports TRACSSharedDatasets

''' <summary>
''' Audit log processing model
''' </summary>
''' <remarks></remarks>
Public Class AuditLogModel

    ''' <summary>
    ''' Change notification result
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum NotificationResult
        NoChangeToSend
        Sent
        SendError
        Disabled
    End Enum

#Region "Methods"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_showPending"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAuditLog(ByVal p_showPending As Boolean) As List(Of AuditLogEntry)

        Dim listAuditLog As New List(Of AuditLogEntry)
        Dim objEntry As AuditLogEntry = Nothing

        Dim dstAuditLog As DataSet = Depository.Current.GetAuditLog()
        Dim tmpDataViewMgr As DataViewManager
        Dim tmpDataView As DataView

        tmpDataViewMgr = dstAuditLog.DefaultViewManager
        tmpDataView = tmpDataViewMgr.CreateDataView(dstAuditLog.Tables(0))
        tmpDataView.Sort = NameAid.Column.ChangedDateTime

        For Each drw As DataRow In tmpDataView.Table.Rows

            If (p_showPending And drw(NameAid.Column.ApprovalStatus).ToString().ToUpper() = "P") Or (Not p_showPending) Then
                objEntry = New AuditLogEntry()

                objEntry.ChangeLogID = drw(NameAid.Column.ChangeLogId)
                objEntry.ChangeDateTime = drw(NameAid.Column.ChangedDateTime)
                objEntry.ChangedBy = IIf(drw(NameAid.Column.ChangedBy) Is DBNull.Value, "<null>", drw(NameAid.Column.ChangedBy))
                objEntry.Area = IIf(drw(NameAid.Column.Area) Is DBNull.Value, "<null>", drw(NameAid.Column.Area))
                objEntry.ChangedFieldElement = IIf(drw(NameAid.Column.ChangedFiledElement) Is DBNull.Value, "<null>", drw(NameAid.Column.ChangedFiledElement))
                objEntry.OldValue = IIf(drw(NameAid.Column.OldValue) Is DBNull.Value, "<null>", drw(NameAid.Column.OldValue))
                objEntry.NewValue = IIf(drw(NameAid.Column.NewValue) Is DBNull.Value, "<null>", drw(NameAid.Column.NewValue))

                If drw(NameAid.Column.ApprovalStatus).ToString().ToUpper() = "A" Then
                    objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Approved
                ElseIf drw(NameAid.Column.ApprovalStatus).ToString().ToUpper() = "R" Then
                    objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Deny
                Else
                    objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Pending
                End If
                listAuditLog.Add(objEntry)
            End If

        Next

        Return listAuditLog

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetApprovalReasons(ByVal p_intCertificationTypeId As Integer) As DataSet

        Dim dstApprovalReasons As DataSet = Depository.Current.GetApprovalReasons(p_intCertificationTypeId)

        Return dstApprovalReasons

    End Function

    ''' <summary>
    ''' Refresh the list of entries (current session) with the updated status
    ''' </summary>
    ''' <param name="p_lstAuditLogEntry">Will be modified within this method</param>
    ''' <param name="p_lstChangeLogID"></param>
    ''' <param name="p_blnIsApproved"></param>
    ''' <remarks></remarks>
    Public Sub RefreshAuditList(ByRef p_lstAuditLogEntry As List(Of AuditLogEntry), _
                                ByVal p_lstChangeLogID As List(Of Integer), _
                                ByVal p_blnIsApproved As Boolean)

        For Each objEntry As AuditLogEntry In p_lstAuditLogEntry
            If Not p_lstChangeLogID.Contains(objEntry.ChangeLogID) Then Continue For

            objEntry.ApprovalStatus = IIf(p_blnIsApproved, AuditLogEntry.StatusOfChange.Approved, AuditLogEntry.StatusOfChange.Deny)
        Next

    End Sub

    ''' <summary>
    ''' Method to update the database. Only the list (p_lstAuditLogEntry) of ID will be updated to the database.
    ''' </summary>
    ''' <param name="p_lstAuditLogEntry"></param>
    ''' <param name="p_lstChangeLogID"></param>
    ''' <param name="p_blnIsApproved"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateAuditLog(ByVal p_lstAuditLogEntry As List(Of AuditLogEntry), _
                                   ByVal p_dtmChangeDateTime As DateTime, _
                                   ByVal p_lstChangeLogID As List(Of Integer), _
                                   ByVal p_blnIsApproved As Boolean) As Boolean

        Dim blnSaved As Boolean = False

        For Each entry As AuditLogEntry In p_lstAuditLogEntry
            If Not p_lstChangeLogID.Contains(entry.ChangeLogID) Then Continue For

            Dim status As String = IIf(p_blnIsApproved, "A", "R")
            Dim strApprover As String = SecurityModel.GetUserName
            blnSaved = Depository.Current.UpdateAuditLogEntry(entry.ChangeLogID, p_dtmChangeDateTime, status, strApprover)

            If Not blnSaved Then Exit For
        Next

        Return blnSaved

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAuditLogPendingCount(ByVal p_showPending As Boolean) As Integer

        Dim intPendingCount As Integer = 0
        Dim objApprovalModel As New AuditLogModel()
        Dim listAuditLog As List(Of AuditLogEntry) = objApprovalModel.GetAuditLog(p_showPending)

        For Each ale As AuditLogEntry In listAuditLog
            If Not ale.ApprovalStatus = AuditLogEntry.StatusOfChange.Pending Then Continue For

            intPendingCount += 1
        Next

        Return intPendingCount

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_dtmChangeDateTime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAuditLogAfterDate(ByVal p_dtmChangeDateTime As DateTime) As List(Of AuditLogEntry)

        Dim dstAuditLog As DataSet = Depository.Current.GetAuditLogAfterDate(p_dtmChangeDateTime)
        Dim dtbAuditLog As DataTable = dstAuditLog.Tables(0)

        Dim listAuditLog As List(Of AuditLogEntry) = MapTableToAuditLog(dtbAuditLog)
        Return listAuditLog

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_dtmChangeDateTime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAuditLogAfterDateAsText(ByVal p_dtmChangeDateTime As DateTime) As String

        Return GetAuditResultsAsText(GetAuditLogAfterDate(p_dtmChangeDateTime))

    End Function

    ''' <summary>
    ''' Map table to auditlog list
    ''' </summary>
    ''' <param name="p_dtbAuditLog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MapTableToAuditLog(ByVal p_dtbAuditLog As DataTable) As List(Of AuditLogEntry)

        Dim listAuditLog As List(Of AuditLogEntry) = New List(Of AuditLogEntry)
        Dim objEntry As AuditLogEntry = Nothing

        For Each drw As DataRow In p_dtbAuditLog.Rows
            objEntry = New AuditLogEntry()

            objEntry.ChangeLogID = drw(NameAid.Column.ChangeLogId)
            objEntry.ChangeDateTime = drw(NameAid.Column.ChangedDateTime)
            objEntry.ChangedBy = IIf(drw(NameAid.Column.ChangedBy) Is DBNull.Value, "<null>", drw(NameAid.Column.ChangedBy))
            objEntry.Area = IIf(drw(NameAid.Column.Area) Is DBNull.Value, "<null>", drw(NameAid.Column.Area))
            objEntry.ChangedFieldElement = IIf(drw(NameAid.Column.ChangedFiledElement) Is DBNull.Value, "<null>", drw(NameAid.Column.ChangedFiledElement))
            objEntry.OldValue = IIf(drw(NameAid.Column.OldValue) Is DBNull.Value, "<null>", drw(NameAid.Column.OldValue))
            objEntry.NewValue = IIf(drw(NameAid.Column.NewValue) Is DBNull.Value, "<null>", drw(NameAid.Column.NewValue))
            objEntry.Approver = IIf(drw(NameAid.Column.Approver) Is DBNull.Value, "<null>", drw(NameAid.Column.Approver))
            objEntry.Note = IIf(drw(NameAid.Column.Note) Is DBNull.Value, "<null>", drw(NameAid.Column.Note))

            If drw(NameAid.Column.ApprovalStatus).ToString().ToUpper() = "A" Then
                objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Approved
            ElseIf drw(NameAid.Column.ApprovalStatus).ToString().ToUpper() = "R" Then
                objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Deny
            Else
                objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Pending
            End If
            listAuditLog.Add(objEntry)
        Next

        Return listAuditLog

    End Function

    ''' <summary>
    ''' Get audit results as text from the list
    ''' </summary>
    ''' <param name="listAuditEntries"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAuditResultsAsText(ByVal listAuditEntries As List(Of AuditLogEntry)) As String

        Dim strResults As String = String.Empty
        Dim strDelim As String = " ; "
        Dim strHTML As String = String.Empty

        For Each aleEntry As AuditLogEntry In listAuditEntries
            If Not aleEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Approved Or InStr(aleEntry.Area, "Marketing") > 0 Then
                If strHTML = String.Empty Then
                    strHTML = "<html>"
                    strHTML &= "<body>"
                    strHTML &= "<table border=1>"
                    strHTML &= "<tr style=background-color:lightsteelblue>"
                    If InStr(aleEntry.Area.ToUpper(), "CERTIFICATION") <= 0 Then
                        strHTML &= "<td>Area</td><td>Change Date</td><td>Changed By</td><td>Changed Field</td><td>Note</td><td>Status</td><td>Approver</td>"
                    Else
                        strHTML &= "<td>Area</td><td>Change Date</td><td>Changed By</td><td>Changed Field</td><td>Old Value</td><td>New Value</td><td>Note</td><td>Status</td><td>Approver</td>"
                    End If
                    strHTML &= "</tr>"
                End If
                strHTML &= "<tr>"
                strHTML &= "<td>" & aleEntry.Area & "</td>"
                strHTML &= "<td>" & aleEntry.ChangeDateTime.ToString() & "</td>"
                strHTML &= "<td>" & aleEntry.ChangedBy & "</td>"
                strHTML &= "<td>" & aleEntry.ChangedFieldElement & "</td>"
                If InStr(aleEntry.Area.ToUpper(), "CERTIFICATION") > 0 Then
                    strHTML &= "<td>" & aleEntry.OldValue & "</td>"
                    strHTML &= "<td>" & aleEntry.NewValue & "</td>"
                End If
                strHTML &= "<td>" & aleEntry.Note & "</td>"
                'strHTML &= "<td>" & aleEntry.ApprovalStatus & "</td>"
                strHTML &= "<td>" & aleEntry.Approver & "</td>"
                strHTML &= "</tr>"
            End If
        Next

        'If no audit entries then string will be empty so we don't want to add the closing tags for the html
        If strHTML <> String.Empty Then
            strHTML &= "</table>"
            strHTML &= "</body>"
            strHTML &= "</html>"
        End If

        Return strHTML

    End Function

    ''' <summary>
    ''' Send notification of changes since given time
    ''' </summary>
    ''' <param name="p_enuAreaOfChange"></param>
    ''' <param name="p_dtmSinceTime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CheckForChangesAndSendNotification(ByVal p_enuAreaOfChange As AuditLogEntry.AreaOfChange, ByVal p_dtmSinceTime As DateTime) As NotificationResult

        Dim unuNotificationResult As NotificationResult = NotificationResult.NoChangeToSend

        Dim strSubject As String = "Changes in the area - " & p_enuAreaOfChange.ToString()

        Dim objApprovalModel As AuditLogModel = New AuditLogModel()
        Dim strMessage As String = objApprovalModel.GetAuditLogAfterDateAsText(p_dtmSinceTime)

        If Not String.IsNullOrEmpty(strMessage) Then
            If p_enuAreaOfChange = AuditLogEntry.AreaOfChange.Marketing Then
                Dim enuSendResult As Emailer.SendResult = Emailer.SendMarketing(strSubject, strMessage)

                Select Case enuSendResult
                    Case Emailer.SendResult.Sucess
                        unuNotificationResult = NotificationResult.Sent
                    Case Emailer.SendResult.Failure
                        unuNotificationResult = NotificationResult.SendError
                    Case Emailer.SendResult.Disabled
                        unuNotificationResult = NotificationResult.Disabled
                    Case Else
                        ' Default
                End Select
            Else
                Dim enuSendResult As Emailer.SendResult = Emailer.Send(strSubject, strMessage)

                Select Case enuSendResult
                    Case Emailer.SendResult.Sucess
                        unuNotificationResult = NotificationResult.Sent
                    Case Emailer.SendResult.Failure
                        unuNotificationResult = NotificationResult.SendError
                    Case Emailer.SendResult.Disabled
                        unuNotificationResult = NotificationResult.Disabled
                    Case Else
                        ' Default
                End Select
            End If
        End If

        Return unuNotificationResult

    End Function

    ''' <summary>
    ''' Check for changes since given time
    ''' </summary>
    ''' <param name="p_enuAreaOfChange"></param>
    ''' <param name="p_dtmSinceTime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CheckForChanges(ByVal p_enuAreaOfChange As AuditLogEntry.AreaOfChange, ByVal p_dtmSinceTime As DateTime) As List(Of AuditLogEntry)
        Dim objApprovalModel As AuditLogModel = New AuditLogModel()
        Dim listAuditLog As List(Of AuditLogEntry) = objApprovalModel.GetAuditLogAfterDate(p_dtmSinceTime)

        Return listAuditLog

    End Function

    ''' <summary>
    ''' Audit objects and save results
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>to be used as part of the transactional save</remarks>
    Public Shared Function SaveResults(ByVal p_lstAuditLog As List(Of AuditLogEntry)) As Boolean

        Dim blnDone As Boolean = False

        For Each aleEntry As AuditLogEntry In p_lstAuditLog
            blnDone = Depository.Current.SaveAuditLogEntry(aleEntry.ChangeDateTime, aleEntry.ChangedBy, aleEntry.Area, aleEntry.ChangedFieldElement, aleEntry.OldValue, aleEntry.NewValue, aleEntry.ReasonID, aleEntry.Note)
        Next

        Return blnDone

    End Function

#End Region

End Class
