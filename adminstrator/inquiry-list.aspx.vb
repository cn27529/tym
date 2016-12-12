Imports System.Data
Imports System.Data.OleDb
Partial Class adminstrator_inquiry_list
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ShowProcess(1)           
        End If
    End Sub
#Region "GridView1"
    Protected Sub GridView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBound
        GridViewPageInfo.GetGridViewInfo(Me.GridView1, Me.Page, SDS_Data)
    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName = "InquiryDelete" Then
            ClassDB.UpdateDB("update Inquiry set IsDel=1 where UID=" & e.CommandArgument & "")
            GridView1.DataBind()
        End If

        If e.CommandName = "ViewData" Then
            Dim UID As Integer = CInt(e.CommandArgument)
            Session("UID") = UID
            ShowProcess(2)
            ShowData()
            ClassDB.UpdateDB("update Inquiry set IsRead=1 where UID=" & e.CommandArgument & "")
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = 0 OrElse e.Row.RowState = 1 Then
                Dim drowView As System.Data.DataRowView = CType(e.Row.DataItem, System.Data.DataRowView)
                Dim RowIndex As Integer = e.Row.RowIndex
                Dim LitNum As Literal = CType(e.Row.FindControl("LitNum"), Literal)
                LitNum.Text = GridView1.PageIndex * GridView1.PageSize + RowIndex + 1
                Dim LitRead As Literal = CType(e.Row.FindControl("LitRead"), Literal)
                If drowView.Item("IsRead").ToString = True Then
                    LitRead.Text = "<SPAN class=searchpdno>已檢視</SPAN>"
                Else
                    LitRead.Text = "未檢視"
                End If
                'Dim LinkSubject As LinkButton = CType(e.Row.FindControl("LinkSubject"), LinkButton)
                'LinkSubject.Text = drowView.Item("CompanyName") & "/" & drowView.Item("UserName")
            End If
        End If
    End Sub
#End Region
#Region "顯示資料"
    Private Sub ShowData()
        LitCode.Text = ""
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select * from Inquiry where UID=" & Session("UID") & "")
        Dim WebSite As String
        Dim SiteID As Integer
        If dr.Read Then
            LitUserName.Text = dr("UserName").ToString
            LitCompanyName.Text = dr("CompanyName").ToString
            LitPhone.Text = dr("Phone").ToString
            LitFax.Text = dr("Fax").ToString
            LitTel.Text = dr("Tel").ToString
            LitEmail.Text = "<a href=""mailto:" & dr("Email").ToString & """ target=""_blank"">" & dr("Email").ToString & "</a>"
            If Not IsDBNull(dr("WebSite")) Then
                WebSite = dr("WebSite").ToString
            End If
            If WebSite <> "" Then
                LitWebSite.Text = "<a href=""" & dr("WebSite").ToString & """ target=""_blank"">" & dr("WebSite").ToString & "</a>"
            End If
            LitContent.Text = dr("Content").ToString.Replace(vbCrLf, "<br>")
            SiteID = dr("SiteID").ToString
        End If
        dr.Close()
        dr = ClassDB.GetDataReader("select InquiryRelationCode.*,Code.Text,Code.TextEN from InquiryRelationCode inner join Code on Code.CodeID=InquiryRelationCode.CodeID where InquiryRelationCode.UID=" & Session("UID") & "")
        Dim DText As String
        While dr.Read
            If SiteID = 1 Then
                DText = dr("Text")
            Else
                DText = dr("TextEN")
            End If
            Dim Ps As String
            If Not IsDBNull(dr("Ps")) Then
                Ps = dr("Ps").ToString
            End If
            LitCode.Text += "<div>" & DText
            If Ps <> "" Then
                LitCode.Text += "(" & dr("Ps") & ")"
            End If
            LitCode.Text += "</div>"
        End While
        dr.Close()
        dr = ClassDB.GetDataReader("select Product.Subject,Product.ProductName,InquiryRelationProduct.Q1,Product.ProductNo,Product.ProductCar,Product.ProductYear from InquiryRelationProduct inner join Product on InquiryRelationProduct.PID=Product.PublicID where InquiryRelationProduct.UID=" & Session("UID") & "")
        Dim Str As String
        Dim Subject As String
        Dim i As Integer = 1
        While dr.Read
            Str += "<tr>" & vbCrLf
            If Not IsDBNull(dr("subject")) Then
                Subject = dr("Subject").ToString
            Else
                Subject = dr("ProductName").ToString
            End If
            If Subject = "" Then
                Subject = dr("ProductName").ToString
            End If
            Str += "<td height=""30"" bgcolor=""#f6f6f6""><div align=""center"">" & i & "</div></td>" & vbCrLf
			Str += "<td height=""30"" bgcolor=""#f6f6f6""><div align=""center"">" & dr("ProductNo").ToString & "</div></td>" & vbCrLf
			Str += "<td height=""30"" bgcolor=""#f6f6f6""><div align=""center"">" & dr("ProductCar").ToString & "&nbsp;/&nbsp;" & dr("ProductYear").ToString & "</div></td>" & vbCrLf
            Str += "<td bgcolor=""#f6f6f6""><div align=""center"">" & Subject & "</div></td>" & vbCrLf
            Str += "<td bgcolor=""#f6f6f6""><div align=""center"">" & dr("Q1").ToString & "</div></td>" & vbCrLf
            Str += "</tr>" & vbCrLf
            i += 1
            '內容
        End While
        dr.Close()
        LitHtml.Text = Str
    End Sub
#End Region
    Private Sub ShowProcess(ByVal k As Integer)
        show1.Visible = False
        show2.Visible = False
        If k = 1 Then
            show1.Visible = True
        ElseIf k = 2 Then
            show2.Visible = True
        End If

    End Sub

    Protected Sub SDS_Data_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles SDS_Data.Load
        Dim Strsql As String = ""
        Strsql = "select Inquiry.*,Site.SiteName from Inquiry inner join Site on Inquiry.SiteID=Site.SiteID where IsDel=false"
        If Kind.SelectedValue = 1 Then
            If SSearch.Text <> "" Then
                Strsql = Strsql & " and CompanyName like '%" & SSearch.Text & "%'"
            End If
        End If
        If Kind.SelectedValue = 2 Then
            If SSearch.Text <> "" Then
                Strsql = Strsql & " and UserName like '%" & SSearch.Text & "%'"
            End If
        End If
        Strsql = Strsql & " order by PostDate DESC"
        Me.SDS_Data.SelectCommand = Strsql
        Me.SDS_Data.SelectCommandType = SqlDataSourceCommandType.Text

    End Sub

    Protected Sub BtnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBack.Click
        ShowProcess(1)
    End Sub
End Class
