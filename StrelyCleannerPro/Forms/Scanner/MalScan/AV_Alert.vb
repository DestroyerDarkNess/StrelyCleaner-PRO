Public Class AV_Alert

#Region " Properties "

    Private _Info As XylonV2.Engine.External.Core.DetectionResult = Nothing
    Public Property Info As XylonV2.Engine.External.Core.DetectionResult
        Get
            Return _Info
        End Get
        Set(value As XylonV2.Engine.External.Core.DetectionResult)
            _Info = value
        End Set
    End Property

    Private _FilePath As String = String.Empty
    Public Property FilePath As String
        Get
            Return _FilePath
        End Get
        Set(value As String)
            _FilePath = value
        End Set
    End Property

    Private _AlertNotify As Notify = Nothing
    Public Property AlertNotify As Notify
        Get
            Return _AlertNotify
        End Get
        Set(value As Notify)
            _AlertNotify = value
        End Set
    End Property

    Private _AlertLevel As Core.Instances.WarnLevel = Core.Instances.WarnLevel.Danger
    Public Property AlertLevel As Core.Instances.WarnLevel
        Get
            Return _AlertLevel
        End Get
        Set(value As Core.Instances.WarnLevel)
            _AlertLevel = value
        End Set
    End Property

    Private _Action As Action = Nothing
    Public Property AlertAction As Action
        Get
            Return _Action
        End Get
        Set(value As Action)
            _Action = value
        End Set
    End Property

#End Region


    Private InfoOk As String = "Solve"

    Private Sub AV_Alert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2ShadowForm1.SetShadowForm(Me)
        ExploreButton.Visible = IO.File.Exists(_FilePath)
        Guna2HtmlLabel2.Text = IO.Path.GetFileName(_FilePath)
        Guna2HtmlLabel3.Text = _Info.Signature
        Guna2HtmlToolTip1.SetToolTip(Guna2HtmlLabel4, _Info.Description)

        Select Case _AlertLevel
            Case Core.Instances.WarnLevel.Danger
                Guna2HtmlLabel1.Text = "Danger"
                Guna2PictureBox1.Image = My.Resources.icons8_biohazard_48
                Guna2Panel2.BackColor = Color.FromArgb(255, 118, 118)
                Guna2HtmlLabel3.ForeColor = Color.Red
            Case Core.Instances.WarnLevel.Warning
                Guna2HtmlLabel1.Text = "Warning"
                Guna2PictureBox1.Image = My.Resources.icons8_warning_64
                Guna2Panel2.BackColor = Color.FromArgb(255, 182, 34)
                Guna2HtmlLabel3.ForeColor = Color.FromArgb(255, 216, 0)
                Guna2Panel1.BorderColor = Color.FromArgb(255, 182, 34)
            Case Core.Instances.WarnLevel.Sure
                Guna2HtmlLabel1.Text = "You Are Protected"
                InfoOk = "OK"
                LeaveButton.Visible = False
                Guna2PictureBox1.Image = My.Resources.icons8_protect_100
                Guna2Panel2.BackColor = Color.FromArgb(47, 241, 117)
                Guna2HtmlLabel3.ForeColor = Color.MediumSpringGreen
                Guna2HtmlLabel1.Font = New Font("Segoe UI", 20)
                Guna2Panel1.BorderColor = Color.FromArgb(47, 241, 117)
        End Select

        SolveButton.Text = InfoOk

        Me.Clock.Start(20000)
    End Sub

    Private Sub SolveButton_Click(sender As Object, e As EventArgs) Handles SolveButton.Click
        Me.Clock.Stop()
        Solve()
    End Sub

    Private Sub LeaveButton_Click(sender As Object, e As EventArgs) Handles LeaveButton.Click
        Me.Clock.Stop()
        LeaveAlert(False)
    End Sub

    Private Sub ExploreButton_Click(sender As Object, e As EventArgs) Handles ExploreButton.Click
        Core.Utils.ExploreFile(_FilePath)
    End Sub

    Private Sub Solve()
        Me.Hide()

        Dim tProcess As Task(Of Boolean) = Task.Run(Function()
                                                        Dim IsDeleteCorrect As Boolean = False
                                                        If IO.File.Exists(_FilePath) = True Then
                                                            Dim ForDeleter As MK.Tools.ForceDel.FileDeleter = New MK.Tools.ForceDel.FileDeleter()
                                                            IsDeleteCorrect = ForDeleter.Delete(_FilePath)
                                                            If IsDeleteCorrect = False Then
                                                                Core.Instances.ErrorsLogger.Add("[ AV ] " & _FilePath & "   ->   " & "Could not be removed successfully")
                                                            End If
                                                            _AlertNotify = Nothing
                                                        End If
                                                        If _Action IsNot Nothing Then
                                                            _Action()
                                                        End If
                                                        Return IsDeleteCorrect
                                                    End Function)
        '   tProcess.Wait()
        tProcess.GetAwaiter().OnCompleted(AddressOf LeaveAlert)

    End Sub

    Private Sub LeaveAlert(Optional ByVal IsSolved As Boolean = True)

        If Guna2CheckBox1.Checked = True Then
            Dim InfoEx As New Core.ExType
            InfoEx.FilePath = _FilePath
            InfoEx.IsSolve = IsSolved
            Core.ExclusionManager.Add(InfoEx)
        End If

        If _AlertNotify Is Nothing Then
            Me.Close()
        Else
            _AlertNotify.CloseEx()
        End If
    End Sub

