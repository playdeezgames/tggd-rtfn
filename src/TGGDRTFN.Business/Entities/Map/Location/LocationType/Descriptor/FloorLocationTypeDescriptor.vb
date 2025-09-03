Friend Class FloorLocationTypeDescriptor
    Inherits LocationTypeDescriptor
    Public Sub New()
        MyBase.New(Business.LocationType.Floor, False)
    End Sub

    Friend Overrides Sub OnBump(location As ILocation, character As ICharacter)
    End Sub
End Class
