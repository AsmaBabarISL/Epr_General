<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true"
    CodeFile="dashboardNotifications.aspx.cs" Inherits="Dashboard_dashboardNotifications" %>

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
    <div id="Div1" runat="server" style=" background:url(/images/bg_shadow.png) repeat;width:100%;height:100%;position:fixed;
    z-index:999;top:0;left:0;z-index:99999;display:block;"> 
           <img src="/images/ajax-loader.gif" style="position:fixed; left:0; right:0; top:0; bottom:0; margin:auto;" />
            </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel id="pnlpopup" runat="server" >
    
     <ContentTemplate>


         <div class="row">
             <div class="col-lg-12 animated fadeInRight">
                 <div class="table-responsive">
                 <asp:ListView runat="server" ID="lvmessagesmaindetail" DataKeyNames="intNotificationId" OnItemCommand="lvmessagesmain_ItemCommand">
                <EmptyDataTemplate>
                    <table class="" id="ContentPlaceHolder1_gvAdminInventory">
                        <tr>
                            <td>
                             <div class="user-chat-body">
                                No Notifications found.
                                </div>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="padding:0;">
                            <table class="notification-table" style="width:100%;">
                                <tr>
                                    <td><h4> <asp:Label CssClass="font-noraml" ID="Label7" runat="server" Text='<%# Eval("RecievedFrom") %>' /> </h4></td>
                                    <td>
                                        <div class="pull-right tooltip-demo" id="Div4" runat="server" visible='<%#Eval("bitIsReaded").ToString().Equals("False")%>'>
                                            <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("intFromUserId") %>' Visible='<%# !(Eval("intFromUserId").ToString().Equals("0") )%>'
                                                CommandName="OpenPopupFromUser" runat="server" CssClass="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top">
                                                <i class="fa fa-reply"></i> Reply</asp:LinkButton>

                                            <asp:LinkButton ID="LinkButton6" CommandArgument='<%# Eval("intFromOrganizationId") %>' Visible='<%# !(Eval("intFromOrganizationId").ToString().Equals("0") )%>'
                                                CommandName="OpenPopupFromOrg" runat="server" CssClass="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" ToolTip="Reply">
                                                <i class="fa fa-reply"></i> Reply</asp:LinkButton>

                                            <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" ToolTip="Mark As Read"
                                                Visible='<%#(Eval("bitIsReaded").ToString().Equals("False")&& !Eval("intFromUserId").ToString().Equals(UserInfo.GetCurrentUserInfo().UserId.ToString()))%>'
                                                CommandArgument='<%# Eval("intNotificationId") %>' CommandName="MarkRead">
                                                <i class="fa fa-envelope"></i> Mark as Read</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td><h5> <asp:Label CssClass="font-noraml" ID="Label8" runat="server" Text='<%# Eval("vchNotificationText") %>' /> </h5></td>
                                    <td style="width:225px;"><h5> <span class="pull-right font-noraml" id="Address1" runat="server" Visible='<%#(Eval("bitIsReaded").ToString().Equals("True")) %>'> <i class="fa fa-eye"></i> Seen At <%# Eval("dtmDateReaded") %> </span> </h5>
                                        <div id="Div1" style="display:none;" runat="server" visible='<%# Eval("bitIsReaded") %>'> Seen </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table id="Table6" runat="server" class="" cellspacing="0" cellpadding="0" wrap="nowrap"
                        style="width: 100%; border-collapse: collapse;">
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
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
                                                     <asp:Label ID="intFromOrganizationIdLabel" runat="server" Text='<%# Eval("RecievedFrom") %>' />
                                                 </strong></td>
                                                 <td class="mail-subject">
                                                     <asp:Label ID="vchNotificationTextLabel" runat="server" Text='<%# Eval("vchNotificationText") %>' />
                                                 </td>
                                                 <td style="width: 125px;" class="text-right">
                                                     <div id="dvactions" runat="server" visible='<%#Eval("bitIsReaded").ToString().Equals("False")%>'>
                                                         <div>
                                                             <asp:LinkButton ID="LinkButton5" CssClass="chat-icons read-icon" ToolTip="Mark As Read"
                                                                 Visible='<%#(Eval("bitIsReaded").ToString().Equals("False")&& !Eval("intFromUserId").ToString().Equals(UserInfo.GetCurrentUserInfo().UserId.ToString()))%>'
                                                                 runat="server" CommandArgument='<%# Eval("intNotificationId") %>' CommandName="MarkRead">Mark as Read</asp:LinkButton>
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
                         <asp:RequiredFieldValidator ID="requiretxtnotification" ControlToValidate="txtNotification" ValidationGroup="notfication" runat="server"
                             CssClass="custom-error" ErrorMessage="Please enter text"></asp:RequiredFieldValidator>
                     </div>
                     <asp:Button ID="btnSendNotification" runat="server" Text="Send" CssClass="btn btn-sm btn-primary" ValidationGroup="notfication" OnClick="btnSendNotification_Click" />
                     <asp:Button ID="btnClosePopup" runat="server" Text="Close" CssClass="btn btn-sm btn-white" OnClick="btnClosePopup_Click" />
                 </div>
             </div>
         </div>


        <div class="right-contain-outer" style="display:none;">
            <div class="chat-notifications-block-main">
                    <div class="notification-title">
                        Notifications
                    </div>
                    <asp:ListView runat="server" ID="lvmessagesmain" DataKeyNames="intNotificationId"
                        OnItemCommand="lvmessagesmain_ItemCommand">
                        <EmptyDataTemplate>
                            <table id="Table3" runat="server" style="">
                                <tr>
                                    <td>
                                     <div class="user-chat-body">
                                        No Notifications found.
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <div class="notification-content">
                                        <asp:Label ID="Label3" CssClass="pull-left" runat="server" Text='<%# Eval("RecievedFrom") %>' />
                                        <asp:Label ID="Label4" CssClass="rec-fromTitle pull-left" runat="server" Text='<%# Eval("vchNotificationText") %>' />
                                    </div>
                                    <div id="Div2" runat="server" visible='<%# Eval("bitIsReaded") %>'>
                                        Seen
                                    </div>
                                    <div class="notification-footer">
                                        <div id="Div3" runat="server" visible='<%#Eval("bitIsReaded").ToString().Equals("False")%>'>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="chat-icons read-icon" 
                                        ToolTip="Mark As Read"
                                            Visible='<%#(Eval("bitIsReaded").ToString().Equals("False")&& !Eval("intFromUserId").ToString().Equals(UserInfo.GetCurrentUserInfo().UserId.ToString()))%>'
                                            CommandArgument='<%# Eval("intNotificationId") %>' CommandName="MarkRead"
                                        >Mark as Read</asp:LinkButton>
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
      </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>
