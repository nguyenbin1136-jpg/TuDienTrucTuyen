<%@ Page ResponseEncoding="utf-8" Title="Đăng nhập" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-wrap">
        <div class="panel">
            <div class="eyebrow" style="color:#2864dc">CHÀO MỪNG TRỞ LẠI</div>
            <h1>Đăng nhập</h1>
            <p class="muted">Đăng nhập để sử dụng các chức năng dành cho thành viên.</p>
            <asp:Literal ID="litMessage" runat="server" />
            <div class="field">
                <label>Tên đăng nhập</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="input" />
            </div>
        <div class="field">
            <label>Mật khẩu</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input" />
        </div>
    <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
    <p class="muted" style="margin-top:18px">Chưa có tài khoản? <a href="Register.aspx">Đăng ký ngay</a>
    </p>
<div class="note">Tài khoản quản trị mẫu: <b>admin</b> / <b>Admin@123</b>. Hãy đổi mật khẩu sau khi triển khai thực tế.</div>
</div>
</div>
</asp:Content>
