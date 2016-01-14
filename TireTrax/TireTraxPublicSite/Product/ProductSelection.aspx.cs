using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class ProductSelection_ProductSelection : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetPermission(ResourceType.ProductSelection, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        if (User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("/");
        }
        if (!IsPostBack)
        {
            lblSuccess.Text = "";
            lblSuccess.Visible = false;
            LoadProducts();
            SelectOnLoad();
            lblErrorOnlyForSubType.Text = string.Empty;
            lblError.Text = string.Empty;
            lblErrorPopup.Text = string.Empty;
            lblErrorForSSMB.Text = string.Empty;
            GetProductsByOrgId();
        }
    }


    protected void LoadProducts()
    {
        try
        {

            Utils.GetLookUpData<DropDownList>(ref ddlProducts, LookUps.ProductType, UserOrganizationId);

            List<ListItem> listLi = new List<ListItem>();

            foreach (ListItem item in ddlProducts.Items)
            {
                if (item.Text.ToString().Trim().ToLower().Contains("tire"))
                {
                    listLi.Add(item);
                }
            }
            if (listLi.Count > 0)
            {
                ddlProducts.Items.Remove(listLi.ElementAt(0));
            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Publicsite ProductSelection.LoadProducts", ex);
        }
    }
    protected void SelectOnLoad()
    {
        try
        {
            DataSet ds = Product.GetSelectedSubCategory(UserOrganizationId, Convert.ToInt32(ddlProducts.SelectedValue));
            if (chkProducts.Items.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (ListItem chkbox in chkProducts.Items)
                {
                    for (int index = 0; index < ds.Tables[0].Rows.Count; index++)
                    {
                        if (chkbox.Text.ToString().Trim().ToLower() == ds.Tables[0].Rows[index][0].ToString().Trim().ToLower())
                        {
                            chkbox.Selected = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ProductSelection SelectOnLoad", ex);
        }
    }
    protected void ClearAllFields()
    {
        txtSubType.Text = string.Empty;
        txtSubDescription.Text = string.Empty;
        txtSize.Text = string.Empty;
        //txtSizeDescription.Text = string.Empty;
        txtShape.Text = string.Empty;
        //txtShapeDescription.Text = string.Empty;
        txtMaterial.Text = string.Empty;
        //txtMaterialDescription.Text = string.Empty;
        txtBrand.Text = string.Empty;
        //txtBrandDescription.Text = string.Empty;
        lblErrorOnlyForSubType.Text = string.Empty;
        lblErrorForSSMB.Text = string.Empty;



    }
    protected void GetProductsByOrgId()
    {
        try
        {

            chkSelectedProducts.DataSource = Product.GetProductNames();
            chkSelectedProducts.DataBind();

            DataSet ds = OrganizationInfo.GetProductCategoryByOrgId(UserOrganizationId);

            if (chkSelectedProducts.Items.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (ListItem chkbox in chkSelectedProducts.Items)
                {
                    for (int index = 0; index < ds.Tables[0].Rows.Count; index++)
                    {
                        if (chkbox.Text.ToString().Trim().ToLower() == ds.Tables[0].Rows[index][1].ToString().Trim().ToLower())
                        {
                            chkbox.Selected = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ProductSelection GetProductsByOrgId", ex);
        }

    }

    #region Popup functions
    protected void lnkAddProduct_Click(object sender, EventArgs e)
    {
        string SubCatIDs = "";
        foreach (ListItem item in chkProducts.Items)
        {
            if (item.Selected)
            {
                SubCatIDs += item.Value + ",";
            }
        }
        if (SubCatIDs.EndsWith(","))
            SubCatIDs = SubCatIDs.TrimEnd(',');

        Product.InsertProductTypes(UserOrganizationId, Convert.ToInt32(ddlProducts.SelectedValue), SubCatIDs);
        lblSuccess.Text = "Products selected successfully!";
        lblSuccess.Visible = true;
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        SelectOnLoad();


    }
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        chkProducts.ClearSelection();
        //Response.Redirect("productselection");
        lblSuccess.Text = "";
        lblSuccess.Visible = false;
    }
    #endregion


    protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblSuccess.Text = "";
            lblSuccess.Visible = false;
            if (lnkUpdateSize.Visible || lnkUpdateShape.Visible || lnkUpdateMaterial.Visible || lnkUpdateBrand.Visible)
            {
                //lnkUpdateSize.Visible = false;
                //lnkCancelSize.Visible = false;
                dvSizeUpdateButtons.Visible = false;
                lnkAddSize.Visible = true;

                //lnkUpdateShape.Visible = false;
                //lnkCancelShape.Visible = false;
                dvShapeUpdateButtons.Visible = false;
                lnkAddShape.Visible = true;

                //lnkUpdateMaterial.Visible = false;
                //lnkCancelMaterial.Visible = false;
                dvMaterialUpdateButtons.Visible = false;
                lnkAddMaterial.Visible = true;

                //lnkUpdateBrand.Visible = false;
                //lnkCancelBrand.Visible = false;
                dvBrandUpdateButtons.Visible = false;
                lnkAddBrand.Visible = true;
            }

            if (ddlProducts.SelectedIndex == 0)
            {
                lblError.Visible = false;
                dvProductProperties.Visible = false;
                divButtons.Visible = false;
                chkProducts.Visible = false;
                return;
            }
            ClearAllFields();
            dvProductProperties.Visible = true;



            DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
            if (ds != null & ds.Tables[0].Rows.Count > 0)
            {
                dvOnlyForSubType.Visible = true;
                Utils.GetLookUpData<DropDownList>(ref ddlSubType, LookUps.SelectedSubCategory, Convert.ToInt32(ddlProducts.SelectedValue), UserOrganizationId, true);
                divSizeShapeMaterialBrand.Visible = false;
                dvOnlyForSubType.Visible = true;

                Utils.GetLookUpData<CheckBoxList>(ref chkProducts, LookUps.SubCategory, Convert.ToInt32(ddlProducts.SelectedValue),UserOrganizationId, false);  //comment if problem
                lblError.Visible = false;  //comment if problem
                divButtons.Visible = true;  //comment if problem
                chkProducts.Visible = true;  //comment if problem
                SelectOnLoad();  //comment if problem


            }
            else
            {
                divSizeShapeMaterialBrand.Visible = true;
                dvOnlyForSubType.Visible = false;
                chkProducts.Visible = false;
                int CatType = Convert.ToInt32(ddlProducts.SelectedValue);
                //int subType = Convert.ToInt32(ddlSubType.SelectedValue);
                Utils.GetProductProperties<ListBox>(ref lstSize, LookUps.ProductSize, CatType, LanguageId);
                Utils.GetProductProperties<ListBox>(ref lstShape, LookUps.ProductShape, CatType, LanguageId);
                Utils.GetProductProperties<ListBox>(ref lstMaterial, LookUps.ProductMaterial, CatType, LanguageId);
                Utils.GetProductProperties<ListBox>(ref lstBrand, LookUps.ProductBrand, CatType, LanguageId);


                lblError.Text = "No SubCategories found against selected product.";  //comment if problem
                lblError.Visible = true;  //comment if problem
                divButtons.Visible = false;  //comment if problem
            }

            //uncomment if problem
            //Utils.GetLookUpData<CheckBoxList>(ref chkProducts, LookUps.SubCategory, Convert.ToInt32(ddlProducts.SelectedValue), false);
            //if (chkProducts == null || chkProducts.Items.Count == 0)
            //{
            //    lblError.Text = "No products found.";
            //    lblError.Visible = true;
            //    divButtons.Visible = false;
            //}
            //else
            //{
            //    lblError.Visible = false;
            //    divButtons.Visible = true;
            //    chkProducts.Visible = true;
            //    SelectOnLoad();
            //}




        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ProductSelection ddlProducts_SelectedIndexChanged", ex);
        }
    }
    protected void lnkAddMoreProductSubType_Click(object sender, EventArgs e)
    {
        dvAddProductSubType.Visible = true;
        lblErrorPopup.Visible = false;
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        DataSet ds = Product.GetProductSubTypeName(Convert.ToInt32(ddlProducts.SelectedValue), txtSubType.Text.Trim());

        if (string.IsNullOrEmpty(txtSubType.Text.Trim()) || string.IsNullOrWhiteSpace(txtSubType.Text.Trim()))
        {
            lblErrorPopup.Text = "Please provide Sub Type Name";
            lblErrorPopup.Visible = true;
            return;
        }


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lblErrorPopup.Text = "Sub Type Name already exists for this product";
            lblErrorPopup.Visible = true;
            return;
        }

        //int SubTypeId = Convert.ToInt32(ddlProducts.SelectedValue);
        if (LookupsManagement.InsertUpdateLookUpSubType(0, txtSubType.Text.Trim(), txtSubDescription.Text.Trim(), DateTime.Now, true, Convert.ToInt32(ddlProducts.SelectedValue)))
        {
            txtSubType.Text = "";
            txtSubDescription.Text = "";
            dvAddProductSubType.Visible = false;
            ddlProducts_SelectedIndexChanged(null, null);
            Utils.GetLookUpData<DropDownList>(ref ddlSubType, LookUps.SubCategory, Convert.ToInt32(ddlProducts.SelectedValue), UserOrganizationId, true);
            ddlSubType.SelectedIndex = 0;
            ClearAllFields();
        }
    }
    protected void lnkbtnCancel_Click(object sender, EventArgs e)
    {
        dvAddProductSubType.Visible = false;
    }
    protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstSize.Items.Clear();
        lstShape.Items.Clear();
        lstMaterial.Items.Clear();
        lstBrand.Items.Clear();
        lblSuccess.Text = "";
        lblSuccess.Visible = false;

        if (ddlSubType.SelectedIndex == 0)
        {
            lblErrorOnlyForSubType.Visible = true;
            lblErrorOnlyForSubType.Text = "Please select the Sub Type first";
            divSizeShapeMaterialBrand.Visible = false;
            return;
        }
        else
        {
            lblErrorOnlyForSubType.Visible = false;
            lblErrorOnlyForSubType.Text = string.Empty;
            divSizeShapeMaterialBrand.Visible = true;

            Utils.GetProductProperties<ListBox>(ref lstSize, LookUps.ProductSize, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
            Utils.GetProductProperties<ListBox>(ref lstShape, LookUps.ProductShape, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
            Utils.GetProductProperties<ListBox>(ref lstMaterial, LookUps.ProductMaterial, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
            Utils.GetProductProperties<ListBox>(ref lstBrand, LookUps.ProductBrand, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));

        }
        ClearAllFields();
    }



    #region Listbox functions
    protected void lnkAddSize_Click(object sender, EventArgs e)
    {
        int result = 0;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ddlSubType.SelectedIndex == 0)
            {
                lblErrorOnlyForSubType.Visible = true;
                lblErrorOnlyForSubType.Text = "Please select the Sub Type first";
                return;
            }
            else
            {
                result = Product.InsertUpdateSize(LanguageId, txtSize.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, Convert.ToInt32(ddlSubType.SelectedValue), 0);
                Utils.GetProductProperties<ListBox>(ref lstSize, LookUps.ProductSize, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
            }
        }
        else
        {
            result = Product.InsertUpdateSize(LanguageId, txtSize.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, 0, 0);
            Utils.GetProductProperties<ListBox>(ref lstSize, LookUps.ProductSize, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId);
        }

        if (result != 0)
        {
            lblSuccess.Text = "Size inserted successfully!";
            lblSuccess.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);

        }
        else
        {
            lblErrorForSSMB.Text = "Size name already exists";//SSMB = Shize Shape Material Brand
            lblErrorForSSMB.Visible = true;
            //ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        txtSize.Text = string.Empty;
    }
    protected void lnkAddShape_Click(object sender, EventArgs e)
    {

        int result = 0;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ddlSubType.SelectedIndex == 0)
            {
                lblErrorOnlyForSubType.Visible = true;
                lblErrorOnlyForSubType.Text = "Please select the Sub Type first";
                return;
            }
            else
            {
                result = Product.InsertUpdateShape(LanguageId, txtShape.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, Convert.ToInt32(ddlSubType.SelectedValue), 0);
                Utils.GetProductProperties<ListBox>(ref lstShape, LookUps.ProductShape, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
            }
        }
        else
        {
            result = Product.InsertUpdateShape(LanguageId, txtShape.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, 0, 0);
            Utils.GetProductProperties<ListBox>(ref lstShape, LookUps.ProductShape, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId);
        }

        if (result != 0)
        {
            lblSuccess.Text = "Shape inserted successfully!";
            lblSuccess.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        else
        {
            lblErrorForSSMB.Text = "Shape name already exists";
            lblErrorForSSMB.Visible = true;
            //ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        txtShape.Text = string.Empty;
    }
    protected void lnkAddMaterial_Click(object sender, EventArgs e)
    {
        int result = 0;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ddlSubType.SelectedIndex == 0)
            {
                lblErrorOnlyForSubType.Visible = true;
                lblErrorOnlyForSubType.Text = "Please select the Sub Type first";
                return;
            }
            else
            {
                result = Product.InsertUpdateMaterial(LanguageId, txtMaterial.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, Convert.ToInt32(ddlSubType.SelectedValue), 0);
                Utils.GetProductProperties<ListBox>(ref lstMaterial, LookUps.ProductMaterial, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
            }
        }
        else
        {
            result = Product.InsertUpdateMaterial(LanguageId, txtMaterial.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, 0, 0);
            Utils.GetProductProperties<ListBox>(ref lstMaterial, LookUps.ProductMaterial, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId);
        }

        if (result != 0)
        {
            lblSuccess.Text = "Material inserted successfully!";
            lblSuccess.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        else
        {
            lblErrorForSSMB.Text = "Material name already exists";
            lblErrorForSSMB.Visible = true;
            //ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        txtMaterial.Text = string.Empty;
    }
    protected void lnkAddBrand_Click(object sender, EventArgs e)
    {
        int result = 0;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ddlSubType.SelectedIndex == 0)
            {
                lblErrorOnlyForSubType.Visible = true;
                lblErrorOnlyForSubType.Text = "Please select the Sub Type first";
                return;
            }
            else
            {
                result = Product.InsertUpdateBrand(LanguageId, txtBrand.Text.Trim(), Conversion.ParseInt(ddlProducts.SelectedValue), DateTime.Now, Conversion.ParseInt(ddlSubType.SelectedValue), 0);
                Utils.GetProductProperties<ListBox>(ref lstBrand, LookUps.ProductBrand, Conversion.ParseInt(ddlProducts.SelectedValue), LanguageId, Conversion.ParseInt(ddlSubType.SelectedValue));
            }
        }
        else
        {
            result = Product.InsertUpdateBrand(LanguageId, txtBrand.Text.Trim(), Conversion.ParseInt(ddlProducts.SelectedValue), DateTime.Now, 0, 0);
            Utils.GetProductProperties<ListBox>(ref lstBrand, LookUps.ProductBrand, Conversion.ParseInt(ddlProducts.SelectedValue), LanguageId);
        }

        if (result != 0)
        {
            lblSuccess.Text = "Brand inserted successfully!";
            lblSuccess.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        else
        {
            lblErrorForSSMB.Text = "Brand name already exists";
            lblErrorForSSMB.Visible = true;
        }
        txtBrand.Text = string.Empty;
    }
    protected void lnkUpdateSize_Click(object sender, EventArgs e)
    {
        int result = 0;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ddlSubType.SelectedIndex == 0)
            {
                lblErrorOnlyForSubType.Visible = true;
                lblErrorOnlyForSubType.Text = "Please select the Sub Type first";
                return;
            }
            else
            {
                result = Product.InsertUpdateSize(LanguageId, txtSize.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(lstSize.SelectedValue));
                Utils.GetProductProperties<ListBox>(ref lstSize, LookUps.ProductSize, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
            }
        }
        else
        {
            result = Product.InsertUpdateSize(LanguageId, txtSize.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, 0, Convert.ToInt32(lstSize.SelectedValue));
            Utils.GetProductProperties<ListBox>(ref lstSize, LookUps.ProductSize, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId);
        }

        if (result != 0)
        {
            lblSuccess.Text = "Size updated successfully!";
            lblSuccess.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        else
        {
            lblErrorForSSMB.Text = "Size name already exists";
            lblErrorForSSMB.Visible = true;
            //ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        txtSize.Text = string.Empty;
        dvSizeUpdateButtons.Visible = false;
        lnkAddSize.Visible = true;

    }
    protected void lnkUpdateShape_Click(object sender, EventArgs e)
    {
        int result = 0;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ddlSubType.SelectedIndex == 0)
            {
                lblErrorOnlyForSubType.Visible = true;
                lblErrorOnlyForSubType.Text = "Please select the Sub Type first";
                return;
            }
            else
            {
                result = Product.InsertUpdateShape(LanguageId, txtShape.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(lstShape.SelectedValue));
                Utils.GetProductProperties<ListBox>(ref lstShape, LookUps.ProductShape, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
            }
        }
        else
        {
            result = Product.InsertUpdateShape(LanguageId, txtShape.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, 0, Convert.ToInt32(lstShape.SelectedValue));
            Utils.GetProductProperties<ListBox>(ref lstShape, LookUps.ProductShape, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId);
        }

        if (result != 0)
        {
            lblSuccess.Text = "Shape inserted successfully!";
            lblSuccess.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        else
        {
            lblErrorForSSMB.Text = "Shape name already exists";
            lblErrorForSSMB.Visible = true;
            //ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        txtShape.Text = string.Empty;
        dvShapeUpdateButtons.Visible = false;
        lnkAddShape.Visible = true;

    }
    protected void lnkUpdateMaterial_Click(object sender, EventArgs e)
    {
        int result = 0;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ddlSubType.SelectedIndex == 0)
            {
                lblErrorOnlyForSubType.Visible = true;
                lblErrorOnlyForSubType.Text = "Please select the Sub Type first";
                return;
            }
            else
            {
                result = Product.InsertUpdateMaterial(LanguageId, txtMaterial.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(lstMaterial.SelectedValue));
                Utils.GetProductProperties<ListBox>(ref lstMaterial, LookUps.ProductMaterial, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
            }
        }
        else
        {
            result = Product.InsertUpdateMaterial(LanguageId, txtMaterial.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, 0, Convert.ToInt32(lstMaterial.SelectedValue));
            Utils.GetProductProperties<ListBox>(ref lstMaterial, LookUps.ProductMaterial, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId);
        }

        if (result != 0)
        {
            lblSuccess.Text = "Material inserted successfully!";
            lblSuccess.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);

        }
        else
        {
            lblErrorForSSMB.Text = "Material name already exists";
            lblErrorForSSMB.Visible = true;
            //ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        txtMaterial.Text = string.Empty;
        dvMaterialUpdateButtons.Visible = false;
        lnkAddMaterial.Visible = true;

    }
    protected void lnkUpdateBrand_Click(object sender, EventArgs e)
    {
        int result = 0;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ddlSubType.SelectedIndex == 0)
            {
                lblErrorOnlyForSubType.Visible = true;
                lblErrorOnlyForSubType.Text = "Please select the Sub Type first";
                return;
            }
            else
            {
                result = Product.InsertUpdateBrand(LanguageId, txtBrand.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(lstBrand.SelectedValue));
                Utils.GetProductProperties<ListBox>(ref lstBrand, LookUps.ProductBrand, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
            }
        }
        else
        {
            result = Product.InsertUpdateBrand(LanguageId, txtBrand.Text.Trim(), Convert.ToInt32(ddlProducts.SelectedValue), DateTime.Now, 0, Convert.ToInt32(lstBrand.SelectedValue));
            Utils.GetProductProperties<ListBox>(ref lstBrand, LookUps.ProductBrand, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId);
        }

        if (result != 0)
        {
            lblSuccess.Text = "Brand inserted successfully!";
            lblSuccess.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        else
        {
            lblErrorForSSMB.Text = "Brand name already exists";
            lblErrorForSSMB.Visible = true;
            //ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        txtBrand.Text = string.Empty;
        dvBrandUpdateButtons.Visible = false;
        lnkAddBrand.Visible = true;
    }
    protected void lstSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSize.Text = lstSize.SelectedItem.Text.ToString();
        lnkAddSize.Visible = false;
        dvSizeUpdateButtons.Visible = true;
    }
    protected void lstShape_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtShape.Text = lstShape.SelectedItem.Text.ToString();
        lnkAddShape.Visible = false;
        dvShapeUpdateButtons.Visible = true;
    }
    protected void lstMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMaterial.Text = lstMaterial.SelectedItem.Text.ToString();
        lnkAddMaterial.Visible = false;
        dvMaterialUpdateButtons.Visible = true;

    }
    protected void lstBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBrand.Text = lstBrand.SelectedItem.Text.ToString();
        lnkAddBrand.Visible = false;
        dvBrandUpdateButtons.Visible = true;
    }
    protected void lnkCancelSize_Click(object sender, EventArgs e)
    {
        txtSize.Text = string.Empty;
        dvSizeUpdateButtons.Visible = false;
        lnkAddSize.Visible = true;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null & ds.Tables[0].Rows.Count > 0)
            Utils.GetProductProperties<ListBox>(ref lstSize, LookUps.ProductSize, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
        else
            Utils.GetProductProperties<ListBox>(ref lstSize, LookUps.ProductSize, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId);

    }
    protected void lnkCancelShape_Click(object sender, EventArgs e)
    {
        txtShape.Text = string.Empty;
        dvShapeUpdateButtons.Visible = false;
        lnkAddShape.Visible = true;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null & ds.Tables[0].Rows.Count > 0)
            Utils.GetProductProperties<ListBox>(ref lstShape, LookUps.ProductShape, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
        else
            Utils.GetProductProperties<ListBox>(ref lstShape, LookUps.ProductShape, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId);
    }
    protected void lnkCancelMaterial_Click(object sender, EventArgs e)
    {
        txtMaterial.Text = string.Empty;
        dvMaterialUpdateButtons.Visible = false;
        lnkAddMaterial.Visible = true;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null & ds.Tables[0].Rows.Count > 0)
            Utils.GetProductProperties<ListBox>(ref lstMaterial, LookUps.ProductMaterial, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
        else
            Utils.GetProductProperties<ListBox>(ref lstMaterial, LookUps.ProductMaterial, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId);
    }
    protected void lnkCancelBrand_Click(object sender, EventArgs e)
    {
        txtBrand.Text = string.Empty;
        dvBrandUpdateButtons.Visible = false;
        lnkAddBrand.Visible = true;
        DataSet ds = Product.GetAllSubCategories(Convert.ToInt32(ddlProducts.SelectedValue));
        if (ds != null & ds.Tables[0].Rows.Count > 0)
            Utils.GetProductProperties<ListBox>(ref lstBrand, LookUps.ProductBrand, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId, Convert.ToInt32(ddlSubType.SelectedValue));
        else
            Utils.GetProductProperties<ListBox>(ref lstBrand, LookUps.ProductBrand, Convert.ToInt32(ddlProducts.SelectedValue), LanguageId);
    }
    #endregion


    protected void lnkCancelForStewardship_Click(object sender, EventArgs e)
    {
        Response.Redirect("productselection");
    }
    protected void lnkAddProductsForStewardship_Click(object sender, EventArgs e)
    {
        string catIDs = "";
        foreach (ListItem item in chkSelectedProducts.Items)
        {
            if (item.Selected)
            {
                catIDs += item.Value + ",";
            }
        }
        catIDs = catIDs.TrimEnd(',');

        int inserted = Product.UpdateOrgProductCategory(UserOrganizationId, catIDs);



        foreach (ListItem item in chkSelectedProducts.Items)
        {
            DataSet Check = Product.GetAllSubCategories(Convert.ToInt32(item.Value));
            if (Check != null && Check.Tables[0].Rows.Count > 0 && item.Selected)
            {
                string SubIds = "";
                foreach (DataRow row in Check.Tables[0].Rows)
                {
                    SubIds += row["SubProductId"] + ",";
                }
                if (SubIds.EndsWith(","))
                    SubIds = SubIds.TrimEnd(',');
                Product.InsertProductTypes(UserOrganizationId, CatId, SubIds);
            }
        }

        if (inserted > 0)
        {
            lblSuccess.Text = "Your Request for new product registration has been sent to Admin.";
            lblSuccess.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        Response.Redirect("productselection");
    }
}