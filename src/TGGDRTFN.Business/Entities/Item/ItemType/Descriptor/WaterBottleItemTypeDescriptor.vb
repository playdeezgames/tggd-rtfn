Imports TGGD.Business

Friend Class WaterBottleItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Shared ReadOnly DRINK_CHOICE As String = NameOf(DRINK_CHOICE)
    Const DRINK_TEXT = "Drink!"

    Public Sub New()
        MyBase.New(Business.ItemType.WaterBottle, "Water Bottle", 0, True)
    End Sub

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As Item)
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return False
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return False
    End Function

    Friend Overrides Function GetName(item As Item) As String
        Return "bottle(full)"
    End Function

    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Select Case choice
            Case DRINK_CHOICE
                Return Drink(item, character)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function Drink(item As IItem, character As ICharacter) As IDialog
        character.RemoveItem(item)
        item.Recycle()
        character.World.CreateItem(Business.ItemType.EmptyBottle, character)
        Dim hydrationDelta = character.GetStatisticMaximum(StatisticType.Hydration) - character.GetStatistic(StatisticType.Hydration)
        character.ChangeStatistic(StatisticType.Hydration, hydrationDelta)
        Return New MessageDialog(
            {
                $"+{hydrationDelta} {StatisticType.Hydration.ToStatisticTypeDescriptor.StatisticTypeName}."
            },
            {
                (OK_IDENTIFIER, OK_TEXT, Function() Nothing),
                (INVENTORY_IDENTIFIER, INVENTORY_TEXT, Function() New InventoryDialog(character)),
                (ACTIONS_IDENTIFIER, ACTIONS_TEXT, Function() New ActionListDialog(character))
            },
            Function() Nothing)
    End Function

    Friend Overrides Function GetAvailableChoices(item As Item, character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Return {(DRINK_CHOICE, DRINK_TEXT)}
    End Function
End Class
