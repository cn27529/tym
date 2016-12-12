Imports System.Data
Imports System.Data.OleDb
Partial Class adminstrator_adminstrator_mod
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ImageEdit.Attributes.Add("onclick", "return Chk()")
        ImageEdit.Attributes.Add("onkeypress", "return Chk()")
    End Sub

    Protected Sub ImageEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageEdit.Click
        Dim Dscript As String = ChkData()
        If Dscript <> "" Then
            Misc.AlertMsg(Page, Dscript)
            Exit Sub
        End If
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select UserID from ManagerData where UserID=" & Session("UserID") & " and PWd='" & OLDPWD.Text & "'")
        Dim chk As Boolean = False
        If dr.Read Then
            chk = True
        End If
        dr.Close()
        If chk = False Then
            Misc.AlertMsg(Page, "舊密碼輸入錯誤，變更密碼失敗")
        Else
            ClassDB.UpdateDB("update ManagerData set PWd='" & PWD2.Text & "' where UserID=" & Session("UserID") & "")
            Misc.AlertMsg(Page, "變更密碼成功")
        End If
    End Sub
    Function ChkData() As String
        Dim Err
        If ChkStr(OLDPWD.Text) = False Then
            Err = Err & "舊密碼只能輸入英數字\n"
        End If
        If ChkStr(PWD1.Text) = False Then
            Err = Err & "新密碼只能輸入英數字\n"
        End If
        If PWD1.Text.Length < 4 Then
            Err = Err & "新密碼請至少輸入4個字元\n"
        End If
        Return Err
    End Function
End Class
