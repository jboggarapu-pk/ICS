
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender


''' <summary>
''' Contains data access methods related to Audit Log Processing Model .
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
''' <term>Sujitha</term>
''' <description>
''' <para>09/25/2019</para>
''' <para>Implemented Coding Standards</para>
''' </description>
''' </item> 
''' </list>
''' </remarks> 
Public Class AuditLogModel
#Region "Variables Declaration"
    ''' <summary>
    ''' Variable to hold Change notification result
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum NotificationResult
        NoChangeToSend
        Sent
        SendError
        Disabled
    End Enum

    ''' <summary>
    ''' Variable to hold Approval Status. 
    ''' </summary>
    ''' <remarks></remarks>
    Private strApprovalStatus As String = "A"

    ''' <summary>
    ''' Variable to hold Deny Status. 
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenyStatus As String = "R"

    ''' <summary>
    ''' Variable to hold Pending Status. 
    ''' </summary>
    ''' <remarks></remarks>
    Private strPendingStatus As String = "P"

#End Region

#Region "Methods"

    ''' <summary>
    '''  Method to get Audit log.
    ''' </summary>
    ''' <param name="p_showPending">Show Pending</param>
    ''' <returns>List of Audit log</returns> 
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Function GetAuditLog(ByVal p_showPending As Boolean) As List(Of AuditLogEntry)

        Dim listAuditLog As New List(Of AuditLogEntry)
        Dim objEntry As AuditLogEntry = Nothing

        Dim dstAuditLog As DataSet = Depository.Current.GetAuditLog()
        Dim tmpDataViewMgr As DataViewManager
        Dim tmpDataView As DataView
        Try
            tmpDataViewMgr = dstAuditLog.DefaultViewManager
            tmpDataView = tmpDataViewMgr.CreateDataView(dstAuditLog.Tables(0))
            tmpDataView.Sort = NameAid.Column.ChangedDateTime

            For Each drw As DataRow In tmpDataView.Table.Rows

                If (p_showPending And drw(NameAid.Column.ApprovalStatus).ToString().ToUpper() = strPendingStatus) Or (Not p_showPending) Then
                    objEntry = New AuditLogEntry()

                    objEntry.ChangeLogID = CInt(drw(NameAid.Column.ChangeLogId))
                    objEntry.ChangeDateTime = CDate(drw(NameAid.Column.ChangedDateTime))
                    objEntry.ChangedBy = CStr(IIf(drw(NameAid.Column.ChangedBy) Is DBNull.Value, "<null>", drw(NameAid.Column.ChangedBy)))
                    objEntry.Area = CStr(IIf(drw(NameAid.Column.Area) Is DBNull.Value, "<null>", drw(NameAid.Column.Area)))
                    objEntry.ChangedFieldElement = CStr(IIf(drw(NameAid.Column.ChangedFiledElement) Is DBNull.Value, "<null>", drw(NameAid.Column.ChangedFiledElement)))
                    objEntry.OldValue = CStr(IIf(drw(NameAid.Column.OldValue) Is DBNull.Value, "<null>", drw(NameAid.Column.OldValue)))
                    objEntry.NewValue = CStr(IIf(drw(NameAid.Column.NewValue) Is DBNull.Value, "<null>", drw(NameAid.Column.NewValue)))

                    If drw(NameAid.Column.ApprovalStatus).ToString().ToUpper() = strApprovalStatus Then
                        objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Approved
                    ElseIf drw(NameAid.Column.ApprovalStatus).ToString().ToUpper() = strDenyStatus Then
                        objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Deny
                    Else
                        objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Pending
                    End If
                    listAuditLog.Add(objEntry)
                End If

            Next
        Catch
            Throw
        End Try
        Return listAuditLog

    End Function

    ''' <summary>
    '''  Method to get Approval Reasons
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <returns>Dataset with Approval reasons</returns> 
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Shared Function GetApprovalReasons(ByVal p_intCertificationTypeId As Integer) As DataSet
        Try
            Dim dstApprovalReasons As DataSet = Depository.Current.GetApprovalReasons(p_intCertificationTypeId)

            Return dstApprovalReasons
        Catch
            Throw
        End Try

    End Function

    ''' <summary>
    '''  Method to Refresh the list of entries (current session) with the updated status
    ''' </summary>
    ''' <param name="p_lstAuditLogEntry">Will be modified within this method</param>
    ''' <param name="p_lstChangeLogID"></param>
    ''' <param name="p_blnIsApproved"></param>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Sub RefreshAuditList(ByRef p_lstAuditLogEntry As List(Of AuditLogEntry), _
                                ByVal p_lstChangeLogID As List(Of Integer), _
                                ByVal p_blnIsApproved As Boolean)
        Try
            For Each objEntry As AuditLogEntry In p_lstAuditLogEntry
                If Not p_lstChangeLogID.Contains(objEntry.ChangeLogID) Then Continue For

                objEntry.ApprovalStatus = CType(IIf(p_blnIsApproved, AuditLogEntry.StatusOfChange.Approved, AuditLogEntry.StatusOfChange.Deny), AuditLogEntry.StatusOfChange)
            Next
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to update the database. Only the list (p_lstAuditLogEntry) of ID will be updated to the database.
    ''' </summary>
    ''' <param name="p_lstAuditLogEntry">List of Audit Log Entry</param>
    ''' <param name="p_dtmChangeDateTime">Change DateTime</param>
    ''' <param name="p_lstChangeLogID">List of Change Log Id</param>
    ''' <param name="p_blnIsApproved">Boolean Is Approved</param>
    ''' <returns>Boolean</returns> 
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Function UpdateAuditLog(ByVal p_lstAuditLogEntry As List(Of AuditLogEntry), _
                                   ByVal p_dtmChangeDateTime As DateTime, _
                                   ByVal p_lstChangeLogID As List(Of Integer), _
                                   ByVal p_blnIsApproved As Boolean) As Boolean

        Dim blnSaved As Boolean = False
        Try

            For Each entry As AuditLogEntry In p_lstAuditLogEntry
                If Not p_lstChangeLogID.Contains(entry.ChangeLogID) Then Continue For

                Dim status As String = CStr(IIf(p_blnIsApproved, strApprovalStatus, strDenyStatus))
                Dim strApprover As String = SecurityModel.GetUserName
                blnSaved = Depository.Current.UpdateAuditLogEntry(entry.ChangeLogID, p_dtmChangeDateTime, status, strApprover)

                If Not blnSaved Then Exit For
            Next
        Catch
            Throw
        End Try
        Return blnSaved

    End Function

    ''' <summary>
    ''' Method to get list of Audit log Pending count
    ''' </summary>
    ''' <param name="p_showPending">Show Pending</param> 
    ''' <returns>Integer of Pending Count</returns> 
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Function GetAuditLogPendingCount(ByVal p_showPending As Boolean) As Integer

        Dim intPendingCount As Integer = 0
        Try

            Dim objApprovalModel As New AuditLogModel()
            Dim listAuditLog As List(Of AuditLogEntry) = objApprovalModel.GetAuditLog(p_showPending)

            For Each ale As AuditLogEntry In listAuditLog
                If Not ale.ApprovalStatus = AuditLogEntry.StatusOfChange.Pending Then Continue For

                intPendingCount += 1
            Next
        Catch
            Throw
        End Try

        Return intPendingCount

    End Function

    ''' <summary>
    ''' Method to get list of Audit log after date
    ''' </summary>
    ''' <param name="p_dtmChangeDateTime">Change Datetime</param> 
    ''' <returns>List of Audit Log</returns> 
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Function GetAuditLogAfterDate(ByVal p_dtmChangeDateTime As DateTime) As List(Of AuditLogEntry)
        Dim dstAuditLog As DataSet = Nothing
        Dim dtbAuditLog As DataTable = Nothing
        Dim listAuditLog As List(Of AuditLogEntry) = Nothing
        Try
            dstAuditLog = Depository.Current.GetAuditLogAfterDate(p_dtmChangeDateTime)
            dtbAuditLog = dstAuditLog.Tables(0)
            listAuditLog = MapTableToAuditLog(dtbAuditLog)
        Catch
            Throw
        End Try
        Return listAuditLog

    End Function

    ''' <summary>
    ''' Method to get list of Audit log after date
    ''' </summary>
    ''' <param name="p_dtmChangeDateTime"></param>
    ''' <returns>List of Audit Results</returns> 
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>   
    Public Function GetAuditLogAfterDateAsText(ByVal p_dtmChangeDateTime As DateTime) As String
        Try
            Return GetAuditResultsAsText(GetAuditLogAfterDate(p_dtmChangeDateTime))
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Map table to audit log list
    ''' </summary>
    ''' <param name="p_dtbAuditLog">Audit Log</param>
    ''' <returns>List of Audit Log</returns> 
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>      
    Private Function MapTableToAuditLog(ByVal p_dtbAuditLog As DataTable) As List(Of AuditLogEntry)

        Dim listAuditLog As List(Of AuditLogEntry) = New List(Of AuditLogEntry)
        Dim objEntry As AuditLogEntry = Nothing
        Try
            For Each drw As DataRow In p_dtbAuditLog.Rows
                objEntry = New AuditLogEntry()

                objEntry.ChangeLogID = CInt(drw(NameAid.Column.ChangeLogId))
                objEntry.ChangeDateTime = CDate(drw(NameAid.Column.ChangedDateTime))
                objEntry.ChangedBy = CStr(IIf(drw(NameAid.Column.ChangedBy) Is DBNull.Value, "<null>", drw(NameAid.Column.ChangedBy)))
                objEntry.Area = CStr(IIf(drw(NameAid.Column.Area) Is DBNull.Value, "<null>", drw(NameAid.Column.Area)))
                objEntry.ChangedFieldElement = CStr(IIf(drw(NameAid.Column.ChangedFiledElement) Is DBNull.Value, "<null>", drw(NameAid.Column.ChangedFiledElement)))
                objEntry.OldValue = CStr(IIf(drw(NameAid.Column.OldValue) Is DBNull.Value, "<null>", drw(NameAid.Column.OldValue)))
                objEntry.NewValue = CStr(IIf(drw(NameAid.Column.NewValue) Is DBNull.Value, "<null>", drw(NameAid.Column.NewValue)))
                objEntry.Approver = CStr(IIf(drw(NameAid.Column.Approver) Is DBNull.Value, "<null>", drw(NameAid.Column.Approver)))
                objEntry.Note = CStr(IIf(drw(NameAid.Column.Note) Is DBNull.Value, "<null>", drw(NameAid.Column.Note)))

                If drw(NameAid.Column.ApprovalStatus).ToString().ToUpper() = strApprovalStatus Then
                    objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Approved
                ElseIf drw(NameAid.Column.ApprovalStatus).ToString().ToUpper() = strDenyStatus Then
                    objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Deny
                Else
                    objEntry.ApprovalStatus = AuditLogEntry.StatusOfChange.Pending
                End If
                listAuditLog.Add(objEntry)
            Next
        Catch
            Throw
        End Try
        Return listAuditLog

    End Function

    ''' <summary>
    ''' Method to Get audit results as text from the list
    ''' </summary>
    ''' <param name="listAuditEntries">List of Audit Entries</param>
    ''' <returns>List of Audit Log</returns> 
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Shared Function GetAuditResultsAsText(ByVal listAuditEntries As List(Of AuditLogEntry)) As String

        Dim strResults As String = String.Empty
        Dim strDelim As String = " ; "
        Dim strHTML As String = String.Empty
        Try
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
        Catch
            Throw
        End Try
        Return strHTML

    End Function

    ''' <summary>
    ''' Method to Send notification of changes since given time.
    ''' </summary>
    ''' <param name="p_enuAreaOfChange">enu Area of Change</param>
    ''' <param name="p_dtmSinceTime">Since Time</param>
    ''' <returns>enum value</returns> 
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>   
    Public Shared Function CheckForChangesAndSendNotification(ByVal p_enuAreaOfChange As AuditLogEntry.AreaOfChange, ByVal p_dtmSinceTime As DateTime) As NotificationResult

        Dim unuNotificationResult As NotificationResult = NotificationResult.NoChangeToSend

        Dim strSubject As String = "Changes in the area - " & p_enuAreaOfChange.ToString()

        Dim objApprovalModel As AuditLogModel = New AuditLogModel()
        Dim strMessage As String = String.Empty
        Try
            strMessage = objApprovalModel.GetAuditLogAfterDateAsText(p_dtmSinceTime)

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
        Catch
            Throw
        End Try
        Return unuNotificationResult

    End Function

    ''' <summary>
    ''' Method to Check for changes since given time
    ''' </summary>
    ''' <param name="p_enuAreaOfChange">enu Area of Change</param>
    ''' <param name="p_dtmSinceTime">Since Time</param>
    ''' <returns>List of Audit log</returns> 
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
    Public Shared Function CheckForChanges(ByVal p_enuAreaOfChange As AuditLogEntry.AreaOfChange, ByVal p_dtmSinceTime As DateTime) As List(Of AuditLogEntry)
        Dim objApprovalModel As AuditLogModel = New AuditLogModel()
        Dim listAuditLog As List(Of AuditLogEntry) = Nothing

        Try
            listAuditLog = objApprovalModel.GetAuditLogAfterDate(p_dtmSinceTime)
        Catch
            Throw
        End Try

        Return listAuditLog

    End Function

    ''' <summary>
    ''' Method to Audit objects and save results to be used as part of the transactional save
    ''' </summary>
    ''' <param name="p_lstAuditLog">LIst of Audit Log</param>
    ''' <returns>Boolean Value</returns> 
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
    Public Shared Function SaveResults(ByVal p_lstAuditLog As List(Of AuditLogEntry)) As Boolean

        Dim blnDone As Boolean = False
        Try
            For Each aleEntry As AuditLogEntry In p_lstAuditLog
                blnDone = Depository.Current.SaveAuditLogEntry(aleEntry.ChangeDateTime, aleEntry.ChangedBy, aleEntry.Area, aleEntry.ChangedFieldElement, aleEntry.OldValue, aleEntry.NewValue, aleEntry.ReasonID, aleEntry.Note)
            Next
        Catch
            Throw
        End Try
        Return blnDone

    End Function

#End Region

End Class
