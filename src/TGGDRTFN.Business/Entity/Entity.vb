Imports TGGDRTFN.Data

Public MustInherit Class Entity(Of TEntityData As EntityData)
    Implements IEntity
    Protected ReadOnly Property PlaySfx As Action(Of String)
    Protected MustOverride ReadOnly Property EntityData As TEntityData
    Sub New(playSfx As Action(Of String))
        Me.PlaySfx = playSfx
    End Sub
    Public Overridable Sub Clear() Implements IEntity.Clear

    End Sub
End Class
