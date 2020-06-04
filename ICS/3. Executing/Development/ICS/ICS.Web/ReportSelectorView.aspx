<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ICS.Master" Codebehind="ReportSelectorView.aspx.vb"
    Inherits="CooperTire.ICS.Web.ReportSelectorView" Title="Available reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphICSContentHolder" runat="server">
    <asp:UpdatePanel ID="upnlUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblInfoText" runat="server" Text="Available reports to submit" Width="561px"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblErrorText" SkinID="ErrorText" runat="server" EnableViewState="False"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 6px">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label1" runat="server" Text="Available Reports:"></asp:Label></td>
                    <td>
                        &nbsp;<asp:DropDownList ID="ddlReports" runat="server" AutoPostBack="True" DataTextField="RptDesc"
                            DataValueField="RptName">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="ParamLine1" runat="server">
                    <td align="right">
                        <asp:Label ID="lblParam1" runat="server" Text="Certification Number:"></asp:Label></td>
                    <td>
                        &nbsp;<asp:TextBox ID="txtParam" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="ParamLine2" runat="server">
                    <td align="right">
                        <asp:Label ID="lblParam2" runat="server" Text="Extension Number:"></asp:Label></td>
                    <td>
                        &nbsp;<asp:TextBox ID="txtParam2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="ParamLine3" runat="server">
                    <td align="right">
                        <asp:Label ID="lblParam3" runat="server" Text="Date:"></asp:Label></td>
                    <td>
                        &nbsp;<asp:TextBox ID="txtParam3" runat="server"></asp:TextBox>
                        <ajaxtoolkit:CalendarExtender ID="CalExtdateTextParam3" runat="server" TargetControlID="txtParam3">
                        </ajaxtoolkit:CalendarExtender>
                    </td>
                </tr>
                <tr id="ParamLine4" runat="server">
                    <td align="right">
                        <asp:Label ID="lblParam4" runat="server" Text="CertificateType:"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:DropDownList ID="ddlCertTypes" runat="server" />
                    </td>
                </tr>
                <tr id="ParamLine5" runat="server">
                    <td align="right">
                        <asp:Label ID="lblParam5" runat="server" Text="Brand :"></asp:Label></td>
                    <td>
                        &nbsp;<asp:TextBox ID="txtParam5" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="ParamLine6" visible="false" runat="server">
                    <td align="right" style="height: 18px">
                        <asp:Label ID="lblParam6" runat="server" Text="Brand:"></asp:Label></td>
                    <td style="height: 18px">
                        &nbsp;<asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectedIndexChanged_ddlBrand"
                            Width="133px">
                        </asp:DropDownList></td>
                </tr>
                <tr id="ParamLine7" visible="false" runat="server">
                    <td align="right">
                        <asp:Label ID="lblParam7" runat="server" Text="Brand Line:"></asp:Label></td>
                    <td>
                        &nbsp;<asp:DropDownList ID="ddlBrandLine" runat="server" Width="266px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="ParamLine8" visible="false" runat="server">
                    <td align="right">
                        <asp:Label ID="lblParam8" runat="server" Text="Include Archived Certificates :"></asp:Label></td>
                    <td>
                        &nbsp;<asp:CheckBox ID="cbIncludeArchivedCertificates" runat="server" Width="266px" Checked="false">
                        </asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 6px">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Load" />
                </tr>
                <tr>
                    <td colspan="2" style="height: 6px">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
        </Triggers>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="upnlUpdatePanel" runat="server">
        <ProgressTemplate>
            <img alt="progress" src="Images/ajax-loader.gif" />
            Processing...
        </ProgressTemplate>
    </asp:UpdateProgress>
    &nbsp;
</asp:Content>
