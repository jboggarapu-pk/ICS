<%@ Control Language="vb" AutoEventWireup="false" Codebehind="DetachOrMoveCertificationUC.ascx.vb"
    Inherits="CooperTire.ICS.Web.DetachOrMoveCertificationUC" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="validation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="pnlDetachOrMoveCertification" runat="server" Visible="True" CssClass="UCActionPanel">
    <asp:Label ID="lblFormTitle" runat="server"></asp:Label><br />
    <asp:Label ID="lblSuccessText" runat="server" SkinID="SuccessText" Text="&nbsp;"></asp:Label><br />
    <asp:Label ID="lblErrorText" runat="server" SkinID="ErrorText"></asp:Label>     
    <table border="0" width="70%">
        <tr>
            <td colspan="3">
                <table border="0" width="70%">                  
                    <tr>
                        <td>
                            <asp:Label ID="lblCertNumber" runat="server" Text="Certificate" /></td>
                        <td>
                            <asp:Label ID="lblExtension" runat="server" Text="Extension" Visible="false" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCertNumber" runat="server" AutoPostBack="false" MaxLength="20" Width="150px" /></td>
                        <td>
                            <asp:TextBox ID="txtExtension" runat="server" AutoPostBack="false" MaxLength="30" Width="100px" Visible="false" /></td>   
                            <ajaxToolkit:FilteredTextBoxExtender ID="FTBExtension" runat="server" 
                                                                 TargetControlID="txtExtension" FilterType="Numbers" />                            
                        
                        <td>
                            <asp:Button ID="btnList" runat="server" Text="List" OnClick="Click_btnList" Width="60px" /></td>                    
                    </tr>
                    <tr>
                        <td>                       
                            <asp:Label ID="lblErrCertNumber" runat="server" Text="The Certificate Number is not valid."
                                       Visible="false" SkinID="ErrorText" /></td>
                        <td>
                            <asp:Label ID="lblErrExtension" runat="server" Text="The Extesnion is not valid."
                                       Visible="false" SkinID="ErrorText" /></td>    
                        <td></td>                   
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 20px"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:GridView ID="gvCertMaterials" SkinID="Professional" runat="server" 
                              AutoGenerateColumns="False" Font-Size="X-Small" BorderStyle="Solid"
                              AllowPaging="true" PageSize="10" PagerStyle-HorizontalAlign="Left" 
                              OnPageIndexChanging="gvCertMaterials_PageIndexChanging">
                    <Columns>                               
                        <asp:BoundField DataField="CertificationTypeName" HeaderText="Type">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CertificateNumber" HeaderText="Certificate">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Extension_En" HeaderText="Ext" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Matl_Num" HeaderText="Matl_Num">
                            <HeaderStyle HorizontalAlign="Left"/>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SpeedRating" HeaderText="SpeedRating">
                            <HeaderStyle HorizontalAlign="Left"/>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CertificateId" Visible="False">                                      
                        </asp:BoundField>
                        <asp:BoundField DataField="SkuId" Visible="False">                                                                            
                        </asp:BoundField>                                    
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnDetach" runat="server" OnClick="Click_btnDetach" Width="60px" Text="Detach" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnMove" runat="server" OnClick="Click_btnMove" Width="60px" Text="Move"  />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
            </td>
        </tr>         
    </table>  
    <asp:HiddenField ID="hidSkuId" runat="server" Value="0" />
    <asp:HiddenField ID="hidCertificateId" runat="server" Value="0" />  
    <ajaxToolkit:ModalPopupExtender ID="DetachAlertPopUp" runat="server" PopupControlID="pnlDetachAlert"
        TargetControlID="hidDetachAlert" BackgroundCssClass="modalBackground" />
    <asp:HiddenField ID="hidDetachAlert" runat="server" />
    <asp:Panel ID="pnlDetachAlert" runat="server" CssClass="modalPopup" Style="display: none"
        Width="280px">
        <table>
            <tr>
                <td colspan="2">
                    <label>
                        This would delete the productcertificate record for this certificate/material. Would you like to continue?</label>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnDetachOk" runat="server" Text="OK" OnClick="Click_DetachConfirm" />&nbsp;
                </td>
                <td align="center">
                    <asp:Button ID="btnDetachCancel" runat="server" Text="Cancel" OnClick="Click_DetachCancel" />
                </td>
            </tr>           
        </table>
    </asp:Panel>   
    <ajaxToolkit:ModalPopupExtender ID="MoveAlertPopUp" runat="server" PopupControlID="pnlMoveAlert"
        TargetControlID="hidMove" BackgroundCssClass="modalBackground" />
    <asp:HiddenField ID="hidMove" runat="server" />
    <asp:Panel ID="pnlMoveAlert" runat="server" CssClass="modalPopup" Style="display: none"
        Width="350px"> 
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblNewCertificate" runat="server" Text="New Certificate" /></td>
                <td>
                    <asp:Label ID="lblNewExtension" runat="server" Text="New Extension" Visible="false" /></td>                
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtNewCertNumber" runat="server" AutoPostBack="false" MaxLength="20" Width="150px" /></td>
                <td>
                    <asp:TextBox ID="txtNewExtension" runat="server" AutoPostBack="false" MaxLength="30" Width="100px" Visible="false" /></td>   
                    <ajaxToolkit:FilteredTextBoxExtender ID="FTBNewExtension" runat="server" 
                                                         TargetControlID="txtNewExtension" FilterType="Numbers" />                                   
            </tr>
            <tr>
                <td>                       
                    <asp:Label ID="lblErrNewCertNumber" runat="server" Text="The New Certificate Number is not valid."
                               Visible="false" SkinID="ErrorText" /></td>
                <td>
                <asp:Label ID="lblErrNewExtension" runat="server" Text="The New Extesnion is not valid."
                               Visible="false" SkinID="ErrorText" /></td>    
                <td></td>                   
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnMoveConfirm" runat="server" Text="Move" OnClick="Click_MoveConfirm" />&nbsp;
                    <asp:Button ID="btnMoveCancel" runat="server" Text="Cancel" OnClick="Click_MoveCancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>    
</asp:Panel>
