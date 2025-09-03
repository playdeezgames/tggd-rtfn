Friend Class WallLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(Business.LocationType.Wall, True)
    End Sub

    Friend Overrides Sub OnBump(location As ILocation, character As ICharacter)
    End Sub
End Class
