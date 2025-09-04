Imports TGGD.Business

Public Interface ICharacter
    Inherits IInventoryEntity
    ReadOnly Property CharacterType As String
    ReadOnly Property CharacterId As Integer
    ReadOnly Property Location As ILocation
    ReadOnly Property Map As IMap
    Function Perform(verbType As String) As IDialog
    Sub MoveTo(destination As ILocation)
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
End Interface
