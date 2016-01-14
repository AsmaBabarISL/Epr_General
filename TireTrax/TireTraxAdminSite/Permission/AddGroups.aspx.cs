using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using TireTraxLib.Security;
using System.Data;

public partial class Permission_AddGroups : BasePage
{
    int groupid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liPermission');", true);
            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liPermission','{0}');", ResourceMgr.GetMessage("Permissions")), true);
            loadGroups();
        }
    }

    protected void gvGroups_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdfActiveb = (HiddenField)e.Row.FindControl("hdfActivebit");
            hdfactive.Value = hdfActiveb.Value;
        }
    }

    protected void gvGroups_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvGroups.EditIndex = e.NewEditIndex;
        loadGroups();
    }
    protected void gvGroups_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit")
            {
                dvGroupsInfo.Visible = false;
                groupid = Convert.ToInt32(e.CommandArgument);
            }

            else if (e.CommandName == "Delete")
            {
                DataSet ds = null;
                ds = Groups.getRolesStatusByGroupId(Conversion.ParseInt(e.CommandArgument));

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dvGroupsInfo.Visible = true;
                    gvGroupsInfo.DataSource = ds;
                    gvGroupsInfo.DataBind();

                }
                else
                {

                    if (Convert.ToBoolean(hdfactive.Value) == true)
                        Groups.ActiveDeactiveGroup(Convert.ToInt32(e.CommandArgument), false);
                    else
                        Groups.ActiveDeactiveGroup(Convert.ToInt32(e.CommandArgument), true);
                    loadGroups();
                }
            }
            else if (e.CommandName == "AddMore")
            {
                dvGroupsInfo.Visible = false;
                LinkButton lnkbtnAddMoreGroup = gvGroups.FooterRow.FindControl("lnkbtnAddMore") as LinkButton;
                LinkButton lnkbtnAddGroup = gvGroups.FooterRow.FindControl("lnkbtnAddGroup") as LinkButton;
                LinkButton lnkbtnCancelGroup = gvGroups.FooterRow.FindControl("lnkbtnCancelGroup") as LinkButton;
                lnkbtnAddGroup.Visible = true;
                lnkbtnAddMoreGroup.Visible = false;
                lnkbtnCancelGroup.Visible = true;

                TextBox txtGroupNamefoot = gvGroups.FooterRow.FindControl("txtGroupNamefooter") as TextBox;

                txtGroupNamefoot.Visible = true;
            }
            else if (e.CommandName == "CancelGroup")
            {
                dvGroupsInfo.Visible = false;
                gvGroups.EditIndex = -1;
                loadGroups();
            }
            else if (e.CommandName == "Insert")
            {
                dvGroupsInfo.Visible = false;
                TextBox txtGroupName = gvGroups.FooterRow.FindControl("txtGroupNamefooter") as TextBox;
                Label lblInfo = gvGroups.FooterRow.FindControl("lblInfoF") as Label;

                Groups grp = new Groups();
                grp.vchName = txtGroupName.Text;
                grp.intGroupID = 0;
                int status = Groups.getGroupNameStatus(txtGroupName.Text);
                if (status == 0)
                {
                    Groups.InsertUpdateGroups(grp);

                    loadGroups();
                }
                else
                {
                    lblInfo.Text = "Group Name Already Exist";
                }
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddGroups.aspx.gvGroups_RowCommand", ex);
        }

    }
    protected void gvGroups_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvGroups_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void gvGroups_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            TextBox txtGroupName = (TextBox)gvGroups.Rows[e.RowIndex].FindControl("txtGroupName");
            Label lblInfo = gvGroups.Rows[e.RowIndex].FindControl("lblInfo") as Label;

            Groups grp = new Groups();
            grp.intGroupID = Convert.ToInt32(gvGroups.DataKeys[e.RowIndex].Values[0].ToString());
            grp.vchName = txtGroupName.Text;

            int status = Groups.getGroupNameStatus(txtGroupName.Text);
            if (status == 0)
            {
                Groups.InsertUpdateGroups(grp);

                gvGroups.EditIndex = -1;
                loadGroups();
            }
            else
            {
                lblInfo.Text = "Group Name Already Exist";
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddGroups.aspx.gvGroups_RowUpdating", ex);
        }
    }
    protected void gvGroups_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvGroups_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void gvGroups_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvGroups.EditIndex = -1;
        loadGroups();
    }

    public void loadGroups()
    {
        try
        {
            DataSet ds = Groups.GetGroups();
            gvGroups.DataSource = ds;
            gvGroups.DataBind();

            if (gvGroups.Rows.Count == 0)
            {
                pnlAddPTE.Visible = true;
            }
            else
            {
                pnlAddPTE.Visible = false;
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddGroups.aspx.loadGroups", ex);
        }
    }

    protected void lnkbtnAddMore_Click(object sender, EventArgs e)
    {
        lnkbtnAddGroup.Visible = true;
        lnkbtnAddMore.Visible = false;
        lnkbtnCancelGroup.Visible = true;

        txtGroupNamefooter.Visible = true;
    }

    protected void lnkbtnAddGroup_Click(object sender, EventArgs e)
    {
        try
        {
            Groups grp = new Groups();
            grp.intGroupID = 0;
            grp.vchName = txtGroupNamefooter.Text;

            int status = Groups.getGroupNameStatus(txtGroupNamefooter.Text);
            if (status == 0)
            {
                Groups.InsertUpdateGroups(grp);

                loadGroups();
            }
            else
            {
                lblInfoF.Text = "Group Name Already Exist";
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddGroups.aspx.lnkbtnAddGroup_Click", ex);
        }
    }

    protected void lnkbtnCancelGroup_Click(object sender, EventArgs e)
    {
        lnkbtnAddGroup.Visible = false;
        lnkbtnAddMore.Visible = true;
        lnkbtnCancelGroup.Visible = false;

        txtGroupNamefooter.Visible = false;
    }
}