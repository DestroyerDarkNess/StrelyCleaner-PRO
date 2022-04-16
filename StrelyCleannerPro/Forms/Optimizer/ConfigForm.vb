Public Class ConfigForm

#Region " Declare "

    Private Optimize As New Core.Optimizer.Optimize
    Private ProcessListScrool As Core.ScrollManager
    Private ReadAllSettings As Boolean = False

#End Region

    Public Sub New()

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ConfigForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Me.Refresh()
        DioMarqueeProgressBar1.Start()
        ProcessListScrool = New Core.ScrollManager(PanelContainer, {Guna2VScrollBar1}, True)
        StartUI()
    End Sub

#Region " GUI "

    Public Sub StartUI()
        ' Theme
        Dim SysThemeIni As String = Core.Utils.ReadIni("BossterOption", "SysTheme", String.Empty)
        If SysThemeIni = "Dark" Then Guna2ComboBox1.StartIndex = 0 Else Guna2ComboBox1.StartIndex = 1
        Dim AppThemeIni As String = Core.Utils.ReadIni("BossterOption", "AppTheme", String.Empty)
        If AppThemeIni = "Dark" Then Guna2ComboBox2.StartIndex = 0 Else Guna2ComboBox2.StartIndex = 1

        ' ClassicRibbon

        Dim ClassicRibbonIni As String = Core.Utils.ReadIni("BossterOption", "ClassicRibbon", String.Empty)
        If Not ClassicRibbonIni = String.Empty Then Guna2Button10.Checked = IsEnabled(ClassicRibbonIni) Else Guna2Button9.Checked = IsEnabled(ClassicRibbonIni)

        'XboxLive

        Dim XboxLiveIni As String = Core.Utils.ReadIni("BossterOption", "XboxLive", String.Empty)
        If Not XboxLiveIni = String.Empty Then Guna2Button1.Checked = IsEnabled(XboxLiveIni) Else Guna2Button2.Checked = IsEnabled(XboxLiveIni)

        'EnhancePrivacy

        Dim EnhancePrivacyIni As String = Core.Utils.ReadIni("BossterOption", "EnhancePrivacy", String.Empty)
        If Not EnhancePrivacyIni = String.Empty Then Guna2Button4.Checked = IsEnabled(EnhancePrivacyIni) Else Guna2Button3.Checked = IsEnabled(EnhancePrivacyIni)

        'Cortana

        Dim CortanaIni As String = Core.Utils.ReadIni("BossterOption", "Cortana", String.Empty)
        If Not CortanaIni = String.Empty Then Guna2Button6.Checked = IsEnabled(CortanaIni) Else Guna2Button5.Checked = IsEnabled(CortanaIni)

        'OneDrive

        Dim OneDriveIni As String = Core.Utils.ReadIni("BossterOption", "OneDrive", String.Empty)
        If Not OneDriveIni = String.Empty Then Guna2Button8.Checked = IsEnabled(OneDriveIni) Else Guna2Button7.Checked = IsEnabled(OneDriveIni)

        DioMarqueeProgressBar1.Stop()
        DioMarqueeProgressBar1.Visible = False
        Panel1.Visible = True

        ReadAllSettings = True
    End Sub

    Private Sub ConfigForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Private Function IsEnabled(ByVal Ini As String)
        If Ini = String.Empty Then
            Return False
        Else
            Return (Ini = "On")
        End If
    End Function

#End Region

