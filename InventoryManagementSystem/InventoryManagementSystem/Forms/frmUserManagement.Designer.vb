<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmUserManagement
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblTitle = New Label()
        DataGridView1 = New DataGridView()
        txtUsername = New TextBox()
        txtFullName = New TextBox()
        txtEmail = New TextBox()
        txtPassword = New TextBox()
        txtConfirmPassword = New TextBox()
        cmbUserRole = New ComboBox()
        cmbRoleFilter = New ComboBox()
        txtSearch = New TextBox()
        btnSearch = New Button()
        btnRefresh = New Button()
        btnAdd = New Button()
        btnUpdate = New Button()
        btnDelete = New Button()
        btnResetPassword = New Button()
        chkIsActive = New CheckBox()
        lblPasswordNote = New Label()
        lblTotalUsers = New Label()
        lblUserStats = New Label()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        lblTitle.Location = New Point(20, 15)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(204, 30)
        lblTitle.TabIndex = 0
        lblTitle.Text = "User Management"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.BackgroundColor = Color.White
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(25, 100)
        DataGridView1.MultiSelect = False
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Size = New Size(934, 291)
        DataGridView1.TabIndex = 5
        ' 
        ' txtUsername
        ' 
        txtUsername.Font = New Font("Segoe UI", 10.0F)
        txtUsername.Location = New Point(25, 470)
        txtUsername.Name = "txtUsername"
        txtUsername.PlaceholderText = "Username"
        txtUsername.Size = New Size(180, 25)
        txtUsername.TabIndex = 6
        ' 
        ' txtFullName
        ' 
        txtFullName.Font = New Font("Segoe UI", 10.0F)
        txtFullName.Location = New Point(215, 470)
        txtFullName.Name = "txtFullName"
        txtFullName.PlaceholderText = "Full Name"
        txtFullName.Size = New Size(220, 25)
        txtFullName.TabIndex = 7
        ' 
        ' txtEmail
        ' 
        txtEmail.Font = New Font("Segoe UI", 10.0F)
        txtEmail.Location = New Point(445, 470)
        txtEmail.Name = "txtEmail"
        txtEmail.PlaceholderText = "Email"
        txtEmail.Size = New Size(250, 25)
        txtEmail.TabIndex = 8
        ' 
        ' txtPassword
        ' 
        txtPassword.Font = New Font("Segoe UI", 10.0F)
        txtPassword.Location = New Point(25, 505)
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "●"c
        txtPassword.PlaceholderText = "Password"
        txtPassword.Size = New Size(180, 25)
        txtPassword.TabIndex = 9
        ' 
        ' txtConfirmPassword
        ' 
        txtConfirmPassword.Font = New Font("Segoe UI", 10.0F)
        txtConfirmPassword.Location = New Point(215, 505)
        txtConfirmPassword.Name = "txtConfirmPassword"
        txtConfirmPassword.PasswordChar = "●"c
        txtConfirmPassword.PlaceholderText = "Confirm Password"
        txtConfirmPassword.Size = New Size(220, 25)
        txtConfirmPassword.TabIndex = 10
        ' 
        ' cmbUserRole
        ' 
        cmbUserRole.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUserRole.Font = New Font("Segoe UI", 10.0F)
        cmbUserRole.Location = New Point(445, 505)
        cmbUserRole.Name = "cmbUserRole"
        cmbUserRole.Size = New Size(150, 25)
        cmbUserRole.TabIndex = 11
        ' 
        ' cmbRoleFilter
        ' 
        cmbRoleFilter.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRoleFilter.Font = New Font("Segoe UI", 10.0F)
        cmbRoleFilter.Location = New Point(320, 60)
        cmbRoleFilter.Name = "cmbRoleFilter"
        cmbRoleFilter.Size = New Size(150, 25)
        cmbRoleFilter.TabIndex = 2
        ' 
        ' txtSearch
        ' 
        txtSearch.Font = New Font("Segoe UI", 10.0F)
        txtSearch.Location = New Point(25, 60)
        txtSearch.Name = "txtSearch"
        txtSearch.PlaceholderText = "Search by username, name, or email..."
        txtSearch.Size = New Size(280, 25)
        txtSearch.TabIndex = 1
        ' 
        ' btnSearch
        ' 
        btnSearch.Font = New Font("Segoe UI", 10.0F)
        btnSearch.Location = New Point(480, 60)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(80, 27)
        btnSearch.TabIndex = 3
        btnSearch.Text = "Search"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Font = New Font("Segoe UI", 10.0F)
        btnRefresh.Location = New Point(570, 60)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(80, 27)
        btnRefresh.TabIndex = 4
        btnRefresh.Text = "Refresh"
        btnRefresh.UseVisualStyleBackColor = True
        ' 
        ' btnAdd
        ' 
        btnAdd.Font = New Font("Segoe UI", 10.0F)
        btnAdd.Location = New Point(25, 570)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(90, 30)
        btnAdd.TabIndex = 14
        btnAdd.Text = "Add"
        btnAdd.UseVisualStyleBackColor = True
        ' 
        ' btnUpdate
        ' 
        btnUpdate.Font = New Font("Segoe UI", 10.0F)
        btnUpdate.Location = New Point(125, 570)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Size = New Size(90, 30)
        btnUpdate.TabIndex = 15
        btnUpdate.Text = "Update"
        btnUpdate.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Font = New Font("Segoe UI", 10.0F)
        btnDelete.Location = New Point(225, 570)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(90, 30)
        btnDelete.TabIndex = 16
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnResetPassword
        ' 
        btnResetPassword.Font = New Font("Segoe UI", 10.0F)
        btnResetPassword.Location = New Point(325, 570)
        btnResetPassword.Name = "btnResetPassword"
        btnResetPassword.Size = New Size(130, 30)
        btnResetPassword.TabIndex = 17
        btnResetPassword.Text = "Reset Password"
        btnResetPassword.UseVisualStyleBackColor = True
        ' 
        ' chkIsActive
        ' 
        chkIsActive.AutoSize = True
        chkIsActive.Checked = True
        chkIsActive.CheckState = CheckState.Checked
        chkIsActive.Font = New Font("Segoe UI", 10.0F)
        chkIsActive.Location = New Point(610, 507)
        chkIsActive.Name = "chkIsActive"
        chkIsActive.Size = New Size(65, 23)
        chkIsActive.TabIndex = 12
        chkIsActive.Text = "Active"
        ' 
        ' lblPasswordNote
        ' 
        lblPasswordNote.AutoSize = True
        lblPasswordNote.Font = New Font("Segoe UI", 9.0F, FontStyle.Italic)
        lblPasswordNote.ForeColor = Color.Gray
        lblPasswordNote.Location = New Point(25, 535)
        lblPasswordNote.Name = "lblPasswordNote"
        lblPasswordNote.Size = New Size(210, 15)
        lblPasswordNote.TabIndex = 13
        lblPasswordNote.Text = "Password must be at least 6 characters"
        ' 
        ' lblTotalUsers
        ' 
        lblTotalUsers.AutoSize = True
        lblTotalUsers.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblTotalUsers.Location = New Point(750, 470)
        lblTotalUsers.Name = "lblTotalUsers"
        lblTotalUsers.Size = New Size(98, 19)
        lblTotalUsers.TabIndex = 18
        lblTotalUsers.Text = "Total Users: 0"
        ' 
        ' lblUserStats
        ' 
        lblUserStats.AutoSize = True
        lblUserStats.Font = New Font("Segoe UI", 9.0F)
        lblUserStats.ForeColor = Color.Gray
        lblUserStats.Location = New Point(750, 495)
        lblUserStats.Name = "lblUserStats"
        lblUserStats.Size = New Size(182, 15)
        lblUserStats.TabIndex = 19
        lblUserStats.Text = "Admins: 0 | Managers: 0 | Users: 0"
        ' 
        ' frmUserManagement
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(984, 561)
        Controls.Add(lblTitle)
        Controls.Add(txtSearch)
        Controls.Add(cmbRoleFilter)
        Controls.Add(btnSearch)
        Controls.Add(btnRefresh)
        Controls.Add(DataGridView1)
        Controls.Add(txtUsername)
        Controls.Add(txtFullName)
        Controls.Add(txtEmail)
        Controls.Add(txtPassword)
        Controls.Add(txtConfirmPassword)
        Controls.Add(cmbUserRole)
        Controls.Add(chkIsActive)
        Controls.Add(lblPasswordNote)
        Controls.Add(btnAdd)
        Controls.Add(btnUpdate)
        Controls.Add(btnDelete)
        Controls.Add(btnResetPassword)
        Controls.Add(lblTotalUsers)
        Controls.Add(lblUserStats)
        MaximizeBox = False
        Name = "frmUserManagement"
        StartPosition = FormStartPosition.CenterScreen
        Text = "User Management"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtConfirmPassword As TextBox
    Friend WithEvents cmbUserRole As ComboBox
    Friend WithEvents cmbRoleFilter As ComboBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnResetPassword As Button
    Friend WithEvents chkIsActive As CheckBox
    Friend WithEvents lblPasswordNote As Label
    Friend WithEvents lblTotalUsers As Label
    Friend WithEvents lblUserStats As Label

End Class
