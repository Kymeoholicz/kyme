<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInventory
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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblItemName = New Label()
        lblCategory = New Label()
        lblQuantity = New Label()
        lblCondition = New Label()
        lblLocation = New Label()
        txtItemName = New TextBox()
        txtQuantity = New TextBox()
        txtLocation = New TextBox()
        txtCondition = New TextBox()
        txtCategory = New TextBox()
        btnAdd = New Button()
        btnUpdate = New Button()
        btnDelete = New Button()
        btnClear = New Button()
        btnRefresh = New Button()
        btnSearch = New Button()
        lblSearch = New Label()
        txtSearch = New TextBox()
        dgvInventory = New DataGridView()
        btnBack = New Button()
        CType(dgvInventory, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'lblItemName
        '
        lblItemName.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblItemName.Location = New Point(40, 80)
        lblItemName.Name = "lblItemName"
        lblItemName.Size = New Size(100, 25)
        lblItemName.TabIndex = 0
        lblItemName.Text = "Item Name:"
        '
        'lblCategory
        '
        lblCategory.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblCategory.Location = New Point(400, 80)
        lblCategory.Name = "lblCategory"
        lblCategory.Size = New Size(80, 25)
        lblCategory.TabIndex = 1
        lblCategory.Text = "Category:"
        '
        'lblQuantity
        '
        lblQuantity.Location = New Point(40, 120)
        lblQuantity.Name = "lblQuantity"
        lblQuantity.Size = New Size(100, 25)
        lblQuantity.TabIndex = 2
        lblQuantity.Text = "Quantity:"
        '
        'lblCondition
        '
        lblCondition.AutoSize = True
        lblCondition.Location = New Point(260, 120)
        lblCondition.Name = "lblCondition"
        lblCondition.Size = New Size(63, 15)
        lblCondition.TabIndex = 3
        lblCondition.Text = "Condition:"
        '
        'lblLocation
        '
        lblLocation.Location = New Point(600, 120)
        lblLocation.Name = "lblLocation"
        lblLocation.Size = New Size(80, 25)
        lblLocation.TabIndex = 4
        lblLocation.Text = "Location:"
        '
        'txtItemName
        '
        txtItemName.Location = New Point(133, 80)
        txtItemName.Name = "txtItemName"
        txtItemName.Size = New Size(200, 23)
        txtItemName.TabIndex = 5
        '
        'txtQuantity
        '
        txtQuantity.Location = New Point(140, 120)
        txtQuantity.Name = "txtQuantity"
        txtQuantity.Size = New Size(80, 23)
        txtQuantity.TabIndex = 6
        '
        'txtLocation
        '
        txtLocation.Location = New Point(680, 120)
        txtLocation.Name = "txtLocation"
        txtLocation.Size = New Size(200, 23)
        txtLocation.TabIndex = 7
        '
        'txtCondition
        '
        txtCondition.Location = New Point(350, 120)
        txtCondition.Name = "txtCondition"
        txtCondition.Size = New Size(200, 23)
        txtCondition.TabIndex = 8
        '
        'txtCategory
        '
        txtCategory.Location = New Point(480, 80)
        txtCategory.Name = "txtCategory"
        txtCategory.Size = New Size(200, 23)
        txtCategory.TabIndex = 9
        '
        'btnAdd
        '
        btnAdd.BackColor = Color.SteelBlue
        btnAdd.ForeColor = Color.White
        btnAdd.Location = New Point(140, 170)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(90, 35)
        btnAdd.TabIndex = 10
        btnAdd.Text = "Add"
        btnAdd.UseVisualStyleBackColor = False
        '
        'btnUpdate
        '
        btnUpdate.BackColor = Color.SteelBlue
        btnUpdate.ForeColor = Color.White
        btnUpdate.Location = New Point(240, 170)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Size = New Size(90, 35)
        btnUpdate.TabIndex = 11
        btnUpdate.Text = "Update"
        btnUpdate.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        btnDelete.BackColor = Color.SteelBlue
        btnDelete.ForeColor = Color.White
        btnDelete.Location = New Point(340, 170)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(90, 35)
        btnDelete.TabIndex = 12
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        btnClear.BackColor = Color.SteelBlue
        btnClear.ForeColor = Color.White
        btnClear.Location = New Point(440, 170)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(90, 35)
        btnClear.TabIndex = 13
        btnClear.Text = "Clear"
        btnClear.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        btnRefresh.BackColor = Color.SteelBlue
        btnRefresh.ForeColor = Color.White
        btnRefresh.Location = New Point(540, 170)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(90, 35)
        btnRefresh.TabIndex = 14
        btnRefresh.Text = "Refresh"
        btnRefresh.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        btnSearch.BackColor = Color.SteelBlue
        btnSearch.ForeColor = Color.White
        btnSearch.Location = New Point(380, 230)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(90, 30)
        btnSearch.TabIndex = 15
        btnSearch.Text = "Search"
        btnSearch.UseVisualStyleBackColor = False
        '
        'lblSearch
        '
        lblSearch.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblSearch.Location = New Point(40, 230)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(80, 25)
        lblSearch.TabIndex = 16
        lblSearch.Text = "Search:"
        '
        'txtSearch
        '
        txtSearch.Location = New Point(120, 230)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(250, 23)
        txtSearch.TabIndex = 17
        '
        'dgvInventory
        '
        dgvInventory.AllowUserToAddRows = False
        dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvInventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvInventory.Location = New Point(40, 280)
        dgvInventory.Name = "dgvInventory"
        dgvInventory.ReadOnly = True
        dgvInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvInventory.Size = New Size(900, 300)
        dgvInventory.TabIndex = 18
        '
        'btnBack
        '
        btnBack.BackColor = Color.FromArgb(0, 120, 215)
        btnBack.FlatAppearance.BorderSize = 0
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.Font = New Font("Segoe UI", 9.75!, FontStyle.Bold, GraphicsUnit.Point)
        btnBack.ForeColor = Color.White
        btnBack.Location = New Point(20, 20)
        btnBack.Name = "btnBack"
        btnBack.Size = New Size(160, 35)
        btnBack.TabIndex = 19
        btnBack.Text = "← Back to Main Menu"
        btnBack.UseVisualStyleBackColor = False
        '
        'frmInventory
        '
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(984, 611)
        Controls.Add(btnBack)
        Controls.Add(dgvInventory)
        Controls.Add(txtSearch)
        Controls.Add(lblSearch)
        Controls.Add(btnSearch)
        Controls.Add(btnRefresh)
        Controls.Add(btnClear)
        Controls.Add(btnDelete)
        Controls.Add(btnUpdate)
        Controls.Add(btnAdd)
        Controls.Add(txtCategory)
        Controls.Add(txtCondition)
        Controls.Add(txtLocation)
        Controls.Add(txtQuantity)
        Controls.Add(txtItemName)
        Controls.Add(lblLocation)
        Controls.Add(lblCondition)
        Controls.Add(lblQuantity)
        Controls.Add(lblCategory)
        Controls.Add(lblItemName)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "frmInventory"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Inventory Management"
        CType(dgvInventory, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblItemName As Label
    Friend WithEvents lblCategory As Label
    Friend WithEvents lblQuantity As Label
    Friend WithEvents lblCondition As Label
    Friend WithEvents lblLocation As Label
    Friend WithEvents txtItemName As TextBox
    Friend WithEvents txtQuantity As TextBox
    Friend WithEvents txtLocation As TextBox
    Friend WithEvents txtCondition As TextBox
    Friend WithEvents txtCategory As TextBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents dgvInventory As DataGridView
    Friend WithEvents btnBack As Button
End Class
