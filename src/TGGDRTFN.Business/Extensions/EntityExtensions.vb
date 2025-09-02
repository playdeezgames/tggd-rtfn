Imports System.Runtime.CompilerServices

Friend Module EntityExtensions
    <Extension>
    Sub SetDestinationLocation(entity As IEntity, destinationLocation As ILocation)
        entity.SetStatistic(StatisticType.DestinationLocationId, destinationLocation.LocationId)
    End Sub
    <Extension>
    Function GetDestinationLocation(entity As IEntity) As ILocation
        Return entity.World.GetLocation(entity.GetStatistic(StatisticType.DestinationLocationId))
    End Function
End Module
