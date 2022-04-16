Imports System.ServiceProcess
Imports Microsoft.Win32
Imports XylonV2

Namespace Core.Optimizer
    Public Class Utilities

        Friend Shared Sub RunCommand(ByVal command As String)
            Using p As Process = New Process()
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                p.StartInfo.FileName = "cmd.exe"
                p.StartInfo.Arguments = "/C " & command

                Try
                    p.Start()
                    p.WaitForExit()
                    p.Close()
                Catch ex As Exception
                    'ErrorLogger.LogError("Utilities.RunCommand", ex.Message, ex.StackTrace)
                End Try
            End Using
        End Sub

        Friend Shared Function ServiceExists(ByVal serviceName As String) As Boolean
            Return ServiceController.GetServices().Any(Function(serviceController) serviceController.ServiceName.Equals(serviceName))
        End Function

        Friend Shared Sub StopService(ByVal serviceName As String)

            If ServiceExists(serviceName) Then

                Try
                    Dim svcStatus As ServiceControllerStatus = ServiceUtils.GetStatus(serviceName)
                    If Not svcStatus = ServiceControllerStatus.Stopped Then
                        ServiceUtils.SetStatus(svcStatus, ServiceUtils.SvcStatus.Stop, wait:=True, throwOnStatusMissmatch:=False)
                    End If
                Catch ex As Exception

                    Try
                        Dim sc As ServiceController = New ServiceController(serviceName)
                        If sc.CanStop Then
                            sc.[Stop]()
                        End If
                    Catch exe As Exception
                        'ErrorLogger.LogError("Utilities.StopService", exe.Message, exe.StackTrace)
                    End Try

                End Try

            End If

        End Sub

        Friend Shared Sub StartService(ByVal serviceName As String)

            If ServiceExists(serviceName) Then

                Try
                    Dim svcStatus As ServiceControllerStatus = ServiceUtils.GetStatus(serviceName)

                    If Not svcStatus = ServiceControllerStatus.Running Then
                        ServiceUtils.SetStatus(svcStatus, ServiceUtils.SvcStatus.Start, wait:=True, throwOnStatusMissmatch:=False)
                    End If
                Catch ex As Exception
                    Try
                        Dim sc As ServiceController = New ServiceController(serviceName)
                        sc.Start()
                    Catch exe As Exception
                        'ErrorLogger.LogError("Utilities.StartService", exe.Message, exe.StackTrace)
                    End Try
                End Try


            End If

        End Sub

        Friend Shared Sub RunBatchFile(ByVal batchFile As String)
            Try

                Using p As Process = New Process()
                    p.StartInfo.CreateNoWindow = True
                    p.StartInfo.FileName = batchFile
                    p.StartInfo.UseShellExecute = False
                    p.Start()
                    p.WaitForExit()
                    p.Close()
                End Using

            Catch ex As Exception
                'ErrorLogger.LogError("Utilities.RunBatchFile", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Friend Shared Sub ImportRegistryScript(ByVal scriptFile As String)
            Dim path As String = """" & scriptFile & """"
            Dim p As Process = New Process()

            Try
                p.StartInfo.FileName = "regedit.exe"
                p.StartInfo.UseShellExecute = False
                p = Process.Start("regedit.exe", "/s " & path)
                p.WaitForExit()
            Catch ex As Exception
                p.Dispose()
                'ErrorLogger.LogError("Utilities.ImportRegistryScript", ex.Message, ex.StackTrace)
            Finally
                p.Dispose()
            End Try
        End Sub

        Friend Shared Function GetWindows10Build() As String
            Return CStr(Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", ""))
        End Function

    End Class
End Namespace

