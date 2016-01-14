<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="Dashboard_dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $(".ajaxModal-popup").appendTo("form");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="pnlpopup" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-lg-3">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <span class="label label-success pull-right">Annual</span>
                            <h5>Income</h5>
                        </div>
                        <div class="ibox-content">
                            <h1 class="no-margins">
                                <asp:Label runat="server" ID="lblYearlyRevenue"></asp:Label>
                            </h1>
                            <%--<div class="stat-percent font-bold text-success">98% <i class="fa fa-bolt"></i></div>
                            <small>Total income</small>--%>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <span class="label label-info pull-right">Half Year</span>
                            <h5>Income</h5>
                        </div>
                        <div class="ibox-content">
                            <h1 class="no-margins">
                                <asp:Label runat="server" ID="lblHalfYearly"></asp:Label>
                            </h1>
                            <%-- <div class="stat-percent font-bold text-info">20% <i class="fa fa-level-up"></i></div>
                            <small>New orders</small>--%>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <span class="label label-primary pull-right">This Quarter</span>
                            <h5>Income</h5>
                        </div>
                        <div class="ibox-content">
                            <h1 class="no-margins"> <asp:Label runat="server" ID="lblQuarterly"></asp:Label></h1>
                           <%-- <div class="stat-percent font-bold text-navy">44% <i class="fa fa-level-up"></i></div>
                            <small>New visits</small>--%>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <span class="label label-danger pull-right">This Month</span>
                            <h5>Income</h5>
                        </div>
                        <div class="ibox-content">
                            <h1 class="no-margins"><asp:Label runat="server" ID="lblMonthly"></asp:Label></h1>
                            <%--<div class="stat-percent font-bold text-danger">38% <i class="fa fa-level-down"></i></div>
                            <small>In first month</small>--%>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-9">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>New users</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-hover epr-sec-table no-margins">
                                            <thead>
                                                <tr>
                                                    <th>Login</th>
                                                    <th>Business Name</th>
                                                    <th>Role</th>
                                                    <th>Contact Name</th>
                                                    <th>Approve</th>
                                                    <th>Reject</th>
                                                    <th>Status</th>
                                                    <th>Edit</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>admin@nexencorporation.com</td>
                                                    <td>Nexen Corporation</td>
                                                    <td>Admin</td>
                                                    <td>Admin Nexen</td>
                                                    <td><a href="#" class="text-navy"><i class="fa fa-check"></i>Approve </a></td>
                                                    <td><a href="#" class="text-danger"><i class="fa fa-times"></i>Reject </a></td>
                                                    <td>
                                                        <label class="badge badge-primary">Approved</label></td>
                                                    <td><a href="#" class="text-navy"><i class="fa fa-pencil"></i>Edit </a></td>
                                                </tr>
                                                <tr>
                                                    <td>admin@nexencorporation.com</td>
                                                    <td>Nexen Corporation</td>
                                                    <td>Admin</td>
                                                    <td>Admin Nexen</td>
                                                    <td><a href="#" class="text-navy"><i class="fa fa-check"></i>Approve </a></td>
                                                    <td><a href="#" class="text-danger"><i class="fa fa-times"></i>Reject </a></td>
                                                    <td>
                                                        <label class="badge badge-primary">Approved</label></td>
                                                    <td><a href="#" class="text-navy"><i class="fa fa-pencil"></i>Edit </a></td>
                                                </tr>
                                                <tr>
                                                    <td>admin@nexencorporation.com</td>
                                                    <td>Nexen Corporation</td>
                                                    <td>Admin</td>
                                                    <td>Admin Nexen</td>
                                                    <td><a href="#" class="text-navy"><i class="fa fa-check"></i>Approve </a></td>
                                                    <td><a href="#" class="text-danger"><i class="fa fa-times"></i>Reject </a></td>
                                                    <td>
                                                        <label class="badge badge-danger">Rejected</label></td>
                                                    <td><a href="#" class="text-navy"><i class="fa fa-pencil"></i>Edit </a></td>
                                                </tr>
                                                <tr>
                                                    <td>admin@nexencorporation.com</td>
                                                    <td>Nexen Corporation</td>
                                                    <td>Admin</td>
                                                    <td>Admin Nexen</td>
                                                    <td><a href="#" class="text-navy"><i class="fa fa-check"></i>Approve </a></td>
                                                    <td><a href="#" class="text-danger"><i class="fa fa-times"></i>Reject </a></td>
                                                    <td>
                                                        <label class="badge badge-warning">Pending</label></td>
                                                    <td><a href="#" class="text-navy"><i class="fa fa-pencil"></i>Edit </a></td>
                                                </tr>
                                                <tr>
                                                    <td>admin@nexencorporation.com</td>
                                                    <td>Nexen Corporation</td>
                                                    <td>Admin</td>
                                                    <td>Admin Nexen</td>
                                                    <td><a href="#" class="text-navy"><i class="fa fa-check"></i>Approve </a></td>
                                                    <td><a href="#" class="text-danger"><i class="fa fa-times"></i>Reject </a></td>
                                                    <td>
                                                        <label class="badge badge-primary">Approved</label></td>
                                                    <td><a href="#" class="text-navy"><i class="fa fa-pencil"></i>Edit </a></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Notifications</h5>
                            <div class="ibox-tools">
                                <a class="" href="dashboardnotfications">See All </a>
                            </div>
                        </div>
                        <div class="ibox-content no-border-last">
                            <div class="row">
                                <div class="col-lg-12">
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

                                                        <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("intFromUserId") %>' Visible='<%# !(Eval("intFromUserId").ToString().Equals("0") )%>'
                                                            CommandName="OpenPopupFromUser" runat="server">
                                                            <asp:Label ID="Label3" runat="server" CssClass="" Text='<%# Eval("RecievedFrom") %>' />
                                                            <asp:Label ID="Label1" runat="server" CssClass="" Text='<%# Eval("vchNotificationText") %>' />
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton6" CommandArgument='<%# Eval("intFromOrganizationId") %>' Visible='<%# !(Eval("intFromOrganizationId").ToString().Equals("0") )%>'
                                                            CommandName="OpenPopupFromOrg" runat="server">
                                                            <asp:Label ID="Label2" CssClass="notific-label" runat="server" Text='<%# Eval("RecievedFrom") %>' />
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchNotificationText") %>' />
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div id="Div2" runat="server" visible='<%# Eval("bitIsReaded") %>'>
                                                        Seen
                                                    </div>
                                                    <div class="mark-read">
                                                        <div id="Div3" runat="server" visible='<%#Eval("bitIsReaded").ToString().Equals("False")%>'>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass=""
                                                                ToolTip="Mark As Read"
                                                                Visible='<%#(Eval("bitIsReaded").ToString().Equals("False")&& !Eval("intFromUserId").ToString().Equals(UserInfo.GetCurrentUserInfo().UserId.ToString()))%>'
                                                                CommandArgument='<%# Eval("intNotificationId") %>' CommandName="MarkRead">Mark as Read</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>

                                        </ItemTemplate>
                                        <LayoutTemplate>

                                            <table id="Table5" runat="server" class="inventory-grid" width="100%" border="0" cellspacing="0"
                                                cellpadding="0">
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>

                                        </LayoutTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="dvpopupaddnotification" class="ajaxModal-popup inmodal" runat="server" visible="true">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title">Notification Details
                        </h4>
                    </div>
                    <div class="modal-body modal-body-overflow">
                        <div class="mail-box">
                            <asp:ListView runat="server" ID="lvmessages" DataKeyNames="intNotificationId" OnItemCommand="lvmessages_ItemCommand">
                                <EmptyDataTemplate>
                                    <table id="Table1" runat="server" style="">
                                        <tr>
                                            <td>
                                                <div class="user-chat-body">
                                                    No message found.
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr style="">
                                        <td class="p0">
                                            <table>
                                                <tr>
                                                    <td class="mail-ontact"><strong>
                                                        <asp:Label ID="intFromOrganizationIdLabel" runat="server" Text='<%# Eval("RecievedFrom") %>' /></strong></td>
                                                    <td class="mail-subject">
                                                        <asp:Label ID="vchNotificationTextLabel" runat="server" Text='<%# Eval("vchNotificationText") %>' />
                                                    </td>
                                                    <td id="Td1" style="width: 125px;" runat="server" class="text-right">
                                                        <div id="dvactions" runat="server" visible='<%#Eval("bitIsReaded").ToString().Equals("False")%>'>
                                                            <div>
                                                                <asp:LinkButton ID="LinkButton5" ToolTip="Mark As Read" CssClass="text-navy"
                                                                    Visible='<%#(Eval("bitIsReaded").ToString().Equals("False")&& !Eval("intFromUserId").ToString().Equals(UserInfo.GetCurrentUserInfo().UserId.ToString()))&&Eval("intFromOrganizationId").ToString().Equals(currentUserInfo.OrganizationId.ToString())%>'
                                                                    runat="server" CommandArgument='<%# Eval("intNotificationId") %>' CommandName="MarkRead"><i class="fa fa-check"></i> Mark as Read</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" class="text-right pt0">
                                                        <h6 id="Div5" class="m0" runat="server" visible='<%# Eval("bitIsReaded") %>'>
                                                            <i class="fa fa-eye"></i>Seen at: &nbsp;<%# Eval("dtmDateReaded")%>
                                                        </h6>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <LayoutTemplate>
                                    <table id="itemPlaceholderContainer" class="table table-hover table-mail mb0">
                                        <tr id="itemPlaceHolder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                            </asp:ListView>
                            <asp:HiddenField ID="hfToId" runat="server" Value="0" />
                            <asp:HiddenField ID="hfIsOrg" runat="server" Value="false" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="form-group text-left mb0">
                            <label>Send Message</label>
                            <asp:TextBox ID="txtNotification" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiretxtnotification" ControlToValidate="txtNotification"
                                ValidationGroup="notfication" runat="server" CssClass="custom-error" ErrorMessage="Please enter text"></asp:RequiredFieldValidator>
                        </div>
                        <asp:Button ID="btnSendNotification" runat="server" Text="Send" CssClass="btn btn-sm btn-primary" ValidationGroup="notfication" OnClick="btnSendNotification_Click" />
                        <asp:Button ID="btnClosePopup" runat="server" Text="Close" CssClass="btn btn-sm btn-white" OnClick="btnClosePopup_Click" />

                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
