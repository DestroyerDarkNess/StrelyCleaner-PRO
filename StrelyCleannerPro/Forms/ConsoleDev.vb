Public Class ConsoleDev

    Dim Istested As Boolean = False
    Private Sub ConsoleDev_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        TestingConsole(True)
    End Sub

    Public Sub StartConsole(Optional AppArgs As String = "")
        ConsoleHostV21.Unsecure_Initialize(False, AppArgs)
    End Sub

    Dim IntCount As Integer = 0

    Public Sub WriteLog(ByVal ErrorInfo As Core.ErrorLog, Optional ByVal ColEx As ConsoleColor = ConsoleColor.White)
        If IntCount >= 2000 Then
            IntCount = 0
            Console.Clear()
        Else
            IntCount += 1
        End If
        Dim Header As String = ErrorInfo.SigInfo
        If Header = String.Empty Then
            Header = "[" & Header & "]"
        Else
            Header = "» "
        End If
        Console.WriteLine(Header & " " & ErrorInfo.MessageError.ToString)
    End Sub

    Private Sub ConfigButton_Click(sender As Object, e As EventArgs) Handles ConfigButton.Click
        DirectCast(Core.Instances.MainInstance, Form1).IsShow = False
        Me.Hide()
    End Sub


    Private Sub TestingConsole(Optional ByVal ClearFinalize As Boolean = False)
        Try
            For Each color As ConsoleColor In [Enum].GetValues(GetType(ConsoleColor))
                Console.ForegroundColor = color
                Console.WriteLine("Foreground color set to " & color)
            Next

            Console.WriteLine("=====================================")


            Console.ForegroundColor = ConsoleColor.White

            For Each color As ConsoleColor In [Enum].GetValues(GetType(ConsoleColor))
                Console.BackgroundColor = color

                Console.WriteLine("Background color set to " & color)


            Next

            Console.WriteLine("=====================================")

            Console.ResetColor()
            If ClearFinalize = True Then Console.Clear()
        Catch ex As Exception
            'Core.ErrorLogger.LogError("Console.Testing", ex.Message, ex.StackTrace)
        End Try
    End Sub


End Class