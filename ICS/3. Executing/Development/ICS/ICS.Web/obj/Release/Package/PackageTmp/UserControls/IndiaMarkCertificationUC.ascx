<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="IndiaMarkCertificationUC.ascx.vb" Inherits="CooperTire.ICS.Web.IndiaMarkCertificationUC" %>
<%@ Register Src="TestResultsUC.ascx" TagName="TestResultsUC" TagPrefix="uc1" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="validation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="pnlIndiaMark" runat="server" Visible="True">
    <asp:Panel ID="pnlIndiaMarkCertificate" runat="server" Visible="True" CssClass="UCPanel">       
        <table>
            <tr>
                <td align="right">
                    <asp:Label ID="lblFormTitle" runat="server" Text="India_Mark"></asp:Label>
                    <asp:Label ID="lblPopupTarget" runat="server" Width="0px"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:LinkButton ID="lbtnDefaultValues" runat="server" Width="350px">View and modify Certificate user-specified values</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2" align="left">
                    <asp:Label ID="lblInfoText" runat="server" SkinID="SuccessText" Text="&nbsp;"></asp:Label>
                    <asp:Label ID="lblErrorText" runat="server" SkinID="ErrorText"></asp:Label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCertificationNo" runat="server" Text="Certification No. :"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCertificationNo" runat="server" Enabled="False"></asp:TextBox></td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvCertificationNUmber" runat="server" ControlToValidate="txtCertificationNo"
                        PropertyName="CertificateNumber" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblJobReportNo" runat="server" Text="Job / Report No. :"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtJobReportNo" runat="server"></asp:TextBox></td>
                <td colspan="3" align="left"> <%--yfye 11/21/2011--%>
                    <validation:PropertyProxyValidator ID="ppvJob" runat="server" ControlToValidate="txtJobReportNo"
                        PropertyName="JobReportNumber_CEN" RulesetName="IndiaMark" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate"></validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCertDateSubmitted" runat="server" Text="Date Submitted :"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCertDateSubmitted" runat="server"></asp:TextBox><br />
                    <ajaxToolkit:CalendarExtender ID="CalExtCertdateSubmitted" runat="server" TargetControlID="txtCertDateSubmitted">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvCertDateSubmitted" runat="server" ControlToValidate="txtCertDateSubmitted"
                        PropertyName="CertDateSubmitted" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCertDateApproved" runat="server" Text="Date Approved :"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCertDateApproved" runat="server"></asp:TextBox><br />
                    <ajaxToolkit:CalendarExtender ID="calextCertDateApproved" runat="server" TargetControlID="txtCertDateApproved">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvCertDateApproved" runat="server" ControlToValidate="txtCertDateApproved"
                        PropertyName="CertDateApproved_CEGI" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate" RulesetName="IndiaMark"></validation:PropertyProxyValidator></td>
            </tr>
             <tr>
                <td align="right">
                    <asp:Label ID="lblDateExpiry" runat="server" Text="Date of Expiration :"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtDateExpiry" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calextDateExpiry" runat="server" TargetControlID="txtDateExpiry">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td align="left">
                    <validation:PropertyProxyValidator ID="ppvIndiamarkExpirydate" runat="server" ControlToValidate="txtDateExpiry"
                        PropertyName="ExpiryDate_I" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate"
                        RulesetName="Imark"></validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblRenewalRequired" runat="server" Text="Renewal Required :"></asp:Label></td>
                <td>
                    <asp:RadioButtonList ID="rbtnlstRenewalRequired" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvRenewalRequired" runat="server" ControlToValidate="rbtnlstRenewalRequired"
                        PropertyName="RenewalRequired_CGIN" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate"></validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblActiveStatus" runat="server" Text="Active Status :"></asp:Label></td>
                <td>
                    <asp:RadioButtonList ID="rbtnLstActiveStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvActiveStatus" runat="server" ControlToValidate="rbtnLstActiveStatus"
                        PropertyName="ActiveStatus" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate"></validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblMaterialNo" runat="server" Text="Material :"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtMaterialNo" runat="server" ReadOnly="True" Width="128px"></asp:TextBox></td>
                <td colspan="3" align="left">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="height: 20px">
                    <asp:Label ID="lblProductData" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblRemoveMatlNum" runat="server" Text="Remove Material:"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="cbxRemoveMatlNum" runat="server" />
                </td>
            </tr>
            <%--Added as per project 2706 technical specification--%>
            <tr>
                <td align="right">
                    <asp:Label ID="lblDiscDateTitle" runat="server" Text="Disc Date :"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDiscDate" runat="server"></asp:Label>
                </td>                          
            </tr>
                        <%--Added for request 203625 --%>
            <tr>
                <td align="right">
                    <asp:Label ID="lblAddInfo" runat="server" Text="Additional Information :"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtAddInfo" runat="server"></asp:TextBox><br />
                </td>                          
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblDateSubmitted" runat="server" Text="Material Date Submitted :"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtDateSubmitted" runat="server"></asp:TextBox><br />
                    <ajaxToolkit:CalendarExtender ID="CalExtdateSubmitted" runat="server" TargetControlID="txtDateSubmitted">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvDateSubmited" runat="server" ControlToValidate="txtDateSubmitted"
                        PropertyName="DateSubmitted" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblDateApproved" runat="server" Text="Material Date Approved :"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtDateApproved" runat="server"></asp:TextBox><br />
                    <ajaxToolkit:CalendarExtender ID="calextDateApproved" runat="server" TargetControlID="txtDateApproved">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvDateApproved" runat="server" ControlToValidate="txtDateApproved"
                        PropertyName="DateApproved_CEGI" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate" RulesetName="IndiaMark"></validation:PropertyProxyValidator></td>
            </tr>
            <%-- JBH_2.00 Project 5325: Added Mold Change Required Checkbox --%>
            <tr>
                <td align="right">
                    <asp:Label ID="lblMoldChgRequired" runat="server" Text="Mold Change Required:"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="cbxMoldChgRequired" runat="server" />
                </td>
            </tr>
            <%-- JBH_2.00 Project 5325: Added Operations Date Approved Text Box --%>
            <tr>
                <td align="right">
                    <asp:Label ID="lblOperDateApproved" runat="server" Text="Operations Date Approved :"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtOperDateApproved" runat="server"></asp:TextBox><br />
                    <ajaxToolkit:CalendarExtender ID="calextOperDateApproved" runat="server" TargetControlID="txtOperDateApproved">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvOperDateApproved" runat="server" ControlToValidate="txtOperDateApproved"
                        PropertyName="OperDateApproved" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate" RulesetName="India_Mark"></validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblProductLocation" runat="server" Text="Product Location :"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtProductLocation" runat="server"></asp:TextBox>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvProductLocation" runat="server" PropertyName="ProductLocation"
                        RulesetName="IndiaMark" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate"
                        ControlToValidate="txtProductLocation"></validation:PropertyProxyValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnGettestResult" runat="server" Text="Get Results" />
                </td>
                <td colspan="4" align="left">
                    <asp:Button ID="btnSave" runat="server" Text="Save" />
                </td>
            </tr>
        </table>
        <ajaxToolkit:ModalPopupExtender ID="SimilarTirePopUp" runat="server" PopupControlID="pnlSimilarTireFound"
            TargetControlID="lblPopupTarget" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="pnlSimilarTireFound" runat="server" CssClass="modalPopup" Style="display: none"
            Width="350px">
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblSimilarTireMessage" runat="server"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:DropDownList ID="ddlSimilarCertificate" runat="server" DataSource="<%# SimilarCertificateDS %>" DataTextField="CERTIFICATENUMBER" DataValueField="CERTIFICATEID" Width="200px"></asp:DropDownList>
                        <asp:Label ID="lblSimilarSKUID" runat="server" Visible="False" Width="0"></asp:Label>
                        <asp:Label ID="lblSimilarMaterial" runat="server" Visible="False" Width="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="Click_Yes" />&nbsp;
                    </td>
                    <td align="center">
                        <asp:Button ID="btnNo" runat="server" Text="No" OnClick="Click_No" />&nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <ajaxtoolkit:ModalPopupExtender ID="ChangedFieldsPopup" runat="server" PopupControlID="pnlChangedFields"
             TargetControlID="lblInfoText" BackgroundCssClass="modalBackground" /> 
        <asp:Panel ID="pnlChangedFields" runat="server" CssClass="modalPopup" Style="display: none"
            Width="1175px">
            <table>
                <tr>
                    <td>
                        <div style="height: 150px; overflow:auto;">
                        <asp:GridView ID="gvChangedFields" SkinID="Professional" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" BorderStyle="Solid">
                            <Columns>
                                <asp:TemplateField HeaderText="Reason For Change">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlReasonList" DataSource="<%# ReasonDS %>" DataTextField="REASON" DataValueField="REASONID" runat="server" Width="250px"></asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Note">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNote" runat="server" MaxLength="50" Width="275px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="275px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Area" HeaderText="Area" >
                                    <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ChangeDateTime" HeaderText="Change Date" >
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ChangedFieldElement" HeaderText="Changed Field" >
                                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ChangeLogID" HeaderText="ChangeLogID">
                                    <HeaderStyle HorizontalAlign="Left"/>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ChangedBy" HeaderText="ChangedBy">
                                    <HeaderStyle HorizontalAlign="Left"/>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OldValue" HeaderText="OldValue">
                                    <HeaderStyle HorizontalAlign="Left"/>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NewValue" HeaderText="NewValue">
                                    <HeaderStyle HorizontalAlign="Left"/>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Approver" HeaderText="Approver">
                                    <HeaderStyle HorizontalAlign="Left"/>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView> 
                        </div>                      
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSaveReasons" runat="server" Text="Save" OnClick="Click_SaveReasons" />&nbsp;
                        <asp:Button ID="btnCancelReasons" runat="server" Text="Cancel" OnClick="Click_CancelReasons" />&nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel> 
    <uc1:TestResultsUC ID="ucTestResults" Visible="false" runat="server" />
</asp:Panel>
