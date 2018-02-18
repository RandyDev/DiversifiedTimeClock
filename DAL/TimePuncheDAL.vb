Imports System.Data
Imports System.Data.SqlServerCe

Public Class TimePuncheDAL

    Public Function insertTimePunche(ByVal tp As TimePunche) As String
        Dim errStr As String = String.Empty
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim sqlstr As String
        Dim tpid As String = tp.ID.ToString
        Dim tpeid As String = tp.EmployeeID.ToString
        Dim tpdeptid As String = tp.DepartmentID.ToString
        sqlstr = "INSERT INTO TimePunche(ID,EmployeeID,DepartmentID,DateWorked,LocationID,isClosed) values('" & tpid & "','" & tpeid & "','" & tpdeptid & "','" & tp.DateWorked & "','" & tp.LocationID.ToString & "','" & tp.IsClosed & "')"
        Try
            a.ExecuteNonQuery(sqlstr)
        Catch ex As Exception
            errStr = ex.Message
            Dim result1 As DialogResult = MessageBox.Show(errStr, "ooophf", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        End Try
        Return errStr
    End Function

    Public Function updateTimePunche(ByVal tp As TimePunche) As String
        Dim errStr As String = String.Empty
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim sqlstr As String
        Dim tpid As String = tp.ID.ToString
        Dim tpeid As String = tp.EmployeeID.ToString
        Dim tpdeptid As String = tp.DepartmentID.ToString
        sqlstr = "Update TimePunche SET ID='" & tpid & "',EmployeeID='" & tpeid & "',DepartmentID='" & tpdeptid & "',DateWorked='" & tp.DateWorked & "',LocationID='" & tp.LocationID.ToString & "',isClosed='" & tp.IsClosed & "' WHERE ID= '" & tp.ID.ToString & "'"
        Try
            a.ExecuteNonQuery(sqlstr)
        Catch ex As Exception
            errStr = ex.Message
            Dim result1 As DialogResult = MessageBox.Show(errStr, "ooophf", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        End Try
        Return errStr
    End Function

    Public Function insertTIO(ByVal tio As TimeInOut) As String
        Dim errStr As String = String.Empty
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim sqlstr As String
        sqlstr = "INSERT INTO TimeInOut (ID,TimepuncheID, TimeIn,TimeOut, IsHourly, JobDescriptionID) values ('" & tio.ID.ToString & "','" & tio.TimepuncheID.ToString & "','" & tio.TimeIn & "','1/1/1900','" & tio.isHourly & "','" & tio.JobDescriptionID.ToString & "')"
        Try
            a.ExecuteNonQuery(sqlstr)
        Catch ex As Exception
            errStr = ex.Message
            Dim result1 As DialogResult = MessageBox.Show(errStr, "ooophf", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        End Try
        Return errStr
    End Function

    Public Function UpdateTIO(ByVal tio As TimeInOut) As String
        Dim errStr As String = String.Empty
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim sqlstr As String
        sqlstr = "UPDATE TimeInOut Set ID='" & tio.ID.ToString & "',TimepuncheID='" & tio.TimepuncheID.ToString & "', TimeIn='" & tio.TimeIn & "',TimeOut='" & tio.TimeOut & "',IsHourly='" & tio.isHourly & "' WHERE id = '" & tio.ID.ToString & "'"
        Try
            a.ExecuteNonQuery(sqlstr)
        Catch ex As Exception
            errStr = ex.Message
            Dim result1 As DialogResult = MessageBox.Show(errStr, "ooophf", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        End Try
        Return errStr
    End Function


    Public Function getTimePuncheByID(ByVal tpid As String) As TimePunche
        Dim tp As TimePunche = New TimePunche

        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim dt As DataTable = New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT ID, EmployeeID,DepartmentID,DateWorked,LocationID,isClosed from TimePunche where ID ='" & tpid & "' order by dateworked"
        dt = a.QueryDatabaseForTable(sqlstr)
        If dt Is Nothing Then
            Return tp
        Else

            If dt.Rows.Count > 0 Then
                tp = New TimePunche

                tp.ID = IIf(IsDBNull(dt.Rows(0).Item("ID")), "", dt.Rows(0).Item("ID"))
                tp.EmployeeID = IIf(IsDBNull(dt.Rows(0).Item("EmployeeID")), "", dt.Rows(0).Item("EmployeeID"))
                tp.DepartmentID = IIf(IsDBNull(dt.Rows(0).Item("DepartmentID")), "", dt.Rows(0).Item("DepartmentID"))
                tp.DateWorked = IIf(IsDBNull(dt.Rows(0).Item("DateWorked")), "", dt.Rows(0).Item("DateWorked"))
                tp.LocationID = IIf(IsDBNull(dt.Rows(0).Item("LocationID")), "", dt.Rows(0).Item("LocationID"))
                tp.IsClosed = IIf(IsDBNull(dt.Rows(0).Item("isClosed")), False, dt.Rows(0).Item("isClosed"))

                sqlstr = "SELECT ID, TimepuncheID,TimeIn,TimeOut,IsHourly,JobDescriptionID FROM TimeInOut Where TimepuncheID = '" & tp.ID.ToString & "' ORDER BY TimeIn"
                dt = a.QueryDatabaseForTable(sqlstr)
                Dim tiolist As New List(Of TimeInOut)
                For Each row As DataRow In dt.Rows
                    Dim tio As New TimeInOut
                    tio.ID = row.Item("ID")
                    tio.TimepuncheID = row.Item("TimepuncheID")
                    tio.TimeIn = row.Item("TimeIn")
                    tio.TimeOut = row.Item("TimeOut")
                    tio.isHourly = row.Item("IsHourly")
                    tio.JobDescriptionID = row.Item("JobDescriptionID")
                    tiolist.Add(tio)
                Next
                tp.tpList = tiolist
            End If
        End If
        Return tp
    End Function

    Public Function getTimeInOutID(ByVal tioid As String) As TimeInOut
        Dim errStr As String = String.Empty
        Dim tio As TimeInOut = New TimeInOut
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim dt As DataTable = New DataTable
        Dim sqlstr As String
        sqlstr = "SELECT ID,TimepuncheID, TimeIn,TimeOut, IsHourly, JobDescriptionID from TimeInOut where ID ='" & tioid & "'"
        dt = a.QueryDatabaseForTable(sqlstr)
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                tio = New TimeInOut
                tio.ID = IIf(IsDBNull(dt.Rows(0).Item("ID")), "", dt.Rows(0).Item("ID"))
                tio.TimepuncheID = IIf(IsDBNull(dt.Rows(0).Item("TimepuncheID")), "", dt.Rows(0).Item("TimepuncheID"))
                tio.TimeOut = IIf(IsDBNull(dt.Rows(0).Item("TimeOut")), "", dt.Rows(0).Item("TimeOut"))
                tio.isHourly = IIf(IsDBNull(dt.Rows(0).Item("isHourly")), "", dt.Rows(0).Item("IsHourly"))
                tio.JobDescriptionID = IIf(IsDBNull(dt.Rows(0).Item("JobDescriptionID")), "", dt.Rows(0).Item("JobDescriptionID"))
            End If
        End If
        Return tio
    End Function

    Public Function getLatestOpenTimePunchByEmployeeID(ByVal empid As String) As TimePunche
        Dim tp As New TimePunche
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim dt As DataTable = New DataTable
        Dim sqlstr As String
        sqlstr = "select id from TimePunche " & _
        "where EmployeeID='" & empid & "' " & _
        "and IsClosed = 'False'"
        dt = a.QueryDatabaseForTable(sqlstr)
        If dt.Rows.Count = 0 Then ' if no record WAS found then
            Return Nothing
            Exit Function
        Else
            Dim tpdal As New TimePuncheDAL()
            tp = tpdal.getTimePuncheByID(dt.Rows(0).Item(0).ToString)
        End If
        Return tp
    End Function

    'create new timepunche EmployeeID, DepartmentID, DateWorked, IsClosed, ID, LocationID
    Public Function CreateTimeCard(ByVal tp As TimePunche, ByVal jobdescid As String, Optional ByVal isHourly As Boolean = True) As String
        Dim newTPid As String = insertTimePunche(tp)
        Return newTPid
    End Function


    Public Function TimeOn(ByVal eid As String) As String
        Dim retstr As String = String.Empty
        Dim a As New SqlCeAdapter("TimeClock.sdf")
        Dim strSQL As String = "SELECT TimeInOut.TimeIn" & _
        " FROM Employee INNER JOIN" & _
        " TimePunche ON Employee.ID = TimePunche.EmployeeID INNER JOIN" & _
        " TimeInOut ON TimePunche.ID = TimeInOut.TimepuncheID" & _
        " WHERE (TimeInOut.TimeOut= '1/1/1900 12:00:00 AM' OR TimeInOut.TimeOut IS NULL) AND (Employee.ID = '" & eid & "')"
        Try
            retstr = a.QueryDatabaseForScalar(strSQL).ToString
        Catch ex As Exception
            retstr = ex.Message
        End Try
        Dim mat As String = String.Empty
        Return retstr
    End Function
End Class
