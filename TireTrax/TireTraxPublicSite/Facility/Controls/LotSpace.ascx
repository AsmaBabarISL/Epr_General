<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LotSpace.ascx.cs" Inherits="Facility_Controls_LotSpace" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<%@ Register Src="~/Facility/Controls/SpaceTires.ascx" TagName="SpaceTires" TagPrefix="uc6" %>

<script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>

<asp:Panel ID="pnlLane" runat="server" Visible="false">
    <div class="modal-header">
        <h4 class="modal-title">
            <asp:Label ID="lblCreateNewLane" runat="server" Text="Spaces Information"></asp:Label>
        </h4>
        <label class="font-bold">
            <asp:Label ID="lblfacilityLabelForLane" runat="server" Text=" Facility Name:"></asp:Label>
            <asp:Label ID="lblfacilityForLane" runat="server" CssClass=""></asp:Label>
        </label>
        <label class="font-bold block">
            <asp:Label ID="lblSpacelabel" runat="server" Text="Row Name:"></asp:Label>
            <asp:Label ID="lblSpaceName" runat="server" CssClass=""></asp:Label>
        </label>
        <label class="font-bold block">
            <asp:Label ID="lblLotlabelLane" runat="server" Text="Storage Lot Name:"></asp:Label>
            <asp:Label ID="lblLotNumberLane" runat="server" CssClass=""></asp:Label>
        </label>
        </div>
    <asp:Label ID="lblErrorLane" runat="server" Text="" CssClass="custom-error"></asp:Label>
    <div class="modal-body">
        <div id="dvLaneRecord" runat="server">
            <div class="table-responsive">
                <asp:GridView ID="gvLane" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                    EnableViewState="true" DataKeyNames="intLaneId" EmptyDataText="No data found" OnRowCommand="gvLane_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="View Tire" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="lnkbtnspacenamepopup" runat="server" Text='<%# Eval("nvchLaneName")%>' CommandArgument='<%# Eval("intLaneId") %>' ToolTip="View Tire" ImageUrl="/Images/ico_View.png" CommandName="TireInfoBySpaceIdPopUp"></asp:ImageButton>
                                <asp:HiddenField ID="hdnspacename" runat="server" Value='<%# Eval("nvchLaneName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Spaces">
                            <ItemTemplate>
                                <asp:Label ID="lblSpace" runat="server" Text='<%# Eval("nvchLaneName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Tires">
                            <ItemTemplate>
                                <asp:Label ID="lbltotal" runat="server" Text='<%# Eval("TotalTires")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                    <div class="reg_button-outer" style="right: 0px; bottom: 0px;">

                        <asp:LinkButton ID="lnkbtnCancelSpace" CssClass="reg_button" runat="server" Visible="false" Text="Close" OnClick="lnkbtnCancelSpace_Click"></asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnBackSpace" CssClass="reg_button" runat="server" Visible="false" Text="Back" OnClick="lnkbtnBackSpace_Click"></asp:LinkButton>
                    </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdnlotid" runat="server" />
    <asp:HiddenField ID="hdnidfaclityid" runat="server" />
    <asp:HiddenField ID="hdnfacilityname" runat="server" />
    <asp:HiddenField ID="hdnlotname" runat="server" />
    <asp:HiddenField ID="hdnrowname" runat="server" />
    <asp:HiddenField ID="hdnrowIds" runat="server" />
</asp:Panel>
<uc6:SpaceTires ID="ucSpaceTires" runat="server" />


