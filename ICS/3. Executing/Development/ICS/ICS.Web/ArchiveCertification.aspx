<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ICS.Master" CodeBehind="ArchiveCertification.aspx.vb" Inherits="CooperTire.ICS.Web.ArchiveCertification" 
    title="Archive" %>
<%@ Register Src="UserControls/ArchiveCertificationUC.ascx" TagName="ArchiveCertificationUC"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphICSContentHolder" runat="server">
        <uc1:ArchiveCertificationUC ID="ArchiveCertificationUC" runat="server" />
</asp:Content>
