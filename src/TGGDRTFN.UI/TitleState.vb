Imports TGGD.UI
Imports TGGDRTFN.Business

Friend Class TitleState
    Inherits BaseState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String))
        MyBase.New(buffer, world, playSfx)
    End Sub

    Public Overrides Sub Refresh()
        Buffer.Fill()
        Buffer.Write(0, 0, "Hello, world!", Hue.LightGray, Hue.DarkGray)
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        Return Me
    End Function
End Class
