Imports System.Data.OleDb
Imports System.IO
Imports ADOX  ' ✅ Add reference: Project > Add Reference > COM > "Microsoft ADO Ext. 6.0 for DDL and Security"

Public Module DatabaseConfig
    ' Central connection string
    Public ConnectionString As String = ""

    ' Get the database path
    Public Function GetDatabasePath() As String
        ' Database will be in application's startup path
        Return Path.Combine(Application.StartupPath, "inventory.accdb")
    End Function

    ' Initialize connection string
    Public Sub InitializeConnectionString()
        Dim dbPath As String = GetDatabasePath()
        ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbPath & ";Persist Security Info=False;"
    End Sub

    ' ✅ Automatically create database if missing
    Public Sub CreateDatabaseIfMissing()
        Dim dbPath As String = GetDatabasePath()
        If Not File.Exists(dbPath) Then
            Try
                Dim cat As New Catalog()
                cat.Create("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbPath & ";")
                cat = Nothing
                MessageBox.Show("Database file created successfully: " & dbPath, "Database Created", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error creating database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' Test database connection
    Public Function TestConnection() As Boolean
        Try
            Using con As New OleDbConnection(ConnectionString)
                con.Open()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' ✅ Create all tables
    Public Sub CreateTables()
        Try
            Using con As New OleDbConnection(ConnectionString)
                con.Open()
                Dim cmd As New OleDbCommand()
                cmd.Connection = con

                ' tblInventory
                If Not TableExists(con, "tblInventory") Then
                    cmd.CommandText = "
                        CREATE TABLE tblInventory (
                            ItemID AUTOINCREMENT PRIMARY KEY,
                            ItemName TEXT(100),
                            Category TEXT(50),
                            Quantity INTEGER,
                            Condition TEXT(50),
                            Location TEXT(100),
                            DateAdded DATETIME
                        )"
                    cmd.ExecuteNonQuery()
                End If

                ' tblIssuedEquipment
                If Not TableExists(con, "tblIssuedEquipment") Then
                    cmd.CommandText = "
                        CREATE TABLE tblIssuedEquipment (
                            IssueID AUTOINCREMENT PRIMARY KEY,
                            ItemID INTEGER,
                            IssuedTo TEXT(100),
                            DateIssued DATETIME,
                            ReturnDate DATETIME,
                            Remarks TEXT(255)
                        )"
                    cmd.ExecuteNonQuery()
                End If

                ' ✅ tblUsers (fixed reserved word + clean syntax)
                If Not TableExists(con, "tblUsers") Then
                    cmd.CommandText = "
                        CREATE TABLE tblUsers (
                            UserID AUTOINCREMENT PRIMARY KEY,
                            Username TEXT(50),
                            [UserPassword] TEXT(255),
                            FullName TEXT(100),
                            Email TEXT(100),
                            UserRole TEXT(20),
                            IsActive YESNO,
                            DateCreated DATETIME,
                            CreatedBy INTEGER
                        )"
                    cmd.ExecuteNonQuery()
                End If

                ' ✅ tblActivityLog (use LONGTEXT instead of MEMO)
                If Not TableExists(con, "tblActivityLog") Then
                    cmd.CommandText = "
                        CREATE TABLE tblActivityLog (
                            LogID AUTOINCREMENT PRIMARY KEY,
                            UserID INTEGER,
                            ActivityType TEXT(50),
                            Description LONGTEXT,
                            ActivityDate DATETIME
                        )"
                    cmd.ExecuteNonQuery()
                End If

                MessageBox.Show("All tables created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error creating tables: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Check if table exists
    Private Function TableExists(con As OleDbConnection, tableName As String) As Boolean
        Try
            Dim dt As DataTable = con.GetSchema("Tables")
            For Each row As DataRow In dt.Rows
                If row("TABLE_NAME").ToString().Equals(tableName, StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
            Next
            Return False
        Catch
            Return False
        End Try
    End Function

    ' ✅ Create default admin (fixed type mismatch + runs once)
    Public Sub CreateDefaultAdmin()
        Try
            Using con As New OleDbConnection(ConnectionString)
                con.Open()

                ' Check if admin already exists
                Dim checkCmd As New OleDbCommand("SELECT COUNT(*) FROM tblUsers WHERE Username = 'admin'", con)
                Dim count As Integer = CInt(checkCmd.ExecuteScalar())

                If count = 0 Then
                    ' Hash for "admin123"
                    Dim adminHash As String = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9"

                    Dim cmd As New OleDbCommand("
                        INSERT INTO tblUsers 
                        (Username, UserPassword, FullName, Email, UserRole, IsActive, DateCreated, CreatedBy)
                        VALUES (?,?,?,?,?,?,?,?)", con)

                    ' ✅ Define parameter types explicitly
                    cmd.Parameters.Add("?", OleDbType.VarWChar, 50).Value = "admin"
                    cmd.Parameters.Add("?", OleDbType.VarWChar, 255).Value = adminHash
                    cmd.Parameters.Add("?", OleDbType.VarWChar, 100).Value = "System Administrator"
                    cmd.Parameters.Add("?", OleDbType.VarWChar, 100).Value = "admin@system.com"
                    cmd.Parameters.Add("?", OleDbType.VarWChar, 20).Value = "Admin"
                    cmd.Parameters.Add("?", OleDbType.Boolean).Value = True
                    cmd.Parameters.Add("?", OleDbType.Date).Value = Date.Now
                    cmd.Parameters.Add("?", OleDbType.Integer).Value = 1

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Default admin account created!" & vbCrLf &
                                    vbCrLf & "Username: admin" &
                                    vbCrLf & "Password: admin123",
                                    "Admin Created", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error creating admin: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Get connection
    Public Function GetConnection() As OleDbConnection
        Return New OleDbConnection(ConnectionString)
    End Function

    ' Verify database structure
    Public Function VerifyDatabaseStructure() As Boolean
        Try
            Using con As New OleDbConnection(ConnectionString)
                con.Open()

                Dim requiredTables() As String = {"tblInventory", "tblIssuedEquipment", "tblUsers", "tblActivityLog"}
                Dim missingTables As New List(Of String)

                For Each tableName In requiredTables
                    If Not TableExists(con, tableName) Then
                        missingTables.Add(tableName)
                    End If
                Next

                If missingTables.Count > 0 Then
                    Dim msg As String = "Missing tables detected:" & vbCrLf &
                                        String.Join(", ", missingTables) &
                                        vbCrLf & vbCrLf & "Would you like to create them now?"
                    If MessageBox.Show(msg, "Database Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        CreateTables()
                        CreateDefaultAdmin()
                        Return True
                    End If
                    Return False
                End If

                Return True
            End Using
        Catch ex As Exception
            MessageBox.Show("Error verifying database: " & ex.Message & vbCrLf &
                            vbCrLf & "Database Path: " & GetDatabasePath(),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ' Show database info
    Public Sub ShowDatabaseInfo()
        Dim info As String = "Database Information:" & vbCrLf & vbCrLf
        info &= "Location: " & GetDatabasePath() & vbCrLf
        info &= "Exists: " & File.Exists(GetDatabasePath()).ToString() & vbCrLf
        info &= "Connection String: " & vbCrLf & ConnectionString & vbCrLf & vbCrLf

        If File.Exists(GetDatabasePath()) Then
            info &= "File Size: " & (New FileInfo(GetDatabasePath()).Length / 1024).ToString("N2") & " KB"
        End If

        MessageBox.Show(info, "Database Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Module
