<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditionControl.ascx.cs" Inherits="CommonControls_EditionControl" %>
<a data-toggle="dropdown" class="dropdown-toggle" href="#">
    <span class="clear">
        <span class="text-xs block">
            <asp:Literal ID="litLangauge" runat="server" Text="English"></asp:Literal>
            <img id="img" runat="server" src='/images/usa_flag.png' />
            <b class="caret"></b></span>
    </span>
</a>
<ul class="dropdown-menu animated fadeInRight m-t-xs">
    <asp:Repeater ID="rptLanguage" runat="server" OnItemCommand="rptLanguage_ItemCommand">
        <ItemTemplate>
            <li>
                <asp:LinkButton CssClass="" ID="lnkEnglish" runat="server" CommandName="ChangeLanguage" CausesValidation="false" CommandArgument='<%#Eval("Specific") %>'> <p class="mb0">
                    <img src="/images/<%#Eval("Flag") %>" /> <%#Eval("CountryName")%>  </p> </asp:LinkButton>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
