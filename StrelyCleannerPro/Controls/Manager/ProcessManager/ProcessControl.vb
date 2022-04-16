Imports XylonV2

Public Class ProcessControl

#Region " Properties "

    Private _Process As Process = Nothing
    Public Property ProcesEx As Process
        Get
            Return _Process
        End Get
        Set(value As Process)
            _Process = value
        End Set
    End Property

    Private _ProcPath As String = String.Empty
    Public Property ProcPath As String
        Get
            Return _ProcPath
        End Get
        Set(value As String)
            _ProcPath = value
        End Set
    End Property

    Private _Comandline As String = String.Empty
    Public Property Comandline As String
        Get
            Return _Comandline
        End Get
        Set(value As String)
            _Comandline = value
        End Set
    End Property

#End Region

    Public Property CallParent As Boolean = False
    Public Property is64 As Boolean = False
    Public Property NModules As List(Of String) = Nothing
    Public Property AutoSelect As Boolean = False

    Private Sub ProcessControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _Process.ProcessName.Length > 25 Then
            GunaAdvenceButton2.Text = _Process.ProcessName.Substring(0, 22) & "..."

        Else
            GunaAdvenceButton2.Text = _Process.ProcessName
        End If
        BoosterToolTip1.SetToolTip(GunaAdvenceButton2, _Process.ProcessName & vbNewLine & "ID: " & _Process.Id & vbNewLine)

        Dim MiscGet As String = "Comandline: " & Comandline & vbNewLine

        If Engine.PE.Binary.PEChecker.IsNetAssembly(_ProcPath) Then

            MiscGet += "Process Is Managed (.Net)" & vbNewLine

        End If

        BoosterToolTip1.SetToolTip(GunaAdvenceButton2, BoosterToolTip1.GetToolTip(GunaAdvenceButton2).ToString & MiscGet)
        If Not _ProcPath = String.Empty Then
            is64 = Core.Utils.Is64BitPE(_ProcPath)

            Try
                Dim ico As Icon = Icon.ExtractAssociatedIcon(_ProcPath)
                If ico IsNot Nothing Then GunaAdvenceButton2.CheckedState.Image = ico.ToBitmap : GunaAdvenceButton2.Image = ico.ToBitmap
            Catch ex As Exception

            End Try

        End If

        GunaAdvenceButton2.Checked = CallParent

        If NModules Is Nothing Then
            NModules = Core.Utils.GetNativeProcessModules(_Process)
        End If

        If AutoSelect = True Then
            GunaAdvenceButton2.Checked = True
        End If

    End Sub

    Private Sub GunaAdvenceButton2_CheckedChanged(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.CheckedChanged
        If GunaAdvenceButton2.Checked = True Then
            DirectCast(Me.ParentForm, ProcessManagerForm).SetProcess(Me)
        End If
    End Sub

    Public Function CheckProcess() As Boolean
        Try
            Dim processes As Process = Process.GetProcessById(_Process.Id)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub OpenFileLocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileLocationToolStripMenuItem.Click
        Process.Start("explorer.exe", "/select, " & """" & _ProcPath & """")
    End Sub

    Private Sub TerminateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TerminateToolStripMenuItem.Click
        Try

            _Process.Kill()
            CheckExits()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub CheckExits()
        If CheckProcess() = False Then

            GunaAdvenceButton2.CheckedState.BorderColor = Color.Red
            GunaAdvenceButton2.ForeColor = Color.Gray
            GunaAdvenceButton2.CustomBorderColor = Color.Gray
            GunaAdvenceButton2.Image = Core.Utils.GrayScale_Image(GunaAdvenceButton2.Image, Core.Utils.GrayScale.Dark_Gray)
            GunaAdvenceButton2.Checked = False

        End If
    End Sub

End Class
