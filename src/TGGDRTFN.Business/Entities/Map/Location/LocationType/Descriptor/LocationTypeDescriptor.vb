Friend MustInherit Class LocationTypeDescriptor
    Friend ReadOnly Property LocationType As String
    Sub New(locationType As String)
        Me.LocationType = locationType
    End Sub

    Friend MustOverride Sub OnBump(location As ILocation, character As ICharacter)
    Friend MustOverride Sub OnLeave(location As ILocation, character As ICharacter)
    Friend MustOverride Sub OnEnter(location As ILocation, character As ICharacter)
    Friend MustOverride Function CanEnter(location As ILocation, character As ICharacter) As Boolean
End Class
