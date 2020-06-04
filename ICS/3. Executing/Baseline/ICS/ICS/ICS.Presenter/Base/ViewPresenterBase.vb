Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class ViewPresenterBase

#Region "Members"

    Private m_view As IView

#End Region

#Region "Constructors / Destructors"

    Public Sub New(ByVal p_view As IView)

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

    End Sub

    Protected Sub OnInitView()

        AuthorizeUser()

    End Sub

    ''' <summary>
    ''' Authorize User to proceed with the view and menu items
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AuthorizeUser()

        Dim strViewTypeName As String = DirectCast(m_view, Object).GetType().BaseType.Name

        If Not SecurityModel.IsUserAuthorized(strViewTypeName) Then
            m_view.ShowMainView()
            Return
        End If

        m_view.HideUserMenuItems(SecurityModel.GetUnauthorizedMenuItems())

    End Sub

#End Region

End Class
