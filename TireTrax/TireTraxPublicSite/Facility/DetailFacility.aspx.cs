using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.Drawing;

public partial class Facility_DetailFacility : BasePage
{
    int LotId;
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






    #region DeleteLotWhenNoTires


    protected void lnkCancelLot_Click(object sender, EventArgs e)
    {
        dvLotDeteleInfo.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;
    }
    protected void lnkbtnDeleteLot_Click(object sender, EventArgs e)
    {

        Lots.deleteLot(Convert.ToInt32(Session["OldLotId"]));

        lblErrorDeletedSuccesfullytemp.Text = "Lot Deleted Successfully";
        lblErrorDeletedSuccesfullytemp.Visible = true;
        lblErrorDeletedSuccesfullytemp.CssClass = "alert-success custom-absolute-alert";
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);

        dvLotDeteleInfo.Visible = false;


        Load_AllAdminInventory(1);
        Session["OldLotId"] = "";



    }


    #endregion




    #region ParkingLotFunctionsPopUp


    protected void lnkPermanentLot_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidSelectedLot.Value))
        {
            lblErrorPermanentLotdv.Text = "Please select Parking Lot";
            return;
        }



        LoadSpaces();

        //  LinkButton2.Visible = true;




    }


    private void LoadSpaces()
    {
        try
        {




            string str = hidSelectedLot.Value.Replace(",", "");

            if (Convert.ToInt32(Session["OldLotId"].ToString()) == Convert.ToInt32(str))
            {
                lblErrorPermanentLotdv.Text = "You have slected to Deactivate this Lot. Please shift the tires in any other Lot ";
                lblErrorPermanentLotdv.ForeColor = Color.Red;
                lblTireInfoDisplay.Text = string.Empty;
                return;
            }


            DataSet ds = Lots.getPermanentLotSpace(Convert.ToInt32(str));
            grdSpaces.DataSource = ds;
            grdSpaces.DataBind();
            grdSpaces.Visible = true;
            if (ds == null && ds.Tables.Count <= 0)
            {

                LinkButton2.Visible = false;
            }
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                    LinkButton2.Visible = false;
                else
                    LinkButton2.Visible = true;
            }
            pnlPermanentLot.Visible = true;
            dvParkingLot1.Visible = false;
            dvPermanentLot.Visible = true;
            dvSpace.Visible = true;
            dvlane.Visible = false;

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "lotInfo .LoadSpaces", ex);
        }
    }



    protected void lnkCancel_Click(object sender, EventArgs e)
    {

        Session["SpaceId"] = "";

        Session["OldLotId"] = "";

        pnlPermanentLot.Visible = false;


        dvSpace.Visible = false;
        dvlane.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;
    }


    #endregion

    #region SpaceInfoPopAfterParkingLot

    protected void lnkSpacePerLot_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidSelectedSpace.Value))
        {
            lblErrorPermanentLotSpacedv.Text = "Please select a Row";
            return;
        }



        string str = hidSelectedSpace.Value.Replace(",", "");

        //LotsInfo.TransferTemporaryLot(Convert.ToInt32(Session["TemporaryLotId"]), Convert.ToInt32(str));
        //pnlPermanentLot.Visible = false;
        //dvSpace.Visible = false;
        //dvPermanentLot.Visible = false;
        //Session["TemporaryLotId"] = "";
        //Session["SpaceId"] = "";
        Session["SpaceId"] = str;



        gvlane.Visible = true;

        LoadLanes();
    }



    private void LoadLanes()
    {
        try
        {
            string str = hidSelectedSpace.Value.Replace(",", "");
            DataSet ds = Lots.getPermanentLotSpaceLane(Convert.ToInt32(str));
            gvlane.DataSource = ds;
            gvlane.DataBind();
            if (ds == null && ds.Tables.Count <= 0)
            {

                LinkButton3.Visible = false;
            }
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                    LinkButton3.Visible = false;
                else
                    LinkButton3.Visible = true;
            }



            pnlPermanentLot.Visible = true;
            dvPermanentLot.Visible = true;
            dvSpace.Visible = false;
            dvlane.Visible = true;



        }
        catch (Exception e)
        {
            new SqlLog().InsertSqlLog(0, "DetailFacility.LoadSpaces", e);

        }

    }


    protected void lnkbtnBackSpace_Click(object sender, EventArgs e)
    {
        Utils.SetSelectedIdsGridView(ref grvPermanentLot, "", "Radio1", "", true, hidSelectedLot.Value);

        pnlPermanentLot.Visible = true;
        dvParkingLot1.Visible = true;
        grvPermanentLot.Visible = true;
        lnkCancel1.Visible = true;
        LinkButton1.Visible = true;
        dvlane.Visible = false;

        dvSpace.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;

    }




    #endregion



    #region LaneInfoAfterSpacePopUp



    protected void lnkLanePerLot_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidSelectedLane.Value))
        {
            lblErrorPermanentLotLanedv.Text = "Please select a Space";
            return;
        }


        string str = hidSelectedSpace.Value.Replace(",", "");
        string str2 = hidSelectedLane.Value.Replace(",", "");
        string str3 = hidSelectedLot.Value.Replace(",", "");


        int lotid = Lots.shiftTireByLotId(Convert.ToInt32(Session["OldLotId"].ToString()), Convert.ToInt32(str3), Convert.ToInt32(str), Convert.ToInt32(str2));

        if (lotid > 0)
        {

            pnlPermanentLot.Visible = false;
            dvSpace.Visible = false;
            dvPermanentLot.Visible = true;
            dvParkingLot1.Visible = false;
            Lots.deleteLot(Convert.ToInt32(Session["OldLotId"].ToString()));
            lbldeleteerror.Text = "Tire Shifted Succesfully and Lot Deleted";
            lblErrorDeletedSuccesfullytemp.Text = "Tire Shifted Succesfully and Lot Deactivated";
            Session["SpaceId"] = "";

            //grvPermanentLot.Visible = true;
            //LinkButton1.Visible = true;
            //LinkButton3.Visible = false;
            dvlane.Visible = false;

            Load_AllAdminInventory(1);
            Response.Redirect("lots");



            Session["OldLotId"] = "";



        }

    }




    protected void lnkbtnBackLane_Click(object sender, EventArgs e)
    {
        Utils.SetSelectedIdsGridView(ref grdSpaces, "", "Radio1", "", true, hidSelectedSpace.Value);



        //  LinkButton2.Visible = true;
        grdSpaces.Visible = true;
        dvlane.Visible = false;
        dvSpace.Visible = true;
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;
        lnkCancel2.Visible = true;

        dvParkingLot1.Visible = false;
    }


    #endregion

    protected void btnCancelDetail_Click(object sender, EventArgs e)
    {
        dvSpaceInfo.Visible = false;

    }


   



    #region ParkingLotbyOrganizationIdFunctions

    //Main Funcation Load Lots on Grid

    private void Load_AllAdminInventory(int pageNo)
    {
        try
        {
            lbldeleteerror.Text = string.Empty;
            lblTireInfoDisplay.Text = string.Empty;
            pageSize = 25;// Convert.ToInt32(ddlPageSize.SelectedValue);
            gvAdminInventory.PageSize = pageSize;
            CurrentPage = pageNo;
            string parkinglotnumber = txtParkingLotName.Text.Trim();

            int count = 0;

            DataSet ds = Lots.getParkingLot(pageNo, pageSize, UserOrganizationId, out count, parkinglotnumber);


            gvAdminInventory.DataSource = ds;
            gvAdminInventory.DataBind();
            // GridPaging();
            this.TotalItems = count;
            this.pgrLots.DrawPager(pageNo, TotalItems, pageSize, MaxPagesToShow);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "DetailFacility.aspx Load_AllAdminInventory", ex);
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
        //ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);

        lblErrorDeletedSuccesfullytemp.Text = "";
        if (!IsPostBack)
        {
            ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
            Load_AllAdminInventory(1);
        }
        else if (TotalItems > 0)
        {
            pgrLots.DrawPager(CurrentPage, TotalItems, pageSize, MaxPagesToShow);

        }

    }

    protected override bool OnBubbleEvent(object source, EventArgs args)
    {

        if (this.pgrLots.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPageR = Convert.ToInt32(cmdArgs.CommandArgument);
            this.Load_AllAdminInventory(CurrentPageR);
        }

        return base.OnBubbleEvent(source, args);
    }


    //// Search Lots funcation

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_AllAdminInventory(1);

    }
    protected void gvAdminInventory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtLot = (TextBox)gvAdminInventory.Rows[e.RowIndex].FindControl("txtLotNumber");

        HiddenField lanehdnlot = (HiddenField)gvAdminInventory.Rows[e.RowIndex].FindControl("hdnspaceid");



        int lotid = Convert.ToInt32(lanehdnlot.Value);
        string lotnumber = txtLot.Text.Trim();

        Lots.updateLotByLotId(lotid, lotnumber);
        gvAdminInventory.EditIndex = -1;
        Load_AllAdminInventory(1);
    }

    protected void gvAdminInventory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvAdminInventory.EditIndex = -1;
        Load_AllAdminInventory(1);
    }



    protected void gvAdminInventory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvAdminInventory.EditIndex = e.NewEditIndex;
        Load_AllAdminInventory(1);

    }

    protected void gvAdminInventory_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void gvAdminInventory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvAdminInventory_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }



    //// Rowcommand Function Load Main Lots on PageLot



    protected void gvAdminInventory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {

            LotId = Convert.ToInt32(e.CommandArgument);

        }

        else if (e.CommandName == "LotRedirect")
        {
            Response.Redirect("inventory-tire?tireid=" + e.CommandArgument + "&islot=true");
            // Server.Transfer("inventory-tire.aspx?tireid="+e.CommandArgument+"&islot=true",true);
            //  HttpContext.Current.RewritePath("inventory-tire.aspx?tireid=" + e.CommandArgument + "&islot=true");
        }
        else if (e.CommandName == "SpaceInfo")
        {
            gvSpace.DataSource =LotRows.getSpacesbyLotId(Convert.ToInt32(e.CommandArgument));
            gvSpace.DataBind();
            //ScriptManager.RegisterStartupScript(this, GetType(), "DataInfo", "ShowSpaceInfo();", true);
            Lots objlots = new Lots(Conversion.ParseInt(e.CommandArgument));
            lblLotNumber.Text = objlots.LotNumber;
            
            dvLotDeteleInfo.Visible = false;



            pnlPermanentLot.Visible = false;
            dvSpaceInfo.Visible = true;

        }

        else if (e.CommandName == "Cancel")
        {

            gvAdminInventory.EditIndex = -1;
            Load_AllAdminInventory(1);
        }
        else if (e.CommandName == "Delete")
        {
            int tirecount = Lots.gettirecount(Convert.ToInt32(e.CommandArgument));


             if (tirecount > 0)
            {
                dvLotDeteleInfo.Visible = false;
                lblTireInfoDisplay.Text = "Inventory must be transferred to a new or existing Storage Location before this Storage Lot can be Deactivated";
                lblTireInfoDisplay.ForeColor = Color.Red;
                Session["TemporaryLotId"] = "";
                Session["SpaceId"] = "";
                Session["OldLotId"] = e.CommandArgument.ToString();
                hidSelectedOrgId.Value = e.CommandArgument.ToString();
                LoadPermanentGrid();



                //ScriptManager.RegisterStartupScript(this, GetType(), "DataInfo", "ShowDeleteLotInfo();", true);



            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "DataInfo", "ShowDeleteLotInfo();", true);

                pnlPermanentLot.Visible = false;
                dvLotDeteleInfo.Visible = true;
                Session["OldLotId"] = e.CommandArgument.ToString();
                //Space.DeleteLot(Convert.ToInt32(e.CommandArgument));
                // LoadPermanentGrid();
                //ScriptManager.RegisterStartupScript(this, GetType(), "DataInfo", "ShowLotInfo();", true);

                //Load_AllAdminInventory(1);



            }

            ////Space.DeleteLot(Convert.ToInt32(e.CommandArgument));
            //Load_AllAdminInventory(1);

        }



    }


    #endregion



    /// <summary>
    /// funcation If Lots Tire Greater Then 0 then Pop Load All Lots Info In Which We Shift Old Lot Tire To New Lots
    /// </summary>

    private void LoadPermanentGrid()
    {
        try
        {

            Load_AllPermanentLot(1);
            //grvPermanentLot.DataSource = ds;
            //grvPermanentLot.DataBind();
            DataSet dscount = Tire.getPermenentLotTiresCount(UserOrganizationId,CatId);
            lblTotalTire.Text = dscount.Tables[0].Rows[0][0].ToString();
            pnlPermanentLot.Visible = true;
            dvPermanentLot.Visible = true;
            grvPermanentLot.Visible = true;
            dvParkingLot1.Visible = true;

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "DetailFacility .LoadPermanentGrid", ex);
        }
    }

    private void Load_AllPermanentLot(int pageNo)
    {

        pageSize = 25;// Convert.ToInt32(ddlPageSize.SelectedValue);
        grvPermanentLot.PageSize = pageSize;
        CurrentPage = pageNo;
        int count = 0;
        DataSet ds = Lots.getPermanentLot(pageNo, pageSize, out count, UserOrganizationId,CatId);

        this.TotalItemsR = count;
        this.pgrLoad.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);

        grvPermanentLot.DataSource = ds;
        grvPermanentLot.DataBind();
    }
}