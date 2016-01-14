<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="EditTire.aspx.cs" Inherits="Lots_EditTire" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="http://code.jquery.com/ui/1.11.0/jquery-ui.js">
    </script>
    <script type="text/javascript">
        function ClearFields() {
            $("#<%=txtInventoryDotNumber.ClientID%>").val('');
            $("#<%=txtTireSize.ClientID%>").val('');
            $("#<%=txtBrandName.ClientID%>").val('');
            $("#<%=txtBarcode.ClientID%>").val('');
            $("#<%=txtSpaceName.ClientID%>").val('');
            $("#<%=txtUserName.ClientID%>").val('');
            $("#<%=txtLaneName.ClientID%>").val('');
            $("#<%=txtLotNumber.ClientID%>").val('');
            $("#<%=txttodate.ClientID%>").val('');
            $("#<%=txtFromDate.ClientID%>").val('');
            $("#<%=btnInventorySearch.ClientID%>")[0].click();
        }

        function ClearFieldsProduct() {
            $("#<%=txtBrandNameProduct.ClientID%>").val('');
            $("#<%=txtSizeProduct.ClientID%>").val('');
            $("#<%=txtShapeProduct.ClientID%>").val('');
            $("#<%=txtMaterialProduct.ClientID%>").val('');
            $("#<%=txtToDateProduct.ClientID%>").val('');
            $("#<%=txtFromDateProduct.ClientID%>").val('');
            $("#<%=btnProductSearch.ClientID%>")[0].click();
        }


        function TextToUpper() {

            var $elPlantCode = $("#<%=txtDOTPlant.ClientID%>");
            var $elSizeCode = $("#<%=txtDOTSize.ClientID%>");
            var $elBrandCode = $("#<%=txtDOTBrand.ClientID%>");
            $elPlantCode.val($elPlantCode.val().toUpperCase());
            $elSizeCode.val($elSizeCode.val().toUpperCase());
            $elBrandCode.val($elBrandCode.val().toUpperCase());

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
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;

        }
        function GenerateBarCode(obj) {
            if (isNaN(obj)) return;
            var $elPlantCode = $("#<%=txtDOTPlant.ClientID%>");
            var $elSizeCode = $("#<%=txtDOTSize.ClientID%>");
            var $elBrandCode = $("#<%=txtDOTBrand.ClientID%>");
            var $elWeekCode = $("#<%=txtDOTWeek.ClientID%>");
            var $elYearCode = $("#<%=txtDOTYear.ClientID%>");
            var $hdnIsPlantCodeValid = $("#<%=hdnIsPlantCodeValid.ClientID%>");
            var $hdnIsSizeCodeValid = $("#<%=hdnIsSizeCodeValid.ClientID%>");

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
                if ($elWeekCode.val() == $elWeekCode.attr("prevValue")) {
                    return;
                }
            }

            if (obj == 5) {
                if ($elYearCode.val() == $elYearCode.attr("prevValue")) {
                    return;
                }
            }

            if ($elPlantCode.val() != "" &&
                $hdnIsPlantCodeValid.val() == "1" &&
                $hdnIsSizeCodeValid.val() == "1" &&
                $elSizeCode.val() != "" &&
                $elBrandCode.val() != "" &&
                $elWeekCode.val() != "" &&
                !isNaN($elWeekCode.val()) &&
                parseInt($elWeekCode.val()) > 0 &&
                parseInt($elWeekCode.val()) < 53 &&
                $elYearCode.val() != "") {
                $("#<%=btnGenerateBarCode.ClientID%>")[0].click();
            }
        }
        function ValidateDotNumber(src, args) {
            var $elPlantCode = $("#<%=txtDOTPlant.ClientID%>");
            var $elSizeCode = $("#<%=txtDOTSize.ClientID%>");
            var $elBrandCode = $("#<%=txtDOTBrand.ClientID%>");
            var $elWeekCode = $("#<%=txtDOTWeek.ClientID%>");
            var $elYearCode = $("#<%=txtDOTYear.ClientID%>");
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

        function rmSeccesMsg() {

            $("#<%=lblTireUpdate.ClientID%>").delay(3000).fadeOut(300);
        };

        function fadeOut() {
            $(".custom-absolute-alert").delay(3000).fadeOut(300);
            $(".custom-absolute-alert").appendTo("form");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>


    <div>
        <!-- Search Panel For Tires Start -->
        <asp:Panel ID="pnlSearchTire" runat="server" DefaultButton="btnInventorySearch">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Search Filters")%> </h5>
                            <div class="ibox-tools">
                                <a class="ico_add" id="a5" href="addInventory" style="display: none;">
                                    <%= ResourceMgr.GetMessage("Add")%>
                                </a>


                            </div>
                        </div>
                        <!--Start of Search Filters -->
                        <div class="ibox-content" style="display: block;">
                            <div role="form" class="row search-filter" id="">
                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("TTx-SKU")%></label>
                                    <asp:TextBox ID="txtBarcode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("Size")%></label>
                                    <asp:TextBox ID="txtTireSize" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("Brand")%></label>
                                    <asp:TextBox ID="txtBrandName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("User Name")%></label>
                                    <asp:TextBox ID="txtUserName" runat="server" class="form-control"></asp:TextBox>
                                </div>

                                <div id="date_range">
                                    <div class="input-daterange">
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("From")%></label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("To")%> </label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("Lot #")%> </label>
                                    <asp:TextBox ID="txtLotNumber" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("Row #")%></label>
                                    <asp:TextBox ID="txtSpaceName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("Space #")%> </label>

                                    <asp:TextBox ID="txtLaneName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label>
                                        <%= ResourceMgr.GetMessage("DOT")%>
                                    </label>
                                    <asp:TextBox ID="txtInventoryDotNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-12 mb0">
                                    <cc1:ResourceLinkButton ID="btnInventorySearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                        OnClick="btnInventorySearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                    <cc1:ResourceLinkButton ID="btnInventoryCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
                                        OnClientClick="ClearFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                                </div>
                            </div>
                        </div>
                        <!--End of Search Filters -->
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!-- Search Panel For Tires End -->

        <!-- Search Panel For Products Start -->
        <asp:Panel ID="pnlSearchProduct" runat="server" DefaultButton="btnProductSearch">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Search Filters")%> </h5>
                        </div>
                        <!--Start of Search Filters -->
                        <div class="ibox-content" style="display: block;">
                            <div role="form" class="row search-filter" id="">
                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("Brand Name")%></label>
                                    <asp:TextBox ID="txtBrandNameProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("Size")%></label>
                                    <asp:TextBox ID="txtSizeProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("Shape")%></label>
                                    <asp:TextBox ID="txtShapeProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("Material")%></label>
                                    <asp:TextBox ID="txtMaterialProduct" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div id="date_range_p">
                                    <div class="input-daterange">
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("From")%></label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFromDateProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("To")%> </label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtToDateProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="form-group col-md-12 mb0">
                                    <cc1:ResourceLinkButton ID="btnProductSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                        OnClick="btnInventorySearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                    <cc1:ResourceLinkButton ID="btnProductCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
                                        OnClientClick="ClearFieldsProduct(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                                </div>
                            </div>
                        </div>
                        <!--End of Search Filters -->
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!-- Search Panel For Products End -->

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

            <ContentTemplate>
                <div>
                    <asp:Label ID="lblTireUpdate" runat="server" Text="Inventory updated Successfully!" CssClass="alert-success custom-absolute-alert" Visible="false"></asp:Label>
                </div>

                <div class="row" id="TireDiv" runat="server">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Edit Inventory")%></h5>
                                <div class="ibox-tools">
                                    <div class="form-group">
                                        <asp:LinkButton ID="MoreTire" runat="server" CssClass="btn btn-sm btn-primary font-bold" OnClick="LinkButton1_Click">
                                        <i class="fa fa-plus"></i> Add More Inventory
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvAdminInventory" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table"
                                                EnableViewState="true" EmptyDataText="No data Available" runat="server" OnRowCommand="gvAdminInventory_RowCommand"
                                                OnRowEditing="gvAdminInventory_RowEditing" OnRowDataBound="gvAdminInventory_RowDataBound"
                                                EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Size")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("ProductSize")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Brand")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("BrandName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("DOT")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdndotnumber" runat="server" Value='<%#Eval("DOTNumber")%>' />
                                                            <asp:Label ID="lbldotnumber" runat="server"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("COO")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("Abbreviation")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Status")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("Status")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Days")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("Days") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Location")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("Location")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("TTx-SKU")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("BarcodeLast")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Edit")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="ImageButton1" CssClass="btn btn-white btn-bitbucket" runat="server" ToolTip="Edit Tire"
                                                                OnClientClick="$('.box_blockCmp').show();" CommandName="EditTireInfo" CommandArgument='<%# Bind("ProductId") %>'>
                                                                <i class ="fa fa-edit"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </div>
                                <div class=" row">
                                    <div class="col-md-12">
                                        <div class="txt-pagination">
                                            <uc2:Pager ID="pager" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <div class="row" id="ProductDiv" runat="server">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Edit Inventory")%></h5>
                                <div class="ibox-tools">
                                    <div class="form-group">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-primary font-bold" OnClick="LinkButton1_Click">
                                        <i class="fa fa-plus"></i> Add More Inventory
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <div>
                                                <asp:Label ID="lblProductUpdate" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <asp:GridView ID="gvProductInventory" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table"
                                                EnableViewState="true" EmptyDataText="No data Available" runat="server" OnRowCommand="gvProductInventory_RowCommand"
                                                EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Brand Name")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("BrandName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Size")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("Size")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Shape")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("Shape")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Material")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("Material")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Status")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label CssClass="badge" ID="Label2" runat="server" Text='<%# Convert.ToBoolean(Eval("bitCompleted"))? (Convert.ToBoolean(Eval("bitPermanent"))?"Completed":"Completed-Temp"):"Completed" %>'
                                                                Visible='<%# Convert.ToBoolean(Eval("bitCompleted"))? Convert.ToBoolean(1):Convert.ToBoolean(0) %>'></asp:Label>
                                                            <asp:LinkButton CssClass="badge badge-primary" ID="LinkButton1" runat="server" CommandArgument='<%# Bind("LotId") %>'
                                                                CommandName="Edit" Visible='<%# Convert.ToBoolean(Eval("bitCompleted"))? Convert.ToBoolean(0):Convert.ToBoolean(1) %>'><%=ResourceMgr.GetMessage("Open")%></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Days")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("Days")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Edit")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="ImageButton1" CssClass="btn btn-white btn-bitbucket" runat="server" ToolTip="Edit Product"
                                                                OnClientClick="$('.box_blockCmp').show();" CommandName="EditProductInfo" CommandArgument='<%# Bind("ProductId") %>'>
                                                                <i class ="fa fa-edit"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                        </div>

                                    </div>
                                </div>
                                <div class=" row">
                                    <div class="col-md-12">
                                        <div class="txt-pagination">
                                            <uc2:Pager ID="pagerProduct" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--  Update Inventory Pop Div--%>

                <div id="dvInventoryUpdate" runat="server" class="ajaxModal-popup inmodal" visible="false">
                    <div class="ajaxModal-body animated bounceInRight">
                        <div class="modal-header text-left pb0">
                            <div class="row">
                                <div class="col-md-6 col-sm-6">
                                    <h4 class=" modal-title text-left ">
                                        <%= ResourceMgr.GetMessage("Update Inventory")%></h4>
                                    <span class="label label-primary pull-left fSize-lrg">
                                        <asp:Label ID="lblLotNumber" runat="server" Text="Lot#"></asp:Label>
                                    </span>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="barcode-pane text-right">
                                        <asp:Image ID="imgTireBarcode" runat="server" ImageUrl="" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="row">

                                <div class="col-md-6 col-sm-6 form-group barcode-pane2">
                                    <label><%= ResourceMgr.GetMessage("TTx-Barcode")%>:</label>
                                    <asp:Image ID="imgBarCode" runat="server" ImageAlign="AbsMiddle" ImageUrl="" Visible="false" CssClass="img-responsive" />
                                </div>
                                <div class="col-md-6 col-sm-6 form-group">
                                    <label><%= ResourceMgr.GetMessage("C Barcode")%>:</label>
                                    <asp:TextBox ID="txtCBarCode" runat="server" TabIndex="1" onkeypress="return isNumeric(event);" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                </div>
                                <div class="col-md-6 col-sm-6 form-group">
                                    <label>
                                        <%= ResourceMgr.GetMessage("Brand")%>:
                                    </label>
                                    <asp:TextBox ID="txtBrand" runat="server" Enabled="false" TabIndex="7" CssClass="form-control" ToolTip="Plant Brand"></asp:TextBox>

                                </div>
                                <div class="col-md-12">
                                    <label><%= ResourceMgr.GetMessage("DOT Number")%>:</label>
                                    <asp:CustomValidator ID="cvDotNumber" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                        CssClass="error_message" Display="Dynamic" ClientValidationFunction="ValidateDotNumber"></asp:CustomValidator>
                                </div>

                                <div class="col-md-3 col-sm-3 form-group">
                                    <asp:TextBox ID="txtDOTPlant" runat="server" TabIndex="2" prevValue="" AutoPostBack="true" OnTextChanged="btnValidatePlantCode_Click" CssClass="form-control"
                                        MaxLength="2" onblur="TextToUpper();"
                                        onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" ValidationGroup="GenerateBarCode"
                                        Display="None" runat="server" ErrorMessageText="Enter Plant" ControlToValidate="txtDOTPlant"
                                        CssClass="error"></cc1:ResourceRequiredFieldValidator>
                                    <%= ResourceMgr.GetMessage("Plant")%>
                                </div>
                                <div class="col-md-2 col-sm-2 form-group">
                                    <asp:TextBox ID="txtDOTSize" runat="server" prevValue="" TabIndex="3" AutoPostBack="true" OnTextChanged="btnValidateSizeCode_Click"
                                        MaxLength="2" onblur="TextToUpper();" CssClass="form-control"
                                        onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator2" ValidationGroup="GenerateBarCode"
                                        Display="None" runat="server" ErrorMessageText="Enter Size" ControlToValidate="txtDOTSize"
                                        CssClass="error_message"></cc1:ResourceRequiredFieldValidator>
                                    <%= ResourceMgr.GetMessage("Size")%>
                                </div>
                                <div class="col-md-3 col-sm-3 form-group">
                                    <asp:TextBox ID="txtDOTBrand" runat="server" prevValue="" TabIndex="4"
                                        CssClass="form-control" MaxLength="4" onkeypress="return isAlphaNumeric(event);" onblur="TextToUpper();"
                                        AutoPostBack="true" OnTextChanged="btnValidateBrandCode_Click"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator3" ValidationGroup="GenerateBarCode"
                                        Display="None" runat="server" ErrorMessageText="Enter Brand" ControlToValidate="txtDOTBrand"
                                        CssClass="error"></cc1:ResourceRequiredFieldValidator>
                                    <%= ResourceMgr.GetMessage("Brand")%>
                                </div>
                                <div class="col-md-2 col-sm-2 form-group">
                                    <asp:TextBox ID="txtDOTWeek" runat="server" prevValue="" TabIndex="5" AutoPostBack="true" OnTextChanged="btnValidateWeek_Click"
                                        CssClass="form-control" MaxLength="2" onkeypress="return isNumeric(event);"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator4" ValidationGroup="GenerateBarCode"
                                        Display="None" runat="server" ErrorMessageText="Enter Week" ControlToValidate="txtDOTWeek"
                                        CssClass="error"></cc1:ResourceRequiredFieldValidator>
                                    <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator1"
                                        runat="server" ControlToValidate="txtDOTWeek" ErrorMessageText="Enter Week i.e 1-52"
                                        CssClass="error_message" ValidationGroup="GenerateBarCode" Display="None" ValidationExpression="^([0]?[1-9]{1}|[1-4]{1}[0-9]{1}|[5]{1}[0-2]{1})$"></cc1:ResourceRegularExpressionValidator>
                                    <%= ResourceMgr.GetMessage("Week")%>
                                </div>
                                <div class="col-md-2 col-sm-2 form-group">
                                    <asp:TextBox ID="txtDOTYear" runat="server" prevValue="" AutoPostBack="true" OnTextChanged="btnValidateYear_Click"
                                        CssClass="form-control" MaxLength="2" onkeypress="return isNumeric(event);" TabIndex="6"></asp:TextBox>

                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator5" ValidationGroup="GenerateBarCode"
                                        Display="None" runat="server" ErrorMessageText="Enter Year" ControlToValidate="txtDOTYear"
                                        CssClass="error"></cc1:ResourceRequiredFieldValidator>
                                    <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator2"
                                        runat="server" ControlToValidate="txtDOTYear" ValidationGroup="GenerateBarCode"
                                        Display="None" ErrorMessageText="Enter Year e.g 12" CssClass="error"
                                        ValidationExpression="^[0-9]{1}[1-9]{1}$"></cc1:ResourceRegularExpressionValidator>

                                    <%= ResourceMgr.GetMessage("Year")%>
                                </div>

                                <div class="col-md-12">
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
                                </div>

                                <div class="col-md-12 ">
                                    <asp:Label ID="lblerror" runat="server" CssClass="error"></asp:Label>
                                </div>

                                <div class="col-md-6 col-sm-6 form-group">
                                    <label>
                                        <%= ResourceMgr.GetMessage("Brand 2")%>:
                                    </label>
                                    <asp:TextBox ID="txtBrand2" class="form-control" TabIndex="8" runat="server" AutoPostBack="true" OnTextChanged="btnValidateBrandCode_Click" ToolTip="Brand Name"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator6" ValidationGroup="AddInventoryValidationGroup"
                                        Display="Dynamic" runat="server" ErrorMessageText="Please Enter New Brand" ControlToValidate="txtBrand2"
                                        CssClass="custom-error"></cc1:ResourceRequiredFieldValidator>
                                </div>


                                <div class="col-md-6 col-sm-6 form-group">
                                    <label>
                                        <%= ResourceMgr.GetMessage("Size")%>:
                                    </label>

                                    <asp:TextBox ID="txtSize" CssClass="form-control" runat="server" Enabled="false" TabIndex="9" ToolTip="Tire Size"></asp:TextBox>
                                </div>

                                <div class="col-md-6 col-sm-6 form-group">
                                    <label><%= ResourceMgr.GetMessage("Class")%>:</label>

                                    <asp:DropDownList class="form-control" ID="ddlTireClass" runat="server" TabIndex="9">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-6 col-sm-6 form-group">
                                    <label>
                                        <%= ResourceMgr.GetMessage("Action")%>: 
                                    </label>
                                    <asp:DropDownList class="form-control" ID="ddlTireState" runat="server" TabIndex="10">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6 col-sm-6 form-group">
                                    <label>
                                        <%= ResourceMgr.GetMessage("Outcome")%>:
                                    </label>
                                    <asp:DropDownList class="form-control" ID="ddlRecycleState" runat="server" TabIndex="11">
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                        <!-- modal body ends here -->
                        <div class="modal-footer">
                            <a class="reg_button" href="#" style="display: none;">
                                <%= ResourceMgr.GetMessage("Cancel")%></a>
                            <asp:LinkButton ID="lnkCancel" runat="server" ValidationGroup="AddInventoryValidationGroup" CausesValidation="false"
                                CssClass="btn btn-white" OnClick="lnkCancel_Click" OnClientClick="$('.box_blockCmp').hide();"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnUpdateInventory" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                CausesValidation="true" CssClass="btn btn-primary"
                                OnClick="lnkbtnUpdateInventory_Click" OnClientClick="$('.box_blockCmp').hide();"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>
                        </div>
                    </div>
                </div>

                <asp:HiddenField runat="server" ID="hdnLotBarCodeImageFileName" />
                <asp:HiddenField runat="server" ID="hdnBarCodeImageFileName" />
                <asp:HiddenField runat="server" ID="hdnIsPlantCodeValid" Value="0" />
                <asp:HiddenField runat="server" ID="hdnIsSizeCodeValid" Value="0" />
                <asp:HiddenField runat="server" ID="hdnOldBarCodeId" />

                <asp:HiddenField runat="server" ID="hdnIsWeekCodeValid" Value="0" />
                <asp:HiddenField runat="server" ID="hdnIsYearCodeValid" Value="0" />

                <asp:HiddenField runat="server" ID="hdnBrand2ID" />
                <asp:HiddenField runat="server" ID="hdnTireID" />
                <asp:HiddenField runat="server" ID="hdnProductId" />

                <div id="dvProductUpdate" runat="server" class="ajaxModal-popup inmodal" visible="false">
                    <div class="ajaxModal-body animated bounceInRight">
                        <div class="modal-header text-left pb0">
                            <div class="row">
                                <div class="col-md-6 col-sm-6">
                                    <h4 class=" modal-title text-left ">
                                        <%= ResourceMgr.GetMessage("Update Inventory")%></h4>
                                    <span class="label label-primary pull-left fSize-lrg">
                                        <asp:Label ID="lblProductLot" runat="server"></asp:Label>
                                    </span>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="barcode-pane text-right">
                                        <asp:Image ID="imgProductLotBarCode" runat="server" ImageUrl="" Visible="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="row">

                                <div class="col-md-6 col-sm-6 form-group barcode-pane2">
                                    <label><%= ResourceMgr.GetMessage("TTx-Barcode")%>:</label>
                                    <asp:Image ID="imgProducBarCode" runat="server" ImageAlign="AbsMiddle" ImageUrl="" Visible="true" CssClass="img-responsive" />
                                </div>
                                <br clear="all" />
                                <div class="col-md-6 col-sm-6 form-group">
                                    <label>
                                        <%= ResourceMgr.GetMessage("Brand")%>:
                                    </label>
                                    <asp:DropDownList runat="server" ID="ddlBrand" CssClass="form-control"></asp:DropDownList>
                                    <cc1:ResourceRequiredFieldValidator runat="server" ID="ResourceRequiredFieldValidator7" CssClass="custom-error" ControlToValidate="ddlBrand" ErrorMessage="Please select size" InitialValue="0" ValidationGroup="UpdateProduct"></cc1:ResourceRequiredFieldValidator>

                                </div>
                                <br clear="all" />
                                <div class="col-md-6 col-sm-6 form-group">
                                    <label>
                                        <%= ResourceMgr.GetMessage("Size")%>:
                                    </label>
                                    <asp:DropDownList runat="server" ID="ddlSize" CssClass="form-control"></asp:DropDownList>
                                    <cc1:ResourceRequiredFieldValidator runat="server" ID="rfvSize" CssClass="custom-error" ControlToValidate="ddlSize" ErrorMessage="Please select size" InitialValue="0" ValidationGroup="UpdateProduct"></cc1:ResourceRequiredFieldValidator>
                                </div>
                                <br clear="all" />
                                <div class="col-md-6 col-sm-6 form-group">
                                    <label>
                                        <%= ResourceMgr.GetMessage("Shape")%>:
                                    </label>
                                    <asp:DropDownList runat="server" ID="ddlShape" CssClass="form-control"></asp:DropDownList>
                                    <cc1:ResourceRequiredFieldValidator runat="server" ID="ResourceRequiredFieldValidator14" CssClass="custom-error" ControlToValidate="ddlShape" ErrorMessage="Please select Product Shape" InitialValue="0" ValidationGroup="UpdateProduct"></cc1:ResourceRequiredFieldValidator>

                                </div>
                                <br clear="all" />
                                <div class="col-md-6 col-sm-6 form-group">
                                    <label>
                                        <%= ResourceMgr.GetMessage("Material")%>:
                                    </label>
                                    <asp:DropDownList runat="server" ID="ddlMaterial" CssClass="form-control"></asp:DropDownList>
                                    <cc1:ResourceRequiredFieldValidator runat="server" ID="rfvProductMaterial" CssClass="custom-error" ControlToValidate="ddlMaterial" ErrorMessage="Please select Product Shape" InitialValue="0" ValidationGroup="UpdateProduct"></cc1:ResourceRequiredFieldValidator>

                                </div>


                            </div>
                        </div>
                        <!-- modal body ends here -->
                        <div class="modal-footer">
                            <asp:LinkButton ID="CancelProduct" runat="server" ValidationGroup="UpdateProduct" CausesValidation="false"
                                CssClass="btn btn-white btn-sm" OnClick="CancelProduct_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                            <asp:LinkButton ID="UpdateProduct" runat="server" ValidationGroup="UpdateProduct"
                                CausesValidation="true" CssClass="btn btn-primary btn-sm"
                                OnClick="UpdateProduct_Click"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
            <%--    <triggers>
                <asp:AsyncPostBackTrigger ControlID="btnInventorySearch" EventName="Click" />
            </triggers>--%>
        </asp:UpdatePanel>

    </div>
    <%-- <div class="ajax-loader" id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
        <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
    </div>--%>
</asp:Content>

