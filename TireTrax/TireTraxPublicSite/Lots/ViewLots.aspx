<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true"
    CodeFile="ViewLots.aspx.cs" Inherits="Inventory_Lots_ViewLots" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function isNumeric(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!/\d/.test(String.fromCharCode(charCode)))
                return false;
            return true;
        }

        function fadeOut() {
            $(".custom-absolute-alert").delay(3000).fadeOut(300);
            $(".custom-absolute-alert").appendTo("form");
        }
        function ClearSearchFields() {

            $("#<%=txtUserName.ClientID%>").val('');
            $("#<%=txtQuantity.ClientID%>").val('');
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
            $("#<%=btnInventorySearch.ClientID%>")[0].click();
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div class="ajax-loader" id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                    <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <%--Remove if not required--%>
        <a class="ico_print" href="javascript:PrintBarcodes();" style="display: none;">
            <%= ResourceMgr.GetMessage("Print Bar Code(s)")%>
        </a>
    </div>
    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnInventorySearch">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5><%= ResourceMgr.GetMessage("Search Filters")%> </h5>

                    </div>
                    <div class="ibox-content" style="display: block;">
                        <asp:Label ID="lblsuccessfull" runat="server" Text=""></asp:Label>
                        <div role="form" class="row search-filter" id="">
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("User Name")%></label>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Units")%></label>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" onkeypress="return isNumeric(event);"></asp:TextBox>
                            </div>

                            <div id="date_range">
                                <div class="input-daterange">
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("From ")%></label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtFrmDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("To")%></label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("C-BarCode")%></label>
                                <asp:TextBox ID="txtGuid" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label><%= ResourceMgr.GetMessage("Facility / Storage Lot")%></label>
                                <asp:TextBox ID="txtLot" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-12 mb0">
                                <cc1:ResourceLinkButton ID="btnInventorySearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                    OnClick="btnSearch_Click"> <%= ResourceMgr.GetMessage("Search")%> </cc1:ResourceLinkButton>
                                <cc1:ResourceLinkButton ID="btnInventoryCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
                                    OnClientClick="ClearSearchFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%> </cc1:ResourceLinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <%-- Extra Fields / Remove if not required --%>
        <div class="search-filter-content-outer" style="display: none;">
            <div class="content-txt">
                <%= ResourceMgr.GetMessage("Space")%>
            </div>
            <div class="content-field">
                <asp:TextBox ID="txtLane" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="search-filter-content-outer" style="display: none;">
            <div class="content-txt">
                <%= ResourceMgr.GetMessage("Row")%>
            </div>
            <div class="content-field">
                <asp:TextBox ID="txtSpace" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="search-filter-content-outer" style="display: none;">
            <div class="content-txt">
                <%= ResourceMgr.GetMessage("Latitude")%>
            </div>
            <div class="content-field">
                <asp:TextBox ID="txtLatitude" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="search-filter-content-outer" style="display: none;">
            <div class="content-txt">
                <%= ResourceMgr.GetMessage("Longitude")%>
            </div>
            <div class="content-field">
                <asp:TextBox ID="txtLongitude" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="search-filter-content-outer" style="display: none;">
            <div class="content-txt">
                <%= ResourceMgr.GetMessage("Size")%>
            </div>
            <div class="content-field">
                <asp:TextBox ID="txtSize" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="search-filter-content-outer" style="display: none;">
            <div class="content-txt">
                <%= ResourceMgr.GetMessage("Brand")%>
            </div>
            <div class="content-field">
                <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="search-filter-content-outer" style="display: none;">
            <div class="content-txt">
                <%= ResourceMgr.GetMessage("Tire State")%>
            </div>
            <div class="content-field">
                <asp:DropDownList ID="ddlTireState" runat="server">
                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Recycle" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Retread" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </asp:Panel>


    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%= ResourceMgr.GetMessage("LOTS")%></h5>
                    <div class="ibox-tools" id="divaddhrfaddInv" runat="server">
                        <div class="form-group">
                            <a href="addInventory" class="btn btn-sm btn-primary"><i class="fa fa-plus"></i><strong><%= ResourceMgr.GetMessage("Add Inventory")%></strong></a>
                        </div>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvAdminInventory" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data found"
                                    runat="server" OnRowCommand="gvAdminInventory_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Creation Date")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Convert.ToDateTime(Eval("DateCreated")).ToString("MM/dd/yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("C-BarCode")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("SerialNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("User")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("UserName") == null ? "" : Eval("UserName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Facility / Storage Lot")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("LotNumber")%>
                                                <asp:HiddenField ID="hidLotStatus" runat="server" Value='<%# Eval("bitCompleted")%>' />
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
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("State")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("TireStateDescription")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Modified Date")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# string.IsNullOrEmpty( Eval("ModifiedDate").ToString())?"N/A": Eval("ModifiedDate")%>
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
                                                <asp:Label CssClass="badge" ID="Label2" runat="server" Text='<%# Convert.ToBoolean(Eval("bitCompleted"))? (Convert.ToBoolean(Eval("bitPermanent"))?"Completed":"Completed-Temp"):"Completed" %>'
                                                    Visible='<%# Convert.ToBoolean(Eval("bitCompleted"))? Convert.ToBoolean(1):Convert.ToBoolean(0) %>'></asp:Label>
                                                <asp:LinkButton CssClass="badge badge-primary" ID="LinkButton1" runat="server" CommandArgument='<%# Bind("LotId") %>'
                                                    CommandName="Edit" Visible='<%# Convert.ToBoolean(Eval("bitCompleted"))? Convert.ToBoolean(0):Convert.ToBoolean(1) %>'><%=ResourceMgr.GetMessage("Open")%></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="90" ItemStyle-Wrap="false">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Actions")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="width: 75px;">
                                                    <asp:LinkButton ID="imgbtnViewTire" ToolTip="View Products" runat="server" CommandName="ViewTires" CommandArgument='<%# Bind("LotId") %>' CssClass="btn btn-white btn-bitbucket"> <i class="fa fa-eye"></i> </asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton1" runat="server" CommandName="LotRedirect" ToolTip="Edit Inventory" CommandArgument='<%# Bind("LotId") %>' CssClass="btn btn-white btn-bitbucket"> <i class="fa fa-edit"></i> </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkTransfer" runat="server" CommandArgument='<%# Bind("OrganizationId") %>' ToolTip="Transfer" CommandName="transfer" Visible='<%# (Convert.ToBoolean(Eval("bitCompleted"))&&!Convert.ToBoolean(Eval("bitPermanent")))%>' CssClass="btn btn-white btn-bitbucket"> <i class="fa fa-truck"></i> </asp:LinkButton>
                                                    <asp:HiddenField ID="hidLotSelectId" runat="server" Value='<%# Eval("LotId") %>' />
                                                </div>
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

    <%--Facility Lot--%>
    <asp:Panel ID="pnlPermanentLot" runat="server">
        <asp:HiddenField ID="hidSelectedOrgId" runat="server" Visible="false" />
        <asp:HiddenField ID="hidSelectedLot" runat="server" Value="" />
        <asp:HiddenField ID="hidSelectedSpace" runat="server" Value="" />
        <asp:HiddenField ID="hidSelectedLane" runat="server" Value="" />
        <div class="" id="dvPermanentLot" runat="server" visible="false">
            <div class="ajaxModal-popup inmodal">
                <div class="ajaxModal-body animated bounceInRight" id="dvParkingLot1" runat="server">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("Facility Storage LOTS")%>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <h4>
                            <b>
                                <asp:Label ID="lblErrorPermanentLotdv" runat="server"></asp:Label>
                            </b>
                        </h4>
                        <br />
                        <asp:GridView ID="grvPermanentLot" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table"
                            EnableViewState="true" EmptyDataText="No Facility Parking Lots found" EmptyDataRowStyle-CssClass="alert alert-danger" CellPadding="0"
                            runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="20">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input id="Radio1" type="radio" runat="server" name="rbt" value='<%# Eval("LotId")%>' onclick="javascript: SelectLot(this.value); RadioCheckgrvPermanentLot(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Facility")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("vchFacilityName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Lots#")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("SerialNumber")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Lot")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("LotNumber")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Rows")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("SpaceCount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Spaces")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("LaneCount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Total")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("TireCount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="row">
                            <div class="col-md-12">
                                <uc2:Pager ID="pgrFacilityLot" runat="server" />
                            </div>
                        </div>
                        <small class="font-bold">
                            <asp:Label ID="lblTotalTire" runat="server" Visible=" false"></asp:Label>
                        </small>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="lnkCancel1" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                        <asp:LinkButton ID="lnkPermanentLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                            CausesValidation="true" CssClass="btn btn-primary btn-sm" OnClick="lnkPermanentLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                    </div>
                </div>
                <div class="ajaxModal-body animated bounceInRight" id="dvSpace" runat="server" visible="false">
                    <div class="modal-header">
                        <h4 class="modal-title"><%= ResourceMgr.GetMessage("Scrap Bin Rows")%> </h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblErrorPermanentLotSpacedv" runat="server"></asp:Label><br />
                        <asp:GridView ID="grdSpaces" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table"
                            EnableViewState="true" EmptyDataText="There is no Row available in this Parking LOT" EmptyDataRowStyle-CssClass="alert alert-danger"
                            wrap="nowrap" CellPadding="0" Width="100%" runat="server">
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
                                        <%=ResourceMgr.GetMessage("Row Name")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("vchSpaceName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="lnkCancel2" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                        <asp:LinkButton ID="lnkBackPermanentLotSpace" CssClass="btn btn-white btn-sm" OnClick="lnkBackPermanentLotSpace_Click"
                            runat="server"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                        <asp:LinkButton ID="lnkSpacePerLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                            CausesValidation="true" CssClass="btn btn-primary btn-sm" OnClick="lnkSpacePerLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                        
                    </div>
                </div>
                <div class="ajaxModal-body animated bounceInRight" id="dvlane" runat="server" visible="false">
                    <div class="modal-header">
                        <h4 class="modal-title"><%= ResourceMgr.GetMessage("Scrap Bin Spaces")%> </h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblErrorPermanentLotLanedv" runat="server"></asp:Label><br />
                        <asp:GridView ID="gvlane" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table"
                            EnableViewState="true" EmptyDataText="There is no Space available in this Parking LOT" EmptyDataRowStyle-CssClass="alert alert-danger"
                            CellPadding="0" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("intLaneId")%>'
                                            onclick="javascript: SelectLane(this.value); RadioCheckgvlane(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Spaces Available")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("nvchLaneName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="lnkCancel3" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                        <asp:LinkButton ID="lnkBackPermanentLotLane" CssClass="btn btn-white btn-sm" OnClick="lnkBackPermanentLotLane_Click"
                            runat="server"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                        <asp:LinkButton ID="lnkLanePerLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                            CausesValidation="true" CssClass="btn btn-primary btn-sm" OnClick="lnkLanePerLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                        
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--View Lot--%>
    <div id="dvAllTire" runat="server" class="ajaxModal-popup inmodal" visible="false">
        <div class="ajaxModal-body animated bounceInRight">
            <div class="modal-header">
                <div id="dvLoadSummaryInfo" runat="server">
                    <h4 class="modal-title">
                        <%= ResourceMgr.GetMessage("Inventory Lot - Details - View")%>
                    </h4>
                    <label class="font-bold">
                        <asp:Label ID="lblLotNumberlabel" runat="server" Text="Lot #:"></asp:Label>
                        <asp:Label ID="lblLotNumber" runat="server"></asp:Label>
                    </label>
                </div>
            </div>
            <div class="modal-body">
                <asp:GridView ID="gvAllTire" AutoGenerateColumns="False" GridLines="None"
                    CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data Available" EmptyDataRowStyle-CssClass="alert alert-danger"
                    wrap="nowrap" CellPadding="0" Width="100%" runat="server"
                    OnRowDataBound="gvAllTire_RowDataBound">
                    <Columns>

                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Size")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("TireSize")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Brand")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("BrandName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("DOT")%>
                            </HeaderTemplate>
                            <ItemTemplate>


                                <asp:HiddenField ID="hdndotnumber" Value='<%#Eval("DOTNumber")%>' runat="server" />
                                <asp:Label ID="AllTireDotNumber" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("COO")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("Abbreviation")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Status")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("Status")%>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Days")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Days") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Location")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("Location")%>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("SKU")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("BarcodeLast")%>
                            </ItemTemplate>
                        </asp:TemplateField>




                    </Columns>
                </asp:GridView>
            </div>
            <div class="modal-footer">
                <asp:LinkButton ID="lnkCancel" runat="server" ValidationGroup="AddInventoryValidationGroup" CausesValidation="false"
                    CssClass="btn btn-primary" OnClick="lnkCancelAllTire_Click" OnClientClick="$('.box_blockCmp').hide();"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
            </div>
        </div>
    </div>


    <%--View Lot product--%>
    <div id="dvAllProducts" runat="server" class="ajaxModal-popup inmodal" visible="false">
        <div class="ajaxModal-body animated bounceInRight">
            <div class="modal-header">
                <div id="Div3" runat="server">
                    <h4 class="modal-title">
                        <%= ResourceMgr.GetMessage("Inventory Lot - Details - View")%>
                    </h4>
                    <label class="font-bold">
                        <asp:Label ID="Label3" runat="server" Text="Lot #:"></asp:Label>
                        <asp:Label ID="lblLotNumberProduct" runat="server"></asp:Label>
                    </label>
                </div>
            </div>
            <div class="modal-body">
                <asp:GridView ID="gvAllProducts" AutoGenerateColumns="False" GridLines="None"
                    CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data Available" EmptyDataRowStyle-CssClass="alert alert-danger"
                    wrap="nowrap" CellPadding="0" Width="100%" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Brand")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("BrandName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Date Created")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("DateCreated")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Size")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Size")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Shape")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Shape")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Material")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Material")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Days")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Days") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="true">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Sub Category")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblProductSubtype" Text=' <%#Eval("ProductSubType")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:HiddenField ID="hdnLotId" runat="server" />
                        <uc2:Pager ID="pgrLotProduct" runat="server"/>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <asp:LinkButton ID="lnkCancelAllProduct" runat="server" ValidationGroup="AddInventoryValidationGroup" CausesValidation="false"
                    CssClass="btn btn-white btn-sm" OnClick="lnkCancelAllProduct_Click" OnClientClick="$('.box_blockCmp').hide();"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
