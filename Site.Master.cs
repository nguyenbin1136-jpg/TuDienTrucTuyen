using System;
public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataStore.EnsureFiles();
        string user = Convert.ToString(Session["Username"]);
        string role = Convert.ToString(Session["Role"]);
        phUser.Visible = !String.IsNullOrEmpty(user);
        phAdmin.Visible = role == "admin";
        if (String.IsNullOrEmpty(user))
        {
            litAccount.Text = "<a class='btn btn-outline btn-small' href='" + ResolveUrl("~/Login.aspx") + "'>Đăng nhập</a>" +
                " <a class='btn btn-primary btn-small' href='" + ResolveUrl("~/Register.aspx") + "'>Đăng ký</a>";
        }
        else
        {
            litAccount.Text = "<span class='hello'>Xin chào, " + Server.HtmlEncode(Convert.ToString(Session["DisplayName"])) + "</span>" +
                " <a class='btn btn-outline btn-small' href='" + ResolveUrl("~/CaNhan.aspx") + "'>Tài khoản</a>" +
                " <a class='btn btn-primary btn-small' href='" + ResolveUrl("~/Logout.aspx") + "'>Đăng xuất</a>";
        }
    }
}
