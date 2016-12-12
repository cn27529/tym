Imports System.Data
Imports System.Data.OleDb
Partial Class English_news
    Inherits System.Web.UI.Page
    Dim UploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString.Replace("~", "..")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ShowTable()
        Session("T") = 3
        If ChkIsNumeric(Request("NID")) = True Then
            show1.Visible = False
            show2.Visible = True
            ShowData(Request("NID"))
        Else
            LitTitle.Text = "GOLDEN ARBUTUS ENTERPRISE CORP.>>news"
            show1.Visible = True
            show2.Visible = False
        End If
        If Request("NID") = "" Then
            LitMeta.Text = "<META NAME=""KEYWORDS"" CONTENT=""GOLDEN ARBUTUS ENTERPRISE CORP.,news"">" & vbCrLf
        End If
    End Sub
#Region "Show list"
    Private Sub ShowTable()
        Dim ds As DataSet
        ds = ClassDB.RunReturnDataSet("select * from New where Isonline=true and SiteID=2 order by PostDate Desc", "News")
        Dim myTable As DataTable = ds.Tables("News")
        Dim intcount As Integer = 0
        Dim k As Integer = 1
        '分頁

        Dim Numbers, Perpage, Curpage, Pages, Offset, page, FromPage, ToPage, SData, EData As Integer
        'Numbers共幾筆,perpage每頁顯示幾個,Curpage當頁,pages共幾頁
        Numbers = myTable.Rows.Count
        LitCount.Text = myTable.Rows.Count
        '分頁筆數
        Perpage = 10

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
            list += "<tr>" & vbCrLf
            list += "<td bgcolor=""#CEEAFF""><div align=""center"">" & i + 1 & "</div></td>" & vbCrLf
            list += "<td bgcolor=""#CEEAFF"" class=""c_4""><a href=""news.aspx?NID=" & myTable.Rows(i).Item("NID").ToString & """>" & myTable.Rows(i).Item("Subject").ToString & "</a></td>" & vbCrLf
            list += "<td height=""30"" bgcolor=""#CEEAFF""><div align=""center"">" & CDate(myTable.Rows(i).Item("PostDate")).ToString("yyyy.MM.dd") & "</div></td>" & vbCrLf
            list += "</tr>" & vbCrLf
            k = k + 1
        Next
        '這裡是內容
        LitHtml.Text = list

        '這裡是分頁的
        If list = "" Then
            LitHtml.Text = "<tr><td colspan=3 align=center><font color=red>No news!</font></td></tr>"
            LitPage.Text = ""
        Else
            LitPage.Text = Page_VBEN(k, page, Pages, FromPage, ToPage, Curpage)
        End If
    End Sub
#End Region
#Region "Show information"
    Private Sub ShowData(ByVal NID As Integer)
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select * from New where NID=" & NID & " and SiteID=2")
        Dim FileName As String
        If dr.Read Then
            LitTitle.Text = "GOLDEN ARBUTUS ENTERPRISE CORP.>>news>>" & dr("Subject").ToString
            LitMeta.Text = "<META NAME=""KEYWORDS"" CONTENT=""GOLDEN ARBUTUS ENTERPRISE CORP.,news," & dr("Subject").ToString & """>" & vbCrLf
            'LitTitle1.Text = ">" & dr("Subject").ToString
            LitSubject.Text = dr("Subject").ToString
            LitContent.Text = dr("Content").ToString
            If Not IsDBNull(dr("FileName")) Then
                FileName = dr("FileName").ToString
            End If
            If FileName <> "" Then
                LitFile.Text = GetFileHtml(UploadPath.ToString, dr("FilePath"), FileName)
            End If
        Else
            show1.Visible = True
            show2.Visible = False
        End If
        dr.Close()
    End Sub
#End Region
End Class
