Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Query Presenter
''' </summary>
''' <remarks></remarks>
Public Class QueryPresenter

#Region "Members"

    Private m_view As IQueryView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IQueryView)

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

        AddHandler m_view.ReloadViewData, AddressOf OnReloadViewData
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
            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error loading form data."
        End Try

    End Sub

    ''' <summary>
    ''' Reoad data for the control - start anew
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnReloadViewData()

        ' Flush all properties:

    End Sub

    ''' <summary>
    ''' Load data from business process
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewData()
        Dim certModel As New CertificateModel
        Dim FilterList As New List(Of String)

        m_view.GridSource = certModel.GetQueryControlGridSource()

        FilterList.Add("")
        FilterList.Add("=")
        FilterList.Add("LIKE")
        FilterList.Add(">")
        FilterList.Add("<")
        FilterList.Add(">=")
        FilterList.Add("<=")
        FilterList.Add("<>")
        m_view.FilterSource = FilterList

    End Sub

#End Region

End Class
