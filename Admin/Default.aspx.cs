using System;
public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender,EventArgs e)
    {
        if(Session["Role"]==null||Session["Role"].ToString()!="admin")
        {
            Response.Redirect("../Login.aspx");
            return;
        }
        if(!IsPostBack)
        {
            litWords.Text=DataStore.GetWords("Tất cả","").Count.ToString();
            litUsers.Text=DataStore.GetUsers().Count.ToString();
        }
    }
}
