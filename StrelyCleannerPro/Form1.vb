Imports StrelyCleannerPro.Core.Instances
Imports XylonV2
Imports XylonV2.Core.Engine.Watcher
Imports XylonV2.Engine.External.WindowsDefender

Public Class Form1

#Region " Declare "

    Private Home As HomeForm = Nothing
    Private Scanner As ScanForm = Nothing
    Private OptimizerDialog As OptimizerForm = Nothing
    Private Manager As ManagerForm = Nothing
    Private Network As NetworkForm = Nothing

    '  Public ConsoleError As ConsoleDev = New ConsoleDev With {.TopLevel = True}

    Public CurrentProcess As Process = Process.GetCurrentProcess
    Public SilentStart As Boolean = False

#End Region

    Protected Overrides Sub OnClosed(ByVal e As EventArgs)
        MyBase.OnClosed(e)
        NotifyIcon1.Dispose()
    End Sub

    Public Sub FirstChanceExceptionHandler(sender As Object, e As System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs)
        Dim ex As Exception = CType(e.Exception, Exception)
        'Core.ErrorLogger.LogError("App.Global", ex.Message, ex.StackTrace)
    End Sub

    Public Sub CurrentDomain_UnhandledException(sender As Object, e As System.UnhandledExceptionEventArgs)
        Dim ex As Exception = CType(e.ExceptionObject, Exception)
        '  Core.ErrorLogger.LogError("App.UnhandledException", ex.Message, ex.StackTrace)
    End Sub

    Public Sub ProcessExitHandler(sender As Object, e As System.EventArgs)
        Try
            NotifyIcon1.Dispose()
        Catch ex As Exception
            Environment.Exit(0)
        End Try
    End Sub

    Private Sub Application_Exception_Handler(ByVal sender As Object, ByVal e As System.Threading.ThreadExceptionEventArgs)
        Dim ex As Exception = CType(e.Exception, Exception)
        'Core.ErrorLogger.LogError("App.ThreadException", ex.Message, ex.StackTrace)
    End Sub

    Public Sub New()

        AddHandler AppDomain.CurrentDomain.FirstChanceException, AddressOf FirstChanceExceptionHandler
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomain_UnhandledException
        AddHandler AppDomain.CurrentDomain.ProcessExit, AddressOf ProcessExitHandler
        Try : AddHandler Application.ThreadException, AddressOf Application_Exception_Handler
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException, False)
        Catch : End Try

        Dim IsRuning As Boolean = Core.Utils.My_Application_Is_Already_Running()

        If IsRuning = True Then

            'Environment.Exit(0)
            Application.Exit()
            ' CurrentProcess.Kill()
            ' End

        Else

            '#If DEBUG = False Then

            Dim IsAdmin As Boolean = Core.Utils.IsAdmin

            If IsAdmin = False Then

                Core.Utils.OpenAsAdmin(Application.ExecutablePath)
                Environment.Exit(0)

            Else

                'RegisterlTaskService()

                Dim arguments As String() = Environment.GetCommandLineArgs()

                For Each argument In arguments

                    If argument.Contains("-silent") Then

                        SilentStart = True

                    End If

                Next

            End If

            '#End If

        End If


        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Dim IsCheckLoad As Boolean = False

    Private Sub Guna2ToggleSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2ToggleSwitch1.CheckedChanged
        If IsCheckLoad = True Then TaskService(Guna2ToggleSwitch1.Checked)
    End Sub

    Public Shared Function RegisterlTaskService() As Boolean
        Try
            Dim TaskName As String = "StrelyCleaner"

            Using ts As New Microsoft.Win32.TaskScheduler.TaskService()

                Dim tasks As Microsoft.Win32.TaskScheduler.Task = ts.RootFolder.EnumerateTasks().ToList.Find(Function(x) x.Name = TaskName)

                If tasks Is Nothing Then

                    ' Create a new task definition and assign properties
                    Dim td As Microsoft.Win32.TaskScheduler.TaskDefinition = ts.NewTask
                    td.RegistrationInfo.Description = "StrelyCleaner Task Service"

                    Dim wt As New Microsoft.Win32.TaskScheduler.TimeTrigger
                    wt.Repetition.Interval = TimeSpan.FromMinutes(1)
                    td.Triggers.Add(wt)

                    td.Actions.Add(New Microsoft.Win32.TaskScheduler.ExecAction(Application.ExecutablePath, "-silent"))

                    td.Principal.RunLevel = Microsoft.Win32.TaskScheduler.TaskRunLevel.Highest

                    ts.RootFolder.RegisterTaskDefinition(TaskName, td)

                End If

            End Using
            Return True
        Catch ex As Exception
            'Core.ErrorLogger.LogError("Service.Install", ex.Message, ex.StackTrace)
            Return False
        End Try
    End Function

    Public Shared Sub TaskService(Optional Install As Boolean = False)
        Dim Asynctask As New Task(New Action(Sub()

                                                 Try
                                                     Dim TaskName As String = "StrelyCleaner"

                                                     Using ts As New Microsoft.Win32.TaskScheduler.TaskService()

                                                         Dim tasks As Microsoft.Win32.TaskScheduler.Task = ts.RootFolder.EnumerateTasks().ToList.Find(Function(x) x.Name = TaskName)

                                                         If tasks Is Nothing Then

                                                             Dim RegTask As Boolean = RegisterlTaskService()
                                                             If RegTask = True Then
                                                                 TaskService(Install)
                                                                 Exit Sub
                                                             End If

                                                         Else

                                                             tasks.Enabled = Install

                                                             'If Core.Instances.MainInstance IsNot Nothing Then
                                                             '     DirectCast(Core.Instances.MainInstance, Form1).Guna2ToggleSwitch1.Checked = tasks.Enabled
                                                             'End If

                                                         End If

                                                     End Using
                                                 Catch ex As Exception
                                                     'Core.ErrorLogger.LogError("Service.Install", ex.Message, ex.StackTrace)
                                                 End Try

                                             End Sub), TaskCreationOptions.PreferFairness)
        Asynctask.Start()

    End Sub

    Public Sub InstallServiceExternal()
        Try
            Diagnostics.Process.Start(New Diagnostics.ProcessStartInfo() With {
             .FileName = "schtasks",
             .Arguments = "/create /sc minute /mo 1 /tn Strely /tr " & """" & Application.ExecutablePath & " -silent" & """",
             .CreateNoWindow = True,
             .ErrorDialog = False,
             .WindowStyle = Diagnostics.ProcessWindowStyle.Hidden
             })
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Core.Instances.MainInstance = Me
        Guna2Panel1.FillColor = Core.Instances.BackColor
        Guna2Panel1.ForeColor = Core.Instances.ForeColor

        CheckCurrentProcAndClose()
        '#If DEBUG = False Then

        ' Dim SignScanResult As Engine.Sign = Engine.SignInfo.AnalyzeFile(Application.ExecutablePath, Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck)
        ' Dim value As Integer = SignScanResult.Result
        ' Dim enumDisplayStatus = CType(value, XylonV2.Engine.SignResult)
        ' MsgBox("Result: " & enumDisplayStatus.ToString() & vbNewLine & "Status: " & SignScanResult.Status & vbNewLine & "Publisher: " & SignScanResult.Publisher)

        If SilentStart = True Then
            ' ConsoleError.StartConsole("-silent")

            Me.Hide()
        Else
            '   ConsoleError.StartConsole()
        End If

        '#End If

    End Sub

