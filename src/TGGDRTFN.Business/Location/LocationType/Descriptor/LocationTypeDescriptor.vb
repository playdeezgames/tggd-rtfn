Friend MustInherit Class LocationTypeDescriptor
    Friend ReadOnly Property LocationType As String
    Friend ReadOnly Property IsSolid As Boolean
    Sub New(locationType As String, isSolid As Boolean)
        Me.LocationType = locationType
        Me.IsSolid = isSolid
    End Sub
End Class
