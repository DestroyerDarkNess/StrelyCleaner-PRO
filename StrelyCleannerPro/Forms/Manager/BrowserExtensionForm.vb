Imports XylonV2.Core.Engine.WebBrowser.Chrome


Public Class BrowserExtensionForm

    Public Sub New()

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub BrowserExtensionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Me.Refresh()

        ProcessListScrool = New Core.ScrollManager(PanelContainer, {Guna2VScrollBar1}, True)

    End Sub


#Region " GUI "

    Private Sub BrowserExtensionForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        StartList()
    End Sub

#End Region


#Region " Listener "

    Private ProcessListener As Boolean = False
    Private ProcessListScrool As Core.ScrollManager = Nothing
    Private ListenerProcess As New ControlLister With {.OrientationControls = Orientation.Vertical, .Margen = New Point(5, 10)}
    Private LoadCheck As Boolean = False

    Public Sub StartList()
        Dim ItemText As String = Guna2ComboBox1.Items(Guna2ComboBox1.SelectedIndex)

        DioMarqueeProgressBar1.Start()
        Panel2.Visible = False
        DioMarqueeProgressBar1.Visible = True
        PanelContainer.Controls.Clear()
        GetExtensionList(ItemText)
        LoadCheck = False
    End Sub

    Public Sub GetExtensionList(ByVal BrowserSelected As String)


        If ProcessListener = False Then
            ProcessListener = True

            Dim Asynctask As New Task(New Action(Async Sub()

                                                     Dim IDName As Integer = 0

                                                     Dim ExtensionsArray As New List(Of Object)

                                                     Select Case BrowserSelected
                                                         Case "Google Chome"

                                                             Dim ChromeExtensionManager As XylonV2.Core.Engine.WebBrowser.Chrome = New XylonV2.Core.Engine.WebBrowser.Chrome

                                                             Dim GetExtensions As List(Of XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension) = ChromeExtensionManager.Extensions()

                                                             For Each ChromeExtension As XylonV2.Core.Engine.WebBrowser.Chrome.ChromeExtension In GetExtensions

                                                                 If ChromeExtension.LoadState = XylonV2.Core.Engine.WebBrowser.Chrome.StateLoaded.Loaded Then

                                                                     ExtensionsArray.Add(ChromeExtension)

                                                                 End If

                                                             Next

                                                     End Select


                                                     For Each Ext As Object In ExtensionsArray
                                                         Try
                                                             Me.BeginInvoke(Sub()

                                                                                Dim ListerThis As Boolean = True

                                                                                If ListerThis = True Then
                                                                                    Dim NewProc As New ExtensionControl With {.Extension = Ext, .Visible = True}
                                                                                    NewProc.Name = IDName
                                                                                    ListenerProcess.Add(PanelContainer, NewProc)
                                                                                End If

                                                                            End Sub)
                                                             IDName += 1

                                                         Catch ex As Exception
                                                             'Core.ErrorLogger.LogError("BrowserExtension.Lister", ex.Message, ex.StackTrace)
                                                         End Try
                                                     Next

                                                     Me.BeginInvoke(Sub()
                                                                        Panel2.Visible = True
                                                                        DioMarqueeProgressBar1.Visible = False
                                                                        DioMarqueeProgressBar1.Stop()
                                                                    End Sub)
                                                     LoadCheck = True
                                                 End Sub), TaskCreationOptions.PreferFairness)
            Asynctask.Start()
            ProcessListener = False
        End If

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        StartList()
    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged

        Dim ItemText As String = Guna2ComboBox1.Items(Guna2ComboBox1.SelectedIndex)

        Select Case ItemText
                Case "Google Chome"
                    Guna2PictureBox1.Image = My.Resources.icons8_chrome_64
            End Select

    End Sub

#End Region


End Class