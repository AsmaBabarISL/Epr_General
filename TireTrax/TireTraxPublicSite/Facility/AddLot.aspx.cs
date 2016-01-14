using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Text;

public partial class Facility_AddLot : BasePage
{
    int SpaceId, LaneId;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorLot.Text = "";
        lblErrorSpace.Text = "";
        lblErrorLane.Text = "";
        lbldeleteError.Text = "";

        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
        ViewState["spaceid"] = ViewState["spaceid"] == null ? 0 : ViewState["spaceid"];

        pageSize = 5;
        if (!IsPostBack)
        {

            //lblErrorLot.Visible = false;
            if (Request.QueryString["lotid"] != null)
            {
                hidLotId.Value = Request.QueryString["lotid"];
                pnlSpace.Visible = true;
                pnlLot.Visible = false;
                pnlLane.Visible = false;
                LoadRows(1);
                DataSet ds = Lots.getParkingLotNumberByLotId(Convert.ToInt32(hidLotId.Value));
                //lblLotNumber.Visible = true;
                lblLotNumberSpace.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            else if (Request.QueryString["fid"] != null)
            {
                hndfacilityId.Value = Request.QueryString["fid"];

                DataSet ds = Facility.GetFacilityNameByFacilityId(Conversion.ParseInt(hndfacilityId.Value));
                lblfacilityname.Text = ds.Tables[0].Rows[0][0].ToString();

                LoadLots(1, Conversion.ParseInt(hndfacilityId.Value));


            }
            else if (Request.QueryString["fids"] != null)
            {
                hndfacilityId.Value = Request.QueryString["fids"];
                DataSet ds = Facility.GetFacilityNameByFacilityId(Conversion.ParseInt(hndfacilityId.Value));
                lblfacilityname.Text = ds.Tables[0].Rows[0][0].ToString();

                LoadLots(1, Conversion.ParseInt(hndfacilityId.Value));


            }


            else
            {
                LoadLots(1);
            }

        }
        else
        {
            if (TotalItems > 0)
            {
                pgrLots.DrawPager(CurrentPage, TotalItems, pageSize, MaxPagesToShow);

            }

          if (TotalItemsR > 0)
            {
                pgrRows.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
           }

           if (TotalItemsR2 > 0)
            {
                pgrSpaces.DrawPager(CurrentPageR2, TotalItemsR2, pageSize, MaxPagesToShow);

            }
        }
    }



    protected override bool OnBubbleEvent(object source, EventArgs args)
    {

        if (this.pgrLots.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Conversion.ParseInt(cmdArgs.CommandArgument);

            this.LoadLots(CurrentPage, Conversion.ParseInt(hndfacilityId.Value));
        }
        else if (this.pgrSpaces.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            int pageid = Conversion.ParseInt(cmdArgs.CommandArgument);
            CurrentPageR2 =pageid;
            int spaceid = Convert.ToInt32(ViewState["spaceid"]);

            this.LoadSpaces(CurrentPageR2, spaceid);
        }
        else if (this.pgrRows.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPageR = Conversion.ParseInt(cmdArgs.CommandArgument);
            

            this.LoadRows(CurrentPageR);
        }
        return base.OnBubbleEvent(source, args);
    }



    /// <summary>
    /// Lots Load Again Facility Id
    /// </summary>
    /// <param name="pageNo"></param>
    /// <param name="facilityid"></param>
    public void LoadLots(int pageNo, int facilityid = 0)
    {

        try
        {
            int pageSize = 5;
            gvLot.PageSize = pageSize;
            CurrentPage = pageNo;
       

            int count = 0;

            DataSet ds = Lots.getParkingLot(pageNo, pageSize, UserOrganizationId, out count, null, Conversion.ParseInt(hndfacilityId.Value));


            gvLot.DataSource = ds;
            gvLot.DataBind();

            this.TotalItems = count;
            this.pgrLots.DrawPager(pageNo, this.TotalItems, pageSize, MaxPagesToShow);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddLot.aspx LoadLots", ex);
        }
    }



