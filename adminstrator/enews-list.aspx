<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/adminstrator/MasterPage.master" Title="鑫梅企業股份有限公司-電子報" CodeFile="enews-list.aspx.vb" Inherits="adminstrator_enews_list"  ValidateRequest="false"%>
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/JavaScript">
<!--
function MM_openBrWindow(theURL,winName,features) { //v2.0
  window.open(theURL,winName,features);
}

function MM_goToURL() { //v3.0
  var i, args=MM_goToURL.arguments; document.MM_returnValue = false;
  for (i=0; i<(args.length-1); i+=2) eval(args[i]+".location='"+args[i+1]+"'");
}
//-->
</script>
<asp:Panel ID="show1" runat="server">
 <TABLE width="100%" border=0 cellPadding=10 cellSpacing=0 class=tableBorder>  
 <TR>
 <TD class=tableHeading>電子報管理</TD>
 </TR>
 <TR>
   <TD class=tableCellTwo>
	<TABLE cellSpacing=0 cellPadding=1 width="100%" align=center border=0>
         <TR>
           <TD>
		   <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1">
             <tr class="tableCellTwo">
               <td align="middle">&nbsp;</td>
               <td width="23%" align="right" class="defaultBold"><asp:Button ID="BtnAdd" CssClass="b_2" runat="server" Text="新增電子報" /></td>
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
                       <asp:TemplateField>
                           <ItemTemplate>
                           <asp:Image ID="Image1" runat="server" ImageUrl="~/adminstrator/images/file_info.gif" />
                           </ItemTemplate>
                           <HeaderStyle Width="6%" HorizontalAlign="Center" />
						   <ItemStyle height="30px" HorizontalAlign="Center"  />	   
					   </asp:TemplateField>
                       <asp:TemplateField HeaderText="標題" SortExpression="Subject">
                           <ItemTemplate>
                           <asp:LinkButton ID="LinkSubject" runat="server" CommandArgument='<%# Bind("EID") %>' CommandName="DataView" Text='<%# Bind("Subject") %>'></asp:LinkButton>
                           </ItemTemplate>
                           <ItemStyle CssClass="defaultBold" HorizontalAlign="Left"  />
                           <HeaderStyle HorizontalAlign="Center" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="處理狀態" SortExpression="Isonline">
                           <ItemTemplate>
                           <asp:Literal ID="LitIsonline" runat="server"></asp:Literal>
                           </ItemTemplate>
                           <HeaderStyle Width="10%" HorizontalAlign="Center" />
                           <ItemStyle CssClass="defaultBold" HorizontalAlign="Center"  />
                       </asp:TemplateField>
                       <asp:BoundField DataField="PostDate" HeaderText="發佈日期" SortExpression="PostDate" >
                           <HeaderStyle HorizontalAlign="Center" />
                           <ItemStyle Width="150" HorizontalAlign="Center"  />
                       </asp:BoundField>
                       <asp:TemplateField>
                           <ItemTemplate>
                           <asp:Button ID="BtnPublish" runat="server" CssClass="b_2" Text="編輯" CausesValidation="False" CommandName="DataUp" CommandArgument='<%# Bind("EID") %>' />
                           <asp:Button ID="btnDel" runat="server" CssClass="b_2" Text="刪除" CausesValidation="False" CommandName="DataDel" CommandArgument='<%# Bind("EID") %>' OnClientClick="return confirm('檔案一但刪除無法回復！');">
                           </asp:Button>
                           </ItemTemplate>
                           <HeaderStyle Width="100" HorizontalAlign="Center" />
                           <ItemStyle CssClass="defaultBold" HorizontalAlign="Center"  />
                       </asp:TemplateField>
                   </Columns>
               </asp:GridView>
               <asp:AccessDataSource ID="SDS_Data" runat="server" DataFile="~/App_Data/Tym.mdb" SelectCommand="select * from epaper order by PostDate Desc"></asp:AccessDataSource>
