<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="AddDeliveryNote.aspx.cs" Inherits="DeliveryNotes_AddDeliveryNote" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SelectOrganization(ID) {

            var text = $("#" + ID).html();
            var StreetAddress = $("#" + ID + "_street").html();
            var StateAddress = $("#" + ID + "_stateAddress").html();
            $('#<%=hidOrgID.ClientID%>').val(ID);
            $('#<%=hidText.ClientID%>').val(text.trim());
            $('#<%=hidStreetAddress.ClientID%>').val(StreetAddress.trim());
            $('#<%=hidStateAddress.ClientID%>').val(StateAddress.trim());

        }
        function IsAlphaNumeric1(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));

            return ret;
        }
        function isNumeric(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode == 9 || charCode == 8 || charCode == '7F' || charCode == '46' || charCode == '37' || charCode == '39')
                return true;
            else if (!/\d/.test(String.fromCharCode(charCode)))
                return false;
            return true;
        }
        function isAlphaNumeric(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode == 9 || charCode == 8 || charCode == '7F' || charCode == '46' || charCode == '37' || charCode == '39') {
                return true;
            }
            else if (!/\w/.test(String.fromCharCode(charCode))) {

                return false;
            }
            return true;
        }
        function RadioCheckgrvOrganizations(rb) {
            var gv = document.getElementById("<%=grvOrganizations.ClientID%>");
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
        function SelectOrganizationTranp(ID) {

            var text = $("#" + ID).html();
            var StreetAddress = $("#" + ID + "_street").html();
            var StateAddress = $("#" + ID + "_stateAddress").html();
            $('#<%=hdTranspoterID.ClientID%>').val(ID);
            $('#<%=hdTransporterName.ClientID%>').val(text.trim());
            $('#<%=hdTransporterStreetAddress.ClientID%>').val(StreetAddress.trim());
            $('#<%=hdTransporterStateAddress.ClientID%>').val(StateAddress.trim());
            $('#divTransptor').show();

        }
        function RadioCheckgrvOrganizationsTranp(rb) {
            var gv = document.getElementById("<%=grvTanspoterOrganizations.ClientID%>");
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

        function ShowSearchCompanies() {

            $('#<%=dvOrganization.ClientID%>').css({ visibility: "visible" });

        }
        function SelectedLoads(ID, rb) {
            debugger;
            var loadisds = $('#<%=hdLoadIDs.ClientID%>').val();
            var removeLodiIds = $('#<%=hdRemoveLoadIds.ClientID%>').val();
            var text = ID;
            var chk = document.getElementById(rb.id);

            if (chk.checked) {
                loadisds = loadisds + ',' + text;
            }
            else {
                removeLodiIds = removeLodiIds + ',' + text;
            }

            $('#<%=hdLoadIDs.ClientID%>').val(loadisds);
            $('#<%=hdRemoveLoadIds.ClientID%>').val(removeLodiIds);
        }


        $(document).ready(function () {
            $("[id*=rdTransportor] input").on("click", function () {
                var selectedValue = $(this).val();
                var selectedText = $(this).next().html();
                if (selectedValue == 3)
                    $('#divTransptor').slideDown();
                else if (selectedValue == 1 || selectedValue == 2) {
                    $('#divTransptor').slideUp();
                    $('#<%=txtTransporterID.ClientID%>').val('');
                    $('#<%=ltrTranspoterAddress.ClientID%>').html('');
                }
            });
        });

    function showTranDiv() {
        $('#divTransptor').show();
    }

    function AddPopupClass() {
        $(".ajaxModal-popup").appendTo("body");
        $(".ajax-loader").remove();
    }

    function AjaxLoader() {
        $(".ajax-loader").appendTo("form");
    }
    $(document).ready(function () {
        $(".ajaxModal-popup").appendTo("form");
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Create Delivery Notes</h5>
                </div>
                <div class="ibox-content" style="display: block;">
                    <div class="panel-body">
                        <div class="panel-group" id="accordion">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h5 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" class="">Reciever Information</a>
                                    </h5>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse in" aria-expanded="true" style="">
                                    <div class="panel-body">
                                        <div role="form" class="row search-filter" id="">
                                            <div class="col-md-3">
                                                <div class="row">
                                                    <div class="form-group col-md-12">
                                                        <label>Ship to</label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtShipToidnumber" runat="server" CssClass="form-control" ReadOnly="true" onkeypress="return IsAlphaNumeric(event);">
                                                            </asp:TextBox>
                                                            <asp:LinkButton ID="lnkSearchCompany" CssClass="input-group-addon" OnClick="lnkSearchCompany_Click" runat="server">
                                                                <i class="fa fa-search"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvHaul" ValidationGroup="createDeliverygroup"
                                                            CssClass="custom-error" runat="server" ErrorText="Please Select Company Name" ControlToValidate="txtShipToidnumber"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        <p>
                                                            <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-md-offset-4">
                                                <div class="row">
                                                    <div class="form-group col-md-12">
                                                        <label>Delivery Name</label>
                                                        <asp:TextBox ID="txtDeliveryName" runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator2" ValidationGroup="createDeliverygroup"
                                                            CssClass="custom-error" runat="server" ErrorText="Please enter Delivery Name" ControlToValidate="txtDeliveryName"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-12 ">
                                                        <label>Delivery Date</label>
                                                        <div class="input-group date">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            <asp:TextBox ID="txtDeliveryDate" runat="server" class="form-control start-date"></asp:TextBox>
                                                        </div>
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator3" ValidationGroup="createDeliverygroup"
                                                            CssClass="custom-error" runat="server" ErrorText="Please enter Delivery Date" ControlToValidate="txtDeliveryDate"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" class="" aria-expanded="true">Transportation Information</a>
                                    </h4>
                                </div>
                                <div id="collapseTwo" class="panel-collapse collapse in" aria-expanded="true" style="">
                                    <div class="panel-body">
                                        <div role="form" class="row search-filter" id="">
                                            <div class="col-md-3">
                                                <div class="row">
                                                    <div class="form-group col-md-12">
                                                        <label>Vehicle Details</label>
                                                        <asp:TextBox ID="txtVehicleDetails" runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator4" ValidationGroup="createDeliverygroup"
                                                            CssClass="custom-error" runat="server" ErrorText="Please enter Vehicle Details" ControlToValidate="txtVehicleDetails"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Weight</label>
                                                        <asp:TextBox ID="txtWeight" runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator5" ValidationGroup="createDeliverygroup"
                                                            CssClass="custom-error" runat="server" ErrorText="Please enter Weight" ControlToValidate="txtWeight"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Delivery Estimate</label>
                                                        <div class="input-group date">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            <asp:TextBox ID="txtDeliveryEstimaDateTime" runat="server" class="form-control start-date"></asp:TextBox>
                                                        </div>
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator6" ValidationGroup="createDeliverygroup"
                                                            CssClass="custom-error" runat="server" ErrorText="Please enter Delivery Date" ControlToValidate="txtDeliveryEstimaDateTime"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-md-offset-4">
                                                <div class="row">
                                                    <div class="form-group col-md-12">
                                                        <label>Transportation</label>
                                                        <label class="checkbox">
                                                            <asp:RadioButtonList ID="rdTransportor" runat="server">
                                                                <asp:ListItem Value="1" Text="Sender"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Receiver"></asp:ListItem>
                                                                <asp:ListItem Value="3" Text="Transporter"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </label>
                                                    </div>
                                                    <div class="form-group col-md-12" id="divTransptor" style="display: none;">
                                                        <label>Transporter</label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtTransporterID" runat="server" class="form-control" ReadOnly="true" onkeypress="return IsAlphaNumeric(event);">
                                                            </asp:TextBox>
                                                            <asp:LinkButton ID="lnkGetTransporterID" CssClass="input-group-addon" OnClick="lnkGetTransporters_Click" runat="server">
                                                            <i class="fa fa-search"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" ValidationGroup="trnp"
                                                            CssClass="custom-error" runat="server" ErrorText="Please Select Company Name" ControlToValidate="txtTransporterID"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        <p>
                                                            <asp:Literal ID="ltrTranspoterAddress" runat="server"></asp:Literal>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
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
                    <h5>Add Batches to Delivery Note</h5>
                    <div class="ibox-tools">
                        <div class="form-group">
                            <asp:LinkButton ID="lnkGetAllLoads" CssClass="btn btn-sm btn-primary font-bold" OnClick="lnkGetAllLoads_Click" runat="server">
                                <i class="fa fa-plus"></i> <%= ResourceMgr.GetMessage("Add Batch")%>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="ibox-content" style="display: block;">
                    <asp:Label ID="lblErrorMessageLoad" runat="server" CssClass="custom-error" Visible="false"></asp:Label>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvAllLoads" CssClass="table table-bordered epr-sec-table" AutoGenerateColumns="False" DataKeyNames="LoadID" EnableViewState="true"
                                    EmptyDataText="No data Available" runat="server" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Batch Name")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("LoadNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Total Units")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("quantity")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Weight")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("weight")%>
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

    <div class="row" runat="server" id="dvTire" visible="false">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Add Products to Delivery Note</h5>
                </div>
                <div class="ibox-content" style="display: block;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvAllTire" AutoGenerateColumns="False" GridLines="None" DataKeyNames="ProductId"
                                            CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data Available" runat="server" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("ID")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ProductId")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Brand")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Brand")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                        <%=ResourceMgr.GetMessage("Week")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("MonthCode")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Year")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("YearCode")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("DOT")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="AllTireDotNumber" runat="server" Text='<%#Eval("DOTNumber")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("SKU")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("TireSerialNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <uc2:Pager ID="pgrTiresLoad" runat="server" />
                                </div>
                            </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="row" runat="server" visible="false" id="dvProduct">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Add Products to Delivery Note</h5>
                </div>
                <div class="ibox-content" style="display: block;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvAllProduct" AutoGenerateColumns="False" GridLines="None" DataKeyNames="ProductId"
                                            CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data Available" runat="server" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Product")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Product")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Serial Number")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("SerialNumber").ToString().Split('.')[0]%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                        <%=ResourceMgr.GetMessage("Shape")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ProductShape")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Material")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("ProductMaterial")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Sub Category")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("SubCat")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <uc2:Pager ID="pgrProduct" runat="server" />
                                </div>
                            </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:LinkButton ID="lnkbtnAddDelivery" runat="server" ValidationGroup="createDeliverygroup" CausesValidation="true" CssClass="btn btn-primary" OnClick="lnkbtnAddDelivery_Click">
                <%= ResourceMgr.GetMessage("Save")%></asp:LinkButton>
            <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn btn-white" OnClick="lnkbtnCancel_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
        </div>
    </div>



    <!-- Modal Popups -->



    <div class="ajaxModal-popup inmodal" id="dvOrganization" runat="server" visible="false">
        <div class="ajaxModal-body animated bounceInRight">
            <asp:HiddenField ID="hidOrgID" runat="server" />
            <asp:HiddenField ID="hidText" runat="server" />
            <asp:HiddenField ID="hidStreetAddress" runat="server" />
            <asp:HiddenField ID="hidStateAddress" runat="server" />
            <div class="popInner_block" style="display: none;"></div>
            <div id="dvParkingLot1" runat="server">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <%= ResourceMgr.GetMessage("Company List")%>
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblErrorPermanentLotdv" CssClass="custom-error" runat="server"></asp:Label><br />
                    <asp:GridView ID="grvOrganizations" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data available" EmptyDataRowStyle-CssClass="alert alert-danger text-center" runat="server">
                        <Columns>

                            <asp:TemplateField HeaderText="" ItemStyle-Width="20">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("OrganizationId")%>' onclick="javascript: SelectOrganization(this.value); RadioCheckgrvOrganizations(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Organization Name")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span id='<%# Eval("OrganizationId")%>'>
                                        <%# Eval("LegalName")%>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Address")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span id='<%# Eval("OrganizationId")+"_1"%>'>
                                        <%# Eval("FullAddress")%>
                                    </span>
                                    <span id='<%# Eval("OrganizationId")+"_street"%>' style="display: none">
                                        <%# Eval("streetaddress")%>
                                    </span>
                                    <span id='<%# Eval("OrganizationId")+"_stateAddress"%>' style="display: none">
                                        <%# Eval("citystateAddress")%>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="lnkCancel" CssClass="btn btn-white btn-sm" runat="server" OnClick="lnkCancel_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                    <asp:LinkButton ID="lnkCompany" runat="server" ValidationGroup="AddInventoryValidationGroup"
                        CausesValidation="true" CssClass="btn btn-primary btn-sm" OnClick="lnkCompany_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="ajaxModal-popup inmodal" id="dvTranspoterOrganization" runat="server" visible="false">
        <div class="ajaxModal-body animated bounceInRight">
            <asp:HiddenField ID="hdTranspoterID" runat="server" />
            <asp:HiddenField ID="hdTransporterName" runat="server" />
            <asp:HiddenField ID="hdTransporterStreetAddress" runat="server" />
            <asp:HiddenField ID="hdTransporterStateAddress" runat="server" />
            <div class="popInner_block" style="display: none;"></div>
            <div id="Div2" runat="server">
                <div class="modal-header">
                    <h4 class="modal-title"><%= ResourceMgr.GetMessage("Company List")%></h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblErrorTransporter" CssClass="custom-error" runat="server"></asp:Label><br />
                    <asp:GridView ID="grvTanspoterOrganizations" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data available"
                        EmptyDataRowStyle-CssClass="alert alert-danger text-center" runat="server">
                        <Columns>

                            <asp:TemplateField HeaderText="" ItemStyle-Width="20">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("OrganizationId")%>' onclick="javascript: SelectOrganizationTranp(this.value); RadioCheckgrvOrganizationsTranp(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Organization Name")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span id='<%# Eval("OrganizationId")%>'>
                                        <%# Eval("LegalName")%>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Address")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span id='<%# Eval("OrganizationId")+"_1"%>'>
                                        <%# Eval("FullAddress")%>
                                    </span>
                                    <span id='<%# Eval("OrganizationId")+"_street"%>' style="display: none">
                                        <%# Eval("streetaddress")%>
                                    </span>
                                    <span id='<%# Eval("OrganizationId")+"_stateAddress"%>' style="display: none">
                                        <%# Eval("citystateAddress")%>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="lnkTranspoterCancel" CssClass="btn btn-white btn-sm" runat="server"
                        OnClick="lnkTranspoterCancel_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                    <asp:LinkButton ID="lnkTranporterContinue" runat="server" ValidationGroup="AddTranpValidationGroup"
                        CausesValidation="true" CssClass="btn btn-primary btn-sm"
                        OnClick="lnkTranporterContinue_Click" OnClientClick="javascript:showTranDiv();"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>


    <div class="ajaxModal-popup inmodal" id="dvGetAllLoad" runat="server" visible="false">
        <div class="ajaxModal-body animated bounceInRight">
            <asp:HiddenField ID="hdLoadIDs" runat="server" />
            <asp:HiddenField ID="hdRemoveLoadIds" runat="server" />
            <div class="popInner_block" style="display: none;"></div>
            <div id="Div3" runat="server">
                <div class="modal-header">
                    <h4 class="modal-title"><%= ResourceMgr.GetMessage("Loads")%></h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblGetAllLoadError" CssClass="custom-error" runat="server"></asp:Label><br />
                    <asp:GridView ID="gvGetAllLoads" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data available"
                        EmptyDataRowStyle-CssClass="alert alert-danger text-center" runat="server">

                        <Columns>

                            <asp:TemplateField HeaderText="" ItemStyle-Width="20">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input id="chkLoads" runat="server" type="checkbox" name="chkLoads" value='<%# Eval("LoadId")%>' onclick="javascript: SelectedLoads(this.value, this); " />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" Visible="false">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("LoadId")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span id='<%# Eval("LoadId")%>'>
                                        <%# Eval("LoadId")%>
                                    </span>


                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Load Name")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span id='<%# Eval("OrganizationId")+"_1"%>'>
                                        <%# Eval("LoadNumber")%>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Units")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("Quantity")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Weight")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("Weight")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-white btn-sm" runat="server" OnClick="lnkGetAllLoadCancel_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                    <asp:LinkButton ID="lnkGetAllLoadContinue" runat="server" ValidationGroup="GetAllLoadGroup"
                        CausesValidation="true" CssClass="btn btn-primary btn-sm" OnClick="lnkGetAllLoadContinue_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

