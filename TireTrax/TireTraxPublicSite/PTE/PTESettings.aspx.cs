using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class PTE_PTESettings : BasePage
{
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
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMessage.Visible = false;
        pageSize = 10;
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
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liSettings','{0}');", ResourceMgr.GetMessage("PTE")), true);

        if (!IsPostBack)
        {
            //LoadAllSetting();
            LoadProductsToDropdownlist();
        }
        else if (TotalItems > 0)
        {
            pager.DrawPager(CurrentPage, this.TotalItems, pageSize, MaxPagesToShow);

        }

    }
    private void LoadAllSetting()
    {
        try
        {
            if (Convert.ToInt32(ddlOrganizationProducts.SelectedValue) == (int)ProductCategory.Tire)
            {

                gvSetting.Visible = true;
                gvSettingsProduct.Visible = false;
                pnlAddPteProduct.Visible = false;
                loadGridAndHeaderText();

            }
            else
            {
                gvSetting.Visible = false;
                gvSettingsProduct.Visible = true;
                pnlAddPTE.Visible = true;
                loadGridFroProduct();

            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "PTE.LoadAllSetting", ex);
        }
    }

    protected void gvSetting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlStakeholderTypeFooter = e.Row.FindControl("ddlstakeholderTypeListfooter") as DropDownList;

            //System.Data.SqlClient.SqlParameter[] prm = new System.Data.SqlClient.SqlParameter[1];
            //prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
            Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeFooter, LookUps.LoadStakeholderTypes, LanguageId);

            int index = ddlStakeholderTypeFooter.Items.IndexOf(ddlStakeholderTypeFooter.Items.FindByText("Stewardship                   "));
            if (index > -1)
            {
                ddlStakeholderTypeFooter.Items.RemoveAt(index);
            }
            DropDownList ddlTireSizeListeditor = e.Row.FindControl("ddlTireSizeListfooter") as DropDownList;

            ddlTireSizeListeditor.Items.Clear();

            ddlTireSizeListeditor.DataValueField = "SizeId";
            ddlTireSizeListeditor.DataTextField = "ProductSize";

            ddlTireSizeListeditor.DataSource = PTE.GetAllSizes();
            ddlTireSizeListeditor.DataBind();

            ddlTireSizeListeditor.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));
        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == gvSetting.EditIndex)
        {
            DropDownList ddlStakeholderTypeeditor = e.Row.FindControl("ddlstakeholderTypeListeditor") as DropDownList;

            System.Data.SqlClient.SqlParameter[] prm = new System.Data.SqlClient.SqlParameter[1];
            prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
            Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeeditor, LookUps.LoadStakeholderTypes, LanguageId);

            int index = ddlStakeholderTypeeditor.Items.IndexOf(ddlStakeholderTypeeditor.Items.FindByText("Stewardship                   "));
            if (index > -1)
            {
                ddlStakeholderTypeeditor.Items.RemoveAt(index);
            }
            HiddenField hdnOrganizationSubTypeId = e.Row.FindControl("hdnOrganizationSubTypeId") as HiddenField;

            ddlStakeholderTypeeditor.SelectedValue = hdnOrganizationSubTypeId.Value;

            DropDownList ddlTireSizeListeditor = e.Row.FindControl("ddlTireSizeListeditor") as DropDownList;

            ddlTireSizeListeditor.Items.Clear();

            ddlTireSizeListeditor.DataValueField = "SizeId";
            ddlTireSizeListeditor.DataTextField = "ProductSize";

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
        gvSetting.EditIndex = e.NewEditIndex;// +((CurrentPage - 1) * pageSize);
        ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPickerRowEditing", "SetDatePicket();", true);
        loadGridAndHeaderText();
    }
    protected void gvSetting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            //pteID = Convert.ToInt32(e.CommandArgument);
            hdnPTEId.Value = e.CommandArgument.ToString();
            lblErrorMessage.Visible = false;
        }

        else if (e.CommandName == "Delete")
        {
            lblErrorMessage.Visible = false;
            PTE.DeleteSetting(Convert.ToInt32(e.CommandArgument), DateTime.Now, UserInfo.GetCurrentUserInfo().UserId);
            loadGridAndHeaderText();
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

            DropDownList dllstakeholdertypefooter = gvSetting.FooterRow.FindControl("ddlstakeholderTypeListfooter") as DropDownList;
            DropDownList dlltiresizefooter = gvSetting.FooterRow.FindControl("ddlTireSizeListfooter") as DropDownList;
            TextBox txtEffectiveDate = gvSetting.FooterRow.FindControl("txteffectivedatefooter") as TextBox;
            TextBox txtExpirtaionDate = gvSetting.FooterRow.FindControl("txtexpirationdatefooter") as TextBox;
            TextBox txtDollarValue = gvSetting.FooterRow.FindControl("txtDollarValuefooter") as TextBox;

            dllstakeholdertypefooter.Visible = true;
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
            loadGridAndHeaderText();
        }
        else if (e.CommandName == "Insert")
        {
            DropDownList dllstakeholdertypefooter = gvSetting.FooterRow.FindControl("ddlstakeholderTypeListfooter") as DropDownList;
            DropDownList dlltiresizefooter = gvSetting.FooterRow.FindControl("ddlTireSizeListfooter") as DropDownList;
            TextBox txtEffectiveDate = gvSetting.FooterRow.FindControl("txteffectivedatefooter") as TextBox;
            TextBox txtExpirtaionDate = gvSetting.FooterRow.FindControl("txtexpirationdatefooter") as TextBox;
            TextBox txtDollarValue = gvSetting.FooterRow.FindControl("txtDollarValuefooter") as TextBox;

            if (Convert.ToDateTime(txtExpirtaionDate.Text) < Convert.ToDateTime(txtEffectiveDate.Text))
            {
                lblErrorMessage.Text = "Expiry date must be greater than effective date";
                lblErrorMessage.CssClass = "alert-danger custom-absolute-alert";
                lblErrorMessage.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                return;
            }

            if (Convert.ToInt16(txtDollarValue.Text) <= 0)
            {
                lblErrorMessage.Text = "The amount entered should be greater than 0";
                lblErrorMessage.Visible = true;
                return;
            }

            PTE objPTE = new PTE();
            //objPTE.OrganizationId = UserOrganizationId;
            objPTE.StateId = stateID;
            objPTE.OrganizationSubTypeId = Convert.ToInt32(dllstakeholdertypefooter.SelectedValue);
            objPTE.SizeId = Convert.ToInt32(dlltiresizefooter.SelectedValue);
            objPTE.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text, System.Globalization.CultureInfo.InvariantCulture);
            objPTE.ExpirationDate = Convert.ToDateTime(txtExpirtaionDate.Text, System.Globalization.CultureInfo.InvariantCulture);
            objPTE.DollarValue = float.Parse(txtDollarValue.Text);
            objPTE.LanguageId = LanguageId;
            objPTE.CreatedDate = DateTime.Now;
            objPTE.CreatedByUserId = UserInfo.GetCurrentUserInfo().UserId;

            int checkBit = 0;
            if (Convert.ToInt32(ddlOrganizationProducts.SelectedValue) == (int)ProductCategory.Tire)
            {
                checkBit = PTE.AddSetting(objPTE);
            }
            else
            {
                checkBit = PTE.AddSettingProduct(objPTE);
            }

            if (checkBit == 0)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Record already Exists...";
                lblErrorMessage.CssClass = "alert-danger custom-absolute-alert";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                return;
            }

            lblErrorMessage.Text = "Record added successfully...";
            lblErrorMessage.Visible = true;
            lblErrorMessage.CssClass = "alert-success custom-absolute-alert";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);


            loadGridAndHeaderText();
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
        DropDownList ddlsatkeholdertypeeditor = (DropDownList)gvSetting.Rows[e.RowIndex].FindControl("ddlstakeholderTypeListeditor");
        DropDownList ddltiresizeeditor = (DropDownList)gvSetting.Rows[e.RowIndex].FindControl("ddlTireSizeListeditor");
        TextBox txteffectdate = (TextBox)gvSetting.Rows[e.RowIndex].FindControl("txteffectivedateeditor");
        TextBox txtexpireddate = (TextBox)gvSetting.Rows[e.RowIndex].FindControl("txtexpirationdateeditor");
        TextBox txtdollarvalue = (TextBox)gvSetting.Rows[e.RowIndex].FindControl("txtDollarValueeditor");

        string effectDate = txteffectdate.Text;
        string expiryDate = txtexpireddate.Text;

        if (Convert.ToDateTime(txtexpireddate.Text.Trim()) < Convert.ToDateTime(txteffectdate.Text.Trim()))
        {
            lblErrorMessage.Text = "Expiry date must be greater than effective date";
            lblErrorMessage.CssClass = "alert-danger custom-absolute-alert";
            lblErrorMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            return;
        }
        lblErrorMessage.Visible = false;
        PTE objPTE = new PTE();
        //objPTE.OrganizationId = UserOrganizationId;
        objPTE.PteId = Convert.ToInt32(hdnPTEId.Value);
        objPTE.StateId = stateID;
        objPTE.PteId = Convert.ToInt32(hdnPTEId.Value); //Convert.ToInt32(gvSetting.DataKeys[e.RowIndex].Values[0].ToString());
        objPTE.OrganizationSubTypeId = Convert.ToInt32(ddlsatkeholdertypeeditor.SelectedValue);
        objPTE.SizeId = Convert.ToInt32(ddltiresizeeditor.SelectedValue);
        objPTE.EffectiveDate = Convert.ToDateTime(txteffectdate.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.ExpirationDate = Convert.ToDateTime(txtexpireddate.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.DollarValue = float.Parse(txtdollarvalue.Text);
        objPTE.UpdatedDate = DateTime.Now;
        objPTE.UpdatedByUserId = UserInfo.GetCurrentUserInfo().UserId;

        int chkBit = PTE.UpdateSetting(objPTE);

        if (chkBit == 0)
        {
            lblErrorMessage.Text = "Record Already Exists...";
            lblErrorMessage.Visible = true;
            lblErrorMessage.CssClass = "alert-danger custom-absolute-alert";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            return;
        }

        lblErrorMessage.Text = "Record updated successfully...";
        lblErrorMessage.Visible = true;
        lblErrorMessage.CssClass = "alert-success custom-absolute-alert";
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);


        gvSetting.EditIndex = -1;
        loadGridAndHeaderText();
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
        lblErrorMessage.Visible = false;
        loadGridAndHeaderText();

    }
    public void loadGridAndHeaderText()
    {
        int count = 0;

        pageSize = 10;
        DataSet ds = PTE.getSetting(LanguageId, stateID, ViewState["PageId"] == null ? 1 : Convert.ToInt32(ViewState["PageId"]), pageSize, out count);
        gvSetting.DataSource = ds;
        gvSetting.DataBind();
        this.TotalItems = count;
        this.pager.DrawPager(ViewState["PageId"] == null ? 1 : Convert.ToInt32(ViewState["PageId"]), this.TotalItems, pageSize, MaxPagesToShow);
        if (gvSetting.Rows.Count != 0)
        {
            LinkButton lnkBtn = (LinkButton)pager.FindControl("Button_" + CurrentPage.ToString());
            if (lnkBtn != null)
            {
                lnkBtn.Font.Bold = true;
            }

        }
        if (gvSetting.Rows.Count == 0)
        {
            pnlAddPTE.Visible = true;
            pnlAddPteProduct.Visible = false;
        }
        else
        {
            pnlAddPTE.Visible = false;
            pnlAddPteProduct.Visible = false;
        }

    }
    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pager.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);
            this.pageId = CurrentPage;
            ViewState["PageId"] = CurrentPage;
            LoadAllSetting();

        }


        return base.OnBubbleEvent(source, args);
    }
    protected void lnkbtnAddMore_Click(object sender, EventArgs e)
    {
        lnkbtnAddSetting.Visible = true;
        lnkbtnAddMore.Visible = false;
        lnkbtnCancelSetting.Visible = true;

        ddlstakeholderTypeListfooter.Visible = true;
        ddlTireSizeListfooter.Visible = true;
        txteffectivedatefooter.Visible = true;
        txtexpirationdatefooter.Visible = true;
        txtDollarValuefooter.Visible = true;

        //System.Data.SqlClient.SqlParameter[] prm;

        //prm = new System.Data.SqlClient.SqlParameter[1];
        //prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
        Utils.GetLookUpData<DropDownList>(ref ddlstakeholderTypeListfooter, LookUps.LoadStakeholderTypes, LanguageId);

        ddlTireSizeListfooter.Items.Clear();

        ddlTireSizeListfooter.DataValueField = "SizeId";
        ddlTireSizeListfooter.DataTextField = "ProductSize";

        ddlTireSizeListfooter.DataSource = PTE.GetAllSizes();
        ddlTireSizeListfooter.DataBind();

        ddlTireSizeListfooter.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));

        ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPickerFooter", "SetDatePicket();", true);
    }
    protected void lnkbtnAddSetting_Click(object sender, EventArgs e)
    {
        PTE objPTE = new PTE();
        //objPTE.OrganizationId = UserOrganizationId;
        objPTE.StateId = stateID;
        objPTE.OrganizationSubTypeId = Convert.ToInt32(ddlstakeholderTypeListfooter.SelectedValue);
        objPTE.SizeId = Convert.ToInt32(ddlTireSizeListfooter.SelectedValue);
        objPTE.EffectiveDate = Convert.ToDateTime(txteffectivedatefooter.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.ExpirationDate = Convert.ToDateTime(txtexpirationdatefooter.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.DollarValue = float.Parse(txtDollarValuefooter.Text);
        objPTE.LanguageId = LanguageId;
        objPTE.CreatedDate = DateTime.Now;
        objPTE.CreatedByUserId = UserInfo.GetCurrentUserInfo().UserId;

        if (Convert.ToInt32(ddlOrganizationProducts.SelectedValue) == (int)ProductCategory.Tire)
        {
            PTE.AddSetting(objPTE);
        }
        else
        {
            PTE.AddSettingProduct(objPTE);
        }

        loadGridAndHeaderText();
    }
    protected void lnkbtnCancelSetting_Click(object sender, EventArgs e)
    {
        lnkbtnAddSetting.Visible = false;
        lnkbtnAddMore.Visible = true;
        lnkbtnCancelSetting.Visible = false;

        ddlstakeholderTypeListfooter.Visible = false;
        ddlTireSizeListfooter.Visible = false;
        txteffectivedatefooter.Visible = false;
        txtexpirationdatefooter.Visible = false;
        txtDollarValuefooter.Visible = false;
    }


    #region ForProducts
    protected void loadGridFroProduct()
    {
        int count = 0;

        pageSize = 10;
        DataSet ds = null;
        if (dvSubType.Visible)
        {
            ds = PTE.getSettingForProductAndSubtype(LanguageId, stateID, ViewState["PageId"] == null ? 1 : Convert.ToInt32(ViewState["PageId"]), pageSize, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), Convert.ToInt32(ddlProductSubTypes.SelectedValue), out count);
        }
        else
        {
            ds = PTE.getSettingForProduct(LanguageId, stateID, ViewState["PageId"] == null ? 1 : Convert.ToInt32(ViewState["PageId"]), pageSize, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), out count);

        }
        gvSettingsProduct.DataSource = ds;
        gvSettingsProduct.DataBind();

        this.TotalItems = count;
        this.pager.DrawPager(ViewState["PageId"] == null ? 1 : Convert.ToInt32(ViewState["PageId"]), this.TotalItems, pageSize, MaxPagesToShow);
        pnlAddPTE.Visible = false;

        if (gvSettingsProduct.Rows.Count != 0)
        {
            LinkButton lnkBtn = (LinkButton)pager.FindControl("Button_" + CurrentPage.ToString());
            if (lnkBtn != null)
            {
                lnkBtn.Font.Bold = true;
            }

        }
        if (gvSettingsProduct.Rows.Count == 0)
        {

            pnlAddPteProduct.Visible = true;
        }
        else
        {
            pnlAddPteProduct.Visible = false;
        }
        gvSetting.Visible = false;
    }
    protected void lnkAddSettingProduct_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtExpiryDateProduct.Text.Trim()) < Convert.ToDateTime(txtEffectiveDateProduct.Text.Trim()))
        {
            lblErrorMessage.Text = "Expiry date must be greater than effective date";
            lblErrorMessage.CssClass = "alert-danger custom-absolute-alert";
            lblErrorMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            return;
        }
        PTE objPTE = new PTE();
        //objPTE.OrganizationId = UserOrganizationId;
        objPTE.StateId = stateID;
        objPTE.OrganizationSubTypeId = Convert.ToInt32(ddlStakeholderTypeProduct.SelectedValue);
        //objPTE.ProductCategoryId = Convert.ToInt32(ddlProductName.SelectedValue);
        objPTE.ProductCategoryId = Convert.ToInt32(ddlOrganizationProducts.SelectedValue);

        if (dvSubType.Visible)
            objPTE.ProductSubCategoryId = Convert.ToInt32(ddlProductSubTypes.SelectedValue);
        else
            objPTE.ProductSubCategoryId = 0;
        objPTE.SizeId = Convert.ToInt32(ddlProductSize.SelectedValue);
        objPTE.ShapeId = Convert.ToInt32(ddlProductShape.SelectedValue);
        objPTE.MaterialId = Convert.ToInt32(ddlProductMaterial.SelectedValue);
        objPTE.EffectiveDate = Convert.ToDateTime(txtEffectiveDateProduct.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.ExpirationDate = Convert.ToDateTime(txtExpiryDateProduct.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.DollarValue = float.Parse(txtDollarProduct.Text);
        objPTE.LanguageId = LanguageId;
        objPTE.CreatedDate = DateTime.Now;
        objPTE.CreatedByUserId = UserInfo.GetCurrentUserInfo().UserId;
        if (Convert.ToInt32(ddlOrganizationProducts.SelectedValue) == (int)ProductCategory.Tire)
        {
            PTE.AddSetting(objPTE);
        }
        else
        {
            PTE.AddSettingProduct(objPTE);
        }
        loadGridFroProduct();
    }
    protected void lnkCancelSettingProduct_Click(object sender, EventArgs e)
    {
        lnkAddSettingProduct.Visible = false;
        lnkAddMoreProducts.Visible = true;
        lnkCancelSettingProduct.Visible = false;

        ddlStakeholderTypeProduct.Visible = false;
        //ddlProductName.Visible = false;
        //ddlProductSubName.Visible = false;
        ddlProductSize.Visible = false;
        ddlProductShape.Visible = false;
        ddlProductMaterial.Visible = false;
        txtEffectiveDateProduct.Visible = false;
        txtExpiryDateProduct.Visible = false;
        txtDollarProduct.Visible = false;
    }
    protected void gvSettingsProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (dvSubType.Visible)
        {
            int productId = Convert.ToInt32(ddlOrganizationProducts.SelectedValue);
            int SubtypeId = Convert.ToInt32(ddlProductSubTypes.SelectedValue);

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlStakeholderTypeFooter = e.Row.FindControl("ddlstakeholderTypeListfooterProduct") as DropDownList;
                //System.Data.SqlClient.SqlParameter[ ] prm = new System.Data.SqlClient.SqlParameter[1];
                //prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
                Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeFooter, LookUps.LoadStakeholderTypes, LanguageId);

                int index = ddlStakeholderTypeFooter.Items.IndexOf(ddlStakeholderTypeFooter.Items.FindByText("Stewardship                   "));
                if (index > -1)
                {
                    ddlStakeholderTypeFooter.Items.RemoveAt(index);
                }


                DataSet Check = null;
                Check = Product.GetAllSubCategories(Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
                if (Check != null && Check.Tables[0].Rows.Count > 0)
                {
                    DropDownList ddlProductSubNameFooter = e.Row.FindControl("ddlProductSubNameFooter") as DropDownList;
                    ddlProductSubNameFooter.Items.Clear();
                    ddlProductSubNameFooter.DataValueField = "SubProductId";
                    ddlProductSubNameFooter.DataTextField = "SubProductName";
                    Utils.GetLookUpData<DropDownList>(ref ddlProductSubNameFooter, LookUps.ProductSubCategoryName, Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
                }

                DropDownList ddlTireSizeListfooterProduct = e.Row.FindControl("ddlTireSizeListfooterProduct") as DropDownList;
                ddlTireSizeListfooterProduct.Items.Clear();
                ddlTireSizeListfooterProduct.DataValueField = "SizeId";
                ddlTireSizeListfooterProduct.DataTextField = "ProductSize";
                Utils.GetProductProperties<DropDownList>(ref ddlTireSizeListfooterProduct, LookUps.ProductSize, productId, LanguageId, SubtypeId);

                DropDownList ddlProductShapeFooter = e.Row.FindControl("ddlProductShapeFooter") as DropDownList;
                ddlProductShapeFooter.Items.Clear();
                ddlProductShapeFooter.DataValueField = "ShapeId";
                ddlProductShapeFooter.DataTextField = "ProductShape";
                Utils.GetProductProperties<DropDownList>(ref ddlProductShapeFooter, LookUps.ProductShape, productId, LanguageId, SubtypeId);


                DropDownList ddlProductMaterialFooter = e.Row.FindControl("ddlProductMaterialFooter") as DropDownList;
                ddlProductMaterialFooter.Items.Clear();
                ddlProductMaterialFooter.DataValueField = "MaterialId";
                ddlProductMaterialFooter.DataTextField = "ProductMaterial";
                Utils.GetProductProperties<DropDownList>(ref ddlProductMaterialFooter, LookUps.ProductMaterial, productId, LanguageId, SubtypeId);



            }

            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == gvSettingsProduct.EditIndex)
            {
                DropDownList ddlStakeholderTypeeditor = e.Row.FindControl("ddlstakeholderTypeListeditorProduct") as DropDownList;

                System.Data.SqlClient.SqlParameter[] prm = new System.Data.SqlClient.SqlParameter[1];
                prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
                Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeeditor, LookUps.LoadStakeholderTypes, LanguageId);

                int index = ddlStakeholderTypeeditor.Items.IndexOf(ddlStakeholderTypeeditor.Items.FindByText("Stewardship                   "));
                if (index > -1)
                {
                    ddlStakeholderTypeeditor.Items.RemoveAt(index);
                }
                HiddenField hdnOrganizationSubTypeId = e.Row.FindControl("hdnOrganizationSubTypeIdProduct") as HiddenField;

                ddlStakeholderTypeeditor.SelectedValue = hdnOrganizationSubTypeId.Value;


                DropDownList ddlProductSubNamegv = e.Row.FindControl("ddlProductSubNamegv") as DropDownList;
                ddlProductSubNamegv.Items.Clear();
                ddlProductSubNamegv.DataValueField = "SubProductId";
                ddlProductSubNamegv.DataTextField = "SubProductName";
                Utils.GetLookUpData<DropDownList>(ref ddlProductSubNamegv, LookUps.ProductSubCategoryName, Convert.ToInt32(ddlOrganizationProducts.SelectedValue));

                DataSet Check = null;
                Check = Product.GetAllSubCategories(Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
                if (Check != null && Check.Tables[0].Rows.Count > 0)
                {
                    HiddenField SubProductId = e.Row.FindControl("hdnProductSubId") as HiddenField;
                    ddlProductSubNamegv.SelectedValue = SubProductId.Value;
                }




                DropDownList ddlTireSizeListeditorProduct = e.Row.FindControl("ddlTireSizeListeditorProduct") as DropDownList;
                ddlTireSizeListeditorProduct.Items.Clear();

                //ddlTireSizeListeditorProduct.DataValueField = "ProductId";
                //ddlTireSizeListeditorProduct.DataTextField = "ProductName";
                Utils.GetProductProperties<DropDownList>(ref ddlTireSizeListeditorProduct, LookUps.ProductSize, productId, LanguageId, SubtypeId);
                ddlTireSizeListeditorProduct.ClearSelection();
                HiddenField hdnSizeIdd = e.Row.FindControl("hdnSizeIdd") as HiddenField;
                ddlTireSizeListeditorProduct.SelectedValue = hdnSizeIdd.Value;


                DropDownList ddlProductShape = e.Row.FindControl("ddlProductShape") as DropDownList;
                ddlProductShape.Items.Clear();
                ddlProductShape.DataValueField = "ProductId";
                ddlProductShape.DataTextField = "ProductName";
                Utils.GetProductProperties<DropDownList>(ref ddlProductShape, LookUps.ProductShape, productId, LanguageId, SubtypeId);
                HiddenField hdnShapeId = e.Row.FindControl("hdnShapeId") as HiddenField;
                ddlProductShape.SelectedValue = hdnShapeId.Value;

                DropDownList ddlProductMaterial = e.Row.FindControl("ddlProductMaterial") as DropDownList;
                ddlProductMaterial.Items.Clear();
                ddlProductMaterial.DataValueField = "ProductId";
                ddlProductMaterial.DataTextField = "ProductName";
                Utils.GetProductProperties<DropDownList>(ref ddlProductMaterial, LookUps.ProductMaterial, productId, LanguageId, SubtypeId);
                HiddenField hdnMaterialId = e.Row.FindControl("hdnMaterialId") as HiddenField;
                ddlProductMaterial.SelectedValue = hdnMaterialId.Value;



                //ResourceRequiredFieldValidator rfvProductSubName = (ResourceRequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator77");
                //if (Check != null && Check.Tables[0].Rows.Count > 0)
                //{
                //    rfvProductSubName.Enabled = true;
                //}
                //else
                //{
                //    rfvProductSubName.Enabled = false;
                //}



            }
        }
        else
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlStakeholderTypeFooter = e.Row.FindControl("ddlstakeholderTypeListfooterProduct") as DropDownList;

                Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeFooter, LookUps.LoadStakeholderTypes, LanguageId);

                int index = ddlStakeholderTypeFooter.Items.IndexOf(ddlStakeholderTypeFooter.Items.FindByText("Stewardship                   "));
                if (index > -1)
                {
                    ddlStakeholderTypeFooter.Items.RemoveAt(index);
                }



                DataSet Check = null;
                Check = Product.GetAllSubCategories(Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
                if (Check != null && Check.Tables[0].Rows.Count > 0)
                {
                    DropDownList ddlProductSubNameFooter = e.Row.FindControl("ddlProductSubNameFooter") as DropDownList;
                    ddlProductSubNameFooter.Items.Clear();
                    ddlProductSubNameFooter.DataValueField = "SubProductId";
                    ddlProductSubNameFooter.DataTextField = "SubProductName";
                    Utils.GetLookUpData<DropDownList>(ref ddlProductSubNameFooter, LookUps.ProductSubCategoryName, Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
                }

                DropDownList ddlTireSizeListfooterProduct = e.Row.FindControl("ddlTireSizeListfooterProduct") as DropDownList;
                ddlTireSizeListfooterProduct.Items.Clear();
                ddlTireSizeListfooterProduct.DataValueField = "SizeId";
                ddlTireSizeListfooterProduct.DataTextField = "ProductSize";
                Utils.GetProductProperties<DropDownList>(ref ddlTireSizeListfooterProduct, LookUps.ProductSize, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), LanguageId);

                DropDownList ddlProductShapeFooter = e.Row.FindControl("ddlProductShapeFooter") as DropDownList;
                ddlProductShapeFooter.Items.Clear();
                ddlProductShapeFooter.DataValueField = "ShapeId";
                ddlProductShapeFooter.DataTextField = "ProductShape";
                Utils.GetProductProperties<DropDownList>(ref ddlProductShapeFooter, LookUps.ProductShape, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), LanguageId);


                DropDownList ddlProductMaterialFooter = e.Row.FindControl("ddlProductMaterialFooter") as DropDownList;
                ddlProductMaterialFooter.Items.Clear();
                ddlProductMaterialFooter.DataValueField = "MaterialId";
                ddlProductMaterialFooter.DataTextField = "ProductMaterial";
                Utils.GetProductProperties<DropDownList>(ref ddlProductMaterialFooter, LookUps.ProductMaterial, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), LanguageId);



            }

            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == gvSettingsProduct.EditIndex)
            {
                DropDownList ddlStakeholderTypeeditor = e.Row.FindControl("ddlstakeholderTypeListeditorProduct") as DropDownList;

                System.Data.SqlClient.SqlParameter[] prm = new System.Data.SqlClient.SqlParameter[1];
                prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
                Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeeditor, LookUps.LoadStakeholderTypes, LanguageId);

                int index = ddlStakeholderTypeeditor.Items.IndexOf(ddlStakeholderTypeeditor.Items.FindByText("Stewardship                   "));
                if (index > -1)
                {
                    ddlStakeholderTypeeditor.Items.RemoveAt(index);
                }
                HiddenField hdnOrganizationSubTypeId = e.Row.FindControl("hdnOrganizationSubTypeIdProduct") as HiddenField;

                ddlStakeholderTypeeditor.SelectedValue = hdnOrganizationSubTypeId.Value;




                DropDownList ddlProductSubNamegv = e.Row.FindControl("ddlProductSubNamegv") as DropDownList;
                ddlProductSubNamegv.Items.Clear();
                ddlProductSubNamegv.DataValueField = "SubProductId";
                ddlProductSubNamegv.DataTextField = "SubProductName";
                Utils.GetLookUpData<DropDownList>(ref ddlProductSubNamegv, LookUps.ProductSubCategoryName, Convert.ToInt32(ddlOrganizationProducts.SelectedValue));

                DataSet Check = null;
                Check = Product.GetAllSubCategories(Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
                if (Check != null && Check.Tables[0].Rows.Count > 0)
                {
                    HiddenField SubProductId = e.Row.FindControl("hdnProductSubId") as HiddenField;
                    ddlProductSubNamegv.SelectedValue = SubProductId.Value;
                }




                DropDownList ddlTireSizeListeditorProduct = e.Row.FindControl("ddlTireSizeListeditorProduct") as DropDownList;
                ddlTireSizeListeditorProduct.Items.Clear();
                ddlTireSizeListeditorProduct.DataValueField = "ProductId";
                ddlTireSizeListeditorProduct.DataTextField = "ProductName";
                Utils.GetProductProperties<DropDownList>(ref ddlTireSizeListeditorProduct, LookUps.ProductSize, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), LanguageId);
                HiddenField hdnSizeIdd = e.Row.FindControl("hdnSizeIdd") as HiddenField;
                ddlTireSizeListeditorProduct.SelectedValue = hdnSizeIdd.Value;


                DropDownList ddlProductShape = e.Row.FindControl("ddlProductShape") as DropDownList;
                ddlProductShape.Items.Clear();
                ddlProductShape.DataValueField = "ProductId";
                ddlProductShape.DataTextField = "ProductName";
                Utils.GetProductProperties<DropDownList>(ref ddlProductShape, LookUps.ProductShape, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), LanguageId);
                HiddenField hdnShapeId = e.Row.FindControl("hdnShapeId") as HiddenField;
                ddlProductShape.SelectedValue = hdnShapeId.Value;

                DropDownList ddlProductMaterial = e.Row.FindControl("ddlProductMaterial") as DropDownList;
                ddlProductMaterial.Items.Clear();
                ddlProductMaterial.DataValueField = "ProductId";
                ddlProductMaterial.DataTextField = "ProductName";
                Utils.GetProductProperties<DropDownList>(ref ddlProductMaterial, LookUps.ProductMaterial, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), LanguageId);
                HiddenField hdnMaterialId = e.Row.FindControl("hdnMaterialId") as HiddenField;
                ddlProductMaterial.SelectedValue = hdnMaterialId.Value;


                //ResourceRequiredFieldValidator rfvProductSubName = (ResourceRequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator77");
                //if (Check != null && Check.Tables[0].Rows.Count > 0)
                //{
                //    rfvProductSubName.Enabled = true;
                //}
                //else
                //{
                //    rfvProductSubName.Enabled = false;
                //}

            }
        }
    }
    protected void gvSettingsProduct_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSettingsProduct.EditIndex = e.NewEditIndex;
        DataSet Check = null;
        loadGridFroProduct();
        Check = Product.GetAllSubCategories(Convert.ToInt32(ddlOrganizationProducts.SelectedValue));

        if (Check != null && Check.Tables[0].Rows.Count > 0)
        {
            ResourceRequiredFieldValidator rfv = new ResourceRequiredFieldValidator();
            DropDownList ddlProductSubNamegv = gvSettingsProduct.Rows[e.NewEditIndex].FindControl("ddlProductSubNamegv") as DropDownList;
            rfv.ControlToValidate = ddlProductSubNamegv.ID;
            rfv.ValidationGroup = "updateSettingValidationProduct";
            rfv.InitialValue = "0";
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPickerRowEditing", "SetDatePicket();", true);
        //loadGridFroProduct();
    }
    protected void gvSettingsProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            //pteID = Convert.ToInt32(e.CommandArgument);
            hdnPTEId.Value = e.CommandArgument.ToString();
            lblErrorMessage.Visible = false;

        }

        else if (e.CommandName == "Delete")
        {
            lblErrorMessage.Visible = false;
            PTE.DeleteSetting(Convert.ToInt32(e.CommandArgument), DateTime.Now, LoginMemberId);
            //LoadAllSetting();
            loadGridFroProduct();
            Response.Redirect("Settings");
        }
        else if (e.CommandName == "AddMore")
        {
            //    LinkButton lnkbtnAddMore = gvSettings.FooterRow.FindControl("lnkbtnAddMore") as LinkButton;
            LinkButton lnkbtnAddMoreProduct = gvSettingsProduct.FooterRow.FindControl("lnkbtnAddMoreProduct") as LinkButton;
            LinkButton lnkbtnAddSettingProduct = gvSettingsProduct.FooterRow.FindControl("lnkbtnAddSettingProduct") as LinkButton;
            LinkButton lnkbtnCancelSettingProduct = gvSettingsProduct.FooterRow.FindControl("lnkbtnCancelSettingProduct") as LinkButton;
            lnkbtnAddMoreProduct.Visible = false;
            lnkbtnAddSettingProduct.Visible = true;
            lnkbtnCancelSettingProduct.Visible = true;

            DropDownList ddlstakeholderTypeListfooterProduct = gvSettingsProduct.FooterRow.FindControl("ddlstakeholderTypeListfooterProduct") as DropDownList;
            //DropDownList ddlProductNameFooter = gvSettingsProduct.FooterRow.FindControl("ddlProductNameFooter") as DropDownList;
            DropDownList ddlTireSizeListfooterProduct = gvSettingsProduct.FooterRow.FindControl("ddlTireSizeListfooterProduct") as DropDownList;
            DropDownList ddlProductShapeFooter = gvSettingsProduct.FooterRow.FindControl("ddlProductShapeFooter") as DropDownList;
            DropDownList ddlProductMaterialFooter = gvSettingsProduct.FooterRow.FindControl("ddlProductMaterialFooter") as DropDownList;
            TextBox txteffectivedatefooterProduct = gvSettingsProduct.FooterRow.FindControl("txteffectivedatefooterProduct") as TextBox;
            TextBox txtexpirationdatefooterProduct = gvSettingsProduct.FooterRow.FindControl("txtexpirationdatefooterProduct") as TextBox;
            TextBox txtDollarValuefooterProduct = gvSettingsProduct.FooterRow.FindControl("txtDollarValuefooterProduct") as TextBox;
            DropDownList ddlProductSubNameFooter = gvSettingsProduct.FooterRow.FindControl("ddlProductSubNameFooter") as DropDownList;

            ddlstakeholderTypeListfooterProduct.Visible = true;
            //ddlProductNameFooter.Visible = true;
            //ddlProductNameFooter.SelectedValue = CatId.ToString();
            //ddlProductNameFooter.Enabled = false;

            ddlTireSizeListfooterProduct.Visible = true;
            ddlProductShapeFooter.Visible = true;
            ddlProductMaterialFooter.Visible = true;
            txteffectivedatefooterProduct.Visible = true;
            txtexpirationdatefooterProduct.Visible = true;
            txtDollarValuefooterProduct.Visible = true;
            lblErrorMessage.Visible = false;


            DataSet Check = null;
            Check = Product.GetAllSubCategories(Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
            if (Check != null && Check.Tables[0].Rows.Count > 0)
            {

                ddlProductSubNameFooter.Visible = true;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPickerFooter", "SetDatePicket();", true);
        }
        else if (e.CommandName == "CancelSetting")
        {
            lblErrorMessage.Visible = false;
            gvSettingsProduct.EditIndex = -1;
            loadGridFroProduct();
        }
        else if (e.CommandName == "Insert")
        {
            DropDownList ddlstakeholderTypeListfooterProduct = gvSettingsProduct.FooterRow.FindControl("ddlstakeholderTypeListfooterProduct") as DropDownList;
            //DropDownList ddlProductNameFooter = gvSettingsProduct.FooterRow.FindControl("ddlProductNameFooter") as DropDownList;
            DropDownList ddlTireSizeListfooterProduct = gvSettingsProduct.FooterRow.FindControl("ddlTireSizeListfooterProduct") as DropDownList;
            DropDownList ddlProductShapeFooter = gvSettingsProduct.FooterRow.FindControl("ddlProductShapeFooter") as DropDownList;
            DropDownList ddlProductMaterialFooter = gvSettingsProduct.FooterRow.FindControl("ddlProductMaterialFooter") as DropDownList;
            TextBox txteffectivedatefooterProduct = gvSettingsProduct.FooterRow.FindControl("txteffectivedatefooterProduct") as TextBox;
            TextBox txtexpirationdatefooterProduct = gvSettingsProduct.FooterRow.FindControl("txtexpirationdatefooterProduct") as TextBox;
            TextBox txtDollarValuefooterProduct = gvSettingsProduct.FooterRow.FindControl("txtDollarValuefooterProduct") as TextBox;
            DropDownList ddlProductSubNameFooter = null;

            DataSet Check = null;
            Check = Product.GetAllSubCategories(Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
            if (Check != null && Check.Tables[0].Rows.Count > 0)
            {
                ddlProductSubNameFooter = gvSettingsProduct.FooterRow.FindControl("ddlProductSubNameFooter") as DropDownList;
            }

            if (Convert.ToDateTime(txtexpirationdatefooterProduct.Text) < Convert.ToDateTime(txteffectivedatefooterProduct.Text))
            {
                lblErrorMessage.Text = "Expiry date must be greater than effective date";
                lblErrorMessage.CssClass = "alert-danger custom-absolute-alert";
                lblErrorMessage.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                return;
            }

            if (Convert.ToInt16(txtDollarValuefooterProduct.Text) <= 0)
            {
                lblErrorMessage.Text = "The amount entered should be greater than 0";
                lblErrorMessage.Visible = true;
                return;
            }

            PTE objPTE = new PTE();
            //objPTE.OrganizationId = UserOrganizationId;
            objPTE.StateId = stateID;
            objPTE.OrganizationSubTypeId = Convert.ToInt32(ddlstakeholderTypeListfooterProduct.SelectedValue);
            //objPTE.ProductCategoryId = Convert.ToInt32(ddlProductNameFooter.SelectedValue);
            objPTE.ProductCategoryId = Convert.ToInt32(ddlOrganizationProducts.SelectedValue);
            if (Check != null && Check.Tables[0].Rows.Count > 0)
                objPTE.ProductSubCategoryId = Convert.ToInt32(ddlProductSubTypes.SelectedValue);
            else
                objPTE.ProductSubCategoryId = 0;
            objPTE.SizeId = Convert.ToInt32(ddlTireSizeListfooterProduct.SelectedValue);
            objPTE.ShapeId = Convert.ToInt32(ddlProductShapeFooter.SelectedValue);
            objPTE.MaterialId = Convert.ToInt32(ddlProductMaterialFooter.SelectedValue);
            objPTE.EffectiveDate = Convert.ToDateTime(txteffectivedatefooterProduct.Text, System.Globalization.CultureInfo.InvariantCulture);
            objPTE.ExpirationDate = Convert.ToDateTime(txtexpirationdatefooterProduct.Text, System.Globalization.CultureInfo.InvariantCulture);
            objPTE.DollarValue = float.Parse(txtDollarValuefooterProduct.Text);
            objPTE.LanguageId = LanguageId;
            objPTE.CreatedDate = DateTime.Now;
            objPTE.CreatedByUserId = UserInfo.GetCurrentUserInfo().UserId;

            int checkBit = 0;
            if (Convert.ToInt32(ddlOrganizationProducts.SelectedValue) == (int)ProductCategory.Tire)
            {
                checkBit = PTE.AddSetting(objPTE);
            }
            else
            {
                checkBit = PTE.AddSettingProduct(objPTE);
            }

            if (checkBit == 0)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Record already Exists...";
                lblErrorMessage.CssClass = "alert-danger custom-absolute-alert";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                return;
            }

            lblErrorMessage.Text = "Record added successfully...";
            lblErrorMessage.Visible = true;
            lblErrorMessage.CssClass = "alert-success custom-absolute-alert";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);


            loadGridFroProduct();
        }


    }
    protected void gvSettingsProduct_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvSettingsProduct_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void gvSettingsProduct_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList ddlstakeholderTypeListeditorProduct = gvSettingsProduct.Rows[e.RowIndex].FindControl("ddlstakeholderTypeListeditorProduct") as DropDownList;
        //DropDownList ddlProductNamegv = gvSettingsProduct.Rows[e.RowIndex].FindControl("ddlProductNamegv") as DropDownList;
        DropDownList ddlTireSizeListeditorProduct = gvSettingsProduct.Rows[e.RowIndex].FindControl("ddlTireSizeListeditorProduct") as DropDownList;
        DropDownList ddlProductShape = gvSettingsProduct.Rows[e.RowIndex].FindControl("ddlProductShape") as DropDownList;
        DropDownList ddlProductMaterial = gvSettingsProduct.Rows[e.RowIndex].FindControl("ddlProductMaterial") as DropDownList;

        TextBox txteffectivedateeditorProduct = gvSettingsProduct.Rows[e.RowIndex].FindControl("txteffectivedateeditorProduct") as TextBox;
        TextBox txtexpirationdateeditorProduct = gvSettingsProduct.Rows[e.RowIndex].FindControl("txtexpirationdateeditorProduct") as TextBox;
        TextBox txtDollarValueeditorProduct = gvSettingsProduct.Rows[e.RowIndex].FindControl("txtDollarValueeditorProduct") as TextBox;
        DropDownList ddlProductSubNamegv = null;

        DataSet Check = null;
        Check = Product.GetAllSubCategories(Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
        if (Check != null && Check.Tables[0].Rows.Count > 0)
        {
            ddlProductSubNamegv = gvSettingsProduct.Rows[e.RowIndex].FindControl("ddlProductSubNamegv") as DropDownList;
        }

        if (Convert.ToDateTime(txtexpirationdateeditorProduct.Text) < Convert.ToDateTime(txteffectivedateeditorProduct.Text))
        {
            lblErrorMessage.Text = "Expiry date must be greater than effective date";
            lblErrorMessage.CssClass = "alert-danger custom-absolute-alert";
            lblErrorMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            return;
        }

        if (Convert.ToInt16(txtDollarValueeditorProduct.Text) <= 0)
        {
            lblErrorMessage.Text = "The amount entered should be greater than 0";
            lblErrorMessage.Visible = true;
            return;
        }

        PTE objPTE = new PTE();
        //objPTE.OrganizationId = UserOrganizationId;
        objPTE.StateId = stateID;
        objPTE.OrganizationSubTypeId = Convert.ToInt32(ddlstakeholderTypeListeditorProduct.SelectedValue);
        //objPTE.ProductCategoryId = Convert.ToInt32(ddlProductNamegv.SelectedValue);
        objPTE.ProductCategoryId = Convert.ToInt32(ddlOrganizationProducts.SelectedValue);

        if (Check != null && Check.Tables[0].Rows.Count > 0)
            objPTE.ProductSubCategoryId = Convert.ToInt32(ddlProductSubTypes.SelectedValue);
        else
            objPTE.ProductSubCategoryId = 0;
        objPTE.PteId = Convert.ToInt32(hdnPTEId.Value);
        objPTE.SizeId = Convert.ToInt32(ddlTireSizeListeditorProduct.SelectedValue);
        objPTE.ShapeId = Convert.ToInt32(ddlProductShape.SelectedValue);
        objPTE.MaterialId = Convert.ToInt32(ddlProductMaterial.SelectedValue);
        objPTE.EffectiveDate = Convert.ToDateTime(txteffectivedateeditorProduct.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.ExpirationDate = Convert.ToDateTime(txtexpirationdateeditorProduct.Text, System.Globalization.CultureInfo.InvariantCulture);
        objPTE.DollarValue = float.Parse(txtDollarValueeditorProduct.Text);
        objPTE.LanguageId = LanguageId;
        objPTE.CreatedDate = DateTime.Now;
        objPTE.CreatedByUserId = UserInfo.GetCurrentUserInfo().UserId;

        int checkBit = 0;
        if (Convert.ToInt32(ddlOrganizationProducts.SelectedValue) == (int)ProductCategory.Tire)
        {
            checkBit = PTE.AddSetting(objPTE);
        }
        else
        {
            checkBit = PTE.AddSettingProduct(objPTE);
        }

        if (checkBit == 0)
        {
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = "Record already Exists...";
            lblErrorMessage.CssClass = "alert-danger custom-absolute-alert";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            return;
        }

        lblErrorMessage.Text = "Record added successfully...";
        lblErrorMessage.Visible = true;
        lblErrorMessage.CssClass = "alert-success custom-absolute-alert";
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);

        gvSettingsProduct.EditIndex = -1;
        loadGridFroProduct();
    }
    protected void gvSettingsProduct_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSettingsProduct.EditIndex = -1;
        lblErrorMessage.Visible = false;
        loadGridFroProduct();
        gvSetting.Visible = false;
        pnlAddPTE.Visible = false;
    }
    protected void lnkAddMoreProducts_Click(object sender, EventArgs e)
    {
        lnkAddSettingProduct.Visible = true;
        lnkAddMoreProducts.Visible = false;
        lnkCancelSettingProduct.Visible = true;

        ddlStakeholderTypeProduct.Visible = true;
        //ddlProductName.Visible = true;

        ddlProductSize.Visible = true;
        ddlProductShape.Visible = true;
        ddlProductMaterial.Visible = true;
        txtEffectiveDateProduct.Visible = true;
        txtExpiryDateProduct.Visible = true;
        txtDollarProduct.Visible = true;

        Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeProduct, LookUps.LoadStakeholderTypes, LanguageId);

        //ddlProductName.Items.Clear();
        //ddlProductName.DataValueField = "ProductId";
        //ddlProductName.DataTextField = "ProductName";
        //Utils.GetLookUpData<DropDownList>(ref ddlProductName, LookUps.ProductName);
        //ddlProductName.SelectedValue = CatId.ToString();
        //ddlProductName.Enabled = false;

        //ddlProductSubName.Items.Clear();
        //ddlProductSubName.DataValueField = "SubProductId";
        //ddlProductSubName.DataTextField = "SubProductName";
        //Utils.GetLookUpData<DropDownList>(ref ddlProductSubName, LookUps.ProductSubCategoryName, Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
        //DataSet Check = null;
        //Check = Product.GetAllSubCategories(Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
        //if (Check != null && Check.Tables[0].Rows.Count > 0)
        //    ddlProductSubName.Visible = true;
        //else
        //    ddlProductSubName.Visible = false;

        ddlProductSize.Items.Clear();
        ddlProductSize.DataValueField = "SizeId";
        ddlProductSize.DataTextField = "ProductSize";
        //Utils.GetLookUpData<DropDownList>(ref ddlProductSize, LookUps.ProductSize);
        Utils.GetProductProperties<DropDownList>(ref ddlProductSize, LookUps.ProductSize, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), LanguageId);

        ddlProductShape.Items.Clear();
        ddlProductShape.DataValueField = "ShapeId";
        ddlProductShape.DataTextField = "ProductShape";
        //Utils.GetLookUpData<DropDownList>(ref ddlProductShape, LookUps.ProductShape);
        Utils.GetProductProperties<DropDownList>(ref ddlProductShape, LookUps.ProductShape, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), LanguageId);

        ddlProductMaterial.Items.Clear();
        ddlProductMaterial.DataValueField = "MaterialId";
        ddlProductMaterial.DataTextField = "ProductMaterial";
        //Utils.GetLookUpData<DropDownList>(ref ddlProductMaterial, LookUps.ProductMaterial);
        Utils.GetProductProperties<DropDownList>(ref ddlProductMaterial, LookUps.ProductMaterial, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), LanguageId);

        //ResourceRequiredFieldValidator rfvProductSubName = ResourceRequiredFieldValidator10;
        //if (Check != null && Check.Tables[0].Rows.Count > 0)
        //{
        //    rfvProductSubName.Enabled = true;
        //}
        //else
        //{
        //    rfvProductSubName.Enabled = false;
        //}

        ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPickerFooter", "SetDatePicket();", true);
    }
    #endregion



    protected void ddlOrganizationProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["PageId"] = 1;
        CurrentPage = 1;
        if (ddlOrganizationProducts.SelectedIndex == 0)
        {
            dvSubType.Visible = false;
            pnlAddPTE.Visible = false;
            pnlAddPteProduct.Visible = false;
            gvSetting.Visible = false;
            gvSettingsProduct.Visible = false;
            this.pager.DrawPager(0, 0, pageSize, MaxPagesToShow);
            return;
        }
        if (Convert.ToInt32(ddlOrganizationProducts.SelectedValue) == (int)ProductCategory.Tire)
        {

            gvSetting.Visible = true;
            gvSettingsProduct.Visible = false;
            pnlAddPteProduct.Visible = false;
            dvSubType.Visible = false;
            loadGridAndHeaderText();

        }
        else
        {

            if (!lnkAddMoreProducts.Visible)
            {
                lnkAddMoreProducts.Visible = true;
                ddlStakeholderTypeProduct.Visible = false;
                //ddlProductSubName.Visible = false;
                ddlProductSize.Visible = false;
                ddlProductShape.Visible = false;
                ddlProductMaterial.Visible = false;
                txtEffectiveDateProduct.Visible = false;
                txtExpiryDateProduct.Visible = false;
                txtDollarProduct.Visible = false;
                lnkAddSettingProduct.Visible = false;
                lnkCancelSettingProduct.Visible = false;

            }

            DataSet Check = null;
            Check = Product.GetAllSubCategories(Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
            if (Check != null && Check.Tables[0].Rows.Count > 0)
            {
                dvSubType.Visible = true;
                gvSetting.Visible = false;
                gvSettingsProduct.Visible = false;
                pnlAddPTE.Visible = false;
                pnlAddPteProduct.Visible = false;
                Utils.GetLookUpData<DropDownList>(ref ddlProductSubTypes, LookUps.SelectedSubCategory, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), UserOrganizationId);
                this.pager.DrawPager(0, 0, pageSize, MaxPagesToShow);
            }
            else
            {
                gvSetting.Visible = false;
                gvSettingsProduct.Visible = true;
                pnlAddPTE.Visible = true;
                dvSubType.Visible = false;
                loadGridFroProduct();

            }


        }
    }

    protected void LoadProductsToDropdownlist()
    {
        try
        {
            ddlOrganizationProducts.DataSource = Product.GetProductNames();
            ddlOrganizationProducts.DataBind();
            ddlOrganizationProducts.Items.Insert(0, "--Select--");
            pnlAddPTE.Visible = false;
            pnlAddPteProduct.Visible = false;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "PTE.LoadProductsToDropdownlist", ex);
        }

    }
    protected void ddlProductSubTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["PageId"] = 1;
        CurrentPage = 1;
        if (ddlProductSubTypes.SelectedIndex == 0)
        {
            gvSettingsProduct.Visible = false;
            pnlAddPteProduct.Visible = false;
            this.pager.DrawPager(0, 0, pageSize, MaxPagesToShow);
            return;
        }

        int ProductId = Convert.ToInt32(ddlOrganizationProducts.SelectedValue);
        int SubTypeID = Convert.ToInt32(ddlProductSubTypes.SelectedValue);
        int count = 0;
        DataSet CheckPTE = null;
        CheckPTE = PTE.GetDatasetForProductAndSubtype(ProductId, SubTypeID, stateID);
        if (CheckPTE != null && CheckPTE.Tables[0].Rows.Count > 0)
        {
            gvSettingsProduct.Visible = true;
            pnlAddPteProduct.Visible = false;
            gvSettingsProduct.DataSource = PTE.getSettingForProductAndSubtype(LanguageId, stateID, ViewState["PageId"] == null ? 1 : Convert.ToInt32(ViewState["PageId"]), pageSize, ProductId, SubTypeID, out count);
            gvSettingsProduct.DataBind();
            this.TotalItems = count;
            this.pager.DrawPager(ViewState["PageId"] == null ? 1 : Convert.ToInt32(ViewState["PageId"]), this.TotalItems, pageSize, MaxPagesToShow);
            if (gvSettingsProduct.Rows.Count != 0)
            {
                LinkButton lnkBtn = (LinkButton)pager.FindControl("Button_" + CurrentPage.ToString());
                if (lnkBtn != null)
                {
                    lnkBtn.Font.Bold = true;
                }

            }
            foreach (GridViewRow row in gvSettingsProduct.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if ((row.RowState & DataControlRowState.Edit) > 0)
                    {
                        DropDownList ddlTireSizeListeditorProduct = row.FindControl("ddlTireSizeListeditorProduct") as DropDownList;

                        Utils.GetProductProperties<DropDownList>(ref ddlTireSizeListeditorProduct, LookUps.ProductSize, ProductId, LanguageId, SubTypeID);


                        DropDownList ddlProductShape = row.FindControl("ddlProductShape") as DropDownList;


                        Utils.GetProductProperties<DropDownList>(ref ddlProductShape, LookUps.ProductShape, ProductId, LanguageId, SubTypeID);


                        DropDownList ddlProductMaterial = row.FindControl("ddlProductMaterial") as DropDownList;

                        Utils.GetProductProperties<DropDownList>(ref ddlProductMaterial, LookUps.ProductMaterial, ProductId, LanguageId, SubTypeID);

                    }
                }
            }
        }
        else
        {
            gvSettingsProduct.Visible = false;
            pnlAddPteProduct.Visible = true;
        }

    }
}