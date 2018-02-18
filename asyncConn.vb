Imports System.DateTime
Imports System.Reflection
Imports System.Data.SqlServerCe
Imports System.IO
Imports System.Xml
Imports System.Data

Public Class asyncConn
    Inherits Telerik.WinControls.UI.RadForm
    Public locaPrefix As String
    Public subscriber As String
    Public Shared ser As Integer


    Private ReadOnly Property DataFileName() As String
        Get
            Return Path.ChangeExtension(Assembly.GetExecutingAssembly().GetName().CodeBase, ".sdf")
        End Get
    End Property

    Private ReadOnly Property DataSourceConnectionString() As String
        Get
            Return String.Format("Data Source = {0};", Me.DataFileName)
        End Get
    End Property


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private myUserInterfaceUpdateEvent As EventHandler
    Private tableName As String
    Private percentage As Integer
    Private eventStatus As SyncStatus
    Private repl As SqlCeReplication

    Friend Enum SyncStatus
        PercentComplete
        BeginUpload
        BeginDownload
        SyncComplete
    End Enum 'SyncStatus

    Public Sub New()
        InitializeComponent()
        Me.myUserInterfaceUpdateEvent = New EventHandler(AddressOf UserInterfaceUpdateEvent)

    End Sub 'New

    Private Sub asyncConn_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        DiversifiedTimeClock.TimerSync.Enabled = False
        lblNetworkStatus.ForeColor = Color.Gray
        If DiversifiedTimeClock.con Then
            lblNetworkStatus.ForeColor = Color.Green
            DiversifiedTimeClock.labelLastSyncValue.ForeColor = Color.Green
            syncme()
        Else
            DiversifiedTimeClock.labelLastSyncValue.ForeColor = Color.Red
            DiversifiedTimeClock.labelLastSyncValue.Text = "NETWORK NOT FOUND"
            Me.Close()

        End If
    End Sub
    Public Sub syncme()
        If DiversifiedTimeClock.con Then
            Try
                Me.repl = New SqlCeReplication()
                repl.SubscriberConnectionString = "Data Source=TimeClock.sdf"
                repl.LoadProperties()
                repl.ConnectionManager = True
                Dim ar As IAsyncResult = repl.BeginSynchronize(New AsyncCallback(AddressOf Me.SyncCompletedCallback), _
                           New OnStartTableUpload(AddressOf Me.OnStartTableUploadCallback), _
                            New OnStartTableDownload(AddressOf Me.OnStartTableDownloadCallback), _
                            New OnSynchronization(AddressOf Me.OnSynchronizationCallback), repl)
            Catch ex As SqlCeException
                MessageBox.Show(ex.Message, "string") 'SQL Server Compact encountered problems when opening the database. [ Internal Error Number = 25046,Internal Error String = The database file cannot be found. Check the path to the database. [,,,Data Source,,] ]
                'Dim frm As mySettings = New mySettings
                'frm.Show()
            End Try
        Else
        End If
    End Sub
