Imports System
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Partial Class VerifyCode_V_Img
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在這裡放置使用者程式碼以初始化網頁
        'RndNum是一個自訂函數
        Dim VNum As String = RndNum(4)
        Session("V_Img") = VNum
        ValidateCode(VNum)
    End Sub
    '產生圖像驗證碼函數
    Sub ValidateCode(ByVal VNum)
        Dim Img As System.Drawing.Bitmap
        Dim g As Graphics
        Dim ms As MemoryStream
        Dim gheight As Integer = Int(Len(VNum) * 12)
        'gheight為圖片寬度,根據字型長度自動更改圖片寬度
        Img = New Bitmap(gheight, 19)
        g = Graphics.FromImage(Img)
        g.DrawString(VNum, (New Font("Tahoma", 9)), (New SolidBrush(Color.forestgreen)), 6, 2) '在矩形內繪製字串（字串,字體,畫筆顏色,左上x.左上y）
        ms = New MemoryStream
        Img.Save(ms, ImageFormat.Png)
        Response.ClearContent() '需要輸出圖像資訊 要修改HTTP頭
        Response.ContentType = "image/Png"
        Response.BinaryWrite(ms.ToArray())
        g.Dispose()
        Img.Dispose()
        Response.End()
    End Sub
    '--------------------------------------------
    '函數名稱:RndNum
    '函數參數:VcodeNum--設定返回隨機字型串的位數
    '函數功能:產生數字和字型混合的隨機字型串
    Function RndNum(ByVal VcodeNum) As String
        Dim Vchar As String = "1,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,k,m,n,z,p,q,r,s,t,u,w,x,y,z"
        Dim VcArray() As String = Split(Vchar, ",") '將字型串產生數組
        Dim VNum As String = ""
        Dim i As Byte
        For i = 1 To VcodeNum
            Randomize()
            VNum = VNum & VcArray(Int(35 * Rnd())) '數組一般從0開始讀取,所以這裡為35*Rnd
        Next
        Return VNum
    End Function
End Class
