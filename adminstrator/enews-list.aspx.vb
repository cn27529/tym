Imports System.Data
Imports System.Data.OleDb
Partial Class adminstrator_enews_list
    Inherits System.Web.UI.Page
    Dim UploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString
#Region "GridView1"
    Protected Sub GridView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBound
        GridViewPageInfo.GetGridViewInfo(Me.GridView1, Me.Page, SDS_Data)
    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName = "DataDel" Then
            ClassDB.UpdateDB("Delete from epaper where EID=" & e.CommandArgument & "")
            ClassDB.UpdateDB("Delete from epaperRelationProduct where EID=" & e.CommandArgument & "")
            Response.Redirect("enews-list.aspx")
        End If
        If e.CommandName = "DataView" Then
            Dim EID As Integer = CInt(e.CommandArgument)
            Session("EID") = EID
            ShowProcess(3)
            ShowData2()
        End If
        If e.CommandName = "DataUp" Then
            Dim EID As Integer = CInt(e.CommandArgument)
            Session("EID") = EID
            ShowProcess(2)
            ShowData()
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = 0 OrElse e.Row.RowState = 1 Then
                Dim drowView As System.Data.DataRowView = CType(e.Row.DataItem, System.Data.DataRowView)
                Dim RowIndex As Integer = e.Row.RowIndex
                Dim LitNum As Literal = CType(e.Row.FindControl("LitNum"), Literal)
                LitNum.Text = GridView1.PageIndex * GridView1.PageSize + RowIndex + 1
                Dim LitIsonline As Literal = CType(e.Row.FindControl("LitIsonline"), Literal)
                If drowView.Item("ISsend").ToString = True Then
                    LitIsonline.Text = "<SPAN class=searchpdno>已發送</SPAN>"
                Else
                    LitIsonline.Text = "未發送"
                End If
            End If
        End If
    End Sub
