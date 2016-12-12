Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Public Module File
    Public Function ChangeFileName(ByVal FileName As String) As String
        Dim AddString As String = ""
        Dim NewFileName As String = ""
        If FileName.Length > 0 Then
            AddString = "_" & Now.ToString("mmssff", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Dim i As Integer = 0
            i = FileName.LastIndexOf(".")

            If i >= 0 Then
                NewFileName = FileName.Substring(0, i) & AddString & FileName.Substring(i, FileName.Length - i)
            End If
        End If

        Return NewFileName
    End Function
    Public Function GetFileData(ByVal UploadPath As String, ByVal FilePath As String, ByVal FileName As String) As String
        Dim R As String
        If FileName <> "" Then
            R = "<a href=""" & UploadPath.Replace("~", "..") & FilePath & FileName & """ target=""_blank"">" & FileName & "</a>"
        End If
        Return R
    End Function
    Public Function GetFileHtml(ByVal UploadPath As String, ByVal FilePath As String, ByVal FileName As String) As String
        Dim R As String
        Dim ArrFileName() As String
        If FileName <> "" Then
            ArrFileName = FileName.Split(".")
            Dim Url As String = UploadPath.Replace("~/", "") & FilePath & FileName
            If ArrFileName(UBound(ArrFileName)).ToLower = "swf" Then
                R = R & "<div id=""pic_4"" style=""display:""><object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"" width=""120"" title=""" & Url & """>"
                R = R & "<param name=""movie"" value=""" & Url & """ />"
                R = R & "<param name=""quality"" value=""high"" />"
                R = R & "<embed src=""" & Url & """ quality=""high"" pluginspage=""http://www.macromedia.com/go/getflashplayer"" type=""application/x-shockwave-flash"" width=""120""></embed>" & FileName
                R = R & "</object></div>"
            Else
                R = "<div id=""pic_4"" style=""display:""><a href=""" & Url & """ target=""_blank""><img src=""" & Url & """ alt="""" width=""120"" border=""0""></a></div>"
            End If
        End If
        Return R
    End Function
    Public Function GetFileData2(ByVal UploadPath As String, ByVal FID As Integer) As String
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select FileName,FilePath from AttFiles where FID=" & FID & "")
        Dim R As String
        If dr.Read Then
            R = "<a href=""" & UploadPath.Replace("~", "..") & dr("FilePath") & dr("FileName") & """ target=""_blank"">" & dr("FileName") & "</a>"
        End If
        dr.Close()
        Return R
    End Function
    Public Function GetFileHtmlSmallList(ByVal UploadPath As String, ByVal FID As Integer) As String
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select FileName,FilePath from AttFiles where FID=" & FID & "")
        Dim R As String
        Dim ArrFileName() As String
        If dr.Read Then
            Dim Url As String = UploadPath.Replace("~/", "") & dr("FilePath") & dr("FileName")
            ArrFileName = dr("FileName").Split(".")
            If ArrFileName(UBound(ArrFileName)).ToLower = "swf" Then
                R = R & "<div id=""pic_4"" style=""display:""><object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"" width=""128"" title=""" & Url & """>"
                R = R & "<param name=""movie"" value=""" & Url & """ />"
                R = R & "<param name=""quality"" value=""high"" />"
                R = R & "<embed src=""" & Url & """ quality=""high"" pluginspage=""http://www.macromedia.com/go/getflashplayer"" type=""application/x-shockwave-flash"" width=""128""></embed>" & dr("FileName")
                R = R & "</object></div>"
            Else
                R = "<img src=""" & Url & """ alt="""" width=""128"" border=""0"">"
            End If
        Else
            R = "<img src=""images/pic_cno.jpg"" alt="""" width=""128"" border=""0"">"
        End If
        dr.Close()
        Return R
    End Function
    Public Function GetFileHtmlSmall(ByVal UploadPath As String, ByVal FID As Integer) As String
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select FileName,FilePath from AttFiles where FID=" & FID & "")
        Dim R As String
        Dim ArrFileName() As String
        If dr.Read Then
            Dim Url As String = UploadPath.Replace("~/", "") & dr("FilePath") & dr("FileName")
            ArrFileName = dr("FileName").Split(".")
            If ArrFileName(UBound(ArrFileName)).ToLower = "swf" Then
                R = R & "<div id=""pic_4"" style=""display:""><object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"" width=""120"" title=""" & Url & """>"
                R = R & "<param name=""movie"" value=""" & Url & """ />"
                R = R & "<param name=""quality"" value=""high"" />"
                R = R & "<embed src=""" & Url & """ quality=""high"" pluginspage=""http://www.macromedia.com/go/getflashplayer"" type=""application/x-shockwave-flash"" width=""120""></embed>" & dr("FileName")
                R = R & "</object></div>"
            Else
                R = "<img src=""" & Url & """ alt="""" width=""120"" border=""0"">"
            End If
        Else
            R = "<img src=""images/pic_cno.jpg"" alt="""" width=""120"" border=""0"">"
        End If
        dr.Close()
        Return R
    End Function
    Public Function GetFileHtmlBig(ByVal UploadPath As String, ByVal FID As Integer) As String
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select FileName,FilePath from AttFiles where FID=" & FID & "")
        Dim R As String
        Dim ArrFileName() As String
        If dr.Read Then
            Dim Url As String = UploadPath.Replace("~/", "") & dr("FilePath") & dr("FileName")
            ArrFileName = dr("FileName").Split(".")
            If ArrFileName(UBound(ArrFileName)).ToLower = "swf" Then
                R = R & "<div id=""pic_4"" style=""display:""><object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"" width=""500"" title=""" & Url & """>"
                R = R & "<param name=""movie"" value=""" & Url & """ />"
                R = R & "<param name=""quality"" value=""high"" />"
                R = R & "<embed src=""" & Url & """ quality=""high"" pluginspage=""http://www.macromedia.com/go/getflashplayer"" type=""application/x-shockwave-flash"" width=""500""></embed>" & dr("FileName")
                R = R & "</object></div>"
            Else
                R = "<img src=""" & Url & """ alt="""" width=""500"" border=""0"">"
            End If
        Else
            R = "" ' "<img src=""images/bigpic_1.jpg"" alt="""" width=""500"" border=""0"">"
        End If
        dr.Close()
        Return R
    End Function
End Module
