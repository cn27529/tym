Imports System.Data
Imports System.Data.OleDb
Partial Class adminstrator_enews_product_search
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = Nothing Then
            Response.Write("<script>alert('尚未登入，請先登入!');location.href='adminstrator_login.aspx';</script>")
            Response.End()
        End If
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select PublicID from Product where epaper=true")
        Dim i As Integer = 1
        Dim StrHtml As String
        Dim StrCheck As String
        If CheckBox1.Checked = True Then
            StrCheck = "checked"
        Else
            StrCheck = ""
        End If
        While dr.Read
            StrHtml += " <tr class=tableCellTwo>" & vbCrLf
            StrHtml += "<td height=23 align=middle>" & i & ".</td>" & vbCrLf
            StrHtml += "<td align=middle class=defaultBold>"
            StrHtml += "<input type=""checkbox"" id=""ProductName"" " & StrCheck & " name=""ProductName" & i & """ value=""" & GetProductData(dr("PublicID")) & """>"
            StrHtml += "<input type=""Hidden"" id=""PID"" name=""PID" & i & """ value=""" & dr("PublicID").ToString & """>"
            StrHtml += "</td>" & vbCrLf
            StrHtml += "<td class=defaultBold style=""PADDING-RIGHT: 0px; PADDING-LEFT: 10px; PADDING-TOP: 6px"">" & GetProductData(dr("PublicID")) & "</td>" & vbCrLf
            StrHtml += "</tr>" & vbCrLf
            StrHtml += "<tr>" & vbCrLf
            StrHtml += "<td bgcolor=#cad0d5 colspan=3 height=1></td>" & vbCrLf
            StrHtml += "</tr>" & vbCrLf
            i += 1
        End While
        dr.Close()
        LitHtml.Text = StrHtml
        If i = 1 Or i = 2 Then
            Me.CheckBox1.Visible = False
        Else
            Me.CheckBox1.Visible = True
        End If
    End Sub
End Class
