<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="ViewDeliveryReceipt.aspx.cs" Inherits="DeliveryReceipt_ViewDeliveryReceipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function ClearSearchFields() {
            $("#<%=txtShipFrom.ClientID%>").val('');
            $("#<%=txtFrmDeliveryDate.ClientID%>").val('');
            $("#<%=txtToDeliveryDate.ClientID%>").val('');
            $("#<%=txtDeliveryName.ClientID%>").val('');
            $("#<%=btnDeliveryReceiptSearch.ClientID%>")[0].click();
        }
        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("body");
            $(".ajax-loader").remove();
        }

        function AjaxLoader() {
            $(".ajax-loader").appendTo("body");
        }

    </script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>

    <div class="grid-contain-outer">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                    <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5><%= ResourceMgr.GetMessage("Search Filters")%></h5>
                    </div>
                    <div class="ibox-content">
                        <div id="midSearch" class="row search-filter">
                            <div class="search-filter-outer" style="height: auto;">
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnDeliveryReceiptSearch">
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("Delivery Name")%></label>
                                        <asp:TextBox ID="txtDeliveryName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label><%= ResourceMgr.GetMessage("Ship From")%></label>
                                        <asp:TextBox ID="txtShipFrom" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div id="date_range">
                                        <div class="input-daterange">
                                            <div class="form-group col-md-4 col-lg-3">
                                                <label><%= ResourceMgr.GetMessage("From")%></label>
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFrmDeliveryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-4 col-lg-3">
                                                <label><%= ResourceMgr.GetMessage("To")%></label>
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtToDeliveryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12 mb0">
                                        <cc1:ResourceLinkButton ID="btnDeliveryReceiptSearch" runat="server" CssClass="btn btn-sm btn-primary font-bold"
                                            OnClick="btnSearch_Click"> <%= ResourceMgr.GetMessage("Search")%></cc1:ResourceLinkButton>
                                        <cc1:ResourceLinkButton ID="btnDeliveryRecCancel" runat="server" CssClass="btn btn-sm btn-white font-bold"
                                            OnClientClick="ClearSearchFields(); return false;"><%= ResourceMgr.GetMessage("Reset")%></cc1:ResourceLinkButton>


                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="ibox-title">
            <h5>Delivery Receipt</h5>

        </div>
        <div class="ibox-content">

            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvDeliveryinfo" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data found" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                    wrap="nowrap" CellPadding="0" Width="100%" runat="server" OnRowCommand="gvDeliveryinfo_RowCommand" OnRowDataBound="gvDeliveryinfo_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" Visible="false" >
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("DeliveryID")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("DeliveryID")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Delivery Name")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("DeliveryName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Ship From")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("OrganizationShipfrom")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Transporter")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("OrganizationTransporter")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Delivery Date")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Convert.ToDateTime(Eval("DeliveryDate")).ToShortDateString()%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Vehicle Details")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("vehicleDetails")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Status")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgApproved" runat="server" ImageUrl="/Images/thumb-up_icon.png"
                                                    ToolTip="Delivery Accepted"
                                                    Visible="false"></asp:ImageButton>

                                                <asp:ImageButton ID="imgRejected" runat="server" ImageUrl="/Images/thumb-down_icon.png"
                                                    ToolTip="Delivery Rejected "
                                                    Visible="false"></asp:ImageButton>

                                                <asp:ImageButton ID="imgPending" runat="server" ImageUrl="/Images/pending.png" ToolTip="Waiting for Delivery response"
                                                    Visible="false"></asp:ImageButton>



                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Status")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:Label ID="imgApproved" runat="server" Visible="false" CssClass="badge badge-primary"> 
                                                    Delivery Accepted
                                                </asp:Label>

                                                <asp:Label ID="imgRejected" runat="server" Visible="false" CssClass="badge badge-danger">
                                                    Delivery Rejected
                                                </asp:Label>

                                                <asp:Label ID="imgPending" runat="server" Visible="false" CssClass="badge badge-warning-light">
                                                    Waiting for Delivery
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left">
                                            <HeaderTemplate>
                                                Actions
                                            </HeaderTemplate>
                                            <ItemTemplate>


                                                <asp:LinkButton ID="ImgbtnDeliveryID" CssClass="btn btn-white btn-bitbucket" runat="server" ToolTip="View Delivery"
                                                    CommandArgument='<%# Eval("DeliveryID") %>' CommandName="DeliveryInfo">
                                                        <i class="fa fa-eye"></i>
                                                </asp:LinkButton>


                                                <asp:LinkButton ID="imgbtnApprove" CssClass="btn btn-white btn-bitbucket" runat="server" ToolTip="Accept Delivery"
                                                    CommandArgument='<%# Bind("DeliveryID") %>' CommandName="Accept" OnClientClick="return confirm('Are you sure you want to Accept This Delivery?');">
                                                        <i class="fa fa-check "></i>
                                                </asp:LinkButton>


                                                <asp:LinkButton ID="imgbtnRejected" CssClass="btn btn-white btn-bitbucket" runat="server" ToolTip="Reject Delivery"
                                                    CommandArgument='<%# Bind("DeliveryID") %>' CommandName="Reject" OnClientClick="return confirm('Are you sure you want to Reject This Delivery?');">
                                                        <i class="fa fa-close"></i>
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <uc2:Pager ID="pgrLoad" runat="server" />
                    </div>

                    <!-- Modal Popup -->
                    <div id="dvMainLoad" runat="server" class="ajaxModal-popup inmodal" visible="false">
                        <div class="ajaxModal-body animated bounceInRight">

                            <div id="dvLoadSummaryInfo" runat="server" class="modal-header">
                                <h4 class="modal-title">
                                    <%= ResourceMgr.GetMessage("Delivery Notes Info")%>
                                </h4>
                            </div>

                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <dl class="dl-horizontal">
                                            <dt runat="server" visible="false">
                                                <asp:Label ID="Label3" runat="server"><%= ResourceMgr.GetMessage("Delivery Notes ID:")%></asp:Label></dt>
                                            <dd runat="server" visible="false">
                                                <asp:Label ID="lblDeliveryID" runat="server"></asp:Label></dd>
                                            <dt>
                                                <asp:Label ID="lblDeNam" runat="server"><%= ResourceMgr.GetMessage("Delivery Notes Name:")%></asp:Label></dt>
                                            <dd>
                                                <asp:Label ID="lblDeliveryName" runat="server"></asp:Label></dd>
                                            <dt>
                                                <asp:Label ID="lbltirecount" runat="server"><%= ResourceMgr.GetMessage("Delivery Date:")%></asp:Label></dt>
                                            <dd>
                                                <asp:Label ID="lblDeliveryDate" runat="server" CssClass=""></asp:Label></dd>
                                            <dt>
                                                <asp:Label ID="Label2" runat="server"><%= ResourceMgr.GetMessage("Delivery Estima:")%></asp:Label></dt>
                                            <dd>
                                                <asp:Label ID="lblDeliveryEstimaDateTime" runat="server" CssClass=""></asp:Label></dd>
                                            <dt>
                                                <asp:Label ID="Label4" runat="server"><%= ResourceMgr.GetMessage("Ship To:")%></asp:Label></dt>
                                            <dd>
                                                <asp:Label ID="lblShipToidnumber" runat="server" CssClass=""></asp:Label></dd>

                                        </dl>
                                    </div>
                                    <div class="col-md-6">
                                        <dl class="dl-horizontal">
                                            <dt>
                                                <asp:Label ID="Label5" runat="server"><%= ResourceMgr.GetMessage("Vehicle Details:")%></asp:Label></dt>
                                            <dd>
                                                <asp:Label ID="lblVehicleDetails" runat="server" CssClass=""></asp:Label></dd>
                                            <dt>
                                                <asp:Label ID="Label6" runat="server"><%= ResourceMgr.GetMessage("Weight:")%></asp:Label></dt>
                                            <dd>
                                                <asp:Label ID="lblWeight" runat="server" CssClass=""></asp:Label></dd>
                                            <dt>
                                                <asp:Label ID="Label1" runat="server" Text="Load Type:"><%= ResourceMgr.GetMessage("Transporter:")%></asp:Label></dt>
                                            <dd>
                                                <asp:Label ID="lblTransporterName" runat="server"></asp:Label></dd>
                                            <dt>
                                                <asp:Label ID="Label7" runat="server" Text="Load Type:"><%= ResourceMgr.GetMessage("Total Loads:")%></asp:Label></dt>
                                            <dd>
                                                <asp:Label ID="lblTotalLoads" runat="server"></asp:Label></dd>
                                            <dt>
                                                <asp:Label ID="Label8" runat="server" Text="Load Type:"><%= ResourceMgr.GetMessage("Total Units:")%></asp:Label></dt>
                                            <dd>
                                                <asp:Label ID="lblLoadTireCount" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <div id="dvLoadTireInfo" runat="server">
                                                <asp:GridView ID="gvAllTire" AutoGenerateColumns="False" DataKeyNames="Productid" CssClass="table table-bordered epr-sec-table" EnableViewState="true"
                                                    EmptyDataText="No data Available" EmptyDataRowStyle-CssClass="alert alert-danger text-center" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" Visible="false">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("ID")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("Productid")%>
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
                                </div>
                            </div>

                            <div class="modal-footer">
                                <cc1:ResourceLinkButton class="" ID="ResourceLinkButton2" runat="server" CssClass="btn btn-white btn-sm" OnClick="btnDeliveryDetailBack_Click">Back</cc1:ResourceLinkButton>
                            </div>
                            <asp:HiddenField ID="hdnlotid" runat="server" />
                        </div>
                    </div>

                    <%--                    <div id="dvMainLoad" runat="server" class="box_blockCmp" visible="false">
                        <div class="popUp_lotInfo">

                            <div id="dvLoadSummaryInfo" runat="server">
                                <div class="textTitle" style="border-bottom: solid 1px #ddd; padding-bottom: 5px; margin-bottom: 20px;">
                                    <%= ResourceMgr.GetMessage("Delivery Notes Info")%>
                                </div>
                            </div>

                            <div class="view-label">
                                <asp:Label ID="Label3" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Delivery Notes ID:")%></asp:Label>
                                <asp:Label ID="lblDeliveryID" runat="server"></asp:Label>
                            </div>
                            <div class="view-label">
                                <asp:Label ID="lblDeNam" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Delivery Notes Name:")%></asp:Label>
                                <asp:Label ID="lblDeliveryName" runat="server"></asp:Label>
                            </div>

                            <div class="view-label">
                                <asp:Label ID="lbltirecount" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Delivery Date:")%></asp:Label>
                                <asp:Label ID="lblDeliveryDate" runat="server" CssClass="lefts"></asp:Label>
                            </div>
                            <div class="view-label">
                                <asp:Label ID="Label2" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Delivery Estima:")%></asp:Label>
                                <asp:Label ID="lblDeliveryEstimaDateTime" runat="server" CssClass="lefts"></asp:Label>
                            </div>
                            <div class="view-label">
                                <asp:Label ID="Label4" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Ship To:")%></asp:Label>
                                <asp:Label ID="lblShipToidnumber" runat="server" CssClass="lefts"></asp:Label>
                            </div>
                            <div class="view-label">
                                <asp:Label ID="Label5" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Vehicle Details:")%></asp:Label>
                                <asp:Label ID="lblVehicleDetails" runat="server" CssClass="lefts"></asp:Label>
                            </div>

                            <div class="view-label">
                                <asp:Label ID="Label6" runat="server" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Weight:")%></asp:Label>
                                <asp:Label ID="lblWeight" runat="server" CssClass="lefts"></asp:Label>
                            </div>

                            <div class="view-label">
                                <asp:Label ID="Label1" runat="server" Text="Load Type:" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Transporter:")%></asp:Label>
                                <asp:Label ID="lblTransporterName" runat="server"></asp:Label>
                            </div>
                            <div class="view-label">
                                <asp:Label ID="Label7" runat="server" Text="Load Type:" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Total Loads:")%></asp:Label>
                                <asp:Label ID="lblTotalLoads" runat="server"></asp:Label>
                            </div>
                            <div class="view-label">
                                <asp:Label ID="Label8" runat="server" Text="Load Type:" Style="font-weight: bold;"><%= ResourceMgr.GetMessage("Total Tires:")%></asp:Label>
                                <asp:Label ID="lblLoadTireCount" runat="server"></asp:Label>
                            </div>
                            <br clear="all" />
                            <div id="dvLoadTireInfo" runat="server" style="overflow-y: scroll; height: 228px; padding-top: 20px;">
                                <asp:GridView ID="gvAllTire" AutoGenerateColumns="False" GridLines="None" DataKeyNames="TireId"
                                    CssClass="add-new-inventory" EnableViewState="true" EmptyDataText="No data Available"
                                    CellPadding="0" Width="100%" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("ID")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("TireId")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Brand")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("Brand")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                <%=ResourceMgr.GetMessage("Week")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("MonthCode")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Year")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("YearCode")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("DOT")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="AllTireDotNumber" runat="server" Text='<%#Eval("DOTNumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
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

                            <div style="padding-left: 29px; padding-bottom: 10px;">

                                <cc1:ResourceLinkButton class="reg_button" ID="ResourceLinkButton2" runat="server"
                                    OnClick="btnReceiptBack_Click">Back</cc1:ResourceLinkButton>

                            </div>
                            <asp:HiddenField ID="hdnlotid" runat="server" />
                        </div>
                    </div>--%>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnDeliveryReceiptSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
