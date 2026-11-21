<%@ Page ResponseEncoding="utf-8" Title="Trang chủ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="hero">
        <div class="hero-inner">
            <div>TỪ ĐIỂN TRỰC TUYẾN · HỌC TẬP MỖI NGÀY</div>
            <h1>Khám phá ý nghĩa.<br />Kết nối tri thức.</h1>
            <p>Tra cứu nhanh chóng nhiều loại từ điển, xem nghĩa, phiên âm, ví dụ và tra chéo từ các nguồn đáng tin cậy.</p>
            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" CssClass="searchbox">
                <asp:TextBox ID="txtKeyword" runat="server" ClientIDMode="Static" placeholder="Nhập từ hoặc cụm từ bạn muốn tra cứu..." />
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem>Tất cả</asp:ListItem>
                    <asp:ListItem>Anh - Việt</asp:ListItem>
                    <asp:ListItem>Việt - Anh</asp:ListItem>
                    <asp:ListItem>Anh - Anh</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnSearch" runat="server" Text="Tra cứu" CssClass="btn btn-primary" OnClientClick="return validateSearch();" OnClick="btnSearch_Click" />
            </asp:Panel>
        </div>
    </section>

    <div class="section-head">
        <div>
            <h2>Khám phá các loại từ điển</h2>
            <p>Chọn loại từ điển phù hợp với nhu cầu của bạn.</p>
        </div>
        <a href="TraCuu.aspx">Tra cứu nâng cao →</a>
    </div>
    <div class="grid">
        <a class="tile" href="TraCuu.aspx?type=Anh%20-%20Vi%E1%BB%87t">
            <span class="tile-icon">📖</span>
            <h3>Từ điển Anh – Việt</h3>
            <p>Tra nghĩa tiếng Việt, phiên âm và ví dụ của từ tiếng Anh.</p>
        </a>
        <a class="tile" href="TraCuu.aspx?type=Vi%E1%BB%87t%20-%20Anh">
            <span class="tile-icon">↔</span>
            <h3>Từ điển Việt – Anh</h3>
            <p>Tìm từ tiếng Anh tương ứng với từ hoặc cụm từ tiếng Việt.</p>
        </a>
        <a class="tile" href="TraCuu.aspx?type=Anh%20-%20Anh">
            <span class="tile-icon">📖</span>
            <h3>Từ điển Anh – Anh</h3>
            <p>Hiểu định nghĩa tiếng Anh, cách dùng và các từ đồng nghĩa.</p>
        </a>
    </div>

    <div class="section-head">
        <div>
            <h2>Từ vựng nổi bật</h2>
            <p>Một số từ được chuẩn bị sẵn để bạn trải nghiệm.</p>
        </div>
    </div>
    <asp:Repeater ID="rptFeatured" runat="server">
        <ItemTemplate>
            <div class="word-card">
                <div class="word-top">
                    <div>
                        <h3><%#: Eval("Word") %></h3>
                        <div class="pron"><%#: Eval("Pronunciation") %> · <%#: Eval("PartOfSpeech") %></div>
                    </div>
                    <span class="tag"><%#: Eval("DictionaryType") %></span>
                </div>
                <p class="meaning"><%#: Eval("Meaning") %></p>
                <a href='ChiTietTu.aspx?id=<%# Eval("Id") %>'>Xem chi tiết →</a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
