Public Class CurrentUser
    '==========================
    ' USER PROPERTIES
    '==========================
    Public Shared Property UserID As Integer = 0
    Public Shared Property Username As String = ""
    Public Shared Property FullName As String = ""
    Public Shared Property UserRole As String = ""
    Public Shared Property IsLoggedIn As Boolean = False
    Public Shared Property LoginTime As DateTime = Nothing
    Public Shared Property LastActivity As DateTime = Nothing

    '==========================
    ' ROLE CHECKING
    '==========================
    Public Shared Function IsAdmin() As Boolean
        Return UserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase)
    End Function

    Public Shared Function IsManager() As Boolean
        Return UserRole.Equals("Manager", StringComparison.OrdinalIgnoreCase) OrElse IsAdmin()
    End Function

    Public Shared Function IsUser() As Boolean
        Return UserRole.Equals("User", StringComparison.OrdinalIgnoreCase)
    End Function

    '==========================
    ' PERMISSION SYSTEM
    '==========================
    Public Shared Function HasPermission(permission As String) As Boolean
        If Not IsLoggedIn Then Return False

        Select Case permission.ToLower()
            ' Inventory Permissions
            Case "view_inventory"
                Return IsLoggedIn ' All logged-in users can view
            Case "add_inventory"
                Return IsLoggedIn ' All logged-in users can add
            Case "edit_inventory"
                Return IsLoggedIn ' All logged-in users can edit
            Case "delete_inventory"
                Return IsManager() ' Only managers and admins can delete

            ' User Management Permissions
            Case "manage_users"
                Return IsAdmin() ' Only admins
            Case "view_users"
                Return IsAdmin() ' Only admins

            ' Equipment Permissions
            Case "issue_equipment"
                Return IsLoggedIn ' All logged-in users
            Case "return_equipment"
                Return IsLoggedIn ' All logged-in users

            ' Report Permissions
            Case "view_reports"
                Return IsLoggedIn ' All logged-in users
            Case "export_reports"
                Return IsLoggedIn ' All logged-in users

            ' Activity Log Permissions
            Case "view_activity_log"
                Return IsManager() ' Managers and admins
            Case "clear_activity_log"
                Return IsAdmin() ' Only admins

            ' System Permissions
            Case "backup_database"
                Return IsAdmin() ' Only admins
            Case "restore_database"
                Return IsAdmin() ' Only admins
            Case "system_settings"
                Return IsAdmin() ' Only admins

            Case Else
                Return False
        End Select
    End Function

    '==========================
    ' SESSION MANAGEMENT
    '==========================
    Public Shared Sub Login(userID As Integer, username As String, fullName As String, userRole As String)
        userID = userID
        username = username
        fullName = fullName
        userRole = userRole
        IsLoggedIn = True
        LoginTime = DateTime.Now
        LastActivity = DateTime.Now
    End Sub

    Public Shared Sub Logout()
        UserID = 0
        Username = ""
        FullName = ""
        UserRole = ""
        IsLoggedIn = False
        LoginTime = Nothing
        LastActivity = Nothing
    End Sub

    Public Shared Sub UpdateActivity()
        LastActivity = DateTime.Now
    End Sub

    '==========================
    ' SESSION INFO
    '==========================
    Public Shared Function GetSessionDuration() As TimeSpan
        If LoginTime = Nothing Then Return TimeSpan.Zero
        Return DateTime.Now - LoginTime
    End Function

    Public Shared Function GetIdleTime() As TimeSpan
        If LastActivity = Nothing Then Return TimeSpan.Zero
        Return DateTime.Now - LastActivity
    End Function

    Public Shared Function IsSessionExpired(timeoutMinutes As Integer) As Boolean
        If Not IsLoggedIn Then Return True
        Return GetIdleTime().TotalMinutes > timeoutMinutes
    End Function

    '==========================
    ' USER INFO
    '==========================
    Public Shared Function GetDisplayName() As String
        If String.IsNullOrEmpty(FullName) Then
            Return Username
        Else
            Return FullName
        End If
    End Function

    Public Shared Function GetRoleDisplayName() As String
        Select Case UserRole.ToLower()
            Case "admin"
                Return "Administrator"
            Case "manager"
                Return "Manager"
            Case "user"
                Return "User"
            Case Else
                Return UserRole
        End Select
    End Function

    Public Shared Function GetUserInfo() As String
        If Not IsLoggedIn Then
            Return "Not logged in"
        End If

        Return $"{GetDisplayName()} ({GetRoleDisplayName()})" & vbCrLf &
               $"Logged in: {LoginTime:yyyy-MM-dd HH:mm:ss}" & vbCrLf &
               $"Session duration: {GetSessionDuration().ToString("hh\:mm\:ss")}"
    End Function

    '==========================
    ' VALIDATION
    '==========================
    Public Shared Function ValidateSession() As Boolean
        If Not IsLoggedIn Then
            Return False
        End If

        If UserID <= 0 OrElse String.IsNullOrEmpty(Username) Then
            Logout()
            Return False
        End If

        Return True
    End Function

    '==========================
    ' PERMISSION MESSAGES
    '==========================
    Public Shared Sub ShowAccessDeniedMessage(Optional feature As String = "this feature")
        MessageBox.Show($"Access Denied!{vbCrLf}{vbCrLf}" &
                       $"You don't have permission to access {feature}.{vbCrLf}{vbCrLf}" &
                       $"Current role: {GetRoleDisplayName()}{vbCrLf}" &
                       $"Required role: Administrator or Manager",
                       "Access Denied",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Stop)
    End Sub

    Public Shared Sub ShowSessionExpiredMessage()
        MessageBox.Show("Your session has expired due to inactivity." & vbCrLf & vbCrLf &
                       "Please login again.",
                       "Session Expired",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning)
    End Sub

    '==========================
    ' SECURITY CHECKS
    '==========================
    Public Shared Function CheckPermissionWithMessage(permission As String, Optional feature As String = "") As Boolean
        If Not ValidateSession() Then
            ShowSessionExpiredMessage()
            Return False
        End If

        If Not HasPermission(permission) Then
            ShowAccessDeniedMessage(If(String.IsNullOrEmpty(feature), permission.Replace("_", " "), feature))
            Return False
        End If

        UpdateActivity()
        Return True
    End Function

    '==========================
    ' DEBUG INFO
    '==========================
    Public Shared Function GetDebugInfo() As String
        Return $"=== CURRENT USER DEBUG INFO ===" & vbCrLf &
               $"UserID: {UserID}" & vbCrLf &
               $"Username: {Username}" & vbCrLf &
               $"FullName: {FullName}" & vbCrLf &
               $"UserRole: {UserRole}" & vbCrLf &
               $"IsLoggedIn: {IsLoggedIn}" & vbCrLf &
               $"LoginTime: {LoginTime}" & vbCrLf &
               $"LastActivity: {LastActivity}" & vbCrLf &
               $"SessionDuration: {GetSessionDuration()}" & vbCrLf &
               $"IdleTime: {GetIdleTime()}" & vbCrLf &
               $"IsAdmin: {IsAdmin()}" & vbCrLf &
               $"IsManager: {IsManager()}" & vbCrLf &
               $"IsUser: {IsUser()}"
    End Function

    '==========================
    ' COPY USER INFO TO CLIPBOARD
    '==========================
    Public Shared Sub CopyUserInfoToClipboard()
        Try
            Clipboard.SetText(GetUserInfo())
            MessageBox.Show("User information copied to clipboard!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error copying to clipboard: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class