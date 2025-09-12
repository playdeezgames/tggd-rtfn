Imports TGGD.UI
Imports Contemn.Business

Friend Module NeutralState
    Friend Function DetermineState(
                                  buffer As IUIBuffer(Of Integer),
                                  world As IWorld,
                                  playSfx As Action(Of String)) As IUIState
        Dim avatar = world.Avatar
        If world.GetStatistic(StatisticType.Points) = 0 Then
            world.PlaySfx(Sfx.WooHoo)
            Return New VictoryState(buffer, world, playSfx)
        ElseIf avatar.GetStatistic(StatisticType.Health) = avatar.GetStatisticMinimum(StatisticType.Health) Then
            world.PlaySfx(Sfx.PlayerDeath)
            Return New DeadState(buffer, world, playSfx)
        End If
        Return New NavigationState(buffer, world, playSfx)
    End Function
End Module
