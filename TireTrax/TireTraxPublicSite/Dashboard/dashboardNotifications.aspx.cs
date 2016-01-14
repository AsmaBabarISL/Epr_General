using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class Dashboard_dashboardNotifications : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('lidashboardNotfications','{0}');", ResourceMgr.GetMessage("Notifications")), true);
        if (!IsPostBack)
        {

            lvmessagesmaindetail.DataSource = Notifications.getAllNotificationsReadUnread(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId,  true,10,1);
            lvmessagesmaindetail.DataBind();
            lvmessagesmain.DataSource = Notifications.getAllNotifications(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId, false, true,10,1);
            lvmessagesmain.DataBind();
            dvpopupaddnotification.Visible = false;
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
        
        }else
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
        }
        else
            if (e.CommandName == "Delete")
            {
                Notifications objNotifications = new Notifications(Conversion.ParseInt(e.CommandArgument));
                objNotifications.BitIsActive = false;
                objNotifications.InsertUpdate();
            }
      
        lvmessagesmain.DataSource = Notifications.getAllNotifications(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId, false, true,10,1);
        lvmessagesmain.DataBind();
        lvmessagesmaindetail.DataSource = Notifications.getAllNotificationsReadUnread(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId,  true, 10, 1);
        lvmessagesmaindetail.DataBind();
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
            objNotifications.IntToOrganizationId =Conversion.ParseInt( hfToId.Value);
            objNotifications.IntFromUserId = 0;
            objNotifications.IntToUserId = 0;
        }
        else
        {
            objNotifications.IntFromOrganizationId = 0;
            objNotifications.IntToOrganizationId =0 ;
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
        lvmessagesmain.DataSource = Notifications.getAllNotifications(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId, false, true,10,1);
        lvmessagesmain.DataBind();
        lvmessagesmaindetail.DataSource = Notifications.getAllNotificationsReadUnread(UserInfo.GetCurrentUserInfo().UserId, UserOrganizationId,  true, 10, 1);
        lvmessagesmaindetail.DataBind();

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