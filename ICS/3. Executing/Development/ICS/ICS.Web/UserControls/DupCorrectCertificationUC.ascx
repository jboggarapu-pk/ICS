<%@ Control Language="vb" AutoEventWireup="false" Codebehind="DupCorrectCertificationUC.ascx.vb"
    Inherits="CooperTire.ICS.Web.DupCorrectCertificationUC" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="validation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="pnlDupCorrectCertification" runat="server" Visible="True" CssClass="UCActionPanel">
    <asp:Label ID="lblFormTitle" runat="server"></asp:Label><br />
    <asp:Label ID="lblSuccessText" runat="server" SkinID="SuccessText" Text="&nbsp;"></asp:Label><br />
    <asp:Label ID="lblErrorText" runat="server" SkinID="ErrorText"></asp:Label>
    <table border="0" width="70%">
        <tr>
            <td colspan="3">
                <table border="0" width="70%">
                    <tr>
                        <td>
                            <asp:Label ID="lblMatlNumber" runat="server" Text="Material" /></td>
                        <td>
                            <asp:Label ID="lblSpeedRating" runat="server" Text="Speed Rating" /></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtMatlNumber" runat="server" AutoPostBack="false" MaxLength="18"
                                Width="150px" /></td>
                        <td>
                            <asp:TextBox ID="txtSpeedRating" runat="server" AutoPostBack="false" MaxLength="10"
                                Width="100px" /></td>
                        <td>
                            <asp:Button ID="btnView" runat="server" Text="View" OnClick="Click_btnView" Width="60" /></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 20px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:GridView ID="gvDuplicateCertificates" runat="server" SkinID="Professional" AutoGenerateColumns="False"
                    Font-Size="X-Small" PagerStyle-HorizontalAlign="Left" AllowPaging="True" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MATL_NUM" HeaderText="MATL_NUM">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SPEEDRATING" HeaderText="SPEEDRATING">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CERTIFICATIONTYPENAME" HeaderText="TYPE">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CERTIFICATENUMBER" HeaderText="CERTIFICATENUMBER">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Width="60" Text="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <ajaxToolkit:ModalPopupExtender ID="CancelAlertPopUp" runat="server" PopupControlID="pnlCancelAlert"
        TargetControlID="hidCancel" BackgroundCssClass="modalBackground" />
    <asp:HiddenField ID="hidCancel" runat="server" />
    <asp:Panel ID="pnlCancelAlert" runat="server" CssClass="modalPopup" Style="display: none"
        Width="300px">
        <table>
            <tr>
                <td colspan="2">
                    <label>
                        Are you sure you permanenty want to<br />
                        delete this duplicate material record and<br />
                        remove it from all certificates ?</label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="Button_OK" runat="server" Text="OK" OnClick="Click_Confirm" />
                </td>
                <td align="center">
                    <asp:Button ID="Button_Cancel" runat="server" Text="Cancel" OnClick="Click_Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
