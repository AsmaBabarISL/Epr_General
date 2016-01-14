<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="EditLoad.aspx.cs" Inherits="Load_EditLoad" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="/Scripts/themes/ui-lightness/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
<script type="text/javascript">
   

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
    $(document).ready(function () {
        $(".ajaxModal-popup").appendTo("form");
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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
                        <h3 class="company-name"><asp:Label ID="txtCompanyName" runat="server"></asp:Label></h3>
                    </div>
                    <div class="mail-body">
                        <div class="row">
                            <div id="divLoads" runat="server" class="col-md-12">
                                <%--<h3><%= ResourceMgr.GetMessage("Update Load")%></h3>--%>
                                <div role="form">
                                    <asp:Panel ID="pnlControls" runat="server" CssClass="row">
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("Load Name")%></label>
                                            <asp:TextBox ID="txtLoadnumber" runat="server" class="form-control" MaxLength="20" onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Enter Load Number" ControlToValidate="txtLoadnumber"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("PO Number")%></label>
                                            <asp:TextBox ID="txtponumber" runat="server" class="form-control"
                                                onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="rfvPonumber" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Enter PO Number" ControlToValidate="txtponumber"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>

                                            <asp:Label ID="lblerrorPONumber" runat="server" Text="" CssClass="custom-error"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("Invoice Number")%></label>
                                            <asp:TextBox ID="txtinvoicenumber" runat="server" class="form-control"
                                                onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="rfvInvoice" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Enter Invoice Number" ControlToValidate="txtinvoicenumber"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                            <asp:Label ID="lblErrorInvoiceNumber" runat="server" Text="" CssClass="custom-error"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("Seal Number")%></label>
                                            <asp:TextBox ID="txtsealnumber" runat="server" MaxLength="16" class="form-control" onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="rfvSeal" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Enter Seal Number" ControlToValidate="txtsealnumber"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3 clear-both">
                                            <label><%= ResourceMgr.GetMessage("Trailer Number")%></label>
                                            <asp:TextBox ID="txttrailernumber" runat="server" class="form-control" onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="rfvTrail" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Enter Trailer Number" ControlToValidate="txttrailernumber"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3" id="dvSearchHauler1" runat="server">
                                            <label><%= ResourceMgr.GetMessage("Hauler Name")%></label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txthauleridnumber" runat="server" class="form-control" ReadOnly="true" onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                                <asp:LinkButton ID="lnkSearchParkingLot" CssClass="input-group-addon shPopUp" OnClientClick="AddPopupClass" OnClick="lnkParkingLot_Click" runat="server">
                                                <i class="fa fa-search"></i>
                                                </asp:LinkButton>
                                            </div>
                                            <cc1:ResourceRequiredFieldValidator ID="rfvHaul" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Please Select Hauler Name" ControlToValidate="txthauleridnumber"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator><asp:Label ID="lblHaulerError" CssClass="custom-error" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("Weight")%></label>
                                            <asp:TextBox ID="txtweight" runat="server" class="form-control" onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="rfvWeight" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Enter Weight" ControlToValidate="txtweight"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("Bill of Lading Number")%></label>
                                            <asp:TextBox ID="txtladingnumber" runat="server" class="form-control" onkeypress="return isAlphaNumeric(event);"></asp:TextBox>
                                            <cc1:ResourceRequiredFieldValidator ID="rfvLanding" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Enter Bill of Lading Number" ControlToValidate="txtladingnumber"
                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3 clear-both">
                                            <label><%= ResourceMgr.GetMessage("TTx-Load Barcode")%>:</label>
                                            <asp:Image CssClass="img-responsive" ID="imgLotBarcode" runat="server" ImageAlign="AbsMiddle" ImageUrl="" Visible="false" />
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlDropDowns" runat="server" Visible="false" CssClass="row">
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("Facility")%>:</label>
                                            <asp:DropDownList ID="ddlFacility" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <cc1:ResourceRequiredFieldValidator ID="rvfFacility" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Select Facility" ControlToValidate="ddlFacility"
                                                Display="Dynamic" InitialValue="0"></cc1:ResourceRequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("Storage Lot")%>:</label>
                                            <asp:DropDownList CssClass="form-control" ID="ddlLot" runat="server" OnSelectedIndexChanged="ddlLot_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <cc1:ResourceRequiredFieldValidator ID="rvfLot" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Select Storage Lot" ControlToValidate="ddlLot"
                                                Display="Dynamic" InitialValue="0"></cc1:ResourceRequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("Row")%>:</label>
                                            <asp:DropDownList CssClass="form-control" ID="ddlSpace" runat="server" OnSelectedIndexChanged="ddlSpace_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>

                                            <cc1:ResourceRequiredFieldValidator ID="rvfSpace" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Select Row" ControlToValidate="ddlSpace"
                                                Display="Dynamic" InitialValue="0"></cc1:ResourceRequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("Space")%>:</label>
                                            <asp:DropDownList CssClass="form-control" ID="ddlLane" runat="server"></asp:DropDownList>
                                            <cc1:ResourceRequiredFieldValidator ID="rvfLane" ValidationGroup="createloadgroup"
                                                CssClass="custom-error" runat="server" ErrorText="Select Space" ControlToValidate="ddlLane"
                                                Display="Dynamic" InitialValue="0"></cc1:ResourceRequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><%= ResourceMgr.GetMessage("TTx-Load Barcode")%>:</label>
                                            <asp:Image CssClass="img-responsive" ID="imgLoadBarCodeTransfer" runat="server" ImageAlign="AbsMiddle" ImageUrl="" Visible="false" />
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:LinkButton ID="lnkbtnAddCreateLoad" runat="server" ValidationGroup="createloadgroup" CausesValidation="true" CssClass="btn btn-sm btn-primary"
                                            OnClientClick="HideLoginErrorMessage();" OnClick="lnkbtnAddCreateLoad_Click"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnCancelCreateLoad" runat="server" CausesValidation="false" CssClass="btn btn-sm btn-white" OnClick="lnkbtnCancelCreateLoad_Click">
                                    <%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="ajaxModal-popup inmodal" id="dvOrganization" runat="server" visible="false">
        <div class="ajaxModal-body animated bounceInRight">
            <asp:HiddenField ID="hidOrgID" runat="server" />
            <asp:HiddenField ID="hidText" runat="server" />
            <div class="popInner_block" style="display: none;"></div>
            <div id="dvParkingLot1" runat="server">
                <div class="modal-header">
                    <h4 class="modal-title"><%= ResourceMgr.GetMessage("Hauler IDs")%></h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblErrorPermanentLotdv" CssClass="custom-error" runat="server"></asp:Label>
                    <div class="table-responsive">
                        <asp:GridView ID="grvOrganizations" AutoGenerateColumns="False" GridLines="None"
                            CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data available" 
                            EmptyDataRowStyle-CssClass="alert alert-danger"
                            wrap="nowrap" CellPadding="0" Width="100%" runat="server"
                            OnRowDataBound="grvOrganizations_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="" HeaderStyle-Width="30">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="Radio1" runat="server" type="radio" name="rbt"  Value='<%# Eval("OrganizationId")%>' onclick="javascript:SelectOrganization(this.value);RadioCheckgrvOrganizations(this);" />
                                        <asp:HiddenField ID="hdnhaulerId" runat="server" Value='<%# Eval("OrganizationId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
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
                <div class="modal-footer">
                    <asp:LinkButton ID="lnkCancelPermanentLot" CssClass="btn btn-sm btn-white" runat="server"
                        OnClick="lnkCancelPermanentLot_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>

                    <asp:LinkButton ID="lnkPermanentLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                        CausesValidation="true" CssClass="btn btn-sm btn-primary"
                        OnClick="lnkPermanentLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>

                    <asp:LinkButton ID="AddNewHauler" runat="server" Visible="false"
                        CausesValidation="true" CssClass="btn btn-sm btn-primary" OnClick="lnkAddNewHauler_Click"><%= ResourceMgr.GetMessage("Add New Hauler")%></asp:LinkButton>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="upnlHiddenFields" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdnLotBarCodeImageFileName" />
                <asp:HiddenField runat="server" ID="hdnBarCodeImageFileName" />
                <asp:HiddenField runat="server" ID="hdnIsPlantCodeValid" Value="0" />
                <asp:HiddenField runat="server" ID="hdnIsSizeCodeValid" Value="0" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

