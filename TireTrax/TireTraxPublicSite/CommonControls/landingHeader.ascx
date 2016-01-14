<%@ Control Language="C#" AutoEventWireup="true" CodeFile="landingHeader.ascx.cs" Inherits="CommonControls_landingHeader" %>


<%@ Register Assembly="TireTraxLib" Namespace="TireTraxLib" TagPrefix="cc1" %>
<div class="ubltools hidden" id="languageMenu">
  <div class="ubltoolswrap">
      <div class="ublt">
          <ul>
              <li><a href="#">
                  <img id="img" runat="server" src='/images/usa_flag.png' /> <span><asp:Literal ID="litLangauge" runat="server" Text="English"></asp:Literal></span>
              </a>
              </li>
              <asp:Repeater ID="rptLanguage" runat="server" OnItemCommand="rptLanguage_ItemCommand">
                  <ItemTemplate>
                      <li>
                          <asp:LinkButton ID="lnkEnglish" runat="server" CommandName="ChangeLanguage" CausesValidation="false"
                               CommandArgument='<%#Eval("Specific") %>'>
                              <img src="/images/<%#Eval("Flag") %>" />
                               <span><%#Eval("CountryName")%></span>

                          </asp:LinkButton>
                      </li>
                  </ItemTemplate>
              </asp:Repeater>
          </ul>
      </div> 
    <div class="ubltb ubltb_open"><i class="fa fa-language"></i></div>
  </div>
</div>