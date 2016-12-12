<%@ Page Language="VB" AutoEventWireup="false" CodeFile="adminstrator_login.aspx.vb" Inherits="adminstrator_adminstrator_login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>鑫梅企業股份有限公司</title>
<link href="images/common.css" rel="stylesheet" type="text/css">
<script type="text/JavaScript">
<!--
function MM_openBrWindow(theURL,winName,features) { //v2.0
  window.open(theURL,winName,features);
}

function MM_goToURL() { //v3.0
  var i, args=MM_goToURL.arguments; document.MM_returnValue = false;
  for (i=0; i<(args.length-1); i+=2) eval(args[i]+".location='"+args[i+1]+"'");
}

function ManagerLogin()
{
        if (form1.UID.value=='')
			{
				alert('請輸入帳號');
				form1.UID.focus();
				event.returnValue=false;  
			}			
		else if (form1.PWD.value=='')
			{
				alert('請輸入登入密碼');
				form1.PWD.focus();
				event.returnValue=false;  
			}
		else if (form1.Code.value=='')
			{
				alert('請輸入驗證碼');
				form1.Code.focus();
				event.returnValue=false;  
			}
}
//-->
</script>
</head>

<BODY leftmargin="0px" topmargin="0px">
<form id="form1" runat="server">
<TABLE width=559 height=411 border=0 align="center" cellPadding=0 cellSpacing=0 id=Table_01>
  <TR>
    <TD height="40" colSpan=4>&nbsp;</TD>
  </TR>
  <TR>
    <TD colSpan=4><IMG height=20 alt=tableline src="images/login-m_01.gif" width=559></TD>
  </TR>
  <TR>
    <TD rowSpan=2><IMG height=372 alt=tableline src="images/login-m_02.gif" width=8></TD>
    <TD vAlign=top width=543 colSpan=2 height=108><TABLE cellSpacing=0 cellPadding=0 width="92%" align=center border=0>
        <TR>
          <TD class=title style="PADDING-BOTTOM:5px" vAlign=bottom align=left height=50>管理者登入 Administration Login</TD>
        </TR>
        <TR>
          <TD bgColor=#d8d8d8><IMG height=1 alt=spacer src="images/spacer.gif" width=1></TD>
        </TR>
        <TR>
          <TD class="content11" style="PADDING-TOP:18px" vAlign=top align=left>請輸入管理者帳號、密碼及選擇語系，進入後端管理系統。<br>
          <A href="../aboutus.aspx">回網站首頁</A></TD>
        </TR>
    </TABLE></TD>
    <TD rowSpan=2><IMG height=372 alt=tableline src="images/login-m_04.gif" width=8></TD>
  </TR>
  <TR>
    <TD vAlign=top width=293 height=264><TABLE cellSpacing=0 cellPadding=0 width="93%" align=right border=0>
      <TR>
        <TD colSpan=2 align=left>&nbsp;</TD>
      </TR>
          <TR>
            <TD class="content11" align=left colSpan=2>帳號：</TD>
          </TR>
          <TR>
            <TD align=left colSpan=2>
                <asp:TextBox ID="UID" runat="server" CssClass="tab_1"></asp:TextBox></TD>
          </TR>
          <TR>
            <TD class="content11" style="PADDING-TOP:5px" align=left colSpan=2>密碼：</TD>
          </TR>
          <TR>
            <TD colSpan=2 align=left>
                <asp:TextBox ID="PWD" runat="server" CssClass="tab_1" TextMode="Password"></asp:TextBox></TD>
          </TR>          
          <TR>
            <TD align=left colSpan=2>&nbsp;</TD>
          </TR>
          <TR>
            <TD class="content11" style="PADDING-TOP:5px" vAlign=bottom align=left colSpan=2 height=40>驗證碼：</TD>
          </TR>
          <TR>
            <TD align=left width=100>
            <asp:TextBox ID="Code" runat="server" CssClass="tab_1" Width="90px"></asp:TextBox></TD>
            <TD align=left>
            <asp:Image ID="Image1" runat="server" AlternateText="驗證碼" ImageUrl="~/adminstrator/Common/V_Img.aspx" /></TD>
          </TR>
          <TR>
            <TD colSpan=2><IMG height=10 alt=spacer src="images/spacer.gif" width=1></TD>
          </TR>
          <TR>
            <TD bgColor=#d8d8d8 colSpan=2><IMG height=1 alt=spacer src="images/spacer.gif" width=1></TD>
          </TR>
          <TR>
            <TD style="PADDING-LEFT:0px; PADDING-TOP:10px" colSpan=2><a href="adminstrator_main.asp"></a><asp:ImageButton
                    ID="ImageLog" runat="server" ImageUrl="images/button-login.gif" /></TD>
          </TR>   
    </TABLE>        </TD>
    <TD width=250 height=264><TABLE height=264 cellSpacing=0 cellPadding=0 width="93%" border=0>
        <TR>
          <TD style="PADDING-BOTTOM:20px" vAlign=bottom align=right><IMG height=140 alt=pic src="images/login-m_06.gif" width=106></TD>
        </TR>
    </TABLE></TD>
  </TR>
  <TR>
    <TD colSpan=4><IMG height=19 alt=tableline src="images/login-m_07.gif" width=559></TD>
  </TR>
</TABLE>
</form> 
</BODY>
</html>
