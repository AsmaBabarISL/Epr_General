using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;

public partial class DeliveryReceipt_ViewDeliveryReceipt : BasePage
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
        #region Permission
        GetPermission(ResourceType.DeliveryReceipt, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        #endregion
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liBA','{0}');", ResourceMgr.GetMessage("Delivery Receipt")), true);

        pageSize = 10;
        if (!IsPostBack)
        {
            DeliveryRecInfo(1);
        }
        if (TotalItemsR > 0)
        {
            pgrLoad.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
        }

    }

    #region Load Function
    /// <summary>
    /// use for pagination
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pgrLoad.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.DeliveryRecInfo(CurrentPage);
        }


        return base.OnBubbleEvent(source, args);
    }
    /// <summary>
    /// use to load the delivery receipt details in grid
    /// </summary>
    /// <param name="pageNo"></param>
    protected void DeliveryRecInfo(int pageNo)
    {
        try
        {
            pageSize = 10;
            gvDeliveryinfo.PageSize = pageSize;
            CurrentPageR = pageNo;
            DateTime frmDate = string.IsNullOrEmpty(txtFrmDeliveryDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFrmDeliveryDate.Text);
            DateTime toDate = string.IsNullOrEmpty(txtToDeliveryDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtToDeliveryDate.Text);

            int count = 0;
            gvDeliveryinfo.DataSource = Delivery.LoadAllReceivedDeliveriesByOrgID(UserOrganizationId, pageNo, pageSize, out count, frmDate, toDate, txtShipFrom.Text, txtDeliveryName.Text,CatId);
            gvDeliveryinfo.DataBind();

            this.TotalItemsR = count;
            this.pgrLoad.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewDeliveryReceipt.DeliveryInfo", ex);
        }

    }
    /// <summary>
    /// use to show the delivery detail popup
    /// </summary>
    /// <param name="DeliveryID"></param>
    public void LoadPopInfobyDeliveryId(int DeliveryID)
    {
        Delivery objDelivery = new Delivery(DeliveryID);
        lblDeliveryDate.Text = Conversion.ParseString(objDelivery.DeliveryDate.ToShortDateString());
        lblDeliveryEstimaDateTime.Text = Conversion.ParseString(objDelivery.DeliveryEstimateDates.ToShortDateString());
        lblDeliveryID.Text = Conversion.ParseString(objDelivery.DeliveryID);
        lblDeliveryName.Text = Conversion.ParseString(objDelivery.DeliveryName);
        lblShipToidnumber.Text = Conversion.ParseString(objDelivery.OrganizationShipTo);
        lblTransporterName.Text = Conversion.ParseString(objDelivery.OrganizationTransporter);
        lblVehicleDetails.Text = Conversion.ParseString(objDelivery.VehicleDetails);
        lblWeight.Text = objDelivery.Weight.ToString();

        try
        {
            string[] loads = objDelivery.LoadIds.Split(',');
            lblTotalLoads.Text = Conversion.ParseString(loads.Length);
            int count = 0;
            gvAllTire.DataSource = Loads.getTiresInfoByLoadIds(objDelivery.LoadIds, 1, pageSize, out count);
            gvAllTire.DataBind();
            lblLoadTireCount.Text = Conversion.ParseString(count);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewDeliveryReceipt.LoadPopInfobyDeliveryId", ex);
        }

    }
    #endregion
    #region Button Events
    /// <summary>
    /// use to search the specific record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DeliveryRecInfo(1);
    }

    /// <summary>
    /// use to hide the popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDeliveryDetailBack_Click(object sender, EventArgs e)
    {
        dvMainLoad.Visible = false;
    }
  
    #endregion

    #region Grid Events
    protected void gvDeliveryinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeliveryInfo")
        {
            dvMainLoad.Visible = true;
            LoadPopInfobyDeliveryId(Convert.ToInt32(e.CommandArgument));
        }
        else if (e.CommandName == "Accept")
        {
            Delivery.acceptRejectDeliveryByDeliveryIdOrgID(Conversion.ParseInt(e.CommandArgument.ToString()), UserOrganizationId, true, false);
            SendNotification(Conversion.ParseInt(e.CommandArgument.ToString()), UserOrganizationId, true);
            DeliveryRecInfo(1);
        }
        else if (e.CommandName == "Reject")
        {
            Delivery.acceptRejectDeliveryByDeliveryIdOrgID(Conversion.ParseInt(e.CommandArgument.ToString()), UserOrganizationId, false, true);
            SendNotification(Conversion.ParseInt(e.CommandArgument.ToString()), UserOrganizationId, false);
            DeliveryRecInfo(1);
        }
    }

    protected void gvDeliveryinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton imgbtnApprove = (LinkButton)e.Row.FindControl("imgbtnApprove");
            LinkButton imgbtnRejected = (LinkButton)e.Row.FindControl("imgbtnRejected");

            Label imgApproved = (Label)e.Row.FindControl("imgApproved");
            Label imgRejected = (Label)e.Row.FindControl("imgRejected");
            Label imgPending = (Label)e.Row.FindControl("imgPending");
            


            if (Conversion.ParseDBNullInt(DataBinder.Eval(e.Row.DataItem, "Organizationid")) == UserOrganizationId)
            {
                if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsShipToAccepted")) == true) 
                {
                    imgbtnApprove.Visible = false;
                    imgbtnRejected.Visible = false;
                    imgApproved.Visible = true;

                }
                else if ((Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsShipToRejected")) == true))
                {
                    imgbtnApprove.Visible = false;
                    imgbtnRejected.Visible = false;
                    imgRejected.Visible = true;
                }
                else
                {
                    imgbtnApprove.Visible = true;
                    imgbtnRejected.Visible = true;
                    imgPending.Visible = true;
                }
            }
            else if (Conversion.ParseDBNullInt(DataBinder.Eval(e.Row.DataItem, "OrganizationTransporterId")) == UserOrganizationId)
            {
                if ((Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsTranspoterAccepted")) == true) )
                {
                    imgbtnApprove.Visible = false;
                    imgbtnRejected.Visible = false;
                    imgApproved.Visible = true;
                }
                else if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsTranspoterRejected")) == true)
                {
                    imgbtnApprove.Visible = false;
                    imgbtnRejected.Visible = false;
                    imgRejected.Visible = true;
                }
                else
                {
                    imgbtnApprove.Visible = true;
                    imgbtnRejected.Visible = true;
                    imgPending.Visible = true;
                }
            }
        }
    }
    #endregion 
    #region Notification
    /// <summary>
    /// use to send notification
    /// </summary>
    /// <param name="DeliveryID"></param>
    /// <param name="NotificationSendToID"></param>
    /// <param name="IsAccepted"></param>
    private void SendNotification(int DeliveryID, int NotificationSendToID, bool IsAccepted)
    {

        Delivery ObjDelivery = new Delivery(DeliveryID);

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
        objNotifications.IntToUserId = 0;

        if (NotificationSendToID == ObjDelivery.OrganizationShipToId)
        {
            objNotifications.IntToOrganizationId = ObjDelivery.OrganizationShipToId;
            if (IsAccepted)
                objNotifications.VchNotificationText = "Delivery '" + txtDeliveryName.Text.Trim() + "'  Ship From  '" + ObjDelivery.OrganizationName + "' To '" + ObjDelivery.OrganizationShipTo + "'  is accepted";
            else
                objNotifications.VchNotificationText = "Delivery '" + txtDeliveryName.Text.Trim() + "'  Ship From  '" + ObjDelivery.OrganizationName + "' To '" + ObjDelivery.OrganizationShipTo + "'  is rejected";
        }
        else if (NotificationSendToID == ObjDelivery.OrganizationTransporterId)
        {
            objNotifications.IntToOrganizationId = ObjDelivery.OrganizationTransporterId;
            if (IsAccepted)
                objNotifications.VchNotificationText = "Delivery '" + txtDeliveryName.Text.Trim() + "'  Ship From  '" + ObjDelivery.OrganizationName + "' To '" + ObjDelivery.OrganizationTransporter + "' is accepted by Transporter '" + ObjDelivery.OrganizationTransporter+"'";
            else
                objNotifications.VchNotificationText = "Delivery '" + txtDeliveryName.Text.Trim() + "'  Ship From  '" + ObjDelivery.OrganizationName + "' To '" + ObjDelivery.OrganizationTransporter + "' is rejected by Transporter '" + ObjDelivery.OrganizationTransporter + "'";
        }

        objNotifications.InsertUpdate();
    }
    #endregion

   
   
}