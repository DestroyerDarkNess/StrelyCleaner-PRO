Public Class AV_Item

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

    Private _Info As XylonV2.Engine.External.Core.DetectionResult = Nothing
    Public Property Info As XylonV2.Engine.External.Core.DetectionResult
        Get
            Return _Info
        End Get
        Set(value As XylonV2.Engine.External.Core.DetectionResult)
            _Info = value
        End Set
    End Property

    Private _FilePath As String = String.Empty
    Public Property FilePath As String
        Get
            Return _FilePath
        End Get
        Set(value As String)
            _FilePath = value
        End Set
    End Property

    Private _AlertLevel As Core.Instances.WarnLevel = Core.Instances.WarnLevel.Danger
    Public Property AlertLevel As Core.Instances.WarnLevel
        Get
            Return _AlertLevel
        End Get
        Set(value As Core.Instances.WarnLevel)
            _AlertLevel = value
        End Set
    End Property

    Private _Action As Action = Nothing
    Public Property AlertAction As Action
        Get
            Return _Action
        End Get
        Set(value As Action)
            _Action = value
        End Set
    End Property

#End Region

    Private FolderPath As String = String.Empty

    Private Sub AV_Item_Load(sender As Object, e As EventArgs) Handles Me.Load

        If _Extension Is Nothing Then

            Guna2PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
            ExploreButton.Visible = IO.File.Exists(_FilePath)
            Guna2HtmlLabel2.Text = IO.Path.GetFileName(_FilePath)
            Guna2HtmlLabel3.Text = _Info.Signature
            Guna2HtmlToolTip1.SetToolTip(Guna2HtmlLabel4, _FilePath)

        Else

            Guna2PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            Guna2PictureBox1.Image = My.Resources.icons8_puzzle_48

            If TypeOf _Extension Is XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension Then

                Dim ExtensionParsed As XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension = DirectCast(_Extension, XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension)

                Guna2HtmlLabel2.Text = ExtensionParsed.ManifestJson.name
                Guna2HtmlLabel3.Text = ExtensionParsed.ManifestJson.version

                If Not ExtensionParsed.IconToList.Count = 0 Then

                    Try
                        Dim ExtensionIcon As Image = XylonV2.Core.Helper.Util.ToIcon(ExtensionParsed.IconToList.LastOrDefault, True, Color.Transparent).ToBitmap
                        If ExtensionIcon IsNot Nothing Then Guna2PictureBox1.Image = ExtensionIcon
                    Catch ex As Exception
                        'Core.ErrorLogger.LogError("Extension.Icon", ex.Message, ex.StackTrace)
                    End Try

                End If

                FolderPath = ExtensionParsed.FullPath
                ExploreButton.Visible = IO.Directory.Exists(ExtensionParsed.FullPath)
                Guna2HtmlLabel2.Text = ExtensionParsed.ManifestJson.name
                Guna2HtmlLabel3.Text = "BrowserExtension!" & _Info.Signature
                Guna2HtmlToolTip1.SetToolTip(Guna2HtmlLabel4, ExtensionParsed.FullPath)

            ElseIf TypeOf _Extension Is XylonV2.Core.Engine.WebBrowser.Edge.EdgeExtension Then

            End If

        End If

        LoadStatus()
    End Sub


    Private Sub ExploreButton_Click(sender As Object, e As EventArgs) Handles ExploreButton.Click
        If _Extension Is Nothing Then
            Core.Utils.ExploreFile(_FilePath)
        Else
            Core.Utils.ExploreFile(FolderPath)
        End If
    End Sub

    Private Sub LoadStatus()


        Try
            If _Extension Is Nothing Then

                Select Case _AlertLevel
                    Case Core.Instances.WarnLevel.Danger
                        '  Guna2HtmlLabel1.Text = "Danger"
                        Guna2PictureBox1.Image = My.Resources.icons8_biohazard_48
                        '  Guna2Panel2.BackColor = Color.FromArgb(255, 118, 118)
                        Guna2HtmlLabel3.ForeColor = Color.Red
                    Case Core.Instances.WarnLevel.Warning
                        ' Guna2HtmlLabel1.Text = "Warning"
                        Guna2PictureBox1.Image = My.Resources.icons8_warning_64
                        'Guna2Panel2.BackColor = Color.FromArgb(255, 182, 34)
                        Guna2HtmlLabel3.ForeColor = Color.FromArgb(255, 216, 0)
                        Guna2Panel1.BorderColor = Color.FromArgb(255, 182, 34)
                    Case Core.Instances.WarnLevel.Sure
                        '  Guna2HtmlLabel1.Text = "You Are Protected"
                        Guna2PictureBox1.Image = My.Resources.icons8_protect_100
                        ' Guna2Panel2.BackColor = Color.FromArgb(47, 241, 117)
                        Guna2HtmlLabel3.ForeColor = Color.MediumSpringGreen
                        '  Guna2HtmlLabel1.Font = New Font("Segoe UI", 20)
                        Guna2Panel1.BorderColor = Color.FromArgb(47, 241, 117)
                End Select

                If ExploreButton.Visible = True Then
                    Guna2PictureBox1.Image = Icon.ExtractAssociatedIcon(_FilePath).ToBitmap
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Function Solve() As Boolean

        IsLoading(True)

        Dim tProcess As Task(Of Boolean) = Task.Run(Function()
                                                        Dim IsDeleteCorrect As Boolean = False

                                                        If _Extension Is Nothing Then

                                                            If IO.File.Exists(_FilePath) = True Then
                                                                Dim ForDeleter As MK.Tools.ForceDel.FileDeleter = New MK.Tools.ForceDel.FileDeleter()
                                                                IsDeleteCorrect = ForDeleter.Delete(_FilePath)
                                                                If IsDeleteCorrect = False Then
                                                                    Core.Instances.ErrorsLogger.Add("[ AV ] " & _FilePath & "   ->   " & "Could not be removed successfully")
                                                                End If
                                                            End If
                                                            If _Action IsNot Nothing Then
                                                                _Action()
                                                            End If

                                                        Else

                                                            If TypeOf _Extension Is XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension Then

                                                                Dim ExtensionParsed As XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension = DirectCast(_Extension, XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension)

                                                                If IO.File.Exists(ExtensionParsed.ManifestJson.FilePathJson) = True Then
                                                                    Dim ForDeleter As MK.Tools.ForceDel.FileDeleter = New MK.Tools.ForceDel.FileDeleter()
                                                                    IsDeleteCorrect = ForDeleter.Delete(ExtensionParsed.ManifestJson.FilePathJson)
                                                                End If
                                                                Try
                                                                    IO.Directory.Delete(ExtensionParsed.FullPath, True)

                                                                Catch ex As Exception

                                                                End Try

                                                            ElseIf TypeOf _Extension Is XylonV2.Core.Engine.WebBrowser.Edge.EdgeExtension Then

                                                            End If

                                                        End If


                                                        Return IsDeleteCorrect
                                                    End Function)
        '   tProcess.Wait()
        tProcess.GetAwaiter().OnCompleted(AddressOf StatusSolve)

        For i As Integer = 0 To 2
            If _AlertLevel = Core.Instances.WarnLevel.Sure Then
                Exit For
            End If
            Application.DoEvents()
            i -= 1
        Next

        Return tProcess.Result
    End Function

    Private Sub StatusSolve()
        IsLoading(True, 100)
        _AlertLevel = Core.Instances.WarnLevel.Sure
    End Sub

    Public Sub IsLoading(ByVal State As Boolean, Optional ByVal StateINt As Integer = 1)
        Me.BeginInvoke(Sub()
                           Progress1.Visible = State
                           Progress1.Value = StateINt
                           Guna2ToggleSwitch1.Visible = Not State
                       End Sub)
    End Sub

    Public Sub SetSwitch(ByVal State As Boolean)
        Guna2ToggleSwitch1.Checked = State
    End Sub

End Class
