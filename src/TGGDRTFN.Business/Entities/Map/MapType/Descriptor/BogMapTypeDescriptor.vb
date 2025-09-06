Imports TGGD.Business

Friend Class BogMapTypeDescriptor
    Inherits BaseRoomMapTypeDescriptor

    Public Sub New()
        MyBase.New(Business.MapType.Bog, 1)
    End Sub

    Friend Overrides Sub OnInitialize(map As IMap)
        MyBase.OnInitialize(map)
        Dim candidate = RNG.FromEnumerable(map.Locations.Where(Function(x) Not x.HasCharacter AndAlso Not x.HasItems AndAlso x.LocationType = Business.LocationType.Floor AndAlso Not x.GetTag(TagType.DoorExit)))
        candidate.LocationType = Business.LocationType.Loo
    End Sub
End Class
