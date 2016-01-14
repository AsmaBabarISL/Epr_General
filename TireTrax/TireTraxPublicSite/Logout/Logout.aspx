<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout_Logout" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="login_container">
<div class="login-outer_block">
  <div class="title_block">
  	<span>Thankyou to using EPRTS</span>
    <!--<div class="signup"> New Here? <a href="#">Signup</a> </div>-->
  </div>
  <div class="welcomeUser_outer"> 
    <p> Thank You for using EPRTS Online. </p>
    <br /><a style="color:Blue" href="adminLogin.aspx">Login</a>
  </div>
    
    <%--<asp:LinkButton ID="btnWelcome" runat="server" CssClass="Login-btn">Login</asp:LinkButton>--%>
    
    <br clear="all" />
  </div>
</div>
</asp:Content>

