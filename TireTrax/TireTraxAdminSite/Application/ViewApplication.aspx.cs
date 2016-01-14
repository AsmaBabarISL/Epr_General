using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.Text;

public partial class Application_ViewApplication : BasePage
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
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStewardship','{0}');", ResourceMgr.GetMessage("Applications")), true);
        if (!IsPostBack)
        {
            LoadPendingApplications();
        }
    }

    protected void GridPaging()
    {
        try
        {
            int startRecordNumber = (CurPageNum - 1) * pageSize + 1;
            int endRecordNumber = startRecordNumber + gvApplicationNotApproved.Rows.Count - 1;

            if (gvApplicationNotApproved.Rows.Count == 0)
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
            new SqlLog().InsertSqlLog(0, "adminApplications.GridPaging", ex);
        }
    }

    protected void btnStakeSearch_Click(object sender, EventArgs e)
    {
        LoadPendingApplications();
    }

    private void LoadPendingApplications()
    {
        try
        {
            pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            gvApplicationNotApproved.PageSize = pageSize;
            int Organizationid = Convert.ToInt32(Request.QueryString["OrganizationId"]);
            int OrganizationtypeId = Convert.ToInt32(Request.QueryString["OrganizationTypeId"]);
            string StakeholderName = txtStakeholderName.Text.Trim();
            string DBAName = txtDBAName.Text.Trim();
            string ContactName = txtPrimaryCotnact.Text.Trim();
            string ZIPCode = txtZipCode.Text.Trim();
            DateTime CreatedFromDate = txtCreatedFromDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedFromDate.Text, System.Globalization.CultureInfo.InvariantCulture);
            DateTime CreatedToDate = txtCreatedToDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedToDate.Text, System.Globalization.CultureInfo.InvariantCulture);

            gvApplicationNotApproved.DataSource = OrganizationInfo.SearchStakeholdersByCriteria(CurPageNum, pageSize, out totalRows, Organizationid, OrganizationtypeId, false, StakeholderName, DBAName, ContactName, ZIPCode, CreatedFromDate, CreatedToDate, LanguageId, 1, txtEmail.Text.Trim());
            gvApplicationNotApproved.DataBind();
            GridPaging();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Stakeholder/ViewStakeholder", ex);
        }
    }

    protected void gvApplicationNotApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteStakeholder")
        {
            //UserInfo.DeleteAdminUser(Convert.ToInt32(e.CommandArgument));
            OrganizationInfo.InApprovedStakeholderInActive(Convert.ToInt32(e.CommandArgument));
            LoadPendingApplications();
        }
        else if (e.CommandName == "Approve")
        {
           // UserInfo.ApproveAdminUser(Convert.ToInt32(e.CommandArgument));
            //DateTime dt = DateTime.Now;
            OrganizationInfo.ApprovedStakeholderByAdmin(Convert.ToInt32(e.CommandArgument), currentUserInfo.UserId, DateTime.Now);
            LoadPendingApplications();
        }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPendingApplications();
    }
}