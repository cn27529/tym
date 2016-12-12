<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/adminstrator/MasterPage.master" Title="鑫梅企業股份有限公司-訊價信件" CodeFile="inquiry-list.aspx.vb" Inherits="adminstrator_inquiry_list"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="show1" runat="server">
 <TABLE width="100%" border=0 cellPadding=10 cellSpacing=0 class=tableBorder>  
 <TR>
 <TD class=tableHeading>詢價信件</TD>
 </TR>
 <TR>
   <TD class=tableCellTwo>
	<TABLE cellSpacing=0 cellPadding=1 width="100%" align=center border=0>
         <TR>
           <TD>
             <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1">
               <tr class="tableCellTwo">
                 <td align="right" class="defaultBold">
                     <asp:DropDownList ID="Kind" runat="server" Cssclass="location" style="width:100px;">
                     <asp:ListItem Value="1">公司名</asp:ListItem>
                     <asp:ListItem Value="2">聯絡人</asp:ListItem>
                     </asp:DropDownList>
                     <asp:TextBox ID="SSearch" runat="server" style="width:180px; height:16px" />
					 <asp:Button ID="BtnSearch" runat="server" CssClass="b_2" Text="搜尋" />
				 </td>
                 </tr>
             </table>
            <asp:GridView ID="GridView1" runat="server" cellspacing=1 cellpadding=2 width="100%" bgcolor=#a9cdd9 align=center border=0 AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SDS_Data" PageSize="20">
                   <Columns>
                       <asp:TemplateField HeaderText="編號">
                           <ItemTemplate>
                           <asp:Literal ID="LitNum" runat="server"></asp:Literal>
                           </ItemTemplate>
                           <HeaderStyle Width="6%" height="24px" HorizontalAlign="Center" />
                           <ItemStyle height="30px" HorizontalAlign="Center" />
                       </asp:TemplateField>                       
                       <asp:TemplateField HeaderText="公司名" SortExpression="CompanyName">
                           <ItemTemplate>
                           <asp:LinkButton ID="LinkSubject" runat="server" CommandArgument='<%# Bind("UID") %>' CommandName="ViewData" Text='<%# Bind("CompanyName") %>'></asp:LinkButton>
                           </ItemTemplate>
                           <ItemStyle CssClass="defaultBold" HorizontalAlign="Left" />
                           <HeaderStyle HorizontalAlign="Center" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="聯絡人" SortExpression="UserName">
                           <ItemTemplate>
                           <asp:LinkButton ID="LinkSubject2" runat="server" CommandArgument='<%# Bind("UID") %>' CommandName="ViewData" Text='<%# Bind("UserName") %>'></asp:LinkButton>
                           </ItemTemplate>
                           <ItemStyle CssClass="defaultBold" HorizontalAlign="Left" />
                           <HeaderStyle HorizontalAlign="Center" />
                       </asp:TemplateField>                       
                       <asp:BoundField DataField="SiteName" HeaderText="來源" SortExpression="SiteName" >
                           <HeaderStyle Width="10%" HorizontalAlign="Center" />
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:TemplateField HeaderText="處理狀態" SortExpression="IsRead">
                           <ItemTemplate>
                           <asp:Literal ID="LitRead" runat="server"></asp:Literal>
                           </ItemTemplate>
                           <HeaderStyle Width="10%"  />
						   <ItemStyle HorizontalAlign="Center" />
                       </asp:TemplateField>
                       <asp:BoundField DataField="PostDate" HeaderText="詢價日期" SortExpression="PostDate" >
                           <HeaderStyle HorizontalAlign="Center" />
                           <ItemStyle Width="150" HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:TemplateField>
                           <ItemTemplate>                               
                           <asp:Button ID="btnDel" runat="server" CssClass="b_2" Text="刪除" CausesValidation="False" CommandName="InquiryDelete" CommandArgument='<%# Bind("UID") %>' OnClientClick="return confirm('您確定要刪除這筆資料？資料刪除無法回復！');">
                           </asp:Button>
                           </ItemTemplate>
                           <HeaderStyle Width="60" HorizontalAlign="Center" />
                           <ItemStyle CssClass="defaultBold" HorizontalAlign="Center" />
                       </asp:TemplateField>
                   </Columns>
               </asp:GridView>
               <asp:AccessDataSource ID="SDS_Data" runat="server" DataFile="~/App_Data/Tym.mdb"></asp:AccessDataSource>
