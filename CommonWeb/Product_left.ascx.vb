Imports System.Data
Imports System.Data.OleDb
Partial Class CommonWeb_Product_left
    Inherits System.Web.UI.UserControl
    Dim TxtMenu As String
    Dim PublicID1, PublicID2, PublicID3 As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Sql As String
        If Request.QueryString("PID") <> "" Then
            If ChkIsNumeric(Request("PID")) = False Then
                Response.Redirect("product.aspx")
                Response.End()
            End If
            GetPublicID1(Request("PID"))
        End If
        Dim i As Integer
        Sql = "select PublicID,Subject from Product where IsDel=false and Isonline=true and SiteID=1 and ParentPublicID=0 and Lvl=1 order by OrderData"
        Dim dr As OleDbDataReader = ClassDB.GetDataReader(Sql)
        While dr.Read
            TxtMenu += "<DIV class=m_1><A href=""?PID=" & dr("PublicID") & """>" & dr("Subject") & "</A></DIV>" & vbCrLf
            TxtMenu += "<DIV id=line_2><IMG height=7 alt="""" src=""images/space.gif"" width=1></DIV>"
            If PublicID1 = 0 Then
                PublicID1 = dr("PublicID").ToString
            End If
            If PublicID1 = dr("PublicID").ToString Then
                If PublicID1 <> 0 Then
                    ChildMenu2(PublicID1)
                End If
                'If PublicID2 <> 0 Then
                '    ChildMenu3(PublicID2)
                'End If
                'If PublicID3 <> 0 Then
                '    ChildMenu3(PublicID3)
                'End If
            End If
        End While
        dr.Close()
        
        LitHtml.Text = TxtMenu
    End Sub
    Private Sub GetPublicID1(ByVal PublicID As Integer)
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select ParentPublicID,Lvl,PublicID from Product where IsDel=false and Isonline=true and SiteID=1 and PublicID=" & PublicID & "")
        Dim ParentPublicID As Integer
        If dr.Read Then
            If dr("lvl") = 1 Then
                PublicID1 = dr("PublicID")
            End If
            ParentPublicID = dr("ParentPublicID").ToString
        End If
        dr.Close()
        '記錄所有階層的NodeID
        If ParentPublicID > 0 Then
            GetPublicID1(ParentPublicID)
        End If

    End Sub
    Private Sub ChildMenu2(ByVal PublicID As String)
        Try
            Dim dr As OleDbDataReader = ClassDB.GetDataReader("select PublicID,Subject,ProductName,Kind from Product where IsDel=false and Isonline=true and SiteID=1 and ParentPublicID=" & PublicID & " order by OrderData")
            While dr.Read

                If dr("Kind") = 1 Then
                    TxtMenu += " <DIV class=m_2><A href=""?PID=" & dr("PublicID") & """>" & dr("Subject").ToString & "</a></div>"
                Else
                    TxtMenu += " <DIV class=m_2p><A href=""?PID=" & dr("PublicID") & """>" & dr("ProductName").ToString & "</a></div>"
                End If
                If Request("PID") <> "" Then
                    If Request("PID") = dr("PublicID") Then
                        ChildMenu3(dr("PublicID"))
                    End If
                End If
            End While
            dr.Close()
        Catch ex As Exception
            ' WriteErrLog(ex, Me.Page)
        Finally
        End Try
    End Sub
    Private Sub ChildMenu3(ByVal PublicID As String)
        Try
            Dim dr As OleDbDataReader = ClassDB.GetDataReader("select PublicID,Subject,ProductName,Kind from Product where IsDel=false and SiteID=1 and ParentPublicID=" & PublicID & " order by OrderData")
        Catch ex As Exception
            ' WriteErrLog(ex, Me.Page)
        Finally
        End Try
    End Sub
End Class
