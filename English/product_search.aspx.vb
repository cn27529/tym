Imports System.Data
Imports System.Data.OleDb
Partial Class English_product_search
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("S") <> "" Then
            If Sql_Injection(Request("S")) = False Then
                Response.Write("<script>alert('Material mistake');location.href='product.aspx';</script>")
                Response.End()
            End If
            LitS.Text = Request("S")
        End If
        If Request("K") <> "" Then
            If ChkIsNumeric(Request("K")) = False Then
                Response.Write("<script>alert('Material mistake');location.href='product.aspx';</script>")
                Response.End()
            Else
                If Request("k") <> 1 And Request("k") <> 2 Then
                    Response.Write("<script>alert('Material mistake');location.href='product.aspx';</script>")
                    Response.End()
                End If
            End If
        End If
        If Request("S") <> "" Then
            ShowData()
        Else
            LitS.Text = "  <div><font color=red>Please input the key words</font></div>"
        End If
        Session("T") = 5
    End Sub
    Private Sub ShowData()
        Dim sql As String
        sql = "select PublicID,Subject,Kind,ProductName,ProductNo,ProductSubject,ProductYear,ProductCar from Product where IsDel=false and Isonline=true and SiteID=2 "
        If Request("K") <> "" Then
            sql = sql & " and Kind=" & Request("k") & ""
        End If
        If Request("S") <> "" Then
            sql = sql & " and (Subject like '%" & Request("s") & "%' or ProductName like '%" & Request("s") & "%')"
        End If
        Dim ds As DataSet
        ds = ClassDB.RunReturnDataSet(sql, "Product_search")
        Dim myTable As DataTable = ds.Tables("Product_search")
        Dim intcount As Integer = 0
        Dim k As Integer = 1
        '分頁

        Dim Numbers, Perpage, Curpage, Pages, Offset, page, FromPage, ToPage, SData, EData As Integer
        'Numbers共幾筆,perpage每頁顯示幾個,Curpage當頁,pages共幾頁
        Numbers = myTable.Rows.Count
        ' LitCount1.Text = myTable.Rows.Count
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
        Dim Subject, ProductNo, ProductSubject, ProductYear, ProductCar As String
        '顯示資料 
        For i = SData To EData - 1
            ProductNo = ""
            If myTable.Rows(i).Item("Kind").ToString = 1 Then
                list += "  <div class=""s_1""><a href=""product.aspx?PID=" & myTable.Rows(i).Item("PublicID").ToString & """>" & myTable.Rows(i).Item("Subject").ToString & "</a></div>"
            Else
                Subject = myTable.Rows(i).Item("ProductName").ToString
                If Not IsDBNull(myTable.Rows(i).Item("ProductYear").ToString) Then
                    ProductYear = myTable.Rows(i).Item("ProductYear").ToString
                End If
                If ProductYear <> "" Then
                    ProductSubject = myTable.Rows(i).Item("ProductSubject").ToString
                End If
                If ProductSubject <> "" Then
                    ProductCar = myTable.Rows(i).Item("ProductCar").ToString
                End If
                If ProductCar <> "" Then
                    Subject = ProductSubject & " / " & ProductCar & " / " & ProductYear & " / " & Subject
                End If
                list += " <div class=""s_2""><a href=""product.aspx?PID=" & myTable.Rows(i).Item("PublicID").ToString & """>" & Subject & "</a></div>"
            End If
            list += " <div id=""line_5""><img src=""images/space.gif"" alt="""" height=""1""></div>"

        Next

        '這裡是內容
        LitHtml.Text = list

        '這裡是分頁的
        If list = "" Then
            LitHtml.Text = "  <div align=""center""><font color=red>Does not have this material</font></div>"
            LitPage.Text = ""
        Else
            LitPage.Text = Page_VBEN(k, page, Pages, FromPage, ToPage, Curpage)
        End If
    End Sub
End Class
