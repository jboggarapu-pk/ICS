<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ArchiveCertificationUC.ascx.vb" Inherits="CooperTire.ICS.Web.ArchiveCertificationUC" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="validation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="pnlArchiveCertification" runat="server" Visible="True" CssClass="UCPanel">
    <asp:Label ID="lblFormTitle" runat="server"></asp:Label><br />
    <asp:Label ID="lblSuccessText" runat="server" SkinID="SuccessText" Text="&nbsp;"></asp:Label>
    <asp:Label ID="lblErrorText" runat="server" SkinID="ErrorText"></asp:Label>
    <table border="0" width="100%">
        <tr>
            <td colspan="3">
                <table border="0" width="100%">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblCertNumber" runat="server" Text="Certificate No. :" Width="325px"></asp:Label></td>
                        <td style="width: 200px">
                            <asp:TextBox ID="txtCertNumber" runat="server" AutoPostBack="false" MaxLength="20" Width="150px"></asp:TextBox></td>
                        <td style="width: 320px" align="left">
                            <%--<validation:PropertyProxyValidator ID="ppvCertificationNumber" runat="server" ControlToValidate="txtCertNumber"
                                PropertyName="CertificateNumber" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                            </validation:PropertyProxyValidator>--%>
                            <asp:Label ID="lblErrCertNumber" runat="server" Text="The Certificate Number is not valid."
                                Visible="false" Width="320px" SkinID="ErrorText"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="1" style="height: 27px" align="center">
                <asp:Button ID="btnArchiveCertification" runat="server" Text="Archive" />
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
                        Are you sure you want to archive this certification?</label>
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
