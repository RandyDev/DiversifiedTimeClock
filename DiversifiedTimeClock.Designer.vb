<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiversifiedTimeClock
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DiversifiedTimeClock))
        Me.TimerClock = New System.Windows.Forms.Timer(Me.components)
        Me.lblTime = New Telerik.WinControls.UI.RadLabel()
        Me.lblpassworderr = New Telerik.WinControls.UI.RadLabel()
        Me.lblPrevtx = New Telerik.WinControls.UI.RadLabel()
        Me.labelLastSyncValue = New Telerik.WinControls.UI.RadLabel()
        Me.lcTimeCards = New Telerik.WinControls.UI.RadListControl()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.lblTimeCard = New Telerik.WinControls.UI.RadLabel()
        Me.lblCompanyAndLocation = New Telerik.WinControls.UI.RadLabel()
        Me.lblVersion = New Telerik.WinControls.UI.RadLabel()
        Me.lblDiversified = New Telerik.WinControls.UI.RadLabel()
        Me.lblTimeClock = New Telerik.WinControls.UI.RadLabel()
        Me.pnlTimeCard = New Telerik.WinControls.UI.RadPanel()
        Me.btnClockOUTonBreak = New Telerik.WinControls.UI.RadButton()
        Me.cbJobDescription = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblToday = New Telerik.WinControls.UI.RadLabel()
        Me.btnClockOUTgoHOME = New Telerik.WinControls.UI.RadButton()
        Me.txtPassword = New Telerik.WinControls.UI.RadTextBox()
        Me.cbDepartment = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblPassword = New Telerik.WinControls.UI.RadLabel()
        Me.lblDepartment = New Telerik.WinControls.UI.RadLabel()
        Me.btnClockIN = New Telerik.WinControls.UI.RadButton()
        Me.lblJobDescription = New Telerik.WinControls.UI.RadLabel()
        Me.lblEmployeeName = New Telerik.WinControls.UI.RadLabel()
        Me.TimerSync = New System.Windows.Forms.Timer(Me.components)
        Me.btnHibernate = New Telerik.WinControls.UI.RadButton()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.lblTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpassworderr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPrevtx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.labelLastSyncValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcTimeCards, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTimeCard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCompanyAndLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVersion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiversified, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTimeClock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlTimeCard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTimeCard.SuspendLayout()
        CType(Me.btnClockOUTonBreak, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbJobDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClockOUTgoHOME, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClockIN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJobDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHibernate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TimerClock
        '
        Me.TimerClock.Enabled = True
        Me.TimerClock.Interval = 10000
        '
        'lblTime
        '
        Me.lblTime.BackColor = System.Drawing.Color.Transparent
        Me.lblTime.Font = New System.Drawing.Font("Segoe UI", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTime.Location = New System.Drawing.Point(449, 85)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(187, 56)
        Me.lblTime.TabIndex = 1120
        Me.lblTime.Text = "00:00 AM"
        Me.lblTime.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblpassworderr
        '
        Me.lblpassworderr.BackColor = System.Drawing.Color.Transparent
        Me.lblpassworderr.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpassworderr.ForeColor = System.Drawing.Color.Red
        Me.lblpassworderr.Location = New System.Drawing.Point(160, 170)
        Me.lblpassworderr.Name = "lblpassworderr"
        Me.lblpassworderr.Size = New System.Drawing.Size(117, 19)
        Me.lblpassworderr.TabIndex = 1116
        Me.lblpassworderr.Text = "Incorrect Password"
        Me.lblpassworderr.Visible = False
        '
        'lblPrevtx
        '
        Me.lblPrevtx.AutoSize = False
        Me.lblPrevtx.BackColor = System.Drawing.Color.Transparent
        Me.lblPrevtx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.lblPrevtx.CausesValidation = False
        Me.lblPrevtx.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrevtx.Location = New System.Drawing.Point(449, 136)
        Me.lblPrevtx.Name = "lblPrevtx"
        Me.lblPrevtx.Size = New System.Drawing.Size(363, 24)
        Me.lblPrevtx.TabIndex = 1124
        '
        'labelLastSyncValue
        '
        Me.labelLastSyncValue.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.labelLastSyncValue.Location = New System.Drawing.Point(629, 12)
        Me.labelLastSyncValue.Name = "labelLastSyncValue"
        '
        '
        '
        Me.labelLastSyncValue.RootElement.AccessibleDescription = "sdfs"
        Me.labelLastSyncValue.RootElement.AccessibleName = "sdfs"
        Me.labelLastSyncValue.ShowItemToolTips = False
        Me.labelLastSyncValue.Size = New System.Drawing.Size(85, 18)
        Me.labelLastSyncValue.TabIndex = 1123
        Me.labelLastSyncValue.Tag = ""
        Me.labelLastSyncValue.Text = "Last Sync: 00:00"
        Me.ToolTip.SetToolTip(Me.labelLastSyncValue, "Click to Sync/Refresh")
        '
        'lcTimeCards
        '
        Me.lcTimeCards.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lcTimeCards.ItemHeight = 24
        Me.lcTimeCards.Location = New System.Drawing.Point(29, 130)
        Me.lcTimeCards.Name = "lcTimeCards"
        Me.lcTimeCards.Size = New System.Drawing.Size(305, 458)
        Me.lcTimeCards.TabIndex = 1126
        Me.lcTimeCards.Text = "RadListControl1"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(349, 462)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(129, 34)
        Me.btnCancel.TabIndex = 1127
        Me.btnCancel.TabStop = False
        Me.btnCancel.Text = "CANCEL"
        '
        'lblTimeCard
        '
        Me.lblTimeCard.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimeCard.Location = New System.Drawing.Point(85, 98)
        Me.lblTimeCard.Name = "lblTimeCard"
        Me.lblTimeCard.Size = New System.Drawing.Size(122, 33)
        Me.lblTimeCard.TabIndex = 1117
        Me.lblTimeCard.Text = "Time Cards"
        '
        'lblCompanyAndLocation
        '
        Me.lblCompanyAndLocation.AutoSize = False
        Me.lblCompanyAndLocation.Font = New System.Drawing.Font("Segoe UI", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyAndLocation.ImageAlignment = System.Drawing.ContentAlignment.TopRight
        Me.lblCompanyAndLocation.Location = New System.Drawing.Point(29, 69)
        Me.lblCompanyAndLocation.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCompanyAndLocation.Name = "lblCompanyAndLocation"
        Me.lblCompanyAndLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCompanyAndLocation.Size = New System.Drawing.Size(305, 21)
        Me.lblCompanyAndLocation.TabIndex = 1115
        Me.lblCompanyAndLocation.Text = "Southeast Unloading : Jax"
        Me.lblCompanyAndLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCompanyAndLocation.TextWrap = False
        '
        'lblVersion
        '
        Me.lblVersion.Location = New System.Drawing.Point(108, 13)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(46, 18)
        Me.lblVersion.TabIndex = 1114
        Me.lblVersion.Text = "v 1.0.0.0"
        '
        'lblDiversified
        '
        Me.lblDiversified.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiversified.Location = New System.Drawing.Point(29, 12)
        Me.lblDiversified.Name = "lblDiversified"
        Me.lblDiversified.Size = New System.Drawing.Size(69, 19)
        Me.lblDiversified.TabIndex = 1112
        Me.lblDiversified.Text = "Diversified"
        '
        'lblTimeClock
        '
        Me.lblTimeClock.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimeClock.Location = New System.Drawing.Point(27, 22)
        Me.lblTimeClock.Name = "lblTimeClock"
        Me.lblTimeClock.Size = New System.Drawing.Size(155, 41)
        Me.lblTimeClock.TabIndex = 1113
        Me.lblTimeClock.Text = "Time Clock"
        '
        'pnlTimeCard
        '
        Me.pnlTimeCard.Controls.Add(Me.btnClockOUTonBreak)
        Me.pnlTimeCard.Controls.Add(Me.cbJobDescription)
        Me.pnlTimeCard.Controls.Add(Me.lblToday)
        Me.pnlTimeCard.Controls.Add(Me.btnClockOUTgoHOME)
        Me.pnlTimeCard.Controls.Add(Me.txtPassword)
        Me.pnlTimeCard.Controls.Add(Me.cbDepartment)
        Me.pnlTimeCard.Controls.Add(Me.lblPassword)
        Me.pnlTimeCard.Controls.Add(Me.lblDepartment)
        Me.pnlTimeCard.Controls.Add(Me.btnClockIN)
        Me.pnlTimeCard.Controls.Add(Me.lblJobDescription)
        Me.pnlTimeCard.Controls.Add(Me.lblpassworderr)
        Me.pnlTimeCard.Location = New System.Drawing.Point(349, 184)
        Me.pnlTimeCard.Name = "pnlTimeCard"
        Me.pnlTimeCard.Size = New System.Drawing.Size(463, 242)
        Me.pnlTimeCard.TabIndex = 1122
        Me.pnlTimeCard.Visible = False
        '
        'btnClockOUTonBreak
        '
        Me.btnClockOUTonBreak.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClockOUTonBreak.Location = New System.Drawing.Point(23, 129)
        Me.btnClockOUTonBreak.Name = "btnClockOUTonBreak"
        Me.btnClockOUTonBreak.Size = New System.Drawing.Size(228, 34)
        Me.btnClockOUTonBreak.TabIndex = 1111
        Me.btnClockOUTonBreak.TabStop = False
        Me.btnClockOUTonBreak.Text = "Clock OUT on Break"
        '
        'cbJobDescription
        '
        Me.cbJobDescription.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbJobDescription.DropDownMaxSize = New System.Drawing.Size(0, 300)
        Me.cbJobDescription.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cbJobDescription.EnableAlternatingItemColor = True
        Me.cbJobDescription.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbJobDescription.Location = New System.Drawing.Point(22, 77)
        Me.cbJobDescription.Name = "cbJobDescription"
        Me.cbJobDescription.Size = New System.Drawing.Size(265, 32)
        Me.cbJobDescription.TabIndex = 1117
        Me.cbJobDescription.Text = " "
        '
        'lblToday
        '
        Me.lblToday.AutoSize = False
        Me.lblToday.CausesValidation = False
        Me.lblToday.EnableTheming = False
        Me.lblToday.Font = New System.Drawing.Font("Segoe UI Light", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToday.Location = New System.Drawing.Point(256, 3)
        Me.lblToday.Name = "lblToday"
        Me.lblToday.Size = New System.Drawing.Size(204, 130)
        Me.lblToday.TabIndex = 2
        Me.lblToday.Text = "         Enjoy your Day," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    Play nice with others" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "         and BE SAFE!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " "
        Me.lblToday.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'btnClockOUTgoHOME
        '
        Me.btnClockOUTgoHOME.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClockOUTgoHOME.Location = New System.Drawing.Point(148, 195)
        Me.btnClockOUTgoHOME.Name = "btnClockOUTgoHOME"
        Me.btnClockOUTgoHOME.Size = New System.Drawing.Size(240, 34)
        Me.btnClockOUTgoHOME.TabIndex = 1111
        Me.btnClockOUTgoHOME.Text = "Clock OUT Go Home"
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 13.25!)
        Me.txtPassword.Location = New System.Drawing.Point(20, 23)
        Me.txtPassword.MaxLength = 4
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(52, 29)
        Me.txtPassword.TabIndex = 1
        Me.txtPassword.Text = "0000"
        '
        'cbDepartment
        '
        Me.cbDepartment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbDepartment.DropDownMaxSize = New System.Drawing.Size(0, 300)
        Me.cbDepartment.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cbDepartment.EnableAlternatingItemColor = True
        Me.cbDepartment.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDepartment.Location = New System.Drawing.Point(22, 131)
        Me.cbDepartment.Name = "cbDepartment"
        Me.cbDepartment.Size = New System.Drawing.Size(217, 32)
        Me.cbDepartment.TabIndex = 2
        Me.cbDepartment.Text = " "
        '
        'lblPassword
        '
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.Location = New System.Drawing.Point(10, 6)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(62, 19)
        Me.lblPassword.TabIndex = 1
        Me.lblPassword.Text = "Password"
        '
        'lblDepartment
        '
        Me.lblDepartment.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(16, 114)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(77, 19)
        Me.lblDepartment.TabIndex = 2
        Me.lblDepartment.Text = "Department"
        '
        'btnClockIN
        '
        Me.btnClockIN.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClockIN.Location = New System.Drawing.Point(16, 195)
        Me.btnClockIN.Name = "btnClockIN"
        Me.btnClockIN.Size = New System.Drawing.Size(129, 34)
        Me.btnClockIN.TabIndex = 1111
        Me.btnClockIN.TabStop = False
        Me.btnClockIN.Text = "Clock IN"
        '
        'lblJobDescription
        '
        Me.lblJobDescription.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJobDescription.Location = New System.Drawing.Point(10, 59)
        Me.lblJobDescription.Name = "lblJobDescription"
        Me.lblJobDescription.Size = New System.Drawing.Size(97, 19)
        Me.lblJobDescription.TabIndex = 2
        Me.lblJobDescription.Text = "Job Description"
        '
        'lblEmployeeName
        '
        Me.lblEmployeeName.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeName.Location = New System.Drawing.Point(349, 153)
        Me.lblEmployeeName.Name = "lblEmployeeName"
        Me.lblEmployeeName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmployeeName.Size = New System.Drawing.Size(276, 37)
        Me.lblEmployeeName.TabIndex = 1119
        Me.lblEmployeeName.Text = "<--  SELECT Time Card "
        '
        'TimerSync
        '
        Me.TimerSync.Interval = 30000
        '
        'btnHibernate
        '
        Me.btnHibernate.AutoSize = True
        Me.btnHibernate.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHibernate.Location = New System.Drawing.Point(680, 459)
        Me.btnHibernate.Name = "btnHibernate"
        Me.btnHibernate.Size = New System.Drawing.Size(132, 37)
        Me.btnHibernate.TabIndex = 1128
        Me.btnHibernate.TabStop = False
        Me.btnHibernate.Text = "Pause Sync"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(183, 22)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(151, 45)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1129
        Me.PictureBox1.TabStop = False
        '
        'DiversifiedTimeClock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(831, 601)
        Me.Controls.Add(Me.lblCompanyAndLocation)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnHibernate)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.lblPrevtx)
        Me.Controls.Add(Me.labelLastSyncValue)
        Me.Controls.Add(Me.lcTimeCards)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblTimeCard)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblDiversified)
        Me.Controls.Add(Me.lblTimeClock)
        Me.Controls.Add(Me.pnlTimeCard)
        Me.Controls.Add(Me.lblEmployeeName)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DiversifiedTimeClock"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DiversifiedTimeClock"
        CType(Me.lblTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpassworderr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPrevtx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.labelLastSyncValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcTimeCards, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTimeCard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCompanyAndLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVersion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiversified, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTimeClock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlTimeCard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTimeCard.ResumeLayout(False)
        Me.pnlTimeCard.PerformLayout()
        CType(Me.btnClockOUTonBreak, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbJobDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClockOUTgoHOME, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClockIN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJobDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHibernate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TimerClock As System.Windows.Forms.Timer
    Friend WithEvents lblTime As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblpassworderr As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblPrevtx As Telerik.WinControls.UI.RadLabel
    Friend WithEvents labelLastSyncValue As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lcTimeCards As Telerik.WinControls.UI.RadListControl
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblTimeCard As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblCompanyAndLocation As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblVersion As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblDiversified As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblTimeClock As Telerik.WinControls.UI.RadLabel
    Friend WithEvents pnlTimeCard As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnClockOUTonBreak As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblToday As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnClockOUTgoHOME As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtPassword As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents cbDepartment As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lblDepartment As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblPassword As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnClockIN As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblJobDescription As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblEmployeeName As Telerik.WinControls.UI.RadLabel
    Friend WithEvents TimerSync As System.Windows.Forms.Timer
    Friend WithEvents btnHibernate As Telerik.WinControls.UI.RadButton
    Friend WithEvents cbJobDescription As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
