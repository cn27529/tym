Imports System.Data
Imports System.Data.OleDb
Partial Class adminstrator_enews_product_preview
    Inherits System.Web.UI.Page
    Dim SiteDomainName As String = System.Configuration.ConfigurationManager.AppSettings("SiteDomainName").ToString
    Dim UploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserID") = Nothing Then
        '    Response.Write("<script>alert('尚未登入，請先登入!');location.href='adminstrator_login.aspx';</script>")
        '    Response.End()
        'End If
        If Not IsPostBack Then
            LitCss.Text = "<link href=""" & SiteDomainName & "/adminstrator/images/common.css"" rel=""stylesheet"" type=""text/css"">"
            'LitCss.Text = "<link href=""images/common.css"" rel=""stylesheet"" type=""text/css"">"
            If Request("EID") <> "" Then
                DataView1()
            Else
                DataView2()
            End If
        End If
    End Sub
    Private Sub DataView1()
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select content from Epaper where EID=" & Request("EID") & "")

        If dr.Read Then
            LitContent.Text = dr("content").ToString.Replace("/UserFiles", SiteDomainName & "/UserFiles")
        End If
        dr.Close()
        dr = ClassDB.GetDataReader("select Product.PublicID from epaperRelationProduct,Product where Product.PublicID=epaperRelationProduct.PublicID and EID=" & Request("EID") & "")
        Dim PIDs As String
        Dim i As Integer = 1
        While dr.Read
            If PIDs = "" Then
                PIDs = dr("PublicID").ToString
            Else
                PIDs = PIDs & ";" & dr("PublicID").ToString
            End If
        End While        
        dr.Close()
        ShowProduct(PIDs)
    End Sub
    Private Sub DataView2()
        If Session("content") <> "" Then
            LitContent.Text = Session("content").ToString.Replace("/UserFiles", SiteDomainName & "/UserFiles")
        End If
        ShowProduct(Session("PIDs"))
        Session("PIDs") = Nothing
        Session("content") = Nothing
    End Sub
    Private Sub ShowProduct(ByVal PIDs As String)
        If PIDs <> "" Then
            Dim i As Integer
            Dim ArrPID() As String = PIDs.Split(";")
            Dim Sql As String
            Sql = "select * from Product"
            For i = 0 To UBound(ArrPID)
                If i = 0 Then
                    Sql = Sql & " where PublicID=" & ArrPID(i) & ""
                Else
                    Sql = Sql & " or PublicID=" & ArrPID(i) & ""
                End If
            Next
            Dim dr As OleDbDataReader = ClassDB.GetDataReader(Sql)
            i = 1
            Dim r As String
            While dr.Read
                r += "<tr class=tableCellTwo>" & vbCrLf
                r += "<td align=middle height=20>" & i & ".</td>" & vbCrLf
				r += "<td width=120 style=""PADDING-RIGHT: 4px; PADDING-left: 4px;PADDING-BOTTOM: 2px; PADDING-TOP: 2px""><div id=""pic_2"">" & vbCrLf
				r += "<a href=""" & SiteDomainName & "/product.aspx?PID=" & dr("publicid") & """ target=_blank>" & vbCrLf
                r += ImgData(dr("FID3"))
                r += "</a></div></td>" & vbCrLf
                r += "<td width=160 class=defaultBold style=""PADDING-RIGHT: 0px; PADDING-LEFT: 10px; PADDING-TOP: 6px""><a href=""" & SiteDomainName & "/product.aspx?PID=" & dr("publicid") & """ target=_blank>" & dr("ProductSubject") & "&nbsp;/&nbsp;" & dr("ProductNo") & "</a></td>" & vbCrLf
                r += "<TD align=middle>" & dr("ProductCar") & "&nbsp;/&nbsp;" & dr("ProductYear") & "&nbsp;</TD>" & vbCrLf
                r += "<TD align=middle>" & dr("ProductName") & "&nbsp;/&nbsp;" & dr("ProductENName") & "&nbsp;</TD>" & vbCrLf
                r += "<TD align=middle>" & dr("Raw") & "&nbsp;</TD>" & vbCrLf
                r += "<td class=""default"" style=""PADDING-RIGHT: 0px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px"">" & dr("content1").ToString.Replace("/UserFiles", SiteDomainName & "/UserFiles") & "&nbsp;</TD>" & vbCrLf
                r += "</tr>" & vbCrLf
                r += "<tr><td bgcolor=#cad0d5 colspan=7 height=1></td></tr>"
                i += 1
            End While
            dr.Close()
            LitHtml.Text = r
            If r = "" Then
                show1.Visible = False
            Else
                show1.Visible = True
            End If
        End If
    End Sub
    Function ImgData(ByVal FID As Integer) As String
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select FileName,FilePath from AttFiles where FID=" & FID & "")
        Dim R As String
        Dim ArrFileName() As String
        If dr.Read Then
            Dim Url As String = SiteDomainName & UploadPath.Replace("~", "") & dr("FilePath") & dr("FileName")
            R = "<img src=""" & Url & """ alt="""" width=""120"" border=""0"">"
        Else
            R = "<img src=""" & SiteDomainName & "/images/pic_cno.jpg"" alt=""圖片製作中"" width=""120"" border=""0"">"
        End If
        dr.Close()
        Return R
    End Function
End Class
