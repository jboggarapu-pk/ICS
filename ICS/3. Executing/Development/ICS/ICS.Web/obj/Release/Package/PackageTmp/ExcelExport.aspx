<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExcelExport.aspx.vb" Inherits="CooperTire.ICS.Web.ExcelExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Excel Export</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tblCCCProdDesc" runat="server">
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblTitle" Text="Product Description Form" Font-Bold="True" Font-Names="Times New Roman" runat="server" Font-Size="Large"></asp:Label>
                </td>
                <td>
                    Application No.
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
                <td>
                    Application Date
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Factory Name
                </td>
                <td>
                    <asp:Label ID="lblBlank" Width="560px" runat="server"></asp:Label>
                </td>
                <td>
                    CCC Factory Code
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCCCProductDesc" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="NO" HeaderText="No">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="TRADEMARK" HeaderText="Trade Mark">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CCCTIRETYPEABBR" HeaderText="Tyre Category">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ASPECTRATIO" HeaderText="Unit/Series">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SIZESTAMP" HeaderText="Size Designation">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SPEEDRATING" HeaderText="Speed Symbol">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SINGLOADINDEX" HeaderText="Load Index">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Ply Rating">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="STANDARDREINFORCED" HeaderText="Standard/Reinforced">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="TREADPATTERN" HeaderText="Tread Pattern Name">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="TREADTYPE" HeaderText="Tread Pattern Type">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Three Peaked mountain snowflake symbol">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="MEASURERIM" HeaderText="Measuring Rim">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DIAGONALRADIAL" HeaderText="Diagonal/Radial">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="TUBELESSYN" HeaderText="Tube-tyre/Tubeless">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Tread Framework Material and Lays">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Sidewall Framework Material and Lays">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Pressure (kPa)">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <table id="tblCCCNote" runat="server">
            <tr>
                <td>
                    Note:
                </td>
                <td colspan="3">
                    <asp:Label ID="Label1" Text="1. Tyre Category:PC,LT,TB,MC" Font-Names="Times New Roman" runat="server" Font-Size="Smaller"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Label ID="Label2" Text="2. Unit/Series:Classification of Tyre Product Compulsory Certification Product Application Units" Font-Names="Times New Roman" runat="server" Font-Size="Smaller"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Label ID="Label3" Text="3. For Passenger car tyres,Load index should not be lower than the value in GB/T2978-2008; For TB & LT, Load index should not be lower than the value in GB/T2977-2008, and Ply rating should be same as the value in GB/T2977-2008.
                " Font-Names="Times New Roman" runat="server" Font-Size="Smaller"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Label ID="Label4" Text="3. Tread Pattern Type: normal/M+S/Snow for PC, highway/tractor for TB & LT, Front/Non-front for MC" Font-Names="Times New Roman" runat="server" Font-Size="Smaller"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Label ID="Label5" Text="4. Tread Framework Material and Sidewall Framework Material: For example 1Polyester+2Steel+1Nylon" Font-Names="Times New Roman" runat="server" Font-Size="Smaller"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
