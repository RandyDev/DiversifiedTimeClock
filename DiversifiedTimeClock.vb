Imports System.Data
Imports System.Windows.Forms
Imports Telerik.WinControls.UI

Public Class DiversifiedTimeClock

    Dim version As String = Application.ProductVersion
    Dim objThread1 As CallBackThread
    Dim objThread2 As CallBackThread
    Public connum As Integer = 0
    Public con As Boolean = True
    Public locaid As String = My.Settings.locaID
    Public locaname As String = String.Empty
    Public ser As Integer = 0

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    'Load
    '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    Private Sub DiversifiedTimeClock_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim utl As New Utils
        TimerClock.Start()
        lblTime.Text = Date.Now.ToShortTimeString
        '@@@@@@@@@@ Location @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        '        utl.GetLocationSnapShot()
        Dim a As New SqlCeAdapter("Locations.sdf")
        Dim dt As DataTable = New DataTable
        Dim strqry As String = "select Name From Location WHERE (Location.ID = '" & locaid & "')"
        locaname = a.QueryDatabaseForScalar(strqry)

        Dim today As Date = Date.Now.ToShortDateString
        '        Dim filename As String = "TimeClock.sdf"
        '        If System.IO.File.Exists(filename) Then
        '        strqry = "select dateworked from TimePunche Order by dateworked desc"
        '        Dim dttp As DataTable = a.QueryDatabaseForTable(strqry)
        '        If Not dttp Is Nothing Then
        '        TimerSync.Enabled = False
        '        Dim f As New asyncConn
        '        f.Show()
        '        Else
        '        Try
        '        System.IO.File.Delete(filename)
        '        utl.getSnapShot()
        '        Catch ex As Exception
        '        End Try
        '        End If
        '                    Else
        '        utl.getSnapShot()
        '        End If

        strqry = "select ParentCompanyID From Location WHERE ID = '" & locaid & "'"
        Dim pdt As DataTable = a.QueryDatabaseForTable(strqry)
        Dim pid As String = String.Empty
        If Not pdt Is Nothing Then
            pid = pdt.Rows(0).Item(0).ToString
        End If
        Select Case pid
            Case "4de4a6c5-f928-46cf-b00c-ff5f70388ed3"
                PictureBox1.Image = My.Resources.TripleCenterprise
            Case "xxx" 'First Coast Pallets has no ID
                PictureBox1.Image = My.Resources.FCP
            Case Else
                PictureBox1.Image = My.Resources.SEU
        End Select
        PictureBox1.Refresh()
        PictureBox1.Visible = True


        lblVersion.Text = "v " & Application.ProductVersion
        lblCompanyAndLocation.Text = locaname
        loadjobs()
        loadDepartments()
        loademployees()
        objThread2 = New CallBackThread(Me, AddressOf ThreadMethod2, AddressOf CallBackMethod2)
        objThread2.Start()
        btnHibernate.Visible = False
    End Sub

    Sub SetConnectionStatus(ByVal val As Boolean)
        con = val
    End Sub

    Private Sub loademployees()
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim dtAllEmps As New DataTable
        Dim strSQL As String = "SELECT ID, LastName + ', ' + FirstName + ' (' + Login + ')' AS Name " & _
                               " FROM Employee " & _
                               "ORDER BY LastName"
        dtAllEmps = a.QueryDatabaseForTable(strSQL)
        Dim empitem As New RadListDataItem
        For Each row As DataRow In dtAllEmps.Rows
            empitem = New RadListDataItem
            empitem.Text = row.Item("Name")
            empitem.Value = row.Item("ID")
            Me.lcTimeCards.Items.Add(empitem)
        Next
    End Sub

    Private Sub lcTimeCards_SelectedIndexChanged(sender As Object, e As Data.PositionChangedEventArgs) Handles lcTimeCards.SelectedIndexChanged
        clearForm()
        pnlTimeCard.Visible = True
        If lcTimeCards.SelectedIndex > -1 Then
            Dim eid As String = lcTimeCards.SelectedItem.Value.ToString
            loadTimeCard(eid)
        End If
    End Sub

    Private Sub clearForm()
        lblTime.BackColor = Color.Transparent
        txtPassword.Text = String.Empty
        cbDepartment.SelectedIndex = -1
        cbJobDescription.SelectedIndex = -1
        lblEmployeeName.Text = "<-- SELECT Time Card"
        btnClockIN.Enabled = False
        btnClockOUTgoHOME.Enabled = False
    End Sub

    Private Sub loadTimeCard(ByVal eid As String)
        lblpassworderr.Visible = False
        pnlTimeCard.Visible = True
        lblPrevtx.Text = String.Empty
        lblPrevtx.Visible = False
        lblEmployeeName.Text = lcTimeCards.SelectedItem.Text
        Dim utl As New Utils
        Dim tpList As List(Of TimePunche) = getTimePunches(eid)
        If tpList.Count > 0 Then
            lblToday.Text = String.Empty
            lblToday.TextAlignment = ContentAlignment.TopRight

            For Each tp As TimePunche In tpList
                Dim vjobdesc As String = String.Empty
                Dim vdept As String = String.Empty
                vjobdesc = utl.jobDescriptionByID(tp.tpList(0).JobDescriptionID.ToString)
                vdept = IIf(vjobdesc.Contains("Unload"), "General", utl.deptartmentnamebyid(tp.DepartmentID.ToString))
                lblToday.Text &= vjobdesc & " : " & vdept & vbCrLf
                Dim i As Integer = 0
                Dim sortedtio = tp.tpList.OrderBy(Function(k) k.TimeIn)
                For Each tio As TimeInOut In sortedtio
                    lblToday.Text &= "IN: " & CType(tio.TimeIn, Date).ToShortTimeString & vbCrLf

                    If tio.TimeOut > "1/1/1900" Then
                        lblToday.Text &= "OUT: " & CType(tio.TimeOut, Date).ToShortTimeString & vbCrLf

                        If Not tp.IsClosed And i + 1 <= tp.tpList.Count Then
                            lblToday.Text &= "On Break" & vbCrLf
                            cbDepartment.SelectedItem = cbDepartment.FindItemExact(vdept, False)
                            cbJobDescription.SelectedItem = cbJobDescription.FindItemExact(vjobdesc, False)
                        End If
                    Else
                        lblToday.Text &= "---" & vbCrLf
                    End If
                    i += 1
                Next

                If tp.IsClosed Then lblToday.Text &= "---" & vbCrLf
            Next
        End If
        If isOnClock(eid) Then
            btnClockOUTonBreak.Visible = True
            btnClockOUTgoHOME.Visible = True
            btnClockOUTgoHOME.Enabled = True
            lblDepartment.Visible = False
            lblJobDescription.Visible = False
            lblTime.BackColor = Color.Orange
            cbDepartment.Visible = False
            cbJobDescription.Visible = False
            btnClockIN.Visible = False
            btnClockIN.Enabled = False
        Else
            btnClockOUTonBreak.Visible = False
            btnClockOUTgoHOME.Visible = False
            lblDepartment.Visible = True
            lblJobDescription.Visible = True
            lblTime.BackColor = Color.LawnGreen
            cbDepartment.Visible = True
            cbJobDescription.Visible = True
            btnClockIN.Visible = True
            btnClockIN.Enabled = False
        End If
        txtPassword.Focus()
    End Sub

