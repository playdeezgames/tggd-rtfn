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
            Case UI.Command.Up
                Return HandleMove(DirectionType.North)
            Case UI.Command.Down
                Return HandleMove(DirectionType.South)
            Case UI.Command.Left
                Return HandleMove(DirectionType.West)
            Case UI.Command.Right
                Return HandleMove(DirectionType.East)
            Case Else
                Return Me
        End Select
    End Function

    Private Function HandleMove(directionType As String) As IUIState
        World.Avatar.Move(directionType)
        Return NeutralState.DetermineState(Buffer, World, PlaySfx)
    End Function
End Class
