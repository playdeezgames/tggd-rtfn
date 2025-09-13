Imports TGGD.Business

Friend Class MarketMapTypeDescriptor
    Inherits BaseRoomMapTypeDescriptor

    Public Sub New()
        MyBase.New(Business.MapType.Market, MARKET_COUNT)
    End Sub

    Friend Overrides Sub OnInitialize(map As IMap)
        MyBase.OnInitialize(map)
        Dim candidate = RNG.FromEnumerable(map.Locations.Where(Function(x) Not x.HasCharacter AndAlso Not x.HasItems AndAlso x.LocationType = Business.LocationType.Floor))
        candidate.LocationType = Business.LocationType.Store
    End Sub
End Class
