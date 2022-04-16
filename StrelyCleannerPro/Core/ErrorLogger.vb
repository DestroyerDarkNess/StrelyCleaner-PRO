
Namespace Core

    Public Class ErrorLog
        Public Property SigInfo As String = String.Empty
        Public Property MessageError As String = String.Empty
        Public Property StackTrace As String = String.Empty
    End Class

    Public Class ErrorLogger

        ' Public Shared Logger As List(Of ErrorLog) = New List(Of ErrorLog)


        '  Public Shared Sub LogError(ByVal SigInfoEx As String, ByVal MessageErrorEx As String, ByVal StackTraceEx As String)
        '  Dim ErrorEx As ErrorLog = New ErrorLog With {.SigInfo = SigInfoEx, .MessageError = MessageErrorEx, .StackTrace = StackTraceEx}
        '  If Core.Instances.MainInstance IsNot Nothing Then DirectCast(Core.Instances.MainInstance, Form1).ConsoleError.WriteLog(ErrorEx)
        '  Logger.Add(ErrorEx)
        '  End Sub

    End Class

End Namespace


