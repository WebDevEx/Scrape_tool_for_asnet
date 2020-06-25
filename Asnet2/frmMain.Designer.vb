<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tt1 = New System.Windows.Forms.ToolStrip()
        Me.CmdStart = New System.Windows.Forms.ToolStripButton()
        Me.StopButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveButton = New System.Windows.Forms.ToolStripButton()
        Me.RefreshButton = New System.Windows.Forms.ToolStripButton()
        Me.LoadingButton = New System.Windows.Forms.ToolStripButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatusBar = New System.Windows.Forms.ToolStripStatusLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Tb1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.AxWebBrowser1 = New AxSHDocVw.AxWebBrowser()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.tt1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Tb1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.AxWebBrowser1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tt1
        '
        Me.tt1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CmdStart, Me.StopButton, Me.SaveButton, Me.RefreshButton, Me.LoadingButton})
        Me.tt1.Location = New System.Drawing.Point(0, 0)
        Me.tt1.Name = "tt1"
        Me.tt1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.tt1.Size = New System.Drawing.Size(1289, 89)
        Me.tt1.TabIndex = 32
        Me.tt1.Text = "ToolStrip1"
        '
        'CmdStart
        '
        Me.CmdStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CmdStart.Image = CType(resources.GetObject("CmdStart.Image"), System.Drawing.Image)
        Me.CmdStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.CmdStart.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CmdStart.Name = "CmdStart"
        Me.CmdStart.Size = New System.Drawing.Size(84, 86)
        Me.CmdStart.Text = "Start"
        Me.CmdStart.ToolTipText = "Start" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'StopButton
        '
        Me.StopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.StopButton.Image = CType(resources.GetObject("StopButton.Image"), System.Drawing.Image)
        Me.StopButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.StopButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.StopButton.Name = "StopButton"
        Me.StopButton.Size = New System.Drawing.Size(84, 86)
        Me.StopButton.Text = "ToolStripButton2"
        Me.StopButton.ToolTipText = "Stop"
        Me.StopButton.Visible = False
        '
        'SaveButton
        '
        Me.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveButton.Image = CType(resources.GetObject("SaveButton.Image"), System.Drawing.Image)
        Me.SaveButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(84, 86)
        Me.SaveButton.Text = "ToolStripButton3"
        Me.SaveButton.ToolTipText = "Save"
        '
        'RefreshButton
        '
        Me.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RefreshButton.Image = CType(resources.GetObject("RefreshButton.Image"), System.Drawing.Image)
        Me.RefreshButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(84, 86)
        Me.RefreshButton.Text = "ToolStripButton4"
        Me.RefreshButton.ToolTipText = "Restart"
        '
        'LoadingButton
        '
        Me.LoadingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.LoadingButton.Image = CType(resources.GetObject("LoadingButton.Image"), System.Drawing.Image)
        Me.LoadingButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.LoadingButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.LoadingButton.Name = "LoadingButton"
        Me.LoadingButton.Size = New System.Drawing.Size(74, 86)
        Me.LoadingButton.Text = "ToolStripButton5"
        Me.LoadingButton.ToolTipText = "In Progress"
        Me.LoadingButton.Visible = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatusBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 636)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1289, 22)
        Me.StatusStrip1.TabIndex = 33
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatusBar
        '
        Me.lblStatusBar.ForeColor = System.Drawing.Color.Red
        Me.lblStatusBar.Name = "lblStatusBar"
        Me.lblStatusBar.Size = New System.Drawing.Size(48, 17)
        Me.lblStatusBar.Text = "Ready..."
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Tb1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 89)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1289, 547)
        Me.GroupBox1.TabIndex = 34
        Me.GroupBox1.TabStop = False
        '
        'Tb1
        '
        Me.Tb1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tb1.Controls.Add(Me.TabPage1)
        Me.Tb1.Controls.Add(Me.TabPage2)
        Me.Tb1.Location = New System.Drawing.Point(0, 19)
        Me.Tb1.Name = "Tb1"
        Me.Tb1.Padding = New System.Drawing.Point(2, 3)
        Me.Tb1.SelectedIndex = 0
        Me.Tb1.Size = New System.Drawing.Size(1289, 522)
        Me.Tb1.TabIndex = 33
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.AxWebBrowser1)
        Me.TabPage1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1281, 496)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "   Main   "
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'AxWebBrowser1
        '
        Me.AxWebBrowser1.AllowDrop = True
        Me.AxWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxWebBrowser1.Enabled = True
        Me.AxWebBrowser1.Location = New System.Drawing.Point(3, 3)
        Me.AxWebBrowser1.OcxState = CType(resources.GetObject("AxWebBrowser1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxWebBrowser1.Size = New System.Drawing.Size(1275, 490)
        Me.AxWebBrowser1.TabIndex = 37
        Me.AxWebBrowser1.UseWaitCursor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DataGridView1)
        Me.TabPage2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1281, 496)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "   Result   "
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1275, 490)
        Me.DataGridView1.TabIndex = 0
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1289, 658)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.tt1)
        Me.IsMdiContainer = True
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Scrapper For Asnet2.com........"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tt1.ResumeLayout(False)
        Me.tt1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.Tb1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.AxWebBrowser1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tt1 As ToolStrip
    Friend WithEvents CmdStart As ToolStripButton
    Friend WithEvents StopButton As ToolStripButton
    Friend WithEvents SaveButton As ToolStripButton
    Friend WithEvents RefreshButton As ToolStripButton
    Friend WithEvents LoadingButton As ToolStripButton
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatusBar As ToolStripStatusLabel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Tb1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents AxWebBrowser1 As AxSHDocVw.AxWebBrowser
    Friend WithEvents DataGridView1 As DataGridView
End Class
