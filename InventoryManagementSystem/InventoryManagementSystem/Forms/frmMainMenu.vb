Public Class frmMainMenu
    Private Sub btnManageInventory_Click(sender As Object, e As EventArgs) Handles btnManageInventory.Click
        Dim inventoryForm As New frmInventory()
        inventoryForm.ShowDialog()
    End Sub

    Private Sub btnIssuedEquipment_Click(sender As Object, e As EventArgs) Handles btnIssuedEquipment.Click
        Dim issuedForm As New frmIssuedEquipment()
        issuedForm.ShowDialog()
    End Sub

    Private Sub btnReports_Click(sender As Object, e As EventArgs) Handles btnReports.Click
        Dim reportForm As New frmReports()
        reportForm.ShowDialog()
    End Sub

    Private Sub btnUserManagement_Click(sender As Object, e As EventArgs) Handles btnUserManagement.Click
        If CurrentUser.IsAdmin() Then
            Dim userForm As New frmUserManagement()
            userForm.ShowDialog()
        Else
            MessageBox.Show("Access Denied! Only administrators can manage users.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub btnActivityLog_Click(sender As Object, e As EventArgs) Handles btnActivityLog.Click
        If CurrentUser.IsAdmin() OrElse CurrentUser.IsManager() Then
            Dim activityForm As New frmActivityLog()
            activityForm.ShowDialog()
        Else
            MessageBox.Show("Access Denied! Only administrators and managers can view activity logs.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            CurrentUser.Logout()
            Dim loginForm As New frmLogin()
            Me.Close()
            loginForm.ShowDialog()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub frmMainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Me.Text = "Inventory Management System - Main Menu"

        ' Display logged-in user info
        lblWelcome.Text = "Welcome, " & CurrentUser.FullName & " (" & CurrentUser.UserRole & ")"

        ' Show/hide buttons based on role
        If CurrentUser.IsAdmin() Then
            btnUserManagement.Visible = True
            btnActivityLog.Visible = True
        ElseIf CurrentUser.IsManager() Then
            btnUserManagement.Visible = False
            btnActivityLog.Visible = True
        Else
            btnUserManagement.Visible = False
            btnActivityLog.Visible = False
        End If

        ' Set colors and styling
        Me.BackColor = Color.FromArgb(245, 245, 245)
        Panel1.BackColor = Color.FromArgb(0, 122, 204)
        lblWelcome.ForeColor = Color.White
        lblSystemTitle.ForeColor = Color.White
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Update date/time display
        lblDateTime.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy - hh:mm:ss tt")
    End Sub
End Class