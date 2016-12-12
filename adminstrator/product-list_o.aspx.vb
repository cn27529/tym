Imports System.Data
Imports System.Data.OleDb
Partial Class adminstrator_product_list
    Inherits System.Web.UI.Page
    Dim UploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString
    Dim PublicIDs As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("V") = Nothing
            ShowCode()
            ShowBtn(1)
            ShowProcess(1)
        End If
    End Sub
#Region "GridView1"
    Protected Sub GridView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBound
        GridViewPageInfo.GetGridViewInfo(Me.GridView1, Me.Page, SDS_Data)
    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName = "ProductDelete" Then
            '修改SiteID
            PublicIDs = e.CommandArgument
            GetPublicID(e.CommandArgument)
            Dim Sql As String
            Sql = "update Product set IsDel=true where"
            Dim ArrPublicIDs() As String = PublicIDs.Split(",")
            Dim i As Integer
            For i = 0 To UBound(ArrPublicIDs)
                If i = 0 Then
                    Sql = Sql & " PublicID=" & ArrPublicIDs(i) & ""
                Else
                    Sql = Sql & " or PublicID=" & ArrPublicIDs(i) & ""
                End If
            Next
            ClassDB.UpdateDB(Sql)
            ' Response.Redirect("news-list.aspx")
        End If
        '檢視第２層
        If e.CommandName = "BtnView" Then
            Dim PublicID As Integer = CInt(e.CommandArgument)
            Dim Key() As String = e.CommandArgument.ToString.Split(",")
            If Key(1) = 1 Then
                Session("PublicID") = Nothing
                If Key(2) = 1 Then
                    Session("ViewPublicID1") = Key(0)
                    ShowBtn(2)
                ElseIf Key(2) = 2 Then
                    Session("ViewPublicID2") = Key(0)
                    Dim kk As String = Session("ViewPublicID2")
                    ShowBtn(3)
                End If
                Session("V") = Key(2)
                GridViewProcess()
            Else
                Session("PublicID") = Key(0)
                LitTitle2.Text = "編輯產品"
                ShowData2(Key(0))
                ShowProcess(3)
            End If
        End If
        '修改資料
        If e.CommandName = "BtnProduct" Then
            If Session("ViewPublicID1") <> Nothing Then               
                ShowFile.Visible = True
            Else
                ShowFile.Visible = False
            End If
            Dim Key() As String = e.CommandArgument.ToString.Split(",")
            Session("PublicID") = Key(0)
            If Key(1) = 1 Then
                LitTitle.Text = "編輯目錄"
                ShowData1(Key(0))
                ShowProcess(2)
            Else
                LitTitle2.Text = "編輯產品"
                ShowData2(Key(0))
                ShowProcess(3)
            End If
        End If
        '加入電子報
        If e.CommandName = "AddEpaper" Then
            ClassDB.UpdateDB("Update Product set Epaper=true where PublicID=" & e.CommandArgument & "")
        End If
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated


        If e.Row.RowIndex = 0 Then
            Dim gv As GridView = DirectCast(sender, GridView)
            Dim gvRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim T1 As New TableCell()
            Dim T2 As New TableCell()
            Dim T3 As New TableCell()
            Dim T4 As New TableCell()
            Dim T5 As New TableCell()
            Dim T6 As New TableCell()
            T1.Text = "&nbsp;"            
            T2.Text = "<img src=""images/file_no.gif"" alt="""" width=""16"" height=""16"" vspace=""6"">"
            T3.Text = "&nbsp;"
            If Session("ViewPublicID1") = Nothing Then
                T4.Text = "目錄"
            Else
                Dim PublicID As Integer
                If Session("ViewPublicID3") <> Nothing Then
                    PublicID = Session("ViewPublicID3")                    
                End If
                If PublicID = 0 Then
                    If Session("ViewPublicID2") <> Nothing Then
                        PublicID = Session("ViewPublicID2")
                    End If
                End If
                If PublicID = 0 Then
                    If Session("ViewPublicID1") <> Nothing Then
                        PublicID = Session("ViewPublicID1")
                    End If
                End If
                Dim dr As OleDbDataReader = ClassDB.GetDataReader("select Subject from Product where PublicID=" & PublicID & "")
                If dr.Read Then
                    T4.Text = dr("Subject").ToString
                End If
                dr.Close()
            End If
                T5.Text = "&nbsp;"
                T6.Text = "&nbsp;"
                T6.CssClass = "defaultBold"
                T5.CssClass = "defaultBold"
                gvRow.Cells.Add(T1)
                gvRow.Cells.Add(T2)
                gvRow.Cells.Add(T3)
                gvRow.Cells.Add(T4)
                gvRow.Cells.Add(T5)
                gvRow.Cells.Add(T6)
                gv.Controls(0).Controls.AddAt(1, gvRow)
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
                Dim LitImg As Literal = CType(e.Row.FindControl("LitImg"), Literal)

                If drowView.Item("Kind").ToString = 2 Then
                    LitImg.Text = "<img src=""images/file_info.gif"" alt="""">"
                Else
                    LitImg.Text = "<img src=""images/file.gif"" alt="""">"
                End If
                Dim BtnPublish As Button = CType(e.Row.FindControl("BtnPublish"), Button)
                BtnPublish.CommandArgument = drowView.Item("PublicID").ToString & "," & drowView.Item("Kind").ToString & "," & drowView.Item("ParentPublicID").ToString
                Dim V As Integer = 0
                If Session("V") = Nothing Then
                    V = 1
                Else
                    V = 2
                End If

                Dim LinkSubject As LinkButton = CType(e.Row.FindControl("LinkSubject"), LinkButton)
                LinkSubject.CommandArgument = drowView.Item("PublicID").ToString & "," & drowView.Item("Kind").ToString & "," & V
                '加入電子報
                Dim BtnEpaper As Button = CType(e.Row.FindControl("BtnEpaper"), Button)
                BtnEpaper.CommandArgument = drowView.Item("PublicID")
                If drowView.Item("Kind").ToString = 2 Then
                    LinkSubject.Text = drowView.Item("ProductName").ToString
                    If drowView.Item("Epaper") = True Then
                        BtnEpaper.Enabled = False
                    Else
                        BtnEpaper.Enabled = True
                    End If
                    BtnEpaper.Visible = True
                Else
                    LinkSubject.Text = drowView.Item("Subject").ToString
                    BtnEpaper.Visible = False
                End If
                If drowView.Item("Isonline").ToString = False Then
                    BtnEpaper.Visible = False
                End If
            End If
        End If

    End Sub
    Protected Sub SDS_Data_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles SDS_Data.Load
        GridViewProcess()
    End Sub
    Private Sub GridViewProcess()
        Dim ParentPublicID As Integer = 0
        If Session("ViewPublicID3") <> Nothing Then
            ParentPublicID = Session("ViewPublicID3")
        End If
        If ParentPublicID = 0 Then
            If Session("ViewPublicID2") <> Nothing Then
                ParentPublicID = Session("ViewPublicID2")
            End If
        End If
        If ParentPublicID = 0 Then
            If Session("ViewPublicID1") <> Nothing Then
                ParentPublicID = Session("ViewPublicID1")
            End If
        End If
        Dim sql As String
        sql = "select PublicID,SiteName,Subject,Isonline,Kind,ParentPublicID,ProductName,Epaper from "
        sql = sql & " Product inner join Site on Product.SiteId=Site.SiteID "
        sql = sql & " where ParentPublicID=" & ParentPublicID & " and IsDel=false "
        sql = sql & " order by OrderData"
        Me.SDS_Data.SelectCommand = sql
        Me.SDS_Data.SelectCommandType = SqlDataSourceCommandType.Text
    End Sub
#End Region
#Region "動作判斷"
    Private Sub ShowProcess(ByVal k As Integer)
        If Session("Result") <> Nothing Then
            Misc.AlertMsg(Page, Session("Result"))
            Session("Result") = Nothing
        End If
        show1.Visible = False
        Show2.Visible = False
        show3.Visible = False
        Show4.Visible = False
        If k = 1 Then
            show1.Visible = True
        ElseIf k = 2 Then
            Show2.Visible = True
        ElseIf k = 3 Then
            show3.Visible = True
        ElseIf k = 4 Then
            Show4.Visible = True
        End If

    End Sub
#End Region
#Region "顯示目錄資料"
    Private Sub ShowData1(ByVal PublicID As Integer)
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select ParentPublicID,SiteID,Subject,Isonline,FID1,FID2 from Product where PublicID=" & PublicID & "")
        If dr.Read Then
            SiteID.SelectedValue = dr("SiteID").ToString
            Subject.Text = dr("Subject").ToString.Replace("&#39;", "'")
            Isonline.Checked = dr("Isonline").ToString
            If dr("ParentPublicID") = 0 Then
                SiteID.Enabled = True
            Else
                SiteID.Enabled = False
            End If
            ' SiteID.Enabled = False
            LitFile1.Text = GetFileData2(UploadPath, dr("FID1"))
            LitFile2.Text = GetFileData2(UploadPath, dr("FID2"))
        End If
        dr.Close()
    End Sub
#End Region
#Region "目錄新增／修改"
    Protected Sub BtnAdd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd1.Click
        Session("PublicID") = Nothing
        If Session("ViewPublicID1") <> Nothing Then
            Dim dr As OleDbDataReader = ClassDB.GetDataReader("select SiteID from Product where PublicID=" & Session("ViewPublicID1") & "")
            If dr.Read Then
                SiteID.Enabled = False
                SiteID.SelectedValue = dr("SiteID").ToString
            End If
            dr.Close()
            ShowFile.Visible = True
        Else
            ShowFile.Visible = False
            SiteID.Enabled = True
            SiteID.SelectedIndex = 0
        End If
        LitTitle.Text = "新增目錄"
        Subject.Text = ""
        LitFile1.Text = ""
        LitFile2.Text = ""
        Isonline.Checked = True
        ShowProcess(2)
    End Sub

    Protected Sub BtnEdit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEdit1.Click
        EditData()
    End Sub
    Private Sub EditData()
        Dim DataPath As String
        Dim FID1, FID2 As Integer
        '判斷檔案
        If ShowFile.Visible = True Then
            Dim Chk As Integer = ChkFile(1)
            If Chk = 1 Then
                Misc.AlertMsg(Page, "目錄小圖只能上傳JPG、GIF、SWF類型")
                Exit Sub
            ElseIf Chk = 2 Then
                Misc.AlertMsg(Page, "目錄小圖最多只能上傳10MB")
                Exit Sub
            End If
            Chk = ChkFile(2)
            If Chk = 1 Then
                Misc.AlertMsg(Page, "目錄大圖只能上傳JPG、GIF、SWF類型")
                Exit Sub
            ElseIf Chk = 2 Then
                Misc.AlertMsg(Page, "目錄大圖最多只能上傳10MB")
                Exit Sub
            End If
            '上傳目錄小圖   
            DataPath = "Product/Small/" & Now.ToString("yyyyMM", System.Globalization.DateTimeFormatInfo.InvariantInfo) & "/"
            FID1 = UPFile(DataPath, 1)
            '上傳目錄大圖   
            DataPath = "Product/Big/" & Now.ToString("yyyyMM", System.Globalization.DateTimeFormatInfo.InvariantInfo) & "/"
            FID2 = UPFile(DataPath, 2)
        End If
        Dim Sql As String
        Dim PublicID As Integer = Session("PublicID")
        Dim ParentPublicID As Integer
        If Session("V") = 1 Then
            ParentPublicID = Session("ViewPublicID1")
        ElseIf Session("V") = 2 Then
            ParentPublicID = Session("ViewPublicID2")
        End If
        If Not IsNumeric(PublicID) Then PublicID = 0
        If Not IsNumeric(ParentPublicID) Then ParentPublicID = 0
        Dim Lvl As Integer
        If PublicID = 0 Then
            If ParentPublicID = 0 Then
                Lvl = 1
            Else
                Lvl = 2
            End If
            Sql = "insert into Product(ParentPublicID,SiteID,Subject,FID1,FID2,Isonline,PostDate,LastDate,UserID,RUserID,Kind,Lvl,Epaper) values"
            Sql = Sql & "(" & ParentPublicID & "," & SiteID.SelectedValue & ",'" & Subject.Text.Replace("'", "&#39;") & "'," & FID1 & "," & FID2 & "," & Isonline.Checked & ",'" & Now & "','" & Now & "'," & Session("UserID") & "," & Session("UserID") & ",1," & Lvl & ",false)"
            ClassDB.UpdateDB(Sql)
            '修改OrderData
            Dim dr As OleDbDataReader = ClassDB.GetDataReader("select Max(PublicID) as MaxPublicID from Product")
            If dr.Read Then
                PublicID = dr("MaxPublicID").ToString
            End If
            dr.Close()
            UpOrderData(PublicID, ParentPublicID)
            Session("Result") = "新增目錄成功"
        Else
            Sql = "update Product set SiteID=" & SiteID.SelectedValue & ",subject='" & Subject.Text.Replace("'", "&#39;") & "',Isonline=" & Isonline.Checked & ",LastDate='" & Now & "',RUserID=" & Session("UserID") & ""
            If FID1 <> 0 Then
                Sql = Sql & ",FID1=" & FID1 & ""
            End If
            If FID2 <> 0 Then
                Sql = Sql & ",FID2=" & FID2 & ""
            End If
            Sql = Sql & " where PublicID=" & Session("PublicID") & ""
            ClassDB.UpdateDB(Sql)
            '修改SiteID
            PublicIDs = Session("PublicID")
            GetPublicID(Session("PublicID"))
            Sql = "update Product set SiteID=" & SiteID.SelectedValue & " where "
            Dim ArrPublicIDs() As String = PublicIDs.Split(",")
            Dim i As Integer
            For i = 0 To UBound(ArrPublicIDs)
                If i = 0 Then
                    Sql = Sql & " PublicID=" & ArrPublicIDs(i) & ""
                Else
                    Sql = Sql & " or PublicID=" & ArrPublicIDs(i) & ""
                End If
            Next
            Session("Result") = "編輯目錄成功"
            ClassDB.UpdateDB(Sql)
        End If

        ShowProcess(1)
    End Sub
    Private Sub UpOrderData(ByVal PublicID As Integer, ByVal ParentPublicID As Integer)
        Dim Sql As String
        If ParentPublicID = 0 Then
            Sql = "select max(OrderData) as MaxOrderData from Product where ParentPublicID=" & ParentPublicID & " and SiteID=" & SiteID.SelectedValue & ""
        Else
            Sql = "select max(OrderData) as MaxOrderData from Product where ParentPublicID=" & ParentPublicID & ""
        End If
        Dim dr As OleDbDataReader = ClassDB.GetDataReader(Sql)
        Dim MaxOrder As Integer
        If dr.Read Then
            If Not IsDBNull(dr("MaxOrderData")) Then
                If Not IsNumeric(dr("MaxOrderData")) Then
                    MaxOrder = 1
                Else
                    MaxOrder = CInt(dr("MaxOrderData")) + 1
                End If
            Else
                MaxOrder = 1
            End If
        End If
        dr.Close()
        If Not IsNumeric(MaxOrder) Then MaxOrder = 1
        ClassDB.UpdateDB("update Product set OrderData=" & MaxOrder & " where PublicID=" & PublicID & "")
    End Sub
    Private Sub GetPublicID(ByVal PublicID As Integer)
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select ParentPublicID,PublicID from Product where ParentPublicID=" & PublicID & "")
        While dr.Read
            PublicIDs = PublicIDs & "," & dr("PublicID")
            GetPublicID(dr("PublicID"))
        End While
        dr.Close()
    End Sub
#End Region
#Region "檔案"
    Function ChkFile(ByVal K As Integer) As Integer
        Dim HttpPostedFile1 As HttpPostedFile
        Dim FileName1 As String
        If K = 1 Then
            HttpPostedFile1 = Me.FileUpload1.PostedFile
            FileName1 = Me.FileUpload1.FileName
        ElseIf K = 2 Then
            HttpPostedFile1 = Me.FileUpload2.PostedFile
            FileName1 = Me.FileUpload2.FileName
        ElseIf K = 3 Then
            HttpPostedFile1 = Me.FileUpload3.PostedFile
            FileName1 = Me.FileUpload3.FileName
        ElseIf K = 4 Then
            HttpPostedFile1 = Me.FileUpload4.PostedFile
            FileName1 = Me.FileUpload4.FileName
        ElseIf K = 5 Then
            HttpPostedFile1 = Me.FileUpload5.PostedFile
            FileName1 = Me.FileUpload5.FileName
        ElseIf K = 6 Then
            HttpPostedFile1 = Me.FileUpload6.PostedFile
            FileName1 = Me.FileUpload6.FileName
        End If
        If HttpPostedFile1.ContentLength = Nothing Then
            Return 3
            Exit Function
        End If
        Dim AttFileName() As String = FileName1.Split(".")
        If K <> 6 Then
            If K = 1 Or K = 2 Or K = 3 Then
                If AttFileName(UBound(AttFileName)).ToLower <> "jpg" And AttFileName(UBound(AttFileName)).ToLower <> "gif" Then
                    Return 1
                    Exit Function
                End If
            Else
                If AttFileName(UBound(AttFileName)).ToLower <> "jpg" And AttFileName(UBound(AttFileName)).ToLower <> "gif" And AttFileName(UBound(AttFileName)).ToLower <> "swf" Then
                    Return 1
                    Exit Function
                End If
            End If
        End If
        If CInt(HttpPostedFile1.ContentLength) > 10242000 Then
            Return 2
            Exit Function
        End If
        Return 0
    End Function
    Function UPFile(ByVal DataPath As String, ByVal K As Integer) As Integer

        Dim FilePath As String = UploadPath & "/" & DataPath
        Dim HttpPostedFile1 As HttpPostedFile
        Dim FileName As String
        If K = 1 Then
            FileName = Me.FileUpload1.FileName
            HttpPostedFile1 = Me.FileUpload1.PostedFile
        ElseIf K = 2 Then
            FileName = Me.FileUpload2.FileName
            HttpPostedFile1 = Me.FileUpload2.PostedFile
        ElseIf K = 3 Then
            FileName = Me.FileUpload3.FileName
            HttpPostedFile1 = Me.FileUpload3.PostedFile
        ElseIf K = 4 Then
            FileName = Me.FileUpload4.FileName
            HttpPostedFile1 = Me.FileUpload4.PostedFile
        ElseIf K = 5 Then
            FileName = Me.FileUpload5.FileName
            HttpPostedFile1 = Me.FileUpload5.PostedFile
        ElseIf K = 6 Then
            FileName = Me.FileUpload6.FileName
            HttpPostedFile1 = Me.FileUpload6.PostedFile
        End If

        'GetFileName
        If FileName <> "" Then
            Dim dr As OleDbDataReader = ClassDB.GetDataReader("Select FileName from AttFiles where FileName='" & FileName & "'")
            If dr.Read Then
                FileName = ChangeFileName(FileName)
            End If
            dr.Close()
            Try
                '檢查目錄是否已存在，否則建立
                If Not FileIO.FileSystem.DirectoryExists(Server.MapPath(FilePath)) Then
                    FileIO.FileSystem.CreateDirectory(Server.MapPath(FilePath))
                End If
                '儲存檔案
                Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")
                Dim Sql As String
                Sql = "insert into AttFiles(FileName,FilePath,PostDate,IPAddress,UserID)values"
                Sql = Sql & "('" & FileName & "','" & DataPath & "','" & Now & "','" & IPAddress & "'," & Session("UserID") & ")"
                ClassDB.UpdateDB(Sql)
                '上傳檔案
                If K = 1 Then Me.FileUpload1.SaveAs(Server.MapPath(FilePath & FileName))
                If K = 2 Then Me.FileUpload2.SaveAs(Server.MapPath(FilePath & FileName))
                If K = 3 Then Me.FileUpload3.SaveAs(Server.MapPath(FilePath & FileName))
                If K = 4 Then Me.FileUpload4.SaveAs(Server.MapPath(FilePath & FileName))
                If K = 5 Then Me.FileUpload5.SaveAs(Server.MapPath(FilePath & FileName))
                If K = 6 Then Me.FileUpload6.SaveAs(Server.MapPath(FilePath & FileName))
            Catch ex As Exception
                Return 0
            End Try
            dr = ClassDB.GetDataReader("select FID from AttFiles where FileName='" & FileName & "'")
            Dim FID As Integer
            If dr.Read Then
                FID = dr("FID").ToString
            End If
            dr.Close()
            Return FID
        Else
            Return 0
        End If
    End Function
#End Region

#Region "顯示Code"
    Private Sub ShowCode()
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select * from site")
        SiteID.Items.Clear()
        SiteID2.Items.Clear()
        Q1NO.Items.Clear()
        Q2NO.Items.Clear()
        Q3NO.Items.Clear()
        Q4NO.Items.Clear()
        SiteID.Items.Add(New ListItem("請選擇", ""))
        SiteID2.Items.Add(New ListItem("請選擇", ""))
        Q1NO.Items.Add(New ListItem("請選擇", ""))
        Q1NO.Items.Add(New ListItem("PC", "PC"))
        Q1NO.Items.Add(New ListItem("PCS", "PCS"))
        Q2NO.Items.Add(New ListItem("請選擇", ""))
        Q2NO.Items.Add(New ListItem("CFT", "CFT"))
        'Q2NO.Items.Add(New ListItem("KGW", "KGW"))
        Q3NO.Items.Add(New ListItem("請選擇", ""))
        Q3NO.Items.Add(New ListItem("KG", "KG"))
        'Q3NO.Items.Add(New ListItem("KGW", "KGW"))
        Q4NO.Items.Add(New ListItem("請選擇", ""))
        Q4NO.Items.Add(New ListItem("KG", "KG"))
        'Q4NO.Items.Add(New ListItem("KGW", "KGW"))
        While dr.Read
            SiteID.Items.Add(New ListItem(dr("SiteName").ToString, dr("SiteID").ToString))
            SiteID2.Items.Add(New ListItem(dr("SiteName").ToString, dr("SiteID").ToString))
        End While
        dr.Close()

    End Sub
#End Region
#Region "上面按鈕判斷"
    Private Sub ShowBtn(ByVal k As Integer)
        Session("PublicID") = Nothing
        If k = 1 Then
            Session("V") = Nothing
            BtnAdd1.Visible = True
            BtnAdd2.Visible = False
            BtnOrder.Visible = True
            BtnBack.Visible = False
            Session("ViewPublicID1") = Nothing
            Session("ViewPublicID2") = Nothing
        ElseIf k = 2 Then
            Session("V") = 1
            Session("ViewPublicID2") = Nothing
            BtnAdd1.Visible = True
            BtnAdd2.Visible = True
            BtnOrder.Visible = True
            BtnBack.Visible = True
        ElseIf k = 3 Then
            Session("V") = 2
            BtnAdd1.Visible = False
            BtnAdd2.Visible = True
            BtnOrder.Visible = True
            BtnBack.Visible = True
        End If
    End Sub
#End Region

#Region "回上一頁及上一層"
    Protected Sub BtnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBack.Click
        ShowBtn(Session("V"))
        GridViewProcess()
        ShowProcess(1)
    End Sub
    Protected Sub BtnBack2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBack2.Click
        If Session("ViewPublicID1") = Nothing Then
            ShowBtn(1)
        Else
            ShowBtn(2)
        End If
        ShowProcess(1)
    End Sub
    Protected Sub BtnBack3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBack3.Click
        If Session("ViewPublicID1") = Nothing Then
            ShowBtn(1)
        ElseIf Session("ViewPublicID2") = Nothing Then
            ShowBtn(2)
        Else
            ShowBtn(3)
        End If
        ShowProcess(1)
    End Sub
    Protected Sub BtnBack4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBack4.Click
        If Session("ViewPublicID1") = Nothing Then
            ShowBtn(1)
        ElseIf Session("ViewPublicID2") = Nothing Then
            ShowBtn(2)
        Else
            ShowBtn(3)
        End If
        ShowProcess(1)
    End Sub
#End Region
#Region "顯示產品資料"
    Private Sub ShowData2(ByVal PublicID As Integer)
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select * from Product where PublicID=" & PublicID & "")
        If dr.Read Then
            SiteID2.SelectedValue = dr("SiteID").ToString
            Subject.Text = dr("Subject").ToString.Replace("&#39;", "'")
            Isonline2.Checked = dr("Isonline").ToString
            LitFile3.Text = GetFileData2(UploadPath, dr("FID3"))
            LitFile4.Text = GetFileData2(UploadPath, dr("FID4"))
            LitFile5.Text = GetFileData2(UploadPath, dr("FID5"))
            LitFile6.Text = GetFileData2(UploadPath, dr("FID6"))
            ProductNo.Text = dr("ProductNo").ToString.Replace("&#39;", "'")
            ProductName.Text = dr("ProductName").ToString.Replace("&#39;", "'")
            ProductENName.Text = dr("ProductENName").ToString.Replace("&#39;", "'")
            ProductYear.Text = dr("ProductYear").ToString.Replace("&#39;", "'")
            ProductCar.Text = dr("ProductCar").ToString.Replace("&#39;", "'")
            ProductSubject.Text = dr("ProductSubject").ToString.Replace("&#39;", "'")
            If dr("Q1") <> 0 Then
                Q1.Text = dr("Q1").ToString
            End If
            If dr("Q2") <> 0 Then
                Q2.Text = dr("Q2").ToString
            End If
            If dr("Q3") <> 0 Then
                Q3.Text = dr("Q3").ToString
            End If
            If dr("Q4") <> 0 Then
                Q4.Text = dr("Q4").ToString
            End If
            Try
                Q1NO.SelectedValue = dr("Q1No")
            Catch ex As Exception

            End Try
            Try
                Q2NO.SelectedValue = dr("Q2NO")
            Catch ex As Exception

            End Try
            Try
                Q3NO.SelectedValue = dr("Q3NO")
            Catch ex As Exception

            End Try
            Try
                Q4NO.SelectedValue = dr("Q4NO")
            Catch ex As Exception

            End Try
            Raw.Text = dr("Raw").ToString
            Me.FCKeditor1.Value = dr("content1").ToString.Replace("&#39;", "'")
            Me.FCKeditor2.Value = dr("content2").ToString.Replace("&#39;", "'")
            FileTitle.Text = dr("FileTitle").ToString.Replace("&#39;", "'")
        End If
        dr.Close()
    End Sub
#End Region
#Region "產品資料清空"
    Protected Sub BtnAdd2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd2.Click
        If Session("ViewPublicID1") <> Nothing Then
            Dim dr As OleDbDataReader = ClassDB.GetDataReader("select SiteID from Product where PublicID=" & Session("ViewPublicID1") & "")
            If dr.Read Then
                SiteID2.SelectedValue = dr("SiteID").ToString
            End If
            dr.Close()
        ElseIf Session("ViewPublicID2") <> Nothing Then
            Dim dr As OleDbDataReader = ClassDB.GetDataReader("select SiteID from Product where PublicID=" & Session("ViewPublicID1") & "")
            If dr.Read Then
                SiteID2.SelectedValue = dr("SiteID").ToString
            End If
            dr.Close()
        End If
        LitTitle2.Text = "新增產品"
        ProductNo.Text = ""
        ProductName.Text = ""
        ProductENName.Text = ""
        ProductYear.Text = ""
        ProductCar.Text = ""
        ProductSubject.Text = ""
        Q1.Text = ""
        Q2.Text = ""
        Q3.Text = ""
        Q4.Text = ""
        Q1NO.SelectedIndex = 0
        Q2NO.SelectedIndex = 0
        Q3NO.SelectedIndex = 0
        Q4NO.SelectedIndex = 0
        Raw.Text = ""
        Me.FCKeditor1.Value = ""
        Me.FCKeditor2.Value = ""
        FileTitle.Text = ""
        Isonline2.Checked = True
        LitFile3.Text = ""
        LitFile4.Text = ""
        LitFile5.Text = ""
        LitFile6.Text = ""
        Session("PublicID") = Nothing
        ShowProcess(3)
    End Sub
#End Region


#Region "產品新增／修改"
    Protected Sub BtnEdit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEdit2.Click
        EditData2()
    End Sub
    Private Sub EditData2()
        Dim DScript As String = ChkData()
        If DScript <> "" Then
            Misc.AlertMsg(Page, DScript)
            Exit Sub
        End If
        '判斷檔案
        Dim Chk As Integer = ChkFile(3)
        If Chk = 1 Then
            Misc.AlertMsg(Page, "目錄小圖只能上傳JPG、GIF、SWF類型")
            Exit Sub
        ElseIf Chk = 2 Then
            Misc.AlertMsg(Page, "目錄小圖最多只能上傳10MB")
            Exit Sub
        End If
        Chk = ChkFile(4)
        If Chk = 1 Then
            Misc.AlertMsg(Page, "目錄大圖只能上傳JPG、GIF、SWF類型")
            Exit Sub
        ElseIf Chk = 2 Then
            Misc.AlertMsg(Page, "目錄大圖最多只能上傳10MB")
            Exit Sub
        End If
        Chk = ChkFile(5)
        If Chk = 1 Then
            Misc.AlertMsg(Page, "目錄大圖只能上傳JPG、GIF、SWF類型")
            Exit Sub
        ElseIf Chk = 2 Then
            Misc.AlertMsg(Page, "目錄大圖最多只能上傳10MB")
            Exit Sub
        End If
        Chk = ChkFile(6)
        If Chk = 1 Then
            Misc.AlertMsg(Page, "目錄大圖最多只能上傳10MB")
            Exit Sub
        End If
        Dim DataPath As String
        Dim FID3, FID4, FID5, FID6 As Integer
        '上傳目錄小圖   
        DataPath = "Product/Img/" & Now.ToString("yyyyMM", System.Globalization.DateTimeFormatInfo.InvariantInfo) & "/"
        FID3 = UPFile(DataPath, 3)
        FID4 = UPFile(DataPath, 4)
        FID5 = UPFile(DataPath, 5)
        FID6 = UPFile(DataPath, 6)
        Dim Sql As String
        Dim PublicID As Integer = Session("PublicID")
        Dim ParentPublicID As Integer
        Dim Lvl As Integer
        If Session("V") = 1 Then
            ParentPublicID = Session("ViewPublicID1")
            Lvl = 2
        ElseIf Session("V") = 2 Then
            ParentPublicID = Session("ViewPublicID2")
            Lvl = 3
        ElseIf Session("V") = 3 Then
            ParentPublicID = Session("ViewPublicID3")
        End If
        If Not IsNumeric(PublicID) Then PublicID = 0
        If Not IsNumeric(ParentPublicID) Then ParentPublicID = 0
        Dim DQ1, DQ2, DQ3, DQ4 As Double
        If Q1.Text = "" Then
            DQ1 = 0
        Else
            DQ1 = Q1.Text
        End If
        If Q2.Text = "" Then
            DQ2 = 0
        Else
            DQ2 = Q2.Text
        End If
        If Q3.Text = "" Then
            DQ3 = 0
        Else
            DQ3 = Q3.Text
        End If
        If Q4.Text = "" Then
            DQ4 = 0
        Else
            DQ4 = Q4.Text
        End If
        If PublicID = 0 Then
            Sql = "insert into Product(ParentPublicID,SiteID,ProductNo,ProductName,ProductEnName,ProductYear,ProductCar"
            Sql = Sql & ",ProductSubject,Q1,Q2,Q3,Q4,Q1No,Q2No,Q3No,Q4No,Raw,Content1,Content2,FID3,FID4,FID5,FID6"
            Sql = Sql & ",FileTitle,Isonline,PostDate,LastDate,UserID,RUserID,Kind,IsDel,Lvl,Epaper) values"
            Sql = Sql & "(" & ParentPublicID & "," & SiteID2.SelectedValue & ",'" & ProductNo.Text.Replace("'", "&#39;") & "'"
            Sql = Sql & ",'" & ProductName.Text.Replace("'", "&#39;") & "','" & ProductENName.Text.Replace("'", "&#39;") & "'"
            Sql = Sql & ",'" & ProductYear.Text.Replace("'", "&#39;") & "','" & ProductCar.Text.Replace("'", "&#39;") & "'"
            Sql = Sql & ",'" & ProductSubject.Text.Replace("'", "&#39;") & "'," & DQ1 & "," & DQ2 & "," & DQ3 & "," & DQ4 & ""
            Sql = Sql & ",'" & Q1NO.SelectedValue & "','" & Q2NO.SelectedValue & "','" & Q3NO.SelectedValue & "','" & Q4NO.SelectedValue & "'"
            Sql = Sql & ",'" & Raw.Text.Replace("'", "&#39;") & "','" & Me.FCKeditor1.Value.Replace("'", "&#39;") & "'"
            Sql = Sql & ",'" & FCKeditor1.Value.Replace("'", "&#39;") & "'," & FID3 & "," & FID4 & "," & FID5 & "," & FID6 & ""
            Sql = Sql & ",'" & FileTitle.Text.Replace("'", "&#39;") & "'," & Isonline2.Checked & ""
            Sql = Sql & ",'" & Now & "','" & Now & "'," & Session("UserID") & "," & Session("UserID") & ",2,false," & Lvl & ",false)"
            ClassDB.UpdateDB(Sql)
            '修改OrderData
            Dim dr As OleDbDataReader = ClassDB.GetDataReader("select Max(PublicID) as MaxPublicID from Product")
            If dr.Read Then
                PublicID = dr("MaxPublicID").ToString
            End If
            dr.Close()
            UpOrderData(PublicID, ParentPublicID)
            Session("Result") = "新增產品成功"
        Else
            Sql = "update Product set SiteID=" & SiteID2.SelectedValue & ",ProductNo='" & ProductNo.Text.Replace("'", "&#39;") & "'"
            Sql = Sql & ",ProductName='" & ProductName.Text.Replace("'", "&#39;") & "',ProductEnName='" & ProductENName.Text.Replace("'", "&#39;") & "'"
            Sql = Sql & ",ProductYear='" & ProductYear.Text.Replace("'", "&#39;") & "',ProductCar='" & ProductCar.Text.Replace("'", "&#39;") & "'"
            Sql = Sql & ",ProductSubject='" & ProductSubject.Text.Replace("'", "&#39;") & "'"
            Sql = Sql & ",Q1='" & DQ1 & "',Q2='" & DQ2 & "',Q3='" & DQ3 & "',Q4='" & DQ4 & "'"
            Sql = Sql & ",Q1No='" & Q1NO.SelectedValue & "',Q2No='" & Q2NO.SelectedValue & "',Q3No='" & Q3NO.SelectedValue & "',Q4No='" & Q4NO.SelectedValue & "'"
            Sql = Sql & ",Raw='" & Raw.Text.Replace("'", "&#39;") & "',Content1='" & FCKeditor1.Value.Replace("'", "&#39;") & "'"
            Sql = Sql & ",Content2='" & FCKeditor2.Value.Replace("'", "&#39;") & "',FileTitle='" & FileTitle.Text.Replace("'", "&#39;") & "'"
            Sql = Sql & ",Isonline=" & Isonline2.Checked & ",LastDate='" & Now & "',RUserID=" & Session("UserID") & ""
            If FID3 <> 0 Then
                Sql = Sql & ",FID3=" & FID3 & ""
            End If
            If FID4 <> 0 Then
                Sql = Sql & ",FID4=" & FID4 & ""
            End If
            If FID5 <> 0 Then
                Sql = Sql & ",FID5=" & FID5 & ""
            End If
            If FID6 <> 0 Then
                Sql = Sql & ",FID6=" & FID6 & ""
            End If
            Sql = Sql & " where PublicID=" & Session("PublicID") & ""
            Session("Result") = "編輯產品成功"
            ClassDB.UpdateDB(Sql)
        End If
        ShowProcess(1)
    End Sub
    Function ChkData() As String
        Dim Err As String
        If Q1.Text <> "" Then
            If IsNumeric(Q1.Text) = False Then
                Err = Err & "數量請輸入數字\n"
            End If
        End If
        If Q2.Text <> "" Then
            If IsNumeric(Q2.Text) = False Then
                Err = Err & "材數請輸入數字\n"
            End If
        End If
        If Q3.Text <> "" Then
            If IsNumeric(Q3.Text) = False Then
                Err = Err & "淨重請輸入數字\n"
            End If
        End If
        If Q4.Text <> "" Then
            If IsNumeric(Q4.Text) = False Then
                Err = Err & "毛重請輸入數字\n"
            End If
        End If
        Return Err
    End Function
#End Region

    Protected Sub BtnOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOrder.Click
        ShowProcess(4)
    End Sub
#Region "GridView2"

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = 0 OrElse e.Row.RowState = 1 Then
                Dim drowView As System.Data.DataRowView = CType(e.Row.DataItem, System.Data.DataRowView)
                Dim RowIndex As Integer = e.Row.RowIndex
                Dim LitImg2 As Literal = CType(e.Row.FindControl("LitImg2"), Literal)
                If drowView.Item("Kind").ToString = 2 Then
                    LitImg2.Text = "<img src=""images/file_info.gif"" alt="""">"
                Else
                    LitImg2.Text = "<img src=""images/file.gif"" alt="""">"
                End If
                Dim LitGridView2Subject As Literal = CType(e.Row.FindControl("LitGridView2Subject"), Literal)                
                If drowView.Item("Kind").ToString = 2 Then
                    LitGridView2Subject.Text = drowView.Item("ProductName").ToString
                Else
                    LitGridView2Subject.Text = drowView.Item("Subject").ToString
                End If
                Dim TxtOrderData As TextBox = CType(e.Row.FindControl("TxtOrderData"), TextBox)
                TxtOrderData.Text = drowView.Item("OrderData").ToString
                Dim HiddenPublicID As HiddenField = CType(e.Row.FindControl("HiddenPublicID"), HiddenField)
                HiddenPublicID.Value = drowView.Item("PublicID").ToString
            End If
        End If

    End Sub
    Protected Sub Sds_Data2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Sds_Data2.Load
        GridViewProcess2()
    End Sub
    Private Sub GridViewProcess2()
        Dim ParentPublicID As Integer = 0
        If Session("ViewPublicID3") <> Nothing Then
            ParentPublicID = Session("ViewPublicID3")
        End If
        If ParentPublicID = 0 Then
            If Session("ViewPublicID2") <> Nothing Then
                ParentPublicID = Session("ViewPublicID2")
            End If
        End If
        If ParentPublicID = 0 Then
            If Session("ViewPublicID1") <> Nothing Then
                ParentPublicID = Session("ViewPublicID1")
            End If
        End If
        Dim sql As String
        sql = "select PublicID,Subject,OrderData,Kind,ProductName from Product "
        sql = sql & " where ParentPublicID=" & ParentPublicID & " and IsDel=false"
        sql = sql & " order by OrderData"
        Me.Sds_Data2.SelectCommand = sql
        Me.Sds_Data2.SelectCommandType = SqlDataSourceCommandType.Text
    End Sub
#End Region


    Protected Sub BtnEditOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEditOrder.Click
        Dim Row As GridViewRow

        For Each Row In GridView2.Rows
            Dim TxtOrderData As TextBox = CType(Row.FindControl("TxtOrderData"), TextBox)
            If ChkIsNumeric(TxtOrderData.Text) = False Then
                Misc.AlertMsg(Page, "排序請輸入正整數!")
                Exit Sub
            End If
        Next
        For Each Row In GridView2.Rows
            Dim TxtOrderData As TextBox = CType(Row.FindControl("TxtOrderData"), TextBox)
            Dim HiddenPublicID As HiddenField = CType(Row.FindControl("HiddenPublicID"), HiddenField)
            ClassDB.UpdateDB("Update Product Set OrderData=" & TxtOrderData.Text & " where PublicID=" & HiddenPublicID.Value & "")
        Next
        Session("Result") = "排序設定成功"
        ShowProcess(1)
    End Sub
End Class