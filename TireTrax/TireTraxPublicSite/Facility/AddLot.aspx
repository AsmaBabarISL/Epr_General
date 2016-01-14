<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="AddLot.aspx.cs" Inherits="Facility_AddLot" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>

<%@ Register Src="~/Facility/Controls/FacilityLots.ascx" TagName="FacilityLot" TagPrefix="uc3" %>
<%@ Register Src="~/Facility/Controls/LotRows.ascx" TagName="LotRow" TagPrefix="uc4" %>
<%@ Register Src="~/Facility/Controls/LotSpace.ascx" TagName="LotSpace" TagPrefix="uc5" %>
<%@ Register Src="~/Facility/Controls/SpaceTires.ascx" TagName="SpaceTires" TagPrefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ClearLblErrorLot() {
            if ($("#<%=txtParkingLot.ClientID%>").val() == "") {
                $("#<%=lblErrorLot.ClientID%>").text("");
            }


        }
        function ClearLblErrorSpace() {
            if ($("#<%=txtSpaceheader.ClientID%>").val() == "") {
                $("#<%=lblErrorSpace.ClientID%>").text("");
            }


        }
        function ClearLblErrorLane() {
            if ($("#<%=txtLaneheader.ClientID%>").val() == "") {
                $("#<%=lblErrorLane.ClientID%>").text("");
            }


        }

        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("form");
            //$(".ajax-loader").remove();
            $(".ajax-loader").hide();
        }

        function AjaxLoader() {
            $(".ajax-loader").show();
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
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div id="Div1" class="ajax-loader" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <asp:Label ID="lbldeleteError" runat="server" Visible="false"></asp:Label>

            <asp:Panel ID="pnlLot" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>
                                    <asp:Label ID="lblCreateNewLot" runat="server" Text="Create New Storage Lot"></asp:Label></h5>
                            </div>
                            <div class="ibox-content">
                                <div role="form" class="row search-filter" id="">
                                    <div class="col-md-12">
                                        <dl class="inline">
                                            <dt class="inline">
                                                <asp:Label ID="lblfacilitynamelbl" runat="server" Text=" Facility:"></asp:Label></dt>
                                            <dd class="inline">
                                                <asp:Label ID="lblfacilityname" runat="server" CssClass="badge badge-primary"></asp:Label></dd>
                                        </dl>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>
                                            <asp:Label ID="lblCreatingTextDisplayLot" runat="server" Text="Type Storage Lot:"></asp:Label></label>
                                        <asp:TextBox ID="txtParkingLot" runat="server" CssClass="form-control" MaxLength="100" OnTextChanged="txtParkingLot_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfvAcount" ValidationGroup="LotInfo"
                                            CssClass="custom-error" runat="server" ErrorText="Enter Parking Lot" ControlToValidate="txtParkingLot"
                                            Display="Dynamic" ErrorMessage="Enter Storage Parking Lot"></cc1:ResourceRequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-md-12 mb0">
                                        <asp:LinkButton ID="lnkbtnNext" CssClass="btn btn-sm btn-primary font-bold"
                                            OnClientClick="ClearLblErrorLot();" runat="server" Text="Add" CausesValidation="true"
                                            ValidationGroup="LotInfo" OnClick="lbkNextLot_Click"></asp:LinkButton>

                                        <asp:LinkButton ID="lnkbtnCancelLot" CssClass="btn btn-sm btn-white font-bold" runat="server" Text="Back" CausesValidation="false"
                                            OnClick="lbkCancel_Click"></asp:LinkButton>

                                        <asp:LinkButton ID="lbkSaveLot" CssClass="btn btn-sm btn-primary font-bold"
                                            Style="display: none;" OnClientClick="ClearLblErrorLot();" runat="server" Text="Done"
                                            CausesValidation="true" ValidationGroup="LotInfo" OnClick="lbkSave_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <asp:Label ID="lblErrorLot" runat="server" Visible="false"></asp:Label>
                </div>
                <div id="dvLotRecord" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5><%= ResourceMgr.GetMessage("Facility Storage Lots")%></h5>
                                </div>
                                <div class="ibox-content">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvLot" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table" EnableViewState="true" DataKeyNames="LotId,LotNumber" EmptyDataText="No data found"
                                                    EmptyDataRowStyle-CssClass="alert alert-danger text-center" OnRowCancelingEdit="gvLot_RowCancelingEdit"
                                                    OnRowCommand="gvLot_RowCommand" OnRowDeleted="gvLot_RowDeleted" OnRowDeleting="gvLot_RowDeleting"
                                                    OnRowEditing="gvLot_RowEditing" OnRowUpdated="gvLot_RowUpdated" OnRowUpdating="gvLot_RowUpdating">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Storage Lot">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllotnumber" runat="server" Text='<%# Eval("LotNumber")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdGVLotId" runat="server" Value='<%# Eval("LotId") %>' />
                                                                <asp:HiddenField ID="hdGVLotNumber" runat="server" Value='<%# Eval("LotNumber") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtLotNumber" runat="server" Width="280px" Text='<%# Eval("LotNumber") %>'></asp:TextBox>
                                                                <asp:HiddenField ID="hdnlotid1" runat="server" Value='<%# Eval("LotId") %>' />
                                                                <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor2" ForeColor="Red" ValidationGroup="updateSettingValidation"
                                                                    runat="server" ControlToValidate="txtLotNumber" ErrorText="*" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                            </EditItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Rows")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("TotalSpaces")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Spaces")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("TotalLanes")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Add Rows">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="imgaddlane" ToolTip="Add Rows" runat="server" CausesValidation="false" Text="Add Rows"
                                                                    CommandName="AddLotSpace" CommandArgument='<%# Eval("LotId") %>' CssClass="btn btn-white btn-bitbucket"> 
                                                                     <i class="fa fa-plus"></i> </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <div style="width: 75px;">
                                                                    <asp:LinkButton ID="lnkbtnFacilityName" ToolTip="View Rows" runat="server" CommandName="RowInfoPopUp" CausesValidation="false" CommandArgument='<%# Eval("LotId") %>'
                                                                        CssClass="btn btn-white btn-bitbucket"> <i class="fa fa-eye"></i> </asp:LinkButton>

                                                                    <asp:LinkButton ID="imgbtnEditSetting" ToolTip="Edit Storage" runat="server" CommandName="Edit" CausesValidation="false" Text="Edit"
                                                                        CommandArgument='<%# Eval("LotId") %>' CssClass="btn btn-white btn-bitbucket"> <i class="fa fa-edit"></i> </asp:LinkButton>

                                                                    <asp:LinkButton ID="imgbtnDeleteSetting" ToolTip="Delete Storage Lot" runat="server" CommandName="Delete" CausesValidation="false" Text="Delete"
                                                                        CommandArgument='<%# Eval("LotId") %>' CssClass="btn btn-white btn-bitbucket" OnClientClick="return confirm('Are you sure to Delete this Row?');"> 
                                                                        <i class="fa fa-remove"></i> </asp:LinkButton>
                                                                </div>


                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <cc1:ResourceLinkButton ID="lnkbtnAddSetting" runat="server" CommandName="Insert"
                                                                    ToolTip="Add Lot" CausesValidation="true" TextMessage="Add" CssClass="btn btn-white"
                                                                    Visible="false" ValidationGroup="InsertSettingValidation" ImageUrl="~/Images/add_icon.png"></cc1:ResourceLinkButton>
                                                                <cc1:ResourceLinkButton ID="lnkbtnCancelSetting" runat="server" CommandName="CancelSetting"
                                                                    ToolTip="Cancel Lot" CausesValidation="false" TextMessage="Cancel" CssClass="btn btn-white"
                                                                    Visible="false" ValidationGroup="InsertSettingValidation" ImageUrl="~/Images/delete_icon.png"></cc1:ResourceLinkButton>
                                                                <cc1:ResourceLinkButton ID="lnkbtnAddMore" TextMessage="Add More" runat="server"
                                                                    ToolTip="Add More Lot" CssClass="btn btn-white" CommandName="AddMore"
                                                                    ImageUrl="~/Images/add_icon.png"></cc1:ResourceLinkButton>
                                                            </FooterTemplate>
                                                            <EditItemTemplate>
                                                                <cc1:ResourceLinkButton ID="btnUpdateSetting" runat="server" CausesValidation="true"
                                                                    ToolTip="Update Storage Lot" ValidationGroup="updateSettingValidation" TextMessage="Update"
                                                                    CommandName="update" CssClass="btn btn-white" ImageUrl="~/Images/add_new_icon2.png" />
                                                                <cc1:ResourceLinkButton ID="btnCancelSetting" runat="server" CausesValidation="false"
                                                                    ToolTip="Cancel Storage Lot" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-white"
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
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlSpace" runat="server" Visible="false">

                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>
                                    <asp:Label ID="lblCreateNewSpace" runat="server" Text="Create New Row"></asp:Label></h5>

                            </div>
                            <div class="ibox-content">
                                <div role="form" class="row search-filter" id="">
                                    <div class="col-md-12">
                                        <dl>
                                            <dt class="inline">
                                                <asp:Label ID="lblFacilityLabelForSpace" runat="server" Text="Facility:"></asp:Label></dt>
                                            <dd class="inline">
                                                <asp:Label ID="lblFacilityForSpace" CssClass="badge badge-primary" runat="server"></asp:Label></dd>
                                        </dl>
                                        <dl>
                                            <dt class="inline">
                                                <asp:Label ID="lblLotlabelSpace" runat="server" Text="Facility Lot:"></asp:Label></dt>
                                            <dd class="inline">
                                                <asp:Label ID="lblLotNumberSpace" CssClass="badge badge-primary" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>
                                            <asp:Label ID="lblCreatingTextDisplaySpace" runat="server" Text="Enter New Row Name:"></asp:Label></label>
                                        <asp:TextBox ID="txtSpaceheader" runat="server" MaxLength="500" AutoPostBack="true" CssClass="form-control"
                                            OnTextChanged="txtSpaceheader_TextChanged"></asp:TextBox>

                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" ValidationGroup="SpaceInfo"
                                            CssClass="custom-error" runat="server" ErrorText="Enter New Row Name" ControlToValidate="txtSpaceheader"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                        <asp:Label ID="lblErrorSpace" runat="server" Visible="false"></asp:Label>
                                    </div>

                                    <div class="form-group col-md-12 mb0">
                                        <asp:LinkButton ID="lblSaveSpace" CssClass="btn btn-sm btn-primary font-bold" runat="server"
                                            Style="display: none;" OnClientClick="ClearLblErrorSpace();" Text="Done" CausesValidation="true"
                                            ValidationGroup="SpaceInfo"
                                            OnClick="lbkSaveSpace_Click"></asp:LinkButton>

                                        <asp:LinkButton ID="lblNext" CssClass="btn btn-sm btn-primary font-bold" runat="server"
                                            OnClientClick="ClearLblErrorSpace();" Text="Add" CausesValidation="true" ValidationGroup="SpaceInfo"
                                            OnClick="lbkNextRow_Click"></asp:LinkButton>

                                        <asp:LinkButton ID="LinkButton2" CssClass="btn btn-sm btn-white font-bold" runat="server" Text="Back" CausesValidation="false"
                                            OnClick="lbkCancelSpace_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="dvSpaceRecord" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5><%= ResourceMgr.GetMessage("Facility Storage Lots Rows")%></h5>

                                </div>
                                <div class="ibox-content">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table-responsive">

                                                <asp:GridView ID="gvSetting" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                    CssClass="table table-bordered epr-sec-table" EnableViewState="true" DataKeyNames="intSpaceId"
                                                    EmptyDataText="No data found" wrap="nowrap" CellPadding="0" Width="100%"
                                                    OnRowEditing="gvSetting_RowEditing" OnRowCommand="gvSetting_RowCommand" OnRowUpdated="gvSetting_RowUpdated"
                                                    OnRowUpdating="gvSetting_RowUpdating" OnSelectedIndexChanged="gvSetting_SelectedIndexChanged"
                                                    OnRowCancelingEdit="gvSetting_RowCancelingEdit" OnRowDeleted="gvSetting_RowDeleted"
                                                    OnRowDeleting="gvSetting_RowDeleting" EmptyDataRowStyle-CssClass="alert alert-danger text-center">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Rows Name" HeaderStyle-CssClass="txt-had" ItemStyle-Width="300">
                                                            <HeaderStyle HorizontalAlign="left" />
                                                            <ItemStyle HorizontalAlign="left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblspacename" runat="server" Text='<%# Eval("vchSpaceName")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnspaceid" runat="server" Value='<%# Eval("intSpaceId") %>' />


                                                                <asp:Image ID="Image1" runat="server" Visible="false" src="/Images/history_icon.png" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtSpaces" runat="server" Width="280px" Text='<%# Eval("vchSpaceName") %>'></asp:TextBox><br />
                                                                <asp:HiddenField ID="hdnspaceid1" runat="server" Value='<%# Eval("intSpaceId") %>' />
                                                                <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor2" ForeColor="Red" ValidationGroup="updateSettingValidation"
                                                                    runat="server" ControlToValidate="txtSpaces" ErrorText="*" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                            </EditItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Total Spaces")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("TotalLanes")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="History" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemTemplate>
                                                                <a href="#">
                                                                    <img title="View History" src="/Images/history_icon.png" /></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Add Spaces" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="imgaddlane" ToolTip="Add Space" runat="server" CausesValidation="false" Text="Add Lane"
                                                                    CommandName="AddSpaceLane" CommandArgument='<%# Eval("intSpaceId") %>' CssClass="btn btn-white btn-bitbucket"> 
                                                                     <i class="fa fa-plus"></i> </asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="txt-had" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>

                                                                <div style="width: 75px;">
                                                                    <asp:LinkButton ID="lnkbtnFacilityName" ToolTip="View Spaces" runat="server" CommandName="SpaceInfoPopUp" CausesValidation="false" CommandArgument='<%# Eval("intSpaceId") %>'
                                                                        CssClass="btn btn-white btn-bitbucket"> <i class="fa fa-eye"></i> </asp:LinkButton>

                                                                    <asp:LinkButton ID="imgbtnEditSetting" ToolTip="Edit Row" runat="server" CommandName="Edit" CausesValidation="false" Text="Edit"
                                                                        CommandArgument='<%# Eval("intSpaceId") %>' CssClass="btn btn-white btn-bitbucket"> <i class="fa fa-edit"></i> </asp:LinkButton>

                                                                    <asp:LinkButton ID="imgbtnDeleteSetting" ToolTip="Delete Row" runat="server" CommandName="Delete" CausesValidation="false" Text="Delete"
                                                                        CommandArgument='<%# Eval("intSpaceId") %>' CssClass="btn btn-white btn-bitbucket" OnClientClick="return confirm('Are you sure to delete this Row?');"> 
                                                                    <i class="fa fa-remove"></i> </asp:LinkButton>
                                                                </div>


                                                            </ItemTemplate>

                                                            <EditItemTemplate>
                                                                <cc1:ResourceLinkButton ID="btnUpdateSetting" runat="server" CausesValidation="true"
                                                                    ToolTip="Update Row" ValidationGroup="updateSettingValidation" TextMessage="Update"
                                                                    CommandName="update" CssClass="btn btn-white" ImageUrl="~/Images/add_new_icon2.png" />
                                                                <cc1:ResourceLinkButton ID="btnCancelSetting" runat="server" CausesValidation="false"
                                                                    ToolTip="Cancel Row" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-white"
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
                                            <uc2:Pager ID="pgrRows" runat="server" />
                                        </div>
                                    </div>
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlLane" runat="server" Visible="false">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>
                                    <asp:Label ID="lblCreateNewLane" runat="server" Text="Create New Space"></asp:Label></h5>
                            </div>
                            <div class="ibox-content">
                                <div role="form" class="row search-filter" id="">
                                    <div class="col-md-12">
                                        <dl>
                                            <dt class="inline">
                                                <asp:Label ID="lblfacilityLabelForLane" runat="server" Text=" Facility:"></asp:Label></dt>
                                            <dd class="inline">
                                                <asp:Label ID="lblfacilityForLane" runat="server" CssClass="badge badge-primary"></asp:Label></dd>
                                        </dl>
                                        <dl>
                                            <dt class="inline">
                                                <asp:Label ID="lblLotlabelLane" runat="server" Text="Facility Lot:"></asp:Label></dt>
                                            <dd class="inline">
                                                <asp:Label ID="lblLotNumberLane" runat="server" CssClass="badge badge-primary"></asp:Label></dd>
                                        </dl>
                                        <dl>
                                            <dt class="inline">
                                                <asp:Label ID="lblSpacelabel" runat="server" Text="Row:"></asp:Label></dt>
                                            <dd class="inline">
                                                <asp:Label ID="lblSpaceName" runat="server" CssClass="badge badge-primary"></asp:Label></dd>
                                        </dl>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>
                                            <asp:Label ID="lblCreatingTextDisplayLane" runat="server" Text="Enter Name of Space:"></asp:Label></label>
                                        <asp:TextBox ID="txtLaneheader" runat="server" MaxLength="500" class=" form-control"
                                            OnTextChanged="txtLaneheader_TextChanged" AutoPostBack="true"></asp:TextBox>

                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator2" ValidationGroup="LaneInfo"
                                            CssClass="custom-error" runat="server" ErrorText="Enter Lane Name" ControlToValidate="txtLaneheader"
                                            Display="Dynamic" ErrorMessage="Enter New Space Name"></cc1:ResourceRequiredFieldValidator>
                                        <asp:Label ID="lblErrorLane" runat="server" Visible="false"></asp:Label>
                                    </div>


                                    <div class="form-group col-md-12 mb0">

                                        <asp:LinkButton ID="lblSaveLane" CssClass="btn btn-sm btn-primary" Style="display: none;" OnClientClick="ClearLblErrorLane();" runat="server" Text="Done" CausesValidation="true" ValidationGroup="LaneInfo"
                                            OnClick="lbkSaveLane_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="lblbtnNext" CssClass="btn btn-sm btn-primary" runat="server" OnClientClick="ClearLblErrorLane();" Text="Add" CausesValidation="true" ValidationGroup="LaneInfo"
                                            OnClick="lbkNextLane_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton1" CssClass="btn btn-sm btn-white" runat="server" Text="Back" CausesValidation="false"
                                            OnClick="lbkCancelLane_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div id="dvLaneRecord" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>Space
                                    </h5>
                                </div>
                                <div class="ibox-content">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvLane" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table"
                                            EnableViewState="true" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                            DataKeyNames="intLaneId" EmptyDataText="No data found" OnRowCancelingEdit="gvLane_RowCancelingEdit"
                                            OnRowCommand="gvLane_RowCommand"
                                            OnRowDeleted="gvLane_RowDeleted" OnRowDeleting="gvLane_RowDeleting" OnRowEditing="gvLane_RowEditing"
                                            OnRowUpdated="gvLane_RowUpdated" OnRowUpdating="gvLane_RowUpdating" OnSelectedIndexChanged="gvLane_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Space">
                                                    <ItemTemplate>
                                                        <%# Eval("nvchLaneName")%>
                                                        <asp:HiddenField ID="hdnspacename" runat="server" Value='<%# Eval("nvchLaneName") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtLanes" runat="server" Width="280px" Text='<%# Eval("nvchLaneName") %>'></asp:TextBox>
                                                        <asp:HiddenField ID="hdnspaceid" runat="server" Value='<%# Eval("intLaneId") %>' />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor2" ForeColor="Red" ValidationGroup="updateSettingValidation"
                                                            runat="server" ControlToValidate="txtLanes" ErrorText="*" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="History" Visible="false">
                                                    <ItemTemplate>
                                                        <a href="#">
                                                            <img title="View History" src="/Images/history_icon.png" /></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actions" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div style="width: 75px;">
                                                            <asp:LinkButton ID="lnkbtnFacilityName" ToolTip="View Tires" runat="server" CommandName="LaneInfoPopUp" CommandArgument='<%# Bind("intLaneId") %>'
                                                                CssClass="btn btn-white btn-bitbucket"> <i class="fa fa-eye"></i> </asp:LinkButton>

                                                            <asp:LinkButton ID="imgbtnEditSetting" ToolTip="Edit Space" runat="server" CommandName="Edit" CausesValidation="false" Text="Edit"
                                                                CommandArgument='<%# Bind("intLaneId") %>' CssClass="btn btn-white btn-bitbucket"> <i class="fa fa-edit"></i> </asp:LinkButton>

                                                            <asp:LinkButton ID="imgbtnDeleteSetting" ToolTip="Delete Space" runat="server" CommandName="Delete" CausesValidation="false" Text="Delete"
                                                                CommandArgument='<%# Bind("intLaneId") %>' CssClass="btn btn-white btn-bitbucket" OnClientClick="return confirm('Are you sure to delete this Space?');"> 
                                                                    <i class="fa fa-remove"></i> </asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>

                                                    <EditItemTemplate>
                                                        <cc1:ResourceLinkButton ID="btnUpdateSetting" runat="server" CausesValidation="true"
                                                            ToolTip="Update Space" ValidationGroup="updateSettingValidation" TextMessage="Update"
                                                            CommandName="update" CssClass="btn btn-white" ImageUrl="~/Images/add_new_icon2.png" />
                                                        <cc1:ResourceLinkButton ID="btnCancelSetting" runat="server" CausesValidation="false"
                                                            ToolTip="Cancel Space" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-white"
                                                            ImageUrl="~/Images/delete_icon.png" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <uc2:Pager ID="pgrSpaces" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>




                </div>

            </asp:Panel>

            <div id="dvpopupfacilityinfo" runat="server" class="box_blockCmp" visible="false">
                <div class="popUp_lotInfo">
                    <div class="ajaxModal-popup inmodal">
                        <div class="ajaxModal-body animated bounceInRight" id="dvParkingLot1" runat="server">

                            <uc4:LotRow ID="LotRowControl" runat="server" />
                            <uc5:LotSpace ID="ucFacilitySpaces" runat="server" />
                            <uc6:SpaceTires ID="ucSpaceTires" runat="server" />


                            <asp:HiddenField runat="server" ID="hdnlotname" />
                            <asp:HiddenField runat="server" ID="HiddenField2" />
                            <asp:HiddenField runat="server" ID="HiddenField3" Value="0" />
                            <asp:HiddenField runat="server" ID="HiddenField4" Value="0" />
                            <asp:HiddenField ID="HiddenField5" runat="server" Value='<%# Eval("LotId")%>' />

                            <asp:HiddenField ID="hdnidfaclityid" runat="server" />

                            <div class="modal-footer">
                                <asp:LinkButton ID="lnkCancel1" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Close")%></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <asp:HiddenField runat="server" ID="hndfacilityId" />
            <asp:HiddenField runat="server" ID="hdnLotBarCodeImageFileName" />
            <asp:HiddenField runat="server" ID="hdnBarCodeImageFileName" />
            <asp:HiddenField runat="server" ID="hdnIsPlantCodeValid" Value="0" />
            <asp:HiddenField runat="server" ID="hdnIsSizeCodeValid" Value="0" />
            <asp:HiddenField ID="hidLotId" runat="server" Value='<%# Eval("LotId")%>' />
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>

