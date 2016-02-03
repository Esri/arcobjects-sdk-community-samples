Imports ESRI.ArcGIS
Public Class PlayAnimation

    <STAThread()> _
    Shared Sub Main()
        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.ArcReader) Then
            If Not RuntimeManager.Bind(ProductCode.EngineOrDesktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If

        Application.Run(New PlayAnimation())
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        'Open a file dialog for selecting map documents
        OpenFileDialog1.Title = "Select Published Map Document"
        OpenFileDialog1.Filter = "Published Map Documents (*.pmf)|*.pmf"
        OpenFileDialog1.ShowDialog()

        'Exit if no map document is selected
        Dim sFilePath As String
        sFilePath = OpenFileDialog1.FileName
        If sFilePath = "" Then Exit Sub

        'Load the specified pmf
        If AxArcReaderGlobeControl1.CheckDocument(sFilePath) = True Then
            AxArcReaderGlobeControl1.LoadDocument(sFilePath)
        Else
            MsgBox("This document cannot be loaded!")
            Exit Sub
        End If

        If AxArcReaderGlobeControl1.ARGlobe.AnimationCount <> 0 Then
            'Enable Controls
            enableCommands(True)

            'Populate combo with animations, clearing any existing animations listed previously
            cboAnimations.Items.Clear()
            Dim i As Integer = 0

            Do Until i = AxArcReaderGlobeControl1.ARGlobe.AnimationCount - 1
                cboAnimations.Items.Add(AxArcReaderGlobeControl1.ARGlobe.AnimationName(i))
                cboAnimations.SelectedIndex = 0
                i = i + 1
            Loop
        Else
            System.Windows.Forms.MessageBox.Show("This sample requires you load a PMF that contains animations")
            'Disable Controls
            enableCommands(False)
        End If

    End Sub

    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlay.Click

        'Play animation showing or hiding window depending if checkbox is checked
        AxArcReaderGlobeControl1.ARGlobe.PlayAnimation(AxArcReaderGlobeControl1.ARGlobe.AnimationName(cboAnimations.SelectedIndex))

    End Sub

    Private Sub chkShowWindow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowWindow.CheckedChanged

        'Show or hide window if checkbox is changed
        AxArcReaderGlobeControl1.ShowARGlobeWindow(ESRI.ArcGIS.PublisherControls.esriARGlobeWindows.esriARGlobeWindowsAnimation, chkShowWindow.Checked, AxArcReaderGlobeControl1.ARGlobe.AnimationName(cboAnimations.SelectedIndex))

    End Sub

    Private Sub cboAnimations_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAnimations.SelectedIndexChanged

        'Updates the combo box of the animation window if its present
        AxArcReaderGlobeControl1.ShowARGlobeWindow(ESRI.ArcGIS.PublisherControls.esriARGlobeWindows.esriARGlobeWindowsAnimation, chkShowWindow.Checked, AxArcReaderGlobeControl1.ARGlobe.AnimationName(cboAnimations.SelectedIndex))

    End Sub

    Private Sub PlayAnimation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Disable commands
        enableCommands(False)
    End Sub

    Private Sub enableCommands(ByVal enable As Boolean)
        cboAnimations.Enabled = enable
        chkShowWindow.Enabled = enable
        btnPlay.Enabled = enable
    End Sub

End Class
