Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Approval process view presenter
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
''' <para>10/15/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>s
''' </remarks>
Public Class ApprovalPresenter
    Inherits ViewPresenterBase

#Region "Members"
    ''' <summary>
    '''  Approval interface to the approval form view.
    ''' </summary>
    Private m_view As IApprovalView

#End Region

#Region "Constructors"
    ''' <summary>
    '''  Custom Constructor to initialize class members.
    ''' </summary>
    ''' <param name="p_view">View</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As IApprovalView)

        MyBase.New(p_view)
        Const ErrorCreatingText As String = "Error creating "
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw New Exception(ErrorCreatingText + Me.ToString())
        End Try

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Attach presenter to view�s events.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()
        Try
            AddHandler m_view.InitView, AddressOf OnInitView
            AddHandler m_view.ApproveSelected, AddressOf OnApproveSelected
            AddHandler m_view.DenySelected, AddressOf OnDenySelected
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Init data for the view.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Overloads Sub OnInitView()
        Const ErrorInitializeFormdataText As String = "Error initializing form data."
        Try
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorInitializeFormdataText
        End Try

    End Sub

    ''' <summary>
    ''' Init data for the view.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadViewData()
        Try
            Dim objApprovalModel As New AuditLogModel()
            Dim blnPending As Boolean = True
            Dim listAuditLog As List(Of AuditLogEntry) = objApprovalModel.GetAuditLog(blnPending)
            m_view.AuditLogEntries = listAuditLog
            m_view.DataBindView()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Init data for the view.
    ''' </summary>
    ''' <param name="p_objListLogID">Object list log Id</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnApproveSelected(ByVal p_objListLogID As Object)
        Try
            UpdateSelectedEntriesStatus(p_objListLogID, True)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Init data for the view.
    ''' </summary>
    ''' <param name="p_objListLogID">Object list log Id</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnDenySelected(ByVal p_objListLogID As Object)
        Try
            UpdateSelectedEntriesStatus(p_objListLogID, False)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Update selected entries status.
    ''' </summary>
    ''' <param name="p_objListLogID">object list log Id</param>
    ''' <param name="p_blnIsApproved">Is Approved</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub UpdateSelectedEntriesStatus(ByVal p_objListLogID As Object, ByVal p_blnIsApproved As Boolean)

        Dim strInfoText As String = String.Empty
        m_view.ErrorText = String.Empty
        Dim enuNotificationResult As AuditLogModel.NotificationResult
        Const SelectCertificateFromListText As String = "Please select a certificate from the list before proceed to approve or deny the certificate"
        Const UpdatedText As String = "Updated."
        Const NoUpdatesText As String = "No updates."
        Const NotificationSentText As String = "Notification sent."
        Const NotificationFailedText As String = "Notification failed."
        Const NotificationDisabledText As String = "Notification disabled."
        Const ErrorApproveDenyText As String = "Error on Approve / Deny Selected."

        Try
            Dim lstChangeLogID As List(Of Integer) = CType(p_objListLogID, List(Of Integer))
            Dim dtmSaveTime As DateTime = DateTime.Now

            'fixing bug 24
            If lstChangeLogID.Count = 0 Then
                m_view.ErrorText = SelectCertificateFromListText
            Else
                Dim blnSaved As Boolean = UpdateRefreshAuditLog(lstChangeLogID, p_blnIsApproved)
                strInfoText = CStr(IIf(blnSaved, UpdatedText, NoUpdatesText))
            End If

            enuNotificationResult = AuditLogModel.CheckForChangesAndSendNotification(AuditLogEntry.AreaOfChange.Approval, dtmSaveTime)
            strInfoText &= " "
            If enuNotificationResult = AuditLogModel.NotificationResult.Sent Then
                strInfoText &= NotificationSentText
            ElseIf enuNotificationResult = AuditLogModel.NotificationResult.SendError Then
                strInfoText &= NotificationFailedText
            ElseIf enuNotificationResult = AuditLogModel.NotificationResult.Disabled Then
                strInfoText &= NotificationDisabledText
            End If

            m_view.InfoText = strInfoText
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorApproveDenyText
        End Try

    End Sub

    ''' <summary>
    ''' Update refresh audit log.
    ''' </summary>
    ''' <param name="p_lstChangeLogID">1st change log Id</param>
    ''' <param name="p_blnIsApproved">Is Approved</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function UpdateRefreshAuditLog(ByVal p_lstChangeLogID As List(Of Integer), ByVal p_blnIsApproved As Boolean) As Boolean

        Dim blnDone As Boolean = False
        Try
            If p_lstChangeLogID.Count > 0 Then

                Dim objAuditLogModel As New AuditLogModel()
                blnDone = objAuditLogModel.UpdateAuditLog(m_view.AuditLogEntries, DateTime.Now, p_lstChangeLogID, p_blnIsApproved)
                If blnDone Then
                    objAuditLogModel.RefreshAuditList(m_view.AuditLogEntries, p_lstChangeLogID, p_blnIsApproved)
                End If

            End If

            Return blnDone
        Catch
            Throw
        End Try

    End Function

#End Region

End Class
