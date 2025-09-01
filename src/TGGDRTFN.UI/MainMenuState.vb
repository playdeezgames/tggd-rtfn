Imports TGGD.UI
Imports TGGDRTFN.Business

Friend Class MainMenuState
    Inherits PickerState
    Shared ReadOnly EMBARK_IDENTIFIER As String = NameOf(EMBARK_IDENTIFIER)
    Const EMBARK_TEXT = "Embark!"
    Shared ReadOnly OPTIONS_IDENTIFIER As String = NameOf(OPTIONS_IDENTIFIER)
    Const OPTIONS_TEXT = "Options"
    Shared ReadOnly ABOUT_IDENTIFIER As String = NameOf(ABOUT_IDENTIFIER)
    Const ABOUT_TEXT = "About"
    Public Sub New(
                  buffer As IUIBuffer(Of Integer),
                  world As Business.IWorld,
                  playSfx As Action(Of String))
        MyBase.New(
            buffer,
            world,
            playSfx,
            "Main Menu",
            Hue.Magenta,
            {
                (EMBARK_IDENTIFIER, EMBARK_TEXT),
                (OPTIONS_IDENTIFIER, OPTIONS_TEXT),
                (ABOUT_IDENTIFIER, ABOUT_TEXT)
            })
    End Sub

    Protected Overrides Function HandleMenuItem(identifier As String) As IUIState
        Return Me
    End Function
End Class
