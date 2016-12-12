Imports System.Data
Imports System.Data.OleDb
Partial Class adminstrator_adminstrator_login
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("UserID") = Nothing
        Dim V_Img As String = Session("V_Img")
        Session("V_Img") = Nothing
        ViewState("V_Img") = V_Img
        ImageLog.Attributes.Add("onclick", "return ManagerLogin()")
        ImageLog.Attributes.Add("onkeypress", "return ManagerLogin()")
    End Sub

    Protected Sub ImageLog_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageLog.Click
        Dim Dscript As String = ChkData()
        If Dscript <> "" Then
            Misc.AlertMsg(Page, Dscript)
            Exit Sub
        End If
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select UserID from ManagerData where UserName='" & UID.Text & "' and PWD='" & PWD.Text & "'")
        If dr.Read Then
            Session("UserID") = dr("UserID")
            Session.Timeout = 120
            Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")
            ClassDB.UpdateDB("Update ManagerData set LogDate=LogDate2,IPAddress=IPAddress2 where UserID=" & dr("UserID") & "")
            ClassDB.UpdateDB("Update ManagerData set LogDate2='" & Now & "',IPAddress2='" & IPAddress & "' where UserID=" & dr("UserID") & "")
            Response.Redirect("adminstrator_main.aspx")
        Else
            dr.Close()
            Misc.AlertMsg(Page, "登入失敗，帳號或密碼錯誤")
            Exit Sub
        End If
        dr.Close()
    End Sub
    Function ChkData() As String
        Dim Err As String
        If Code.Text <> CStr(ViewState("V_Img")) Then
            Err = Err & "所填寫的驗證碼與所給的不符\n"
        End If
        If ChkStr(UID.Text) = False Then
            Err = Err & "帳號請輸入英文或數字\n"
        End If
        If ChkStr(PWD.Text) = False Then
            Err = Err & "密碼請輸入英文或數字\n"
        End If
        Return Err
    End Function
End Class
