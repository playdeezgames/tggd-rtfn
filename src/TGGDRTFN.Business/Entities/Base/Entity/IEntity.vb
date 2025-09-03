Public Interface IEntity
    Sub Clear()
    Sub Initialize()
    ReadOnly Property World As IWorld
    Sub SetStatistic(statisticType As String, statisticValue As Integer?)
    Function GetStatistic(statisticType As String) As Integer
    Sub SetMetadata(metadataType As String, metadataValue As String)
    Function GetMetadata(metadataType As String) As String
    Sub SetTag(tagType As String, value As Boolean)
    Function GetTag(tagType As String) As Boolean
End Interface
