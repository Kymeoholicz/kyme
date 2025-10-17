Imports System.Data.OleDb
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Threading
Imports ADOX  ' ✅ COM Reference: Microsoft ADO Ext. 6.0 for DDL and Security

Public Module DatabaseConfig

    ' === Centralized Connection String ===
    Public ConnectionString As String = ""
    Private isShuttingDown As Boolean = False
    Private ReadOnly connectionLock As New Object()
    Private activeConnections As New List(Of WeakReference)

    ' === Get Full Path of Database ===
    Public Function GetDatabasePath() As String
        Return Path.Combine(Application.StartupPath, "inventory.accdb")
    End Function

    ' === Initialize Connection String ===
    Public Sub InitializeConnectionString()
        Dim dbPath As String = GetDatabasePath()
        ' CRITICAL: Add OLE DB Services=-4 to disable connection pooling
        ' This prevents COM objects from being reused incorrectly
        ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Persist Security Info=False;OLE DB Services=-4;"
    End Sub

    ' === Automatically Create Database File If Missing ===
    Public Sub CreateDatabaseIfMissing()
        Dim dbPath As String = GetDatabasePath()
        If Not File.Exists(dbPath) Then
            Dim cat As Catalog = Nothing
            Try
                cat = New Catalog()
                cat.Create($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};")

                ' CRITICAL: Explicitly release COM object
                If cat IsNot Nothing Then
                    Marshal.ReleaseComObject(cat)
                    cat = Nothing
                End If

                ' Force immediate cleanup
                GC.Collect()
                GC.WaitForPendingFinalizers()
                GC.Collect()

                MessageBox.Show($"Database file created successfully: {dbPath}",
                                "Database Created", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"Error creating database: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ' Ensure COM object is released
                If cat IsNot Nothing Then
                    Try
                        Marshal.ReleaseComObject(cat)
                    Catch
                    End Try
                    cat = Nothing
                End If

                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End If
    End Sub

    ' === Test Connection ===
    Public Function TestConnection() As Boolean
        If isShuttingDown Then Return False

        Try
            Using con As New OleDbConnection(ConnectionString)
                con.Open()
                Return True
            End Using
        Catch
            Return False
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Function

    ' === Create All Tables (Safe and Consistent) ===
    Public Sub CreateTables()
        If isShuttingDown Then Return

        Try
            Using con As New OleDbConnection(ConnectionString)
                con.Open()
                Using cmd As New OleDbCommand()
                    cmd.Connection = con

                    ' tblInventory
                    If Not TableExists(con, "tblInventory") Then
                        cmd.CommandText =
"CREATE TABLE tblInventory (
    ItemID AUTOINCREMENT PRIMARY KEY,
    ItemName TEXT(100),
    Category TEXT(50),
    Quantity LONG,
    [Condition] TEXT(50),
    Location TEXT(100),
    DateAdded DATETIME
)"
                        cmd.ExecuteNonQuery()
                    End If

                    ' tblIssuedEquipment
                    If Not TableExists(con, "tblIssuedEquipment") Then
                        cmd.CommandText =
"CREATE TABLE tblIssuedEquipment (
    IssueID AUTOINCREMENT PRIMARY KEY,
    ItemID LONG,
    IssuedTo TEXT(100),
    DateIssued DATETIME,
    ReturnDate DATETIME,
    Remarks TEXT(255)
)"
                        cmd.ExecuteNonQuery()
                    End If

                    ' tblUsers
                    If Not TableExists(con, "tblUsers") Then
                        cmd.CommandText =
"CREATE TABLE tblUsers (
    UserID AUTOINCREMENT PRIMARY KEY,
    Username TEXT(50),
    [UserPassword] TEXT(255),
    FullName TEXT(100),
    Email TEXT(100),
    UserRole TEXT(20),
    IsActive YESNO,
    DateCreated DATETIME,
    CreatedBy LONG
)"
                        cmd.ExecuteNonQuery()
                    End If

                    ' tblActivityLog
                    If Not TableExists(con, "tblActivityLog") Then
                        cmd.CommandText =
