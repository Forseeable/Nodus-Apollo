<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class clickguy
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.FlatGroupBox1 = New AnyDesk.FlatGroupBox()
        Me.FlatGroupBox14 = New AnyDesk.FlatGroupBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.BunifuCheckbox13 = New Bunifu.Framework.UI.BunifuCheckbox()
        Me.BunifuCheckbox12 = New Bunifu.Framework.UI.BunifuCheckbox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.BunifuCheckbox11 = New Bunifu.Framework.UI.BunifuCheckbox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.FlatGroupBox14.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'FlatGroupBox1
        '
        Me.FlatGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.FlatGroupBox1.BaseColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.FlatGroupBox1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FlatGroupBox1.Location = New System.Drawing.Point(82, 99)
        Me.FlatGroupBox1.Name = "FlatGroupBox1"
        Me.FlatGroupBox1.ShowText = True
        Me.FlatGroupBox1.Size = New System.Drawing.Size(240, 53)
        Me.FlatGroupBox1.TabIndex = 0
        Me.FlatGroupBox1.Text = "Modules                                     ⯆"
        '
        'FlatGroupBox14
        '
        Me.FlatGroupBox14.BackColor = System.Drawing.Color.Transparent
        Me.FlatGroupBox14.BaseColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.FlatGroupBox14.Controls.Add(Me.Label38)
        Me.FlatGroupBox14.Controls.Add(Me.BunifuCheckbox13)
        Me.FlatGroupBox14.Controls.Add(Me.BunifuCheckbox12)
        Me.FlatGroupBox14.Controls.Add(Me.Label37)
        Me.FlatGroupBox14.Controls.Add(Me.BunifuCheckbox11)
        Me.FlatGroupBox14.Controls.Add(Me.Label36)
        Me.FlatGroupBox14.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FlatGroupBox14.Location = New System.Drawing.Point(399, 121)
        Me.FlatGroupBox14.Name = "FlatGroupBox14"
        Me.FlatGroupBox14.ShowText = True
        Me.FlatGroupBox14.Size = New System.Drawing.Size(254, 130)
        Me.FlatGroupBox14.TabIndex = 16
        Me.FlatGroupBox14.Text = "Misc                                               ⯆"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.Label38.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.Label38.ForeColor = System.Drawing.Color.White
        Me.Label38.Location = New System.Drawing.Point(38, 93)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(80, 13)
        Me.Label38.TabIndex = 55
        Me.Label38.Text = "Always on Top"
        '
        'BunifuCheckbox13
        '
        Me.BunifuCheckbox13.BackColor = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.BunifuCheckbox13.ChechedOffColor = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.BunifuCheckbox13.Checked = False
        Me.BunifuCheckbox13.CheckedOnColor = System.Drawing.Color.SandyBrown
        Me.BunifuCheckbox13.ForeColor = System.Drawing.Color.White
        Me.BunifuCheckbox13.Location = New System.Drawing.Point(18, 90)
        Me.BunifuCheckbox13.Margin = New System.Windows.Forms.Padding(3, 35, 3, 35)
        Me.BunifuCheckbox13.Name = "BunifuCheckbox13"
        Me.BunifuCheckbox13.Size = New System.Drawing.Size(20, 20)
        Me.BunifuCheckbox13.TabIndex = 54
        '
        'BunifuCheckbox12
        '
        Me.BunifuCheckbox12.BackColor = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.BunifuCheckbox12.ChechedOffColor = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.BunifuCheckbox12.Checked = False
        Me.BunifuCheckbox12.CheckedOnColor = System.Drawing.Color.SandyBrown
        Me.BunifuCheckbox12.ForeColor = System.Drawing.Color.White
        Me.BunifuCheckbox12.Location = New System.Drawing.Point(18, 66)
        Me.BunifuCheckbox12.Margin = New System.Windows.Forms.Padding(3, 27, 3, 27)
        Me.BunifuCheckbox12.Name = "BunifuCheckbox12"
        Me.BunifuCheckbox12.Size = New System.Drawing.Size(20, 20)
        Me.BunifuCheckbox12.TabIndex = 52
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.Label37.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.Label37.ForeColor = System.Drawing.Color.White
        Me.Label37.Location = New System.Drawing.Point(38, 69)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(85, 13)
        Me.Label37.TabIndex = 53
        Me.Label37.Text = "Hide In TaskBar"
        '
        'BunifuCheckbox11
        '
        Me.BunifuCheckbox11.BackColor = System.Drawing.Color.SandyBrown
        Me.BunifuCheckbox11.ChechedOffColor = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.BunifuCheckbox11.Checked = True
        Me.BunifuCheckbox11.CheckedOnColor = System.Drawing.Color.SandyBrown
        Me.BunifuCheckbox11.ForeColor = System.Drawing.Color.White
        Me.BunifuCheckbox11.Location = New System.Drawing.Point(18, 42)
        Me.BunifuCheckbox11.Margin = New System.Windows.Forms.Padding(3, 21, 3, 21)
        Me.BunifuCheckbox11.Name = "BunifuCheckbox11"
        Me.BunifuCheckbox11.Size = New System.Drawing.Size(20, 20)
        Me.BunifuCheckbox11.TabIndex = 50
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.Label36.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.Label36.ForeColor = System.Drawing.Color.White
        Me.Label36.Location = New System.Drawing.Point(38, 45)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(113, 13)
        Me.Label36.TabIndex = 51
        Me.Label36.Text = "Ignore Binds In Chat"
        '
        'clickguy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.FlatGroupBox14)
        Me.Controls.Add(Me.FlatGroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "clickguy"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "clickguy"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.FlatGroupBox14.ResumeLayout(False)
        Me.FlatGroupBox14.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents FlatGroupBox1 As FlatGroupBox
    Friend WithEvents FlatGroupBox14 As FlatGroupBox
    Friend WithEvents Label38 As Label
    Friend WithEvents BunifuCheckbox13 As Bunifu.Framework.UI.BunifuCheckbox
    Friend WithEvents BunifuCheckbox12 As Bunifu.Framework.UI.BunifuCheckbox
    Friend WithEvents Label37 As Label
    Friend WithEvents BunifuCheckbox11 As Bunifu.Framework.UI.BunifuCheckbox
    Friend WithEvents Label36 As Label
End Class
