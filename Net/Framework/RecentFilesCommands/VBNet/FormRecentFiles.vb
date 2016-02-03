Public Class FormRecentFiles
    Public Sub PopulateFileList(ByVal files As String())
        lstFiles.Items.Clear()
        lstFiles.Items.AddRange(files)
    End Sub
End Class