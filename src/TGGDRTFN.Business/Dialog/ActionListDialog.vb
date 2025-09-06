Imports TGGD.Business

Friend Class ActionListDialog
    Inherits BaseDialog
    Shared ReadOnly INVENTORY_CHOICE As String = NameOf(INVENTORY_CHOICE)
    Const INVENTORY_TEXT = "Inventory"
    Private ReadOnly character As ICharacter
    Sub New(character As ICharacter)
        MyBase.New("Actions...", GenerateChoices(character), Array.Empty(Of String))
        Me.character = character
    End Sub
    Private Shared Function GenerateChoices(character As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
        If character.HasItems Then
            result.Add((INVENTORY_CHOICE, INVENTORY_TEXT))
        End If
        Return result
    End Function
    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case INVENTORY_CHOICE
                Return New InventoryDialog(character)
            Case NEVER_MIND_CHOICE
                Return Nothing
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
