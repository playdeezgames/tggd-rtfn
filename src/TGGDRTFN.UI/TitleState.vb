Imports TGGD.UI

Friend Class TitleState
    Inherits BaseState

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String))
        MyBase.New(buffer, world, playSfx)
    End Sub

    Public Overrides Sub Refresh()
        Buffer.Fill(AscW("@"), Business.Hue.LightGray, Business.Hue.Black)
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        Return Me
    End Function
End Class
