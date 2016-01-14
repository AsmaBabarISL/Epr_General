<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="CreateLoad.aspx.cs" Inherits="Load_CreateLoad" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/themes/ui-lightness/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right

        function ShowLoginErrorMessage() {
            $("#LoginNameExists").show();
        }


        function HideLoginErrorMessage() {
            $("#LoginNameExists").hide();
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

        function toggleSelection(source) {
            var isChecked = source.checked;
            $("#<%=gvTires.ClientID%> input[id*='rbtSelectTire']").each(function (index) {
                $(this).attr('checked', false);
            });
            source.checked = isChecked;
        }



        function SelectOrganization(ID) {

            var text = $("#" + ID).html();
            $('#<%=hidOrgID.ClientID%>').val(ID);
            $('#<%=hidText.ClientID%>').val(text.trim());
        }
        function isNumeric(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode == 9 || charCode == 8 || charCode == '7F' || charCode == '46' || charCode == '37' || charCode == '39')
                return true;
            else if (!/\d/.test(String.fromCharCode(charCode)))
                return false;
            return true;
        }




        function IsAlphaNumeric1(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));

            return ret;
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
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnl3">
        <ProgressTemplate>
            <div class="ajax-loader" id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdatePanel ID="Upnl" runat="server">
        <ContentTemplate>

            <div id="dvLoadOption" runat="server" class="row">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <%= ResourceMgr.GetMessage("Inventory Wizard")%>
                    </div>
                    <div class="ibox-content">
                        <div class="col-md-12 text-center">
                            <strong>Is this Load a single intake or multiple intake?
                            </strong>
                        </div>
                        <div class="col-md-9"></div>
                        <div class="col-md-3">
                            <asp:LinkButton ID="lnkSingle" CssClass="btn btn-primary" runat="server"
                                OnClick="lnkSingle_Click"><%= ResourceMgr.GetMessage("Single")%></asp:LinkButton>
                            <asp:LinkButton ID="lnkMultiple" CssClass="btn btn-white" runat="server" OnClick="lnkMultiple_Click">
                                <%= ResourceMgr.GetMessage("Multiple")%></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divLoadType" runat="server" class="row">
                <div class="col-md-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Inventory Wizard")%></h5>
                        </div>
                        <div class="mail-box-header">
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label><%= ResourceMgr.GetMessage("Load Type")%></label>
                                    <asp:DropDownList ID="ddlLoadType" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlLoadType_SelectedIndexChanged" CssClass="form-control">
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                        <div class="mail-box">
                            <div class="ibox-content ibox-heading">
                                <h4 class="company-name"><%= ResourceMgr.GetMessage("Company Name")%>: </h4>
                                <h3 class="company-name">
                                    <asp:Label ID="txtCompanyName" runat="server"></asp:Label></h3>
                            </div>
                            <div class="mail-body">
                                <div class="row">
                                    <div id="divLoads" runat="server" visible="false" class="col-md-12">
                                        <%--<h3><%= ResourceMgr.GetMessage("Update Load")%></h3>--%>
                                        <div role="form">
                                            <asp:Panel ID="pnlControls" runat="server" CssClass="row">
                                                <div class="form-group col-md-3">
                                                    <label><%= ResourceMgr.GetMessage("Load Name")%></label>
                                                    <asp:TextBox ID="txtLoadnumber" runat="server" class="form-control" MaxLength="20" onkeypress="return IsAlphaNumeric1(event);" ondrop="return false;" onpaste="return false;">
                                                    </asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Enter Load Name" ControlToValidate="txtLoadnumber"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-md-3">
                                                    <label><%= ResourceMgr.GetMessage("Quantity")%></label>
                                                    <asp:TextBox ID="txtquantity" runat="server" class="form-control" onkeypress="return isNumeric(event);">
                                                    </asp:TextBox><br />
                                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator9" ValidationGroup="createloadgroup" Enabled="false" CssClass="custom-error" runat="server" ErrorText="Enter Quantity" ControlToValidate="txtquantity"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label><%= ResourceMgr.GetMessage("PO Number")%></label>
                                                    <asp:TextBox ID="txtponumber" runat="server" class="form-control"
                                                        onkeypress="return IsAlphaNumeric1(event);"></asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="rfvPonumber" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Enter PO Number" ControlToValidate="txtponumber"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>


                                                    <asp:Label ID="lblerrorPONumber" runat="server" Text="" CssClass="custom-error"></asp:Label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label><%= ResourceMgr.GetMessage("Invoice Number")%></label>
                                                    <asp:TextBox ID="txtinvoicenumber" runat="server" class="form-control"
                                                        onkeypress="return IsAlphaNumeric1(event);"></asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="rfvInvoice" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Enter Invoice Number" ControlToValidate="txtinvoicenumber"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    <asp:Label ID="lblErrorInvoiceNumber" runat="server" Text="" CssClass="custom-error"></asp:Label>
                                                </div>
                                                <div class="form-group col-md-3 clear-both">
                                                    <label><%= ResourceMgr.GetMessage("Seal Number")%></label>
                                                    <asp:TextBox ID="txtsealnumber" runat="server" MaxLength="16" class="form-control" onkeypress="return IsAlphaNumeric1(event);"></asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="rfvSeal" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Enter Seal Number" ControlToValidate="txtsealnumber"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label><%= ResourceMgr.GetMessage("Trailer Number")%></label>
                                                    <asp:TextBox ID="txttrailernumber" runat="server" class="form-control" onkeypress="return IsAlphaNumeric1(event);"></asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="rfvTrail" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Enter Trailer Number" ControlToValidate="txttrailernumber"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-3" id="dvSearchHauler" runat="server">
                                                    <label><%= ResourceMgr.GetMessage("Hauler Name")%></label>
                                                    <div class="input-group">

                                                        <asp:TextBox ID="txthauleridnumber" runat="server" class="form-control" ReadOnly="true" onkeypress="return IsAlphaNumeric(event);"></asp:TextBox>
                                                        <asp:LinkButton ID="lnkSearchParkingLot" CssClass="input-group-addon shPopUp"
                                                            OnClick="lnkParkingLot_Click" runat="server">
                                                    <i class="fa fa-search"></i></asp:LinkButton>
                                                    </div>
                                                    <cc1:ResourceRequiredFieldValidator ID="rfvHaul" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Please Select Hauler Name" ControlToValidate="txthauleridnumber"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    <asp:Label ID="lblHaulerError" CssClass="custom-error" runat="server"></asp:Label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label><%= ResourceMgr.GetMessage("Weight")%></label>
                                                    <asp:TextBox ID="txtweight" runat="server" class="form-control" onkeypress="return IsAlphaNumeric1(event);">
                                                    </asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator14" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Enter Weight" ControlToValidate="txtweight"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-3 clear-both">
                                                    <br />
                                                    <label><%= ResourceMgr.GetMessage("Bill of Lading Number")%></label>
                                                    <asp:TextBox ID="txtladingnumber" runat="server" class="form-control" onkeypress="return IsAlphaNumeric1(event);">
                                                    </asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="rfvLanding" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Enter Bill of Lading Number" ControlToValidate="txtladingnumber"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-3 clear-both">
                                                    <br />
                                                    <label><%= ResourceMgr.GetMessage("TTx-Load Barcode")%>:</label>
                                                    <asp:Image ID="imgLotBarcode" CssClass="img-circle" runat="server" ImageUrl="" Visible="false" />
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlDropDowns" runat="server" Visible="false" CssClass="row">
                                                <div class="form-group col-md-3">
                                                    <label><%= ResourceMgr.GetMessage("Facility")%>:</label>
                                                    <asp:DropDownList ID="ddlFacility" runat="server"
                                                        OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                                    </asp:DropDownList>

                                                    <cc1:ResourceRequiredFieldValidator ID="rvfFacility" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Select Facility" ControlToValidate="ddlFacility"
                                                        Display="Dynamic" InitialValue="0"></cc1:ResourceRequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label><%= ResourceMgr.GetMessage("Storage Lot")%>:</label>
                                                    <asp:DropDownList ID="ddlLot" runat="server" CssClass="form-control"
                                                        OnSelectedIndexChanged="ddlLot_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <cc1:ResourceRequiredFieldValidator ID="rvfLot" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Select Storage Lot" ControlToValidate="ddlLot"
                                                        Display="Dynamic" InitialValue="0"></cc1:ResourceRequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label><%= ResourceMgr.GetMessage("Row")%>:</label>
                                                    <asp:DropDownList ID="ddlSpace" runat="server" CssClass="form-control"
                                                        OnSelectedIndexChanged="ddlSpace_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <cc1:ResourceRequiredFieldValidator ID="rvfSpace" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Select Row" ControlToValidate="ddlSpace"
                                                        Display="Dynamic" InitialValue="0"></cc1:ResourceRequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label><%= ResourceMgr.GetMessage("Space")%>:</label>
                                                    <asp:DropDownList ID="ddlLane" runat="server" CssClass="form-control"></asp:DropDownList>

                                                    <cc1:ResourceRequiredFieldValidator ID="rvfLane" ValidationGroup="createloadgroup"
                                                        CssClass="custom-error" runat="server" ErrorText="Select Space" ControlToValidate="ddlLane"
                                                        Display="Dynamic" InitialValue="0"></cc1:ResourceRequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-md-3">
                                                    <br />
                                                    <label><%= ResourceMgr.GetMessage("TTx-Load Barcode")%>:</label>
                                                    <asp:Image ID="imgLoadBarCodeTransfer" runat="server" CssClass="img-responsive" ImageAlign="AbsMiddle" ImageUrl="" Visible="false" />
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:LinkButton ID="lnkbtnAddCreateLoad" runat="server" ValidationGroup="createloadgroup"
                                                    CausesValidation="true" CssClass="btn btn-primary font-bold" OnClientClick="HideLoginErrorMessage();"
                                                    OnClick="lnkbtnAddCreateLoad_Click"><%= ResourceMgr.GetMessage("Save")%></asp:LinkButton>
                                                <asp:LinkButton ID="lnkContinue" CssClass="btn btn-primary" Visible="false" runat="server" OnClick="lnkContinue_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnCancelCreateLoad" runat="server" CausesValidation="false"
                                                    CssClass="btn btn-white font-bold" OnClick="lnkbtnCancelCreateLoad_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>

                                                <asp:LinkButton ID="lnkAccept" CssClass="btn btn-sm btn-white font-bold" Visible="false" runat="server" OnClick="lnkAccept_Click">
                                                <%= ResourceMgr.GetMessage("Accept")%></asp:LinkButton>
                                                <asp:LinkButton ID="lnkReject" CssClass="btn btn-sm btn-danger font-bold" runat="server" Visible="false" OnClick="lnkReject_Click">
                                                <%= ResourceMgr.GetMessage("Reject")%></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="Upnl2" runat="server">
                <ContentTemplate>
                    <div id="dvLot" runat="server" visible="false" class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5><%= ResourceMgr.GetMessage("LOTS")%></h5>
                                    <asp:Label ID="lblLotError" runat="server" CssClass="error_message"></asp:Label>

                                </div>
                                <div class="ibox-content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvAdminInventory" AutoGenerateColumns="False" GridLines="None" OnRowDataBound="gvAdminInventory_RowDataBound"
                                                    CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data available"
                                                    wrap="nowrap" CellPadding="0" Width="100%" runat="server" OnRowCommand="AdminInventory_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-CssClass="txt-had" >
                                                            <HeaderTemplate>
                                                                <%--<input type="checkbox" id="chkhead" onclick="ToggleChilds(this.checked);" />--%>
                                                                <%--<asp:CheckBox ID="chkhead" runat="server" onclick="ToggleChilds(this.checked);" />--%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%--<input type="checkbox" value='<%#Eval("barCodeId") %>' onclick="CheckParent();" />--%>
                                                                <%--<asp:CheckBox ID="chkSelect" runat="server" onclick="CheckParent();"/>--%>
                                                                <asp:CheckBox ID="chkLotSelect" runat="server" AutoPostBack="true" OnCheckedChanged="LotSelectMethod" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Facility")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("vchFacilityName")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Lot Number")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("LotNumber")%>
                                                                <asp:HiddenField ID="hidLotId" runat="server" Value='<%# Eval("LotId")%>' />
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
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Quantity")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("Quantity")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <uc2:Pager ID="PgrLots" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="upnl3" runat="server">
                <ContentTemplate>

                    <div id="dvTires" runat="server" visible="false" class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5><%= ResourceMgr.GetMessage("TIRES")%></h5>
                                    <asp:Label ID="lblTireError" runat="server" CssClass="custom-error"></asp:Label>
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                </div>
                                <div class="ibox-content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvTires" AutoGenerateColumns="False" GridLines="None" OnRowDataBound="gvTires_RowDataBound"
                                                    CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data available"
                                                    wrap="nowrap" CellPadding="0" runat="server" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%--  <asp:RadioButton ID="rbtSelectTire" runat="server" onclick="toggleSelection(this);"/>--%>
                                                                <asp:Literal ID="litTireId" runat="server" Visible="false" Text='<%# Eval("LotTireIds") %>'></asp:Literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:CheckBox ID="chkSelectTire" runat="server" AutoPostBack="true" OnCheckedChanged="SelectTireMethod" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Serial Number")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("SerialNumber")%>
                                                                <asp:HiddenField ID="hidTireId" runat="server" Value='<%#Eval("ProductId") %>' />
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
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" >
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Plant Code")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%#Eval("PlantNumber")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" >
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Size Code")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("SizeNumber")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" >
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Week")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%#Eval("MonthCode")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" >
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


                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <uc2:Pager ID="pgrTires" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="lnkbtnAddInventory" runat="server" OnClick="lnkbtnAddInventory_Click"
                                                CausesValidation="true" CssClass="btn btn-primary"><%= ResourceMgr.GetMessage("Add")%></asp:LinkButton>
                                            <a class="btn btn-white" style="cursor: pointer;" href="addload">
                                                <%= ResourceMgr.GetMessage("Cancel")%>
                                            </a>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                    <div id="dvProducts" runat="server" visible="false" class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5><%= ResourceMgr.GetMessage("Products")%></h5>
                                    <asp:Label ID="lblProductError" runat="server" CssClass="custom-error"></asp:Label>
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                </div>
                                <div class="ibox-content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvProduct" AutoGenerateColumns="False" GridLines="None" OnRowDataBound="gvProduct_RowDataBound"
                                                    CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data available"
                                                    wrap="nowrap" CellPadding="0" runat="server" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" Visible="false">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%--  <asp:RadioButton ID="rbtSelectTire" runat="server" onclick="toggleSelection(this);"/>--%>
                                                                <asp:Literal ID="litTireId" runat="server" Visible="false" Text='<%# Eval("LotTireIds") %>'></asp:Literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:CheckBox ID="chkSelectProduct" runat="server" AutoPostBack="true" OnCheckedChanged="SelectProduct" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Serial Number")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("SerialNumber").ToString().Split('.')[0]%>
                                                                <asp:HiddenField ID="hidProductId" runat="server" Value='<%#Eval("ProductId") %>' />
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
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Shape")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("shape")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Size")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%#Eval("size")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Material")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%#Eval("Material")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("SubCategory")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%#Eval("ProductSubCat")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <uc2:Pager ID="pgrProduct" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="lnkbtnProductsAdd" runat="server" OnClick="lnkbtnAddInventory_Click"
                                                CausesValidation="true" CssClass="btn btn-primary"><%= ResourceMgr.GetMessage("Add")%></asp:LinkButton>
                                            <a class="btn btn-white" style="cursor: pointer;" href="addload">
                                                <%= ResourceMgr.GetMessage("Cancel")%>
                                            </a>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <div id="dvOrganization" runat="server" class="ajaxModal-popup inmodal" visible="false">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("Hauler IDs")%>
                            <asp:HiddenField ID="hidOrgID" runat="server" />
                            <asp:HiddenField ID="hidText" runat="server" />
                        </h4>

                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblErrorPermanentLotdv" CssClass="custom-error" runat="server"></asp:Label>
                        <div class="table-responsive">
                            <asp:GridView ID="grvOrganizations" AutoGenerateColumns="False" GridLines="None"
                                CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data available"
                                wrap="nowrap" runat="server" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                <Columns>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("OrganizationId")%>' onclick="javascript: SelectOrganization(this.value); RadioCheckgrvOrganizations(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Hauler")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <span id='<%# Eval("OrganizationId")%>'>
                                                <%# Eval("LegalName")%>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <div class="modal-footer">
                        <asp:LinkButton ID="lnkCancelPermanentLot" CssClass="btn btn-white btn-sm" runat="server"
                            OnClick="lnkCancelPermanentLot_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>

                        <asp:LinkButton ID="lnkPermanentLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                            CausesValidation="true" CssClass="btn btn-primary btn-sm"
                            OnClick="lnkPermanentLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>

                    </div>
                </div>
            </div>

            <div id="dvPteNotDefined" runat="server" class="ajaxModal-popup inmodal" visible="false">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("PTE not defined")%>
                        </h4>

                    </div>
                    <div class="modal-body">
                        <p>Either PTE for the following tires has not yet been defined or their PTE has been expired. The current load will proceed with 0 PTE for these tires. Kindly consult your stewardship to avoid this happening again.</p>
                        <div class="table-responsive">
                            <asp:GridView ID="gvSizeCodes" AutoGenerateColumns="False" GridLines="None"
                                CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data available"
                                wrap="nowrap" runat="server">


                                <Columns>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                        <HeaderTemplate>
                                            Serial #
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                        <HeaderTemplate>
                                            Product Size
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("ProductSize")%>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="lnkOkBtnForPTeDiv" CssClass="btn btn-primary " runat="server"
                                OnClick="lnkOkBtnForPTeDiv_Click"><%= ResourceMgr.GetMessage("OK")%></asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>


            <div id="dvPteNotDefinedProduct" runat="server" class="ajaxModal-popup inmodal" visible="false">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("PTE not defined")%>
                        </h4>

                    </div>
                    <div class="modal-body">
                        <p>Either PTE for the following selected products has not yet been defined or their PTE has been expired. The current load will proceed with 0 PTE for these products. Kindly consult your stewardship to avoid this happening again.</p>
                        <div class="table-responsive">
                            <asp:GridView ID="gvSizeCodesProduct" AutoGenerateColumns="False" GridLines="None"
                                CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data available"
                                wrap="nowrap" runat="server">


                                <Columns>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                        <HeaderTemplate>
                                            Serial #
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                        <HeaderTemplate>
                                            Product Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("ProductName")%>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                        <HeaderTemplate>
                                            Product Size
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("ProductSize")%>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                        <HeaderTemplate>
                                            Product Shape
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("ProductShape")%>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                        <HeaderTemplate>
                                            Product Material
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("ProductMaterial")%>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="lnkOkBtnForPTeDivProduct" CssClass="btn btn-primary " runat="server"
                                OnClick="lnkOkBtnForPTeDivProduct_Click"><%= ResourceMgr.GetMessage("OK")%></asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="upnlHiddenFields" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnLotBarCodeImageFileName" />
            <asp:HiddenField runat="server" ID="hdnBarCodeImageFileName" />
            <asp:HiddenField runat="server" ID="hdnIsPlantCodeValid" Value="0" />
            <asp:HiddenField runat="server" ID="hdnIsSizeCodeValid" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
