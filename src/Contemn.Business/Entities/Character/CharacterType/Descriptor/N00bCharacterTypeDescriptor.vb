Imports TGGD.Business

Friend Class N00bCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(Business.CharacterType.N00b, 1)
    End Sub

    Const MAXIMUM_SATIETY = 100
    Const MAXIMUM_HEALTH = 100
    Const MAXIMUM_HYDRATION = 100
    Const SATIETY_WARNING = MAXIMUM_SATIETY / 10
    Const HYDRATION_WARNING = MAXIMUM_HYDRATION / 10

    Friend Overrides Sub OnInitialize(character As ICharacter)
        character.World.Avatar = character
        character.SetStatisticMinimum(Business.StatisticType.Health, 0)
        character.SetStatisticMaximum(Business.StatisticType.Health, MAXIMUM_HEALTH)
        character.SetStatistic(Business.StatisticType.Health, MAXIMUM_HEALTH)

        character.SetStatisticMinimum(Business.StatisticType.Satiety, 0)
        character.SetStatisticMaximum(Business.StatisticType.Satiety, MAXIMUM_SATIETY)
        character.SetStatistic(Business.StatisticType.Satiety, MAXIMUM_SATIETY)

        character.SetStatisticMinimum(Business.StatisticType.Hydration, 0)
        character.SetStatisticMaximum(Business.StatisticType.Hydration, MAXIMUM_HYDRATION)
        character.SetStatistic(Business.StatisticType.Hydration, MAXIMUM_HYDRATION)

        character.SetStatistic(Business.StatisticType.Points, 0)
        character.SetStatistic(Business.StatisticType.Score, 0)

        For Each dummy In Enumerable.Range(0, 3)
            character.World.CreateItem(ItemType.WaterBottle, character)
        Next
    End Sub

    Friend Overrides Function OnBump(character As ICharacter, location As ILocation) As IDialog
        Return location.HandleBump(character)
    End Function

    Private Shared Function ProcessHunger(character As ICharacter) As Integer
        If character.GetStatistic(StatisticType.Satiety) > character.GetStatisticMinimum(StatisticType.Satiety) Then
            If character.ChangeStatistic(StatisticType.Satiety, -1) < SATIETY_WARNING Then
                character.World.AddMessage(MoodType.Warning, "Yer hungry! Better eat soon.")
            End If
            Return 0
        End If
        Return -1
    End Function

    Private Shared Sub ProcessStarvation(character As ICharacter, starvation As Integer, dehydration As Integer)
        If starvation < 0 OrElse dehydration < 0 Then
            character.PlaySfx(Sfx.PlayerHit)
            character.World.AddMessage(MoodType.Danger, "Yer starving/dehydrated!")
            character.World.AddMessage(MoodType.Danger, "Eat/drink immediately!")
            character.ChangeStatistic(StatisticType.Health, starvation + dehydration)
        End If
    End Sub

    Friend Overrides Sub OnEnter(character As ICharacter, location As ILocation)
        Dim dehydration = ProcessThirst(character)
        Dim starvation = ProcessHunger(character)
        ProcessStarvation(character, starvation, dehydration)
        Dim items = location.Items
        For Each item In items
            location.RemoveItem(item)
            character.World.AddMessage(MoodType.Info, $"You pick up {item.Name}.")
            character.AddItem(item)
        Next
    End Sub

    Private Shared Function ProcessThirst(character As ICharacter) As Integer
        If character.GetStatistic(StatisticType.Hydration) > character.GetStatisticMinimum(StatisticType.Hydration) Then
            If character.ChangeStatistic(StatisticType.Hydration, -1) < HYDRATION_WARNING Then
                character.World.AddMessage(MoodType.Warning, "Yer thirsty! Better drink soon.")
            End If
            Return 0
        End If
        Return -1
    End Function

    Friend Overrides Sub OnLeave(character As ICharacter, location As ILocation)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return True
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

    Friend Overrides Function OnInteract(target As ICharacter, initiator As ICharacter) As IDialog
        Throw New NotImplementedException()
    End Function
End Class
