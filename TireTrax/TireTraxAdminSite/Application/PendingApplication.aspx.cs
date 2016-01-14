using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Text;
using System.Data;
public partial class Application_PendingApplication : BasePage
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
      
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liApplication','{0}');", ResourceMgr.GetMessage("Applications")), true);
        if (!IsPostBack)
        {
            LoadLatestStewardship();
        }
    }

    protected void GridPaging()
    {
        try
        {
            int startRecordNumber = (CurPageNum - 1) * pageSize + 1;
            int endRecordNumber = startRecordNumber + gvLatestSteward.Rows.Count - 1;

            if (gvLatestSteward.Rows.Count == 0)
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
        LoadLatestStewardship();
    }
    //protected void gvLatestSteward_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        int statusid = Convert.ToInt32(((HiddenField)e.Row.FindControl("hfStatus")).Value);
    //        DropDownList ddl = e.Row.FindControl("ddlStatus") as DropDownList;
    //        ddl.SelectedValue = statusid.ToString();
    //        if (ddl != null)
    //        {
    //            ddl.SelectedIndexChanged += new EventHandler(ddlStatus_SelectedIndexChanged);
    //        }
    //    }
    //}
    protected void gvLatestSteward_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Approve")
        //{

        //    OrganizationInfo.PendingStewardshipChange(Convert.ToInt32(e.CommandArgument));

        //    Response.Redirect("/Application/PendingApplication.aspx");
        //}

    }

    private void LoadLatestStewardship()
    {
        try
        {
            pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            gvLatestSteward.PageSize = pageSize;
            string StakeholderName = txtStakeholderName.Text.Trim();
            string DBAName = txtDBAName.Text.Trim();
            string ContactName = txtPrimaryCotnact.Text.Trim();
           
            DateTime CreatedFromDate = txtCreatedFromDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedFromDate.Text, System.Globalization.CultureInfo.InvariantCulture);
            DateTime CreatedToDate = txtCreatedToDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedToDate.Text, System.Globalization.CultureInfo.InvariantCulture);
            DataSet ds;
            ds = OrganizationInfo.getPendingApplicationByOrgIdAndLangId(CurPageNum, pageSize, out totalRows, 20, LanguageId, StakeholderName, DBAName, ContactName,CreatedFromDate, CreatedToDate);
            gvLatestSteward.DataSource = ds;
            gvLatestSteward.DataBind();

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminDashboard.LoadLatestStewardship", ex);
        }
    }

   // protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
   // {
   //     //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "Confirm", true);
   //     //string confirmValue = Request.Form["confirm_value"];
   //     //if (confirmValue == "Yes")
   //     //{
   //     //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
   //     //}
   //     DropDownList DropDownList1 = (DropDownList)sender;
   //     int statusId = Convert.ToInt32(DropDownList1.SelectedItem.Value);
   //     GridViewRow grdrDropDownRow = ((GridViewRow)DropDownList1.Parent.Parent);
   //  int orgId= Convert.ToInt32(((HiddenField) grdrDropDownRow.FindControl("hfOrganizationId")).Value);
   //  OrganizationStatus s = (OrganizationStatus)statusId;
   ////  OrganizationInfo.SetStatus(s, orgId);
   //  LoadLatestStewardship();

   // }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLatestStewardship();
    }
}