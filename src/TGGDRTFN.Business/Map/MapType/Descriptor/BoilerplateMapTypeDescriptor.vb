Friend Class BoilerplateMapTypeDescriptor
    Inherits MapTypeDescriptor

    Public Sub New()
        MyBase.New(Business.MapType.Boilerplate, 1)
    End Sub

    Friend Overrides Sub OnInitialize(map As IMap)
        map.Columns = 8
        map.Rows = 9
        For Each column In Enumerable.Range(0, map.Columns)
            For Each row In Enumerable.Range(0, map.Rows)
                map.World.CreateLocation(LocationType.Floor, map, column, row)
            Next
        Next
    End Sub
End Class
