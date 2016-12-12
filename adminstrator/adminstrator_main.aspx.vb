Imports System.Data
Imports System.Data.OleDb
Partial Class adminstrator_adminstrator_main
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = Nothing Then
            Response.Redirect("adminstrator_login.aspx")
            Response.End()
        End If
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select LogDate,IPAddress from ManagerData where UserID=" & Session("UserID") & "")
        If dr.Read Then
            LitDate.Text = dr("LogDate").ToString
            LitIPAddress.Text = dr("IPAddress").ToString
        End If
        dr.Close()
    End Sub
End Class
