Imports Microsoft.VisualBasic.FileIO
Imports Microsoft.Win32
Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Text

Namespace Core.Optimizer

    Public Class Optimize

#Region " Required "

        Shared ReadOnly readyMadeMenusItems As String() = {ReadyMadeMenusFolder & "DesktopShortcuts.reg", ReadyMadeMenusFolder & "SystemShortcuts.reg", ReadyMadeMenusFolder & "PowerMenu.reg", ReadyMadeMenusFolder & "SystemTools.reg", ReadyMadeMenusFolder & "WindowsApps.reg", ReadyMadeMenusFolder & "InstallTakeOwnership.reg", ReadyMadeMenusFolder & "RemoveTakeOwnership.reg"}
        Shared ReadOnly readyMadeMenusFiles As String() = {My.Resources.DesktopShortcuts, My.Resources.SystemShortcuts, My.Resources.PowerMenu, My.Resources.SystemTools, My.Resources.WindowsApps, My.Resources.InstallTakeOwnership, My.Resources.RemoveTakeOwnership}
        Shared ReadOnly scriptItems As String() = {ScriptsFolder & "DisableOfficeTelemetryTasks.bat", ScriptsFolder & "DisableOfficeTelemetryTasks.reg", ScriptsFolder & "EnableOfficeTelemetryTasks.bat", ScriptsFolder & "EnableOfficeTelemetryTasks.reg", ScriptsFolder & "DisableTelemetryTasks.bat", ScriptsFolder & "EnableTelemetryTasks.bat", ScriptsFolder & "DisableXboxTasks.bat", ScriptsFolder & "EnableXboxTasks.bat", ScriptsFolder & "OneDrive_Uninstaller.cmd", ScriptsFolder & "GPEditEnablerInHome.bat", ScriptsFolder & "FlushDNSCache.bat", ScriptsFolder & "AddOpenWithCMD.reg"}
        Shared ReadOnly scriptFiles As String() = {My.Resources.DisableOfficeTelemetryTasks, My.Resources.DisableOfficeTelemetry, My.Resources.EnableOfficeTelemetryTasks, My.Resources.EnableOfficeTelemetry, My.Resources.DisableTelemetryTasks, My.Resources.EnableTelemetryTasks, My.Resources.DisableXboxTasks, My.Resources.EnableXboxTasks, Encoding.UTF8.GetString(My.Resources.OneDrive_Uninstaller), My.Resources.GPEditEnablerInHome, My.Resources.FlushDNSCache, My.Resources.AddOpenWithCMD}

        Public Shared Async Function Deploy() As Task(Of Boolean)
            Try
                If Not Directory.Exists(CoreFolder) Then
                    Directory.CreateDirectory(CoreFolder)
                End If

                If Not Directory.Exists(ReadyMadeMenusFolder) Then
                    Directory.CreateDirectory(ReadyMadeMenusFolder)
                End If

                If Not Directory.Exists(ScriptsFolder) Then
                    Directory.CreateDirectory(ScriptsFolder)
                End If

                If Not Directory.Exists(ExtractedIconsFolder) Then
                    Directory.CreateDirectory(ExtractedIconsFolder)
                End If

                If Not Directory.Exists(FavIconsFolder) Then
                    Directory.CreateDirectory(FavIconsFolder)
                End If

                If Not Directory.Exists(StartupItemsBackupFolder) Then
                    Directory.CreateDirectory(StartupItemsBackupFolder)
                End If

                For i As Integer = 0 To readyMadeMenusItems.Length - 1
                    If Not File.Exists(IO.Path.Combine(ReadyMadeMenusFolder, readyMadeMenusItems(i))) Then File.WriteAllText(IO.Path.Combine(ReadyMadeMenusFolder, readyMadeMenusItems(i)), readyMadeMenusFiles(i))
                Next

                For i As Integer = 0 To scriptItems.Length - 1

                    If Not File.Exists(scriptItems(i)) Then

                        If scriptItems(i).Contains("OneDrive") Then
                            File.WriteAllBytes(IO.Path.Combine(ScriptsFolder, scriptItems(i)), Encoding.UTF8.GetBytes(scriptFiles(i)))
                        Else
                            File.WriteAllText(IO.Path.Combine(ScriptsFolder, scriptItems(i)), scriptFiles(i))
                        End If
                    End If
                Next
                Return True
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.Deploy", ex.Message, ex.StackTrace)
                Return False
            End Try
        End Function

