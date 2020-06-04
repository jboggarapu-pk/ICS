Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common

''' <summary>
''' Home view pesenter
''' </summary>
''' <remarks></remarks>
Public Class HomePresenter
    Inherits ViewPresenterBase

#Region "Members"

    Private m_view As IHomeView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IHomeView)

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
        AddHandler m_view.LoadView, AddressOf OnLoadView
        AddHandler m_view.EnterMarketing, AddressOf OnEnterMarketing
        AddHandler m_view.EnterMarketingNew, AddressOf OnEnterMarketingNew
        AddHandler m_view.EnterQuality, AddressOf OnEnterQuality

    End Sub

    Protected Overloads Sub OnInitView()

        If (Not m_view.IsPostBackView) Then
        End If

    End Sub

    ''' <summary>
    ''' Load data for the view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)

        Try
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            Throw exc
        End Try

    End Sub
    '
    Private Sub OnEnterMarketing(ByVal sender As Object, ByVal e As EventArgs)

        m_view.ShowMarketingForm()

    End Sub
    '
    Private Sub OnEnterMarketingNew(ByVal sender As Object, ByVal e As EventArgs)

        m_view.ShowMarketingNewForm()

    End Sub

    Private Sub OnEnterQuality(ByVal sender As Object, ByVal e As EventArgs)

        ' Only once per session, check for any Pending Approvals;
        If m_view.PendingApprovalCount < 0 Then

            Dim objApprovalModel As New AuditLogModel()
            m_view.PendingApprovalCount = objApprovalModel.GetAuditLogPendingCount(False)

            If m_view.PendingApprovalCount > 0 Then
                m_view.InfoText = "Please note - there are " & m_view.PendingApprovalCount & " pending approvals."
                Return
            End If

        End If

        m_view.ShowCertificationForm()

    End Sub

    ''' <summary>
    ''' Load data from business process model
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewData()

        Dim enuOperationMode As IHomeView.OperationMode = IHomeView.OperationMode.None
        Dim strUserGroup As String = SecurityModel.GetGroupName

        'Select Case SecurityModel.UserGroup
        Select Case strUserGroup
            Case SecurityModel.GroupName.MarketingUser
                enuOperationMode = IHomeView.OperationMode.Marketing
            Case SecurityModel.GroupName.QualityManager, _
                SecurityModel.GroupName.QualityUser
                enuOperationMode = IHomeView.OperationMode.Both
            Case SecurityModel.GroupName.ITUser
                enuOperationMode = IHomeView.OperationMode.Both
            Case Else
                ' default
                enuOperationMode = IHomeView.OperationMode.None
        End Select

        ' Only once per session, check for any Pending Approvals;
        'If m_view.IsUserNameShown Then
        m_view.InfoText = "Welcome " & SecurityModel.GetUserName() & ", you are in " & SecurityModel.GetGroupName() & " user group"

        ''TODO - for debug only
        'm_view.InfoText &= " (OperationMode = " & enuOperationMode.ToString()
        'If SecurityModel.UserGroup = SecurityModel.GroupName.QualityManager OrElse SecurityModel.UserGroup = SecurityModel.GroupName.QualityUser Then
        '    enuOperationMode = IHomeView.OperationMode.Both
        '    m_view.InfoText &= ", PromotionMode = " & enuOperationMode.ToString()
        'End If
        'm_view.InfoText &= ")"
        'TODO - for debug only

        m_view.IsUserNameShown = False
        'End If

        m_view.SetupStartProperties(enuOperationMode)

    End Sub

#End Region

End Class
