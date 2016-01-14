<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="AddPages.aspx.cs" Inherits="Permission_AddPages" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    <div id="Div1" runat="server" style=" background:url(/images/bg_shadow.png) repeat;width:100%;height:100%;position:fixed;
    z-index:999;top:0;left:0;z-index:99999;display:block;"> 
           <img src="/images/ajax-loader.gif" style="position:fixed; left:0; right:0; top:0; bottom:0; margin:auto;" />
            </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
<div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5> <%= ResourceMgr.GetMessage("Add Pages")%></h5>
                    <div class="ibox-tools">
                    </div>
                </div>
                <div class="ibox-content" style="display: block;">
                
           
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
 <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                    <asp:GridView ID="gvPages" runat="server" AutoGenerateColumns="False" GridLines="None"
                        CssClass="table table-bordered epr-sec-table" EnableViewState="true" DataKeyNames="intResourceId"
                        EmptyDataText="No data was found" ShowFooter="true" OnRowDataBound="gvPages_RowDataBound" OnRowEditing="gvPages_RowEditing"
                        OnRowCommand="gvPages_RowCommand" OnRowUpdated="gvPages_RowUpdated" OnRowUpdating="gvPages_RowUpdating"
                        OnSelectedIndexChanged="gvPages_SelectedIndexChanged" OnRowCancelingEdit="gvPages_RowCancelingEdit"
                        OnRowDeleted="gvPages_RowDeleted" OnRowDeleting="gvPages_RowDeleting" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                        
                        <Columns>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had" HeaderStyle-Width="30%">
                                <HeaderStyle HorizontalAlign="left" />
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Page Name")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("vchName")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPageName" runat="server" CssClass="field_block" Text='<%# Eval("vchName") %>'>
                                    </asp:TextBox><br />
                                    <cc1:ResourceRequiredFieldValidator ID="rfvPageName" ValidationGroup="updateSettingValidation" CssClass="custom-error"
                                        runat="server" ControlToValidate="txtPageName" ErrorText="Enter Page Name" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPageNamefooter" CssClass="field_block" runat="server" Visible="false">
                                    </asp:TextBox><br />
                                    <cc1:ResourceRequiredFieldValidator ID="rfvPageNamefooter" CssClass="custom-error" ValidationGroup="InsertSettingValidation"
                                        runat="server" ControlToValidate="txtPageNamefooter" ErrorMessage="Enter Page Name"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderTemplate>
                                        <%=ResourceMgr.GetMessage("Page Path")%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("vchPath")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPagePath" runat="server" Text='<%# Eval("vchPath") %>'>
                                        </asp:TextBox><br />
                                        <cc1:ResourceRequiredFieldValidator ID="rfvPagePath" CssClass="custom-error" ValidationGroup="updateSettingValidation"
                                            runat="server" ControlToValidate="txtPagePath" ErrorText="Enter Page Path"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtPagePathfooter" runat="server" Visible="false">
                                        </asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="rfvPagePathfooter" CssClass="custom-error" ValidationGroup="InsertSettingValidation"
                                            runat="server" ControlToValidate="txtPagePathfooter" ErrorMessage="Enter Page Path"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-Width="200">
                                <HeaderStyle HorizontalAlign="left" />
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Page Domain")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDomain" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdfparentid" runat="server" Value='<%# Eval("intparentid") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlType" runat="server" CausesValidation="True" Visible="false">
                                    </asp:DropDownList>
                                    <cc1:ResourceRequiredFieldValidator ID="ValidatorType" runat="server" ControlToValidate="ddlType"
                                        Display="Dynamic" ErrorMessage="*" CssClass="custom-error" InitialValue="0" ValidationGroup="save"></cc1:ResourceRequiredFieldValidator>
                                    <br />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlTypeFooter" runat="server" AutoPostBack="True" CausesValidation="True" Visible="false">
                                    </asp:DropDownList>
                                    <cc1:ResourceRequiredFieldValidator ID="ValidatorType" runat="server" ControlToValidate="ddlTypeFooter"
                                        Display="Dynamic" ErrorMessage="*" CssClass="custom-error" InitialValue="0" ValidationGroup="save"></cc1:ResourceRequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdfActivebit" runat="server" Value='<%# Eval("bitIsDeleted") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-Width="130">
                                <HeaderTemplate>
                                    <%=ResourceMgr.GetMessage("Actions")%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="imgbtnEditPage" runat="server" CausesValidation="false" Text="Edit"
                                        ToolTip="Edit Group" CommandArgument='<%# Eval("intResourceId") %>' CommandName="Edit"  CssClass="btn btn-white btn-bitbucket"><i class="fa fa-edit"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgbtnDeletePage" runat="server" CausesValidation="false" ToolTip="Deactive Page"
                                        Text="Delete" CommandArgument='<%# Eval("intResourceId") %>' OnClientClick="return confirm('Are you sure you want to deactivate Page?');"
                                        CommandName="Delete" CssClass="btn btn-white btn-bitbucket"><i class="fa fa-trash-o"></i></asp:LinkButton>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <cc1:ResourceLinkButton ID="lnkbtnAddPage" runat="server" CommandName="Insert" ToolTip="Add Page"
                                        CausesValidation="true" Visible="false" ValidationGroup="InsertSettingValidation" CssClass="btn btn-sm btn-white font-bold">Add</cc1:ResourceLinkButton>
                                    <cc1:ResourceLinkButton ID="lnkbtnCancelPage" runat="server" CommandName="CancelPage"
                                        ToolTip="Cancel Page" CausesValidation="false"  Visible="false" ValidationGroup="InsertSettingValidation" CssClass="btn btn-sm btn-white font-bold">Cancel</cc1:ResourceLinkButton>
                                    <cc1:ResourceLinkButton ID="lnkbtnAddMore" runat="server" ToolTip="Add More Page" CssClass="btn btn-sm btn-white font-bold" CommandName="AddMore" >Add More</cc1:ResourceLinkButton>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <cc1:ResourceLinkButton ID="btnUpdatePage" runat="server" CausesValidation="true"
                                        ToolTip="Update Page" ValidationGroup="updateSettingValidation" TextMessage="Update"
                                        CommandName="update" CssClass="btn btn-sm btn-white font-bold"/>
                                    <cc1:ResourceLinkButton ID="btnCancelPage" runat="server" CausesValidation="false"
                                        ToolTip="Cancel Page" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-sm btn-white font-bold"/>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Panel ID="pnlAddPTE" runat="server">
                        <table class="add-new-inventory" cellpadding="0" style="width: 100%;">
                            <tr>
                                <th class="txt-had">
                                    <%=ResourceMgr.GetMessage("Page Name")%>
                                </th>
                                <th class="txt-had">
                                        <%=ResourceMgr.GetMessage("Page Path")%>
                                    </th>
                                <th class="txt-had">
                                    <%=ResourceMgr.GetMessage("Page Domain")%>
                                </th>
                                <th class="txt-had">
                                    <%=ResourceMgr.GetMessage("Actions")%>
                                </th>
                            </tr>
                            <tr class="validateFooterGrid">
                                <td>
                                    <asp:TextBox ID="txtPageNamefooter" runat="server" Visible="false">
                                    </asp:TextBox>
                                    <cc1:ResourceRequiredFieldValidator ID="rfvPageNameNewfooter" CssClass="custom-error" ValidationGroup="InsertSettingValidation"
                                        runat="server" ControlToValidate="txtPageNamefooter" ErrorText="Enter Page Name"
                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                </td>
                                <td>
                                        <asp:TextBox ID="txtPagePathfooter" runat="server" Visible="false">
                                        </asp:TextBox>
                                        <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" CssClass="custom-error" ValidationGroup="InsertSettingValidation"
                                            runat="server" ControlToValidate="txtPagePathfooter" ErrorText="Enter Page Path"
                                            Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                    </td>
                                <td>
                                   <asp:DropDownList ID="ddlTypeFooter" runat="server" AutoPostBack="True" CausesValidation="True" Visible="false">
                                    </asp:DropDownList>
                                    <cc1:ResourceRequiredFieldValidator ID="ValidatorType" runat="server" ControlToValidate="ddlTypeFooter" Display="Dynamic" ErrorMessage="*" CssClass="custom-error" InitialValue="0" ValidationGroup="save"></cc1:ResourceRequiredFieldValidator>
                                </td>
                                <td>
                                    <cc1:ResourceLinkButton ID="lnkbtnAddPage" runat="server" OnClick="lnkbtnAddPage_Click"
                                        ToolTip="Add Page" CausesValidation="true" CssClass="btn btn-sm btn-white font-bold"
                                        Visible="False" ValidationGroup="InsertSettingValidation">Add</cc1:ResourceLinkButton>
                                    <cc1:ResourceLinkButton ID="lnkbtnCancelPage" runat="server" CausesValidation="false"
                                        ToolTip="Cancel Page" OnClick="lnkbtnCancelPage_Click" CssClass="btn btn-sm btn-white font-bold"
                                        Visible="false" ValidationGroup="InsertSettingValidation">Cancel</cc1:ResourceLinkButton>
                                    <cc1:ResourceLinkButton ID="lnkbtnAddMore" runat="server" ToolTip="Add More Page" CssClass="btn btn-sm btn-white font-bold">Add More</cc1:ResourceLinkButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:HiddenField ID="hdfactive" runat="server" />
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

