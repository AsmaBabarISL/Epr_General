<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewLots.aspx.cs" Inherits="Lots_ViewLots" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function isNumeric(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!/\d/.test(String.fromCharCode(charCode)))
                return false;
            return true;
        }

        function ClearSearchFields() {

            $("#<%=txtUserName.ClientID%>").val('');
            $("#<%=txtQuantity.ClientID%>").val('');
            $("#<%=txtDOT.ClientID%>").val('');
            $("#<%=txtLane.ClientID%>").val('');
            $("#<%=txtSpace.ClientID%>").val('');

            $("#<%=txtFrmDate.ClientID%>").val('');
            $("#<%=txtToDate.ClientID%>").val('');
            $("#<%=txtLatitude.ClientID%>").val('');
            $("#<%=txtLongitude.ClientID%>").val('');
            $("#<%=txtGuid.ClientID%>").val('');
            $("#<%=txtLot.ClientID%>").val('');
            $("#<%=txtSize.ClientID%>").val('');
            $("#<%=txtBrand.ClientID%>").val('');

            $("#<%=ddlTireState.ClientID%>")[0].click();
        }

        function SelectLot(obj) {
            $('#<%=hidSelectedLot.ClientID%>').val(obj);

        }

        function SelectSpace(obj) {
            $('#<%=hidSelectedSpace.ClientID%>').val(obj);

        }
        function SelectLane(obj) {
            $('#<%=hidSelectedLane.ClientID%>').val(obj);

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
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnInventorySearch" CssClass="search-filter_inner">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Search Filters")%></h5>
                            <div class="ibox-tools">
                                <div class="form-group">
                                    <a class="btn btn-sm btn-primary font-bold" href="javascript:PrintBarcodes();" style="display: none;">
                                        <%= ResourceMgr.GetMessage("Print Bar Code(s)")%></a>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("User Name")%></label>
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Quantity")%></label>
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("DOT#")%></label>
                                    <asp:TextBox ID="txtDOT" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Lane")%></label>
                                    <asp:TextBox ID="txtLane" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Space")%></label>
                                    <asp:TextBox ID="txtSpace" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("TTx-IDD")%></label>
                                    <asp:TextBox ID="txtGuid" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>


                                <div id="date_range">
                                    <div class="input-daterange">
                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("From Date")%></label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label><%= ResourceMgr.GetMessage("To Date")%></label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Latitude")%></label>
                                    <asp:TextBox ID="txtLatitude" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Longitude")%></label>
                                    <asp:TextBox ID="txtLongitude" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Lot")%></label>
                                    <asp:TextBox ID="txtLot" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Size")%></label>
                                    <asp:TextBox ID="txtSize" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Brand")%></label>
                                    <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-6 col-lg-3" style="display: none;">
                                    <label><%= ResourceMgr.GetMessage("Tire State")%></label>
                                    <asp:DropDownList ID="ddlTireState" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Recycle" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Retread" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-sm-12 mb0">
                                    <cc1:ResourceLinkButton ID="btnInventorySearch" runat="server" CssClass="btn btn-primary btn-sm font-bold"
                                        OnClick="btnSearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                    <cc1:ResourceLinkButton ID="btnInventoryCancel" runat="server" CssClass="btn btn-white btn-sm font-bold"
                                        OnClientClick="ClearSearchFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                        
                    </asp:Panel> 
            </div>
        </div>

      
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                      <div class="row">
            <div class="col-md-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5><%= ResourceMgr.GetMessage("LOTS")%></h5>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvAdminInventory" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data found"
                        runat="server" OnRowCommand="gvAdminInventory_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Date")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("ModifiedDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("TTx-Lot ID")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("SerialNumber")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Facility Lot Name")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("LotNumber")%>
                                    <asp:HiddenField ID="hidLotStatus" runat="server" Value='<%# Eval("bitCompleted")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Quantity")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("Quantity")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("User Name")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("UserName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("State")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("TireStateDescription")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Convert.ToBoolean(Eval("bitCompleted")) == true ? "Closed" : "Open"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Status")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" CssClass="badge" runat="server" Text='<%# Convert.ToBoolean(Eval("bitCompleted"))? (Convert.ToBoolean(Eval("bitPermanent"))?"Completed":"Completed-Temp"):"Completed" %>'
                                        Visible='<%# Convert.ToBoolean(Eval("bitCompleted"))? Convert.ToBoolean(1):Convert.ToBoolean(0) %>'></asp:Label>
                                    <asp:LinkButton CssClass="badge badge-primary" ID="LinkButton1" runat="server" CommandArgument='<%# Bind("LotId") %>'
                                        CommandName="Edit" Visible='<%# Convert.ToBoolean(Eval("bitCompleted"))? Convert.ToBoolean(0):Convert.ToBoolean(1) %>'><%=ResourceMgr.GetMessage("Open")%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Action")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="lnkTransfer" runat="server" CommandArgument='<%# Bind("OrganizationId") %>'
                                        CommandName="transfer" Visible='<%# Convert.ToBoolean(Eval("bitCompleted"))? (Convert.ToBoolean(Eval("bitPermanent"))?Convert.ToBoolean(0):Convert.ToBoolean(1)):Convert.ToBoolean(0) %>'>
                                        <i class="fa fa-truck"></i>
                                    </asp:LinkButton>
                                    <asp:HiddenField ID="hidLotSelectId" runat="server" Value='<%# Eval("LotId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Edit")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="ImageButton1" CssClass="btn btn-white btn-bitbucket" runat="server" CommandName="LotRedirect" CommandArgument='<%# Bind("LotId") %>'>
                                        <i class="fa fa-edit"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <uc2:Pager ID="pgrLots" runat="server" />
                            </div>
                            </div>

                    </div>
                </div>
            </div>
        </div>

                    <%--New UI not done on this popup--%>
                    <asp:Panel ID="pnlPermanentLot" runat="server">
                        <asp:HiddenField ID="hidSelectedOrgId" runat="server" Visible="false" />
                        <asp:HiddenField ID="hidSelectedLot" runat="server" />
                        <asp:HiddenField ID="hidSelectedSpace" runat="server" />
                        <asp:HiddenField ID="hidSelectedLane" runat="server" />
                        <div class="" id="dvPermanentLot" runat="server" visible="false">
                            <div class="ajaxModal-popup inmodal">
                                <div class="ajaxModal-body animated bounceInRight" id="dvParkingLot1" runat="server">
                                    <div class="modal-header">
                                       <h4 class="modal-title">
                                           <%= ResourceMgr.GetMessage("Parking LOTS")%>
                                       </h4>
                                   </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblErrorPermanentLotdv" CssClass="custom-error" runat="server"></asp:Label>
                                        <asp:GridView ID="grvPermanentLot" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table"
                                            EnableViewState="true" EmptyDataText="No data found" runat="server" EmptyDataRowStyle-CssClass="alert alert text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <input id="Radio1" type="radio" name="rbt" value='<%# Eval("LotId")%>' onclick="javascript: SelectLot(this.value); RadioCheckgrvPermanentLot(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Lots#")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("SerialNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Parking Lots Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("LotNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Total Spaces")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("SpaceCount")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Total Lanes")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("LaneCount")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
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
                                    <div class="modal-footer">
                                        <asp:LinkButton ID="lnkCancel1" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                        <asp:LinkButton ID="lnkPermanentLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                            CausesValidation="true" CssClass="btn btn-primary" OnClick="lnkPermanentLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                                    </div>
                                </div>

                                <div class="ajaxModal-body animated bounceInRight" id="dvSpace" runat="server" visible="false">
                                    <div class="modal-header">
                                        <h4 class="modal-title">
                                            <%= ResourceMgr.GetMessage("Parking LOTS Spaces")%>
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblErrorPermanentLotSpacedv" CssClass="custom-error" runat="server"></asp:Label>
                                        <asp:GridView ID="grdSpaces" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table"
                                            EnableViewState="true" EmptyDataText="There is no Space available in this Parking LOT" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                            runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("intSpaceId")%>'
                                                            onclick="javascript: SelectSpace(this.value); RadioCheckgrdSpaces(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Spaces Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("vchSpaceName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:LinkButton ID="lnkBackPermanentLotSpace" CssClass="btn btn-white" OnClick="lnkBackPermanentLotSpace_Click"
                                            runat="server"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                                        <asp:LinkButton ID="lnkCancel2" CssClass="btn btn-white" OnClick="lnkCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                        <asp:LinkButton ID="lnkSpacePerLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                            CausesValidation="true" CssClass="btn btn-primary" OnClick="lnkSpacePerLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                                    </div>
                                </div>

                                <div class="ajaxModal-body animated bounceInRight" id="dvlane" runat="server" visible="false">
                                    <div class="modal-header">
                                        <h4 class="modal-title">
                                            <%= ResourceMgr.GetMessage("Parking LOTS Lanes")%>
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblErrorPermanentLotLanedv" CssClass="custom-error" runat="server"></asp:Label>
                                        <asp:GridView ID="gvlane" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table"
                                            EnableViewState="true" EmptyDataText="There is no Lane available in this Parking LOT" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("intLaneId")%>'
                                                            onclick="javascript: SelectLane(this.value); RadioCheckgvlane(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Lanes Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("nvchLaneName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:LinkButton ID="lnkBackPermanentLotLane" CssClass="btn btn-white" OnClick="lnkBackPermanentLotLane_Click"
                                            runat="server"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                                        <asp:LinkButton ID="lnkCancel3" CssClass="btn btn-white" OnClick="lnkCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                        <asp:LinkButton ID="lnkLanePerLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                            CausesValidation="true" CssClass="btn btn-primary" OnClick="lnkLanePerLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnInventorySearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
</asp:Content>

