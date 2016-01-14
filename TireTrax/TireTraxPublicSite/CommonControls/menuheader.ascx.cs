using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using TireTraxLib;


public partial class CommonControls_menuheader : BaseControl
{
    bool subMenu = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetMenuPermission();
            SubMenuLoad();
        }
    }

    private void GetMenuPermission()
    {
        try
        {
            UserInfo obj = UserInfo.GetCurrentUserInfo();

            DataSet dt = TireTraxLib.Security.Permission.GetHeaderWithPermissions(UserInfo.GetCurrentUserInfo().UserGroupsCommaSeprated);
            if (dt != null && dt.Tables[0].Rows.Count > 0)
            {
                Rptmenu.DataSource = dt;
                Rptmenu.DataBind();
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "HeaderMenu.ascx GetMenuPermission", ex);
        }
    }

    private void SubMenuLoad()
    {
        try
        {
            UserInfo obj = UserInfo.GetCurrentUserInfo();

            DataSet dt = TireTraxLib.Security.Permission.GetSubHeaderWithPermissions(UserInfo.GetCurrentUserInfo().UserGroupsCommaSeprated);
            if (dt != null && dt.Tables[0].Rows.Count > 0)
            {
                rttSubMenu.DataSource = dt;
                rttSubMenu.DataBind();
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "HeaderMenu.ascx GetSubMenuPermission", ex);
        }
    }

    protected void ItemBound(object sender, RepeaterItemEventArgs args)
    {
        if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hid = (HiddenField)args.Item.FindControl("hidResourceId");
            Literal ltrSubMenu = (Literal)args.Item.FindControl("ltrMenu");
            StringBuilder str = new StringBuilder(255);
            if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Admin"))
            {

            }
            else
            {
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Home"))
                {
                    GetPermission(ResourceType.Home, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@" <a href='dashboard' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        if (Request.Url.AbsolutePath.ToLower().Contains("dashboard") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "dashboard")
                            str.Append("' class='selected-tabs'>");
                        else
                            str.Append("'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</a>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Stakeholders"))
                {
                    GetPermission(ResourceType.Stakeholders, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@" <a href='Stakeholders' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        if (Request.Url.AbsolutePath.ToLower().Contains("stakeholders") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "stakeholders")
                            str.Append("' class='selected-tabs'>");
                        else
                            str.Append("'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</a>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Applications"))
                {
                    GetPermission(ResourceType.Applications, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@" <a href='Applications' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        if (Request.Url.AbsolutePath.ToLower().Contains("applications") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "applications")
                            str.Append("' class='selected-tabs'>");
                        else
                            str.Append("'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</a>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Inventory"))
                {
                    GetPermission(ResourceType.Inventory, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@" <a href='lotinfo' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        if (Request.Url.AbsolutePath.ToLower().Contains("lotinfo") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "inventory")
                            str.Append("' class='selected-tabs'>");
                        else
                            str.Append("'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</a>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Revenue"))
                {
                    GetPermission(ResourceType.Revenue, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@" <a href='Revenue' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        if (Request.Url.AbsolutePath.ToLower().Contains("revenue") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "revenue")
                            str.Append("' class='selected-tabs'>");
                        else
                            str.Append("'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</a>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Reports"))
                {
                    GetPermission(ResourceType.Reports, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@" <a href='Reports' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        if (Request.Url.AbsolutePath.ToLower().Contains("reports") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "reports")
                            str.Append("' class='selected-tabs'>");
                        else
                            str.Append("'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</a>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Users"))
                {
                    GetPermission(ResourceType.Users, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@" <a href='Users' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        if (Request.Url.AbsolutePath.ToLower().Contains("users") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "users")
                            str.Append("' class='selected-tabs'>");
                        else
                            str.Append("'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</a>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("PTE"))
                {
                    GetPermission(ResourceType.PTE, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@" <a href='Settings' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        if (Request.Url.AbsolutePath.ToLower().Contains("settings") || DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower().Contains("ptestandard"))
                            str.Append("' class='selected-tabs'>");
                        else
                            str.Append("'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</a>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Settings"))
                {
                    GetPermission(ResourceType.Settings, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@" <a href='logosetting' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        if (Request.Url.AbsolutePath.ToLower().Contains("logosetting") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString() == "settings")
                            str.Append("' class='selected-tabs'>");
                        else
                            str.Append("'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</a>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Account Management"))
                {
                    GetPermission(ResourceType.AccountManagement, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@" <a href='invoices' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        if (Request.Url.AbsolutePath.ToLower().Contains("invoices") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString() == "Account Management")
                            str.Append("' class='selected-tabs'>");
                        else
                            str.Append("'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</a>");
                    }
                }
                ltrSubMenu.Text = ltrSubMenu.Text + str;
            }
            
        }
        else if (!subMenu && args.Item.ItemType == ListItemType.Footer)
        {
            Literal litfooter = (Literal)args.Item.FindControl("litfooter");
            litfooter.Text = "</ul></div></div>";

        }
    }

    protected void ItemSubMenuBound(object sender, RepeaterItemEventArgs args)
    {
        if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal ltrSubMenu = (Literal)args.Item.FindControl("litSubMenuCtrl");
            StringBuilder str = new StringBuilder();
            if (Request.Url.AbsolutePath.ToLower().Contains("ptestandard") || Request.Url.AbsolutePath.ToLower().Contains("settings"))
            {
                if ( DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("settings"))
                {
                    GetPermission(ResourceType.LogoSettings, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='settings'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Standard") )
                {
                    GetPermission(ResourceType.LogoSettings, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='PTEStandard'");
                        str.Append(@" id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                rttSubMenu.Visible = true;
            }

            if (Request.Url.AbsolutePath.ToLower().Contains("lotinfo")
                || Request.Url.AbsolutePath.ToLower().Contains("inventory-load") || Request.Url.AbsolutePath.ToLower().Contains("addparkinglot") || Request.Url.AbsolutePath.ToLower().Contains("inventory-tire") || Request.Url.AbsolutePath.ToLower().Contains("addinventory") || Request.Url.AbsolutePath.ToLower().Contains("lots") || Request.Url.AbsolutePath.ToLower().Contains("addload")
                || Request.Url.AbsolutePath.ToLower().Contains("facility") || Request.Url.AbsolutePath.ToLower().Contains("deliverynotes") || Request.Url.AbsolutePath.ToLower().Contains("adddeliverynote") || Request.Url.AbsolutePath.ToLower().Contains("deliveryreceipt"))
            {
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Lots"))
                {
                    GetPermission(ResourceType.Lots, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='lotinfo'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Loads"))
                {
                    GetPermission(ResourceType.Loads, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='inventory-load'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Facility"))
                {
                    GetPermission(ResourceType.Facility, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='facility'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Delivery Notes"))
                {
                    GetPermission(ResourceType.DeliveryNotes, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='deliverynotes'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Delivery Receipt"))
                {
                    GetPermission(ResourceType.DeliveryReceipt, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='deliveryreceipt'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                rttSubMenu.Visible = true;
            }
            if (Request.Url.AbsolutePath.ToLower().Contains("logosetting")
                || Request.Url.AbsolutePath.ToLower().Contains("bankaccount")
                || Request.Url.AbsolutePath.ToLower().Contains("creditcard")
                || Request.Url.AbsolutePath.ToLower().Contains("profile")
                 || Request.Url.AbsolutePath.ToLower().Contains("templates"))
            {
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Logo Settings"))
                {
                    GetPermission(ResourceType.LogoSettings, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='logosetting'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Bank Account"))
                {
                    GetPermission(ResourceType.BankAccount, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='bankaccount'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Credit Card"))
                {
                    GetPermission(ResourceType.CreditCard, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='creditcard'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Profile"))
                {
                    GetPermission(ResourceType.Profile, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='profile'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Templates"))
                {
                    GetPermission(ResourceType.Templates, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='templates'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
               
                rttSubMenu.Visible = true;
            }

            if (Request.Url.AbsolutePath.ToLower().Contains("invoices") )//|| Request.Url.AbsolutePath.ToLower().Contains("AddInvoice"))
            {
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Invoices"))
                {
                    GetPermission(ResourceType.LogoSettings, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        str.Append(@"<li><a href='invoices'");
                        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("</a></li>");
                    }
                }
                //if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("AddInvoice"))
                //{
                //    GetPermission(ResourceType.LogoSettings, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                //    if (canView)
                //    {
                //        str.Append(@"<li><a href='invoices'");
                //        str.Append(@"id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString() + "'>");
                //        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                //        str.Append("</a></li>");
                //    }
                //}
                rttSubMenu.Visible = true;
            }

            ltrSubMenu.Text = ltrSubMenu.Text + str;
        }
        else if (args.Item.ItemType == ListItemType.Footer)
        {
            //Literal litfooter = (Literal)args.Item.FindControl("litSumMenuFooter");
            //litfooter.Text = "</ul></div></div>";
        }
    }
}
