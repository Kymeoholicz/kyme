Imports System.Data.OleDb

Public Class frmInventory
    Dim con As OleDbConnection
    Dim cmd As OleDbCommand
    Dim da As OleDbDataAdapter
    Dim dt As DataTable
    Dim selectedItemID As Integer = 0

    Private Sub frmInventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con = DatabaseConfig.GetConnection()
        LoadData()
        ClearFields()
        Me.Text = "Inventory Management"
    End Sub

    '======================== LOAD DATA ========================
    Private Sub LoadData()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            da = New OleDbDataAdapter("SELECT ItemID, ItemName, Category, Quantity, [Condition], [Location], DateAdded FROM tblInventory ORDER BY ItemID DESC", con)
            dt = New DataTable
            da.Fill(dt)
            dgvInventory.DataSource = dt

            If dgvInventory.Columns.Count > 0 Then
                dgvInventory.Columns(0).Width = 60
                dgvInventory.Columns(1).Width = 150
                dgvInventory.Columns(2).Width = 100
                dgvInventory.Columns(3).Width = 80
                dgvInventory.Columns(4).Width = 100
                dgvInventory.Columns(5).Width = 150
                dgvInventory.Columns(6).Width = 120
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    '======================== CLEAR FIELDS ========================
    Private Sub ClearFields()
        txtItemName.Clear()
        txtCategory.Clear()
        txtQuantity.Clear()
        txtCondition.Clear()
        txtLocation.Clear()
        txtSearch.Clear()
        selectedItemID = 0

        btnAdd.Enabled = True
        btnUpdate.Enabled = False
        btnDelete.Enabled = False
    End Sub

    '======================== ADD ITEM ========================
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If ValidateInput() Then
            Try
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()

                cmd = New OleDbCommand("INSERT INTO tblInventory (ItemName, Category, Quantity, [Condition], [Location], DateAdded) VALUES (?,?,?,?,?,?)", con)
                cmd.Parameters.AddWithValue("@1", txtItemName.Text.Trim())
                cmd.Parameters.AddWithValue("@2", txtCategory.Text.Trim())
                cmd.Parameters.AddWithValue("@3", CInt(txtQuantity.Text))
                cmd.Parameters.AddWithValue("@4", txtCondition.Text.Trim())
                cmd.Parameters.AddWithValue("@5", txtLocation.Text.Trim())
                cmd.Parameters.AddWithValue("@6", Date.Now)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Item Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LogActivity("Add Inventory", "Added item: " & txtItemName.Text)

                LoadData()
                ClearFields()
            Catch ex As Exception
                MessageBox.Show("Error adding item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If con.State = ConnectionState.Open Then con.Close()
            End Try
        End If
    End Sub

    '======================== UPDATE ITEM ========================
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If selectedItemID > 0 AndAlso ValidateInput() Then
            Try
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()

                cmd = New OleDbCommand("UPDATE tblInventory SET ItemName=?, Category=?, Quantity=?, [Condition]=?, [Location]=? WHERE ItemID=?", con)
                cmd.Parameters.AddWithValue("@1", txtItemName.Text.Trim())
                cmd.Parameters.AddWithValue("@2", txtCategory.Text.Trim())
                cmd.Parameters.AddWithValue("@3", CInt(txtQuantity.Text))
                cmd.Parameters.AddWithValue("@4", txtCondition.Text.Trim())
                cmd.Parameters.AddWithValue("@5", txtLocation.Text.Trim())
                cmd.Parameters.AddWithValue("@6", selectedItemID)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Item Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LogActivity("Update Inventory", "Updated item ID: " & selectedItemID)

                LoadData()
                ClearFields()
            Catch ex As Exception
                MessageBox.Show("Error updating item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If con.State = ConnectionState.Open Then con.Close()
            End Try
        End If
    End Sub

    '======================== DELETE ITEM ========================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If selectedItemID > 0 Then
            If MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Try
                    If con.State = ConnectionState.Open Then con.Close()
                    con.Open()

                    cmd = New OleDbCommand("DELETE FROM tblInventory WHERE ItemID=?", con)
                    cmd.Parameters.AddWithValue("@1", selectedItemID)
                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Item Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LogActivity("Delete Inventory", "Deleted item ID: " & selectedItemID)

                    LoadData()
                    ClearFields()
                Catch ex As Exception
                    MessageBox.Show("Error deleting item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If con.State = ConnectionState.Open Then con.Close()
                End Try
            End If
        End If
    End Sub

    '======================== SEARCH ITEM ========================
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtSearch.Text.Trim() <> "" Then
            Try
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()

                da = New OleDbDataAdapter("SELECT * FROM tblInventory WHERE ItemName LIKE ? OR Category LIKE ? OR [Location] LIKE ?", con)
                Dim searchTerm As String = "%" & txtSearch.Text.Trim() & "%"
                da.SelectCommand.Parameters.AddWithValue("@1", searchTerm)
                da.SelectCommand.Parameters.AddWithValue("@2", searchTerm)
                da.SelectCommand.Parameters.AddWithValue("@3", searchTerm)

                dt = New DataTable
                da.Fill(dt)
                dgvInventory.DataSource = dt
            Catch ex As Exception
                MessageBox.Show("Error searching: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If con.State = ConnectionState.Open Then con.Close()
            End Try
        Else
            LoadData()
        End If
    End Sub

    '======================== REFRESH ========================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadData()
        ClearFields()
    End Sub

    '======================== CLEAR BUTTON ========================
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    '======================== DATAGRID CLICK ========================
    Private Sub dgvInventory_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInventory.CellClick
        If e.RowIndex >= 0 Then
            Try
                Dim row As DataGridViewRow = dgvInventory.Rows(e.RowIndex)
                selectedItemID = CInt(row.Cells(0).Value)
                txtItemName.Text = row.Cells(1).Value.ToString()
                txtCategory.Text = row.Cells(2).Value.ToString()
                txtQuantity.Text = row.Cells(3).Value.ToString()
                txtCondition.Text = row.Cells(4).Value.ToString()
                txtLocation.Text = row.Cells(5).Value.ToString()

                btnAdd.Enabled = False
                btnUpdate.Enabled = True
                btnDelete.Enabled = True
            Catch ex As Exception
                MessageBox.Show("Error selecting row: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    '======================== VALIDATION ========================
    Private Function ValidateInput() As Boolean
        If String.IsNullOrWhiteSpace(txtItemName.Text) Then
            MessageBox.Show("Please enter item name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtItemName.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtCategory.Text) Then
            MessageBox.Show("Please enter category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCategory.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtQuantity.Text) OrElse Not IsNumeric(txtQuantity.Text) Then
            MessageBox.Show("Please enter valid quantity.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtQuantity.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtCondition.Text) Then
            MessageBox.Show("Please enter condition.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCondition.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtLocation.Text) Then
            MessageBox.Show("Please enter location.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtLocation.Focus()
            Return False
        End If

        Return True
    End Function

    '======================== ACTIVITY LOG ========================
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
            ' Ignore logging errors
        End Try
    End Sub

End Class
