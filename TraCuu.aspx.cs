using System;
using System.Collections.Generic;
public partial class TraCuu : System.Web.UI.Page
{
    protected void Page_Load(object sender,EventArgs e)
    {
        if(!IsPostBack)
        {
            if(Request.QueryString["q"]!=null)txtKeyword.Text=Request.QueryString["q"];
            string t=Request.QueryString["type"];
            if(!String.IsNullOrEmpty(t)&&ddlType.Items.FindByValue(t)!=null)ddlType.SelectedValue=t;
            // Tu dong tim khi co tu khoa hoac da chon mot loai tu dien cu the.
            if(!String.IsNullOrWhiteSpace(txtKeyword.Text)||ddlType.SelectedValue!="Tất cả")Search();
        }
    }
    protected void btnSearch_Click(object sender,EventArgs e)
    {
        Search();
    }
    private void Search()
    {
        string q=txtKeyword.Text.Trim();
        List<DictionaryWord> result=DataStore.GetWords(ddlType.SelectedValue,q);
        rptResults.DataSource=result;
        rptResults.DataBind();
        pnlEmpty.Visible=result.Count==0;
        if(result.Count>0)
        {
            string label=String.IsNullOrWhiteSpace(q)
                ?"loại từ điển <b>"+Server.HtmlEncode(ddlType.SelectedValue)+"</b>"
                :"từ khóa <b>"+Server.HtmlEncode(q)+"</b>";
            litMessage.Text="<div class='note'>Tìm thấy <b>"+result.Count+"</b> kết quả cho "+label+".</div>";
        }
        else
        {
            litMessage.Text="";
        }
        if(Session["Username"]!=null&&!String.IsNullOrEmpty(q))
        {
            List<string> h=Session["SearchHistory"] as List<string>;
            if(h==null)
            {
                h=new List<string>();
                Session["SearchHistory"]=h;
            }
            h.Insert(0,q+" · "+DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            if(h.Count>30)h.RemoveAt(h.Count-1);
        }
    }
}
