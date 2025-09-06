Imports System.Runtime.CompilerServices
Imports TGGDRTFN.Business

Friend Module ItemExtensions
    Private ReadOnly itemPixelTable As IReadOnlyDictionary(Of String, Func(Of IItem, Integer)) =
        New Dictionary(Of String, Func(Of IItem, Integer)) From
        {
            {ItemType.Food, AddressOf FoodToPixel},
            {ItemType.Points, AddressOf PointsToPixel}
        }

    Private Function PointsToPixel(item As IItem) As Integer
        Return UIBufferExtensions.ToPixel(19, Hue.LightBlue, Hue.Black)
    End Function

    Private Function FoodToPixel(item As IItem) As Integer
        Return UIBufferExtensions.ToPixel(Asc("+"), Hue.Green, Hue.Black)
    End Function

    <Extension>
    Friend Function ToPixel(item As IItem) As Integer
        Return itemPixelTable(item.ItemType)(item)
    End Function

End Module
