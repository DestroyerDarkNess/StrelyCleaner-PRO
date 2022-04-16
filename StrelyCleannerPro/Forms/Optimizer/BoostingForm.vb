Public Class BoostingForm

#Region " Declare "

    Private Optimize As New Core.Optimizer.Optimize
    Private ProcessListScrool As Core.ScrollManager

#End Region

    Public Sub New()

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub BoostingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Me.Refresh()
        DioMarqueeProgressBar1.Start()
        ProcessListScrool = New Core.ScrollManager(PanelContainer, {Guna2VScrollBar1}, True)
        StartUI()
    End Sub

#Region " GUI "

    Public Sub StartUI()
        ListOptions()
    End Sub

    Private Sub BoostingForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

#End Region

#Region " Lister Options "

    Public ProcessListener As Boolean = False
    Private ListenerProcess As New ControlLister With {.OrientationControls = Orientation.Vertical, .Margen = New Point(20, 10)}

    Public Sub ListOptions()

        If ProcessListener = False Then

            ProcessListener = True
            PanelContainer.Controls.Clear()

            Dim BoostOp As List(Of BoosterItem) = GetOptions()

            Dim Asynctask As New Task(New Action(Async Sub()



                                                     Dim ID As Integer = 0
                                                     For Each ImInfo As BoosterItem In BoostOp
                                                         Application.DoEvents()

                                                         Try
                                                             Me.BeginInvoke(Sub()
                                                                                ImInfo.Name = ID
                                                                                ListenerProcess.Add(PanelContainer, ImInfo, False)
                                                                            End Sub)
                                                             ID += 1
                                                         Catch ex As Exception

                                                         End Try

                                                     Next

                                                     Me.BeginInvoke(Sub()
                                                                        Dim ReadIni As String = Core.Utils.ReadIni("BossterOption", "Boost", String.Empty)
                                                                        If ReadIni = String.Empty Then Core.Utils.WriteIni("BossterOption", "Boost", "On")

                                                                        If ReadIni = "Off" Then
                                                                            Guna2Button2.Checked = True
                                                                        Else
                                                                            Guna2Button1.Checked = True
                                                                        End If

                                                                        If Panel1.Visible = False Then
                                                                            DioMarqueeProgressBar1.Stop()
                                                                            DioMarqueeProgressBar1.Visible = False
                                                                            Panel1.Visible = True
                                                                        End If
                                                                    End Sub)

                                                     ProcessListener = False

                                                 End Sub), TaskCreationOptions.PreferFairness)

            Asynctask.Start()

        End If

    End Sub

    Private Function GetOptions() As List(Of BoosterItem)

        Dim Result As New List(Of BoosterItem)

        Dim EnablePerformanceTweaks As New BoosterItem() With {.OptionName = "EnablePerformanceTweaks", .Visible = False}
        EnablePerformanceTweaks.Description = "To adjust visual effects in Windows, type performance in the search bar and then select Adjust the appearance and performance of Windows. Select Adjust for best performance on the Visual Effects tab and then select Apply."

        Dim DisableTransparency As New BoosterItem() With {.OptionName = "DisableTransparency", .Visible = False}
        DisableTransparency.Description = "To Disable Transparency in Windows, type performance in the search bar and then select Adjust the appearance and performance of Windows. Select Adjust for best performance on the Visual Effects tab and then select Apply."

        Dim DisableTaskbarColor As New BoosterItem() With {.OptionName = "DisableTaskbarColor", .Visible = False}
        DisableTaskbarColor.Description = "To Disable Taskbar Color in Windows, type performance in the search bar and then select Adjust the appearance and performance of Windows. Select Adjust for best performance on the Visual Effects tab and then select Apply."

        Dim DisableNetworkThrottling As New BoosterItem() With {.OptionName = "DisableNetworkThrottling", .Visible = False}
        DisableNetworkThrottling.Description = "Windows implements a network throttling mechanism, the idea behind such throttling is that processing of network packets can be a resource-intensive task. It is beneficial to turn off such throttling for achieving maximum throughput."

        Dim DisableLegacyVolumeSlider As New BoosterItem() With {.OptionName = "DisableLegacyVolumeSlider", .Visible = False}
        DisableLegacyVolumeSlider.Description = "In most cases, the change to the old volume slider takes effect immediately, so click on the volume icon in your Windows taskbar to see if it worked."

        Dim DisableOneDrive As New BoosterItem() With {.OptionName = "DisableOneDrive", .Visible = False}
        DisableOneDrive.Description = "OneDrive is a Microsoft cloud storage service that lets you store your personal files in one place, share them with others, and get to them from any device connected to the Internet."

        Dim DisableSensorServices As New BoosterItem() With {.OptionName = "DisableSensorServices", .Visible = False}
        DisableSensorServices.Description = "Defaults in Windows A service for sensors that manages different sensors' functionality. Manages Simple Device Orientation (SDO) and History for sensors. Loads the SDO sensor that reports device orientation changes."

        Dim DisableGameBar As New BoosterItem() With {.OptionName = "DisableGameBar", .Visible = False}
        DisableGameBar.Description = "The Windows Game Bar is an underrated feature which allows you to easily screenshot, record, and stream directly from a game, Turning it off Can improve gaming performance by 5%. "

        Dim DisableQuickAccessHistory As New BoosterItem() With {.OptionName = "DisableQuickAccessHistory", .Visible = False}
        DisableQuickAccessHistory.Description = "The Quick Access feature in Windows lets you access frequently used files and folders easily. Turning it off You can improve the response time of Windows Explorer."

        Dim DisableStartMenuAds As New BoosterItem() With {.OptionName = "DisableStartMenuAds", .Visible = False}
        DisableStartMenuAds.Description = "Greatly improves the response time of the Start Menu."

        Dim DisableMyPeople As New BoosterItem() With {.OptionName = "DisableMyPeople", .Visible = False}
        DisableMyPeople.Description = "Starting with Windows 10 build 16184, there is a new People button in the taskbar notification area. Whether you want to share a photo, make dinner plans, or get a response from a friend or co-worker, that's My People."

        Dim DisableWindowsDefender As New BoosterItem() With {.OptionName = "DisableWindowsDefender", .Visible = False}
        DisableWindowsDefender.Description = "Windows Defender is the default antivirus for Windows. It is disabled because it slows down your computer. (Don't be discouraged StrelyCleaner comes with an antivirus engine for you)"

        Dim DisableSystemRestore As New BoosterItem() With {.OptionName = "DisableSystemRestore", .Visible = False}
        DisableSystemRestore.Description = "With Windows System Restore, you can undo some changes made to Windows during installations, updates and other events in case post-event errors or problems arise."

        Dim DisablePrintService As New BoosterItem() With {.OptionName = "DisablePrintService", .Visible = False}
        DisablePrintService.Description = "Es una pequeña aplicación que administra trabajos de impresión en papel enviados desde la computadora a la impresora o servidor de impresión."

        Dim DisableMediaPlayerSharing As New BoosterItem() With {.OptionName = "DisableMediaPlayerSharing", .Visible = False}
        DisableMediaPlayerSharing.Description = "Media streaming (media sharing) allows you to send your music, pictures, and videos to other computers and devices on your same home or work network. "

        Dim DisableErrorReporting As New BoosterItem() With {.OptionName = "DisableErrorReporting", .Visible = False}
        DisableErrorReporting.Description = "The error reporting feature enables users to notify Microsoft of application faults, kernel faults, unresponsive applications, and other application specific problems."

        Dim DisableHomeGroup As New BoosterItem() With {.OptionName = "DisableHomeGroup", .Visible = False}
        DisableHomeGroup.Description = "The Homegroup is a group of Windows computers and devices connected to the same LAN (Local Area Network) that can share content and connected devices. "

        Dim DisableSuperfetch As New BoosterItem() With {.OptionName = "DisableSuperfetch", .Visible = False}
        DisableSuperfetch.Description = "Superfetch is a feature that was introduced back in Windows Vista. The official description of the Superfetch service says that it 'maintains and improves system performance over time,' but that's vague and doesn't explain the whole story."

        Dim DisableTelemetryTasks As New BoosterItem() With {.OptionName = "DisableTelemetryTasks", .Visible = False}
        DisableTelemetryTasks.Description = "Periodically sends usage and performance data to select Microsoft IP addresses. Microsoft says that telemetry helps improve the user experience and troubleshoot potential issues. This obviously raises privacy concerns for many users."

        Dim DisableOffice2016Telemetry As New BoosterItem() With {.OptionName = "DisableOffice2016Telemetry", .Visible = False}
        DisableOffice2016Telemetry.Description = "Office telemetry is a 'Compatibility Monitoring Framework' for administrators to identify business-critical Office documents and add-ins, and test compatibility and performance with new versions of Office as they are released."

        Dim DisableCompatibilityAssistant As New BoosterItem() With {.OptionName = "DisableCompatibilityAssistant", .Visible = False}
        DisableCompatibilityAssistant.Description = "Detects known compatibility issues and notifies the user if there is an issue and offers to remedy the issue the next time the program is run."

        Dim DisableFaxService As New BoosterItem() With {.OptionName = "DisableFaxService", .Visible = False}
        DisableFaxService.Description = "The fax service, a Telephony Application Programming Interface (TAPI)-compliant system service, allows users to send and receive faxes from their desktop applications using either a local fax device or a shared network fax device. The service also offers the following features: Routing of inbound faxes Server and device configuration management."

        Dim DisableSmartScreen As New BoosterItem() With {.OptionName = "DisableSmartScreen", .Visible = False}
        DisableSmartScreen.Description = "SmartScreen (officially called Windows SmartScreen, Windows Defender SmartScreen and SmartScreen Filter in different places) is a cloud-based anti-phishing and anti-malware component included in several Microsoft products, including Windows."

        Dim DisableStickyKeys As New BoosterItem() With {.OptionName = "DisableStickyKeys", .Visible = False}
        DisableStickyKeys.Description = "Sticky Keys is a Microsoft Windows accessibility feature that causes modifier keys to remain active, even after they were pressed and released, making it easier to use keyboard shortcuts. "

        Result.AddRange({EnablePerformanceTweaks, DisableTransparency, DisableTaskbarColor, DisableNetworkThrottling, DisableLegacyVolumeSlider,
                       DisableOneDrive, DisableSensorServices, DisableGameBar, DisableQuickAccessHistory, DisableStartMenuAds,
                     DisableMyPeople, DisableWindowsDefender, DisableSystemRestore,
                         DisablePrintService, DisableMediaPlayerSharing, DisableErrorReporting, DisableHomeGroup,
                         DisableSuperfetch, DisableTelemetryTasks, DisableOffice2016Telemetry, DisableCompatibilityAssistant,
                         DisableFaxService, DisableSmartScreen, DisableStickyKeys})

        Return Result
    End Function

