Imports TGGD.Business

Friend Class LooLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(Business.LocationType.Loo)
    End Sub

    Friend Overrides Function OnBump(location As ILocation, character As ICharacter) As IDialog
        If character.HasItemsOfType(ItemType.EmptyBottle) Then
            Dim items = character.Items.Where(Function(x) x.ItemType = ItemType.EmptyBottle).ToList
            For Each item In items
                character.RemoveItem(item)
                item.Recycle()
                character.World.CreateItem(ItemType.WaterBottle, character)
            Next
            character.World.AddMessage(MoodType.Info, $"You fill yer empty bottles!")
            character.PlaySfx.Invoke(Sfx.Craft)
            Return Nothing
        End If
        Dim points = character.GetStatistic(StatisticType.Points)
        If points > 0 Then
            character.SetStatistic(StatisticType.Points, 0)
            character.ChangeStatistic(StatisticType.Score, points)
            character.World.ChangeStatistic(StatisticType.Points, -points)
            character.World.AddMessage(MoodType.Info, $"You flush {points} points down the loo!")
            character.PlaySfx.Invoke(Sfx.Shucks)
        End If
        Return Nothing
    End Function

    Friend Overrides Sub OnLeave(location As ILocation, character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Function OnEnter(location As ILocation, character As ICharacter) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function CanEnter(location As ILocation, character As ICharacter) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawn(location As ILocation, itemType As String) As Boolean
        Return False
    End Function

    Friend Overrides Sub OnInitialize(location As Location)
    End Sub
End Class
