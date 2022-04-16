Imports System.ComponentModel

Public Class OptimizerForm

#Region " Declare "

    Private SystemInfoExo As SystemInfoForm = Nothing
    Private Boosting As BoostingForm = Nothing
    Private Config As ConfigForm = Nothing

#End Region

    Public Sub New()

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub OptimizerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Me.Refresh()
        DioMarqueeProgressBar1.Start()
        StartUI()
    End Sub


#Region " GUI "

    Public Sub StartUI()
        SystemInfoExo = New SystemInfoForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        Boosting = New BoostingForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        Config = New ConfigForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        Panel2.Controls.Add(SystemInfoExo)
        Panel2.Controls.Add(Boosting)
        Panel2.Controls.Add(Config)
        '  Panel2.Controls.AddRange({SystemInfoExo, Boosting, Config})
        SystemInfoButton.Checked = True

        Dim DeployResources As Task(Of Boolean) = Core.Optimizer.Optimize.Deploy


        DioMarqueeProgressBar1.Stop()
        DioMarqueeProgressBar1.Visible = False
    End Sub

    Private Sub OptimizerForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Private Sub Tabs_Controller(sender As Object, e As EventArgs) Handles SystemInfoButton.CheckedChanged, BoostingButton.CheckedChanged, ConfigButton.CheckedChanged
        Dim TypeController As Guna.UI2.WinForms.Guna2Button = DirectCast(sender, Guna.UI2.WinForms.Guna2Button)

        If TypeController.Checked = True Then
            TypeController.ImageSize = New Size(30, 30)
        Else
            TypeController.ImageSize = New Size(20, 20)
        End If

        Select Case TypeController.Name
            Case SystemInfoButton.Name : SystemInfoExo.Visible = TypeController.Checked
            Case BoostingButton.Name : Boosting.Visible = TypeController.Checked
            Case ConfigButton.Name : Config.Visible = TypeController.Checked
        End Select

    End Sub

#End Region


End Class