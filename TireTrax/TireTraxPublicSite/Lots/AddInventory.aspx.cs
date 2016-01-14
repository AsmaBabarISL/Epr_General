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

public partial class Lots_AddInventory : BasePage
{
    DataTable ActivityTable = null;
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
    protected void Page_Load(object sender, EventArgs e)
    {

        /*       AddNew Lot Method On Load,In this Automaticaly New Lot created and Tires Add on this Lot               */
        lblErrorSuccessInventory.Visible = false;


        if (!IsPostBack)
        {
            GetPermission(ResourceType.AddInventoryInventory, ref canView, ref canAdd, ref canUpdate, ref canDelete);
            if (!canView)
            {
                Response.Redirect("error");
            }

            if (CatId == (int)ProductCategory.Tire)
            {
                dvInventoryAdd.Visible = true;
            }
            else
            {
                dvProducts.Visible = true;
                RequiredFieldValidator rfv = rfvSubCategory;
                DataSet Check = Product.GetAllSubCategories(CatId);
                if (Check != null && Check.Tables[0].Rows.Count > 0)
                {
                    dvSubCategory.Visible = true;
                    Utils.GetLookUpData<DropDownList>(ref ddlSubCategory, LookUps.SelectedSubCategory, CatId, UserOrganizationId);


                    rfv.Enabled = true;


                    ddlMaterial.Enabled = false;
                    ddlShape.Enabled = false;
                    ddlSize.Enabled = false;
                    ddlBrand.Enabled = false;

                }
                else
                {
                    Utils.GetProductProperties<DropDownList>(ref ddlMaterial, LookUps.ProductMaterial, CatId, LanguageId);
                    Utils.GetProductProperties<DropDownList>(ref ddlShape, LookUps.ProductShape, CatId, LanguageId);
                    Utils.GetProductProperties<DropDownList>(ref ddlSize, LookUps.ProductSize, CatId, LanguageId);
                    Utils.GetProductProperties<DropDownList>(ref ddlBrand, LookUps.ProductBrand, CatId, LanguageId);
                }

            }

            if (Request.QueryString["LoadId"] != null)
            {
                Loads ObjLoad = new Loads(Conversion.ParseInt(Request.QueryString["LoadId"]));

                lblLotNumber.Text = "Load# " + ObjLoad.LoadNumber;
                lblProductLot.Text = "Load " + ObjLoad.LoadNumber;
                imgProductLot.ImageUrl = String.Format("/images/temp/{0}.Gif", ObjLoad.Guid);
                imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", ObjLoad.Guid);
                imgLotBarcode.Visible = true;
                imgProductLot.Visible = true;

            }
            else
            {
                AddNewLOT();
            }

            string OrgName = string.Empty;
            OrgName = OrganizationInfo.GetOrgLegalNameByOrgId(UserInfo.GetCurrentUserInfo().OrganizationId);
            txtQuantity.Text = "0";

            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory" + " " + OrgName)), true);
            lblCBarcodeText.Text = OrgName + " Barcode";
            if (User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("/");
            }

            //txtQuantity.Text = "0";
            //dvLot.Visible = false;
            ////txtCompanyName.Text = UserInfo.GetCurrentUserInfo().FullName;
            ////txtCompanyName.Enabled = false;
            //txtdate.Text = DateTime.Today.ToString("MM/dd/yyyy");

            //if (Request.QueryString["p"] == null)
            //{
            //    Session["Tire"] = "";
            //    Session["SubLotTireIds"] = "";
            //    Session["SelectedLotId"] = "";
            //    Load_AllAdminInventory();

            //    if (Request.QueryString["lotId"] != null)
            //    {
            //        dvLot.Visible = false;
            //        dvInventoryAdd.Visible = true;
            //        dvLotOption.Visible = false;
            //        dvLot1.Visible = false;
            //    }
            //}
            //else if (Convert.ToString(Session["Tire"]) == "1")
            //{
            //    Load_TireInventory(CurPageNum);
            //    chkSublot.Checked = true;
            //    Check_Clicked(null, null);
            //}
            //else
            //{
            //    Load_AllAdminInventory();
            //    chkSublot.Checked = true;
            //    Check_Clicked(null, null);

            //    dvLot.Visible = true;
            //    dvLotOption.Visible = false;
            //    dvLot.Visible = true;

            //}




            ddlTireState.DataSource = Tire.getTireStateCategory();
            ddlTireState.DataBind();
            //Utils.GetLookUpData<DropDownList>(ref ddlTireClass, LookUps.LoadInventoryClass);
            //ddlTireClass.SelectedIndex = 1;

            //Utils.GetLookUpData<DropDownList>(ref ddlTireState, LookUps.LoadInventoryAction);
            //ddlTireState.SelectedIndex = 1;
            //Utils.GetLookUpData<DropDownList>(ref ddlRecycleState, LookUps.LoadInventoryOutcome);
            //ddlRecycleState.SelectedIndex = 1;
            Utils.GetLookUpData<DropDownList>(ref ddlTireClass, LookUps.LoadInventoryClass);
            Utils.GetLookUpData<DropDownList>(ref ddlTireState, LookUps.LoadInventoryAction);
            Utils.GetLookUpData<DropDownList>(ref ddlRecycleState, LookUps.LoadInventoryOutcome);
            ddlTireState.SelectedIndex = 1;
            ddlTireClass.SelectedIndex = 1;
            ddlRecycleState.SelectedIndex = 1;

            //commented by bilal LoadTireStateType();
            lnkMultiple_Click(null, null);

            //ddlTireState.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));
            if (Request.QueryString["OpenLotId"] != null)
            {
                hidLotId.Value = Request.QueryString["OpenLotId"].ToString();
                DataSet ds = Lots.getBarcodeByLotId(Convert.ToInt32(hidLotId.Value));
                //lblLotNumber.Visible = true;
                lblLotNumber.Text = "Lot# " + ds.Tables[0].Rows[0][1].ToString();

                imgLotBarcode.Visible = true;
                imgLotBarcode.ImageUrl = "/Handlers/GetBarcodeImage.ashx?LotID=" + Request.QueryString["OpenLotId"];
                imgProductLot.ImageUrl = "/Handlers/GetBarcodeImage.ashx?LotID=" + Request.QueryString["OpenLotId"];
                lblProductLot.Text = "Lot# " + ds.Tables[0].Rows[0][1].ToString();
                if (CatId == (int)ProductCategory.Tire)
                {
                    dvInventoryAdd.Visible = true;
                    gvAllTire.DataSource = Tire.getCompleteTireInfo_ByLotID(Conversion.ParseInt(Request.QueryString["OpenLotId"]));
                    gvAllTire.DataBind();
                }
                else
                {
                    dvInventoryAdd.Visible = false;
                    gvProduct.DataSource = Product.GetAllProductsByLotId(Conversion.ParseInt(Request.QueryString["OpenLotId"]));
                    gvProduct.DataBind();
                }
            }
        }

    }

    #region Add Inventory

