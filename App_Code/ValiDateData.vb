Imports Microsoft.VisualBasic

Public Module ValiDateDataa
    Dim Injection As String = ConfigurationManager.AppSettings("Injection").ToString
    Public Function Sql_Injection(ByVal Injection_chk As String) As Boolean
        Dim chk() As String = Injection.Split(",")
        Dim i As Integer
        Dim chk2 As String = "true"
        For i = 0 To UBound(chk)
            If UCase(Injection_chk).Replace(UCase(chk(i)), "") <> UCase(Injection_chk) Then
                Return False
                chk2 = "false"
            End If
        Next
        If chk2 = "true" Then Return True
    End Function
    Public Function ChkStr(ByVal Str As String) As Boolean
        Dim Chk As Boolean = True
        If Str <> "" Then
            Dim s As String = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
            Dim i As Integer
            For i = 0 To Str.Length - 1
                Dim o As String = Str.Substring(i, 1)
                If s.IndexOf(o) = -1 Then
                    Chk = False
                    Exit For
                End If
            Next
        Else
            Chk = False
        End If
        Return Chk
    End Function
    Public Function ChkIsNumeric(ByVal Str As String) As Boolean
        Dim Chk As Boolean = True
        If Str <> "" Then
            Dim s As String = "0123456789"
            Dim i As Integer
            For i = 0 To Str.Length - 1
                Dim o As String = Str.Substring(i, 1)
                If s.IndexOf(o) = -1 Then
                    Chk = False
                    Exit For
                End If
            Next
        Else
            Chk = False
        End If
        Return Chk
    End Function
End Module
