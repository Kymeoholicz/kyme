Imports System.Data.OleDb

Public Class frmIssuedEquipment
    Dim con As OleDbConnection
    Dim cmd As OleDbCommand
    Dim da As OleDbDataAdapter
    Dim dt As DataTable
    Dim selectedIssueID As Integer = 0

    Private Sub frmIssuedEquipment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con = DatabaseConfig.GetConnection()
        Me.Text = "Issued Equipment Management"
        Me.WindowState = FormWindowState.Maximized

        LoadIssuedData()
        LoadItemsComboBox()
        ClearFields()
        dtpDateIssued.Value = Date.Now
        dtpReturnDate.Value = Date.Now.AddDays(7)
    End Sub

    Private Sub LoadItemsComboBox()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            ' Load only items with available quantity
            Dim query As String = "SELECT ItemID, ItemName, Quantity FROM tblInventory WHERE Quantity > 0 ORDER BY ItemName"
            da = New OleDbDataAdapter(query, con)
            dt = New DataTable
            da.Fill(dt)

            ' Bind to ComboBox
            cmbItemID.DataSource = dt
            cmbItemID.DisplayMember = "ItemName"
            cmbItemID.ValueMember = "ItemID"

            If dt.Rows.Count > 0 Then
                cmbItemID.SelectedIndex = 0
            Else
                MessageBox.Show("No items with available stock found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub LoadIssuedData()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            ' Join with inventory to show item names
            Dim query As String = "SELECT ie.IssueID AS [ID], " &
                                "i.ItemName AS [Item Name], " &
                                "i.Category, " &
                                "ie.IssuedTo AS [Issued To], " &
                                "Format(ie.DateIssued, 'yyyy-MM-dd') AS [Date Issued], " &
                                "Format(ie.ReturnDate, 'yyyy-MM-dd') AS [Expected Return], " &
                                "ie.Remarks " &
                                "FROM tblIssuedEquipment ie " &
                                "INNER JOIN tblInventory i ON ie.ItemID = i.ItemID " &
                                "ORDER BY ie.IssueID DESC"

            da = New OleDbDataAdapter(query, con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            ' Format DataGridView
            FormatDataGrid()

            ' Update stats
            lblTotalIssued.Text = "Total Issued: " & dt.Rows.Count.ToString()

            ' Calculate overdue items
            Dim overdueCount As Integer = 0
            For Each row As DataRow In dt.Rows
                Dim returnDate As Date = CDate(row("Expected Return"))
                If returnDate < Date.Now Then
                    overdueCount += 1
                End If
            Next
            lblOverdue.Text = "Overdue: " & overdueCount.ToString()
            lblOverdue.ForeColor = If(overdueCount > 0, Color.Red, Color.Green)

        Catch ex As Exception
            MessageBox.Show("Error loading issued data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub FormatDataGrid()
        With DataGridView1
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .RowHeadersVisible = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray
            .DefaultCellStyle.SelectionBackColor = Color.DarkBlue
            .DefaultCellStyle.SelectionForeColor = Color.White

            If .Columns.Count > 0 Then
                .Columns(0).Width = 50   ' ID
                .Columns(1).Width = 150  ' Item Name
                .Columns(2).Width = 100  ' Category
                .Columns(3).Width = 150  ' Issued To
                .Columns(4).Width = 100  ' Date Issued
                .Columns(5).Width = 100  ' Expected Return
                .Columns(6).Width = 200  ' Remarks
            End If
        End With
    End Sub

    Private Sub ClearFields()
        If cmbItemID.Items.Count > 0 Then
            cmbItemID.SelectedIndex = 0
        End If
        txtIssuedTo.Clear()
        txtRemarks.Clear()
        dtpDateIssued.Value = Date.Now
        dtpReturnDate.Value = Date.Now.AddDays(7)
        selectedIssueID = 0
        btnIssue.Enabled = True
        btnReturn.Enabled = False
        lblAvailableQty.Text = "Available: 0"
        UpdateAvailableQuantity()
    End Sub

    Private Sub btnIssue_Click(sender As Object, e As EventArgs) Handles btnIssue.Click
        If ValidateInput() Then
            Try
                Dim itemID As Integer = CInt(cmbItemID.SelectedValue)
                Dim availableQty As Integer = GetAvailableQuantity(itemID)

                If availableQty <= 0 Then
                    MessageBox.Show("No stock available for this item!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If con.State = ConnectionState.Open Then con.Close()
                con.Open()

                ' Start transaction
                Dim transaction As OleDbTransaction = con.BeginTransaction()

                Try
                    ' Insert into issued equipment
                    cmd = New OleDbCommand("INSERT INTO tblIssuedEquipment (ItemID, IssuedTo, DateIssued, ReturnDate, Remarks) VALUES (?,?,?,?,?)", con, transaction)
                    cmd.Parameters.AddWithValue("@1", itemID)
                    cmd.Parameters.AddWithValue("@2", txtIssuedTo.Text.Trim())
                    cmd.Parameters.AddWithValue("@3", dtpDateIssued.Value)
                    cmd.Parameters.AddWithValue("@4", dtpReturnDate.Value)
                    cmd.Parameters.AddWithValue("@5", txtRemarks.Text.Trim())
                    cmd.ExecuteNonQuery()

                    ' Decrease quantity in inventory
                    cmd = New OleDbCommand("UPDATE tblInventory SET Quantity = Quantity - 1 WHERE ItemID = ?", con, transaction)
                    cmd.Parameters.AddWithValue("@1", itemID)
                    cmd.ExecuteNonQuery()

                    ' Commit transaction
                    transaction.Commit()

                    MessageBox.Show("Equipment Issued Successfully!" & vbCrLf & vbCrLf &
                                  "Item: " & cmbItemID.Text & vbCrLf &
                                  "Issued To: " & txtIssuedTo.Text & vbCrLf &
                                  "Return By: " & dtpReturnDate.Value.ToString("yyyy-MM-dd"),
                                  "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Log activity
                    LogActivity("Issue Equipment", "Issued " & cmbItemID.Text & " to " & txtIssuedTo.Text)

                    LoadIssuedData()
                    LoadItemsComboBox()
                    ClearFields()

                Catch ex As Exception
                    transaction.Rollback()
                    Throw
                End Try

            Catch ex As Exception
                MessageBox.Show("Error issuing equipment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If con.State = ConnectionState.Open Then con.Close()
            End Try
        End If
    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        If selectedIssueID > 0 Then
            If MessageBox.Show("Mark this equipment as returned?", "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Try
                    If con.State = ConnectionState.Open Then con.Close()
                    con.Open()

                    ' Start transaction
                    Dim transaction As OleDbTransaction = con.BeginTransaction()

                    Try
                        ' Get ItemID and details before deleting
                        cmd = New OleDbCommand("SELECT ie.ItemID, i.ItemName, ie.IssuedTo FROM tblIssuedEquipment ie INNER JOIN tblInventory i ON ie.ItemID = i.ItemID WHERE ie.IssueID = ?", con, transaction)
                        cmd.Parameters.AddWithValue("@1", selectedIssueID)
                        Dim reader As OleDbDataReader = cmd.ExecuteReader()

                        Dim itemID As Integer = 0
                        Dim itemName As String = ""
                        Dim issuedTo As String = ""

                        If reader.Read() Then
                            itemID = CInt(reader("ItemID"))
                            itemName = reader("ItemName").ToString()
                            issuedTo = reader("IssuedTo").ToString()
                        End If
                        reader.Close()

                        ' Delete from issued equipment
                        cmd = New OleDbCommand("DELETE FROM tblIssuedEquipment WHERE IssueID = ?", con, transaction)
                        cmd.Parameters.AddWithValue("@1", selectedIssueID)
                        cmd.ExecuteNonQuery()

                        ' Increase quantity in inventory
                        cmd = New OleDbCommand("UPDATE tblInventory SET Quantity = Quantity + 1 WHERE ItemID = ?", con, transaction)
                        cmd.Parameters.AddWithValue("@1", itemID)
                        cmd.ExecuteNonQuery()

                        ' Commit transaction
                        transaction.Commit()

                        MessageBox.Show("Equipment Returned Successfully!" & vbCrLf & vbCrLf &
                                      "Item: " & itemName & vbCrLf &
                                      "From: " & issuedTo,
                                      "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' Log activity
                        LogActivity("Return Equipment", "Returned " & itemName & " from " & issuedTo)

                        LoadIssuedData()
                        LoadItemsComboBox()
                        ClearFields()

                    Catch ex As Exception
                        transaction.Rollback()
                        Throw
                    End Try

                Catch ex As Exception
                    MessageBox.Show("Error returning equipment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If con.State = ConnectionState.Open Then con.Close()
                End Try
            End If
        Else
            MessageBox.Show("Please select an issued item from the grid to return.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Try
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                selectedIssueID = CInt(row.Cells(0).Value)

                ' Highlight selected row
                DataGridView1.ClearSelection()
                row.Selected = True

                btnIssue.Enabled = False
                btnReturn.Enabled = True

                ' Display info
                lblSelectedItem.Text = "Selected: " & row.Cells(1).Value.ToString() & " (Issued to: " & row.Cells(3).Value.ToString() & ")"

            Catch ex As Exception
                MessageBox.Show("Error selecting row: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Function ValidateInput() As Boolean
        If cmbItemID.SelectedIndex = -1 Then
            MessageBox.Show("Please select an item.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbItemID.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtIssuedTo.Text) Then
            MessageBox.Show("Please enter who the equipment is issued to.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtIssuedTo.Focus()
            Return False
        End If

        If dtpReturnDate.Value < dtpDateIssued.Value Then
            MessageBox.Show("Return date cannot be before issue date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dtpReturnDate.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function GetAvailableQuantity(itemID As Integer) As Integer
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            cmd = New OleDbCommand("SELECT Quantity FROM tblInventory WHERE ItemID = ?", con)
            cmd.Parameters.AddWithValue("@1", itemID)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                Return CInt(result)
            End If
        Catch ex As Exception
            MessageBox.Show("Error getting quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
        Return 0
    End Function

    Private Sub cmbItemID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbItemID.SelectedIndexChanged
        UpdateAvailableQuantity()
    End Sub

    Private Sub UpdateAvailableQuantity()
        Try
            If cmbItemID.SelectedIndex >= 0 AndAlso cmbItemID.SelectedValue IsNot Nothing Then
                Dim itemID As Integer = CInt(cmbItemID.SelectedValue)
                Dim qty As Integer = GetAvailableQuantity(itemID)
                lblAvailableQty.Text = "Available: " & qty.ToString()

                If qty <= 0 Then
                    lblAvailableQty.ForeColor = Color.Red
                    lblAvailableQty.Font = New Font(lblAvailableQty.Font, FontStyle.Bold)
                ElseIf qty <= 5 Then
                    lblAvailableQty.ForeColor = Color.Orange
                    lblAvailableQty.Font = New Font(lblAvailableQty.Font, FontStyle.Bold)
                Else
                    lblAvailableQty.ForeColor = Color.Green
                    lblAvailableQty.Font = New Font(lblAvailableQty.Font, FontStyle.Regular)
                End If
            End If
        Catch ex As Exception
            ' Ignore errors during initialization
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
        DataGridView1.ClearSelection()
        lblSelectedItem.Text = "Selected: None"
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadIssuedData()
        LoadItemsComboBox()
        ClearFields()
        lblSelectedItem.Text = "Selected: None"
    End Sub

    Private Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        LoadIssuedData()
    End Sub

    Private Sub btnViewOverdue_Click(sender As Object, e As EventArgs) Handles btnViewOverdue.Click
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            Dim query As String = "SELECT ie.IssueID AS [ID], " &
                                "i.ItemName AS [Item Name], " &
                                "i.Category, " &
                                "ie.IssuedTo AS [Issued To], " &
                                "Format(ie.DateIssued, 'yyyy-MM-dd') AS [Date Issued], " &
                                "Format(ie.ReturnDate, 'yyyy-MM-dd') AS [Expected Return], " &
                                "ie.Remarks " &
                                "FROM tblIssuedEquipment ie " &
                                "INNER JOIN tblInventory i ON ie.ItemID = i.ItemID " &
                                "WHERE ie.ReturnDate < Date() " &
                                "ORDER BY ie.ReturnDate ASC"

            da = New OleDbDataAdapter(query, con)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            FormatDataGrid()

            lblTotalIssued.Text = "Overdue Items: " & dt.Rows.Count.ToString()

            If dt.Rows.Count > 0 Then
                MessageBox.Show("Found " & dt.Rows.Count & " overdue item(s)!", "Overdue Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show("No overdue items found!", "All Clear", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading overdue items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If Not String.IsNullOrWhiteSpace(txtSearch.Text) Then
            Try
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()

                Dim searchTerm As String = "%" & txtSearch.Text.Trim() & "%"
                Dim query As String = "SELECT ie.IssueID AS [ID], " &
                                    "i.ItemName AS [Item Name], " &
                                    "i.Category, " &
                                    "ie.IssuedTo AS [Issued To], " &
                                    "Format(ie.DateIssued, 'yyyy-MM-dd') AS [Date Issued], " &
                                    "Format(ie.ReturnDate, 'yyyy-MM-dd') AS [Expected Return], " &
                                    "ie.Remarks " &
                                    "FROM tblIssuedEquipment ie " &
                                    "INNER JOIN tblInventory i ON ie.ItemID = i.ItemID " &
                                    "WHERE i.ItemName LIKE ? OR ie.IssuedTo LIKE ? OR i.Category LIKE ? " &
                                    "ORDER BY ie.IssueID DESC"

                da = New OleDbDataAdapter(query, con)
                da.SelectCommand.Parameters.AddWithValue("@1", searchTerm)
                da.SelectCommand.Parameters.AddWithValue("@2", searchTerm)
                da.SelectCommand.Parameters.AddWithValue("@3", searchTerm)

                dt = New DataTable
                da.Fill(dt)
                DataGridView1.DataSource = dt

                FormatDataGrid()

                lblTotalIssued.Text = "Search Results: " & dt.Rows.Count.ToString()

            Catch ex As Exception
                MessageBox.Show("Error searching: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If con.State = ConnectionState.Open Then con.Close()
            End Try
        Else
            LoadIssuedData()
        End If
    End Sub

    Private Sub LogActivity(activityType As String, description As String)
        Try
            Dim logCon As OleDbConnection = DatabaseConfig.GetConnection()
            logCon.Open()
            Dim logCmd As New OleDbCommand("INSERT INTO tblActivityLog (UserID, ActivityType, Description, ActivityDate) VALUES (?,?,?,?)", logCon)
            logCmd.Parameters.AddWithValue("@1", CurrentUser.UserID)
            logCmd.Parameters.AddWithValue("@2", activityType)
            logCmd.Parameters.AddWithValue("@3", description)
            logCmd.Parameters.AddWithValue("@4", Date.Now)
            logCmd.ExecuteNonQuery()
            logCon.Close()
        Catch ex As Exception
            ' Silent fail for logging
        End Try
    End Sub
End Class