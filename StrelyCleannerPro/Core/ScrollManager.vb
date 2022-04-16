Namespace Core

    Public Class ScrollManager : Implements IDisposable

#Region " Declare "

        Dim vScrollHelperMain As Guna.UI2.WinForms.Helpers.PanelScrollHelper 'Guna.UI2.Lib.ScrollBar.PanelScrollHelper
        Dim ControlTarget As Panel = Nothing

#End Region

#Region " Public Methods "

        Public Sub New(ByVal ControlA As Panel, ByVal ScrollBarArray() As Control, Optional ByVal AutoSizeScroll As Boolean = False)
            ControlTarget = ControlA
            For Each ScrollBar As Control In ScrollBarArray

                If TypeOf ScrollBar Is Guna.UI2.WinForms.Guna2VScrollBar Then

                    Dim PatchScroll As Guna.UI2.WinForms.Guna2VScrollBar = TryCast(ScrollBar, Guna.UI2.WinForms.Guna2VScrollBar)
                    vScrollHelperMain = New Guna.UI2.WinForms.Helpers.PanelScrollHelper(ControlA, PatchScroll, AutoSizeScroll)

                ElseIf TypeOf ScrollBar Is Guna.UI2.WinForms.Guna2HScrollBar Then

                    Dim PatchScroll As Guna.UI2.WinForms.Guna2HScrollBar = TryCast(ScrollBar, Guna.UI2.WinForms.Guna2HScrollBar)
                    vScrollHelperMain = New Guna.UI2.WinForms.Helpers.PanelScrollHelper(ControlA, PatchScroll, AutoSizeScroll)

                End If

            Next

            vScrollHelperMain.UpdateScrollBar()

            AddHandler ControlA.Resize, AddressOf Control_Resize
        End Sub

        Public Sub UpdateScroll()
            vScrollHelperMain.UpdateScrollBar()
        End Sub

#End Region

#Region " Private Methods "

        Private Sub Control_Resize(sender As Object, e As EventArgs)
            If vScrollHelperMain IsNot Nothing Then vScrollHelperMain.UpdateScrollBar()
        End Sub

#End Region


#Region " IDisposable "

        ''' <summary>
        ''' To detect redundant calls when disposing.
        ''' </summary>
        Private IsDisposed As Boolean = False
        ''' <summary>
        ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>
        ''' Releases unmanaged and - optionally - managed resources.
        ''' </summary>
        ''' <param name="IsDisposing">
        ''' <c>true</c> to release both managed and unmanaged resources; 
        ''' <c>false</c> to release only unmanaged resources.
        ''' </param>
        Protected Sub Dispose(ByVal IsDisposing As Boolean)
            If IsDisposed = False Then
                If IsDisposing = True Then
                    If Me.ControlTarget IsNot Nothing Then

                        With Me.ControlTarget
                            RemoveHandler .Resize, AddressOf Control_Resize
                        End With

                        Me.ControlTarget = Nothing

                    End If
                End If
            End If

            IsDisposed = True
        End Sub

#End Region

    End Class

End Namespace

