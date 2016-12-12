Imports System.Data
Imports System.Data.OleDb
Partial Class EmailToManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("Action") = "TymRunData" And ChkIsNumeric(Request("UID")) = True Then
                Dim dr As OleDbDataReader = ClassDB.GetDataReader("select * from Inquiry where UID=" & Request("UID") & "")
                If dr.Read Then
                    LitUserName.Text = dr("UserName").ToString
                    LitCompanyName.Text = dr("CompanyName").ToString
                    LitPhone.Text = dr("Phone").ToString
                    LitFax.Text = dr("Fax").ToString
                    LitTel.Text = dr("Tel").ToString
                    LitEmail.Text = dr("Email").ToString
                    LitWebSite.Text = dr("WebSite").ToString
                    If dr("Content").ToString <> "" Then
                        LitContent.Text = dr("Content").ToString.Replace(vbCrLf, "<br>")
                    End If
                    GetCode(Request("UID"))
                    GetProduct(Request("UID"))
                End If
                dr.Close()
            End If
        End If
    End Sub
    Private Sub GetCode(ByVal UID As Integer)
        Dim dr As OleDbDataReader
        dr = ClassDB.GetDataReader("select InquiryRelationCode.*,Code.Text,Code.TextEN from InquiryRelationCode inner join Code on Code.CodeID=InquiryRelationCode.CodeID where InquiryRelationCode.UID=" & UID & "")
        Dim DText As String
        While dr.Read
            DText = dr("Text")
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
    End Sub
    Private Sub GetProduct(ByVal UID As Integer)
        Dim dr As OleDbDataReader
        dr = ClassDB.GetDataReader("select Product.ProductNo,Product.ProductCar,Product.Subject,Product.ProductName,InquiryRelationProduct.Q1 from InquiryRelationProduct inner join Product on InquiryRelationProduct.PID=Product.PublicID where InquiryRelationProduct.UID=" & UID & "")
        Dim Str As String
        Dim Subject As String
        Dim i As Integer = 1
        While dr.Read
            If Not IsDBNull(dr("subject")) Then
                Subject = dr("Subject").ToString
            Else
                Subject = dr("ProductName").ToString
            End If
            If Subject = "" Then
                Subject = dr("ProductName").ToString
            End If
            Str += "編號：" & i & "<Br>" & vbCrLf
            Str += "料號：" & dr("ProductNo") & "<Br>" & vbCrLf
            Str += "車型：" & dr("ProductCar") & "<Br>" & vbCrLf
            Str += "詢價產品內容：" & Subject & "<Br>" & vbCrLf
            Str += "採購數量" & dr("Q1").ToString & "<Br>" & vbCrLf
            i += 1
            '內容
        End While
        dr.Close()
        LitData.Text = Str
    End Sub
End Class