#Region "Combo Boxes"
    Private Sub loadjobs()
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim dtJobDescription As New DataTable
        dtJobDescription = a.QueryDatabaseForTable("select JobDescription, ID from JobDescriptions Order By JobDescription")
        cbJobDescription.DataSource = dtJobDescription
        cbJobDescription.DisplayMember = "JobDescription"
        cbJobDescription.ValueMember = "ID"
        cbJobDescription.SelectedIndex = -1
        cbJobDescription.DropDownListElement.ListElement.Font = New Font("Arial", 14.0F)
        cbJobDescription.ListElement.ItemHeight = 40
    End Sub

    Private Sub loadDepartments()
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim dtDepartment As DataTable = New DataTable
        dtDepartment = a.QueryDatabaseForTable("select ID as DeptID, Name as DeptName from Department Order By DeptName")
        cbDepartment.DisplayMember = "DeptName"
        cbDepartment.ValueMember = "DeptID"
        cbDepartment.DataSource = dtDepartment
        cbDepartment.DropDownListElement.ListElement.Font = New Font("Arial", 14.0F)
        cbDepartment.ListElement.ItemHeight = 40
        cbDepartment.SelectedIndex = -1
    End Sub
    '@@@@@@@@@@@@@@@@@@@@@@ Combo Box Events@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    Private Sub cbJobDescription_SelectedIndexChanged(sender As Object, e As Data.PositionChangedEventArgs) Handles cbJobDescription.SelectedIndexChanged
        If cbJobDescription.SelectedIndex > -1 Then
            If cbJobDescription.SelectedItem.Text.Contains("Unload") Then
                cbDepartment.SelectedItem = cbDepartment.FindItemExact("General", False)
                cbDepartment.Enabled = False
                btnClockIN.Enabled = True
            Else
                If Not cbDepartment.SelectedItem Is Nothing Then

                    If cbDepartment.SelectedItem.Text = "General" Then
                        cbDepartment.SelectedIndex = -1

                    End If
                End If
                cbDepartment.Enabled = True
                btnClockIN.Enabled = False
                End If
        End If
        If cbDepartment.SelectedIndex > -1 And cbJobDescription.SelectedIndex > -1 Then
            btnClockIN.Enabled = True
        Else
            btnClockIN.Enabled = False
        End If
    End Sub

    Private Sub txtPassword_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyUp
        If txtPassword.Text.Length = 4 And cbDepartment.SelectedIndex > -1 And cbJobDescription.SelectedIndex > -1 Then
            btnClockIN.Enabled = True
        End If
    End Sub

    Private Sub txtPassword_LostFocus(sender As Object, e As EventArgs) Handles txtPassword.LostFocus
        If cbDepartment.SelectedIndex > -1 And cbJobDescription.SelectedIndex > -1 Then
            btnClockIN.Enabled = True
        Else
            btnClockIN.Enabled = False
        End If
    End Sub


    Private Sub cbDepartment_SelectedIndexChanged(sender As Object, e As Data.PositionChangedEventArgs) Handles cbDepartment.SelectedIndexChanged
        If cbDepartment.SelectedIndex > -1 And cbJobDescription.SelectedIndex > -1 Then
            btnClockIN.Enabled = True
        Else
            btnClockIN.Enabled = False
        End If
    End Sub

