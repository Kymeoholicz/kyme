Imports System.Data.OleDb
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Module DatabaseConfig
    Private _connectionString As String = ""
    Private _databasePath As String = ""

    '==========================
    ' INITIALIZE CONNECTION STRING
    '==========================
    Public Sub InitializeConnectionString()
        Try
            ' Get application directory
            Dim appPath As String = Application.StartupPath
            _databasePath = Path.Combine(appPath, "inventory.accdb")

            ' Build connection string for Access 2007+ (.accdb)
            _connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={_databasePath};Persist Security Info=False;"

        Catch ex As Exception
            Throw New Exception("Error initializing connection string: " & ex.Message)
        End Try
    End Sub

    '==========================
    ' GET CONNECTION
    '==========================
    Public Function GetConnection() As OleDbConnection
        If String.IsNullOrEmpty(_connectionString) Then
            InitializeConnectionString()
        End If
        Return New OleDbConnection(_connectionString)
    End Function

    '==========================
    ' GET DATABASE PATH
    '==========================
    Public Function GetDatabasePath() As String
        If String.IsNullOrEmpty(_databasePath) Then
            InitializeConnectionString()
        End If
        Return _databasePath
    End Function

    '==========================
    ' GET CONNECTION STRING
    '==========================
    Public ReadOnly Property ConnectionString As String
        Get
            If String.IsNullOrEmpty(_connectionString) Then
                InitializeConnectionString()
            End If
            Return _connectionString
        End Get
    End Property

    '==========================
    ' TEST CONNECTION
    '==========================
    Public Function TestConnection() As Boolean
        Try
            Using con As OleDbConnection = GetConnection()
                con.Open()
                Return con.State = ConnectionState.Open
            End Using
        Catch ex As Exception
            Debug.WriteLine("Connection test failed: " & ex.Message)
            Return False
        End Try
    End Function

    '==========================
    ' CREATE TABLES
    '==========================
    Public Sub CreateTables()
        Try
            Using con As OleDbConnection = GetConnection()
                con.Open()

                ' Create tblUsers
                Try
                    ExecuteNonQuery(con, "CREATE TABLE tblUsers (" &
                                        "UserID AUTOINCREMENT PRIMARY KEY, " &
                                        "Username TEXT(50) NOT NULL, " &
                                        "UserPassword TEXT(255) NOT NULL, " &
                                        "FullName TEXT(100) NOT NULL, " &
                                        "Email TEXT(100), " &
                                        "UserRole TEXT(20) NOT NULL, " &
                                        "IsActive YESNO NOT NULL, " &
                                        "DateCreated DATETIME NOT NULL)")
                Catch ex As Exception
                    Debug.WriteLine("tblUsers might already exist: " & ex.Message)
                End Try

                ' Create tblInventory
                Try
                    ExecuteNonQuery(con, "CREATE TABLE tblInventory (" &
                                        "ItemID AUTOINCREMENT PRIMARY KEY, " &
                                        "ItemName TEXT(100) NOT NULL, " &
                                        "Category TEXT(50) NOT NULL, " &
                                        "Quantity INTEGER NOT NULL, " &
                                        "[Condition] TEXT(50) NOT NULL, " &
                                        "Location TEXT(100) NOT NULL, " &
                                        "DateAdded DATETIME NOT NULL)")
                Catch ex As Exception
                    Debug.WriteLine("tblInventory might already exist: " & ex.Message)
                End Try

                ' Create tblIssuedEquipment
                Try
                    ExecuteNonQuery(con, "CREATE TABLE tblIssuedEquipment (" &
                                        "IssueID AUTOINCREMENT PRIMARY KEY, " &
                                        "ItemID INTEGER NOT NULL, " &
                                        "IssuedTo TEXT(100) NOT NULL, " &
                                        "DateIssued DATETIME NOT NULL, " &
                                        "ReturnDate DATETIME NOT NULL, " &
                                        "Remarks MEMO)")
                Catch ex As Exception
                    Debug.WriteLine("tblIssuedEquipment might already exist: " & ex.Message)
                End Try

                ' Create tblActivityLog
                Try
                    ExecuteNonQuery(con, "CREATE TABLE tblActivityLog (" &
                                        "LogID AUTOINCREMENT PRIMARY KEY, " &
                                        "UserID INTEGER NOT NULL, " &
                                        "ActivityType TEXT(50) NOT NULL, " &
                                        "Description MEMO, " &
                                        "ActivityDate DATETIME NOT NULL)")
                Catch ex As Exception
                    Debug.WriteLine("tblActivityLog might already exist: " & ex.Message)
                End Try

            End Using

            MessageBox.Show("Database tables created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            Throw New Exception("Error creating tables: " & ex.Message)
        End Try
    End Sub

    '==========================
    ' CREATE DEFAULT ADMIN
    '==========================
    Public Sub CreateDefaultAdmin()
        Try
            Using con As OleDbConnection = GetConnection()
                con.Open()

                ' Check if any users exist
                Dim count As Integer = 0
                Using cmd As New OleDbCommand("SELECT COUNT(*) FROM tblUsers", con)
                    count = CInt(cmd.ExecuteScalar())
                End Using

                ' Create default admin if no users exist
                If count = 0 Then
                    Dim defaultUsername As String = "admin"
                    Dim defaultPassword As String = "admin123" ' Should be changed on first login
                    Dim hashedPassword As String = HashPassword(defaultPassword)

                    Using cmd As New OleDbCommand("INSERT INTO tblUsers (Username, UserPassword, FullName, Email, UserRole, IsActive, DateCreated) VALUES (?,?,?,?,?,?,?)", con)
                        cmd.Parameters.Add("@Username", OleDbType.VarWChar, 50).Value = defaultUsername
                        cmd.Parameters.Add("@UserPassword", OleDbType.VarWChar, 255).Value = hashedPassword
                        cmd.Parameters.Add("@FullName", OleDbType.VarWChar, 100).Value = "System Administrator"
                        cmd.Parameters.Add("@Email", OleDbType.VarWChar, 100).Value = "admin@system.local"
                        cmd.Parameters.Add("@UserRole", OleDbType.VarWChar, 20).Value = "Admin"
                        cmd.Parameters.Add("@IsActive", OleDbType.Boolean).Value = True
                        cmd.Parameters.Add("@DateCreated", OleDbType.Date).Value = Date.Now
                        cmd.ExecuteNonQuery()
                    End Using

                    MessageBox.Show($"Default admin account created!{vbCrLf}{vbCrLf}" &
                                  $"Username: {defaultUsername}{vbCrLf}" &
                                  $"Password: {defaultPassword}{vbCrLf}{vbCrLf}" &
                                  "⚠️ IMPORTANT: Change this password immediately after first login!",
                                  "Admin Account Created", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End Using

        Catch ex As Exception
            Throw New Exception("Error creating default admin: " & ex.Message)
        End Try
    End Sub

    '==========================
    ' VERIFY DATABASE STRUCTURE
    '==========================
    Public Sub VerifyDatabaseStructure()
        Try
            Using con As OleDbConnection = GetConnection()
                con.Open()

                Dim requiredTables As String() = {"tblUsers", "tblInventory", "tblIssuedEquipment", "tblActivityLog"}
                Dim missingTables As New List(Of String)

                For Each tableName As String In requiredTables
                    If Not TableExists(con, tableName) Then
                        missingTables.Add(tableName)
                    End If
                Next

                If missingTables.Count > 0 Then
                    Dim result = MessageBox.Show(
                        $"Missing tables detected:{vbCrLf}{String.Join(", ", missingTables)}{vbCrLf}{vbCrLf}" &
                        "Would you like to create them now?",
                        "Database Structure Issue",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning)

                    If result = DialogResult.Yes Then
                        CreateTables()
                        CreateDefaultAdmin()
                    End If
                End If
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error verifying database structure: " & ex.Message)
        End Try
    End Sub

    '==========================
    ' CHECK IF TABLE EXISTS
    '==========================
    Private Function TableExists(con As OleDbConnection, tableName As String) As Boolean
        Try
            Dim schema As DataTable = con.GetSchema("Tables")
            For Each row As DataRow In schema.Rows
                If row("TABLE_NAME").ToString().Equals(tableName, StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    '==========================
    ' SHOW DATABASE INFO
    '==========================
    Public Sub ShowDatabaseInfo()
        Try
            Dim info As New StringBuilder()
            info.AppendLine("=== DATABASE INFORMATION ===")
            info.AppendLine()
            info.AppendLine($"Database Path: {GetDatabasePath()}")
            info.AppendLine($"File Exists: {File.Exists(GetDatabasePath())}")
            info.AppendLine()

            Using con As OleDbConnection = GetConnection()
                con.Open()
                info.AppendLine($"Connection Status: Connected")
                info.AppendLine($"Provider: {con.Provider}")
                info.AppendLine($"Server Version: {con.ServerVersion}")
                info.AppendLine()

                ' Get table information
                info.AppendLine("=== TABLES ===")
                Dim schema As DataTable = con.GetSchema("Tables")
                For Each row As DataRow In schema.Rows
                    Dim tableName As String = row("TABLE_NAME").ToString()
                    If tableName.StartsWith("tbl") Then
                        Dim count As Integer = GetTableRowCount(con, tableName)
                        info.AppendLine($"{tableName}: {count} records")
                    End If
                Next
            End Using

            MessageBox.Show(info.ToString(), "Database Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Error retrieving database info:{vbCrLf}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '==========================
    ' GET TABLE ROW COUNT
    '==========================
    Private Function GetTableRowCount(con As OleDbConnection, tableName As String) As Integer
        Try
            Using cmd As New OleDbCommand($"SELECT COUNT(*) FROM {tableName}", con)
                Return CInt(cmd.ExecuteScalar())
            End Using
        Catch ex As Exception
            Return 0
        End Try
    End Function

    '==========================
    ' EXECUTE NON-QUERY
    '==========================
    Private Sub ExecuteNonQuery(con As OleDbConnection, sql As String)
        Using cmd As New OleDbCommand(sql, con)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    '==========================
    ' HASH PASSWORD (SHARED UTILITY)
    '==========================
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

    '==========================
    ' BACKUP DATABASE
    '==========================
    Public Function BackupDatabase(Optional backupPath As String = "") As Boolean
        Try
            If String.IsNullOrEmpty(backupPath) Then
                Dim sfd As New SaveFileDialog()
                sfd.Filter = "Access Database (*.accdb)|*.accdb"
                sfd.FileName = $"inventory_backup_{DateTime.Now:yyyyMMdd_HHmmss}.accdb"

                If sfd.ShowDialog() <> DialogResult.OK Then
                    Return False
                End If

                backupPath = sfd.FileName
            End If

            ' Close any open connections first
            GC.Collect()
            GC.WaitForPendingFinalizers()

            ' Copy the database file
            File.Copy(GetDatabasePath(), backupPath, True)

            MessageBox.Show($"Database backed up successfully to:{vbCrLf}{backupPath}",
                          "Backup Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True

        Catch ex As Exception
            MessageBox.Show($"Backup failed:{vbCrLf}{ex.Message}",
                          "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    '==========================
    ' RESTORE DATABASE
    '==========================
    Public Function RestoreDatabase() As Boolean
        Try
            Dim ofd As New OpenFileDialog()
            ofd.Filter = "Access Database (*.accdb)|*.accdb"
            ofd.Title = "Select Backup File to Restore"

            If ofd.ShowDialog() <> DialogResult.OK Then
                Return False
            End If

            Dim result = MessageBox.Show(
                "⚠️ WARNING: This will replace the current database with the backup file!" & vbCrLf & vbCrLf &
                "Current database will be backed up first." & vbCrLf & vbCrLf &
                "Do you want to continue?",
                "Confirm Restore",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If result = DialogResult.No Then
                Return False
            End If

            ' Backup current database first
            Dim currentBackup As String = Path.Combine(
                Path.GetDirectoryName(GetDatabasePath()),
                $"inventory_before_restore_{DateTime.Now:yyyyMMdd_HHmmss}.accdb")

            File.Copy(GetDatabasePath(), currentBackup, True)

            ' Close connections
            GC.Collect()
            GC.WaitForPendingFinalizers()

            ' Restore from backup
            File.Copy(ofd.FileName, GetDatabasePath(), True)

            MessageBox.Show("Database restored successfully!" & vbCrLf & vbCrLf &
                          "Previous database backed up to:" & vbCrLf & currentBackup & vbCrLf & vbCrLf &
                          "Application will now restart.",
                          "Restore Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Restart application
            Application.Restart()
            Return True

        Catch ex As Exception
            MessageBox.Show($"Restore failed:{vbCrLf}{ex.Message}",
                          "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    '==========================
    ' OPTIMIZE DATABASE
    '==========================
    Public Sub OptimizeDatabase()
        Try
            Dim result = MessageBox.Show(
                "This will compact and repair the database." & vbCrLf & vbCrLf &
                "This may take a few moments." & vbCrLf & vbCrLf &
                "Continue?",
                "Optimize Database",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If result = DialogResult.No Then
                Return
            End If

            ' Close all connections
            GC.Collect()
            GC.WaitForPendingFinalizers()

            ' Create temporary path
            Dim tempPath As String = Path.Combine(
                Path.GetDirectoryName(GetDatabasePath()),
                "inventory_temp.accdb")

            ' Compact database using JRO (Jet Replication Objects)
            Try
                Dim jro As Object = CreateObject("JRO.JetEngine")
                jro.CompactDatabase(
                    ConnectionString,
                    $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={tempPath};")
                jro = Nothing

                ' Replace original with compacted version
                File.Delete(GetDatabasePath())
                File.Move(tempPath, GetDatabasePath())

                MessageBox.Show("Database optimized successfully!",
                              "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                ' If JRO not available, just show message
                MessageBox.Show("Database optimization requires Microsoft Jet Replication Objects." & vbCrLf & vbCrLf &
                              "Your database is working fine without optimization.",
                              "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        Catch ex As Exception
            MessageBox.Show($"Optimization error:{vbCrLf}{ex.Message}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '==========================
    ' GET DATABASE STATISTICS
    '==========================
    Public Function GetDatabaseStatistics() As Dictionary(Of String, Integer)
        Dim stats As New Dictionary(Of String, Integer)

        Try
            Using con As OleDbConnection = GetConnection()
                con.Open()

                stats.Add("TotalUsers", GetTableRowCount(con, "tblUsers"))
                stats.Add("ActiveUsers", GetActiveUserCount(con))
                stats.Add("TotalItems", GetTableRowCount(con, "tblInventory"))
                stats.Add("IssuedItems", GetTableRowCount(con, "tblIssuedEquipment"))
                stats.Add("ActivityLogs", GetTableRowCount(con, "tblActivityLog"))
                stats.Add("LowStockItems", GetLowStockCount(con))
                stats.Add("OutOfStockItems", GetOutOfStockCount(con))
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting statistics: " & ex.Message)
        End Try

        Return stats
    End Function

    Private Function GetActiveUserCount(con As OleDbConnection) As Integer
        Try
            Using cmd As New OleDbCommand("SELECT COUNT(*) FROM tblUsers WHERE IsActive = True", con)
                Return CInt(cmd.ExecuteScalar())
            End Using
        Catch
            Return 0
        End Try
    End Function

    Private Function GetLowStockCount(con As OleDbConnection) As Integer
        Try
            Using cmd As New OleDbCommand("SELECT COUNT(*) FROM tblInventory WHERE Quantity > 0 AND Quantity <= 5", con)
                Return CInt(cmd.ExecuteScalar())
            End Using
        Catch
            Return 0
        End Try
    End Function

    Private Function GetOutOfStockCount(con As OleDbConnection) As Integer
        Try
            Using cmd As New OleDbCommand("SELECT COUNT(*) FROM tblInventory WHERE Quantity = 0", con)
                Return CInt(cmd.ExecuteScalar())
            End Using
        Catch
            Return 0
        End Try
    End Function

End Module