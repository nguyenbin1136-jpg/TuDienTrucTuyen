using System;
using System.Text.RegularExpressions;
public partial class Register : System.Web.UI.Page
{
    protected void btnRegister_Click(object sender,EventArgs e)
    {
        string name=txtName.Text.Trim(),u=txtUsername.Text.Trim(),p=txtPassword.Text;
        if(name.Length==0||u.Length<3||p.Length<6)
        {
            litMessage.Text="<div class='message'>Vui lòng nhập họ tên, tên đăng nhập từ 3 ký tự và mật khẩu từ 6 ký tự.</div>";
            return;
        }
        if(!Regex.IsMatch(u,"^[a-zA-Z0-9_.-]+$"))
        {
            litMessage.Text="<div class='message'>Tên đăng nhập chỉ gồm chữ không dấu, số, dấu chấm, gạch dưới hoặc gạch ngang.</div>";
            return;
        }
        if(p!=txtConfirm.Text)
        {
            litMessage.Text="<div class='message'>Mật khẩu nhập lại không khớp.</div>";
            return;
        }
        if(!DataStore.Register(u,name,p))
        {
            litMessage.Text="<div class='message'>Tên đăng nhập đã tồn tại.</div>";
            return;
        }
        litMessage.Text="<div class='message success'>Đăng ký thành công. Bạn có thể đăng nhập ngay.</div>";
        txtPassword.Text="";
        txtConfirm.Text="";
    }
}
