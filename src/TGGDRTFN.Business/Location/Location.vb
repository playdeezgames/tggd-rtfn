Imports TGGDRTFN.Data

Friend Class Location
    Inherits InventoryEntity(Of LocationData)
    Implements ILocation

    Public Sub New(data As WorldData, locationId As Integer, playSfx As Action(Of String))
        MyBase.New(data, playSfx)
        Me.LocationId = locationId
    End Sub

    Public ReadOnly Property LocationId As Integer Implements ILocation.LocationId

    Public ReadOnly Property LocationType As String Implements ILocation.LocationType
        Get
            Return EntityData.LocationType
        End Get
    End Property

    Public ReadOnly Property Column As Integer Implements ILocation.Column
        Get
            Return EntityData.Column
        End Get
    End Property

    Public ReadOnly Property Row As Integer Implements ILocation.Row
        Get
            Return EntityData.Row
        End Get
    End Property

    Public ReadOnly Property Map As IMap Implements ILocation.Map
        Get
            Return New Map(Data, EntityData.MapId, PlaySfx)
        End Get
    End Property

    Protected Overrides ReadOnly Property EntityData As LocationData
        Get
            Return Data.Locations(LocationId)
        End Get
    End Property
End Class
