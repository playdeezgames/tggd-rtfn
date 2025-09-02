Imports TGGD.UI
Imports TGGDRTFN.Business

Friend Class NavigationState
    Inherits BaseState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String))
        MyBase.New(buffer, world, playSfx)
    End Sub

    Public Overrides Sub Refresh()
        Buffer.Fill
        Dim map = World.Avatar.Map
        For Each column In Enumerable.Range(0, map.Columns)
            For Each row In Enumerable.Range(0, map.Rows)
                Dim location = map.GetLocation(column, row)
                If location.HasCharacter Then
                    Buffer.SetPixel(column, row, location.Character.ToPixel())
                Else
                    Buffer.SetPixel(column, row, location.ToPixel())
                End If
            Next
        Next
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        Select Case command
            Case UI.Command.Red
                Return New GameMenuState(Buffer, World, PlaySfx)
            Case Else
                Return Me
        End Select
    End Function
End Class
