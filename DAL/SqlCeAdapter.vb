Imports System.Data.SqlServerCe
Imports System.Data

Public Class SqlCeAdapter
    '' 

    ''' The connection object used for communicating with the database
    ''' 
    Private sqlConnection As SqlCeConnection

    '' 
    ''' Creates a new SimpleSqlCeAdapter object for an unencrypted
    ''' database.
    ''' 
    ''' The path to the database file
    Public Sub New(ByVal path As String)
        Dim connectionString As String = "Data Source=" & path
        sqlConnection = New SqlCeConnection(connectionString)
    End Sub
    '' 

    ''' Creates a new SimpleSqlCeAdapter object
    ''' 
    ''' The connection string used for 
    ''' connecting to the database
    ''' The path to the database file
    Public Sub New(ByVal path As String, ByVal connection As String)
        Dim connectionString As String = connection & path
        sqlConnection = New SqlCeConnection(connectionString)
    End Sub
    '' 

    ''' Queries the database for a DataTable object
    ''' 
    ''' A SQL SELECT statment
    ''' A datatable if the sql statment is correct. 
    ''' Otherwise it returns Nothing
    Public Function QueryDatabaseForTable(ByVal queryCommand As String, Optional ByVal strparam As String = "") As DataTable
        Try
            Dim table1 As New DataTable("Table1")

            Dim sqlCommand As New SqlCeCommand(queryCommand, sqlConnection)

            If strparam > "" Then
                Dim param As SqlCeParameter = Nothing
                param = New SqlCeParameter("@id", SqlDbType.NVarChar, 36)
                sqlCommand.Parameters.Add(param)
                sqlCommand.Parameters(0).Value = strparam
            End If

            sqlConnection.Open()
            sqlCommand.Prepare()
            Dim sqlReader = sqlCommand.ExecuteReader()
            table1.Load(sqlReader)
            sqlReader.Close()
            Return table1
        Catch ex As Exception
            Console.Error.Write(ex.Message)
            Return Nothing
        Finally
            sqlConnection.Close()
        End Try
    End Function
    '' 

    ''' Queries the database for a String object.
    ''' 
    ''' A SQL SELECT statement that
    ''' returns a single value.
    ''' 
    Public Function QueryDatabaseForScalar(ByVal queryCommand As String) _
                    As String
        Try
            Dim sqlCommand As New SqlCeCommand(queryCommand, sqlConnection)
            sqlConnection.Open()
            Return CType(sqlCommand.ExecuteScalar(), String)
        Catch ex As Exception
            Console.Error.Write(ex.Message)
            Return Nothing
        Finally
            sqlConnection.Close()
        End Try
    End Function
    '' 

    ''' Executes a SQL statement which returns no value (Which is not
    ''' a query).
    ''' 
    ''' The SQL statement which will be
    ''' executed
    Public Sub ExecuteNonQuery(ByVal queryCommand As String)
        Try
            Dim sqlCommand As New SqlCeCommand(queryCommand, sqlConnection)
            sqlConnection.Open()
            sqlCommand.ExecuteNonQuery()
        Catch ex As Exception
            Console.Error.Write(ex.Message)
        Finally
            sqlConnection.Close()
        End Try
    End Sub
End Class