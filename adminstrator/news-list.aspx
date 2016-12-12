<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/adminstrator/MasterPage.master" Title="鑫梅企業股份有限公司-最新消息" CodeFile="news-list.aspx.vb" Inherits="adminstrator_news_list"  ValidateRequest="false"%>
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="show1" runat="server">
 <TABLE width="100%" border=0 cellPadding=10 cellSpacing=0 class=tableBorder>  
 <TR>
 <TD class=tableHeading>最新消息管理</TD>
 </TR>
 <TR>
   <TD class=tableCellTwo>
	<TABLE cellSpacing=0 cellPadding=1 width="100%" align=center border=0>
         <TR>
           <TD>
		      <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1">
                   <tr class="tableCellTwo">
                     <td align="middle">&nbsp;</td>
                     <td width="23%" align="right" class="defaultBold"><asp:Button ID="BtnAdd" CssClass="b_2" runat="server" Text="新增消息" /></td>
                   </tr>
               </table>
             <asp:GridView ID="GridView1" runat="server" cellspacing=1 cellpadding=2 width="100%" bgcolor=#a9cdd9 align=center border=0 AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SDS_Data" PageSize="20">
                   <Columns>
                       <asp:TemplateField HeaderText="編號">
                           <ItemTemplate>
                           <asp:Literal ID="LitNum" runat="server"></asp:Literal>
                           </ItemTemplate>
                           <HeaderStyle Width="6%" height="24px" HorizontalAlign="Center" />
                           <ItemStyle height="30px" HorizontalAlign="Center"  />
                       </asp:TemplateField>
                       <asp:BoundField DataField="SiteName" HeaderText="網站" SortExpression="SiteName" >
                           <HeaderStyle Width="10%" HorizontalAlign="Center" />
                           <ItemStyle HorizontalAlign="Center"  />
					   </asp:BoundField>
                       <asp:TemplateField>
                           <ItemTemplate>
                           <asp:Image ID="Image1" runat="server" ImageUrl="~/adminstrator/images/file_info.gif" />
						   </ItemTemplate>
                           <HeaderStyle Width="6%" HorizontalAlign="Center" />
                           <ItemStyle HorizontalAlign="Center"  />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="標題" SortExpression="Subject">
                          <ItemTemplate>
                          <asp:LinkButton ID="LinkSubject" runat="server" CommandArgument='<%# Bind("NID") %>' CommandName="BtnNew" Text='<%# Bind("Subject") %>'></asp:LinkButton>
                          </ItemTemplate>
                          <ItemStyle CssClass="defaultBold" HorizontalAlign="Left" />
                          <HeaderStyle HorizontalAlign="Center" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="上線" SortExpression="Isonline">
                           <ItemTemplate>
                           <asp:Literal ID="LitIsonline" runat="server"></asp:Literal>
                           </ItemTemplate>
                           <HeaderStyle Width="5%" HorizontalAlign="Center" />
                           <ItemStyle CssClass="defaultBold" HorizontalAlign="Center"  />
                       </asp:TemplateField>
                       <asp:BoundField DataField="PostDate" HeaderText="發佈日期" SortExpression="PostDate" >
                           <HeaderStyle HorizontalAlign="Center" />
                           <ItemStyle Width="150" HorizontalAlign="Center"  />
					   </asp:BoundField>
                       <asp:TemplateField>
                           <ItemTemplate>
                           <asp:Button ID="BtnPublish" runat="server" CssClass="b_2" Text="編輯" CausesValidation="False" CommandName="BtnNew" CommandArgument='<%# Bind("NID") %>' />
                           <asp:Button ID="btnDel" runat="server" CssClass="b_2" Text="刪除" CausesValidation="False" CommandName="NewDelete" CommandArgument='<%# Bind("NID") %>' OnClientClick="return confirm('您確定要刪除這筆資料？資料刪除無法回復！');"></asp:Button>
                           </ItemTemplate>
                           <HeaderStyle Width="100" HorizontalAlign="Center" />
                           <ItemStyle CssClass="defaultBold" HorizontalAlign="Center"  />
                       </asp:TemplateField>
                   </Columns>
              </asp:GridView>
              <asp:AccessDataSource ID="SDS_Data" runat="server" DataFile="~/App_Data/Tym.mdb" SelectCommand="select NID,SiteName,Subject,Isonline,PostDate from New inner join Site on New.SiteId=Site.SiteID order by LastDate Desc"></asp:AccessDataSource>
</TD>
</TR>
</TABLE>
</TD>
</TR>
</TABLE>
</asp:Panel>     
<asp:Panel ID="Show2" runat="server">
     <table class=tableBorder style="WIDTH: 100%" cellspacing=0 cellpadding=10 border=0>
     <tr>
       <td class=tableHeading><asp:Literal ID="LitTitle" runat="server"></asp:Literal></td>
     </tr>
     <tr>
       <td class=tableCellTwo><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
           <tr>
             <td width="100%">
               <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3" class="tab_login">
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr >
                   <td height="35" bgcolor="#F6FFFF" align="center">上稿網站：</td>
                   <td>
                       <asp:DropDownList ID="SiteID" Cssclass="location" style="width:100px;" runat="server">
                       </asp:DropDownList><asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="&nbsp;&nbsp;請選擇上稿網站！" ControlToValidate="SiteID" ValidationGroup="2"></asp:RequiredFieldValidator>
                   </td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">標題：</td>
                   <td class="commontxt3">
                   <asp:TextBox ID="Subject" runat="server" style="width:98%" Cssclass="form"></asp:TextBox>
                   <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="&nbsp;&nbsp;請輸入標題！" ControlToValidate="Subject" ValidationGroup="2"></asp:RequiredFieldValidator>
                   </td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">內容：</td>
                   <td class="commontxt3">                   
                        <FCKeditorV2:FCKeditor ID="FCKeditor3" runat="server" ToolbarStartExpanded="false" BasePath="Components/Publisher/_vit_cnfs/_vit_cnfs/" Height="300px" Width="98%">
                        </FCKeditorV2:FCKeditor>
                   </td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">小圖：</td>
                   <td class="commontxt3">
                     <table width="100%" border="0" cellpadding="0" cellspacing="0">
                       <tr>
                         <td width="340" valign="top"><asp:FileUpload ID="FileUpload1" CssClass="form" style="width:98%" runat="server" />
                         <asp:Literal ID="LitFile" runat="server"></asp:Literal>
                         </td>
                         <td>* 格式為 jpg / gif / swf，圖形限制：寬度 120 像素，大小 50K 以內。</td>
                       </tr>
                   </table></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr >
                   <td height="35" bgcolor="#F6FFFF" align="center">發佈 / 上架</td>
                   <td >
                       <asp:CheckBox ID="Isonline" runat="server" Text="上架" />
                   </td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <!--Dynamic Item List End -->
               </table>
               <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                 <tr>
                   <td>&nbsp;</td>
                 </tr>
                 <tr>
                   <td style="padding-left:5px;">&nbsp;&nbsp;
				   <asp:Button ID="BtnEdit"  class="b_1" runat="server" Text="確認送出" ValidationGroup="2" />&nbsp;&nbsp;
                   <input name="重設" type="reset" class="b_1" value="重設">&nbsp;&nbsp;
                   <asp:Button ID="BtnBack"  class="b_1" runat="server" Text="回上一頁" />
                   </TD>
                 </TR>
                 </TABLE>
</TD>
</TR>
</TABLE>
</TD>
</TR>
</TABLE>
</asp:Panel>                 
</asp:Content> 