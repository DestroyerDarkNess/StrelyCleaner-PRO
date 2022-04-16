Imports StrelyCleannerPro.Core.Optimizer

Public Class CounterFixerDialog

    Private Sub CounterFixerDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Core.Instances.MainInstance IsNot Nothing Then Me.Location = Core.Utils.CenterForm(Core.Instances.MainInstance, Me)
    End Sub

    Private Sub CounterFixerDialog_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim Asynctask As New Task(New Action(Async Sub()
                                                 Me.Guna2ProgressBar1.Value = 30
                                                 Utilities.RunCommand("cd C:\Windows\system32 & lodctr /r")
                                                 Me.Guna2ProgressBar1.Value = 60
                                                 Utilities.RunCommand("cd C:\Windows\SysWOW64 & lodctr /r")
                                                 Me.Guna2ProgressBar1.Value = 90
                                                 System.Threading.Thread.Sleep(1000)
                                                 Me.Guna2ProgressBar1.Value = 100
                                                 System.Threading.Thread.Sleep(2000)

                                                 Me.BeginInvoke(Sub()
                                                                    Me.Close()
                                                                End Sub)

                                             End Sub), TaskCreationOptions.PreferFairness)

        Asynctask.Start()

    End Sub

End Class