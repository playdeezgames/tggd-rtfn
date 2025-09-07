Imports System.Runtime.CompilerServices

Public Module EntityExtensions
    <Extension>
    Friend Sub SetDestinationLocation(entity As IEntity, destinationLocation As ILocation)
        entity.SetStatistic(StatisticType.DestinationLocationId, destinationLocation.LocationId)
    End Sub
    <Extension>
    Public Function GetDestinationLocation(entity As IEntity) As ILocation
        Return entity.World.GetLocation(entity.GetStatistic(StatisticType.DestinationLocationId))
    End Function
End Module
