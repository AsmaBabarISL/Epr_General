using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class Application_ViewApplications : BasePage
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


        GetPermission(ResourceType.Applications, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        
        if (User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("/");
        }

        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liApplications','{0}');", ResourceMgr.GetMessage("Applications")), true);
        if (!IsPostBack)
        {
            LoadPendingApplications(1);

        }

        if (TotalItemsR > 0)
        {

            pager.DrawPager(CurrentPage, TotalItemsR, pageSize, MaxPagesToShow);
        }
    }



    protected void btnStakeSearch_Click(object sender, EventArgs e)
    {
        LoadPendingApplications(1);
    }

    private void LoadPendingApplications(int pageNo)
    {

        gvApplicationNotApproved.PageSize = pageSize;
        int OrganizationId = UserOrganizationId;
        int OrganizationTypeId = 0;
        string StakeholderName = txtStakeholderName.Text.Trim();
        string DBAName = txtDBAName.Text.Trim();
        string ContactName = txtPrimaryCotnact.Text.Trim();
        string ZIPCode = txtZipCode.Text.Trim();
        DateTime CreatedFromDate = txtCreatedFromDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedFromDate.Text, System.Globalization.CultureInfo.InvariantCulture);
        DateTime CreatedToDate = txtCreatedToDate.Text.Trim() == "" ? DateTime.MinValue : Convert.ToDateTime(txtCreatedToDate.Text, System.Globalization.CultureInfo.InvariantCulture);
        int count = 0;
        gvApplicationNotApproved.DataSource = OrganizationInfo.SearchStakeholdersByCriteria(pageNo, pageSize, out count, OrganizationId, OrganizationTypeId, false, StakeholderName, DBAName, ContactName, ZIPCode, CreatedFromDate, CreatedToDate, LanguageId,1,txtEmail.Text.Trim());
        gvApplicationNotApproved.DataBind();
        this.TotalItems = count;
        this.pager.DrawPager(pageNo, this.TotalItems, pageSize, MaxPagesToShow);

    }

    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pager.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.LoadPendingApplications(CurrentPage);
        }


        return base.OnBubbleEvent(source, args);
    }

    protected void gvApplicationNotApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteStakeholder")
        {
            OrganizationInfo.InApprovedStakeholderInActive(Convert.ToInt32(e.CommandArgument));
            LoadPendingApplications(1);
        }
        else if (e.CommandName == "Approve")
        {
            DateTime dt = DateTime.Now;
            OrganizationInfo.ApprovedStakeholderByAdmin(Convert.ToInt32(e.CommandArgument), currentUserInfo.UserId, dt);
            LoadPendingApplications(1);
        }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPendingApplications(1);
    }
}