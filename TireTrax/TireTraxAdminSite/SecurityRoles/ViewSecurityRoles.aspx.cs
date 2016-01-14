using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Text;


public partial class SecurityRoles_ViewSecurityRoles : BasePage
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

        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liRoles');", true);
        LoadGrid();

    }

    private void LoadGrid()
    {
        try
        {
            pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            gvStewardship.PageSize = pageSize;
            gvStewardship.DataSource = LookupsManagement.GetLookupsData(11, 0).Tables[0];
            gvStewardship.DataBind();

            GridPaging();

            //Utils.GetLookUpData<DropDownList>(ref ddlOrganizationType, LookUps.OrganizationType);
            //Utils.GetLookUpData<DropDownList>(ref ddlOrganizationTypeForUser, LookUps.OrganizationType);

            //if (ddlOrganizationType.Items.FindByValue("20") != null)
            //{
            //    ddlOrganizationType.Items.Remove(ddlOrganizationType.Items.FindByValue("20"));
            //}

            //if (ddlOrganizationTypeForUser.Items.FindByValue("20") != null)
            //{
            //    ddlOrganizationTypeForUser.Items.Remove(ddlOrganizationTypeForUser.Items.FindByValue("20"));
            //}


        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminStewardship.LoadGrid", ex);
        }
    }

    protected void GridPaging()
    {
        try
        {
            int startRecordNumber = (CurPageNum - 1) * pageSize + 1;
            int endRecordNumber = startRecordNumber + gvStewardship.Rows.Count - 1;

            if (gvStewardship.Rows.Count == 0)
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
            new SqlLog().InsertSqlLog(0, "adminStewardship.Loading", ex);
        }
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
}