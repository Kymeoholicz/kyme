Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports System.Text

Public Class frmUserManagement
    Dim con As OleDbConnection
    Dim cmd As OleDbCommand
    Dim da As OleDbDataAdapter
    Dim dt As DataTable
    Dim selectedUserID As Integer = 0

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmUserManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Only admins can manage users
        If Not CurrentUser.IsAdmin() Then
            MessageBox.Show("Access Denied! Only administrators can manage users.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Me.Close()
            Return
        End If

        con = DatabaseConfig.GetConnection()
        LoadRoles()
        LoadData()
        ClearFields()
        Me.Text = "User Management - Admin Only"
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub LoadRoles()
        cmbUserRole.Items.Clear()
        cmbUserRole.Items.Add("Admin")
        cmbUserRole.Items.Add("Manager")
        cmbUserRole.Items.Add("User")
        cmbUserRole.SelectedIndex = 2
    End Sub

    Private Sub LoadData()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            da = New OleDbDataAdapter("SELECT UserID AS [ID], Username, FullName AS [Full Name], Email, UserRole AS [Role], IsActive AS [Active], Format(DateCreated, 'yyyy-MM-dd') AS [Created] FROM tblUsers ORDER BY UserID DESC", con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            If DataGridView1.Columns.Count > 0 Then
                DataGridView1.Columns(0).Width = 50
                DataGridView1.Columns(1).Width = 120
                DataGridView1.Columns(2).Width = 180
                DataGridView1.Columns(3).Width = 200
                DataGridView1.Columns(4).Width = 80
                DataGridView1.Columns(5).Width = 70
                DataGridView1.Columns(6).Width = 100
                DataGridView1.Columns(5).ReadOnly = False
            End If

            lblTotalUsers.Text = "Total Users: " & dt.Rows.Count.ToString()
            Dim admins = dt.AsEnumerable().Count(Function(r) r.Field(Of String)("Role") = "Admin")
            Dim managers = dt.AsEnumerable().Count(Function(r) r.Field(Of String)("Role") = "Manager")
            Dim users = dt.AsEnumerable().Count(Function(r) r.Field(Of String)("Role") = "User")
            lblUserStats.Text = $"Admins: {admins} | Managers: {managers} | Users: {users}"
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub ClearFields()
        txtUsername.Clear()
        txtFullName.Clear()
        txtEmail.Clear()
        txtPassword.Clear()
        txtConfirmPassword.Clear()
        If cmbUserRole.Items.Count > 0 Then cmbUserRole.SelectedIndex = 2
        chkIsActive.Checked = True
        selectedUserID = 0
        btnAdd.Enabled = True
        btnUpdate.Enabled = False
        btnDelete.Enabled = False
        btnResetPassword.Enabled = False
        txtUsername.Enabled = True
        txtPassword.Enabled = True
        txtConfirmPassword.Enabled = True
        lblPasswordNote.Text = "Password must be at least 6 characters"
        lblPasswordNote.ForeColor = Color.Gray
    End Sub

    ' Log activity inside frmUserManagement
    Private Sub LogActivity(activityType As String, description As String)
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            cmd = New OleDbCommand("INSERT INTO tblActivityLog (UserID, ActivityType, Description, ActivityDate) VALUES (?,?,?,?)", con)
            cmd.Parameters.AddWithValue("@1", CurrentUser.UserID)
            cmd.Parameters.AddWithValue("@2", activityType)
            cmd.Parameters.AddWithValue("@3", description)
            cmd.Parameters.AddWithValue("@4", Date.Now)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            ' Optional: handle logging errors
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    ' Reset password button
    Private Sub btnResetPassword_Click(sender As Object, e As EventArgs) Handles btnResetPassword.Click
        If selectedUserID > 0 Then
            Dim resetForm As New frmResetPassword(selectedUserID, txtUsername.Text)
            If resetForm.ShowDialog() = DialogResult.OK Then
                ' Log activity here, inside frmUserManagement
                LogActivity("Reset Password", "Reset password for user: " & txtUsername.Text)
                MessageBox.Show("Password reset successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    '... Keep all other methods (btnAdd, btnUpdate, btnDelete, DataGridView1_CellClick, btnSearch, chkShowPassword, etc.) unchanged ...
End Class
