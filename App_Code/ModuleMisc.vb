''' <summary>
''' 雜項模組服務
''' </summary>
''' <remarks></remarks>
Public Module Misc

    ''' <summary>
    ''' Javascript突顯式對話框
    ''' </summary>
    ''' <param name="webform"></param>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Sub AlertMsg(ByVal Webform As System.Web.UI.Page, ByVal Message As String)

        Dim script As String = "alert('" & Message & "');"
        Webform.ClientScript.RegisterStartupScript(Webform.Page.GetType, "waring", script, True)
    End Sub
End Module
