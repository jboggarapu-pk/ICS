<%@ Control Language="vb" AutoEventWireup="false" Codebehind="NOMCertificationUC.ascx.vb"
    Inherits="CooperTire.ICS.Web.NOMCertificationUC" %>
<%@ Register Src="TestResultsUC.ascx" TagName="TestResultsUC" TagPrefix="uc1" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="validation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Panel ID="pnlNOM" runat="server" Visible="True">
    <asp:Panel ID="pnlNOMCertificate" runat="server" Visible="True" CssClass="UCPanel">
        <table>
            <tr>
                <td align="right">
                    <asp:Label ID="lblFormTitle" runat="server" Text="NOM"></asp:Label>
                    <asp:Label ID="lblPopupTarget" runat="server" Width="0px"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:LinkButton ID="lbtnDefaultValues" runat="server" Width="350px">View and modify Certificate user-specified values</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="2" align="left">
                    <asp:Label ID="lblInfoText" runat="server" SkinID="SuccessText" Text="&nbsp;"></asp:Label>
                    <asp:Label ID="lblErrorText" runat="server" SkinID="ErrorText"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCertificationNo" runat="server" Text="Certification No. :"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCertificationNo" runat="server"></asp:TextBox>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvCertificationNUmber" runat="server" ControlToValidate="txtCertificationNo"
                        PropertyName="CertificateNumber" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblExtensionOrRevision" runat="server" Text="Extension/Revision :" Visible="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtExtensionOrRevision" runat="server" ReadOnly="false" Visible="false"></asp:TextBox>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvtxtExtensionOrRevision" runat="server"
                        ControlToValidate="txtExtensionOrRevision" PropertyName="Extension_EN" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblJobReportNo" runat="server" Text="Job / Report No. :" Visible="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtJobReportNo" runat="server" Visible="False"></asp:TextBox>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvJobNumber" runat="server" ControlToValidate="txtJobReportNo"
                        PropertyName="JobReportNumber_CEN" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCertDateSubmitted" runat="server" Text="Date Sent :"></asp:Label></td>
                <td align="left">
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
                    <asp:Label ID="lblRenewalRequired" runat="server" Text="Renewal Required :"></asp:Label>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="optlstRenewalRequired" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvRenewalRequired" runat="server" ControlToValidate="optlstRenewalRequired"
                        PropertyName="RenewalRequired_CGIN" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblActiveStatus" runat="server" Text="Active Status :"></asp:Label></td>
                <td align="left">
                    <asp:RadioButtonList ID="optLstActiveStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvActiveStatus" runat="server" ControlToValidate="optLstActiveStatus"
                        PropertyName="ActiveStatus" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCustomerSpecific" runat="server" Text="Customer Specific :"></asp:Label></td>
                <td align="left">
                    <asp:RadioButtonList ID="optlstCustomerSpecific" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvCustomerSpecific" runat="server" ControlToValidate="optlstCustomerSpecific"
                        PropertyName="CustomerSpecific_N" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCreateAsNewCustomer" runat="server" Text="Create as new customer :"></asp:Label>
                </td>
                <td align="left">
                    <asp:CheckBox ID="cbxCreateAsNewCustomer" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblActSigReq" runat="server" Text="Actual Signature Required :"></asp:Label>
                </td>
                <td align="left">
                    <asp:CheckBox ID="cbxActSigReq" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCustomer" runat="server" Text="Customer :"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCustomer" runat="server" TextMode="MultiLine" Height="50" Width="270"></asp:TextBox>
                    <asp:TextBox ID="txtCustomerID" runat="server" Width="0" Visible="False"></asp:TextBox>
                    <asp:HiddenField ID="hidCurrentCustomer" runat="server" />
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvCustomer" runat="server" ControlToValidate="txtCustomer"
                        PropertyName="Customer_N" SourceTypeName="CooperTire.ICS.DomainEntities.Customer">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCustomerAddress" runat="server" Text="Customer Address :"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCustomerAddress" runat="server" TextMode="MultiLine" Width="270" Height="50" Wrap="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCreateAsNewImporter" runat="server" Text="Create as new importer :"></asp:Label>
                </td>
                <td align="left">
                    <asp:CheckBox ID="cbxCreateAsNewImporter" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblImporter" runat="server" Text="Importer :"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtImporter" runat="server" TextMode="MultiLine" Width="270" Height="25"></asp:TextBox>
                    <asp:TextBox ID="txtImporterID" runat="server" Width="0" Visible="false"></asp:TextBox>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvImporter" runat="server" ControlToValidate="txtImporter"
                        PropertyName="Importer_N" SourceTypeName="CooperTire.ICS.DomainEntities.Customer">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblImporterAddress" runat="server" Text="Importer Address :"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtImporterAddress" runat="server" TextMode="MultiLine" Width="270" Height="50" Wrap="True"></asp:TextBox>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvImporterAddress" runat="server" ControlToValidate="txtImporterAddress"
                        PropertyName="ImporterAddress_N" SourceTypeName="CooperTire.ICS.DomainEntities.Customer">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblImporterRepresentative" runat="server" Text="Importer Representative :"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtImporterRepresentative" runat="server" TextMode="MultiLine" Height="25" Width="270"></asp:TextBox>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvImporterRepresentative" runat="server"
                        ControlToValidate="txtImporterRepresentative" PropertyName="ImporterRepresentative_N"
                        SourceTypeName="CooperTire.ICS.DomainEntities.Customer">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCountryLocation" runat="server" Text="Country Location :" Visible="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCountryLocation" runat="server" Width="220" Visible="False"></asp:TextBox>
                </td>
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
                    <asp:Label ID="lblMaterialNo" runat="server" Text="Material:"></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="txtMaterialNo" runat="server" ReadOnly="True" Width="128px"></asp:TextBox></td>
                <td colspan="3" align="left" style="width: 120px">
                    <validation:PropertyProxyValidator ID="ppvtxtMaterialNo" runat="server" ControlToValidate="txtMaterialNo"
                        PropertyName="MaterialNumber" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="height: 20px">
                    <asp:Label ID="lblProductData" runat="server"></asp:Label>
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
                    <asp:Label ID="lblDateSubmitted" runat="server" Text="Material Date Sent: " Visible="False"></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="txtDateSubmitted" runat="server" Visible="False"></asp:TextBox><br />
                    <ajaxtoolkit:CalendarExtender ID="CalExtdateSubmitted" runat="server" TargetControlID="txtDateSubmitted">
                    </ajaxtoolkit:CalendarExtender>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvDateSubmited" runat="server" ControlToValidate="txtDateSubmitted"
                        PropertyName="DateSubmitted" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblManufactureCountry" runat="server" Text="Country of Manufacture :"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtManufactureCountry" Width="270px" runat="server"></asp:TextBox>
                </td>
                <td colspan="3" align="left">
                    <validation:PropertyProxyValidator ID="ppvManufactureCountry" runat="server" ControlToValidate="txtManufactureCountry"
                        PropertyName="CountryOfManufacture_N" SourceTypeName="CooperTire.ICS.DomainEntities.Certificate">
                    </validation:PropertyProxyValidator></td>
            </tr>
            <tr>
                <td colspan="3" align="center" style="height: 27px">
                    <asp:Button ID="btnGetTestResult" runat="server" Text="Get Results" />
                    <asp:Button ID="btnBlankResult" runat="server" Text="Blank Results" />
                    <asp:Button ID="btnAddCustomer" runat="server" Text="Add Customer" Visible="false" />
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
             TargetControlID="lblInfoText" BackgroundCssClass="modalBackground"  /> 
        <asp:Panel ID="pnlChangedFields" DefaultButton="btnSaveReasons" runat="server" CssClass="modalPopup" Style="display: none"
            Width="1000px">
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
