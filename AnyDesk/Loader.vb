Imports System.IO
Imports System.Management
Imports System.Net
Imports Microsoft.Win32
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Loader
    Dim loc As Integer = -8
    Dim r As Integer = 198
    Dim g As Integer = 150
    Dim b As Integer = 74
    Dim rockload As Integer = 446
    Dim loadnumb As Integer = 0
    Dim result As String


    Private Sub Loader_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Function GetDriveSerialNumber() As String
        Dim DriveSerial As Integer
        'Create a FileSystemObject object
        Dim fso As Object = CreateObject("Scripting.FileSystemObject")
        Dim Drv As Object = fso.GetDrive(fso.GetDriveName(Application.StartupPath))
        With Drv
            If .IsReady Then
                DriveSerial = .SerialNumber
            Else    '"Drive Not Ready!"
                DriveSerial = -1
            End If
        End With
        Return DriveSerial.ToString("X2")
    End Function



    Private Sub Load_Tick(sender As Object, e As EventArgs) Handles Load.Tick

        If loadnumb = 0 Then
            FlatLabel1.Text = "Authenticating..."



        End If
        If loadnumb = 10 Then
            FlatLabel1.Text = "Authenticated."



        End If
        If loadnumb = 20 Then
            FlatLabel1.Text = "Fetching Data..."

        End If
        If loadnumb = 40 Then
            FlatLabel1.Text = "Data Fetched."

        End If
        If loadnumb = 50 Then
            FlatLabel1.Text = "Loading."

        End If
        If loadnumb = 60 Then
            FlatLabel1.Text = "Loading.."

        End If
        If loadnumb = 70 Then
            FlatLabel1.Text = "Loading..."

        End If
        If loadnumb = 80 Then
            FlatLabel1.Text = "Loading."

        End If
        If loadnumb = 100 Then
            FlatLabel1.Text = "Loaded!"

        End If

        If loadnumb = 100 Then
            Me.Hide()
            Main.Show()
        Else
            loadnumb = loadnumb + 1
            BunifuProgressBar1.Value = loadnumb

        End If

    End Sub

    Private Sub Loader_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim hi As String = "https://auth.apolloclicker.pw/" + GetDriveSerialNumber()
        Dim myRequest As HttpWebRequest = DirectCast(WebRequest.Create(hi), HttpWebRequest)
        myRequest.UserAgent = "ApolloClicker/3.1 (VB Version :/ )"
        myRequest.Method = "GET"
        Dim myResponse As WebResponse = myRequest.GetResponse()
        Dim sr As New StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8)
        result = sr.ReadToEnd()

        If result.Contains("""Valid"": true") Then
            Load.Start()
        Else
            MsgBox("Unauthorised.")
            Application.Exit()
        End If
    End Sub
End Class
