Imports System.Data
Imports System.Data.OleDb
Partial Class adminstrator_news_list
    Inherits System.Web.UI.Page
    Dim UploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString
#Region "GridView1"
    Protected Sub GridView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBound
        GridViewPageInfo.GetGridViewInfo(Me.GridView1, Me.Page, SDS_Data)
    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName = "NewDelete" Then
            ClassDB.UpdateDB("Delete from New where NID=" & e.CommandArgument & "")
            Response.Redirect("news-list.aspx")
        End If

        If e.CommandName = "BtnNew" Then
            Dim NID As Integer = CInt(e.CommandArgument)
            LitTitle.Text = "編輯資料"
            Session("NID") = NID
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
                If drowView.Item("Isonline").ToString = True Then
                    LitIsonline.Text = "<img src=""images/v.gif"" alt="""">"
                Else
                    LitIsonline.Text = "<img src=""images/x.gif"" alt="""">"
                End If
            End If
        End If
    End Sub
#End Region

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        SiteID.SelectedIndex = 0
        Subject.Text = ""
        Me.FCKeditor3.Value = ""
        Isonline.Checked = True
        LitFile.Text = ""
        ShowProcess(2)
        Session("NID") = Nothing
        LitTitle.Text = "新增消息"
    End Sub

    Private Sub ShowProcess(ByVal k As Integer)
        If Session("Result") <> Nothing Then
            Misc.AlertMsg(Page, Session("Result"))
            Session("Result") = Nothing
        End If
        show1.Visible = False
        Show2.Visible = False
        If k = 1 Then
            show1.Visible = True
        ElseIf k = 2 Then
            Show2.Visible = True
        End If

    End Sub

    Protected Sub BtnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        If Me.FCKeditor3.Value = "" Then
            Misc.AlertMsg(Page, "請輸入內容")
            Exit Sub
        End If
        '判斷檔案
        Dim Chk As Integer = ChkFile()
        If Chk = 1 Then
            Misc.AlertMsg(Page, "檔案只能上傳JPG、GIF、SWF類型")
            Exit Sub
        ElseIf Chk = 2 Then
            Misc.AlertMsg(Page, "檔案最多只能上傳10MB")
            Exit Sub
        End If
        Dim DataPath, FileName As String
        If Chk <> 3 Then
            '上傳檔案   
            DataPath = "New/" & Now.ToString("yyyyMM", System.Globalization.DateTimeFormatInfo.InvariantInfo) & "/"
            FileName = UPFile(DataPath)
            If FileName = "" Then
                Misc.AlertMsg(Page, "檔案上傳失敗")
                Exit Sub
            End If
        End If
        Dim Sql As String
        If Me.Session("NID") = Nothing Then
            Sql = "insert into New(SiteID,Subject,Content,FileName,FilePath,Isonline,PostDate,LastDate,UserID,RuserID)"
            Sql = Sql & " values(" & SiteID.SelectedValue & ",'" & Subject.Text.Replace("'", "&#39;") & "','" & FCKeditor3.Value.Replace("'", "&#39;") & "',"
            Sql = Sql & " '" & FileName & "','" & DataPath & "'," & Isonline.Checked & ",'" & Now & "', '" & Now & "', " & Session("UserID") & "," & Session("UserID") & ")"
            Session("Result") = "新增最新消息成功"
        Else
            Sql = "update New set Subject='" & Subject.Text.Replace("'", "&#39;") & "',SiteID=" & SiteID.SelectedValue & ""
            Sql = Sql & ",content='" & FCKeditor3.Value.Replace("'", "&#39;") & "',Isonline=" & Isonline.Checked & ""
            Sql = Sql & ",LastDate='" & Now & "',RUserID=" & Session("UserID") & ""
            If FileName <> "" Then
                Sql = Sql & ",FileName='" & FileName & "',FilePath='" & DataPath & "'"
            End If
            Sql = Sql & " where NID=" & Session("NID") & ""
            Session("Result") = "編輯最新消息成功"
        End If
        ClassDB.UpdateDB(Sql)
        Session("NID") = Nothing
        Response.Redirect("news-list.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ShowProcess(1)
            Dim dr As OleDbDataReader = ClassDB.GetDataReader("select * from site")
            SiteID.Items.Clear()
            SiteID.Items.Add(New ListItem("請選擇", ""))
            While dr.Read
                SiteID.Items.Add(New ListItem(dr("SiteName").ToString, dr("SiteID").ToString))
            End While
            dr.Close()
        End If
    End Sub
#Region "顯示資料"
    Private Sub ShowData()
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select * from new where NID=" & Session("NID") & "")
        Dim FileName As String
        If dr.Read Then
            SiteID.SelectedValue = dr("SiteID").ToString
            Subject.Text = dr("Subject").ToString.Replace("&#39;", "'")
            Me.FCKeditor3.Value = dr("Content").ToString.Replace("&#39;", "'")
            If Not IsDBNull(dr("FileName")) Then
                FileName = dr("FileName").ToString
            End If
            If FileName <> "" Then
                LitFile.Text = GetFileData(UploadPath.ToString, dr("FilePath"), FileName)
            End If
            Isonline.Checked = dr("Isonline").ToString
        End If
        dr.Close()
    End Sub
#End Region
#Region "檔案"
    Function ChkFile() As Integer
        Dim HttpPostedFile1 As HttpPostedFile = Me.FileUpload1.PostedFile
        Dim FileName1 As String = Me.FileUpload1.FileName
        If HttpPostedFile1.ContentLength = Nothing Then
            Return 3
            Exit Function
        End If
        Dim AttFileName() As String = FileName1.Split(".")
        If AttFileName(UBound(AttFileName)).ToLower <> "jpg" And AttFileName(UBound(AttFileName)).ToLower <> "gif" And AttFileName(UBound(AttFileName)).ToLower <> "swf" Then
            Return 1
            Exit Function
        End If
        If CInt(HttpPostedFile1.ContentLength) > 10242000 Then
            Return 2
            Exit Function
        End If
        Return 0
    End Function
    Function UPFile(ByVal DataPath As String) As String

        Dim FilePath As String = UploadPath & "/" & DataPath
        Dim FileName As String = Me.FileUpload1.FileName
        Dim HttpPostedFile1 As HttpPostedFile = Me.FileUpload1.PostedFile
        'GetFileName
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("Select FileName from New where FileName='" & FileName & "'")
        If dr.Read Then
            FileName = ChangeFileName(FileName)
        End If
        dr.Close()
        Try
            '檢查目錄是否已存在，否則建立
            If Not FileIO.FileSystem.DirectoryExists(Server.MapPath(FilePath)) Then
                FileIO.FileSystem.CreateDirectory(Server.MapPath(FilePath))
            End If
            '存檔
            Me.FileUpload1.SaveAs(Server.MapPath(FilePath & FileName))
        Catch ex As Exception
            Return ""
        End Try
        Return FileName
    End Function
#End Region

    Protected Sub BtnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBack.Click
        ShowProcess(1)
    End Sub
End Class
