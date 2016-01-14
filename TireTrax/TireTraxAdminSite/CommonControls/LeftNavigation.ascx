<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftNavigation.ascx.cs" Inherits="CommonControls_LeftNavigation" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<script type ="text/javascript">
    function NavigateToSrc(src) {
        //if (document.getElementById(src.id).className != 'active') {
        //    document.getElementById(src.id).className = 'active';
        //}
        document.getElementById(src.id).className = 'active';
        
    }
   

    function ActivateSelectedTab() {
        
        if (window.location.pathname == '/Lots/ViewLots.aspx' || window.location.pathname == '/Loads/ViewLoads.aspx') {

            document.getElementById('liInventory').className = 'active';
            document.getElementById('ulInventory').className += ' collapse in';
            document.getElementById('ulInventory').setAttribute('aria-expanded', 'true');
        }
        else if (window.location.pathname == '/PTE/PTESettings.aspx' || window.location.pathname == '/PTE/PTEStandard.aspx') {

            document.getElementById('liPTE').className = 'active';
            document.getElementById('ulPTE').className += ' collapse in';
            document.getElementById('ulPTE').setAttribute('aria-expanded', 'true');
        }
        else if (window.location.pathname == '/BankAccount/ViewBankAccount.aspx' ||
            window.location.pathname == '/Creditcard/ViewCreditcard.aspx' ||
            window.location.pathname == '/Commission/ViewCommission.aspx' ||
            window.location.pathname == '/Product/ProductSelection.aspx') {

            document.getElementById('liSettings').className = 'active';
            document.getElementById('ulSettings').className += ' collapse in';
            document.getElementById('ulSettings').setAttribute('aria-expanded', 'true');
        }
        else if (window.location.pathname == '/Permission/PagePermissions.aspx' ||
            window.location.pathname == '/Permission/AddPages.aspx' ||
            window.location.pathname == '/Permission/AddRole.aspx' ||
            window.location.pathname == '/Permission/GroupPermissions.aspx' ||
            window.location.pathname == '/Permission/UsersPermission.aspx' ||
            window.location.pathname == '/Permission/AddGroups.aspx' ||
            window.location.pathname == '/User/ViewAdminUsers.aspx') {

            document.getElementById('liPermission').className = 'active';
            document.getElementById('ulPermissions').className += ' collapse in';
            document.getElementById('ulPermissions').setAttribute('aria-expanded', 'true');
        }
        else if (window.location.pathname == '/Dashboard/admindashboard.aspx') {
            document.getElementById('liHome').className = 'active';
        }
        else if (window.location.pathname == '/Stewardship/ViewStewardship.aspx') {
            document.getElementById('liStewardship').className = 'active';
        }
        else if (window.location.pathname == '/Revenue/Revenue.aspx') {
            document.getElementById('liRevenue').className = 'active';
        }
        else if (window.location.pathname == '/Application/PendingApplication.aspx') {
            document.getElementById('liApplication').className = 'active';
        }
        else if (window.location.pathname == '/Accounting/ViewAccounting.aspx') {
            document.getElementById('liAccounting').className = 'active';
        }
        else if (window.location.pathname == '/Reports/ViewReports.aspx') {
            document.getElementById('liReports').className = 'active';
        }
        else if (window.location.pathname == '/LookupManagment/ViewLookUpManagment.aspx') {
            document.getElementById('liLookup').className = 'active';
        }
        else if (window.location.pathname == '/Invoices/ViewInvoice.aspx') {
            document.getElementById('liAccountManagement').className = 'active';
        }
       
    }

    function SetHeaderMenu(el, msg) {
        debugger;
        if (msg == null)
            document.getElementById('hPageHeading').innerHTML = document.getElementById(el).textContent;
        else
            document.getElementById('hPageHeading').innerHTML = msg;
    }
   
</script>

