Imports System.Transactions
Imports TGGD.Business
Imports TGGDRTFN.Data

Friend Class Character
    Inherits InventoryEntity(Of CharacterData)
    Implements ICharacter

    Public Sub New(data As WorldData, characterId As Integer, playSfx As Action(Of String))
        MyBase.New(data, playSfx)
        Me.CharacterId = characterId
    End Sub

    Public ReadOnly Property CharacterType As String Implements ICharacter.CharacterType
        Get
            Return EntityData.CharacterType
        End Get
    End Property

    Public ReadOnly Property CharacterId As Integer Implements ICharacter.CharacterId

    Public ReadOnly Property Location As ILocation Implements ICharacter.Location
        Get
            Return New Location(Data, EntityData.LocationId, PlaySfx)
        End Get
    End Property

    Public ReadOnly Property Map As IMap Implements ICharacter.Map
        Get
            Return Location.Map
        End Get
    End Property

    Public ReadOnly Property Column As Integer Implements ICharacter.Column
        Get
            Return Location.Column
        End Get
    End Property

    Public ReadOnly Property Row As Integer Implements ICharacter.Row
        Get
            Return Location.Row
        End Get
    End Property

    Protected Overrides ReadOnly Property EntityData As CharacterData
        Get
            Return Data.Characters(CharacterId)
        End Get
    End Property
    Public Overrides Sub Initialize()
        MyBase.Initialize()
        Data.Locations(EntityData.LocationId).CharacterId = CharacterId
        CharacterType.ToCharacterTypeDescriptor().OnInitialize(Me)
    End Sub

    Public Function Perform(verbType As String, directionType As String) As IDialog Implements ICharacter.Perform
        Dim descriptor = directionType.ToDirectionTypeDescriptor
        Dim nextColumn = descriptor.GetNextColumn(Column)
        Dim nextRow = descriptor.GetNextRow(Row)
        Dim nextLocation = Map.GetLocation(nextColumn, nextRow)
        MoveTo(nextLocation)
        Return Nothing
    End Function

    Public Sub MoveTo(destination As ILocation) Implements ICharacter.MoveTo
        If destination Is Nothing Then
            Return
        End If
        If CanEnter(destination) Then
            Enter(destination)
            Return
        End If
        Bump(destination)
    End Sub

    Private Sub Bump(nextLocation As ILocation)
        Me.HandleBump(nextLocation)
        nextLocation.HandleBump(Me)
    End Sub

    Private Sub Enter(nextLocation As ILocation)
        Me.HandleLeave(Location)
        Location.HandleLeave(Me)
        Data.Locations(EntityData.LocationId).CharacterId = Nothing
        EntityData.LocationId = nextLocation.LocationId
        Data.Locations(EntityData.LocationId).CharacterId = CharacterId
        Me.HandleEnter(Location)
        Location.HandleEnter(Me)
    End Sub

    Private Function CanEnter(nextLocation As ILocation) As Boolean
        Return nextLocation.LocationType.ToLocationTypeDescriptor.CanEnter(nextLocation, Me)
    End Function
End Class