#Region " Cuenta Regresiva "

    ''' <summary>
    ''' The <see cref="TimeMeasurer"/> instance that measure time intervals.
    ''' </summary>
    Private WithEvents Clock As New TimeMeasurer With {.UpdateInterval = 100}

    Private Sub Clock_RemainingTimeUpdated(ByVal sender As Object, ByVal e As TimeMeasurer.TimeMeasureEventArgs) _
    Handles Clock.RemainingTimeUpdated

        SolveButton.Text = InfoOk & " (" & e.Second & ")"

    End Sub

    Private Sub Clock_RemainingTimeFinished(ByVal sender As Object, ByVal e As TimeMeasurer.TimeMeasureEventArgs) _
    Handles Clock.RemainingTimeFinished

        Solve()

    End Sub

#End Region

#Region " No Windows Focus "

    Private Const SW_SHOWNOACTIVATE As Integer = 4
    Private Const HWND_TOPMOST As Integer = -1
    Private Const SWP_NOACTIVATE As UInteger = &H10

    <System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint:="SetWindowPos")>
    Private Shared Function SetWindowPos(ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInteger) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Private Shared Function ShowWindow(ByVal hWnd As System.IntPtr, ByVal nCmdShow As Integer) As Boolean
    End Function

    Public Shared Sub ShowInactiveTopmost(ByVal frm As System.Windows.Forms.Form)
        Try
            ShowWindow(frm.Handle, SW_SHOWNOACTIVATE)
            SetWindowPos(frm.Handle.ToInt32(), HWND_TOPMOST, frm.Left, frm.Top, frm.Width, frm.Height, SWP_NOACTIVATE)
        Catch ex As System.Exception
        End Try
    End Sub

    Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean
        Get
            Return True
        End Get
    End Property

    Private Const WS_EX_TOPMOST As Integer = &H8

    Private Const WS_THICKFRAME As Integer = &H40000
    Private Const WS_CHILD As Integer = &H40000000
    Private Const WS_EX_NOACTIVATE As Integer = &H8000000
    Private Const WS_EX_TOOLWINDOW As Integer = &H80

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim createParamsA As CreateParams = MyBase.CreateParams
            createParamsA.ExStyle = createParamsA.ExStyle Or WS_EX_TOPMOST Or WS_EX_NOACTIVATE Or WS_EX_TOOLWINDOW
            Return createParamsA
        End Get
    End Property

    Private Sub Guna2CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2CheckBox1.CheckedChanged

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Guna2Button2.Visible = False
        Dim InfoDetail As String = "File: " & _FilePath & vbNewLine &
            "Level: " & Guna2HtmlLabel1.Text & vbNewLine &
        "Signature: " & _Info.Signature & vbNewLine &
        "Description: " & _Info.Description & vbNewLine & vbNewLine & vbNewLine &
        "Copyright StrelyCleaner - https://github.com/DestroyerDarkNess"

        Clipboard.SetText(InfoDetail)
        Guna2Button2.Visible = True
    End Sub

#End Region


End Class