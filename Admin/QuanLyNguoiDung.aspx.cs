using System;
public partial class Admin_QuanLyNguoiDung : System.Web.UI.Page
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
            gvUsers.DataSource=DataStore.GetUsers();
            gvUsers.DataBind();
        }
    }
}
