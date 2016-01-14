<%@ Control Language="C#" AutoEventWireup="true" CodeFile="adminMenuControl.ascx.cs" Inherits="CommonControls_adminMenuControl" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<script type="text/javascript">
    function SetHeaderMenu(el, msg) {
        $("#ulMenu").find("a.selected-tabs").removeClass("selected-tabs");
        $("#" + el).find("a").addClass("selected-tabs");
        if (msg == null)
            $("#hPageHeading").html($("#" + el).find("a").html());
        else
            $("#hPageHeading").html(msg);
    }

    $(document).ready(function () {

        validateUrl();
//        $('.header-tabs > ul > li').bind('mouseout', closeSubMenu);
//        $('.header-tabs > ul > li > #Settings').bind('mouseover', openSubMenu);

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



    });
</script>

<ul>
    <li id="liHome"> <a  href="/Dashboard/admindashboard.aspx"><%= ResourceMgr.GetMessage("Home")%></a> </li>
    <li id="liStewardship"> <a href="/Stewardship/ViewStewardship.aspx"><%= ResourceMgr.GetMessage("Stewardships")%></a> </li>
    <%--<li id="liRoles"> <a href="/securityRoles.aspx"><%= ResourceMgr.GetMessage("Security/Roles")%></a> </li>--%>
   <%-- <li id="liRoles"> <a href="/SecurityRoles/ViewSecurityRoles.aspx"><%= ResourceMgr.GetMessage("Security/Roles")%></a> </li>--%>
   <li id="liApplication"> <a href="/Application/PendingApplication.aspx"><%= ResourceMgr.GetMessage("Applications")%></a> </li>
    <li id="liAccounting"> <a href="/Accounting/ViewAccounting.aspx"><%= ResourceMgr.GetMessage("Accounting")%></a> </li>
    <li id="liInventory"> <a href="/Lots/ViewLots.aspx" id="Inventory"><%= ResourceMgr.GetMessage("Inventory")%></a> </li>
    <li id="liReports"> <a href="/Reports/ViewReports.aspx"><%= ResourceMgr.GetMessage("Reports")%></a> </li>
    <li id="liPTE"> <a href="/PTE/PTESettings.aspx"><%= ResourceMgr.GetMessage("PTE")%></a> </li>
    <li id="liLookup"> <a href="/LookupManagment/ViewLookUpManagment.aspx"><%= ResourceMgr.GetMessage("Lookup Management")%></a></li>
    <li id="liSettings"> <a href="/BankAccount/ViewBankAccount.aspx" id="Settings"><%= ResourceMgr.GetMessage("Setting")%></a></li>
     <li id="liPermission" > <a href="/Permission/PagePermissions.aspx" id="permissions"><%= ResourceMgr.GetMessage("Permissions")%></a>
     
     
     </li>
</ul>


