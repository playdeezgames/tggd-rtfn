Public Class WorldData
    Inherits EntityData
    Property Maps As New List(Of MapData)
    Property Locations As New List(Of LocationData)
    Property Characters As New List(Of CharacterData)
    Property AvatarCharacterId As Integer?
    Property Messages As New List(Of MessageData)
End Class
