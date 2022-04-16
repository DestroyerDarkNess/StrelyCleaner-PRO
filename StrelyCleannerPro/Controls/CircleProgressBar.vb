Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel

Namespace Apple
    Public Class CircleProgressBar
        Inherits Control

        Private _progress As Single = 0F
        Private _Wpen As Single = 1
        Private _Npen As Single = 5
        Private _Fwidth As Single = 10
        <Description("进度条颜色")>
        Public Property CircleColor As Color

        <Description("外圈粗度")>
        Public Property WpenThin As Single
            Get
                Return _Wpen
            End Get
            Set(ByVal value As Single)
                _Wpen = value
            End Set
        End Property

        <Description("内圈粗度")>
        Public Property NpenThin As Single
            Get
                Return _Npen
            End Get
            Set(ByVal value As Single)
                _Npen = value
            End Set
        End Property

        <Description("内心方形边长")>
        Public Property Fwitdh As Single
            Get
                Return _Fwidth
            End Get
            Set(ByVal value As Single)
                _Fwidth = value
            End Set
        End Property

        Public Sub PaintProgress(ByVal e As PaintEventArgs)
            Dim x As Single = Me.Width / 2
            Dim y As Single = Me.Height / 2
            Dim Wr As Single = x - WpenThin / 2
            Dim Nr As Single = x - NpenThin / 2
            Dim Wx As Integer = CInt((x - Wr))
            Dim Wy As Integer = CInt((y - Wr))
            Dim Nx As Integer = CInt((x - Nr))
            Dim Ny As Integer = CInt((y - Nr))
            Dim Fy As Integer = CInt((y - Fwitdh / 2))
            Dim Fx As Integer = CInt((x - Fwitdh / 2))
            Dim dc As Graphics = Me.CreateGraphics()
            dc.Clear(Me.BackColor)
            Dim Wpen As Pen = New Pen(CircleColor, WpenThin)
            Dim Npen As Pen = New Pen(CircleColor, NpenThin)
            Dim Fbrush As Brush = New SolidBrush(CircleColor)
            dc.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            Dim startAngle As Single = -90
            Dim sweepAngle As Single = Progress / 100 * 360
            Dim Wrec As Rectangle = New Rectangle(Wx, Wy, 2 * CInt(Wr), 2 * CInt(Wr))
            Dim Nrec As Rectangle = New Rectangle(Nx, Ny, 2 * CInt(Nr), 2 * CInt(Nr))
            Dim Frec As Rectangle = New Rectangle(Fx, Fy, CInt(Fwitdh), CInt(Fwitdh))
            dc.DrawEllipse(Wpen, Wrec)
            dc.FillRectangle(Fbrush, Frec)
            dc.DrawArc(Npen, Nrec, startAngle, sweepAngle)
        End Sub

        Public Property Progress As Single
            Get
                Return _progress
            End Get
            Set(ByVal value As Single)

                If _progress <> value AndAlso value >= 0 AndAlso value <= 100 Then
                    _progress = value
                    OnProgressChanged()
                End If
            End Set
        End Property

        Protected Overridable Sub OnProgressChanged()
            Me.Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            PaintProgress(e)
            MyBase.OnPaint(e)
        End Sub
    End Class
End Namespace
