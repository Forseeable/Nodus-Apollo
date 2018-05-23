Imports System.IO
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.VisualBasic.Devices
Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Resources
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Media
Imports System.Threading
Imports System.Management
Imports System.Net
Imports System.Net.NetworkInformation


Public Class Main
    Dim rnd As New Random
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal key As Integer) As Integer
    Private Declare Function apimouse_event Lib "user32.dll" Alias "mouse_event" (ByVal dwFlags As Int32, ByVal dX As Int32, ByVal dY As Int32, ByVal cButtons As Int32, ByVal dwExtraInfo As Int32) As Boolean
    Private Declare Function apiGetMessageExtraInfo Lib "user32" Alias "GetMessageExtraInfo" () As Int32
    Private Declare Function SetCursorPos Lib "user32.dll" (ByVal X As Integer, ByVal Y As Integer) As Boolean
    Private Declare Function GetCursorPos Lib "user32" (ByRef point As Point) As Long
    Public Declare Function eAPISMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As String) As Integer
    Private Declare Sub keybd_event Lib "user32.dll" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)
    Public Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer
    Private Declare Function GetActiveWindow Lib "user32" () As Long
    Public Const MOUSEEVENTF_LEFTDOWN = &H2
    Public Const MOUSEEVENTF_LEFTUP = &H4
    Public Const MOUSEEVENTF_MIDDLEDOWN = &H20
    Public Const MOUSEEVENTF_MIDDLEUP = &H40
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8
    Public Const MOUSEEVENTF_RIGHTUP = &H10
    Public Const MOUSEEVENTF_MOVE = &H1
    Dim dobinds As String = "Enabled"
    Public bHook As Boolean
    Public bTimerEnd As Boolean
    Public timeLastClick As DateTime
    Public intervalClick As Integer
    Dim isHeld_LMB As Boolean
    Dim shouldClick As Boolean = False
    Dim ignoreNextRelease As Boolean = False
    Dim JitterYMax As Integer = 0
    Dim JitterYMin As Integer = 0
    Dim JitterXMin As Integer = 0
    Declare Function mouse_event Lib "user32.dll" Alias "mouse_event" (ByVal dwFlags As Int32, ByVal dX As Int32, ByVal dY As Int32, ByVal cButtons As Int32, ByVal dwExtraInfo As Int32) As Boolean
    Private Declare Function GetKeyPress Lib "user32" Alias "GetAsyncKeyState" (ByVal key As Integer) As Integer
    Dim moveStartX As Integer
    Dim moveStartY As Integer
    Dim moveEndX As Integer
    Private WithEvents mHook As New Hook
    Dim moveEndY As Integer
    Private WithEvents kbHook As New KeyboardHook
    Dim trwprlbind As Boolean = False
    Dim pottbind As Boolean = False
    Dim lavbind As Boolean = False
    Dim watbind As Boolean = False
    Dim slot As Integer = 0

    Dim dropkey As String
    Dim forwardkey As String
    Dim backwardkey As String
    Dim sprintkey As String
    Dim invkey As String
    Dim chatkey As String

    Dim cldownhide = False

    Dim wtapdis As Boolean = False
    Dim stapdis As Boolean = False

    Dim ClickerBind As Keys = Keys.None
    Dim WTapBind As Keys = Keys.None
    Dim STapBind As Keys = Keys.None

    Dim BindPearl As Keys = Keys.None
    Dim BindLave As Keys = Keys.None
    Dim BindWater As Keys = Keys.None
    Dim BindPot As Keys = Keys.None

    Dim HideBind As Keys = Keys.None
    Dim DestructBind As Keys = Keys.None
    Dim wait As Boolean = False
    Public Sub jitter(ByVal distance As Integer, ByVal steps As Integer)
        Dim start As Point
        GetCursorPos(start)
        Dim newPosition As Point = start
        Dim random As Random = New Random
        newPosition.X = (newPosition.X - random.Next((distance * -1), distance))
        newPosition.Y = (newPosition.Y - random.Next((distance * -1), distance))
        Dim iterPoint As PointF = start
        Dim slope As PointF = New PointF((newPosition.X - start.X), (newPosition.Y - start.Y))
        slope.X = (slope.X / steps)
        slope.Y = (slope.Y / steps)
        Dim i As Integer = 0
        Do While (i < steps)
            iterPoint = New PointF((iterPoint.X + slope.X), (iterPoint.Y + slope.Y))
            SetCursorPos(Point.Round(iterPoint).X, Point.Round(iterPoint).Y)
            Thread.Sleep(1)
            i = (i + 1)
        Loop

        SetCursorPos(newPosition.X, newPosition.Y)
    End Sub



    Structure POINTAPI
        Dim x As Int32
        Dim y As Int32
    End Structure
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label39.Hide()
        BunifuCheckbox14.Hide()

        VisualStudioGroupBox2.Hide()
        VisualStudioGroupBox1.Hide()
        ThirteenComboBox9.Hide()
        Label32.Hide()
        BunifuCheckbox10.Hide()
        GetMcOptions()
        BunifuCheckbox9.Hide()
        Label33.Hide()
        Label34.Hide()
        Label35.Hide()
        BunifuCheckbox7.Hide()
        Label27.Hide()
        ThirteenComboBox2.Hide()
        ThirteenComboBox1.Hide()
        BunifuCheckbox6.Hide()
        Label29.Hide()
        ThirteenComboBox3.Hide()
        ThirteenComboBox4.Hide()
        BunifuCheckbox8.Hide()
        Label30.Hide()
        ThirteenComboBox6.Hide()
        ThirteenComboBox5.Hide()
        BunifuCheckbox9.Hide()
        Label31.Hide()
        ThirteenComboBox8.Hide()
        ThirteenComboBox7.Hide()

        mHook.HookMouse()
        PictureBox7.Hide()
        JitterPic.Size = New System.Drawing.Size(JitterValue.Value + 10, JitterValue.Value + 10)
        JitterPic.Location = New Point(CInt(45 - JitterValue.Value / 2), CInt(45 - JitterValue.Value / 2))
        FlatGroupBox13.Size = New Size(187, 135)
        FlatGroupBox12.Size = New Size(186, 116)
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

    <DllImport("user32.dll", EntryPoint:="GetWindowRect")>
    Private Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As RECT) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Private Structure RECT
        Public left, top, right, bottom As Integer
    End Structure


    Private Sub Main_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        Static lpos As Point
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Me.Location += New Size(e.X - lpos.X, e.Y - lpos.Y)
        Else
            lpos = e.Location
        End If
    End Sub


    Private Sub JitterValue_Scroll(sender As Object) Handles JitterValue.Scroll
        JitterPic.Size = New System.Drawing.Size(JitterValue.Value + 10, JitterValue.Value + 10)
        JitterPic.Location = New Point(CInt(45 - JitterValue.Value / 2), CInt(45 - JitterValue.Value / 2))
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub FlatTrackBar1_Scroll(sender As Object)

    End Sub

    Private Sub MonoFlat_TrackBar1_ValueChanged()

    End Sub

    Private Sub MonoFlat_TrackBar2_ValueChanged()

    End Sub

    Private Sub MinCpsLabel_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MaxCpsLabel_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MinCps_Scroll(sender As Object) Handles MinCps.Scroll
        Label2.Text = MinCps.Value
        If MinCps.Value > MaxCps.Value - 1 Then
            MaxCps.Value = MinCps.Value
            Label4.Text = MaxCps.Value
        End If
    End Sub

    Private Sub MaxCps_Scroll(sender As Object) Handles MaxCps.Scroll
        Label4.Text = MaxCps.Value
        If MinCps.Value > MaxCps.Value - 1 Then
            MinCps.Value = MaxCps.Value
            Label2.Text = MinCps.Value
        End If
    End Sub

    Private Sub mHook_Mouse_Left_Down(ByVal ptLocat As System.Drawing.Point) Handles mHook.Mouse_Left_Down
        Dim shakeThread As New System.Threading.Thread(AddressOf Jitter)
        isHeld_LMB = True
        Me.shouldClick = True
        If Autoclicker.Enabled = True And FlatToggle4.Checked Then
            If shakeThread.IsAlive = False Then
                jitter(JitterValue.Value, 10)
            Else
                jitter(JitterValue.Value, 10)
            End If
        Else
            shakeThread.Abort()
        End If
    End Sub

    Private Sub mHook_Mouse_Left_Up(ByVal ptLocat As System.Drawing.Point) Handles mHook.Mouse_Left_Up
        isHeld_LMB = False
        If Not Me.ignoreNextRelease Then
            Me.shouldClick = False
        End If
        Me.ignoreNextRelease = False
    End Sub


    Sub Jitter()

    End Sub



    Sub Delay(ByVal dblSecs As Double)

        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
        Do Until Now > dblWaitTil
            Application.DoEvents()
        Loop

    End Sub


    Public Sub runLeftDown()
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
    End Sub

    Public Sub runLeftUp()
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
    End Sub


    Sub soundclick()
        If (Operators.CompareString(Me.FlatComboBox1.Text, "Default Sound", False) = 0) Then
            My.Computer.Audio.Play(My.Resources.Untitled, AudioPlayMode.Background)
        End If
        If (Operators.CompareString(Me.FlatComboBox1.Text, "Logitech G502", False) = 0) Then
            My.Computer.Audio.Play(My.Resources.test, AudioPlayMode.Background)
        End If
        If (Operators.CompareString(Me.FlatComboBox1.Text, "Logitech GPro", False) = 0) Then
            My.Computer.Audio.Play(My.Resources.GPro, AudioPlayMode.Background)
        End If
        If (Operators.CompareString(Me.FlatComboBox1.Text, "Logitech G303", False) = 0) Then
            My.Computer.Audio.Play(My.Resources.g3032, AudioPlayMode.Background)
        End If
        If (Operators.CompareString(Me.FlatComboBox1.Text, "Microsoft Mouse", False) = 0) Then
            My.Computer.Audio.Play(My.Resources.microsoft, AudioPlayMode.Background)
        End If
        If (Operators.CompareString(Me.FlatComboBox1.Text, "HP Mouse", False) = 0) Then
            My.Computer.Audio.Play(My.Resources.hp, AudioPlayMode.Background)
        End If
        If (Operators.CompareString(Me.FlatComboBox1.Text, "Non-Brand Mouse", False) = 0) Then
            My.Computer.Audio.Play(My.Resources.oldmouse, AudioPlayMode.Background)
        End If
        If (Operators.CompareString(Me.FlatComboBox1.Text, "Razer Deathadder", False) = 0) Then
            My.Computer.Audio.Play(My.Resources.test2, AudioPlayMode.Background)
        End If
    End Sub

    Sub razer()
        My.Computer.Audio.Play(My.Resources.test2, AudioPlayMode.Background)
    End Sub
    Sub g502()
        My.Computer.Audio.Play(My.Resources.test, AudioPlayMode.Background)
    End Sub
    Sub def()
        My.Computer.Audio.Play(My.Resources.Untitled, AudioPlayMode.Background)
    End Sub
    Sub gpro()
        My.Computer.Audio.Play(My.Resources.GPro, AudioPlayMode.Background)
    End Sub
    Sub g303()
        My.Computer.Audio.Play(My.Resources.g3032, AudioPlayMode.Background)
    End Sub
    Sub soft()
        My.Computer.Audio.Play(My.Resources.microsoft, AudioPlayMode.Background)
    End Sub
    Sub hp()
        My.Computer.Audio.Play(My.Resources.hp, AudioPlayMode.Background)
    End Sub
    Sub nb()
        My.Computer.Audio.Play(My.Resources.oldmouse, AudioPlayMode.Background)
    End Sub
    Private Async Sub Autoclicker_Tick(sender As Object, e As EventArgs) Handles Autoclicker.Tick
        Dim minval As Integer
        Dim maxval As Integer

        minval = 1000 / (MinCps.Value + MaxCps.Value * 0.2)
        maxval = 1000 / (MinCps.Value + MaxCps.Value * 0.48)
        Dim razerThread As New System.Threading.Thread(AddressOf razer)
        Dim g502Thread As New System.Threading.Thread(AddressOf g502)
        Dim gproThread As New System.Threading.Thread(AddressOf gpro)
        Dim defThread As New System.Threading.Thread(AddressOf def)
        Dim g303Thread As New System.Threading.Thread(AddressOf g303)
        Dim softThread As New System.Threading.Thread(AddressOf soft)
        Dim hpThread As New System.Threading.Thread(AddressOf hp)
        Dim nbThread As New System.Threading.Thread(AddressOf nb)

        Autoclicker.Interval = 100
        If BunifuCheckbox2.Checked = False Then
            If MinCps.Enabled = True Then
                Autoclicker.Interval = rnd.Next(maxval, minval)
            End If
        End If


        If shouldClick = True Then
            If GetCaption.StartsWith(BunifuMetroTextbox1.Text) Then
                If FlatToggle2.Checked = True Then
                    soundclick()
                End If
                runLeftDown()
                Await Task.Delay(rnd.Next(50, 70))
                ignoreNextRelease = True
                runLeftUp()
            End If
        End If


    End Sub

    Private Sub FlatToggle1_CheckedChanged(sender As Object) Handles FlatToggle1.CheckedChanged
        If FlatToggle1.Checked Then
            Autoclicker.Start()
        Else
            Autoclicker.Stop()
        End If
    End Sub

    Private Sub poof_Tick(sender As Object, e As EventArgs) Handles poof.Tick
        Me.Opacity = Me.Opacity - 0.1
        If Me.Opacity = 0 Then
            Dim location As String = System.Environment.GetCommandLineArgs()(0)
            Dim appName As String = System.IO.Path.GetFileName(location)
            TabPage1.Dispose()
            TabPage6.Dispose()
            TabPage3.Dispose()
            TabPage4.Dispose()
            TabPage5.Dispose()
            FlatTabControl1.Dispose()
            PictureBox4.Dispose()
            Label2.Dispose()
            Label4.Dispose()
            Label5.Dispose()
            Label6.Dispose()
            FlatComboBox1.Dispose()
            FlatGroupBox5.Dispose()
            Label3.Dispose()
            Label1.Dispose()
            Label8.Dispose()
            FlatGroupBox1.Dispose()
            FlatGroupBox2.Dispose()
            FlatGroupBox3.Dispose()
            FlatGroupBox4.Dispose()
            DestructButton.Dispose()
            Label7.Dispose()
            Label8.Dispose()
            Label9.Dispose()
            Label10.Dispose()
            Label11.Dispose()
            Label12.Dispose()
            Label13.Dispose()
            Label14.Dispose()
            Label15.Dispose()
            Label16.Dispose()
            Label18.Dispose()
            Label19.Dispose()
            Label20.Dispose()
            Label21.Dispose()
            Label22.Dispose()
            Label23.Dispose()
            Label24.Dispose()
            Label25.Dispose()
            Label26.Dispose()
            Label28.Dispose()
            Label29.Dispose()
            Label30.Dispose()
            Label31.Dispose()
            FlatGroupBox6.Dispose()
            FlatGroupBox7.Dispose()
            FlatGroupBox8.Dispose()
            FlatToggle1.Dispose()
            FlatToggle2.Dispose()
            FlatToggle3.Dispose()
            FlatToggle4.Dispose()
            FlatButton10.Dispose()
            FlatButton12.Dispose()
            FlatButton13.Dispose()
            FlatButton14.Dispose()
            FlatButton15.Dispose()
            FlatButton16.Dispose()
            FlatButton9.Dispose()
            FlatButton8.Dispose()
            FlatButton2.Dispose()


            If BunifuCheckbox4.Checked Then
                Dim Info As New ProcessStartInfo()
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del """ & Application.ExecutablePath & """"
                Info.WindowStyle = ProcessWindowStyle.Hidden


                Info.CreateNoWindow = True
                Info.FileName = "cmd.exe"
                Process.Start(Info)
                Application.Exit()

            End If
            If BunifuCheckbox5.Checked Then
                Dim Info As New ProcessStartInfo()
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & del C:\Windows\prefetch\*""" & appName & """*/s/q"
                Info.WindowStyle = ProcessWindowStyle.Hidden


                Info.CreateNoWindow = True
                Info.FileName = "cmd.exe"
                Process.Start(Info)


            End If
            Me.Dispose()
            Application.Exit()
        End If
    End Sub

    Private Sub Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        poof.Start()
    End Sub

    Private Sub BunifuCheckbox2_OnChange(sender As Object, e As EventArgs) Handles BunifuCheckbox2.OnChange
        If BunifuCheckbox2.Checked Then
            MaxCps.Enabled = False
            MinCps.Enabled = False
            MaxCps.HatchColor = Color.FromArgb(45, 47, 49)
            MinCps.HatchColor = Color.FromArgb(45, 47, 49)
            MinCps.Refresh()
            MaxCps.Refresh()
            Label4.Hide()
            Label2.Hide()
            PictureBox7.Show()
        Else
            MaxCps.Enabled = True
            MinCps.Enabled = True
            MaxCps.HatchColor = Color.SandyBrown
            MinCps.HatchColor = Color.SandyBrown
            MinCps.Refresh()
            MaxCps.Refresh()
            Label4.Show()
            Label2.Show()
            PictureBox7.Hide()
        End If
    End Sub

    Private Sub DestructButton_Click(sender As Object, e As EventArgs) Handles DestructButton.Click
        poof.Start()
    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Click

    End Sub


    Sub endprl()

        If GetCaption.StartsWith(BunifuMetroTextbox1.Text) Then
            wait = True
            If ThirteenComboBox1.Text = "1" Then
                InputHelper.SetKeyState(Keys.D1, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D1, True)
            End If
            If ThirteenComboBox1.Text = "2" Then
                InputHelper.SetKeyState(Keys.D2, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D2, True)
            End If
            If ThirteenComboBox1.Text = "3" Then
                InputHelper.SetKeyState(Keys.D3, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D3, True)
            End If
            If ThirteenComboBox1.Text = "4" Then
                InputHelper.SetKeyState(Keys.D4, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D4, True)
            End If
            If ThirteenComboBox1.Text = "5" Then
                InputHelper.SetKeyState(Keys.D5, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D5, True)
            End If
            If ThirteenComboBox1.Text = "6" Then
                InputHelper.SetKeyState(Keys.D6, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D6, True)
            End If
            If ThirteenComboBox1.Text = "7" Then
                InputHelper.SetKeyState(Keys.D7, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D7, True)
            End If
            If ThirteenComboBox1.Text = "8" Then
                InputHelper.SetKeyState(Keys.D8, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D8, True)
            End If
            If ThirteenComboBox1.Text = "9" Then
                InputHelper.SetKeyState(Keys.D9, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D9, True)
            End If
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.05)
            If BunifuCheckbox7.Checked = True Then
                Delay(0.2)
                apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
                Delay(0.05)
                apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
                Delay(0.05)
            End If
            If ThirteenComboBox2.Text = "1" Then
                InputHelper.SetKeyState(Keys.D1, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D1, True)
            End If
            If ThirteenComboBox2.Text = "2" Then
                InputHelper.SetKeyState(Keys.D2, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D2, True)
            End If
            If ThirteenComboBox2.Text = "3" Then
                InputHelper.SetKeyState(Keys.D3, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D3, True)
            End If
            If ThirteenComboBox2.Text = "4" Then
                InputHelper.SetKeyState(Keys.D4, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D4, True)
            End If
            If ThirteenComboBox2.Text = "5" Then
                InputHelper.SetKeyState(Keys.D5, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D5, True)
            End If
            If ThirteenComboBox2.Text = "6" Then
                InputHelper.SetKeyState(Keys.D6, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D6, True)
            End If
            If ThirteenComboBox2.Text = "7" Then
                InputHelper.SetKeyState(Keys.D7, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D7, True)
            End If
            If ThirteenComboBox2.Text = "8" Then
                InputHelper.SetKeyState(Keys.D8, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D8, True)
            End If
            If ThirteenComboBox2.Text = "9" Then
                InputHelper.SetKeyState(Keys.D9, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D9, True)
            End If
            wait = False
        End If

    End Sub


    Sub watthrow()

        If GetCaption.StartsWith(BunifuMetroTextbox1.Text) Then
            wait = True
            If ThirteenComboBox5.Text = "1" Then
                InputHelper.SetKeyState(Keys.D1, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D1, True)
            End If
            If ThirteenComboBox5.Text = "2" Then
                InputHelper.SetKeyState(Keys.D2, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D2, True)
            End If
            If ThirteenComboBox5.Text = "3" Then
                InputHelper.SetKeyState(Keys.D3, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D3, True)
            End If
            If ThirteenComboBox5.Text = "4" Then
                InputHelper.SetKeyState(Keys.D4, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D4, True)
            End If
            If ThirteenComboBox5.Text = "5" Then
                InputHelper.SetKeyState(Keys.D5, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D5, True)
            End If
            If ThirteenComboBox5.Text = "6" Then
                InputHelper.SetKeyState(Keys.D6, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D6, True)
            End If
            If ThirteenComboBox5.Text = "7" Then
                InputHelper.SetKeyState(Keys.D7, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D7, True)
            End If
            If ThirteenComboBox5.Text = "8" Then
                InputHelper.SetKeyState(Keys.D8, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D8, True)
            End If
            If ThirteenComboBox5.Text = "9" Then
                InputHelper.SetKeyState(Keys.D9, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D9, True)
            End If
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.05)
            If BunifuCheckbox8.Checked = True Then
                Delay(0.2)
                apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
                Delay(0.05)
                apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
                Delay(0.05)
            End If
            If ThirteenComboBox6.Text = "1" Then
                InputHelper.SetKeyState(Keys.D1, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D1, True)
            End If
            If ThirteenComboBox6.Text = "2" Then
                InputHelper.SetKeyState(Keys.D2, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D2, True)
            End If
            If ThirteenComboBox6.Text = "3" Then
                InputHelper.SetKeyState(Keys.D3, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D3, True)
            End If
            If ThirteenComboBox6.Text = "4" Then
                InputHelper.SetKeyState(Keys.D4, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D4, True)
            End If
            If ThirteenComboBox6.Text = "5" Then
                InputHelper.SetKeyState(Keys.D5, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D5, True)
            End If
            If ThirteenComboBox6.Text = "6" Then
                InputHelper.SetKeyState(Keys.D6, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D6, True)
            End If
            If ThirteenComboBox6.Text = "7" Then
                InputHelper.SetKeyState(Keys.D7, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D7, True)
            End If
            If ThirteenComboBox6.Text = "8" Then
                InputHelper.SetKeyState(Keys.D8, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D8, True)
            End If
            If ThirteenComboBox6.Text = "9" Then
                InputHelper.SetKeyState(Keys.D9, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D9, True)
            End If
            wait = False
        End If
    End Sub


    Sub lavthrow()

        If GetCaption.StartsWith(BunifuMetroTextbox1.Text) Then
            wait = True
            If ThirteenComboBox3.Text = "1" Then
                InputHelper.SetKeyState(Keys.D1, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D1, True)
            End If
            If ThirteenComboBox3.Text = "2" Then
                InputHelper.SetKeyState(Keys.D2, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D2, True)
            End If
            If ThirteenComboBox3.Text = "3" Then
                InputHelper.SetKeyState(Keys.D3, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D3, True)
            End If
            If ThirteenComboBox3.Text = "4" Then
                InputHelper.SetKeyState(Keys.D4, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D4, True)
            End If
            If ThirteenComboBox3.Text = "5" Then
                InputHelper.SetKeyState(Keys.D5, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D5, True)
            End If
            If ThirteenComboBox3.Text = "6" Then
                InputHelper.SetKeyState(Keys.D6, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D6, True)
            End If
            If ThirteenComboBox3.Text = "7" Then
                InputHelper.SetKeyState(Keys.D7, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D7, True)
            End If
            If ThirteenComboBox3.Text = "8" Then
                InputHelper.SetKeyState(Keys.D8, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D8, True)
            End If
            If ThirteenComboBox3.Text = "9" Then
                InputHelper.SetKeyState(Keys.D9, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D9, True)
            End If
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.05)
            If BunifuCheckbox6.Checked = True Then
                Delay(0.2)
                apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
                Delay(0.05)
                apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
                Delay(0.05)
            End If
            If ThirteenComboBox4.Text = "1" Then
                InputHelper.SetKeyState(Keys.D1, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D1, True)
            End If
            If ThirteenComboBox4.Text = "2" Then
                InputHelper.SetKeyState(Keys.D2, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D2, True)
            End If
            If ThirteenComboBox4.Text = "3" Then
                InputHelper.SetKeyState(Keys.D3, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D3, True)
            End If
            If ThirteenComboBox4.Text = "4" Then
                InputHelper.SetKeyState(Keys.D4, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D4, True)
            End If
            If ThirteenComboBox4.Text = "5" Then
                InputHelper.SetKeyState(Keys.D5, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D5, True)
            End If
            If ThirteenComboBox4.Text = "6" Then
                InputHelper.SetKeyState(Keys.D6, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D6, True)
            End If
            If ThirteenComboBox4.Text = "7" Then
                InputHelper.SetKeyState(Keys.D7, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D7, True)
            End If
            If ThirteenComboBox4.Text = "8" Then
                InputHelper.SetKeyState(Keys.D8, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D8, True)
            End If
            If ThirteenComboBox4.Text = "9" Then
                InputHelper.SetKeyState(Keys.D9, False)
                Delay(0.025)
                InputHelper.SetKeyState(Keys.D9, True)
            End If
            wait = False
        End If

    End Sub

    Async Sub trwpot()
        Dim dictionary As New Dictionary(Of String, Keys)
        dictionary.Add("A", Keys.A)
        dictionary.Add("B", Keys.B)
        dictionary.Add("C", Keys.C)
        dictionary.Add("D", Keys.D)
        dictionary.Add("E", Keys.E)
        dictionary.Add("F", Keys.F)
        dictionary.Add("G", Keys.G)
        dictionary.Add("H", Keys.H)
        dictionary.Add("I", Keys.I)
        dictionary.Add("J", Keys.J)
        dictionary.Add("K", Keys.K)
        dictionary.Add("L", Keys.L)
        dictionary.Add("M", Keys.M)
        dictionary.Add("N", Keys.N)
        dictionary.Add("O", Keys.O)
        dictionary.Add("P", Keys.P)
        dictionary.Add("Q", Keys.Q)
        dictionary.Add("R", Keys.R)
        dictionary.Add("S", Keys.S)
        dictionary.Add("T", Keys.T)
        dictionary.Add("U", Keys.U)
        dictionary.Add("V", Keys.V)
        dictionary.Add("W", Keys.W)
        dictionary.Add("X", Keys.X)
        dictionary.Add("Y", Keys.Y)
        dictionary.Add("Z", Keys.Z)
        dictionary.Add("a", Keys.A)
        dictionary.Add("b", Keys.B)
        dictionary.Add("c", Keys.C)
        dictionary.Add("d", Keys.D)
        dictionary.Add("e", Keys.E)
        dictionary.Add("f", Keys.F)
        dictionary.Add("g", Keys.G)
        dictionary.Add("h", Keys.H)
        dictionary.Add("i", Keys.I)
        dictionary.Add("j", Keys.J)
        dictionary.Add("k", Keys.K)
        dictionary.Add("l", Keys.L)
        dictionary.Add("m", Keys.M)
        dictionary.Add("n", Keys.N)
        dictionary.Add("o", Keys.O)
        dictionary.Add("p", Keys.P)
        dictionary.Add("q", Keys.Q)
        dictionary.Add("r", Keys.R)
        dictionary.Add("s", Keys.S)
        dictionary.Add("t", Keys.T)
        dictionary.Add("u", Keys.U)
        dictionary.Add("v", Keys.V)
        dictionary.Add("w", Keys.W)
        dictionary.Add("x", Keys.X)
        dictionary.Add("y", Keys.Y)
        dictionary.Add("z", Keys.Z)
        dictionary.Add("R-SHIFT", Keys.RShiftKey)

        If GetCaption.StartsWith(BunifuMetroTextbox1.Text) Then
            If wait = False Then
                If slot = 0 Then
                    slot = ThirteenComboBox7.Text
                End If
                wait = True
                If slot = 1 Then
                    InputHelper.SetKeyState(Keys.D1, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D1, True)
                End If
                If slot = 2 Then
                    InputHelper.SetKeyState(Keys.D2, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D2, True)
                End If
                If slot = 3 Then
                    InputHelper.SetKeyState(Keys.D3, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D3, True)
                End If
                If slot = 4 Then
                    InputHelper.SetKeyState(Keys.D4, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D4, True)
                End If
                If slot = 5 Then
                    InputHelper.SetKeyState(Keys.D5, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D5, True)
                End If
                If slot = 6 Then
                    InputHelper.SetKeyState(Keys.D6, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D6, True)
                End If
                If slot = 7 Then
                    InputHelper.SetKeyState(Keys.D7, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D7, True)
                End If
                If slot = 8 Then
                    InputHelper.SetKeyState(Keys.D8, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D8, True)
                End If
                If slot = 9 Then
                    InputHelper.SetKeyState(Keys.D9, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D9, True)
                End If
                If slot = 10 Then
                    InputHelper.SetKeyState(Keys.D0, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D0, True)
                End If
                Delay(0.03)
                apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
                Delay(0.05)
                apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
                Delay(0.05)
                If BunifuCheckbox9.Checked Then
                    InputHelper.SetKeyState(dictionary(dropkey), False)
                    Delay(0.025)
                    InputHelper.SetKeyState(dictionary(dropkey), True)
                End If
                Delay(0.005)
                If ThirteenComboBox8.Text = "1" Then
                    InputHelper.SetKeyState(Keys.D1, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D1, True)
                End If
                If ThirteenComboBox8.Text = "2" Then
                    InputHelper.SetKeyState(Keys.D2, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D2, True)
                End If
                If ThirteenComboBox8.Text = "3" Then
                    InputHelper.SetKeyState(Keys.D3, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D3, True)
                End If
                If ThirteenComboBox8.Text = "4" Then
                    InputHelper.SetKeyState(Keys.D4, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D4, True)
                End If
                If ThirteenComboBox8.Text = "5" Then
                    InputHelper.SetKeyState(Keys.D5, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D5, True)
                End If
                If ThirteenComboBox8.Text = "6" Then
                    InputHelper.SetKeyState(Keys.D6, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D6, True)
                End If
                If ThirteenComboBox8.Text = "7" Then
                    InputHelper.SetKeyState(Keys.D7, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D7, True)
                End If
                If ThirteenComboBox8.Text = "8" Then
                    InputHelper.SetKeyState(Keys.D8, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D8, True)
                End If
                If ThirteenComboBox8.Text = "9" Then
                    InputHelper.SetKeyState(Keys.D9, False)
                    Delay(0.025)
                    InputHelper.SetKeyState(Keys.D9, True)
                End If
                slot = slot + 1
                If slot = ThirteenComboBox9.Text + 1 Then
                    slot = ThirteenComboBox7.Text
                End If
                Await Task.Delay(100)
                wait = False
            End If
        End If

    End Sub

    Private Async Sub kbHook_KeyDown(ByVal Key As System.Windows.Forms.Keys) Handles kbHook.KeyDown
        Debug.WriteLine(Key.ToString)
        Dim dictionary As New Dictionary(Of String, Keys)
        dictionary.Add("A", Keys.A)
        dictionary.Add("B", Keys.B)
        dictionary.Add("C", Keys.C)
        dictionary.Add("D", Keys.D)
        dictionary.Add("E", Keys.E)
        dictionary.Add("F", Keys.F)
        dictionary.Add("G", Keys.G)
        dictionary.Add("H", Keys.H)
        dictionary.Add("I", Keys.I)
        dictionary.Add("J", Keys.J)
        dictionary.Add("K", Keys.K)
        dictionary.Add("L", Keys.L)
        dictionary.Add("M", Keys.M)
        dictionary.Add("N", Keys.N)
        dictionary.Add("O", Keys.O)
        dictionary.Add("P", Keys.P)
        dictionary.Add("Q", Keys.Q)
        dictionary.Add("R", Keys.R)
        dictionary.Add("S", Keys.S)
        dictionary.Add("T", Keys.T)
        dictionary.Add("U", Keys.U)
        dictionary.Add("V", Keys.V)
        dictionary.Add("W", Keys.W)
        dictionary.Add("X", Keys.X)
        dictionary.Add("Y", Keys.Y)
        dictionary.Add("Z", Keys.Z)
        dictionary.Add("a", Keys.A)
        dictionary.Add("b", Keys.B)
        dictionary.Add("c", Keys.C)
        dictionary.Add("d", Keys.D)
        dictionary.Add("e", Keys.E)
        dictionary.Add("f", Keys.F)
        dictionary.Add("g", Keys.G)
        dictionary.Add("h", Keys.H)
        dictionary.Add("i", Keys.I)
        dictionary.Add("j", Keys.J)
        dictionary.Add("k", Keys.K)
        dictionary.Add("l", Keys.L)
        dictionary.Add("m", Keys.M)
        dictionary.Add("n", Keys.N)
        dictionary.Add("o", Keys.O)
        dictionary.Add("p", Keys.P)
        dictionary.Add("q", Keys.Q)
        dictionary.Add("r", Keys.R)
        dictionary.Add("s", Keys.S)
        dictionary.Add("t", Keys.T)
        dictionary.Add("u", Keys.U)
        dictionary.Add("v", Keys.V)
        dictionary.Add("w", Keys.W)
        dictionary.Add("x", Keys.X)
        dictionary.Add("y", Keys.Y)
        dictionary.Add("z", Keys.Z)
        dictionary.Add("R-SHIFT", Keys.RShiftKey)
        dictionary.Add("L-SHIFT", Keys.LShiftKey)
        dictionary.Add("SHIFT", Keys.Shift)
        dictionary.Add("leftctrl", Keys.LControlKey)

        If wtapdis = False Then
            If Key = dictionary(forwardkey) And CInt(Autoclicker.Enabled = True) And CInt(Me.shouldClick = True = True) And FlatButton18.BaseColor = Color.SandyBrown Then
                wtapdis = True
                InputHelper.SetKeyState(dictionary(forwardkey), True)
                Delay(0.025)
                InputHelper.SetKeyState(dictionary(forwardkey), False)
                Await Task.Delay(FlatTrackBar3.Value)
                wtapdis = False
            End If
        End If

        If FlatButton4.BaseColor = Color.SandyBrown Then
            If Key = dictionary(forwardkey) Then
                InputHelper.SetKeyState(dictionary(sprintkey), True)
            End If
        End If

        If stapdis = False Then
            If Key = dictionary(forwardkey) And CInt(Autoclicker.Enabled = True) And CInt(Me.shouldClick = True = True) And FlatButton19.BaseColor = Color.SandyBrown Then
                stapdis = True
                InputHelper.SetKeyState(dictionary(backwardkey), False)
                Delay(0.025)
                InputHelper.SetKeyState(dictionary(backwardkey), True)
                Await Task.Delay(FlatTrackBar2.Value)
                stapdis = False
            End If
        End If



        If FlatButton10.Text = "> ... <" Then
            BindPearl = Key
            FlatButton10.Text = "(" + Key.ToString + ") " + "Ender Pearl"
            If Key = Keys.Escape Then
                trwprlbind = False
                FlatButton10.Text = "Ender Pearl"
                Dim BindPearl As Keys = Keys.None
                FlatButton10.Refresh()
            End If
        End If

        If Key = ClickerBind Then
            If FlatToggle1.Checked = True Then
                FlatToggle1.Checked = False
                FlatToggle1.Refresh()
            Else
                FlatToggle1.Checked = True
                FlatToggle1.Refresh()
            End If
        End If

        If FlatToggle1.Checked Then
            Autoclicker.Start()
        Else
            Autoclicker.Stop()
        End If

        If FlatButton2.Text = ">...<" Then
            If Key = Keys.Escape Then
                FlatButton2.Text = "Autoclicker"
                FlatButton2.Refresh()
                ClickerBind = Keys.None
            Else
                FlatButton2.Text = "(" + Key.ToString + ") " + "Autoclicker"
                FlatButton2.Refresh()
                ClickerBind = Key
            End If
        End If




        If FlatButton8.Text = ">...<" Then
            If Key = Keys.Escape Then
                FlatButton8.Text = "Hide"
                FlatButton8.Refresh()
                HideBind = Keys.None
            Else
                FlatButton8.Text = "(" + Key.ToString + ") " + "Hide"
                FlatButton8.Refresh()
                HideBind = Key
            End If
        End If


        If FlatButton12.Text = "> ... <" Then
            BindLave = Key
            FlatButton12.Text = "(" + Key.ToString + ") " + "Lava Bucket"
            If Key = Keys.Escape Then
                lavbind = False
                FlatButton12.Text = "Lava Bucket"
                Dim BindLave As Keys = Keys.None
                FlatButton12.Refresh()
            End If
        End If

        If FlatButton14.Text = "> ... <" Then
            BindWater = Key
            FlatButton14.Text = "(" + Key.ToString + ") " + "Water Bucket"
            If Key = Keys.Escape Then
                watbind = False
                FlatButton14.Text = "Water Bucket"
                Dim BindWater As Keys = Keys.None
                FlatButton14.Refresh()
            End If
        End If

        If FlatButton16.Text = "> ... <" Then
            BindPot = Key
            FlatButton16.Text = "(" + Key.ToString + ") " + "Potions"
            If Key = Keys.Escape Then
                pottbind = False
                FlatButton16.Text = "Potions"
                Dim BindPot As Keys = Keys.None
                FlatButton16.Refresh()
            End If
        End If


        If Key = (dictionary(invkey)) Then
            If BunifuCheckbox10.Checked Then
                slot = 0
            End If
        End If

        FlatButton10.Refresh()
        FlatButton16.Refresh()
        FlatButton14.Refresh()
        FlatButton12.Refresh()

    End Sub


    Sub GetMcOptions()
        Dim FILE_NAME As String = Environ("USERPROFILE") & "\AppData\Roaming\.minecraft\options.txt"
        Dim findstring = System.IO.File.ReadAllText(FILE_NAME)

        If findstring.Contains("key_key.drop:16") Then
            dropkey = "q"
        End If
        If findstring.Contains("key_key.drop:17") Then
            dropkey = "w"
        End If
        If findstring.Contains("key_key.drop:18") Then
            dropkey = "e"
        End If
        If findstring.Contains("key_key.drop:19") Then
            dropkey = "r"
        End If
        If findstring.Contains("key_key.drop:20") Then
            dropkey = "t"
        End If
        If findstring.Contains("key_key.drop:21") Then
            dropkey = "y"
        End If
        If findstring.Contains("key_key.drop:22") Then
            dropkey = "u"
        End If
        If findstring.Contains("key_key.drop:23") Then
            dropkey = "i"
        End If
        If findstring.Contains("key_key.drop:24") Then
            dropkey = "o"
        End If
        If findstring.Contains("key_key.drop:25") Then
            dropkey = "p"
        End If
        If findstring.Contains("key_key.drop:30") Then
            dropkey = "a"
        End If
        If findstring.Contains("key_key.drop:31") Then
            dropkey = "s"
        End If
        If findstring.Contains("key_key.drop:32") Then
            dropkey = "d"
        End If
        If findstring.Contains("key_key.drop:33") Then
            dropkey = "f"
        End If
        If findstring.Contains("key_key.drop:34") Then
            dropkey = "g"
        End If
        If findstring.Contains("key_key.drop:35") Then
            dropkey = "h"
        End If
        If findstring.Contains("key_key.drop:36") Then
            dropkey = "j"
        End If
        If findstring.Contains("key_key.drop:37") Then
            dropkey = "k"
        End If
        If findstring.Contains("key_key.drop:38") Then
            dropkey = "l"
        End If
        If findstring.Contains("key_key.drop:44") Then
            dropkey = "z"
        End If
        If findstring.Contains("key_key.drop:45") Then
            dropkey = "x"
        End If
        If findstring.Contains("key_key.drop:46") Then
            dropkey = "c"
        End If
        If findstring.Contains("key_key.drop:47") Then
            dropkey = "v"
        End If
        If findstring.Contains("key_key.drop:48") Then
            dropkey = "b"
        End If
        If findstring.Contains("key_key.drop:49") Then
            dropkey = "n"
        End If
        If findstring.Contains("key_key.drop:50") Then
            dropkey = "m"
        End If
        If findstring.Contains("key_key.drop:15") Then
            dropkey = "tab"
        End If
        If findstring.Contains("key_key.drop:29") Then
            dropkey = "leftctrl"
        End If
        If findstring.Contains("key_key.drop:49") Then
            dropkey = "leftshift"
        End If
        If findstring.Contains("key_key.drop:54") Then
            dropkey = "rightshift"
        End If
        If findstring.Contains("key_key.drop:56") Then
            dropkey = "leftalt"
        End If
        If findstring.Contains("key_key.drop:98") Then
            dropkey = "rightalt"
        End If
        If findstring.Contains("key_key.drop:99") Then
            dropkey = "rightctrl"
        End If






        If findstring.Contains("key_key.forward:16") Then
            forwardkey = "q"
        End If
        If findstring.Contains("key_key.forward:17") Then
            forwardkey = "w"
        End If
        If findstring.Contains("key_key.forward:18") Then
            forwardkey = "e"
        End If
        If findstring.Contains("key_key.forward:19") Then
            forwardkey = "r"
        End If
        If findstring.Contains("key_key.forward:20") Then
            forwardkey = "t"
        End If
        If findstring.Contains("key_key.forward:21") Then
            forwardkey = "y"
        End If
        If findstring.Contains("key_key.forward:22") Then
            forwardkey = "u"
        End If
        If findstring.Contains("key_key.forward:23") Then
            forwardkey = "i"
        End If
        If findstring.Contains("key_key.forward:24") Then
            forwardkey = "o"
        End If
        If findstring.Contains("key_key.forward:25") Then
            forwardkey = "p"
        End If
        If findstring.Contains("key_key.forward:30") Then
            forwardkey = "a"
        End If
        If findstring.Contains("key_key.forward:31") Then
            forwardkey = "s"
        End If
        If findstring.Contains("key_key.forward:32") Then
            forwardkey = "d"
        End If
        If findstring.Contains("key_key.forward:33") Then
            forwardkey = "f"
        End If
        If findstring.Contains("key_key.forward:34") Then
            forwardkey = "g"
        End If
        If findstring.Contains("key_key.forward:35") Then
            forwardkey = "h"
        End If
        If findstring.Contains("key_key.forward:36") Then
            forwardkey = "j"
        End If
        If findstring.Contains("key_key.forward:37") Then
            forwardkey = "k"
        End If
        If findstring.Contains("key_key.forward:38") Then
            forwardkey = "l"
        End If
        If findstring.Contains("key_key.forward:44") Then
            forwardkey = "z"
        End If
        If findstring.Contains("key_key.forward:45") Then
            forwardkey = "x"
        End If
        If findstring.Contains("key_key.forward:46") Then
            forwardkey = "c"
        End If
        If findstring.Contains("key_key.forward:47") Then
            forwardkey = "v"
        End If
        If findstring.Contains("key_key.forward:48") Then
            forwardkey = "b"
        End If
        If findstring.Contains("key_key.forward:49") Then
            forwardkey = "n"
        End If
        If findstring.Contains("key_key.forward:50") Then
            forwardkey = "m"
        End If
        If findstring.Contains("key_key.forward:15") Then
            forwardkey = "tab"
        End If
        If findstring.Contains("key_key.forward:29") Then
            forwardkey = "leftctrl"
        End If
        If findstring.Contains("key_key.forward:49") Then
            forwardkey = "leftshift"
        End If
        If findstring.Contains("key_key.forward:54") Then
            forwardkey = "rightshift"
        End If
        If findstring.Contains("key_key.forward:56") Then
            forwardkey = "leftalt"
        End If
        If findstring.Contains("key_key.forward:98") Then
            forwardkey = "rightalt"
        End If
        If findstring.Contains("key_key.forward:99") Then
            forwardkey = "rightctrl"
        End If






        If findstring.Contains("key_key.sprint:16") Then
            sprintkey = "q"
        End If
        If findstring.Contains("key_key.sprint:17") Then
            sprintkey = "w"
        End If
        If findstring.Contains("key_key.sprint:18") Then
            sprintkey = "e"
        End If
        If findstring.Contains("key_key.sprint:19") Then
            sprintkey = "r"
        End If
        If findstring.Contains("key_key.sprint:20") Then
            sprintkey = "t"
        End If
        If findstring.Contains("key_key.sprint:21") Then
            sprintkey = "y"
        End If
        If findstring.Contains("key_key.sprint:22") Then
            sprintkey = "u"
        End If
        If findstring.Contains("key_key.sprint:23") Then
            sprintkey = "i"
        End If
        If findstring.Contains("key_key.sprint:24") Then
            sprintkey = "o"
        End If
        If findstring.Contains("key_key.sprint:25") Then
            sprintkey = "p"
        End If
        If findstring.Contains("key_key.sprint:30") Then
            sprintkey = "a"
        End If
        If findstring.Contains("key_key.sprint:31") Then
            sprintkey = "s"
        End If
        If findstring.Contains("key_key.sprint:32") Then
            sprintkey = "d"
        End If
        If findstring.Contains("key_key.sprint:33") Then
            sprintkey = "f"
        End If
        If findstring.Contains("key_key.sprint:34") Then
            sprintkey = "g"
        End If
        If findstring.Contains("key_key.sprint:35") Then
            sprintkey = "h"
        End If
        If findstring.Contains("key_key.sprint:36") Then
            sprintkey = "j"
        End If
        If findstring.Contains("key_key.sprint:37") Then
            sprintkey = "k"
        End If
        If findstring.Contains("key_key.sprint:38") Then
            sprintkey = "l"
        End If
        If findstring.Contains("key_key.sprint:44") Then
            sprintkey = "z"
        End If
        If findstring.Contains("key_key.sprint:45") Then
            sprintkey = "x"
        End If
        If findstring.Contains("key_key.sprint:46") Then
            sprintkey = "c"
        End If
        If findstring.Contains("key_key.sprint:47") Then
            sprintkey = "v"
        End If
        If findstring.Contains("key_key.sprint:48") Then
            sprintkey = "b"
        End If
        If findstring.Contains("key_key.sprint:49") Then
            sprintkey = "n"
        End If
        If findstring.Contains("key_key.sprint:50") Then
            sprintkey = "m"
        End If
        If findstring.Contains("key_key.sprint:15") Then
            sprintkey = "tab"
        End If
        If findstring.Contains("key_key.sprint:29") Then
            sprintkey = "leftctrl"
        End If
        If findstring.Contains("key_key.sprint:49") Then
            sprintkey = "leftshift"
        End If
        If findstring.Contains("key_key.sprint:54") Then
            sprintkey = "rightshift"
        End If
        If findstring.Contains("key_key.sprint:56") Then
            sprintkey = "leftalt"
        End If
        If findstring.Contains("key_key.sprint:98") Then
            sprintkey = "rightalt"
        End If
        If findstring.Contains("key_key.sprint:99") Then
            sprintkey = "rightctrl"
        End If



        If findstring.Contains("key_key.inventory:16") Then
            invkey = "q"
        End If
        If findstring.Contains("key_key.inventory:17") Then
            invkey = "w"
        End If
        If findstring.Contains("key_key.inventory:18") Then
            invkey = "e"
        End If
        If findstring.Contains("key_key.inventory:19") Then
            invkey = "r"
        End If
        If findstring.Contains("key_key.inventory:20") Then
            invkey = "t"
        End If
        If findstring.Contains("key_key.inventory:21") Then
            invkey = "y"
        End If
        If findstring.Contains("key_key.inventory:22") Then
            invkey = "u"
        End If
        If findstring.Contains("key_key.inventory:23") Then
            invkey = "i"
        End If
        If findstring.Contains("key_key.inventory:24") Then
            invkey = "o"
        End If
        If findstring.Contains("key_key.inventory:25") Then
            invkey = "p"
        End If
        If findstring.Contains("key_key.inventory:30") Then
            invkey = "a"
        End If
        If findstring.Contains("key_key.inventory:31") Then
            invkey = "s"
        End If
        If findstring.Contains("key_key.inventory:32") Then
            invkey = "d"
        End If
        If findstring.Contains("key_key.inventory:33") Then
            invkey = "f"
        End If
        If findstring.Contains("key_key.inventory:34") Then
            invkey = "g"
        End If
        If findstring.Contains("key_key.inventory:35") Then
            invkey = "h"
        End If
        If findstring.Contains("key_key.inventory:36") Then
            invkey = "j"
        End If
        If findstring.Contains("key_key.inventory:37") Then
            invkey = "k"
        End If
        If findstring.Contains("key_key.inventory:38") Then
            invkey = "l"
        End If
        If findstring.Contains("key_key.inventory:44") Then
            invkey = "z"
        End If
        If findstring.Contains("key_key.inventory:45") Then
            invkey = "x"
        End If
        If findstring.Contains("key_key.inventory:46") Then
            invkey = "c"
        End If
        If findstring.Contains("key_key.inventory:47") Then
            invkey = "v"
        End If
        If findstring.Contains("key_key.inventory:48") Then
            invkey = "b"
        End If
        If findstring.Contains("key_key.inventory:49") Then
            invkey = "n"
        End If
        If findstring.Contains("key_key.inventory:50") Then
            invkey = "m"
        End If
        If findstring.Contains("key_key.inventory:15") Then
            invkey = "tab"
        End If
        If findstring.Contains("key_key.inventory:29") Then
            invkey = "leftctrl"
        End If
        If findstring.Contains("key_key.inventory:49") Then
            invkey = "leftshift"
        End If
        If findstring.Contains("key_key.inventory:54") Then
            invkey = "rightshift"
        End If
        If findstring.Contains("key_key.inventory:56") Then
            invkey = "leftalt"
        End If
        If findstring.Contains("key_key.inventory:98") Then
            invkey = "rightalt"
        End If
        If findstring.Contains("key_key.inventory:99") Then
            invkey = "rightctrl"
        End If




        If findstring.Contains("key_key.chat:16") Then
            chatkey = "q"
        End If
        If findstring.Contains("key_key.chat:17") Then
            chatkey = "w"
        End If
        If findstring.Contains("key_key.chat:18") Then
            chatkey = "e"
        End If
        If findstring.Contains("key_key.chat:19") Then
            chatkey = "r"
        End If
        If findstring.Contains("key_key.chat:20") Then
            chatkey = "t"
        End If
        If findstring.Contains("key_key.chat:21") Then
            chatkey = "y"
        End If
        If findstring.Contains("key_key.chat:22") Then
            chatkey = "u"
        End If
        If findstring.Contains("key_key.chat:23") Then
            chatkey = "i"
        End If
        If findstring.Contains("key_key.chat:24") Then
            chatkey = "o"
        End If
        If findstring.Contains("key_key.chat:25") Then
            chatkey = "p"
        End If
        If findstring.Contains("key_key.chat:30") Then
            chatkey = "a"
        End If
        If findstring.Contains("key_key.chat:31") Then
            chatkey = "s"
        End If
        If findstring.Contains("key_key.chat:32") Then
            chatkey = "d"
        End If
        If findstring.Contains("key_key.chat:33") Then
            chatkey = "f"
        End If
        If findstring.Contains("key_key.chat:34") Then
            chatkey = "g"
        End If
        If findstring.Contains("key_key.chat:35") Then
            chatkey = "h"
        End If
        If findstring.Contains("key_key.chat:36") Then
            chatkey = "j"
        End If
        If findstring.Contains("key_key.chat:37") Then
            chatkey = "k"
        End If
        If findstring.Contains("key_key.chat:38") Then
            chatkey = "l"
        End If
        If findstring.Contains("key_key.chat:44") Then
            chatkey = "z"
        End If
        If findstring.Contains("key_key.chat:45") Then
            chatkey = "x"
        End If
        If findstring.Contains("key_key.chat:46") Then
            chatkey = "c"
        End If
        If findstring.Contains("key_key.chat:47") Then
            chatkey = "v"
        End If
        If findstring.Contains("key_key.chat:48") Then
            chatkey = "b"
        End If
        If findstring.Contains("key_key.chat:49") Then
            chatkey = "n"
        End If
        If findstring.Contains("key_key.chat:50") Then
            chatkey = "m"
        End If
        If findstring.Contains("key_key.chat:15") Then
            chatkey = "tab"
        End If
        If findstring.Contains("key_key.chat:29") Then
            chatkey = "leftctrl"
        End If
        If findstring.Contains("key_key.chat:49") Then
            chatkey = "leftshift"
        End If
        If findstring.Contains("key_key.chat:54") Then
            chatkey = "rightshift"
        End If
        If findstring.Contains("key_key.chat:56") Then
            chatkey = "leftalt"
        End If
        If findstring.Contains("key_key.chat:98") Then
            chatkey = "rightalt"
        End If
        If findstring.Contains("key_key.chat:99") Then
            chatkey = "rightctrl"
        End If



        If findstring.Contains("key_key.back:16") Then
            backwardkey = "q"
        End If
        If findstring.Contains("key_key.back:17") Then
            backwardkey = "w"
        End If
        If findstring.Contains("key_key.back:18") Then
            backwardkey = "e"
        End If
        If findstring.Contains("key_key.back:19") Then
            backwardkey = "r"
        End If
        If findstring.Contains("key_key.back:20") Then
            backwardkey = "t"
        End If
        If findstring.Contains("key_key.back:21") Then
            backwardkey = "y"
        End If
        If findstring.Contains("key_key.back:22") Then
            backwardkey = "u"
        End If
        If findstring.Contains("key_key.back:23") Then
            backwardkey = "i"
        End If
        If findstring.Contains("key_key.back:24") Then
            backwardkey = "o"
        End If
        If findstring.Contains("key_key.back:25") Then
            backwardkey = "p"
        End If
        If findstring.Contains("key_key.back:30") Then
            backwardkey = "a"
        End If
        If findstring.Contains("key_key.back:31") Then
            backwardkey = "s"
        End If
        If findstring.Contains("key_key.back:32") Then
            backwardkey = "d"
        End If
        If findstring.Contains("key_key.back:33") Then
            backwardkey = "f"
        End If
        If findstring.Contains("key_key.back:34") Then
            backwardkey = "g"
        End If
        If findstring.Contains("key_key.back:35") Then
            backwardkey = "h"
        End If
        If findstring.Contains("key_key.back:36") Then
            backwardkey = "j"
        End If
        If findstring.Contains("key_key.back:37") Then
            backwardkey = "k"
        End If
        If findstring.Contains("key_key.back:38") Then
            backwardkey = "l"
        End If
        If findstring.Contains("key_key.back:44") Then
            backwardkey = "z"
        End If
        If findstring.Contains("key_key.back:45") Then
            backwardkey = "x"
        End If
        If findstring.Contains("key_key.back:46") Then
            backwardkey = "c"
        End If
        If findstring.Contains("key_key.back:47") Then
            backwardkey = "v"
        End If
        If findstring.Contains("key_key.back:48") Then
            backwardkey = "b"
        End If
        If findstring.Contains("key_key.back:49") Then
            backwardkey = "n"
        End If
        If findstring.Contains("key_key.back:50") Then
            backwardkey = "m"
        End If
        If findstring.Contains("key_key.back:15") Then
            backwardkey = "tab"
        End If
        If findstring.Contains("key_key.back:29") Then
            backwardkey = "leftctrl"
        End If
        If findstring.Contains("key_key.back:49") Then
            backwardkey = "leftshift"
        End If
        If findstring.Contains("key_key.back:54") Then
            backwardkey = "rightshift"
        End If
        If findstring.Contains("key_key.back:56") Then
            backwardkey = "leftalt"
        End If
        If findstring.Contains("key_key.back:98") Then
            backwardkey = "rightalt"
        End If
        If findstring.Contains("key_key.back:99") Then
            backwardkey = "rightctrl"
        End If

    End Sub

    Private Async Sub kbHook_KeyUp(ByVal Key As System.Windows.Forms.Keys) Handles kbHook.KeyUp
        Dim dictionary As New Dictionary(Of String, Keys)
        dictionary.Add("A", Keys.A)
        dictionary.Add("B", Keys.B)
        dictionary.Add("C", Keys.C)
        dictionary.Add("D", Keys.D)
        dictionary.Add("E", Keys.E)
        dictionary.Add("F", Keys.F)
        dictionary.Add("G", Keys.G)
        dictionary.Add("H", Keys.H)
        dictionary.Add("I", Keys.I)
        dictionary.Add("J", Keys.J)
        dictionary.Add("K", Keys.K)
        dictionary.Add("L", Keys.L)
        dictionary.Add("M", Keys.M)
        dictionary.Add("N", Keys.N)
        dictionary.Add("O", Keys.O)
        dictionary.Add("P", Keys.P)
        dictionary.Add("Q", Keys.Q)
        dictionary.Add("R", Keys.R)
        dictionary.Add("S", Keys.S)
        dictionary.Add("T", Keys.T)
        dictionary.Add("U", Keys.U)
        dictionary.Add("V", Keys.V)
        dictionary.Add("W", Keys.W)
        dictionary.Add("X", Keys.X)
        dictionary.Add("Y", Keys.Y)
        dictionary.Add("Z", Keys.Z)
        dictionary.Add("a", Keys.A)
        dictionary.Add("b", Keys.B)
        dictionary.Add("c", Keys.C)
        dictionary.Add("d", Keys.D)
        dictionary.Add("e", Keys.E)
        dictionary.Add("f", Keys.F)
        dictionary.Add("g", Keys.G)
        dictionary.Add("h", Keys.H)
        dictionary.Add("i", Keys.I)
        dictionary.Add("j", Keys.J)
        dictionary.Add("k", Keys.K)
        dictionary.Add("l", Keys.L)
        dictionary.Add("m", Keys.M)
        dictionary.Add("n", Keys.N)
        dictionary.Add("o", Keys.O)
        dictionary.Add("p", Keys.P)
        dictionary.Add("q", Keys.Q)
        dictionary.Add("r", Keys.R)
        dictionary.Add("s", Keys.S)
        dictionary.Add("t", Keys.T)
        dictionary.Add("u", Keys.U)
        dictionary.Add("v", Keys.V)
        dictionary.Add("w", Keys.W)
        dictionary.Add("x", Keys.X)
        dictionary.Add("y", Keys.Y)
        dictionary.Add("z", Keys.Z)
        dictionary.Add("R-SHIFT", Keys.RShiftKey)
        If Key = Keys.None Then
        Else
            If Key = Keys.Escape Then
                dobinds = "Enabled"
            End If

            If Key = Keys.Enter Then
                dobinds = "Enabled"
            End If


            If chatkey = "" Then
            Else
                If Key = (dictionary(chatkey)) Then
                    dobinds = "Disabled"
                End If
            End If



            If Key = HideBind Then

            End If

            If Key = HideBind Then

            End If

            If dobinds = "Disabled" And BunifuCheckbox11.Checked = True Then

            Else
                If wait = False Then

                    If Key = BindPearl Then
                        endprl()
                    End If

                    If Key = BindPot Then
                        trwpot()
                    End If
                    If Key = BindLave Then
                        lavthrow()
                    End If
                    If Key = BindWater Then
                        watthrow()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub FlatButton5_Click(sender As Object, e As EventArgs)
        FlatButton2.Text = "Autoclicker"
        FlatButton8.Text = "Hide"
        Dim ClickerBind As Keys = Keys.None
        Dim PotBind As Keys = Keys.None
        Dim WTapBind As Keys = Keys.None
        Dim STapBind As Keys = Keys.None
        Dim BindPearl As Keys = Keys.None
        Dim HideBind As Keys = Keys.None
        Dim DestructBind As Keys = Keys.None
        FlatButton2.Refresh()
        FlatButton8.Refresh()
    End Sub


    Async Sub recale()
        BunifuCheckbox7.Location = New Point(16, FlatButton10.Location.Y + 21)
        Label27.Location = New Point(BunifuCheckbox7.Location.X + 21, BunifuCheckbox7.Location.Y + 2)
        ThirteenComboBox2.Location = New Point(16, BunifuCheckbox7.Location.Y + 23)
        ThirteenComboBox1.Location = New Point(16, ThirteenComboBox2.Location.Y + 23)


        BunifuCheckbox6.Location = New Point(16, FlatButton12.Location.Y + 21)
        Label29.Location = New Point(BunifuCheckbox6.Location.X + 21, BunifuCheckbox6.Location.Y + 2)
        ThirteenComboBox3.Location = New Point(16, BunifuCheckbox6.Location.Y + 23)
        ThirteenComboBox4.Location = New Point(16, ThirteenComboBox3.Location.Y + 23)


        BunifuCheckbox8.Location = New Point(16, FlatButton14.Location.Y + 21)
        Label30.Location = New Point(BunifuCheckbox8.Location.X + 21, BunifuCheckbox8.Location.Y + 2)
        ThirteenComboBox5.Location = New Point(16, BunifuCheckbox8.Location.Y + 23)
        ThirteenComboBox6.Location = New Point(16, ThirteenComboBox5.Location.Y + 23)


        BunifuCheckbox9.Location = New Point(16, FlatButton16.Location.Y + 21)
        Label31.Location = New Point(BunifuCheckbox9.Location.X + 21, BunifuCheckbox9.Location.Y + 2)
        ThirteenComboBox8.Location = New Point(85, BunifuCheckbox10.Location.Y + 23)
        Label33.Location = New Point(16, BunifuCheckbox10.Location.Y + 23)
        ThirteenComboBox7.Location = New Point(85, ThirteenComboBox8.Location.Y + 23)
        Label34.Location = New Point(16, ThirteenComboBox8.Location.Y + 23)
        ThirteenComboBox9.Location = New Point(85, ThirteenComboBox7.Location.Y + 23)
        Label35.Location = New Point(16, ThirteenComboBox7.Location.Y + 23)

        BunifuCheckbox10.Location = New Point(16, BunifuCheckbox9.Location.Y + 21)
        Label32.Location = New Point(BunifuCheckbox10.Location.X + 21, BunifuCheckbox10.Location.Y + 2)



        Await Task.Delay(1)
        BunifuCheckbox7.Location = New Point(16, FlatButton10.Location.Y + 21)
        Label27.Location = New Point(BunifuCheckbox7.Location.X + 21, BunifuCheckbox7.Location.Y + 2)
        ThirteenComboBox2.Location = New Point(16, BunifuCheckbox7.Location.Y + 23)
        ThirteenComboBox1.Location = New Point(16, ThirteenComboBox2.Location.Y + 23)


        BunifuCheckbox6.Location = New Point(16, FlatButton12.Location.Y + 21)
        Label29.Location = New Point(BunifuCheckbox6.Location.X + 21, BunifuCheckbox6.Location.Y + 2)
        ThirteenComboBox4.Location = New Point(16, BunifuCheckbox6.Location.Y + 23)
        ThirteenComboBox3.Location = New Point(16, ThirteenComboBox4.Location.Y + 23)


        BunifuCheckbox8.Location = New Point(16, FlatButton14.Location.Y + 21)
        Label30.Location = New Point(BunifuCheckbox8.Location.X + 21, BunifuCheckbox8.Location.Y + 2)
        ThirteenComboBox6.Location = New Point(16, BunifuCheckbox8.Location.Y + 23)
        ThirteenComboBox5.Location = New Point(16, ThirteenComboBox6.Location.Y + 23)


        BunifuCheckbox9.Location = New Point(16, FlatButton16.Location.Y + 21)
        Label31.Location = New Point(BunifuCheckbox9.Location.X + 21, BunifuCheckbox9.Location.Y + 2)
        ThirteenComboBox8.Location = New Point(85, BunifuCheckbox10.Location.Y + 23)
        Label33.Location = New Point(16, BunifuCheckbox10.Location.Y + 23)
        ThirteenComboBox7.Location = New Point(85, ThirteenComboBox8.Location.Y + 23)
        Label34.Location = New Point(16, ThirteenComboBox8.Location.Y + 23)
        ThirteenComboBox9.Location = New Point(85, ThirteenComboBox7.Location.Y + 23)
        Label35.Location = New Point(16, ThirteenComboBox7.Location.Y + 23)

        BunifuCheckbox10.Location = New Point(16, BunifuCheckbox9.Location.Y + 21)
        Label32.Location = New Point(BunifuCheckbox10.Location.X + 21, BunifuCheckbox10.Location.Y + 2)
    End Sub

    Private Sub FlatButton12_Click(sender As Object, e As EventArgs) Handles FlatButton12.Click
        If GetAsyncKeyState(Keys.LShiftKey) Then
            FlatButton12.Text = "> ... <"
            FlatButton12.Refresh()

        Else
            If Label29.Visible = False Then



                FlatButton14.Location = New Point(16, FlatButton14.Location.Y + 73)
                FlatButton13.Location = New Point(154, FlatButton13.Location.Y + 73)
                FlatButton16.Location = New Point(16, FlatButton16.Location.Y + 73)
                FlatButton15.Location = New Point(154, FlatButton15.Location.Y + 73)
                FlatGroupBox13.Size = New Size(187, FlatGroupBox13.Size.Height + 73)
                BunifuCheckbox6.Show()
                Label29.Show()
                ThirteenComboBox3.Show()
                ThirteenComboBox4.Show()
                FlatButton11.Text = "⯆"
                recale()
            Else
                FlatButton14.Location = New Point(16, FlatButton14.Location.Y - 73)
                FlatButton13.Location = New Point(154, FlatButton13.Location.Y - 73)
                FlatButton16.Location = New Point(16, FlatButton16.Location.Y - 73)
                FlatButton15.Location = New Point(154, FlatButton15.Location.Y - 73)
                FlatGroupBox13.Size = New Size(187, FlatGroupBox13.Size.Height - 73)
                BunifuCheckbox6.Hide()
                Label29.Hide()
                ThirteenComboBox3.Hide()
                ThirteenComboBox4.Hide()
                FlatButton11.Text = "⯇"
                recale()
            End If
        End If
    End Sub

    Private Sub FlatButton10_Click(sender As Object, e As EventArgs) Handles FlatButton10.Click
        If GetAsyncKeyState(Keys.LShiftKey) Then
            FlatButton10.Text = "> ... <"
            FlatButton10.Refresh()

        Else

            If Label27.Visible = False Then
                FlatButton12.Location = New Point(16, FlatButton12.Location.Y + 73)
                FlatButton11.Location = New Point(154, FlatButton11.Location.Y + 73)
                FlatButton14.Location = New Point(16, FlatButton14.Location.Y + 73)
                FlatButton13.Location = New Point(154, FlatButton13.Location.Y + 73)
                FlatButton16.Location = New Point(16, FlatButton16.Location.Y + 73)
                FlatButton15.Location = New Point(154, FlatButton15.Location.Y + 73)



                FlatGroupBox13.Size = New Size(187, FlatGroupBox13.Size.Height + 73)
                BunifuCheckbox7.Show()
                Label27.Show()
                ThirteenComboBox1.Show()
                ThirteenComboBox2.Show()
                FlatButton9.Text = "⯆"
                recale()
            Else
                FlatButton12.Location = New Point(16, FlatButton12.Location.Y - 73)
                FlatButton11.Location = New Point(154, FlatButton11.Location.Y - 73)
                FlatButton14.Location = New Point(16, FlatButton14.Location.Y - 73)
                FlatButton13.Location = New Point(154, FlatButton13.Location.Y - 73)
                FlatButton16.Location = New Point(16, FlatButton16.Location.Y - 73)
                FlatButton15.Location = New Point(154, FlatButton15.Location.Y - 73)
                FlatGroupBox13.Size = New Size(187, FlatGroupBox13.Size.Height - 73)
                BunifuCheckbox7.Hide()
                Label27.Hide()
                ThirteenComboBox1.Hide()
                ThirteenComboBox2.Hide()
                FlatButton9.Text = "⯇"
                recale()
            End If
        End If
    End Sub

    Private Sub FlatButton14_Click(sender As Object, e As EventArgs) Handles FlatButton14.Click
        If GetAsyncKeyState(Keys.LShiftKey) Then
            FlatButton14.Text = "> ... <"
            FlatButton14.Refresh()

        Else
            If Label30.Visible = False Then
                FlatButton16.Location = New Point(16, FlatButton16.Location.Y + 73)
                FlatButton15.Location = New Point(154, FlatButton15.Location.Y + 73)
                FlatGroupBox13.Size = New Size(187, FlatGroupBox13.Size.Height + 73)
                BunifuCheckbox8.Show()
                Label30.Show()
                ThirteenComboBox6.Show()
                ThirteenComboBox5.Show()
                FlatButton13.Text = "⯆"
                recale()
            Else
                FlatButton16.Location = New Point(16, FlatButton16.Location.Y - 73)
                FlatButton15.Location = New Point(154, FlatButton15.Location.Y - 73)
                FlatGroupBox13.Size = New Size(187, FlatGroupBox13.Size.Height - 73)
                BunifuCheckbox8.Hide()
                Label30.Hide()
                ThirteenComboBox6.Hide()
                ThirteenComboBox5.Hide()
                FlatButton13.Text = "⯇"
                recale()
            End If
        End If
    End Sub

    Private Sub FlatButton16_Click(sender As Object, e As EventArgs) Handles FlatButton16.Click
        If GetAsyncKeyState(Keys.LShiftKey) Then
            FlatButton16.Text = "> ... <"
            FlatButton16.Refresh()

        Else
            If Label31.Visible = False Then
                FlatGroupBox13.Size = New Size(187, FlatGroupBox13.Size.Height + 120)
                Label31.Show()
                ThirteenComboBox8.Show()
                ThirteenComboBox7.Show()
                ThirteenComboBox9.Show()
                Label32.Show()
                BunifuCheckbox10.Show()
                BunifuCheckbox9.Show()
                Label33.Show()
                Label34.Show()
                Label35.Show()
                FlatButton15.Text = "⯆"
                recale()
            Else
                FlatGroupBox13.Size = New Size(187, FlatGroupBox13.Size.Height - 120)
                BunifuCheckbox9.Hide()
                Label31.Hide()
                ThirteenComboBox8.Hide()
                ThirteenComboBox7.Hide()
                ThirteenComboBox9.Hide()
                Label32.Hide()
                BunifuCheckbox10.Hide()
                BunifuCheckbox9.Hide()
                Label33.Hide()
                Label34.Hide()
                Label35.Hide()
                FlatButton15.Text = "⯇"
                recale()
            End If
        End If
    End Sub

    Private Sub BunifuCheckbox12_OnChange(sender As Object, e As EventArgs) Handles BunifuCheckbox12.OnChange
        If BunifuCheckbox12.Checked Then
            Me.ShowInTaskbar = False
        Else
            Me.ShowInTaskbar = True
        End If
    End Sub

    Private Sub BunifuCheckbox13_OnChange(sender As Object, e As EventArgs) Handles BunifuCheckbox13.OnChange
        If BunifuCheckbox13.Checked Then
            Me.TopMost = True
        Else
            Me.TopMost = False
        End If
    End Sub

    Private Async Sub FlatButton19_Click(sender As Object, e As EventArgs) Handles FlatButton19.Click

        If FlatButton17.Text = "⯇" Then
            FlatButton18.Location = New Point(16, FlatButton18.Location.Y + 65)
            FlatButton1.Location = New Point(154, FlatButton1.Location.Y + 65)
            FlatButton4.Location = New Point(16, FlatButton4.Location.Y + 65)

            VisualStudioGroupBox1.Show()
            FlatGroupBox12.Size = New Size(FlatGroupBox12.Size.Width, FlatGroupBox12.Size.Height + 65)
            FlatButton17.Text = "⯆"
            FlatButton17.Refresh()
            Await Task.Delay(1)
            VisualStudioGroupBox1.Location = New Size(16, FlatButton19.Location.Y + 17)
            VisualStudioGroupBox2.Location = New Size(16, FlatButton18.Location.Y + 17)
        Else
            FlatButton18.Location = New Point(16, FlatButton18.Location.Y - 65)
            FlatButton1.Location = New Point(154, FlatButton1.Location.Y - 65)
            FlatButton4.Location = New Point(16, FlatButton4.Location.Y - 65)
            FlatGroupBox12.Size = New Size(187, FlatGroupBox12.Size.Height - 65)
            VisualStudioGroupBox1.Hide()
            FlatButton17.Text = "⯇"
            FlatButton17.Refresh()
            Await Task.Delay(1)
            VisualStudioGroupBox1.Location = New Size(16, FlatButton19.Location.Y + 17)
            VisualStudioGroupBox2.Location = New Size(16, FlatButton18.Location.Y + 17)
        End If
    End Sub

    Private Sub FlatGroupBox13_Click(sender As Object, e As EventArgs) Handles FlatGroupBox13.Click

    End Sub

    Private Async Sub FlatButton18_Click(sender As Object, e As EventArgs) Handles FlatButton18.Click
        If FlatButton1.Text = "⯇" Then
            FlatButton4.Location = New Point(16, FlatButton4.Location.Y + 65)

            VisualStudioGroupBox2.Show()
            FlatGroupBox12.Size = New Size(FlatGroupBox12.Size.Width, FlatGroupBox12.Size.Height + 65)
            FlatButton1.Text = "⯆"
            FlatButton1.Refresh()
            Await Task.Delay(1)
            VisualStudioGroupBox1.Location = New Size(16, FlatButton19.Location.Y + 17)
            VisualStudioGroupBox2.Location = New Size(16, FlatButton18.Location.Y + 17)
        Else
            FlatButton4.Location = New Point(16, FlatButton4.Location.Y - 65)
            FlatGroupBox12.Size = New Size(187, FlatGroupBox12.Size.Height - 65)
            VisualStudioGroupBox2.Hide()
            FlatButton1.Text = "⯇"
            FlatButton1.Refresh()
            Await Task.Delay(1)
            VisualStudioGroupBox1.Location = New Size(16, FlatButton19.Location.Y + 17)
            VisualStudioGroupBox2.Location = New Size(16, FlatButton18.Location.Y + 17)
        End If
    End Sub

    Private Sub FlatButton4_Click(sender As Object, e As EventArgs) Handles FlatButton4.Click
        If FlatButton4.BaseColor = Color.SandyBrown Then
            FlatButton4.BaseColor = Color.FromArgb(40, 40, 40)
        Else
            FlatButton4.BaseColor = Color.SandyBrown
        End If
    End Sub

    Private Sub FlatToggle6_CheckedChanged(sender As Object) Handles FlatToggle6.CheckedChanged
        If FlatToggle6.Checked Then
            FlatButton19.BaseColor = Color.SandyBrown
            FlatButton17.BaseColor = Color.SandyBrown
            FlatButton17.Refresh()
            FlatButton19.Refresh()
        Else
            FlatButton19.BaseColor = Color.FromArgb(40, 40, 40)
            FlatButton17.BaseColor = Color.FromArgb(40, 40, 40)
            FlatButton17.Refresh()
            FlatButton19.Refresh()
        End If
    End Sub

    Private Sub FlatToggle7_CheckedChanged(sender As Object) Handles FlatToggle7.CheckedChanged
        If FlatToggle7.Checked Then
            FlatButton18.BaseColor = Color.SandyBrown
            FlatButton1.BaseColor = Color.SandyBrown
            FlatButton1.Refresh()
            FlatButton18.Refresh()
        Else
            FlatButton18.BaseColor = Color.FromArgb(40, 40, 40)
            FlatButton1.BaseColor = Color.FromArgb(40, 40, 40)
            FlatButton1.Refresh()
            FlatButton18.Refresh()
        End If
    End Sub

    Private Sub BunifuCheckbox14_OnChange(sender As Object, e As EventArgs) Handles BunifuCheckbox14.OnChange
        If BunifuCheckbox14.Checked = True Then
            backover.Show()

        Else
            backover.Hide()
            clickguy.Hide()
        End If
    End Sub

    Private Sub FlatButton2_Click(sender As Object, e As EventArgs) Handles FlatButton2.Click
        FlatButton2.Text = ">...<"
    End Sub

    Private Sub FlatButton8_Click(sender As Object, e As EventArgs) Handles FlatButton8.Click
        FlatButton8.Text = ">...<"
    End Sub

    Private Sub BunifuFlatButton1_Click(sender As Object, e As EventArgs) Handles BunifuFlatButton1.Click

    End Sub
End Class