Imports System.IO
Imports XylonV2

Public Class HomeForm

    Public Sub New()

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub HomeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Me.Refresh()
        StartUI()
    End Sub

#Region " GUI "

    Public Sub StartUI()

        Dim PcInfo As String = Core.Home.SystemInformation.GetComputerInformation.GetAwaiter.GetResult
        Dim Antivir As String = Core.Home.SystemInformation.GetAntiVirus().GetAwaiter.GetResult
        Dim Firewall As String = Core.Home.SystemInformation.GetFirewall().GetAwaiter.GetResult

        If String.IsNullOrEmpty(PcInfo) = True Then
            lbl_PcInfo.Text = "PC Info : " & My.Computer.Info.OSFullName
            lbl_PcInfo.ForeColor = Color.Red
        Else
            lbl_PcInfo.Text = PcInfo
            lbl_PcInfo.ForeColor = Color.Lime
        End If

        If String.IsNullOrEmpty(Antivir) = True Then
            lbl_Antivirus.Text = "AntiVirus : You Do Not Own Registered !"
            lbl_Antivirus.ForeColor = Color.Red
        Else
            lbl_Antivirus.Text = "AntiVirus : " & Antivir
            lbl_Antivirus.ForeColor = Color.Lime
        End If

        If String.IsNullOrEmpty(Firewall) = True Then
            lbl_Firewall.Text = "Firewall : You Do Not Own Registered !"
            lbl_Firewall.ForeColor = Color.Red
        Else
            lbl_Firewall.Text = "Firewall : " & Firewall
            lbl_Firewall.ForeColor = Color.Lime
        End If
        StartAds.Start()

    End Sub

    Private Sub HomeForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

#End Region


#Region " Ads "

    Structure AdStructure
        Public Title As String
        Public LinkAds As String
        Public ImageURL As String
    End Structure

    Dim LaunchURL As String = String.Empty

    Private StartAds As New Task(New Action(Async Sub()

                                                Dim AdsInfo As AdStructure = GetAdsInfo()

                                                Me.BeginInvoke(Sub()
                                                                   If Not AdsInfo.ImageURL = Nothing Then

                                                                       Guna2CirclePictureBox1.Visible = False
                                                                       AdsTitle.Text = AdsInfo.Title
                                                                       LaunchURL = AdsInfo.LinkAds
                                                                       Guna2PictureBox4.LoadAsync(AdsInfo.ImageURL)
                                                                       Guna2PictureBox4.Cursor = Cursors.Hand

                                                                   End If
                                                               End Sub)

                                            End Sub), TaskCreationOptions.PreferFairness)

    Private Sub Guna2PictureBox4_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox4.Click
        If Not LaunchURL = String.Empty Then Process.Start(LaunchURL)
    End Sub

    Public Function GetAdsInfo() As AdStructure
        Try

            Dim DataServerRecive As String() = XylonV2.Core.Helper.Util.GetDataPage("http://softwarefuturenet.000webhostapp.com/XAC/Ads.txt").Split(";")
            Dim DataServer As AdStructure = New AdStructure
            DataServer.Title = DataServerRecive(0)
            DataServer.LinkAds = DataServerRecive(1)
            DataServer.ImageURL = DataServerRecive(2)

            Return DataServer
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region " HackNews "

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        WebBrowser1.Navigate("https://hackerweb.app/")
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        WebBrowser1.GoBack()
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        WebBrowser1.GoForward()
    End Sub

    Private Sub WebBrowser1_Navigated(sender As Object, e As WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
        Guna2TextBox1.Text = e.Url.ToString
    End Sub

    Private Sub Guna2TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles Guna2TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrWhiteSpace(Guna2TextBox1.Text) = False Then
                WebBrowser1.Navigate(Guna2TextBox1.Text)
            End If
        End If
    End Sub

#End Region

End Class