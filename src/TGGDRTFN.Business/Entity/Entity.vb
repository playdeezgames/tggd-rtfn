Imports TGGDRTFN.Data

Public MustInherit Class Entity(Of TEntityData As EntityData)
    Implements IEntity
    Protected ReadOnly Property PlaySfx As Action(Of String)
    Protected ReadOnly Data As WorldData
    Protected MustOverride ReadOnly Property EntityData As TEntityData
    Sub New(data As WorldData, playSfx As Action(Of String))
        Me.Data = data
        Me.PlaySfx = playSfx
    End Sub
    Public Overridable Sub Clear() Implements IEntity.Clear

    End Sub

    Public Overridable Sub Initialize() Implements IEntity.Initialize
        Clear()
    End Sub
End Class
