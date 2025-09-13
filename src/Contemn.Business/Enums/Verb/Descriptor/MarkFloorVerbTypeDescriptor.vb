Imports TGGD.Business

Friend Class MarkFloorVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(Business.VerbType.MarkFloor, "Mark Floor")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        character.World.AddMessage(MoodType.Info, "You mark the floor with an X.")
        Return Nothing
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return False
    End Function
End Class
