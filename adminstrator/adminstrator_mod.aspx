<%@ Page Language="VB"   MasterPageFile="~/adminstrator/MasterPage.master"  AutoEventWireup="false" Title="鑫梅企業股份有限公司" CodeFile="adminstrator_mod.aspx.vb" Inherits="adminstrator_adminstrator_mod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/JavaScript">
function Chk()
{
        if (aspnetForm.ctl00_ContentPlaceHolder1_OLDPWD.value=='')
			{
				alert('請輸入舊密碼');
				aspnetForm.ctl00_ContentPlaceHolder1_OLDPWD.focus();
				event.returnValue=false;  
			}			
		else if (aspnetForm.ctl00_ContentPlaceHolder1_PWD1.value=='')
			{
				alert('請輸入新密碼');
				aspnetForm.ctl00_ContentPlaceHolder1_PWD1.focus();
				event.returnValue=false;  
			}
		else if (aspnetForm.ctl00_ContentPlaceHolder1_PWD2.value=='')
			{
				alert('請輸入密碼確認');
				aspnetForm.ctl00_ContentPlaceHolder1_PWD2.focus();
				event.returnValue=false;  
			}			
		else if (aspnetForm.ctl00_ContentPlaceHolder1_PWD1.value!=aspnetForm.ctl00_ContentPlaceHolder1_PWD2.value)
			{
				alert('密碼與確認密碼不同');
				aspnetForm.ctl00_ContentPlaceHolder1_PWD2.focus();
				event.returnValue=false;  
			}		
}
</script>
<table class="tableBorder" style="WIDTH: 100%" cellspacing="0" cellpadding="10" border="0">
  <tr>
    <td class="tableHeading">管理者變更密碼</td>
  </tr>
  <tr>
    <td class="tableCellTwo"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">

      <tr>
        <td height="10"></td>
      </tr>
      <tr>
        <td class="content11" style="PADDING-TOP:5px" align="left">請輸入舊密碼：</td>
      </tr>
      <tr>
        <td><asp:TextBox ID="OLDPWD" runat="server" CssClass="tab_1" Width="152px" TextMode="Password"></asp:TextBox></td>
      </tr>
      <tr>
        <td height="20"></td>
      </tr>
      <tr>
        <td class="content11" style="PADDING-TOP:5px" align="left"><span class="content11" style="PADDING-TOP:5px">請輸入新</span>密碼：</td>
      </tr>
      <tr>
        <td align="left"><asp:TextBox ID="PWD1" runat="server" CssClass="tab_1" Width="152px" TextMode="Password"></asp:TextBox></td>
      </tr>
      <tr>
        <td class="content11" style="PADDING-TOP:5px" align="left"><span class="content11" style="PADDING-TOP:5px">再次輸入新</span>密碼：</td>
      </tr>
      <tr>
        <td align="left"><asp:TextBox ID="PWD2" runat="server" CssClass="tab_1" Width="152px" TextMode="Password"></asp:TextBox></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        </tr>
      <tr>
        <td bgcolor="#d8d8d8"><img height="1" alt="spacer" src="images/spacer.gif" width="1" /></td>
      </tr>
      <tr>
        <td style="PADDING-TOP:10px"><asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="images/button-logina.gif" /></td>
      </tr>
    </table></td>
  </tr>
</table>
</asp:Content> 