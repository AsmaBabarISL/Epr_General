<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="AddInvoice.aspx.cs" Inherits="Invoices_AddInvoice" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">


         function SelectedDeliveries(ID, rb) {
             var loadisds = $('#<%=hdDeliveryIDs.ClientID%>').val();
            var removeLodiIds = $('#<%=hdRemoveDeliveryIds.ClientID%>').val();
            var text = $("#" + ID).html();
            var chk = document.getElementById(rb.id);

            if (chk.checked) {
                loadisds = loadisds + ',' + text;
            }
            else {
                removeLodiIds = removeLodiIds + ',' + text;
            }

            $('#<%=hdDeliveryIDs.ClientID%>').val(loadisds);
            $('#<%=hdRemoveDeliveryIds.ClientID%>').val(removeLodiIds);
        }
        function SelectOrganization(ID) {

            var text = $("#" + ID).html();
            var StreetAddress = $("#" + ID + "_street").html();
            var StateAddress = $("#" + ID + "_stateAddress").html();
            $('#<%=hidOrgID.ClientID%>').val(ID);
            $('#<%=hidText.ClientID%>').val(text.trim());
            $('#<%=hidStreetAddress.ClientID%>').val(StreetAddress.trim());
            $('#<%=hidStateAddress.ClientID%>').val(StateAddress.trim());

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
    </script>
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>

    

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Create Invoice</h5>
                </div>
                <div class="ibox-content" style="display: block;">
                    <div class="panel-body">
                        <div class="panel-group" id="accordion">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h5 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" class="">Invoice Details</a>
                                    </h5>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse in" aria-expanded="true">
                                    <div class="panel-body">
                                        <div role="form" class="row search-filter" id="">
                                            <div class="col-md-3 col-sm-6">
                                                <div class="row">
                                                    <div class="form-group col-md-12">
                                                        <label>To</label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtShipToidnumber" runat="server" class="form-control" ReadOnly="true" onkeypress="return IsAlphaNumeric(event);"></asp:TextBox>
                                                            <asp:LinkButton ID="lnkSearchCompany" CssClass="input-group-addon" OnClick="lnkSearchCompany_Click" runat="server">
                                                                <i class="fa fa-search"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvHaul" ValidationGroup="createDeliverygroup"
                                                            CssClass="custom-error" runat="server" ErrorText="Please Select Company Name" ControlToValidate="txtShipToidnumber"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        <p>
                                                            <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                                                             <asp:Label ID="lblOrgName" runat="server" ></asp:Label><br />
            <asp:Label ID="lblOrgstreet" runat="server"></asp:Label><br />
            <asp:Label ID="lblOrgstate" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-md-offset-4 col-sm-6">
                                                <div class="row">
                                                    <div class="form-group col-md-12 ">
                                                        <label>Date</label>
                                                        <div class="input-group date">
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            <asp:TextBox ID="txtInvoiceDate" runat="server" class="form-control start-date"></asp:TextBox>
                                                        </div>
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator3" ValidationGroup="createDeliverygroup"
                                                            CssClass="custom-error" runat="server" ErrorText="Please enter Invoice Date" ControlToValidate="txtInvoiceDate"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Invoice ID:</label>
                                                        <asp:Label ID="lblInvoice" runat="server" Text="xxxxxxxxxx"></asp:Label><br />
                                                        <label>Invoice Date:</label>
                                                        <asp:Label ID="lblInvoiceDate" runat="server" Text="xxxxxxxxxx"></asp:Label><br />
                                                        <label>Invoice Amount:</label>
                                                        $<asp:Label ID="lblInvoiceAmount" runat="server" Text="0"></asp:Label>

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
                    <h5>Invoice Items</h5>
                    <div class="ibox-tools">
                        <div class="form-group">
                            <asp:LinkButton ID="lnkGetAllLoads" CssClass="btn btn-sm btn-primary font-bold" OnClick="lnkGetAllLoads_Click" runat="server">
                                 <i class="fa fa-plus"></i> <%= ResourceMgr.GetMessage("Add Delivery(s)")%>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>

                <div class="ibox-content" style="display: block;">
                    <asp:Label ID="lblErrorMessageLoad" runat="server" CssClass="custom-error" Visible="false"></asp:Label>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvInvoicesinfo" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data available"
                                    runat="server" OnRowDataBound="gvInvoicesinfo_RowDataBound" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                DeliveryID
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("DeliveryID")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                Delivery Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("DeliveryName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                Size
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("TireSize")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                Units
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("NoOfTires")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                Fee
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("FeePerTire")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                Amount
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("PTEamount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                Actions
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ImgbtnDeliveryID" runat="server" ToolTip="View Delivery" CssClass="btn btn-white btn-bitbucket" CommandArgument='<%# Eval("DeliveryID") %>' CommandName="DeliveryInfo">
                              <i class="fa fa-eye"></i></asp:LinkButton>
                                                <asp:LinkButton ID="imgbtnEditLoad" runat="server" ToolTip="Edit Delivery Info" CssClass="btn btn-white btn-bitbucket" CommandName="Edit" CommandArgument='<%# Bind("DeliveryID")%>'>
                             <i class="fa fa-edit"></i></asp:LinkButton>

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
    <!-- row ends here-->
    <div class="row">
        <div class="col-md-12">
            <asp:LinkButton ID="lnkbtnAddInvoice" runat="server" ValidationGroup="createDeliverygroup" Visible="false" CausesValidation="true" CssClass="btn btn-primary" OnClick="lnkbtnAddInvoice_Click">
                <%= ResourceMgr.GetMessage("Save")%></asp:LinkButton>
            <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" CssClass="btn btn-white" OnClick="lnkbtnCancel_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
        </div>
    </div>
   

 <!-- Modal Popups organization list -->
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
                    <%= ResourceMgr.GetMessage("Company List")%></h4>
                </div>
                 <div class="modal-body">
                     <asp:Label ID="lblErrorPermanentLotdv" CssClass="custom-error" runat="server"></asp:Label>
                     <asp:GridView ID="grvOrganizations" AutoGenerateColumns="False" GridLines="None"
                        CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data available"
                         runat="server" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                        <Columns>

                            <asp:TemplateField HeaderText="" ItemStyle-Width="20">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("OrganizationId")%>' onclick="javascript: SelectOrganization(this.value); RadioCheckgrvOrganizations(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Organization Name")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span id='<%# Eval("OrganizationId")%>'>
                                        <%# Eval("LegalName")%>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
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
                        <asp:LinkButton ID="lnkCompany" runat="server" ValidationGroup="AddInventoryValidationGroup"
                        CausesValidation="true" CssClass="btn btn-primary"
                        OnClick="lnkCompany_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                      <asp:LinkButton ID="lnkCancel" CssClass="btn btn-white" runat="server"
                        OnClick="lnkCancel_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>

                 </div>
                </div>
                </div>
            </div>


    <!-- Modal Popups organization list -->
 <div class="ajaxModal-popup inmodal modal-lg" id="dvGetAllDelivery" runat="server" visible="false">
            <div class="ajaxModal-body animated bounceInRight" style="width:900px;">
                <asp:HiddenField ID="hdDeliveryIDs" runat="server" />
            <asp:HiddenField ID="hdRemoveDeliveryIds" runat="server" />
            <div class="popInner_block" style="display: none;"></div>
                 <div id="Div3" runat="server">
                    <div class="modal-header">
                        <h4 class="modal-title"><%= ResourceMgr.GetMessage("Deliveries")%></h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblGetAllDeliveriesError" CssClass="custom-error" runat="server"></asp:Label>
                        <asp:GridView ID="gvGetAllDeliveries" AutoGenerateColumns="False" GridLines="None"
                        CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data available" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                        runat="server">

                        <Columns>

                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="20">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input id="chkLoads" runat="server" type="checkbox" name="chkLoads" value='<%# Eval("DeliveryId")%>' onclick="javascript: SelectedDeliveries(this.value, this); " />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("DeliveryID")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span id='<%# Eval("DeliveryID")%>'>
                                        <%# Eval("DeliveryID")%>
                                    </span>
 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Delivery Name")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span id='<%# Eval("OrganizationId")+"_1"%>'>
                                        <%# Eval("DeliveryName")%>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("ShipTo")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("OrganizationShipTo")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Transporter")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("OrganizationTransporter")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("DeliveryDate")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Convert.ToDateTime(Eval("DeliveryDate")).ToShortDateString()%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("vehicleDetails")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("vehicleDetails")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                        </div>
                    <div class="modal-footer">
                     <asp:LinkButton ID="lnkGetAllDeliveryContinue" runat="server" ValidationGroup="GetAllDeliveryGroup"
                        CausesValidation="true" CssClass="btn btn-primary"
                        OnClick="lnkGetAllDeliveryContinue_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                     <asp:LinkButton ID="LinkButton2" CssClass="btn btn-white" runat="server"
                        OnClick="lnkGetAllDeliveryCancel_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>

                     </div>
                     </div>
                </div>
     </div>

</asp:Content>

