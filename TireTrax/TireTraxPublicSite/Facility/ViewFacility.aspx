<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="ViewFacility.aspx.cs" Inherits="Inventory_Facility_ViewFacility" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<%@ Register Src="~/Facility/Controls/FacilityLots.ascx" TagName="FacilityLot" TagPrefix="uc3" %>
<%@ Register Src="~/Facility/Controls/LotRows.ascx" TagName="LotRow" TagPrefix="uc4" %>
<%@ Register Src="~/Facility/Controls/LotSpace.ascx" TagName="LotSpace" TagPrefix="uc5" %>  


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
<script type="text/javascript">

    function ClearSearchFields() {

        $("#<%=txtFaciliyNameForSearch.ClientID%>").val('');
        $("#<%=lnkbtnSearch.ClientID%>")[0].click();

    }

    function ClearErrorFileds() {
        if ($("#<%=txtFacilityName.ClientID%>").val() == '') {
            $("#<%=lblErrorMessage.ClientID%>").text('');
            $("#<%=lblFacilityNameError.ClientID%>").text('');
            
        }
        else {
           
        }
        }



         function ClearErrorFiledsForSearch() {


             if ($("#<%=txtFacilityName.ClientID%>").val() == '') {
                 $("#<%=lblErrorMessage.ClientID%>").text('');
                 $("#<%=lblFacilityNameError.ClientID%>").text('');
                 
             }
             else {

             }

         }

         var specialKeys = new Array();
         specialKeys.push(8); //Backspace
         specialKeys.push(9); //Tab
         specialKeys.push(46); //Delete
         specialKeys.push(36); //Home
         specialKeys.push(35); //End
         specialKeys.push(37); //Left
         specialKeys.push(39); //Right
         function IsAlphaNumeric1(e) {
             var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
             var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));

             return ret;
         }


         function AddPopupClass() {
             $(".ajaxModal-popup").appendTo("form");
             $(".ajax-loader").remove();
         }

         function AjaxLoader() {
             $(".ajax-loader").appendTo("form");
         }
         function facilityVisible() {
             $("#facility-popup").css("visibility", "visible");
             $(".ajaxModal-popup").appendTo("form");
         }

       $(function () {
             $("#<%=lblFacilityNameError.ClientID%>").delay(3000).fadeOut(300);
             $("#<%=lblErrorMessage.ClientID%>").delay(3000).fadeOut(300);
         });
    
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript">
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
         Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);

    </script>
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    <div class="ajax-loader" id="Div1"  runat="server" style=" background:url(/images/bg_shadow.png) repeat;width:100%;height:100%;position:fixed;
    z-index:999;top:0;left:0;z-index:99999;display:block;"> 
           <img src="/images/ajax-loader.gif" style="position:fixed; left:0; right:0; top:0; bottom:0; margin:auto;" />
            </div>
    </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlFacility" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Search Facility")%> </h5>
                            </div>
                            <div class="ibox-content">
                                <div role="form" class="row search-filter" id="">
                                    <asp:Label ID="Label2" runat="server" Text="" CssClass="custom-error"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text="Search Facility" Visible="False"></asp:Label>
                                    <div class="form-group col-md-4 col-lg-3">
                                        <label>
                                            <asp:Label ID="Label3" runat="server" Text="Enter Facility Name:"></asp:Label></label>
                                        <asp:TextBox ID="txtFaciliyNameForSearch" runat="server" MaxLength="100" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-12 mb0">
                                        <asp:LinkButton ID="lnkbtnSearch" CssClass="btn btn-sm btn-primary font-bold" runat="server" Text="Search" CausesValidation="false" OnClick="lnkbtnSearchFacility_Click">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnSearchClear" CssClass="btn btn-sm btn-white font-bold" runat="server" Text="Clear" CausesValidation="false" OnClick="lnkbtnClearSearch_Click">
                                        </asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Facility")%></h5>
                                <div class="ibox-tools">
                                    <div class="form-group">
                                        <a class="ico_view btn btn-sm btn-primary font-bold" onclick="facilityVisible();" id="aAdd" runat="server" href="javascript:void();">
                                            <i class="fa fa-plus"></i>
                                            <%= ResourceMgr.GetMessage("Add Facility")%>
                                        </a>
                                        <a class="ico_view btn btn-sm btn-primary font-bold" href="lots">
                                            <i class="fa fa-list"></i>
                                            <%= ResourceMgr.GetMessage("Detail Facility")%>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <div id="dvFacilityRecord" runat="server">
                                                <asp:GridView ID="gvFacility" runat="server" AutoGenerateColumns="False" RowStyle-Wrap="true" CssClass="table table-bordered epr-sec-table" 
                                                    EnableViewState="true" DataKeyNames="intFacilityID,vchFacilityName" EmptyDataText="No data found" OnRowCancelingEdit="gvFacility_RowCancelingEdit"
                                                    OnRowCommand="gvFacility_RowCommand" OnRowEditing="gvFacility_RowEditing" OnRowUpdating="gvFacility_RowUpdating" EmptyDataRowStyle-CssClass="alert alert-danger text-center">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Facility Name">

                                                            <ItemTemplate>

                                                                <%# Eval("vchFacilityName") %>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtFacilityName" runat="server" Text='<%# Eval("vchFacilityName") %>'></asp:TextBox>
                                                                <asp:HiddenField ID="hdnFacilityID" runat="server" Value='<%# Eval("intFacilityID") %>' />
                                                                <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor2" ForeColor="Red" ValidationGroup="updateSettingValidation"
                                                                    runat="server" ControlToValidate="txtFacilityName" ErrorText="*" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                            </EditItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Date Created")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <%# Convert.ToDateTime(Eval("dtmDateCreated")).ToString("MM/dd/yyyy")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Add New Storage Lots" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="imgaddlot" runat="server" CausesValidation="false"
                                                                    ToolTip="Add New Storage Lot" CommandName="AddLot"
                                                                    CommandArgument='<%# Bind("intFacilityID") %>' CssClass="btn btn-white btn-bitbucket"> 
                                                                            <i class="fa fa-plus"></i> </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="60" ItemStyle-Wrap="false">
                                                            <ItemStyle/>
                                                            <ItemTemplate >
                                                                <div style="width: 84px;">
                                                                <asp:LinkButton ID="lnkbtnFacilityName" runat="server"
                                                                    ToolTip="View Lot" CommandName="FacilityInfoPopUp"
                                                                    CommandArgument='<%# Bind("intFacilityID") %>' CssClass="btn btn-white btn-bitbucket"> 
                                                                            <i class="fa fa-eye"></i> </asp:LinkButton>

                                                                <asp:LinkButton ID="imgbtnEditSetting" runat="server" CausesValidation="false"
                                                                    ToolTip="Edit Facility" CommandName="Edit" Text="Edit"
                                                                    CommandArgument='<%# Bind("intFacilityID") %>' CssClass="btn btn-white btn-bitbucket"> 
                                                                            <i class="fa fa-edit"></i> </asp:LinkButton>
                                                                <asp:ImageButton ID="imgbtnDeleteSetting" runat="server" CausesValidation="false" Visible="false"
                                                                    ToolTip="Deactive Facility" Text="Delete" CommandArgument='<%# Eval("intFacilityID") %>' ImageUrl="~/Images/delete_icon.png"
                                                                    Style="position: relative; top: 3px;" OnClientClick="return confirm('Are you sure you want to delete facility?');"
                                                                    CommandName="Delete" />

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <cc1:ResourceLinkButton ID="lnkbtnAddSetting" runat="server" CommandName="Insert"
                                                                    ToolTip="Add Storage Lot" CausesValidation="true" TextMessage="Add" CssClass="btn btn-white"
                                                                    Visible="false" ValidationGroup="InsertSettingValidation" ImageUrl="~/Images/add_icon.png"></cc1:ResourceLinkButton>
                                                                <cc1:ResourceLinkButton ID="lnkbtnCancelSetting" runat="server" CommandName="CancelSetting"
                                                                    ToolTip="Cancel Lot" CausesValidation="false" TextMessage="Cancel" CssClass="btn btn-white"
                                                                    Visible="false" ValidationGroup="InsertSettingValidation" ImageUrl="~/Images/delete_icon.png"></cc1:ResourceLinkButton>
                                                                <cc1:ResourceLinkButton ID="lnkbtnAddMore" TextMessage="Add More" runat="server"
                                                                    ToolTip="Add More Lot" CssClass="btnAddUpdateSettings" CommandName="AddMore"
                                                                    ImageUrl="~/Images/add_icon.png"></cc1:ResourceLinkButton>
                                                            </FooterTemplate>
                                                            <EditItemTemplate>
                                                                <cc1:ResourceLinkButton ID="btnUpdateSetting" runat="server" CausesValidation="true"
                                                                    ToolTip="Update Facility" ValidationGroup="updateSettingValidation" TextMessage="Update"
                                                                    CommandName="update" CssClass="btn btn-white" ImageUrl="~/Images/add_new_icon2.png" />
                                                                <cc1:ResourceLinkButton ID="btnCancelSetting" runat="server" CausesValidation="false"
                                                                    ToolTip="Cancel Facility" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-white"
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
                                            <uc2:Pager ID="pgrFacility" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

          
            <asp:Label ID="lblErrorMessage" runat="server" Text="" CssClass="alert-success custom-absolute-alert" Visible="false"></asp:Label>
            <asp:Label ID="lblFacilityNameError" runat="server" CssClass="alert-danger custom-absolute-alert" Text="" Visible="false"></asp:Label>

            <div id="dvpopupfacilityinfo" runat="server" class="" visible="false">
                <div class="ajaxModal-popup inmodal">
                    <div class="ajaxModal-body animated bounceInRight" id="dvParkingLot1" runat="server">
                        <uc3:FacilityLot ID="ucFacilityLots" runat="server"></uc3:FacilityLot>
                        <uc4:LotRow ID="LotRowControl" runat="server" />
                        <uc5:LotSpace ID="ucFacilitySpaces" runat="server" />



                        <asp:HiddenField runat="server" ID="hdnLotBarCodeImageFileName" />
                        <asp:HiddenField runat="server" ID="hdnBarCodeImageFileName" />
                        <asp:HiddenField runat="server" ID="hdnIsPlantCodeValid" Value="0" />
                        <asp:HiddenField runat="server" ID="hdnIsSizeCodeValid" Value="0" />
                        <asp:HiddenField ID="hidLotId" runat="server" Value='<%# Eval("LotId")%>' />

                        <asp:HiddenField ID="hdnidfaclityid" runat="server" />
                        <div class="modal-footer">
                            <asp:LinkButton ID="lnkCancel1" CssClass="btn btn-white btn-sm" OnClick="lnkCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Close")%></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
      <div class="ajaxModal-popup inmodal" id="facility-popup" style="visibility: hidden;">
                <div class="ajaxModal-body animated bounceInRight">
                    <div class="modal-header">
                            <h4 class="modal-title"><%= ResourceMgr.GetMessage("Add Facility")%></h4>
                        </div>
                        <div class="modal-body">
                            <div role="form" class="row search-filter" id="">
                                <div class="form-group col-md-12 mb0">
                                    <label>
                                        <asp:Label ID="lblCreatingTextDisplay" runat="server" Text="Enter New Facility Name:"></asp:Label></label>
                                    <asp:TextBox ID="txtFacilityName" runat="server" MaxLength="100" class="form-control" onkeypress="return IsAlphaNumeric1(event);"></asp:TextBox>

                                    <cc1:ResourceRequiredFieldValidator ID="rfvAcount" ValidationGroup="Facility"
                                        CssClass="custom-error" runat="server" ErrorText="Enter Facility" ControlToValidate="txtFacilityName"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="lbkSave" CssClass="btn btn-primary btn-sm" runat="server"
                                Text="Add" CausesValidation="true" ValidationGroup="Facility"
                                OnClick="lbkSaveFacility_Click" OnClientClick="ClearErrorFileds();"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnClear" CssClass="btn btn-white btn-sm" runat="server"
                                Text="Close" CausesValidation="false" OnClick="lnkbtnClear_Click"></asp:LinkButton>
                        </div>
                </div>
            </div>


</asp:Content>

