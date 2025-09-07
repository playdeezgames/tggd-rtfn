Friend Module Constants
    Friend Const ROOM_COLUMNS = 25
    Friend Const ROOM_ROWS = 25
    Friend Const MAZE_COLUMNS = 8
    Friend Const MAZE_ROWS = 8
    Friend Const BOG_COUNT = 4
    Friend Const DOOR_COLUMN_LEFT = (ROOM_COLUMNS \ 4)
    Friend Const DOOR_COLUMN_RIGHT = (ROOM_COLUMNS \ 4) * 3
    Friend Const DOOR_ROW_TOP = (ROOM_ROWS \ 4)
    Friend Const DOOR_ROW_BOTTOM = (ROOM_ROWS \ 4) * 3
    Friend ReadOnly NEVER_MIND_CHOICE As String = NameOf(NEVER_MIND_CHOICE)
    Friend Const NEVER_MIND_TEXT = "Never Mind"
    Friend ReadOnly OK_IDENTIFIER As String = NameOf(OK_IDENTIFIER)
    Friend Const OK_TEXT = "OK"
    Friend ReadOnly INVENTORY_IDENTIFIER As String = NameOf(INVENTORY_IDENTIFIER)
    Friend Const INVENTORY_TEXT = "Inventory"
    Friend ReadOnly ACTIONS_IDENTIFIER As String = NameOf(ACTIONS_IDENTIFIER)
    Friend Const ACTIONS_TEXT = "Actions"
End Module
