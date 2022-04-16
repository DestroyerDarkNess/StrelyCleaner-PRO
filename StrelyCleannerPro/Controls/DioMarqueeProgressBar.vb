Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports Timer = System.Timers.Timer

Namespace Dio

    Friend Structure DioDefaults
        Public Shared ReadOnly Property DefaultBackColor As Color = Color.CornflowerBlue
        Public Shared ReadOnly Property DefaultForeColor As Color = Color.White
        Public Shared ReadOnly Property DefaultFont As Font = New Font(New FontFamily("Segoe UI"), 8.25F)
        Public Shared ReadOnly Property DioDefaultshadowColor As Color = Color.FromArgb(200, 50, 50, 50)
        Public Shared ReadOnly Property DefaultBorderColor As Color = Color.FromArgb(100, 70, 70, 70)
        Public Shared ReadOnly Property DefaultDarkForeColor As Color = Color.FromArgb(200, 45, 45, 45)
    End Structure

    Public Class DioMarqueeProgressBar
        Inherits Control

        <Category("Appearance")>
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Bindable(False)>
        Public Property _increment As Integer = 1
        Private ReadOnly timer As Timer
        Private _marqueePos As Integer
        Private _marqueeWidth As Integer
        Private _marqueeWidthConst As Integer
        Private _overlapColor As Color = Color.FromArgb(0, 0, 0, 0)

        Public Sub New()
            timer = New Timer()
            timer.Interval = 12
            AddHandler timer.Elapsed, Sub(sender, args) Callback()
            Size = New Size(150, 14)
            DrawBorder = False
            BorderColor = DioDefaults.DefaultBorderColor
            Font = DioDefaults.DefaultFont
            AddHandler HandleCreated, AddressOf DoubleBuffer
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.CacheText Or ControlStyles.DoubleBuffer Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
            BackColor = Color.White
            ForeColor = DioDefaults.DefaultBackColor
            MarqueeWidth = 50
            MarqueeXPos = 20
            MarqueeVisible = True
            _marqueeWidth = MarqueeWidth
            _marqueePos = MarqueeXPos
        End Sub

        Private Sub DoubleBuffer()
            DoubleBuffered = True
        End Sub

        <Category("Appearance")>
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Bindable(False)>
        Public Property BorderColor As Color
        <Category("Appearance")>
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Bindable(False)>
        Public Property DrawBorder As Boolean
        <Category("Appearance")>
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Bindable(False)>
        Public Property MarqueeVisible As Boolean

        <Category("Appearance")>
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Bindable(False)>
        Public Property MarqueeWidth As Integer
            Get
                Return _marqueeWidthConst
            End Get
            Set(ByVal value As Integer)
                _marqueeWidthConst = value
                _marqueeWidth = value
            End Set
        End Property

        <Category("Appearance")>
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Bindable(False)>
        Public Property MarqueeXPos As Integer

        Protected Overrides Sub OnPaint(ByVal pevent As PaintEventArgs)
            Dim g As Graphics = pevent.Graphics
            g.Clear(Parent.BackColor)
            g.SmoothingMode = SmoothingMode.HighQuality
            g.InterpolationMode = InterpolationMode.HighQualityBicubic

            Using borderPen As Pen = New Pen(BorderColor)

                Using backColorBrush As SolidBrush = New SolidBrush(BackColor), foreColorBrush As SolidBrush = New SolidBrush(ForeColor)
                    g.FillRectangle(backColorBrush, 0, 0, Width - 1, Height - 1)
                    If MarqueeVisible Then g.FillRectangle(foreColorBrush, MarqueeXPos, 0, _marqueeWidth, Height - 1)
                    If DrawBorder = True Then g.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1)
                End Using
            End Using
        End Sub

        Public Sub Start()
            MarqueeVisible = True
            timer.Start()
        End Sub

        Public Sub [Stop]()
            timer.[Stop]()
        End Sub

        Private Sub Callback()
            If MarqueeXPos + MarqueeWidth >= Width Then _marqueeWidth = Width - MarqueeXPos - 2

            If _marqueePos < 0 Then
                MarqueeXPos = 0
                _marqueeWidth += _increment
                _marqueePos += _increment
            ElseIf MarqueeXPos >= Width Then
                _marqueePos = -MarqueeWidth
            Else
                MarqueeXPos += _increment
            End If

            Invalidate()
        End Sub
    End Class
End Namespace
