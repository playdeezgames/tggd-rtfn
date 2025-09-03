Imports System.ComponentModel.Design.Serialization
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
        RenderMap()
        RenderMessages()
        RenderStatistics()
    End Sub

    Private Sub RenderStatistics()
        Dim y As Integer = 0
        Buffer.Write(VIEW_WIDTH, y, World.Avatar.FormatStatistic(StatisticType.Health), Hue.Red, Hue.Black)
        y += 1
        Buffer.Write(VIEW_WIDTH, y, World.Avatar.FormatStatistic(StatisticType.Satiety), Hue.Magenta, Hue.Black)
    End Sub

    Shared ReadOnly moodColors As IReadOnlyDictionary(Of String, (ForegroundColor As Integer, BackgroundColor As Integer)) =
        New Dictionary(Of String, (ForegroundColor As Integer, BackgroundColor As Integer)) From
        {
            {MoodType.Info, (Hue.LightGray, Hue.Black)}
        }


    Private Sub RenderMessages()
        While World.MessageCount > MESSAGE_LINES
            World.DismissMessage()
        End While
        Dim row = VIEW_HEIGHT
        For Each line In Enumerable.Range(0, World.MessageCount)
            Dim message = World.GetMessage(line)
            Dim colors = moodColors(message.Mood)
            Buffer.Write(0, row, message.Text, colors.ForegroundColor, colors.BackgroundColor)
            row += 1
        Next
    End Sub

    Private Sub RenderMap()
        Dim map = World.Avatar.Map
        For Each columnOffset In Enumerable.Range(-VIEW_WIDTH \ 2, VIEW_WIDTH)
            Dim column = World.Avatar.Column + columnOffset
            Dim displayColumn = VIEW_WIDTH \ 2 + columnOffset
            For Each rowOffset In Enumerable.Range(-VIEW_HEIGHT \ 2, VIEW_HEIGHT)
                Dim row = World.Avatar.Row + rowOffset
                Dim displayRow = VIEW_HEIGHT \ 2 + rowOffset
                Dim location = map.GetLocation(column, row)
                If location Is Nothing Then
                    Buffer.Fill(displayColumn, displayRow, 1, 1, character:=&HB0, Hue.Cyan, Hue.Black)
                ElseIf location.HasCharacter Then
                    Buffer.SetPixel(displayColumn, displayRow, location.Character.ToPixel())
                Else
                    Buffer.SetPixel(displayColumn, displayRow, location.ToPixel())
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
