Imports TGGDRTFN.Data

Friend MustInherit Class InventoryEntity(Of TEntityData As InventoryEntityData)
    Inherits Entity(Of TEntityData)
    Implements IInventoryEntity

    Protected Sub New(data As WorldData, playSfx As Action(Of String))
        MyBase.New(data, playSfx)
    End Sub
End Class
