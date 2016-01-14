<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FacilityLots.ascx.cs" Inherits="Facility_Controls_FacilityLots" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<%@ Register Src="~/Facility/Controls/LotRows.ascx" TagName="LotRow" TagPrefix="uc4" %>
<script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>

<asp:Panel ID="pnlLot" runat="server" Visible="false">

    <div class="modal-header">
        <h4 class="modal-title">
            <asp:Label ID="lblCreateNewLot" runat="server" Text="Storage Lots Information"></asp:Label>
        </h4>
        <label class="font-bold">
            <asp:Label ID="lblfacilitynamelbl" runat="server" Text=" Facility Name:"></asp:Label>
            <asp:Label ID="lblfacilityname" runat="server" CssClass=""></asp:Label>
        </label>
        <asp:Label ID="lblErrorLot" runat="server" Text="" CssClass="cutom-error"></asp:Label>
    </div>

    <div id="dvLotRecord" runat="server">
        <div class="modal-body">
            <div class="table-responsive">

                <asp:GridView ID="gvLot" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                    CssClass="table table-bordered epr-sec-table"
                    EnableViewState="true" DataKeyNames="LotId,LotNumber" EmptyDataText="No data found"
                    OnRowCommand="gvLot_RowCommand">

                    <Columns>
                        <asp:TemplateField HeaderText="View Storage Lot" Visible="false">
                            <HeaderStyle />
                            <ItemStyle />
                            <ItemTemplate>

                                <asp:ImageButton ID="lnkbtnlotinfopop" runat="server" ToolTip="View Storage Lot"
                                     ImageUrl="/Images/ico_View.png" CommandArgument='<%# Eval("LotId") %>' CommandName="RowInfobyLotIdPopUp"></asp:ImageButton>

                                <asp:HiddenField ID="hdGVLotNumber" runat="server" Value='<%# Eval("LotNumber") %>' />
                            </ItemTemplate>


                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Storage Lot Name">
                            <HeaderStyle />
                            <ItemStyle />
                            <ItemTemplate>
                                <asp:Label ID="lbl" runat="server" Text='<%# Eval("LotNumber")%>'></asp:Label>

                            </ItemTemplate>


                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Total Rows")%>
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



                    </Columns>
                </asp:GridView>
            </div>
            <div class="row text-right">
                <div class="col-md-12">
                    <uc2:Pager ID="pgrLots" runat="server" />
                </div>
            </div>
        </div>
    </div>





    <asp:HiddenField runat="server" ID="hdnLotBarCodeImageFileName" />
    <asp:HiddenField runat="server" ID="hdnBarCodeImageFileName" />
    <asp:HiddenField runat="server" ID="hdnIsPlantCodeValid" Value="0" />
    <asp:HiddenField runat="server" ID="hdnIsSizeCodeValid" Value="0" />
    <asp:HiddenField ID="hidLotId" runat="server" Value='<%# Eval("LotId")%>' />


    <asp:HiddenField ID="hdnidfaclityid" runat="server" />
    <asp:HiddenField ID="hdnfacilityname" runat="server" />



</asp:Panel>
<uc4:LotRow ID="LotRowControl" runat="server"></uc4:LotRow>

<%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
<%-- </div>
                </div>--%>