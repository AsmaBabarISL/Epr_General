<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BreadCrumb.ascx.cs" Inherits="CommonControls_BreadCrumb" %>
<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>

<div class="row wrapper border-bottom white-bg page-heading">
    <div class="col-lg-10">
       <h1 id="hPageHeading">Dashboard</h1>
        <%= ResourceMgr.GetMessage("You are here")%>:
        <ol class="breadcrumb">
            <asp:SiteMapPath ID="SiteMapPath1" runat="server">
                
                <PathSeparatorTemplate>
                    /
                </PathSeparatorTemplate>
                <PathSeparatorStyle CssClass="display-none" /> 
            </asp:SiteMapPath>
        </ol>
    </div>
    <div class="col-lg-2">
    </div>
</div>
<script type="text/javascript">
    function SetHeaderMenu(el, msg) {
       
        if (msg == null)
            
            document.getElementById('hPageHeading').innerHTML = document.getElementById(el).textContent;
        else
            document.getElementById("hPageHeading").innerHTML = msg;
           
    }
</script>