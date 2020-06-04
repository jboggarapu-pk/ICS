<%@ Page Language="vb" MasterPageFile="~/ICS.master" AutoEventWireup="true" Codebehind="CertificationSearchEx.aspx.vb"
    Inherits="CooperTire.ICS.Web.CertificationSearchEx" Title="Certification Page" %>

<%@ Register Src="UserControls/ImarkCertificationUC.ascx" TagName="ImarkCertificationUC"
    TagPrefix="uc5" %>
<%@ Register Src="UserControls/NOMCertificationUC.ascx" TagName="NOMCertificationUC"
    TagPrefix="uc6" %>
<%@ Register Src="UserControls/EmarkCertificationUC.ascx" TagName="EmarkCertificationUC"
    TagPrefix="uc4" %>
<%@ Register Src="UserControls/GSOCertificationUC.ascx" TagName="GSOCertificationUC"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/CCCCertificationUC.ascx" TagName="CCCCertificationUC"
    TagPrefix="uc2" %>
<%@ Register Src="UserControls/AddCertificationUC.ascx" TagName="AddCertificationUC"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/Emark117CertificationUC.ascx" TagName="Emark117CertificationUC"
    TagPrefix="uc7" %>
<%@ Register Src="UserControls/IndiaMarkCertificationUC.ascx" TagName="IndiaMarkCertificationUC"
    TagPrefix="uc8" %>
<%--Added as per project 2706 technical specification--%>
<%@ Register Src="UserControls/RenameCertificationUC.ascx" TagName="RenameCertificationUC"
    TagPrefix="uc9" %>
<%@ Register Src="UserControls/DeleteCertificationUC.ascx" TagName="DeleteCertificationUC"
    TagPrefix="uc10" %>
<%@ Register Src="~/UserControls/DetachOrMoveCertificationUC.ascx" TagName="DetachOrMoveCertificationUC"
    TagPrefix="uc11" %>
<%@ Register Src="~/UserControls/DupCorrectCertificationUC.ascx" TagName="DupCorrectCertificationUC"
    TagPrefix="uc12" %>
<%@ Register Src="~/UserControls/FamilyMaintenanceUC.ascx" TagName="FamilyMaintenanceUC"
    TagPrefix="uc13" %>
<%@ Register Src="~/UserControls/CopyCertificationUC.ascx" TagName="CopyCertificationUC"
    TagPrefix="uc14" %>
<%@ Register Src="UserControls/EditCertificationUC.ascx" TagName="EditCertificationUC"
    TagPrefix="uc15" %>
<%@ Register Src="UserControls/AttachCertificationUC.ascx" TagName="AttachCertificationUC"
    TagPrefix="uc16" %>
<%@ Register Src="UserControls/RefreshProductUC.ascx" TagName="RefreshProductUC"
    TagPrefix="uc17" %>
