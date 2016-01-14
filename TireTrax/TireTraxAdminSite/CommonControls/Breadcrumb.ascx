<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Breadcrumb.ascx.cs" Inherits="CommonControls_Breadcrumb" %>

<div class="row wrapper border-bottom white-bg page-heading">
    <div class="col-lg-10">
        <h1 id="hPageHeading">Dashboard</h1>
        You are here:
        <ol class="breadcrumb">
            <asp:SiteMapPath ID="SiteMapPath1" runat="server">
                <PathSeparatorTemplate> / </PathSeparatorTemplate>
                <PathSeparatorStyle CssClass="display-none" /> 
            </asp:SiteMapPath>
        </ol>
    </div>
    <div class="col-lg-2">
    </div>
</div>

