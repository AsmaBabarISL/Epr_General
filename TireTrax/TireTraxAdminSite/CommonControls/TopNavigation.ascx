<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopNavigation.ascx.cs" Inherits="CommonControls_TopNavigation" %>
<%@ Register Src="~/CommonControls/EditionControl.ascx" TagPrefix="uc1" TagName="EditionControl" %>

<nav class="navbar navbar-static-top" role="navigation" style="margin-bottom: 0">
        <div class="navbar-header">    
            <a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="#"><i class="fa fa-bars"></i> </a>
            <div class="select-edition">
                <uc1:EditionControl runat="server" ID="EditionControl" />       
            </div>
        </div>
            <ul class="nav navbar-top-links navbar-right">
                <li class="dropdown user-details">
                    <a class="dropdown-toggle count-info" data-toggle="dropdown" href="#">
                    <span class="m-r-sm text-muted"> Welcome <asp:Literal ID="litLoginName" runat="server"></asp:Literal> <b class="caret"></b></span>
                        </a>
                    <ul class="dropdown-menu dropdown-alerts">
                        <li>
                            <a>
                                <p>
                                    <asp:Literal ID="litLastLoginNotAvailable" runat="server" Text="First Time Login" Visible="false"></asp:Literal><asp:Literal ID="litLastLoginDate" runat="server">Last Login on <b>29 October, 2012</b> at <b>11:00am</b></asp:Literal>
                                </p>
                            </a>
                        </li>
                    </ul>
                </li>


                <li>
                    <a href="/Login/Logout.aspx">
                        <i class="fa fa-sign-out"></i> Log out
                    </a>
                </li>
            </ul>

        </nav>
            <div></div>