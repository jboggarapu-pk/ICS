<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CertificationDefaultsUC.ascx.vb" Inherits="CooperTire.ICS.Web.CertificationDefaultsUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Panel ID="Panel1" runat="server" Width="930px">
<asp:UpdatePanel ID="upnlApprovalUpdatePanel" runat="server">
  <ContentTemplate>
<asp:Label ID="lblErrorText" SkinID="ErrorText" runat="server" EnableViewState="False"></asp:Label>
<table border="0" cellpadding="0" cellspacing="0" style="width: 824px">
<tr>
<td colspan="2" style="height: 4px"></td>
</tr>

<tr>
   <td colspan="2" align="left"><asp:Label ID="lblInfo" runat="server" Text="Certification Type default values" Width="561px"></asp:Label><br />
   </td>
</tr>
<tr>
<td colspan="2" style="height: 4px">&nbsp;</td>
</tr>
<tr>
<td colspan="2" style="height: 2px" align="left"><asp:Label ID="lblInfoText" runat="server" SkinID="SuccessText" Text="&nbsp;"></asp:Label></td>
</tr>
<tr>
<td colspan="2" style="height: 2px">&nbsp;</td>
</tr>
<tr>
<td align="left" colspan="2">
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<td align="right"><asp:Label ID="Label1" runat="server" Text="Certificate Type  :"></asp:Label></td>
<td align="left">&nbsp;<asp:DropDownList ID="ddlCertNames" runat="server" Width="111px" AutoPostBack="True">
    </asp:DropDownList></td>
</tr>
<tr>
<td colspan="2" style="height: 4px"></td>
</tr>

<tr>
<td align="right"><asp:Label ID="lblCertNo" runat="server" Text="Certificate No :"></asp:Label></td>
<td align="left">&nbsp;<asp:TextBox ID="txtCertificateNo" runat="server" Width="108px"></asp:TextBox></td>
</tr>
</table>
</td>
</tr>
<tr>
<td colspan="2" style="height: 4px"></td>
</tr>
<tr>
<td colspan="2" align="center">
    <asp:GridView ID="gvCertificateFields" SkinID="Professional" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" Width="811px">
    <Columns>
    <asp:BoundField DataField="Report" HeaderText="Report" >
        <HeaderStyle HorizontalAlign="Left" Width="300px" />
        <ItemStyle HorizontalAlign="Left" />
    </asp:BoundField>
    <asp:BoundField DataField="Text" HeaderText="Field Label" >
        <HeaderStyle HorizontalAlign="Left" Width="300px" />
        <ItemStyle HorizontalAlign="Left" />
    </asp:BoundField>
     <asp:TemplateField HeaderText="Field Value">
          <ItemTemplate>
               <asp:TextBox ID="FieldValue" ReadOnly="false" Columns="100" Width="500" MaxLength="395" Text='<%# Eval("Value") %>' runat="server" />
               <asp:RadioButtonList ID="FieldRad" Runat="server" Visible="False"></asp:RadioButtonList>
          </ItemTemplate>
         <HeaderStyle HorizontalAlign="Left" Width="400px" />
         <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    </Columns>
    </asp:GridView>
</td>
</tr>
<tr>
<td colspan="2" style="height: 5px">&nbsp;</td>
</tr>
<tr>
<td align="left">
    <asp:Button ID="btnSaveDefault" runat="server" Text="Save Default Value" Width="206px" /></td>
<td align="right" style="width: 243px">
    <asp:Button ID="btnSaveCert" runat="server" Text="Save Certificate value" Width="210px" /></td>
</tr>
</table>

   </ContentTemplate>
</asp:UpdatePanel>
    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="upnlApprovalUpdatePanel"
        runat="server">
        <ProgressTemplate>
            <img alt="progress" src="Images/ajax-loader.gif" />
            Processing...
        </ProgressTemplate>
    </asp:UpdateProgress>
    </asp:Panel>
