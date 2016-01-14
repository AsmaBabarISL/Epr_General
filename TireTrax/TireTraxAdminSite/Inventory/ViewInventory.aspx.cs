using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TireTraxLib;
using System.Text;

public partial class Inventory_ViewInventory : BasePage
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
    protected void GridPaging()
    {
        try
        {
            int startRecordNumber = (CurPageNum - 1) * pageSize + 1;
            int endRecordNumber = startRecordNumber + gvAdminInventory.Rows.Count - 1;

            if (gvAdminInventory.Rows.Count == 0)
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
            new SqlLog().InsertSqlLog(0, "adminInventory.GridPaging", ex);
        }
    }

    private void Load_AllAdminInventory()
    {
        pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        gvAdminInventory.PageSize = pageSize;

        int status = 1;
        int TireState = Convert.ToInt32(ddlTireState.SelectedValue);

        string tx_barcode = txtInventoryTX_Barcode.Text.Trim();
        string stewardship = txtInventoryStewardship.Text.Trim();
        string PlantCode = txtPlantCode.Text.Trim();
        string SizeCode = txtSizeCode.Text.Trim();

        DataSet ds = Tire.SearchInventory(CurPageNum, pageSize, out totalRows, tx_barcode, stewardship, PlantCode, SizeCode, TireState, status);
        gvAdminInventory.DataSource = ds;
        gvAdminInventory.DataBind();
        GridPaging();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liInventory');", true);
        if (!IsPostBack)
        {
            Load_AllAdminInventory();
        }
    }

    protected void btnInventorySearch_Click(object sender, EventArgs e)
    {
        Load_AllAdminInventory();
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_AllAdminInventory();
    }

    protected void gvAdminInventory_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "timeline")
        {
            Response.Redirect("Inventory-timeline.aspx?serial=" + HttpUtility.UrlEncode(Encryption.Encrypt(e.CommandArgument.ToString())), false);
        }
    }
}