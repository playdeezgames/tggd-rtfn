Imports System.Runtime.CompilerServices

Friend Module CharacterExtensions
    <Extension>
    Friend Sub HandleBump(character As ICharacter, location As ILocation)
        character.CharacterType.ToCharacterTypeDescriptor.OnBump(character, location)
    End Sub
    <Extension>
    Friend Sub HandleLeave(character As ICharacter, location As ILocation)
        character.CharacterType.ToCharacterTypeDescriptor.OnLeave(character, location)
    End Sub
    <Extension>
    Friend Sub HandleEnter(character As ICharacter, location As ILocation)
        character.CharacterType.ToCharacterTypeDescriptor.OnEnter(character, location)
    End Sub

End Module
