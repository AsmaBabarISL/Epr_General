<%@ Control Language="C#" AutoEventWireup="true" CodeFile="permissionResource.ascx.cs" Inherits="CommonControls_permissionResource" %>

<asp:HiddenField ID="hdResourceId" runat="server" Value="0" />
<div class="form-inline">
    <div class="row">
        <div class="col-md-5">
             <div class="form-group">
     <b><asp:Label ID="lblPermissionLabel" runat="server"></asp:Label></b>
    </div>
        </div>
        <div class="col-md-7">
             <div class="checkbox m-l m-r-xs">
        <label class="checkbox-inline">
           <asp:CheckBox ID="chkAdd" runat="server" Text="Insert" /></label>
    </div>
    <div class="checkbox m-l m-r-xs">
        <label class="checkbox-inline">
           <asp:CheckBox ID="chkUpdate" runat="server" Text="Update" /></label></div>
    <div class="checkbox m-l m-r-xs">
        <label class="checkbox-inline">
            <asp:CheckBox ID="chkDelete" runat="server" Text="Delete" /></label>
    </div>
    <div class="checkbox m-l m-r-xs">
        <label class="checkbox-inline">
            <asp:CheckBox ID="chkView" runat="server" Text="View" /></label>
    </div>
        </div>
    </div>
   

   
  
</div>

