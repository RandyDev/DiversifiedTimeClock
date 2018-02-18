<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class asyncConn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(asyncConn))
        Me.labelLastSyncValue = New System.Windows.Forms.Label()
        Me.lblNetworkStatus = New System.Windows.Forms.Label()
        Me.labelStatusValue = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'labelLastSyncValue
        '
        Me.labelLastSyncValue.Location = New System.Drawing.Point(28, 167)
        Me.labelLastSyncValue.Name = "labelLastSyncValue"
        Me.labelLastSyncValue.Size = New System.Drawing.Size(237, 20)
        Me.labelLastSyncValue.TabIndex = 10
        '
        'lblNetworkStatus
        '
        Me.lblNetworkStatus.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblNetworkStatus.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblNetworkStatus.Location = New System.Drawing.Point(84, -40)
        Me.lblNetworkStatus.Name = "lblNetworkStatus"
        Me.lblNetworkStatus.Size = New System.Drawing.Size(150, 20)
        Me.lblNetworkStatus.TabIndex = 12
        Me.lblNetworkStatus.Text = "Network Status"
        Me.lblNetworkStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'labelStatusValue
        '
        Me.labelStatusValue.Location = New System.Drawing.Point(30, 90)
        Me.labelStatusValue.Name = "labelStatusValue"
        Me.labelStatusValue.Size = New System.Drawing.Size(234, 46)
        Me.labelStatusValue.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(29, 149)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 18)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Last Sync:"
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(41, 54)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(223, 33)
        Me.RadLabel1.TabIndex = 15
        Me.RadLabel1.Text = "One moment please ..."
        '
        'asyncConn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(321, 147)
        Me.Controls.Add(Me.RadLabel1)
        Me.Controls.Add(Me.labelLastSyncValue)
        Me.Controls.Add(Me.lblNetworkStatus)
        Me.Controls.Add(Me.labelStatusValue)
        Me.Controls.Add(Me.Label2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "asyncConn"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sync/Refresh"
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents labelLastSyncValue As System.Windows.Forms.Label
    Friend WithEvents lblNetworkStatus As System.Windows.Forms.Label
    Friend WithEvents labelStatusValue As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
End Class

