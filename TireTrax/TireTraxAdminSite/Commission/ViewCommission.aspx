<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewCommission.aspx.cs" Inherits="Commission_ViewCommission" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Div1" runat="server" style="background: url(/images/bg_shadow.png) repeat; width: 100%; height: 100%; position: fixed; z-index: 999; top: 0; left: 0; z-index: 99999; display: block;">
                <img src="/images/ajax-loader.gif" style="position: fixed; left: 0; right: 0; top: 0; bottom: 0; margin: auto;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upnlsearch" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><%= ResourceMgr.GetMessage("Commission")%></h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row search-filter" id="">
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Country")%></label>
                                    <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="ddlCountryRequired" runat="server" ControlToValidate="ddlcountry" InitialValue="0" ErrorMessage="Please select Stewardship"
                                        Text="*" CssClass="error-validator asteric"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                    <label><%= ResourceMgr.GetMessage("Stewardship")%></label>
                                    <asp:DropDownList ID="ddlstewardshiptype" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlstewardshiptype_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="ddlStewardshipRequired" runat="server" ControlToValidate="ddlstewardshiptype" InitialValue="0" ErrorMessage="Please select Stewardship" Text="*" CssClass="error-validator asteric"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvCommissionType" runat="server" DataKeyNames="CommisionId,OrganizationSubTypeId,StewardshipID"
                                        AutoGenerateColumns="False" CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data found" OnRowDataBound="gvCommissionType_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%=ResourceMgr.GetMessage("Stakeholder") %>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("OrganizationSubType")%>
                                                    <asp:HiddenField ID="hdnfldId" Value='<%# Eval("CommisionId") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" HeaderStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <%= ResourceMgr.GetMessage("Commission Type")%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rbType" runat="server" AutoPostBack='true'
                                                        OnSelectedIndexChanged="rbType_SelectedIndexChanged" RepeatDirection="Horizontal" CssClass="">
                                                        <asp:ListItem Value="1"><span style="padding:5px">Amount</span></asp:ListItem>
                                                        <asp:ListItem Value="2"><span style="padding:5px">Percentage</span></asp:ListItem>
                                                        <asp:ListItem Value="3" Selected="True"><span style="padding:5px">None</span></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="160">
                                                <HeaderTemplate>
                                                    <%= ResourceMgr.GetMessage("Amount/Percentage")%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox Enabled="false" ID="txtAmount" runat="server" ValidationGroup="txt" Text='<%#DataBinder.Eval(Container.DataItem, "Amount") %>' MaxLength="5" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-12">
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-sm btn-primary font-bold" Text="Update" OnClick="btnUpdate_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:Label ID="lblerror" runat="server" CssClass="custom-absolute-alert alert-success"></asp:Label>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

