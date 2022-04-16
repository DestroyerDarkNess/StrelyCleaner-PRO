<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BoostingForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2Button2 = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2Button1 = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2VScrollBar1 = New Guna.UI2.WinForms.Guna2VScrollBar()
        Me.PanelContainer = New StrelyCleannerPro.PanelFX()
        Me.DioMarqueeProgressBar1 = New StrelyCleannerPro.Dio.DioMarqueeProgressBar()
        Me.CircularProgress1 = New StrelyCleannerPro.CircularProgress()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Guna2Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.CircularProgress1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(858, 451)
        Me.Panel1.TabIndex = 6
        Me.Panel1.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Guna2Panel1)
        Me.Panel2.Controls.Add(Me.Guna2VScrollBar1)
        Me.Panel2.Controls.Add(Me.PanelContainer)
        Me.Panel2.Location = New System.Drawing.Point(0, 203)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(858, 248)
        Me.Panel2.TabIndex = 0
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.Controls.Add(Me.Guna2Button2)
        Me.Guna2Panel1.Controls.Add(Me.Guna2Button1)
        Me.Guna2Panel1.Location = New System.Drawing.Point(351, 0)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.ShadowDecoration.Parent = Me.Guna2Panel1
        Me.Guna2Panel1.Size = New System.Drawing.Size(200, 25)
        Me.Guna2Panel1.TabIndex = 0
        '
        'Guna2Button2
        '
        Me.Guna2Button2.Animated = True
        Me.Guna2Button2.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.Guna2Button2.CheckedState.CustomBorderColor = System.Drawing.Color.Red
        Me.Guna2Button2.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Guna2Button2.CheckedState.Parent = Me.Guna2Button2
        Me.Guna2Button2.CustomBorderThickness = New System.Windows.Forms.Padding(0, 0, 0, 2)
        Me.Guna2Button2.CustomImages.Parent = Me.Guna2Button2
        Me.Guna2Button2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Guna2Button2.FillColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.Guna2Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2Button2.ForeColor = System.Drawing.Color.White
        Me.Guna2Button2.HoverState.Parent = Me.Guna2Button2
        Me.Guna2Button2.Location = New System.Drawing.Point(101, 0)
        Me.Guna2Button2.Name = "Guna2Button2"
        Me.Guna2Button2.ShadowDecoration.Parent = Me.Guna2Button2
        Me.Guna2Button2.Size = New System.Drawing.Size(101, 25)
        Me.Guna2Button2.TabIndex = 1
        Me.Guna2Button2.Text = "Restore"
        '
        'Guna2Button1
        '
        Me.Guna2Button1.Animated = True
        Me.Guna2Button1.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.Guna2Button1.CheckedState.CustomBorderColor = System.Drawing.Color.Lime
        Me.Guna2Button1.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Guna2Button1.CheckedState.Parent = Me.Guna2Button1
        Me.Guna2Button1.CustomBorderThickness = New System.Windows.Forms.Padding(0, 0, 0, 2)
        Me.Guna2Button1.CustomImages.Parent = Me.Guna2Button1
        Me.Guna2Button1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Guna2Button1.FillColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.Guna2Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2Button1.ForeColor = System.Drawing.Color.White
        Me.Guna2Button1.HoverState.Parent = Me.Guna2Button1
        Me.Guna2Button1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Button1.Name = "Guna2Button1"
        Me.Guna2Button1.ShadowDecoration.Parent = Me.Guna2Button1
        Me.Guna2Button1.Size = New System.Drawing.Size(101, 25)
        Me.Guna2Button1.TabIndex = 0
        Me.Guna2Button1.Text = "Boost"
        '
        'Guna2VScrollBar1
        '
        Me.Guna2VScrollBar1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Guna2VScrollBar1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2VScrollBar1.HoverState.Parent = Nothing
        Me.Guna2VScrollBar1.LargeChange = 10
        Me.Guna2VScrollBar1.Location = New System.Drawing.Point(847, 0)
        Me.Guna2VScrollBar1.MouseWheelBarPartitions = 10
        Me.Guna2VScrollBar1.Name = "Guna2VScrollBar1"
        Me.Guna2VScrollBar1.PressedState.Parent = Me.Guna2VScrollBar1
        Me.Guna2VScrollBar1.ScrollbarSize = 11
        Me.Guna2VScrollBar1.Size = New System.Drawing.Size(11, 248)
        Me.Guna2VScrollBar1.TabIndex = 1
        Me.Guna2VScrollBar1.ThumbColor = System.Drawing.Color.White
        Me.Guna2VScrollBar1.ThumbSize = 5.0!
        '
        'PanelContainer
        '
        Me.PanelContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelContainer.DoubleBuffered = True
        Me.PanelContainer.Location = New System.Drawing.Point(3, 26)
        Me.PanelContainer.Name = "PanelContainer"
        Me.PanelContainer.PreventFlickering = True
        Me.PanelContainer.Size = New System.Drawing.Size(867, 219)
        Me.PanelContainer.TabIndex = 0
        '
        'DioMarqueeProgressBar1
        '
        Me.DioMarqueeProgressBar1._increment = 10
        Me.DioMarqueeProgressBar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.DioMarqueeProgressBar1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.DioMarqueeProgressBar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.DioMarqueeProgressBar1.DrawBorder = False
        Me.DioMarqueeProgressBar1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.DioMarqueeProgressBar1.ForeColor = System.Drawing.Color.DeepSkyBlue
        Me.DioMarqueeProgressBar1.Location = New System.Drawing.Point(0, 0)
        Me.DioMarqueeProgressBar1.MarqueeVisible = True
        Me.DioMarqueeProgressBar1.MarqueeWidth = 50
        Me.DioMarqueeProgressBar1.MarqueeXPos = 20
        Me.DioMarqueeProgressBar1.Name = "DioMarqueeProgressBar1"
        Me.DioMarqueeProgressBar1.Size = New System.Drawing.Size(858, 5)
        Me.DioMarqueeProgressBar1.TabIndex = 5
        Me.DioMarqueeProgressBar1.Text = "DioMarqueeProgressBar1"
        '
        'CircularProgress1
        '
        Me.CircularProgress1.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.CircularProgress1.AnimationSpeed = 500
        Me.CircularProgress1.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgress1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CircularProgress1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.CircularProgress1.ForeColor = System.Drawing.Color.White
        Me.CircularProgress1.InnerColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CircularProgress1.InnerMargin = 2
        Me.CircularProgress1.InnerWidth = -1
        Me.CircularProgress1.Location = New System.Drawing.Point(365, 6)
        Me.CircularProgress1.MarqueeAnimationSpeed = 2000
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.OuterColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.CircularProgress1.OuterMargin = -25
        Me.CircularProgress1.OuterWidth = 26
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressWidth = 25
        Me.CircularProgress1.SecondaryFont = New System.Drawing.Font("Microsoft Sans Serif", 36.0!)
        Me.CircularProgress1.Size = New System.Drawing.Size(164, 156)
        Me.CircularProgress1.StartAngle = 0
        Me.CircularProgress1.SubscriptColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.CircularProgress1.SubscriptMargin = New System.Windows.Forms.Padding(10, -35, 0, 0)
        Me.CircularProgress1.SubscriptText = ""
        Me.CircularProgress1.SuperscriptColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.CircularProgress1.SuperscriptMargin = New System.Windows.Forms.Padding(10, 35, 0, 0)
        Me.CircularProgress1.SuperscriptText = ""
        Me.CircularProgress1.TabIndex = 2
        Me.CircularProgress1.Text = "Optimize"
        Me.CircularProgress1.TextMargin = New System.Windows.Forms.Padding(0)
        Me.CircularProgress1.Value = 68
        '
        'BoostingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(858, 456)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DioMarqueeProgressBar1)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "BoostingForm"
        Me.Text = "BoostingForm"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Guna2Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DioMarqueeProgressBar1 As Dio.DioMarqueeProgressBar
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Guna2VScrollBar1 As Guna.UI2.WinForms.Guna2VScrollBar
    Friend WithEvents PanelContainer As PanelFX
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Button2 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2Button1 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents CircularProgress1 As CircularProgress
End Class
