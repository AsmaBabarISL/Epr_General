<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="AddInventory.aspx.cs" Inherits="Lots_AddInventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">

        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("form");
            $(".ajax-loader").remove();
        }

        function AjaxLoader() {
            $(".ajax-loader").appendTo("form");
        }

        $(document).ready(function () {
            $(".ajaxModal-popup").appendTo("form");
        });

        function SelectLot(obj) {
            $('#<%=hidSelectedLot.ClientID%>').val(obj);

        }

        function RadioCheckgvAdminInventory(rb) {
            var gv = document.getElementById("<%=gvAdminInventory.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;

            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }

        }
        function RadioCheckgrvPermanentLot(rb) {
            var gv = document.getElementById("<%=grvPermanentLot.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;

            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }

        }
        function RadioCheckgrdSpaces(rb) {
            var gv = document.getElementById("<%=grdSpaces.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;

            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }

        }
        function RadioCheckgvlane(rb) {
            var gv = document.getElementById("<%=gvlane.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;

            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }

        }
        function ShowToolTip1() {

            $(".tooltip_block1").css("display", "");
        }
        function ShowToolTip2() {

            $(".tooltip_block2").css("display", "");
        }
        function ShowToolTip3() {

            $(".tooltip_block3").css("display", "");
        }

        function HideToolTip1() {
            $(".tooltip_block1").css("display", "none");
        }
        function HideToolTip2() {
            $(".tooltip_block2").css("display", "none");
        }
        function HideToolTip3() {
            $(".tooltip_block3").css("display", "none");
        }


        function SetRecycleState(obj) {
            //            if (obj == "2") {
            //                $("#dvRecyleState").show();
            //               
            //            }
            //            else {
            //                $("#dvRecyleState").hide();
            //               
            //            }
        }



        function isAlphaNumeric(evt) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;

            else if (event) keycode = event.keyCode;
            else if (e) keycode = e.which;

            else return true; if ((keycode >= 47 && keycode <= 57) || (keycode >= 65 && keycode <= 90) || (keycode >= 97 && keycode <= 122)) {

                return true;
            }

            else {

                return false;
            }

            return true;
        }


        function isAlpha(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode == 9 || charCode == 8 || charCode == '7F' || charCode == '46' || charCode == '37' || charCode == '39')
                return true;
            else if (!/[a-zA-Z]/.test(String.fromCharCode(charCode)))
                return false;
            return true;
        }

        function isNumeric(evt) {
            //            return true
            //            var charCode = (evt.which) ? evt.which : event.keyCode
            //            if (charCode > 31 && (charCode < 48 || charCode > 57))
            //                return false;

            //            return true;
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;

        }


        function TextToUpper() {
            var $elPlantCode = $("#<%=txtDOTPlant.ClientID%>");
            var $elSizeCode = $("#<%=txtDOTSize.ClientID%>");
            var $elBrandCode = $("#<%=txtDOTBrand.ClientID%>");
            $elPlantCode.val($elPlantCode.val().toUpperCase());
            $elSizeCode.val($elSizeCode.val().toUpperCase());
            $elBrandCode.val($elBrandCode.val().toUpperCase());

        }

        //        Code not in use
        function GenerateBarCode(obj) {
            if (isNaN(obj)) return;
            var $elPlantCode = $("#<%=txtDOTPlant.ClientID%>");
            var $elSizeCode = $("#<%=txtDOTSize.ClientID%>");
            var $elBrandCode = $("#<%=txtDOTBrand.ClientID%>");
            var $elWeekCode = $("#<%=txtDOTWeek.ClientID%>");
            var $elYearCode = $("#<%=txtDOTYear.ClientID%>");
            var $hdnIsPlantCodeValid = $("#<%=hdnIsPlantCodeValid.ClientID%>");
            var $hdnIsSizeCodeValid = $("#<%=hdnIsSizeCodeValid.ClientID%>");
            var $hdnIsYearCodeValid = $("#<%=hdnIsYearCodeValid.ClientID%>");
            var $hdnIsWeekCodeValid = $("#<%=hdnIsWeekCodeValid.ClientID%>");

            $elPlantCode.val($elPlantCode.val().toUpperCase());
            $elSizeCode.val($elSizeCode.val().toUpperCase());
            $elBrandCode.val($elBrandCode.val().toUpperCase());

            if (obj == 1) {
                if ($elPlantCode.val() != "" && $elPlantCode.val() != $elPlantCode.attr("prevValue")) {
                    $("#<%=btnValidatePlantCode.ClientID%>")[0].click();
                    return;
                }
                if ($elPlantCode.val() == $elPlantCode.attr("prevValue")) {
                    return;
                }
            }

            if (obj == 2) {
                if ($elSizeCode.val() != "" && $elSizeCode.val() != $elSizeCode.attr("prevValue")) {
                    $("#<%=btnValidateSizeCode.ClientID%>")[0].click();
                    return;
                }
                if ($elSizeCode.val() == $elSizeCode.attr("prevValue")) {
                    return;
                }
            }

            if (obj == 3) {
                if ($elBrandCode.val() == $elBrandCode.attr("prevValue")) {
                    return;
                }
            }

            if (obj == 4) {
                if ($elWeekCode.val() != "" && $elWeekCode.val() != $elWeekCode.attr("prevValue")) {
                    alert('Week');
                    $("#<%=btnValidateWeek.ClientID%>")[0].click();
                    return;
                }
                if ($elWeekCode.val() == $elWeekCode.attr("prevValue")) {
                    return;
                }
            }

            if (obj == 5) {
                if ($elYearCode.val() != "" && $elYearCode.val() != $elYearCode.attr("prevValue")) {
                    alert('Years');
                    $("#<%=btnValidateYear.ClientID%>")[0].click();
                    return;
                }
                if ($elYearCode.val() == $elYearCode.attr("prevValue")) {
                    return;
                }
            }


            //                $elWeekCode.val() != "" &&
            //                !isNaN($elWeekCode.val()) &&
            //                parseInt($elWeekCode.val()) > 0 &&
            //                parseInt($elWeekCode.val()) < 53 &&
            //                $elYearCode.val() != ""


            if ($elPlantCode.val() != "" &&
                $hdnIsPlantCodeValid.val() == "1" &&
                $hdnIsSizeCodeValid.val() == "1" &&
                $hdnIsYearCodeValid.val() == "1" &&
                $hdnIsWeekCodeValid.val() == "1" &&
                $elSizeCode.val() != "" &&
                $elBrandCode.val() != ""
                    ) {
                $("#<%=btnGenerateBarCode.ClientID%>")[0].click();
            }
        }

        ///Above code is not used


        function ValidateDotNumber(src, args) {
            var $elPlantCode = $("#<%=txtDOTPlant.ClientID%>");
            var $elSizeCode = $("#<%=txtDOTSize.ClientID%>");
            var $elBrandCode = $("#<%=txtDOTBrand.ClientID%>");
            var $elWeekCode = $("#<%=txtDOTWeek.ClientID%>");
            var $elYearCode = $("#<%=txtDOTYear.ClientID%>");

            var $elWeekCodeLength = $("#<%=txtDOTWeek.ClientID%>").val().length;
            var $elYearCodeLength = $("#<%=txtDOTYear.ClientID%>").val().length;
            var $hdnIsPlantCodeValid = $("#<%=hdnIsPlantCodeValid.ClientID%>");
            var $hdnIsSizeCodeValid = $("#<%=hdnIsSizeCodeValid.ClientID%>");
            var $hdnIsWeekCodeValid = $("#<%=hdnIsWeekCodeValid.ClientID%>");
            var $hdnIsYearCodeValid = $("#<%=hdnIsYearCodeValid.ClientID%>");
            $("#<%=lblerror.ClientID%>").html('').hide();

            if ($elPlantCode.val() == "") {
                src.innerHTML = "Please enter Plant Code in DOT Number";
                src.errormessage = "Please enter Plant Code in DOT Number";
                args.IsValid = false;
                return;
            }

            if ($elWeekCodeLength == 1) {

                $("0").appendTo("#<%=txtDOTWeek.ClientID%>").val();
            }
            if ($elYearCodeLength == 1) {

                $("0").appendTo("#<%=txtDOTYear.ClientID%>").val();
            }

            if ($hdnIsPlantCodeValid.val() != "1") {
                src.innerHTML = "Please enter valid Plant Code in DOT Number";
                src.errormessage = "Please enter valid Plant Code in DOT Number";
                args.IsValid = false;
                return;
            }



            if ($elSizeCode.val() == "") {
                src.innerHTML = "Please enter Size Code in DOT Number"
                src.errormessage = "Please enter Size Code in DOT Number";
                args.IsValid = false;
                return;
            }
            if ($hdnIsSizeCodeValid.val() == "") {
                src.innerHTML = "Please enter valid Size Code in DOT Number"
                src.errormessage = "Please enter valid Size Code in DOT Number";
                args.IsValid = false;
                return;
            }
            if ($elBrandCode.val() == "") {
                src.innerHTML = "Please enter Brand in DOT Number";
                src.errormessage = "Please enter Brand in DOT Number";
                args.IsValid = false;
                return;
            }
            if ($elWeekCode.val() == "") {
                src.innerHTML = "Please enter Week in DOT Number";
                src.errormessage = "Please enter Week in DOT Number";
                args.IsValid = false;
                return;
            }
            if (isNaN($elWeekCode.val()) || parseInt($elWeekCode.val()) < 1 || parseInt($elWeekCode.val()) > 52) {
                src.innerHTML = "Please enter valid Week in DOT Number. ie 1-52";
                src.errormessage = "Please enter valid Week in DOT Number. ie 1-52";
                args.IsValid = false;
                return;
            }
            if ($elYearCode.val() == "") {
                src.innerHTML = "Please enter Year in DOT Number";
                src.errormessage = "Please enter Year in DOT Number";
                args.IsValid = false;
                return;
            }
            if ($elYearCode.val().length != 2) {
                src.innerHTML = "Please enter two digit Year in DOT Number. e.g 12";
                src.errormessage = "Please enter two digit Year in DOT Number. e.g 12";
                args.IsValid = false;
                return;
            }
            if ($hdnIsYearCodeValid.val() != "1") {
                src.innerHTML = "Please enter valid Year Code in DOT Number e.g 12";
                src.errormessage = "Please enter valid Year Code in DOT Number e.g 12";
                args.IsValid = false;
                return;
            }

            if ($hdnIsWeekCodeValid.val() != "1") {
                src.innerHTML = "Please enter valid Week Code in DOT Number ie 1-52";
                src.errormessage = "Please enter valid Week Code in DOT Number ie 1-52";
                args.IsValid = false;
                return;
            }
        }





        function ShowOrganizationSelection() {
            $("#dvOrganizationSelection").show();
            $("#dvInventoryAdd").hide();
        }


        function ShowScrapCategory(value) {
            if (value == 1) {
                $("#dvRecyleState").show();
                $("#dvScrapCategories").show();



            } else {
                $("#dvRecyleState").hide();
                $("#dvScrapCategories").hide();

            }
        }

        function HideLotSaveLnk() {
            $('#<%=lnkLotSave.ClientID%>').hide();
        }

        function SelectLot2(obj) {

            $('#<%=hidSelectedLot2.ClientID%>').val(obj);
            $('#<%=lblErrorPermanentLotdv.ClientID%>').text("");

        }

        function SelectSpace(obj) {
            $('#<%=hidSelectedSpace.ClientID%>').val(obj);
            $('#<%=lblErrorPermanentLotSpacedv.ClientID%>').text("");

        }
        function SelectLane(obj) {
            $('#<%=hidSelectedLane.ClientID%>').val(obj);
            $('#<%=lblErrorPermanentLotLanedv.ClientID%>').text("");
        }

        function SetFocusOnStep1() {
            if ($("#<%=txtLotNmber.ClientID %>").val() == "" || $("#<%=txtLotNmber.ClientID %>").val() == $("#<%=txtLotNmber.ClientID %>").attr("WaterMarkText"))
                $("#<%=txtLotNmber.ClientID %>").select().val($("#<%=txtLotNmber.ClientID %>").attr("WaterMarkText")).select();

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


    </script>
    <script type="text/javascript">
        
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="display: none">
        <asp:UpdatePanel ID="upnlAddInventoryForm1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>


                <%  //Close dvLotOption Div becoz we want to hide dvAddInventory Div // %>

                <div id="dvLotOption" class="add-inventory-outer_block" runat="server" style="display: none;">
                    <div class="add-inventory-title_block">
                        <span>
                            <%= ResourceMgr.GetMessage("Inventory Wizard")%></span>
                    </div>

                    <div style="font: 15px arial; font-weight: bold; padding-left: 60px;">
                        Is this Lot a single intake or multiple intake?
                    </div>

                    <div class="new_inventory-block">
                        <div class="inv_title" style="width: 200px;">
                            <asp:LinkButton ID="lnkSingle" CssClass="reg_button" Width="130" runat="server" OnClick="lnkSingle_Click"><%= ResourceMgr.GetMessage("Single")%></asp:LinkButton>
                        </div>
                        <div class="inv_field">
                            <asp:LinkButton ID="lnkMultiple" CssClass="reg_button" Width="130" runat="server"
                                OnClick="lnkMultiple_Click"><%= ResourceMgr.GetMessage("Multiple")%></asp:LinkButton>
                        </div>
                    </div>
                    <div class="new_inventory-block">
                        <div class="inv_title">
                            &nbsp;
                        </div>
                        <div class="inv_field" style="width: 300px;">
                        </div>
                    </div>
                    <br clear="all" />
                </div>




                <%--///////////////////////////////////////////////// ADD LOT SECTION  also hide for dvaddInventory//////////////////////////////////////////////////////////--%>
                <div id="dvLot" class="add-inventory-outer_block" runat="server" style="display: none;">
                    <div class="new_inventory-block" style="display: none;">
                        <div class="inv_title">
                            <%= ResourceMgr.GetMessage("Company Name")%>:
                        </div>
                        <div class="inv_field" style="padding-top: 5px;">
                            <asp:Label ID="txtCompanyName" runat="server"></asp:Label>

                        </div>
                    </div>

                    <div class="new_inventory-block">
                        <div class="inv_title">
                            &nbsp;
                        </div>
                        <div class="inv_field" style="padding-top: 5px; display: none;">
                            <label>
                                <asp:CheckBox ID="chkSublot" runat="server" AutoPostBack="true" OnCheckedChanged="Check_Clicked" />&nbsp;&nbsp;<%= ResourceMgr.GetMessage("Is this a Sub-Lot?")%></label>
                        </div>
                    </div>

                </div>

                <%--////////////////////////////////////////Sub LOT ///////////////////////////////////////////////////--%>


                <div runat="server" id="dvSubLot" visible="false" class="box_blockCmp">
                    <div class="popUp_lotInfo">
                        <div class="popInner_block" style="display: none;"></div>
                        <div class="search-filter-outer" style="display: none;">
                            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnInventorySearch" Visible="false" CssClass="search-filter_inner">
                                <div class="search-filter_heading">
                                    <%= ResourceMgr.GetMessage("Search Filters")%>
                                </div>
                                <div class="search-filter-content-outer">
                                    <div class="content-txt">
                                        <%= ResourceMgr.GetMessage("Serial Number")%>
                                    </div>
                                    <div class="content-field">
                                        <asp:TextBox ID="txtInventoryTX_Barcode" runat="server" CssClass="txt-field"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="search-filter-content-outer">
                                    <div class="content-txt">
                                        <%= ResourceMgr.GetMessage("Organization")%>
                                    </div>
                                    <div class="content-field">
                                        <asp:TextBox ID="txtInventoryStewardship" runat="server" CssClass="txt-field"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="search-filter-content-outer">
                                    <div class="content-txt">
                                        <%= ResourceMgr.GetMessage("Plant Code")%>
                                    </div>
                                    <div class="content-field">
                                        <asp:TextBox ID="txtPlantCode" runat="server" CssClass="txt-field"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="search-filter-content-outer">
                                    <div class="content-txt">
                                        <%= ResourceMgr.GetMessage("Size Code")%>
                                    </div>
                                    <div class="content-field">
                                        <asp:TextBox ID="txtSizeCode" runat="server" CssClass="txt-field"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="search-filter-content-outer">
                                    <div class="content-txt">
                                        <%= ResourceMgr.GetMessage("Tire State")%>
                                    </div>
                                    <div class="content-field">
                                        <asp:DropDownList ID="DropDownList2" runat="server">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Recycle" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Retread" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="btn-search_outer">
                                    <cc1:ResourceLinkButton ID="btnInventoryCancel" runat="server" CssClass="btn-search"
                                        OnClientClick="ClearFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                                    <cc1:ResourceLinkButton ID="btnInventorySearch" runat="server" CssClass="btn-search"
                                        OnClick="btnInventorySearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                </div>
                            </asp:Panel>
                        </div>



                        <asp:HiddenField ID="hidSelectedLot" runat="server" />
                        <div id="dvLot1" runat="server" visible="false">
                            <div class="textTitle">
                                <%= ResourceMgr.GetMessage("Parking LOTS")%>
                            </div>
                            <div style="text-align: center;">
                                <asp:Label ID="Label1" CssClass="custom-error" runat="server"></asp:Label>
                            </div>
                            <div class="main_blockPopup">
                                <asp:GridView ID="gvAdminInventory" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="add-new-inventory" EnableViewState="true" EmptyDataText="No data available"
                                    wrap="nowrap" CellPadding="0" Width="100%" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:HiddenField ID="hidLotId" runat="server" Value='<%# Eval("LotId")%>' />

                                                <%--<input type="checkbox" id="chkhead" onclick="ToggleChilds(this.checked);" />--%>
                                                <%--<asp:CheckBox ID="chkhead" runat="server" onclick="ToggleChilds(this.checked);" />--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--<input type="checkbox" value='<%#Eval("barCodeId") %>' onclick="CheckParent();" />--%>
                                                <%--<asp:CheckBox ID="chkSelect" runat="server" onclick="CheckParent();"/>--%>

                                                <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("LotId")%>' onclick="javascript: SelectLot(this.value); RadioCheckgvAdminInventory(this);" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Lot ID#")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("SerialNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Parking Lots Name")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("LotNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Total Spaces")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("SpaceCount")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Total Lanes")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("LaneCount")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Total Inventory")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("TireCount")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                                <%--<div class="txt-pagination">
                    <div class="pagination-left" style="margin-top: 9px;">
                        <asp:DropDownList runat="server" ID="ddlPageSize" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="75" Value="75"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            <asp:ListItem Text="250" Value="250"></asp:ListItem>
                            <asp:ListItem Text="500" Value="500"></asp:ListItem>
                        </asp:DropDownList>
                        <%=ResourceMgr.GetMessage("Records Per Page")%>
                        <asp:Label ID="lblPagingLeft" runat="server" style="padding-left:10px;"></asp:Label>
                    </div>
                    <div class="pagination-right">
                        <asp:Literal ID="ltrlPaging" runat="server"></asp:Literal>
                    </div>
                </div>--%>
                            </div>
                            <div class="reg_button-outer" style="right: 0px; bottom: 0px;">
                                <asp:LinkButton ID="lnkSubParkingLotCancel" CssClass="reg_button" OnClick="lnkSubParkingLotCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>

                                <%--<a class="reg_button" onclick="ShowOrganizationSelection(); return false;" style="cursor: pointer;">
                            <%= ResourceMgr.GetMessage("Cancel")%></a>--%>
                                <asp:LinkButton ID="lnkSubParkingLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                    CausesValidation="true" CssClass="reg_button" OnClick="lnkSubParkingLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                            </div>
                            <br clear="all" />


                        </div>


                        <div id="dvTires" runat="server" visible="false">
                            <div class="textTitle">
                                <%= ResourceMgr.GetMessage("Tires")%>
                            </div>
                            <div style="text-align: center;">
                                <asp:Label ID="lblerrorTire" CssClass="custom-error" runat="server"></asp:Label>
                            </div>
                            <div class="main_blockPopup">
                                <asp:GridView ID="gvTires" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="add-new-inventory" EnableViewState="true" EmptyDataText="No data available"
                                    wrap="nowrap" CellPadding="0" Width="100%" runat="server">
                                    <Columns>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidTireId" runat="server" Value='<%#Eval("ProductId") %>' />
                                                <asp:CheckBox ID="chkSelectTire" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Serial Number")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("SerialNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Organization")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("LegalName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Plant Code")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("PlantNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Size Code")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("SizeNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Week")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("MonthCode")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Year")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                20<%#Eval("YearCode")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Tire State")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("TireState")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("State Description")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("TireStateDescription")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <%--<div class="txt-pagination">
                    <div class="pagination-left" style="margin-top: 9px;">
                        <asp:DropDownList runat="server" ID="DropDownList3" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged2">
                            <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="75" Value="75"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            <asp:ListItem Text="250" Value="250"></asp:ListItem>
                            <asp:ListItem Text="500" Value="500"></asp:ListItem>
                        </asp:DropDownList>
                        <%=ResourceMgr.GetMessage("Records Per Page")%>
                        <asp:Label ID="lblPagingLeft1" runat="server" style="padding-left:10px;"></asp:Label>
                    </div>
                    <div class="pagination-right">
                        <asp:Literal ID="ltrlPaging1" runat="server"></asp:Literal>
                    </div>
                </div>--%>
                            </div>
                            <div class="reg_button-outer" style="right: 0px; bottom: 0px;">
                                <%--<a class="reg_button" onclick="ShowOrganizationSelection(); return false;" style="cursor: pointer;">
                            <%= ResourceMgr.GetMessage("Cancel")%></a>--%>
                                <asp:LinkButton ID="lnkSubParkingLotTire" CssClass="reg_button" OnClick="lnkSubParkingLotCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>

                                <asp:LinkButton ID="lnkSubParkingLotSaveBtn" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                    CausesValidation="true" CssClass="reg_button" OnClick="lnkSubParkingLotSaveBtn_Click" OnClientClick="$('.box_blockCmp').hide();"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkBackSubLotTire" CssClass="reg_button" OnClick="lnkBackSubLotTire_Click" runat="server"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                            </div>
                            <br clear="all" />
                        </div>
                    </div>


                </div>

                <%    ////////////////////////////  Parking Lot Grid      //////////////////////////////////////////%>

                <div class="box_blockCmp" id="dvPermanentLot" runat="server" visible="false">
                    <div class="popUp_lotInfo">
                        <asp:HiddenField ID="hidSelectedOrgId" runat="server" Visible="false" />
                        <asp:HiddenField ID="hidSelectedLot2" runat="server" />
                        <asp:HiddenField ID="hidSelectedSpace" runat="server" />
                        <asp:HiddenField ID="hidSelectedLane" runat="server" />
                        <asp:HiddenField ID="hidPermanentLotName" runat="server" />
                        <%-- <div onclick="$('#<%=dvPermanentLot.ClientID%>').hide(); $('.box_blockCmp').hide();" class="popInner_block">
                    X
                </div>--%>
                        <div class="popInner_block" style="display: none;"></div>
                        <div id="dvParkingLot1" runat="server">
                            <div class="textTitle">
                                <%= ResourceMgr.GetMessage("Parking LOTS")%>
                            </div>
                            <div style="text-align: center;">
                                <asp:Label ID="lblErrorPermanentLotdv" CssClass="custom-error" runat="server"></asp:Label>
                            </div>
                            <div class="main_blockPopup">
                                <asp:GridView ID="grvPermanentLot" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="add-new-inventory popup_griDst" EnableViewState="true" EmptyDataText="No data available"
                                    wrap="nowrap" CellPadding="0" Width="100%" runat="server">
                                    <Columns>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="20">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("LotId")%>' onclick="javascript: SelectLot2(this.value); RadioCheckgrvPermanentLot(this);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Lot ID#")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("SerialNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Parking Lots Name")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("LotNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Total Spaces")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("SpaceCount")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Total Lanes")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("LaneCount")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Total Inventory")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("TireCount")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="reg_button-outer" style="right: 0px; bottom: 0px;">

                                <asp:LinkButton ID="lnkCancelPermanentLot" CssClass="reg_button" OnClick="lnkCancelLot_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>

                                <asp:LinkButton ID="lnkPermanentLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                    CausesValidation="true" CssClass="reg_button" OnClick="lnkPermanentLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                            </div>
                        </div>

                        <div id="dvSpace" runat="server" visible="false">
                            <div class="textTitle">
                                <%= ResourceMgr.GetMessage("Inventory Lot Rows")%>
                            </div>
                            <div style="text-align: center;">
                                <asp:Label ID="lblErrorPermanentLotSpacedv" CssClass="custom-error" runat="server"></asp:Label>
                            </div>

                            <div class="main_blockPopup">
                                <asp:GridView ID="grdSpaces" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="add-new-inventory" EnableViewState="true" EmptyDataText="No data available"
                                    wrap="nowrap" CellPadding="0" Width="100%" runat="server">
                                    <Columns>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="20">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("intSpaceId")%>' onclick="javascript: SelectSpace(this.value); RadioCheckgrdSpaces(this);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Row Name")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <%# Eval("vchSpaceName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="reg_button-outer" style="right: 0px; bottom: 0px;">
                                <%--<a class="reg_button" onclick="ShowOrganizationSelection(); return false;" style="cursor: pointer;">
                            <%= ResourceMgr.GetMessage("Cancel")%></a>--%>

                                <asp:LinkButton ID="lnkCancelPermanentLotSpace" CssClass="reg_button" OnClick="lnkCancelLot_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkSpacePerLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                    CausesValidation="true" CssClass="reg_button" OnClick="lnkSpacePerLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkBackPermanentLotSpace" CssClass="reg_button" OnClick="lnkBackPermanentLotSpace_Click" runat="server"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                            </div>
                            <br clear="all" />
                        </div>



                        <div id="dvlane" runat="server" visible="false">
                            <div class="textTitle">
                                <%= ResourceMgr.GetMessage("Inventory Lot Spaces")%>
                            </div>
                            <div style="text-align: center;">
                                <asp:Label ID="lblErrorPermanentLotLanedv" CssClass="custom-error" runat="server"></asp:Label>
                            </div>
                            <div class="main_blockPopup">
                                <asp:GridView ID="gvlane" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="add-new-inventory" EnableViewState="true" EmptyDataText="No data available"
                                    wrap="nowrap" CellPadding="0" Width="100%" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="20">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <input id="Radio1" type="radio" name="rbt" value='<%# Eval("intLaneId")%>' onclick="javascript: SelectLane(this.value); RadioCheckgvlane(this);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Space Name")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <%# Eval("nvchLaneName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="reg_button-outer" style="right: 0px; bottom: 0px;">
                                <%--<a class="reg_button" onclick="ShowOrganizationSelection(); return false;" style="cursor: pointer;">
                            <%= ResourceMgr.GetMessage("Cancel")%></a>--%>

                                <asp:LinkButton ID="lnkCancelPermanentLotLane" CssClass="reg_button" OnClick="lnkCancelLot_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkLanePerLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                    CausesValidation="true" CssClass="reg_button" OnClientClick="$('.box_blockCmp').hide();" OnClick="lnkLanePerLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkBackPermanentLotLane" CssClass="reg_button" OnClick="lnkBackPermanentLotLane_Click" runat="server"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                            </div>
                            <br clear="all" />
                        </div>
                    </div>
                </div>




                <% ////////////////////////////////////////////////////////////////////////////////////////////////// %>
                <div id="dvaddinventory" style="display: none;">

                    <div class="new_inventory-block">
                        <div class="inv_title">
                            <%= ResourceMgr.GetMessage("Inventory Lot ID")%>:
                        </div>
                        <div class="inv_field" style="padding-top: 5px;">

                            <asp:TextBox ID="txtLotNmber" runat="server" Enabled="false" CssClass="field_block"></asp:TextBox>

                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkSearchParkingLot" CssClass="lots-btn shPopUp" Style="margin-top: 0px;" OnClick="lnkParkingLot_Click" runat="server"><%= ResourceMgr.GetMessage("Search Lots")%></asp:LinkButton>

                            &nbsp;
                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator7" ValidationGroup="addlot"
                                runat="server" ErrorMessageText="Lot number cannot be empty" ControlToValidate="txtLotNmber"
                                CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
                        </div>
                        <div class="inv_field">

                            <%-- <cc1:ResourceRequiredFieldValidator ID="reqLotNmber" runat="server"
                             ValidationGroup="addlot" ControlToValidate="txtLotNmber" ErrorMessage="*" ForeColor="Red"></cc1:ResourceRequiredFieldValidator>
                            --%>
                        </div>
                    </div>

                    <div class="new_inventory-block">
                        <div class="inv_title">
                            <%= ResourceMgr.GetMessage("Quantity")%>:
                        </div>
                        <div class="inv_field" style="padding-top: 5px;">
                            <asp:TextBox ID="txtQuantity" runat="server" Width="160" Height="20" Text="0" MaxLength="8" CssClass="field_block" onkeypress="return isNumeric(event);"></asp:TextBox>

                            <asp:Literal ID="litSingle" runat="server" Text="Single" Visible="false"></asp:Literal>
                        </div>
                        <%-- <div style="margin-left:154px;">
                         <cc1:ResourceRequiredFieldValidator ID="reqQuantity" runat="server"
                             ValidationGroup="addlot" ControlToValidate="txtQuantity" ErrorMessage="Please enter Quantity" ForeColor="Red"></cc1:ResourceRequiredFieldValidator>
                        </div>--%>
                    </div>

                    <div class="new_inventory-block">
                        <div class="inv_title">
                            <%= ResourceMgr.GetMessage("Date")%>:
                        </div>
                        <div class="inv_field" style="padding-top: 5px;">
                            <asp:TextBox ID="txtdate" runat="server" CssClass="field_block" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="inv_field">
                            &nbsp;
                        </div>
                    </div>
                    <div class="new_inventory-block">
                        <div class="inv_title">
                            <%= ResourceMgr.GetMessage("TTx-Lot ID#")%>:
                        </div>
                        <%--<div class="inv_field">
                            <asp:Image ID="imgLotBarcode" runat="server" Width="550" ImageAlign="AbsMiddle" ImageUrl="" Visible="false"/>&nbsp;
                        </div>--%>
                    </div>
                    <div class="new_inventory-block">
                        <div class="inv_title">
                            &nbsp;
                        </div>
                        <div class="inv_field">
                            <asp:LinkButton ID="lnkCancelAddLot" CssClass="reg_button" runat="server" Text="Cancel" CausesValidation="false" OnClick="lnkCancelAddLot_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnkLotSave" CssClass="reg_button" runat="server" ValidationGroup="addlot" OnClick="lnkLotSave_Click"><%= ResourceMgr.GetMessage("Next")%></asp:LinkButton>


                            <asp:LinkButton ID="lnkContinue" CssClass="reg_button" Visible="false" runat="server" OnClick="lnkContinue_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                        </div>
                    </div>



                    <br clear="all" />
                </div>

                <%--///////////////////////////////////////////////// END  ADD LOT SECTION //////////////////////////////////////////////////////////--%>

                <%--///////////////////////////////////////////////// ADD Inventory SECTION //////////////////////////////////////////////////////////--%>

                <div id="dvInventoryAdd1" runat="server" class="add-inventory-outer_block">
                    <div>
                        &nbsp;
                    </div>
                    <div class="add-inventory-title_block">

                        <span style="margin-top: 0;">
                            <%= ResourceMgr.GetMessage("Add New Inventory")%></span>
                        <!--<div class="signup"> New Here? <a href="#">Signup</a> </div>-->
                    </div>

                    <div>
                        <div style="float: left; margin-right: 10px; margin-top: 20px;">
                            <div style="text-align: center; width: 310px; float: right;">
                            </div>
                            <div class="new_inventory-block">

                                <div class="inv_title title-width_adj" style="line-height: 1.2;">
                                </div>
                                <div class="inv_field">
                                    <br />

                                    <br clear="all">
                                </div>
                            </div>
                            <div class="new_inventory-block">
                                <div class="inv_title title-width_adj">
                                    <%= ResourceMgr.GetMessage("TTx-Barcode")%>:
                                </div>
                                <div class="inv_field" style="height: 61px;">
                                    &nbsp;
                                </div>
                            </div>
                            <%--
                      <asp:UpdatePanel ID="upnlAddInventoryDOT" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
            <ContentTemplate>--%>
                            <div class="new_inventory-block">
                                <div class="inv_title title-width_adj">
                                    <%= ResourceMgr.GetMessage("DOT Number")%>:
                                </div>
                                <div id="dvDotFields" style="position: relative;" class="inv_field">

                                    <asp:Button runat="server" ID="btnGenerateBarCode" OnClick="btnGenerateBarCode_Click"
                                        Style="display: none;" ValidationGroup="GenerateBarCode" CausesValidation="true" />
                                    <asp:Button runat="server" ID="btnValidatePlantCode" OnClick="btnValidatePlantCode_Click"
                                        Style="display: none;" />
                                    <asp:Button runat="server" ID="btnValidateWeek" OnClick="btnValidateWeek_Click"
                                        Style="display: none;" />
                                    <asp:Button runat="server" ID="btnValidateYear" OnClick="btnValidateYear_Click"
                                        Style="display: none;" />
                                    <asp:Button runat="server" ID="btnValidateSizeCode" OnClick="btnValidateSizeCode_Click"
                                        Style="display: none;" />

                                    <%-- <asp:UpdateProgress runat="server" ID="UpdateProgress2" AssociatedUpdatePanelID="upnlAddInventoryDOT">
                                                <ProgressTemplate>
                                                    <div id="dvDotFieldLoading" style="position: absolute; top: -2px; right: -40px; display: none;">
                                                     <img src="/images/lightbox-ico-loading.gif" alt="Loading" />
                                                       </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>--%>
                                    <div id="dvDotFieldLoading" style="position: absolute; top: -2px; right: -40px; display: none;">
                                        <img src="/images/lightbox-ico-loading.gif" alt="Loading" />
                                    </div>
                                </div>
                            </div>
                            <div class="new_inventory-block">
                                <div class="inv_title" style="height: 5px; width: 100px;">
                                    &nbsp;
                                </div>
                                <div class="inv_text inv_textby2">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: -5px;">
                                        <tr>
                                            <td colspan="5" style="border-left: 0px; padding-bottom: 5px; text-align: left;"></td>
                                        </tr>
                                        <tr>
                                            <td width="20%" style="border-left: 0px;">
                                                <%= ResourceMgr.GetMessage("Plant")%>
                                            </td>
                                            <td width="20%" style="border-left: 0px;">
                                                <%= ResourceMgr.GetMessage("Size")%>
                                            </td>
                                            <td width="21%" style="border-left: 0px;">
                                                <%= ResourceMgr.GetMessage("Brand")%>
                                            </td>
                                            <td width="20%" style="border-left: 0px;">
                                                <%= ResourceMgr.GetMessage("Week")%>
                                            </td>
                                            <td width="19%" style="border-left: 0px;">
                                                <%= ResourceMgr.GetMessage("Year")%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <%--   </ContentTemplate>
                   
                    </asp:UpdatePanel>--%>


                            <div class="new_inventory-block">
                                <div class="inv_title title-width_adj">
                                    <%= ResourceMgr.GetMessage("Brand")%>:
                                </div>
                                <div class="inv_field">
                                </div>

                                <div class="inv_icon">
                                    <img onmouseover="ShowToolTip1()" onmouseout="HideToolTip1()" src="/images/idea_icon.png" />
                                </div>
                            </div>
                            <div class="tooltip_block1" style="display: none;">
                                <div class="tooltip_edge">
                                </div>
                                <div class="tooltip_content">
                                    <%--<div class="close-tooltip">
                                <a onclick="HideToolTip()" href="#">
                                    <img src="images/tooltip_close.png" /></a>
                            </div>--%>
                                    <p>
                                        Plant Brand
                                    </p>
                                </div>
                            </div>
                            <div class="new_inventory-block">
                                <div class="inv_title title-width_adj">
                                    <%= ResourceMgr.GetMessage("Brand 2")%>:
                                </div>
                                <div class="inv_field">
                                </div>



                                <div class="inv_icon">
                                    <img onmouseover="ShowToolTip2()" onmouseout="HideToolTip2()" src="/images/idea_icon.png" />
                                </div>
                            </div>
                            <div class="tooltip_block2" style="display: none;">
                                <div class="tooltip_edge">
                                </div>
                                <div class="tooltip_content">
                                    <%--<div class="close-tooltip">
                                <a onclick="HideToolTip()" href="#">
                                    <img src="images/tooltip_close.png" /></a>
                            </div>--%>
                                    <p>
                                        Brand Name
                                    </p>
                                </div>
                            </div>
                            <div class="new_inventory-block">
                                <div class="inv_title title-width_adj">
                                    <%= ResourceMgr.GetMessage("Size")%>:
                                </div>
                                <div class="inv_field">
                                </div>
                                <div class="inv_icon">
                                    <img src="/images/idea_icon.png" onmouseover="ShowToolTip3()" onmouseout="HideToolTip3()" />
                                </div>
                            </div>
                            <div class="tooltip_block3" style="display: none;">
                                <div class="tooltip_edge">
                                </div>
                                <div class="tooltip_content">
                                    <%--<div class="close-tooltip">
                                <a onclick="HideToolTip()" href="#">
                                    <img src="images/tooltip_close.png" /></a>
                            </div>--%>
                                    <p>
                                        Tire Size
                                    </p>
                                </div>
                            </div>
                            <div class="new_inventory-block">
                                <div class="inv_title title-width_adj">
                                    <%= ResourceMgr.GetMessage("Class")%>:
                                </div>
                                <div class="inv_field">
                                </div>
                            </div>

                            <div id="dvScrapCategories" class="new_inventory-block">
                                <div class="inv_title title-width_adj">
                                    <%= ResourceMgr.GetMessage("Action")%>:
                                </div>
                                <div class="inv_field">
                                </div>
                            </div>
                            <div id="dvRecyleState" class="new_inventory-block">
                                <div class="inv_title title-width_adj">
                                    <%= ResourceMgr.GetMessage("Outcome")%>:
                                </div>
                                <div class="inv_field">
                                </div>
                            </div>
                            <div class="reg_button-outer" style="right: 15px; bottom: 0px; clear: both;">
                            </div>

                        </div>

                        <div class="main_blockPopup" style="float: left; border-left: 1px solid #E6E6E6; padding-left: 10px; min-height: 415px; width: 655px;">
                            <%-- <asp:UpdatePanel ID="upnlsearch" runat="server" UpdateMode="Conditional"  >
            <ContentTemplate>--%>

                            <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
                        </div>
                    </div>
                    <br clear="all" />
                </div>



            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvInventoryLoading" style="position: absolute; display: none;">
            <img src="/images/lightbox-ico-loading.gif" alt="Loading" />
        </div>
    </div>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel runat="server" ID="upnlAddInventoryForm" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField ID="hidLotId" runat="server" />

            <div class="row" id="dvInventoryAdd" runat="server" visible="false">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Add New Inventory </h5>


                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-5 b-r" id="dvTire" runat="server">
                                    <div class="form-group">
                                        <label>
                                            <asp:Label ID="lblNumberOfRecord" runat="server" Text="Number Of Record"> </asp:Label>
                                        </label>
                                        <asp:TextBox ID="txtNumberOfRecord" runat="server" class="form-control" MaxLength="2" TabIndex="1" onkeypress="return isNumeric(event);"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator10" runat="server" ControlToValidate="txtNumberOfRecord"
                                            ErrorControlText="This field is required" CssClass="custom-error" ValidationGroup="GenerateBarCode">
                                        </cc1:ResourceRequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="custom-error"
                                            ControlToValidate="txtNumberOfRecord" ErrorMessage="Number Of Record Must Be Greater Then Zero"
                                            Operator="GreaterThan" Type="Integer"
                                            ValueToCompare="0" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            <asp:Label ID="lblCBarcodeText" runat="server" Text=""></asp:Label>
                                        </label>
                                        <asp:TextBox ID="txtCBarCode" runat="server" class="form-control" MaxLength="6" TabIndex="1" onkeypress="return isNumeric(event);"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfvTxtCBarCode" runat="server" ControlToValidate="txtCBarCode"
                                            ErrorControlText="This field is required" CssClass="custom-error" ValidationGroup="GenerateBarCode">
                                        </cc1:ResourceRequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="custom-error"
                                            ControlToValidate="txtCBarCode" ErrorMessage="Barcode Must Be Greater Then Zero"
                                            Operator="GreaterThan" Type="Integer"
                                            ValueToCompare="0" />
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("TTx- Barcode") %></label>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Image ID="imgBarCode" Height="97" runat="server" ImageAlign="AbsMiddle" ImageUrl="" Visible="false"
                                                    Width="250" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("DOT Number") %></label>
                                        <div class="row">
                                            <div class="form-group col-lg-2">
                                                <asp:TextBox ID="txtDOTPlant" runat="server" class="field_dot field_dotby2" prevValue="" AutoPostBack="true" OnTextChanged="btnValidatePlantCode_Click"
                                                    MaxLength="2" onblur="TextToUpper();" onkeypress="return isAlphaNumeric(event);" TabIndex="2"
                                                    placeholder="Plant" CssClass="form-control text-center" CausesValidation="true" ValidationGroup="GenerateBarCode"></asp:TextBox>
                                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" ValidationGroup="GenerateBarCode"
                                                    Display="Dynamic" runat="server" ErrorMessageText="Enter Plant" ControlToValidate="txtDOTPlant"
                                                    CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <asp:TextBox ID="txtDOTSize" runat="server" CssClass="form-control text-center" prevValue="" AutoPostBack="true" OnTextChanged="btnValidateSizeCode_Click"
                                                    MaxLength="2" onblur="TextToUpper();"
                                                    onkeypress="return isAlphaNumeric(event);" TabIndex="2" placeholder="Size" CausesValidation="true" ValidationGroup="GenerateBarCode"></asp:TextBox>
                                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator2" ValidationGroup="GenerateBarCode"
                                                    Display="Dynamic" runat="server" ErrorMessageText="Enter Size" ControlToValidate="txtDOTSize"
                                                    CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3">
                                                <asp:TextBox ID="txtDOTBrand" runat="server" CssClass="form-control text-center" prevValue=""
                                                    MaxLength="4" onkeypress="return isAlphaNumeric(event);" TabIndex="3" onblur="TextToUpper();" AutoPostBack="true" OnTextChanged="btnValidateBrandCode_Click" placeholder="Brand" CausesValidation="true"
                                                    ValidationGroup="GenerateBarCode"></asp:TextBox>
                                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator3" ValidationGroup="GenerateBarCode"
                                                    Display="Dynamic" runat="server" ErrorMessageText="Enter Brand" ControlToValidate="txtDOTBrand"
                                                    CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3">
                                                <asp:TextBox ID="txtDOTWeek" runat="server" CssClass="form-control text-center" prevValue=""
                                                    AutoPostBack="true" OnTextChanged="btnValidateWeek_Click"
                                                    MaxLength="2" onkeypress="return isNumeric(event);" TabIndex="4" placeholder="Week"
                                                    CausesValidation="true">
                                                </asp:TextBox>

                                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator4" ValidationGroup="GenerateBarCode"
                                                    Display="Dynamic" runat="server" ErrorMessageText="Enter Week" ControlToValidate="txtDOTWeek"
                                                    CssClass="custom-error">
                                                </cc1:ResourceRequiredFieldValidator>

                                                <div style="display: block;">
                                                    <%--<cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator1"
                                                runat="server" ControlToValidate="txtDOTWeek" ErrorMessageText="Enter Week i.e 1-52"
                                                 ValidationGroup="GenerateBarCode" Display="Dynamic" ValidationExpression="^([0]?[1-9]{1}|         [1-4]{1}[0-9]{1}|[5]{1}[0-2]{1})$"></cc1:ResourceRegularExpressionValidator>--%>
                                                </div>

                                            </div>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtDOTYear" runat="server" CssClass="form-control text-center" prevValue="" AutoPostBack="true" OnTextChanged="btnValidateYear_Click" placeholder="Year"
                                                    MaxLength="2" onkeypress="return isNumeric(event);" TabIndex="5" CausesValidation="true" ValidationGroup="GenerateBarCode"></asp:TextBox>
                                                <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator5" ValidationGroup="GenerateBarCode"
                                                    Display="Dynamic" runat="server" ErrorMessageText="Enter Year" ControlToValidate="txtDOTYear"
                                                    CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>

                                            </div>
                                            <div class="col-md-12">

                                                <asp:CustomValidator ID="cvDotNumber" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                                    CssClass="custom-error" Display="Dynamic" ClientValidationFunction="ValidateDotNumber">
                                                </asp:CustomValidator>

                                                <cc1:ResourceCompareValidator ID="CompareWeeks" runat="server" ControlToValidate="txtDOTWeek" Operator="LessThanEqual" ValueToCompare="52" ErrorMessageText="Enter week less then or equal to 52." Display="Dynamic" CssClass="custom-error">
                                                </cc1:ResourceCompareValidator>



                                                <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator2" runat="server" ControlToValidate="txtDOTYear" ValidationGroup="GenerateBarCode" Display="Dynamic" ErrorMessageText="Enter Year e.g 12"
                                                    ValidationExpression="^[0-9]{1}[1-9]{1}$" CssClass="custom-error">
                                                </cc1:ResourceRegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Brand") %></label>
                                        <asp:TextBox ID="txtBrand" runat="server" Enabled="false" CssClass="form-control" TabIndex="6" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Brand 2") %></label>
                                        <asp:TextBox ID="txtBrand2" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="btnValidateBrandCode_Click" TabIndex="7"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator6" ValidationGroup="AddInventoryValidationGroup"
                                            Display="Dynamic" runat="server" ErrorMessageText="Please Enter New Brand" ControlToValidate="txtBrand2"
                                            CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Size") %></label>
                                        <asp:TextBox ID="txtSize" CssClass="form-control" runat="server" Enabled="false" TabIndex="8"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Class") %></label>
                                        <asp:DropDownList CssClass="form-control" ID="ddlTireClass" runat="server" TabIndex="9">
                                        </asp:DropDownList>

                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator9" runat="server" InitialValue="0"
                                            ValidationGroup="AddInventoryValidationGroup" ErrorText="Please select Class Type"
                                            ControlToValidate="ddlTireClass" CssClass="custom-error" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Action") %></label>
                                        <asp:DropDownList class="form-control" ID="ddlTireState" runat="server" TabIndex="10">
                                        </asp:DropDownList>
                                        <br />
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator8" runat="server" InitialValue="0"
                                            ValidationGroup="AddInventoryValidationGroup" ErrorText="Please select Action Type"
                                            ControlToValidate="ddlTireState" CssClass="custom-error" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>

                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("OutCome") %></label>
                                        <asp:DropDownList class="form-control" ID="ddlRecycleState" runat="server" TabIndex="11">
                                        </asp:DropDownList>
                                        <cc1:ResourceRequiredFieldValidator ID="rsvldreqddlRecycleState" runat="server" InitialValue="0"
                                            ValidationGroup="AddInventoryValidationGroup" ErrorText="Please select Outcome Type"
                                            ControlToValidate="ddlRecycleState" CssClass="custom-error" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkbtnAddInventory" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                            CausesValidation="true" CssClass="btn btn-sm btn-primary" OnClick="lnkbtnAddInventory_Click" TabIndex="12"><%= ResourceMgr.GetMessage("Add/Next")%></asp:LinkButton>

                                        <asp:LinkButton ID="lnkbtnClearInventory" runat="server"
                                            CausesValidation="true" CssClass="btn btn-sm btn-primary" OnClick="lnkbtnClearInventory_Click" TabIndex="12"><%= ResourceMgr.GetMessage("Clear")%></asp:LinkButton>

                                        <asp:LinkButton ID="lnkCompleted" runat="server" TabIndex="13"
                                            CausesValidation="true" CssClass="btn btn-sm btn-primary" OnClick="lnkbtnComplete_Click"><%= ResourceMgr.GetMessage("Done")%></asp:LinkButton>

                                        <a class="btn btn-sm btn-primary" href="lotinfo">
                                            <%= ResourceMgr.GetMessage("Cancel")%></a>



                                        <asp:LinkButton ID="lnkbtnUpdateInventory" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                            CausesValidation="true" CssClass="btn btn-sm btn-primary" OnClick="lnkbtnUpdateInventory_Click" Visible="false" TabIndex="13"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>

                                    </div>
                                </div>

                                <div class="col-sm-7">
                                    <label class="badge badge-primary inline">
                                        <asp:Label ID="lblLotNumber" runat="server" Text="Lot#"></asp:Label></label>
                                    <div class="ibox-tools">
                                        <asp:Image ID="imgLotBarcode" runat="server" Width="300" ImageAlign="AbsMiddle" ImageUrl="" Visible="false" />
                                    </div>
                                    <div class=" table-responsive clear-both">
                                        <asp:GridView ID="gvAllTire" AutoGenerateColumns="False" GridLines="None" DataKeyNames="Productid" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                            CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data Available"
                                            CellPadding="0" runat="server" OnRowCommand="gvAllTire_RowCommand"
                                            OnRowDataBound="gvAllTire_RowDataBound"
                                            OnRowDeleted="gvAllTire_RowDeleted" OnRowDeleting="gvAllTire_RowDeleting">
                                            <Columns>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblBarcodeheader" runat="server"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("C-BarCode")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("DOT")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdndotnumber" Value='<%#Eval("DOTNumber")%>' runat="server" />
                                                        <asp:Label ID="AllTireDotNumber" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Brand")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Brand")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Class")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("ClassName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Action")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("ActionName")%>
                                                        <%--<asp:Label ID="Label1" runat="server" Text='<%# Eval("RecycleActionText") %> '  Visible='<%#  Eval("RecycleActionText") == null  ? false: true  %>'></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("RetreadActionText") %> '  Visible='<%#  Eval("RetreadActionText") == null  ? false: true  %>'></asp:Label>      --%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Outcome")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("OutComeName")%>
                                                        <%--  <asp:Label ID="Label3" runat="server" Text='<%# Eval("RecycleOutComeText") %> '  Visible='<%#  Eval("RecycleOutComeText") == null  ? false: true  %>'></asp:Label>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("RetreadOutComeText") %> '  Visible='<%#  Eval("RetreadOutComeText") == null  ? false: true  %>'></asp:Label>      
                                                        --%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("SKU")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("SerialNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" FooterStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgEditTire" runat="server" CommandName="LoadTireInfo" CommandArgument='<%# Bind("Productid") %>' CssClass="btn btn-white btn-bitbucket">
                                            <i class="fa fa-edit" ></i> </asp:LinkButton>
                                                        <asp:LinkButton ID="imgDeleteTire" runat="server" OnClientClick="javascript:return confirm('Do you want to Delete this record?');" CommandName="DeleteSP" CommandArgument='<%# Bind("Productid") %>' CssClass="btn btn-white btn-bitbucket">
                                            <i class="fa fa-trash"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>


            <div class="row" id="dvProducts" runat="server" visible="false">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Add New Inventory </h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">

                                <div class="col-sm-5 b-r" id="">

                                    <div class="form-group" runat="server" id="dvSubCategory" visible="false">
                                        <label><%= ResourceMgr.GetMessage("Sub Category") %></label>
                                        <asp:DropDownList runat="server" ID="ddlSubCategory" CssClass="form-control" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <cc1:ResourceRequiredFieldValidator ID="rfvSubCategory" runat="server" ControlToValidate="ddlSubCategory" ErrorControlText="This field is required" CssClass="custom-error" Enabled="false" ValidationGroup="GenerateBarCodeForProduct">
                                        </cc1:ResourceRequiredFieldValidator>
                                        <asp:Label runat="server" ID="lblSubCatError" CssClass="custom-error"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            <asp:Label ID="lblProductsRecords" runat="server" Text="Number Of Copies"> </asp:Label>
                                        </label>
                                        <asp:TextBox ID="txtProductRecordNumber" runat="server" class="form-control" MaxLength="2" TabIndex="1" onkeypress="return isNumeric(event);"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator11" runat="server" ControlToValidate="txtProductRecordNumber"
                                            ErrorControlText="This field is required" CssClass="custom-error" ValidationGroup="GenerateBarCodeForProduct">
                                        </cc1:ResourceRequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" CssClass="custom-error"
                                            ControlToValidate="txtProductRecordNumber" ErrorMessage="Number Of Record Must Be Greater Then Zero"
                                            Operator="GreaterThan" Type="Integer"
                                            ValueToCompare="0" ValidationGroup="GenerateBarCodeForProduct" />
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Product Size") %></label>
                                        <asp:DropDownList runat="server" ID="ddlSize" CssClass="form-control"></asp:DropDownList>
                                        <cc1:ResourceRequiredFieldValidator runat="server" ID="rfvSize" CssClass="custom-error" ControlToValidate="ddlSize" ErrorMessage="Please select Product Size" InitialValue="0" ValidationGroup="GenerateBarCodeForProduct"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Product Shape") %></label>
                                        <asp:DropDownList runat="server" ID="ddlShape" CssClass="form-control"></asp:DropDownList>
                                        <cc1:ResourceRequiredFieldValidator runat="server" ID="ResourceRequiredFieldValidator14" CssClass="custom-error" ControlToValidate="ddlShape" ErrorMessage="Please select Product Shape" InitialValue="0" ValidationGroup="GenerateBarCodeForProduct"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Product Material") %></label>
                                        <asp:DropDownList runat="server" ID="ddlMaterial" CssClass="form-control"></asp:DropDownList>
                                        <cc1:ResourceRequiredFieldValidator runat="server" ID="rfvProductMaterial" CssClass="custom-error" ControlToValidate="ddlMaterial" ErrorMessage="Please select Product Material" InitialValue="0" ValidationGroup="GenerateBarCodeForProduct"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Brand") %></label>
                                        <asp:DropDownList runat="server" ID="ddlBrand" CssClass="form-control"></asp:DropDownList>
                                        <cc1:ResourceRequiredFieldValidator runat="server" ID="ResourceRequiredFieldValidator12" CssClass="custom-error" ControlToValidate="ddlBrand" ErrorMessage="Please select Product Brand" InitialValue="0" ValidationGroup="GenerateBarCodeForProduct"></cc1:ResourceRequiredFieldValidator>
                                        <%--<div class="col-md-2" style="margin-top: 18px;">
                                                <asp:LinkButton runat="server" ID="lnkAddBrand" ToolTip="Add Brand" CssClass="btn btn-white btn-bitbucket" OnClick="lnkAddBrand_Click"><i class="fa fa-plus"></i></asp:LinkButton>
                                            </div>--%>
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Field 1") %></label>
                                        <asp:TextBox runat="server" ID="txtField1" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Field 2") %></label>
                                        <asp:TextBox runat="server" ID="txtField2" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label><%= ResourceMgr.GetMessage("Field 3") %></label>
                                        <asp:TextBox runat="server" ID="txtField3" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:HiddenField ID="hdnProductBrandId" runat="server" />
                                        <asp:HiddenField ID="hdnProdcutWeekCodeValid" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdnProductYearCodeValid" runat="server" Value="0" />
                                    </div>
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkbtnAddProductInventory" runat="server" ValidationGroup="GenerateBarCodeForProduct"
                                            CausesValidation="true" CssClass="btn btn-sm btn-primary" OnClick="lnkbtnAddProductInventory_Click" TabIndex="12"><%= ResourceMgr.GetMessage("Add/Next")%></asp:LinkButton>

                                        <asp:LinkButton ID="lnkBtnClearProductFiedls" runat="server"
                                            CausesValidation="true" CssClass="btn btn-sm btn-primary" OnClick="lnkBtnClearProductFiedls_Click" TabIndex="12"><%= ResourceMgr.GetMessage("Clear")%></asp:LinkButton>

                                        <asp:LinkButton ID="lnkbtnDoneProduct" runat="server" TabIndex="13"
                                            CausesValidation="true" CssClass="btn btn-sm btn-primary" OnClick="lnkbtnDoneProduct_Click"><%= ResourceMgr.GetMessage("Done")%></asp:LinkButton>

                                        <a class="btn btn-sm btn-primary" href="lotinfo">
                                            <%= ResourceMgr.GetMessage("Cancel")%></a>



                                        <asp:LinkButton ID="lnkBtnUpdateProduct" runat="server" ValidationGroup="GenerateBarCodeForProduct"
                                            CausesValidation="true" CssClass="btn btn-sm btn-primary" OnClick="lnkBtnUpdateProduct_Click" Visible="false" TabIndex="13"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnCancelUpdate" runat="server" TabIndex="13"
                                            CausesValidation="true" CssClass="btn btn-sm btn-primary" OnClick="lnkBtnCancelUpdate_Click" Visible="false"><%=        ResourceMgr.GetMessage("Cancel Update")%></asp:LinkButton>
                                    </div>
                                    <div class="form-group">
                                    </div>
                                </div>

                                <div class="col-sm-7">
                                    <label class="badge badge-primary inline">
                                        <asp:Label ID="lblProductLot" runat="server" Text="Lot#"></asp:Label></label>
                                    <div class="ibox-tools">
                                        <asp:Image ID="imgProductLot" runat="server" Width="300" ImageAlign="AbsMiddle" ImageUrl="" Visible="false" />
                                    </div>
                                    <div class=" table-responsive clear-both">
                                        <asp:GridView ID="gvProduct" AutoGenerateColumns="False" GridLines="None" DataKeyNames="Productid" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                            CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data Available"
                                            CellPadding="0" runat="server" OnRowCommand="gvProduct_RowCommand">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Serial Number") %>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("SerialNumber").ToString().Split('.')[0] %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Product Type") %>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("CatName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Product Sub Type") %>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ProductSubType")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Size")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("Size")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Shape")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("Shape")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Material")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("Material")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Brand")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("BrandName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Wrap="false" FooterStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgEditProduct" runat="server" CommandName="LoadProductInfo" CommandArgument='<%# Bind("Productid") %>' CssClass="btn btn-white btn-bitbucket">
                                            <i class="fa fa-edit" ></i> </asp:LinkButton>
                                                        <asp:LinkButton ID="imgDeleteProduct" runat="server" OnClientClick="javascript:return confirm('Do you want to Delete this record?');" CommandName="DeleteSP" CommandArgument='<%# Bind("Productid") %>' CssClass="btn btn-white btn-bitbucket">
                                            <i class="fa fa-trash"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <asp:Label ID="lblErrorExistCBarCode" runat="server" CssClass="custom-absolute-alert alert-danger"></asp:Label>
            <asp:Label ID="lblErrorSuccessInventory" Visible="false" runat="server" CssClass="custom-absolute-alert alert-success"></asp:Label>
            <asp:Label ID="lblErrorAddInventory" runat="server" CssClass="custom-absolute-alert alert-danger"></asp:Label>
            <asp:Label ID="lblerror" runat="server" CssClass="custom-absolute-alert alert-danger"></asp:Label>
            <asp:Label ID="lblProductWeekError" runat="server" CssClass="custom-absolute-alert alert-danger"></asp:Label>

            <asp:HiddenField runat="server" ID="hdnLotBarCodeImageFileName" />
            <asp:HiddenField runat="server" ID="hdnBarCodeImageFileName" />
            <asp:HiddenField runat="server" ID="hdnIsPlantCodeValid" Value="0" />
            <asp:HiddenField runat="server" ID="hdnIsSizeCodeValid" Value="0" />
            <asp:HiddenField runat="server" ID="hdnIsWeekCodeValid" Value="0" />
            <asp:HiddenField runat="server" ID="hdnIsYearCodeValid" Value="0" />
            <asp:HiddenField runat="server" ID="hdnOldTXBarcodeID" />
            <asp:HiddenField runat="server" ID="hdnBrand2ID" />
            <asp:HiddenField runat="server" ID="hdnTireID" />
            <asp:HiddenField runat="server" ID="hdnProductId" />

            <div class="ajaxModal-popup inmodal" id="dvBrand" visible="false" runat="server">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title"><%= ResourceMgr.GetMessage("Add Brand")%></h4>
                    </div>
                    <div class="modal-body">
                        <div role="form" class="row search-filter" id="">
                            <div class="form-group col-md-12 mb0">
                                <label>
                                    <asp:Label ID="lblCreatingTextDisplay" runat="server" Text="Enter New Brand Name: "></asp:Label></label>
                                <asp:TextBox ID="txtBrandName" runat="server" MaxLength="100" class="form-control" onkeypress="return IsAlphaNumeric1(event);"></asp:TextBox>
                                <cc1:ResourceRequiredFieldValidator ID="rfvAcount" ValidationGroup="Brand"
                                    CssClass="custom-error" runat="server" ErrorText="Enter Brand" ControlToValidate="txtBrandName"
                                    Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="lnkSaveBrand" CssClass="btn btn-primary" runat="server"
                            Text="Add" CausesValidation="true" ValidationGroup="Brand"
                            OnClick="lnkSaveBrand_Click"></asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnClear" CssClass="btn btn-white" runat="server"
                            Text="Close" CausesValidation="false" OnClick="lnkbtnClear_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AddInventoryForm_BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddInventoryForm_EndRequestHandler);

        function AddInventoryForm_BeginRequestHandler(sender, args) {
            if (args._postBackElement.id.indexOf("btnGenerateBarCode") != -1 || args._postBackElement.id.indexOf("btnValidatePlantCode") != -1 || args._postBackElement.id.indexOf("btnValidateSizeCode") != -1) {
                $("#dvDotFieldLoading").show();
            }
            else if (args._postBackElement.id.indexOf("txtDOTPlant") != -1 || args._postBackElement.id.indexOf("txtDOTSize") != -1 || args._postBackElement.id.indexOf("txtDOTBrand") != -1 || args._postBackElement.id.indexOf("txtDOTWeek") != -1 || args._postBackElement.id.indexOf("txtDOTYear") != -1) {
                $("#dvDotFieldLoading").show();
            }
            else {
                $("#dvInventoryLoading").css("top", $("#" + args._postBackElement.id).offset().top - 160);
                $("#dvInventoryLoading").css("left", $("#" + args._postBackElement.id).offset().left + $("#" + args._postBackElement.id).width() - 80);
            }
        }

        function AddInventoryForm_EndRequestHandler(sender, args) {

            $("#dvInventoryLoading").hide();
            $("#dvDotFieldLoading").hide();
        }
    </script>

</asp:Content>

