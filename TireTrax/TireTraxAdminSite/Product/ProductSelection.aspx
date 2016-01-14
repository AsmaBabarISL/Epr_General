<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ProductSelection.aspx.cs" Inherits="Product_ProductSelection" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />

    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chkProducts.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }

        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chkSelectedProducts.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }

        function fadeOut() {
            $(".custom-absolute-alert").delay(3000).fadeOut(300);
            $(".custom-absolute-alert").appendTo("form");
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AddPopupClass);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(AjaxLoader);
    </script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                    <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" ID="upnlAddInventoryForm" UpdateMode="Always">
            <ContentTemplate>
                <!--work to be done for product addition -->
                <div class="row" style="display:none">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Set Products")%> </h5>
                            </div>
                            <div class="ibox-content ">
                                <label>Products: </label>
                                <asp:CheckBoxList ID="chkSelectedProducts" runat="server" class="prod-type-chkbox " RepeatColumns="3" RepeatDirection="Horizontal"
                                    DataTextField="ProductName" DataValueField="ProductId">
                                </asp:CheckBoxList>
                                <cc1:ResourceCustomValidator ID="rfvOrgProducts" runat="server"
                                    ValidationGroup="OrgProducts" ClientValidationFunction="ValidateCheckBoxList"
                                    ErrorText="Please select atleast one product." CssClass="custom-block-error"
                                    Display="Dynamic"></cc1:ResourceCustomValidator>
                                <br />
                                <asp:LinkButton ID="lnkAddProductsForStewardship" runat="server" CssClass="btn btn-primary btn-sm font-bold" CausesValidation="true"
                                    ValidationGroup="OrgProducts" OnClick="lnkAddProductsForStewardship_Click"><%= ResourceMgr.GetMessage("Add")%></asp:LinkButton>
                                <asp:LinkButton ID="lnkCancelForStewardship" runat="server" CssClass="btn btn-white btn-sm font-bold"
                                    OnClick="lnkCancelForStewardship_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Product Selection")%> </h5>
                            </div>
                            <div class="ibox-content ">

                                <div class="">
                                    <label><b>Select your product type: </b></label>
                                    <asp:DropDownList ID="ddlProducts" runat="server" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged" AutoPostBack="True"
                                        Width="15%">
                                    </asp:DropDownList>

                                    <br />



                                    <br />

                                    <asp:ListBox ID="chkProducts" runat="server" CssClass="" Visible="false" Width="35%">
                                    </asp:ListBox><br />
                                    <%--<cc1:ResourceCustomValidator ID="checkselect"
                                        runat="server" ErrorText="Please select atleast one sub product." CssClass="custom-block-error"
                                        Display="Dynamic" ValidationGroup="ProductSelection" ClientValidationFunction="ValidateCheckBoxList"> </cc1:ResourceCustomValidator>--%>
                                    <asp:Label ID="lblError" runat="server" CssClass="custom-block-error" Visible="false"></asp:Label>
                                    <asp:Label ID="lblSuccess" runat="server" CssClass="custom-absolute-alert alert-success" Visible="false"></asp:Label>
                                    <br />


                                    <div class="form-group" id="divButtons" runat="server" visible="false">
                                        <asp:LinkButton ID="lnkAddProduct" runat="server" ValidationGroup="ProductSelection" Visible="false"
                                            CausesValidation="true" CssClass="btn btn-primary btn-sm font-bold" OnClick="lnkAddProduct_Click"><%= ResourceMgr.GetMessage("Save")%></asp:LinkButton>
                                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" OnClick="lnkCancel_Click" Visible="false"
                                            CssClass="btn btn-white btn-sm font-bold"><%= ResourceMgr.GetMessage("Clear")%></asp:LinkButton>
                                        <asp:LinkButton ID="lnkAddMoreProductSubType" runat="server" CssClass="btn btn-primary btn-sm font-bold" OnClick="lnkAddMoreProductSubType_Click"><%= ResourceMgr.GetMessage("Add +")%></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="ibox float-e-margins" id="dvProductProperties" runat="server" visible="false">
                    <div class="ibox-title">
                        <h5><%= ResourceMgr.GetMessage("Set Product Properties")%></h5>
                        <asp:Label ID="lblErrorForSSMB" runat="server" CssClass="custom-block-error col-md-4" Visible="false"></asp:Label>
                        <%-- <div class="ibox-tools">
                    <a class="collapse-link">
                        <i class="fa fa-chevron-up"></i>
                    </a>
                </div>--%>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-12">
                                <div id="dvOnlyForSubType" runat="server" visible="false">
                                    <label><b>Select your product sub type: </b></label>
                                    <asp:DropDownList ID="ddlSubType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged" Width="15%"></asp:DropDownList>

                                    <asp:Label ID="lblErrorOnlyForSubType" runat="server" CssClass="custom-block-error" Visible="false"></asp:Label>

                                </div>
                                <br />
                                <div id="divSizeShapeMaterialBrand" runat="server" visible="false">
                                    <div class="col-md-3">
                                        <label>Size</label>
                                        <asp:ListBox ID="lstSize" wrap='on' SelectionMode="Single" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstSize_SelectedIndexChanged"></asp:ListBox><br />
                                        <label>Name</label>
                                        <asp:TextBox ID="txtSize" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtSize" ValidationGroup="Size" Display="Dynamic" ErrorMessage="Please enter the name of size" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>
                                        <br />
                                        <%--<label>Description</label>
                                <asp:TextBox ID="txtSizeDescription" runat="server" CssClass="form-control"></asp:TextBox><br />--%>
                                        <asp:LinkButton ID="lnkAddSize" runat="server" class="btn btn-sm btn-primary font-bold" ValidationGroup="Size" OnClick="lnkAddSize_Click"><%= ResourceMgr.GetMessage("Add")%></asp:LinkButton>
                                        <div id="dvSizeUpdateButtons" runat="server" visible="false">
                                            <asp:LinkButton ID="lnkUpdateSize" runat="server" class="btn btn-sm btn-primary font-bold" ValidationGroup="Size" OnClick="lnkUpdateSize_Click"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>
                                            <asp:LinkButton ID="lnkCancelSize" runat="server" class="btn btn-sm btn-white font-bold" OnClick="lnkCancelSize_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                        </div>
                                    </div>


                                    <div class="col-md-3" style="border-left: 1px solid black">
                                        <label>Shape</label>
                                        <asp:ListBox ID="lstShape" wrap='on' SelectionMode="Single" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstShape_SelectedIndexChanged"></asp:ListBox><br />
                                        <label>Name</label>
                                        <asp:TextBox ID="txtShape" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtShape" ValidationGroup="Shape" Display="Dynamic" ErrorMessage="Please enter the name of shape" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>
                                        <br />
                                        <%--<label>Description</label>
                                <asp:TextBox ID="txtShapeDescription" runat="server" CssClass="form-control"></asp:TextBox><br />--%>
                                        <asp:LinkButton ID="lnkAddShape" runat="server" class="btn btn-sm btn-primary font-bold" ValidationGroup="Shape" OnClick="lnkAddShape_Click"><%= ResourceMgr.GetMessage("Add")%></asp:LinkButton>
                                        <div id="dvShapeUpdateButtons" runat="server" visible="false">
                                            <asp:LinkButton ID="lnkUpdateShape" runat="server" class="btn btn-sm btn-primary font-bold" ValidationGroup="Shape" OnClick="lnkUpdateShape_Click"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>
                                            <asp:LinkButton ID="lnkCancelShape" runat="server" class="btn btn-sm btn-white font-bold" OnClick="lnkCancelShape_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                        </div>
                                    </div>


                                    <div class="col-md-3" style="border-left: 1px solid black">
                                        <label>Material</label>
                                        <asp:ListBox ID="lstMaterial" wrap='on' SelectionMode="Single" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstMaterial_SelectedIndexChanged"></asp:ListBox><br />
                                        <label>Name</label>
                                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtMaterial" ValidationGroup="Material" Display="Dynamic" ErrorMessage="Please enter the name of material" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>
                                        <br />
                                        <%--<label>Description</label>
                                <asp:TextBox ID="txtMaterialDescription" runat="server" CssClass="form-control"></asp:TextBox><br />--%>
                                        <asp:LinkButton ID="lnkAddMaterial" runat="server" class="btn btn-sm btn-primary font-bold" ValidationGroup="Material" OnClick="lnkAddMaterial_Click"><%= ResourceMgr.GetMessage("Add")%></asp:LinkButton>
                                        <div id="dvMaterialUpdateButtons" runat="server" visible="false">
                                            <asp:LinkButton ID="lnkUpdateMaterial" runat="server" class="btn btn-sm btn-primary font-bold" ValidationGroup="Material" OnClick="lnkUpdateMaterial_Click"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>
                                            <asp:LinkButton ID="lnkCancelMaterial" runat="server" class="btn btn-sm btn-white font-bold" OnClick="lnkCancelMaterial_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                        </div>
                                    </div>


                                    <div class="col-md-3" style="border-left: 1px solid black">
                                        <label>Brand</label>
                                        <asp:ListBox ID="lstBrand" wrap='on' SelectionMode="Single" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstBrand_SelectedIndexChanged"></asp:ListBox><br />
                                        <label>Name</label>
                                        <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtBrand" ValidationGroup="Brand" Display="Dynamic" ErrorMessage="Please enter the name of brand" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>
                                        <br />
                                        <%--<label>Description</label>
                                <asp:TextBox ID="txtBrandDescription" runat="server" CssClass="form-control"></asp:TextBox><br />--%>
                                        <asp:LinkButton ID="lnkAddBrand" runat="server" class="btn btn-sm btn-primary font-bold" ValidationGroup="Brand" OnClick="lnkAddBrand_Click"><%= ResourceMgr.GetMessage("Add")%></asp:LinkButton>
                                        <div id="dvBrandUpdateButtons" runat="server" visible="false">
                                            <asp:LinkButton ID="lnkUpdateBrand" runat="server" class="btn btn-sm btn-primary font-bold" ValidationGroup="Brand" OnClick="lnkUpdateBrand_Click"><%= ResourceMgr.GetMessage("Update")%></asp:LinkButton>
                                            <asp:LinkButton ID="lnkCancelBrand" runat="server" class="btn btn-sm btn-white font-bold" OnClick="lnkCancelBrand_Click"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <div class="ajaxModal-popup inmodal" id="dvAddProductSubType" runat="server" visible="false">
                    <div class="ajaxModal-body animated bounceInRight">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <%= ResourceMgr.GetMessage("Add Product Sub Type")%>
                            </h4>
                        </div>

                        <div class="modal-body">
                            <div role="form" class="row search-filter" id="">
                                <div class="form-group col-md-6">
                                    <asp:Label ID="lblErrorPopup" runat="server" CssClass="custom-block-error" Visible="false"></asp:Label>
                                    <br />
                                    Product Sub Type Name:
                            <asp:TextBox ID="txtSubType" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="txtSubType" ValidationGroup="Required"
                                        ErrorMessage="Please provide Sub Type Name" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>
                                    <br />
                                    <br />
                                    Product Sub Type Description:
                            <asp:TextBox ID="txtSubDescription" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="rfv7" runat="server" ControlToValidate="txtSubDescription" ValidationGroup="Required"
                                        ErrorMessage="Please provide Sub Type Description" CssClass="custom-block-error"></cc1:ResourceRequiredFieldValidator>
                                    <br />
                                    <br />
                                    <asp:LinkButton ID="lnkAdd" runat="server" OnClick="lnkAdd_Click" CssClass="btn btn-primary btn-sm font-bold" CausesValidation="true"
                                        ValidationGroup="Required"><%= ResourceMgr.GetMessage("Add")%></asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_Click" CssClass="btn btn-white btn-sm font-bold"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
