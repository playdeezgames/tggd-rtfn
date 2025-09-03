Friend Class N00bCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(Business.CharacterType.N00b, 1)
    End Sub

    Friend Overrides Sub OnInitialize(character As Character)
        character.World.Avatar = character
        character.SetStatisticMinimum(Business.StatisticType.Health, 0)
        character.SetStatisticMaximum(Business.StatisticType.Health, 100)
        character.SetStatistic(Business.StatisticType.Health, 100)
        character.SetStatisticMinimum(Business.StatisticType.Satiety, 0)
        character.SetStatisticMaximum(Business.StatisticType.Satiety, 100)
        character.SetStatistic(Business.StatisticType.Satiety, 100)
    End Sub

    Friend Overrides Sub OnBump(character As ICharacter, location As ILocation)
    End Sub

    Private Sub Starve(character As ICharacter)
        If character.GetStatistic(StatisticType.Satiety) > character.GetStatisticMinimum(StatisticType.Satiety) Then
            character.ChangeStatistic(StatisticType.Satiety, -1)
        Else
            character.ChangeStatistic(StatisticType.Health, -1)
        End If
    End Sub

    Friend Overrides Sub OnEnter(character As ICharacter, location As ILocation)
        Starve(character)
    End Sub

    Friend Overrides Sub OnLeave(character As ICharacter, location As ILocation)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return Not map.GetTag(TagType.DeadEnd) AndAlso map.Locations.Any(AddressOf CanSpawnLocation)
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return Not location.HasCharacter AndAlso location.LocationType = LocationType.Floor
    End Function
End Class
