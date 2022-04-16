Imports XylonV2.StartupManager
Imports XylonV2.StartupManager.Services.Directories
Imports XylonV2.StartupManager.Services.Registries

Public Class StartupManagerForm

    Public Sub New()

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub StartupManagerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Me.Refresh()
    End Sub



#Region " GUI "

    Private Sub StartupManagerForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ProcessListScrool = New Core.ScrollManager(PanelContainer, {Guna2VScrollBar1}, True)
        StartList()
    End Sub

#End Region


#Region " Listener "

    Private ProcessListener As Boolean = False
    Private ProcessListScrool As Core.ScrollManager = Nothing
    Private ListenerProcess As New ControlLister With {.OrientationControls = Orientation.Vertical, .Margen = New Point(5, 10)}
    Private LoadCheck As Boolean = False

    Public Sub StartList()
        DioMarqueeProgressBar1.Start()
        Panel2.Visible = False
        DioMarqueeProgressBar1.Visible = True
        PanelContainer.Controls.Clear()
        GetStartupList(Guna2ToggleSwitch1.Checked, Guna2ToggleSwitch2.Checked, Guna2ToggleSwitch3.Checked)
        LoadCheck = False
    End Sub

    Public Sub GetStartupList(ByVal WStartup As Boolean, ByVal Regedit As Boolean, ByVal Task As Boolean)

        If ProcessListener = False Then
            ProcessListener = True

            Dim Asynctask As New Task(New Action(Async Sub()

                                                     Dim IDName As Integer = 0

                                                     Dim RegistryService As RegistryService = New RegistryService()
                                                     Dim DirectoryService As DirectoryService = New DirectoryService()

                                                     Dim startupPrograms As List(Of Models.StartupList) = New List(Of Models.StartupList)

                                                     Dim startupStates = RegistryService.GetStartupProgramStates()
                                                     Dim registryStartups = RegistryService.GetStartupPrograms(startupStates)

                                                     If registryStartups IsNot Nothing Then
                                                         startupPrograms.AddRange(registryStartups)
                                                     End If

                                                     Dim shellStartups = DirectoryService.GetStartupPrograms(startupStates)
                                                     If shellStartups IsNot Nothing Then
                                                         startupPrograms.AddRange(shellStartups)
                                                     End If


                                                     Dim ListerThis As Boolean = True

                                                     For Each ItemStartup As Models.StartupList In startupPrograms

                                                         ListerThis = True

                                                         If Not ItemStartup.GetParsedPath = Application.ExecutablePath AndAlso
                                                         Not IO.Path.GetFileName(ItemStartup.GetParsedPath) = "desktop.ini" Then

                                                             Select Case ItemStartup.Type
                                                                 Case Models.StartupList.StartupType.Regedit
                                                                     ListerThis = Regedit
                                                                 Case Models.StartupList.StartupType.Shortcut
                                                                     ListerThis = WStartup
                                                                 Case Models.StartupList.StartupType.TaskScheduler
                                                                     ListerThis = Task
                                                             End Select

                                                             Me.BeginInvoke(Sub()

                                                                                If ListerThis = True Then
                                                                                    Dim NewProc As New StartupControl With {.StartupItem = ItemStartup, .Visible = True}
                                                                                    NewProc.Name = IDName
                                                                                    ListenerProcess.Add(PanelContainer, NewProc)
                                                                                    IDName += 1
                                                                                End If

                                                                            End Sub)

                                                         End If

                                                     Next

                                                     Me.BeginInvoke(Sub()
                                                                        Panel2.Visible = True
                                                                        DioMarqueeProgressBar1.Visible = False
                                                                        DioMarqueeProgressBar1.Stop()
                                                                    End Sub)
                                                     LoadCheck = True
                                                 End Sub), TaskCreationOptions.PreferFairness)
            Asynctask.Start()
            ProcessListener = False
        End If

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        StartList()
    End Sub

#End Region

End Class