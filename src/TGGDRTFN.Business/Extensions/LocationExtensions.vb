Imports System.Runtime.CompilerServices

Friend Module LocationExtensions
    <Extension>
    Friend Sub HandleBump(location As ILocation, character As ICharacter)
        location.LocationType.ToLocationTypeDescriptor.OnBump(location, character)
    End Sub
    <Extension>
    Friend Sub HandleLeave(location As ILocation, character As ICharacter)
    End Sub
    <Extension>
    Friend Sub HandleEnter(location As ILocation, character As ICharacter)
    End Sub
End Module