#End Region


        Friend Shared ReadOnly ProgramData As String = Core.Paths.CachePath 'Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)

        Friend Shared ReadOnly CoreFolder As String = ProgramData & "\Optimizer\"
        Friend Shared ReadOnly ReadyMadeMenusFolder As String = ProgramData & "\Optimizer\ReadyMadeMenus\"
        Friend Shared ReadOnly ScriptsFolder As String = ProgramData & "\Optimizer\Required\"
        Friend Shared ReadOnly ExtractedIconsFolder As String = ProgramData & "\Optimizer\ExtractedIcons\"
        Friend Shared ReadOnly FavIconsFolder As String = ProgramData & "\Optimizer\FavIcons\"
        Friend Shared ReadOnly StartupItemsBackupFolder As String = ProgramData & "\Optimizer\StartupBackup\"


        Public Shared ReadOnly CompatTelRunnerFile As String = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Windows\System32\CompatTelRunner.exe")
        Public Shared ReadOnly CompatTelRunnerFileOff As String = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Windows\System32\CompatTelRunner.exe.OFF")
        Public Shared ReadOnly CompatTelRunnerFileName As String = "CompatTelRunner.exe"
        Public Shared ReadOnly CompatTelRunnerFileNameOff As String = "CompatTelRunner.exe.OFF"
        Public Shared ReadOnly DiagnosisAutoLoggerFolder As String = Path.Combine(ProgramData, "Microsoft\Diagnosis\ETLLogs\AutoLogger")

        Friend Async Sub DisableTelemetryRunner()
            Try
                If File.Exists(CompatTelRunnerFileOff) Then File.Delete(CompatTelRunnerFileOff)

                If File.Exists(CompatTelRunnerFile) Then
                    Utilities.RunCommand(String.Format("takeown /F {0}", CompatTelRunnerFile))
                    Utilities.RunCommand(String.Format("icacls ""{0}"" /grant administrators:F", CompatTelRunnerFile))
                    FileSystem.RenameFile(CompatTelRunnerFile, CompatTelRunnerFileNameOff)
                End If

            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.DisableTelemetryRunner", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Friend Async Sub EnableTelemetryRunner()
            Try

                If File.Exists(CompatTelRunnerFileOff) Then
                    FileSystem.RenameFile(CompatTelRunnerFileOff, CompatTelRunnerFileName)
                End If

            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.EnableTelemetryRunner", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Friend Async Sub EnablePerformanceTweaks()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "Append Completion", "yes", RegistryValueKind.String)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "AutoSuggest", "yes", RegistryValueKind.String)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "EnableAutoTray", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Remote Assistance", "fAllowToGetHelp", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DisallowShaking", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CLASSES_ROOT\AllFilesystemObjects\shellex\ContextMenuHandlers\Copy To", "", "{C2FBB630-2971-11D1-A18C-00C04FD75D13}")
            Registry.SetValue("HKEY_CLASSES_ROOT\AllFilesystemObjects\shellex\ContextMenuHandlers\Move To", "", "{C2FBB631-2971-11D1-A18C-00C04FD75D13}")
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "AutoEndTasks", "1")
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "HungAppTimeout", "1000")
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "MenuShowDelay", "8")
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "WaitToKillAppTimeout", "2000")
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "LowLevelHooksTimeout", "1000")
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Mouse", "MouseHoverTime", "8")
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoLowDiskSpaceChecks", "00000001", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "LinkResolveIgnoreLinkInfo", "00000001", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoResolveSearch", "00000001", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoResolveTrack", "00000001", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoInternetOpenWith", "00000001", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control", "WaitToKillServiceTimeout", "2000")
            Utilities.StopService("DiagTrack")
            Utilities.StopService("diagnosticshub.standardcollector.service")
            Utilities.StopService("dmwappushservice")
            Utilities.RunCommand("sc config ""RemoteRegistry"" start= disabled")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DiagTrack", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\dmwappushservice", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "HideFileExt", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "Hidden", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "SystemResponsiveness", 1, RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "GPU Priority", 8, RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "Priority", 6, RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "Scheduling Category", "High", RegistryValueKind.String)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "SFIO Priority", "High", RegistryValueKind.String)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Low Latency", "GPU Priority", 0, RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Low Latency", "Priority", 8, RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Low Latency", "Scheduling Category", "Medium", RegistryValueKind.String)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Low Latency", "SFIO Priority", "High", RegistryValueKind.String)
        End Sub

        Friend Async Sub DisablePerformanceTweaks()
            Try
                Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", True).DeleteValue("Append Completion", False)
                Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", True).DeleteValue("AutoSuggest", False)
                Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer", True).DeleteValue("EnableAutoTray", False)
                Registry.SetValue("HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Remote Assistance", "fAllowToGetHelp", "1", RegistryValueKind.DWord)
                Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DisallowShaking", "0", RegistryValueKind.DWord)
                Registry.ClassesRoot.DeleteSubKeyTree("AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Copy To", False)
                Registry.ClassesRoot.DeleteSubKeyTree("AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Move To", False)
                Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True).DeleteValue("AutoEndTasks", False)
                Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True).DeleteValue("HungAppTimeout", False)
                Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True).DeleteValue("WaitToKillAppTimeout", False)
                Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True).DeleteValue("LowLevelHooksTimeout", False)
                Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "MenuShowDelay", "400")
                Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Mouse", "MouseHoverTime", "400")
                Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", True).DeleteValue("NoLowDiskSpaceChecks", False)
                Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", True).DeleteValue("LinkResolveIgnoreLinkInfo", False)
                Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", True).DeleteValue("NoResolveSearch", False)
                Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", True).DeleteValue("NoResolveTrack", False)
                Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", True).DeleteValue("NoInternetOpenWith", False)
                Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control", "WaitToKillServiceTimeout", "5000")
                Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DiagTrack", "Start", "2", RegistryValueKind.DWord)
                Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", "Start", "2", RegistryValueKind.DWord)
                Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\dmwappushservice", "Start", "2", RegistryValueKind.DWord)
                Utilities.StartService("DiagTrack")
                Utilities.StartService("diagnosticshub.standardcollector.service")
                Utilities.StartService("dmwappushservice")
                Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "HideFileExt", "1", RegistryValueKind.DWord)
                Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "Hidden", "0", RegistryValueKind.DWord)
                Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "SystemResponsiveness", 14, RegistryValueKind.DWord)
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", True).DeleteValue("GPU Priority", False)
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", True).DeleteValue("Priority", False)
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", True).DeleteValue("Scheduling Category", False)
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", True).DeleteValue("SFIO Priority", False)
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Low Latency", True).DeleteValue("GPU Priority", False)
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Low Latency", True).DeleteValue("Priority", False)
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Low Latency", True).DeleteValue("Scheduling Category", False)
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Low Latency", True).DeleteValue("SFIO Priority", False)
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.DisablePerformanceTweaks", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Friend Async Sub DisableTelemetryServices()
            Utilities.StopService("DiagTrack")
            Utilities.StopService("diagnosticshub.standardcollector.service")
            Utilities.StopService("dmwappushservice")
            Utilities.StopService("DcpSvc")
            Utilities.StopService("DPS")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DiagTrack", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\dmwappushservice", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DcpSvc", "Start", "4", RegistryValueKind.DWord)
            Utilities.RunCommand("sc config ""DPS"" start=disabled")
            Utilities.RunCommand("reg add ""HKLM\Software\Microsoft\PolicyManager\default\WiFi\AllowAutoConnectToWiFiSenseHotspots"" /v value /t REG_DWORD /d 0 /f")
            Utilities.RunCommand("reg add ""HKLM\Software\Microsoft\PolicyManager\default\WiFi\AllowWiFiHotSpotReporting"" /v value /t REG_DWORD /d 0 /f")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "PublishUserActivities", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\SQMClient\Windows", "CEIPEnable", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\AppCompat", "AITEnable", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\AppCompat", "DisableUAR", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Device Metadata", "PreventDeviceMetadataFromNetwork", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\MRT", "DontOfferThroughWUAU", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\WMI\AutoLogger\SQMLogger", "Start", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableTelemetryServices()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DiagTrack", "Start", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", "Start", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\dmwappushservice", "Start", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DcpSvc", "Start", "2", RegistryValueKind.DWord)
            Utilities.RunCommand("sc config ""DPS"" start=demand")
            Utilities.StartService("DiagTrack")
            Utilities.StartService("diagnosticshub.standardcollector.service")
            Utilities.StartService("dmwappushservice")
            Utilities.StartService("DcpSvc")
            Utilities.StartService("DPS")
        End Sub

        Friend Async Sub DisableMediaPlayerSharing()
            Utilities.StopService("WMPNetworkSvc")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WMPNetworkSvc", "Start", "4", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableMediaPlayerSharing()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WMPNetworkSvc", "Start", "2", RegistryValueKind.DWord)
            Utilities.StartService("WMPNetworkSvc")
        End Sub

        Friend Async Sub DisableNetworkThrottling()
            Dim tempInt As Int32 = Convert.ToInt32("ffffffff", 16)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "NetworkThrottlingIndex", tempInt, RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Psched", "NonBestEffortLimit", 0, RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableNetworkThrottling()
            Try
                Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Psched", "NonBestEffortLimit", 80, RegistryValueKind.DWord)
                Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile", True).DeleteValue("NetworkThrottlingIndex", False)
                Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\Dnscache\Parameters", True).DeleteValue("MaxCacheTtl", False)
                Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\Dnscache\Parameters", True).DeleteValue("MaxNegativeCacheTtl", False)
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.EnableNetworkThrottling", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Friend Async Sub DisableHomeGroup()
            Utilities.StopService("HomeGroupListener")
            Utilities.StopService("HomeGroupProvider")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\HomeGroupListener", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\HomeGroupProvider", "Start", "4", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableHomeGroup()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\HomeGroupListener", "Start", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\HomeGroupProvider", "Start", "2", RegistryValueKind.DWord)
            Utilities.StartService("HomeGroupListener")
            Utilities.StartService("HomeGroupProvider")
        End Sub

        Friend Async Sub DisablePrintService()
            Utilities.StopService("Spooler")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Spooler", "Start", "3", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnablePrintService()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Spooler", "Start", "2", RegistryValueKind.DWord)
            Utilities.StartService("Spooler")
        End Sub

        Friend Async Sub DisableSuperfetch()
            Utilities.StopService("SysMain")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SysMain", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters", "EnableSuperfetch", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters", "EnablePrefetcher", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableSuperfetch()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SysMain", "Start", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters", "EnableSuperfetch", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters", "EnablePrefetcher", "1", RegistryValueKind.DWord)
            Utilities.StartService("SysMain")
        End Sub

        Friend Async Sub EnableCompatibilityAssistant()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PcaSvc", "Start", "2", RegistryValueKind.DWord)
            Utilities.StartService("PcaSvc")
        End Sub

        Friend Async Sub DisableCompatibilityAssistant()
            Utilities.StopService("PcaSvc")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PcaSvc", "Start", "4", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableSystemRestore()
            Try

                Using p As Process = New Process()
                    p.StartInfo.CreateNoWindow = True
                    p.StartInfo.FileName = "vssadmin"
                    p.StartInfo.Arguments = "delete shadows /for=c: /all /quiet"
                    p.StartInfo.UseShellExecute = False
                    p.Start()
                    p.WaitForExit()
                    p.Close()
                End Using

            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.DisableSystemRestore", ex.Message, ex.StackTrace)
            End Try

            Utilities.StopService("VSS")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableConfig", "00000001", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableSystemRestore()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableConfig", "00000000", RegistryValueKind.DWord)
            Utilities.StartService("VSS")
        End Sub

        Friend Async Sub DisableDefender()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\MpEngine", "MpEnablePus", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "PUAProtection", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Policy Manager", "DisableScanningNetworkFiles", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "DisableRealtimeMonitoring", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows Defender\Spynet", "SpyNetReporting", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows Defender\Spynet", "SubmitSamplesConsent", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\MRT", "DontReportInfectionInformation", "1", RegistryValueKind.DWord)
            Registry.ClassesRoot.DeleteSubKeyTree("\CLSID\{09A47860-11B0-4DA5-AFA5-26D86198A780}", False)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "1", RegistryValueKind.DWord)
            Dim k As RegistryKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64)

            Using tmp As RegistryKey = k.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                tmp.DeleteValue("WindowsDefender", False)
                tmp.DeleteValue("SecurityHealth", False)
            End Using

            Dim rootPath As String

            If Environment.Is64BitOperatingSystem Then
                rootPath = Environment.ExpandEnvironmentVariables("%ProgramW6432%")
            Else
                rootPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            End If

            Utilities.RunCommand("regsvr32 /u /s """ & rootPath & """")
            Utilities.RunCommand("Gpupdate /Force")
        End Sub

        Friend Async Sub EnableDefender()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\MpEngine", "MpEnablePus", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "PUAProtection", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Policy Manager", "DisableScanningNetworkFiles", "0", RegistryValueKind.DWord)
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows Defender", True).DeleteValue("DisableRealtimeMonitoring", False)
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows Defender", True).DeleteValue("DisableAntiSpyware", False)
            Registry.LocalMachine.OpenSubKey("Software\Policies\Microsoft\Windows Defender\Spynet", True).DeleteValue("SpyNetReporting", False)
            Registry.LocalMachine.OpenSubKey("Software\Policies\Microsoft\Windows Defender\Spynet", True).DeleteValue("SubmitSamplesConsent", False)
            Registry.LocalMachine.OpenSubKey("Software\Policies\Microsoft\MRT", True).DeleteValue("DontReportInfectionInformation", False)
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", True).DeleteValue("DisableBehaviorMonitoring", False)
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", True).DeleteValue("DisableOnAccessProtection", False)
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", True).DeleteValue("DisableScanOnRealtimeEnable", False)
            Utilities.RunCommand("Gpupdate /Force")
        End Sub

        Friend Async Sub DisableErrorReporting()
            Utilities.StopService("WerSvc")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WerSvc", "Start", "4", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableErrorReporting()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WerSvc", "Start", "2", RegistryValueKind.DWord)
            Utilities.StartService("WerSvc")
        End Sub

        Friend Async Sub EnableDarkThemeSys()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes", "SystemUsesLightTheme", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableDarkThemeApp()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes", "AppsUseLightTheme", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableLightThemeSys()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes", "SystemUsesLightTheme", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableLightThemeApp()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes", "AppsUseLightTheme", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableLegacyVolumeSlider()
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", "00000000", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableLegacyVolumeSlider()
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", "00000001", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableTaskbarColor()
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", "00000001", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", "00000000", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableTaskbarColor()
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", "00000000", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", "00000001", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableTransparency()
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableTransparency()
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Function UninstallOneDrive() As Task(Of Boolean)
            Utilities.RunBatchFile(ScriptsFolder & "OneDrive_Uninstaller.cmd")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OneDrive", "DisableFileSyncNGSC", "1", RegistryValueKind.DWord)
            Dim oneDriveFolders As String() = {Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\OneDrive", Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) & "OneDriveTemp", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Microsoft\OneDrive", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Microsoft OneDrive"}

            For Each x As String In oneDriveFolders

                If Directory.Exists(x) Then

                    Try
                        Directory.Delete(x, True)
                    Catch ex As Exception
                        'ErrorLogger.LogError("Optimize.UninstallOneDrive", ex.Message, ex.StackTrace)
                    End Try
                End If
            Next

            Utilities.RunCommand("SCHTASKS /Delete /TN ""OneDrive Standalone Update Task"" /F")
            Utilities.RunCommand("SCHTASKS /Delete /TN ""OneDrive Standalone Update Task v2"" /F")
            Dim rootKey As String = "CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}"
            Registry.ClassesRoot.CreateSubKey(rootKey)
            Dim byteArray As Integer = BitConverter.ToInt32(BitConverter.GetBytes(&HB090010D), 0)
            Dim reg = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64)

            Try

                Using key = Registry.ClassesRoot.OpenSubKey(rootKey, True)
                    key.SetValue("System.IsPinnedToNameSpaceTree", 0, RegistryValueKind.DWord)
                End Using

                Using key = Registry.ClassesRoot.OpenSubKey(rootKey & "\ShellFolder", True)

                    If key IsNot Nothing Then
                        key.SetValue("Attributes", byteArray, RegistryValueKind.DWord)
                    End If
                End Using

                Dim reg2 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)

                Using key = reg2.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
                    key.DeleteValue("OneDriveSetup", False)
                End Using

                If Environment.Is64BitOperatingSystem Then

                    Using key = reg.OpenSubKey(rootKey, True)

                        If key IsNot Nothing Then
                            key.SetValue("System.IsPinnedToNameSpaceTree", 0, RegistryValueKind.DWord)
                        End If
                    End Using

                    Using key = reg.OpenSubKey(rootKey & "\ShellFolder", True)

                        If key IsNot Nothing Then
                            key.SetValue("Attributes", byteArray, RegistryValueKind.DWord)
                        End If
                    End Using
                End If
                Return True
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.UninstallOneDrive", ex.Message, ex.StackTrace)
                Return False
            End Try
        End Function

        Friend Async Function InstallOneDrive() As Task(Of Boolean)
            Try
                Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OneDrive", "DisableFileSyncNGSC", "0", RegistryValueKind.DWord)
                Dim oneDriveInstaller As String

                If Environment.Is64BitOperatingSystem Then
                    oneDriveInstaller = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Windows\SysWOW64\OneDriveSetup.exe")
                Else
                    oneDriveInstaller = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Windows\System32\OneDriveSetup.exe")
                End If

                Process.Start(oneDriveInstaller)
                Return True
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.InstallOneDrive", ex.Message, ex.StackTrace)
                Return False
            End Try
        End Function

        Friend Async Sub DisableCortana()
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowCortanaButton", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\SearchSettings", "IsDeviceSearchHistoryEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCortana", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "DisableWebSearch", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "ConnectedSearchUseWeb", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "ConnectedSearchUseWebOverMeteredConnections", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowCortanaButton", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", "HistoryViewEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", "DeviceHistoryEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "AllowSearchToUseLocation", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "BingSearchEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "CortanaConsent", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCloudSearch", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableCortana()
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowCortanaButton", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCortana", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "DisableWebSearch", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "ConnectedSearchUseWeb", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "ConnectedSearchUseWebOverMeteredConnections", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowCortanaButton", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", "HistoryViewEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", "DeviceHistoryEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "AllowSearchToUseLocation", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "BingSearchEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "CortanaConsent", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCloudSearch", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableXboxLive()
            Utilities.StopService("XboxNetApiSvc")
            Utilities.StopService("XblAuthManager")
            Utilities.StopService("XblGameSave")
            Utilities.StopService("XboxGipSvc")
            Utilities.StopService("xbgm")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers", "HwSchMode", "00000002", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XboxNetApiSvc", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XblAuthManager", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XblGameSave", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XboxGipSvc", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\xbgm", "Start", "4", RegistryValueKind.DWord)

            Try

                If Not File.Exists(ScriptsFolder & "DisableXboxTasks.bat") Then
                    File.WriteAllText(ScriptsFolder & "DisableXboxTasks", My.Resources.DisableXboxTasks)
                End If

            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.EnableXboxLive", ex.Message, ex.StackTrace)
            End Try

            Utilities.RunBatchFile(ScriptsFolder & "DisableXboxTasks.bat")
        End Sub

        Friend Async Sub EnableXboxLive()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XboxNetApiSvc", "Start", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XblAuthManager", "Start", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XblGameSave", "Start", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XboxGipSvc", "Start", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\xbgm", "Start", "2", RegistryValueKind.DWord)

            Try

                If Not File.Exists(ScriptsFolder & "EnableXboxTasks.bat") Then
                    File.WriteAllText(ScriptsFolder & "EnableXboxTasks.bat", My.Resources.EnableXboxTasks)
                End If

            Catch ex As Exception
                ''ErrorLogger.LogError("Optimize.EnableXboxLive", ex.Message, ex.StackTrace)
            End Try

            Utilities.RunBatchFile(ScriptsFolder & "EnableXboxTasks.bat")
        End Sub

        Friend Async Sub DisableAutomaticUpdates()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\DeliveryOptimization", "SystemSettingsDownloadMode", "3", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_USERS\S-1-5-20\Software\Microsoft\Windows\CurrentVersion\DeliveryOptimization\Settings", "DownloadMode", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings", "UxOption", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", "NoAutoUpdate", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", "AUOptions", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", "NoAutoRebootWithLoggedOnUsers", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization\Config", "DODownloadMode", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DoSvc", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SilentInstalledAppsEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableSoftLanding", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "PreInstalledAppsEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableWindowsConsumerFeatures", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "OemPreInstalledAppsEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\WindowsStore", "AutoDownload", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\Maintenance", "MaintenanceDisabled", "1", RegistryValueKind.DWord)
            Utilities.StopService("DoSvc")
        End Sub

        Friend Async Sub EnableAutomaticUpdates()
            Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\DeliveryOptimization", True).DeleteValue("SystemSettingsDownloadMode", False)
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\WindowsUpdate\UX\Settings", True).DeleteValue("UxOption", False)
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", True).DeleteValue("AUOptions", False)
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", True).DeleteValue("NoAutoUpdate", False)
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", True).DeleteValue("NoAutoRebootWithLoggedOnUsers", False)
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization\Config", True).DeleteValue("DODownloadMode", False)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DoSvc", "Start", "3", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\Maintenance", "MaintenanceDisabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SilentInstalledAppsEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableSoftLanding", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "PreInstalledAppsEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableWindowsConsumerFeatures", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "OemPreInstalledAppsEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\WindowsStore", "AutoDownload", "4", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableOneDrive()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OneDrive", "DisableFileSyncNGSC", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableOneDrive()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OneDrive", "DisableFileSyncNGSC", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableSensorServices()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SensrSvc", "Start", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SensorService", "Start", "2", RegistryValueKind.DWord)
            Utilities.StartService("SensrSvc")
            Utilities.StartService("SensorService")
        End Sub

        Friend Async Sub DisableSensorServices()
            Utilities.StopService("SensrSvc")
            Utilities.StopService("SensorService")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SensrSvc", "Start", "4", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SensorService", "Start", "4", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableTelemetryTasks()
            Utilities.RunCommand(String.Format("icacls {0} /deny SYSTEM:`(OI`)`(CI`)F", DiagnosisAutoLoggerFolder))
            DisableTelemetryRunner()
            Utilities.RunBatchFile(ScriptsFolder & "DisableTelemetryTasks.bat")
        End Sub

        Friend Async Sub EnableTelemetryTasks()
            Try

                If Not File.Exists(ScriptsFolder & "EnableTelemetryTasks.bat") Then
                    File.WriteAllText(ScriptsFolder & "EnableTelemetryTasks.bat", My.Resources.EnableTelemetryTasks)
                End If

            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.EnableTelemetryTasks", ex.Message, ex.StackTrace)
            End Try

            Utilities.RunBatchFile(ScriptsFolder & "EnableTelemetryTasks.bat")
        End Sub

        Friend Async Sub DisableOffice2016Telemetry()
            Utilities.RunBatchFile(ScriptsFolder & "DisableOfficeTelemetryTasks.bat")
            Utilities.ImportRegistryScript(ScriptsFolder & "DisableOfficeTelemetryTasks.reg")
        End Sub

        Friend Async Sub EnableOffice2016Telemetry()
            Try

                If Not File.Exists(ScriptsFolder & "EnableOfficeTelemetryTasks.reg") Then
                    File.WriteAllText(ScriptsFolder & "EnableOfficeTelemetryTasks.reg", My.Resources.EnableOfficeTelemetry)
                End If

                If Not File.Exists(ScriptsFolder & "EnableOfficeTelemetryTasks.bat") Then
                    File.WriteAllText(ScriptsFolder & "EnableOfficeTelemetryTasks.bat", My.Resources.EnableOfficeTelemetryTasks)
                End If

            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.EnableOffice2016Telemetry", ex.Message, ex.StackTrace)
            End Try

            Utilities.RunBatchFile(ScriptsFolder & "EnableOfficeTelemetryTasks.bat")
            Utilities.ImportRegistryScript(ScriptsFolder & "EnableOfficeTelemetryTasks.reg")
        End Sub

        Friend Async Sub EnhancePrivacy()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\FindMyDevice", "AllowFindMyDevice", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Settings\FindMyDevice", "LocationSyncEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "EnableActivityFeed", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "EnableCdp", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Privacy", "TailoredExperiencesWithDiagnosticDataEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\Privacy", "TailoredExperiencesWithDiagnosticDataEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Diagnostics\DiagTrack", "ShowedToastAtLevel", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\CurrentVersion\Diagnostics\DiagTrack", "ShowedToastAtLevel", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Speech_OneCore\Settings\OnlineSpeechPrivacy", "HasAccepted", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location", "Value", "Deny", RegistryValueKind.String)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Settings\FindMyDevice", "LocationSyncEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Sensor\Overrides\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}", "SensorPermissionState", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\lfsvc\Service\Configuration", "Status", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Biometrics", "Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Feeds", "ShellFeedsTaskbarOpenOnHover", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CDP", "CdpSessionUserAuthzPolicy", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CDP", "NearShareChannelUserAuthzPolicy", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CDP", "RomeSdkChannelUserAuthzPolicy", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows NT\CurrentVersion\Software Protection Platform", "NoGenTicket", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost", "EnableWebContentEvaluation", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost\EnableWebContentEvaluation", "Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International\User Profile", "HttpAcceptLanguageOptOut", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SmartGlass", "UserAuthPolicy", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Personalization\Settings", "AcceptedPrivacyPolicy", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", "Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\InputPersonalization", "RestrictImplicitTextCollection", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\InputPersonalization", "RestrictImplicitInkCollection", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\InputPersonalization\TrainedDataStore", "HarvestContacts", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Input\TIPC", "Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\AppPrivacy", "LetAppsSyncWithDevices", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\LooselyCoupled", "Value", "Deny", RegistryValueKind.String)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", "MaxTelemetryAllowed", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "UploadUserActivities", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Siuf\Rules", "PeriodInNanoSeconds", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Siuf\Rules", "NumberOfSIUFInPeriod", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", "AllowTelemetry", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\DataCollection", "AllowTelemetry", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\WMI\AutoLogger\AutoLogger-Diagtrack-Listener", "Start", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\WMI\AutoLogger\AutoLogger-Diagtrack-Listener", "Start", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config", "AutoConnectAllowedOEM", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\Tethering", "Hotspot2SignUp", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WlanSvc\AnqpCache", "OsuRegistrationStatus", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\Tethering", "RemoteStartupDisabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Connect", "AllowProjectionToPC", "0", RegistryValueKind.DWord)

            ' If Utilities.CurrentWindowsVersion = WindowsVersion.Windows10 AndAlso Utilities.GetOS().ToLowerInvariant().Contains("home") Then
            EnableGPEDitor()
            ' End If
        End Sub

        Friend Shared Async Sub EnableGPEDitor()
            Utilities.RunBatchFile(ScriptsFolder & "GPEditEnablerInHome.bat")
        End Sub

        Friend Async Sub CompromisePrivacy()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\FindMyDevice", "AllowFindMyDevice", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Settings\FindMyDevice", "LocationSyncEnabled", "1", RegistryValueKind.DWord)

            Try
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\System", True).DeleteValue("EnableActivityFeed", False)
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.CompromisePrivacy", ex.Message, ex.StackTrace)
            End Try

            Try
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\System", True).DeleteValue("EnableCdp", False)
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.CompromisePrivacy", ex.Message, ex.StackTrace)
            End Try

            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Sensor\Overrides\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}", "SensorPermissionState", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\lfsvc\Service\Configuration", "Status", "1", RegistryValueKind.DWord)

            Try
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Biometrics", True).DeleteValue("Enabled", False)
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.CompromisePrivacy", ex.Message, ex.StackTrace)
            End Try

            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows NT\CurrentVersion\Software Protection Platform", "NoGenTicket", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost", "EnableWebContentEvaluation", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost\EnableWebContentEvaluation", "Enabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International\User Profile", "HttpAcceptLanguageOptOut", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SmartGlass", "UserAuthPolicy", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\AppPrivacy", "LetAppsSyncWithDevices", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\LooselyCoupled", "Value", "Allow", RegistryValueKind.String)

            Try
                Registry.CurrentUser.OpenSubKey("Software\Microsoft\Siuf\Rules", True).DeleteValue("PeriodInNanoSeconds", False)
                Registry.CurrentUser.OpenSubKey("Software\Microsoft\Siuf\Rules", True).DeleteValue("NumberOfSIUFInPeriod", False)
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.CompromisePrivacy", ex.Message, ex.StackTrace)
            End Try

            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", "AllowTelemetry", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\DataCollection", "AllowTelemetry", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\WMI\AutoLogger\AutoLogger-Diagtrack-Listener", "Start", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\WMI\AutoLogger\AutoLogger-Diagtrack-Listener", "Start", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config", "AutoConnectAllowedOEM", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\Tethering", "Hotspot2SignUp", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WlanSvc\AnqpCache", "OsuRegistrationStatus", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\Tethering", "RemoteStartupDisabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Connect", "AllowProjectionToPC", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableGameBar()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AutoGameModeEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AppCaptureEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AudioCaptureEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "CursorCaptureEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\GameBar", "UseNexusForGameBarEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\GameBar", "ShowStartupPanel", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AllowAutoGameMode", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\GameDVR", "AllowGameDVR", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableGameBar()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AutoGameModeEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AppCaptureEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AudioCaptureEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "CursorCaptureEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\GameBar", "UseNexusForGameBarEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\GameBar", "ShowStartupPanel", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AllowAutoGameMode", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_Enabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\GameDVR", "AllowGameDVR", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableQuickAccessHistory()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\OperationStatusManager", "EnthusiastMode", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSyncProviderNotifications", "0", RegistryValueKind.DWord)

            Using k As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer", True)
                k.SetValue("ShowFrequent", 0, RegistryValueKind.DWord)
                k.SetValue("ShowRecent", 0, RegistryValueKind.DWord)
            End Using

            Using k As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", True)
                k.SetValue("LaunchTo", 1, RegistryValueKind.DWord)
            End Using

            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "HideSCAMeetNow", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", "HideSCAMeetNow", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\FileHistory", "Disabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Feeds", "ShellFeedsTaskbarViewMode", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "SearchboxTaskbarMode", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowTaskViewButton", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableQuickAccessHistory()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\OperationStatusManager", "EnthusiastMode", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSyncProviderNotifications", "1", RegistryValueKind.DWord)

            Using k As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer", True)
                k.SetValue("ShowFrequent", 1, RegistryValueKind.DWord)
                k.SetValue("ShowRecent", 1, RegistryValueKind.DWord)
            End Using

            Using k As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", True)
                k.SetValue("LaunchTo", 2, RegistryValueKind.DWord)
                k.DeleteValue("ShowTaskViewButton", False)
            End Using

            Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\FileHistory", True).DeleteValue("Disabled", False)
            Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Search", True).DeleteValue("SearchboxTaskbarMode", False)
            Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Feeds", True).DeleteValue("ShellFeedsTaskbarViewMode", False)
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", True).DeleteValue("HideSCAMeetNow", False)
            Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", True).DeleteValue("HideSCAMeetNow", False)
        End Sub

        Friend Async Sub DisableStartMenuAds()
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\UserProfileEngagement", "ScoobeSystemSettingEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "ContentDeliveryAllowed", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "PreInstalledAppsEverEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SilentInstalledAppsEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-314559Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338387Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338389Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SystemPaneSuggestionsEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338393Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-353694Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-353696Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-310093Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338388Enabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContentEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SoftLandingEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "FeatureManagementEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "DisableSearchBoxSuggestions", 1, RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableStartMenuAds()
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\UserProfileEngagement", "ScoobeSystemSettingEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "ContentDeliveryAllowed", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "PreInstalledAppsEverEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SilentInstalledAppsEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-314559Enabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338387Enabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338389Enabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SystemPaneSuggestionsEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338393Enabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-353694Enabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-353696Enabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-310093Enabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContent-338388Enabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SubscribedContentEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SoftLandingEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "FeatureManagementEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "DisableSearchBoxSuggestions", 0, RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableMyPeople()
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced\People", "PeopleBand", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableMyPeople()
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced\People", "PeopleBand", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub ExcludeDrivers()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "ExcludeWUDriversInQualityUpdate", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings", "ExcludeWUDriversInQualityUpdate", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\Update\ExcludeWUDriversInQualityUpdate", "value", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\Update", "ExcludeWUDriversInQualityUpdate", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\current\device\Update", "ExcludeWUDriversInQualityUpdate", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub IncludeDrivers()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "ExcludeWUDriversInQualityUpdate", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings", "ExcludeWUDriversInQualityUpdate", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\Update\ExcludeWUDriversInQualityUpdate", "value", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\Update", "ExcludeWUDriversInQualityUpdate", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\current\device\Update", "ExcludeWUDriversInQualityUpdate", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableWindowsInk()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\WindowsInkWorkspace", "AllowWindowsInkWorkspace", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\WindowsInkWorkspace", "AllowSuggestedAppsInWindowsInkWorkspace", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnableInkingWithTouch", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableWindowsInk()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\WindowsInkWorkspace", "AllowWindowsInkWorkspace", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\WindowsInkWorkspace", "AllowSuggestedAppsInWindowsInkWorkspace", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnableInkingWithTouch", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableSpellingAndTypingFeatures()
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnableAutocorrection", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnableSpellchecking", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Input\Settings", "InsightsEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnableDoubleTapSpace", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnablePredictionSpaceInsertion", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnableTextPrediction", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableSpellingAndTypingFeatures()
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnableAutocorrection", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnableSpellchecking", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Input\Settings", "InsightsEnabled", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnableDoubleTapSpace", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnablePredictionSpaceInsertion", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\TabletTip\1.7", "EnableTextPrediction", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableFaxService()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Fax", "Start", "3", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableFaxService()
            Utilities.StopService("Fax")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Fax", "Start", "4", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableInsiderService()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\wisvc", "Start", "3", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableInsiderService()
            Utilities.StopService("wisvc")
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\wisvc", "Start", "4", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableForcedFeatureUpdates()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "DisableOSUpgrade", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\OSUpgrade", "AllowOSUpgrade", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\OSUpgrade", "ReservationsAllowed", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\WindowsStore", "DisableOSUpgrade", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\Setup\UpgradeNotification", "UpgradeAvailable", "0", RegistryValueKind.DWord)

            Try
                Dim buildNumber As String = CStr(Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", String.Empty))
                If Not String.IsNullOrEmpty(buildNumber) Then Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "TargetReleaseVersionInfo", buildNumber, RegistryValueKind.String)
                Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "TargetReleaseVersion", "1", RegistryValueKind.DWord)
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.DisableForcedFeatureUpdates", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Friend Async Sub EnableForcedFeatureUpdates()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "DisableOSUpgrade", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\OSUpgrade", "AllowOSUpgrade", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\OSUpgrade", "ReservationsAllowed", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\WindowsStore", "DisableOSUpgrade", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\Setup\UpgradeNotification", "UpgradeAvailable", "1", RegistryValueKind.DWord)

            Try
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", True).DeleteValue("TargetReleaseVersionInfo", False)
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", True).DeleteValue("TargetReleaseVersion", False)
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.EnableForcedFeatureUpdates", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Friend Async Sub DisableSmartScreen()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Attachments", "SaveZoneInformation", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Attachments", "ScanWithAntiVirus", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "ShellSmartScreenLevel", "Warn", RegistryValueKind.String)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "EnableSmartScreen", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", "SmartScreenEnabled", "Off", RegistryValueKind.String)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\PhishingFilter", "EnabledV9", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableSmartScreen()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Attachments", "SaveZoneInformation", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Attachments", "ScanWithAntiVirus", "2", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "EnableSmartScreen", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", "SmartScreenEnabled", "On", RegistryValueKind.String)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\PhishingFilter", "EnabledV9", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableCloudClipboard()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "AllowClipboardHistory", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "AllowCrossDeviceClipboard", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Clipboard", "EnableClipboardHistory", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Clipboard", "EnableClipboardHistory", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableCloudClipboard()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "AllowClipboardHistory", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "AllowCrossDeviceClipboard", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Clipboard", "EnableClipboardHistory", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Clipboard", "EnableClipboardHistory", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableLongPaths()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem", "LongPathsEnabled", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableLongPaths()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem", "LongPathsEnabled", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableStickyKeys()
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Accessibility\StickyKeys", "Flags", "506", RegistryValueKind.String)
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response", "Flags", "122", RegistryValueKind.String)
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Accessibility\ToggleKeys", "Flags", "58", RegistryValueKind.String)
            Registry.SetValue("HKEY_USERS\.DEFAULT\Control Panel\Accessibility\StickyKeys", "Flags", "506", RegistryValueKind.String)
            Registry.SetValue("HKEY_USERS\.DEFAULT\Control Panel\Accessibility\Keyboard Response", "Flags", "122", RegistryValueKind.String)
            Registry.SetValue("HKEY_USERS\.DEFAULT\Control Panel\Accessibility\ToggleKeys", "Flags", "58", RegistryValueKind.String)
        End Sub

        Friend Async Sub EnableStickyKeys()
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Accessibility\StickyKeys", "Flags", "510", RegistryValueKind.String)
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response", "Flags", "126", RegistryValueKind.String)
            Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Accessibility\ToggleKeys", "Flags", "62", RegistryValueKind.String)
            Registry.SetValue("HKEY_USERS\.DEFAULT\Control Panel\Accessibility\StickyKeys", "Flags", "510", RegistryValueKind.String)
            Registry.SetValue("HKEY_USERS\.DEFAULT\Control Panel\Accessibility\Keyboard Response", "Flags", "126", RegistryValueKind.String)
            Registry.SetValue("HKEY_USERS\.DEFAULT\Control Panel\Accessibility\ToggleKeys", "Flags", "62", RegistryValueKind.String)
        End Sub

        Friend Async Sub RemoveCastToDevice()
            Try
                Utilities.RunCommand("REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Shell Extensions\Blocked"" /V {7AD84985-87B4-4a16-BE58-8B72A5B390F7} /T REG_SZ /D ""Play to Menu"" /F")
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.RemoveCastToDevice", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Friend Async Sub AddCastToDevice()
            Try
                Utilities.RunCommand("REG Delete ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Shell Extensions\Blocked"" /V {7AD84985-87B4-4a16-BE58-8B72A5B390F7} /F")
            Catch ex As Exception
                'ErrorLogger.LogError("Optimize.AddCastToDevice", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Friend Async Sub DisableActionCenter()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\PushNotifications", "ToastEnabled", "0", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer", "DisableNotificationCenter", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableActionCenter()
            Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\Explorer", True).DeleteValue("DisableNotificationCenter", False)
            Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\PushNotifications", True).DeleteValue("ToastEnabled", False)
        End Sub

        Friend Async Sub AlignTaskbarToLeft()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarAl", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub AlignTaskbarToCenter()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarAl", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableSnapAssist()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "EnableSnapAssistFlyout", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableSnapAssist()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "EnableSnapAssistFlyout", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableWidgets()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarDa", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableWidgets()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarDa", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableChat()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarMn", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableChat()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarMn", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub SmallerTaskbar()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarSi", "0", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DefaultTaskbarSize()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "TaskbarSi", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub DisableShowMoreOptions()
            Registry.SetValue("HKEY_CURRENT_USER\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32", "", "")
        End Sub

        Friend Async Sub EnableShowMoreOptions()
            Registry.CurrentUser.DeleteSubKeyTree("Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}", False)
        End Sub

        Friend Async Sub DisableTPMCheck()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\Setup\MoSetup", "AllowUpgradesWithUnsupportedTPMOrCPU", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\Setup\LabConfig", "BypassTPMCheck", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\Setup\LabConfig", "BypassRAMCheck", "1", RegistryValueKind.DWord)
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\Setup\LabConfig", "BypassSecureBootCheck", "1", RegistryValueKind.DWord)
        End Sub

        Friend Async Sub EnableTPMCheck()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SYSTEM\Setup\MoSetup", "AllowUpgradesWithUnsupportedTPMOrCPU", "0", RegistryValueKind.DWord)
            Registry.LocalMachine.OpenSubKey("SYSTEM\Setup\LabConfig", True).DeleteValue("BypassTPMCheck", False)
            Registry.LocalMachine.OpenSubKey("SYSTEM\Setup\LabConfig", True).DeleteValue("BypassRAMCheck", False)
            Registry.LocalMachine.OpenSubKey("SYSTEM\Setup\LabConfig", True).DeleteValue("BypassSecureBootCheck", False)
        End Sub

        Friend Async Sub EnableFileExplorerClassicRibbon()
            Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Shell Extensions\Blocked", "{e2bf9676-5f8f-435c-97eb-11607a5bedf7}", "")
        End Sub

        Friend Async Sub DisableFileExplorerClassicRibbon()
            Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Shell Extensions\Blocked", True).DeleteValue("{e2bf9676-5f8f-435c-97eb-11607a5bedf7}", False)
        End Sub
    End Class

End Namespace

