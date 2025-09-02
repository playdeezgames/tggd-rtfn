Imports TGGD.UI
Imports TGGDRTFN.Business

Friend Class TitleState
    Inherits BaseState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String))
        MyBase.New(buffer, world, playSfx)
    End Sub

    Public Overrides Sub Refresh()
        Buffer.Fill()
        Buffer.WriteCentered(0, "Scalawag of SPLORR!!", Hue.LightCyan, Hue.Black)
        Dim y = 1
        Buffer.Write(0, y, "Controls:", Hue.LightGray, Hue.Black)
        y += 1
        Buffer.Write(0, y, "Z, W, UpArrow: Up", Hue.LightGray, Hue.Black)
        y += 1
        Buffer.Write(0, y, "S, DownArrow: Down", Hue.LightGray, Hue.Black)
        y += 1
        Buffer.Write(0, y, "A, Q, LeftArrow: Left", Hue.LightGray, Hue.Black)
        y += 1
        Buffer.Write(0, y, "D, RightArrow: Right", Hue.LightGray, Hue.Black)
        y += 1
        Buffer.Write(0, y, "Space: Action", Hue.LightGray, Hue.Black)
        y += 1
        Buffer.Write(0, y, "Backspace: Cancel", Hue.LightGray, Hue.Black)
        Buffer.WriteCentered(Buffer.Rows - 1, "Press <ACTION>", Hue.White, Hue.Black)
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        If command = UI.Command.Green Then
            Return New MainMenuState(Buffer, World, PlaySfx)
        End If
        Return Me
    End Function
End Class
