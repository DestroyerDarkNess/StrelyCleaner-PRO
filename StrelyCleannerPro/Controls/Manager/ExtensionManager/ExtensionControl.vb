Public Class ExtensionControl

#Region " Properties "

    Private _Extension As Object = Nothing
    Public Property Extension As Object
        Get
            Return _Extension
        End Get
        Set(value As Object)
            _Extension = value
        End Set
    End Property

    Public PathEx As String = String.Empty
    Public ManifestPathEx As String = String.Empty

#End Region

    Private Sub ExtensionControl_Load(sender As Object, e As EventArgs) Handles Me.Load

        If TypeOf _Extension Is XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension Then

            Dim ExtensionParsed As XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension = DirectCast(_Extension, XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension)

            Guna2HtmlLabel2.Text = ExtensionParsed.ManifestJson.name
            Guna2HtmlLabel3.Text = ExtensionParsed.ManifestJson.version

            Try

                If Not ExtensionParsed.IconToList.Count = 0 Then

                    Dim ExtensionIcon As Image = XylonV2.Core.Helper.Util.ToIcon(ExtensionParsed.IconToList.LastOrDefault, True, Color.Transparent).ToBitmap
                    If ExtensionIcon IsNot Nothing Then Guna2PictureBox1.Image = ExtensionIcon

                End If

            Catch ex As Exception
                'Core.ErrorLogger.LogError("Extension.Icon", ex.Message, ex.StackTrace)
            End Try

            If IO.Directory.Exists(ExtensionParsed.FullPath) Then PathEx = ExtensionParsed.FullPath : ExploreButton.Visible = True
            If IO.File.Exists(ExtensionParsed.ManifestJson.FilePathJson) Then ManifestPathEx = ExtensionParsed.ManifestJson.FilePathJson

            Dim ManifestPermissions As List(Of String) = ExtensionParsed.ManifestJson.permissions

            If ManifestPermissions IsNot Nothing Then
                Dim MPer As String = String.Join("  /  ", ManifestPermissions)
                Label1.Text = MPer
            End If

        ElseIf TypeOf _Extension Is XylonV2.Core.Engine.WebBrowser.Edge.EdgeExtension Then

        End If

    End Sub

    Private Sub ExploreButton_Click(sender As Object, e As EventArgs) Handles ExploreButton.Click
        If PathEx IsNot String.Empty Then Core.Utils.ExploreFile(PathEx)
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim DeleteExtension As Boolean = Solve()
        Me.ExploreButton.Visible = False
        Me.Guna2Button1.Visible = False
        Me.BackColor = Color.DarkGray
    End Sub

    Dim DeleteComplete As Boolean = False

    Public Function Solve() As Boolean

        IsLoading(True)

        Dim tProcess As Task(Of Boolean) = Task.Run(Function()
                                                        Dim IsDeleteCorrect As Boolean = False
                                                        If IO.File.Exists(ManifestPathEx) = True Then
                                                            Dim ForDeleter As MK.Tools.ForceDel.FileDeleter = New MK.Tools.ForceDel.FileDeleter()
                                                            IsDeleteCorrect = ForDeleter.Delete(ManifestPathEx)
                                                        End If
                                                        Try
                                                            If PathEx IsNot String.Empty Then IO.Directory.Delete(PathEx, True)

                                                        Catch ex As Exception

                                                        End Try

                                                        Return IsDeleteCorrect
                                                    End Function)

        tProcess.GetAwaiter().OnCompleted(AddressOf StatusSolve)

        For i As Integer = 0 To 2
            If DeleteComplete = True Then
                Exit For
            End If
            Application.DoEvents()
            i -= 1
        Next

        Return tProcess.Result
    End Function

    Private Sub StatusSolve()
        IsLoading(False, 100)
        DeleteComplete = True
    End Sub

    Public Sub IsLoading(ByVal State As Boolean, Optional ByVal StateINt As Integer = 1)
        Me.BeginInvoke(Sub()
                           DioMarqueeProgressBar1.Visible = State
                           ' Progress1.Value = StateINt
                           If State = True Then DioMarqueeProgressBar1.Start() Else DioMarqueeProgressBar1.Stop()

                           Guna2Button1.Visible = Not State
                       End Sub)
    End Sub

    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click
        '  MsgBox(PathEx & vbNewLine & vbNewLine & ManifestPathEx)
    End Sub
End Class
