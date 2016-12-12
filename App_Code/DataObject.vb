Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb 
Imports System.Web
''' <summary>
''' ��Ʈw�s���Ҳ�
''' </summary>
''' <remarks></remarks>
Public Module ClassDB

    ''' <summary>
    ''' ��Ʈw�s�u�r��
    ''' </summary>
    ''' <remarks></remarks>
    'Private CnnString As String = System.Configuration.ConfigurationSettings.AppSettings("ConnData")
    Private CnnString As String = ConfigurationSettings.AppSettings("myds") + HttpContext.Current.Request.MapPath("~") + (ConfigurationSettings.AppSettings("myconn").Trim())

    ''' <summary>
    ''' ���o�å��}��Ʈw�s������
    ''' </summary>
    ''' <returns>��Ʈw�s������</returns>
    ''' <remarks></remarks>
    Private Function GetConnection() As OleDbConnection

        Dim ret_conn As OleDbConnection
        ret_conn = New OleDbConnection(CnnString)

        ret_conn.Open()
        GetConnection = ret_conn
    End Function



    ''' <summary>
    ''' ������Ʈw�s������
    ''' </summary>
    ''' <param name="conn">��Ʈw�s������</param>
    ''' <remarks></remarks>
    Private Sub CloseConnection(ByVal conn As OleDbConnection)
        conn.Close()
        conn = Nothing
    End Sub


#Region " getReturnValue(RV_Para-String, RVAL_Para-ArrayList, DS_Para-DataSet, DR_Para-OleDbDataReader) "


    ''' <summary>
    ''' �Ǧ^OleDbDataReader
    ''' </summary>
    ''' <param name="strSQL">SQL�d�ߦr��</param>
    ''' <returns>OleDbDataReader����</returns>
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
            ' ModuleWriteLog.WriteLog("Sql�s�u���~->" & strSP, ex.Message & ex.Source & ex.StackTrace)
        Finally
            CloseConnection(cn)
        End Try
        Return ds

    End Function
#End Region



#Region " SQLDataTransaction(Insert�BUpdate�BDelete) "

    ''' <summary>
    ''' ��Ʈw��s�d��
    ''' </summary>
    ''' <param name="strSP">�d�ߦr��</param>
    ''' <remarks></remarks>
    Public Sub UpdateDB(ByVal strSP As String)

        Dim cn As OleDbConnection = GetConnection()

        '��ƥ���}�l
        'Dim myTrans As SqlTransaction = cn.BeginTransaction(IsolationLevel.ReadCommitted, "DataTransaction")

        Try
            Dim cmd As New OleDbCommand(strSP, cn)
            cmd.CommandType = CommandType.Text


            cmd.ExecuteNonQuery()
            cmd.Dispose()

            '�������
            ' myTrans.Commit()
        Catch ex As Exception
        Finally
            CloseConnection(cn)
        End Try

    End Sub

#End Region

End Module
