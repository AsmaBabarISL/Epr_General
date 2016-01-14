<%@ Page Title="" Language="C#" MasterPageFile="~/skeleton.Master" AutoEventWireup="true" CodeFile="RegistrationFormMX.aspx.cs" Inherits="Registration_RegistrationFormMX" %>



<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />

    <script type="text/javascript" src="/Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="/Scripts/ui/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="/Scripts/plugins.js"></script>
    <script type="text/javascript" src="/Scripts/wl_Breadcrumb.js"></script>
    <script type="text/javascript" src="/Scripts/config.js"></script>
    <script type="text/javascript" src="/Scripts/wizard.js"></script>

    <script type="text/javascript">

        function VarifyDomainName(src, args) {
            args.IsValid = true;
            var url = $("#<%=txtBusinessWebsite.ClientID %>").val();
            var email = $("#<%=txtPrimaryContactEmail.ClientID %>").val();
            if (email != "") {
                var m = email.match(/^(http:\/\/|mailto: )?([\w\-]+(\:[\w\-]+)?@)?([\w\.\-]+[^\.\?\#])/);

            }
            if (url != "") {
                if (url.indexOf(m[4]) == -1)
                    args.IsValid = false;
                return false;
            }

            return true;

        }
        function SetFocusOnStep1() {
            if ($("#<%=txtBusinessLegalName.ClientID %>").val() == "" || $("#<%=txtBusinessLegalName.ClientID %>").val() == $("#<%=txtBusinessLegalName.ClientID %>").attr("WaterMarkText"))
                $("#<%=txtBusinessLegalName.ClientID %>").select().val($("#<%=txtBusinessLegalName.ClientID %>").attr("WaterMarkText")).select();
            else {
                $("#<%=txtBusinessLegalName.ClientID %>").focus();
            }
        }

        function SetFocusOnStep2() {
            if ($("#<%=txtLocationBusinessName.ClientID %>").val() == "" || $("#<%=txtLocationBusinessName.ClientID %>").val() == $("#<%=txtLocationBusinessName.ClientID %>").attr("WaterMarkText"))
                $("#<%=txtLocationBusinessName.ClientID %>").select().val($("#<%=txtLocationBusinessName.ClientID %>").attr("WaterMarkText")).select();
            else {
                $("#<%=txtLocationBusinessName.ClientID %>").focus();
            }
        }

        function SetFocusOnStep4() {
            $("#<%=txtSupplierCompanyName.ClientID %>").focus();
        }

        function SetFocusOnStep5() {
            $("#<%=txtclientsCompanyName.ClientID %>").focus();
        }

        function SetFocusOnStep6() {

        }

        function ValidateChecked(self, other, IsUncheckAllowed, SetDateRange, SetImportExport) {
            if (self.checked == true) {
                $("#" + other).attr("checked", false);
                if (SetDateRange)
                    ShowDateRange($("#<%=chkLocationTemporary.ClientID%>").is(':checked'));
                if (SetImportExport)
                    ShowImportExport($("#<%=chkImportExportYes.ClientID%>").is(':checked'));
            }
            else {
                if (!IsUncheckAllowed)
                    return false;
                else {
                    if (SetDateRange)
                        ShowDateRange($("#<%=chkLocationTemporary.ClientID%>").is(':checked'));
                    if (SetImportExport)
                        ShowImportExport($("#<%=chkImportExportYes.ClientID%>").is(':checked'));
                }
            }
        }

        function ShowImportExport(obj) {
            if (obj)
                $("#dvImportExportContent").show();
            else
                $("#dvImportExportContent").hide();
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function ClientValidateCheckBox(src, args) {

            if ($("input:checked", $("#" + $(src).attr('Section'))).length == 0) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }

        }

        function ClientValidateStep6(src, args) {
            if ($("input[type=checkbox][id$=" + $(src).attr('Field') + "]")[0].checked == false)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function ValidateSupplierCount(src, args) {
            if (parseInt($("#<%=hdnSupplierCount.ClientID%>").val()) < 3)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function ValidateClientCount(src, args) {
            if (parseInt($("#<%=hdnClientCount.ClientID%>").val()) < 3)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function SetHiddenFieldValue(hdn, val) {
            $("#" + hdn).val(val);
        }

        function ShowThankYou() {
            $("#content").hide();
            $("#thankyou").show();
        }

        function ValidateClientBusinessPhone(src, args) {
            ValidatePhone(src, args, $("#<%=txtClientBusinessPhone1.ClientID%>"), $("#<%=txtClientBusinessPhone2.ClientID%>"), $("#<%=txtClientBusinessPhone3.ClientID%>"), "<%= ResourceMgr.GetError("Please enter Primary Contact Phone") %>", "<%= ResourceMgr.GetError("Please enter valid Primary Contact Cell Phone") %>");
        }

        function ValidateClientCellPhone(src, args) {
            ValidatePhone(src, args, $("#<%=txtClientCellPhone1.ClientID%>"), $("#<%=txtClientCellPhone2.ClientID%>"), $("#<%=txtClientCellPhone3.ClientID%>"), "<%= ResourceMgr.GetError("Please enter Primary Contact Cell Phone") %>", "<%= ResourceMgr.GetError("Please enter valid Primary Contact Cell Phone") %>");
        }

        function ValidateSupplierBusinessPhone(src, args) {
            ValidatePhone(src, args, $("#<%=txtSupplierBusinessPhone1.ClientID%>"), $("#<%=txtSupplierBusinessPhone2.ClientID%>"), $("#<%=txtSupplierBusinessPhone3.ClientID%>"), "<%= ResourceMgr.GetError("Please enter Primary Contact Phone") %>", "<%= ResourceMgr.GetError("Please enter valid Primary Contact Cell Phone") %>");
    }

    function ValidateSupplierCellPhone(src, args) {
        ValidatePhone(src, args, $("#<%=txtSupplierContactCellPhone1.ClientID%>"), $("#<%=txtSupplierContactCellPhone2.ClientID%>"), $("#<%=txtSupplierContactCellPhone3.ClientID%>"), "<%= ResourceMgr.GetError("Please enter Primary Contact Cell Phone") %>", "<%= ResourceMgr.GetError("Please enter valid Primary Contact Cell Phone") %>");
        }

        function ValidatePrimaryBusinessPhone(src, args) {
            ValidatePhone(src, args, $("#<%=txtPrimaryContactBusinessPhone1.ClientID%>"), $("#<%=txtPrimaryContactBusinessPhone2.ClientID%>"), $("#<%=txtPrimaryContactBusinessPhone3.ClientID%>"), "<%= ResourceMgr.GetError("Please enter Primary Contact Phone") %>", "<%= ResourceMgr.GetError("Please enter valid Primary Contact Cell Phone") %>");
        }

        function ValidatePrimaryCellPhone(src, args) {
            ValidatePhone(src, args, $("#<%=txtPrimaryContactCellPhone1.ClientID%>"), $("#<%=txtPrimaryContactCellPhone2.ClientID%>"), $("#<%=txtPrimaryContactCellPhone3.ClientID%>"), "<%= ResourceMgr.GetError("Please enter Primary Contact Cell Phone") %>", "<%= ResourceMgr.GetError("Please enter valid Primary Contact Cell Phone") %>");
        }

        function ValidateBillingBusinessPhone(src, args) {
            ValidatePhone(src, args, $("#<%=txtBillingContactPhoneNumber1.ClientID%>"), $("#<%=txtBillingContactPhoneNumber2.ClientID%>"), $("#<%=txtBillingContactPhoneNumber3.ClientID%>"), "<%= ResourceMgr.GetError("Please enter Billing Contact Phone") %>", "<%= ResourceMgr.GetError("Please enter valid Billing Contact Phone") %>");
        }

        function ValidateBillingCellPhone(src, args) {

            ValidatePhone(src, args, $("#<%=txtBillingContactCellNumber1.ClientID%>"), $("#<%=txtBillingContactCellNumber2.ClientID%>"), $("#<%=txtBillingContactCellNumber3.ClientID%>"), "<%= ResourceMgr.GetError("Please enter Billing Contact Cell Phone") %>", "<%= ResourceMgr.GetError("Please enter valid Billing Contact Phone") %>");
        }

        function ValidateLocationBusinessPhone(src, args) {
            ValidatePhone(src, args, $("#<%=txtLocationBusinessPhone1.ClientID%>"), $("#<%=txtLocationBusinessPhone2.ClientID%>"), $("#<%=txtLocationBusinessPhone3.ClientID%>"), "<%= ResourceMgr.GetError("Please enter Business Phone") %>", "<%= ResourceMgr.GetError("Please enter valid Business Phone") %>");
        }

        function ValidateLocationCellPhone(src, args) {
            ValidatePhone(src, args, $("#<%=txtLocationCellPhone1.ClientID%>"), $("#<%=txtLocationCellPhone2.ClientID%>"), $("#<%=txtLocationCellPhone3.ClientID%>"), "<%= ResourceMgr.GetError("Please enter Cell Phone") %>", "<%= ResourceMgr.GetError("Please enter valid Cell Phone") %>");
        }

        function ValidatePhone(src, args, Phone1, Phone2, Phone3, EmptyErrorMessage, InValidErrorMessage) {
            var length = parseInt(0);
            if (Phone1.val() != "" && Phone1.val() != Phone1.attr("WaterMarkText"))
                length = length + parseInt(Phone1.val().length);
            if (Phone2.val() != "" && Phone2.val() != Phone2.attr("WaterMarkText"))
                length = length + parseInt(Phone2.val().length);
            if (Phone3.val() != "" && Phone3.val() != Phone3.attr("WaterMarkText"))
                length = length + parseInt(Phone3.val().length);

            if (length == 0) {
                var str = $("#<%=ddlOrganizationType.ClientID%> option:selected").text();
                if (str.indexOf("Government Agency") != -1 || str.indexOf("Law Enforcement Agency") != -1) {
                    args.IsValid = true;
                    return;
                }
                else {
                    args.IsValid = false;
                    src.errormessage = EmptyErrorMessage;
                    src.innerHTML = EmptyErrorMessage;
                }
            }
            else if (length < 10) {
                args.IsValid = false;
                src.errormessage = InValidErrorMessage;
                src.innerHTML = InValidErrorMessage;
            }
            else {
                args.IsValid = true;
            }
        }

        //function GotoNextStep() {
        //    $(".breadcrumb").wl_Breadcrumb('enable');
        //    $(".breadcrumb").find('a.active').parent().nextAll(':visible').first().find('a').trigger('click.wl_Breadcrumb');
        //    $(".breadcrumb").wl_Breadcrumb('disable');
        //}

        //function GotoPrevStep() {
        //    var $aActive = $(".breadcrumb").find('a.active');
        //    var vg = $aActive.attr('ValidationGroup');
        //    if (vg != null)
        //        $aActive.removeAttr('ValidationGroup');

        //    $(".breadcrumb").wl_Breadcrumb('enable');
        //    $(".breadcrumb").find('a.active').parent().prevAll(':visible').first().find('a').trigger('click.wl_Breadcrumb');
        //    $(".breadcrumb").wl_Breadcrumb('disable');

        //    if (vg != null)
        //        $aActive.attr('ValidationGroup', vg);
        //}

        function GotoNextStep(liId, validationGroup) {

            if (liId == 1) {
                if ($('#<%=lblemailalreadyexists.ClientID%>').val() != '') {
                     return;
                 }
             }

            if (liId == 5) {

                if (Page_ClientValidate(validationGroup) && Page_ClientValidate("WholeStep3"))  {//&& Page_ClientValidate("SupplierZipCode")
                    var start = liId + 1;

                    for (var i = start; i < 8; i++) {
                        if ($('#li' + i).css('display') != 'none') {
                            $('#li' + i).addClass('active');
                            $('#tab-' + i).addClass('active');
                            $('#li' + liId).removeClass('active');
                            $('#tab-' + liId).removeClass('active');
                            return;
                        }
                    }
                }
            }
            else if (liId == 6) {
                if (Page_ClientValidate(validationGroup) && Page_ClientValidate("ClientTab")) {//&& Page_ClientValidate("ClientZipCode")
                    var start = liId + 1;

                    for (var i = start; i < 8; i++) {
                        if ($('#li' + i).css('display') != 'none') {
                            $('#li' + i).addClass('active');
                            $('#tab-' + i).addClass('active');
                            $('#li' + liId).removeClass('active');
                            $('#tab-' + liId).removeClass('active');
                            return;
                        }
                    }
                }
            }
            else {
                if (Page_ClientValidate(validationGroup)) {
                    var start = liId + 1;

                    for (var i = start; i < 8; i++) {
                        if ($('#li' + i).css('display') != 'none') {
                            $('#li' + i).addClass('active');
                            $('#tab-' + i).addClass('active');
                            $('#li' + liId).removeClass('active');
                            $('#tab-' + liId).removeClass('active');
                            return;
                        }
                    }
                }
            }


        }

        function GotoPrevStep(liId) {

            var start = liId - 1;

            for (var i = start; i < 8; i--) {
                if ($('#li' + i).css('display') != 'none') {
                    $('#li' + i).addClass('active');
                    $('#tab-' + i).addClass('active');
                    $('#li' + liId).removeClass('active');
                    $('#tab-' + liId).removeClass('active');
                    return;
                }
            }

        }

        function LoginNameNotAvailable() {
            alert('Login Name Not Available.');
        }

        function ShowLoginExistsError() {
            $("#LoginNameExists").show();
        }

        function ClearAdditionalLocationFields() {
            $("input", $("#tabpage_2")).each(function () { $(this).val(''); });
            $("select", $("#tabpage_2")).each(function () { $(this).val('0'); });
            $("input:checked", $("#tabpage_2")).each(function () { $(this).removeAttr('checked'); });
        }

        function ClearSupplierFields() {
            $("input", $("#tabpage_5")).each(function () { $(this).val(''); });
            $("select", $("#tabpage_5")).each(function () { $(this).val('0'); });
        }

        function ClearClientFields() {
            $("input", $("#tabpage_6")).each(function () { $(this).val(''); });
            $("select", $("#tabpage_6")).each(function () { $(this).val('0'); });
        }

        $(function () { ApplyWaterMarkOnTextBoxes(); SetFocusOnStep1(); ShowDateRange(false); });

        function ShowDateRange(obj) {
            if (obj) {
                $("#<%=txtLocationFromDate.ClientID %>").datepicker({
                    defaultDate: "+1w",
                    numberOfMonths: 1,
                    onSelect: function (selectedDate) {
                        $('#txtLocationToDate').attr('readonly', true);
                        $("#<%=txtLocationToDate.ClientID %>").datepicker("option", "minDate", selectedDate);
                        ApplyWaterMarkOnTextBox($("#<%=txtLocationToDate.ClientID %>"));
                    },
                    onClose: function (dateText, inst) {
                        if (dateText == "") {
                            $("#<%=txtLocationToDate.ClientID %>").datepicker("option", "minDate", null);
                            ApplyWaterMarkOnTextBox($("#<%=txtLocationToDate.ClientID %>"));
                        }
                    }
                });
                $("#<%=txtLocationToDate.ClientID %>").datepicker({
                    defaultDate: "+1w",
                    numberOfMonths: 1,
                    onSelect: function (selectedDate) {
                        $('#txtLocationFromDate').attr('readonly', true);
                        $("#<%=txtLocationFromDate.ClientID %>").datepicker("option", "maxDate", selectedDate);
                        ApplyWaterMarkOnTextBox($("#<%=txtLocationFromDate.ClientID %>"));
                    },
                    onClose: function (dateText, inst) {
                        if (dateText == "") {
                            $("#<%=txtLocationFromDate.ClientID %>").datepicker("option", "maxDate", null);
                            ApplyWaterMarkOnTextBox($("#<%=txtLocationFromDate.ClientID %>"));
                        }
                    }
                });
                $("#dvDateRange").show();
            }
            else {
                $("#dvDateRange").hide();
            }
            ValidatorEnable($("#<%=ResourceRequiredFieldValidator18.ClientID %>")[0], obj);
            ValidatorEnable($("#<%=ResourceCustomValidator5.ClientID %>")[0], obj);
        }

        function ValidateDateRange(src, args) {
            if ($("#<%=chkLocationTemporary.ClientID%>").is(":checked") == true) {
                var $from = $("#<%=txtLocationFromDate.ClientID %>");
                var $to = $("#<%=txtLocationToDate.ClientID %>");
                var reg = new RegExp(/^(0[1-9]|1[012])[- \/.](0[1-9]|[12][0-9]|3[01])[- \/.](19|20)\d\d$/);
                if ($from.val() == "" || $from.val() == $from.attr("WaterMarkText") || $to.val() == "" || $to.val() == $to.attr("WaterMarkText")) {
                    args.IsValid = false;
                    src.errormessage = "Should be valid Date Range";
                    src.innerHTML = "Should be valid Date Range";
                }
                else if (reg.test($from.val()) == false || reg.test($to.val()) == false) {
                    args.IsValid = false;
                    src.errormessage = "Should be valid Date Range";
                    src.innerHTML = "Should be valid Date Range";
                }
                else {
                    args.IsValid = true;
                }
            }
            else {
                args.IsValid = true;
            }
        }

        function SetBillingContactSameAsPrimaryContact() {
            if ($("#<%= txtPrimaryContactFirstName.ClientID %>").val() != $("#<%= txtPrimaryContactFirstName.ClientID %>").attr("WaterMarkText"))
                $("#<%= txtBillingContactFirstName.ClientID %>").val($("#<%= txtPrimaryContactFirstName.ClientID %>").val()).removeClass("WaterMark");

            if ($("#<%= txtPrimaryContactLastName.ClientID %>").val() != $("#<%= txtPrimaryContactLastName.ClientID %>").attr("WaterMarkText"))
                $("#<%= txtBillingContactLastName.ClientID %>").val($("#<%= txtPrimaryContactLastName.ClientID %>").val()).removeClass("WaterMark");

            if ($("#<%= txtPrimaryContactBusinessPhone1.ClientID %>").val() != $("#<%= txtPrimaryContactBusinessPhone1.ClientID %>").attr("WaterMarkText"))
                $("#<%= txtBillingContactPhoneNumber1.ClientID %>").val($("#<%= txtPrimaryContactBusinessPhone1.ClientID %>").val()).removeClass("WaterMark");

            if ($("#<%= txtPrimaryContactBusinessPhone2.ClientID %>").val() != $("#<%= txtPrimaryContactBusinessPhone2.ClientID %>").attr("WaterMarkText"))
                $("#<%= txtBillingContactPhoneNumber2.ClientID %>").val($("#<%= txtPrimaryContactBusinessPhone2.ClientID %>").val()).removeClass("WaterMark");

            if ($("#<%= txtPrimaryContactBusinessPhone3.ClientID %>").val() != $("#<%= txtPrimaryContactBusinessPhone3.ClientID %>").attr("WaterMarkText"))
                $("#<%= txtBillingContactPhoneNumber3.ClientID %>").val($("#<%= txtPrimaryContactBusinessPhone3.ClientID %>").val()).removeClass("WaterMark");

            $("#<%= txtBillingContactPhoneExtension.ClientID %>").val($("#<%= txtPrimaryContactBusinessPhoneExtension.ClientID %>").val());

            if ($("#<%= txtPrimaryContactCellPhone1.ClientID %>").val() != $("#<%= txtPrimaryContactCellPhone1.ClientID %>").attr("WaterMarkText"))
                $("#<%= txtBillingContactCellNumber1.ClientID %>").val($("#<%= txtPrimaryContactCellPhone1.ClientID %>").val()).removeClass("WaterMark");

            if ($("#<%= txtPrimaryContactCellPhone2.ClientID %>").val() != $("#<%= txtPrimaryContactCellPhone2.ClientID %>").attr("WaterMarkText"))
                $("#<%= txtBillingContactCellNumber2.ClientID %>").val($("#<%= txtPrimaryContactCellPhone2.ClientID %>").val()).removeClass("WaterMark");

            if ($("#<%= txtPrimaryContactCellPhone3.ClientID %>").val() != $("#<%= txtPrimaryContactCellPhone3.ClientID %>").attr("WaterMarkText"))
                $("#<%= txtBillingContactCellNumber3.ClientID %>").val($("#<%= txtPrimaryContactCellPhone3.ClientID %>").val()).removeClass("WaterMark");

            $("#<%= chkBillingContactAcceptTextMessages.ClientID %>").attr("checked", $("#<%= chkPrimaryContactAcceptTextMessages.ClientID %>").is(":checked"));
            $("#<%= txtBillingContactEmail.ClientID %>").val($("#<%= txtPrimaryContactEmail.ClientID %>").val());
        }

        function SetDBANameAsBusinessName() {
            if ($("#<%= txtBusinessLegalName.ClientID %>").val() != $("#<%= txtBusinessLegalName.ClientID %>").attr("WaterMarkText"))
                $("#<%= txtDBAName.ClientID %>").val($("#<%= txtBusinessLegalName.ClientID %>").val()).removeClass("WaterMark");
        }

        function SetAdditionalLocationDBANameAsBusinessName() {
            if ($("#<%= txtLocationBusinessName.ClientID %>").val() != $("#<%= txtLocationBusinessName.ClientID %>").attr("WaterMarkText"))
                $("#<%= txtLocationDBAName.ClientID %>").val($("#<%= txtLocationBusinessName.ClientID %>").val()).removeClass("WaterMark");
        }

        function SelectPermanentTemporary(src, args) {
            if ($("#<%=chkLocationPermanent.ClientID %>").is(":checked") == true || $("#<%=chkLocationTemporary.ClientID %>").is(":checked") == true)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        function ApplyWaterMarkOnTextBoxes() {
            $("input[WaterMarkText],textarea[WaterMarkText]").each(function () {
                ApplyWaterMarkOnTextBox($(this));
            });
        }

        function ApplyWaterMarkOnTextBox($obj) {
            if ($obj.attr("WaterMarkText") != null) {
                var maxLen = $obj.attr("MaxLength");

                if (maxLen) {
                    $obj.attr("tempMaxLength", maxLen);
                    $obj.removeAttr("MaxLength");
                }

                $obj.blur(function () {
                    if ($obj.val() == '' || $obj.val() == $obj.attr("WaterMarkText")) {
                        var maxLen = $obj.attr("MaxLength");
                        if (maxLen) {
                            $obj.attr("tempMaxLength", maxLen);
                            $obj.removeAttr("MaxLength");
                        }
                        $obj.val($obj.attr("WaterMarkText")).addClass("WaterMark");
                    }
                });
                $obj.focus(function () {
                    if ($obj.val() == '' || $obj.val() == $obj.attr("WaterMarkText")) {
                        if ($obj.attr("tempMaxLength")) {
                            $obj.attr("MaxLength", $obj.attr("tempMaxLength"));
                        }
                        $obj.val('').removeClass("WaterMark");
                    }
                });

                if ($obj.val() == '' || $obj.val() == $obj.attr("WaterMarkText")) {
                    $obj.val($obj.attr("WaterMarkText")).addClass("WaterMark");
                }
            }
        }

        function SetWizard(ddl) {

            $(".use8steps").hide();
            $(".use7steps").hide();
            $(".use4steps").hide();
            $(".use3steps").hide();
            $("#dvCertComplainceTrax").hide();
            $("#dvCertSTrax").hide();

            $("#li2").hide();
            $("#li3").hide();
            $("#li4").hide();
            $("#li5").hide();
            $("#li6").hide();
            $("#orgsubType").hide();
            $("input[type=checkbox]", $("#dvCertSTrax")).removeAttr("checked");
            $("input[type=checkbox]", $("#dvCertComplainceTrax")).removeAttr("checked");
            ValidatorEnable($("#<%=rsvldcusSTrax.ClientID %>")[0], false);
            if ($(ddl).val() == "0" || $(ddl).children("option:selected").text().indexOf("Consumer") != -1) {


                $(".use3steps").show();
                $("#li2").hide();
                $("#li3").hide();
                $("#li4").hide();
                $("#li5").hide();
                $("#li6").hide();
                $(".use3steps").show();

            } else
                if ($(ddl).val() == "0" || $(ddl).children("option:selected").text().indexOf("Stakeholder") != -1) {

                    if ($(ddl).val() == "0") {
                        $(".use8steps").show();
                        $("#li3").show();
                    }
                    if ($(ddl).children("option:selected").text().indexOf("Stakeholder") != -1) {
                        $(".use7steps").show();

                    }
                    $("#li2").show();
                    $("#li4").show();
                    $("#li5").show();
                    $("#li6").show();
                    $("#orgsubType").show();
                }
                else {
                    if ($(ddl).children("option:selected").text().indexOf("Stewardship") != -1) {
                        $(".use3steps").show();
                    }
                    else {
                        $("#li3").show();
                        $(".use4steps").show();
                    }

                    if ($(ddl).children("option:selected").text().indexOf("Law Enforcement Agency") != -1 || $(ddl).children("option:selected").text().indexOf("Government Agency") != -1) {
                        $("#dvCertComplainceTrax").show();
                    }

                    if ($(ddl).children("option:selected").text().indexOf("Global Steward") != -1 || $(ddl).children("option:selected").text().indexOf("Local Steward") != -1) {
                        $("#dvCertSTrax").show();
                        ValidatorEnable($("#<%=rsvldcusSTrax.ClientID %>")[0], true);
                    }
                    else {
                        $("#dvCertSTrax").hide();
                    }
                }
        }

        function ValidatePrimaryContactName(src, args) {
            args.IsValid = ValidateFirstLastName($("#<%=txtPrimaryContactFirstName.ClientID%>"), $("#<%=txtPrimaryContactLastName.ClientID%>"));
        }

        function ValidateBillingContactName(src, args) {
            args.IsValid = ValidateFirstLastName($("#<%=txtBillingContactFirstName.ClientID%>"), $("#<%=txtBillingContactLastName.ClientID%>"));
        }

        function ValidateLocationContactName(src, args) {
            args.IsValid = ValidateFirstLastName($("#<%=txtLocationContactFirstName.ClientID%>"), $("#<%=txtLocationContactLastName.ClientID%>"));
        }

        function ValidateSupplierContactName(src, args) {
            args.IsValid = ValidateFirstLastName($("#<%=txtSupplierscontactFirstName.ClientID%>"), $("#<%=txtSupplierscontactLastName.ClientID%>"));
        }

        function ValidateClientContactName(src, args) {
            args.IsValid = ValidateFirstLastName($("#<%=txtclientsContactFirstName.ClientID%>"), $("#<%=txtclientsContactLastName.ClientID%>"));
        }

        function ValidateFirstLastName($FirstName, $LastName) {
            if ($FirstName.val() == "" || $FirstName.val() == $FirstName.attr("WaterMarkText") || $LastName.val() == "" || $LastName.val() == $LastName.attr("WaterMarkText"))
                return false;
            else
                return true;
        }

        function HideBusinessZipCodeLabel() {
            $("#<%=lblBusinessZipCode.ClientID%>").hide();
        }

        function HideMailingZipCodeLabel() {
            $("#<%=lblMailingZipCode.ClientID%>").hide();
        }

        function HideLocationZipCodeLabel() {
            $("#<%=lblLocationZipCode.ClientID%>").hide();
        }

        function HideSupplierZipCodeLabel() {
            $("#<%=lblSupplierZipCode.ClientID%>").hide();
        }

        function HideClientZipCodeLabel() {
            $("#<%=lblClientZipCode.ClientID%>").hide();
        }

        function HideBusinessStateZipCode() {
            $("#<%=lblStewardshipExists.ClientID%>").hide();
        }

        $(document).ready(function () {
            $('#openprivacyagr').toggle(function () {
                $('#dvPrivacy').slideDown();
            }, function () {
                $('#dvPrivacy').slideUp();
            });

            $('#ClosePrivacy').toggle(function () {
                $('#dvPrivacy').hide();
                $('#dvPrivacy').slideUp();

            });

            $('#openStewardshipAgr').toggle(function () {
                $('#dvStewardship').slideDown();
            }, function () {
                $('#dvStewardship').slideUp();
            });
            $('#lnkCancelPermanentLot').toggle(function () {
                $('#dvStewardship').slideDown();
            }, function () {
                $('#dvStewardship').slideUp();
            });


        });

        function ValidateCheckBoxList(sender, args) {
            //debugger;
            var checkBoxList = document.getElementById("<%=chkProductId.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }

        function visibleDiv(divName) {
            if (divName == 'stewardModal')
                document.getElementById("<%=stewardModal.ClientID%>").style.display = "block";
            else
                document.getElementById("<%=eprtsModal.ClientID%>").style.display = "block";
        }
        function dismissDiv(divName) {
            if (divName == 'stewardModal')
                document.getElementById("<%=stewardModal.ClientID%>").style.display = "none";
            else
                document.getElementById("<%=eprtsModal.ClientID%>").style.display = "none";
        }

    </script>
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("form");
            //$(".ajax-loader").remove();
            $(".ajax-loader").hide();
        }

        function AjaxLoader() {
            $(".ajax-loader").show();
            $(".ajax-loader").appendTo("form");
        }
        $(document).ready(function () {
            $(".ajaxModal-popup").appendTo("form");
        });

    </script>
    <div class="row">
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
        </script>
        <div class="col-lg-12">
            <div class="reg-logo text-center p-md">
                <img src="/img/main-Logo.png" id="logoimg" runat="server" style="height: 80px;" alt="Product Loop" />
                <%--<img src="/images/NewTTlogo.png" alt="Logo" width="300" />--%>
                <%--<h2 class="tag-line"><%= ResourceMgr.GetMessage("Real World Solutions for Global Resource Recycling")%></h2>--%>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="wrapper wrapper-content animated fadeInRight reg">
                <div class="panel-heading p0">
                    <div class="panel-options">
                        <ul class="nav nav-tabs" data-connect="breadcrumbcontent">
                            <li id="li1" class="active">
                                <a data-toggle="tab" href="#tab-1" validationgroup="PrimaryRegistartion">
                                    <%= ResourceMgr.GetMessage("Primary Registration")%></a></li>
                            <li id="li2"><a data-toggle="tab" href="#tab-2" validationgroup="AdditionalLocations">
                                <%= ResourceMgr.GetMessage("Additional Locations")%></a></li>
                            <li id="li3"><a data-toggle="tab" href="#tab-3" validationgroup="Stewardships">
                                <%= ResourceMgr.GetMessage("Stewardships")%></a></li>
                            <li id="li4"><a data-toggle="tab" href="#tab-4">
                                <%= ResourceMgr.GetMessage("Stakeholders")%></a></li>
                            <li id="li5"><a data-toggle="tab" href="#tab-5" validationgroup="WholeStep3">
                                <%= ResourceMgr.GetMessage("Suppliers")%></a></li>
                            <li id="li6"><a data-toggle="tab" href="#tab-6" validationgroup="WholeStep4">
                                <%= ResourceMgr.GetMessage("Clients")%></a></li>
                            <li id="li7"><a data-toggle="tab" href="#tab-7" validationgroup="SubmitApplication">
                                <%= ResourceMgr.GetMessage("Submit Application")%></a></li>

                        </ul>
                    </div>
                </div>

                <div id="breadcrumbcontent" class="tab-content">
                    <div id="tab-1" class="tab-pane active">
                        <div class="mail-box-header">
                            <div class="pull-right tooltip-demo">
                                <label class="badge badge-primary pull-right use8steps">
                                    <%= ResourceMgr.GetMessage("Step 1 of 7")%></label>
                                <label class="badge badge-primary pull-right use7steps" style="display: none;">
                                    <%= ResourceMgr.GetMessage("Step 1 of 6")%></label>
                                <label class="badge badge-primary pull-right use4steps" style="display: none;">
                                    <%= ResourceMgr.GetMessage("Step 1 of 3")%></label>
                                <label class="badge badge-primary pull-right use3steps" style="display: none;">
                                    <%= ResourceMgr.GetMessage("Step 1 of 2")%></label>
                            </div>
                            <h2 class="login_title">
                                <%= ResourceMgr.GetMessage("Primary Registration")%>
                            </h2>

                        </div>
                        <div class="mail-box">
                            <div class="mail-body">
                                <%--   Update Panel here--%>
                                <asp:UpdatePanel ID="upnlStep1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Business Name:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtBusinessLegalName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtBusinessLegalName" ValidationGroup="PrimaryRegistartion"
                                                                CssClass="custom-block-error" Display="Dynamic">
                                                                <%= ResourceMgr.GetError("Please enter Business Name")%>  
                                                            </cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("DBA Name:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtDBAName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <button type="button" onclick="SetDBANameAsBusinessName();" class="btn btn-sm btn-white" />
                                                                    <%= ResourceMgr.GetMessage("Same")%></button>
                                                                    
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Organization:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlOrganizationType" runat="server" CssClass="form-control" onchange="SetWizard(this);"></asp:DropDownList>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator3" runat="server"
                                                                ControlToValidate="ddlOrganizationType" InitialValueErrorText="0" ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error">
                                                                <%= ResourceMgr.GetError("Please select Organization Type")%> 
                                                            </cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group" id="orgsubType" runat="server">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Stakeholder Type:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlOrganizationSubType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Product Type:")%></label>
                                                        <div class="col-sm-7">

                                                                <asp:CheckBoxList ID="chkProductId" runat="server" CssClass="prod-type-chkbox" RepeatDirection="Horizontal"
                                                                     RepeatColumns="3" DataTextField="ProductName" DataValueField="ProductId" Style="margin-top:9px">
                                                                </asp:CheckBoxList>

                                                            <cc1:ResourceCustomValidator ID="CustomValidator2"
                                                                CssClass="custom-block-error" ClientValidationFunction="ValidateCheckBoxList" runat="server"
                                                                ValidationGroup="PrimaryRegistartion"  Display="Dynamic">
                                                                <%= ResourceMgr.GetError("Please select at least one Product type")%>  
                                                            </cc1:ResourceCustomValidator>
                                                        </div>
                                                       
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Primary Contact Name:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtPrimaryContactFirstName" runat="server" Placeholder='<%# ResourceMgr.GetError("First Name") %>' CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtPrimaryContactLastName" runat="server" Placeholder='<%# ResourceMgr.GetError("Last Name") %>' CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator15" runat="server"
                                                                ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error" ClientValidationFunction="ValidatePrimaryContactName">
                                                                <%= ResourceMgr.GetError("Please enter Primary Contact Name")%>
                                                            </cc1:ResourceCustomValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Title:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlPrimaryContactTitle" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator14" runat="server"
                                                                ControlToValidate="ddlPrimaryContactTitle" InitialValueErrorText="0" ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error">
                                                                <%= ResourceMgr.GetError("Please select Title")%>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Business Phone:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtPrimaryContactBusinessPhone1" runat="server" CssClass="form-control text-center"
                                                                        MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtPrimaryContactBusinessPhone2" runat="server" CssClass="form-control text-center"
                                                                        MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtPrimaryContactBusinessPhone3" runat="server" CssClass="form-control text-center"
                                                                        MaxLength="4" placeholder="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator1" runat="server" ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error" ClientValidationFunction="ValidatePrimaryBusinessPhone">
                                                                <%= ResourceMgr.GetMessage("Please enter Primary Contact Phone")%>
                                                            </cc1:ResourceCustomValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Extension:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtPrimaryContactBusinessPhoneExtension" runat="server" CssClass="form-control"  onkeypress="return isNumberKey(event);" MaxLength="4"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Cell Phone:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtPrimaryContactCellPhone1" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtPrimaryContactCellPhone2" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtPrimaryContactCellPhone3" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator2" runat="server" ValidationGroup="PrimaryRegistartion" Display="Dynamic" CssClass="custom-block-error" ClientValidationFunction="ValidatePrimaryCellPhone"></cc1:ResourceCustomValidator>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Accept Text Messages:")%></label>
                                                        <div class="col-sm-7">
                                                            <label class="checkbox">
                                                                <asp:CheckBox ID="chkPrimaryContactAcceptTextMessages" runat="server" CssClass="m-l-md" /><%= ResourceMgr.GetMessage("Yes")%>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Primary Contact Email:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtPrimaryContactEmail" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                OnTextChanged="txtPrimaryContactEmail_TextChanged"></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator4" runat="server"
                                                                ControlToValidate="txtPrimaryContactEmail" ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error">
                                                                <%= ResourceMgr.GetError("Please enter Primary Contact Email")%>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator1"
                                                                runat="server" ControlToValidate="txtPrimaryContactEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ValidationGroup="PrimaryRegistartion" Display="Dynamic" CssClass="custom-block-error">
                                                                <%= ResourceMgr.GetError("Please enter valid Primary Contact Email")%></cc1:ResourceRegularExpressionValidator>
                                                            <asp:Label ID="lblemailalreadyexists" runat="server" CssClass="custom-block-error" Text="Email already exists for this state.Choose another." Visible="false"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Billing Contact Same:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="btn btn-sm btn-white" onclick="SetBillingContactSameAsPrimaryContact();">
                                                                <%= ResourceMgr.GetMessage("Same")%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Billing Contact Name:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtBillingContactFirstName" runat="server" placeholder='<%# ResourceMgr.GetError("First Name")%>' CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtBillingContactLastName" runat="server" placeholder='<%# ResourceMgr.GetError("Last Name")%>' CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator16" runat="server" ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error"
                                                                ClientValidationFunction="ValidateBillingContactName">
                                                                <%= ResourceMgr.GetError("Please enter Billing Contact Name")%>
                                                            </cc1:ResourceCustomValidator>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Title:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlBillingContactTitle" runat="server" CssClass="form-control"></asp:DropDownList>

                                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator9" runat="server" ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error"
                                                                ClientValidationFunction="ValidateBillingContactName">
                                                                <%= ResourceMgr.GetError("Please enter Billing Contact Name")%>
                                                            </cc1:ResourceCustomValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">
                                                            <%= ResourceMgr.GetMessage("Billing Phone:")%>
                                                        </label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtBillingContactPhoneNumber1" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>

                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtBillingContactPhoneNumber2" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">

                                                                    <asp:TextBox ID="txtBillingContactPhoneNumber3" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator3" runat="server" ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error" ClientValidationFunction="ValidateBillingBusinessPhone">
                                                                <%= ResourceMgr.GetError("Please enter Business Phone")%></cc1:ResourceCustomValidator>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Extension:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtBillingContactPhoneExtension" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event);" MaxLength="4"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Billing Cell Phone:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtBillingContactCellNumber1" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtBillingContactCellNumber2" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtBillingContactCellNumber3" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator4" runat="server" ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error" ClientValidationFunction="ValidateBillingCellPhone">
                                                                <%= ResourceMgr.GetError("Please enter Business Phone")%>
                                                            </cc1:ResourceCustomValidator>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Accept Text Messages:")%></label>
                                                        <div class="col-sm-7">
                                                            <label class="checkbox">
                                                                <asp:CheckBox ID="chkBillingContactAcceptTextMessages" runat="server" CssClass="m-l-md" /><%= ResourceMgr.GetMessage("Yes")%>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Billing Email:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtBillingContactEmail" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator5" runat="server"
                                                                ControlToValidate="txtBillingContactEmail" ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error">
                                                                <%= ResourceMgr.GetError("Please enter Billing Contact Email")%>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRegularExpressionValidator
                                                                ID="ResourceRegularExpressionValidator13" runat="server"
                                                                ControlToValidate="txtBillingContactEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ValidationGroup="PrimaryRegistartion" Display="Dynamic" CssClass="custom-block-error">
                                                                 <%= ResourceMgr.GetError("Please enter valid Billing Contact Email")%>
                                                            </cc1:ResourceRegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <%-- right side columns--%>
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Facility ZIP Code:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtBusinessZipCode" runat="server" CssClass="form-control" onblur="HideBusinessZipCodeLabel();" AutoPostBack="true" OnTextChanged="txtBusinessZipCode_TextChanged" CausesValidation="true" ValidationGroup="AdditionalInfoBusinessZipCode" MaxLength="10"></asp:TextBox><asp:HiddenField ID="hdnBusinessZipCodeId" runat="server" Value="" />
                                                            <asp:HiddenField ID="hfCityId" runat="server" Value="0" />
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator6" runat="server" ControlToValidate="txtBusinessZipCode"
                                                                ValidationGroup="PrimaryRegistartion" CssClass="custom-block-error" >
                                                                        <%= ResourceMgr.GetError("Please enter Facility ZIP Code")%>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator2" runat="server" ControlToValidate="txtBusinessZipCode"
                                                                ValidationGroup="PrimaryRegistartion" CssClass="custom-block-error" Display="None" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$">
                                                                        <%= ResourceMgr.GetError("Please enter valid Facility ZIP Code e.g; 06514, M3C 0C1")%>
                                                            </cc1:ResourceRegularExpressionValidator>
                                                            <asp:Label ID="lblBusinessZipCode" runat="server" CssClass="custom-block-error"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Facility Street Address:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtBusinessAddress1" runat="server" placeholder='<%# ResourceMgr.GetError("Primary physical location Street # - Street name")%>' CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator8" runat="server" ControlToValidate="txtBusinessAddress1"
                                                                ValidationGroup="PrimaryRegistartion" Display="Dynamic"
                                                                CssClass="custom-block-error"><%= ResourceMgr.GetError("Please enter Facility Street Address")%></cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Suit #:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtBusinessAddress2" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("City:")%> </label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtBuinessCity" Enabled="false" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator10" runat="server"
                                                                ControlToValidate="txtBuinessCity" ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error"><%= ResourceMgr.GetError("Please enter City")%></cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("State:")%> </label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlBusinessProvince" Enabled="false" runat="server" onchange="HideBusinessStateZipCode();" CssClass="form-control" OnSelectedIndexChanged="dllState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator11" runat="server" ControlToValidate="ddlBusinessProvince"
                                                                InitialValueErrorText="0" ValidationGroup="PrimaryRegistartion" Display="Dynamic" CssClass="custom-block-error">
                                                                <%= ResourceMgr.GetError("Please select State")%>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                            <asp:Label ID="lblStewardshipExists" runat="server" CssClass="custom-block-error" Text="Stewardship already exists for this state." Visible="false"></asp:Label>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Country:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlBusinessCountry" Enabled="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="dllCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator12" runat="server" ControlToValidate="ddlBusinessCountry"
                                                                InitialValueErrorText="0" ValidationGroup="PrimaryRegistartion"
                                                                Display="Dynamic" CssClass="custom-block-error"><%= ResourceMgr.GetMessage("Please select Facility Country")%></cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Mailing Address Same:")%></label>
                                                        <div class="col-sm-7">
                                                            <cc1:ResourceLinkButton CssClass="btn btn-sm btn-white" ID="rslnkbtnBillingSameAsFacility" runat="server" OnClick="rslnkbtnBillingSameAsFacility_Click">
                                                                <%= ResourceMgr.GetMessage("Same")%>
                                                            </cc1:ResourceLinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Mailing ZIP Code:")%> </label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtMailingZipCode" runat="server" CssClass="form-control" onblur="HideMailingZipCodeLabel();" AutoPostBack="true" OnTextChanged="txtMailingZipCode_TextChanged" CausesValidation="true" ValidationGroup="MailingZipCode" MaxLength="10"></asp:TextBox><asp:HiddenField ID="hdnMailingZipCodeId" runat="server" Value="" />

                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator42" runat="server" ControlToValidate="txtMailingZipCode"
                                                                ValidationGroup="MailingZipCode" CssClass="custom-block-error" Display="None">
                                                                        <%= ResourceMgr.GetError("Please enter ZIP Code")%>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator14" runat="server" ControlToValidate="txtMailingZipCode"
                                                                ValidationGroup="MailingZipCode" CssClass="custom-block-error"
                                                                Display="None" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$">
                                                                        <%= ResourceMgr.GetError("Please enter valid Mailing ZIP Code e.g; 06514, M3C 0C1")%>
                                                            </cc1:ResourceRegularExpressionValidator>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator43" runat="server" ControlToValidate="txtMailingZipCode"
                                                                ValidationGroup="PrimaryRegistartion" Display="Dynamic" CssClass="custom-block-error">
                                                                        <%= ResourceMgr.GetError("Please enter Mailing ZIP Code")%>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator15" runat="server" ControlToValidate="txtMailingZipCode"
                                                                ValidationGroup="PrimaryRegistartion"
                                                                CssClass="custom-block-error" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$" Display="None">
                                                                        <%= ResourceMgr.GetError("Please enter valid Mailing ZIP Code e.g; 06514, M3C 0C1")%>
                                                            </cc1:ResourceRegularExpressionValidator>
                                                            <asp:Label ID="lblMailingZipCode" runat="server" CssClass="custom-block-error"></asp:Label>
                                                            <asp:HiddenField ID="hfmailingCityId" runat="server" Value="0" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Street Address:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtMailingAddress1" runat="server" CssClass="form-control" WaterMarkText='<%# ResourceMgr.GetError("Mailing Street# - Street name") %>' MaxLength="200"></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator13" runat="server" ControlToValidate="txtMailingAddress1"
                                                                ValidationGroup="PrimaryRegistartion" Display="Dynamic" CssClass="custom-block-error">
                                                                        <%= ResourceMgr.GetError("Please enter Mailing Address")%>
                                                            </cc1:ResourceRequiredFieldValidator>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Suite# or PO Box#:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtMailingAddress2" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("City:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox Enabled="false" ID="txtMailingCity" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator15" runat="server" ControlToValidate="txtMailingCity" ValidationGroup="PrimaryRegistartion" Display="Dynamic" CssClass="custom-block-error">
                                                                        <%= ResourceMgr.GetError("Please enter City")%>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("State:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList Enabled="false" ID="ddlMailingState" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator16" runat="server" ControlToValidate="ddlMailingState"
                                                                InitialValueErrorText="0" ValidationGroup="PrimaryRegistartion" Display="Dynamic"
                                                                CssClass="custom-block-error"><%= ResourceMgr.GetError("Please select State")%></cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Country:")%> </label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList Enabled="false" ID="ddlMailingCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="dllCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator17" runat="server" ControlToValidate="ddlMailingCountry" InitialValueErrorText="0"
                                                                ValidationGroup="PrimaryRegistartion" Display="Dynamic" CssClass="custom-block-error">
                                                                        <%= ResourceMgr.GetError("Please select  Country") %>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Website:")%> </label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtBusinessWebsite" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Accounting Interface:")%> </label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlBusinessAccountingInterface" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Inventory Interface:")%> </label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlBusinessInventoryInterface" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Accept Auto-Fund Transfers:")%></label>
                                                        <div class="col-sm-7">
                                                            <label class="checkbox">
                                                                <asp:CheckBox ID="chkBusinessAcceptAutoFundTransfers" runat="server" CssClass="m-l-md" />
                                                                <%# ResourceMgr.GetMessage("Yes")%>
                                                            </label>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row text-right">
                                            <div class="col-md-12">
                                                <a id="aStep1" class="btn btn-primary" href="#" style="display: none;"><%= ResourceMgr.GetMessage("Next") %></a>
                                                <cc1:ResourceLinkButton ID="lnkbtnStep1" CssClass="btn btn-primary" ValidationGroup="PrimaryRegistartion" runat="server"
                                                    OnClick="lnkbtnStep1_Click"><%= ResourceMgr.GetMessage("Next") %></cc1:ResourceLinkButton>
                                                <asp:HiddenField ID="hdnOrganizationID" runat="server" Value="" />
                                            </div>
                                        </div>


                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkbtnStep1" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <!-- tab 1 ends here-->

                    <div id="tab-2" class="tab-pane">
                        <div class="mail-box-header">
                            <div class="pull-right tooltip-demo">
                                <label class="badge badge-primary pull-right">
                                    <%= ResourceMgr.GetMessage("Step 2 of 7")%></label>
                                <label class="badge badge-primary pull-right" style="display: none;">
                                    <%= ResourceMgr.GetMessage("Step 2 of 6")%></label>
                            </div>
                            <h2 class="login_title"><%= ResourceMgr.GetMessage("Additional Locations")%></h2>
                        </div>
                        <div class="mail-box">
                            <div class="ibox-content ibox-heading">
                                <p class="mb0">
                                    <i class="fa fa-exclamation-circle"></i>
                                    <%= ResourceMgr.GetMessage("Additional Locations are generally for Temporary Events or if the Stakeholder has no permanent management on premise. Please, file all managed facilities as a New and seperate location. There are no additional charges. The primary facility's stakeholder is provided with both individual and consolidated facilities management reports.")%>
                                </p>
                            </div>
                            <div class="mail-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Location Name:")%></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtLocationBusinessName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator2" runat="server"
                                                        ControlToValidate="txtLocationBusinessName" ValidationGroup="AdditionalLocation"
                                                        Display="Dynamic" CssClass="custom-block-error">
                                                        <%= ResourceMgr.GetError("Please enter Location Name")%>
                                                    </cc1:ResourceRequiredFieldValidator>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("DBA Name:")%></label>
                                                <div class="col-sm-7">
                                                    <div class="row">
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txtLocationDBAName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="btn btn-sm btn-white" onclick="SetAdditionalLocationDBANameAsBusinessName();">
                                                                <%= ResourceMgr.GetMessage("Same")%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Location Status:")%></label>
                                                <div class="col-sm-7">
                                                    <label class="checkbox-inline">
                                                        <asp:CheckBox ID="chkLocationPermanent" runat="server" />
                                                        <%= ResourceMgr.GetMessage("Permanent")%>
                                                    </label>
                                                    <label class="checkbox-inline">
                                                        <asp:CheckBox ID="chkLocationTemporary" runat="server" />
                                                        <%= ResourceMgr.GetMessage("Temporary")%>
                                                    </label>
                                                    <cc1:ResourceCustomValidator ID="vldcusLocationStatus" runat="server" ClientValidationFunction="SelectPermanentTemporary"
                                                        Display="None" ValidationGroup="AdditionalLocation" EnableClientScript="false"
                                                        CssClass="custom-block-error"><%= ResourceMgr.GetError("Please check mark Permanent or Temporary box")%> </cc1:ResourceCustomValidator>
                                                </div>
                                            </div>
                                            <div id="dvDateRange">
                                                <div class="form-group">
                                                    <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Location Event:")%></label>
                                                    <div class="col-sm-7">
                                                        <asp:DropDownList ID="ddlLocationEventType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator18" runat="server"
                                                            ControlToValidate="ddlLocationEventType" InitialValueErrorText="0" ValidationGroup="AdditionalLocation"
                                                            Display="Static" CssClass="custom-block-error">
                                                        <%= ResourceMgr.GetError("Please select Temporary Event Type")%>
                                                        </cc1:ResourceRequiredFieldValidator>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Event Dates:")%></label>
                                                    <div class="col-sm-7">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtLocationFromDate" runat="server" CssClass="datepicker form-control" placeholder='<%# ResourceMgr.GetError("From") %>' MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">

                                                                <asp:TextBox ID="txtLocationToDate" runat="server" CssClass="form-control" placeholder='<%# ResourceMgr.GetError("To")%>' MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator5" runat="server" ClientValidationFunction="ValidateDateRange"
                                                                Display="Static" ValidationGroup="AdditionalLocation"
                                                                CssClass="custom-block-error"><%= ResourceMgr.GetError("Should be valid date range") %></cc1:ResourceCustomValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Location Contact Person:") %></label>
                                                <div class="col-sm-7">
                                                    <div class="row">
                                                        <div class="col-sm-6 ">
                                                            <asp:TextBox ID="txtLocationContactFirstName" runat="server" placeholder='<%# ResourceMgr.GetError("First Name") %>' CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtLocationContactLastName" runat="server" placeholder='<%# ResourceMgr.GetError("Last Name")%>' CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <cc1:ResourceCustomValidator ID="ResourceCustomValidator17" runat="server" ValidationGroup="AdditionalLocation"
                                                        Display="Dynamic" CssClass="custom-block-error"
                                                        ClientValidationFunction="ValidateLocationContactName">
                                                        <%= ResourceMgr.GetError("Please enter Location Contact Name") %>
                                                    </cc1:ResourceCustomValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Title:")%></label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlLocationContactTitle" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Location Business Phone:")%></label>
                                                <div class="col-sm-7">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtLocationBusinessPhone1" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtLocationBusinessPhone2" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtLocationBusinessPhone3" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <cc1:ResourceCustomValidator ID="ResourceCustomValidator6" runat="server" ValidationGroup="AdditionalLocation"
                                                        Display="Dynamic" CssClass="custom-block-error"
                                                        ClientValidationFunction="ValidateLocationBusinessPhone"><%= ResourceMgr.GetError("Please enter Business Phone")%></cc1:ResourceCustomValidator>

                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Extension:")%></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtLocationBusinessPhoneExtension" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event);" MaxLength="4"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Cell Phone:")%></label>
                                                <div class="col-sm-7">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtLocationCellPhone1" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtLocationCellPhone2" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtLocationCellPhone3" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <cc1:ResourceCustomValidator ID="ResourceCustomValidator7" runat="server" ValidationGroup="AdditionalLocation"
                                                        Display="Dynamic" CssClass="custom-block-error"
                                                        ClientValidationFunction="ValidateLocationCellPhone"><%= ResourceMgr.GetError("Please enter Business Phone")%></cc1:ResourceCustomValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Accept Text Messages:")%></label>
                                                <div class="col-sm-7">
                                                    <label class="checkbox m-l-md">
                                                        <asp:CheckBox ID="chkLocationContactAcceptTextMessages" runat="server" />
                                                        <%= ResourceMgr.GetMessage("Yes")%>
                                                    </label>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Location Contact Email:")%></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtLocationContactEmail" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator19" runat="server"
                                                        ControlToValidate="txtLocationContactEmail" ValidationGroup="AdditionalLocation"
                                                        Display="Dynamic" CssClass="custom-block-error">
                                                        <%= ResourceMgr.GetError("Please enter Location Contact Email")%>
                                                    </cc1:ResourceRequiredFieldValidator>
                                                    <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator4"
                                                        runat="server" ControlToValidate="txtLocationContactEmail"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AdditionalLocation"
                                                        CssClass="custom-block-error" Display="Dynamic">
                                                        <%= ResourceMgr.GetError("Please enter valid Location Contact Email")%>
                                                    </cc1:ResourceRegularExpressionValidator>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Location ZIP Code:")%> </label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtLocationZipCode" runat="server" CssClass="form-control" onblur="HideLocationZipCodeLabel();" AutoPostBack="true" OnTextChanged="txtLocationZipCode_TextChanged" CausesValidation="true" ValidationGroup="AdditionalLocation" MaxLength="10"></asp:TextBox><asp:HiddenField ID="hdnLocationZipCodeID" runat="server" Value="" />

                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator21" runat="server" ControlToValidate="txtLocationZipCode"
                                                                ValidationGroup="AdditionalLocation" Display="Dynamic" CssClass="custom-block-error">
                                                                <%= ResourceMgr.GetError("Please enter ZIP Code")%>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator6" runat="server" ControlToValidate="txtLocationZipCode"
                                                                ValidationGroup="AdditionalLocation" Display="Dynamic"
                                                                CssClass="custom-block-error" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$">
                                                                <%= ResourceMgr.GetError("Please enter valid ZIP Code e.g; 06514, M3C 0C1")%>
                                                            </cc1:ResourceRegularExpressionValidator>
                                                            <asp:Label ID="lblLocationZipCode" runat="server" CssClass="custom-block-error"></asp:Label>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Location Street Address:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtLocationAddress1" runat="server" placeholder='<%# ResourceMgr.GetError("Primary physical location Street # - Street name") %>' CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator22" runat="server" InitialValueErrorText="Primary physical location Street # - Street name"
                                                                ControlToValidate="txtLocationAddress1" ValidationGroup="AdditionalLocation" Display="Dynamic" CssClass="custom-block-error">
                                                                <%= ResourceMgr.GetError("Please enter Address")%>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Suit #:") %></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtLocationAddress2" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("City:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtLocationCity" Enabled="false" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator48" runat="server" ControlToValidate="txtLocationCity" ValidationGroup="AdditionalLocation" Display="Dynamic" CssClass="custom-block-error">
                                                                <%= ResourceMgr.GetError("Please enter City")%>
                                                            </cc1:ResourceRequiredFieldValidator>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("State:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlLocationState" Enabled="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator24" runat="server" ControlToValidate="ddlLocationState"
                                                                InitialValueErrorText="0" ValidationGroup="AdditionalLocation" Display="Dynamic"
                                                                CssClass="custom-block-error"><%= ResourceMgr.GetError("Please select State")%></cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Country:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlLocationCountry" Enabled="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="dllCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtLocationZipCode" EventName="TextChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlLocationState" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlLocationCountry" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Location Permit #:")%></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtLocationPermitNumber" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator25" runat="server"
                                                        ControlToValidate="txtLocationPermitNumber" ValidationGroup="AdditionalLocation"
                                                        Display="Dynamic" CssClass="custom-block-error">
                                                        <%= ResourceMgr.GetError("Please enter Location Permit #")%>
                                                    </cc1:ResourceRequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:UpdatePanel ID="upnlLocation" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row-fluid text-right">
                                                    <cc1:ResourceLinkButton ID="lnkBtnLocationAdd" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                                        CausesValidation="true" ValidationGroup="AdditionalLocation"
                                                        OnClick="lnkBtnLocationAdd_Click"> <%=ResourceMgr.GetMessage("Add this Location") %></cc1:ResourceLinkButton>
                                                </div>
                                                <div class="table-responsive m-t-md">
                                                    <asp:GridView ID="gvAdditionLocations" runat="server" GridLines="None" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="table table-bordered epr-sec-table" Width="100%" wrap="nowrap" OnRowCommand="gvAdditionLocations_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                        <HeaderStyle />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="&nbsp;&nbsp;&nbsp;Location Name" DataField="LegalName" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Location Status" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <%# Convert.ToBoolean(Eval("LocationStatus")) == true ? "Permanent" : "Temporary"%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Contact Name" DataField="ContactName" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Business Phone" DataField="Number" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="ZIP Code" DataField="ZipPostalCode" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Permit #" DataField="LocationPermitNumber" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkBtnDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this record.');" CommandName="DeleteLocation" CommandArgument='<%#Eval("OrganizationId") %>' CssClass="btn btn-white btn-bitbucket"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate><%= ResourceMgr.GetMessage("No Record Exists") %></EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>


                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="lnkBtnLocationAdd" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row text-right">
                                    <div class="col-md-12">
                                        <a class="btn btn-white" onclick="GotoPrevStep(2);" href="#"><%= ResourceMgr.GetMessage("Back") %></a>
                                        <a class="btn btn-primary" onclick="GotoNextStep(2,'AdditionalLocation');" href="#"><%= ResourceMgr.GetMessage("Next")%></a>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- tab 2 ends here-->

                    <div id="tab-3" class="tab-pane">
                        <div class="mail-box-header">
                            <div class="pull-right tooltip-demo">
                                <label class="badge badge-primary pull-right use8steps"><%= ResourceMgr.GetMessage("Step 3 of 7")%></label>
                                <label class="badge badge-primary pull-right use4steps" style="display: none;"><%= ResourceMgr.GetMessage("Step 2 of 3")%></label>
                            </div>
                            <h2 class="login_title"><%= ResourceMgr.GetMessage("Stewardships")%></h2>
                        </div>
                        <div class="mail-box">
                            <div class="mail-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <h3><%= ResourceMgr.GetMessage("Stewardship Modules")%> </h3>
                                        <cc1:ResourceCustomValidator ID="ResourceCustomValidator12" ValidationGroup="Stewardships"
                                            runat="server" ClientValidationFunction="ClientValidateCheckBox"
                                            CssClass="custom-block-error" Display="Dynamic" section="dvstModules">
                                            <%= ResourceMgr.GetMessage("Please select at least one Stewardship Module")%>
                                        </cc1:ResourceCustomValidator>
                                        <div id="dvstModules">
                                            <label class="checkbox m-l-md">
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                                <%=ResourceMgr.GetMessage("Tire Trax") %>
                                                <label class="block pl0"><%= ResourceMgr.GetMessage("Scrap Tires")%></label>
                                                <label class="block pl0"><%= ResourceMgr.GetMessage("Scrap Tubes and Flaps")%></label>
                                            </label>
                                            <label class="checkbox m-l-md">
                                                <asp:CheckBox ID="CheckBox19" runat="server" />
                                                <%= ResourceMgr.GetMessage("Tronic Trax")%>
                                                <label class="block pl0"><%= ResourceMgr.GetMessage("E-Waste")%></label>

                                            </label>
                                            <label class="checkbox m-l-md">
                                                <asp:CheckBox ID="CheckBox20" runat="server" />
                                                <%= ResourceMgr.GetMessage("Toxic Trax")%>
                                                <label class="block pl0"><%= ResourceMgr.GetMessage("Paint and Paint Products")%></label>
                                                <label class="block pl0"><%= ResourceMgr.GetMessage("Household Chemicals")%></label>
                                            </label>
                                            <label class="checkbox m-l-md">
                                                <asp:CheckBox ID="CheckBox2" runat="server" />
                                                <%= ResourceMgr.GetMessage("Consumer Trax")%>
                                                <label class="block pl0"><%= ResourceMgr.GetMessage("Home Appliances")%></label>
                                                <label class="block pl0"><%= ResourceMgr.GetMessage("Carpets and Rugs")%></label>
                                                <label class="block pl0"><%= ResourceMgr.GetMessage("Mattresses")%></label>
                                            </label>
                                            <label class="checkbox m-l-md">
                                                <asp:CheckBox ID="CheckBox3" runat="server" />
                                                <%= ResourceMgr.GetMessage("Center Trax")%>
                                                <label class="block pl0"><%= ResourceMgr.GetMessage("Redemption Centers")%></label>
                                                <label class="block pl0"><%= ResourceMgr.GetMessage("Recycling Centers")%></label>
                                            </label>

                                            <div id="dvCertComplainceTrax">
                                                <label class="checkbox m-l-md">
                                                    <asp:CheckBox ID="CheckBox4" runat="server" />
                                                    <%= ResourceMgr.GetMessage("Compliance Trax")%>
                                                    <label class="block pl0"><%= ResourceMgr.GetMessage("Government Agencies")%></label>
                                                    <label class="block pl0"><%= ResourceMgr.GetMessage("Law Enforcement Agencies")%></label>
                                                </label>

                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <h3><%= ResourceMgr.GetMessage("Stewardship Services")%> </h3>
                                        <label class="checkbox m-l-md">
                                            <asp:CheckBox ID="CheckBox22" runat="server" />
                                            TraxExchange.com
                                               <label class="block pl0"><%= ResourceMgr.GetMessage("Global Waste Resource e-Commerce Site")%></label>
                                            <label class="block pl0"><%= ResourceMgr.GetMessage("Global Market Values and Futures Exchange")%></label>
                                        </label>
                                        <label class="checkbox m-l-md">
                                            <asp:CheckBox ID="CheckBox23" runat="server" />
                                            GreenEquation.org
                                              <label class="block pl0"><%= ResourceMgr.GetMessage("Green Business Innovation")%></label>
                                            <label class="block pl0"><%= ResourceMgr.GetMessage("Green Collar Jobs")%></label>
                                            <label class="block pl0"><%= ResourceMgr.GetMessage("ISO 14064-III Carbon Credit Management")%></label>
                                        </label>
                                        <div id="dvCertSTrax">
                                            <label class="checkbox m-l-md">
                                                <asp:CheckBox ID="CheckBox5" runat="server" />
                                                <%= ResourceMgr.GetMessage("S-Trax")%>
                                                <label class="block pl0"><%= ResourceMgr.GetMessage("Global Producer and Product Registry")%></label>
                                                <cc1:ResourceCustomValidator ID="rsvldcusSTrax" ValidationGroup="Stewardships" runat="server"
                                                    ClientValidationFunction="ClientValidateCheckBox"
                                                    CssClass="custom-block-error" Display="Dynamic" section="dvCertSTrax">
                                                <%=ResourceMgr.GetError("Please check mark S-Trax Stewardship Services") %>
                                                </cc1:ResourceCustomValidator>

                                            </label>


                                        </div>

                                    </div>
                                    <div class="col-md-4 ">
                                        <h3><%= ResourceMgr.GetMessage("Community Services")%></h3>
                                        <label class="checkbox m-l-md">
                                            <asp:CheckBox ID="CheckBox26" runat="server" />
                                            <%= ResourceMgr.GetMessage("Trash Tweets")%>
                                            <label class="block pl0"><%= ResourceMgr.GetMessage("Social Media NPO Recycling/Fund-Raising Program")%></label>
                                        </label>

                                        <label class="checkbox m-l-md">
                                            <asp:CheckBox ID="CheckBox28" runat="server" />
                                            <%= ResourceMgr.GetMessage("Trash Trax")%>
                                            <label class="block pl0"><%= ResourceMgr.GetMessage("Home Waste Self-Survey and Solutions")%></label>
                                            <label class="block pl0"><%= ResourceMgr.GetMessage("Business Waste Self-Survey and Solutions")%></label>
                                        </label>

                                        <label class="checkbox m-l-md">
                                            <asp:CheckBox ID="CheckBox6" runat="server" />
                                            <%= ResourceMgr.GetMessage("Resource Trax")%>
                                            <label class="block pl0"><%= ResourceMgr.GetMessage("Home Energy/Water Self-Survey and Solutions")%></label>
                                            <label class="block pl0"><%= ResourceMgr.GetMessage("Business Energy/Water Self-Survey and Solutions")%></label>
                                        </label>
                                    </div>
                                </div>
                                <div class="row text-right">
                                    <div class="col-md-12">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <a onclick="GotoPrevStep(3);" href="#" class="btn btn-white"><%= ResourceMgr.GetMessage("Back")%></a>
                                                <a id="a1" class="btn btn-primary" href="#" style="display: none;"><%= ResourceMgr.GetMessage("Next")%></a>
                                                <cc1:ResourceLinkButton ID="ResourceLinkButton1" ValidationGroup="Stewardships" runat="server"
                                                    OnClick="lnkBtnStep2_Click" CssClass="btn btn-primary"><%= ResourceMgr.GetMessage("Next") %></cc1:ResourceLinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ResourceLinkButton1" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                    <!-- tab3 ends here-->

                    <div id="tab-4" class="tab-pane">
                        <div class="mail-box-header">
                            <div class="pull-right tooltip-demo">
                                <label class="badge badge-primary pull-right use8steps"><%= ResourceMgr.GetMessage("Step 4 of 7")%></label>
                                <label class="badge badge-primary pull-right use7steps" style="display: none;"><%= ResourceMgr.GetMessage("Step 3 of 6")%></label>
                            </div>
                            <h2 class="login_title"><%= ResourceMgr.GetMessage("Stakeholders")%></h2>
                        </div>
                        <div class="mail-box">
                            <div class="mail-body">
                                <div class="row">
                                    <div id="dvstCert1" class="col-md-5">
                                        <!-- Id Changed from dvCert to dvCert1 because navigation was not working -->
                                        <h3><%= ResourceMgr.GetMessage("Stakeholder Certifications")%></h3>
                                        <cc1:ResourceCustomValidator ID="CustomValidator1" runat="server" ValidationGroup="Stakeholders"
                                            ClientValidationFunction="ClientValidateCheckBox"
                                            CssClass="custom-block-error" Display="Dynamic" section="dvstCert">
                                            <%= ResourceMgr.GetError("Please select at least one Stakeholder Certification")%>
                                        </cc1:ResourceCustomValidator>
                                        <div class="m-l-sm m-t-md">
                                            <asp:Repeater ID="rptStakeCertificates" runat="server">
                                                <ItemTemplate>
                                                    <label class="checkbox m-l-md font-noraml">
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CertificationID") %>' />
                                                        <%# DataBinder.Eval(Container.DataItem, "Name") %>
                                                        <span class="label pull-right clearfix"><%= ResourceMgr.GetMessage("Define")%></span>
                                                    </label>

                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>

                                    </div>
                                    <div id="dvstCert" class="col-md-5 col-md-offset-1">
                                        <h3><%= ResourceMgr.GetMessage("Import/Export")%></h3>
                                        <div class="m-l-sm m-t-md">

                                            <label class="checkbox m-l-md font-noraml">
                                                <asp:CheckBox ID="chkImportExportYes" runat="server" />
                                                <%= ResourceMgr.GetMessage("New Products or Materials")%>
                                                <span class="label pull-right clearfix"><%= ResourceMgr.GetMessage("Define")%></span>
                                            </label>

                                            <label class="checkbox m-l-md font-noraml">
                                                <asp:CheckBox ID="chkImportExportNo" runat="server" />
                                                <%= ResourceMgr.GetMessage("Used Products or Materials")%>
                                                <span class="label pull-right clearfix"><%= ResourceMgr.GetMessage("Define")%></span>
                                            </label>

                                            <label class="checkbox m-l-md font-noraml">

                                                <asp:CheckBox ID="chkImportExportNationally" runat="server" /><%= ResourceMgr.GetMessage("Nationally")%>
                                                <span class="label pull-right clearfix"><%= ResourceMgr.GetMessage("Define")%></span>
                                            </label>

                                            <label class="checkbox m-l-md font-noraml">
                                                <asp:CheckBox ID="chkImportExportNAFTA" runat="server" />
                                                <%= ResourceMgr.GetMessage("NAFTA")%>
                                                <span class="label pull-right clearfix"><%= ResourceMgr.GetMessage("Define")%></span>
                                            </label>

                                            <label class="checkbox m-l-md font-noraml">
                                                <asp:CheckBox ID="chkImportExportInternationally" runat="server" />
                                                <%= ResourceMgr.GetMessage("Internationally")%>
                                                <span class="label pull-right clearfix"><%= ResourceMgr.GetMessage("Define")%></span>
                                            </label>

                                        </div>
                                    <div class="hr-line-dashed " style="clear: both;"></div>
                                    <h3><%= ResourceMgr.GetMessage("Government Permits")%></h3>
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Waste Storage Permit #1:")%></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtWasteStoragePermit1" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Waste Storage Permit #2:")%></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtWasteStoragePermit2" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Waste Hauling Permit #1:")%></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtWasteHaulingPermit1" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Waste Hauling Permit #2:")%></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtWasteHaulingPermit2" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <!-- row ends here-->
                                <div class="hr-line-dashed " style="clear: both;"></div>
                                <div class="row text-right">
                                    <div class="col-md-12">
                                        <asp:UpdatePanel ID="upnlStep2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <a onclick="GotoPrevStep(4);" href="#" class="btn btn-white"><%= ResourceMgr.GetMessage("Back")%></a>
                                                <a id="aStep2" class="btn btn-primary" href="#" style="display: none;"><%= ResourceMgr.GetMessage("Next")%></a>
                                                <cc1:ResourceLinkButton ID="lnkBtnStep2" CssClass="btn btn-primary" ValidationGroup="Stakeholders" runat="server"
                                                    OnClick="lnkBtnStep2_Click"><%= ResourceMgr.GetMessage("Next")%></cc1:ResourceLinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="lnkBtnStep2" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- tab4 end here--->
                    <div id="tab-5" class="tab-pane">
                        <asp:UpdatePanel ID="upnlSupplierAdd" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="mail-box-header">
                                    <div class="pull-right tooltip-demo">
                                        <label class="badge badge-primary pull-right"><%= ResourceMgr.GetMessage("Step 5 of 7")%></label>
                                        <label class="badge badge-primary pull-right" style="display: none;"><%= ResourceMgr.GetMessage("Step 4 of 6")%></label>
                                    </div>
                                    <h2 class="login_title"><%= ResourceMgr.GetMessage("Suppliers – Trade References")%></h2>
                                    <cc1:ResourceCustomValidator ID="CustomValidator10" runat="server" ClientValidationFunction="ValidateSupplierCount"
                                        ValidationGroup="WholeStep3" CssClass="custom-block-error"
                                        Display="Dynamic"><%= ResourceMgr.GetError("Please add at least three Suppliers’ references") %></cc1:ResourceCustomValidator>
                                </div>
                                <div class="mail-box">
                                    <div class="mail-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Company Name:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtSupplierCompanyName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator27" runat="server"
                                                                ControlToValidate="txtSupplierCompanyName" InitialValueErrorText="" ValidationGroup="Step3"
                                                                Display="Dynamic" CssClass="custom-block-error">
                                                                <%= ResourceMgr.GetError("Please enter Company Name") %>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <asp:UpdatePanel ID="upnlSupplier" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("ZIP Code:") %></label>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox ID="txtSupplierZipCode" runat="server" CssClass="form-control" onblur="HideSupplierZipCodeLabel();" AutoPostBack="true" OnTextChanged="txtSupplierZipCode_TextChanged" CausesValidation="true" ValidationGroup="SupplierZipCode" MaxLength="10"></asp:TextBox><asp:HiddenField ID="hdnSupplierZipCode" runat="server" Value="" />
                                                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator28" runat="server" ControlToValidate="txtSupplierZipCode"
                                                                        ValidationGroup="Step3" Display="Dynamic" CssClass="custom-block-error">
                                                                        <%= ResourceMgr.GetError("Please enter ZIP Code") %>
                                                                    </cc1:ResourceRequiredFieldValidator>
                                                                    <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator7" runat="server" ControlToValidate="txtSupplierZipCode"
                                                                        ValidationGroup="SupplierZipCode" Display="Dynamic"
                                                                        CssClass="custom-block-error" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$">
                                                                        <%= ResourceMgr.GetError("Please enter valid ZIP Code e.g; 06514, M3C 0C1") %>
                                                                    </cc1:ResourceRegularExpressionValidator>
                                                                    <asp:Label ID="lblSupplierZipCode" runat="server" CssClass="custom-block-error"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("City:")%></label>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox Enabled="false" ID="txtSupplierCity" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                    <%--<cc1:ResourceRequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtSupplierCity" ValidationGroup="Step3" ErrorText="Please select City" Display="Dynamic" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("State:")%></label>
                                                                <div class="col-sm-7">
                                                                    <asp:DropDownList Enabled="false" ID="ddlSuppliersProvince" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    <%-- <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator45" runat="server" ControlToValidate="ddlSuppliersProvince" InitialValueErrorText="0" ValidationGroup="Step3" ErrorText="Please select State" Display="Dynamic" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Country:")%></label>
                                                                <div class="col-sm-7">
                                                                    <asp:DropDownList Enabled="false" ID="ddlSuppliersCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="dllCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
                                                                    <%-- <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator30" runat="server" ControlToValidate="ddlSuppliersCountry" InitialValueErrorText="0" ValidationGroup="Step3" ErrorText="Please select Country" Display="Dynamic" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlSuppliersProvince" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlSuppliersCountry" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtSupplierZipCode" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>

                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Primary Contact Name:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtSupplierscontactFirstName" runat="server" CssClass="form-control" MaxLength="100" placeholder='<%# ResourceMgr.GetError("First Name") %>'></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtSupplierscontactLastName" runat="server" CssClass="form-control" MaxLength="100" placeholder='<%# ResourceMgr.GetError("Last Name") %>'></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator18" runat="server" ValidationGroup="Step3"
                                                                Display="Dynamic" CssClass="custom-block-error"
                                                                ClientValidationFunction="ValidateSupplierContactName">
                                                                <%= ResourceMgr.GetError("Please enter Supplier Contact Name") %>
                                                            </cc1:ResourceCustomValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Business Phone:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtSupplierBusinessPhone1" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtSupplierBusinessPhone2" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtSupplierBusinessPhone3" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator8" runat="server" ValidationGroup="Step3"
                                                                Display="Dynamic" CssClass="custom-block-error" ClientValidationFunction="ValidateSupplierBusinessPhone"> 
                                                                <%=ResourceMgr.GetError("Please enter Business Phone") %></cc1:ResourceCustomValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Extension:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtSupplierBusinessPhoneExt" runat="server" CssClass="form-control" MaxLength="4" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Cell Phone:")%></label>
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtSupplierContactCellPhone1" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtSupplierContactCellPhone2" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtSupplierContactCellPhone3" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <cc1:ResourceCustomValidator ID="ResourceCustomValidator21" runat="server" ValidationGroup="Step3"
                                                                Display="Dynamic" CssClass="custom-block-error"
                                                                ClientValidationFunction="ValidateSupplierCellPhone">
                                                                <%= ResourceMgr.GetError("Please enter Business Phone") %>
                                                            </cc1:ResourceCustomValidator>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Email:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtSuppliersownerEmail" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator32" runat="server"
                                                                ControlToValidate="txtSuppliersownerEmail" ValidationGroup="Step3"
                                                                CssClass="custom-block-error" Display="Dynamic"> <%= ResourceMgr.GetError("Please enter Primary Contact Email")%> </cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator9"
                                                                runat="server" ControlToValidate="txtSuppliersownerEmail"
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Step3"
                                                                CssClass="custom-block-error" Display="Dynamic">
                                                                <%= ResourceMgr.GetError("Please enter valid Primary Contact Email") %>

                                                            </cc1:ResourceRegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="text-right">
                                                    <asp:HiddenField ID="hdnSupplierId" Value="0" runat="server" />
                                                    <cc1:ResourceLinkButton CssClass="btn btn-sm btn-primary" ID="lnkAddSupplier" runat="server" CausesValidation="true" ValidationGroup="Step3"
                                                        OnClick="lnkAddSupplier_Click"><%= ResourceMgr.GetMessage("Add Supplier")%></cc1:ResourceLinkButton>
                                                    <asp:HiddenField ID="hdnSupplierCount" runat="server" Value="0" />
                                                    <br />
                                                    <div class="error_message" runat="server" visible="false" id="dvsuppliererrormsg">
                                                        <%= ResourceMgr.GetError("Please enter valid data. Each supplier should have unique company name,business phone and email.") %>
                                                    </div>
                                                    <asp:Label ID="lblSupplierStatus" ForeColor="Green" runat="server" Visible="false" Text=""></asp:Label>
                                                </div>
                                                <div class="table-responsive m-t-md">
                                                    <asp:GridView ID="gvSupplier" runat="server" GridLines="None" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="table table-bordered epr-sec-table" CellPadding="0" CellSpacing="0" BorderWidth="0" Width="100%" wrap="nowrap" OnRowCommand="gvSupplier_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                        <HeaderStyle CssClass="" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Company Name" DataField="CompanyName" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Country" DataField="CountryName" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="State" DataField="StateName" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="City" DataField="City" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Contact Name" DataField="ContactName" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Business Phone" DataField="BussinessPhone" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Email" DataField="OwnerManagerEmail" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <cc1:ResourceLinkButton ID="lnkbuttonedit" runat="server" CommandName="EditSupplier" CommandArgument='<%#Eval("SupplierId") %>' CssClass="btn btn-white btn-bitbucket"><i class="fa fa-edit"></i></cc1:ResourceLinkButton>
                                                                    <cc1:ResourceLinkButton ID="lnkBtnDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this record.');" CommandName="DeleteSupplier" CommandArgument='<%#Eval("SupplierId") %>' CssClass="btn btn-white btn-bitbucket"><i class="fa fa-trash"></i></cc1:ResourceLinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate><%= ResourceMgr.GetMessage("No Record Exists")%></EmptyDataTemplate>
                                                    </asp:GridView>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row text-right">
                                            <div class="col-md-12">
                                                <a onclick="GotoPrevStep(5);" href="#" class="btn btn-white"><%= ResourceMgr.GetMessage("Back")%></a>
                                                <a onclick="GotoNextStep(5,'Step3');" href="#" class="btn btn-primary"><%= ResourceMgr.GetMessage("Next")%></a>
                                            </div>

                                        </div>

                                    </div>
                                </div>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkAddSupplier" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <!-- tab5 end here--->

                    <div id="tab-6" class="tab-pane ">
                        <div class="mail-box-header">
                            <div class="pull-right tooltip-demo">
                                <label class="badge badge-primary pull-right"><%= ResourceMgr.GetMessage("Step 6 of 7")%></label>
                                <label class="badge badge-primary pull-right" style="display: none;"><%= ResourceMgr.GetMessage("Step 5 of 6")%></label>
                            </div>
                            <h2 class="login_title"><%= ResourceMgr.GetMessage("Clients – Non-Retail Client Trade References")%></h2>
                             <cc1:ResourceCustomValidator ID="ResourceCustomValidator10" runat="server" ClientValidationFunction="ValidateClientCount"
                                ValidationGroup="ClientTab" CssClass="custom-block-error"
                                Display="Dynamic"><%= ResourceMgr.GetError("Please add at least three Clients’ references") %></cc1:ResourceCustomValidator>
                        </div>
                        <div class="mail-box">
                            <div class="mail-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Company Name:")%></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtclientsCompanyName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator33" runat="server"
                                                        ControlToValidate="txtclientsCompanyName" ValidationGroup="Step4"
                                                        Display="Dynamic" CssClass="custom-block-error">
                                                        <%=ResourceMgr.GetError("Please enter Company Name") %>
                                                    </cc1:ResourceRequiredFieldValidator>

                                                </div>
                                            </div>

                                            <asp:UpdatePanel ID="upnlClient" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>

                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("ZIP Code:")%> </label>
                                                        <div class="col-sm-7">

                                                            <asp:TextBox ID="txtClientZipCode" runat="server" CssClass="form-control" onblur="HideClientZipCodeLabel();" AutoPostBack="true" OnTextChanged="txtClientZipCode_TextChanged" CausesValidation="true" ValidationGroup="ClientZipCode" MaxLength="10"></asp:TextBox><asp:HiddenField ID="hdnClientZipCode" runat="server" Value="" />

                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator34" runat="server"
                                                                ControlToValidate="txtClientZipCode" ValidationGroup="Step4"
                                                                Display="Dynamic" CssClass="custom-block-error">
                                                                <%=ResourceMgr.GetError("Please enter ZIP Code") %>
                                                            </cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator10" runat="server" ControlToValidate="txtClientZipCode" ErrorText="Please enter valid ZIP Code e.g; 06514, M3C 0C1" ValidationGroup="Step4" Display="Dynamic" CssClass="custom-block-error" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$"></cc1:ResourceRegularExpressionValidator>
                                                            <%-- <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator35" runat="server" ControlToValidate="txtClientZipCode" ValidationGroup="ClientZipCode" ErrorText="Please enter ZIP Code" CssClass="custom-block-error" Display="None"></cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator11" runat="server" ControlToValidate="txtClientZipCode" ErrorText="Please enter valid ZIP Code e.g; 06514, M3C 0C1" ValidationGroup="ClientZipCode" CssClass="custom-block-error" Display="None" ValidationExpression="^\d{5}$|^([a-zA-Z]\d[a-zA-Z]( )\d[a-zA-Z]\d)$"></cc1:ResourceRegularExpressionValidator>--%>
                                                            <asp:Label ID="lblClientZipCode" runat="server" CssClass="custom-block-error"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("City:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox Enabled="false" ID="txtClientCity" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                            <%-- <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator36" runat="server" ControlToValidate="txtClientCity" ValidationGroup="Step4" ErrorText="Please enter City" Display="Dynamic" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("State:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList Enabled="false" ID="ddlclientsProvince" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            <%--<cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator46" runat="server" ControlToValidate="ddlclientsProvince" ValidationGroup="Step4" InitialValueErrorText="0" ErrorText="Please select State" Display="Dynamic" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Country:")%></label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList Enabled="false" ID="ddlclientsCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="dllCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            <%-- <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator47" runat="server" ControlToValidate="ddlclientsCountry" ValidationGroup="Step4" InitialValueErrorText="0" ErrorText="Please select Country" Display="Dynamic" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>--%>
                                                        </div>
                                                    </div>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlclientsProvince" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlclientsCountry" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="txtClientZipCode" EventName="TextChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Primary Contact Name:")%></label>
                                                <div class="col-sm-7">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtclientsContactFirstName" runat="server" CssClass="form-control" MaxLength="100" placeholder='<%# ResourceMgr.GetError("First Name") %>'></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtclientsContactLastName" runat="server" CssClass="form-control" MaxLength="100" placeholder='<%# ResourceMgr.GetError("Last Name") %>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <cc1:ResourceCustomValidator ID="ResourceCustomValidator19" runat="server" ValidationGroup="Step4"
                                                        Display="Dynamic" CssClass="custom-block-error"
                                                        ClientValidationFunction="ValidateClientContactName">
                                                            <%=ResourceMgr.GetError("Please enter Client Contact Name") %>
                                                    </cc1:ResourceCustomValidator>



                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Business Phone:")%></label>
                                                <div class="col-sm-7">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtClientBusinessPhone1" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtClientBusinessPhone2" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtClientBusinessPhone3" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <cc1:ResourceCustomValidator ID="vldcusClientBusinessPhone" runat="server"
                                                        ValidationGroup="Step4" Display="Dynamic"
                                                        CssClass="custom-block-error" ClientValidationFunction="ValidateClientBusinessPhone">
                                                            <%=ResourceMgr.GetError("Please enter Business Phone") %>
                                                    </cc1:ResourceCustomValidator>



                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Extension:")%></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtClientBusinessPhoneExt" runat="server" CssClass="form-control" MaxLength="4" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Primary Contact Cell Phone:")%></label>
                                                <div class="col-sm-7">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtClientCellPhone1" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtClientCellPhone2" runat="server" CssClass="form-control text-center" MaxLength="3" placeholder="000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtClientCellPhone3" runat="server" CssClass="form-control text-center" MaxLength="4" placeholder="0000" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <cc1:ResourceCustomValidator ID="ResourceCustomValidator11" runat="server"
                                                        ValidationGroup="Step4"
                                                        Display="Dynamic" CssClass="custom-block-error" ClientValidationFunction="ValidateClientCellPhone">
                                                            <%=ResourceMgr.GetError("Please enter Primary Contact Cell Phone") %>
                                                    </cc1:ResourceCustomValidator>



                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label"><%= ResourceMgr.GetMessage("Primary Contact Email:")%></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtclientsEmail" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator38" runat="server" ControlToValidate="txtclientsEmail"
                                                        ValidationGroup="Step4" Display="Dynamic" CssClass="custom-block-error">
                                                        <%=ResourceMgr.GetError("Please enter Primary Contact Email") %></cc1:ResourceRequiredFieldValidator>
                                                    <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator12" runat="server"
                                                        ControlToValidate="txtclientsEmail"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Step4"
                                                        CssClass="custom-block-error" Display="Dynamic">
                                                        <%=ResourceMgr.GetError("Please enter valid Email Address") %>
                                                    </cc1:ResourceRegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <asp:UpdatePanel ID="upnlClientAdd" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="text-right">
                                                    <cc1:ResourceLinkButton ID="lnkAddClient" runat="server"
                                                        CausesValidation="true" ValidationGroup="Step4" CssClass="btn btn-sm btn-primary"
                                                        OnClick="lnkAddClient_Click">
                                                        <%=ResourceMgr.GetMessage("Add Supplier") %>
                                                    </cc1:ResourceLinkButton><asp:HiddenField ID="hdnClientCount" runat="server" Value="0" />
                                                </div>
                                                <div class="table-responsive m-t-md">
                                                    <asp:GridView ID="gvClient" runat="server" GridLines="None" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="table table-bordered epr-sec-table" CellPadding="0" CellSpacing="0" BorderWidth="0" Width="100%" wrap="nowrap" OnRowCommand="gvClient_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                        <HeaderStyle CssClass="" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Company " DataField="CompanyName" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="City" DataField="City" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Contact " DataField="ContactName" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Business " DataField="BussinessPhone" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Extiontion " DataField="Extention" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Email" DataField="OwnerManagerEmail" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:TemplateField HeaderText="Actions" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkBtnDelete" CssClass="btn btn-white btn-bitbucket" runat="server" OnClientClick="return confirm('Are you sure you want to delete this record.');" CommandName="DeleteClient" CommandArgument='<%#Eval("ClientId") %>'>  <i class="fa fa-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate><%# ResourceMgr.GetMessage("RecordExistence")%> </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>


                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkAddClient" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="row text-right">
                                    <div class="col-md-12">
                                        <a class="btn btn-white" onclick="GotoPrevStep(6);" href="#"><%= ResourceMgr.GetMessage("Back")%></a>
                                        <a class="btn btn-primary" onclick="GotoNextStep(6,'Step4');" href="#"><%= ResourceMgr.GetMessage("Next") %></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- tab 6 ends here-->

                    <div id="tab-7" class="tab-pane">
                        <div class="mail-box-header">
                            <div class="pull-right tooltip-demo">
                                <label class="badge badge-primary pull-right use8steps"><%= ResourceMgr.GetMessage("Step 7 of 7") %></label>
                                <label class="badge badge-primary pull-right use7steps" style="display: none;"><%= ResourceMgr.GetMessage("Step 6 of 6")%></label>
                                <label class="badge badge-primary pull-rightuse4steps" style="display: none;"><%= ResourceMgr.GetMessage("Step 3 of 3")%></label>
                                <label class="badge badge-primary pull-rightuse3steps" style="display: none;"><%= ResourceMgr.GetMessage("Step 2 of 2")%></label>
                                <label class="badge badge-primary pull-right" style="display: none;"><%= ResourceMgr.GetMessage("Step 4 of 4")%></label>
                            </div>
                            <h2 class="login_title"><%= ResourceMgr.GetMessage("SubmitApplication")%></h2>
                        </div>
                        <div class="mail-box">
                            <div class="ibox-content ibox-heading">
                                <p class="mb0"><i class="fa fa-exclamation-circle"></i><%= ResourceMgr.GetMessage("Please, review the Terms and Conditions of the Stewardship and EPRTS Privacy agreements before submitting your enrollment application.")%></p>
                            </div>
                            <div class="mail-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="checkbox m-l-md">
                                            <asp:CheckBox ID="chkotsPrivacy" runat="server" />
                                            <%= ResourceMgr.GetMessage("I accept the terms of")%>
                                            <a href="#stewardModal" data-toggle="modal" id="openStewardshipAgr" onclick="visibleDiv('stewardModal')"><%= ResourceMgr.GetMessage("Stewardship Privacy Agreement")%></a>
                                        </label>
                                        <cc1:ResourceCustomValidator ID="CustomValidator4" runat="server" ValidationGroup="SubmitApplication"
                                            ClientValidationFunction="ClientValidateStep6"
                                            CssClass="custom-block-error" Display="Dynamic" Field="chkotsPrivacy">
                                            <%= ResourceMgr.GetError("Please select Steward Privacy Agreement")%>
                                        </cc1:ResourceCustomValidator>
                                        <label class="checkbox m-l-md">
                                            <asp:CheckBox ID="chktiretraxPrivacy" runat="server" />
                                            <%= ResourceMgr.GetMessage("I accept the terms of")%>
                                            <a href="#eprtsModal" data-toggle="modal" id="openprivacyagr" onclick="visibleDiv('eprtsModal')"><%= ResourceMgr.GetMessage("EPRTS Privacy Agreement")%></a>
                                        </label>
                                        <cc1:ResourceCustomValidator ID="CustomValidator5" runat="server" ValidationGroup="SubmitApplication"
                                            ClientValidationFunction="ClientValidateStep6" CssClass="custom-block-error"
                                            Display="Dynamic" Field="chktiretraxPrivacy">
                                            <%= ResourceMgr.GetError("Please select EPRTS Privacy Agreement")%>
                                        </cc1:ResourceCustomValidator>
                                    </div>
                                </div>
                                <div class="row text-right">
                                    <div class="col-md-12">
                                        <a class="btn btn-white" onclick="GotoPrevStep(7);" href="#"><%= ResourceMgr.GetMessage("Back")%></a>
                                        <asp:UpdatePanel ID="upnSubmit" runat="server" UpdateMode="Conditional" style="display: inline-block">
                                            <ContentTemplate>
                                                <cc1:ResourceLinkButton ID="submit" runat="server" CssClass="btn btn-primary" OnClick="submit_Click" CausesValidation="true" OnClientClick="Page_ClientValidate('Step6');" ValidationGroup="SubmitApplication"><%= ResourceMgr.GetMessage("Submit")%></cc1:ResourceLinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="submit" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePrivacyDoc" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="ajaxModal-popup inmodal" id="stewardModal" role="dialog" runat="server" style="display: none">
                                        <div class="ajaxModal-body animated bounceInRight">
                                            <div id="dvParkingLot1" runat="server">
                                                <div class="modal-header">
                                                    <h4 class="modal-title"><%= ResourceMgr.GetMessage(" Stewardship Privacy Agreement")%></h4>
                                                </div>
                                                <div class="modal-body">
                                                    <asp:Literal ID="ltrlstewardshipPrivacyAgreement" runat="server" Text=""></asp:Literal>
                                                </div>
                                                <div class="modal-footer">
                                                    <a id="lnkCancelPermanentLot" class="btn btn-white btn-sm" data-dismiss="modal" onclick="dismissDiv('stewardModal')"><%= ResourceMgr.GetMessage("Close")%></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ajaxModal-popup inmodal" id="eprtsModal" role="dialog" runat="server" style="display: none;">
                                        <div class="ajaxModal-body animated bounceInRight">
                                            <div id="Div2" runat="server">
                                                <div class="modal-header">
                                                    <h4 class="modal-title"><%= ResourceMgr.GetMessage(" EPRTS Privacy Agreement")%></h4>
                                                </div>
                                                <div class="modal-body">
                                                    <asp:Literal ID="ltrlTireTraxPrivacyAgreement" runat="server" Text=""></asp:Literal>
                                                </div>
                                                <div class="modal-footer">
                                                    <a id="ClosePrivacy" class="btn btn-white btn-sm" data-dismiss="modal" onclick="dismissDiv('eprtsModal')"><%= ResourceMgr.GetMessage("Close")%></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <!-- tab 7 ends here-->
                </div>
            </div>
        </div>

    </div>


    <!-- Must Be in MODAL Popup -->
    <div class="ajaxModal-popup inmodal" id="thankyou" style="display: none;">
        <div class="ajaxModal-body animated bounceInRight">
            <div class="modal-header">

                <h4 class="modal-title"><%= ResourceMgr.GetMessage("Thank You")%></h4>

            </div>
            <div class="modal-body">
                <p><%= ResourceMgr.GetMessage("Thank you for submitting your application. Your application has been forwarded for approval. Once your application is approved we will let you know by email.")%></p>
            </div>
            <div class="modal-footer">
                <a class="btn btn-primary" href="/default.aspx"><%= ResourceMgr.GetMessage("Go back to Home Page")%></a>
            </div>
        </div>
    </div>

    <div id="dvLoading" style="position: absolute; display: none;">
        <img src="/images/lightbox-ico-loading.gif" alt="Loading" />
    </div>

</asp:Content>
