<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ICS.Master" CodeBehind="Query.aspx.vb" Inherits="CooperTire.ICS.Web.Query" 
    title="Query" %>
<%@ Register Src="UserControls/QueryUC.ascx" TagName="QueryUC"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphICSContentHolder" runat="server">
        <uc1:QueryUC ID="QueryUC" runat="server" />
</asp:Content>
