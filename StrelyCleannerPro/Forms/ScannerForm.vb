Public Class ScanForm

#Region " Declare "

    Private Malware As MalScanForm = Nothing
    Private Cleaner As CleanerForm = Nothing
    Private Regedit As RegeditForm = Nothing

#End Region

    Public Sub New()

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ScannerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Me.Refresh()
        StartUI()
    End Sub

#Region " GUI "

    Private Sub ScanForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Public Sub StartUI()
        Malware = New MalScanForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        ' Cleaner = New CleanerForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        ' Regedit = New RegeditForm With {.TopLevel = False, .Dock = DockStyle.Fill, .Visible = False}
        Panel2.Controls.Add(Malware)
        ' Panel2.Controls.AddRange({Malware, Cleaner, Regedit})

        MalwareButton.Checked = True
    End Sub

    Private Sub Tabs_Controller(sender As Object, e As EventArgs) Handles MalwareButton.CheckedChanged, CleanerButton.CheckedChanged, RegeditButton.CheckedChanged
        Dim TypeController As Guna.UI2.WinForms.Guna2Button = DirectCast(sender, Guna.UI2.WinForms.Guna2Button)

        If TypeController.Checked = True Then
            TypeController.ImageSize = New Size(30, 30)
            Dim TargetPoint As Point = New Point(Guna2VSeparator1.Location.X, TypeController.Location.Y + 5)
            Dim RestDirectional As String = "y"
            For i As Integer = 0 To 2
                Dim YPOINT As Integer = Guna2VSeparator1.Location.Y
                Dim RestNumb As Integer = 0
                RestNumb += 2

                If YPOINT > TargetPoint.Y Then
                    If RestDirectional = "y" Then RestDirectional = "-"
                    If RestDirectional = "+" Then Guna2VSeparator1.Location = New Point(Guna2VSeparator1.Location.X, TargetPoint.Y)
                    Guna2VSeparator1.Location = New Point(Guna2VSeparator1.Location.X, Guna2VSeparator1.Location.Y - RestNumb)
                ElseIf YPOINT < TargetPoint.Y Then
                    If RestDirectional = "y" Then RestDirectional = "+"
                    If RestDirectional = "-" Then Guna2VSeparator1.Location = New Point(Guna2VSeparator1.Location.X, TargetPoint.X)
                    Guna2VSeparator1.Location = New Point(Guna2VSeparator1.Location.X, Guna2VSeparator1.Location.Y + RestNumb)
                ElseIf YPOINT = TargetPoint.Y Then
                    Exit For
                End If

                i -= 1
                Application.DoEvents()
            Next

        Else
            TypeController.ImageSize = New Size(20, 20)
        End If

        Select Case TypeController.Name
            Case MalwareButton.Name : Malware.Visible = TypeController.Checked
            Case CleanerButton.Name : Cleaner.Visible = TypeController.Checked
            Case RegeditButton.Name : Regedit.Visible = TypeController.Checked
        End Select

    End Sub

#End Region


End Class