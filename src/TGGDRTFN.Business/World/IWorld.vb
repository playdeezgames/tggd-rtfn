
Public Interface IWorld
    Inherits IEntity
    Function CreateMap(mapType As String) As IMap
    Function CreateLocation(locationType As String, map As IMap, column As Integer, row As Integer) As ILocation
    Function CreateCharacter(characterType As String, location As ILocation) As ICharacter
    Function CreateItem(itemType As String, entity As IInventoryEntity) As IItem
End Interface
