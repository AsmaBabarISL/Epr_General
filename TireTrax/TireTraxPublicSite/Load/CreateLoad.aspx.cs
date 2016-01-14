using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Drawing;
using System.Text;
using System.Drawing.Text;
using System.Data;
using System.Collections.Specialized;
using System.Data.SqlClient;
using AjaxControlToolkit;

public partial class Load_CreateLoad : BasePage
{

    public int GlobalCountryID = 235;
    public string tireids = "";
    public string ExistingPteTireIdsString = null;
    public string NONExistingPteTireIdsString = null;


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

        //try
        //{
        //    GridView grd = (GridView)gvAdminInventory;
        //    var list = (List<CheckBox>)Session["test"];

        //    CheckBox chkPrv;
        //    int i = 0;
        //    foreach (GridViewRow row in grd.Rows)
        //    {

        //        chkPrv = (CheckBox)row.FindControl("chkLotSelect");
        //        chkPrv.Checked = list[i].Checked;
        //        i++;
        //    }
        //}
        //catch (Exception exe) { }

        string test = hdnLotBarCodeImageFileName.Value + HiddenField5.Value + hidOrgID.Value + hidText.Value + HiddenField1.Value;
        if (!IsPostBack)
        {



            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
            pageSize = 5;
            Utils.GetLookUpData<DropDownList>(ref ddlLoadType, LookUps.LoadType);
            ddlLoadType.SelectedIndex = 1;
            OrganizationInfo orgInfo = new OrganizationInfo(UserInfo.GetCurrentUserInfo().OrganizationId);
            txtCompanyName.Text = orgInfo.LegalName;
            lnkMultiple_Click(null, null);
            //Session["SelectedLotId"] = "";
            Session["Load"] = "";
            Session["loadId"] = "";

            if (Request.QueryString["loadId"] != null && Request.QueryString["ship"] != null)
            {

                SetShipLoadInfo();
            }
            else
            {
                Session["SelectedLotId"] = "";
                Session["SelectedTiresId"] = "";
                Session["SelectedProductId"] = "";
            }

        }
        else
        {
            pageSize = 5;
            if (TotalItemsR > 0)
            {
                PgrLots.DrawPager(CurrentPage, TotalItemsR, pageSize, MaxPagesToShow);

            }
            if (TotalItemsR > 0)
            {

                this.pgrTires.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
            }
            if (TotalItemsR > 0)
            {
                this.pgrProduct.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
            }



        }

    }

    protected void SetShipLoadInfo()
    {
        try
        {



            divLoads.Visible = true;
            dvLoadOption.Visible = false;

            lnkAccept.Visible = true;
            lnkReject.Visible = true;
            //txtquantity.Enabled = false;
            Loads load = new Loads(Convert.ToInt32(Request.QueryString["loadId"]));
            txtLoadnumber.Text = load.LoadNumber.ToString();
            txtLoadnumber.ReadOnly = true;
            txtquantity.Text = load.Quantity.ToString();
            txtquantity.ReadOnly = true;
            txtponumber.Text = load.POnumber.ToString();
            txtponumber.ReadOnly = true;
            txtinvoicenumber.Text = load.InvoiceNumber.ToString();
            txtinvoicenumber.ReadOnly = true;
            txtsealnumber.Text = load.SealNumber.ToString();
            txtsealnumber.ReadOnly = true;
            txtsealnumber.Text = load.TrailerNumber.ToString();
            txtsealnumber.ReadOnly = true;
            txtweight.Text = load.Weight.ToString();
            txtweight.ReadOnly = true;
            txtladingnumber.Text = load.BillOfLandingNumber.ToString();
            txtladingnumber.ReadOnly = true;
            txttrailernumber.Text = load.TrailerNumber.ToString();
            txttrailernumber.ReadOnly = true;
            txthauleridnumber.Text = new OrganizationInfo(load.HaulerIDNumber).LegalName;
            txthauleridnumber.ReadOnly = true;

            lnkbtnAddCreateLoad.Visible = false;

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "CreateLoad.aspx SetShipLoadInfo()", ex);
        }
    }
    protected void lnkAccept_Click(object sender, EventArgs e)
    {
        try
        {
            txtLoadnumber.Text = string.Empty;
            txtLoadnumber.ReadOnly = false;
            lnkbtnAddCreateLoad.Visible = true;
            lnkAccept.Visible = false;
            lnkReject.Visible = false;
            rfvHaul.Enabled = false;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "CreateLoad.aspx lnkAccept_Click()", ex);
        }
    }
    protected void lnkReject_Click(object sender, EventArgs e)
    {
        try
        {
            Loads.RejectLoadShip(Convert.ToInt32(Request.QueryString["loadId"]), true);
            Response.Redirect("inventory-load");
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "CreateLoad.aspx lnkReject_Click()", ex);
        }
    }
    protected void lnkbtnAddCreateLoad_Click(object sender, EventArgs e)
    {


        if (ddlLoadType.SelectedValue == "74")
        {
            dvSearchHauler.Visible = false;
            rfvHaul.Enabled = false;

            lblerrorPONumber.Text = string.Empty;
            lblErrorInvoiceNumber.Text = string.Empty;
            if (txtponumber.Text.Trim().ToString() == txtsealnumber.Text.Trim().ToString())
            {
                if (txtinvoicenumber.Text.Trim().ToString() == txtladingnumber.Text.Trim().ToString())
                {
                    lblerrorPONumber.Text = "PO Number Seal Number are Same. !";
                    //  lblerrorPONumber.Text = string.Empty;
                    lblErrorInvoiceNumber.Text = "Invoice Number Bill Of Lading Number are Same. !";
                }
                else
                {
                    lblerrorPONumber.Text = "PO Number Seal Number are Same. !";
                }


            }
            else
            {
                lblerrorPONumber.Text = string.Empty;

                if (txtinvoicenumber.Text.Trim().ToString() == txtladingnumber.Text.Trim().ToString())
                {
                    lblErrorInvoiceNumber.Text = "Invoice Number Bill Of Lading Number are Same. !";
                }
                else
                {
                    lblErrorInvoiceNumber.Text = string.Empty;

                    BarCode br = new BarCode();
                    br.DateCreated = DateTime.Now.ToShortDateString();
                    br.OrganizationID = UserOrganizationId;
                    br.BarCodeNumber = GenerateLotSerialNumber();

                    // Guid g = Guid.NewGuid();
                    string str = br.BarCodeNumber.ToString().Replace("-", "");
                    if (br.GenerateLotBarCodeImage(str))
                    {
                        hdnLotBarCodeImageFileName.Value = str + ".gif";
                        imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", str);
                        imgLotBarcode.Visible = true;
                        //txtBrand.Focus();
                    }
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value))))
                    {
                        br.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value)));

                    }

                    lnkbtnAddCreateLoad.Visible = false;
                    lnkContinue.Visible = true;
                    int barcodeId = BarCode.Insert(br);
                    int loadId = 0;

                    loadId = Loads.InsertRecieveLoad(74, txtponumber.Text, txtinvoicenumber.Text, txtsealnumber.Text, txttrailernumber.Text, txtweight.Text, txtladingnumber.Text, UserOrganizationId, LoginMemberId, barcodeId, str, txtLoadnumber.Text, CatId);
                    Response.Redirect("addInventory?LoadId=" + loadId);



                }
            }

        }
        else if (ddlLoadType.SelectedValue == "73" || ddlLoadType.SelectedValue == "75")
        {
            dvSearchHauler.Visible = true;
            rfvHaul.Enabled = true;
            lblerrorPONumber.Text = string.Empty;
            lblErrorInvoiceNumber.Text = string.Empty;
            if (txtponumber.Text.Trim().ToString() == txtsealnumber.Text.Trim().ToString())
            {
                if (txtinvoicenumber.Text.Trim().ToString() == txtladingnumber.Text.Trim().ToString())
                {
                    lblerrorPONumber.Text = "PO Number Seal Number are Same. !";
                    //  lblerrorPONumber.Text = string.Empty;
                    lblErrorInvoiceNumber.Text = "Invoice Number Bill Of Lading Number are Same. !";
                }
                else
                {
                    lblerrorPONumber.Text = "PO Number Seal Number are Same. !";
                }


            }
            else
            {
                lblerrorPONumber.Text = string.Empty;

                if (txtinvoicenumber.Text.Trim().ToString() == txtladingnumber.Text.Trim().ToString())
                {
                    lblErrorInvoiceNumber.Text = "Invoice Number Bill Of Lading Number are Same. !";
                }
                else
                {
                    lblErrorInvoiceNumber.Text = string.Empty;

                    BarCode br = new BarCode();
                    br.DateCreated = DateTime.Now.ToShortDateString();
                    br.OrganizationID = UserOrganizationId;
                    br.BarCodeNumber = GenerateLotSerialNumber();

                    // Guid g = Guid.NewGuid();
                    string str = br.BarCodeNumber.ToString().Replace("-", "");
                    if (br.GenerateLotBarCodeImage(str))
                    {
                        hdnLotBarCodeImageFileName.Value = str + ".gif";
                        imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", str);
                        imgLotBarcode.Visible = true;
                        //txtBrand.Focus();
                    }
                    if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value))))
                    {
                        br.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value)));

                    }

                    lnkbtnAddCreateLoad.Visible = false;
                    lnkContinue.Visible = true;
                    int barcodeId = BarCode.Insert(br);
                    int loadId = 0;

                    if (ddlLoadType.SelectedItem.Text.ToLower().Contains("transfer"))
                    {
                        string loadname = Guid.NewGuid().ToString().Substring(0, 6);

                        loadId = Loads.InsertLoad(Conversion.ParseInt(ddlLoadType.SelectedValue), string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, UserOrganizationId, barcodeId, str, loadname, LoginMemberId, CatId, Conversion.ParseInt(ddlLot.SelectedValue), Conversion.ParseInt(ddlSpace.SelectedValue), Conversion.ParseInt(ddlLane.SelectedValue));
                    }
                    else
                        loadId = Loads.InsertLoad(Conversion.ParseInt(ddlLoadType.SelectedValue), txtponumber.Text.Trim(), txtinvoicenumber.Text.Trim(), txtsealnumber.Text.Trim(), txttrailernumber.Text.Trim(), Conversion.ParseInt(hidOrgID.Value), txtweight.Text.Trim(), txtladingnumber.Text.Trim(), UserOrganizationId, barcodeId, str, txtLoadnumber.Text.Trim(), LoginMemberId, CatId);




                    Session["loadId"] = loadId.ToString(); // success messages are dispaleyed here lblerrorPONumber.Text = "PO Number Seal Number are Same. !";
                }
            }


            // if (txtinvoicenumber.Text.Trim().ToString() == txtladingnumber.Text.Trim().ToString())
            //{
            //    lblerrorPONumber.Text = string.Empty;
            //    lblErrorInvoiceNumber.Text = "Invoice Number Bill Of Lading Number are Same. !";

            //}
            //else
            //{



            //}
        }
        else
        {

            BarCode br = new BarCode();
            br.DateCreated = DateTime.Now.ToShortDateString();
            br.OrganizationID = UserOrganizationId;
            br.BarCodeNumber = GenerateLotSerialNumber();

            // Guid g = Guid.NewGuid();
            string str = br.BarCodeNumber.ToString().Replace("-", "");
            if (br.GenerateLotBarCodeImage(str))
            {
                hdnLotBarCodeImageFileName.Value = str + ".gif";
                imgLoadBarCodeTransfer.ImageUrl = String.Format("/images/temp/{0}.Gif", str);
                imgLoadBarCodeTransfer.Visible = true;


                //txtBrand.Focus();
            }
            if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value))))
            {
                br.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value)));

            }

            lnkbtnAddCreateLoad.Visible = false;
            lnkContinue.Visible = true;
            int barcodeId = BarCode.Insert(br);
            int loadId = 0;


            if (ddlLoadType.SelectedItem.Text.ToLower().Contains("transfer"))
            {
                string loadname = Guid.NewGuid().ToString().Substring(0, 6);
                loadId = Loads.InsertLoad(Conversion.ParseInt(ddlLoadType.SelectedValue), string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, UserOrganizationId, barcodeId, str, loadname, LoginMemberId, CatId, Conversion.ParseInt(ddlLot.SelectedValue), Conversion.ParseInt(ddlSpace.SelectedValue), Conversion.ParseInt(ddlLane.SelectedValue));
            }
            else
                loadId = Loads.InsertLoad(Conversion.ParseInt(ddlLoadType.SelectedValue), txtponumber.Text.Trim(), txtinvoicenumber.Text.Trim(), txtsealnumber.Text.Trim(), txttrailernumber.Text.Trim(), Conversion.ParseInt(hidOrgID.Value), txtweight.Text.Trim(), txtladingnumber.Text.Trim(), UserOrganizationId, barcodeId, str, txtLoadnumber.Text.Trim(), LoginMemberId, CatId);
            Session["loadId"] = loadId.ToString();
        }        /* lot.BarCodeId*/
        //int id = BarCode.Insert(br);



    }
    private void SendNotification(int LoadId)
    {

        Loads loadObject = new Loads(LoadId);

        Notifications objNotifications = new Notifications();
        objNotifications.BitIsActive = true;
        objNotifications.BitIsReaded = false;
        objNotifications.DtmDateCreated = DateTime.Now;
        objNotifications.DtmDateReaded = DateTime.MinValue;
        objNotifications.IntFromOrganizationId = UserOrganizationId;
        objNotifications.IntFromUserId = 0;
        objNotifications.IntNotificationId = 0;
        objNotifications.IntParentNotificationId = 0;
        objNotifications.IntSourceId = 0;
        objNotifications.IntToOrganizationId = Conversion.ParseInt(hidOrgID.Value);
        objNotifications.IntToUserId = 0;
        objNotifications.VchNotificationText = "Load  " + txtLoadnumber.Text.Trim() + "  Ship From  " + loadObject.TransferOrganization + " To " + loadObject.HaulerOrganization + " .";
        objNotifications.InsertUpdate();
    }
    protected void lnkbtnCancelCreateLoad_Click(object sender, EventArgs e)
    {
        Response.Redirect("inventory-load");
    }
    protected void lnkSingle_Click(object sender, EventArgs e)
    {
        dvLoadOption.Visible = false;
        divLoadType.Visible = true;
        Session["Load"] = "single";
        txtquantity.Text = "1";
        txtquantity.Enabled = false;
    }
    protected void lnkMultiple_Click(object sender, EventArgs e)
    {
        dvLoadOption.Visible = false;
        divLoadType.Visible = true;
        Session["Load"] = "multiple";
        txtquantity.Enabled = true;
    }
    protected void ddlLoadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLoadType.SelectedIndex > 0)
        {
            Session["LoadType"] = ddlLoadType.SelectedValue;
            divLoads.Visible = true;

            if (ddlLoadType.SelectedItem.Text.ToLower().Contains("recieve"))
            {

                dvSearchHauler.Visible = false;
                rfvHaul.Enabled = false;
            }
            else if (ddlLoadType.SelectedItem.Text.ToLower().Contains("transfer"))
            {

                rfvPonumber.Enabled = false;
                rfvInvoice.Enabled = false;
                rfvSeal.Enabled = false;
                rfvTrail.Enabled = false;
                rfvHaul.Enabled = false;
                rfvLanding.Enabled = false;
            }
            else
            {
                dvSearchHauler.Visible = true;
                rfvHaul.Enabled = true;
                rfvPonumber.Enabled = true;
                rfvInvoice.Enabled = true;
                rfvSeal.Enabled = true;
                rfvTrail.Enabled = true;
                rfvHaul.Enabled = true;
                rfvLanding.Enabled = true;
            }
            HideControlsForTransfer();
        }
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
    protected void lnkContinue_Click(object sender, EventArgs e)
    {
        Load_AllAdminInventory(1);
        dvLot.Visible = true;
        divLoads.Visible = false;
        //Response.Redirect("/load.aspx");
        //dvLot.Visible = false;
        //dvInventoryAdd.Visible = true;

    }
    protected void Load_AllAdminInventory(int pageNo)
    {
        try
        {



            DataSet ds = Lots.getAllLotsByOrganizationId(pageNo, pageSize, out totalRows, UserOrganizationId, CatId);// UserOrganizationId);
            gvAdminInventory.DataSource = ds;
            gvAdminInventory.DataBind();
            this.TotalItems = totalRows;

            this.PgrLots.DrawPager(pageNo, TotalItems, pageSize, MaxPagesToShow);
            LinkButton lnkBtn = (LinkButton)PgrLots.FindControl("Button_" + pageNo.ToString());
            lnkBtn.Font.Bold = true;

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddLots.aspx Load_AllAdminInventory", ex);
        }
    }
    protected void AdminInventory_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "loadtire")
        {
            if (Session["SelectedLotId"] == "")
                Session["SelectedLotId"] += e.CommandArgument.ToString();
            else
                Session["SelectedLotId"] += "," + e.CommandArgument.ToString();

            Load_TireInventory(1);
        }
    }
    protected void Load_TireInventory(int PageIdCurrent)
    {
        try
        {

            DataSet ds = Tire.getAllTireByLotId(PageIdCurrent, pageSize, out totalRows, Session["SelectedLotId"].ToString());
            gvTires.DataSource = ds;
            gvTires.DataBind();
            //GridPagingTire();

            dvTires.Visible = true;
            if (Session["Load"].ToString() == "single")
            {
                gvTires.Columns[0].Visible = true;
                gvTires.Columns[1].Visible = false;
            }
            else
            {
                gvTires.Columns[1].Visible = true;
                gvTires.Columns[0].Visible = false;
            }
            this.TotalItemsR = totalRows;

            this.pgrTires.DrawPager(PageIdCurrent, TotalItemsR, pageSize, MaxPagesToShow);
            if (gvTires.Rows.Count > 0)
            {
                LinkButton lnkBtn = (LinkButton)pgrTires.FindControl("Button_" + PageIdCurrent.ToString());
                lnkBtn.Font.Bold = true;
            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddLots.aspx Load_TireInventory", ex);
        }
    }
    public void LotSelectMethod(object sender, EventArgs e)
    {

        GridView grd = (GridView)gvAdminInventory;
        HiddenField hid;
        CheckBox chk;


        foreach (GridViewRow row in grd.Rows)
        {

            chk = (CheckBox)row.FindControl("chkLotSelect");

            hid = (HiddenField)row.FindControl("hidLotId");


            if (chk.Checked)
            {
                if (Session["SelectedLotId"] == "")
                    Session["SelectedLotId"] += hid.Value;
                else
                {
                    string str = Conversion.ParseString(Session["SelectedLotId"]);
                    if (str.Contains("," + hid.Value + ","))
                    {
                        Session["SelectedLotId"] = str;
                    }
                    else if (str.Contains("," + hid.Value))
                    {
                        Session["SelectedLotId"] = str;
                    }

                    else if (str.Contains(hid.Value + ","))
                    {
                        Session["SelectedLotId"] = str;
                    }
                    else if (str.Contains(hid.Value))
                    {
                        Session["SelectedLotId"] = str;
                    }
                    else
                        Session["SelectedLotId"] += "," + hid.Value;
                }

            }
            else
            {
                string str = Conversion.ParseString(Session["SelectedLotId"]);
                if (str.Contains("," + hid.Value + ","))
                {
                    str = str.Replace("," + hid.Value + ",", ",");
                    Session["SelectedLotId"] = str;
                }
                else if (str.Contains("," + hid.Value))
                {
                    str = str.Replace("," + hid.Value, "");
                    Session["SelectedLotId"] = str;
                }

                else if (str.Contains(hid.Value + ","))
                {
                    str = str.Replace(hid.Value + ",", "");
                    Session["SelectedLotId"] = str;
                }
                else if (str.Contains(hid.Value))
                {
                    str = str.Replace(hid.Value, "");
                    Session["SelectedLotId"] = str;
                }

            }

        }

        if ((int)ProductCategory.Tire == CatId)
        {
            Load_TireInventory(1);
        }
        else
        {
            Load_ProductInventory(1);
        }
    }

    private void Load_ProductInventory(int PageNo)
    {
        try
        {
            int count = 0;
            DataSet ds = Product.GetProductsByLotIds(pageSize, PageNo, Session["SelectedLotId"].ToString(), out count);
            gvProduct.DataSource = ds;
            gvProduct.DataBind();
            dvProducts.Visible = true;

            this.TotalItemsR = count;

            this.pgrProduct.DrawPager(PageNo, TotalItemsR, pageSize, MaxPagesToShow);
            if (gvProduct.Rows.Count > 0)
            {
                LinkButton lnkBtn = (LinkButton)pgrProduct.FindControl("Button_" + PageNo.ToString());
                lnkBtn.Font.Bold = true;
            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "CreateLoad.aspx Load_ProductInventory", ex);
        }
    }

    public void SelectTireMethod(object sender, EventArgs e)
    {
        //GridView grd = (GridView)gvTires;


        CheckBox chk = (CheckBox)sender;
        //GridViewRow row = (GridViewRow)chk.NamingContainer; 
        HiddenField hid;
        Literal lit;

        //Session["SelectedTiresId"] = "";
        foreach (GridViewRow row in gvTires.Rows)
        {
            chk = (CheckBox)row.FindControl("chkSelectTire");
            hid = (HiddenField)row.FindControl("hidTireId");
            lit = (Literal)row.FindControl("litTireId");


            if (chk.Checked)
            {

                //int stateid = OrganizationInfo.getStateId(UserOrganizationId);
                //int organizaionsubtypeid = new OrganizationInfo(UserOrganizationId).OrganizationSubTypeID;

                //DataSet ds = Loads.GetPteByTireId(stateid, organizaionsubtypeid, Conversion.ParseInt(hid.Value));

                //if(ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                //{


                //}
                //else
                //{
                //    dvPteNotDefined.Visible = true;
                //    chk.Checked = false;
                //    return;
                //}


                if (Session["SelectedTiresId"] == "")
                    Session["SelectedTiresId"] += hid.Value;
                else
                {
                    string str = Conversion.ParseString(Session["SelectedTiresId"]);
                    if (str.Contains("," + hid.Value + ","))
                    {
                        Session["SelectedTiresId"] = str;
                    }
                    else if (str.Contains("," + hid.Value))
                    {
                        Session["SelectedTiresId"] = str;
                    }

                    else if (str.Contains(hid.Value + ","))
                    {
                        Session["SelectedTiresId"] = str;
                    }
                    else if (str.Contains(hid.Value))
                    {
                        Session["SelectedTiresId"] = str;
                    }
                    else
                        Session["SelectedTiresId"] += "," + hid.Value;
                }

            }
            else
            {
                string str = Conversion.ParseString(Session["SelectedTiresId"]);
                if (str.Contains("," + hid.Value + ","))
                {
                    str = str.Replace("," + hid.Value + ",", ",");
                    Session["SelectedTiresId"] = str;
                }
                else if (str.Contains("," + hid.Value))
                {
                    str = str.Replace("," + hid.Value, "");
                    Session["SelectedTiresId"] = str;
                }

                else if (str.Contains(hid.Value + ","))
                {
                    str = str.Replace(hid.Value + ",", "");
                    Session["SelectedTiresId"] = str;
                }
                else if (str.Contains(hid.Value))
                {
                    str = str.Replace(hid.Value, "");
                    Session["SelectedTiresId"] = str;
                }

            }



            //if (chk.Checked)
            //{
            //    if (Session["SelectedTiresId"] == "")
            //        Session["SelectedTiresId"] = Conversion.ParseString(Session["SelectedTiresId"]) + hid.Value;
            //    else
            //        Session["SelectedTiresId"] = Conversion.ParseString(Session["SelectedTiresId"]) + "," + hid.Value;
            //}
            //else
            //{
            //    try
            //    {
            //        string _tireIds = Conversion.ParseString(Session["SelectedTiresId"]);
            //        _tireIds = _tireIds.Replace("\r\n", "").Replace(" ", "").Trim(',');
            //        string removeids = lit.Text;
            //        removeids = removeids.Replace("\r\n", "").Replace(" ", "").Trim(',');

            //        string[] remList = _tireIds.Split(',');

            //        if (remList.Length > 0)
            //            for (int i = 0; i <= remList.Length - 1; i++)
            //            {
            //                if (remList[i].ToString().Contains("," + removeids + ","))
            //                {
            //                    _tireIds = _tireIds.Replace("," + removeids + ",", ",");
            //                    _tireIds = _tireIds.Replace(" ", "").Trim(',');
            //                }
            //                else if (remList[i].ToString().Contains(removeids + ","))
            //                {
            //                    _tireIds = _tireIds.Replace(removeids + ",", ",");
            //                    _tireIds = _tireIds.Replace(" ", "").Trim(',');
            //                }
            //                else if (remList[i].ToString().Contains("," + removeids))
            //                {
            //                    _tireIds = _tireIds.Replace("," + removeids, ",");
            //                    _tireIds = _tireIds.Replace(" ", "").Trim(',');
            //                }
            //                else if (remList[i].ToString().Contains( removeids))
            //                {
            //                    _tireIds = _tireIds.Replace(removeids , ",");
            //                    _tireIds = _tireIds.Replace(" ", "").Trim(',');
            //                }
            //            }
            //        Session["SelectedTiresId"] = _tireIds;
            //    }
            //    catch (Exception ex) { }


            //}
        }

    }

    public void SelectProduct(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        HiddenField hid;
        Literal lit;

        foreach (GridViewRow row in gvProduct.Rows)
        {
            chk = (CheckBox)row.FindControl("chkSelectProduct");
            hid = (HiddenField)row.FindControl("hidProductId");
            lit = (Literal)row.FindControl("litTireId");


            if (chk.Checked)
            {
                if (Session["SelectedProductId"] == "")
                    Session["SelectedProductId"] += hid.Value;
                else
                {
                    string str = Conversion.ParseString(Session["SelectedProductId"]);
                    if (str.Contains("," + hid.Value + ","))
                    {
                        Session["SelectedProductId"] = str;
                    }
                    else if (str.Contains("," + hid.Value))
                    {
                        Session["SelectedProductId"] = str;
                    }

                    else if (str.Contains(hid.Value + ","))
                    {
                        Session["SelectedProductId"] = str;
                    }
                    else if (str.Contains(hid.Value))
                    {
                        Session["SelectedProductId"] = str;
                    }
                    else
                        Session["SelectedProductId"] += "," + hid.Value;
                }

            }
            else
            {
                string str = Conversion.ParseString(Session["SelectedProductId"]);
                if (str.Contains("," + hid.Value + ","))
                {
                    str = str.Replace("," + hid.Value + ",", ",");
                    Session["SelectedProductId"] = str;
                }
                else if (str.Contains("," + hid.Value))
                {
                    str = str.Replace("," + hid.Value, "");
                    Session["SelectedProductId"] = str;
                }

                else if (str.Contains(hid.Value + ","))
                {
                    str = str.Replace(hid.Value + ",", "");
                    Session["SelectedProductId"] = str;
                }
                else if (str.Contains(hid.Value))
                {
                    str = str.Replace(hid.Value, "");
                    Session["SelectedProductId"] = str;
                }

            }
        }

    }

    protected void lnkbtnAddInventory_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["SelectedLotId"] == "")
            {
                lblLotError.Text = "Please Select Any Lot!.";
                return;
            }
            else
                lblLotError.Text = string.Empty;

            StringBuilder str = new StringBuilder(255);
            if ((int)ProductCategory.Tire == CatId)
            {
                str.Append(Conversion.ParseString(Session["SelectedTiresId"]));
            }
            else
            {
                str.Append(Conversion.ParseString(Session["SelectedProductId"]));
            }

            if (str.ToString()[0] == ',')
            {
                str = str.Remove(0, 1);
            }


            //yahan sy 
            string[] arr = str.ToString().Split(',');
            arr = arr.Distinct().ToArray();
            List<string> LotIds = new List<string>();
            foreach (string st in arr)
            {
                string[] arrLot = st.Split(',');
                if (!LotIds.Contains(arrLot[0]))
                {
                    LotIds.Add(arrLot[0]);
                }
            }
            //StringBuilder tireIds = new StringBuilder(255);
            //List<string> TireIds = new List<string>();
            // string[] LotTires = new string[2];

            foreach (string lotid in LotIds)
            {
                int count = 0;
                tireids = "";
                foreach (string id in arr)
                {
                    string[] bothids = id.Split(',');
                    if (bothids[0] == lotid)
                    {
                        if (count == 0)
                        {

                            tireids += bothids[count];
                        }
                        else
                        {
                            tireids += "," + bothids[count];
                        }
                        count++;
                    }
                }

                //yaha tak
                string testtireids = tireids;
                if (string.IsNullOrEmpty(tireids.ToString()))
                {
                    if (CatId == (int)ProductCategory.Tire)
                    {
                        lblTireError.Text = "Please Select Any Tire!.";
                    }
                    else
                    {
                        lblProductError.Text = "Please Select Any Product!.";
                    }

                    return;
                }
                else
                {
                    lblTireError.Text = string.Empty;
                    DataSet ds = null;
                    string ExistingPteTireIds = null;
                    int stateid = OrganizationInfo.getStateId(UserOrganizationId);
                    int organizaionsubtypeid = new OrganizationInfo(UserOrganizationId).OrganizationSubTypeID;
                    string SelectedTiresIds = string.Join(",", arr);
                    if (CatId == (int)ProductCategory.Tire)
                    {
                        ds = Loads.GetPteByTireId(stateid, organizaionsubtypeid, SelectedTiresIds);
                    }
                    else
                    {
                        ds = Loads.GetPteByProductId(stateid, organizaionsubtypeid, SelectedTiresIds, CatId);
                    }


                    foreach (DataRow row in ds.Tables[0].Rows)
                        ExistingPteTireIds += row["ProductId"].ToString() + ",";
                    if (ExistingPteTireIds != null)
                        ExistingPteTireIds = ExistingPteTireIds.Remove(ExistingPteTireIds.Length - 1);
                    //string ExistingPteTireIdsString = string.Join(",", TireIdsForExistingPTE.Select(n => n.ToString()).ToArray());
                    string ExistingPteTireIdsString = string.Join(",", ExistingPteTireIds);
                    string[] NONExistingPteTireIds = SelectedTiresIds.Split(new char[] { ',' }).Except(ExistingPteTireIdsString.Split(new char[] { ',' })).ToArray();
                    NONExistingPteTireIdsString = string.Join(",", NONExistingPteTireIds);
                }

                string tired = Conversion.ParseString(Session["SelectedTiresId"]);
                Loads.AddLoadTires(Convert.ToInt32(Session["loadId"]), tireids, LoginMemberId);
            }
            Loads loadobj = new Loads(Convert.ToInt32(Session["loadId"]));
            if (loadobj.LoadTypeId == 75)
            {
                SendNotification(Convert.ToInt32(Session["loadId"]));
            }

            Response.Redirect("inventory-load", true);

            //if (NONExistingPteTireIdsString != null && NONExistingPteTireIdsString.Length > 0)
            //{

            //    //DataSet dset = null;
            //    //if (CatId == (int)ProductCategory.Tire)
            //    //{
            //    //    dvPteNotDefined.Visible = true;
            //    //    dset = Tire.getSizecodeForTireIds(NONExistingPteTireIdsString);
            //    //    gvSizeCodes.DataSource = dset;
            //    //    gvSizeCodes.DataBind();
            //    //}
            //    //else
            //    //{
            //    //    dvPteNotDefinedProduct.Visible = true;
            //    //    dset = Product.getSizecodeForProductIds(NONExistingPteTireIdsString);
            //    //    gvSizeCodesProduct.DataSource = dset;
            //    //    gvSizeCodesProduct.DataBind();

            //    //    Response.Redirect("inventory-load", true);
            //    //}


            //}
            //else
                

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "createload.lnkbtnAddInventory_Click", ex);
        }
    }

    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.PgrLots.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);
            this.Load_AllAdminInventory(CurrentPage);
        }
        else if (this.pgrTires.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPageR = Convert.ToInt32(cmdArgs.CommandArgument);

            this.Load_TireInventory(CurrentPageR);
        }
        else if (this.pgrProduct.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPageR = Convert.ToInt32(cmdArgs.CommandArgument);
            this.Load_ProductInventory(CurrentPageR);
        }

        return base.OnBubbleEvent(source, args);
    }

    private void HideControlsForTransfer()
    {

        if (ddlLoadType.SelectedItem.Text.ToLower().Contains("recieve"))
        {

            pnlControls.Visible = true;
            pnlDropDowns.Visible = false;
            rvfFacility.Enabled = false;
            rvfLane.Enabled = false;
            rvfLot.Enabled = false;
            rvfSpace.Enabled = false;
            dvSearchHauler.Visible = false;
            rfvHaul.Enabled = false;
        }

        else if (ddlLoadType.SelectedItem.Text.ToLower().Contains("transfer"))
        {
            pnlControls.Visible = false;
            pnlDropDowns.Visible = true;
            BindddlFacility();
            BindddlLots();
            BindddlSpace();
            BindddlLane();
            rvfFacility.Enabled = true;
            rvfLane.Enabled = true;
            rvfLot.Enabled = true;
            rvfSpace.Enabled = true;
        }
        else
        {
            dvSearchHauler.Visible = true;
            rfvHaul.Enabled = true;

            pnlControls.Visible = true;
            pnlDropDowns.Visible = false;
            rvfFacility.Enabled = false;
            rvfLane.Enabled = false;
            rvfLot.Enabled = false;
            rvfSpace.Enabled = false;
        }
    }

    protected void ddlLot_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindddlSpace();
    }

    protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindddlLots();
    }

    protected void ddlSpace_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindddlLane();
    }

    private void BindddlFacility()
    {
        Utils.GetLookUpData<DropDownList>(ref ddlFacility, LookUps.Facility, new SqlParameter[] { new SqlParameter("@intOrganizationID", UserInfo.GetCurrentUserInfo().OrganizationId) });
    }

    private void BindddlLots()
    {
        Utils.GetLookUpData<DropDownList>(ref ddlLot, LookUps.Lot, new SqlParameter[] { new SqlParameter("@intFacilityID", ddlFacility.SelectedValue) });
    }

    private void BindddlSpace()
    {
        Utils.GetLookUpData<DropDownList>(ref ddlSpace, LookUps.Space, new SqlParameter[] { new SqlParameter("@intLotID", ddlLot.SelectedValue) });
    }

    private void BindddlLane()
    {
        Utils.GetLookUpData<DropDownList>(ref ddlLane, LookUps.Lane, new SqlParameter[] { new SqlParameter("@intSpaceID", ddlSpace.SelectedValue) });
    }

    protected void lnkPermanentLot_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidOrgID.Value) && (grvOrganizations.Rows.Count > 0))
        {
            lblErrorPermanentLotdv.Text = "Please select Hauler";
            return;
        }

        else if (grvOrganizations.Rows.Count == 0)
        {
            lblErrorPermanentLotdv.Text = "You Did not have any Organization";
            return;
        }
        txthauleridnumber.Text = hidText.Value;
        dvOrganization.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;
    }

    protected void lnkCancelPermanentLot_Click(object sender, EventArgs e)
    {
        dvOrganization.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;
    }

    protected void lnkParkingLot_Click(object sender, EventArgs e)
    {

        LoadHaulerGrid();
    }

    private void LoadHaulerGrid()
    {
        try
        {
            string standardstewardshipIds = "";
            standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
            OrganizationInfo objorg = new OrganizationInfo(UserOrganizationId);
            string[] arr = standardstewardshipIds.Split(',');
            bool isstewardship = false;
            foreach (var id in arr)
            {
                if (Conversion.ParseInt(id) == objorg.OrganizationTypeId)
                    isstewardship = true;
            }
            //  UserInfo userobj = new UserInfo(LoginMemberId);
            if (isstewardship)
            {
                DataSet ds = Lots.GetAllHaulerbyOrganizationId(UserOrganizationId);
                grvOrganizations.DataSource = ds;
                grvOrganizations.DataBind();
            }
            else
            {
                DataSet ds = Lots.GetOrganizationsbyStewardship(UserOrganizationId, CatId);
                grvOrganizations.DataSource = ds;
                grvOrganizations.DataBind();
                if (!(ds != null && ds.Tables[0].Rows.Count > 0))
                {
                    lnkPermanentLot.Visible = false;                    
                }
            }
            dvOrganization.Visible = true;
            //if (grvOrganizations.Rows.Count == 0)
            //{
                
            //}
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "lotInfo .LoadPermanentGrid", ex);
        }
    }
    protected void gvAdminInventory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView grd = (GridView)gvAdminInventory;
        HiddenField hid;
        CheckBox chk;
        int counter = 0;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!string.IsNullOrEmpty(Session["SelectedLotId"].ToString()))
            {

                chk = (CheckBox)e.Row.FindControl("chkLotSelect");
                hid = (HiddenField)e.Row.FindControl("hidLotId");

                string[] SelectedLotId = Session["SelectedLotId"].ToString().Split(',');
                SelectedLotId = SelectedLotId.Distinct().ToArray();
                if (!string.IsNullOrEmpty(SelectedLotId[counter].ToString()))
                {
                    foreach (String element in SelectedLotId)
                    {
                        if (element.ToString() == hid.Value)
                            chk.Checked = true;
                    }
                }

            }
            counter++;
        }
    }

    protected void gvTires_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        GridView grd = (GridView)gvTires;
        HiddenField hid;
        CheckBox chk;
        int counter = 0;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!string.IsNullOrEmpty(Session["SelectedTiresId"].ToString()))
            {

                chk = (CheckBox)e.Row.FindControl("chkSelectTire");
                hid = (HiddenField)e.Row.FindControl("hidTireId");

                string[] SelectedLotId = Session["SelectedTiresId"].ToString().Split(',');
                SelectedLotId = SelectedLotId.Distinct().ToArray();
                if (!string.IsNullOrEmpty(SelectedLotId[counter].ToString()))
                {
                    foreach (String element in SelectedLotId)
                    {
                        if (element.ToString() == hid.Value)
                            chk.Checked = true;
                    }
                }

            }
            counter++;
        }
    }
    protected void lnkOkBtnForPTeDiv_Click(object sender, EventArgs e)
    {

        //Loads.AddLoadTires(Convert.ToInt32(Session["loadId"]), tireids, LoginMemberId);
        //Loads loadobj = new Loads(Convert.ToInt32(Session["loadId"]));
        //if (loadobj.LoadTypeId == 75)
        //{
        //    SendNotification(Convert.ToInt32(Session["loadId"]));
        //}
        dvPteNotDefined.Visible = false;
        Response.Redirect("inventory-load");
    }

    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void lnkOkBtnForPTeDivProduct_Click(object sender, EventArgs e)
    {
        dvPteNotDefinedProduct.Visible = false;
        Response.Redirect("inventory-load");
    }
}


