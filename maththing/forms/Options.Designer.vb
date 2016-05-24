<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Options
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.codeRTB = New System.Windows.Forms.RichTextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.bgPrevPanel = New System.Windows.Forms.Panel()
        Me.fgPrevLabel = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdColorPrev = New System.Windows.Forms.PictureBox()
        Me.funcColorPrev = New System.Windows.Forms.PictureBox()
        Me.svColorPrev = New System.Windows.Forms.PictureBox()
        Me.cvColorPrev = New System.Windows.Forms.PictureBox()
        Me.cvHexTB = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pickCVColorBtn = New System.Windows.Forms.Button()
        Me.svHexTB = New System.Windows.Forms.TextBox()
        Me.funcHexTB = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pickSVColorBtn = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pickFuncColorBtn = New System.Windows.Forms.Button()
        Me.cmdHexTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pickCMDColorBtn = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.appFGHex = New System.Windows.Forms.TextBox()
        Me.appBGHex = New System.Windows.Forms.TextBox()
        Me.pickFGColorBtn = New System.Windows.Forms.Button()
        Me.pickBGColorBtn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.bgColorPrev = New System.Windows.Forms.PictureBox()
        Me.bgHexTB = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pickTBBGColorBtn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.bgPrevPanel.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.cmdColorPrev, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.funcColorPrev, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.svColorPrev, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cvColorPrev, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.bgColorPrev, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(463, 350)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(455, 324)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Button2)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(455, 324)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Theme"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.codeRTB)
        Me.GroupBox4.Location = New System.Drawing.Point(247, 85)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(200, 154)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Code Editor Preview"
        '
        'codeRTB
        '
        Me.codeRTB.BackColor = System.Drawing.Color.White
        Me.codeRTB.Location = New System.Drawing.Point(6, 19)
        Me.codeRTB.Name = "codeRTB"
        Me.codeRTB.ReadOnly = True
        Me.codeRTB.Size = New System.Drawing.Size(188, 125)
        Me.codeRTB.TabIndex = 0
        Me.codeRTB.Text = "def: x = %pi * 2;" & Global.Microsoft.VisualBasic.ChrW(10) & "calc: x * [input];"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.bgPrevPanel)
        Me.GroupBox3.Location = New System.Drawing.Point(247, 9)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(200, 70)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "General Preview"
        '
        'bgPrevPanel
        '
        Me.bgPrevPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.bgPrevPanel.Controls.Add(Me.fgPrevLabel)
        Me.bgPrevPanel.Location = New System.Drawing.Point(6, 19)
        Me.bgPrevPanel.Name = "bgPrevPanel"
        Me.bgPrevPanel.Size = New System.Drawing.Size(188, 37)
        Me.bgPrevPanel.TabIndex = 0
        '
        'fgPrevLabel
        '
        Me.fgPrevLabel.AutoSize = True
        Me.fgPrevLabel.Location = New System.Drawing.Point(63, 11)
        Me.fgPrevLabel.Name = "fgPrevLabel"
        Me.fgPrevLabel.Size = New System.Drawing.Size(61, 13)
        Me.fgPrevLabel.TabIndex = 0
        Me.fgPrevLabel.Text = "Foreground"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.bgColorPrev)
        Me.GroupBox2.Controls.Add(Me.bgHexTB)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.pickTBBGColorBtn)
        Me.GroupBox2.Controls.Add(Me.cmdColorPrev)
        Me.GroupBox2.Controls.Add(Me.funcColorPrev)
        Me.GroupBox2.Controls.Add(Me.svColorPrev)
        Me.GroupBox2.Controls.Add(Me.cvColorPrev)
        Me.GroupBox2.Controls.Add(Me.cvHexTB)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.pickCVColorBtn)
        Me.GroupBox2.Controls.Add(Me.svHexTB)
        Me.GroupBox2.Controls.Add(Me.funcHexTB)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.pickSVColorBtn)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.pickFuncColorBtn)
        Me.GroupBox2.Controls.Add(Me.cmdHexTB)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.pickCMDColorBtn)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 85)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(235, 154)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Code Editor"
        '
        'cmdColorPrev
        '
        Me.cmdColorPrev.BackColor = System.Drawing.Color.White
        Me.cmdColorPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmdColorPrev.Location = New System.Drawing.Point(121, 16)
        Me.cmdColorPrev.Name = "cmdColorPrev"
        Me.cmdColorPrev.Size = New System.Drawing.Size(14, 20)
        Me.cmdColorPrev.TabIndex = 16
        Me.cmdColorPrev.TabStop = False
        '
        'funcColorPrev
        '
        Me.funcColorPrev.BackColor = System.Drawing.Color.White
        Me.funcColorPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.funcColorPrev.Location = New System.Drawing.Point(121, 43)
        Me.funcColorPrev.Name = "funcColorPrev"
        Me.funcColorPrev.Size = New System.Drawing.Size(14, 20)
        Me.funcColorPrev.TabIndex = 15
        Me.funcColorPrev.TabStop = False
        '
        'svColorPrev
        '
        Me.svColorPrev.BackColor = System.Drawing.Color.White
        Me.svColorPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.svColorPrev.Location = New System.Drawing.Point(121, 70)
        Me.svColorPrev.Name = "svColorPrev"
        Me.svColorPrev.Size = New System.Drawing.Size(14, 20)
        Me.svColorPrev.TabIndex = 14
        Me.svColorPrev.TabStop = False
        '
        'cvColorPrev
        '
        Me.cvColorPrev.BackColor = System.Drawing.Color.White
        Me.cvColorPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cvColorPrev.Location = New System.Drawing.Point(121, 97)
        Me.cvColorPrev.Name = "cvColorPrev"
        Me.cvColorPrev.Size = New System.Drawing.Size(14, 20)
        Me.cvColorPrev.TabIndex = 4
        Me.cvColorPrev.TabStop = False
        '
        'cvHexTB
        '
        Me.cvHexTB.Location = New System.Drawing.Point(141, 97)
        Me.cvHexTB.MaxLength = 7
        Me.cvHexTB.Name = "cvHexTB"
        Me.cvHexTB.Size = New System.Drawing.Size(57, 20)
        Me.cvHexTB.TabIndex = 12
        Me.cvHexTB.Text = "#112F45"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 100)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Class Variables"
        '
        'pickCVColorBtn
        '
        Me.pickCVColorBtn.Location = New System.Drawing.Point(204, 95)
        Me.pickCVColorBtn.Name = "pickCVColorBtn"
        Me.pickCVColorBtn.Size = New System.Drawing.Size(25, 23)
        Me.pickCVColorBtn.TabIndex = 13
        Me.pickCVColorBtn.UseVisualStyleBackColor = True
        '
        'svHexTB
        '
        Me.svHexTB.Location = New System.Drawing.Point(141, 70)
        Me.svHexTB.MaxLength = 7
        Me.svHexTB.Name = "svHexTB"
        Me.svHexTB.Size = New System.Drawing.Size(57, 20)
        Me.svHexTB.TabIndex = 9
        Me.svHexTB.Text = "#112F45"
        '
        'funcHexTB
        '
        Me.funcHexTB.Location = New System.Drawing.Point(141, 43)
        Me.funcHexTB.MaxLength = 7
        Me.funcHexTB.Name = "funcHexTB"
        Me.funcHexTB.Size = New System.Drawing.Size(57, 20)
        Me.funcHexTB.TabIndex = 9
        Me.funcHexTB.Text = "#112F45"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Special Variables"
        '
        'pickSVColorBtn
        '
        Me.pickSVColorBtn.Location = New System.Drawing.Point(204, 68)
        Me.pickSVColorBtn.Name = "pickSVColorBtn"
        Me.pickSVColorBtn.Size = New System.Drawing.Size(25, 23)
        Me.pickSVColorBtn.TabIndex = 10
        Me.pickSVColorBtn.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Functions"
        '
        'pickFuncColorBtn
        '
        Me.pickFuncColorBtn.Location = New System.Drawing.Point(204, 41)
        Me.pickFuncColorBtn.Name = "pickFuncColorBtn"
        Me.pickFuncColorBtn.Size = New System.Drawing.Size(25, 23)
        Me.pickFuncColorBtn.TabIndex = 10
        Me.pickFuncColorBtn.UseVisualStyleBackColor = True
        '
        'cmdHexTB
        '
        Me.cmdHexTB.Location = New System.Drawing.Point(141, 16)
        Me.cmdHexTB.MaxLength = 7
        Me.cmdHexTB.Name = "cmdHexTB"
        Me.cmdHexTB.Size = New System.Drawing.Size(57, 20)
        Me.cmdHexTB.TabIndex = 6
        Me.cmdHexTB.Text = "#112F45"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Commands"
        '
        'pickCMDColorBtn
        '
        Me.pickCMDColorBtn.Location = New System.Drawing.Point(204, 14)
        Me.pickCMDColorBtn.Name = "pickCMDColorBtn"
        Me.pickCMDColorBtn.Size = New System.Drawing.Size(25, 23)
        Me.pickCMDColorBtn.TabIndex = 7
        Me.pickCMDColorBtn.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.appFGHex)
        Me.GroupBox1.Controls.Add(Me.appBGHex)
        Me.GroupBox1.Controls.Add(Me.pickFGColorBtn)
        Me.GroupBox1.Controls.Add(Me.pickBGColorBtn)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(235, 70)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "General"
        '
        'appFGHex
        '
        Me.appFGHex.Location = New System.Drawing.Point(141, 40)
        Me.appFGHex.MaxLength = 7
        Me.appFGHex.Name = "appFGHex"
        Me.appFGHex.Size = New System.Drawing.Size(57, 20)
        Me.appFGHex.TabIndex = 4
        Me.appFGHex.Text = "#FFFFFF"
        '
        'appBGHex
        '
        Me.appBGHex.Location = New System.Drawing.Point(141, 13)
        Me.appBGHex.MaxLength = 7
        Me.appBGHex.Name = "appBGHex"
        Me.appBGHex.Size = New System.Drawing.Size(57, 20)
        Me.appBGHex.TabIndex = 1
        Me.appBGHex.Text = "#112F45"
        '
        'pickFGColorBtn
        '
        Me.pickFGColorBtn.Location = New System.Drawing.Point(204, 38)
        Me.pickFGColorBtn.Name = "pickFGColorBtn"
        Me.pickFGColorBtn.Size = New System.Drawing.Size(25, 23)
        Me.pickFGColorBtn.TabIndex = 3
        Me.pickFGColorBtn.UseVisualStyleBackColor = True
        '
        'pickBGColorBtn
        '
        Me.pickBGColorBtn.Location = New System.Drawing.Point(204, 11)
        Me.pickBGColorBtn.Name = "pickBGColorBtn"
        Me.pickBGColorBtn.Size = New System.Drawing.Size(25, 23)
        Me.pickBGColorBtn.TabIndex = 2
        Me.pickBGColorBtn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Application Background"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Application Foreground"
        '
        'bgColorPrev
        '
        Me.bgColorPrev.BackColor = System.Drawing.Color.White
        Me.bgColorPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.bgColorPrev.Location = New System.Drawing.Point(121, 124)
        Me.bgColorPrev.Name = "bgColorPrev"
        Me.bgColorPrev.Size = New System.Drawing.Size(14, 20)
        Me.bgColorPrev.TabIndex = 17
        Me.bgColorPrev.TabStop = False
        '
        'bgHexTB
        '
        Me.bgHexTB.Location = New System.Drawing.Point(141, 124)
        Me.bgHexTB.MaxLength = 7
        Me.bgHexTB.Name = "bgHexTB"
        Me.bgHexTB.Size = New System.Drawing.Size(57, 20)
        Me.bgHexTB.TabIndex = 19
        Me.bgHexTB.Text = "#112F45"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 127)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Background"
        '
        'pickTBBGColorBtn
        '
        Me.pickTBBGColorBtn.Location = New System.Drawing.Point(204, 122)
        Me.pickTBBGColorBtn.Name = "pickTBBGColorBtn"
        Me.pickTBBGColorBtn.Size = New System.Drawing.Size(25, 23)
        Me.pickTBBGColorBtn.TabIndex = 20
        Me.pickTBBGColorBtn.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(374, 295)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(6, 295)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(3, 242)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(226, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "*General theme settings are not well optimized!"
        '
        'Options
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(463, 350)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Options"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Options"
        Me.TopMost = True
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.bgPrevPanel.ResumeLayout(False)
        Me.bgPrevPanel.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.cmdColorPrev, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.funcColorPrev, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.svColorPrev, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cvColorPrev, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.bgColorPrev, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents bgPrevPanel As Panel
    Friend WithEvents fgPrevLabel As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents codeRTB As RichTextBox
    Friend WithEvents pickBGColorBtn As Button
    Friend WithEvents appFGHex As TextBox
    Friend WithEvents appBGHex As TextBox
    Friend WithEvents pickFGColorBtn As Button
    Friend WithEvents cmdColorPrev As PictureBox
    Friend WithEvents funcColorPrev As PictureBox
    Friend WithEvents svColorPrev As PictureBox
    Friend WithEvents cvColorPrev As PictureBox
    Friend WithEvents cvHexTB As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents pickCVColorBtn As Button
    Friend WithEvents svHexTB As TextBox
    Friend WithEvents funcHexTB As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents pickSVColorBtn As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents pickFuncColorBtn As Button
    Friend WithEvents cmdHexTB As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents pickCMDColorBtn As Button
    Friend WithEvents bgColorPrev As PictureBox
    Friend WithEvents bgHexTB As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents pickTBBGColorBtn As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label8 As Label
End Class
