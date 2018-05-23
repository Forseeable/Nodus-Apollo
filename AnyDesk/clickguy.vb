Public Class clickguy
    Private Sub clickguy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Me.TopMost = True
        Me.Text = Main.BunifuMetroTextbox1.Text
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Size = backover.Size
        Me.Location = backover.Location
    End Sub

    Private Sub FlatGroupBox1_Click(sender As Object, e As EventArgs) Handles FlatGroupBox1.Click

    End Sub

    Private Sub FlatGroupBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles FlatGroupBox1.MouseMove
        Static lpos As Point
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            FlatGroupBox1.Location += New Size(e.X - lpos.X, e.Y - lpos.Y)
        Else
            lpos = e.Location
        End If
    End Sub

    Private Sub FlatGroupBox14_Click(sender As Object, e As EventArgs) Handles FlatGroupBox14.Click

    End Sub

    Private Sub FlatGroupBox14_MouseMove(sender As Object, e As MouseEventArgs) Handles FlatGroupBox14.MouseMove
        Static lpos As Point
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            FlatGroupBox14.Location += New Size(e.X - lpos.X, e.Y - lpos.Y)
        Else
            lpos = e.Location
        End If
    End Sub
End Class