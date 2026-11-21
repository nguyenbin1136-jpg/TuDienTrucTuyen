<%@ Page ResponseEncoding="utf-8" Title="Tra cứu từ điển" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="TraCuu.aspx.cs" Inherits="TraCuu" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-title">Tra cứu từ điển</h1>
    <p class="sub-title">Nhập từ khóa, chọn loại từ điển và xem kết quả phù hợp.</p>
    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" CssClass="panel">
        <div class="form-row">
            <div class="field" style="flex:3">
                <label>Từ cần tra cứu</label>
                <asp:TextBox ID="txtKeyword" runat="server" ClientIDMode="Static" CssClass="input" placeholder="Ví dụ: hello, computer, xin chào..." />
            </div>
            <div class="field">
                <label>Loại từ điển</label>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="select">
                    <asp:ListItem>Tất cả</asp:ListItem>
                    <asp:ListItem>Anh - Việt</asp:ListItem>
                    <asp:ListItem>Việt - Anh</asp:ListItem>
                    <asp:ListItem>Anh - Anh</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="field" style="flex:0 0 130px">
                <label>&nbsp;</label>
                <asp:Button ID="btnSearch" runat="server" Text="Tra cứu" CssClass="btn btn-primary" OnClientClick="return validateSearch();" OnClick="btnSearch_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:Literal ID="litMessage" runat="server" />
    <asp:Repeater ID="rptResults" runat="server">
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
                <div class="example"><b>Ví dụ:</b> <%#: Eval("Example") %></div>
                <p class="muted">Từ đồng nghĩa/liên quan: <%#: Eval("Synonyms") %></p>
                <a href='ChiTietTu.aspx?id=<%# Eval("Id") %>'>Xem chi tiết và tra chéo →</a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Panel ID="pnlEmpty" runat="server" CssClass="empty" Visible="false">Không tìm thấy kết quả phù hợp. Hãy thử từ khóa khác hoặc đổi loại từ điển.</asp:Panel>
</asp:Content>
