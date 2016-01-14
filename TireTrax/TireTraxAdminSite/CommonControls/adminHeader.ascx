<%@ Control Language="C#" AutoEventWireup="true" CodeFile="adminHeader.ascx.cs" Inherits="CommonControls_adminHeader" %>


<%@ Register Src="adminMenuControl.ascx" TagName="adminMenuControls" TagPrefix="uc1" %>
<%@ Register Src="EditionControl.ascx" TagName="adminEditionControls" TagPrefix="uc2" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<script type="text/javascript">


    $(document).ready(function () {

        validateUrl();
        //        $('.header-tabs > ul > li').bind('mouseout', closeSubMenu);
        //              $('.header-tabs > ul > li > #Settings').bind('mouseover', openSettingMenu);
        //               $('.header-tabs > ul > li > #Inventory').bind('mouseover', openInventoryMenu);
        //               $('.header-tabs > ul > li > #groups').bind('mouseover', openPermissionMenu);

        $('.submenu').Click(function () {
            $('.submenu').css('visibility', 'visible');
        });

        function openSubMenu() {
            $('.submenu').css('visibility', 'visible');

        };

        function closeSubMenu() {
            $('.submenu').css('visibility', 'hidden');
            validateUrl();
        };

        function openSettingMenu() {
            $('.Inventory').hide();
            $('.Settings').show();
            $('.groups').hide();
            $('.PTE').hide();
        };

        function openInventoryMenu() {
            openSubMenu();
            $('.Inventory').show();
            $('.Settings').hide();
            $('.PTE').hide();
            $('.groups').hide();
        };

        function openPermissionMenu() {
            openSubMenu();
            $('.Inventory').hide();
            $('.Settings').hide();
            $('.PTE').hide();
            $('.groups').show();
        };
        function openPermissionMenu() {
            openSubMenu();
            $('.Inventory').hide();
            $('.Settings').hide();
            $('.groups').hide();
            $('.PTE').show();
        };

    });

    function validateUrl() {
        if (window.location.href.toLowerCase().indexOf("logosetting") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').show();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("bankaccount") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').show();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("ptesettings") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').show();
        }
        else if (window.location.href.toLowerCase().indexOf("ptestandard") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').show();
        }
        else if (window.location.href.toLowerCase().indexOf("/dashboard/admindashboard") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/stewardship/viewstewardship") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/application/pendingapplication") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/accounting/viewaccounting") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/reports/viewreports") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/pte/ptesettings") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').show();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/lookupmanagment/viewlookupmanagment") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/application/viewapplication") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/user/viewuser") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/user/edituser") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/user/adduser") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }


        else if (window.location.href.toLowerCase().indexOf("/stakeholder/viewstakeholder") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/stewardship/viewstewardship") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }

        else if (window.location.href.toLowerCase().indexOf("/stewardship/viewdetailstewardship") > -1) {
            $('header-tabs').show();
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }




        else if (window.location.href.toLowerCase().indexOf("creditcard") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').show();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("commission") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').show();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("admintypecommission") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').show();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("adminpermission") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').show();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("addpermission") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').hide();
            $('.Settings').show();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("lots/viewlots") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').show();
            $('.Settings').hide();
            $('.groups').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/loads/viewloads") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').show();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("facility") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').show();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("facility-detail") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').show();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("viewinventory") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').show();
            $('.groups').hide();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("permission") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').show();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/user/viewadminusers") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').show();
            $('.Settings').hide();
            $('.PTE').hide();
        }
        else if (window.location.href.toLowerCase().indexOf("/user/addadminuser") > -1) {
            $('.submenu').css('visibility', 'visible');
            $('.Inventory').hide();
            $('.groups').show();
            $('.Settings').hide();
            $('.PTE').hide();
        }



    }
</script>


