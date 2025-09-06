Imports TGGD.Business

Friend Class MessageDialog
    Inherits BaseDialog

    Shared ReadOnly OK_IDENTIFIER As String = NameOf(OK_IDENTIFIER)
    Const OK_TEXT = "OK"
    ReadOnly nextDialog As Func(Of IDialog)

    Public Sub New(lines As IEnumerable(Of String), nextDialog As Func(Of IDialog))
        MyBase.New("Message", {(OK_IDENTIFIER, OK_TEXT)}, lines)
        Me.nextDialog = nextDialog
    End Sub

    Public Overrides Function Choose(choice As String) As IDialog
        Return nextDialog()
    End Function

    Public Overrides Function CancelDialog() As IDialog
        Return nextDialog()
    End Function
End Class
