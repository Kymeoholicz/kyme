Imports System.Data.OleDb
Imports System.IO

Public Class frmReports
    Dim con As OleDbConnection
    Dim cmd As OleDbCommand
    Dim da As OleDbDataAdapter
    Dim dt As DataTable

    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con = DatabaseConfig.GetConnection()
        Me.Text = "Reports - Inventory Management System"
        Me.WindowState = FormWindowState.Maximized

        ' Populate report types
        cmbReportType.Items.Clear()
        cmbReportType.Items.Add("All Inventory Items")
        cmbReportType.Items.Add("Low Stock Items (≤5)")
        cmbReportType.Items.Add("Currently Issued Equipment")
        cmbReportType.Items.Add("Inventory by Category")
        cmbReportType.Items.Add("Inventory by Condition")
        cmbReportType.Items.Add("Inventory by Location")
        cmbReportType.Items.Add("Items Added This Month")
        cmbReportType.Items.Add("Items Added This Year")
        cmbReportType.Items.Add("Out of Stock Items")
        cmbReportType.Items.Add("High Stock Items (>20)")
        cmbReportType.SelectedIndex = 0

        ' Set date range to current month
        dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
        dtpToDate.Value = Date.Now
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Select Case cmbReportType.SelectedIndex
            Case 0 ' All Inventory
                GenerateAllInventoryReport()
            Case 1 ' Low Stock Items
                GenerateLowStockReport()
            Case 2 ' Currently Issued Equipment
                GenerateIssuedEquipmentReport()
            Case 3 ' Inventory by Category
                GenerateCategoryReport()
            Case 4 ' Inventory by Condition
                GenerateConditionReport()
            Case 5 ' Inventory by Location
                GenerateLocationReport()
            Case 6 ' Items Added This Month
                GenerateItemsThisMonthReport()
            Case 7 ' Items Added This Year
                GenerateItemsThisYearReport()
            Case 8 ' Out of Stock Items
                GenerateOutOfStockReport()
            Case 9 ' High Stock Items
                GenerateHighStockReport()
        End Select
    End Sub

    Private Sub GenerateAllInventoryReport()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            da = New OleDbDataAdapter("SELECT ItemID AS [ID], ItemName AS [Item Name], Category, Quantity AS [Qty], Condition, Location, Format(DateAdded, 'yyyy-MM-dd') AS [Date Added] FROM tblInventory ORDER BY ItemID DESC", con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            ' Calculate statistics
            Dim totalItems As Integer = dt.Rows.Count
            Dim totalQty As Integer = 0
            Dim categories As New HashSet(Of String)

            For Each row As DataRow In dt.Rows
                totalQty += CInt(row("Qty"))
                categories.Add(row("Category").ToString())
            Next

            lblTotalItems.Text = "Total Items: " & totalItems.ToString()
            lblTotalQuantity.Text = "Total Quantity: " & totalQty.ToString()
            lblSummary.Text = "Categories: " & categories.Count & " | Avg Qty per Item: " & If(totalItems > 0, (totalQty / totalItems).ToString("N1"), "0")

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub GenerateLowStockReport()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            Dim threshold As Integer = 5
            da = New OleDbDataAdapter("SELECT ItemID AS [ID], ItemName AS [Item Name], Category, Quantity AS [Qty], Condition, Location FROM tblInventory WHERE Quantity <= " & threshold & " ORDER BY Quantity ASC, ItemName", con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            lblTotalItems.Text = "Low Stock Items: " & dt.Rows.Count.ToString()
            lblTotalQuantity.Text = "Threshold: " & threshold.ToString() & " or below"
            lblSummary.Text = "⚠️ ACTION REQUIRED: These items need restocking!"
            lblSummary.ForeColor = Color.Red

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub GenerateIssuedEquipmentReport()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            Dim query As String = "SELECT ie.IssueID AS [ID], i.ItemName AS [Item Name], i.Category, ie.IssuedTo AS [Issued To], " &
                                "Format(ie.DateIssued, 'yyyy-MM-dd') AS [Date Issued], " &
                                "Format(ie.ReturnDate, 'yyyy-MM-dd') AS [Expected Return], " &
                                "ie.Remarks " &
                                "FROM tblIssuedEquipment ie INNER JOIN tblInventory i ON ie.ItemID = i.ItemID " &
                                "ORDER BY ie.DateIssued DESC"
            da = New OleDbDataAdapter(query, con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            ' Calculate overdue items
            Dim overdueCount As Integer = 0
            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("Expected Return")) Then
                    Dim returnDate As Date = CDate(row("Expected Return"))
                    If returnDate < Date.Now Then
                        overdueCount += 1
                    End If
                End If
            Next

            lblTotalItems.Text = "Currently Issued: " & dt.Rows.Count.ToString()
            lblTotalQuantity.Text = "Overdue Returns: " & overdueCount.ToString()
            lblSummary.Text = If(overdueCount > 0, "⚠️ " & overdueCount & " items are overdue for return!", "All equipment return dates are current")
            lblSummary.ForeColor = If(overdueCount > 0, Color.Red, Color.Green)

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub GenerateCategoryReport()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            da = New OleDbDataAdapter("SELECT Category, Count(*) AS [Item Count], Sum(Quantity) AS [Total Quantity], Avg(Quantity) AS [Avg Quantity] FROM tblInventory GROUP BY Category ORDER BY Sum(Quantity) DESC", con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            lblTotalItems.Text = "Total Categories: " & dt.Rows.Count.ToString()
            Dim totalQty As Integer = 0
            For Each row As DataRow In dt.Rows
                totalQty += CInt(row("Total Quantity"))
            Next
            lblTotalQuantity.Text = "Grand Total Quantity: " & totalQty.ToString()
            lblSummary.Text = "Summary by Category"

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub GenerateConditionReport()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            da = New OleDbDataAdapter("SELECT Condition, Count(*) AS [Item Count], Sum(Quantity) AS [Total Quantity] FROM tblInventory GROUP BY Condition ORDER BY Sum(Quantity) DESC", con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            lblTotalItems.Text = "Condition Types: " & dt.Rows.Count.ToString()
            lblTotalQuantity.Text = ""
            lblSummary.Text = "Summary by Item Condition"

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub GenerateLocationReport()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            da = New OleDbDataAdapter("SELECT Location, Count(*) AS [Item Count], Sum(Quantity) AS [Total Quantity] FROM tblInventory GROUP BY Location ORDER BY Location", con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            lblTotalItems.Text = "Total Locations: " & dt.Rows.Count.ToString()
            lblTotalQuantity.Text = ""
            lblSummary.Text = "Inventory Distribution by Location"

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub GenerateItemsThisMonthReport()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            Dim query As String = "SELECT ItemID AS [ID], ItemName AS [Item Name], Category, Quantity AS [Qty], Location, Format(DateAdded, 'yyyy-MM-dd') AS [Date Added] " &
                                "FROM tblInventory WHERE Month(DateAdded) = " & Date.Now.Month & " AND Year(DateAdded) = " & Date.Now.Year &
                                " ORDER BY DateAdded DESC"
            da = New OleDbDataAdapter(query, con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            lblTotalItems.Text = "Items Added This Month: " & dt.Rows.Count.ToString()
            lblTotalQuantity.Text = "Month: " & Date.Now.ToString("MMMM yyyy")
            lblSummary.Text = "New inventory items added this month"

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub GenerateItemsThisYearReport()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            Dim query As String = "SELECT ItemID AS [ID], ItemName AS [Item Name], Category, Quantity AS [Qty], Location, Format(DateAdded, 'yyyy-MM-dd') AS [Date Added] " &
                                "FROM tblInventory WHERE Year(DateAdded) = " & Date.Now.Year &
                                " ORDER BY DateAdded DESC"
            da = New OleDbDataAdapter(query, con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            lblTotalItems.Text = "Items Added This Year: " & dt.Rows.Count.ToString()
            lblTotalQuantity.Text = "Year: " & Date.Now.Year.ToString()
            lblSummary.Text = "New inventory items added in " & Date.Now.Year

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub GenerateOutOfStockReport()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            da = New OleDbDataAdapter("SELECT ItemID AS [ID], ItemName AS [Item Name], Category, Quantity AS [Qty], Location, Format(DateAdded, 'yyyy-MM-dd') AS [Date Added] FROM tblInventory WHERE Quantity = 0 ORDER BY ItemName", con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            lblTotalItems.Text = "Out of Stock Items: " & dt.Rows.Count.ToString()
            lblTotalQuantity.Text = "⚠️ CRITICAL"
            lblSummary.Text = "These items are completely out of stock and need immediate attention!"
            lblSummary.ForeColor = Color.Red

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub GenerateHighStockReport()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            da = New OleDbDataAdapter("SELECT ItemID AS [ID], ItemName AS [Item Name], Category, Quantity AS [Qty], Condition, Location FROM tblInventory WHERE Quantity > 20 ORDER BY Quantity DESC", con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            lblTotalItems.Text = "High Stock Items: " & dt.Rows.Count.ToString()
            lblTotalQuantity.Text = "Threshold: > 20 units"
            lblSummary.Text = "Items with high inventory levels"
            lblSummary.ForeColor = Color.Green

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If DataGridView1.Rows.Count > 0 Then
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
            saveFileDialog.FilterIndex = 1
            saveFileDialog.FileName = "InventoryReport_" & cmbReportType.Text.Replace(" ", "_") & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss")

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    If saveFileDialog.FileName.EndsWith(".csv") Then
                        ExportToCSV(saveFileDialog.FileName)
                    ElseIf saveFileDialog.FileName.EndsWith(".xlsx") Then
                        MessageBox.Show("Excel export requires Microsoft.Office.Interop.Excel." & vbCrLf & "Exporting as CSV instead.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ExportToCSV(saveFileDialog.FileName.Replace(".xlsx", ".csv"))
                    Else
                        ExportToCSV(saveFileDialog.FileName)
                    End If

                    MessageBox.Show("Report exported successfully to:" & vbCrLf & saveFileDialog.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Ask if user wants to open the file
                    If MessageBox.Show("Would you like to open the exported file?", "Open File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Process.Start(saveFileDialog.FileName)
                    End If

                Catch ex As Exception
                    MessageBox.Show("Error exporting report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Else
            MessageBox.Show("No data to export! Please generate a report first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ExportToCSV(filename As String)
        Try
            Dim writer As New StreamWriter(filename)

            ' Write report header
            writer.WriteLine("Inventory Management System - Report")
            writer.WriteLine("Report Type: " & cmbReportType.Text)
            writer.WriteLine("Generated: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            writer.WriteLine("Generated By: " & CurrentUser.FullName)
            writer.WriteLine("")

            ' Write column headers
            Dim headers As String = ""
            For i As Integer = 0 To DataGridView1.Columns.Count - 1
                headers += """" & DataGridView1.Columns(i).HeaderText & """"
                If i < DataGridView1.Columns.Count - 1 Then
                    headers += ","
                End If
            Next
            writer.WriteLine(headers)

            ' Write data rows
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    Dim line As String = ""
                    For i As Integer = 0 To DataGridView1.Columns.Count - 1
                        Dim cellValue As String = ""
                        If row.Cells(i).Value IsNot Nothing Then
                            cellValue = row.Cells(i).Value.ToString().Replace("""", """""")
                        End If
                        line += """" & cellValue & """"
                        If i < DataGridView1.Columns.Count - 1 Then
                            line += ","
                        End If
                    Next
                    writer.WriteLine(line)
                End If
            Next

            ' Write summary
            writer.WriteLine("")
            writer.WriteLine("Summary:")
            writer.WriteLine(lblTotalItems.Text)
            writer.WriteLine(lblTotalQuantity.Text)
            writer.WriteLine(lblSummary.Text)

            writer.Close()

        Catch ex As Exception
            Throw New Exception("CSV Export Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            ' Create a simple print preview
            Dim printDialog As New PrintDialog()
            If printDialog.ShowDialog() = DialogResult.OK Then
                MessageBox.Show("Print functionality requires PrintDocument component setup." & vbCrLf & vbCrLf & "Alternative: Export to CSV/Excel and print from there.", "Print Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Print error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        btnGenerate_Click(Nothing, Nothing)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        DataGridView1.DataSource = Nothing
        lblTotalItems.Text = "Total Items: 0"
        lblTotalQuantity.Text = "Total Quantity: 0"
        lblSummary.Text = "Select a report type and click Generate"
        lblSummary.ForeColor = Color.Black
    End Sub

    Private Sub btnDateFilter_Click(sender As Object, e As EventArgs) Handles btnDateFilter.Click
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            Dim query As String = "SELECT ItemID AS [ID], ItemName AS [Item Name], Category, Quantity AS [Qty], Condition, Location, Format(DateAdded, 'yyyy-MM-dd') AS [Date Added] " &
                                "FROM tblInventory WHERE DateAdded >= ? AND DateAdded <= ? ORDER BY DateAdded DESC"

            da = New OleDbDataAdapter(query, con)
            da.SelectCommand.Parameters.AddWithValue("@1", dtpFromDate.Value.Date)
            da.SelectCommand.Parameters.AddWithValue("@2", dtpToDate.Value.Date.AddDays(1).AddSeconds(-1))

            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            lblTotalItems.Text = "Items in Date Range: " & dt.Rows.Count.ToString()
            lblTotalQuantity.Text = "From " & dtpFromDate.Value.ToString("yyyy-MM-dd") & " to " & dtpToDate.Value.ToString("yyyy-MM-dd")
            lblSummary.Text = "Custom date range report"

        Catch ex As Exception
            MessageBox.Show("Error filtering by date: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub FormatDataGrid()
        ' Format the DataGridView for better appearance
        With DataGridView1
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .RowHeadersVisible = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue
            .DefaultCellStyle.SelectionBackColor = Color.DarkBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
        End With
    End Sub

    Private Sub cmbReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbReportType.SelectedIndexChanged
        ' Auto-generate report when selection changes (optional)
        ' btnGenerate_Click(Nothing, Nothing)
    End Sub
End Class