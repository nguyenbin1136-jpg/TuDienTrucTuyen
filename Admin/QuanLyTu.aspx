<%@ Page ResponseEncoding="utf-8" Title="Quản lý từ vựng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="QuanLyTu.aspx.cs" Inherits="Admin_QuanLyTu" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-title">Quản lý từ điển</h1>
    <p class="sub-title">Khu vực dành cho nhóm cập nhật thông tin. Thêm, sửa và xóa dữ liệu từ vựng.</p>
    <nav class="admin-nav">
        <a href="Default.aspx">Bảng điều khiển</a>
        <a href="QuanLyTu.aspx" class="active">Quản lý từ</a>
        <a href="QuanLyNguoiDung.aspx">Quản lý người dùng</a>
    </nav>
    <asp:Literal ID="litMessage" runat="server" />
    <div class="panel">
        <h3><asp:Literal ID="litFormTitle" runat="server" Text="Thêm từ mới" /></h3>
        <asp:HiddenField ID="hdnId" runat="server" />
        <div class="form-row">
            <div class="field">
                <label>Loại từ điển</label>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="select">
                    <asp:ListItem>Anh - Việt</asp:ListItem>
                    <asp:ListItem>Việt - Anh</asp:ListItem>
                    <asp:ListItem>Anh - Anh</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="field">
                <label>Từ vựng</label>
                <asp:TextBox ID="txtWord" runat="server" CssClass="input" MaxLength="100" />
            </div>
            <div class="field">
                <label>Phiên âm</label>
                <asp:TextBox ID="txtPron" runat="server" CssClass="input" />
            </div>
            <div class="field">
                <label>Từ loại</label>
                <asp:TextBox ID="txtPos" runat="server" CssClass="input" />
            </div>
        </div>
        <div class="field">
            <label>Nghĩa / định nghĩa</label>
            <asp:TextBox ID="txtMeaning" runat="server" CssClass="textarea" TextMode="MultiLine" />
        </div>
        <div class="field">
            <label>Ví dụ</label>
            <asp:TextBox ID="txtExample" runat="server" CssClass="textarea" TextMode="MultiLine" />
        </div>
        <div class="field">
            <label>Từ đồng nghĩa / liên quan</label>
            <asp:TextBox ID="txtSynonyms" runat="server" CssClass="input" />
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Lưu từ vựng" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Làm mới" CssClass="btn btn-outline" OnClick="btnCancel_Click" CausesValidation="false" />
    </div>
    <div class="panel">
        <h3>Danh sách từ vựng</h3>
        <asp:GridView ID="gvWords" runat="server" AutoGenerateColumns="false" CssClass="table" GridLines="None" OnRowCommand="gvWords_RowCommand">
            <Columns>
                <asp:BoundField DataField="Word" HeaderText="Từ" />
                <asp:BoundField DataField="DictionaryType" HeaderText="Loại từ điển" />
                <asp:BoundField DataField="PartOfSpeech" HeaderText="Từ loại" />
                <asp:BoundField DataField="Meaning" HeaderText="Nghĩa" />
                <asp:TemplateField HeaderText="Thao tác">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" Text="Sửa" CommandName="EditWord" CommandArgument='<%# Eval("Id") %>' /> | <asp:LinkButton ID="btnDelete" runat="server" Text="Xóa" CssClass="link-danger" CommandName="DeleteWord" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirmDelete('Bạn chắc chắn muốn xóa từ này?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
