Imports System.Data
Imports System.Data.OleDb
Partial Class English_product
    Inherits System.Web.UI.Page
    Dim PublicID1, PublicID2, PublicID3 As Integer
    Dim UploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString.Replace("~", "..")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("R") = Nothing
        If HiddenAdd.Value = "add" Then
            If HiddenPublicID.Value <> "" Then
                If Session("OrderData") = Nothing Then
                    Session("OrderData") = HiddenPublicID.Value
                Else
                    Session("OrderData") = Session("OrderData") & "," & HiddenPublicID.Value
                End If
            End If
            HiddenAdd.Value = ""
            HiddenPublicID.Value = ""
        End If
        If HiddenAdd.Value = "esc" Then
            If HiddenPublicID.Value <> "" Then
                Dim ArrOrderData() As String = Session("OrderData").ToString.Split(",")
                Dim i As Integer
                Dim OrderData As String = ""
                For i = 0 To UBound(ArrOrderData)
                    If HiddenPublicID.Value <> ArrOrderData(i) Then
                        If OrderData = "" Then
                            OrderData = ArrOrderData(i)
                        Else
                            OrderData = OrderData & "," & ArrOrderData(i)
                        End If
                    End If
                Next
                If OrderData = "" Then
                    Session("OrderData") = Nothing
                Else
                    Session("OrderData") = OrderData
                End If
            End If
            HiddenAdd.Value = ""
            HiddenPublicID.Value = ""
        End If
        Session("T") = 2
        Dim dr As OleDbDataReader
        Dim Sql As String
        Dim MetaData As String
        LitTitle.Text = "GOLDEN ARBUTUS ENTERPRISE CORP.>>Product Line"
        If Request.QueryString("PID") <> "" Then
            If ChkIsNumeric(Request("PID")) = False Then
                Response.Redirect("product.aspx")
                Response.End()
            End If
            Dim Chk As Boolean = False
            Dim L As Integer
            Sql = "select PublicID,lvl from Product where IsDel=false and Isonline=true and SiteID=2 and PublicID=" & Request("PID") & ""
            dr = ClassDB.GetDataReader(Sql)
            If dr.Read Then
                L = dr("LVL").ToString
                Chk = True
            End If
            dr.Close()
            If Chk = False Then
                Response.Write("<script>alert('Does not have this material');location.href='product.aspx';</script>")
                Response.End()
            End If

            GetPublicID(Request("PID"))
            '顯示Title                        
            LitProductTitle.Text = ""
            If PublicID1 <> 0 Then
                LitTitle.Text += ">>" & GetTitle(PublicID1)
                LitProductTitle.Text += " > <a href=""?PID=" & PublicID1 & """>" & GetTitleNoHtml(PublicID1) & "</a>"
                MetaData = GetTitle(PublicID1)
            End If
            If PublicID2 <> 0 Then
                LitTitle.Text += ">>" & GetTitle(PublicID2)
                LitProductTitle.Text += " > <a href=""?PID=" & PublicID2 & """>" & GetTitleNoHtml(PublicID2) & "</a>"
                MetaData = MetaData & "," & GetTitle(PublicID2)
            End If
            If PublicID3 <> 0 Then
                LitTitle.Text += ">>" & GetTitle(PublicID3)
                LitProductTitle.Text += " > <a href=""?PID=" & PublicID3 & """>" & GetTitleNoHtml(PublicID3) & "</a>"
                MetaData = MetaData & "," & GetTitle(PublicID3)
            End If
        End If
        Dim K As Integer
        Dim PID As Integer
        If Request("PID") = "" Then
            Sql = "select top 1 PublicID from Product where IsDel=false and Isonline=true and SiteID=2 and ParentPublicID=0 order by OrderData"
            dr = ClassDB.GetDataReader(Sql)
            If dr.Read Then
                PID = dr("PublicID").ToString
            End If
            dr.Close()
            LitProductTitle.Text = ""
            LitTitle.Text += ">>" & GetTitle(PID)
            LitProductTitle.Text += " > <a href=""?PID=" & PID & """>" & GetTitleNoHtml(PID) & "</a>"
            MetaData = GetTitle(PID)
        Else
            PID = Request("PID")
        End If
        If PID = 0 Then
            Response.Write("<script>alert('Does not have any product information');location.href='aboutus.aspx';</script>")
            Response.End()
        Else
            dr = ClassDB.GetDataReader("select Lvl,Kind,Subject,ProductName,FID2 from Product where PublicID=" & PID & "")
            If dr.Read Then
                If dr("Lvl") = 1 Then
                    K = 1
                ElseIf dr("Lvl") = 2 Then
                    If dr("Kind") = 1 Then
                        K = 2
                    Else
                        K = 3
                    End If
                Else
                    K = 3
                End If
                If dr("Kind") = 1 Then
                    LitSubject1.Text = dr("Subject").ToString
                    LitSubject2.Text = dr("Subject").ToString
                    If dr("FID2") = 0 Then
                        LitFileBig.Text = "<img src=""images/bigpic_1.jpg"" alt=""img coming soon"" width=""500"" border=""0"">"
                        ShowBig2.Visible = False
                    Else
                        LitFileBig.Text = GetFileHtmlBig(UploadPath, dr("FID2").ToString)
                        ShowBig2.Visible = True
                    End If
                    'LitSubject3.Text = dr("Subject").ToString
                Else
                    LitSubject1.Text = dr("ProductName").ToString
                    LitSubject2.Text = dr("ProductName").ToString
                    'LitSubject3.Text = dr("ProductName").ToString
                End If
            End If
            dr.Close()
        End If
        ShowPanel(K)
        If K = 1 Then
            ShowData1(PID)
        ElseIf K = 2 Then
            ShowData2(PID)
        ElseIf K = 3 Then
            ShowData3(PID)
        End If
        LitMeta.Text = "<META NAME=""KEYWORDS"" CONTENT=""GOLDEN ARBUTUS ENTERPRISE CORP.,Product Line," & MetaData & """>" & vbCrLf
    End Sub
    Private Sub GetPublicID(ByVal PublicID As Integer)
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select ParentPublicID,Lvl,PublicID from Product where IsDel=false and Isonline=true and SiteID=2 and PublicID=" & PublicID & "")
        Dim ParentPublicID As Integer
        If dr.Read Then
            If dr("lvl") = 1 Then
                PublicID1 = dr("PublicID")
            ElseIf dr("lvl") = 2 Then
                PublicID2 = dr("PublicID")
            ElseIf dr("lvl") = 3 Then
                PublicID3 = dr("PublicID")
            End If
            ParentPublicID = dr("ParentPublicID").ToString
        End If
        dr.Close()
        '記錄所有階層的NodeID
        If ParentPublicID > 0 Then
            GetPublicID(ParentPublicID)
        End If

    End Sub
#Region "得到開頭"
    Function GetTitle(ByVal PublicID As Integer) As String
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select Subject,ProductName,Kind from  Product where IsDel=false and Isonline=true and SiteID=2 and PublicID=" & PublicID & "")
        Dim R As String
        If dr.Read Then
            If dr("Kind") = 1 Then
                R = dr("Subject").ToString
            Else
                R = dr("ProductName").ToString
            End If
        End If
        dr.Close()
        Return R
    End Function
    Function GetTitleNoHtml(ByVal PublicID As Integer) As String
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select Subject,ProductName,Kind from  Product where IsDel=false and Isonline=true and SiteID=2 and PublicID=" & PublicID & "")
        Dim R As String
        If dr.Read Then
            If dr("Kind") = 1 Then
                R = dr("Subject").ToString
            Else
                R = dr("ProductName").ToString
            End If
        End If
        dr.Close()
        Return R
    End Function
#End Region
    Private Sub ShowPanel(ByVal k As Integer)
        show1.Visible = False
        show2.Visible = False
        show3.Visible = False
        If k = 1 Then
            show1.Visible = True
        ElseIf k = 2 Then
            show2.Visible = True
        ElseIf k = 3 Then
            show3.Visible = True
        End If
    End Sub
    Private Sub ShowData1(ByVal PublicID As Integer)
        Dim sql As String
        sql = "select PublicID,Subject,Kind,ProductName,FID1,FID3 from Product where IsDel=false and Isonline=true and SiteID=2 "
        sql = sql & " and parentPublicID=" & PublicID & " and Kind=1 order by OrderData"
        Dim ds As DataSet
        ds = ClassDB.RunReturnDataSet(sql, "Product1")
        Dim myTable As DataTable = ds.Tables("Product1")
        Dim intcount As Integer = 0
        Dim k As Integer = 1
        '分頁

        Dim Numbers, Perpage, Curpage, Pages, Offset, page, FromPage, ToPage, SData, EData As Integer
        'Numbers共幾筆,perpage每頁顯示幾個,Curpage當頁,pages共幾頁
        Numbers = myTable.Rows.Count
        LitCount1.Text = myTable.Rows.Count
        '分頁筆數
        Perpage = 6

        page = 9
        Offset = 4

        If (Numbers Mod Perpage = 0) Then '總頁數            
            Pages = Numbers \ Perpage
        Else
            Pages = (Numbers \ Perpage) + 1
        End If
        Curpage = Request("page")
        If Curpage = 0 Then Curpage = 1
        If (Curpage >= Pages) Then Curpage = Pages
        If (Curpage <= 1) Then Curpage = 1
        FromPage = Curpage - Offset
        ToPage = Curpage + page - Offset - 1

        SData = (Curpage - 1) * Perpage
        EData = Curpage * Perpage
        If EData > myTable.Rows.Count Then EData = myTable.Rows.Count
        Dim i As Integer
        Dim list As String = ""
        '顯示資料 
        For i = SData To EData - 1
            If k Mod 3 = 1 Then list += " <div id=""group_1"">" & vbCrLf
            list += "<div id=""product"">" & vbCrLf
            list += "<div id=""pic_1"">" & vbCrLf
            list += "<TABLE WIDTH=156 BORDER=0 CELLPADDING=0 CELLSPACING=0>" & vbCrLf
            list += "<TR>" & vbCrLf
            list += "<TD width=""10"" height=""10""><IMG SRC=""images/t_1.jpg"" WIDTH=14 HEIGHT=14 ALT=""""></TD>" & vbCrLf
            list += "<TD height=""10"" background=""images/t_2.jpg""></TD>" & vbCrLf
            list += "<TD width=""10"" height=""10""><IMG SRC=""images/t_4.jpg"" WIDTH=14 HEIGHT=14 ALT=""""></TD>" & vbCrLf
            list += "</TR>" & vbCrLf
            list += "<TR>" & vbCrLf
            list += "<TD width=""10"" background=""images/t_5.jpg""></TD>" & vbCrLf
            list += "<TD width=""128"" height=""128"" bgcolor=""#FFFFFF"">" & vbCrLf
            If myTable.Rows(i).Item("Kind").ToString = 1 Then
                list += "<a href=""?PID=" & myTable.Rows(i).Item("PublicID").ToString & """>" & GetFileHtmlSmallList(UploadPath, myTable.Rows(i).Item("FID1").ToString) & "</a>"
            Else
                list += "<a href=""?PID=" & myTable.Rows(i).Item("PublicID").ToString & """>" & GetFileHtmlSmallList(UploadPath, myTable.Rows(i).Item("FID3").ToString) & "</a>"
            End If
            list += "</TD>" & vbCrLf
            list += "<TD width=""10"" background=""images/t_8.jpg""></TD>" & vbCrLf
            list += "</TR>" & vbCrLf
            list += "<TR>" & vbCrLf
            list += "<TD><IMG SRC=""images/t_13.jpg"" WIDTH=14 HEIGHT=14 ALT=""""></TD>" & vbCrLf
            list += "<TD height=""10"" background=""images/t_14.jpg""></TD>" & vbCrLf
            list += "<TD><IMG SRC=""images/t_16.jpg"" WIDTH=14 HEIGHT=14 ALT=""""></TD>" & vbCrLf
            list += "</TR>" & vbCrLf
            list += "</TABLE>" & vbCrLf
            list += "</div>" & vbCrLf
            If myTable.Rows(i).Item("Kind").ToString = 1 Then
                list += "<div class=""t12_4""><a href=""?PID=" & myTable.Rows(i).Item("PublicID").ToString & """>" & myTable.Rows(i).Item("Subject").ToString & "</a></div>"
            Else
                list += "<div class=""t12_4""><a href=""?PID=" & myTable.Rows(i).Item("PublicID").ToString & """>" & myTable.Rows(i).Item("ProductName").ToString & "</a></div>"
            End If
            list += "</div>" & vbCrLf
            If k Mod 3 = 0 Then list += "</div>" & vbCrLf
            k = k + 1
        Next
        If (k - 1) Mod 3 <> 0 Then
            list += "</div>" & vbCrLf
        End If

        '這裡是內容
        LitHtml.Text = list

        '這裡是分頁的
        If list = "" Then
            list += "<div id=""line_3""><img src=""images/line_4.jpg"" alt="""" width=""510"" height=""1""></div>" & vbCrLf
            list += "<div id=""line_4"">" & vbCrLf
            list += "<div id=""contace_1"">" & vbCrLf
            list += "<div class=""h_1""><span class=""c_3_3"">Does not have any product information!</span></div>" & vbCrLf
            list += "<div class=""c_1"">&nbsp;</div>" & vbCrLf
            list += "<div class=""h_2"">&nbsp;</div>" & vbCrLf
            list += "</div>" & vbCrLf
            list += "</div>" & vbCrLf
            LitHtml.Text = list
            LitPage.Text = ""
        Else
            LitPage.Text = Page_VBEN(k, page, Pages, FromPage, ToPage, Curpage)
        End If
    End Sub
    Private Sub ShowData2(ByVal PublicID As Integer)
        Dim sql As String
        sql = "select PublicID,Subject,Kind,ProductName,FID3,content1 from Product where IsDel=false and Isonline=true and SiteID=2 "
        sql = sql & " and parentPublicID=" & PublicID & " order by OrderData"
        Dim ds As DataSet
        ds = ClassDB.RunReturnDataSet(sql, "Product2")
        Dim myTable As DataTable = ds.Tables("Product2")
        Dim intcount As Integer = 0
        Dim k As Integer = 1
        '分頁

        Dim Numbers, Perpage, Curpage, Pages, Offset, page, FromPage, ToPage, SData, EData As Integer
        'Numbers共幾筆,perpage每頁顯示幾個,Curpage當頁,pages共幾頁
        Numbers = myTable.Rows.Count
        LitCount2.Text = myTable.Rows.Count
        '分頁筆數
        Perpage = 6

        page = 9
        Offset = 4

        If (Numbers Mod Perpage = 0) Then '總頁數            
            Pages = Numbers \ Perpage
        Else
            Pages = (Numbers \ Perpage) + 1
        End If
        Curpage = Request("page")
        If Curpage = 0 Then Curpage = 1
        If (Curpage >= Pages) Then Curpage = Pages
        If (Curpage <= 1) Then Curpage = 1
        FromPage = Curpage - Offset
        ToPage = Curpage + page - Offset - 1

        SData = (Curpage - 1) * Perpage
        EData = Curpage * Perpage
        If EData > myTable.Rows.Count Then EData = myTable.Rows.Count
        Dim i As Integer
        Dim list As String = ""
        '顯示資料 
        For i = SData To EData - 1
            list += "<div id=""line_3""><img src=""images/line_4.jpg"" alt="""" width=""510"" height=""1""></div>" & vbCrLf
            list += "<div id=""line_4"">" & vbCrLf
            list += "<div id=""pic_2""><a href=""?PID=" & myTable.Rows(i).Item("PublicID").ToString & """>" & GetFileHtmlSmall(UploadPath, myTable.Rows(i).Item("FID3").ToString) & "</a></div>"
            list += "<div id=""contace_1"">" & vbCrLf
            list += "<div class=""h_1""><a href=""?PID=" & myTable.Rows(i).Item("PublicID").ToString & """>" & myTable.Rows(i).Item("ProductName").ToString & "</a></div>"
            list += "<div class=""c_1"">" & Left(myTable.Rows(i).Item("Content1").ToString, 100) & "...</div>" & vbCrLf
            list += "<div class=""h_2"">" & GetCheck(myTable.Rows(i).Item("PublicID").ToString) & "</div>" & vbCrLf
            list += "</div>" & vbCrLf
            list += "</div>" & vbCrLf
            ' myTable.Rows(i).Item("Kind").ToString 
            k = k + 1
        Next
        'If list <> "" Then list += "<div id=""line_3""><img src=""images/line_4.jpg"" alt="""" width=""510"" height=""1""></div>"

        '這裡是內容
        LitHtml2.Text = list

        '這裡是分頁的
        If list = "" Then
            list = "<div id=""line_3""><img src=""images/line_4.jpg"" alt="""" width=""510"" height=""1""></div>" & vbCrLf
            list += "<div id=""line_4"">" & vbCrLf
            ' list += "<div id=""pic_2"">&nbsp;</div>" & vbCrLf
            list += "<div id=""contace_1"">" & vbCrLf
            list += "<div class=""h_1""><span class=""c_3_3"">Does not have any product information!</span></div>" & vbCrLf
            list += "<div class=""c_1"">&nbsp;</div>" & vbCrLf
            list += "<div class=""h_2"">&nbsp;</div>" & vbCrLf
            list += "</div>" & vbCrLf
            list += "</div>" & vbCrLf
            'If list <> "" Then list += "<div id=""line_3""><img src=""images/line_4.jpg"" alt="""" width=""510"" height=""1""></div>"
            LitHtml2.Text = list
            LitPage2.Text = ""
        Else
            LitPage2.Text = Page_VBEN(k, page, Pages, FromPage, ToPage, Curpage)
        End If
    End Sub
    Private Sub ShowData3(ByVal PublicID As Integer)
        Dim sql As String
        sql = "select * from Product where IsDel=false and Isonline=true and SiteID=2 and PublicID=" & PublicID & ""
        Dim ParentPublicID As Integer
        Dim dr As OleDbDataReader = ClassDB.GetDataReader(sql)
        Dim dr2 As OleDbDataReader
        Dim Url4, Url5 As String
        Dim ShowStr As String = "<div id=""line_5""><img src=""images/space.gif"" alt="""" height=""1""></div>"
        If dr.Read Then
            'LitCheck.Text = "<input name=""AddOrderData"" onclick=""AddOrder('" & dr("PublicID").ToString & "')"" type=""checkbox"" value=""""><span class=""c_3_2"">Add to Inquiry Cart</span>"
            LitCheck.Text = GetCheck(dr("PublicID").ToString)
            ParentPublicID = dr("ParentPublicID").ToString
            'LitSubject3.Text = dr("ProductName").ToString
            'LitFID4.Text = GetFileHtmlBig(UploadPath, dr("FID4").ToString)
            If dr("ProductNo").ToString <> "" Then
                LitProductNo.Text = "<div class=""h_3""><strong>ITEM NO:</strong> " & dr("ProductNo").ToString & "</div>" & vbCrLf
                LitProductNo.Text += ShowStr & vbCrLf
            End If
            LitProductName.Text = "<div class=""h_3""><strong>DESCRIPTION:</strong> " & dr("ProductName").ToString & "</div>" & vbCrLf
            If dr("ProductENName").ToString <> "" Then
                LitProductENName.Text = "<div style=""display:none"">" & dr("ProductENName").ToString & "</div>" & vbCrLf
                LitProductENName.Text += ShowStr & vbCrLf
            End If
            If dr("ProductYear").ToString <> "" Then
                LitProductYear.Text = "&nbsp;" & dr("ProductYear").ToString & "</div>" & vbCrLf
                LitProductYear.Text += ShowStr & vbCrLf
            End If
            If dr("ProductCar").ToString <> "" Then
                LitProductCar.Text = "<div class=""h_3""><strong>MODEL:</strong> " & dr("ProductCar").ToString & "" & vbCrLf
                LitProductCar.Text += Url4 & vbCrLf
            End If
            If dr("ProductSubject").ToString <> "" Then
                LitProductSubject.Text = "<div class=""h_3""><strong>MARK:</strong> " & dr("ProductSubject").ToString & "</div>" & vbCrLf
            End If
            If dr("Q1") <> 0 Then
                LitQ1.Text = "<div class=""h_3"">" & dr("Q1").ToString & "&nbsp;" & dr("Q1No").ToString & "" & vbCrLf
                LitQ1.Text += Url4 & vbCrLf
            End If
            If dr("Q2") <> 0 Then
                LitQ2.Text = "/" & dr("Q2").ToString & "&nbsp;" & dr("Q2No").ToString & "</div>" & vbCrLf
                LitQ2.Text += ShowStr & vbCrLf
            End If
            If dr("Q3") <> 0 Then
                LitQ3.Text = "/" & dr("Q3").ToString & "&nbsp;" & dr("Q3No").ToString & "" & vbCrLf
                LitQ3.Text += Url4 & vbCrLf
            End If
            If dr("Q4") <> 0 Then
                LitQ4.Text = "/" & dr("Q4").ToString & "&nbsp;" & dr("Q4No").ToString & "" & vbCrLf
                LitQ4.Text += Url4 & vbCrLf
            End If
            If dr("Raw").ToString <> "" Then
                LitRaw.Text = "<div class=""h_3""><strong>OEM#:</strong>  " & dr("Raw").ToString & "</div>" & vbCrLf
                LitRaw.Text += ShowStr & vbCrLf
            End If
            If dr("Content1").ToString <> "" Then
                LitContent1.Text = "<div id=""group_1"">" & vbCrLf
                LitContent1.Text += "<div class=""h_3""><strong>ADDTION INFORMATION: </strong></div>" & vbCrLf
                LitContent1.Text += "<div class=""c_2"">" & vbCrLf
                LitContent1.Text += dr("Content1").ToString & "</div></div>" & vbCrLf
                LitContent1.Text += ShowStr & vbCrLf
            End If
            If dr("Content2").ToString <> "" Then
                LitContent2.Text = "<div id=""group_1"">" & vbCrLf
                LitContent2.Text += "<div class=""h_3""><strong>REMARKS:</strong> </div>" & vbCrLf
                LitContent2.Text += "<div class=""c_2"">" & vbCrLf
                LitContent2.Text += dr("Content2").ToString & "</div></div>" & vbCrLf
                LitContent2.Text += ShowStr & vbCrLf
            End If
            If dr("FID5") <> 0 Then
                dr2 = ClassDB.GetDataReader("select FileName,FilePath from AttFiles where FID=" & dr("FID5") & "")
                If dr2.Read Then
                    Url5 = UploadPath.Replace("~", "..") & dr2("FilePath") & dr2("FileName")
                End If
                dr2.Close()
            End If
            If Url5 <> "" Then
                LitImg.Text = "<div id=""bupic_1""><a href=""#""><img src=""images/bupic_1.jpg"" alt=""Show Detail"" width=""100"" height=""26"" border=""0"" onClick=""MM_openBrWindow('" & Url5 & "','','resizable=yes,width=760,height=570')""></a></div>"
            Else
                LitImg.Text = ""
            End If
            If dr("FID4") <> 0 Then
                dr2 = ClassDB.GetDataReader("select FileName,FilePath from AttFiles where FID=" & dr("FID4") & "")
                If dr2.Read Then
                    Url4 = UploadPath.Replace("~", "..") & dr2("FilePath") & dr2("FileName")
                End If
                dr2.Close()
            End If
            If Url4 <> "" Then
                If Url5 <> "" Then
                    LitImg2.Text = "<div id=""pic_3""><a href=""#""><img src=""" & Url4 & """ alt=""Show Detail"" width=""400"" border=""0"" onClick=""MM_openBrWindow('" & Url5 & "','','resizable=yes,width=760,height=570')""></a></div>"
                Else
                    LitImg2.Text = "<div id=""pic_3""><img src=""" & Url4 & """ alt=""Show Detail"" width=""400"" border=""0""></div>"
                End If
            Else
                LitImg2.Text = ""
            End If
            If dr("FID6") <> 0 Then
                dr2 = ClassDB.GetDataReader("select FileName,FilePath from AttFiles where FID=" & dr("FID6") & "")
                If dr2.Read Then
                    LitFileDown.Text = " <div class=""h_3"" style=""display:""><strong>ASS PIC: </strong><a href=""" & UploadPath.Replace("~", "..") & dr2("FilePath") & dr2("FileName") & """ target=""_blank"">" & dr("FileTitle") & "</a></div>" & vbCrLf
                End If
                dr2.Close()
            End If
        End If
        dr.Close()
        dr = ClassDB.GetDataReader("select FID2,Subject from Product where PublicID=" & ParentPublicID & "")
        If dr.Read Then
            LitSubject3.Text = dr("Subject").ToString
            LitFID1.Text = GetFileHtmlBig(UploadPath, dr("FID2").ToString)
            If LitFID1.Text = "" Then
                ShowBig.Visible = False
            Else
                ShowBig.Visible = True
            End If
        End If
        dr.Close()
    End Sub

    Protected Sub BtnCheck2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCheck2.Click
        Response.Redirect("inquiry.aspx")
    End Sub

    Protected Sub BtnCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCheck.Click
        Response.Redirect("inquiry.aspx")
    End Sub
    Function GetCheck(ByVal PublicID As Integer) As String
        Dim R As String
        If Session("OrderData") = Nothing Then
            R = "<input name=""AddOrderData"" onclick=""AddOrder('" & PublicID & "')"" type=""checkbox"" value=""""><span class=""c_3_2"">Add to Inquiry Cart</span>"
        Else
            Dim ArrOrderData() As String = Session("OrderData").ToString.Split(",")
            Dim i As Integer
            Dim Chk As Boolean = False
            For i = 0 To UBound(ArrOrderData)
                If PublicID = ArrOrderData(i) Then
                    Chk = True
                    Exit For
                End If
            Next
            If Chk = True Then
                R = "<input name=""EscOrderData"" onclick=""EscOrder('" & PublicID & "')"" type=""checkbox"" checked value=""""><span class=""c_3_2"">Add to Inquiry Cart</span>"
            Else
                R = "<input name=""AddOrderData"" onclick=""AddOrder('" & PublicID & "')"" type=""checkbox"" value=""""><span class=""c_3_2"">Add to Inquiry Cart</span>"
            End If
        End If
        Return R
    End Function
End Class
