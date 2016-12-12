Imports System.Data
Imports System.Data.OleDb
Partial Class inquiryOrder
    Inherits System.Web.UI.Page
    Private InquiryEmail As String = System.Configuration.ConfigurationManager.AppSettings("InquiryEmail").ToString
    Private MailFrom As String = System.Configuration.ConfigurationManager.AppSettings("MailFrom").ToString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("OrderData") = Nothing Or Session("OrderQ") = Nothing Then
            Response.Write("<Script>alert('資料錯誤');location.href='product.aspx';</script>")
            Response.End()
        End If
        If Not IsPostBack Then
            show1.Visible = True
            show2.Visible = False
            Dim dr As OleDbDataReader = ClassDB.GetDataReader("select * from Code where CodeID<>6")
            While dr.Read
                CodeID.Items.Add(New ListItem(dr("Text").ToString, dr("CodeID")))
            End While
            dr.Close()
            CodeID6.Text = "其他"
            BtnSave.Attributes.Add("onclick", "return Chk()")
            BtnSave.Attributes.Add("onkeypress", "return Chk()")
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = 0 OrElse e.Row.RowState = 1 Then
                Dim drowView As System.Data.DataRowView = CType(e.Row.DataItem, System.Data.DataRowView)
                Dim RowIndex As Integer = e.Row.RowIndex
                Dim LitQ As Literal = CType(e.Row.FindControl("LitQ"), Literal)
                Dim ArrOrderData() As String = Session("OrderData").ToString.Split(",")
                Dim ArrOrderQ() As String = Session("OrderQ").ToString.Split(",")
                Dim i As Integer
                For i = 0 To UBound(ArrOrderData)
                    If ArrOrderData(i) = drowView.Item("publicID") Then
                        LitQ.Text = ArrOrderQ(i)
                        Exit For
                    End If
                Next

            End If
        End If
    End Sub
    Protected Sub Sds_Data_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Sds_Data.Load
        Dim Sql As String
        If Session("OrderData") <> Nothing Then
            Dim ArrOrderData() As String = Session("OrderData").ToString.Split(",")
            Dim i As Integer
            Sql = "select PublicID,ProductName,FID4 from Product where IsDel=false and Isonline=true and SiteID=1 and Kind=2"
            For i = 0 To UBound(ArrOrderData)
                If i = 0 Then
                    Sql = Sql & " and ( PublicID=" & ArrOrderData(i) & ""
                Else
                    Sql = Sql & " or PublicID=" & ArrOrderData(i) & ""
                End If
            Next
            Sql = Sql & ")"
        Else
            Sql = "select PublicID,ProductName,FID4 from Product where 1=2"
        End If
        Me.Sds_Data.SelectCommand = Sql
        Me.Sds_Data.SelectCommandType = SqlDataSourceCommandType.Text
    End Sub
    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Session("R") = True Then
            Session("R") = Nothing
            Response.Write("<Script>alert('請勿按重新整理');location.href='product.aspx';</script>")
            Exit Sub
        End If
        '新增Inquiry
        Dim Sql As String
        Sql = "insert into Inquiry(SiteID,UserName,CompanyName,Phone,Fax,Tel,Email,Website,Content,IsRead,IsDel,PostDate)values"
        Sql = Sql & "(1,'" & UserName.Text.Replace("'", "&#39;") & "','" & CompanyName.Text.Replace("'", "&#39;") & "','" & Phone.Text.Replace("'", "&#39;") & "','" & Fax.Text.Replace("'", "&#39;") & "'"
        Sql = Sql & ",'" & Tel.Text.Replace("'", "&#39;") & "','" & Email.Text & "','" & WebSite.Text.Replace("'", "&#39;") & "','" & Content.Text.Replace("'", "&#39;") & "'"
        Sql = Sql & ",false,false,'" & Now & "')"
        ClassDB.UpdateDB(Sql)
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select Max(UID) as MaxUID from Inquiry")
        Dim UID As Integer
        If dr.Read Then
            UID = dr("MaxUID").ToString
        End If
        dr.Close()
        Dim l As ListItem
        For Each l In CodeID.Items
            If l.Selected = True Then
                ClassDB.UpdateDB("insert into InquiryRelationCode(UID,CodeID)values(" & UID & "," & l.Value & ")")
            End If
        Next
        If CodeID6.Checked = True Then
            ClassDB.UpdateDB("insert into InquiryRelationCode(UID,CodeID,PS)values(" & UID & ",6,'" & Ps.Text.Replace("'", "&#39;") & "')")
        End If
        Dim ArrOrderData() As String = Session("OrderData").ToString.Split(",")
        Dim ArrOrderQ() As String = Session("OrderQ").ToString.Split(",")
        Dim i As Integer
        For i = 0 To UBound(ArrOrderData)
            ClassDB.UpdateDB("insert into InquiryRelationProduct(UID,PID,Q1)values(" & UID & "," & ArrOrderData(i) & "," & ArrOrderQ(i) & ")")
        Next
        '寄信給管理者
        '寄信
        Dim MailBody As String = GetMailBody("EmailToManager.aspx?UID=" & UID & "&Action=TymRunData")
        SendMail("詢價清單", MailBody, InquiryEmail, MailFrom, , True)
        show1.Visible = False
        show2.Visible = True
        Session("OrderData") = Nothing
        Session("OrderQ") = Nothing
        Session("R") = True
    End Sub
End Class
