Imports TGGDRTFN.Data

Public Class World
    Inherits Entity(Of WorldData)
    Implements IWorld
    Sub New(data As WorldData, playSfx As Action(Of String))
        MyBase.New(data, playSfx)
    End Sub
    Protected Overrides ReadOnly Property EntityData As WorldData
        Get
            Return Data
        End Get
    End Property
    Public Overrides Sub Clear()
        MyBase.Clear()
    End Sub
    Public Overrides Sub Initialize()
        MyBase.Initialize()
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
        Throw New NotImplementedException()
    End Function

    Public Function CreateCharacter(characterType As String, location As ILocation) As ICharacter Implements IWorld.CreateCharacter
        Throw New NotImplementedException()
    End Function

    Public Function CreateItem(itemType As String, entity As IInventoryEntity) As IItem Implements IWorld.CreateItem
        Throw New NotImplementedException()
    End Function
End Class
