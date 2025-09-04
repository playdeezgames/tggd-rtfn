Imports TGGD.Business

Friend MustInherit Class VerbTypeDescriptor
    ReadOnly Property VerbType As String
    Sub New(verbType As String)
        Me.VerbType = verbType
    End Sub
    MustOverride Function Perform(character As ICharacter) As IDialog
End Class
