using System;
public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender,EventArgs e)
    {
        DataStore.EnsureFiles();
    }
    protected void btnLogin_Click(object sender,EventArgs e)
    {
        string u=txtUsername.Text.Trim();
        if(u.Length==0||txtPassword.Text.Length==0)
        {
            litMessage.Text="<div class='message'>Vui lòng nhập đầy đủ thông tin.</div>";
            return;
        }
        DictionaryUser user=DataStore.Login(u,txtPassword.Text);
        if(user==null)
        {
            litMessage.Text="<div class='message error'>Tên đăng nhập hoặc mật khẩu không chính xác.</div>";
            return;
        }
        Session.Clear();
        Session["Username"]=user.Username;
        Session["DisplayName"]=user.DisplayName;
        Session["Role"]=user.Role;
        string returnUrl = Request.QueryString["returnUrl"];
        if (!String.IsNullOrEmpty(returnUrl) && returnUrl.StartsWith("/") == false &&
            returnUrl.IndexOf("://", StringComparison.Ordinal) < 0)
            Response.Redirect("~/" + returnUrl);
        Response.Redirect(user.Role=="admin"?"Admin/QuanLyTu.aspx":"Default.aspx");
    }
}
