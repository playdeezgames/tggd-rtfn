Imports TGGDRTFN.Data

Public Class World
    Inherits Entity(Of WorldData)
    Implements IWorld
    Sub New(data As WorldData, playSfx As Action(Of String))
        MyBase.New(playSfx)
        Me.EntityData = data
    End Sub
    Protected Overrides ReadOnly Property EntityData As WorldData
    Public Overrides Sub Clear()
        MyBase.Clear()
    End Sub
End Class