</TD>
</TR>
</TABLE>
</TD>
</TR>
</TABLE>
</asp:Panel>
<asp:Panel ID="show2" runat="server">
<TABLE class=tableBorder style="WIDTH: 100%" cellSpacing=0 cellPadding=10 border=0>
     <TR>
     <TD height="62" class=tableHeading>詢價信件</TD>	
     </TR>
     <tr>
       <td class=tableCellTwo><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0"> 
             <tr>
               <td width="100%"><table cellspacing=3 cellpadding=0 width="100%" align=center border=0>
                 <tr valign=bottom>
                   <td width="26%" height=1 colspan=5 bgcolor=#a9cdd9></td>
                 </tr>
                 <tr class="tableCellOne" valign="bottom">
                   <td height="22" align="middle">編號</td>
                   <td align="middle"><div align="center">料號</div></td>
                   <td align="middle"><div align="center">車型</div></td>
                   <td align="middle"><div align="center">詢價產品內容</div></td>
                   <td align="middle"><div align="center">採購數量</div></td>
                 </tr>
                 <tr>
                   <td bgcolor=#a9cdd9 colspan=5 height=1></td>
                 </tr><asp:Literal ID="LitHtml" runat="server"></asp:Literal><tr>
                   <td bgcolor=#a9cdd9 colspan=5 height=1></td>
                 </tr>
                 <tr class=tableCellTwo>
                   <td height="10" colspan="5" align=middle></td>
                  </tr>
               </table>
                 <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3" class="tab_login">
			     <tr>
                   <td width="10%" height="1" bgcolor="#AADDFF"></td>
			       <td height="1" bgcolor="#E1E1E1"></td>
			       </tr>
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：</td>
                       <td class="commontxt3"><asp:Literal ID="LitUserName" runat="server"></asp:Literal></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">公&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;司：</td>
                       <td class="commontxt3"><asp:Literal ID="LitCompanyName" runat="server"></asp:Literal></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">電&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;話：</td>
                       <td class="commontxt3"><asp:Literal ID="LitPhone" runat="server"></asp:Literal></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">傳&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;真：</td>
                       <td class="commontxt3"><asp:Literal ID="LitFax" runat="server"></asp:Literal></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">行動電話：</td>
                       <td class="commontxt3"><asp:Literal ID="LitTel" runat="server"></asp:Literal></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">E - MAIL：</td>
                       <td class="commontxt3"><asp:Literal ID="LitEmail" runat="server"></asp:Literal></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">公司網址：</td>
                       <td class="commontxt3"><asp:Literal ID="LitWebSite" runat="server"></asp:Literal>&nbsp;</td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">業務型態：</td>
                       <td class="commontxt3"><asp:Literal ID="LitCode" runat="server"></asp:Literal></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">訊&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;息：</td>
                       <td class="commontxt3"><asp:Literal ID="LitContent" runat="server"></asp:Literal></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <!--Dynamic Item List End -->
                 </table>
                 <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">

                   <TR>
                     <TD>&nbsp;</TD>
                   </TR>
                   <tr>
                     <td style="padding-left:5px;">&nbsp;&nbsp;<asp:Button ID="BtnBack" runat="server" Text="回上一頁" class="b_1" /></td>
                   </tr>
                 </table>
			   </td>
                 </tr>
                 </table>
				 </td>
                 </tr>
                 </table>
</asp:Panel>               
</asp:Content> 