<%@ Page Language="vb" MasterPageFile="~/ICS.master" AutoEventWireup="true" CodeBehind="Error.aspx.vb" Inherits="CooperTire.ICS.Web._Error" %>

<asp:Content ID="contError" ContentPlaceHolderID="cphICSContentHolder" Runat="Server">
        <br />
        <asp:Label ID="lblError" runat="server" Text="An error occured in ICS application. Please see the Event Log for details." Font-Size="Large" ForeColor="Black"></asp:Label>
        <br />
</asp:Content>
