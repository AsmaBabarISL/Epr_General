<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="AddCommission.aspx.cs" Inherits="Commission_AddCommission" %>
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
<div class="grid-contain-outer">
        <div class="txt-main-had">
           
            <div class="txt-had-left" style="background: none;">
                <%= ResourceMgr.GetMessage("Commission")%>
            </div>
            
        </div>
          <asp:UpdatePanel ID="upnlsearch" runat="server">
            <ContentTemplate>  
            <div class="search-filter-outer">
                <div class="login_block_inner" >
                   
                        
                        <div class="login-content_block" >
                            <div class="basic_login-had" style="width:95px;" >
                                <%= ResourceMgr.GetMessage("Country :")%>
                            </div>
                            <div class="login_field">
                                <asp:DropDownList ID="ddlcountry" runat="server"  AutoPostBack="true" CssClass="txt-field" 
                                    onselectedindexchanged="ddlcountry_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        

                        <div class="login-content_block">
                            <div class="basic_login-had" style="width:95px;">
                                <%= ResourceMgr.GetMessage("Stewardship :")%>
                            </div>
                            <div class="login_field">
                                <asp:DropDownList ID="ddlstewardshiptype" runat="server" AutoPostBack="true" CssClass="txt-field" 
                                    onselectedindexchanged="ddlstewardshiptype_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                 <asp:Label ID="lblerror" runat="server" CssClass="messagewithgreen" ></asp:Label>
                </div>
            </div>
      
    
     <asp:GridView ID="gvCommissionType" runat="server" DataKeyNames="CommisionId,FeetypeId,LookupTypeID"
            AutoGenerateColumns="False" GridLines="None"
                    CssClass="add-new-inventory"  EnableViewState="true" EmptyDataText="No data found"
                    wrap="nowrap" CellPadding="5" Width="100%" ShowFooter="true" onrowdatabound="gvCommissionType_RowDataBound" 
                     >
                    <AlternatingRowStyle CssClass="highlighted-row" />
                    <HeaderStyle CssClass="txt-had" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="" ItemStyle-HorizontalAlign="left">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Stakeholder") %>
                            </HeaderTemplate>
                            <ItemTemplate>
                               
                               <%# Eval("LookupTypeName")%>
                                <asp:HiddenField ID="hdnfldId" Value='<%# Eval("CommisionId") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                             <asp:DropDownList ID="ddlStakeholderType" runat="server" Height="20px" Width="133px">
                                </asp:DropDownList>
                                <cc1:ResourceRequiredFieldValidator ID="rsvldreqddlstakeholderTypeListeditor123" InitialValue="0"
                                            runat="server" ControlToValidate="ddlStakeholderType" ForeColor="Red"
                                            ValidationGroup="updateSettingValidation" ErrorText="Select Value" Display="Dynamic"></cc1:ResourceRequiredFieldValidator>
                            </EditItemTemplate>
                            <HeaderStyle CssClass="" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%= ResourceMgr.GetMessage("Commission Type")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:RadioButtonList ID="rbType" runat="server"  AutoPostBack='true' 
                                            onselectedindexchanged="rbType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Amount</asp:ListItem>
                                            <asp:ListItem Value="2">Percentage</asp:ListItem>
                                            <asp:ListItem Value="3" Selected="True">None</asp:ListItem>
                                        </asp:RadioButtonList>
                            </ItemTemplate>
                            <HeaderStyle CssClass="" />
                            
                            <ItemStyle HorizontalAlign="Center" />
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%= ResourceMgr.GetMessage("Amount/Percentage")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                 <asp:TextBox Enabled="false" ID="txtAmount" runat="server" ValidationGroup="txt" Text='<%#DataBinder.Eval(Container.DataItem, "Amount") %>' MaxLength="5"  OnTextChanged ="txtAmount_TextChanged" AutoPostBack="true" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="" />
                            
                            <ItemStyle HorizontalAlign="Center" />
                            
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                </ContentTemplate>
        </asp:UpdatePanel>
                
                </div>

</asp:Content>

