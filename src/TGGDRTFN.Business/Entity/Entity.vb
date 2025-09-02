Imports TGGDRTFN.Data

Public MustInherit Class Entity(Of TEntityData As EntityData)
    Implements IEntity
    Protected ReadOnly Property PlaySfx As Action(Of String)
    Protected ReadOnly Data As WorldData
    Protected MustOverride ReadOnly Property EntityData As TEntityData

    Public ReadOnly Property World As IWorld Implements IEntity.World
        Get
            Return New World(Data, PlaySfx)
        End Get
    End Property

    Sub New(data As WorldData, playSfx As Action(Of String))
        Me.Data = data
        Me.PlaySfx = playSfx
    End Sub
    Public Overridable Sub Clear() Implements IEntity.Clear
        EntityData.Statistics.Clear()
    End Sub

    Public Overridable Sub Initialize() Implements IEntity.Initialize
        Clear()
    End Sub

    Public Sub SetStatistic(statisticType As String, statisticValue As Integer?) Implements IEntity.SetStatistic
        If statisticValue.HasValue Then
            EntityData.Statistics(statisticType) = statisticValue.Value
        Else
            EntityData.Statistics.Remove(statisticType)
        End If
    End Sub

    Public Function GetStatistic(statisticType As String) As Integer Implements IEntity.GetStatistic
        Return EntityData.Statistics(statisticType)
    End Function

    Public Sub SetMetadata(metadataType As String, metadataValue As String) Implements IEntity.SetMetadata
        If metadataValue IsNot Nothing Then
            EntityData.Metadatas(metadataType) = metadataValue
        Else
            EntityData.Metadatas.Remove(metadataType)
        End If
    End Sub

    Public Function GetMetadata(metadataType As String) As String Implements IEntity.GetMetadata
        Dim result As String = Nothing
        If EntityData.Metadatas.TryGetValue(metadataType, result) Then
            Return result
        End If
        Return Nothing
    End Function
End Class
