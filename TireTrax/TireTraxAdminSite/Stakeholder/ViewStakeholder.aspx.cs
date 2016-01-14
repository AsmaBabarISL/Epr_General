using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Text;
using System.Data;

public partial class Stakeholder_ViewStakeholder : BasePage
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
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStewardship','{0}');", ResourceMgr.GetMessage("Stakeholders")), true);
        if (!IsPostBack)
        {
            LoadStakeholders();
        }
    }

    protected void GridPaging()
    {
        try
        {
            int startRecordNumber = (CurPageNum - 1) * pageSize + 1;
            int endRecordNumber = startRecordNumber + gvApplicationApproved.Rows.Count - 1;

            if (gvApplicationApproved.Rows.Count == 0)
                startRecordNumber = 0;

            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows) / Convert.ToDecimal(pageSize)));
            lblApprovalStakeholder.Text = "Showing " + startRecordNumber + " to " + endRecordNumber + " of " + totalRows;
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
            ltrApprovalStakeholder.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminStakeholders.GridPaging", ex);
        }
    }

    protected void btnStakeSearch_Click(object sender, EventArgs e)
    {
        LoadStakeholders();
    }
    private void LoadStakeholders()
    {
        try
        {
            pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            gvApplicationApproved.PageSize = pageSize;

            int OrganizationId = Convert.ToInt32(Request.QueryString["OrganizationId"]);
            int OrganizationTypeId = Convert.ToInt32(Request.QueryString["OrganizationTypeId"]);
            string StakeholderName = txtStakeholderName.Text.Trim();
            string DBAName = txtDBAName.Text.Trim();
            string ContactName = txtPrimaryCotnact.Text.Trim();
            string ZIPCode = txtZipCode.Text.Trim();

            DateTime CreatedFromDate = txtCreatedFromDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedFromDate.Text, System.Globalization.CultureInfo.InvariantCulture);
            DateTime CreatedToDate = txtCreatedToDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedToDate.Text, System.Globalization.CultureInfo.InvariantCulture);

            DataSet ds;
            ds = OrganizationInfo.SearchStakeholdersByStewardShip(CurPageNum, pageSize, out totalRows, OrganizationId, OrganizationTypeId, true, StakeholderName, DBAName, ContactName, ZIPCode, CreatedFromDate, CreatedToDate, LanguageId);
            gvApplicationApproved.DataSource = ds;
            gvApplicationApproved.DataBind();
            GridPaging();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Stakeholder/ViewStakeholder", ex);
        }
    }

    protected void gvApplicationApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteStakeholder")
        {
            OrganizationInfo.ApprovedStakeholderInActive(Convert.ToInt32(e.CommandArgument));
            LoadStakeholders();

        }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStakeholders();
    }
}