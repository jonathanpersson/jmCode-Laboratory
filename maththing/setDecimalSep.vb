Public Class setDecimalSep
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.decimalSeparator = TextBox1.Text
        If CheckBox1.Checked = False Then
            My.Settings.askOnStart = False
        End If
        Close()
    End Sub

    Private Sub setDecimalSep_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.decimalSeparator
    End Sub
End Class