#Region " System Settings "

    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged
        If ReadAllSettings = True Then

            Select Case Guna2ComboBox1.SelectedIndex
                Case 0 : Optimize.EnableDarkThemeSys() : Core.Utils.WriteIni("BossterOption", "SysTheme", "Dark")
                Case 1 : Optimize.EnableLightThemeSys() : Core.Utils.WriteIni("BossterOption", "SysTheme", "Light")
            End Select

        End If
    End Sub

    Private Sub Guna2ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox2.SelectedIndexChanged
        If ReadAllSettings = True Then

            Select Case Guna2ComboBox1.SelectedIndex
                Case 0 : Optimize.EnableDarkThemeApp() : Core.Utils.WriteIni("BossterOption", "AppTheme", "Dark")
                Case 1 : Optimize.EnableLightThemeApp() : Core.Utils.WriteIni("BossterOption", "AppTheme", "Light")
            End Select

        End If
    End Sub

    Private Sub XboxLive_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2Button1.CheckedChanged, Guna2Button2.CheckedChanged
        If ReadAllSettings = True Then
            Dim ButtonCheck As Guna.UI2.WinForms.Guna2Button = DirectCast(sender, Guna.UI2.WinForms.Guna2Button)

            Select Case ButtonCheck.Text
                Case Guna2Button1.Text : Optimize.EnableXboxLive() : Core.Utils.WriteIni("BossterOption", "XboxLive", "On")
                Case Guna2Button2.Text : Optimize.DisableXboxLive() : Core.Utils.WriteIni("BossterOption", "XboxLive", "Off")
            End Select
        End If
    End Sub

    Private Sub EnhancePrivacy_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2Button4.CheckedChanged, Guna2Button3.CheckedChanged
        If ReadAllSettings = True Then
            Dim ButtonCheck As Guna.UI2.WinForms.Guna2Button = DirectCast(sender, Guna.UI2.WinForms.Guna2Button)

            Select Case ButtonCheck.Text
                Case Guna2Button4.Text : Optimize.EnhancePrivacy() : Core.Utils.WriteIni("BossterOption", "EnhancePrivacy", "On")
                Case Guna2Button3.Text : Optimize.CompromisePrivacy() : Core.Utils.WriteIni("BossterOption", "EnhancePrivacye", "Off")
            End Select
        End If
    End Sub

    Private Sub Cortana_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2Button6.CheckedChanged, Guna2Button5.CheckedChanged
        If ReadAllSettings = True Then
            Dim ButtonCheck As Guna.UI2.WinForms.Guna2Button = DirectCast(sender, Guna.UI2.WinForms.Guna2Button)

            Select Case ButtonCheck.Text
                Case Guna2Button6.Text : Optimize.EnableCortana() : Core.Utils.WriteIni("BossterOption", "Cortana", "On")
                Case Guna2Button5.Text : Optimize.DisableCortana() : Core.Utils.WriteIni("BossterOption", "Cortana", "Off")
            End Select
        End If
    End Sub

    Dim InProcessOneDrive As Boolean = False

    Private Sub OneDrive_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2Button8.CheckedChanged, Guna2Button7.CheckedChanged
        If ReadAllSettings = True Then

            Dim ButtonCheck As Guna.UI2.WinForms.Guna2Button = DirectCast(sender, Guna.UI2.WinForms.Guna2Button)

            Select Case ButtonCheck.Text
                Case Guna2Button8.Text
                    Guna2Button7.Enabled = False
                    Guna2Button8.Enabled = False
                    DioMarqueeProgressBar2.Start()
                    DioMarqueeProgressBar2.Visible = True
                    Dim InstallOneDrive As Task(Of Boolean) = Optimize.InstallOneDrive()
                    InstallOneDrive.GetAwaiter.OnCompleted(Sub()

                                                               Guna2Button7.Enabled = True
                                                               Guna2Button8.Enabled = True
                                                               DioMarqueeProgressBar2.Stop()
                                                               DioMarqueeProgressBar2.Visible = False
                                                           End Sub)

                    Core.Utils.WriteIni("BossterOption", "OneDrive", "On")
                Case Guna2Button7.Text
                    Guna2Button7.Enabled = False
                    Guna2Button8.Enabled = False
                    DioMarqueeProgressBar2.Start()
                    DioMarqueeProgressBar2.Visible = True
                    Dim UninstallOneDrive As Task(Of Boolean) = Optimize.UninstallOneDrive()
                    UninstallOneDrive.GetAwaiter.OnCompleted(Sub()
                                                                 Guna2Button7.Enabled = True
                                                                 Guna2Button8.Enabled = True
                                                                 DioMarqueeProgressBar2.Stop()
                                                                 DioMarqueeProgressBar2.Visible = False
                                                             End Sub)

                    Core.Utils.WriteIni("BossterOption", "OneDrive", "Off")
            End Select

        End If
    End Sub

    Private Sub ClassicRibbon_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2Button10.CheckedChanged, Guna2Button9.CheckedChanged
        If ReadAllSettings = True Then
            Dim ButtonCheck As Guna.UI2.WinForms.Guna2Button = DirectCast(sender, Guna.UI2.WinForms.Guna2Button)

            Select Case ButtonCheck.Text
                Case Guna2Button10.Text : Optimize.EnableFileExplorerClassicRibbon() : Core.Utils.WriteIni("BossterOption", "ClassicRibbon", "On")
                Case Guna2Button9.Text : Optimize.DisableFileExplorerClassicRibbon() : Core.Utils.WriteIni("BossterOption", "ClassicRibbon", "Off")
            End Select
        End If
    End Sub

#End Region


End Class