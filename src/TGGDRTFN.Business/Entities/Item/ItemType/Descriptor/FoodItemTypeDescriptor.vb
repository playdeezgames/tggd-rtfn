Friend Class FoodItemTypeDescriptor
    Inherits ItemTypeDescriptor
    Public Sub New()
        MyBase.New(Business.ItemType.Food, 20)
    End Sub
    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return map.Locations.Any(Function(x) CanSpawnLocation(x))
    End Function
    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return location.LocationType.ToLocationTypeDescriptor.CanSpawn(location, ItemType)
    End Function

    Friend Overrides Function GetName(item As Item) As String
        Return "food"
    End Function
End Class
