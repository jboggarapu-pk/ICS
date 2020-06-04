<%@ Control Language="vb" AutoEventWireup="false" Codebehind="FamilyMaintenanceUC.ascx.vb"
    Inherits="CooperTire.ICS.Web.FamilyMaintenanceUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="pnlIFamilyMaintenance" runat="server" Visible="True" CssClass="UCPanel">
    <asp:Label ID="lblFormTitle" runat="server"></asp:Label><br />
    <asp:Label ID="lblSuccessText" runat="server" SkinID="SuccessText" Text="&nbsp;"
        Font-Size="Smaller"></asp:Label>
    <asp:Label ID="lblImark" runat="server" Text="Certificate"></asp:Label>
    <asp:DropDownList ID="ddImark" runat="server" AutoPostBack="True">
    </asp:DropDownList><br />
    <asp:Label ID="lblErrorText" runat="server" SkinID="ErrorText"></asp:Label>
    <table style="border-style: solid; border-width: medium; border-color: #E3EAEB; height: auto;
        width: 292px;">
        <tr>
            <td>
                <div id="divFamilies" runat="server" style="width: 790px; height: 400px; overflow: scroll; 
                    float: left;padding: 10px; ">
                    <asp:GridView ID="gvFamilyMaintenance" runat="server" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#839FAF" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" ShowFooter="true" HeaderStyle-Font-Bold="false" HeaderStyle-ForeColor="Black"
                        Font-Size="X-Small" DataKeyNames="Family_Id" OnRowCommand="Grid_RowCommand" SkinID="Professional">
                        <AlternatingRowStyle BackColor="#CFDFE9" />
                        <HeaderStyle BackColor="#CFDFE9" Font-Bold="true" Font-Size="x-Small" />               
                        <Columns>
                            <asp:TemplateField HeaderText="EDIT" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit" CommandName="EditFamily" CommandArgument='<%#Bind("FAMILY_ID") %>'
                                        Text="Edit" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DELETE" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbDelete" CommandName="DeleteFamily" CommandArgument='<%#Bind("FAMILY_ID") %>'
                                        Text="Delete" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Family ID" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblFamilyId" runat="server" Text='<%#Bind("FAMILY_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Family Code" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblFamilyCode" runat="server" Text='<%#Bind("FAMILY_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mold Stamping" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblFamilyDesc" runat="server" Text='<%#Bind("FAMILY_DESC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Application Cat" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblApplicationCat" runat="server" Text='<%#Bind("APPLICATION_CAT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Structure Type" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblConstructionType" runat="server" Text='<%#Bind("CONSTRUCTION_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Use Category" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblStructureType" runat="server" Text='<%#Bind("STRUCTURE_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mounting Type" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblMountingType" runat="server" Text='<%#Bind("MOUNTING_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Aspect Ratio Cat" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAspectRatioCat" runat="server" Text='<%#Bind("ASPECT_RATIO_CAT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Speed Rating Cat" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpeedRatingCat" runat="server" Text='<%#Bind("SPEED_RATING_CAT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Load Index Cat" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblLoadIndexCat" runat="server" Text='<%#Bind("LOAD_INDEX_CAT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <ajaxToolkit:ModalPopupExtender ID="DeleteConfirmPopUp" runat="server" PopupControlID="pnlDeleteConfirm"
                        TargetControlID="hidConfirm" BackgroundCssClass="modalBackground" />
                    <asp:HiddenField ID="hidConfirm" runat="server" />
                    <asp:Panel ID="pnlDeleteConfirm" runat="server" CssClass="modalPopup" Style="display: none"
                        Width="280px">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        This would delete Family. Would you like to continue?</label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnDeleteConfirm" runat="server" Text="Confirm" OnClick="Click_DeleteConfirm" />&nbsp;
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnDeleteCancel" runat="server" Text="Cancel" OnClick="Click_DeleteCancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="float: left; padding-top: 20px;">
                    <asp:Button ID="btnAdd" Text="Add" runat="server" OnClick="Click_AddFamily" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
            
            </td>
        </tr>
    </table>
    <div id="divFamilyDetails" visible="false" runat="server" style="float: left; padding-top: 20px;
        width: 820px;">
        <table style="border-style: solid; border-width: medium; border-color: #E3EAEB; height: auto;
            width: 820px;"  class="contentArea" >
            <tr>
                <td align="right"style="font-weight: bold; width: 180px;">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small; font-weight: bold;">
                    Family ID:</span></td>
                <td style="width: 220px;">
                    <asp:TextBox ID="txtftrFamilyId" runat="server" MaxLength="3" />                    
                    <asp:RequiredFieldValidator ID="rfvFamilyId"  ControlToValidate="txtftrFamilyId"
                        ErrorMessage="Please enter family Id." ValidationGroup="vgFamily" Display="None" runat="server"/>
                    <asp:RegularExpressionValidator ID="revFamilyId" ControlToValidate="txtftrFamilyId" ValidationGroup="vgFamily" 
                        ErrorMessage="Only numbers allowed for family Id." ValidationExpression="\d+" Display="None"  runat="server"></asp:RegularExpressionValidator>
                        </td>
                <td style="width: 20px;">
                </td>
                <td align="right" style="font-weight: bold; width: 180px;">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small; font-weight: bold;">Family Code:</span> </td>
                <td style="width: 220px;">
                    <asp:TextBox ID="txtftrFamilyCode"  runat="server" MaxLength="10" />                    
                    <asp:RequiredFieldValidator ID="rfvFamilyCode" runat="server" ControlToValidate="txtftrFamilyCode"
                        ErrorMessage="Please enter family code." ValidationGroup="vgFamily" Display="None" />
                </td>
            </tr>
            <tr>
                <td align="right" style="font-weight: bold;">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small; font-weight: bold;">Mold Stamping:</span> </td>
                <td>                    
                    <asp:TextBox ID="txtftrFamilyDesc" runat="server" MaxLength="50" />                    
                </td>
                <td>
                </td>
                <td align="right" style="font-weight: bold;">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small; font-weight: bold;">Application Category:</span> </td>
                <td>
                    <asp:TextBox ID="txtftrApplicationCat" runat="server" MaxLength="2" />
                </td>
            </tr>
            <tr>
                <td align="right" style="font-weight: bold;">
                   <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small; font-weight: bold;"> Structure Type:</span> </td>
                <td>
                    <asp:TextBox ID="txtftrConstructionType" runat="server" MaxLength="2" />
                </td>
                <td>
                </td>
                <td align="right" style="font-weight: bold;">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small; font-weight: bold;">Use Category:</span> </td>
                <td>
                    <asp:TextBox ID="txtftrStructureType" runat="server" MaxLength="2" />
                </td>
            </tr>
            <tr>
                <td align="right" style="font-weight: bold;">
                   <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small; font-weight: bold;"> Mounting Type: </span> </td>
                <td>
                    <asp:TextBox ID="txtftrMountingType" runat="server" MaxLength="2" />
                </td>
                <td>
                </td>
                <td align="right" style="font-weight: bold;">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small; font-weight: bold;">Aspect Ratio Category:</span></td>
                <td>
                    <asp:TextBox ID="txtftrAspectRatioCat" runat="server" MaxLength="2" />
                </td>
            </tr>
            <tr>
                <td align="right" style="font-weight: bold;">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small; font-weight: bold;">Speed Rating Category:</span></td>
                <td>
                    <asp:TextBox ID="txtftrSpeedRatingCat" runat="server" MaxLength="2" />
                </td>
                <td>
                </td>
                <td align="right" style="font-weight: bold;">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small; font-weight: bold;">Load Index Category:</span></td>
                <td>
                    <asp:TextBox ID="txtftrLoadIndexCat" runat="server" MaxLength="2" />
                </td>
            </tr>            
            <tr>                
                <td colspan="6">
                <center>
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small; font-weight: bold;"><asp:ValidationSummary ID="vsFamilyValidations" ShowSummary="true"
                        runat="server"  Visible="true" ValidationGroup ="vgFamily" /></span> </center>
                </td>                
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgFamily" OnClick="Click_SaveFamily" /></td>
                <td align="center" colspan="2">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="Click_CancelFamily" />
                </td>
            </tr>
        </table>
    </div>
    
</asp:Panel>
