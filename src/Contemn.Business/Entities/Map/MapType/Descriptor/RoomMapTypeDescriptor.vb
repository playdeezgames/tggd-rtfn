Friend Class RoomMapTypeDescriptor
    Inherits BaseRoomMapTypeDescriptor

    Public Sub New()
        MyBase.New(Business.MapType.Room, MAZE_COLUMNS * MAZE_ROWS - BOG_COUNT - MARKET_COUNT)
    End Sub
End Class
