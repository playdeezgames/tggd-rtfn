Public Interface IEntity
    Sub Clear()
    Sub Initialize()
    ReadOnly Property World As IWorld
    Sub SetStatistic(statisticType As String, statisticValue As Integer?)
End Interface
