<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftNavigation.ascx.cs" Inherits="CommonControls_LeftNavigation" %>

<nav class="navbar-default navbar-static-side" role="navigation">
    <div class="sidebar-collapse">
        <ul class="nav" id="side-menu">
            <li class="nav-logo">
                <a href="dashboard" class="p0">
                     <img src="/img/productLoop-Logo.png" id="logoimg" runat="server" style="height: 40px; width: auto" alt="Product Loop">
                </a>
            </li>

            <li class="nav-header text-center">
                
                <div class="dropdown profile-element">
                    <span>
                        <img alt="image" id="profileimage"  style="height: 65px; width: 65px;" runat="server" class="img-circle" src="/img/placeholder.png" />
                    </span>
                    <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                        <span class="clear"><span class="block m-t-xs"><strong class="font-bold">
                            <asp:Label runat="server" ID="lblCompanyName"></asp:Label>
                        </strong>
                        </span><span class="text-muted text-xs block">
                            <asp:Label runat="server" ID="lblRoleName"></asp:Label>
                            <b class="caret"></b></span></span></a>
                    <ul class="dropdown-menu animated fadeInRight m-t-xs">
                        <li><a href="profile">Profile</a></li>
                        <li><a href="Users">Users</a></li>
                        <li><a href="bankaccount">Bank Account</a></li>
                        <li class="divider"></li>
                        <li><a href="/Logout/Logout.aspx">Logout</a></li>
                    </ul>
                </div>
                <%--<div class="logo-element">
                    EPR
                </div>--%>
                <span class="clear"><span class="block m-t-xs"><strong class="font-bold">
                    <asp:Label ID="lblProduct" runat="server" ForeColor="White"></asp:Label>
                </strong></span>
                </span>
            </li>

            <asp:Repeater ID="Rptmenu" runat="server" OnItemDataBound="ItemBound">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="hidResourceId" runat="server" Value='<%#Eval("intResourceId")%>' />
                    <asp:Literal ID="ltrMenu" runat="server"></asp:Literal>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
        </ul>
    </div>
</nav>
