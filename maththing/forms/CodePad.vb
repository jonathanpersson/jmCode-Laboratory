Public Class CodePad

    'Functions
    Dim funcList As New Dictionary(Of String, String)

    'Classes
    Dim classList As New Dictionary(Of String, String)

    Dim isExecuting As Boolean = False
    Dim waitLines As Integer = 0

    Dim isLooping As Boolean = False
    Dim loopCycles As Long = 0
    Dim loopStartIndex As Long = 0

    Private Sub CodePad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Do normal startup stuff
        If My.Settings.askOnStart = True Then
            setDecimalSep.ShowDialog()
        End If
        loadSyntax()
        setTheme()
        getCodeStats()
        highlightSyntax()
        stopExecBtn.Visible = False
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

    Public Sub setTheme()
        BackColor = Syntax.uiColor
        ForeColor = Syntax.fgColor
        MenuStrip1.BackColor = Syntax.uiColor
        MenuStrip1.ForeColor = Syntax.fgColor
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
        For x = 0 To IO.File.ReadAllLines("lang\sv.txt").Length - 1
            Dim svL As List(Of String) = IO.File.ReadAllLines("lang\sv.txt").ToList
            Dim svlL As List(Of String) = IO.File.ReadAllLines("lang\svl.txt").ToList
            Variables.svList.Add(svL.Item(x), svlL.Item(x))
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
                codeRTB.SelectionColor = Syntax.fgColor
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
                For Each word As String In Variables.svList.Keys
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
        stopExecBtn.Visible = True

        Print("STARTING CODE EXECUTION")

        'Clear variable listbox
        varLB.Items.Clear()

        While isExecuting = True 'Rewrite this a bit
            For i = 0 To codeRTB.Lines.Count - 1
                Application.DoEvents()

                If isExecuting = False Then i = codeRTB.Lines.Count - 1

                Dim line As String = codeRTB.Lines(i)
                If waitLines = 0 Then
                    line.Replace(",", My.Settings.decimalSeparator)
                    line.Replace(".", My.Settings.decimalSeparator)
                    Try
                        Dim cmd As String = getCmd(line).ToString.ToLower.Replace(" ", "")
                        Dim arg As String
                        Dim argList As New List(Of String)

                        If cmd = "endloop" Then

                        Else
                            arg = findArgs(line)
                            argList = getArgs(arg.Replace(" ", ""))
                        End If

                            If cmd = "loop" Then loopStartIndex = i

                            If isLooping = True Then
                                If cmd = "endloop" Then
                                    loopCycles -= 1
                                    If loopCycles = 0 Then
                                        isLooping = False
                                    Else
                                        i = loopStartIndex
                                    End If
                                Else
                                execCommand(cmd, argList, arg)
                            End If
                            Else
                            execCommand(cmd, argList, arg)
                        End If
                        Catch ex As Exception
                            Print("[ERROR] Invalid syntax on line " & i + 1 & "! Skipping line...")
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
        Variables.VarList.Clear()

        'Clear functions
        funcList.Clear()

        'Clear classes
        classList.Clear()

        Print("CODE EXECUTION FINISHED")

        stopExecBtn.Visible = False
    End Sub

    Private Function getCmd(ByVal line As String)
        Dim cmd As String = line.Substring(0, line.IndexOf(Syntax.lineStarter)).Trim
        Return cmd
    End Function

    Private Function findArgs(ByVal line As String)
        Dim startPos As Integer = InStr(line, Syntax.lineStarter)
        Dim endPos As Integer = InStr(line, Syntax.lineEnder)
        Dim arg As String = Mid(line, startPos + 1, endPos - startPos - 1)
        Return arg
    End Function

    Private Sub execCommand(ByVal cmd As String, ByVal arg As List(Of String), ByVal oArg As String) 'Rewrite this a bit
        Dim op As Char = ""
        op = getOperator(oArg)

        'Identify command and do thing
        Select Case cmd
            Case "def"
                def(arg)
            Case "calc"
                Print(oArg.Replace(" ", "") & " = " & calc(arg))
            Case "sqrt"
                sqrt(arg)
            Case "loop" 'Start loop
                doLoop(arg)
            Case "function"
                defFunc(arg)
            Case "skip"
                waitLines = oArg.Replace(" ", "")
            Case "set"
                setVar(arg)
            Case "class"

            Case "import"
                importClass(oArg.Replace(" ", ""))
            Case "print"
                Print(getNum(arg.Item(0)))
            Case "printl"
                Print(getString(oArg))
            Case Else
                If funcList.ContainsKey(cmd) = True Then
                    getFunc(cmd, arg)
                ElseIf classList.ContainsKey(cmd) = True Then
                    useClass(cmd, arg)
                Else
                    Print("[ERROR] Command: '" & cmd & "' is invalid! Skipping line...")
                End If
        End Select
    End Sub

    Private Function getString(ByVal oArg As String)
        Dim startPos As Integer = oArg.IndexOf(""""c)
        Dim endPos As Integer = oArg.LastIndexOf(""""c)
        Return Mid(oArg, startPos + 2, endPos - startPos - 1)
    End Function

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
            ElseIf variables.svList.ContainsKey(arg) Then
                Return Variables.svList.Item(arg).Replace(",", My.Settings.decimalSeparator)
            Else
                Return Variables.VarList.Item(arg)
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
        If Variables.VarList.ContainsKey(var) Then
            Print("[ERROR] Variable '" & var & "' already exists. Skipping line...")
        Else
            arg.RemoveAt(1)
            arg.RemoveAt(0)
            If arg.Count = 1 Then
                Variables.VarList.Add(var, arg.Item(0))
            ElseIf arg.Count > 2 Then
                Variables.VarList.Add(var, calc(arg))
            Else
                Print("[ERROR] Invalid syntax! Def command arguments must be 1 or > 2! Skipping line...")
            End If
            varLB.Items.Add(var & " = " & Variables.VarList.Item(var))
        End If
    End Sub

    Private Sub setVar(ByVal arg As List(Of String)) 'Fix this
        If Variables.VarList.ContainsKey(arg.Item(0)) Then
            Dim var = arg.Item(0)
            arg.RemoveAt(1)
            arg.RemoveAt(0)
            If arg.Count = 1 Then
                Variables.VarList.Item(var) = arg.Item(0)
            ElseIf arg.Count > 2 Then
                Variables.VarList.Item(var) = calc(arg)
            Else
                Print("[ERROR] Invalid syntax! Set command arguments must be 1 or > 2! Skipping line...")
            End If
        End If
    End Sub

    Private Sub importClass(ByVal Arg As String)
        Dim className As String
        Dim finishedImport As Boolean = False

        If My.Computer.FileSystem.FileExists(Arg) = False Then
            Print("[ERROR] Class '" & Arg & "' could not be found! Skipping line...")
        Else
            While finishedImport = False
                For Each line As String In IO.File.ReadAllLines(Arg)
                    If getCmd(line).ToString.ToLower = "class" Then
                        Dim argList As List(Of String) = getArgs(line.Replace(getCmd(line.Replace(" ", "")) & Syntax.lineStarter, "").Replace(Syntax.lineEnder, ""))
                        className = argList.Item(0)
                        finishedImport = True
                    Else

                    End If
                Next
            End While
        End If

        className = className.Replace(" ", "") 'Make sure there are no spaces in className


        If className IsNot Nothing Then 'Try adding className to classList
            classList.Add(className.ToLower, Arg)
            If classList.ContainsKey(className) Then
                Print("Class '" & className & "' added!")
            Else
                Print("[ERROR] Failed to add '" & className & "'! An unexpected error occured.")
            End If
        Else
            Print("[ERROR] Failed to add class ' " & Arg & "'! No className is declared. Skipping line...")
        End If
    End Sub

    Private Sub useClass(ByVal className As String, ByVal arg As List(Of String))
        Dim sArg As Double 'Argument sent to class

        'Get sArg
        If arg.Count = 1 Then
            sArg = getNum(arg.Item(0))
        ElseIf arg.Count > 2 Then
            sArg = calc(arg)
        Else
            Print("[ERROR] Invalid syntax! Class argument needs to be 1 or > 2! Skipping line...")
        End If

        For Each line As String In IO.File.ReadAllLines(classList.Item(className)) 'Loop through class
            Dim cmd As String = getCmd(line)
            Dim oArg As String = findArgs(line).replace("[input]", sArg)
            Dim cArg As List(Of String) = getArgs(oArg)
            execCommand(cmd, cArg, oArg) 'Create execCommand specific for classes since not all commands are compatible
        Next
    End Sub

    Private Sub doLoop(ByVal arg As List(Of String))
        If arg.Count = 1 Then
            Dim n As Double = arg.Item(0)
            n = Math.Round(n, 0)
            If n = 0 Then n = 1
            loopCycles = n
            isLooping = True
        ElseIf arg.Count > 2 Then
            Dim n As Double = calc(arg)
            n = Math.Round(n, 0)
            If n = 0 Then n = 1
            loopCycles = n
            isLooping = True
        Else
            Print("[ERROR] Invalid syntax! Loop command arguments must be 1 or > 2! Skipping line...")
        End If
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

        funcList.Add(funcName.ToLower, argStr)

        Print("Added function '" & funcName & "'!")
    End Sub

    Private Sub getFunc(ByVal cmd As String, ByVal arg As List(Of String)) 'I know...
        Dim funcArg As String = funcList.Item(cmd)
        Dim argNum As Double = arg.Item(0)
        funcArg = funcArg.Replace("_", argNum.ToString)
        Print("[" & cmd & "] " & funcArg & " = " & calc(getArgs(funcArg)))
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        isExecuting = True
        execLoop()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles stopExecBtn.Click
        isExecuting = False
        stopExecBtn.Visible = False
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
        isExecuting = True
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

    Private Sub ExecuteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExecuteToolStripMenuItem.Click
        isExecuting = True
        execLoop()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Options.Show()
    End Sub
End Class