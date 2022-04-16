<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScanForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScanForm))
        Me.PanelContainer = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Guna2VSeparator1 = New Guna.UI2.WinForms.Guna2VSeparator()
        Me.RegeditButton = New Guna.UI2.WinForms.Guna2Button()
        Me.CleanerButton = New Guna.UI2.WinForms.Guna2Button()
        Me.MalwareButton = New Guna.UI2.WinForms.Guna2Button()
        Me.PanelContainer.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelContainer
        '
        Me.PanelContainer.Controls.Add(Me.Panel2)
        Me.PanelContainer.Controls.Add(Me.Panel1)
        Me.PanelContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelContainer.Location = New System.Drawing.Point(0, 0)
        Me.PanelContainer.Name = "PanelContainer"
        Me.PanelContainer.Size = New System.Drawing.Size(907, 461)
        Me.PanelContainer.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(49, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(858, 461)
        Me.Panel2.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Guna2VSeparator1)
        Me.Panel1.Controls.Add(Me.RegeditButton)
        Me.Panel1.Controls.Add(Me.CleanerButton)
        Me.Panel1.Controls.Add(Me.MalwareButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(49, 461)
        Me.Panel1.TabIndex = 1
        '
        'Guna2VSeparator1
        '
        Me.Guna2VSeparator1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2VSeparator1.Location = New System.Drawing.Point(3, 117)
        Me.Guna2VSeparator1.Name = "Guna2VSeparator1"
        Me.Guna2VSeparator1.Size = New System.Drawing.Size(5, 20)
        Me.Guna2VSeparator1.TabIndex = 0
        '
        'RegeditButton
        '
        Me.RegeditButton.BackColor = System.Drawing.Color.Transparent
        Me.RegeditButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.RegeditButton.CheckedState.Parent = Me.RegeditButton
        Me.RegeditButton.CustomImages.Parent = Me.RegeditButton
        Me.RegeditButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.RegeditButton.FillColor = System.Drawing.Color.Transparent
        Me.RegeditButton.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.RegeditButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.RegeditButton.HoverState.Parent = Me.RegeditButton
        Me.RegeditButton.Image = CType(resources.GetObject("RegeditButton.Image"), System.Drawing.Image)
        Me.RegeditButton.Location = New System.Drawing.Point(0, 74)
        Me.RegeditButton.Name = "RegeditButton"
        Me.RegeditButton.ShadowDecoration.Parent = Me.RegeditButton
        Me.RegeditButton.Size = New System.Drawing.Size(49, 37)
        Me.RegeditButton.TabIndex = 8
        Me.RegeditButton.Visible = False
        '
        'CleanerButton
        '
        Me.CleanerButton.BackColor = System.Drawing.Color.Transparent
        Me.CleanerButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.CleanerButton.CheckedState.Parent = Me.CleanerButton
        Me.CleanerButton.CustomImages.Parent = Me.CleanerButton
        Me.CleanerButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.CleanerButton.FillColor = System.Drawing.Color.Transparent
        Me.CleanerButton.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.CleanerButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.CleanerButton.HoverState.Parent = Me.CleanerButton
        Me.CleanerButton.Image = CType(resources.GetObject("CleanerButton.Image"), System.Drawing.Image)
        Me.CleanerButton.Location = New System.Drawing.Point(0, 37)
        Me.CleanerButton.Name = "CleanerButton"
        Me.CleanerButton.ShadowDecoration.Parent = Me.CleanerButton
        Me.CleanerButton.Size = New System.Drawing.Size(49, 37)
        Me.CleanerButton.TabIndex = 7
        Me.CleanerButton.Visible = False
        '
        'MalwareButton
        '
        Me.MalwareButton.BackColor = System.Drawing.Color.Transparent
        Me.MalwareButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
        Me.MalwareButton.CheckedState.Parent = Me.MalwareButton
        Me.MalwareButton.CustomImages.Parent = Me.MalwareButton
        Me.MalwareButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.MalwareButton.FillColor = System.Drawing.Color.Transparent
        Me.MalwareButton.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.MalwareButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.MalwareButton.HoverState.Parent = Me.MalwareButton
        Me.MalwareButton.Image = CType(resources.GetObject("MalwareButton.Image"), System.Drawing.Image)
        Me.MalwareButton.Location = New System.Drawing.Point(0, 0)
        Me.MalwareButton.Name = "MalwareButton"
        Me.MalwareButton.ShadowDecoration.Parent = Me.MalwareButton
        Me.MalwareButton.Size = New System.Drawing.Size(49, 37)
        Me.MalwareButton.TabIndex = 6
        '
        'ScanForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(907, 461)
        Me.Controls.Add(Me.PanelContainer)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ScanForm"
        Me.Text = "ScannerForm"
        Me.PanelContainer.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelContainer As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents RegeditButton As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents CleanerButton As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents MalwareButton As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2VSeparator1 As Guna.UI2.WinForms.Guna2VSeparator
End Class
