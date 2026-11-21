<%@ Page Title="Trang cá nhân" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="CaNhan.aspx.cs" Inherits="CaNhan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="profile-header">
        <div class="profile-avatar"><asp:Literal ID="litAvatar" runat="server" /></div>
        <div>
            <span class="eyebrow">TÀI KHOẢN CỦA BẠN</span>
            <h1 class="page-title">Trang cá nhân</h1>
            <p class="muted">Quản lý thông tin và bảo mật tài khoản TuDienOnline.</p>
        </div>
    </div>

    <asp:Literal ID="litMessage" runat="server" />

    <div class="profile-grid">
        <section class="panel profile-panel">
            <div class="panel-heading">
                <div><h2>Thông tin cá nhân</h2><p class="muted">Thông tin hiển thị trong tài khoản của bạn.</p></div>
            </div>
            <div class="form-group">
                <label>Tên đăng nhập</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="input" ReadOnly="true" />
                <small class="field-help">Tên đăng nhập không thể thay đổi.</small>
            </div>
            <div class="form-group">
                <label>Họ và tên hiển thị</label>
                <asp:TextBox ID="txtDisplayName" runat="server" CssClass="input" MaxLength="80" />
                <asp:RequiredFieldValidator ID="valDisplayName" runat="server" ControlToValidate="txtDisplayName" ErrorMessage="Vui lòng nhập họ tên." CssClass="validation" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label>Vai trò</label>
                <asp:TextBox ID="txtRole" runat="server" CssClass="input" ReadOnly="true" />
            </div>
            <asp:Button ID="btnSaveProfile" runat="server" Text="Lưu thông tin" CssClass="btn btn-primary" OnClick="btnSaveProfile_Click" />
        </section>

        <section class="panel profile-panel">
            <div class="panel-heading">
                <div><h2>Đổi mật khẩu</h2><p class="muted">Dùng mật khẩu hiện tại để xác nhận thay đổi.</p></div>
            </div>
            <div class="form-group">
                <label>Mật khẩu hiện tại</label>
                <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="input" TextMode="Password" />
            </div>
            <div class="form-group">
                <label>Mật khẩu mới</label>
                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="input" TextMode="Password" />
                <small class="field-help">Tối thiểu 6 ký tự.</small>
            </div>
            <div class="form-group">
                <label>Xác nhận mật khẩu mới</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="input" TextMode="Password" />
            </div>
            <asp:Button ID="btnChangePassword" runat="server" Text="Cập nhật mật khẩu" CssClass="btn btn-primary" OnClick="btnChangePassword_Click" CausesValidation="false" />
        </section>
    </div>

    <section class="panel profile-summary">
        <h2>Hoạt động gần đây</h2>
        <p class="muted">Lịch sử tra cứu được lưu trong phiên đăng nhập hiện tại.</p>
        <asp:Panel ID="pnlNoHistory" runat="server" Visible="false" CssClass="empty-state">Bạn chưa thực hiện lượt tra cứu nào trong phiên này.</asp:Panel>
        <asp:Repeater ID="rptRecent" runat="server">
            <HeaderTemplate><ul class="profile-history"></HeaderTemplate>
            <ItemTemplate><li><span class="history-dot"></span><%#: Container.DataItem %></li></ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
        <a class="text-link" href="LichSu.aspx">Xem toàn bộ lịch sử tra cứu →</a>
    </section>
</asp:Content>
