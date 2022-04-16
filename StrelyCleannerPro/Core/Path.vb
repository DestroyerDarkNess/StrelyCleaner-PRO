Namespace Core
    Public Class Paths

        Public Shared CurrentUserStartup As String = Environment.GetFolderPath(Environment.SpecialFolder.Startup)
        Public Shared AllUsersStartup As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup)
        Public Shared RootPath As String = IO.Path.GetPathRoot(Environment.CurrentDirectory)
        Public Shared AppData As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)

        Public Shared Document As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments

        Public Shared Desktop As String = My.Computer.FileSystem.SpecialDirectories.Desktop

        Public Shared Pictures As String = My.Computer.FileSystem.SpecialDirectories.MyPictures

        Public Shared Music As String = My.Computer.FileSystem.SpecialDirectories.MyMusic

        Public Shared ProgramFiles As String = My.Computer.FileSystem.SpecialDirectories.ProgramFiles

        Public Shared TempPath As String = IO.Path.GetTempPath

        Public Shared CachePath As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\Local\Strely\")

        Public Shared Downloads As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads\")

        Public Shared DefenderExeLocation As String = "C:\Program Files\Windows Defender\MpCmdRun.exe"

        Public Shared WindowsRecent As String = Environment.GetFolderPath(Environment.SpecialFolder.Recent)

    End Class
End Namespace