</TD>
</TR>
</TABLE>
</TD>
</TR>
</TABLE>
</asp:Panel>     
<asp:Panel ID="Show2" runat="server">   
     <TABLE width="100%" border=0 cellPadding=10 cellSpacing=0 class=tableBorder>
     <TR>
     <TD class=tableHeading><asp:Literal ID="LitTitle" runat="server"></asp:Literal></TD>	   
     </TR>
     <tr>
       <td class=tableCellTwo><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
             <tr>
               <td width="100%">
			   <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3" class="tab_login">
			     <tr>
                   <td width="10%" height="1" bgcolor="#AADDFF"></td>
			       <td height="1" bgcolor="#E1E1E1"></td>
			       </tr>
                     <tr>
                       <td height="35" align="center" bgcolor="#F6FFFF">標&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;題：</td>
                       <td class="commontxt3"><asp:TextBox ID="Subject" runat="server" style="width:98%" Cssclass="form"></asp:TextBox></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height="35" align="center" bgcolor="#F6FFFF">寄&nbsp;&nbsp;件&nbsp;&nbsp;者：</td>
                       <td class="commontxt3"><asp:TextBox ID="EmailName" runat="server" style="width:98%" Cssclass="form"></asp:TextBox></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>                    
                     <tr>
                       <td height="35" align="center" bgcolor="#F6FFFF">發送對象：</td>
                       <td class="commontxt3"><asp:TextBox ID="SendEmail" runat="server" TextMode="MultiLine" class="form"  style="width:98%;"></asp:TextBox></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height="35" align="center" bgcolor="#F6FFFF">瀏覽名單：</td>
                       <td class="commontxt3">&nbsp;&nbsp;<a href="#" onClick="MM_openBrWindow('enews-mail-search.aspx?SEmail='+document.aspnetForm.ctl00_ContentPlaceHolder1_SendEmail.value,'','scrollbars=yes,width=800,height=600')">選擇發送對象</a></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height="35" align="center" bgcolor="#F6FFFF">內&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;容：</td>
                       <td class="commontxt3">
                       <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" ToolbarStartExpanded="false" BasePath="Components/Publisher/_vit_cnfs/_vit_cnfs/" Height="300px" Width="98%">
                       </FCKeditorV2:FCKeditor>
                       </td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                       </tr>
                       <tr>
                       <td height="35" align="center" bgcolor="#F6FFFF">增加說明：</td>
                       <td class="commontxt3">					   
				  	   <table width="100%" border="0" cellpadding="0" cellspacing="0">
                       <tr>
                       <td width="340" valign="top"><asp:TextBox ID="TxtProduct" runat="server" CssClass="form" style="width:260px"></asp:TextBox>
                       <asp:Button ID="BtnProductAdd" CssClass="b_3" runat="server" Text="加入說明" />  
                       </td>
                       <td>&nbsp;&nbsp;<a href="#" onClick="MM_openBrWindow('enews-product-search.aspx','','scrollbars=yes,width=800,height=600')">產品明細</a></td>
                       </tr>
                       </table>  
                       </td>
                       </tr>
                       <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <!--Dynamic Item List End -->
                  </table>
                   <asp:HiddenField ID="HiddenPID" runat="server" />
                   <asp:HiddenField ID="DelPID" runat="server" />
                   <asp:HiddenField ID="Act" runat="server" />
			     <table cellspacing=3 cellpadding=0 width="100%" align=center border=0>
                   <tr valign=bottom>
                     <td bgcolor=#a9cdd9 colspan=3 height=1></td>
                   </tr>
                   <tr class=tableCellOne valign=bottom>
                     <td width="6%" height="14" align=middle>編號</td>
                     <td height="20" align=middle>產品名稱</td>
                      <td width="60" align=middle>刪除</td>
                   </tr>                    
                   <tr>
                     <td bgcolor=#a9cdd9 colspan=3 height=1></td>
                   </tr>
                 <asp:Literal ID="LitHtml1" runat="server"></asp:Literal></table>                   
                 <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                   <TR>
                     <TD>&nbsp;</TD>
                   </TR>
                   <tr>
                     <td style="padding-left:5px;">&nbsp;&nbsp;
					 <asp:Button ID="BtnEdit"  class="b_1" runat="server" Text="發送電子報" />&nbsp;&nbsp;                     
					 <asp:Button ID="BtnView1"  class="b_1" runat="server" Text="預覽" />&nbsp;&nbsp;                     
					 <input name="重設" type="reset" class="b_1" id="Reset1" value="重設">&nbsp;&nbsp;
                     <asp:Button ID="Btnback"  class="b_1" runat="server" Text="回上一頁" />
					 </td>
                   </tr>
                 </table>
