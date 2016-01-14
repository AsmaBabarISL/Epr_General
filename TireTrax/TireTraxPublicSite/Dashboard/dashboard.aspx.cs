using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class Dashboard_dashboard : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Authent();
            loaddropdown();
            dvpopupaddnotification.Visible = false;
            LoadCharts();
        }
        
    }

    private void LoadCharts()
    {
        try
        {
            string pYear = "Year " + DateTime.Now.Year.ToString();
            string pMoth = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
            int[] Quarters = new int[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4 };
            string pQuarter = "Quarter " + Quarters[DateTime.Now.Month - 1].ToString();
            int pHalf = (DateTime.Now.Month - 1) / 6;

            DataSet dsYearly = RevenuInventory.GetRevenueByYear(UserOrganizationId, DateTime.Now.Year.ToString());
            DataSet dsMonthly = RevenuInventory.GetRevenueByMonth(UserOrganizationId, pMoth, DateTime.Now.Year.ToString());
            DataSet dsQuarterly = RevenuInventory.GetRevenueByQuarter(UserOrganizationId, pQuarter, DateTime.Now.Year.ToString());
            DataSet dsBiannually = RevenuInventory.GetRevenueByHalfYear(UserOrganizationId, pHalf, DateTime.Now.Year.ToString());
            
            if (dsYearly != null && dsYearly.Tables.Count > 0)
            {
                if (dsYearly.Tables[0].Rows.Count > 0)
                {
                    lblYearlyRevenue.Text = dsYearly.Tables[0].Rows[0]["InvoiceAmount"].ToString();
                    if (string.IsNullOrEmpty(lblYearlyRevenue.Text))
                    {
                        lblYearlyRevenue.Text = "0.00";
                    }
                }
                else
                {
                    lblYearlyRevenue.Text = "0.00";
                }
            }

            if (dsMonthly != null && dsMonthly.Tables.Count > 0)
            {
                if (dsMonthly.Tables[0].Rows.Count > 0)
                {
                    lblMonthly.Text = dsMonthly.Tables[0].Rows[0]["InvoiceAmount"].ToString();
                    if (string.IsNullOrEmpty(lblMonthly.Text))
                    {
                        lblMonthly.Text = "0.00";
                    }
                }
                else
                {
                    lblMonthly.Text = "0.00";
                }
            }

            if (dsQuarterly != null && dsQuarterly.Tables.Count > 0)
            {
                if (dsQuarterly.Tables[0].Rows.Count > 0)
                {
                    lblQuarterly.Text = dsQuarterly.Tables[0].Rows[0]["InvoiceAmount"].ToString();
                    if (string.IsNullOrEmpty(lblQuarterly.Text))
                    {
                        lblQuarterly.Text = "0.00";
                    }
                }
                else
                {
                    lblQuarterly.Text = "0.00";
                }
            }

            if (dsBiannually != null && dsBiannually.Tables.Count > 0)
            {
                if (dsBiannually.Tables[0].Rows.Count > 0)
                {
                    lblHalfYearly.Text = dsBiannually.Tables[0].Rows[0]["InvoiceAmount"].ToString();
                    if (string.IsNullOrEmpty(lblHalfYearly.Text))
                    {
                        lblHalfYearly.Text = "0.00";
                    }
                }
                else
                {
                    lblHalfYearly.Text = "0.00";
                }
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ClientDashboard LoadCharts", ex);
        }
        



    }
    private void loaddropdown()
    {
       
        lvmessagesmain.DataSource = Notifications.getAllNotifications(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId, false, true, 7, 1);
        lvmessagesmain.DataBind();
    }
    private void Authent()
    {
        if (User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("/");
        }
        //ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('Dashboard');", true);
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liDashboard','{0}');", ResourceMgr.GetMessage("Dashboard")), true);
    }
    protected void TmrNotificationlv_Tick(object sender, EventArgs e)
    {
       
        lvmessagesmain.DataSource = Notifications.getAllNotifications(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId, false, true, 7, 1);
        lvmessagesmain.DataBind();
       

        
    }
    protected void gvMessages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ImageButton imgSt = (ImageButton)e.Row.FindControl("imgStatus");
            //    bool bitstatus = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "bitImage"));

            //    if (bitstatus == true)
            //    {
            //        imgSt.ImageUrl = "/images/active.png";
            //    }
            //    else
            //    {
            //        imgSt.ImageUrl = "/images/inactive.png";
            //    }
            //}
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Generic Error", ex);

        }

    }

    protected void lvmessagesmain_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "OpenPopupFromUser")
        {
            int fromId = (Conversion.ParseInt(e.CommandArgument));
            hfToId.Value = fromId.ToString();
            hfIsOrg.Value = "false";
            lvmessages.DataSource = Notifications.getNotifications(fromId, UserInfo.GetCurrentUserInfo().UserId, 0, 0, false, true);
            lvmessages.DataBind();
            dvpopupaddnotification.Visible = true;

        }
        else
            if (e.CommandName == "OpenPopupFromOrg")
            {
                int fromId = (Conversion.ParseInt(e.CommandArgument));
                hfToId.Value = fromId.ToString();
                hfIsOrg.Value = "true";
                lvmessages.DataSource = Notifications.getNotifications(0, 0, fromId, UserOrganizationId, false, true);
                lvmessages.DataBind();
                dvpopupaddnotification.Visible = true;

            }
            else
        if (e.CommandName == "MarkRead")
        {
            Notifications objNotifications = new Notifications(Conversion.ParseInt(e.CommandArgument));
            objNotifications.BitIsReaded = true;
            objNotifications.DtmDateReaded = DateTime.Now;
            objNotifications.InsertUpdate();
        }else
        if (e.CommandName == "Delete")
        {
            Notifications objNotifications = new Notifications(Conversion.ParseInt(e.CommandArgument));
            objNotifications.BitIsActive = false;
            objNotifications.InsertUpdate();
        }
     
        lvmessagesmain.DataSource = Notifications.getAllNotifications(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId, false, true, 7, 1);
        lvmessagesmain.DataBind();
    }
   

    protected void btnClosePopup_Click(object sender, EventArgs e)
    {
        lvmessages.DataSource = null;
        lvmessages.DataBind();
        txtNotification.Text = string.Empty;
        dvpopupaddnotification.Visible = false;
    }
    protected void btnSendNotification_Click(object sender, EventArgs e)
    {
        Notifications objNotifications = new Notifications();
        objNotifications.BitIsActive = true;
        objNotifications.BitIsReaded = false;
        objNotifications.DtmDateCreated = DateTime.Now;
        objNotifications.DtmDateReaded = DateTime.MinValue;
        if (hfIsOrg.Value == "true")
        {
            objNotifications.IntFromOrganizationId = UserOrganizationId;
            objNotifications.IntToOrganizationId = Conversion.ParseInt(hfToId.Value);
            objNotifications.IntFromUserId = 0;
            objNotifications.IntToUserId = 0;
        }
        else
        {
            objNotifications.IntFromOrganizationId = 0;
            objNotifications.IntToOrganizationId = 0;
            objNotifications.IntFromUserId = UserInfo.GetCurrentUserInfo().UserId;
            objNotifications.IntToUserId = Conversion.ParseInt(hfToId.Value);
        }

        objNotifications.IntParentNotificationId = 0;
        objNotifications.IntSourceId = 0;


        objNotifications.VchNotificationText = txtNotification.Text.Trim();
        txtNotification.Text = string.Empty;
        objNotifications.InsertUpdate();
        if (hfIsOrg.Value == "true")
        {
            int fromId = Conversion.ParseInt(hfToId.Value);
            lvmessages.DataSource = Notifications.getNotifications(0, 0, fromId, UserOrganizationId, false, true);
            lvmessages.DataBind();
        }
        else
        {
            int fromId = Conversion.ParseInt(hfToId.Value);
            lvmessages.DataSource = Notifications.getNotifications(fromId, UserInfo.GetCurrentUserInfo().UserId, 0, 0, false, true);
            lvmessages.DataBind();
        }
        lvmessagesmain.DataSource = Notifications.getAllNotifications(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId, false, true, 7, 1);
        lvmessagesmain.DataBind();

    }
    protected void lvmessages_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "MarkRead")
        {
            Notifications objNotifications = new Notifications(Conversion.ParseInt(e.CommandArgument));
            objNotifications.BitIsReaded = true;
            objNotifications.DtmDateReaded = DateTime.Now;
            objNotifications.InsertUpdate();
        }
        if (e.CommandName == "Delete")
        {
            Notifications objNotifications = new Notifications(Conversion.ParseInt(e.CommandArgument));
            objNotifications.BitIsActive = false;
            objNotifications.InsertUpdate();
        }
        lvmessages.DataSource = Notifications.getNotifications(Conversion.ParseInt(hfToId), UserInfo.GetCurrentUserInfo().UserId, 0, 0, false, true);
        lvmessages.DataBind();

    }
   
}