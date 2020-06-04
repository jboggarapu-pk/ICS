Imports System.IO


Partial Public Class Report
    Inherits System.Web.UI.Page


#Region "Methods"
    ''' <summary>
    '''  Method for page load.
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' <para>Fixed ICS Issues</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Const DispTypeText As String = "DispType"
        Const ApplicationPdfText As String = "application/pdf"
        Const ContentDispText As String = "Content-Disposition"
        Const FilenameText As String = "filename="
        Const ReportText As String = "Report.pdf"
        Const ReportSelectorViewText As String = "reportselectorview_aspxDataSet"
        Const ApplicationVndText As String = "application/vnd.ms-excel"
        Const ContentdispositionText As String = "content-disposition"
        Const AttachmentText As String = "attachment;filename=CCCProductDescription.xls"
        Const RText As String = "R"
        Const EText As String = "E"

        Try
            Dim strDispType As String
            gvExcel.Visible = False

            'Determine if this is a crystal report or an Excel document
            strDispType = Request(DispTypeText)
            If strDispType = RText Then
                Try
                    Dim IOStream As Stream = Global_asax.IOStream
                    Response.Clear()

                    'set type of file to be streamed and set filename when streamed out
                    Response.ContentType = ApplicationPdfText
                    Response.AddHeader(ContentDispText, FilenameText & ReportText)

                    'stream file to the client
                    Dim ByteStream(CType(IOStream.Length, Integer)) As Byte
                    IOStream.Read(ByteStream, 0, CInt(IOStream.Length))
                    Response.BinaryWrite(ByteStream)

                    'Set cache setting to a small number of seconds so that the page_load event is not fired twice on initial hit
                    Response.Cache.SetMaxAge(New TimeSpan(0, 0, 5))

                    Response.End()
                Catch ex As Exception
                    Response.Write(ex.StackTrace)
                End Try
            ElseIf strDispType = EText Then ' Excel

                Dim tblSource As DataSet = TryCast(Session(ReportSelectorViewText), DataSet)
                gvExcel.DataSource = tblSource.Tables(2)
                gvExcel.DataBind()

                gvExcel.Visible = True
                Dim frm As HtmlForm = New HtmlForm()
                Response.Clear()
                Response.ContentType = ApplicationVndText
                ' Remove the charset from the Content-Type header.
                Response.Charset = ""
                Response.AddHeader(ContentdispositionText, AttachmentText)
                Me.EnableViewState = False
                Dim tw As New System.IO.StringWriter
                Dim hw As New System.Web.UI.HtmlTextWriter(tw)
                Controls.Add(frm)
                frm.Controls.Add(gvExcel)
                frm.RenderControl(hw)
                frm.Visible = True

                Response.Write(tw.ToString())
                ' End the response.
                Response.End()
            End If
        Catch
            Throw
        End Try
        
    End Sub

#End Region

End Class