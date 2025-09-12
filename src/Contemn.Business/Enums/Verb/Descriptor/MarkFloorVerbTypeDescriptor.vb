Imports TGGD.Business

Friend Class MarkFloorVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(Business.VerbType.MarkFloor, "Mark Floor")
    End Sub

    Public Overrides Function Perform(character As ICharacter) As IDialog
        character.Location.SetTag(TagType.Mark, True)
        character.World.AddMessage(MoodType.Info, "You mark the floor with an X.")
        Return Nothing
    End Function

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return Not character.Location.GetTag(TagType.Mark)
    End Function
End Class
