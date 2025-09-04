Imports TGGD.Business

Friend Class InventoryDialog
    Implements IDialog

    Private ReadOnly character As ICharacter

    Public Sub New(character As ICharacter)
        Me.character = character
    End Sub

    Public ReadOnly Property Caption As String Implements IDialog.Caption
        Get
            Return "Inventory"
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of (Choice As String, Text As String)) Implements IDialog.Choices
        Get
            Dim result As New List(Of (Choice As String, Text As String)) From {
                (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
            }
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
            Case NEVER_MIND_CHOICE
                Return New VerbListDialog(character)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
