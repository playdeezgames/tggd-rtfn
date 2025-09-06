Imports TGGD.Business

Friend Class InventoryDialog
    Inherits BaseDialog
    Private ReadOnly character As ICharacter
    Public Sub New(character As ICharacter)
        MyBase.New("Inventory", GenerateChoices(character), Array.Empty(Of String))
        Me.character = character
    End Sub
    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        Dim itemStacks = character.Items.GroupBy(Function(x) x.ItemType)
        For Each itemStack In itemStacks
            result.Add((itemStack.Key, $"{itemStack.Key.ToItemTypeDescriptor.ItemTypeName}({itemStack.Count})"))
        Next
        Return result
    End Function
    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return New ActionListDialog(character)
            Case Else
                Return New ItemTypeDialog(character, choice)
        End Select
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return Choose(NEVER_MIND_CHOICE)
    End Function
End Class
