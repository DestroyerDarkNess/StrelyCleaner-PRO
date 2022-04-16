Public Class ProcessManagerForm

    Public Sub New()

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ProcessManagerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Me.Refresh()
    End Sub



#Region " GUI "

    Private Sub ProcessManagerForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ProcessListScrool = New Core.ScrollManager(PanelContainer, {Guna2VScrollBar1}, True)
        Core.Scintilla.VbNetRecipe_Dark.SetVbNetDarkStyle(Scintilla1)
        StartList()
    End Sub

    Private Sub GunaGoogleSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles GunaGoogleSwitch1.CheckedChanged
        If GunaGoogleSwitch1.Checked = True Then
            ' GunaGoogleSwitch1.FillColor = Color.White
            Label1.ForeColor = Color.White
        Else
            ' GunaGoogleSwitch1.FillColor = Color.FromArgb(44, 44, 44)
            Label1.ForeColor = Color.Gray
        End If

        If LoadCheck = True Then
            StartList()
        End If
    End Sub

    Private Sub GunaGoogleSwitch2_CheckedChanged(sender As Object, e As EventArgs) Handles GunaGoogleSwitch2.CheckedChanged
        If GunaGoogleSwitch2.Checked = True Then
            ' GunaGoogleSwitch2.FillColor = Color.White
            Label2.ForeColor = Color.White
        Else
            ' GunaGoogleSwitch2.FillColor = Color.FromArgb(44, 44, 44)
            Label2.ForeColor = Color.Gray
        End If

        If LoadCheck = True Then
            StartList()
        End If

    End Sub

#End Region

#Region " Listener "

    Private ProcessListener As Boolean = False
    Private ProcessListScrool As Core.ScrollManager = Nothing
    Private ListenerProcess As New ControlLister With {.OrientationControls = Orientation.Vertical, .Margen = New Point(0, 0)}
    Private LoadCheck As Boolean = False

    Public Sub StartList()
        DioMarqueeProgressBar1.Start()
        Panel1.Visible = False
        DioMarqueeProgressBar1.Visible = True
        PanelContainer.Controls.Clear()
        GetProcessList()
        LoadCheck = False
    End Sub

    Public Sub GetProcessList()

        If ProcessListener = False Then
            ProcessListener = True

            Dim Asynctask As New Task(New Action(Async Sub()
                                                     Try
                                                         Dim IDName As Integer = 0
                                                         Dim Firs As Boolean = True

                                                         Dim ProcessAll As Process() = Process.GetProcesses()

                                                         For Each CurrentProc As Process In ProcessAll

                                                             Dim ProcInfo As XylonV2.Core.Engine.WMI.Win32_Process = XylonV2.Core.Engine.WMI.Win32_Process.GetProcesses(CurrentProc.Id)

                                                             If ProcInfo IsNot Nothing Then

                                                                 Me.BeginInvoke(Sub()

                                                                                    Dim ListerThis As Boolean = True

                                                                                    If GunaGoogleSwitch1.Checked = False Then

                                                                                        If ProcInfo.ExecutablePath Is Nothing Then
                                                                                            ListerThis = False
                                                                                        Else
                                                                                            Dim PathFolder As String = IO.Path.GetDirectoryName(ProcInfo.ExecutablePath)
                                                                                            If LCase(PathFolder.ToString).Contains("windows") = True Then
                                                                                                ListerThis = False
                                                                                            End If
                                                                                        End If

                                                                                    End If

                                                                                    If GunaGoogleSwitch2.Checked = True Then
                                                                                        If IsListed(CurrentProc.ProcessName) = True Then
                                                                                            ListerThis = False
                                                                                        End If
                                                                                    End If

                                                                                    If ProcInfo.ExecutablePath = Application.ExecutablePath Then
                                                                                        ListerThis = False
                                                                                    End If

                                                                                    If ListerThis = True Then
                                                                                        Dim NewProc As New ProcessControl With {.ProcesEx = CurrentProc, .Comandline = ProcInfo.CommandLine, .ProcPath = ProcInfo.ExecutablePath, .Visible = False}
                                                                                        NewProc.Name = IDName

                                                                                        If Firs = True Then
                                                                                            NewProc.CallParent = True
                                                                                            Firs = False
                                                                                        End If

                                                                                        ListenerProcess.Add(PanelContainer, NewProc)
                                                                                    End If

                                                                                End Sub)
                                                                 IDName += 1

                                                             End If


                                                         Next

                                                         Me.BeginInvoke(Sub()
                                                                            Panel1.Visible = True
                                                                            DioMarqueeProgressBar1.Visible = False
                                                                            DioMarqueeProgressBar1.Stop()
                                                                        End Sub)

                                                     Catch ex As Exception
                                                         'Core.ErrorLogger.LogError("Process.Listing", ex.Message, ex.StackTrace)
                                                     End Try
                                                     LoadCheck = True
                                                 End Sub), TaskCreationOptions.PreferFairness)
            Asynctask.Start()
            ProcessListener = False
        End If

    End Sub

    Private Function IsListed(ByVal ProcName As String) As Boolean
        For Each PControl As ProcessControl In PanelContainer.Controls
            If PControl.ProcesEx.ProcessName = ProcName Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        StartList()
    End Sub

    Dim LasControl As ProcessControl = Nothing

    Public Sub SetProcess(ByVal Proc As ProcessControl)
        Proc.CheckExits()

        If Proc.CheckProcess Then

            LasControl = Proc

            For Each PControl As ProcessControl In PanelContainer.Controls
                If Not PControl.ProcesEx.Id = Proc.ProcesEx.Id Then
                    PControl.GunaAdvenceButton2.Checked = False
                End If
            Next

            ShowProcessInfo(Proc)

        End If
    End Sub

