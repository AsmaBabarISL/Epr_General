<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="ViewLoad.aspx.cs" Inherits="Load_ViewLoad" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#asearch').toggle(function () {
                $('#midSearch').slideDown();
            }, function () {
                $('#midSearch').slideUp();
            });
        });

        function ClearSearchFields() {

            $("#<%=txtUserName.ClientID%>").val('');

            $("#<%=txtFrmDate.ClientID%>").val('');
            $("#<%=txtToDate.ClientID%>").val('');
            $("#<%=txtCompanyName.ClientID%>").val('');
            $("#<%=btnInventorySearch.ClientID%>")[0].click();


        }

        function acceptLoad(src) {
            
            if ($(src).hasClass('aspNetDisabled')) {

            }
            else {
                return confirm('Are you sure you want to Accept This Load?');
            }
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
        $(".ajaxModal-popup").appendTo("body");
        $(".ajax-loader").remove();


    }

    function AjaxLoader() {
        $(".ajax-loader").appendTo("body");
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

    <div class="grid-contain-outer" style="display: none;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                    <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <div id="" class="box_blockCmp" visible="false">
            <div class="popUp_lotInfo">

                <div id="Div2" runat="server">
                    <div class="textTitle" style="border-bottom: solid 1px #ddd; padding-bottom: 5px; margin-bottom: 20px;">
                        <%= ResourceMgr.GetMessage("Notes For Reject Load")%>
                    </div>
                </div>


            </div>
        </div>

        <div class="box_blockCmp" id="dvPermanentLot" runat="server" visible="false">
        </div>

    </div>
    <!-- Extra Feilds end here -->

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Search Filters </h5>
                </div>
                <div class="ibox-content" style="display: block;">
                    <!-- Filters-->
                    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnInventorySearch">
                        <div role="form" class="row search-filter" id="">
                            <div class="form-group col-md-4 col-lg-3">
                                <label>
                                    <%= ResourceMgr.GetMessage("Company")%>
                                </label>
                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-lg-3">
                                <label>
                                    <%= ResourceMgr.GetMessage("User")%>
                                </label>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div id="date_range">
                                <div class="input-daterange">
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label>
                                            <%= ResourceMgr.GetMessage("From ")%>
                                        </label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label>
                                            <%= ResourceMgr.GetMessage("To")%>
                                        </label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="form-group col-md-12 mb0">

                                <cc1:ResourceLinkButton ID="btnInventorySearch" runat="server" CssClass="btn btn-sm btn-primary"
                                    OnClick="btnSearch_Click"><strong> <%= ResourceMgr.GetMessage("Search")%> </strong></cc1:ResourceLinkButton>
                                <cc1:ResourceLinkButton ID="btnInventoryCancel" runat="server" CssClass="btn btn-sm btn-white"
                                    OnClientClick="ClearSearchFields(); return false;"><strong><%= ResourceMgr.GetMessage("Reset")%></strong></cc1:ResourceLinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%=ResourceMgr.GetMessage("Loads") %></h5>
                            <div class="ibox-tools">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlLoadStatus" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlLoadStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <cc1:ResourceLinkButton CssClass="btn btn-primary btn-sm" ID="dvAdd" runat="server" OnClick="btnAddLoad_Click"> 
                            <i class="fa fa-plus"></i> <%= ResourceMgr.GetMessage("Add Load")%></cc1:ResourceLinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">

                                <div class="col-sm-12">
                                    <div class="table-responsive">

                                        <asp:GridView ID="gvloadsinfo" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data found"
                                            wrap="nowrap" runat="server" OnRowCommand="gvloadsinfo_RowCommand" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                            OnRowDataBound="gvloadsinfo_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Load")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("LoadNumber")%>
                                                        <asp:LinkButton ID="lnkbtnLoadNumber" Visible="false" runat="server" CommandName="LoadTireInfo" CommandArgument='<%# Bind("LoadId")%>' Text='<%# Eval("LoadNumber")%>'> </asp:LinkButton>

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
                                                        <%=ResourceMgr.GetMessage("Company")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("LegalName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Transfer Company")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("HaulerCompany")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Load Type")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litType" runat="server" Text='<%# Eval("LookupTypeName")%>'></asp:Literal>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Status")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="imgbtnApproved" runat="server"
                                                            Visible='<%# Convert.ToBoolean(Eval("bitIsAccepted"))==true && Convert.ToBoolean(Eval("bitReject"))==false ? true: false  %>'> 
                                     <span class="badge badge-primary" >Approved</span>
                                                        </asp:Label>
                                                        <asp:Label ID="imgbtnRejected" runat="server"
                                                            Visible='<%# Convert.ToBoolean(Eval("bitReject"))==true && Convert.ToBoolean(Eval("bitIsAccepted"))==false ? true: false  %>'>
                                         <span class ="badge badge-danger">Rejected</span>
                                                        </asp:Label>
                                                        <asp:Label ID="imgbtnPending" runat="server"
                                                            Visible='<%#  Convert.ToBoolean(Eval("bitIsAccepted"))==false && Convert.ToBoolean(Eval("bitReject"))==false ? true: false  %>'>
                                         <span class ="badge badge-warning-light">Pending</span>
                                                        </asp:Label>
                                                        <asp:Label ID="imgbtnTransfered" runat="server">
                                          <span class ="badge badge-info">Transfered</span>
                                                        </asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Created Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("DateCreated")).ToShortDateString()%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("User")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("UserName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="90" ItemStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        Actions
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width: 75px;">
                                                            <asp:LinkButton ID="ImgbtnLoadNumber" runat="server" ToolTip="View Loads"
                                                                CommandArgument='<%# Eval("LoadId") %>' CommandName="LoadTireInfo" CssClass="btn btn-white btn-bitbucket"> 
                                            <i class="fa fa-eye"></i>
                                                            </asp:LinkButton>
                                                            <asp:HiddenField ID="hdLoadNumber" runat="server" Value='<%# Eval("LoadNumber") %>' />
                                                            <asp:LinkButton ID="imgbtnTransfer" runat="server" CommandArgument='<%# Bind("OrganizationId") %>' ToolTip="Transfer Load"
                                                                CommandName="transfer" CssClass="btn btn-white btn-bitbucket">
                                        <i class="fa fa-truck"></i>
                                                            </asp:LinkButton>
                                                            <asp:HiddenField ID="hidLoadSelectId" runat="server" Value='<%# Eval("LoadId") %>' />
                                                            <asp:LinkButton ID="imgbtnApprove" runat="server"
                                                                ToolTip="Accept this Ship" OnClientClick="acceptLoad(this);"
                                                                CommandName="Accept" CommandArgument='<%# Bind("LoadId") %>' CssClass="btn btn-white btn-bitbucket">
                                    <i class="fa fa-check"></i> 
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="imgbtnDisApprove" runat="server"
                                                                ToolTip="Reject this Ship" OnClientClick="return confirm('Are you sure you want to Reject This Load?');"
                                                                CommandName="Reject" CommandArgument='<%# Bind("LoadId")%>' CssClass="btn btn-white btn-bitbucket">
                                  <i class="fa fa-close"></i>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton ID="imgbtnEditLoad" runat="server" CssClass="btn btn-white btn-bitbucket" ToolTip="Edit Load Info"
                                                                CommandName="Edit" CommandArgument='<%# Bind("LoadId")%>'>
                                   <i class="fa fa-edit"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <uc2:Pager ID="pgrLoad" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>



            <div id="dvMainLoad" runat="server" class="ajaxModal-popup inmodal" visible="false">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <div id="Div3" runat="server">
                            <h4 class="modal-title">
                                <%= ResourceMgr.GetMessage("Load Tires Info")%>
                            </h4>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <dl class="dl-horizontal">
                                    <dt>
                                        <asp:Label ID="lblLotlabelLane" runat="server" Text="Load Name:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblLoadNumber" runat="server"></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="lbltirecount" runat="server" Text="Load Tire Count:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblLoadTireCount" runat="server" CssClass=""></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label2" runat="server" Text="Transferor Name:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblTransferOrganization" runat="server" CssClass=""></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label4" runat="server" Text="Hauler Name:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblHaulerOrganization" runat="server" CssClass=""></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label1" runat="server" Text="Load Type:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblLoadTypeName" runat="server"></asp:Label></dd>
                                </dl>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:GridView ID="grvLoadTireInfo" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table mb0" EnableViewState="true"
                                    EmptyDataText="No data available" runat="server" OnRowDataBound="grvLoadTireInfo_RowDataBound"
                                    EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                    <Columns>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Product Serial #")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("SerialNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Company")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("LegalName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("DOT")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdndotnumber" runat="server" Value='<%# Eval("DOTNumber")%>' />
                                                <asp:Label ID="lbldotnumber" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Status")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("ActionName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("OutCome")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("OutComeName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Class")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("ClassName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnlotid" runat="server" />
                    <asp:HiddenField ID="hdnCurrentPage" runat="server" />
                    <div class="modal-footer">
                        <cc1:ResourceLinkButton CssClass="btn btn-primary" ID="ResourceLinkButton1" runat="server" Visible="false"
                            OnClick="btnAddLoadType_Click"><%=ResourceMgr.GetMessage("Save") %> </cc1:ResourceLinkButton>
                        <cc1:ResourceLinkButton CssClass="btn btn-primary" ID="ResourceLinkButton2" runat="server"
                            OnClick="btnAddLoadTypeCancel_Click"><%=ResourceMgr.GetMessage("Back") %></cc1:ResourceLinkButton>
                    </div>
                </div>
            </div>

            <div id="divProductLoad" runat="server" class="ajaxModal-popup inmodal" visible="false">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <div id="Div9" runat="server">
                            <h4 class="modal-title">
                                <%= ResourceMgr.GetMessage("Load Products Info")%>
                            </h4>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <dl class="dl-horizontal">
                                    <dt>
                                        <asp:Label ID="Label3" runat="server" Text="Load Name:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblLoadName" runat="server"></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label6" runat="server" Text="Load Tire Count:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblProductCount" runat="server" CssClass=""></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label8" runat="server" Text="Transfer Name:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblTransferName" runat="server" CssClass=""></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label10" runat="server" Text="Hauler Name:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblHauerName" runat="server" CssClass=""></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="Label12" runat="server" Text="Load Type:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblLoadType" runat="server"></asp:Label></dd>
                                </dl>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:GridView ID="gvLoadProductInfo" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table mb0"
                                    EnableViewState="true" EmptyDataText="No data available" runat="server"
                                    EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                    <Columns>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Product Serial #")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("SerialNumber").ToString().Split('.')[0]%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Company")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("LegalName")%>
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




                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <cc1:ResourceLinkButton CssClass="btn btn-white btn-sm" ID="btnBackProductPopup" runat="server"
                            OnClick="btnBackProductPopup_Click"><%=ResourceMgr.GetMessage("Back") %></cc1:ResourceLinkButton>
                    </div>
                </div>
            </div>

            <div id="dvAcceptLoad" runat="server" class="ajaxModal-popup inmodal" visible="false">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <%= ResourceMgr.GetMessage("Accept Delivery First")%>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <cc1:ResourceLabel runat="server" ID="lblAcceptLoad"></cc1:ResourceLabel>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <cc1:ResourceLinkButton CssClass="btn btn-white btn-sm" ID="btnBackAcceptLoad" OnClick="btnBackAcceptLoad_Click" runat="server"><%=ResourceMgr.GetMessage("Back") %></cc1:ResourceLinkButton>
                    </div>
                </div>
            </div>

            <asp:HiddenField ID="hdnLoadId" runat="server" />
            <asp:Panel ID="pnlPermanentLot" runat="server">
                <asp:HiddenField ID="hidSelectedOrgId" runat="server" Visible="false" />
                <asp:HiddenField ID="hidSelectedLot" runat="server" Value="" />
                <asp:HiddenField ID="hidSelectedSpace" runat="server" Value="" />
                <asp:HiddenField ID="hidSelectedLane" runat="server" Value="" />

                <div id="dvParkingLot1" runat="server" class="ajaxModal-popup inmodal" visible="false">
                    <div class="ajaxModal-body animated bounceInRight">
                        <div class="modal-header">
                            <div id="Div5" runat="server">
                                <h4 class="modal-title">
                                    <%= ResourceMgr.GetMessage("Facility Storage LOTS")%>
                                </h4>
                                <asp:Label ID="lblErrorPermanentLotdv" CssClass="error_message" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="modal-body table-responsive">
                            <h4>
                                <b>
                                    <asp:Label ID="lblNotify" runat="server"></asp:Label></b>
                            </h4>
                            <br />
                            <asp:GridView ID="grvPermanentLot" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table mb0"
                                EnableViewState="true" EmptyDataText="No data found" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                wrap="nowrap" runat="server">

                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<label class ="radio">--%>
                                            <input runat="server" id="Radio1" type="radio" name="rbt" value='<%# Eval("LotId")%>' onclick="javascript: SelectLot(this.value); RadioCheckgrvPermanentLot(this);" />
                                            </label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Facility")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("vchFacilityName")%>
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
                                            <%=ResourceMgr.GetMessage("Lot")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("LotNumber")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Total Rows")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("SpaceCount")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <%=ResourceMgr.GetMessage("Total Spaces")%>
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
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <div class="modal-footer">
                            <asp:LinkButton ID="lnkCancel1" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click" runat="server">
                                    <%= ResourceMgr.GetMessage("Cancel")%>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkPermanentLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                CausesValidation="true" CssClass="btn btn-primary btn-sm" OnClick="lnkPermanentLot_Click">
                                    <%= ResourceMgr.GetMessage("Continue")%>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>

                <div id="dvSpace" runat="server" class="ajaxModal-popup inmodal" visible="false">
                    <div class="ajaxModal-body animated bounceInRight">
                        <div class="modal-header">
                            <div id="Div6" runat="server">
                                <h4 class="modal-title">
                                    <%= ResourceMgr.GetMessage("Scrap Bin Rows")%>
                                </h4>
                                <asp:Label ID="lblErrorPermanentLotSpacedv" CssClass="error" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="grdSpaces" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table mb0"
                                EnableViewState="true" EmptyDataText="There is no Row available in this Parking Lot"
                                EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                wrap="nowrap" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="20">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("intSpaceId")%>'
                                                onclick="javascript: SelectSpace(this.value); RadioCheckgrdSpaces(this);" />

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


                            <div class="modal-footer">

                                <asp:LinkButton ID="lnkCancel2" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click" runat="server">
                                            <%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkBackPermanentLotSpace" CssClass="btn btn-white btn-sm"
                                    OnClick="lnkBackPermanentLotSpace_Click" runat="server"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkSpacePerLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                    CausesValidation="true" CssClass="btn btn-primary btn-sm" OnClick="lnkSpacePerLot_Click"><%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>



                <div id="dvlane" runat="server" class="ajaxModal-popup inmodal" visible="false">
                    <div class="ajaxModal-body animated bounceInRight">
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
                                EnableViewState="true" EmptyDataText="There is no Space available in this Parking LOT"
                                EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                wrap="nowrap" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" ItemStyle-Width="20">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <input id="Radio1" runat="server" type="radio" name="rbt" value='<%# Eval("intLaneId")%>'
                                                onclick="javascript: SelectLane(this.value); RadioCheckgvlane(this);" />

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


                            <div class="modal-footer">
                                <asp:LinkButton ID="lnkCancel3" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click" runat="server">
                                    <%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>

                                <asp:LinkButton ID="lnkBackPermanentLotLane" CssClass="btn btn-white btn-sm" OnClick="lnkBackPermanentLotLane_Click"
                                    runat="server"><%= ResourceMgr.GetMessage("Back")%></asp:LinkButton>

                                <asp:LinkButton ID="lnkLanePerLot" runat="server" ValidationGroup="AddInventoryValidationGroup"
                                    CausesValidation="true" CssClass="btn btn-primary btn-sm" OnClick="lnkLanePerLot_Click">
                                    <%= ResourceMgr.GetMessage("Continue")%></asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>

                <div id="dvRejectLoadNotes" runat="server" class="ajaxModal-popup inmodal" visible="false">
                    <div class="ajaxModal-body animated bounceInRight">
                        <div class="modal-header">
                            <div id="Div8" runat="server">
                                <h4 class="modal-title">
                                    <%= ResourceMgr.GetMessage("Rejection Notes")%>
                                </h4>
                            </div>
                        </div>
                        <div class="row modal-body">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-3"></div>
                        </div>

                        <div class="modal-footer">
                            <cc1:ResourceLinkButton class="btn btn-white btn-sm" ID="btnRejectNotesCancel" runat="server"
                                OnClick="btnRejectNotesCancel_Click"><%=ResourceMgr.GetMessage("Back") %></cc1:ResourceLinkButton>
                            <cc1:ResourceLinkButton class="btn btn-primary btn-sm" ID="btnRejectNotesSave" runat="server"
                                OnClick="btnRejectNotesSave_Click"><%=ResourceMgr.GetMessage("Save") %> </cc1:ResourceLinkButton>
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

