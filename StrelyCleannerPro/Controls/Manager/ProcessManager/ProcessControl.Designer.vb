<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProcessControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProcessControl))
        Me.GunaAdvenceButton2 = New Guna.UI2.WinForms.Guna2Button()
        Me.LogInContextMenu1 = New StrelyCleannerPro.LogInContextMenu()
        Me.OpenFileLocationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TerminateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BoosterToolTip1 = New Guna.UI2.WinForms.Guna2HtmlToolTip()
        Me.LogInContextMenu1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GunaAdvenceButton2
        '
        Me.GunaAdvenceButton2.Animated = True
        Me.GunaAdvenceButton2.BackColor = System.Drawing.Color.Transparent
        Me.GunaAdvenceButton2.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.GunaAdvenceButton2.CheckedState.Parent = Me.GunaAdvenceButton2
        Me.GunaAdvenceButton2.ContextMenuStrip = Me.LogInContextMenu1
        Me.GunaAdvenceButton2.CustomBorderColor = System.Drawing.Color.Transparent
        Me.GunaAdvenceButton2.CustomBorderThickness = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.GunaAdvenceButton2.CustomImages.Parent = Me.GunaAdvenceButton2
        Me.GunaAdvenceButton2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GunaAdvenceButton2.FillColor = System.Drawing.Color.Transparent
        Me.GunaAdvenceButton2.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.GunaAdvenceButton2.ForeColor = System.Drawing.Color.White
        Me.GunaAdvenceButton2.HoverState.Parent = Me.GunaAdvenceButton2
        Me.GunaAdvenceButton2.Image = CType(resources.GetObject("GunaAdvenceButton2.Image"), System.Drawing.Image)
        Me.GunaAdvenceButton2.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.GunaAdvenceButton2.ImageSize = New System.Drawing.Size(30, 30)
        Me.GunaAdvenceButton2.Location = New System.Drawing.Point(0, 0)
        Me.GunaAdvenceButton2.Name = "GunaAdvenceButton2"
        Me.GunaAdvenceButton2.ShadowDecoration.Parent = Me.GunaAdvenceButton2
        Me.GunaAdvenceButton2.Size = New System.Drawing.Size(198, 46)
        Me.GunaAdvenceButton2.TabIndex = 12
        Me.GunaAdvenceButton2.Text = "ProcessName"
        '
        'LogInContextMenu1
        '
        Me.LogInContextMenu1.FontColour = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LogInContextMenu1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LogInContextMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenFileLocationToolStripMenuItem, Me.TerminateToolStripMenuItem})
        Me.LogInContextMenu1.Name = "LogInContextMenu1"
        Me.LogInContextMenu1.ShowImageMargin = False
        Me.LogInContextMenu1.Size = New System.Drawing.Size(149, 48)
        '
        'OpenFileLocationToolStripMenuItem
        '
        Me.OpenFileLocationToolStripMenuItem.Name = "OpenFileLocationToolStripMenuItem"
        Me.OpenFileLocationToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.OpenFileLocationToolStripMenuItem.Text = "Open File Location"
        '
        'TerminateToolStripMenuItem
        '
        Me.TerminateToolStripMenuItem.Name = "TerminateToolStripMenuItem"
        Me.TerminateToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.TerminateToolStripMenuItem.Text = "Terminate"
        '
        'BoosterToolTip1
        '
        Me.BoosterToolTip1.AllowLinksHandling = True
        Me.BoosterToolTip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.BoosterToolTip1.ForeColor = System.Drawing.Color.White
        Me.BoosterToolTip1.MaximumSize = New System.Drawing.Size(0, 0)
        Me.BoosterToolTip1.UseGdiPlusTextRendering = True
        '
        'ProcessControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.Controls.Add(Me.GunaAdvenceButton2)
        Me.Name = "ProcessControl"
        Me.Size = New System.Drawing.Size(198, 46)
        Me.LogInContextMenu1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GunaAdvenceButton2 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents BoosterToolTip1 As Guna.UI2.WinForms.Guna2HtmlToolTip
    Friend WithEvents LogInContextMenu1 As LogInContextMenu
    Friend WithEvents OpenFileLocationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TerminateToolStripMenuItem As ToolStripMenuItem
End Class
