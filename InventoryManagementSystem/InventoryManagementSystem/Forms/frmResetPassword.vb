Imports System.Security.Cryptography
Imports System.Text
Imports System.Data.OleDb

Public Class frmResetPassword
    Private _userID As Integer
    Private _username As String
    Dim con As OleDbConnection
    Dim cmd As OleDbCommand

    Public Sub New(userID As Integer, username As String)
        InitializeComponent()
        _userID = userID
        _username = username
        lblUsername.Text = "Resetting password for: " & _username
        con = DatabaseConfig.GetConnection()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If ValidateInput() Then
            Try
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()
                Dim hashedPassword As String = HashPassword(txtPassword.Text)
                cmd = New OleDbCommand("UPDATE tblUsers SET Password=? WHERE UserID=?", con)
                cmd.Parameters.AddWithValue("@1", hashedPassword)
                cmd.Parameters.AddWithValue("@2", _userID)
                cmd.ExecuteNonQuery()

                Me.DialogResult = DialogResult.OK
                Me.Close()
            Catch ex As Exception
                MessageBox.Show("Error resetting password: " & ex.Message)
            Finally
                If con.State = ConnectionState.Open Then con.Close()
            End Try
        End If
    End Sub

    Private Sub chkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        If chkShowPassword.Checked Then
            txtPassword.PasswordChar = ControlChars.NullChar
            txtConfirmPassword.PasswordChar = ControlChars.NullChar
        Else
            txtPassword.PasswordChar = "●"c
            txtConfirmPassword.PasswordChar = "●"c
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnGeneratePassword_Click(sender As Object, e As EventArgs) Handles btnGeneratePassword.Click
        Dim randomPassword As String = GenerateRandomPassword(10)
        txtPassword.Text = randomPassword
        txtConfirmPassword.Text = randomPassword
    End Sub

    Private Function ValidateInput() As Boolean
        If String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("Please enter a new password.")
            txtPassword.Focus()
            Return False
        End If
        If txtPassword.Text.Length < 6 Then
            MessageBox.Show("Password must be at least 6 characters long.")
            txtPassword.Focus()
            Return False
        End If
        If txtPassword.Text <> txtConfirmPassword.Text Then
            MessageBox.Show("Passwords do not match!")
            txtConfirmPassword.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim builder As New StringBuilder()
            For Each b As Byte In bytes
                builder.Append(b.ToString("x2"))
            Next
            Return builder.ToString()
        End Using
    End Function

    ' Modern random password generator
    Private Function GenerateRandomPassword(length As Integer) As String
        Const chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()"
        Dim result As New StringBuilder(length)
        Dim buffer As Byte() = New Byte(0) {}

        For i As Integer = 1 To length
            buffer = New Byte(0) {}
            RandomNumberGenerator.Fill(buffer)
            Dim idx As Integer = buffer(0) Mod chars.Length
            result.Append(chars(idx))
        Next

        Return result.ToString()
    End Function
End Class
