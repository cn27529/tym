<%@ Page Language="VB" AutoEventWireup="false" CodeFile="enews-mail-search.aspx.vb" Inherits="adminstrator_enews_mail_search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>鑫梅企業股份有限公司-電子報明細</title>
    <link href="images/common.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
<TABLE class=tableBorder style="WIDTH: 100%" cellSpacing=0 cellPadding=10 border=0>
  <TR>
    <TD class=tableHeading>主動詢價者名單</TD>
  </TR>
  <TR>
    <TD class=tableCellTwo><TABLE cellSpacing=0 cellPadding=1 width="100%" align=center border=0>
      <TR>
        <TD>
          <table cellspacing=3 cellpadding=0 width="100%" align=center border=0>
                <tr valign=bottom>
                  <td bgcolor=#a9cdd9 colspan=6 height=1></td>
                </tr>
                <tr class=tableCellOne valign=bottom>
                  <td width="6%" height="14" align=middle>編號</td>
                  <td width="6%" align=middle>選擇</td>
                  <td align=middle><asp:Literal ID="OCompanyName" runat="server"></asp:Literal></td>
                  <td width="18%" align=middle><asp:Literal ID="OUserName" runat="server"></asp:Literal></td>
                  <td width="10%" align=middle><asp:Literal ID="OSiteID" runat="server"></asp:Literal></td>
                  <td width="30%" align=middle><asp:Literal ID="OEmail" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                  <td bgcolor=#a9cdd9 colspan=6 height=1></td>
                </tr>
                <asp:Literal ID="LitHtml" runat="server"></asp:Literal>
            </table>
          <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td><TABLE cellSpacing=0 cellPadding=0 width="100%" align=center border=0>
                  <TR>
                    <TD height="4" colspan="2"></TD>
                  </TR>
                  <TR>
                    <TD width="80" style="PADDING-LEFT: 60px"><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="全選" /></TD>
                    <TD><input type="button" name="button1" class="b_2" value=" 確定 " onClick="chk()"></TD>
                  </TR>
              </TABLE></td>
            
            </tr>
          </table></TD>
      </TR>
    </TABLE></TD>
  </TR>
</TABLE>
<asp:HiddenField ID="HiddenEmail" runat="server" />
    </form>
</body>
<script>
function chk()
{
    var selFlag = false;
    var varNum=document.form1.HiddenEmail.value;
    if (document.all("Email").length==undefined)
    {
     if(document.all["Email1"].checked)
     {
       varNum = document.all["Email1"].value;
     }
     else
     {
     	    alert("請至少選擇一個產品");
		    return;
     }     
    }
    else
    {
        for(i=1; i<=document.all("Email").length; i++) {    
		    if(document.all["Email"+i].checked) {
			    selFlag = true;
			    break;
		    }
	    }
	    if(selFlag==false){
		    alert("請至少選擇發送對象");
		    return;
	    }		
	    for(i=1; i<=document.all("Email").length; i++) {    
            if(document.all["Email"+i].checked)
            {
			    if(varNum=="")
				    varNum = document.all["Email"+i].value;
			    else
			        varNum += ";"+document.all["Email"+i].value;	
	        }
	    }
	}
	
	opener.document.aspnetForm.ctl00_ContentPlaceHolder1_SendEmail.value=varNum;
	window.close();
}
</script>    
</html>
