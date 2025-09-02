Friend Class DoorLocationTypeDescriptor
    Inherits LocationTypeDescriptor

    Public Sub New()
        MyBase.New(Business.LocationType.Door, True)
    End Sub

    Friend Overrides Sub OnBump(location As ILocation, character As ICharacter)
        Dim destination = location.GetDestinationLocation()
        character.MoveTo(destination)
    End Sub
End Class
