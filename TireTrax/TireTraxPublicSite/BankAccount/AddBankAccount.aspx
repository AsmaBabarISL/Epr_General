<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="AddBankAccount.aspx.cs" Inherits="BankAccount_AddBankAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.mask.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.mask.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowLoginErrorMessage() {
            $("#LoginNameExists").show();
        }

        function HideLoginErrorMessage() {
            $("#LoginNameExists").hide();
        }

        $(function () { ApplyWaterMarkOnTextBoxes(); });

        function ApplyWaterMarkOnTextBoxes() {
            $("input[WaterMarkText],textarea[WaterMarkText]").each(function () {
                var $this = $(this);
                if ($this.attr("WaterMarkText") != null) {
                    var maxLen = $this.attr("MaxLength");

                    if (maxLen) {
                        $this.attr("tempMaxLength", maxLen);
                        $this.removeAttr("MaxLength");
                    }

                    $this.blur(function () {
                        if ($this.val() == '') {
                            var maxLen = $this.attr("MaxLength");
                            if (maxLen) {
                                $this.attr("tempMaxLength", maxLen);
                                $this.removeAttr("MaxLength");
                            }
                            $this.val($this.attr("WaterMarkText")).addClass("WaterMark");
                        }
                    });
                    $this.focus(function () {
                        if ($this.val() == $this.attr("WaterMarkText")) {
                            if ($this.attr("tempMaxLength")) {
                                $this.attr("MaxLength", $this.attr("tempMaxLength"));
                            }
                            $this.val('').removeClass("WaterMark");
                        }
                    });

                    if ($(this).val() == '' || $(this).val() == $(this).attr("WaterMarkText")) {
                        $this.val($this.attr("WaterMarkText")).addClass("WaterMark");
                    }
                }
            });
        }


        function isAlphaNumeric(evt) {
            var key;
            if (window.event) {
                key = window.event.keyCode; //IE
            }
            else {
                key = e.which; //firefox
            }

            if (!((key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122))) {
                // alert(" You can enter only characters a to z,A to Z,0 to 9 ");
                return false;
            }
            else return true;
        }

        function isNumeric(evt) {
            var key;
            if (window.event) {
                key = window.event.keyCode; //IE
            }
            else {
                key = e.which; //firefox
            }

            if (!((key >= 48 && key <= 57))) {
                // alert(" You can enter only characters a to z,A to Z,0 to 9 ");
                return false;
            }
            else return true;
        }

        function routingRangeValidator(sender, args) {
            args.IsValid = false;
            var val = $('#<%=txtRoutingNum.ClientID %>').val();
            if (val.length >= 8 && val.length <= 9) {
                args.IsValid = true;
                return true;
            }
            return false;
        }

        function accnumRangeValidator(sender, args) {
            args.IsValid = false;
            var val = $('#<%=txtAccountNum.ClientID %>').val();
        if (val.length >= 14 && val.length <= 16) {
            args.IsValid = true;
            return true;
        }
        return false;
        }

        function noScriptInValue(sender, args) {
            args.IsValid = false;
            var val = document.getElementById(sender.controltovalidate).value;
            
            if (!(val.indexOf("script") > -1)) {
                args.IsValid = true;
                return true;
            }
            return false;

        }

        jQuery(document).ready(function ($) {
            $("#<%=txtPhoneNum.ClientID%>").mask("999-999-9999");

        });

        function fadeOut() {
            $(".custom-absolute-alert").delay(3000).fadeOut(300);
            $(".custom-absolute-alert").appendTo("form");
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/Images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/Images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>
                        <asp:Label ID="lblId1" runat="server"><%= ResourceMgr.GetMessage("Account Information")%></asp:Label></h5>
                </div>
                <div class="ibox-content">
                    <div class="row">

                            <div class="form-group col-md-4 col-lg-3">
                                <label>
                                    <asp:Label runat="server" ID="lblId2"><%= ResourceMgr.GetMessage("Account Type")%></asp:Label></label>
                                <asp:DropDownList ID="ddlIAcountType" runat="server" CssClass="form-control"></asp:DropDownList>
                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" ValidationGroup="AddBankInfo"
                                    InitialValue="0" CssClass="custom-error" runat="server" ErrorText="Select Account Type" ControlToValidate="ddlIAcountType"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label>
                                    <asp:Label runat="server" ID="lblId3"><%= ResourceMgr.GetMessage("Account Title")%></asp:Label></label>
                                <asp:TextBox ID="txtAccountTitle" runat="server" class="form-control" onpaste="return false" oncut="return false"
                                    oncopy="return false" autocomplete="off"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="rfvAcount" ValidationGroup="AddBankInfo"
                                    CssClass="custom-error" runat="server" ErrorText="Enter Account Title" ControlToValidate="txtAccountTitle"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator2" 
                                    ControlToValidate="txtAccountTitle" ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" 
                                    ValidationGroup="AddBankInfo" Display="Dynamic" CssClass="custom-error"
                                    runat="server"></cc1:ResourceRegularExpressionValidator>
                                
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label>
                                    <asp:Label runat="server" ID="lblId4"><%= ResourceMgr.GetMessage("Account#")%></asp:Label></label>
                                <asp:TextBox ID="txtAccountNum" runat="server" class="form-control" MaxLength="16" onpaste="return false" oncut="return false"
                                    oncopy="return false" autocomplete="off" onkeypress="return isNumeric(event);"></asp:TextBox>
                                <asp:Label ID="lblerrorbankacc" runat="server" CssClass="alert-danger custom-absolute-alert" Visible="false"></asp:Label>
                                <cc1:ResourceRequiredFieldValidator ID="rfvAcountno" ValidationGroup="AddBankInfo"
                                    CssClass="custom-error" runat="server" ErrorText="Enter Acount#" ControlToValidate="txtAccountNum"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>

                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator3" runat="server" 
                                    ControlToValidate="txtAccountNum" ErrorText="Length must be from 14 to 16" ValidationGroup="AddBankInfo"
                                     CssClass="custom-error" Display="Dynamic" ValidationExpression="^(0|[1-9][0-9]{13,16})$"></cc1:ResourceRegularExpressionValidator>

                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label>
                                    <asp:Label runat="server" ID="lblId5"><%= ResourceMgr.GetMessage("Routing#")%></asp:Label></label>
                                <asp:TextBox ID="txtRoutingNum" runat="server" class="form-control" MaxLength="9" onpaste="return false" oncut="return false"
                                    oncopy="return false" autocomplete="off" onkeypress="return isNumeric(event);"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="rfvRouting" ValidationGroup="AddBankInfo"
                                    CssClass="custom-error" runat="server" ErrorText="Enter Routing#" ControlToValidate="txtRoutingNum"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                <cc1:ResourceCustomValidator ID="CustomValidator33" runat="server" CssClass="custom-error" ErrorMessage="Routing number should be 8-9 digit code"
                                    ControlToValidate="txtRoutingNum" ClientValidationFunction="routingRangeValidator" Display="Dynamic" ValidationGroup="AddBankInfo"
                                    Font-Size="X-Small"></cc1:ResourceCustomValidator>
                            </div>
                            
                       
                            <div class="form-group col-md-4 col-lg-3">
                                <label>
                                    <asp:Label runat="server" ID="lblId6"><%= ResourceMgr.GetMessage("Bank Name")%></asp:Label></label>
                                <asp:TextBox ID="txtBankName" runat="server" class="form-control" onpaste="return false" oncut="return false"
                                    oncopy="return false" autocomplete="off"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="rfvBank" ValidationGroup="AddBankInfo"
                                    CssClass="custom-error" runat="server" ErrorText="Enter Bank Name" ControlToValidate="txtBankName"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator4" ControlToValidate="txtBankName" 
                                    ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" ValidationGroup="AddBankInfo" Display="Dynamic" CssClass="custom-error"
                                    runat="server"></cc1:ResourceRegularExpressionValidator>
                               
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label>
                                    <asp:Label runat="server" ID="lblId7"><%= ResourceMgr.GetMessage("Branch")%></asp:Label></label>
                                <asp:TextBox ID="txtBranch" runat="server" class="form-control" onpaste="return false" oncut="return false"
                                    oncopy="return false" autocomplete="off"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="rfvBranch" ValidationGroup="AddBankInfo"
                                    CssClass="custom-error" runat="server" ErrorText="Enter Branch" ControlToValidate="txtBranch"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label>
                                    <asp:Label runat="server" ID="lblId8"><%= ResourceMgr.GetMessage("IBAN#")%></asp:Label></label>
                                <asp:TextBox ID="txtIBANNum" runat="server" class="form-control" onpaste="return false" oncut="return false"
                                    oncopy="return false" autocomplete="off" onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="rfvIBAN" ValidationGroup="AddBankInfo"
                                    CssClass="custom-error" runat="server" ErrorText="Enter IBAN#" ControlToValidate="txtIBANNum"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label>
                                    <asp:Label runat="server" ID="lblId9"><%= ResourceMgr.GetMessage("Swift Code")%></asp:Label></label>
                                <asp:TextBox ID="txtSwiftCode" runat="server" class="form-control" onpaste="return false" oncut="return false"
                                    oncopy="return false" autocomplete="off" onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="rfvSwiftCode" ValidationGroup="AddBankInfo"
                                    CssClass="custom-error" runat="server" ErrorText="Enter Swift Code" ControlToValidate="txtSwiftCode"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            </div>
                        <div class="form-group col-md-4 col-lg-3 mb0">
                                <asp:CheckBox ID="chkboxetf" runat="server" />
                                <label>
                                    <asp:Label runat="server" ID="lblchk" Text="ETF"></asp:Label></label>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>
                        <asp:Label ID="lblId16" runat="server"><%= ResourceMgr.GetMessage("Bank Address Information")%></asp:Label></h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="form-group col-md-4 col-lg-3">
                            <label>
                                <asp:Label runat="server" ID="lblId10"><%= ResourceMgr.GetMessage("Street Number")%></asp:Label></label>
                            <asp:TextBox ID="txtStreetNum" runat="server" class="form-control" onpaste="return false" oncut="return false"
                                    oncopy="return false" autocomplete="off" onkeypress="return isNumeric(event);"></asp:TextBox>
                            <cc1:ResourceRequiredFieldValidator ID="rfvStreetNumer" ValidationGroup="AddBankInfo"
                                CssClass="custom-error" runat="server" ErrorText="Enter Street Number" ControlToValidate="txtStreetNum"
                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            

                        </div>
                        <div class="form-group col-md-4 col-lg-3">
                            <label>
                                <asp:Label runat="server" ID="lblId11"><%= ResourceMgr.GetMessage("Street Name")%></asp:Label></label>
                            <asp:TextBox ID="txtStreetName" runat="server" class="form-control" onpaste="return false" oncut="return false"
                                    oncopy="return false" autocomplete="off"></asp:TextBox>
                            <cc1:ResourceRequiredFieldValidator ID="rfvName" ValidationGroup="AddBankInfo"
                                CssClass="custom-error" runat="server" ErrorText="Enter Street Name" ControlToValidate="txtStreetName"
                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator5" ControlToValidate="txtStreetName"
                                ErrorText="Enter Only text" ValidationExpression="^[a-zA-Z ]*$" ValidationGroup="AddBankInfo" Display="Dynamic" CssClass="custom-error"
                                runat="server"></cc1:ResourceRegularExpressionValidator>
                            
                        </div>
                        <div class="form-group col-md-4 col-lg-3">
                            <label>
                                <asp:Label runat="server" ID="lblId15"><%= ResourceMgr.GetMessage("Phone#")%></asp:Label></label>
                            <asp:TextBox ID="txtPhoneNum" runat="server" MaxLength="20" class="form-control" onpaste="return false" oncut="return false"
                                    oncopy="return false" autocomplete="off" placeholder="000-000-0000"></asp:TextBox>
                            <cc1:ResourceRequiredFieldValidator ID="rfvPhoneNum"
                                CssClass="custom-error" runat="server" ErrorText="Enter Phone#" ControlToValidate="txtPhoneNum" ValidationGroup='AddBankInfo'
                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            <cc1:ResourceRegularExpressionValidator ID="revPhoneNumber" runat="server" ControlToValidate="txtPhoneNum"
                                 ErrorText="Please enter a valid Phone Number. e.g; 000-000-0000" CssClass="custom-error"
                                 ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$"
                                 ValidationGroup="AddBankInfo" Display="Dynamic"></cc1:ResourceRegularExpressionValidator>

                            <%--<cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator1" 
                                ErrorText="Enter Valid Phone# e.g (000-000-0000)" CssClass="custom-error"
                                ValidationExpression="^([\(]{1}[0-9]{3}[\)]{1}[\.| |\-]{0,1}|^[0-9]{3}[\.|\-| ]?)?[0-9]{3}(\.|\-| )?[0-9]{4}$" 
                                Display="Dynamic" ControlToValidate="txtPhoneNum" ValidationGroup='AddBankInfo'
                                runat="server"></cc1:ResourceRegularExpressionValidator>--%>
                        </div>
                        <div class="form-group col-md-4 col-lg-3">
                            <label>
                                <asp:Label runat="server" ID="lblId13"><%= ResourceMgr.GetMessage("City")%></asp:Label></label>
                            <asp:TextBox ID="txtCity" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                            <cc1:ResourceRequiredFieldValidator ID="rfvCity" ValidationGroup="AddBankInfo"
                                CssClass="custom-error" runat="server" ErrorText="Enter City" ControlToValidate="txtCity"
                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-4 col-lg-3">
                            <label>
                                <asp:Label runat="server" ID="lblId14"><%= ResourceMgr.GetMessage("State")%></asp:Label></label>
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator11" ValidationGroup="AddBankInfo"
                                InitialValue="0" CssClass="custom-error" runat="server" ErrorText="Select State" ControlToValidate="ddlState"
                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-4 col-lg-3">
                            <label>
                                <asp:Label runat="server" ID="lblId12"><%= ResourceMgr.GetMessage("Zip Code")%></asp:Label></label>
                            <asp:TextBox ID="txtZipCode" runat="server" MaxLength="10" AutoPostBack="true" OnTextChanged="txtLocationZipCode_TextChanged" 
                                class="form-control"></asp:TextBox>
                            
                            <asp:HiddenField ID="hdnLocationZipCodeID" runat="server" Value="" />
                            <asp:Label ID="lblLocationZipCode" runat="server" CssClass="custom-error"></asp:Label>
                            <br />
                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator21" runat="server" ControlToValidate="txtZipCode"
                                 ValidationGroup="AddBankInfo" ErrorText="Please enter ZIP Code" Display="Dynamic" CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator6" runat="server" ControlToValidate="txtZipCode"
                                ErrorText="Please enter valid ZIP Code e.g; 06514, M3C 0C1. " ValidationGroup="AddBankInfo" Display="Dynamic" 
                                CssClass="custom-error" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$"></cc1:ResourceRegularExpressionValidator>
                            
                            
                        </div>
                        
                    </div>
                    
                </div>
                
            </div>
            
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="form-group">
                <asp:LinkButton ID="lnkbtnAddBankAccount" runat="server" CssClass="btn btn-primary"
                    CausesValidation="true" ValidationGroup="AddBankInfo" OnClick="lnkbtnAddBankAccount_Click" OnClientClick="HideLoginErrorMessage();"> <%= ResourceMgr.GetMessage("Save")%> </asp:LinkButton>
                <asp:LinkButton ID="lnkbtnUpdateBankAccount" runat="server" CssClass="btn btn-primary"
                    CausesValidation='true' ValidationGroup='AddBankInfo'
                    OnClientClick="HideLoginErrorMessage();" OnClick="lnkbtnUpdateBankAccount_Click" Text="Update">  </asp:LinkButton>
                <asp:LinkButton ID="lnkbtnCancelBankAccount" runat="server" CausesValidation="false"
                    CssClass="btn btn-white" OnClick="lnkbtnCancelBankAccount_Click">
                <%= ResourceMgr.GetMessage("Cancel")%> </asp:LinkButton>

                
            </div>
        </div>
    </div>








</asp:Content>