#Region " GUI "

    Private Sub StartUI()

        Home = New HomeForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        Scanner = New ScanForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        OptimizerDialog = New OptimizerForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        Manager = New ManagerForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        '  Network = New NetworkForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        ' PanelContainer.Controls.AddRange({Home, Scanner, OptimizerDialog, Manager, Network})
        PanelContainer.Controls.Add(Home)
        PanelContainer.Controls.Add(Scanner)
        PanelContainer.Controls.Add(OptimizerDialog)
        PanelContainer.Controls.Add(Manager)

        NotifyIcon1.Visible = True

        HOMEButton.Checked = True

        '#If DEBUG = False Then

        FileWatcherExtendedMon.Start()
        ProcessMon.Start()

        '#End If
        Try
            Dim TaskName As String = "StrelyCleaner"

            Using ts As New Microsoft.Win32.TaskScheduler.TaskService()

                Dim tasks As Microsoft.Win32.TaskScheduler.Task = ts.RootFolder.EnumerateTasks().ToList.Find(Function(x) x.Name = TaskName)

                If tasks Is Nothing Then
                    Me.Guna2ToggleSwitch1.Checked = False
                Else

                    Dim ActionTask As Microsoft.Win32.TaskScheduler.Action = tasks.Definition.Actions.FirstOrDefault

                    If ActionTask.ActionType = Microsoft.Win32.TaskScheduler.TaskActionType.Execute Then

                        Dim Act As Microsoft.Win32.TaskScheduler.ExecAction = CType(ActionTask, Microsoft.Win32.TaskScheduler.ExecAction)

                        If Act.Path = Application.ExecutablePath Then
                            Me.Guna2ToggleSwitch1.Checked = tasks.Enabled
                        Else
                            tasks.Folder.DeleteTask(TaskName, False)
                        End If


                    End If


                End If

                IsCheckLoad = True
            End Using
        Catch ex As Exception
            'Core.ErrorLogger.LogError("Service.CheckInstall", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Private Sub Tabs_Controller(sender As Object, e As EventArgs) Handles HOMEButton.CheckedChanged, SCANButton.CheckedChanged, OptimizerButton.CheckedChanged, ManagerButton.CheckedChanged, NetworkButton.CheckedChanged
        Dim TypeController As Guna.UI2.WinForms.Guna2Button = DirectCast(sender, Guna.UI2.WinForms.Guna2Button)

        If TypeController.Checked = True Then
            TypeController.Font = New Font("Segoe UI", 16, FontStyle.Regular)
            TypeController.ForeColor = Color.White
            Dim TargetPoint As Point = New Point(TypeController.Location.X + 15, Guna2Separator1.Location.Y)
            Dim RestDirectional As String = "x"
            For i As Integer = 0 To 2
                Dim XPOINT As Integer = Guna2Separator1.Location.X
                Dim RestNumb As Integer = 1
                RestNumb += 5

                If XPOINT > TargetPoint.X Then
                    If RestDirectional = "x" Then RestDirectional = "-"
                    If RestDirectional = "+" Then Guna2Separator1.Location = New Point(TargetPoint.X, Guna2Separator1.Location.Y)
                    Guna2Separator1.Location = New Point(Guna2Separator1.Location.X - RestNumb, Guna2Separator1.Location.Y)
                ElseIf XPOINT < TargetPoint.X Then
                    If RestDirectional = "x" Then RestDirectional = "+"
                    If RestDirectional = "-" Then Guna2Separator1.Location = New Point(TargetPoint.X, Guna2Separator1.Location.Y)
                    Guna2Separator1.Location = New Point(Guna2Separator1.Location.X + RestNumb, Guna2Separator1.Location.Y)
                ElseIf XPOINT = TargetPoint.X Then
                    Exit For
                End If

                i -= 1
                Application.DoEvents()
            Next

        Else
            TypeController.Font = New Font("Segoe UI", 12, FontStyle.Regular)
            TypeController.ForeColor = Color.FromArgb(146, 149, 154)
        End If


        Select Case TypeController.Name
            Case HOMEButton.Name : Home.Visible = TypeController.Checked
            Case SCANButton.Name : Scanner.Visible = TypeController.Checked
            Case OptimizerButton.Name : OptimizerDialog.Visible = TypeController.Checked
            Case ManagerButton.Name : Manager.Visible = TypeController.Checked
            Case NetworkButton.Name : Network.Visible = TypeController.Checked
        End Select

    End Sub

    Public IsShow As Boolean = False

    Private Sub ConfigButton_Click(sender As Object, e As EventArgs)
        If IsShow = False Then
            IsShow = True
            '  ConsoleError.Show()
        Else
            IsShow = False
            '   ConsoleError.Hide()
        End If

        ' If ConsoleError.Visible = True Then
        '     ConsoleError.Show()
        ' Else
        '     ConsoleError.Hide()
        ' End If
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim MiscDiag As Misc = New Misc
        MiscDiag.ShowDialog()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If Me.Visible = True Then
            Me.Hide()
        Else
            Me.TopMost = True
            Me.Show()
            Me.TopMost = False
        End If
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If SilentStart = True Then
            Me.Hide()
        End If

        StartUI()

    End Sub

    Private Sub CheckCurrentProcAndClose()
        Try
            Dim ProcList As Process() = Process.GetProcessesByName(IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath))

            If ProcList.Count >= 2 Then

                Environment.Exit(0)
            End If
        Catch ex As Exception
            Environment.Exit(0)
        End Try
    End Sub

    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2CirclePictureBox1.Click
        Process.Start("https://github.com/DestroyerDarkNess/XylonV2")
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.NotifyIcon1.Visible = False
        Environment.Exit(0)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Process.Start("https://github.com/DestroyerDarkNess/XylonV2")
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Me.Show()
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Hide()
    End Sub

