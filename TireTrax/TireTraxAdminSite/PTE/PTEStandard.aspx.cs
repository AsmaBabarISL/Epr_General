using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.Text;

public partial class PTE_PTEStandard : BasePage
{

    public int CurPageNum
    {
        get
        {
            if (Request.QueryString["p"] != null)
            {
                
                return Conversion.ParseInt(Request.QueryString["p"]);
            }
            else
                
                return 1;
        }
    }

    private int StewardshipId
    {
        get
        {
            int _StewardshipId = UserOrganizationId;

            if (UserOrganizationRoleId != Convert.ToInt32(UserInfo.UserRole.Stewardship))
            {
                _StewardshipId = OrganizationInfo.GetStewardshipByStakeholderId(UserOrganizationId);
            }

            return _StewardshipId;
        }
    }

    int stateID;
    int pteID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] == null) Session["ss"] = null;
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liPTE');", true);
        stateID = OrganizationInfo.getStateId(UserOrganizationId);
        GetPermission(ResourceType.PTE, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        else if (!canAdd)
        {
            gvSetting.DataBind();
            gvSetting.Columns[6].Visible = false;
        }
        else if (!canUpdate)
        {
            gvSetting.DataBind();
            gvSetting.Columns[6].Visible = false;
        }
        if (User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("/");
        }
        // ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liSettings','{0}');", ResourceMgr.GetMessage("PTE")), true);

        if (!IsPostBack)
        {
            Utils.GetLookUpData<DropDownList>(ref ddlStewardship, LookUps.StateIdAndName, LanguageId);
            LoadData();
            if (Session["ss"] != null)
            {
                ddlStewardship.SelectedValue = Conversion.ParseString(Session["ss"]);
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        }

    }
    //private void LoadAllSetting()
    //{

    //        loadGridAndHeaderText();

    //}

    protected void gvSetting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //DropDownList ddlStakeholderTypeFooter = e.Row.FindControl("ddlstakeholderTypeListfooter") as DropDownList;

            ////System.Data.SqlClient.SqlParameter[] prm = new System.Data.SqlClient.SqlParameter[1];
            ////prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
            //Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeFooter, LookUps.LoadStakeholderTypes, LanguageId);

            //int index = ddlStakeholderTypeFooter.Items.IndexOf(ddlStakeholderTypeFooter.Items.FindByText("Stewardship                   "));
            //if (index > -1)
            //{
            //    ddlStakeholderTypeFooter.Items.RemoveAt(index);
            //}
            DropDownList ddlTireSizeListeditor = e.Row.FindControl("ddlTireSizeListfooter") as DropDownList;

            ddlTireSizeListeditor.Items.Clear();

            ddlTireSizeListeditor.DataValueField = "SizeId";
            ddlTireSizeListeditor.DataTextField = "TireSize";

            ddlTireSizeListeditor.DataSource = PTE.GetAllSizes();
            ddlTireSizeListeditor.DataBind();

            ddlTireSizeListeditor.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));
        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == gvSetting.EditIndex)
        {
            //DropDownList ddlStakeholderTypeeditor = e.Row.FindControl("ddlstakeholderTypeListeditor") as DropDownList;

            //System.Data.SqlClient.SqlParameter[] prm = new System.Data.SqlClient.SqlParameter[1];
            //prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
            //Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeeditor, LookUps.LoadStakeholderTypes, LanguageId);
            //int index = ddlStakeholderTypeeditor.Items.IndexOf(ddlStakeholderTypeeditor.Items.FindByText("Stewardship                   "));
            //if (index > -1)
            //{
            //    ddlStakeholderTypeeditor.Items.RemoveAt(index);
            //}
            //HiddenField hdnOrganizationSubTypeId = e.Row.FindControl("hdnOrganizationSubTypeId") as HiddenField;

            //ddlStakeholderTypeeditor.SelectedValue = hdnOrganizationSubTypeId.Value;

            DropDownList ddlTireSizeListeditor = e.Row.FindControl("ddlTireSizeListeditor") as DropDownList;

            ddlTireSizeListeditor.Items.Clear();

            ddlTireSizeListeditor.DataValueField = "SizeId";
            ddlTireSizeListeditor.DataTextField = "TireSize";

            ddlTireSizeListeditor.DataSource = PTE.GetAllSizes();
            ddlTireSizeListeditor.DataBind();

            ddlTireSizeListeditor.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));

            HiddenField hdnSizeId = e.Row.FindControl("hdnSizeId") as HiddenField;

            ddlTireSizeListeditor.SelectedValue = hdnSizeId.Value;
        }
    }
    protected void gvSetting_DataBound(object sender, EventArgs e)
    {

    }
    protected void gvSetting_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSetting.EditIndex = e.NewEditIndex;
        ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPickerRowEditing", "SetDatePicket();", true);
        LoadData();
    }
    protected void gvSetting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {

            pteID = Convert.ToInt32(e.CommandArgument);
            lblErrorMessage.Visible = false;

        }

        else if (e.CommandName == "Delete")
        {
            lblErrorMessage.Visible = false;
            PTEStandards.DeleteSetting(Convert.ToInt32(e.CommandArgument), DateTime.Now, UserInfo.GetCurrentUserInfo().UserId);
            LoadData();
        }
        else if (e.CommandName == "AddMore")
        {
            //    LinkButton lnkbtnAddMore = gvSetting.FooterRow.FindControl("lnkbtnAddMore") as LinkButton;
            LinkButton lnkbtnAddMoreSetting = gvSetting.FooterRow.FindControl("lnkbtnAddMore") as LinkButton;
            LinkButton lnkbtnAddSetting = gvSetting.FooterRow.FindControl("lnkbtnAddSetting") as LinkButton;
            LinkButton lnkbtnCancelSetting = gvSetting.FooterRow.FindControl("lnkbtnCancelSetting") as LinkButton;
            lnkbtnAddSetting.Visible = true;
            lnkbtnAddMoreSetting.Visible = false;
            lnkbtnCancelSetting.Visible = true;

            //    DropDownList dllstakeholdertypefooter = gvSetting.FooterRow.FindControl("ddlstakeholderTypeListfooter") as DropDownList;
            DropDownList dlltiresizefooter = gvSetting.FooterRow.FindControl("ddlTireSizeListfooter") as DropDownList;
            TextBox txtEffectiveDate = gvSetting.FooterRow.FindControl("txteffectivedatefooter") as TextBox;
            TextBox txtExpirtaionDate = gvSetting.FooterRow.FindControl("txtexpirationdatefooter") as TextBox;
            TextBox txtDollarValue = gvSetting.FooterRow.FindControl("txtDollarValuefooter") as TextBox;

            //   dllstakeholdertypefooter.Visible = true;
            dlltiresizefooter.Visible = true;
            txtEffectiveDate.Visible = true;
            txtExpirtaionDate.Visible = true;
            txtDollarValue.Visible = true;
            lblErrorMessage.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPickerFooter", "SetDatePicket();", true);
        }
        else if (e.CommandName == "CancelSetting")
        {
            lblErrorMessage.Visible = false;
            gvSetting.EditIndex = -1;
            LoadData();
        }
        else if (e.CommandName == "Insert")
        {
            DropDownList dllstakeholdertypefooter = gvSetting.FooterRow.FindControl("ddlstakeholderTypeListfooter") as DropDownList;
            DropDownList dlltiresizefooter = gvSetting.FooterRow.FindControl("ddlTireSizeListfooter") as DropDownList;
            TextBox txtEffectiveDate = gvSetting.FooterRow.FindControl("txteffectivedatefooter") as TextBox;
            TextBox txtExpirtaionDate = gvSetting.FooterRow.FindControl("txtexpirationdatefooter") as TextBox;
            TextBox txtDollarValue = gvSetting.FooterRow.FindControl("txtDollarValuefooter") as TextBox;


            PTEStandards objPTE = new PTEStandards();
            //objPTE.OrganizationId = UserOrganizationId;
            objPTE.StateId = Conversion.ParseInt(ddlStewardship.SelectedItem.Value);
            //  objPTE.OrganizationSubTypeId = Convert.ToInt32(dllstakeholdertypefooter.SelectedValue);
            objPTE.SizeId = Convert.ToInt32(dlltiresizefooter.SelectedValue);
            objPTE.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text, System.Globalization.CultureInfo.InvariantCulture);
            objPTE.ExpirationDate = Convert.ToDateTime(txtExpirtaionDate.Text, System.Globalization.CultureInfo.InvariantCulture);
            objPTE.DollarValue = float.Parse(txtDollarValue.Text);
            objPTE.LanguageId = LanguageId;
            objPTE.CreatedDate = DateTime.Now;
            objPTE.CreatedByUserId = UserInfo.GetCurrentUserInfo().UserId;

            int checkBit = PTEStandards.AddSetting(objPTE);
            if (checkBit == 0)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.CssClass = "custom-absolute-alert alert-danger";
                lblErrorMessage.Text = "Record already Exists...";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);


            }

            else
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.CssClass = "custom-absolute-alert alert-success";
                lblErrorMessage.Text = "Record added successfully...";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            }

            LoadData();
        }

    }
    protected void gvSetting_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvSetting_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void gvSetting_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //   DropDownList ddlsatkeholdertypeeditor = (DropDownList)gvSetting.Rows[e.RowIndex].FindControl("ddlstakeholderTypeListeditor");
        DropDownList ddltiresizeeditor = (DropDownList)gvSetting.Rows[e.RowIndex].FindControl("ddlTireSizeListeditor");
        TextBox txteffectdate = (TextBox)gvSetting.Rows[e.RowIndex].FindControl("txteffectivedateeditor");
        TextBox txtexpireddate = (TextBox)gvSetting.Rows[e.RowIndex].FindControl("txtexpirationdateeditor");
        TextBox txtdollarvalue = (TextBox)gvSetting.Rows[e.RowIndex].FindControl("txtDollarValueeditor");

        PTEStandards objPTE = new PTEStandards();
        //objPTE.OrganizationId = UserOrganizationId;
        objPTE.StateId = Conversion.ParseInt(ddlStewardship.SelectedItem.Value); ;
        objPTE.PteStandardId = Convert.ToInt32(gvSetting.DataKeys[e.RowIndex].Values[0].ToString());
        //    objPTE.OrganizationSubTypeId = Convert.ToInt32(ddlsatkeholdertypeeditor.SelectedValue);
        objPTE.SizeId = Convert.ToInt32(ddltiresizeeditor.SelectedValue);
        objPTE.EffectiveDate = Convert.ToDateTime(txteffectdate.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.ExpirationDate = Convert.ToDateTime(txtexpireddate.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.DollarValue = float.Parse(txtdollarvalue.Text);
        objPTE.UpdatedDate = DateTime.Now;
        objPTE.UpdatedByUserId = UserInfo.GetCurrentUserInfo().UserId;

        PTEStandards.UpdateSetting(objPTE);

        gvSetting.EditIndex = -1;
        lblErrorMessage.Visible = true;
        lblErrorMessage.CssClass = "custom-absolute-alert alert-success";
        lblErrorMessage.Text = "Record Updated Successfully";
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);

        LoadData();
    }
    protected void gvSetting_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvSetting_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void gvSetting_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSetting.EditIndex = -1;
        LoadData();

    }


    public void LoadData(int pagenum = -1)
    {
        try
        {
            pageSize = Conversion.ParseInt(ddlPageSize.SelectedValue);
            gvSetting.PageSize = pageSize;



            int stateid;
            if (Session["ss"] != null)
                stateid = Conversion.ParseInt(Session["ss"]);
            else
                stateid = Conversion.ParseInt(ddlStewardship.SelectedValue);
            if (stateid == 0)
                gvSetting.DataSource = null;
            else
            {
                DataSet ds = null;
                if (pagenum > 0)
                    ds = PTEStandards.getSetting(LanguageId, stateid, pagenum, pageSize, out totalRows);
                else
                    ds = PTEStandards.getSetting(LanguageId, stateid, CurPageNum, pageSize, out totalRows);

                gvSetting.DataSource = ds;
            }
            gvSetting.DataBind();
            GridPaging();
            this.TotalItems = totalRows;

            if (gvSetting.Rows.Count == 0)
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
            new SqlLog().InsertSqlLog(0, "PTE.loadGridAndHeaderText", ex);
        }
    }

    protected void GridPaging()
    {
        try
        {
            int startRecordNumber = (CurPageNum - 1) * pageSize + 1;
            int endRecordNumber = startRecordNumber + gvSetting.Rows.Count - 1;

            if (gvSetting.Rows.Count == 0)
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
    protected void lnkbtnAddMore_Click(object sender, EventArgs e)
    {
        lnkbtnAddSetting.Visible = true;
        lnkbtnAddMore.Visible = false;
        lnkbtnCancelSetting.Visible = true;

        // ddlstakeholderTypeListfooter.Visible = true;
        ddlTireSizeListfooter.Visible = true;
        txteffectivedatefooter.Visible = true;
        txtexpirationdatefooter.Visible = true;
        txtDollarValuefooter.Visible = true;

        //System.Data.SqlClient.SqlParameter[] prm;

        //prm = new System.Data.SqlClient.SqlParameter[1];
        //prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
        //     Utils.GetLookUpData<DropDownList>(ref ddlstakeholderTypeListfooter, LookUps.LoadStakeholderTypes, LanguageId);

        ddlTireSizeListfooter.Items.Clear();

        ddlTireSizeListfooter.DataValueField = "SizeId";
        ddlTireSizeListfooter.DataTextField = "TireSize";

        ddlTireSizeListfooter.DataSource = PTE.GetAllSizes();
        ddlTireSizeListfooter.DataBind();

        ddlTireSizeListfooter.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));

        ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPickerFooter", "SetDatePicket();", true);
    }

    protected void lnkbtnAddSetting_Click(object sender, EventArgs e)
    {
        PTEStandards objPTE = new PTEStandards();
        //objPTE.OrganizationId = UserOrganizationId;
        objPTE.StateId = Conversion.ParseInt(ddlStewardship.SelectedItem.Value);
        //   objPTE.OrganizationSubTypeId = Convert.ToInt32(ddlstakeholderTypeListfooter.SelectedValue);
        objPTE.SizeId = Convert.ToInt32(ddlTireSizeListfooter.SelectedValue);
        objPTE.EffectiveDate = Convert.ToDateTime(txteffectivedatefooter.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.ExpirationDate = Convert.ToDateTime(txtexpirationdatefooter.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.DollarValue = float.Parse(txtDollarValuefooter.Text);
        objPTE.LanguageId = LanguageId;
        objPTE.CreatedDate = DateTime.Now;
        objPTE.CreatedByUserId = UserInfo.GetCurrentUserInfo().UserId;

        PTEStandards.AddSetting(objPTE);

        LoadData();
    }

    protected void lnkbtnCancelSetting_Click(object sender, EventArgs e)
    {
        lnkbtnAddSetting.Visible = false;
        lnkbtnAddMore.Visible = true;
        lnkbtnCancelSetting.Visible = false;

        //  ddlstakeholderTypeListfooter.Visible = false;
        ddlTireSizeListfooter.Visible = false;
        txteffectivedatefooter.Visible = false;
        txtexpirationdatefooter.Visible = false;
        txtDollarValuefooter.Visible = false;
    }
    protected void ddlStewardship_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        Session["ss"] = ddlStewardship.SelectedValue;
        LoadData();
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {

        pageSize = Conversion.ParseInt(ddlPageSize.SelectedValue);
        LoadData();
    }

}