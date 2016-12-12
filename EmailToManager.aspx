<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmailToManager.aspx.vb" Inherits="EmailToManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>通知管理者</title>
</head>
<body>
姓名：<asp:Literal ID="LitUserName" runat="server"></asp:Literal><br />
公司：<asp:Literal ID="LitCompanyName" runat="server"></asp:Literal><br />
電話：<asp:Literal ID="LitPhone" runat="server"></asp:Literal><br />
傳真：<asp:Literal ID="LitFax" runat="server"></asp:Literal><br />
行動電話：<asp:Literal ID="LitTel" runat="server"></asp:Literal><br />
E - MAIL：<asp:Literal ID="LitEmail" runat="server"></asp:Literal><br />
公司網址：<asp:Literal ID="LitWebSite" runat="server"></asp:Literal><br />
業務型態：<asp:Literal ID="LitCode" runat="server"></asp:Literal><br />
訊息：<asp:Literal ID="LitContent" runat="server"></asp:Literal><br />
<asp:Literal ID="LitData" runat="server"></asp:Literal>

</body>
</html>
