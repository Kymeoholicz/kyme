Public Class CurrentUser
    Public Shared Property UserID As Integer = 0
    Public Shared Property Username As String = ""
    Public Shared Property FullName As String = ""
    Public Shared Property UserRole As String = ""
    Public Shared Property IsLoggedIn As Boolean = False

    Public Shared Function IsAdmin() As Boolean
        Return UserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase)
    End Function

    Public Shared Function IsManager() As Boolean
        Return UserRole.Equals("Manager", StringComparison.OrdinalIgnoreCase) OrElse IsAdmin()
    End Function

    Public Shared Sub Logout()
        UserID = 0
        Username = ""
        FullName = ""
        UserRole = ""
        IsLoggedIn = False
    End Sub

    Public Shared Function HasPermission(permission As String) As Boolean
        Select Case permission.ToLower()
            Case "add_inventory", "edit_inventory", "view_inventory"
                Return IsLoggedIn
            Case "delete_inventory"
                Return IsManager()
            Case "manage_users"
                Return IsAdmin()
            Case "view_reports"
                Return IsLoggedIn
            Case "issue_equipment"
                Return IsLoggedIn
            Case Else
                Return False
        End Select
    End Function
End Class