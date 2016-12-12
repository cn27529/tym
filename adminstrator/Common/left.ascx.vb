
Partial Class adminstrator_Common_left
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If Session("UserID") = Nothing Then
            Response.Write("<script>alert('尚未登入，請先登入!');location.href='adminstrator_login.aspx';</script>")
            Response.End()
        End If
    End Sub
End Class
