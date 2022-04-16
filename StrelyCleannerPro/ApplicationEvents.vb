Namespace My
    ' Los siguientes eventos están disponibles para MyApplication:
    ' Inicio: Se genera cuando se inicia la aplicación, antes de que se cree el formulario de inicio.
    ' Apagado: Se genera después de haberse cerrado todos los formularios de aplicación.  Este evento no se genera si la aplicación termina de forma anómala.
    ' UnhandledException: Se genera si la aplicación encuentra una excepción no controlada.
    ' StartupNextInstance: Se genera cuando se inicia una aplicación de instancia única y dicha aplicación está ya activa. 
    ' NetworkAvailabilityChanged: Se genera cuando se conecta o desconecta la conexión de red.
    Partial Friend Class MyApplication

        Private Sub AppStart(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            Try
                If Core.Utils.IsAdmin = True Then

                    Dim Windows_Symbols As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "symbols\dll\")

                    Dim Wntdll As String = IO.Path.Combine(Windows_Symbols, "wntdll.pdb")
                    Dim Wkernelbase As String = IO.Path.Combine(Windows_Symbols, "wkernelbase.pdb")

                    If IO.Directory.Exists(Windows_Symbols) = False Then
                        IO.Directory.CreateDirectory(Windows_Symbols)
                    End If

                    If IO.File.Exists(Wntdll) = False Then
                        IO.File.WriteAllBytes(Wntdll, My.Resources.wntdll)
                    End If

                    If IO.File.Exists(Wkernelbase) = False Then
                        IO.File.WriteAllBytes(Wkernelbase, My.Resources.wkernelbase)
                    End If

                End If
            Catch ex As Exception
                Console.WriteLine("AppStart Error: " & ex.Message)
            End Try

        End Sub

    End Class

End Namespace
