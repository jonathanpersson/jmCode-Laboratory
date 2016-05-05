Public Class CodePad

    'Variables
    Dim varList As New ArrayList
    Dim valList As New ArrayList

    'Functions
    Dim funcList As New ArrayList
    Dim funcArgList As New ArrayList

    'Classes
    Dim classList As New ArrayList

    Dim isExecuting As Boolean = False
    Dim waitLines As Integer = 0

    Private Sub CodePad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.askOnStart = True Then
            setDecimalSep.ShowDialog()
        End If
        loadSyntax()
        'setTheme()
        getCodeStats()
        highlightSyntax()
        stopLoopBtn.Visible = False
    End Sub

    Private Sub openScript()
        Try
            Dim openFile As New OpenFileDialog
            openFile.Filter = "jmCode Script Files *.jmcode | *.jmcode"
            If openFile.ShowDialog = DialogResult.OK Then
                codeRTB.Text = IO.File.ReadAllText(openFile.FileName)
                Project.saveLoc = openFile.FileName
                codeTab.Text = openFile.FileName
            End If
        Catch ex As Exception
            MsgBox("Something happened. (Dev note: add proper message later).")
        End Try
    End Sub

    Private Sub setTheme()
        BackColor = Syntax.uiColor
        MenuStrip1.BackColor = Syntax.uiColor
        ToolStrip1.BackColor = Syntax.uiColor
        codeRTB.BackColor = Syntax.bgColor
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles codeRTB.TextChanged
        highlightSyntax()
        getCodeStats()
    End Sub

    Private Sub getCodeStats()
        lineCountLabel.Text = "Lines: " & codeRTB.Lines.Count & " Characters: " & codeRTB.Text.Length
    End Sub

    Private Sub loadSyntax()
        'Make this load syntax from file later
        'Load Keywords
        For Each line As String In IO.File.ReadAllLines("lang\commands.txt")
            Syntax.commandList.Add(line)
        Next

        'Load Functions
        For Each line As String In IO.File.ReadAllLines("lang\functions.txt")
            Syntax.functionList.Add(line)
        Next

        'Load operators
        For Each character As Char In IO.File.ReadAllText("lang\operators.txt")
            Syntax.opList.Add(character)
        Next

        'Load special variables
        For Each line As String In IO.File.ReadAllLines("lang\sv.txt")
            Syntax.svList.Add(line)
        Next

        For Each line As String In IO.File.ReadAllLines("lang\svl.txt")
            Syntax.svlList.Add(line)
        Next

        'Load class variables
        For Each line As String In IO.File.ReadAllLines("lang\classvariables.txt")
            Syntax.classVar.Add(line)
        Next
    End Sub

    Private Sub highlightSyntax() 'Rewrite this
        If Syntax.doHighlightning = True Then

            If codeRTB.Text.Length > 0 Then
                Dim selectStart As Integer = codeRTB.SelectionStart
                codeRTB.Select(0, codeRTB.TextLength)
                codeRTB.SelectionColor = Color.Black
                codeRTB.DeselectAll()

                'Keywords
                For Each word As String In Syntax.commandList
                    Dim pos As Integer = 0
                    Do While codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                        pos = codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos)
                        codeRTB.Select(pos, word.Length)
                        codeRTB.SelectionColor = Syntax.commandColor
                        pos += 1
                    Loop
                Next

                'Functions
                For Each word As String In Syntax.functionList
                    Dim pos As Integer = 0
                    Do While codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                        pos = codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos)
                        codeRTB.Select(pos, word.Length)
                        codeRTB.SelectionColor = Syntax.functionColor
                        pos += 1
                    Loop
                Next

                'Special variables
                For Each word As String In Syntax.svList
                    Dim pos As Integer = 0
                    Do While codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                        pos = codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos)
                        codeRTB.Select(pos, word.Length)
                        codeRTB.SelectionColor = Syntax.svColor
                        pos += 1
                    Loop
                Next

                'Class variables
                For Each word As String In Syntax.classVar
                    Dim pos As Integer = 0
                    Do While codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                        pos = codeRTB.Text.ToUpper.IndexOf(word.ToUpper, pos)
                        codeRTB.Select(pos, word.Length)
                        codeRTB.SelectionColor = Syntax.classVarColor
                        pos += 1
                    Loop
                Next

                codeRTB.SelectionStart = selectStart
                codeRTB.DeselectAll()
            End If
        End If
    End Sub

    Private Sub Print(s As String)
        consoleTB.AppendText(s & vbCrLf)
    End Sub

    Private Sub execLoop() 'Main loop for the interpreter, this wil read code line by line and execute it.
        Print("STARTING CODE EXECUTION")

        'Clear variable listbox
        varLB.Items.Clear()

        While isExecuting = True 'Rewrite this a bit
            Dim lineCount As Integer = 1
            For Each line As String In codeRTB.Lines()
                If waitLines = 0 Then
                    Try
                        Dim cmd As String = getCmd(line)
                        Dim arg As String = findArgs(line)
                        Dim argList As New List(Of String)
                        argList = getArgs(arg)
                        getCommand(cmd.ToLower, argList, arg)
                        lineCount += 1
                    Catch ex As Exception
                        Print("[ERROR] Invalid syntax on line " & lineCount & "! Skipping line...")
                        Print("[ERROR - VB OUTPUT] " & ex.Message)
                        'ADD : Option to push VB errors as message boxes.
                    End Try
                Else
                    waitLines -= 1
                End If
            Next
            isExecuting = False
        End While

        'Clear variables
        varList.Clear()
        valList.Clear()

        'Clear functions
        funcList.Clear()
        funcArgList.Clear()

        Print("CODE EXECUTION FINISHED")
    End Sub

    Private Function getCmd(ByVal line As String)
        Dim cmd As String = line.Substring(0, line.IndexOf(Syntax.lineStarter)).Trim
        Return cmd
    End Function

    Private Function findArgs(ByVal line As String)
        Dim startPos As Integer = InStr(line, Syntax.lineStarter)
        Dim endPos As Integer = InStr(line, Syntax.lineEnder)
        Dim arg As String = Mid(line, startPos + 1, endPos - startPos - 1).Replace(" ", "")
        Return arg
    End Function

    Private Sub getCommand(ByVal cmd As String, ByVal arg As List(Of String), ByVal oArg As String) 'Rewrite this a bit
        Dim op As Char = ""
        op = getOperator(oArg)

        'Identify command and do thing
        Select Case cmd
            Case "def"
                def(arg)
            Case "calc"
                Print(oArg & " = " & calc(arg))
            Case "sqrt"
                sqrt(arg)
            Case "loop(calc)" 'Completely rewrite
                doLoop(arg, oArg)
            Case "function"
                defFunc(arg)
            Case "skip"
                waitLines = oArg
            Case "set"
                setVar(arg)
            Case "class"

            Case "import"
                importClass(oArg)
            Case Else
                If funcList.Contains(cmd) Then
                    getFunc(cmd, arg)
                Else
                    Print("[ERROR] Command: '" & cmd & "' is invalid! Skipping line...")
                End If
        End Select
    End Sub

    Private Function getArgs(ByVal arg As String) 'This is the 'new and improved' getArgs function
        Dim argList As New List(Of String)
        Dim argStr As String = ""

        For Each character As Char In arg
            If Syntax.opList.Contains(character) Then
                argList.Add(argStr.ToString)
                argList.Add(character)
                argStr = ""
            Else
                argStr = argStr & character
            End If
        Next
        argList.Add(argStr)

        Return argList
    End Function

    Private Function getOperator(ByVal arg As String) 'Obsolete, if using new getArgs
        Dim op As Char = ""
        For Each character As Char In arg
            If Syntax.opList.Contains(character) Then
                op = character
                Return op
            End If
        Next
        Return op
    End Function

    Private Function getNum(ByVal arg As String)
        If IsNumeric(arg) = True Then
            Return arg
        Else
            If Syntax.opList.Contains(arg) Then
                Return arg
            ElseIf Syntax.svList.Contains(arg) Then
                Dim indexNum As Integer = Syntax.svList.IndexOf(arg)
                Return Syntax.svlList.Item(indexNum).Replace(",", My.Settings.decimalSeparator)
            Else
                Dim indexNum As Integer = varList.IndexOf(arg)
                Return valList.Item(indexNum)
            End If
        End If
    End Function

    Private Function calculateNum(ByVal num1 As Double, ByVal num2 As Double, ByVal op As Char)
        Dim finalNum As Double

        Select Case op
            Case "+"
                finalNum = num1 + num2
            Case "-"
                finalNum = num1 - num2
            Case "/"
                finalNum = num1 / num2
            Case "*"
                finalNum = num1 * num2
            Case "^"
                finalNum = Math.Pow(num1, num2)
        End Select

        Return finalNum
    End Function

    Private Sub def(ByVal arg As List(Of String)) 'rewrite this to support inputting equations.
        Dim var As String = arg.Item(0)
        If varList.Contains(var) Then
            Print("[ERROR] Variable '" & var & "' already exists. Skipping line...")
        Else
            arg.RemoveAt(1)
            arg.RemoveAt(0)
            If arg.Count = 1 Then
                varList.Add(var)
                valList.Add(arg.Item(0))
            ElseIf arg.Count > 2 Then
                varList.Add(var)
                valList.Add(calc(arg))
            Else
                Print("[ERROR] Invalid syntax! Def command arguments must be 1 or > 2! Skipping line...")
            End If

            varLB.Items.Add(varList.Item(varList.Count - 1) & " = " & valList.Item(valList.Count - 1))
        End If
    End Sub

    Private Sub setVar(ByVal arg As List(Of String))
        If varList.Contains(arg.Item(0)) Then
            Dim varIndex = varList.IndexOf(arg.Item(0))
            Dim var = arg.Item(0)
            arg.RemoveAt(1)
            arg.RemoveAt(0)
            If arg.Count = 1 Then
                valList.Item(varList.IndexOf(arg.Item(0))) = arg.Item(0)
                Print("Variable '" & var & "' changed!")
            ElseIf arg.Count > 2 Then
                valList.Item(varIndex) = calc(arg)
                Print("Variable '" & var & "' changed!")
            Else
                Print("[ERROR] Invalid syntax! Set command arguments must be 1 or > 2! Skipping line...")
            End If
        End If
    End Sub

    Private Sub importClass(ByVal Arg As String) 'Fix stuff
        Dim className As String
        Dim finishedImport As Boolean = False

        If My.Computer.FileSystem.FileExists(Arg) = False Then
            Print("[ERROR] Class '" & Arg & "' could not be found! Skipping line...")
        Else
            finishedImport = True
            While finishedImport = True
                For Each line As String In IO.File.ReadAllLines(Arg)
                    If getCmd(line).ToString.ToLower = "class" Then
                        Dim argList As List(Of String) = getArgs(line.Replace(getCmd(line.Replace(" ", "")) & Syntax.lineStarter, "").Replace(Syntax.lineEnder, ""))
                        className = argList.Item(0)
                        finishedImport = False
                    Else

                    End If
                Next
            End While
        End If

        If className IsNot Nothing Then
            classList.Add(className)
            classList.Add(Arg)
            Print("Class '" & className & "' added!")
        End If
    End Sub

    Private Sub doLoop(ByVal arg As List(Of String), ByVal oArg As String)
        'Fix this.
        'Make user able to select custom length.
    End Sub

    Private Function calc(ByVal arg As List(Of String)) 'Add modulus support later
        Dim isLooping = False

        For Each op As Char In Syntax.opList 'Loop through each operator
            Dim i As Integer = 0
            Dim num1 As Double
            Dim num2 As Double

            isLooping = True

            Do While isLooping = True 'Check entire argument for operator
                If arg.Item(i) = op Then
                    If arg.Item(i - 1) = Syntax.delStr Then
                        num1 = getNum(arg.Item(i - 2))
                    Else
                        num1 = getNum(arg.Item(i - 1))
                    End If
                    num2 = getNum(arg.Item(i + 1))
                    arg.Item(i) = calculateNum(num1, num2, op)
                    If arg.Item(i - 1) = Syntax.delStr Then
                        arg.Item(i - 2) = Syntax.delStr
                    Else
                        arg.Item(i - 1) = Syntax.delStr
                    End If
                    arg.Item(i + 1) = Syntax.delStr
                End If
                i += 1
                If i = arg.Count Then isLooping = False
            Loop

            If arg.Contains(Syntax.delStr) Then 'Delete delStr if needed
                Dim noStr = False
                While noStr = False
                    Dim delString As Integer = arg.IndexOf(Syntax.delStr)
                    arg.RemoveAt(delString)
                    If arg.Contains(Syntax.delStr) Then
                        noStr = False
                    Else
                        noStr = True
                    End If
                End While
            End If

        Next
        Return arg.Item(0)
    End Function

    Private Sub sqrt(ByVal arg As List(Of String))
        Dim num As Double = getNum(arg.Item(0))

        Print("sqrt: " & num & "; = " & Math.Sqrt(num))
    End Sub

    Private Sub defFunc(ByVal arg As List(Of String)) 'Thou who art undead, art chosen.
        Dim funcName As String = arg.Item(0)
        Dim argStr As String = ""

        arg.RemoveAt(1)
        arg.RemoveAt(0)

        For Each item As String In arg
            argStr &= item
        Next

        funcList.Add(funcName)
        funcArgList.Add(argStr)

        Print("Added function '" & funcName & "'!")
    End Sub

    Private Sub getFunc(ByVal cmd As String, ByVal arg As List(Of String)) 'I know...
        Dim funcArg As String = funcArgList.Item(funcList.IndexOf(cmd))
        Dim argNum As Double = arg.Item(0)
        funcArg = funcArg.Replace("_", argNum.ToString)
        Print("[" & cmd & "] " & funcArg & " = " & calc(getArgs(funcArg)))
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        isExecuting = True
        execLoop()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles stopLoopBtn.Click
        isExecuting = False
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveFile.ShowDialog()
    End Sub

    Private Sub SyntaxHighlightningToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SyntaxHighlightningToolStripMenuItem.Click
        If SyntaxHighlightningToolStripMenuItem.Checked = False Then
            Syntax.doHighlightning = True
            SyntaxHighlightningToolStripMenuItem.Checked = True
            highlightSyntax()
        Else
            Syntax.doHighlightning = False
            SyntaxHighlightningToolStripMenuItem.Checked = False
            codeRTB.SelectAll()
            codeRTB.SelectionColor = Color.Black
            codeRTB.DeselectAll()
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        openScript()
    End Sub

    Private Sub OpenExecuteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenExecuteToolStripMenuItem.Click
        openScript()
        execLoop()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        About.Show()
    End Sub

    Private Sub JmCodeScriptFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JmCodeScriptFileToolStripMenuItem.Click
        codeRTB.Clear()
        Project.saveLoc = ""
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If Project.saveLoc = "" Then
            SaveFile.Show()
        Else
            codeRTB.SaveFile(Project.saveLoc, RichTextBoxStreamType.PlainText)
        End If
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        codeRTB.Undo()
    End Sub

    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        codeRTB.Redo()
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        codeRTB.Cut()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        codeRTB.Copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        codeRTB.Paste()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        codeRTB.SelectAll()
    End Sub

    Private Sub DeselectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeselectAllToolStripMenuItem.Click
        codeRTB.DeselectAll()
    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click
        codeRTB.Clear()
    End Sub

    Private Sub WindowToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles WindowToolStripMenuItem1.Click
        Dim wind As New CodePad
        wind.Show()
    End Sub
End Class