Imports TGGD.Business

Friend Class NpcDialog
    Inherits BaseDialog

    Private ReadOnly initiator As ICharacter
    Private ReadOnly target As ICharacter
    Private Shared ReadOnly LEAVE_CHOICE As String = NameOf(LEAVE_CHOICE)
    Const LEAVE_TEXT = "Leave"

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
        Return result
    End Function

    Public Overrides Function Choose(choice As String) As IDialog
        Return Nothing
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return Nothing
    End Function
End Class
