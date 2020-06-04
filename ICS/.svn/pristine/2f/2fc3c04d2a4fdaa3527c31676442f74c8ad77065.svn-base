Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities
Imports System.Configuration

''' <summary>
''' Add certification presenter
''' </summary>
''' <remarks></remarks>
Public Class SessionTimeOutMonitorPresenter

#Region "Members"

    Private m_view As ISessionTimeOutMonitorView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As ISessionTimeOutMonitorView)

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

        AddHandler m_view.LoadView, AddressOf OnLoadView

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
            Else
                RestoreView()
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
        End Try

    End Sub

    ''' <summary>
    ''' Load data from business process
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewData()

        Dim milliseconds As Integer = 60000
        m_view.TimerInterval = m_view.SessionTimeout * milliseconds

    End Sub

    Private Sub RestoreView()

    End Sub

#End Region

End Class
