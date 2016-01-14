using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using TireTraxLib.Security;
using System.Data;

public partial class Permission_AddRole : BasePage
{

    private int _roleId;

    public int RoleId
    {
        get { return _roleId; }
        set { _roleId = value; }
    }
    protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton imgSt = (LinkButton)e.Row.FindControl("imgStatus");
                bool bitstatus = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitIsDefault"));

                if (bitstatus == true)
                {
                    imgSt.Text = "Active";
                    imgSt.CssClass = "badge badge-primary";
                }
                else
                {
                    imgSt.Text = "Inactive";
                    imgSt.CssClass = "badge badge-danger";
                }
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Generic Error", ex);

        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liPermission','{0}');", ResourceMgr.GetMessage("Permissions")), true);
            // ddlgroup.Items.Insert(0, new ListItem("--Select Group--", "0"));
            LoadRoles();
            // Utils.GetLookUpData<DropDownList>(ref ddlgroup, LookUps.Groups, true);
            //lblInfo.Text = "";  
        }

        else
        {
            if (TotalItems > 0)
            {
                pgrLots.DrawPager(CurrentPage, TotalItems, pageSize, MaxPagesToShow);
            }
        }
    }

    private void LoadRoles()
    {
        try
        {
            Utils.GetLookUpData<DropDownList>(ref ddlRole, LookUps.OrganizationType, LanguageId);
            //DataTable dt = Groups.GetMainRoles(11);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    ddlRole.DataSource = dt;
            //    ddlRole.DataTextField = "RoleName";
            //    ddlRole.DataValueField = "RoleId";
            //    ddlRole.DataBind();
            //}
            //ddlRole.Items.Insert(0, new ListItem("--Select Role--", "0"));
        }
        catch (Exception ex)
        {

            new SqlLog().InsertSqlLog(0, "AddRole.aspx. LoadRoles", ex);
        }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int groupId = Conversion.ParseInt(ddlRole.SelectedItem.Value);

            loadRolesbyGroupId("1", groupId);
        }
        catch (Exception ex)
        {

            new SqlLog().InsertSqlLog(0, "AddRole.aspx. LoadRoles", ex);
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int status = Groups.getRoleNameStatus(txtRole.Text.Trim(), Conversion.ParseInt(ddlRole.SelectedItem.Value));
            if (status == 0)
            {
                lblRoleNameError.Text = string.Empty;

                Groups.AddGroupRole(Convert.ToInt32(ddlRole.SelectedValue), txtRole.Text.Trim());
                lblInfo.Text = "Role is Successfully Added";
                txtRole.Text = string.Empty;
                int groupId = Conversion.ParseInt(ddlRole.SelectedItem.Value);

                loadRolesbyGroupId("1", groupId);
            }
            else
            {
                lblInfo.Text = string.Empty;
                lblRoleNameError.Text = "Role Name Already Exist";
            }
        }
        catch (Exception ex)
        {

            new SqlLog().InsertSqlLog(0, "AddRole.aspx. btnSave_Click", ex);
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Permission/AddRole.aspx");
    }

    public void loadRolesbyGroupId(string pageNo, int groupId)
    {
        try
        {

            hdngroupid.Value = groupId.ToString();
            gvRoles.PageSize = pageSize;
            CurrentPage = Conversion.ParseInt(pageNo);
            int count = 0;
            gvRoles.DataSource = Groups.getRolesByGroupId(Conversion.ParseInt(pageNo), pageSize, groupId, out count);
            gvRoles.DataBind();
            this.TotalItems = count;
            this.pgrLots.DrawPager(Conversion.ParseInt(pageNo), this.TotalItems, pageSize, MaxPagesToShow);


        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddRole.aspx.loadRolesbyGroupId", ex);
        }

    }
    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        try
        {
            if (this.pgrLots.Equals(source))
            {
                CommandEventArgs cmdArgs = (CommandEventArgs)args;
                CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

                this.loadRolesbyGroupId(CurrentPage.ToString(), Conversion.ParseInt(hdngroupid.Value));
            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddRole.aspx.OnBubbleEvent", ex);
        }
        return base.OnBubbleEvent(source, args);
    }
    protected void gvRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvRoles_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            lblInfo.Text = string.Empty;
            lblRoleNameError.Text = string.Empty;
            if (e.CommandName == "Status")
            {
                LinkButton imgStatus = (LinkButton)e.CommandSource;    // the button
                GridViewRow grRow = (GridViewRow)imgStatus.Parent.Parent;  // the row
                GridView grimage = (GridView)sender; // the gridview
                string ID = grimage.DataKeys[grRow.RowIndex].Value.ToString();
                Role.ActiveDeactiveRole(Convert.ToInt32(e.CommandArgument), Conversion.ParseInt(ddlRole.SelectedItem.Value));
                
             //   lblError.Visible = false;

                int groupId = Conversion.ParseInt(ddlRole.SelectedItem.Value);

                loadRolesbyGroupId("1", groupId);
            }
            else
            if (e.CommandName == "Delete")
            {


                DataSet ds = null;
                hdnSubRoleTypeId.Value = e.CommandArgument.ToString();
                ds = Groups.getUserRolesStatusByRoleId(Conversion.ParseInt(e.CommandArgument));

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dvUserRoleInfo.Visible = true;
                    gvUserRoleInfo.DataSource = ds;
                    gvUserRoleInfo.DataBind();

                }
                else
                {


                    RoleId = Conversion.ParseInt(e.CommandArgument);
                    Groups.deleteRolebyRoleId(RoleId);
                    int groupId = Conversion.ParseInt(ddlRole.SelectedItem.Value);

                    loadRolesbyGroupId("1", groupId);
                }
            }
            else if (e.CommandName == "Edit")
            {
                RoleId = Conversion.ParseInt(e.CommandArgument);


            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddRole.aspx.gvRoles_RowCommand", ex);
        }


    }
    protected void gvRoles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvRoles.EditIndex = -1;
        loadRolesbyGroupId("1", Conversion.ParseInt(hdngroupid.Value));
    }
    protected void gvRoles_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvRoles.EditIndex = e.NewEditIndex;
        loadRolesbyGroupId("1", Conversion.ParseInt(hdngroupid.Value));
    }
    protected void gvRoles_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void gvRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            TextBox txtRoleName1 = (TextBox)gvRoles.Rows[e.RowIndex].FindControl("txtRoleName");

            HiddenField hndroleid = (HiddenField)gvRoles.Rows[e.RowIndex].FindControl("hdnroleid");
            RoleId = Conversion.ParseInt(hndroleid.Value);

            Label lblInfoGV = gvRoles.Rows[e.RowIndex].FindControl("lblInfoF") as Label;

            int status = Groups.getRoleNameStatus(txtRoleName1.Text.Trim(), Conversion.ParseInt(ddlRole.SelectedItem.Value));
            if (status == 0)
            {
                Groups.updateRoleInfoByRoleId(RoleId, txtRoleName1.Text.Trim());


                gvRoles.EditIndex = -1;

                loadRolesbyGroupId("1", Conversion.ParseInt(hdngroupid.Value));
            }
            else
            {
                lblInfoGV.Text = "Role Name Already Exist";
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddRole.aspx.gvRoles_RowUpdating", ex);
        }
    }
    protected void gvUserRoleInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            //     GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            //     int RemoveAt = gvr.RowIndex;
            //     DropDownList ddlSubtypeRoleName = (DropDownList)gvUserRoleInfo.Rows[RemoveAt].FindControl("ddlRoleName");
            //     GridViewRow gvRow = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            //     Int32 rowind = gvRow.RowIndex;
            //     DropDownList ddlState = (DropDownList)gvRow.FindControl("ddlRoleName1");
            //     //GridViewRow grdRow = (GridViewRow)e.CommandSource;
            //     //DropDownList ddlSubtypeRoleName = (DropDownList)grdRow.FindControl("ddlRoleName");
            //     //DropDownList ddlSubtypeRoleName = (DropDownList)gvUserRoleInfo.Rows[0].Cells[0].FindControl("ddlRoleName");
            //    // GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            //// DropDownList ddlSubtypeRoleName = (DropDownList)row.Cells[0].FindControl("ddlRoleName");

            //     //DropDownList ddlSubtypeRoleName = (DropDownList)gvUserRoleInfo.Rows[e.RowIndex].FindControl("ddlstakeholderTypeListeditor");
            //     Utils.GetLookUpData<DropDownList>(ref ddlState, LookUps.SubRolesTypes, Conversion.ParseInt(e.CommandArgument));
            // }
        }
    }
    protected void gvUserRoleInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvUserRoleInfo.EditIndex = e.NewEditIndex;

            //  DropDownList ddlStewardships = gvSetting.Rows[e.RowIndex].FindControl("ddlStewardships") as DropDownList;


            DataSet ds = Groups.getUserRolesStatusByRoleId(Conversion.ParseInt(hdnSubRoleTypeId.Value));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dvUserRoleInfo.Visible = true;
                gvUserRoleInfo.DataSource = ds;
                gvUserRoleInfo.DataBind();

            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddRole.aspx.gvUserRoleInfo_RowEditing", ex);
        }
    }
    protected void gvUserRoleInfo_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void gvUserRoleInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        try
        {
            DropDownList ddlSubRoleTypeId = (DropDownList)gvUserRoleInfo.Rows[e.RowIndex].FindControl("ddlRoleName");
            HiddenField hdnLookupType = (HiddenField)gvUserRoleInfo.Rows[e.RowIndex].FindControl("hdnLookupType");
            HiddenField hdnUserId = (HiddenField)gvUserRoleInfo.Rows[e.RowIndex].FindControl("hdnUserId");

            int count = Groups.UpdateSubRoleByRoleIdandUserId(Conversion.ParseInt(hdnUserId.Value), Conversion.ParseInt(ddlSubRoleTypeId.SelectedValue.ToString()));

            gvUserRoleInfo.EditIndex = -1;
            DataSet ds = Groups.getUserRolesStatusByRoleId(Conversion.ParseInt(hdnSubRoleTypeId.Value));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dvUserRoleInfo.Visible = true;
                gvUserRoleInfo.DataSource = ds;
                gvUserRoleInfo.DataBind();

            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddRole.aspx.gvUserRoleInfo_RowUpdating", ex);
        }
    }
    protected void gvUserRoleInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        try
        {
            gvUserRoleInfo.EditIndex = -1;
            DataSet ds = Groups.getUserRolesStatusByRoleId(Conversion.ParseInt(hdnSubRoleTypeId.Value));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dvUserRoleInfo.Visible = true;
                gvUserRoleInfo.DataSource = ds;
                gvUserRoleInfo.DataBind();

            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddRole.aspx.gvUserRoleInfo_RowCancelingEdit", ex);
        }
    }
    protected void gvUserRoleInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvUserRoleInfo_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void gvUserRoleInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == gvUserRoleInfo.EditIndex)
            {
                DropDownList ddlSubRoleName = e.Row.FindControl("ddlRoleName") as DropDownList;
                HiddenField hdnSubLookUpTypeId = e.Row.FindControl("hdnLookupType") as HiddenField;

                Utils.GetLookUpData<DropDownList>(ref ddlSubRoleName, LookUps.SubRolesTypes, Conversion.ParseInt(hdnSubLookUpTypeId.Value));
                ddlSubRoleName.SelectedValue = hdnSubRoleTypeId.Value;


            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddRole.aspx.gvUserRoleInfo_RowDataBound", ex);
        }
    }


    protected void lnkAddRoleCancel_Click(object sender, EventArgs e)
    {
        dvUserRoleInfo.Visible = false;
    }
}