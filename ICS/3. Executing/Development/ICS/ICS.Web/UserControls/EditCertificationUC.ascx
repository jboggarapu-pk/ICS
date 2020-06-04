<%@ Control Language="vb" AutoEventWireup="false" Codebehind="EditCertificationUC.ascx.vb"
    Inherits="CooperTire.ICS.Web.EditCertificationUC" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="validation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="pnlArchiveCertification" runat="server" Visible="True" CssClass="UCActionPanel">
    <asp:Label ID="lblFormTitle" runat="server" Text="Material Maint"></asp:Label><br />
    <asp:Label ID="lblSuccessText" runat="server" SkinID="SuccessText" Text="&nbsp;"></asp:Label>
    <asp:Label ID="lblErrorText" runat="server" SkinID="ErrorText"></asp:Label>
    <table style="margin-left: 30px;">
        <tr>
            <td>
                 <asp:Label ID="lblMateNumber" runat="server" Text="Material No. :" Width="75px"></asp:Label></td>
            </td>
            <td>
                <asp:TextBox ID="txtMatNo" runat="server"></asp:TextBox></td>
            <td>
                <asp:Button ID="btnShowMatNo" runat="server" Text="Show Details" OnClick="btnShowMatNo_Click" /></td>
        </tr>
    </table>
    <table id="tblMain" style="border-style: solid; border-width: medium; border-color: #E3EAEB;
        height: auto; width: 618px;" runat="server" visible="false">
        <tr>
            <td>
                <div id="divMaterialMaint" runat="server" style="width: 618px; height: 200px; float: left;
                    padding: 10px;" visible="false">
                    <asp:GridView ID="gvMaterialMaint" runat="server" AutoGenerateColumns="false" BackColor="White"
                        BorderColor="#cccccc" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowFooter="false"
                        HeaderStyle-Font-Bold="false" HeaderStyle-ForeColor="Black" Font-Size="X-Small"
                        DataKeyNames="skuid" SkinID="Professional" OnRowCommand="Grid_RowCommand">
                        <AlternatingRowStyle BackColor="#CFDFE9" />
                        <HeaderStyle BackColor="#CFDFE9" Font-Bold="true" Font-Size="x-Small" />
                        <Columns>
                            <asp:TemplateField HeaderText="EDIT" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit" CommandName="EditMaterialMaint" CommandArgument='<%#Bind("SKUID") %>'
                                        Text="Edit" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SKU ID" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSkuId" runat="server" Text='<%#Bind("SKUID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SKU" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSku" runat="server" Text='<%#Bind("SKU") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SPEEDRATING" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpeedrating" runat="server" Text='<%#Bind("SPEEDRATING") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MATERIAL NUMBER" HeaderStyle-Width="150px" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblMatlNum" runat="server" Text='<%#Bind("MATL_NUM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <div align="left">
        <asp:Label Text="Product record with the highest SKUID is the most recent" runat="server"
            ID="lblMessage" Visible="false"></asp:Label>
    </div>
    <div id="divEditMatMaintDetails" visible="false" runat="server" style="float: left;
        padding-top: 20px; width: 650px;">
        <table style="border-style: solid; border-width: medium; border-color: #E3EAEB; height: auto;
            width: 650px; padding: 10px;">
            <tr>
                <td align="right">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small;
                        font-weight: bold;">SKU ID :</span>
                </td>
                <td style="width: 20px;">
                </td>
                <td align="left">
                    <asp:TextBox ID="txtSKUId" runat="server" ReadOnly="true" Enabled="false" Width="140px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small;
                        font-weight: bold;">SKU :</span></td>
                <td style="width: 20px;">
                </td>
                <td align="left">
                    <asp:TextBox ID="txtSKU" runat="server" ReadOnly="true" Enabled="false" Width="140px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small;
                        font-weight: bold;">SPEEDRATING :</span></td>
                <td style="width: 20px;">
                </td>
                <td align="left">
                    <asp:TextBox ID="txtSpeedrating" runat="server" ReadOnly="false" Width="140px" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span style="display: inline-block; font-family: Verdana; color: darkslategray; font-size: x-small;
                        font-weight: bold;">MATERIAL NUMBER:</span></td>
                <td style="width: 20px;">
                </td>
                <td align="left">
                    <asp:TextBox ID="txtMaterialNumber" runat="server" ReadOnly="true" Enabled="false"
                        Width="140px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnUpdateMaterialNo" Text="Update" runat="server" OnClick="btnUpdateMaterialNo_Click" /></td>
                <td style="width: 20px;">
                </td>
                <td align="left">
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Panel>