#End Region '"Combo Boxes"


#Region "Threads"
    Public Sub ThreadMethod1()
        TimerSync.Enabled = False
        Dim isconnected As Boolean = Utils.Connected
        objThread1.UpdateUI(isconnected)
    End Sub

    Public Sub CallBackMethod1(ByVal status As Boolean)
        'call back method for Thread 1 which will write in Green
        If status Then
            labelLastSyncValue.ForeColor = Color.Green
            connum += 1
            con = True
            Dim utl As New Utils
            asyncConn.syncme()
        Else
            labelLastSyncValue.ForeColor = Color.Red
            con = False
        End If
        TimerSync.Enabled = True
    End Sub

    Public Sub ThreadMethod2()
        TimerSync.Enabled = False
        Dim isconnected As Boolean = Utils.Connected
        objThread2.UpdateUI(isconnected)

    End Sub

    Public Sub CallBackMethod2(ByVal status As Boolean)
        'call back method for Thread 2 which will write in Green
        If status Then
            labelLastSyncValue.ForeColor = Color.Green
            con = True
            '           TimerSync.Enabled = True
        Else
            labelLastSyncValue.ForeColor = Color.Red
            labelLastSyncValue.Text = "Working"
            con = False
            objThread2 = New CallBackThread(Me, AddressOf ThreadMethod2, AddressOf CallBackMethod2)
            objThread2.Start()
        End If
    End Sub
