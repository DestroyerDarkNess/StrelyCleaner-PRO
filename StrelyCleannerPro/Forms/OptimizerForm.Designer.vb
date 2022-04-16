<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OptimizerForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptimizerForm))
        Me.DioMarqueeProgressBar1 = New StrelyCleannerPro.Dio.DioMarqueeProgressBar()
        Me.PanelContainer = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ConfigButton = New Guna.UI2.WinForms.Guna2Button()
        Me.BoostingButton = New Guna.UI2.WinForms.Guna2Button()
        Me.SystemInfoButton = New Guna.UI2.WinForms.Guna2Button()
        Me.PanelContainer.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.DioMarqueeProgressBar1.Size = New System.Drawing.Size(907, 5)
        Me.DioMarqueeProgressBar1.TabIndex = 2
        Me.DioMarqueeProgressBar1.Text = "DioMarqueeProgressBar1"
        '
        'PanelContainer
        '
        Me.PanelContainer.Controls.Add(Me.Panel2)
        Me.PanelContainer.Controls.Add(Me.Panel1)
        Me.PanelContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelContainer.Location = New System.Drawing.Point(0, 5)
        Me.PanelContainer.Name = "PanelContainer"
        Me.PanelContainer.Size = New System.Drawing.Size(907, 456)
        Me.PanelContainer.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(49, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(858, 456)
        Me.Panel2.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.Panel1.Controls.Add(Me.ConfigButton)
        Me.Panel1.Controls.Add(Me.BoostingButton)
        Me.Panel1.Controls.Add(Me.SystemInfoButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(49, 456)
        Me.Panel1.TabIndex = 1
        '
        'ConfigButton
        '
        Me.ConfigButton.BackColor = System.Drawing.Color.Transparent
        Me.ConfigButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.ConfigButton.CheckedState.Parent = Me.ConfigButton
        Me.ConfigButton.CustomImages.Parent = Me.ConfigButton
        Me.ConfigButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.ConfigButton.FillColor = System.Drawing.Color.Transparent
        Me.ConfigButton.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.ConfigButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.ConfigButton.HoverState.Parent = Me.ConfigButton
        Me.ConfigButton.Image = CType(resources.GetObject("ConfigButton.Image"), System.Drawing.Image)
        Me.ConfigButton.ImageSize = New System.Drawing.Size(25, 25)
        Me.ConfigButton.Location = New System.Drawing.Point(0, 74)
        Me.ConfigButton.Name = "ConfigButton"
        Me.ConfigButton.ShadowDecoration.Parent = Me.ConfigButton
        Me.ConfigButton.Size = New System.Drawing.Size(49, 37)
        Me.ConfigButton.TabIndex = 8
        '
        'BoostingButton
        '
        Me.BoostingButton.BackColor = System.Drawing.Color.Transparent
        Me.BoostingButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.BoostingButton.CheckedState.Parent = Me.BoostingButton
        Me.BoostingButton.CustomImages.Parent = Me.BoostingButton
        Me.BoostingButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.BoostingButton.FillColor = System.Drawing.Color.Transparent
        Me.BoostingButton.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.BoostingButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.BoostingButton.HoverState.Parent = Me.BoostingButton
        Me.BoostingButton.Image = CType(resources.GetObject("BoostingButton.Image"), System.Drawing.Image)
        Me.BoostingButton.Location = New System.Drawing.Point(0, 37)
        Me.BoostingButton.Name = "BoostingButton"
        Me.BoostingButton.ShadowDecoration.Parent = Me.BoostingButton
        Me.BoostingButton.Size = New System.Drawing.Size(49, 37)
        Me.BoostingButton.TabIndex = 7
        '
        'SystemInfoButton
        '
        Me.SystemInfoButton.BackColor = System.Drawing.Color.Transparent
        Me.SystemInfoButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.SystemInfoButton.CheckedState.Parent = Me.SystemInfoButton
        Me.SystemInfoButton.CustomImages.Parent = Me.SystemInfoButton
        Me.SystemInfoButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.SystemInfoButton.FillColor = System.Drawing.Color.Transparent
        Me.SystemInfoButton.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.SystemInfoButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.SystemInfoButton.HoverState.Parent = Me.SystemInfoButton
        Me.SystemInfoButton.Image = CType(resources.GetObject("SystemInfoButton.Image"), System.Drawing.Image)
        Me.SystemInfoButton.Location = New System.Drawing.Point(0, 0)
        Me.SystemInfoButton.Name = "SystemInfoButton"
        Me.SystemInfoButton.ShadowDecoration.Parent = Me.SystemInfoButton
        Me.SystemInfoButton.Size = New System.Drawing.Size(49, 37)
        Me.SystemInfoButton.TabIndex = 6
        '
        'OptimizerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(907, 461)
        Me.Controls.Add(Me.PanelContainer)
        Me.Controls.Add(Me.DioMarqueeProgressBar1)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "OptimizerForm"
        Me.Text = "OptimizerForm"
        Me.PanelContainer.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DioMarqueeProgressBar1 As Dio.DioMarqueeProgressBar
    Friend WithEvents PanelContainer As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ConfigButton As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BoostingButton As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents SystemInfoButton As Guna.UI2.WinForms.Guna2Button
End Class
