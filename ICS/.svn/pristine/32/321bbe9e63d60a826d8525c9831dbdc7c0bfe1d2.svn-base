<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="QueryUC.ascx.vb" Inherits="CooperTire.ICS.Web.QueryUC" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="validation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="pnlArchiveCertification" runat="server" Visible="True" CssClass="UCPanel">
    <asp:Label ID="lblFormTitle" Text="Query Database" runat="server"></asp:Label><br />
    <asp:Label ID="lblErrorText" runat="server" SkinID="ErrorText"></asp:Label>
    <table>
        <tr>
            <td>
                <asp:DropDownList ID="ddlFilterColumn1" runat="server"></asp:DropDownList>  
            </td>
            <td>
                <asp:DropDownList ID="ddlFilter1" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtFilter1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlFilterColumn2" runat="server"></asp:DropDownList>  
            </td>
            <td>
                <asp:DropDownList ID="ddlFilter2" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtFilter2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlFilterColumn3" runat="server"></asp:DropDownList>  
            </td>
            <td>
                <asp:DropDownList ID="ddlFilter3" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtFilter3" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlFilterColumn4" runat="server"></asp:DropDownList>  
            </td>
            <td>
                <asp:DropDownList ID="ddlFilter4" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtFilter4" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlFilterColumn5" runat="server"></asp:DropDownList>  
            </td>
            <td>
                <asp:DropDownList ID="ddlFilter5" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtFilter5" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlFilterColumn6" runat="server"></asp:DropDownList>  
            </td>
            <td>
                <asp:DropDownList ID="ddlFilter6" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtFilter6" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlFilterColumn7" runat="server"></asp:DropDownList>  
            </td>
            <td>
                <asp:DropDownList ID="ddlFilter7" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtFilter7" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnFilter" runat="server" Text="Filter" />
            </td>
        </tr>
    </table>
    
    <asp:GridView ID="gvQuery" CellPadding="5" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" PageSize="20" runat="server" Font-Size="Small" Width="1100px">
        <Columns>
            <asp:BoundField DataField="SKU" HeaderText="SKU"  ReadOnly="True" HeaderStyle-BackColor="#E3EAEB" HeaderStyle-HorizontalAlign="Center"
                 SortExpression="SKU" />
            <asp:BoundField DataField="Brand" HeaderText="Brand" ReadOnly="True" HeaderStyle-BackColor="#E3EAEB" HeaderStyle-HorizontalAlign="Center"
                 SortExpression="Brand"/>
            <asp:BoundField DataField="SizeStamp" HeaderText="Size" ReadOnly="True" HeaderStyle-BackColor="#E3EAEB"
                 SortExpression="SizeStamp"/>
            <asp:BoundField DataField="Reg3054Number" HeaderText="Reg 30/54 Number" ReadOnly="True" HeaderStyle-BackColor="#E3EAEB" HeaderStyle-HorizontalAlign="Center"
                 SortExpression="Reg3054Number"/>
            <asp:BoundField DataField="Reg3054Approval" HeaderText="Reg 30/54 Approval" ReadOnly="True" HeaderStyle-BackColor="#E3EAEB" HeaderStyle-HorizontalAlign="Center"
                 SortExpression="Reg3054Approval"/>
            <asp:BoundField DataField="Reg117Number" HeaderText="Reg 117 Number" ReadOnly="True" HeaderStyle-BackColor="#E3EAEB" HeaderStyle-HorizontalAlign="Center"
                 SortExpression="Reg117Number"/>
            <asp:BoundField DataField="Reg117Approval" HeaderText="Reg 117 Approval" ReadOnly="True" HeaderStyle-BackColor="#E3EAEB" HeaderStyle-HorizontalAlign="Center"
                 SortExpression="Reg117Approval"/>
        </Columns>
    </asp:GridView>
</asp:Panel>