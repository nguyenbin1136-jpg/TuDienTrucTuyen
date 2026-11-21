using System;
public partial class Admin_QuanLyTu : System.Web.UI.Page
{
    protected void Page_Load(object sender,EventArgs e)
    {
        if(Session["Role"]==null||Session["Role"].ToString()!="admin")
        {
            Response.Redirect("../Login.aspx");
            return;
        }
        if(!IsPostBack)BindWords();
    }
    private void BindWords()
    {
        gvWords.DataSource=DataStore.GetWords("Tất cả","");
        gvWords.DataBind();
    }
    protected void btnSave_Click(object sender,EventArgs e)
    {
        if(txtWord.Text.Trim().Length==0||txtMeaning.Text.Trim().Length==0)
        {
            litMessage.Text="<div class='message'>Từ vựng và nghĩa là thông tin bắt buộc.</div>";
            return;
        }
        DictionaryWord w=new DictionaryWord
        {
            Id=hdnId.Value,DictionaryType=ddlType.SelectedValue,Word=txtWord.Text.Trim(),Pronunciation=txtPron.Text.Trim(),PartOfSpeech=txtPos.Text.Trim(),Meaning=txtMeaning.Text.Trim(),Example=txtExample.Text.Trim(),Synonyms=txtSynonyms.Text.Trim()
        }
        ;
        DataStore.SaveWord(w);
        ClearForm();
        BindWords();
        litMessage.Text="<div class='message success'>Đã lưu thông tin từ vựng.</div>";
    }
    protected void btnCancel_Click(object sender,EventArgs e)
    {
        ClearForm();
    }
    private void ClearForm()
    {
        hdnId.Value="";
        txtWord.Text="";
        txtPron.Text="";
        txtPos.Text="";
        txtMeaning.Text="";
        txtExample.Text="";
        txtSynonyms.Text="";
        litFormTitle.Text="Thêm từ mới";
    }
    protected void gvWords_RowCommand(object sender,System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        string id=Convert.ToString(e.CommandArgument);
        if(e.CommandName=="EditWord")
        {
            DictionaryWord w=DataStore.GetWord(id);
            if(w!=null)
            {
                hdnId.Value=w.Id;
                ddlType.SelectedValue=w.DictionaryType;
                txtWord.Text=w.Word;
                txtPron.Text=w.Pronunciation;
                txtPos.Text=w.PartOfSpeech;
                txtMeaning.Text=w.Meaning;
                txtExample.Text=w.Example;
                txtSynonyms.Text=w.Synonyms;
                litFormTitle.Text="Cập nhật từ: "+Server.HtmlEncode(w.Word);
            }
        }
        else if(e.CommandName=="DeleteWord")
        {
            DataStore.DeleteWord(id);
            litMessage.Text="<div class='message success'>Đã xóa từ vựng.</div>";
            BindWords();
        }
    }
}
