﻿Imports System.Runtime.CompilerServices

Friend Module ItemTypes
    Friend ReadOnly Descriptors As IReadOnlyDictionary(Of String, ItemTypeDescriptor) =
        New List(Of ItemTypeDescriptor) From
        {
            New FoodItemTypeDescriptor(),
            New PointsItemTypeDescriptor(),
            New WaterBottleItemTypeDescriptor(),
            New EmptyBottleItemTypeDescriptor()
        }.ToDictionary(Function(x) x.ItemType, Function(x) x)
    Friend ReadOnly Property All As IEnumerable(Of String)
        Get
            Return Descriptors.Keys
        End Get
    End Property
    <Extension>
    Function ToItemTypeDescriptor(itemType As String) As ItemTypeDescriptor
        Return Descriptors(itemType)
    End Function

End Module
