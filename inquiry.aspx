<%@ Page Language="VB" AutoEventWireup="false" CodeFile="inquiry.aspx.vb" Inherits="inquiry" %>
<%@ Register Src="CommonWeb/topWeb.ascx" TagName="topWeb" TagPrefix="uc1" %>
<%@ Register Src="CommonWeb/DownWeb.ascx" TagName="DownWeb" TagPrefix="uc1" %>
<%@ Register Src="CommonWeb/inquiry_left.ascx" TagName="inquiry_left" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>鑫梅企業股份有限公司>>詢價清單</title>
<link href="images/css.css" rel="stylesheet" type="text/css">
<SCRIPT src="images/jscript.js" type=text/javascript></SCRIPT>
<script type="text/JavaScript">
<!--
function MM_openBrWindow(theURL,winName,features) { //v2.0
  window.open(theURL,winName,features);
}
function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
//-->
</script>
</head>
<body leftmargin="0" topmargin="0" onLoad="MM_preloadImages('images/buon_1.jpg','images/buon_2.jpg','images/menuon_1.jpg','images/menuon_2.jpg','images/menuon_3.jpg','images/menuon_4.jpg','images/menuon_5.jpg')">
<!--020515- Start-->
<div class="download_1"><div class="download_2"><a href="http://www.smtrucks.com"><img src="images/download_1.png" onMouseOver="MM_swapImage('download_btn','','images/download_2.png',9)" onMouseOut="MM_swapImgRestore()" id="download_btn"></a></div></div>
<!--020515- End-->
<form id="form1" runat="server">
<table width="820" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="252" colspan="3"><uc1:TopWeb ID="TopWeb1" runat="server" />
  </tr>
  <tr>
    <td width="179" valign="top"><uc1:inquiry_left ID="inquiry_left1" runat="server" /></td>
    <td width="616" valign="top" bgcolor="#BDE3FF"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="46"><img src="images/bg_1.jpg" alt="" width="46" height="33"></td>
        <td background="images/line_2.jpg"><div class="t12_2">‧首頁 > <a href="inquiry.aspx">詢價清單</a></div></td>
        <td width="29"><img src="images/bg_2.jpg" alt="" width="29" height="33"></td>
      </tr>
    </table>
    <asp:Panel ID="show1" runat="server">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="46"><img src="images/space.gif" alt="" width="46" height="20"></td>
          <td>
		  <div class="t12_3">詢價清單</div>	
		  <asp:Panel ID="ShowStatus1" runat="server">
		  <div id="line_3_1"><img src="images/line_4.jpg" alt="" width="510" height="1"></div>		  
		  <div id="line_6" style="display:"><div class="c_3_1">您尚未點選任何商品。</div></div> 
		  </asp:Panel>
		  <asp:Panel ID="ShowStatus2" runat="server">
		  <div id="line_6">
		  <div id="group_1">
          <asp:GridView id="GridView1" width="500px" border="1" cellpadding="0" cellspacing="2" bordercolor="#BDE3FF" CssClass="c_3" runat="server" DataSourceID="Sds_Data" DataKeyNames="PublicID" AutoGenerateColumns="False"><Columns>
               <asp:TemplateField HeaderText="產品圖片">
               <ItemTemplate>
               <asp:Literal ID="LitImg2" runat="server"></asp:Literal>
               <asp:HiddenField ID="HiddenPublicID" runat="server" />
               </ItemTemplate>
               <HeaderStyle Width="80px" height="28px" BackColor="#B5DFFF" HorizontalAlign="Center" />
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" BackColor="#CEEAFF" />
               </asp:TemplateField>                                 
               <asp:BoundField HeaderText="詢價清單內容" SortExpression="ProductName" DataField="ProductName"  >
               <ItemStyle BackColor="#CEEAFF" HorizontalAlign="Center" height="24px" />
               </asp:BoundField>
               <asp:TemplateField HeaderText="採購數量"><ItemTemplate>
               <asp:TextBox ID="TxtQ1" runat="server" style="width:50px; height:12px">
               </asp:TextBox>
               </ItemTemplate>
               <HeaderStyle Width="80px" BackColor="#B5DFFF" HorizontalAlign="Center" />    
               <ItemStyle BackColor="#CEEAFF" HorizontalAlign="Center" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="刪除"><ItemTemplate>
               <asp:Button ID="BtnDel" runat="server" class="b_2" Text="刪除" CommandName="DelData" />
               </ItemTemplate>
               <HeaderStyle Width="60px" BackColor="#B5DFFF" HorizontalAlign="Center" />    
               <ItemStyle BackColor="#CEEAFF" HorizontalAlign="Center" />
               </asp:TemplateField>
               </Columns>
               </asp:GridView>    		  		
               <asp:AccessDataSource ID="Sds_Data" runat="server" DataFile="~/App_Data/Tym.mdb">
               </asp:AccessDataSource> 	   
		  </div>
		  </div>
		  </asp:Panel>
		  
		  <div id="line_3"><img src="images/line_4.jpg" alt="" width="510" height="1"></div>  
		  <div>
		  <div align="center">
          <asp:Button ID="BtnEdit" runat="server" class="b_1" Text="確認表單" />&nbsp;&nbsp;&nbsp;&nbsp;
		  <asp:Button ID="BtnDel" runat="server" class="b_1" Text="全部刪除" />&nbsp;&nbsp;&nbsp;&nbsp;
		  <asp:Button ID="BtnGoProduct" runat="server" class="b_1" Text="加入其他商品" />		  
		  </div>
		  </div>
		  </td>
          <td width="29"><img src="images/space.gif" alt="" width="29" height="20"></td>
        </tr>
      </table>
      </asp:Panel>      
      </td>
    <td valign="top"><img src="images/products_6.jpg" width="25" height="483" alt="" /></td>
  </tr>
  <tr>
    <td colspan="3"><uc1:DownWeb ID="DownWeb1" runat="server" /></td>
  </tr>
</table></form>
<script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ?
"https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost +
"google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
try {
var pageTracker = _gat._getTracker("UA-7310952-1");
pageTracker._trackPageview(); } catch(err) {}</script>
</body>
</html>