#End Region

#Region " Process Info "

    Private Sub ShowProcessInfo(ByVal Proc As ProcessControl)
        Try
            Scintilla1.Text = vbNewLine & vbTab & "+ Process Name : " & Proc.ProcesEx.ProcessName & vbNewLine
            Scintilla1.Text += vbTab & "+ ID           : " & Proc.ProcesEx.Id & vbNewLine
            Scintilla1.Text += vbTab & "+ Handle       : " & Proc.ProcesEx.Handle.ToString & vbNewLine
            Scintilla1.Text += vbTab & "+ Window Title : " & Proc.ProcesEx.MainWindowTitle & vbNewLine
            Scintilla1.Text += vbTab & "+ Process Path : " & """" & Proc.ProcPath & """" & vbNewLine
            Scintilla1.Text += vbTab & "+ Comand Line  : " & Proc.Comandline.ToString.Replace(Proc.ProcPath.ToString, "").Replace("""", "") & vbNewLine

            If Proc.NModules IsNot Nothing Then
                For Each PModule As String In Proc.NModules
                    Dim ListerThis As Boolean = True

                    If Guna2ToggleSwitch1.Checked = False Then
                        Dim PathFolder As String = IO.Path.GetDirectoryName(PModule.ToString).ToString
                        If LCase(PathFolder.ToString).Contains("windows") = True Then
                            ListerThis = False
                        End If
                    End If

                    If ListerThis = True Then
                        Scintilla1.Text += vbNewLine & vbNewLine
                        Scintilla1.Text += vbTab & "Module " & IO.Path.GetFileNameWithoutExtension(PModule).ToString & vbNewLine
                        Scintilla1.Text += vbTab & "    Path           " & """" & PModule.ToString & """" & vbNewLine
                        Scintilla1.Text += vbTab & "End Module " & vbNewLine
                    End If

                Next
            End If

            Scintilla1.Text += vbNewLine & vbNewLine

        Catch ex As Exception
            Scintilla1.Text = ex.Message.ToString
            Scintilla1.Text += vbNewLine & vbNewLine
            Scintilla1.Text += ex.Source.ToString
        End Try
    End Sub

    Private Sub Guna2ToggleSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2ToggleSwitch1.CheckedChanged
        If LasControl IsNot Nothing Then
            ShowProcessInfo(LasControl)
        End If
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Process.Start("https://toolslib.net/downloads/finish/1926-dll-injector-hacker/")
    End Sub

#End Region

End Class