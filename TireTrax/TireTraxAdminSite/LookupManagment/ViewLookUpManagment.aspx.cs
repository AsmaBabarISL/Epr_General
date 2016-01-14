using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class LookupManagment_ViewLookUpManagment : BasePage
{
    string menu = string.Empty;
    string submenu = string.Empty;
    string memberType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        loadLabel();

        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liLookup');", true);
        
        if (!IsPostBack)
        {
            LoadInformation();
            setBreadCrum();
        }
        lbl_msg.Text = string.Empty;

        if (ddllookupstable.SelectedItem.Text.Equals("Member Type"))
        {
            // this.chk_IsCaller.Visible = true;
            this.chk_IsInternet.Visible = false;
        }
        else if (ddllookupstable.SelectedItem.Text.Equals("Lead Source Type"))
        {
            this.chk_IsCaller.Visible = true;
            this.chk_IsInternet.Visible = true;
        }
        else
        {
            this.chk_IsCaller.Visible = false;
            this.chk_IsInternet.Visible = false;
        }
        if (lstvalue.SelectedIndex != -1)
        {
            divadd.Style.Add("display", "none");
            divupdate.Style.Add("display", "inline");
           // divdelete.Style.Add("display", "inline");
            divCancel.Style.Add("display", "inline");
        }


    }



    public void loadLabel()
    {
        lbllookuptable.DataBind();
        lbl_SectionType.DataBind();
        lbl_ComponentType.DataBind();
        lbl_SchoolType.DataBind();
        lbl_LocationType.DataBind();
        lblvalue.DataBind();
        Label2.DataBind();
        Label1.DataBind();


    }


    public void loadHeaderText()
    {
        gvData.HeaderRow.Cells[0].Text = ResourceMgr.GetMessage("Type");
        gvData.HeaderRow.Cells[1].Text = ResourceMgr.GetMessage("Order");


    }



    public void LoadInformation()
    {
        try
        {
            System.Data.SqlClient.SqlParameter[] prm = new System.Data.SqlClient.SqlParameter[1];
            prm[0] = new System.Data.SqlClient.SqlParameter("@LanguageId", LanguageId);
            Utils.GetLookUpData<DropDownList>(ref ddllookupstable, LookUps.LookUp, prm);
            Utils.GetLookUpData<CheckBoxList>(ref chkRoles, LookUps.Role);
            lstvalue.Attributes.Add("onchange", "fill();");
            //btnUpdate.Attributes.Add("onclick", "return ConfirmUpdate();");
            //btnAdd.Attributes.Add("onclick", "return Validate();");
            btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");


        }
        catch (Exception e)
        {
            new SqlLog().InsertSqlLog(0, "Lookup_Management.LoadInformation", e);
        }
    }
    private void HidControl()
    {
        this.lbl_SectionType.Visible = false;
        this.ddl_SectionType.Visible = false;
        lbl_ComponentType.Visible = false;
        this.ddl_ComponentType.Visible = false;
        this.ddl_SchoolType.Visible = false;
        this.lbl_SchoolType.Visible = false;
        this.lbl_LocationType.Visible = false;
        this.ddl_LocationType.Visible = false;
        divadd.Style.Add("display", "inline");
        divupdate.Style.Add("display", "none");
       // divdelete.Style.Add("display", "none");
        divCancel.Style.Add("display", "none");
        pnl_material.Visible = false;
        this.chk_IsCaller.Visible = false;
        this.chk_IsInternet.Visible = false;
        this.chk_OrderList.Visible = false;
        trcommercial.Visible = false;
        this.divOrderList.Style.Add("display", "none");

    }
    protected void ddllookupstable_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            loadListBox("", "", "");
            pnlSubType.Visible = false;
            pnlSubType_Type.Visible = false;
            divadd.Style.Add("display", "inline");
            divupdate.Style.Add("display", "none");
            divdelete.Style.Add("display", "none");
            divCancel.Style.Add("display", "none");
            txtLegalName.Text = "";
            txtdescription.Text = "";
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "LookupManagement.ddllookupstable_SelectedIndexChanged", ex);
        }

       
    }
    public void loadListBox(string table_name, string pk_col, string desc_col)
    {
        lstvalue.DataValueField = "ID";
        lstvalue.DataTextField = "Name";
        lstvalue.Items.Clear();

        DataSet ds = null;
        ds = LookupsManagement.GetLookupsData(Convert.ToInt32(ddllookupstable.SelectedValue), -1);

        lstvalue.DataSource = ds.Tables[0];
        lstvalue.DataBind();
        ddl_SectionType.Visible = false;
        lbl_SectionType.Visible = false;

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        

        if (ddllookupstable.SelectedItem != null && ddllookupstable.SelectedValue != "0")
        {
            LookupsManagement.LookupTypeInsert(Convert.ToInt32(ddllookupstable.SelectedValue), txtLegalName.Text.Trim(), txtdescription.Text.Trim(), DateTime.Now, true);
        }

        loadListBox("", "", "");
        txtLegalName.Text = "";
        txtdescription.Text = "";

        
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        

        if (lstvalue.SelectedItem != null)
        {
            LookupsManagement.LookupTypeUpdate(Convert.ToInt32(lstvalue.SelectedValue), txtLegalName.Text.Trim(), txtdescription.Text.Trim());
        }

        loadListBox("", "", "");
        txtLegalName.Text = "";
        txtdescription.Text = "";

        divadd.Style.Add("display", "inline");
        divupdate.Style.Add("display", "none");
        divdelete.Style.Add("display", "none");
        divCancel.Style.Add("display", "none");

       
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
       

        if (lstvalue.SelectedItem != null)
        {
            LookupsManagement.LookupTypeDelete(Convert.ToInt32(lstvalue.SelectedValue));
        }

        loadListBox("", "", "");
        txtLegalName.Text = "";
        txtdescription.Text = "";

        divadd.Style.Add("display", "inline");
        divupdate.Style.Add("display", "none");
        divdelete.Style.Add("display", "none");
        divCancel.Style.Add("display", "none");

        
    }
    protected void btnSubAdd_Click(object sender, EventArgs e)
    {
        int SubTypeId = !string.IsNullOrEmpty(lstSubType.SelectedValue) ? Convert.ToInt32(lstSubType.SelectedValue) : 0;
        if (LookupsManagement.InsertUpdateLookUpSubType(SubTypeId, txtSubType.Text.Trim(), txtSubDescription.Text.Trim(), DateTime.Now, true, Convert.ToInt32(lstvalue.SelectedValue)))
        {
            txtSubType.Text = "";
            txtSubDescription.Text = "";
            lstvalue_SelectedIndexChanged(null, null);
        }

        
    }
    protected void btnSubDelete_Click(object sender, EventArgs e)
    {


        if (lstSubType.SelectedItem != null)
        {
            LookupsManagement.LookupSubTypeDelete(Convert.ToInt32(lstSubType.SelectedValue));
        }


        txtSubType.Text = "";
        txtSubDescription.Text = "";
        lstvalue_SelectedIndexChanged(null, null);


        divSubAdd.Style.Add("display", "inline");
        divSubUpdate.Style.Add("display", "none");
        divSubDelete.Style.Add("display", "none");
        divSubCancel.Style.Add("display", "none");


    }
    protected void btnSubCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.lstSubType.SelectedIndex = -1;
            txtSubType.Text = "";
            txtSubDescription.Text = "";

            divSubAdd.Style.Add("display", "inline");
            divSubUpdate.Style.Add("display", "none");
            divSubDelete.Style.Add("display", "none");
            divSubCancel.Style.Add("display", "none");
            pnlSubType_Type.Visible = false;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }
    protected void btnSubTypeAdd_Click(object sender, EventArgs e)
    {
        int SubTypeId = !string.IsNullOrEmpty(lstSubType_Type.SelectedValue) ? Convert.ToInt32(lstSubType_Type.SelectedValue) : 0;
        if (LookupsManagement.InsertUpdateLookUpSubType(SubTypeId, txtSubType_Type.Text.Trim(), txtSubType_TypeDes.Text.Trim(), DateTime.Now, true, Convert.ToInt32(lstvalue.SelectedValue), Convert.ToInt32(lstSubType.SelectedValue)))
        {
            txtSubType_Type.Text = "";
            txtSubType_TypeDes.Text = "";
            lstSubType_SelectedIndexChanged(null, null);
        }


    }
    protected void btnSubTypeCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.lstSubType_Type.SelectedIndex = -1;
            txtSubType_Type.Text = "";
            txtSubType_TypeDes.Text = "";
            
            divAddSubType_Type.Style.Add("display", "inline");
            divUpdateSubType_Type.Style.Add("display", "none");
            divSubDelete.Style.Add("display", "none");
            divCancelSubType_Type.Style.Add("display", "none");

            
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }
    protected void btnSubTypeDelete_Click(object sender, EventArgs e)
    {


        if (lstSubType_Type.SelectedItem != null)
        {
            LookupsManagement.LookupSubTypeDelete(Convert.ToInt32(lstSubType_Type.SelectedValue));
        }


        txtSubType_Type.Text = "";
        txtSubType_TypeDes.Text = "";
        lstSubType_SelectedIndexChanged(null, null);
        
        divAddSubType_Type.Style.Add("display", "inline");
        divUpdateSubType_Type.Style.Add("display", "none");
        divDeleteSubType_Type.Style.Add("display", "none");
        divCancelSubType_Type.Style.Add("display", "none");
        

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.lstvalue.SelectedIndex = -1;
            this.txtdescription.Text = "";
            txtLegalName.Text = "";
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            divadd.Style.Add("display", "inline");
            divupdate.Style.Add("display", "none");
            divdelete.Style.Add("display", "none");
            divCancel.Style.Add("display", "none");

            pnlSubType_Type.Visible = false;
            pnlSubType.Visible = false;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }
    protected void ddl_SectionType_SelectedIndexChanged(object sender, EventArgs e)
    {

        loadListBox("", "", "");

        
    }
    protected void ddl_ComponentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.txtdescription.Text = "Add New";
            txtLegalName.Text = "Add New";
            divadd.Style.Add("display", "inline");
            divupdate.Style.Add("display", "none");
            //divdelete.Style.Add("display", "none");
            divCancel.Style.Add("display", "none");
            int index = ddllookupstable.SelectedItem.Value.IndexOf("$#$");
            string table_name = ddllookupstable.SelectedItem.Value.Substring(0, index);
            string pk_col = ddllookupstable.SelectedItem.Value.Substring(index + 3, (ddllookupstable.SelectedItem.Value.LastIndexOf("$#$") - index - 3));
            string desc_col = ddllookupstable.SelectedItem.Value.Substring(ddllookupstable.SelectedItem.Value.LastIndexOf("$#$") + 3);
            memberType = ddllookupstable.SelectedItem.Value.Substring(ddllookupstable.SelectedItem.Value.LastIndexOf("_") + 1);
            desc_col = desc_col.Replace("_" + memberType, "");

            loadListBox(table_name, pk_col, desc_col);
            lblerror.Text = string.Empty;


        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }
    protected void lstvalue_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImageButton1.Visible = true;
        ImageButton2.Visible = true;
        txtSubType.Text = string.Empty;
        txtSubDescription.Text = string.Empty;
        DataTable dt = LookupsManagement.GetLookupSubTypeData(Convert.ToInt32(lstvalue.SelectedValue),0);
        if (dt != null && dt.Rows.Count > 0)
        {
            pnlSubType.Visible = true;
            lstSubType.DataSource = dt;
            lstSubType.DataTextField = "LookupSubTypeName";
            lstSubType.DataValueField = "LookupSubTypeID";
            lstSubType.DataBind();
            lstSubType.Attributes.Add("onchange", "fillSubType();");
        }
        else
        {
            lstSubType.DataSource = string.Empty;
            lstSubType.DataBind();
            pnlSubType.Visible = true;
            
        }
        divdelete.Style.Add("display", "inline");

        divSubAdd.Style.Add("display", "inline");
        divSubCancel.Style.Add("display", "none");
        divSubDelete.Style.Add("display", "none");
        divSubUpdate.Style.Add("display", "none");

        pnlSubType_Type.Visible = false; 
        
    }

    protected void lstSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = LookupsManagement.GetLookupSubTypeData(Convert.ToInt32(lstvalue.SelectedValue), Convert.ToInt32(lstSubType.SelectedValue));
        if (dt != null && dt.Rows.Count > 0)
        {
            pnlSubType_Type.Visible = true;
            lstSubType_Type.DataSource = dt;
            lstSubType_Type.DataTextField = "LookupSubTypeName";
            lstSubType_Type.DataValueField = "LookupSubTypeID";
            lstSubType_Type.DataBind();
            lstSubType_Type.Attributes.Add("onchange", "fillTypeSubType();");
        }
        else
        {
            lstSubType_Type.DataSource = string.Empty;
            lstSubType_Type.DataBind();
            pnlSubType_Type.Visible = true;
        }
        txtSubType_Type.Text = string.Empty;
        txtSubType_TypeDes.Text = string.Empty;
        divSubAdd.Style.Add("display", "none");
        divSubUpdate.Style.Add("display", "inline");
        divSubDelete.Style.Add("display", "inline");
        divSubCancel.Style.Add("display", "inline");
        divdelete.Style.Add("display", "none");
    }

    protected void lstSubType_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = LookupsManagement.GetLookupSubTypeData(Convert.ToInt32(lstSubType.SelectedValue), Convert.ToInt32(lstSubType_Type.SelectedValue));
        if (dt != null && dt.Rows.Count > 0)
        {
            pnlSubTypeSubType_Type.Visible = true;
            lstSubTypeSubType_Type.DataSource = dt;
            lstSubTypeSubType_Type.DataTextField = "LookupSubTypeName";
            lstSubTypeSubType_Type.DataValueField = "LookupSubTypeID";
            lstSubTypeSubType_Type.DataBind();
            lstSubTypeSubType_Type.Attributes.Add("onchange", "fillTypeSubType();");
        }
        else
        {
            lstSubTypeSubType_Type.DataSource = string.Empty;
            lstSubTypeSubType_Type.DataBind();
            pnlSubTypeSubType_Type.Visible = true;
        }
        txtSubTypeSubType_Type.Text = string.Empty;
        txtSubTypeSubType_TypeDes.Text = string.Empty;
        divAddSubTypeSubType.Style.Add("display", "none");
        divUpdateSubTypeSubType.Style.Add("display", "inline");
        divDeleteSubTypeSubType.Style.Add("display", "inline");
        divCancelSubTypeSubType.Style.Add("display", "inline");
        divSubDelete.Style.Add("display", "none");
    }
    
    private void setBreadCrum()
    {
       
    }
    protected void ddl_SchoolType_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddl_LocationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        

    }
    protected void ddlMaterial2_SelectedIndexChanged(object sender, EventArgs e)
    {


        

    }
    protected void txtOrder_TextChanged(object sender, EventArgs e)
    {
        
    }
    private void LoadOrderList()
    {
       
    }
    protected void chk_OrderList_CheckedChanged(object sender, EventArgs e)
    {
        
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        int lookupsubtypeid = Int32.Parse(lstvalue.SelectedValue);
        int lookupsubtypeid2 = (lstvalue.SelectedIndex);
        int i = 0, uppervalue = 0;
        foreach (ListItem item in lstvalue.Items)
        {
            i++;
            if (i == lookupsubtypeid2)
            {
                uppervalue = Convert.ToInt32(item.Value);
                break;
            }

        }


        LookupsManagement.GetLookupSwapedData(lookupsubtypeid, uppervalue);

        lstvalue.DataValueField = "ID";
        lstvalue.DataTextField = "Name";
        lstvalue.Items.Clear();

        DataSet ds = null;
        ds = LookupsManagement.GetLookupsData(Convert.ToInt32(ddllookupstable.SelectedValue), -1);

        lstvalue.DataSource = ds.Tables[0];
        lstvalue.DataBind();
        try
        {
            this.lstvalue.SelectedIndex = -1;
            this.txtdescription.Text = "";
            txtLegalName.Text = "";

            divadd.Style.Add("display", "inline");
            divupdate.Style.Add("display", "none");
            //divdelete.Style.Add("display", "none");
            divCancel.Style.Add("display", "none");
            //ImageButton1.Visible = false;
            //ImageButton2.Visible = false;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        int lookupsubtypeid = Int32.Parse(lstvalue.SelectedValue);
        int lookupsubtypeid2 = (lstvalue.SelectedIndex) + 2;
        int i = 0, uppervalue = 0;
        foreach (ListItem item in lstvalue.Items)
        {
            i++;
            if (i == lookupsubtypeid2)
            {
                uppervalue = Convert.ToInt32(item.Value);
                break;
            }

        }


        LookupsManagement.GetLookupSwapedData(lookupsubtypeid, uppervalue);

        lstvalue.DataValueField = "ID";
        lstvalue.DataTextField = "Name";
        lstvalue.Items.Clear();

        DataSet ds = null;
        ds = LookupsManagement.GetLookupsData(Convert.ToInt32(ddllookupstable.SelectedValue), -1);

        lstvalue.DataSource = ds.Tables[0];
        lstvalue.DataBind();

        try
        {
            this.lstvalue.SelectedIndex = -1;
            this.txtdescription.Text = "";
            txtLegalName.Text = "";

            divadd.Style.Add("display", "inline");
            divupdate.Style.Add("display", "none");
           // divdelete.Style.Add("display", "none");
            divCancel.Style.Add("display", "none");
            //ImageButton1.Visible = false;
            //ImageButton2.Visible = false;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }

    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        int lookupsubtypeid = Int32.Parse(lstSubType.SelectedValue);
        int lookupsubtypeid2 = (lstSubType.SelectedIndex);
        int i = 0, uppervalue = 0;
        foreach (ListItem item in lstSubType.Items)
        {
            i++;
            if (i == lookupsubtypeid2)
            {
                uppervalue = Convert.ToInt32(item.Value);
                break;
            }

        }


        LookupsManagement.GetLookupSubTypeSwapedData(lookupsubtypeid, uppervalue);

        lstvalue_SelectedIndexChanged(null,null);

        //lstSubType.DataValueField = "LookupSubTypeID";
        //lstSubType.DataTextField = "LookupSubTypeName";
        //lstSubType.Items.Clear();

        //DataSet ds = null;
        //ds = LookupsManagement.GetLookupsData(Convert.ToInt32(ddllookupstable.SelectedValue), -1);

        //lstvalue.DataSource = ds.Tables[0];
        //lstvalue.DataBind();
        try
        {
            this.lstSubType.SelectedIndex = -1;
            this.txtSubDescription.Text = "";
            txtSubType.Text = "";

            //divadd.Style.Add("display", "inline");
            //divupdate.Style.Add("display", "none");
            ////divdelete.Style.Add("display", "none");
            //divCancel.Style.Add("display", "none");
            //ImageButton1.Visible = false;
            //ImageButton2.Visible = false;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {

        int lookupsubtypeid = Int32.Parse(lstSubType.SelectedValue);
        int lookupsubtypeid2 = (lstSubType.SelectedIndex) + 2;
        int i = 0, uppervalue = 0;
        foreach (ListItem item in lstSubType.Items)
        {
            i++;
            if (i == lookupsubtypeid2)
            {
                uppervalue = Convert.ToInt32(item.Value);
                break;
            }

        }


        LookupsManagement.GetLookupSubTypeSwapedData(lookupsubtypeid, uppervalue);
        lstvalue_SelectedIndexChanged(null, null);

        //lstvalue.DataValueField = "ID";
        //lstvalue.DataTextField = "Name";
        //lstvalue.Items.Clear();

        //DataSet ds = null;
        //ds = LookupsManagement.GetLookupsData(Convert.ToInt32(ddllookupstable.SelectedValue), -1);

        //lstvalue.DataSource = ds.Tables[0];
        //lstvalue.DataBind();

        try
        {
            this.lstSubType.SelectedIndex = -1;
            this.txtSubDescription.Text = "";
            txtSubType.Text = "";

            //divadd.Style.Add("display", "inline");
            //divupdate.Style.Add("display", "none");
            //// divdelete.Style.Add("display", "none");
            //divCancel.Style.Add("display", "none");
            //ImageButton1.Visible = false;
            //ImageButton2.Visible = false;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int lookupsubtypeid = Int32.Parse(lstSubType_Type.SelectedValue);
        int lookupsubtypeid2 = (lstSubType_Type.SelectedIndex);
        int i = 0, uppervalue = 0;
        foreach (ListItem item in lstSubType_Type.Items)
        {
            i++;
            if (i == lookupsubtypeid2)
            {
                uppervalue = Convert.ToInt32(item.Value);
                break;
            }

        }

        LookupsManagement.GetLookupSubTypeSwapedData(lookupsubtypeid, uppervalue);
        lstSubType_SelectedIndexChanged(null, null);

        //lstSubType.DataValueField = "LookupSubTypeID";
        //lstSubType.DataTextField = "LookupSubTypeName";
        //lstSubType.Items.Clear();

        //DataSet ds = null;
        //ds = LookupsManagement.GetLookupsData(Convert.ToInt32(ddllookupstable.SelectedValue), -1);

        //lstvalue.DataSource = ds.Tables[0];
        //lstvalue.DataBind();
        try
        {
            this.lstSubType_Type.SelectedIndex = -1;
            this.txtSubType_TypeDes.Text = "";
            txtSubType_Type.Text = "";

            //divadd.Style.Add("display", "inline");
            //divupdate.Style.Add("display", "none");
            ////divdelete.Style.Add("display", "none");
            //divCancel.Style.Add("display", "none");
            //ImageButton1.Visible = false;
            //ImageButton2.Visible = false;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        int lookupsubtypeid = Int32.Parse(lstSubType_Type.SelectedValue);
        int lookupsubtypeid2 = (lstSubType_Type.SelectedIndex) + 2;
        int i = 0, uppervalue = 0;
        foreach (ListItem item in lstSubType_Type.Items)
        {
            i++;
            if (i == lookupsubtypeid2)
            {
                uppervalue = Convert.ToInt32(item.Value);
                break;
            }

        }


        LookupsManagement.GetLookupSubTypeSwapedData(lookupsubtypeid, uppervalue);
        lstSubType_SelectedIndexChanged(null, null);

        //lstvalue.DataValueField = "ID";
        //lstvalue.DataTextField = "Name";
        //lstvalue.Items.Clear();

        //DataSet ds = null;
        //ds = LookupsManagement.GetLookupsData(Convert.ToInt32(ddllookupstable.SelectedValue), -1);

        //lstvalue.DataSource = ds.Tables[0];
        //lstvalue.DataBind();

        try
        {
            this.lstSubType_Type.SelectedIndex = -1;
            this.txtSubType_TypeDes.Text = "";
            txtSubType_Type.Text = "";

            //divadd.Style.Add("display", "inline");
            //divupdate.Style.Add("display", "none");
            //// divdelete.Style.Add("display", "none");
            //divCancel.Style.Add("display", "none");
            //ImageButton1.Visible = false;
            //ImageButton2.Visible = false;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }


    protected void lnSubTypeSubType_Click(object sender, EventArgs e)
    {
        int lookupsubtypeid = Int32.Parse(lstSubTypeSubType_Type.SelectedValue);
        int lookupsubtypeid2 = (lstSubTypeSubType_Type.SelectedIndex);
        int i = 0, uppervalue = 0;
        foreach (ListItem item in lstSubTypeSubType_Type.Items)
        {
            i++;
            if (i == lookupsubtypeid2)
            {
                uppervalue = Convert.ToInt32(item.Value);
                break;
            }

        }
        LookupsManagement.GetLookupSubTypeSwapedData(lookupsubtypeid, uppervalue);
        lstSubType_Type_SelectedIndexChanged(null, null);
        try
        {
            this.lstSubType_Type.SelectedIndex = -1;
            this.txtSubType_TypeDes.Text = "";
            txtSubType_Type.Text = "";
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }

    protected void lnSubTypeSubType2_Click(object sender, EventArgs e)
    {

        int lookupsubtypeid = Int32.Parse(lstSubType_Type.SelectedValue);
        int lookupsubtypeid2 = (lstSubType_Type.SelectedIndex) + 2;
        int i = 0, uppervalue = 0;
        foreach (ListItem item in lstSubType_Type.Items)
        {
            i++;
            if (i == lookupsubtypeid2)
            {
                uppervalue = Convert.ToInt32(item.Value);
                break;
            }

        }
        LookupsManagement.GetLookupSubTypeSwapedData(lookupsubtypeid, uppervalue);
        lstSubType_Type_SelectedIndexChanged(null, null);
        try
        {
            this.lstSubType_Type.SelectedIndex = -1;
            this.txtSubType_TypeDes.Text = "";
            txtSubType_Type.Text = "";
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }


    protected void btnSubTypeSubTypeAdd_Click(object sender, EventArgs e)
    {
        int SubTypeId = !string.IsNullOrEmpty(lstSubTypeSubType_Type.SelectedValue) ? Convert.ToInt32(lstSubTypeSubType_Type.SelectedValue) : 0;
        if (LookupsManagement.InsertUpdateLookUpSubType(SubTypeId, txtSubType_Type.Text.Trim(), txtSubType_TypeDes.Text.Trim(), DateTime.Now, true, Convert.ToInt32(lstvalue.SelectedValue), Convert.ToInt32(lstSubType.SelectedValue)))
        {
            txtSubType_Type.Text = "";
            txtSubType_TypeDes.Text = "";
            lstSubType_Type_SelectedIndexChanged(null, null);
        }


    }
    protected void btnSubTypeSubTypeCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.lstSubTypeSubType_Type.SelectedIndex = -1;
            txtSubType_Type.Text = "";
            txtSubType_TypeDes.Text = "";

            divAddSubTypeSubType.Style.Add("display", "inline");
            divUpdateSubTypeSubType.Style.Add("display", "none");
            divDeleteSubTypeSubType.Style.Add("display", "none");
            divCancelSubTypeSubType.Style.Add("display", "none");

            
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "", ex);
        }
    }
    protected void btnSubTypeSubTypeDelete_Click(object sender, EventArgs e)
    {


        if (lstSubType_Type.SelectedItem != null)
        {
            LookupsManagement.LookupSubTypeDelete(Convert.ToInt32(lstSubTypeSubType_Type.SelectedValue));
        }


        txtSubType_Type.Text = "";
        txtSubType_TypeDes.Text = "";
        lstSubType_Type_SelectedIndexChanged(null, null);

        divAddSubTypeSubType.Style.Add("display", "inline");
        divUpdateSubTypeSubType.Style.Add("display", "none");
        divDeleteSubTypeSubType.Style.Add("display", "none");
        divCancelSubTypeSubType.Style.Add("display", "none");
        

    }

}