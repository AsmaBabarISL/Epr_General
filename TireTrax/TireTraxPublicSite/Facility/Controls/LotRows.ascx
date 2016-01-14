<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LotRows.ascx.cs" Inherits="Facility_Controls_LotRows" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>

<%@ Register Src="~/Facility/Controls/LotSpace.ascx" TagName="LotSpace" TagPrefix="uc5" %>


<script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
<script type="text/javascript">
 


  

</script>


<div id="pnlSpace" runat="server" visible="false">
    <div class="modal-header">
        <h4 class="modal-title">
            <asp:Label ID="lblCreateNewSpace" runat="server" Text="Rows Information"></asp:Label></h4>
        <label class="font-bold">
            <asp:Label ID="lblFacilityLabelForSpace" runat="server" Text="Facility Name:"></asp:Label>
            <asp:Label ID="lblFacilityForSpace" runat="server" CssClass=""></asp:Label>
        </label>
        
        <label class="font-bold block">
            <asp:Label ID="lblLotlabelSpace" runat="server" Text=" Storage Lot Name:"></asp:Label>
            <asp:Label ID="lblLotNumberSpace" runat="server" CssClass=""></asp:Label>
        </label>
        <asp:Label ID="lblErrorSpace" runat="server" Text="" CssClass="custom-error"></asp:Label>
    </div>

    <div class="modal-body">
        <div id="dvSpaceRecord" runat="server">
            <div class="table-responsive">
                <asp:GridView ID="gvSetting" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                    CssClass="table table-bordered epr-sec-table"
                    EnableViewState="true" DataKeyNames="intSpaceId"
                    EmptyDataText="No data found"
                    OnRowCommand="gvSetting_RowCommand">

                    <Columns>
                        <asp:TemplateField HeaderText="View Row" Visible="false">
                            <HeaderStyle />
                            <ItemStyle />
                            <ItemTemplate>

                                <asp:ImageButton ID="lnkbtnspacenamepopup" runat="server" ToolTip="View Row" ImageUrl="/Images/ico_View.png" CommandArgument='<%# Eval("intSpaceId") %>' CommandName="LaneInfoBySpaceIdPopUp"></asp:ImageButton>

                                <asp:HiddenField ID="hdnRowName" runat="server" Value='<%# Eval("vchSpaceName") %>' />
                            </ItemTemplate>


                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rows Name">
                            <HeaderStyle />
                            <ItemStyle />
                            <ItemTemplate>


                                <asp:Label ID="lbl" runat="server" Text='<%# Eval("vchSpaceName") %>'></asp:Label>

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
                        <asp:TemplateField HeaderText="History" Visible="false">
                            <ItemTemplate>
                                <a href="#">
                                    <img title="View History" src="/Images/history_icon.png" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


            </div>
        </div>
    </div>
    
    <div class="row text-right">
        <div class="col-lg-12">

            <asp:LinkButton ID="lnkbtnCancelRows" CssClass="btn btn-sm btn-primary" runat="server" Visible="false"
                Text="Close"
                OnClick="lbkCancelSpace_Click"></asp:LinkButton>
            <asp:LinkButton ID="lnkbtnBackRows" CssClass="btn btn-sm btn-white" runat="server" Visible="false"
                Text="Back"
                OnClick="lnkbtnBackRows_Click"></asp:LinkButton>
        </div>
    </div>


    <asp:HiddenField runat="server" ID="hdnLotBarCodeImageFileName" />
    <asp:HiddenField runat="server" ID="hdnBarCodeImageFileName" />
    <asp:HiddenField runat="server" ID="hdnIsPlantCodeValid" Value="0" />
    <asp:HiddenField runat="server" ID="hdnIsSizeCodeValid" Value="0" />
    <asp:HiddenField ID="hidLotId" runat="server" />

    <asp:HiddenField ID="hdnidfaclityid" runat="server" />
    <asp:HiddenField ID="hdnfacilityname" runat="server" />
    <asp:HiddenField ID="hdnlotname" runat="server" />


</div>
<uc5:LotSpace ID="lotspace" runat="server" />






