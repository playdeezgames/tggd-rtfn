Imports System.Runtime.CompilerServices
Imports TGGD.UI
Imports TGGDRTFN.Business

Friend Module UIBufferExtensions
    Public Const CharacterCount = 256
    <Extension>
    Friend Sub Fill(
                   buffer As IUIBuffer(Of Integer),
                   Optional character As Byte = 0,
                   Optional foregroundColor As Integer = 0,
                   Optional backgroundColor As Integer = 0)
        buffer.Fill(ToPixel(character, foregroundColor, backgroundColor))
    End Sub

    Private Function ToPixel(
                            character As Byte,
                            foregroundColor As Integer,
                            backgroundColor As Integer) As Integer
        Return character + foregroundColor * CharacterCount + backgroundColor * HueCount * CharacterCount
    End Function

    <Extension>
    Friend Sub Write(
                    buffer As IUIBuffer(Of Integer),
                    column As Integer, row As Integer,
                    text As String,
                    foregroundColor As Integer,
                    backgroundColor As Integer)
        For Each character In text
            buffer.SetPixel(column, row, ToPixel(CByte(AscW(character)), foregroundColor, backgroundColor))
            column += 1
        Next
    End Sub
    <Extension>
    Friend Sub WriteCentered(
                    buffer As IUIBuffer(Of Integer),
                    row As Integer,
                    text As String,
                    foregroundColor As Integer,
                    backgroundColor As Integer)
        buffer.Write((buffer.Columns - text.Length) \ 2, row, text, foregroundColor, backgroundColor)
    End Sub
End Module
