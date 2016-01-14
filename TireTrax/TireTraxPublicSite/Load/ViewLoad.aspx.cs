using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class Load_ViewLoad : BasePage
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



    protected void Page_Load(object sender, EventArgs e)
    {


        GetPermission(ResourceType.LoadsInventory, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        else if (!canAdd)
        {
            dvAdd.Visible = false;
        }

        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liInventory','{0}');", ResourceMgr.GetMessage("Inventory")), true);
        ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        pageSize = 15;


        if (!IsPostBack)
        {
            Utils.GetLookUpData<DropDownList>(ref ddlLoadStatus, LookUps.LoadType);
            if (ddlLoadStatus.SelectedValue == "0")
            {
                ddlLoadStatus.Items.RemoveAt(0);
                ddlLoadStatus.Items.Add(new ListItem("All Loads", "0"));
                //ddlLoadStatus.Items.Remove(new ListItem("--Select--", "0"));
            }

            ddlLoadStatus.SelectedValue = "0";
            //Load_AllAdminInventory(1);
            //Utils.GetLookUpData<DropDownList>(ref ddlChangeLoadStatus, LookUps.LoadType);

            //    ddlChangeLoadStatus.Items.Remove(ddlChangeLoadStatus.Items.FindByValue("0"));
            LoadsInfo(1);
        }
        if (TotalItemsR > 0)
        {
            pgrLoad.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
        }

        //if (TotalItems > 0)
        //{
        //    pgrLots.DrawPager(CurrentPage, TotalItems, pageSize, MaxPagesToShow);

        //}

    }

    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pgrLoad.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.LoadsInfo(CurrentPage);
        }
        else
        {
            this.LoadsInfo(Conversion.ParseInt(hdnCurrentPage.Value));
        }


        return base.OnBubbleEvent(source, args);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        LoadsInfo(1);

    }

    protected void btnAddLoad_Click(object sender, EventArgs e)
    {
        Response.Redirect("addload");
    }


    protected void LoadsInfo(int pageNo)
    {
        try
        {
            pageSize = 15;// Convert.ToInt32(ddlloadsinfo.SelectedValue);
            gvloadsinfo.PageSize = pageSize;
            CurrentPageR = pageNo;
            DateTime frmDate = string.IsNullOrEmpty(txtFrmDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFrmDate.Text);
            DateTime toDate = string.IsNullOrEmpty(txtToDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text);
            //int quantity = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? 0 : Convert.ToInt32(txtQuantity.Text.Trim());
            int count = 0;
            gvloadsinfo.DataSource = Loads.loadsInfo(pageNo, pageSize, out count, txtUserName.Text.Trim(),
                      frmDate, toDate, UserOrganizationId, Conversion.ParseInt(ddlLoadStatus.SelectedValue), txtCompanyName.Text.Trim(), CatId);
            gvloadsinfo.DataBind();
            hdnCurrentPage.Value = pageNo.ToString();
            this.TotalItemsR = count;
            this.pgrLoad.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);
            if (gvloadsinfo.Rows.Count != 0)
            {
                LinkButton lnkBtn = (LinkButton)pgrLoad.FindControl("Button_" + pageNo.ToString());
                lnkBtn.Font.Bold = true;
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewLoads.LoadsInfo", ex);
        }

    }


    protected void gvloadsinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "LoadTireInfo")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RemoveAt = gvr.RowIndex;
            HiddenField lnkbtn = (HiddenField)gvloadsinfo.Rows[RemoveAt].FindControl("hdLoadNumber");



            if (CatId == (int)ProductCategory.Tire)
            {
                dvMainLoad.Visible = true;
                lblLoadNumber.Text = lnkbtn.Value;
                LoadPopInfobyLoadId(Convert.ToInt32(e.CommandArgument));
            }
            else
            {
                divProductLoad.Visible = true;
                lblLoadName.Text = lnkbtn.Value;
                LoadPopInfobyLoadIdForProduct(Convert.ToInt32(e.CommandArgument));
            }

        }
        else if (e.CommandName == "shipdetail")
        {
            Response.Redirect("addload?loadId=" + e.CommandArgument.ToString() + "&ship=1");
        }


        else if (e.CommandName == "Accept")
        {
            try
            {
                ImageButton imgbtnAccept = e.CommandSource as ImageButton;
                //imgbtnAccept.Enabled = false;
                int parentOrgId = 0;
                int parentOrgSutTypeID = 0;
                DataSet ds = Loads.getDeliveryLoadByLoadId(Conversion.ParseInt(e.CommandArgument.ToString()));
                int deliveryId = 0;
                decimal amount = 0;

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataSet dsAcceptanceCriteria = Loads.GetDeliveryAcceptanceByLoadId(Conversion.ParseInt(e.CommandArgument.ToString()));
                        if (dsAcceptanceCriteria != null && dsAcceptanceCriteria.Tables.Count > 0)
                        {
                            if (dsAcceptanceCriteria.Tables[0].Rows.Count > 0)
                            {
                                bool isAcceptOrRejected = (Convert.ToBoolean(dsAcceptanceCriteria.Tables[0].Rows[0][0]) ^ Convert.ToBoolean(dsAcceptanceCriteria.Tables[0].Rows[0][1]));
                                if (!isAcceptOrRejected)
                                {
                                    dvAcceptLoad.Visible = true;
                                    lblAcceptLoad.Text = "Please Accept the Delivery Note " + dsAcceptanceCriteria.Tables[0].Rows[0]["DeliveryName"].ToString()+" on your delivery receipt page.";
                                    return;
                                }
                            }
                        }
                        DataSet dsAmount = null;
                        deliveryId = Conversion.ParseInt(ds.Tables[0].Rows[0]["deliveryid"]);
                        if (CatId == (int)ProductCategory.Tire)
                        {
                            dsAmount = Invoice.getDeliveryInvoicesAggreatedAmount(Conversion.ParseString(deliveryId));
                        }
                        else
                        {
                            dsAmount = Invoice.GetInvoiceAmountForProduct(deliveryId);
                        }


                        if (dsAmount != null && ds.Tables.Count > 0)
                        {
                            if (dsAmount.Tables[0].Rows.Count > 0)
                            {
                                amount = Conversion.ParseDecimal(dsAmount.Tables[0].Rows[0]["AggrAmount"]);
                                parentOrgId = Conversion.ParseInt(dsAmount.Tables[0].Rows[0]["organizationId"]);
                                parentOrgSutTypeID = Conversion.ParseInt(dsAmount.Tables[0].Rows[0]["OrganizationSubTypeID"]);
                            }
                        }

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            int loadid = Conversion.ParseInt(row["loadid"]);
                            Loads.acceptLoadByLoadId(loadid);
                            try
                            {
                                SendNotification(loadid);
                            }
                            catch (Exception ex)
                            {
                                new SqlLog().InsertSqlLog(LoginMemberId, "View load AcceptButton", ex);
                            }

                        }
                        createInvoice(deliveryId, amount, parentOrgId, parentOrgSutTypeID);
                    }
                    else
                    {
                        dvAcceptLoad.Visible = true;
                        lblAcceptLoad.Text = "There are no delivery notes attached to this load. Please consult the sender organization";
                    }
                }
                else
                {
                    dvAcceptLoad.Visible = true;
                    lblAcceptLoad.Text = "There are no delivery notes attached to this load. Please consult the sender organization";
                }
                LoadsInfo(1);
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "ViewLoad.aspx.cs. AcceptLoad", ex);
            }
        }
        else if (e.CommandName == "Reject")
        {
            hdnLoadId.Value = e.CommandArgument.ToString();
            dvRejectLoadNotes.Visible = true;

        }


        else if (e.CommandName == "Edit")
        {
            Response.Redirect("editload?LoadId=" + e.CommandArgument);
        }
        else if (e.CommandName == "transfer")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            HiddenField hid = (HiddenField)row.FindControl("hidLoadSelectId");
            Session["SelectedLoadId"] = "";
            Session["SpaceId"] = "";
            Session["SelectedLoadId"] = hid.Value.ToString();
            hidSelectedOrgId.Value = UserOrganizationId.ToString();
            LoadPermanentGrid();
        }

    }

    protected void createInvoice(int deliveryId, decimal amount, int ParentOrgId, int parentOrgSutTypeID)
    {
        #region Create Invoice for Stewardship
        int stewardshipID = OrganizationInfo.GetStewardshipByStakeholderId(ParentOrgId);
        if (stewardshipID == 0)
        {
            stewardshipID = ParentOrgId;
        }

        Random r = new Random();
        int rNumber = r.Next(0, 10000);

        Invoice objInvoice = new Invoice();
        objInvoice.InvoiceID = 0;
        objInvoice.Active = true;
        objInvoice.InvoiceDate = DateTime.Now;
        objInvoice.InvoiceAmount = amount;
        objInvoice.DueDate = DateTime.Now.AddDays(1).Date;
        objInvoice.Organizationid = ParentOrgId;
        objInvoice.Status = "Pending";
        objInvoice.CreatedBy = LoginMemberId;
        objInvoice.DateCreated = DateTime.Now;
        objInvoice.ModifiedBy = LoginMemberId;
        objInvoice.DateModified = DateTime.Now;
        objInvoice.IsPaid = false;
        objInvoice.DeliveryIDs = Conversion.ParseString(deliveryId);
        objInvoice.OrganizationForId = Utils.GetAdminOrganization(); //send the id of EPR Admin static ID
        objInvoice.IsSent = false;
        objInvoice.ProductCategoryId = CatId;
        objInvoice.InvoiceNumber = new OrganizationInfo(UserOrganizationId).LegalName.Substring(0, 3).ToUpper() + System.DateTime.Now.ToString("ddMMyyyy") + rNumber.ToString();
        Invoice.Invoice_InsertUpdate(objInvoice);

        ///////////////////////////////////////////  Stewardship's Invoice ///////////////////////////////////////////////////
        DataSet ds = Commission.GetCommissionOfStewardship(stewardshipID, parentOrgSutTypeID);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow row = ds.Tables[0].Rows[0];
            int comissionType = Conversion.ParseInt(row["tntCommssionType"]);
            if (comissionType == 1)
            {
                decimal ComissionAmount = Conversion.ParseDecimal(row["Amount"]);
                amount = ComissionAmount;
            }
            else if (comissionType == 2)
            {
                decimal ComissionPercentage = Conversion.ParseDecimal(row["Percentage"]);
                amount = amount * (ComissionPercentage / 100);
            }


        }
        /////////////////Stewardship invoice
        Random r2 = new Random();
        int rStewardShip = r2.Next(0, 10000);

        objInvoice = new Invoice();
        objInvoice.InvoiceID = 0;
        objInvoice.Active = true;
        objInvoice.InvoiceDate = DateTime.Now;
        objInvoice.InvoiceAmount = amount;
        objInvoice.DueDate = DateTime.Now.AddDays(1).Date;
        objInvoice.Organizationid = stewardshipID;
        objInvoice.Status = "Pending";
        objInvoice.CreatedBy = LoginMemberId;
        objInvoice.DateCreated = DateTime.Now;
        objInvoice.ModifiedBy = LoginMemberId;
        objInvoice.DateModified = DateTime.Now;
        objInvoice.IsPaid = false;
        objInvoice.DeliveryIDs = Conversion.ParseString(deliveryId);
        objInvoice.OrganizationForId = Utils.GetAdminOrganization();          //send the id of EPR Admin static ID
        objInvoice.IsSent = false;
        objInvoice.ProductCategoryId = CatId;
        objInvoice.InvoiceNumber = new OrganizationInfo(stewardshipID).LegalName.Substring(0, 3).ToUpper() + System.DateTime.Now.ToString("ddMMyyyy") + rStewardShip.ToString();


        Invoice.Invoice_InsertUpdate(objInvoice);
        #endregion
    }

    protected void gvloadsinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //LinkButton lnkDetail = (LinkButton)e.Row.FindControl("lnkDetail");
            LinkButton imgbtnAccept = (LinkButton)e.Row.FindControl("imgbtnApprove");
            LinkButton imgbtnReject = (LinkButton)e.Row.FindControl("imgbtnDisApprove");
            LinkButton imgbtnTransfer = (LinkButton)e.Row.FindControl("imgbtnTransfer");
            LinkButton imgbtnEdit = (LinkButton)e.Row.FindControl("imgbtnEditLoad");
            Label imgbtnApproved = (Label)e.Row.FindControl("imgbtnApproved");
            Label imgbtnRejected = (Label)e.Row.FindControl("imgbtnRejected");
            Label imgbtnPending = (Label)e.Row.FindControl("imgbtnPending");
            Label imgbtnTransfered = (Label)e.Row.FindControl("imgbtnTransfered");

            if ((Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitTransfered")) == true))
            {

                imgbtnTransfered.Visible = true;
                imgbtnRejected.Visible = false;
                imgbtnPending.Visible = false;
                imgbtnApproved.Visible = false;
                imgbtnEdit.Visible = false;
                imgbtnTransfer.Visible = false;


            }
            else if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitIsShipped")) == true)
            {
                imgbtnTransfered.Visible = false;
                imgbtnRejected.Visible = false;
                imgbtnPending.Visible = false;
                imgbtnApproved.Visible = false;
                imgbtnEdit.Visible = false;
                imgbtnTransfer.Visible = false;




            }
            else
            {
                imgbtnTransfered.Visible = false;




            }
            if ((Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitIsAccepted")) == true) && (DataBinder.Eval(e.Row.DataItem, "LookupTypeName").ToString().Trim() == "Recieve") && Conversion.ParseInt(DataBinder.Eval(e.Row.DataItem, "HaulerIDNumber")) != UserOrganizationId)
            {
                imgbtnTransfer.Visible = false;

                imgbtnEdit.Visible = false;


            }


            if ((DataBinder.Eval(e.Row.DataItem, "LookupTypeName").ToString().Trim() == "Ship") && (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitIsShipped")) == false))
            {

                if (Conversion.ParseInt(DataBinder.Eval(e.Row.DataItem, "HaulerIDNumber")) == UserOrganizationId && (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitReject")) == false) && (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitIsAccepted")) == false))
                {

                    imgbtnReject.Visible = true;
                    imgbtnAccept.Visible = true;
                    imgbtnTransfered.Visible = false;
                    if ((Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsShipToAccepted")) == false) && (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsShipToRejected")) == false))
                    {
                        imgbtnReject.Enabled = false;
                        //imgbtnAccept.Enabled = false;
                        imgbtnAccept.ToolTip = "Please accept the delivery (" + DataBinder.Eval(e.Row.DataItem, "DeliveryName").ToString().Trim() + ") then you will be able to accept the Load.";
                        imgbtnReject.ToolTip = "Please accept the delivery (" + DataBinder.Eval(e.Row.DataItem, "DeliveryName").ToString().Trim() + ") then you will be able to accept or reject the Load.";
                    }





                }
                else if ((Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitReject")) == true) || (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitReject")) == false) && (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitIsAccepted")) == false))
                {
                    imgbtnReject.Visible = false;
                    imgbtnAccept.Visible = false;
                    imgbtnTransfered.Visible = false;
                    //Literal type = (Literal)e.Row.FindControl("litType");
                    //type.Text = "Rejected";
                }
                else
                {

                    imgbtnReject.Visible = true;
                    imgbtnAccept.Visible = true;
                    imgbtnTransfered.Visible = false;
                    if ((Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsShipToAccepted")) == false) && (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsShipToRejected")) == false))
                    {
                        imgbtnReject.Enabled = false;
                        imgbtnAccept.Enabled = false;
                        imgbtnAccept.ToolTip = "Please accept the delivery (" + DataBinder.Eval(e.Row.DataItem, "DeliveryName").ToString().Trim() + ") then you will be able to accept the Load.";
                        imgbtnReject.ToolTip = "Please accept the delivery (" + DataBinder.Eval(e.Row.DataItem, "DeliveryName").ToString().Trim() + ") then you will be able to accept or reject the Load.";
                    }

                }

                if (Conversion.ParseInt(DataBinder.Eval(e.Row.DataItem, "HaulerIDNumber")) == UserOrganizationId && (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitReject")) == true))
                {
                    imgbtnEdit.Visible = false;
                    imgbtnTransfer.Visible = false;
                    imgbtnTransfered.Visible = false;

                }
                else if (Conversion.ParseInt(DataBinder.Eval(e.Row.DataItem, "HaulerIDNumber")) == UserOrganizationId && (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitReject")) == false) && (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitIsAccepted")) == false))
                {



                    imgbtnEdit.Visible = false;
                    imgbtnTransfer.Visible = false;
                    imgbtnTransfered.Visible = false;

                }
                else if ((Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitReject")) == false) && (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitIsAccepted")) == false))
                {
                    imgbtnEdit.Visible = false;
                    imgbtnTransfer.Visible = false;
                    imgbtnTransfered.Visible = false;
                }
                else
                {
                    imgbtnEdit.Visible = true;
                    imgbtnTransfer.Visible = true;
                    imgbtnTransfered.Visible = false;
                }

                // else
                //{

                //    imgbtnReject.Visible = true;
                //    imgbtnAccept.Visible = true;


                //}
                // lnkDetail.PostBackUrl = "addload?loadId=59&ship=1";


            }



            else
            {
                //ddlChangeLoadStatus.Enabled = true;
                imgbtnReject.Visible = false;
                imgbtnAccept.Visible = false;
                //imgbtnEdit.Visible = true;
                //imgbtnTransfer.Visible = true;
                //imgbtnTransfered.Visible =false;

                //imgbtnTransfer.Visible = true;
                //imgbtnEdit.Visible = true;
            }



        }
    }

    protected void btnRejectNotesCancel_Click(object sender, EventArgs e)
    {

        dvRejectLoadNotes.Visible = false;
        hdnLoadId.Value = string.Empty;

    }
    protected void btnRejectNotesSave_Click(object sender, EventArgs e)
    {

        Loads.rejectLoadByLoadId(Conversion.ParseInt(hdnLoadId.Value));

        SendNotificationForReject(Conversion.ParseInt(hdnLoadId.Value));
        dvRejectLoadNotes.Visible = false;
        LoadsInfo(1);


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
        objNotifications.IntToOrganizationId = loadObject.OrganizationId;
        objNotifications.IntToUserId = 0;
        objNotifications.VchNotificationText = "Load  " + loadObject.LoadNumber + "  Accepted From  " + loadObject.HaulerOrganization + " that sent from " + loadObject.TransferOrganization + " .";
        objNotifications.InsertUpdate();
    }

    private void SendNotificationForReject(int LoadId)
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
        objNotifications.IntToOrganizationId = loadObject.OrganizationId;
        objNotifications.IntToUserId = 0;
        objNotifications.VchNotificationText = "Load " + loadObject.LoadNumber + "that sent from " + loadObject.TransferOrganization + " to " + loadObject.HaulerOrganization + " Rejected because " + txtNotes.Text.Trim();
        objNotifications.InsertUpdate();
    }

    /// <summary>
    /// Loads Tires Info Start All Pop Info By Load Id on LowCommand Click
    /// </summary>
    /// <param name="loadid"></param>


    public void LoadPopInfobyLoadId(int loadid)
    {
        Loads loadobj = new Loads(loadid);
        int LoadTypeId = loadobj.LoadTypeId;
        lblLoadTypeName.Text = loadobj.LookUpTypeName;
        hdnlotid.Value = loadid.ToString();
        lblLoadTireCount.Text = Convert.ToString(Loads.getCountLoadByLoadId(loadid));
        lblHaulerOrganization.Text = loadobj.HaulerOrganization;
        lblTransferOrganization.Text = loadobj.TransferOrganization;


        DataSet ds = Loads.getLoadTireInfoByLoadId(loadid);
        grvLoadTireInfo.DataSource = ds;
        grvLoadTireInfo.DataBind();


    }
    public void LoadPopInfobyLoadIdForProduct(int loadid)
    {
        Loads loadobj = new Loads(loadid);
        int LoadTypeId = loadobj.LoadTypeId;
        lblLoadType.Text = loadobj.LookUpTypeName;
        hdnlotid.Value = loadid.ToString();
        lblProductCount.Text = Convert.ToString(Loads.getCountLoadByLoadId(loadid));
        lblHauerName.Text = loadobj.HaulerOrganization;
        lblTransferName.Text = loadobj.TransferOrganization;

        DataSet ds = Loads.getProductInfoByLoadId(loadid, CatId);
        gvLoadProductInfo.DataSource = ds;
        gvLoadProductInfo.DataBind();
    }


    protected void ddlLoadStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadsInfo(1);
    }

    protected void btnAddLoadType_Click(object sender, EventArgs e)
    {
        //Loads.updateLoadByLoadTypeId(Conversion.ParseInt(hdnlotid.Value), Conversion.ParseInt(ddlChangeLoadStatus.SelectedValue));
        dvMainLoad.Visible = false;

        LoadsInfo(1);

    }
    protected void btnAddLoadTypeCancel_Click(object sender, EventArgs e)
    {
        dvMainLoad.Visible = false;
        hdnCurrentPage.Value = CurrentPage.ToString();
    }
    /////////// End Load Tire Info//////////////////////////////////////////////











    /////////////////////////Shift Load Tire To Permenent Lot //////////////////////////////////////
    protected void lnkPermanentLot_Click(object sender, EventArgs e)
    {
        string str = hidSelectedLot.Value.Replace(",", "");

        if (string.IsNullOrEmpty(str))
        {
            lblErrorPermanentLotdv.Text = "Please select Facility Storage Lot for transfer";
            lblErrorPermanentLotdv.CssClass = "alert-danger";
            //lblNotify.Visible = true;
            //lblNotify.Text = "Please select target facility for transfer";
            //lblNotify.CssClass = "alert alert-danger";
            return;
        }
        LoadSpaces();
        dvParkingLot1.Visible = false;
        dvSpace.Visible = true;
        dvlane.Visible = false;
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Session["SelectedLoadId"] = "";
        Session["SpaceId"] = "";
        dvPermanentLot.Visible = false;
        dvParkingLot1.Visible = false;

        dvSpace.Visible = false;
        dvlane.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;

        pgrLoad.DrawPager(Conversion.ParseInt(hdnCurrentPage.Value), TotalItemsR, pageSize, MaxPagesToShow);



    }

    protected void lnkBackPermanentLotSpace_Click(object sender, EventArgs e)
    {


        Utils.SetSelectedIdsGridView(ref grvPermanentLot, "", "Radio1", "", true, hidSelectedLot.Value);

        pnlPermanentLot.Visible = true;
        dvParkingLot1.Visible = true;
        grvPermanentLot.Visible = true;


        dvlane.Visible = false;

        dvSpace.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;





    }
    protected void lnkBackPermanentLotLane_Click(object sender, EventArgs e)
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
    protected void lnkSpacePerLot_Click(object sender, EventArgs e)
    {

        string str = hidSelectedSpace.Value.Replace(",", "");
        if (string.IsNullOrEmpty(str))
        {
            lblErrorPermanentLotSpacedv.Text = "Please select a Row";
            lblErrorPermanentLotSpacedv.CssClass = "alert-danger";
            return;
        }

        Session["SpaceId"] = str;
        dvParkingLot1.Visible = false;
        dvSpace.Visible = false;
        dvlane.Visible = true;
        LoadLanes();
    }

    protected void lnkLanePerLot_Click(object sender, EventArgs e)
    {
        string str2 = hidSelectedLane.Value.Replace(",", "");
        if (string.IsNullOrEmpty(str2))
        {
            lblErrorPermanentLotLanedv.Text = "Please select a Space";
            lblErrorPermanentLotLanedv.CssClass = "";
            return;
        }
        string str = hidSelectedSpace.Value.Replace(",", "");


        Loads.transferLoadTiresToPermenentLot(Convert.ToInt32(Session["SelectedLoadId"]), Convert.ToInt32(str), Convert.ToInt32(str2));
        lblErrorPermanentLotdv.Text = string.Empty;
        lblErrorPermanentLotLanedv.Text = string.Empty;
        lblErrorPermanentLotSpacedv.Text = string.Empty;
        dvParkingLot1.Visible = false;
        dvPermanentLot.Visible = false;
        dvSpace.Visible = false;
        dvlane.Visible = false;
        Session["SelectedLoadId"] = "";
        Session["SpaceId"] = "";
        LoadsInfo(1);

    }
    private void LoadSpaces()
    {
        try
        {

            string str = hidSelectedLot.Value.Replace(",", "");
            DataSet ds = Lots.getPermanentLotSpace(Convert.ToInt32(str));
            grdSpaces.DataSource = ds;
            grdSpaces.DataBind();

            if (ds == null && ds.Tables.Count <= 0)
            {

                lnkSpacePerLot.Visible = false;
            }
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                    lnkSpacePerLot.Visible = false;
                else
                    lnkSpacePerLot.Visible = true;
            }
            dvPermanentLot.Visible = true;
            dvPermanentLot.Visible = true;
            dvSpace.Visible = true;
            dvlane.Visible = false;

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
            gvlane.DataSource = ds;
            gvlane.DataBind();
            if (ds == null && ds.Tables.Count <= 0)
            {

                lnkLanePerLot.Visible = false;
            }
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                    lnkLanePerLot.Visible = false;
                else
                    lnkLanePerLot.Visible = true;
            }



            dvPermanentLot.Visible = true;
            dvSpace.Visible = false;
            dvlane.Visible = true;



        }
        catch (Exception e)
        {


        }

    }
    private void LoadPermanentGrid()
    {
        try
        {

            Load_AllPermanentLot(1);
            //DataSet ds = Lots.getPermanentLot(Convert.ToInt32(hidSelectedOrgId.Value));
            //grvPermanentLot.DataSource = ds;
            //grvPermanentLot.DataBind();
            dvPermanentLot.Visible = true;
            grvPermanentLot.Visible = true;
            dvParkingLot1.Visible = true;

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "lotInfo .LoadPermanentGrid", ex);
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



        if (count > 0)
        {
            lblNotify.Visible = true;
            lblNotify.Text = "Please select target facility for transfer";
        }
        else
        {
            lblNotify.Visible = false;
            lblNotify.Text = string.Empty;
        }


        //this.FacilityStorageLOTSPager.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);

        grvPermanentLot.DataSource = ds;
        grvPermanentLot.DataBind();
    }
    protected void grvLoadTireInfo_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void btnBackProductPopup_Click(object sender, EventArgs e)
    {
        divProductLoad.Visible = false;
        hdnCurrentPage.Value = CurrentPage.ToString();
    }

    protected void btnBackAcceptLoad_Click(object sender, EventArgs e)
    {
        dvAcceptLoad.Visible = false;
    }
}
