<%@ Page Language="VB"   MasterPageFile="~/adminstrator/MasterPage.master"  AutoEventWireup="false" Title="鑫梅企業股份有限公司" CodeFile="adminstrator_main.aspx.vb" Inherits="adminstrator_adminstrator_main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <TABLE class=tableBorder style="WIDTH: 100%" cellSpacing=0 cellPadding=10 border=0>
              <TR>
              <TD class=tableHeading>管理者登入狀態</TD>
              <TR>
              <TD class=tableCellTwo>
              <H2>系統管理者<BR><SPAN class=content12_blue>最後登入IP :<asp:Literal ID="LitIPAddress" runat="server"></asp:Literal> </SPAN><BR><SPAN class=content12_blue>最後登入 : <asp:Literal ID="LitDate" runat="server"></asp:Literal></SPAN></H2>
              <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
              <TR>
              <TD>&nbsp;</TD></TR>
              <TR>
              <TD class=caldaycellhover>後端管理編輯注意事項 :</TD></TR>
              <TR>
              <TD style="PADDING-TOP: 10px">
                  <UL>
                  <LI>要上傳的檔案或圖檔，檔案名稱不得為中文或含有空白及特殊字元，系統可能會發生辨識錯誤。 
                  <LI>檔名不得重複，否則會互相置換。 
                  <LI>圖片長寬、檔案大小最好符合建議的尺寸，以免系統在縮圖時，會造成圖檔變形。 
                  <LI>HTML編輯時，不能直接從WORD剪貼複製，必須先貼到『筆記本』，確認字體大小，再複製到HTML編輯區；換行不能用Enter，要用 Shift+Enter。<BR><BR>
                  </UL>
              </TD></TR>
			  </TABLE>
		  </TD>
		  </TR>
	      </TABLE>
</asp:Content> 