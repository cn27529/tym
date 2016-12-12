<%@ Page Language="VB" AutoEventWireup="false" CodeFile="enews-product-preview.aspx.vb" Inherits="adminstrator_enews_product_preview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>鑫梅企業股份有限公司</title>
 <asp:Literal ID="LitCss" runat="server"></asp:Literal>
</head>
<body>
<TABLE class=tableBorder style="WIDTH: 100%" cellSpacing=0 cellPadding=10 border=0>
<TR>
<TD class=tableHeading>電子報</TD>
</TR>
<TR>
<TD class=tableCellTwo valign="top" style="PADDING-bottom:20px;">
	<TABLE cellSpacing=0 cellPadding=1 width="100%" align=center border=0 >
      <TR>
        <TD><table width="100%" border="0" align="center" cellpadding="2" cellspacing="1">
          <tr>
            <td><div class="tableHeading1">鑫梅企業股份有限公司 GOLDEN ARBUTUS ENTERPRISE CORP&nbsp;&nbsp;326 No.164, Sec1, ChungShan N. RD,Yangmei,Taoyuang, Taiwan R.O.C.<br />
              桃園縣楊梅鎮中山北路一段164號&nbsp;&nbsp;Tel： +886-3-4782960&nbsp;&nbsp;Fax： +886-3-4758086&nbsp;&nbsp;E-mail：smtruck@gmail.com&nbsp;&nbsp;統編：84360247</div></td>
          </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1">
          <tr>
            <td><asp:Literal ID="LitContent" runat="server"></asp:Literal></td>
            </tr>
        </table>        
        <asp:Panel ID="show1" runat="server">
          <table cellspacing=3 cellpadding=0 width="100%" align=center border=0>
                <tr valign=bottom>
                  <td bgcolor=#a9cdd9 colspan=7 height=1></td>
                </tr>
                <tr class="tableCellOne" valign="bottom">
                  <td width="4%" height="22" align=middle><div align="center">編號</div></td>
                  <td width="140" align="middle"><div align="center">產品圖片</div></td>
                  <td width="12%" align="middle"><div align="center">廠牌 / 料號</div></td>
                  <td width="12%" align="middle"><div align="center">車型 / 年份</div></td>
                  <td width="10%" align="middle"><div align="center">品名</div></td>
                  <td width="14%" align="middle"><div align="center">OEM#</div></td>
                  <td align="middle"><div align="center">產品說明</div></td>
                </tr>
                <tr>
                  <td bgcolor=#a9cdd9 colspan=7 height=1></td>
                </tr>
                <asp:Literal ID="LitHtml" runat="server"></asp:Literal>
            </table>
            </asp:Panel>
		  </TD>
      </TR>
    </TABLE>
</TD>
</TR>
</TABLE>
</body>
</html>
