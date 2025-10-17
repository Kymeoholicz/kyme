Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Public Class frmLogin
    Dim con As OleDbConnection
    Dim cmd As OleDbCommand
    Dim dr As OleDbDataReader
    Private loginAttempts As Integer = 0
    Private Const MaxLoginAttempts As Integer = 3

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Me.Text = "Login - Inventory Management System"
        txtPassword.PasswordChar = "●"
        txtUsername.Focus()

        Me.AcceptButton = btnLogin

        ' Initialize database
        InitializeDatabase()
    End Sub

    Private Sub InitializeDatabase()
        Try
            ' Initialize connection string
            DatabaseConfig.InitializeConnectionString()
            con = DatabaseConfig.GetConnection()

            Dim dbPath As String = DatabaseConfig.GetDatabasePath()

            ' Check if database file exists
            If Not File.Exists(dbPath) Then
                Dim result = MessageBox.Show("Database not found!" & vbCrLf & vbCrLf &
                    "Expected location: " & dbPath & vbCrLf & vbCrLf &
                    "Would you like to create a new database with default tables and admin account?",
                    "Database Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    ' Create empty database file using ADOX
                    Try
                        Dim cat As Object = CreateObject("ADOX.Catalog")
                        cat.Create(DatabaseConfig.ConnectionString)
                        cat = Nothing

                        ' Create tables
                        DatabaseConfig.CreateTables()
                        DatabaseConfig.CreateDefaultAdmin()
                    Catch ex As Exception
                        MessageBox.Show("Error creating database: " & ex.Message & vbCrLf & vbCrLf &
                            "Please create 'inventory.accdb' manually using Microsoft Access and run the application again.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Application.Exit()
                    End Try
                Else
                    Application.Exit()
                End If
            Else
                ' Verify database structure
                DatabaseConfig.VerifyDatabaseStructure()
            End If

            ' Test connection
            If Not DatabaseConfig.TestConnection() Then
                MessageBox.Show("Cannot connect to database!" & vbCrLf &
                    "Path: " & dbPath & vbCrLf & vbCrLf &
                    "Please ensure Microsoft Access Database Engine is installed.",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Database initialization error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrWhiteSpace(txtUsername.Text) Then
            MessageBox.Show("Please enter username.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUsername.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("Please enter password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPassword.Focus()
            Return
        End If

        If AuthenticateUser(txtUsername.Text.Trim(), txtPassword.Text) Then
            MessageBox.Show("Login successful! Welcome " & CurrentUser.FullName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LogActivity("Login", "User logged in successfully")

            Dim mainMenu As New frmMainMenu()
            Me.Hide()
            mainMenu.ShowDialog()
            Me.Close()
        Else
            loginAttempts += 1
            Dim remainingAttempts As Integer = MaxLoginAttempts - loginAttempts

            If remainingAttempts > 0 Then
                MessageBox.Show("Invalid username or password!" & vbCrLf & "Attempts remaining: " & remainingAttempts, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Clear()
                txtPassword.Focus()
            Else
                MessageBox.Show("Maximum login attempts exceeded. Application will close.", "Security", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Application.Exit()
            End If
        End If
    End Sub

    Private Function AuthenticateUser(username As String, password As String) As Boolean
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            ' Hash the input password before comparing
            Dim hashedPassword As String = HashPassword(password)

            ' ✅ Corrected SQL to match actual Access field names
            cmd = New OleDbCommand("
            SELECT UserID, Username, FullName, UserRole, IsActive 
            FROM tblUsers 
            WHERE Username = ? AND UserPassword = ?", con)

            cmd.Parameters.AddWithValue("@1", username)
            cmd.Parameters.AddWithValue("@2", hashedPassword)

            dr = cmd.ExecuteReader()

            If dr.Read() Then
                If Not CBool(dr("IsActive")) Then
                    MessageBox.Show("Your account has been deactivated.", "Account Inactive", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If

                ' ✅ Assign user details to CurrentUser object
                CurrentUser.UserID = CInt(dr("UserID"))
                CurrentUser.Username = dr("Username").ToString()
                CurrentUser.FullName = dr("FullName").ToString()
                CurrentUser.UserRole = dr("UserRole").ToString()
                CurrentUser.IsLoggedIn = True

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show("Login error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If dr IsNot Nothing Then dr.Close()
            If con.State = ConnectionState.Open Then con.Close()
        End Try
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
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub chkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        If chkShowPassword.Checked Then
            txtPassword.PasswordChar = ""
        Else
            txtPassword.PasswordChar = "●"
        End If
    End Sub

    Private Sub txtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress
        If e.KeyChar = " " Then
            e.Handled = True
        End If
    End Sub

    Private Sub lblForgotPassword_Click(sender As Object, e As EventArgs) Handles lblForgotPassword.Click
        MessageBox.Show("Please contact your system administrator to reset your password.", "Password Recovery", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnDatabaseInfo_Click(sender As Object, e As EventArgs) Handles btnDatabaseInfo.Click
        DatabaseConfig.ShowDatabaseInfo()
    End Sub
End Class