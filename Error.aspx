<%@ Page ResponseEncoding="utf-8" Title="Có lỗi xảy ra" Language="C#" MasterPageFile="~/Site.Master" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="empty">
        <h2>Không thể xử lý yêu cầu</h2>
        <p>Đã xảy ra lỗi trong quá trình xử lý. Vui lòng quay lại trang chủ hoặc thử lại sau.</p>
        <a class="btn btn-primary" href="Default.aspx">Về trang chủ</a>
    </div>
</asp:Content>
