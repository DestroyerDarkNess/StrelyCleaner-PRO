<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ManagerForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManagerForm))
        Me.PanelContainer = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.StartupManagerForm = New Guna.UI2.WinForms.Guna2Button()
        Me.BrowserExtensionButton = New Guna.UI2.WinForms.Guna2Button()
        Me.ProcessManagerButton = New Guna.UI2.WinForms.Guna2Button()
        Me.DioMarqueeProgressBar1 = New StrelyCleannerPro.Dio.DioMarqueeProgressBar()
        Me.Guna2HtmlToolTip1 = New Guna.UI2.WinForms.Guna2HtmlToolTip()
        Me.PanelContainer.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelContainer
        '
        Me.PanelContainer.Controls.Add(Me.Panel2)
        Me.PanelContainer.Controls.Add(Me.Panel1)
        Me.PanelContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelContainer.Location = New System.Drawing.Point(0, 5)
        Me.PanelContainer.Name = "PanelContainer"
        Me.PanelContainer.Size = New System.Drawing.Size(907, 456)
        Me.PanelContainer.TabIndex = 5
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
        Me.Panel1.Controls.Add(Me.StartupManagerForm)
        Me.Panel1.Controls.Add(Me.BrowserExtensionButton)
        Me.Panel1.Controls.Add(Me.ProcessManagerButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(49, 456)
        Me.Panel1.TabIndex = 1
        '
        'StartupManagerForm
        '
        Me.StartupManagerForm.BackColor = System.Drawing.Color.Transparent
        Me.StartupManagerForm.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.StartupManagerForm.CheckedState.Parent = Me.StartupManagerForm
        Me.StartupManagerForm.CustomImages.Parent = Me.StartupManagerForm
        Me.StartupManagerForm.Dock = System.Windows.Forms.DockStyle.Top
        Me.StartupManagerForm.FillColor = System.Drawing.Color.Transparent
        Me.StartupManagerForm.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.StartupManagerForm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.StartupManagerForm.HoverState.Parent = Me.StartupManagerForm
        Me.StartupManagerForm.Image = CType(resources.GetObject("StartupManagerForm.Image"), System.Drawing.Image)
        Me.StartupManagerForm.Location = New System.Drawing.Point(0, 74)
        Me.StartupManagerForm.Name = "StartupManagerForm"
        Me.StartupManagerForm.ShadowDecoration.Parent = Me.StartupManagerForm
        Me.StartupManagerForm.Size = New System.Drawing.Size(49, 37)
        Me.StartupManagerForm.TabIndex = 8
        Me.Guna2HtmlToolTip1.SetToolTip(Me.StartupManagerForm, "Startup Manager")
        '
        'BrowserExtensionButton
        '
        Me.BrowserExtensionButton.BackColor = System.Drawing.Color.Transparent
        Me.BrowserExtensionButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.BrowserExtensionButton.CheckedState.Parent = Me.BrowserExtensionButton
        Me.BrowserExtensionButton.CustomImages.Parent = Me.BrowserExtensionButton
        Me.BrowserExtensionButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.BrowserExtensionButton.FillColor = System.Drawing.Color.Transparent
        Me.BrowserExtensionButton.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.BrowserExtensionButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.BrowserExtensionButton.HoverState.Parent = Me.BrowserExtensionButton
        Me.BrowserExtensionButton.Image = CType(resources.GetObject("BrowserExtensionButton.Image"), System.Drawing.Image)
        Me.BrowserExtensionButton.Location = New System.Drawing.Point(0, 37)
        Me.BrowserExtensionButton.Name = "BrowserExtensionButton"
        Me.BrowserExtensionButton.ShadowDecoration.Parent = Me.BrowserExtensionButton
        Me.BrowserExtensionButton.Size = New System.Drawing.Size(49, 37)
        Me.BrowserExtensionButton.TabIndex = 7
        Me.Guna2HtmlToolTip1.SetToolTip(Me.BrowserExtensionButton, "Browser Extension Manager")
        '
        'ProcessManagerButton
        '
        Me.ProcessManagerButton.BackColor = System.Drawing.Color.Transparent
        Me.ProcessManagerButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.ProcessManagerButton.CheckedState.Parent = Me.ProcessManagerButton
        Me.ProcessManagerButton.CustomImages.Parent = Me.ProcessManagerButton
        Me.ProcessManagerButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.ProcessManagerButton.FillColor = System.Drawing.Color.Transparent
        Me.ProcessManagerButton.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.ProcessManagerButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.ProcessManagerButton.HoverState.Parent = Me.ProcessManagerButton
        Me.ProcessManagerButton.Image = CType(resources.GetObject("ProcessManagerButton.Image"), System.Drawing.Image)
        Me.ProcessManagerButton.Location = New System.Drawing.Point(0, 0)
        Me.ProcessManagerButton.Name = "ProcessManagerButton"
        Me.ProcessManagerButton.ShadowDecoration.Parent = Me.ProcessManagerButton
        Me.ProcessManagerButton.Size = New System.Drawing.Size(49, 37)
        Me.ProcessManagerButton.TabIndex = 6
        Me.Guna2HtmlToolTip1.SetToolTip(Me.ProcessManagerButton, "Process Manager")
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
        Me.DioMarqueeProgressBar1.TabIndex = 4
        Me.DioMarqueeProgressBar1.Text = "DioMarqueeProgressBar1"
        '
        'Guna2HtmlToolTip1
        '
        Me.Guna2HtmlToolTip1.AllowLinksHandling = True
        Me.Guna2HtmlToolTip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.Guna2HtmlToolTip1.ForeColor = System.Drawing.Color.White
        Me.Guna2HtmlToolTip1.MaximumSize = New System.Drawing.Size(0, 0)
        Me.Guna2HtmlToolTip1.UseGdiPlusTextRendering = True
        '
        'ManagerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(907, 461)
        Me.Controls.Add(Me.PanelContainer)
        Me.Controls.Add(Me.DioMarqueeProgressBar1)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ManagerForm"
        Me.Text = "ManagerForm"
        Me.PanelContainer.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelContainer As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents BrowserExtensionButton As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents ProcessManagerButton As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents DioMarqueeProgressBar1 As Dio.DioMarqueeProgressBar
    Friend WithEvents Guna2HtmlToolTip1 As Guna.UI2.WinForms.Guna2HtmlToolTip
    Friend WithEvents StartupManagerForm As Guna.UI2.WinForms.Guna2Button
End Class
