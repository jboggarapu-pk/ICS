<%@ Control Language="vb" AutoEventWireup="false" Codebehind="DeleteCertificationUC.ascx.vb"
    Inherits="CooperTire.ICS.Web.DeleteCertificationUC" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="validation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="pnlDeleteCertification" runat="server" Visible="True" CssClass="UCActionPanel">
    <asp:Label ID="lblFormTitle" runat="server"></asp:Label><br />
    <asp:Label ID="lblSuccessText" runat="server" SkinID="SuccessText" Text="&nbsp;"></asp:Label>
    <asp:Label ID="lblErrorText" runat="server" SkinID="ErrorText"></asp:Label>
    <table border="0" width="70%">
        <tr>
            <td colspan="3">
                <table border="0" width="100%">                  
                    <tr>
                        <td>
                            <asp:Label ID="lblCertNumber" runat="server" Text="Certificate" /></td>
                        <td>
                            <asp:Label ID="lblExtension" runat="server" Text="Extension" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCertNumber" runat="server" AutoPostBack="false" MaxLength="20" Width="150px" /></td>
                        <td>
                            <asp:TextBox ID="txtExtension" runat="server" AutoPostBack="false" MaxLength="30" Width="100px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTBExtension" runat="server" 
                                                                 TargetControlID="txtExtension" FilterType="Numbers" />                            
                        
                        </td>                       
                    </tr>
                    <tr>
                        <td>                       
                            <asp:Label ID="lblErrCertNumber" runat="server" Text="The Certificate Number is not valid."
                                       Visible="false" SkinID="ErrorText" /></td>
                        <td>
                            <asp:Label ID="lblErrExtension" runat="server" Text="The Extension Number is not valid."
                                       Visible="false" SkinID="ErrorText" /></td>                       
                    </tr>
                </table>
            </td>
        </tr>  
        <tr>
            <td colspan="1" style="height: 27px" align="center">
                &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="Click_btnSave" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="Click_btnCancel" />
            </td>
        </tr>
    </table>
    <ajaxToolkit:ModalPopupExtender ID="ConfirmPopUp" runat="server" PopupControlID="pnlConfirm"
        TargetControlID="hidConfirm" BackgroundCssClass="modalBackground" />
    <asp:HiddenField ID="hidConfirm" runat="server" />
    <asp:Panel ID="pnlConfirm" runat="server" CssClass="modalPopup" Style="display: none"
        Width="280px">
        <table>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblWarningMessage" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                 <td align="center">
                    <asp:Button ID="btnSaveConfirm" runat="server" Text="Confirm" OnClick="Click_SaveConfirm" />&nbsp;
                </td>
                <td align="center">
                    <asp:Button ID="btnSaveCancel" runat="server" Text="Cancel" OnClick="Click_SaveCancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="CancelAlertPopUp" runat="server" PopupControlID="pnlCancelAlert"
        TargetControlID="hidCancel" BackgroundCssClass="modalBackground" />
    <asp:HiddenField ID="hidCancel" runat="server" />
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
</asp:Panel>
