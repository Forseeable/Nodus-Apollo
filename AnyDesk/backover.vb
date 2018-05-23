Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Resources
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms

Public Class backover
    Inherits Form
    Private recttt As backover.RECT

    Public Structure RECT
        Public left As Integer

        Public top As Integer

        Public right As Integer

        Public bottom As Integer
    End Structure

    Public Const WINDOW_NAME As String = "Minecraft 1.7.10"

    Private handle As IntPtr = backover.FindWindow(Nothing, "Minecraft 1.7.10")


    <DllImport("user32.dll", CharSet:=CharSet.None, ExactSpelling:=False, SetLastError:=True)>
    Private Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function


    <DllImport("user32.dll", CharSet:=CharSet.None, ExactSpelling:=False, SetLastError:=True)>
    Private Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.None, ExactSpelling:=False, SetLastError:=True)>
    Private Shared Function GetWindowRect(ByVal hwnd As IntPtr, <Out> ByRef lpRect As backover.RECT) As Boolean
    End Function


    <DllImport("user32.dll", CharSet:=CharSet.None, ExactSpelling:=False)>
    Private Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        clickguy.Show()
        Me.Opacity = 0.4
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.Timer1.Enabled = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Dim windowLong As Integer = backover.GetWindowLong(MyBase.Handle, -20)
        backover.SetWindowLong(MyBase.Handle, -20, windowLong Or 524288 Or 32)
        backover.GetWindowRect(Me.handle, Me.recttt)
        Me.Size = New System.Drawing.Size(Me.recttt.right - Me.recttt.left, Me.recttt.bottom - Me.recttt.top)
        Me.Top = Me.recttt.top

    End Sub

    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Private Shared Function GetAsyncKeyState(vKey As Keys) As Short
    End Function

    <DllImport("user32", CharSet:=CharSet.Ansi, ExactSpelling:=True, SetLastError:=True)>
    Private Shared Function GetForegroundWindow() As IntPtr
    End Function

    <DllImport("user32", CharSet:=CharSet.Auto, ExactSpelling:=False, SetLastError:=True)>
    Private Shared Function GetWindowText(ByVal hWnd As IntPtr, ByVal lpString As StringBuilder, ByVal cch As Integer) As Integer
    End Function

    Private Function GetCaption() As String
        Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder(256)
        Dim foregroundWindow As IntPtr = Me.GetForegroundWindow()
        Me.GetWindowText(foregroundWindow, stringBuilder, stringBuilder.Capacity)
        Return stringBuilder.ToString()
    End Function

    <DllImport("user32.dll", EntryPoint:="FindWindowW")>
    Private Shared Function FindWindowW(<MarshalAs(UnmanagedType.LPTStr)> ByVal lpClassName As String, <MarshalAs(UnmanagedType.LPTStr)> ByVal lpWindowName As String) As IntPtr
    End Function



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If GetCaption.StartsWith(Main.BunifuMetroTextbox1.Text) Then

            Me.Opacity = 0.4
            clickguy.Opacity = 1
        Else
            Me.Opacity = 0
            clickguy.Opacity = 0
        End If
        backover.GetWindowRect(Me.handle, Me.recttt)
        MyBase.Top = Me.recttt.top + 30
        MyBase.Left = Me.recttt.left + 8
        MyBase.Size = New System.Drawing.Size(Me.recttt.right - Me.recttt.left - 15, Me.recttt.bottom - Me.recttt.top - 36)
    End Sub

    Private Sub FaderGroupBox1_Click(sender As Object, e As EventArgs)

    End Sub

End Class
