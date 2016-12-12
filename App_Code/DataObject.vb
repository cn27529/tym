Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb 
Imports System.Web
''' <summary>
''' 資料庫存取模組
''' </summary>
''' <remarks></remarks>
Public Module ClassDB

    ''' <summary>
    ''' 資料庫連線字串
    ''' </summary>
    ''' <remarks></remarks>
    'Private CnnString As String = System.Configuration.ConfigurationSettings.AppSettings("ConnData")
    Private CnnString As String = ConfigurationSettings.AppSettings("myds") + HttpContext.Current.Request.MapPath("~") + (ConfigurationSettings.AppSettings("myconn").Trim())

    ''' <summary>
    ''' 取得並打開資料庫連結物件
    ''' </summary>
    ''' <returns>資料庫連結物件</returns>
    ''' <remarks></remarks>
    Private Function GetConnection() As OleDbConnection

        Dim ret_conn As OleDbConnection
        ret_conn = New OleDbConnection(CnnString)

        ret_conn.Open()
        GetConnection = ret_conn
    End Function



    ''' <summary>
    ''' 關閉資料庫連結物件
    ''' </summary>
    ''' <param name="conn">資料庫連結物件</param>
    ''' <remarks></remarks>
    Private Sub CloseConnection(ByVal conn As OleDbConnection)
        conn.Close()
        conn = Nothing
    End Sub


#Region " getReturnValue(RV_Para-String, RVAL_Para-ArrayList, DS_Para-DataSet, DR_Para-OleDbDataReader) "


    ''' <summary>
    ''' 傳回OleDbDataReader
    ''' </summary>
    ''' <param name="strSQL">SQL查詢字串</param>
    ''' <returns>OleDbDataReader物件</returns>
    ''' <remarks></remarks>
    Public Function GetDataReader(ByVal strSQL As String) As OleDbDataReader

        Dim cn As OleDbConnection = GetConnection()
        Dim rdr As OleDbDataReader

        Dim cmd As New OleDbCommand(strSQL, cn)
        rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        cmd.Dispose()

        Return rdr

    End Function
    Public Function RunReturnDataSet(ByVal strSP As String, ByVal DataTableName As String) As DataSet

        Dim cn As OleDbConnection = GetConnection()

        Dim ds As New DataSet
        Try

            Dim da As New OleDbDataAdapter(strSP, cn)

            da.Fill(ds, DataTableName)

            da.Dispose()

        Catch ex As Exception
            ' ModuleWriteLog.WriteLog("Sql連線錯誤->" & strSP, ex.Message & ex.Source & ex.StackTrace)
        Finally
            CloseConnection(cn)
        End Try
        Return ds

    End Function
#End Region



#Region " SQLDataTransaction(Insert、Update、Delete) "

    ''' <summary>
    ''' 資料庫更新查詢
    ''' </summary>
    ''' <param name="strSP">查詢字串</param>
    ''' <remarks></remarks>
    Public Sub UpdateDB(ByVal strSP As String)

        Dim cn As OleDbConnection = GetConnection()

        '資料交易開始
        'Dim myTrans As SqlTransaction = cn.BeginTransaction(IsolationLevel.ReadCommitted, "DataTransaction")

        Try
            Dim cmd As New OleDbCommand(strSP, cn)
            cmd.CommandType = CommandType.Text


            cmd.ExecuteNonQuery()
            cmd.Dispose()

            '交易完成
            ' myTrans.Commit()
        Catch ex As Exception
        Finally
            CloseConnection(cn)
        End Try

    End Sub

#End Region

End Module
