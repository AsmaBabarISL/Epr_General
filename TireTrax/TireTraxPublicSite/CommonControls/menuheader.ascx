<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menuheader.ascx.cs" Inherits="CommonControls_menuheader" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<script type="text/javascript">


    function SetHeaderMenu(el, msg) {
        $("#ulMenu").find("a.selected-tabs").removeClass("selected-tabs");
        $("#" + el).find("a").addClass("selected-tabs");
        if (msg == null)
            $("#h1").html($("#" + el).find("a").html());
        else
            $("#h1").html(msg);
    }
    function validateUrl() {
       
        if (window.location.href.toLowerCase().indexOf("logosetting") > -1) {
            
            $('.Settings').show();
        }
        else if (window.location.href.toLowerCase().indexOf("bankaccount") > -1) {
            
            $('.Settings').show();
        }
        else if (window.location.href.toLowerCase().indexOf("creditcard") > -1) {
             
            $('.Settings').show();
        }
        else if (window.location.href.toLowerCase().indexOf("profile") > -1) {
            
            $('.Settings').show();
        }

        else if (window.location.href.toLowerCase().indexOf("settings") > -1) {
            
            $('.PTE').show();
        }
        else if (window.location.href.toLowerCase().indexOf("inventory-load") > -1) {

            $('.Inventory').show();
        }
        else if (window.location.href.toLowerCase().indexOf("lotinfo") > -1) {

            $('.Inventory').show();
        }
        else if (window.location.href.toLowerCase().indexOf("facility") > -1) {

            $('.Inventory').show();
        }
        else if (window.location.href.toLowerCase().indexOf("deliverynotes") > -1) {

            $('.Inventory').show();
        }
        else if (window.location.href.toLowerCase().indexOf("deliveryreceipt") > -1) {

            $('.Inventory').show();
        }
        else if (window.location.href.toLowerCase().indexOf("templates") > -1) {

            $('.Settings').show();
        }
        else if (window.location.href.toLowerCase().indexOf("AccountManagement") > -1) {

            $('.Settings').show();
        }
       

    }

    $(document).ready(function () {

        validateUrl();
        $('.header-tabs > ul > li > #link_Settings').bind('onClick', openSettingMenu);
        $('.header-tabs > ul > li > #link_Inventory').bind('onClick', openInventoryMenu);
        $('.header-tabs > ul > li > #link_PTE').bind('onClick', openPTEMenu);


        $('.Settings').hover(function () {
            $('.Settings').show();

        });

        $('.Inventory').hover(function () {
            $('.Inventory').show();
        });

        $('.Inventory').hover(function () {
            $('.Inventory').show();
        });

        function openSettingMenu() {
            $('.Inventory').hide();
            $('.Settings').show();
        };

        function openPTEMenu() {
          
            $('.Inventory').hide();
            $('.Settings').hide();
            $('.ptestandard').show();
        };


        function openInventoryMenu() {
           
            $('.Settings').hide();
            $('.Inventory').show();
        };

        function closeSubMenu() {

            $('.Settings').hide();
            // validateUrl();
        };
        function closeInventoryMenu() {

            $('.Inventory').hide();
            //validateUrl();
        };



    });
</script>
<asp:Repeater ID="Rptmenu" runat="server" OnItemDataBound="ItemBound">
    <HeaderTemplate>
        <div class="top-navigation-outer">
            <div class="header-tabs">
                <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <asp:HiddenField ID="hidResourceId" runat="server" Value='<%#Eval("intResourceId")%>' />
        <li>
            <asp:Literal ID="ltrMenu" runat="server"></asp:Literal>
        </li>
        <%--<asp:Repeater ID="rptSubMenu" runat="server" OnItemDataBound="ItemSubMenuBound">
                        
                            <HeaderTemplate>
                           </ul>
                            </div> </div>
               
                            <br clear="all">
                            <div id="menu-sub" style=" visibility:hidden;" class="submenu">
                            <ul  >
                            </HeaderTemplate>
                            <ItemTemplate>
                                
                            <li ><a href="<%#Eval("vchModuleName")%>" 
                                id="link_<%#Eval("vchName")%>">
                                <%#ResourceMgr.GetMessage(Eval("vchName").ToString())%>
                            </a></li>
                           
                            </ItemTemplate>
                            <FooterTemplate>
                             </ul>
                          <br clear="all">
                      </div>
                                <asp:Literal ID="litSumMenuFooter" runat="server"></asp:Literal>
                        </FooterTemplate>
                        </asp:Repeater>--%>
    </ItemTemplate>
    <FooterTemplate>
        <asp:Literal ID="litfooter" runat="server"></asp:Literal>
        <%----%>
    </FooterTemplate>
</asp:Repeater>

<asp:Repeater ID="rttSubMenu" runat="server" OnItemDataBound="ItemSubMenuBound" Visible="false">
    <HeaderTemplate>

        <div id="menu-sub" class="submenu">
            <%--<img src="../images/arrow_edge.png" />--%>
            <ul style="display: block;">
    </HeaderTemplate>
    <ItemTemplate>
    <asp:Literal ID="litSubMenuCtrl" runat="server"></asp:Literal>
       <%-- <li><a href="<%#Eval("vchModuleName")%>" id="link_<%#Eval("vchName")%>">
            <%#ResourceMgr.GetMessage(Eval("vchName").ToString())%>
        </a></li>--%>
    </ItemTemplate>
    <FooterTemplate>
        </ul> 
        <br clear="all">
        </div>
    </FooterTemplate>
</asp:Repeater>
<h1 id="hPageHeading">
</h1>
<div class="breadcrum-wrapper">

    <h1 id="h1">
        Dashboard</h1>
    <div class="breadcrum_sub">
        <%= ResourceMgr.GetMessage("You are here")%>:
        <img src="/images/breadcrum_arrow.png">
        <asp:SiteMapPath ID="SiteMapPath1" runat="server">
            <PathSeparatorTemplate>
                <img src="/images/breadcrum_arrow.png">
            </PathSeparatorTemplate>
        </asp:SiteMapPath>
    </div> 
</div>
