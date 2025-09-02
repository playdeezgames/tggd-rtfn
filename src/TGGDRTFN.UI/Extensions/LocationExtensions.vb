Imports System.Runtime.CompilerServices
Imports TGGDRTFN.Business

Friend Module LocationExtensions
    Private ReadOnly locationPixelTable As IReadOnlyDictionary(Of String, Func(Of ILocation, Integer)) =
        New Dictionary(Of String, Func(Of ILocation, Integer)) From
        {
            {LocationType.Wall, AddressOf WallToPixel},
            {LocationType.Floor, AddressOf FloorToPixel},
            {LocationType.Door, AddressOf DoorToPixel},
            {LocationType.Sign, AddressOf SignToPixel}
        }

    Private Function SignToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(Asc("?"), Hue.White, Hue.Black)
    End Function

    Private Function DoorToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(Asc("+"), Hue.Brown, Hue.Black)
    End Function

    Private Function FloorToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(Asc("."), Hue.DarkGray, Hue.Black)
    End Function

    Private Function WallToPixel(location As ILocation) As Integer
        Return UIBufferExtensions.ToPixel(Asc("#"), Hue.Black, Hue.Blue)
    End Function

    <Extension>
    Friend Function ToPixel(location As ILocation) As Integer
        Return locationPixelTable(location.LocationType)(location)
    End Function
End Module