#Region "Threading Update UI"

    Public Sub UserInterfaceUpdateEvent(ByVal sender As Object, ByVal e As System.EventArgs)
        Select Case Me.eventStatus
            Case SyncStatus.BeginUpload
                Me.labelStatusValue.Text = "Uploading table : " & tableName

            Case SyncStatus.PercentComplete
                Me.labelStatusValue.Text = "Sync with SQL Server is " & percentage.ToString() & "% complete."

            Case SyncStatus.BeginDownload
                Me.labelStatusValue.Text = "Downloading table : " & tableName

            Case SyncStatus.SyncComplete
                ser = ser + 1
                DiversifiedTimeClock.connum = ser
                Dim lastupdate As String = GetLastSuccessfulSyncTime().ToString()
                Me.labelStatusValue.Text = "Synchronization has completed successfully: " & lastupdate
                DiversifiedTimeClock.labelLastSyncValue.Text = "Last Sync: " & lastupdate & vbCrLf & "Click to Refresh"
                Me.Close()

        End Select
    End Sub 'UserInterfaceUpdateEvent

    Public Sub SyncCompletedCallback(ByVal ar As IAsyncResult)
        Dim repl As SqlCeReplication = CType(ar.AsyncState, SqlCeReplication)
        Try

            repl.SaveProperties()
            repl.EndSynchronize(ar)

            Me.eventStatus = SyncStatus.SyncComplete
        Catch e As SqlCeException

            MessageBox.Show(e.Message) 'The database is currently synchronizing with the server.

        Finally
            ' NOTE: If you want to set Control properties from within this 
            ' method, you must use Control.Invoke method to marshal
            ' the call to the UI thread; otherwise you might deadlock your 
            ' application; See Control.Invoke documentation for more information
            Me.Invoke(Me.myUserInterfaceUpdateEvent)
        End Try

    End Sub 'SyncCompletedCallback

    Public Sub OnSynchronizationCallback(ByVal ar As IAsyncResult, ByVal percentComplete As Integer)
        Me.percentage = percentComplete
        Me.eventStatus = SyncStatus.PercentComplete

        ' NOTE: If you want to set Control properties from within this 
        ' method, you must use Control.Invoke method to marshal
        ' the call to the UI thread; otherwise you might deadlock your 
        ' application; See Control.Invoke documentation for more information
        '
        Me.Invoke(Me.myUserInterfaceUpdateEvent)

    End Sub 'OnSynchronizationCallback

    Public Sub OnStartTableUploadCallback(ByVal ar As IAsyncResult, ByVal tableName As String)
        Me.tableName = tableName
        Me.eventStatus = SyncStatus.BeginUpload

        ' NOTE: If you want to set Control properties from within this 
        ' method, you must use Control.Invoke method to marshal
        ' the call to the UI thread; otherwise you might deadlock your 
        ' application; See Control.Invoke documentation for more information
        '
        Me.Invoke(Me.myUserInterfaceUpdateEvent)

    End Sub 'OnStartTableUploadCallback

    Public Sub OnStartTableDownloadCallback(ByVal ar As IAsyncResult, ByVal tableName As String)
        Me.tableName = tableName
        Me.eventStatus = SyncStatus.BeginDownload

        ' NOTE: If you want to set Control properties from within this 
        ' method, you must use Control.Invoke method to marshal
        ' the call to the UI thread; otherwise you might deadlock your 
        ' application; See Control.Invoke documentation for more information
        '
        Me.Invoke(Me.myUserInterfaceUpdateEvent)

    End Sub 'OnStartTableDownloadCallback

    Public Function GetLastSuccessfulSyncTime() As DateTime
        Dim localDateTime As DateTime = Date.Now
        Dim dt As DataTable = Nothing
        '        Dim va, vb, vc As String = String.Empty


        Dim conn As SqlCeConnection = Nothing
        Dim cmd As SqlCeCommand = Nothing
        Dim a As New SqlCeAdapter("TimeClock.sdf")

        Try
            conn = New SqlCeConnection("Data Source = TimeClock.sdf")
            conn.Open()

            cmd = conn.CreateCommand()
            cmd.CommandText = "SELECT LastSuccessfulSync FROM __sysMergeSubscriptions " & _
                "WHERE Publication='SEUdock'" '@publication"
            '            cmd.Parameters.Add("@publication", SqlDbType.NVarChar, 4000)
            '            cmd.Parameters("@publication").Value = "SEUdock"
            'Note: LastSuccessfulSync is stored in local time, not UTC time
            localDateTime = CType(cmd.ExecuteScalar(), DateTime)
            Return localDateTime
        Finally
            conn.Close()
        End Try

    End Function 'GetLastSuccessfulSyncTime
#End Region '"Threading Update UI"




    Private Sub asyncConn_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub



End Class


