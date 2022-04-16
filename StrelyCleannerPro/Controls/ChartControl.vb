Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class ChartControl
    Inherits Control

    Public Sub New()
        ForeColor = Color.FromArgb(17, 125, 187)
        DoubleBuffered = True
    End Sub

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Property Graph As List(Of Point) = {New Point(1, 1), New Point(2, 3.5)}.ToList() ', New Point(3, 1.1), New Point(4, 7), New Point(5, 7), New Point(6, 1.5), New Point(7, 6), New Point(8, 7.3), New Point(9, 2.6)}.ToList()
    Dim _xunit = 10
    <Browsable(True), DefaultValue(10), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Property XUnit As Integer
        Get
            Return _xunit
        End Get
        Set(value As Integer)
            _xunit = value
            Me.Refresh()
        End Set
    End Property
    Dim _yunit = 10
    <Browsable(True), DefaultValue(10), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Property YUnit As Integer
        Get
            Return _yunit
        End Get
        Set(value As Integer)
            _yunit = value
            Me.Refresh()
        End Set
    End Property
    WithEvents Animatorx As Controls.Core.Animator
    WithEvents Animatory As Controls.Core.Animator
    Dim bufferpos As Point = New Point(0, 0)
    Dim _GraphPosition = New Point(0, 0)
    <Browsable(True), DefaultValue("0; 0"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Property GraphPosition As Point
        Get
            Return _GraphPosition
        End Get
        Set(value As Point)
            If Not Animatorx Is Nothing Then Animatorx.Cancel()
            Animatorx = New Controls.Core.Animator(Me.GraphPosition.X, value.X, 500)
            Animatorx.Start()
            If Not Animatory Is Nothing Then Animatory.Cancel()
            Animatory = New Controls.Core.Animator(Me.GraphPosition.Y, value.Y, 500)
            Animatory.Start()
            _GraphPosition = value
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.InterpolationMode = InterpolationMode.High
        Dim bounds = New Rectangle(0, 0, Me.Width, Me.Height)
        Using pen As New Pen(ForeColor, 1) ' Borders
            g.DrawRectangle(pen, New Rectangle(pen.Width, pen.Width, Me.Width - pen.Width * 2, Me.Height - pen.Width * 2))
        End Using
        Using pen As New Pen(Color.FromArgb(200, ForeColor), 1.5)
            Try
                For i As Integer = 0 To Graph.Count - 1
                    If Not i = Graph.Count - 1 Then
                        Dim point = GetPoint(Graph(i))
                        Dim point2 = GetPoint(Graph(i + 1))
                        'If Not bounds.Contains(point) AndAlso Not bounds.Contains(point2) Then Continue For
                        If point.X < 0 AndAlso point2.X < 0 Then Continue For
                        'If point.Y > point2.Y Then
                        '    Using brush As New LinearGradientBrush(point2, New Point(point.X, Me.Height), Color.FromArgb(30, ForeColor), Color.FromArgb(0, ForeColor))
                        '        g.FillPolygon(brush, {New Point(point.X, Me.Height), point, point2, New Point(point2.X, Me.Height)})
                        '    End Using
                        'Else
                        '    Using brush As New LinearGradientBrush(point, New Point(point2.X, Me.Height), Color.FromArgb(30, ForeColor), Color.FromArgb(0, ForeColor))
                        '        g.FillPolygon(brush, {New Point(point.X, Me.Height), point, point2, New Point(point2.X, Me.Height)})
                        '    End Using
                        'End If
                        Using brush As New SolidBrush(Color.FromArgb(30, ForeColor))
                            g.FillPolygon(brush, {New Point(point.X, Me.Height), point, point2, New Point(point2.X, Me.Height)})
                        End Using
                        g.DrawLine(pen, point, point2)
                        Application.DoEvents()
                    End If
                Next
            Catch ex As Exception

            End Try
        End Using
    End Sub

    Function GetPoint(point As Point) As Point

        point = New Point(point.X * XUnit, Me.Height - point.Y * YUnit)
        point = point + bufferpos
        Return point
    End Function

    Function GetLinearGradientBrush(p1 As Point, p2 As Point) As LinearGradientBrush
        If p1.Y = p2.Y Then
            Return New LinearGradientBrush(New Rectangle(p1.X, p1.Y, p1.X - p2.X, Me.Height - p1.Y), Color.FromArgb(30, ForeColor), Color.FromArgb(0, ForeColor), 90, True)
        End If
        If p1.Y > p2.Y Then
            Dim angle As Integer = CType(90, Double) + Math.Atan((p1.Y - p2.Y) / (p2.X - p1.X)) * (CType(180.0, Double) / Math.PI)
            Return New LinearGradientBrush(New Rectangle(p1.X, p1.Y, p1.X - p2.X, Me.Height - p2.Y), Color.FromArgb(30, ForeColor), Color.FromArgb(0, ForeColor), angle, True)
        Else
            Dim angle As Integer = Math.Atan((p2.Y - p1.Y) / (p2.X - p1.X)) * (CType(180.0, Double) / Math.PI) - 45.0
            Return New LinearGradientBrush(New Rectangle(p1.X, p1.Y, p1.X - p2.X, Me.Height - p1.Y), Color.FromArgb(30, ForeColor), Color.FromArgb(0, ForeColor), angle, True)
        End If

        'Dim TopP As Point = If(p1.Y > p2.Y, p1, p2)
        'Dim m = (p1.Y - p2.Y) / (p1.X - p2.X)
        'Dim c1 = TopP

    End Function

    Private Sub Animatorx_ValueChanged(value As Integer) Handles Animatorx.ValueChanged
        bufferpos = New Point(value, bufferpos.Y)
        Try
            Me.Invoke(Sub() Me.Refresh())
        Catch ex As Exception

        End Try
        Application.DoEvents()
    End Sub
    Private Sub Animatory_ValueChanged(value As Integer) Handles Animatory.ValueChanged
        bufferpos = New Point(bufferpos.X, value)
        Debug.Print(bufferpos.ToString())
        Try
            Me.Invoke(Sub() Me.Refresh())
        Catch ex As Exception

        End Try
        Application.DoEvents()
    End Sub
End Class