    private void loadtyres()
    {
        if (Request.QueryString["LoadId"] != null)
        {
            gvAllTire.DataSource = Loads.getLoadTireInfoByLoadId(Convert.ToInt32(Request.QueryString["LoadId"]));
            gvAllTire.DataBind();
        }

        else if (!string.IsNullOrEmpty(hidLotId.Value))
        {
            gvAllTire.DataSource = Tire.getCompleteTireInfo_ByLotID(Convert.ToInt32(hidLotId.Value));
            gvAllTire.DataBind();
            //   upnlAddInventoryForm.Update();

        }
    }
    private void AddNewTire(int nOfTires, int cbarcode)
    {
        if (nOfTires == 0)
        {
            cbarcode = Conversion.ParseInt(txtCBarCode.Text.Trim());
        }

        try
        {
            if (chkIfCBarcoreExists(cbarcode, nOfTires))
            {
                return;
            }



            //string lotNumber = txtLotNumber.Text;
            Tire objInventory = new Tire();


            objInventory.C_BarCode = cbarcode;

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
            // objInventory.TireStateCategoryId = Convert.ToInt32(ddlTireState.SelectedValue);
            objInventory.TireClassId = Conversion.ParseInt(ddlTireClass.SelectedValue);
            objInventory.TireActionId = Conversion.ParseInt(ddlTireState.SelectedValue);
            objInventory.TireOutComeID = Conversion.ParseInt(ddlRecycleState.SelectedValue);


            string BarCodeImage = "";

            if (nOfTires == 0)
            {
                objInventory.SerialNumber = hdnBarCodeImageFileName.Value;
                BarCodeImage = hdnBarCodeImageFileName.Value;
            }

            else
            {
                btnGenerateBarCode_Click(null, null);
                objInventory.SerialNumber = hdnBarCodeImageFileName.Value;
                BarCodeImage = objInventory.SerialNumber;

            }


            //if (objInventory.TireStateCategoryId == 1)
            //    objInventory.RecycleStateId = ddlRecycleState.SelectedIndex > 0 ? Convert.ToInt32(ddlRecycleState.SelectedValue) : 0;
            //else
            //    objInventory.RetreadStateId = ddlRecycleState.SelectedIndex > 0 ? Convert.ToInt32(ddlRecycleState.SelectedValue) : 0;


            if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", BarCodeImage))))
            {
                objInventory.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/Images/temp/{0}", BarCodeImage)));
            }
            objInventory.Space = string.Empty;
            objInventory.Lane = string.Empty;
            objInventory.TireId = Tire.addNewInventory(objInventory);

            if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", BarCodeImage))))
            {
                System.IO.File.Delete(Server.MapPath(String.Format("/Images/temp/{0}", BarCodeImage)));
            }

            if (nOfTires == Conversion.ParseInt(txtNumberOfRecord.Text.ToString()) - 1)
            {
                hdnBarCodeImageFileName.Value = "";
                imgBarCode.Visible = false;
            }


            int spaceId = string.IsNullOrEmpty(hidSelectedSpace.Value) ? 0 : Convert.ToInt32(hidSelectedSpace.Value);
            int laneId = string.IsNullOrEmpty(hidSelectedLane.Value) ? 0 : Convert.ToInt32(hidSelectedLane.Value);
            //if (Conversion.ParseString(Session["lot"]) == "single")
            //{
            //    Lots.insertLotsTires(Convert.ToInt32(hidLotId.Value), objInventory.TireId, 0, DateTime.Now, 1, true, false, false, spaceId, laneId);
            //    Lots.finishedLot(Convert.ToInt32(hidLotId.Value), true);
            //    Response.Redirect("lotinfo");
            //}

            if (Request.QueryString["LoadId"] != null)
            {
                int LoadId = Conversion.ParseInt(Request.QueryString["LoadId"]);
                if (!Loads.InsertLoadTireForReceving(LoadId, objInventory.TireId, LoginMemberId))
                {
                    lblErrorAddInventory.Text = "Error, Please try again!";

                    //lblErrorAddInventory.Attributes.Add("class", "custom-absolute-alert alert-danger");

                }
                else
                {
                    if (nOfTires == Conversion.ParseInt(txtNumberOfRecord.Text.ToString()) - 1)
                    {
                        lblErrorSuccessInventory.Text = "New Record Added.";
                        lblErrorSuccessInventory.Visible = true;
                        lblErrorExistCBarCode.Text = string.Empty;
                        txtCBarCode.Text = "";
                        txtDOTPlant.Text = ""; txtDOTSize.Text = ""; txtDOTBrand.Text = ""; txtDOTWeek.Text = ""; txtDOTYear.Text = "";
                        ddlTireClass.SelectedIndex = 1;
                        ddlTireState.SelectedIndex = 1;
                        ddlRecycleState.SelectedIndex = 1;
                        txtBrand.Text = "";
                        txtBrand2.Text = "";
                        txtSize.Text = "";
                        imgBarCode.Visible = false;
                        imgBarCode.ImageUrl = string.Empty;
                        txtDOTPlant.Attributes.Add("prevValue", "0");
                        txtDOTSize.Attributes.Add("prevValue", "0");
                        txtDOTBrand.Attributes.Add("prevValue", "0");
                        txtDOTWeek.Attributes.Add("prevValue", "0");
                        txtDOTYear.Attributes.Add("prevValue", "0");
                        gvAllTire.DataSource = Loads.getLoadTireInfoByLoadId(Convert.ToInt32(Request.QueryString["LoadId"]));
                        gvAllTire.DataBind();
                    }

                }
            }
            else
            {


                if (!Lots.insertLotsTires(Convert.ToInt32(hidLotId.Value), objInventory.TireId, 0, DateTime.Now, 1, true, false, false, spaceId, laneId))
                {
                    lblErrorAddInventory.Text = "Error, Please try again!";
                }
                else
                {
                    if (nOfTires == Conversion.ParseInt(txtNumberOfRecord.Text.ToString()) - 1)
                    {
                        lblErrorSuccessInventory.Text = "New Record Added.";
                        lblErrorSuccessInventory.Visible = true;
                        lblErrorExistCBarCode.Text = string.Empty;
                        txtCBarCode.Text = "";
                        txtDOTPlant.Text = ""; txtDOTSize.Text = ""; txtDOTBrand.Text = ""; txtDOTWeek.Text = ""; txtDOTYear.Text = "";
                        ddlTireClass.SelectedIndex = 1;
                        ddlTireState.SelectedIndex = 1;
                        ddlRecycleState.SelectedIndex = 1;
                        txtBrand.Text = "";
                        txtBrand2.Text = "";
                        txtSize.Text = "";
                        imgBarCode.Visible = false;
                        imgBarCode.ImageUrl = string.Empty;
                        txtDOTPlant.Attributes.Add("prevValue", "0");
                        txtDOTSize.Attributes.Add("prevValue", "0");
                        txtDOTBrand.Attributes.Add("prevValue", "0");
                        txtDOTWeek.Attributes.Add("prevValue", "0");
                        txtDOTYear.Attributes.Add("prevValue", "0");
                        gvAllTire.DataSource = Tire.getCompleteTireInfo_ByLotID(Convert.ToInt32(hidLotId.Value));
                        gvAllTire.DataBind();

                        if (Request.QueryString["OpenLotId"] != null)
                        {
                            Lots.ChangeModifiedDate(Convert.ToInt32(hidLotId.Value));
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddInventory.AddNewTire", ex);
        }
    }

    private bool chkIfCBarcoreExists(int cbarcode, int nOfTires)
    {
        if (!string.IsNullOrEmpty(lblerror.Text) || (Tire.getTireCBarCodeStatus(cbarcode, UserOrganizationId)) > 0)
        {
            if (nOfTires == 0)
            {
                lblErrorExistCBarCode.Text = "C-BarCode Already Exists.!";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                lblErrorAddInventory.Text = string.Empty;
                lblErrorSuccessInventory.Text = string.Empty;
                return true;
            }
            else
            {
                Random rnd = new Random();
                cbarcode = rnd.Next(1, 999999);
                chkIfCBarcoreExists(cbarcode, nOfTires);
                return false;
            }

        }
        else
        {
            return false;
        }
    }

    private void UpdateTire()
    {
        try
        {
            Tire objInventory = new Tire();
            objInventory.C_BarCode = txtCBarCode.Text.Trim() == "" ? 0 : Convert.ToInt32(txtCBarCode.Text.Trim());
            objInventory.TX_BarCodeId = 0;
            objInventory.DotNumber = txtDOTPlant.Text.Trim() + txtDOTSize.Text.Trim() + txtDOTBrand.Text.Trim() + txtDOTWeek.Text.Trim() + txtDOTYear.Text.Trim();
            objInventory.BrandId1 = 1;
            objInventory.BrandId2 = Conversion.ParseInt(hdnBrand2ID.Value);
            objInventory.TireType = "";
            objInventory.PlantCode = txtDOTPlant.Text.Trim();
            objInventory.SizeNumber = txtDOTSize.Text.Trim();
            objInventory.MonthCode = txtDOTWeek.Text.Trim();
            objInventory.YearCode = txtDOTYear.Text.Trim();
            objInventory.DateCreated = DateTime.Now;
            objInventory.CreatedById = LoginMemberId;
            objInventory.LangaugeId = LanguageId;
            objInventory.OrganizationId = UserOrganizationId;
            objInventory.SerialNumber = GenerateSerialNumber();
            objInventory.TireClassId = Conversion.ParseInt(ddlTireClass.SelectedValue);
            objInventory.TireOutComeID = Conversion.ParseInt(ddlRecycleState.SelectedValue);
            objInventory.TireActionId = Conversion.ParseInt(ddlTireState.SelectedValue);

            if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value))))
            {
                objInventory.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value)));
            }
            objInventory.Space = string.Empty;
            objInventory.Lane = string.Empty;
            objInventory.TireId = Convert.ToInt32(hdnTireID.Value);

            if (Request.QueryString["LoadId"] != null)
            {
                objInventory.TireId = Tire.updateTireForLoadRecieve(objInventory, Conversion.ParseInt(hdnOldTXBarcodeID.Value));
            }
            else
            {
                objInventory.TireId = Tire.updateInventory(objInventory, Conversion.ParseInt(hdnOldTXBarcodeID.Value), Convert.ToInt32(hidLotId.Value), DateTime.Now);
            }
            if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value))))
            {
                System.IO.File.Delete(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value)));
            }


            if (objInventory.TireId > 0)
            {
                hdnTireID.Value = "";
                hdnOldTXBarcodeID.Value = "";
                lblErrorSuccessInventory.Text = "Record is successfully updated";
                lnkbtnUpdateInventory.Visible = false;
                lnkbtnAddInventory.Visible = true;
                lnkCompleted.Visible = true;

                txtCBarCode.Text = "";
                txtDOTPlant.Text = ""; txtDOTSize.Text = ""; txtDOTBrand.Text = ""; txtDOTWeek.Text = ""; txtDOTYear.Text = "";
                ViewState["UpdatTire"] = "0";
                ddlTireState.SelectedIndex = 1;
                ddlRecycleState.SelectedIndex = 1;
                ddlTireClass.SelectedIndex = 1;
                txtBrand.Text = "";
                txtBrand2.Text = "";
                txtSize.Text = "";
                txtDOTPlant.Attributes.Add("prevValue", "0");
                txtDOTSize.Attributes.Add("prevValue", "0");
                txtDOTBrand.Attributes.Add("prevValue", "0");
                txtDOTWeek.Attributes.Add("prevValue", "0");
                txtDOTYear.Attributes.Add("prevValue", "0");

                imgBarCode.Visible = false;
                imgBarCode.ImageUrl = string.Empty;

                if (Request.QueryString["OpenLotId"] != null)
                {
                    Lots.ChangeModifiedDate(Convert.ToInt32(hidLotId.Value));
                }



                if (Request.QueryString["LoadId"] != null)
                {
                    gvAllTire.DataSource = Loads.getLoadTireInfoByLoadId(Convert.ToInt32(Request.QueryString["LoadId"]));
                    gvAllTire.DataBind();

                }
                else
                {

                    gvAllTire.DataSource = Tire.getCompleteTireInfo_ByLotID(Conversion.ParseInt(hidLotId.Value));
                    gvAllTire.DataBind();
                }

            }
            txtNumberOfRecord.Enabled = true;
            ResourceRequiredFieldValidator10.Enabled = true;
            ResourceRequiredFieldValidator10.CssClass = "custom-error";
            ResourceRequiredFieldValidator10.ValidationGroup = "GenerateBarCode";
            ResourceRequiredFieldValidator10.ControlToValidate = "txtNumberOfRecord";
            ResourceRequiredFieldValidator10.ErrorControlText = "This field is required";

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddInventory.UpdateTire", ex);
        }
    }
    private void LoadTireInfo(int TireId)
    {
        try
        {
            txtNumberOfRecord.Enabled = false;
            ResourceRequiredFieldValidator10.Enabled = false;
            txtNumberOfRecord.CssClass = "form-control";
            txtNumberOfRecord.Text = string.Empty;


            lblErrorAddInventory.Text = "";
            lnkCompleted.Visible = false;
            lnkbtnUpdateInventory.Visible = true;
            lnkbtnAddInventory.Visible = false;

            //ddlTireClass.Items.Clear();
            //ddlTireState.Items.Clear();
            //ddlRecycleState.Items.Clear();
            //ddlTireState.DataSource = Tire.getTireStateCategory();
            //ddlTireState.DataBind();
            //LoadTireStateType();

            Utils.GetLookUpData<DropDownList>(ref ddlTireClass, LookUps.LoadInventoryClass);
            Utils.GetLookUpData<DropDownList>(ref ddlTireState, LookUps.LoadInventoryAction);
            Utils.GetLookUpData<DropDownList>(ref ddlRecycleState, LookUps.LoadInventoryOutcome);


            hdnTireID.Value = TireId.ToString();

            Tire objInv = new Tire(TireId);
            txtCBarCode.Text = Conversion.ParseString(objInv.C_BarCode);
            txtDOTPlant.Text = objInv.PlantCode;
            txtDOTSize.Text = objInv.SizeNumber;
            txtDOTBrand.Text = objInv.BrandCode;
            txtDOTWeek.Text = objInv.MonthCode;
            txtDOTYear.Text = objInv.YearCode;
            txtBrand.Text = objInv.Brand1Name;
            btnGenerateBarCode_Click(null, null);
            //System.Text.Encoding enc = System.Text.Encoding.ASCII;
            //hdnBarCodeImageFileName.Value = enc.GetString(objInv.BarcodeImage);
            hdnIsPlantCodeValid.Value = "1";
            hdnIsSizeCodeValid.Value = "1";
            hdnIsWeekCodeValid.Value = "1";
            hdnIsYearCodeValid.Value = "1";
            txtBrand2.Text = objInv.Brand2Name;
            txtDOTBrand.Attributes.Add("prevValue", objInv.BrandCode);
            hdnBrand2ID.Value = Conversion.ParseString(objInv.BrandId2);
            txtSize.Text = objInv.TireSize;
            hdnTireID.Value = objInv.TireId.ToString();
            hdnOldTXBarcodeID.Value = objInv.TX_BarCodeId.ToString();
            ddlTireClass.SelectedValue = objInv.TireClassId.ToString();
            ddlTireState.SelectedValue = objInv.TireActionId.ToString();
            //LoadTireStateType();
            ddlRecycleState.SelectedValue = objInv.TireOutComeID.ToString();
            imgBarCode.Visible = true;
            imgBarCode.ImageUrl = "/Handlers/GetBarcodeImage.ashx?TireID=" + TireId;

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Addinventory.LoadTireInfo", ex);
        }
    }

    private bool DeleteTireInfo(int TireId)
    {
        bool result = false;
        try
        {

            if (Request.QueryString["LoadId"] != null)
            {
                if (Tire.deleteTireInfoForRecieveLoad(TireId))
                {

                    //gvAllTire.DataSource = Tire.getCompleteTireInfo_ByLotID(Convert.ToInt32(hidLotId.Value));
                    //gvAllTire.DataBind();
                    return true;
                }
                else
                    return false;

            }

            else
            {
                if (Tire.deleteTireInfo(TireId))
                {

                    //gvAllTire.DataSource = Tire.getCompleteTireInfo_ByLotID(Convert.ToInt32(hidLotId.Value));
                    //gvAllTire.DataBind();
                    return true;
                }
                else
                    return false;
            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddInventory.DeleteTireInfo", ex);
        }
        return result;
    }
    protected void gvAllTire_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "LoadTireInfo")
            {
                LoadTireInfo(Conversion.ParseInt(e.CommandArgument));

            }
            else if (e.CommandName == "DeleteSP")
            {
                if (DeleteTireInfo(Conversion.ParseInt(e.CommandArgument)))
                {

                    lblErrorAddInventory.Text = string.Empty;
                    //upnlsearch.Update();
                }
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddInventory.gvAllTire_RowCommand", ex);
        }
        loadtyres();
    }

    // Functin used in Generate Barcode for Tire
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
    private void LoadTireStateType()
    {
        //   commented by bilal   System.Data.SqlClient.SqlParameter[] prams = new System.Data.SqlClient.SqlParameter[1];
        //  commented by bilal    prams[0] = new System.Data.SqlClient.SqlParameter("@TireState", System.Data.SqlDbType.Int, 4);
        // commented by bilalprams[0].Value = ddlTireState.SelectedValue;
        //      Utils.GetLookUpData<DropDownList>(ref ddlRecycleState, LookUps.RecycleState, prams);
        Utils.GetLookUpData<DropDownList>(ref ddlRecycleState, LookUps.LoadInventoryOutcome);


    }
    //Generate Tire Barcode Image
    private bool GenerateBarCodeImage(Guid g)
    {
        try
        {
            if (hdnBarCodeImageFileName.Value != "")
            {
                if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value))))
                {
                    System.IO.File.Delete(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value)));
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

            oBitmap.Save(Server.MapPath(String.Format("/Images/temp/{0}.Gif", g)), System.Drawing.Imaging.ImageFormat.Gif);

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
    protected void lnkbtnUpdateInventory_Click(object sender, EventArgs e)
    {
        UpdateTire();

    }
    protected void lnkbtnClearInventory_Click(object sender, EventArgs e)
    {
        txtDOTPlant.Attributes.Add("prevValue", "");
        txtDOTSize.Attributes.Add("prevValue", "");
        txtDOTBrand.Attributes.Add("prevValue", "");
        txtDOTWeek.Attributes.Add("prevValue", "");
        txtDOTYear.Attributes.Add("prevValue", "");

        txtCBarCode.Text = string.Empty;
        txtDOTPlant.Text = string.Empty;
        txtDOTSize.Text = string.Empty;
        txtDOTBrand.Text = string.Empty;
        txtDOTWeek.Text = string.Empty;
        txtDOTYear.Text = string.Empty;
        txtBrand.Text = string.Empty;
        txtBrand2.Text = string.Empty;
        txtSize.Text = string.Empty;
        //hidLotId.Value = string.Empty;
        hdnOldTXBarcodeID.Value = string.Empty;
        Session["SelectedLotId"] = string.Empty;
        //hidSelectedLot.Value = string.Empty;
        Session["SubLotTireIds"] = "";
        lblErrorAddInventory.Text = "";
        ddlTireClass.SelectedIndex = 0;
        ddlTireState.SelectedIndex = 0;
        ddlRecycleState.SelectedIndex = 0;
        imgBarCode.Visible = false;
        imgBarCode.ImageUrl = string.Empty;
        lnkbtnAddInventory.Visible = true;
        lnkbtnUpdateInventory.Visible = false;
        lnkCompleted.Visible = true;


        txtNumberOfRecord.Enabled = true;
        ResourceRequiredFieldValidator10.Enabled = true;
        ResourceRequiredFieldValidator10.CssClass = "custom-error";
        ResourceRequiredFieldValidator10.ValidationGroup = "GenerateBarCode";
        ResourceRequiredFieldValidator10.ControlToValidate = "txtNumberOfRecord";
        ResourceRequiredFieldValidator10.ErrorControlText = "This field is required";
    }

    private bool ValidateTextBox()
    {
        if (!string.IsNullOrEmpty(txtCBarCode.Text) &&
           !string.IsNullOrEmpty(txtDOTPlant.Text) &&
           !string.IsNullOrEmpty(txtDOTSize.Text) &&
           !string.IsNullOrEmpty(txtDOTBrand.Text) &&
           !string.IsNullOrEmpty(txtDOTWeek.Text) &&
           !string.IsNullOrEmpty(txtDOTYear.Text) &&
            !string.IsNullOrEmpty(txtCBarCode.Text) &&
            !string.IsNullOrEmpty(txtBrand.Text) &&
            ddlTireClass.SelectedIndex > 0 &&
            ddlTireState.SelectedIndex > 0 &&
            ddlRecycleState.SelectedIndex > 0

            )
            return true;
        else if (string.IsNullOrEmpty(txtCBarCode.Text) &&
           string.IsNullOrEmpty(txtDOTPlant.Text) &&
           string.IsNullOrEmpty(txtDOTSize.Text) &&
           string.IsNullOrEmpty(txtDOTBrand.Text) &&
           string.IsNullOrEmpty(txtDOTWeek.Text) &&
           string.IsNullOrEmpty(txtDOTYear.Text) &&
           string.IsNullOrEmpty(txtCBarCode.Text) &&
           string.IsNullOrEmpty(txtBrand.Text) &&
            ddlTireClass.SelectedIndex == 0 &&
            ddlTireState.SelectedIndex == 0 &&
            ddlRecycleState.SelectedIndex == 0
            )
            return true;
        else
            return false;
    }
    protected void lnkbtnComplete_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["LoadId"] != null)
        {
            if (!ValidateTextBox())
            {
                lblErrorAddInventory.Text = "Please fill/clear the form and then click Done.";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                return;
            }


            lnkbtnAddInventory_Click(null, null);

            Response.Redirect("inventory-load");

        }
        else
        {
            if (!ValidateTextBox())
            {
                lblErrorAddInventory.Text = "Please fill/clear the form and then click Done.";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                return;
            }

            lnkbtnAddInventory_Click(null, null);
            Lots.updateLotQuantity(Convert.ToInt32(hidLotId.Value));


            Response.Redirect("lotinfo");
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);

    }
    protected void lnkbtnAddInventory_Click(object sender, EventArgs e)
    {
        int cbarcode = 0;
        int NumberOfRecord = Conversion.ParseInt(txtNumberOfRecord.Text);
        Random rnd = new Random();


        for (int i = 0; i < NumberOfRecord; i++)
        {
            cbarcode = rnd.Next(1, 999999);
            AddNewTire(i, cbarcode);
        }
        txtNumberOfRecord.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
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
                hdnBarCodeImageFileName.Value = str + ".gif";
                imgBarCode.ImageUrl = String.Format("/Images/temp/{0}.Gif", str);
                imgBarCode.Visible = true;
                txtBrand.Focus();
            }
            else
            {
                if (hdnBarCodeImageFileName.Value != "")
                {
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value)));
                    }
                    hdnBarCodeImageFileName.Value = "";
                }
                imgBarCode.Visible = false;
            }
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "SetRecycleStatebtnGenerateBarCode_Click", String.Format("ShowInventoryForm();SetRecycleState({0});", ddlTireState.SelectedValue), true);
    }
    protected void btnValidatePlantCode_Click(object sender, EventArgs e)
    {
        lblErrorAddInventory.Text = string.Empty;
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
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value)));
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
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value)));
                    }
                    hdnBarCodeImageFileName.Value = "";
                }
                imgBarCode.Visible = false;
                txtDOTSize.Focus();
                lblerror.Text = "Size Code not found";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
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
                txtDOTWeek.Focus();
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
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value)));
                    }
                    hdnBarCodeImageFileName.Value = "";
                }
                imgBarCode.Visible = false;
                txtDOTWeek.Focus();
                lblerror.Text = "Please enter valid Week Code ";
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
        txtDOTYear.Attributes.Add("prevValue", txtDOTYear.Text);
        int yearlength = txtDOTYear.Text.Length;

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
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value)));
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
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value))))
                    {
                        System.IO.File.Delete(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value)));
                    }
                    hdnBarCodeImageFileName.Value = "";
                }
                imgBarCode.Visible = false;
                txtDOTWeek.Focus();
                //lblerror.Text = "Please enter valid Week Code from 1-"+weekNum +" for the Year "+yearNo;
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
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("lotinfo");
    }
    protected void ddlStewardship_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlStakeholder.Items.Clear();
        //if (Convert.ToInt32(ddlStewardship.SelectedValue) > 0)
        //{
        //    ddlStakeholder.DataSource = OrganizationInfo.GetApprovedStakeholdersByStewardshipId(Convert.ToInt32(ddlStewardship.SelectedValue));
        //    ddlStakeholder.DataBind();
        //}
        //ddlStakeholder.Items.Insert(0, new ListItem(ResourceMgr.GetMessage("Select"), "0"));
    }
    // Commented by bilal protected void ddlTireState_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlTireState.SelectedValue != "0")
    //    {
    //        LoadTireStateType();
    //    }

    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "ddlTireState_SelectedIndexChanged", String.Format("ShowInventoryForm();SetRecycleState({0});", ddlTireState.SelectedValue), true);
    //}


    #endregion

    #region NOT IN USED
    // Select Single Intake 
    protected void lnkSingle_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        Session["lot"] = "single";
        dvLotOption.Visible = false;
        dvLot.Visible = true;
        txtQuantity.Text = "1";
        txtQuantity.Visible = false;
        // reqQuantity.Enabled = false;
        litSingle.Visible = true;
        //dvInventoryAdd.Visible = true;
        // ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
    }

    // Select Multiple Intake 
    protected void lnkMultiple_Click(object sender, EventArgs e)
    {
        Session["lot"] = "multiple";
        dvLotOption.Visible = false;
        dvLot.Visible = true;
        txtQuantity.Text = "0";
        txtQuantity.Visible = true;
        // reqQuantity.Enabled = true;
        litSingle.Visible = false;
    }
    #endregion

    #region Add LOT


    // Generate LOT Serial Number
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
    // Add New LOT Record
    protected void lnkLotSave_Click(object sender, EventArgs e)
    {
        AddNewLOT();
    }
    // Continue Functionality
    protected void lnkContinue_Click(object sender, EventArgs e)
    {
        dvLot.Visible = false;
        dvInventoryAdd.Visible = true;

    }

    private void AddNewLOT()
    {
        try
        {

            //  if (string.IsNullOrEmpty(txtLotNmber.Text))
            if (string.IsNullOrEmpty(hidLotId.Value))
            {
                if (txtLotNmber.Text == "Create New Lot")
                {
                    txtLotNmber.Text = string.Empty;
                }
                int id = 0;
                Lots lot = new Lots();
                string lotNum = Guid.NewGuid().ToString().Substring(0, 6);
                lblLotNumber.Text = "Lot# " + lotNum;
                lot.LotNumber = lotNum;//txtLotNmber.Text;
                lot.Quantity = string.IsNullOrEmpty(txtQuantity.Text) ? 0 : Convert.ToInt32(txtQuantity.Text);
                lot.OrganizationId = UserOrganizationId;// Convert.ToInt32(ddlStakeholder.SelectedValue);
                lot.DateCreated = DateTime.Now;//DateTime.Parse(txtdate.Text.ToString());
                lot.IsActive = true;
                lot.SpaceId = 1;
                lot.UserID = LoginMemberId;// currentUserInfo.UserId;
                lot.RoleID = UserOrganizationRoleId;// currentUserInfo.RoleId;
                lot.IsCompleted = false;
                lot.ProductCategoryId = CatId;


                BarCode br = new BarCode();
                br.DateCreated = DateTime.Now.ToShortDateString();
                br.OrganizationID = UserOrganizationId;
                br.BarCodeNumber = GenerateLotSerialNumber();
                // Guid g = Guid.NewGuid();
                string str = br.BarCodeNumber.ToString().Replace("-", "");
                if (br.GenerateLotBarCodeImage(str))
                {
                    hdnLotBarCodeImageFileName.Value = str + ".gif";
                    imgLotBarcode.ImageUrl = String.Format("/Images/temp/{0}.Gif", str);
                    imgLotBarcode.Visible = true;
                    imgProductLot.ImageUrl = String.Format("/Images/temp/{0}.Gif", str);
                    imgProductLot.Visible = true;
                }
                if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnLotBarCodeImageFileName.Value))))
                {
                    br.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/Images/temp/{0}", hdnLotBarCodeImageFileName.Value)));

                }
                lot.BarCodeId = BarCode.Insert(br);

                if (chkSublot.Checked)
                    lot.SubLot = true;
                else
                    lot.SubLot = false;
                string serialNumber = Lots.insertLot(lot, out id, str);
                hidLotId.Value = id.ToString();
                imgLotBarcode.Visible = true;
                imgLotBarcode.ImageUrl = "/Handlers/GetBarcodeImage.ashx?LotID=" + id;
                imgProductLot.Visible = true;
                imgProductLot.ImageUrl = "/Handlers/GetBarcodeImage.ashx?LotID=" + id;
                // lblLotNumber.Text += " " + txtLotNmber.Text;
                lblLotNumber.Text = "Lot# " + lotNum;
                lblProductLot.Text = "Lot# " + lotNum;
                lnkSingle.Visible = false;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "HideLotSaveLnk();", true);

                if (chkSublot.Checked)
                {
                    Lots.updateSubLotTires(Convert.ToInt32(hidSelectedLot.Value), id, Session["SubLotTireIds"].ToString());
                    Session["SubLotTireIds"] = "";
                    Response.Redirect("lotinfo");
                }
                else
                {
                    lnkContinue.Visible = true;
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(hidSelectedLot.Value))
                    hidLotId.Value = hidSelectedLot.Value;

                lblLotNumber.Text = "Lot# " + Lots.getLotNumberByLotId(Convert.ToInt32(hidLotId.Value));
                imgLotBarcode.Visible = true;
                imgLotBarcode.ImageUrl = "/Handlers/GetBarcodeImage.ashx?LotID=" + hidLotId.Value;
            }
            //dvLot.Visible = false;
            //dvInventoryAdd.Visible = true;
            gvAllTire.DataSource = Tire.getCompleteTireInfo_ByLotID(Convert.ToInt32(hidLotId.Value));
            gvAllTire.DataBind();

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "addInventory.lnkLotSave_Click", ex);
        }
    }

    //Generate Lot Barcode Image
    //private bool GenerateLotBarCodeImage(string g)
    //{
    //    try
    //    {
    //        if (hdnBarCodeImageFileName.Value != "")
    //        {
    //            if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value))))
    //            {
    //                System.IO.File.Delete(Server.MapPath(String.Format("/images/temp/{0}", hdnBarCodeImageFileName.Value)));
    //            }
    //            hdnBarCodeImageFileName.Value = "";
    //        }
    //        string Code = g.ToString(); //txtLotNmber.Text;

    //        Bitmap oBitmap = new Bitmap((Code.Length * 30), 110);

    //        Graphics oGraphics = Graphics.FromImage(oBitmap);
    //        oGraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, (Code.Length * 30), 110);

    //        PrivateFontCollection pfc = new PrivateFontCollection();
    //        pfc.AddFontFile(Server.MapPath("/Font/IDAutomationHC39M.ttf"));
    //        Font oFont = new Font(pfc.Families[0], 18);

    //        oGraphics.DrawString("*" + Code + "*", oFont, new SolidBrush(Color.Black), 20, 10);

    //        oBitmap.Save(Server.MapPath(String.Format("/images/temp/{0}.Gif", g)), System.Drawing.Imaging.ImageFormat.Gif);

    //        oBitmap.Dispose();
    //        oGraphics.Dispose();
    //        oFont.Dispose();
    //        pfc.Dispose();

    //        oBitmap = null;
    //        oGraphics = null;
    //        oFont = null;
    //        pfc = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        new SqlLog().InsertSqlLog(currentUserInfo.UserId, "GenerateBarCodeImageAndBytes", ex);
    //        return false;
    //    }

    //    return true;
    //}
    #endregion


    #region SUBLOT


    protected void Load_AllAdminInventory()
    {
        try
        {
            pageSize = 0;// Convert.ToInt32(ddlPageSize.SelectedValue);
            //gvAdminInventory.PageSize = pageSize;


            //int status = 1;
            // int TireState = Convert.ToInt32(ddlTireState.SelectedValue);

            //string tx_barcode = txtInventoryTX_Barcode.Text.Trim();
            //string stewardship = txtInventoryStewardship.Text.Trim();
            //string SizeCode = txtSizeCode.Text.Trim();

            //DataSet ds = Inventory.SearchLotInventory(1, 0, 0, out totalRows, "", "", "", "", 0, status,UserOrganizationId);

            Load_AllPermanentLot(1);
            //DataSet ds = Lots.getPermanentLot(UserOrganizationId);
            //gvAdminInventory.DataSource = ds;
            //gvAdminInventory.DataBind();
            // GridPaging();
            dvLot1.Visible = true;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddInventory.aspx Load_AllAdminInventory", ex);
        }
    }

    private void Load_AllPermanentLot(int pageNo)
    {

        pageSize = 25;// Convert.ToInt32(ddlPageSize.SelectedValue);
        grvPermanentLot.PageSize = pageSize;
        CurrentPage = pageNo;
        int count = 0;
        DataSet ds = Lots.getPermanentLot(pageNo, pageSize, out count, Convert.ToInt32(hidSelectedOrgId.Value), CatId);

        this.TotalItemsR = count;
        // this.FacilityStorageLOTSPager.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);

        grvPermanentLot.DataSource = ds;
        grvPermanentLot.DataBind();
    }


    protected void Load_TireInventory(int PageIdCurrent)
    {
        try
        {
            int status = 1;

            if (string.IsNullOrEmpty(Convert.ToString(Session["SelectedLotId"])))
                Session["SelectedLotId"] = hidSelectedLot.Value;
            DataSet ds = Tire.SearchTireInventory(1, 0, Convert.ToInt32(Session["SelectedLotId"].ToString()), out totalRows, "", "", "", "", 0, status);
            gvTires.DataSource = ds;
            gvTires.DataBind();
            //GridPagingTire();
            dvLot1.Visible = false;
            dvTires.Visible = true;
            dvLotOption.Visible = false;
            dvLot.Visible = true;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddInventory.aspx Load_TireInventory", ex);
        }
    }

    // SubLot Click Functionality
    protected void Check_Clicked(Object sender, EventArgs e)
    {

        if (chkSublot.Checked)
        {
            Load_AllAdminInventory();
            dvSubLot.Visible = true;
            dvLot1.Visible = true;
            dvTires.Visible = false;
            txtQuantity.Enabled = false;
            //   dvLot1.Visible = true;
        }
        else
        {
            dvTires.Visible = false;
            dvSubLot.Visible = false;
            dvLot1.Visible = true;
            txtQuantity.Enabled = true;
        }
    }
    protected void lnkSubParkingLot_Click(object sender, EventArgs e)
    {
        Session["SelectedLotId"] = hidSelectedLot.Value;
        Load_TireInventory(1);
    }
    protected void lnkSubParkingLotCancel_Click(object sender, EventArgs e)
    {
        Session["SelectedLotId"] = "";
        Session["SubLotTireIds"] = "";
        dvSubLot.Visible = false;
        dvLot1.Visible = true;
        dvTires.Visible = false;
        chkSublot.Checked = false;
    }
    protected void lnkSubParkingLotSaveBtn_Click(object sender, EventArgs e)
    {
        dvLot1.Visible = true;
        dvSubLot.Visible = false;
        dvTires.Visible = false;
        int count = 0;

        GridView gv = gvTires;
        StringBuilder str = new StringBuilder(255);
        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelectTire");
            if (chk.Checked)
            {
                HiddenField hid = (HiddenField)row.FindControl("hidTireId");
                str.Append(hid.Value);
                str.Append(",");
                count++;
            }
        }
        txtQuantity.Text = count.ToString();
        str.Length = str.Length - 1;
        txtLotNmber.Enabled = true;
        txtLotNmber.Text = Lots.getPermanentLotName(Convert.ToInt32(hidSelectedLot.Value)) + " SubLot";
        Session["SubLotTireIds"] = str.ToString();
        txtLotNmber.Enabled = false;
        // dvSubLot.Visible = false;
    }
    protected void lnkBackSubLotTire_Click(object sender, EventArgs e)
    {
        Utils.SetSelectedIdsGridView(ref gvAdminInventory, "", "Radio1", "", true, hidSelectedLot.Value);
        dvSubLot.Visible = true;
        dvLot1.Visible = true;
        dvTires.Visible = false;
    }


    #endregion //sublot

    #region Parmanent Parking LOT DIV
    protected void btnInventorySearch_Click(object sender, EventArgs e)
    {
        Load_AllAdminInventory();
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  Load_AllAdminInventory();
    }
    protected void ddlPageSize_SelectedIndexChanged2(object sender, EventArgs e)
    {
        //  Load_AllAdminInventory();
    }
    protected void lnkParkingLot_Click(object sender, EventArgs e)
    {
        LoadPermanentGrid();
        dvPermanentLot.Visible = true;
    }

    protected void lnkCancelAddLot_Click(object sender, EventArgs e)
    {
        Response.Redirect("lotinfo", true);

    }
    private void LoadPermanentGrid()
    {
        try
        {
            Session["TemporaryLotId"] = "";
            Session["SpaceId"] = "";
            Load_AllPermanentLot(1);

            //DataSet ds = Lots.getPermanentLot(UserOrganizationId);
            //grvPermanentLot.DataSource = ds;
            //grvPermanentLot.DataBind();
            dvPermanentLot.Visible = true;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "lotInfo .LoadPermanentGrid", ex);
        }
    }



    protected void lnkPermanentLot_Click(object sender, EventArgs e)
    {
        //if (!Utils.CheckValueIsSelectedInGridview(grvPermanentLot, "", "Radio1", true))
        if (string.IsNullOrEmpty(hidSelectedLot2.Value))
        {
            lblErrorPermanentLotdv.Text = "Please select Parking Lot";
            return;
        }
        LoadSpaces();
        dvParkingLot1.Visible = false;
        dvSpace.Visible = true;
        dvlane.Visible = false;
    }

    protected void lnkSpacePerLot_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidSelectedSpace.Value))
        {
            lblErrorPermanentLotSpacedv.Text = "Please select a Space";
            return;
        }
        string str = hidSelectedSpace.Value.Replace(",", "");
        Session["SpaceId"] = str;
        dvParkingLot1.Visible = false;
        dvSpace.Visible = false;
        dvlane.Visible = true;
        LoadLanes();
    }
    protected void lnkLanePerLot_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidSelectedLane.Value))
        {
            lblErrorPermanentLotLanedv.Text = "Please select a Lane";
            return;
        }
        // LotsInfo.TransferTemporaryLot(Convert.ToInt32(Session["TemporaryLotId"]), Convert.ToInt32(str), Convert.ToInt32(str2));
        Session["TemporaryLotId"] = "";
        Session["SpaceId"] = "";
        txtLotNmber.Enabled = true;
        txtLotNmber.Text = Lots.getPermanentLotName(Convert.ToInt32(hidSelectedLot2.Value));
        hidLotId.Value = hidSelectedLot2.Value.Replace(",", "");
        if (string.IsNullOrEmpty(txtQuantity.Text))
            txtQuantity.Text = "0";
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;
        dvParkingLot1.Visible = true;
        dvPermanentLot.Visible = false;
        dvSpace.Visible = false;
        dvlane.Visible = false;
        txtLotNmber.Enabled = false;
        // Load_AllAdminInventory(1);

    }
    protected void lnkCancelLot_Click(object sender, EventArgs e)
    {
        Session["TemporaryLotId"] = "";
        Session["SpaceId"] = "";
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;
        dvParkingLot1.Visible = true;
        dvPermanentLot.Visible = false;
        dvSpace.Visible = false;
        dvlane.Visible = false;
    }



    protected void lnkBackPermanentLotSpace_Click(object sender, EventArgs e)
    {
        Utils.SetSelectedIdsGridView(ref grvPermanentLot, "", "Radio1", "", true, hidSelectedLot2.Value);
        dvPermanentLot.Visible = true;
        dvParkingLot1.Visible = true;
        dvSpace.Visible = false;
        dvlane.Visible = false;
    }
    protected void lnkBackPermanentLotLane_Click(object sender, EventArgs e)
    {
        Utils.SetSelectedIdsGridView(ref grdSpaces, "", "Radio1", "", true, hidSelectedSpace.Value);
        dvParkingLot1.Visible = false;
        dvSpace.Visible = true;
        dvlane.Visible = false;
    }
    private void LoadSpaces()
    {
        try
        {

            string str = hidSelectedLot2.Value.Replace(",", "");
            DataSet ds = Lots.getPermanentLotSpace(Convert.ToInt32(str));
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                lnkSpacePerLot.Visible = false;
            else
                lnkSpacePerLot.Visible = true;
            grdSpaces.DataSource = ds;
            grdSpaces.DataBind();

            dvPermanentLot.Visible = true;
            dvPermanentLot.Visible = true;
            dvSpace.Visible = true;

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "lotInfo .LoadSpaces", ex);
        }
    }
    private void LoadLanes()
    {
        try
        {
            string str = hidSelectedSpace.Value.Replace(",", "");
            DataSet ds = Lots.getPermanentLotSpaceLane(Convert.ToInt32(str));
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                lnkLanePerLot.Visible = false;
            else
                lnkLanePerLot.Visible = true;

            gvlane.DataSource = ds;
            gvlane.DataBind();

            dvPermanentLot.Visible = true;
            dvPermanentLot.Visible = true;
            dvSpace.Visible = false;
            dvlane.Visible = true;



        }
        catch (Exception e)
        {


        }

    }

    #endregion

    protected void gvAllTire_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnfeilddotnumber = e.Row.FindControl("hdndotnumber") as HiddenField;
            Label lbldot = e.Row.FindControl("AllTireDotNumber") as Label;
            if (hdnfeilddotnumber.Value.Length <= 11)
            {
                lbldot.Text = '0' + hdnfeilddotnumber.Value.Substring(0, 1) + ' ' + hdnfeilddotnumber.Value.Substring(1, 2) + ' ' + hdnfeilddotnumber.Value.Substring(3, 4) + ' ' + hdnfeilddotnumber.Value.Substring(7, 2) + ' ' + hdnfeilddotnumber.Value.Substring(9, 2);
            }
            else
            {

                lbldot.Text = hdnfeilddotnumber.Value.Substring(0, 2) + ' ' + hdnfeilddotnumber.Value.Substring(2, 2) + ' ' + hdnfeilddotnumber.Value.Substring(4, 4) + ' ' + hdnfeilddotnumber.Value.Substring(8, 2) + ' ' + hdnfeilddotnumber.Value.Substring(10, 2);
            }



        }

        else if (e.Row.RowType == DataControlRowType.Header)
        {

            Label lblbarcodeheader = e.Row.FindControl("lblBarcodeheader") as Label;
            if (Request.QueryString["LoadId"] != null)
            {
                lblbarcodeheader.Text = "C-BarCode"; //Load #
            }
            else
            {
                lblbarcodeheader.Text = "C-BarCode"; //Lot #
            }

        }


    }
    protected void gvAllTire_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        loadtyres();
    }
    protected void gvAllTire_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        loadtyres();
    }


    #region Generic Product

    private void AddNewProduct(int records)
    {
        try
        {
            Product objProduct = new Product();

            if (dvSubCategory.Visible)
            {
                if (!(Convert.ToInt16(ddlSubCategory.SelectedValue) > 0))
                {
                    lblSubCatError.Text = "Please select product sub category!";
                    return;
                }
                else
                {
                    objProduct.ProductSubCategory = Convert.ToInt16(ddlSubCategory.SelectedValue);
                    lblSubCatError.Text = "";
                }
            }
            else
                objProduct.ProductSubCategory = 0;

            objProduct.Tx_BarCode = 0;
            objProduct.CreatedByID = currentUserInfo.UserId;
            objProduct.DateCreated = DateTime.Now;
            objProduct.LangaugeId = LanguageId;

            objProduct.ProductMaterial = Convert.ToInt32(ddlMaterial.SelectedValue);
            objProduct.ProductShape = Convert.ToInt32(ddlShape.SelectedValue);
            objProduct.ProductSize = Convert.ToInt32(ddlSize.SelectedValue);
            objProduct.BrandId = Convert.ToInt32(ddlBrand.SelectedValue);
            objProduct.OrganizationId = currentUserInfo.OrganizationId;

            objProduct.ProductField1 = txtField1.Text.Trim();
            objProduct.ProductField2 = txtField2.Text.Trim();
            objProduct.ProductField3 = txtField3.Text.Trim();

            objProduct.ProductCategoryId = CatId;

            GenerateBarCodeForProduct();
            objProduct.SerialNumber = hdnBarCodeImageFileName.Value;

            if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value))))
            {
                objProduct.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value)));
            }

            objProduct.ProductId = Product.InsertProdut(objProduct);

            int spaceId = string.IsNullOrEmpty(hidSelectedSpace.Value) ? 0 : Convert.ToInt32(hidSelectedSpace.Value);
            int laneId = string.IsNullOrEmpty(hidSelectedLane.Value) ? 0 : Convert.ToInt32(hidSelectedLane.Value);

            if (Request.QueryString["LoadId"] != null)
            {
                int LoadId = Conversion.ParseInt(Request.QueryString["LoadId"]);
                if (!Loads.InsertLoadTireForReceving(LoadId, objProduct.ProductId, LoginMemberId))
                {
                    lblErrorAddInventory.Text = "Error, Please try again!";
                }
                else
                {
                    if (records == Conversion.ParseInt(txtProductRecordNumber.Text.ToString()))
                    {
                        if (Request.QueryString["OpenLotId"] != null)
                        {
                            Lots.ChangeModifiedDate(Convert.ToInt32(hidLotId.Value));
                        }
                        lblErrorSuccessInventory.Text = "Inventory Added Successfully";
                        ClearProductFields();
                        gvProduct.DataSource = Product.GetAllProductsByLoadId(Convert.ToInt32(Request.QueryString["LoadId"]));
                        gvProduct.DataBind();
                    }

                }
            }
            else
            {
                if (!Lots.insertLotProduct(Convert.ToInt32(hidLotId.Value), objProduct.ProductId, 0, DateTime.Now, 1, true, false, false, spaceId, laneId))
                {
                    lblErrorAddInventory.Text = "Inventory Not Added.";
                }
                else
                {
                    if (records == Convert.ToInt32(txtProductRecordNumber.Text))
                    {
                        lblErrorSuccessInventory.Text = "Inventory Added Successfully";
                        ClearProductFields();
                        gvProduct.DataSource = Product.GetAllProductsByLotId(Convert.ToInt32(hidLotId.Value));
                        gvProduct.DataBind();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddInventory.aspx AddNewProduct", ex);
        }

    }

    private void ClearProductFields()
    {
        txtProductRecordNumber.Text = string.Empty;

        txtField3.Text = string.Empty;
        txtField2.Text = string.Empty;
        txtField1.Text = string.Empty;

        ddlBrand.SelectedIndex = 0;
        ddlSize.SelectedIndex = 0;
        ddlShape.SelectedIndex = 0;
        ddlMaterial.SelectedIndex = 0;
    }

    private void GenerateBarCodeForProduct()
    {
        BarCode br = new BarCode();
        br.DateCreated = DateTime.Now.ToShortDateString();
        br.OrganizationID = UserOrganizationId;
        br.BarCodeNumber = GenerateLotSerialNumber();

        string str = br.BarCodeNumber.ToString().Replace("-", "");

        if (br.GenerateLotBarCodeImage(str))
        {
            hdnBarCodeImageFileName.Value = str + ".gif";
        }
    }

    protected void lnkbtnAddProductInventory_Click(object sender, EventArgs e)
    {
        int NumberOfRecord = Conversion.ParseInt(txtProductRecordNumber.Text);
        for (int i = 1; i <= NumberOfRecord; i++)
        {
            AddNewProduct(i);
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
    }

    protected void lnkBtnClearProductFiedls_Click(object sender, EventArgs e)
    {
        ClearProductFields();
        ddlSize.SelectedIndex = 0;
        ddlShape.SelectedIndex = 0;
        ddlMaterial.SelectedIndex = 0;
        txtProductRecordNumber.Enabled = true;
        ResourceRequiredFieldValidator11.Enabled = true;
        ResourceRequiredFieldValidator11.CssClass = "custom-error";
        ResourceRequiredFieldValidator11.ValidationGroup = "GenerateBarCodeForProduct";
        ResourceRequiredFieldValidator11.ControlToValidate = "txtProductRecordNumber";
        ResourceRequiredFieldValidator11.ErrorControlText = "This field is required";
    }

    protected void lnkbtnDoneProduct_Click(object sender, EventArgs e)
    {
        lnkbtnDoneProduct.Enabled = false;
        if (Request.QueryString["LoadId"] != null)
        {
            if (!AreProductFieldsValid())
            {
                lblErrorAddInventory.Text = "Please fill/clear the form and then click Done.";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                return;
            }

            lnkbtnAddProductInventory_Click(null, null);
            Response.Redirect("inventory-load");

        }
        else
        {
            if (!AreProductFieldsValid())
            {
                lblErrorAddInventory.Text = "Please fill/clear the form and then click Done.";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                return;
            }

            lnkbtnAddProductInventory_Click(null, null);
            Lots.updateLotQuantity(Convert.ToInt32(hidLotId.Value));


            Response.Redirect("lotinfo");
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
    }

    private bool AreProductFieldsValid()
    {
        if (txtProductRecordNumber.Text == string.Empty &&
            ddlSize.SelectedIndex == 0 &&
            ddlShape.SelectedIndex == 0 &&
            ddlMaterial.SelectedIndex == 0 &&
            ddlBrand.SelectedIndex == 0)
            return true;
        else if (
        !(txtProductRecordNumber.Text == string.Empty) &&
            ddlSize.SelectedIndex > 0 &&
            ddlShape.SelectedIndex > 0 &&
            ddlMaterial.SelectedIndex > 0 &&
            ddlBrand.SelectedIndex > 0)
            return true;
        else return false;
    }

    protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "LoadProductInfo")
            {
                LoadProductInfo(Conversion.ParseInt(e.CommandArgument));

            }
            else if (e.CommandName == "DeleteSP")
            {
                if (DeleteTireInfo(Conversion.ParseInt(e.CommandArgument)))
                {
                    lblErrorAddInventory.Text = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddInventory.gvAllTire_RowCommand", ex);
        }
    }

    private void LoadProductInfo(int ProductId)
    {
        lnkbtnAddProductInventory.Visible = false;
        lnkbtnDoneProduct.Visible = false;
        lnkBtnUpdateProduct.Visible = true;
        lnkBtnCancelUpdate.Visible = true;
        hdnProductId.Value = ProductId.ToString();
        Product objProduct = new Product(ProductId);


        ddlSize.Enabled = true;
        ddlShape.Enabled = true;
        ddlMaterial.Enabled = true;
        ddlBrand.Enabled = true;

        if (ddlSubCategory.Visible)
        {


            ddlSubCategory.ClearSelection();
            ddlSubCategory.Items.FindByValue(objProduct.ProductSubCategory.ToString()).Selected = true;

            Utils.GetProductProperties<DropDownList>(ref ddlMaterial, LookUps.ProductMaterial, CatId, LanguageId, Convert.ToInt32(ddlSubCategory.SelectedValue));
            Utils.GetProductProperties<DropDownList>(ref ddlShape, LookUps.ProductShape, CatId, LanguageId, Convert.ToInt32(ddlSubCategory.SelectedValue));
            Utils.GetProductProperties<DropDownList>(ref ddlSize, LookUps.ProductSize, CatId, LanguageId, Convert.ToInt32(ddlSubCategory.SelectedValue));
            Utils.GetProductProperties<DropDownList>(ref ddlBrand, LookUps.ProductBrand, CatId, LanguageId, Convert.ToInt32(ddlSubCategory.SelectedValue));
        }
        ddlSize.ClearSelection();
        ddlSize.Items.FindByValue(objProduct.ProductSize.ToString()).Selected = true;
        ddlShape.ClearSelection();
        ddlShape.Items.FindByValue(objProduct.ProductShape.ToString()).Selected = true;
        ddlMaterial.ClearSelection();
        ddlMaterial.Items.FindByValue(objProduct.ProductMaterial.ToString()).Selected = true;
        ddlBrand.ClearSelection();
        ddlBrand.Items.FindByValue(objProduct.BrandId.ToString()).Selected = true;




        txtProductRecordNumber.Enabled = false;
        txtProductRecordNumber.CssClass = "form-control";
        ResourceRequiredFieldValidator11.Enabled = false;

    }

    protected void lnkBtnUpdateProduct_Click(object sender, EventArgs e)
    {
        UpdateInventory();
    }

    private void UpdateInventory()
    {
        Product objProduct = new Product();

        objProduct.ProductId = Convert.ToInt32(hdnProductId.Value);
        objProduct.ProductMaterial = Convert.ToInt32(ddlMaterial.SelectedValue);
        objProduct.ProductShape = Convert.ToInt32(ddlShape.SelectedValue);
        objProduct.ProductSize = Convert.ToInt32(ddlSize.SelectedValue);
        objProduct.ProductMaterial = Convert.ToInt32(ddlMaterial.SelectedValue);
        objProduct.LangaugeId = LanguageId;
        objProduct.ProductField1 = string.Empty;
        objProduct.ProductField2 = string.Empty;
        objProduct.ProductField3 = string.Empty;
        objProduct.BrandId = Convert.ToInt32(ddlBrand.SelectedValue);
        objProduct.DateCreated = DateTime.Now;

        objProduct.ProductField1 = txtField1.Text.Trim();
        objProduct.ProductField2 = txtField2.Text.Trim();
        objProduct.ProductField3 = txtField3.Text.Trim();

        int ProductId = 0;


        if (Request.QueryString["LoadId"] != null)
        {
            ProductId = Product.UpdateProductInventoryForRecieveTypeLoad(objProduct);
        }
        else
        {
            ProductId = Product.UpdateProductInventory(objProduct, Convert.ToInt32(hidLotId.Value));
        }

        if (ProductId > 0)
        {
            if (Request.QueryString["OpenLotId"] != null)
            {
                Lots.ChangeModifiedDate(Convert.ToInt32(hidLotId.Value));
            }
            gvProduct.DataSource = Product.GetAllProductsByLotId(Convert.ToInt32(hidLotId.Value));
            gvProduct.DataBind();
            lnkbtnAddProductInventory.Visible = true;
            lnkBtnUpdateProduct.Visible = false;
            ClearProductFields();
        }
        else
            lblErrorSuccessInventory.Text = "Inventory Item Updated Successfully";


        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);

        lnkbtnAddProductInventory.Visible = true;
        lnkbtnDoneProduct.Visible = true;
        lnkBtnCancelUpdate.Visible = false;
        lnkBtnUpdateProduct.Visible = false;

        txtProductRecordNumber.Enabled = true;
        ResourceRequiredFieldValidator11.Enabled = true;
        ResourceRequiredFieldValidator11.CssClass = "custom-error";
        ResourceRequiredFieldValidator11.ValidationGroup = "GenerateBarCodeForProduct";
        ResourceRequiredFieldValidator11.ControlToValidate = "txtProductRecordNumber";
        ResourceRequiredFieldValidator11.ErrorControlText = "This field is required";
    }

    protected void lnkBtnCancelUpdate_Click(object sender, EventArgs e)
    {
        lnkbtnAddProductInventory.Visible = true;
        lnkbtnDoneProduct.Visible = true;
        lnkBtnCancelUpdate.Visible = false;
        lnkBtnUpdateProduct.Visible = false;
        ClearProductFields();
        txtProductRecordNumber.Enabled = true;
        ResourceRequiredFieldValidator11.Enabled = true;
        ResourceRequiredFieldValidator11.CssClass = "custom-error";
        ResourceRequiredFieldValidator11.ValidationGroup = "GenerateBarCodeForProduct";
        ResourceRequiredFieldValidator11.ControlToValidate = "txtProductRecordNumber";
        ResourceRequiredFieldValidator11.ErrorControlText = "This field is required";
    }

    protected void lnkAddBrand_Click(object sender, EventArgs e)
    {
        dvBrand.Visible = true;
    }

    protected void lnkSaveBrand_Click(object sender, EventArgs e)
    {
        try
        {
            int returnVal = Product.InsertBrand(txtBrandName.Text.Trim(), CountryIDByLanguageId, CatId);
            if (returnVal > 0)
            {
                lblErrorSuccessInventory.Text = "Brand Added Successfully";
            }
            else
            {
                lblErrorSuccessInventory.Text = "Brand Already exists";
            }
            dvBrand.Visible = false;
            Utils.GetLookUpData<DropDownList>(ref ddlBrand, LookUps.BrandCat, CatId);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddInventory.aspx.cs lnkSaveBrand_Click", ex);
        }
    }

    protected void lnkbtnClear_Click(object sender, EventArgs e)
    {
        dvBrand.Visible = false;
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSubCatError.Visible = false;

        if (ddlSubCategory.SelectedIndex == 0)
        {
            ddlMaterial.Items.Clear();
            ddlShape.Items.Clear();
            ddlSize.Items.Clear();
            ddlBrand.Items.Clear();
            ddlMaterial.Enabled = false;
            ddlShape.Enabled = false;
            ddlSize.Enabled = false;
            ddlBrand.Enabled = false;
            return;
        }
        int SubCat = Convert.ToInt32(ddlSubCategory.SelectedValue.Trim());
        ddlMaterial.Enabled = true;
        ddlShape.Enabled = true;
        ddlSize.Enabled = true;
        ddlBrand.Enabled = true;
        Utils.GetProductProperties<DropDownList>(ref ddlMaterial, LookUps.ProductMaterial, CatId, LanguageId, SubCat);
        Utils.GetProductProperties<DropDownList>(ref ddlShape, LookUps.ProductShape, CatId, LanguageId, SubCat);
        Utils.GetProductProperties<DropDownList>(ref ddlSize, LookUps.ProductSize, CatId, LanguageId, SubCat);
        Utils.GetProductProperties<DropDownList>(ref ddlBrand, LookUps.ProductBrand, CatId, LanguageId, SubCat);
    }

    #endregion


}