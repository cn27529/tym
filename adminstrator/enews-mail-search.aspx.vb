Imports System.Data
Imports System.Data.OleDb
Partial Class adminstrator_enews_mail_search
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = Nothing Then
            Response.Write("<script>alert('尚未登入，請先登入!');location.href='adminstrator_login.aspx';</script>")
            Response.End()
        End If
        Dim K, o As Integer
        If Request("SEmail") <> "" Then
            HiddenEmail.Value = Request("SEmail")
        End If
        If Request("K") <> "" Then
            If Not IsNumeric(Request("K")) Then
                K = 0
            Else
                K = Request("k")
            End If
        End If
        If Request("o") <> "" Then
            If Not IsNumeric(Request("o")) Then
                o = 0
            Else
                o = Request("o")
            End If
        End If
        Dim Sql As String
        Sql = "select Email,CompanyName,UserName,SiteName from Inquiry,Site where Inquiry.SiteID=Site.SiteID and IsDel=false"
        If K = 1 Then
            If o = 1 Then
                Sql = Sql & " order by CompanyName Desc"
            Else
                Sql = Sql & " order by CompanyName Asc"
            End If
        ElseIf K = 2 Then
            If o = 1 Then
                Sql = Sql & " order by UserName Desc"
            Else
                Sql = Sql & " order by UserName Asc"
            End If
        ElseIf K = 3 Then
            If o = 1 Then
                Sql = Sql & " order by Inquiry.SiteID Desc"
            Else
                Sql = Sql & " order by Inquiry.SiteID Asc"
            End If
        ElseIf K = 4 Then
            If o = 1 Then
                Sql = Sql & " order by Email Desc"
            Else
                Sql = Sql & " order by Email Asc"
            End If
        End If
        If o = 1 Then
            o = 0
        Else
            o = 1
        End If
        Me.OCompanyName.Text = "<a href=""?k=1&o=" & o & """>公司名</a>"
        Me.OUserName.Text = "<a href=""?k=2&o=" & o & """>聯絡人</a>"
        Me.OSiteID.Text = "<a href=""?k=3&o=" & o & """>來源</a>"
        Me.OEmail.Text = "<a href=""?k=4&o=" & o & """>電子信箱</a>"
        Dim dr As OleDbDataReader = ClassDB.GetDataReader(Sql)
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
            StrHtml += " <td align=middle height=23>" & i & ".</td>" & vbCrLf
            StrHtml += "<td align=middle class=defaultBold>"
            StrHtml += "<input type=""checkbox"" id=""Email"" " & StrCheck & " name=""Email" & i & """ value=""" & dr("Email").ToString & """>"
            StrHtml += "</td>" & vbCrLf
            StrHtml += "<td class=defaultBold style=""PADDING-RIGHT: 0px; PADDING-LEFT: 10px; PADDING-TOP: 6px"">" & dr("CompanyName").ToString & "</td>" & vbCrLf
            StrHtml += "<TD align=middle class=defaultBold style=""PADDING-RIGHT: 0px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px"">" & dr("UserName").ToString & "</TD>" & vbCrLf
            StrHtml += "<TD align=middle>" & dr("SiteName").ToString & "</TD>" & vbCrLf
            StrHtml += "<TD class=defaultBold style=""PADDING-RIGHT: 0px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px"">" & dr("Email").ToString & "</TD>" & vbCrLf
            StrHtml += "<tr>" & vbCrLf
            StrHtml += "<td bgcolor=#cad0d5 colspan=6 height=1></td>" & vbCrLf
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
