<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="DetailRevenue.aspx.cs" Inherits="Revenue_DetailRevenue" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<%-- <script type="text/javascript">

     function ClearFields() {
         $("#<%=txtfrmdate.ClientID%>").val('');
         $("#<%=txttodate.ClientID%>").val('');

         $("#<%=btnRevenueSearch.ClientID%>")[0].click();
     }
      </script>--%>

 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       
     <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Revenue</h5>
                            <div class="ibox-tools">
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="table-responsive">

        
                <asp:GridView ID="gvRevenue" AutoGenerateColumns="False" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                    CssClass="table table-bordered epr-sec-table" EnableViewState="true" EmptyDataText="No data found"
                     runat="server" >
                  <Columns>
                        <%--<asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt-had">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Organization")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                 <%# Eval("LegalName")%>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                       
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("DotNumber")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("DOTNumber")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Tire Size")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("TireSize")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Bar Code")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("SerialNumber")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <%=ResourceMgr.GetMessage("Amount")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Convert.ToDecimal(Eval("Amount")).ToString("C")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                       
                        
                    </Columns>
                   
                </asp:GridView>
                
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRevenueSearch" EventName="Click" />
            </Triggers>--%>
 
  

</asp:Content>

