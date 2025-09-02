Imports TGGD.Business
Imports TGGDRTFN.Data

Public Class World
    Inherits Entity(Of WorldData)
    Implements IWorld
    Sub New(data As WorldData, playSfx As Action(Of String))
        MyBase.New(data, playSfx)
    End Sub

    Public ReadOnly Property Maps As IEnumerable(Of IMap) Implements IWorld.Maps
        Get
            Return Enumerable.
                Range(0, Data.Maps.Count).
                Select(Function(x) New Business.Map(Data, x, PlaySfx))
        End Get
    End Property

    Public Property Avatar As ICharacter Implements IWorld.Avatar
        Get
            Return If(
                Data.AvatarCharacterId.HasValue,
                New Character(Data, Data.AvatarCharacterId.Value, PlaySfx),
                Nothing)
        End Get
        Set(value As ICharacter)
            Data.AvatarCharacterId = value?.CharacterId
        End Set
    End Property

    Protected Overrides ReadOnly Property EntityData As WorldData
        Get
            Return Data
        End Get
    End Property
    Public Overrides Sub Clear()
        MyBase.Clear()
        Data.Maps.Clear()
        Data.Locations.Clear()
        Data.Characters.Clear()
    End Sub
    Public Overrides Sub Initialize()
        MyBase.Initialize()
        CreateMaps()
        CreateCharacters()
    End Sub

    Private Sub CreateCharacters()
        For Each characterType In CharacterTypes.All
            Dim descriptor = characterType.ToCharacterTypeDescriptor
            Dim candidateMaps = Maps.Where(Function(x) descriptor.CanSpawnMap(x))
            For Each dummy In Enumerable.Range(0, descriptor.CharacterCount)
                Dim map = RNG.FromEnumerable(candidateMaps)
                Dim candidateLocations = map.Locations.Where(Function(x) descriptor.CanSpawnLocation(x))
                CreateCharacter(characterType, RNG.FromEnumerable(candidateLocations))
            Next
        Next
    End Sub

    Private Sub CreateMaps()
        For Each mapType In MapTypes.All
            Dim descriptor = mapType.ToMapTypeDescriptor
            For Each dummy In Enumerable.Range(0, descriptor.MapCount)
                CreateMap(mapType)
            Next
        Next
    End Sub

    Public Function CreateMap(mapType As String) As IMap Implements IWorld.CreateMap
        Dim mapId = Data.Maps.Count
        Data.Maps.Add(New MapData With {.MapType = mapType})
        Dim result = New Map(Data, mapId, PlaySfx)
        result.Initialize()
        Return result
    End Function

    Public Function CreateLocation(locationType As String, map As IMap, column As Integer, row As Integer) As ILocation Implements IWorld.CreateLocation
        Dim locationId = Data.Locations.Count
        Data.Locations.Add(New LocationData With {
                            .LocationType = locationType,
                            .MapId = map.MapId,
                            .Column = column,
                            .Row = row})
        Dim result = New Location(
            Data,
            locationId,
            PlaySfx)
        map.SetLocation(column, row, result)
        result.Initialize()
        Return result
    End Function

    Public Function CreateCharacter(characterType As String, location As ILocation) As ICharacter Implements IWorld.CreateCharacter
        Dim characterId = Data.Characters.Count
        Data.Characters.Add(New CharacterData With {
                            .CharacterType = characterType,
                            .LocationId = location.LocationId})
        Dim result = New Character(
            Data,
            characterId,
            PlaySfx)
        result.Initialize()
        Return result
    End Function

    Public Function CreateItem(itemType As String, entity As IInventoryEntity) As IItem Implements IWorld.CreateItem
        Throw New NotImplementedException()
    End Function
End Class
