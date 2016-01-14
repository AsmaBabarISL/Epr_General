<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="AccessDenied.aspx.cs" Inherits="AccessDenied" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="grid-contain-outer">
        <div class="txt-main-had">
            <div class="txt-had-left" style="background: none;">
                <%= ResourceMgr.GetMessage("Access Denied")%>
            </div>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Access Denied" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>

