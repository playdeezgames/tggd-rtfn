Imports TGGD.Business
Imports TGGD.UI

Friend Class DialogState
    Inherits BaseState

    ReadOnly dialog As IDialog

    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String),
                  dialog As IDialog)
        MyBase.New(buffer, world, playSfx)
        Me.dialog = dialog
    End Sub

    Public Overrides Sub Refresh()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function HandleCommand(command As String) As IUIState
        Throw New NotImplementedException()
    End Function
End Class
