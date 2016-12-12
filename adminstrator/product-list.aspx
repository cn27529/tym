<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/adminstrator/MasterPage.master" Title="鑫梅企業股份有限公司-產品管理" CodeFile="product-list.aspx.vb" Inherits="adminstrator_product_list" ValidateRequest="false"%>
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="show1" runat="server">
 <TABLE width="100%" border=0 cellPadding=10 cellSpacing=0 class=tableBorder>  
 <TR>
 <TD class=tableHeading>產品管理</TD>
 </TR>
 <TR>
   <TD class=tableCellTwo>
	<TABLE cellSpacing=0 cellPadding=1 width="100%" align=center border=0>
         <TR>
           <TD>
             <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1">
             <tr class="tableCellTwo">
                 <td align="right" class="defaultBold">
                     產品料號：<asp:TextBox ID="TxtS" runat="server"></asp:TextBox>
                     <asp:Button ID="BtnSearch" runat="server" Text="搜尋" CssClass="b_2" />
                     <asp:Button ID="BtnClear" runat="server" Text="清除" CssClass="b_2" />
                 </td>
               </tr>
               <tr class="tableCellTwo">
                 <td align="right" class="defaultBold">
                     <asp:Button ID="BtnAdd1" runat="server" Text="新增目錄" CssClass="b_2" />
                     <asp:Button ID="BtnAdd2" runat="server" Text="新增產品" CssClass="b_2" />
                     <asp:Button ID="BtnBack" runat="server" Text="回上一層" CssClass="b_2" />
                     <asp:Button ID="BtnOrder" runat="server" Text="排序" CssClass="b_2" />
                 </td>
               </tr>
             </table>
             <asp:GridView ID="GridView1" runat="server" cellspacing=1 cellpadding=2 width="100%" bgcolor=#a9cdd9 align=center border=0 AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SDS_Data" PageSize="20">
                    <Columns>
                       <asp:TemplateField HeaderText="編號" >
                           <ItemTemplate>
                           <asp:Literal ID="LitNum" runat="server"></asp:Literal>
                           </ItemTemplate>
                           <HeaderStyle Width="6%" height="24px" HorizontalAlign="Center" />
                           <ItemStyle height="30px" HorizontalAlign="Center" />
                       </asp:TemplateField>
                       <asp:TemplateField>
                           <ItemTemplate>
                           <asp:Literal ID="LitImg" runat="server"></asp:Literal>
                           </ItemTemplate>
                           <HeaderStyle Width="6%" HorizontalAlign="Center" />
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:TemplateField>
                         <asp:BoundField DataField="SiteName" HeaderText="網站" SortExpression="SiteName" >
                           <HeaderStyle Width="10%" HorizontalAlign="Center" />
                           <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                       <asp:TemplateField HeaderText="產品名稱" SortExpression="Subject">
                           <ItemTemplate>
                           <asp:LinkButton ID="LinkSubject" runat="server" CommandName="BtnView"></asp:LinkButton>
                           </ItemTemplate>
                           <ItemStyle CssClass="defaultBold" HorizontalAlign="Left" />
                           <HeaderStyle HorizontalAlign="Center" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="上線" SortExpression="Isonline">
                           <ItemTemplate>
                           <asp:Literal ID="LitIsonline" runat="server"></asp:Literal>
                           </ItemTemplate>
                           <HeaderStyle Width="6%" HorizontalAlign="Center" />
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:TemplateField>
                       <asp:TemplateField>
                           <ItemTemplate>
                           <asp:Button ID="BtnPublish" runat="server" class="b_2" Text="編輯" CausesValidation="False" CommandName="BtnProduct" />
                           <asp:Button ID="btnDel" runat="server" class="b_2" Text="刪除" CausesValidation="False" CommandName="ProductDelete" CommandArgument='<%# Bind("PublicID") %>' OnClientClick="return confirm('您確定要刪除這筆資料？資料刪除無法回復！');">
                           </asp:Button>
                           <asp:Button ID="BtnEpaper" runat="server" class="b_2" Text="加入電子報" CausesValidation="False" CommandName="AddEpaper" />
                           </ItemTemplate>
                           <HeaderStyle Width="200px" HorizontalAlign="Center" />
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
<asp:Panel ID="Show2" runat="server">
<TABLE class=tableBorder style="WIDTH: 100%" cellSpacing=0 cellPadding=10 border=0>
     <TR>
     <TD class=tableHeading><asp:Literal ID="LitTitle" runat="server"></asp:Literal></TD>	 
     </TR>
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
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="&nbsp;&nbsp;請選擇上稿網站！" ControlToValidate="SiteID" ValidationGroup="2"></asp:RequiredFieldValidator>
			       </td>
		         </tr>
			     
			     <tr>
                   <td width="10%" height="1" bgcolor="#AADDFF"></td>
			       <td height="1" bgcolor="#E1E1E1"></td>
		         </tr>
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">目錄名稱：</td>
                       <td class="commontxt3">
                   <asp:TextBox ID="Subject" runat="server" style="width:98%" Cssclass="form" ></asp:TextBox>
                   <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="&nbsp;&nbsp;請輸入標題！" ControlToValidate="Subject" ValidationGroup="2"></asp:RequiredFieldValidator>
                       </td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                   <asp:Panel ID="ShowFile" runat="server">
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">目錄小圖：</td>
                       <td class="commontxt3">
					       <table width="100%" border="0" cellpadding="0" cellspacing="0">
                           <tr>
                           <td width="340" valign="top">
                               <asp:FileUpload ID="FileUpload1" CssClass="form" style="width:98%" runat="server" />
                               <asp:Literal ID="LitFile1" runat="server"></asp:Literal>
                           </td>                       
                           <td>* 格式為 jpg / gif ，圖形限制：寬度 128 像素，大小 50K 以內。</td>
                           </tr>
                           </table>
						   </td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
                     <tr>
                       <td height=35 align="center" bgcolor="#F6FFFF">目錄大圖：</td>
                       <td class="commontxt3"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                           <tr>
                             <td width="340" valign="top">
                             <asp:FileUpload ID="FileUpload2" CssClass="form" style="width:98%" runat="server" />
                             <asp:Literal ID="LitFile2" runat="server"></asp:Literal>
                             </td>
                             <td>* 格式為 jpg / gif ，圖形限制：寬度 500 像素，大小 100K 以內。</td>
                           </tr>
                       </table></td>
                     </tr>
                     <tr>
                       <td height="1" bgcolor="#AADDFF"></td>
                       <td height="1" bgcolor="#E1E1E1"></td>
                     </tr>
              </asp:Panel>                     
                     <tr >
                       <td height="35" bgcolor="#F6FFFF" align="center">發佈 / 上架</td>
                       <td ><asp:CheckBox ID="Isonline" runat="server" Text="上架" /></td>
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
                     <td style="padding-left:5px;">&nbsp;&nbsp;<asp:Button ID="BtnEdit1"  class="b_1" runat="server" Text="確認送出" ValidationGroup="2" />&nbsp;&nbsp;
                       <input name="重設" type="reset" class="b_1" value="重設" />&nbsp;&nbsp;
                       <asp:Button ID="BtnBack2"  class="b_1" runat="server" Text="回上一頁"  />
                     </td>
                   </tr>
                 </table>
				 </td>
                 </tr>
                 </table>
				 </td>
                 </tr>
                 </table>
