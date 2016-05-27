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
        openNewTab()
        setTheme()
        getCodeStats()
        highlightSyntax()
        stopExecBtn.Visible = False
    End Sub

    Private Sub openNewTab()
        Dim cRTB As New RichTextBox
        TabControl2.TabPages.Add(1, "Untitled")
        TabControl2.SelectTab(TabControl2.TabPages.Count - 1)
        cRTB.Name = "codeRTB"
        cRTB.Dock = DockStyle.Fill
        cRTB.BorderStyle = BorderStyle.None
        TabControl2.SelectedTab.Controls.Add(cRTB)
        AddHandler cRTB.TextChanged, AddressOf cRTBTextChange
    End Sub

    Private Sub openScript()
        Try
            Dim openFile As New OpenFileDialog
            openFile.Filter = "jmCode Script Files *.jmcode | *.jmcode"
            If openFile.ShowDialog = DialogResult.OK Then
                CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Text = IO.File.ReadAllText(openFile.FileName)

                If Project.saveLocs.ContainsKey(TabControl2.SelectedTab.Text) Then
                    Project.saveLocs.Remove(TabControl2.SelectedTab.Text)
                End If

                Dim fileNmIndex As Integer = openFile.FileName.LastIndexOf("\")
                Dim fileName As String = openFile.FileName.Substring(fileNmIndex + 1, openFile.FileName.Length - fileNmIndex - 1)

                Project.saveLocs.Add(fileName, openFile.FileName)
                TabControl2.SelectedTab.Text = fileName
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub setTheme()
        BackColor = Syntax.uiColor
        ForeColor = Syntax.fgColor
        MenuStrip1.BackColor = Syntax.uiColor
        MenuStrip1.ForeColor = Syntax.fgColor
        ToolStrip1.BackColor = Syntax.uiColor
        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).BackColor = Syntax.bgColor
    End Sub

    Private Sub getCodeStats()
        lineCountLabel.Text = "Lines: " & CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Lines.Count & " Characters: " & CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Text.Length
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

            If CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Text.Length > 0 Then
                Dim selectStart As Integer = CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).SelectionStart
                CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Select(0, CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).TextLength)
                CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Syntax.fgColor
                CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).DeselectAll()

                'Keywords
                For Each word As String In Syntax.commandList
                    Dim pos As Integer = 0
                    Do While CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                        pos = CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Text.ToUpper.IndexOf(word.ToUpper, pos)
                        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Select(pos, word.Length)
                        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Syntax.commandColor
                        pos += 1
                    Loop
                Next

                'Functions
                For Each word As String In Syntax.functionList
                    Dim pos As Integer = 0
                    Do While CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                        pos = CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Text.ToUpper.IndexOf(word.ToUpper, pos)
                        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Select(pos, word.Length)
                        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Syntax.functionColor
                        pos += 1
                    Loop
                Next

                'Special variables
                For Each word As String In Variables.svList.Keys
                    Dim pos As Integer = 0
                    Do While CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                        pos = CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Text.ToUpper.IndexOf(word.ToUpper, pos)
                        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Select(pos, word.Length)
                        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Syntax.svColor
                        pos += 1
                    Loop
                Next

                'Class variables
                For Each word As String In Syntax.classVar
                    Dim pos As Integer = 0
                    Do While CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Text.ToUpper.IndexOf(word.ToUpper, pos) >= 0
                        pos = CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Text.ToUpper.IndexOf(word.ToUpper, pos)
                        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Select(pos, word.Length)
                        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Syntax.classVarColor
                        pos += 1
                    Loop
                Next

                CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).SelectionStart = selectStart
                CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).DeselectAll()
            End If
        End If
    End Sub

    Private Sub Print(s As String)
        consoleTB.AppendText(s & vbCrLf)
    End Sub

    Private Sub execLoop() 'Main loop for the interpreter, this wil read code line by line and execute it.
        stopExecBtn.Visible = True

        'Clear variable listbox
        varLB.Items.Clear()
        consoleTB.Clear()

        Print("STARTING CODE EXECUTION")

        While isExecuting = True 'Rewrite this a bit
            For i = 0 To CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Lines.Count - 1
                Application.DoEvents()

                If isExecuting = False Then i = CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Lines.Count - 1

                Dim line As String = CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Lines(i)
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

        clearVariables()

        Print("CODE EXECUTION FINISHED")

        stopExecBtn.Visible = False
    End Sub

    Private Sub clearVariables()
        Variables.VarList.Clear()
        Variables.boolList.Clear()
        Variables.stringList.Clear()
        classList.Clear()
        funcList.Clear()
        waitLines = 0
        isLooping = False
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
            Case "def", "var"
                defVar(arg)
            Case "calc"
                Print(oArg.Replace(" ", "") & " = " & calc(arg))
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
                If oArg.Contains(""""c) Then
                    Print(getString(oArg))
                Else
                    Dim itm As String = oArg.Replace(" ", "")
                    If Variables.stringList.ContainsKey(itm) Then
                        Print(Variables.stringList.Item(itm))
                    Else
                        Print("[ERROR] String variable: '" & oArg & "' could not be found!")
                    End If
                End If
                    Case "bool"
                defBool(arg)
            Case "string"
                defString(arg, oArg)
            Case "library"

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

    Private Sub defVar(ByVal arg As List(Of String))
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

    Private Sub defBool(ByVal arg As List(Of String))
        Dim value As Boolean = False

        If arg.Item(2).ToLower = "true" Then
            value = True
        ElseIf arg.Item(2).ToLower = "false" Then
            value = False
        ElseIf arg.Item(2) = 1 Then
            value = True
        ElseIf arg.Item(2) = 0 Then
            value = False
        End If

        Variables.boolList.Add(arg.Item(0), value)
        Print(value)
    End Sub

    Private Sub defString(ByVal arg As List(Of String), ByVal oArg As String)
        Dim str As String = getString(oArg)
        Variables.stringList.Add(arg.Item(0), str)
        Print("'" & arg.Item(0) & "' = '" & str & "'")
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

    Private Sub importLibrary(ByVal oArg As String)
        For Each line As String In IO.File.ReadAllLines(oArg)
            importClass(line)
        Next
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

    Private Sub defFunc(ByVal arg As List(Of String)) 'Obsolete, use classes.
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

    Private Sub getFunc(ByVal cmd As String, ByVal arg As List(Of String)) 'Obsolete, use classes.
        Dim funcArg As String = funcList.Item(cmd)
        Dim argNum As Double = arg.Item(0)
        funcArg = funcArg.Replace("_", argNum.ToString)
        Print("[" & cmd & "] " & funcArg & " = " & calc(getArgs(funcArg)))
    End Sub

    Private Sub cRTBTextChange()
        highlightSyntax()
        getCodeStats()
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
            CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).SelectAll()
            CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).SelectionColor = Color.Black
            CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).DeselectAll()
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
        openNewTab()
        Project.saveLoc = ""
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If Project.saveLoc = "" Then
            SaveFile.Show()
        Else
            CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).SaveFile(Project.saveLocs.Item(TabControl2.SelectedTab.Text), RichTextBoxStreamType.PlainText)
        End If
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Undo()
    End Sub

    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Redo()
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Cut()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Paste()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).SelectAll()
    End Sub

    Private Sub DeselectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeselectAllToolStripMenuItem.Click
        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).DeselectAll()
    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click
        CType(TabControl2.SelectedTab.Controls.Item(0), RichTextBox).Clear()
    End Sub

    Private Sub ExecuteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExecuteToolStripMenuItem.Click
        isExecuting = True
        execLoop()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Options.Show()
    End Sub

    Private Sub LibrariesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LibrariesToolStripMenuItem.Click
        Library_Browser.Show()
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        TabControl2.TabPages.RemoveAt(TabControl2.SelectedIndex)
        TabControl2.SelectTab(TabControl2.TabPages.Count - 1)
    End Sub

    Private Sub OpenNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenNewToolStripMenuItem.Click
        openNewTab()
        openScript()
    End Sub
End Class