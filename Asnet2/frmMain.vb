Imports System.IO
Imports System.Text.RegularExpressions
Imports Scraper

Public Class frmMain

    'Sub setStatus(ByVal status As String)
    'Me.lblStatusBar.Text = status
    'End Sub

    Private document As mshtml.HTMLDocument
    Private scraper As Scrape

    Private enumerator As IEnumerator

    Dim bindingSource As BindingSource = New BindingSource()

    Private Delegate Sub GridCallback()

    Private Delegate Sub UiCallback()



    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        'grdResults.DataSource = dtResults
        'ToolWorkingIndicator.Visible = True
        'ExpirySet()
        AxWebBrowser1.Navigate("https://www.asnet2.com/asnet/auth/login/")
        AxWebBrowser1.Silent = True
        setStatus("Go to www.asnet2.com...")
    End Sub

    Private Sub ExpirySet()
        Dim a = DateTime.Now()
        If (a.Day > 22) Then
            Application.Exit()
            MessageBox.Show("crashed")
        End If
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        setStatus("Downloading CSV and Image, please wait..")
        If scraper.details IsNot Nothing Then
            If scraper.details.Count > 0 Then
                Dim randomDate = DateTime.Now.ToString("yyyyMMddHHmmssfff")
                Dim filePath As String = Path.GetFullPath($"./Csv/Output_{randomDate}.csv")
                scraper.ExportToCSV(filePath)
                Dim ImagefilePath As String = Path.GetFullPath($"./DownloadedImages")
                scraper.ExportToImage(ImagefilePath)
                ShowMessage($"Your CSV has been exported successfully at this path {filePath} \r\n Your Images has been exporting at this path {ImagefilePath}\r\n Please Wait It will get finished soon")
            End If
        End If
        setStatus("CSV and Images downloaded")
    End Sub

    Private Sub RefreshButton_Click(sender As Object, e As EventArgs) Handles RefreshButton.Click
        Application.Restart()
    End Sub

    Private Sub CmdStart_Click(sender As Object, e As EventArgs) Handles CmdStart.Click
        setStatus("Scrapping the data...")
        document = AxWebBrowser1.Document

        If scraper Is Nothing Then
            scraper = New Scrape(document.cookie, document.body.innerHTML, Sub() PopulateDetailGrid(), Sub() DisableUI(), Sub() SetStatusUi())


            scraper.Start(True, 0)
            'Me.PopulateDetailGrid()

        Else
            scraper.Start(True, 0, document.body.innerHTML)
        End If

        CmdStart.Visible = False
        StopButton.Visible = True
        StopButton.Enabled = False
        LoadingButton.Visible = True
    End Sub

    Private Sub CmdStop_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        CmdStart.Visible = True
        StopButton.Visible = False
        StopButton.Enabled = False
    End Sub

    Public Sub ShowMessage(message As String)
        MessageBox.Show(message, "Please wait", MessageBoxButtons.OK)
    End Sub

    Public Sub DisableUI()
        Try
            Dim d As New UiCallback(AddressOf SetUi)
            Me.BeginInvoke(d, New Object() {})
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub


    Public Sub SetStatusUi()
        Try
            Dim d As New UiCallback(AddressOf UpdateUi)
            Me.BeginInvoke(d, New Object() {})
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

    Public Sub PopulateDetailGrid()
        Try
            If (DataGridView1.InvokeRequired) Then
                Dim d As New GridCallback(AddressOf SetGridData)
                Me.BeginInvoke(d, New Object() {})
            End If
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

    Private Sub SetGridData()
        Try
            bindingSource.DataSource = scraper.details.ToList()
            DataGridView1.DataSource = bindingSource
            DataGridView1.Columns("ACTUAL_PICTURE1_LINK").Visible = False
            DataGridView1.Columns("ACTUAL_PICTURE2_LINK").Visible = False
            DataGridView1.Columns("ACTUAL_PICTURE3_LINK").Visible = False
            DataGridView1.Columns("ACTUAL_PICTURE4_LINK").Visible = False
            DataGridView1.Columns("ACTUAL_PICTURE1_NAME").Visible = False
            DataGridView1.Columns("ACTUAL_PICTURE2_NAME").Visible = False
            DataGridView1.Columns("ACTUAL_PICTURE3_NAME").Visible = False
            DataGridView1.Columns("ACTUAL_PICTURE4_NAME").Visible = False
            bindingSource.ResetBindings(False)
            DataGridView1.Refresh()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try

    End Sub

    Private Sub SetUi()
        Try
            StopButton.Enabled = True
            LoadingButton.Visible = False
            ShowMessage("Scraping Has Been Completed...")
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

    Private Sub UpdateUi()
        Try
            'setStatus($"Fetching {scraper.currentRow} / {scraper.maxRow}")
            setStatus($"Fetched {scraper.details.Count}/{scraper.maxRowNum}")
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

End Class
