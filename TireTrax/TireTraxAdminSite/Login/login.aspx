<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Login_login" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title></title>
    <!-- Stylesheets Duplicate in Styles folder only for Login page - do not remove -->
    <link href="~/Styles/bootstrap.min.css" rel="stylesheet" media="screen"  />
    <link href="~/Styles/font-awesome.css" rel="stylesheet" media="screen"  />
    <link href="~/Styles/style.css" rel="stylesheet" media="screen" />
    <link href="~/Styles/eprAdmin-style.css" rel="stylesheet" media="screen"  />
    <!-- Stylesheets Duplicate in Styles folder only for Login page - do not remove -->



</head>
<body class="gray-bg">
    <form id="form1" runat="server">
        <div class="container">
            <div class="middle-box text-center loginscreen animated fadeInDown">
                <h1 class="logo-name text-center">
                    <img src="/images/main-Logo.png" width="75" />
                  
                </h1>
                <div class="login-box">
                    <h3>Admin Login</h3>
                    <p>Enter Username and Password to continue</p>
                    <div class="m-t">
                        <asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnAdminLogin">

                            
                            <div class="form-group">
                                <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="usernameRequired" ControlToValidate="txtusername"
                                    runat="server" ErrorText="*" CssClass="error-validator"></cc1:ResourceRequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="passwordRequired" ControlToValidate="txtpassword"
                                    runat="server" ErrorText="*" CssClass="error-validator"></cc1:ResourceRequiredFieldValidator>
                                 <asp:Label ID="lbl_ErrorMsg" runat="server" CssClass="custom-error"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label class="checkbox text-left m-l-md">
                                    <asp:CheckBox ID="chkBoxRemember" runat="server" />
                                    <%= ResourceMgr.GetMessage("Remember me")%>
                                </label>
                            </div>
                           
                            <asp:LinkButton ID="btnAdminLogin" runat="server" CssClass="btn btn-primary block full-width m-b" OnClick="btnAdminLogin_Click">
                                <%= ResourceMgr.GetMessage("Login")%></asp:LinkButton>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

            <!-- Mainly scripts -->
    

    </form>

</body>
<script src="../Scripts/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="/js/bootstrap.min.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text').blur(function () {
                var txtId = this.id;
                var el = $("#" + txtId)[0];

                var refinedVal = $(el).val().replace(/[<>]+/g, "");
                $($(el)[0]).val(refinedVal);

            });
            $('textarea').blur(function () {
                var textarea = $("#" + this.id)[0];
                var refinTextAreaVal = $(textarea).val().replace(/[<>]+/g, "");
                $($(textarea)[0]).val(refinTextAreaVal);
            });

        });
        $('input:password').blur(function () {
            var txtId = this.id;
            var el = $("#" + txtId)[0];

            var refinedVal = $(el).val().replace(/[<>]+/g, "");
            $($(el)[0]).val(refinedVal);

        });

    </script>

</html>
