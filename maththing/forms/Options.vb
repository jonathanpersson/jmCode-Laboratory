Public Class Options
    Private Sub Options_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadCurrent()
    End Sub

    Private Sub loadCurrent()
        appBGHex.Text = getHexFromColor(Syntax.uiColor)
        appFGHex.Text = getHexFromColor(Syntax.fgColor)
        cmdHexTB.Text = getHexFromColor(Syntax.commandColor)
        funcHexTB.Text = getHexFromColor(Syntax.functionColor)
        svHexTB.Text = getHexFromColor(Syntax.svColor)
        cvHexTB.Text = getHexFromColor(Syntax.classVarColor)
        bgHexTB.Text = getHexFromColor(Syntax.bgColor)
    End Sub

    Private Function getColorFromHex(ByVal s As String)
        s = s.Replace("#", "")
        Dim R As Integer = Convert.ToInt32(s.Substring(0, s.Length - 4), 16) : s = s.Remove(0, 2)
        Dim G As Integer = Convert.ToInt32(s.Substring(0, s.Length - 2), 16) : s = s.Remove(0, 2)
        Dim B As Integer = Convert.ToInt32(s.Substring(0, s.Length), 16)
        Return Color.FromArgb(255, R, G, B)
    End Function

    Private Function getHexFromColor(ByVal c As Color)
        Dim R As String = Hex(c.R) : If R.Length = 1 Then R = 0 & R
        Dim G As String = Hex(c.G) : If G.Length = 1 Then G = 0 & G
        Dim B As String = Hex(c.B) : If B.Length = 1 Then B = 0 & B

        Return "#" & R & G & B
    End Function

    Private Function chooseColor()
        Dim colorPicker As New ColorDialog
        If colorPicker.ShowDialog = DialogResult.OK Then
            Return Color.FromArgb(255, colorPicker.Color.R, colorPicker.Color.G, colorPicker.Color.B)
        End If
    End Function

    Private Sub highlightSyntax()
        If codeRTB.Text.Length > 0 Then

            If bgColorPrev.BackColor = Color.Transparent = False Then
                codeRTB.BackColor = bgColorPrev.BackColor
            End If

            Dim selectStart As Integer = codeRTB.SelectionStart
            codeRTB.Select(0, codeRTB.TextLength)
            codeRTB.SelectionColor = fgPrevLabel.ForeColor
            codeRTB.DeselectAll()

            'Keywords
            For Each word As String In Syntax.commandList
                Dim pos As Integer = 0
                Do While codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                    pos = codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos)
                    codeRTB.Select(pos, word.Length)
                    codeRTB.SelectionColor = cmdColorPrev.BackColor
                    pos += 1
                Loop
            Next

            'Functions
            For Each word As String In Syntax.functionList
                Dim pos As Integer = 0
                Do While codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                    pos = codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos)
                    codeRTB.Select(pos, word.Length)
                    codeRTB.SelectionColor = funcColorPrev.BackColor
                    pos += 1
                Loop
            Next

            'Special variables
            For Each word As String In Variables.svList.Keys
                Dim pos As Integer = 0
                Do While codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                    pos = codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos)
                    codeRTB.Select(pos, word.Length)
                    codeRTB.SelectionColor = svColorPrev.BackColor
                    pos += 1
                Loop
            Next

            'Class variables
            For Each word As String In Syntax.classVar
                Dim pos As Integer = 0
                Do While codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                    pos = codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos)
                    codeRTB.Select(pos, word.Length)
                    codeRTB.SelectionColor = cvColorPrev.BackColor
                    pos += 1
                Loop
            Next

            codeRTB.SelectionStart = selectStart
            codeRTB.DeselectAll()
        End If
    End Sub

    Private Sub appBGHex_TextChanged(sender As Object, e As EventArgs) Handles appBGHex.TextChanged
        If appBGHex.Text.Length = 7 Then bgPrevPanel.BackColor = getColorFromHex(appBGHex.Text)
    End Sub

    Private Sub appFGHex_TextChanged(sender As Object, e As EventArgs) Handles appFGHex.TextChanged
        If appFGHex.Text.Length = 7 Then fgPrevLabel.ForeColor = getColorFromHex(appFGHex.Text)
    End Sub

    Private Sub pickBGColorBtn_Click(sender As Object, e As EventArgs) Handles pickBGColorBtn.Click
        appBGHex.Text = getHexFromColor(chooseColor())
        bgPrevPanel.BackColor = getColorFromHex(appBGHex.Text)
    End Sub

    Private Sub pickFGColorBtn_Click(sender As Object, e As EventArgs) Handles pickFGColorBtn.Click
        appFGHex.Text = getHexFromColor(chooseColor())
        fgPrevLabel.ForeColor = getColorFromHex(appFGHex.Text)
    End Sub

    Private Sub cmdHexTB_TextChanged(sender As Object, e As EventArgs) Handles cmdHexTB.TextChanged
        If cmdHexTB.Text.Length = 7 Then cmdColorPrev.BackColor = getColorFromHex(cmdHexTB.Text)
        highlightSyntax()
    End Sub

    Private Sub funcHexTB_TextChanged(sender As Object, e As EventArgs) Handles funcHexTB.TextChanged
        If funcHexTB.Text.Length = 7 Then funcColorPrev.BackColor = getColorFromHex(funcHexTB.Text)
        highlightSyntax()
    End Sub

    Private Sub svHexTB_TextChanged(sender As Object, e As EventArgs) Handles svHexTB.TextChanged
        If svHexTB.Text.Length = 7 Then svColorPrev.BackColor = getColorFromHex(svHexTB.Text)
        highlightSyntax()
    End Sub

    Private Sub cvHexTB_TextChanged(sender As Object, e As EventArgs) Handles cvHexTB.TextChanged
        If cvHexTB.Text.Length = 7 Then cvColorPrev.BackColor = getColorFromHex(cvHexTB.Text)
        highlightSyntax()
    End Sub

    Private Sub bgHexTB_TextChanged(sender As Object, e As EventArgs) Handles bgHexTB.TextChanged
        If bgHexTB.Text.Length = 7 Then bgColorPrev.BackColor = getColorFromHex(bgHexTB.Text)
        highlightSyntax()
    End Sub

    Private Sub pickCMDColorBtn_Click(sender As Object, e As EventArgs) Handles pickCMDColorBtn.Click
        cmdHexTB.Text = getHexFromColor(chooseColor())
        cmdColorPrev.BackColor = getColorFromHex(cmdHexTB.Text)
        highlightSyntax()
    End Sub

    Private Sub pickFuncColorBtn_Click(sender As Object, e As EventArgs) Handles pickFuncColorBtn.Click
        funcHexTB.Text = getHexFromColor(chooseColor())
        funcColorPrev.BackColor = getColorFromHex(funcHexTB.Text)
        highlightSyntax()
    End Sub

    Private Sub pickSVColorBtn_Click(sender As Object, e As EventArgs) Handles pickSVColorBtn.Click
        svHexTB.Text = getHexFromColor(chooseColor())
        svColorPrev.BackColor = getColorFromHex(svHexTB.Text)
        highlightSyntax()
    End Sub

    Private Sub pickCVColorBtn_Click(sender As Object, e As EventArgs) Handles pickCVColorBtn.Click
        cvHexTB.Text = getHexFromColor(chooseColor())
        cvColorPrev.BackColor = getColorFromHex(cvHexTB.Text)
        highlightSyntax()
    End Sub

    Private Sub pickTBBGColorBtn_Click(sender As Object, e As EventArgs) Handles pickTBBGColorBtn.Click
        bgHexTB.Text = getHexFromColor(chooseColor())
        bgColorPrev.BackColor = getColorFromHex(bgHexTB.Text)
        highlightSyntax()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Syntax.uiColor = getColorFromHex(appBGHex.Text)
        Syntax.fgColor = getColorFromHex(appFGHex.Text)
        Syntax.bgColor = getColorFromHex(bgHexTB.Text)
        Syntax.commandColor = getColorFromHex(cmdHexTB.Text)
        Syntax.functionColor = getColorFromHex(funcHexTB.Text)
        Syntax.svColor = getColorFromHex(svHexTB.Text)
        Syntax.classVarColor = getColorFromHex(cvHexTB.Text)
        CodePad.setTheme()
        Close()
    End Sub
End Class