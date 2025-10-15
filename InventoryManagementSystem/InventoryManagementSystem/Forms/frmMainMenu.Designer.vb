<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMainMenu
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
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Panel1 = New Panel()
        lblDateTime = New Label()
        lblWelcome = New Label()
        lblSystemTitle = New Label()
        Panel2 = New Panel()
        Label1 = New Label()
        PictureBox1 = New PictureBox()
        btnActivityLog = New Button()
        btnUserManagement = New Button()
        btnReports = New Button()
        btnIssuedEquipment = New Button()
        btnManageInventory = New Button()
        Panel3 = New Panel()
        btnExit = New Button()
        btnLogout = New Button()
        Timer1 = New Timer(components)
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(0), CByte(122), CByte(204))
        Panel1.Controls.Add(lblDateTime)
        Panel1.Controls.Add(lblWelcome)
        Panel1.Controls.Add(lblSystemTitle)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(4, 3, 4, 3)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(933, 115)
        Panel1.TabIndex = 0
        ' 
        ' lblDateTime
        ' 
        lblDateTime.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblDateTime.Font = New Font("Microsoft Sans Serif", 9.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblDateTime.ForeColor = Color.White
        lblDateTime.Location = New Point(467, 87)
        lblDateTime.Margin = New Padding(4, 0, 4, 0)
        lblDateTime.Name = "lblDateTime"
        lblDateTime.Size = New Size(453, 23)
        lblDateTime.TabIndex = 2
        lblDateTime.Text = "Monday, January 01, 2025 - 12:00:00 PM"
        lblDateTime.TextAlign = ContentAlignment.TopRight
        ' 
        ' lblWelcome
        ' 
        lblWelcome.AutoSize = True
        lblWelcome.Font = New Font("Microsoft Sans Serif", 12.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblWelcome.ForeColor = Color.White
        lblWelcome.Location = New Point(14, 81)
        lblWelcome.Margin = New Padding(4, 0, 4, 0)
        lblWelcome.Name = "lblWelcome"
        lblWelcome.Size = New Size(176, 20)
        lblWelcome.TabIndex = 1
        lblWelcome.Text = "Welcome, User (Admin)"
        ' 
        ' lblSystemTitle
        ' 
        lblSystemTitle.AutoSize = True
        lblSystemTitle.Font = New Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblSystemTitle.ForeColor = Color.White
        lblSystemTitle.Location = New Point(14, 17)
        lblSystemTitle.Margin = New Padding(4, 0, 4, 0)
        lblSystemTitle.Name = "lblSystemTitle"
        lblSystemTitle.Size = New Size(520, 31)
        lblSystemTitle.TabIndex = 0
        lblSystemTitle.Text = "INVENTORY MANAGEMENT SYSTEM"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.White
        Panel2.BorderStyle = BorderStyle.FixedSingle
        Panel2.Controls.Add(Label1)
        Panel2.Controls.Add(PictureBox1)
        Panel2.Controls.Add(btnActivityLog)
        Panel2.Controls.Add(btnUserManagement)
        Panel2.Controls.Add(btnReports)
        Panel2.Controls.Add(btnIssuedEquipment)
        Panel2.Controls.Add(btnManageInventory)
        Panel2.Location = New Point(58, 150)
        Panel2.Margin = New Padding(4, 3, 4, 3)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(816, 438)
        Panel2.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.BackColor = Color.SteelBlue
        Label1.Font = New Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.White
        Label1.Location = New Point(443, 288)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(327, 104)
        Label1.TabIndex = 6
        Label1.Text = "Quick Access" & vbCrLf & "Dashboard"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.SteelBlue
        PictureBox1.Location = New Point(443, 265)
        PictureBox1.Margin = New Padding(4, 3, 4, 3)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(327, 150)
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' btnActivityLog
        ' 
        btnActivityLog.BackColor = Color.SteelBlue
        btnActivityLog.Cursor = Cursors.Hand
        btnActivityLog.FlatAppearance.BorderSize = 0
        btnActivityLog.FlatStyle = FlatStyle.Flat
        btnActivityLog.Font = New Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnActivityLog.ForeColor = Color.White
        btnActivityLog.Location = New Point(47, 265)
        btnActivityLog.Margin = New Padding(4, 3, 4, 3)
        btnActivityLog.Name = "btnActivityLog"
        btnActivityLog.Size = New Size(327, 63)
        btnActivityLog.TabIndex = 4
        btnActivityLog.Text = "📝 Activity Log"
        btnActivityLog.UseVisualStyleBackColor = False
        ' 
        ' btnUserManagement
        ' 
        btnUserManagement.BackColor = Color.SteelBlue
        btnUserManagement.Cursor = Cursors.Hand
        btnUserManagement.FlatAppearance.BorderSize = 0
        btnUserManagement.FlatStyle = FlatStyle.Flat
        btnUserManagement.Font = New Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnUserManagement.ForeColor = Color.White
        btnUserManagement.Location = New Point(443, 179)
        btnUserManagement.Margin = New Padding(4, 3, 4, 3)
        btnUserManagement.Name = "btnUserManagement"
        btnUserManagement.Size = New Size(327, 63)
        btnUserManagement.TabIndex = 3
        btnUserManagement.Text = "👥 User Management"
        btnUserManagement.UseVisualStyleBackColor = False
        ' 
        ' btnReports
        ' 
        btnReports.BackColor = Color.SteelBlue
        btnReports.Cursor = Cursors.Hand
        btnReports.FlatAppearance.BorderSize = 0
        btnReports.FlatStyle = FlatStyle.Flat
        btnReports.Font = New Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnReports.ForeColor = Color.White
        btnReports.Location = New Point(47, 179)
        btnReports.Margin = New Padding(4, 3, 4, 3)
        btnReports.Name = "btnReports"
        btnReports.Size = New Size(327, 63)
        btnReports.TabIndex = 2
        btnReports.Text = "📊 Reports"
        btnReports.UseVisualStyleBackColor = False
        ' 
        ' btnIssuedEquipment
        ' 
        btnIssuedEquipment.BackColor = Color.SteelBlue
        btnIssuedEquipment.Cursor = Cursors.Hand
        btnIssuedEquipment.FlatAppearance.BorderSize = 0
        btnIssuedEquipment.FlatStyle = FlatStyle.Flat
        btnIssuedEquipment.Font = New Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnIssuedEquipment.ForeColor = Color.White
        btnIssuedEquipment.Location = New Point(443, 92)
        btnIssuedEquipment.Margin = New Padding(4, 3, 4, 3)
        btnIssuedEquipment.Name = "btnIssuedEquipment"
        btnIssuedEquipment.Size = New Size(327, 63)
        btnIssuedEquipment.TabIndex = 1
        btnIssuedEquipment.Text = "🔄 Issued Equipment"
        btnIssuedEquipment.UseVisualStyleBackColor = False
        ' 
        ' btnManageInventory
        ' 
        btnManageInventory.BackColor = Color.SteelBlue
        btnManageInventory.Cursor = Cursors.Hand
        btnManageInventory.FlatAppearance.BorderSize = 0
        btnManageInventory.FlatStyle = FlatStyle.Flat
        btnManageInventory.Font = New Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnManageInventory.ForeColor = Color.White
        btnManageInventory.Location = New Point(47, 92)
        btnManageInventory.Margin = New Padding(4, 3, 4, 3)
        btnManageInventory.Name = "btnManageInventory"
        btnManageInventory.Size = New Size(327, 63)
        btnManageInventory.TabIndex = 0
        btnManageInventory.Text = "📦 Manage Inventory"
        btnManageInventory.UseVisualStyleBackColor = False
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.White
        Panel3.BorderStyle = BorderStyle.FixedSingle
        Panel3.Controls.Add(btnExit)
        Panel3.Controls.Add(btnLogout)
        Panel3.Location = New Point(58, 612)
        Panel3.Margin = New Padding(4, 3, 4, 3)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(816, 92)
        Panel3.TabIndex = 2
        ' 
        ' btnExit
        ' 
        btnExit.BackColor = Color.FromArgb(CByte(220), CByte(53), CByte(69))
        btnExit.Cursor = Cursors.Hand
        btnExit.FlatAppearance.BorderSize = 0
        btnExit.FlatStyle = FlatStyle.Flat
        btnExit.Font = New Font("Microsoft Sans Serif", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnExit.ForeColor = Color.White
        btnExit.Location = New Point(443, 17)
        btnExit.Margin = New Padding(4, 3, 4, 3)
        btnExit.Name = "btnExit"
        btnExit.Size = New Size(327, 52)
        btnExit.TabIndex = 1
        btnExit.Text = "❌ Exit Application"
        btnExit.UseVisualStyleBackColor = False
        ' 
        ' btnLogout
        ' 
        btnLogout.BackColor = Color.SteelBlue
        btnLogout.Cursor = Cursors.Hand
        btnLogout.FlatAppearance.BorderSize = 0
        btnLogout.FlatStyle = FlatStyle.Flat
        btnLogout.Font = New Font("Microsoft Sans Serif", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnLogout.ForeColor = Color.White
        btnLogout.Location = New Point(47, 17)
        btnLogout.Margin = New Padding(4, 3, 4, 3)
        btnLogout.Name = "btnLogout"
        btnLogout.Size = New Size(327, 52)
        btnLogout.TabIndex = 0
        btnLogout.Text = "🚪 Logout"
        btnLogout.UseVisualStyleBackColor = False
        ' 
        ' Timer1
        ' 
        Timer1.Enabled = True
        Timer1.Interval = 1000
        ' 
        ' frmMainMenu
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(245), CByte(245))
        ClientSize = New Size(933, 750)
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        Name = "frmMainMenu"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Inventory Management System - Main Menu"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblSystemTitle As Label
    Friend WithEvents lblWelcome As Label
    Friend WithEvents lblDateTime As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnManageInventory As Button
    Friend WithEvents btnIssuedEquipment As Button
    Friend WithEvents btnReports As Button
    Friend WithEvents btnUserManagement As Button
    Friend WithEvents btnActivityLog As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnLogout As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
End Class