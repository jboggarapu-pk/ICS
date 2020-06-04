<%@ Page Language="vb" MasterPageFile="~/ICS.master" AutoEventWireup="false" CodeBehind="Approval.aspx.vb" Inherits="CooperTire.ICS.Web.Approval" Title="Approval Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphICSContentHolder" runat="Server">

    <div id="up_container" style="width: 100%">
        <asp:UpdatePanel ID="upnlApprovalUpdatePanel" runat="server">
            <ContentTemplate>

                <table id="TABLE1">
                    <tr>
                   
                        <td colspan="4" style="text-align: left; width: 213px; height: 1px;">
                            <asp:Label ID="lblInfo" runat="server" Text="Approval of test result adjustments." Width="561px"></asp:Label><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left; width: 213px; height: 17px;">
                            <asp:Label ID="lblError" runat="server" SkinID="ErrorText" Width="561px"></asp:Label>
                            </td>
                    </tr>
                    <tr style="height: 100%; width: 100%;" valign="top">
                        <td style="width: 122%; height: 89%;" align="center" colspan="4">
                            <asp:GridView ID="grdAuditLog" SkinID="Professional" runat="server" HorizontalAlign="Left"
                                AutoGenerateColumns="False" Font-Size="X-Small" AllowPaging="True"
                                PagerStyle-HorizontalAlign="Left" OnPageIndexChanging="PageIndexChanging_grdAuditLog" Width="946px" EnableModelValidation="True" >
                                <PagerStyle HorizontalAlign="Left" />
                                <Columns>    
                                         
                                    <asp:TemplateField>
                                    <HeaderTemplate> 
                                       <asp:CheckBox runat="server" ID="SEL" EnableViewState="true" OnCheckedChanged="click_selCheckedChanged" AutoPostBack="true" />
                                    </HeaderTemplate>
                                      <ItemTemplate>
                                          <asp:CheckBox ID="AuditLogEntrySelector" runat="server" EnableViewState="true" />&nbsp;
                                      </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Area" HeaderText="Area of change" />
                                    <asp:BoundField DataField="ChangedFieldElement" HeaderText="Field/Element" ReadOnly="True">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OldValue" HeaderText="Old value" ReadOnly="True" />
                                    <asp:BoundField DataField="NewValue" HeaderText="New value" ReadOnly="True" />
                                    <asp:BoundField DataField="ChangedBy" HeaderText="Changed by" ReadOnly="True" />
                                    <asp:BoundField DataField="ChangeDateTime" HeaderText="Change date" ReadOnly="True" >
                                        <HeaderStyle Width="200px" />
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApprovalStatus" HeaderText="Status" ReadOnly="True">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                   
                                  <asp:TemplateField ShowHeader="False">
                                  <ItemStyle HorizontalAlign="Center" />
                                  <ItemTemplate>
                                     <asp:ImageButton ID="btnApprove" runat="server"  OnClick="Approved_clicked"
                                     CausesValidation="False" CommandName="Approve" 
                                     ImageUrl="~/Images/checked.png" ToolTip="Approve" />
                                   </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField ShowHeader="False">
                                  <ItemStyle HorizontalAlign="Center" />
                                  <ItemTemplate>
                                     <asp:ImageButton ID="btnDeny" runat="server"  OnClick="Deny_clicked"
                                     CausesValidation="False" CommandName="Deny" 
                                     ImageUrl="~/Images/unchecked.png" ToolTip="Deny" />
                                   </ItemTemplate>
                                  </asp:TemplateField>                                  
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 213px" colspan="2">
<asp:Button ID="btnApproveSelected" runat="server" OnClick="Click_btnApproveSelected"
Text="Approve Selected" Width="152px" />
</td>
<td colspan="2" align="left">
<asp:Button ID="btnDenySelected" runat="server" OnClick="Click_btnDenySelected" Text="Deny Selected" Width="151px" />
                            </td>
                    </tr>
                </table>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="upnlApprovalUpdatePanel"
        runat="server">
        <ProgressTemplate>
            <img alt="progress" src="Images/ajax-loader.gif" />
            Processing...
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
