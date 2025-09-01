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
        Buffer.WriteCentered(0, "Name of Game", Hue.White, Hue.Black)
        Buffer.WriteCentered(Buffer.Rows - 1, "Press <SPACE>", Hue.White, Hue.Black)
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        If command = UI.Command.Green Then
            Return New MainMenuState(Buffer, World, PlaySfx)
        End If
        Return Me
    End Function
End Class
