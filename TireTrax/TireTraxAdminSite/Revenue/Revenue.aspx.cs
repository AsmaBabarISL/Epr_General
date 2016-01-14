using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.IO;
using System.Data;
using System.Text;

public partial class Revenue_Revenue : BasePage
{
    //public int CurPageNum
    //{
    //    get
    //    {
    //        if (Request.QueryString["p"] != null)
    //            return Conversion.ParseInt(Request.QueryString["p"]);
    //        else
    //            return 1;
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorOrganization.Visible = false;
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liRevenue','{0}');", ResourceMgr.GetMessage("Revenue")), true);
        GetPermission(ResourceType.Revenue, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        if (User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("/");
        }
        if (!IsPostBack)
        {
            LoadRevenue();
            lblFromDateError.Visible = false;
            lblToDateError.Visible = false;
        }
        else
        {
            pageSize = 10;
            if (TotalItemsR > 0)
            {
                pgrRevenueDetails.DrawPager(CurrentPageR, this.TotalItemsR, pageSize, MaxPagesToShow);
            }
        }

    }

    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pgrRevenueDetails.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPageR = Convert.ToInt32(cmdArgs.CommandArgument);

            this.GetRevenueInfo(Convert.ToInt32(hdnOrganizationID.Value), CurrentPageR);
        }
        return base.OnBubbleEvent(source, args);
    }


    private void LoadRevenue()
    {
        Utils.GetLookUpData<DropDownList>(ref ddlState, LookUps.GetStates, CountryIDByLanguageId);
        DataSet ds = RevenuInventory.GetRevenueAdmin(0, 0, DateTime.MinValue, DateTime.MinValue,CountryIDByLanguageId);
        gvRevenue.DataSource = ds;
        gvRevenue.DataBind();
    }

    protected void btnRevenueSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblErrorOrganization.Visible = false;
            lblFromDateError.Visible = false;
            lblToDateError.Visible = false;
            string fromDate = txtFromDate.Text.ToString();
            string toDate = txtToDate.Text.ToString();
            
            //if (ddlOrganization.Enabled && ddlOrganization.SelectedIndex == 0)
            //{
            //    lblErrorOrganization.Text = "Please select organization";
            //    lblErrorOrganization.Visible = true;
            //    return;
            //}
            if (string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                lblFromDateError.Text = "Please provide start date";
                lblFromDateError.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                lblToDateError.Text = "Please provide end date";
                lblToDateError.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                hdnStartDate.Value = fromDate;
                hdnEndDate.Value = toDate;
                if (Convert.ToDateTime(fromDate) > Convert.ToDateTime(toDate))
                {
                    lblToDateError.Text = "Start date must be anterior to end date";
                    lblToDateError.Visible = true;
                    return;
                }
            }
            DataSet ds = null;
            if (ddlState.SelectedIndex == 0)
                ds = RevenuInventory.GetRevenueAdmin(0, 0, txtFromDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(fromDate), txtToDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(toDate),CountryIDByLanguageId);
            else if(ddlState.SelectedIndex != 0 && ddlOrganization.SelectedIndex == 0)
                ds = RevenuInventory.GetRevenueAdmin(Convert.ToInt32(ddlState.SelectedValue), 0, txtFromDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(fromDate), txtToDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(toDate), CountryIDByLanguageId);
            else
                ds = RevenuInventory.GetRevenueAdmin(Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlOrganization.SelectedValue), txtFromDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(fromDate), txtToDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(toDate),CountryIDByLanguageId);
            gvRevenue.DataSource = ds;
            gvRevenue.DataBind();


        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewRevenue btnRevenueSearch_Click", ex);
        }

    }


    protected void btnRevenueCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Revenue");
        try
        {
            ddlState.SelectedIndex = 0;
            if (ddlOrganization.Enabled)
            {
                ddlOrganization.SelectedIndex = 0;
                ddlOrganization.Enabled = false;
            }
            lblFromDateError.Visible = false;
            lblToDateError.Visible = false;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            gvRevenue.DataSource = RevenuInventory.GetRevenueAdmin(0, 0, DateTime.MinValue, DateTime.MinValue,CountryIDByLanguageId);
            gvRevenue.DataBind();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewRevenue btnRevenueCancel_Click", ex);
        }
    }

    protected void gvRevenue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RevenueInfo")
        {
            int OrgID = Convert.ToInt32(e.CommandArgument);
            hdnOrganizationID.Value = OrgID.ToString();
            GetRevenueInfo(OrgID, 1);
        }
    }

    private void GetRevenueInfo(int OrgID, int pageNo)
    {
        // string PeriodYear = (string)e.CommandArgument;
        //int count = 0;
        pageSize = 10;
        DataSet ds = null;
        ds = RevenuInventory.GetRevenueDetailsAdmin(pageNo, pageSize, out totalRows, OrgID, hdnStartDate.Value == string.Empty ? DateTime.MinValue : Convert.ToDateTime(hdnStartDate.Value), hdnEndDate.Value == string.Empty ? DateTime.MinValue : Convert.ToDateTime(hdnEndDate.Value));
        TotalItemsR = totalRows;
        dvRevenueDetail.Visible = true;
        gvViewDetail.DataSource = ds;
        gvViewDetail.DataBind();
        pgrRevenueDetails.DrawPager(pageNo, this.TotalItemsR, pageSize, MaxPagesToShow);
        if (gvRevenue.Rows.Count != 0)
        {
            LinkButton lnkBtn = (LinkButton)pgrRevenueDetails.FindControl("Button_" + pageNo.ToString());
            if (lnkBtn != null)
            {
                lnkBtn.Font.Bold = true;
            }

        }
    }
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        dvRevenueDetail.Visible = false;
        //Response.Redirect("~/Revenue/Revenue.aspx?p=1");
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex == 0)
        {
            ddlOrganization.Enabled = false;
            return;
        }
        else
        {
            ddlOrganization.Enabled = true;
            Utils.GetLookUpData<DropDownList>(ref ddlOrganization, LookUps.AllOrganizationsOfState, Convert.ToInt32(ddlState.SelectedValue));
        }
    }
    //protected void GridPaging()
    //{
    //    try
    //    {
    //        int startRecordNumber = (CurPageNum - 1) * pageSize + 1;
    //        int endRecordNumber = startRecordNumber + gvViewDetail.Rows.Count - 1;

    //        if (gvViewDetail.Rows.Count == 0)
    //            startRecordNumber = 0;

    //        int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows) / Convert.ToDecimal(pageSize)));
    //        lblPagingLeft.Text = "Showing " + startRecordNumber + " to " + endRecordNumber + " of " + totalRows;
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append(@"<div class='Pages'><div class='Paginator'>");
    //        Pagination pagingstring = new Pagination();
    //        pagingstring.CurPage = CurPageNum;
    //        pagingstring.BaseUrl = Request.Url.GetLeftPart(UriPartial.Path).ToString();
    //        pagingstring.TotalRows = totalRows;
    //        pagingstring.PerPage = pageSize;
    //        pagingstring.PrevLink = "&lt; Prev";
    //        pagingstring.NextLink = "Next &gt;";
    //        pagingstring.LastLink = "Last &gt;";
    //        pagingstring.FirstLink = "&lt; First";
    //        sb.Append(pagingstring.GetPageLinks());
    //        sb.Append(@"</div></div>");
    //        ltrlPaging.Text = sb.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        new SqlLog().InsertSqlLog(0, "adminRevenue.GridPaging", ex);
    //    }
    //}
}