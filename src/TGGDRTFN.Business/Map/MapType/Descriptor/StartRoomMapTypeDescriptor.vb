Friend Class StartRoomMapTypeDescriptor
    Inherits MapTypeDescriptor
    Const MAP_COLUMNS = 8
    Const MAP_ROWS = 9

    Public Sub New()
        MyBase.New(Business.MapType.StartRoom, 1)
    End Sub

    Friend Overrides Sub OnInitialize(map As IMap)
        map.Columns = MAP_COLUMNS
        map.Rows = MAP_ROWS
        For Each column In Enumerable.Range(0, map.Columns)
            For Each row In Enumerable.Range(0, map.Rows)
                Dim locationType = If(
                    column = 0 OrElse
                        row = 0 OrElse
                        column = MAP_COLUMNS - 1 OrElse
                        row = MAP_ROWS - 1,
                    Business.LocationType.Wall,
                    Business.LocationType.Floor)
                map.World.CreateLocation(locationType, map, column, row)
            Next
        Next
    End Sub
End Class
