Imports System.Data.OleDb
Imports System.IO
Imports System.Text

Public Class frmActivityLog
    Dim con As OleDbConnection
    Dim da As OleDbDataAdapter
    Dim dt As DataTable
    Dim cmd As OleDbCommand

    Private Sub frmActivityLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con = DatabaseConfig.GetConnection()
        LoadFilterTypes()
        LoadData()
        ClearFilters()
        Me.Text = "Activity Log Management"
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub LoadFilterTypes()
        cmbFilterType.Items.Clear()
        cmbFilterType.Items.Add("All")
        cmbFilterType.Items.Add("User")
        cmbFilterType.Items.Add("Activity Type")
        cmbFilterType.Items.Add("Description")
        cmbFilterType.SelectedIndex = 0
    End Sub

    Private Sub LoadData(Optional sql As String = "")
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            If String.IsNullOrWhiteSpace(sql) Then
                da = New OleDbDataAdapter("SELECT LogID, UserID, ActivityType, Description, Format(ActivityDate,'yyyy-MM-dd') AS [Date] FROM tblActivityLog ORDER BY ActivityDate DESC", con)
            Else
                da = New OleDbDataAdapter(sql, con)
            End If

            dt = New DataTable()
            da.Fill(dt)
            DataGridView1.DataSource = dt

            lblTotalRecords.Text = "Total Records: " & dt.Rows.Count.ToString()
            lblActivitySummary.Text = $"Summary: Showing {dt.Rows.Count} Records"

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub ClearFilters()
        txtSearch.Clear()
        dtpFrom.Value = Date.Now.AddMonths(-1)
        dtpTo.Value = Date.Now
        cmbFilterType.SelectedIndex = 0
    End Sub

    ' Filter by search term
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim searchTerm As String = "%" & txtSearch.Text.Trim() & "%"
        Dim filterColumn As String = ""

        Select Case cmbFilterType.SelectedItem.ToString()
            Case "User"
                filterColumn = "UserID"
            Case "Activity Type"
                filterColumn = "ActivityType"
            Case "Description"
                filterColumn = "Description"
            Case Else
                filterColumn = "UserID OR ActivityType OR Description"
        End Select

        Dim sql As String
        If filterColumn.Contains("OR") Then
            sql = "SELECT LogID, UserID, ActivityType, Description, Format(ActivityDate,'yyyy-MM-dd') AS [Date] FROM tblActivityLog " &
                  "WHERE UserID LIKE ? OR ActivityType LIKE ? OR Description LIKE ? ORDER BY ActivityDate DESC"
        Else
            sql = $"SELECT LogID, UserID, ActivityType, Description, Format(ActivityDate,'yyyy-MM-dd') AS [Date] FROM tblActivityLog WHERE {filterColumn} LIKE ? ORDER BY ActivityDate DESC"
        End If

        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            da = New OleDbDataAdapter(sql, con)
            If filterColumn.Contains("OR") Then
                da.SelectCommand.Parameters.AddWithValue("@1", searchTerm)
                da.SelectCommand.Parameters.AddWithValue("@2", searchTerm)
                da.SelectCommand.Parameters.AddWithValue("@3", searchTerm)
            Else
                da.SelectCommand.Parameters.AddWithValue("@1", searchTerm)
            End If

            dt = New DataTable()
            da.Fill(dt)
            DataGridView1.DataSource = dt
            lblTotalRecords.Text = "Total Records: " & dt.Rows.Count.ToString()
            lblActivitySummary.Text = $"Summary: Showing {dt.Rows.Count} Records"
        Catch ex As Exception
            MessageBox.Show("Error searching: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    ' Filter by date range
    Private Sub btnDateFilter_Click(sender As Object, e As EventArgs) Handles btnDateFilter.Click
        Dim fromDate As String = dtpFrom.Value.ToString("yyyy-MM-dd")
        Dim toDate As String = dtpTo.Value.ToString("yyyy-MM-dd")
        Dim sql As String = "SELECT LogID, UserID, ActivityType, Description, Format(ActivityDate,'yyyy-MM-dd') AS [Date] " &
                            "FROM tblActivityLog WHERE ActivityDate BETWEEN ? AND ? ORDER BY ActivityDate DESC"

        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            da = New OleDbDataAdapter(sql, con)
            da.SelectCommand.Parameters.AddWithValue("@1", fromDate)
            da.SelectCommand.Parameters.AddWithValue("@2", toDate)
            dt = New DataTable()
            da.Fill(dt)
            DataGridView1.DataSource = dt
            lblTotalRecords.Text = "Total Records: " & dt.Rows.Count.ToString()
            lblActivitySummary.Text = $"Summary: Showing {dt.Rows.Count} Records"
        Catch ex As Exception
            MessageBox.Show("Error filtering by date: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    ' Refresh button
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadData()
        ClearFilters()
    End Sub

    ' Clear old logs (90+ days)
    Private Sub btnClearOldLogs_Click(sender As Object, e As EventArgs) Handles btnClearOldLogs.Click
        If MessageBox.Show("Are you sure you want to delete logs older than 90 days?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Try
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()
                cmd = New OleDbCommand("DELETE FROM tblActivityLog WHERE ActivityDate < ?", con)
                cmd.Parameters.AddWithValue("@1", Date.Now.AddDays(-90))
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                MessageBox.Show($"{rowsAffected} old log(s) deleted.")
                LoadData()
            Catch ex As Exception
                MessageBox.Show("Error clearing old logs: " & ex.Message)
            Finally
                If con.State = ConnectionState.Open Then con.Close()
            End Try
        End If
    End Sub

    ' Export to CSV
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            MessageBox.Show("No data to export.")
            Return
        End If

        Using sfd As New SaveFileDialog()
            sfd.Filter = "CSV files (*.csv)|*.csv"
            sfd.FileName = "ActivityLogExport.csv"
            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    Using sw As New StreamWriter(sfd.FileName, False, Encoding.UTF8)
                        ' Write header
                        Dim columnNames = dt.Columns.Cast(Of DataColumn).Select(Function(c) c.ColumnName)
                        sw.WriteLine(String.Join(",", columnNames))

                        ' Write rows
                        For Each row As DataRow In dt.Rows
                            Dim fields = row.ItemArray.Select(Function(f) """" & f.ToString().Replace("""", """""") & """")
                            sw.WriteLine(String.Join(",", fields))
                        Next
                    End Using
                    MessageBox.Show("Export successful!")
                Catch ex As Exception
                    MessageBox.Show("Error exporting CSV: " & ex.Message)
                End Try
            End If
        End Using
    End Sub
End Class