#End Region 'Threads

#Region "Labels Buttons/Timer"
    Private Sub TimerSync_Tick(sender As Object, e As EventArgs) Handles TimerSync.Tick
        TimerSync.Enabled = False
        objThread1 = New CallBackThread(Me, AddressOf ThreadMethod1, AddressOf CallBackMethod1)
        objThread1.Start()
    End Sub

    Private Sub labelLastSyncValue_MouseHover(sender As Object, e As EventArgs) Handles labelLastSyncValue.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub
    Private Sub labelLastSyncValue_MouseLeave(sender As Object, e As EventArgs) Handles labelLastSyncValue.MouseLeave
        Me.Cursor = DefaultCursor
    End Sub

    Private Sub labelLastSyncValue_Click(sender As Object, e As EventArgs) Handles labelLastSyncValue.Click
        clearForm()
        lcTimeCards.SelectedIndex = -1
        pnlTimeCard.Visible = False
        lblpassworderr.Visible = False
        TimerSync.Enabled = False
        Dim f As New asyncConn
        f.Show()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearForm()
        lcTimeCards.SelectedIndex = -1
        pnlTimeCard.Visible = lcTimeCards.SelectedIndex > -1
    End Sub

    Private Sub btnHibernate_Click(sender As Object, e As EventArgs) Handles btnHibernate.Click

        TimerSync.Enabled = False
        btnHibernate.Visible = False
    End Sub
    Private Sub btnClockIN_Click(sender As Object, e As EventArgs) Handles btnClockIN.Click
        Dim eid As Guid = lcTimeCards.SelectedItem.Value
        Dim utl As New Utils
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim sqlval As String = "SELECT password FROM Employee WHERE id = '" & eid.ToString & "'"
        Dim isvalid As Boolean = a.QueryDatabaseForScalar(sqlval) = txtPassword.Text
        If isvalid Then
            lblpassworderr.Visible = False
        Else
            lblpassworderr.Visible = True
            Exit Sub
        End If
        Dim tpdal As New TimePuncheDAL
        Dim tp As TimePunche = tpdal.getLatestOpenTimePunchByEmployeeID(eid.ToString)
        If tp Is Nothing Then 'create new timepunche object
            tp = New TimePunche
            tp.ID = Guid.NewGuid
            tp.DateWorked = Date.Now.ToShortDateString
            tp.LocationID = New Guid(My.Settings.locaID)
            tp.LocationName = locaname
            tp.IsClosed = False
            tp.EmployeeID = eid
            tp.DepartmentID = cbDepartment.SelectedItem.Value
        Else 'use existing
            If tp.DepartmentID <> cbDepartment.SelectedItem.Value Then
                'changed department so 'close existing and create new timepunche
                tp.IsClosed = True
                tpdal.updateTimePunche(tp)
                tp = New TimePunche
                tp.ID = Guid.NewGuid
                tp.DateWorked = Date.Now.ToShortDateString
                tp.LocationID = New Guid(My.Settings.locaID)
                tp.LocationName = locaname
                tp.IsClosed = False
                tp.EmployeeID = eid
                tp.DepartmentID = cbDepartment.SelectedItem.Value
            End If
        End If
        Dim tio As New TimeInOut
        tio.ID = Guid.NewGuid
        tio.TimepuncheID = tp.ID
        tio.isHourly = Not cbJobDescription.Text.Contains("Percentage") And Not cbJobDescription.Text.Contains("%")
        tio.JobDescriptionID = cbJobDescription.SelectedItem.Value
        tio.TimeIn = Date.Now

        tpdal.insertTimePunche(tp)
        tpdal.insertTIO(tio)

        Dim sqlEname As String = "SELECT FirstName + ' ' + LastName AS Name FROM Employee WHERE id = '" & eid.ToString & "'"
        Dim eName As String = a.QueryDatabaseForScalar(sqlEname)
        lblPrevtx.Visible = True
        lblPrevtx.Text = eName & ":-> Clocked IN"
        clearForm()
        lcTimeCards.SelectedIndex = -1
        pnlTimeCard.Visible = False
        lblpassworderr.Visible = False
        TimerSync.Enabled = False
        Dim f As New asyncConn
        f.Show()

    End Sub

    Private Sub btnClockOUTonBreak_Click(sender As Object, e As EventArgs) Handles btnClockOUTonBreak.Click
        Dim eid As Guid = lcTimeCards.SelectedItem.Value
        Dim utl As New Utils
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim sqlval As String = "SELECT password FROM Employee WHERE id = '" & eid.ToString & "'"
        Dim isvalid As Boolean = a.QueryDatabaseForScalar(sqlval) = txtPassword.Text
        If Not isvalid Then
            lblpassworderr.Visible = True
            Exit Sub
        End If
        Dim tpdal As New TimePuncheDAL
        Dim tp As TimePunche = tpdal.getLatestOpenTimePunchByEmployeeID(eid.ToString)
        Dim int As Integer = tp.tpList.Count - 1
        Dim tio As TimeInOut = tp.tpList(int)
        tio.TimeOut = Date.Now
        tpdal.UpdateTIO(tio)
        Dim sqlEname As String = "SELECT FirstName + ' ' + LastName AS Name FROM Employee WHERE id = '" & eid.ToString & "'"
        Dim eName As String = a.QueryDatabaseForScalar(sqlEname)
        lblPrevtx.Visible = True
        lblPrevtx.Text = eName & ":-> Clocked OUT on Break"

        clearForm()
        lcTimeCards.SelectedIndex = -1
        pnlTimeCard.Visible = False
        lblpassworderr.Visible = False
        TimerSync.Enabled = False
        Dim f As New asyncConn
        f.Show()
    End Sub

    Private Sub btnClockOUTgoHOME_Click(sender As Object, e As EventArgs) Handles btnClockOUTgoHOME.Click
        Dim eid As Guid = lcTimeCards.SelectedItem.Value
        Dim utl As New Utils
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim sqlval As String = "SELECT password FROM Employee WHERE id = '" & eid.ToString & "'"
        Dim isvalid As Boolean = a.QueryDatabaseForScalar(sqlval) = txtPassword.Text
        If Not isvalid Then
            lblpassworderr.Visible = True
            Exit Sub
        End If
        Dim tpdal As New TimePuncheDAL
        Dim tp As TimePunche = tpdal.getLatestOpenTimePunchByEmployeeID(eid.ToString)
        tp.IsClosed = True
        tpdal.updateTimePunche(tp)
        Dim int As Integer = tp.tpList.Count - 1
        Dim tio As TimeInOut = tp.tpList(int)
        tio.TimeOut = Date.Now
        tpdal.UpdateTIO(tio)
        Dim sqlEname As String = "SELECT FirstName + ' ' + LastName AS Name FROM Employee WHERE id = '" & eid.ToString & "'"
        Dim eName As String = a.QueryDatabaseForScalar(sqlEname)
        lblPrevtx.Visible = True
        lblPrevtx.Text = eName & ":-> Clocked OUT going Home"
        clearForm()
        lcTimeCards.SelectedIndex = -1
        pnlTimeCard.Visible = False
        lblpassworderr.Visible = False
        TimerSync.Enabled = False
        Dim f As New asyncConn
        f.Show()
    End Sub

    Private Sub TimerClock_Tick(sender As Object, e As EventArgs) Handles TimerClock.Tick
        lblTime.Text = Date.Now.ToShortTimeString
        Dim intSyncInterval As Integer = 10000 '30 seconds
        TimerClock.Interval = intSyncInterval
    End Sub
