Public Class SaveFile

    Private Sub SaveFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Save()
        Try
            CodePad.codeRTB.SaveFile(saveDirTB.Text & "\" & projNameTB.Text & ComboBox1.SelectedItem,
                                     RichTextBoxStreamType.PlainText)
            Project.saveLoc = saveDirTB.Text & "\" & projNameTB.Text & ComboBox1.SelectedItem
        Catch

        End Try
    End Sub

    Private Sub openDir()
        Dim openFolder As New FolderBrowserDialog
        If openFolder.ShowDialog = DialogResult.OK Then
            saveDirTB.Text = openFolder.SelectedPath
        End If
    End Sub

    Private Sub getDirBtn_Click(sender As Object, e As EventArgs) Handles getDirBtn.Click
        openDir()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Save()
        CodePad.codeTab.Text = saveDirTB.Text & "\" & projNameTB.Text & ComboBox1.SelectedItem
        Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub
End Class