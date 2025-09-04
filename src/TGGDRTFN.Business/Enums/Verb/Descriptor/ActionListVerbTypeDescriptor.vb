Imports TGGD.Business

Friend Class ActionListVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(Business.VerbType.ActionList)
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        Return New ActionListDialog(character)
    End Function
End Class
