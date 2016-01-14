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

public partial class DeliveryNotes_AddDeliveryNote : BasePage
{
    #region Properties
    private int TransporterSelection
    {
        get
        {
            return Conversion.ParseInt(Session["trpt"]);
        }
        set
        {
            Session["trpt"] = value;
        }
    }
    private DataTable dt
    {
        get
        {
            return (DataTable)Session["dt"];
        }
        set
        {
            Session["dt"] = value;
        }
    }
    private string _LoadIds
    {
        get
        {
            return Conversion.ParseString(Session["loadIds"]);
        }
        set
        {
            Session["loadIds"] = value;
        }
    }
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
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {


        #region Permission
        GetPermission(ResourceType.AddDeliveryNote, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canAdd)
        {
            Response.Redirect("error");
        }
        #endregion
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liBA','{0}');", ResourceMgr.GetMessage("Delivery Notes")), true);
      
        if (!IsPostBack)
        {
            TransporterSelection = 0;
            _LoadIds = "";
            if (Request.QueryString["DeliveryId"] != null)
            {
                LoadByDeliveryID(Conversion.ParseInt(Request.QueryString["DeliveryId"]));
            }
        }

        #region Script Load and Pager setting
        pageSize = 10;
        //ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        if (rdTransportor.SelectedValue == "3")
        {
            if (TransporterSelection == 1)
            ScriptManager.RegisterStartupScript(this, GetType(), "trnP", "showTranDiv();", true);
        }
        else
        {
            txtTransporterID.Text = string.Empty;
            ltrTranspoterAddress.Text = string.Empty;
            TransporterSelection = 0;
        }
        if (TotalItemsR > 0)
        {
            pgrTiresLoad.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
            this.pgrProduct.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
        }
        #endregion
    }

    #region Load Functions
    /// <summary>
    /// Use for update the Delivery Records
    /// </summary>
    /// <param name="deliveryId">Delivery Id sent from View Delivery Notes Page</param>
    protected void LoadByDeliveryID(int deliveryId)
    {
        Delivery objDelivery = new Delivery(deliveryId);
        txtDeliveryDate.Text = Conversion.ParseString(objDelivery.DeliveryDate.ToShortDateString());
        txtDeliveryEstimaDateTime.Text = Conversion.ParseString(objDelivery.DeliveryEstimateDates.ToShortDateString());
        //txtDeliveryID.Text = Conversion.ParseString(objDelivery.DeliveryID);
        txtDeliveryName.Text = Conversion.ParseString(objDelivery.DeliveryName);
        txtShipToidnumber.Text = Conversion.ParseString(objDelivery.OrganizationShipTo);
        txtTransporterID.Text = Conversion.ParseString(objDelivery.OrganizationTransporter);
        txtVehicleDetails.Text = Conversion.ParseString(objDelivery.VehicleDetails);
        txtWeight.Text = objDelivery.Weight.ToString();

        if (objDelivery.TranspotationType == 1)
            rdTransportor.SelectedValue = "1";
        else if (objDelivery.TranspotationType == 2)
            rdTransportor.SelectedValue = "2";
        else if (objDelivery.TranspotationType == 3)
            rdTransportor.SelectedValue = "3";

        hdLoadIDs.Value = objDelivery.LoadIds;
        _LoadIds = objDelivery.LoadIds;
        hidOrgID.Value = objDelivery.OrganizationShipToId.ToString();
        hdTranspoterID.Value = objDelivery.OrganizationTransporterId.ToString();
        LoadsInfo(1);
        getLoads(objDelivery.LoadIds);
        LoadTireInfoByLoadIds(objDelivery.LoadIds, 1);

    }


