Imports System.Threading
Imports StrelyCleannerPro.Core.Instances
Imports XylonV2
Imports XylonV2.Engine.External.WindowsDefender
Imports XylonV2.StartupManager
Imports XylonV2.StartupManager.Models
Imports XylonV2.StartupManager.Services.Directories
Imports XylonV2.StartupManager.Services.Registries

Public Class MultiScanForm

#Region " Properties "

    Public Property TaskT As TaskType = TaskType.Quick
    Public Property CustomDirScan As String = String.Empty

#End Region


#Region " Declare "

    Private ProcessListScrool As Core.ScrollManager
    Dim cts As New CancellationTokenSource
    Dim token As CancellationToken = cts.Token

#End Region

    Private Sub MultiScanForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        StartUI()
    End Sub


    Private Sub StartUI()
        ProcessListScrool = New Core.ScrollManager(PanelContainer, {Guna2VScrollBar1}, True)
        Guna2ProgressIndicator1.Start()

        Select Case TaskT
            Case TaskType.Quick : Guna2HtmlLabel2.Text = "Quick Scan"
            Case TaskType.Full : Guna2HtmlLabel2.Text = "Full Scan"
            Case TaskType.Custom : Guna2HtmlLabel2.Text = "Custom Scan"
        End Select

        Guna2HtmlLabel3.Text = "Calculating..."
        Guna2HtmlLabel6.Text = "Analyzing..."

    End Sub

