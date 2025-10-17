Imports System.Runtime.InteropServices

Namespace My
    Partial Friend Class MyApplication

        ' P/Invoke for immediate process termination
        <DllImport("kernel32.dll", SetLastError:=True)>
        Private Shared Function TerminateProcess(hProcess As IntPtr, uExitCode As UInteger) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Private Shared Function GetCurrentProcess() As IntPtr
        End Function

        ' === Handle Unhandled Exceptions ===
        Private Sub MyApplication_UnhandledException(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Try
                ' Log the error
                Debug.WriteLine($"Unhandled Exception: {e.Exception.Message}")
                Debug.WriteLine($"Stack Trace: {e.Exception.StackTrace}")

                ' If it's an AccessViolationException, suppress and force exit
                If TypeOf e.Exception Is AccessViolationException Then
                    Debug.WriteLine("AccessViolationException caught - forcing immediate clean exit")

                    ' Prevent normal exception handling
                    e.ExitApplication = False

                    ' Force immediate process termination to avoid COM cleanup
                    Try
                        TerminateProcess(GetCurrentProcess(), 0)
                    Catch
                        Environment.Exit(0)
                    End Try
                Else
                    ' For other exceptions, show error but don't crash
                    MessageBox.Show($"An error occurred: {e.Exception.Message}",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
                    e.ExitApplication = False
                End If
            Catch
                ' If error handling fails, force exit
                Try
                    TerminateProcess(GetCurrentProcess(), 0)
                Catch
                    Environment.Exit(0)
                End Try
            End Try
        End Sub

        ' === Application Shutdown ===
        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
            Try
                ' Call DatabaseConfig shutdown
                DatabaseConfig.PrepareForShutdown()

                ' Minimal cleanup - don't overdo it
                GC.Collect()
                GC.WaitForPendingFinalizers()

                ' Short delay
                System.Threading.Thread.Sleep(50)
            Catch ex As Exception
                Debug.WriteLine($"Shutdown error: {ex.Message}")
            End Try
        End Sub

        ' === Application Startup ===
        Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' Set up AppDomain exception handling as last resort
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf AppDomain_UnhandledException

            ' Disable COM finalizer thread timeout
            Try
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(Nothing)
            Catch
            End Try
        End Sub

        ' === AppDomain Level Exception Handler ===
        Private Sub AppDomain_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
            Try
                Dim ex As Exception = TryCast(e.ExceptionObject, Exception)
                If ex IsNot Nothing Then
                    Debug.WriteLine($"AppDomain Exception: {ex.Message}")

                    ' If it's an AccessViolationException during termination, force exit
                    If TypeOf ex Is AccessViolationException Then
                        Debug.WriteLine("AccessViolationException in AppDomain - forcing immediate exit")

                        ' Force immediate termination
                        Try
                            TerminateProcess(GetCurrentProcess(), 0)
                        Catch
                            Environment.Exit(0)
                        End Try
                    End If
                End If
            Catch
                ' Last resort - force exit
                Try
                    Environment.Exit(0)
                Catch
                End Try
            End Try
        End Sub

    End Class
End Namespace