</asp:Panel>
<asp:Panel ID="show3" runat="server">
<TABLE class=tableBorder style="WIDTH: 100%" cellSpacing=0 cellPadding=10 border=0>
     <TR>
     <TD class=tableHeading><asp:Literal ID="LitTitle2" runat="server"></asp:Literal></TD>	   
     </TR>
     <tr>
       <td class=tableCellTwo><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
             <tr>
               <td width="100%"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="3" class="tab_login">
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr >
                   <td height="35" bgcolor="#F6FFFF" align="center">上稿網站：</td>
                   <td ><asp:DropDownList ID="SiteID2" CssClass="location" style="width:100px;" runat="server" Enabled="false"></asp:DropDownList></td>
                 </tr>
                 <tr>
                   <td width="10%" height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">產品料號：</td>
                   <td class="commontxt3"><asp:TextBox ID="ProductNo" CssClass="form" style="width:98%" runat="server" ></asp:TextBox></td>
                 </tr>
                 <tr>
                   <td bgcolor="#AADDFF" style="height: 1px"></td>
                   <td bgcolor="#E1E1E1" style="height: 1px"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">產品名稱：</td>
                   <td class="commontxt3"><asp:TextBox ID="ProductName" CssClass="form" style="width:98%" runat="server"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ProductName" Display="Dynamic" ErrorMessage="請輸入產品名稱！" validationgroup="3"></asp:RequiredFieldValidator></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">英文名稱：</td>
                   <td class="commontxt3"><asp:TextBox ID="ProductENName" CssClass="form" runat="server" style="width:98%"></asp:TextBox></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;份：</td>
                   <td class="commontxt3"><asp:TextBox ID="ProductYear" CssClass="form" runat="server" style="width:98%"></asp:TextBox></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">車&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;型：</td>
                   <td class="commontxt3"><asp:TextBox ID="ProductCar" CssClass="form" runat="server" style="width:98%"></asp:TextBox></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">廠&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;牌：</td>
                   <td class="commontxt3"><asp:TextBox ID="ProductSubject" CssClass="form" runat="server" style="width:98%"></asp:TextBox></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">數&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;量：</td>
                   <td class="commontxt3"><asp:TextBox ID="Q1" CssClass="form" runat="server" style="width:10%" ></asp:TextBox>
                     &nbsp;&nbsp;
                     <asp:DropDownList ID="Q1NO" runat="server" CssClass="location" style="width:100px;"></asp:DropDownList>
                   </td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">材&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;數：</td>
                   <td class="commontxt3"><asp:TextBox ID="Q2" CssClass="form" runat="server" style="width:10%" ></asp:TextBox>
                     &nbsp;&nbsp;
                     <asp:DropDownList ID="Q2NO" runat="server" CssClass="location" style="width:100px;"></asp:DropDownList>
                   </td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">淨&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;重：</td>
                   <td class="commontxt3"><asp:TextBox ID="Q3" CssClass="form" runat="server" style="width:10%" ></asp:TextBox>
                     &nbsp;&nbsp;
                     <asp:DropDownList ID="Q3NO" runat="server" CssClass="location" style="width:100px;"></asp:DropDownList>
                   </td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">毛&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;重：</td>
                   <td class="commontxt3"><asp:TextBox ID="Q4" CssClass="form" runat="server" style="width:10%" ></asp:TextBox>
                     &nbsp;&nbsp;
                     <asp:DropDownList ID="Q4NO" runat="server" CssClass="location" style="width:100px;"></asp:DropDownList>
                   </td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">原廠料號：</td>
                   <td class="commontxt3"><asp:TextBox ID="Raw" CssClass="form" runat="server" style="width:98%"></asp:TextBox></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">產品說明：</td>
                   <td class="commontxt3"><FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" ToolbarStartExpanded="false" BasePath="Components/Publisher/_vit_cnfs/_vit_cnfs/" Height="300px" Width="98%"> </FCKeditorV2:FCKeditor></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">功能說明：</td>
                   <td class="commontxt3"><FCKeditorV2:FCKeditor ID="FCKeditor2" runat="server" ToolbarStartExpanded="false" BasePath="Components/Publisher/_vit_cnfs/_vit_cnfs/" Height="300px" Width="98%"> </FCKeditorV2:FCKeditor></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">產品小圖：</td>
                   <td class="commontxt3"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                       <tr>
                         <td width="340" valign="top"><asp:FileUpload ID="FileUpload3" CssClass="form" style="width:98%" runat="server" />
                         <asp:Literal ID="LitFile3" runat="server"></asp:Literal>
                         </td>
                         <td>* 格式為 jpg / gif ，圖形限制：寬度 120 像素，大小 50K 以內。</td>
                       </tr>
                   </table></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">產品大圖：</td>
                   <td class="commontxt3"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                       <tr>
                         <td width="340" valign="top"><asp:FileUpload ID="FileUpload4" CssClass="form" style="width:98%" runat="server" />
                             <asp:Literal ID="LitFile4" runat="server"></asp:Literal>
                         </td>
                         <td>* 格式為 jpg / gif / swf，圖形限制：寬度 400 像素，大小 100K 以內。</td>
                       </tr>
                   </table></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF">產品詳圖：</td>
                   <td class="commontxt3"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                       <tr>
                         <td width="340" valign="top"><asp:FileUpload ID="FileUpload5" CssClass="form" style="width:98%" runat="server" />
                             <asp:Literal ID="LitFile5" runat="server"></asp:Literal>
                         </td>
                         <td>* 格式為 jpg / gif / swf，圖形限制：大小 200K 以內。</td>
                       </tr>
                   </table></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF" >檔案名稱：</td>
                   <td class="commontxt3"><asp:TextBox ID="FileTitle" runat="server" CssClass="form" style="width:98%"></asp:TextBox></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr>
                   <td height=35 align="center" bgcolor="#F6FFFF" >檔案下載：</td>
                   <td class="commontxt3"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                       <tr>
                         <td width="340" valign="top"><asp:FileUpload ID="FileUpload6" CssClass="form" style="width:98%" runat="server" />
                             <asp:Literal ID="LitFile6" runat="server"></asp:Literal></td>
                         <td>&nbsp;</td>
                       </tr>
                   </table></td>
                 </tr>
                 <tr>
                   <td height="1" bgcolor="#AADDFF"></td>
                   <td height="1" bgcolor="#E1E1E1"></td>
                 </tr>
                 <tr >
                   <td height="35" bgcolor="#F6FFFF" align="center">發佈 / 上架</td>
                   <td ><asp:CheckBox ID="Isonline2" runat="server" Text="上架" /></td>
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
                     <td style="padding-left:5px;">&nbsp;&nbsp;<asp:Button ID="BtnEdit2"  class="b_1" runat="server" Text="確認送出" ValidationGroup="3" />&nbsp;&nbsp;
                       <input name="重設" type="reset" class="b_1" value="重設">&nbsp;&nbsp;
                       <asp:Button ID="BtnBack3"  class="b_1" runat="server" Text="回上一頁"  />                     </td>
                   </tr>
                 </table>
			    </td>
                 </tr>
                 </table>
				 </td>
                 </tr>
                 </table>
