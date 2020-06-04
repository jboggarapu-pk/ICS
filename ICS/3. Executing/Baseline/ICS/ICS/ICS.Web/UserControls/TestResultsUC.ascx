<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TestResultsUC.ascx.vb"
    Inherits="CooperTire.ICS.Web.TestResultsUC" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="validation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Panel ID="pnlTestResult" runat="server" Visible="true" CssClass="UCPanel">
    <br />
    <div id="divTestResultTitle_CEGIND">
        <table>
            <tr>
                <td class="TableTitle">
                    <asp:Label ID="title" Text="Test Results" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="TableCellTitle">
                    <asp:Label ID="lblInfoText" runat="server" ForeColor="#0000C0">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TableCellTitle">
                    <asp:Label ID="lblErrorText" runat="server" ForeColor="Red">
                    </asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="ProjectTestList_CEGIND" runat="server">
        <asp:Panel ID="pnlProjectTest_Data" runat="server">
            <table>
                <asp:Panel ID="pnlMeasureProjectTest_EGIN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblMeasureProjectNumber" runat="server" Text="Inspection Lot#"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMeasureProjectNumber" runat="server" MaxLength="12" Width="100px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblMeasureTireNumber" runat="server" Text="Tire Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMeasureTireNumber" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 120px">
                            <validation:PropertyProxyValidator ID="ppvMeasureTireNumber" runat="server" ControlToValidate="txtMeasureTireNumber"
                                PropertyName="TireNumber" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Measure"
                                OnValueConvert="ppvNumber_ValueConvert">
                            </validation:PropertyProxyValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblMeasureOperation" runat="server" Text="Operation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMeasureOperation" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblMeasureTestSpec" runat="server" Text="Test ID"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMeasureTestSpec" runat="server" MaxLength="7" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlPlungerProjectTest_GN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblPlungerProjectNumber" runat="server" Text="Inspection Lot#"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlungerProjectNumber" runat="server" MaxLength="12" Width="100px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblPlungerTireNumber" runat="server" Text="Tire Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlungerTireNumber" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 120px">
                            <validation:PropertyProxyValidator ID="ppvPlungerTireNumber" runat="server" ControlToValidate="txtPlungerTireNumber"
                                PropertyName="TireNumber" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Plunger"
                                OnValueConvert="ppvNumber_ValueConvert" RulesetName="NOM"></validation:PropertyProxyValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblPlungerOperation" runat="server" Text="Operation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlungerOperation" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblPlungerTestSpec" runat="server" Text="Test ID"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlungerTestSpec" runat="server" MaxLength="7" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlBeadUnSeatProjectTest_GN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblBeadUnSeatProjectNumber" runat="server" Text="Inspection Lot#"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBeadUnSeatProjectNumber" runat="server" MaxLength="12" Width="100px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblBeadUnSeatTireNumber" runat="server" Text="Tire Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBeadUnSeatTireNumber" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 120px">
                            <validation:PropertyProxyValidator ID="ppvBeadUnSeatTireNumber" runat="server" ControlToValidate="txtBeadUnSeatTireNumber"
                                PropertyName="TireNumber" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.BeadUnSeat"
                                OnValueConvert="ppvNumber_ValueConvert" RulesetName="NOM"></validation:PropertyProxyValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblBeadUnSeatOperation" runat="server" Text="Operation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBeadUnSeatOperation" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblBeadUnSeatTestSpec" runat="server" Text="Test ID"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBeadUnSeatTestSpec" runat="server" MaxLength="7" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTreadwearProjectTest_EIN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTreadwearProjectNumber" runat="server" Text="Inspection Lot#"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearProjectNumber" runat="server" MaxLength="12" Width="100px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblTreadwearTireNumber" runat="server" Text="Tire Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearTireNumber" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 120px">
                            <validation:PropertyProxyValidator ID="ppvTreadwearTireNumber" runat="server" ControlToValidate="txtTreadwearTireNumber"
                                PropertyName="TireNumber" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Treadwear"
                                OnValueConvert="ppvNumber_ValueConvert" RulesetName="Emark"></validation:PropertyProxyValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblTreadwearOperation" runat="server" Text="Operation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearOperation" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblTreadwearTestSpec" runat="server" Text="Test ID"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearTestSpec" runat="server" MaxLength="7" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceProjectTest_EGIN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceProjectNumber" runat="server" Text="Inspection Lot#"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEnduranceProjectNumber" runat="server" MaxLength="12" Width="100px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblEnduranceTireNumber" runat="server" Text="Tire Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEnduranceTireNumber" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 120px">
                            <validation:PropertyProxyValidator ID="ppvEnduranceTireNumber" runat="server" ControlToValidate="txtEnduranceTireNumber"
                                PropertyName="TireNumber" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Endurance"></validation:PropertyProxyValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblEnduranceOperation" runat="server" Text="Operation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEnduranceOperation" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblEnduranceTestSpec" runat="server" Text="Test ID"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEnduranceTestSpec" runat="server" MaxLength="7" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedProjectTest_EGIN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedProjectNumber" runat="server" Text="Inspection Lot#"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedProjectNumber" runat="server" MaxLength="12" Width="100px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblHighSpeedTireNumber" runat="server" Text="Tire Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedTireNumber" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 120px">
                            <validation:PropertyProxyValidator ID="ppvHighSpeedTireSize" runat="server" ControlToValidate="txtHighSpeedTireNumber"
                                PropertyName="TireNumber" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.HighSpeed"></validation:PropertyProxyValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHighSpeedOperation" runat="server" Text="Operation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedOperation" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblHighSpeedTestSpec" runat="server" Text="Test ID"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedTestSpec" runat="server" MaxLength="7" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlSoundProjectTest_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblSoundProjectNumber" runat="server" Text="Inspection Lot#"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSoundProjectNumber" runat="server" MaxLength="12" Width="100px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblSoundTireNumber" runat="server" Text="Tire Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSoundTireNumber" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 120px">
                            <validation:PropertyProxyValidator ID="ppvSoundTireSize" runat="server" ControlToValidate="txtSoundTireNumber"
                                PropertyName="TireNumber" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Sound"></validation:PropertyProxyValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblSoundOperation" runat="server" Text="Operation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSoundOperation" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblSoundTestSpec" runat="server" Text="Test ID"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSoundTestSpec" runat="server" MaxLength="7" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlWetGripProjectTest_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblWetGripProjectNumber" runat="server" Text="Inspection Lot#"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWetGripProjectNumber" runat="server" MaxLength="12" Width="100px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblWetGripTireNumber" runat="server" Text="Tire Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWetGripTireNumber" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 120px">
                            <validation:PropertyProxyValidator ID="ppvWetGripTireSize" runat="server" ControlToValidate="txtWetGripTireNumber"
                                PropertyName="TireNumber" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.WetGrip"></validation:PropertyProxyValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblWetGripOperation" runat="server" Text="Operation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWetGripOperation" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 60px">
                        </td>
                        <td>
                            <asp:Label ID="lblWetGripTestSpec" runat="server" Text="Test ID"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWetGripTestSpec" runat="server" MaxLength="7" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlGetRequestedTests_EGIN" runat="server">
                    <tr>
                        <td align="center" colspan="9">
                            <asp:Button ID="btnGetRequestedTests" runat="server" Text="Get Requested Tests" />
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="ProductData_CEGINDX" runat="server">
        <asp:Panel ID="pnlProductDataHeader" runat="server" SkinID="CollapsePanelHeader"
            Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">
                    Product Data</div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="imgbtnProductExpend" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlProductData_Data" runat="server" Height="0">
            <table class="ListTable">
                <asp:Panel ID="pnlTPN_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTPN" runat="server" Text="TPN"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTPN" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTrademark_CEGIND" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTrademark" runat="server" Text="Trademark"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTrademark_CEGN" runat="server" Width="220"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTreadPattern_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTreadPattern" runat="server" Text="Tread Pattern"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadPattern_E" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlSizeDesignation" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblSizeDesignation" runat="server" Text="Size Designation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSizeDesignation" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="3" class="TableCellValidator">
                            <validation:PropertyProxyValidator ID="ppvSizeDesignation" runat="server" ControlToValidate="txtSizeDesignation"
                                PropertyName="TireSizeStamp" SourceTypeName="CooperTire.ICS.DomainEntities.Product">
                            </validation:PropertyProxyValidator>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlMudSnow_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblMudSnow_E" runat="server" Text="Mud+Snow"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optlstMudSnow_E" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSevereWeatherIndicator_E" runat="server" Text="Severe Weather Indicator"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optlstSevereWeatherIndicator_E" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlSpecialProtectiveBand_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblSpecialProtectiveBand" runat="server" Text="Special protective band"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSpecialProtectiveBand_E" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlStructureConstruction" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblStructureConstruction" runat="server" Text="Structure/Construction"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optlstStructureConstruction" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Radial</asp:ListItem>
                                <asp:ListItem>Bias</asp:ListItem>
                                <asp:ListItem>Bias Belted</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlSpeedCategory_CEGID" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblSpeedCategory" runat="server" Text="Speed Category"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSpeedCategory_CEGID" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlSingLoadCapacityIndex_CEGID" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblSingLoadCapacityIndex" runat="server" Text="Single Load-Capacity Index"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSingLoadCapacityIndex_CEGID" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlDualLoadCapacityIndex_CEGID" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblDualLoadCapacityIndex" runat="server" Text="Dual Load-Capacity Index"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDualLoadCapacityIndex_CEGID" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlPlyRatingNumber_CEGD" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblPlyRatingNumber" runat="server" Text="Ply-rating number for diagonal (bias-ply) tires"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlyRatingNumber_CEGD" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlIndicationTubeless_CEGD" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblIndicationTubeless" runat="server" Text="Indication - Tubeless/Tube Type"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optlstIndicationTubeless_CEGD" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Tubeless</asp:ListItem>
                                <asp:ListItem>Tube</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlIndicationReinforced_CEGD" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblIndicationReinforced" runat="server" Text="Indication - Reinforced"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optlstIndicationReinforced_CEGD" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlIndicationExtraLoad_CEGD" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblIndicationExtraLoad" runat="server" Text="Indication - ExtraLoad"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optlstIndicationExtraLoad_CEGD" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlRegroovable_CEGD" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblIndicationRegroovable" runat="server" Text="Regroovable"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optlstIndicationRegroovable_CEGD" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlMeasuringRim_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblMeasuringRim" runat="server" Text="Measuring Rim"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMeasuringRim_E" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="3">
                            <validation:PropertyProxyValidator ID="ppvMeaRimWidth" runat="server" ControlToValidate="txtMeasuringRim_E"
                                PropertyName="MeaRimWidth" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Product"
                                EnableClientScript="False">
                            </validation:PropertyProxyValidator>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlMFGWWYY_CEGID" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblMFGWWYY" runat="server" Text="Date of Manufacture(WWYY)"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMFGWWYY_CEGID" runat="server" MaxLength="4"></asp:TextBox>
                        </td>
                        <td colspan="3">
                           <validation:PropertyProxyValidator ID="ppvMFGWWYY" runat="server" ControlToValidate="txtMFGWWYY_CEGID"
                                PropertyName="MFGWWYY" SetFocusOnError="True" SourceTypeName="CooperTire.ICS.DomainEntities.Product"
                                EnableClientScript="False">
                            </validation:PropertyProxyValidator>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTireSerialNumber_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTireSerialNumber" runat="server" Text="Tire Serial Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTireSerialNumber_E" MaxLength="15" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlDOTCode_EN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblDOTCode" runat="server" Text="DOT Code"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDOTCode_E" MaxLength="15" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlNominalTireWidth_CGD" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblNominalTireWidth" runat="server" Text="Nominal Tire Width"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNominalTireWidth_CGD" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlAspectRatio_CGD" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblAspectRatio" runat="server" Text="Aspect Ratio"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAspectRatio_CGD" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlNominalRimDiameter_CEGD" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblNominalRimDiameter" runat="server" Text="Nominal Rim Diameter(inch)"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNominalRimDiameter_CGD" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTemperatureRating_CGD" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTemperatureRating" runat="server" Text="Temperature Rating"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTemperatureRating_CGD" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTraction_CGD" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTraction" runat="server" Text="Traction"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTraction_CGD" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTreadWear_CGD" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTreadWear" runat="server" Text="Treadwear"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadWear_CGD" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlIMarkMudSnow_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblIMarkMudSnow_I" runat="server" Text="Mud+Snow"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optlstIMarkMudSnow_I" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
            </asp:Panel>
                <asp:Panel ID="pnlIMarkSevereWeatherInd_I" runat="server" >
                    <tr>
                        <td>
                            <asp:Label ID="lblIMarkSevereWeatherInd_I" runat="server" Text="Severe Weather Indicator"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optlstIMarkSevereWeatherInd_I" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlManufacturingLocationOfOrigin_CID" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblManufacturingLocationOfOrigin" runat="server" Text="Manufacturing Location / Country of Origin"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtManufacturingLocationOfOrigin_CGIND" runat="server" Width="220"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTreadwearIndicators_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTreadwearIndicators" runat="server" Text="Treadwear Indicators"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearIndicators_I" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlInmetroMark_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblInmetroMark" runat="server" Text="Inmetro Mark"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInmetroMark_I" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlCargoCapacity_N" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblCargoCapacity" runat="server" Text="Cargo Capacity"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCargoCapacity_N" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlType_IN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblType" runat="server" Text="Type"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtType_N" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlNameOfManufacturer_N" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblNameOfManufacturer" runat="server" Text="Name of Manufacturer"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNameOfManufacturer_N" runat="server" Width="220" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTireType" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTireType" runat="server" Text="Tire Type"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlTireType" runat="server" AutoPostBack="true" 
                                DataTextField="TIRETYPENAME" DataValueField="TIRETYPEID" OnSelectedIndexChanged="TireType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtTireId" runat="server" Width="0" Visible="false" ReadOnly="True" Text="0"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlDateOfTest_CGND" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblDateOfTest" runat="server" Text="Date of Test"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDateOfTest_CGND" runat="server"></asp:TextBox>
                            <ajaxtoolkit:CalendarExtender ID="CalDateOfTest_CGND" runat="server" TargetControlID="txtDateOfTest_CGND">
                            </ajaxtoolkit:CalendarExtender>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </asp:Panel>
        <br />
    </asp:Panel>
    <asp:Panel ID="Measurement_EGIN" runat="server">
        <asp:Panel ID="pnlMeasurementHeader" runat="server" SkinID="CollapsePanelHeader"
            Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">
                    Measurement</div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="imgbtnMeasurementExpend" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMeasurement_Data" runat="server" Height="0">
            <table class="ListTable">
                <asp:Panel ID="pnlTestedMaterial_CEGIND" runat="server">
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblMeasureMatlNum_CEGIND" runat="server" Text="Measure Test Material"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTreadwearMatlNum_CEGIND" runat="server" Text="Treadwear Test Material"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPlungerMatlNum_CEGIND" runat="server" Text="Plunger Test Material"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBeadUnseatMatlNum_CEGIND" runat="server" Text="Unseat Test Material"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTyre" runat="server" Text="Tire"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMeasureMatlNum_CEGIND" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearMatlNum_CEGIND" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlungerMatlNum_CEGIND" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBeadUnseatMatlNum_CEGIND" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblGTSpec" runat="server" Text="GT Spec"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGTSpecMeasureMatlNum_CEGIND" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGTSpecTreadwearMatlNum_CEGIND" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGTSpecPlungerMatlNum_CEGIND" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGTSpecBeadUnseatMatlNum_CEGIND" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlDOTSerialNumber_CEGIND" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblDOTSerialNumber_CEGIND" runat="Server" Text="DOT Serial Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDOTSerialNumber_CEGIND" runat="Server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlInflationPresure_E" runat="server">
                    <tr>
                        <th>
                            &nbsp;</th>
                        <th>
                            Date</th>
                        <th>
                            Time</th>
                        <th>
                            Inflation Pressure</th>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtInflationPressure_E" Visible="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStart" runat="server" Text="Start"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate_E" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            <ajaxtoolkit:CalendarExtender ID="CalStartDate_E" runat="server" TargetControlID="txtStartDate_E">
                            </ajaxtoolkit:CalendarExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartTime_E" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            <ajaxtoolkit:MaskedEditExtender ID="mskStartTime_E" runat="server" TargetControlID="txtStartTime_E"
                                Mask="99:99" MaskType="Time" AcceptAMPM="true">
                            </ajaxtoolkit:MaskedEditExtender>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtStartInflationPressure_E" runat="server"></asp:TextBox>bar
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEnd" runat="server" Text="End"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate_E" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            <ajaxtoolkit:CalendarExtender ID="CalEndDate_E" runat="server" TargetControlID="txtEndDate_E">
                            </ajaxtoolkit:CalendarExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndTime_E" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            <ajaxtoolkit:MaskedEditExtender ID="mskEndTime_E" runat="server" TargetControlID="txtEndTime_E"
                                Mask="99:99" MaskType="Time" AcceptAMPM="true">
                            </ajaxtoolkit:MaskedEditExtender>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEndInflationPressure_E" runat="server"></asp:TextBox>bar
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalTime_E" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlOverallWithTreadwearHeight_G" runat="server">
                    <tr>
                        <th>
                            &nbsp;</th>
                        <th>
                            1</th>
                        <th>
                            2</th>
                        <th>
                            3</th>
                        <th>
                            4</th>
                        <th>
                            5</th>
                        <th>
                            6</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblOverallWidth" runat="server" Text="Overall Width(mm)"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOverallWidth1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOverallWidth2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOverallWidth3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOverallWidth4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOverallWidth5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOverallWidth6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlSectionWidth_EGIN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblSectionWidth" runat="server" Text="Section Width(mm)"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSectionWidth1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSectionWidth2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSectionWidth3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSectionWidth4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSectionWidth5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSectionWidth6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTreadwearHeight_EI" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTreadwearHeight" runat="server" Text="Treadwear Height"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearHeight1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearHeight2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearHeight3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearHeight4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearHeight5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearHeight6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <asp:Panel ID="pnlAverage_GN" runat="server">
                        <td>
                            <asp:Label ID="lblAverageWidth" runat="server" Text="Average Section Width"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtAverageWidth" runat="server"></asp:TextBox>
                        </td>
                    </asp:Panel>
                    <asp:Panel ID="pnlAdjustment_N" runat="server">
                        <td>
                            <asp:Label ID="lblAdjustment" runat="server" Text="Adjustment"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtAdjustment" runat="server"></asp:TextBox>
                        </td>
                    </asp:Panel>
                </tr>
                <asp:Panel ID="pnlActualSizeFactor_N" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblActualSizeFactor" runat="server" Text="Actual Size Factor"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtActualSizeFactor" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlMinimumSizeFactor_N" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblMinimumSizeFactor" runat="server" Text="Minimum Size Factor"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtMinimumSizeFactor" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlCircumference_IN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblCircumference" runat="server" Text="Circumference(mm)"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtCircumference" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlOuterDiameter_EI" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblOuterDiameter" runat="server" Text="Outer Diameter"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtOuterDiameter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlNominalDiameter_EI" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblNominalDiameter" runat="server" Text="Nominal Diameter"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNominalDiameter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlNominalWidth_EIN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblNominalWidth" runat="server" Text="Nominal Width"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNominalWidth" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optlstNominalWidthYN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Pass</asp:ListItem>
                                <asp:ListItem>Fail</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlNominalDifference_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblNominalDifference" runat="server" Text="Difference"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNominalDifference" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlNominalTolerance_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblNominalTolerance" runat="server" Text="Tolerance"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNominalTolerance" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlMaxOverallWidth_G" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblMaxOverallWidth" runat="server" Text="Max Overall Width"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtMaxOverallWidth" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlMaxOverallDiameter_G" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblMaxOverallDiameter" runat="server" Text="Max Overall Diameter"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtMaxOverallDiameter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlMinOverallDiameter_G" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblMinOverallDiameter" runat="server" Text="Min Overall Diameter"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtMinOverallDiameter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlOW_G" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblOW" runat="server" Text="Average Overall Width"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtOW" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optlstOWYN" runat="server" RepeatDirection="Horizontal"
                                Visible="False">
                                <asp:ListItem Selected="True">Pass</asp:ListItem>
                                <asp:ListItem>Fail</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlOD_GN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblOD" runat="server" Text="O.D."></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtOD" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optlstODYN" runat="server" RepeatDirection="Horizontal"
                                Visible="False">
                                <asp:ListItem Selected="True">Pass</asp:ListItem>
                                <asp:ListItem>Fail</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlOverallDifference_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblOverallDifference" runat="server" Text="Difference"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtOverallDifference" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlOverallTolerance_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblOverallTolerance" runat="server" Text="Tolerance"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtOverallTolerance" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlRimRim_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblRimRim" runat="server" Text="Rim"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtRimRim" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlRimPressure_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblRimPressure" runat="server" Text="Pressure"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtRimPressure" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTreadwearIndicator_N" runat="server">
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblTreadwearResult" runat="server" Text="Result"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTreadwearRequirement" runat="server" Text="Requirement"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTreadwearIndictor" runat="server" Text="Treadwear indicator"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearIndicatorsResult" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTreadwearIndicatorsRequirement" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optlstTreadwearIndicatorsYN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Pass</asp:ListItem>
                                <asp:ListItem>Fail</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlPlunger_GN" runat="server">
                    <tr>
                        <td colspan="7">
                            <asp:Label ID="lblPlunger" runat="server" Text="Plunger"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            1&nbsp;<asp:TextBox ID="txtPlunger1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblPlungerAverage" runat="server" Text="Average"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlungerAverage" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlungerAverageJ" runat="server" SkinID="MediumTextBox" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            2&nbsp;<asp:TextBox ID="txtPlunger2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            3&nbsp;<asp:TextBox ID="txtPlunger3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            4&nbsp;<asp:TextBox ID="txtPlunger4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            5&nbsp;<asp:TextBox ID="txtPlunger5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlPlungerPassYN_N" runat="server">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optlstPlungerYN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Pass</asp:ListItem>
                                <asp:ListItem>Fail</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlBeadUnseatTest_GN" runat="server">
                    <tr>
                        <td colspan="7">
                            <asp:Label ID="lblBeadUnseatTest" runat="server" Text="Bead Unseat Test"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            1&nbsp;<asp:TextBox ID="txtBeadUnseatTest1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            2&nbsp;<asp:TextBox ID="txtBeadUnseatTest2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblBeadUnseatTestKN" runat="server" Text="Bead Unseat(avg)"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBeadUnseatTestKN" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            3&nbsp;<asp:TextBox ID="txtBeadUnseatTest3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            4&nbsp;<asp:TextBox ID="txtBeadUnseatTest4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            5&nbsp;<asp:TextBox ID="txtBeadUnseatTest5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlBeadUnSeatPassYN_N" runat="server">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optlstBeadUnseatTestYN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Pass</asp:ListItem>
                                <asp:ListItem>Fail</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <asp:Panel ID="pnlTensileStrength_G" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTensileStrength" runat="server" Text="Tensile Strength"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTensileStrength1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTensileStrength2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlElongation_G" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblElongation" runat="server" Text="Elongation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtElongation1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtElongation2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTensileStrengthAfterAging_G" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTensileStrengthAfterAging" runat="server" Text="Tensile Strength After Aging"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTensileStrengthAfterAging1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTensileStrengthAfterAging2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTemperatureResistanceGrading_G" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblTemperatureResistanceGrading" runat="server" Text="Temperature Resistance Grading"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTemperatureResistanceGrading" runat="server" MaxLength="1"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </asp:Panel>
        <br />
    </asp:Panel>
    <asp:Panel ID="EnduranceTestBefore_EI" runat="server">
        <asp:Panel ID="pnlEnduranceTestBeforeHeader" runat="server" SkinID="CollapsePanelHeader"
            Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">
                    Endurance Test General Before</div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="imgbtnTestGeneralBeforeExpend" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlEnduranceTestBefore_Data" runat="server" Height="0">
            <table class="ListTable">
                <asp:Panel ID="pnlEnduranceTestMachine_EI" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceTestMachine" runat="server" Text="Test Machine"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceTestMachine" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceDiameterTestDrum_EI" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceDiameterTestDrum" runat="server" Text="Diameter Test Drum"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceDiameterTestDrum" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceTestRim_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceTestRim" runat="server" Text="Test Rim"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceTestRim" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceDateOfManufacture_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceDateOfManufacture" runat="server" Text="Date Of Manufacture"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceDateOfManufacture" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceTestWheelPosition_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceTestWheelPosition" runat="server" Text="Test Wheel Position"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceTestWheelPosition" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceTireSerialNumber_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceTireSerialNumber" runat="server" Text="Tire Serial Number"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceTireSerialNumber" MaxLength="15" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceDOTCode_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceDOTCode" runat="server" Text="DOT Code"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceDOTCode" MaxLength="15" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEndurancePreconditioningTime_E" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEndurancePreconditioningTime" runat="server" Text="Tire was pre-conditioned for a minimal of "></asp:Label>
                            <asp:TextBox ID="txtEndurancePreconditioningTime" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:Label ID="lblEndurancePreconditioningTimeHour" runat="server" Text=" hours"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEndurancePreconditioningTemperature_E" runat="server">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEndurancePreconditioningTemperature" runat="server" Text="Pre-conditioned room temperature was "></asp:Label>
                            <asp:TextBox ID="txtEndurancePreconditioningTemperature" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceInflationPressure_E" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceInflationPressure" runat="server" Text="Inflation Pressure"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceInflationPressure" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceInflationPressureAdjusted_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceInflationPressureAdjusted" runat="server" Text="Inflation Pressure Adjusted"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceInflationPressureAdjusted" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceOuterDiametersBeforeTitle_EI" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceOuterDiametersBeforeTitle" runat="server" Text="Outer Diameter before testing"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td colspan="6">
                        <table>
                            <tr>
                                <asp:Panel ID="pnlEnduranceCircumferenceBeforeTitle_EI" runat="server">
                                    <td>
                                        <asp:Label ID="lblEnduranceCircumferenceBefore" runat="server" Text="Circumference(mm)"></asp:Label>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceOuterDiameterBeforeTitle_EI" runat="server">
                                    <td>
                                        <asp:Label ID="lblEnduranceOuterDiameterBefore" runat="server" Text="Outer Diameter"></asp:Label>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceTestInflationPressureBeforeTitle_E" runat="server">
                                    <td>
                                        <asp:Label ID="lblEnduranceTestInflationPressureBefore" runat="server" Text="Infl. Pressure"></asp:Label>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlEnduranceCircumferenceBefore_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceCircumferenceBefore" runat="server"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceOuterDiameterBefore_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceOuterDiameterBefore" runat="server"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceTestInflationPressureBefore_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceTestInflationPressureBefore" runat="server"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
    </asp:Panel>
    <asp:Panel ID="Endurance_EGIN" runat="server">
        <asp:Panel ID="pnlEnduranceHeader" runat="server" SkinID="CollapsePanelHeader" Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">
                    Endurance</div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="imgbtnEnduranceExpend" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlEndurance_Data" runat="server" Height="0">
            <table class="ListTable">
                <asp:Panel ID="pnlEnduranceMaterial_EGIN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceMatlNum_EGIN" runat="server" Text="Tested Material"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblGTSpecEnduranceMatlNum_EGIN" runat="server" Text="GT Spec"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtEnduranceMatlNum" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGTSpecEnduranceMatlNum_EGIN" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceLowPressureStartInflation_N" runat="server">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEnduranceLowPressureStartInflation" runat="server" Text="Low Pressure Start Inflation"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceLowPressureStartInflation" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceLowPressureEndInflation_N" runat="server">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEnduranceLowPressureEndInflation" runat="server" Text="Low Pressure End Inflation"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceLowPressureEndInflation" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceLowPressureEndTemp_N" runat="server">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEnduranceLowPressureEndTemp" runat="server" Text="Low Pressure End Temperature"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceLowPressureEndTemp" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceInflationPressureBefore_GIN" runat="server">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEnduranceInflationPressureBefore" runat="server" Text="Inflation Pressure Before"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceInflationPressureBefore" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceTemperatureBefore_I" runat="server">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEnduranceTemperatureBefore" runat="server" Text="Temperature Before"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceTemperatureBefore" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceSpeed_N" runat="server">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEnduranceSpeed" runat="server" Text="Speed"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceSpeed" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <table>
                            <tr>
                                <asp:Panel ID="pnlEnduranceDate_E" runat="server">
                                    <th>
                                        Date</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceClock_E" runat="server">
                                    <th>
                                        Clock Time</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceHours_EIN" runat="server">
                                    <th>
                                        Hours</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceTotalKm_E" runat="server">
                                    <th>
                                        Total Km</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceAirPressure_E" runat="server">
                                    <th>
                                        Air Pressure</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceRoomTemp_EIN" runat="server">
                                    <th>
                                        Room Temp</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoad_EIN" runat="server">
                                    <th>
                                        Load(kg)</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoadPerc_EIN" runat="server">
                                    <th>
                                        Load(%)</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceDetailSpeed_E" runat="server">
                                    <th>
                                        Speed</th>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlEnduranceDate0_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceDate0" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceClock0_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceClockTime0" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceHours0_EIN" runat="server">
                                    <td>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceTotalKm0_E" runat="server">
                                    <td>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceAirPressure0_E" runat="server">
                                    <td>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceRoomTemp0_EIN" runat="server">
                                    <td>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoad0_EIN" runat="server">
                                    <td>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoadPerc0_EIN" runat="server">
                                    <td>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceSpeed0_E" runat="server">
                                    <td>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlEnduranceDate1_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceDate1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceClock1_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceClockTime1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceHours1_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceHours1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceTotalKm1_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceTotalKm1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceAirPressure1_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceAirPressure1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceRoomTemp1_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceRoomTemperature1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoad1_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceLoadKG1" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoadPerc1_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceLoadPercentage1" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceSpeed1_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceSpeed1" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlEnduranceDate2_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceDate2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceClock2_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceClockTime2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceHours2_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceHours2" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceTotalKm2_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceTotalKm2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceAirPressure2_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceAirPressure2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceRoomTemp2_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceRoomTemperature2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoad2_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceLoadKG2" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoadPerc2_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceLoadPercentage2" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceSpeed2_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceSpeed2" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlEnduranceDate3_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceDate3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceClock3_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceClockTime3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceHours3_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceHours3" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceTotalKm3_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceTotalKm3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceAirPressure3_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceAirPressure3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceRoomTemp3_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceRoomTemperature3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoad3_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceLoadKG3" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoadPerc3_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceLoadPercentage3" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceSpeed3_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceSpeed3" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlEnduranceDate4_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceDate4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceClock4_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceClockTime4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceHours4_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceHours4" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceTotalKm4_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceTotalKm4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceAirPressure4_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceAirPressure4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceRoomTemp4_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceRoomTemperature4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoad4_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceLoadKG4" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceLoadPerc4_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceLoadPercentage4" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceSpeed4_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceSpeed4" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                        </table>
                    </td>
                </tr>
                <asp:Panel ID="pnlEnduranceFinalTotalKM_I" runat="server">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblEnduranceFinalTotalKM" runat="server" Text="Total"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceFinalTotalKM" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceTestPassYN_GN" runat="server">
                    <tr>
                        <td colspan="2">
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optlstEnduranceTestPassYN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Pass</asp:ListItem>
                                <asp:ListItem>Fail</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceInflationPressureAfter_GIN" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEnduranceInflationPressureAfter" runat="server" Text="Inflation Pressure After"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceInflationPressureAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceTemperatureAfter_IN" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEnduranceTemperatureAfter" runat="server" Text="Temperature After"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceTemperatureAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceXHours_GI" runat="server">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEnduranceXHours" runat="server" Text="Endurance 34 Hours"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEnduranceXHours" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceTestResultYN_" runat="server">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblEnduranceTestResultYN" runat="server" Text="Result"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optlstEnduranceTestResultYN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Pass</asp:ListItem>
                                <asp:ListItem>Fail</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlVisualVerification_I" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblVisualVerification" runat="server" Text="Visual Verification (after test)"
                                Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlPossibleFailuresFound_I" runat="server">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblPossibleFailuresFound" runat="server" Text="Possible Failures Found"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtPossibleFailuresFound" MaxLength="10" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </asp:Panel>
        <br />
    </asp:Panel>
    <asp:Panel ID="EnduranceTestAfter_EIN" runat="server">
        <asp:Panel ID="pnlEnduranceTestAfterHeader" runat="server" SkinID="CollapsePanelHeader"
            Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">
                    Endurance Test General After</div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="imgbtnTestGeneralAfterExpend" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlEnduranceTestAfter_Data" runat="server" Height="0">
            <table class="ListTable">
                <asp:Panel ID="pnlEndurancePostConditioningTime_E" runat="server">
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="lblEndurancePostConditioningTime" runat="server" Text="Tire was post-conditioned for a minimal of  "></asp:Label>
                            <asp:TextBox ID="txtEndurancePostConditioningTime" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:Label ID="lblEndurancePostConditioningTimeHour" runat="server" Text=" hours"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceOuterDiametersAfterTitle_EI" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEnduranceOuterDiameterAfterTitle" runat="server" Text="Outer Diameter after testing"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td colspan="6">
                        <table>
                            <tr>
                                <asp:Panel ID="pnlEnduranceCircumferenceAfterTitle_EI" runat="server">
                                    <td>
                                        <asp:Label ID="lblEnduranceCircumferenceAfter" runat="server" Text="Circumference(mm)"></asp:Label>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceOuterDiameterAfterTitle_EI" runat="server">
                                    <td>
                                        <asp:Label ID="lblEnduranceOuterDiameterAfter" runat="server" Text="Outer Diameter(mm)"></asp:Label>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceTestInflationPressureAfterTitle_E" runat="server">
                                    <td>
                                        <asp:Label ID="lblEnduranceTestInflationPressureAfter" runat="server" Text="Infl. Pressure(bar)"></asp:Label>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlEnduranceCircumferenceAfter_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceCircumferenceAfter" runat="server"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceOuterDiameterAfter_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceOuterDiameterAfter" runat="server"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlEnduranceTestInflationPressureAfter_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtEnduranceTestInflationPressureAfter" runat="server"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                        </table>
                    </td>
                </tr>
                <asp:Panel ID="pnlEnduranceDifferenceOuterDiameterMMAfterTitle_E" runat="server">
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="lblEnduranceDifferenceOuterDiameterMMAfterTitle" runat="server" Text="Difference outer diameter before and after testing(max 3.5%)"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceDifferenceOuterDiameterMMAfter_E" runat="server">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblEnduranceDifferenceOuterDiameterMMAfter" runat="server" Text="mm"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtEnduranceDifferenceOuterDiameterMMAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceDifferenceOuterDiameterToleranceAfter_E" runat="server">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblEnduranceDifferenceOuterDiameterToleranceAfter" runat="server"
                                Text="Percent Change in Diameter"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtEnduranceDifferenceOuterDiameterToleranceAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceSeriesAfter_N" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblEnduranceSeriesAfter" runat="server" Text="Series"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtEnduranceSeriesAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceFinalJudgementAfter_I" runat="server">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblEnduranceFinalJudgementAfter" runat="server" Text="Final Judgement"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="optlstEnduranceFinalJudgement" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Approved</asp:ListItem>
                                <asp:ListItem>Not Approved</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlEnduranceApproverAfter_E" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblEnduranceApproverAfter" runat="server" Text="Approver"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtEnduranceApproverAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </asp:Panel>
        <br />
    </asp:Panel>
    <asp:Panel ID="HighSpeedTestBefore_EI" runat="server">
        <asp:Panel ID="pnlHighSpeedTestBeforeHeader" runat="server" SkinID="CollapsePanelHeader"
            Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">
                    High Speed Test General Before</div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="imgbtnHighSpeedTestBeforeExpend" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlHighSpeedTestBefore_Data" runat="server" Height="0">
            <table class="ListTable">
                <asp:Panel ID="pnlHighSpeedTestMachine_EI" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedTestMachine" runat="server" Text="Test Machine"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtHighSpeedTestMachine" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedDiameterTestDrum_EI" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedDiameterTestDrum" runat="server" Text="Diameter Test Drum"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtHighSpeedDiameterTestDrum" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedTestRim_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedTestRim" runat="server" Text="Test Rim"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtHighSpeedTestRim" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedDateOfManufacture_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedDateOfManufacture" runat="server" Text="Date Of Manufacture"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtHighSpeedDateOfManufacture" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedTestWheelPosition_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedTestWheelPosition" runat="server" Text="Test Wheel Position"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtHighSpeedTestWheelPosition" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedTireSerialNumber_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedTireSerialNumber" runat="server" Text="Tire Serial Number"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtHighSpeedTireSerialNumber" MaxLength="15" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedDOTCode_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedDOTCode" runat="server" Text="DOT Code"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtHighSpeedDOTCode" MaxLength="15" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedPreconditioningTime_E" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblHighSpeedPreconditioningTime" runat="server" Text="Tire was pre-conditioned for "></asp:Label>
                            <asp:TextBox ID="txtHighSpeedPreconditioningTime" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:Label ID="lblHighSpeedPreconditioningTimeHour" runat="server" Text=" hours"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedPreconditioningTemperature_E" runat="server">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblHighSpeedPreconditioningTemperature" runat="server" Text="Pre-conditioned room temperature was "></asp:Label>
                            <asp:TextBox ID="txtHighSpeedPreconditioningTemperature" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedInflationPressure_EI" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedInflationPressure" runat="server" Text="Inflation Pressure"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtHighSpeedInflationPressure" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedInflationPressureAdjusted_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedInflationPressureAdjusted" runat="server" Text="Inflation Pressure Adjusted"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtHighSpeedInflationPressureAdjusted" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedOuterDiametersTitle_EI" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedOuterDiametersTitle" runat="server" Text="Outer Diameter before testing"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedCircumferenceBefore_EI" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedCircumferenceBefore" runat="server" Text="Circumference(mm)"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedCircumferenceBefore" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedOuterDiameterBefore_EI" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedOuterDiameterBefore" runat="server" Text="Outer Diameter"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedOuterDiameterBefore" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedTestInflationPressureBefore_E" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedTestInflationPressureBefore" runat="server" Text="Infl. Pressure"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedTestInflationPressureBefore" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </asp:Panel>
        <br />
    </asp:Panel>
    <asp:Panel ID="HighSpeed_EGIN" runat="server">
        <asp:Panel ID="pnlHighSpeedHeader" runat="server" SkinID="CollapsePanelHeader" Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">
                    High Speed</div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="imgbtnHighSpeedExpend" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlHighSpeed_Data" runat="server" Height="0">
            <table class="ListTable">
                <asp:Panel ID="pnlTestedMaterial_EGIN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedMatlNum" runat="server" Text="Tested Material"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblGTSpecHighSpeedMatlNum" runat="server" Text="GT Spec"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtHighSpeedMatlNum" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGTSpecHighSpeedMatlNum" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedInfPressureBefore_GIN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedInfPressureBefore" runat="server" Text="Inflation Pressure Before"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedInfPressureBefore" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedTempBefore_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedTempBefore" runat="server" Text="Temperature Before"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedTempBefore" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedDurationTitle_EIN" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedDurantionTitle" runat="server" Text="Duration"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td colspan="8">
                        <table>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedDurationTableStepTitle_EI" runat="server">
                                    <th>
                                        Step</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableDateTitle_E" runat="server">
                                    <th>
                                        Date</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTimeTitle_E" runat="server">
                                    <th>
                                        Time</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTotalTitle_EI" runat="server">
                                    <th>
                                        Total</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableAcSpeedTitle_E" runat="server">
                                    <th>
                                        Actual Speed</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableRoomTempCTitle_E" runat="server">
                                    <th>
                                        Room Temp(C)</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoadTitle_EIN" runat="server">
                                    <th>
                                        Load(kg)</th>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoadPerTitle_N" runat="server">
                                    <th>
                                        Load(%)</th>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedDurationTableStep0_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationStep0" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableDate0_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationDate0" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTime0_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTime0" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTotal0_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTotalTime0" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableAcSpeed0_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationActSpeed0" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableRoomTempC0_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationRoomTemp0" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoad0_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoad0" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoadPerc0_N" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoadPerc0" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedDurationTableStep1_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationStep1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableDate1_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationDate1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTime1_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTime1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTotal1_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTotalTime1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableAcSpeed1_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationActSpeed1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableRoomTempC1_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationRoomTemp1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoad1_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoad1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoadPerc1_N" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoadPerc1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedDurationTableStep2_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationStep2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableDate2_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationDate2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTime2_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTime2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTotal2_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTotalTime2" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableAcSpeed2_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationActSpeed2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableRoomTempC2_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationRoomTemp2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoad2_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoad2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoadPerc2_N" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoadPerc2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedDurationTableStep3_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationStep3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableDate3_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationDate3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTime3_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTime3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTotal3_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTotalTime3" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableAcSpeed3_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationActSpeed3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableRoomTempC3_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationRoomTemp3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoad3_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoad3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoadPerc3_N" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoadPerc3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedDurationTableStep4_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationStep4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableDate4_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationDate4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTime4_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTime4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTotal4_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTotalTime4" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableAcSpeed4_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationActSpeed4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableRoomTempC4_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationRoomTemp4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoad4_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoad4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoadPerc4_N" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoadPerc4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedDurationTableStep5_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationStep5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableDate5_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationDate5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTime5_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTime5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTotal5_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTotalTime5" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableAcSpeed5_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationActSpeed5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableRoomTempC5_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationRoomTemp5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoad5_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoad5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoadPerc5_N" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoadPerc5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedDurationTableStep6_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationStep6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableDate6_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationDate6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTime6_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTime6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTotal6_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTotalTime6" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableAcSpeed6_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationActSpeed6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableRoomTempC6_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationRoomTemp6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoad6_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoad6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoadPerc6_N" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoadPerc6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedDurationTableStep7_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationStep7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableDate7_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationDate7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTime7_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTime7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTotal7_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTotalTime7" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableAcSpeed7_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationActSpeed7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableRoomTempC7_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationRoomTemp7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoad7_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoad7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoadPerc7_N" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoadPerc7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedDurationTableStep8_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationStep8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableDate8_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationDate8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTime8_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTime8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableTotal8_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationTotalTime8" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableAcSpeed8_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationActSpeed8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableRoomTempC8_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationRoomTemp8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoad8_EIN" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoad8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedDurationTableLoadPerc8_N" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedDurationLoadPerc8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                        </table>
                    </td>
                </tr>
                <asp:Panel ID="pnlHighSpeedTotalTime_G" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedTotalTime" runat="server" Text="Total Time"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedTotalTime" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedMaxSpeed_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedMaxSpeed" runat="server" Text="Max Speed"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedMaxSpeed" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedMaxLoad_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedMaxLoad" runat="server" Text="Max Load"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedMaxLoad" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedWheelSpeed_I" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedWheelSpeed" runat="server" Text="Wheel Speed"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedWheelSpeedRPM" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblHighSpeedWheelSpeedrpm" runat="server" Text="rpm"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedWheelSpeedKMH" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblHighSpeedWheelSpeedkmh" runat="server" Text="kmh"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedPassYN_GN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedPassYN" runat="server" Text="Pass/Fail"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optlstHighSpeedDurationPassYN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Pass</asp:ListItem>
                                <asp:ListItem>Fail</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedInfPressureAfter_GIN" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedInfPressureAfter" runat="server" Text="Inflation Pressure After"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedInfPressureAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedTempAfter_IN" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedTempAfter" runat="server" Text="Temperature After"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHighSpeedTempAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </asp:Panel>
        <br />
    </asp:Panel>
    <asp:Panel ID="HighSpeedTestAfter_EIN" runat="server">
        <asp:Panel ID="pnlHighSpeedTestAfterHeader" runat="server" SkinID="CollapsePanelHeader"
            Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">
                    High Speed Test General After</div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="imgbtnHighSpeedTestAfterExpend" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlHighSpeedTestAfter_Data" runat="server" Height="0">
            <table class="ListTable">
                <asp:Panel ID="pnlHighSpeedPostConditioningTime_E" runat="server">
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="lblHighSpeedPostConditioningTime" runat="server" Text="Tire was post-conditioned for a minimal of  "></asp:Label>
                            <asp:TextBox ID="txtHighSpeedPostConditioningTime" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:Label ID="lblHighSpeedPostConditioningTimeHour" runat="server" Text=" hours"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedOuterDiametersAfterTitle_EI" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblHighSpeedOuterDiametersAfterTitle" runat="server" Text="Outer Diameter after testing"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td colspan="6">
                        <table>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedCircumferenceAfterTitle_EI" runat="server">
                                    <td>
                                        <asp:Label ID="lblHighSpeedCircumferenceAfter" runat="server" Text="Circumference(mm)"></asp:Label>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedOuterDiameterAfterTitle_EI" runat="server">
                                    <td>
                                        <asp:Label ID="lblHighSpeedOuterDiameterAfter" runat="server" Text="Outer Diameter(mm)"></asp:Label>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedTestInflationPressureAfterTitle_E" runat="server">
                                    <td>
                                        <asp:Label ID="lblHighSpeedTestInflationPressureAfter" runat="server" Text="Infl. Pressure(bar)"></asp:Label>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlHighSpeedCircumferenceAfter_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedCircumferenceAfter" runat="server"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedOuterDiameterAfter_EI" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedOuterDiameterAfter" runat="server"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                                <asp:Panel ID="pnlHighSpeedTestInflationPressureAfter_E" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtHighSpeedTestInflationPressureAfter" runat="server"></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                        </table>
                    </td>
                </tr>
                <asp:Panel ID="pnlHighSpeedDifferenceOuterDiameterMMAfterTitle_E" runat="server">
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="lblHighSpeedDifferenceOuterDiameterMMAfterTitle" runat="server" Text="Difference outer diameter before and after testing(max 3.5%)"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedDifferenceOuterDiameterMMAfter_E" runat="server">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblHighSpeedDifferenceOuterDiameterMMAfter" runat="server" Text="mm"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtHighSpeedDifferenceOuterDiameterMMAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedDifferenceOuterDiameterToleranceAfter_E" runat="server">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblHighSpeedDifferenceOuterDiameterToleranceAfter" runat="server"
                                Text="Percent Change in Diameter"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtHighSpeedDifferenceOuterDiameterToleranceAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedSeriesAfter_N" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblHighSpeedSeriesAfter" runat="server" Text="Series"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtHighSpeedSeriesAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedFinalJudgementAfter_I" runat="server">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblHighSpeedFinalJudgementAfter" runat="server" Text="Final Judgement"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="optlstHighSpeedFinalJudgement" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Approved</asp:ListItem>
                                <asp:ListItem>Not Approved</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHighSpeedApproverAfter_E" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblHighSpeedApproverAfter" runat="server" Text="Approver"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtHighSpeedApproverAfter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </asp:Panel>
        <br />
    </asp:Panel>
    <asp:Panel ID="SoundWetGrip_E" runat="server">
        <asp:Panel ID="pnlSoundWetGripHeader" runat="server" SkinID="CollapsePanelHeader"
            Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">
                    Sound & Wet Grip</div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="imgbtnSoundWetGripExpend" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSoundWetGrip_Data" runat="server" Height="0">
            <table class="ListTable">
                <asp:Panel ID="pnlSoundWetGrip_E" runat="server">
                    <tr>
                        <td colspan="12">
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSWTestReportNo" runat="server" Text="Test Report No."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSWTestReportNo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSWManuNameOrBrandName" runat="server" Text="Manufacturer and Brand Name or Trade Description"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSWManuNameOrBrandName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSWTireClass" runat="server" Text="Tire CLass"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSWTireClass" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSWCategoryOfUse" runat="server" Text="Category of Use"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSWCategoryOfUse" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSoundDateOfTest" runat="server" Text="Date of Test"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundDateOfTest" runat="server"></asp:TextBox>
                                        <ajaxtoolkit:CalendarExtender ID="calSoundDateOfTest" runat="server" TargetControlID="txtSoundDateOfTest">
                                        </ajaxtoolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSoundTestVehicle" runat="server" Text="Test Vehicle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTestVehicle" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSoundTestVehicleWheelbase" runat="server" Text="Test Vehicle Wheelbase"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTestVehicleWheelbase" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSoundLocationOfTestTrack" runat="server" Text="Location Of TestTrack"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLocationOfTestTrack" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSoundDateOfTrackCertification" runat="server" Text="Date Of Track Certification to ISO 10844:1994"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundDateOfTrackCertification" runat="server"></asp:TextBox>
                                        <ajaxtoolkit:CalendarExtender ID="calSoundDateOfTrackCertification" runat="server"
                                            TargetControlID="txtSoundDateOfTrackCertification">
                                        </ajaxtoolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSoundTireSizeDesignation" runat="server" Text="Tire Size Designation"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTireSizeDesignation" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSoundTireServiceDescription" runat="server" Text="Tire Service Description"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTireServiceDescription" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSoundReferenceInflationPressure" runat="server" Text="Reference Inflation Pressure kPa"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundReferenceInflationPressure" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            <table>
                                <tr>
                                    <th>
                                        Test Data
                                    </th>
                                    <th>
                                        Front L
                                    </th>
                                    <th>
                                        Front R
                                    </th>
                                    <th>
                                        Rear L
                                    </th>
                                    <th>
                                        Rear R
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundTestMass" runat="server" Text="Test Mass"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTestMassFL" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTestMassFR" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTestMassRL" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTestMassRR" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundTireLoadIndex" runat="server" Text="Tire Load Index"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTireLoadIndexFL" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTireLoadIndexFR" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTireLoadIndexRL" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTireLoadIndexRR" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundInflationPressure" runat="server" Text="Inflation Pressure"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundInflationPressureFL" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundInflationPressureFR" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundInflationPressureRL" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundInflationPressureRR" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundTestRimWidthCode" runat="server" Text="Test Rim Width Code"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTestRimWidthCode" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundTemperatureMeasurementSensorType" runat="server" Text="Temperature Measurement Sensor Type"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTemperatureMeasurementSensorType" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSoundValidTestResults" runat="server" Text="Valid Test Results"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            <table>
                                <tr>
                                    <th>
                                        Run No.
                                    </th>
                                    <th>
                                        Test Speed km/h
                                    </th>
                                    <th>
                                        Direction of run
                                    </th>
                                    <th>
                                        Sound Level Left 2/measured dB(A)
                                    </th>
                                    <th>
                                        Sound Level Right 2/measured dB(A)
                                    </th>
                                    <th>
                                        Air Temp
                                    </th>
                                    <th>
                                        Track Temp
                                    </th>
                                    <th>
                                        Sound Level Left 2/temp-corrected dB(A)
                                    </th>
                                    <th>
                                        Sound Level Right 2/temp-corrected dB(A)
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundRunNo1" runat="server" Text="1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundSpeed1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundDirectOfRun1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftMeasured1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightMeasured1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundAirTemp1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTrackTemp1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftTempCorrected1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightTempCorrected1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundRunNo2" runat="server" Text="2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundSpeed2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundDirectOfRun2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftMeasured2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightMeasured2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundAirTemp2" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTrackTemp2" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftTempCorrected2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightTempCorrected2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundRunNo3" runat="server" Text="3"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundSpeed3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundDirectOfRun3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftMeasured3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightMeasured3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundAirTemp3" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTrackTemp3" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftTempCorrected3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightTempCorrected3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundRunNo4" runat="server" Text="4"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundSpeed4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundDirectOfRun4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftMeasured4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightMeasured4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundAirTemp4" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTrackTemp4" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftTempCorrected4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightTempCorrected4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundRunNo5" runat="server" Text="5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundSpeed5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundDirectOfRun5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftMeasured5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightMeasured5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundAirTemp5" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTrackTemp5" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftTempCorrected5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightTempCorrected5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundRunNo6" runat="server" Text="6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundSpeed6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundDirectOfRun6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftMeasured6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightMeasured6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundAirTemp6" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTrackTemp6" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftTempCorrected6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightTempCorrected6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundRunNo7" runat="server" Text="7"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundSpeed7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundDirectOfRun7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftMeasured7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightMeasured7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundAirTemp7" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTrackTemp7" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftTempCorrected7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightTempCorrected7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSoundRunNo8" runat="server" Text="8"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundSpeed8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundDirectOfRun8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftMeasured8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightMeasured8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundAirTemp8" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundTrackTemp8" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelLeftTempCorrected8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSoundLevelRightTempCorrected8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblWetDateOfTest" runat="server" Text="Date of Test"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetDateOfTest" runat="server"></asp:TextBox>
                                        <ajaxtoolkit:CalendarExtender ID="calWetDateOfTest" runat="server" TargetControlID="txtWetDateOfTest">
                                        </ajaxtoolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblWetTestVehicle" runat="server" Text="Test Vehicle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetTestVehicle" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblWetLocationOfTestTrack" runat="server" Text="Location Of TestTrack"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetLocationOfTestTrack" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblWetTestTrackCharacteristics" runat="server" Text="Test Track Characteristics"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetTestTrackCharacteristics" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblWetIssuedBy" runat="server" Text="Issued By"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetIssuedBy" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblWetMethodOfCertification" runat="server" Text="Method Of Certification"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetMethodOfCertification" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblWetTestTireDetail" runat="server" Text="Test Tire Detail"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetTestTireDetail" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblWetTireSizeDesignation" runat="server" Text="Tire Size Designation and Service Description"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetTireSizeDesignation" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblWetTireBrand" runat="server" Text="Tire Brand and Trade Description"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtwetTireBrand" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblWetReferenceInflationPressure" runat="server" Text="Reference Inflation Pressure kPa"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetReferenceInflationPressure" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblWetTestData" runat="server" Text="Test Data"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            <table>
                                <tr>
                                    <th>
                                        Tire
                                    </th>
                                    <th>
                                        SRTT
                                    </th>
                                    <th>
                                        Candidate
                                    </th>
                                    <th>
                                        Control
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetTestTireLoad" runat="server" Text="Test Tire Load kg"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetTestTireLoadSRTT" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetTestTireLoadCandidate" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetTestTireLoadControl" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetWaterDepth" runat="server" Text="Water Depth mm"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWaterDepthSRTT" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWaterDepthCandidate" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWaterDepthControl" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetWettedTrackTemperature" runat="server" Text="Wetted Track Temperature Average"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWettedTrackTemperature" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetTestRimWidthCode" runat="server" Text="Test Rim Width Code"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetTestRimWidthCode" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetTemperatureMeasurementSensorType" runat="server" Text="Temperature Measurement Sensor Type"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetTemperatureMeasurementSensorType" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetIdentificationOfSRTT" runat="server" Text="Identification of the SRTT"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetIdentificationOfSRTT" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblWetTestResults" runat="server" Text="Valid Test Results"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            <table>
                                <tr>
                                    <th>
                                        Run No.
                                    </th>
                                    <th>
                                        Test Speed km/h
                                    </th>
                                    <th>
                                        Direction of run
                                    </th>
                                    <th>
                                        SRTT
                                    </th>
                                    <th>
                                        Candidate Tire
                                    </th>
                                    <th>
                                        Peak Brake force coefficient(pbfc)
                                    </th>
                                    <th>
                                        Mean Fully Developed deceleration(mfdd)
                                    </th>
                                    <th>
                                        Wet Grip Index(G)
                                    </th>
                                    <th>
                                        Comments
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetRunNo1" runat="server" Text="1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSpeed1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetDirectOfRun1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSRTT1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetCandidateTire1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetPBFC1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetMFDD1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWetGripIndex1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetComments1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetRunNo2" runat="server" Text="2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSpeed2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetDirectOfRun2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSRTT2" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetCandidateTire2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetPBFC2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetMFDD2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWetGripIndex2" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetComments2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetRunNo3" runat="server" Text="3"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSpeed3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetDirectOfRun3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSRTT3" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetCandidateTire3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetPBFC3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetMFDD3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWetGripIndex3" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetComments3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetRunNo4" runat="server" Text="4"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSpeed4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetDirectOfRun4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSRTT4" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetCandidateTire4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetPBFC4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetMFDD4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWetGripIndex4" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetComments4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetRunNo5" runat="server" Text="5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSpeed5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetDirectOfRun5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSRTT5" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetCandidateTire5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetPBFC5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetMFDD5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWetGripIndex5" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetComments5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetRunNo6" runat="server" Text="6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSpeed6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetDirectOfRun6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSRTT6" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetCandidateTire6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetPBFC6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetMFDD6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWetGripIndex6" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetComments6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetRunNo7" runat="server" Text="7"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSpeed7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetDirectOfRun7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSRTT7" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetCandidateTire7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetPBFC7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetMFDD7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWetGripIndex7" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetComments7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWetRunNo8" runat="server" Text="8"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSpeed8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetDirectOfRun8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetSRTT8" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetCandidateTire8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetPBFC8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetMFDD8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetWetGripIndex8" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWetComments8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </asp:Panel>
    </asp:Panel>
    <ajaxtoolkit:CollapsiblePanelExtender ID="cpeProductData" runat="Server" TargetControlID="pnlProductData_Data"
        ExpandControlID="pnlProductDataHeader" CollapseControlID="pnlProductDataHeader"
        Collapsed="True" TextLabelID="Label1" ImageControlID="imgbtnProductExpend" ExpandedImage="~/images/collapse.jpg"
        CollapsedImage="~/images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
    <ajaxtoolkit:CollapsiblePanelExtender ID="cpeMeasurement" runat="Server" TargetControlID="pnlMeasurement_Data"
        ExpandControlID="pnlMeasurementHeader" CollapseControlID="pnlMeasurementHeader"
        Collapsed="True" TextLabelID="Label1" ImageControlID="imgbtnMeasurementExpend"
        ExpandedImage="~/images/collapse.jpg" CollapsedImage="~/images/expand.jpg" SuppressPostBack="true"
        SkinID="CollapsiblePanelDemo" />
    <ajaxtoolkit:CollapsiblePanelExtender ID="cpeEnduranceTestGeneralBefore" runat="Server"
        TargetControlID="pnlEnduranceTestBefore_Data" ExpandControlID="pnlEnduranceTestBeforeHeader"
        CollapseControlID="pnlEnduranceTestBeforeHeader" Collapsed="True" TextLabelID="Label1"
        ImageControlID="imgbtnTestGeneralBeforeExpend" ExpandedImage="~/images/collapse.jpg"
        CollapsedImage="~/images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
    <ajaxtoolkit:CollapsiblePanelExtender ID="cpeEndurance" runat="Server" TargetControlID="pnlEndurance_Data"
        ExpandControlID="pnlEnduranceHeader" CollapseControlID="pnlEnduranceHeader" Collapsed="True"
        TextLabelID="Label1" ImageControlID="imgbtnEnduranceExpend" ExpandedImage="~/images/collapse.jpg"
        CollapsedImage="~/images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
    <ajaxtoolkit:CollapsiblePanelExtender ID="cpeEnduranceTestGeneralAfter" runat="Server"
        TargetControlID="pnlEnduranceTestAfter_Data" ExpandControlID="pnlEnduranceTestAfterHeader"
        CollapseControlID="pnlEnduranceTestAfterHeader" Collapsed="True" TextLabelID="Label1"
        ImageControlID="imgbtnTestGeneralAfterExpend" ExpandedImage="~/images/collapse.jpg"
        CollapsedImage="~/images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
    <ajaxtoolkit:CollapsiblePanelExtender ID="cpeHighSpeedTestGeneralBefore" runat="Server"
        TargetControlID="pnlHighSpeedTestBefore_Data" ExpandControlID="pnlHighSpeedTestBeforeHeader"
        CollapseControlID="pnlHighSpeedTestBeforeHeader" Collapsed="True" TextLabelID="Label1"
        ImageControlID="imgbtnHighSpeedTestBeforeExpend" ExpandedImage="~/images/collapse.jpg"
        CollapsedImage="~/images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
    <ajaxtoolkit:CollapsiblePanelExtender ID="cpeHighSpeed" runat="Server" TargetControlID="pnlHighSpeed_Data"
        ExpandControlID="pnlHighSpeedHeader" CollapseControlID="pnlHighSpeedHeader" Collapsed="True"
        TextLabelID="Label1" ImageControlID="imgbtnHighSpeedExpend" ExpandedImage="~/images/collapse.jpg"
        CollapsedImage="~/images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
    <ajaxtoolkit:CollapsiblePanelExtender ID="cpeHighSpeedTestGeneralAfter" runat="Server"
        TargetControlID="pnlHighSpeedTestAfter_Data" ExpandControlID="pnlHighSpeedTestAfterHeader"
        CollapseControlID="pnlHighSpeedTestAfterHeader" Collapsed="True" TextLabelID="Label1"
        ImageControlID="imgbtnHighSpeedTestAfterExpend" ExpandedImage="~/images/collapse.jpg"
        CollapsedImage="~/images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
    <ajaxtoolkit:CollapsiblePanelExtender ID="cpeSoundWetGrip" runat="Server" TargetControlID="pnlSoundWetGrip_Data"
        ExpandControlID="pnlSoundWetGripHeader" CollapseControlID="pnlSoundWetGripHeader"
        Collapsed="True" TextLabelID="Label1" ImageControlID="imgbtnSoundWetGripExpend"
        ExpandedImage="~/images/collapse.jpg" CollapsedImage="~/images/expand.jpg" SuppressPostBack="true"
        SkinID="CollapsiblePanelDemo" />
</asp:Panel>