#End Region '"Buttons/Timer"

    Public Function getTimePunches(ByVal eid As String) As List(Of TimePunche)
        Dim tpList As New List(Of TimePunche)
        Dim tp As New TimePunche
        Dim tioList As New List(Of TimeInOut)
        Dim tio As New TimeInOut
        Dim dt As New DataTable
        Dim strSQL As String = "SELECT tp.ID, tp.EmployeeID, tp.DateWorked, tp.DepartmentID, tp.IsClosed, tp.LocationID " & _
            "FROM TimePunche AS tp INNER JOIN " & _
            "Employee AS e ON tp.EmployeeID = e.ID " & _
            "WHERE (e.ID = '" & eid & "')"
        '            "WHERE (e.ID = '" & eid & "') AND (tp.DateWorked = '" & Date.Now.ToShortDateString & "')"
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        dt = a.QueryDatabaseForTable(strSQL)
        If Not dt Is Nothing Then

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    tp = New TimePunche
                    tp.ID = row.Item("ID")
                    tp.DateWorked = row.Item("DateWorked")
                    tp.DepartmentID = row.Item("DepartmentID")
                    tp.EmployeeID = row.Item("EmployeeID")
                    tp.IsClosed = row.Item("IsClosed")
                    tp.LocationID = row.Item("LocationID")
                    tpList.Add(tp)
                Next
                For Each tp In tpList
                    strSQL = "SELECT ID, TimePuncheID, JobDescriptionID, TimeIn, TimeOut from TimeInOut where TimePuncheID = '" & tp.ID.ToString & "'"
                    dt = a.QueryDatabaseForTable(strSQL)
                    If dt.Rows.Count > 0 Then
                        For Each row As DataRow In dt.Rows
                            tio = New TimeInOut
                            tio.ID = row.Item("ID")
                            tio.TimepuncheID = row.Item("TimePuncheID")
                            tio.JobDescriptionID = row.Item("JobDescriptionID")
                            tio.TimeIn = row.Item("TimeIn")
                            tio.TimeOut = row.Item("TimeOut")
                            tioList.Add(tio)
                        Next
                        tp.tpList = tioList
                    End If
                Next
            Else
                lblToday.TextAlignment = ContentAlignment.TopCenter
                lblToday.Text = vbCrLf & vbCrLf & "         Enjoy your Day," & vbCrLf & "    Play nice with others" & vbCrLf & "         and BE SAFE!" & vbCrLf
            End If
        End If

        Return tpList
    End Function

    Public Function getFirstNameByeid(ByVal eid As String) As String
        Dim retstr As String = String.Empty
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim strSQL As String = "SELECT firstname from employee where id ='" & eid & "'"
        Try
            retstr = a.QueryDatabaseForScalar(strSQL)
        Catch ex As Exception
            retstr = "--"
        End Try
        Return retstr
    End Function

    Public Function isOnClock(ByVal eid As String) As Boolean
        Dim retboo As Boolean = False
        Dim a As New SqlCeAdapter("TimeClock.sdf")

        Dim strSQL As String = "SELECT Employee.ID, Employee.FirstName, Employee.LastName, TimeInOut.TimeIn, TimeInOut.TimeOut " & _
            "FROM Employee INNER JOIN " & _
            "TimePunche ON Employee.ID = TimePunche.EmployeeID INNER JOIN " & _
            "TimeInOut ON TimePunche.ID = TimeInOut.TimepuncheID " & _
            "WHERE (TimeInOut.TimeOut = CONVERT(DATETIME, '1900-01-01 00:00:00', 102)) AND (Employee.ID = '" & eid & "') OR " & _
            "(TimeInOut.TimeOut IS NULL) "
        Dim dtemponclock As DataTable = a.QueryDatabaseForTable(strSQL)
        '        If dtemponclock.Rows.Count > 0 Then empName = dtemponclock.Rows(0).Item("FirstName") & dtemponclock.Rows(0).Item("LastName")
        If Not dtemponclock Is Nothing Then
            retboo = dtemponclock.Rows.Count > 0
        Else
            retboo = False
        End If
        Return retboo
    End Function

    Public Function isoutforday(ByVal eid As String) As Boolean
        Dim retbool As Boolean = True
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim strSQL As String = "SELECT e.id,e.FirstName, e.LastName " & _
        "FROM TimePunche INNER JOIN " & _
        "Employee e ON TimePunche.EmployeeID = e.ID INNER JOIN " & _
        "TimeInOut Tio ON TimePunche.ID = Tio.TimepuncheID " & _
        "WHERE (Tio.TimeOut > '1/1/1900') AND (e.id = '" & eid & "') and timepunche.isclosed = 1"
        Dim dtemponclock As New DataTable
        dtemponclock = a.QueryDatabaseForTable(strSQL)
        Dim empName As String = String.Empty
        If dtemponclock.Rows.Count > 0 Then empName = dtemponclock.Rows(0).Item("FirstName") & dtemponclock.Rows(0).Item("LastName")
        retbool = dtemponclock.Rows.Count > 0
        Return retbool
    End Function

    Public Function isOnBreak(ByVal eid As String) As Boolean
        Dim retbool As Boolean = True
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim strSQL As String = "SELECT e.id,e.FirstName, e.LastName,Tio.TimeIn,Tio.TimeOut  " & _
        "FROM TimePunche INNER JOIN " & _
        "Employee e ON TimePunche.EmployeeID = e.ID INNER JOIN " & _
        "TimeInOut Tio ON TimePunche.ID = Tio.TimepuncheID " & _
        "WHERE (Tio.TimeOut > '1/1/1900') AND (e.id = '" & eid & "') and timepunche.isclosed = 0"
        Dim dtemponclock As New DataTable
        dtemponclock = a.QueryDatabaseForTable(strSQL)
        Dim empName As String = String.Empty
        If dtemponclock.Rows.Count > 0 Then empName = dtemponclock.Rows(0).Item("FirstName") & dtemponclock.Rows(0).Item("LastName")
        retbool = dtemponclock.Rows.Count > 0
        Return retbool
    End Function


