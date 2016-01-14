using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using TireTraxLib.Security;
using System.Security.AccessControl;
using System.Data;

public partial class Permission_PagePermissions : BasePage
{
    int intPageID;
    int intGroupID;
    string menu = string.Empty;
    string submenu = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        // ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liPermission');", true);
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liPermission','{0}');", ResourceMgr.GetMessage("Permissions")), true);

        // Page.Title = "NRCIA > Permissions";
        //GetPermission(TireTraxLib.ResourceType.PagePermissions, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        //if (!canView)
        //{
        //    Response.Redirect("/AccessDenied.aspx");
        //    //Response.Redirect("/adminDashboard.aspx");
        //}
        if (Request["menu"] != null)
        {
            menu = Request["menu"].ToString();
        }
        if (Request["subMenu"] != null)
        {
            submenu = Request["subMenu"].ToString();
        }
        if (!IsPostBack)
        {
            loaddetails();
            loadlists();
        }
    }

    private void loaddetails()
    {
        //DataSet ds = GroupPages.GetPages();
        //rptPages.DataSource = ds;
        //rptPages.DataBind();
    }

    private void loadlists()
    {
        try
        {
            ddlGroupName.Items.Insert(0, new ListItem("--Select Group--", "0"));
            Utils.GetLookUpData<DropDownList>(ref ddlGroupName, LookUps.Groups, true);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "PagePermission.aspx loadlists", ex);
        }
    }


    protected void btnAddResources_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGroupName.SelectedIndex != 0)
            {
                //GetPermission(TireTraxLib.ResourceType.PagePermissions, ref canView, ref canAdd, ref canUpdate, ref canDelete,ddlGroupName.SelectedValue);
                //if (!canUpdate)
                //{
                //    lblInfo.Text = "Sorry, you dont have right to change the premission";
                //}
                //else
                //{
                int groupId = Convert.ToInt32(ddlGroupName.SelectedValue.ToString());
                AccessToken token = new AccessToken();
                string accessToken = AccessToken.ReturnEmptyToken();
                if (chkAdmin.Checked)
                    accessToken = token.AdminToken(64, accessToken);
                else
                    accessToken = ReadPermissions(accessToken, token);

                RoleManagement.UpdateSecurityToken(groupId, accessToken);

                //it is used to update the Security token for Roles
                DataTable dt = RoleManagement.getDistinctRolesByGroupId(groupId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int TempRoleId = Conversion.ParseInt(dt.Rows[i]["RoleId"]);
                        int TempSubRoleId = Conversion.ParseInt(dt.Rows[i]["SubRoleId"]);
                        regenerateSecurityTokens(TempRoleId, TempSubRoleId);
                    }
                }
                lblInfo.ForeColor = System.Drawing.Color.Green;
                lblInfo.Text = "Permission is Successfully Added";

                //Update Cookies
                // UserInfo user = UserInfo.GetCurrentUserInfo();
                Response.Cookies["securityToken"].Expires = DateTime.Now.AddMinutes(-1);
                string securityToken = string.Empty;
                securityToken = RoleManagement.GetSecurityTokenByRoleId(groupId);
                //HttpCookie securityCookie = new HttpCookie("securityToken");
                //Decoder d= Encoding.UTF8.GetDecoder();

                //securityCookie.Value = Server.UrlEncode(securityToken);
                //HttpContext.Current.Response.Cookies.Add(securityCookie);
                // }
            }
            else
            {
                lblInfo.ForeColor = System.Drawing.Color.Red;
                lblInfo.Text = "Please Select the Group";
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "PagePermission.aspx btnAddResources_Click", ex);
        }
    }
    public void regenerateSecurityTokens(int roleId, int subroleId)
    {
        try
        {
            AccessToken token = new AccessToken();
            string accessToken = AccessToken.ReturnEmptyToken();
            // loading existing token
            int roleid = Conversion.ParseInt(roleId);
            int subroleid = Conversion.ParseInt(subroleId);
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
            new SqlLog().InsertSqlLog(0, "PagePermission.aspx regenerateSecurityTokens", ex);
        }
    }
    private string ReadDetailPermissions(int resourceid, bool canView, bool canAdd, bool canUpdate, bool canDelete, string accessToken, AccessToken token)
    {

        try
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
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "PagePermission.aspx ReadDetailPermissions", ex);
        }
        return accessToken;
    }

    private string ReadPermissions(string accessToken, AccessToken token)
    {
        try
        {
            Control pageContent = Master.FindControl("ContentPlaceHolder1");
            Control permssion = pageContent.FindControl("permission");



            for (int i = 1; i < permssion.Controls.Count; i++)
            {
                Control permissionPanel = (Control)permssion.Controls[i];
                accessToken = ReadPermissionsLoop(accessToken, token, permissionPanel);
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "PagePermission.aspx ReadPermissions", ex);
        }
        return accessToken;

    }
    private string ReadPermissionsLoop(string accessToken, AccessToken token, Control permissionPanel)
    {
        try
        {
            for (int i = 1; i < permissionPanel.Controls.Count; i++)
            {
                Control c = (Control)permissionPanel.Controls[i];
                if (c.ToString() == "ASP.commoncontrols_permissionresource_ascx")
                {
                    accessToken = ReadDetailPermissions(c, accessToken, token);
                }

            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "PagePermission.aspx ReadPermissionsLoop", ex);
        }
        return accessToken;
    }
    private void ReadFromAccess(string accessToken, AccessToken token)
    {
        try
        {
            Control pageContent = Master.FindControl("ContentPlaceHolder1");
            Control permssion = pageContent.FindControl("permission");

            for (int i = 0; i < permssion.Controls.Count; i++)
            {
                Control permissionPanel = (Control)permssion.Controls[i];
                ReadFromAccessLoop(accessToken, token, permissionPanel);
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "PagePermission.aspx ReadFromAccess", ex);
        }
    }
    private void ReadFromAccessLoop(string accessToken, AccessToken token, Control permissionPanel)
    {
        for (int i = 0; i < permissionPanel.Controls.Count; i++)
        {
            Control c = (Control)permissionPanel.Controls[i];
            if (c.ToString() == "ASP.commoncontrols_permissionresource_ascx")
            {
                ReadFromAccessToken(c, accessToken, token);
            }
        }
    }


    private string ReadDetailPermissions(Control permissionControl, string accessToken, AccessToken token)
    {
        HiddenField resourceTopicId = (HiddenField)permissionControl.FindControl("hdResourceId");
        int resourceId = Convert.ToInt32(resourceTopicId.Value);

        // View Permission
        int viewResourceId = resourceId;
        accessToken = token.CreateToken(viewResourceId, ReadViewPermission(permissionControl), accessToken);

        // Add Permission
        int addResourceId = resourceId + 2;
        accessToken = token.CreateToken(addResourceId, ReadAddPermission(permissionControl), accessToken);

        // Updated Permission
        int updateResourceId = resourceId + 3;
        accessToken = token.CreateToken(updateResourceId, ReadUpdatePermission(permissionControl), accessToken);

        // Delete Permission
        int deleteResourceId = resourceId + 4;
        accessToken = token.CreateToken(deleteResourceId, ReadDeletePermission(permissionControl), accessToken);

        return accessToken;
    }

    private void ReadFromAccessToken(Control permissionControl, string accessToken, AccessToken token)
    {
        HiddenField resourceTopicId = (HiddenField)permissionControl.FindControl("hdResourceId");
        int resourceId = Convert.ToInt32(resourceTopicId.Value);

        // View Permission
        int viewResourceId = resourceId;
        OnOffViewPermission(token.CheckRights(viewResourceId, accessToken), permissionControl);

        // Add Permission
        int addResourceId = resourceId + 2;
        OnOffAddPermission(token.CheckRights(addResourceId, accessToken), permissionControl);

        // Updated Permission
        int updateResourceId = resourceId + 3;
        OnOffUpdatePermission(token.CheckRights(updateResourceId, accessToken), permissionControl);

        // Delete Permission
        int deleteResourceId = resourceId + 4;
        OnOffDeletePermission(token.CheckRights(deleteResourceId, accessToken), permissionControl);
    }

    private void OnOffAddPermission(bool permission, Control permissionControl)
    {
        CheckBox chkAdd = (CheckBox)permissionControl.FindControl("chkAdd");
        if (permission)
            chkAdd.Checked = true;
        else
            chkAdd.Checked = false;
    }

    private void OnOffUpdatePermission(bool permission, Control permissionControl)
    {
        CheckBox chkUpdate = (CheckBox)permissionControl.FindControl("chkUpdate");
        if (permission)
            chkUpdate.Checked = true;
        else
            chkUpdate.Checked = false;
    }

    private void OnOffDeletePermission(bool permission, Control permissionControl)
    {
        CheckBox chkDelete = (CheckBox)permissionControl.FindControl("chkDelete");
        if (permission)
            chkDelete.Checked = true;
        else
            chkDelete.Checked = false;
    }

    private void OnOffViewPermission(bool permission, Control permissionControl)
    {
        CheckBox chkEdit = (CheckBox)permissionControl.FindControl("chkView");
        if (permission)
            chkEdit.Checked = true;
        else
            chkEdit.Checked = false;
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

    protected void ddlGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblInfo.Text = "";
            if (ddlGroupName.SelectedIndex != 0)
            {
                EnableControls(true);
                chkAdmin.Enabled = true;
                chkAdmin.Checked = false;
                SetAllPermissionsOff();
                int roleId = Convert.ToInt32(ddlGroupName.SelectedValue.ToString());
                string securityToken = RoleManagement.GetSecurityToken(roleId);

                AccessToken token = new AccessToken();
                if (token.AdminToken(64, token.EmptyString()).Equals(securityToken))
                {
                    SetAllPermissionsOn();
                    EnableControls(false);
                    chkAdmin.Checked = true;
                }
                else if (!string.IsNullOrEmpty(securityToken))
                {
                    ReadFromAccess(securityToken, token);
                    chkAdmin.Checked = false;
                }
                else
                {
                    SetAllPermissionsOff();
                    chkAdmin.Checked = false;
                    EnableControls(true);
                }
            }
            else
            {
                SetAllPermissionsOff();
                chkAdmin.Enabled = false;
                EnableControls(false);
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "PagePermission.aspx ddlGroupName_SelectedIndexChanged", ex);
        }
    }

    private void SetAllPermissionsOff()
    {
        Control pageContent = Master.FindControl("ContentPlaceHolder1");
        Control permssion = pageContent.FindControl("permission");


        for (int i = 1; i < permssion.Controls.Count; i++)
        {
            Control permissionPanel = (Control)permssion.Controls[i];
            SetAllPermissionsOff(permissionPanel);
        }
    }
    private void SetAllPermissionsOff(Control permissionPanel)
    {
        for (int i = 1; i < permissionPanel.Controls.Count; i++)
        {
            Control c = (Control)permissionPanel.Controls[i];
            if (c.ToString() == "ASP.commoncontrols_permissionresource_ascx")
            {
                OnOffAddPermission(false, c);
                OnOffDeletePermission(false, c);
                OnOffViewPermission(false, c);
                OnOffUpdatePermission(false, c);
            }
        }
    }
    private void SetAllPermissionsOn()
    {
        Control pageContent = Master.FindControl("ContentPlaceHolder1");
        Control permssion = pageContent.FindControl("permission");

        for (int i = 1; i < permssion.Controls.Count; i++)
        {
            Control permissionPanel = (Control)permssion.Controls[i];
            SetAllPermissionsOnLoop(permissionPanel);
        }

    }
    private void SetAllPermissionsOnLoop(Control permissionPanel)
    {
        for (int i = 1; i < permissionPanel.Controls.Count; i++)
        {
            Control c = (Control)permissionPanel.Controls[i];
            if (c.ToString() == "ASP.commoncontrols_permissionresource_ascx")
            {
                OnOffAddPermission(true, c);
                OnOffDeletePermission(true, c);
                OnOffViewPermission(true, c);
                OnOffUpdatePermission(true, c);
            }
        }
    }
    protected void chkAdmin_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAdmin.Checked)
        {
            SetAllPermissionsOn();
            EnableControls(false);
        }
        else
        {
            SetAllPermissionsOff();
            EnableControls(true);
        }
    }

    private void EnableControls(bool value)
    {
        Control pageContent = Master.FindControl("ContentPlaceHolder1");
        Control permssion = pageContent.FindControl("permission");

        for (int i = 1; i < permssion.Controls.Count; i++)
        {
            Control permissionPanel = (Control)permssion.Controls[i];
            EnableControlsLoop(permissionPanel, value);
        }

    }
    private void EnableControlsLoop(Control permissionPanel, bool value)
    {
        for (int i = 1; i < permissionPanel.Controls.Count; i++)
        {
            Control c = (Control)permissionPanel.Controls[i];
            if (c.ToString() == "ASP.commoncontrols_permissionresource_ascx")
            {
                CheckBox chkAdd = (CheckBox)c.FindControl("chkAdd");
                chkAdd.Enabled = value;

                CheckBox chkUpdate = (CheckBox)c.FindControl("chkUpdate");
                chkUpdate.Enabled = value;

                CheckBox chkDelete = (CheckBox)c.FindControl("chkDelete");
                chkDelete.Enabled = value;

                CheckBox chkEdit = (CheckBox)c.FindControl("chkView");
                chkEdit.Enabled = value;
            }


        }
    }
}