Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Partial Public Class QueryUC
    Inherits BaseUserControl
    Implements IQueryView
#Region "Constructors"
    ''' <summary>
    ''' Constructor for this class.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New QueryPresenter(Me)

    End Sub

#End Region

#Region "Members"
    ''' <summary>
    ''' variable to hold Presenter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_presenter As QueryPresenter

    ''' <summary>
    ''' variable to hold Reload View Data.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ReloadViewData() Implements IQueryView.ReloadViewData

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets or sets Error Text value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Error Text.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements IQueryView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Filter Column Source value.
    ''' </summary>
    ''' <value>List of String</value>
    ''' <returns>Filter Column Source.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property FilterColumnSource() As List(Of String) Implements IQueryView.FilterColumnSource
        Get
            Return CType(ddlFilterColumn1.DataSource, Global.System.Collections.Generic.List(Of String))
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

    ''' <summary>
    ''' Gets or sets Filter Source value.
    ''' </summary>
    ''' <value>list of String</value>
    ''' <returns>Filter Source.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property FilterSource() As List(Of String) Implements IQueryView.FilterSource
        Get
            Return CType(ddlFilter1.DataSource, Global.System.Collections.Generic.List(Of String))
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

    ''' <summary>
    ''' Gets or sets Grid Source value.
    ''' </summary>
    ''' <value>Datatable</value>
    ''' <returns>Grid Source.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property GridSource() As DataTable Implements IQueryView.GridSource
        Get
            Return CType(gvQuery.DataSource, DataTable)
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

    ''' <summary>
    ''' Page Index Changing Event.
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <para>11/05/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub gvQuery_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvQuery.PageIndexChanging
        Try
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
        Catch ex As Exception
            Throw
        End Try
        
    End Sub

    ''' <summary>
    ''' Sorting Event.
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <para>11/05/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub gvQuery_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvQuery.Sorting
        Try
            Dim dt As DataTable = GridSource

            If dt IsNot Nothing Then

                'Sort the data.
                dt.DefaultView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
                gvQuery.DataSource = dt
                gvQuery.DataBind()

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to get Sort Direction.
    ''' </summary> 
    ''' <param name="column">Column</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function GetSortDirection(ByVal column As String) As String
        Try
            ' By default, set the sort direction to ascending.
            Dim sortDirection As String = "ASC"

            ' Retrieve the last column that was sorted.
            Dim sortExpression As String = TryCast(ViewState("SortExpression"), String)

            If sortExpression IsNot Nothing Then
                ' Check if the same column is being sorted.
                ' Otherwise, the default value can be returned.
                If sortExpression = column Then
                    Dim lastDirection As String = TryCast(ViewState("SortDirection"), String)
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
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Filter Button Click .
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <para>11/05/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        Try
            Dim FilterExpression As String = String.Empty
            Dim FilterColumn1 As String = String.Empty
            Dim FilterColumn2 As String = String.Empty
            Dim FilterColumn3 As String = String.Empty
            Dim FilterColumn4 As String = String.Empty
            Dim FilterColumn5 As String = String.Empty
            Dim FilterColumn6 As String = String.Empty
            Dim FilterColumn7 As String = String.Empty
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

End Class