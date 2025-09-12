Imports TGGD.Business

Friend Class PointsItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(Business.ItemType.Points, "Points!", 100, False)
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
        item.PlaySfx(Sfx.Take)
        Dim points = item.GetStatistic(StatisticType.Points)
        character.ChangeStatistic(StatisticType.Points, points)
        character.RemoveItem(item)
        item.Recycle()
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As Item)
        Dim points = 10
        item.SetStatistic(StatisticType.Points, points)
        item.World.ChangeStatistic(StatisticType.Points, points)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return True
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return location.LocationType.ToLocationTypeDescriptor.CanSpawn(location, ItemType)
    End Function

    Friend Overrides Function GetName(item As Item) As String
        Return $"{item.GetStatistic(StatisticType.Points)} Points!"
    End Function

    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function GetAvailableChoices(item As Item, character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Return Array.Empty(Of (Choice As String, Text As String))
    End Function
End Class