</TD>
</TR>
</TABLE>
</TD>
</TR>
</TABLE>
   </asp:Panel>             
   <asp:Panel ID="show3" runat="server">
   <TABLE width="100%" border=0 cellPadding=10 cellSpacing=0 class=tableBorder>
     <TR>
     <TD class=tableHeading>電子報內容</TD>	   
     </TR>
     <tr>
       <td class=tableCellTwo><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
             <tr>
               <td width="100%">
			   <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3" class="tab_login">
			     <tr>
                   <td width="10%" height="1" bgcolor="#AADDFF"></td>
			       <td height="1" bgcolor="#E1E1E1"></td>
			       </tr>
                     <tr>
                       <td height="35" align="center" bgcolor="#F6FFFF">標&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;題：</td>
                       <td class="commontxt3"><asp:Literal ID="LitSubject" runat="server"></asp:Literal></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height="35" align="center" bgcolor="#F6FFFF">寄&nbsp;&nbsp;件&nbsp;&nbsp;者：</td>
                       <td class="commontxt3"><asp:Literal ID="LitEmailName" runat="server"></asp:Literal></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height="35" align="center" bgcolor="#F6FFFF">發送對象：</td>
                       <td class="commontxt3"><asp:Literal ID="LitEmail" runat="server"></asp:Literal> </td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height="35" align="center" bgcolor="#F6FFFF">內&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;容：</td>
                       <td class="commontxt3">
                       <asp:Literal ID="LitContent" runat="server"></asp:Literal>
                       </td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <!--Dynamic Item List End -->
                  </table>
                 <table cellspacing=3 cellpadding=0 width="100%" align=center border=0>
                   <tr valign=bottom>
                     <td bgcolor=#a9cdd9 colspan=2 height=1></td>
                   </tr>
                   <tr class=tableCellOne valign=bottom>
                     <td width="6%" height="20" align=middle>編號</td>
                     <td align=middle>產品名稱</td>
                   </tr>
                   <tr>
                     <td bgcolor=#a9cdd9 colspan=2 height=1></td>
                   </tr>                   
                 <asp:Literal ID="LitHtml2" runat="server"></asp:Literal>
				 </table>
                 <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                   <TR>
                     <TD>&nbsp;</TD>
                   </TR>
                   <tr>
                     <td style="padding-left:5px;">&nbsp;&nbsp;
                     <asp:Button ID="BtnView" runat="server"  CssClass="b_1" Text="修改" />&nbsp;&nbsp;
					 <asp:Button ID="BtnBack2"  class="b_1" runat="server" Text="回上一頁" /></td>
                   </tr>
                 </table>
</TD>
</TR>
</TABLE>
</TD>
</TR>
</TABLE>
</asp:Panel>       
<script>
function ProductDel(z)
{
    aspnetForm.ctl00_ContentPlaceHolder1_Act.value='Del';
    aspnetForm.ctl00_ContentPlaceHolder1_DelPID.value=z;
    aspnetForm.submit();
}
</script>
</asp:Content> 