<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmIssuedEquipment
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
        Label1 = New Label()
        cmbItemID = New ComboBox()
        lblAvailableQty = New Label()
        Label2 = New Label()
        txtIssuedTo = New TextBox()
        Label3 = New Label()
        dtpDateIssued = New DateTimePicker()
        Label4 = New Label()
        dtpReturnDate = New DateTimePicker()
        Label5 = New Label()
        txtRemarks = New TextBox()
        btnIssue = New Button()
        btnReturn = New Button()
        btnClear = New Button()
        btnRefresh = New Button()
        Label6 = New Label()
        txtSearch = New TextBox()
        btnSearch = New Button()
        btnViewAll = New Button()
        btnViewOverdue = New Button()
        DataGridView1 = New DataGridView()
        Panel1 = New Panel()
        lblSelectedItem = New Label()
        lblOverdue = New Label()
        lblTotalIssued = New Label()
        Panel2 = New Panel()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(14, 17)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(76, 16)
        Label1.TabIndex = 0
        Label1.Text = "Select Item:"
        ' 
        ' cmbItemID
        ' 
        cmbItemID.DropDownStyle = ComboBoxStyle.DropDownList
        cmbItemID.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        cmbItemID.FormattingEnabled = True
        cmbItemID.Location = New Point(113, 14)
        cmbItemID.Margin = New Padding(4, 3, 4, 3)
        cmbItemID.Name = "cmbItemID"
        cmbItemID.Size = New Size(349, 24)
        cmbItemID.TabIndex = 1
        ' 
        ' lblAvailableQty
        ' 
        lblAvailableQty.AutoSize = True
        lblAvailableQty.Font = New Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblAvailableQty.ForeColor = SystemColors.ControlText
        lblAvailableQty.Location = New Point(490, 16)
        lblAvailableQty.Margin = New Padding(4, 0, 4, 0)
        lblAvailableQty.Name = "lblAvailableQty"
        lblAvailableQty.Size = New Size(93, 18)
        lblAvailableQty.TabIndex = 2
        lblAvailableQty.Text = "Available: 0"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(14, 58)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(70, 16)
        Label2.TabIndex = 3
        Label2.Text = "Issued To:"
        ' 
        ' txtIssuedTo
        ' 
        txtIssuedTo.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtIssuedTo.Location = New Point(113, 54)
        txtIssuedTo.Margin = New Padding(4, 3, 4, 3)
        txtIssuedTo.Name = "txtIssuedTo"
        txtIssuedTo.Size = New Size(349, 22)
        txtIssuedTo.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(14, 98)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(82, 16)
        Label3.TabIndex = 5
        Label3.Text = "Date Issued:"
        ' 
        ' dtpDateIssued
        ' 
        dtpDateIssued.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtpDateIssued.Format = DateTimePickerFormat.Short
        dtpDateIssued.Location = New Point(120, 95)
        dtpDateIssued.Margin = New Padding(4, 3, 4, 3)
        dtpDateIssued.Name = "dtpDateIssued"
        dtpDateIssued.Size = New Size(139, 22)
        dtpDateIssued.TabIndex = 6
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(280, 98)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(109, 16)
        Label4.TabIndex = 7
        Label4.Text = "Expected Return:"
        ' 
        ' dtpReturnDate
        ' 
        dtpReturnDate.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtpReturnDate.Format = DateTimePickerFormat.Short
        dtpReturnDate.Location = New Point(414, 95)
        dtpReturnDate.Margin = New Padding(4, 3, 4, 3)
        dtpReturnDate.Name = "dtpReturnDate"
        dtpReturnDate.Size = New Size(139, 22)
        dtpReturnDate.TabIndex = 8
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(14, 138)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(65, 16)
        Label5.TabIndex = 9
        Label5.Text = "Remarks:"
        ' 
        ' txtRemarks
        ' 
        txtRemarks.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtRemarks.Location = New Point(113, 135)
        txtRemarks.Margin = New Padding(4, 3, 4, 3)
        txtRemarks.Multiline = True
        txtRemarks.Name = "txtRemarks"
        txtRemarks.Size = New Size(349, 57)
        txtRemarks.TabIndex = 10
        ' 
        ' btnIssue
        ' 
        btnIssue.BackColor = Color.SteelBlue
        btnIssue.FlatStyle = FlatStyle.Flat
        btnIssue.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnIssue.ForeColor = Color.White
        btnIssue.Location = New Point(18, 213)
        btnIssue.Margin = New Padding(4, 3, 4, 3)
        btnIssue.Name = "btnIssue"
        btnIssue.Size = New Size(152, 40)
        btnIssue.TabIndex = 11
        btnIssue.Text = "Issue Equipment"
        btnIssue.UseVisualStyleBackColor = False
        ' 
        ' btnReturn
        ' 
        btnReturn.BackColor = Color.SteelBlue
        btnReturn.FlatStyle = FlatStyle.Flat
        btnReturn.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnReturn.ForeColor = Color.White
        btnReturn.Location = New Point(176, 213)
        btnReturn.Margin = New Padding(4, 3, 4, 3)
        btnReturn.Name = "btnReturn"
        btnReturn.Size = New Size(152, 40)
        btnReturn.TabIndex = 12
        btnReturn.Text = "Return Equipment"
        btnReturn.UseVisualStyleBackColor = False
        ' 
        ' btnClear
        ' 
        btnClear.BackColor = Color.SteelBlue
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnClear.ForeColor = Color.White
        btnClear.Location = New Point(335, 213)
        btnClear.Margin = New Padding(4, 3, 4, 3)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(93, 40)
        btnClear.TabIndex = 13
        btnClear.Text = "Clear"
        btnClear.UseVisualStyleBackColor = False
        ' 
        ' btnRefresh
        ' 
        btnRefresh.BackColor = Color.SteelBlue
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnRefresh.ForeColor = Color.White
        btnRefresh.Location = New Point(435, 213)
        btnRefresh.Margin = New Padding(4, 3, 4, 3)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(93, 40)
        btnRefresh.TabIndex = 14
        btnRefresh.Text = "Refresh"
        btnRefresh.UseVisualStyleBackColor = False
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(14, 275)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(53, 16)
        Label6.TabIndex = 15
        Label6.Text = "Search:"
        ' 
        ' txtSearch
        ' 
        txtSearch.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtSearch.Location = New Point(84, 271)
        txtSearch.Margin = New Padding(4, 3, 4, 3)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(233, 22)
        txtSearch.TabIndex = 16
        ' 
        ' btnSearch
        ' 
        btnSearch.BackColor = Color.FromArgb(CByte(23), CByte(162), CByte(184))
        btnSearch.FlatStyle = FlatStyle.Flat
        btnSearch.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnSearch.ForeColor = Color.White
        btnSearch.Location = New Point(324, 268)
        btnSearch.Margin = New Padding(4, 3, 4, 3)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(88, 32)
        btnSearch.TabIndex = 17
        btnSearch.Text = "Search"
        btnSearch.UseVisualStyleBackColor = False
        ' 
        ' btnViewAll
        ' 
        btnViewAll.BackColor = Color.FromArgb(CByte(23), CByte(162), CByte(184))
        btnViewAll.FlatStyle = FlatStyle.Flat
        btnViewAll.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnViewAll.ForeColor = Color.White
        btnViewAll.Location = New Point(419, 268)
        btnViewAll.Margin = New Padding(4, 3, 4, 3)
        btnViewAll.Name = "btnViewAll"
        btnViewAll.Size = New Size(88, 32)
        btnViewAll.TabIndex = 18
        btnViewAll.Text = "View All"
        btnViewAll.UseVisualStyleBackColor = False
        ' 
        ' btnViewOverdue
        ' 
        btnViewOverdue.BackColor = Color.FromArgb(CByte(220), CByte(53), CByte(69))
        btnViewOverdue.FlatStyle = FlatStyle.Flat
        btnViewOverdue.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnViewOverdue.ForeColor = Color.White
        btnViewOverdue.Location = New Point(513, 268)
        btnViewOverdue.Margin = New Padding(4, 3, 4, 3)
        btnViewOverdue.Name = "btnViewOverdue"
        btnViewOverdue.Size = New Size(117, 32)
        btnViewOverdue.TabIndex = 19
        btnViewOverdue.Text = "View Overdue"
        btnViewOverdue.UseVisualStyleBackColor = False
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = New Point(0, 0)
        DataGridView1.Margin = New Padding(4, 3, 4, 3)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Size = New Size(1381, 384)
        DataGridView1.TabIndex = 20
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(248), CByte(249), CByte(250))
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(lblSelectedItem)
        Panel1.Controls.Add(lblOverdue)
        Panel1.Controls.Add(lblTotalIssued)
        Panel1.Dock = DockStyle.Bottom
        Panel1.Location = New Point(0, 517)
        Panel1.Margin = New Padding(4, 3, 4, 3)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(984, 44)
        Panel1.TabIndex = 21
        ' 
        ' lblSelectedItem
        ' 
        lblSelectedItem.AutoSize = True
        lblSelectedItem.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblSelectedItem.Location = New Point(408, 12)
        lblSelectedItem.Margin = New Padding(4, 0, 4, 0)
        lblSelectedItem.Name = "lblSelectedItem"
        lblSelectedItem.Size = New Size(114, 16)
        lblSelectedItem.TabIndex = 2
        lblSelectedItem.Text = "Selected: None"
        ' 
        ' lblOverdue
        ' 
        lblOverdue.AutoSize = True
        lblOverdue.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblOverdue.ForeColor = Color.Green
        lblOverdue.Location = New Point(210, 12)
        lblOverdue.Margin = New Padding(4, 0, 4, 0)
        lblOverdue.Name = "lblOverdue"
        lblOverdue.Size = New Size(82, 16)
        lblOverdue.TabIndex = 1
        lblOverdue.Text = "Overdue: 0"
        ' 
        ' lblTotalIssued
        ' 
        lblTotalIssued.AutoSize = True
        lblTotalIssued.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTotalIssued.Location = New Point(12, 12)
        lblTotalIssued.Margin = New Padding(4, 0, 4, 0)
        lblTotalIssued.Name = "lblTotalIssued"
        lblTotalIssued.Size = New Size(109, 16)
        lblTotalIssued.TabIndex = 0
        lblTotalIssued.Text = "Total Issued: 0"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(DataGridView1)
        Panel2.Location = New Point(0, 312)
        Panel2.Margin = New Padding(4, 3, 4, 3)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1381, 384)
        Panel2.TabIndex = 22
        ' 
        ' frmIssuedEquipment
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(984, 561)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Controls.Add(btnViewOverdue)
        Controls.Add(btnViewAll)
        Controls.Add(btnSearch)
        Controls.Add(txtSearch)
        Controls.Add(Label6)
        Controls.Add(btnRefresh)
        Controls.Add(btnClear)
        Controls.Add(btnReturn)
        Controls.Add(btnIssue)
        Controls.Add(txtRemarks)
        Controls.Add(Label5)
        Controls.Add(dtpReturnDate)
        Controls.Add(Label4)
        Controls.Add(dtpDateIssued)
        Controls.Add(Label3)
        Controls.Add(txtIssuedTo)
        Controls.Add(Label2)
        Controls.Add(lblAvailableQty)
        Controls.Add(cmbItemID)
        Controls.Add(Label1)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmIssuedEquipment"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Issued Equipment Management"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cmbItemID As ComboBox
    Friend WithEvents lblAvailableQty As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtIssuedTo As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpDateIssued As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents dtpReturnDate As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents txtRemarks As TextBox
    Friend WithEvents btnIssue As Button
    Friend WithEvents btnReturn As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnViewAll As Button
    Friend WithEvents btnViewOverdue As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblSelectedItem As Label
    Friend WithEvents lblOverdue As Label
    Friend WithEvents lblTotalIssued As Label
    Friend WithEvents Panel2 As Panel
End Class