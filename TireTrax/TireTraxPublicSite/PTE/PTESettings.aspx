﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="PTESettings.aspx.cs" Inherits="PTE_PTESettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />
    <script type="text/javascript">
        function SetDatePicket() {
            $(".datepicker").datepicker();
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
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%= ResourceMgr.GetMessage("PTE")%></h5>

                </div>
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="row">
                                <asp:Label ID="lblErrorMessage" runat="server" Text="" CssClass="custom-error"></asp:Label>
                                <div class="col-md-12">
                                    <div class="table-responsive">

                                        <asp:HiddenField ID="hdnPTEId" runat="server" />
                                        <div class="row form-group col-md-4 col-lg-4">
                                            <label><%= ResourceMgr.GetMessage("Select Product")%></label>
                                            <asp:DropDownList ID="ddlOrganizationProducts" runat="server" DataTextField="ProductName" DataValueField="ProductId"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlOrganizationProducts_SelectedIndexChanged" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div id="dvSubType" runat="server" visible="false" class="form-group col-md-4 col-lg-4">
                                            <label><%= ResourceMgr.GetMessage("Select Sub Type")%></label>
                                            <asp:DropDownList ID="ddlProductSubTypes" runat="server" AutoPostBack="true" DataTextField="SubProductName" DataValueField="SubProductId"
                                                OnSelectedIndexChanged="ddlProductSubTypes_SelectedIndexChanged" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>

                                        <asp:GridView ID="gvSetting" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered epr-sec-table" EnableViewState="true" DataKeyNames="PTEId"
                                            EmptyDataText="No data found" OnDataBound="gvSetting_DataBound" OnRowDataBound="gvSetting_RowDataBound"
                                            OnRowEditing="gvSetting_RowEditing" OnRowCommand="gvSetting_RowCommand" OnRowUpdated="gvSetting_RowUpdated"
                                            OnRowUpdating="gvSetting_RowUpdating" OnSelectedIndexChanged="gvSetting_SelectedIndexChanged"
                                            OnRowCancelingEdit="gvSetting_RowCancelingEdit" OnRowDeleted="gvSetting_RowDeleted"
                                            OnRowDeleting="gvSetting_RowDeleting" ShowFooter="true" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Organization Type")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStakeholderType" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlstakeholderTypeListeditor" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnOrganizationSubTypeId" Value='<%# Eval("OrganizationSubTypeId") %>'
                                                            runat="server" />
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rsvldreqddlstakeholderTypeListeditor" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlstakeholderTypeListeditor" ForeColor="Red"
                                                            ValidationGroup="updateSettingValidation" ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlstakeholderTypeListfooter" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rsvldddlstakeholderTypeListfooter" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlstakeholderTypeListfooter" ForeColor="Red"
                                                            ValidationGroup="InsertSettingValidation" ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Product Size")%>
                                                    </HeaderTemplate>


                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTireSize" runat="server" Text='<%# Eval("ProductSize") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlTireSizeListeditor" runat="server" Height="20px" Width="133px">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnSizeId" Value='<%# Eval("SizeId") %>' runat="server" />
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlTireSizeListeditor" ForeColor="Red" ValidationGroup="updateSettingValidation"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlTireSizeListfooter" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator111" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlTireSizeListfooter" ForeColor="Red" ValidationGroup="InsertSettingValidation"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Effective Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEffactiveDate" runat="server" Text='<%# Convert.ToDateTime(Eval("EffectiveDate")).ToString("MM/dd/yyyy") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txteffectivedateeditor" runat="server" CssClass="datepicker" Text='<%# Convert.ToDateTime(Eval("EffectiveDate")).ToString("MM/dd/yyyy") %>'></asp:TextBox>
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditoasdasr2" ForeColor="Red" ValidationGroup="updateSettingValidation"
                                                            runat="server" ControlToValidate="txteffectivedateeditor" ErrorText="Enter Effective Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txteffectivedatefooter" runat="server" CssClass="datepicker" Visible="false">
                                                        </asp:TextBox>
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor2" ValidationGroup="InsertSettingValidation" ForeColor="Red"
                                                            runat="server" ControlToValidate="txteffectivedatefooter" ErrorText="Enter Effective Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Expiration Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpirationDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpirationDate")).ToString("MM/dd/yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtexpirationdateeditor" runat="server" CssClass="datepicker" Text='<%# Convert.ToDateTime(Eval("ExpirationDate")).ToString("MM/dd/yyyy") %>'><%--  CssClass="datepicker"--%></asp:TextBox><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditordsad1" ForeColor="Red"
                                                            ValidationGroup="updateSettingValidation"
                                                            runat="server" ControlToValidate="txtexpirationdateeditor" ErrorText="Enter Expiration Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtexpirationdatefooter" runat="server" CssClass="datepicker" Visible="false">
       
                                                        </asp:TextBox><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor1" ForeColor="Red" ValidationGroup="InsertSettingValidation"
                                                            runat="server" ControlToValidate="txtexpirationdatefooter" ErrorText="Enter Expiration Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Amount")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDollarValue" runat="server" Text='<%# Convert.ToDecimal(Eval("DollarValue")).ToString("C") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDollarValueeditor" runat="server" Text='<%# Eval("DollarValue") %>' MaxLength="4">
                                                        </asp:TextBox><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor" ForeColor="Red" ValidationGroup="updateSettingValidation"
                                                            runat="server" ControlToValidate="txtDollarValueeditor" ErrorText="Enter Dollar Value"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        <cc1:ResourceRegularExpressionValidator ID="revDollarValueeditor" ForeColor="Red"
                                                            ValidationGroup="updateSettingValidation" runat="server" ControlToValidate="txtDollarValueeditor"
                                                            ValidationExpression="^[0-9\.]*$" ErrorText="Enter Numeric Only." Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtDollarValuefooter" runat="server" Visible="false" MaxLength="4">
                                                        </asp:TextBox><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValuefooter" ForeColor="Red" ValidationGroup="InsertSettingValidation"
                                                            runat="server" ControlToValidate="txtDollarValuefooter" ErrorMessage="Enter Dollar Value"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        <cc1:ResourceRegularExpressionValidator ID="revDollarValue" ForeColor="Red" ValidationGroup="InsertSettingValidation"
                                                            runat="server" ControlToValidate="txtDollarValuefooter" ValidationExpression="^[0-9\.]*$"
                                                            ErrorText="Enter Numeric Only." Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("History") %>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <a href="#">

                                                            <img title="View History" src="/Images/history_icon.png" /></a>

                                                    </ItemTemplate>


                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="60" ItemStyle-Wrap="false" FooterStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgbtnEditSetting" ToolTip="Edit PTE" CausesValidation="false" runat="server" CommandName="Edit"
                                                            CommandArgument='<%# Eval("PTEId") %>' Text="Edit"
                                                            CssClass="btn btn-white btn-bitbucket"> <i class="fa fa-edit"></i> </asp:LinkButton>

                                                        <asp:LinkButton ID="imgbtnDeleteSetting" ToolTip="Delete PTE" runat="server" CommandName="Delete" CausesValidation="false" Text="Delete"
                                                            CommandArgument='<%# Eval("PTEId") %>' CssClass="btn btn-white btn-bitbucket" OnClientClick="return confirm('Are you sure you want to delete PTE?');"> <i class="fa fa-remove"></i> </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <FooterTemplate>

                                                        <cc1:ResourceLinkButton ID="lnkbtnAddSetting" runat="server" CommandName="Insert"
                                                            ToolTip="Add PTE" CausesValidation="true" TextMessage="Add" CssClass="btn btn-primary" ForeColor="White"
                                                            Visible="false" ValidationGroup="InsertSettingValidation"></cc1:ResourceLinkButton>
                                                        <cc1:ResourceLinkButton ID="lnkbtnCancelSetting" runat="server" CommandName="CancelSetting"
                                                            ToolTip="Cancel PTE" CausesValidation="false" TextMessage="Cancel" CssClass="btn btn-white"
                                                            Visible="false" ValidationGroup="InsertSettingValidation"></cc1:ResourceLinkButton>

                                                        <cc1:ResourceLinkButton ID="lnkbtnAddMore" TextMessage="Add More" runat="server" ForeColor="White"
                                                            ToolTip="Add More PTE" CssClass="btn btn-primary text-center" CommandName="AddMore">
                                                        </cc1:ResourceLinkButton>

                                                    </FooterTemplate>
                                                    <EditItemTemplate>

                                                        <cc1:ResourceLinkButton ID="btnUpdateSetting" runat="server" CausesValidation="true"
                                                            ToolTip="Update PTE" ValidationGroup="updateSettingValidation" TextMessage="Update" ForeColor="White"
                                                            CommandName="update" CssClass="btn btn-sm btn-primary" />
                                                        <cc1:ResourceLinkButton ID="btnCancelSetting" runat="server" CausesValidation="false"
                                                            ToolTip="Cancel PTE" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-sm btn-white" />

                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:Panel ID="pnlAddPTE" runat="server">
                                            <table class="add-new-inventory">
                                                <tr>

                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Organization Type")%></th>
                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Product Size")%></th>
                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Effective Date")%></th>
                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Expiration Date")%></th>
                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Amount")%></th>
                                                    <th class="txt-had" style="text-align: center;"><%=ResourceMgr.GetMessage("Actions")%></th>
                                                </tr>
                                                <tr class="validateFooterGrid">
                                                    <td>
                                                        <asp:DropDownList ID="ddlstakeholderTypeListfooter" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlstakeholderTypeListfooter" ForeColor="Red"
                                                            ValidationGroup="InsertSettingValidation" ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTireSizeListfooter" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rsvldreqddlTireSizeListfooter" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlTireSizeListfooter" ForeColor="Red" ValidationGroup="InsertSettingValidation"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txteffectivedatefooter" runat="server" CssClass="datepicker" Visible="false">
                                                        </asp:TextBox><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor3" ForeColor="Red" ValidationGroup="InsertSettingValidation"
                                                            runat="server" ControlToValidate="txteffectivedatefooter" ErrorText="Enter Effective Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtexpirationdatefooter" runat="server" CssClass="datepicker" Visible="false"></asp:TextBox>
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor1" ForeColor="Red" ValidationGroup="InsertSettingValidation"
                                                            runat="server" ControlToValidate="txtexpirationdatefooter" ErrorText="Enter Expiration Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDollarValuefooter" runat="server" Visible="false" MaxLength="4">
                                                        </asp:TextBox>
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValuefooter" ForeColor="Red" ValidationGroup="InsertSettingValidation"
                                                            runat="server" ControlToValidate="txtDollarValuefooter" ErrorText="Enter Dollar Value"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        <cc1:ResourceRegularExpressionValidator ID="revDollarValue" ForeColor="Red" ValidationGroup="InsertSettingValidation"
                                                            runat="server" ControlToValidate="txtDollarValuefooter" ValidationExpression="^[0-9\.]*$"
                                                            ErrorText="Enter Numeric Only." Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <cc1:ResourceLinkButton ID="lnkbtnAddSetting" runat="server" OnClick="lnkbtnAddSetting_Click"
                                                            ToolTip="Add PTE" CausesValidation="true" TextMessage="Add" CssClass="btnAddUpdateSettings"
                                                            Visible="False" ValidationGroup="InsertSettingValidation" ImageUrl="/Images/add_icon.png"
                                                            Style="white-space: nowrap; float: left;"></cc1:ResourceLinkButton>
                                                        <cc1:ResourceLinkButton ID="lnkbtnCancelSetting" runat="server" CausesValidation="false"
                                                            ToolTip="Cancel PTE" OnClick="lnkbtnCancelSetting_Click" TextMessage="Cancel"
                                                            CssClass="btnAddUpdateSettings" Visible="false" ValidationGroup="InsertSettingValidation"
                                                            ImageUrl="/Images/delete_icon.png" Style="white-space: nowrap; float: left;"></cc1:ResourceLinkButton>
                                                        <cc1:ResourceLinkButton ID="lnkbtnAddMore" TextMessage="Add PTE" runat="server"
                                                            ToolTip="Add PTE" CssClass="btnAddUpdateSettings" ImageUrl="/Images/add_icon.png"
                                                            OnClick="lnkbtnAddMore_Click" Style="white-space: nowrap;"></cc1:ResourceLinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>


                                        <asp:GridView ID="gvSettingsProduct" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered epr-sec-table" EnableViewState="true" DataKeyNames="PTEId"
                                            EmptyDataText="No data found" OnRowDataBound="gvSettingsProduct_RowDataBound"
                                            OnRowEditing="gvSettingsProduct_RowEditing" OnRowCommand="gvSettingsProduct_RowCommand"
                                            OnRowUpdating="gvSettingsProduct_RowUpdating" OnRowCancelingEdit="gvSettingsProduct_RowCancelingEdit"
                                            OnSelectedIndexChanged="gvSettingsProduct_SelectedIndexChanged" OnRowUpdated="gvSettingsProduct_RowUpdated"
                                            ShowFooter="true" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                            <Columns>


                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Organization Type")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStakeholderTypeProduct" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlstakeholderTypeListeditorProduct" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnOrganizationSubTypeIdProduct" Value='<%# Eval("OrganizationSubTypeId") %>'
                                                            runat="server" />
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rsvldreqddlstakeholderTypeListeditorProduct" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlstakeholderTypeListeditorProduct" ForeColor="Red"
                                                            ValidationGroup="updateSettingValidationProduct" ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlstakeholderTypeListfooterProduct" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rsvldddlstakeholderTypeListfooterProduct" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlstakeholderTypeListfooterProduct" ForeColor="Red"
                                                            ValidationGroup="InsertSettingValidationProduct" ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Product Name")%>
                                                    </HeaderTemplate>


                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlProductNamegv" runat="server" Height="20px" Width="133px" >
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnProductId" Value='<%# Eval("ProductCategoryid") %>' runat="server" />
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="RequiredFieldValidator7" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductNamegv" ForeColor="Red" ValidationGroup="updateSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlProductNameFooter" runat="server" Visible="false" >
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator8" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductNameFooter" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>--%>


                                                <asp:TemplateField HeaderText="Product Sub Name" Visible="false">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Product Sub Name")%>
                                                    </HeaderTemplate>


                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductSubName" runat="server" Text='<%# Eval("SubProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlProductSubNamegv" runat="server" Height="20px" Width="133px">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnProductSubId" Value='<%# Eval("SubProductId") %>' runat="server" />
                                                        <br />
                                                        <%--<cc1:ResourceRequiredFieldValidator ID="RequiredFieldValidator77" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductSubNamegv" ForeColor="Red" ValidationGroup="updateSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>--%>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlProductSubNameFooter" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <%--<cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator88" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductSubNameFooter" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>--%>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Product Size")%>
                                                    </HeaderTemplate>


                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTireSizeProduct" runat="server" Text='<%# Eval("ProductSize") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlTireSizeListeditorProduct" runat="server" Height="20px" Width="133px">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnSizeIdd" Value='<%# Eval("ProductSizeid") %>' runat="server" />
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlTireSizeListeditorProduct" ForeColor="Red" ValidationGroup="updateSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlTireSizeListfooterProduct" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator17" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlTireSizeListfooterProduct" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Product Shape")%>
                                                    </HeaderTemplate>


                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductShape" runat="server" Text='<%# Eval("ProductShape") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlProductShape" runat="server" Height="20px" Width="133px">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnShapeId" Value='<%# Eval("ProductShapeid") %>' runat="server" />
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="RequiredFieldValidator18" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductShape" ForeColor="Red" ValidationGroup="updateSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlProductShapeFooter" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator3" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductShapeFooter" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Product Material")%>
                                                    </HeaderTemplate>


                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductMaterial" runat="server" Text='<%# Eval("ProductMaterial") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlProductMaterial" runat="server" Height="20px" Width="133px">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnMaterialId" Value='<%# Eval("ProductMaterialid") %>' runat="server" />
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductMaterial" ForeColor="Red" ValidationGroup="updateSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlProductMaterialFooter" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator5" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductMaterialFooter" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Effective Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEffactiveDateProduct" runat="server" Text='<%# Convert.ToDateTime(Eval("EffectiveDate")).ToString("MM/dd/yyyy") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txteffectivedateeditorProduct" runat="server" CssClass="datepicker" Text='<%# Convert.ToDateTime(Eval("EffectiveDate")).ToString("MM/dd/yyyy") %>'></asp:TextBox>
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditoasdasr2Product" ForeColor="Red" ValidationGroup="updateSettingValidationProduct"
                                                            runat="server" ControlToValidate="txteffectivedateeditorProduct" ErrorText="Enter Effective Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txteffectivedatefooterProduct" runat="server" CssClass="datepicker" Visible="false">
                                                        </asp:TextBox>
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor2Product" ValidationGroup="InsertSettingValidationProduct" ForeColor="Red"
                                                            runat="server" ControlToValidate="txteffectivedatefooterProduct" ErrorText="Enter Effective Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Expiration Date")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpirationDateProduct" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpirationDate")).ToString("MM/dd/yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtexpirationdateeditorProduct" runat="server" CssClass="datepicker" Text='<%# Convert.ToDateTime(Eval("ExpirationDate")).ToString("MM/dd/yyyy") %>'><%--  CssClass="datepicker"--%></asp:TextBox><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditordsad1Product" ForeColor="Red"
                                                            ValidationGroup="updateSettingValidationProduct"
                                                            runat="server" ControlToValidate="txtexpirationdateeditorProduct" ErrorText="Enter Expiration Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtexpirationdatefooterProduct" runat="server" CssClass="datepicker" Visible="false">
       
                                                        </asp:TextBox><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor1Product" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            runat="server" ControlToValidate="txtexpirationdatefooterProduct" ErrorText="Enter Expiration Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Amount")%>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDollarValueProduct" runat="server" Text='<%# Convert.ToDecimal(Eval("DollarValue")).ToString("C") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDollarValueeditorProduct" runat="server" Text='<%# Eval("DollarValue") %>' MaxLength="4">
                                                        </asp:TextBox><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditorProduct" ForeColor="Red" ValidationGroup="updateSettingValidationProduct"
                                                            runat="server" ControlToValidate="txtDollarValueeditorProduct" ErrorText="Enter Dollar Value"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        <cc1:ResourceRegularExpressionValidator ID="revDollarValueeditorProduct" ForeColor="Red"
                                                            ValidationGroup="updateSettingValidationProduct" runat="server" ControlToValidate="txtDollarValueeditorProduct"
                                                            ValidationExpression="^[0-9\.]*$" ErrorText="Enter Numeric Only." Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtDollarValuefooterProduct" runat="server" Visible="false" MaxLength="4">
                                                        </asp:TextBox><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvDollarValuefooterProduct" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            runat="server" ControlToValidate="txtDollarValuefooterProduct" ErrorMessage="Enter Dollar Value"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        <cc1:ResourceRegularExpressionValidator ID="revDollarValueProduct" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            runat="server" ControlToValidate="txtDollarValuefooterProduct" ValidationExpression="^[0-9\.]*$"
                                                            ErrorText="Enter Numeric Only." Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("History") %>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <a href="#">

                                                            <img title="View History" src="/Images/history_icon.png" /></a>

                                                    </ItemTemplate>


                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="60" ItemStyle-Wrap="false" FooterStyle-Wrap="false">
                                                    <HeaderTemplate>
                                                        <%= ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgbtnEditSettingProduct" ToolTip="Edit PTE" CausesValidation="false" runat="server" CommandName="Edit"
                                                            CommandArgument='<%# Eval("PTEId") %>' Text="Edit"
                                                            CssClass="btn btn-white btn-bitbucket"> <i class="fa fa-edit"></i> </asp:LinkButton>

                                                        <asp:LinkButton ID="imgbtnDeleteSettingProduct" ToolTip="Delete PTE" runat="server" CommandName="Delete" CausesValidation="false" Text="Delete"
                                                            CommandArgument='<%# Eval("PTEId") %>' CssClass="btn btn-white btn-bitbucket" OnClientClick="return confirm('Are you sure you want to delete PTE?');"> <i class="fa fa-remove"></i> </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <FooterTemplate>

                                                        <cc1:ResourceLinkButton ID="lnkbtnAddSettingProduct" runat="server" CommandName="Insert"
                                                            ToolTip="Add PTE" CausesValidation="true" TextMessage="Add" CssClass="btn btn-primary" ForeColor="White"
                                                            Visible="false" ValidationGroup="InsertSettingValidationProduct"></cc1:ResourceLinkButton>
                                                        <cc1:ResourceLinkButton ID="lnkbtnCancelSettingProduct" runat="server" CommandName="CancelSetting"
                                                            ToolTip="Cancel PTE" CausesValidation="false" TextMessage="Cancel" CssClass="btn btn-white"
                                                            Visible="false" ValidationGroup="InsertSettingValidationProduct"></cc1:ResourceLinkButton>

                                                        <cc1:ResourceLinkButton ID="lnkbtnAddMoreProduct" TextMessage="Add More" runat="server" ForeColor="White"
                                                            ToolTip="Add More PTE" CssClass="btn btn-primary text-center" CommandName="AddMore">
                                                        </cc1:ResourceLinkButton>

                                                    </FooterTemplate>
                                                    <EditItemTemplate>

                                                        <cc1:ResourceLinkButton ID="btnUpdateSettingProduct" runat="server" CausesValidation="true"
                                                            ToolTip="Update PTE" ValidationGroup="updateSettingValidationProduct" TextMessage="Update" ForeColor="White"
                                                            CommandName="update" CssClass="btn btn-sm btn-primary" />
                                                        <cc1:ResourceLinkButton ID="btnCancelSettingProduct" runat="server" CausesValidation="false"
                                                            ToolTip="Cancel PTE" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-sm btn-white" />

                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:Panel ID="pnlAddPteProduct" runat="server">
                                            <table class="add-new-inventory">
                                                <tr>

                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Organization Type")%></th>
                                                    <%--<th class="txt-had"><%=ResourceMgr.GetMessage("Product Name")%></th>--%>
                                                    <%-- <th class="txt-had" id="SubName" runat="server"><%=ResourceMgr.GetMessage("Product Sub Name")%></th>--%>
                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Product Size")%></th>
                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Product Shape")%></th>
                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Product Material")%></th>
                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Effective Date")%></th>
                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Expiration Date")%></th>
                                                    <th class="txt-had"><%=ResourceMgr.GetMessage("Amount")%></th>
                                                    <th class="txt-had" style="text-align: center;"><%=ResourceMgr.GetMessage("Actions")%></th>
                                                </tr>
                                                <tr class="validateFooterGrid">
                                                    <td>
                                                        <asp:DropDownList ID="ddlStakeholderTypeProduct" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator2" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlStakeholderTypeProduct" ForeColor="Red"
                                                            ValidationGroup="InsertSettingValidationProduct" ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>
                                                    <%-- <td>
                                                        <asp:DropDownList ID="ddlProductName" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator9" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductName" ForeColor="Red"
                                                            ValidationGroup="InsertSettingValidationProduct" ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>--%>
                                                    <%--<td>
                                                        <asp:DropDownList ID="ddlProductSubName" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator10" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductSubName" ForeColor="Red"
                                                            ValidationGroup="InsertSettingValidationProduct" ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>--%>

                                                    <td>
                                                        <asp:DropDownList ID="ddlProductSize" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator3" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductSize" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlProductShape" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator7" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductShape" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlProductMaterial" runat="server" Visible="false">
                                                        </asp:DropDownList><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator8" InitialValue="0"
                                                            runat="server" ControlToValidate="ddlProductMaterial" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEffectiveDateProduct" runat="server" CssClass="datepicker" Visible="false">
                                                        </asp:TextBox><br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator4" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            runat="server" ControlToValidate="txtEffectiveDateProduct" ErrorText="Enter Effective Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExpiryDateProduct" runat="server" CssClass="datepicker" Visible="false"></asp:TextBox>
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator5" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            runat="server" ControlToValidate="txtExpiryDateProduct" ErrorText="Enter Expiration Date"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDollarProduct" runat="server" Visible="false" MaxLength="4">
                                                        </asp:TextBox>
                                                        <br />
                                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator6" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            runat="server" ControlToValidate="txtDollarProduct" ErrorText="Enter Dollar Value"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        <cc1:ResourceRegularExpressionValidator ID="ResourceRegularExpressionValidator1" ForeColor="Red" ValidationGroup="InsertSettingValidationProduct"
                                                            runat="server" ControlToValidate="txtDollarProduct" ValidationExpression="^[0-9\.]*$"
                                                            ErrorText="Enter Numeric Only." Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <cc1:ResourceLinkButton ID="lnkAddSettingProduct" runat="server" OnClick="lnkAddSettingProduct_Click"
                                                            ToolTip="Add PTE" CausesValidation="true" TextMessage="Add" CssClass="btnAddUpdateSettings"
                                                            Visible="False" ValidationGroup="InsertSettingValidationProduct" ImageUrl="/Images/add_icon.png"
                                                            Style="white-space: nowrap; float: left;"></cc1:ResourceLinkButton>
                                                        <cc1:ResourceLinkButton ID="lnkCancelSettingProduct" runat="server" CausesValidation="false"
                                                            ToolTip="Cancel PTE" OnClick="lnkCancelSettingProduct_Click" TextMessage="Cancel"
                                                            CssClass="btnAddUpdateSettings" Visible="false" ValidationGroup="InsertSettingValidationProduct"
                                                            ImageUrl="/Images/delete_icon.png" Style="white-space: nowrap; float: left;"></cc1:ResourceLinkButton>
                                                        <cc1:ResourceLinkButton ID="lnkAddMoreProducts" TextMessage="Add PTE" runat="server"
                                                            ToolTip="Add PTE" CssClass="btnAddUpdateSettings" ImageUrl="/Images/add_icon.png"
                                                            OnClick="lnkAddMoreProducts_Click" Style="white-space: nowrap;"></cc1:ResourceLinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>

                                    </div>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-md-12">
                                    <uc2:Pager ID="pager" runat="server" />

                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