    /// <summary>
    /// Used for Paging
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pgrTiresLoad.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.LoadTireInfoByLoadIds(_LoadIds, CurrentPage);
            //this.LoadsInfo(CurrentPage);
        }
        else if (this.pgrProduct.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.LoadProductInfo(_LoadIds, CurrentPage);
        }


        return base.OnBubbleEvent(source, args);
    }

    /// <summary>
    /// use to get the Tire information against the Load Ids
    /// </summary>
    /// <param name="_LoadIds"></param>
    /// <param name="pageNo"></param>
    protected void LoadTireInfoByLoadIds(string _LoadIds, int pageNo)
    {
        dvTire.Visible = true;
        pageSize = 10;
        //CurrentPageR = pageNo;
        int count = 0;
        gvAllTire.DataSource = Loads.getTiresInfoByLoadIds(_LoadIds, pageNo, pageSize, out count);
        gvAllTire.DataBind();
        //gvAllTire.PageSize = count;
        //this.TotalItemsR = count;
        //this.pgrTiresLoad.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);
    }

    /// <summary>
    /// Use to get Company Loads
    /// </summary>
    /// <param name="pageNo"></param>
    protected void LoadsInfo(int pageNo)
    {
        try
        {
            pageSize = 0;// Convert.ToInt32(ddlloadsinfo.SelectedValue);

            //CurrentPageR = pageNo;
            int count = 0;
            DataSet ds = Delivery.GetAllLoadsForDelivery(pageNo, pageSize, out count, string.Empty, DateTime.MinValue, DateTime.MinValue, UserOrganizationId, 0, string.Empty,CatId);
            dt = ds.Tables[0];
            gvGetAllLoads.DataSource = dt;
            gvGetAllLoads.DataBind();
            //gvGetAllLoads.PageSize = count;
            //this.TotalItemsR = count;
            //this.pgrLoad.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);
        }
        catch (Exception ex)
        {

        }

    }

    /// <summary>
    /// Use to get the selected load details from Load dataset of popup
    /// </summary>
    /// <param name="_LoadIds"></param>
    private void getLoads(string _LoadIds)
    {
        DataTable dtGetAll = dt;
        dtGetAll.DefaultView.RowFilter = "[LoadId] in (" + _LoadIds + ")";
        DataTable dtOutput = dtGetAll.DefaultView.ToTable();
        gvAllLoads.DataSource = dtOutput;
        gvAllLoads.DataBind();
    }

    /// <summary>
    /// Use to get the companis within same stewardship
    /// </summary>
    private void LoadCompaniesGrid()
    {
        try
        {
            //string standardstewardshipIds = "";
            //standardstewardshipIds = System.Configuration.ConfigurationManager.AppSettings["StewardshipStandardIDs"];
            //OrganizationInfo objorg = new OrganizationInfo(UserOrganizationId);
            //string[] arr = standardstewardshipIds.Split(',');
            //bool isstewardship = false;
            //foreach (var id in arr)
            //{
            //    if (Conversion.ParseInt(id) == objorg.OrganizationTypeId)
            //        isstewardship = true;
            //}
            ////  UserInfo userobj = new UserInfo(LoginMemberId);
            //if (isstewardship)
            //{
            //    DataSet ds = Lots.GetAllCompanieswithAddressbyStewardshipId(UserOrganizationId);
            //    grvOrganizations.DataSource = ds;
            //    grvOrganizations.DataBind();
            //    grvTanspoterOrganizations.DataSource = ds;
            //    grvTanspoterOrganizations.DataBind();
            //}
            //else
            //{
            DataSet ds = Lots.GetAllCompanieswithAddressWithinSameStewardship(UserOrganizationId, CatId);
                grvOrganizations.DataSource = ds;
                grvOrganizations.DataBind();
                grvTanspoterOrganizations.DataSource = ds;
                grvTanspoterOrganizations.DataBind();
            //}

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "lotInfo .LoadCompaniesGrid", ex);
        }
    }

    /// <summary>
    /// Use to get all transporter companies within same stewardship
    /// </summary>
    private void LoadTransporterCompaniesGrid()
    {
        try
        {
            DataSet ds = Lots.GetAllTransporterCompanieswithAddressWithinSameStewardship(UserOrganizationId);
            grvOrganizations.DataSource = ds;
            grvOrganizations.DataBind();
            grvTanspoterOrganizations.DataSource = ds;
            grvTanspoterOrganizations.DataBind();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "lotInfo .LoadTransporterCompaniesGrid", ex);
        }
    }
    #endregion

    #region Ship To Company Button Events


    /// <summary>
    /// Use to show the selected companies details from Popup to Main page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkCompany_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hidOrgID.Value) && (grvOrganizations.Rows.Count > 0))
        {
            lblErrorPermanentLotdv.Text = "Please select Organization";
            return;
        }

        else if (grvOrganizations.Rows.Count == 0)
        {
            lblErrorPermanentLotdv.Text = "You have no Organization";
            return;
        }
        txtShipToidnumber.Text = hidText.Value;
        ltrAddress.Text = hidStreetAddress.Value;
        ltrAddress.Text += "<br/>" + hidStateAddress.Value;
        dvOrganization.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;

    }

    /// <summary>
    /// Use for ShipTo field to show the companies in popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkSearchCompany_Click(object sender, EventArgs e)
    {
        LoadCompaniesGrid();
        dvOrganization.Visible = true;
    }
    /// <summary>
    /// to hid the Company popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        dvOrganization.Visible = false;
        lblErrorPermanentLotdv.Text = string.Empty;
    }


    #endregion

    #region Load Button Events
    /// <summary>
    /// use to Add Batch Button to show the Loads in popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkGetAllLoads_Click(object sender, EventArgs e)
    {
        LoadsInfo(1);
        dvGetAllLoad.Visible = true;


    }
    /// <summary>
    /// Cancel the Load Popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkGetAllLoadCancel_Click(object sender, EventArgs e)
    {
        dvGetAllLoad.Visible = false;
        lblGetAllLoadError.Text = string.Empty;
    }

    /// <summary>
    /// Use to set the selected load from popup to main page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkGetAllLoadContinue_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hdLoadIDs.Value) && (gvGetAllLoads.Rows.Count > 0))
        {
            lblGetAllLoadError.Text = "Please select Organization";
            return;
        }
        else if (gvGetAllLoads.Rows.Count == 0)
        {
            lblGetAllLoadError.Text = "You have no Organization";
            return;
        }
        try
        {
            string _loadIds = hdLoadIDs.Value;
            _loadIds = _loadIds.Replace("\r\n", "").Replace(" ", "").Trim(',');
            string removeids = hdRemoveLoadIds.Value;
            removeids = removeids.Replace("\r\n", "").Replace(" ", "").Trim(',');
            string[] remList = removeids.Split(',');
            if (remList.Length > 0)
                for (int i = 0; i <= remList.Length - 1; i++)
                {
                    if (_loadIds.Contains("," + remList[i].ToString() + ","))
                    {
                        _loadIds = _loadIds.Replace("," + remList[i].ToString() + ",", ",");
                        _loadIds = _loadIds.Replace(" ", "").Trim(',');
                    }
                    else if (_loadIds.Contains(remList[i].ToString() + ","))
                    {
                        _loadIds = _loadIds.Replace(remList[i].ToString() + ",", ",");
                        _loadIds = _loadIds.Replace(" ", "").Trim(',');
                    }
                    else if (_loadIds.Contains("," + remList[i].ToString()))
                    {
                        _loadIds = _loadIds.Replace("," + remList[i].ToString(), ",");
                        _loadIds = _loadIds.Replace(" ", "").Trim(',');
                    }
                }
            _LoadIds = _loadIds;
            hdLoadIDs.Value = "";
        }
        catch (Exception ex) { }

        dvGetAllLoad.Visible = false;
        lblGetAllLoadError.Text = string.Empty;
        lblErrorMessageLoad.Visible = false;
        lblErrorMessageLoad.Text = "";
        getLoads(_LoadIds);
        //DataTable dtGetAll = dt;
        //dtGetAll.DefaultView.RowFilter = "[loadid] in (" + _LoadIds + ")";
        //DataTable dtOutput = dtGetAll.DefaultView.ToTable();
        //gvAllLoads.DataSource = dtOutput;
        //gvAllLoads.DataBind();
        if (CatId == (int)ProductCategory.Tire)
        {
            LoadTireInfoByLoadIds(_LoadIds, 1);
        }
        else
        {
            LoadProductInfo(_LoadIds, 1);
        }
        

    }

    private void LoadProductInfo(string _LoadIds, int PageNo)
    {
        try
        {
            dvProduct.Visible = true;
            pageSize = 10;
            CurrentPageR = PageNo;
            int count = 0;
            gvAllProduct.DataSource = Loads.GetProductsInfoByLoadIds(_LoadIds, PageNo, pageSize, out count, CatId);
            gvAllProduct.DataBind();
            gvAllProduct.PageSize = count;
            this.TotalItemsR = count;
            this.pgrProduct.DrawPager(PageNo, TotalItemsR, pageSize, MaxPagesToShow);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddDeliveryNotes.aspx.cs LoadProductInfo", ex);
        }
        
    }
    #endregion

    #region Transporter Button Events



    /// <summary>
    /// use for Transporter Radio button to show the companies in popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkGetTransporters_Click(object sender, EventArgs e)
    {
        LoadTransporterCompaniesGrid();
        dvTranspoterOrganization.Visible = true;
        TransporterSelection = 1;

    }

    /// <summary>
    /// to hide the Transporter popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkTranspoterCancel_Click(object sender, EventArgs e)
    {
        dvTranspoterOrganization.Visible = false;
        lblErrorTransporter.Text = string.Empty;
    }


    /// <summary>
    /// Use to show the selected transporter companies details from Popup to Main page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkTranporterContinue_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hdTranspoterID.Value) && (grvTanspoterOrganizations.Rows.Count > 0))
        {
            lblErrorTransporter.Text = "Please select Organization";
            return;
        }

        else if (grvTanspoterOrganizations.Rows.Count == 0)
        {
            lblErrorTransporter.Text = "You have no Organization";
            return;
        }
        txtTransporterID.Text = hdTransporterName.Value;
        ltrTranspoterAddress.Text = hdTransporterStreetAddress.Value;
        ltrTranspoterAddress.Text += "<br/>" + hdTransporterStateAddress.Value;
        dvTranspoterOrganization.Visible = false;
        lblErrorTransporter.Text = string.Empty;
        TransporterSelection = 1;

    }

    #endregion

    #region Add Delivery Button Events

    /// <summary>
    /// Use to save the data in delivery notes table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkbtnAddDelivery_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDeliveryName.Text) || string.IsNullOrEmpty(txtDeliveryDate.Text) || string.IsNullOrEmpty(txtShipToidnumber.Text))
        {
            return;
        }
        else if (gvAllLoads.Rows.Count == 0)
        {
            lblErrorMessageLoad.Visible = true;
            lblErrorMessageLoad.Text = "Please select Load/Batch";
            return;
        }

        int result = InserUpdateDelivery();
        if (result > 0)
        {
            SendNotification(result, Conversion.ParseInt(hidOrgID.Value));
            if (rdTransportor.SelectedValue == "3")
                SendNotification(result, Conversion.ParseInt(hdTranspoterID.Value));

            Response.Redirect("deliverynotes");
        }

    }

    /// <summary>
    /// use to cancel the new delivery and redirect to View Delivery Notes page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkbtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("deliverynotes");
    }
    #endregion

    #region Insert Function
    /// <summary>
    /// Use the set the fields values to Delivery class instance and save the record 
    /// </summary>
    /// <returns></returns>
    private int InserUpdateDelivery()
    {
        int result = -1;
        try
        {

            Delivery objDelivery = new Delivery();
            objDelivery.IsActive = true;
            objDelivery.Createdby = currentUserInfo.UserId;
            objDelivery.Datecreated = DateTime.Now;
            objDelivery.Datemodified = DateTime.Now;
            objDelivery.DeliveryDate = Conversion.ParseDateTime(txtDeliveryDate.Text);
            objDelivery.DeliveryEstimateDates = Conversion.ParseDateTime(txtDeliveryEstimaDateTime.Text);

            if (Request.QueryString["DeliveryId"] != null)
            {
                objDelivery.DeliveryID = Conversion.ParseInt(Request.QueryString["DeliveryId"]);
            }
            
            objDelivery.DeliveryName = txtDeliveryName.Text;
            objDelivery.LoadIds = _LoadIds;
            objDelivery.Modifiedby = currentUserInfo.UserId;
            objDelivery.Organizationid = UserOrganizationId;
            objDelivery.OrganizationShipToId = Conversion.ParseInt(hidOrgID.Value);

            objDelivery.Status = "";
            if (rdTransportor.SelectedValue == "1")
            {
                objDelivery.TranspotationType = 1;
                //objDelivery.OrganizationTransporterId = UserOrganizationId;
            }
            else if (rdTransportor.SelectedValue == "2")
            {
                objDelivery.TranspotationType = 2;
                //objDelivery.OrganizationTransporterId = UserOrganizationId;
            }
            else if (rdTransportor.SelectedValue == "3")
            {
                objDelivery.TranspotationType = 3;
                objDelivery.OrganizationTransporterId = Conversion.ParseInt(hdTranspoterID.Value);
            }
            objDelivery.VehicleDetails = txtVehicleDetails.Text;
            objDelivery.Weight = Conversion.ParseDecimal(txtWeight.Text);
            result = Delivery.InsertUpdateDelivery(objDelivery);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "AddDeliveryNotes.InserUpdateDelivery", ex);
        }
        return result;
    }
    #endregion

    #region Notification function
    /// <summary>
    /// use to send the notification to the respected organization
    /// </summary>
    /// <param name="DeliveryID"></param>
    /// <param name="NotificationSendToID"></param>
    private void SendNotification(int DeliveryID, int NotificationSendToID)
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
            objNotifications.VchNotificationText = "Delivery '" + txtDeliveryName.Text.Trim() + "'  Ship From  '" + ObjDelivery.OrganizationName + "' To '" + ObjDelivery.OrganizationShipTo + "' .";
        }
        else if (NotificationSendToID == ObjDelivery.OrganizationTransporterId)
        {
            objNotifications.IntToOrganizationId = ObjDelivery.OrganizationTransporterId;
            objNotifications.VchNotificationText = "Delivery '" + txtDeliveryName.Text.Trim() + "'  Ship From  '" + ObjDelivery.OrganizationName + "' To '" + ObjDelivery.OrganizationTransporter + "' and Transporter '" + ObjDelivery.OrganizationTransporter + "' .";
        }

        objNotifications.InsertUpdate();
    }
    #endregion
}