Imports System.Runtime.CompilerServices
Imports Contemn.Business

Friend Module LocationExtensions
    Private ReadOnly locationPixelTable As IReadOnlyDictionary(Of String, Func(Of ILocation, Integer)) =
        New Dictionary(Of String, Func(Of ILocation, Integer)) From
        {
            {LocationType.Wall, AddressOf WallToPixel},
            {LocationType.Floor, AddressOf FloorToPixel},
            {LocationType.Door, AddressOf DoorToPixel},
            {LocationType.Sign, AddressOf SignToPixel},
            {LocationType.Loo, AddressOf LooToPixel},
            {LocationType.Store, AddressOf StoreToPixel}
        }

    Private Function StoreToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(Asc("$"), Hue.Black, Hue.Green)
    End Function

    Private Function LooToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(21, Hue.Blue, Hue.Black)
    End Function

    Private Function SignToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(Asc("?"), Hue.White, Hue.Black)
    End Function

    Private Function DoorToPixel(location As ILocation) As Integer
        Dim destination = location.GetDestinationLocation()
        If destination.Map.GetTag(TagType.Explored) Then
            Return UIBufferExtensions.ToPixel(Asc("+"), Hue.Black, Hue.Brown)
        Else
            Return UIBufferExtensions.ToPixel(Asc("+"), Hue.Brown, Hue.Black)
        End If
    End Function

    Private Function FloorToPixel(location As ILocation) As Integer

        Return UIBufferExtensions.ToPixel(
            CByte(If(location.GetTag(TagType.Mark), Asc("x"), Asc("."))),
            If(location.GetTag(TagType.Step), Hue.LightGray, Hue.DarkGray),
            Hue.Black)
    End Function

    Private Function WallToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(Asc("#"), Hue.Black, Hue.Blue)
    End Function

    <Extension>
    Friend Function ToPixel(location As ILocation) As Integer
        Return locationPixelTable(location.LocationType)(location)
    End Function
End Module
