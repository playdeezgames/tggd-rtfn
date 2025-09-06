Imports TGGD.Business

Friend Class ItemTypeDialog
    Inherits BaseDialog

    Private ReadOnly character As ICharacter
    Private ReadOnly itemType As String

    Public Sub New(character As ICharacter, itemType As String)
        MyBase.New(
            GenerateCaption(character, itemType),
            GenerateChoices(character, itemType),
            GenerateLines(character, itemType))
        Me.character = character
        Me.itemTYpe = itemType
    End Sub

    Private Shared Function GenerateLines(character As ICharacter, itemType As String) As IEnumerable(Of String)
        Return {$"You have {character.GetCountOfItemType(itemType)}."}
    End Function

    Private Shared Function GenerateChoices(character As ICharacter, itemType As String) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From
        {
            (NEVER_MIND_CHOICE, NEVER_MIND_TEXT)
        }
        Dim descriptor = itemType.ToItemTypeDescriptor
        If descriptor.IsAggregate Then
            GenerateAggregateChoices(character, itemType, result)
        Else
            GenerateItemChoices(character, itemType, result)
        End If
        Return result
    End Function

    Private Shared Sub GenerateItemChoices(
                                       character As ICharacter,
                                       itemType As String,
                                       choices As List(Of (Choice As String, Text As String)))
        Throw New NotImplementedException()
    End Sub

    Private Shared Sub GenerateAggregateChoices(
                                               character As ICharacter,
                                               itemType As String,
                                               choices As List(Of (Choice As String, Text As String)))
        Dim item = character.GetItemOfType(itemType)
        For Each choice In item.GetAvailableChoices(character)
            choices.Add(choice)
        Next
    End Sub

    Private Shared Function GenerateCaption(character As ICharacter, itemType As String) As String
        Return itemType.ToItemTypeDescriptor.ItemTypeName
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case NEVER_MIND_CHOICE
                Return New InventoryDialog(character)
            Case Else
                Dim descriptor = itemType.ToItemTypeDescriptor
                Return descriptor.Choose(character.GetItemOfType(itemType), character, choice)
        End Select
    End Function

    Friend Shared Function DetermineNextDialog(character As ICharacter, itemType As String) As Func(Of IDialog)
        If character.HasItemsOfType(itemType) Then
            Return Function() New ItemTypeDialog(character, itemType)
        Else
            Return Function() New InventoryDialog(character)
        End If
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return Choose(NEVER_MIND_CHOICE)
    End Function
End Class
