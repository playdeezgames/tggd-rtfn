Imports TGGDRTFN.Data

Friend Class Map
    Inherits Entity(Of MapData)
    Implements IMap

    Public Sub New(data As WorldData, mapId As Integer, playSfx As Action(Of String))
        MyBase.New(data, playSfx)
        Me.MapId = mapId
    End Sub

    Public ReadOnly Property MapId As Integer Implements IMap.MapId

    Public ReadOnly Property MapType As String Implements IMap.MapType
        Get
            Return EntityData.MapType
        End Get
    End Property

    Protected Overrides ReadOnly Property EntityData As MapData
        Get
            Return Data.Maps(MapId)
        End Get
    End Property

    Public Overrides Sub Initialize()
        MyBase.Initialize()
        MapType.ToMapTypeDescriptor.OnInitialize(Me)
    End Sub
End Class
