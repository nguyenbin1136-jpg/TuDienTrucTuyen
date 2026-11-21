using System;
using System.Collections.Generic;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            List<DictionaryWord> all=DataStore.GetWords("Tất cả","");
            int take=Math.Min(3,all.Count);
            rptFeatured.DataSource=all.GetRange(0,take);
            rptFeatured.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("TraCuu.aspx?q="+Server.UrlEncode(txtKeyword.Text.Trim())+"&type="+Server.UrlEncode(ddlType.SelectedValue));
    }
}
