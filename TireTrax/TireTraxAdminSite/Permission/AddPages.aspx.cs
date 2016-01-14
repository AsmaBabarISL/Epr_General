using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using TireTraxLib.Security;
using System.Data;

public partial class Permission_AddPages : BasePage
{
    int pageid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liPermission','{0}');", ResourceMgr.GetMessage("Permissions")), true);
            //ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liPermission');", true);
            loadPages();
            loadlists();
        }
    }

    protected void gvPages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdfActiveb = (HiddenField)e.Row.FindControl("hdfActivebit");
                hdfactive.Value = hdfActiveb.Value;

                HiddenField hdfparentid = (HiddenField)e.Row.FindControl("hdfparentid");
                if (hdfparentid != null)
                {
                    Label lblDomainName = (Label)e.Row.FindControl("lblDomain");
                    DataSet ds = GroupPages.GetDomainName(Convert.ToInt32(hdfparentid.Value));

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblDomainName.Text = ds.Tables[0].Rows[0]["vchName"].ToString();
                    }
                }

                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlDomain = (DropDownList)e.Row.FindControl("ddlType");
                    DataTable dt = GroupPages.GetDomain();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlDomain.DataSource = dt;
                        ddlDomain.DataTextField = "vchName";
                        ddlDomain.DataValueField = "intResourceId";
                        ddlDomain.DataBind();
                        DataSet ds = GroupPages.GetDomainID(Convert.ToInt32(gvPages.DataKeys[e.Row.RowIndex].Value));
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ddlDomain.SelectedIndex = ddlDomain.Items.IndexOf(ddlDomain.Items.FindByValue(ds.Tables[0].Rows[0]["intparentid"].ToString()));
                        }
                    }
                    ddlDomain.Items.Insert(0, new ListItem("--Select Domain--", "0"));
                    ddlDomain.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Addpages.aspx.gvPages_RowDataBound", ex);
        }
    }

    protected void gvPages_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPages.EditIndex = e.NewEditIndex;
        loadPages();
    }
    protected void gvPages_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit")
            {
                pageid = Convert.ToInt32(e.CommandArgument);
            }

            else if (e.CommandName == "Delete")
            {
                if (Convert.ToBoolean(hdfactive.Value) == true)
                    GroupPages.ActiveDeactiveGroupPage(Convert.ToInt32(e.CommandArgument), false);
                else
                    GroupPages.ActiveDeactiveGroupPage(Convert.ToInt32(e.CommandArgument), true);
                loadPages();
            }
            else if (e.CommandName == "AddMore")
            {
                LinkButton lnkbtnAddMorePage = gvPages.FooterRow.FindControl("lnkbtnAddMore") as LinkButton;
                LinkButton lnkbtnAddPage = gvPages.FooterRow.FindControl("lnkbtnAddPage") as LinkButton;
                LinkButton lnkbtnCancelPage = gvPages.FooterRow.FindControl("lnkbtnCancelPage") as LinkButton;
                lnkbtnAddPage.Visible = true;
                lnkbtnAddMorePage.Visible = false;
                lnkbtnCancelPage.Visible = true;

                TextBox txtPageNamefoot = gvPages.FooterRow.FindControl("txtPageNamefooter") as TextBox;
                DropDownList ddlDomainNamefoot = gvPages.FooterRow.FindControl("ddlTypeFooter") as DropDownList;
                TextBox txtPagePathfoot = gvPages.FooterRow.FindControl("txtPagePathfooter") as TextBox;

                txtPageNamefoot.Visible = true;
                ddlDomainNamefoot.Visible = true;
                txtPagePathfoot.Visible = true;

                ddlDomainNamefoot.Items.Insert(0, new ListItem("--Select Domain--", "0"));
                Utils.GetLookUpData<DropDownList>(ref ddlDomainNamefoot, LookUps.MainResources, true);

            }
            else if (e.CommandName == "CancelPage")
            {
                gvPages.EditIndex = -1;
                loadPages();
            }
            else if (e.CommandName == "Insert")
            {
                TextBox txtPageName = gvPages.FooterRow.FindControl("txtPageNamefooter") as TextBox;
                DropDownList ddlDomainName = gvPages.FooterRow.FindControl("ddlTypeFooter") as DropDownList;
                TextBox txtPagePath = gvPages.FooterRow.FindControl("txtPagePathfooter") as TextBox;

                GroupPages grp = new GroupPages();
                grp.vchName = txtPageName.Text;
                grp.vchPath = txtPagePath.Text;
                grp.intParentID = Convert.ToInt32(ddlDomainName.SelectedItem.Value);
                grp.intResourceID = 0;

                GroupPages.InsertUpdateResources(grp);

                loadPages();
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Addpages.aspx.gvPages_RowCommand", ex);
        }
    }
    protected void gvPages_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvPages_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void gvPages_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            TextBox txtPageName = (TextBox)gvPages.Rows[e.RowIndex].FindControl("txtPageName");
            DropDownList ddlDomainName = gvPages.Rows[e.RowIndex].FindControl("ddlType") as DropDownList;
            TextBox txtPagePath = (TextBox)gvPages.Rows[e.RowIndex].FindControl("txtPagePath");

            GroupPages grp = new GroupPages();
            grp.intResourceID = Convert.ToInt32(gvPages.DataKeys[e.RowIndex].Values[0].ToString());
            grp.vchName = txtPageName.Text + "(" + ddlDomainName.SelectedItem.Text + ")";
            grp.intParentID = Convert.ToInt32(ddlDomainName.SelectedItem.Value);
            grp.vchPath = txtPagePath.Text;

            GroupPages.InsertUpdateResources(grp);

            gvPages.EditIndex = -1;
            loadPages();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Addpages.aspx.gvPages_RowUpdating", ex);
        }
    }
    protected void gvPages_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvPages_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void gvPages_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPages.EditIndex = -1;
        loadPages();
    }

    public void loadPages()
    {
        try
        {
            DataSet ds = GroupPages.GetPages();
            gvPages.DataSource = ds;
            gvPages.DataBind();

            if (gvPages.Rows.Count == 0)
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
            new SqlLog().InsertSqlLog(0, "Addpages.aspx.loadPages", ex);
        }
    }

    protected void lnkbtnAddMore_Click(object sender, EventArgs e)
    {
        lnkbtnAddPage.Visible = true;
        lnkbtnAddMore.Visible = false;
        lnkbtnCancelPage.Visible = true;

        txtPageNamefooter.Visible = true;
        ddlTypeFooter.Visible = true;
        txtPagePathfooter.Visible = true;
        loadlists();
    }

    protected void lnkbtnAddPage_Click(object sender, EventArgs e)
    {
        try
        {
            GroupPages grp = new GroupPages();
            grp.intResourceID = 0;
            grp.vchName = txtPageNamefooter.Text + "(" + ddlTypeFooter.SelectedItem.Text + ")";
            grp.intParentID = Convert.ToInt32(ddlTypeFooter.SelectedItem.Value);
            grp.vchPath = txtPagePathfooter.Text;

            GroupPages.InsertUpdateResources(grp);

            loadPages();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Addpages.aspx.lnkbtnAddPage_Click", ex);
        }
    }

    public void loadlists()
    {
        try
        {
            ddlTypeFooter.Items.Insert(0, new ListItem("--Select Domain--", "0"));
            Utils.GetLookUpData<DropDownList>(ref ddlTypeFooter, LookUps.MainResources, true);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Addpages.aspx.loadlists", ex);
        }
    }

    protected void lnkbtnCancelPage_Click(object sender, EventArgs e)
    {
        lnkbtnAddPage.Visible = false;
        lnkbtnAddMore.Visible = true;
        lnkbtnCancelPage.Visible = false;

        txtPageNamefooter.Visible = false;
        ddlTypeFooter.Visible = false;
        txtPagePathfooter.Visible = false;
    }
}