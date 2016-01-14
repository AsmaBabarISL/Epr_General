using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TireTraxLib;
using System.Globalization;
using System.Text;
public partial class PTE_PTESettings : BasePage
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
    int pteId;
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liPTE');", true);
        if (!IsPostBack)
        {
            //LoadAllSetting();
            LoadProductsToDropdownlist();
            if (CurPageNum > 1)
            {
                if (Session["ProductId"] != null)
                {
                    ddlOrganizationProducts.ClearSelection();
                    ddlOrganizationProducts.Items.FindByValue(Session["ProductId"].ToString()).Selected = true;
                }
                loadGridAndHeaderText();
            }
        //    pageSize = 20;
          //  loadGridAndHeaderText();
            ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        }
        //if (TotalItemsR > 0)
        //{

        //    pager.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
        //}
        
    }
    protected void GridPaging()
    {
        try
        {
            int startRecordNumber = (CurPageNum - 1) * pageSize + 1;

            int endRecordNumber = 0;
            if (Convert.ToInt32(ddlOrganizationProducts.SelectedValue) == (int)ProductCategory.Tire)
            {
                endRecordNumber = startRecordNumber + gvSetting.Rows.Count - 1;
                if (gvSetting.Rows.Count == 0)
                    startRecordNumber = 0;
            }
            else
            {
                endRecordNumber = startRecordNumber + gvSettingsProduct.Rows.Count - 1;
                if (gvSettingsProduct.Rows.Count == 0)
                    startRecordNumber = 0;
            }

           

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
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        pageSize = Conversion.ParseInt(ddlPageSize.SelectedValue);
        LoadAllSetting();
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
            //lblErrorMessage.Visible = true;
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
            DropDownList ddlStewardshipsfooter = e.Row.FindControl("ddlStewardshipsfooter") as DropDownList;
            DropDownList ddlStakeholderTypeFooter = e.Row.FindControl("ddlstakeholderTypeListfooter") as DropDownList;

           

            //System.Data.SqlClient.SqlParameter[] prm = new System.Data.SqlClient.SqlParameter[1];
            //prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
            //Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeFooter, LookUps.OrganizationSubType, prm);

            Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeFooter, LookUps.LoadStakeholderTypes, LanguageId);
            Utils.GetLookUpData<DropDownList>(ref ddlStewardshipsfooter, LookUps.StewardshipTypes, CountryIDByLanguageId);
        
            DropDownList ddlTireSizeListeditor = e.Row.FindControl("ddlTireSizeListfooter") as DropDownList;

           // LoadStewardships(ddlStewardshipsfooter);

            ddlTireSizeListeditor.Items.Clear();
            ddlTireSizeListeditor.DataValueField = "SizeId";
            ddlTireSizeListeditor.DataTextField = "ProductSize";
            ddlTireSizeListeditor.DataSource = PTE.GetAllSizes();
            ddlTireSizeListeditor.DataBind();
            ddlTireSizeListeditor.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));
        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == gvSetting.EditIndex)
        {
            DropDownList ddlStewardships = e.Row.FindControl("ddlStewardships") as DropDownList;
            DropDownList ddlStakeholderTypeeditor = e.Row.FindControl("ddlstakeholderTypeListeditor") as DropDownList;
            DropDownList ddlTireSizeListeditor = e.Row.FindControl("ddlTireSizeListeditor") as DropDownList;

            HiddenField hdnOrganizationSubTypeId = e.Row.FindControl("hdnOrganizationSubTypeId") as HiddenField;
            HiddenField hdnStateId = e.Row.FindControl("hdnStateId") as HiddenField;
            HiddenField hdnSizeId = e.Row.FindControl("hdnSizeId") as HiddenField;

            //LoadStewardships(ddlStewardships);
            //ddlStewardships.SelectedValue = hdnOrganizationId.Value;
            Utils.GetLookUpData<DropDownList>(ref ddlStewardships, LookUps.StewardshipTypes, CountryIDByLanguageId);
            ddlStewardships.SelectedValue = hdnStateId.Value;
            //System.Data.SqlClient.SqlParameter[] prm = new System.Data.SqlClient.SqlParameter[1];
            //prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
            //Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeeditor, LookUps.OrganizationSubType, prm);

            Utils.GetLookUpData<DropDownList>(ref ddlStakeholderTypeeditor, LookUps.LoadStakeholderTypes, LanguageId);
            ddlStakeholderTypeeditor.SelectedValue = hdnOrganizationSubTypeId.Value;

            ddlTireSizeListeditor.Items.Clear();
            ddlTireSizeListeditor.DataValueField = "SizeId";
            ddlTireSizeListeditor.DataTextField = "ProductSize";
            ddlTireSizeListeditor.DataSource = PTE.GetAllSizes();
            ddlTireSizeListeditor.DataBind();
            ddlTireSizeListeditor.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));
            ddlTireSizeListeditor.SelectedValue = hdnSizeId.Value;
        }
    }

    private void LoadStewardships(DropDownList ddl)
    {
        ddl.Items.Clear();
        DataSet ds = OrganizationInfo.GetStewardshipByCountryID(CountryIDByLanguageId);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ListItem item = new ListItem();
                item.Value = dr["OrganizationId"].ToString();
                item.Text = dr["StateName"].ToString();
                ddl.Items.Add(item);
            }
        }
        ddl.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));

        ds.Dispose();
        ds = null;
    }


    protected void gvSetting_DataBound(object sender, EventArgs e)
    {

    }
    protected void gvSetting_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSetting.EditIndex = e.NewEditIndex;
        ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPickerRowEditing", "SetDatePicket();", true);
        loadGridAndHeaderText();
    }
    protected void gvSetting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            pteId = Convert.ToInt32(e.CommandArgument);
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
            //       LinkButton lnkbtnAddMore = gvSetting.FooterRow.FindControl("lnkbtnAddMore") as LinkButton;
            LinkButton lnkbtnAddMoreSetting = gvSetting.FooterRow.FindControl("lnkbtnAddMore") as LinkButton;
            LinkButton lnkbtnAddSetting = gvSetting.FooterRow.FindControl("lnkbtnAddSetting") as LinkButton;
            LinkButton lnkbtnCancelSetting = gvSetting.FooterRow.FindControl("lnkbtnCancelSetting") as LinkButton;
            lnkbtnAddSetting.Visible = true;
            lnkbtnAddMoreSetting.Visible = false;
            lnkbtnCancelSetting.Visible = true;

            DropDownList ddlStewardshipsfooter = gvSetting.FooterRow.FindControl("ddlStewardshipsfooter") as DropDownList;
            DropDownList dllstakeholdertypefooter = gvSetting.FooterRow.FindControl("ddlstakeholderTypeListfooter") as DropDownList;
            DropDownList dlltiresizefooter = gvSetting.FooterRow.FindControl("ddlTireSizeListfooter") as DropDownList;
            TextBox txtEffectiveDate = gvSetting.FooterRow.FindControl("txteffectivedatefooter") as TextBox;
            TextBox txtExpirtaionDate = gvSetting.FooterRow.FindControl("txtexpirationdatefooter") as TextBox;
            TextBox txtDollarValue = gvSetting.FooterRow.FindControl("txtDollarValuefooter") as TextBox;

            ddlStewardshipsfooter.Visible = true;
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
            DropDownList ddlStewardshipsfooter = gvSetting.FooterRow.FindControl("ddlStewardshipsfooter") as DropDownList;
            DropDownList dllstakeholdertypefooter = gvSetting.FooterRow.FindControl("ddlstakeholderTypeListfooter") as DropDownList;
            DropDownList dlltiresizefooter = gvSetting.FooterRow.FindControl("ddlTireSizeListfooter") as DropDownList;
            TextBox txtEffectiveDate = gvSetting.FooterRow.FindControl("txteffectivedatefooter") as TextBox;
            TextBox txtExpirtaionDate = gvSetting.FooterRow.FindControl("txtexpirationdatefooter") as TextBox;
            TextBox txtDollarValue = gvSetting.FooterRow.FindControl("txtDollarValuefooter") as TextBox;

            if (Conversion.ParseDateTime(txtEffectiveDate.Text) > Conversion.ParseDateTime(txtExpirtaionDate.Text))
            {
                lblErrorMessage.Text = "Expiry date must be greater than effective date";
                lblErrorMessage.Visible = true;
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                lblErrorMessage.Text = "";
                lblErrorMessage.Visible = false;
            }

            PTE objPTE = new PTE();
            objPTE.StateId = Convert.ToInt32(ddlStewardshipsfooter.SelectedItem.Value);
            objPTE.OrganizationSubTypeId = Convert.ToInt32(dllstakeholdertypefooter.SelectedItem.Value);
            objPTE.SizeId = Convert.ToInt32(dlltiresizefooter.SelectedValue);
            objPTE.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text, CultureInfo.InvariantCulture);
            objPTE.ExpirationDate = Convert.ToDateTime(txtExpirtaionDate.Text, CultureInfo.InvariantCulture);
            objPTE.DollarValue = float.Parse(txtDollarValue.Text);
            objPTE.LanguageId = LanguageId;
            objPTE.CreatedDate = DateTime.Now;
            objPTE.CreatedByUserId = UserInfo.GetCurrentUserInfo().UserId;
           // objPTE.OrganizationId = UserOrganizationId;

           int checkBit = PTE.AddSetting(objPTE);
           if (checkBit == 0)
           {
               lblErrorMessage.Visible = true;  
               lblErrorMessage.Text = "Record Already Exists...";
           }
           
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
        DropDownList ddlStewardships = gvSetting.Rows[e.RowIndex].FindControl("ddlStewardships") as DropDownList;
        DropDownList ddlsatkeholdertypeeditor = (DropDownList)gvSetting.Rows[e.RowIndex].FindControl("ddlstakeholderTypeListeditor");
        DropDownList ddltiresizeeditor = (DropDownList)gvSetting.Rows[e.RowIndex].FindControl("ddlTireSizeListeditor");
        TextBox txteffectdate = (TextBox)gvSetting.Rows[e.RowIndex].FindControl("txteffectivedateeditor");
        TextBox txtexpireddate = (TextBox)gvSetting.Rows[e.RowIndex].FindControl("txtexpirationdateeditor");
        TextBox txtdollarvalue = (TextBox)gvSetting.Rows[e.RowIndex].FindControl("txtDollarValueeditor");

        if (Conversion.ParseDateTime(txtexpireddate.Text) < Conversion.ParseDateTime(txteffectdate.Text) )
        {
            lblErrorMessage.Text = "Expiry date must be greater than effective date";
            lblErrorMessage.Visible = true;
            return;
        }
        else
        {
            lblErrorMessage.Text = "";
            lblErrorMessage.Visible = false;
        }

        PTE objPTE = new PTE();
        objPTE.PteId = Convert.ToInt32(gvSetting.DataKeys[e.RowIndex].Values[0].ToString());
        objPTE.StateId = Convert.ToInt32(ddlStewardships.SelectedItem.Value);
        objPTE.OrganizationSubTypeId = Convert.ToInt32(ddlsatkeholdertypeeditor.SelectedValue);
        objPTE.SizeId = Convert.ToInt32(ddltiresizeeditor.SelectedValue);
        objPTE.EffectiveDate = Convert.ToDateTime(txteffectdate.Text, CultureInfo.InvariantCulture);
        objPTE.ExpirationDate = Convert.ToDateTime(txtexpireddate.Text, CultureInfo.InvariantCulture);
        objPTE.DollarValue = float.Parse(txtdollarvalue.Text);
        objPTE.UpdatedDate = DateTime.Now;
        objPTE.UpdatedByUserId = UserInfo.GetCurrentUserInfo().UserId;

        PTE.UpdateSetting(objPTE);

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
     //   LoadAllSetting();

    }


    public void loadGridAndHeaderText()
    {
        try
        {
            int count = 0;
            //pageSize = 20;
            pageSize = Conversion.ParseInt(ddlPageSize.SelectedValue);
            gvSetting.PageSize = pageSize;
          
            pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            gvSetting.PageSize = pageSize;
            DataSet ds = PTE.getSetting(LanguageId, 0, CurPageNum, pageSize,out totalRows);
            gvSetting.DataSource = ds;
            gvSetting.DataBind();
            GridPaging();
            this.TotalItems = totalRows;
           // this.pager.DrawPager(Conversion.ParseInt(pageId), this.TotalItems, pageSize, MaxPagesToShow);
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
            new SqlLog().InsertSqlLog(currentUserInfo.UserId, "PTESetting.LoadGridAndHeaderText()", ex);
        }
    }

    protected void lnkbtnAddMore_Click(object sender, EventArgs e)
    {
        lnkbtnAddSetting.Visible = true;
        lnkbtnAddMore.Visible = false;
        lnkbtnCancelSetting.Visible = true;

        ddlStewardshipsfooter.Visible = true;
        ddlstakeholderTypeListfooter.Visible = true;
        ddlTireSizeListfooter.Visible = true;
        txteffectivedatefooter.Visible = true;
        txtexpirationdatefooter.Visible = true;
        txtDollarValuefooter.Visible = true;

        //System.Data.SqlClient.SqlParameter[] prm;
        //prm = new System.Data.SqlClient.SqlParameter[1];
        //prm[0] = new System.Data.SqlClient.SqlParameter("@OrganizationTypeId", Convert.ToInt32(OrganizationType.Stakeholder));
        //Utils.GetLookUpData<DropDownList>(ref ddlstakeholderTypeListfooter, LookUps.OrganizationSubType, prm);
        Utils.GetLookUpData<DropDownList>(ref ddlstakeholderTypeListfooter, LookUps.LoadStakeholderTypes, LanguageId);
        Utils.GetLookUpData<DropDownList>(ref ddlStewardshipsfooter, LookUps.StewardshipTypes, CountryIDByLanguageId);
        //LoadStewardships(ddlStewardshipsfooter);
       
            

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
        objPTE.StateId = Convert.ToInt32(ddlStewardshipsfooter.SelectedItem.Value);
        objPTE.OrganizationSubTypeId = Convert.ToInt32(ddlstakeholderTypeListfooter.SelectedItem.Value);
        objPTE.SizeId = Convert.ToInt32(ddlTireSizeListfooter.SelectedValue);
        objPTE.EffectiveDate = Convert.ToDateTime(txteffectivedatefooter.Text, CultureInfo.InvariantCulture);
        objPTE.ExpirationDate = Convert.ToDateTime(txtexpirationdatefooter.Text, CultureInfo.InvariantCulture);
        objPTE.DollarValue = float.Parse(txtDollarValuefooter.Text);
        objPTE.LanguageId = LanguageId;
        objPTE.CreatedDate = DateTime.Now;
        objPTE.CreatedByUserId = UserInfo.GetCurrentUserInfo().UserId;

        PTE.AddSetting(objPTE);

        LoadAllSetting();
    }

    protected void lnkbtnCancelSetting_Click(object sender, EventArgs e)
    {
        lnkbtnAddSetting.Visible = false;
        lnkbtnAddMore.Visible = true;
        lnkbtnCancelSetting.Visible = false;

        ddlStewardshipsfooter.Visible = false;
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
            ds = PTE.getSettingForProductAndSubtype(LanguageId, 0, pageId, pageSize, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), Convert.ToInt32(ddlProductSubTypes.SelectedValue), out count);
        }
        else
        {
            ds = PTE.getSettingForProduct(LanguageId, 0, pageId, pageSize, Convert.ToInt32(ddlOrganizationProducts.SelectedValue), out count);
        }
        totalRows = count;
        gvSettingsProduct.Visible = true;
        gvSettingsProduct.DataSource = ds;
        gvSettingsProduct.DataBind();

        this.TotalItems = count;
        //this.pager.DrawPager(pageId, this.TotalItems, pageSize, MaxPagesToShow);
        pnlAddPTE.Visible = false;
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
        objPTE.StateId = 0;
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
        ddlStateProduct.Visible = false;
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
                DropDownList ddlStewardshipsfooterProduct = e.Row.FindControl("ddlStewardshipsfooterProduct") as DropDownList;
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


                Utils.GetLookUpData<DropDownList>(ref ddlStewardshipsfooterProduct, LookUps.StewardshipTypes, CountryIDByLanguageId);

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

                DropDownList ddlStewardshipsProduct = e.Row.FindControl("ddlStewardshipsProduct") as DropDownList;
                Utils.GetLookUpData<DropDownList>(ref ddlStewardshipsProduct, LookUps.StewardshipTypes, CountryIDByLanguageId);
                HiddenField hdnStateIdProduct = e.Row.FindControl("hdnStateIdProduct") as HiddenField;
                ddlStewardshipsProduct.SelectedValue = hdnStateIdProduct.Value;

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
                DropDownList ddlStewardshipsfooterProduct = e.Row.FindControl("ddlStewardshipsfooterProduct") as DropDownList;

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

                Utils.GetLookUpData<DropDownList>(ref ddlStewardshipsfooterProduct, LookUps.StewardshipTypes, CountryIDByLanguageId);

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

                DropDownList ddlStewardshipsProduct = e.Row.FindControl("ddlStewardshipsProduct") as DropDownList;
                Utils.GetLookUpData<DropDownList>(ref ddlStewardshipsProduct, LookUps.StewardshipTypes, CountryIDByLanguageId);
                HiddenField hdnStateIdProduct = e.Row.FindControl("hdnStateIdProduct") as HiddenField;
                ddlStewardshipsProduct.SelectedValue = hdnStateIdProduct.Value;

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

            DropDownList ddlStewardshipsfooterProduct = gvSettingsProduct.FooterRow.FindControl("ddlStewardshipsfooterProduct") as DropDownList;
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
            ddlStewardshipsfooterProduct.Visible = true;
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
            DropDownList ddlStewardshipsfooterProduct = gvSettingsProduct.FooterRow.FindControl("ddlStewardshipsfooterProduct") as DropDownList;
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
            objPTE.StateId = Convert.ToInt32(ddlStewardshipsfooterProduct.SelectedValue);
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
        DropDownList ddlStewardshipsProduct = gvSettingsProduct.Rows[e.RowIndex].FindControl("ddlStewardshipsProduct") as DropDownList;
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
        objPTE.StateId = Convert.ToInt32(ddlStewardshipsProduct.SelectedValue);
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
        ddlStateProduct.Visible = true;
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

        Utils.GetLookUpData<DropDownList>(ref ddlStateProduct, LookUps.StewardshipTypes, CountryIDByLanguageId);
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


        if (ddlOrganizationProducts.SelectedIndex == 0)
        {
            dvSubType.Visible = false;
            pnlAddPTE.Visible = false;
            pnlAddPteProduct.Visible = false;
            gvSetting.Visible = false;
            gvSettingsProduct.Visible = false;
            return;
        }
        Session["ProductId"] = ddlOrganizationProducts.SelectedValue;
        //CurPageNum = 1;
        if (Convert.ToInt32(ddlOrganizationProducts.SelectedValue) == (int)ProductCategory.Tire)
        {

            gvSetting.Visible = true;
            gvSettingsProduct.Visible = false;
            pnlAddPteProduct.Visible = false;
            dvSubType.Visible = false;
            loadGridAndHeaderText();
            GridPaging();

        }
        else
        {

            if (!lnkAddMoreProducts.Visible)
            {
                ddlStateProduct.Visible = false;
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

                Utils.GetLookUpData<DropDownList>(ref ddlProductSubTypes, LookUps.ProductSubCategoryName, Convert.ToInt32(ddlOrganizationProducts.SelectedValue));
            }
            else
            {
                gvSetting.Visible = false;
                gvSettingsProduct.Visible = true;
                pnlAddPTE.Visible = false;
                dvSubType.Visible = false;
                loadGridFroProduct();
                
            }
            GridPaging();

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
        if (ddlProductSubTypes.SelectedIndex == 0)
        {
            gvSettingsProduct.Visible = false;
            pnlAddPteProduct.Visible = false;
            return;
        }

        int ProductId = Convert.ToInt32(ddlOrganizationProducts.SelectedValue);
        int SubTypeID = Convert.ToInt32(ddlProductSubTypes.SelectedValue);
        int count = 0;
        DataSet CheckPTE = null;
        CheckPTE = PTE.GetDatasetForProductAndSubtype(ProductId, SubTypeID, 0);
        if (CheckPTE != null && CheckPTE.Tables[0].Rows.Count > 0)
        {
            gvSettingsProduct.Visible = true;
            pnlAddPteProduct.Visible = false;
            gvSettingsProduct.DataSource = PTE.getSettingForProductAndSubtype(LanguageId, 0, pageId, pageSize, ProductId, SubTypeID, out count);
            gvSettingsProduct.DataBind();
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