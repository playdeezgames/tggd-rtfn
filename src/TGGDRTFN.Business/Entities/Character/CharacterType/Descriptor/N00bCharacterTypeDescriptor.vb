Imports System.ComponentModel.DataAnnotations.Schema

Friend Class N00bCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(Business.CharacterType.N00b, 1)
    End Sub

    Friend Overrides Sub OnInitialize(character As Character)
        character.World.Avatar = character
    End Sub

    Friend Overrides Function CanSpawnMap(map As IMap) As Boolean
        Return Not map.GetTag(TagType.DeadEnd) AndAlso map.Locations.Any(AddressOf CanSpawnLocation)
    End Function

    Friend Overrides Function CanSpawnLocation(location As ILocation) As Boolean
        Return Not location.HasCharacter AndAlso location.LocationType = LocationType.Floor
    End Function
End Class
