<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ICS.Master" CodeBehind="CertificationDefaults.aspx.vb" Inherits="CooperTire.ICS.Web.CertificationDefaults" 
    title="Default Values" %>

<%@ Register Src="UserControls/CertificationDefaultsUC.ascx" TagName="CertificationDefaultsUC"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphICSContentHolder" runat="server">
    <uc1:CertificationDefaultsUC ID="CertificationDefaultsUC1" runat="server" />
</asp:Content>
