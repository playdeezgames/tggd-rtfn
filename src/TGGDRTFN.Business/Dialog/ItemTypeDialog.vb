Imports TGGD.Business

Friend Class ItemTypeDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            GenerateCaption(character, itemType),
            GenerateChoices(character, itemType),
            GenerateLines(character, itemType))
        Me.character = character
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, itemType As String) As IEnumerable(Of String)
        Return Array.Empty(Of String)
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, itemType As String) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From
        {
            (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
        }
        Return result
    End Function

    Private Shared Function GenerateCaption(character As ICharacter, itemType As String) As String
        Return itemType.ToItemTypeDescriptor.ItemTypeName
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return New InventoryDialog(character)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
