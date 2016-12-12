<%@ Page Language="VB" AutoEventWireup="false" CodeFile="product_search.aspx.vb" Inherits="English_product_search" %>
<%@ Register Src="CommonWeb/topWeb.ascx" TagName="topWeb" TagPrefix="uc1" %>
<%@ Register Src="CommonWeb/DownWeb.ascx" TagName="DownWeb" TagPrefix="uc1" %>
<%@ Register Src="CommonWeb/Product_left2.ascx" TagName="Product_left" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>GOLDEN ARBUTUS ENTERPRISE CORP.>>Result</title>
<META content="Truck parts,truck parts supplier,Truck Accessories,truck parts manufacturer,truck body parts supplier,truck body parts manufacturer
,truck parts wholesale,Truck bumper,Truck Grille,Truck Door"  name="KeyWords"> 
<META content="GOLDEN ARBUTUS ENTERPRISE CORP was founded in Yang Mei, Taiwan, in 1965. Before 1991, GOLDEN ARBUTUS ENTERPRISE CORP  was called Ta Yang Mei Corp. We are professional engineers at reaper truck, and switched over to truck parts manufacturing. So we know how to make strong good Quality parts for our customers. Our goal is to provide low cost, high quality, parts for our clients. We will make a great partnership."      name="Description">

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
<div class="download_1"><div class="download_2"><a href="http://www.smtrucks.com"><img src="../images/download_1.png" onMouseOver="MM_swapImage('download_btn','','../images/download_2.png',9)" onMouseOut="MM_swapImgRestore()" id="download_btn"></a></div></div>
<!--020515- End-->
<form id="form1" runat="server">
<table width="820" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="252" colspan="3"><uc1:TopWeb ID="TopWeb1" runat="server" />
  </tr>
  <tr>
    <td width="179" valign="top"><uc1:Product_left ID="Product_left2" runat="server" /></td>
    <td width="616" valign="top" bgcolor="#BDE3FF"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="46"><img src="images/bg_1.jpg" alt="" width="46" height="33"></td>
        <td background="images/line_2.jpg"><div class="t12_2">‧home > <a href="product_search.aspx">Product Search</a></div></td>
        <td width="29"><img src="images/bg_2.jpg" alt="" width="29" height="33"></td>
      </tr>
    </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="46"><img src="images/space.gif" alt="" width="46" height="20"></td>
          <td><div class="t12_3">Search result： <span class="m_4"><asp:Literal ID="LitS" runat="server"></asp:Literal></span></div>
		  	  <div class="space_1"><img src="images/space.gif" alt="" width="1" height="1"></div>
		      <div id="contace_2">
			  <asp:Literal ID="LitHtml" runat="server"></asp:Literal>
			  </div>
			   <div id="line_1"><div id="t12_5"><asp:Literal ID="LitPage" runat="server"></asp:Literal></div></div>
			   <div class="space"><img src="images/space.gif" alt="" width="1" height="30"></div>
		  </td>
          <td width="29"><img src="images/space.gif" alt="" width="29" height="20"></td>
        </tr>
      </table>    </td>
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
<script language="javascript">
    function reloadPage()
    { 
    document.form1.submit(); 
    }
    function reloadPage2(z)
    { 	 	
	    document.form1.page.value=z;
	    document.form1.submit(); 
    }		
</script>
</html>