End Class

Public Class Unloader
#Region "Private Variables"
    Private _EmployeeID As Guid
    Private _EmployeeName As String
    Private _EmployeeLogin As String
#End Region
#Region "Public Properties"
    Public Property EmployeeID() As Guid
        Get
            Return _EmployeeID
        End Get
        Set(ByVal value As Guid)
            _EmployeeID = value
        End Set
    End Property
    Public Property EmployeeName() As String
        Get
            Return _EmployeeName
        End Get
        Set(ByVal value As String)
            _EmployeeName = value
        End Set
    End Property
    Public Property EmployeeLogin() As String
        Get
            Return _EmployeeLogin
        End Get
        Set(ByVal value As String)
            _EmployeeLogin = value
        End Set
    End Property

#End Region

End Class

Class UnLoaderUtils

    Public Function isUnloader(ByVal eid As String) As Boolean
        Dim retbool As Boolean
        Return retbool
    End Function

    Public Function isOnLoad(ByVal eid As String) As Boolean
        Dim numloads As Integer = 0
        numloads = countUnloaderLoads(eid)
        isOnLoad = numloads > 0
        Return isOnLoad
    End Function


    Public Function getOpenLoads() As DataTable
        Dim dt As New DataTable
        'Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        'Dim strSQL As String = "SELECT id from workorder where status < 74"
        Return dt
    End Function

    Public Function getOpenLoaders() As DataTable
        Dim dt As DataTable = New DataTable

        Return dt

    End Function

    Public Function countUnloaderLoads(ByVal eid As String) As Integer
        Dim retstr As Integer = 0
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim strSQL As String = "SELECT COUNT(WorkOrder.ID) AS Expr1 " & _
            "FROM WorkOrder INNER JOIN " & _
            "Unloader ON WorkOrder.ID = Unloader.LoadID " & _
            "WHERE (WorkOrder.Status < 74) " & _
            "AND (Unloader.EmployeeID = '" & eid & "')"
        retstr = a.QueryDatabaseForScalar(strSQL)
        Return retstr
    End Function

End Class
