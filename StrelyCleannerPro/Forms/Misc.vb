Public Class Misc

    Private Sub Misc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Core.Instances.MainInstance IsNot Nothing Then Me.Location = Core.Utils.CenterForm(Core.Instances.MainInstance, Me)
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Process.Start(Guna2TextBox1.Text)
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Process.Start("https://www.paypal.me/SalvadorKrilewski")
    End Sub

End Class