Friend Class FloorLocationTypeDescriptor
    Inherits LocationTypeDescriptor
    Public Sub New()
        MyBase.New(Business.LocationType.Floor)
    End Sub

    Friend Overrides Sub OnBump(location As ILocation, character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Sub OnLeave(location As ILocation, character As ICharacter)
    End Sub

    Friend Overrides Sub OnEnter(location As ILocation, character As ICharacter)
    End Sub

    Friend Overrides Function CanEnter(location As ILocation, character As ICharacter) As Boolean
        Return True
    End Function
End Class