#End Region

    Private Sub Guna2Button1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2Button1.CheckedChanged
        If Guna2Button1.Checked = True Then
            Core.Utils.WriteIni("BossterOption", "Boost", "On")
            CircularProgress1.Text = "Optimize"
            CircularProgress1.ProgressColor = Color.SpringGreen
            CircularProgress1.Refresh()

            For Each BOption2 As Control In PanelContainer.Controls
                If TypeOf BOption2 Is BoosterItem Then
                    Dim BOptionEx As BoosterItem = DirectCast(BOption2, BoosterItem)
                    BOptionEx.SetSwitch(True)
                End If
            Next
        End If
    End Sub

    Private Sub Guna2Button2_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2Button2.CheckedChanged
        If Guna2Button2.Checked = True Then
            Core.Utils.WriteIni("BossterOption", "Boost", "Off")
            CircularProgress1.Text = "Deoptimize"
            CircularProgress1.ProgressColor = Color.Red
            CircularProgress1.Refresh()
            For Each BOption2 As Control In PanelContainer.Controls
                If TypeOf BOption2 Is BoosterItem Then
                    Dim BOptionEx As BoosterItem = DirectCast(BOption2, BoosterItem)
                    BOptionEx.SetSwitch(False)
                End If
            Next
        End If
    End Sub

    Private Sub Optimized()

        Dim ControlsList As Control.ControlCollection = PanelContainer.Controls
        CircularProgress1.ProgressColor = Color.SpringGreen
        Dim MaxVal As Integer = ControlsList.Count

        Dim Asynctask As New Task(New Action(Async Sub()

                                                 Dim CurrentVal As Integer = 0

                                                 For Each BOption As Control In ControlsList
                                                     CurrentVal += 1
                                                     Dim CalculateVal As Integer = Math.Round((CurrentVal * 100) / MaxVal)

                                                     Me.BeginInvoke(Sub()
                                                                        CircularProgress1.Value = CalculateVal
                                                                    End Sub)

                                                     If TypeOf BOption Is BoosterItem Then
                                                         Dim BOptionEx As BoosterItem = DirectCast(BOption, BoosterItem)
                                                         Dim OptionName As String = BOptionEx.OptionName
                                                         Dim Switch As Boolean = BOptionEx.GetSwitch

                                                         BOptionEx.IsLoading(True)

                                                         Select Case OptionName

                                                             Case "EnablePerformanceTweaks"
                                                                 If Switch Then
                                                                     Optimize.EnablePerformanceTweaks()
                                                                 Else
                                                                     Optimize.DisablePerformanceTweaks()
                                                                 End If
                                                             Case "DisableTransparency"
                                                                 If Switch Then
                                                                     Optimize.DisableTransparency()
                                                                 Else
                                                                     Optimize.EnableTransparency()
                                                                 End If
                                                             Case "DisableTaskbarColor"
                                                                 If Switch Then
                                                                     Optimize.DisableTaskbarColor()
                                                                 Else
                                                                     Optimize.EnableTaskbarColor()
                                                                 End If
                                                             Case "DisableNetworkThrottling"
                                                                 If Switch Then
                                                                     Optimize.DisableNetworkThrottling()
                                                                 Else
                                                                     Optimize.EnableNetworkThrottling()
                                                                 End If
                                                             Case "DisableLegacyVolumeSlider"
                                                                 If Switch Then
                                                                     Optimize.DisableLegacyVolumeSlider()
                                                                 Else
                                                                     Optimize.EnableLegacyVolumeSlider()
                                                                 End If
                                                             Case "DisableOneDrive"
                                                                 If Switch Then
                                                                     Optimize.DisableOneDrive()
                                                                 Else
                                                                     Optimize.EnableOneDrive()
                                                                 End If
                                                             Case "DisableSensorServices"
                                                                 If Switch Then
                                                                     Optimize.DisableSensorServices()
                                                                 Else
                                                                     Optimize.EnableSensorServices()
                                                                 End If
                                                             Case "DisableGameBar"
                                                                 If Switch Then
                                                                     Optimize.DisableGameBar()
                                                                 Else
                                                                     Optimize.EnableGameBar()
                                                                 End If
                                                             Case "DisableQuickAccessHistory"
                                                                 If Switch Then
                                                                     Optimize.DisableQuickAccessHistory()
                                                                 Else
                                                                     Optimize.EnableQuickAccessHistory()
                                                                 End If
                                                             Case "DisableStartMenuAds"
                                                                 If Switch Then
                                                                     Optimize.DisableStartMenuAds()
                                                                 Else
                                                                     Optimize.EnableStartMenuAds()
                                                                 End If
                                                             Case "DisableMyPeople"
                                                                 If Switch Then
                                                                     Optimize.DisableMyPeople()
                                                                 Else
                                                                     Optimize.EnableMyPeople()
                                                                 End If
                                                             Case "DisableWindowsDefender"
                                                                 If Switch Then
                                                                     Optimize.DisableDefender()
                                                                 Else
                                                                     Optimize.EnableDefender()
                                                                 End If
                                                             Case "DisableSystemRestore"
                                                                 If Switch Then
                                                                     Optimize.DisableSystemRestore()
                                                                 Else
                                                                     Optimize.EnableSystemRestore()
                                                                 End If
                                                             Case "DisablePrintService"
                                                                 If Switch Then
                                                                     Optimize.DisablePrintService()
                                                                 Else
                                                                     Optimize.EnablePrintService()
                                                                 End If
                                                             Case "DisableMediaPlayerSharing"
                                                                 If Switch Then
                                                                     Optimize.DisableMediaPlayerSharing()
                                                                 Else
                                                                     Optimize.EnableMediaPlayerSharing()
                                                                 End If
                                                             Case "DisableErrorReporting"
                                                                 If Switch Then
                                                                     Optimize.DisableErrorReporting()
                                                                 Else
                                                                     Optimize.EnableErrorReporting()
                                                                 End If
                                                             Case "DisableHomeGroup"
                                                                 If Switch Then
                                                                     Optimize.DisableHomeGroup()
                                                                 Else
                                                                     Optimize.EnableHomeGroup()
                                                                 End If
                                                             Case "DisableSuperfetch"
                                                                 If Switch Then
                                                                     Optimize.DisableSuperfetch()
                                                                 Else
                                                                     Optimize.EnableSuperfetch()
                                                                 End If
                                                             Case "DisableTelemetryTasks"
                                                                 If Switch Then
                                                                     Optimize.DisableTelemetryTasks()
                                                                 Else
                                                                     Optimize.EnableTelemetryTasks()
                                                                 End If
                                                             Case "DisableOffice2016Telemetry"
                                                                 If Switch Then
                                                                     Optimize.DisableOffice2016Telemetry()
                                                                 Else
                                                                     Optimize.EnableOffice2016Telemetry()
                                                                 End If
                                                             Case "DisableCompatibilityAssistant"
                                                                 If Switch Then
                                                                     Optimize.DisableCompatibilityAssistant()
                                                                 Else
                                                                     Optimize.EnableCompatibilityAssistant()
                                                                 End If
                                                             Case "DisableFaxService"
                                                                 If Switch Then
                                                                     Optimize.DisableFaxService()
                                                                 Else
                                                                     Optimize.EnableFaxService()
                                                                 End If
                                                             Case "DisableSmartScreen"
                                                                 If Switch Then
                                                                     Optimize.DisableSmartScreen()
                                                                 Else
                                                                     Optimize.EnableSmartScreen()
                                                                 End If
                                                             Case " DisableStickyKeys"
                                                                 If Switch Then
                                                                     Optimize.DisableStickyKeys()
                                                                 Else
                                                                     Optimize.EnableStickyKeys()
                                                                 End If
                                                         End Select

                                                         Me.BeginInvoke(Sub()
                                                                            BOptionEx.IsLoading(True, 100)
                                                                        End Sub)

                                                     End If

                                                     System.Threading.Thread.Sleep(500)

                                                 Next

                                                 Me.BeginInvoke(Sub()
                                                                    For Each BOption2 As Control In PanelContainer.Controls
                                                                        If TypeOf BOption2 Is BoosterItem Then
                                                                            Dim BOptionEx As BoosterItem = DirectCast(BOption2, BoosterItem)
                                                                            BOptionEx.IsLoading(False, 0)
                                                                        End If
                                                                    Next
                                                                End Sub)

                                                 Dim ReadIni As String = Core.Utils.ReadIni("BossterOption", "Boost", String.Empty)
                                                 If ReadIni = String.Empty Then Core.Utils.WriteIni("BossterOption", "Boost", "On")

                                                 Me.BeginInvoke(Sub()
                                                                    If ReadIni = "Off" Then
                                                                        Guna2Button1.Checked = True
                                                                    Else
                                                                        Guna2Button2.Checked = True
                                                                    End If

                                                                End Sub)

                                             End Sub), TaskCreationOptions.PreferFairness)

        Asynctask.Start()

    End Sub

    Private Sub CircularProgress1_MouseHover(sender As Object, e As EventArgs) Handles CircularProgress1.MouseHover
        CircularProgress1.InnerColor = Color.FromArgb(24, 24, 24)
        CircularProgress1.Cursor = Cursors.Hand
    End Sub

    Private Sub CircularProgress1_MouseLeave(sender As Object, e As EventArgs) Handles CircularProgress1.MouseLeave
        CircularProgress1.InnerColor = Color.FromArgb(64, 64, 64)
        CircularProgress1.Cursor = Cursors.Default
    End Sub

    Private Sub CircularProgress1_Click(sender As Object, e As EventArgs) Handles CircularProgress1.Click
        Optimized()
    End Sub

End Class