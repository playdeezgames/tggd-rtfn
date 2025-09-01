Public Interface ILocation
    Inherits IInventoryEntity
    ReadOnly Property LocationId As Integer
    ReadOnly Property LocationType As String
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    ReadOnly Property Map As IMap
End Interface
