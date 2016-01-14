﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Text;
using System.Data;
using System.Configuration;
using System.Threading;

public partial class User_ViewUser : BasePage
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
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStewardship','{0}');", ResourceMgr.GetMessage("Users")), true);
        if (!IsPostBack)
        {
            SearchAdminUsers();
        }
    }

    protected void GridPaging()
    {
        try
        {
            int startRecordNumber = (CurPageNum - 1) * pageSize + 1;
            int endRecordNumber = startRecordNumber + gvUserAdmin.Rows.Count - 1;

            if (gvUserAdmin.Rows.Count == 0)
                startRecordNumber = 0;

            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows) / Convert.ToDecimal(pageSize)));
            lblPagingLeft.Text = "Showing " + startRecordNumber + " to " + endRecordNumber + " of " + totalRows;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div class='Pages'><div class='Paginator'>");
            Pagination pagingstring = new Pagination();
            pagingstring.CurPage = CurPageNum;
            pagingstring.BaseUrl = Request.Url.GetLeftPart(UriPartial.Path).ToString();
            pagingstring.TotalRows = totalRows;
            pagingstring.PerPage = pageSize;
            pagingstring.PrevLink = "&lt; Prev";
            pagingstring.NextLink = "Next &gt;";
            pagingstring.LastLink = "Last &gt;";
            pagingstring.FirstLink = "&lt; First";
            sb.Append(pagingstring.GetPageLinks());
            sb.Append(@"</div></div><br clear='all' />");
            ltrlPaging.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminStakeholders.GridPaging", ex);
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
    protected void gvUserAdmin_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve")
        {
           
            UserInfo.ApproveAdminUser(Convert.ToInt32(e.CommandArgument));
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
            SearchAdminUsers();
        }
        else if (e.CommandName == "DeleteUser")
        {

            UserInfo.DeleteAdminUser(Convert.ToInt32(e.CommandArgument));
            SearchAdminUsers();
        }
        else if (e.CommandName == "DisApprove")
        {

            UserInfo.DisApproveAdminUser(Convert.ToInt32(e.CommandArgument));
            SearchAdminUsers();
        }
        else if (e.CommandName == "UserInfo")
        {
            //   ScriptManager.RegisterStartupScript(this, GetType(), "DataInfo", "ShowUserInfo();", true);
            ViewUserInfo(Convert.ToInt32(e.CommandArgument));
            //SearchAdminUsers();
            ScriptManager.RegisterStartupScript(this, GetType(), "DataInfo", "ShowUserInfo();", true);

        }
    }

    protected void btnUserSearch_Click(object sender, EventArgs e)
    {
        SearchAdminUsers();
    }

    protected void SearchAdminUsers()
    {
        try
        {
            if (Request.QueryString["OrganizationId"] == null || Request.QueryString["OrganizationTypeId"] == null)
            {
                Response.Redirect("/Stewardship/ViewStewardship.aspx");
            }
            else
            {

                pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
                gvUserAdmin.PageSize = pageSize;

                int OrganizationId = Convert.ToInt32(Request.QueryString["OrganizationId"]);

                string FirstName = txtFirstName.Text.Trim();
                string LastName = txtLastName.Text.Trim();
                string LoginName = txtLogin.Text.Trim();
                DateTime CreatedFromDate = txtCreatedFromDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedFromDate.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                DateTime CreatedToDate = txtCreatedToDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedToDate.Text.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                bool isactive = false;
                if (ddlActive.SelectedItem.Value == "0")
                {
                    isactive = true;
                }
                gvUserAdmin.DataSource = UserInfo.getUsers(CurPageNum, pageSize, OrganizationId, out totalRows, FirstName, LastName, LoginName, CreatedFromDate.Date, CreatedToDate.Date, LanguageId, isactive);
                gvUserAdmin.DataBind();
                GridPaging();
            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AdminUsers.LoadAdminUsers", ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtLogin.Text = "";
        txtCreatedFromDate.Text = "";
        txtCreatedToDate.Text = "";
        SearchAdminUsers();
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        pageSize = Conversion.ParseInt(ddlPageSize.SelectedValue);
        SearchAdminUsers();
    }


    private void ViewUserInfo(int UserId)
    {
        DataSet ds = null;

        ds = UserInfo.ViewUserInfoById(UserId);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            //ltrBusinessName.Text = Convert.ToString(ds.Tables[0].Rows[0]["LegalName"]);
            //ltrDBAName.Text = Convert.ToString(ds.Tables[0].Rows[0]["DBAName"]);
            lblLogin.Text = Convert.ToString(ds.Tables[0].Rows[0]["Login"]);
            ltrFirstName.Text = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);
            ltrMiddleName.Text = Convert.ToString(ds.Tables[0].Rows[0]["MiddleName"]);
            ltrLastName.Text = Convert.ToString(ds.Tables[0].Rows[0]["LastName"]);
            ltrprimaryEmail.Text = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);

            ltrLanguage.Text = Convert.ToString(ds.Tables[0].Rows[0]["Language"]);
            ltrPhoneNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["PhoneNumber"]);
            ltrRoleType.Text = Convert.ToString(ds.Tables[0].Rows[0]["LookupTypeName"]);
            ltrWebsite.Text = Convert.ToString(ds.Tables[0].Rows[0]["Website"]);
            ltrOrganizationName.Text = Convert.ToString(ds.Tables[0].Rows[0]["LegalName"]);
            ltrFranchiseName.Text = Convert.ToString(ds.Tables[0].Rows[0]["FranchiseName"]);
            ltrRoleName.Text = Convert.ToString(ds.Tables[0].Rows[0]["RoleName"]);
            //ltrOrganization.Text = Convert.ToString(ds.Tables[0].Rows[0]["Description"]);
            //ltrContactTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactTitleName"]);

            ////ltrBusinessType.Text = Convert.ToString(ds.Tables[0].Rows[0]["BusinessType"]);
            ////ltrwebsite.Text = Convert.ToString(ds.Tables[0].Rows[0]["Website"]);
            ////ltrBusinessAddress1.Text = Convert.ToString(ds.Tables[0].Rows[0]["BusinessAddress1"]);
            ////ltrBusinessAddress2.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address2"]);
            ////ltrBusinessPhoneType.Text = Convert.ToString(ds.Tables[0].Rows[0]["BusinessPhoneType"]);
            ////lrtBusinessTextMessage.Text = Convert.ToString(ds.Tables[0].Rows[0]["BusinessIsAccepTextMessages"]);


            //ltrAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
            //ltrBillingContact.Text = Convert.ToString(ds.Tables[0].Rows[0]["BilingContact"]);
            //ltrFax.Text = Convert.ToString(ds.Tables[0].Rows[0]["Fax"]);
            //ltrCountryAbbreviation.Text = Convert.ToString(ds.Tables[0].Rows[0]["Abbreviation"]);
            //ltrCellTextMessage.Text = Convert.ToString(ds.Tables[0].Rows[0]["CellIsAcceptTextMessages"]);
            //ltrCellPhoneType.Text = Convert.ToString(ds.Tables[0].Rows[0]["CellPhoneType"]);
            //ltrBillingMailAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["BillMailAddress"]);


        }
    }
}