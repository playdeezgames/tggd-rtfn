Imports System.Reflection.Metadata.Ecma335
Imports TGGD.Business

Friend Class FoodItemTypeDescriptor
    Inherits ItemTypeDescriptor
    Public Sub New()
        MyBase.New(
            Business.ItemType.Food,
            "Food",
            75,
            True)
    End Sub
    Shared ReadOnly EAT_CHOICE As String = NameOf(EAT_CHOICE)
    Const EAT_TEXT = "Eat"
    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return True
    End Function
    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return location.LocationType.ToLocationTypeDescriptor.CanSpawn(location, ItemType)
    End Function
    Friend Overrides Function GetName(item As Item) As String
        Return "food"
    End Function
    Friend Overrides Function Choose(item As IItem, character As ICharacter, choice As String) As IDialog
        Select Case choice
            Case EAT_CHOICE
                Return Eat(item, character)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function Eat(item As IItem, character As ICharacter) As IDialog
        item.PlaySfx(Sfx.Eat)
        Dim satietyDelta = character.GetStatisticMaximum(StatisticType.Satiety) - character.GetStatistic(StatisticType.Satiety)
        character.SetStatistic(StatisticType.Satiety, character.GetStatisticMaximum(StatisticType.Satiety))
        character.RemoveItem(item)
        item.Recycle()
        Return New MessageDialog(
            {
                $"+{satietyDelta} {StatisticType.Satiety.ToStatisticTypeDescriptor.StatisticTypeName}."
            },
            {
                (OK_IDENTIFIER, OK_TEXT, Function() Nothing),
                (ACTIONS_IDENTIFIER, ACTIONS_TEXT, Function() New ActionListDialog(character))
            },
            Function() Nothing)
    End Function


    Friend Overrides Function GetAvailableChoices(item As Item, character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Return {(EAT_CHOICE, EAT_TEXT)}
    End Function

    Friend Overrides Sub HandleAddItem(item As IItem, character As ICharacter)
        item.PlaySfx(Sfx.Yoink)
    End Sub

    Friend Overrides Sub HandleRemoveItem(item As IItem, character As ICharacter)
    End Sub

    Friend Overrides Sub HandleInitialize(item As Item)
    End Sub
End Class
