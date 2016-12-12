
Partial Class CommonWeb_TopWeb
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("T") = 1 Then
            LitImage.Text = "<div id=""head""><img src=""images/h_about.jpg"" alt="""" /></div>"
        ElseIf Session("T") = 2 Then
            show1.Visible = True
            LitImage.Text = "<div id=""head""><img src=""images/h_product.jpg"" alt="""" /></div>"
        ElseIf Session("T") = 3 Then
            LitImage.Text = "<div id=""head""><img src=""images/h_news.jpg"" alt="""" /></div>"
        ElseIf Session("T") = 4 Then
            LitImage.Text = "<div id=""head""><img src=""images/h_inquiry.jpg"" alt="""" /></div>"
        ElseIf Session("T") = 5 Then
            show1.Visible = True
            LitImage.Text = "<div id=""head""><img src=""images/h_search.jpg"" alt="""" /></div>"
        End If
        If Not IsPostBack Then
            SKind.Items.Add(New ListItem("全部", ""))
            SKind.Items.Add(New ListItem("目錄搜尋", "1"))
            SKind.Items.Add(New ListItem("產品搜尋", "2"))
        End If
    End Sub

    Protected Sub ImageSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageSearch.Click
        If SKeyWord.Text = "" Then
            Misc.AlertMsg(Page, "請輸入關鍵字")
            Exit Sub
        End If
        Response.Redirect("product_search.aspx?K=" & SKind.SelectedValue & "&S=" & SKeyWord.Text & "")
    End Sub
End Class
