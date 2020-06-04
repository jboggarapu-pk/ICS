Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Partial Public Class QueryUC
    Inherits BaseUserControl
    Implements IQueryView
#Region "Constructors"

    Public Sub New()

        m_presenter = New QueryPresenter(Me)

    End Sub

#End Region

#Region "Members"

    Private m_presenter As QueryPresenter
    Public Event ReloadViewData() Implements IQueryView.ReloadViewData

#End Region

#Region "Properties"

    Public Property ErrorText() As String Implements IQueryView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property FilterColumnSource() As List(Of String) Implements IQueryView.FilterColumnSource
        Get
            Return ddlFilterColumn1.DataSource
        End Get
        Set(ByVal value As List(Of String))
            'Set the data source for the column drop-downs
            ddlFilterColumn1.DataSource = value
            ddlFilterColumn2.DataSource = value
            ddlFilterColumn3.DataSource = value
            ddlFilterColumn4.DataSource = value
            ddlFilterColumn5.DataSource = value
            ddlFilterColumn6.DataSource = value
            ddlFilterColumn7.DataSource = value

            'Bind the data source
            ddlFilterColumn1.DataBind()
            ddlFilterColumn2.DataBind()
            ddlFilterColumn3.DataBind()
            ddlFilterColumn4.DataBind()
            ddlFilterColumn5.DataBind()
            ddlFilterColumn6.DataBind()
            ddlFilterColumn7.DataBind()
        End Set
    End Property

    Public Property FilterSource() As List(Of String) Implements IQueryView.FilterSource
        Get
            Return ddlFilter1.DataSource
        End Get
        Set(ByVal value As List(Of String))
            'Set the data source for the column drop-downs
            ddlFilter1.DataSource = value
            ddlFilter2.DataSource = value
            ddlFilter3.DataSource = value
            ddlFilter4.DataSource = value
            ddlFilter5.DataSource = value
            ddlFilter6.DataSource = value
            ddlFilter7.DataSource = value

            'Bind the data source
            ddlFilter1.DataBind()
            ddlFilter2.DataBind()
            ddlFilter3.DataBind()
            ddlFilter4.DataBind()
            ddlFilter5.DataBind()
            ddlFilter6.DataBind()
            ddlFilter7.DataBind()
        End Set
    End Property

    Public Property GridSource() As DataTable Implements IQueryView.GridSource
        Get
            Return gvQuery.DataSource
        End Get
        Set(ByVal value As DataTable)
            'Set the Data Source
            gvQuery.DataSource = value
            gvQuery.DataBind()

            Dim colList As New List(Of String)
            colList.Add("Choose a column...")
            For Each Column As BoundField In gvQuery.Columns
                colList.Add(Column.HeaderText)
            Next
            FilterColumnSource = colList
        End Set
    End Property

#End Region

#Region "Methods"

    Private Sub gvQuery_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvQuery.PageIndexChanging

        ' Cancel the paging operation if the user attempts to navigate
        ' to another page while the GridView control is in edit mode. 
        If gvQuery.EditIndex <> -1 Then

            ' Use the Cancel property to cancel the paging operation.
            e.Cancel = True

            ' Display an error message.
            Dim newPageNumber As Integer = e.NewPageIndex + 1
            ErrorText = "Please update the record before moving to page " & _
              newPageNumber.ToString() & "."

        Else
            ' Clear the error message.
            ErrorText = ""

            'Navigate to the new page
            gvQuery.PageIndex = e.NewPageIndex
            gvQuery.DataBind()

        End If
    End Sub

    Private Sub gvQuery_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvQuery.Sorting
        'Retrieve the table from the session object.
        'Dim dt = TryCast(Session("GridSource"), DataTable)
        Dim dt = GridSource

        If dt IsNot Nothing Then

            'Sort the data.
            dt.DefaultView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
            gvQuery.DataSource = dt
            gvQuery.DataBind()

        End If
    End Sub

    Private Function GetSortDirection(ByVal column As String) As String

        ' By default, set the sort direction to ascending.
        Dim sortDirection = "ASC"

        ' Retrieve the last column that was sorted.
        Dim sortExpression = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then
            ' Check if the same column is being sorted.
            ' Otherwise, the default value can be returned.
            If sortExpression = column Then
                Dim lastDirection = TryCast(ViewState("SortDirection"), String)
                If lastDirection IsNot Nothing _
                  AndAlso lastDirection = "ASC" Then

                    sortDirection = "DESC"

                End If
            End If
        End If

        ' Save new values in ViewState.
        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column

        Return sortDirection

    End Function

    Private Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click

        Dim FilterExpression As String = String.Empty
        Dim FilterColumn1 As String = String.Empty
        Dim FilterColumn2 As String = String.Empty
        Dim FilterColumn3 As String = String.Empty
        Dim FilterColumn4 As String = String.Empty
        Dim FilterColumn5 As String = String.Empty
        Dim FilterColumn6 As String = String.Empty
        Dim FilterColumn7 As String = String.Empty


    End Sub

#End Region

End Class