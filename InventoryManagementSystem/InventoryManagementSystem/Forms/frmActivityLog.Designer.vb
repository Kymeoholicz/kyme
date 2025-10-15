<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmActivityLog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose
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

    'Required by Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'Controls
    Friend WithEvents cmbFilterType As ComboBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents btnFilter As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnExport As Button
    Friend WithEvents btnClearOldLogs As Button
    Friend WithEvents btnDateFilter As Button
    Friend WithEvents lblTotalRecords As Label
    Friend WithEvents lblActivitySummary As Label
    Friend WithEvents DataGridView1 As DataGridView

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.cmbFilterType = New System.Windows.Forms.ComboBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.btnFilter = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnClearOldLogs = New System.Windows.Forms.Button()
        Me.btnDateFilter = New System.Windows.Forms.Button()
        Me.lblTotalRecords = New System.Windows.Forms.Label()
        Me.lblActivitySummary = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbFilterType
        '
        Me.cmbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilterType.FormattingEnabled = True
        Me.cmbFilterType.Location = New System.Drawing.Point(12, 12)
        Me.cmbFilterType.Name = "cmbFilterType"
        Me.cmbFilterType.Size = New System.Drawing.Size(150, 24)
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(170, 12)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(200, 22)
        '
        'dtpFrom
        '
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(380, 12)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(110, 22)
        '
        'dtpTo
        '
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(500, 12)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(110, 22)
        '
        'btnFilter
        '
        Me.btnFilter.Location = New System.Drawing.Point(620, 10)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(75, 25)
        Me.btnFilter.Text = "Filter"
        Me.btnFilter.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(700, 10)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 25)
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(780, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 25)
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(860, 10)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(120, 25)
        Me.btnExport.Text = "Export to CSV"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnClearOldLogs
        '
        Me.btnClearOldLogs.Location = New System.Drawing.Point(12, 45)
        Me.btnClearOldLogs.Name = "btnClearOldLogs"
        Me.btnClearOldLogs.Size = New System.Drawing.Size(180, 25)
        Me.btnClearOldLogs.Text = "Clear Old Logs (90+ days)"
        Me.btnClearOldLogs.UseVisualStyleBackColor = True
        '
        'btnDateFilter
        '
        Me.btnDateFilter.Location = New System.Drawing.Point(200, 45)
        Me.btnDateFilter.Name = "btnDateFilter"
        Me.btnDateFilter.Size = New System.Drawing.Size(130, 25)
        Me.btnDateFilter.Text = "Filter by Date Range"
        Me.btnDateFilter.UseVisualStyleBackColor = True
        '
        'lblTotalRecords
        '
        Me.lblTotalRecords.AutoSize = True
        Me.lblTotalRecords.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalRecords.Location = New System.Drawing.Point(12, 80)
        Me.lblTotalRecords.Name = "lblTotalRecords"
        Me.lblTotalRecords.Size = New System.Drawing.Size(119, 18)
        Me.lblTotalRecords.Text = "Total Records: 0"
        '
        'lblActivitySummary
        '
        Me.lblActivitySummary.AutoSize = True
        Me.lblActivitySummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblActivitySummary.Location = New System.Drawing.Point(200, 80)
        Me.lblActivitySummary.Name = "lblActivitySummary"
        Me.lblActivitySummary.Size = New System.Drawing.Size(66, 18)
        Me.lblActivitySummary.Text = "Summary"
        '
        'DataGridView1
        '
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 110)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1000, 400)
        '
        'frmActivityLog
        '
        Me.ClientSize = New System.Drawing.Size(1000, 510)
        Me.Controls.Add(Me.cmbFilterType)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.btnFilter)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnClearOldLogs)
        Me.Controls.Add(Me.btnDateFilter)
        Me.Controls.Add(Me.lblTotalRecords)
        Me.Controls.Add(Me.lblActivitySummary)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmActivityLog"
        Me.Text = "Activity Log Management"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
End Class
