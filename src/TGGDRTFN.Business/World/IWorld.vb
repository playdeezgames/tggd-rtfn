
Public Interface IWorld
    Inherits IEntity
    Function CreateMap(mapType As String) As IMap
    ReadOnly Property Maps As IEnumerable(Of IMap)
    Function GetMap(mapId As Integer) As IMap
    Function CreateLocation(locationType As String, map As IMap, column As Integer, row As Integer) As ILocation
    Function CreateCharacter(characterType As String, location As ILocation) As ICharacter
    Function CreateItem(itemType As String, entity As IInventoryEntity) As IItem
    Property Avatar As ICharacter
End Interface
