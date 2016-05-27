Public Class Library_Browser
    Private Sub Library_Browser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getInstalledLibs()
    End Sub

    Private Sub getInstalledLibs()
        For Each item As String In IO.Directory.GetFiles("library\", "*.jmlib")
            installedLibBox.Items.Add(item)
        Next
    End Sub
End Class