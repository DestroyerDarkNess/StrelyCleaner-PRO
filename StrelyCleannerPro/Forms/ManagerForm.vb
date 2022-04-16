Public Class ManagerForm

#Region " Declare "

    Private ProcessManager As ProcessManagerForm = Nothing
    Private BrowserExForm As BrowserExtensionForm = Nothing
    Private StartupManager As StartupManagerForm = Nothing

#End Region


    Public Sub New()

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ManagerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Me.Refresh()
        StartUI()
    End Sub


#Region " GUI "

    Public Sub StartUI()
        ProcessManager = New ProcessManagerForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        BrowserExForm = New BrowserExtensionForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        StartupManager = New StartupManagerForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}

        Panel2.Controls.Add(ProcessManager)
        Panel2.Controls.Add(BrowserExForm)
        Panel2.Controls.Add(StartupManager)
        '  Panel2.Controls.AddRange({ProcessManager, BrowserExForm, StartupManager})

        ProcessManagerButton.Checked = True

        DioMarqueeProgressBar1.Stop()
        DioMarqueeProgressBar1.Visible = False
    End Sub

    Private Sub ManagerForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Private Sub Tabs_Controller(sender As Object, e As EventArgs) Handles ProcessManagerButton.CheckedChanged, BrowserExtensionButton.CheckedChanged, StartupManagerForm.CheckedChanged
        Dim TypeController As Guna.UI2.WinForms.Guna2Button = DirectCast(sender, Guna.UI2.WinForms.Guna2Button)

        If TypeController.Checked = True Then
            TypeController.ImageSize = New Size(30, 30)
        Else
            TypeController.ImageSize = New Size(20, 20)
        End If

        Select Case TypeController.Name
            Case ProcessManagerButton.Name : ProcessManager.Visible = TypeController.Checked
            Case BrowserExtensionButton.Name : BrowserExForm.Visible = TypeController.Checked
            Case StartupManagerForm.Name : StartupManager.visible = TypeController.Checked
        End Select

    End Sub

#End Region




End Class