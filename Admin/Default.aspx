<%@ Page ResponseEncoding="utf-8" Title="Quản trị hệ thống" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-title">Bảng điều khiển quản trị</h1>
    <p class="sub-title">Quản lý dữ liệu từ điển và tài khoản sử dụng.</p>
    <nav class="admin-nav">
        <a href="Default.aspx" class="active">Bảng điều khiển</a>
        <a href="QuanLyTu.aspx">Quản lý từ</a>
        <a href="QuanLyNguoiDung.aspx">Quản lý người dùng</a>
    </nav>
    <div class="stats">
        <div class="stat">
            <strong><asp:Literal ID="litWords" runat="server" /></strong>
            <span>Từ vựng hiện có</span>
        </div>
        <div class="stat">
            <strong><asp:Literal ID="litUsers" runat="server" /></strong>
            <span>Tài khoản</span>
        </div>
        <div class="stat">
            <strong>02</strong>
            <span>Nhóm quyền</span>
        </div>
    </div>
    <div class="grid grid-2">
        <a class="tile" href="QuanLyTu.aspx">
            <span class="tile-icon">📖</span>
            <h3>Quản lý từ điển</h3>
            <p>Thêm mới, chỉnh sửa và xóa mục từ.</p>
            <p style="margin-top:15px">Mở quản lý từ →</p>
        </a>
        <a class="tile" href="QuanLyNguoiDung.aspx">
            <span class="tile-icon">♙</span>
            <h3>Quản lý người dùng</h3>
            <p>Xem danh sách tài khoản và phân nhóm quyền.</p>
            <p style="margin-top:15px">Mở danh sách →</p>
        </a>
    </div>
</asp:Content>
