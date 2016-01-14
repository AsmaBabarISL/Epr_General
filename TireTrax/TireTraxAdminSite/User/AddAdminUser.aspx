<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="AddAdminUser.aspx.cs" Inherits="User_AddAdminUser" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtLogin.ClientID%>").blur(function () {

          showProgressBar();
          $.post('/Handlers/Login.ashx', { Login: $("#<%=txtLogin.ClientID%>").val() }
                , function (data) {
                    if (data == "true") {
                        var x = document.getElementById('<%=loginBar.ClientID%>');
                        x.style.visibility = "hidden";
                        var email = document.getElementById('<%=txtEmail.ClientID%>');
                        email.value = document.getElementById('<%=txtLogin.ClientID%>').value
                        if (email.value.length > 0) {

                            document.getElementById('fldEmail').style.display = "none";
                        }
                        document.getElementById('loginStatus').style.visibility = "hidden";
                        document.getElementById('<%=lnkbtnAddInventory.ClientID%>').disabled = false;

                        //alert(data);
                    }
                    else if (data == "No Result") {

                        document.getElementById('loginStatus').style.visibility = "hidden";
                        var x = document.getElementById('<%=loginBar.ClientID%>');
                        x.style.visibility = "hidden";
                        document.getElementById('<%=lnkbtnAddInventory.ClientID%>').disabled = true;
                    }
                    else {
                        var warning = document.getElementById('loginStatus');
                        warning.removeAttribute("style");
                        warning.style.color = "red";
                        var x = document.getElementById('<%=loginBar.ClientID%>');
                        x.style.visibility = "hidden";
                        document.getElementById('<%=lnkbtnAddInventory.ClientID%>').disabled = true;
                        //alert("Else part " + data);

                    }
                });
            });
  });

    function showProgressBar() {
        var x = document.getElementById('<%=loginBar.ClientID%>');
        x.removeAttribute("style");
        document.getElementById('loginStatus').style.display = "none";
        //alert("Progress Bar activated");
    }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="LoginNameExists" class="error_message" style="width: 800px; display: none;">
        This Login is not available. Please change Login and try again.
    </div>




    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>
                        <asp:Label ID="lblAddUser" runat="server"
                            Text="Add Admin User"></asp:Label>
                    </h5>
                </div>
                <div class="ibox-content" style="display: block;">
                    <div role="form" class="row" id="">

                        <div class="row">
                            <div class="col-md-4 col-lg-4 col-sm-6">
                                <div class="form-group col-md-12">
                                    <label><%= ResourceMgr.GetMessage("Login:")%></label>
                                    <asp:TextBox ID="txtLogin" runat="server" class="form-control"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="rfvLogin" ValidationGroup="AddUserG"
                                        CssClass="custom-error" runat="server" ErrorText="Enter Login" ControlToValidate="txtLogin" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    <cc1:ResourceRegularExpressionValidator ID="regxvLogin" runat="server" ControlToValidate="txtLogin" CssClass="custom-error"
                                        ErrorText="Enter Valid Login (example: abc@xyz.com)" ValidationGroup="AddUserG"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                    <div id="loginStatus" style="display: none">
                                        <p class="custom-error">Login already exists, Please Choose another Login</p>
                                    </div>
                                    <div id="loginBar" runat="server" style="display: none">
                                        <img src="/Images/loading.gif" alt="" />
                                    </div>
                                </div>
                                <div class="form-group col-md-12 ">
                                    <label><%= ResourceMgr.GetMessage("Password:")%></label>
                                    <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="rfvPassword" ValidationGroup="AddUserG"
                                        CssClass="custom-error" runat="server" ErrorText="Enter Password" ControlToValidate="txtPassword"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-12">
                                    <label><%= ResourceMgr.GetMessage("Re Enter Password:")%></label>
                                    <asp:TextBox ID="txtRepeatPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="rfvRepeatPassword" ValidationGroup="AddUserG"
                                        CssClass="custom-error" runat="server" ErrorText="Enter Repeat Password" ControlToValidate="txtRepeatPassword"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    <cc1:ResourceCompareValidator ID="cvPassword" ValidationGroup="AddUserG"
                                        CssClass="custom-error" runat="server" ErrorText="Password does not match" Display="Dynamic"
                                        ControlToValidate="txtRepeatPassword" ControlToCompare="txtPassword" Type="String"></cc1:ResourceCompareValidator>
                                </div>
                                <div class="form-group col-md-12">
                                    <label><%= ResourceMgr.GetMessage("First Name:")%></label>
                                    <asp:TextBox ID="txtFirstName" runat="server" class="form-control"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="rfvFirstName" ValidationGroup="AddUserG"
                                        CssClass="custom-error" runat="server" ErrorText="Enter First Name" ControlToValidate="txtFirstName"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-12">
                                    <label><%= ResourceMgr.GetMessage("Middle Name:")%></label>
                                    <asp:TextBox ID="txtMiddleName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-12">
                                    <label><%= ResourceMgr.GetMessage("Last Name:")%></label>


                                    <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="rfvLastName" ValidationGroup="AddUserG"
                                        CssClass="custom-error" runat="server" ErrorText="Enter Last Name" ControlToValidate="txtLastName"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>

                                </div>
                                <div id="fldEmail" class="form-group col-md-12">
                                    <label><%= ResourceMgr.GetMessage("Email:")%></label>
                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-12">
                                    <label><%= ResourceMgr.GetMessage("Phone Number:")%></label>
                                    <asp:TextBox ID="txtPhoneNumber" runat="server" class="form-control"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="rfvExt" ControlToValidate="txtPhoneNumber"
                                        runat="server" ErrorText="Please enter Phone" CssClass="custom-error" Display="Dynamic"
                                        ValidationGroup="AddUserG"></cc1:ResourceRequiredFieldValidator>
                                    <cc1:ResourceRegularExpressionValidator ID="revPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber"
                                        ErrorText="Please enter a valid Phone Number. e.g; 000-000-0000" CssClass="custom-error"
                                        ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$" ValidationGroup="AddUserG"
                                        Display="Dynamic"></cc1:ResourceRegularExpressionValidator>

                                </div>


                                <div class="form-group col-md-12 mb0">
                                    <br />
                                    <asp:Button ID="lnkbtnAddInventory" runat="server" ValidationGroup="AddUserG" CssClass="btn btn-primary btn-sm font-bold"
                                        OnClick="lnkbtnAddInventory_Click" Text="Add" />
                                    <asp:Button ID="lnkbtnCancelInventory" runat="server" CssClass="btn btn-white btn-sm font-bold"
                                        Text="Cancel" OnClick="lnkbtnCancelInventory_Click" />
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

