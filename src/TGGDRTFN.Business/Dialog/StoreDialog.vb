Imports TGGD.Business

Friend Class StoreDialog
    Inherits BaseDialog
    Shared ReadOnly LEAVE_CHOICE As String = NameOf(LEAVE_CHOICE)
    Shared ReadOnly BUY_CHOICE As String = NameOf(BUY_CHOICE)
    Private ReadOnly character As ICharacter
    Private ReadOnly location As ILocation
    Const LEAVE_TEXT = "Leave"
    Const BUY_TEXT = "Buy!"

    Public Sub New(character As ICharacter, location As ILocation)
        MyBase.New("Store", GenerateChoices(character, location), GenerateLines(character, location))
        Me.character = character
        Me.location = location
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, location As ILocation) As IEnumerable(Of String)
        Dim result As New List(Of String) From {
            $"Food costs {location.GetStatistic(StatisticType.FoodPrice)} point(s)!",
            $"You have {character.GetCountOfItemType(ItemType.Food)} food.",
            $"You have {character.GetStatistic(StatisticType.Points)} points."
        }
        Return result
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, location As ILocation) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From {
            (LEAVE_CHOICE, LEAVE_TEXT)
            }
        If character.GetStatistic(StatisticType.Points) >= location.GetStatistic(StatisticType.FoodPrice) Then
            result.Add((BUY_CHOICE, BUY_TEXT))
        End If
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case LEAVE_CHOICE
                Return Nothing
            Case BUY_CHOICE
                Return BuyFood()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function BuyFood() As IDialog
        Dim points = location.GetStatistic(StatisticType.FoodPrice)
        character.ChangeStatistic(StatisticType.Points, -points)
        character.World.ChangeStatistic(StatisticType.Points, -points)
        character.World.CreateItem(ItemType.Food, character)
        location.ChangeStatistic(StatisticType.FoodPrice, 1)
        Return New StoreDialog(character, location)
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return Nothing
    End Function
End Class
