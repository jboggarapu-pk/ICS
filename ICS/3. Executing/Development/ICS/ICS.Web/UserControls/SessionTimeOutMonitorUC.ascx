<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SessionTimeOutMonitorUC.ascx.vb"
    Inherits="CooperTire.ICS.Web.SessionTimeOutMonitorUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
    <ContentTemplate>
        <asp:Timer ID="TimerTimeOut" runat="server" OnTick="TimerTimeOut_Tick">
        </asp:Timer>
        <asp:HiddenField ID="hidTimeOutFlag" runat="server"/>
        <ajaxtoolkit:ModalPopupExtender ID="ConfirmPopUp" runat="server" PopupControlID="pnlTimeout"
            TargetControlID="hidTimeOutFlag" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="pnlTimeout" runat="server" CssClass="modalPopup" Style="display: none">
            <asp:Label ID="lblSessionExpireInfo" runat="server" Text="Your Session has expired <br /> Click OK navigate you to ICS home page"></asp:Label><br />
            <asp:Button ID="btnNaviHome" runat="server" Text="OK" OnClick="btnNaviHome_Click" />
        </asp:Panel>
    </ContentTemplate>