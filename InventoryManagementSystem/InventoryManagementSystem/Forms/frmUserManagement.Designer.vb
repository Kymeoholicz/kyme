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
        Me.lblUserStats = New System.Windows.Forms.Label()
        Me.lblTotalUsers = New System.Windows.Forms.Label()
        Me.lblPasswordNote = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.cmbUserRole = New System.Windows.Forms.ComboBox()
        Me.chkIsActive = New System.Windows.Forms.CheckBox()
        Me.chkShowPassword = New System.Windows.Forms.CheckBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnResetPassword = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUserStats
        '
        Me.lblUserStats.AutoSize = True
        Me.lblUserStats.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblUserStats.Location = New System.Drawing.Point(20, 15)
        Me.lblUserStats.Name = "lblUserStats"
        Me.lblUserStats.Size = New System.Drawing.Size(60, 25)
        Me.lblUserStats.Text = "Stats"
        '
        'lblTotalUsers
        '
        Me.lblTotalUsers.AutoSize = True
        Me.lblTotalUsers.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalUsers.Location = New System.Drawing.Point(100, 20)
        Me.lblTotalUsers.Name = "lblTotalUsers"
        Me.lblTotalUsers.Size = New System.Drawing.Size(100, 19)
        Me.lblTotalUsers.Text = "Total Users: 0"
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(25, 60)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(200, 23)
        Me.txtUsername.PlaceholderText = "Username"
        '
        'txtFullName
        '
        Me.txtFullName.Location = New System.Drawing.Point(240, 60)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(200, 23)
        Me.txtFullName.PlaceholderText = "Full Name"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(460, 60)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(200, 23)
        Me.txtEmail.PlaceholderText = "Email"
        '
        'cmbUserRole
        '
        Me.cmbUserRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUserRole.Location = New System.Drawing.Point(680, 60)
        Me.cmbUserRole.Name = "cmbUserRole"
        Me.cmbUserRole.Size = New System.Drawing.Size(150, 23)
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(25, 100)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = "●"c
        Me.txtPassword.Size = New System.Drawing.Size(200, 23)
        Me.txtPassword.PlaceholderText = "Password"
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.Location = New System.Drawing.Point(240, 100)
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.PasswordChar = "●"c
        Me.txtConfirmPassword.Size = New System.Drawing.Size(200, 23)
        Me.txtConfirmPassword.PlaceholderText = "Confirm Password"
        '
        'chkShowPassword
        '
        Me.chkShowPassword.AutoSize = True
        Me.chkShowPassword.Location = New System.Drawing.Point(460, 104)
        Me.chkShowPassword.Name = "chkShowPassword"
        Me.chkShowPassword.Size = New System.Drawing.Size(108, 19)
        Me.chkShowPassword.Text = "Show Password"
        '
        'chkIsActive
        '
        Me.chkIsActive.AutoSize = True
        Me.chkIsActive.Location = New System.Drawing.Point(680, 104)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(60, 19)
        Me.chkIsActive.Text = "Active"
        '
        'lblPasswordNote
        '
        Me.lblPasswordNote.AutoSize = True
        Me.lblPasswordNote.ForeColor = System.Drawing.Color.DimGray
        Me.lblPasswordNote.Location = New System.Drawing.Point(25, 130)
        Me.lblPasswordNote.Name = "lblPasswordNote"
        Me.lblPasswordNote.Size = New System.Drawing.Size(215, 15)
        Me.lblPasswordNote.Text = "Password must be at least 6 characters"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(25, 160)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(100, 30)
        Me.btnAdd.Text = "Add User"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(135, 160)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(100, 30)
        Me.btnUpdate.Text = "Update"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(245, 160)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 30)
        Me.btnDelete.Text = "Delete"
        '
        'btnResetPassword
        '
        Me.btnResetPassword.Location = New System.Drawing.Point(355, 160)
        Me.btnResetPassword.Name = "btnResetPassword"
        Me.btnResetPassword.Size = New System.Drawing.Size(120, 30)
        Me.btnResetPassword.Text = "Reset Password"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(485, 160)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 30)
        Me.btnClear.Text = "Clear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(595, 160)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 30)
        Me.btnRefresh.Text = "Refresh"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(25, 210)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(250, 23)
        Me.txtSearch.PlaceholderText = "Search users..."
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(285, 208)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 27)
        Me.btnSearch.Text = "Search"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 250)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(860, 400)
        '
        'frmUserManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(860, 650)
        Me.Controls.Add(Me.lblUserStats)
        Me.Controls.Add(Me.lblTotalUsers)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.txtFullName)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.cmbUserRole)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtConfirmPassword)
        Me.Controls.Add(Me.chkShowPassword)
        Me.Controls.Add(Me.chkIsActive)
        Me.Controls.Add(Me.lblPasswordNote)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnResetPassword)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmUserManagement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Management"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblUserStats As Label
    Friend WithEvents lblTotalUsers As Label
    Friend WithEvents lblPasswordNote As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtConfirmPassword As TextBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents cmbUserRole As ComboBox
    Friend WithEvents chkIsActive As CheckBox
    Friend WithEvents chkShowPassword As CheckBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnResetPassword As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents DataGridView1 As DataGridView

End Class
