

Public Class SystemInfoForm

#Region " Declare "

    Private cpuGraph As XylonV2.ComputerInfo.Graph.CPUGraph = Nothing
    Private ramGraph As XylonV2.ComputerInfo.Graph.RAMGraph = Nothing

    Private ChartControl1 As ChartControl = New StrelyCleannerPro.ChartControl()

    Private cpu As XylonV2.ComputerInfo.WMI.CPU = Nothing
    Private ram As XylonV2.ComputerInfo.WMI.RAM = Nothing
    Private gpu As XylonV2.ComputerInfo.WMI.GPU = Nothing
    Private os As XylonV2.ComputerInfo.WMI.OS = Nothing

#End Region

    Public Sub New()

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub SystemInfoForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Me.Refresh()
        DioMarqueeProgressBar1.Start()
        StartUI()
    End Sub

#Region " GUI "

    Public Sub StartUI()
        '
        'ChartControl1
        '
        Me.ChartControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ChartControl1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.ChartControl1.GraphPosition = New System.Drawing.Point(0, 0)
        Me.ChartControl1.Location = New System.Drawing.Point(0, 2)
        Me.ChartControl1.Name = "ChartControl1"
        Me.ChartControl1.Size = New System.Drawing.Size(212, 65)
        Me.ChartControl1.TabIndex = 0
        Me.ChartControl1.Text = "ChartControl1"
        Me.ChartControl1.Visible = False
        Me.Panel2.Controls.Add(Me.ChartControl1)

        StartMonitor()
    End Sub

    Private Sub SystemInfoForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

#End Region

#Region " Monitor "

    Public Sub StartMonitor()

        ChartControl1.BackColor = Color.FromArgb(18, 23, 29)

        Asynctask.Start()

    End Sub

    Private Asynctask As New Task(New Action(Async Sub()

                                                 os = New XylonV2.ComputerInfo.WMI.OS()

                                                 cpu = New XylonV2.ComputerInfo.WMI.CPU()
                                                 ram = New XylonV2.ComputerInfo.WMI.RAM()
                                                 ramGraph = New XylonV2.ComputerInfo.Graph.RAMGraph()
                                                 cpuGraph = New XylonV2.ComputerInfo.Graph.CPUGraph()

                                                 Me.BeginInvoke(Sub()
                                                                    Dim DoubleBytes As Double = CDbl(ram.PysicalSize / 1073741824)
                                                                    Guna2HtmlLabel2.Text = cpu.Name
                                                                    Guna2HtmlLabel3.Text = cpu.CoreCount
                                                                    Guna2HtmlLabel4.Text = cpu.Voltage
                                                                    Guna2HtmlLabel11.Text = ram.Manufacturer
                                                                    Guna2HtmlLabel10.Text = Math.Round(DoubleBytes).ToString & " GB"
                                                                    Guna2HtmlLabel9.Text = ram.Speed
                                                                End Sub)

                                                 Dim FixCounter As Boolean = False

                                                 Try
                                                     Dim Processing As Boolean = False

                                                     For i As Integer = 0 To 2

                                                         If ram.Update = True Then ramGraph.RefreshGraph(ram)
                                                         cpuGraph.RefreshGraph()

                                                         System.Threading.Thread.Sleep(500)

                                                         If FixCounter = False Then
                                                             If cpuGraph.ErrorOnCounter = True Then
                                                                 Dim WinCounterFixer As New CounterFixerDialog
                                                                 WinCounterFixer.ShowDialog()
                                                                 System.Threading.Thread.Sleep(300)
                                                                 FixCounter = True
                                                             End If
                                                         End If

                                                         System.Threading.Thread.Sleep(500)

                                                         If Me.WindowState = FormWindowState.Normal Then

                                                             If Processing = False Then

                                                                 Me.BeginInvoke(Sub()
                                                                                    Processing = True
                                                                                    Try

                                                                                        If Not Me.WindowState = FormWindowState.Minimized Then
                                                                                            Dim CPU_Usage As Integer = cpuGraph.CPU_Usage
                                                                                            Dim LastXPoint As Point = ChartControl1.Graph.LastOrDefault
                                                                                            Dim YPoint As Double = (CPU_Usage / 10) / 2
                                                                                            Dim XPoint As Integer = LastXPoint.X + 1

                                                                                            If XPoint > 20 Then

                                                                                                ChartControl1.Graph.Clear()
                                                                                                XPoint = 0

                                                                                            End If

                                                                                            Dim NewPoint As New Point(XPoint, YPoint)
                                                                                            Guna2ProgressBar1.Value = CPU_Usage
                                                                                            CircularProgress1.Value = CPU_Usage
                                                                                            ChartControl1.Graph.Add(NewPoint)
                                                                                            ChartControl1.Invalidate()

                                                                                            Guna2ProgressBar2.Value = ramGraph.Ram_Physical_Usage
                                                                                            CircularProgress2.Value = ramGraph.Ram_Physical_Usage

                                                                                            If Panel1.Visible = False Then
                                                                                                DioMarqueeProgressBar1.Visible = False
                                                                                                DioMarqueeProgressBar1.Stop()
                                                                                                Panel1.Visible = True
                                                                                                ChartControl1.Visible = True
                                                                                            End If
                                                                                        End If

                                                                                    Catch ex As Exception

                                                                                    End Try
                                                                                    Processing = False
                                                                                End Sub)
                                                             End If


                                                         End If


                                                         System.Threading.Thread.Sleep(500)

                                                         i -= 1
                                                     Next

                                                 Catch ex As Exception
                                                     Exit Sub
                                                 End Try

                                             End Sub), TaskCreationOptions.PreferFairness)

    Private Sub Guna2ProgressBar1_ValueChanged(sender As Object, e As EventArgs) Handles Guna2ProgressBar1.ValueChanged, Guna2ProgressBar2.ValueChanged
        Dim TypeController As Guna.UI2.WinForms.Guna2ProgressBar = DirectCast(sender, Guna.UI2.WinForms.Guna2ProgressBar)
        Dim CVal As Integer = TypeController.Value

        If CVal <= 40 Then

            TypeController.ProgressColor = Color.Lime
            TypeController.ProgressColor2 = Color.Lime

        ElseIf CVal > 41 And CVal <= 50 Then

            TypeController.ProgressColor = Color.Yellow
            TypeController.ProgressColor2 = Color.Yellow

        ElseIf CVal > 51 And CVal <= 60 Then

            TypeController.ProgressColor = Color.Orange
            TypeController.ProgressColor2 = Color.Orange

        ElseIf CVal > 61 And CVal <= 70 Then

            TypeController.ProgressColor = Color.OrangeRed
            TypeController.ProgressColor2 = Color.OrangeRed

        ElseIf CVal > 71 And CVal <= 90 Then

            TypeController.ProgressColor = Color.Red
            TypeController.ProgressColor2 = Color.Red

        ElseIf CVal > 91 And CVal <= 100 Then

            TypeController.ProgressColor = Color.DarkRed
            TypeController.ProgressColor2 = Color.DarkRed

        End If

        Select Case TypeController.Name
            Case Guna2ProgressBar1.Name : ChartControl1.ForeColor = CircularProgress1.ProgressColor : CircularProgress1.ProgressColor = TypeController.ProgressColor : CircularProgress1.Text = CVal & "%"
            Case Guna2ProgressBar2.Name : CircularProgress2.ProgressColor = TypeController.ProgressColor : CircularProgress2.Text = CVal & "%"
        End Select

    End Sub

#End Region

End Class