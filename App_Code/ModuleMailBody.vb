Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Net
Imports System
Imports System.Net.Mail
Public Module ModuleMailBody
    Dim SiteDomainName As String = System.Configuration.ConfigurationManager.AppSettings("SiteDomainName").ToString
    'Private ePaperUID As String = System.Configuration.ConfigurationManager.AppSettings("ePaperUID").ToString
    'Private ePaperPWD As String = System.Configuration.ConfigurationManager.AppSettings("ePaperPWD").ToString
    Private mMailServer As String = System.Configuration.ConfigurationManager.AppSettings("MailServer").ToString
    Private mPort As String = System.Configuration.ConfigurationManager.AppSettings("MailServerPort").ToString
    Private mMailFrom As String = System.Configuration.ConfigurationManager.AppSettings("MailFrom").ToString
    Public Sub SendMail( _
ByVal MailSubject As String, _
ByVal MailBody As String, _
ByVal MailTo As String, _
ByVal mMailFromDisplayName As String, _
Optional ByVal MailFrom As String = "", _
Optional ByVal IsBodyHtml As Boolean = False)

        'No Send Mail when mail server is Not Ready
        If mMailServer Is Nothing OrElse mMailServer = "" OrElse mMailServer.Length = 0 Then Exit Sub

        Try

            Dim Mailmsg As New System.Net.Mail.MailMessage
            Mailmsg.IsBodyHtml = IsBodyHtml '為html內容格式
            Mailmsg.Subject = MailSubject
            Mailmsg.Body = MailBody
            Mailmsg.From = New Net.Mail.MailAddress(mMailFrom, mMailFromDisplayName)

            If MailTo.IndexOf(";") = -1 Then
                Mailmsg.To.Add(MailTo)
            Else
                Dim strCC() As String = Split(MailTo, ";")
                Dim strThisCC As String
                For Each strThisCC In strCC
                    Mailmsg.To.Add(Trim(strThisCC))
                Next
            End If
            Dim SmtpClient As New SmtpClient(mMailServer, mPort)
            SmtpClient.UseDefaultCredentials = True
            'If ePaperUID <> "" Then
            '    SmtpClient.Credentials = New System.Net.NetworkCredential(ePaperUID, ePaperPWD)
            'End If
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network
            SmtpClient.Send(Mailmsg)

            Mailmsg = Nothing
            SmtpClient = Nothing
        Catch
        End Try
    End Sub
    Function GetMailBody(ByVal BodyPath As String) As String
        '取得完整的執行中網址(包含特殊的Port)
        If Not SiteDomainName.EndsWith("/") Then SiteDomainName = SiteDomainName & "/"
        Dim request As WebRequest = WebRequest.Create(SiteDomainName & BodyPath)
        request.Credentials = CredentialCache.DefaultCredentials
        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
        Dim dataStream As Stream = response.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd()
        reader.Close()
        dataStream.Close()
        response.Close()
        Return responseFromServer
    End Function
End Module
