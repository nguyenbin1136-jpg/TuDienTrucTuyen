using System;
public partial class TraCuuNgoai : System.Web.UI.Page
{
    protected void Page_Load(object sender,EventArgs e)
    {
        if(!IsPostBack&&Request.QueryString["q"]!=null)txtKeyword.Text=Request.QueryString["q"];
    }
    protected void btnGo_Click(object sender,EventArgs e)
    {
        string q=txtKeyword.Text.Trim();
        if(q.Length==0)
        {
            litMessage.Text="<div class='message'>Vui lòng nhập từ cần tra cứu.</div>";
            return;
        }
        string u="";
        switch(ddlSource.SelectedValue)
        {
            case "cambridge":u="https://dictionary.cambridge.org/dictionary/english/"+Server.UrlEncode(q);
            break;
            case "oxford":u="https://www.oxfordlearnersdictionaries.com/definition/english/"+Server.UrlEncode(q);
            break;
            case "google":u="https://translate.google.com/?sl=auto&tl=vi&text="+Server.UrlEncode(q)+"&op=translate";
            break;
            default:u="https://www.google.com/search?q="+Server.UrlEncode(q+" dictionary");
            break;
        }
        Response.Redirect(u,false);
        Context.ApplicationInstance.CompleteRequest();
    }
}
