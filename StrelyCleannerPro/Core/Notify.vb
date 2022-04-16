Imports System.Runtime.CompilerServices

Public Class Notify

#Region " Enums "

    Enum actionEnum
        wait
        start
        close
    End Enum

#End Region

#Region " Declare "

    Private x, y As Integer
    Private Timer1 As New Timer
    Public FormNoty As Form = Nothing
    Private action As actionEnum = actionEnum.start

#End Region

#Region " Public Methods "

    Dim CloseEnd As Boolean = True

    Public Sub New(ByVal FormN As Form, Optional ByVal CloseEx As Boolean = True)
        CloseEnd = CloseEx
        FormNoty = FormN
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
        '   AddHandler FormN.FormClosing, AddressOf Form_FormClosing
    End Sub

    Public Sub Show()
        If FormNoty IsNot Nothing Then
            FormNoty.Opacity = 0
            FormNoty.StartPosition = FormStartPosition.Manual
            Dim fname As String
            For i As Integer = 1 To 10
                fname = "alert" & i.ToString
                Dim f As Form = Application.OpenForms.Item(fname)
                If f Is Nothing Then
                    FormNoty.Name = fname
                    x = Screen.PrimaryScreen.WorkingArea.Width - FormNoty.Width + 15
                    y = Screen.PrimaryScreen.WorkingArea.Height - FormNoty.Height * i - 5 * i
                    FormNoty.Location = New Point(x, y)
                    Exit For
                End If
            Next
            x = Screen.PrimaryScreen.WorkingArea.Width - FormNoty.Width - 5

            '  Me.TopMost = True
            '  Me.ShowIcon = False
            '  Me.ShowInTaskbar = False
            FormNoty.Show()
            Timer1.Interval = 1
            Timer1.Start()
        End If
    End Sub

    Public Sub CloseEx()
        action = actionEnum.close
        Me.Timer1.Enabled = True
    End Sub

#End Region

#Region " Private Methods "

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)
        If FormNoty IsNot Nothing Then
            Select Case action
                Case actionEnum.start
                    Timer1.Interval = 1
                    FormNoty.Opacity += 0.1
                    If x < FormNoty.Location.X Then
                        FormNoty.Left -= 1
                    Else
                        If FormNoty.Opacity = 1 Then
                            action = actionEnum.wait
                        End If
                    End If
                Case actionEnum.wait
                    If CloseEnd = True Then
                        Timer1.Interval = 5000
                        action = actionEnum.close
                    Else
                        Timer1.Enabled = False
                    End If
                Case actionEnum.close
                    Timer1.Interval = 1
                    FormNoty.Opacity -= 0.1
                    FormNoty.Left -= 3
                    If FormNoty.Opacity = 0 Then
                        FormNoty.Close()
                    End If
            End Select
        End If
    End Sub

#End Region

End Class
