<%@ Page ResponseEncoding="utf-8" Title="Đăng ký" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-wrap">
        <div class="panel">
            <h1>Tạo tài khoản</h1>
            <p class="muted">Tài khoản mới được cấp quyền thành viên tra cứu.</p>
            <asp:Literal ID="litMessage" runat="server" />
            <div class="field">
                <label>Họ và tên</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="input" MaxLength="80" />
            </div>
        <div class="field">
            <label>Tên đăng nhập</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="input" MaxLength="30" />
        </div>
    <div class="field">
        <label>Mật khẩu (ít nhất 6 ký tự)</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input" />
    </div>
<div class="field">
    <label>Nhập lại mật khẩu</label>
    <asp:TextBox ID="txtConfirm" runat="server" TextMode="Password" CssClass="input" />
</div>
<asp:Button ID="btnRegister" runat="server" Text="Đăng ký" CssClass="btn btn-primary" OnClick="btnRegister_Click" />
<p class="muted" style="margin-top:18px">Đã có tài khoản? <a href="Login.aspx">Đăng nhập</a>
</p>
</div>
</div>
</asp:Content>
