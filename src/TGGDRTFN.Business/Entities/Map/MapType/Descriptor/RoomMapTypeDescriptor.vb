Friend Class RoomMapTypeDescriptor
    Inherits BaseRoomMapTypeDescriptor

    Public Sub New()
        MyBase.New(Business.MapType.Room, MAZE_COLUMNS * MAZE_ROWS - 1)
    End Sub
End Class
