<%@ Control Language="C#" AutoEventWireup="true" CodeFile="topheader.ascx.cs" Inherits="CommonControls_topheader" %>


<%--<%@ Register src="menuControl.ascx" tagname="menuControl" tagprefix="uc1" %>--%>
<%@ Register src="CountryFlagTop.ascx" tagname="editionControl" tagprefix="uc2" %>
<%@ Register src="menuheader.ascx" tagname="menuHeader" tagprefix="uc3" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<link href="/css/style.css" rel="stylesheet" type="text/css" />
<script src="/scripts/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $("document").ready(function () {
        $(".msg-icon").click(function () {
            $(".block-notify-abs").toggle(500);
        });
    });
</script>
<div class="header">
 
     <script type="text/javascript">
//         $(function () {
//             var thetitle = $('title').text();
//             $('.notif').click(function () { 

//                 var countNotif = parseInt($('.counter').text());
//                 var newcountNotif = ++countNotif;
//                 $('#msg-icon').removeClass('msg-icon').addClass('msg-iconh');
//                 $('.counter').text(newcountNotif).show();
//                 $('title').text('(' + newcountNotif + ') ' + thetitle);

//                 jQuery('<div/>', {
//                     id: 'notif-bot',
//                     class: 'notif-bot alert alert-info',
//                     text: 'You just got a notification!'
//                 }).appendTo('.notif-bot-cnt')
//                        .delay(5000)
//                        .fadeOut();

//             });

