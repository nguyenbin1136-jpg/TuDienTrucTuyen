<%@ Page ResponseEncoding="utf-8" Title="Chi tiết từ vựng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ChiTietTu.aspx.cs" Inherits="ChiTietTu" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <a href="TraCuu.aspx">← Quay lại tra cứu</a>
    </p>
<asp:Panel ID="pnlWord" runat="server" CssClass="panel">
    <asp:Literal ID="litWord" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlMissing" runat="server" CssClass="empty" Visible="false">Không tìm thấy từ vựng này.</asp:Panel>
</asp:Content>
