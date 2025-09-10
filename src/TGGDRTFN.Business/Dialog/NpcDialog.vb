Imports TGGD.Business

Friend Class NpcDialog
    Inherits BaseDialog

    Private ReadOnly initiator As ICharacter
    Private ReadOnly target As ICharacter
    Private Shared ReadOnly LEAVE_CHOICE As String = NameOf(LEAVE_CHOICE)
    Private Shared ReadOnly FEED_CHOICE As String = NameOf(FEED_CHOICE)
    Const LEAVE_TEXT = "Leave"
    Const FEED_TEXT = "Feed(-1 Food)"

    Public Sub New(initiator As ICharacter, target As ICharacter)
        MyBase.New("Dialog", GenerateChoices(initiator, target), GenerateLines(initiator, target))
        Me.initiator = initiator
        Me.target = target
    End Sub

    Private Shared Function GenerateLines(initiator As ICharacter, target As ICharacter) As IEnumerable(Of String)
        Return {
            "You have encountered another person."
            }
    End Function

    Private Shared Function GenerateChoices(initiator As ICharacter, target As ICharacter) As IEnumerable(Of (Choice As String, Text As String))
        Dim result As New List(Of (Choice As String, Text As String)) From
            {
                (LEAVE_CHOICE, LEAVE_TEXT)
            }
        If initiator.HasItemsOfType(ItemType.Food) Then
            result.Add((FEED_CHOICE, FEED_TEXT))
        End If
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Select Case choice
            Case LEAVE_CHOICE
                Return Nothing
            Case FEED_CHOICE
                Return Feed()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function Feed() As IDialog
        Dim item = initiator.GetItemOfType(ItemType.Food)
        initiator.RemoveItem(item)
        item.Recycle()
        target.World.CreateItem(ItemType.Points, target.Location)
        target.MoveTo(Nothing)
        target.Recycle()
        Return New MessageDialog(
            {
                "You feed them."
            },
            {
                (OK_IDENTIFIER, OK_TEXT, Function() Nothing)
            },
            Function() Nothing)
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return Nothing
    End Function
End Class
