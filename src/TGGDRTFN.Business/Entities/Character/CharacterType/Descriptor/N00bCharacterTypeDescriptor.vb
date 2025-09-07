Friend Class N00bCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(Business.CharacterType.N00b, 1)
    End Sub

    Const MAXIMUM_SATIETY = 100
    Const MAXIMUM_HEALTH = 100
    Const SATIETY_WARNING = MAXIMUM_SATIETY / 10

    Friend Overrides Sub OnInitialize(character As ICharacter)
        character.World.Avatar = character
        character.SetStatisticMinimum(Business.StatisticType.Health, 0)
        character.SetStatisticMaximum(Business.StatisticType.Health, MAXIMUM_HEALTH)
        character.SetStatistic(Business.StatisticType.Health, MAXIMUM_HEALTH)

        character.SetStatisticMinimum(Business.StatisticType.Satiety, 0)
        character.SetStatisticMaximum(Business.StatisticType.Satiety, MAXIMUM_SATIETY)
        character.SetStatistic(Business.StatisticType.Satiety, MAXIMUM_SATIETY)

        character.SetStatistic(Business.StatisticType.Points, 0)
    End Sub

    Friend Overrides Sub OnBump(character As ICharacter, location As ILocation)
    End Sub

    Private Sub Starve(character As ICharacter)
        If character.GetStatistic(StatisticType.Satiety) > character.GetStatisticMinimum(StatisticType.Satiety) Then
            If character.ChangeStatistic(StatisticType.Satiety, -1) < SATIETY_WARNING Then
                character.World.AddMessage(MoodType.Warning, "Yer hungry!")
            End If
        Else
            character.World.AddMessage(MoodType.Danger, "Yer starving!")
            character.ChangeStatistic(StatisticType.Health, -1)
        End If
    End Sub

    Friend Overrides Sub OnEnter(character As ICharacter, location As ILocation)
        location.Map.SetTag(TagType.Explored, True)
        Starve(character)
        Dim items = location.Items
        For Each item In items
            location.RemoveItem(item)
            character.World.AddMessage(MoodType.Info, $"You pick up {item.Name}.")
            character.AddItem(item)
        Next
    End Sub

    Friend Overrides Sub OnLeave(character As ICharacter, location As ILocation)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return Not map.GetTag(TagType.DeadEnd) AndAlso map.Locations.Any(AddressOf CanSpawnLocation)
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return Not location.HasCharacter AndAlso location.LocationType = LocationType.Floor
    End Function

    Friend Overrides Sub HandleAddItem(character As ICharacter, item As IItem)
        item.ItemType.ToItemTypeDescriptor.HandleAddItem(item, character)
    End Sub

    Friend Overrides Sub HandleRemoveItem(character As ICharacter, item As IItem)
        item.ItemType.ToItemTypeDescriptor.HandleRemoveItem(item, character)
    End Sub
End Class
