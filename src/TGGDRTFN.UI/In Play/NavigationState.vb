Imports System.ComponentModel.Design.Serialization
Imports System.Data.Common
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
        RenderMiniMap()
        RenderMessages()
        RenderStatistics()
    End Sub

    Private Sub RenderMiniMap()
        Dim currentMap = World.Avatar.Map
        Dim currentColumn = currentMap.GetStatistic(StatisticType.MazeColumn)
        Dim currentRow = currentMap.GetStatistic(StatisticType.MazeRow)
        Dim mazeColumns = World.GetStatistic(StatisticType.MazeColumns)
        Dim mazeRows = World.GetStatistic(StatisticType.MazeRows)
        For Each column In Enumerable.Range(0, mazeColumns)
            Dim foreground = MiniMapForeground(currentColumn, column)
            Dim background = MiniMapBackground(currentColumn, column)
            Buffer.Write(MiniMapColumn(column), MINI_MAP_TOP, ColumnText(column), foreground, background)
            Buffer.Write(MiniMapColumn(column), MINI_MAP_TOP + mazeRows + 1, ColumnText(column), foreground, background)
        Next
        For Each row In Enumerable.Range(0, mazeRows)
            Dim foreground = MiniMapForeground(currentRow, row)
            Dim background = MiniMapBackground(currentRow, row)
            Buffer.Write(MINI_MAP_LEFT, MiniMapRow(row), RowText(row), foreground, background)
            Buffer.Write(MINI_MAP_LEFT + mazeColumns + 1, MiniMapRow(row), RowText(row), foreground, background)
        Next
        For Each map In World.Maps
            Dim column = map.GetStatistic(StatisticType.MazeColumn)
            Dim row = map.GetStatistic(StatisticType.MazeRow)
            If currentMap.MapId = map.MapId Then
                Buffer.Write(MiniMapColumn(column), MiniMapRow(row), "@", Hue.White, Hue.Black)
            ElseIf map.GetTag(TagType.Explored) Then
                Buffer.Write(MiniMapColumn(column), MiniMapRow(row), " ", Hue.White, Hue.Black)
            Else
                Buffer.Write(MiniMapColumn(column), MiniMapRow(row), "?", Hue.DarkGray, Hue.Black)
            End If

        Next
    End Sub

    Private Shared Function MiniMapBackground(currentColumn As Integer, column As Integer) As Integer
        Return If(column = currentColumn, Hue.LightGray, Hue.Black)
    End Function

    Private Shared Function MiniMapForeground(currentColumn As Integer, column As Integer) As Integer
        Return If(column = currentColumn, Hue.Black, Hue.LightGray)
    End Function

    Const MINI_MAP_LEFT = VIEW_WIDTH + 5
    Const MINI_MAP_TOP = 0

    Private Shared Function MiniMapColumn(column As Integer) As Integer
        Return MINI_MAP_LEFT + column + 1
    End Function

    Private Shared Function MiniMapRow(row As Integer) As Integer
        Return row + 1
    End Function

    Private Shared Function RowText(row As Integer) As Char
        Return Chr(49 + row)
    End Function

    Private Shared Function ColumnText(column As Integer) As Char
        Return Chr(65 + column)
    End Function

    Private Sub RenderStatistics()
        Dim y As Integer = World.GetStatistic(StatisticType.MazeRows) + 3 + MINI_MAP_TOP
        Buffer.Write(VIEW_WIDTH, y, World.Avatar.FormatStatistic(StatisticType.Health), Hue.Red, Hue.Black)
        y += 1
        Buffer.Write(VIEW_WIDTH, y, World.Avatar.FormatStatistic(StatisticType.Satiety), Hue.Magenta, Hue.Black)
        y += 1
        Buffer.Write(VIEW_WIDTH, y, World.Avatar.FormatStatistic(StatisticType.Hydration), Hue.Blue, Hue.Black)
        y += 1
        Buffer.Write(VIEW_WIDTH, y, World.Avatar.FormatStatistic(StatisticType.Points), Hue.Green, Hue.Black)
        y += 1
        Buffer.Write(VIEW_WIDTH, y, World.Avatar.FormatStatistic(StatisticType.Score), Hue.LightGreen, Hue.Black)
    End Sub

    Shared ReadOnly moodColors As IReadOnlyDictionary(Of String, (ForegroundColor As Integer, BackgroundColor As Integer)) =
        New Dictionary(Of String, (ForegroundColor As Integer, BackgroundColor As Integer)) From
        {
            {MoodType.Info, (Hue.LightGray, Hue.Black)},
            {MoodType.Danger, (Hue.Black, Hue.Red)},
            {MoodType.Warning, (Hue.Black, Hue.Yellow)}
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
                RenderLocation(displayColumn, displayRow, location)
            Next
        Next
    End Sub

    Private Sub RenderLocation(displayColumn As Integer, displayRow As Integer, location As ILocation)
        If location Is Nothing Then
            Buffer.Fill(displayColumn, displayRow, 1, 1, character:=&HB0, Hue.Cyan, Hue.Black)
        ElseIf location.HasCharacter Then
            Buffer.SetPixel(displayColumn, displayRow, location.Character.ToPixel())
        ElseIf location.HasItems Then
            Buffer.SetPixel(displayColumn, displayRow, location.Items.First().ToPixel())
        Else
            Buffer.SetPixel(displayColumn, displayRow, location.ToPixel())
        End If
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        Select Case command
            Case UI.Command.Red
                Return New GameMenuState(Buffer, World, PlaySfx)
            Case UI.Command.Up
                Return HandleMove(VerbType.MoveNorth)
            Case UI.Command.Down
                Return HandleMove(VerbType.MoveSouth)
            Case UI.Command.Left
                Return HandleMove(VerbType.MoveWest)
            Case UI.Command.Right
                Return HandleMove(VerbType.MoveEast)
            Case UI.Command.Green
                Return New DialogState(Buffer, World, PlaySfx, World.Avatar.Perform(VerbType.ActionList))
            Case Else
                Return Me
        End Select
    End Function

    Private Function HandleMove(verbType As String) As IUIState
        Dim dialog = World.Avatar.Perform(verbType)
        If dialog IsNot Nothing Then
            Return New DialogState(Buffer, World, PlaySfx, dialog)
        End If
        Return NeutralState.DetermineState(Buffer, World, PlaySfx)
    End Function
End Class
