Imports System.Runtime.CompilerServices

Public Module WorldExtensions
    <Extension>
    Sub Initialize(world As IWorld)
        world.Clear()
    End Sub
End Module
