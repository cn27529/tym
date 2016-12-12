Public Module Page_index
    Public Function Page_VB(ByVal k As Integer, ByVal page As Integer, ByVal Pages As Integer, ByVal FromPage As Integer, ByVal ToPage As Integer, ByVal Curpage As Integer) As String
        'Numbers共幾筆,perpage每頁顯示幾個,Curpage當頁,pages共幾頁
        Dim strHTML As String
        Dim del_k, i As Integer
        del_k = k
        If (page >= Pages) Then
            FromPage = 1
            ToPage = Pages
        Else
            If (FromPage < 1) Then
                ToPage = Curpage + 1 - FromPage
                FromPage = 1
                If ((ToPage - FromPage) < page And (ToPage - FromPage) < Pages) Then
                    ToPage = page
                ElseIf (ToPage > Pages) Then
                    FromPage = Curpage - Pages + ToPage
                    ToPage = Pages
                    If ((ToPage - FromPage) < page And (ToPage - FromPage) < Pages) Then
                        FromPage = Pages - page + 1
                    End If
                End If
            End If
        End If

        If (Curpage <> 1) Then
            strHTML += "<a href=# onclick=""reloadPage2(1)"" onkeypress=""reloadPage2(1)"">←</a>"
        End If

        If (Curpage <> 1) Then
            strHTML += "&nbsp;&nbsp;<a href=#  onclick=""reloadPage2(" & (Curpage - 1) & ")"" onkeypress=""reloadPage2(" & (Curpage - 1) & ")"">上一頁</a>&nbsp;&nbsp;"
        End If

        If (Curpage - 10 >= 1) Then
            strHTML += "&nbsp;&nbsp;<a href=#  onclick=""reloadPage2(" & Curpage - 10 & ")"" onkeypress=""reloadPage2(" & Curpage - 10 & ")"">上十頁</a>&nbsp;&nbsp;"
        End If

        For i = FromPage To ToPage
            If i <= Pages Then
                If (i <> Curpage) Then
                    strHTML += "<a href=#  onclick=""reloadPage2(" & i & ")"" onkeypress=""reloadPage2(" & i & ")"">" & i & "</a> "
                Else
                    strHTML += "<span class=""m_4"">" & i & "</span>"
                    If (i <> Pages) Then
                        strHTML += "  "
                    End If
                End If
            End If
        Next
        If (Curpage + 10 <= Pages) Then
            strHTML += "&nbsp;&nbsp;<a href=#  onclick=""reloadPage2(" & Curpage + 10 & ")"" onkeypress=""reloadPage2(" & Curpage + 10 & ")"">下十頁</a>"
        End If
        If (Curpage <> Pages And Pages > 0) Then
            strHTML += "&nbsp;&nbsp;<a href=#  onclick=""reloadPage2(" & (Curpage + 1) & ")"" onkeypress=""reloadPage2(" & (Curpage + 1) & ")"">下一頁</a>"
        End If
        If Pages > 0 Then
            strHTML += ("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<select class=""c_2"" name=""page"" onChange=""reloadPage()"" runat=""server"">")
            Dim cs As String
            For i = 1 To Pages
                If Curpage = i Then cs = "selected" Else cs = ""
                strHTML += ("<option value=" & i & " " & cs & ">&nbsp;" & i & "&nbsp;</option>")
            Next
            strHTML += ("</select>")
        Else
            strHTML += "<input type=hidden id=page runat=server>"
        End If
        Return strHTML
    End Function
    Public Function Page_VBEN(ByVal k As Integer, ByVal page As Integer, ByVal Pages As Integer, ByVal FromPage As Integer, ByVal ToPage As Integer, ByVal Curpage As Integer) As String
        'Numbers共幾筆,perpage每頁顯示幾個,Curpage當頁,pages共幾頁
        Dim strHTML As String
        Dim del_k, i As Integer
        del_k = k
        If (page >= Pages) Then
            FromPage = 1
            ToPage = Pages
        Else
            If (FromPage < 1) Then
                ToPage = Curpage + 1 - FromPage
                FromPage = 1
                If ((ToPage - FromPage) < page And (ToPage - FromPage) < Pages) Then
                    ToPage = page
                ElseIf (ToPage > Pages) Then
                    FromPage = Curpage - Pages + ToPage
                    ToPage = Pages
                    If ((ToPage - FromPage) < page And (ToPage - FromPage) < Pages) Then
                        FromPage = Pages - page + 1
                    End If
                End If
            End If
        End If

        If (Curpage <> 1) Then
            strHTML += "<a href=# onclick=""reloadPage2(1)"" onkeypress=""reloadPage2(1)"">←</a> "
        End If

        If (Curpage <> 1) Then
            strHTML += "&nbsp;&nbsp;<a href=#  onclick=""reloadPage2(" & (Curpage - 1) & ")"" onkeypress=""reloadPage2(" & (Curpage - 1) & ")"">Previous page</a>&nbsp;&nbsp;"
        End If

        If (Curpage - 10 >= 1) Then
            strHTML += "&nbsp;&nbsp;<a href=#  onclick=""reloadPage2(" & Curpage - 10 & ")"" onkeypress=""reloadPage2(" & Curpage - 10 & ")"">Previous 10 page</a>&nbsp;&nbsp;"
        End If

        For i = FromPage To ToPage
            If i <= Pages Then
                If (i <> Curpage) Then
                    strHTML += "<a href=#  onclick=""reloadPage2(" & i & ")"" onkeypress=""reloadPage2(" & i & ")"">" & i & "</a> "
                Else
                    strHTML += "<span class=""m_4"">" & i & "</span>"
                    If (i <> Pages) Then
                        strHTML += "  "
                    End If
                End If
            End If
        Next
        If (Curpage + 10 <= Pages) Then
            strHTML += "&nbsp;&nbsp;<a href=#  onclick=""reloadPage2(" & Curpage + 10 & ")"" onkeypress=""reloadPage2(" & Curpage + 10 & ")"">Next 10 page</a>"
        End If
        If (Curpage <> Pages And Pages > 0) Then
            strHTML += "&nbsp;&nbsp;<a href=#  onclick=""reloadPage2(" & (Curpage + 1) & ")"" onkeypress=""reloadPage2(" & (Curpage + 1) & ")"">Next page</a>"
        End If
        If Pages > 0 Then
            strHTML += ("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<select class=""c_2"" name=""page"" onChange=""reloadPage()"" runat=""server"">")
            Dim cs As String
            For i = 1 To Pages
                If Curpage = i Then cs = "selected" Else cs = ""
                strHTML += ("<option value=" & i & " " & cs & ">&nbsp;" & i & "&nbsp;</option>")
            Next
            strHTML += ("</select>")
        Else
            strHTML += "<input type=hidden id=page runat=server>"
        End If
        Return strHTML
    End Function
End Module
