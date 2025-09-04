Friend MustInherit Class ItemTypeDescriptor
    ReadOnly Property ItemType As String
    ReadOnly Property ItemTypeName As String
    ReadOnly Property ItemCount As Integer
    Sub New(
           itemType As String,
           itemTypeName As String,
           itemCount As Integer)
        Me.ItemType = itemType
        Me.ItemTypeName = itemTypeName
        Me.ItemCount = itemCount
    End Sub
    Friend MustOverride Function CanSpawnMap(map As IMap) As Boolean
    Friend MustOverride Function CanSpawnLocation(location As ILocation) As Boolean
    Friend MustOverride Function GetName(item As Item) As String
End Class
