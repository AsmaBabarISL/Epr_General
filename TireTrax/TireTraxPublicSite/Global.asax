<%@ Application Language="C#" %>

<script runat="server">


    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        SiteMap.SiteMapResolve += ResolveCustomNodes;

    }

    private SiteMapNode ResolveCustomNodes(object sender, SiteMapResolveEventArgs e)
    {
        string[] path = e.Context.Request.AppRelativeCurrentExecutionFilePath.Split('/');
        SiteMapNode currentNode=null;
        SiteMapNode tempNode;
        switch (path[path.Length - 1])
        {
            case "dashboard":
                currentNode = e.Provider.FindSiteMapNode("/dashboard").Clone(true);
                break;
            case "dashboardnotfications":
                currentNode = e.Provider.FindSiteMapNode("/dashboardnotfications").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                break; 
            case "Stakeholders":
                currentNode = e.Provider.FindSiteMapNode("/stakeholders").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                break;
            case "ViewStakeholderDetail":
                currentNode = e.Provider.FindSiteMapNode("/ViewStakeholderDetail").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                break;

            case "Applications":
                currentNode = e.Provider.FindSiteMapNode("/Applications").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                break;

            case "Inventory":
                currentNode = e.Provider.FindSiteMapNode("/Inventory").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
              
                break;
         
                
            case "Revenue":
                currentNode = e.Provider.FindSiteMapNode("/Revenue").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;

            case "Reports":
                currentNode = e.Provider.FindSiteMapNode("/Reports").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
              
                break;

            case "Users":
                currentNode = e.Provider.FindSiteMapNode("/Users").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;

            case "editUser":
                currentNode = e.Provider.FindSiteMapNode("/editUser").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
              
                break;

            case "Settings":
                currentNode = e.Provider.FindSiteMapNode("/Settings").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
              
                break;

            case "PTEStandard":
                currentNode = e.Provider.FindSiteMapNode("/PTEStandard").Clone(true); 
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/Settings";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                
                break;
            
            case "InventoryTimeLine":

                currentNode = e.Provider.FindSiteMapNode("/InventoryTimeLine").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;

            case "inventory-tire":

                currentNode = e.Provider.FindSiteMapNode("/inventory-tire").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;

            case "inventory-load":

                currentNode = e.Provider.FindSiteMapNode("/inventory-load").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
              
                break;

            case "InventoryRevenue":
                currentNode = e.Provider.FindSiteMapNode("/InventoryRevenue").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
              
                break;


            case "logosetting":
                currentNode = e.Provider.FindSiteMapNode("/logosetting").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;
            case "bankaccount":
                currentNode = e.Provider.FindSiteMapNode("/bankaccount").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/logosetting";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;

            case "invoices":
                currentNode = e.Provider.FindSiteMapNode("/invoices").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                break;
            case "addinvoice":
                currentNode = e.Provider.FindSiteMapNode("/AddInvoice").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/invoices";
                tempNode.ParentNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";

                break; 
            case "creditcard":
                currentNode = e.Provider.FindSiteMapNode("/creditcard").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/logosetting";
                tempNode.ParentNode.Url= "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
              
                break;

            case "addbankaccount":
                currentNode = e.Provider.FindSiteMapNode("/addBankAccount").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/bankaccount";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/logosetting";
                tempNode.ParentNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                
                break; 
            case "editBankAccount":
                currentNode = e.Provider.FindSiteMapNode("/editBankAccount").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/bankaccount";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/logosetting";
                tempNode.ParentNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";

               
                break;

            case "addcreditcard":
                currentNode = e.Provider.FindSiteMapNode("/addcreditcard").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/creditcard";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/logosetting";
                tempNode.ParentNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";

                
                break;

            //case "editCreditCard":
            //    currentNode = e.Provider.FindSiteMapNode("/editCreditCard").Clone(true);
            //    tempNode = currentNode.ParentNode;
            //    tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/creditcard";
            //    tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
            //    return currentNode;
            //    break;

            case "addInventory":
                currentNode = e.Provider.FindSiteMapNode("/addInventory").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;

            case "profile":
                //currentNode = e.Provider.FindSiteMapNode("/profile").Clone(true);
                //tempNode = currentNode.ParentNode;
                //tempNode.Url = "";
                //tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";

                currentNode = e.Provider.FindSiteMapNode("/profile").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                
                break;
           

            case "addload":
                currentNode = e.Provider.FindSiteMapNode("/addload").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/inventory-load";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";

                
                break;


            case "editload":
                currentNode = e.Provider.FindSiteMapNode("/editload").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/inventory-load";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";

                
                break;
            case "lotinfo":
                currentNode = e.Provider.FindSiteMapNode("/lotinfo").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                
                break;

            case "lots":
                currentNode = e.Provider.FindSiteMapNode("/lots").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/facility";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                
                break;

            case "addparkinglot":
                currentNode = e.Provider.FindSiteMapNode("/addparkinglot").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lots";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/facility";
                tempNode.ParentNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.ParentNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;

            case "facility":
                currentNode = e.Provider.FindSiteMapNode("/facility").Clone(true);
                tempNode = currentNode.ParentNode;

                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                
                break;
                
            case "DetailRevenue":
                currentNode = e.Provider.FindSiteMapNode("/Revenue").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;
            case "deliverynotes":
                currentNode = e.Provider.FindSiteMapNode("/deliverynotes").Clone(true);
                tempNode = currentNode.ParentNode;

                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;
            case "adddeliverynote":
                currentNode = e.Provider.FindSiteMapNode("/AddDeliveryNote").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/deliverynotes";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;
            case "deliveryreceipt":
                currentNode = e.Provider.FindSiteMapNode("/deliveryReceipt").Clone(true);
                tempNode = currentNode.ParentNode;

                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/lotinfo";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
               
                break;

            case "templates":
                currentNode = e.Provider.FindSiteMapNode("/templates").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/logosetting";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";

                break;

            case "addtemplate":
                currentNode = e.Provider.FindSiteMapNode("/addtemplate").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/templates";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/logosetting";
                tempNode.ParentNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                break;
            case "productselection":
                currentNode = e.Provider.FindSiteMapNode("/productselection").Clone(true);
                tempNode = currentNode.ParentNode;
                tempNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/logosetting";
                tempNode.ParentNode.Url = "/" + path[path.Length - 3] + "/" + path[path.Length - 2] + "/dashboard";
                break;
           

        }
        return currentNode;
        
         // use default implementation;
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    public static void RegisterRoutes(System.Web.Routing.RouteCollection rc)
    {
        rc.MapPageRoute("Registration-page", "Registration/", "~/Default.aspx");

        rc.MapPageRoute("US-Registration-page", "us/registration/", "~/Default.aspx");
        rc.MapPageRoute("CA-Registration-page", "ca/registration/", "~/Default.aspx");
        rc.MapPageRoute("CA-en-Registration-page", "ca/registration/en/", "~/Default.aspx");
        rc.MapPageRoute("CA-fr-Registration-page", "ca/registration/fr/", "~/Default.aspx");
        rc.MapPageRoute("MX-Registration-page", "mx/registration/", "~/Default.aspx");
        rc.MapPageRoute("JA-Registration-page", "ja/registration/", "~/Default.aspx");
        rc.MapPageRoute("SK-Registration-page", "sk/registration/", "~/Default.aspx");
        rc.MapPageRoute("AU-Registration-page", "au/registration/", "~/Default.aspx");
        rc.MapPageRoute("CN-Registration-page", "cn/registration/", "~/Default.aspx");

        rc.MapPageRoute("US-Registration-page-1", "us/", "~/Default.aspx");
        rc.MapPageRoute("CA-Registration-page-1", "ca/", "~/Default.aspx");
        rc.MapPageRoute("CA-en-Registration-page-1", "ca/en/", "~/Default.aspx");
        rc.MapPageRoute("CA-fr-Registration-page-1", "ca/fr/", "~/Default.aspx");
        rc.MapPageRoute("MX-Registration-page-1", "mx/", "~/Default.aspx");
        rc.MapPageRoute("JA-Registration-page-1", "ja/", "~/Default.aspx");
        rc.MapPageRoute("SK-Registration-page-1", "sk/", "~/Default.aspx");
        rc.MapPageRoute("AU-Registration-page-1", "au/", "~/Default.aspx");
        rc.MapPageRoute("CN-Registration-page-1", "cn/", "~/Default.aspx");

        rc.MapPageRoute("US-RegistrationForm-page", "us/registrationform/", "~/Registration/RegistrationFormUS.aspx");
        rc.MapPageRoute("CA-RegistrationForm-page", "ca/registrationform/", "~/Registration/RegistrationFormCA.aspx");
        rc.MapPageRoute("CA-en-RegistrationForm-page", "ca/registrationform/en/", "~/Registration/RegistrationFormCA.aspx");
        rc.MapPageRoute("CA-fr-RegistrationForm-page", "ca/registrationform/fr/", "~/Registration/RegistrationFormCA.aspx");
        rc.MapPageRoute("MX-RegistrationForm-page", "mx/registrationform/", "~/Registration/RegistrationFormMX.aspx");
        rc.MapPageRoute("JA-RegistrationForm-page", "ja/registrationform/", "~/Registration/RegistrationFormMX.aspx");
        rc.MapPageRoute("SK-RegistrationForm-page", "sk/registrationform/", "~/Registration/RegistrationFormMX.aspx");
        rc.MapPageRoute("AU-RegistrationForm-page", "au/registrationform/", "~/Registration/RegistrationFormMX.aspx");
        rc.MapPageRoute("CN-RegistrationForm-page", "cn/registrationform/", "~/Registration/RegistrationFormMX.aspx");

        rc.MapPageRoute("US-Default", "us/login", "~/Login/login.aspx");
        rc.MapPageRoute("CA-Default", "ca/login", "~/Login/login.aspx");
        rc.MapPageRoute("MX-Default", "mx/login", "~/Login/login.aspx");
        rc.MapPageRoute("JA-Default", "ja/login", "~/Login/login.aspx");
        rc.MapPageRoute("SK-Default", "sk/login", "~/Login/login.aspx");
        rc.MapPageRoute("AU-Default", "au/login", "~/Login/login.aspx");
        rc.MapPageRoute("CN-Default", "cn/login", "~/Login/login.aspx");

        rc.MapPageRoute("US-forgotPassword", "us/forgotpassword", "~/Login/forgotPassword.aspx");
        rc.MapPageRoute("CA-forgotPassword", "ca/forgotpassword", "~/Login/forgotPassword.aspx");
        rc.MapPageRoute("MX-forgotPassword", "mx/forgotpassword", "~/Login/forgotPassword.aspx");
        rc.MapPageRoute("JA-forgotPassword", "ja/forgotpassword", "~/Login/forgotPassword.aspx");
        rc.MapPageRoute("SK-forgotPassword", "sk/forgotpassword", "~/Login/forgotPassword.aspx");
        rc.MapPageRoute("AU-forgotPassword", "au/forgotpassword", "~/Login/forgotPassword.aspx");
        rc.MapPageRoute("CN-forgotPassword", "cn/forgotpassword", "~/Login/forgotPassword.aspx");

        //rc.MapPageRoute("US-logosetting", "us/logosetting", "~/logo-setting.aspx");
        //rc.MapPageRoute("CA-logosetting", "ca/logosetting", "~/logo-setting.aspx");
        //rc.MapPageRoute("MX-logosetting", "mx/logosetting", "~/logo-setting.aspx");
        //rc.MapPageRoute("JA-logosetting", "ja/logosetting", "~/logo-setting.aspx");
        //rc.MapPageRoute("SK-logosetting", "sk/logosetting", "~/logo-setting.aspx");
        //rc.MapPageRoute("AU-logosetting", "au/logosetting", "~/logo-setting.aspx");
        //rc.MapPageRoute("CN-logosetting", "cn/logosetting", "~/logo-setting.aspx");

        rc.MapPageRoute("US-errorPage", "pagenotfound", "~/errorPage.aspx");

        System.Data.DataSet ds = TireTraxLib.OrganizationInfo.GetAllStewardships(); //(14,39,49,116,124,159,235) -- Australia,Canada,China,Japan,South Korea,Mexico,United States
        System.Data.DataRow[] dr;

        #region US URLs

        dr = ds.Tables[0].Select("CountryId=" + 235);
        foreach (System.Data.DataRow d in dr)
        {
            rc.MapPageRoute(String.Format("US-Registration-page-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/registration/", Convert.ToString(d["State"]).ToLower()), "~/Registration/RegistrationFormUS.aspx");
            rc.MapPageRoute(String.Format("US-Dashboard-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/dashboard", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/Dashboard.aspx");
            rc.MapPageRoute(String.Format("US-dashboardNotfications-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/dashboardnotfications", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/dashboardNotifications.aspx");

            rc.MapPageRoute(String.Format("US-About-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/about", Convert.ToString(d["State"]).ToLower()), "~/About.aspx");
            rc.MapPageRoute(String.Format("US-addInventory-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/addinventory", Convert.ToString(d["State"]).ToLower()), "~/Lots/AddInventory.aspx");
            rc.MapPageRoute(String.Format("US-Alerts-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/alerts", Convert.ToString(d["State"]).ToLower()), "~/Alerts.aspx");
            rc.MapPageRoute(String.Format("US-Applications-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/applications", Convert.ToString(d["State"]).ToLower()), "~/Application/ViewApplications.aspx");
            rc.MapPageRoute(String.Format("US-ViewStakeholderDetail-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/ViewStakeholderDetail", Convert.ToString(d["State"]).ToLower()), "~/Application/viewStakeholder.aspx");

            rc.MapPageRoute(String.Format("US-Default-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/login", Convert.ToString(d["State"]).ToLower()), "~/Login/login.aspx");

            rc.MapPageRoute(String.Format("US-forgotPassword-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/forgotpassword", Convert.ToString(d["State"]).ToLower()), "~/Login/forgotPassword.aspx");
            rc.MapPageRoute(String.Format("US-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/inventory", Convert.ToString(d["State"]).ToLower()), "~/inventory.aspx");
            rc.MapPageRoute(String.Format("US-InvenotryRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/inventoryrevenue", Convert.ToString(d["State"]).ToLower()), "~/InventoryRevenue.aspx");
            rc.MapPageRoute(String.Format("US-InvenotryTimeLine-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/inventorytimeline", Convert.ToString(d["State"]).ToLower()), "~/InventoryTimelLine.aspx");
            rc.MapPageRoute(String.Format("US-Logout-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/logout", Convert.ToString(d["State"]).ToLower()), "~/Logout/Logout.aspx");

            rc.MapPageRoute(String.Format("US-PrintBarCode-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/printbarcode", Convert.ToString(d["State"]).ToLower()), "~/PrintBarCode.aspx");
            rc.MapPageRoute(String.Format("US-Reports-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/reports", Convert.ToString(d["State"]).ToLower()), "~/Reports/ViewReports.aspx");
            rc.MapPageRoute(String.Format("US-Revenue-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/revenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/ViewRevenue.aspx");
            rc.MapPageRoute(String.Format("US-DetailRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/detailrevenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/DetailRevenue.aspx");
            rc.MapPageRoute(String.Format("US-Settings-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/settings", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTESettings.aspx");
            rc.MapPageRoute(String.Format("US-PTEStandard-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/ptestandard", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTEStandards.aspx");
            rc.MapPageRoute(String.Format("US-Stakeholders-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/stakeholders", Convert.ToString(d["State"]).ToLower()), "~/Stakeholder/ViewStakeholder.aspx");
            rc.MapPageRoute(String.Format("US-Users-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/users", Convert.ToString(d["State"]).ToLower()), "~/Users/ViewUsers.aspx");
            rc.MapPageRoute(String.Format("US-viewStakeholder-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/viewstakeholder", Convert.ToString(d["State"]).ToLower()), "~/viewStakeholder.aspx");
            rc.MapPageRoute(String.Format("US-viewStewardship-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/viewstewardship", Convert.ToString(d["State"]).ToLower()), "~/viewStewardship.aspx");
            rc.MapPageRoute(String.Format("US-Welcome-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/welcome", Convert.ToString(d["State"]).ToLower()), "~/Welcome.aspx");
            rc.MapPageRoute(String.Format("US-logosetting-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/logosetting", Convert.ToString(d["State"]).ToLower()), "~/LogoSetting/UploadOrgLogo.aspx");
            rc.MapPageRoute(String.Format("US-bankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/bankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/ViewBankAccount.aspx");
            rc.MapPageRoute(String.Format("US-editbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/editbankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/EditBankAccount.aspx");
            rc.MapPageRoute(String.Format("US-addbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/addbankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/AddBankAccount.aspx");
            rc.MapPageRoute(String.Format("US-creditcard-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/creditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/ViewCreditCard.aspx");
            //rc.MapPageRoute(String.Format("US-editcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/editcreditcard", Convert.ToString(d["State"]).ToLower()), "~/CreditCard/EditCreditCard.aspx");

            rc.MapPageRoute(String.Format("US-addcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/addcreditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/AddCreditCard.aspx");
            rc.MapPageRoute(String.Format("US-profile-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/profile", Convert.ToString(d["State"]).ToLower()), "~/ProfileSetting/ProfileSetting.aspx");
            rc.MapPageRoute(String.Format("US-lot-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/lot-inventory", Convert.ToString(d["State"]).ToLower()), "~/lot-inventory.aspx");
            rc.MapPageRoute(String.Format("US-lotinfo-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/lotinfo", Convert.ToString(d["State"]).ToLower()), "~/Lots/ViewLots.aspx");
            //rc.MapPageRoute(String.Format("US-createload-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/createload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            //rc.MapPageRoute(String.Format("US-createload-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/createload", Convert.ToString(d["State"]).ToLower()), "~/Load/CreateLoad.aspx");
            rc.MapPageRoute(String.Format("US-inventory-tire-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/inventory-tire", Convert.ToString(d["State"]).ToLower()), "~/Lots/EditTire.aspx");
            rc.MapPageRoute(String.Format("US-addload-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/addload", Convert.ToString(d["State"]).ToLower()), "~/Load/CreateLoad.aspx");
            rc.MapPageRoute(String.Format("US-editload-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/editload", Convert.ToString(d["State"]).ToLower()), "~/Load/EditLoad.aspx");
            rc.MapPageRoute(String.Format("US-editUser-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/edituser", Convert.ToString(d["State"]).ToLower()), "~/Users/AddUser.aspx");
            rc.MapPageRoute(String.Format("US-lots-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/lots", Convert.ToString(d["State"]).ToLower()), "~/Facility/DetailFacility.aspx");
            rc.MapPageRoute(String.Format("US-inventory-load-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/inventory-load", Convert.ToString(d["State"]).ToLower()), "~/Load/ViewLoad.aspx");

            rc.MapPageRoute(String.Format("US-addparkinglot-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/addparkinglot", Convert.ToString(d["State"]).ToLower()), "~/Facility/AddLot.aspx");
            rc.MapPageRoute(String.Format("US-facility-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/facility", Convert.ToString(d["State"]).ToLower()), "~/Facility/ViewFacility.aspx");
            rc.MapPageRoute(String.Format("US-AccessDenied-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/error", Convert.ToString(d["State"]).ToLower()), "~/AccessDenied.aspx");

            rc.MapPageRoute(String.Format("US-deliverynotes-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/deliverynotes", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/ViewDeliveryNotes.aspx");
            rc.MapPageRoute(String.Format("US-adddeliverynote-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/adddeliverynote", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/AddDeliveryNote.aspx");

            rc.MapPageRoute(String.Format("US-deliveryreceipt-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/deliveryreceipt", Convert.ToString(d["State"]).ToLower()), "~/DeliveryReceipt/ViewDeliveryReceipt.aspx");

            rc.MapPageRoute(String.Format("US-templates-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/templates", Convert.ToString(d["State"]).ToLower()), "~/Templates/ViewTemplates.aspx");
            rc.MapPageRoute(String.Format("US-addtemplate-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/addtemplate", Convert.ToString(d["State"]).ToLower()), "~/Templates/AddTemplate.aspx");

            rc.MapPageRoute(String.Format("US-invoices-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/invoices", Convert.ToString(d["State"]).ToLower()), "~/Invoices/ViewInvoice.aspx");
            rc.MapPageRoute(String.Format("US-addInvoice-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/AddInvoice", Convert.ToString(d["State"]).ToLower()), "~/Invoices/AddInvoice.aspx");

            rc.MapPageRoute(String.Format("US-product-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/productselection", Convert.ToString(d["State"]).ToLower()), "~/Product/ProductSelection.aspx");

        }

        #endregion

        #region CA URLs

        dr = ds.Tables[0].Select("CountryId=" + 39);
        foreach (System.Data.DataRow d in dr)
        {
            rc.MapPageRoute(String.Format("CA-Registration-page-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/registration/", Convert.ToString(d["State"]).ToLower()), "~/Registration/RegistrationFormCA.aspx");
            rc.MapPageRoute(String.Format("CA-Dashboard-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/dashboard", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/Dashboard.aspx");
            rc.MapPageRoute(String.Format("CA-dashboardNotifications-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/dashboardnotfications", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/dashboardNotifications.aspx");

            rc.MapPageRoute(String.Format("CA-About-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/about", Convert.ToString(d["State"]).ToLower()), "~/About.aspx");
            rc.MapPageRoute(String.Format("CA-addInventory-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/addinventory", Convert.ToString(d["State"]).ToLower()), "~/Lots/AddInventory.aspx");
            rc.MapPageRoute(String.Format("CA-Alerts-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/alerts", Convert.ToString(d["State"]).ToLower()), "~/Alerts.aspx");
            rc.MapPageRoute(String.Format("CA-Applications-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/applications", Convert.ToString(d["State"]).ToLower()), "~/Application/ViewApplications.aspx");
            rc.MapPageRoute(String.Format("CA-ViewStakeholderDetail-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/ViewStakeholderDetail", Convert.ToString(d["State"]).ToLower()), "~/Application/viewStakeholder.aspx");
            
            rc.MapPageRoute(String.Format("CA-Default-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/login", Convert.ToString(d["State"]).ToLower()), "~/Login/login.aspx");
            rc.MapPageRoute(String.Format("CA-forgotPassword-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/forgotpassword", Convert.ToString(d["State"]).ToLower()), "~/Login/forgotPassword.aspx");
            rc.MapPageRoute(String.Format("CA-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/inventory", Convert.ToString(d["State"]).ToLower()), "~/inventory.aspx");
            rc.MapPageRoute(String.Format("CA-InventoryRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/inventoryrevenue", Convert.ToString(d["State"]).ToLower()), "~/InventoryRevenue.aspx");
            rc.MapPageRoute(String.Format("CA-InvenotryTimeLine-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/inventorytimeline", Convert.ToString(d["State"]).ToLower()), "~/InventoryTimelLine.aspx");
            rc.MapPageRoute(String.Format("CA-Logout-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/logout", Convert.ToString(d["State"]).ToLower()), "~/Logout/Logout.aspx");
            rc.MapPageRoute(String.Format("CA-PTEStandard-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/ptestandard", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTEStandards.aspx");
            rc.MapPageRoute(String.Format("CA-PrintBarCode-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/printbarcode", Convert.ToString(d["State"]).ToLower()), "~/PrintBarCode.aspx");
            rc.MapPageRoute(String.Format("CA-Reports-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/reports", Convert.ToString(d["State"]).ToLower()), "~/Reports/ViewReports.aspx");
            rc.MapPageRoute(String.Format("CA-Revenue-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/revenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/ViewRevenue.aspx");
            rc.MapPageRoute(String.Format("CA-DetailRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/detailrevenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/DetailRevenue.aspx");
            rc.MapPageRoute(String.Format("CA-Settings-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/settings", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTESettings.aspx");
           
            rc.MapPageRoute(String.Format("CA-Stakeholders-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/stakeholders", Convert.ToString(d["State"]).ToLower()), "~/Stakeholder/ViewStakeholder.aspx");
            rc.MapPageRoute(String.Format("CA-Users-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/users", Convert.ToString(d["State"]).ToLower()), "~/Users/ViewUsers.aspx");
            rc.MapPageRoute(String.Format("CA-viewStakeholder-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/viewstakeholder", Convert.ToString(d["State"]).ToLower()), "~/viewStakeholder.aspx");
            rc.MapPageRoute(String.Format("CA-viewStewardship-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/viewstewardship", Convert.ToString(d["State"]).ToLower()), "~/viewStewardship.aspx");
            rc.MapPageRoute(String.Format("CA-Welcome-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/welcome", Convert.ToString(d["State"]).ToLower()), "~/Welcome.aspx");
            rc.MapPageRoute(String.Format("CA-logosetting-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/logosetting", Convert.ToString(d["State"]).ToLower()), "~/LogoSetting/UploadOrgLogo.aspx");
            rc.MapPageRoute(String.Format("CA-bankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/bankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/ViewBankAccount.aspx");
            rc.MapPageRoute(String.Format("CA-editbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/editbankaccount", Convert.ToString(d["State"]).ToLower()), "~/editBankAccount.aspx");
            rc.MapPageRoute(String.Format("CA-addbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/addbankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/AddBankAccount.aspx");
            rc.MapPageRoute(String.Format("CA-creditcard-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/creditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/ViewCreditCard.aspx");
            //rc.MapPageRoute(String.Format("CA-editcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/editcreditcard", Convert.ToString(d["State"]).ToLower()), "~/editCreditCard.aspx");
            rc.MapPageRoute(String.Format("CA-addcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/addcreditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/AddCreditCard.aspx");
            rc.MapPageRoute(String.Format("CA-profile-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/profile", Convert.ToString(d["State"]).ToLower()), "~/ProfileSetting/ProfileSetting.aspx");
            rc.MapPageRoute(String.Format("CA-lot-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/lot-inventory", Convert.ToString(d["State"]).ToLower()), "~/lot-inventory.aspx");
            rc.MapPageRoute(String.Format("CA-lotinfo-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/lotinfo", Convert.ToString(d["State"]).ToLower()), "~/Lots/ViewLots.aspx");
            rc.MapPageRoute(String.Format("CA-createload-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/createload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            rc.MapPageRoute(String.Format("CA-editload-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/editload", Convert.ToString(d["State"]).ToLower()), "~/Load/EditLoad.aspx");
            rc.MapPageRoute(String.Format("CA-inventory-tire-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/inventory-tire", Convert.ToString(d["State"]).ToLower()), "~/Lots/EditTire.aspx");
            rc.MapPageRoute(String.Format("CA-addload-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/addload", Convert.ToString(d["State"]).ToLower()), "~/Load/CreateLoad.aspx");
            rc.MapPageRoute(String.Format("CA-editUser-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/edituser", Convert.ToString(d["State"]).ToLower()), "~/Users/AddUser.aspx");

            rc.MapPageRoute(String.Format("CA-inventory-load-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/inventory-load", Convert.ToString(d["State"]).ToLower()), "~/Load/ViewLoad.aspx");

            rc.MapPageRoute(String.Format("CA-facility-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/facility", Convert.ToString(d["State"]).ToLower()), "~/Facility/ViewFacility.aspx");
            rc.MapPageRoute(String.Format("CA-lots-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/lots", Convert.ToString(d["State"]).ToLower()), "~/Facility/DetailFacility.aspx");
            rc.MapPageRoute(String.Format("CA-addparkinglot-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/addparkinglot", Convert.ToString(d["State"]).ToLower()), "~/Facility/AddLot.aspx");
            //rc.MapPageRoute(String.Format("CA-inventory-load-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/inventory-load", Convert.ToString(d["State"]).ToLower()), "~/Load/ViewLoad.aspx");
            rc.MapPageRoute(String.Format("CA-deliverynotes-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/deliverynotes", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/ViewDeliveryNotes.aspx");
            rc.MapPageRoute(String.Format("CA-adddeliverynote-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/adddeliverynote", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/AddDeliveryNote.aspx");
            rc.MapPageRoute(String.Format("CA-deliveryreceipt-{0}", Convert.ToString(d["StateName"])), String.Format("ca/{0}/deliveryreceipt", Convert.ToString(d["State"]).ToLower()), "~/DeliveryReceipt/ViewDeliveryReceipt.aspx");
            rc.MapPageRoute(String.Format("CA-templates-{0}", Convert.ToString(d["StateName"])), String.Format("CA/{0}/templates", Convert.ToString(d["State"]).ToLower()), "~/Templates/ViewTemplates.aspx");
            rc.MapPageRoute(String.Format("CA-addtemplate-{0}", Convert.ToString(d["StateName"])), String.Format("CA/{0}/addtemplate", Convert.ToString(d["State"]).ToLower()), "~/Templates/AddTemplate.aspx");

            rc.MapPageRoute(String.Format("CA-invoices-{0}", Convert.ToString(d["StateName"])), String.Format("CA/{0}/invoices", Convert.ToString(d["State"]).ToLower()), "~/Invoices/ViewInvoice.aspx");
            rc.MapPageRoute(String.Format("CA-AddInvoice-{0}", Convert.ToString(d["StateName"])), String.Format("CA/{0}/AddInvoice", Convert.ToString(d["State"]).ToLower()), "~/Invoices/AddInvoice.aspx");

            rc.MapPageRoute(String.Format("US-product-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/productselection", Convert.ToString(d["State"]).ToLower()), "~/Product/ProductSelection.aspx");

        }

        #endregion

        #region MX URLs

        dr = ds.Tables[0].Select("CountryId=" + 159);
        foreach (System.Data.DataRow d in dr)
        {
            rc.MapPageRoute(String.Format("MX-Registration-page-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/registration/", Convert.ToString(d["State"]).ToLower()), "~/Registration/RegistrationFormMX.aspx");
            rc.MapPageRoute(String.Format("MX-Dashboard-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/dashboard", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/Dashboard.aspx");
            rc.MapPageRoute(String.Format("MX-dashboardNotifications-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/dashboardnotfications", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/dashboardNotifications.aspx");

            rc.MapPageRoute(String.Format("MX-About-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/about", Convert.ToString(d["State"]).ToLower()), "~/About.aspx");
            rc.MapPageRoute(String.Format("MX-addInventory-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/addinventory", Convert.ToString(d["State"]).ToLower()), "~/Lots/AddInventory.aspx");
            rc.MapPageRoute(String.Format("MX-Alerts-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/alerts", Convert.ToString(d["State"]).ToLower()), "~/Alerts.aspx");
            rc.MapPageRoute(String.Format("MX-Applications-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/applications", Convert.ToString(d["State"]).ToLower()), "~/Application/ViewApplications.aspx");
            rc.MapPageRoute(String.Format("MX-ViewStakeholderDetail-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/ViewStakeholderDetail", Convert.ToString(d["State"]).ToLower()), "~/Application/viewStakeholder.aspx");
            
            rc.MapPageRoute(String.Format("MX-Default-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/login", Convert.ToString(d["State"]).ToLower()), "~/Login/login.aspx");
            rc.MapPageRoute(String.Format("MX-forgotPassword-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/forgotpassword", Convert.ToString(d["State"]).ToLower()), "~/Login/forgotPassword.aspx");
            rc.MapPageRoute(String.Format("MX-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/inventory", Convert.ToString(d["State"]).ToLower()), "~/inventory.aspx");
            rc.MapPageRoute(String.Format("MX-InventoryRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/inventoryrevenue", Convert.ToString(d["State"]).ToLower()), "~/InventoryRevenue.aspx");
            rc.MapPageRoute(String.Format("MX-InvenotryTimeLine-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/inventorytimeline", Convert.ToString(d["State"]).ToLower()), "~/InventoryTimelLine.aspx");
            rc.MapPageRoute(String.Format("MX-Logout-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/logout", Convert.ToString(d["State"]).ToLower()), "~/Logout/Logout.aspx");

            rc.MapPageRoute(String.Format("MX-PrintBarCode-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/printbarcode", Convert.ToString(d["State"]).ToLower()), "~/PrintBarCode.aspx");
            rc.MapPageRoute(String.Format("MX-Reports-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/reports", Convert.ToString(d["State"]).ToLower()), "~/Reports/ViewReports.aspx");
            rc.MapPageRoute(String.Format("MX-Revenue-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/revenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/ViewRevenue.aspx");
            rc.MapPageRoute(String.Format("MX-DetailRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/detailrevenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/DetailRevenue.aspx");
            rc.MapPageRoute(String.Format("MX-Settings-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/settings", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTESettings.aspx");
            rc.MapPageRoute(String.Format("MX-PTEStandard-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/ptestandard", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTEStandards.aspx");
            rc.MapPageRoute(String.Format("MX-Stakeholders-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/stakeholders", Convert.ToString(d["State"]).ToLower()), "~/Stakeholder/ViewStakeholder.aspx");
            rc.MapPageRoute(String.Format("MX-Users-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/users", Convert.ToString(d["State"]).ToLower()), "~/Users/ViewUsers.aspx");
            rc.MapPageRoute(String.Format("MX-viewStakeholder-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/viewstakeholder", Convert.ToString(d["State"]).ToLower()), "~/viewStakeholder.aspx");
            rc.MapPageRoute(String.Format("MX-viewStewardship-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/viewstewardship", Convert.ToString(d["State"]).ToLower()), "~/viewStewardship.aspx");
            rc.MapPageRoute(String.Format("MX-Welcome-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/welcome", Convert.ToString(d["State"]).ToLower()), "~/Welcome.aspx");
            rc.MapPageRoute(String.Format("MX-logosetting-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/logosetting", Convert.ToString(d["State"]).ToLower()), "~/LogoSetting/UploadOrgLogo.aspx");
            rc.MapPageRoute(String.Format("MX-bankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/bankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/ViewBankAccount.aspx");
            rc.MapPageRoute(String.Format("MX-editbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/editbankaccount", Convert.ToString(d["State"]).ToLower()), "~/editBankAccount.aspx");
            rc.MapPageRoute(String.Format("MX-addbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/addbankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/AddBankAccount.aspx");
            rc.MapPageRoute(String.Format("MX-creditcard-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/creditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/ViewCreditCard.aspx");
            //rc.MapPageRoute(String.Format("MX-editcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/editcreditcard", Convert.ToString(d["State"]).ToLower()), "~/editCreditCard.aspx");
            rc.MapPageRoute(String.Format("MX-addcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/addcreditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/AddCreditCard.aspx");
            rc.MapPageRoute(String.Format("MX-profile-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/profile", Convert.ToString(d["State"]).ToLower()), "~/ProfileSetting/ProfileSetting.aspx");
            rc.MapPageRoute(String.Format("MX-lot-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/lot-inventory", Convert.ToString(d["State"]).ToLower()), "~/lot-inventory.aspx");
            rc.MapPageRoute(String.Format("MX-lotinfo-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/lotinfo", Convert.ToString(d["State"]).ToLower()), "~/Lots/ViewLots.aspx");
            rc.MapPageRoute(String.Format("MX-createload-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/createload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            rc.MapPageRoute(String.Format("MX-editload-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/editload", Convert.ToString(d["State"]).ToLower()), "~/Load/EditLoad.aspx");
            rc.MapPageRoute(String.Format("MX-inventory-tire-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/inventory-tire", Convert.ToString(d["State"]).ToLower()), "~/Lots/EditTire.aspx");
            rc.MapPageRoute(String.Format("MX-addload-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/addload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            rc.MapPageRoute(String.Format("MX-editUser-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/edituser", Convert.ToString(d["State"]).ToLower()), "~/edituser.aspx");
            rc.MapPageRoute(String.Format("MX-inventory-load-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/inventory-load", Convert.ToString(d["State"]).ToLower()), "~/inventory-load.aspx");
            rc.MapPageRoute(String.Format("MX-facility-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/facility", Convert.ToString(d["State"]).ToLower()), "~/AddFacility.aspx");
            rc.MapPageRoute(String.Format("MX-AccessDenied-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/error", Convert.ToString(d["State"]).ToLower()), "~/AccessDenied.aspx");
            rc.MapPageRoute(String.Format("MX-deliverynotes-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/deliverynotes", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/ViewDeliveryNotes.aspx");
            rc.MapPageRoute(String.Format("MX-adddeliverynote-{0}", Convert.ToString(d["StateName"])), String.Format("mx/{0}/adddeliverynote", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/AddDeliveryNote.aspx");
            rc.MapPageRoute(String.Format("MX-deliveryreceipt-{0}", Convert.ToString(d["StateName"])), String.Format("MX/{0}/deliveryreceipt", Convert.ToString(d["State"]).ToLower()), "~/DeliveryReceipt/ViewDeliveryReceipt.aspx");

            rc.MapPageRoute(String.Format("MX-templates-{0}", Convert.ToString(d["StateName"])), String.Format("MX/{0}/templates", Convert.ToString(d["State"]).ToLower()), "~/Templates/ViewTemplates.aspx");
            rc.MapPageRoute(String.Format("MX-addtemplate-{0}", Convert.ToString(d["StateName"])), String.Format("MX/{0}/addtemplate", Convert.ToString(d["State"]).ToLower()), "~/Templates/AddTemplate.aspx");

            rc.MapPageRoute(String.Format("MX-invoices-{0}", Convert.ToString(d["StateName"])), String.Format("MX/{0}/invoices", Convert.ToString(d["State"]).ToLower()), "~/Invoices/ViewInvoice.aspx");
            rc.MapPageRoute(String.Format("MX-AddInvoice-{0}", Convert.ToString(d["StateName"])), String.Format("MX/{0}/AddInvoice", Convert.ToString(d["State"]).ToLower()), "~/Invoices/AddInvoice.aspx");

            rc.MapPageRoute(String.Format("US-product-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/productselection", Convert.ToString(d["State"]).ToLower()), "~/Product/ProductSelection.aspx");

        }

        #endregion

        #region JA URLs

        dr = ds.Tables[0].Select("CountryId=" + 116);
        foreach (System.Data.DataRow d in dr)
        {
            rc.MapPageRoute(String.Format("JA-RegistrationForm-page-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/registration/", Convert.ToString(d["State"]).ToLower()), "~/Registration/RegistrationFormMX.aspx");
            rc.MapPageRoute(String.Format("JA-Dashboard-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/dashboard", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/Dashboard.aspx");
            rc.MapPageRoute(String.Format("JA-dashboardNotifications-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/dashboardnotfications", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/dashboardNotifications.aspx");

            rc.MapPageRoute(String.Format("JA-About-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/about", Convert.ToString(d["State"]).ToLower()), "~/About.aspx");
            rc.MapPageRoute(String.Format("JA-addInventory-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/addinventory", Convert.ToString(d["State"]).ToLower()), "~/Lots/AddInventory.aspx");
            rc.MapPageRoute(String.Format("JA-Alerts-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/alerts", Convert.ToString(d["State"]).ToLower()), "~/Alerts.aspx");
            rc.MapPageRoute(String.Format("JA-Applications-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/applications", Convert.ToString(d["State"]).ToLower()), "~/Application/ViewApplications.aspx");
            rc.MapPageRoute(String.Format("JA-ViewStakeholderDetail-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/ViewStakeholderDetail", Convert.ToString(d["State"]).ToLower()), "~/Application/viewStakeholder.aspx");
            
            rc.MapPageRoute(String.Format("JA-Default-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/login", Convert.ToString(d["State"]).ToLower()), "~/Login/login.aspx");
            rc.MapPageRoute(String.Format("JA-forgotPassword-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/forgotpassword", Convert.ToString(d["State"]).ToLower()), "~/Login/forgotPassword.aspx");
            rc.MapPageRoute(String.Format("JA-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/inventory", Convert.ToString(d["State"]).ToLower()), "~/inventory.aspx");
            rc.MapPageRoute(String.Format("JA-InventoryRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/inventoryrevenue", Convert.ToString(d["State"]).ToLower()), "~/InventoryRevenue.aspx");
            rc.MapPageRoute(String.Format("JA-InvenotryTimeLine-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/inventorytimeline", Convert.ToString(d["State"]).ToLower()), "~/InventoryTimelLine.aspx");
            rc.MapPageRoute(String.Format("JA-Logout-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/logout", Convert.ToString(d["State"]).ToLower()), "~/Logout.aspx");

            rc.MapPageRoute(String.Format("JA-PrintBarCode-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/printbarcode", Convert.ToString(d["State"]).ToLower()), "~/PrintBarCode.aspx");
            rc.MapPageRoute(String.Format("JA-Reports-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/reports", Convert.ToString(d["State"]).ToLower()), "~/Reports/ViewReports.aspx");
            rc.MapPageRoute(String.Format("JA-Revenue-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/revenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/ViewRevenue.aspxx");
            rc.MapPageRoute(String.Format("JA-DetailRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/detailrevenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/DetailRevenue.aspx");
            rc.MapPageRoute(String.Format("JA-Settings-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/settings", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTESettings.aspx");
            rc.MapPageRoute(String.Format("JA-PTEStandard-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/ptestandard", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTEStandards.aspx");
            rc.MapPageRoute(String.Format("JA-Stakeholders-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/stakeholders", Convert.ToString(d["State"]).ToLower()), "~/Stakeholder/ViewStakeholder.aspx");
            rc.MapPageRoute(String.Format("JA-Users-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/users", Convert.ToString(d["State"]).ToLower()), "~/Users/ViewUsers.aspx");
            rc.MapPageRoute(String.Format("JA-viewStakeholder-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/viewstakeholder", Convert.ToString(d["State"]).ToLower()), "~/viewStakeholder.aspx");
            rc.MapPageRoute(String.Format("JA-viewStewardship-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/viewstewardship", Convert.ToString(d["State"]).ToLower()), "~/viewStewardship.aspx");
            rc.MapPageRoute(String.Format("JA-Welcome-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/welcome", Convert.ToString(d["State"]).ToLower()), "~/Welcome.aspx");
            rc.MapPageRoute(String.Format("JA-logosetting-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/logosetting", Convert.ToString(d["State"]).ToLower()), "~/LogoSetting/UploadOrgLogo.aspx");
            rc.MapPageRoute(String.Format("JA-bankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/bankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/ViewBankAccount.aspx");
            rc.MapPageRoute(String.Format("JA-editbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/editbankaccount", Convert.ToString(d["State"]).ToLower()), "~/editBankAccount.aspx");
            rc.MapPageRoute(String.Format("JA-addbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/addbankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/AddBankAccount.aspx");
            rc.MapPageRoute(String.Format("JA-creditcard-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/creditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/ViewCreditCard.aspx");
            //rc.MapPageRoute(String.Format("JA-editcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/editcreditcard", Convert.ToString(d["State"]).ToLower()), "~/editCreditCard.aspx");
            rc.MapPageRoute(String.Format("JA-addcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/addcreditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/AddCreditCard.aspx");
            rc.MapPageRoute(String.Format("JA-profile-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/profile", Convert.ToString(d["State"]).ToLower()), "~/ProfileSetting/ProfileSetting.aspx");
            rc.MapPageRoute(String.Format("JA-lot-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/lot-inventory", Convert.ToString(d["State"]).ToLower()), "~/lot-inventory.aspx");
            rc.MapPageRoute(String.Format("JA-lotinfo-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/lotinfo", Convert.ToString(d["State"]).ToLower()), "~/Lots/ViewLots.aspx");
            rc.MapPageRoute(String.Format("JA-createload-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/createload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            rc.MapPageRoute(String.Format("JA-inventory-tire-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/inventory-tire", Convert.ToString(d["State"]).ToLower()), "~/Lots/EditTire.aspx");
            rc.MapPageRoute(String.Format("JA-addload-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/addload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            rc.MapPageRoute(String.Format("JA-editUser-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/edituser", Convert.ToString(d["State"]).ToLower()), "~/edituser.aspx");
            rc.MapPageRoute(String.Format("JA-lots-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/lots", Convert.ToString(d["State"]).ToLower()), "~/Facility/DetailFacility.aspx");
            rc.MapPageRoute(String.Format("JA-inventory-load-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/inventory-load", Convert.ToString(d["State"]).ToLower()), "~/Load/ViewLoad.aspx");
            rc.MapPageRoute(String.Format("JA-editload-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/editload", Convert.ToString(d["State"]).ToLower()), "~/Load/EditLoad.aspx");
            rc.MapPageRoute(String.Format("JA-facility-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/facility", Convert.ToString(d["State"]).ToLower()), "~/AddFacility.aspx");
            rc.MapPageRoute(String.Format("JA-AccessDenied-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/error", Convert.ToString(d["State"]).ToLower()), "~/AccessDenied.aspx");
            
            rc.MapPageRoute(String.Format("JA-addparkinglot-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/addparkinglot", Convert.ToString(d["State"]).ToLower()), "~/Facility/AddLot.aspx");
            rc.MapPageRoute(String.Format("JA-facility-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/facility", Convert.ToString(d["State"]).ToLower()), "~/Facility/ViewFacility.aspx");

            rc.MapPageRoute(String.Format("JA-deliverynotes-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/deliverynotes", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/ViewDeliveryNotes.aspx");
            rc.MapPageRoute(String.Format("JA-adddeliverynote-{0}", Convert.ToString(d["StateName"])), String.Format("ja/{0}/adddeliverynote", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/AddDeliveryNote.aspx");
            rc.MapPageRoute(String.Format("JA-deliveryreceipt-{0}", Convert.ToString(d["StateName"])), String.Format("JA/{0}/deliveryreceipt", Convert.ToString(d["State"]).ToLower()), "~/DeliveryReceipt/ViewDeliveryReceipt.aspx");
            rc.MapPageRoute(String.Format("JA-templates-{0}", Convert.ToString(d["StateName"])), String.Format("JA/{0}/templates", Convert.ToString(d["State"]).ToLower()), "~/Templates/ViewTemplates.aspx");
            rc.MapPageRoute(String.Format("JA-addtemplate-{0}", Convert.ToString(d["StateName"])), String.Format("JA/{0}/addtemplate", Convert.ToString(d["State"]).ToLower()), "~/Templates/AddTemplate.aspx");

            rc.MapPageRoute(String.Format("JA-invoices-{0}", Convert.ToString(d["StateName"])), String.Format("JA/{0}/invoices", Convert.ToString(d["State"]).ToLower()), "~/Invoices/ViewInvoice.aspx");
            rc.MapPageRoute(String.Format("JA-AddInvoice-{0}", Convert.ToString(d["StateName"])), String.Format("JA/{0}/AddInvoice", Convert.ToString(d["State"]).ToLower()), "~/Invoices/AddInvoice.aspx");

            rc.MapPageRoute(String.Format("US-product-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/productselection", Convert.ToString(d["State"]).ToLower()), "~/Product/ProductSelection.aspx");

        }

        #endregion

        #region SK URLs

        dr = ds.Tables[0].Select("CountryId=" + 124);
        foreach (System.Data.DataRow d in dr)
        {
            rc.MapPageRoute(String.Format("SK-Registration-page-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/registration/", Convert.ToString(d["State"]).ToLower()), "~/Registration/RegistrationFormMX.aspx");
            rc.MapPageRoute(String.Format("SK-Dashboard-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/dashboard", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/Dashboard.aspx");
            rc.MapPageRoute(String.Format("SK-dashboardNotifications-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/dashboardnotfications", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/dashboardNotifications.aspx");

            rc.MapPageRoute(String.Format("SK-About-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/about", Convert.ToString(d["State"]).ToLower()), "~/About.aspx");
            rc.MapPageRoute(String.Format("SK-addInventory-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/addinventory", Convert.ToString(d["State"]).ToLower()), "~/Lots/AddInventory.aspx");
            rc.MapPageRoute(String.Format("SK-Alerts-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/alerts", Convert.ToString(d["State"]).ToLower()), "~/Alerts.aspx");
            rc.MapPageRoute(String.Format("SK-Applications-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/applications", Convert.ToString(d["State"]).ToLower()), "~/Application/ViewApplications.aspx");
            rc.MapPageRoute(String.Format("SK-ViewStakeholderDetail-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/ViewStakeholderDetail", Convert.ToString(d["State"]).ToLower()), "~/Application/viewStakeholder.aspx");
            
            rc.MapPageRoute(String.Format("SK-Default-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/login", Convert.ToString(d["State"]).ToLower()), "~/Login/login.aspx");
            rc.MapPageRoute(String.Format("SK-forgotPassword-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/forgotpassword", Convert.ToString(d["State"]).ToLower()), "~/Login/forgotPassword.aspx");
            rc.MapPageRoute(String.Format("SK-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/inventory", Convert.ToString(d["State"]).ToLower()), "~/inventory.aspx");
            rc.MapPageRoute(String.Format("SK-InventoryRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/inventoryrevenue", Convert.ToString(d["State"]).ToLower()), "~/InventoryRevenue.aspx");
            rc.MapPageRoute(String.Format("SK-InvenotryTimeLine-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/inventorytimeline", Convert.ToString(d["State"]).ToLower()), "~/InventoryTimelLine.aspx");
            rc.MapPageRoute(String.Format("SK-Logout-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/logout", Convert.ToString(d["State"]).ToLower()), "~/Logout/Logout.aspx");

            rc.MapPageRoute(String.Format("SK-PrintBarCode-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/printbarcode", Convert.ToString(d["State"]).ToLower()), "~/PrintBarCode.aspx");
            rc.MapPageRoute(String.Format("SK-Reports-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/reports", Convert.ToString(d["State"]).ToLower()), "~/Reports/ViewReports.aspx");
            rc.MapPageRoute(String.Format("SK-Revenue-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/revenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/ViewRevenue.aspx");
            rc.MapPageRoute(String.Format("SK-DetailRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/detailrevenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/DetailRevenue.aspx");
            rc.MapPageRoute(String.Format("SK-Settings-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/settings", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTESettings.aspx");
            rc.MapPageRoute(String.Format("SK-PTEStandard-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/ptestandard", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTEStandards.aspx");
            rc.MapPageRoute(String.Format("SK-Stakeholders-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/stakeholders", Convert.ToString(d["State"]).ToLower()), "~/Stakeholder/ViewStakeholder.aspx");
            rc.MapPageRoute(String.Format("SK-Users-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/users", Convert.ToString(d["State"]).ToLower()), "~/Users/ViewUsers.aspx");
            rc.MapPageRoute(String.Format("SK-viewStakeholder-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/viewstakeholder", Convert.ToString(d["State"]).ToLower()), "~/viewStakeholder.aspx");
            rc.MapPageRoute(String.Format("SK-viewStewardship-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/viewstewardship", Convert.ToString(d["State"]).ToLower()), "~/viewStewardship.aspx");
            rc.MapPageRoute(String.Format("SK-Welcome-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/welcome", Convert.ToString(d["State"]).ToLower()), "~/Welcome.aspx");
            rc.MapPageRoute(String.Format("SK-logosetting-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/logosetting", Convert.ToString(d["State"]).ToLower()), "~/LogoSetting/UploadOrgLogo.aspx");
            rc.MapPageRoute(String.Format("SK-bankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/bankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/ViewBankAccount.aspx");
            rc.MapPageRoute(String.Format("SK-editbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/editbankaccount", Convert.ToString(d["State"]).ToLower()), "~/editBankAccount.aspx");
            rc.MapPageRoute(String.Format("SK-addbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/addbankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/AddBankAccount.aspx");
            rc.MapPageRoute(String.Format("SK-creditcard-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/creditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/ViewCreditCard.aspx");
            //rc.MapPageRoute(String.Format("SK-editcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/editcreditcard", Convert.ToString(d["State"]).ToLower()), "~/editCreditCard.aspx");
            rc.MapPageRoute(String.Format("SK-addcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/addcreditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/AddCreditCard.aspx");
            rc.MapPageRoute(String.Format("SK-profile-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/profile", Convert.ToString(d["State"]).ToLower()), "~/ProfileSetting/ProfileSetting.aspx");
            rc.MapPageRoute(String.Format("SK-lot-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/lot-inventory", Convert.ToString(d["State"]).ToLower()), "~/lot-inventory.aspx");
            rc.MapPageRoute(String.Format("SK-lotinfo-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/lotinfo", Convert.ToString(d["State"]).ToLower()), "~/Lots/ViewLots.aspx");
            rc.MapPageRoute(String.Format("SK-createload-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/createload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            rc.MapPageRoute(String.Format("SK-inventory-tire-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/inventory-tire", Convert.ToString(d["State"]).ToLower()), "~/Lots/EditTire.aspx");
            rc.MapPageRoute(String.Format("SK-addload-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/addload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            rc.MapPageRoute(String.Format("SK-editUser-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/edituser", Convert.ToString(d["State"]).ToLower()), "~/edituser.aspx");
            rc.MapPageRoute(String.Format("SK-inventory-load-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/inventory-load", Convert.ToString(d["State"]).ToLower()), "~/inventory-load.aspx");

            rc.MapPageRoute(String.Format("SK-editload-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/editload", Convert.ToString(d["State"]).ToLower()), "~/Load/EditLoad.aspx");
            rc.MapPageRoute(String.Format("SK-facility-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/facility", Convert.ToString(d["State"]).ToLower()), "~/AddFacility.aspx");
            rc.MapPageRoute(String.Format("SK-AccessDenied-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/error", Convert.ToString(d["State"]).ToLower()), "~/AccessDenied.aspx");

            rc.MapPageRoute(String.Format("SK-deliverynotes-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/deliverynotes", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/ViewDeliveryNotes.aspx");
            rc.MapPageRoute(String.Format("SK-adddeliverynote-{0}", Convert.ToString(d["StateName"])), String.Format("sk/{0}/adddeliverynote", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/AddDeliveryNote.aspx");
            rc.MapPageRoute(String.Format("SK-deliveryreceipt-{0}", Convert.ToString(d["StateName"])), String.Format("SK/{0}/deliveryreceipt", Convert.ToString(d["State"]).ToLower()), "~/DeliveryReceipt/ViewDeliveryReceipt.aspx");
            rc.MapPageRoute(String.Format("SK-templates-{0}", Convert.ToString(d["StateName"])), String.Format("SK/{0}/templates", Convert.ToString(d["State"]).ToLower()), "~/Templates/ViewTemplates.aspx");
            rc.MapPageRoute(String.Format("SK-addtemplate-{0}", Convert.ToString(d["StateName"])), String.Format("SK/{0}/addtemplate", Convert.ToString(d["State"]).ToLower()), "~/Templates/AddTemplate.aspx");
            rc.MapPageRoute(String.Format("SK-invoices-{0}", Convert.ToString(d["StateName"])), String.Format("SK/{0}/invoices", Convert.ToString(d["State"]).ToLower()), "~/Invoices/ViewInvoice.aspx");
            rc.MapPageRoute(String.Format("SK-AddInvoice-{0}", Convert.ToString(d["StateName"])), String.Format("SK/{0}/AddInvoice", Convert.ToString(d["State"]).ToLower()), "~/Invoices/AddInvoice.aspx");

            rc.MapPageRoute(String.Format("US-product-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/productselection", Convert.ToString(d["State"]).ToLower()), "~/Product/ProductSelection.aspx");

        }

        #endregion

        #region AU URLs

        dr = ds.Tables[0].Select("CountryId=" + 14);
        foreach (System.Data.DataRow d in dr)
        {
            rc.MapPageRoute(String.Format("AU-Registration-page-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/registration/", Convert.ToString(d["State"]).ToLower()), "~/Registration/RegistrationFormMX.aspx");
            rc.MapPageRoute(String.Format("AU-Dashboard-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/dashboard", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/Dashboard.aspx");
            rc.MapPageRoute(String.Format("AU-dashboardNotifications-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/dashboardnotfications", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/dashboardNotifications.aspx");

            rc.MapPageRoute(String.Format("AU-About-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/about", Convert.ToString(d["State"]).ToLower()), "~/About.aspx");
            rc.MapPageRoute(String.Format("AU-addInventory-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/addinventory", Convert.ToString(d["State"]).ToLower()), "~/Lots/AddInventory.aspx");
            rc.MapPageRoute(String.Format("AU-Alerts-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/alerts", Convert.ToString(d["State"]).ToLower()), "~/Alerts.aspx");
            rc.MapPageRoute(String.Format("AU-Applications-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/applications", Convert.ToString(d["State"]).ToLower()), "~/Application/ViewApplications.aspx");
            rc.MapPageRoute(String.Format("AU-ViewStakeholderDetail-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/ViewStakeholderDetail", Convert.ToString(d["State"]).ToLower()), "~/Application/viewStakeholder.aspx");
            
            rc.MapPageRoute(String.Format("AU-Default-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/login", Convert.ToString(d["State"]).ToLower()), "~/Login/login.aspx");
            rc.MapPageRoute(String.Format("AU-forgotPassword-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/forgotpassword", Convert.ToString(d["State"]).ToLower()), "~/Login/forgotPassword.aspx");
            rc.MapPageRoute(String.Format("AU-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/inventory", Convert.ToString(d["State"]).ToLower()), "~/inventory.aspx");
            rc.MapPageRoute(String.Format("AU-InventoryRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/inventoryrevenue", Convert.ToString(d["State"]).ToLower()), "~/InventoryRevenue.aspx");
            rc.MapPageRoute(String.Format("AU-InvenotryTimeLine-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/inventorytimeline", Convert.ToString(d["State"]).ToLower()), "~/InventoryTimelLine.aspx");
            rc.MapPageRoute(String.Format("AU-Logout-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/logout", Convert.ToString(d["State"]).ToLower()), "~/Logout/Logout.aspx");

            rc.MapPageRoute(String.Format("AU-PrintBarCode-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/printbarcode", Convert.ToString(d["State"]).ToLower()), "~/PrintBarCode.aspx");
            rc.MapPageRoute(String.Format("AU-Reports-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/reports", Convert.ToString(d["State"]).ToLower()), "~/Reports/ViewReports.aspx");
            rc.MapPageRoute(String.Format("AU-Revenue-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/revenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/ViewRevenue.aspx");
            rc.MapPageRoute(String.Format("AU-DetailRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/detailrevenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/DetailRevenue.aspx");
            rc.MapPageRoute(String.Format("AU-Settings-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/settings", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTESettings.aspx");
            rc.MapPageRoute(String.Format("AU-PTEStandard-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/ptestandard", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTEStandards.aspx");
            rc.MapPageRoute(String.Format("AU-Stakeholders-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/stakeholders", Convert.ToString(d["State"]).ToLower()), "~/Stakeholder/ViewStakeholder.aspx");
            rc.MapPageRoute(String.Format("AU-Users-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/users", Convert.ToString(d["State"]).ToLower()), "~/Users/ViewUsers.aspx");
            rc.MapPageRoute(String.Format("AU-viewStakeholder-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/viewstakeholder", Convert.ToString(d["State"]).ToLower()), "~/viewStakeholder.aspx");
            rc.MapPageRoute(String.Format("AU-viewStewardship-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/viewstewardship", Convert.ToString(d["State"]).ToLower()), "~/viewStewardship.aspx");
            rc.MapPageRoute(String.Format("AU-Welcome-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/welcome", Convert.ToString(d["State"]).ToLower()), "~/Welcome.aspx");
            rc.MapPageRoute(String.Format("AU-logosetting-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/logosetting", Convert.ToString(d["State"]).ToLower()), "~/LogoSetting/UploadOrgLogo.aspx");
            rc.MapPageRoute(String.Format("AU-bankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/bankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/ViewBankAccount.aspx");
            rc.MapPageRoute(String.Format("AU-editbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/editbankaccount", Convert.ToString(d["State"]).ToLower()), "~/editBankAccount.aspx");
            rc.MapPageRoute(String.Format("AU-addbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/addbankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/AddBankAccount.aspx");
            rc.MapPageRoute(String.Format("AU-creditcard-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/creditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/ViewCreditCard.aspx");
            //rc.MapPageRoute(String.Format("AU-editcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/editcreditcard", Convert.ToString(d["State"]).ToLower()), "~/editCreditCard.aspx");
            rc.MapPageRoute(String.Format("AU-addcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/addcreditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/AddCreditCard.aspx");
            rc.MapPageRoute(String.Format("AU-profile-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/profile", Convert.ToString(d["State"]).ToLower()), "~/ProfileSetting/ProfileSetting.aspx");
            rc.MapPageRoute(String.Format("AU-lot-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/lot-inventory", Convert.ToString(d["State"]).ToLower()), "~/lot-inventory.aspx");
            rc.MapPageRoute(String.Format("AU-lotinfo-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/lotinfo", Convert.ToString(d["State"]).ToLower()), "~/Lots/ViewLots.aspx");
            rc.MapPageRoute(String.Format("AU-createload-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/createload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            rc.MapPageRoute(String.Format("AU-inventory-tire-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/inventory-tire", Convert.ToString(d["State"]).ToLower()), "~/Lots/EditTire.aspx");
            rc.MapPageRoute(String.Format("AU-addload-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/addload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            rc.MapPageRoute(String.Format("AU-editUser-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/edituser", Convert.ToString(d["State"]).ToLower()), "~/edituser.aspx");
            rc.MapPageRoute(String.Format("AU-lots-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/lots", Convert.ToString(d["State"]).ToLower()), "~/Facility/DetailFacility.aspx");
            rc.MapPageRoute(String.Format("AU-inventory-load-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/inventory-load", Convert.ToString(d["State"]).ToLower()), "~/Load/ViewLoad.aspx");
            rc.MapPageRoute(String.Format("AU-editload-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/editload", Convert.ToString(d["State"]).ToLower()), "~/Load/EditLoad.aspx");
            rc.MapPageRoute(String.Format("AU-facility-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/facility", Convert.ToString(d["State"]).ToLower()), "~/AddFacility.aspx");
            rc.MapPageRoute(String.Format("AU-AccessDenied-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/error", Convert.ToString(d["State"]).ToLower()), "~/AccessDenied.aspx");
            
            rc.MapPageRoute(String.Format("AU-addparkinglot-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/addparkinglot", Convert.ToString(d["State"]).ToLower()), "~/Facility/AddLot.aspx");
            rc.MapPageRoute(String.Format("AU-facility-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/facility", Convert.ToString(d["State"]).ToLower()), "~/Facility/ViewFacility.aspx");

            rc.MapPageRoute(String.Format("AU-deliverynotes-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/deliverynotes", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/ViewDeliveryNotes.aspx");
            rc.MapPageRoute(String.Format("AU-adddeliverynote-{0}", Convert.ToString(d["StateName"])), String.Format("au/{0}/adddeliverynote", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/AddDeliveryNote.aspx");
            rc.MapPageRoute(String.Format("AU-deliveryreceipt-{0}", Convert.ToString(d["StateName"])), String.Format("AU/{0}/deliveryreceipt", Convert.ToString(d["State"]).ToLower()), "~/DeliveryReceipt/ViewDeliveryReceipt.aspx");
            rc.MapPageRoute(String.Format("AU-templates-{0}", Convert.ToString(d["StateName"])), String.Format("AU/{0}/templates", Convert.ToString(d["State"]).ToLower()), "~/Templates/ViewTemplates.aspx");
            rc.MapPageRoute(String.Format("AU-addtemplate-{0}", Convert.ToString(d["StateName"])), String.Format("AU/{0}/addtemplate", Convert.ToString(d["State"]).ToLower()), "~/Templates/AddTemplate.aspx");
            rc.MapPageRoute(String.Format("AU-invoices-{0}", Convert.ToString(d["StateName"])), String.Format("AU/{0}/invoices", Convert.ToString(d["State"]).ToLower()), "~/Invoices/ViewInvoice.aspx");
            rc.MapPageRoute(String.Format("AU-AddInvoice-{0}", Convert.ToString(d["StateName"])), String.Format("AU/{0}/AddInvoice", Convert.ToString(d["State"]).ToLower()), "~/Invoices/AddInvoice.aspx");

            rc.MapPageRoute(String.Format("US-product-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/productselection", Convert.ToString(d["State"]).ToLower()), "~/Product/ProductSelection.aspx");
        }

        #endregion

        #region CN URLs

        dr = ds.Tables[0].Select("CountryId=" + 49);
        foreach (System.Data.DataRow d in dr)
        {
            rc.MapPageRoute(String.Format("CN-Registration-page-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/registration/", Convert.ToString(d["State"]).ToLower()), "~/Registration/RegistrationFormMX.aspx");
            rc.MapPageRoute(String.Format("CN-Dashboard-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/dashboard", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/Dashboard.aspx");
            rc.MapPageRoute(String.Format("CN-dashboardNotifications-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/dashboardnotfications", Convert.ToString(d["State"]).ToLower()), "~/Dashboard/dashboardNotifications.aspx");

            rc.MapPageRoute(String.Format("CN-About-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/about", Convert.ToString(d["State"]).ToLower()), "~/About.aspx");
            rc.MapPageRoute(String.Format("CN-addInventory-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/inventory/add", Convert.ToString(d["State"]).ToLower()), "~/Lots/AddInventory.aspx");
            rc.MapPageRoute(String.Format("CN-Alerts-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/alerts", Convert.ToString(d["State"]).ToLower()), "~/Alerts.aspx");
            rc.MapPageRoute(String.Format("CN-Applications-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/applications", Convert.ToString(d["State"]).ToLower()), "~/Application/ViewApplications.aspx");
            rc.MapPageRoute(String.Format("CN-ViewStakeholderDetail-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/ViewStakeholderDetail", Convert.ToString(d["State"]).ToLower()), "~/Application/viewStakeholder.aspx");
            
            rc.MapPageRoute(String.Format("CN-Default-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/login", Convert.ToString(d["State"]).ToLower()), "~/Login/login.aspx");
            rc.MapPageRoute(String.Format("CN-forgotPassword-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/forgotpassword", Convert.ToString(d["State"]).ToLower()), "~/Login/forgotPassword.aspx");
            rc.MapPageRoute(String.Format("CN-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/inventory", Convert.ToString(d["State"]).ToLower()), "~/inventory.aspx");
            rc.MapPageRoute(String.Format("CN-InventoryRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/inventoryrevenue", Convert.ToString(d["State"]).ToLower()), "~/InventoryRevenue.aspx");
            rc.MapPageRoute(String.Format("CN-InvenotryTimeLine-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/inventorytimeline", Convert.ToString(d["State"]).ToLower()), "~/InventoryTimelLine.aspx");
            rc.MapPageRoute(String.Format("CN-Logout-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/logout", Convert.ToString(d["State"]).ToLower()), "~/Logout/Logout.aspx");

            rc.MapPageRoute(String.Format("CN-PrintBarCode-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/printbarcode", Convert.ToString(d["State"]).ToLower()), "~/PrintBarCode.aspx");
            rc.MapPageRoute(String.Format("CN-Reports-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/reports", Convert.ToString(d["State"]).ToLower()), "~/Reports/ViewReports.aspx");
            rc.MapPageRoute(String.Format("CN-Revenue-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/revenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/ViewRevenue.aspx");
            rc.MapPageRoute(String.Format("CN-DetailRevenue-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/detailrevenue", Convert.ToString(d["State"]).ToLower()), "~/Revenue/DetailRevenue.aspx");
            rc.MapPageRoute(String.Format("CN-Settings-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/settings", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTESettings.aspx");
            rc.MapPageRoute(String.Format("CN-PTEStandard-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/ptestandard", Convert.ToString(d["State"]).ToLower()), "~/PTE/PTEStandards.aspx");
            rc.MapPageRoute(String.Format("CN-Stakeholders-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/stakeholders", Convert.ToString(d["State"]).ToLower()), "~/Stakeholder/ViewStakeholder.aspx");
            rc.MapPageRoute(String.Format("CN-Users-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/users", Convert.ToString(d["State"]).ToLower()), "~/Users/ViewUsers.aspx");
            rc.MapPageRoute(String.Format("CN-viewStakeholder-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/viewstakeholder", Convert.ToString(d["State"]).ToLower()), "~/viewStakeholder.aspx");
            rc.MapPageRoute(String.Format("CN-viewStewardship-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/viewstewardship", Convert.ToString(d["State"]).ToLower()), "~/viewStewardship.aspx");
            rc.MapPageRoute(String.Format("CN-Welcome-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/welcome", Convert.ToString(d["State"]).ToLower()), "~/Welcome.aspx");
            rc.MapPageRoute(String.Format("CN-logosetting-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/logosetting", Convert.ToString(d["State"]).ToLower()), "~/LogoSetting/UploadOrgLogo.aspx");
            rc.MapPageRoute(String.Format("CN-bankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/bankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/ViewBankAccount.aspx");
            rc.MapPageRoute(String.Format("CN-editbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/editbankaccount", Convert.ToString(d["State"]).ToLower()), "~/editBankAccount.aspx");
            rc.MapPageRoute(String.Format("CN-addbankaccount-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/addbankaccount", Convert.ToString(d["State"]).ToLower()), "~/BankAccount/AddBankAccount.aspx");
            rc.MapPageRoute(String.Format("CN-creditcard-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/creditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/ViewCreditCard.aspx");
            //rc.MapPageRoute(String.Format("CN-editcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/editcreditcard", Convert.ToString(d["State"]).ToLower()), "~/editCreditCard.aspx");
            rc.MapPageRoute(String.Format("CN-addcreditcard-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/addcreditcard", Convert.ToString(d["State"]).ToLower()), "~/Creditcard/AddCreditCard.aspx");
            rc.MapPageRoute(String.Format("CN-profile-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/profile", Convert.ToString(d["State"]).ToLower()), "~/ProfileSetting/ProfileSetting.aspx");
            rc.MapPageRoute(String.Format("CN-lot-inventory-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/lot-inventory", Convert.ToString(d["State"]).ToLower()), "~/lot-inventory.aspx");
            rc.MapPageRoute(String.Format("CN-lotinfo-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/lotinfo", Convert.ToString(d["State"]).ToLower()), "~/Lots/ViewLots.aspx");
            rc.MapPageRoute(String.Format("CN-createload-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/createload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            rc.MapPageRoute(String.Format("CN-inventory-tire-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/inventory-tire", Convert.ToString(d["State"]).ToLower()), "~/Lots/EditTire.aspx");
            rc.MapPageRoute(String.Format("CN-addload-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/addload", Convert.ToString(d["State"]).ToLower()), "~/Load/AddLoad.aspx");
            rc.MapPageRoute(String.Format("CN-editUser-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/edituser", Convert.ToString(d["State"]).ToLower()), "~/edituser.aspx");
            rc.MapPageRoute(String.Format("CN-editload-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/editload", Convert.ToString(d["State"]).ToLower()), "~/Load/EditLoad.aspx");
            rc.MapPageRoute(String.Format("CN-inventory-load-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/inventory-load", Convert.ToString(d["State"]).ToLower()), "~/inventory-load.aspx");
            rc.MapPageRoute(String.Format("CN-facility-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/facility", Convert.ToString(d["State"]).ToLower()), "~/AddFacility.aspx");
            rc.MapPageRoute(String.Format("CN-AccessDenied-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/error", Convert.ToString(d["State"]).ToLower()), "~/AccessDenied.aspx");

            rc.MapPageRoute(String.Format("Cn-deliverynotes-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/deliverynotes", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/ViewDeliveryNotes.aspx");
            rc.MapPageRoute(String.Format("CN-adddeliverynote-{0}", Convert.ToString(d["StateName"])), String.Format("cn/{0}/adddeliverynote", Convert.ToString(d["State"]).ToLower()), "~/DeliveryNotes/AddDeliveryNote.aspx");
            rc.MapPageRoute(String.Format("CN-deliveryreceipt-{0}", Convert.ToString(d["StateName"])), String.Format("CN/{0}/deliveryreceipt", Convert.ToString(d["State"]).ToLower()), "~/DeliveryReceipt/ViewDeliveryReceipt.aspx");
            rc.MapPageRoute(String.Format("CN-templates-{0}", Convert.ToString(d["StateName"])), String.Format("CN/{0}/templates", Convert.ToString(d["State"]).ToLower()), "~/Templates/ViewTemplates.aspx");
            rc.MapPageRoute(String.Format("CN-addtemplate-{0}", Convert.ToString(d["StateName"])), String.Format("CN/{0}/addtemplate", Convert.ToString(d["State"]).ToLower()), "~/Templates/AddTemplate.aspx");
            rc.MapPageRoute(String.Format("CN-invoices-{0}", Convert.ToString(d["StateName"])), String.Format("CN/{0}/invoices", Convert.ToString(d["State"]).ToLower()), "~/Invoices/ViewInvoice.aspx");
            rc.MapPageRoute(String.Format("CN-AddInvoice-{0}", Convert.ToString(d["StateName"])), String.Format("CN/{0}/AddInvoice", Convert.ToString(d["State"]).ToLower()), "~/Invoices/AddInvoice.aspx");

            rc.MapPageRoute(String.Format("US-product-{0}", Convert.ToString(d["StateName"])), String.Format("us/{0}/productselection", Convert.ToString(d["State"]).ToLower()), "~/Product/ProductSelection.aspx");
        }

        #endregion

    }
       
</script>
