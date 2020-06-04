<%@ Page Language="vb" MasterPageFile="~/ICS.master" AutoEventWireup="true" CodeBehind="Home.aspx.vb" Inherits="CooperTire.ICS.Web.Home" %>

<asp:Content ID="contICS" ContentPlaceHolderID="cphICSContentHolder" Runat="Server">
    <br />
    <div id="divTitle" style="vertical-align: middle; text-align: center">
        <asp:Label ID="lblTitle" runat="server" Text="International Certification System" SkinID="HomePageTitle" ForeColor="MidnightBlue"></asp:Label>
    </div>
    <br />
    <br />
    <div id="divMarketing" style="text-align: center">
        <asp:Button ID="btnMarketing" runat="server" Text="Marketing" Width="144px" Enabled="True" Visible="False" /><br />
    </div>
    <br />
    <div id="divMarketingNew" style="text-align: center">
    <asp:Button ID="btnMarketingNew" runat="server" Text="Marketing" Width="144px" Enabled="True" /><br />
     </div>
    <br />
    <div id="divQuality" style="text-align: center">
        <asp:Button ID="btnQuality" runat="server" Text="Quality" Width="144px" Enabled="True" /><br />
    </div>
    <br />
    <br />
    <div id="divInfo" style="vertical-align: middle; text-align: center">
        <asp:Label ID="lblInfo" SkinID="NotificationText" runat="server" ForeColor="MidnightBlue" ></asp:Label>
    </div>
    <br />
</asp:Content>
