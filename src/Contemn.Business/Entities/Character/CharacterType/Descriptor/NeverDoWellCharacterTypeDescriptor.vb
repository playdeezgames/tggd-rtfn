Imports TGGD.Business

Friend Class NeverDoWellCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(Business.CharacterType.NeverDoWell, 100)
    End Sub

    Friend Overrides Sub OnInitialize(character As ICharacter)
    End Sub

    Friend Overrides Function OnBump(character As ICharacter, location As ILocation) As IDialog
        Throw New NotImplementedException()
    End Function

    Friend Overrides Sub OnEnter(character As ICharacter, location As ILocation)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Sub OnLeave(character As ICharacter, location As ILocation)
    End Sub

    Friend Overrides Sub HandleAddItem(character As ICharacter, item As IItem)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Sub HandleRemoveItem(character As ICharacter, item As IItem)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return True
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return Not location.HasCharacter AndAlso location.LocationType = LocationType.Floor AndAlso Not location.HasItems
    End Function

    Friend Overrides Function OnInteract(target As ICharacter, initiator As ICharacter) As IDialog
        Return New NpcDialog(initiator, target)
    End Function
End Class
