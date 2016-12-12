Imports System.Data
Imports System.Data.OleDb
Partial Class inquiry
    Inherits System.Web.UI.Page
    Dim UploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString.Replace("~/", "")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("T") = 4
                    If Session("OrderData") = Nothing Then
                ShowStatus1.Visible = True
                ShowStatus2.Visible = False
            Else
                If ChkData() = False Then
                    ShowStatus1.Visible = True
                    ShowStatus2.Visible = False
                Else
                    ShowStatus1.Visible = False
                    ShowStatus2.Visible = True
                End If
            End If
            If ShowStatus1.Visible = True Then
                BtnEdit.Visible = False
                BtnDel.Visible = False
            Else
                BtnEdit.Visible = True
                BtnDel.Visible = True
            End If
            End Sub
    Function ChkData() As Boolean
        Dim Chk As Boolean = False
        Dim ArrOrderData() As String = Session("OrderData").ToString.Split(",")
        Dim i As Integer
        Dim Sql As String
        Sql = "select PublicID from Product where IsDel=false and Isonline=true and SiteID=1 and Kind=2"
        For i = 0 To UBound(ArrOrderData)
            If i = 0 Then
                Sql = Sql & " and ( PublicID=" & ArrOrderData(i) & ""
            Else
                Sql = Sql & " or PublicID=" & ArrOrderData(i) & ""
            End If
        Next
        Sql = Sql & ")"
        Dim dr As OleDbDataReader = ClassDB.GetDataReader(Sql)
        Dim OrderData As String
        While dr.Read
            If OrderData = "" Then
                OrderData = dr("PublicID").ToString
            Else
                OrderData = OrderData & "," & dr("PublicID").ToString
            End If
            Chk = True
        End While
        dr.Close()
        If OrderData = "" Then
            Session("OrderData") = Nothing
            BtnEdit.Visible = False
            BtnDel.Visible = False
        Else
            Session("OrderData") = OrderData
            BtnEdit.Visible = True
            BtnDel.Visible = True
        End If
        Return Chk
    End Function
    Protected Sub BtnGoProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGoProduct.Click
        Response.Redirect("product.aspx")
    End Sub
#Region "GridView1"
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        '刪除
        If e.CommandName = "DelData" Then
            If Session("OrderData") <> Nothing Then
                Dim ArrOrderData() As String = Session("OrderData").ToString.Split(",")
                Dim i As Integer
                Dim OrderData As String
                For i = 0 To UBound(ArrOrderData)
                    If e.CommandArgument <> ArrOrderData(i) Then
                        If OrderData = "" Then
                            OrderData = ArrOrderData(i)
                        Else
                            OrderData = OrderData & "," & ArrOrderData(i)
                        End If
                    End If
                Next
                If OrderData = "" Then
                    Session("OrderData") = Nothing
                    BtnEdit.Visible = False
                    BtnDel.Visible = False
                Else
                    Session("OrderData") = OrderData
                    BtnEdit.Visible = True
                    BtnDel.Visible = True
                End If
            End If
            GridViewProcess1()
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = 0 OrElse e.Row.RowState = 1 Then
                Dim drowView As System.Data.DataRowView = CType(e.Row.DataItem, System.Data.DataRowView)
                Dim RowIndex As Integer = e.Row.RowIndex
                Dim LitImg2 As Literal = CType(e.Row.FindControl("LitImg2"), Literal)
                Dim Url As String
                If drowView.Item("FID4") <> 0 Then
                    Dim dr As OleDbDataReader
                    dr = ClassDB.GetDataReader("select FileName,FilePath from AttFiles where FID=" & drowView.Item("FID4") & "")
                    If dr.Read Then
                        Url = UploadPath.Replace("~", "..") & dr("FilePath") & dr("FileName")
                        LitImg2.Text = "<a href=""#""><img src=""images/ms_image.gif"" alt="""" width=""16"" height=""16"" border=""0"" onClick=""MM_openBrWindow('" & Url & "','','width=500,height=358')""></a>"
                    End If
                    dr.Close()
                End If
                Dim HiddenPublicID As HiddenField = CType(e.Row.FindControl("HiddenPublicID"), HiddenField)
                HiddenPublicID.Value = drowView.Item("PublicID").ToString                
                '刪除
                Dim BtnDel As Button = CType(e.Row.FindControl("BtnDel"), Button)
                BtnDel.CommandArgument = drowView.Item("PublicID").ToString
            End If
        End If

    End Sub
    Protected Sub Sds_Data_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Sds_Data.Load
        If Session("OrderData") <> Nothing Then
            If ChkData() = True Then
                GridViewProcess1()
            End If
        End If
    End Sub
    Private Sub GridViewProcess1()
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
            ShowStatus1.Visible = True
            ShowStatus2.Visible = False
        End If
        Me.Sds_Data.SelectCommand = Sql
        Me.Sds_Data.SelectCommandType = SqlDataSourceCommandType.Text
    End Sub
#End Region

    Protected Sub BtnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDel.Click
        Session("OrderData") = Nothing
        Response.Redirect("inquiry.aspx")
    End Sub

    Protected Sub BtnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Dim Row As GridViewRow
        For Each Row In GridView1.Rows
            Dim TxtQ1 As TextBox = CType(Row.FindControl("TxtQ1"), TextBox)
            If ChkIsNumeric(TxtQ1.Text) = False Then
                Misc.AlertMsg(Page, "數量請輸入正整數!")
                Exit Sub
            End If
        Next
        Dim Q1, OrderData As String
        For Each Row In GridView1.Rows
            Dim TxtQ1 As TextBox = CType(Row.FindControl("TxtQ1"), TextBox)
            Dim HiddenPublicID As HiddenField = CType(Row.FindControl("HiddenPublicID"), HiddenField)
            If OrderData = "" Then
                OrderData = HiddenPublicID.Value
                Q1 = TxtQ1.Text
            Else
                OrderData = OrderData & "," & HiddenPublicID.Value
                Q1 = Q1 & "," & TxtQ1.Text
            End If
        Next
        Session("OrderData") = OrderData
        Session("OrderQ") = Q1
        Server.Transfer("inquiryOrder.aspx")
    End Sub
End Class
