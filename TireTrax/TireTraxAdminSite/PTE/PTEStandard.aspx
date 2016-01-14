<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="PTEStandard.aspx.cs" Inherits="PTE_PTEStandard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" type="text/css" href="/Scripts/themes/base/jquery.ui.all.css" />
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
     <script src="/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        function SetDatePicket() {
            $(".datepicker").datepicker({
                minDate: new Date(),
                onSelect: function (a, b) {
                    if ($(this).parent().next().find(".datepicker2").val() == "")
                        $(this).parent().next().find(".datepicker2").val(b.currentMonth + "/" + b.currentDay + "/" + (b.currentYear + 1));
                }
            });

            $(".datepicker2").datepicker({ minDate: new Date() });
        }

    </script>
  

                        

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>Add PTE Standards</h5>
                                    <div class="ibox-tools">
                                        <div class="form-group">
                                            <strong class="m-r-xs">Select State:</strong>
                                            <asp:DropDownList ID="ddlStewardship" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="ddlStewardship_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="ddlStewardshipRequired" runat="server" ControlToValidate="ddlStewardship" InitialValue="0" ErrorMessage="Please select Stewardship" Text="*" CssClass="error-validator"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="pnlAddPTE" runat="server">
                                    <div class="ibox-content">
                                        <table class="table table-bordered epr-sec-table">
                                            <tr>

                                                <th class="txt-had"><%=ResourceMgr.GetMessage("Product Size")%></th>
                                                <th class="txt-had"><%=ResourceMgr.GetMessage("Effective Date")%></th>
                                                <th class="txt-had"><%=ResourceMgr.GetMessage("Expiration Date")%></th>
                                                <th class="txt-had"><%=ResourceMgr.GetMessage("Amount")%></th>
                                                <th class="txt-had" style="text-align: center; width:150px;"><%=ResourceMgr.GetMessage("Actions")%></th>
                                            </tr>
                                            <tr class="validateFooterGrid">

                                                <td>
                                                    <asp:DropDownList ID="ddlTireSizeListfooter" runat="server" Visible="false">
                                                    </asp:DropDownList>
                                                    <cc1:ResourceRequiredFieldValidator ID="rsvldreqddlTireSizeListfooter" InitialValue="0"
                                                        runat="server" ControlToValidate="ddlTireSizeListfooter" CssClass="custom-block-error" ValidationGroup="InsertSettingValidation"
                                                        ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txteffectivedatefooter" runat="server" CssClass="datepicker" Visible="false">
                                                    </asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor3" CssClass="custom-block-error" ValidationGroup="InsertSettingValidation"
                                                        runat="server" ControlToValidate="txteffectivedatefooter" ErrorText="Enter Effective Date"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtexpirationdatefooter" runat="server" CssClass="datepicker2" Visible="false"></asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor1" CssClass="custom-block-error" ValidationGroup="InsertSettingValidation"
                                                        runat="server" ControlToValidate="txtexpirationdatefooter" ErrorText="Enter Expiration Date"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDollarValuefooter" runat="server" Visible="false">
                                                    </asp:TextBox>
                                                    <cc1:ResourceRequiredFieldValidator ID="rfvDollarValuefooter" CssClass="custom-block-error" ValidationGroup="InsertSettingValidation"
                                                        runat="server" ControlToValidate="txtDollarValuefooter" ErrorText="Enter Dollar Value"
                                                        Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                    <cc1:ResourceRegularExpressionValidator ID="revDollarValue" CssClass="custom-block-error" ValidationGroup="InsertSettingValidation"
                                                        runat="server" ControlToValidate="txtDollarValuefooter" ValidationExpression="^[0-9\.]*$"
                                                        ErrorText="Enter Numeric Only." Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                                </td>
                                                <td class="text-center">
                                                    <cc1:ResourceLinkButton ID="lnkbtnAddSetting" runat="server" Text="Add" OnClick="lnkbtnAddSetting_Click"
                                                        ToolTip="Add PTE Standard" CausesValidation="true" CssClass="btn btn-sm btn-white font-bold" Visible="False" ValidationGroup="InsertSettingValidation">
                                                    </cc1:ResourceLinkButton>

                                                    <cc1:ResourceLinkButton ID="lnkbtnCancelSetting" runat="server" Text="Cancel" CausesValidation="false"
                                                        ToolTip="Cancel" OnClick="lnkbtnCancelSetting_Click" CssClass="btn btn-sm btn-white font-bold" Visible="false" ValidationGroup="InsertSettingValidation">
                                                    </cc1:ResourceLinkButton>
                                                    <cc1:ResourceLinkButton ID="lnkbtnAddMore" runat="server" ToolTip="Add PTE Standard" Text="Add PTE Standard" CssClass="btn btn-sm btn-white font-bold"
                                                        OnClick="lnkbtnAddMore_Click"></cc1:ResourceLinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                              
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>PTE Standards</h5>
                                </div>
                                <div class="ibox-content">
                                    <asp:Label ID="lblErrorMessage" runat="server" Visible="false"></asp:Label> 
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvSetting" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table" EnableViewState="true" DataKeyNames="PTEStandardId"
                                                EmptyDataText="No data found" ShowFooter="true" OnDataBound="gvSetting_DataBound" OnRowDataBound="gvSetting_RowDataBound"
                                                OnRowEditing="gvSetting_RowEditing" OnRowCommand="gvSetting_RowCommand" OnRowUpdated="gvSetting_RowUpdated"
                                                OnRowUpdating="gvSetting_RowUpdating" OnSelectedIndexChanged="gvSetting_SelectedIndexChanged"
                                                OnRowCancelingEdit="gvSetting_RowCancelingEdit" OnRowDeleted="gvSetting_RowDeleted"
                                                OnRowDeleting="gvSetting_RowDeleting" EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Product Size")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTireSize" runat="server" Text='<%# Eval("ProductSize") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlTireSizeListeditor" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdnSizeId" Value='<%# Eval("SizeId") %>' runat="server" />
                                                            <cc1:ResourceRequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0"
                                                                runat="server" ControlToValidate="ddlTireSizeListeditor" CssClass="custom-block-error" ValidationGroup="updateSettingValidation"
                                                                ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlTireSizeListfooter" runat="server" Visible="false">
                                                            </asp:DropDownList>
                                                            <cc1:ResourceRequiredFieldValidator ID="ResourceRequiredFieldValidator1" InitialValue="0"
                                                                runat="server" ControlToValidate="ddlTireSizeListfooter" CssClass="custom-block-error" ValidationGroup="InsertSettingValidation"
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
                                                            <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditoasdasr2" CssClass="custom-block-error" ValidationGroup="updateSettingValidation"
                                                                runat="server" ControlToValidate="txteffectivedateeditor" ErrorText="Enter Effective Date"
                                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txteffectivedatefooter" runat="server" CssClass="datepicker" Visible="false">
                                                            </asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor2" CssClass="custom-block-error" ValidationGroup="InsertSettingValidation"
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
                                                            <asp:TextBox ID="txtexpirationdateeditor" runat="server" CssClass="datepicker" Text='<%# Convert.ToDateTime(Eval("ExpirationDate")).ToString("MM/dd/yyyy") %>'><%--  CssClass="datepicker"--%></asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditordsad1" CssClass="custom-block-error" ValidationGroup="updateSettingValidation"
                                                                runat="server" ControlToValidate="txtexpirationdateeditor" ErrorText="Enter Expiration Date"
                                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtexpirationdatefooter" runat="server" CssClass="datepicker" Visible="false">
       
                                                            </asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor1" CssClass="custom-block-error" ValidationGroup="InsertSettingValidation"
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
                                                            <asp:TextBox ID="txtDollarValueeditor" runat="server" Text='<%# Eval("DollarValue") %>'>
                                                            </asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="rfvDollarValueeditor" CssClass="custom-block-error" ValidationGroup="updateSettingValidation"
                                                                runat="server" ControlToValidate="txtDollarValueeditor" ErrorText="Enter Dollar Value"
                                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRegularExpressionValidator ID="revDollarValueeditor" CssClass="custom-block-error"
                                                                ValidationGroup="updateSettingValidation" runat="server" ControlToValidate="txtDollarValueeditor"
                                                                ValidationExpression="^[0-9\.]*$" ErrorText="Enter Numeric Only." Display="Dynamic"></cc1:ResourceRegularExpressionValidator>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtDollarValuefooter" runat="server" Visible="false">
                                                            </asp:TextBox>
                                                            <cc1:ResourceRequiredFieldValidator ID="rfvDollarValuefooter" CssClass="custom-block-error" ValidationGroup="InsertSettingValidation"
                                                                runat="server" ControlToValidate="txtDollarValuefooter" ErrorMessage="Enter Dollar Value"
                                                                Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                                                            <cc1:ResourceRegularExpressionValidator ID="revDollarValue" CssClass="custom-block-error" ValidationGroup="InsertSettingValidation"
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
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <%= ResourceMgr.GetMessage("Actions")%>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnEditSetting" runat="server" CausesValidation="false" Text="Edit"
                                                                ToolTip="Edit PTE Standard" CommandArgument='<%# Eval("PTEStandardId") %>' CommandName="Edit">
                                                                <i class="fa fa-pencil"></i>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton CssClass="btn btn-white btn-bitbucket" ID="imgbtnDeleteSetting" runat="server" CausesValidation="false"
                                                                ToolTip="Delete PTE Standard" Text="Delete" CommandArgument='<%# Eval("PTEStandardId") %>'
                                                                OnClientClick="return confirm('Are you sure you want to delete PTE?');" CommandName="Delete">
                                                                <i class="fa fa-trash"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <cc1:ResourceLinkButton ID="lnkbtnAddSetting" runat="server" CommandName="Insert" ToolTip="Add PTE Standard" CausesValidation="true" TextMessage="Add" CssClass="btn btn-white btn-bitbucket" Visible="false" ValidationGroup="InsertSettingValidation">
                                                            </cc1:ResourceLinkButton>
                                                            
                                                            <cc1:ResourceLinkButton ID="lnkbtnCancelSetting" runat="server" CommandName="CancelSetting" ToolTip="Cancel" CausesValidation="false"           TextMessage="Cancel" CssClass="btn btn-white btn-bitbucket" Visible="false" ValidationGroup="InsertSettingValidation">
                                                            </cc1:ResourceLinkButton>

                                                            <cc1:ResourceLinkButton ID="lnkbtnAddMore" TextMessage="Add More" CssClass="btn btn-white btn-bitbucket" runat="server" 
                                                                ToolTip="Add More PTE Standard" CommandName="AddMore">
                                                            </cc1:ResourceLinkButton>
                                                        </FooterTemplate>
                                                        <EditItemTemplate>
                                                            <cc1:ResourceLinkButton ID="btnUpdateSetting" runat="server" CausesValidation="true"
                                                                ToolTip="Update PTE Standard" ValidationGroup="updateSettingValidation" TextMessage="Update"
                                                                CommandName="update" CssClass="btn btn-white btn-bitbucket" ImageUrl="/Images/add_new_icon2.png" />
                                                            <cc1:ResourceLinkButton ID="btnCancelSetting" runat="server" CausesValidation="false"
                                                                ToolTip="Cancel" TextMessage="Cancel" CommandName="Cancel" CssClass="btn btn-white btn-bitbucket"
                                                                ImageUrl="../Images/delete_icon.png" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>

                                        <div class="row">
                                        <div class="col-lg-6">
                                            <asp:DropDownList runat="server" ID="ddlPageSize" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                                <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                                <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                            </asp:DropDownList>
                                            <%=ResourceMgr.GetMessage("Records Per Page")%>
                                            <asp:Label ID="lblPagingLeft" runat="server" CssClass="m-l-sm"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Literal ID="ltrlPaging" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>



</asp:Content>
