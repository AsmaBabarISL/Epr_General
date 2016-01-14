using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using TireTraxLib.Security;
using System.Data;
using System.Text;
using System.Data.SqlClient;
public partial class Permission_UsersPermission : BasePage
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
        if (!IsPostBack)
        {
            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liPermission','{0}');", ResourceMgr.GetMessage("UserPermissions")), true);
            loadUserpermission();
            LoadDropDown();
        }
    }
    private void LoadDropDown()
    {
        Utils.GetLookUpData<DropDownList>(ref ddlCountry, LookUps.Country);
        Utils.GetLookUpData<DropDownList>(ref ddlOrganizationType, LookUps.OrganizationType,LanguageId);
    }
    protected void GridPaging()
    {
        try
        {
            int startRecordNumber = (CurPageNum - 1) * pageSize + 1;
            int endRecordNumber = startRecordNumber + gvUserPermission.Rows.Count - 1;

            if (gvUserPermission.Rows.Count == 0)
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
            new SqlLog().InsertSqlLog(0, "UserPermission.GridPaging", ex);
        }
    }
    public void loadUserpermission()
    {
        try
        {
           
            bool IsActive = false;
            if (ddlActive.SelectedValue == "1")
                IsActive = true;

            DataSet ds = UserInfo.getUserPermissionByFilter(CurPageNum, pageSize, Conversion.ParseInt(ddlRole.SelectedValue), Conversion.ParseInt(ddlOrganizationType.SelectedValue), txtUserName.Text, txtLegalName.Text, txtLogin.Text, Conversion.ParseInt(ddlCountry.SelectedValue), Conversion.ParseInt(ddlState.SelectedValue), IsActive, out totalRows);
            gvUserPermission.DataSource = ds;
            gvUserPermission.DataBind();
            GridPaging();


        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Addpages.aspx.loadPages", ex);
        }
    }
    protected void gvUserPermission_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteUser")
        {

            UserInfo.DeleteAdminUser(Convert.ToInt32(e.CommandArgument));
            loadUserpermission();
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtLegalName.Text = "";
        txtUserName.Text = "";
        txtLogin.Text = "";
        ddlActive.SelectedValue = "1";
        ddlCountry.SelectedIndex = 0;
        ddlOrganizationType.SelectedIndex = 0;
        ddlRole.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        loadUserpermission();
    }

    protected void btnUserSearch_Click(object sender, EventArgs e)
    {
        loadUserpermission();
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvstate.Visible = true;
        ddlState.Items.Clear();
        if (ddlCountry.SelectedValue != "0")
        {
            Utils.GetLookUpData<DropDownList>(ref ddlState, LookUps.StewardshipTypes, Conversion.ParseInt(ddlCountry.SelectedValue));
        }
        ClientScript.RegisterStartupScript(GetType(), "ShowSearchMenu", "javascript:ShowSearch();"); 
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        pageSize =Conversion.ParseInt( ddlPageSize.SelectedValue);
        loadUserpermission();
    }
    protected void ddlOrganizationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRole.Items.Clear();
        Utils.GetLookUpData<DropDownList>(ref ddlRole, LookUps.RoleName, Conversion.ParseInt(ddlOrganizationType.SelectedValue));
        ClientScript.RegisterStartupScript(GetType(), "ShowSearchMenu", "javascript:ShowSearch();");
    }
    protected void gvUserPermission_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label imgSt = (Label)e.Row.FindControl("imgStatus");

                    bool bitstatus = Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsActive"));


                    if (bitstatus)
                    {
                        imgSt.CssClass = "badge badge-primary";
                        imgSt.Text = "Active";
                    }
                    else
                    {
                        imgSt.CssClass = "badge badge-danger";
                        imgSt.Text = "In Active";
                    }

                }
            
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "gvUserPermission_RowDataBound", ex);

        }
    }
}