<nav class="navbar-default navbar-static-side" role="navigation">
            <div class="sidebar-collapse">
                <ul class="nav" id="side-menu">
                    <li class="nav-logo">
                        <a href="/Dashboard/admindashboard.aspx" class="p0">
                            <img src="/img/Logo-epr.png" alt="EPR Logo">
                        </a>
                    </li>

                    <li id ="liHome">
                        <a href="/Dashboard/admindashboard.aspx"><i class="fa fa-home"></i> <span class="nav-label">
                            <%= ResourceMgr.GetMessage("Dashboard")%>
                        </span> </a>
                    </li>
                    <li id ="liStewardship">
                        <a href="/Stewardship/ViewStewardship.aspx"><i class="fa fa-users"></i> <span class="nav-label">
                            <%= ResourceMgr.GetMessage("Stewardships")%></span> </a>
                    </li>
                     <li id ="liRevenue">
                        <a href="/Revenue/Revenue.aspx"><i class="fa fa-line-chart"></i> <span class="nav-label">
                            <%= ResourceMgr.GetMessage("Revenue")%></span> </a>
                    </li>
                    <li id ="liApplication">
                        <a href="/Application/PendingApplication.aspx"><i class="fa fa-pencil"></i> <span class="nav-label">
                            <%= ResourceMgr.GetMessage("Applications")%></span> </a>
                    </li>
                    <li id ="liAccounting">
                        <a href="/Accounting/ViewAccounting.aspx"><i class="fa fa-edit"></i> <span class="nav-label">
                            <%= ResourceMgr.GetMessage("Accounting")%></span> </a>
                    </li>
                    <li id ="liInventory" onclick ="NavigateToSrc(this);">
                        <a href="/Lots/ViewLots.aspx"><i class="fa fa-clipboard"></i> <span class="nav-label">
                            <%= ResourceMgr.GetMessage("Inventory")%></span><span class="fa arrow"></span></a>
                        <ul id ="ulInventory" class="nav nav-second-level">
                            <li><a href="/Lots/ViewLots.aspx"><%= ResourceMgr.GetMessage("Lots")%></a></li>
                            <li><a href="/Loads/ViewLoads.aspx"><%= ResourceMgr.GetMessage("Loads")%></a></li>
                        </ul>
                    </li>
                    <li id ="liReports">
                        <a href="/Reports/ViewReports.aspx"><i class="fa fa-file-text"></i> <span class="nav-label"><%= ResourceMgr.GetMessage("Reports")%></span> </a>
                    </li>
                    <li id ="liPTE" onclick ="NavigateToSrc(this);">
                        <a href="/PTE/PTESettings.aspx"><i class="fa fa-copy"></i> <span class="nav-label"><%= ResourceMgr.GetMessage("PTE")%></span><span class="fa arrow"></span></a>
                        <ul id ="ulPTE" class="nav nav-second-level">
                            <li><a href="/PTE/PTESettings.aspx"><%= ResourceMgr.GetMessage("Settings")%></a></li>
                            <li><a href="/PTE/PTEStandard.aspx"><%= ResourceMgr.GetMessage("Standards")%></a></li>
                        </ul>
                    </li>
                    <li id ="liLookup">
                        <a href="/LookupManagment/ViewLookUpManagment.aspx"><i class="fa fa-check-square"></i> <span class="nav-label">
                            <%= ResourceMgr.GetMessage("Lookup Management")%></span> </a>
                    </li>
                    <li id ="liSettings" onclick ="NavigateToSrc(this);">
                        <a href="/BankAccount/ViewBankAccount.aspx"><i class="fa fa-gear"></i> <span class="nav-label">
                            <%= ResourceMgr.GetMessage("Settings")%></span><span class="fa arrow"></span></a>
                        <ul id ="ulSettings" class="nav nav-second-level">
                            <li><a href="/BankAccount/ViewBankAccount.aspx"><%= ResourceMgr.GetMessage("Bank Accounts")%></a></li>
                            <li><a href="/Creditcard/ViewCreditcard.aspx"><%= ResourceMgr.GetMessage("Credit Cards")%></a></li>
                            <li><a href="/Commission/ViewCommission.aspx"><%= ResourceMgr.GetMessage("Commisions")%></a></li>
                            <%--<li><a href="/Product/ProductSelection.aspx"><%= ResourceMgr.GetMessage("Product Selection")%></a></li>--%>
                        </ul>
                    </li>
                     <li id ="liPermission" onclick ="NavigateToSrc(this);">
                        <a href="/Permission/PagePermissions.aspx"><i class="fa fa-lock"></i> <span class="nav-label"><%= ResourceMgr.GetMessage("Permissions")%></span><span class="fa arrow"></span></a>
                        <ul id ="ulPermissions" class="nav nav-second-level">
                            <li><a href="/Permission/AddGroups.aspx"><%= ResourceMgr.GetMessage("Add Groups")%></a></li>
                            <li><a href="/Permission/AddPages.aspx"><%= ResourceMgr.GetMessage("Add Pages")%></a></li>
                            <li><a href="/Permission/AddRole.aspx"><%= ResourceMgr.GetMessage("Add Roles")%></a></li>
                            <li><a href="/Permission/GroupPermissions.aspx"><%= ResourceMgr.GetMessage("Group Permissions")%></a></li>
                            <li><a href="/Permission/UsersPermission.aspx"><%= ResourceMgr.GetMessage("User Permissions")%></a></li>
                            <li><a href="/User/ViewAdminUsers.aspx"><%= ResourceMgr.GetMessage("Admin Users")%></a></li>
                            <li><a href="/Permission/PagePermissions.aspx"><%= ResourceMgr.GetMessage("Page Permissions")%></a></li>
                        </ul>
                    </li>
                    <li id ="liAccountManagement">
                        
                       <a href ="/Invoices/ViewInvoice.aspx"><i class="fa fa-lock"></i><span class="nav-label">
                           <%= ResourceMgr.GetMessage("Account Management") %></span></a>
                    </li>
                </ul>

            </div>
        </nav>