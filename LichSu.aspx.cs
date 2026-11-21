using System;
using System.Collections.Generic;
public partial class LichSu : System.Web.UI.Page
{
    protected void Page_Load(object sender,EventArgs e)
    {
        if(Session["Username"]==null)
        {
            pnlLogin.Visible=true;
            return;
        }
        List<string> h=Session["SearchHistory"] as List<string>;
        if(h==null||h.Count==0)
        {
            pnlEmpty.Visible=true;
            return;
        }
        rptHistory.DataSource=h;
        rptHistory.DataBind();
    }
}