</asp:Panel>                      
<asp:Panel ID="Show4" runat="server">
<table class="tableBorder" style="WIDTH: 100%" cellspacing="0" cellpadding="10" border="0">
  <tr>
  <td class="tableHeading">產品管理</td>
  </tr>
  <tr>
  <td class="tableCellTwo">
           <asp:GridView id="GridView2" runat="server" cellspacing=1 cellpadding=2 width="100%" align=center border=1 DataSourceID="Sds_Data2" DataKeyNames="PublicID" AutoGenerateColumns="False">
           <Columns>
           <asp:TemplateField>
           <ItemTemplate>
           <asp:Literal ID="LitImg2" runat="server"></asp:Literal>
           <asp:HiddenField ID="HiddenPublicID" runat="server" />
           </ItemTemplate>
           <HeaderStyle Width="6%" height="24px" HorizontalAlign="Center" />
           <ItemStyle height="32px" HorizontalAlign="Center" />
           </asp:TemplateField>                       
           <asp:TemplateField HeaderText="產品名稱" SortExpression="Subject">
           <ItemTemplate>
           <asp:Literal ID="LitGridView2Subject" runat="server"></asp:Literal>
           </ItemTemplate>
           <ItemStyle CssClass="defaultBold" HorizontalAlign="Left" />
           <HeaderStyle HorizontalAlign="Center" />
           </asp:TemplateField>                       
           <asp:TemplateField HeaderText="排序">
           <ItemTemplate>
           <asp:TextBox ID="TxtOrderData" runat="server" Width="40px" />
           </ItemTemplate> 
           <ItemStyle CssClass="defaultBold" HorizontalAlign="Center" />
           <HeaderStyle Width="10%" HorizontalAlign="Center" />   
           </asp:TemplateField>
           </Columns>
           </asp:GridView>            
           <asp:AccessDataSource ID="Sds_Data2" runat="server" DataFile="~/App_Data/Tym.mdb">
           </asp:AccessDataSource>           
           <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
           <TR>
           <TD>&nbsp;</TD>
           </TR>
           <tr>
           <td style="padding-left:5px;">&nbsp;&nbsp;<asp:Button ID="BtnEditOrder"  class="b_1" runat="server" Text="確認送出" />&nbsp;&nbsp;
           <input name="重設" type="reset" class="b_1" value="重設">&nbsp;&nbsp;
            <asp:Button ID="BtnBack4"  class="b_1" runat="server" Text="回上一頁"  />
  </td>
  </tr>
  </table>
  </td>
  </tr>
  </table>
  </asp:Panel>
</asp:Content> 