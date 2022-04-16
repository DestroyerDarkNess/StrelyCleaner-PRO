Public Class BoosterItem

    Public Property OptionName As String = String.Empty
    Public Property Description As String = String.Empty

    Private Sub BoosterItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label1.Text = Description
        Guna2HtmlLabel2.Text = OptionName '.Replace("Enable", "").Replace("Disable", "")
        Me.Visible = True
    End Sub

    Public Function GetSwitch() As Boolean
        Return Guna2ToggleSwitch1.Checked
    End Function

    Public Sub SetSwitch(ByVal State As Boolean)
        Guna2ToggleSwitch1.Checked = State
    End Sub

    Public Sub IsLoading(ByVal State As Boolean, Optional ByVal StateINt As Integer = 1)
        Me.BeginInvoke(Sub()
                           Progress1.Visible = State
                           Progress1.Value = StateINt
                           Guna2ToggleSwitch1.Visible = Not State
                       End Sub)
    End Sub


End Class
