﻿Public Class Syntax
    'Keywords, such as Def...
    Public Shared commandList As New List(Of String)
    Public Shared commandColor As Color = Color.OrangeRed

    'Functions, these can be built in, or custom
    Public Shared functionList As New List(Of String)
    Public Shared functionColor As Color = Color.Purple

    'Operators, such as + or -
    Public Shared opList As New List(Of Char)

    'Special variables
    Public Shared svList As New List(Of String)
    Public Shared svlList As New List(Of String)
    Public Shared svColor As Color = Color.Red

    'Class variables
    Public Shared classVar As New List(Of String)
    Public Shared classVarColor As Color = Color.Magenta

    'Add more

    'Things
    Public Shared lineStarter As Char = ":"
    Public Shared lineEnder As Char = ";"
    Public Shared delStr As String = "|DELETE|"

    'Other theme things - move to theme class later
    Public Shared bgColor As Color = Color.White
    Public Shared fgColor As Color = Color.Black
    Public Shared uiColor As Color = Color.FromName("Control")
    Public Shared doHighlightning As Boolean = True
End Class
