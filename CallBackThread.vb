

Public Delegate Sub CallBackDelegate(ByVal status As String)
Public Delegate Sub ThreadFunctionDelegate()

Public Class CallBackThread
    Implements IDisposable
    Private m_BaseControl As Control 'the form/control that implements the call back function
    Private m_ThreadFunction As ThreadFunctionDelegate 'The pointer to the function that implements the thread logic
    Private m_CallBackFunction As CallBackDelegate
    Private m_Thread As System.Threading.Thread 'internal thread object
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private m_startedValue As Boolean = False 'internal flag to detect redundant calls

    Public Sub New(ByRef caller As Control, _
                   ByRef threadMethod As ThreadFunctionDelegate, _
                   ByRef callbackFunction As CallBackDelegate)
        m_BaseControl = caller
        m_ThreadFunction = threadMethod
        m_CallBackFunction = callbackFunction
        m_Thread = New Threading.Thread(AddressOf ThreadFunction)
    End Sub

    Public Sub Start()
        If Not m_startedValue Then
            m_Thread.Start()
            m_startedValue = True
        Else
            Throw New ApplicationException("Thread already started")
        End If
    End Sub
    Private Sub ThreadFunction()
        m_ThreadFunction.Invoke()
        m_startedValue = False
    End Sub
    Public Sub UpdateUI(ByVal msg As String)
        If m_BaseControl IsNot Nothing AndAlso m_CallBackFunction IsNot Nothing Then
            m_BaseControl.Invoke(m_CallBackFunction, New Object() {msg})
        End If
    End Sub



    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class



' Simple threading scenario:  Start a Shared method running
' on a second thread.

'Public Class ChadarThread



'    Public syncnum As Integer = 0
'    ' The ThreadProc method is called when the thread starts.
'    ' It loops ten times, writing to the console and yielding 
'    ' the rest of its time slice each time, and then ends.

'    Public Shared Sub ThreadProc()
'        Dim utl As New Utils
'        Dim iInterval As Integer = 20000 '60secs
'        Dim sint As String = utl.getxmldata("Sync_Interval")
'        While True
'            ' Yield the rest of the time slice.
'            Thread.Sleep(iInterval)
'            Dim go As Boolean = Utils.Connected
'            If go Then
'                Form1.con = True

'                Form1.iscon()
'                '                asyncConn.syncme()
'            Else
'                Dim clr As String = Form1.CtrlConn1.Text
'                Form1.con = False
'                Form1.iscon()
'                Dim n As Boolean = go
'            End If
'        End While
'    End Sub
'    Public Shared Sub ThreadMain()
'        ' The constructor for the Thread class requires a ThreadStart 
'        ' delegate.  The Visual Basic AddressOf operator creates this
'        ' delegate for you.
'        Dim t As New Thread(AddressOf ThreadProc)
'        ' Start ThreadProc.  On a uniprocessor, the thread does not get 
'        ' any processor time until the main thread yields.  Uncomment 
'        ' the Thread.Sleep that follows t.Start() to see the difference.
'        t.Start()
'        'Thread.Sleep(0)
'    End Sub
'End Class
