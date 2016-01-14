using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TireTraxLib;

public partial class Load_EditLoad :BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
        if (!IsPostBack)
        {
            LoadEditInfoOnLoadPage();
        }



  
    }





    private void LoadEditInfoOnLoadPage()
    {
        int loadid = Conversion.ParseInt(Request.QueryString["LoadId"]);

        Loads objLoad = new Loads(loadid);
        Utils.GetLookUpData<DropDownList>(ref ddlLoadType, LookUps.LoadType);
        ddlLoadType.SelectedValue = objLoad.LoadTypeId.ToString();

        if (objLoad.LoadTypeId == 73 || objLoad.LoadTypeId == 75)
        {

            BarCode objBarCode = new BarCode(objLoad.BarcodeId);




            txtCompanyName.Text = UserInfo.GetCurrentUserInfo().FullName;
            txtLoadnumber.Text = objLoad.LoadNumber;
            hidOrgID.Value = objLoad.HaulerIDNumber.ToString();
            txtponumber.Text = objLoad.POnumber;
            txtinvoicenumber.Text = objLoad.InvoiceNumber;
            txtsealnumber.Text = objLoad.SealNumber;
            txttrailernumber.Text = objLoad.TrailerNumber;
            txthauleridnumber.Text = objLoad.HaulerOrganization;
            txtweight.Text = objLoad.Weight;
            txtladingnumber.Text = objLoad.BillOfLandingNumber;

            hdnLotBarCodeImageFileName.Value = objBarCode.BarCodeNumber + ".gif";
            imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", objBarCode.BarCodeNumber);
            imgLotBarcode.Visible = true;
        }
        else if (objLoad.LoadTypeId == 72)
        {
            BarCode objBarCode = new BarCode(objLoad.BarcodeId);

            pnlControls.Visible = false;
            pnlDropDowns.Visible = true;
            BindddlFacility();


            BindddlLane();
            txtCompanyName.Text = UserInfo.GetCurrentUserInfo().FullName;
            ddlFacility.SelectedValue = objLoad.FacilityId.ToString();
            BindddlLots();
            ddlLot.SelectedValue = objLoad.LotID.ToString();
            BindddlSpace();
            ddlSpace.SelectedValue = objLoad.SpaceId.ToString();
            BindddlLane();
            ddlLane.SelectedValue = objLoad.Lane;

            hdnLotBarCodeImageFileName.Value = objBarCode.BarCodeNumber + ".gif";
            imgLoadBarCodeTransfer.ImageUrl = String.Format("/images/temp/{0}.Gif", objBarCode.BarCodeNumber);
            imgLoadBarCodeTransfer.Visible = true;

        }
        else
        {
            BarCode objBarCode = new BarCode(objLoad.BarcodeId);

            pnlControls.Visible = true;
            pnlDropDowns.Visible = false;
            dvSearchHauler1.Visible = false;
            txtCompanyName.Text = UserInfo.GetCurrentUserInfo().FullName;
            txtLoadnumber.Text = objLoad.LoadNumber;

            txtponumber.Text = objLoad.POnumber;
            txtinvoicenumber.Text = objLoad.InvoiceNumber;
            txtsealnumber.Text = objLoad.SealNumber;
            txttrailernumber.Text = objLoad.TrailerNumber;
            txthauleridnumber.Text = objLoad.HaulerOrganization;
            txtweight.Text = objLoad.Weight;
            txtladingnumber.Text = objLoad.BillOfLandingNumber;

            hdnLotBarCodeImageFileName.Value = objBarCode.BarCodeNumber + ".gif";
            imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", objBarCode.BarCodeNumber);
            imgLotBarcode.Visible = true;
            hidOrgID.Value = objLoad.HaulerIDNumber.ToString();

        }

    }




    protected void ddlLoadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLoadType.SelectedIndex > 0)
        {
            Session["LoadType"] = ddlLoadType.SelectedValue;
            divLoads.Visible = true;
            //divLoadType.Visible = false;
            if (ddlLoadType.SelectedValue=="74")
            {

                dvSearchHauler1.Visible = false;
                rfvHaul.Enabled = false;
            }
            else if (ddlLoadType.SelectedValue=="72")
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
                dvSearchHauler1.Visible = true;
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


    private void HideControlsForTransfer()
    {
        int loadid = Conversion.ParseInt(Request.QueryString["LoadId"]);

        Loads objLoad = new Loads(loadid);
        BarCode objBarCode = new BarCode(objLoad.BarcodeId);

        if (ddlLoadType.SelectedValue=="74")
        {

            pnlControls.Visible = true;
            pnlDropDowns.Visible = false;
            rvfFacility.Enabled = false;
            rvfLane.Enabled = false;
            rvfLot.Enabled = false;
            rvfSpace.Enabled = false;
            dvSearchHauler1.Visible = false;
            rfvHaul.Enabled = false;
            hdnLotBarCodeImageFileName.Value = objBarCode.BarCodeNumber + ".gif";
            imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", objBarCode.BarCodeNumber);
            imgLotBarcode.Visible = true;
           
        }

        else if (ddlLoadType.SelectedValue=="72")
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
           
            hdnLotBarCodeImageFileName.Value = objBarCode.BarCodeNumber + ".gif";
            imgLoadBarCodeTransfer.ImageUrl = String.Format("/images/temp/{0}.Gif", objBarCode.BarCodeNumber);
            imgLoadBarCodeTransfer.Visible = true;
          
        }
        else 
        {
            dvSearchHauler1.Visible = true;
            rfvHaul.Enabled = true;

            pnlControls.Visible = true;
            pnlDropDowns.Visible = false;
            rvfFacility.Enabled = false;
            rvfLane.Enabled = false;
            rvfLot.Enabled = false;
            rvfSpace.Enabled = false;
            hdnLotBarCodeImageFileName.Value = objBarCode.BarCodeNumber + ".gif";
            imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", objBarCode.BarCodeNumber);
            imgLotBarcode.Visible = true;
          
        }
    }


    protected void lnkAddNewHauler_Click(object sender, EventArgs e)
    {


        dvParkingLot1.Visible = false;
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
                DataSet ds = Lots.GetOrganizationsbyStewardship(UserOrganizationId,CatId);
                grvOrganizations.DataSource = ds;
                grvOrganizations.DataBind();
            }
            dvOrganization.Visible = true;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "lotInfo .LoadPermanentGrid", ex);
        }
    }







    protected void lnkbtnAddCreateLoad_Click(object sender, EventArgs e)
    {
        int loadid = Conversion.ParseInt(Request.QueryString["LoadId"]);

            Loads objLoad = new Loads(loadid);
            BarCode objBarCode = new BarCode(objLoad.BarcodeId);
        if (ddlLoadType.SelectedValue == "74")
        {
            dvSearchHauler1.Visible = false;
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

                    //BarCode br = new BarCode();
                    //br.DateCreated = DateTime.Now.ToShortDateString();
                    //br.OrganizationID = UserOrganizationId;
                    //br.BarCodeNumber = GenerateLotSerialNumber();

                    //// Guid g = Guid.NewGuid();
                    //string str = br.BarCodeNumber.ToString().Replace("-", "");
                    //if (br.GenerateLotBarCodeImage(str))
                    //{
                    //    hdnLotBarCodeImageFileName.Value = str + ".gif";
                    //    imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", str);
                    //    imgLotBarcode.Visible = true;
                    //    //txtBrand.Focus();
                    //}
                    //if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value))))
                    //{
                    //    br.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value)));

                    //}

                    
                    //lnkContinue.Visible = true;
                    //int barcodeId = BarCode.Insert(br);
                    //int loadId = 0;
                    Loads.updateRecieveLoad(loadid, Conversion.ParseInt(ddlLoadType.SelectedValue),txtponumber.Text, txtinvoicenumber.Text, txtsealnumber.Text, txttrailernumber.Text, txtweight.Text, txtladingnumber.Text, UserOrganizationId,LoginMemberId, objLoad.BarcodeId, objBarCode.BarCodeNumber, txtLoadnumber.Text);
                   
                    Response.Redirect("inventory-load");



                }
            }

        }
        else if (ddlLoadType.SelectedValue == "73" || ddlLoadType.SelectedValue == "75")
        {


            dvSearchHauler1.Visible = true;
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

                    //BarCode br = new BarCode();
                    //br.DateCreated = DateTime.Now.ToShortDateString();
                    //br.OrganizationID = UserOrganizationId;
                    //br.BarCodeNumber = GenerateLotSerialNumber();

                    //// Guid g = Guid.NewGuid();
                    //string str = br.BarCodeNumber.ToString().Replace("-", "");
                    //if (br.GenerateLotBarCodeImage(str))
                    //{
                    //    hdnLotBarCodeImageFileName.Value = str + ".gif";
                    //    imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", str);
                    //    imgLotBarcode.Visible = true;
                    //    //txtBrand.Focus();
                    //}
                    //if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value))))
                    //{
                    //    br.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value)));

                    //}

                    //lnkbtnAddCreateLoad.Visible = false;
                    ////lnkContinue.Visible = true;
                    //int barcodeId = BarCode.Insert(br);
                    //int loadId = 0;

                    if (Conversion.ParseInt(hidOrgID.Value) == UserOrganizationId)
                    {
                        lblHaulerError.Text = "Please Select Any other Hauler Name";
                        return;
                    }
                    else
                    {
                        lblHaulerError.Text = string.Empty;
                        Loads.updatePendingAndShipLoad(loadid, Conversion.ParseInt(ddlLoadType.SelectedValue), txtponumber.Text.Trim(), txtinvoicenumber.Text.Trim(), txtsealnumber.Text.Trim(), txttrailernumber.Text.Trim(), txtweight.Text.Trim(), txtladingnumber.Text.Trim(), UserOrganizationId, LoginMemberId, Conversion.ParseInt(hidOrgID.Value), objLoad.BarcodeId, objBarCode.BarCodeNumber, txtLoadnumber.Text.Trim());
                        if (Conversion.ParseInt(ddlLoadType.SelectedValue) == 75)
                        {
                            SendNotification(loadid);
                        }
                        Response.Redirect("inventory-load");
                    }
                       
                }
            }


     
        }
        else
        {

            //BarCode br = new BarCode();
            //br.DateCreated = DateTime.Now.ToShortDateString();
            //br.OrganizationID = UserOrganizationId;
            //br.BarCodeNumber = GenerateLotSerialNumber();

            //// Guid g = Guid.NewGuid();
            //string str = br.BarCodeNumber.ToString().Replace("-", "");
            //if (br.GenerateLotBarCodeImage(str))
            //{
            //    hdnLotBarCodeImageFileName.Value = str + ".gif";
            //    imgLoadBarCodeTransfer.ImageUrl = String.Format("/images/temp/{0}.Gif", str);
            //    imgLoadBarCodeTransfer.Visible = true;


            //    //txtBrand.Focus();
            //}
            //if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value))))
            //{
            //    br.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value)));

            //}

            //lnkbtnAddCreateLoad.Visible = false;
            //lnkContinue.Visible = true;
            //int barcodeId = BarCode.Insert(br);
            //int loadId = 0;


            if (ddlLoadType.SelectedValue =="72")
            {
                Loads.updateTransferLoad(loadid,Conversion.ParseInt(ddlLoadType.SelectedValue), Conversion.ParseInt(ddlLot.SelectedValue), Conversion.ParseInt(ddlSpace.SelectedValue), Conversion.ParseInt(ddlLane.SelectedValue), UserOrganizationId,LoginMemberId,objLoad.BarcodeId, objLoad.LoadNumber);
                Response.Redirect("inventory-load");

            }
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

    protected void lnkbtnCancelCreateLoad_Click(object sender, EventArgs e)
    {
        Response.Redirect("inventory-load");
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
    protected void grvOrganizations_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            RadioButton rbtHualerNumberId = (RadioButton)e.Row.FindControl("Radio1");
            HiddenField hdnHaulerId = (HiddenField)e.Row.FindControl("hdnhaulerId");


            if (hdnHaulerId.Value == hidOrgID.Value)
            {
                rbtHualerNumberId.Checked = true;


            }
            else
            {
                rbtHualerNumberId.Checked = false;
            }
        }

    }
}
