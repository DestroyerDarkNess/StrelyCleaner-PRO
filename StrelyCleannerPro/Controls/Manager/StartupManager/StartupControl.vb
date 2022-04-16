Imports XylonV2.StartupManager

Public Class StartupControl

#Region " Properties "

    Private _StartupItem As Models.StartupList = Nothing
    Public Property StartupItem As Models.StartupList
        Get
            Return _StartupItem
        End Get
        Set(value As Models.StartupList)
            _StartupItem = value
        End Set
    End Property

    Public PathEx As String = String.Empty

#End Region



    Private Sub StartupControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim ParsedPath As String = _StartupItem.GetParsedPath
        If IO.File.Exists(ParsedPath) Then
            Label1.Text = ParsedPath
        Else
            Label1.Text = _StartupItem.Path
        End If

        Select Case _StartupItem.Type
            Case Models.StartupList.StartupType.Regedit
                Dim FullRegPath As String = _StartupItem.RegistryPath & "\" & _StartupItem.RegistryName
                Guna2HtmlLabel2.Text = _StartupItem.RegistryName
                Label1.Text += vbNewLine & FullRegPath
            Case Models.StartupList.StartupType.Shortcut
                Guna2HtmlLabel2.Text = _StartupItem.Name

            Case Models.StartupList.StartupType.TaskScheduler
                Guna2HtmlLabel2.Text = _StartupItem.Name

        End Select

        Try
            Guna2PictureBox1.Image = Icon.ExtractAssociatedIcon(_StartupItem.GetParsedPath).ToBitmap
        Catch ex As Exception

        End Try

    End Sub


    Private Sub ExploreButton_Click(sender As Object, e As EventArgs) Handles ExploreButton.Click
        Dim ParsedPath As String = _StartupItem.GetParsedPath
        If IO.File.Exists(ParsedPath) Then
            Core.Utils.ExploreFile(ParsedPath)
        Else
            Core.Utils.ExploreFile(_StartupItem.Path)
        End If
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
                                                        Try
                                                            Dim FilePath As String = String.Empty
                                                            Dim ParsedPath As String = _StartupItem.GetParsedPath

                                                            If IO.File.Exists(ParsedPath) Then
                                                                FilePath = ParsedPath
                                                            Else
                                                                FilePath = _StartupItem.Path
                                                            End If

                                                            Dim IsDeleteCorrect As Boolean = False
                                                            If IO.File.Exists(FilePath) = True Then
                                                                Dim ForDeleter As MK.Tools.ForceDel.FileDeleter = New MK.Tools.ForceDel.FileDeleter()
                                                                IsDeleteCorrect = ForDeleter.Delete(FilePath)
                                                            End If

                                                            Try
                                                                Select Case _StartupItem.Type
                                                                    Case Models.StartupList.StartupType.Regedit

                                                                        Dim exist1 As Boolean = XylonV2.Engine.Reg.RegEdit.ExistValue(fullKeyPath:="HKCU\" & _StartupItem.RegistryPath, valueName:=_StartupItem.RegistryName)
                                                                        If exist1 = True Then
                                                                            XylonV2.Engine.Reg.RegEdit.DeleteValue(fullKeyPath:="HKCU\" & _StartupItem.RegistryPath, valueName:=_StartupItem.RegistryName)
                                                                        End If

                                                                        Dim exist2 As Boolean = XylonV2.Engine.Reg.RegEdit.ExistValue(fullKeyPath:="HKLM\" & _StartupItem.RegistryPath, valueName:=_StartupItem.RegistryName)
                                                                        If exist2 = True Then
                                                                            XylonV2.Engine.Reg.RegEdit.DeleteValue(fullKeyPath:="HKLM\" & _StartupItem.RegistryPath, valueName:=_StartupItem.RegistryName)
                                                                        End If

                                                                    Case Models.StartupList.StartupType.Shortcut

                                                                    Case Models.StartupList.StartupType.TaskScheduler

                                                                        _StartupItem.Task.Stop()
                                                                        _StartupItem.Task.Enabled = False

                                                                End Select

                                                            Catch ex As Exception
                                                                'Core.ErrorLogger.LogError("StartupControl.Regedit", ex.Message, ex.StackTrace)
                                                            End Try

                                                            Return IsDeleteCorrect
                                                        Catch ex As Exception
                                                            'Core.ErrorLogger.LogError("StartupControl.Global", ex.Message, ex.StackTrace)
                                                            Return False
                                                        End Try
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

End Class