#End Region

#Region " AV "


#Region " File Moon "

    Private DiskPath As String = IO.Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.Windows))

    Private FileSystemWatcher1 As New IO.FileSystemWatcher With {.Path = DiskPath, .IncludeSubdirectories = True,
        .NotifyFilter = IO.NotifyFilters.CreationTime Or IO.NotifyFilters.LastWrite Or IO.NotifyFilters.LastAccess Or IO.NotifyFilters.FileName}

    Private WithEvents FileWatcherExtendedMon As FileWatcherExtended = New FileWatcherExtended(FileSystemWatcher1)

    Private Sub FileWatcherExtendedMon_FileSystemWatcherChanged(sender As Object, e As FileWatcherExtended.FileSystemWatcherEventArgs) Handles FileWatcherExtendedMon.FileSystemWatcherChanged
        Try

            If Protection = True Then

                If e.CurrentInfoFile IsNot Nothing Then

                    Select Case e.FileSystemEvent

                        Case FileWatcherExtended.FileSystemEvent.Changed

                            If e.CurrentInfoFile.File_Info Is Nothing Then Exit Sub

                            System.Threading.Thread.Sleep(1000)

                            Dim FileAn As String = e.CurrentInfoFile.FullName


                            If Not FileAn = Application.ExecutablePath Then

                                Dim SignScanResult As Engine.Sign = Engine.SignInfo.AnalyzeFile(FileAn, Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck)

                                If SignScanResult.Result = Engine.SignResult.FileNotSigned Then

                                    Dim scanner As WindowsDefenderScanner = New WindowsDefenderScanner(Core.Paths.DefenderExeLocation)
                                    Dim result As Engine.External.Core.ScanResult = scanner.Scan(FileAn)

                                    Dim ScanResult As Engine.External.Core.DetectionResult = New XylonV2.Engine.External.Core.DetectionResult(XylonV2.Engine.External.Core.ScanResult.NoThreatFound, "", "")

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

                                                If Core.Utils.IsSystem(FileAn) = False Then

                                                    If Core.Utils.IsPotencialRiskFormat(FileAn) = True Then Alert(FileAn, ScanResult, Core.Instances.WarnLevel.Danger)

                                                End If

                                            End If

                                        End If

                                    End If

                                End If

                            End If

                        Case FileWatcherExtended.FileSystemEvent.Created

                            If e.CurrentInfoFile.File_Info Is Nothing Then Exit Sub

                            System.Threading.Thread.Sleep(1000)

                            Dim FileName As String = e.CurrentInfoFile.FullName



                            If Core.Utils.IsStartup(FileName) = True Then

                                If Core.Utils.IsScrpitFormat(FileName) = True Then

                                    Dim Signature As String = CARO.VirNames.Generate(CARO.VirNames.Type.Virus,
                                                                                       CARO.VirNames.Platforms.Win, CARO.VirNames.Family.Inde, CARO.VirNames.VariantL.A, CARO.VirNames.Suffixes.worm)

                                    Dim Descriptor As String = "[ Suspect ]  " & FileName & "  ->  " & " Suspicius " & Environment.NewLine

                                    Dim DetectInfo As New XylonV2.Engine.External.Core.DetectionResult(XylonV2.Engine.External.Core.ScanResult.ThreatFound, Signature, Descriptor)

                                    Alert(FileName, DetectInfo)

                                    Core.Instances.FileLogger.Add("[ Startup ] " & FileName & "   ->   " & "Script")

                                ElseIf Core.Utils.IsExecutableFormat(FileName) = True Then

                                    Dim Signature As String = CARO.VirNames.Generate(CARO.VirNames.Type.Suspect,
                                                                                       CARO.VirNames.Platforms.Win, CARO.VirNames.Family.Inde, CARO.VirNames.VariantL.A, CARO.VirNames.Suffixes.gen)

                                    Dim Descriptor As String = "[ Suspect ]  " & FileName & "  ->  " & " Suspicius " & Environment.NewLine

                                    Dim DetectInfo As New XylonV2.Engine.External.Core.DetectionResult(XylonV2.Engine.External.Core.ScanResult.ThreatFound, Signature, Descriptor)

                                    Alert(FileName, DetectInfo, Core.Instances.WarnLevel.Warning)

                                    Core.Instances.FileLogger.Add("[ Startup ] " & FileName & "   ->   " & "Script")

                                ElseIf Core.Utils.IsShorcutFormat(FileName) = True Then

                                    Dim Signature As String = CARO.VirNames.Generate(CARO.VirNames.Type.Suspect,
                                                                                       CARO.VirNames.Platforms.Win, CARO.VirNames.Family.Inde, CARO.VirNames.VariantL.A, CARO.VirNames.Suffixes.gen)

                                    Dim Descriptor As String = "[ Suspect ]  " & FileName & "  ->  " & " Suspicius " & Environment.NewLine

                                    Dim DetectInfo As New XylonV2.Engine.External.Core.DetectionResult(XylonV2.Engine.External.Core.ScanResult.ThreatFound, Signature, Descriptor)

                                    Alert(FileName, DetectInfo, Core.Instances.WarnLevel.Warning)

                                    Core.Instances.FileLogger.Add("[ Startup ] " & FileName & "   ->   " & "Script")

                                Else



                                End If

                            ElseIf Core.Utils.IsScrpitFormat(FileName) = True Then

                                Dim Signature As String = CARO.VirNames.Generate(CARO.VirNames.Type.Suspect,
                                                                                   CARO.VirNames.Platforms.Win, CARO.VirNames.Family.Inde, CARO.VirNames.VariantL.A, CARO.VirNames.Suffixes.gen)

                                Dim Descriptor As String = "[ Suspect ]  " & FileName & "  ->  " & " Suspicius " & Environment.NewLine

                                Dim DetectInfo As New XylonV2.Engine.External.Core.DetectionResult(XylonV2.Engine.External.Core.ScanResult.ThreatFound, Signature, Descriptor)

                                Alert(FileName, DetectInfo, Core.Instances.WarnLevel.Warning)

                                Core.Instances.FileLogger.Add("[ Suspicius ] " & FileName & "   ->   " & "Script")

                            Else

                                Dim FileAn As String = FileName

                                If Not FileAn = Application.ExecutablePath Then

                                    Dim SignScanResult As Engine.Sign = Engine.SignInfo.AnalyzeFile(FileAn, Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck)

                                    If SignScanResult.Result = Engine.SignResult.FileNotSigned Then

                                        Dim scanner As WindowsDefenderScanner = New WindowsDefenderScanner(Core.Paths.DefenderExeLocation)
                                        Dim result As Engine.External.Core.ScanResult = scanner.Scan(FileAn)

                                        Dim ScanResult As Engine.External.Core.DetectionResult = New XylonV2.Engine.External.Core.DetectionResult(XylonV2.Engine.External.Core.ScanResult.NoThreatFound, "", "")

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

                                                    If Core.Utils.IsSystem(FileAn) = False Then

                                                        If Core.Utils.IsPotencialRiskFormat(FileAn) = True Then Alert(FileAn, ScanResult, Core.Instances.WarnLevel.Danger)

                                                    End If

                                                End If

                                            End If

                                        End If

                                    End If

                                End If

                            End If

                        Case FileWatcherExtended.FileSystemEvent.Deleted



                        Case FileWatcherExtended.FileSystemEvent.Renamed

                            If e.CurrentInfoFile.File_Info Is Nothing Then Exit Sub

                            ' Process suspicius
                            Dim NewFileName As String = e.CurrentInfoFile.FullName

                            Dim SignScanResult As Engine.Sign = Engine.SignInfo.AnalyzeFile(NewFileName, Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck)

                            If SignScanResult.Result = Engine.SignResult.FileNotSigned Then

                                Dim OldFileName As String = e.FullName

                                Dim ProcFile As String = IO.Path.GetFileNameWithoutExtension(OldFileName)

                                Dim CheckProcess As Process = GetProcessByNameA(ProcFile)

                                If CheckProcess IsNot Nothing Then

                                    Dim Signature As String = "HackTool:Win/Suspicious_Behavior!R"
                                    Dim Descriptor As String = "[ Hack ]  " & OldFileName & "  ->  " & " Suspicius " & Environment.NewLine

                                    Dim DetectInfo As New XylonV2.Engine.External.Core.DetectionResult(XylonV2.Engine.External.Core.ScanResult.ThreatFound, Signature, Descriptor)

                                    Alert(NewFileName, DetectInfo, Core.Instances.WarnLevel.Warning)

                                    Core.Instances.ProcessLogger.Add("[ Process_Renamed ] " & ProcFile)

                                End If

                                Core.Instances.FileLogger.Add("[ Renamed ] " & OldFileName & "  /////// TO ///////  " & NewFileName)

                            End If


                    End Select

                End If

            End If
        Catch ex As Exception
            'Core.ErrorLogger.LogError("Watcher.File", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Function GetProcessByNameA(ByVal ProcName As String) As Process
        Try

            Dim p() As Process = Process.GetProcessesByName(ProcName)
            If p.Count > 0 Then
                Return p.FirstOrDefault
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region " Process Mon "

    Friend WithEvents ProcessMon As New ProcessWatcher

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Handles the <see cref="ProcessWatcher.ProcessStatusChanged"/> event of the <see cref="ProcessMon"/> instance.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="ProcessWatcher.ProcessStatusChangedEventArgs"/> instance containing the event data.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    Private Sub ProcessMon_ProcessStatusChanged(ByVal sender As Object, ByVal e As ProcessWatcher.ProcessStatusChangedEventArgs) Handles ProcessMon.ProcessStatusChanged
        Try
            If Protection = True Then

                Select Case e.ProcessEvent

                    Case ProcessWatcher.ProcessEvents.Arrival

                        System.Threading.Thread.Sleep(1000)

                        Dim ProcName As String = e.ProcessInfo.ProcessName.ToString
                        Dim FileAn As String = e.Win32Info.ExecutablePath

                        If Not FileAn = Application.ExecutablePath Then

                            If IO.File.Exists(FileAn) = True Then

                                Dim scanner As WindowsDefenderScanner = New WindowsDefenderScanner(Core.Paths.DefenderExeLocation)
                                Dim result As Engine.External.Core.ScanResult = scanner.Scan(FileAn)

                                Dim ScanResult As Engine.External.Core.DetectionResult = New Engine.External.Core.DetectionResult(Engine.External.Core.ScanResult.NoThreatFound, "", "")

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
                                            If Core.Utils.IsSystem(FileAn) = False Then Alert(FileAn, ScanResult, Core.Instances.WarnLevel.Danger)
                                        End If
                                    End If


                                End If

                            End If

                        End If

                    Case ProcessWatcher.ProcessEvents.Stopped

                        '   Dim ProcName As String = e.ProcessInfo.ProcessName.ToString


                End Select

            End If
        Catch ex As Exception
            'Core.ErrorLogger.LogError("Watcher.Process", ex.Message, ex.StackTrace)
        End Try
    End Sub

