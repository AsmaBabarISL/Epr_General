using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Configuration;
using System.Text.RegularExpressions;

public partial class CommonControls_LeftNavigation :BaseControl
{
    bool subMenu = false;
    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (!IsPostBack)
        {

            DataTable dt = OrganizationInfo.GetLogoPath(UserOrganizationId);
            if (dt != null && dt.Rows.Count > 0)
            {
                string str = ConfigurationManager.AppSettings["LogoUploadLocation"] + dt.Rows[0]["vchLogoPath"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["vchLogoPath"].ToString()))
                {
                    str = str.Replace("//", "/");
                    logoimg.Src = str;
                }
            }
            UserInfo obj = UserInfo.GetCurrentUserInfo();
            UserInfo objUser = new UserInfo(obj.UserId);
            if (objUser.UserProfileImage != null)
            {
                profileimage.Src = "data:image/(gif|png|jpeg|jpg);base64," + Convert.ToBase64String(objUser.UserProfileImage);
            }
        }
        GetMenuPermission();
        GetOrganizationAndRoleName();
        //lblProduct.Text = CatName; 
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

    private void GetOrganizationAndRoleName()
    {
        UserInfo objUser = new UserInfo(LoginMemberId);
        lblRoleName.Text = objUser.SubRoleName;
        //lblCompanyName.Text = OrganizationInfo.GetOrgLegalNameByOrgId(UserOrganizationId);
        lblCompanyName.Text = objUser.FirstName + " " + objUser.LastName;
    }

    private string SubMenuLoad(int ParentId)
    {
        string SubMenuText = "";
        try
        {
            UserInfo obj = UserInfo.GetCurrentUserInfo();
            StringBuilder str = new StringBuilder();
            DataSet ds = TireTraxLib.Security.Permission.GetSubHeaderWithPermissions(UserInfo.GetCurrentUserInfo().UserGroupsCommaSeprated,ParentId);
            DataTable dt = null;
            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                   
                    //if (Request.Url.AbsolutePath.ToLower().Contains("ptestandard") || Request.Url.AbsolutePath.ToLower().Contains("settings"))
                    //{
                    //    if (dRow["vchName"].ToString().Equals("settings")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("settings")
                    //    {
                    //        GetPermission(ResourceType.LogoSettings, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    //        if (canView)
                    //        {
                    //            str.Append(@"<li><a href='settings'");
                    //            str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                    //            str.Append(dRow["vchName"].ToString());
                    //            str.Append("</a></li>");
                    //        }
                    //    }
                    //    if (dRow["vchName"].ToString().Equals("Standard")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Standard")
                    //    {
                    //        GetPermission(ResourceType.LogoSettings, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    //        if (canView)
                    //        {
                    //            str.Append(@"<li><a href='PTEStandard'");
                    //            str.Append(@" id='link_" + dRow["vchName"].ToString() + "'>");
                    //            str.Append(dRow["vchName"].ToString());
                    //            str.Append("</a></li>");
                    //        }
                    //    }

                    //}

                    if (Request.Url.AbsolutePath.ToLower().Contains("lotinfo")
                        || Request.Url.AbsolutePath.ToLower().Contains("inventory-load") || Request.Url.AbsolutePath.ToLower().Contains("addparkinglot") || Request.Url.AbsolutePath.ToLower().Contains("inventory-tire") || Request.Url.AbsolutePath.ToLower().Contains("addinventory") || Request.Url.AbsolutePath.ToLower().Contains("lots") || Request.Url.AbsolutePath.ToLower().Contains("addload")
                        || Request.Url.AbsolutePath.ToLower().Contains("facility") || Request.Url.AbsolutePath.ToLower().Contains("deliverynotes") || Request.Url.AbsolutePath.ToLower().Contains("adddeliverynote") || Request.Url.AbsolutePath.ToLower().Contains("deliveryreceipt"))
                    {
                        if (dRow["vchName"].ToString().Equals("Lots")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Lots")
                        {
                            GetPermission(ResourceType.Lots, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='lotinfo'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                                str.Append(dRow["vchName"].ToString());
                                str.Append("</a></li>");
                            }
                        }
                        if (dRow["vchName"].ToString().Equals("Loads")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Loads")
                        {
                            GetPermission(ResourceType.Loads, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='inventory-load'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                                str.Append(dRow["vchName"].ToString());
                                str.Append("</a></li>");
                            }
                        }
                        if (dRow["vchName"].ToString().Equals("Facility")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Facility")
                        {
                            GetPermission(ResourceType.Facility, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='facility'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                                str.Append(dRow["vchName"].ToString());
                                str.Append("</a></li>");
                            }
                        }
                        if (dRow["vchName"].ToString().Equals("Delivery Notes")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Delivery Notes")
                        {
                            GetPermission(ResourceType.DeliveryNotes, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='deliverynotes'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                                str.Append(dRow["vchName"].ToString());
                                str.Append("</a></li>");
                            }
                        }
                        if (dRow["vchName"].ToString().Equals("Delivery Receipt")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Delivery Receipt")
                        {
                            GetPermission(ResourceType.DeliveryReceipt, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='deliveryreceipt'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                                str.Append(dRow["vchName"].ToString());
                                str.Append("</a></li>");
                            }
                        }

                    }
                    if (Request.Url.AbsolutePath.ToLower().Contains("logosetting")
                        || Request.Url.AbsolutePath.ToLower().Contains("bankaccount")
                        || Request.Url.AbsolutePath.ToLower().Contains("creditcard")
                        //|| Request.Url.AbsolutePath.ToLower().Contains("profile")
                        || Request.Url.AbsolutePath.ToLower().Contains("templates")
                        || Request.Url.AbsolutePath.ToLower().Contains("productselection")
                        || Request.Url.AbsolutePath.ToLower().Contains("settings"))
                    {
                        
                        if (dRow["vchName"].ToString().Equals("Logo Settings")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Logo Settings")
                        {
                            GetPermission(ResourceType.LogoSettings, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='logosetting'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                                str.Append(dRow["vchName"].ToString());
                                str.Append("</a></li>");
                            }
                        }
                        //new start
                        if (dRow["vchName"].ToString().Equals("PTE")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Logo Settings")
                        {
                            GetPermission(ResourceType.PTE, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='Settings'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                                str.Append(dRow["vchName"].ToString());
                                str.Append("</a></li>");
                            }
                        }
                        //new end
                        if (dRow["vchName"].ToString().Equals("Bank Account")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Bank Account")
                        {
                            GetPermission(ResourceType.BankAccount, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='bankaccount'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                                str.Append(dRow["vchName"].ToString());
                                str.Append("</a></li>");
                            }
                        }
                        if (dRow["vchName"].ToString().Equals("Credit Card")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Credit Card")
                        {
                            GetPermission(ResourceType.CreditCard, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='creditcard'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                                str.Append(dRow["vchName"].ToString());
                                str.Append("</a></li>");
                            }
                        }
                        //if (dRow["vchName"].ToString().Equals("Profile")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Profile")
                        //{
                        //    GetPermission(ResourceType.Profile, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                        //    if (canView)
                        //    {
                        //        str.Append(@"<li><a href='profile'");
                        //        str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                        //        str.Append(dRow["vchName"].ToString());
                        //        str.Append("</a></li>");
                        //    }
                        //}
                        if (dRow["vchName"].ToString().Equals("Templates")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Templates")
                        {
                            GetPermission(ResourceType.Templates, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='templates'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                                str.Append(dRow["vchName"].ToString());
                                str.Append("</a></li>");
                            }
                        }

                        if (dRow["vchName"].ToString().Equals("Product Selection")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("ProductSelection")
                        {
                            GetPermission(ResourceType.ProductSelection, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='productselection'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString() + "'>");
                                str.Append(dRow["vchName"].ToString());
                                str.Append("</a></li>");
                            }
                        }

                    }

                    if (Request.Url.AbsolutePath.ToLower().Contains("invoices"))//|| Request.Url.AbsolutePath.ToLower().Contains("AddInvoice"))
                    {
                        if (dRow["vchName"].ToString().Equals("Invoices")) //DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Invoices")
                        {
                            GetPermission(ResourceType.Invoices, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                            if (canView)
                            {
                                str.Append(@"<li><a href='invoices'");
                                str.Append(@"id='link_" + dRow["vchName"].ToString().Equals("Invoices") + "'>");
                                str.Append(dRow["vchName"].ToString().Equals("Invoices"));
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


                        //rttSubMenu.Visible = true;
                    }
                }


            }
            SubMenuText = str.ToString();
            return SubMenuText;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "LeftNavigation.ascx SubMenuLoad", ex);
            return null;
        }
    }

    protected void ItemBound(object sender, RepeaterItemEventArgs args)
    {
        if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hid = (HiddenField)args.Item.FindControl("hidResourceId");
            int ParentId = Convert.ToInt32(hid.Value.ToString());
            Literal ltrSubMenu = (Literal)args.Item.FindControl("ltrMenu");

            StringBuilder str = new StringBuilder(255);
            if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Admin"))
            {

            }
            else
            {
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Home")||(DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Dashboard")))
                {
                    GetPermission(ResourceType.Home, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        //<li> for main menu is started from here
                        if (Request.Url.AbsolutePath.ToLower().Contains("dashboard") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "home")
                            str.Append("<li class='active'>");
                        else
                            str.Append("<li>");

                        str.Append(@"<a href='dashboard' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("'><i class='fa fa-th-large'></i> <span class='nav-label'>");
                        str.Append("Dashboard");
                        //str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</span></a><li>");
                        //main menu ended for Home
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Stakeholders"))
                {
                    GetPermission(ResourceType.Stakeholders, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        //menu for  Stakeholders
                        if (Request.Url.AbsolutePath.ToLower().Contains("stakeholders") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "stakeholders")
                            str.Append("<li class='active' >");
                        else
                            str.Append("<li id='Stakeholdeactive'>");
                        str.Append(@" <a href='Stakeholders' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("'><i class='fa fa-user'></i> <span class='nav-label'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</span></a>"); str.Append("</li>");
                        //Menu ended 
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Applications"))
                {
                    GetPermission(ResourceType.Applications, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {

                        if (Request.Url.AbsolutePath.ToLower().Contains("applications") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "applications")
                            str.Append("<li class='active'>");
                        else
                            str.Append("<li>");
                        str.Append(@" <a href='Applications' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("' ><i class='fa fa-pencil'></i> <span class='nav-label'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</span></a>"); str.Append("</li>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Inventory"))
                {
                    GetPermission(ResourceType.Inventory, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        if ((Request.Url.AbsolutePath.ToLower().Contains("lotinfo") || Request.Url.AbsolutePath.ToLower().Contains("inventory-load") || Request.Url.AbsolutePath.ToLower().Contains("addparkinglot") || Request.Url.AbsolutePath.ToLower().Contains("inventory-tire") || Request.Url.AbsolutePath.ToLower().Contains("addinventory") || Request.Url.AbsolutePath.ToLower().Contains("lots") || Request.Url.AbsolutePath.ToLower().Contains("addload")
                || Request.Url.AbsolutePath.ToLower().Contains("facility") || Request.Url.AbsolutePath.ToLower().Contains("deliverynotes") || Request.Url.AbsolutePath.ToLower().Contains("adddeliverynote") || Request.Url.AbsolutePath.ToLower().Contains("deliveryreceipt")) && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "inventory")
                            str.Append("<li class='active' onclick ='showHideInventory(this)'>");
                        else
                            str.Append("<li onclick ='showHideInventory(this)'>");

                        str.Append(@" <a href='lotinfo' runat='server' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("' ><i class='fa fa-clipboard'></i> <span class='nav-label'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();

                        string SubMenuText = SubMenuLoad(ParentId);

                        if (SubMenuText == null || SubMenuText == "")
                            str.Append("</span><span class='fa arrow'></span></a>");
                        else
                        {
                            str.Append("</span><span class='fa arrow'></span></a>");
                            str.Append("<div id ='second-nav-inventory'><ul class ='nav nav-second-level'>");
                            str.Append(SubMenuText);
                            str.Append("</ul></div>");
                        }

                        str.Append("</li>");


                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Revenue"))
                {
                    GetPermission(ResourceType.Revenue, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {

                        if (Request.Url.AbsolutePath.ToLower().Contains("revenue") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "revenue")
                            str.Append("<li class='active'>");
                        else
                            str.Append("<li>");
                        str.Append(@" <a href='Revenue' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("'><i class='fa fa-line-chart'></i> <span class='nav-label'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</span></a>"); str.Append("</li>");
                    }
                }
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Reports"))
                {
                    GetPermission(ResourceType.Reports, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {

                        if (Request.Url.AbsolutePath.ToLower().Contains("reports") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "reports")
                            str.Append("<li class='active'>");
                        else
                            str.Append("<li>");
                        str.Append(@" <a href='Reports' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());

                        str.Append("'><i class='fa fa-file-text-o'></i> <span class='nav-label'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</span></a>"); str.Append("</li>");
                    }
                }

                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Account Management"))
                {
                    GetPermission(ResourceType.AccountManagement, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {

                        if (Request.Url.AbsolutePath.ToLower().Contains("invoices") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString() == "Account Management")
                            str.Append("<li class='active'>");
                        else
                            str.Append("<li>");
                        str.Append(@" <a href='invoices' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("'><i class='fa fa-lock'></i> <span class='nav-label'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("</span></a>"); str.Append("</li>");
                    }
                }

                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Users"))
                {
                    GetPermission(ResourceType.Users, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        if (Request.Url.AbsolutePath.ToLower().Contains("users") && DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower() == "users")
                            str.Append("<li class='active' id='Uactive'>");
                        else
                            str.Append("<li>");
                        str.Append(@" <a href='Users' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("'><i class='fa fa-users'></i> <span class='nav-label'>");
                        //str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        str.Append("User Management");
                        str.Append("</span></a>");
                        str.Append("</li>");
                    }
                }
                //if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("PTE"))
                //{
                //    GetPermission(ResourceType.PTE, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                //    if (canView)
                //    {

                //        if (Request.Url.AbsolutePath.ToLower().Contains("settings") || DataBinder.Eval(args.Item.DataItem, "vchName").ToString().ToLower().Contains("ptestandard"))
                //            str.Append("<li class='active' onclick ='showHideInventory(this)'>");
                //        else
                //            str.Append("<li onclick ='showHideInventory(this)'>");
                //        str.Append(@" <a href='Settings' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                //        str.Append("'><i class='fa fa-files-o'></i> <span class='nav-label'>");
                //        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();


                //        string SubMenuText = SubMenuLoad(ParentId);
                //        if (SubMenuText == null || SubMenuText == "")
                //            str.Append("</span></a>");
                //        else
                //        {
                //            str.Append("</span><span class='fa arrow'></span></a>");
                //            str.Append("  <div id ='second-nav'><ul class ='nav nav-second-level'>");
                //            str.Append(SubMenuText);
                //            str.Append("</ul></div>");
                //        }
                //    }
                //}
                if (DataBinder.Eval(args.Item.DataItem, "vchName").ToString().Equals("Settings"))
                {
                    GetPermission(ResourceType.Settings, ref canView, ref canAdd, ref canUpdate, ref canDelete);
                    if (canView)
                    {
                        if (Request.Url.AbsolutePath.ToLower().Contains("logosetting")
                                || Request.Url.AbsolutePath.ToLower().Contains("bankaccount")
                                || Request.Url.AbsolutePath.ToLower().Contains("creditcard")
                                //|| Request.Url.AbsolutePath.ToLower().Contains("profile")
                                || Request.Url.AbsolutePath.ToLower().Contains("templates")
                                || Request.Url.AbsolutePath.ToLower().Contains("productselection")
                                || Request.Url.AbsolutePath.ToLower().Contains("settings"))
                            str.Append("<li class='active' onclick ='showHideInventory(this)'>");
                        else
                            str.Append("<li onclick ='showHideInventory(this)'>");

                        str.Append(@" <a href='logosetting' id='link_" + DataBinder.Eval(args.Item.DataItem, "vchName").ToString());
                        str.Append("' onclick = 'showHideSettings()'><i class='fa fa-gear'></i> <span class='nav-label'>");
                        str.Append(DataBinder.Eval(args.Item.DataItem, "vchName")).ToString();
                        string SubMenuText = SubMenuLoad(ParentId);
                        if (SubMenuText == null || SubMenuText == "")
                            str.Append("</span><span class='fa arrow'></span></a>");
                        else
                        {
                            str.Append("</span><span class='fa arrow'></span></a>");
                            str.Append("  <div id ='second-nav-settings'><ul class ='nav nav-second-level'>");
                            str.Append(SubMenuText);
                            str.Append("</ul></div>");
                        }

                        str.Append("</li>");

                    }
                }
               
                ltrSubMenu.Text = ltrSubMenu.Text + str;
            }

        }
        else if (!subMenu && args.Item.ItemType == ListItemType.Footer)
        {
            //Literal litfooter = (Literal)args.Item.FindControl("litfooter");
            //litfooter.Text = "</ul></div></div>";

        }
    }

}