<div class="header">
    <%--<div class="header-top-login"> <a href="../Logout.aspx"><%= ResourceMgr.GetMessage("logout")%></a> </div>--%>
    <div style="clear: both"></div>
    <div class="logo_outer-left">
        <div class="logo" style="margin-left: 0px; margin-top: 0px;"><a href="#">
            <img src="/images/NewTTlogo.png" width="300" /></a> </div>
        <div class="logo-second"></div>
    </div>
    <div class="header_right">
        <div class="admin_section">
            <div class="pd5">Welcome <b>
                <asp:Literal ID="litLoginName" runat="server"></asp:Literal>
            </b>&nbsp; | &nbsp; <a href="/Login/Logout.aspx">Logout</a></div>
            <div class="sm_txt">
                <asp:Literal ID="litLastLoginNotAvailable" runat="server" Text="First Time Login" Visible="false"></asp:Literal><asp:Literal ID="litLastLoginDate" runat="server">Last Login on <b>29 October, 2012</b> at <b>11:00am</b></asp:Literal></div>
        </div>
        <img src="/images/arrow_edge.png" class="arrow">
        <div class="flag_block">
            <uc2:adminEditionControls ID="adminEditionControls1" runat="server" />
        </div>
    </div>
    <div style="clear: both"></div>
    <div class="top-navigation-outer">
        <div class="header-tabs">
            <uc1:adminMenuControls ID="adminMenuControls1" runat="server" />
        </div>

    </div>
    <br clear="all" />
    <div id="menu-sub" style="visibility: hidden;" class="submenu">
        <ul>

            <li><a href="/BankAccount/ViewBankAccount.aspx" class="Settings"><%= ResourceMgr.GetMessage("Bank Accounts")%></a></li>
            <li><a href="/Creditcard/ViewCreditcard.aspx" class="Settings"><%= ResourceMgr.GetMessage("Credit Cards")%></a></li>
            <li><a href="/Commission/ViewCommission.aspx" class="Settings"><%= ResourceMgr.GetMessage("Commissions")%></a></li>
            <li><a href="/Templates/ViewTemplates.aspx" class="Settings"><%= ResourceMgr.GetMessage("Template")%></a></li>
            <%--      <li > <a href="/AdminPermission.aspx" class="Settings"><%= ResourceMgr.GetMessage("Permissions")%></a></li>--%>

            <li><a href="/Lots/ViewLots.aspx" class="Inventory"><%= ResourceMgr.GetMessage("Lots")%></a></li>
            <li><a href="/Loads/ViewLoads.aspx" class="Inventory"><%= ResourceMgr.GetMessage("Loads")%></a></li>

            <li><a href="/PTE/PTEStandard.aspx" class="PTE"><%= ResourceMgr.GetMessage("Standards")%></a></li>




            <li><a href="/Permission/AddGroups.aspx" id="groups" class="groups"><%= ResourceMgr.GetMessage("Add Groups")%></a></li>
            <li><a href="/Permission/AddPages.aspx" id="pages" class="groups"><%= ResourceMgr.GetMessage("Add Pages")%></a></li>
            <li><a href="/Permission/AddRole.aspx" id="A2" class="groups"><%= ResourceMgr.GetMessage("Add Roles")%></a></li>
            <li><a href="/Permission/GroupPermissions.aspx" id="A1" class="groups"><%= ResourceMgr.GetMessage("Group Permission")%></a></li>
            <li><a href="/Permission/UsersPermission.aspx" id="A3" class="groups"><%= ResourceMgr.GetMessage("User Permissions")%></a></li>
            <li><a href="/User/ViewAdminUsers.aspx" id="A4" class="groups"><%= ResourceMgr.GetMessage("Admin Users")%></a></li>

        </ul>
        <br clear="all" />
    </div>
    <div class="breadcrum-wrapper">
        <%-- Please dont remove or change below h1 id=hPageHeading --%>
        <h1 id="hPageHeading">Dashboard</h1>
        <div class="breadcrum_sub">
            <%= ResourceMgr.GetMessage("You are here")%>:
            <img src="/Images/breadcrum_arrow.png">
            <asp:SiteMapPath ID="SiteMapPath1" runat="server">
                <PathSeparatorTemplate>
                    <img src="/Images/breadcrum_arrow.png">
                </PathSeparatorTemplate>
            </asp:SiteMapPath>
            <%-- <a href="javascript:window.history.back();" style="float:right;">back</a>--%>
        </div>
    </div>
    <%--<div class="welcome-bar" align="center"> <%= ResourceMgr.GetMessage("Welcome")%> ! Rick </div>--%>
</div>
