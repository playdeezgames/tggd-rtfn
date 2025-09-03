Imports TGGD.UI
Imports TGGDRTFN.Business

Friend Class DeadState
    Inherits BaseState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String))
        MyBase.New(buffer, world, playSfx)
    End Sub

    Public Overrides Sub Refresh()
        Buffer.Fill()
        Buffer.WriteCentered(Buffer.Rows \ 2, "Yer Dead!", Hue.Red, Hue.Black)
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        Select Case command
            Case UI.Command.Green
                World.Clear()
                Return New MainMenuState(Buffer, World, PlaySfx)
            Case Else
                Return Me
        End Select
    End Function
End Class
