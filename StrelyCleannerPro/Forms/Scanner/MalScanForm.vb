Imports StrelyCleannerPro.Core.Instances

Public Class MalScanForm

    Private AllSettingLoader As Boolean = False
    Private OptionName As String = "AVState"

    Private Sub MalScanForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ReadIni As String = Core.Utils.ReadIni("ScannerOption", OptionName, String.Empty)
        If ReadIni = String.Empty Then Core.Utils.WriteIni("ScannerOption", OptionName, "On")
        Guna2ToggleSwitch1.Checked = Not (ReadIni = "Off")
        Protection = Guna2ToggleSwitch1.Checked
        AllSettingLoader = True
    End Sub

#Region " GUI "

    Private Sub MalScanForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

#End Region

    Private Sub Guna2ToggleSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2ToggleSwitch1.CheckedChanged
        If AllSettingLoader = True Then

            Dim StateWriter As String = "On"

            If Guna2ToggleSwitch1.Checked = False Then StateWriter = "Off"

            Core.Utils.WriteIni("ScannerOption", OptionName, StateWriter)

            Protection = Guna2ToggleSwitch1.Checked
        End If
    End Sub

    Private Sub ScanerTabController_Click(sender As Object, e As EventArgs) Handles QuickScan.Click, FullScan.Click, CustomScan.Click
        Dim ButtonType As Guna.UI2.WinForms.Guna2Button = DirectCast(sender, Guna.UI2.WinForms.Guna2Button)
        Dim ScanType As TaskType = TaskType.Quick
        Dim MultiScannerForm As MultiScanForm = New MultiScanForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}

        Select Case ButtonType.Name
            Case QuickScan.Name : ScanType = TaskType.Quick
                MultiScannerForm.TaskT = ScanType
                CreateScan(MultiScannerForm)
            Case FullScan.Name : ScanType = TaskType.Full
                MultiScannerForm.TaskT = ScanType
                CreateScan(MultiScannerForm)
            Case CustomScan.Name
                ScanType = TaskType.Custom

                Dim FolderDialog As New FileBorserDialog.FolderBrowserDialog

                If FolderDialog.ShowDialog(Me) = DialogResult.OK Then

                    MultiScannerForm.TaskT = ScanType
                    MultiScannerForm.CustomDirScan = FolderDialog.DirectoryPath
                    CreateScan(MultiScannerForm)

                End If

        End Select

    End Sub

    Public Sub CreateScan(ByVal FormDiag As Form)
        ' FormDiag.Location = New Point(0, 0)
        Me.Controls.Add(FormDiag)
        FormDiag.BringToFront()
        FormDiag.Visible = True
        DirectCast(FormDiag, MultiScanForm).StartScanner()
    End Sub

    Public Sub RemoveScan(ByVal FormDiag As Form)
        FormDiag.Dispose()
        Me.Controls.Remove(FormDiag)
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim ExDialog As New ExcludeManager
        ExDialog.ShowDialog()
    End Sub

End Class