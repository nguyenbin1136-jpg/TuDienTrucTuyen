<%@ Page ResponseEncoding="utf-8" Title="Quản lý người dùng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="QuanLyNguoiDung.aspx.cs" Inherits="Admin_QuanLyNguoiDung" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-title">Quản lý người dùng</h1>
    <p class="sub-title">Danh sách tài khoản và nhóm quyền sử dụng website.</p>
    <nav class="admin-nav">
        <a href="Default.aspx">Bảng điều khiển</a>
        <a href="QuanLyTu.aspx">Quản lý từ</a>
        <a href="QuanLyNguoiDung.aspx" class="active">Quản lý người dùng</a>
    </nav>
    <div class="note">Hệ thống có hai nhóm: <b>Quản trị viên</b> (cập nhật thông tin từ điển) và <b>Thành viên</b> (tra cứu từ điển).</div>
    <div class="panel">
        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" CssClass="table" GridLines="None">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="Tên đăng nhập" />
                <asp:BoundField DataField="DisplayName" HeaderText="Họ tên" />
                <asp:BoundField DataField="Role" HeaderText="Nhóm quyền" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