#Region " Public Methods "

    Private IsScaning As Boolean = False
    Private IntCount As Integer = 0


    Public Sub StartScanner()
        Execution_Start()

        Dim Asynctask As Task = Task.Factory.StartNew(Sub() ScanTask(token), token, TaskCreationOptions.PreferFairness)

        Asynctask.GetAwaiter.OnCompleted(Sub()

                                             Guna2ProgressBar1.Maximum = 100
                                             Guna2ProgressBar1.Value = 100
                                             Guna2HtmlLabel8.Text = ""
                                             Dim TimeElapsed As String = Execution_Watcher.Elapsed.Hours & "h:" & Execution_Watcher.Elapsed.Minutes & "m:" & Execution_Watcher.Elapsed.Seconds & "s:" & Execution_Watcher.Elapsed.Milliseconds & "ms"
                                             Guna2HtmlLabel3.Text = TimeElapsed
                                             Guna2HtmlLabel6.Text = PanelContainer.Controls.Count
                                             Guna2Button1.FillColor = Color.Transparent
                                             Guna2ProgressIndicator1.Stop()
                                             IsScaning = False

                                             Guna2Button1.FillColor = Color.FromArgb(18, 23, 29)

                                             If PanelContainer.Controls.Count = 0 Then
                                                 Guna2HtmlLabel4.Text = "Congratulations, no Threats were found. Files Scanned : " & IntCount
                                                 Guna2Button1.Text = "OK"
                                             Else
                                                 Guna2HtmlLabel4.Text = "Threats have been found, Action is recommended."
                                             End If

                                             '    Guna2Button2.Visible = False
                                             Execution_End()
                                         End Sub)
    End Sub

    Private Sub ScanTask(ByVal token As CancellationToken)

        IsScaning = True

        Dim FilesEX As New List(Of String)
        Dim ScanStartup As Boolean = True
        Dim ProcessScan As Boolean = True
        Dim ExtensionScan As Boolean = True

        Select Case TaskT

            Case TaskType.Quick
                ScanStartup = True
                ProcessScan = True
                ExtensionScan = True

                FilesEX.AddRange(FileDirSearcher.GetFilePaths(Core.Paths.CurrentUserStartup, IO.SearchOption.AllDirectories).ToList)
                FilesEX.AddRange(FileDirSearcher.GetFilePaths(Core.Paths.AllUsersStartup, IO.SearchOption.AllDirectories).ToList)

            Case TaskType.Full

                ScanStartup = True
                ProcessScan = True
                ExtensionScan = True

                Dim PathsEx As String() = {Core.Paths.CurrentUserStartup, Core.Paths.AllUsersStartup, Core.Paths.Desktop, Core.Paths.Downloads,
                Core.Paths.AppData, Core.Paths.TempPath, Core.Paths.Document, Core.Paths.Music, Core.Paths.Pictures}


                For Each Filesx As String In PathsEx

                    Dim FilesFinder As IEnumerable(Of IO.FileInfo) = FileDirSearcher.GetFiles(dirPath:=Filesx,
                                                                 searchOption:=IO.SearchOption.AllDirectories,
                                                                 fileNamePatterns:={"*"},
                                                                 fileExtPatterns:={"*.dll", "*.exe", "*.sfx", "*.bat", "*.bat", "*.cmd", "*.vbs",
                                                                 "*.wsf", "*.js", "*.ps1", "*.hta", "*.lnk", "*.doc", "*.docx", "*.odt", "*.pdf",
                                                                 "*.rtf", "*.ppt", "*.pptx", "*.xls"},
                                                                 ignoreCase:=True,
                                                                 throwOnError:=False)

                    FilesEX.AddRange(FilesFinder.[Select](Function(X) X.FullName).ToList())

                Next

            Case TaskType.Custom
                ScanStartup = False
                ProcessScan = False
                ExtensionScan = False

                If Core.Utils.IsFolder(CustomDirScan) = True Then
                    FilesEX.AddRange(FileDirSearcher.GetFilePaths(CustomDirScan, IO.SearchOption.AllDirectories).ToList)
                Else
                    FilesEX.Add(CustomDirScan)
                End If

        End Select

        If ScanStartup = True Then

            Dim RegistryService As RegistryService = New RegistryService()
            Dim DirectoryService As DirectoryService = New DirectoryService()

            Dim startupPrograms As List(Of Models.StartupList) = New List(Of Models.StartupList)

            Dim startupStates As IEnumerable(Of StartupState) = RegistryService.GetStartupProgramStates()
            Dim registryStartups = RegistryService.GetStartupPrograms(startupStates)

            If registryStartups IsNot Nothing Then
                startupPrograms.AddRange(registryStartups)
            End If

            Dim shellStartups = DirectoryService.GetStartupPrograms(startupStates)
            If shellStartups IsNot Nothing Then
                startupPrograms.AddRange(shellStartups)
            End If


            For Each ItemStartup As Models.StartupList In startupPrograms

                Dim FileName As String = ItemStartup.GetParsedPath()

                FilesEX.Add(FileName)

            Next

        End If

        If ProcessScan = True Then

            For Each ProcessEx As Process In Process.GetProcesses
                Try

                    Dim ProcInfo As XylonV2.Core.Engine.WMI.Win32_Process = XylonV2.Core.Engine.WMI.Win32_Process.GetProcesses(ProcessEx.Id)

                    If ProcInfo IsNot Nothing Then

                        If Core.Utils.IsSystem(ProcInfo.ExecutablePath) = False Then FilesEX.Add(ProcInfo.ExecutablePath)

                    End If

                Catch ex As Exception
                    'Core.ErrorLogger.LogError("MiltiScanner.ProcessGet", ex.Message, ex.StackTrace)
                End Try
            Next

        End If


        Dim FilterEx As New List(Of String)

        Try

            FilterEx.AddRange(FilesEX.Distinct.ToList)
            IntCount = FilterEx.Count

            Me.BeginInvoke(Sub()
                               Guna2ProgressBar1.Maximum = IntCount
                           End Sub)

        Catch ex As Exception
            'Core.ErrorLogger.LogError("MultiScan.ListForm", ex.Message, ex.StackTrace)
        End Try

        For Each FileAn As String In FilterEx

            Try
                token.ThrowIfCancellationRequested()
            Catch ex As Exception
                Exit Sub
            End Try

            Try

                Guna2ProgressBar1.Value += 1

                Me.BeginInvoke(Sub()
                                   Guna2HtmlLabel8.Text = FileAn
                               End Sub)

                If Not FileAn = Application.ExecutablePath Then

                    If Core.Utils.IsSystem(FileAn) = False AndAlso Core.Utils.IsPotencialRiskFormat(FileAn) = True Then

                        Dim scanner As WindowsDefenderScanner = New WindowsDefenderScanner(Core.Paths.DefenderExeLocation)
                        Dim result As Engine.External.Core.ScanResult = scanner.Scan(FileAn)

                        Dim ScanResult As Engine.External.Core.DetectionResult = Nothing

                        If result = Engine.External.Core.ScanResult.ThreatFound Then

                            Dim DetectResult As New Engine.External.Core.DetectionResult(Engine.External.Core.ScanResult.ThreatFound, scanner.ResultParsed, "Malware Detected")
                            ScanResult = DetectResult

                        Else

                            ScanResult = Engine.PE.Analysis.StringScan(FileAn)

                        End If

                        If Not ScanResult.Result = Engine.External.Core.ScanResult.ThreatFound Then
                            If Engine.PE.Binary.PEChecker.IsNetAssembly(FileAn) = True Then
                                ScanResult = Engine.PE.Net.Core.NetAnalysis.NetScanner(FileAn)
                            End If
                        End If

                        If Not ScanResult.Result = Engine.External.Core.ScanResult.ThreatFound Then
                            If Core.Utils.IsStartup((FileAn)) = True Then

                                Dim DetectResult As New Engine.External.Core.DetectionResult(Engine.External.Core.ScanResult.ThreatFound, "Suspicius.Startup", "Windows Startup Item")
                                ScanResult = DetectResult

                            End If
                        End If

                        If ScanResult.Result = Engine.External.Core.ScanResult.ThreatFound Then

                            If Not LCase(IO.Path.GetFileName(FileAn)) = "desktop.ini" Then
                                If String.IsNullOrEmpty(ScanResult.Signature) = False Then
                                    Alert(FileAn, ScanResult, Core.Instances.WarnLevel.Danger)
                                End If
                            Else
                                Dim FileAParse As New IO.FileInfo(FileAn)
                                FileAParse.Attributes = IO.FileAttributes.Hidden Or IO.FileAttributes.System
                            End If


                        End If

                    End If

                End If

            Catch ex As Exception
                'Core.ErrorLogger.LogError("MultiScan.Main", ex.Message, ex.StackTrace)
            End Try

            System.Threading.Thread.Sleep(500)

        Next

        Me.BeginInvoke(Sub()
                           Guna2ProgressBar1.Maximum = 100
                           Guna2ProgressBar1.ProgressColor = Color.SpringGreen
                           Guna2ProgressBar1.ProgressColor2 = Color.SpringGreen
                           Guna2ProgressBar1.Value = 50
                       End Sub)

        If ExtensionScan = True Then

            Dim ChromeExtensionManager As XylonV2.Core.Engine.WebBrowser.Chrome = New XylonV2.Core.Engine.WebBrowser.Chrome

            Dim GetExtensions As List(Of XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension) = ChromeExtensionManager.Extensions()

            For Each ChromeExtension As XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension In GetExtensions

                If ChromeExtension.LoadState = XylonV2.Core.Engine.WebBrowser.Chrome.StateLoaded.Loaded Then

                    Dim GScanner As XylonV2.Core.Engine.WebBrowser.Chrome.ChromeScanner = New XylonV2.Core.Engine.WebBrowser.Chrome.ChromeScanner
                    Dim ResultGScan As Boolean = GScanner.IsSuspiciusExtension(ChromeExtension)
                    If ResultGScan = True Then
                        Dim DetectResult As New Engine.External.Core.DetectionResult(Engine.External.Core.ScanResult.ThreatFound, GScanner.SuspiciusInfo, "Browser Extension")
                        Alert(ChromeExtension.FullPath, DetectResult, Core.Instances.WarnLevel.Danger, Nothing, ChromeExtension)
                    End If

                End If

            Next

        End If


        Me.BeginInvoke(Sub()
                           Guna2ProgressBar1.Value = Guna2ProgressBar1.Maximum
                       End Sub)
    End Sub


