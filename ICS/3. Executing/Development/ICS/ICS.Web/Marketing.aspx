<%@ Page Language="vb" MasterPageFile="~/ICS.Master" AutoEventWireup="false" Codebehind="Marketing.aspx.vb"
    Inherits="CooperTire.ICS.Web.Marketing" Title="Marketing Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphICSContentHolder" runat="Server">
    <div id="up_container" style="width: 100%">
        <asp:UpdatePanel ID="upnlUpdatePanelMarketing" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblInfo" runat="server" Text="Please select Brand and Brand Line to view the certified regions"></asp:Label><br />
                <asp:Label ID="lblSuccess" runat="server" SkinID="SuccessText"></asp:Label>
                <asp:Label ID="lblError" runat="server" SkinID="ErrorText"></asp:Label><br />
                <table>
                    <tr>
                        <td style="width: 77px" align="right">
                            <asp:Label ID="lblBrand" runat="server" Text="Brand:" Width="97px" /></td>
                        <td style="width: 117px">
                            <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectedIndexChanged_ddlBrand"
                                Width="133px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trBrandLine" runat="server" visible="false">
                        <td style="width: 77px" align="right">
                            <asp:Label ID="lblBrandLine" runat="server" Text="Brand Line:" Width="97px" /></td>
                        <td style="width: 117px">
                            <asp:DropDownList ID="ddlBrandLine" runat="server" Width="266px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>                        
                        <td>
                            <asp:Panel ID="pnlDefaultButton" DefaultButton="btnSearchBrand" runat="server">                              
                                <asp:Button ID="btnSearchBrand" runat="server" OnClick="Click_btnSearchBrand" Text="Search"
                                    Width="114px" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 77px; height: 26px;" align="right">
                            <asp:Label ID="lblRegion" runat="server" Text="Region:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCertifiedRegions" Width="155px" runat="server" OnSelectedIndexChanged="SelectChange_ddlCertifiedRegions"
                                AutoPostBack="True" Enabled="false">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hidCertRegionName" runat="server" Value="" />
                        </td>
                        <td>
                            <asp:Label ID="lblAddRegion" runat="server" Text="Add Region:"></asp:Label>
                            <asp:DropDownList ID="ddlUncertifiedRegions" Width="155px" runat="server" OnSelectedIndexChanged="SelectChange_ddlUncertifiedRegions"
                                AutoPostBack="True" Enabled="false">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hidUnCertRegionName" runat="server" Value="" />
                        </td>
                    </tr>
                </table>
                <table style="width: 688px; position: static;">
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Label ID="lblSelectedRegion" runat="server" SkinID="SelectedRegion"></asp:Label>
                            <asp:HiddenField ID="hidDataDirtyFlag" runat="server" />
                        </td>
                    </tr>
                    <tr style="height: 100%; width: 100%;" valign="top">
                        <td style="width: 100%; height: 89%;" align="center" colspan="4">
                            <asp:GridView ID="grdCertRegion" SkinID="Professional" runat="server" HorizontalAlign="Left"
                                AutoGenerateColumns="False" Font-Size="X-Small" AllowPaging="true" PageSize="10"
                                PagerStyle-HorizontalAlign="Left" OnPageIndexChanging="grdCertRegion_PageIndexChanging" OnRowDataBound="grdCertRegion_RowDataBound" >
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Button ID="btnSave" runat="server" OnClick="Click_btnSave" Text="Save" Visible="False" />
                        </td>
                    </tr>
                </table>
                <ajaxtoolkit:ModalPopupExtender ID="ConfirmPopUp" runat="server" PopupControlID="pnlConfirm"
                    TargetControlID="hidDataDirtyFlag" BackgroundCssClass="modalBackground" />
                <asp:Panel ID="pnlConfirm" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="270px">
                    <table>
                        <tr>
                            <td colspan="2">
                                <label>
                                    By switching regions your changes will be lost, would you like to continue?</label>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="Click_Confirm" />&nbsp;
                            </td>
                            <td align="center">
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="Click_Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>               
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="upnlUpdatePanelMarketing"
        runat="server">
        <ProgressTemplate>
            <img alt="progress" src="Images/ajax-loader.gif" />
            Processing...
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%--
    <ajaxtoolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" runat="server"
        TargetControlID="upnlUpdatePanelMarketing">
        <Animations>
            <OnUpdating>
               <Parallel duration=".15" Fps="30">
                    <FadeOut AnimationTarget="up_container" minimumOpacity=".2" />        
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration=".15" Fps="30">
                    <FadeIn AnimationTarget="up_container" minimumOpacity=".2" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxtoolkit:UpdatePanelAnimationExtender>
--%>
</asp:Content>
