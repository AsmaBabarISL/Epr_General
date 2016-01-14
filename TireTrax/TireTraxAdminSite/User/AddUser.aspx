<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true"
    CodeFile="AddUser.aspx.cs" Inherits="User_AddUser" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            if (typeof (evt) != "undefined") {
                var charCode = (evt.which) ? evt.which : evt.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }
     
        function ClientValidateUserRole(src, args) {
            if ($("input:checked", $("#<%=chkboxList.ClientID%>")).length > 0) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        $(function () {
            $("input[type=checkbox]", $("#<%=chkboxList.ClientID%>")).each(function () {
                $(this).bind("click", function () {
                    $("input:checked", $("#<%=chkboxList.ClientID%>")).not($(this)).removeAttr("checked");
                });
            });
        });

//        function ShowLoginErrorMessage() {
//            $("#LoginNameExists").show();
//        }

//        function HideLoginErrorMessage() {
//            $("#LoginNameExists").hide();
//        }

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

    <div id="LoginNameExists" class="custom-error" style="width: 800px; display: none;">
        This Login is not available. Please change Login and try again.</div>

    
        <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Add User")%> </h5>
                            </div>
            <div class="ibox-content" style="display: block;">
                                <div role="form" class="row" id="">


                                     <div class="row">
                                        <div class="col-md-3">

                                            <div class="form-group col-md-12">
                                                <label><%= ResourceMgr.GetMessage("Login:")%></label>
                                                <asp:TextBox ID="txtLogin" runat="server" class="form-control"></asp:TextBox>

                                                <cc1:ResourceRequiredFieldValidator ID="rfvLogin" ValidationGroup="AddUserG"
                        CssClass="custom-error" runat="server" ErrorText="Enter Login" ControlToValidate="txtLogin"
                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                    <cc1:ResourceRegularExpressionValidator ID="regxvLogin" runat="server" ControlToValidate="txtLogin" CssClass="custom-error"
                       ErrorText="Enter Valid Login (example: abc@xyz.com)"  ValidationGroup="AddUserG"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                        <p id="loginStatus" style="display: none" class="custom-error">Login already exists, Please Choose another Login</p>


                                           <div id="loginBar" class="loader-ico" runat="server" style="display: none"> <img src="/Images/loading.gif" alt="" /> </div>     

                                            </div>

                                            <div class="form-group col-md-12">
                                                <label><%= ResourceMgr.GetMessage("First Name:")%> </label>
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

                                            <div class="form-group col-md-12">
                                                <label><%= ResourceMgr.GetMessage("Password:")%> </label>
                                                <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                                <cc1:ResourceRequiredFieldValidator ID="rfvPassword" ValidationGroup="AddUserG"
                                                CssClass="custom-error" runat="server" ErrorText="Enter Password" ControlToValidate="txtPassword"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-md-12">
                                                <label><%= ResourceMgr.GetMessage("Re Enter Password:")%> </label>

                                                <asp:TextBox ID="txtRepeatPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                               <cc1:ResourceRequiredFieldValidator ID="rfvRepeatPassword" ValidationGroup="AddUserG"
                                                    CssClass="custom-error" runat="server" ErrorText="Enter Repeat Password" ControlToValidate="txtRepeatPassword"
                                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                <cc1:ResourceCompareValidator ID="cvPassword" ValidationGroup="AddUserG"
                                                    CssClass="custom-error" runat="server" ErrorText="Password does not match" Display="Dynamic"
                                                    ControlToValidate="txtRepeatPassword" ControlToCompare="txtPassword" Type="String"></cc1:ResourceCompareValidator>
                                            </div>

                                            <div class="form-group col-md-12">
                                                <div id="fldEmail" class="new_inventory-block">
                                                    <label><%= ResourceMgr.GetMessage("Email:")%> </label>
                                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                                                   <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator5" ValidationGroup="AddUserG"
                                                    CssClass="custom-error" runat="server" ErrorText="Enter Email" ControlToValidate="txtEmail"
                                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                  <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" CssClass="custom-error"
                                                   ErrorText="Enter Valid Login (example: abc@xyz.com)"  ValidationGroup="AddUserG"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-md-12">
                                                <label><%= ResourceMgr.GetMessage("Phone Number:")%> </label>

                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtPrimaryContactCellPhone1" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" 
                                                            onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                       <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator2" ValidationGroup="AddUserG"
                                                            CssClass="custom-error" runat="server" ErrorText="*" ControlToValidate="txtPrimaryContactCellPhone1"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        <cc1:ResourceRequiredFieldValidator  ID="rfvExt" ControlToValidate="txtPrimaryContactCellPhone1" runat="server"
                                                        ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>                 
                                                    </div>
                                                    <div class="col-md-4 pl0 pr0">
                                                        <asp:TextBox ID="txtPrimaryContactCellPhone2" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" 
                                                            onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator3" ValidationGroup="AddUserG"
                                                            CssClass="custom-error" runat="server" ErrorText="*" ControlToValidate="txtPrimaryContactCellPhone2"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                         <cc1:ResourceRequiredFieldValidator  ID="ResourceRequiredFieldValidator1" ControlToValidate="txtPrimaryContactCellPhone2" runat="server"
                                                        ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>                 
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtPrimaryContactCellPhone3" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="0000" 
                                                            onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator4" ValidationGroup="AddUserG"
                                                            CssClass="custom-error" runat="server" ErrorText="*" ControlToValidate="txtPrimaryContactCellPhone3"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRequiredFieldValidator  ID="ResourceRequiredFieldValidator9" ControlToValidate="txtPrimaryContactCellPhone3" runat="server"
                                                        ErrorText="*" CssClass="custom-error" Display="Dynamic" ValidationGroup="AddadminUserValidationGroup"></cc1:ResourceRequiredFieldValidator>                 
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                            <div class="col-md-6 col-md-offset-1">
                                            <div class="row">
                                                <div class="form-group col-md-12">
                                                <label><%= ResourceMgr.GetMessage("User Role:")%> </label>
                                                <asp:CheckBoxList ID="chkboxList" runat="server"
                                                    CssClass="" Style="width:100%;" RepeatColumns="3" RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                                <cc1:ResourceCustomValidator ID="rsvldcuschkboxList" runat="server" CssClass="custom-error" 
                                                    Display="Dynamic" ClientValidationFunction="ClientValidateUserRole" 
                                                    ErrorText="Select atleast one Role" ValidationGroup="AddUserG">
                                                </cc1:ResourceCustomValidator>
                                                    </div>
                                                    </div>
                                                </div>
                                     </div>

                                    <div class="form-group col-md-12 mb0">

                                        <asp:Button ID="lnkbtnAddInventory" Text="Add" runat="server" ValidationGroup="AddUserG"
                                            CausesValidation="true" CssClass="btn btn-primary btn-sm font-bold" OnClick="lnkbtnAddInventory_Click"></asp:Button>
                                        <asp:Button ID="lnkbtnCancelInventory" Text="Cancel" runat="server" CausesValidation="false" OnClick="lnkbtnCancelInventory_Click"
                                            CssClass="btn btn-white  btn-sm font-bold"></asp:Button>

                                    </div>





</div>
                                 </div>
                             </div>
                         </div>
                 </div>
</asp:Content>