#End Region

#Region " Code Execution Time "

    ' [ Code Execution Time ]
    '
    ' // By Elektro H@cker
    '
    ' Examples :
    ' Execution_Start() : Threading.Thread.Sleep(500) : Execution_End()

    Dim Execution_Watcher As New Stopwatch

    Private Sub Execution_Start()
        If Execution_Watcher.IsRunning Then Execution_Watcher.Restart()
        Execution_Watcher.Start()
    End Sub

    Private Sub Execution_End()
        If Execution_Watcher.IsRunning Then
            Execution_Watcher.Reset()
        End If
    End Sub

#End Region


#Region " Listerner "

    Private ListenerProcess As New ControlLister With {.OrientationControls = Orientation.Vertical, .Margen = New Point(20, 2)}
    Dim ID As Integer = 0

    Private Alerts As New List(Of AV_Item)

    Public Sub Alert(ByVal FilePath As String, ByVal InfoEx As XylonV2.Engine.External.Core.DetectionResult, Optional ByVal Level As Core.Instances.WarnLevel = Core.Instances.WarnLevel.Danger, Optional AcEx As Action = Nothing, Optional ExtensionBrowser As Object = Nothing)

        Me.BeginInvoke(Sub()

                           Dim DiagAlert As New AV_Item
                           DiagAlert.Extension = ExtensionBrowser
                           DiagAlert.Name = ID
                           DiagAlert.Info = InfoEx
                           DiagAlert.FilePath = FilePath
                           DiagAlert.AlertLevel = Level
                           DiagAlert.AlertAction = AcEx
                           DiagAlert.Visible = False
                           '  Alerts.Add(DiagAlert)
                           ListenerProcess.Add(PanelContainer, DiagAlert)
                           DiagAlert.Visible = True

                           ID += 1
                       End Sub)

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click

        If IsScaning = False Then

            Dim ControlsList As Control.ControlCollection = PanelContainer.Controls
            Dim MaxVal As Integer = ControlsList.Count
            If MaxVal = 0 Then
                cts.Cancel()
                DirectCast(Me.ParentForm, MalScanForm).RemoveScan(Me)
            Else
                Dim Asynctask As New Task(New Action(Async Sub()
                                                         Dim CurrentVal As Integer = 0

                                                         For Each BOption As Control In ControlsList
                                                             CurrentVal += 1
                                                             Dim CalculateVal As Integer = Math.Round((CurrentVal * 100) / MaxVal)

                                                             Me.BeginInvoke(Sub()
                                                                                Guna2ProgressBar1.Value = CalculateVal
                                                                            End Sub)

                                                             If TypeOf BOption Is AV_Item Then
                                                                 Dim BOptionEx As AV_Item = DirectCast(BOption, AV_Item)

                                                                 BOptionEx.Solve()

                                                             End If

                                                             System.Threading.Thread.Sleep(500)

                                                         Next

                                                         Me.BeginInvoke(Sub()
                                                                            PanelContainer.Controls.Clear()
                                                                            DirectCast(Me.ParentForm, MalScanForm).RemoveScan(Me)
                                                                        End Sub)

                                                     End Sub), TaskCreationOptions.PreferFairness)

                Asynctask.Start()
            End If

        End If

    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        cts.Cancel()
        DirectCast(Me.ParentForm, MalScanForm).RemoveScan(Me)
    End Sub

    Private Sub Guna2Button4_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2Button4.CheckedChanged
        If Guna2Button4.Checked = True Then
            For Each BOption2 As Control In PanelContainer.Controls
                If TypeOf BOption2 Is AV_Item Then
                    Dim BOptionEx As AV_Item = DirectCast(BOption2, AV_Item)
                    BOptionEx.SetSwitch(True)
                End If
            Next
        End If
    End Sub

    Private Sub Guna2Button3_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2Button3.CheckedChanged
        If Guna2Button3.Checked = True Then

            For Each BOption2 As Control In PanelContainer.Controls
                If TypeOf BOption2 Is AV_Item Then
                    Dim BOptionEx As AV_Item = DirectCast(BOption2, AV_Item)
                    BOptionEx.SetSwitch(False)
                End If
            Next
        End If
    End Sub

#End Region



End Class