//             $('#msg-icon').click(function () {
//                 $('this').removeClass('msg-iconh').addClass('msg-icon');
//               //  $('.counter').text('0').hide();
//             //    $('.notif-bot').hide();
//                 $('title').text(thetitle);
//             })
//         });
     </script>
    <div class="header_right">
        <div class="admin_section">
       <%-- <div class="notif">notify me</div>--%>
    
       
        <div class="pd5">
            <span class="name-user"> Welcome <b><asp:Literal ID="litLoginName" runat="server"></asp:Literal> </b> </span>
           
            <%-- <a href="dashboardnotfications" > --%> <div id="msg-icon" class="msg-icon">
     		 <div class="counter"> 
                    <asp:Label id="lblnotficationcount" runat="server" Text="" ></asp:Label> 
                </div>
     	    </div> 
             
           <%-- </a>--%> | &nbsp; <a href="/Logout/Logout.aspx">Logout</a>
        </div>


            <%-- <asp:Timer runat="server" ID="TmrNotificationheader" 
            ontick="TmrNotification_Tick" Interval="300000" ></asp:Timer>--%>
           
            <div class="sm_txt"><asp:Literal ID="litLastLoginNotAvailable" runat="server" Text="First Time Login" Visible="false"></asp:Literal><asp:Literal ID="litLastLoginDate" runat="server">Last Login on <b>29 October, 2012</b> at <b>11:00am</b></asp:Literal></div>
              <div class="sm_txt">
            <asp:Label ID="lblcompanyname" runat="server" Text="Company Name" ></asp:Label>
            </div>
            <div class="sm_txt"> Your Role <b><asp:Label ID="lblSubRoleName" runat="server" ></asp:Label></b> <b>
                <asp:Label ID="lblRoleName" runat="server" ></asp:Label></b> </div>
                <div class="sm_txt"><img id="img" runat="server" src='/images/usa_flag.png' /> <span class="cnt_name"> <asp:Literal ID="litRole" runat="server"></asp:Literal> </span> </div>
         </div>
            
        <img src="/images/arrow_edge.png" class="arrow"> 
        <%--<div class="flag_block">
            
            <%--<uc2:editionControl ID="editionControl1" runat="server" />
        </div>--%>

    </div>


    <div class="logo_outer-left">
        <%--<div class="logo"  style="margin-left: 0px; margin-top: 0px;"> <a href="#">
        <img src="/images/txlogo.png" />
        </a> </div>--%>
        <div class="logo" > <a href="#">

            <img src="/images/NewTTlogo.png" width="300" runat="server" id="imgLogoDefault" visible="false"/>
            <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Gray" style=" visibility:hidden;">Edit</asp:HyperLink>
           <%-- <asp:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="imgLogoDefault" PopupControlID="LinkButton1"
             PopDelay="50">
            </asp:HoverMenuExtender>--%>
            
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="/logo-setting.aspx" style="color:#8A8A8A; visibility:hidden;">Edit</asp:LinkButton>
            
            <asp:Image ID="imgLogo" runat="server" ImageUrl="/images/txlogo.png" />
            
       <%-- <img src="/images/logo-stewardship.png" />--%>
        </a> 
        <span>
        <%--<asp:Label ID="lblName" runat="server" Text="Ontario Tire Stewardship" Font-Bold="true"  Font-Size="X-Large"></asp:Label>--%>
        </span>
        </div>
    </div>
    <div style="clear:both"> </div>
    <%--<uc1:menuControl ID="menuControl1" runat="server" />--%>
    <uc3:menuHeader ID="test2" runat="server" />
  
  <div class="chat-notifications-block-main block-notify-abs" style="display:none;">
                <%--  <div class="status-grid">
        <table class="status-sets" width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td>A/R</td>
            <td>Week-41</td>
            <td class="amount">$2,038</td>
          </tr>
          <tr>
            <td>A/R</td>
            <td>Total</td>
            <td class="amount">$3,093</td>
          </tr>
        </table>
      </div>--%>
                    <%--    <div class="stocks-title"> S&P 500 Stocks Furthest Above and Below their 50-200 Day Moving </div>--%>
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
                                      
                                  <div Visible='<%# !(Eval("intFromUserId").ToString().Equals("0") )%>'
                                     runat="server" >
                                     <b> <asp:Label ID="Label3" runat="server" Text='<%# Eval("RecievedFrom") %>' /> </b>
                                    <asp:Label ID="Label1" CssClass="rec-fromTitle" runat="server" Text='<%# Eval("vchNotificationText") %>' />
                                    </div>
                                       <div Visible='<%# !(Eval("intFromOrganizationId").ToString().Equals("0") )%>'
                                     runat="server" >
                                     : <asp:Label ID="Label2" runat="server" Text='<%# Eval("RecievedFrom") %>' />
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("vchNotificationText") %>' />
                                    </div>        
                                    </div>
                                    <div id="Div2" runat="server" visible='<%# Eval("bitIsReaded") %>'>
                                        Seen
                                    </div>
                                    <div class="notification-footer">
                                        <div id="Div3" runat="server" visible='<%#Eval("bitIsReaded").ToString().Equals("False")%>'>
                                     <%--   <asp:ImageButton ID="img" ImageUrl="/Images/approval_icon.png"  />--%>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="chat-icons-gray read-icon-gray" 
                                        ToolTip="Mark As Read"
                                            Visible='<%#(Eval("bitIsReaded").ToString().Equals("False")&& !Eval("intFromUserId").ToString().Equals(UserInfo.GetCurrentUserInfo().UserId.ToString()))%>'
                                            CommandArgument='<%# Eval("intNotificationId") %>' CommandName="MarkRead"
                                        >Mark as Read</asp:LinkButton>
                                    </div>
                                    </div>
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Visible='<%#Eval("isActive").ToString().Equals("true")%>'
                                        CommandArgument='<%# Eval("intNotificationId") %>' CommandName="Delete" Text="Delete" />
                                </td>
                            </tr>--%>
                        </ItemTemplate>
                        <LayoutTemplate>
                           
                                        <table id="Table5" runat="server" class="inventory-grid" width="100%" border="0" cellspacing="0"
                                            cellpadding="0">
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    
                        </LayoutTemplate>
                    </asp:ListView>

                    <a class="seeall-notification" href="dashboardnotfications" > See All </a>
            </div>

  </div>

