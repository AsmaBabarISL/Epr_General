<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true"
    CodeFile="AddRole.aspx.cs" Inherits="Permission_AddRole" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ClearErrorFileds() {
            if ($("#<%=txtRole.ClientID%>").val() == '') {
                $("#<%=lblRoleNameError.ClientID%>").text('');
            }
            if ($("#<%=lblInfo.ClientID%>").text() == 'Role is Successfully Added' ||
               $("#<%=lblInfo.ClientID%>").text() == 'Role Name Already Exist') {
                $("#<%=lblInfo.ClientID%>").text('');
            }
        }
    </script>
    <script type="text/javascript">
        function AddPopupClass() {
            $(".ajaxModal-popup").appendTo("form");
            $(".ajax-loader").remove();
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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlPermisionAdd" runat="server" DefaultButton="btnSave">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%= ResourceMgr.GetMessage("Add Roles")%></h5>
                            </div>
                            <div class="ibox-content">
                                <asp:Label ID="lblInfo" runat="server" Text="" CssClass="custom-absolute-alert alert-success"></asp:Label>
                                <asp:Label ID="lblRoleNameError" runat="server" Text="" CssClass="custom-absolute-alert alert-danger"></asp:Label>
                                <div class="row search-filter" id="">
                                    <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                        <label>Organization Type</label>
                                        <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" CausesValidation="True"
                                            TabIndex="1" class="form-control" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <cc1:ResourceRequiredFieldValidator ID="ValidatorRole" runat="server" ControlToValidate="ddlRole"
                                            Display="Dynamic" ErrorMessage="Please Select Group" CssClass="custom-error" InitialValue="0" ValidationGroup="save"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                        <label>Role</label>
                                        <asp:TextBox ID="txtRole" runat="server" class="form-control"></asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtRole" Display="Dynamic" ErrorMessage="Please Enter Role Name"
                                            CssClass="custom-error" ValidationGroup="save"></cc1:ResourceRequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-sm-12">
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="ClearErrorFileds();"
                                            Text="Save" ValidationGroup="save" CssClass="btn btn-sm btn-primary font-bold" />

                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                            CausesValidation="false" CssClass="btn btn-sm btn-white font-bold" ValidationGroup="save" />
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
                                <h5><%= ResourceMgr.GetMessage("Roles")%></h5>
                                <div class="ibox-tools">
                                </div>
                            </div>
                            <div class="ibox-content" style="display: block;">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table" EnableViewState="true" DataKeyNames="RoleId" EmptyDataText="No data was found" OnRowCancelingEdit="gvRoles_RowCancelingEdit" OnRowCommand="gvRoles_RowCommand"
                                        OnRowDeleted="gvRoles_RowDeleted" OnRowDeleting="gvRoles_RowDeleting" OnRowEditing="gvRoles_RowEditing"
                                        OnRowUpdated="gvRoles_RowUpdated" OnRowUpdating="gvRoles_RowUpdating" OnRowDataBound="gvRole_RowDataBound" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <%=ResourceMgr.GetMessage("Role Name")%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("RoleName") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRoleName" runat="server" Text='<%# Eval("RoleName") %>'></asp:TextBox>
                                                    <asp:Label ID="lblInfoF" runat="server" Text="" CssClass="custom-error"></asp:Label>
                                                    <asp:HiddenField ID="hdnroleid" runat="server" Value='<%# Eval("RoleId") %>' />
                                                    <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor2" ValidationGroup="updateSettingValidation"
                                                        runat="server" CssClass="custom-error" ControlToValidate="txtRoleName" ErrorText="*" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <%=ResourceMgr.GetMessage("Date Created")%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDateTime(Eval("DateCreated")).ToString("MM/dd/yyyy")%>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="imgbtnEditSetting" runat="server" CausesValidation="false"
                                                        ToolTip="Edit Role" CommandArgument='<%# Eval("RoleId") %>' CommandName="Edit" CssClass="btn btn-white btn-bitbucket"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="imgbtnDeleteSetting" runat="server" CausesValidation="false"
                                                        ToolTip="Deactive Role" CommandArgument='<%# Eval("RoleId") %>' OnClientClick="return confirm('Are you sure you want to delete Role?');" CommandName="Delete" CssClass="btn btn-white btn-bitbucket"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <cc1:ResourceLinkButton ID="btnUpdateSetting" runat="server" CausesValidation="true"
                                                        ToolTip="Update Role" ValidationGroup="updateSettingValidation" TextMessage="Update" OnClientClick="$('.error-L').hide();"
                                                        CommandName="update" CssClass="btn btn-sm btn-white font-bold" />
                                                    <cc1:ResourceLinkButton ID="btnCancelSetting" runat="server" CausesValidation="false"
                                                        ToolTip="Cancel Role" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-sm btn-white font-bold" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active/Inactive">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="imgStatus" Text="" ToolTip="Status" runat="server" CommandName="Status"
                                                        CommandArgument='<%#Eval("RoleId") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <div class="txt-pagination">
                                        <uc2:Pager ID="pgrLots" runat="server" />
                                    </div>
                                    <asp:HiddenField ID="hdngroupid" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>

            <div id="dvUserRoleInfo" runat="server" visible="false">
                <div class="ajaxModal-popup inmodal">
                    <div class="ajaxModal-body animated bounceInRight" id="dvParkingLot1" runat="server">
                        <div class="modal-header">
                            <h4 class="modal-title">Roles
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="alert alert-danger">
                                You can not delete this Role beacause it has following User roles
                            </div>

                            <div class="table-responsive">
                                <asp:GridView ID="gvUserRoleInfo" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered epr-sec-table"
                                    EnableViewState="true" EmptyDataText="No data was found" runat="server"
                                    OnRowCancelingEdit="gvUserRoleInfo_RowCancelingEdit"
                                    OnRowCommand="gvUserRoleInfo_RowCommand"
                                    OnRowDeleted="gvUserRoleInfo_RowDeleted"
                                    OnRowDeleting="gvUserRoleInfo_RowDeleting"
                                    OnRowEditing="gvUserRoleInfo_RowEditing"
                                    OnRowUpdated="gvUserRoleInfo_RowUpdated"
                                    OnRowUpdating="gvUserRoleInfo_RowUpdating"
                                    OnRowDataBound="gvUserRoleInfo_RowDataBound" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("RoleName")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("RoleName")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlRoleName" runat="server">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdnLookupType" Value='<%# Eval("LookupTypeID") %>' runat="server" />

                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("User Name")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("Login")%>
                                                <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# Eval("UserId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Date Created")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Convert.ToDateTime(Eval("DateCreated")).ToString("MM/dd/yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <%=ResourceMgr.GetMessage("Edit")%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgbtnEditRole" runat="server" CausesValidation="false" Text="Edit"
                                                    ToolTip="Edit Role" CommandArgument='<%# Eval("LookupTypeID") %>' CommandName="Edit" CssClass="btn btn-white btn-bitbucket"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%-- <asp:ImageButton ID="imgbtnEditRole" runat="server" CausesValidation="false" Text="Edit"
                                        ToolTip="Edit Role" CommandArgument='<%# Eval("LookupTypeID") %>' CommandName="Edit" />--%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <cc1:ResourceLinkButton ID="btnUpdateRole" runat="server" ToolTip="Update SubRole" TextMessage="Update"
                                                    CommandName="Update" CssClass="btn btn-sm btn-white font-bold" />
                                                <cc1:ResourceLinkButton ID="btnCancelSetting" runat="server"
                                                    ToolTip="Cancel SubRole" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-sm btn-white font-bold" />

                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:HiddenField ID="hdnSubRoleTypeId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="lnkSubParkingLotCancel" CssClass="btn btn-sm btn-white font-bold" OnClick="lnkAddRoleCancel_Click" runat="server"><%= ResourceMgr.GetMessage("Cancel")%></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
