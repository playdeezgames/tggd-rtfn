Friend MustInherit Class CharacterTypeDescriptor
    ReadOnly Property CharacterType As String
    ReadOnly Property CharacterCount As Integer
    Sub New(characterType As String, characterCount As Integer)
        Me.CharacterType = characterType
        Me.CharacterCount = characterCount
    End Sub
    Friend MustOverride Function CanSpawnMap(map As IMap) As Boolean
    Friend MustOverride Function CanSpawnLocation(location As ILocation) As Boolean
    Friend MustOverride Sub OnInitialize(character As Character)
End Class
