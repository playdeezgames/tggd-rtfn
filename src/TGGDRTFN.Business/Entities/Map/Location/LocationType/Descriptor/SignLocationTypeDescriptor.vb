Friend Class SignLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(Business.LocationType.Sign, True)
    End Sub

    Friend Overrides Sub OnBump(location As ILocation, character As ICharacter)
        location.World.AddMessage(MoodType.Info, location.GetMetadata(MetadataType.SignText))
    End Sub
End Class
