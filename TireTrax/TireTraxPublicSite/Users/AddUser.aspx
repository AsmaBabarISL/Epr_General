<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="Users_AddUser" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function HideLabel(labelID) {
            setTimeout("HideLabelHelper('" + labelID + "');", 3000);
        }
        function HideLabelHelper(labelID) {
            document.getElementById(labelID).style.display = "none";
        }

        function ClientValidateUserRole(src, args) {
            if ($("input:checked", $("#<%=chkboxList.ClientID%>")).length > 0) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        function AtLeastOneContact_ClientValidate(source, args) {
            if (document.getElementById("<%= txtPrimaryContactCellPhone1.ClientID %>").value == "" ||
                document.getElementById("<%= txtPrimaryContactCellPhone2.ClientID %>").value == "" ||
                            document.getElementById("<%= txtPrimaryContactCellPhone3.ClientID %>").value == ""){
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function AtLeastOneContact_ClientValidate1(source, args) {

            if (document.getElementById("<%= txtphoneNumber1Add.ClientID %>").value == "" ||
                 document.getElementById("<%= txtphoneNumber2Add.ClientID %>").value == "" ||
                document.getElementById("<%= txtphoneNumber3Add.ClientID%>").value == "" ||
                document.getElementById("<%= txtphoneNumber1Add.ClientID %>").value == "000"||
                 document.getElementById("<%= txtphoneNumber2Add.ClientID %>").value == "000" ||
                            document.getElementById("<%= txtphoneNumber3Add.ClientID%>").value == "0000")  {

                            args.IsValid = false;
            }
            else {

                args.IsValid = true;
            }
        }

        <%--function AtLeastOneContact_ClientValidate2(source, args) {
            
            if (document.getElementById("<%= txtphoneNumber1Add.ClientID %>").value !== "" &&
                 document.getElementById("<%= txtphoneNumber2Add.ClientID %>").value == "" &&
                document.getElementById("<%= txtphoneNumber3Add.ClientID%>").value == "" ){
                
                args.IsValid = false;
            }
            else if (document.getElementById("<%= txtphoneNumber1Add.ClientID %>").value == "" &&
             document.getElementById("<%= txtphoneNumber2Add.ClientID %>").value !== "" &&
                document.getElementById("<%= txtphoneNumber3Add.ClientID%>").value == "") {
                    
                    args.IsValid = false;
            }
            else if (document.getElementById("<%= txtphoneNumber1Add.ClientID %>").value == "" &&
             document.getElementById("<%= txtphoneNumber2Add.ClientID %>").value == "" &&
                document.getElementById("<%= txtphoneNumber3Add.ClientID%>").value !== "") {
                    
                    args.IsValid = false;
                }
            else {
                
                args.IsValid = true;
            }
        }--%>

        $(function () {
            $("input[type=checkbox]", $("#<%=chkboxList.ClientID%>")).each(function () {
                $(this).bind("click", function () {
                    $("input:checked", $("#<%=chkboxList.ClientID%>")).not($(this)).removeAttr("checked");
                });
            });
        });

        function ClientValidateUserRoleForAdd(src, args) {
            if ($("input:checked", $("#<%=chkUserRole.ClientID%>")).length > 0) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        $(function () {
            $("input[type=checkbox]", $("#<%=chkUserRole.ClientID%>")).each(function () {
                $(this).bind("click", function () {
                    $("input:checked", $("#<%=chkUserRole.ClientID%>")).not($(this)).removeAttr("checked");
                });
            });
        });


        $(function () {
            $("input[type=checkbox]", $("#<%=chkboxList.ClientID%>")).each(function () {
                $(this).bind("click", function () {
                    $("input:checked", $("#<%=chkboxList.ClientID%>")).not(this).removeAttr("checked");
                });
            });

            SetPasswordField(false);
        });
        function SetPasswordField(obj) {

            if (obj) {
                $("#dvPassword").slideDown();
            }
            else {
                $("#dvPassword").slideUp();
            }
            ValidatorEnable(document.getElementById('<%=rfvPassword.ClientID%>'), obj);
            ValidatorEnable(document.getElementById('<%=rfvRepeatPassword.ClientID%>'), obj);
            if (obj) {
                $("#<%=rfvPassword.ClientID%>").hide();
                $("#<%=rfvRepeatPassword.ClientID%>").hide();
            }
        }
        function isNumberKey(evt) {
            if (typeof (evt) != "undefined") {
                var charCode = (evt.which) ? evt.which : evt.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="pnlEditUser" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5><%= ResourceMgr.GetMessage("Edit User")%> </h5>
                    </div>

                    <div class="ibox-content" style="display: block;">
                        <div role="form" class="row" id="">
                            <asp:Label ID="lblUpdateSuccesfully" runat="server" Visible="false" CssClass="custom-error"></asp:Label>
                            <div class="form-group col-md-12">
                                <label><%= ResourceMgr.GetMessage("Login:")%></label>
                                <asp:Label ID="lblLogin" runat="server"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group col-md-12">
                                        <%= ResourceMgr.GetMessage("Update Password:")%>
                                        <asp:CheckBox ID="chkUpdatePassword" runat="server" onclick="SetPasswordField(this.checked);" CssClass="m-l-md" />
                                    </div>
                                    <div id="dvPassword" style="display: none; clear: both;">

                                        <div class="form-group col-md-12">
                                            <label><%= ResourceMgr.GetMessage("Password:")%> </label>
                                            <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="rfvPassword" ValidationGroup="EditadminUserValidationGroup"
                                                CssClass="custom-error" runat="server" ErrorText="Enter Password" ControlToValidate="txtPassword"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        </div>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="false" DisplayMode="BulletList" ShowSummary="true" />

                                        <div class="form-group col-md-12">
                                            <label><%= ResourceMgr.GetMessage("Re Enter Password:")%></label>
                                            <asp:TextBox ID="txtRepeatPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="rfvRepeatPassword" ValidationGroup="EditadminUserValidationGroup"
                                                CssClass="custom-error" runat="server" ErrorText="Re Enter Password"
                                                ControlToValidate="txtRepeatPassword" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                            <cc1:ResourceCompareValidator ID="cvPassword" ValidationGroup="EditadminUserValidationGroup"
                                                CssClass="custom-error" runat="server" ErrorText="Password does not match" Display="Dynamic"
                                                ControlToValidate="txtRepeatPassword" ControlToCompare="txtPassword" Type="String"></cc1:ResourceCompareValidator>
                                        </div>

                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("First Name:")%> </label>
                                        <asp:TextBox ID="txtFirstName" runat="server" class="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfvFirstName" ValidationGroup="EditadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Enter First Name" ControlToValidate="txtFirstName"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Middle Name:")%></label>
                                        <asp:TextBox ID="txtMiddleName" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Last Name:")%> </label>
                                        <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfvLastName" ValidationGroup="EditadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Enter Last Name" ControlToValidate="txtLastName"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Email:")%></label>
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfvEmail" ValidationGroup="EditadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Enter Email" ControlToValidate="txtEmail"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        <cc1:ResourceRegularExpressionValidator ID="RegularExpressionValidator1" CssClass="custom-error"
                                             runat="server" ControlToValidate="txtEmail" ErrorText="Enter Valid Email" ValidationGroup="EditadminUserValidationGroup"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                    </div>
                                    <div class="form-group  col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Phone Number:")%></label>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPrimaryContactCellPhone1" runat="server" CssClass="form-control text-center"
                                                     MaxLength="3" WaterMarkText="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                <%--<cc1:ResourceRequiredFieldValidator ID="rfvExt" ControlToValidate="txtPrimaryContactCellPhone1" runat="server"
                                                    ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="EditadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>--%>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPrimaryContactCellPhone2" runat="server" CssClass="form-control text-center" MaxLength="3"
                                                     WaterMarkText="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                <%--<cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" ControlToValidate="txtPrimaryContactCellPhone2" runat="server"
                                                    ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="EditadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>--%>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPrimaryContactCellPhone3" runat="server" CssClass="form-control text-center" MaxLength="4"
                                                     WaterMarkText="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                <%--<cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator9" ControlToValidate="txtPrimaryContactCellPhone3" runat="server"
                                                    ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="EditadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>--%>
                                            </div>

                                            <div class="col-md-12">
                                                <br clear="all" />
                                                <asp:CustomValidator ID="AtLeastOneContact" runat="server"
                                                    ErrorMessage="Please Enter Phone number"
                                                    Display="Dynamic"
                                                    CssClass="custom-error"
                                                    ValidationGroup="EditadminUserValidationGroup"
                                                    ClientValidationFunction="AtLeastOneContact_ClientValidate" />
                                            </div>
                                            <br clear="all" />

                                        </div>

                                    </div>

                                </div>
                                <div class="col-md-6 col-md-offset-1">
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label><%= ResourceMgr.GetMessage("User Role:")%></label>
                                            <asp:RadioButtonList ID="chkboxList" runat="server" CellPadding="5"
                                                Style="width: 100%;" RepeatColumns="3" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList><br />
                                            <cc1:ResourceCustomValidator ID="rsvldcuschkboxList" runat="server" CssClass="custom-error"
                                                 Display="Dynamic" ClientValidationFunction="ClientValidateUserRole" ErrorText="Please select Role"
                                                 ValidationGroup="EditadminUserValidationGroup"></cc1:ResourceCustomValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-md-12 mb0">
                                <asp:LinkButton ID="lnkbtnAddInventory" runat="server" ValidationGroup="EditadminUserValidationGroup"
                                    CausesValidation="true" CssClass="btn btn-primary btn-sm font-bold" OnClick="lnkbtnAddInventory_Click"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnCancelInventory" runat="server" CausesValidation="false" OnClick="lnkbtnCancelInventory_Click"
                                    CssClass="btn btn-white btn-sm font-bold"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


    <%--This Script is associated with Add User Section--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtUserLogin.ClientID%>").blur(function () {
            showProgressBar();
            $.post('/Handlers/PublicLogin.ashx', { Login: $("#<%=txtUserLogin.ClientID%>").val() }
                , function (data) {
                    if (data == "true") {
                        var x = document.getElementById('<%=loginBar.ClientID%>');
                        x.style.visibility = "hidden";
                        var email = document.getElementById('<%=txtUserEmail.ClientID%>');
                        email.value = document.getElementById('<%=txtUserLogin.ClientID%>').value
                        if (email.value.length > 0) {

                            document.getElementById('fldEmail').style.display = "none";
                        }
                        document.getElementById('loginStatus').style.visibility = "hidden";
                        document.getElementById('<%=LinkButton2.ClientID%>').disabled = false;

                        //alert(data);
                    }
                    else if (data == "No Result") {

                        document.getElementById('loginStatus').style.visibility = "hidden";
                        var x = document.getElementById('<%=loginBar.ClientID%>');
                        x.style.visibility = "hidden";
                        document.getElementById('<%=LinkButton2.ClientID%>').disabled = true;
                    }
                    else {
                        var warning = document.getElementById('loginStatus');
                        warning.removeAttribute("style");
                        warning.style.color = "red";
                        var x = document.getElementById('<%=loginBar.ClientID%>');
                        x.style.visibility = "hidden";
                        document.getElementById('<%=LinkButton2.ClientID%>').disabled = true;
                        //alert("Else part " + data);

                    }
                });
        });
            $("#<%=txtUserLogin.ClientID%>").blur(function () {
                $("#<%=loginStatus.ClientID%>").hide();
    });
        });

        

    function showProgressBar() {
        var x = document.getElementById('<%=loginBar.ClientID%>');
        x.removeAttribute("style");
        document.getElementById('loginStatus').style.display = "none";
    }

       
    </script>



    <asp:Panel ID="pnlAddUser" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5><%= ResourceMgr.GetMessage("Add User")%> </h5>
                    </div>

                    <div class="ibox-content" style="display: block;">
                        <div role="form" class="row" id="">
                            <asp:Label ID="Label1" runat="server" Visible="false" CssClass="custom-error"></asp:Label>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Login:")%></label>
                                        <asp:TextBox ID="txtUserLogin" runat="server" class="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator2" ValidationGroup="AddadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Enter Login" ControlToValidate="txtUserLogin"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                  
                                        <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator2" CssClass="custom-error"
                                            runat="server" ControlToValidate="txtUserLogin"
                                            ErrorText="Enter Valid Login Email <br />" ValidationGroup="AddadminUserValidationGroup"
                                            ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>

                                        <asp:Label runat="server" id="loginStatus" Visible="false" CssClass="custom-error"></asp:Label>

                                        <div id="loginBar" class="loader-ico" runat="server" style="display: none">
                                            <img src="/Images/loading.gif" alt="" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("First Name:")%> </label>
                                        <asp:TextBox ID="txtUserFirstName" runat="server" class="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator5" ValidationGroup="AddadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Enter First Name" ControlToValidate="txtUserFirstName"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Middle Name:")%></label>
                                        <asp:TextBox ID="txtUserMiddleName" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Last Name:")%></label>
                                        <asp:TextBox ID="txtUserLastName" runat="server" class="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator6" ValidationGroup="AddadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Enter Last Name" ControlToValidate="txtUserLastName"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Password:")%> </label>
                                        <asp:TextBox ID="txtUserPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator3" ValidationGroup="AddadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Enter Password" ControlToValidate="txtUserPassword"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Re Enter Password:")%> </label>

                                        <asp:TextBox ID="txtUserRepeatPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator4" ValidationGroup="AddadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Re Enter Password"
                                            ControlToValidate="txtUserRepeatPassword" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        <cc1:ResourceCompareValidator ID="ResourceCompareValidator1" ValidationGroup="AddadminUserValidationGroup"
                                            CssClass="custom-error" runat="server" ErrorText="Password does not match" Display="Dynamic"
                                            ControlToValidate="txtUserRepeatPassword" ControlToCompare="txtUserPassword" Type="String"></cc1:ResourceCompareValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <div id="fldEmail" class="new_inventory-block">
                                            <label><%= ResourceMgr.GetMessage("Email:")%> </label>
                                            <asp:TextBox ID="txtUserEmail" runat="server" class="form-control"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator7" ValidationGroup="AddadminUserValidationGroup"
                                                CssClass="custom-error" runat="server" ErrorText="Enter Email" ControlToValidate="txtUserEmail"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator3" CssClass="custom-error"
                                                 runat="server" ControlToValidate="txtUserEmail"
                                                ErrorText="Enter Valid Email" ValidationGroup="AddadminUserValidationGroup"
                                                ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label><%= ResourceMgr.GetMessage("Phone Number:")%> </label>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtphoneNumber1Add" runat="server" CssClass="form-control text-center" MaxLength="3"
                                                     placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                <%--<cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator8" ControlToValidate="txtphoneNumber1Add" runat="server"
                                                    ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>--%>
                                            </div>
                                            <div class="col-md-4 pl0 pr0">
                                                <asp:TextBox ID="txtphoneNumber2Add" runat="server" CssClass="form-control text-center" MaxLength="3"
                                                     placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                <%--<cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator10" ControlToValidate="txtphoneNumber2Add" runat="server"
                                                    ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>--%>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtphoneNumber3Add" runat="server" CssClass="form-control text-center" MaxLength="4"
                                                     placeholder="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                <%--<cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator11" ControlToValidate="txtphoneNumber3Add" runat="server"
                                                    ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>--%>
                                            </div>
                                            <div class="col-md-12">
                                               
                                                <asp:CustomValidator ID="CustomValidator1" runat="server"
                                                    ErrorMessage="Please Enter Phone number"
                                                    Display="Dynamic"
                                                    CssClass="custom-error"
                                                    ValidationGroup="AddadminUserValidationGroup"
                                                    ClientValidationFunction="AtLeastOneContact_ClientValidate1" />
                                               <%-- <asp:CustomValidator ID="CustomValidator2" runat="server"
                                                    ErrorMessage="Please enter a valid Phone Number"
                                                    Display="Dynamic"
                                                    CssClass="custom-error"
                                                    ValidationGroup="AddadminUserValidationGroup"
                                                    ClientValidationFunction="AtLeastOneContact_ClientValidate2" />--%>
                                                <br clear="all" />
                                            </div>
                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-md-offset-1">
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label><%= ResourceMgr.GetMessage("User Role:")%> </label>
                                            <asp:RadioButtonList ID="chkUserRole" runat="server"
                                                CssClass="prod-type-chkbox" Style="width: 100%;" RepeatColumns="3" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator1" runat="server" CssClass="custom-error"
                                                Display="Dynamic" ClientValidationFunction="ClientValidateUserRoleForAdd" 
                                                ErrorText="Please select Role" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceCustomValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-md-12 mb0">

                                <asp:LinkButton ID="LinkButton2" Text="Add" runat="server" ValidationGroup="AddadminUserValidationGroup" CausesValidation="true"
                                     CssClass="btn btn-primary btn-sm font-bold" OnClick="lnkbtnAddUser_Click"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton1" Text="Cancel" runat="server" OnClick="lnkbtnCancelInventory_Click" CausesValidation="false"
                                    CssClass="btn btn-white  btn-sm font-bold"></asp:LinkButton>
                               

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    
</asp:Content>

