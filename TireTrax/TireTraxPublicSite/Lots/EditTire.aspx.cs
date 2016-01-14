using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text;

public partial class Lots_EditTire : BasePage
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


    private void Load_AllAdminInventory(int pageNo)
    {
        gvAdminInventory.PageSize = pageSize;
        int LotID = Convert.ToInt32(Request.QueryString["OpenLotId"]);

        int count = 0;
        if (CatId == (int)ProductCategory.Tire)
        {
            string dotnumber = txtInventoryDotNumber.Text.Trim().Replace(" ", string.Empty);
            string barcode = txtBarcode.Text.Trim();
            string brandname = txtBrandName.Text.Trim();
            string tiresize = txtTireSize.Text.Trim();
            DateTime frmDate = string.IsNullOrEmpty(txtFromDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text);
            DateTime toDate = string.IsNullOrEmpty(txttodate.Text) ? DateTime.MinValue : Convert.ToDateTime(txttodate.Text);
            string username = txtUserName.Text.Trim();
            string lanename = txtLaneName.Text.Trim();
            string spacename = txtSpaceName.Text.Trim();
            string lotnumber = txtLotNumber.Text.Trim();
            gvAdminInventory.DataSource = Tire.inventoryTireByLotId(LotID, pageNo, pageSize, out count, dotnumber, tiresize, brandname, barcode, spacename, lotnumber, lanename, frmDate, toDate, username);
            gvAdminInventory.DataBind();
            ProductDiv.Visible = false;
            pnlSearchProduct.Visible = false;
            this.TotalItems = count;
            this.pager.DrawPager(pageNo, this.TotalItems, pageSize, MaxPagesToShow);

        }
        else
        {
            string brandNameProduct = txtBrandNameProduct.Text.Trim();
            string sizeProduct = txtSizeProduct.Text.Trim();
            string shapeProduct = txtShapeProduct.Text.Trim();
            string materialProduct = txtMaterialProduct.Text.Trim();
            DateTime frmDateProduct = string.IsNullOrEmpty(txtFromDateProduct.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFromDateProduct.Text);
            DateTime toDateProduct = string.IsNullOrEmpty(txtToDateProduct.Text) ? DateTime.MinValue : Convert.ToDateTime(txtToDateProduct.Text);
            DataSet ds = Tire.SearchProduct(LotID, pageNo, pageSize, out count, brandNameProduct, frmDateProduct, toDateProduct, sizeProduct, shapeProduct, materialProduct);
            gvProductInventory.DataSource = ds;
            gvProductInventory.DataBind();
            TireDiv.Visible = false;
            pnlSearchTire.Visible = false;
            this.TotalItems = count;
            this.pagerProduct.DrawPager(pageNo, this.TotalItems, pageSize, MaxPagesToShow);
        }



    }

    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pager.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.Load_AllAdminInventory(CurrentPage);
        }
        else if (this.pagerProduct.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.Load_AllAdminInventory(CurrentPage);
        }
        return base.OnBubbleEvent(source, args);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
        lblProductUpdate.Text = "";
        lblTireUpdate.Visible = false;
        if (User.Identity.IsAuthenticated == false)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
            Response.Redirect("/");
        }

        if (!IsPostBack)
        {

            //ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
            Load_AllAdminInventory(1);
            if (Request.QueryString["status"].ToLower() == "false")
            {
                MoreTire.Visible = true;
                LinkButton1.Visible = true;
            }
            else
            {
                MoreTire.Visible = false;
                LinkButton1.Visible = false;
            }

        }
        else if (TotalItems > 0)
        {
            if (CatId == (int)ProductCategory.Tire)
                this.pager.DrawPager(CurrentPage, TotalItems, pageSize, MaxPagesToShow);
            else
                this.pagerProduct.DrawPager(CurrentPage, TotalItems, pageSize, MaxPagesToShow);

        }
    }

    protected void btnInventorySearch_Click(object sender, EventArgs e)
    {
        Load_AllAdminInventory(1);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("addInventory?OpenLotId=" + Request.QueryString["OpenLotId"].ToString());
    }

    protected void gvAdminInventory_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvAdminInventory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditTireInfo")
        {
            lblTireUpdate.Visible = false;
            hdnTireID.Value = e.CommandArgument.ToString();
            dvInventoryUpdate.Visible = true;

            LoadUpdateInventory(Convert.ToInt32(e.CommandArgument));


        }

    }


    /// <summary>
    /// Update pop Up Funcation For Tire Info
    /// </summary>
    /// <param name="tireid"></param>

    protected void LoadUpdateInventory(int tireid)
    {
        dvInventoryUpdate.Visible = true;

        //ddlTireState.Items.Clear();
        //ddlRecycleState.Items.Clear();
        //ddlTireState.DataSource = Tire.getTireStateCategory();
        //ddlTireState.DataBind();

        //LoadTireStateType();
        ddlTireState.Items.Clear();
        ddlRecycleState.Items.Clear();
        ddlTireClass.Items.Clear();

        Utils.GetLookUpData<DropDownList>(ref ddlTireClass, LookUps.LoadInventoryClass);
        Utils.GetLookUpData<DropDownList>(ref ddlTireState, LookUps.LoadInventoryAction);
        Utils.GetLookUpData<DropDownList>(ref ddlRecycleState, LookUps.LoadInventoryOutcome);
        btnGenerateBarCode_Click(null, null);
        Tire inventoryobj = new Tire(tireid);
        txtCBarCode.Text = inventoryobj.C_BarCode.ToString();
        txtDOTPlant.Text = inventoryobj.PlantCode;
        txtDOTSize.Text = inventoryobj.SizeNumber;
        txtDOTBrand.Text = inventoryobj.BrandCode;
        txtDOTWeek.Text = inventoryobj.MonthCode;
        txtDOTYear.Text = inventoryobj.YearCode;
        txtBrand.Text = inventoryobj.Brand1Name;
        txtBrand2.Text = inventoryobj.Brand2Name;
        btnGenerateBarCode_Click(null, null);
        hdnIsPlantCodeValid.Value = "1";
        hdnIsSizeCodeValid.Value = "1";
        hdnIsWeekCodeValid.Value = "1";
        hdnIsYearCodeValid.Value = "1";
        hdnBrand2ID.Value = Conversion.ParseString(inventoryobj.BrandId2);
        hdnTireID.Value = inventoryobj.TireId.ToString();
        hdnOldBarCodeId.Value = inventoryobj.TX_BarCodeId.ToString();
        txtSize.Text = inventoryobj.TireSize;
        //if (String.IsNullOrEmpty(inventoryobj.TireClassId.ToString()) || String.IsNullOrEmpty(inventoryobj.TireActionId.ToString()) || String.IsNullOrEmpty(inventoryobj.TireOutComeID.ToString()))
        //{
        //    ddlTireClass.SelectedIndex = 1;
        //    ddlTireClass.SelectedIndex = 1;
        //    ddlRecycleState.SelectedIndex = 1;

        //}
        ddlTireClass.SelectedValue = inventoryobj.TireClassId.ToString();
        ddlTireState.SelectedValue = inventoryobj.TireActionId.ToString();

        ddlRecycleState.SelectedValue = inventoryobj.TireOutComeID.ToString();

        DataSet ds = Lots.getBarcodeByTireId(tireid);
        //lblLotNumber.Visible = true;
        lblLotNumber.Text = "Lot# " + ds.Tables[0].Rows[0]["LotId"].ToString();

        imgTireBarcode.Visible = true;
        imgTireBarcode.ImageUrl = "/Handlers/GetBarcodeImage.ashx?LotId=" + Request.QueryString["OpenLotId"];

        imgBarCode.Visible = true;
        imgBarCode.ImageUrl = "/Handlers/GetBarcodeImage.ashx?TireID=" + tireid;



    }

    #region Update Inventory


    private void UpdateTire()
    {
        try
        {
            if (!string.IsNullOrEmpty(lblerror.Text))
            {
                return;
            }
            //string lotNumber = txtLotNumber.Text;
            Tire objInventory = new Tire();
            objInventory.C_BarCode = txtCBarCode.Text.Trim() == "" ? 0 : Convert.ToInt32(txtCBarCode.Text.Trim());
            objInventory.TX_BarCodeId = 0;
            objInventory.DotNumber = txtDOTPlant.Text.Trim() + txtDOTSize.Text.Trim() + txtDOTBrand.Text.Trim() + txtDOTWeek.Text.Trim() + txtDOTYear.Text.Trim();
            objInventory.BrandId1 = 1; // Convert.ToInt32(txtBrand.Text.Trim());
            objInventory.BrandId2 = Conversion.ParseInt(hdnBrand2ID.Value); // Convert.ToInt32(txtBrand2.Text.Trim());
            objInventory.TireType = "";
            objInventory.PlantCode = txtDOTPlant.Text.Trim();
            objInventory.SizeNumber = txtDOTSize.Text.Trim();
            objInventory.MonthCode = txtDOTWeek.Text.Trim();
            objInventory.YearCode = txtDOTYear.Text.Trim();
            objInventory.DateCreated = DateTime.Now;
            objInventory.CreatedById = LoginMemberId;
            objInventory.LangaugeId = LanguageId;
            objInventory.OrganizationId = UserOrganizationId;//string.IsNullOrEmpty(Session["OrganizationId"].ToString()) ? Convert.ToInt32(ddlStakeholder.SelectedValue) : Convert.ToInt32(Session["OrganizationId"]);
            objInventory.TireStateCategoryId = Convert.ToInt32(ddlTireState.SelectedValue);
            objInventory.SerialNumber = GenerateSerialNumber();
            objInventory.TireClassId = Conversion.ParseInt(ddlTireClass.SelectedValue);
            objInventory.TireOutComeID = Conversion.ParseInt(ddlRecycleState.SelectedValue);
            objInventory.TireActionId = Conversion.ParseInt(ddlTireState.SelectedValue);

            //if (objInventory.TireStateCategoryId == 1)
            //    objInventory.RecycleStateId = ddlRecycleState.SelectedIndex > 0 ? Convert.ToInt32(ddlRecycleState.SelectedValue) : 0;
            //else
            //    objInventory.RetreadStateId = ddlRecycleState.SelectedIndex > 0 ? Convert.ToInt32(ddlRecycleState.SelectedValue) : 0;

            if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value))))
            {
                objInventory.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value)));
            }
            objInventory.Space = string.Empty;
            objInventory.Lane = string.Empty;
            int lotid = Convert.ToInt32(Request.QueryString["OpenLotId"]);

            objInventory.TireId = Convert.ToInt32(hdnTireID.Value);

            objInventory.TireId = Tire.updateInventorybyTireIdandLotId(objInventory, Convert.ToInt32(hdnOldBarCodeId.Value), lotid, DateTime.Now);
            lblTireUpdate.ForeColor = Color.Green;
            lblTireUpdate.Visible = true;

            ScriptManager.RegisterStartupScript(this, GetType(), "RemoveIt", "rmSeccesMsg();", true);

            if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value))))
            {
                System.IO.File.Delete(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value)));
            }
            hdnBarCodeImageFileName.Value = "";
            imgBarCode.Visible = false;


            //int spaceId = string.IsNullOrEmpty(hidSelectedSpace.Value) ? 0 : Convert.ToInt32(hidSelectedSpace.Value);
            //int laneId = string.IsNullOrEmpty(hidSelectedLane.Value) ? 0 : Convert.ToInt32(hidSelectedLane.Value);
            //if (Conversion.ParseString(Session["lot"]) == "single")
            //{
            //    LotsInfo.InsertLotsTires(Convert.ToInt32(hidLotId.Value), objInventory.TireId, 0, DateTime.Now, 1, true, false, false, spaceId, laneId);
            //    LotsInfo.FinishedLot(Convert.ToInt32(hidLotId.Value), true);
            //    Response.Redirect("lotinfo");
            //}
            //else
            //{
            //    if (!LotsInfo.InsertLotsTires(Convert.ToInt32(hidLotId.Value), objInventory.TireId, 0, DateTime.Now, 1, true, false, false, spaceId, laneId))
            //        Response.Redirect("lotinfo");


            txtCBarCode.Text = "";
            txtDOTPlant.Text = ""; txtDOTSize.Text = ""; txtDOTBrand.Text = ""; txtDOTWeek.Text = ""; txtDOTYear.Text = "";

            ddlTireState.SelectedIndex = 0;
            ddlRecycleState.SelectedIndex = 0;
            txtBrand.Text = "";
            txtBrand2.Text = "";
            txtDOTPlant.Attributes.Add("prevValue", "0");
            txtDOTSize.Attributes.Add("prevValue", "0");
            txtDOTBrand.Attributes.Add("prevValue", "0");
            txtDOTWeek.Attributes.Add("prevValue", "0");
            txtDOTYear.Attributes.Add("prevValue", "0");
            hdnOldBarCodeId.Value = "";
            Load_AllAdminInventory(1);
            //}

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "inventory-tire.UpdateTire", ex);
        }
    }

    private string GenerateSerialNumber()
    {
        string SerialNumber = "";
        DataSet ds = OrganizationInfo.GetCountryAndStateCodeByOrganizationId(UserOrganizationId);//string.IsNullOrEmpty(Session["OrganizationId"].ToString()) ? Convert.ToInt32(ddlStakeholder.SelectedValue) : Convert.ToInt32(Session["OrganizationId"]));

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            SerialNumber += ds.Tables[0].Rows[0][0].ToString() + ds.Tables[0].Rows[0][1].ToString();
        }

        ds.Dispose();
        ds = null;

        SerialNumber += Guid.NewGuid().ToString().Substring(0, 3);
        SerialNumber += Guid.NewGuid().ToString().Substring(0, 5);

        return SerialNumber.ToUpper();

    }






    protected void btnGenerateBarCode_Click(object sender, EventArgs e)
    {
        txtDOTPlant.Attributes.Add("prevValue", txtDOTPlant.Text);
        txtDOTSize.Attributes.Add("prevValue", txtDOTSize.Text);
        txtDOTBrand.Attributes.Add("prevValue", txtDOTBrand.Text);
        txtDOTWeek.Attributes.Add("prevValue", txtDOTWeek.Text);
        txtDOTYear.Attributes.Add("prevValue", txtDOTYear.Text);

        if (ValidateDotCode())
        {
            BarCode br = new BarCode();
            br.DateCreated = DateTime.Now.ToShortDateString();
            br.OrganizationID = UserOrganizationId;
            br.BarCodeNumber = GenerateLotSerialNumber();

            string str = br.BarCodeNumber.ToString().Replace("-", "");



            // Guid g = Guid.NewGuid();
            if (br.GenerateLotBarCodeImage(str))
            {
                hdnBarCodeImageFileName.Value = str.ToString() + ".gif";
                imgBarCode.ImageUrl = String.Format("/images/temp/{0}.Gif", str);
                imgBarCode.Visible = true;
                txtBrand.Focus();
            }
            else
            {
                if (hdnBarCodeImageFileName.Value != "")
                {
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value)));
                    }
                    hdnBarCodeImageFileName.Value = "";
                }
                imgBarCode.Visible = false;
            }
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "SetRecycleStatebtnGenerateBarCode_Click", String.Format("ShowInventoryForm();SetRecycleState({0});", ddlTireState.SelectedValue), true);
    }

    private string GenerateLotSerialNumber()
    {
        StringBuilder SerialNumber = new StringBuilder(255);
        DataSet ds = OrganizationInfo.GetCountryAndStateCodeByOrganizationId(UserOrganizationId);//string.IsNullOrEmpty(Session["OrganizationId"].ToString()) ? Convert.ToInt32(ddlStakeholder.SelectedValue) : Convert.ToInt32(Session["OrganizationId"]));

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            SerialNumber.Append(ds.Tables[0].Rows[0][0].ToString() + ds.Tables[0].Rows[0][1].ToString());
        }

        ds.Dispose();
        ds = null;

        SerialNumber.Append(Guid.NewGuid().ToString().Substring(0, 3));
        SerialNumber.Append(Guid.NewGuid().ToString().Substring(0, 4));
        SerialNumber.Append("L");

        return SerialNumber.ToString().ToUpper();


    }


    protected void btnValidatePlantCode_Click(object sender, EventArgs e)
    {
        hdnIsPlantCodeValid.Value = "0";
        txtDOTPlant.Attributes.Add("prevValue", txtDOTPlant.Text);
        if (txtDOTPlant.Text.Trim() != "")
        {
            string PlantName = string.Empty;
            if (Tire.ValidatePlantCode(txtDOTPlant.Text.Trim(), out PlantName))
            {
                lblerror.Text = "";
                hdnIsPlantCodeValid.Value = "1";
                txtBrand.Text = PlantName;
                txtDOTSize.Focus();
                if (ValidateDotCode())
                {
                    btnGenerateBarCode_Click(null, null);
                }

            }
            else
            {
                if (hdnBarCodeImageFileName.Value != "")
                {
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value)));
                    }
                    hdnBarCodeImageFileName.Value = "";
                }
                imgBarCode.Visible = false;
                txtDOTPlant.Focus();
                lblerror.Text = "Plant Code not found";
            }
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "btnValidatePlantCode_Click", String.Format("ShowInventoryForm();SetRecycleState({0});", ddlTireState.SelectedValue), true);
    }



    protected void btnValidateSizeCode_Click(object sender, EventArgs e)
    {
        hdnIsSizeCodeValid.Value = "0";
        txtDOTSize.Attributes.Add("prevValue", txtDOTSize.Text);
        string Size = string.Empty;
        if (txtDOTSize.Text.Trim() != "")
        {
            if (Tire.ValidateSizeCode(txtDOTSize.Text.Trim(), out Size))
            {
                hdnIsSizeCodeValid.Value = "1";
                txtDOTBrand.Focus();
                txtSize.Text = Size;
                lblerror.Text = "";
                if (ValidateDotCode())
                {
                    btnGenerateBarCode_Click(null, null);
                }
            }
            else
            {
                if (hdnBarCodeImageFileName.Value != "")
                {
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value)));
                    }
                    hdnBarCodeImageFileName.Value = "";
                }
                imgBarCode.Visible = false;
                txtDOTSize.Focus();
                lblerror.Text = "Size Code not found";
            }
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "btnValidateSizeCode_Click", String.Format("ShowInventoryForm();SetRecycleState({0});", ddlTireState.SelectedValue), true);
    }



    protected void btnValidateBrandCode_Click(object sender, EventArgs e)
    {
        txtDOTBrand.Attributes.Add("prevValue", txtDOTBrand.Text);
        if (txtDOTBrand.Text.Trim() != "")
        {
            int bcod = txtDOTBrand.Text.Trim().Length;
            if (bcod < 4)
            {
                lblerror.Text = "Brand code must be 4 char's long";
                txtDOTBrand.Focus();
                return;
            }
            int BrandID = 0;
            string BrandName = string.Empty;
            lblerror.Text = string.Empty;
            if (Tire.ValidateBrandCode(txtDOTBrand.Text.Trim(), out BrandID, out BrandName))
            {
                hdnBrand2ID.Value = Conversion.ParseString(BrandID);
                txtBrand2.Text = BrandName;
                txtDOTWeek.Focus();
            }
            else if (txtBrand2.Text.Trim() != "")
            {
                if (Tire.ValidateBrandCodeAndInsertBrandCode(txtDOTBrand.Text.Trim(), txtBrand2.Text.Trim(), CountryIDByLanguageId, out BrandID, out BrandName))
                {


                    hdnBrand2ID.Value = Conversion.ParseString(BrandID);
                    txtBrand2.Text = BrandName;
                    txtDOTWeek.Focus();
                }
            }
            else
            {

            }
            if (ValidateDotCode())
            {
                btnGenerateBarCode_Click(null, null);
            }
        }
    }
    protected void btnValidateWeek_Click(object sender, EventArgs e)
    {
        hdnIsWeekCodeValid.Value = "0";
        txtDOTWeek.Attributes.Add("prevValue", txtDOTWeek.Text);
        if (txtDOTWeek.Text.Trim() != "")
        {

            int weekNo = Convert.ToInt32(txtDOTWeek.Text);
            int weeklength = txtDOTWeek.Text.Length;

            if (weeklength == 1)
            {
                txtDOTWeek.Text = "0" + txtDOTWeek.Text;
            }
            if (weekNo < 1 || weekNo > 52)
            {
                if (hdnBarCodeImageFileName.Value != "")
                {
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value)));
                    }
                    hdnBarCodeImageFileName.Value = "";
                }
                imgBarCode.Visible = false;
                txtDOTWeek.Focus();
                //lblerror.Text = "Please enter valid Week Code in DOT Number ie 1-52";
                lblerror.Text = "Please enter valid Week Code";
            }
            else
            {
                lblerror.Text = "";
                hdnIsWeekCodeValid.Value = "1";
                txtDOTYear.Focus();
                if (ValidateDotCode())
                {
                    btnGenerateBarCode_Click(null, null);
                }
            }
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "btnValidateWeek_Click", String.Format("ShowInventoryForm();SetRecycleState({0});", ddlTireState.SelectedValue), true);
    }
    protected void btnValidateYear_Click(object sender, EventArgs e)
    {

        hdnIsYearCodeValid.Value = "0";
        int yearlength = txtDOTYear.Text.Length;
        txtDOTYear.Attributes.Add("prevValue", txtDOTYear.Text);
        if (txtDOTYear.Text.Trim() != "")
        {
            if (yearlength == 1)
            {

                txtDOTYear.Text = "0" + txtDOTYear.Text;
            }
            int yearNo = Convert.ToInt32(txtDOTYear.Text);
            int currentYear = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2));
            int weekNum = Utils.GetWeekNumber(DateTime.Today.Date);

            if (yearNo > currentYear)
            {
                if (hdnBarCodeImageFileName.Value != "")
                {
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value)));
                    }
                    hdnBarCodeImageFileName.Value = "";
                }
                imgBarCode.Visible = false;
                txtDOTYear.Focus();
                //lblerror.Text = "Please enter valid Year Code less then or equal to " + currentYear;
                lblerror.Text = "Please enter valid Year Code";
            }
            else if (yearNo == currentYear && Convert.ToInt32(txtDOTWeek.Text) > weekNum)
            {
                if (hdnBarCodeImageFileName.Value != "")
                {
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value)));
                    }
                    hdnBarCodeImageFileName.Value = "";
                }
                imgBarCode.Visible = false;
                txtDOTWeek.Focus();
                //lblerror.Text = "Please enter valid Week Code from 1-" + weekNum + " for the Year " + yearNo;
                lblerror.Text = "Please enter valid Week Code";

            }
            else
            {
                lblerror.Text = "";
                hdnIsYearCodeValid.Value = "1";
                txtBrand2.Focus();
                if (ValidateDotCode())
                {
                    btnGenerateBarCode_Click(null, null);
                }
            }
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "btnValidateWeek_Click", String.Format("ShowInventoryForm();SetRecycleState({0});", ddlTireState.SelectedValue), true);
    }













    protected void ddlTireState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTireState.SelectedValue != "0")
        {
            LoadTireStateType();
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlTireState_SelectedIndexChanged", String.Format("ShowInventoryForm();SetRecycleState({0});", ddlTireState.SelectedValue), true);
    }


    private void LoadTireStateType()
    {
        System.Data.SqlClient.SqlParameter[] prams = new System.Data.SqlClient.SqlParameter[1];
        prams[0] = new System.Data.SqlClient.SqlParameter("@TireState", System.Data.SqlDbType.Int, 4);
        prams[0].Value = ddlTireState.SelectedValue;
        Utils.GetLookUpData<DropDownList>(ref ddlRecycleState, LookUps.RecycleState, prams);
    }


    private bool ValidateDotCode()
    {
        string PlantName = string.Empty;
        string Size = string.Empty;
        if (hdnIsSizeCodeValid.Value != "1")
        {
            if (txtDOTSize.Text.Trim() != "" && Tire.ValidateSizeCode(txtDOTSize.Text.Trim(), out Size))
                hdnIsSizeCodeValid.Value = "1";
            else
            {
                hdnIsSizeCodeValid.Value = "0";
                return false;
            }
        }

        if (hdnIsPlantCodeValid.Value != "1")
        {
            if (Tire.ValidatePlantCode(txtDOTPlant.Text.Trim(), out PlantName))
                hdnIsPlantCodeValid.Value = "1";
            else
            {
                hdnIsPlantCodeValid.Value = "0";
                return false;
            }
        }

        if (txtDOTBrand.Text.Trim() == "")
            return false;

        if (Utils.IsNumeric(txtDOTWeek.Text.Trim()) == false || Convert.ToInt32(txtDOTWeek.Text.Trim()) < 1 || Convert.ToInt32(txtDOTWeek.Text.Trim()) > 52)
            return false;

        if (Utils.IsNumeric(txtDOTYear.Text.Trim()) == false || txtDOTYear.Text.Trim().Length != 2)
            return false;

        return true;
    }



    private bool GenerateBarCodeImage(Guid g)
    {
        try
        {
            if (hdnBarCodeImageFileName.Value != "")
            {
                if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value))))
                {
                    System.IO.File.Delete(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value)));
                }
                hdnBarCodeImageFileName.Value = "";
            }
            string Code = txtDOTPlant.Text.Trim() + "-" + txtDOTSize.Text.Trim() + "-" + txtDOTBrand.Text.Trim() + "-" + txtDOTWeek.Text.Trim() + "-" + txtDOTYear.Text.Trim();

            Bitmap oBitmap = new Bitmap((Code.Length * 30), 110);

            Graphics oGraphics = Graphics.FromImage(oBitmap);
            oGraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, (Code.Length * 30), 110);

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(Server.MapPath("/Font/IDAutomationHC39M.ttf"));
            Font oFont = new Font(pfc.Families[0], 18);

            oGraphics.DrawString("*" + Code + "*", oFont, new SolidBrush(Color.Black), 20, 10);

            oBitmap.Save(Server.MapPath(String.Format("/images/temp/{0}.Gif", g)), System.Drawing.Imaging.ImageFormat.Gif);

            oBitmap.Dispose();
            oGraphics.Dispose();
            oFont.Dispose();
            pfc.Dispose();

            oBitmap = null;
            oGraphics = null;
            oFont = null;
            pfc = null;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(currentUserInfo.UserId, "GenerateBarCodeImageAndBytes", ex);
            return false;
        }

        return true;
    }





    #endregion

    protected void lnkbtnUpdateInventory_Click(object sender, EventArgs e)
    {
        UpdateTire();
        dvInventoryUpdate.Visible = false;
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        dvInventoryUpdate.Visible = false;
        hdnOldBarCodeId.Value = "";

    }

    protected void gvAdminInventory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HiddenField hdndotnumber = e.Row.FindControl("hdndotnumber") as HiddenField;
            Label lbldotnumber = e.Row.FindControl("lbldotnumber") as Label;
            if (hdndotnumber.Value.Length <= 11)
            {
                lbldotnumber.Text = '0' + hdndotnumber.Value.Substring(0, 1) + ' ' + hdndotnumber.Value.Substring(1, 2) + ' ' + hdndotnumber.Value.Substring(3, 4) + ' ' + hdndotnumber.Value.Substring(7, 2) + ' ' + hdndotnumber.Value.Substring(9, 2);
            }
            else
            {

                lbldotnumber.Text = hdndotnumber.Value.Substring(0, 2) + ' ' + hdndotnumber.Value.Substring(2, 2) + ' ' + hdndotnumber.Value.Substring(4, 4) + ' ' + hdndotnumber.Value.Substring(8, 2) + ' ' + hdndotnumber.Value.Substring(10, 2);
            }

        }

    }

    protected void gvProductInventory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditProductInfo")
        {
            lblProductUpdate.Visible = false;
            hdnProductId.Value = e.CommandArgument.ToString();
            dvInventoryUpdate.Visible = false;
            lblTireUpdate.Visible = false;
            LoadUpdateProduct(Convert.ToInt32(e.CommandArgument));
        }

    }

    protected void LoadUpdateProduct(int productId)
    {
        try
        {
            dvProductUpdate.Visible = true;

            DataSet ds = Product.GetProductDetailById(productId);
            DataSet Check = Product.GetAllSubCategories(CatId);
            if (Check != null && Check.Tables[0].Rows.Count > 0)
            {
                int SubCat = Convert.ToInt32(ds.Tables[0].Rows[0]["Productsubcategoryid"].ToString());
                
                Utils.GetProductProperties<DropDownList>(ref ddlMaterial, LookUps.ProductMaterial, CatId, LanguageId, SubCat);
                Utils.GetProductProperties<DropDownList>(ref ddlShape, LookUps.ProductShape, CatId, LanguageId, SubCat);
                Utils.GetProductProperties<DropDownList>(ref ddlSize, LookUps.ProductSize, CatId, LanguageId, SubCat);
                Utils.GetProductProperties<DropDownList>(ref ddlBrand, LookUps.ProductBrand, CatId, LanguageId, SubCat);
            }
            else
            {
                Utils.GetProductProperties<DropDownList>(ref ddlMaterial, LookUps.ProductMaterial, CatId, LanguageId);
                Utils.GetProductProperties<DropDownList>(ref ddlShape, LookUps.ProductShape, CatId, LanguageId);
                Utils.GetProductProperties<DropDownList>(ref ddlSize, LookUps.ProductSize, CatId, LanguageId);
                Utils.GetProductProperties<DropDownList>(ref ddlBrand, LookUps.ProductBrand, CatId, LanguageId);

            }
            

            ddlSize.SelectedValue = ds.Tables[0].Rows[0]["ProductSize"].ToString();
            ddlShape.SelectedValue = ds.Tables[0].Rows[0]["ProductShape"].ToString();
            ddlMaterial.SelectedValue = ds.Tables[0].Rows[0]["ProductMaterial"].ToString();
            ddlBrand.SelectedValue = ds.Tables[0].Rows[0]["BrandId"].ToString();
            DataSet dss = Lots.GetBarCodeByProductId(productId);
            lblProductLot.Text = "Lot# " + dss.Tables[0].Rows[0][1].ToString();
            imgProductLotBarCode.ImageUrl = "/Handlers/GetProductBarcodeImage.ashx?LotId=" + Request.QueryString["OpenLotId"]; ;
            imgProducBarCode.ImageUrl = "/Handlers/GetProductBarcodeImage.ashx?TireID=" + productId;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "EditTire.aspx LoadUpdateProduct", ex);
        }

    }

    protected void UpdateProduct_Click(object sender, EventArgs e)
    {
        try
        {
            string size = ddlSize.SelectedValue;
            string shape = ddlShape.SelectedValue;
            string material = ddlMaterial.SelectedValue;
            string brand = ddlBrand.SelectedValue;
            if (Product.UpdateProductById(Convert.ToInt32(hdnProductId.Value), size, shape, material, brand))
            {
                lblProductUpdate.Visible = true;
                lblProductUpdate.Text = "Product Updated Successfully!";
                lblProductUpdate.CssClass = "custom-absolute-alert alert-success";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            }
            else
            {
                lblProductUpdate.Visible = true;
                lblProductUpdate.Text = "Product Not Updated Successfully!";
                lblProductUpdate.CssClass = "custom-absolute-alert alert-danger";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            }
            dvProductUpdate.Visible = false;
            Load_AllAdminInventory(1);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "EditTire.aspx UpdateProduct_Click", ex);
        }
    }
    protected void CancelProduct_Click(object sender, EventArgs e)
    {
        dvProductUpdate.Visible = false;
    }
}