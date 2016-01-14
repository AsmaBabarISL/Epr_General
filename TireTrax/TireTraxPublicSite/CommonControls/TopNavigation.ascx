<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopNavigation.ascx.cs" Inherits="CommonControls_topnavigation" %>

<%@ Register Src="CountryFlagTop.ascx" TagName="editionControl" TagPrefix="uc2" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>

<div class="header">
    <div class="logo_outer-left" style="display: none;">
        <div class="logo">
            <a href="#">

                <img src="/images/NewTTlogo.png" width="300" runat="server" id="imgLogoDefault" visible="false" />
                <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Gray" Style="visibility: hidden;">Edit</asp:HyperLink>


                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="/logo-setting.aspx" Style="color: #8A8A8A; visibility: hidden;">Edit</asp:LinkButton>

                <asp:Image ID="imgLogo" runat="server" ImageUrl="/images/txlogo.png" />

            </a>
            <span></span>
        </div>
    </div>
</div>
<!-- New UI Below -->
<asp:UpdatePanel ID="upnlTopNav" runat="server">
    <ContentTemplate>


        <nav class="navbar navbar-static-top" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="#"><i class="fa fa-bars"></i></a>
            </div>
            <div class="navbar-header" style="position: inherit; margin-top: 22px;">
                <span class="m-r-sm text-muted">
                    <asp:Label ID="lblDetail" runat="server" ></asp:Label>
                </span>
            </div>
            <ul class="nav navbar-top-links navbar-right">
                <li class="dropdown user-details">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <span class="m-r-sm text-muted">Welcome
                    <asp:Literal ID="litLoginName" runat="server"></asp:Literal>
                            <%--<b class="caret"></b>--%>

                        </span>
                    </a>
                    <ul class="dropdown-menu dropdown-alerts dropdown-userinfo">
                        <li>
                            <a>
                                <p>
                                    <asp:Literal ID="litLastLoginNotAvailable" runat="server" Text="First Time Login" Visible="false"></asp:Literal>
                                    <asp:Literal ID="litLastLoginDate" runat="server">Last Login on <b> 29 October, 2012 </b> at <b>11:00am</b></asp:Literal>
                                    <br />
                                    <asp:Label ID="lblcompanyname" runat="server" Text="Company Name"></asp:Label>
                                    <br />
                                    Your Role <b>
                                        <asp:Label ID="lblSubRoleName" runat="server"></asp:Label>
                                    </b>
                                    <b>
                                        <asp:Label ID="lblRoleName" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <img id="img" runat="server" src='/images/usa_flag.png' />
                                    <asp:Literal ID="litRole" runat="server"></asp:Literal>
                                </p>
                            </a>
                        </li>
                    </ul>
                </li>
                <li class="dropdown">
                    <a class="dropdown-toggle count-info" data-toggle="dropdown" href="#">
                        <i class="fa fa-bell"></i>
                        <asp:Label CssClass="label label-primary" ID="lblnotficationcount" runat="server" Text=""></asp:Label>
                    </a>
                    <div class="dropdown-menu dropdown-alerts dropdown-notification">
                        <asp:ListView runat="server" ID="lvmessagesmain" DataKeyNames="intNotificationId"
                            OnItemCommand="lvmessagesmain_ItemCommand">
                            <EmptyDataTemplate>
                                <table id="Table3" runat="server" style="">
                                    <tr>
                                        <td>
                                            <div>
                                                No Notifications found.
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr style="">
                                    <td>
                                        <div class="custom-notification">

                                            <div visible='<%# !(Eval("intFromUserId").ToString().Equals("0") )%>' runat="server">
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("RecievedFrom") %>' />
                                                <asp:Label ID="Label1" CssClass="rec-fromTitle" runat="server" Text='<%# Eval("vchNotificationText") %>' />
                                            </div>
                                            <div visible='<%# !(Eval("intFromOrganizationId").ToString().Equals("0") )%>'
                                                runat="server">
                                                <asp:Label ID="Label2" runat="server" CssClass="notific-label" Text='<%# Eval("RecievedFrom") %>' />
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchNotificationText") %>' />
                                            </div>
                                        </div>
                                        <div id="Div2" runat="server" visible='<%# Eval("bitIsReaded") %>'>
                                            Seen
                                        </div>
                                        <div class="mark-read">
                                            <div id="Div3" runat="server" visible='<%#Eval("bitIsReaded").ToString().Equals("False")%>'>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="" ToolTip="Mark As Read"
                                                    Visible='<%#(Eval("bitIsReaded").ToString().Equals("False")&& !Eval("intFromUserId").ToString().Equals(UserInfo.GetCurrentUserInfo().UserId.ToString()))%>'
                                                    CommandArgument='<%# Eval("intNotificationId") %>' CommandName="MarkRead">Mark as Read</asp:LinkButton>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>

                                <table id="Table5" runat="server" class="" width="100%" border="0" cellspacing="0"
                                    cellpadding="0">
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>

                            </LayoutTemplate>
                        </asp:ListView>
                        <div class="text-center link-block">
                            <a class="seeall-notification" href="dashboardnotfications">
                                <strong>See All</strong>
                                <i class="fa fa-angle-right"></i>
                            </a>
                        </div>
                    </div>

                </li>


                <li>
                    <a href="/Logout/Logout.aspx">
                        <i class="fa fa-sign-out"></i>Log out
                    </a>
                </li>
            </ul>

        </nav>
    </ContentTemplate>
</asp:UpdatePanel>
