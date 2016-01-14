<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpaceTires.ascx.cs" Inherits="Facility_Controls_SpaceTires" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>





<asp:Panel ID="pnlTires" runat="server" Visible="false">
    <div class="modal-header">
        <h4 class="modal-title"> <asp:Label ID="lblCreateNewLane" runat="server" Text="Tires Information"></asp:Label> </h4>
        <label class="font-bold">
            <asp:Label ID="lblfacilityLabelForLane" runat="server" Text=" Facility Name:"></asp:Label>
            <asp:Label ID="lblfacilityForLane" runat="server" CssClass=""></asp:Label>
        </label>
        <label class="font-bold block">
            <asp:Label ID="lblLotlabelLane" runat="server" Text="Storage Lot Name:"></asp:Label>
            <asp:Label ID="lblLotNumberLane" runat="server" CssClass=""></asp:Label>
        </label>
        <label class="font-bold block">
            <asp:Label ID="lblSpacelabel" runat="server" Text="Row Name:"></asp:Label>
            <asp:Label ID="lblSpaceName" runat="server" CssClass=""></asp:Label>
        </label>
        <label class="font-bold block">
            <asp:Label ID="lblLanelabel" runat="server" Text="Space Number:" ></asp:Label>
            <asp:Label ID="lblLaneName" runat="server" CssClass=""></asp:Label>
        </label>
    </div>
    <asp:Label ID="lblErrorLane" runat="server" Text="" CssClass="custom-error"></asp:Label>
    <div class="modal-body">
        <div id="dvTireRecord" runat="server">
            <div class="table-responsive">

                <asp:GridView ID="gvTires" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table"
                    EnableViewState="true" EmptyDataText="No data found">

                    <Columns>
                        <asp:TemplateField HeaderText="Dot Number">
                            <ItemTemplate>
                                <%# Eval("DOTNumber")%>
                            </ItemTemplate>


                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size Number">
                            <ItemTemplate>
                                <%# Eval("SizeNumber")%>
                            </ItemTemplate>


                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Plant Number">
                            <ItemTemplate>
                                <%# Eval("PlantNumber")%>
                            </ItemTemplate>

                        </asp:TemplateField>



                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
     
                    
                    
<div class="reg_button-outer" style="right: 0px; bottom: 0px;">

                        <asp:LinkButton ID="lnkbtnCancelTire" CssClass="reg_button" runat="server" Visible="false"
                            Text="Close"
                            OnClick="lnkbtnCancelTire_Click"></asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnBackTire" CssClass="reg_button" runat="server" Visible="false"
                            Text="Back"
                            OnClick="lnkbtnBackTire_Click"></asp:LinkButton>
                    </div>
               
                


    <asp:HiddenField ID="hdnlotid" runat="server" />
    <asp:HiddenField ID="hdnspacename" runat="server" />
    <asp:HiddenField ID="hdnrowname" runat="server" />
    <asp:HiddenField ID="hndspaceId" runat="server" />

    <asp:HiddenField ID="hdnidfaclityid" runat="server" />
    <asp:HiddenField ID="hdnrowIds" runat="server" />
    <asp:HiddenField ID="hdnfacilityname" runat="server" />
    <asp:HiddenField ID="hdnlotname" runat="server" />
</asp:Panel>
