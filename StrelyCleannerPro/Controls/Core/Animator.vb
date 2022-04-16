Namespace Controls.Core
    Public Class Animator

        Private WithEvents Timer As New System.Timers.Timer
        ''' <summary>
        ''' Start value
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property StartValue As Integer
        ''' <summary>
        ''' Stop value
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property [StopValue] As Integer
        ''' <summary>
        ''' Current Time in milli seconds
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property Time As Integer
        ''' <summary>
        ''' Durration of Animation in milli seconds
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property Duration As Integer
        ''' <summary>
        ''' Funktion that defines value calculation
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property [Function] As Func(Of Single, Single)

        ''' <summary>
        ''' New linear Animator
        ''' </summary>
        ''' <param name="Start">Start value</param>
        ''' <param name="[Stop]">Stop Value</param>
        ''' <param name="Duration">Duration in milli seconds</param>
        Public Sub New(Start As Integer, [Stop] As Integer, Duration As Integer)
            Me.New(Start, [Stop], Duration, Function(x) x)
        End Sub
        ''' <summary>
        ''' New custom Animator
        ''' </summary>
        ''' <param name="Start">Start value</param>
        ''' <param name="[Stop]">Stop value</param>
        ''' <param name="Duration">Duration in milli seconds</param>
        ''' <param name="[Function]">Funktion that defines value calculation</param>
        Public Sub New(Start As Integer, [Stop] As Integer, Duration As Integer, [Function] As Func(Of Single, Single))
            Me.StartValue = Start
            Me.[StopValue] = [Stop]
            Me.Duration = Duration
            Me.[Function] = [Function]
            Time = 0
            Timer.Interval = 1
            t = DateTime.Now.TimeOfDay
        End Sub

        Dim t As TimeSpan

        Public Sub Start()
            Timer.Enabled = True
        End Sub
        Public Sub Cancel()
            Timer.Stop()
            Timer.Enabled = False
            Timer.Close()
        End Sub

        Private Sub Timer_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles Timer.Elapsed
            Dim t = If(Duration = 0, 0, Time / Duration)
            RaiseEvent ValueChanged(StartValue + [Function](t) * (StopValue - StartValue))
            _Time += (DateTime.Now.TimeOfDay - Me.t).TotalMilliseconds 'Timer.Interval
            Me.t = DateTime.Now.TimeOfDay
            If Time >= Duration Then
                RaiseEvent ValueChanged(StopValue)
                Cancel()
                RaiseEvent Finished()
            End If
        End Sub

        Public Event ValueChanged(value As Integer)
        Public Event Finished()

    End Class
End Namespace