#End Region

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        HiddenPID.Value = ""
        Subject.Text = ""
        EmailName.Text = ""
        SendEmail.Text = ""
        Me.FCKeditor1.Value = ""
        TxtProduct.Text = ""
        LitHtml1.Text = ""
        ShowProcess(2)
    End Sub

    Private Sub ShowProcess(ByVal k As Integer)
        If Session("Result") <> Nothing Then
            Misc.AlertMsg(Page, Session("Result"))
            Session("Result") = Nothing
        End If
        show1.Visible = False
        Show2.Visible = False
        show3.Visible = False
        If k = 1 Then
            show1.Visible = True
        ElseIf k = 2 Then
            Show2.Visible = True
        ElseIf k = 3 Then
            show3.Visible = True
        End If
    End Sub

    Protected Sub BtnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEdit.Click       
        If Subject.Text = "" Then
            Misc.AlertMsg(Page, "請輸入標題")
            Exit Sub
        End If
        '判斷Email
        Dim ArrEmail() As String = SendEmail.Text.Split(";")
        Dim i As Integer
        For i = 0 To UBound(ArrEmail)
            If ArrEmail(i) <> "" Then
                If IsValidEmail(ArrEmail(i)) = False Then
                    Misc.AlertMsg(Page, "收信者Email錯誤")
                    Exit Sub
                End If
            End If
        Next
        Dim Sql As String
        If Session("EID") <> Nothing Then
            Sql = "update epaper set Subject='" & Subject.Text.Replace("'", "&#39;") & "',EmailName='" & EmailName.Text.Replace("'", "&#39;") & "'"
            Sql = Sql & ",content='" & FCKeditor1.Value.Replace("'", "&#39;") & "',SendEmail='" & SendEmail.Text & "'"
            Sql = Sql & ",PostDate='" & Now & "',UserID=" & Session("UserID") & ""
            Sql = Sql & " where EID=" & Session("EID") & ""
            Session("Result") = "修改電子報並發送成功"
            ClassDB.UpdateDB("delete from epaperRelationProduct where PublicID=" & Session("EID") & "")
        Else
            Sql = "insert into epaper(subject,EmailName,Content,SendEmail,PostDate,UserID,ISsend)values('" & Subject.Text.Replace("'", "&#39;") & "',"
            Sql = Sql & "'" & EmailName.Text.Replace("'", "&#39;") & "','" & FCKeditor1.Value.Replace("'", "&#39;") & "',"
            Sql = Sql & "'" & SendEmail.Text & "','" & Now & "'," & Session("UserID") & ",1)"
            Session("Result") = "新增電子報並發送成功"
        End If
        ClassDB.UpdateDB(Sql)
        ClassDB.UpdateDB("update product set epaper=false")
        '取得EID
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select Max(EID) as MaxEID from epaper")
        If dr.read Then
            Session("EID") = dr("MaxEID").ToString
        End If
        dr.Close()
        '刪除產品關連
        ClassDB.UpdateDB("Delete from epaperRelationProduct where EID=" & Session("EID") & "")
        '新增產品
        If HiddenPID.Value <> "" Then
            Dim ArrPID() As String = HiddenPID.Value.Split(";")
            For i = 0 To UBound(ArrPID)
                ClassDB.UpdateDB("insert into epaperRelationProduct(EID,PublicID)values(" & Session("EID") & "," & ArrPID(i) & ")")
            Next
        End If
        '寄信
        Dim MailBody As String = GetMailBody("adminstrator/enews-product-preview.aspx?EID=" & Session("EID") & "")
        Dim ArrSendEmail() As String = SendEmail.Text.Split(";")
        For i = 0 To UBound(ArrSendEmail)
            SendMail(Subject.Text, MailBody, ArrSendEmail(i), EmailName.Text, , True)
        Next
        Session("EID") = Nothing
        Response.Redirect("enews-list.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Act.Value = "Del" Then
            Dim i As Integer
            Dim PIDs As String
            Dim ArrPID() As String = HiddenPID.Value.Split(";")
            HiddenPID.Value = ""
            For i = 0 To UBound(ArrPID)
                If DelPID.Value <> ArrPID(i) Then
                    If PIDs = "" Then
                        PIDs = ArrPID(i)
                    Else
                        PIDs = PIDs & ";" & ArrPID(i)
                    End If
                End If
            Next
            HiddenPID.Value = PIDs
            LitHtml1.Text = GetProductHtml(Me.HiddenPID.Value)
            Act.Value = ""
        End If
        If Not IsPostBack Then
            ShowProcess(1)
        End If
    End Sub
#Region "顯示資料"
    Private Sub ShowData()
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select * from Epaper where EID=" & Session("EID") & "")

        If dr.Read Then
            Subject.Text = dr("Subject").ToString
            EmailName.Text = dr("EmailName").ToString
            Me.FCKeditor1.Value = dr("Content").ToString
            SendEmail.Text = dr("SendEmail").ToString
        End If
        dr.Close()
        dr = ClassDB.GetDataReader("select Product.PublicID from epaperRelationProduct,Product where Product.PublicID=epaperRelationProduct.PublicID and EID=" & Session("EID") & "")
        While dr.Read
            If HiddenPID.Value = "" Then
                HiddenPID.Value = dr("PublicID")
            Else
                HiddenPID.Value = HiddenPID.Value & ";" & dr("PublicID")
            End If
        End While
        dr.Close()
        LitHtml1.Text = GetProductHtml(Me.HiddenPID.Value)
    End Sub
    Private Sub ShowData2()
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select * from Epaper where EID=" & Session("EID") & "")

        If dr.Read Then
            LitSubject.Text = dr("Subject").ToString
            LitEmailName.Text = dr("EmailName").ToString
            LitContent.Text = dr("Content").ToString
            LitEmail.Text = dr("SendEmail").ToString
        End If
        dr.Close()
        dr = ClassDB.GetDataReader("select Product.PublicID from epaperRelationProduct,Product where Product.PublicID=epaperRelationProduct.PublicID and EID=" & Session("EID") & "")
        Dim StrHtml As String
        Dim i As Integer = 1
        While dr.Read
            StrHtml += "<tr class=tableCellTwo>" & vbCrLf
            StrHtml += "<td align=middle height=23>" & i & ".</td>" & vbCrLf
            StrHtml += "<td height=23 class=defaultBold style=""PADDING-LEFT: 10px;"">" & GetProductData(dr("PublicID")) & "</td>" & vbCrLf
            StrHtml += "</tr>" & vbCrLf
            StrHtml += "<tr>" & vbCrLf
            StrHtml += "<td bgcolor=#cad0d5 colspan=2 height=1></td>" & vbCrLf
            StrHtml += "</tr>" & vbCrLf
            i += 1
        End While
        LitHtml2.Text = StrHtml
        dr.Close()
    End Sub
#End Region

    Protected Sub BtnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBack.Click
        ShowProcess(1)
    End Sub

    Protected Sub BtnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnView.Click
        ShowProcess(2)
        ShowData()
    End Sub

    Function IsValidEmail(ByVal email As String) As Boolean
        Dim names, Name, i, c
        IsValidEmail = True
        names = Split(email, "@")
        If UBound(names) <> 1 Then
            IsValidEmail = False
            Return IsValidEmail
            Exit Function
        End If
        For Each Name In names
            If Len(Name) <= 0 Then
                IsValidEmail = False
                Return IsValidEmail
                Exit For
            End If
            For i = 1 To Len(Name)
                c = LCase(Mid(Name, i, 1))
                If InStr("abcdefghijklmnopqrstuvwxyz_-.", c) <= 0 And Not IsNumeric(c) Then
                    IsValidEmail = False                    
                    Exit For
                End If
            Next
            If Left(Name, 1) = "." Or Right(Name, 1) = "." Then
                IsValidEmail = False
                Exit For
            End If
        Next
        If InStr(names(1), ".") <= 0 Then
            IsValidEmail = False
        End If
        i = Len(names(1)) - InStrRev(names(1), ".")
        If i <> 2 And i <> 3 Then
            IsValidEmail = False
        End If
        If InStr(email, "..") > 0 Then
            IsValidEmail = False
        End If
        Return IsValidEmail
    End Function

    Protected Sub BtnView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnView1.Click
        Session("Content") = Me.FCKeditor1.Value
        Session("PIDs") = HiddenPID.Value
       Response.Write("<script>window.open('enews-product-preview.aspx','','scrollbars=yes,resizable=yes,width=960,height=680');</script>")
    End Sub

    Protected Sub BtnProductAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnProductAdd.Click
        If TxtProduct.Text = "" Or HiddenPID.Value = "" Then
            Misc.AlertMsg(Page, "請輸入產品")
        End If
        TxtProduct.Text = ""
        LitHtml1.Text = GetProductHtml(Me.HiddenPID.Value)
    End Sub
    Function GetProductHtml(ByVal PIDs As String) As String
        Dim R As String
        If PIDs <> "" Then
            Dim ArrPID() As String = PIDs.Split(";")
            Dim i As Integer
            Dim Sql As String
            Sql = "select PublicID from Product"
            For i = 0 To UBound(ArrPID)
                If i = 0 Then
                    Sql = Sql & " where PublicID=" & ArrPID(i) & ""
                Else
                    Sql = Sql & " or PublicID=" & ArrPID(i) & ""
                End If
            Next
            Dim dr As OleDbDataReader = ClassDB.GetDataReader(Sql)
            i = 1
            While dr.Read
                R += "<tr class=tableCellTwo>" & vbCrLf
                R += "<td align=middle>" & i & ".</td>" & vbCrLf
                R += "<td class=defaultBold style=""PADDING-LEFT: 10px;"">" & GetProductData(dr("PublicID")) & "</td>" & vbCrLf
                R += "<td align=middle><input class=""b_3"" type=button value=""刪除"" onclick=""ProductDel('" & dr("PublicID") & "');""></td>" & vbCrLf
                R += "</tr>" & vbCrLf
                R += "<tr>" & vbCrLf
                R += "<td bgcolor=#cad0d5 colspan=3 height=1></td>" & vbCrLf
                R += "</tr>" & vbCrLf
                i += 1
            End While
            dr.Close()
        End If
        Return R
    End Function

    Protected Sub BtnBack2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBack2.Click
        ShowProcess(1)
    End Sub
End Class
