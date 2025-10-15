Imports System.Data.OleDb

Public Class DatabaseHelper
    Private Shared Function GetConnectionString() As String
        Return DatabaseConfig.ConnectionString
    End Function

    Public Shared Function GetConnection() As OleDbConnection
        Return New OleDbConnection(GetConnectionString())
    End Function

    Public Shared Function TestConnection() As Boolean
        Try
            Using con As OleDbConnection = GetConnection()
                con.Open()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function ExecuteQuery(query As String, parameters As Dictionary(Of String, Object)) As Boolean
        Try
            Using con As OleDbConnection = GetConnection()
                con.Open()
                Using cmd As New OleDbCommand(query, con)
                    For Each param In parameters
                        cmd.Parameters.AddWithValue(param.Key, param.Value)
                    Next
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show("Error executing query: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Shared Function GetDataTable(query As String, Optional parameters As Dictionary(Of String, Object) = Nothing) As DataTable
        Dim dt As New DataTable()
        Try
            Using con As OleDbConnection = GetConnection()
                con.Open()
                Using cmd As New OleDbCommand(query, con)
                    If parameters IsNot Nothing Then
                        For Each param In parameters
                            cmd.Parameters.AddWithValue(param.Key, param.Value)
                        Next
                    End If
                    Using da As New OleDbDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return dt
    End Function
End Class