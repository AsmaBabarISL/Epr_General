﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
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
        GetPermission(ResourceType.Stakeholders, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }

        if (User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("/");
        }
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liStakeholders','{0}');", ResourceMgr.GetMessage("Stakeholders")), true);
        if (!IsPostBack)
        {
            LoadStakeholders(1);
        }
        if (TotalItemsR > 0)
        {

            pager.DrawPager(CurrentPage, TotalItemsR, pageSize, MaxPagesToShow);
        }
    }



    protected void btnStakeSearch_Click(object sender, EventArgs e)
    {
        LoadStakeholders(1);
    }
    private void LoadStakeholders(int pageNo)
    {


        gvApplicationApproved.PageSize = pageSize;

        int OrganizationId = UserOrganizationId;
        int OrganizationTypeId = 0;
        string StakeholderName = txtStakeholderName.Text.Trim();
        string DBAName = txtDBAName.Text.Trim();
        string ContactName = txtPrimaryCotnact.Text.Trim();
        string ZIPCode = txtZipCode.Text.Trim();
        DateTime CreatedFromDate = txtCreatedFromDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedFromDate.Text, System.Globalization.CultureInfo.InvariantCulture);
        DateTime CreatedToDate = txtCreatedToDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedToDate.Text, System.Globalization.CultureInfo.InvariantCulture);
        int count = 0;
        DataSet ds;
       int statusid= Convert.ToInt32(ddlStatus.SelectedItem.Value);
        ds = OrganizationInfo.SearchStakeholdersByCriteria(CurPageNum, pageSize, out count, OrganizationId, OrganizationTypeId,true, StakeholderName, DBAName, ContactName, ZIPCode, CreatedFromDate, CreatedToDate, LanguageId,statusid,txtEmail.Text.Trim());
        gvApplicationApproved.DataSource = ds;
        gvApplicationApproved.DataBind();
        this.TotalItems = count;
        this.pager.DrawPager(pageNo, this.TotalItems, pageSize, MaxPagesToShow);

    }

    protected void gvLatestSteward_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int statusid = Convert.ToInt32(((HiddenField)e.Row.FindControl("hfStatus")).Value);
            Label lblText = e.Row.FindControl("status") as Label;
            if (statusid == 1) { lblText.CssClass = "badge badge-warning"; lblText.Text = "Pending"; }
            if (statusid == 2) { lblText.CssClass = "badge badge-primary"; lblText.Text = "Approved"; }
            if (statusid == 3) { lblText.CssClass = "badge badge-danger"; lblText.Text = "Rejected"; }
            if (statusid == 4) { lblText.CssClass = "badge badge-disable"; lblText.Text = "Deleted"; }

        }
    }

    protected void gvApplicationApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteStakeholder")
        {
            OrganizationInfo.ApprovedStakeholderInActive(Convert.ToInt32(e.CommandArgument));
            LoadStakeholders(1);

        }
    }
    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pager.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.LoadStakeholders(CurrentPage);
        }


        return base.OnBubbleEvent(source, args);
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStakeholders(1);
    }
}