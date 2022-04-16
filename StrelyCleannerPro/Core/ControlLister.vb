Public Class ControlLister

#Region " Properties "

    Private _Orientation As Orientation = System.Windows.Forms.Orientation.Horizontal
    Public Property OrientationControls As Orientation
        Get
            Return _Orientation
        End Get
        Set(value As Orientation)
            _Orientation = value
        End Set
    End Property

    Public Property Margen As Point
        Get
            Return MargenP
        End Get
        Set(value As Point)
            MargenP = value
        End Set
    End Property

    Private _MaxItems As Integer = 0
    Public Property MaxItemInLine As Integer
        Get
            Return _MaxItems
        End Get
        Set(value As Integer)
            _MaxItems = value
        End Set
    End Property

#End Region

#Region " Declare "

    Private LocationX As Integer = 4
    Private LocationY As Integer = 4
    Private SeparationX As Integer = 4
    Private SeparationY As Integer = 4
    Private XSizeCupon As Integer = 0
    Private YSizeCupon As Integer = 0
    Private MargenP As Point = New Point(4, 4)

    Private ControlsCount As Integer = 0

#End Region

#Region " Methods "

    Public Sub Add(ByVal ContainerControl As Panel, ByVal ControlEx As Control, Optional ByVal LimitedLocation As Boolean = False)
        If ContainerControl IsNot Nothing Then
            ControlEx.Visible = False

            Dim TheLastControl As Control = Nothing

            If Not ContainerControl.Controls.Count = 0 Then
                TheLastControl = ContainerControl.Controls(ContainerControl.Controls.Count - 1)
            End If

            If TheLastControl Is Nothing Then

                ContainerControl.Controls.Add(ControlEx)
                ControlEx.Location = New Point(MargenP.X, MargenP.Y)

                XSizeCupon = ContainerControl.Width - (ContainerControl.Controls(0).Location.X + ContainerControl.Controls(0).Width)
                YSizeCupon = ContainerControl.Height - (ContainerControl.Controls(0).Location.X + ContainerControl.Controls(0).Height)

            Else

                Dim NewPostX As Integer = 0
                Dim NewPostY As Integer = 0

                If _Orientation = Orientation.Horizontal Then

                    If LimitedLocation = False Then '

                        NewPostX = TheLastControl.Location.X + TheLastControl.Width + SeparationX
                        NewPostY = TheLastControl.Location.Y

                    Else

                        If XSizeCupon >= (ControlEx.Width + SeparationX) Then

                            NewPostX = TheLastControl.Location.X + TheLastControl.Width + SeparationX
                            NewPostY = TheLastControl.Location.Y

                        Else

                            NewPostX = MargenP.X
                            NewPostY = TheLastControl.Location.Y + TheLastControl.Height + SeparationY

                        End If

                    End If


                ElseIf _Orientation = Orientation.Vertical Then

                    If LimitedLocation = False Then ' GetInlineItemsCount(ContainerControl) = _MaxItems OrElse

                        NewPostX = TheLastControl.Location.X
                        NewPostY = TheLastControl.Location.Y + TheLastControl.Height + SeparationY

                    Else

                        If YSizeCupon >= (ControlEx.Height + SeparationY) Then

                            NewPostX = TheLastControl.Location.X
                            NewPostY = TheLastControl.Location.Y + TheLastControl.Height + SeparationY

                        Else

                            NewPostX = TheLastControl.Location.X + TheLastControl.Width + SeparationX
                            NewPostY = MargenP.Y

                        End If

                    End If

                End If

                LocationX = NewPostX
                LocationY = NewPostY


                ContainerControl.Controls.Add(ControlEx)
                ControlEx.Location = New Point(LocationX, LocationY)

                TheLastControl = ContainerControl.Controls(ContainerControl.Controls.Count - 1)
                XSizeCupon = ContainerControl.Width - (TheLastControl.Location.X + ContainerControl.Controls(0).Width)
                YSizeCupon = ContainerControl.Height - (TheLastControl.Location.Y + ContainerControl.Controls(0).Height)

                ControlsCount += 1

            End If

            ControlEx.Visible = True
        End If
    End Sub

    Private Function GetInlineItemsCount(ByVal Container As Panel) As Integer
        Dim ItemInline As Integer = 0
        Dim TheLastControl As Control = Container.Controls(Container.Controls.Count - 1)

        For Each ItemN As Control In Container.Controls

            If _Orientation = Orientation.Horizontal Then

                If ItemN.Location.X = TheLastControl.Location.X Then

                    ItemInline += 1

                End If

            ElseIf _Orientation = Orientation.Vertical Then

                If ItemN.Location.Y = TheLastControl.Location.Y Then

                    ItemInline += 1

                End If

            End If

        Next

        Return ItemInline
    End Function

#End Region

End Class
