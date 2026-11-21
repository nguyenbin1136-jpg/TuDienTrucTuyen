using System;
using System.Text;
public partial class ChiTietTu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        DictionaryWord w = DataStore.GetWord(Request.QueryString["id"]);
        if (w == null)
        {
            pnlWord.Visible = false;
            pnlMissing.Visible = true;
            return;
        }
        string word = Server.HtmlEncode(w.Word);
        StringBuilder html = new StringBuilder();
        html.Append("<span class='tag'>").Append(Server.HtmlEncode(w.DictionaryType)).Append("</span>");
        html.Append("<h1 class='page-title' style='margin-top:18px'>").Append(word).Append("</h1>");
        html.Append("<p class='pron'>").Append(Server.HtmlEncode(w.Pronunciation)).Append(" · ").Append(Server.HtmlEncode(w.PartOfSpeech)).Append("</p>");
        html.Append("<h3>Ý nghĩa</h3><p class='meaning'>").Append(Server.HtmlEncode(w.Meaning)).Append("</p>");
        html.Append("<h3>Ví dụ</h3><div class='example'>").Append(Server.HtmlEncode(w.Example)).Append("</div>");
        html.Append("<h3>Từ đồng nghĩa / liên quan</h3><p>").Append(Server.HtmlEncode(w.Synonyms)).Append("</p>");
        html.Append("<div class='external-links'>");
        html.Append("<a class='btn btn-outline' target='_blank' rel='noopener' href='https://dictionary.cambridge.org/dictionary/english/").Append(Server.UrlEncode(w.Word)).Append("'>Tra trên Cambridge ↗</a>");
        html.Append("<a class='btn btn-outline' target='_blank' rel='noopener' href='https://www.google.com/search?q=").Append(Server.UrlEncode(w.Word + " dictionary")).Append("'>Tìm trên Google ↗</a>");
        html.Append("<a class='btn btn-outline' href='TraCuuNgoai.aspx?q=").Append(Server.UrlEncode(w.Word)).Append("'>Tra cứu chéo ↗</a></div>");
        litWord.Text = html.ToString();
    }
}