<%@ Register Src="UserControls/GeneralCertificationUC.ascx" TagName="GeneralCertificationUC"
    TagPrefix="uc18" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphICSContentHolder" runat="Server">

    <script type="text/javascript" language="javascript">
        // hide or unhide controls.
        function pageLoad(sender, args) 
        {    
            var controlName = document.getElementById('<%=ddlAction.ClientId %>');
            Hide(controlName);
        }
      
        function OnSelectedIndexChange(ddlId)
        {
            var controlName = document.getElementById(ddlId.id);
            Hide(controlName);
        } 
               
        function Hide(controlName)
        {
            if(controlName.value == 5)  
             {
               document.getElementById('trMaterialMaint').style.display = 'block';
             }
             else
             {
               document.getElementById('trMaterialMaint').style.display = 'none';
             }
        }
    </script>

    <table style="width: 688px; height: 700px;">
        <tr>
            <td style="height: 20px; width: 196px;" valign="top">
                <asp:UpdatePanel ID="upnlUpdatePanelLeft" runat="server">
                    <ContentTemplate>
                        <table style="border-style: solid; border-width: medium; border-color: #E3EAEB; height: auto;
                            width: 292px;">
                            <tr>
                                <td colspan="2" style="text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left">
                                    <asp:Label ID="lblAddInfo" runat="server" Text="Certificate Maintenance" Width="241px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 199px;">
                                    <asp:Label ID="lblAdd" runat="server" Text="Type Name:" /></td>
                                <td style="width: 117px" align="left">
                                    <asp:DropDownList ID="ddlCertNames" runat="server" Width="134px" Enabled="True" /></td>
                            </tr>
                            <%--Added as per project 2706 technical specification--%>
                            <tr>
                                <td style="text-align: right; width: 199px;">
                                    <asp:Label ID="lblAction" runat="server" Text="Action:" />
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlAction" runat="server" Width="134px" Enabled="True" onchange="OnSelectedIndexChange(this);">
                                        <asp:ListItem Selected="True" Value="0">Select ...</asp:ListItem>
                                        <asp:ListItem Value="1">Add</asp:ListItem>
                                        <asp:ListItem Value="2">Rename</asp:ListItem>
                                        <asp:ListItem Value="3">Detach/Move</asp:ListItem>
                                        <asp:ListItem Value="4">Delete</asp:ListItem>
                                        <asp:ListItem Value="5">Material Maint</asp:ListItem>
                                        <asp:ListItem Value="6">Family Maintenance</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trMaterialMaint">
                                <td style="text-align: right; width: 199px;">
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlMaterialMaint" runat="server" Width="134px">
                                        <asp:ListItem Selected="True" Value="0">Select ...</asp:ListItem>
                                        <asp:ListItem Value="1">Delete</asp:ListItem>
                                        <asp:ListItem Value="2">Copy</asp:ListItem>
                                        <asp:ListItem Value="3">Edit</asp:ListItem>
                                        <asp:ListItem Value="4">Attach</asp:ListItem>
                                        <asp:ListItem Value="5">Refresh Product Data</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <tr>
                                    <td style="text-align: right; width: 199px;">
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnExecute" runat="server" OnClick="Click_btnExecute" Text="Execute"
                                            Width="135px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 199px; text-align: right">
                                        &nbsp;
                                    </td>
                                    <td style="width: 117px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: left">
                                        <asp:Label ID="lblSearchInfo" runat="server" Text="Find Certification" Width="241px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 199px" align="right">
                                        <asp:Label ID="lblSearcType" runat="server" Text="Search Type:" Width="97px" /></td>
                                    <td style="width: 117px">
                                        <asp:DropDownList ID="ddlSearchTypes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectedIndexChanged_ddlSearchTypes"
                                            Width="133px" /></td>
                                </tr>
                                <tr id="trBrand" visible="true" runat="server">
                                    <td style="width: 199px" align="right">
                                        <asp:Label ID="lblBrand" runat="server" Text="Brand:" Width="97px" /></td>
                                    <td style="width: 117px">
                                        <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectedIndexChanged_ddlBrand"
                                            Width="133px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trBrandLine" visible="false" runat="server">
                                    <td style="width: 199px" align="right">
                                        <asp:Label ID="lblBrandLine" runat="server" Text="Brand Line:" Width="97px" /></td>
                                    <td style="width: 117px">
                                        <asp:DropDownList ID="ddlBrandLine" runat="server" Width="206px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trSearch" runat="server" visible="false">
                                    <td style="text-align: right; width: 199px;">
                                        <asp:Label ID="SearchFor" runat="server" Text="Search For:" /></td>
                                    <td style="width: 117px">
                                        <asp:Panel ID="pnlDefaultButton" DefaultButton="btnSearch" runat="server">
                                            <asp:TextBox ID="txtSearchFor" runat="server" Width="126px" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 199px;">
                                        <asp:Label ID="Extension" runat="server" Text="Extension:" Visible="false"></asp:Label>
                                        <asp:Label ID="lblImarkFamily" runat="server" Text="Family:" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExtensionNo" runat="server" Width="63px" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtImarkFamily" runat="server" Width="63px" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblExtInfo" runat="server" Text="* = All Extensions," Visible="false"></asp:Label><br />
                                        <asp:Label ID="lblExtHighest" runat="server" Text="H = Highest Extension" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 199px;">
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnSearch" runat="server" OnClick="Click_btnSearch" Text="Search"
                                            Width="133px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="text-align: left">
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"> </asp:Label></td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TreeView ID="treSearchTypeResults" runat="server" OnSelectedNodeChanged="SelectedNodeChanged_treSearchTypeResults"
                                            ShowLines="True" Width="125px" OnLoad="treSearchTypeResults_Load" ShowExpandCollapse="True">
                                            <Nodes>
                                                <asp:TreeNode Text="New Node" Value="New Node">
                                                    <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                                                    <asp:TreeNode Text="New Node" Value="New Node">
                                                        <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                                                        <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                                                    </asp:TreeNode>
                                                    <asp:TreeNode Text="New Node" Value="New Node">
                                                        <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                                                        <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                                                    </asp:TreeNode>
                                                </asp:TreeNode>
                                            </Nodes>
                                        </asp:TreeView>
                                    </td>
                                </tr>
                        </table>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="height: 20px" valign="top" align="center">
                <asp:UpdateProgress ID="updProgressRight" AssociatedUpdatePanelID="" DynamicLayout="true"
                    runat="server">
                    <ProgressTemplate>
                        <img alt="progress" src="Images/ajax-loader.gif" />
                        Processing...
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="upnlUpdatePanelRight" runat="server">
                    <ContentTemplate>
                        <br />
                        <br />
                        <asp:MultiView ID="mvRight" runat="server">
                            <asp:View ID="viewAddCert" runat="server">
                                <uc3:AddCertificationUC ID="ucAddCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewEmark" runat="server">
                                <uc4:EmarkCertificationUC ID="ucEmarkCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewGSO" runat="server">
                                <uc1:GSOCertificationUC ID="ucGSOCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewNOM" runat="server">
                                <uc6:NOMCertificationUC ID="ucNOMCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewImark" runat="server">
                                <uc5:ImarkCertificationUC ID="ucImarkCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewCCC" runat="server">
                                <uc2:CCCCertificationUC ID="ucCCCCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewEmark117" runat="server">
                                <uc7:Emark117CertificationUC ID="ucEmark117Certification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewIndiaMark" runat="server">
                                <%--yfye add india mark user control--%>
                                <uc8:IndiaMarkCertificationUC ID="ucIndiaMarkCertification" runat="server" />
                            </asp:View>
                              <%--Added as per project 2706 technical specification--%>
                            <asp:View ID="viewRenameCert" runat="server">
                                <uc9:RenameCertificationUC ID="ucRenameCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewDeleteCert" runat="server">
                                <uc10:DeleteCertificationUC ID="ucDeleteCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewDetachOrMove" runat="server">
                                <uc11:DetachOrMoveCertificationUC ID="ucDetachOrMoveCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewDupCorrect" runat="server">
                                <uc12:DupCorrectCertificationUC ID="ucDupCorrectCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewFamilyMaintenance" runat="server">
                                <uc13:FamilyMaintenanceUC ID="ucFamilyMaintenance" runat="server" />
                            </asp:View>
                            <asp:View ID="viewCopyCertification" runat="server">
                                <uc14:CopyCertificationUC ID="ucCopyCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewEditCertification" runat="server">
                                <uc15:EditCertificationUC ID="ucEditCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewAttachCertification" runat="server">
                                <uc16:AttachCertificationUC ID="ucAttachCertification" runat="server" />
                            </asp:View>
                            <asp:View ID="viewRefreshProduct" runat="server">
                                <uc17:RefreshProductUC ID="ucRefreshProduct" runat="server"></uc17:RefreshProductUC>
                            </asp:View>
                            <asp:View ID="viewGeneralCert" runat="server">
                                <%--jeseitz add General certificate user control--%>
                                <uc18:GeneralCertificationUC ID="ucGeneralCertification" runat="server" />
                            </asp:View>
                        </asp:MultiView><br />
                        <br />
                        <br />
                        <br />
                        &nbsp;&nbsp;
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
