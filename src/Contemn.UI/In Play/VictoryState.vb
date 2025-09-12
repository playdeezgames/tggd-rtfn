Imports TGGD.UI
Imports Contemn.Business

Friend Class VictoryState
    Inherits BaseState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String))
        MyBase.New(buffer, world, playSfx)
    End Sub

    Public Overrides Sub Refresh()
        Buffer.Fill()
        Dim y = Buffer.Rows \ 2
        Buffer.WriteCentered(y, "You Win!", Hue.LightGreen, Hue.Black)
        y += 1
        Buffer.WriteCentered(y, $"Final Score: {World.Avatar.GetStatistic(StatisticType.Score)}", Hue.LightGray, Hue.Black)
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
