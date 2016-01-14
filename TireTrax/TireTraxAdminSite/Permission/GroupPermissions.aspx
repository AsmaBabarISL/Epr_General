<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true"
    CodeFile="GroupPermissions.aspx.cs" Inherits="Permission_GroupPermissions" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function fadeOut() {
            alert("hi");
            $(".custom-absolute-alert").delay(3000).fadeOut(300);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlPermisionAdd" runat="server" DefaultButton="btnSaveChanges">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>
                                    <asp:Label ID="lbHeader" runat="server"></asp:Label>
                                    Group Permissions</h5>
                                <div class="ibox-tools">
                                </div>
                            </div>
                            <div class="ibox-content" style="display: block;">
                                <div class="row">
                                    <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                        <label>Organization Type</label>
                                        <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" CausesValidation="True"
                                            TabIndex="1" class="form-control" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <cc1:ResourceRequiredFieldValidator ID="ValidatorRole" runat="server" ControlToValidate="ddlRole"
                                            Display="Dynamic" ErrorMessage="*" CssClass="custom-error" InitialValue="0" ValidationGroup="save"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div id="dvSubRole" runat="server" visible="false">
                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label>Role</label>
                                            <asp:DropDownList ID="ddlSubRole" runat="server" AutoPostBack="True" CausesValidation="True"
                                                class="form-control" OnSelectedIndexChanged="ddlSubRole_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="dvgrp" runat="server" visible="false">
                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label>Groups</label>
                                            <asp:DropDownList ID="ddlgroup" runat="server" AutoPostBack="True" CausesValidation="True" CssClass="form-control">
                                            </asp:DropDownList>
                                            <cc1:ResourceRequiredFieldValidator ID="Validatorgroup" runat="server" ControlToValidate="ddlgroup"
                                                Display="Dynamic" ErrorMessage="*" CssClass="custom-error" ValidationGroup="save"></cc1:ResourceRequiredFieldValidator>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group col-md-12">
                                        <label id="groupName" runat="server" visible="">Group Names</label><br clear="all"/><br clear="all"/>
                                        <asp:CheckBoxList RepeatColumns="2" Enabled="true" RepeatDirection="Horizontal" ID="cblGroupName" class="roles-Chkbox checkbox-inline prod-type-chkbox" runat="server"></asp:CheckBoxList>
                                    </div>

                                   <%-- <div class="form-group col-sm-12">--%>

                                        <%--btnSave modified from "btnSave_Click" to "btnCancelChanges_Click" --%>
                                        <%--btnCancel modified from "btnCancel_Click" to "btnCancelChanges_Click" --%>

                                       <%-- <asp:Button ID="btnSave" runat="server" 
                                            OnClick="btnSaveChanges_Click" 
                                            Text="Save" ValidationGroup="save"
                                            Enabled="false" CssClass="btn btn-primary btn-sm font-bold" Visible="false" />
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancelChanges_Click" Text="Cancel"
                                            CssClass="btn btn-white btn-sm font-bold" Visible="false" />--%>
                                   <%-- </div>--%>

                                    <div class="form-group col-sm-12 mb0">
                                        <asp:Button ID="btnSaveChanges" runat="server" OnClick="btnSaveChanges_Click" Text="Save" ValidationGroup="save"
                                            CssClass="btn btn-primary btn-sm font-bold" Visible="false" />
                                        <asp:Button ID="btnCancelChanges" runat="server" OnClick="btnCancelChanges_Click" Text="Cancel"
                                            CssClass="btn btn-white btn-sm font-bold" Visible="false" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:Label ID="lblInfo" runat="server" CssClass="custom-absolute-alert alert-success"></asp:Label>
                <asp:Label ID="lblError" runat="server" CssClass="custom-absolute-alert alert-danger"></asp:Label>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