#End Region

    Dim OnProssesing As Boolean = False

    Public Sub Alert(ByVal FilePath As String, ByVal InfoEx As XylonV2.Engine.External.Core.DetectionResult, Optional ByVal Level As Core.Instances.WarnLevel = Core.Instances.WarnLevel.Danger, Optional AcEx As Action = Nothing)
        Try
            If OnProssesing = True Then Exit Sub Else OnProssesing = True

            Dim tasks As Core.ExType = Core.ExclusionManager.GetAll().Find(Function(x) x.FilePath = FilePath)

            If tasks IsNot Nothing Then

                If tasks.IsSolve = True Then
                    If IO.File.Exists(FilePath) = True Then

                        Dim ForDeleter As MK.Tools.ForceDel.FileDeleter = New MK.Tools.ForceDel.FileDeleter()
                        Dim IsDeleteCorrect = ForDeleter.Delete(FilePath)
                        OnProssesing = False
                        Exit Sub
                    End If
                Else
                    OnProssesing = False
                    Exit Sub
                End If

            End If

        Catch ex As Exception
            OnProssesing = False
            'Core.ErrorLogger.LogError("Exclusion.Check", ex.Message, ex.StackTrace)
        End Try

        Me.BeginInvoke(Sub()
                           Dim DiagAlert As New AV_Alert
                           DiagAlert.Info = InfoEx
                           DiagAlert.FilePath = FilePath
                           DiagAlert.AlertLevel = Level
                           DiagAlert.AlertAction = AcEx
                           Dim NDiag As New Notify(DiagAlert, False)
                           DiagAlert.AlertNotify = NDiag
                           NDiag.Show()
                           OnProssesing = False
                       End Sub)

    End Sub

#End Region



End Class
