using System;
using System.Collections.Generic;

public partial class CaNhan : System.Web.UI.Page
{
    private string CurrentUsername
    {
        get { return Convert.ToString(Session["Username"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(CurrentUsername))
        {
            Response.Redirect("~/Login.aspx?returnUrl=CaNhan.aspx");
            return;
        }

        if (!IsPostBack) LoadProfile();
    }

    private void LoadProfile()
    {
        DictionaryUser user = DataStore.GetUser(CurrentUsername);
        if (user == null)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
            return;
        }

        txtUsername.Text = user.Username;
        txtDisplayName.Text = user.DisplayName;
        txtRole.Text = user.Role == "admin" ? "Quản trị viên" : "Thành viên";
        litAvatar.Text = Server.HtmlEncode(
            String.IsNullOrWhiteSpace(user.DisplayName) ? "U" : user.DisplayName.Substring(0, 1).ToUpperInvariant());

        List<string> history = Session["SearchHistory"] as List<string>;
        if (history == null || history.Count == 0)
        {
            pnlNoHistory.Visible = true;
            rptRecent.Visible = false;
        }
        else
        {
            List<string> recent = history.Count > 5 ? history.GetRange(0, 5) : history;
            rptRecent.DataSource = recent;
            rptRecent.DataBind();
        }
    }

    protected void btnSaveProfile_Click(object sender, EventArgs e)
    {
        string displayName = txtDisplayName.Text.Trim();
        if (displayName.Length == 0 || displayName.Length > 80)
        {
            ShowMessage("Họ tên phải có từ 1 đến 80 ký tự.", false);
            return;
        }

        if (DataStore.UpdateDisplayName(CurrentUsername, displayName))
        {
            Session["DisplayName"] = displayName;
            ShowMessage("Đã cập nhật thông tin cá nhân thành công.", true);
            LoadProfile();
        }
        else ShowMessage("Không thể cập nhật thông tin. Vui lòng thử lại.", false);
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        string current = txtCurrentPassword.Text;
        string next = txtNewPassword.Text;
        string confirm = txtConfirmPassword.Text;

        if (String.IsNullOrEmpty(current) || String.IsNullOrEmpty(next) || String.IsNullOrEmpty(confirm))
        {
            ShowMessage("Vui lòng nhập đầy đủ các trường mật khẩu.", false);
            return;
        }
        if (next.Length < 6)
        {
            ShowMessage("Mật khẩu mới phải có ít nhất 6 ký tự.", false);
            return;
        }
        if (next != confirm)
        {
            ShowMessage("Mật khẩu xác nhận không khớp.", false);
            return;
        }
        if (!DataStore.ChangePassword(CurrentUsername, current, next))
        {
            ShowMessage("Mật khẩu hiện tại không chính xác.", false);
            return;
        }

        txtCurrentPassword.Text = "";
        txtNewPassword.Text = "";
        txtConfirmPassword.Text = "";
        ShowMessage("Đổi mật khẩu thành công. Hãy sử dụng mật khẩu mới ở lần đăng nhập tiếp theo.", true);
    }

    private void ShowMessage(string message, bool success)
    {
        litMessage.Text = "<div class='message " + (success ? "success" : "error") + "'>" +
            Server.HtmlEncode(message) + "</div>";
    }
}
