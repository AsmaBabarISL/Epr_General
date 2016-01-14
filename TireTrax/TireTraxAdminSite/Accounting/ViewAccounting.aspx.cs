using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Text;
using System.Data;

public partial class Accounting_ViewAccounting : BasePage
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
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liAccounting');", true);
        if (!IsPostBack)
        {
            LoadAccounting();
        }

    }

    protected void GridPaging()
    {
        try
        {
            int startRecordNumber = (CurPageNum - 1) * pageSize + 1;
            int endRecordNumber = startRecordNumber + gvAccounting.Rows.Count - 1;

            if (gvAccounting.Rows.Count == 0)
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
            new SqlLog().InsertSqlLog(0, "acounting.GridPaging", ex);
        }
    }


    protected void LoadAccounting()
    {
        pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        gvAccounting.PageSize = pageSize;

        String organizationame = txtLegalName.Text.Trim();
        String username = txtUserName.Text.Trim();
        String location = txtLocation.Text.Trim();

        DataSet ds;
        ds = OrganizationInfo.getTransactionInfo(pageId, pageSize, out totalRows, organizationame, username, location);
        gvAccounting.DataSource = ds;
        gvAccounting.DataBind();
        GridPaging();

    }



    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccounting();
    }
    protected void btnAccountingSearch_Click(object sender, EventArgs e)
    {
        LoadAccounting();
    }
}