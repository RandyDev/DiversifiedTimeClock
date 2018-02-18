Imports System
Imports System.Data
Imports System.Xml
Imports System.Data.SqlServerCe
Imports System.Runtime.InteropServices
Imports System.Net

Public Class Utils
    Private Shared count As Int16 = 1

    Public Function isValidGUID(ByVal str As String) As Boolean
        If str = "00000000-0000-0000-0000-000000000000" Then
            Return False
        End If
        Dim retbool As Boolean = Nothing
        Dim myguid As Guid = Nothing
        Try
            myguid = New Guid(str)
            retbool = True
        Catch ex As Exception
            retbool = False
        End Try
        Return retbool
    End Function

    Public Function zeroGuid() As Guid
        Return New Guid("00000000-0000-0000-0000-000000000000")
    End Function

    Public Function getempname(ByVal empid As String) As String
        Dim retstr As String = String.Empty
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim strSQL As String = "Select firstname + ' ' + lastname from employee where id = '" & empid & "'"
        retstr = a.QueryDatabaseForScalar(strSQL)
        Return retstr
    End Function

    Public Function getxmldata(ByVal itm As String) As String
        Dim strRet As String = String.Empty
        Dim locaconfig As String = "TimeClockConfig.xml"
        Dim xmldoc As New XmlDocument
        xmldoc.Load(locaconfig)
        '        Dim txt As XmlNode = xmldoc.SelectSingleNode("/SEUdockConfig/" & itm)
        '        strRet = txt.InnerText
        Dim txt As XmlNode = xmldoc.SelectSingleNode("/SEUdockConfig/DefaultLocation")
        With txt
            Select Case itm
                Case "Location_Name"
                    strRet = .SelectSingleNode("Location_Name").InnerText
                Case "Location_ID"
                    strRet = .SelectSingleNode("Location_ID").InnerText
                Case "Parent_Name"
                    strRet = .SelectSingleNode("Parent_Name").InnerText
                Case "Parent_ID"
                    strRet = .SelectSingleNode("Parent_ID").InnerText
                Case "Print_TimeStamp"
                    strRet = .SelectSingleNode("Print_TimeStamp").InnerText
                Case "Sync_Interval"
                    strRet = .SelectSingleNode("Sync_Interval").InnerText
            End Select
        End With
        If strRet = "00000000-0000-0000-0000-000000000000" Then
            'do something?
        End If
        Return strRet
    End Function

    Public Function getVendorName(ByVal vid As String, ByVal pid As String) As String
        Dim retstr As String = String.Empty
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim strSQL As String = "Select Name from Vendor where number = '" & vid & "' AND ParentCompanyID = '" & pid & "'"
        retstr = a.QueryDatabaseForScalar(strSQL)
        If vid > "" Then
            If retstr = "" Then retstr = "NOT LISTED"
        End If
        Return retstr
    End Function

    '*********************************************************************************************************
    Public Function getCustomerID(ByVal parentID As String, ByVal vnum As String) As String
        Dim cid As String = Nothing
        '*****************
        '*****************
        Dim conn As New SqlCeConnection("DataSource = TimeClock.sdf;")
        conn.Open()
        Dim command As SqlCeCommand = conn.CreateCommand
        command.CommandText = "SELECT Vendor.ID FROM Vendor WHERE ParentCompanyID = @parentID  AND (Vendor.Number = @vnum) "
        Dim param As SqlCeParameter
        param = New SqlCeParameter("@parentID", parentID)
        command.Parameters.Add(param)
        param = New SqlCeParameter("@vnum", vnum)
        command.Parameters.Add(param)

        Dim Vendor As DataTable = New DataTable
        command.Prepare()
        Dim sqlreader = command.ExecuteReader
        Vendor.Load(sqlreader)
        sqlreader.Close()

        Dim dt As DataTable = Vendor
        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            cid = row.Item("ID").ToString
        Else
            cid = "00000000-0000-0000-0000-000000000000"

        End If
        Return cid
    End Function

    Public Function getstatusText(ByVal status As Integer, ByVal sched As Boolean) As String
        Dim retstr As String = String.Empty
        Select Case status
            Case 1, 9, 3
                If sched Then
                    retstr = "Scheduled"
                Else
                    retstr = "Checked In"
                End If
            Case 10
                retstr = "Assigned"
            Case 74
                retstr = "Not Printed"
            Case 76
                retstr = "Re-Print"
            Case 78
                retstr = "Complete"
        End Select
        Return retstr
    End Function

    Public Function carriernamebyid(ByVal cid As String) As String
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim strSQL As String = "Select name from carrier where id = '" & cid & "'"
        Dim retstr As String = a.QueryDatabaseForScalar(strSQL)
        retstr = IIf(retstr = "", "NOT LISTED", retstr)
        Return retstr
    End Function

    Public Function deptartmentnamebyid(ByVal did As String) As String
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim strSQL As String = "Select Name from department where id = '" & did & "'"
        Dim retstr As String = a.QueryDatabaseForScalar(strSQL)
        Return retstr
    End Function

    Public Function jobDescriptionByID(ByVal jobdescriptionid As String) As String
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim strSQL As String = "Select JobDescription from JobDescriptions where id = '" & jobdescriptionid & "'"
        Dim retstr As String = a.QueryDatabaseForScalar(strSQL)
        Return retstr
    End Function

    ''create the SEUdockConfig.xml file
    Public Sub createConfig(ByVal LocationName As String, ByVal LocationID As String, ByVal LocationPrefix As String, ByVal ParentName As String, ByVal ParentID As String, ByVal SyncInterval As String, ByVal PrintTimeStamp As String)
        Dim xwriter As New XmlTextWriter("TimeClockConfig.xml", System.Text.Encoding.UTF8)
        xwriter.WriteStartDocument(True)
        xwriter.Formatting = Formatting.Indented
        xwriter.Indentation = 4
        xwriter.WriteStartElement("TimeClockConfig ")

        xwriter.WriteStartElement("DefaultLocation")

        xwriter.WriteStartElement("Location_Name")
        xwriter.WriteString(LocationName)
        xwriter.WriteEndElement()

        xwriter.WriteStartElement("Location_ID")
        xwriter.WriteString(LocationID)
        xwriter.WriteEndElement()

        xwriter.WriteStartElement("Location_Prefix")
        xwriter.WriteString(LocationPrefix)
        xwriter.WriteEndElement()

        xwriter.WriteStartElement("Parent_Name")
        xwriter.WriteString(ParentName)
        xwriter.WriteEndElement()

        xwriter.WriteStartElement("Parent_ID")
        xwriter.WriteString(ParentID)
        xwriter.WriteEndElement()

        xwriter.WriteStartElement("Sync_Interval")
        xwriter.WriteString(SyncInterval)
        xwriter.WriteEndElement()


        xwriter.WriteEndElement() 'Default Location
        xwriter.WriteEndElement()
        xwriter.WriteEndDocument()
        xwriter.Close()

    End Sub

    Public Sub doconfig()
        Dim locaid As String = My.Settings.locaID
        Cursor.Current = Cursors.WaitCursor
        '        DiversifiedTimeClock.lblCompanyAndLocation.Text = "Building New Configuration File"
        Dim locationName As String = String.Empty
        ''start gathering information to create SEUdockConfig.xml
        Dim ParentCompanyName As String = String.Empty
        Dim ParentCompanyID As String = String.Empty
        Dim syncInterval As Integer = 20
        Dim printTimeStamp As Boolean = False
        'gather config data
        Dim a As New SqlCeAdapter("Locations.sdf")
        Dim dt As DataTable = New DataTable
        Dim strqry As String = "select Name,ParentCompanyID, hhSyncInterval, hhPrintTimeStamp  From Location WHERE (Location.ID = '" & locaid & "')"
        dt = a.QueryDatabaseForTable(strqry)
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    ParentCompanyID = row.Item("ParentCompanyID").ToString
                    locationName = row.Item("Name")
                    syncInterval = row.Item("hhSyncInterval").ToString
                    printTimeStamp = row.Item("hhPrintTimeStamp").ToString
                Next
            End If
            strqry = "select Name From ParentCompany where (id= '" & ParentCompanyID & "')"
            ParentCompanyName = a.QueryDatabaseForScalar(strqry)
            'finish config data gathering and create config xml file
            Dim utl As New Utils
            utl.createConfig(locationName, locaid, "", ParentCompanyName, ParentCompanyID, syncInterval, printTimeStamp)
            '            btnReturn.Enabled = True
            '            lblNetworkConnection.Text = "Configuration File Created" & vbCrLf & locationName
        End If
        Cursor.Current = Cursors.Default
    End Sub

    <System.Runtime.InteropServices.DllImport("wininet.dll")> _
    Public Shared Function InternetGetConnectedState(ByRef Description As Integer, ByVal ReservedValue As Integer) As Boolean
    End Function
    Public Shared Function CheckNet() As Boolean
        Return InternetGetConnectedState(0&, 0&) = 1
    End Function


    Public Shared Function Connected() As Boolean
        ' Call this class as follows: bool bResponse = Net.IsWebAccessible();
        Dim hwrRequest As HttpWebRequest
        Dim hwrResponse As HttpWebResponse
        Dim bConnected As Boolean = False
        Dim strurl As String = String.Empty
        Try
            If (count = 1 Or count = 4) Then
                strurl = "http://seu.div-log.com" + "/"
            ElseIf (count = 2 Or count = 5) Then
                strurl = "http://google.com" + "/"
            ElseIf (count = 3 Or count = 6) Then
                strurl = "http://yahoo.com" + "/"
            End If
            ' If count = 17 Then count = 1
            count = count + 1
            If count = 7 Then
                count = 1
            End If
            ' hwrRequest = DirectCast(WebRequest.Create(strUrl), HttpWebRequest)
            hwrRequest = WebRequest.Create(strurl)

            hwrRequest.Timeout = 10000
            'hwrRequest.Method = "GET"
            'hwrRequest.Proxy = System.Net.GlobalProxySelection.GetEmptyWebProxy()
            'hwrResponse = DirectCast(hwrRequest.GetResponse(), HttpWebResponse)
            hwrResponse = hwrRequest.GetResponse()
            If hwrResponse.StatusCode = HttpStatusCode.OK Then
                bConnected = True
            End If

            hwrRequest.Abort()

            hwrRequest = Nothing
            hwrResponse = Nothing
        Catch we As WebException
            bConnected = False
            hwrRequest = Nothing
            hwrResponse = Nothing
        Catch ex As Exception
            bConnected = False
            hwrRequest = Nothing
            hwrResponse = Nothing


        End Try

        Return bConnected
    End Function
    Dim vrecordcount As Integer = 0
    Dim seudockadapter As New SqlCeAdapter("SEUdock.sdf")
    '    Dim seuholdadapter As New SqlCeAdapter("\Program Files\SEUdock\SEUdockHold.sdf")
    Dim strSQL As String

    Public Sub CreateyesterdayDB()
        'Dim dt As DataTable = New DataTable
        'Dim wodal As New WorkOrderDAL
        'If Not System.IO.File.Exists("\Program Files\SEUdock\SEUdockHold.sdf") Then
        '    CreateHoldDB()
        'End If
        '' do any of these tables have records?
        'vrecordcount = 0
        'strSQL = "Select Count(ID) from WorkOrder"
        'vrecordcount += seudockadapter.QueryDatabaseForScalar(strSQL)
        'strSQL = "Select Count(ID) from Unloader"
        'vrecordcount += seudockadapter.QueryDatabaseForScalar(strSQL)
        'strSQL = "Select Count(ID) from TimePunche"
        'vrecordcount += seudockadapter.QueryDatabaseForScalar(strSQL)
        'strSQL = "Select Count(ID) from TimeInOut"
        'vrecordcount += seudockadapter.QueryDatabaseForScalar(strSQL)
        'If vrecordcount > 0 Then 'we need to save the records
        '    Dim woList As List(Of WorkOrder) = New List(Of WorkOrder)
        '    dt = New DataTable
        '    strSQL = "Select ID FROM WorkOrder"
        '    dt = seudockadapter.QueryDatabaseForTable(strSQL)
        '    For Each row As DataRow In dt.Rows
        '        Dim wo As WorkOrder
        '        wo = wodal.GetLoadByID(row.Item(0).ToString)
        '        woList.Add(wo)
        '    Next
        '    For Each existwo As WorkOrder In woList
        '        wodal.AddHoldWorkOrder(existwo)
        '    Next

        '    Dim tpdal As New TimePuncheDAL
        '    Dim tpList As List(Of TimePunche) = New List(Of TimePunche)
        '    strSQL = "Select ID FROM TimePunche"
        '    dt = seudockadapter.QueryDatabaseForTable(strSQL)
        '    For Each row As DataRow In dt.Rows
        '        Dim tp As New TimePunche
        '        tp = tpdal.getTimePuncheByID(row.Item(0).ToString)
        '        tpList.Add(tp)
        '    Next
        '    For Each vtp As TimePunche In tpList
        '        tpdal.insertHoldTimePunche(vtp)
        '    Next

        '    Dim tioList As List(Of TimeInOut) = New List(Of TimeInOut)
        '    strSQL = "Select ID FROM TimeInOut"
        '    dt = seudockadapter.QueryDatabaseForTable(strSQL)
        '    For Each row As DataRow In dt.Rows
        '        Dim tio As New TimeInOut
        '        tio = tpdal.getTimeInOutID(row.Item(0).ToString)
        '        tioList.Add(tio)
        '    Next
        '    For Each vtio As TimeInOut In tioList
        '        tpdal.insertHoldTIO(vtio)
        '    Next
        'End If
    End Sub

    Public Sub getholdrecords()
    End Sub

    Public Sub clearHoldRecords()
    End Sub

    Protected Sub CreateHoldDB()
    End Sub

    Public Sub getholdData()
    End Sub
    Public Sub GetLocationSnapShot()
        '        System.IO.File.Delete("\Program Files\SEUdock\Locations.sdf")
        Dim repl As SqlCeReplication = New SqlCeReplication
        repl.InternetUrl = "http://seudockrepl.seuhh.com/SEUdock/sqlcesa35.dll"
        repl.Publisher = "SQL-SBS"
        repl.PublisherDatabase = "RTDS"
        repl.PublisherSecurityMode = SecurityType.DBAuthentication
        repl.PublisherLogin = "rtds"
        repl.PublisherPassword = "southeast1"
        repl.InternetLogin = "div-log\SEUdockSyncUser" '"MyInternetLogin"
        repl.InternetPassword = "2n@f1s#" '"<password>"
        'repl.HostName = "cfe1f703-f8d3-4f23-b30c-192672b13bcc"
        repl.Publication = "rtdsLocations"
        repl.Subscriber = "Locations"
        repl.SubscriberConnectionString = "Data Source=Locations.sdf"
        'delete existing .sdf file if exist
        'TO DO ... check for SEUdock.sdf location table.
        If System.IO.File.Exists("Locations.sdf") Then
            System.IO.File.Delete("Locations.sdf")
        Else
            Cursor.Current = Cursors.Default
        End If
        Cursor.Current = Cursors.WaitCursor
        Try
            'init new Locations Subscription for Settings Page.
            repl.AddSubscription(AddOption.CreateDatabase)
            repl.Synchronize()
        Catch ex As Exception
            MessageBox.Show("Failed to initialize 'Locations' Subscription" & vbCrLf & _
                            ex.Message, "Subscription Init Failed!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        Finally
            repl.Dispose()
            Cursor.Current = Cursors.Default
        End Try

        Cursor.Current = Cursors.Default

    End Sub

    Public Sub getSnapShot()
        Dim locaid As String = My.Settings.locaID

        DiversifiedTimeClock.TimerSync.Enabled = False
        Dim filename As String = "TimeClock.sdf"
        Dim repl As SqlCeReplication = New SqlCeReplication
        '        repl.SubscriberConnectionString = "Data Source=\Program Files\SEUdock\SEUdock.sdf"
        repl.SubscriberConnectionString = "Data Source='" + filename + "';Password='';" & "Max Database Size='4091';ssce:default lock timeout='4000';Default Lock Escalation ='100';"
        repl.InternetUrl = "http://seudockrepl.seuhh.com/SEUdock/sqlcesa35.dll"
        repl.Publisher = "SQL-SBS"
        repl.PublisherDatabase = "RTDS"
        repl.PublisherSecurityMode = SecurityType.DBAuthentication
        repl.PublisherLogin = "rtds"
        repl.PublisherPassword = "southeast1"
        repl.InternetLogin = "div-log\SEUdockSyncUser" '"MyInternetLogin"
        repl.InternetPassword = "2n@f1s#" '"<password>"
        repl.HostName = locaid 'Guid as string passed mySettings form
        repl.Publication = "SEUdock"

        Dim utl As New Utils
        repl.Subscriber = locaid 'set to LocationName by mySettings Form
        If System.IO.File.Exists(filename) Then
            '            lblProgress.Visible = True
            '            lblProgress.Text = "Backing up database"
            '            utl.CreateyesterdayDB()
            Try
                System.IO.File.Delete(filename)
            Catch ex As Exception
                Dim msgex As String = ex.Message

            End Try
        End If
        repl.AddSubscription(AddOption.CreateDatabase)
        '        lblProgress.Text = String.Empty
        '        lblProgress.Visible = False
        Cursor.Current = Cursors.WaitCursor
        Try
            repl.Synchronize()
        Catch ex As SqlCeException
            '            MessageBox.Show(ex.Message)
            ' a mechanism to retry until con=true
        Finally
            Cursor.Current = Cursors.Default
            repl.SaveProperties()
            repl.Dispose()
        End Try
        Cursor.Current = Cursors.Default

    End Sub 'New


End Class

