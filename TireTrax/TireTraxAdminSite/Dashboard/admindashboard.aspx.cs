using System;
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

public partial class Dashboard_admindashboard : BasePage
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
    string standardIds = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liHome');", true);
        if (!IsPostBack)
        {
            LoadlatestGlobalStwards();
            LoadLatestStewardship();
            LoadLatsetStakeholder();
            LoadPendingGovernmentAgency();
            LoadPendingLawEnforment();
            LoadPendingProducts();
          
        }
    }

    private void LoadPendingProducts()
    {
        gvPendingProducts.DataSource = Product.GetProductsForApproval();
        gvPendingProducts.DataBind();
    }

    protected void GridPaging()
    {
        try
        {
            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows) / Convert.ToDecimal(pageSize)));
            //lblPagingLeft.Text = "Showing " + CurPageNum + " to " + totalPages + " of " + totalRows + " entries";
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
            //ltrlPaging.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminStakeholders.GridPaging", ex);
        }
    }
    private void LoadlatestGlobalStwards()
    {
        try
        {
            //DataSet ds;
           // ds = OrganizationInfo.getStakeholderTypeAndLangId(132, LanguageId);
            //gvOrganizationAlert.DataSource = ds;
            //gvOrganizationAlert.DataBind();
            DataSet ds;
            standardIds = System.Configuration.ConfigurationManager.AppSettings["GlobalStewardStandardIDs"];
            ds = OrganizationInfo.getStakeholderbyTypeAndLangId(standardIds, LanguageId);
            gvOrganizationAlert.DataSource = ds;
            gvOrganizationAlert.DataBind();



        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminDashboard.LoadOrganizationAlerts", ex);
        }

    }
    private void LoadLatestStewardship()
    {
        try
        {
            DataSet ds;
            standardIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
            ds = OrganizationInfo.getInfoByOrganizationTypeAndLangId(standardIds, LanguageId);
            gvLatestSteward.DataSource = ds;
            gvLatestSteward.DataBind();

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminDashboard.LoadLatestStewardship", ex);
        }
    }
    

    private void LoadPendingLawEnforment()
    {
        try
        {
            //DataSet ds;
            //ds = OrganizationInfo.getStakeholderTypeAndLangId(24, LanguageId);
            //gvLawEnforcement.DataSource = ds;
            //gvLawEnforcement.DataBind();
            DataSet ds;
            standardIds = System.Configuration.ConfigurationManager.AppSettings["LawEnforcementAgencyStandardIDs"];
            ds = OrganizationInfo.getStakeholderbyTypeAndLangId(standardIds, LanguageId);
            gvLawEnforcement.DataSource = ds;
            gvLawEnforcement.DataBind();


        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminDashboard.LoadPendingLawEnforment", ex);
        }
    }
    private void LoadPendingGovernmentAgency()
    {
        try
        {
            //DataSet ds;
            //ds = OrganizationInfo.getStakeholderTypeAndLangId(23, LanguageId);
            //gvGovernmentAgency.DataSource = ds;
            //gvGovernmentAgency.DataBind();
            DataSet ds;
            standardIds = System.Configuration.ConfigurationManager.AppSettings["GovernmentAgencyStandardIDs"];
            ds = OrganizationInfo.getStakeholderbyTypeAndLangId(standardIds, LanguageId);
            gvGovernmentAgency.DataSource = ds;
            gvGovernmentAgency.DataBind();


        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminDashboard.LoadGovernmentAgency", ex);
        }
    }

    private void LoadLatsetStakeholder()
    {
        try
        {
            //DataSet ds;
            //ds = OrganizationInfo.getStakeholderTypeAndLangId(22, LanguageId);
            //gvLatestStakeholder.DataSource = ds;
            //gvLatestStakeholder.DataBind();
            DataSet ds;
            standardIds = System.Configuration.ConfigurationManager.AppSettings["StakeholderStandardIDs"];
            ds = OrganizationInfo.getStakeholderbyTypeAndLangId(standardIds, LanguageId);
            gvLatestStakeholder.DataSource = ds;
            gvLatestStakeholder.DataBind();


        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminDashboard.LoadLatestStakeholder", ex);
        }


    }

    protected void GridPagingForStewardship()
    {
        try
        {
            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows) / Convert.ToDecimal(pageSize)));

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

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminStakeholders.GridPaging", ex);
        }
    }

    protected void gvOrganizationAlert_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve")
        {
            OrganizationInfo o = new OrganizationInfo(Convert.ToInt32(e.CommandArgument));
            standardIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
            OrganizationInfo.SetStatus(OrganizationStatus.Accepted, o.OrganizationId, "Approved from dashboard", o.OrganizationTypeId, standardIds);
            DataSet ds = UserInfo.getDefaultUsers(o.OrganizationId);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                UserInfo user = new UserInfo(Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"].ToString()));
                if (!user.IsApproved)
                {
                    Emails email = new Emails();
                    email.To = user.Login;
                    email.URL = ConfigurationManager.AppSettings["EmailUrl"].ToString() + "ChangePassword.aspx?userId=" + Encryption.Encrypt(user.UserId.ToString());
                    email.From = "noreply@EPRTS.com";
                    email.Subject = "Registration Approval Email";
                    //Thread Email_Thread = new Thread(() => SendEmails(email, Emails.EmailType.ApplicationApprovedEmail.ToString()));
                    SendEmails(email, Emails.EmailType.ApplicationApprovedEmail.ToString());
                    //Email_Thread.Start();
                }
            }
            Response.Redirect("/Dashboard/admindashboard.aspx");
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
    protected void gvLatestStakeholder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve")
        {

            OrganizationInfo o = new OrganizationInfo(Convert.ToInt32(e.CommandArgument));
            standardIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
            OrganizationInfo.SetStatus(OrganizationStatus.Accepted, o.OrganizationId, "Approved from dashboard", o.OrganizationTypeId, standardIds);
            DataSet ds = UserInfo.getDefaultUsers(o.OrganizationId);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                UserInfo user = new UserInfo(Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"].ToString()));
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
            }
            Response.Redirect("/Dashboard/admindashboard.aspx");
        }
    }

    protected void gvGovernmentAgency_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve")
        {

            OrganizationInfo o = new OrganizationInfo(Convert.ToInt32(e.CommandArgument));
            standardIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
            OrganizationInfo.SetStatus(OrganizationStatus.Accepted, o.OrganizationId, "Approved from dashboard", o.OrganizationTypeId, standardIds);
            DataSet ds = UserInfo.getDefaultUsers(o.OrganizationId);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                UserInfo user = new UserInfo(Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"].ToString()));
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
            }
            Response.Redirect("/Dashboard/admindashboard.aspx");
        }

    }

    protected void gvLawEnforcement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve")
        {

            OrganizationInfo o = new OrganizationInfo(Convert.ToInt32(e.CommandArgument));
            standardIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
            OrganizationInfo.SetStatus(OrganizationStatus.Accepted, o.OrganizationId, "Approved from dashboard", o.OrganizationTypeId, standardIds);
            DataSet ds = UserInfo.getDefaultUsers(o.OrganizationId);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                UserInfo user = new UserInfo(Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"].ToString()));
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
            }
            Response.Redirect("/Dashboard/admindashboard.aspx");
        }
    }

    protected void gvLatestSteward_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve")
        {

            OrganizationInfo o = new OrganizationInfo(Convert.ToInt32(e.CommandArgument));
            standardIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
            OrganizationInfo.SetStatus(OrganizationStatus.Accepted, o.OrganizationId, "Approved from dashboard", o.OrganizationTypeId, standardIds);
            DataSet ds = UserInfo.getDefaultUsers(o.OrganizationId);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                UserInfo user = new UserInfo(Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"].ToString()));
                if (!user.IsApproved)
                {
                    Emails email = new Emails();
                    email.To = user.Login;
                    email.URL = ConfigurationManager.AppSettings["EmailUrl"].ToString() + "ChangePassword.aspx?userId=" + Encryption.Encrypt(user.UserId.ToString());
                    email.From = "noreply@EPRTS.com";
                    email.Subject = "Registration Approval Email";
                    //If thread doesnt works then uncomment the commented area below and comment 2 lines of threading.
                    Thread Email_Thread = new Thread(() => SendEmails(email, Emails.EmailType.ApplicationApprovedEmail.ToString()));
                    Email_Thread.Start();
                    //SendEmails(email, Emails.EmailType.ApplicationApprovedEmail.ToString());
                }
            }
            Response.Redirect("/Dashboard/admindashboard.aspx");//,false);
            //Context.ApplicationInstance.CompleteRequest();
        }

    }
    protected void gvPendingProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve")
        {
            int CategoryId = Convert.ToInt32(e.CommandArgument);
            ApproveProducts(CategoryId);
            LoadPendingProducts();
        }
    }

    private void ApproveProducts(int CategoryId)
    {
        Product.ApproveProductCategory(CategoryId);
    }
}