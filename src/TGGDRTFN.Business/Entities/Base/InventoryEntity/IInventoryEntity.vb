Public Interface IInventoryEntity
    Inherits IEntity
    Sub AddItem(item As IItem)
    ReadOnly Property HasItems As Boolean
    ReadOnly Property Items As IEnumerable(Of IItem)
End Interface
