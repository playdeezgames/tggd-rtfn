Imports System.Data
Imports TGGD.Business
Imports Contemn.Data

Public Class World
    Inherits Entity(Of WorldData)
    Implements IWorld
    Shared ReadOnly KnightMazeDirections As IReadOnlyDictionary(Of Integer, MazeDirection(Of Integer)) =
        New Dictionary(Of Integer, MazeDirection(Of Integer)) From
        {
            {0, New MazeDirection(Of Integer)(4, 1, -2)},
            {1, New MazeDirection(Of Integer)(5, 2, -1)},
            {2, New MazeDirection(Of Integer)(6, 2, 1)},
            {3, New MazeDirection(Of Integer)(7, 1, 2)},
            {4, New MazeDirection(Of Integer)(0, -1, 2)},
            {5, New MazeDirection(Of Integer)(1, -2, 1)},
            {6, New MazeDirection(Of Integer)(2, -2, -1)},
            {7, New MazeDirection(Of Integer)(3, -1, -2)}
        }
    Shared ReadOnly KnightDoorPositions As IReadOnlyDictionary(Of Integer, (Column As Integer, Row As Integer)) =
        New Dictionary(Of Integer, (Column As Integer, Row As Integer)) From
        {
            {0, (DOOR_COLUMN_RIGHT, 0)},
            {1, (ROOM_COLUMNS - 1, DOOR_ROW_TOP)},
            {2, (ROOM_COLUMNS - 1, DOOR_ROW_BOTTOM)},
            {3, (DOOR_COLUMN_RIGHT, ROOM_ROWS - 1)},
            {4, (DOOR_COLUMN_LEFT, ROOM_ROWS - 1)},
            {5, (0, DOOR_ROW_BOTTOM)},
            {6, (0, DOOR_ROW_TOP)},
            {7, (DOOR_COLUMN_LEFT, 0)}
        }
    Friend Shared ReadOnly KnightDoorDestinationPositions As IReadOnlyDictionary(Of Integer, (Column As Integer, Row As Integer)) =
        New Dictionary(Of Integer, (Column As Integer, Row As Integer)) From
        {
            {0, (DOOR_COLUMN_LEFT, ROOM_ROWS - 2)},
            {1, (1, DOOR_ROW_BOTTOM)},
            {2, (1, DOOR_ROW_TOP)},
            {3, (DOOR_COLUMN_LEFT, 1)},
            {4, (DOOR_COLUMN_RIGHT, 1)},
            {5, (ROOM_COLUMNS - 2, DOOR_ROW_TOP)},
            {6, (ROOM_COLUMNS - 2, DOOR_ROW_BOTTOM)},
            {7, (DOOR_COLUMN_RIGHT, ROOM_ROWS - 2)}
        }
    Sub New(data As WorldData, playSfx As Action(Of String))
        MyBase.New(data, playSfx)
    End Sub

    Public ReadOnly Property Maps As IEnumerable(Of IMap) Implements IWorld.Maps
        Get
            Return Enumerable.
                Range(0, Data.Maps.Count).
                Select(Function(x) New Business.Map(Data, x, AddressOf PlaySfx))
        End Get
    End Property

    Public Property Avatar As ICharacter Implements IWorld.Avatar
        Get
            Return If(
                Data.AvatarCharacterId.HasValue,
                New Character(Data, Data.AvatarCharacterId.Value, AddressOf PlaySfx),
                Nothing)
        End Get
        Set(value As ICharacter)
            Data.AvatarCharacterId = value?.CharacterId
        End Set
    End Property

    Public ReadOnly Property MessageCount As Integer Implements IWorld.MessageCount
        Get
            Return Data.Messages.Count
        End Get
    End Property

    Protected Overrides ReadOnly Property EntityData As WorldData
        Get
            Return Data
        End Get
    End Property
    Public Overrides Sub Clear()
        MyBase.Clear()
        Data.Maps.Clear()
        Data.RecycledMaps.Clear()
        Data.Locations.Clear()
        Data.RecycledLocations.Clear()
        Data.Characters.Clear()
        Data.RecycledCharacters.Clear()
        Data.Messages.Clear()
        Data.Items.Clear()
        Data.RecycledItems.Clear()
        Data.AvatarCharacterId = Nothing
    End Sub
    Public Overrides Sub Initialize()
        MyBase.Initialize()
        SetStatistic(StatisticType.Points, 0)
        SetStatistic(StatisticType.MazeColumns, MAZE_COLUMNS)
        SetStatistic(StatisticType.MazeRows, MAZE_ROWS)
        CreateMaps()
        GenerateMaze()
        CreateCharacters()
        CreateItems()
        AddMessage(MoodType.Info, "Welcome to Scalawag of SPLORR!!")
        AddMessage(MoodType.Info, "Arrows, WASD, ZQSD: MOVE")
        AddMessage(MoodType.Info, "Space: ACTIONS | Backspace: GAME MENU")
    End Sub

    Private Sub CreateItems()
        For Each itemType In ItemTypes.All
            Dim descriptor = itemType.ToItemTypeDescriptor
            Dim candidateMaps = Maps.Where(Function(x) descriptor.CanSpawnMap(x))
            For Each dummy In Enumerable.Range(0, descriptor.itemCount)
                Dim map = RNG.FromEnumerable(candidateMaps)
                Dim candidateLocations = map.Locations.Where(Function(x) descriptor.CanSpawnLocation(x))
                CreateItem(itemType, RNG.FromEnumerable(candidateLocations))
            Next
        Next
    End Sub

    Public Sub AddMessage(mood As String, text As String) Implements IWorld.AddMessage
        Data.Messages.Add(New MessageData With {.Mood = mood, .Text = text})
    End Sub

    Public Sub DismissMessage() Implements IWorld.DismissMessage
        If Data.Messages.Any Then
            Data.Messages.RemoveAt(0)
        End If
    End Sub

    Private Sub GenerateMaze()
        Dim mapIds = New HashSet(Of Integer)(Enumerable.Range(0, EntityData.Maps.Count))
        Dim rooms(MAZE_COLUMNS, MAZE_ROWS) As IMap
        For Each column In Enumerable.Range(0, MAZE_COLUMNS)
            For Each row In Enumerable.Range(0, MAZE_ROWS)
                Dim mapId = RNG.FromEnumerable(mapIds)
                mapIds.Remove(mapId)
                Dim map = GetMap(mapId)
                map.SetStatistic(StatisticType.MazeColumn, column)
                map.SetStatistic(StatisticType.MazeRow, row)
                rooms(column, row) = map
            Next
        Next
        Dim maze As New Maze(Of Integer)(MAZE_COLUMNS, MAZE_ROWS, KnightMazeDirections)
        maze.Generate()
        For Each column In Enumerable.Range(0, MAZE_COLUMNS)
            For Each row In Enumerable.Range(0, MAZE_ROWS)
                Dim mazeCell = maze.GetCell(column, row)
                Dim map = rooms(column, row)
                Dim centerLocation = map.GetLocation(map.Columns \ 2, map.Rows \ 2)
                centerLocation.LocationType = LocationType.Sign
                Dim doorCount = 0
                For Each directionId In KnightMazeDirections.Keys
                    Dim mazeDoor = mazeCell.GetDoor(directionId)
                    If mazeDoor IsNot Nothing AndAlso mazeDoor.Open Then
                        Dim doorPosition = KnightDoorPositions(directionId)
                        Dim doorLocation = map.GetLocation(doorPosition.Column, doorPosition.Row)
                        doorLocation.LocationType = LocationType.Door
                        Dim destinationMap = rooms(column + CInt(KnightMazeDirections(directionId).DeltaX), row + CInt(KnightMazeDirections(directionId).DeltaY))
                        Dim destinationPosition = KnightDoorDestinationPositions(directionId)
                        Dim destinationLocation = destinationMap.GetLocation(destinationPosition.Column, destinationPosition.Row)
                        destinationLocation.SetTag(TagType.DoorExit, True)
                        doorLocation.SetDestinationLocation(destinationLocation)
                        doorCount += 1
                    End If
                Next
                If doorCount = 1 Then
                    map.SetTag(TagType.DeadEnd, True)
                End If
            Next
        Next
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
        Dim result = New Map(Data, mapId, AddressOf PlaySfx)
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
            AddressOf PlaySfx)
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
            AddressOf PlaySfx)
        result.Initialize()
        Return result
    End Function

    Public Function CreateItem(itemType As String, entity As IInventoryEntity) As IItem Implements IWorld.CreateItem
        Dim itemId As Integer
        If Data.RecycledItems.Any Then
            itemId = Data.RecycledItems.First
            Data.RecycledItems.Remove(itemId)
            Data.Items(itemId) = New ItemData With {.ItemType = itemType}
        Else
            itemId = Data.Items.Count
            Data.Items.Add(New ItemData With {.ItemType = itemType})
        End If
        Dim result = New Item(Data, itemId, AddressOf PlaySfx)
        result.Initialize()
        entity.AddItem(result)
        Return result
    End Function

    Public Function GetMap(mapId As Integer) As IMap Implements IWorld.GetMap
        Return New Map(Data, mapId, AddressOf PlaySfx)
    End Function

    Public Function GetLocation(locationId As Integer) As ILocation Implements IWorld.GetLocation
        Return New Location(Data, locationId, AddressOf PlaySfx)
    End Function

    Public Function GetMessage(line As Integer) As IMessage Implements IWorld.GetMessage
        Return New Message(Data, line)
    End Function

    Public Function GetItem(itemId As Integer) As IItem Implements IWorld.GetItem
        Return New Item(Data, itemId, AddressOf PlaySfx)
    End Function

    Public Overrides Sub Recycle()
        Clear()
    End Sub
End Class
