Partial Public Class ExcelExport
    Inherits System.Web.UI.Page
    ''' <summary>
    '''  Method for Page load.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
    ''' <para>10/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' <para>Fixed ICS Issues</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Const ReportSelectorViewText As String = "reportselectorview_aspxDataSet"
        Const ApplicationText As String = "application/vnd.ms-excel"
        Const ContentDispositionText As String = "content-disposition"
        Const FilenameText As String = "attachment;filename=CCCProductDescription.xls"

        Try
            Dim tblSource As DataSet = TryCast(Session(ReportSelectorViewText), DataSet)
            gvCCCProductDesc.DataSource = tblSource.Tables(2)
            gvCCCProductDesc.DataBind()
            gvCCCProductDesc.Visible = True

            Dim frm As HtmlForm = New HtmlForm()
            Response.Clear()
            Response.ContentType = ApplicationText
            ' Remove the charset from the Content-Type header.
            Response.Charset = ""
            Response.AddHeader(ContentDispositionText, FilenameText)
            Me.EnableViewState = False
            Dim tw As New System.IO.StringWriter
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Controls.Add(frm)
            frm.Controls.Add(tblCCCProdDesc)
            frm.Controls.Add(gvCCCProductDesc)
            frm.Controls.Add(tblCCCNote)
            frm.RenderControl(hw)
            frm.Visible = True

            Response.Write(tw.ToString())
            ' End the response.
            Response.End()
        Catch
            Throw
        End Try
    End Sub

End Class