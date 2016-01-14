<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true"
    CodeFile="AddGroups.aspx.cs" Inherits="Permission_AddGroups" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%= ResourceMgr.GetMessage("Add Groups")%></h5>
                    <div class="ibox-tools">
                    </div>
                </div>
                <div class="ibox-content" style="display: block;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvGroups" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered epr-sec-table" EnableViewState="true" DataKeyNames="intGroupID"
                                            EmptyDataText="No data was found" wrap="nowrap" ShowFooter="true" OnRowDataBound="gvGroups_RowDataBound" OnRowEditing="gvGroups_RowEditing"
                                            OnRowCommand="gvGroups_RowCommand" OnRowUpdated="gvGroups_RowUpdated" OnRowUpdating="gvGroups_RowUpdating"
                                            OnSelectedIndexChanged="gvGroups_SelectedIndexChanged" OnRowCancelingEdit="gvGroups_RowCancelingEdit"
                                            OnRowDeleted="gvGroups_RowDeleted" OnRowDeleting="gvGroups_RowDeleting" EmptyDataRowStyle-CssClass="alert alert-danger text-center">

                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Group Name")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("vchName")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtGroupName" runat="server" Text='<%# Eval("vchName") %>'>
                                                        </asp:TextBox><asp:Label ID="lblInfo" runat="server" Text="" CssClass="custom-error"></asp:Label>
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvGroupName" ForeColor="Red" ValidationGroup="updateSettingValidation"
                                                            runat="server" ControlToValidate="txtGroupName" ErrorText="Enter Group Name" CssClass="custom-error"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtGroupNamefooter" runat="server" Visible="false">
                                                        </asp:TextBox><asp:Label ID="lblInfoF" runat="server" Text="" CssClass="custom-error"></asp:Label>
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvGroupNamefooter" ForeColor="Red" ValidationGroup="InsertSettingValidation"
                                                            runat="server" ControlToValidate="txtGroupNamefooter" ErrorMessage="Enter Group Name" CssClass="custom-error"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdfActivebit" runat="server" Value='<%# Eval("bitIsDeleted") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="150">
                                                    <HeaderTemplate>
                                                        <%=ResourceMgr.GetMessage("Actions")%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgbtnEditGroup" runat="server" CausesValidation="false" Text="Edit"
                                                            ToolTip="Edit Group" CommandArgument='<%# Eval("intGroupID") %>' CommandName="Edit" CssClass="btn btn-white btn-bitbucket"><i class="fa fa-edit"></i></asp:LinkButton>
                                                       
                                                        <asp:LinkButton ID="imgbtnDeleteGroup" runat="server" CausesValidation="false" ToolTip="Delete Group"
                                            CommandArgument='<%# Eval("intGroupID") %>' CssClass="btn btn-white btn-bitbucket" OnClientClick="return confirm('Are you sure you want to delete Group?');"
                                            CommandName="Delete" ><i class="fa fa-trash-o"></i></asp:LinkButton>
                                       
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <cc1:ResourceLinkButton ID="lnkbtnAddGroup" runat="server" CommandName="Insert" ToolTip="Add Group"
                                                            CausesValidation="true" CssClass="btn btn-sm btn-white font-bold" Visible="false" OnClientClick="$('.error-L').hide();"
                                                            ValidationGroup="InsertSettingValidation">Add</cc1:ResourceLinkButton>
                                                        <cc1:ResourceLinkButton ID="lnkbtnCancelGroup" runat="server" CommandName="CancelGroup"
                                                            ToolTip="Cancel Group" CausesValidation="false" CssClass="btn btn-sm btn-white font-bold"
                                                            Visible="false" ValidationGroup="InsertSettingValidation">Cancel</cc1:ResourceLinkButton>
                                                        <cc1:ResourceLinkButton ID="lnkbtnAddMore" runat="server"
                                                            ToolTip="Add More Group" CssClass="btn btn-sm btn-white font-bold" CommandName="AddMore">Add More</cc1:ResourceLinkButton>
                                                    </FooterTemplate>
                                                    <EditItemTemplate>
                                                        <cc1:ResourceLinkButton ID="btnUpdateGroup" runat="server" CausesValidation="true" ToolTip="Update Group" ValidationGroup="updateSettingValidation" TextMessage="Update" CommandName="update" CssClass="btn btn-sm btn-white font-bold" OnClientClick="$('.error-L').hide();" />
                                                        <cc1:ResourceLinkButton ID="btnCancelGroup" runat="server" CausesValidation="false" ToolTip="Cancel Group" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-sm btn-white font-bold" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Panel ID="pnlAddPTE" runat="server">
                                            <table class="add-new-inventory" cellpadding="0" style="width: 100%;">
                                                <tr>
                                                    <th class="txt-had">
                                                        <%=ResourceMgr.GetMessage("Group Name")%>
                                                    </th>
                                                    <th class="txt-had" style="text-align: center;">
                                                        <%=ResourceMgr.GetMessage("Actions")%>
                                                    </th>
                                                </tr>
                                                <tr class="validateFooterGrid">
                                                    <td>
                                                        <asp:Label ID="lblInfoF" runat="server" Text="" CssClass="custom-error"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtGroupNamefooter" runat="server" Visible="false">
                                                        </asp:TextBox>
                                                        <cc1:ResourceRequiredFieldValidator ID="rfvGroupNameNewfooter" ValidationGroup="InsertSettingValidation"
                                                            runat="server" ControlToValidate="txtGroupNamefooter" ErrorText="Enter Group Name" CssClass="custom-error"
                                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <cc1:ResourceLinkButton ID="lnkbtnAddGroup" runat="server" OnClick="lnkbtnAddGroup_Click" OnClientClick="$('.error-L').hide();" ToolTip="Add Group" CausesValidation="true" CssClass="btn btn-sm btn-white font-bold" Visible="False" ValidationGroup="InsertSettingValidation">Add</cc1:ResourceLinkButton>
                                                        <cc1:ResourceLinkButton ID="lnkbtnCancelGroup" runat="server" CausesValidation="false" ToolTip="Cancel Group" OnClick="lnkbtnCancelGroup_Click" CssClass="btn btn-sm btn-white font-bold" Visible="false" ValidationGroup="InsertSettingValidation">Cancel</cc1:ResourceLinkButton>
                                                        <cc1:ResourceLinkButton ID="lnkbtnAddMore" runat="server" ToolTip="Add More Group" CssClass="btn btn-sm btn-white font-bold" OnClick="lnkbtnAddMore_Click">Add more</cc1:ResourceLinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:HiddenField ID="hdfactive" runat="server" />
                                        <%--pop UP For Not Delete Groups   --%>
                                        <div id="dvGroupsInfo" runat="server" class="box_blockCmp" visible="false">
                                            <div class="popUp_lotInfo">
                                                <div style="float: right; margin-top: -9px; cursor: pointer;" onclick='$("#<%=dvGroupsInfo.ClientID%>").hide();'>
                                                    x
                                                </div>
                                                <div style="float: left; width: 700px; text-align: center; color: red; margin-top: -9px; margin-bottom: 9px;">
                                                    you can not delete this group beacause it has following roles
                                                </div>
                                                <asp:GridView ID="gvGroupsInfo" AutoGenerateColumns="False" GridLines="None" CssClass="add-new-inventory"
                                                    EnableViewState="true" EmptyDataText="No data found" wrap="nowrap" CellPadding="0"
                                                    Width="100%" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Role")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("LookupTypeName")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                                                            <HeaderTemplate>
                                                                <%=ResourceMgr.GetMessage("Role Name")%>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Eval("RoleName")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
