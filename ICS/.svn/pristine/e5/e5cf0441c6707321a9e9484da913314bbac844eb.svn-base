Partial Public Class ExcelExport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        gvCCCProductDesc.DataSource = Session("reportselectorview_aspxDataSet").tables(2)

        gvCCCProductDesc.DataBind()
        gvCCCProductDesc.Visible = True

        Dim frm As HtmlForm = New HtmlForm()
        Response.Clear()
        Response.ContentType = "application/vnd.ms-excel"
        ' Remove the charset from the Content-Type header.
        Response.Charset = ""
        Response.AddHeader("content-disposition", "attachment;filename=CCCProductDescription.xls")
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
    End Sub

End Class