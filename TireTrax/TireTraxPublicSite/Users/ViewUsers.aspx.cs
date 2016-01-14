using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Configuration;
using System.Threading;
using System.Data;

public partial class Users_ViewUsers : BasePage
{
    public int CurPageNum
    {
        get
        {
            if (Request.QueryString["p"] != null)
                return Conversion.ParseInt(Request.QueryString["p"]);
            else
                return 1;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        GetPermission(ResourceType.Users, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }


        if (User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("/");
        }
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liUsers','{0}');", ResourceMgr.GetMessage("Users")), true);
        if (!IsPostBack)
        {
            SearchAdminUsers(1);
        }

        if (TotalItems > 0)
        {
            pager.DrawPager(CurrentPage, TotalItems, pageSize, MaxPagesToShow);
        }

        if (Request.QueryString["OrganizationTypeID"] == null && Request.QueryString["OrganizationId"] == null)
        {
            Session["OrganizationId"] = "";
            Session["OrganizationTypeID"] = "";
        }

        if (Request.QueryString["OrganizationTypeID"] != null)
        {
            //btnadduser.Visible = false;
            ClientScript.RegisterStartupScript(this.GetType(), "removeActiveClass", "removeActiveClass()", true);
        }

    }



    protected void gvUserAdmin_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Approve")
            {
                UserInfo user = new UserInfo(Convert.ToInt32(e.CommandArgument));
                if (!user.IsApproved)
                {
                    Emails email = new Emails();
                    email.To = user.Login;
                    email.URL = ConfigurationManager.AppSettings["EmailUrl"].ToString() + "ChangePassword.aspx?userId=" + Encryption.Encrypt(user.UserId.ToString());
                    email.From = "noreply@EPRTS.com";
                    email.Subject = "Registration Approval Email";
                    Thread Email_Thread = new Thread(() => SendEmails(email, Emails.EmailType.ApplicationApprovedEmail.ToString()));
                    Email_Thread.Start();
                }


                UserInfo.ApproveAdminUser(Convert.ToInt32(e.CommandArgument));
                SearchAdminUsers(1);
            }
            else if (e.CommandName == "DeleteUser")
            {

                UserInfo.DeleteAdminUser(Convert.ToInt32(e.CommandArgument));
                SearchAdminUsers(1);
            }
            else if (e.CommandName == "DisApprove")
            {

                UserInfo.DisApproveAdminUser(Convert.ToInt32(e.CommandArgument));
                SearchAdminUsers(1);

            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewUsers.aspx.gvUserAdmin_RowCommand", ex);
        }


    }

    private void SendEmails(Emails email, string type)
    {
        try
        {
            Emails.SendEmail(email, type);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewUsers.aspx SendEmails", ex);
        }

    }

    protected void btnUserSearch_Click(object sender, EventArgs e)
    {
        SearchAdminUsers(1);
    }

    protected void SearchAdminUsers(int pageNo)
    {
        try
        {

            gvUserAdmin.PageSize = pageSize;

            int OrganizationId = UserOrganizationId;

            if (string.IsNullOrEmpty(Request.QueryString["OrganizationId"]) == false)
            {
                OrganizationId = Convert.ToInt32(Request.QueryString["OrganizationId"]);
                Session["OrganizationId"] = Request.QueryString["OrganizationId"];
                Session["OrganizationTypeID"] = Request.QueryString["OrganizationTypeID"];
            }

            string FirstName = txtFirstName.Text.Trim();
            string LastName = txtLastName.Text.Trim();
            string LoginName = txtLogin.Text.Trim();
            DateTime CreatedFromDate = txtCreatedFromDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedFromDate.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);
            DateTime CreatedToDate = txtCreatedToDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedToDate.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);
            int count = 0;
            bool? status = null;
            if (ddlstatus.SelectedItem.Value == "1")
                status = false;
            else if (ddlstatus.SelectedItem.Value == "2")
                status = true;
            DataSet dt = UserInfo.getAdminUsers(pageNo, pageSize, OrganizationId, out count, FirstName, LastName, LoginName, CreatedFromDate.Date, CreatedToDate.Date, LanguageId, LoginMemberId, status);
            if(dt != null && dt.Tables[0].Rows.Count > 0)
                Session["SubTypeId"] = dt.Tables[0].Rows[0]["OrganizationSubTypeID"].ToString();

            gvUserAdmin.DataSource = dt;
            gvUserAdmin.DataBind();

            this.TotalItems = count;
            this.pager.DrawPager(pageNo, TotalItems, pageSize, MaxPagesToShow);

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewUsers.aspx.SearchAdminUsers", ex);
        }
    }

    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        try
        {
            if (this.pager.Equals(source))
            {
                CommandEventArgs cmdArgs = (CommandEventArgs)args;
                CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

                this.SearchAdminUsers(CurrentPage);
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewUsers.aspx. OnBubbleEvent", ex);
        }

        return base.OnBubbleEvent(source, args);
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtLogin.Text = "";
        txtCreatedFromDate.Text = "";
        txtCreatedToDate.Text = "";
        ddlstatus.SelectedIndex = 0;
        SearchAdminUsers(1);
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchAdminUsers(1);
    }
    protected void a1_ServerClick(object sender, EventArgs e)
    {

    }

    protected override void OnLoadComplete(EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "removeActiveClass", "removeActiveClass()", true);
        base.OnLoadComplete(e);
    }

    //protected void gvUserAdmin_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (Request.QueryString["OrganizationTypeID"] != null)
    //    {
    //        e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
    //    }
    //}
}