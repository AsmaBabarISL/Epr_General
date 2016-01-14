<%@ Page Title="" Language="C#" MasterPageFile="~/skeleton.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Login_ForgotPassword" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="~/Scripts/jquery.min.js"></script>
    <asp:UpdatePanel ID="forgotpass" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="dvforgotpassword" runat="server" class="middle-box loginscreen animated fadeInDown">
                <h1 class="logo-name text-center">
                    <%--<img src="/images/NewTTlogo.png" width="300" />--%>
                    <img src="/img/main-Logo.png" style="height: 75px; width: auto; border-radius: 3px;" alt="Logo"  />
                </h1>
                <div class="login-box">
                    <h3 class="text-center">
                        <%= ResourceMgr.GetMessage("Forgot Password?")%>
                    </h3>
                    <p>To reset your password, enter the email address below.</p>
                    <div class="m-t" role="form">
                        <div class="form-group ">
                            <asp:TextBox ID="eMail" runat="server" CssClass="form-control text-center "></asp:TextBox>
                                
                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator1"
                                runat="server" ControlToValidate="eMail" Display="Dynamic" CssClass="custom-error"
                                ErrorMessage="Invalid Email"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="submit"></cc1:ResourceRegularExpressionValidator>
                            
                            <asp:RequiredFieldValidator ValidationGroup="submit" ID="eMailRequired" ControlToValidate="eMail" runat="server" 
                                ErrorMessage="Type Email" CssClass="custom-error"></asp:RequiredFieldValidator>
                            

                            <br />
                            
                            <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="custom-error"></asp:Label>
                            <asp:Label ID="lblStatus" ForeColor="Green" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:LinkButton ID="submit" ValidationGroup="submit" runat="server" CssClass="btn btn-primary block full-width m-b" OnClick="submit_Click">
                            <%= ResourceMgr.GetMessage("Submit")%></asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnBack" runat="server" CssClass="btn btn-sm btn-white btn-block" OnClick="lnkBtnBack_Click"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                    </div>
                </div>
            </div>

            <div id="dvthankyou" runat="server" visible="false" class="middle-box text-center loginscreen animated fadeInDown">
                <h1 class="logo-name">
                    <%--<img src="/images/NewTTlogo.png" width="300" />--%>
                    <img src="img/main-Logo.png" style="height: 75px; width: auto; border-radius: 3px;" alt="Logo"/>
                </h1>
                <div class="login-box">
                    <h3><%= ResourceMgr.GetMessage("Thank You")%></h3>
                    <p><%= ResourceMgr.GetMessage("An Email has been sent to confirm this password change. Please, Check your Email and approve this update.")%></p>
                    <div class="m-t">
                        <a class="btn btn-primary block full-width m-b" href="/default.aspx"><%= ResourceMgr.GetMessage("Go back to Home Page")%></a>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


