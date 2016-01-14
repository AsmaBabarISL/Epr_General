using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using TireTraxLib.Security;
using System.Data;

public partial class Permission_GroupPermissions : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liPermission','{0}');", ResourceMgr.GetMessage("Permissions")), true);
            Utils.GetLookUpData<CheckBoxList>(ref cblGroupName, LookUps.Groups, true);
            LoadRoles();

        }
        lblError.Text = "";
        lblInfo.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        try
        {
            if (ddlgroup.SelectedIndex == 0)
            {
                lblError.Text = "Please Select Group";
            }
            else
            {
                Groups.AddRoleGroup(Convert.ToInt32(ddlgroup.SelectedValue), Convert.ToInt32(ddlRole.SelectedValue), Convert.ToInt32(ddlSubRole.SelectedValue));
                lblInfo.Text = "Permission is Successfully Added";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "GroupPermission.aspx.btnSave_Click", ex);

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Utils.GetLookUpData<DropDownList>(ref ddlgroup, LookUps.Groups, true);
            //dvgrp.Visible = false;
            dvSubRole.Visible = false;
            LoadRoles();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "GroupPermission.aspx.btnCancel_Click", ex);
        }
    }


    private void LoadRoles()
    {
        try
        {
            ddlgroup.Items.Insert(0, new ListItem("--Select Group--", "0"));
            Utils.GetLookUpData<DropDownList>(ref ddlRole, LookUps.OrganizationType, LanguageId);
            Utils.GetLookUpData<DropDownList>(ref ddlgroup, LookUps.Groups, true);
        }
        catch (Exception ex)
        {

            new SqlLog().InsertSqlLog(0, "GroupPermission.aspx.LoadRoles", ex);
        }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        //groupName.Visible = false;
        //cblGroupName.Visible = false;
        cblGroupName.ClearSelection();
        btnSaveChanges.Visible = false;
        btnCancelChanges.Visible = false;

        Utils.GetLookUpData<DropDownList>(ref ddlSubRole, LookUps.RoleName, Conversion.ParseInt(ddlRole.SelectedValue));

        if (ddlRole.SelectedIndex == 0)
        {
            //dvgrp.Visible = false;
            dvSubRole.Visible = false;

            btnSaveChanges.Visible = false;
            btnCancelChanges.Visible = false;

        }
        else
        {
            //dvgrp.Visible = false;
            dvSubRole.Visible = true;
        }
    }

    protected void ddlSubRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        cblGroupName.ClearSelection();
        loadGroups();
        #region comments
        //try
        //{
        //    int roleid = Conversion.ParseInt(ddlRole.SelectedValue);
        //    int subroleid = Conversion.ParseInt(ddlSubRole.SelectedValue);
        //    DataTable dt = RoleManagement.GetRoleSubroleNGroupByIds(roleid, subroleid);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        //cblGroupName.Visible = true;
        //        lblError.Text = "";
        //        lblInfo.Text = "";
        //        //dvgrp.Visible = false;
        //        btnCancelChanges.Visible = false;
        //        btnSaveChanges.Visible = false;


        //        loadGroups();
        //        return;
        //    }
        //    else
        //    {
        //        lblError.Text = "";
        //        lblInfo.Text = "";

        //        if (ddlSubRole.SelectedIndex == 0)
        //        {

        //            //dvgrp.Visible = false;
        //            btnCancelChanges.Visible = false;
        //            btnSaveChanges.Visible = false;
        //            groupName.Visible = false;
        //            cblGroupName.Visible = false;
                    
        //            //cblGroupName.Visible = true;
        //        }
        //        else
        //        {
        //            //dvgrp.Visible = true;
        //            btnSaveChanges.Visible = true;
        //            btnCancelChanges.Visible = true;
        //            groupName.Visible = true;
        //            cblGroupName.Visible = true;
                    
        //            //cblGroupName.Visible = false;
        //        }

                

                
        //        return;
        //    }
        //}
        //catch (Exception ex)
        //{

        //    new SqlLog().InsertSqlLog(0, "GroupPermission.aspx.ddlSubRole_SelectedIndexChanged", ex);
        //}
        #endregion
    }

    protected void btnCancelChanges_Click(object sender, EventArgs e)
    {
        //groupName.Visible = false;
        //cblGroupName.Visible = false;
        ddlRole.SelectedIndex = -1;

        btnSaveChanges.Visible = false;
        btnCancelChanges.Visible = false;
        dvSubRole.Visible = false;
    }
    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        try
        {
            bool atleaseOneRoleCheck = false;
            int roleid = Conversion.ParseInt(ddlRole.SelectedValue);
            int subroleid = Conversion.ParseInt(ddlSubRole.SelectedValue);
            string groupid = ""; bool firstitem = true;
            List<int> groupIds = new List<int>();
            foreach (ListItem i in cblGroupName.Items)
            {

                if (i.Selected)
                {
                    atleaseOneRoleCheck = true;

                    if (firstitem)
                    {

                        firstitem = false;
                    }
                    else groupid += ",";
                    groupid += i.Value.ToString();
                    groupIds.Add(Convert.ToInt32(i.Value));
                }

            }
            if (atleaseOneRoleCheck)
            {
                Groups.UpdateRoleGroup(groupid, roleid, Convert.ToInt32(subroleid));
                loadGroups();
                lblInfo.Text = "Permission is Successfully Added";
                lblInfo.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                loadsecuritycode();
            }
            else
            {
                
                lblError.Text = "Please Select Atlease One Group Name";
                lblError.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            }
        }
        catch (Exception ex)
        {

            new SqlLog().InsertSqlLog(0, "GroupPermission.aspx.btnSaveChanges_Click", ex);
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
    }
    private void loadsecuritycode()
    {
        // updating token
        try
        {
            int roleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            AccessToken token = new AccessToken();
            string accessToken = AccessToken.ReturnEmptyToken();
            //accessToken = ReadPermissions(accessToken, token);

            // loading existing token
            int roleid = Conversion.ParseInt(ddlRole.SelectedValue);
            int subroleid = Conversion.ParseInt(ddlSubRole.SelectedValue);
            DataTable dt = RoleManagement.GetRoleSubroleNGroupByIds(roleid, subroleid);
            List<int> groupids = new List<int>();
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string groupid = dt.Rows[j]["intGroupID"].ToString();
                    groupids.Add(Convert.ToInt32(groupid));
                }
                System.Data.DataTable resourceTopics = GroupPages.GetAllResources();
                for (int i = 0; i < resourceTopics.Rows.Count; i++)
                {
                    DataRow row = resourceTopics.Rows[i];
                    int resourceid = Convert.ToInt32(row["intResourceId"].ToString());
                    //foreach (ResourceType item in Enum.GetValues(typeof(ResourceType)))
                    //{
                    List<string> securityTokens = new List<string>();
                    foreach (int gid in groupids)
                    {
                        securityTokens.Add(RoleManagement.GetSecurityToken(Convert.ToInt32(gid)));
                    }
                    canView = canAdd = canUpdate = canDelete = false;
                    foreach (string st in securityTokens)
                    {
                        PermissionManagement.GetPermissionOnlyFalsed(resourceid, ref canView, ref canAdd, ref canUpdate, ref canDelete, st);
                    }
                    accessToken = ReadDetailPermissions(resourceid, canView, canAdd, canUpdate, canDelete, accessToken, token);
                    //}
                }

            }
            // generating new token
            RoleManagement.UpdateSecurityTokenbyRoleId(subroleid, accessToken);
        }
        catch (Exception ex)
        {

            new SqlLog().InsertSqlLog(0, "GroupPermission.aspx.loadsecuritycode", ex);
        }
    }
    private string ReadDetailPermissions(ResourceType item, bool canView, bool canAdd, bool canUpdate, bool canDelete, string accessToken, AccessToken token)
    {

        try
        {
            int resourceId = Convert.ToInt32(item);

            // View Permission
            int viewResourceId = resourceId;
            if (canView)
                accessToken = token.CreateToken(viewResourceId, SwitchType.On, accessToken);
            else
                accessToken = token.CreateToken(viewResourceId, SwitchType.Off, accessToken);
            // Add Permission
            int addResourceId = resourceId + 2;
            if (canAdd)
                accessToken = token.CreateToken(addResourceId, SwitchType.On, accessToken);
            else
                accessToken = token.CreateToken(addResourceId, SwitchType.Off, accessToken);
            // Updated Permission
            int updateResourceId = resourceId + 3;
            if (canUpdate)
                accessToken = token.CreateToken(updateResourceId, SwitchType.On, accessToken);
            else
                accessToken = token.CreateToken(updateResourceId, SwitchType.Off, accessToken);
            // Delete Permission
            int deleteResourceId = resourceId + 4;
            if (canDelete)
                accessToken = token.CreateToken(deleteResourceId, SwitchType.On, accessToken);
            else
                accessToken = token.CreateToken(deleteResourceId, SwitchType.Off, accessToken);
        }
        catch (Exception ex)
        {

            new SqlLog().InsertSqlLog(0, "GroupPermission.aspx.ReadDetailPermissions", ex);
        }
        return accessToken;
    }
    private string ReadDetailPermissions(int resourceid, bool canView, bool canAdd, bool canUpdate, bool canDelete, string accessToken, AccessToken token)
    {

        int resourceId = Convert.ToInt32(resourceid);

        // View Permission
        int viewResourceId = resourceId;
        if (canView)
            accessToken = token.CreateToken(viewResourceId, SwitchType.On, accessToken);
        else
            accessToken = token.CreateToken(viewResourceId, SwitchType.Off, accessToken);
        // Add Permission
        int addResourceId = resourceId + 2;
        if (canAdd)
            accessToken = token.CreateToken(addResourceId, SwitchType.On, accessToken);
        else
            accessToken = token.CreateToken(addResourceId, SwitchType.Off, accessToken);
        // Updated Permission
        int updateResourceId = resourceId + 3;
        if (canUpdate)
            accessToken = token.CreateToken(updateResourceId, SwitchType.On, accessToken);
        else
            accessToken = token.CreateToken(updateResourceId, SwitchType.Off, accessToken);
        // Delete Permission
        int deleteResourceId = resourceId + 4;
        if (canDelete)
            accessToken = token.CreateToken(deleteResourceId, SwitchType.On, accessToken);
        else
            accessToken = token.CreateToken(deleteResourceId, SwitchType.Off, accessToken);

        return accessToken;
    }
    private void loadGroups()
    {
        try
        {
            //groupName.Visible = true;
            //cblGroupName.Visible = true;
            btnSaveChanges.Visible = true;
            btnCancelChanges.Visible = true;
            //Utils.GetLookUpData<CheckBoxList>(ref cblGroupName, LookUps.Groups, true);
            int roleid = Conversion.ParseInt(ddlRole.SelectedValue);
            int subroleid = Conversion.ParseInt(ddlSubRole.SelectedValue);
            DataTable dt = RoleManagement.GetRoleSubroleNGroupByIds(roleid, subroleid);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (ListItem i in cblGroupName.Items)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string groupid = dt.Rows[j]["intGroupID"].ToString();
                        if (i.Value == groupid)
                        {
                            i.Selected = true;
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {

            new SqlLog().InsertSqlLog(0, "GroupPermission.aspx.loadGroups", ex);
        }
    }
    private SwitchType ReadAddPermission(Control permissionControl)
    {
        CheckBox chkAdd = (CheckBox)permissionControl.FindControl("chkAdd");
        if (chkAdd.Checked)
            return SwitchType.On;
        else
            return SwitchType.Off;
    }

    private SwitchType ReadUpdatePermission(Control permissionControl)
    {
        CheckBox chkUpdate = (CheckBox)permissionControl.FindControl("chkUpdate");
        if (chkUpdate.Checked)
            return SwitchType.On;
        else
            return SwitchType.Off;
    }

    private SwitchType ReadViewPermission(Control permissionControl)
    {
        CheckBox chkEdit = (CheckBox)permissionControl.FindControl("chkView");
        if (chkEdit.Checked)
            return SwitchType.On;
        else
            return SwitchType.Off;
    }

    private SwitchType ReadDeletePermission(Control permissionControl)
    {
        CheckBox chkDelete = (CheckBox)permissionControl.FindControl("chkDelete");
        if (chkDelete.Checked)
            return SwitchType.On;
        else
            return SwitchType.Off;
    }





}