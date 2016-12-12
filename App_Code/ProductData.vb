Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Public Module ProductData
    Function GetProductData(ByVal PublicID As Integer) As String
        Dim R As String
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select ProductName,ParentPublicID from product where PublicID=" & PublicID & "")
        If dr.Read Then
            R = GetProductData2(dr("ParentPublicID")) & "/" & dr("ProductName").ToString
        End If
        dr.Close()
        Return R
    End Function
    Function GetProductData2(ByVal PublicID As Integer) As String
        Dim R As String
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select Subject,ParentPublicID from product where PublicID=" & PublicID & "")
        If dr.Read Then
            If dr("ParentPublicID") <> 0 Then
                R = GetProductData2(dr("ParentPublicID")) & "/" & dr("Subject").ToString
            Else
                R = dr("Subject").ToString
            End If
        End If
        dr.Close()
        Return R
    End Function

    Function GetProductDataMeta(ByVal PublicID As Integer) As String
        Dim R As String
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select ProductName,ParentPublicID from product where PublicID=" & PublicID & "")
        If dr.Read Then
            R = GetProductData2(dr("ParentPublicID")) & "," & dr("ProductName").ToString
        End If
        dr.Close()
        Return R
    End Function
    Function GetProductDataMeta2(ByVal PublicID As Integer) As String
        Dim R As String
        Dim dr As OleDbDataReader = ClassDB.GetDataReader("select Subject,ParentPublicID from product where PublicID=" & PublicID & "")
        If dr.Read Then
            If dr("ParentPublicID") <> 0 Then
                R = GetProductDataMeta2(dr("ParentPublicID")) & "," & dr("Subject").ToString
            Else
                R = dr("Subject").ToString
            End If
        End If
        dr.Close()
        Return R
    End Function
End Module
