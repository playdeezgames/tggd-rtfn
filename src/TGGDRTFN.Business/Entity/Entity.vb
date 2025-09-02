Imports TGGDRTFN.Data

Public MustInherit Class Entity(Of TEntityData As EntityData)
    Implements IEntity
    Protected ReadOnly Property PlaySfx As Action(Of String)
    Protected ReadOnly Data As WorldData
    Protected MustOverride ReadOnly Property EntityData As TEntityData

    Public ReadOnly Property World As IWorld Implements IEntity.World
        Get
            Return New World(Data, PlaySfx)
        End Get
    End Property

    Sub New(data As WorldData, playSfx As Action(Of String))
        Me.Data = data
        Me.PlaySfx = playSfx
    End Sub
    Public Overridable Sub Clear() Implements IEntity.Clear
        EntityData.Statistics.Clear()
    End Sub

    Public Overridable Sub Initialize() Implements IEntity.Initialize
        Clear()
    End Sub

    Public Sub SetStatistic(statisticType As String, statisticValue As Integer?) Implements IEntity.SetStatistic
        If statisticValue.HasValue Then
            EntityData.Statistics(statisticType) = statisticValue.Value
        Else
            EntityData.Statistics.Remove(statisticType)
        End If
    End Sub
End Class
