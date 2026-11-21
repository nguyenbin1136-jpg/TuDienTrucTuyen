<%@ Page ResponseEncoding="utf-8" Language="C#" %>
<script runat="server">protected void Page_Load(object sender, System.EventArgs e){Session.Clear();Session.Abandon();Response.Redirect("Default.aspx");}</script>
