Public Class ExcludeManager
    Private Sub ExcludeManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetTheme()
    End Sub

    Private Sub SetTheme()
        Try
            Dim j As MARGINS = New MARGINS()
            GlassTheme.SetAero7(Me.Handle, j)
            GlassTheme.SetAero10(Me.Handle)
        Catch ex As Exception
            Me.BackColor = Core.Instances.BackColor
        End Try
    End Sub

    Public Sub ListEx()
        Dim AllList As List(Of Core.ExType) = Core.ExclusionManager.GetAll
        If AllList IsNot Nothing Then
            For Each ItemEx As Core.ExType In AllList
                ListBox1.Items.Add(ItemEx)
            Next
        End If
        If Not ListBox1.Items.Count = 0 Then ListBox1.SelectedIndex = 0
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            Dim SelectedItem As String = ListBox1.Items(ListBox1.SelectedIndex)
            Dim tasks As Core.ExType = Core.ExclusionManager.GetAll().Find(Function(x) x.FilePath = SelectedItem)

            If tasks IsNot Nothing Then
                Core.ExclusionManager.RemoveByFile(tasks)
            End If

        Catch ex As Exception
            'Core.ErrorLogger.LogError("Exclusion.Deleter", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ViewActionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewActionToolStripMenuItem.Click
        Try
            Dim SelectedItem As String = ListBox1.Items(ListBox1.SelectedIndex)
            Dim tasks As Core.ExType = Core.ExclusionManager.GetAll().Find(Function(x) x.FilePath = SelectedItem)

            If tasks IsNot Nothing Then
                Dim ActionStr As String = "Solve"
                If tasks.IsSolve = True Then ActionStr = "Solve" Else ActionStr = "Leave"
                Dim Message As String = "File: " & tasks.FilePath & vbNewLine & "Action: " & ActionStr
                MsgBox(Message)
            End If

        Catch ex As Exception
            'Core.ErrorLogger.LogError("Exclusion.ViewAction", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ChangeActionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeActionToolStripMenuItem.Click
        Try
            Dim SelectedItem As String = ListBox1.Items(ListBox1.SelectedIndex)
            Dim tasks As Core.ExType = Core.ExclusionManager.GetAll().Find(Function(x) x.FilePath = SelectedItem)

            If tasks IsNot Nothing Then
                Core.ExclusionManager.RemoveByFile(tasks)
                System.Threading.Thread.Sleep(500)
                tasks.IsSolve = Not tasks.IsSolve
                Core.ExclusionManager.Add(tasks)
            End If

        Catch ex As Exception
            'Core.ErrorLogger.LogError("Exclusion.ChangeAction", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

End Class