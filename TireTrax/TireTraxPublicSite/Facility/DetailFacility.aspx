<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="DetailFacility.aspx.cs" Inherits="Facility_DetailFacility" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">



        function ClearSearchFields() {

            $("#<%=txtParkingLotName.ClientID%>").val('');
            $("#<%=btnInventorySearch.ClientID%>")[0].click();

        }
        function ShowSpaceInfo() {


            var marginleft = (($("#dvSpaceInfo").width()) / 2) * -1;
            $("#dvSpaceInfo").css({
            });

            $("#dvSpaceInfo").show();

        }
        function SelectLot(obj) {
            $('#<%=hidSelectedLot.ClientID%>').val(obj);
            $('#<%=lbldeleteerror.ClientID%>').text("");
        }

        function SelectSpace(obj) {
            $('#<%=hidSelectedSpace.ClientID%>').val(obj);
            $('#<%=lbldeleteerror.ClientID%>').text("");
        }
        function SelectLane(obj) {
            $('#<%=hidSelectedLane.ClientID%>').val(obj);
            $('#<%=lbldeleteerror.ClientID%>').text("");
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
            $(".ajaxModal-popup").appendTo("body");
            $(".ajax-loader").remove();
        }

        function AjaxLoader() {
            $(".ajax-loader").appendTo("body");
        }

        $(document).ready(function () {
            $(".ajaxModal-popup").appendTo("form");
        });


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
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div class="ajax-loader" id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                    <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>


        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnInventorySearch" CssClass="search-filter_inner">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Search Filters")%></h5>
                            <a class="ico_print" href="javascript:PrintBarcodes();" style="display: none;">
                                <%= ResourceMgr.GetMessage("Print Bar Code(s)")%>
                            </a>
                        </div>


                        <div class="ibox-content" style="display: block;">


                            <div role="form" class="row search-filter" id="">
                                <div class="form-group col-md-3">

                                    <label><%= ResourceMgr.GetMessage("Facility Storage Lot Name")%></label>
                                    <asp:TextBox ID="txtParkingLotName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-12 mb0">
                                    <cc1:ResourceLinkButton ID="btnInventorySearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                        OnClick="btnSearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                    <cc1:ResourceLinkButton ID="btnInventoryCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
                                        OnClientClick="ClearSearchFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Label ID="lblErrorDeletedSuccesfullytemp" runat="server" ></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Facility Storage Lots")%></h5>
                                <div class="ibox-tools" id="divaddhrfaddInv" runat="server">
                                </div>
                            </div>
                            
                            <div class="ibox-content">
                                <%--<asp:Label ID="lblErrorDeletedSuccesfullytemp" runat="server" Visible="false" CssClass="alert-success notific-label"></asp:Label>--%>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvAdminInventory" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table" EnableViewState="true"
                                                EmptyDataText="No data found" runat="server"
                                                OnRowCommand="gvAdminInventory_RowCommand"
                                                OnRowDeleted="gvAdminInventory_RowDeleted"
                                                OnRowDeleting="gvAdminInventory_RowDeleting"
                                                OnRowEditing="gvAdminInventory_RowEditing"
                                                OnRowUpdated="gvAdminInventory_RowUpdated"
                                                OnRowUpdating="gvAdminInventory_RowUpdating"
                                                OnRowCancelingEdit="gvAdminInventory_RowCancelingEdit"
                                                EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Facility Name")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("vchFacilityName")%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Storage Lot Name")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("LotNumber")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>

                                                            <asp:TextBox ID="txtLotNumber" Text='<%# Eval("LotNumber")%>' runat="server"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnspaceid" runat="server" Value='<%# Eval("LotId") %>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("TTx Lot ID#")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("SerialNumber")%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Total Rows")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("TotalSpaces")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Total Spaces")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("TotalLanes")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Add Storage Lot")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <a href='addparkinglot?fids=<%# Eval("intFacilityID") %>' class="btn btn-white btn-bitbucket" title="Add Lot">
                                                                <i class="fa fa-plus"></i>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                        <HeaderTemplate>
                                                            <%=ResourceMgr.GetMessage("Actions")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <div style="width: 75px;">
                                                                <asp:LinkButton ID="LinkButton1" ToolTip="View Lot" runat="server"
                                                                    CommandName="SpaceInfo" CommandArgument='<%# Eval("LotId") %>' CssClass="btn btn-white btn-bitbucket"> 
                                                                    <i class="fa fa-eye"></i> </asp:LinkButton>
                                                                <asp:LinkButton ID="imgbtnEditSetting" ToolTip="Edit Lot" runat="server" Text="Edit" CausesValidation="false"
                                                                    CommandName="Edit" CommandArgument='<%# Bind("LotId") %>' CssClass="btn btn-white btn-bitbucket"> 
                                                                    <i class="fa fa-edit"></i> </asp:LinkButton>
                                                                <asp:LinkButton ID="imgbtnDeleteSetting" ToolTip="Delete Lot" runat="server" Text="Delete"
                                                                    CommandName="Delete" CommandArgument='<%# Bind("LotId") %>' CssClass="btn btn-white btn-bitbucket">
                                                                    <i class="fa fa-remove"></i> </asp:LinkButton>
                                                            </div>

                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <cc1:ResourceLinkButton ID="btnUpdateSetting" runat="server" CausesValidation="true"
                                                                ToolTip="Update Lot" ValidationGroup="updateSettingValidation" TextMessage="Update"
                                                                CommandName="update" CssClass="btn btn-white" ImageUrl="~/Images/add_new_icon2.png" />
                                                            <cc1:ResourceLinkButton ID="btnCancelSetting" runat="server" CausesValidation="false"
                                                                ToolTip="Cancel Lot" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-white"
                                                                ImageUrl="~/Images/delete_icon.png" />
                                                        </EditItemTemplate>
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

                <%    //////////////////////////// Delete Lot Grid      //////////////////////////////////////////%>

                <div id="dvLotDeteleInfo" runat="server" visible="false" class="box_blockCmp">
                    <div class="ajaxModal-popup inmodal">
                        <div class="ajaxModal-body animated bounceInRight" id="Div3" runat="server">
                            <div class="modal-body">
                                <div style="text-align: center;">
                                    <asp:Label ID="lblTireNoFoundError" runat="server" Text="No Tire In This Lot You can Delete ." CssClass="error_message"></asp:Label>
                                </div>
                                <asp:LinkButton ID="lnkbtnCancelLot" CssClass="btn btn-white" OnClick="lnkCancelLot_Click" OnClientClick="$('#dvLotDeteleInfo').hide();" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnDeleteLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                    CausesValidation="true" CssClass="btn btn-primary" OnClick="lnkbtnDeleteLot_Click" OnClientClick="$('#dvLotDeteleInfo').hide();"><%= ResourceMgr.GetMessage("Delete")%></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>


                <%    ////////////////////////////  Parking Lot Grid      //////////////////////////////////////////%>

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
                                        <%= ResourceMgr.GetMessage("Facility Storage LOTS")%>
                                    </h4>
                                </div>
                                <div style="text-align: center;">
                                    <asp:Label ID="lblErrorPermanentLotdv" runat="server"></asp:Label>
                                </div>

                                <div style="text-align: left; padding: 5px 0px 0px 5px;">
                                    <asp:Label ID="lblTireInfoDisplay" runat="server" Text=""></asp:Label>
                                </div>
                                <br clear="all" />
                                <div style="text-align: center;">
                                    <asp:Label ID="lbldeleteerror" runat="server" Text="" CssClass="error_message"></asp:Label>
                                </div>


                                <div class="modal-body">

                                    <asp:GridView ID="grvPermanentLot" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table"
                                        EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                        EnableViewState="true" EmptyDataText="No data found" wrap="nowrap" CellPadding="0"
                                        Width="100%" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="20">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <input id="Radio1" type="radio" name="rbt" value='<%# Eval("LotId")%>' onclick="javascript: SelectLot(this.value); RadioCheckgrvPermanentLot(this);" />
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
                                        <div class="col-sm-12">
                                            <uc2:Pager ID="pgrLoad" runat="server" />
                                        </div>
                                    </div>

                                    <small class="font-bold">
                                        <asp:Label ID="lblTotalTire" runat="server"></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Style="" Text="Total Tires:"></asp:Label>
                                    </small>
                                </div>

                                <div class="modal-footer">
                                    <asp:LinkButton ID="lnkCancel1" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                        CausesValidation="true" CssClass="btn btn-primary btn-sm" OnClick="lnkPermanentLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>

                                </div>

                            </div>

                            <br clear="all" />
                            <%    ////////////////////////////  Space Info Pop After Parking Lot     //////////////////////////////////////////%>




                            <div id="dvSpace" runat="server" class="ajaxModal-body animated bounceInRight" visible="false">
                                <div class="">
                                    <div class="modal-header">
                                        <div id="Div6" runat="server">
                                            <h4 class="modal-title">
                                                <%= ResourceMgr.GetMessage("Scrap Bin Rows")%>
                                            </h4>

                                        </div>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblErrorPermanentLotSpacedv" CssClass="error_message" runat="server"></asp:Label>
                                        <asp:GridView ID="grdSpaces" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table mb0"
                                            EnableViewState="true" EmptyDataText="There is no Row available in this Parking Lot"
                                            EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                            wrap="nowrap" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <input id="Radio1" type="radio" name="rbt" value='<%# Eval("intSpaceId")%>'
                                                            runat="server" onclick="javascript: SelectSpace(this.value); RadioCheckgrdSpaces(this);" />
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
                                        <%--<a class="reg_button" onclick="ShowOrganizationSelection(); return false;" style="cursor: pointer;">
                            <%= ResourceMgr.GetMessage("Cancel")%></a>--%>                                        
                                        <asp:LinkButton ID="lnkCancel2" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click"
                                            runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtn" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                            CausesValidation="true" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnBackSpace_Click"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                            CausesValidation="true" CssClass="btn btn-primary btn-sm" OnClick="lnkSpacePerLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                                    </div>
                                    <br clear="all" />

                                </div>
                            </div>


                            <%    ////////////////////////////  Lane Info Pop After Space     //////////////////////////////////////////%>


                            <div id="dvlane" runat="server" class="ajaxModal-body animated bounceInRight" visible="false">
                                <div class="">
                                    <div class="modal-header">
                                        <div id="Div7" runat="server">
                                            <h4 class="modal-title">
                                                <%= ResourceMgr.GetMessage("Scrap Bin Spaces")%>
                                            </h4>
                                            <asp:Label ID="lblErrorPermanentLotLanedv" CssClass="error" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="modal-body">
                                        <asp:GridView ID="gvlane" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table mb0"
                                            EnableViewState="true" EmptyDataText="There is no Space available in this Parking LOT" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                            wrap="nowrap" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="20">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <input id="Radio1" type="radio" name="rbt" value='<%# Eval("intLaneId")%>' runat="server" onclick="javascript: SelectLane(this.value); RadioCheckgvlane(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
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
                                        <%--<a class="reg_button" onclick="ShowOrganizationSelection(); return false;" style="cursor: pointer;">
                            <%= ResourceMgr.GetMessage("Cancel")%></a>--%>
                                        <asp:LinkButton ID="lnkCancel3" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click"
                                            runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton3" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                            CausesValidation="true" CssClass="btn btn-primary btn-sm" Style="width: 125px;" OnClick="lnkLanePerLot_Click"><%= ResourceMgr.GetMessage("Move And Delete")%></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton4" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                            CausesValidation="true" CssClass="btn btn-white btn-sm" OnClick="lnkbtnBackLane_Click"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                                    </div>
                                    <br clear="all" />

                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <%    ////////////////////////////  Tire Counter Div     //////////////////////////////////////////%>

                <div id="dvSpaceInfo" class="ajaxModal-popup inmodal" visible="false" runat="server">
                    <div class="ajaxModal-body animated bounceInRight">
                        <div class="modal-header">
                            <div id="dvLoadSummaryInfo" runat="server">
                                <h4 class="modal-title">
                                    <%= ResourceMgr.GetMessage("Lot Tires Info")%>
                                </h4>
                                <label class="font-bold">
                                    <asp:Label ID="lblLotNumberlabel" runat="server" Text="Lot #:"></asp:Label>
                                    <asp:Label ID="lblLotNumber" runat="server"></asp:Label>
                                </label>
                            </div>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="gvSpace" AutoGenerateColumns="False" GridLines="None" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true" EmptyDataText="No data found"
                                wrap="nowrap" CellPadding="0" Width="100%" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Row")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("vchSpaceName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Space")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("nvchLaneName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Total Lanes")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("LaneCount")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Tires")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("TireCount")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Tire Name")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="modal-footer">
                            <cc1:ResourceLinkButton class="reg_button" ID="ResourceLinkButton2" runat="server"
                                CssClass="btn btn-primary" OnClick="btnCancelDetail_Click">Back</cc1:ResourceLinkButton>
                        </div>
                    </div>
                </div>


            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnInventorySearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:HiddenField ID="hdnoldlotid" runat="server" />
        <asp:HiddenField ID="hdnnewlotid" runat="server" />


    </div>

</asp:Content>