"CREATE TABLE tblActivityLog (
    LogID AUTOINCREMENT PRIMARY KEY,
    UserID LONG,
    ActivityType TEXT(50),
    Description MEMO,
    ActivityDate DATETIME
)"
                        cmd.ExecuteNonQuery()
                    End If
                End Using
            End Using

            MessageBox.Show("✅ All tables created successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Error creating tables: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    ' === Check if a Table Exists ===
    Private Function TableExists(con As OleDbConnection, tableName As String) As Boolean
        Dim schema As DataTable = Nothing
        Try
            schema = con.GetSchema("Tables")
            For Each row As DataRow In schema.Rows
                If row("TABLE_NAME").ToString().Equals(tableName, StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
            Next
        Catch
        Finally
            ' Dispose schema DataTable
            If schema IsNot Nothing Then
                Try
                    schema.Dispose()
                Catch
                End Try
            End If
        End Try
        Return False
    End Function

    ' === Create Default Admin (Only Once) ===
    Public Sub CreateDefaultAdmin()
        If isShuttingDown Then Return

        Try
            Using con As New OleDbConnection(ConnectionString)
                con.Open()

                Using checkCmd As New OleDbCommand("SELECT COUNT(*) FROM tblUsers WHERE Username='admin'", con)
                    Dim count As Integer = CInt(checkCmd.ExecuteScalar())

                    If count = 0 Then
                        ' SHA256("admin123")
                        Dim adminHash As String = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9"

                        Using insertCmd As New OleDbCommand("
INSERT INTO tblUsers (Username, [UserPassword], FullName, Email, UserRole, IsActive, DateCreated, CreatedBy)
VALUES (?,?,?,?,?,?,?,?)", con)

                            insertCmd.Parameters.Add("?", OleDbType.VarWChar, 50).Value = "admin"
                            insertCmd.Parameters.Add("?", OleDbType.VarWChar, 255).Value = adminHash
                            insertCmd.Parameters.Add("?", OleDbType.VarWChar, 100).Value = "System Administrator"
                            insertCmd.Parameters.Add("?", OleDbType.VarWChar, 100).Value = "admin@system.com"
                            insertCmd.Parameters.Add("?", OleDbType.VarWChar, 20).Value = "Admin"
                            insertCmd.Parameters.Add("?", OleDbType.Boolean).Value = True
                            insertCmd.Parameters.Add("?", OleDbType.Date).Value = Date.Now
                            insertCmd.Parameters.Add("?", OleDbType.Integer).Value = 1
                            insertCmd.ExecuteNonQuery()

                            MessageBox.Show("✅ Default admin account created!" & vbCrLf &
                                            "Username: admin" & vbCrLf &
                                            "Password: admin123",
                                            "Admin Created", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Using
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error creating admin: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    ' === Verify Database Tables ===
    Public Function VerifyDatabaseStructure() As Boolean
        If isShuttingDown Then Return False

        Try
            Using con As New OleDbConnection(ConnectionString)
                con.Open()
                Dim requiredTables() As String = {"tblInventory", "tblIssuedEquipment", "tblUsers", "tblActivityLog"}
                Dim missing As New List(Of String)

                For Each name In requiredTables
                    If Not TableExists(con, name) Then missing.Add(name)
                Next

                If missing.Count > 0 Then
                    Dim msg As String = "Missing tables detected:" & vbCrLf &
                        String.Join(", ", missing) & vbCrLf & vbCrLf &
                        "Would you like to create them now?"

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
            MessageBox.Show($"Error verifying database: {ex.Message}" & vbCrLf &
                            $"Database Path: {GetDatabasePath()}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Function

    ' === Display Database Info ===
    Public Sub ShowDatabaseInfo()
        Dim info As String = $"Database Information:{vbCrLf}{vbCrLf}" &
            $"Location: {GetDatabasePath()}{vbCrLf}" &
            $"Exists: {File.Exists(GetDatabasePath())}{vbCrLf}" &
            $"Connection String:{vbCrLf}{ConnectionString}{vbCrLf}{vbCrLf}"

        If File.Exists(GetDatabasePath()) Then
            info &= $"File Size: {(New FileInfo(GetDatabasePath()).Length / 1024):N2} KB"
        End If

        MessageBox.Show(info, "Database Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' === Shared Connection Helper ===
    Public Function GetConnection() As OleDbConnection
        If isShuttingDown Then
            Throw New InvalidOperationException("Application is shutting down")
        End If
        Return New OleDbConnection(ConnectionString)
    End Function

    ' === Shutdown Handler - Call this when app closes ===
    Public Sub PrepareForShutdown()
        isShuttingDown = True

        ' Force aggressive cleanup
        For i As Integer = 0 To 5
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, True, True)
            GC.WaitForPendingFinalizers()
            System.Threading.Thread.Sleep(20)
        Next
    End Sub

End Module