    protected void gvSetting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {

        }

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == gvSetting.EditIndex)
        {

        }
    }
    protected void gvSetting_DataBound(object sender, EventArgs e)
    {

    }
    protected void gvSetting_RowEditing(object sender, GridViewEditEventArgs e)
    {


        gvSetting.EditIndex = e.NewEditIndex;
        LoadRows(1);
    }

    /// <summary>
    /// Load Rows Info
    /// 
    /// </summary>

    public void LoadRows(int pageNo)
    {
        //int lotid = 90;
        //string.IsNullOrEmpty(hidLotId.Value) ? 0 : Convert.ToInt32(hidLotId.Value);
        int pageSize = 5;
        gvSetting.PageSize = pageSize;
        CurrentPageR = pageNo;
        try
        {
            int count = 0;
            int lotid = Convert.ToInt32(hidLotId.Value);
            DataSet ds = LotRows.getLotSpacesByLotId(pageNo,pageSize, out count, lotid);
            gvSetting.DataSource = ds.Tables[0];
            gvSetting.DataBind();
            this.TotalItemsR = count;
            this.pgrRows.DrawPager(pageNo, this.TotalItemsR, pageSize, MaxPagesToShow);


        }
        catch (Exception ex)
        {

            new SqlLog().InsertSqlLog(0, "lotinfo.aspx loadGridAndHeaderText", ex);
        }



    }


    /// <summary>
    /// Load Space Info Function
    /// </summary>
    /// <param name="spaceid"></param>

    public void LoadSpaces(int pageNo,int spaceid)
    {
        try
        {
            int pageSize = 5;
            gvLane.PageSize = pageSize;
            CurrentPageR2 = pageNo;
            int count = 0;
            gvLane.DataSource = LotSpace.getLane(pageNo,pageSize,out count,spaceid);
            gvLane.DataBind();
            this.TotalItemsR2 = count;
            this.pgrSpaces.DrawPager(pageNo, this.TotalItemsR2, pageSize, MaxPagesToShow);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddLot.aspx loadLaneText", ex);
        }
    


    }
    


    /// <summary>
    /// Start of Rows Grid in Rows
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>


    protected void gvSetting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            SpaceId = Convert.ToInt32(e.CommandArgument);
        }

        else if (e.CommandName == "Delete")
        {
            int spaceId = Convert.ToInt32(e.CommandArgument);
            if (!rowhaveTire(spaceId))
            {
                SpaceId = Convert.ToInt32(spaceId);
                lblErrorLot.Text = string.Empty;
                LotRows.deleteSpaces(SpaceId);
                LoadRows(1);
                lblErrorLane.Text = string.Empty;
                lblErrorLot.Text = string.Empty;
                lblErrorSpace.Text = string.Empty;
            }
        }
        else if (e.CommandName == "AddMore")
        {

            LinkButton lnkbtnAddMoreSetting = gvSetting.FooterRow.FindControl("lnkbtnAddMore") as LinkButton;
            LinkButton lnkbtnAddSetting = gvSetting.FooterRow.FindControl("lnkbtnAddSetting") as LinkButton;
            LinkButton lnkbtnCancelSetting = gvSetting.FooterRow.FindControl("lnkbtnCancelSetting") as LinkButton;


            lnkbtnAddSetting.Visible = true;
            lnkbtnAddMoreSetting.Visible = false;
            lnkbtnCancelSetting.Visible = true;

            TextBox txtSpaceFooter = gvSetting.FooterRow.FindControl("txtSpacesfooter") as TextBox;
            //TextBox txtSpace = gvSetting.FooterRow.FindControl("txtSpaces") as TextBox;
            txtSpaceFooter.Visible = true;

            //txtSpace.Visible = true;

        }
        else if (e.CommandName == "CancelSetting")
        {
            gvSetting.EditIndex = -1;
            LoadRows(1);
        }
       


        else if (e.CommandName == "Insert")
        {

            TextBox txtSpaceFooter = gvSetting.FooterRow.FindControl("txtSpacesfooter") as TextBox;
            //HiddenField hiddenfieldspaceid = this.FindControl("hdnspaceid") as HiddenField;



            //int index = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = gvSetting.Rows[index];
            //HiddenField hiddenfieldspaceid = (HiddenField)row.FindControl("hdnspaceid");

            LotRows spc = new LotRows();
            spc.SpaceName = txtSpaceFooter.Text.Trim();
            spc.IsActive = true;
            spc.DateCreated = DateTime.Now;
            spc.Description = string.Empty;
            int temp = LotRows.insertUpdate(spc, Convert.ToInt32(hidLotId.Value));
            ViewState["spaceid"] = temp;
            //ViewState["spaceid"] = temp;

            LoadRows(1);



        }
        else if (e.CommandName == "SpaceInfoPopUp")
        {
                   
                   ucFacilitySpaces.Visible=true;
           

            ViewState["spaceid"] = e.CommandArgument;
       
            lblErrorSpace.Text = String.Empty;
            dvpopupfacilityinfo.Visible = true;
            //pnlLot.Visible = false;
            //pnlSpace.Visible = false;
            //pnlLane.Visible = true;
            //PanelLaneValue.Visible = true;
            //addmoreLane.Visible = false;
            //gvLane.Visible = true;
            //lblfacilityForLane.Text = lblfacilityname.Text;

            //lblLotNumberLane.Text = lblLotNumberSpace.Text;
            Lots objLots = new Lots(Conversion.ParseInt(hidLotId.Value));

            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            Label lbl = (Label)gvSetting.Rows[RemoveAt].FindControl("lblspacename");



            ucFacilitySpaces.loadFacilityLotAndRowName(objLots.FacilityName, objLots.LotNumber, lbl.Text, Convert.ToInt32(hidLotId.Value), Conversion.ParseInt(hdnidfaclityid.Value));
            ucFacilitySpaces.loadLaneText(Convert.ToInt32(e.CommandArgument));

        }
        else if (e.CommandName == "AddSpaceLane")
        {
            ViewState["spaceid"] = e.CommandArgument;
            lblErrorLane.Text = String.Empty;
            lblErrorLot.Text = String.Empty;
            lblErrorSpace.Text = String.Empty;
            pnlLot.Visible = false;
            pnlSpace.Visible = false;
            pnlLane.Visible = true;
            //PanelLaneValue.Visible = true;
            //addmoreLane.Visible = false;
            //gvLane.Visible = true;
            lblfacilityForLane.Text = lblfacilityname.Text;

            lblLotNumberLane.Text = lblLotNumberSpace.Text;
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            Label lbl = (Label)gvSetting.Rows[RemoveAt].FindControl("lblspacename");
            lblSpaceName.Text = lbl.Text;
            // DataSet ds = Space.GetParkingLotNumberByLotId(Convert.ToInt32(hidLotId.Value));

            // Label lbl = (Label)gvSetting.Rows[RemoveAt].FindControl("lblspacename");

            LoadSpaces(1,Convert.ToInt32(e.CommandArgument));

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

        //TextBox txtSpaceFooter = gvSetting.FooterRow.FindControl("txtSpacesfooter") as TextBox;
        //HiddenField lanehdnspace=gvSetting.FooterRow.FindControl("hdnspaceid1") as HiddenField;
        TextBox txtSpace = (TextBox)gvSetting.Rows[e.RowIndex].FindControl("txtSpaces");

        HiddenField lanehdnspace = (HiddenField)gvSetting.Rows[e.RowIndex].FindControl("hdnspaceid1");


        LotRows spc = new LotRows();
        spc.SpaceId = Convert.ToInt32(lanehdnspace.Value);
        spc.SpaceName = txtSpace.Text.Trim();
        spc.IsActive = true;
        spc.DateCreated = DateTime.Now;
        spc.Description = string.Empty;
        LotRows.insertUpdate(spc, Convert.ToInt32(hidLotId.Value));
        lblErrorSpace.Text = "Row Name Updated Successfully!";
        lblErrorSpace.Visible = true;
        lblErrorSpace.CssClass = "alert-success custom-absolute-alert";
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        gvSetting.EditIndex = -1;
        LoadRows(1);
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
        LoadRows(1);

    }


    /// <summary>
    /// ENd of Rows Grid Info
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>


    protected void lbkCancel_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["fid"] != null)
        {
            Response.Redirect("facility");
        }
        else if (Request.QueryString["fids"] != null)
        {
            Response.Redirect("lots");
        }

    }





    protected void lnkbtnAddLane_Click(object sender, EventArgs e)
    {


        //TextBox txtSpaceFooter = gvSetting.FooterRow.FindControl("txtLanes") as TextBox;
        //String lanename = txtLaneValue.Text.Trim();
        int spaceid = Convert.ToInt32(ViewState["spaceid"]);
        //Space.InsertLane(lanename, spaceid);
        LoadSpaces(1, spaceid);


    }








    /// <summary>
    /// Done Button Click For Rows
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbkSaveSpace_Click(object sender, EventArgs e)
    {
        lblErrorLane.Text = string.Empty;
        lblErrorLot.Text = string.Empty;
        lblErrorSpace.Text = string.Empty;

        LotRows spc = new LotRows();
        spc.SpaceName = txtSpaceheader.Text.Trim();
        spc.IsActive = true;
        spc.DateCreated = DateTime.Now;
        spc.Description = string.Empty;
        int id = LotRows.insertUpdate(spc, Convert.ToInt32(hidLotId.Value));


        LoadRows(1);
        if (id > 0)
        {
            lblErrorSpace.Text = "Row added successfully!";
            lblErrorSpace.Visible = true;
            lblErrorSpace.CssClass = "alert-success custom-absolute-alert";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);

            txtSpaceheader.Text = string.Empty;
            LoadLots(1);

            //Response.Redirect("facility");
            pnlLot.Visible = false;
            pnlSpace.Visible = true;
            pnlLane.Visible = false;


        }

    }



    /// <summary>
    /// Next Button Click For Row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void lbkNextRow_Click(object sender, EventArgs e)
    {
        lblErrorLane.Text = string.Empty;
        lblErrorLot.Text = string.Empty;
        lblErrorSpace.Text = string.Empty;

        LotRows spc = new LotRows();
        spc.SpaceName = txtSpaceheader.Text.Trim();
        spc.IsActive = true;
        spc.DateCreated = DateTime.Now;
        spc.Description = string.Empty;
        int id = LotRows.insertUpdate(spc, Convert.ToInt32(hidLotId.Value));


        LoadRows(1);
        if (id > 0)
        {
            lblErrorSpace.Text = "Row added successfully!";
            lblErrorSpace.Visible = true;
            lblErrorSpace.CssClass = "alert-success custom-absolute-alert";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);

            txtSpaceheader.Text = string.Empty;
            LoadLots(1);
        }

    }


    /// <summary>
    /// Cancel button click for Spaces
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void lbkCancelLane_Click(object sender, EventArgs e)
    {


        pnlLot.Visible = false;
        pnlSpace.Visible = true;
        pnlLane.Visible = false;
        LoadRows(1);
    }



    /// <summary>
    /// DOne Button For Spaces
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbkSaveLane_Click(object sender, EventArgs e)
    {
        lblErrorLane.Text = string.Empty;
        lblErrorLot.Text = string.Empty;
        lblErrorSpace.Text = string.Empty;

        String lanename = txtLaneheader.Text.Trim();
        //int quantity = Convert.ToInt32(txtLaneQuantity.Text.Trim());

        int spaceid = Convert.ToInt32(ViewState["spaceid"]);
        int id = LotSpace.insertLane(lanename, spaceid);
        LoadSpaces(1, spaceid);
        if (id > 0)
        {
            lblErrorLane.Text = "Space added successfully!";
            lblErrorLane.Visible = true;
            lblErrorLane.CssClass = "alert-success custom-absolute-alert";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);


            txtLaneheader.Text = string.Empty;
            //txtLaneQuantity.Text = string.Empty;
            LoadRows(1);
            LoadLots(1);
            //pnlLot.Visible = false;
            //pnlSpace.Visible = true;
            //pnlLane.Visible = false;

            Response.Redirect("facility");
        }
    }
    /// <summary>
    /// Next Button For Spaces
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>



    protected void lbkNextLane_Click(object sender, EventArgs e)
    {
        lblErrorLane.Text = string.Empty;
        lblErrorLot.Text = string.Empty;
        lblErrorSpace.Text = string.Empty;

        String lanename = txtLaneheader.Text.Trim();
        //int quantity = Convert.ToInt32(txtLaneQuantity.Text.Trim());

        int spaceid = Convert.ToInt32(ViewState["spaceid"]);
        int id = LotSpace.insertLane(lanename, spaceid);
        LoadSpaces(1, spaceid);
        if (id > 0)
        {
            lblErrorLane.Text = "Space added successfully!";
            lblErrorLane.Visible = true;
            lblErrorLane.CssClass = "alert-success custom-absolute-alert";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);

            txtLaneheader.Text = string.Empty;
            //txtLaneQuantity.Text = string.Empty;
            LoadRows(1);
            LoadLots(1);
        }
    }





    /// <summary>
    /// Rows Cancel Button 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void lbkCancelSpace_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["fid"] != null)
        {

            int facilityid = Convert.ToInt32(Request.QueryString["fid"]);
            LoadLots(1, facilityid);
            pnlLot.Visible = true;
            pnlLane.Visible = false;
            pnlSpace.Visible = false;
        }
        else if (Request.QueryString["fids"] != null)
        {
            int facilityid = Convert.ToInt32(Request.QueryString["fids"]);
            LoadLots(1, facilityid);
            pnlLot.Visible = true;
            pnlLane.Visible = false;
            pnlSpace.Visible = false;

        }



    }



    /// <summary>
    /// Storage Lot Done Button Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void lbkSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Lots.getAllLotStatusByOrganizationIdAndLotNumber(UserOrganizationId,Conversion.ParseInt(hndfacilityId.Value), txtParkingLot.Text.Trim()) > 0)
            {
                lblErrorLot.Text = "Storage Lot Already Exist!";
                lblErrorLot.Visible = true;
                lblErrorLot.CssClass = "alert-danger custom-absolute-alert";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                return;


            }


            lblErrorLane.Text = string.Empty;
            lblErrorLot.Text = string.Empty;
            lblErrorSpace.Text = string.Empty;

            int id = 0;
            Lots lot = new Lots();
            lot.LotNumber = txtParkingLot.Text.Trim(); //Guid.NewGuid().ToString().Substring(0, 6);
            lot.Quantity = 0;
            lot.OrganizationId = UserOrganizationId;// Convert.ToInt32(ddlStakeholder.SelectedValue);
            lot.DateCreated = DateTime.Now;
            lot.IsActive = true;
            lot.SpaceId = 0;
            lot.UserID = LoginMemberId;// currentUserInfo.UserId;
            lot.RoleID = UserOrganizationRoleId;// currentUserInfo.RoleId;
            lot.IsCompleted = true;


            BarCode br = new BarCode();
            br.DateCreated = DateTime.Now.ToString();
            br.OrganizationID = UserOrganizationId;
            br.BarCodeNumber = GenerateLotSerialNumber();
            //Guid g = Guid.NewGuid();
            string str = br.BarCodeNumber.ToString().Replace("-", "");
           // if (GenerateLotBarCodeImage(str))
            if (br.GenerateLotBarCodeImage(str))
            {
                hdnLotBarCodeImageFileName.Value = str + ".gif";
                //imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", str);
                //imgLotBarcode.Visible = true;
            }
            if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value))))
            {
                br.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value)));

            }
            lot.BarCodeId = BarCode.Insert(br);
            lot.SubLot = false;
            lot.Permanent = true;
            string serialNumber;
            if (Request.QueryString["fid"] != null)
            {

                int facilityid = Convert.ToInt32(Request.QueryString["fid"]);



                serialNumber = Lots.insertLot(lot, out id, str, facilityid);
                LoadLots(1, facilityid);
                hidLotId.Value = id.ToString();
                dvLotRecord.Visible = true;
                if (id > 0)
                {
                    lblErrorLot.Text = "Storage Lot added successfully!";
                    txtParkingLot.Text = string.Empty;
                    lblErrorLot.CssClass = "alert-success custom-absolute-alert";
                    lblErrorLot.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                    Response.Redirect("facility");

                }


            }
            else if (Request.QueryString["fids"] != null)
            {

                int facilityid = Convert.ToInt32(Request.QueryString["fids"]);



                serialNumber = Lots.insertLot(lot, out id, str, facilityid);
                LoadLots(1, facilityid);
                hidLotId.Value = id.ToString();
                dvLotRecord.Visible = true;
                if (id > 0)
                {
                    lblErrorLot.Text = "Storage Parking Lot added successfully!";
                    lblErrorLot.CssClass = "alert-success custom-absolute-alert";
                    lblErrorLot.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                    txtParkingLot.Text = string.Empty;
                    Response.Redirect("lots");

                }


            }
            else
            {
                serialNumber = Lots.insertLot(lot, out id, str, 0);
                hidLotId.Value = id.ToString();
                dvLotRecord.Visible = true;
                if (id > 0)
                {
                    lblErrorLot.Text = "Storage Parking Lot added successfully!";
                    lblErrorLot.CssClass = "alert-success custom-absolute-alert";
                    ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                    lblErrorLot.Visible = true;
                    txtParkingLot.Text = string.Empty;
                    Response.Redirect("lots");
                }
                LoadLots(1);
            }
            // Response.Redirect("lots");
            //dvSpaceRecord.Visible = true;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "addInventory.lnkLotSave_Click", ex);
        }

    }




    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        dvpopupfacilityinfo.Visible = false;
      
        LotRowControl.Visible = false;
        ucFacilitySpaces.Visible = false;
        ucSpaceTires.Visible = false;
        //hidLotId.Value = string.Empty;

    }

    /// <summary>
    /// Storage Lot InfO Next button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void lbkNextLot_Click(object sender, EventArgs e)
    {
        try
        {
            
            if( Lots.getAllLotStatusByOrganizationIdAndLotNumber(UserOrganizationId,Conversion.ParseInt(hndfacilityId.Value),txtParkingLot.Text.Trim())>0)
            {
                lblErrorLot.Text = "Storage Lot Already Exist!";
                lblErrorLot.Visible = true;
                lblErrorLot.CssClass = "alert-danger custom-absolute-alert";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                return;


            }



            lblErrorLane.Text = string.Empty;
            lblErrorLot.Text = string.Empty;
            lblErrorSpace.Text = string.Empty;



            int id = 0;
            Lots lot = new Lots();
            lot.LotNumber = txtParkingLot.Text.Trim(); //Guid.NewGuid().ToString().Substring(0, 6);
            lot.Quantity = 0;
            lot.OrganizationId = UserOrganizationId;// Convert.ToInt32(ddlStakeholder.SelectedValue);
            lot.DateCreated = DateTime.Now;
            lot.IsActive = true;
            lot.SpaceId = 0;
            lot.UserID = LoginMemberId;// currentUserInfo.UserId;
            lot.RoleID = UserOrganizationRoleId;// currentUserInfo.RoleId;
            lot.IsCompleted = true;


            BarCode br = new BarCode();
            br.DateCreated = DateTime.Now.ToString();
            br.OrganizationID = UserOrganizationId;
            br.BarCodeNumber = GenerateLotSerialNumber();
           // Guid g = Guid.NewGuid();
            string str = br.BarCodeNumber.ToString().Replace("-", "");
            if (br.GenerateLotBarCodeImage(str))
            {
                hdnLotBarCodeImageFileName.Value = str + ".gif";
                //imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", str);
                //imgLotBarcode.Visible = true;
            }
            if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value))))
            {
                br.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value)));

            }
            lot.BarCodeId = BarCode.Insert(br);
            lot.SubLot = false;
            lot.Permanent = true;
            lot.ProductCategoryId = CatId;
            string serialNumber;
            if (Request.QueryString["fid"] != null)
            {

                int facilityid = Convert.ToInt32(Request.QueryString["fid"]);



                serialNumber = Lots.insertLot(lot, out id, str, facilityid);
                LoadLots(1, facilityid);
                hidLotId.Value = id.ToString();
                dvLotRecord.Visible = true;
                if (id > 0)
                {
                    lblErrorLot.Text = "Storage Lot added successfully!";
                    lblErrorLot.Visible = true;
                    lblErrorLot.CssClass = "alert-success custom-absolute-alert";
                    ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                    txtParkingLot.Text = string.Empty;


                }


            }
            else if (Request.QueryString["fids"] != null)
            {

                int facilityid = Convert.ToInt32(Request.QueryString["fids"]);



                serialNumber = Lots.insertLot(lot, out id, str, facilityid);
                LoadLots(1, facilityid);
                hidLotId.Value = id.ToString();
                dvLotRecord.Visible = true;
                if (id > 0)
                {
                    lblErrorLot.Text = "Storage Lot added successfully!";
                    lblErrorLot.CssClass = "alert-success custom-absolute-alert";
                    lblErrorLot.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                    txtParkingLot.Text = string.Empty;


                }


            }
            else
            {
                serialNumber = Lots.insertLot(lot, out id, str, 0);
                hidLotId.Value = id.ToString();
                dvLotRecord.Visible = true;
                if (id > 0)
                {
                    lblErrorLot.Text = "Storage Lot added successfully!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                    
                    lblErrorLot.CssClass = "alert-success custom-absolute-alert";
                    lblErrorLot.Visible = true;
                    txtParkingLot.Text = string.Empty;

                }
                LoadLots(1);
            }
            // Response.Redirect("lots");
            //dvSpaceRecord.Visible = true;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "addInventory.lnkLotSave_Click", ex);
        }
    }



    /// <summary>
    /// Generate Serial Number
    /// </summary>
    /// <returns></returns>

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



    protected void gvLane_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            TextBox txtLaneFooter = gvLane.FooterRow.FindControl("txtLanefooter") as TextBox;

            //HiddenField hiddenfieldspaceid = this.FindControl("hdnspaceid") as HiddenField;



            //int index = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = gvSetting.Rows[index];
            //HiddenField hiddenfieldspaceid = (HiddenField)row.FindControl("hdnspaceid");


            string Lanename = txtLaneFooter.Text.Trim();
            //int quantity = Convert.ToInt32(txtLaneQuantity.Text.Trim());

            int spaceid = Convert.ToInt32(ViewState["spaceid"]);

            LotSpace.insertLane(Lanename, spaceid);

            LoadSpaces(1,spaceid);

        }
        else if (e.CommandName == "AddMore")
        {
            LinkButton lnkbtnAddMoreSetting = gvLane.FooterRow.FindControl("lnkbtnAddMore") as LinkButton;
            LinkButton lnkbtnAddSetting = gvLane.FooterRow.FindControl("lnkbtnAddSetting") as LinkButton;
            LinkButton lnkbtnCancelSetting = gvLane.FooterRow.FindControl("lnkbtnCancelSetting") as LinkButton;


            lnkbtnAddSetting.Visible = true;
            lnkbtnAddMoreSetting.Visible = false;
            lnkbtnCancelSetting.Visible = true;

            TextBox txtLaneFooter = gvLane.FooterRow.FindControl("txtLanefooter") as TextBox;
            //TextBox txtSpace = gvSetting.FooterRow.FindControl("txtSpaces") as TextBox;
            txtLaneFooter.Visible = true;
        }


        else if (e.CommandName == "CancelLane")
        {
            gvLane.EditIndex = -1;
            //loadGridAndHeaderText();
            int spaceid = Convert.ToInt32(ViewState["spaceid"]);
            LoadSpaces(1, spaceid);
        }

        else if (e.CommandName == "Edit")
        {
            LaneId = Convert.ToInt32(e.CommandArgument);
            lblErrorLot.Text = string.Empty;
        }

        else if (e.CommandName == "Delete")
        {
            int laneId = Convert.ToInt32(e.CommandArgument);
            if (!lanehaveTire(laneId))
            {
                LotSpace.deleteLane(laneId);
                int spaceid = Convert.ToInt32(ViewState["spaceid"]);
                LoadSpaces(1, spaceid);
                lblErrorLane.Text = string.Empty;
                lblErrorLot.Text = string.Empty;
                lblErrorSpace.Text = string.Empty;
            }

            
        }
        else if (e.CommandName == "LaneInfoPopUp")
        {
           
            ucSpaceTires.Visible = true;
            dvpopupfacilityinfo.Visible = true;
        GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        int RemoveAt = gvr.RowIndex;
        Lots objlots = new Lots(Conversion.ParseInt(hidLotId.Value));
        LotSpace objLotSpaces = new LotSpace(Conversion.ParseInt(e.CommandArgument));
        HiddenField lbl = (HiddenField)gvLane.Rows[RemoveAt].FindControl("hdnspacename");
       
        ucSpaceTires.LoadTires(Conversion.ParseInt(e.CommandArgument));
        ucSpaceTires.LoadFacilityLotRowSpaceName(objlots.FacilityName, hidLotId.Value, objlots.LotNumber,objLotSpaces.SpaceId.ToString(),objLotSpaces.SpaceName, lbl.Value);
        }


    }



    protected void gvLane_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void gvLane_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvLane_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }

    protected void gvLane_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvLane.EditIndex = e.NewEditIndex;
        int spaceid = Convert.ToInt32(ViewState["spaceid"]);
        LoadSpaces(1, spaceid);

    }

    protected void gvLane_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //   TextBox txtLaneFooter = gvLane.FooterRow.FindControl("txtLanefooter") as TextBox;
        TextBox txtLane = (TextBox)gvLane.Rows[e.RowIndex].FindControl("txtLanes");
        //TextBox txtquantity = (TextBox)gvLane.Rows[e.RowIndex].FindControl("txtLaneQuantity");
        lblErrorLane.Text = "Space Name Updated successfully!";
        lblErrorLane.Visible = true;
        lblErrorLane.CssClass = "alert-success custom-absolute-alert";
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);



        HiddenField lanehiddenfeild = (HiddenField)gvLane.Rows[e.RowIndex].FindControl("hdnspaceid");


        String lanename = txtLane.Text.Trim();
        int spaceid = Convert.ToInt32(ViewState["spaceid"]);
        LaneId = Convert.ToInt32(lanehiddenfeild.Value);
        //int quantity = Convert.ToInt32(txtquantity.Text.Trim());
        //Space.UpdateLaneInfo(LaneId, spaceid, lanename,quantity);
        LotSpace.updateLaneInfo(LaneId, spaceid, lanename);

        gvLane.EditIndex = -1;
        LoadSpaces(1, spaceid);

    }

    protected void gvLane_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvLane.EditIndex = -1;
        int spaceid = Convert.ToInt32(ViewState["spaceid"]);
        LoadSpaces(1, spaceid);

    }

    protected void gvLane_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// End GridView Space Info Funcations
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>







    /////star of Stroage lots Info Grids Funcations////////////////


    protected void gvLot_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "AddMore")
        {

            LinkButton lnkbtnAddMoreSetting = gvLot.FooterRow.FindControl("lnkbtnAddMore") as LinkButton;
            LinkButton lnkbtnAddSetting = gvLot.FooterRow.FindControl("lnkbtnAddSetting") as LinkButton;
            LinkButton lnkbtnCancelSetting = gvLot.FooterRow.FindControl("lnkbtnCancelSetting") as LinkButton;


            lnkbtnAddSetting.Visible = true;
            lnkbtnAddMoreSetting.Visible = false;
            lnkbtnCancelSetting.Visible = true;

            TextBox txtLotFooter = gvLot.FooterRow.FindControl("txtLotfooter") as TextBox;
            //TextBox txtSpace = gvSetting.FooterRow.FindControl("txtSpaces") as TextBox;
            txtLotFooter.Visible = true;


        }

        if (e.CommandName == "AddLotSpace")
        {
            lblErrorLane.Text = String.Empty;
            lblErrorLot.Text = String.Empty;
            lblErrorSpace.Text = String.Empty;


            pnlLot.Visible = false;
            pnlSpace.Visible = true;
            hidLotId.Value = Convert.ToString(e.CommandArgument);

            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            Label lbl = (Label)gvLot.Rows[RemoveAt].FindControl("lbllotnumber");
            lblLotNumberSpace.Text = lbl.Text;
            lblFacilityForSpace.Text = lblfacilityname.Text;
            LoadRows(1);
        }


        if (e.CommandName == "Insert")
        {
            try
            {
                TextBox txtParkingLot = (TextBox)((GridView)sender).FooterRow.FindControl("txtLotfooter");

                int id = 0;
                Lots lot = new Lots();
                lot.LotNumber = txtParkingLot.Text.Trim(); //Guid.NewGuid().ToString().Substring(0, 6);
                lot.Quantity = 0;
                lot.OrganizationId = UserOrganizationId;// Convert.ToInt32(ddlStakeholder.SelectedValue);
                lot.DateCreated = DateTime.Now;
                lot.IsActive = true;
                lot.SpaceId = 0;
                lot.UserID = LoginMemberId;// currentUserInfo.UserId;
                lot.RoleID = UserOrganizationRoleId;// currentUserInfo.RoleId;
                lot.IsCompleted = true;


                BarCode br = new BarCode();
                br.DateCreated = DateTime.Now.ToString();
                br.OrganizationID = UserOrganizationId;
                br.BarCodeNumber = GenerateLotSerialNumber();
                Guid g = Guid.NewGuid();
                string str = g.ToString().Replace("-", "");
                if (br.GenerateLotBarCodeImage(str))
                {
                    hdnLotBarCodeImageFileName.Value = str + ".gif";
                    //imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", str);
                    //imgLotBarcode.Visible = true;
                }
                if (System.IO.File.Exists(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value))))
                {
                    br.Image = System.IO.File.ReadAllBytes(Server.MapPath(String.Format("/images/temp/{0}", hdnLotBarCodeImageFileName.Value)));

                }
                lot.BarCodeId = BarCode.Insert(br);

                lot.SubLot = false;
                lot.Permanent = true;
                string serialNumber = Lots.insertLot(lot, out id, str);
                hidLotId.Value = id.ToString();
                //lbkSaveLot.Visible = false;
                dvLotRecord.Visible = true;
                LoadLots(1);
                //dvSpaceRecord.Visible = true;
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "addInventory.lnkLotSave_Click", ex);
            }

        }

        else if (e.CommandName == "Edit")
        {

            hidLotId.Value = Convert.ToString(e.CommandArgument);
            lblErrorLot.Text = string.Empty;
        }

        else if (e.CommandName == "Delete")
        {
            int lotId = Convert.ToInt32(e.CommandArgument);
            if (!lothaveTire(lotId))
            {
                Lots.deleteLot(lotId);
                lblErrorLot.Text = string.Empty;
                LoadLots(1);
                lblErrorLane.Text = string.Empty;
                lblErrorLot.Text = string.Empty;
                lblErrorSpace.Text = string.Empty;
            }
        }
        else if (e.CommandName == "RowInfoPopUp")
        {
            LotRowControl.Visible = true;
            
            lblErrorLot.Text = String.Empty;
            //lblErrorSpace.Text = String.Empty;

            dvpopupfacilityinfo.Visible = true;
            //pnlLot.Visible = false;
            
            
            hidLotId.Value = Convert.ToString(e.CommandArgument);
            Lots objLots = new Lots(Conversion.ParseInt(hidLotId.Value));
            
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            HiddenField lbl = (HiddenField)gvLot.Rows[RemoveAt].FindControl("hdGVLotNumber");
            hdnlotname.Value = lbl.Value;
            LotRowControl.loadFacilityandLotName(objLots.FacilityName, lbl.Value, hndfacilityId.Value);
            LotRowControl.loadLotRows(Convert.ToInt32(e.CommandArgument));
        }

        else if (e.CommandName == "Cancel")
        {
            gvLot.EditIndex = -1;
            LoadLots(1);

        }

    }

    protected void gvLot_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvLot.EditIndex = -1;
        LoadLots(1);
    }

    protected void gvLot_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void gvLot_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvLot_RowEditing(object sender, GridViewEditEventArgs e)
    {


        gvLot.EditIndex = e.NewEditIndex;
        int LotId = Convert.ToInt32(hidLotId.Value);
        LoadLots(1);

    }

    protected void gvLot_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }

    protected void gvLot_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


        TextBox txtLot = (TextBox)gvLot.Rows[e.RowIndex].FindControl("txtLotNumber");

        if (Lots.getAllLotStatusByOrganizationIdAndLotNumber(UserOrganizationId, Conversion.ParseInt(hndfacilityId.Value), txtLot.Text.Trim()) > 0)
        {
            lblErrorLot.Text = "Storage Lot Already Exist!";
            lblErrorLot.Visible = true;
            lblErrorLot.CssClass = "alert-danger custom-absolute-alert";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            return;


        }
        

        HiddenField lanehdnlot = (HiddenField)gvLot.Rows[e.RowIndex].FindControl("hdnlotid1");
        int lotid = Convert.ToInt32(lanehdnlot.Value);
        string lotnumber = txtLot.Text.Trim();
        Lots.updateLotByLotId(lotid, lotnumber);
        lblErrorLot.Text = "Storage Lot Updated successfully!";
        lblErrorLot.Visible = true;
        lblErrorLot.CssClass = "alert-success custom-absolute-alert";
        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        gvLot.EditIndex = -1;
        LoadLots(1);
    }

    protected void txtSpaceheader_TextChanged(object sender, EventArgs e)
    {
        lblErrorSpace.Text = string.Empty;
    }

    protected void txtLaneheader_TextChanged(object sender, EventArgs e)
    {
        lblErrorLane.Text = string.Empty;
    }

    protected void txtParkingLot_TextChanged(object sender, EventArgs e)
    {
        lblErrorLot.Text = string.Empty;
    }


    /////////End Storage Lot Grid View Funcations//////////////



    protected bool lothaveTire(int lotId)
    {

        int tirecount = Lots.gettirecount(lotId);

        if (tirecount > 0)
        {
            lbldeleteError.Text = "Tire must be transferred to a new or existing Storage Location before this Storage Lot can be Deleted";
            lbldeleteError.CssClass = "alert-danger custom-absolute-alert";
            lbldeleteError.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOutLong();", true);

            return true;
        }

        else
        {
            return false;
        }
    }

    private bool rowhaveTire(int spaceId)
    {
        int tirecount = Lots.gettirecountrow(spaceId);

        if (tirecount > 0)
        {
            lbldeleteError.Text = "Tire must be transferred to a new or existing Storage Location before this Row can be Deleted";
            lbldeleteError.CssClass = "alert-danger custom-absolute-alert";
            lbldeleteError.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOutLong();", true);

            return true;
        }

        else
        {
            return false;
        }
    }

    protected bool spacehaveTire(int lotId)
    {

        int tirecount = Lots.gettirecount(lotId);

        if (tirecount > 0)
        {
            lbldeleteError.Text = "Tire must be transferred to a new or existing Storage Location before this Storage Lot can be Deleted";
            lbldeleteError.CssClass = "alert-danger custom-absolute-alert";
            lbldeleteError.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOutLong();", true);

            return true;
        }

        else
        {
            return false;
        }
    }

    private bool lanehaveTire(int laneId)
    {
        int tirecount = Lots.gettirecountspace(laneId);

        if (tirecount > 0)
        {
            lbldeleteError.Text = "Tire must be transferred to a new or existing Storage Location before this Space can be Deleted";
            lbldeleteError.CssClass = "alert-danger custom-absolute-alert";
            lbldeleteError.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOutLong();", true);

            return true;
        }

        else
        {
            return false;
        }
    }
}