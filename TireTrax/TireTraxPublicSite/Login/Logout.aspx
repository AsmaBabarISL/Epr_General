<%@ Page Title="" Language="C#" MasterPageFile="~/skeleton.master" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Login_Logout" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="login_container">
<div class="login-outer_block">
  <div class="title_block">
  	<span>Thankyou to using Product Loop</span>
    <!--<div class="signup"> New Here? <a href="#">Signup</a> </div>-->
  </div>
  <div class="welcomeUser_outer"> 
    <p> Thank You for using Product Loop Online. </p>
    <br /><a style="color:Blue" href="adminLogin.aspx">Login</a>
  </div>
    
    <%--<asp:LinkButton ID="btnWelcome" runat="server" CssClass="Login-btn">Login</asp:LinkButton>--%>
    
    <br clear="all" />
  </div>
</div>
</asp:Content>

