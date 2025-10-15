<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmResetPassword
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents lblUsername As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtConfirmPassword As TextBox
    Friend WithEvents chkShowPassword As CheckBox
    Friend WithEvents btnReset As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnGeneratePassword As Button

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox()
        Me.chkShowPassword = New System.Windows.Forms.CheckBox()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnGeneratePassword = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblUsername.Location = New System.Drawing.Point(20, 20)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(200, 19)
        Me.lblUsername.Text = "Resetting password for: "
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(20, 60)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = "●"c
        Me.txtPassword.Size = New System.Drawing.Size(250, 23)
        Me.txtPassword.PlaceholderText = "New Password"
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.Location = New System.Drawing.Point(20, 100)
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.PasswordChar = "●"c
        Me.txtConfirmPassword.Size = New System.Drawing.Size(250, 23)
        Me.txtConfirmPassword.PlaceholderText = "Confirm Password"
        '
        'chkShowPassword
        '
        Me.chkShowPassword.AutoSize = True
        Me.chkShowPassword.Location = New System.Drawing.Point(20, 130)
        Me.chkShowPassword.Name = "chkShowPassword"
        Me.chkShowPassword.Size = New System.Drawing.Size(108, 19)
        Me.chkShowPassword.Text = "Show Password"
        '
        'btnGeneratePassword
        '
        Me.btnGeneratePassword.Location = New System.Drawing.Point(280, 60)
        Me.btnGeneratePassword.Name = "btnGeneratePassword"
        Me.btnGeneratePassword.Size = New System.Drawing.Size(130, 25)
        Me.btnGeneratePassword.Text = "Generate Random"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(20, 170)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(100, 30)
        Me.btnReset.Text = "Reset Password"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(140, 170)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 30)
        Me.btnCancel.Text = "Cancel"
        '
        'frmResetPassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(430, 230)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtConfirmPassword)
        Me.Controls.Add(Me.chkShowPassword)
        Me.Controls.Add(Me.btnGeneratePassword)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reset Password"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
End Class
