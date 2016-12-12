<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TopWeb.ascx.vb" Inherits="CommonWeb_TopWeb" %>

<script language="javascript" type="text/javascript">
// <!CDATA[

    function menu_5_onclick() {

    }

// ]]>
</script>

<div id="language">
  <table border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="63">
    <a href="aboutus.aspx"><img src="images/bu_1.jpg" alt="繁體版" name="bu_1" width="63" height="26" border="0" id="bu_1" 
	onMouseOver="MM_swapImage('bu_1','','images/buon_1.jpg',1)" onMouseOut="MM_swapImgRestore()"></a></td>
    <td width="66"><a href="English/aboutus.aspx"><img src="images/bu_2.jpg" alt="English" name="bu_2" width="66" height="26" border="0" id="bu_2" 
	onMouseOver="MM_swapImage('bu_2','','images/buon_2.jpg',1)" onMouseOut="MM_swapImgRestore()"></a></td>
  </tr>
</table>
</div>

<div id="sea_1">
<asp:Panel ID="show1"  runat="server" Visible="false">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td height="20">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="32"></td>
            <td>
			    <asp:DropDownList ID="SKind" runat="server" CssClass="c_2" style="width:100px; height:20px"> 
                </asp:DropDownList>
            </td>
          </tr>
        </table></td>
    </tr>
    <tr>
      <td>	  
  <table width="300" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td width="46"><img src="images/sea_1.gif" alt="" /></td>
      <td width="180"><asp:TextBox ID="SKeyWord" runat="server" width="180px" height="20px"></asp:TextBox></td>
      <td>
          <asp:ImageButton ID="ImageSearch" ImageUrl ="../images/sea_2.gif" runat="server" /></td>
    </tr>
  </table>	   	  
	  </td>
    </tr>
  </table>
   </asp:Panel>
 
</div>
<div id="menu">
  <TABLE WIDTH=455 BORDER=0 CELLPADDING=0 CELLSPACING=0>
    <TR>
      <TD><a href="adminstrator/adminstrator_login.aspx" target="_blank"><IMG SRC="images/space.gif" ALT="" HEIGHT=44 width=2 border="0"></a></TD>
      <TD><a href="aboutus.aspx"><IMG SRC="images/menu_1.jpg" ALT="公司簡介" name="menu_1" WIDTH=89 HEIGHT=44 border="0" id="menu_1" onMouseOver="MM_swapImage('menu_1','','images/menuon_1.jpg',1)" onMouseOut="MM_swapImgRestore()"></a></TD>
      <TD><a href="product.aspx"><IMG SRC="images/menu_2.jpg" ALT="產品介紹" name="menu_2" WIDTH=87 HEIGHT=44 border="0" id="menu_2" onMouseOver="MM_swapImage('menu_2','','images/menuon_2.jpg',1)" onMouseOut="MM_swapImgRestore()"></a></TD>
      <TD><a href="news.aspx"><IMG SRC="images/menu_3.jpg" ALT="最新消息" name="menu_3" WIDTH=87 HEIGHT=44 border="0" id="menu_3" onMouseOver="MM_swapImage('menu_3','','images/menuon_3.jpg',1)" onMouseOut="MM_swapImgRestore()"></a></TD>
      <TD><a href="inquiry.aspx"><IMG SRC="images/menu_4.jpg" ALT="詢價清單" name="menu_4" WIDTH=87 HEIGHT=44 border="0" id="menu_4" onMouseOver="MM_swapImage('menu_4','','images/menuon_4.jpg',1)" onMouseOut="MM_swapImgRestore()"></a></TD>
      <TD><a href="mailto:smtruck@gmail.com"><IMG SRC="images/menu_5.jpg" ALT="聯絡我們" name="menu_5" WIDTH=103 HEIGHT=44 border="0" id="menu_5" onMouseOver="MM_swapImage('menu_5','','images/menuon_5.jpg',1)" onMouseOut="MM_swapImgRestore()" onclick="return menu_5_onclick()"></a></TD>
    </TR>
  </TABLE>
</div>
<asp:Literal ID="LitImage" runat="server"></asp:Literal>
<table width="820" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="179" background="images/products_1.jpg">&nbsp;</td>
    <td width="616" height="252" background="images/products_2.jpg">&nbsp;</td>
    <td background="images/products_3.jpg">&nbsp;</td>
  </tr>
</table>

