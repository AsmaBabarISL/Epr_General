using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class Revenue_ViewRevenue : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
            //pnlDateoptions.Enabled = false;
            //pnlPeriodoptions.Enabled = false;
            lblFromDateError.Visible = false;
            lblToDateError.Visible = false;
            lblReportTypeError.Visible = false;
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myFunction", "HideBoth();", true);
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

            this.GetRevenueInfo(hdnPeriodYear.Value, CurrentPageR);
        }
        return base.OnBubbleEvent(source, args);
    }


    private void LoadRevenue()
    {
        int fromYear = 2012;
        for (int year = fromYear; year <= DateTime.Now.Year; year++)
        {
            ListItem li = new ListItem();
            li.Text = year.ToString();
            li.Value = year.ToString();
            ddlYearly.Items.Add(li);
            chkboxYears.Items.Add(li);
        }
        Uncheck();
        //rdo1.Checked = true;
        //pnlDateoptions.Enabled = true;
        gvRevenue.DataSource = RevenuInventory.GetRevenue(UserOrganizationId, "Yearly", DateTime.Now.Year.ToString(), DateTime.MinValue, DateTime.MinValue);
        gvRevenue.DataBind();
    }

    protected void btnRevenueSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdo1.Checked)
            {
                lblFromDateError.Visible = false;
                lblToDateError.Visible = false;
                string fromDate = txtFromDate.Text.ToString();
                string toDate = txtToDate.Text.ToString();
                if (string.IsNullOrEmpty(fromDate))
                {
                    //fromDate = "01/01/1753";
                    fromDate = SqlDateTime.MinValue.ToString();
                }
                if (string.IsNullOrEmpty(toDate))
                {
                    toDate = DateTime.Now.ToShortDateString();
                }
                if (Convert.ToDateTime(fromDate) > Convert.ToDateTime(toDate))
                {
                    lblToDateError.Text = "Start date must be anterior to end date";
                    lblToDateError.Visible = true;
                    return;
                }
                string RevType = "Datewise";
                DataSet ds = RevenuInventory.GetRevenue(UserOrganizationId, RevType, string.Empty, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                gvRevenue.DataSource = ds;
                gvRevenue.DataBind();
            }
            else if (rdo2.Checked)
            {
                lblReportTypeError.Visible = false;
                string reportType = ddlReportType.SelectedValue.ToString();
                string year;
                string catIDs = "";
                if (reportType == "-1")
                {
                    lblReportTypeError.Text = "Please select a report type";
                    lblReportTypeError.Visible = true;
                    return;
                }
                else if (reportType == "Monthly" || reportType == "Quarterly" || reportType == "Biannual")
                {
                    year = ddlYearly.SelectedValue.ToString();
                    DataSet ds = RevenuInventory.GetRevenue(UserOrganizationId, reportType, year, DateTime.MinValue, DateTime.MinValue);
                    gvRevenue.DataSource = ds;
                    gvRevenue.DataBind();
                }
                else if (reportType == "Yearly")
                {
                    foreach (ListItem item in chkboxYears.Items)
                    {
                        if (item.Selected)
                        {
                            catIDs += item.Value + ",";
                        }
                    }
                    catIDs = catIDs.TrimEnd(',');
                    if (string.IsNullOrEmpty(catIDs))
                    {
                        lblReportTypeError.Text = "Please select atleast 1 year";
                        lblReportTypeError.Visible = true;
                        return;
                    }
                    DataSet ds = RevenuInventory.GetRevenue(UserOrganizationId, reportType, catIDs, DateTime.MinValue, DateTime.MinValue);
                    gvRevenue.DataSource = ds;
                    gvRevenue.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewRevenue btnRevenueSearch_Click", ex);
        }

    }

    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblReportTypeError.Visible = false;
        string reportType = ddlReportType.SelectedValue.ToString();
        if (reportType == "-1")
        {
            divYear.Visible = false;
            divChkboxesYear.Visible = false;
        }
        else if (reportType == "Monthly" || reportType == "Quarterly" || reportType == "Biannual")
        {
            divYear.Visible = true;
            divChkboxesYear.Visible = false;
        }
        else if (reportType == "Yearly")
        {
            divYear.Visible = false;
            divChkboxesYear.Visible = true;
        }
    }
    protected void btnRevenueCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Revenue");
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myFunction", "HideBoth();", true);
        rdo1.Checked = false;
        rdo2.Checked = false;
        //pnlDateoptions.Enabled = false;
        //pnlPeriodoptions.Enabled = false;
        gvRevenue.DataSource = RevenuInventory.GetRevenue(UserOrganizationId, "Yearly", DateTime.Now.Year.ToString(), DateTime.MinValue, DateTime.MinValue);
        gvRevenue.DataBind();
        Reset();
    }
    protected void rdo2_CheckedChanged(object sender, EventArgs e)
    {
        Reset();
        pnlDateoptions.Enabled = false;
        pnlPeriodoptions.Enabled = true;
    }
    protected void rdo1_CheckedChanged(object sender, EventArgs e)
    {
        Reset();
        pnlDateoptions.Enabled = true;
        pnlPeriodoptions.Enabled = false;
    }
    private void Reset()
    {
        try
        {
            lblFromDateError.Visible = false;
            lblToDateError.Visible = false;
            lblReportTypeError.Visible = false;
            divChkboxesYear.Visible = false;
            divYear.Visible = false;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            ddlReportType.SelectedIndex = 0;
            ddlYearly.SelectedIndex = 0;
            //gvRevenue.DataSource = null;
            //gvRevenue.DataBind();
            Uncheck();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewRevenue Reset", ex);
        }
    }
    protected void gvRevenue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RevenueInfo")
        {
            string PeriodYear = (string)e.CommandArgument;
            hdnPeriodYear.Value = PeriodYear;
            GetRevenueInfo(PeriodYear, 1);
        }
    }

    private void GetRevenueInfo(string PeriodYear, int pageNo)
    {
        // string PeriodYear = (string)e.CommandArgument;
        int count = 0;
        pageSize = 10;
        string Period = PeriodYear.Split(',')[0];
        string Year = PeriodYear.Split(',')[1];
        DataSet ds = null;
        if (!Period.Contains('-'))
            ds = RevenuInventory.GetRevenueDetails(pageNo, pageSize, out count, UserOrganizationId, Period, Year, string.Empty);
        else
            ds = RevenuInventory.GetRevenueDetails(pageNo, pageSize, out count, UserOrganizationId, Period, Year, Period);
        //if (rdo1.Checked)
        //    ds = RevenuInventory.GetRevenueDetails(pageNo, pageSize,out count, UserOrganizationId, Period, Year, Period);
        //else
        //    ds = RevenuInventory.GetRevenueDetails(pageNo, pageSize,out count, UserOrganizationId, Period, Year, string.Empty);
        TotalItemsR = count;
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
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myFunction1", "CheckBoxes();", true);
    }

    private void Uncheck()
    {
        foreach (ListItem li in chkboxYears.Items)
        {
            li.Selected = false;
        }
    }
    protected void gvRevenue_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortExpression == "")
        {
            
        }
    }
}