<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="ViewReports.aspx.cs" Inherits="Reports_ViewReports" %>

<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="grid-contain-outer">
        <!-- Search Filters start here   -->
        <asp:UpdatePanel ID="upnlGrid" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <asp:Panel ID="pnlSearch" runat="server">
                                <div class="ibox-title">
                                    <h5><%=ResourceMgr.GetMessage("Search Filters")%></h5>
                                </div>

                                <div class="ibox-content">
                                    <div class="row search-filter" id="">
                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label>
                                                Sample
                                            </label>
                                            <asp:TextBox runat="server" ID="txt1" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label>
                                                Sample
                                            </label>
                                            <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label>
                                                Sample
                                            </label>
                                            <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control"></asp:TextBox>

                                        </div>

                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label>
                                                Sample
                                            </label>
                                            <asp:TextBox runat="server" ID="TextBox3" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label>
                                                Sample
                                            </label>
                                            <asp:TextBox runat="server" ID="TextBox4" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4 col-sm-6 col-lg-3">
                                            <label>
                                                Sample
                                            </label>
                                            <asp:TextBox runat="server" ID="TextBox5" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-sm-12 mb0">
                                            <asp:LinkButton ID="btnReportsSearch" runat="server" CssClass="btn btn-primary btn-sm">
                                                <%=ResourceMgr.GetMessage("Search") %></asp:LinkButton>
                                            <asp:LinkButton ID="btnReportsCancel" runat="server" CssClass="btn btn-white btn-sm">
                                                <%=ResourceMgr.GetMessage("Reset") %></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5><%=ResourceMgr.GetMessage("Reports") %></h5>
                                <div class="ibox-tools">
                                    <div class="form-group">
                                        <%--A Button if One exists like Add Something--%>
                                    </div>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <table class ="table table-bordered table-bordered epr-sec-table"> 
                                                <!-- EmptyDataRowStyle-CssClass="alert alert-danger text-center" -->
                                                <thead>
                                                    <tr>
                                                        <td>Sample</td>
                                                        <td>Sample</td>
                                                        <td>Sample</td>
                                                        <td>Sample</td>
                                                        <td>Sample</td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>Sample 01</td>
                                                        <td>Sample 01</td>
                                                        <td>Sample 01</td>
                                                        <td>Sample 01</td>
                                                        <td>Sample 01</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sample 02</td>
                                                        <td>Sample 02</td>
                                                        <td>Sample 02</td>
                                                        <td>Sample 02</td>
                                                        <td>Sample 02</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sample 03</td>
                                                        <td>Sample 03</td>
                                                        <td>Sample 03</td>
                                                        <td>Sample 03</td>
                                                        <td>Sample 03</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sample 04</td>
                                                        <td>Sample 04</td>
                                                        <td>Sample 04</td>
                                                        <td>Sample 04</td>
                                                        <td>Sample 04</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sample 05</td>
                                                        <td>Sample 05</td>
                                                        <td>Sample 05</td>
                                                        <td>Sample 05</td>
                                                        <td>Sample 05</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sample 06</td>
                                                        <td>Sample 06</td>
                                                        <td>Sample 06</td>
                                                        <td>Sample 06</td>
                                                        <td>sample 06</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sample 07</td>
                                                        <td>Sample 07</td>
                                                        <td>Sample 07</td>
                                                        <td>Sample 07</td>
                                                        <td>Sample 07</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sample 08</td>
                                                        <td>Sample 08</td>
                                                        <td>Sample 08</td>
                                                        <td>Sample 08</td>
                                                        <td>Sample 08</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sample 09</td>
                                                        <td>Sample 09</td>
                                                        <td>Sample 09</td>
                                                        <td>Sample 09</td>
                                                        <td>Sample 09</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sample 10</td>
                                                        <td>Sample 10</td>
                                                        <td>Sample 10</td>
                                                        <td>Sample 10</td>
                                                        <td>sample 10</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:DropDownList runat="server" ID="ddlPageSize">
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- Search Filters end here   -->
    </div>

</asp:Content>

