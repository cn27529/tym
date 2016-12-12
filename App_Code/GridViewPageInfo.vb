Imports Microsoft.VisualBasic
Imports System.Data

Public Module GridViewPageInfo
    '���oGridView�`����/���X���
    Public Sub GetGridViewInfo(ByVal GridView As System.Web.UI.WebControls.GridView, ByVal Webform As System.Web.UI.Page, Optional ByVal SqlDataSource As SqlDataSource = Nothing)
        If GridView.Rows.Count > 0 Then
            'Dim bottomPagerRow As GridViewRow = GridView.BottomPagerRow
            'GridView.Caption = "<span class=""PageInfo"">�ثe�Ҧb���X(" & GridView.PageIndex + 1 & "/" & GridView.PageCount & ")  </span>"
            'GridView.CaptionAlign = TableCaptionAlign.Right
            Dim lb As New Literal

            '���Ƥέ��X���
            If SqlDataSource IsNot Nothing Then
                Dim dt As DataTable = CType(SqlDataSource.Select(New DataSourceSelectArguments), DataView).Table
                Dim PageSize As Integer = GridView.PageSize
                Dim TotalRowCount As Integer = dt.Rows.Count
                Dim TotalPageCount As Integer = Math.Ceiling(TotalRowCount / PageSize)
                'GridView.Caption = "<span class=""PageInfo"">���X " & GridView.PageIndex + 1 & " / " & TotalPageCount & " �� �`�p " & TotalRowCount & " �����</span>>20 �� / �C����ܵ���"
                GridView.Caption = "<TABLE cellSpacing=0 cellPadding=3 width=""100%"" align=center border=0>"
                GridView.Caption += "<TR>"
                GridView.Caption += "<TD vAlign=bottom align=left width=""47%"" height=20>�@��:<SPAN class=searchpdno> " & TotalRowCount & " </SPAN>��, ����<SPAN class=news_pageNo> " & GridView.PageIndex + 1 & " </SPAN>/ " & TotalPageCount & "</TD>"
                GridView.Caption += " <TD vAlign=bottom align=right width=""53%"">20 �� / �C����ܵ���</TD>"
                GridView.Caption += "</TR>"
                GridView.Caption += "</table>"
            End If
            SetCss(GridView)
        Else
            GridView.Caption = ""
            SetCss(GridView)
        End If

    End Sub
    Private Sub SetCss(ByVal gv As GridView)
        'gv.CssClass = "Dg"
        'gv.FooterStyle.CssClass = "DgFooter"
        gv.RowStyle.CssClass = "tableCellTwo"
        gv.HeaderStyle.VerticalAlign = VerticalAlign.Bottom
        'gv.AlternatingRowStyle.CssClass = "DgAltItem"
        gv.PagerStyle.CssClass = "DgPager"
        gv.HeaderStyle.CssClass = "tableCellOne"
        'gv.SelectedRowStyle.CssClass = "DgItem_edit"
        'gv.SelectedRowStyle.CssClass = "DgItem_edit"
        gv.EmptyDataText = "<tr><td height=160 vAlign=top class=tableCellTwo_1><SPAN >�ثe�S����ơA�αz���d�ߨS����ơC</span></td></tr>"
        gv.PagerSettings.Mode = PagerButtons.NumericFirstLast
        gv.PagerSettings.Position = PagerPosition.Bottom
        'gv.PagerSettings.PageButtonCount = 20
        'gv.PagerStyle.
    End Sub
End Module
