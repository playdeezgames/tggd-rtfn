Imports TGGD.Business

Friend Class ActionListDialog
    Implements IDialog

    Shared ReadOnly INVENTORY_CHOICE As String = NameOf(INVENTORY_CHOICE)
    Const INVENTORY_TEXT = "Inventory"

    Private ReadOnly character As ICharacter
    Sub New(character As ICharacter)
        Me.character = character
    End Sub

    Public ReadOnly Property Caption As String Implements IDialog.Caption
        Get
            Return "Actions..."
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of (Choice As String, Text As String)) Implements IDialog.Choices
        Get
            Dim result As New List(Of (Choice As String, Text As String)) From {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
            If character.HasItems Then
                result.Add((INVENTORY_CHOICE, INVENTORY_TEXT))
            End If
            Return result
        End Get
    End Property

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return Array.Empty(Of String)
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
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
