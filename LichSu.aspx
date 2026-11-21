<%@ Page ResponseEncoding="utf-8" Title="Lịch sử tra cứu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="LichSu.aspx.cs" Inherits="LichSu" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-title">Lịch sử tra cứu</h1>
    <p class="sub-title">Các từ khóa bạn đã tra cứu trong phiên đăng nhập hiện tại.</p>
    <asp:Panel ID="pnlLogin" runat="server" CssClass="note" Visible="false">Vui lòng <a href="Login.aspx">đăng nhập</a> để xem lịch sử tra cứu.</asp:Panel>
    <asp:Repeater ID="rptHistory" runat="server">
        <HeaderTemplate>
            <div class="panel">
                <table class="table">
                    <tr>
                        <th>#</th>
                        <th>Từ khóa · Thời gian</th>
                    </tr>
            </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Container.ItemIndex+1 %>
                </td>
            <td>
                <%#: Container.DataItem %>
            </td>
    </tr>
</ItemTemplate>
<FooterTemplate>
</table>
</div>
</FooterTemplate>
</asp:Repeater>
<asp:Panel ID="pnlEmpty" runat="server" CssClass="empty" Visible="false">Chưa có lịch sử tra cứu trong phiên này.</asp:Panel>
</asp:Content>
