Namespace Core
    Public Class Instances

        Public Shared BackColor As Color = Color.FromArgb(32, 34, 37) 'Color.FromArgb(38, 38, 38)
        Public Shared ForeColor As Color = Color.Black

        Public Shared MainInstance As Form = Nothing

        Public Shared FileLogger As New List(Of String)
        Public Shared ProcessLogger As New List(Of String)
        Public Shared ErrorsLogger As New List(Of String)

        Public Shared Protection As Boolean = True

        Public Enum WarnLevel
            Danger = 0
            Warning = 1
            Sure = 2
        End Enum

        Public Enum TaskType
            Quick
            Full
            Custom
        End Enum

    End Class
End Namespace

