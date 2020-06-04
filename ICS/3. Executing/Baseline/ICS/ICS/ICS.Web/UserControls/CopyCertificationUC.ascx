<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CopyProductUC.ascx.vb"
    Inherits="CooperTire.ICS.Web.CopyCertificationUC" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="validation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="pnlArchiveCertification" runat="server" Visible="True" CssClass="UCActionPanel">
    <asp:Label ID="lblFormTitle" runat="server"></asp:Label><br />
    <asp:Label ID="lblSuccessText" runat="server" SkinID="SuccessText" Text="&nbsp;"></asp:Label>
    <asp:Label ID="lblErrorText" runat="server" SkinID="ErrorText"></asp:Label>
    <table border="0" width="70%">
        <tr>
            <td colspan="3">
                <table border="0" width="70%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblMateNumber" runat="server" Text="Material No. :" Width="75px"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtMateNumber" runat="server" AutoPostBack="false" MaxLength="20"
                                Width="150px"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="lblErrMateNumber" runat="server" Text="The Material Number is not valid."
                                Visible="False" Width="320px" SkinID="ErrorText"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="1" style="height: 27px" align="center">
                <asp:Button ID="btnCopyMaterial" runat="server" Text="Copy" />
            </td>
        </tr>
    </table>
    <ajaxToolkit:ModalPopupExtender ID="ConfirmPopUp" runat="server" PopupControlID="pnlConfirm"
        TargetControlID="lblFormTitle" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlConfirm" runat="server" CssClass="modalPopup" Style="display: none"
        Width="300px">
        <table>
            <tr>
                <td colspan="2">
                    <label>
                        Are you sure you want to Copy this Material?</label>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="Button1" runat="server" Text="OK" OnClick="Click_Confirm" />&nbsp;
                </td>
                <td align="center">
                    <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Click_Cancel" />&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
