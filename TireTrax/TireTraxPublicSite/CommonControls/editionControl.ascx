<%@ Control Language="C#" AutoEventWireup="true" CodeFile="editionControl.ascx.cs" Inherits="CommonControls_editionControl" %>


    <ul class="flag_navigation">
            <li> <a href="#"> <span><asp:Literal ID="litLangauge" runat="server" Text="USA"></asp:Literal></span> &nbsp; <img id="img" runat="server" src='/images/usa_flag.png' /> </a>
              <ul>
                <asp:Repeater ID="rptLanguage" runat="server" onitemcommand="rptLanguage_ItemCommand">
                <ItemTemplate>
                <li> <asp:LinkButton ID="lnkEnglish" runat="server" CommandName="ChangeLanguage" CausesValidation="false" CommandArgument='<%#Eval("Specific") %>'> <span><%#Eval("CountryName")%></span> &nbsp; <img src='/images/<%#Eval("Flag") %>' /> </asp:LinkButton> </li>
                </ItemTemplate>
                </asp:Repeater>
              </ul>
            </li>
          </ul>
 