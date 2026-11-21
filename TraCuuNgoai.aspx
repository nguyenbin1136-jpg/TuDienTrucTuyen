<%@ Page ResponseEncoding="utf-8" Title="Tra cứu từ website khác" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="TraCuuNgoai.aspx.cs" Inherits="TraCuuNgoai" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-title">Tra cứu chéo từ website khác</h1>
    <p class="sub-title">Tìm cùng một từ trên các website từ điển bên ngoài. Kết quả được mở ở tab mới để bạn đối chiếu nhiều nguồn.</p>
    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnGo" CssClass="panel">
        <div class="field">
            <label>Từ hoặc cụm từ cần tra cứu</label>
            <asp:TextBox ID="txtKeyword" runat="server" ClientIDMode="Static" CssClass="input" placeholder="Nhập từ cần tra cứu..." />
        </div>
        <div class="field">
            <label>Nguồn tra cứu</label>
            <asp:DropDownList ID="ddlSource" runat="server" CssClass="select">
                <asp:ListItem Value="cambridge">Cambridge Dictionary (Anh - Anh)</asp:ListItem>
                <asp:ListItem Value="oxford">Oxford Learner's Dictionaries</asp:ListItem>
                <asp:ListItem Value="google">Google Translate</asp:ListItem>
                <asp:ListItem Value="googleSearch">Google Search</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Button ID="btnGo" runat="server" Text="Mở kết quả tra cứu ↗" CssClass="btn btn-primary" OnClientClick="return validateSearch();" OnClick="btnGo_Click" />
    </asp:Panel>
    <asp:Literal ID="litMessage" runat="server" />
    <div class="note">
        <b>Chú ý:</b> Website này chuyển từ khóa sang website nguồn. Nội dung và độ chính xác của kết quả bên ngoài phụ thuộc vào nhà cung cấp tương ứng.</div>
</asp:Content>
