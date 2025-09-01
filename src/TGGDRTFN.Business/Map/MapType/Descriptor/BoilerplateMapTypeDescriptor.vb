Friend Class BoilerplateMapTypeDescriptor
    Inherits MapTypeDescriptor

    Public Sub New()
        MyBase.New(Business.MapType.Boilerplate, 1)
    End Sub

    Friend Overrides Sub OnInitialize(map As Map)
        Throw New NotImplementedException
    End Sub
End Class
