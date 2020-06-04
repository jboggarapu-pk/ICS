<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AddCertificationUC.ascx.vb"
    Inherits="CooperTire.ICS.Web.AddCertificationUC" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="validation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="pnlAddCertification" runat="server" Visible="True" CssClass="UCActionPanel">
    <asp:Label ID="lblFormTitle" runat="server"></asp:Label><br />
    <asp:Label ID="lblSuccessText" runat="server" SkinID="SuccessText" Text="&nbsp;"></asp:Label>
    <asp:Label ID="lblErrorText" runat="server" SkinID="ErrorText"></asp:Label>
    <table border="0" width="400px">
        <tr>
            <td colspan="3">
                <table border="0" width="100%">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblCertNumber" runat="server" Text="Certificate No. :" Width="100px" /></td>
                        <td align="left">
                            <asp:TextBox ID="txtCertNumber" runat="server" AutoPostBack="false" MaxLength="20"
                                Width="150px" /></td>
                        <td align="right">
                            <asp:Label ID="lblExtension" runat="server" Text="Extension :" Width="75px" Visible="false" /></td>
                        <td align="left">
                            <asp:TextBox ID="txtExtension" runat="server" AutoPostBack="false" MaxLength="30"
                                Width="100px" Visible="false" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTBExtension" runat="server" TargetControlID="txtExtension"
                                FilterType="Numbers" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <%--<validation:PropertyProxyValidator ID="ppvCertificationNumber" runat="server" ControlToValidate="txtCertNumber"
                                PropertyName="CertificateNumber" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                            </validation:PropertyProxyValidator>--%>
                            <asp:Label ID="lblErrCertNumber" runat="server" Text="The Certificate Number is not valid."
                                Visible="false" SkinID="ErrorText" /></td>
                        <td>
                            <asp:Label ID="lblErrExtension" runat="server" Text="The Extension is not valid."
                                Visible="false" SkinID="ErrorText" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <asp:Panel ID="pnlAddCert_I" runat="server" Visible="false">
            <tr>
                <td colspan="3">
                    <table border="0" width="100%">
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblSpecNumber_I" runat="server" Text="Spec No. :"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtSpecNumber_I" runat="server"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlAddCert_N" runat="server" Visible="false">
            <tr>
                <td colspan="3">
                    <table border="0" width="100%">
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblImporterNumber_N" runat="server" Text="Importer :"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlImporterNumber_N" runat="server" Width="375px" Enabled="True">
                                </asp:DropDownList></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblCustomerNumber_N" runat="server" Text="Customer :"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCustomerNumber_N" runat="server" Width="375px" Enabled="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </asp:Panel>
        <asp:HiddenField ID="hidCert" runat="server" />
        <asp:Panel ID="pnlMatlNumArea" runat="server">
            <tr>
                <td colspan="3">
                    <table id="tblMatlNum" runat="server" border="0" width="70%">
                    </table>
                    <asp:HiddenField ID="hidMatlNum" runat="server" />
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td colspan="1" style="height: 27px" align="center">
                <asp:Button ID="btnAddMaterial" runat="server" Text="Add Material" />
                <asp:Button ID="btnSave" runat="server" Text="Save" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
            </td>
        </tr>
    </table>
    <ajaxToolkit:ModalPopupExtender ID="ConfirmPopUp" runat="server" PopupControlID="pnlConfirm"
        TargetControlID="hidMatlNum" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlConfirm" runat="server" CssClass="modalPopup" Style="display: none"
        Width="233px">
        <table>
            <tr>
                <td colspan="2">
                    <label>
                        Please enter a valid Material before you add another one.</label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="Click_OK" />&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="CancelAlertPopUp" runat="server" PopupControlID="pnlCancelAlert"
        TargetControlID="hidCert" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlCancelAlert" runat="server" CssClass="modalPopup" Style="display: none"
        Width="280px">
        <table>
            <tr>
                <td colspan="2">
                    <label>
                        All your changes will be lost, would you like to continue?</label>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="Button_OK" runat="server" Text="OK" OnClick="Click_Confirm" />&nbsp;
                </td>
                <td align="center">
                    <asp:Button ID="Button_Cancel" runat="server" Text="Cancel" OnClick="Click_Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ConfirmPopUpErr2" runat="server" PopupControlID="pnlErr2"
        TargetControlID="hidCert" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlErr2" runat="server" CssClass="modalPopup" Style="display: none"
        Width="280px">
        <table>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblErr2" runat="server"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnOkErr2" runat="server" Text="OK" />&nbsp;
                </td>
                <td align="center">
                    <asp:Button ID="btnCancleErr2" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
