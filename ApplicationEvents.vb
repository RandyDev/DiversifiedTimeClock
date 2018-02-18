Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Protected Overrides Function OnInitialize(
ByVal commandLineArgs As System.Collections.
    ObjectModel.ReadOnlyCollection(Of String)
) As Boolean
            ' Set the display time to 5000 milliseconds (5 seconds). 
            Me.MinimumSplashScreenDisplayTime = 20000
            Return MyBase.OnInitialize(commandLineArgs)
        End Function

        Private Sub MyApplication_NetworkAvailabilityChanged(sender As Object, e As Devices.NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
            Dim args As Devices.NetworkAvailableEventArgs = e
            My.Forms.DiversifiedTimeClock.SetConnectionStatus(e.IsNetworkAvailable)
        End Sub

        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown

        End Sub


        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim a As ApplicationServices.StartupEventArgs = e
            Dim utl As New Utils
            '@@@@@@@@@@ Location @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            Dim splash As SplashScreen1 = CType(My.Application.SplashScreen, SplashScreen1)
            Try
                utl.GetLocationSnapShot()
            Catch ex As Exception

            End Try

            Dim filename As String = "TimeClock.sdf"
            Dim adapter As New SqlCeAdapter(filename)
            If System.IO.File.Exists(filename) Then
                Dim strqry As String = "select dateworked from TimePunche Order by dateworked desc"
                Dim dttp As DataTable = adapter.QueryDatabaseForTable(strqry)
                If Not dttp Is Nothing Then
                    Dim f As New asyncConn
                    f.Show()
                Else
                    Try
                        System.IO.File.Delete(filename)
                        utl.getSnapShot()
                    Catch ex As Exception
                    End Try
                End If
            Else
                utl.getSnapShot()
            End If

        End Sub

        Private Sub MyApplication_StartupNextInstance(sender As Object, e As ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            Dim a As ApplicationServices.StartupNextInstanceEventArgs = e

        End Sub
    End Class


End Namespace

