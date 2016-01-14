using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Text;
using System.Threading;
using System.Configuration;


public partial class Stewardship_ViewStewardship :BasePage
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
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liStewardship');", true);
        if (!IsPostBack)
            LoadGrid();
    }

    private void LoadGrid()
    {
        try  
        {
            pageSize = Conversion.ParseInt(ddlPageSize.SelectedValue);
            gvStewardship.PageSize = pageSize;
            int statusid = Convert.ToInt32(ddlStatus.SelectedItem.Value);
            pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            gvStewardship.PageSize = pageSize;
            gvStewardship.DataSource = OrganizationInfo.SearchStewardshipByCriteria(txtLegalName.Text, txtTX_ID.Text, txtPhone.Text, txtEmail.Text, txtContact.Text, LanguageId, Convert.ToInt32(UserInfo.UserRole.Stewardship),statusid, CurPageNum, pageSize, out totalRows);
            gvStewardship.DataBind();

            GridPaging();

            Utils.GetLookUpData<DropDownList>(ref ddlOrganizationType, LookUps.OrganizationType,LanguageId);
            Utils.GetLookUpData<DropDownList>(ref ddlOrganizationTypeForUser, LookUps.OrganizationType,LanguageId);
           string standardIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
           string[] stewardshipIdsArr = standardIds.Split(',');
           foreach (string s in stewardshipIdsArr)
           {
               if (ddlOrganizationType.Items.FindByValue(s) != null)
               {
                   ddlOrganizationType.Items.Remove(ddlOrganizationType.Items.FindByValue(s));
               }

               if (ddlOrganizationTypeForUser.Items.FindByValue(s) != null)
               {
                   ddlOrganizationTypeForUser.Items.Remove(ddlOrganizationTypeForUser.Items.FindByValue(s));
               }
           }


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
            sb.Append(@"</div></div>");
            ltrlPaging.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminStakeholders.GridPaging", ex);
        }
    }

    protected void search_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
    //protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList DropDownList1 = (DropDownList)sender;
    //    int statusId = Convert.ToInt32(DropDownList1.SelectedItem.Value);
    //    GridViewRow grdrDropDownRow = ((GridViewRow)DropDownList1.Parent.Parent);
    //    int orgId = Convert.ToInt32(((HiddenField)grdrDropDownRow.FindControl("hfOrganizationId")).Value);
    //    OrganizationStatus s = (OrganizationStatus)statusId;
    //    OrganizationInfo.SetStatus(s, orgId);

    //    LoadGrid();
    //}
    protected void gvLatestSteward_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int statusid = Convert.ToInt32(((HiddenField)e.Row.FindControl("hfStatus")).Value);
            Label lblText = e.Row.FindControl("status") as Label;
            if (statusid == 1) { lblText.CssClass = "badge badge-warning"; lblText.Text = "Pending"; }
            if (statusid == 2) { lblText.CssClass = "badge badge-primary"; lblText.Text = "Approved"; }
            if (statusid == 3) { lblText.CssClass = "badge badge-danger"; lblText.Text = "Rejected"; }
            if (statusid == 4) { lblText.CssClass = ""; lblText.Text = ""; }
            
        }
    }
    protected void gvStewardship_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        string url = System.Configuration.ConfigurationManager.AppSettings["EmailUrlStaging"].ToString();
        if (e.CommandName == "DeleteStewardship")
        {
          //  OrganizationInfo.UpdateStewardshipAsInactive(Convert.ToInt32(e.CommandArgument));
         //   OrganizationInfo.SetStatus(OrganizationStatus.Deleted, Convert.ToInt32(e.CommandArgument), "Deleted from stewardship");
            LoadGrid();
        }
        else if (e.CommandName == "Approve")
        {
            
            OrganizationInfo.PendingStewardshipChange(Convert.ToInt32(e.CommandArgument));
           
            OrganizationInfo orginfoObj=new OrganizationInfo(Conversion.ParseInt(e.CommandArgument));
            string body = @"<div style='font-size:11px; font-family:Verdana,Arial,Helvetica,sans-serif;'>
                       <p>Dear Concern,</p><p> Your request have been approved click on following URL to continue your application.</p> <br />
                       <a target='_blank' href="+url+">Click</a></div>";
            Email mail = new Email("noreply@EPRTS.com", orginfoObj.PrimaryEmail, "Registration Approval Email For Organization " + orginfoObj.LegalName + " .", body);
           
        
                LoadGrid();
        }
    }

    private void SendEmails(Emails email, string type)
    {
        try
        {
            Emails.SendEmail(email, type);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewStewardship.aspx SendEmails", ex);
        }

    }

    protected void ddlOrganizationTypeForUser_SelectedIndexChanged(object sender, EventArgs e)
    {

        int organizationTypeId=Conversion.ParseInt(ddlOrganizationTypeForUser.SelectedValue);
         int organizationId=Conversion.ParseInt(hdnOrganizationId.Value);
        Utils.GetLookUpData<DropDownList>(ref ddlOrganization, LookUps.OrgStakeholderByStewardshipIdAndOrgTypeId,organizationTypeId,organizationId);

        //int TotalRows;
        //ddlOrganization.Items.Clear();

        //System.Data.DataSet ds = OrganizationInfo.getOrganizationsByIdAndType(1, 100, Convert.ToInt32(hdnOrganizationId.Value), Convert.ToInt32(ddlOrganizationTypeForUser.SelectedValue), true, "", out TotalRows);
        //ddlOrganization.DataSource = ds;
        //ddlOrganization.DataTextField = "LegalName";
        //ddlOrganization.DataValueField = "OrganizationId";

        //ddlOrganization.DataBind();

        //ddlOrganization.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));

    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        pageSize = Conversion.ParseInt(ddlPageSize.SelectedValue);
        LoadGrid();
    }
   
}
