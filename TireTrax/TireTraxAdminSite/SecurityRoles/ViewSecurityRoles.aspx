<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewSecurityRoles.aspx.cs" Inherits="SecurityRoles_ViewSecurityRoles" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 <script type="text/javascript">
     $(document).ready(function () {
         $('#asearch').toggle(function () {
             $('#midSearch').slideDown();
         }, function () {
             $('#midSearch').slideUp();
         });
     });


       
    </script>


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
    <div class="txt-main-had" style="height: 25px;">
        <div class="txt-had-left">
            <%= ResourceMgr.GetMessage("Organization Type")%>
        </div>
        <div class="txt-had-right" style="padding-right: 0px;">
            <a class="ico_view" href="AdminPermission.aspx" style=" display:none;">
                <%= ResourceMgr.GetMessage("View Permission")%>
            </a>
        </div>
    </div>
    <asp:UpdatePanel ID="upnlGrid" runat="server">
        <contenttemplate>
                <asp:GridView ID="gvStewardship" runat="server" GridLines="None" AutoGenerateColumns="false"
                    CssClass="add-new-inventory" CellPadding="0" CellSpacing="0" BorderWidth="0"
                    Width="100%" EmptyDataText="No data found" wrap="nowrap" DataKeyNames="ID"
                    AlternatingRowStyle-CssClass="highlighted-row" AllowPaging="true" style="background:#ffffff;"
                    >
                    <HeaderStyle CssClass="txt-had" Font-Size="11px" />
                    <Columns>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Organization Type")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a href='viewStewardship.aspx?OrganizationId=<%# Eval("ID") %>'>
                                    <%# Eval("Name") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Permissions")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a href='add-role-permission.aspx?TypeId=<%# Eval("ID") %>'>
                                    <img title="Edit Permissions" src="/Images/edit_icon.png" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
                <div class="txt-pagination">
                    <div class="pagination-left" style="margin-top:9px;">
                        <asp:DropDownList runat="server" ID="ddlPageSize" AutoPostBack="true" onselectedindexchanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="75" Value="75"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            <asp:ListItem Text="250" Value="250"></asp:ListItem>
                            <asp:ListItem Text="500" Value="500"></asp:ListItem>
                        </asp:DropDownList> <%=ResourceMgr.GetMessage("Records Per Page")%> 
                        <asp:Label ID="lblPagingLeft" runat="server" style="padding-left:10px;"></asp:Label>
                    </div>
                    <div class="pagination-right">
                        <asp:Literal ID="ltrlPaging" runat="server"></asp:Literal>
                    </div>
                </div>
               
            </contenttemplate>
        <triggers>
                
                <asp:AsyncPostBackTrigger ControlID="ddlPageSize" EventName="SelectedIndexChanged" />
            </triggers>
    </asp:UpdatePanel>
</div>



</asp:Content>

